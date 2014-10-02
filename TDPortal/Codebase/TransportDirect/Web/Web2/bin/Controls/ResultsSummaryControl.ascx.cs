// *********************************************** 
// NAME                 : ResultsSummaryControl.ascx.cs 
// AUTHOR               : Paul Cross
// DATE CREATED         : 13/12/2005
// DESCRIPTION			: A user control to present the user with all of the outward and return journeys
//						  that are valid for the current results screen
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/ResultsSummaryControl.ascx.cs-arc  $
//
//   Rev 1.3   Dec 19 2008 15:06:24   apatel
//XHTML Compliance Changes
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.2   Mar 31 2008 13:22:40   mturner
//Drop3 from Dev Factory
//
//  Rev Feb 05 2008 13:12:12 dsawe
//  White labelling screen change on RefinejourneyPlan.aspx
//  Removed the transport icons & replaced with transport label
//
//   Rev 1.0   Nov 08 2007 13:17:18   mturner
//Initial revision.
//
//   Rev 1.13   Apr 24 2006 16:17:14   mtillett
//Updated the logic so that the row colouring is not dependent on whether the selected column was shown or not shown. Also added the row colouring logic to be applied to printer friendly pages.
//Resolution for 3926: DN068 Extend: colouring of printer friendly page for Choose a Connecting Journey
//
//   Rev 1.12   Mar 14 2006 19:49:58   NMoorhouse
//fix stream3353 merge issues
//
//   Rev 1.11   Mar 14 2006 13:20:14   pcross
//Added header, headelementcontrol, resource manager reference
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.10   Mar 08 2006 20:31:00   pcross
//FxCop
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.9   Mar 06 2006 22:08:40   rhopkins
//Include comparison of JourneyType when testing for checked journey index.
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.8   Mar 02 2006 15:09:52   tolomolaiye
//Modified results display order
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.7   Feb 24 2006 10:45:56   pcross
//Removed option and date columns
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.6   Feb 08 2006 18:12:04   pcross
//Correction to journey index now itinerary is zero bound
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.5   Feb 03 2006 14:49:58   pcross
//Updates to allow different column sizings dependent on columns present in control
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.4   Jan 27 2006 14:05:12   pcross
//Minor FXCop update
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.3   Jan 19 2006 10:48:28   pcross
//Various bug fixes
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.2   Jan 13 2006 14:15:06   pcross
//Latest version
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.1   Jan 10 2006 10:44:58   pcross
//Latest - pretty usable version of this control. Not yet tested - more work will yet be required!
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.0   Dec 21 2005 19:28:20   pcross
//Initial revision.
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//

using System;
using System.Globalization;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.Web.Adapters;
using TransportDirect.Web.Support;
using TransportDirect.Common.ResourceManager;
using TransportDirect.Common;

