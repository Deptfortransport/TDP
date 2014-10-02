// *********************************************** 
// NAME                 : DetailedLegMap.aspx.cs
// AUTHOR               : Peter Norell
// DATE CREATED         : 11/09/2003
// DESCRIPTION			: Detailed leg map.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/JourneyPlanning/DetailedLegMap.aspx.cs-arc  $
//
//   Rev 1.3   Jan 20 2013 16:27:14   mmodi
//Updated for transport modetype Telecabine
//Resolution for 5884: CCN:XXX - Telecabine modetype
//
//   Rev 1.2   Mar 31 2008 13:24:12   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:29:08   mturner
//Initial revision.
//
//DEVFACTORY FEB 22 2008 sbarker
//White labelling
//
//   Rev 1.36   Feb 23 2006 18:41:20   aviitanen
//Manual merge for Enhanced Exposed Services (stream3129).
//
//   Rev 1.35   Feb 10 2006 15:08:48   build
//Automatically merged from branch for stream3180
//
//   Rev 1.34.1.0   Dec 06 2005 16:25:18   NMoorhouse
//TD101 Home Page Phase 2 - Standard page changes
//Resolution for 3180: DEL 8.1 Stream UEE: work on Homepage phase 2
//
//   Rev 1.34   Nov 14 2005 14:35:28   ralonso
//Global.tdResourceManager.GetString replaced with GetResource to update legacy code
//
//   Rev 1.33   Nov 03 2005 16:00:28   kjosling
//Automatically merged from branch for stream2816
//
//   Rev 1.32.1.0   Oct 24 2005 17:40:18   ralonso
//TD089 ES020 image button replacement
//
//   Rev 1.32   Nov 04 2004 11:10:10   JHaydock
//Performance enhancement - use of BLOBs for road route journeys
//
//   Rev 1.31   Oct 12 2004 16:38:58   jgeorge
//Corrections to ensure header control displays correctly.
//Resolution for 1691: Quick Planner - Top tab reverts to door to door when clicking on map in journey details
//
//   Rev 1.30   Oct 08 2004 17:54:18   esevern
//added HeaderControlFindA
//
//   Rev 1.29   Sep 30 2004 12:13:00   rhopkins
//IR1648 Use ReturnOriginLocation and ReturnDestinationLocation when outputting Return Car segments of Extended Journeys.
//
//   Rev 1.28   Sep 17 2004 15:14:22   COwczarek
//Removal of unadjusted road journey planning
//Resolution for 1564: Remove unadjusted route functionality
//
//   Rev 1.27   Jul 05 2004 16:56:40   rgreenwood
//Fixed IR1083 by adding ModeType.Walk to getImageResourceKey switch block
//
//   Rev 1.26   Jun 17 2004 17:27:30   jbroome
//Updated for use with TDItineraryManager and to show road journeys.
//
//   Rev 1.25   Jun 17 2004 12:28:34   JHaydock
//Update to use SelectedInitimediateItinerarySegment where appropriate
//
//   Rev 1.24   Jun 17 2004 10:00:50   jbroome
//Extend Journey. Checks for use of Itinerary Manager. Work in progress.
//
//   Rev 1.23   Apr 19 2004 10:51:48   COwczarek
//Display new transport mode icon for rail replacement bus mode
//Resolution for 697: Bus replacement change
//
//   Rev 1.22   Apr 16 2004 11:04:34   COwczarek
//Page now shows leg locations in title rather than leg number
//Resolution for 782: Map for leg should show names of origin and destination
//
//   Rev 1.21   Dec 17 2003 16:57:58   kcheung
//Updated map logging.
//
//   Rev 1.20   Nov 17 2003 16:38:20   kcheung
//Added catch for MapExceptionGeneral
//
//   Rev 1.19   Nov 17 2003 15:23:48   hahad
//changed the title Tag so it it reads 'Transport Direct' from Langstrings
//
//   Rev 1.18   Nov 05 2003 12:29:48   kcheung
//Added header coments 
//
//   Rev 1.17   Oct 28 2003 11:10:08   kcheung
//Fixed name of imageButtonBack for FXCOP
//
//   Rev 1.16   Oct 22 2003 10:25:32   kcheung
//Fixed the ZoomToEnvelope bug where it was throwing an exception when the envelope was too small.
//
//   Rev 1.15   Oct 17 2003 14:02:42   PNorell
//Fixed minor bugs.
//
//   Rev 1.14   Oct 13 2003 19:38:56   PNorell
//Added correct map logging.
//
//   Rev 1.13   Oct 13 2003 09:41:50   PNorell
//Fixed alt texts for the image buttons.
//
//   Rev 1.12   Oct 10 2003 12:52:22   kcheung
//Page Title read from lang strings.
//
//   Rev 1.11   Oct 09 2003 19:48:32   PNorell
//Corrected zooming functionality.
//
//   Rev 1.10   Oct 03 2003 14:56:32   PNorell
//Added file header.
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Logger = System.Diagnostics.Trace;

