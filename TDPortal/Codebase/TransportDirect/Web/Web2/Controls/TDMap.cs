// *********************************************** 
// NAME                 : TDMap.cs 
// AUTHOR               : Kenny Cheung
// DATE CREATED         : 06/08/2003 
// DESCRIPTION			: Wrapper for the ESRI mapping component.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/TDMap.cs-arc  $
//
//   Rev 1.6   Oct 23 2008 15:24:04   pscott
//SCR   5117 Take out line setting date as this is causing problems in map event reporting.
//Resolution for 5117: Map events being incorecctly recorded
//
//   Rev 1.5   Oct 22 2008 15:39:42   pscott
//SCR 5117 - reverse general  fix to missing time in page entry event as it is causing problems. Fix will be put in control responsible      i.e. get rid of  SetStartTime();
//
//   Rev 1.4   Oct 14 2008 14:21:58   mmodi
//Manual merge for stream5014
//
//   Rev 1.3   Jul 04 2008 14:37:54   pscott
//SCR 5039 -  Map Events being written without valid date  
//
//   Rev 1.2.1.0   Jun 20 2008 14:23:34   mmodi
//Added cycle route method
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.2   Mar 31 2008 13:23:14   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:18:10   mturner
//Initial revision.
//
//   Rev 1.18   Oct 06 2006 16:04:44   mturner
//Merge for Stream SCR-4143
//
//   Rev 1.17.1.1   Aug 30 2006 14:53:32   esevern
//changes to ensure car park icon is visible on maps by default and according to user selection
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.17.1.0   Aug 24 2006 16:50:04   esevern
//CarParks visibility changes
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.17   Jun 01 2006 08:28:48   mmodi
//IR4105: Additional method to fire click event with coordinates
//Resolution for 4105: Del 8.2 - Select new location map feature dropdown values are lost
//
//   Rev 1.16   Apr 04 2006 14:19:12   build
//Automatically merged from stream0034
//
//   Rev 1.15.1.0   Mar 29 2006 17:51:42   RWilby
//Updated Map symbol codes
//Resolution for 3715: Map Symbols
//
//   Rev 1.15   Feb 23 2006 16:13:56   halkatib
//Merge of stream3129 enhanced exposed services
//
//   Rev 1.14   Feb 21 2006 12:23:16   build
//Automatically merged from branch for stream0009
//
//   Rev 1.13.2.1   Feb 09 2006 19:03:40   aviitanen
//Fxcop and review changes
//
//   Rev 1.13.2.0   Feb 03 2006 17:14:22   AViitanen
//TD114 High resolution maps: added MapImageProperties class and method for retrieving properties for high res map.
//
//   Rev 1.13   Sep 02 2005 16:08:12   rgreenwood
//IR2745: Added additional index increment
//
//   Rev 1.12   Aug 19 2005 14:08:48   jgeorge
//Automatically merged from branch for stream2572
//
//   Rev 1.11.1.0   Jul 26 2005 16:04:02   rgreenwood
//DD073 Map Details: Added check in ShowIntermediateNodesonMap() method to drop any invalid OSGridReference objects
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.11   May 04 2005 15:08:20   asinclair
//Removed 'ClearAddedSymbols' from ShowIntermediateNodesOnMap method
//
//   Rev 1.10   Apr 27 2005 10:58:28   pcross
//IR2192. Handle exception for running out of circle icons to represent journey nodes.
//Resolution for 2192: Display normal map page, add numbered start and end legs
//
//   Rev 1.9   Apr 26 2005 13:20:06   pcross
//IR2192. Corrections to extended journey handling
//
//   Rev 1.8   Apr 26 2005 10:17:32   pcross
//IR2192. Minor update to node drawing routine.
//Resolution for 2192: Display normal map page, add numbered start and end legs
//
//   Rev 1.7   Apr 22 2005 16:14:22   pcross
//IR2192. New methods to show and hide intermediate nodes on a map.
//Resolution for 2192: Display normal map page, add numbered start and end legs
//
//   Rev 1.6   Mar 18 2005 15:16:44   asinclair
//Added 'ZoomToJunction' - used for DEL 7 Car Costing 
//
//   Rev 1.5   Apr 07 2004 18:42:22   asinclair
//Updated to Save TrafficMaps viewstate
//
//   Rev 1.4   Mar 10 2004 15:53:20   PNorell
//Updated for IR498 to be included in 5.2
//
//   Rev 1.3   Dec 17 2003 16:44:54   kcheung
//Updated so that correct logging can be performed.
//Resolution for 551: Map Events are being logged with incorrect submission time
//
//   Rev 1.2   Dec 17 2003 10:18:28   PNorell
//Prepared for logging changings.
//
//   Rev 1.1   Dec 16 2003 16:45:20   kcheung
//Added commenting and file header.

