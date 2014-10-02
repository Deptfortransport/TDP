// *********************************************** 
// NAME                 : MapZoomControl.ascx.cs 
// AUTHOR               : Peter Norell
// DATE CREATED         : 05/12/2003 
// DESCRIPTION			: Zoom control for the map component
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/MapZoomControl.ascx.cs-arc  $
//
//   Rev 1.2   Mar 31 2008 13:22:10   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:16:42   mturner
//Initial revision.
//
//   Rev 1.24   Jul 07 2006 13:11:18   rbroddle
//Resoloution for Vantive 4227195 / IR 4130 -  "Zoom Out" button on traffic maps has an incorrect minimum setting.
//Resolution for 4130: "Zoom Out" button on traffic maps has an incorrect minimum setting
//
//   Rev 1.23   May 26 2006 15:15:18   mmodi
//Added property to hide the Previous map button for Live Travel page
//Resolution for 4099: Del 8.2 - Live Travel Previous map button does nothing
//
//   Rev 1.22   Feb 23 2006 16:13:00   halkatib
//Merge of stream3129 enhanced exposed services
//
//   Rev 1.21   Nov 25 2005 14:59:32   ralonso
//Replace 'Find New Map' text with 'New Search'
//Resolution for 3078: UEE: Button text is incorrect for Find New Map on Location and Traffic Maps page
//
//   Rev 1.20   Nov 15 2005 14:25:14   RGriffith
//More UEE Button replacement Code Review suggested changes
//
//   Rev 1.19   Nov 15 2005 14:15:04   RGriffith
//UEE Button replacement Code Review suggested changes
//
//   Rev 1.18   Nov 03 2005 17:06:58   kjosling
//Automatically merged from branch for stream2816
//
//   Rev 1.17.1.1   Oct 21 2005 15:28:28   ralonso
//TD089 ES020 image button replacement
//
//   Rev 1.17.1.0   Oct 12 2005 15:37:48   ralonso
//TD089 ES020 image button replacement
//
//   Rev 1.17   Sep 13 2005 11:31:56   asinclair
//Added property to check if Help button should be displayed
//Resolution for 2723: DN018: Help Buttons and Instructional Text on Live Travel page
//
//   Rev 1.16   Jul 23 2004 11:48:22   passuied
//Changes to add GetResource Method in TDPage and TDUserControl to ease access to resources. Also removal of local GetResouce in controls and pages
//
//   Rev 1.15   Jul 12 2004 18:27:40   JHaydock
//DEL 5.4.7 Merge: IR 1074
//
//   Rev 1.14   Jul 12 2004 14:09:30   jbroome
//IR 804 - SetScale method causing problem with MapControl view state.
//
//   Rev 1.13   May 18 2004 13:26:56   jbroome
//IR861 Resolving issue of Zoom Control levels and Map Symbols.
//
//   Rev 1.12   Apr 30 2004 13:37:10   jbroome
//DEL 5.4 Merge
//JavaScript Map Control
//
//   Rev 1.11   Mar 15 2004 18:18:52   CHosegood
//Del 5.2 Map Changes
//Resolution for 633: Del 5.2 Map Changes
//
//   Rev 1.10   Mar 12 2004 09:45:40   CHosegood
//Added ScrollToHelp property of HelpControl to false.
//Resolution for 633: Del 5.2 Map Changes
//
//   Rev 1.9   Mar 10 2004 15:20:40   CHosegood
//Moved InitialsePanel into PageLoad instead of InitialiseComponent
//
//   Rev 1.8   Mar 09 2004 13:37:42   PNorell
//Added property.
//
//   Rev 1.7   Mar 09 2004 11:56:08   CHosegood
//Help Label set to MapZoomHelpLabelControl
//Resolution for 633: Del 5.2 Map Changes
//
//   Rev 1.6   Mar 08 2004 19:03:48   CHosegood
//Added help control and html formatting
//Resolution for 633: Del 5.2 Map Changes
//
//   Rev 1.5   Mar 01 2004 15:30:34   CHosegood
//DEL5.2 Changes.  Checked in for Integration and not yet complete
//Resolution for 633: Del 5.2 Map Changes
//
//   Rev 1.4   Jan 20 2004 11:03:48   PNorell
//Updated map according to 5.2.
//
//   Rev 1.3   Dec 19 2003 10:36:56   kcheung
//Added commenting.
//
//   Rev 1.2   Dec 15 2003 16:54:32   kcheung
//Alt text for map zoom
//
//   Rev 1.1   Dec 11 2003 10:21:38   kcheung
//Journey Planner Location Map Del 5.1 Update
//
//   Rev 1.0   Dec 10 2003 11:56:02   PNorell
//Initial Revision
using System;
using TransportDirect.Common.ResourceManager;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.PropertyService.Properties;