namespace TransportDirect.UserPortal.Web.Controls
{
	/// <summary>
	///	Displays outward and return journeys in various formats
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public partial class ResultsSummaryControl : TDPrintableUserControl
	{

		// constants for handling row selection
		private const string ROW_HIGHLIGHTER_SCRIPT = "RowHighlighter";
		private const string SELECT_ROW = "eselectRow";
		private const string ODD_ROW = "eoddRow";
		private const string EVEN_ROW = "eevenRow";


		#region Properties and Events
		
		private FormattedJourneySummaryLines summaryLines; 

		private TDJourneyType itemJourneyType;
		private int itemJourneyIndex;
		private int selectedLineIndex;
		private bool hasSelectionChanged;

		private bool showDeleteColumn = false;
		private bool showSelectColumn = false;
		private bool showEmptyDeleteColumn = false;
		private bool showEmptySelectColumn = false;
		private bool useSelectedRowHighlighting = true;

		private int CommandSegment;

		public event CommandEventHandler SelectionChangedEvent;

		/// <summary>
		/// Read/write property to handle the datasource for the control
		/// </summary>
		public FormattedJourneySummaryLines SummaryLines
		{
			get {return summaryLines;}
			set {summaryLines = value;}
		}

		/// <summary>
		/// Read/write property. Gets/sets the index of the selected line
		/// </summary>
		public int SelectedLineIndex
		{
			get { return selectedLineIndex; }
			set { selectedLineIndex = value; }
		}

		/// <summary>
		/// Read/write property. Handles whether Delete column should be visible or not.
		/// </summary>
		public bool ShowDeleteColumn
		{
			get { return showDeleteColumn; }
			set { showDeleteColumn = value; }
		}

		/// <summary>
		/// Read/write property. Handles whether Select column should be visible or not.
		/// </summary>
		public bool ShowSelectColumn
		{
			get { return showSelectColumn; }
			set { showSelectColumn = value; }
		}

		/// <summary>
		/// Read/write property. Handles whether an empty Delete column should be drawn.
		/// </summary>
		public bool ShowEmptyDeleteColumn
		{
			get { return showEmptyDeleteColumn; }
			set
			{
				showEmptyDeleteColumn = value;
				if (showEmptyDeleteColumn)
					showDeleteColumn = true;
			}
		}

		/// <summary>
		/// Read/write property. Handles whether an empty Select column should be drawn.
		/// </summary>
		public bool ShowEmptySelectColumn
		{
			get { return showEmptySelectColumn; }
			set
			{
				showEmptySelectColumn = value;
				if (showEmptySelectColumn)
					showSelectColumn = true;
			}
		}

		/// <summary>
		/// Read/write property. Handles if the selected row highlighting should be handled in this control or not.
		/// This allows a page to handle the highlighting instead if required.
		/// </summary>
		public bool UseSelectedRowHighlighting
		{
			get { return useSelectedRowHighlighting; }
			set { useSelectedRowHighlighting = value; }
		}

		/// <summary>
		/// Read-only property to determine if the user has selected another segment
		/// </summary>
		public bool NewSelection
		{
			get { return hasSelectionChanged; }
		}

		/// <summary>
		/// Read only property. Returns the table summary
		/// </summary>
		/// <returns></returns>
		protected string GetTableSummary
		{
			//TODO: add resource string - need to know whether outward or return table
			get { return GetResource("ResultsSummaryControl.TableSummary"); }
		}

		/// <summary>
		/// Read/write property. Gets/sets the segment index for all controls
		/// </summary>
		protected int CommandArgumentSegmentIndex
		{
			get { return CommandSegment; }
			set { CommandSegment = value; }
		}
		
		/// <summary>
		/// Read/write property. Gets/sets the journeyType of the 
		/// selected journey in the list.
		/// </summary>
		public TDJourneyType SelectedItemJourneyType
		{
			get { return itemJourneyType; }
			set { itemJourneyType = value; }
		}

		/// <summary>
		/// Read/write property. Gets/sets the journeyIndex of the 
		/// selected journey in the list. This is potentially 
		/// different from its index in the list.
		/// </summary>
		public int SelectedItemJourneyIndex
		{
			get { return itemJourneyIndex; }
			set { itemJourneyIndex = value; }
		}

		/// <summary>
		/// Read only property. Returns the Javascript function to execute when the radio button is clicked.
		/// </summary>
		/// <returns>Javascript to execute when the radio button is clicked</returns>
		protected string GetAction
		{
			get
			{
				if (useSelectedRowHighlighting)
					return "return highlightSelectedItem('" + GetTableId + "', '" + ODD_ROW + "','" + EVEN_ROW + "','" + SELECT_ROW + "');";
				else
					return String.Empty;
			}
		}

		/// <summary>
		/// Read only property. Used to set the Id attribute of the Html table in the control.
		/// </summary>
		/// <returns>Table Id string</returns>
		protected string GetTableId
		{
			get
			{
				return journeyRows.ClientID;	
			}
		}	


		#endregion

		#region Constructor

		/// <summary>
		/// Contructor for control
		/// </summary>
		public ResultsSummaryControl()
		{
			// Set the resource file for the control
			this.LocalResourceManager = TDResourceManager.REFINE_JOURNEY_RM;
		}

		#endregion

		#region Page Initialisation

		/// <summary>
		/// Page Load event used to set up the scripts for scriptable radio button
		/// </summary>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (UseSelectedRowHighlighting && ((TDPage)Page).IsJavascriptEnabled && !PrinterFriendly) 
			{
				ScriptRepository.ScriptRepository scriptRepository = (ScriptRepository.ScriptRepository)TDServiceDiscovery.Current[ServiceDiscoveryKey.ScriptRepository];				
				// Output reference to necessary JavaScript file from the ScriptRepository
				Page.ClientScript.RegisterClientScriptBlock(typeof(ResultsSummaryControl), ROW_HIGHLIGHTER_SCRIPT, scriptRepository.GetScript(ROW_HIGHLIGHTER_SCRIPT, ((TDPage)Page).JavascriptDom));

				// Add page startup script to run line highlighting initially (if data exists)
				if (summaryLines != null)
				{
					if (summaryLines.Count > 0 && summaryLines[0] != null)
					{
						Page.ClientScript.RegisterStartupScript(typeof(ResultsSummaryControl), "RowHighlightStartupScript_" + GetTableId, CreateRowHighlightStartupScript());
					}
				}
			}

			// Populate the repeater
			journeyRows.DataSource = summaryLines;
			journeyRows.DataBind();
		}