using TransportDirect.Common;
using TransportDirect.Common.DatabaseInfrastructure;
using TransportDirect.Common.Logging;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ResourceManager;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.Presentation.InteractiveMapping;
using TransportDirect.ReportDataProvider.TDPCustomEvents;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.Resource;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.Web;
using TransportDirect.UserPortal.Web.Controls;
using TransportDirect.Web.Support;

namespace TransportDirect.UserPortal.Web.Templates
{
	/// <summary>
	/// DetailedLegMap presents the leg on the map with an appropriate headline and a back button.
	/// </summary>
	public partial class DetailedLegMap : TransportDirect.UserPortal.Web.TDPage
	{
		protected TransportDirect.UserPortal.Web.Controls.HeaderControl headerControl;
		//td Button
		
		private DateTime operationStartedDateTime;
		private bool outward = true;

		#region Constructor and Page Load

		/// <summary>
		/// Constructor - sets the page Id
		/// </summary>
		public DetailedLegMap()
		{
			pageId = PageId.DetailedLegMap;
		}

		/// <summary>
		/// Page Load - sets up the page.
		/// </summary>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Map.ServerName = Properties.Current["InteractiveMapping.Map.ServerName"];
			Map.ServiceName = Properties.Current["InteractiveMapping.Map.ServiceName"];
            Journey journey = null;
			ITDJourneyRequest journeyRequest = null;
			// Put user code to initialize the page here

