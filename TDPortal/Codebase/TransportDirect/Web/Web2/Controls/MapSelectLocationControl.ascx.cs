// *********************************************** 
// NAME                 : MapSelectLocationControl.ascx.cs 
// AUTHOR               : Atos Origin
// DATE CREATED         : 03/03/2004
// DESCRIPTION			:
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/MapSelectLocationControl.ascx.cs-arc  $
//
//   Rev 1.3   Jan 14 2009 11:52:36   apatel
//XHTML Compliance Changes
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.2   Mar 31 2008 13:22:10   mturner
//Drop3 from Dev Factory
//
//  Rev Devfactory Feb 04 2008 08:53:00 apatel
//  CCN 0427 - Changed the layout of the controls.
//
//   Rev 1.0   Nov 08 2007 13:16:38   mturner
//Initial revision.
//
//   Rev 1.25   Jun 26 2007 16:15:34   mmodi
//Added code to do a second car park search if first fails
//Resolution for 4457: 9.7 - Amendments to car parks
//
//   Rev 1.24   Feb 21 2007 15:40:06   jfrank
//Change for CCN0360 - Select exact toids from a map - uses new ESRI functionality.
//Resolution for 4359: Select Exact TOID from a Map
//
//   Rev 1.23   Nov 29 2006 11:22:20   jfrank
//Code change to enable the selecting of unnamed roads from maps
//Resolution for 4274: Selecting Local Roads On Maps
//
//   Rev 1.22   Oct 06 2006 16:00:02   mturner
//Merge for Stream SCR-4143
//
//   Rev 1.21.1.7   Sep 30 2006 15:05:22   mmodi
//Added check for car parking functionality switched on before reading car park properties and performing map query for car parks
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//Resolution for 4191: Car Parking: Configurable switch should be available to display/hide car parking functionality
//
//   Rev 1.21.1.6   Sep 13 2006 12:19:24   esevern
//Amended MapQuery and SelectedLocation to store and then retrieve the car park location and park and ride indicator, for display on the journey planner input page
//Resolution for 4160: Car Parking: Car park name display format
//
//   Rev 1.21.1.5   Sep 08 2006 14:44:08   esevern
//Removed call to CarParkCatalogue.LoadData. Now done only when specific car park selected.
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.21.1.4   Sep 01 2006 10:04:18   esevern
//Corrected hard coding of easting/northing after ESRI fix provided
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.21.1.3   Aug 31 2006 16:29:34   esevern
//Temporary fix to work around ESRI StrongTypeException error when reading car park data.
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.21.1.2   Aug 16 2006 11:15:04   esevern
//Added methods to check if current selection is a car park and to return the reference for a selected car park
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.21.1.1   Aug 15 2006 16:34:10   esevern
//added setting of car park references to selected location
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.21.1.0   Aug 14 2006 10:49:38   esevern
//Updated SelectedLocation property to add FindNearestCarParks 
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.21   Apr 10 2006 12:21:58   RPhilpott
//Pass all TOID's within 50m of selected point that have same name as the one chosen by the user.
//Resolution for 3752: StartEnd TOIDs: Issue with starting journey on motorway
//
//   Rev 1.20   Mar 20 2006 18:05:22   RWilby
//Merged stream0027: Start/End TOIDs changes.
//
//   Rev 1.19.1.0   Mar 14 2006 14:22:48   RWilby
//Start/End TOIDs change.
//Resolution for 3633: Start/End TOIDs change
//
//   Rev 1.19   Mar 09 2006 16:01:00   RBroddle
//Del 8.1 Code fix exercise - Vantive 4054610 - fix to ensure station info is always displayed.
//
//   Rev 1.18   Feb 23 2006 19:16:58   build
//Automatically merged from branch for stream3129
//
//   Rev 1.17.1.0   Jan 10 2006 15:26:28   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.17   Nov 03 2005 16:09:42   kjosling
//Automatically merged from branch for stream2816
//
//   Rev 1.16.1.0   Oct 19 2005 14:57:50   rhopkins
//TD089 ES020 Image Button Replacement - Replace ScriptableImageButtons and ordinary ImageButtons
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages
//
//   Rev 1.16   Sep 15 2004 09:24:20   jbroome
//Complete re-design of Map tools navigation. Removal of unnecessary stages in screen flow.
//
//   Rev 1.15   Aug 13 2004 11:28:44   jbroome
//IR 1340 - Decoded returned strings from map query to remove HTML character entity references e.g. &amp;
//
//   Rev 1.14   May 26 2004 09:23:42   jgeorge
//Update for TDJourneyParameters changes
//
//   Rev 1.13   Apr 30 2004 13:35:38   jbroome
//DEL 5.4 Merge
//JavaScript Map Control
//
//   Rev 1.12   Apr 01 2004 11:30:10   CHosegood
//If a locations name or common name is empty it is no longer added to the dropdown list
//Resolution for 677: Exception thrown if OK clicked on map page and no locations in drop down
//
//   Rev 1.11   Apr 01 2004 11:05:58   CHosegood
//OK button now disabled if the dropdown is empty.
//Resolution for 677: Exception thrown if OK clicked on map page and no locations in drop down
//
//   Rev 1.10   Mar 26 2004 10:31:06   AWindley
//DEL 5.2 QA Resolution for 681
//
//   Rev 1.9   Mar 18 2004 15:20:18   AWindley
//DEL5.2 Added label text for Screen Reader
//
//   Rev 1.8   Mar 16 2004 16:39:26   CHosegood
//Del 5.2 map changes
//Resolution for 633: Del 5.2 Map Changes
//
//   Rev 1.7   Mar 15 2004 18:18:54   CHosegood
//Del 5.2 Map Changes
//Resolution for 633: Del 5.2 Map Changes
//
//   Rev 1.6   Mar 14 2004 19:12:18   CHosegood
//DEL 5.2 Changes
//Resolution for 633: Del 5.2 Map Changes
//
//   Rev 1.5   Mar 12 2004 16:42:56   COwczarek
//TDLocation object returned by SelectedLocation property now has search type set to Map
//Resolution for 649: Changes to the way ambiguous locations are resolved
//
//   Rev 1.4   Mar 12 2004 15:03:16   CHosegood
//Included OutputMap
//Resolution for 633: Del 5.2 Map Changes
//
//   Rev 1.3   Mar 10 2004 19:03:54   PNorell
//Updated for Map state.
//
//   Rev 1.2   Mar 10 2004 18:23:10   PNorell
//Added stub method for if the journey is return journey or not.
//
//   Rev 1.1   Mar 08 2004 19:07:24   CHosegood
//Added PVCS header
//Resolution for 633: Del 5.2 Map Changes