		/// <summary>
		/// Pre Render method runs script to set row highlight to initial state
		/// </summary>
		private void OnPreRender(object sender, System.EventArgs e)
		{
			journeyRows.DataSource = summaryLines;
			journeyRows.DataBind();
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
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.PreRender += new EventHandler(this.OnPreRender);

			ExtraWiringEvents();
		}

		#endregion

		#region Event Handlers

		/// <summary>
		/// For each data item binding to the journeyRows repeater, set the properties for each control
		/// for the current journey row and add event handlers
		/// </summary>
		private void journeyRows_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
		{
			Label labelTag;
			HtmlTableCell cellTag;

			switch(e.Item.ItemType)
			{         
				case ListItemType.Item:
				case ListItemType.AlternatingItem:

					// Get the object for the current row being bound so we can populate labels and stuff
					// Where the columns are not needed, find the TD elements and set to invisible
					FormattedJourneySummaryLine journeyLine = (FormattedJourneySummaryLine)e.Item.DataItem;

					// Delete column
					cellTag = (HtmlTableCell)e.Item.FindControl("tablecellDelete");
					if (cellTag != null)
					{
						if (showDeleteColumn)
						{
							cellTag.Visible = true;

							// Set the text for the delete hyperlink and wire the event handler
							HyperlinkPostbackControl hyperlinkDelete = (HyperlinkPostbackControl)e.Item.FindControl("hyperlinkDelete");
							
							if	(hyperlinkDelete != null)
							{

								if (showEmptyDeleteColumn)
								{
									hyperlinkDelete.Visible = false;
									cellTag.InnerHtml = "&nbsp;";
									cellTag.Attributes.Add("class", "ejpdeleempty");
								}
								else
								{
									// Assign the journey index for the current data item to the command property so it
									// can be stored against the control to be referred to later (in event handler) so we
									// can see which item the command is being run on
									hyperlinkDelete.CommandName = journeyLine.JourneyIndex.ToString(CultureInfo.InvariantCulture);

									// We only show the delete hyperlink for the 1st and last rows
									// and then, only if there is more than 1 journey
									if (summaryLines.Count > 1 && (e.Item.ItemIndex == 0 || e.Item.ItemIndex == summaryLines.Count - 1))
									{

										string deleteHyperlinkText = GetResource("ResultsSummaryControl.DeleteHyperlink.Text");
										
										// Set text for the delete hyperlink control
										hyperlinkDelete.Text = deleteHyperlinkText;
										hyperlinkDelete.ToolTipText = deleteHyperlinkText;

										// Set to show just a delete button for non-JS mode (instead of label and "Go" button)
										hyperlinkDelete.ShowLabelForNonJS = false;

										// Wire event to the delete hyperlink control
										hyperlinkDelete.link_Clicked += new EventHandler(hyperlinkDelete_link_Clicked);
									}
									else
									{
										hyperlinkDelete.Visible = false;
										cellTag.InnerHtml = "&nbsp;";
									}
								}
							}
						}
						else
						{
							cellTag.Visible = false;
						}
					}

					// From label
					cellTag = (HtmlTableCell)e.Item.FindControl("tablecellOrigin");
					if (cellTag != null)
					{
						// If not showing extra column, the origin column will be the 1st in grid so add left padding
						if (!showDeleteColumn)
						{
							cellTag.Attributes.Add("class", "ejporigwide");
						}
					}
					labelTag = (Label)e.Item.FindControl("labelFrom");
					if (labelTag != null)
					{
						labelTag.Text = journeyLine.GetOriginDescription();
					}

					// To label
					labelTag = (Label)e.Item.FindControl("labelTo");
					if (labelTag != null)
					{
						labelTag.Text = journeyLine.GetDestinationDescription();
					}

                    //// Transport mode icons
                    //TransportModesControl trmControl = (TransportModesControl)e.Item.FindControl("itmTransportMode"); 
                    //if ( trmControl != null)
                    //{ 
                    //    trmControl.DataSource = journeyLine.AllModes;
                    //    trmControl.DataBind();
                    //} 
                    //  White labelling screen change- removed transport icons
                    // Transport Mode label
                    labelTag = (Label)e.Item.FindControl("labelTransport");
                    if (labelTag != null)
                    {
                        labelTag.Text = journeyLine.GetTransportModes();
                    }

					// Leaving time
					labelTag = (Label)e.Item.FindControl("labelLeaveTime");
					if (labelTag != null)
					{
						labelTag.Text = journeyLine.GetDepartureTime();
					}

					// Arrival time
					labelTag = (Label)e.Item.FindControl("labelArriveTime");
					if (labelTag != null)
					{
						labelTag.Text = journeyLine.GetArrivalTime();
					}

					// Duration
					labelTag = (Label)e.Item.FindControl("labelDuration");
					if (labelTag != null)
					{
						labelTag.Text = journeyLine.GetAbbreviatedDuration();
					}


					// Select radio button
					cellTag = (HtmlTableCell)e.Item.FindControl("tablecellSelect");
					if (cellTag != null)
					{
						if (showSelectColumn)
						{
							cellTag.Visible = true;
						}
						else
						{
							cellTag.Visible = false;
						}

						// Set the value of the group name and the index of the ScriptableRadioButton
						ScriptableGroupRadioButton sgrJourney = (ScriptableGroupRadioButton)e.Item.FindControl("journeyRadioButton");
						if (sgrJourney != null)
						{
							// Radio buttons aren't shown on printer page or if empty column is to be displayed
							if (PrinterFriendly || showEmptySelectColumn)
							{
								sgrJourney.Visible = false;
								cellTag.InnerHtml = "&nbsp;";
								cellTag.Attributes.Add("class", "ejpseleempty");

								// Check the journey index to determine if item should be checked
								//if (journeyLine.JourneyIndex == itemJourneyIndex)
								if (((itemJourneyType == TDJourneyType.RoadCongested) && (summaryLines[e.Item.ItemIndex].Type == TDJourneyType.RoadCongested))
									|| ((itemJourneyType != TDJourneyType.RoadCongested) && (summaryLines[e.Item.ItemIndex].Type != TDJourneyType.RoadCongested) && (summaryLines[e.Item.ItemIndex].JourneyIndex == itemJourneyIndex)))
								{
									// Only use selected row highlighting if property is set to allow this
									if (UseSelectedRowHighlighting)
									{
										HtmlTableRow row = (HtmlTableRow)e.Item.FindControl("row");
										row.Attributes["class"] = SELECT_ROW;
									}
									else
									{
										// Just use the default alternating colour
										HtmlTableRow row = (HtmlTableRow)e.Item.FindControl("row");
										row.Attributes["class"] = ((e.Item.ItemIndex % 2) == 0) ? EVEN_ROW : ODD_ROW;
									}
								}
								else
								{
									// For all non-selected rows add the default alternating colours
									HtmlTableRow row = (HtmlTableRow)e.Item.FindControl("row");
									row.Attributes["class"] = ((e.Item.ItemIndex % 2) == 0) ? EVEN_ROW : ODD_ROW;
								}
							}
							else
							{
								sgrJourney.CheckedChanged += new System.EventHandler(this.SelectionChanged);
								// Assign the radio button group name to be dependent on the control ID to allow several controls
								// to exist on one page without the radio button groups affecting each other
								sgrJourney.GroupName = journeyRows.ClientID + "_" + CommandArgumentSegmentIndex.ToString(TDCultureInfo.CurrentUICulture);

								// Check the journey index to determine if item should be checked
								//if (journeyLine.JourneyIndex == itemJourneyIndex)
								if (((itemJourneyType == TDJourneyType.RoadCongested) && (summaryLines[e.Item.ItemIndex].Type == TDJourneyType.RoadCongested))
									|| ((itemJourneyType != TDJourneyType.RoadCongested) && (summaryLines[e.Item.ItemIndex].Type != TDJourneyType.RoadCongested) && (summaryLines[e.Item.ItemIndex].JourneyIndex == itemJourneyIndex)))
								{
									sgrJourney.Checked = true;

									// Only use selected row highlighting if property is set to allow this
									// and javascript is enabled
									if (UseSelectedRowHighlighting && ((TDPage)Page).IsJavascriptEnabled)
									{
										HtmlTableRow row = (HtmlTableRow)e.Item.FindControl("row");
										row.Attributes["class"] = SELECT_ROW;
									}
									else
									{
										// Just use the default alternating colour
										HtmlTableRow row = (HtmlTableRow)e.Item.FindControl("row");
										row.Attributes["class"] = ((e.Item.ItemIndex % 2) == 0) ? EVEN_ROW : ODD_ROW;
									}
								}
								else
								{
									// For all non-selected rows add the default alternating colours
									HtmlTableRow row = (HtmlTableRow)e.Item.FindControl("row");
									row.Attributes["class"] = ((e.Item.ItemIndex % 2) == 0) ? EVEN_ROW : ODD_ROW;
								}
							}
						}
					}

					break;

				case ListItemType.Header:

					// Get the text for the items in the header
					// Where the columns are not needed, find the TH elements and set to invisible

					// Delete column
					cellTag = (HtmlTableCell)e.Item.FindControl("tableheaderDelete");
					if (cellTag != null)
					{
						if (showDeleteColumn)
						{
							cellTag.Visible = true;
							cellTag.InnerHtml = "&nbsp;";

							if (showEmptyDeleteColumn)
							{
								cellTag.Attributes.Add("class", "ejpdelehdempty");
							}
						}
						else
						{
							cellTag.Visible = false;
						}
					}

					// From column
					cellTag = (HtmlTableCell)e.Item.FindControl("tableheaderOrig");
					if (cellTag != null)
					{
						// If the optional delete column is not shown, we can give extra space
						// to existing columns
						if (!ShowDeleteColumn)
						{
							cellTag.Attributes.Add("class", "ejporighdwide");
						}
					}
					
					labelTag = (Label)e.Item.FindControl("headerlabelFrom");
					if (labelTag != null)
					{
						labelTag.Text = GetResource("ResultsSummaryControl.FromLabel.Text");
					}
                    
					// To column
					cellTag = (HtmlTableCell)e.Item.FindControl("tableheaderDest");
					if (cellTag != null)
					{
						// If the optional delete column is not shown, we can give extra space
						// to existing columns
						if (!ShowDeleteColumn)
						{
							cellTag.Attributes.Add("class", "ejpdesthdwide");
						}
					}
					
					labelTag = (Label)e.Item.FindControl("headerlabelTo");
					if (labelTag != null)
					{
						labelTag.Text = GetResource("ResultsSummaryControl.ToLabel.Text");
					}
                    
					// Transport column
					cellTag = (HtmlTableCell)e.Item.FindControl("tableheaderTransport");
					if (cellTag != null)
					{
						// If optional columns are missing, give extra space to transport column
						if (!ShowDeleteColumn)
						{
							if (!ShowSelectColumn)
							{
								cellTag.Attributes.Add("class", "ejptranhdsuperwide");
							}
							else
							{
								cellTag.Attributes.Add("class", "ejptranhdwide");
							}
						}
					}

					labelTag = (Label)e.Item.FindControl("headerlabelTransport");
					if (labelTag != null)
					{
						labelTag.Text = GetResource("ResultsSummaryControl.TransportLabel.Text");
					}
                    
					// Leave time column
					labelTag = (Label)e.Item.FindControl("headerlabelLeaveTime");
					if (labelTag != null)
					{
						labelTag.Text = GetResource("ResultsSummaryControl.LeaveTimeLabel.Text");
					}
                    
					// Arrive time column
					labelTag = (Label)e.Item.FindControl("headerlabelArriveTime");
					if (labelTag != null)
					{
						labelTag.Text = GetResource("ResultsSummaryControl.ArriveTimeLabel.Text");
					}
                    
					// Duration column
					labelTag = (Label)e.Item.FindControl("headerlabelDuration");
					if (labelTag != null)
					{
						labelTag.Text = GetResource("ResultsSummaryControl.DurationLabel.Text");
					}
                    
					// Select radio button column
					cellTag = (HtmlTableCell)e.Item.FindControl("tableheaderSelect");
					if (cellTag != null)
					{
						if (showSelectColumn)
						{
							cellTag.Visible = true;

							labelTag = (Label)e.Item.FindControl("headerlabelSelect");
							if (labelTag != null)
							{
								if (showEmptySelectColumn)
								{
									labelTag.Visible = false;

									// Change class to one which has a bottom line but no left line
									cellTag.InnerHtml = "&nbsp;";
									cellTag.Attributes.Add("class", "ejpselehdempty");
								}
								else
								{
									labelTag.Text = GetResource("ResultsSummaryControl.SelectLabel.Text");
								}
							}
						}
						else
						{
							cellTag.Visible = false;
						}
						
					}
					
					break;
				default :
					break;
			} 	
		}

