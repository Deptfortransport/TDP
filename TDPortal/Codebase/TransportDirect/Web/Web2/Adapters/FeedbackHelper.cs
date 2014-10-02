// *********************************************** 
// NAME                 : FeedbackHelper.ascx
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 08/01/2007
// DESCRIPTION			: Class providing helper methods 
//						  for User Feedback
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Adapters/FeedbackHelper.cs-arc  $ 
//
//   Rev 1.4   May 11 2010 09:33:52   apatel
//Updated SubmitFeedback method to include more information in exception handlers
//
//   Rev 1.3   Oct 13 2008 16:41:26   build
//Automatically merged from branch for stream5014
//
//   Rev 1.2.1.2   Oct 10 2008 15:44:26   mmodi
//Updated for cycle attributes
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.2.1.1   Sep 02 2008 10:43:42   mmodi
//Updated to retrieve cycle planner object for feedback viewer
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.2.1.0   Aug 22 2008 10:23:34   mmodi
//Updated for cycle journeys
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.2   Mar 31 2008 12:58:56   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:11:16   mturner
//Initial revision.
//
//   Rev 1.7   Apr 25 2007 14:40:16   mmodi
//Added code to convert JourneyRequest to string if error thrown during the ConvertToXML
//Resolution for 4344: Contact Us: Feedback Viewer page shows error text when viewing session info
//
//   Rev 1.6   Apr 16 2007 13:49:56   mmodi
//Correct UFE event for submitting email
//Resolution for 4386: User feedback: Reporting error with UFE events
//
//   Rev 1.5   Mar 07 2007 12:18:40   mmodi
//Updated logging and exception handling, and added fall back process for when creating feedback record fails
//Resolution for 4366: Feedback records not being submitted
//
//   Rev 1.4   Jan 22 2007 13:48:58   mmodi
//Added null check when retrieving session info
//Resolution for 4332: Contact Us Improvements - workstream
//
//   Rev 1.3   Jan 19 2007 15:46:16   mmodi
//Updated retrieve session info to display car journeys
//Resolution for 4332: Contact Us Improvements - workstream
//
//   Rev 1.2   Jan 17 2007 18:01:54   mmodi
//Added methods to retrieve Feedback and Session data
//Resolution for 4332: Contact Us Improvements - workstream
//
//   Rev 1.1   Jan 14 2007 17:27:16   mmodi
//Added GetFeedback methods
//Resolution for 4332: Contact Us Improvements - workstream
//
//   Rev 1.0   Jan 12 2007 14:16:58   mmodi
//Initial revision.
//Resolution for 4332: Contact Us Improvements - workstream
//

using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

using TransportDirect.Common;
using TransportDirect.Common.DatabaseInfrastructure;
using TransportDirect.Common.Logging;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ResourceManager;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.ReportDataProvider.TDPCustomEvents;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.Resource;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.Web.Adapters;
using TransportDirect.Web.Support;

using Logger = System.Diagnostics.Trace;
using ControlPopulator = TransportDirect.UserPortal.DataServices.DataServices;
using TD.ThemeInfrastructure;
using System.Web;
using TransportDirect.UserPortal.CyclePlannerControl;
using System.Collections.Generic;

namespace TransportDirect.UserPortal.Web.Adapters
{
	/// <summary>
	/// FeedbackHelper - handles all requests to create, retrieve, and save feedback records. 
	/// Also includes the Email sending for a feedback
	/// </summary>
	public class FeedbackHelper
	{
		#region Constants
		// These const's must match the UserFeedbackStatus dataset
		private const string STATUS_CLOSED = "Closed";
		private const string STATUS_INPROGRESS = "In progress";
		private const string STATUS_SUBMITTED = "Submitted";

		private const string OUTWARDJOURNEY_HEADING = "\r\n\r\n======== OUTWARD JOURNEYS ========";
		private const string RETURNJOURNEY_HEADING = "\r\n\r\n======== RETURN JOURNEYS ========";
		private const string PUBLICJOURNEY_HEADING = "\r\n\r\n======== Public Journey ========\r\n";
		private const string CARJOURNEY_HEADING = "\r\n\r\n======== Car Journey ========\r\n";
        private const string CYCLEJOURNEY_HEADING = "\r\n\r\n======== Cycle Journey ========\r\n";

		private const string NO_OUTWARDJOURNEYS = "\r\nNo Outward journeys were found in the TD Itinerary Manager";
		private const string NO_RETURNJOURNEYS = "\r\nNo Return journeys were found in the TD Itinerary Manager";

		private const string NO_OUTWARDJOURNEYS_PUBLIC = "\r\nNo Outward Public journeys were found in TD Journey Results";
		private const string NO_RETURNJOURNEYS_PUBLIC = "\r\nNo Return Public journeys were found in TD Journey Results";
		private const string NO_OUTWARDJOURNEYS_CAR = "\r\nNo Outward Car journeys were found in TD Journey Results";
		private const string NO_RETURNJOURNEYS_CAR = "\r\nNo Return Car journeys were found in TD Journey Results";
        private const string NO_OUTWARDJOURNEYS_CYCLE = "\r\nNo Outward Cycle journeys were found in TD Cycle Results";
        private const string NO_RETURNJOURNEYS_CYCLE = "\r\nNo Return Cycle journeys were found in TD Cycle Results";
		#endregion

		#region Private variables

		// Parameters used for the Feedback database table
		private int feedbackId;
		private string sessionId;
		private DateTime sessionCreated;
		private DateTime sessionExpires;
		private DateTime emailSubmittedTime; //Time sent to Help desk
		private DateTime emailAcknowledgedTime; //Time sent to User email
		private bool acknowledgementSent; // Acknowledgement sent to User
		private bool userLoggedOn;
		private DateTime timeLogged;
		private string vantiveId;
		private string feedbackStatus;
		private bool deleteFlag;

		private ControlPopulator populator;

		private SqlHelper sqlHelper;
		
		#endregion

		#region Constructor

		/// <summary>
		/// Default constructor
		/// </summary>
		public FeedbackHelper()
		{
			sqlHelper = new SqlHelper();
			populator = (ControlPopulator)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];
		}

		#endregion

		#region Public methods

		/// <summary>
		/// Saves the User Feedback record to the UserFeedback table in the database, and
		/// sends an Email to the Helpdesk and user email address supplied
		/// </summary>
		/// <param name="feedbackRecord"></param>
		/// <returns></returns>
		public bool SubmitFeedback(string userEmailAddress, string userComments, ArrayList userOptionsItems, ArrayList userOptionsValues, int feedbackType)
		{
			// SubmitFeedback does the following
			// 1. Create Feedback record, because we need the Feedback ID for the email to helpdesk
			// 2. Create and send the Email
			// 3. Update the Feedback record details (namely email sent options) to the table
			// 4. Add the Session information to Feedback
			// If any of the above fail, then the fallback is to submit an email only

			bool returnFlag = false;

            bool emailSentFlag = false;
            bool sessionInfoAddedFlag = false;
            bool feedBackOptionsAddedFlag = false;
            bool feedbackSuccessFlag = false;

			try
			{
                // Log feedback
                if (TDTraceSwitch.TraceVerbose)
                {
                    Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose,
                        string.Format("User Feedback: email[{0}] comments[{1}]", userEmailAddress, userComments)));
                }
                