			if (!IsPostBack)
			{
				ITDJourneyResult journeyResult = null;
				TDItineraryManager itineraryManager = TDItineraryManager.Current;
				TDJourneyViewState 	viewState = itineraryManager.JourneyViewState;

				Map.OnMapChangedEvent += new MapChangedEventHandler(this.Map_Changed);

				buttonBack.Text = GetResource("JourneyPlanner.buttonBack.Text");
				
				if (viewState == null)
				{
					return;
				}
				else if (viewState.SelectedIntermediateItinerarySegment < 0)
				{
					journeyResult = itineraryManager.JourneyResult;
					journeyRequest = itineraryManager.JourneyRequest;
				}
				else
				{
					journeyResult = itineraryManager.SpecificJourneyResult(viewState.SelectedIntermediateItinerarySegment);
					journeyRequest = itineraryManager.SpecificJourneyRequest(viewState.SelectedIntermediateItinerarySegment);
					viewState = itineraryManager.SpecificJourneyViewState(viewState.SelectedIntermediateItinerarySegment);
					viewState.SelectedIntermediateItinerarySegment = -1;
				}

				if (journeyResult == null)
					return;

				// outward or return leg selected?
				outward = TDSessionManager.Current.Session[SessionKey.JourneyMapOutward];
				bool arriveBefore = outward ? viewState.JourneyLeavingTimeSearchType : viewState.JourneyReturningTimeSearchType;
				JourneySummaryLine[] summaryLine = outward ? journeyResult.OutwardJourneySummary(arriveBefore) : journeyResult.ReturnJourneySummary(arriveBefore);
				int selectedJourney = outward ? viewState.SelectedOutwardJourney : viewState.SelectedReturnJourney;
				JourneySummaryLine summary = summaryLine[selectedJourney];

				if(outward)
				{
					if (summary.Type == TDJourneyType.PublicOriginal)
						journey = journeyResult.OutwardPublicJourney(summary.JourneyIndex);
					else if (summary.Type == TDJourneyType.PublicAmended)
						journey = journeyResult.AmendedOutwardPublicJourney;
					else
						journey = journeyResult.OutwardRoadJourney();
				}
				else
				{
					if (summary.Type == TDJourneyType.PublicOriginal)
						journey = journeyResult.ReturnPublicJourney(summary.JourneyIndex);
					else if (summary.Type == TDJourneyType.PublicAmended)
						journey = journeyResult.AmendedReturnPublicJourney;
					else
						journey = journeyResult.ReturnRoadJourney();
				}


				try
				{

					if (journey is JourneyControl.PublicJourney)
					{
						JourneyControl.PublicJourney publicJourney = (JourneyControl.PublicJourney)journey;

						// Add route to map
						Map.AddPTRoute(Session.SessionID, publicJourney.RouteNum);
						// Zoom the map to the correct route leg coords
						double minEasting = int.MaxValue;
						double maxEasting = 0;
						double minNorthing = int.MaxValue;
						double maxNorthing = 0;

						OSGridReference[] osgr = publicJourney.Details[viewState.SelectedJourneyLeg].Geometry;
						if (osgr.Length == 0)
						{
							// No geomtry - cannot zoom on map -> default map used
							return;
						}

				
						for (int i = 0; i < osgr.Length; i++)
						{
							minEasting = Math.Min( osgr[i].Easting, minEasting );
							maxEasting = Math.Max( osgr[i].Easting, maxEasting );
							minNorthing = Math.Min( osgr[i].Northing, minNorthing );
							maxNorthing = Math.Max( osgr[i].Northing, maxNorthing );
						}
						double eastingPadding = Math.Max((maxEasting - minEasting) / 20, 300 - (maxEasting - minEasting) / 2);
						minEasting = minEasting - eastingPadding;
						maxEasting = maxEasting + eastingPadding;

						double northingPadding = Math.Max((maxNorthing - minNorthing) / 20, 300 - (maxNorthing - minNorthing) / 2);
						minNorthing = minNorthing - northingPadding;
						maxNorthing = maxNorthing + northingPadding;

						string altText = GetResource("DetailedLegMap.DisplayMap.AlternateText");
						string legLocationsText = getLegLocationsText(publicJourney, viewState.SelectedJourneyLeg);
						string modeText = getMode(publicJourney, viewState.SelectedJourneyLeg);

						DisplayMap.AlternateText = string.Format(altText, legLocationsText);
						labelHeader.Text = modeText;
						modeImage.ImageUrl = GetResource(getImageResourceKey(publicJourney.Details[viewState.SelectedJourneyLeg].Mode));
						modeImage.AlternateText = modeText;
						labelLegLocations.Text = legLocationsText;

						operationStartedDateTime = DateTime.Now;
						Map.ZoomToEnvelope(minEasting,minNorthing, maxEasting, maxNorthing);
						DisplayMap.ImageUrl = Map.ImageUrl;

					}
					else //Journey is a road journey
					{
						JourneyControl.RoadJourney roadJourney = (JourneyControl.RoadJourney) journey;

						//Expand the routes for use
						SqlHelper sqlHelper = new SqlHelper();
						sqlHelper.ConnOpen(SqlHelperDatabase.EsriDB);
						Hashtable htParameters = new Hashtable(2);
						htParameters.Add("@SessionID", Session.SessionID);
						htParameters.Add("@RouteNum", journey.RouteNum);
						sqlHelper.Execute("usp_ExpandRoutes", htParameters);

						// Add route to map
						Map.AddRoadRoute(Session.SessionID, roadJourney.RouteNum);
						
						string altText = GetResource("DetailedLegMap.DisplayMap.AlternateText");
							
						string legLocationsText = getRoadLocationsText(journeyRequest);
						string modeText = GetResource("TransportMode.Car");

						DisplayMap.AlternateText = string.Format(altText, legLocationsText);
						labelHeader.Text = modeText;
						modeImage.ImageUrl = GetResource("JourneyDetailsControl.imageCarUrl");
						modeImage.AlternateText = modeText;
						labelLegLocations.Text = legLocationsText;

						operationStartedDateTime = DateTime.Now;
						Map.ZoomRoadRoute(Session.SessionID, roadJourney.RouteNum);
						DisplayMap.ImageUrl = Map.ImageUrl;

					}

				}
				catch (PropertiesNotSetException pnse)
				{
					// Log the exception
					OperationalEvent operationalEvent = new OperationalEvent
						(TDEventCategory.ThirdParty, TDTraceLevel.Error, "ESRI Map Exception." + pnse.Message);

					Logger.Write(operationalEvent);
				}
				catch (MapNotStartedException mnse)
				{
					// Log the exception
					OperationalEvent operationalEvent = new OperationalEvent
						(TDEventCategory.ThirdParty, TDTraceLevel.Error, "ESRI Map Exception." + mnse.Message);

					Logger.Write(operationalEvent);
				}
				catch (ScaleZeroOrNegativeException szone)
				{
					// Log the exception
					OperationalEvent operationalEvent = new OperationalEvent
						(TDEventCategory.ThirdParty, TDTraceLevel.Error, "ESRI Map Exception." + szone.Message);

					Logger.Write(operationalEvent);
				}
				catch (ScaleOutOfRangeException soore)
				{
					// Log the exception
					OperationalEvent operationalEvent = new OperationalEvent
						(TDEventCategory.ThirdParty, TDTraceLevel.Error, "ESRI Map Exception." + soore.Message);

					Logger.Write(operationalEvent);
				}
				catch (RouteInvalidException rie)
				{
					// Log the exception
					OperationalEvent operationalEvent = new OperationalEvent
						(TDEventCategory.ThirdParty, TDTraceLevel.Error, "ESRI Map Exception." + rie.Message);
				
					Logger.Write(operationalEvent);
				}
				catch (NoPreviousExtentException npee)
				{
					// Log the exception
					OperationalEvent operationalEvent = new OperationalEvent
						(TDEventCategory.ThirdParty, TDTraceLevel.Error, "ESRI Map Exception." + npee.Message);

					Logger.Write(operationalEvent);
				}
				catch (MapExceptionGeneral mge)
				{
					// Log the exception
					OperationalEvent operationalEvent = new OperationalEvent
						(TDEventCategory.ThirdParty, TDTraceLevel.Error, "ESRI Map Exception." + mge.Message);
				}
			}

