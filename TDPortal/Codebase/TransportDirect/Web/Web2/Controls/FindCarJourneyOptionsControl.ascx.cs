// *********************************************** 
// NAME                 : FindCarJourneyOptionsControl.ascx
// AUTHOR               : Reza Bamshad
// DATE CREATED         : 18/01/2005 
// DESCRIPTION          : Displays /allows input of 
//                        car journey options preferences.                           .
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/FindCarJourneyOptionsControl.ascx.cs-arc  $   
//
//   Rev 1.6   Aug 28 2012 10:20:50   mmodi
//Added LocationSuggest functionality for journey planners (d2d, cycle, car)
//Resolution for 5832: CCN Gaz
//
//   Rev 1.5   Mar 14 2011 15:12:04   rphilpott
//Add support for Limited Access changes in CJP del 11.0
//
//   Rev 1.4   Oct 27 2010 10:38:56   rbroddle
//Removed explicit wire up to Page_PreRender as AutoEventWireUp=true for this control so it was firing twice.
//Resolution for 5621: USD8048975 - page_init and other events wired up more than once on some pages and controls
//
//   Rev 1.3   Jun 26 2008 14:04:10   apatel
//CCN 464 Accessibility related changes
//Resolution for 5019: CCN0458 - Accessability Updates improved linking
//
//   Rev 1.2   Mar 31 2008 13:20:24   mturner
//Drop3 from Dev Factory
//
//  Rev Devfactory Jan 30 2008 08:32:00 dsawe
//  White Labeling - modified to add PageOptionsControl.
//
//   Rev 1.0   Nov 08 2007 13:13:58   mturner
//Initial revision.
//
//   Rev 1.23   Apr 26 2006 15:34:12   asinclair
//Added ClearRoads() to move this code from the JPInput page to the control so that it can be reused on other pages.
//Resolution for 3974: DN068 Extend: 'Clear page' does not clear all values on Extend input page
//
//   Rev 1.22   Feb 23 2006 19:16:34   build
//Automatically merged from branch for stream3129
//
//   Rev 1.21.1.0   Jan 10 2006 15:24:28   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.21   Nov 04 2005 11:35:02   NMoorhouse
//Manual merge of stream2816
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages & HTML buttons
//
//   Rev 1.20   Nov 02 2005 14:45:48   jgeorge
//Changed to alter control type instead of creating new type object.
//Resolution for 2935: Del 7.2: Expanding Train Preferences prevents user planning journey
//
//   Rev 1.19.1.0   Oct 25 2005 20:06:38   RGriffith
//TD089 ES020 Image Button Replacement
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages & HTML buttons
//
//   Rev 1.19   May 19 2005 12:11:48   ralavi
//FXCop modifications and fixing problems so that when a value in journey option section is changed and submitted, title "Car journey Details" is displayed. See IR #2523. 
//
//   Rev 1.18   May 06 2005 17:51:46   Ralavi
//Placing comma between  resolved avoidRoads and useRoads and reducing the gap between label and road names. 
//
//   Rev 1.17   May 04 2005 15:21:26   Ralavi
//Placing commas between tolls, Motorways and ferries and solving other cosmetic issues relating to avoid/use Roads. See IR2418.
//
//   Rev 1.16   Apr 29 2005 17:46:54   Ralavi
//Enhancement on functionality of avoidRoads and useRoads. These enhancements had to be made after  removing two rows in WriteToSession Method of journeyPlannerInput to fix displaying of useRoad favourite journeys
//
//   Rev 1.15   Apr 27 2005 10:20:32   COwczarek
//Fix compiler warnings
//
//   Rev 1.14   Apr 26 2005 09:13:22   Ralavi
//Fixed the problems with AvoidRoad and UseRoads as the result of IRs 2011 and 2246
//
//   Rev 1.13   Apr 13 2005 12:25:52   Ralavi
//Added screen reader labels.
//
//   Rev 1.12   Apr 05 2005 10:44:12   Ralavi
//To ensure correct avoid and use roads are displayed and validated
//
//   Rev 1.11   Mar 30 2005 19:15:32   RAlavi
//Modifications for car costing
//
//   Rev 1.10   Mar 30 2005 17:13:54   RAlavi
//Issues with avoidRoad and useRoad when in ambiguity mode
//
//   Rev 1.9   Mar 23 2005 11:16:50   RAlavi
//Added modifications to allow  removal of avoid road use road when clear button is pressed
//
//   Rev 1.8   Mar 18 2005 10:06:20   RAlavi
//Modified problems with avoid roads and use roads
//
//   Rev 1.7   Mar 15 2005 11:44:02   RAlavi
//Car costing modifications
//
//   Rev 1.5   Mar 04 2005 16:28:22   RAlavi
//Modifications to resolve car costing problems
//
//   Rev 1.4   Mar 02 2005 15:24:26   RAlavi
//changes for ambiguity
//
//   Rev 1.3   Feb 23 2005 16:31:54   RAlavi
//Changed for car costing
//
//   Rev 1.2   Feb 21 2005 14:08:14   esevern
//changed use/avoid roads list from arraylist to TDRoad array
//
//   Rev 1.1   Feb 18 2005 17:03:48   esevern
//Car costing - interim working copy checked in for code integration
//
//   Rev 1.0   Jan 28 2005 11:56:24   ralavi
//Initial revision.

namespace TransportDirect.UserPortal.Web.Controls
{
	using System;using TransportDirect.Common.ResourceManager;
	using System.Collections;
    using System.Text;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using TransportDirect.Web.Support;
	using TransportDirect.UserPortal.DataServices;
	using TransportDirect.Common.ServiceDiscovery;
	using TransportDirect.JourneyPlanning.CJPInterface;
	using TransportDirect.UserPortal.SessionManager;
	using TransportDirect.UserPortal.LocationService;
	using TransportDirect.Common;

	/// <summary>
	///		Summary description for FindCarJourneyOptionsControl.
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public partial  class FindCarJourneyOptionsControl : TDUserControl
    {
        #region Private members

        #region controls
        protected TristateRoadControl avoidRoadSelectControl1;
		protected TristateRoadControl avoidRoadSelectControl2;
		protected TristateRoadControl avoidRoadSelectControl3;
		protected TristateRoadControl avoidRoadSelectControl4;
		protected TristateRoadControl avoidRoadSelectControl5;
		protected TristateRoadControl avoidRoadSelectControl6;
		protected TristateRoadControl useRoadSelectControl1;
		protected TristateRoadControl useRoadSelectControl2;
		protected TristateRoadControl useRoadSelectControl3;
		protected TristateRoadControl useRoadSelectControl4;
		protected TristateRoadControl useRoadSelectControl5;
		protected TristateRoadControl useRoadSelectControl6;
		protected TriStateLocationControl2 locationTristateControl;
		
		#endregion

		#region private fields

		//private field for holding location type of this control
		private CurrentLocationType locationType;
		//private field for holding station type of this control
		private StationType stationType;
		//private field for holding LocationSearch session information for this control
		private LocationSearch theSearch;
		//private field for holding TDLocation session information for this control
		private TDLocation theLocation;
		//private field for holding LocationSelectControlType session information for this control
		private TDJourneyParameters.LocationSelectControlType locationControlType;

		private TDJourneyParametersMulti journeyParameters;
        
		/// <summary>
		/// Event fired to signal new location button has been clicked
		/// </summary>
		public event EventHandler NewLocation;
		public event EventHandler MapClick;

        #endregion

		#region Constants
		//Keys used to obtain strings from the resource file
		private const string JourneyOptionsKey = "FindCarJourneyOptionsControl.JourneyOptionsHeader";
		private const string AvoidRoadTypesKey = "FindCarJourneyOptionsControl.AvoidRoadTypes";
		private const string TollsKey = "FindCarJourneyOptionsControl.AvoidTolls";
		private const string FerriesKey = "FindCarJourneyOptionsControl.AvoidFerries";
        private const string MotorwaysKey = "FindCarJourneyOptionsControl.AvoidMotorways";
        private const string LimitedAccessKey = "FindCarJourneyOptionsControl.LimitedAccess";
        private const string AvoidRoadsKey = "FindCarJourneyOptionsControl.AvoidRoads";
		private const string UseRoadsKey = "FindCarJourneyOptionsControl.UseRoads";
		private const string NoteKey = "FindCarJourneyOptionsControl.Note";
		private const string DisplayAvoidKey = "FindCarJourneyOptionsControl.AvoidTypeText";
		private const string AvoidRoadsSRKey = "FindCarJourneyOptionsControl.AvoidSRRoads";
		private const string UseRoadsSRKey = "FindCarJourneyOptionsControl.UseSRRoads";
		private const string TxtSeven = "txtseven";
		private const string TxtSevenB = "txtsevenb";
		private const string PreferencesCarHeaderLabelTextKey = "FindPreferencesOptionsControl.PreferencesCarHeaderLabel.{0}";
		#endregion

		//Page level variables
		private GenericDisplayMode journeyOptionDisplayMode; 
		private GenericDisplayMode avoidRoadDisplayMode; 
		private GenericDisplayMode useRoadDisplayMode;
		private GenericDisplayMode viaRoadDisplayMode; 
		
		private TDRoad[] avoidRoadsList = new TDRoad[0]; 
		private TDRoad[] useRoadsList = new TDRoad[0];

        private bool showLocationControlAutoSuggest = false;
        
		private bool newLocationClicked;
		private bool preferencesChanged;

		private TDRoad road = new TDRoad();

		ArrayList avoidRoadList = new ArrayList();
		ArrayList useRoadList = new ArrayList();

        ArrayList avoidTempRoadList = new ArrayList();
        ArrayList useTempRoadList = new ArrayList();

		private string[] arrayAvoid;
		private string[] arrayUse;
		int useIndex;
		char[] CommaChar = {','};

		private static readonly object JourneyOptionsVisibleChangedEventKey = new object();
        private static readonly object AvoidMotorwaysChangedKey = new object();
        private static readonly object BanLimitedAccessChangedKey = new object();
        private static readonly object AvoidTollsChangedKey = new object();
		private static readonly object AvoidFerriesChangedKey = new object();

        #endregion

        #region page init, load, render

        /// <summary>
		/// Handler for the Init event. Sets up global variables and additional event handlers.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Init(object sender, System.EventArgs e)
		{
			//Assign values to page level variables
			locationTristateControl.NewLocation += new EventHandler(OnNewLocation);
			locationTristateControl.MapClick += new EventHandler(OnMapClick);
			
            locationControl.NewLocationClick += new EventHandler(OnNewLocation);
            locationControl.MapLocationClick += new EventHandler(OnMapClick);
		}

		/// <summary>
		/// Setting label text
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			//Load strings from the languages file
			labelJourneyOptions.Text = GetResource(JourneyOptionsKey);
			roadsToAvoidLabel.Text = GetResource(AvoidRoadTypesKey) + " ";
			theseRoadsToAvoidLabel.Text = GetResource(AvoidRoadsKey) + " ";
            displayTheseRoadsLabel.Text = GetResource(AvoidRoadsKey) + ":" + " ";
			theseRoadsToUseLabel.Text = GetResource(UseRoadsKey);
			avoidRoadsSRLabel.Text = GetResource(AvoidRoadsSRKey);
			useRoadsSRLabel.Text = GetResource(UseRoadsSRKey);
            displayUseTheseRoadsLabel.Text = GetResource(UseRoadsKey) + ":" + " ";
			
			avoidTollsCheckBox.Text = GetResource(TollsKey);
			avoidFerriesCheckBox.Text = GetResource(FerriesKey);
            avoidMotorwayCheckBox.Text = GetResource(MotorwaysKey);
            banLimitedAccessCheckBox.Text = GetResource(LimitedAccessKey);
            note1Label.Text = GetResource(NoteKey);
			note2Label.Text = GetResource(NoteKey);
			
			//set up resource strings on initial page load
			SetUpResources();
			//do not refresh the tristate location control
		
			//refresh the tristate location control
			if (PageId != PageId.JourneyPlannerAmbiguity)
			{
                RefreshTristateControl(IsPostBack);
			}

            //This method populates AvoidRoad and UseRoad fields with the values entered.  

            PopulateRoads();
			
		}

