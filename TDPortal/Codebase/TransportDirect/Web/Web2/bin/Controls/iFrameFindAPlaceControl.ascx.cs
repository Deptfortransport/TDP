namespace TransportDirect.UserPortal.Web.Controls
{
	#region Using statements
	using System;
	using TransportDirect.Common.ResourceManager;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using TransportDirect.UserPortal.Web.Controls;
	using TransportDirect.UserPortal.Web;
	using TransportDirect.Web.Support;
	using TransportDirect.Common.ServiceDiscovery;	
	using TransportDirect.UserPortal.DataServices;  
	using TransportDirect.UserPortal.SessionManager;  
	using TransportDirect.Common;
	using TransportDirect.UserPortal.Resource;
	using TransportDirect.UserPortal.LocationService; 
	using TransportDirect.UserPortal.Web.Adapters;  
	using TransportDirect.UserPortal.ScreenFlow;
	using TransportDirect.ReportDataProvider.TDPCustomEvents;
	using Logger = System.Diagnostics.Trace;
	using System.Text;
	using TransportDirect.Common.Logging;
	using System.Globalization;
	using TransportDirect.Partners;

	#endregion
	

	/// <summary>
	///		iFrameFindAPlaceControl to be used for iFrames.
	/// </summary>	
	[System.Runtime.InteropServices.ComVisible(false)] 
	public partial class iFrameFindAPlaceControl : System.Web.UI.UserControl
	{   		
		
		#region Protected Members
		protected IDataServices populator;

		protected TDResourceManager resourceManager = Global.tdResourceManager;

		//storing the partner Id extracted from query string in hidden field

		#endregion


		#region Event Handlers
		/// <summary>
		/// Event handler for Page Load Event
		/// </summary>
		/// <param name="sender"> sender object</param>
		/// <param name="e">Event Arguments</param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			string validID;
			string buttonImageUrl = "Iframes.SearchButtonImageURL.";
			// Update control for dropdown list, image path, soft content etc
			UpdateControl();

			if ((!IsPostBack))
			{
				partnerId.Value = Server.HtmlDecode(GetValidParam("pid"));
				validID = CheckPartnerId(partnerId.Value);
				if(validID == String.Empty)
				{
					partnerId.Value = "unknown";
				}
				buttonImageUrl +=  partnerId.Value;
				buttonSubmit.ImageUrl = resourceManager.GetString(buttonImageUrl, TDCultureInfo.CurrentUICulture);
				//Mis logging				
				LandingPageEntryEvent lpee = new LandingPageEntryEvent(partnerId.Value, LandingPageService.iFrameFindAPlace, "SessionLess", false);
				Logger.Write(lpee);
			}
		}

		/// <summary>
		/// Page Initial Event 
		/// </summary>
		/// <param name="sender"> sender object</param>
		/// <param name="e">Event Arguments</param> 
		protected void Page_Init(object sender, System.EventArgs e)
		{
			// Poulating dropdown list
			populator  = (IDataServices) TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];
           
		}

		
		/// <summary>
		///	 Submit Button Event Handler
		/// </summary>
		/// <param name="sender"> sender object</param>
		/// <param name="e">Event Arguments</param>
		private void buttonSubmit_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			StringBuilder redirectURL = new StringBuilder("/Web2/iframes/LocationLandingPage.aspx?place=");
			redirectURL.Append(Server.UrlEncode(this.textBoxPlace.Text));
			redirectURL.Append("&locGaz=");
			redirectURL.Append(this.dropDownLocationGazetteerOptions.SelectedValue.ToString());
			redirectURL.Append("&locOpt=");
			redirectURL.Append(this.dropDownLocationShowOptions.SelectedValue.ToString());
			redirectURL.Append("&pid=");
			redirectURL.Append(Server.UrlEncode(this.partnerId.Value));

			Response.Redirect(redirectURL.ToString());


