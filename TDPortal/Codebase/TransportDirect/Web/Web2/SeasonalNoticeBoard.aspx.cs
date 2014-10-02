// ***********************************************
// NAME 		: SeasonalNoticeBoard.aspx.cs
// AUTHOR 		: Sanjeev Chand
// DATE CREATED : 01/11/2004
// DESCRIPTION 	: Page displaying latest Seasonal Information Data
// ************************************************

using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using TransportDirect.Common;
using TransportDirect.Common.ResourceManager;
using TransportDirect.UserPortal.Web;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.Resource;
using TransportDirect.UserPortal.TravelNews;
using TransportDirect.UserPortal.Web.Controls;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.Web.Support;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.UserPortal.SeasonalNoticeBoardImport ;   


// ***********************************************
// NAME 		: SeasonalNoticeBoard.aspx.cs
// AUTHOR 		: Sanjeev Chand
// DATE CREATED : 03/11/2004
// DESCRIPTION 	: Page displaying latest Seasonal notice board data 
// ************************************************

namespace  TransportDirect.UserPortal.Web.Templates
{
	/// <summary>
	/// Summary description for SeasonalNoticeBoard.
	/// This class displays the seasonal information data in control
	/// </summary>
	public partial class SeasonalNoticeBoard : TDPage
	{	
		#region "Page Members" 
		protected System.Web.UI.WebControls.HyperLink hyperlinkBack;   
		protected SeasonalNoticeBoardControl  ctrlSeasonalNoticeBoardControl;	
		protected TransportDirect.UserPortal.Web.Controls.HeaderControl headerControl;
        protected TransportDirect.UserPortal.Web.Controls.ExpandableMenuControl expandableMenuControl;
        protected TransportDirect.UserPortal.Web.Controls.ExpandableMenuControl informationLinksControl;
        protected TransportDirect.UserPortal.Web.Controls.ExpandableMenuControl relatedLinksControl;
        //protected TransportDirect.UserPortal.Web.Controls.ClientLinkControl clientLink;
		SeasonalNoticeBoardImport.SeasonalNoticeBoardHandler seasonalNoticeBoardInfoHandler;
		public static readonly int MINUTES = 10;
		#endregion
		
		#region "Page Constructor"
			public SeasonalNoticeBoard():base()
		{
			pageId = PageId.SeasonalNoticeBoard ;
		}
	
		#endregion

		#region "Page Events"
		protected void Page_Load(object sender, System.EventArgs e)
		{
			
			// Setting up Page header 			
			lblSeasonalTopHeader.Text = PageHeader ;

			// Setting text for back button 
			backButton.Text = GetResource("JourneyPlannerSummary.backButton.Text");

			// getting discovery data 
			seasonalNoticeBoardInfoHandler = (SeasonalNoticeBoardHandler)  TDServiceDiscovery.Current[ServiceDiscoveryKey.SeasonalNoticeBoardImport];
			
			
			//Setting up the column header 
			SetColumnheader(this.ctrlSeasonalNoticeBoardControl) ;

			//Populating the control with data
			PopulateSeasonalInformationControl(this.ctrlSeasonalNoticeBoardControl);

            // Left hand navigation menu set up
            expandableMenuControl1.AddContext(TransportDirect.UserPortal.SuggestionLinkService.Context.HomePageMenu);

            //added for white-labelling Related link part of side menu
            relatedLinksControl.AddContext(TransportDirect.UserPortal.SuggestionLinkService.Context.RelatedLinksContextSeasonalNoticeBoard);
			
           		
		}

		#endregion

		#region "Page Methods"

		/// <summary>
		/// Summary description for SetColumnheader.				
		/// </summary>
		public void SetColumnheader(SeasonalNoticeBoardControl ctrlSeasonalNoticeBoard)
		{	
			ctrlSeasonalNoticeBoard.RegionText = RegionText  ;
			ctrlSeasonalNoticeBoard.TransportModeText = TransportModeText ; 			
			ctrlSeasonalNoticeBoard.InformationText = InformationText ;
			ctrlSeasonalNoticeBoard.EffectedDatesText = EffectedDatesText ;
			ctrlSeasonalNoticeBoard.LastUpdatedText = LastUpdatedText ;				
			ctrlSeasonalNoticeBoard.NoticeHeadingText = NoticeHeadingText; 
			ctrlSeasonalNoticeBoard.NoDataFoundMessage = NoDataFoundMessage;	
			
			// now setting the table summary 
			ctrlSeasonalNoticeBoard.TableSummaryText =  TableSummary;
		}

				
		/// <summary>
		/// Summary description for PopulateSeasonalInformationControl		
		/// It populates the seasonal information data in the control.
		/// </summary>
		private void PopulateSeasonalInformationControl(SeasonalNoticeBoardControl ctrlSeasonalNoticeBoard)
		{
			DataTable SeasonalInfo;			
			try
			{
				// get the data from handler
				SeasonalInfo = (DataTable)seasonalNoticeBoardInfoHandler.GetData(); 
				
				// setting up the datasource 
				ctrlSeasonalNoticeBoard.DataSource = SeasonalInfo;

				// Displaying the data
				ctrlSeasonalNoticeBoard.DisplayData();  
				
			}
			catch(Exception ex)
			{
				throw new Exception("Exception occured in PopulateSeasonalInformationControl " + ex.Message.ToString());    
			}
			finally
			{
				SeasonalInfo = null;
			}
		}

