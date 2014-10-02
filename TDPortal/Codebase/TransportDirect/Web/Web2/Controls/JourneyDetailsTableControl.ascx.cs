// *********************************************** 
// NAME                 : JourneyDetailsTableControl.ascx.cs 
// AUTHOR               : Kenny Cheung
// DATE CREATED         : 03/12/2003
// DESCRIPTION          : A custom control to display
// one or more journeys within JourneyDetailsTableGridControls
// ************************************************
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/JourneyDetailsTableControl.ascx.cs-arc  $
//
//   Rev 1.7   Oct 26 2010 14:30:22   apatel
//Updated to provide additional information to CJP users
//Resolution for 5623: Additional information available to CJP users
//
//   Rev 1.6   Jul 19 2010 11:49:06   mmodi
//Added a div around the table grid to allow margin to be added when there are multiple journeys
//Resolution for 4884: Default view of extended journey does not look very good
//
//   Rev 1.5   Feb 16 2010 11:15:52   apatel
//International Planner code changes
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.4   Nov 16 2009 15:28:52   mmodi
//Updated for new mapping enhancements
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.3   Jan 06 2009 11:09:58   apatel
//XHTML Compliance Changes
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.2   Mar 31 2008 13:21:20   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:15:20   mturner
//Initial revision.
//
//   Rev 1.27   Oct 06 2006 14:41:42   mturner
//Merge for Stream SCR-4143
//
//   Rev 1.26.1.0   Sep 27 2006 11:08:38   esevern
//Added setting of array of unique car park references for use in JourneyDetailsTableGridControl and LegInstructionsControl, so that car parking opening time label will be displayed only once for each occurence of a car park in the journey results
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//Resolution for 4164: Car Parking: Car park note is not displayed in Details view
//
//   Rev 1.26   Apr 21 2006 10:17:58   mdambrine
//When a car journey is selected then we need to display the entire map hence this fix
//Resolution for 3965: DN068: Maps from table view show 1st direction only of car journey
//
//   Rev 1.25   Mar 20 2006 18:07:30   pcross
//Code review updates
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.24   Mar 13 2006 16:56:30   pcross
//Manual merge for stream3353
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.22.3.0   Feb 13 2006 17:52:26   rhopkins
//Changes to implement use of JourneyLeg as the source.
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.22   Aug 19 2005 14:07:30   jgeorge
//Automatically merged from branch for stream2572
//
//   Rev 1.21.1.1   Aug 16 2005 15:12:32   RPhilpott
//FxCop fixes.
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.21.1.0   Jul 28 2005 16:10:56   RPhilpott
//Service Details changes.
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.20   Jul 28 2005 16:04:48   RPhilpott
//Service Details changes. 
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.19   Apr 28 2005 16:28:58   pcross
//IR2369.
//Resolution for 2369: View map leg from details table on journey details page fails for extended journey
//
//   Rev 1.18   Apr 26 2005 13:18:56   pcross
//IR2192. Corrections to extended journey handling
//
//   Rev 1.17   Apr 22 2005 16:04:52   pcross
//IR2192. Now consumes and raises event when map button is pressed on hosted control
//Resolution for 2192: Display normal map page, add numbered start and end legs
//
//   Rev 1.16   Sep 17 2004 15:13:48   COwczarek
//Removal of unadjusted road journey planning
//Resolution for 1564: Remove unadjusted route functionality
//
//   Rev 1.15   Sep 06 2004 12:14:16   jgeorge
//Added code to private Initialise method to make sure belongingPageId is always set to the Id of the containing page if not specified by a call to one of the public Initialise methods.
//Resolution for 1520: Find a flight airport information formatting (QA) plus a crash
//
//   Rev 1.14   Jul 12 2004 15:09:18   jbroome
//Actioned Extend Journey code review comments.
//
//   Rev 1.13   Jun 17 2004 12:57:08   JHaydock
//Minor corrections - Return order display and Map button click
//
//   Rev 1.12   Jun 16 2004 20:23:54   JHaydock
//Update for JourneyDetailsTableControl to use ItineraryManager
//
//   Rev 1.11   Jun 09 2004 10:36:26   jmorrissey
//Added override of Initialise method, so that control can be initialised without a public journey. This will be used when the itinerary needs to be displayed.
//
//   Rev 1.10   Jun 08 2004 18:24:24   jmorrissey
//Removed redundant code
//
//   Rev 1.9   May 28 2004 16:47:22   jmorrissey
//Added logic and amended html so that Checkin and Exit  columns are shownonly when there there are air mode journey legs
//
//   Rev 1.8   May 20 2004 15:42:38   jmorrissey
//Control is still being developed but does allow integration testing to take place.
//
//   Rev 1.7   Apr 02 2004 17:06:40   AWindley
//DEL 5.2 QA Changes: Resolution for 692
//
//   Rev 1.6   Feb 19 2004 17:21:58   COwczarek
//Changes for new DEL 5.2 display format, including display of frequency based legs
//Resolution for 629: Frequency based Journeys
//
//   Rev 1.5   Dec 09 2003 10:12:54   kcheung
//Walk to string added for JourneyDetailsTable control - Del 5.1 update.

