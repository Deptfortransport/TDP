// *********************************************** 
// NAME                 : DataAccess.cs
// AUTHOR               : Steve Tsang
// DATE CREATED         : 26/06/2008
// DESCRIPTION			: Data Access classed used for saving/loading data from the database
// ************************************************ 

using System;
using System.Collections.Generic;
using System.Text;
using TransportDirect.Common.DatabaseInfrastructure;
using System.Collections;
using System.Data.SqlClient;
using System.Data;
using TransportDirect.Common;
using TransportDirect.Common.Logging;
using Logger = System.Diagnostics.Trace;

namespace TransportDirect.Common.RailTicketType
{
    /// <remarks>Singleton Class</remarks>
    public class DataAccess
    {
        private static DataAccess instance = null;

        /// <summary>
        /// Data Access Constructor
        /// </summary>
        private DataAccess()
        {
        }

        /// <summary>
        /// Method for returning static DataAccess instance
        /// </summary>
        public static DataAccess Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DataAccess();
                }

                return instance;
            }
        }

        /// <summary>
        /// Saves a Ticket object to the database
        /// </summary>
        /// <param name="ticket">Ticket object to be saved</param>
        public void SaveTicketToDB(Ticket ticket)
        {
            SqlHelper helper = new SqlHelper();

            try
            {
                helper.ConnOpen(SqlHelperDatabase.TransientPortalDB);
                //tx = helper.sqlConn.BeginTransaction(IsolationLevel.ReadCommitted);


                // Save the Main part of the Ticket object
                Hashtable ticketDescription = new System.Collections.Hashtable();
                ticketDescription.Add("@TicketTypeCode", ticket.TicketTypeCode);
                ticketDescription.Add("@TicketTypeName", ticket.TicketTypeName);
                ticketDescription.Add("@Description", ticket.Description);
                ticketDescription.Add("@Validity", ticket.Validity);
                ticketDescription.Add("@Sleepers", ticket.Sleepers);
                ticketDescription.Add("@FareCategory", ticket.FareCategory);
                ticketDescription.Add("@GroupSave", ticket.GroupSave);
                ticketDescription.Add("@Discounts", ticket.Discounts);
                ticketDescription.Add("@Availability", ticket.Availability);
                ticketDescription.Add("@Retailing", ticket.Retailing);
                ticketDescription.Add("@BookingDeadlines", ticket.BookingDeadlines);
                ticketDescription.Add("@Refunds", ticket.Refunds);
                ticketDescription.Add("@BreaksOfJourney", ticket.BreaksOfJourney);
                ticketDescription.Add("@ChangesToTravelPlans", ticket.ChangesToTravelPlans);
                ticketDescription.Add("@Packages", ticket.Packages);
                ticketDescription.Add("@Conditions", ticket.Conditions);
                ticketDescription.Add("@Easements", ticket.Easements);
                ticketDescription.Add("@InternetOnly", ticket.InternetOnly);
                helper.Execute("SaveTrainTypeDescription", ticketDescription);

                // Save the Applicable Tocs for the Ticket object
                Hashtable applicableTocs = new Hashtable();
                applicableTocs.Add("@AllTocs", ticket.ApplicableTocs.AllTocs);
                applicableTocs.Add("@TocAndConnections", ticket.ApplicableTocs.TocsAndConnections);
                applicableTocs.Add("@TicketTypeCode", ticket.TicketTypeCode);

                //Returns the ID for Applicable Tocs records, used to associate with the included/excluded Tocs 
                string applicableTocsID = helper.GetScalar("SaveApplicableTocs", applicableTocs).ToString();

                //Save the excluded tocs
                Hashtable excludedTocs = null;
                foreach (ExcludedTOCs exToc in ticket.ApplicableTocs.ExcludedTocs)
                {
                    excludedTocs = new Hashtable();
                    excludedTocs.Add(@"TocRef", exToc.TocsRef);
                    excludedTocs.Add(@"AtocName", exToc.AtocName);
                    excludedTocs.Add(@"ApplicableTocs_Id", applicableTocsID);
                    helper.Execute("SaveExcludedTocs", excludedTocs);
                }

                //Save the included tocs
                //Save the validity codes
                Hashtable includedTocs = null;
                foreach (IncludedTOCs inToc in ticket.ApplicableTocs.IncludedTocs)
                {
                    includedTocs = new Hashtable();
                    includedTocs.Add(@"TocRef", inToc.TocRef);
                    includedTocs.Add(@"AtocName", inToc.AtocName);
                    includedTocs.Add(@"ApplicableTocs_Id", applicableTocsID);
                    helper.Execute("SaveIncludedTocs", includedTocs);
                }

                //Save the validity codes
                if (ticket.ValidityCodes != null)
                {
                    Hashtable validityCodes = new Hashtable();
                    validityCodes.Add(@"TicketTypeCode", ticket.TicketTypeCode);
                    helper.Execute("DeleteValidityCode", validityCodes);
                    foreach (string valCode in ticket.ValidityCodes.ValidityCode)
                    {
                        validityCodes = new Hashtable();
                        validityCodes.Add(@"ValidityCode", valCode);
                        validityCodes.Add(@"TicketTypeCode", ticket.TicketTypeCode);
                        helper.Execute("SaveValidityCodes", validityCodes);
                    }
                }
            }
            catch (TDException tdex)
            {
                Logger.Write(new OperationalEvent(TDEventCategory.Database,
                TDTraceLevel.Error, "TransportDirect.Common.RailTicketType.DataAccess - " + tdex.Message));
            }
            catch (Exception ex)
            {
                Logger.Write(new OperationalEvent(TDEventCategory.Database,
                TDTraceLevel.Error, "TransportDirect.Common.RailTicketType.DataAccess - " + ex.Message));
            }
            finally
            {
                helper.ConnClose();
            }
            
        }

        /// <summary>
        /// Checks to see if  a Ticket object exists in the database
        /// </summary>
        /// <param name="TicketCode">The unique identifier for the Ticket object to load</param>
        /// <returns>True/False if ticket exists</returns>
        public bool DoesTicketExist(string TicketCode)
        {
            SqlHelper helper = new SqlHelper();
            bool rtn = false;

            try
            {
                helper.ConnOpen(SqlHelperDatabase.TransientPortalDB);
                //tx = helper.sqlConn.BeginTransaction(IsolationLevel.ReadCommitted);


                // Save the Main part of the Ticket object
                Hashtable ticketDescription = new System.Collections.Hashtable();
                ticketDescription.Add("@TicketTypeCode", TicketCode);

                rtn = Convert.ToBoolean(helper.GetScalar("DoesTicketExist", ticketDescription));
            }
            catch (TDException tdex)
            {
                Logger.Write(new OperationalEvent(TDEventCategory.Database,
                                TDTraceLevel.Error, "TransportDirect.Common.RailTicketType.DataAccess.DoesTicketExist - " + tdex.Message));
            }
            catch (Exception ex)
            {
                Logger.Write(new OperationalEvent(TDEventCategory.Database,
                TDTraceLevel.Error, "TransportDirect.Common.RailTicketType.DataAccess.DoesTicketExist - " + ex.Message));
            }
            finally
            {
                helper.ConnClose();
            }

            return rtn;
        }

        /// <summary>
        /// Loads a Ticket object from the database
        /// </summary>
        /// <param name="TicketCode">The unique identifier for the Ticket object to load</param>
        /// <returns>A Ticket object</returns>
        public Ticket LoadTicketFromDB(string TicketCode)
        {

            SqlHelper helper = new SqlHelper();
            DataSet ticketDataSet = new DataSet();
            DataTableReader ticketDR = null;
            
            string ticketTypeCode = string.Empty;
            string ticketTypeName = string.Empty;
            string description = string.Empty;
            ApplicableTOCs applicableTocs = null;
            ValidityCodes validityCodes = null;
            List<string> valcodes = new List<string>();
            string fareCategory = string.Empty;
            bool groupSave = false;
            string discounts = string.Empty;
            string availability = string.Empty;
            string retailing = string.Empty;
            string bookingDeadlines = string.Empty;
            string changesToTravelPlans = string.Empty;
            string refunds = string.Empty;
            string breaksOfJourney = string.Empty; ;
            string validity = string.Empty;
            string sleepers = string.Empty;
            string packages = string.Empty;
            string conditions = string.Empty;
            string easement = string.Empty;
            string internetonly = string.Empty;

            try
            {
                //Load the ticket data from the database
                helper.ConnOpen(SqlHelperDatabase.TransientPortalDB);
                Hashtable ticketTypeParameters = new Hashtable();
                ticketTypeParameters.Add("@TicketTypeCode", TicketCode);
                ticketDataSet = helper.GetDataSet("loadTicket", ticketTypeParameters);

                //Ticket Information
                ticketDR = ticketDataSet.Tables[0].CreateDataReader();

                //Ticket Type Description
                while (ticketDR.Read())
                {
                    ticketTypeCode = ticketDR["TicketTypeCode"].ToString();
                    ticketTypeName = ticketDR["TicketTypeName"].ToString();
                    description = ticketDR["Description"].ToString();
                    fareCategory = ticketDR["FareCategory"].ToString();
                    groupSave = Convert.ToBoolean(ticketDR["GroupSave"].ToString());
                    discounts = ticketDR["Discounts"].ToString();
                    availability = ticketDR["Availability"].ToString();
                    retailing = ticketDR["Retailing"].ToString();
                    bookingDeadlines = ticketDR["BookingDeadlines"].ToString();
                    changesToTravelPlans = ticketDR["ChangesToTravelPlans"].ToString();
                    refunds = ticketDR["Refunds"].ToString();
                    breaksOfJourney = ticketDR["BreaksOfJourney"].ToString();
                    validity = ticketDR["Validity"].ToString();
                    sleepers = ticketDR["Sleepers"].ToString();
                    packages = ticketDR["Packages"].ToString();
                    conditions = ticketDR["Conditions"].ToString();
                    easement = ticketDR["Easements"].ToString();
                    internetonly = ticketDR["InternetOnly"].ToString();
                }

                //Validity Codes
                ticketDR = ticketDataSet.Tables[1].CreateDataReader();
                while (ticketDR.Read())
                {
                    valcodes.Add(ticketDR["ValidityCode"].ToString());
                }

                //Applicable Tocs
                ticketDR = ticketDataSet.Tables[2].CreateDataReader();
                applicableTocs = new ApplicableTOCs();
                while (ticketDR.Read())
                {
                    applicableTocs.TocsAndConnections = ticketDR["TocAndConnections"].ToString();
                    applicableTocs.AllTocs = Convert.ToBoolean(ticketDR["AllTocs"].ToString());
                }

                //Excluded Tocs
                ticketDR = ticketDataSet.Tables[3].CreateDataReader();
                while (ticketDR.Read())
                {
                    applicableTocs.ExcludedTocs.Add(new ExcludedTOCs(ticketDR["AtocName"].ToString(), ticketDR["TocRef"].ToString()));
                }

                //Excluded Tocs
                ticketDR = ticketDataSet.Tables[4].CreateDataReader();
                while (ticketDR.Read())
                {
                    applicableTocs.IncludedTocs.Add(new IncludedTOCs(ticketDR["AtocName"].ToString(), ticketDR["TocRef"].ToString()));
                }
 
            }
            catch (TDException tdex)
            {
                Logger.Write(new OperationalEvent(TDEventCategory.Database,
                                TDTraceLevel.Error, "TransportDirect.Common.RailTicketType.DataAccess - " + tdex.Message));
            }
            catch (Exception ex)
            {
                Logger.Write(new OperationalEvent(TDEventCategory.Database,
                TDTraceLevel.Error, "TransportDirect.Common.RailTicketType.DataAccess - " + ex.Message));
            }
            finally
            {
                ticketDR.Close();
                helper.ConnClose();
            }

            return new Ticket(ticketTypeCode, ticketTypeName, description, applicableTocs,
                validityCodes, validity, sleepers, fareCategory,
                groupSave, discounts, availability, retailing,
                bookingDeadlines, refunds, breaksOfJourney,
                changesToTravelPlans, packages, conditions, easement, internetonly);

        }
    }
}
