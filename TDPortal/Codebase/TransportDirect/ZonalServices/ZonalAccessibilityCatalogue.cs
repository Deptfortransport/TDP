// *********************************************** 
// NAME                 : ZonalAccessibilityCatalogue.cs
// AUTHOR               : Sanjeev Johal
// DATE CREATED         : 17/06/2008
// DESCRIPTION			: This class contains the reference data that will be used by Zonal Accessibility
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ZonalServices/ZonalAccessibilityCatalogue.cs-arc  $
//
//   Rev 1.5   Oct 10 2012 14:31:02   mmodi
//Updated trace logging level
//Resolution for 5859: Error message logging tidy-up
//
//   Rev 1.4   Jul 17 2008 13:08:06   apatel
//change the getzonalaccessiblitylinks to return accessibility links based on naptan only.
//Resolution for 5071: Zonal accessibilty links do not display in the correct section of the screen
//
//   Rev 1.3   Jul 11 2008 13:24:12   apatel
//zonal accessibility Catalogue updated to handle the null values.
//Resolution for 5033: CCN0458 - Accessability Updates improve linking
//Resolution for 5057: Zonal Accessibility links showing the same text in diagram
//
//   Rev 1.2   Jul 08 2008 09:25:56   apatel
//Accessibility link CCN 458 updates
//Resolution for 5033: CCN0458 - Accessability Updates improve linking
//
//   Rev 1.1   Jul 03 2008 13:27:50   apatel
//change the namespage zonalaccessibility to zonalservices
//Resolution for 5033: CCN0458 - Accessability Updates improve linking
//
//   Rev 1.0   Jun 27 2008 09:44:30   apatel
//Initial revision.
//Resolution for 5033: CCN0458 - Accessability Updates improve linking
//
//   Rev 1.0   Jun 17 2008 11:00:00   sjohal
//Initial revision.

using System;
using System.Collections;
using System.Diagnostics;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common; 
using TransportDirect.Common.Logging;
using TransportDirect.Common.DatabaseInfrastructure;
using TransportDirect.UserPortal.DataServices;
using System.Data.SqlClient;
using TransportDirect.UserPortal.ExternalLinkService;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.Presentation.InteractiveMapping;
using System.Globalization;

namespace TransportDirect.UserPortal.ZonalServices
{
	/// <summary>
	/// Contains the reference data that will be used by Zonal Accessibility
	/// </summary>
	public class ZonalAccessibilityCatalogue
	{
        #region Private Members

        private Hashtable zonalAccessibilityLinks = new Hashtable();
        //Properties used for loading the reference data
        private Hashtable zonalAccessibilityLinksCurrentLoad = new Hashtable();
        // hashtable for loading adminarea & district links
        private Hashtable hashAdminAreaDistrictLinks = new Hashtable();
        // hashtable for loading region, operator and mode links
        private Hashtable hashRegionOperatorModeLinks = new Hashtable();
        private const string CONST_NOLINK = "NOLINK";

        /// <summary>
        /// Structure used to define hashkey for accessing data cached in hashAdminAreaDistrictLinks hashtable
        /// </summary>
        private struct AdminDistrictKey
        {
            public string AdminAreaId, DistrictId;

            public AdminDistrictKey(string AdminAreaId, string DistrictId)
            {
                this.AdminAreaId = AdminAreaId;
                this.DistrictId = DistrictId;
            }
        }

        /// <summary>
        /// Structure used to define hashkey for accessing data cached in hashRegionOperatorModeLinks hashtable
        /// </summary>
        private struct RegionOperatorModeKey
        {
            public string RegionId, OperatorCode, ModeId;

            public RegionOperatorModeKey(string RegionId, string OperatorCode, string ModeId)
            {
                this.RegionId = RegionId;
                this.OperatorCode = OperatorCode;
                this.ModeId = ModeId;
            }
        }

        #endregion

        #region Constructor