using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Collections;

using Logger = System.Diagnostics.Trace;
using TransportDirect.Common.Logging;

using TransportDirect.ReportDataProvider.TDPCustomEvents;
using TransportDirect.Presentation.InteractiveMapping;

using TransportDirect.UserPortal.Web.Adapters;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.Web.Support;
using TransportDirect.UserPortal.Web.Controls;
using TransportDirect.Common.ResourceManager;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.Common;


namespace TransportDirect.UserPortal.Web.Controls
{
	/// <summary>
	/// Wrapper for the ESRI mapping component.  Required so that
	/// OnClick event can be exposed. This will allow a programmatic
	/// fire of the click event to be made.
	/// Also required so that correct map event logging can be performed.
	/// </summary>
	[DefaultProperty("Text"), 
	ToolboxData("<{0}:TDMap runat=server></{0}:TDMap>")]
	public class TDMap : Map
	{
		#region Constructor

		/// <summary>
		/// Constructor - sets up the event handlers.
		/// </summary>
		public TDMap()
		{
			// wire up events.
			this.OnMapChangedEvent += new MapChangedEventHandler(this.Map_Changed);	
		}

		#endregion
			
		#region Local variables

		private int scale;
		private Stops previousStops = Stops.NULL; // stored as ints to not clog up the viewstate to much
		private PointX previousPointX = PointX.NULL; // stored as ints to not clog up the viewstate to much
		private bool previousVisibility = false; 
		private bool carParkVisibility = false;

		private DateTime operationStartedDateTime;
		private bool overlay = false;
		private bool panning = false;
		private bool zooming = false;
		private string intermediateNodeImageFilePrefix = "CIRCLE";

		// Used to store the users map click coordinates
		private InputPageState pageState = TDSessionManager.Current.InputPageState;

		#endregion

		#region Definitions
		private const int STOPS = 0;
		private const int POINTX = 1;
		private const int SCALE = 2;
		private const int MAPVIEWSTATE = 3;
		private const int CARPARK = 4;

		/// <summary>
		/// Hashtable keys for all available stops
		/// </summary>
		[Flags]
			private enum Stops
		{
			NULL = 0,
			BCX = 0x01,
			RLY = 0x02,
			TMU = 0x04,
			FTD = 0x08,
			AIR = 0x10,
			TXR = 0x20,
			STR = 0x40,
			MET = 0x80,
			FER = 0x100

		}

		/// <summary>
		/// Hashtable keys for all avilable PointX
		/// </summary>
		[Flags]
			private enum PointX
		{
			NULL = 0,
			ETDR = 0x01,
			HTGH = 0x02,
			CCMH = 0x04,
			YHST = 0x08,
			ODPT = 0x10,
			SPCM = 0x20,
			VSSC = 0x40,
			BTZL = 0x80,
			HSTC = 0x100,
			RECS = 0x200,
			TOUR = 0x400,
			SURG = 0x800,
			HCLC = 0x1000,
			NRCH = 0x2000,
			CPHO = 0x4000,
			PRNR = 0x8000,
			SMIN = 0x10000,
			CLUN = 0x20000,
			RECR = 0x40000,
			POLC = 0x80000,
			LOCS = 0x100000,
			OTGV = 0x200000,
			PBFC = 0x400000,
			RETL = 0x800000
		}

