// *********************************************** 
// NAME                 : iFrameJourneyPlanningControl.ascx
// AUTHOR               : Darshan
// DATE CREATED         : 11/12/2006
// DESCRIPTION  		: iframe page Plan a journey control
// ************************************************ 

namespace TransportDirect.UserPortal.Web.Controls
{
	using System;	
	using TransportDirect.Common.ResourceManager;
	using System.Collections;
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
	using TransportDirect.JourneyPlanning.CJPInterface;
	using ControlPopulator = TransportDirect.UserPortal.DataServices.DataServices;
	using TransportDirect.ReportDataProvider.TDPCustomEvents;
	using Logger = System.Diagnostics.Trace;
	using TransportDirect.Common.PropertyService.Properties;
	using TransportDirect.Common.Logging;
	using System.Globalization;
	using System.Text;
	using TransportDirect.Partners;

	/// <summary>
	///		Summary description for iFrameJourneyPlanningControl.
	/// </summary>
	public partial class iFrameJourneyPlanningControl : System.Web.UI.UserControl
	{   		
		#region Control Declarations
		// Label Controls

		// TextBox Controls

		// DropDownList Controls

		// CheckBox Controls

		// Custom Controls
		protected TransportDirect.UserPortal.Web.Controls.iFrameAmbiguousDateSelectControl ambiguousDateSelectControl;
		#endregion

		#region Object Members Declarations
		protected TDResourceManager resourceManager = Global.tdResourceManager;

		// Data Services
		protected IDataServices populator;
		private IDataServices ds = (IDataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];

		//Variables storing default outward Date time
		private string outwardDayOfMonth = String.Empty;
		private string outwardMonthYear = String.Empty;
		private string outwardHour = String.Empty;
		private string outwardMinute = String.Empty;
		
		//storing the partner Id extracted from query string in hidden field

		#endregion

