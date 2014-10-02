// *********************************************** 
// NAME                 : ExtendJourneyLineControl.ascx.cs 
// AUTHOR               : Paul Cross
// DATE CREATED         : 15/12/2005
// DESCRIPTION			: A user control to display a sequence of circles and arrowed lines to represent
//						  the current itinerary and any extension that is currently being planned.
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/ExtendJourneyLineControl.ascx.cs-arc  $
//
//   Rev 1.4   Jan 12 2009 11:35:00   devfactory
//XHTML Compliance Changes
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.3   Jan 08 2009 14:57:14   apatel
//XHTML Compliance Changes
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.2   Mar 31 2008 13:19:56   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:13:10   mturner
//Initial revision.
//
//   Rev 1.6   Mar 20 2006 18:04:48   pcross
//Code review updates
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.5   Mar 14 2006 13:20:38   pcross
//Added header, headelementcontrol, resource manager reference
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.4   Jan 27 2006 14:00:32   pcross
//Updates
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.3   Jan 26 2006 16:40:42   AViitanen
//Minor Fxcop change
//
//   Rev 1.2   Jan 19 2006 10:46:50   pcross
//Minor update
//
//   Rev 1.1   Jan 13 2006 14:13:08   pcross
//Early working version of the control that shows an extended journey in diagram format
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.0   Dec 21 2005 19:28:20   pcross
//Initial revision.
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//

namespace TransportDirect.UserPortal.Web.Controls
{
	using System;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using TransportDirect.UserPortal.Web.Adapters;
	using TransportDirect.Common.ServiceDiscovery;
	using TransportDirect.Web.Support;
	using TransportDirect.UserPortal.SessionManager;
	using TransportDirect.UserPortal.LocationService;
	using TransportDirect.Common.ResourceManager;