        /// <summary>
		/// Sets visibility of controls according to the property values
		/// </summary>
		/// <param name="e"></param>
		protected void Page_PreRender(object sender, System.EventArgs e)
		{
			SetUpResources();
			UpdateControls();

            if (Page.IsPostBack)
            {
                // newLocationClicked indicates if there was a new location event triggered
                // in which case don't check the input
                if (!showLocationControlAutoSuggest)
                {
                    RefreshTristateControl(theSearch.InputText.Length != 0);
                }
             
                //If anything is changed in the ambiguity page of the door to door input page, pass
                //the value to journeyParameters
                if (PageId == PageId.JourneyPlannerAmbiguity)
                {
					for (int i = 0; i < AvoidRoadsList.Length; i++)
					{	//Remove comma before passing the value to journey parameter
						avoidRoadsList[i].RoadName = avoidRoadsList[i].RoadName.TrimEnd(CommaChar);
					}
					for (int i = 0; i < UseRoadsList.Length; i++)
					{	//Remove comma before passing the value to journey parameter
						useRoadsList[i].RoadName = useRoadsList[i].RoadName.TrimEnd(CommaChar);
					}
                    journeyParameters.AvoidRoadsList = avoidRoadsList;
                    journeyParameters.UseRoadsList = useRoadsList;
                }
                if (PageId == PageId.JourneyPlannerInput)
                { 
                    PopulateRoadUnspecified();
                }
            }
            else
            {
                //If on door to door input page, pass in the values from journey parameter 
                if (PageId == PageId.JourneyPlannerInput)
                {
                    avoidRoadsList = journeyParameters.AvoidRoadsList;
                    useRoadsList = journeyParameters.UseRoadsList;
                    PopulateRoadUnspecified();
                }	
               
            }
            //If avoidRoad is ambiguous, display the error message
			if ((avoidRoadSelectControl1.RoadAmbiguous.Visible) ||(avoidRoadSelectControl2.RoadAmbiguous.Visible)
				|| (avoidRoadSelectControl3.RoadAmbiguous.Visible) || (avoidRoadSelectControl4.RoadAmbiguous.Visible)
				|| (avoidRoadSelectControl5.RoadAmbiguous.Visible) || (avoidRoadSelectControl6.RoadAmbiguous.Visible)
				||	(useRoadSelectControl1.RoadAmbiguous.Visible) ||(useRoadSelectControl2.RoadAmbiguous.Visible)
				|| (useRoadSelectControl3.RoadAmbiguous.Visible) || (useRoadSelectControl4.RoadAmbiguous.Visible)
				|| (useRoadSelectControl5.RoadAmbiguous.Visible) || (useRoadSelectControl6.RoadAmbiguous.Visible))
			{
				roadAlertLabel.Visible = true;
				roadAlertLabel.Text = Global.tdResourceManager.GetString(
					"AmbiguousRoadSelectControl.lableRoadAlert",
					TDCultureInfo.CurrentUICulture);
			}
			else
			{
				roadAlertLabel.Visible = false;
			}

            // Page Options not visible when ambiguity page 
            if (AmbiguityMode)
                pageOptionsControl.Visible = false;
            else
                pageOptionsControl.Visible = true;

		}

		#region Private Methods

        //Populates AvoidRoads

		private void PopulateAvoidRoad() 
		{
			
			for (int i = 0; i < avoidRoadsList.Length; i++)
			{
										 
				switch (i)
				{
					
					case 0:
						avoidRoadSelectControl1.RoadSpecified = avoidRoadsList[0].RoadName;
						break;
					case 1:
						avoidRoadSelectControl2.RoadSpecified = avoidRoadsList[1].RoadName;
						break;
					case 2:
						avoidRoadSelectControl3.RoadSpecified = avoidRoadsList[2].RoadName;
						break;
					case 3:
						avoidRoadSelectControl4.RoadSpecified= avoidRoadsList[3].RoadName;
						break;
					case 4:
						avoidRoadSelectControl5.RoadSpecified = avoidRoadsList[4].RoadName;
						break;
					case 5:
						avoidRoadSelectControl6.RoadSpecified = avoidRoadsList[5].RoadName;
						break;
				}
			}

			
		}
        