        public ZonalAccessibilityCatalogue()
        {
            Load();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Connects to the PermanentPortal database and retrieves the reference data, which
        /// is loaded into memory at runtime
        /// </summary>
        private void Load()
        {
            SqlHelper helper = new SqlHelper();
            try
            {
                helper.ConnOpen(SqlHelperDatabase.TransientPortalDB);

                #region Load zonal accessibility links
                //Retrieve the reference data into a flat table of results
                SqlDataReader reader = helper.GetReader("GetZonalAccessibilityData", new Hashtable(), new Hashtable());

                //Assign the column ordinals
                int naptanColumnOrdinal = reader.GetOrdinal("Naptan");
                int externalLinkIdColumnOrdinal = reader.GetOrdinal("ExternalLinkID");

                zonalAccessibilityLinksCurrentLoad.Clear();

                while (reader.Read())
                {
                    //Add a reference to each external link against its natpan
                    string naptan = reader.IsDBNull(naptanColumnOrdinal) ? string.Empty : reader.GetString(naptanColumnOrdinal);
                    string externalLinkId = reader.IsDBNull(externalLinkIdColumnOrdinal) ? string.Empty : reader.GetString(externalLinkIdColumnOrdinal);
                    if (!string.IsNullOrEmpty(naptan) && !string.IsNullOrEmpty(externalLinkId))
                        AddRecord(naptan, externalLinkId);
                }
                reader.Close();
                zonalAccessibilityLinks = zonalAccessibilityLinksCurrentLoad;
                #endregion

                #region Load Admin District Links
                reader = helper.GetReader("GetZonalAreaDistrictLinks", new Hashtable(), new Hashtable());

                int adminAreaIdColumnOrdinal = reader.GetOrdinal("AdminAreaId");
                int districtIdColumnOrdinal = reader.GetOrdinal("DistrictId");
                externalLinkIdColumnOrdinal = reader.GetOrdinal("ExternalLinkId");

                while (reader.Read())
                {
                    string adminAreaId = reader.IsDBNull(adminAreaIdColumnOrdinal) ? string.Empty : reader.GetString(adminAreaIdColumnOrdinal);
                    string districtId = reader.IsDBNull(districtIdColumnOrdinal) ? string.Empty : reader.GetString(districtIdColumnOrdinal);
                    string externalLinkId = reader.IsDBNull(externalLinkIdColumnOrdinal) ? string.Empty : reader.GetString(externalLinkIdColumnOrdinal);

                    if ((!string.IsNullOrEmpty(adminAreaId) || !string.IsNullOrEmpty(districtId))
                        && !string.IsNullOrEmpty(externalLinkId))
                    {
                        //string AdminAreaFaresLinkId = reader.GetString(AdminAreaFaresLinkIdColumnOrdinal);
                        AdminDistrictKey hashkeyAdminDistrict = new AdminDistrictKey(adminAreaId, districtId);

                        ArrayList areaexternalLinkIds;
                        //If the hashtable currently contains external links for the current area district combination, then
                        //add the link, else insert a new entry into the hashtable for the current area district combination
                        if (hashAdminAreaDistrictLinks.Contains(hashkeyAdminDistrict))
                        {
                            areaexternalLinkIds = (ArrayList)hashAdminAreaDistrictLinks[hashkeyAdminDistrict];
                            areaexternalLinkIds.Add(externalLinkId);
                        }
                        else
                        {
                            areaexternalLinkIds = new ArrayList();
                            areaexternalLinkIds.Add(externalLinkId);
                            hashAdminAreaDistrictLinks.Add(hashkeyAdminDistrict, areaexternalLinkIds);
                        }
                    }
                }
                reader.Close();

                #endregion

                #region Load Region Operator Mode Links
                reader = helper.GetReader("GetZonalRegionOperatorModeLinks", new Hashtable(), new Hashtable());

                int regionIdColumnOrdinal = reader.GetOrdinal("RegionId");
                int operatorCodeOrdinal = reader.GetOrdinal("OperatorCode");
                int modeIdOrdinal = reader.GetOrdinal("ModeId");
                externalLinkIdColumnOrdinal = reader.GetOrdinal("ExternalLinkId");

                while (reader.Read())
                {
                    string regionId = reader.IsDBNull(regionIdColumnOrdinal) ? string.Empty : reader.GetString(regionIdColumnOrdinal);
                    string operatorCode = reader.IsDBNull(operatorCodeOrdinal) ? string.Empty : reader.GetString(operatorCodeOrdinal);
                    string modeId = reader.IsDBNull(modeIdOrdinal) ? string.Empty : reader.GetString(modeIdOrdinal);
                    string externalLinkId = reader.IsDBNull(externalLinkIdColumnOrdinal) ? string.Empty : reader.GetString(externalLinkIdColumnOrdinal);

                    if (!string.IsNullOrEmpty(regionId) && !string.IsNullOrEmpty(operatorCode)
                        && !string.IsNullOrEmpty(modeId) && !string.IsNullOrEmpty(externalLinkId))
                    {
                        RegionOperatorModeKey hashkeyRegionOperatorMode = new RegionOperatorModeKey(regionId, operatorCode, modeId);


                        ArrayList operatorexternalLinkIds;
                        //If the hashtable currently contains external links for the current area district combination, then
                        //add the link, else insert a new entry into the hashtable for the current area district combination
                        if (hashRegionOperatorModeLinks.Contains(hashkeyRegionOperatorMode))
                        {
                            operatorexternalLinkIds = (ArrayList)hashRegionOperatorModeLinks[hashkeyRegionOperatorMode];
                            operatorexternalLinkIds.Add(externalLinkId);
                        }
                        else
                        {
                            operatorexternalLinkIds = new ArrayList();
                            operatorexternalLinkIds.Add(externalLinkId);
                            hashRegionOperatorModeLinks.Add(hashkeyRegionOperatorMode, operatorexternalLinkIds);
                        }
                    }
                }
                reader.Close();

                #endregion
            }
            catch (SqlException sqlEx)
            {
                OperationalEvent oe = new OperationalEvent(TDEventCategory.Infrastructure,
                    TDTraceLevel.Error, "SQLNUM[" + sqlEx.Number + "] :" + sqlEx.Message + ":");

                throw new TDException("SqlException caught : " + sqlEx.Message, sqlEx,
                    false, TDExceptionIdentifier.TNSQLHelperError);
            }
            catch (TDException tdex)
            {
                string message = "Error Calling Zonal Accessibility Stored Procedures " + tdex.Message;
                OperationalEvent oe = new OperationalEvent(TDEventCategory.Infrastructure,
                    TDTraceLevel.Error, "TDException :" + tdex.Message + ":");

                throw new TDException(message, tdex, false, TDExceptionIdentifier.TNSQLHelperStoredProcedureFailure);
            }
            finally
            {
                if (helper.ConnIsOpen)
                    helper.ConnClose();
            }

        }


        /// <summary>
        /// Adds Zonal Accessibility link information to the cache
        /// </summary>
        /// <param name="naptan">The naptan</param>
        /// <param name="externalLinkId">The unique identifier for the corresponding external link</param>
        private void AddRecord(string naptan, string externalLinkId)
        {
            ArrayList externalLinkIds;
            //If the hashtable currently contains external links for the current naptan, then
            //add the link, else insert a new entry into the hashtable for the current naptan
            if (zonalAccessibilityLinksCurrentLoad.ContainsKey(naptan))
            {
                externalLinkIds = (ArrayList)zonalAccessibilityLinksCurrentLoad[naptan];
                externalLinkIds.Add(externalLinkId);
            }
            else
            {
                externalLinkIds = new ArrayList();
                externalLinkIds.Add(externalLinkId);
                zonalAccessibilityLinksCurrentLoad.Add(naptan, externalLinkIds);
            }
        }

        /// <summary>
        /// Gets the External link details based on Area Id and District Id specified
        /// this methos returns external links for 3 combination of the Area Id and District Id
        /// 1> when Area Id and District Id both specified
        /// 2> when Area Id  is null and District Id specified
        /// 3> when Area id is specified and District Id is null
        /// </summary>
        /// <param name="adminArea">Admin Area id came from gis query</param>
        /// <param name="districtId">District id came from gis query</param>
        /// <returns>Array of External Link Detail</returns>
        public ExternalLinkDetail[] GetAdminAreaDistrictLinks(string adminArea, string districtId)
        {
            ArrayList externalLinkDetails = new ArrayList();

            #region matching both Admin Id and District id
            AdminDistrictKey hashkeyToMatch = new AdminDistrictKey(adminArea, districtId);


            if (hashAdminAreaDistrictLinks.ContainsKey(hashkeyToMatch))
            {

                ExternalLinkDetail[] adminDistrictLinks = AdminDistrictLinks(hashkeyToMatch);

                foreach (ExternalLinkDetail linkDetail in adminDistrictLinks)
                {
                    if (!externalLinkDetails.Contains(linkDetail))
                    {
                        externalLinkDetails.Add(linkDetail);
                    }
                }

            }

            #endregion

            #region matching District id only
            hashkeyToMatch = new AdminDistrictKey(string.Empty , districtId);

            if (hashAdminAreaDistrictLinks.ContainsKey(hashkeyToMatch))
            {
                ExternalLinkDetail[] adminDistrictLinks = AdminDistrictLinks(hashkeyToMatch);

                foreach (ExternalLinkDetail linkDetail in adminDistrictLinks)
                {
                    if (!externalLinkDetails.Contains(linkDetail))
                    {
                        externalLinkDetails.Add(linkDetail);
                    }
                }


            }
            #endregion

            #region matching Admin id only
            hashkeyToMatch = new AdminDistrictKey(adminArea, string.Empty );

            if (hashAdminAreaDistrictLinks.ContainsKey(hashkeyToMatch))
            {
                ExternalLinkDetail[] adminDistrictLinks = AdminDistrictLinks(hashkeyToMatch);

                foreach (ExternalLinkDetail linkDetail in adminDistrictLinks)
                {
                    if (!externalLinkDetails.Contains(linkDetail))
                    {
                        externalLinkDetails.Add(linkDetail);
                    }
                }


            }
            #endregion


            if (externalLinkDetails.Count == 0)
            {
                return null;
            }
            else
            {
                return (ExternalLinkDetail[])externalLinkDetails.ToArray(typeof(ExternalLinkDetail));
            }

        }

        /// <summary>
        /// Returns ExternalLinkDetails for each combination of Admin Id and District Id
        /// </summary>
        /// <param name="hashkeyToMatch"></param>
        /// <returns>ExternalLinkDetail Array</returns>
        private ExternalLinkDetail[] AdminDistrictLinks(AdminDistrictKey hashkeyToMatch)
        {
            ArrayList externalLinkDetails = new ArrayList();

            ArrayList externalLinkIDs = (ArrayList)hashAdminAreaDistrictLinks[hashkeyToMatch];
            //Get the external link for each zonal accessibility. Link must be in publication to be returned
            foreach (string externalLinkID in externalLinkIDs)
            {
                ExternalLinkDetail link = (ExternalLinkDetail)ExternalLinks.Current[externalLinkID];
                if (link != null)
                {
                    if (!string.IsNullOrEmpty(link.LinkText) && !string.IsNullOrEmpty(link.Url))
                    {
                        if (link.Published)
                        { externalLinkDetails.Add(link); }
                    }
                }
                else
                {
                    string errorMessage = "No ExternalLink was found for link id: " + externalLinkID + ". ZonalAccessibility and ExternalLinks are out of sync.";
                    OperationalEvent oe = new OperationalEvent(TDEventCategory.Database,
                        TDTraceLevel.Error, errorMessage);
                    Trace.Write(oe);
                }
            }

            return (ExternalLinkDetail[])externalLinkDetails.ToArray(typeof(ExternalLinkDetail));
        }

        /// <summary>
        /// Returns external accessibility link details for given Region, Mode and Operator
        /// </summary>
        /// <param name="regionId"></param>
        /// <param name="operatorCode"></param>
        /// <param name="modeId"></param>
        /// <returns>Array of external accessibility links</returns>
        public ExternalLinkDetail[] GetRegionOperatorModeLinks(string regionId, string operatorCode, string modeId)
        {
            string operatorLinksUrlId = String.Empty;
            ArrayList linkDetails = new ArrayList();

            RegionOperatorModeKey hashkeyToMatch = new RegionOperatorModeKey(regionId, operatorCode, modeId);

            if (hashRegionOperatorModeLinks.ContainsKey(hashkeyToMatch))
            {
                ArrayList externalLinkIDs = (ArrayList)hashRegionOperatorModeLinks[hashkeyToMatch];
                //Get the external link for each zonal accessibility. Link must be in publication to be returned
                foreach (string externalLinkID in externalLinkIDs)
                {
                    ExternalLinkDetail link = (ExternalLinkDetail)ExternalLinks.Current[externalLinkID];
                    if (link != null)
                    {
                        if (!string.IsNullOrEmpty(link.Url))
                        {
                            if (link.Published)
                            { linkDetails.Add(link); }
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(externalLinkID))
                        {
                            string errorMessage = string.Format("No ExternalLink was found for region id: {0} and mode id: {1} and operator id: {2}.", regionId, modeId, operatorCode);
                            OperationalEvent oe = new OperationalEvent(TDEventCategory.Database,
                                TDTraceLevel.Error, errorMessage);
                            Trace.Write(oe);
                        }
                    }
                }
            }

            if (linkDetails.Count == 0)
            {
                return null;
            }
            else
            {
                return (ExternalLinkDetail[])linkDetails.ToArray(typeof(ExternalLinkDetail));
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Retrieves the zonal accessibility links for a given stop. Returns null if no zonal
        /// accessibility links exist for the given stop
        /// </summary>
        /// <param name="naptan">The naptan identifier for the current stop</param>
        /// <returns>An array of ExternalLinkDetail objects representing the zonal accessibility
        /// links for the given stop</returns>
        public ExternalLinkDetail[] GetZonalAccessibilityLinks(string naptan)
        {

            
            // if(!zonalServiceLinks.ContainsKey(naptan)) return null;

            ArrayList externalLinkDetails = new ArrayList();
            if (zonalAccessibilityLinks.ContainsKey(naptan))
            {
                ArrayList externalLinkIDs = (ArrayList)zonalAccessibilityLinks[naptan];
                //Get the external link for each zonal accessibility. Link must be in publication to be returned
                foreach (string externalLinkID in externalLinkIDs)
                {
                    ExternalLinkDetail link = (ExternalLinkDetail)ExternalLinks.Current[externalLinkID];
                    if (link != null)
                    {
                        if (!string.IsNullOrEmpty(link.LinkText) && !string.IsNullOrEmpty(link.Url))
                        {
                            if (link.Published)
                            { externalLinkDetails.Add(link); }
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(externalLinkID))
                        {
                            string errorMessage = "No ExternalLink was found for link id: " + externalLinkID + ". ZonalAccessibility and ExternalLinks are out of sync.";
                            OperationalEvent oe = new OperationalEvent(TDEventCategory.Database,
                                TDTraceLevel.Error, errorMessage);
                            Trace.Write(oe);
                        }
                    }
                }
            }
            


            if (externalLinkDetails.Count == 0)
            {
                return null;
            }
            else
            {
                return (ExternalLinkDetail[])externalLinkDetails.ToArray(typeof(ExternalLinkDetail));
            }

        }

        #endregion
	
	}
}