using TransportDirect.Presentation.InteractiveMapping;

using TransportDirect.UserPortal.SessionManager;

using TransportDirect.Web.Support;

using Logger = System.Diagnostics.Trace;

namespace TransportDirect.UserPortal.Web.Controls
{

	/// <summary>
	///	Zoom control for the map component
	/// </summary>
	public partial  class MapZoomControl : TDUserControl
	{
		#region Definitions and constants
		private const string RES_URL_SCALESELECTED = "MapToolsControl.imageScaleSelected";
		private const string RES_URL_SCALEUNSELECTED = "MapToolsControl.imageScale";
		private const string RES_ALT_SCALE = "MapToolsControl.imageButtonZoom{0}.AlternateText";

		private const string RES_TXT_ZOOMPLUS = "MapToolsControl.buttonMapPlus.Text";
		private const string RES_TOOLTIP_ZOOMPLUS = "MapToolsControl.buttonZoomIn.ToolTip";
		private const string RES_TXT_ZOOMMINUS = "MapToolsControl.buttonMapMinus.Text";
		private const string RES_TOOLTIP_ZOOMMINUS = "MapToolsControl.buttonZoomOut.ToolTip";

		private const string RES_TOOLTIP_ZOOMIN = "MapToolsControl.buttonZoomIn.ToolTip";
		private const string RES_TXT_ZOOMIN = "MapToolsControl.buttonZoomIn.Text";
		private const string RES_TXT_ZOOMOUT = "MapToolsControl.buttonZoomOut.Text";
		private const string RES_TOOLTIP_ZOOMOUT = "MapToolsControl.buttonZoomOut.ToolTip";

		
		private const string RES_TXT_NEWLOC = "MapToolsControl.buttonFindNewMap.Text";
		
		private const string RES_ALT_NEWLOC = "MapToolsControl.buttonFindNewMap.AlternateText";
		private const string RES_ALT_DISABLEDNEWLOC = "MapToolsControl.buttonDisabledFindNewMap.AlternateText";
		
		
		private const string RES_TXT_PREVIOUS = "MapToolsControl.buttonPreviousMap.Text";
		private const string RES_TOOLTIP_PREVIOUS = "MapToolsControl.buttonPreviousMap.ToolTip";

		private const string RES_TXT_INST_QUERY = "MapToolsControl.labelZoomControlInstructions.Query.Text";
		private const string RES_TXT_INST_ZOOM = "MapToolsControl.labelZoomControlInstructions.ZoomIn.Text";

		private const string DEFAULT_ZOOM_DEFINITION = "MappingComponent";

		private readonly string[] zoomDefinitions = {
														"Web.{0}.ZoomLevelOne",
														"Web.{0}.ZoomLevelTwo",
														"Web.{0}.ZoomLevelThree",
														"Web.{0}.ZoomLevelFour",
														"Web.{0}.ZoomLevelFive",
														"Web.{0}.ZoomLevelSix",
														"Web.{0}.ZoomLevelSeven",
														"Web.{0}.ZoomLevelEight",
														"Web.{0}.ZoomLevelNine",
														"Web.{0}.ZoomLevelTen",
														"Web.{0}.ZoomLevelEleven",
														"Web.{0}.ZoomLevelTwelve",
														"Web.{0}.ZoomLevelThirteen"
													};
		private const string UNSELECTED = "U";
		private const string SELECTED = "S";
		private const string SKIP = "Z";

		private bool displayLowZoomLevelBox = true;

		private bool displayMapHelp = true;

		#endregion

		#region Automatical generated components


		#endregion

		#region Event declarations
		public event ZoomLevelChangedEventHandler Zoom;
		public event EventHandler ZoomIn;
		public event EventHandler ZoomOut;
		public event EventHandler PreviousView;
		public event EventHandler FindNewMap;
		#endregion

		#region Page Load
		protected void Page_Load(object sender, System.EventArgs e)
		{
			InitialisePanelComponent();
			LoadResources();
		}