using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using TransportDirect.Common;
using TransportDirect.Web.Support;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.Resource;


namespace TransportDirect.UserPortal.Web.Controls
{
	/// <summary>
	/// User control to display details of a public journey in a tabular format.
	/// </summary>
	public partial  class JourneyDetailsTableControl : TDPrintableUserControl
    {
        #region Private members

        protected JourneyDetailsTableGridControl[] detailsControl = new JourneyDetailsTableGridControl[0];
		private JourneyControl.Journey[] journeys;
		private bool outward;
		private PageId belongingPageId = PageId.Empty;
		private ArrayList uniqueCarParkRefs;

        // Variables needed to add javascript to the map button click
        private bool addMapJavascript = false;
        private string mapId = "map";
        private string mapJourneyDisplayDetailsDropDownId = "mapdropdown";
        private string scrollToControlId = "mapControl";
        private string sessionId = "session";

        private FindAMode findAMode = FindAMode.None;

        #endregion

        #region Public Events

        // Event to fire to pass through that the map button has been pressed on segment control
		public event MapButtonClickEventHandler MapButtonClicked;
		
		#endregion

		#region Event Handlers

		/// <summary>
		/// Event Handler for the map button (pressed on JourneyDetailsTableGridControl)
		/// </summary>
		private void JourneyDetailsTableGridControl_MapButtonClicked(object sender, MapButtonClickEventArgs e)
		{
			// Find index of journey and adjust leg index accordingly
			JourneyDetailsTableGridControl selectedSegment = sender as JourneyDetailsTableGridControl;
			
			int offset = 0;

			foreach (RepeaterItem currentItem in detailsRepeater.Items)
			{
				JourneyDetailsTableGridControl currentSegment = (JourneyDetailsTableGridControl)currentItem.FindControl("detailsPublicControl");
				if (currentSegment.Equals(selectedSegment))
					break;
				else
				{
					// This could be a road journey or a public journey. If it is a road journey then the offset
					// is 1 as there is only effectively 1 leg
					JourneyControl.PublicJourney publicJourney = (currentSegment.Journey as JourneyControl.PublicJourney);
					if (publicJourney != null)
					{
						offset += publicJourney.Details.Length;
					}
					else
					{
						offset++;
					}
				}
			}

			// Refine details fix, if a private journey is the only segment in the list we need to deduct the offset 
			// to make the map show the entire journey instead of the first entry in the list
			if (detailsRepeater.Items.Count == 1 && (selectedSegment.Journey as JourneyControl.PublicJourney) == null)
			{
				offset--;
			}

			// Raise event so the click can be handled in the journey details form and the map displayed
			// as part of that form
			MapButtonClickEventHandler eventHandler = MapButtonClicked;
			if (eventHandler != null)
				eventHandler(this, new MapButtonClickEventArgs(e.LegIndex + offset));
		}

		#endregion

        #region Page_Load, Page_PreRender

        /// <summary>
		/// Page Load method - initialises this control.
		/// </summary>
		protected void Page_Load(object sender, System.EventArgs e)
		{
		}

		/// <summary>
		/// OnPreRender method.
		/// </summary>
		/// <param name="e"></param>
		protected override void OnPreRender(EventArgs e)
		{	
			base.OnPreRender(e);
        }

        #endregion

        #region Initialise