        //Populates useRoads
		private void PopulateUseRoad() 
		{

			for (int i = 0; i < useRoadsList.Length; i++)
			{
				switch (i)
				{
					case 0:
						useRoadSelectControl1.RoadSpecified = useRoadsList[0].RoadName;
						break;
					case 1:
						useRoadSelectControl2.RoadSpecified = useRoadsList[1].RoadName;
						break;
					case 2:
						useRoadSelectControl3.RoadSpecified = useRoadsList[2].RoadName;
						break;
					case 3:
						useRoadSelectControl4.RoadSpecified = useRoadsList[3].RoadName;
						break;
					case 4:
						useRoadSelectControl5.RoadSpecified = useRoadsList[4].RoadName;
						break;
					case 5:
						useRoadSelectControl6.RoadSpecified = useRoadsList[5].RoadName;
						break;
				}
			}
		
			
		}
        private void PopulateRoads()
        {
            	
            journeyParameters = (TDJourneyParametersMulti)TDSessionManager.Current.JourneyParameters;
            avoidRoadsList = journeyParameters.AvoidRoadsList;
            useRoadsList = journeyParameters.UseRoadsList;
     
					
            //Populate the array with the values entered.
            PopulateRoadArray();
			
            
           
            //Only populates the arrayList if values are entered into the avoidRoad box
            for (int i = 0; i < arrayAvoid.Length; i++)
            {
                if ((arrayAvoid[i] != "") && (arrayAvoid[i] != null))
                {
						
                    TDRoad Road = new TDRoad (arrayAvoid[i]);
                   
					avoidRoadList.Add(Road);					
                    avoidTempRoadList.Add(Road.RoadName.TrimEnd(CommaChar));
                       
                }
            }
				
            //If the arrayList is populated or only one box is populated in the ambiguity page, then continue to populated the avoidRoad boxes 
            //with the latest values. The second condition that checks whether the first box in ambiguity mode is visible, is used
            //because if in journeyPlannerInput page, an invalid road is entered and this is the only road, on submitting the page
            //journeyPlannerAmbiguity page is displayed with the box having a red border. If the user deletes the value from the box and press 
            //submit, journey is planned and the invalid value is cleared. Previously, the previous value was returned.
			
            if ((avoidRoadList.Count > 0)|| (avoidRoadSelectControl1.RoadAmbiguous.Visible))
            {        
                avoidRoadsList = (TDRoad[])avoidRoadList.ToArray(typeof(TDRoad));

                if ((PageId == PageId.JourneyPlannerAmbiguity)|| (PageId == PageId.JourneyPlannerInput))
                {
                    PopulateRoadUnspecified();
                }
                PopulateAvoidRoad();
                
            }
            
            else
            {
                if ((PageId == PageId.JourneyPlannerInput)&& (avoidRoadSelectControl1.RoadUnspecified.Visible))
                {
                    avoidRoadsList = (TDRoad[])avoidRoadList.ToArray(typeof(TDRoad));
                }

                if (avoidRoadsList.Length > 0)
                {
                    PopulateRoadUnspecified();
                    PopulateAvoidRoad();
                }
            }
            
            // If a values is changed in the ambiguity page then pass the value to journey parameter
            if ((PageId == PageId.JourneyPlannerAmbiguity)||(PageId == PageId.FindCarInput))
            {	
				for (int i = 0; i < AvoidRoadsList.Length; i++)
				{	//Remove comma before passing the value to journey parameter
					avoidRoadsList[i].RoadName = avoidRoadsList[i].RoadName.TrimEnd(CommaChar);
				}

				journeyParameters.AvoidRoadsList = avoidRoadsList;
            }
			
            //Only populates the arrayList if values are entered into the useRoad box	
            for (int i = 0; i < arrayUse.Length; i++)
            {

                if ((arrayUse[i] != "") && (arrayUse[i] != null))
                {
						
                    TDRoad Road = new TDRoad (arrayUse[i]);
                    useRoadList.Add(Road);
					
                    useTempRoadList.Add(Road.RoadName.TrimEnd(CommaChar));
                   

                }
            }

            //If the arrayList is populated or only one box is populated in the ambiguity page, then continue to populated the avoidRoad boxes 
            //with the latest values. The second condition that checks whether the first box in ambiguity mode is visible, is used
            //because if in journeyPlannerInput page, an invalid road is entered and this is the only road, on submitting the page
            //journeyPlannerAmbiguity page is displayed with the box having a red border. If the user deletes the value from the box and press 
            //submit, journey is planned and the invalid value is cleared. Previously, the previous value was returned.
            if ((useRoadList.Count > 0)||(useRoadSelectControl1.RoadAmbiguous.Visible))
            {          
                useRoadsList = (TDRoad[])useRoadList.ToArray(typeof(TDRoad));
                
                if ((PageId == PageId.JourneyPlannerAmbiguity)|| (PageId == PageId.JourneyPlannerInput))
                {
                    PopulateRoadUnspecified();
                }
                PopulateUseRoad();
            }
            else
            {
                if ((PageId == PageId.JourneyPlannerInput)&& (useRoadSelectControl1.RoadUnspecified.Visible))
                {
                    useRoadsList = (TDRoad[])useRoadList.ToArray(typeof(TDRoad));
                }
                if (useRoadsList.Length > 0)
                {
                    PopulateRoadUnspecified();
                    PopulateUseRoad();

                }
            }
			// Adding commas between the road names if more than one road resolved
			AddCommaAvoidRoads();
			AddCommaUseRoads();
            // If a values is changed in the ambiguity page then pass the value to journey parameter
            if ((PageId == PageId.JourneyPlannerAmbiguity)||(PageId == PageId.FindCarInput))
            {
				for (int i = 0; i < UseRoadsList.Length; i++)
				{
					// Remove comma before passing the value to journeyParameters
					useRoadsList[i].RoadName = useRoadsList[i].RoadName.TrimEnd(CommaChar);
				}
                journeyParameters.UseRoadsList = useRoadsList;
            }
   
			
            for( int i = 0; i < avoidRoadList.Count; i++)
            {
                
                if (((TDRoad)avoidRoadList[i]).RoadName != "")
                {
                    //Check to see if any of the AvoidRoads are repeated in UseRoad
					
                    useIndex = (useTempRoadList.IndexOf(avoidTempRoadList[i]));
 
                    // if road to avoid is repeated in useRoad, set the boolean property to true.     
                    if(  useIndex != -1 )
                    {	
											
                        switch (useIndex)
                        {
                            case 0:
                                useRoadSelectControl1.IsTextRepeated = true;
								
                                break;
                            case 1:
                                useRoadSelectControl2.IsTextRepeated = true;
								
                                break;
                            case 2:
                                useRoadSelectControl3.IsTextRepeated = true;
								
                                break;
                            case 3:
                                useRoadSelectControl4.IsTextRepeated = true;
								
                                break;
                            case 4:
                                useRoadSelectControl5.IsTextRepeated = true;
								
                                break;
                            case 5:
                                useRoadSelectControl6.IsTextRepeated = true;
								
                                break;
                        }

                        switch (i)
                        {
                            case 0:
                                avoidRoadSelectControl1.IsTextRepeated = true;
                                break;
                            case 1:
                                avoidRoadSelectControl2.IsTextRepeated = true;
                                break;
                            case 2:
                                avoidRoadSelectControl3.IsTextRepeated = true;
                                break;
                            case 3:
                                avoidRoadSelectControl4.IsTextRepeated = true;
                                break;
                            case 4:
                                avoidRoadSelectControl5.IsTextRepeated = true;
                                break;
                            case 5:
                                avoidRoadSelectControl6.IsTextRepeated = true;
                                break;
                        }

							
                    }
                }
				
            }

			
            for( int i = 0; i < avoidRoadList.Count; i++)
            {
                
                if (((TDRoad)avoidRoadList[i]).RoadName != "")
                {
                    //Check to see if any of AvoidRoads are repeated in useRoads
                    useIndex = (useTempRoadList.IndexOf(avoidTempRoadList[i])); 
                    if (road.ValidateRoadName(((TDRoad)avoidRoadList[i]).RoadName))
                    {
                        if (useIndex != -1 )
                        {
						
                            switch (useIndex)
                            {
                                case 0:
                                    useRoadSelectControl1.IsTextRepeated = true;
									
                                    break;
                                case 1:
                                    useRoadSelectControl2.IsTextRepeated = true;
									
                                    break;
                                case 2:
                                    useRoadSelectControl3.IsTextRepeated = true;
									
                                    break;
                                case 3:
                                    useRoadSelectControl4.IsTextRepeated = true;
									
                                    break;
                                case 4:
                                    useRoadSelectControl5.IsTextRepeated = true;
									
                                    break;
                                case 5:
                                    useRoadSelectControl6.IsTextRepeated = true;
									
                                    break;
                            }
									
                            switch (i)
                            {
                                case 0:
                                    avoidRoadSelectControl1.IsTextRepeated = true;
                                    break;
                                case 1:
                                    avoidRoadSelectControl2.IsTextRepeated = true;
                                    break;
                                case 2:
                                    avoidRoadSelectControl3.IsTextRepeated = true;
                                    break;
                                case 3:
                                    avoidRoadSelectControl4.IsTextRepeated = true;
                                    break;
                                case 4:
                                    avoidRoadSelectControl5.IsTextRepeated = true;
                                    break;
                                case 5:
                                    avoidRoadSelectControl6.IsTextRepeated = true;
                                    break;
                            }
                        }
                        else
                        {

                            switch (i)
                            {
                                case 0:
                                    avoidRoadSelectControl1.IsTextRepeated = false;
                                    break;
                                case 1:
                                    avoidRoadSelectControl2.IsTextRepeated = false;
                                    break;
                                case 2:
                                    avoidRoadSelectControl3.IsTextRepeated = false;
                                    break;
                                case 3:
                                    avoidRoadSelectControl4.IsTextRepeated = false;
                                    break;
                                case 4:
                                    avoidRoadSelectControl5.IsTextRepeated = false;
                                    break;
                                case 5:
                                    avoidRoadSelectControl6.IsTextRepeated = false;
                                    break;
                            }
                        }

                    }
                }
				
            }

            for( int i = 0; i < useRoadList.Count; i++)
            {
                if (((TDRoad)useRoadList[i]).RoadName != "")
                {
						
                    useIndex = (avoidTempRoadList).IndexOf(useTempRoadList[i]); 

                    if (road.ValidateRoadName(((TDRoad)useRoadList[i]).RoadName))
                    {
                        //if road to use is repeated in avoidRoad, set the appropriate boolean property to true.
                        if (useIndex != -1 )
                        {						
                            switch (useIndex)
                            {
                                case 0:
                                    avoidRoadSelectControl1.IsTextRepeated = true;
									
                                    break;
                                case 1:
                                    avoidRoadSelectControl2.IsTextRepeated = true;
									
                                    break;
                                case 2:
                                    avoidRoadSelectControl3.IsTextRepeated = true;
									
                                    break;
                                case 3:
                                    avoidRoadSelectControl4.IsTextRepeated = true;
									
                                    break;
                                case 4:
                                    avoidRoadSelectControl5.IsTextRepeated = true;
									
                                    break;
                                case 5:
                                    avoidRoadSelectControl6.IsTextRepeated = true;
									
                                    break;
                            }
									
                            switch (i)
                            {
                                case 0:
                                    useRoadSelectControl1.IsTextRepeated = true;
                                    break;
                                case 1:
                                    useRoadSelectControl2.IsTextRepeated = true;
                                    break;
                                case 2:
                                    useRoadSelectControl3.IsTextRepeated = true;
                                    break;
                                case 3:
                                    useRoadSelectControl4.IsTextRepeated = true;
                                    break;
                                case 4:
                                    useRoadSelectControl5.IsTextRepeated = true;
                                    break;
                                case 5:
                                    useRoadSelectControl6.IsTextRepeated = true;
                                    break;
                            }
                        }
                        else
                        {

                            switch (i)
                            {
                                case 0:
                                    useRoadSelectControl1.IsTextRepeated = false;
                                    break;
                                case 1:
                                    useRoadSelectControl2.IsTextRepeated = false;
                                    break;
                                case 2:
                                    useRoadSelectControl3.IsTextRepeated = false;
                                    break;
                                case 3:
                                    useRoadSelectControl4.IsTextRepeated = false;
                                    break;
                                case 4:
                                    useRoadSelectControl5.IsTextRepeated = false;
                                    break;
                                case 5:
                                    useRoadSelectControl6.IsTextRepeated = false;
                                    break;
                            }
                        }
                    }
                }
				
            }

        }

        //This method is usually called from door to door functionality to ensure the values are passed to the ambiguity page
        private void PopulateRoadUnspecified()
        {
            for (int i = 0; i < avoidRoadsList.Length; i++)
            {
										 
                switch (i)
                {
                    case 0:
                        avoidRoadSelectControl1.RoadUnspecified.RoadTextBox.Text = avoidRoadsList[0].RoadName;
                        break;
                    case 1:
                        avoidRoadSelectControl2.RoadUnspecified.RoadTextBox.Text = avoidRoadsList[1].RoadName;
                        break;
                    case 2:
                        avoidRoadSelectControl3.RoadUnspecified.RoadTextBox.Text = avoidRoadsList[2].RoadName;
                        break;
                    case 3:
                        avoidRoadSelectControl4.RoadUnspecified.RoadTextBox.Text = avoidRoadsList[3].RoadName;
                        break;
                    case 4:
                        avoidRoadSelectControl5.RoadUnspecified.RoadTextBox.Text = avoidRoadsList[4].RoadName;
                        break;
                    case 5:
                        avoidRoadSelectControl6.RoadUnspecified.RoadTextBox.Text = avoidRoadsList[5].RoadName;
                        break;
                }
            }

		
            

            for (int i = 0; i < useRoadsList.Length; i++)
            {
                switch (i)
                {
                    case 0:
                        useRoadSelectControl1.RoadUnspecified.RoadTextBox.Text = useRoadsList[0].RoadName;
                        break;
                    case 1:
                        useRoadSelectControl2.RoadUnspecified.RoadTextBox.Text = useRoadsList[1].RoadName;
                        break;
                    case 2:
                        useRoadSelectControl3.RoadUnspecified.RoadTextBox.Text = useRoadsList[2].RoadName;
                        break;
                    case 3:
                        useRoadSelectControl4.RoadUnspecified.RoadTextBox.Text = useRoadsList[3].RoadName;
                        break;
                    case 4:
                        useRoadSelectControl5.RoadUnspecified.RoadTextBox.Text = useRoadsList[4].RoadName;
                        break;
                    case 5:
                        useRoadSelectControl6.RoadUnspecified.RoadTextBox.Text = useRoadsList[5].RoadName;
                        break;
                }
            }
        }

		private void AddCommaAvoidRoads() 
		{
			
			for (int i = 1; i < avoidTempRoadList.Count; i++)
			{
										 
				switch (i)
				{
					case 1:
						useIndex = (useTempRoadList.IndexOf(avoidTempRoadList[i-1]));
						//If the previous road is validated and it is not repeated in useRoad then add a "," at the end of the previous road
						if ((road.ValidateRoadName(avoidRoadsList[i-1].RoadName)) && useIndex == -1) 
						{
							//Only add commas for validated roads which have a comma
							if (!avoidRoadSelectControl1.RoadSpecified.EndsWith(","))
							{
								avoidRoadSelectControl1.RoadSpecified = avoidRoadsList[i-1].RoadName + ",";
							}
						}
						else
						{	//If the value is not valid, remove comma
							if (avoidRoadSelectControl1.RoadSpecified.EndsWith(","))
							{
								avoidRoadSelectControl1.RoadSpecified=avoidRoadSelectControl1.RoadSpecified.TrimEnd(CommaChar);
							}
						}
						break;
					case 2:
						useIndex = (useTempRoadList.IndexOf(avoidTempRoadList[i-1]));
						//If the previous road is validated then add a "," at the end of the previous road
						
						if ((road.ValidateRoadName(avoidRoadsList[i-1].RoadName)) && useIndex == -1) 
						{
							//Only add commas for validated roads
							if (!avoidRoadSelectControl2.RoadSpecified.EndsWith(","))
							{
								avoidRoadSelectControl2.RoadSpecified = avoidRoadsList[i-1].RoadName + ",";
							}
						}
						else
						{
							if (avoidRoadSelectControl2.RoadSpecified.EndsWith(","))
							{
								avoidRoadSelectControl2.RoadSpecified=avoidRoadSelectControl2.RoadSpecified.TrimEnd(CommaChar);
							}
						}
						break;
					case 3:
						useIndex = (useTempRoadList.IndexOf(avoidTempRoadList[i-1]));
						//If the previous road is validated then add a "," at the end of the previous road
						
						if ((road.ValidateRoadName(avoidRoadsList[i-1].RoadName))&& useIndex == -1)
						{
							//Only add commas for validated roads
							if (!avoidRoadSelectControl3.RoadSpecified.EndsWith(",")) 
							{
								avoidRoadSelectControl3.RoadSpecified = avoidRoadsList[i-1].RoadName + ",";
							}
						}
						else
						{
							if (avoidRoadSelectControl3.RoadSpecified.EndsWith(","))
							{
								avoidRoadSelectControl3.RoadSpecified=avoidRoadSelectControl3.RoadSpecified.TrimEnd(CommaChar);
							}
						}
						break;
					case 4:
						useIndex = (useTempRoadList.IndexOf(avoidTempRoadList[i-1]));
						//If the previous road is validated then add a "," at the end of the previous road
					
						if ((road.ValidateRoadName(avoidRoadsList[i-1].RoadName))&& useIndex == -1)
						{
							//Only add commas for validated roads
							if (!avoidRoadSelectControl4.RoadSpecified.EndsWith(","))
							{
								avoidRoadSelectControl4.RoadSpecified = avoidRoadsList[i-1].RoadName + ",";
							}
						}
						else
						{
							if (avoidRoadSelectControl4.RoadSpecified.EndsWith(","))
							{
								avoidRoadSelectControl4.RoadSpecified=avoidRoadSelectControl4.RoadSpecified.TrimEnd(CommaChar);
							}
						}
						break;
					case 5:
						useIndex = (useTempRoadList.IndexOf(avoidTempRoadList[i-1]));
						//If the previous road is validated then add a "," at the end of the previous road
						if ((road.ValidateRoadName(avoidRoadsList[i-1].RoadName))&& useIndex == -1)
						{
							//Only add commas for validated roads
							if (!avoidRoadSelectControl5.RoadSpecified.EndsWith(","))
							{
								avoidRoadSelectControl5.RoadSpecified = avoidRoadsList[i-1].RoadName + ",";
							}
						}
						else
						{
							if (avoidRoadSelectControl5.RoadSpecified.EndsWith(","))
							{
								avoidRoadSelectControl5.RoadSpecified=avoidRoadSelectControl5.RoadSpecified.TrimEnd(CommaChar);
							}
						}
						break;
				}
			}

			
		}
        