		#endregion

		#region Image/Text Loading from Resource Manager

		private void LoadResources()
		{

			//Text for tdbuttons
			buttonZoomIn.Text = GetResource(RES_TXT_ZOOMIN);

			buttonZoomOut.Text = GetResource(RES_TXT_ZOOMOUT);
			
			buttonFindNewMap.Text	= GetResource(RES_TXT_NEWLOC);

//			imageDisabledNewMap.ImageUrl = GetResource(RES_URL_DISABLEDNEWLOC);
			
			buttonPreviousView.Text = GetResource(RES_TXT_PREVIOUS);;
		
			
//			imageDisabledNewMap.AlternateText = GetResource(RES_ALT_DISABLEDNEWLOC);

			labelZoomControlInstructions1.Text = GetResource(RES_TXT_INST_ZOOM);
			labelZoomControlInstructions2.Text = GetResource(RES_TXT_INST_QUERY);

			MapZoomControlHelp.Visible = displayMapHelp;
		}

		#endregion

		public void MapModeChanged(object sender, ModeChangedEventArgs e)
		{
			//Dependent on what ClickModeType the map is in display what
			//happens if the user clicks on the map under the
			//zoom icons

			// Make sure that both labels are always present so 
			// that they can be accessed via JavaScript routines.
			this.labelZoomControlInstructions1.Visible = true;
			this.labelZoomControlInstructions2.Visible = true;

			if ( e.MapMode == Map.ClickModeType.Query ) 
			{
				this.labelZoomControlInstructions1.Attributes.Add("style", "display:none");
				this.labelZoomControlInstructions2.Attributes.Remove("style");
			} 
			else if ( e.MapMode == Map.ClickModeType.ZoomIn ) 
			{
				this.labelZoomControlInstructions1.Attributes.Remove("style");
				this.labelZoomControlInstructions2.Attributes.Add("style", "display:none");
			} 
			else 
			{
				//This should not happen but just incase blank out any text displayed.
				this.labelZoomControlInstructions1.Attributes.Add("style", "display:none");
				this.labelZoomControlInstructions2.Attributes.Add("style", "display:none");
			}
		}

		#region Variable declarations
		private ImageButton[] imbCache;
		private string zoomSetting = DEFAULT_ZOOM_DEFINITION;
		#endregion