        /// <summary>
		/// Initialisation method used to display a specific journey
		/// </summary>
		/// <param name="thePublicJourney">Public Journey to render details for</param>
		/// <param name="outward">Indicates if rendering for outward or return</param>
		public void Initialise(JourneyControl.Journey journey, bool outward, FindAMode findAMode)
		{
			this.journeys = new JourneyControl.Journey[] {journey};
			this.outward = outward;
            this.findAMode = findAMode;

			Initialise();
		}

		/// <summary>
		/// Initialisation method used when an itinerary should be displayed
		/// </summary>
		/// <param name="outward">Indicates if rendering for outward or return</param>
        public void Initialise(bool outward, FindAMode findAMode)
		{
			this.journeys = null;
			this.outward = outward;
            this.findAMode = findAMode;

			Initialise();
        }

        /// <summary>
        /// Method which sets the values needed to add map javascript to the map buttons
        /// </summary>
        /// <param name="mapId">The id of the map to zoom in</param>
        /// <param name="scrollToControlId">The id of the control page should scroll to</param>
        /// <param name="sessionId">The session id to use when zooming to the journey on the map</param>
        public void SetMapProperties(string mapId, string mapJourneyDisplayDetailsDropDownId,
            string scrollToControlId, string sessionId)
        {
            this.mapId = mapId;
            this.mapJourneyDisplayDetailsDropDownId = mapJourneyDisplayDetailsDropDownId;
            this.scrollToControlId = scrollToControlId;
            this.sessionId = sessionId;
        }

        #endregion

        #region Private methods

