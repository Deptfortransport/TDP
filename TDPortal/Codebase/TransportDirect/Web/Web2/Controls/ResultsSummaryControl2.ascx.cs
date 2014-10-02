// *********************************************** 
// NAME                 : ResultsSummaryControl2.ascx.cs 
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 13/02/2007
// DESCRIPTION			: A user control to present the user with their outward and return journey 
//						  selected. It shows the Transport modes icons on a new line or same line
//						  dependent on flag(s) set
//						  
//						  NOTE: This is similar to the ResultsSummaryControl.ascx but has been 
//						  modified - all select and deletable columns have been removed, and
//						  this control does not use Javascript to set row colours
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/ResultsSummaryControl2.ascx.cs-arc  $
//
//   Rev 1.3   Oct 13 2008 16:44:22   build
//Automatically merged from branch for stream5014
//
//   Rev 1.2.1.0   Sep 17 2008 12:52:18   mmodi
//Updated for xhtml compliance
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.2   Mar 31 2008 13:22:42   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:17:20   mturner
//Initial revision.
//
//   Rev 1.0   Feb 20 2007 17:36:18   mmodi
//Initial revision.
//Resolution for 4350: CO2 Public Transport
//

using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

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
	public partial class ResultsSummaryControl2 : TDPrintableUserControl
	{
		#region Private variables 
		
		// constants for handling row selection
		private const string ODD_ROW = "eoddRow";
		private const string EVEN_ROW = "eevenRow";
	
		private FormattedJourneySummaryLines summaryLines; 

		private bool showTransportColumn = false;
		private bool showTransportRow = false;
		private bool showLeaveArriveDate = false;

		private int CommandSegment;

		#endregion

		#region Properties 
		/// <summary>
		/// Read/write property to handle the datasource for the control
		/// </summary>
		public FormattedJourneySummaryLines SummaryLines
		{
			get {return summaryLines;}
			set {summaryLines = value;}
		}

		/// <summary>
		/// Read/write property. Handles whether the Transport modes column should be drawn.
		/// </summary>
		public bool ShowTransportColumn
		{
			get { return showTransportColumn; }
			set { showTransportColumn = value; }
		}

		/// <summary>
		/// Read/write property. Handles whether the Transport modes row should be drawn.
		/// </summary>
		public bool ShowTransportRow
		{
			get { return showTransportRow; }
			set { showTransportRow = value; }
		}

		/// <summary>
		/// Read/write property. Handles whether the Date should be shown on the Leave/Arrive 
		/// time coloumns
		/// </summary>
		public bool ShowLeaveArriveDate
		{
			get { return showLeaveArriveDate; }
			set { showLeaveArriveDate = value; }
		}

		/// <summary>
		/// Read only property. Returns the table summary
		/// </summary>
		/// <returns></returns>
		protected string GetTableSummary
		{
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

		/// <summary>
		/// Read only property. Used to set the background colour of a row in the Html table.
		/// </summary>
		/// <param name="index"></param>
		/// <returns>CSS Class as string</returns>
		public string GetRowClass1(int index)
		{
			return (index % 2) == 0 ? EVEN_ROW : ODD_ROW;
		}

		/// <summary>
		/// Read only property. Used to set the background colour of a row in the Html table.
		/// Same as GetRowClass1 but gives us the opportunity to have a different colour for Transport
		/// modes row
		/// </summary>
		/// <param name="index"></param>
		/// <returns>CSS Class as string</returns>
		public string GetRowClass2(int index)
		{
			return (index % 2) == 0 ? EVEN_ROW : ODD_ROW;
		}

		#endregion

		#region Constructor

		/// <summary>
		/// Contructor for control
		/// </summary>
		public ResultsSummaryControl2()
		{
			// Set the resource file for the control
			this.LocalResourceManager = TDResourceManager.REFINE_JOURNEY_RM;
		}

		#endregion

		#region Page Initialisation

		/// <summary>
		/// Page Load 
		/// </summary>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Populate the repeater
			journeyRows.DataSource = summaryLines;
			journeyRows.DataBind();
		}

		/// <summary>
		/// Pre Render 
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

                    #region Item

                    // Get the object for the current row being bound so we can populate labels and stuff
					// Where the columns are not needed, find the TD elements and set to invisible
					FormattedJourneySummaryLine journeyLine = (FormattedJourneySummaryLine)e.Item.DataItem;

					// From label
					cellTag = (HtmlTableCell)e.Item.FindControl("tablecellOrigin");
					if (cellTag != null)
					{
						cellTag.Attributes.Add("class", "ejporigwide");
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
				
					#region Transport mode icons 
					// Because a journey has the potential to have lots of transport modes, we want to provide
					// the user the option to display these icons on a new row, hence new functionality
					// has been added to render as per existing functionality in the cell or now in a new row

					// Transport mode icons - Transport Cell
					cellTag = (HtmlTableCell)e.Item.FindControl("tablecellTransport");
					if (cellTag != null)
					{
						if (showTransportColumn)
						{
							cellTag.Visible = true;

							TransportModesControl trmControl = (TransportModesControl)e.Item.FindControl("itmTransportMode"); 
							if ( trmControl != null)
							{
								trmControl.DataSource = journeyLine.AllModes;
								trmControl.DataBind();
							} 
						}
						else
						{
							cellTag.Visible = false;
						}
					}

					// Transport mode icons - Transport Row
					HtmlTableRow rowTransport = (HtmlTableRow)e.Item.FindControl("rowTransport");
					if (rowTransport != null)
					{
						if (showTransportRow)
						{
							rowTransport.Visible = true;

							TransportModesControl trmControl = (TransportModesControl)e.Item.FindControl("itmTransportModeRow"); 
							if ( trmControl != null)
							{
								trmControl.DataSource = journeyLine.AllModes;
								trmControl.DataBind();
							}

							labelTag = (Label)e.Item.FindControl("labelTransport");
							if (labelTag != null)
							{
								labelTag.Text = GetResource("ResultsSummaryControl2.TravellingLabel.Text");
							}
						}
						else
						{
							rowTransport.Visible = false;
						}
					}

					#endregion

					// Leaving time
					labelTag = (Label)e.Item.FindControl("labelLeaveTime");
					if (labelTag != null)
					{
						if (showLeaveArriveDate)
							labelTag.Text = journeyLine.GetDepartureTime();
						else
							labelTag.Text = journeyLine.GetDepartureTimeNoDate();
					}

					// Arrival time
					labelTag = (Label)e.Item.FindControl("labelArriveTime");
					if (labelTag != null)
					{
						if (showLeaveArriveDate)
							labelTag.Text = journeyLine.GetArrivalTime();
						else
							labelTag.Text = journeyLine.GetArrivalTimeNoDate();
					}

					// Duration
					labelTag = (Label)e.Item.FindControl("labelDuration");
					if (labelTag != null)
					{
						labelTag.Text = journeyLine.GetAbbreviatedDuration();
                    }

                    #endregion

                    #region Header attributes required for screen reader

                    HtmlTableCell th = null;
                    HtmlTableCell td = null;

                    #region Column Origin
                    th = (HtmlTableCell)journeyRows.Controls[0].FindControl("tableheaderOrig");
                    if (th != null)
                    {
                        td = (HtmlTableCell)journeyRows.Controls[1].FindControl("tablecellOrigin");
                        if (td != null)
                        {
                            // Associate table cell with the header
                            td.Attributes.Add("headers", th.ClientID);
                        }
                    }
                    #endregion

                    #region Column Destination
                    th = (HtmlTableCell)journeyRows.Controls[0].FindControl("tableheaderDest");
                    if (th != null)
                    {
                        td = (HtmlTableCell)journeyRows.Controls[1].FindControl("tablecellDestination");
                        if (td != null)
                        {
                            // Associate table cell with the header
                            td.Attributes.Add("headers", th.ClientID);
                        }
                    }
                    #endregion

                    #region Column Transport
                    th = (HtmlTableCell)journeyRows.Controls[0].FindControl("tableheaderTransport");
                    if (th != null)
                    {
                        td = (HtmlTableCell)journeyRows.Controls[1].FindControl("tablecellTransport");
                        if (td != null)
                        {
                            // Associate table cell with the header
                            td.Attributes.Add("headers", th.ClientID);
                        }
                    }
                    #endregion

                    #region Column Leave Time
                    th = (HtmlTableCell)journeyRows.Controls[0].FindControl("tableheaderLeaveTime");
                    if (th != null)
                    {
                        td = (HtmlTableCell)journeyRows.Controls[1].FindControl("tablecellLeaveTime");
                        if (td != null)
                        {
                            // Associate table cell with the header
                            td.Attributes.Add("headers", th.ClientID);
                        }
                    }
                    #endregion

                    #region Column Arrive Time
                    th = (HtmlTableCell)journeyRows.Controls[0].FindControl("tableheaderArriveTime");
                    if (th != null)
                    {
                        td = (HtmlTableCell)journeyRows.Controls[1].FindControl("tablecellArriveTime");
                        if (td != null)
                        {
                            // Associate table cell with the header
                            td.Attributes.Add("headers", th.ClientID);
                        }
                    }
                    #endregion

                    #region Column Duration
                    th = (HtmlTableCell)journeyRows.Controls[0].FindControl("tableheaderDuration");
                    if (th != null)
                    {
                        td = (HtmlTableCell)journeyRows.Controls[1].FindControl("tablecellDuration");
                        if (td != null)
                        {
                            // Associate table cell with the header
                            td.Attributes.Add("headers", th.ClientID);
                        }
                    }
                    #endregion

                    #endregion

                    break;

				case ListItemType.Header:

                    #region Header
                    // Get the text for the items in the header

					// From column
					cellTag = (HtmlTableCell)e.Item.FindControl("tableheaderOrig");
					if (cellTag != null)
					{
						if (!showTransportColumn)
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
						if (!showTransportColumn)
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
						if (showTransportColumn)
						{
							cellTag.Visible = true;

							labelTag = (Label)e.Item.FindControl("headerlabelTransport");
							if (labelTag != null)
							{
								labelTag.Text = GetResource("ResultsSummaryControl.TransportLabel.Text");
							}
						}
						else
						{
							cellTag.Visible = false;
						}
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
                    #endregion

                    break;
				default :
					break;
			} 	
		}

		#endregion

		#region Private Methods

		/// <summary>
		/// Wire up extra events
		/// </summary>
		private void ExtraWiringEvents()
		{
			this.journeyRows.ItemDataBound += new System.Web.UI.WebControls.RepeaterItemEventHandler(this.journeyRows_ItemDataBound);
		}

		#endregion
	}
}