		/// <summary>		
		/// It populates the seasonal information header text from the db
		/// </summary>
		private string GetNoticeBoardHeaderText()
		{
			string strSeasonalHeader ; 
			try
			{
				
				strSeasonalHeader = Properties.Current["SeasonalNoticeBoardControl.SeasonalNoticeboardHeading.Text"];  

				if (strSeasonalHeader==null || strSeasonalHeader.Length == 0 )
				{
					return "";
				}
				else
				{
					return (string)strSeasonalHeader.Trim()  ;
				}

			}
			catch (Exception ex)
			{
				string temp = ex.InnerException.Message;
				return "" ;
			}
		
		}
		
		
#endregion

		#region "Page Properties"
		
		/// <summary>		
		/// Gets the page header from the resource file
		/// </summary>
		private string PageHeader
		{
			get
			{			
				return Global.tdResourceManager.GetString("SeasonalNoticeBoard.PageHeader", TDCultureInfo.CurrentUICulture);
			}
		}

		/// <summary>		
		/// Gets the page Region column header from the resource file
		/// </summary>
		private string RegionText
		{
			get
			{			
				return Global.tdResourceManager.GetString(
					"SeasonalNoticeBoard.RegionText", TDCultureInfo.CurrentUICulture);
			}
		}

		/// <summary>		
		/// Gets the page Mode of transport column header from the resource file
		/// </summary>
		private string TransportModeText
		{
			get
			{			
				return Global.tdResourceManager.GetString(
					"SeasonalNoticeBoard.TransportModeText", TDCultureInfo.CurrentUICulture);
			}
		}
		
		/// <summary>		
		/// Gets the page Information column header from the resource file
		/// </summary>
		private string InformationText
		{
			get
			{			
				return Global.tdResourceManager.GetString(
					"SeasonalNoticeBoard.InformationText", TDCultureInfo.CurrentUICulture);
			}
		}
		
		/// <summary>		
		/// Gets the page Effected dates column header from the resource file
		/// </summary>
		private string EffectedDatesText
		{
			get
			{			
				return Global.tdResourceManager.GetString(
					"SeasonalNoticeBoard.EffectedDatesText", TDCultureInfo.CurrentUICulture);
			}
		}

		/// <summary>		
		/// Gets the page Last Updated column header from the resource file
		/// </summary>
		private string LastUpdatedText
		{
			get
			{			
				return Global.tdResourceManager.GetString(
					"SeasonalNoticeBoard.LastUpdatedText", TDCultureInfo.CurrentUICulture);
			}
		}
		
		
		/// <summary>		
		/// Gets the page notice header 
		/// </summary>
		private string NoticeHeadingText
		{
			get
			{			
				return GetNoticeBoardHeaderText();
			}
		}

		/// <summary>		
		/// Gets the page no data message from the resource file
		/// </summary>
		private string NoDataFoundMessage
		{
			get
			{			
				return Global.tdResourceManager.GetString(
					"SeasonalNoticeBoard.NoDataFoundMessage", TDCultureInfo.CurrentUICulture);
			}
		}

		/// <summary>		
		/// Gets the date and time dynamically 
		/// </summary>
		private string PageDateTime
		{
			get
			{
				//display correct time and date
				TDDateTime current = TDDateTime.Now;
				DateTime dt = current.GetDateTime();
				int minutes = current.Minute;

				string mins = string.Empty;
				if(minutes < MINUTES)
					mins = "0" + minutes.ToString();
				else
					mins = minutes.ToString();

				string datetime = current.GetDateTime().Date.ToLongDateString() + ", "
					+ current.Hour.ToString() + ":" + mins;
				
				return datetime;

			}
		}


		/// <summary>		
		/// Gets the Table summary for WAI
		/// </summary>
		private string TableSummary
		{
			get
			{			
				return Global.tdResourceManager.GetString("SeasonalNoticeBoard.TableSummary", TDCultureInfo.CurrentUICulture);
			}
		}


		#endregion

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
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    

		}	
		#endregion

		#region Event Handling code

		private void ExtraWiringEvents() 
		{
			this.backButton.Click += new EventHandler(this.backButton_Click);
		}

        /// <summary>
        /// Handler for the Back button click event.
        /// Calls the SeasonalNoticeBoardBack TransitionEvent processing to 
        /// perform appropriate action depending where we've come from.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backButton_Click(object sender, EventArgs e)
        {
            TransitionEvent te = TransitionEvent.SeasonalNoticeBoardBack;

            TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = te;
        }

		#endregion Event Handling code
	}
}