using System;using TransportDirect.Common.ResourceManager;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.Presentation.InteractiveMapping;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.Web.Support;
using TransportDirect.UserPortal.Web.Adapters;
using Logger = System.Diagnostics.Trace;

namespace TransportDirect.UserPortal.Web.Controls
{

	/// <summary>
	///		Summary description for MapSelectLocationControl.
	/// </summary>
    public partial  class MapSelectLocationControl : TDUserControl
    {


        private bool output = false;
		private bool usesReturnJourney = false;

        #region Public Constants
        public const string STOPS = "STOPS";
        public const string ROADS = "ITN";
        public const string POINTX = "POINTX";
        public const string CLICKPOINT = "ClickPoint";
        public const string X = "X";
        public const string Y = "Y";
        public const int DROPD_TYPE = 0;
        public const int DROPD_X = 1;
        public const int DROPD_Y = 2;
        public const int DROPD_UNIQUEID = 4;
		public const int DROPD_CARPARK_REF = 3;
		public const int DROPD_ISPARKANDRIDE = 4;
		public const int DROPD_CARPARK_LOCATION = 5;
		public const string CARPARKS = "CARPARKS";
		public const string KeyCarRadius1Init = "FindNearestCarParks.Radius1.Initial";
		public const string KeyCarRadius1Max = "FindNearestCarParks.Radius1.Maximum";
		public const string KeyCarRadius2Init = "FindNearestCarParks.Radius2.Initial";
		public const string KeyCarRadius2Max = "FindNearestCarParks.Radius2.Maximum";		
		public const string KeyMaxCarParksReturn = "FindNearestCarParks.NumberCarParksReturned";

		/// <summary>
		/// Start/End TOIDs change: The TOID is stored in the 3rd value for roads.
		/// </summary>
		public const int DROPD_TOID = 3;
		public const int DROPD_NAPTAN = 3;
		public const char SEPARATOR = ',';
		public const char TOID_SEPARATOR = '|';
		public const char CARPARK_SEPERATOR = '(';
		public const int CARPARK_NAME = 0;

        #endregion

        #region Public Properties
		/// <summary>
		/// Sets/Gets if the control should be handling outward or inward journey.
		/// </summary>
		public bool UsesReturnJourney
		{
			get { return usesReturnJourney; }

			set 
			{
				usesReturnJourney = value;
			}
		}

