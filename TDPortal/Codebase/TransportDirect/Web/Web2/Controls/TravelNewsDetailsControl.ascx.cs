// ****************************************************************** 
// NAME         : TravelNewsDetailsControl.ascx
// AUTHOR       : Joe Morrissey
// DATE CREATED : 13/06/2005
// DESCRIPTION  : Displays travel news details in a repeater control
// ****************************************************************** 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/TravelNewsDetailsControl.ascx.cs-arc  $
//
//   Rev 1.9   Oct 24 2011 10:47:04   mmodi
//Updated to display travel news toids for CJP user
//Resolution for 5758: Real Time in Car - Display TOIDs on incident popup for CJP user
//
//   Rev 1.8   Oct 28 2010 13:00:18   RBroddle
//Removed explicit wire up to Page_PreRender as AutoEventWireUp=true for this control so it was firing twice.
//Resolution for 5621: USD8048975 - page_init and other events wired up more than once on some pages and controls
//
//   Rev 1.7   Jul 07 2010 12:20:14   apatel
//updated to hide the no record found message displaying on printer friendly page
//Resolution for 5567: Travel news printer friendly page no incidents reported message error
//
//   Rev 1.6   Jan 11 2010 16:51:42   mmodi
//Updated to display no incidents found message through javascript
//Resolution for 5362: Maps - Message not shown when no incidents found
//
//   Rev 1.5   Nov 26 2009 15:47:26   apatel
//TravelNews page and controls updated for new mapping functionality
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.4   Jan 02 2009 13:48:34   apatel
//XHTML Compliance Changes
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.3   Dec 19 2008 15:49:28   apatel
//XHTML Compliance Changes
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.2   Mar 31 2008 13:23:26   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:18:26   mturner
//Initial revision.
//
//   Rev 1.9   Mar 28 2006 11:09:06   build
//Automatically merged from branch for stream0024
//
//   Rev 1.8.1.2   Mar 10 2006 13:41:52   AViitanen
//Tidying up. 
//Resolution for 24: DEL 8.1 Workstream - Travel News Updates
//
//   Rev 1.8.1.1   Mar 09 2006 14:38:28   AViitanen
//Updated to use StandardTNDateAndTimeFormat. 
//Resolution for 24: DEL 8.1 Workstream - Travel News Updates
//
//   Rev 1.8.1.0   Mar 03 2006 17:54:26   AViitanen
//Travel news updates updates. 
//Resolution for 24: DEL 8.1 Workstream - Travel News Updates
//
//   Rev 1.8   Feb 23 2006 16:14:14   halkatib
//Merge of stream3129 enhanced exposed services
//
//   Rev 1.7   Feb 10 2006 15:04:44   build
//Automatically merged from branch for stream3180
//
//   Rev 1.6.1.0   Dec 01 2005 12:20:22   AViitanen
//Refactored to use TDLinkButton as part of JavaScript changes.
//Resolution for 3180: DEL 8.1 Stream UEE: work on Homepage phase 2
//
//   Rev 1.6   Aug 31 2005 15:15:02   mguney
//Visibility check is done for the control before setting the no records label.
//Resolution for 2700: DN018 - No user error message when  selection criteria returns no incidents
//
//   Rev 1.5   Aug 31 2005 14:25:16   mguney
//DEL 8 - Added lblNoRecords to display no records message.
//
//   Rev 1.4   Aug 31 2005 14:19:36   mguney
//Resolution for IR2700: DEL 8 - Added lblNoRecords to display no records message.
//
//   Rev 1.3   Aug 04 2005 11:27:46   jgeorge
//Corrections
//Resolution for 2558: Del 8 Stream: Incident mapping
//
//   Rev 1.2   Jul 08 2005 14:54:32   jbroome
//Updated control - added command event for link buttons
//Resolution for 2558: Del 8 Stream: Incident mapping
//
//   Rev 1.1   Jul 01 2005 16:56:04   jmorrissey
//Updated GetRowStyle and GetAlternatingRowStyle methods
//Resolution for 2558: Del 8 Stream: Incident mapping
//
//   Rev 1.0   Jul 01 2005 11:15:36   jmorrissey
//Initial revision.
//Resolution for 2558: Del 8 Stream: Incident mapping