		/// <summary>
		/// Page Load Method
		/// </summary>
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
				LandingPageEntryEvent lpee = new LandingPageEntryEvent(partnerId.Value, LandingPageService.iFrameJourneyPlanning, "SessionLess", false);
				Logger.Write(lpee);
			}
		}

		/// <summary>
		/// Page Initialise Method
		/// </summary>
		protected void Page_Init(object sender, System.EventArgs e)
		{
			// Populating the dropdown lists
			populator  = (IDataServices) TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];
		}

		

		/// <summary>
		/// Event handler for clicks of the GO button
		/// </summary>
		private void buttonSubmit_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			StringBuilder redirectURL = new StringBuilder("/Web2/iframes/JourneyLandingPage.aspx?from=");
			
			redirectURL.Append(this.fromDropDownLocationGazeteerOptions.SelectedValue.ToString());
			redirectURL.Append("&txtFrom=");
			redirectURL.Append(Server.UrlEncode(this.textBoxFrom.Text));
			redirectURL.Append("&to=");
			redirectURL.Append(this.toDropDownLocationGazeteerOptions.SelectedValue.ToString());
			redirectURL.Append("&txtTo=");
			redirectURL.Append(Server.UrlEncode(this.textBoxTo.Text));
			redirectURL.Append("&day=");
			redirectURL.Append(this.ambiguousDateSelectControl.Day.ToString());
			redirectURL.Append("&monYr=");
			redirectURL.Append(Server.UrlEncode(this.ambiguousDateSelectControl.MonthYear));
			redirectURL.Append("&hr=");
			redirectURL.Append(this.ambiguousDateSelectControl.Hour.ToString());
			redirectURL.Append("&min=");
			redirectURL.Append(this.ambiguousDateSelectControl.Minute.ToString());
			redirectURL.Append("&public=");
			redirectURL.Append(this.checkBoxPublicTransport.Checked);
			redirectURL.Append("&car=");
			redirectURL.Append(this.checkBoxCarRoute.Checked);
			redirectURL.Append("&pid=");
			redirectURL.Append(Server.UrlEncode(this.partnerId.Value));
			
			Response.Redirect(redirectURL.ToString());
		}
				
		/// <summary>
		/// Populates the labels/buttons with text from the Resource files
		/// </summary>
		private void UpdateControl()
		{					
			// populate dropdown list for place type and show options			
			if ((!IsPostBack))
			{				
				PopulateList();	
			}

			// Updatng Image url path & AlternateText
			imageDoorToDoor.ImageUrl = resourceManager.GetString("PlanAJourneyControl.imageDoorToDoor.ImageUrl", TDCultureInfo.CurrentUICulture);
			imageDoorToDoor.AlternateText = resourceManager.GetString("PlanAJourneyControl.imageDoorToDoor.AlternateText", TDCultureInfo.CurrentUICulture);
			
			
			// Tool tips for Buttons
			buttonSubmit.ToolTip = resourceManager.GetString("PlanAJourneyControl.buttonSubmit.AlternateText", TDCultureInfo.CurrentUICulture);
			
			// Getting text for Labels
			labelFrom.Text = resourceManager.GetString("PlanAJourneyControl.labelFrom.Text", TDCultureInfo.CurrentUICulture);
			labelTo.Text = resourceManager.GetString("PlanAJourneyControl.labelTo.Text", TDCultureInfo.CurrentUICulture);
			labelLeave.Text = resourceManager.GetString("PlanAJourneyControl.labelLeave.Text", TDCultureInfo.CurrentUICulture);
			labelShow.Text = resourceManager.GetString("PlanAJourneyControl.labelShow.Text", TDCultureInfo.CurrentUICulture);

			// Setting screen reader field
			labelFromPlaceTypeScreenReader.Text = resourceManager.GetString("PlanAJourneyControl.labelFromPlaceTypeScreenReader.Text", TDCultureInfo.CurrentUICulture);
			labelToPlaceTypeScreenReader.Text = resourceManager.GetString("PlanAJourneyControl.labelToPlaceTypeScreenReader.Text", TDCultureInfo.CurrentUICulture);
			
			// Getting text for CheckBoxes
			checkBoxPublicTransport.Text = resourceManager.GetString("PlanAJourneyControl.checkboxPublicTransport.Text", TDCultureInfo.CurrentUICulture);
			checkBoxCarRoute.Text = resourceManager.GetString("PlanAJourneyControl.checkboxCarRoute.Text", TDCultureInfo.CurrentUICulture);

			// Getting text for TDButtons
			//buttonSubmit.Text = resourceManager.GetString("PlanAJourneyControl.buttonSubmit.Text", TDCultureInfo.CurrentUICulture);
		}

		/// <summary>
		/// Populates the drop down list controls
		/// </summary>
		private void PopulateList()
		{
			// Populating LocationGazeteerOptions
			// origin DropDown
			populator.LoadListControl(
				DataServiceType.FindLocationGazeteerOptions, fromDropDownLocationGazeteerOptions, resourceManager ); 
			// destination DropDown
			populator.LoadListControl(
				DataServiceType.FindLocationGazeteerOptions, toDropDownLocationGazeteerOptions, resourceManager ); 
			
			// Populate AmbiguousDateSelectControl
			ambiguousDateSelectControl.Populate();

		
			
			TDDateTime leaveDateTime = InitialiseDefaultOutwardTime();
			outwardDayOfMonth = leaveDateTime.ToString("dd");
			outwardMonthYear = leaveDateTime.ToString("MM")
				+ "/"
				+ leaveDateTime.ToString("yyyy");

			ambiguousDateSelectControl.Day = outwardDayOfMonth;
			ambiguousDateSelectControl.MonthYear = outwardMonthYear;
			ambiguousDateSelectControl.Hour = outwardHour;
			ambiguousDateSelectControl.Minute = outwardMinute;
		}

		/// <summary>
		/// Initialises the default Outward Time and returns the TDDateTime object used for 
		/// this initialisation
		/// </summary>
		/// <returns>TDDateTime to use for further initialisation</returns>
		public TDDateTime InitialiseDefaultOutwardTime()
		{
			// Outward Date : Same day (or next day if time is in evening)
			// Outward Time : +15 minutes or next day if in the evening for multimodal, any time if Find a Flight
			
			// Get evening and morning properties
			string evening = Properties.Current["journeyparameters.eveningtime"];
			string morning = Properties.Current["journeyparameters.morningtime"];
			string sMinutesToAdd = Properties.Current["journeyparameters.minutestoadd"];

			int eveningHour = 0;
			int eveningMinute = 0;
			int morningHour = 0;
			int morningMinute = 0;
			double minutesToAdd = 0;

			try
			{
				string[] eveningTime = evening.Split(':');
				string[] morningTime = morning.Split(':');

				eveningHour = Convert.ToInt32(eveningTime[0], CultureInfo.CurrentCulture.NumberFormat);
				eveningMinute = Convert.ToInt32(eveningTime[1], CultureInfo.CurrentCulture.NumberFormat);
				morningHour = Convert.ToInt32(morningTime[0], CultureInfo.CurrentCulture.NumberFormat);
				morningMinute = Convert.ToInt32(morningTime[1], CultureInfo.CurrentCulture.NumberFormat);
				minutesToAdd = Convert.ToDouble(sMinutesToAdd, CultureInfo.CurrentCulture.NumberFormat);
			}
			catch (Exception)
			{
				OperationalEvent oe = new OperationalEvent(
					TDEventCategory.ThirdParty,
					TDTraceLevel.Error,
					"Bad format/Unset Journey parameters 'eveningTime' or 'morningTime' properties");

				Logger.Write(oe);

				throw new TDException(
					"Bad format/Unset Journey parameters 'eveningtime', 'morningtime' or 'minutestoadd properties",
					true,
					TDExceptionIdentifier.SMUnsetParameters);
			}

			// current time should be 15 minutes ahead (or whatever minutesToAdd property is set to)
			// so if current time +15 minutes is after evening time, time is set to next morning
			TDDateTime timeToCompare = TDDateTime.Now.AddMinutes(minutesToAdd);
			int nowHour = timeToCompare.Hour;
			int nowMinute = timeToCompare.Minute;

			TDDateTime leaveDateTime = TDDateTime.Now;
			// If now is between evening time and midnight set time to morning time the next day 
			if (((nowHour*100+nowMinute) >= (eveningHour*100+eveningMinute)) && 
				((nowHour*100+nowMinute) <= 2359))
			{
				leaveDateTime = leaveDateTime.AddDays(1);
				outwardHour = morningHour.ToString(CultureInfo.CurrentCulture.NumberFormat);
				outwardMinute = morningMinute.ToString(CultureInfo.CurrentCulture.NumberFormat);
			}
				// If now is between midnight and morning time, set time to morning time
			else if ((nowHour*100+nowMinute) <= (morningHour*100+morningMinute))
			{
				outwardHour = morningHour.ToString(CultureInfo.CurrentCulture.NumberFormat);
				outwardMinute = morningMinute.ToString(CultureInfo.CurrentCulture.NumberFormat);
			}
			else
			{
				// else take current day + 15 minutes (from properties)
				
				// if minute is not divisible by 5, adjust it to the next divisor of 5 
				//(because minutes are graduated by units of 5)
				if (leaveDateTime.Minute%5 != 0)
					leaveDateTime = leaveDateTime.AddMinutes(-leaveDateTime.Minute%5 + 5);

				// then add the minutes to add!
				leaveDateTime = leaveDateTime.AddMinutes(minutesToAdd);

				outwardHour = leaveDateTime.Hour.ToString(CultureInfo.CurrentCulture.NumberFormat);

				outwardMinute = leaveDateTime.Minute.ToString(CultureInfo.CurrentCulture.NumberFormat);

			}
			return leaveDateTime;
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