		//Populates useRoads
		private void AddCommaUseRoads() 
		{

			for (int i = 1; i < useTempRoadList.Count; i++)
			{
				switch (i)
				{
					case 1:
						useIndex = (avoidTempRoadList.IndexOf(useTempRoadList[i-1]));
						//If the previous road is validated then add a "," at the end of the previous road
						if ((road.ValidateRoadName(useRoadsList[i-1].RoadName)) && useIndex == -1) 
						{
							//Only add commas for validated roads
							if (!useRoadSelectControl1.RoadSpecified.EndsWith(","))
							{
								useRoadSelectControl1.RoadSpecified = useRoadsList[i-1].RoadName + ",";
							}
						}
						else
						{
							if (useRoadSelectControl1.RoadSpecified.EndsWith(","))
							{
								useRoadSelectControl1.RoadSpecified=useRoadSelectControl1.RoadSpecified.TrimEnd(CommaChar);
							}
						}
						break;
					case 2:
						useIndex = (avoidTempRoadList.IndexOf(useTempRoadList[i-1]));
						//If the previous road is validated then add a "," at the end of the previous road
						
						if ((road.ValidateRoadName(useRoadsList[i-1].RoadName)) && useIndex == -1) 
						{
							//Only add commas for validated roads
							if (!useRoadSelectControl2.RoadSpecified.EndsWith(","))
							{
								useRoadSelectControl2.RoadSpecified = useRoadsList[i-1].RoadName + ",";
							}
						}
						else
						{
							if (useRoadSelectControl2.RoadSpecified.EndsWith(","))
							{
								useRoadSelectControl2.RoadSpecified = useRoadSelectControl2.RoadSpecified.TrimEnd(CommaChar);
							}
						}
						break;
					case 3:
						useIndex = (avoidTempRoadList.IndexOf(useTempRoadList[i-1]));
						//If the previous road is validated then add a "," at the end of the previous road
						
						if ((road.ValidateRoadName(useRoadsList[i-1].RoadName))&& useIndex == -1)
						{
							//Only add commas for validated roads
							if (!useRoadSelectControl3.RoadSpecified.EndsWith(",")) 
							{
								useRoadSelectControl3.RoadSpecified = useRoadsList[i-1].RoadName + ",";
							}
						}
						else
						{
							if (useRoadSelectControl3.RoadSpecified.EndsWith(","))
							{
								useRoadSelectControl3.RoadSpecified=useRoadSelectControl3.RoadSpecified.TrimEnd(CommaChar);
							}
						}
						break;
					case 4:
						useIndex = (avoidTempRoadList.IndexOf(useTempRoadList[i-1]));
						//If the previous road is validated then add a "," at the end of the previous road
					
						if ((road.ValidateRoadName(useRoadsList[i-1].RoadName))&& useIndex == -1)
						{
							//Only add commas for validated roads
							if (!useRoadSelectControl4.RoadSpecified.EndsWith(","))
							{
								useRoadSelectControl4.RoadSpecified = useRoadsList[i-1].RoadName + ",";
							}
						}
						else
						{
							if (useRoadSelectControl4.RoadSpecified.EndsWith(","))
							{
								useRoadSelectControl4.RoadSpecified=useRoadSelectControl4.RoadSpecified.TrimEnd(CommaChar);
							}
						}
						break;
					case 5:
						useIndex = (avoidTempRoadList.IndexOf(useTempRoadList[i-1]));
						//If the previous road is validated then add a "," at the end of the previous road
						if ((road.ValidateRoadName(useRoadsList[i-1].RoadName))&& useIndex == -1)
						{
							//Only add commas for validated roads
							if (!useRoadSelectControl5.RoadSpecified.EndsWith(","))
							{
								useRoadSelectControl5.RoadSpecified = useRoadsList[i-1].RoadName + ",";
							}
						}
						else
						{
							if (useRoadSelectControl5.RoadSpecified.EndsWith(","))
							{
								useRoadSelectControl5.RoadSpecified=useRoadSelectControl5.RoadSpecified.TrimEnd(CommaChar);
							}
						}
						break;
			
				}
			}
		}

		#endregion

		#region Public Methods

		public void Search()
		{
			locationTristateControl.Search(true);
		}

		/// <summary>
		/// Sets the control back into the default state that allows a location to be entered by user
		/// </summary>
		public void SetLocationUnspecified() 
		{
			theLocation.Status = TDLocationStatus.Unspecified;
			locationControlType.Type = TDJourneyParameters.ControlType.Default;
			theSearch.ClearSearch();
			locationTristateControl.Populate(DataServiceType.FindStationLocationDrop, locationType, ref theSearch, ref theLocation, ref locationControlType, true, true, true, stationType, false);
		}

		#endregion

		#region Control Appearance

		/// <summary>
		/// Refresh the tristate control
		/// </summary>
		/// <param name="checkInput">indicates if location control checks if input has changed/not</param>
		private void RefreshTristateControl(bool checkInput)
		{
            if (!showLocationControlAutoSuggest)
            {
                //set station type so that gazetteer search is not filtered
                stationType = StationType.Undetermined;
                locationTristateControl.Populate(DataServiceType.CarViaDrop, locationType, ref theSearch, ref theLocation, ref locationControlType, false, true, true, stationType, checkInput);
            }
		}

		private void SetUpResources()
		{
			//travelViaLabel text
			travelViaLabel.Text = Global.tdResourceManager.GetString("FindViaLocationControl.directionLabelVia",TDCultureInfo.CurrentUICulture);

            // setting the label for the tri state location input box
            locationTristateControl.LocationUnspecifiedControl.TypeInstruction.Text = GetResource("FindStationInput.labelSRLocation");
		}
		#endregion

		#endregion

		#region public event handling

        /// <summary>
        /// Occurs when the user ticks the avoid motorways check box
        /// </summary>
        public event EventHandler AvoidMotorwaysChanged
        {
            add { this.Events.AddHandler(AvoidMotorwaysChangedKey, value); }
            remove { this.Events.RemoveHandler(AvoidMotorwaysChangedKey, value); }
        }
        
        /// <summary>
        /// Occurs when the user ticks the ban limited access check box
        /// </summary>
        public event EventHandler BanLimitedAccessChanged
        {
            add { this.Events.AddHandler(BanLimitedAccessChangedKey, value); }
            remove { this.Events.RemoveHandler(BanLimitedAccessChangedKey, value); }
        }

		/// <summary>
		/// Occurs when the user ticks the avoid tolls check box
		/// </summary>
		public event EventHandler AvoidTollsChanged
		{
			add { this.Events.AddHandler(AvoidTollsChangedKey, value); }
			remove { this.Events.RemoveHandler(AvoidTollsChangedKey, value); }
		}

		/// <summary>
		/// Occurs when the user ticks the avoid ferries check box
		/// </summary>
		public event EventHandler AvoidFerriesChanged
		{
			add { this.Events.AddHandler(AvoidFerriesChangedKey, value); }
			remove { this.Events.RemoveHandler(AvoidFerriesChangedKey, value); }
		}

		#endregion

		#region private event handling

		/// <summary>
		/// Handles the AvoidMotorways event 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>

        private void OnAvoidMotorwaysChanged(object sender, EventArgs e)
        {
            //avoidMotorways = avoidMotorwayCheckbox.Checked;
            RaiseEvent(AvoidMotorwaysChangedKey);
        }

        /// <summary>
        /// Handles the BanLimitedAccess event 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnBanLimitedAccessChanged(object sender, EventArgs e)
        {
            RaiseEvent(BanLimitedAccessChangedKey);
        }

        /// <summary>
		/// Handles the AvoidTolls event 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnAvoidTollsChanged(object sender, EventArgs e)
		{
			
			//avoidTolls = avoidTollsCheckbox.Checked;
			RaiseEvent(AvoidTollsChangedKey);
			
		}

		/// <summary>
		/// Handles the AvoidFerries event 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>

		private void OnAvoidFerriesChanged(object sender, EventArgs e)
		{
			//avoidFerries = avoidFerriesCheckbox.Checked;
			RaiseEvent(AvoidFerriesChangedKey);
		}

		/// <summary>
		/// Retrieves the delegate attached to an event handler from the Events
		/// list and calls it.
		/// </summary>
		/// <param name="key"></param>
		private void RaiseEvent(object key)
		{
			EventHandler theDelegate = Events[key] as EventHandler;
			if (theDelegate != null)
				theDelegate(this, EventArgs.Empty);
		}
		
		/// <summary>
		/// Handles the event indicating that the new location button has been clicked
		/// on the nested via location control
		/// </summary>
		/// <param name="sender">event originator</param>
		/// <param name="e">event arguments</param>
		private void OnNewLocation(object sender, EventArgs e)
		{
			theLocation = new TDLocation();
			theSearch = new LocationSearch();
			locationControlType = new TDJourneyParameters.LocationSelectControlType(TDJourneyParameters.ControlType.Default);
			newLocationClicked = true;

			if (NewLocation != null)
				NewLocation(sender, e);
		}


		private void OnMapClick(object sender, EventArgs e)
		{
			if (MapClick != null)
				MapClick(sender, e);
		}
		