namespace TransportDirect.UserPortal.Web.Controls
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using TransportDirect.UserPortal.TravelNewsInterface;
	using TransportDirect.UserPortal.Web.Controls;
	using TransportDirect.Web.Support;
	using TransportDirect.UserPortal.TravelNews;
	using TransportDirect.Common.ResourceManager;
	using TransportDirect.Common;	
	using TransportDirect.UserPortal.Web.Adapters;
    using System.Text;

	/// <summary>
	///	Summary description for TravelNewsDetailsControl.
	/// </summary>
	public partial class TravelNewsDetailsControl : TDPrintableUserControl
	{

		# region Instance members


		private TravelNewsItem[] newsItems;
		private DisplayType displayType;
        private bool isCjpUser = false;

		public event CommandEventHandler IncidentClicked;

		#endregion

		#region Private Methods

		/// <summary>
		/// Page load data binds controls
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			UpdateDetailsTable();
		}

		/// <summary>
		/// Pre Render data binds controls
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_PreRender(object sender, System.EventArgs e)
		{
			UpdateDetailsTable();
		}

		/// <summary>
		/// Binds the data repeater and controls to the appropriate data
		/// </summary>
		/// <param name="PostBack"></param>
		private void UpdateDetailsTable()
		{
			repeaterTravelIncidents.DataSource = newsItems;
			this.DataBind();

			//Check whether the control is visible before setting the no records label.
			if ((this.Visible) && (newsItems != null))
			{
				lblNoRecords.Text = GetResource("ShowNewsControl.ErrorMessage.NoIncidentsFound");

                bool javaScriptSupported = ((TDPage)Page).IsJavascriptEnabled;

                // If javascript supported, hide the label using CSS - this then allows the TravelNews 
                // javascript to subsequently display the label if no news items are found
                if (javaScriptSupported && !this.PrinterFriendly)
                {
                    // If there are items hide label
                    if (newsItems.Length > 0)
                    {
                        if (!lblNoRecords.CssClass.Contains("hide"))
                        {
                            lblNoRecords.CssClass += " hide";
                        }
                    }
                }
                else
                {
                    lblNoRecords.Visible = (newsItems.Length == 0);
                }
                
			}		
		}	

		/// <summary>
		/// Event is fired after an item has been bound to the repeater
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void travelIncidents_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
		{
			// Check if javascript is enabled
			bool javaScriptSupported = ((TDPage)Page).IsJavascriptEnabled;

			// If the user has javascript enabled and the incident is a road incident
			// then show a link button rather than a label in Incident column
			if (e.Item.ItemType == ListItemType.Item || 
				e.Item.ItemType == ListItemType.AlternatingItem) 
			{
				string incidentType = ((TravelNewsItem)e.Item.DataItem).ModeOfTransport;
				Label incidentLabel = (Label)e.Item.FindControl("lblIncident");
				TDLinkButton linkIncident = (TDLinkButton)e.Item.FindControl("linkIncident");
				
				if ((javaScriptSupported) && (!PrinterFriendly)) 
				{
					// Hide the incident label and display the hyperlink
					incidentLabel.Visible = false;
					linkIncident.Visible = true;

					// Set the incident hyperlink properties
					linkIncident.Text = ((TravelNewsItem)e.Item.DataItem).IncidentType;

                    linkIncident.ClientSideJavascript = string.Format("ShowSingleIncident('{0}',{1},{2},{3});return false;", ((TravelNewsItem)e.Item.DataItem).Uid, ((TravelNewsItem)e.Item.DataItem).Easting, ((TravelNewsItem)e.Item.DataItem).Northing, TravelNewsHelper.DefaultIncidentZoomLevel);
				}
			}
		}

        /// <summary>
        /// Returns the toids list as a string
        /// </summary>
        /// <param name="toidsList"></param>
        /// <returns></returns>
        private string GetTOIDsString(string[] toidsList)
        {
            StringBuilder toids = new StringBuilder();
            if (toidsList != null)
            {
                foreach (string toid in toidsList)
                {
                    toids.Append(toid);
                    toids.Append(", ");
                }
            }
            
            return toids.ToString().TrimEnd(new char[2] { ',', ' ' });
        }

		#endregion

		#region Protected Properties and Methods

		/// <summary>
		/// Returns the location for this data item
		/// </summary>
		/// <param name="containerDataItem">Travel news data item</param>
		/// <returns>string</returns>
		protected string AffectedDescriptionText (object containerDataItem)
		{
			return ((TravelNewsItem)containerDataItem).Location;
		}


		/// <summary>
		/// Returns the IncidentType for this data item e.g Roadworks
		/// </summary>
		/// <param name="containerDataItem">Travel news data item</param>
		/// <returns>string</returns>
		protected string IncidentTypeText (object containerDataItem)
		{
			return ((TravelNewsItem)containerDataItem).IncidentType;
		}

		/// <summary>
		/// Returns the SeverityDescription for this data item e.g Severe
		/// </summary>
		/// <param name="containerDataItem">Travel news data item</param>
		/// <returns>string</returns>
		protected string SeverityDescriptionText (object containerDataItem)
		{
			return ((TravelNewsItem)containerDataItem).SeverityDescription;
		}

		/// <summary>
		/// Returns the StartDateTime for this data item e.g 04/05/2005 14:26:28
		/// </summary>
		/// <param name="containerDataItem">Travel news data item</param>
		/// <returns>string</returns>
		protected string StartDateTimeText (object containerDataItem)
		{
			return DisplayFormatAdapter.StandardTNDateAndTimeFormat(((TravelNewsItem)containerDataItem).StartDateTime);
		}

		/// <summary>
		/// Returns the ModeOfTransport for this data item e.g Road
		/// </summary>
		/// <param name="containerDataItem">Travel news data item</param>
		/// <returns>string</returns>
		protected string ModeOfTransportText (object containerDataItem)
		{
			return ((TravelNewsItem)containerDataItem).ModeOfTransport;
		}

		/// <summary>
		/// Returns the LastModifiedDateTime for this data item e.g 30/04/2005 18:31:02
		/// </summary>
		/// <param name="containerDataItem">Travel news data item</param>
		/// <returns>string</returns>
		protected string LastUpdatedText (object containerDataItem)
		{
			return DisplayFormatAdapter.StandardTNDateAndTimeFormat(((TravelNewsItem)containerDataItem).LastModifiedDateTime);

		}

		/// <summary>
		/// Returns the Uid for this data item e.g. RTM15113
		/// </summary>
		/// <param name="containerDataItem">Travel news data item</param>
		/// <returns>string</returns>
		protected string CommandArgumentUid (object containerDataItem)
		{
			return ((TravelNewsItem)containerDataItem).Uid;
		}

		/// <summary>
		/// Returns either the HeadlineText or the Detail Text for this data item 
		/// </summary>
		/// <param name="containerDataItem">a travel news data item</param>
		/// <returns></returns>
		protected string DetailsItemText(object containerDataItem)
		{
			// Return correct text, based on display type
			switch (displayType)
			{
				case (DisplayType.Summary):
					return ((TravelNewsItem)containerDataItem).HeadlineText;
				default:
				case (DisplayType.Full):
                    if (!isCjpUser)
                    {
                        return ((TravelNewsItem)containerDataItem).DetailText;
                    }
                    else
                    {
                        return string.Format("{0}<br /><span class=\"cjperror\">{1}</span>",
                            ((TravelNewsItem)containerDataItem).DetailText,
                            GetTOIDsString(((TravelNewsItem)containerDataItem).AffectedToids));
                    }
            
			}			
		}

		/// <summary>
		/// Event handler for the command event of the link buttons within the repeater
		/// Simply raises public event to be consumed by the page
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void linkIncident_Command(object sender, CommandEventArgs e)
		{
			if (IncidentClicked != null)
			{				
				IncidentClicked(this, e);		
			}
		}

		/// <summary>
		///Read only property that returns the language specific text for the Last Updated column header  
		/// </summary>
		protected string LastUpdatedHeaderText
		{
			get	{ return GetResource("TravelNews.lblLastUpdated.Text"); }
		}

		/// <summary>
		/// Read only property returns the language specific text for the Incident column header  
		/// </summary>
		protected string IncidentHeaderText
		{
			get { return GetResource("TravelNews.lblIncident.Text"); }
		}

		/// <summary>
		/// Read only property that returns the language specific text for the Severity column header  
		/// </summary>
		protected string SeverityHeaderText
		{
			get { return GetResource("TravelNews.lblSeverity.Text"); }
		}

		/// <summary>
		/// Read only property that returns the language specific text for the Occurred column header  
		/// </summary>
		protected string OccurredHeaderText
		{
			get { return GetResource("TravelNews.lblOccurred.Text"); }
		}

		/// <summary>
		///Read only property that returns the language specific text for the Affected column header  
		/// </summary>
		protected string AffectedHeaderText
		{
			get { return GetResource("TravelNews.lblAffected.Text"); }
		}


		/// <summary>
		///Read only property that returns the language specific text for the Details column header  
		/// </summary>
		protected string DetailsHeaderText
		{
			get { return GetResource("TravelNews.lblDetails.Text"); }
		}
				
		#endregion

		#region Public Properties

		/// <summary>
		/// Read/write property array of TravelNewsItems
		/// Used to bind to the repeater control
		/// </summary>
		public TravelNewsItem[] NewsItems
		{
			get { return newsItems; }
			set { newsItems = value; }
		}

		/// <summary>
		/// Read/write property 
		/// Display type for this control - either summary or details
		/// </summary>
		public DisplayType IncidentDisplayType
		{
			get { return displayType; }
			set { displayType = value; }
		}

        /// <summary>
        /// Read/write property
        /// If cjp user, addtional travel news details will be displayed
        /// </summary>
        public bool CJPUser
        {
            get { return isCjpUser; }
            set { isCjpUser = value; }
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
		///	Required method for Designer support - do not modify
		///	the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.repeaterTravelIncidents.ItemDataBound += new System.Web.UI.WebControls.RepeaterItemEventHandler(this.travelIncidents_ItemDataBound);
        }
		#endregion

	}
}