	/// <summary>
	///		Summary description for ExtendJourneyLineControl.
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public partial class ExtendJourneyLineControl : TDPrintableUserControl
	{

		#region Control Declarations


		// References to repeaters

		// References to extension location table cells (these are all invisible by default)

		// References to the optional layout columns (not required if not at max no extensions)

		// References to extension location images and labels

		#endregion

		#region Private Member Variables

		private bool returnJourneySelected;

		#endregion
		
		#region Properties
		
		private TDLocation[] outwardItineraryLocations;
		private TDLocation[] returnItineraryLocations;
		private TDLocation extensionLocation;
		private bool extendInProgress;
		private bool extendEndOfItinerary;

		/// <summary>
		/// Read/write property handling the array of locations in the existing outward itinerary.
		/// </summary>
		public TDLocation[] OutwardItineraryLocations
		{
			get { return outwardItineraryLocations; }
			set { outwardItineraryLocations = value; }
		}

		/// <summary>
		/// Read/write property handling the array of locations in the existing return itinerary.
		/// </summary>
		public TDLocation[] ReturnItineraryLocations
		{
			get { return returnItineraryLocations; }
			set { returnItineraryLocations = value; }
		}

		/// <summary>
		/// Read/write property handling the location of the outward extension being planned
		/// </summary>
		public TDLocation ExtensionLocation
		{
			get { return extensionLocation; }
			set { extensionLocation = value; }
		}

		/// <summary>
		/// Read/write property specifying whether there is an extension in progress
		/// </summary>
		public bool ExtendInProgress
		{
			get { return extendInProgress; }
			set { extendInProgress = value; }
		}

		/// <summary>
		/// Read/write property specifying whether the extension being planned is from the end of the existing itinerary
		/// </summary>
		public bool ExtendEndOfItinerary
		{
			get { return extendEndOfItinerary; }
			set { extendEndOfItinerary = value; }
		}

		#endregion

		#region Initialisation

		/// <summary>
		/// Contructor for control
		/// </summary>
		public ExtendJourneyLineControl()
		{
			// Set the resource file for the control
			this.LocalResourceManager = TDResourceManager.REFINE_JOURNEY_RM;
		}

		#endregion

		#region Private Methods

		/// <summary>
		/// Wires up events to handlers
		/// </summary>
		private void ExtraWiringEvents()
		{
			topRowText.ItemDataBound += new RepeaterItemEventHandler(topRowText_ItemDataBound);
			middleRowIcons.ItemDataBound += new RepeaterItemEventHandler(middleRowIcons_ItemDataBound);
			bottomRowText.ItemDataBound += new RepeaterItemEventHandler(bottomRowText_ItemDataBound);
		}

		/// <summary>
		/// Update the objects on the page associated with the creation of a new extension
		/// (ie when extend is in progress)
		/// </summary>
		private void PopulateExtensionObjects()
		{

			// Set all the extension related cells to invisible on server side (ie don't output at all in HTML)
			// They will be made visible as required in logic below.
			startExtensionCircleCell.Visible = false;
			topRowStartExtensionCell.Visible = false;
			topRowEndExtensionCell.Visible = false;
			startExtensionLineCell.Visible = false;
			endExtensionLineCell.Visible = false;
			endExtensionCircleCell.Visible = false;
			bottomRowStartExtensionCell.Visible = false;
			bottomRowEndExtensionCell.Visible = false;

			// Populate the cells that contain extension location information
			if (extendInProgress)
			{
				// Note that we can have an extend in progress but no extension location when we are in the
				// early stages of extending and haven't yet chosen a location
				if (extendEndOfItinerary)
				{
					// Extension is at the end of the journey

					// Add extension location information
					if (returnJourneySelected)
					{
						topRowEndExtensionCell.Visible = true;
						if (extensionLocation != null)
							topRowEndExtensionLocation.Text = extensionLocation.Description;

						bottomRowEndExtensionCell.Visible = true;
						if (extensionLocation != null)
							bottomRowEndExtensionLocation.Text = extensionLocation.Description;
					}
					else
					{
						bottomRowEndExtensionCell.Visible = true;
						if (extensionLocation != null)
							bottomRowEndExtensionLocation.Text = extensionLocation.Description;
					}

					// Make line image and ball image in end cells visible and assign the appropriate image urls
					endExtensionLineCell.Visible = true;
					if (returnJourneySelected)
					{
						endExtensionLine.ImageUrl = GetResource("ExtendJourneyLineControl.OutwardAndReturnArrowExtension.ImageURL");
						endExtensionLine.AlternateText = GetResource("ExtendJourneyLineControl.OutwardAndReturnArrowExtension.AlternateText");
						endExtensionLine.ToolTip = GetResource("ExtendJourneyLineControl.OutwardAndReturnArrowExtension.AlternateText");
					}
					else
					{
						endExtensionLine.ImageUrl = GetResource("ExtendJourneyLineControl.OutwardArrowExtension.ImageURL");
						endExtensionLine.AlternateText = GetResource("ExtendJourneyLineControl.OutwardArrowExtension.AlternateText");
						endExtensionLine.ToolTip = GetResource("ExtendJourneyLineControl.OutwardArrowExtension.AlternateText");
					}

					endExtensionCircleCell.Visible = true;
					endExtensionCircle.ImageUrl = GetResource("ExtendJourneyLineControl.EndExtensionCircle.ImageURL");
					endExtensionCircle.AlternateText = GetResource("ExtendJourneyLineControl.EndExtensionCircle.AlternateText");
					endExtensionCircle.ToolTip = GetResource("ExtendJourneyLineControl.EndExtensionCircle.AlternateText");

				}
				else
				{
					// Extension is at the start of the journey

					// Add extension location information
					if (returnJourneySelected)
					{
						topRowStartExtensionCell.Visible = true;
						if (extensionLocation != null)
							topRowStartExtensionLocation.Text = extensionLocation.Description;

						bottomRowStartExtensionCell.Visible = true;
						if (extensionLocation != null)
							bottomRowStartExtensionLocation.Text = extensionLocation.Description;
					}
					else
					{
						bottomRowStartExtensionCell.Visible = true;
						if (extensionLocation != null)
							bottomRowStartExtensionLocation.Text = extensionLocation.Description;
					}

					// Make line image and ball image in end cells visible and assign the appropriate image urls
					startExtensionLineCell.Visible = true;
					if (returnJourneySelected)
					{
						startExtensionLine.ImageUrl = GetResource("ExtendJourneyLineControl.OutwardAndReturnArrowExtension.ImageURL");
						startExtensionLine.AlternateText = GetResource("ExtendJourneyLineControl.OutwardAndReturnArrowExtension.AlternateText");
						startExtensionLine.ToolTip = GetResource("ExtendJourneyLineControl.OutwardAndReturnArrowExtension.AlternateText");
					}
					else
					{
						startExtensionLine.ImageUrl = GetResource("ExtendJourneyLineControl.OutwardArrowExtension.ImageURL");
						startExtensionLine.AlternateText = GetResource("ExtendJourneyLineControl.OutwardArrowExtension.AlternateText");
						startExtensionLine.ToolTip = GetResource("ExtendJourneyLineControl.OutwardArrowExtension.AlternateText");
					}

					startExtensionCircleCell.Visible = true;
					startExtensionCircle.ImageUrl = GetResource("ExtendJourneyLineControl.StartExtensionCircle.ImageURL");
					startExtensionCircle.AlternateText = GetResource("ExtendJourneyLineControl.StartExtensionCircle.AlternateText");
					startExtensionCircle.ToolTip = GetResource("ExtendJourneyLineControl.StartExtensionCircle.AlternateText");

				}
			}
		}

		/// <summary>
		/// Controls the layout columns (empty header columns in 1st row are all for layout (width) specifications)
		/// </summary>
		private void SetLayoutColumns()
		{
			// Only show as many columns as necessary
			int locationCount = outwardItineraryLocations.Length + (extendInProgress ? 1: 0);
			
			switch (locationCount)
			{
				case 2:		// 2 locations need 6 cells so make the last 6 invisible
					layoutColumn7.Visible = false;
					layoutColumn8.Visible = false;
					layoutColumn9.Visible = false;
					layoutColumn10.Visible = false;
					layoutColumn11.Visible = false;
					layoutColumn12.Visible = false;
					break;
				case 3:		// 3 locations need 9 cells so make the last 3 invisible
					layoutColumn10.Visible = false;
					layoutColumn11.Visible = false;
					layoutColumn12.Visible = false;
					break;
				case 4:		// all cells required
				default:
					break;
			}

		}

		/// <summary>
		/// Bind the repeaters to the outward and return itinerary locations
		/// </summary>
		private void BindRepeaters()
		{

			// bind to outward text row
			// if we are handling a return journey, this text is on the top row, else it is on the bottom
			if (returnJourneySelected)
			{
				topRowText.DataSource = outwardItineraryLocations;
				topRowText.DataBind();
			}
			else
			{
				topRowText.Visible = false;
				bottomRowText.DataSource = outwardItineraryLocations;
				bottomRowText.DataBind();
			}

			// bind to icon row
			middleRowIcons.DataSource = outwardItineraryLocations;
			middleRowIcons.DataBind();

			// bind to return text row
			// only show return text if we have return journeys
			if (returnJourneySelected)
			{
				// The locations in the return locations array are in the reverse order
				// to the way we want to populate the control so reverse array before binding
				int i = returnItineraryLocations.Length;
				TDLocation[] reversedReturnItineraryLocations = new TDLocation[i];
				
				foreach(TDLocation location in returnItineraryLocations)
				{
					i--;
					reversedReturnItineraryLocations[i] = location;
				}

				bottomRowText.DataSource = reversedReturnItineraryLocations;
				bottomRowText.DataBind();
			}
			else
			{
				topRowText.Visible = false;
			}
		}

		/// <summary>
		/// Gets the table summary text from the resource file to be used in the markup
		/// </summary>
		/// <returns>Table summary text string</returns>
		protected string GetTableSummary()
		{
			return GetResource("ExtendJourneyLineControl.Table.Summary");
		}

		#endregion

		#region Event Handlers
		
		/// <summary>
		/// Initialisations on page load
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, System.EventArgs e)
		{

			// Ensure we have an itinerary to display
			if (outwardItineraryLocations != null && outwardItineraryLocations.Length > 0)
			{
			
				// See if we need to handle return journeys on this page
				if (returnItineraryLocations != null)
				{
					if (returnItineraryLocations.Length > 0 && returnItineraryLocations[0] != null)
						returnJourneySelected = true;
				}

				// Hide layout columns as necessary
				SetLayoutColumns();

				// If extend is in progress, this will populate those associated objects
				PopulateExtensionObjects();

				// Populate the repeaters with the outward and return location information
				BindRepeaters();
			}
			else
			{
				hideIfEmpty.Visible = false;
			}

		}