        /// <summary>
        /// Gets the mapstate to be used by the control
        /// </summary>
        public JourneyMapState MapState
        {
            get 
            { 
                if( UsesReturnJourney )
                {
                    return TDSessionManager.Current.ReturnJourneyMapState;
                }
                return TDSessionManager.Current.JourneyMapState;
            }
        }


		/// <summary>
		/// Gets the buttonSelectCancel button
		/// </summary>
		public TDButton ButtonCancel 
		{
			get { return this.buttonSelectCancel; }
		}


        /// <summary>
        /// Gets the buttonOK button
        /// </summary>
        public TDButton ButtonSelectOK 
        {
            get { return this.buttonOK; }
        }


        /// <summary>
        /// Returns the selected naptan
        /// </summary>
        public string SelectedNaptan 
        {
            get 
            {
                // Populate description depending on what has been selected in
                // the location drop down. This should never be null
                ListItem locationItem = dropDownListLocation.SelectedItem;
                string[] values = locationItem.Value.Split(new char[]{SEPARATOR});

				//Start/End TOIDs change: the TOID is stored in the 3rd value for roads.
				//Added if statement to return an empty string for roads.
				string originalType = values[ DROPD_TYPE ];
				if (originalType == ROADS)
					return string.Empty;
				else	
					// Get the naptan of the location
					return values[ DROPD_NAPTAN ];
            }
        }

		/// <summary>
		/// Checks the currently selected item in the list of 
		/// locations, returning true if the current selection is
		/// a car park
		/// </summary>
		/// <returns>boolean, true if current selection is car park</returns>
		public bool SelectedItemIsCarPark()
		{
			// Populate description depending on what has been selected in
			// the location drop down. 
			ListItem locationItem = dropDownListLocation.SelectedItem;
			string[] values = locationItem.Value.Split(new char[]{SEPARATOR});

			// Get the reference of the location
			string originalType = values[ DROPD_TYPE ];
				
			if (originalType == CARPARKS)
			{
				return true;
			}
			else 
			{
				return false;
			}
		}

		/// <summary>
		/// Returns the CarParkReference of the selected car park
		/// </summary>
		public string SelectedCarParkReference 
		{
			get 
			{
				ListItem locationItem = dropDownListLocation.SelectedItem;
				string[] values = locationItem.Value.Split(new char[]{SEPARATOR});

				//the reference is stored in the 4th value for car parks.
				return values[ DROPD_CARPARK_REF ];
			}
		}

		/// <summary>
		/// Checks the selected car park, returning true if part of a 
		/// park and ride scheme
		/// </summary>
		public bool SelectedCarParkIsParkAndRide
		{
			get 
			{
				ListItem locationItem = dropDownListLocation.SelectedItem;
				string[] values = locationItem.Value.Split(new char[]{SEPARATOR});

				if(	String.Compare(values[DROPD_ISPARKANDRIDE], "true", true) == 0) //ignore case
				{
					return true;
				}
				else
				{
					return false;
				}
			}
		}

		/// <summary>
		/// Returns the location of the selected car park as a string
		/// </summary>
		public string SelectedCarParkLocation 
		{
			get 
			{
				ListItem locationItem = dropDownListLocation.SelectedItem;
				string[] values = locationItem.Value.Split(new char[]{SEPARATOR});

				//the reference is stored in the 6th element for car parks.
				return values[ DROPD_CARPARK_LOCATION ];
			}
		}

        /// <summary>
        /// Returns the selected list time from the drop down list
        /// </summary>
        public ListItem SelectedItem 
        {
            get { return this.dropDownListLocation.SelectedItem; }
        }