//			//Log submit event
//			PageEntryEvent logPage = new PageEntryEvent(PageId.HomePageFindAPlacePlanner, Session.SessionID, TDSessionManager.Current.Authenticated);
//			Logger.Write(logPage);
		}

		#endregion
		
		#region Private & Protected Methods
       
		/// <summary>
		/// Update the control with resource text and list items
		/// </summary>
		private void UpdateControl()
		{
			// populate dropdown list for place type and show options
			if ((!IsPostBack))
				PopulateList();


			// Updating Image url path
			imageMap.ImageUrl = resourceManager.GetString("FindAPlaceControl.imageMap.ImageUrl", TDCultureInfo.CurrentUICulture);
			// Updating alt text 
			imageMap.AlternateText =  resourceManager.GetString("FindAPlaceControl.imageMap.AltText", TDCultureInfo.CurrentUICulture);
			hyperlinkFindAPlaceMore.ToolTip = resourceManager.GetString("FindAPlaceControl.imageMap.AltText", TDCultureInfo.CurrentUICulture);

			buttonSubmit.ToolTip = resourceManager.GetString("FindAPlaceControl.buttonSubmit.AltText", TDCultureInfo.CurrentUICulture); 

			// Getting text for Labels
			labelPlace.Text = resourceManager.GetString("FindAPlaceControl.labelPlace.Text", TDCultureInfo.CurrentUICulture);

			// setting screen reader field
			labelPlaceTypeScreenReader.Text = resourceManager.GetString("FindAPlaceControl.labelPlaceTypeScreenReader.Text", TDCultureInfo.CurrentUICulture);

			// setting show option field
			textBoxShowOption.Text = resourceManager.GetString("FindAPlaceControl.textboxShowOption.Text", TDCultureInfo.CurrentUICulture);

			// setting the text for submit button
			//buttonSubmit.Text = resourceManager.GetString("FindAPlaceControl.buttonSubmit.Text", TDCultureInfo.CurrentUICulture); 
		}
  

		/// <summary>
		/// Polulate the dropdown list with the list items
		/// </summary>
		private void PopulateList()
		{
		  
			// Populating LocationGazeteerOptions
			populator.LoadListControl(
				DataServiceType.FindLocationGazeteerOptions, dropDownLocationGazetteerOptions, resourceManager); 

			// Populating FindLocationShowOptions
			populator.LoadListControl(
				DataServiceType.FindLocationShowOptions, dropDownLocationShowOptions, resourceManager); 
         		 
 
		}

		/// <summary>
		/// Check that the partner Id is valid
		/// </summary>
		/// <param name="partnerId">string representation of Partner ID</param>
		/// <returns>string Partner Id</returns>
		private string CheckPartnerId(string partnerId)
		{
			string ID = String.Empty;
			PartnerCatalogue partnerCatalogue =  (PartnerCatalogue)TDServiceDiscovery.Current[ServiceDiscoveryKey.PartnerCatalogue];  
			try
			{
				ID = partnerCatalogue.GetPartnerIdFromHostName(partnerId).ToString(CultureInfo.InvariantCulture);  							  
			}
			catch
			{
				LogError("Partner ID provided is not a recognised partner id. ", partnerId);
			}
			return ID;
		}

		/// <summary>
		/// Write operational events to the log, logging ip address and Partner ID
		/// </summary>
		/// <param name="description">The description of the error</param>
		/// <param name="partnerId">The error id</param>
		private void LogError (string description, string partnerId)
		{
			StringBuilder message = new StringBuilder(string.Empty);
			message.Append(description);
			message.Append(" Partner ID : ");
			message.Append(partnerId);
			message.Append(" client-ip : ");
			message.Append(Page.Request.UserHostAddress.ToString(CultureInfo.InvariantCulture));
			message.Append(" url-referrer : ");
			message.Append(Page.Request.UrlReferrer);

			OperationalEvent oe = new  OperationalEvent(
				TDEventCategory.Business, TDTraceLevel.Error, message.ToString());

			Logger.Write(oe);
		}

		/// <summary>
		/// Parses the query string variables and return param value
		/// </summary>
		/// <param name="paramName">Query string Parameter to be fetched</param>
		/// <returns></returns>
		private string GetValidParam(string paramName)
		{
			string tempCache = string.Empty;
			switch (paramName)
			{
				case "pid":
					tempCache = Server.UrlDecode(Page.Request.Params.Get(paramName));
					break;

				default:
					tempCache = string.Empty;
					break;

			}

			if (tempCache!= null)
			{
				return Server.HtmlEncode(tempCache);
			}
			else
			{
				return string.Empty;
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
			base.OnInit(e);
		}
		
		/// <summary>
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.buttonSubmit.Click += new System.Web.UI.ImageClickEventHandler(this.buttonSubmit_Click);
		}
		#endregion
		
	}
}