		/// <summary>
		/// Populate the controls in the topRow repeater as the data is bound
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void topRowText_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			Label labelTag;

			// Label setting independent of which item we are looking at
			labelTag = (Label)e.Item.FindControl("topRowItineraryLocation");
			if (labelTag != null && (TDLocation)e.Item.DataItem != null)
			{
				labelTag.Text = ((TDLocation)e.Item.DataItem).Description;
			}
		}

		/// <summary>
		/// Populate the controls in the middleRow repeater as the data is bound
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void middleRowIcons_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			bool settingsMade = false;	// Log when settings made for the item to ensure always set in 'intermediate' scenario (ie when not 1st or last item)

			TDImage imageTag;
			HtmlTableCell cellTag;

			// Display settings depend on whether 1st item in repeater, last item or intermediary

			// If 1st item, show start node image and arrows
			if (e.Item.ItemIndex == 0)
			{
				settingsMade = true;

				// Settings for the circle image
				imageTag = (TDImage)e.Item.FindControl("itineraryCircle");

				if (imageTag != null)
				{
					imageTag.ImageUrl = GetResource("ExtendJourneyLineControl.StartCircle.ImageURL");
					imageTag.AlternateText = GetResource("ExtendJourneyLineControl.StartCircle.AlternateText");
					imageTag.ToolTip = GetResource("ExtendJourneyLineControl.StartCircle.AlternateText");
				}

				// Settings for the arrow(s) image
				imageTag = (TDImage)e.Item.FindControl("itineraryLine");

				if (imageTag != null)
				{
					if (returnJourneySelected)
					{
						imageTag.ImageUrl = GetResource("ExtendJourneyLineControl.OutwardAndReturnArrow.ImageURL");
						imageTag.AlternateText = GetResource("ExtendJourneyLineControl.OutwardAndReturnArrow.AlternateText");
						imageTag.ToolTip = GetResource("ExtendJourneyLineControl.OutwardAndReturnArrow.AlternateText");
					}
					else
					{
						imageTag.ImageUrl = GetResource("ExtendJourneyLineControl.OutwardArrow.ImageURL");
						imageTag.AlternateText = GetResource("ExtendJourneyLineControl.OutwardArrow.AlternateText");
						imageTag.ToolTip = GetResource("ExtendJourneyLineControl.OutwardArrow.AlternateText");
					}
				}

			}

			// If last item, show end node image with no arrows
			if (e.Item.ItemIndex == ((TDLocation[])middleRowIcons.DataSource).Length - 1 && !settingsMade)
			{
				settingsMade = true;
				
				// Settings for the circle image
				imageTag = (TDImage)e.Item.FindControl("itineraryCircle");

				if (imageTag != null)
				{
					imageTag.ImageUrl = GetResource("ExtendJourneyLineControl.EndCircle.ImageURL");
					imageTag.AlternateText = GetResource("ExtendJourneyLineControl.EndCircle.AlternateText");
					imageTag.ToolTip = GetResource("ExtendJourneyLineControl.EndCircle.AlternateText");
				}

				// Hide the arrow(s) image by hiding the containing cell
				cellTag = (HtmlTableCell)e.Item.FindControl("itineraryLineCell");

				if (cellTag != null)
					cellTag.Visible = false;

			}

			// If intermediate item, show intermediate node image with arrows
			if (!settingsMade)
			{
				// Settings for the circle image
				imageTag = (TDImage)e.Item.FindControl("itineraryCircle");

				if (imageTag != null)
				{
					imageTag.ImageUrl = GetResource("ExtendJourneyLineControl.IntermediateCircle.ImageURL");
					imageTag.AlternateText = GetResource("ExtendJourneyLineControl.IntermediateCircle.AlternateText");
					imageTag.ToolTip = GetResource("ExtendJourneyLineControl.IntermediateCircle.AlternateText");
				}

				// Settings for the arrow(s) image
				imageTag = (TDImage)e.Item.FindControl("itineraryLine");

				if (imageTag != null)
				{
					if (returnJourneySelected)
					{
						imageTag.ImageUrl = GetResource("ExtendJourneyLineControl.OutwardAndReturnArrow.ImageURL");
						imageTag.AlternateText = GetResource("ExtendJourneyLineControl.OutwardAndReturnArrow.AlternateText");
						imageTag.ToolTip = GetResource("ExtendJourneyLineControl.OutwardAndReturnArrow.AlternateText");
					}
					else
					{
						imageTag.ImageUrl = GetResource("ExtendJourneyLineControl.OutwardArrow.ImageURL");
						imageTag.AlternateText = GetResource("ExtendJourneyLineControl.OutwardArrow.AlternateText");
						imageTag.ToolTip = GetResource("ExtendJourneyLineControl.OutwardArrow.AlternateText");
					}
				}

			}

		}

		/// <summary>
		/// Populate the controls in the bottomRow repeater as the data is bound
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void bottomRowText_ItemDataBound(object sender, RepeaterItemEventArgs e)
		{
			Label labelTag;

			// Label setting independent of which item we are looking at
			labelTag = (Label)e.Item.FindControl("bottomRowItineraryLocation");
			if (labelTag != null && (TDLocation)e.Item.DataItem != null)
			{
				labelTag.Text = ((TDLocation)e.Item.DataItem).Description;
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
			ExtraWiringEvents();
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

	}
}
