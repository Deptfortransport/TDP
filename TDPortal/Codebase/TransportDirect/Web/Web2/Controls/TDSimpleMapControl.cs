// *********************************************** 
// NAME                 : TDSimpleMapControl.cs 
// AUTHOR               : Kenny Cheung
// DATE CREATED         : 18/12/2003 
// DESCRIPTION			: Control that inherits
// from the ESRI SimpleMap component.
// Required so that correct logging can be performed.
// ************************************************ 

using System;using TransportDirect.Common.ResourceManager;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;

using TransportDirect.ReportDataProvider.TDPCustomEvents;
using TransportDirect.Presentation.InteractiveMapping;

using TransportDirect.UserPortal.Web.Adapters;

namespace TransportDirect.UserPortal.Web.Controls
{
	/// <summary>
	/// Control that inhertits from the ESRI SimpleMap
	/// component.
	/// </summary>
	[DefaultProperty("Text"), 
		ToolboxData("<{0}:TDSimpleMapControl runat=server></{0}:TDSimpleMapControl>")]
	public class TDSimpleMapControl : SimpleMap
	{
		#region Local Variables

		// Indicates when the map event was initiated for event logging purposes.
		DateTime operationStartedDateTime;
		private int scale;
		private bool panning = false;
		private bool zooming = false;
		private bool overlay = true;
		private DateTime currentTravelDate;
		private int currentTravelPeriod;

		#endregion

		#region Definitions

		private const int SCALE = 0;
		private const int MAPVIEWSTATE = 1;
		private const int CURRENTTRAVELDATE = 2;
		private const int CURRENTTRAVELPERIOD = 3;

		#endregion

		#region Constructor

		/// <summary>
		/// Constructor - sets up event handling.
		/// </summary>
		public TDSimpleMapControl()
		{
			// wire up events.
			this.OnMapChangedEvent += new MapChangedEventHandler(this.Map_Changed);
		}

		#endregion

		#region Event Handling
	
		/// <summary>
		/// Overrides the base OnClick event.  
		/// </summary>
		/// <param name="icea"></param>
		protected override void OnClick( ImageClickEventArgs icea )
		{
			if( ClickMode == SimpleMap.ClickModeType.ReCentre )
			{
				// Indicate that panning has been performed so that logging
				// can be performed on the Map Changed Event Handler.
				panning = true;
			}

			// Set the operation start time
			SetStartTime();

			// Call base to perform the actual operation.
			base.OnClick( icea );

		}

		/// <summary>
		/// Handler for the map changed event. Performs logging.
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

				// Zoom event has just occured.
				if(!zooming)
					MapLogging.Write( MapEventCommandCategory.MapZoom, scale, operationStartedDateTime );
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

		#region Other Public Methods

		/// <summary>
		/// Manually fires the click event.
		/// </summary>
		public void FireClickEvent()
		{
			OnClick( new ImageClickEventArgs((int)this.Width.Value/2, (int)this.Height.Value/2) );
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

		#region Viewstate code

		/// <summary>
		/// ADD DOCO!!!
		/// </summary>
		/// <param name="o"></param>
		public void InjectViewState(object o)
		{
			if( o != null )
			{
				MapHelper helper = new MapHelper();
				base.LoadViewState( helper.MirrorToPair( o ) ); 

				// Need to start it somehow
				this.ZoomToViewState();
			}
		}

		/// <summary>
		/// ADD DOCO!!!
		/// </summary>
		/// <returns></returns>
		public object ExtractViewState()
		{
			object ob = base.SaveViewState();
			MapHelper helper = new MapHelper();
			// Create mirror pair
			ob = helper.MirrorToSerPair( ob );

			return ob;
		}

		/// <summary>
		/// Loads the view state.
		/// </summary>
		protected override void LoadViewState(object savedState) 
		{
			if( savedState != null )
			{
				object[] arr = (object[])savedState;
				scale = (int)arr[SCALE];
				base.LoadViewState(arr[MAPVIEWSTATE]);
				currentTravelDate = (DateTime)arr[CURRENTTRAVELDATE];
				currentTravelPeriod = (int)arr[CURRENTTRAVELPERIOD];
			}
		}

		/// <summary>
		/// Saves the viewstate.
		/// </summary>
		protected override object SaveViewState()
		{
			// Save state in arr
			object[] arr = new object[4];
			arr[SCALE] = scale;
			arr[MAPVIEWSTATE] = base.SaveViewState();
			arr[CURRENTTRAVELDATE] = currentTravelDate;
			arr[CURRENTTRAVELPERIOD] = currentTravelPeriod;
			return arr;
		}


		#endregion

		#region Override properties/methods in parent to allow logging

		/// <summary>
		/// Zooms to the full extent of the UK.
		/// </summary>
		public new void ZoomFull()
		{
			// Register the time this method was called and call base.
			SetStartTime();
			base.ZoomFull();
		}

		/// <summary>
		/// Sets the scale of the map to the given scale.
		/// </summary>
		/// <param name="theScale">Scale to set the map to.</param>
		public new void SetScale(int theScale)
		{
			// Register the time this method was called and call base.
			SetStartTime();
			base.SetScale(theScale);
		}

		public new void ZoomPrevious()
		{
			// Register the time this method was called and call base.
			SetStartTime();
			zooming = true;
			base.ZoomPrevious();
		}

		/// <summary>
		/// Get/Set property to set the time period for the congestion map.
		/// </summary>
		public new int TimePeriod
		{
			get
			{
				return base.TimePeriod;
			}
			set
			{
				// Set the "overlay" flag to true if traveltime has changed.
				if( currentTravelPeriod != value )
				{
					overlay = true;
					currentTravelPeriod = value;
				}

				base.TimePeriod = value;
			}
		}

		/// <summary>
		/// Set property to set the travel date for the congestion map.
		/// </summary>
		public new DateTime TravelDate
		{
			set
			{
				// Set the "overlay" flag depending on whether
				// the new value is different from the previous value.
				if( currentTravelDate != value )
				{
					overlay = true;
					currentTravelDate = value;
				}

				base.TravelDate = value;
			}
		}

		// Refreshes the map.
		public new void Refresh()
		{
			// Register datetime, call base Refresh and write log entry if overlay.
			SetStartTime();
			base.Refresh();

			if( overlay )
			{
				// Log the overlay event
				MapLogging.Write( MapEventCommandCategory.MapOverlay, scale, operationStartedDateTime );
				overlay = false;
			}
		}

		/// <summary>
		/// Zooms to the specified point.
		/// </summary>
		/// <param name="easting">Easting</param>
		/// <param name="northing">Northing</param>
		/// <param name="zoomScale">Scale to zoom to.</param>
		public new void ZoomToPoint(double easting, double northing, int zoomScale)
		{
			// Register datetime.
			SetStartTime();
			base.ZoomToPoint(easting, northing, zoomScale);
		}

		#endregion
	}
}