        /// <summary>
        /// Gets the selected location, if no location is selected null is returned
        /// </summary>
		public TDLocation SelectedLocation
		{
			get
			{
				//car park specific params
				int radiusInit1 = 0;			
				int radiusMax1 = 0;
				int radiusInit2 = 0;
				int radiusMax2 = 0;
				int maxNoCarParks = 10;

				if (FindCarParkHelper.CarParkingAvailable)
				{

					try
					{		
						radiusInit1 = Convert.ToInt32(Properties.Current[KeyCarRadius1Init],CultureInfo.InvariantCulture);
						radiusMax1 =  Convert.ToInt32(Properties.Current[KeyCarRadius1Max],CultureInfo.InvariantCulture);
						radiusInit2 = Convert.ToInt32(Properties.Current[KeyCarRadius2Init], CultureInfo.InvariantCulture);
						radiusMax2 = Convert.ToInt32(Properties.Current[KeyCarRadius2Max], CultureInfo.InvariantCulture);
						maxNoCarParks = Convert.ToInt32(Properties.Current[KeyMaxCarParksReturn], CultureInfo.InvariantCulture);
					}
					catch
					{
						OperationalEvent oe = new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, "Missing/Bad format for FindCarPark Properties");
						Logger.Write(oe);

						throw new TDException(
							"Missing/Bad format for FindCarParkResults Properties",
							true,
							TDExceptionIdentifier.PSMissingProperty);
					}
				}
			
				TDJourneyParameters parameters = TDSessionManager.Current.JourneyParameters;

				// Find the max walking distance - used for finding naptans
				int maxWalkingDistance;
				if (parameters is TDJourneyParametersMulti)
					maxWalkingDistance = ((TDJourneyParametersMulti)parameters).MaxWalkingTime * ((TDJourneyParametersMulti)parameters).WalkingSpeed;
				else
					maxWalkingDistance = 0;

				// Populate description depending on what has been selected in
				// the location drop down. This should never be null
				ListItem locationItem = dropDownListLocation.SelectedItem;

				// Add to cache
				TDLocation location = new TDLocation();
				location.SearchType = SearchType.Map;

				string[] values = locationItem.Value.Split(new char[]{SEPARATOR});
				// Ignore last value which should be unique id - only used to ensure ASP.NET finds correct selected item
			
				// Get the x,y coordinates of the location
				string originalType = values[ DROPD_TYPE ];
				
				if(originalType == CARPARKS)
				{
					// DN90 specifies car park name format should be "<location>, <name> car park/park & ride car park" 
					string [] tempDescription = locationItem.Text.Split(new char[]{CARPARK_SEPERATOR});

					string carParkDescription = values[DROPD_CARPARK_LOCATION] + ", " 
							+ tempDescription[CARPARK_NAME];

					if(SelectedCarParkIsParkAndRide)
					{
						location.Description = carParkDescription + " " 
							+ GetResource("CarParking.ParkAndRide.Suffix");
					}
					else
					{
						location.Description = carParkDescription + " " 
							+ GetResource("CarParking.Suffix");
					}
				}
				else 
				{
					// Use the text as the description
					location.Description = locationItem.Text;					
				}

				// Need coordinates in both int and double
				// Cast to double first (as the coordinates may have decimal points)
				double xDouble = Convert.ToDouble( values[DROPD_X], TDCultureInfo.CurrentCulture.NumberFormat);
				double yDouble = Convert.ToDouble( values[DROPD_Y], TDCultureInfo.CurrentCulture.NumberFormat);
				
				// Now cast the double into an int
				int x = Convert.ToInt32( xDouble, TDCultureInfo.CurrentCulture.NumberFormat);
				int y = Convert.ToInt32( yDouble, TDCultureInfo.CurrentCulture.NumberFormat);

				// Location has the correct easting/northing
				location.GridReference.Easting = x;
				location.GridReference.Northing = y;

				// Set the status of the location to "valid"
				location.Status = TDLocationStatus.Valid;

				// Call IGisQuery.FindNearestStopsAndITNs or FindNearestCarParks
				IGisQuery query = (IGisQuery)TDServiceDiscovery.Current[ServiceDiscoveryKey.GisQuery];

				//Vantive 4054610 fix to ensure station info is always displayed -
				//If NaPTAN selected call FindStopsInGroupForStops then FindNearestITNs, 
				//otherwise just call FindNearestStopsAndITNs as before
				//NB there are also changes in MapLocationControl.buttonFindInformation_Click event handler

				QuerySchema querySchema1;
				QuerySchema querySchema2;
				
				if(originalType == STOPS)
				{
					string dropNaptan = values[DROPD_NAPTAN];
					querySchema1 = query.FindStopsInGroupForStops(new string[] {dropNaptan});
					querySchema2 = query.FindNearestITNs(xDouble, yDouble);
				}
				else if (originalType == ROADS)
				{
					querySchema1 = query.FindNearestStops(x, y, maxWalkingDistance);
					querySchema2 = new QuerySchema();	
				}
				else if (originalType == CARPARKS)
				{
					querySchema1 = query.FindNearestCarParks(x, y, radiusInit1, radiusMax1, maxNoCarParks);
					
					// If the result didnt find any car parks, attempt search again with the second radius
					if (querySchema1.CarParks.Rows.Count <= 0)
					{
						query.FindNearestCarParks(x, y, radiusInit2, radiusMax2, maxNoCarParks);
					}

					querySchema2 = querySchema1;
				}
				else
				{
					querySchema1 = query.FindNearestStopsAndITNs(x, y, maxWalkingDistance);
					querySchema2 = querySchema1;
				}
				
				// Add all NapTANS from STOPS
				ArrayList naptans = new ArrayList(); // holds the naptans
				ArrayList xCoords = new ArrayList(); // holds the x-coordinates
				ArrayList yCoords = new ArrayList(); // holds the y-coordinates
				Hashtable htLocality = new Hashtable();
			
				// Find the most popular locality (this is the NatGazID)
				int mostPopularLocalityCount = 0;

				// Set the "locality" of the location to the most popular NatGazID found
				location.Locality = string.Empty;

				foreach(QuerySchema.StopsRow row in querySchema1.Stops.Rows)
				{
					string rowNaptan = row.atcocode;
					// Add naptan if it does not already exist
					if(!naptans.Contains(rowNaptan ) )
					{
						naptans.Add(rowNaptan);
						xCoords.Add( row.X );
						yCoords.Add( row.Y );
					}
					string locality = row.natgazid;
					// Use 1 as default for the count of localities
					int count = 1;
					if(htLocality.ContainsKey(locality))
					{	
						// If it exists use the old value and increment with one increment the count for this Naptan
						count = Convert.ToInt32(htLocality[locality], TDCultureInfo.CurrentCulture.NumberFormat) + 1;
					}
					htLocality[locality] = count;
					
					if  (count > mostPopularLocalityCount)
					{
						// Very popular locality - lets use it
						location.Locality = locality;
						// register the latest count as the highest result
						mostPopularLocalityCount = count;
					}
				}

				ArrayList itns = new ArrayList();

				ArrayList cParks = new ArrayList();
				
				//Start/End TOIDs change 
				if (originalType == ROADS)
				{
					string[] selectedToids = values[DROPD_TOID].Split(new char[]{TOID_SEPARATOR});
					
					foreach (string selectedToid in values[DROPD_TOID].Split(new char[]{TOID_SEPARATOR}))
					{
						itns.Add(selectedToid);
					}
				}
				else if(originalType == CARPARKS)
				{
					foreach(QuerySchema.CarParksRow row in querySchema2.CarParks.Rows)
					{
						string carParkRef = row.CarParkRef;
						if	(!cParks.Contains(carParkRef))
						{
							cParks.Add(carParkRef);
						}
					}
				}
				else
				{
					// Get all the TOIDS from the ITN table.
					foreach(QuerySchema.ITNRow row in querySchema2.ITN.Rows)
					{
						string toid = row.toid;
						if	(!itns.Contains(toid))
						{
							itns.Add(toid);
						}
					}
				}
	
				// Now add the naptans to the TDLocation
				location.NaPTANs = new TDNaptan[naptans.Count];
				for	(int i=0; i < naptans.Count; i++)
				{
					// Get the naptan
					location.NaPTANs[i] = new TDNaptan();
					location.NaPTANs[i].Naptan = naptans[i].ToString();

					// Get the (x,y) coordinates for this NapTan
					double easting = Convert.ToDouble(xCoords[i] , TDCultureInfo.CurrentCulture.NumberFormat);
					double northing = Convert.ToDouble(yCoords[i], TDCultureInfo.CurrentCulture.NumberFormat);

					OSGridReference gridRef = new OSGridReference();
					gridRef.Easting = Convert.ToInt32( easting, TDCultureInfo.CurrentCulture.NumberFormat);
					gridRef.Northing = Convert.ToInt32( northing, TDCultureInfo.CurrentCulture.NumberFormat);
					location.NaPTANs[i].GridReference = gridRef;
				}

				// Add TOIDs
				location.Toid = (string[])itns.ToArray(typeof(string));

				// Check what the original location type is
				if	(originalType == STOPS)
				{
					location.RequestPlaceType = RequestPlaceType.NaPTAN;
				}
				else
				{
					location.RequestPlaceType = RequestPlaceType.Coordinate;
				}

				// add car park refs
				location.CarParkReferences = (string[])cParks.ToArray(typeof(string));

				if( TDTraceSwitch.TraceVerbose)
				{
					// Log the information received
					StringBuilder toidlist = new StringBuilder();
					StringBuilder naptanlist = new StringBuilder();
					foreach( string road in location.Toid )
					{
						toidlist.Append( road + ", ");
					}
					foreach( TDNaptan napt in location.NaPTANs )
					{
						naptanlist.Append( napt.Naptan + ", " );
					}
					string msg = "SelectLocation : toids = ["+toidlist.ToString()+"] naptan = ["+ naptanlist.ToString()+"]";
					OperationalEvent operationalEvent = new OperationalEvent
						(TDEventCategory.Business, TDTraceLevel.Verbose, msg);
					Logger.Write( operationalEvent );
				}

				return location;
			}
		}