		/// <summary>
		/// Extra event subscription
		/// </summary>
		private void ExtraWiringEvents()
		{
			this.buttonZoomIn.Click  += new EventHandler(this.buttonZoomIn_Click);
			this.buttonPreviousView.Click += new EventHandler(this.buttonPreviousView_Click);
			this.buttonZoomOut.Click += new EventHandler(this.buttonZoomOut_Click);
			this.buttonFindNewMap.Click += new EventHandler(this.buttonFindNewMap_Click);
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			ExtraWiringEvents();
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

		#region Public properties

		public string HelpLabel
		{
			get 
			{
				return this.MapZoomControlHelp.HelpLabel;
			}
			set 
			{
				this.MapZoomControlHelp.HelpLabel = value;
			}
		}

		public string ZoomSetting
		{
			get { return zoomSetting; }
			set { this.zoomSetting = value; }
		}

		/// <summary>
		/// Get/Set property to display/hide the find new map button.
		/// </summary>
		public bool EnableFindNewMap
		{
			get
			{
				return this.buttonFindNewMap.Visible;
			}
			set
			{
				this.buttonFindNewMap.Visible = value;
				this.buttonFindNewMap.Enabled = true;
			}
		}

		/// <summary>
		/// Get/Set property to enable/disable the find new map button.
		/// </summary>
		public bool DisplayFindNewMap
		{
			get
			{
				return this.buttonFindNewMap.Visible;
			}
			set
			{
				this.buttonFindNewMap.Visible = value;
				this.buttonFindNewMap.Enabled = value;
			}

		}

		/// <summary>
		/// Get/Set property to enable/disable the visibility of the div box round the zoom panel lower zoom levels.
		/// </summary>
		public bool DisplayLowZoomLevelBox
		{
			get
			{
				return displayLowZoomLevelBox;
			}
			set
			{
				displayLowZoomLevelBox = value;
			}
		}

		/// <summary>
		/// Get/Set property to enable/disable the visibility of Help button in the zoom controls.
		/// </summary>
		public bool DisplayMapHelp
		{
			get
			{
				return displayMapHelp;
			}
			set
			{
				displayMapHelp = value;
			}
		}

		/// <summary>
		/// Get/Set property to enable/disable the previous view button.
		/// </summary>
		public bool DisplayPreviousView
		{
			get
			{
				return this.buttonPreviousView.Visible;
			}
			set
			{
				this.buttonPreviousView.Visible = value;
				this.buttonPreviousView.Enabled = value;
			}

		}

		#endregion

		#region Public methods
		public void ScaleChange(int newscale, bool naptanInRange)
		{

			if (imbCache != null)
			{
				ImageButton selectedButton = null;
				int max = imbCache.Length;
				// Start on 1 and only look towards the lower scale
				for(int i = 0; i < max; i ++)
				{
					ImageButton selBut =imbCache[i];

					selBut.CommandArgument = UNSELECTED;
					selBut.ImageUrl = GetResource( RES_URL_SCALEUNSELECTED );

					if( selectedButton == null )
					{
						int currentZoom = ConvertToInt( selBut.CommandName );
						if( newscale == currentZoom )
						{
							selectedButton = selBut;
							continue;
						}
						if( newscale > currentZoom )
						{
							continue;
						}
						ImageButton prevBut = imbCache[ i - 1];

						int previousZoom = ConvertToInt( prevBut.CommandName );
						// If somehow previous zoom is larger than the scale -> first button to be selected
						if( previousZoom > newscale )
						{
							selectedButton = prevBut;
						}
						else if( i > 0 )
						{

							// Advanced calculation
							int treshold = (currentZoom - previousZoom) / 2;
							int compare = newscale - previousZoom;
							if( treshold > compare )
							{
								// IR861 If previous button is zoom level 5, care should be taken.
								// Only select this if naptanInRange, so can display Map Symbols.
								if (prevBut.ID == zoomDefinitions[4] && !naptanInRange)
								{
									selectedButton = selBut;
								}
								else
								{
									selectedButton = prevBut;
								}
							}
							else
							{
								selectedButton = selBut;
							}
						}
					}
				}

				if( selectedButton == null )
				{
					// Last button to be selected
					selectedButton = imbCache[ max - 1];
				}

				selectedButton.CommandArgument = SELECTED;
				selectedButton.ImageUrl = GetResource( RES_URL_SCALESELECTED );
			}

		}
		#endregion

		#region Private methods
		/// <summary>
		/// Initialises the panel with all the zoom buttons
		/// </summary>
		private void InitialisePanelComponent()
		{
			/// TODO: BITHIR Alt text for these
			// Put user code to initialize the page here
			panelZoomButtons.Controls.Clear();        //CWH
			HtmlTable table = new HtmlTable();
			HtmlTableRow row = new HtmlTableRow();
			HtmlTableCell cell;

			TDButton zoomPlus = new TDButton();
			zoomPlus.ID = "ZoomPlus";
			zoomPlus.CommandName = SKIP;
			zoomPlus.Click += new EventHandler( ZoomPlus );
			zoomPlus.Text = GetResource( RES_TXT_ZOOMPLUS);
			zoomPlus.ToolTip = GetResource (RES_TOOLTIP_ZOOMPLUS);
			
			cell = new HtmlTableCell();
			cell.NoWrap= true;
			cell.Controls.Add( zoomPlus );
			cell.Controls.Add( CreateSpace( 4 ) );
			row.Cells.Add( cell );
			imbCache = new ImageButton[ zoomDefinitions.Length ];

			//Create div border
			HtmlGenericControl borderDiv = new HtmlGenericControl("div");
			borderDiv.Attributes.Add("id", "mapiconshighlight");
			if (displayLowZoomLevelBox)
			{
				cell = new HtmlTableCell();
				cell.NoWrap = true;
				cell.Controls.Add( borderDiv );
				row.Cells.Add( cell );
			}

			//Create the non div border
			HtmlGenericControl nonBorderDiv = new HtmlGenericControl("div");
			nonBorderDiv.Attributes.Add("id", "mapicons");
			cell = new HtmlTableCell();
			cell.NoWrap = true;
			cell.Controls.Add( nonBorderDiv );
			row.Cells.Add( cell );

			for(int i = 0; i < zoomDefinitions.Length; i++)
			{
				string zoomDef = string.Format( zoomDefinitions[i], zoomSetting );
				ImageButton im = new ImageButton();
				im.ImageUrl = GetResource(RES_URL_SCALEUNSELECTED);
				im.ID = zoomDefinitions[i];
				im.CommandName = Properties.Current[ zoomDef ];
				im.CommandArgument = UNSELECTED;
				im.AlternateText = GetResource( string.Format( RES_ALT_SCALE , (i+1)));
				im.Click += new System.Web.UI.ImageClickEventHandler( ZoomByLevel );
				if (displayLowZoomLevelBox && i <= 4)
				{
					borderDiv.Controls.Add(im);
					// Need to add &nbsp; here as well to have some _space_ between the icons
					borderDiv.Controls.Add( CreateSpace( 4 ) );
				} 
				else 
				{
					nonBorderDiv.Controls.Add( im );
					// Need to add &nbsp; here as well to have some _space_ between the icons
					nonBorderDiv.Controls.Add( CreateSpace( 4 ) );
				}
				// For caching purposes
				imbCache[i] = im;
			}

			
			TDButton zoomMinus = new TDButton();
			zoomMinus.ID = "ZoomMinus";
			zoomMinus.CommandName = SKIP;
			zoomMinus.Click += new EventHandler( ZoomMinus );
			zoomMinus.Text = GetResource( RES_TXT_ZOOMMINUS);
			zoomMinus.ToolTip = GetResource (RES_TOOLTIP_ZOOMMINUS);
			
			cell = new HtmlTableCell();
			cell.NoWrap = true;
			cell.Controls.Add( zoomMinus );
			row.Cells.Add( cell );

			table.Rows.Add( row );
			panelZoomButtons.Controls.Add( table );

		}

		/// <summary>
		/// Event handler for the zoom buttons
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ZoomByLevel( object sender, System.Web.UI.ImageClickEventArgs e)
		{
			// Find correct zoom level
			string zoomLevel = ((ImageButton)sender).CommandName;
			// Fire event
			OnZoom( ConvertToInt( zoomLevel ) );
		}

		/// <summary>
		/// Event handler for the zoom +
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ZoomPlus( object sender, EventArgs e)
		{
			ImageButton previous = null;
			int zoomLevel = 0;
			for( int i = 0; i < imbCache.Length; i++)
			{
				ImageButton im = imbCache[i];
				
				if( im.CommandArgument == SELECTED && previous != null)
				{
					im.ImageUrl = GetResource( RES_URL_SCALEUNSELECTED );
					im.CommandArgument = UNSELECTED;
					zoomLevel =  ConvertToInt( previous.CommandName );
					break;
				}
				previous = im;
			}
			if( previous == null )
			{
				// No update needed already on correct zoom level (ie first)
				return;
			}
																 
			// Fire event
			OnZoom( ConvertToInt( zoomLevel ) );
		}

		/// <summary>
		/// Event handler for the zoom -
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ZoomMinus( object sender, EventArgs e)
		{
			ImageButton next;
			int zoomLevel = 0;
			for( int i = 0; i < imbCache.Length; i++)
			{
				ImageButton im = imbCache[i];

				if( im.CommandArgument == SELECTED )
				{
					// Next is two steps away as control
					int j = i + 1;
					if( j == imbCache.Length)
					{
						// Last zoom icon selected - no more up to go
						return;
					}

					im.CommandArgument = UNSELECTED;
					im.ImageUrl = GetResource( RES_URL_SCALEUNSELECTED );

					// Should always be correct
					next = imbCache[j];
					zoomLevel =  ConvertToInt( next.CommandName );
					break;
				}
			}
			// Fire event
			OnZoom( ConvertToInt( zoomLevel ) );
		}
	
		#endregion

		#region Event triggers
		/// <summary>
		/// Fires the PreviousView event
		/// </summary>
		private void OnPreviousView()
		{
			if( PreviousView != null )
			{
				PreviousView( this, EventArgs.Empty );
			}
		}

		/// <summary>
		/// Fires the Zoom event
		/// </summary>
		private void OnZoom(int level)
		{
			if( Zoom != null )
			{
				// Fire the appropriate event defined in the event declaration
				ZoomLevelEventArgs ea = new ZoomLevelEventArgs(level);
				Zoom( this, ea );
			}
		}

		/// <summary>
		/// Fires the Zoom In event.
		/// </summary>
		private void OnZoomIn()
		{
			if( ZoomIn != null)
			{
				// Fire the approriate event
				EventArgs ea = new EventArgs();
				ZoomIn( this, ea );
			}
		}

		/// <summary>
		/// Fires the Zoom Out event.
		/// </summary>
		private void OnZoomOut()
		{
			if( ZoomOut != null)
			{	
				//Vantive 4227195 - prevent "Zoom Out" button zooming to far out
				int zoomLevel = 0;
				ImageButton im = new ImageButton();
				//Loop through image buttons to find out current zoom level
				for( int i = 0; i < imbCache.Length; i++)
				{
					im = imbCache[i];
					if( im.CommandArgument == SELECTED)
					{
						zoomLevel =  ConvertToInt( im.CommandName );
						break;
					}
				}
				//if we are at a lower zoom level than level 6 (1:500000)...
				if (zoomLevel > ConvertToInt( imbCache[7].CommandName) )
				{
					//...Then just effectively click on last slider bar button
					im.CommandArgument = UNSELECTED;
					im.ImageUrl = GetResource( RES_URL_SCALEUNSELECTED );
					OnZoom( ConvertToInt( imbCache[imbCache.GetUpperBound(0)].CommandName ) );
				}
				else
				{
				//Vantive 4227195 end

					// Fire the approriate event
					EventArgs ea = new EventArgs();
					ZoomOut( this, ea );
				}
			}
		}

		/// <summary>
		/// Fires the Find New Map Event
		/// </summary>
		private void OnFindNewMap()
		{
			if( FindNewMap != null)
			{
				// Fire the approriate event
				EventArgs ea = new EventArgs();
				FindNewMap( this, ea );
			}
		}

		#endregion

		#region Convenience methods

		/// <summary>
		/// Conveince method for turning an object into an int in a FXCop friendly way
		/// </summary>
		/// <param name="val">The value as object</param>
		/// <returns>The value as int</returns>
		private int ConvertToInt( object val)
		{
			// Presume it is string
			return Convert.ToInt32( val, TDCultureInfo.CurrentCulture.NumberFormat);
		}

		

		/// <summary>
		/// Returns an transpartent gif image with the specified width
		/// </summary>
		/// <param name="pixels">The width in pixels fo the image</param>
		/// <returns></returns>
		private System.Web.UI.WebControls.Image CreateSpace( int pixels )
		{
			System.Web.UI.WebControls.Image spacer = new System.Web.UI.WebControls.Image();
			spacer.AlternateText = "";
			spacer.ImageUrl
				= Global.tdResourceManager.GetString( "MapZoomControl.spacer.ImageUrl", TDCultureInfo.CurrentUICulture );
			spacer.Width = Unit.Pixel( pixels );
			return spacer;
		}
		#endregion

		#region Event Handlers

		/// <summary>
		/// Handles the Zoom In Event.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonZoomIn_Click(object sender, EventArgs e)
		{
			// Fire the zoom in event.
			OnZoomIn();
		}

		/// <summary>
		/// Handles the Zoom Out Event.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonZoomOut_Click(object sender, EventArgs e)
		{
			// Fire the zoom out event.
			OnZoomOut();
		}

		/// <summary>
		/// Handles the Previous View Button Click.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonPreviousView_Click(object sender, EventArgs e)
		{
			// Fire the previous event event
			OnPreviousView();
		}

		/// <summary>
		/// Handles the Find New Map Button Click.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonFindNewMap_Click(object sender, EventArgs e)
		{
			// Fire the find new map event
			OnFindNewMap();
		}

		#endregion

	}

	#region Custom Event Args class

	/// <summary>
	/// Class inherits from EventArgs.  Event arguments when a
	/// scale has been changed.
	/// </summary>
	public class ZoomLevelEventArgs : EventArgs
	{
		int level = 0;

		/// <summary>
		/// Constructor for the zoom level event args.
		/// </summary>
		/// <param name="level">Zoom level selected.</param>
		public ZoomLevelEventArgs(int level)
		{
			this.level = level;
		}

		/// <summary>
		/// Get property - returns the zoom level.
		/// </summary>
		public int ZoomLevel
		{
			get
			{
				return level;
			}
		}
	}

	#endregion

	#region Delegate defintion for the Zoom Level Event Handler

	/// <summary>
	/// Delegate - Event Handler if zoom level has changed
	/// </summary>
	public delegate void ZoomLevelChangedEventHandler(object sender, ZoomLevelEventArgs e);

	#endregion
}