				// We only want to save Problems to a database and not Suggestions
				if (feedbackType == 0)
				{

					// To prevent multiple feedback records being created by one user, i.e. they press F5 repeatedly
					if ((TDSessionManager.Current.InputPageState.FeedbackPageState == FeedbackState.Problem)
						||
						(TDSessionManager.Current.InputPageState.FeedbackPageState == FeedbackState.Suggestion) )
					{
						try
						{
							Logger.Write (new OperationalEvent(TDEventCategory.Infrastructure,
								TDTraceLevel.Info, "User Feedback - First attempt at creating feedback record started" ));

							returnFlag = CreateFeedback();
						}
						catch (Exception e)
						{	
							Logger.Write (new OperationalEvent(TDEventCategory.Infrastructure,
								TDTraceLevel.Error, "Exception :" + e.Message ));

							Logger.Write (new OperationalEvent(TDEventCategory.Infrastructure,
								TDTraceLevel.Info, "User Feedback - Feedback record not created in database. Second attempt at creating feedback record started, for Session : " 
								+ sessionId.ToString(TDCultureInfo.CurrentCulture)));

							// In case this fails first time, try again
							returnFlag = CreateFeedback();
						}
					
						// Only attempt sending email if Feedback record was created successfully
                        if (returnFlag)
                        {
                           emailSentFlag = returnFlag = SubmitEmail(userEmailAddress, userComments, userOptionsValues, feedbackType);
                           
                        }

						if (returnFlag)
							sessionInfoAddedFlag =  returnFlag = AddSessionInformation();

						if (returnFlag)
							feedBackOptionsAddedFlag = returnFlag = AddFeedbackUserOptions(userOptionsItems, userEmailAddress, userComments);

						// Only attempt updating the Feedback record if email was sent, and Session Info saved successfully 
						// This ensures Feedback records are in the appropriate state, i.e those with submitted state
						// have a corresponding email sent to the helpdesk
						if (returnFlag)
						{
							//update parameters
							timeLogged = DateTime.Now;
							feedbackStatus = STATUS_SUBMITTED;

							feedbackSuccessFlag = returnFlag = UpdateFeedback();
						}

						if (returnFlag)
							Logger.Write (new OperationalEvent(TDEventCategory.Infrastructure,
								TDTraceLevel.Info, "User Feedback - Submit feedback process has completed successfully" ));
					}
				}
				else
				{
					Logger.Write (new OperationalEvent(TDEventCategory.Infrastructure,
						TDTraceLevel.Info, "User Feedback Suggestion - Submit process started" ));

					// Suggestion is being submitted, so don't create a Feedback record in database
					returnFlag = SubmitEmail(userEmailAddress, userComments, userOptionsValues, feedbackType);

					if (returnFlag)
						Logger.Write (new OperationalEvent(TDEventCategory.Infrastructure,
							TDTraceLevel.Info, "User Feedback Suggestion - Submit process has completed successfully" ));
				}
			}
			catch (TDException tdex)
			{
				returnFlag = false;

				OperationalEvent oe = new OperationalEvent(TDEventCategory.Infrastructure,
                    TDTraceLevel.Error, "TDException : User Feedback - Error occured in SubmitFeedback :" + tdex.Message);
				
				Logger.Write(oe);
			}
			catch (Exception e)
			{
				returnFlag = false;

				Logger.Write (new OperationalEvent(TDEventCategory.Infrastructure,
                    TDTraceLevel.Error, "Exception : User Feedback - Error occured in SubmitFeedback :" + e.Message));
			}

			// We have got problems with submitting the feedback record
            // Possible reasons may be failure to insert/update record to the database or SMTP server fail to relay the message
			if (!returnFlag)
			{
               
				try
				{
                    // In case there was problems submiting the Feedback record to the database, as a final attempt
                    // revert to only sending an email to the help desk. This fixes a problem where the users feedback 
                    // submit was repeatedly failing.
                    if (feedbackId == 0)
                    {
                        

                        Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure,
                            TDTraceLevel.Error, "User Feedback - Feedback record not created in database. Final attempt at submitting feedback email only started."));

                        // Add to the user comments note saying there will be no User Feedback reference to inform
                        // support team that only the email was submitted
                        userComments = userComments + "\r\n\r\n -- No User Feedback Record created. Only an email has been submitted --";


                        returnFlag = SubmitEmail(string.Empty, userComments, userOptionsValues, feedbackType);

                       
                    }
                    // we got feedbackId which suggest failure is more likely due to SMTP server fail to relay the message or in one of the process after that
                    else 
                    {
                        string logMessage = "User Feedback - ";
                        string errorReason = "Error creating a User Feedback";
                        if (!emailSentFlag)
                        {
                            errorReason = "Feedback record created successfully but failed to send email to user.";
                        }
                        else if (!sessionInfoAddedFlag)
                        {
                            errorReason = "Feedback record created and email sent successfully, but process failed to add session information for feedback in database.";
                        }
                        else if (!feedBackOptionsAddedFlag)
                        {
                            errorReason = "Feedback record created and email sent successfully, but unable to add the user feedback options in the database.";
                        }
                        else if (!feedbackSuccessFlag)
                        {
                            errorReason = "Feedback process failed to update feedback record to set status as submitted. ";
                        }
                        
                        Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure,
                            TDTraceLevel.Info, logMessage + errorReason));
                        
                        // If the problem got raised after sending email to user,
                        // Add to the user comments note indicating possible reasong why feedback submit failed
                        if (emailSentFlag)
                        {
                            userComments = userComments + "\r\n\r\n -- " + errorReason + " --";
                            userEmailAddress = string.Empty;
                        }


                        returnFlag = SubmitEmail(userEmailAddress, userComments, userOptionsValues, feedbackType);
                    }

                    Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure,
                           TDTraceLevel.Info, "User Feedback - Final attempt at submitting feedback email only suceeded."));
				}
				catch (Exception e)
				{
					returnFlag = false;

					Logger.Write (new OperationalEvent(TDEventCategory.Infrastructure,
						TDTraceLevel.Error, "Exception: User Feedback - Final attempt at submitting feedback email only failed :" + e.Message ));
				}
			}
	
			return returnFlag;
		}

		#region Used for Feedback Viewer
		/// <summary>
		/// Saves the Feedback record back to the database
		/// </summary>
		/// <param name="feedback"></param>
		/// <returns></returns>
		public bool SaveFeedback(Feedback feedback)
		{
			bool returnFlag = false;

			// Need to set Feedback parameters to object so we can call internal Update method

			this.feedbackId = feedback.FeedbackId;
			this.sessionId = feedback.SessionId;
			this.emailSubmittedTime = feedback.EmailSubmittedTime;
			this.emailAcknowledgedTime = feedback.EmailAcknowledgedTime;
			this.acknowledgementSent = feedback.AcknowledgementSent;
			this.userLoggedOn = feedback.UserLoggedOn;
			this.timeLogged = feedback.TimeLogged;
			this.vantiveId = feedback.VantiveId;
			this.feedbackStatus = feedback.FeedbackStatus;
			this.deleteFlag = feedback.DeleteFlag;

			try
			{
				returnFlag = UpdateFeedback();

				// Update delete status of Feedback and linked records
				if (returnFlag)
					returnFlag = DeleteFeedback();
			}
			catch (Exception e)
			{
				returnFlag = false;

				Logger.Write (new OperationalEvent(TDEventCategory.Infrastructure,
					TDTraceLevel.Error, "Exception: User Feedback - Error occured in SaveFeedback :" + e.Message + " : " + e.InnerException ));
			}

			return returnFlag;
		}

		/// <summary>
		/// Retrieves the Feedback record from the database for the supplied Feedback Id
		/// </summary>
		/// <param name="feedbackId"></param>
		/// <returns>Feedback record as Feedback</returns>
		public Feedback GetFeedback(int feedbackId)
		{
			Feedback feedback = new Feedback();
			try
			{
				feedback = RetrieveFeedback(feedbackId);
			}
			catch (Exception e)
			{
				Logger.Write (new OperationalEvent(TDEventCategory.Infrastructure,
					TDTraceLevel.Error, "Exception: User Feedback - Error occured in GetFeedback :" + e.Message + " : " + e.InnerException ));
			}
			return feedback;
		}


		/// <summary>
		/// Retrieves the Journey Request for a Feedback record
		/// </summary>
		/// <param name="feedbackId"></param>
		/// <param name="sessionId"></param>
		/// <returns>TDJourneyRequest as an Xml string</returns>
		public string GetFeedbackJourneyRequest(int feedbackId, string sessionId)
		{
            // Determine where to get the request object from (i.e. JourneyRequest or CycleRequest)
            object objectFindPageState = RetrieveFeedbackSessionData(feedbackId, sessionId, TDSessionManager.KeyFindPageState);

            FindAMode findAMode = FindAMode.None;
            object request = null;

            try
            {
                FindPageState pageState = (FindPageState)objectFindPageState;
                findAMode = pageState.Mode;
            }
            catch
            {
                findAMode = FindAMode.None;
            }

            switch (findAMode)
            {
                case FindAMode.Cycle:
                    request = RetrieveFeedbackSessionData(feedbackId, sessionId, TDSessionManager.KeyCycleRequest);
                    break;
                default:
                    request = RetrieveFeedbackSessionData(feedbackId, sessionId, TDSessionManager.KeyJourneyRequest);
                    break;
            }
			
			try
			{
				return ConvertToXML(request);
			}
			catch (Exception ex1)
			{
				// Error occured, likely for a Find a flight or City to city journey request
				// so attempt to manually build the request object as a string
				try
				{
                    StringBuilder sb = new StringBuilder();
                    sb.Append("======== Manual build of Request ======== ");
                    sb.Append("\n");

                    switch (findAMode)
                    { 
                        case FindAMode.Cycle:
                            TDCyclePlannerRequest cr = (TDCyclePlannerRequest)request;
                            sb.Append(ConvertCycleRequestToString(cr));
                            break;
                        default:
                            TDJourneyRequest jr = (TDJourneyRequest)request;
                            sb.Append(ConvertJourneyRequestToString(jr));
                            break;
                    }

					return sb.ToString();
				}
				catch (Exception ex2)
				{
					StringBuilder sb = new StringBuilder();
					sb.Append( ex1.Message );
					sb.Append( "\r\n" );
					sb.Append( "\r\n" );
					sb.Append( "Error during ConvertJourneyRequestToString \r\nUnable to manually build string of JourneyRequest object: " );
					sb.Append( ex2.Message );
					sb.Append( "\r\n" );
					sb.Append( ex2.InnerException );

					Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, sb.ToString() ));

					return sb.ToString();
				}
			}
		}


		/// <summary>
		/// Retrieves the Journey Result for a Feedback record
		/// </summary>
		/// <param name="feedbackId"></param>
		/// <param name="sessionId"></param>
		/// <returns>Journey Result as an Xml string</returns>
		public string GetFeedbackJourneyResult(int feedbackId, string sessionId)
        {
            #region Get session objects
            TDJourneyResult journeyResult = null;
            TDCyclePlannerResult cycleResult = null;

            try
            {
                journeyResult = (TDJourneyResult)RetrieveFeedbackSessionData(feedbackId, sessionId, TDSessionManager.KeyJourneyResult);
            }
            catch 
            {
                // If unable to get the journey result, likely means the object was not initialised when 
                // feedback submitted
            }
            
            try
            {
                cycleResult = (TDCyclePlannerResult)RetrieveFeedbackSessionData(feedbackId, sessionId, TDSessionManager.KeyCycleResult);
            }
            catch
            {
                // If unable to get the cycle result, likely means the object was not initialised when 
                // feedback submitted
            }

			TDJourneyViewState journeyViewState = (TDJourneyViewState)RetrieveFeedbackSessionData(feedbackId, sessionId, TDSessionManager.KeyJourneyViewState);
			InputPageState inputPageState = (InputPageState)RetrieveFeedbackSessionData(feedbackId, sessionId, TDSessionManager.KeyInputPageState);
            #endregion

            StringBuilder sb = new StringBuilder();

			if ((journeyResult != null) || (cycleResult != null))
			{
				try
				{
					#region Public journeys
					// Gives us all the Public transport journey details
					if ((journeyResult != null) && journeyResult.OutwardPublicJourneyCount > 0)
					{
						sb.Append( OUTWARDJOURNEY_HEADING );
						foreach (JourneyControl.PublicJourney pubJourneyDetails in journeyResult.OutwardPublicJourneys)
						{
							sb.Append( PUBLICJOURNEY_HEADING );
							sb.Append( ConvertToXML( pubJourneyDetails ) );
						}
					}
					else
						sb.Append( NO_OUTWARDJOURNEYS_PUBLIC );

                    if ((journeyResult != null) && journeyResult.ReturnPublicJourneyCount > 0)
					{
						sb.Append( RETURNJOURNEY_HEADING );
						foreach( JourneyControl.PublicJourney pubJourneyDetails in journeyResult.ReturnPublicJourneys)
						{
							sb.Append( PUBLICJOURNEY_HEADING );
							sb.Append( ConvertToXML( pubJourneyDetails ) );
						}
					}
					else 
						sb.Append( NO_RETURNJOURNEYS_PUBLIC );
					#endregion

					#region Car Journeys
                    // Because the Road Journeys cannot be easily serialised to xml, 
					// we need to attempt individually
                        RoadJourney outwardRJ = (journeyResult != null) ? journeyResult.OutwardRoadJourney() : null;
					if (outwardRJ != null)
					{
						sb.Append( OUTWARDJOURNEY_HEADING );
						sb.Append( CARJOURNEY_HEADING );

						// Gives us the Start and End Leg of the Car journey
						foreach (JourneyLeg jl in outwardRJ.JourneyLegs)
							sb.Append( ConvertToXML(jl) );

						sb.Append( GetCarDetail(true, journeyResult, journeyViewState, inputPageState.Units) );
					}
					else
						sb.Append( NO_OUTWARDJOURNEYS_CAR );

					RoadJourney returnRJ = (journeyResult != null) ? journeyResult.ReturnRoadJourney() : null;
					if (returnRJ != null)
					{
						sb.Append( RETURNJOURNEY_HEADING );
						sb.Append( CARJOURNEY_HEADING );
			
						// Gives us the Start and End Leg of the Car journey
						foreach (JourneyLeg jl in returnRJ.JourneyLegs)
							sb.Append( ConvertToXML(jl) );

						sb.Append( GetCarDetail(false, journeyResult, journeyViewState, inputPageState.Units) );
					}
					else
						sb.Append( NO_RETURNJOURNEYS_CAR );
					#endregion

                    #region Cycle Journeys
                    // Because the Cycle Journeys cannot be easily serialised to xml, 
                    // we need to attempt individually
                    if ((cycleResult != null) && cycleResult.OutwardCycleJourneyCount > 0)
                    {
                        CycleJourney outwardCJ = cycleResult.OutwardCycleJourney();
                        if (outwardCJ != null)
                        {
                            sb.Append(OUTWARDJOURNEY_HEADING);
                            sb.Append(CYCLEJOURNEY_HEADING);

                            foreach(JourneyLeg jl in outwardCJ.JourneyLegs)
                                sb.Append(ConvertToXML(jl));

                            sb.Append(GetCycleDetail(true, cycleResult.OutwardCycleJourney(), journeyViewState, inputPageState.Units));
                        }
                    }
                    else
                        sb.Append(NO_OUTWARDJOURNEYS_CYCLE);

                    if ((cycleResult != null) && cycleResult.ReturnCycleJourneyCount > 0)
                    {
                        CycleJourney returnCJ = cycleResult.ReturnCycleJourney();
                        if (returnCJ != null)
                        {
                            sb.Append(RETURNJOURNEY_HEADING);
                            sb.Append(CYCLEJOURNEY_HEADING);

                            foreach(JourneyLeg jl in returnCJ.JourneyLegs)
                                sb.Append(ConvertToXML(jl));

                            sb.Append(GetCycleDetail(false, cycleResult.ReturnCycleJourney(), journeyViewState, inputPageState.Units));
                        }
                    }
                    else
                        sb.Append(NO_RETURNJOURNEYS_CYCLE);
                    
                    
                    #endregion

                    return sb.ToString();
				}
				catch (Exception ex)
				{
					// Error occured, return the message so it can be displayed in the Feedback viewer
					return ex.Message;
				}
			}
			else
			{
				return null;
			}

		}

		/// <summary>
		/// Retrieves the Itinrary Manager for a Feedback record
		/// </summary>
		/// <param name="feedbackId"></param>
		/// <param name="sessionId"></param>
		/// <returns>Itinrary Manager as an Xml string</returns>
		public string GetFeedbackItineraryManager(int feedbackId, string sessionId)
		{
			TDItineraryManager itineraryManager = (TDItineraryManager)RetrieveFeedbackSessionData(feedbackId, sessionId, TDSessionManager.KeyItineraryManager);
			InputPageState inputPageState = (InputPageState)RetrieveFeedbackSessionData(feedbackId, sessionId, TDSessionManager.KeyInputPageState);

			if (itineraryManager != null)
			{
				try
				{
					bool itineraryExists = (itineraryManager.OutwardLength > 0);

					// Add each Outward and Return journey segment to the output
					// (can become a long string)
					StringBuilder sb = new StringBuilder();

					#region Outward journeys
					if (!itineraryExists)
						sb.Append( NO_OUTWARDJOURNEYS );
					else
					{
						sb.Append( OUTWARDJOURNEY_HEADING );

						TDJourneyResult journeyResult;
						TDJourneyViewState journeyViewState;
						JourneyControl.PublicJourney pubJourneyDetails;

						int itineraryLength = itineraryManager.OutwardLength;

						for (int i=0; i<itineraryLength; i++)
						{
							journeyResult = (TDJourneyResult)itineraryManager.SpecificJourneyResult(i);
							journeyViewState = itineraryManager.SpecificJourneyViewState(i);

							// generate details for outward Itinerary journeys
							if (journeyViewState.SelectedOutwardJourneyType == TDJourneyType.RoadCongested)
							{
								sb.Append( CARJOURNEY_HEADING );
								sb.Append( "SEGMENT NUMBER: " + i.ToString() + "\r\n\r\n");

								// Gives us the Start and End Leg of the Car journey
								foreach (JourneyLeg jl in journeyResult.OutwardRoadJourney().JourneyLegs)
									sb.Append( ConvertToXML(jl) );

								sb.Append( GetCarDetail(true, journeyResult, journeyViewState, inputPageState.Units) );

							}
							else
							{
								sb.Append( PUBLICJOURNEY_HEADING );
								sb.Append( "SEGMENT NUMBER: " + i.ToString() + "\r\n\r\n");

								pubJourneyDetails = journeyResult.OutwardPublicJourney(journeyViewState.SelectedOutwardJourneyID);
								sb.Append( ConvertToXML( pubJourneyDetails ) );
							}
						}
					}
					#endregion

					#region Return journeys
					itineraryExists = (itineraryManager.ReturnLength > 0 );
					if (!itineraryExists)
						sb.Append( NO_RETURNJOURNEYS );
					else
					{
						sb.Append( RETURNJOURNEY_HEADING );

						TDJourneyResult journeyResult;
						TDJourneyViewState journeyViewState;
						JourneyControl.PublicJourney pubJourneyDetails;

						int itineraryLength = itineraryManager.Length;

						for (int i=itineraryLength-1; i>-1; i--)
						{
							journeyResult = (TDJourneyResult)itineraryManager.SpecificJourneyResult(i);
							journeyViewState = itineraryManager.SpecificJourneyViewState(i);

							// generate details for outward Itinerary journeys
							if (journeyViewState.SelectedOutwardJourneyType == TDJourneyType.RoadCongested)
							{
								sb.Append( CARJOURNEY_HEADING );
								sb.Append( "SEGMENT NUMBER: " + i.ToString() + "\r\n");

								// Gives us the Start and End Leg of the Car journey
								foreach (JourneyLeg jl in journeyResult.ReturnRoadJourney().JourneyLegs)
									sb.Append( ConvertToXML(jl) );

								sb.Append( GetCarDetail(true, journeyResult, journeyViewState, inputPageState.Units) );

							}
							else
							{
								sb.Append( PUBLICJOURNEY_HEADING );
								sb.Append( "SEGMENT NUMBER: " + i.ToString() + "\r\n");

								pubJourneyDetails = journeyResult.ReturnPublicJourney(journeyViewState.SelectedReturnJourneyID);
								sb.Append( ConvertToXML( pubJourneyDetails ) );
							}
						}
					}
					#endregion

					return sb.ToString();
				}
				catch (Exception ex)
				{
					// Error occured, return the message so it can be displayed in the Feedback viewer
					return ex.Message;
				}
			}
			else
			{
				return null;
			}
		}
		#endregion

		#endregion

		#region Private methods

		#region Create
		/// <summary>
		/// Creates the initial feedback record, uses the object wide parameters
		/// </summary>
		/// <returns>True/false to indicate success</returns>
		private bool CreateFeedback()
		{
			bool returnFlag;
			Hashtable parameters = new Hashtable();

			// Create the Feedback record
			try
			{
				//populate parameters with Initial values that we know
				sessionId  = TDSessionManager.Current.Session.SessionID;
				emailSubmittedTime = DateTime.Now;
				emailAcknowledgedTime = DateTime.Now;
				acknowledgementSent = false;
				userLoggedOn = TDSessionManager.Current.Authenticated;
				timeLogged = DateTime.Now;
				vantiveId = string.Empty;
				feedbackStatus = STATUS_INPROGRESS;
				deleteFlag = false;
								
				//open connection to database
				sqlHelper.ConnOpen(SqlHelperDatabase.UserInfoDB);

				// Add all the required parameters for the Stored Procedure to the hashtable
				// Session specific data is copied to the new Feedback record by the stored procedure
				parameters.Add( "@SessionId", sessionId);
				parameters.Add( "@SubmittedTime", emailSubmittedTime);
				parameters.Add( "@AcknowledgedTime", emailAcknowledgedTime);
				parameters.Add( "@AcknowledgementSent", acknowledgementSent);
				parameters.Add( "@UserLoggedOn", userLoggedOn);
				parameters.Add( "@TimeLogged", timeLogged);
				parameters.Add( "@VantiveId", vantiveId);
				parameters.Add( "@FeedbackStatus", feedbackStatus);
				parameters.Add( "@DeleteFlag", deleteFlag);
				
				// use stored procedure "AddUserFeedback" to add user feedback data to the database.
				int rowsUpdated = sqlHelper.Execute("AddUserFeedback", parameters);

				// obtain the FeedbackId to use in the email
				parameters.Clear();
				parameters.Add( "@SessionId", sessionId);
				feedbackId = (int)sqlHelper.GetScalar("GetUserFeedbackId", parameters);

				if (rowsUpdated == 1)
				{ 
					returnFlag = true;

					// write submit event to the log file
					Logger.Write(new OperationalEvent(TDEventCategory.Database,TDTraceLevel.Info,
						"User Feedback save process has created feedback record with Feedback ID: " + feedbackId.ToString() + " and Session ID: " + sessionId.ToString(TDCultureInfo.CurrentCulture)));
				}
				else
				{
					returnFlag = false;

					// write fail event to the log file
					Logger.Write(new OperationalEvent(TDEventCategory.Database,TDTraceLevel.Info,
						"User Feedback save process updated " + rowsUpdated + " rows. Create feedback record has failed. Session ID: " + sessionId.ToString(TDCultureInfo.CurrentCulture)));
				}			
			}
			catch (SqlException sqlEx)
			{
				returnFlag = false;

				//log error and throw exception
				string message = "SqlException caught in FeedbackHelper.SubmitFeedback method, creating feedback record : " + sqlEx.Message;
				Logger.Write(new OperationalEvent(	TDEventCategory.Database,
					TDTraceLevel.Error, "SQLNUM["+sqlEx.Number+"] :"+sqlEx.Message+":"));

				throw new TDException(message, sqlEx,false, TDExceptionIdentifier.UFESqlHelperError);			
				
			}
			catch (TDException tdex)
			{   
				returnFlag = false;

				//log error and throw exception
				string message = "Error creating a User Feedback in FeedbackHelper.SubmitFeedback method : " + tdex.Message;
				Logger.Write (new OperationalEvent(TDEventCategory.Infrastructure,
					TDTraceLevel.Error, "TDException :" + tdex.Message + ":"));				

				throw new TDException(message, tdex, false, TDExceptionIdentifier.UFECreateUserFeedbackFailure);
				
			}
			finally
			{
				//close the database connection
				sqlHelper.ConnClose();				
			}

			return returnFlag;
		}

		
		/// <summary>
		/// Adds Session infromation to the Feedback record
		/// </summary>
		/// <returns>True/false to indicate success</returns>
		private bool AddSessionInformation()
		{
			bool returnFlag;
			Hashtable parameters = new Hashtable();

			//Add the Session information to the feedback record
			try
			{									
				//open connection to database
				sqlHelper.ConnOpen(SqlHelperDatabase.UserInfoDB);				

				// Add all the required parameters for the Stored Procedure to the hashtable
				parameters.Clear();
				parameters.Add( "@FeedbackId", feedbackId);
				parameters.Add( "@SessionId", sessionId);
				
				//use stored procedure "AddSessionData" to copy session info to feedback record in database
				int rowsUpdated = sqlHelper.Execute("AddSessionData", parameters);

				//write submit event to the log file
				Logger.Write(new OperationalEvent(TDEventCategory.Database,TDTraceLevel.Info,
					"User Feedback session information has been saved to the UserFeedbackSessionData table in TDUserInfo database with Feedback ID: " + feedbackId.ToString() ));

				//method returns true if one row has been successfully updated
				returnFlag = true;
				
			}
			catch (SqlException sqlEx)
			{
				returnFlag = false;

				//log error and throw exception
				string message = "SqlException caught in FeedbackHelper.AddSessionInformation method, for feedback record : " + sqlEx.Message;
				Logger.Write(new OperationalEvent(	TDEventCategory.Database,
					TDTraceLevel.Error, "SQLNUM["+sqlEx.Number+"] :"+sqlEx.Message+":"));

				throw new TDException(message, sqlEx,false, TDExceptionIdentifier.UFESqlHelperError);			
				
			}
			catch (TDException tdex)
			{   
				returnFlag = false;

				//log error and throw exception
				string message = "Error updating a User Feedback in FeedbackHelper.SubmitFeedback method : " + tdex.Message;
				Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure,
					TDTraceLevel.Error, "TDException :" + tdex.Message + ":"));				

				throw new TDException(message, tdex, false, TDExceptionIdentifier.UFEUpdateUserFeedbackFailure);
				
			}
			finally
			{
				//close the database connection
				sqlHelper.ConnClose();				
			}

			return returnFlag;
		}

		/// <summary>
		/// Adds the Options selected by User during the Feedback process
		/// e.g. Report a Problem, Another journey
		/// </summary>
		/// <param name="userOptionsItems"></param>
		/// <returns>True/false to indicate success</returns>
		private bool AddFeedbackUserOptions(ArrayList userOptionsItems, string userEmailAddress, string userComments)
		{
			bool returnFlag;
			Hashtable parameters = new Hashtable();

			//Add the Session information to the feedback record
			try
			{									
				//open connection to database
				sqlHelper.ConnOpen(SqlHelperDatabase.UserInfoDB);

				int i = 0;
				// Add all the required parameters for the Stored Procedure to the hashtable				
				IEnumerator optionEnumerator = userOptionsItems.GetEnumerator();
				while (optionEnumerator.MoveNext() )
				{
					i++;

					parameters.Clear();
					parameters.Add( "@FeedbackId", feedbackId);
					parameters.Add( "@FeedbackTypeId", optionEnumerator.Current.ToString() );

					// Only add the Comments/Email to the last user option line
					if (i == userOptionsItems.Count)
					{
						parameters.Add( "@Details", userComments);
						parameters.Add( "@Email", userEmailAddress);
					}
					else
					{
						parameters.Add( "@Details", string.Empty);
						parameters.Add( "@Email", string.Empty);
					}

					parameters.Add( "@DeleteFlag", deleteFlag);

					//use stored procedure "AddFeedbackUserOptions"
					int rowsUpdated = sqlHelper.Execute("AddFeedbackUserOptions", parameters);
				}

				
				//write submit event to the log file
				Logger.Write(new OperationalEvent(TDEventCategory.Database,TDTraceLevel.Info,
					"User Feedback user option information has been saved to the UserFeedbackClassification table in TDUserInfo database with Feedback ID: " + feedbackId.ToString() ));

				//method returns true if row has been successfully updated
				returnFlag = true;
				
			}
			catch (SqlException sqlEx)
			{
				returnFlag = false;

				//log error and throw exception
				string message = "SqlException caught in FeedbackHelper.AddFeedbackUserOptions method, for feedback record : " + sqlEx.Message;
				Logger.Write(new OperationalEvent(	TDEventCategory.Database,
					TDTraceLevel.Error, "SQLNUM["+sqlEx.Number+"] :"+sqlEx.Message+":"));

				throw new TDException(message, sqlEx,false, TDExceptionIdentifier.UFESqlHelperError);			
				
			}
			catch (TDException tdex)
			{   
				returnFlag = false;

				//log error and throw exception
				string message = "Error updating a User Feedback in FeedbackHelper.SubmitFeedback method : " + tdex.Message;
				Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure,
					TDTraceLevel.Error, "TDException :" + tdex.Message + ":"));

				throw new TDException(message, tdex, false, TDExceptionIdentifier.UFEUpdateUserFeedbackFailure);
				
			}
			finally
			{
				//close the database connection
				sqlHelper.ConnClose();				
			}

			return returnFlag;
		}

		#endregion

		#region Update
		/// <summary>
		/// Updates the Feedback record, uses the object wide parameters 
		/// </summary>
		/// <returns>True/false to indicate success</returns>
		private bool UpdateFeedback()
		{
			bool returnFlag;
			Hashtable parameters = new Hashtable();

			//Update the feedback record
			try
			{	
				//open connection to database
				sqlHelper.ConnOpen(SqlHelperDatabase.UserInfoDB);				

				// Add all the required parameters for the Stored Procedure to the hashtable
				parameters.Clear();
				parameters.Add( "@FeedbackId", feedbackId);
				parameters.Add( "@SessionId", sessionId);
				parameters.Add( "@SubmittedTime", emailSubmittedTime);
				parameters.Add( "@AcknowledgedTime", emailAcknowledgedTime);
				parameters.Add( "@AcknowledgementSent", acknowledgementSent);
				parameters.Add( "@UserLoggedOn", userLoggedOn);
				parameters.Add( "@TimeLogged", timeLogged);
				parameters.Add( "@VantiveId", vantiveId);
				parameters.Add( "@FeedbackStatus", feedbackStatus);
				parameters.Add( "@DeleteFlag", deleteFlag);
				
				//use stored procedure "UpdateUserFeedback" to update user feedback data to the database
				int rowsUpdated = sqlHelper.Execute("UpdateUserFeedback", parameters);

				//method returns true if one row has been successfully updated
				if (rowsUpdated == 1)
				{ 
					returnFlag = true;

					//write submit event to the log file
					Logger.Write(new OperationalEvent(TDEventCategory.Database,TDTraceLevel.Info,
						"User Feedback has been Updated to the UserFeedback table TDUserInfo database with Feedback ID: " + feedbackId.ToString() + " and Session ID: " + sessionId.ToString(TDCultureInfo.CurrentCulture)));
				}
				else
				{
					returnFlag = false;

					// write fail event to the log file
					Logger.Write(new OperationalEvent(TDEventCategory.Database,TDTraceLevel.Info,
						"User Feedback Update process updated " + rowsUpdated + " rows. Update feedback record has failed. With Feedback ID: " + feedbackId.ToString() + " and Session ID: " + sessionId.ToString(TDCultureInfo.CurrentCulture)));
				}
				
			}
			catch (SqlException sqlEx)
			{
				returnFlag = false;

				//log error and throw exception
				string message = "SqlException caught in FeedbackHelper.SubmitFeedback method, updating feedback record : " + sqlEx.Message;
				Logger.Write(new OperationalEvent(	TDEventCategory.Database,
					TDTraceLevel.Error, "SQLNUM["+sqlEx.Number+"] :"+sqlEx.Message+":"));

				throw new TDException(message, sqlEx,false, TDExceptionIdentifier.UFESqlHelperError);			
				
			}
			catch (TDException tdex)
			{   
				returnFlag = false;

				//log error and throw exception
				string message = "Error updating a User Feedback in FeedbackHelper.SubmitFeedback method : " + tdex.Message;
				Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure,
					TDTraceLevel.Error, "TDException :" + tdex.Message + ":"));

				throw new TDException(message, tdex, false, TDExceptionIdentifier.UFEUpdateUserFeedbackFailure);
				
			}
			finally
			{
				//close the database connection
				sqlHelper.ConnClose();				
			}

			return returnFlag;
		}

		#endregion

		#region Get

		/// <summary>
		/// Retrieves the feedback record for supplied id
		/// </summary>
		/// <returns>Feedback record</returns>
		private Feedback RetrieveFeedback(int feedbackId)
		{
			Hashtable parameters = new Hashtable();

			Feedback feedback = null;

			try
			{
				//populate parameter
				parameters.Add( "@FeedbackId", feedbackId);

				//open connection to database
				sqlHelper.ConnOpen(SqlHelperDatabase.UserInfoDB);
				SqlDataReader reader = sqlHelper.GetReader("GetUserFeedback", parameters);

				// Assign Column Ordinals 
				int feedbackIdOrdinal = reader.GetOrdinal("FeedbackId");
				int sessionIdOrdinal = reader.GetOrdinal("SessionId");
				int sessionCreatedOrdinal = reader.GetOrdinal("SessionCreated");
				int sessionExpiresOrdinal = reader.GetOrdinal("SessionExpires");
				int emailSubmittedTimeOrdinal = reader.GetOrdinal("SubmittedTime");
				int emailAcknowledgedTimeOrdinal = reader.GetOrdinal("AcknowledgedTime");
				int acknowledgementSentOrdinal = reader.GetOrdinal("AcknowledgementSent");
				int userLoggedOnOrdinal = reader.GetOrdinal("UserLoggedOn");
				int timeLoggedOrdinal = reader.GetOrdinal("TimeLogged");
				int vantiveIdOrdinal = reader.GetOrdinal("VantiveId");
				int feedbackStatusOrdinal = reader.GetOrdinal("FeedbackStatus");
				int deleteFlagOrdinal = reader.GetOrdinal("DeleteFlag");

				// Data from database is held in the global variables
				// Only 1 feedback record will be returned
				while (reader.Read())
				{
					feedbackId = reader.GetInt32(feedbackIdOrdinal);
					sessionId = reader.GetString(sessionIdOrdinal);
					sessionCreated = reader.GetDateTime(sessionCreatedOrdinal);
					sessionExpires = reader.GetDateTime(sessionExpiresOrdinal);
					emailSubmittedTime = reader.GetDateTime(emailSubmittedTimeOrdinal);
					emailAcknowledgedTime = reader.GetDateTime(emailAcknowledgedTimeOrdinal);
					acknowledgementSent = reader.GetBoolean(acknowledgementSentOrdinal);
					userLoggedOn = reader.GetBoolean(userLoggedOnOrdinal);
					timeLogged = reader.GetDateTime(timeLoggedOrdinal);
					vantiveId = reader.GetString(vantiveIdOrdinal);
					feedbackStatus = reader.GetString(feedbackStatusOrdinal);
					deleteFlag = reader.GetBoolean(deleteFlagOrdinal);

					feedback = new Feedback(
						feedbackId,
						sessionId,			
						sessionCreated,
						sessionExpires, 
						emailSubmittedTime,
						emailAcknowledgedTime,
						acknowledgementSent,
						userLoggedOn,
						timeLogged,
						vantiveId,
						feedbackStatus,
						deleteFlag
						);
				}

				reader.Close();

				// Only attempt to get the Feedback obtions if a feedback object was created above
				if (feedback != null)
				{
					// Get the Feedback user options and add it to our Feedback object
					reader = sqlHelper.GetReader("GetUserFeedbackOptions", parameters);

					// Assign Column Ordinals 
					int feedbackTypeIdOrdinal = reader.GetOrdinal("FeedbackTypeId");
					int feedbackDetailsOrdinal = reader.GetOrdinal("Details");
					int feedbackEmailOrdinal = reader.GetOrdinal("Email");

					string feedbackTypeId = string.Empty;
					string optionValue = string.Empty;
					string options = string.Empty;
					string details = string.Empty;
					string email = string.Empty;

					while (reader.Read())
					{
						// Because options is returned as an ID, we need to get the text literal from a dataset
						feedbackTypeId = reader.GetString(feedbackTypeIdOrdinal);
						optionValue = GetFeedbackOptionValue(feedbackTypeId);
						options += "\r\n" + optionValue;

						// Each set of feedback options for a feedbackId will only have one set of Details and Email
						// entered by the user.
						if (reader.GetString(feedbackDetailsOrdinal) != null)
							details = reader.GetString(feedbackDetailsOrdinal);

						if (reader.GetString(feedbackEmailOrdinal) != null)
							email = reader.GetString(feedbackEmailOrdinal);
					}

					// Add the options string to Feedback object
					feedback.FeedbackOptions = options;
					feedback.FeedbackDetails = details;
					feedback.Email = email;

					reader.Close();
				}
			}
			catch (SqlException sqlEx)
			{
				//log error and throw exception
				string message = "SqlException caught in FeedbackHelper.RetrieveFeedback method, retrieving feedback record : " + sqlEx.Message;
				Logger.Write(new OperationalEvent(	TDEventCategory.Database,
					TDTraceLevel.Error, "SQLNUM["+sqlEx.Number+"] :"+sqlEx.Message+":"));

				throw new TDException(message, sqlEx,false, TDExceptionIdentifier.UFESqlHelperError);
			}
			catch (TDException tdex)
			{   
				//log error and throw exception
				string message = "Error retrieving a User Feedback in FeedbackHelper.RetrieveFeedback method : " + tdex.Message;
				Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure,
					TDTraceLevel.Error, "TDException :" + tdex.Message + ":"));	

				throw new TDException(message, tdex, false, TDExceptionIdentifier.UFEGetUserFeedbackRecordFailure);
				
			}
			finally
			{
				//close the database connection
				sqlHelper.ConnClose();
			}

			return feedback;
		}

		/// <summary>
		/// Translates the UserFeedback OptionId into its text string equivalent
		/// This involves call to retrieve value from a DataSet
		/// </summary>
		/// <param name="valueId"></param>
		/// <returns>String for Feedback Option value</returns>
		private string GetFeedbackOptionValue(string valueId)
		{
			string optionValue = string.Empty;

			// Because we have a number of Feedbackdatasets, we need to try different ones if we don't get a match.
			// Note - we perform look through Datasets in this order because options appear in this order
			// during a User Feedback submit process.
			
			if (populator.GetResourceId(DataServiceType.UserFeedbackType, valueId) != string.Empty)
				optionValue = "FeedbackType: " + populator.GetResourceId(DataServiceType.UserFeedbackType, valueId);
			
			else if (populator.GetResourceId(DataServiceType.UserFeedbackJourneyConfirm, valueId) != string.Empty)
				optionValue = "Journey Confirmation: " + populator.GetResourceId(DataServiceType.UserFeedbackJourneyConfirm, valueId);

			else if (populator.GetResourceId(DataServiceType.UserFeedbackJourneyResult, valueId) != string.Empty)
				optionValue = "Journey Result: " + populator.GetResourceId(DataServiceType.UserFeedbackJourneyResult, valueId);
			
			else if (populator.GetResourceId(DataServiceType.UserFeedbackOtherOptions, valueId) != string.Empty)
				optionValue = "OtherOption: " + populator.GetResourceId(DataServiceType.UserFeedbackOtherOptions, valueId);

			return optionValue;
		}

		#endregion

		#region Get Session Data

		/// <summary>
		/// Create an XML representtaion of the specified object
		/// </summary>
		/// <param name="obj"></param>
		/// <param name="requestId"></param>
		/// <returns>XML string, prefixed by request id</returns>
		private string ConvertToXML(object obj)
		{
			// Placing in a try catch because we're not confident all session objects can be XmlSerialized,
			// especially if the session was corrupted during User Feedback submit process
			try
			{
				if (obj != null)
				{
					XmlSerializer xmls = new XmlSerializer(obj.GetType());
					StringWriter sw = new StringWriter();
					xmls.Serialize(sw, obj);
					sw.Close();

					return sw.ToString();
				}
				else
					return null;
			}
			catch (Exception ex)
			{
				string message = "Unable to load information \r\n\r\nError during XmlSerialize for a Feedback session object : " 
					+ ex.Message 
					+ "\r\n" + ex.InnerException;

				Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure,
					TDTraceLevel.Error, "Error during XmlSerialize for retrieving a Feedback session object :" + ex.Message + ": \r\n" + ex.InnerException));

				// Throw back the exception to provide detail as to why we couldnt serialize
				throw new Exception(message);
			}
		}

		/// <summary>
		/// Converts the Road journey in TDJourneyResult into user viewable instructions
		/// </summary>
		/// <param name="outward"></param>
		/// <param name="journeyResult"></param>
		/// <param name="journeyViewState"></param>
		/// <param name="roadUnits"></param>
		/// <returns>String of Journey Details</returns>
		private string GetCarDetail (bool outward, TDJourneyResult journeyResult, TDJourneyViewState journeyViewState, RoadUnitsEnum roadUnits)
		{
			StringBuilder sb = new StringBuilder();

			// we reuse the Email formatter as it gives us a simple text view
			CarJourneyDetailFormatter detailFormatter = new EmailCarJourneyDetailFormatter(
				journeyResult,
				journeyViewState,
				outward,
				TDCultureInfo.CurrentUICulture,
				roadUnits, 
				false
				);

			IList instructions = detailFormatter.GetJourneyDetails();

			foreach (object[] details in instructions) 
			{
				sb.Append( "\r\n<Detail>" );
				sb.Append( details[0].ToString() + " :: " 
					+ details[1].ToString() + " :: "
					+ details[2].ToString() );
				sb.Append( "</Detail>" );
			}
			
			return sb.ToString();
		}

        /// <summary>
        /// Converts the Cycle journey in TDCyclePlannerResult into user viewable instructions
        /// </summary>
        /// <param name="outward"></param>
        /// <param name="journeyResult"></param>
        /// <param name="journeyViewState"></param>
        /// <param name="roadUnits"></param>
        /// <returns>String of Cycle Journey Details</returns>
        private string GetCycleDetail(bool outward, CycleJourney cycleJourney, TDJourneyViewState journeyViewState, RoadUnitsEnum roadUnits)
        {
            StringBuilder sb = new StringBuilder();

            // we reuse the Email formatter as it gives us a simple text view
            CycleJourneyDetailFormatter detailFormatter = new EmailCycleJourneyDetailFormatter(
                cycleJourney,
                journeyViewState,
                outward,
                roadUnits,
                false,
                true);

            IList instructions = detailFormatter.GetJourneyDetails();

            foreach (object[] details in instructions)
            {
                sb.Append("\r\n<Detail>");
                sb.Append(details[0].ToString() + " :: "
                    + details[1].ToString() + " :: "
                    + details[2].ToString() + " :: "
                    + details[3].ToString());
                sb.Append("</Detail>");
            }

            return sb.ToString();
        }

		/// <summary>
		/// Converts each item in the TDJourneyRequest object to a string. 
		/// To be used if the ConvertToXml fails
		/// </summary>
		/// <param name="jr">TDJourneyRequest</param>
		/// <returns>String of TDJourneyRequest</returns>
		private string ConvertJourneyRequestToString(TDJourneyRequest jr)
		{
			string nl = "\n";

			StringBuilder sb = new StringBuilder();

			sb.Append( "<Modes>" );
			sb.Append( nl );
			foreach (ModeType m in jr.Modes)
			{
				sb.Append( "  <ModeType>" + m.ToString() );
				sb.Append( nl );
			}
			sb.Append( "<IsReturnRequired>" + jr.IsReturnRequired.ToString() );
			sb.Append( nl );
			sb.Append( "<OutwardArriveBefore>" + jr.OutwardArriveBefore.ToString() );
			sb.Append( nl );
			sb.Append( "<ReturnArriveBefore>" + jr.ReturnArriveBefore.ToString() );
			sb.Append( nl );
			foreach (TDDateTime dt in jr.OutwardDateTime)
			{
				sb.Append( "<OutwardDateTime>" + dt.ToString() );
				sb.Append( nl );
			}
			foreach (TDDateTime dt in jr.ReturnDateTime)
			{
				sb.Append( "<ReturnDateTime>" + dt.ToString() );
				sb.Append( nl );
			}
			sb.Append( "<InterchangeSpeed>" + jr.InterchangeSpeed.ToString() );
			sb.Append( nl );
			sb.Append( "<WalkingSpeed>" + jr.WalkingSpeed.ToString() );
			sb.Append( nl );
			sb.Append( "<MaxWalkingTime>" + jr.MaxWalkingTime.ToString() );
			sb.Append( nl );
			sb.Append( "<DrivingSpeed>" + jr.DrivingSpeed.ToString() );
			sb.Append( nl );
			sb.Append( "<AvoidMotorways>" + jr.AvoidMotorways.ToString() );
			sb.Append( nl );
			sb.Append( "<DoNotUseMotorways>" + jr.DoNotUseMotorways.ToString() );
			sb.Append( nl );
			
			#region Locations
			// Origin location
			sb.Append( "<OriginLocation>" );
			sb.Append( nl );
			sb.Append( ConvertLocationToString( jr.OriginLocation ) );			
			// Destination location
			sb.Append( "<DestinationLocation>" );
			sb.Append( nl );
			sb.Append( ConvertLocationToString( jr.DestinationLocation ) );
			// Return origin location
			sb.Append( "<ReturnOriginLocation>" );
			sb.Append( nl );
			sb.Append( ConvertLocationToString( jr.ReturnOriginLocation ) );
			// Return destination location
			sb.Append( "<ReturnDestinationLocation>" );
			sb.Append( nl );
			sb.Append( ConvertLocationToString( jr.ReturnDestinationLocation ) );
			// Public via locations
			sb.Append( "<PublicViaLocations>" );
			sb.Append( nl );
			foreach (TDLocation loc in jr.PublicViaLocations)
			{
				sb.Append( " <Location>" + nl );
				sb.Append( ConvertLocationToString( loc ) );
			}
			// Private via location
			sb.Append( "<PrivateViaLocation>" );
			sb.Append( nl );
			sb.Append( ConvertLocationToString( jr.PrivateViaLocation ) );
			#endregion

			sb.Append( "<TrainUidFilterIsInclude>" + jr.TrainUidFilterIsInclude.ToString() );
			sb.Append( nl );
			sb.Append( "<AvoidRoads>" );
			sb.Append( nl );
			if (jr.AvoidRoads != null)
			{
				foreach (String s in jr.AvoidRoads)
				{
					sb.Append( "  <Road>" + s );
					sb.Append( nl );
				}
			}

			// Alternate locations
			sb.Append( "<AlternateLocationsFrom>" + jr.AlternateLocationsFrom.ToString() );
			sb.Append( nl );
			sb.Append( "<PrivateAlgorithm>" + jr.PrivateAlgorithm.ToString() );
			sb.Append( nl );
			sb.Append( "<PublicAlgorithm>" + jr.PublicAlgorithm.ToString() );
			sb.Append( nl );
			sb.Append( "<ExtraCheckinTime>" + jr.ExtraCheckinTime.ToString() );
			sb.Append( nl );
			sb.Append( "<IsTrunkRequest>" + jr.IsTrunkRequest.ToString() );
			sb.Append( nl );
			sb.Append( "<UseOnlySpecifiedOperators>" + jr.UseOnlySpecifiedOperators.ToString() );
			sb.Append( nl );
			sb.Append( "<SelectedOperators>" );
			sb.Append( nl );
			foreach (String s in jr.SelectedOperators)
			{
				sb.Append( "  <Operator>" + s );
				sb.Append( nl );
			}
			sb.Append( "<DirectFlightsOnly>" + jr.DirectFlightsOnly.ToString() );
			sb.Append( nl );
			sb.Append( "<OutwardAnyTime>" + jr.OutwardAnyTime.ToString() );
			sb.Append( nl );
			sb.Append( "<ReturnAnyTime>" + jr.ReturnAnyTime.ToString() );
			sb.Append( nl );
			sb.Append( "<AvoidTolls>" + jr.AvoidTolls.ToString() );
			sb.Append( nl );
			if (jr.FuelPrice != null)
			{
				sb.Append( "<FuelPrice>" + jr.FuelPrice.ToString() );
				sb.Append( nl );
			}
			sb.Append( "<AvoidFerries>" + jr.AvoidFerries.ToString() );
			sb.Append( nl );
			if (jr.FuelConsumption != null)
			{
				sb.Append( "<FuelConsumption>" + jr.FuelConsumption.ToString() );
				sb.Append( nl );
			}
			if (jr.IncludeRoads != null)
			{
				sb.Append( "<IncludeRoads>" );
				sb.Append( nl );
				foreach (String s in jr.IncludeRoads)
				{
					sb.Append( "  <Road>" + s );
					sb.Append( nl );
				}
			}
			if (jr.CarSize != null)
			{
				sb.Append( "<VehicleType>" + jr.VehicleType.ToString() );
				sb.Append( nl );
				sb.Append( "<CarSize>" + jr.CarSize.ToString() );
				sb.Append( nl );
				sb.Append( "<FuelType>" + jr.FuelType.ToString() );
				sb.Append( nl );
			}
			sb.Append( "<Sequence>" + jr.Sequence.ToString() );
			sb.Append( nl );
			sb.Append( "<IsDirty>" + jr.IsDirty.ToString() );
			sb.Append( nl );

			return sb.ToString();
		}

		/// <summary>
		/// Converts each item in the TDLocation object to a string.
		/// To be used if the ConvertToXml fails
		/// </summary>
		/// <param name="loc">TDLocation</param>
		/// <returns>String of TDLocation</returns>
		private string ConvertLocationToString(TDLocation loc)
		{
			string nl = "\n";

			StringBuilder sb = new StringBuilder();

			if (loc != null)
			{
				sb.Append( "  <Description>" + loc.Description.ToString() );
				sb.Append( nl );
				sb.Append( "  <GridReference>" + loc.GridReference.Easting.ToString() + ", " + loc.GridReference.Northing.ToString() );
				sb.Append( nl );
				sb.Append( "  <Locality>" + loc.Locality.ToString() );
				sb.Append( nl );

				#region Naptans
				sb.Append( "  <NaPTANs>" );
				sb.Append( nl );
				foreach (TDNaptan n in loc.NaPTANs)
				{
					sb.Append( "    <TDNaPTAN>" );
					sb.Append( nl );
					sb.Append( "      <GridReference>" + n.GridReference.Easting.ToString() + ", " + n.GridReference.Northing.ToString() );
					sb.Append( nl );
					sb.Append( "      <NaPTAN>" + n.Naptan );
					sb.Append( nl );
					sb.Append( "      <Name>" + n.Name );
					sb.Append( nl );
					sb.Append( "      <UseForFareEnquiries>" + n.UseForFareEnquiries.ToString() );
					sb.Append( nl );
					sb.Append( "      <Locality>" + n.Locality );
					sb.Append( nl );
				}
				#endregion

				sb.Append( "  <Toid>" );
				sb.Append( nl );
				foreach (String s in loc.Toid)
				{
					sb.Append( "    <string>" + s );
					sb.Append( nl );
				}
				sb.Append( "  <SearchType>" + loc.SearchType.ToString() );
				sb.Append( nl );
				sb.Append( "  <RequestPlaceType>" + loc.RequestPlaceType.ToString() );
				sb.Append( nl );
				sb.Append( "  <Status>" + loc.Status.ToString() );
				sb.Append( nl );
				sb.Append( "  <PartPostcode>" + loc.PartPostcode.ToString() );
				sb.Append( nl );
				sb.Append( "  <PartPostcodeMaxX>" + loc.PartPostcodeMaxX.ToString() );
				sb.Append( nl );
				sb.Append( "  <PartPostcodeMaxY>" + loc.PartPostcodeMaxY.ToString() );
				sb.Append( nl );
				sb.Append( "  <PartPostcodeMinX>" + loc.PartPostcodeMinX.ToString() );
				sb.Append( nl );
				sb.Append( "  <PartPostcodeMinY>" + loc.PartPostcodeMinY.ToString() );
				sb.Append( nl );
				sb.Append( "  <AddressToMatch>" + loc.AddressToMatch );
				sb.Append( nl );
			}

			return sb.ToString();
		}

        /// <summary>
        /// Converts each item in the TDCyclePlannerRequest object to a string. 
        /// To be used if the ConvertToXml fails
        /// </summary>
        /// <param name="jr">TDCyclePlannerRequest</param>
        /// <returns>String of TDCyclePlannerRequest</returns>
        private string ConvertCycleRequestToString(TDCyclePlannerRequest jr)
        {
            string nl = "\n";

            StringBuilder sb = new StringBuilder();

            sb.Append("<IsReturnRequired>" + jr.IsReturnRequired.ToString());
            sb.Append(nl);
            sb.Append("<OutwardArriveBefore>" + jr.OutwardArriveBefore.ToString());
            sb.Append(nl);
            sb.Append("<ReturnArriveBefore>" + jr.ReturnArriveBefore.ToString());
            sb.Append(nl);
            foreach (TDDateTime dt in jr.OutwardDateTime)
            {
                sb.Append("<OutwardDateTime>" + dt.ToString());
                sb.Append(nl);
            }
            foreach (TDDateTime dt in jr.ReturnDateTime)
            {
                sb.Append("<ReturnDateTime>" + dt.ToString());
                sb.Append(nl);
            }

            sb.Append("<OutwardAnyTime>" + jr.OutwardAnyTime.ToString());
            sb.Append(nl);
            sb.Append("<ReturnAnyTime>" + jr.ReturnAnyTime.ToString());
            sb.Append(nl);

            #region Locations
            // Origin location
            sb.Append("<OriginLocation>");
            sb.Append(nl);
            sb.Append(ConvertLocationToString(jr.OriginLocation));
            // Destination location
            sb.Append("<DestinationLocation>");
            sb.Append(nl);
            sb.Append(ConvertLocationToString(jr.DestinationLocation));
            // Cycle via locations
            sb.Append("<CycleViaLocations>");
            sb.Append(nl);
            foreach (TDLocation loc in jr.CycleViaLocations)
            {
                sb.Append(" <Location>" + nl);
                sb.Append(ConvertLocationToString(loc));
            }
            #endregion

            sb.Append("<CycleJourneyType>");
            sb.Append(nl);
            sb.Append(jr.CycleJourneyType);

            sb.Append("<PenaltyFunction>");
            sb.Append(nl);
            sb.Append(jr.PenaltyFunction);

            sb.Append("<UserPreferences>");
            sb.Append(nl);

            foreach (TDCycleUserPreference preference in jr.UserPreferences)
            {
                sb.Append("<UserPreference>");
                sb.Append(nl);
                sb.Append("<parameterID>");
                sb.Append(nl);
                sb.Append(preference.PreferenceKey);
                sb.Append("<parameterValue>");
                sb.Append(nl);
                sb.Append(preference.PreferenceValue);
            }
     
            sb.Append("<IsDirty>" + jr.IsDirty.ToString());
            sb.Append(nl);

            return sb.ToString();
        }

		/// <summary>
		/// Retrieve session information for supplied SessionId and PartionableDeferredKey Key.
		/// This retrieves session information from the TDUserInfo UserFeedbackSessionData table
		/// Time based partition is looked at first, and if null then Cost based looked at.
		/// </summary>
		/// <param name="feedbackId">FeedbackId required</param>
		/// <param name="sessionId"></param>
		/// <param name="key"></param>
		/// <returns>object of session information</returns>
		private object RetrieveFeedbackSessionData(int feedbackId, string sessionId, PartionableDeferredKey key)
		{
			TDSessionSerializer ser = new TDSessionSerializer();
			object sessionObject = null;
						
			// We don't know if user had originall performed a Time or Cost based search
			// so try both if one fails
			sessionObject = ser.RetrieveAndDeserializeFeedbackSessionObject(feedbackId, sessionId, TDSessionPartition.TimeBased, key);

			if (sessionObject == null)
				sessionObject = ser.RetrieveAndDeserializeFeedbackSessionObject(feedbackId, sessionId, TDSessionPartition.CostBased, key);

			return sessionObject;
		}

		#endregion

		#region Delete

		/// <summary>
		/// Sets the Delete status of all record associated with a Feedback ID to true
		/// This does not actually delete the records in the database - this is completed by 
		/// a database process
		/// </summary>
		/// <returns>True/false to indicate success</returns>
		private bool DeleteFeedback()
		{
			bool returnFlag;
			Hashtable parameters = new Hashtable();

			//Update the feedback record
			try
			{
				//open connection to database
				sqlHelper.ConnOpen(SqlHelperDatabase.UserInfoDB);				

				// Add all the required parameters for the Stored Procedure to the hashtable
				parameters.Clear();
				parameters.Add( "@FeedbackId", feedbackId);
				parameters.Add( "@DeleteFlag", deleteFlag);
							
				// Use stored procedure "UpdateUserFeedbackToDelete" to update user feedback data to the database
				// as this procedure marks the Feedback and all related child records to be deleted
				int rowsUpdated = sqlHelper.Execute("UpdateUserFeedbackToDelete", parameters);

				//write submit event to the log file
				Logger.Write(new OperationalEvent(TDEventCategory.Database,TDTraceLevel.Info,
					"User Feedback has been marked for Delete with Feedback ID: " + feedbackId.ToString()));

				//method returns true if one row has been successfully updated
				returnFlag = true;				
			}
			catch (SqlException sqlEx)
			{
				returnFlag = false;

				//log error and throw exception
				string message = "SqlException caught in FeedbackHelper.DeleteFeedback method, updating feedback record : " + sqlEx.Message;
				Logger.Write(new OperationalEvent(	TDEventCategory.Database,
					TDTraceLevel.Error, "SQLNUM["+sqlEx.Number+"] :"+sqlEx.Message+":"));

				throw new TDException(message, sqlEx,false, TDExceptionIdentifier.UFESqlHelperError);			
				
			}
			catch (TDException tdex)
			{   
				returnFlag = false;

				//log error and throw exception
				string message = "Error updating a User Feedback in FeedbackHelper.SubmitFeedback method : " + tdex.Message;
				Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure,
					TDTraceLevel.Error, "TDException :" + tdex.Message + ":"));

				throw new TDException(message, tdex, false, TDExceptionIdentifier.UFEUpdateUserFeedbackFailure);				
			}
			finally
			{
				//close the database connection
				sqlHelper.ConnClose();				
			}

			return returnFlag;
		}

		#endregion

		#region Email
		/// <summary>
		/// Sends Feedback email to helpdesk and to user (if supplied)
		/// </summary>
		/// <param name="userEmailAddress"></param>
		/// <param name="userComments"></param>
		/// <param name="userOptionsValues"></param>
		/// <param name="feedbackType"></param>
		/// <returns>True/false to indicate success</returns>
		private bool SubmitEmail(string userEmailAddress, string userComments, ArrayList userOptionsValues, int feedbackType)
		{
			bool returnFlag = true;
			string feedbackSubjectLine = CreateEmailSubject(userOptionsValues);
			string feedbackBody = string.Empty;

			#region Email Body

			StringBuilder tempBody = new StringBuilder();
			tempBody.Append ( feedbackSubjectLine );
			tempBody.Append ( "\r\n"  );
			tempBody.Append ( "\r\n"  );
			tempBody.Append ( "Comments : " );
			tempBody.Append ( userComments );
			tempBody.Append ( "\r\n" );

			if (userEmailAddress.Length > 0)
			{
				tempBody.Append ( "\r\n" );
				tempBody.Append ( "User Email : " );
				tempBody.Append ( userEmailAddress );
				tempBody.Append ( "\r\n" );
			}
			
			tempBody.Append ( "\r\n" );
			tempBody.Append ( "Date Submitted : " );
			tempBody.Append ( DateTime.Now );			
			tempBody.Append ( "\r\n" );

			// Only add Feedback Id if a Problem is being submitted
			if (feedbackType == 0)
			{
				tempBody.Append ( "Feedback Reference : " );
				tempBody.Append ( feedbackId.ToString() );
			}

            // Add the url host and theme to detemine which whitelabel site this is being submitted for
            Theme currentTheme = TD.ThemeInfrastructure.ThemeProvider.Instance.GetTheme();
            HttpContext context = HttpContextHelper.GetCurrent();
            
            tempBody.Append("\r\n");
            tempBody.Append("Site Host : ");
            tempBody.Append(context.Request.Url.Host);
            tempBody.Append("\r\n");
            tempBody.Append("Site Theme : ");
            tempBody.Append(currentTheme.Name);
            tempBody.Append("\r\n");

			feedbackBody = tempBody.ToString();

			#endregion

			//CustomEmailEvent used to send an email
			CustomEmailEvent ce = null;		
				
			//Place all in try..catch for sanity
			try
			{
				//evaluate which type of feedback 
				switch (feedbackType)
				{	
						//Feedback type = Report a problem
					case (0):
											
						// Get mailAddress from Properties database
						string mailAddress1 = Properties.Current["feedback.emailaddress.general"];
						if (mailAddress1 == null)
						{
							OperationalEvent oe = new OperationalEvent(
								TDEventCategory.Infrastructure,
								TDTraceLevel.Error,
								"Missing property in Property database : 'feedback.emailaddress.general'");
							Logger.Write(oe);

							//if fails throw exception
							throw new TDException(
								"Missing property in Property database : 'feedback.emailaddress.general'",
								TDSessionManager.Current.Authenticated,
								TDExceptionIdentifier.PSMissingProperty);
						}

						//Create Email
						ce = new CustomEmailEvent(mailAddress1, feedbackBody, feedbackSubjectLine);
						Logger.Write(ce);
						//record the time submitted
						emailSubmittedTime = DateTime.Now;
						break;

						//Feedback type = suggestion
					case (1):					
						
						// Get mailAddress from Properties database
						string mailAddress2 = Properties.Current["feedback.emailaddress.suggestion"];
						if (mailAddress2 == null)
						{
							OperationalEvent oe = new OperationalEvent(
								TDEventCategory.Infrastructure,
								TDTraceLevel.Error,
								"Missing property in Property database : 'feedback.emailaddress.suggestion'");
							Logger.Write(oe);

							//if fails throw exception
							throw new TDException(
								"Missing property in Property database : 'feedback.emailaddress.suggestion'",
								TDSessionManager.Current.Authenticated,
								TDExceptionIdentifier.PSMissingProperty);
						}

						//Create Email
						ce = new CustomEmailEvent(mailAddress2, feedbackBody, feedbackSubjectLine);
						Logger.Write(ce);
						//record the time submitted
						emailSubmittedTime = DateTime.Now;
						break;				

						//default
					default:
						string mailAddress3 = Properties.Current["feedback.emailaddress.suggestion"];
						ce = new CustomEmailEvent(mailAddress3, feedbackBody, feedbackSubjectLine);
						Logger.Write(ce);
						break;
				}

				//if user has entered an email address, send an acknowledgement email
				if (userEmailAddress.Length > 0)
				{	
					
					//session ID
					string refID  = TDSessionManager.Current.Session.SessionID;
					
					//body text of email - Amended for IR2570 to include contents of CommentsTextBox at 
					//the end of the e-mail body text.
					string bodyText = string.Empty;
					string feedbackSeperator = "\r\n\r\n\r\nYour message:\r\n-------------\r\n";
					bodyText = Global.tdResourceManager.GetString
						("FeedbackInitialPage.AcknowledgementComment", TDCultureInfo.CurrentUICulture) + feedbackSeperator + userComments;	
                    bodyText =  HttpUtility.HtmlDecode(bodyText);

					//subject line of email
					string subjectLine = Global.tdResourceManager.GetString
						("FeedbackInitialPage.SubjectLine", TDCultureInfo.CurrentUICulture);
								
					//Send email to user
					try
					{							
						CustomEmailEvent me = null;
						me = new CustomEmailEvent(userEmailAddress, bodyText, subjectLine);
						Logger.Write(me);						
						emailAcknowledgedTime = DateTime.Now;
						acknowledgementSent = true;
					}					
						//log UserFeedbackEvent to reporting database,
						//even if an acknowledgement email was not sent for any reason
					finally
					{													
						
						userLoggedOn = TDSessionManager.Current.Authenticated;
						UserFeedbackEvent ufe = null;
						//next lines will store the string version of UserFeedbackEventCategory rather than its integer representation
						UserFeedbackEventCategory eventCategory;
						// Doing this because of Feedback changes, the Suggestion enum is 2.
						// so if feedbackType is 1, it is a suggestion so +1;
						if (feedbackType == 1)
						{
							eventCategory = (UserFeedbackEventCategory)(feedbackType + 1);
						}
						else
						{
							eventCategory = (UserFeedbackEventCategory)feedbackType;
						}
						string feedbackTypeText = eventCategory.ToString();
						//acknowledgementSent will be false if a reply was not sent successfully
						ufe = new UserFeedbackEvent(feedbackTypeText,emailSubmittedTime, emailAcknowledgedTime,acknowledgementSent,refID, userLoggedOn ); 
						Logger.Write(ufe);

						//log the sending of the email
						OperationalEvent oe = new OperationalEvent(
							TDEventCategory.Infrastructure,
							TDTraceLevel.Info,
							"User Feedback acknowledgement sent to user. Submitted success = " + acknowledgementSent.ToString());
						Logger.Write(oe);
					}					
				}
					//if the user did not enter an email for an acknowledgement, then log this fact
				else
				{
					OperationalEvent oe = new OperationalEvent(
						TDEventCategory.Infrastructure,
						TDTraceLevel.Info,
						"User Feedback acknowledgement not requested");
					Logger.Write(oe);
				}

				returnFlag = true;
			}
			catch (TDException tdex)
			{
				returnFlag = false;

				//log error and throw exception
				string message = "Error sending a User Feedback email in FeedbackHelper.SubmitEmail method : " + tdex.Message;
				Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure,
					TDTraceLevel.Error, "TDException :" + tdex.Message + ":"));				

				throw new TDException(message, tdex, false, TDExceptionIdentifier.UFESendEmailFailure);
			}

			return returnFlag;
		}

		/// <summary>
		/// Creates the email subject based on the user options supplied
		/// </summary>
		/// <param name="userOptionValues"></param>
		/// <returns>Email subject as string</returns>
		private string CreateEmailSubject(ArrayList userOptionValues)
		{
			StringBuilder emailSubject = new StringBuilder();

			emailSubject.Append ("TDP Feedback");

			IEnumerator optionEnumerator = userOptionValues.GetEnumerator();
			while (optionEnumerator.MoveNext() )
			{
				emailSubject.Append (" - ");
				emailSubject.Append (optionEnumerator.Current.ToString());				
			}
			
			return emailSubject.ToString();
		}

		#endregion

		#endregion

	}
}