        /// <summary>
        /// Get/Set if this is for the output page
        /// </summary>
        public bool OutputMap
        {
            get { return this.output; }
            set { this.output = value; }
        }

		/// <summary>
		/// Gets the Id for the main div, as if it were a 
		/// server control, which it isn't.
		/// </summary>
		public string GetDivId
		{
			get
			{
				return this.Parent.ClientID + "_mapSelectLocationControl";
			}
		}

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, System.EventArgs e)
        {
            // Put user code to initialize the page here
			buttonOK.ToolTip = GetResource("MapSelectLocationControl.buttonOK.AlternateText");
			buttonOK.Text = GetResource("MapSelectLocationControl.buttonOK.Text");

			buttonSelectCancel.ToolTip = GetResource("MapSelectLocationControl.buttonSelectCancel.AlternateText");
			buttonSelectCancel.Text = GetResource("MapSelectLocationControl.buttonSelectCancel.Text");

			labelSelectInstructions.Text = Global.tdResourceManager.GetString("MapSelectLocationControl.labelSelectInstructions.Text", TDCultureInfo.CurrentUICulture );
			labelSelectLocation.Text = Global.tdResourceManager.GetString("MapSelectLocationControl.labelSelectLocation.Text", TDCultureInfo.CurrentUICulture );
			labelSRLocation.Text = Global.tdResourceManager.GetString("mapSelectLocationControl.labelSRLocation", TDCultureInfo.CurrentUICulture );
                
			labelZoomInstructions.Text = Global.tdResourceManager.GetString("MapSelectLocationControl.labelZoomInstructions.Text", TDCultureInfo.CurrentUICulture );
			labelZoomInstructions2.Text = Global.tdResourceManager.GetString("MapSelectLocationControl.labelZoomInstructions2.Text", TDCultureInfo.CurrentUICulture );

			imageMapError.AlternateText = Global.tdResourceManager.GetString("MapSelectLocationControl.imageMapError.AlternateText", TDCultureInfo.CurrentUICulture );
			imageMapError.ImageUrl = Global.tdResourceManager.GetString("MapSelectLocationControl.imageMapError.ImageUrl", TDCultureInfo.CurrentUICulture );

		}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreRender( EventArgs e ) 
        {
			this.DataBind();
			if ( MapState.SelectEnabled )
            {
                if ( MapState.State == StateEnum.Select )
                {
                    panelInitial.Visible = true;
                    panelResolveLocation.Visible = false;
                    panelZoomLevel.Visible = false;

                    this.buttonOK.Visible = false;
                }
                else if ( MapState.State == StateEnum.Select_Option )
                {
                    panelInitial.Visible = false;
                    panelResolveLocation.Visible = true;
                    panelZoomLevel.Visible = false;

                    this.buttonOK.Visible = dropDownListLocation.Items.Count > 0;
                }
				else // Due to JavaScript functionality, control can be loaded but not displayed
				{
					panelInitial.Visible = false;
					panelResolveLocation.Visible = false;
					panelZoomLevel.Visible = false;
					tableOkCancel.Visible = false;
				}
            }
            else if (MapState.State == StateEnum.Select || MapState.State == StateEnum.Select_Option)
            {
                panelInitial.Visible = false;
                panelResolveLocation.Visible = false;
                panelZoomLevel.Visible = true;

                this.buttonOK.Visible = false;
            }
			else 
			{
				panelInitial.Visible = false;
				panelResolveLocation.Visible = false;
				panelZoomLevel.Visible = false;
				tableOkCancel.Visible = false;
			}

            dropDownListLocation.Visible = dropDownListLocation.Items.Count > 0;

			// Make sure that client display is in sync with server state.
			// Inconsistencies can arise due to use of JavaScript on client.
			AlignClientWithServer();
			//Check for Javascript presence and register client script methods if appropriate
			EnableScriptableObjects();

			base.OnPreRender(e);
        }

        public void MapQuery(object sender, MapQueryEventArgs e)
        {
            if ( MapState.SelectEnabled == false ) 
            {
                return;
            }
		
            MapState.State = StateEnum.Select_Option;
            bool naptanFound = false;

            string stop = Global.tdResourceManager.GetString("MapSelectLocationControl.dropDownListLocation.Stop", TDCultureInfo.CurrentUICulture );
            string road = Global.tdResourceManager.GetString("MapSelectLocationControl.dropDownListLocation.Road", TDCultureInfo.CurrentUICulture );
			string carPark = Global.tdResourceManager.GetString("MapSelectLocationControl.dropDownListLocation.CarPark", TDCultureInfo.CurrentUICulture );

            // Retrive the click points
            string clickPointX = e.QueryDataset.ClickPoint.Rows[0][X].ToString();
            string clickPointY = e.QueryDataset.ClickPoint.Rows[0][Y].ToString();

            // Retrieve all Stops, Roads, PointX and Car Parks from the QuerySchema
            // to populate the drop down.

            // Name for sorting purposes
            ArrayList nameList = new ArrayList();

            // Data for all alternatives	
            Hashtable htData = new Hashtable();
			Hashtable duplicateRoadsToidList = new Hashtable();
			Hashtable duplicateCarParksToidList = new Hashtable();

            // Unique identifier - ensures no values are the same (as can happen with roads)
            int uniqueID = 0;

			if (FindCarParkHelper.CarParkingAvailable)
			{
				foreach(QuerySchema.CarParksRow currentRow in e.QueryDataset.CarParks.Rows)
				{

					if ( !currentRow.CarParkName.Trim().Equals(string.Empty) ) 
					{
						// Get the car park name
						string carParkName = HttpUtility.HtmlDecode(currentRow.CarParkName) + " (" + carPark + ")";

						int easting = Convert.ToInt32( currentRow.Easting, TDCultureInfo.CurrentCulture.NumberFormat );
						int northing = Convert.ToInt32( currentRow.Northing, TDCultureInfo.CurrentCulture.NumberFormat );
						bool isParkAndRide = currentRow.IsParkAndRide;
						string parkAndRideValue = isParkAndRide.ToString();
						string locationValue = currentRow.Location;

						if( htData[carParkName] == null )
						{
							string value = CARPARKS + SEPARATOR + easting + SEPARATOR + northing 
								+ SEPARATOR + currentRow.CarParkRef + SEPARATOR + parkAndRideValue
								+ SEPARATOR + locationValue;

							htData[carParkName] = value;
							nameList.Add(carParkName);
						}
					}
				}
			}

            foreach(QuerySchema.StopsRow currentRow in e.QueryDataset.Stops.Rows)
            {
                if ( !currentRow.commonname.Trim().Equals( string.Empty ) ) 
                {
                    // Get the common name and road namestring in the current row.
					string commonName = HttpUtility.HtmlDecode(currentRow.commonname) + " (" + stop + ")";

                    int x = Convert.ToInt32( currentRow.X, TDCultureInfo.CurrentCulture.NumberFormat );
                    int y = Convert.ToInt32( currentRow.Y, TDCultureInfo.CurrentCulture.NumberFormat );

                    string naptanId = currentRow.atcocode;
					
                    // Determine if a naptan was found
                    naptanFound = naptanFound || (naptanId.Length != 0);

                    if( htData[commonName] == null )
                    {
                        string value = STOPS + SEPARATOR + x + SEPARATOR + y + SEPARATOR + naptanId + SEPARATOR + (uniqueID++);
                        htData[commonName] = value;
                        nameList.Add(commonName);
                    }
                }
            }         
			
			foreach(QuerySchema.ITNRow currentRow in e.QueryDataset.ITN.Rows)
			{
				if (!currentRow.name.Trim().Equals(string.Empty) || !currentRow.toid.Trim().Equals(string.Empty))
				{
					//If the road has a toid but no name, set the name to the local road name defined in the langstring's.
					if (currentRow.name.Trim().Equals(string.Empty))
					{
						currentRow.name = Global.tdResourceManager.GetString("MapSelectLocationControl.localRoadName.Text", TDCultureInfo.CurrentUICulture);
					} 
			
					string roadName = HttpUtility.HtmlDecode(currentRow.name) + " (" + road + ")";
					

					if	(duplicateRoadsToidList[roadName] == null)
					{
						duplicateRoadsToidList[roadName] = currentRow.toid;
					}
				}
			}
			
            foreach(QuerySchema.ITNRow currentRow in e.QueryDataset.ITN.Rows)
            {
                if ( !currentRow.name.Trim().Equals(string.Empty)) 
                {
                    // Get the name of the ITN
                    string roadName = HttpUtility.HtmlDecode(currentRow.name) + " (" + road + ")";
					
					//Start/End TOIDs change: Add the toid to the dropdown data
					string currentToid = currentRow.toid;

                    // Check to see if the item already exists before adding to prevent duplicates
                    if( htData[roadName] == null )
                    {
                        // For ITNs, the x and y is the click point.
                        // Create the new list item.
                        string value = ROADS + SEPARATOR + currentRow.nearestEasting + SEPARATOR + currentRow.nearestNorthing + SEPARATOR + duplicateRoadsToidList[roadName] + SEPARATOR + (uniqueID++);
                        htData[roadName] = value;
                        nameList.Add(roadName);			
                    }
                }
            }

            foreach(QuerySchema.PointXRow currentRow in e.QueryDataset.PointX.Rows)
            {
                if ( !currentRow.name.Trim().Equals( string.Empty ) ) 
                {
                    // Get the common name and road namestring in the current row.
                    string name = HttpUtility.HtmlDecode(currentRow.name);

                    int x = Convert.ToInt32( currentRow.X, TDCultureInfo.CurrentCulture.NumberFormat );
                    int y = Convert.ToInt32( currentRow.Y, TDCultureInfo.CurrentCulture.NumberFormat );

                    // Check to see if the item already exists before adding to prevent duplicates
                    if( htData[name] == null )
                    {
                        // Create the new list item.
                        string value = POINTX + SEPARATOR + x + SEPARATOR + y + SEPARATOR + string.Empty + SEPARATOR + (uniqueID++);
                        htData[name] = value;
                        nameList.Add(name);
                    }
                }
            }

            // Sort it into string order (No consideration taken for other than default culture)
            nameList.Sort();

            // Finally, create an item collection to populate the drop down list with
            ListItem[] listItems = new ListItem[nameList.Count];

            for	(int i=0; i < listItems.Length; i++)
            {
                // Assign each list item the name and the data associated
                string key = (string)nameList[i];
                string value = (string)htData[nameList[i]];
                listItems[i] = new ListItem(key, value);
            }

			if( TDTraceSwitch.TraceVerbose)
			{
				StringBuilder selectionList = new StringBuilder(200); 

				selectionList.Append("Selection list: " + Environment.NewLine);

				foreach (ListItem li in listItems)
				{
					selectionList.Append(li.Text + " " + li.Value + Environment.NewLine);
				}

				Logger.Write(new OperationalEvent
					(TDEventCategory.Business, TDTraceLevel.Verbose, selectionList.ToString()));
			}



            // Populate the drop down list in the MapToolsControl
            dropDownListLocation.Items.Clear();
            dropDownListLocation.Items.AddRange(listItems);

            // If the list is empty, disable the drop down
            dropDownListLocation.Enabled = listItems.Length != 0;

            
        }

		#region JourneyMapState tracking methods
		///<summary>
		/// The EnableClientScript property of a scriptable control is set so that they
		/// output an action attribute when appropriate.
		/// If JavaScript is enabled then appropriate script blocks are added to the page.
		///</summary>
		protected void EnableScriptableObjects()
		{
			// Determine if JavaScript is supported
			bool javaScriptSupported = bool.Parse((string) Session[((TDPage)Page).Javascript_Support]);
			string javaScriptDom = ((TDPage)Page).JavascriptDom;
			ScriptRepository.ScriptRepository scriptRepository = (ScriptRepository.ScriptRepository)TDServiceDiscovery.Current[ServiceDiscoveryKey.ScriptRepository];

			if (javaScriptSupported)
			{
				// Register Client scripts, add script reference, ensure control is present.
				buttonSelectCancel.EnableClientScript = true;
				Page.ClientScript.RegisterStartupScript(typeof(MapSelectLocationControl), buttonSelectCancel.ScriptName, scriptRepository.GetScript(buttonSelectCancel.ScriptName, javaScriptDom));
			}
			else
			{
				// Else, make sure client script is disabled
				buttonSelectCancel.EnableClientScript = false;
			}

		}
		
		///<summary>
		/// Make sure that the controls' style.display attribute corresponds to its current visiblilty.
		/// The client and server need to be kept in sync.
		///</summary>
		private void AlignClientWithServer()
		{
			for (int i=0; i < this.Controls.Count; i++)
			{
				if (this.Controls[i] is WebControl)
				{
					if (this.Controls[i].Visible)
					{
						((WebControl)this.Controls[i]).Attributes.Remove("style");
					}
					else
					{
						this.Controls[i].Visible = true;
						((WebControl)this.Controls[i]).Attributes.Add("style", "display:none");
					}
				}
			}
			
			// buttonOK sits inside table, so need to check seperately
			if (buttonOK.Visible)
				buttonOK.Attributes.Remove("style");
			else
			{
				buttonOK.Visible = true;
				buttonOK.Attributes.Add("style", "display:none");
			}

			string strOutputMap = OutputMap.ToString();
			// Update the Action properties according to control ID
			buttonSelectCancel.Action = "return SelectLocation_Cancel('"+this.Parent.ClientID+"', "+strOutputMap.ToLower()+");";
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
		
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{

		}
		#endregion
	}
}