		#endregion

		#region Map Logging & Status tracking

		/// <summary>
		/// Handler for the map changed event.
		/// </summary>
		private void Map_Changed(object sender, MapChangedEventArgs e)
		{
			int nscale = e.MapScale;
			if( scale == 0 )
			{
				// Scale == 0 - map has just started.
				scale = nscale;
				// Log the event.
				MapLogging.Write(MapEventCommandCategory.MapInitialDisplay, scale, operationStartedDateTime);
			}
			if( nscale != scale )
			{
				scale = nscale;
				// Log zoom event if not flagged that it will be logged already.
				if(!zooming)
					MapLogging.Write( MapEventCommandCategory.MapZoom, scale, operationStartedDateTime );
			}
			if( HasStopsChanged || HasPointXChanged || HasCarParkChanged)
			{
				// Set boolean to indicate that the stops/pointx/car parks have changed.
				overlay = true;
			}
			if ( panning )
			{
				// Log the panning event
				MapLogging.Write( MapEventCommandCategory.MapPan, scale, operationStartedDateTime );
				panning = false;
			}
			if ( zooming )
			{
				// Log the zooming event
				MapLogging.Write( MapEventCommandCategory.MapZoom, scale, operationStartedDateTime );
				zooming = false;
			}

		}
		#endregion

		#region Helper methods