        //Enter the values in avoidRoad and useRoad boxes into an array.
        private void PopulateRoadArray()
        {
            arrayAvoid = new string[6];
            if ((UseAvoidSelectControl1.RoadUnspecified.RoadTextBox.Text != "") || 
                (UseAvoidSelectControl1.RoadAmbiguous.AmbiguousRoadTextBox.Text != "") ||
                (UseAvoidSelectControl1.RoadValid.RoadLabel.Text != ""))
            {

                if (UseAvoidSelectControl1.RoadUnspecified.Visible) 
                    arrayAvoid[0] = avoidRoadSelectControl1.RoadUnspecified.RoadTextBox.Text;
                else
                    if (UseAvoidSelectControl1.RoadAmbiguous.Visible)
                    arrayAvoid[0] = avoidRoadSelectControl1.RoadAmbiguous.AmbiguousRoadTextBox.Text;
                else
                    if (UseAvoidSelectControl1.RoadValid.Visible)
                    arrayAvoid[0] = avoidRoadSelectControl1.RoadValid.RoadLabel.Text;
            }
            else
            {
                //If on door to door ambiguity page and avoidRoad arrayList is already populated, enter the
                //value into the arrayList. This ensures that if the road is changed in the ambiguity page, it is 
                //updated with the new value.
                if (PageId == PageId.JourneyPlannerAmbiguity) 
                {
                    if (avoidRoadsList.Length > 0)
                    {
                        arrayAvoid[0] = avoidRoadsList[0].RoadName;
                    }
                }
            }
										
            
            if ((UseAvoidSelectControl2.RoadUnspecified.RoadTextBox.Text != "") || 
                (UseAvoidSelectControl2.RoadAmbiguous.AmbiguousRoadTextBox.Text != "") ||
                (UseAvoidSelectControl2.RoadValid.RoadLabel.Text != ""))
            {
                if (UseAvoidSelectControl2.RoadUnspecified.Visible)
                    arrayAvoid[1] = avoidRoadSelectControl2.RoadUnspecified.RoadTextBox.Text;
                else
                    if (UseAvoidSelectControl2.RoadAmbiguous.Visible)
                    arrayAvoid[1] = avoidRoadSelectControl2.RoadAmbiguous.AmbiguousRoadTextBox.Text;
                else
                    if (UseAvoidSelectControl2.RoadValid.Visible)
                    arrayAvoid[1] = avoidRoadSelectControl2.RoadValid.RoadLabel.Text;
            }
            else
            {
                //If on door to door ambiguity page and avoidRoad arrayList is already populated, enter the
                //value into the arrayList. This ensures that if the road is changed in the ambiguity page, it is 
                //updated with the new value.
                if (PageId == PageId.JourneyPlannerAmbiguity) 
                {
                    if (avoidRoadsList.Length > 1)
                    {
                        arrayAvoid[1] = avoidRoadsList[1].RoadName;
                    }
                }
            }


            if ((UseAvoidSelectControl3.RoadUnspecified.RoadTextBox.Text != "") || 
                (UseAvoidSelectControl3.RoadAmbiguous.AmbiguousRoadTextBox.Text != "") ||
                (UseAvoidSelectControl3.RoadValid.RoadLabel.Text != ""))
            {
                if (UseAvoidSelectControl3.RoadUnspecified.Visible)
                    arrayAvoid[2] = avoidRoadSelectControl3.RoadUnspecified.RoadTextBox.Text;
                else

                    if (UseAvoidSelectControl3.RoadAmbiguous.Visible)
                    arrayAvoid[2] = avoidRoadSelectControl3.RoadAmbiguous.AmbiguousRoadTextBox.Text;
                else
                    if (UseAvoidSelectControl3.RoadValid.Visible)
                    arrayAvoid[2] = avoidRoadSelectControl3.RoadValid.RoadLabel.Text;
            }
            else
            {
                //If on door to door ambiguity page and avoidRoad arrayList is already populated, enter the
                //value into the arrayList. This ensures that if the road is changed in the ambiguity page, it is 
                //updated with the new value.
                if (PageId == PageId.JourneyPlannerAmbiguity) 
                {
                    if (avoidRoadsList.Length > 2)
                    {
                        arrayAvoid[2] = avoidRoadsList[2].RoadName;
                    }
                }
            }


            if ((UseAvoidSelectControl4.RoadUnspecified.RoadTextBox.Text != "") || 
                (UseAvoidSelectControl4.RoadAmbiguous.AmbiguousRoadTextBox.Text != "") ||
                (UseAvoidSelectControl4.RoadValid.RoadLabel.Text != ""))
            {
                if (UseAvoidSelectControl4.RoadUnspecified.Visible)
                    arrayAvoid[3] = avoidRoadSelectControl4.RoadUnspecified.RoadTextBox.Text;
                else
                    if (UseAvoidSelectControl4.RoadAmbiguous.Visible)
                    arrayAvoid[3] = avoidRoadSelectControl4.RoadAmbiguous.AmbiguousRoadTextBox.Text;
                else
                    if (UseAvoidSelectControl4.RoadValid.Visible)
                    arrayAvoid[3] = avoidRoadSelectControl4.RoadValid.RoadLabel.Text;
            }
            else
            {
                //If on door to door ambiguity page and avoidRoad arrayList is already populated, enter the
                //value into the arrayList. This ensures that if the road is changed in the ambiguity page, it is 
                //updated with the new value.
                if (PageId == PageId.JourneyPlannerAmbiguity) 
                {
                    if (avoidRoadsList.Length > 3)
                    {
                        arrayAvoid[3] = avoidRoadsList[3].RoadName;
                    }
                }
            }

            if ((UseAvoidSelectControl5.RoadUnspecified.RoadTextBox.Text != "") || 
                (UseAvoidSelectControl5.RoadAmbiguous.AmbiguousRoadTextBox.Text != "") ||
                (UseAvoidSelectControl5.RoadValid.RoadLabel.Text != ""))
            {
                if (UseAvoidSelectControl5.RoadUnspecified.Visible)
                    arrayAvoid[4] = avoidRoadSelectControl5.RoadUnspecified.RoadTextBox.Text;
                else
                    if (UseAvoidSelectControl5.RoadAmbiguous.Visible)
                    arrayAvoid[4] = avoidRoadSelectControl5.RoadAmbiguous.AmbiguousRoadTextBox.Text;
                else
                    if (UseAvoidSelectControl5.RoadValid.Visible)
                    arrayAvoid[4] = avoidRoadSelectControl5.RoadValid.RoadLabel.Text;
            }
            else
            {
                //If on door to door ambiguity page and avoidRoad arrayList is already populated, enter the
                //value into the arrayList. This ensures that if the road is changed in the ambiguity page, it is 
                //updated with the new value.
                if (PageId == PageId.JourneyPlannerAmbiguity) 
                {
                    if (avoidRoadsList.Length > 4)
                    {
                        arrayAvoid[4] = avoidRoadsList[4].RoadName;
                    }
                }
            }
            
            if ((UseAvoidSelectControl6.RoadUnspecified.RoadTextBox.Text != "") || 
                (UseAvoidSelectControl6.RoadAmbiguous.AmbiguousRoadTextBox.Text != "") ||
                (UseAvoidSelectControl6.RoadValid.RoadLabel.Text != ""))
            {
                if (UseAvoidSelectControl6.RoadUnspecified.Visible)
                    arrayAvoid[5] = avoidRoadSelectControl6.RoadUnspecified.RoadTextBox.Text;
                else
                    if (UseAvoidSelectControl6.RoadAmbiguous.Visible)
                    arrayAvoid[5] = avoidRoadSelectControl6.RoadAmbiguous.AmbiguousRoadTextBox.Text;
                else
                    if (UseAvoidSelectControl6.RoadValid.Visible)
                    arrayAvoid[5] = avoidRoadSelectControl6.RoadValid.RoadLabel.Text;
            }
            else
            {
                //If on door to door ambiguity page and avoidRoad arrayList is already populated, enter the
                //value into the arrayList. This ensures that if the road is changed in the ambiguity page, it is 
                //updated with the new value.
                if (PageId == PageId.JourneyPlannerAmbiguity) 
                {
                    if (avoidRoadsList.Length > 5)
                    {
                        arrayAvoid[5] = avoidRoadsList[5].RoadName;
                    }
                }
            }


            arrayUse = new string[6];

            if ((UseSelectControl1.RoadUnspecified.RoadTextBox.Text != "") || 
                (UseSelectControl1.RoadAmbiguous.AmbiguousRoadTextBox.Text != "") ||
                (UseSelectControl1.RoadValid.RoadLabel.Text != ""))
            {
                if (UseSelectControl1.RoadUnspecified.Visible) 
                    arrayUse[0] = UseSelectControl1.RoadUnspecified.RoadTextBox.Text;
                else
                    if (UseSelectControl1.RoadAmbiguous.Visible)
                    arrayUse[0] = UseSelectControl1.RoadAmbiguous.AmbiguousRoadTextBox.Text;
                else
                    if (UseSelectControl1.RoadValid.Visible)
                    arrayUse[0] = UseSelectControl1.RoadValid.RoadLabel.Text;
            }
            else
            {
                //If on door to door ambiguity page and useRoad arrayList is already populated, enter the
                //value into the arrayList. This ensures that if the road is changed in the ambiguity page, it is 
                //updated with the new value.
                if (PageId == PageId.JourneyPlannerAmbiguity) 
                {
                    if (useRoadsList.Length > 0)
                    {
                        arrayUse[0] = useRoadsList[0].RoadName;
                    }
                }
            }

            if ((UseSelectControl2.RoadUnspecified.RoadTextBox.Text != "") || 
                (UseSelectControl2.RoadAmbiguous.AmbiguousRoadTextBox.Text != "") ||
                (UseSelectControl2.RoadValid.RoadLabel.Text != ""))
            {
                if (UseSelectControl2.RoadUnspecified.Visible)
                    arrayUse[1] = UseSelectControl2.RoadUnspecified.RoadTextBox.Text;
                else
                    if (UseSelectControl2.RoadAmbiguous.Visible)
                    arrayUse[1] = UseSelectControl2.RoadAmbiguous.AmbiguousRoadTextBox.Text;
                else
                    if (UseSelectControl2.RoadValid.Visible)
                    arrayUse[1] = UseSelectControl2.RoadValid.RoadLabel.Text;
            }
            else
            {
                //If on door to door ambiguity page and useRoad arrayList is already populated, enter the
                //value into the arrayList. This ensures that if the road is changed in the ambiguity page, it is 
                //updated with the new value.
                if (PageId == PageId.JourneyPlannerAmbiguity) 
                {
                    if (useRoadsList.Length > 1)
                    {
                        arrayUse[1] = useRoadsList[1].RoadName;
                    }
                }
            }


            if ((UseSelectControl3.RoadUnspecified.RoadTextBox.Text != "") || 
                (UseSelectControl3.RoadAmbiguous.AmbiguousRoadTextBox.Text != "") ||
                (UseSelectControl3.RoadValid.RoadLabel.Text != ""))
            {
                if (UseSelectControl3.RoadUnspecified.Visible)
                    arrayUse[2] = UseSelectControl3.RoadUnspecified.RoadTextBox.Text;
                else
                    if (UseSelectControl3.RoadAmbiguous.Visible)
                    arrayUse[2] = UseSelectControl3.RoadAmbiguous.AmbiguousRoadTextBox.Text;
                else
                    if (UseSelectControl3.RoadValid.Visible)
                    arrayUse[2] = UseSelectControl3.RoadValid.RoadLabel.Text;
            }
            else
            {
                //If on door to door ambiguity page and useRoad arrayList is already populated, enter the
                //value into the arrayList. This ensures that if the road is changed in the ambiguity page, it is 
                //updated with the new value.
                if (PageId == PageId.JourneyPlannerAmbiguity) 
                {
                    if (useRoadsList.Length > 2)
                    {
                        arrayUse[2] = useRoadsList[2].RoadName;
                    }
                }
            }

            if ((UseSelectControl4.RoadUnspecified.RoadTextBox.Text != "") || 
                (UseSelectControl4.RoadAmbiguous.AmbiguousRoadTextBox.Text != "") ||
                (UseSelectControl4.RoadValid.RoadLabel.Text != ""))
            {
                if (UseSelectControl4.RoadUnspecified.Visible)
                    arrayUse[3] = UseSelectControl4.RoadUnspecified.RoadTextBox.Text;
                else
                    if (UseSelectControl4.RoadAmbiguous.Visible)
                    arrayUse[3] = UseSelectControl4.RoadAmbiguous.AmbiguousRoadTextBox.Text;
                else
                    if (UseSelectControl4.RoadValid.Visible)
                    arrayUse[3] = UseSelectControl4.RoadValid.RoadLabel.Text;
            }
            else
            {
                //If on door to door ambiguity page and useRoad arrayList is already populated, enter the
                //value into the arrayList. This ensures that if the road is changed in the ambiguity page, it is 
                //updated with the new value.
                if (PageId == PageId.JourneyPlannerAmbiguity) 
                {
                    if (useRoadsList.Length > 3)
                    {
                        arrayUse[3] = useRoadsList[3].RoadName;
                    }
                }
            }

            if ((UseSelectControl5.RoadUnspecified.RoadTextBox.Text != "") || 
                (UseSelectControl5.RoadAmbiguous.AmbiguousRoadTextBox.Text != "") ||
                (UseSelectControl5.RoadValid.RoadLabel.Text != ""))
            {
                if (UseSelectControl5.RoadUnspecified.Visible)
                    arrayUse[4] = UseSelectControl5.RoadUnspecified.RoadTextBox.Text;
                else
                    if (UseSelectControl5.RoadAmbiguous.Visible)
                    arrayUse[4] = UseSelectControl5.RoadAmbiguous.AmbiguousRoadTextBox.Text;
                else
                    if (UseSelectControl5.RoadValid.Visible)
                    arrayUse[4] = UseSelectControl5.RoadValid.RoadLabel.Text;
            }
            else
            {
                //If on door to door ambiguity page and useRoad arrayList is already populated, enter the
                //value into the arrayList. This ensures that if the road is changed in the ambiguity page, it is 
                //updated with the new value.
                if (PageId == PageId.JourneyPlannerAmbiguity) 
                {
                    if (useRoadsList.Length > 4)
                    {
                        arrayUse[4] = useRoadsList[4].RoadName;
                    }
                }
            }
            
            if ((UseSelectControl6.RoadUnspecified.RoadTextBox.Text != "") || 
                (UseSelectControl6.RoadAmbiguous.AmbiguousRoadTextBox.Text != "") ||
                (UseSelectControl6.RoadValid.RoadLabel.Text != ""))
            {
                if (UseSelectControl6.RoadUnspecified.Visible)
                    arrayUse[5] = UseSelectControl6.RoadUnspecified.RoadTextBox.Text;
                else
                    if (UseSelectControl6.RoadAmbiguous.Visible)
                    arrayUse[5] = UseSelectControl6.RoadAmbiguous.AmbiguousRoadTextBox.Text;
                else
                    if (UseSelectControl6.RoadValid.Visible)
                    arrayUse[5] = UseSelectControl6.RoadValid.RoadLabel.Text;
            }
            else
            {
                //If on door to door ambiguity page and useRoad arrayList is already populated, enter the
                //value into the arrayList. This ensures that if the road is changed in the ambiguity page, it is 
                //updated with the new value.
                if (PageId == PageId.JourneyPlannerAmbiguity) 
                {
                    if (useRoadsList.Length > 5)
                    {
                        arrayUse[5] = useRoadsList[5].RoadName;
                    }
                }
            }

           
        }