            //Added for white labelling:
            //(Note that this code might not work. However, it
            //is believed that this page is no longer used.)
            ConfigureLeftMenu("DetailedLegMap.clientLink.BookmarkTitle", "DetailedLegMap.clientLink.LinkText", clientLink, expandableMenuControl, TransportDirect.UserPortal.SuggestionLinkService.Context.CONTEXT1);
		}

		#endregion

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.buttonBack.Click += new EventHandler(this.buttonBack_Click);
		}

		#endregion

		#region Event handling

		/// <summary>
		/// Event handler for map changed event.
		/// </summary>
		private void Map_Changed(object sender, MapChangedEventArgs e)
		{
			int scale = e.MapScale;
			MapLogging.Write(MapEventCommandCategory.MapInitialDisplay, scale, operationStartedDateTime);
		}
		
		/// <summary>
		/// Event handler for the back button.
		/// </summary>
		private void buttonBack_Click(object sender, EventArgs e)
		{
			TransitionEvent te = TransitionEvent.GoJourneyDetails;
			if( TDSessionManager.Current.JourneyViewState.CallingPageID == PageId.JourneyAdjust )
			{
				te = TransitionEvent.GoJourneyAdjust;
			}
			TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = te;
		}
		#endregion

		#region Helper methods
        /// <summary>
        /// Returns formatted string stating leg mode type e.g. Rail, Bus etc.
        /// </summary>
        /// <param name="publicJourney">Public journey holding leg details</param>
        /// <param name="legIndex">leg from which to extract mode type</param>
        /// <returns>Formatted string stating leg mode type e.g. Rail, Bus etc.</returns>
        private string getMode(JourneyControl.PublicJourney publicJourney, int legIndex)
		{
			string resourceManagerKey = "TransportMode." +
				publicJourney.Details[legIndex].Mode.ToString();
			
			return GetResource(resourceManagerKey);
		}

        /// <summary>
        /// Returns formatted string stating start and end locations
        /// </summary>
        /// <param name="publicJourney">Public journey holding leg details</param>
        /// <param name="legIndex">leg from which to extract locations</param>
        /// <returns>Formatted string stating start and end locations</returns>
        private string getLegLocationsText(JourneyControl.PublicJourney publicJourney, int legIndex)
        {
            string originText = publicJourney.Details[legIndex].LegStart.Location.Description;
            string destinationText = publicJourney.Details[legIndex].LegEnd.Location.Description;
            string toText = GetResource("DetailedLegMap.To");
            return originText + " " + toText + " " + destinationText;
        }

		/// <summary>
		/// Returns formatted string stating start and end locations of road journey
		/// </summary>
		/// <param name="request">Journey request</param>
		/// <returns>Formatted string stating start and end locations</returns>
		private string getRoadLocationsText(ITDJourneyRequest request)
		{
			string originText = outward ? request.OriginLocation.Description
										: request.ReturnOriginLocation.Description;
			string destinationText = outward ? request.DestinationLocation.Description
										: request.ReturnDestinationLocation.Description;

			string toText = GetResource("DetailedLegMap.To");

			return originText + " " + toText + " " + destinationText;
		}

		/// <summary>
		/// Returns the key to lookup resourcing manager with.
		/// </summary>
		/// <param name="mode">Mode to get key for.</param>
		/// <returns>Key</returns>
		private string getImageResourceKey(ModeType mode)
		{
			switch( mode )
			{
				case ModeType.Car:
					return "JourneyDetailsControl.imageCarUrl";
				case ModeType.Air:
					return "JourneyDetailsControl.imageAirUrl";
				case ModeType.Bus:
					return "JourneyDetailsControl.imageBusUrl";
				case ModeType.Coach:
					return "JourneyDetailsControl.imageCoachUrl";
				case ModeType.Cycle:
					return "JourneyDetailsControl.imageCycleUrl";
				case ModeType.Drt:
					return "JourneyDetailsControl.imageDrtUrl";
				case ModeType.Ferry:
					return "JourneyDetailsControl.imageFerryUrl";
				case ModeType.Metro:
					return "JourneyDetailsControl.imageMetroUrl";
				case ModeType.Rail:
					return "JourneyDetailsControl.imageRailUrl";
				case ModeType.Taxi:
					return "JourneyDetailsControl.imageTaxiUrl";
                case ModeType.Telecabine:
                    return "JourneyDetailsControl.imageTelecabineUrl";
				case ModeType.Tram:
					return "JourneyDetailsControl.imageTramUrl";
				case ModeType.Underground:
					return "JourneyDetailsControl.imageUndergroundUrl";
                case ModeType.RailReplacementBus:
					return "JourneyDetailsControl.imageRailReplacementBusUrl";
				case ModeType.Walk:
					return "JourneyDetailsControl.imageWalkUrl";
                default:
					return string.Empty;
			}
		}

		#endregion
	}
}