		/// <summary>
		/// Event handler on clicking the delete hyperlink.
		/// Deletes the selected journey segment
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void hyperlinkDelete_link_Clicked(object sender, EventArgs e)
		{
			// Get which journey we are working on
			HyperlinkPostbackControl hyperlinkDelete = (HyperlinkPostbackControl)sender;

			int selectedJourneyIndex = Convert.ToInt32(hyperlinkDelete.CommandName, TDCultureInfo.CurrentCulture.NumberFormat);

			// It can only either be the 1st or last segment
			if (selectedJourneyIndex == 0)
				ExtendItineraryManager.Current.DeleteFirstSegment();
			else
				ExtendItineraryManager.Current.DeleteLastSegment();
		}

		#endregion

		#region Local Methods

		/// <summary>
		/// Wire up extra events
		/// </summary>
		private void ExtraWiringEvents()
		{
			this.journeyRows.ItemDataBound += new System.Web.UI.WebControls.RepeaterItemEventHandler(this.journeyRows_ItemDataBound);
		}

		/// <summary>
		/// Create a startup script to call the function to highlight the selected row when the control loads.
		/// </summary>
		/// <returns></returns>
		private string CreateRowHighlightStartupScript()
		{ 
			return 
				"<script language=\"javascript\" type=\"text/javascript\" >" + 
				"highlightSelectedItem('" + GetTableId + "', '" + ODD_ROW + "','" + EVEN_ROW + "','" + SELECT_ROW + "');" + 
				"</script>"; 
		} 

		/// <summary>
		/// Get the index of the currently selected item
		/// </summary>
		protected void SelectionChanged(object sender, System.EventArgs e) 
		{
			foreach (RepeaterItem item in journeyRows.Items)
			{
				ScriptableGroupRadioButton sgrButton = 
					(ScriptableGroupRadioButton)item.FindControl("journeyRadioButton");	

				if (sgrButton != null && sgrButton.Checked)
				{
					// Set the index of the journey in the list
					selectedLineIndex = item.ItemIndex;
					// Set the journey type of the selected item
					itemJourneyType = summaryLines[item.ItemIndex].Type;
					// Set the journey index of the selected item
					itemJourneyIndex = summaryLines[item.ItemIndex].JourneyIndex;
					hasSelectionChanged = true;
					break;
				}
			}
			if (SelectionChangedEvent != null)
			{
				CommandEventArgs selectionCommand = new CommandEventArgs("SelectionChanged", CommandSegment);
				SelectionChangedEvent(sender, selectionCommand);
			}
		}

		#endregion

	}
}