		#endregion
		
		#region Public properties
        
		/// <summary>
		/// Returns true if the control is being displayed in ambiguity mode. This is determined
		/// from the values of <code>journeyOptionDisplayMode</code>.
		/// </summary>
		public bool AmbiguityMode
		{
			get { return ((AvoidRoadDisplayMode != GenericDisplayMode.Normal)
						|| (UseRoadDisplayMode != GenericDisplayMode.Normal)); }

		}

        #region GenericDisplayModes

        /// <summary>
		/// Sets the mode for the control.
		/// Note that in this context, GenericDisplayMode.Ambiguity is treated
		/// the same way as GenericDisplayMode.Readonly.
		/// </summary>
		public GenericDisplayMode JourneyOptionDisplayMode
		{
			get { return journeyOptionDisplayMode; }
			set { journeyOptionDisplayMode = value; }
		}

		public GenericDisplayMode AvoidRoadDisplayMode
		{
			get { return avoidRoadDisplayMode; }
			set { avoidRoadDisplayMode = value; }
		}

		public GenericDisplayMode UseRoadDisplayMode
		{
			get { return useRoadDisplayMode; }
			set { useRoadDisplayMode = value; }
		}

		public GenericDisplayMode ViaRoadDisplayMode
		{
			get { return viaRoadDisplayMode; }
			set { viaRoadDisplayMode = value; }
		}

        #endregion

        #region Controls

        #region Controls - Avoid Roads

        /// <summary>
		/// Allows access to the instance of TristateRoadControl contained within this control.
		/// </summary>
		public TristateRoadControl UseAvoidSelectControl1
		{
			get { return avoidRoadSelectControl1; }
		}

		/// <summary>
		/// Allows access to the instance of TristateRoadControl contained within this control.
		/// </summary>
		public TristateRoadControl UseAvoidSelectControl2
		{
			get { return avoidRoadSelectControl2; }
		}

		/// <summary>
		/// Allows access to the TristateRoadControl contained within this control.
		/// </summary>
		public TristateRoadControl UseAvoidSelectControl3
		{
			get { return avoidRoadSelectControl3; }
		}

		/// <summary>
		/// Allows access to the TristateRoadControl contained within this control.
		/// </summary>
		public TristateRoadControl UseAvoidSelectControl4
		{
			get { return avoidRoadSelectControl4; }
		}

		/// <summary>
		/// Allows access to the TristateRoadControl contained within this control.
		/// </summary>
		public TristateRoadControl UseAvoidSelectControl5
		{
			get { return avoidRoadSelectControl5; }
		}

		/// <summary>
		/// Allows access to the TristateRoadControl contained within this control.
		/// </summary>
		public TristateRoadControl UseAvoidSelectControl6
		{
			get { return avoidRoadSelectControl6; }
		}

        #endregion

        #region Controls - Use Roads

        /// <summary>
		/// Allows access to the TristateRoadControl contained within this control.
		/// </summary>
		public TristateRoadControl UseSelectControl1
		{
			get { return useRoadSelectControl1; }
		}

		/// <summary>
		/// Allows access to the instance of TristateRoadControl contained within this control.
		/// </summary>
		public TristateRoadControl UseSelectControl2
		{
			get { return useRoadSelectControl2; }
		}

		/// <summary>
		/// Allows access to the TristateRoadControl contained within this control.
		/// </summary>
		public TristateRoadControl UseSelectControl3
		{
			get { return useRoadSelectControl3; }
		}

		/// <summary>
		/// Allows access to the TristateRoadControl contained within this control.
		/// </summary>
		public TristateRoadControl UseSelectControl4
		{
			get { return useRoadSelectControl4; }
		}

		/// <summary>
		/// Allows access to the TristateRoadControl contained within this control.
		/// </summary>
		public TristateRoadControl UseSelectControl5
		{
			get { return useRoadSelectControl5; }
		}

		/// <summary>
		/// Allows access to the TristateRoadControl contained within this control.
		/// </summary>
		public TristateRoadControl UseSelectControl6
		{
			get { return useRoadSelectControl6; }
		}

        #endregion

        #region Controls - Labels

        /// <summary>
        /// Read only property returning the avoid road types display label
        /// used when in ambiguity mode to display the label to avoid tolls, ferries, etc.. 
        /// </summary>
        public Label AvoidRoadsLabel
        {
            get
            {
                return roadsToAvoidLabel;
            }
        }

        /// <summary>
        /// Read only property returning avoid these roads label
        /// used when in ambiguity mode.
        /// </summary>
        public Label AvoidTheseRoadsLabel
        {
            get
            {
                return theseRoadsToAvoidLabel;
            }
        }

        /// <summary>
        /// Read only property returning use these roads label
        /// used when in ambiguity mode.
        /// </summary>
        public Label UseTheseRoadsLabel
        {
            get
            {
                return theseRoadsToUseLabel;
            }
        }

        /// <summary>
        /// Readonly property for the road error message label
        /// </summary>
        public Label LabelRoadAlert
        {
            get
            {
                return roadAlertLabel;
            }
        }

        #endregion

        /// <summary>
        /// Read Only property allows access to PageOptionsControl contained within this control.
        /// </summary>
        public FindPageOptionsControl PageOptionsControl
        {
            get
            {
                return pageOptionsControl;
            }
        }

        #endregion

        /// <summary>
        /// Read/Write property for change of car preferences
        /// </summary>
        public bool PreferencesChanged
        {
            get
            {
                // If this control is called from door to door ambiguity page, Check
                // if via location box is visible in ambiguity page. If it is visible, then
                // do not allocate a value to preferenceChanged because this is already done 
                // in ambiguity page.
                if (PageId == PageId.JourneyPlannerAmbiguity)
                {
                    if (AmbiguityMode
                            && (((this.TheSearch != null && this.TheSearch.InputText.Length == 0 && !newLocationClicked)
                                 || (locationControl.Search != null && locationControl.Search.InputText.Length == 0 && !newLocationClicked))
                               && (!AvoidRoadExist) && (!UseRoadExist)
                               && (!AvoidMotorways) && (!AvoidTolls)
                               && (!AvoidFerries) && (!BanLimitedAccess))
                        )
                    {
                        preferencesChanged = false;
                    }
                    else
                    {
                        preferencesChanged = true;
                    }
                }
                else
                {
                    if (AmbiguityMode
                        && (((this.TheSearch != null && this.TheSearch.InputText.Length == 0 && !newLocationClicked)
                             || (locationControl.Search != null && locationControl.Search.InputText.Length == 0 && !newLocationClicked))
                        && (!AvoidRoadExist) && (!UseRoadExist)
                        && (!AvoidMotorways) && (!AvoidTolls)
                        && (!AvoidFerries) && (!BanLimitedAccess)))
                    {
                        preferencesChanged = false;
                    }
                    else
                    {
                        preferencesChanged = true;
                    }
                }

                return preferencesChanged;
            }
            set
            {
                preferencesChanged = value;
            }
        }

        /// <summary>
        /// Read only. Preference has been selected/entered on this control
        /// </summary>
        public bool PreferencesSelected
        {
            get
            {
                return
                     ((this.TheSearch != null && this.TheSearch.InputText.Length > 0) || (locationControl.Search != null && locationControl.Search.InputText.Length > 0))
                     || (AvoidRoadExist)
                     || (UseRoadExist)
                     || (AvoidMotorways)
                     || (AvoidTolls)
                     || (AvoidFerries)
                     || (BanLimitedAccess);
            }
        }