        /// <summary>
		/// Initialises the control
		/// </summary>
		private void Initialise()
		{
			uniqueCarParkRefs = new ArrayList();
			
			if (belongingPageId == PageId.Empty)
			{
				belongingPageId = this.PageId;
			}

            // Set the map buttons addJavascript flag from session
            TDJourneyViewState viewState = TDItineraryManager.Current.JourneyViewState;
            addMapJavascript = outward ? viewState.OutwardShowMap : viewState.ReturnShowMap;

			if (journeys == null)
			{

				TDItineraryManager itineraryManager = TDSessionManager.Current.ItineraryManager;

				if (itineraryManager.FullItinerarySelected) //Display all itinerary journeys
				{
					int segmentIndex;
					int segmentIncrement;

					// Obtain appropriate segments from ItineraryManager
					if (outward)
					{
						journeys = itineraryManager.OutwardJourneyItinerary;
						segmentIndex = 0;
						segmentIncrement = 1;
					}
					else
					{
						journeys = itineraryManager.ReturnJourneyItinerary;
						segmentIndex = itineraryManager.Length - 1;
						segmentIncrement = -1;
					}

					//Bind repeater so that we can get embedded controls
					detailsRepeater.DataSource = new object[journeys.Length];
					detailsRepeater.DataBind();

					detailsControl = new JourneyDetailsTableGridControl[journeys.Length];
					for (int i = 0; i < journeys.Length; i++) //Loop through itinerary journeys
					{
						//Find next JourneyDetailsTableGridControl
						JourneyDetailsTableGridControl detailsPublicControl = (JourneyDetailsTableGridControl)detailsRepeater.Items[i].FindControl("detailsPublicControl");
                        Panel panelDetailsTableGrid = (Panel)detailsRepeater.Items[i].FindControl("panelDetailsTableGrid");

						if (detailsPublicControl != null)
						{
                            // Display the panel the details table is in, and add margin below to sepearate
                            // the different journey details tables
                            if (panelDetailsTableGrid != null)
                            {
                                panelDetailsTableGrid.Visible = true;
                                if ((journeys.Length > 1) && (i < (journeys.Length - 1)))
                                {
                                    panelDetailsTableGrid.CssClass = "jdtgriddiv";
                                }
                            }

							//Initialise the control and store the control in the array for later access
                            detailsPublicControl.Initialise(journeys[i], outward, belongingPageId, segmentIndex, findAMode, TDSessionManager.Current.JourneyRequest);
                            detailsPublicControl.SetMapProperties(addMapJavascript, mapId, mapJourneyDisplayDetailsDropDownId, scrollToControlId, sessionId);
							detailsPublicControl.PrinterFriendly = this.PrinterFriendly;
							detailsPublicControl.Visible = true;
							detailsControl[i] = detailsPublicControl;

							if(i<1)
							{
								detailsPublicControl.UseNameList = false;
							}
							else
							{
								detailsPublicControl.UseNameList = true;
								JourneyLeg[] legs = journeys[i].JourneyLegs;
								
								for(int j=0; j<legs.Length; j++) 
								{
									JourneyLeg leg = legs[j];
									if(leg.LegStart.Location.CarParking != null)
									{
										string carRef = leg.LegStart.Location.CarParking.CarParkReference;
										if(!uniqueCarParkRefs.Contains(carRef))
										{
											uniqueCarParkRefs.Add(carRef);
										}
									}
									if(leg.LegEnd.Location.CarParking != null)
									{
										string carRef = leg.LegEnd.Location.CarParking.CarParkReference;
										if(!uniqueCarParkRefs.Contains(carRef))
										{
											uniqueCarParkRefs.Add(carRef);
										}
									}
								}
								detailsPublicControl.UniqueNameList = uniqueCarParkRefs;
							}
						}

						segmentIndex = segmentIndex + segmentIncrement;
					}
				}
				else //Display only selected journey
				{
					//Bind repeater so that we can get embedded controls
					detailsRepeater.DataSource = new object[1];
					detailsRepeater.DataBind();

					detailsControl = new JourneyDetailsTableGridControl[1];
					//Find JourneyDetailsTableGridControl
					JourneyDetailsTableGridControl detailsPublicControl = (JourneyDetailsTableGridControl)detailsRepeater.Items[0].FindControl("detailsPublicControl");
                    Panel panelDetailsTableGrid = (Panel)detailsRepeater.Items[0].FindControl("panelDetailsTableGrid");

					if (detailsPublicControl != null)
					{
                        if (panelDetailsTableGrid != null)
                        {
                            // Display the panel the details table is in
                            panelDetailsTableGrid.Visible = true;
                        }

						ITDJourneyResult journeyResult = itineraryManager.JourneyResult;
						TDJourneyViewState journeyViewState = itineraryManager.JourneyViewState;
						ITDJourneyRequest journeyRequest = itineraryManager.JourneyRequest;

						//Initialise the control and store the control in the array for later access
						detailsControl[0] = InitialiseControl(
							detailsPublicControl, journeyResult, journeyViewState, journeyRequest, -1);

						detailsPublicControl.UseNameList = false;
					}
				}
			}
			else //Display journey provided
			{
				//Bind repeater so that we can get embedded controls
				detailsRepeater.DataSource = new object[1];
				detailsRepeater.DataBind();

				detailsControl = new JourneyDetailsTableGridControl[1];
				//Find JourneyDetailsTableGridControl
				JourneyDetailsTableGridControl detailsPublicControl = (JourneyDetailsTableGridControl)detailsRepeater.Items[0].FindControl("detailsPublicControl");
                Panel panelDetailsTableGrid = (Panel)detailsRepeater.Items[0].FindControl("panelDetailsTableGrid");

				if (detailsPublicControl != null)
				{
                    if (panelDetailsTableGrid != null)
                    {
                        // Display the panel the details table is in
                        panelDetailsTableGrid.Visible = true;
                    }

					//Initialise the control and store the control in the array for later access
                    detailsPublicControl.Initialise(journeys[0], outward, belongingPageId, -1, findAMode, TDSessionManager.Current.JourneyRequest);
                    detailsPublicControl.SetMapProperties(addMapJavascript, mapId, mapJourneyDisplayDetailsDropDownId, scrollToControlId, sessionId);
					detailsPublicControl.PrinterFriendly = this.PrinterFriendly;
					detailsPublicControl.Visible = true;
					detailsControl[0] = detailsPublicControl;
				}
			}
		}