		/// <summary>
		/// Method to compare if the STOPS hashtables are equal.
		/// </summary>
		/// <param name="stops">Current stops hashtables</param>
		/// <param name="previous">Previous stops hashtables</param>
		/// <returns>True if equal, false otherwise.</returns>
		private bool CompareStopsEqual(Hashtable stops, Stops previous)
		{
			foreach( int iStops in Enum.GetValues( typeof(Stops) ) )
			{
				string sKey = Enum.GetName( typeof(Stops), iStops );
				bool containsBefore = (previous & (Stops)iStops) != 0;
				bool containsNow = stops.ContainsKey( sKey ) && (bool)stops[sKey];
				if( containsBefore != containsNow )
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>
		/// Method to compare if PointX hashtables are equal.
		/// </summary>
		/// <param name="pointx">Current PointX hashtable.</param>
		/// <param name="previous">Previous PointX hashtable.</param>
		/// <returns>True if equal, false otherwise.</returns>
		private bool ComparePointXEqual(Hashtable pointx, PointX previous)
		{
			foreach( int iPointX in Enum.GetValues( typeof(PointX) ) )
			{
				string pKey = Enum.GetName( typeof(PointX), iPointX );
				bool containsBefore = (previous & (PointX)iPointX) != 0;
				bool containsNow = pointx.ContainsKey( pKey ) && (bool)pointx[pKey];
				if( containsBefore != containsNow )
				{
					return false;
				}
			}
			return true;
		}

		private bool CompareCarParkVisible(bool current, bool previous)
		{
			return (current&&previous || !current && !previous);
		}
		#endregion

		#region Properties 

		public bool HasCarParkChanged
		{
			get
			{
				return !CompareCarParkVisible(this.CarParkLayerVisible, previousVisibility);
			}
		}

		public bool HasPointXChanged
		{
			get 
			{
				return !ComparePointXEqual(this.PointXVisible, previousPointX);
			}
		}
		public bool HasStopsChanged
		{
			get
			{
				return !CompareStopsEqual( this.StopsVisible, previousStops );
			}
		}

		/// <summary>
		/// Get property to read the current map scale.
		/// </summary>
		public int Scale
		{
			get
			{
				return scale;
			}
		}

		/// <summary>
		/// Read/write property for the car park layer visibility
		/// </summary>
		public bool CarParkVisibility
		{
			get
			{
				return carParkVisibility;
			}
			set 
			{
				carParkVisibility = value;
			}
		}

		#endregion

		#region Methods to override those in the Map component to enable logging.

		/// <summary>
		/// Add Start Point
		/// </summary>
		/// <param name="easting">Easting</param>
		/// <param name="northing">Northing</param>
		/// <param name="text">Text</param>
		public new void AddStartPoint( double easting, double northing, string text)
		{
			overlay = true;
			base.AddStartPoint( easting, northing, text );
		}

		/// <summary>
		/// Add end point.
		/// </summary>
		/// <param name="easting">Easting</param>
		/// <param name="northing">Northing</param>
		/// <param name="text">Text</param>
		public new void AddEndPoint( double easting, double northing, string text)
		{
			overlay = true;
			base.AddEndPoint( easting, northing, text );
		}

		/// <summary>
		/// Refreshes the map.
		/// </summary>
		public new void Refresh()
		{
			SetStartTime();

			base.Refresh();
			if( overlay )
			{
				// Log overlay event
				MapLogging.Write( MapEventCommandCategory.MapOverlay, scale, operationStartedDateTime );
				overlay = false;
			}
		}

		/// <summary>
		/// Zooms to the full extent of the UK.
		/// </summary>
		public new void ZoomFull()
		{
			SetStartTime();
			base.ZoomFull();
		}

		/// <summary>
		/// Zoom to the PT Route.
		/// </summary>
		/// <param name="sessionID">Session ID</param>
		/// <param name="routeNumber">Route Number</param>
		public new void ZoomPTRoute(string sessionID, int routeNumber)
		{
			SetStartTime();
			base.ZoomPTRoute(sessionID, routeNumber);
		}

		/// <summary>
		/// Zoom to the Road Route
		/// </summary>
		/// <param name="sessionID">Session ID</param>
		/// <param name="routeNumber">Route Number</param>
		public new void ZoomRoadRoute(string sessionID, int routeNumber)
		{
			SetStartTime();
			base.ZoomRoadRoute(sessionID, routeNumber);
		}

        /// <summary>
        /// Zoom to the Cycle Route
        /// </summary>
        public new void ZoomCycleRoute()
        {
            SetStartTime();
            base.ZoomCycleRoute();
        }

		/// <summary>
		/// Zoom to the specified point
		/// </summary>
		/// <param name="x">X coordinate</param>
		/// <param name="y">Y coordinate</param>
		/// <param name="scaleToZoom">Scale to zoom to.</param>
		public new void ZoomToPoint(double x, double y, int scaleToZoom)
		{
			SetStartTime();
			zooming = true;
			base.ZoomToPoint(x, y, scaleToZoom);
		}

		/// <summary>
		/// Zoom to the specified envelope.
		/// </summary>
		/// <param name="minx">Min X.</param>
		/// <param name="miny">Min Y.</param>
		/// <param name="maxx">Max X.</param>
		/// <param name="maxy">Max Y.</param>
		public new void ZoomToEnvelope(double minx, double miny, double maxx, double maxy)
		{
			SetStartTime();
			base.ZoomToEnvelope(minx, miny, maxx, maxy);
		}

		/// <summary>
		/// Updates the map with the previous request.
		/// </summary>
		public new void ZoomPrevious()
		{
			SetStartTime();
			zooming = true;
			base.ZoomPrevious();
		}

		/// <summary>
		/// Registers the time that this event was called and calls base.
		/// </summary>
		/// <param name="i"></param>
		public new void SetScale(int i)
		{
			// Register time here and call base SetScale
			SetStartTime();			
			base.SetScale(i);
		}

		/// <summary>
		/// Clears all symbol points on the map
		/// </summary>
		public new void ClearAddedSymbols()
		{
			// Register the time.
			SetStartTime();
			overlay = true;
			base.ClearAddedSymbols();
		}

		/// <summary>
		/// Adds a symbol point to the map.
		/// </summary>
		/// <param name="x">X coordinate</param>
		/// <param name="y">Y coordinate</param>
		/// <param name="symbol">Symbol to use (defined web.config)</param>
		/// <param name="name">Name of the (x,y) location</param>
		public new void AddSymbolPoint(double x, double y, string symbol, string name)
		{
			// Register the time.
			SetStartTime();
			overlay = true;
			
			// In try loop in case the icon is not found
			try
			{
				base.AddSymbolPoint(x, y, symbol, name);
			}
			catch (System.Exception)
			{
				// If an exception has occurred then we have no icon to show.
				// Best to simply do nothing in this scenario
			}

		}

		/// <summary>
		/// Zoom to the specified point
		/// </summary>
		/// <param name="toid1">last toid of previous drive section</param>
		/// <param name="toid2">fist toid of current drive section</param>
		/// <param name="scale">Scale to zoom to.</param>
		public new void ZoomToJunction(string toid1, string toid2, int scale)
		{
			SetStartTime();
			zooming = true;
			base.ZoomToJunction(toid1, toid2, scale);
		}

		#endregion

		#region Other Public Methods

		/// <summary>
		/// Manually fires the click event.
		/// </summary>
		public void FireClickEvent()
		{
			OnClick( new ImageClickEventArgs((int)this.Width.Value/2, (int)this.Height.Value/2) );
		}

		/// <summary>
		/// Populate selectnewlocation dropdown list with passed user click coordinates
		/// </summary>summary>
		/// <param name="x">X coordinate</param>
		/// <param name="y">Y coordinate</param>
		public void FireClickEvent(int x, int y)
		{
			OnClick( new ImageClickEventArgs(x, y) );
		}

		/// <summary>
		/// Loads a map state into the current TDMap instance.
		/// </summary>
		/// <param name="o">The map state</param>
		public void InjectViewState(object o)
		{
			if( o != null )
			{
				MapHelper helper = new MapHelper();
				base.LoadViewState( helper.MirrorToPair( o ) ); 
			
				this.Width = (Unit)base.ViewState["Width"];
				this.Height = (Unit)base.ViewState["Height"];
			
				// Need to start it somehow
				this.ZoomToViewState();
			}
		}

		/// <summary>
		/// Retrieves the map state of the current TDMap instance
		/// </summary>
		/// <returns>The map state</returns>
		public object ExtractViewState()
		{
			base.ViewState.Add("Width", this.Width);
			base.ViewState.Add("Height", this.Height);
		
			object ob = base.SaveViewState();
			MapHelper helper = new MapHelper();
			// Create mirror pair
			ob = helper.MirrorToSerPair( ob );

			return ob;
		}

		/// <summary>
		/// Hide icons that number the legs of the journey
		/// </summary>
		/// <param name="outward"></param>
		/// <param name="usingItinerary"></param>
		public void HideIntermediateNodesOnMap()
		{
			this.ClearAddedSymbols();
		}

		/// <summary>
		/// Show the icons that number the legs of the journey
		/// </summary>
		/// <param name="outward"></param>
		/// <param name="usingItinerary"></param>
		public void ShowIntermediateNodesOnMap(bool outward)
		{
			MapHelper helper = new MapHelper();

			// Add a symbol at each intermediate node
			OSGridReference[] journeyIntermediateNodesGridReferencesArray = helper.FindIntermediateNodesGridReferences(outward);

			int nodeCount = journeyIntermediateNodesGridReferencesArray.Length;
			if(nodeCount==0)
			{
				// No nodes to display - probably a road map
			}
			else
			{
				int index = 0;

				foreach (OSGridReference osGridReference in journeyIntermediateNodesGridReferencesArray)
				{
					if (osGridReference.Easting > 0 && osGridReference.Northing > 0)
					{
						if (journeyIntermediateNodesGridReferencesArray[index] != null)
						{

							// Get OSGR values for each change
							string iconName;
							int iconNumber;

							iconNumber = index + 1;


							iconName = intermediateNodeImageFilePrefix + iconNumber.ToString(TDCultureInfo.InvariantCulture.NumberFormat);

							// Add symbols to map
							this.AddSymbolPoint(osGridReference.Easting, osGridReference.Northing, iconName, string.Empty);
						}
						else
						{
							break;
						}
					
					}
					else
					{
						// Skip the addition of this icon
						index++;
						continue;
					}

					index++;

				}
			}

		}

		#endregion

		#region Event Handling

		/// <summary>
		/// Overrides the base OnClick event.  
		/// </summary>
		/// <param name="icea"></param>
		protected override void OnClick( ImageClickEventArgs icea )
		{
			// Set the operation start time
			SetStartTime();

			// Save map user click points to session
			pageState.MapClickPointX = icea.X;
			pageState.MapClickPointY = icea.Y;

			if( ClickMode == Map.ClickModeType.ReCentre )
			{
				panning = true;
			}

			base.OnClick( icea );

		}

		#endregion

		#region Viewstate code

		/// <summary>
		/// Loads the view state.
		/// </summary>
		protected override void LoadViewState(object savedState) 
		{
			if( savedState != null )
			{
				object[] arr = (object[])savedState;
				previousStops = (Stops)arr[STOPS];
				previousPointX = (PointX)arr[POINTX];
				scale = (int)arr[SCALE];
				base.LoadViewState(arr[MAPVIEWSTATE]);
				previousVisibility = (bool)arr[CARPARK];
			}
		}

		/// <summary>
		/// Saves the viewstate.
		/// </summary>
		protected override object SaveViewState()
		{
			// Reset old values
			previousPointX = PointX.NULL;
			previousStops = Stops.NULL;
			previousVisibility = false;

			if( this.IsStarted() )
			{
				Hashtable stops = this.StopsVisible;
				Hashtable pointx = this.PointXVisible;

				// Register stops
				foreach( int iStops in Enum.GetValues( typeof(Stops) ) )
				{
					string sKey = Enum.GetName( typeof(Stops), iStops );
					bool containsNow = stops.ContainsKey( sKey ) && (bool)stops[sKey];
					if( containsNow )
					{
						previousStops = previousStops | (Stops)iStops;
					}
				}


				// Register pointX
				foreach( int iPointX in Enum.GetValues( typeof(PointX) ) )
				{
					string pKey = Enum.GetName( typeof(PointX), iPointX );
					bool containsNow = pointx.ContainsKey( pKey ) && (bool)pointx[pKey];
					if( containsNow )
					{
						previousPointX = previousPointX | (PointX)iPointX;
					}
				}

				// register car park
				if(stops.Contains("CPK"))
				{
					previousVisibility = true;					
				}
			}
			// Save state in arr
			object[] arr = new object[5];
			arr[STOPS] = previousStops;
			arr[POINTX] = previousPointX;
			arr[SCALE] = scale;
			arr[MAPVIEWSTATE] = base.SaveViewState();
			arr[CARPARK] = previousVisibility;
			return arr;
		}
		#endregion

		#region Private Methods

		/// <summary>
		/// Sets the start time of the operation.
		/// </summary>
		private void SetStartTime()
		{
			operationStartedDateTime = DateTime.Now;
		}

		#endregion
	}

	#region Serialization class
	/// <summary>
	/// Class for serialising Pair information.
	/// Ugly hack because Microsoft can't design a proper viewstate-pair class that is serialisable
	/// </summary>
	[Serializable]
	public class SerPair
	{
		/// <summary>
		/// The first object
		/// </summary>
		private object first;
		/// <summary>
		/// The second object
		/// </summary>
		private object second;

		/// <summary>
		/// Gets/sets the first object
		/// </summary>
		public object First
		{
			set { first = value; }
			get { return first; }
		}

		/// <summary>
		/// Gets/sets the second object
		/// </summary>
		public object Second 
		{
			set { second = value; }
			get { return second; }
		}

		/// <summary>
		/// Constructs a serialisable pair object
		/// </summary>
		/// <param name="first">The first object</param>
		/// <param name="second">The second object</param>
		public SerPair(object first, object second)
		{
			this.first = first;
			this.second = second;
		}
	}
	#endregion
}