        /// <summary>
        /// Gets/sets if motorways should be avoided
        /// </summary>
        public bool AvoidMotorways
        {
            get
            {
                //return avoidMotorways;
                return this.avoidMotorwayCheckBox.Checked;
            }

            set
            {
                //avoidMotorways = value;
                avoidMotorwayCheckBox.Checked = value;
            }
        }

        /// <summary>
        /// Gets/sets if limited access roads should be banned
        /// </summary>
        public bool BanLimitedAccess
        {
            get
            {
                return this.banLimitedAccessCheckBox.Checked;
            }

            set
            {
                banLimitedAccessCheckBox.Checked = value;
            }
        }

		/// <summary>
		/// Gets/sets if tolls should be avoided
		/// </summary>
		public bool AvoidTolls
		{
			get
			{
				return this.avoidTollsCheckBox.Checked;
			}

			set 
			{
				avoidTollsCheckBox.Checked = value;
			}
		}

		/// <summary>
		/// Gets/sets if ferries should be avoided
		/// </summary>
		public bool AvoidFerries
		{
			get
			{
				return this.avoidFerriesCheckBox.Checked;
			}

			set
			{
				avoidFerriesCheckBox.Checked = value;
			}
		}

        /// <summary>
        /// Read only property to indicate when a value is entered for the avoid roads
        /// </summary>
        public bool AvoidRoadExist
        {
            get
            {
                // Check if anything is entered in any of the avoidRoad boxes
                if ((this.UseAvoidSelectControl1.RoadEntered == null) &&
                    (this.UseAvoidSelectControl2.RoadEntered == null) &&
                    (this.UseAvoidSelectControl3.RoadEntered == null) &&
                    (this.UseAvoidSelectControl4.RoadEntered == null) &&
                    (this.UseAvoidSelectControl5.RoadEntered == null) &&
                    (this.UseAvoidSelectControl6.RoadEntered == null))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        /// <summary>
        ///  Read only property to indicate when a value is entered for the use roads
        /// </summary>
        public bool UseRoadExist
        {
            get
            {
                // Check if anything is entered in any of the useRoad boxes
                if ((this.UseSelectControl1.RoadEntered == null) &&
                    (this.UseSelectControl2.RoadEntered == null) &&
                    (this.UseSelectControl3.RoadEntered == null) &&
                    (this.UseSelectControl4.RoadEntered == null) &&
                    (this.UseSelectControl5.RoadEntered == null) &&
                    (this.UseSelectControl6.RoadEntered == null))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        
        /// <summary>
		/// Read/write property.  ArrayList containing roads to 
		/// avoid when performing car journey planning 
		/// </summary>
		public TDRoad[] AvoidRoadsList
		{
			get { return avoidRoadsList; }
			set { avoidRoadsList = value; }
		}

		/// <summary>
		/// Read/write property.  ArrayList containing roads to 
		/// use when performing car journey planning 
		/// </summary>
		public TDRoad[] UseRoadsList
		{
			get { return useRoadsList; }
			set { useRoadsList = value; }
		}

        #region Via Location

        /// <summary>
        /// Read/Write. Show the standard via location control (default)
        /// or the "auto-suggest" via location control
        /// </summary>
        public bool ShowLocationControlAutoSuggest
        {
            get { return showLocationControlAutoSuggest; }
            set { showLocationControlAutoSuggest = value; }
        }

		/// <summary>
		/// Allows access to the TristateLocationControl contained within this control.
		/// </summary>
		public TriStateLocationControl2 TristateLocationControl
		{
			get { return locationTristateControl; }
		}

        /// <summary>
        /// Read/Write to allow access to the LocationControl (auto suggest) contained within this control
        /// </summary>
        public LocationControl LocationControl
        {
            get { return locationControl; }
        }

		/// <summary>
		/// property holds the location type
		/// this determines the label used for the direction label i.e. Via
		/// </summary>
		public CurrentLocationType LocationType
		{
			get
			{
				return locationType;
			}
			set
			{
				locationType = value;
			}
		}

		/// <summary>
		/// property holds the LocationSearch object used to populate the viaLocationControl		
		/// </summary>
		public LocationSearch TheSearch
		{
			get
			{
				return theSearch;
			}
			set
			{
				theSearch = value;				
			}

		}

		/// <summary>
		/// property holds the TDLocation object used to populate the viaLocationControl	
		/// </summary>
		public TDLocation TheLocation
		{
			get
			{
				return theLocation;
			}
			set
			{
				theLocation = value;
			}

		}

		/// <summary>
		/// property holds the LocationSelectControlType object used to populate the viaLocationControl	
		/// </summary>
		public TDJourneyParameters.LocationSelectControlType LocationControlType
		{
			get
			{
				return locationControlType;
			}
			set
			{
				locationControlType = value;				
			}
		}

		/// <summary>
		/// Sets the Amend mode in the tristate control. See triStateControl for explanations.
		/// </summary>
		public bool AmendMode
		{
			get
			{
				return locationTristateControl.AmendMode;
			}
			set
			{
				locationTristateControl.AmendMode = value;
			}
		}

        #endregion

        #endregion

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			ExtraEventWireUp();
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
		}
		#endregion

		#region Events handling methods

		/// <summary>
		/// Extra event subscription
		/// </summary>
		private void ExtraEventWireUp()
		{
			this.avoidFerriesCheckBox.CheckedChanged += new System.EventHandler(this.OnAvoidFerriesChanged);
			this.avoidMotorwayCheckBox.CheckedChanged += new System.EventHandler(this.OnAvoidMotorwaysChanged);
            this.avoidTollsCheckBox.CheckedChanged += new System.EventHandler(this.OnAvoidTollsChanged);
            this.banLimitedAccessCheckBox.CheckedChanged += new System.EventHandler(this.OnBanLimitedAccessChanged);
        }

		#endregion

		#region update controls

		/// <summary>
		/// Updates the state of nested controls with this object's property values
		/// </summary>
        private void UpdateControls()
        {

            // If in ambiguity mode, display text for avoid motorways, tolls, ferries and limited access depending on which is selected
            if (AmbiguityMode)
            {
                #region Ambiguity Avoid mode labels

                avoidMotorwayCheckBox.Visible = false;
                avoidTollsCheckBox.Visible = false;
                avoidFerriesCheckBox.Visible = false;
                banLimitedAccessCheckBox.Visible = false;
                displayAvoidLabel.Visible = false;
                roadsToAvoidLabel.Visible = false;
                theseRoadsToAvoidLabel.Visible = false;
                theseRoadsToUseLabel.Visible = false;

                displayAvoidTollsLabel.Visible = false;
                displayAvoidMotorwayLabel.Visible = false;
                displayAvoidFerriesLabel.Visible = false;
                displayBanLimitedAccessLabel.Visible = false;

                StringBuilder avoidString = new StringBuilder();

                if (AvoidMotorways)
                {
                    avoidString.Append(GetResource(DisplayAvoidKey)).Append(' ').Append(GetResource(MotorwaysKey));
                }

                if (AvoidTolls)
                {
                    if (avoidString.Length == 0)
                    {
                        avoidString.Append(GetResource(DisplayAvoidKey)).Append(' ');
                    }
                    else
                    {
                        avoidString.Append(", ");
                    }
                    avoidString.Append(GetResource(TollsKey));
                }

                if (AvoidFerries)
                {
                    if (avoidString.Length == 0)
                    {
                        avoidString.Append(GetResource(DisplayAvoidKey)).Append(' ');
                    }
                    else
                    {
                        avoidString.Append(", ");
                    }
                    avoidString.Append(GetResource(FerriesKey));
                }

                if (BanLimitedAccess)
                {
                    if (avoidString.Length == 0)
                    {
                        avoidString.Append(GetResource(DisplayAvoidKey)).Append(' ');
                    }
                    else
                    {
                        avoidString.Append(", ");
                    }
                    avoidString.Append(GetResource(LimitedAccessKey));
                }

                if (avoidString.Length > 0)
                {
                    displayAvoidLabel.Visible = true;
                    displayAvoidLabel.Text = avoidString.ToString();
                }


                if (AvoidRoadsList.Length > 0)
                {
                    displayTheseRoadsLabel.Visible = true;

                }
                else
                {
                    displayTheseRoadsLabel.Visible = false;
                }
                note1Label.Visible = false;


                if (UseRoadsList.Length > 0)
                {
                    displayUseTheseRoadsLabel.Visible = true;

                }
                else
                {
                    displayUseTheseRoadsLabel.Visible = false;
                }
                note2Label.Visible = false;

                #endregion
            }
            else
            {
                note1Label.Visible = true;
                theseRoadsToAvoidLabel.Visible = true;
                displayTheseRoadsLabel.Visible = false;
                theseRoadsToUseLabel.Visible = true;
                displayUseTheseRoadsLabel.Visible = false;
                note2Label.Visible = true;
                displayAvoidMotorwayLabel.Visible = false;
                displayBanLimitedAccessLabel.Visible = false;
                displayAvoidTollsLabel.Visible = false;
                displayAvoidFerriesLabel.Visible = false;
                avoidMotorwayCheckBox.Visible = !AmbiguityMode;
                avoidTollsCheckBox.Visible = !AmbiguityMode;
                avoidFerriesCheckBox.Visible = !AmbiguityMode;
                banLimitedAccessCheckBox.Visible = !AmbiguityMode;
                displayAvoidLabel.Visible = AmbiguityMode && (AvoidMotorways || AvoidTolls || AvoidFerries || BanLimitedAccess);
                roadsToAvoidLabel.Visible = !AmbiguityMode;
            }

            #region Location control visibility

            // Required to allow following location control visibility to be applied (parent asp:panel inherited visibility
            // issue). The panel's visbiilty is subsequently updated 
            panelJourneyOptions.Visible = true;

            // Don't show the via location control if in ambiguous mode and no location has been entered
            // However if in ambiguous mode and user clicks new location, this location will be empty and
            // we do need to make the control visible so use newLocationClicked to determine this case
            if (showLocationControlAutoSuggest)
            {
                if (AmbiguityMode
                    && locationControl.Search.InputText.Length == 0
                    && !newLocationClicked)
                {
                    locationControl.Visible = false;
                    travelViaLabel.Visible = false;
                }
                else
                {
                    locationControl.Visible = true;
                    travelViaLabel.Visible = true;
                }
            }
            else
            {
                if (AmbiguityMode
                    && this.TheSearch.InputText.Length == 0
                    && !newLocationClicked)
                {
                    locationTristateControl.Visible = false;
                    travelViaLabel.Visible = false;
                }
                else
                {
                    locationTristateControl.Visible = true;
                    travelViaLabel.Visible = true;
                }
            }
            
            // Show the appropriate via location control (&& itself in case it's visibility has been set above
            locationTristateControl.Visible = locationTristateControl.Visible && !showLocationControlAutoSuggest;
            locationControl.Visible = locationControl.Visible && showLocationControlAutoSuggest;

            #endregion

            #region Journey options panel visibility

            if (AmbiguityMode && 
                ((!travelViaLabel.Visible) && (!AvoidRoadExist) && (!UseRoadExist) 
                 && (!AvoidMotorways) && (!AvoidTolls) && (!AvoidFerries) && !(BanLimitedAccess)
                 && (!locationTristateControl.Visible) && (!locationControl.Visible)
                ))
            {
                panelJourneyOptions.Visible = false;
            }
            else
            {
                panelJourneyOptions.Visible = true;
                tableJourneyOptions.Visible = true;
                labelJourneyOptions.Visible = true;
            }

            #endregion

            #region Avoid/Use road visibility

            switch (avoidRoadDisplayMode)
            {
                case GenericDisplayMode.ReadOnly:
                case GenericDisplayMode.Ambiguity:

                    avoidRoadSelectControl1.Visible = false;
                    avoidRoadSelectControl2.Visible = false;
                    avoidRoadSelectControl3.Visible = false;
                    avoidRoadSelectControl4.Visible = false;
                    avoidRoadSelectControl5.Visible = false;
                    avoidRoadSelectControl6.Visible = false;

                    for (int i = 0; i < avoidRoadsList.Length; i++)
                    {
                        switch (i)
                        {
                            case 0:
                                avoidRoadSelectControl1.Visible = true;
                                break;
                            case 1:
                                avoidRoadSelectControl2.Visible = true;
                                break;
                            case 2:
                                avoidRoadSelectControl3.Visible = true;
                                break;
                            case 3:
                                avoidRoadSelectControl4.Visible = true;
                                break;
                            case 4:
                                avoidRoadSelectControl5.Visible = true;
                                break;
                            case 5:
                                avoidRoadSelectControl6.Visible = true;
                                break;
                        }
                    }

                    break;
                case GenericDisplayMode.Normal:
                default:
                    avoidRoadSelectControl1.Visible = true;
                    avoidRoadSelectControl2.Visible = true;
                    avoidRoadSelectControl3.Visible = true;
                    avoidRoadSelectControl4.Visible = true;
                    avoidRoadSelectControl5.Visible = true;
                    avoidRoadSelectControl6.Visible = true;

                    avoidRoadSelectControl1.RoadUnspecified.Visible = true;
                    avoidRoadSelectControl2.RoadUnspecified.Visible = true;
                    avoidRoadSelectControl3.RoadUnspecified.Visible = true;
                    avoidRoadSelectControl4.RoadUnspecified.Visible = true;
                    avoidRoadSelectControl5.RoadUnspecified.Visible = true;
                    avoidRoadSelectControl6.RoadUnspecified.Visible = true;

                    avoidRoadSelectControl1.RoadAmbiguous.Visible = false;
                    avoidRoadSelectControl2.RoadAmbiguous.Visible = false;
                    avoidRoadSelectControl3.RoadAmbiguous.Visible = false;
                    avoidRoadSelectControl4.RoadAmbiguous.Visible = false;
                    avoidRoadSelectControl5.RoadAmbiguous.Visible = false;
                    avoidRoadSelectControl6.RoadAmbiguous.Visible = false;

                    avoidRoadSelectControl1.RoadValid.Visible = false;
                    avoidRoadSelectControl2.RoadValid.Visible = false;
                    avoidRoadSelectControl3.RoadValid.Visible = false;
                    avoidRoadSelectControl4.RoadValid.Visible = false;
                    avoidRoadSelectControl5.RoadValid.Visible = false;
                    avoidRoadSelectControl6.RoadValid.Visible = false;
                    break;
            }
            switch (useRoadDisplayMode)
            {
                case GenericDisplayMode.ReadOnly:
                case GenericDisplayMode.Ambiguity:
                    useRoadSelectControl1.Visible = false;
                    useRoadSelectControl2.Visible = false;
                    useRoadSelectControl3.Visible = false;
                    useRoadSelectControl4.Visible = false;
                    useRoadSelectControl5.Visible = false;
                    useRoadSelectControl6.Visible = false;


                    for (int i = 0; i < useRoadsList.Length; i++)
                    {
                        switch (i)
                        {
                            case 0:
                                useRoadSelectControl1.Visible = true;
                                break;
                            case 1:
                                useRoadSelectControl2.Visible = true;
                                break;
                            case 2:
                                useRoadSelectControl3.Visible = true;
                                break;
                            case 3:
                                useRoadSelectControl4.Visible = true;
                                break;
                            case 4:
                                useRoadSelectControl5.Visible = true;
                                break;
                            case 5:
                                useRoadSelectControl6.Visible = true;
                                break;
                        }
                    }
                    break;
                case GenericDisplayMode.Normal:
                default:
                    useRoadSelectControl1.Visible = true;
                    useRoadSelectControl2.Visible = true;
                    useRoadSelectControl3.Visible = true;
                    useRoadSelectControl4.Visible = true;
                    useRoadSelectControl5.Visible = true;
                    useRoadSelectControl6.Visible = true;

                    useRoadSelectControl1.RoadUnspecified.Visible = true;
                    useRoadSelectControl2.RoadUnspecified.Visible = true;
                    useRoadSelectControl3.RoadUnspecified.Visible = true;
                    useRoadSelectControl4.RoadUnspecified.Visible = true;
                    useRoadSelectControl5.RoadUnspecified.Visible = true;
                    useRoadSelectControl6.RoadUnspecified.Visible = true;

                    useRoadSelectControl1.RoadAmbiguous.Visible = false;
                    useRoadSelectControl2.RoadAmbiguous.Visible = false;
                    useRoadSelectControl3.RoadAmbiguous.Visible = false;
                    useRoadSelectControl4.RoadAmbiguous.Visible = false;
                    useRoadSelectControl5.RoadAmbiguous.Visible = false;
                    useRoadSelectControl6.RoadAmbiguous.Visible = false;

                    useRoadSelectControl1.RoadValid.Visible = false;
                    useRoadSelectControl2.RoadValid.Visible = false;
                    useRoadSelectControl3.RoadValid.Visible = false;
                    useRoadSelectControl4.RoadValid.Visible = false;
                    useRoadSelectControl5.RoadValid.Visible = false;
                    useRoadSelectControl6.RoadValid.Visible = false;

                    break;
            }

            #endregion
        }

		public void ClearRoads()
		{
			
			UseAvoidSelectControl1.RoadUnspecified.RoadTextBox.Text = string.Empty;
			UseAvoidSelectControl1.RoadAmbiguous.AmbiguousRoadTextBox.Text = string.Empty;
			UseAvoidSelectControl1.RoadValid.Visible = false;
			LabelRoadAlert.Visible = false;
			UseAvoidSelectControl1.RoadAmbiguous.Visible = false;
			UseAvoidSelectControl1.RoadUnspecified.Visible = true;

			UseAvoidSelectControl2.RoadUnspecified.RoadTextBox.Text = string.Empty;
			UseAvoidSelectControl2.RoadAmbiguous.AmbiguousRoadTextBox.Text = string.Empty;
			UseAvoidSelectControl2.RoadValid.Visible = false;
			UseAvoidSelectControl2.RoadAmbiguous.Visible = false;
			UseAvoidSelectControl2.RoadUnspecified.Visible = true;

			UseAvoidSelectControl3.RoadUnspecified.RoadTextBox.Text = string.Empty;
			UseAvoidSelectControl3.RoadAmbiguous.AmbiguousRoadTextBox.Text = string.Empty;
			UseAvoidSelectControl3.RoadValid.Visible = false;
			UseAvoidSelectControl3.RoadAmbiguous.Visible = false;
			UseAvoidSelectControl3.RoadUnspecified.Visible = true;

			UseAvoidSelectControl4.RoadUnspecified.RoadTextBox.Text = string.Empty;
			UseAvoidSelectControl4.RoadAmbiguous.AmbiguousRoadTextBox.Text = string.Empty;
			UseAvoidSelectControl4.RoadValid.Visible = false;
			UseAvoidSelectControl4.RoadAmbiguous.Visible = false;
			UseAvoidSelectControl4.RoadUnspecified.Visible = true;

			UseAvoidSelectControl5.RoadUnspecified.RoadTextBox.Text = string.Empty;
			UseAvoidSelectControl5.RoadAmbiguous.AmbiguousRoadTextBox.Text = string.Empty;
			UseAvoidSelectControl5.RoadValid.Visible = false;
			UseAvoidSelectControl5.RoadAmbiguous.Visible = false;
			UseAvoidSelectControl5.RoadUnspecified.Visible = true;

			UseAvoidSelectControl6.RoadUnspecified.RoadTextBox.Text = string.Empty;
			UseAvoidSelectControl6.RoadAmbiguous.AmbiguousRoadTextBox.Text = string.Empty;
			UseAvoidSelectControl6.RoadValid.Visible = false;
			UseAvoidSelectControl6.RoadAmbiguous.Visible = false;
			UseAvoidSelectControl6.RoadUnspecified.Visible = true;

			UseSelectControl1.RoadUnspecified.RoadTextBox.Text = string.Empty;
			UseSelectControl1.RoadAmbiguous.AmbiguousRoadTextBox.Text = string.Empty;
			UseSelectControl1.RoadValid.Visible = false;
			UseSelectControl1.RoadAmbiguous.Visible = false;
			UseSelectControl1.RoadUnspecified.Visible = true;

			UseSelectControl2.RoadUnspecified.RoadTextBox.Text = string.Empty;
			UseSelectControl2.RoadAmbiguous.AmbiguousRoadTextBox.Text = string.Empty;
			UseSelectControl2.RoadValid.Visible = false;
			UseSelectControl2.RoadAmbiguous.Visible = false;
			UseSelectControl2.RoadUnspecified.Visible = true;

			UseSelectControl3.RoadUnspecified.RoadTextBox.Text = string.Empty;
			UseSelectControl3.RoadAmbiguous.AmbiguousRoadTextBox.Text = string.Empty;
			UseSelectControl3.RoadValid.Visible = false;
			UseSelectControl3.RoadAmbiguous.Visible = false;
			UseSelectControl3.RoadUnspecified.Visible = true;

			UseSelectControl4.RoadUnspecified.RoadTextBox.Text = string.Empty;
			UseSelectControl4.RoadAmbiguous.AmbiguousRoadTextBox.Text = string.Empty;
			UseSelectControl4.RoadValid.Visible = false;
			UseSelectControl4.RoadAmbiguous.Visible = false;
			UseSelectControl4.RoadUnspecified.Visible = true;

			UseSelectControl5.RoadUnspecified.RoadTextBox.Text = string.Empty;
			UseSelectControl5.RoadAmbiguous.AmbiguousRoadTextBox.Text = string.Empty;
			UseSelectControl5.RoadValid.Visible = false;
			UseSelectControl5.RoadAmbiguous.Visible = false;
			UseSelectControl5.RoadUnspecified.Visible = true;

			UseSelectControl6.RoadUnspecified.RoadTextBox.Text = string.Empty;
			UseSelectControl6.RoadAmbiguous.AmbiguousRoadTextBox.Text = string.Empty;
			UseSelectControl6.RoadValid.Visible = false;
			UseSelectControl6.RoadAmbiguous.Visible = false;
			UseSelectControl6.RoadUnspecified.Visible = true;
		}
		#endregion

	}
}