		/// <summary>
		/// Initialises the JourneyDetailsTableGridControl using the parameters provided
		/// </summary>
		/// <param name="detailsPublicControl">The JourneyDetailsTableGridControl being displayed</param>
		/// <param name="journeyResult">The related journey result</param>
		/// <param name="journeyViewState">The related journey view state</param>
		/// <param name="journeyRequest">The related journey request</param>
		/// <param name="itinerarySegment">The itinerary segment
		/// that the journey relates to - use -1 for unspecified</param>
		private JourneyDetailsTableGridControl InitialiseControl(
			JourneyDetailsTableGridControl detailsPublicControl,
			ITDJourneyResult journeyResult,
			TDJourneyViewState journeyViewState,
			ITDJourneyRequest journeyRequest,
			int itinerarySegment)
		{
			JourneyControl.PublicJourney publicJourney = null;
			JourneyControl.RoadJourney roadJourney = null;

			if (outward)
			{
				//Try to get the outward public journey
				publicJourney = journeyResult.OutwardPublicJourney(journeyViewState.SelectedOutwardJourneyID);

				if (publicJourney == null)
				{
					//No public journey, so try to get the outward road journey
					roadJourney = journeyResult.OutwardRoadJourney();

					//If there is a road journey, then initialise and display
					//the control with the journey, else display nothing
					if (roadJourney != null)
					{
                        detailsPublicControl.Initialise(roadJourney, outward, belongingPageId, itinerarySegment, findAMode, TDSessionManager.Current.JourneyRequest);
                        detailsPublicControl.SetMapProperties(addMapJavascript, mapId, mapJourneyDisplayDetailsDropDownId, scrollToControlId, sessionId);
						detailsPublicControl.PrinterFriendly = this.PrinterFriendly;
						detailsPublicControl.Visible = true;
						return detailsPublicControl;
					}
				}
				else //Valid public journey
				{
					//Initialise and display the control with the public journey
                    detailsPublicControl.Initialise(publicJourney, outward, belongingPageId, itinerarySegment, findAMode, TDSessionManager.Current.JourneyRequest);
                    detailsPublicControl.SetMapProperties(addMapJavascript, mapId, mapJourneyDisplayDetailsDropDownId, scrollToControlId, sessionId);
					detailsPublicControl.PrinterFriendly = this.PrinterFriendly;
					detailsPublicControl.Visible = true;
					return detailsPublicControl;
				}
			}
			else
			{
				//Try to get the return public journey
				publicJourney = journeyResult.ReturnPublicJourney(journeyViewState.SelectedReturnJourneyID);

				if (publicJourney == null)
				{
					//No public journey, so try to get the return road journey
					roadJourney = journeyResult.ReturnRoadJourney();

					//If there is a road journey, then initialise and display
					//the control with the journey, else display nothing
					if (roadJourney != null)
					{
                        detailsPublicControl.Initialise(roadJourney, outward, belongingPageId, itinerarySegment, findAMode, TDSessionManager.Current.JourneyRequest);
                        detailsPublicControl.SetMapProperties(addMapJavascript, mapId, mapJourneyDisplayDetailsDropDownId, scrollToControlId, sessionId);
						detailsPublicControl.PrinterFriendly = this.PrinterFriendly;
						detailsPublicControl.Visible = true;
						return detailsPublicControl;
					}
				}
				else //Valid public journey
				{
					//Initialise and display the control with the public journey
                    detailsPublicControl.Initialise(publicJourney, outward, belongingPageId, itinerarySegment, findAMode, TDSessionManager.Current.JourneyRequest);
                    detailsPublicControl.SetMapProperties(addMapJavascript, mapId, mapJourneyDisplayDetailsDropDownId, scrollToControlId, sessionId);
					detailsPublicControl.PrinterFriendly = this.PrinterFriendly;
					detailsPublicControl.Visible = true;
					return detailsPublicControl;
				}
			}

			return null;
		}

		/// <summary>
		/// This method is called by the ItemDataBound event handler of the detailsRepeater.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void detailsRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			JourneyDetailsTableGridControl jdtc = e.Item.FindControl("detailsPublicControl") as JourneyDetailsTableGridControl;
			
			if	(jdtc != null)
			{
				jdtc.MapButtonClicked += new MapButtonClickEventHandler(this.JourneyDetailsTableGridControl_MapButtonClicked);
			}

        }

        #endregion

        #region Public properties

        /// <summary>
		/// Get/Set property.
		/// The page Id of the page that contains this control
		/// This property must be set before calling initialise and is only provided for backwards compatability
		/// </summary>
		public PageId MyPageId
		{
			get
			{
				return belongingPageId;
			}
			set
			{
				belongingPageId = value;
			}
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
        
        ///     Required method for Designer support - do not modify
        ///     the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.detailsRepeater.ItemDataBound += new System.Web.UI.WebControls.RepeaterItemEventHandler(this.detailsRepeater_ItemDataBound);
		}
        #endregion
    }
}

