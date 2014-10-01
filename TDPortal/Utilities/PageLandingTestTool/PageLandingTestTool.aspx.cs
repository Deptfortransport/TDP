#region History
// *********************************************** 
// NAME                 : PageLandingTestTool.aspx.cs
// AUTHOR               : Rob Greenwood
// DATE CREATED         : 20/09/2005
// DESCRIPTION  : Standalone Test Tool for PageLanding
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Stubs/PageLandingTestTool/PageLandingTestTool.aspx.cs-arc  $ 
//
//   Rev 1.11   Jan 22 2013 14:30:12   mmodi
//Added exclude Telecabine mode
//Resolution for 5884: CCN:677 - Telecabine modetype
//
//   Rev 1.10   Dec 10 2012 15:44:30   mmodi
//Added accessible options landing parameters
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.9   Oct 06 2009 14:41:30   apatel
//Stop Information code changes
//Resolution for 5315: CCN526 Stop Information Page Landing
//
//   Rev 1.8   Oct 01 2009 10:53:42   pghumra
//Applied changes for cycle planner landing page, latitude longitude coordinates in landing page and find nearest car park functionality
//Resolution for 5316: CCN537 Cycle Planning Page Landing
//Resolution for 5317: CCNxxx Lat Long Coordinates in Page Landing
//
//   Rev 1.7   Sep 10 2009 14:40:58   apatel
//Updated page landing test tool for stop Information landing page
//Resolution for 5315: CCN526 Stop Information Page Landing
//
//   Rev 1.6   Aug 24 2009 15:09:16   mmodi
//Updated to allow testing of IFrame journey planner
//Resolution for 5311: CCN532 Page Landing for Bing
//
//   Rev 1.5   Jul 28 2009 14:24:36   mmodi
//Updated url building to be Web2
//
//   Rev 1.4   Oct 20 2008 15:13:22   mmodi
//Corrected build problem
//
//   Rev 1.3   Aug 06 2008 14:23:50   rbroddle
//Updated so that it compiles in VS2005
//
//   Rev 1.2   Jan 25 2008 15:53:46   pscott
//Del 9.10  Add CO2 Landing Page
//
//   Rev 1.1   Nov 29 2007 11:36:50   build
//Updated for Del 9.8
//
//   Rev 1.17   Nov 08 2007 14:51:32   mmodi
//Added Find nearest page landing fields
//Resolution for 4524: Del 9.8 - Car Parking Page Landing
//
//   Rev 1.16   Jun 13 2007 17:58:30   mmodi
//Correct Travelnews url creation problem
//
//   Rev 1.15   May 23 2007 17:50:52   mmodi
//Updated to allow input of CRS codes
//Resolution for 4424: 9.6 - Page Landing with CRS Codes
//
//   Rev 1.14   May 08 2007 11:01:52   mmodi
//Added code to ensure correct URL and Post are generated when a Microsite url entered
//Resolution for 4404: Page landing test tool can not use microsite options
//
//   Rev 1.13   Feb 23 2006 09:50:06   halkatib
//Added Exclusions to the tool
//
//   Rev 1.12   Feb 03 2006 10:15:48   halkatib
//Tool previously populated the url with the word "PartnerId" if no partner id was input. 
//The tool has been changed so that the partner id will be set to string.empty if not input.
//
//   Rev 1.11   Dec 13 2005 16:00:54   halkatib
//Incorectly disabled radio buttons upon first load. This has been corrected.
//
//   Rev 1.10   Dec 13 2005 15:53:26   halkatib
//Fixed encoded parameters issue for encrypted parameters
//
//   Rev 1.9   Dec 13 2005 11:40:50   halkatib
//Fixed problem that did not allow a single naptan to be entered with the find a pages. Added event handlers to only allow the find a pages to be used when at least one naptan field is populated. 
//
//   Rev 1.8   Dec 09 2005 14:58:14   jmcallister
//Unencrypted params
//
//   Rev 1.7   Nov 29 2005 13:24:42   jmcallister
//Fixed bug that meant severity and display mode were not specified in url or Post correctly. Also fixed date format on aspx page as this was misleading
//
//   Rev 1.6   Nov 11 2005 09:49:38   kjosling
//Merged for del 8.0
//
//   Rev 1.5.1.0   Nov 02 2005 11:40:06   halkatib
//Changes required for Travel News
//
//   Rev 1.5   Oct 04 2005 14:17:50   jbroome
//Updated to allow null values
//Resolution for 2610: DEL 8 Stream: Landing page
//
//   Rev 1.4   Sep 30 2005 11:24:34   jbroome
//Fixed more bugs
//Resolution for 2610: DEL 8 Stream: Landing page
//
//   Rev 1.3   Sep 29 2005 19:00:14   jbroome
//Fixed errors in application
//Resolution for 2610: DEL 8 Stream: Landing page
//
//   Rev 1.2   Sep 27 2005 10:13:42   jbroome
//Updated URL path
//Resolution for 2610: DEL 8 Stream: Landing page
//
//   Rev 1.1   Sep 22 2005 12:53:10   rgreenwood
//Changes to encrypted string content and format
//Resolution for 2610: DEL 8 Stream: Landing page
//
//   Rev 1.0   Sep 22 2005 11:42:04   rgreenwood
//Initial revision.
//Resolution for 2610: DEL 8 Stream: Landing page

#endregion

using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ServiceDiscovery;


	/// <summary>
	/// Summary description for WebForm1.
	/// </summary>
	public partial class PageLandingTestTool : System.Web.UI.Page
	{
		#region Controls

		// Labels
        //protected System.Web.UI.WebControls.Label labelServerName;
        //protected System.Web.UI.WebControls.Label labelOriginText;
        //protected System.Web.UI.WebControls.Label labelOriginData;
        //protected System.Web.UI.WebControls.Label labelDestination;
        //protected System.Web.UI.WebControls.Label labelDestinationText;
        //protected System.Web.UI.WebControls.Label labelDestinationData;
        //protected System.Web.UI.WebControls.Label labelOutwardDate;
        //protected System.Web.UI.WebControls.Label labelOutwardTime;
        //protected System.Web.UI.WebControls.Label labelReturnRequired;
        //protected System.Web.UI.WebControls.Label labelReturnDate;
        //protected System.Web.UI.WebControls.Label labelReturnTime;
        //protected System.Web.UI.WebControls.Label labelIncludeCar;
        //protected System.Web.UI.WebControls.Label labelPartnerID;
        //protected System.Web.UI.WebControls.Label labelAutoPlan;
        //protected System.Web.UI.WebControls.Label labelOrigin;
        //protected System.Web.UI.WebControls.Label labelOriginPCodeAddr;
        //protected System.Web.UI.WebControls.Label labelOriginCoords;
        //protected System.Web.UI.WebControls.Label labelOriginLongLat;
        //protected System.Web.UI.WebControls.Label labelDestPCodeAddress;
        //protected System.Web.UI.WebControls.Label labelDestCoords;
        //protected System.Web.UI.WebControls.Label labelDestLongLat;
        //protected System.Web.UI.WebControls.Label labelOutwardDepart;
        //protected System.Web.UI.WebControls.Label labelOutwardArrive;
        //protected System.Web.UI.WebControls.Label labelReturnDepart;
        //protected System.Web.UI.WebControls.Label labelReturnArrive;
        //protected System.Web.UI.WebControls.Label labelMode;
        //protected System.Web.UI.WebControls.Label labelMulti;
        //protected System.Web.UI.WebControls.Label labelModeCar;
        //protected System.Web.UI.WebControls.Label labelModeTrain;
        //protected System.Web.UI.WebControls.Label labelModeCoach;
        //protected System.Web.UI.WebControls.Label labelModeFlight;
        //protected System.Web.UI.WebControls.Label labelOriginNaptan;
        //protected System.Web.UI.WebControls.Label labelDestNaPTAN;
        //protected System.Web.UI.HtmlControls.HtmlTextArea textURLResult;
        //protected System.Web.UI.WebControls.Label labelJPHeader;
        //protected System.Web.UI.WebControls.Label labelOutwardDepartArrive;
        //protected System.Web.UI.WebControls.Label labelOutwardDepArrNull;
        //protected System.Web.UI.WebControls.Label labelReturnDepartArrive;
        //protected System.Web.UI.WebControls.Label labelReturnDepArrNull;
        //protected System.Web.UI.WebControls.Label labelTNHeader;
        //protected System.Web.UI.WebControls.Label lblRegion;
        //protected System.Web.UI.WebControls.Label labelNewsType;		
        //protected System.Web.UI.WebControls.Label labelNewsTypeNotSpecified;		
        //protected System.Web.UI.WebControls.Label labelNewsTypeRoad;		
        //protected System.Web.UI.WebControls.Label labelNewsTypePT;		
        //protected System.Web.UI.WebControls.Label labelNewsTypeBoth;
        //protected System.Web.UI.WebControls.Label labelSeverity;	
        //protected System.Web.UI.WebControls.Label labelTM;

		//RadioButtons
		protected System.Web.UI.WebControls.RadioButtonList radioOriginType;
		protected System.Web.UI.WebControls.RadioButtonList radioDestination;
        
        //protected System.Web.UI.WebControls.RadioButton radioDestinationNaptan;
        //protected System.Web.UI.WebControls.RadioButton radioDestinationPCodeAddr;
        //protected System.Web.UI.WebControls.RadioButton radioDestinationCoord;
        //protected System.Web.UI.WebControls.RadioButton radioDestinationLongLat;
        //protected System.Web.UI.WebControls.RadioButton radioDestinationCRS;
        //protected System.Web.UI.WebControls.RadioButton radioOriginNaptan;
        //protected System.Web.UI.WebControls.RadioButton radioOriginPCodeAddr;
        //protected System.Web.UI.WebControls.RadioButton radioOriginCoord;
        //protected System.Web.UI.WebControls.RadioButton radioOriginLongLat;
        //protected System.Web.UI.WebControls.RadioButton radioOriginCRS;
        //protected System.Web.UI.WebControls.RadioButton radioOutwardDepart;
        //protected System.Web.UI.WebControls.RadioButton radioOutwardArrive;
        //protected System.Web.UI.WebControls.RadioButton radioReturnDepart;
        //protected System.Web.UI.WebControls.RadioButton radioReturnArrive;
        //protected System.Web.UI.WebControls.RadioButton radioModeMulti;
        //protected System.Web.UI.WebControls.RadioButton radioModeCar;
        //protected System.Web.UI.WebControls.RadioButton radioModeTrain;
        //protected System.Web.UI.WebControls.RadioButton radioModeCoach;
        //protected System.Web.UI.WebControls.RadioButton radioModeFlight;			
        //protected System.Web.UI.WebControls.RadioButton radioLocGazAddress;
        //protected System.Web.UI.WebControls.RadioButton radioLocGazTown;
        //protected System.Web.UI.WebControls.RadioButton radioLocGazStation;
        //protected System.Web.UI.WebControls.RadioButton radioLocGazFacility;
        //protected System.Web.UI.WebControls.RadioButton radioFNAutoPlanNull;
        //protected System.Web.UI.WebControls.RadioButton radioFNAutoPlanYes;
        //protected System.Web.UI.WebControls.RadioButton radioFNAutoPlanNo;
        //protected System.Web.UI.WebControls.RadioButton radioFNTypeCarPark;

        ////Textboxes
        //protected System.Web.UI.WebControls.TextBox textOriginText;
        //protected System.Web.UI.WebControls.TextBox textOriginData;
        //protected System.Web.UI.WebControls.TextBox textDestinationText;
        //protected System.Web.UI.WebControls.TextBox textDestinationData;
        //protected System.Web.UI.WebControls.TextBox textOutwardDate;
        //protected System.Web.UI.WebControls.TextBox textOutwardTime;
        //protected System.Web.UI.WebControls.TextBox textReturnDate;
        //protected System.Web.UI.WebControls.TextBox textReturnTime;
        //protected System.Web.UI.WebControls.TextBox textServerName;
        //protected System.Web.UI.WebControls.TextBox textPartnerId;
        //protected System.Web.UI.WebControls.TextBox txtRegion;
        //protected System.Web.UI.WebControls.TextBox txtFNPlace;
        //protected System.Web.UI.WebControls.TextBox txtFNNumberOfResults;

        //protected System.Web.UI.WebControls.RadioButton radioOriginNull;
        //protected System.Web.UI.WebControls.Label labelOriginNull;
        //protected System.Web.UI.WebControls.RadioButton radioDestinationNull;
        //protected System.Web.UI.WebControls.Label labelDestinationNull;
        //protected System.Web.UI.WebControls.CheckBox encryptOrigin;
        //protected System.Web.UI.WebControls.Label labelEncrypt;
        //protected System.Web.UI.WebControls.RadioButton radioModeNull;
        //protected System.Web.UI.WebControls.Label labelModeNull;
        //protected System.Web.UI.WebControls.RadioButton radioCarNull;
        //protected System.Web.UI.WebControls.RadioButton radioCarYes;
        //protected System.Web.UI.WebControls.RadioButton radioCarNo;
        //protected System.Web.UI.WebControls.RadioButton radioAutoNull;
        //protected System.Web.UI.WebControls.RadioButton radioAutoYes;
        //protected System.Web.UI.WebControls.RadioButton radioAutoNo;
        //protected System.Web.UI.WebControls.RadioButton radioReturnNull;
        //protected System.Web.UI.WebControls.RadioButton radioReturnYes;
        //protected System.Web.UI.WebControls.RadioButton radioReturnNo;
        //protected System.Web.UI.WebControls.RadioButton radioOutwardDepArrNull;
        //protected System.Web.UI.WebControls.RadioButton radioReturnDepArrNull;
        //protected System.Web.UI.WebControls.RadioButton radioTMNotSpecified;
        //protected System.Web.UI.WebControls.RadioButton radioTMTable;
        //protected System.Web.UI.WebControls.RadioButton radioTMMap;
        //protected System.Web.UI.WebControls.RadioButton radioSeverityNotSpecified;
        //protected System.Web.UI.WebControls.RadioButton radioSeverityMajor;
        //protected System.Web.UI.WebControls.RadioButton radioSeverityAll;
        //protected System.Web.UI.WebControls.RadioButton radioNewsTypeBoth;
        //protected System.Web.UI.WebControls.RadioButton radioNewsTypePT;
        //protected System.Web.UI.WebControls.RadioButton radioNewsTypeRoad;
        //protected System.Web.UI.WebControls.RadioButton radioNewsTypeNotSpecified;

		//Checkboxes
		protected System.Web.UI.WebControls.CheckBox checkReturnRequired;
		protected System.Web.UI.WebControls.CheckBox checkIncludeCar;
		protected System.Web.UI.WebControls.CheckBox checkAutoPlan;
        //protected System.Web.UI.WebControls.CheckBox chkExcludeRail;
        //protected System.Web.UI.WebControls.CheckBox chkExcludeBusCoach;
        //protected System.Web.UI.WebControls.CheckBox chkExcludeUnderground;
        //protected System.Web.UI.WebControls.CheckBox chkExcludeTram;
        //protected System.Web.UI.WebControls.CheckBox chkExcludeFerry;
        //protected System.Web.UI.WebControls.CheckBox chkExcludePlane;
        //protected System.Web.UI.WebControls.CheckBox chkEncryptJP;
        //protected System.Web.UI.WebControls.CheckBox chkEncryptTN;
        //protected System.Web.UI.WebControls.CheckBox chkEncryptFN;

		//Buttons
        //protected System.Web.UI.WebControls.Button buttonGenerateAll;
        //protected System.Web.UI.WebControls.Button buttonGeneratePost;
        //protected System.Web.UI.WebControls.Button btnGenerateTNGet;
        //protected System.Web.UI.WebControls.Button btnGenerateTNPost;
        //protected System.Web.UI.WebControls.Button btnGenerateFindNearest;
        //protected System.Web.UI.WebControls.Button btnGenerateFindNearestPost;

        //protected System.Web.UI.WebControls.Button btnGenerateCO2;
        //protected System.Web.UI.WebControls.Button btnPostCO2;


		//Validators
		protected System.Web.UI.WebControls.RangeValidator rangevalOutwardTime;
		protected System.Web.UI.WebControls.RangeValidator rangevalReturnDate;

		// Shall we encrypt the origin or the destination?
		private bool encryptingOrigin = true;

		#endregion

		#region Constants

		//URL Constants
		const string urlStart = "http://";
		const string journeyPlanPath = "web2/journeyplanning";
		const string travelnewsPath = "web2/livetravel";
        const string iframePath = "web2/iframes";
		const string transportdirect = "transportdirect";
		const string sitest = "SITEST";
		const string test = "TEST";
		const string www = "WWW";
		const string localhost = "LOCALHOST";

		//Parameter constants
		const string partnerId = "id";

		const string entryType = "et";
		
		// Will have either : or = appended according to which is encrypted
		const string originData = "o";
		const string destinationData = "d" ;

		const string originType = "oo";
		const string originText = "on";
		const string destinationType = "do";
		const string destinationText = "dn";
		const string outwardDate = "dt";
		const string outwardTime = "t";
		const string outwardDepArr = "da";
		const string mode = "m";
		const string carDefault = "c";
		const string autoPlan = "p";
        const string accessibleOption = "ao";
        const string accessibleFewerChanges = "fc";
		const string returnRequired = "r";
		const string returnDate = "rdt";
		const string returnTime = "rt";
		const string returnDepArr = "rda";
		const string encryptedParameters = "enc";
		//parameters for Travel News
		const string tablemap_TN = "tm";
		const string region_TN = "rg";
		const string newsType_TN = "nt";
		const string severity_TN = "sv";
		const string modeToExclude = "ex";
		//parameters for Find Nearest - Car park
		const string FN_type = "ft";
		const string FN_place = "pn";
		const string FN_locGaz = "lg";
        const string FN_loctype = "lo";
        const string FN_locdata = "l";
		const string FN_numOfResultsDisplayed = "nd";
        
        //parameters for CO2
        const string CO2_entryType = "et";
        const string CO2_landingType = "lt";
        const string CO2_Distance = "di";
        const string CO2_ExcludedModes = "lm";
        const string CO2_Units = "un";

        //parameters for IFrame Journey Planner
        const string IFJP_From = "txtFrom";
        const string IFJP_FromGaz = "from";
        const string IFJP_To = "txtTo";
        const string IFJP_ToGaz = "to";
        const string IFJP_Day = "day";
        const string IFJP_MonthYear = "monYr";
        const string IFJP_Hour = "hr";
        const string IFJP_Minute = "min";
        const string IFJP_PT = "public";
        const string IFJP_Car = "car";
        const string IFJP_ShowAdvanced = "advanced";
        const string IFJP_Autoplan = "p";
        const string IFJP_PartnerId = "pid";

        //parameters for Stop Information
        const string SI_StopDataType = "st";
        const string SI_StopData = "sd";
        const string SI_ExcludedFunctions = "ef";

		#region Future use	
		//		const string locationText = "ln=";
		//		const string mapScale = "sc=";
		//		const string actionType = "a=";
		#endregion


		#endregion

		#region Variables

		//Parameter variables		
		string serverName = string.Empty;
		string serverNameHost = string.Empty;
		string partnerIdVal = string.Empty;
		string encryptedPartnerId = string.Empty;
		string jpPageName = "JPLandingPage.aspx";
		string tnPageName = "tnlandingpage.aspx";
        string CO2PageName = "CO2landingpage.aspx";
		string fnPageName = "FindNearestLandingPage.aspx";
        string iframeJourneyPlannerPageName = "JourneyLandingPage.aspx";
        string SIPageName = "StopInformationLandingPage.aspx";

		string entryTypeVal = string.Empty;
		string originTypeVal = "en";
		string originDataVal = string.Empty;
		string encryptedOriginDataVal = string.Empty;
		string originTextVal = string.Empty; //Always string.Empty
		string encryptedoriginDataVal = string.Empty;
		string destinationTypeVal = "en";
		string destinationDataVal = string.Empty; //No default
		string destinationTextVal = string.Empty; //Always string.Empty
		string outwardDateVal = string.Empty; // ddmmyyyy Default to today
		string outwardTimeVal = string.Empty; //hhmm Default to now
		string outwardDepArrVal = "d";
		string modeVal = "m";
		bool carDefaultVal = true;
		string autoPlanVal = string.Empty;
		bool returnRequiredVal = false;
		string returnDateVal = string.Empty; // ddmmyyyy Default to today
		string returnTimeVal = string.Empty; // hhmm Default to now
		string returnDepArrVal = "d";
        string accessibilityOptionVal = string.Empty;
        string accessibilityFewerChangesVal = string.Empty;
		string encryptedParametersVal = string.Empty;
		
        //Travel news variables
		string regionTextVal = string.Empty;
		string encryptedRegionTextVal = string.Empty;
		string tableMapVal = "m";
		string severityVal = "m";
		string newsTypeVal = "b";
		bool encryptTravelNewsUrl;
		bool encryptJPUrl;
		bool encryptFNUrl;
        bool encryptCO2Url;
        bool encryptSIUrl;
		bool isPostFlag = false;			
		string excludedModes = string.Empty;

		// Find Nearest variables
		string findNearestType = string.Empty;
		string findNearestPlace = string.Empty;
		string findNearestLocGaz = string.Empty;
        string findNearestLocType = string.Empty;
        string findNearestLocData = string.Empty;
		string findNearestNumOfResultsDisplayed = string.Empty;
		string findNearestAutoPlan = string.Empty;


        //CO2 variables
        protected System.Web.UI.WebControls.RadioButton radioFNTypeCO2;
        //protected System.Web.UI.WebControls.Label lblCO2Distance;
        //protected System.Web.UI.WebControls.TextBox txtCO2Distance;
        //protected System.Web.UI.WebControls.Label lblCO2Units;
        //protected System.Web.UI.WebControls.RadioButton radioCO2AutoPlanYesNull;
        //protected System.Web.UI.WebControls.RadioButton radioCO2AutoPlanYes;
        //protected System.Web.UI.WebControls.RadioButton radioCO2AutoPlanNo;
        protected System.Web.UI.WebControls.RadioButton RadioModeSCar;
        protected System.Web.UI.WebControls.RadioButton RadioModeLCar;
        protected System.Web.UI.WebControls.RadioButton RadioModeBus;
        protected System.Web.UI.WebControls.RadioButton RadioModeRail;
        protected System.Web.UI.WebControls.RadioButton RadioModeAir;
        //protected System.Web.UI.WebControls.Label CO2ExcludeLabel;
        //protected System.Web.UI.WebControls.CheckBox CheckCO2Encrypt;
        //protected System.Web.UI.WebControls.RadioButton RadioCO2UnitMiles;
        //protected System.Web.UI.WebControls.RadioButton RadioCO2UnitKm;
        string CO2entryTypeVal = "co";
        string CO2landingTypeVal = "co";
        string CO2autoPlan = string.Empty;
        string CO2unitsVal = string.Empty;
        string CO2distanceVal = string.Empty;
        //protected System.Web.UI.WebControls.CheckBox CheckModeSCar;
        //protected System.Web.UI.WebControls.CheckBox CheckModeLCar;
        //protected System.Web.UI.WebControls.CheckBox CheckModeBus;
        //protected System.Web.UI.WebControls.CheckBox CheckModeRail;
        //protected System.Web.UI.WebControls.CheckBox CheckModeAir;
        string CO2excludedModesVal = string.Empty;

        //IFrame Journey Planner
        string IFJP_FromVal = string.Empty;
        string IFJP_FromGazVal = string.Empty;
        string IFJP_ToVal = string.Empty;
        string IFJP_ToGazVal = string.Empty;
        string IFJP_DayVal = string.Empty;
        string IFJP_MonthYearVal = string.Empty;
        string IFJP_HourVal = string.Empty;
        string IFJP_MinuteVal = string.Empty;
        string IFJP_PTVal = string.Empty;
        string IFJP_CarVal = string.Empty;
        string IFJP_ShowAdvancedVal = string.Empty;
        string IFJP_AutoplanVal = string.Empty;

        //Stop Information
        string SIStopDataVal = string.Empty;
        string SIStopTypeVal = "n";
        string SIExcludedFunctionsVal = string.Empty;

		#region Future use
		//		
		//		string regionVal = "r";
		//		
		//		
		//		string locationTextVal = string.Empty; //Always string.Empty
		//		string mapScaleVal = string.Empty;
		//		int actionTypeVal = 1;
		#endregion

		
		#endregion

		#region Page_Load
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			// Put user code to initialize the page here
			if (!Page.IsPostBack)
			{
				radioOriginNull.Checked = true;
				radioDestinationNull.Checked = true;
				radioModeNull.Checked = true;
				radioCarNull.Checked = true;
				radioAutoNull.Checked = true;
                radioAccessibleNull.Checked = true;
				radioReturnNull.Checked = true;
				radioOutwardDepArrNull.Checked = true;
				radioReturnDepArrNull.Checked = true;
				radioTMNotSpecified.Checked=true;
				radioNewsTypeNotSpecified.Checked = true;
				radioSeverityNotSpecified.Checked = true;
				radioFNAutoPlanNull.Checked = true;
				radioLocGazAddress.Checked = true;
				radioFNTypeCarPark.Checked = true;
                radioLocTypePlaceName.Checked = true;
                radioCO2AutoPlanYes.Checked = true;
                RadioCO2UnitKm.Checked = true;
                radioIFJPLocGazFromAddress.Checked = true;
                radioIFJPLocGazToAddress.Checked = true;
                radioIFJPPTYes.Checked = true;
                radioIFJPCarYes.Checked = true;
                radioIFJPAdvancedNotSpecified.Checked = true;
                radioIFJPAutoplanNotSpecified.Checked = true;
                
                // Initialise server
                textServerName.Text  = Request.ServerVariables["HTTP_HOST"];
                
                // Initialise the date fields for ease of entry
                DateTime now = DateTime.Now;

                txtIFJPOutwardDate.Text = now.ToString("ddMMyyyy");
                txtIFJPOutwardTime.Text = now.ToString("HHmm");
                
			}
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Captures the user's inputs common to all Landing pages such as Partner ID.
		/// </summary>
		/// <returns></returns>
		public void DetermineCommonControlValues()
		{
			if (textServerName.Text != string.Empty)
				serverName = textServerName.Text;
			else
				serverName = "ServerName";

			if (textPartnerId.Text != string.Empty)
				partnerIdVal = textPartnerId.Text;
			else
				partnerIdVal = string.Empty;

			// Code to set the serverNameHost dependent on the serverName entered
			string tempServerName = serverName.Trim();
			string[] tempServerNameArray = tempServerName.Split('.');

			if ((string.Equals(tempServerNameArray[0].ToUpper(), sitest))
				|| (string.Equals(tempServerNameArray[0].ToUpper(), test))
				|| (string.Equals(tempServerNameArray[0].ToUpper(), www))
				|| (string.Equals(tempServerNameArray[0].ToUpper(), localhost)))
			{
				serverNameHost = transportdirect;
			}
			else
			{
				serverNameHost = tempServerNameArray[0];
			}
		}

		/// <summary>
		/// Captures the user's inputs, encodes them id necessary and updates internal variables.
		/// TN Landing specific.
		/// </summary>
		/// <returns></returns>
		public void DetermineTNControlValues()
		{
			DetermineCommonControlValues();

			encryptTravelNewsUrl = chkEncryptTN.Checked;

			if (radioNewsTypeBoth.Checked)
				newsTypeVal = "b";
			else if (radioNewsTypeRoad.Checked)
				newsTypeVal = "r";
			else if (radioNewsTypePT.Checked)
				newsTypeVal = "p";

			if (radioTMMap.Checked)
				tableMapVal = "m";
			else if (radioTMTable.Checked)
				tableMapVal = "t";
			
			if (radioSeverityMajor.Checked)
				severityVal = "m";
			else if (radioSeverityAll.Checked)
				severityVal = "all";

			//Region Text
			regionTextVal = EncodeParameters(txtRegion.Text);
			
		}


		/// <summary>
		/// Captures the user's inputs, encodes them if necessary and updates internal variables.
		/// FN Landing specific.
		/// </summary>
		/// <returns></returns>
		public void DetermineFNControlValues()
		{
			DetermineCommonControlValues();

			encryptFNUrl = chkEncryptFN.Checked;

			entryTypeVal = "fn";

			if (radioFNTypeCarPark.Checked)
				findNearestType = "cp";

            if (radioLocTypePlaceName.Checked)
                findNearestLocType = "p";
            else if (radioLocTypeOSGR.Checked)
                findNearestLocType = "en";
            else if (radioLocTypeLatLong.Checked)
                findNearestLocType = "l";

			if (radioLocGazAddress.Checked)
				findNearestLocGaz = "AddressPostcode";
			else if (radioLocGazFacility.Checked)
				findNearestLocGaz = "AttractionFacility";
			else if (radioLocGazStation.Checked)
				findNearestLocGaz = "StationAirport";
			else if (radioLocGazTown.Checked)
				findNearestLocGaz = "CityTownSuburb";

			findNearestPlace = txtFNPlace.Text;
            findNearestLocData = txtFNLocData.Text;

			findNearestNumOfResultsDisplayed = txtFNNumberOfResults.Text;

			if (radioFNAutoPlanYes.Checked)
				findNearestAutoPlan = "1";
			else if (radioFNAutoPlanNo.Checked)
				findNearestAutoPlan = "0";
		}




        /// <summary>
        /// Captures the user's inputs, encodes them if necessary and updates internal variables.
        /// CO2 Landing specific.
        /// </summary>
        /// <returns></returns>
        public void DetermineCO2ControlValues()
        {
            DetermineCommonControlValues();

            encryptCO2Url = CheckCO2Encrypt.Checked;

            entryTypeVal = "co";


            CO2distanceVal = txtCO2Distance.Text;

            if (RadioCO2UnitKm.Checked)
                CO2unitsVal = "km";
            else if (RadioCO2UnitMiles.Checked)
                CO2unitsVal = "miles";


            StringBuilder workingString = new StringBuilder();
            if (CheckModeSCar.Checked)
                workingString.Append("s");
            if (CheckModeLCar.Checked)
                workingString.Append("l");
            if (CheckModeBus.Checked)
                workingString.Append("b");
            if (CheckModeRail.Checked)
                workingString.Append("r");
            if (CheckModeAir.Checked)
                workingString.Append("p");
            CO2excludedModesVal = workingString.ToString();





            if (radioCO2AutoPlanYes.Checked)
                CO2autoPlan = "yes";
            else if (radioCO2AutoPlanNo.Checked)
                CO2autoPlan = "no";





        }


		
		
		/// <summary>
		/// Captures the user's inputs, encodes them id necessary and updates internal variables.
		/// JP Landing specific.
		/// </summary>
		/// <returns></returns>
		public void DetermineControlValues()
		{
			DetermineCommonControlValues();
			
			// Are we encrypting the origin (or the destination)
			encryptingOrigin = encryptOrigin.Checked;

			//Are we encrypting anything at all
			//This overrides encryptOrigin.Checked setting
			encryptJPUrl = chkEncryptJP.Checked;

			//ORIGIN
			//Check radioOrigin group
			if (radioOriginNaptan.Checked)
				originTypeVal = "n";
			if (radioOriginPCodeAddr.Checked)
				originTypeVal = "p";
			if (radioOriginCoord.Checked)
				originTypeVal = "en";
			if (radioOriginLongLat.Checked)
				originTypeVal = "l";
			if (radioOriginCRS.Checked)
				originTypeVal = "c";

			originTextVal = EncodeParameters(textOriginText.Text);
			originDataVal = EncodeParameters(textOriginData.Text);

			//DESTINATION
			//Check radioDestination group
			if (radioDestinationNaptan.Checked)
				destinationTypeVal = "n";
			if (radioDestinationPCodeAddr.Checked)
				destinationTypeVal = "p";
			if (radioDestinationCoord.Checked)
				destinationTypeVal = "en";
			if (radioDestinationLongLat.Checked)
				destinationTypeVal = "l";
			if (radioDestinationCRS.Checked)
				destinationTypeVal = "c";

			destinationTextVal = EncodeParameters(textDestinationText.Text);
			destinationDataVal = EncodeParameters(textDestinationData.Text);

			//OUTWARD DATE & TIME
			outwardDateVal = EncodeParameters(textOutwardDate.Text);
			outwardTimeVal = EncodeParameters(textOutwardTime.Text);

			if (radioReturnYes.Checked)
				returnRequiredVal = true;
			else if (radioReturnNo.Checked)
				returnRequiredVal = false;

			if (radioOutwardDepart.Checked)
				outwardDepArrVal = "d";
			if (radioOutwardArrive.Checked)
				outwardDepArrVal = "a";

			//RETURN DATE & TIME
			returnDateVal = EncodeParameters(textReturnDate.Text);
			returnTimeVal = EncodeParameters(textReturnTime.Text);

			if (radioReturnDepart.Checked)
				returnDepArrVal = "d";
			if (radioReturnArrive.Checked)
				returnDepArrVal = "a";

			//MODE						
			if (radioModeMulti.Checked)
				modeVal = "m";
			if (radioModeCar.Checked)
				modeVal = "r";
			if (radioModeTrain.Checked)
				modeVal = "t";
			if (radioModeCoach.Checked)
				modeVal = "c";
			if (radioModeFlight.Checked)
				modeVal = "a";
            if (radioModeCycle.Checked)
                modeVal = "b";
			

			if (radioCarYes.Checked)
				carDefaultVal = true;
			else if (radioCarNo.Checked)
				carDefaultVal = false;


			if (radioAutoYes.Checked)
				autoPlanVal = "1";
			else if (radioAutoNo.Checked)
				autoPlanVal = "0";

            if (radioAccessibleWheelchairAssistance.Checked)
                accessibilityOptionVal = "wa";
            else if (radioAccessibleWheelchair.Checked)
                accessibilityOptionVal = "w";
            else if (radioAccessibleAssistance.Checked)
                accessibilityOptionVal = "a";

            if (chkAccessibleFewerChanges.Checked)
                accessibilityFewerChangesVal = "1";
		}


        /// <summary>
        /// Captures the user's inputs, encodes them if necessary and updates internal variables.
        /// IFrame Journey Planner Landing specific.
        /// </summary>
        /// <returns></returns>
        public void DetermineIFJPControlValues()
        {
            DetermineCommonControlValues();

            // Location text
            IFJP_FromVal = txtIFJPFrom.Text;
            IFJP_ToVal = txtIFJPTo.Text;

            // Location gaz
            if (radioIFJPLocGazFromAddress.Checked)
                IFJP_FromGazVal = "AddressPostcode";
            else if (radioIFJPLocGazFromFacility.Checked)
                IFJP_FromGazVal = "AttractionFacility";
            else if (radioIFJPLocGazFromStation.Checked)
                IFJP_FromGazVal = "StationAirport";
            else if (radioIFJPLocGazFromTown.Checked)
                IFJP_FromGazVal = "CityTownSuburb";
            else
                IFJP_FromGazVal = string.Empty;

            if (radioIFJPLocGazToAddress.Checked)
                IFJP_ToGazVal = "AddressPostcode";
            else if (radioIFJPLocGazToFacility.Checked)
                IFJP_ToGazVal = "AttractionFacility";
            else if (radioIFJPLocGazToStation.Checked)
                IFJP_ToGazVal = "StationAirport";
            else if (radioIFJPLocGazToTown.Checked)
                IFJP_ToGazVal = "CityTownSuburb";
            else
                IFJP_ToGazVal = string.Empty;

            // Transport modes
            if (radioIFJPPTYes.Checked)
                IFJP_PTVal = true.ToString();
            else if (radioIFJPPTNo.Checked)
                IFJP_PTVal = false.ToString();
            else
                IFJP_PTVal = string.Empty;

            if (radioIFJPCarYes.Checked)
                IFJP_CarVal = true.ToString();
            else if (radioIFJPCarNo.Checked)
                IFJP_CarVal = false.ToString();
            else
                IFJP_CarVal = string.Empty;

            // Advanced
            if (radioIFJPAdvancedYes.Checked)
                IFJP_ShowAdvancedVal = true.ToString();
            else if (radioIFJPAdvancedNo.Checked)
                IFJP_ShowAdvancedVal = false.ToString();
            else
                IFJP_ShowAdvancedVal = string.Empty;

            // Autoplan
            if (radioIFJPAutoplanYes.Checked)
                IFJP_AutoplanVal = true.ToString();
            else if (radioIFJPAutoplanNo.Checked)
                IFJP_AutoplanVal = false.ToString();
            else
                IFJP_AutoplanVal = string.Empty;

            // Date and time
            try
            {
                string date = txtIFJPOutwardDate.Text;
                string time = txtIFJPOutwardTime.Text;

                if (!string.IsNullOrEmpty(date))
                {
                    IFJP_DayVal = date.Substring(0, 2);
                    IFJP_MonthYearVal = date.Substring(2, 2) + "/" + date.Substring(4, 4);
                }

                if (!string.IsNullOrEmpty(time))
                {
                    IFJP_HourVal = time.Substring(0, 2);
                    IFJP_MinuteVal = time.Substring(2, 2);
                }
            }
            catch
            {
                IFJP_DayVal = string.Empty;
                IFJP_MonthYearVal = string.Empty;
                IFJP_HourVal = string.Empty;
                IFJP_MinuteVal = string.Empty;
            }

            // Encode all the values
            IFJP_FromVal = Server.UrlEncode(IFJP_FromVal);
            IFJP_FromGazVal = Server.UrlEncode(IFJP_FromGazVal);
            IFJP_ToVal = Server.UrlEncode(IFJP_ToVal);
            IFJP_ToGazVal = Server.UrlEncode(IFJP_ToGazVal);
            IFJP_DayVal = Server.UrlEncode(IFJP_DayVal);
            IFJP_MonthYearVal = Server.UrlEncode(IFJP_MonthYearVal);
            IFJP_HourVal = Server.UrlEncode(IFJP_HourVal);
            IFJP_MinuteVal = Server.UrlEncode(IFJP_MinuteVal);
            IFJP_PTVal = Server.UrlEncode(IFJP_PTVal);
            IFJP_CarVal = Server.UrlEncode(IFJP_CarVal);
            IFJP_ShowAdvancedVal = Server.UrlEncode(IFJP_ShowAdvancedVal);
        }

		
		/// <summary>
		/// HTML encodes the string
		/// </summary>
		/// <param name="s"></param>
		/// <returns>Encoded string</returns>
		public string EncodeParameters(string s)
		{
			//Encode the string to be HTTP friendly
			string encodedVal = HttpUtility.UrlEncode(s);

			return encodedVal;
		}


		/// <summary>
		/// Encrypts the string
		/// </summary>
		/// <returns>encrypted string</returns>
		public string EncryptParameters(string s)
		{
			//Encrypt the encoded string
			ITDCrypt encryptionEngine = (ITDCrypt)new CryptoFactory().Get();
			string encryptedVal = encryptionEngine.AsymmetricEncrypt(s);

			return encryptedVal;
		}


		
		/// <summary>
		/// Uses a stringbuilder class to output a url string that captures all the 
		/// parameters relating to the user's selections on the page.
		/// This section is JP Landing specific.
		/// </summary>
		/// <returns>string</returns>
		public string GenerateURL()
		{
			//Get the user's inputs
			DetermineControlValues();

			//Build the unencoded start of the URL e.g. http://www.transportdirect.info/JPLandingPage.aspx?
			StringBuilder urlString = new StringBuilder(string.Empty);

			urlString.Append(urlStart);
			urlString.Append(serverName + "/");
			//urlString.Append(serverNameHost + "/");
			urlString.Append(journeyPlanPath + "/");
			urlString.Append(jpPageName);
			urlString.Append("?");

			//Build the string of parameters
			StringBuilder parameterString = new StringBuilder(string.Empty);
			
			# region Build Parameter String

			// Include origin type if specified
			if (!radioOriginNull.Checked)
			{
				parameterString.Append(originType);
				parameterString.Append("=");
				parameterString.Append(originTypeVal);
				parameterString.Append("&");
			}
			parameterString.Append(originText);
			parameterString.Append("=");
			parameterString.Append(originTextVal);
			parameterString.Append("&");
			// Include destination type if specified
			if (!radioDestinationNull.Checked)
			{
				parameterString.Append(destinationType);
				parameterString.Append("=");
				parameterString.Append(destinationTypeVal);
				parameterString.Append("&");
			}
			parameterString.Append(destinationText);
			parameterString.Append("=");
			parameterString.Append(destinationTextVal);
			parameterString.Append("&");
			// Output correct values according to encryption
			if (encryptingOrigin)
			{
				parameterString.Append(destinationData + "=");
				parameterString.Append(destinationDataVal);
				parameterString.Append("&");
			}
			else
			{
				parameterString.Append(originData + "=");
				parameterString.Append(originDataVal);
				parameterString.Append("&");
			}
			parameterString.Append(outwardDate);
			parameterString.Append("=");
			parameterString.Append(outwardDateVal);
			parameterString.Append("&");
			parameterString.Append(outwardTime);
			parameterString.Append("=");
			parameterString.Append(outwardTimeVal);
			parameterString.Append("&");
			// Include out dep/arr if specified
			if (!radioOutwardDepArrNull.Checked)
			{
				parameterString.Append(outwardDepArr);
				parameterString.Append("=");
				parameterString.Append(outwardDepArrVal);
				parameterString.Append("&");
			}
			// Include mode type if specified
			if (!radioModeNull.Checked)
			{
				parameterString.Append(mode);
				parameterString.Append("=");
				parameterString.Append(modeVal);
				parameterString.Append("&");
			}
			// Include car if specified
			if (!radioCarNull.Checked)
			{
				parameterString.Append(carDefault);
				parameterString.Append("=");
				parameterString.Append(carDefaultVal);
				parameterString.Append("&");
			}
			// Include auto plan if specified
			if (!radioAutoNull.Checked)
			{
				parameterString.Append(autoPlan);
				parameterString.Append("=");
				parameterString.Append(autoPlanVal);
				parameterString.Append("&");
			}

            // Include accessible options if specified
            if (!radioAccessibleNull.Checked)
            {
                parameterString.Append(accessibleOption);
                parameterString.Append("=");
                parameterString.Append(accessibilityOptionVal);
                parameterString.Append("&");

                if (chkAccessibleFewerChanges.Checked)
                {
                    parameterString.Append(accessibleFewerChanges);
                    parameterString.Append("=");
                    parameterString.Append(accessibilityFewerChangesVal);
                    parameterString.Append("&");
                }
            }

			// Include return required if specified
			if (!radioReturnNull.Checked)
			{
				parameterString.Append(returnRequired);
				parameterString.Append("=");
				parameterString.Append(returnRequiredVal);
				parameterString.Append("&");
			}
			parameterString.Append(returnDate);
			parameterString.Append("=");
			parameterString.Append(returnDateVal);
			parameterString.Append("&");
			parameterString.Append(returnTime);
			parameterString.Append("=");
			parameterString.Append(returnTimeVal);
			parameterString.Append("&");
			// Include ret dep/arr if specified
			if (!radioReturnDepArrNull.Checked)
			{
				parameterString.Append(returnDepArr);
				parameterString.Append("=");
				parameterString.Append(returnDepArrVal);
				parameterString.Append("&");
			}

			parameterString.Append(modeToExclude);
			parameterString.Append("=");
			parameterString.Append(GetExcludedModes());
			parameterString.Append("&");
		


			#region Future use
			//			parameterString.Append(tableMap);
			//			parameterString.Append(tableMapVal);
			//			parameterString.Append("&");
			//			parameterString.Append(region);
			//			parameterString.Append(regionVal);
			//			parameterString.Append("&");
			//			parameterString.Append(severity);
			//			parameterString.Append(severityVal);
			//			parameterString.Append("&");
			//			parameterString.Append(locationType);
			//			parameterString.Append(locationTypeVal);
			//			parameterString.Append("&");
			//			parameterString.Append(locationData);
			//			parameterString.Append(locationDataVal);
			//			parameterString.Append("&");
			//			parameterString.Append(locationText);
			//			parameterString.Append(locationTextVal);
			//			parameterString.Append("&");
			//			parameterString.Append(mapScale);
			//			parameterString.Append(mapScaleVal);
			//			parameterString.Append("&");
			//			parameterString.Append(actionType);
			//			parameterString.Append(actionTypeVal);
			//			parameterString.Append("&");
			#endregion

			#endregion

			if (this.encryptJPUrl)
			{
				return urlString.ToString() + parameterString.ToString() + encryptedParameters + "=" + GetEncryptedURL();
			}
			else
			{
				return urlString.ToString() + parameterString.ToString() + GetEncryptedURL();
				
			}
			
		}

		private string GetEncryptedURL ()
		{
			// Build the URL to display
			string workingString = string.Empty;
			string argumentDelimiter = "=";
			// Encrypt correct values if appropriate
			if (encryptJPUrl)
			{

				argumentDelimiter = ":";
			}
			else
			{
				argumentDelimiter = "=";
			}


			if (encryptingOrigin)
			{
				workingString = (partnerId + argumentDelimiter + partnerIdVal + "&" + originData + argumentDelimiter + originDataVal);
			}
			else
			{
				workingString = (partnerId + argumentDelimiter + partnerIdVal + "&" + destinationData + argumentDelimiter + destinationDataVal);
			}

			if (encryptJPUrl)
			{
				// Encrypt necessary portion of query string
				string encryptedString = EncryptParameters(workingString);
				// Encode the encrypted string
				string encodedEncryptedString;
				if (isPostFlag)
				{
					//do not encode
					encodedEncryptedString = encryptedString;
				}
				else
				{
					//encode
					encodedEncryptedString = EncodeParameters(encryptedString);
				}

				// Return full URL with encrypted encoded portion
				return encodedEncryptedString;
			}
			else
			{
				// Return full URL with unencrypted params
				return workingString;
			}
		}

		//button event handling for Travel news.
		public string GenerateTNURL()
		{
			//Get the user's inputs
			DetermineTNControlValues();

			//Build the unencoded start of the URL e.g. http://www.transportdirect.info/JPLandingPage.aspx?
			StringBuilder urlString = new StringBuilder(string.Empty);

			urlString.Append(urlStart);
			urlString.Append(serverName + "/");
			//urlString.Append(serverNameHost + "/");
			urlString.Append(travelnewsPath + "/");
			urlString.Append(tnPageName);
			urlString.Append("?");

			//Build the string of parameters
			StringBuilder parameterString = new StringBuilder(string.Empty);
			
			# region Build Parameter String

			// Include newstype type if specified
			if (!radioNewsTypeNotSpecified.Checked)
			{
				parameterString.Append(newsType_TN);
				parameterString.Append("=");
				parameterString.Append(newsTypeVal);
				parameterString.Append("&");
			}
			// Include Table/Map type if specified
			if (!radioTMNotSpecified.Checked)
			{
				parameterString.Append(tablemap_TN);
				parameterString.Append("=");
				parameterString.Append(tableMapVal);
				parameterString.Append("&");
			}
			// Include Severity type if specified
			if (!radioSeverityNotSpecified.Checked)
			{
				parameterString.Append(severity_TN);
				parameterString.Append("=");
				parameterString.Append(severityVal);
				parameterString.Append("&");
			}
			
			if (encryptTravelNewsUrl)
			{
				parameterString.Append(encryptedParameters);
				parameterString.Append("=");
			}

			#endregion


//			// Build the URL to display
//			string stringToEncrypt = string.Empty;
//			// Encrypt correct values
//			stringToEncrypt = (partnerId + "=" + partnerIdVal + "&" + region_TN + ":" + regionTextVal);			

			//string argsString;

			if (encryptTravelNewsUrl)
			{
				//conditional check if the post is not set, then encode the parameters.
				//else do not encode
				string encodedEncryptedString;
				if (isPostFlag)
				{
					//do not encode parameters
					encodedEncryptedString = EncryptParameters((partnerId + ":" + partnerIdVal + "&" + region_TN + ":" + regionTextVal));
				}
				else
				{
					//not post, encode paramters
					encodedEncryptedString = EncodeParameters(EncryptParameters((partnerId + ":" + partnerIdVal + "&" + region_TN + ":" + regionTextVal)));

				}
				// Return full URL				
				return urlString.ToString() + parameterString.ToString() + encodedEncryptedString;
			}
			else
			{
				return urlString.ToString() + parameterString.ToString() + (partnerId + "=" + partnerIdVal + "&" + region_TN + "=" + regionTextVal);
			}

		}

		/// <summary>
		/// Uses a stringbuilder class to output a url string that captures all the 
		/// parameters relating to the user's selections on the page.
		/// This section is Find Nearest Landing specific.
		/// </summary>
		/// <returns>string</returns>
		public string GenerateFNURL()
		{
			//Get the user's inputs
			DetermineFNControlValues();

			//Build the unencoded start of the URL e.g. http://www.transportdirect.info/JPLandingPage.aspx?
			StringBuilder urlString = new StringBuilder(string.Empty);

			urlString.Append(urlStart);
			urlString.Append(serverName + "/");
			//urlString.Append(serverNameHost + "/");
			urlString.Append(journeyPlanPath + "/");
			urlString.Append(fnPageName);
			urlString.Append("?");

			//Build the string of parameters
			StringBuilder parameterString = new StringBuilder(string.Empty);

			# region Build Parameter String

			parameterString.Append(entryType);
			parameterString.Append("=");
			parameterString.Append(entryTypeVal);
			parameterString.Append("&");

			parameterString.Append(FN_type);
			parameterString.Append("=");
			parameterString.Append(findNearestType);
			parameterString.Append("&");

			if (findNearestNumOfResultsDisplayed != string.Empty)
			{
				parameterString.Append(FN_numOfResultsDisplayed);
				parameterString.Append("=");
				parameterString.Append(findNearestNumOfResultsDisplayed);
				parameterString.Append("&");
			}

			// Include auto plan if specified
			if ((radioFNAutoPlanYes.Checked) || (radioFNAutoPlanYes.Checked))
			{
				parameterString.Append(autoPlan);
				parameterString.Append("=");
				parameterString.Append(findNearestAutoPlan);
				parameterString.Append("&");
			}

			#endregion

			if (this.encryptFNUrl)
			{
				return urlString.ToString() + parameterString.ToString() + encryptedParameters + "=" + GetEncryptedFNURL();
			}
			else
			{
				return urlString.ToString() + parameterString.ToString() + GetEncryptedFNURL();
				
			}
		}

		private string GetEncryptedFNURL ()
		{
			// Build the URL to display
			//string workingString = string.Empty;
			string argumentDelimiter = "=";
			string amp = "&";
			
			// Encrypt correct values if appropriate
			if (encryptFNUrl)
			{
				argumentDelimiter = ":";
			}
			else
			{
				argumentDelimiter = "=";
			}

			StringBuilder workingString = new StringBuilder();

			workingString.Append(partnerId);
			workingString.Append(argumentDelimiter);
			workingString.Append(partnerIdVal);
			workingString.Append(amp);
			workingString.Append(FN_place);
			workingString.Append(argumentDelimiter);
			workingString.Append(findNearestPlace);
			workingString.Append(amp);
			workingString.Append(FN_locGaz);
			workingString.Append(argumentDelimiter);
			workingString.Append(findNearestLocGaz);
            workingString.Append(amp);
            workingString.Append(FN_loctype);
            workingString.Append(argumentDelimiter);
            workingString.Append(findNearestLocType);
            workingString.Append(amp);
            workingString.Append(FN_locdata);
            workingString.Append(argumentDelimiter);
            workingString.Append(findNearestLocData);
			if (encryptFNUrl)
			{
				// Encrypt necessary portion of query string
				string encryptedString = EncryptParameters(workingString.ToString());
				// Encode the encrypted string
				string encodedEncryptedString;
				if (isPostFlag)
				{
					//do not encode
					encodedEncryptedString = encryptedString;
				}
				else
				{
					//encode
					encodedEncryptedString = EncodeParameters(encryptedString);
				}

				// Return full URL with encrypted encoded portion
				return encodedEncryptedString;
			}
			else
			{
				// Return full URL with unencrypted params
				return workingString.ToString();
			}
		}


        /// <summary>
        /// Uses a stringbuilder class to output a url string that captures all the 
        /// parameters relating to the user's selections on the page.
        /// This section is Find Nearest Landing specific.
        /// </summary>
        /// <returns>string</returns>
        public string GenerateCO2URL()
        {
            //Get the user's inputs
            DetermineCO2ControlValues();

            //Build the unencoded start of the URL e.g. http://www.transportdirect.info/JPLandingPage.aspx?
            StringBuilder urlString = new StringBuilder(string.Empty);

            urlString.Append(urlStart);
            urlString.Append(serverName + "/");
            //urlString.Append(serverNameHost + "/");
            urlString.Append(journeyPlanPath + "/");
            urlString.Append(CO2PageName);
            urlString.Append("?");

            //Build the string of parameters
            StringBuilder parameterString = new StringBuilder(string.Empty);


        #endregion

            if (this.encryptCO2Url)
            {
                return urlString.ToString() + parameterString.ToString() + encryptedParameters + "=" + GetEncryptedCO2URL();
            }
            else
            {
                return urlString.ToString() + parameterString.ToString() + GetEncryptedCO2URL();

            }
        }

        private string GetEncryptedCO2URL()
        {
            // Build the URL to display
            //string workingString = string.Empty;
            string argumentDelimiter = "=";
            string amp = "&";

            // Encrypt correct values if appropriate
            if (encryptCO2Url)
            {
                argumentDelimiter = ":";
            }
            else
            {
                argumentDelimiter = "=";
            }

            StringBuilder workingString = new StringBuilder();


            # region Build Parameter String

            workingString.Append(CO2_entryType);
            workingString.Append(argumentDelimiter);
            workingString.Append(CO2entryTypeVal);
            workingString.Append(amp);

            workingString.Append(CO2_landingType);
            workingString.Append(argumentDelimiter);
            workingString.Append(CO2landingTypeVal);
            workingString.Append(amp);

            workingString.Append(partnerId);
            workingString.Append(argumentDelimiter);
            workingString.Append(partnerIdVal);
            workingString.Append(amp);

            workingString.Append(CO2_Distance);
            workingString.Append(argumentDelimiter);
            workingString.Append(CO2distanceVal);
            workingString.Append(amp);

            workingString.Append(CO2_Units);
            workingString.Append(argumentDelimiter);
            workingString.Append(CO2unitsVal);
            workingString.Append(amp);

            workingString.Append(CO2_ExcludedModes);
            workingString.Append(argumentDelimiter);
            workingString.Append(CO2excludedModesVal);
            workingString.Append(amp);

            workingString.Append(autoPlan);
            workingString.Append(argumentDelimiter);
            workingString.Append(CO2autoPlan);


            if (encryptCO2Url)
            {
                // Encrypt necessary portion of query string
                string encryptedString = EncryptParameters(workingString.ToString());
                // Encode the encrypted string
                string encodedEncryptedString;
                if (isPostFlag)
                {
                    //do not encode
                    encodedEncryptedString = encryptedString;
                }
                else
                {
                    //encode
                    encodedEncryptedString = EncodeParameters(encryptedString);
                }

                // Return full URL with encrypted encoded portion
                return encodedEncryptedString;
            }
            else
            {
                // Return full URL with unencrypted params
                return workingString.ToString();
            }
            #endregion       
        }

        /// <summary>
        /// Uses a stringbuilder class to output a url string that captures all the 
        /// parameters relating to the user's selections on the page.
        /// This section is IFrame Journey Planner Landing specific.
        /// </summary>
        /// <returns>string</returns>
        public string GenerateIFJPURL()
        {
            //Get the user's inputs
            DetermineIFJPControlValues();

            //Build the unencoded start of the URL e.g. http://www.transportdirect.info/JPLandingPage.aspx?
            StringBuilder urlString = new StringBuilder(string.Empty);

            urlString.Append(urlStart);
            urlString.Append(serverName + "/");
            urlString.Append(iframePath + "/");
            urlString.Append(iframeJourneyPlannerPageName);
            urlString.Append("?");

            // Determine if empty parameters should be added
            bool includeAll = !chkIFJPIncludeSpecifiedOptions.Checked;

            //Build the string of parameters
            StringBuilder parameterString = new StringBuilder(string.Empty);

            # region Build Parameter String

            string eq = "=";
            string amp = "&";

            if ((!string.IsNullOrEmpty(IFJP_FromVal)) || (includeAll))
            {
                parameterString.Append(IFJP_From);
                parameterString.Append(eq);
                parameterString.Append(IFJP_FromVal);
                parameterString.Append(amp);
            }

            if ((!string.IsNullOrEmpty(IFJP_FromGazVal)) || (includeAll))
            {
                parameterString.Append(IFJP_FromGaz);
                parameterString.Append(eq);
                parameterString.Append(IFJP_FromGazVal);
                parameterString.Append(amp);
            }

            if ((!string.IsNullOrEmpty(IFJP_ToVal)) || (includeAll))
            {
                parameterString.Append(IFJP_To);
                parameterString.Append(eq);
                parameterString.Append(IFJP_ToVal);
                parameterString.Append(amp);
            }

            if ((!string.IsNullOrEmpty(IFJP_ToGazVal)) || (includeAll))
            {
                parameterString.Append(IFJP_ToGaz);
                parameterString.Append(eq);
                parameterString.Append(IFJP_ToGazVal);
                parameterString.Append(amp);
            }

            if ((!string.IsNullOrEmpty(IFJP_DayVal)) || (includeAll))
            {
                parameterString.Append(IFJP_Day);
                parameterString.Append(eq);
                parameterString.Append(IFJP_DayVal);
                parameterString.Append(amp);
            }

            if ((!string.IsNullOrEmpty(IFJP_MonthYearVal)) || (includeAll))
            {
                parameterString.Append(IFJP_MonthYear);
                parameterString.Append(eq);
                parameterString.Append(IFJP_MonthYearVal);
                parameterString.Append(amp);
            }

            if ((!string.IsNullOrEmpty(IFJP_HourVal)) || (includeAll))
            {
                parameterString.Append(IFJP_Hour);
                parameterString.Append(eq);
                parameterString.Append(IFJP_HourVal);
                parameterString.Append(amp);
            }

            if ((!string.IsNullOrEmpty(IFJP_MinuteVal)) || (includeAll))
            {
                parameterString.Append(IFJP_Minute);
                parameterString.Append(eq);
                parameterString.Append(IFJP_MinuteVal);
                parameterString.Append(amp);
            }

            if ((!string.IsNullOrEmpty(IFJP_PTVal)) || (includeAll))
            {
                parameterString.Append(IFJP_PT);
                parameterString.Append(eq);
                parameterString.Append(IFJP_PTVal);
                parameterString.Append(amp);
            }

            if ((!string.IsNullOrEmpty(IFJP_CarVal)) || (includeAll))
            {
                parameterString.Append(IFJP_Car);
                parameterString.Append(eq);
                parameterString.Append(IFJP_CarVal);
                parameterString.Append(amp);
            }

            if ((!string.IsNullOrEmpty(IFJP_ShowAdvancedVal)) || (includeAll))
            {
                parameterString.Append(IFJP_ShowAdvanced);
                parameterString.Append(eq);
                parameterString.Append(IFJP_ShowAdvancedVal);
                parameterString.Append(amp);
            }

            if ((!string.IsNullOrEmpty(IFJP_AutoplanVal)) || (includeAll))
            {
                parameterString.Append(IFJP_Autoplan);
                parameterString.Append(eq);
                parameterString.Append(IFJP_AutoplanVal);
                parameterString.Append(amp);
            }

            if ((!string.IsNullOrEmpty(partnerIdVal)) || (includeAll))
            {
                parameterString.Append(IFJP_PartnerId);
                parameterString.Append(eq);
                parameterString.Append(partnerIdVal);
            }

            #endregion

            string parameters = parameterString.ToString();

            // remove the trailing amp
            if ((!string.IsNullOrEmpty(parameters)) && (parameters.EndsWith(amp)))
            {
                parameters = parameters.Substring(0, parameters.Length - 1);
            }

            return urlString.ToString() + parameters;

        }

        /// <summary>
        /// Generates page landing url for Stop Information page
        /// </summary>
        /// <returns></returns>
        private string GenerateStopInformationURL()
        {
            
            //Get the user's inputs
            DetermineSIControlValues();

            //Build the unencoded start of the URL e.g. http://www.transportdirect.info/StopInformationLandingPage.aspx?
            StringBuilder urlString = new StringBuilder(string.Empty);

            urlString.Append(urlStart);
            urlString.Append(serverName + "/");
            //urlString.Append(serverNameHost + "/");
            urlString.Append(journeyPlanPath + "/");
            urlString.Append(SIPageName);
            urlString.Append("?");

            //Build the string of parameters
            StringBuilder parameterString = new StringBuilder(string.Empty);

            # region Build Parameter String

            parameterString.Append(entryType);
            parameterString.Append("=");
            parameterString.Append(entryTypeVal);
            parameterString.Append("&");

            parameterString.Append(SI_ExcludedFunctions);
            parameterString.Append("=");
            parameterString.Append(SIExcludedFunctionsVal);
            parameterString.Append("&");

            #endregion

            if (this.encryptSIUrl)
            {
                return urlString.ToString() + parameterString.ToString() + encryptedParameters + "=" + GetEncryptedSIURL();
            }
            else
            {
                return urlString.ToString() + parameterString.ToString() + GetEncryptedSIURL();

            }
        }

        private string GetEncryptedSIURL()
        {
            // Build the URL to display
            //string workingString = string.Empty;
            string argumentDelimiter = "=";
            string amp = "&";

            // Encrypt correct values if appropriate
            if (encryptSIUrl)
            {
                argumentDelimiter = ":";
            }
            else
            {
                argumentDelimiter = "=";
            }

            StringBuilder workingString = new StringBuilder();


            # region Build Parameter String

            workingString.Append(partnerId);
            workingString.Append(argumentDelimiter);
            workingString.Append(partnerIdVal);
            workingString.Append(amp);

            workingString.Append(SI_StopDataType);
            workingString.Append(argumentDelimiter);
            workingString.Append(SIStopTypeVal);
            workingString.Append(amp);

            workingString.Append(SI_StopData);
            workingString.Append(argumentDelimiter);
            workingString.Append(SIStopDataVal);
            

            if (encryptCO2Url)
            {
                // Encrypt necessary portion of query string
                string encryptedString = EncryptParameters(workingString.ToString());
                // Encode the encrypted string
                string encodedEncryptedString;
                if (isPostFlag)
                {
                    //do not encode
                    encodedEncryptedString = encryptedString;
                }
                else
                {
                    //encode
                    encodedEncryptedString = EncodeParameters(encryptedString);
                }

                // Return full URL with encrypted encoded portion
                return encodedEncryptedString;
            }
            else
            {
                // Return full URL with unencrypted params
                return workingString.ToString();
            }
            #endregion       
        }

        private void DetermineSIControlValues()
        {
            DetermineCommonControlValues();

            encryptCO2Url = CheckStopInformationEncrypt.Checked;

            entryTypeVal = "si";


            SIStopDataVal = txtStopData.Text;

            if (RadioStopDataCRS.Checked)
                SIStopTypeVal = "c";
            else if (RadioStopDataIATA.Checked)
                SIStopTypeVal = "i";
            else if (RadioStopDataSMS.Checked)
                SIStopTypeVal = "s";
            else
                SIStopTypeVal = "n";


            StringBuilder workingString = new StringBuilder();
            if (CheckSIMap.Checked)
                workingString.Append("m");
            if (CheckSIJourneyPlanning.Checked)
            {
                if ((workingString != null && workingString.ToString().Length != 0))
                    workingString.Append(",j");
                else
                    workingString.Append("j");
            }
            if (CheckSITaxiInformation.Checked)
            {
                if ((workingString != null && workingString.ToString().Length != 0))
                    workingString.Append(",t");
                else
                    workingString.Append("t");
            }
            if (CheckSIOperators.Checked)
            {
                if ((workingString != null && workingString.ToString().Length != 0))
                    workingString.Append(",o");
                else
                    workingString.Append("o");
            }
            if (CheckSINextServices.Checked)
            {
                if ((workingString != null && workingString.ToString().Length != 0))
                    workingString.Append(",n");
                else
                    workingString.Append("n");
            }
            if (CheckSIRealtimeLinks.Checked)
            {
                if ((workingString != null && workingString.ToString().Length != 0))
                    workingString.Append(",r");
                else
                    workingString.Append("r");
            }
            if (CheckSILocationInformation.Checked)
            {
                if ((workingString != null && workingString.ToString().Length != 0))
                    workingString.Append(",l");
                else
                    workingString.Append("l");
            }
            if (CheckSIFacilityInformation.Checked)
            {
                if ((workingString != null && workingString.ToString().Length != 0))
                    workingString.Append(",f");
                else
                    workingString.Append("f");
            }
            SIExcludedFunctionsVal = workingString.ToString();


        }
                
		#region Events

		/// <summary>
		/// Click event for the "Generate Post Request" button.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonGenerateAll_Click(object sender, System.EventArgs e)
		{
			isPostFlag = false;
			// Display the hyperlink in the text area
			textURLResult.InnerText = GenerateURL();
		}


		/// <summary>
		/// Click event for the "Generate Post Request" button.
		/// Generates hidden input fields based upon the user's inputs and
		/// then displays a Submit button to enable the user to actually do the post.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonGeneratePost_Click(object sender, System.EventArgs e)
		{
			isPostFlag = true;
			//Get the user inputs from the form
			DetermineControlValues();

			encryptedParametersVal = this.GetEncryptedURL(); 
			//Encrypt specified parameters
//			if (encryptingOrigin)
//			{
//				encryptedParametersVal = EncryptParameters(partnerId + ":" +  partnerIdVal + "&" + originData + ":" + originDataVal);
//			}
//			else // Encrypting destination
//			{
//				encryptedParametersVal = EncryptParameters(partnerId + ":" + partnerIdVal + "&" + destinationData + ":" + destinationDataVal);
//			}

			
			// Write out start of form
			//Response.Write("<form method=post target=_blank action='" + urlStart + serverName + "/" + serverNameHost + "/" + journeyPlanPath + "/" + jpPageName + "' name='myform'>");
            Response.Write("<form method=post target=_blank action='" + urlStart + serverName + "/" + journeyPlanPath + "/" + jpPageName + "' name='myform'>");

			// Write out each of the parameter values as hidden fields
	
			if (!radioOriginNull.Checked)
				Response.Write("<input type=hidden name=" + "'" + originType + "'" + " value=" + "'" + originTypeVal + "'" +  ">");
			
			Response.Write("<input type=hidden name=" + "'" + originText + "'" + " value=" + "'" + originTextVal + "'" +  ">");
			
			if (!radioDestinationNull.Checked)
				Response.Write("<input type=hidden name=" + "'" + destinationType + "'" + " value=" + "'" + destinationTypeVal + "'" +  ">");
			
			Response.Write("<input type=hidden name=" + "'" + destinationText + "'" + " value=" + "'" + destinationTextVal + "'" +  ">");
			
			// Output correct field acording to encryption
			if (encryptingOrigin)
			{
				Response.Write("<input type=hidden name=" + "'" + destinationData + "'" + " value=" + "'" + destinationDataVal + "'" +  ">");
			}
			else
			{
				Response.Write("<input type=hidden name=" + "'" + originData + "'" + " value=" + "'" + originDataVal + "'" +  ">");
			}			
			
			Response.Write("<input type=hidden name=" + "'" + outwardDate + "'" + " value=" + "'" + outwardDateVal + "'" +  ">");
			Response.Write("<input type=hidden name=" + "'" + outwardTime + "'" + " value=" + "'" + outwardTimeVal + "'" +  ">");
			
			if (!radioOutwardDepArrNull.Checked)
				Response.Write("<input type=hidden name=" + "'" + outwardDepArr + "'" +	 " value=" + "'" + outwardDepArrVal + "'" +  ">");
			
			if (!radioModeNull.Checked)
				Response.Write("<input type=hidden name=" + "'" + mode + "'" + " value=" + "'" + modeVal + "'" +  ">");
			
			if (!radioCarNull.Checked)
				Response.Write("<input type=hidden name=" + "'" + carDefault + "'" + " value=" + "'" + carDefaultVal + "'" +  ">");
		
			if (!radioAutoNull.Checked)
				Response.Write("<input type=hidden name=" + "'" + autoPlan + "'" + " value=" + "'" + autoPlanVal + "'" +  ">");

            if (!radioAccessibleNull.Checked)
            {
                Response.Write("<input type=hidden name=" + "'" + accessibleOption + "'" + " value=" + "'" + accessibilityOptionVal + "'" + ">");

                if (chkAccessibleFewerChanges.Checked)
                    Response.Write("<input type=hidden name=" + "'" + accessibleFewerChanges + "'" + " value=" + "'" + accessibilityFewerChangesVal + "'" + ">");
            }
	
			if (!radioReturnNull.Checked)
				Response.Write("<input type=hidden name=" + "'" + returnRequired + "'" + " value=" + "'" + returnRequiredVal + "'" +  ">");
		
			Response.Write("<input type=hidden name=" + "'" + returnDate + "'" + " value=" + "'" + returnDateVal + "'" +  ">");
			Response.Write("<input type=hidden name=" + "'" + returnTime + "'" + " value=" + "'" + returnTimeVal + "'" +  ">");
			
			if (!radioReturnDepArrNull.Checked)
				Response.Write("<input type=hidden name=" + "'" + returnDepArr + "'" + " value=" + "'" + returnDepArrVal + "'" +  ">");

			Response.Write("<input type=hidden name=" + "'" + modeToExclude + "'" + " value=" + "'" + GetExcludedModes() + "'" +  ">");

			#region Future use
			//			Response.Write("<input type=hidden name=" + "'" + tableMap + "'" +	"value=" + "'" + tableMapVal + "'" +  ">");
			//			Response.Write("<input type=hidden name=" + "'" + region + "'" +	"value=" + "'" + regionVal + "'" +  ">");
			//			Response.Write("<input type=hidden name=" + "'" + newsType + "'" +	"value=" + "'" + newsTypeVal + "'" +  ">");
			//			Response.Write("<input type=hidden name=" + "'" + severity + "'" +	"value=" + "'" + severityVal + "'" +  ">");
			//			Response.Write("<input type=hidden name=" + "'" + locationType + "'" +	"value=" + "'" + locationTypeVal + "'" +  ">");
			//			Response.Write("<input type=hidden name=" + "'" + locationData + "'" +	"value=" + "'" + locationDataVal + "'" +  ">");
			//			Response.Write("<input type=hidden name=" + "'" + locationText + "'" +	"value=" + "'" + locationTextVal + "'" +  ">");
			//			Response.Write("<input type=hidden name=" + "'" + mapScale + "'" +	"value=" + "'" + mapScaleVal + "'" +  ">");
			//			Response.Write("<input type=hidden name=" + "'" + actionType + "'" +	"value=" + "'" + actionTypeVal + "'" +  ">");
			#endregion

			if (encryptJPUrl)
			{
				Response.Write("<input type=hidden name=" + "'" + encryptedParameters + "'" + " value=" + "'" + encryptedParametersVal + "'" +  ">");
			}
			else
			{

				Response.Write("<input type=hidden name=" + "'" + partnerId + "'" + " value=" + "'" + partnerIdVal + "'" +  ">");

				if (this.encryptingOrigin)
				{
					Response.Write("<input type=hidden name=" + "'" + originData + "'" + " value=" + "'" + originDataVal + "'" +  ">");
				}
				else
				{
					Response.Write("<input type=hidden name=" + "'" + destinationData + "'" + " value=" + "'" + destinationDataVal + "'" +  ">");
				}
			}
			// Write out the end of the form
			Response.Write(@"<input type=submit value='POST Request'/><" + "/" + "form>");
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
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.radioOriginNull.CheckedChanged += new System.EventHandler(this.Naptan_CheckedChanged);
			this.radioOriginNaptan.CheckedChanged += new System.EventHandler(this.Naptan_CheckedChanged);
			this.radioOriginPCodeAddr.CheckedChanged += new System.EventHandler(this.Naptan_CheckedChanged);
			this.radioOriginCoord.CheckedChanged += new System.EventHandler(this.Naptan_CheckedChanged);
			this.radioOriginLongLat.CheckedChanged += new System.EventHandler(this.Naptan_CheckedChanged);
			this.radioOriginCRS.CheckedChanged += new System.EventHandler(this.Naptan_CheckedChanged);
			this.radioDestinationNull.CheckedChanged += new System.EventHandler(this.Naptan_CheckedChanged);
			this.radioDestinationNaptan.CheckedChanged += new System.EventHandler(this.Naptan_CheckedChanged);
			this.radioDestinationPCodeAddr.CheckedChanged += new System.EventHandler(this.Naptan_CheckedChanged);
			this.radioDestinationCoord.CheckedChanged += new System.EventHandler(this.Naptan_CheckedChanged);
			this.radioDestinationLongLat.CheckedChanged += new System.EventHandler(this.Naptan_CheckedChanged);
			this.radioDestinationCRS.CheckedChanged += new System.EventHandler(this.Naptan_CheckedChanged);
			this.buttonGenerateAll.Click += new System.EventHandler(this.buttonGenerateAll_Click);
			this.buttonGeneratePost.Click += new System.EventHandler(this.buttonGeneratePost_Click);
			this.btnGenerateFindNearest.Click += new System.EventHandler(this.btnGenerateFindNearest_Click);
			this.btnGenerateFindNearestPost.Click += new System.EventHandler(this.btnGenerateFindNearestPost_Click);
            this.btnGenerateCO2.Click += new System.EventHandler(this.btnGenerateCO2_Click);
            this.btnPostCO2.Click += new System.EventHandler(this.btnPostCO2_Click);
            this.btnGenerateTNGet.Click += new System.EventHandler(this.btnGenerateTNGet_Click);
            this.btnGenerateTNPost.Click += new System.EventHandler(this.btnGenerateTNPost_Click);
            this.btnIFJPGenerateGet.Click += new EventHandler(btnIFJPGenerateGet_Click);
            this.btnIFJPGeneratePost.Click += new EventHandler(btnIFJPGeneratePost_Click);
            this.btnGenerateStopInformation.Click += new EventHandler(btnGenerateStopInformation_Click);
            this.btnPostStopInformation.Click += new EventHandler(btnPostStopInformation_Click);
            this.Load += new System.EventHandler(this.Page_Load);

		}

       
		#endregion

		#region TN Button Click
		//button event handling for Travel news.
		private void btnGenerateTNGet_Click(object sender, System.EventArgs e)
		{
			isPostFlag = false;
			textURLResult.InnerText = GenerateTNURL();
		}

		private void btnGenerateTNPost_Click(object sender, System.EventArgs e)
		{
			isPostFlag = true;
			//Get the user inputs from the form
			DetermineTNControlValues();

			//Encrypt specified parameters
			if(this.encryptTravelNewsUrl)
			{
				encryptedParametersVal = EncryptParameters(partnerId + ":" +  partnerIdVal + "&" + region_TN + ":" + regionTextVal);		
			}
			
			// Write out start of form
			//Response.Write("<form method=post target=_blank action='" + urlStart + serverName + "/" + serverNameHost + "/" + travelnewsPath + "/" + tnPageName + "' name='myform'>");
            Response.Write("<form method=post target=_blank action='" + urlStart + serverName + "/" + travelnewsPath + "/" + tnPageName + "' name='myform'>");

			// Write out each of the parameter values as hidden fields	
			if (!radioNewsTypeNotSpecified.Checked)
				Response.Write("<input type=hidden name=" + "'" + newsType_TN + "'" + " value=" + "'" + newsTypeVal + "'" +  ">");
						
			if (!radioTMNotSpecified.Checked)
				Response.Write("<input type=hidden name=" + "'" + tablemap_TN + "'" + " value=" + "'" + tableMapVal + "'" +  ">");
			
			if (!radioSeverityNotSpecified.Checked)
				Response.Write("<input type=hidden name=" + "'" + severity_TN + "'" +	 " value=" + "'" + severityVal + "'" +  ">");
			
			if(this.encryptTravelNewsUrl)
			{
				Response.Write("<input type=hidden name=" + "'" + encryptedParameters + "'" + " value=" + "'" + encryptedParametersVal + "'" +  ">");
			}
			else
			{
				Response.Write("<input type=hidden name=" + "'" + region_TN + "'" + " value=" + "'" + regionTextVal + "'" +  ">");
				Response.Write("<input type=hidden name=" + "'" + partnerId + "'" + " value=" + "'" + partnerIdVal + "'" +  ">");
			}
			// Write out the end of the form
			Response.Write(@"<input type=submit value='POST Request'/><" + "/" + "form>");
		
		}
		#endregion

		#region Find Nearest Button Click
        /// <summary>
		/// Click event for the "Generate Post Request" button.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnGenerateFindNearest_Click(object sender, System.EventArgs e)
		{
			isPostFlag = false;
			// Display the hyperlink in the text area
			textURLResult.InnerText = GenerateFNURL();
		}

		private void btnGenerateFindNearestPost_Click(object sender, System.EventArgs e)
		{
			isPostFlag = true;
			//Get the user inputs from the form
			DetermineFNControlValues();

			//Encrypt specified parameters
			if(this.encryptFNUrl)
			{
				encryptedParametersVal =  this.GetEncryptedFNURL();
			}
			
			// Write out start of form
			//Response.Write("<form method=post target=_blank action='" + urlStart + serverName + "/" + serverNameHost + "/" + journeyPlanPath + "/" +  fnPageName + "' name='myform'>");
            Response.Write("<form method=post target=_blank action='" + urlStart + serverName + "/" + journeyPlanPath + "/" + fnPageName + "' name='myform'>");

			// Write out each of the parameter values as hidden fields	

			Response.Write("<input type=hidden name=" + "'" + entryType + "'" + " value=" + "'" + entryTypeVal + "'" +  ">");
			Response.Write("<input type=hidden name=" + "'" + FN_type + "'" + " value=" + "'" + findNearestType + "'" +  ">");
			
			if (txtFNNumberOfResults.Text != string.Empty)
				Response.Write("<input type=hidden name=" + "'" + FN_numOfResultsDisplayed + "'" + " value=" + "'" + findNearestNumOfResultsDisplayed + "'" +  ">");

			if (!radioFNAutoPlanNull.Checked)
				Response.Write("<input type=hidden name=" + "'" + autoPlan + "'" + " value=" + "'" + findNearestAutoPlan + "'" +  ">");


			if(this.encryptFNUrl)
			{
				Response.Write("<input type=hidden name=" + "'" + encryptedParameters + "'" + " value=" + "'" + encryptedParametersVal + "'" +  ">");
			}
			else
			{
				Response.Write("<input type=hidden name=" + "'" + FN_place + "'" + " value=" + "'" + findNearestPlace + "'" +  ">");
				Response.Write("<input type=hidden name=" + "'" + FN_locGaz + "'" + " value=" + "'" + findNearestLocGaz + "'" +  ">");
                Response.Write("<input type=hidden name=" + "'" + FN_loctype + "'" + " value=" + "'" + findNearestLocType + "'" + ">");
                Response.Write("<input type=hidden name=" + "'" + FN_locdata + "'" + " value=" + "'" + findNearestLocData + "'" + ">");
			}

			// Write out the end of the form
			Response.Write(@"<input type=submit value='POST Request'/><" + "/" + "form>");

			// Automatically generate URL as well
			btnGenerateFindNearest_Click(sender, e);
		}
		#endregion

        #region CO2 Button Click

        /// <summary>
        /// Click event for the "Generate Post Request" button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGenerateCO2_Click(object sender, System.EventArgs e)
        {
            isPostFlag = false;
            // Display the hyperlink in the text area
            textURLResult.InnerText = GenerateCO2URL();
        }


        private void btnPostCO2_Click(object sender, System.EventArgs e)
        {
            isPostFlag = true;
            //Get the user inputs from the form
            DetermineCO2ControlValues();

            //Encrypt specified parameters
            if (this.encryptCO2Url)
            {
                encryptedParametersVal = this.GetEncryptedCO2URL();
            }

            // Write out start of form
            Response.Write("<form method=post target=_blank action='"
                + urlStart + serverName + "/"
                //+ serverNameHost + "/" + journeyPlanPath
                + journeyPlanPath
                + "/" + CO2PageName + "' name='myform'>");

            // Write out each of the parameter values as hidden fields	
            string jps = encryptedParametersVal;
            if (this.encryptCO2Url)
            {
                Response.Write("<input type=hidden name=" + "'" + encryptedParameters + "'" + " value=" + "'" + encryptedParametersVal + "'" + ">");

            }
            else
            {
                Response.Write("<input type=hidden name=" + "'" + CO2_entryType + "'" + " value=" + "'" + CO2entryTypeVal + "'" + ">");
                Response.Write("<input type=hidden name=" + "'" + CO2_landingType + "'" + " value=" + "'" + CO2landingTypeVal + "'" + ">");
                Response.Write("<input type=hidden name=" + "'" + CO2_Distance + "'" + " value=" + "'" + CO2distanceVal + "'" + ">");
                Response.Write("<input type=hidden name=" + "'" + CO2_Units + "'" + " value=" + "'" + CO2unitsVal + "'" + ">");
                Response.Write("<input type=hidden name=" + "'" + CO2_ExcludedModes + "'" + " value=" + "'" + CO2excludedModesVal + "'" + ">");
                Response.Write("<input type=hidden name=" + "'" + CO2_ExcludedModes + "'" + " value=" + "'" + CO2excludedModesVal + "'" + ">");
                Response.Write("<input type=hidden name=" + "'" + partnerId + "'" + " value=" + "'" + partnerIdVal + "'" + ">");
                Response.Write("<input type=hidden name=" + "'" + autoPlan + "'" + " value=" + "'" + CO2autoPlan + "'" + ">");

            }


            // Write out the end of the form
            Response.Write(@"<input type=submit value='POST Request'/><" + "/" + "form>");

            // Automatically generate URL as well
            btnGenerateCO2_Click(sender, e);
        }
        #endregion

        #region IFrame Journey Planner Button Click
        /// <summary>
        /// Click event for the "Generate Get Request" button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnIFJPGenerateGet_Click(object sender, System.EventArgs e)
        {
            isPostFlag = false;

            // Display the hyperlink in the text area
            textURLResult.InnerText = GenerateIFJPURL();
        }

        /// <summary>
        /// Click event for the "Generate Post Request" button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnIFJPGeneratePost_Click(object sender, System.EventArgs e)
        {
            isPostFlag = true;

            //Get the user inputs from the form
            DetermineIFJPControlValues();

            // Determine if empty parameters should be added
            bool includeAll = !chkIFJPIncludeSpecifiedOptions.Checked;
           
            // Write out start of form
            Response.Write("<form method=post target=_blank action='" + urlStart + serverName + "/" + iframePath + "/" + iframeJourneyPlannerPageName + "' name='myform'>");

            // Write out each of the parameter values as hidden fields	
            if ((!string.IsNullOrEmpty(IFJP_FromVal)) || (includeAll))
            {
                Response.Write("<input type=hidden name=" + "'" + IFJP_From + "'" + " value=" + "'" + IFJP_FromVal + "'" + ">");
            }

            if ((!string.IsNullOrEmpty(IFJP_FromGazVal)) || (includeAll))
            {
                Response.Write("<input type=hidden name=" + "'" + IFJP_FromGaz + "'" + " value=" + "'" + IFJP_FromGazVal + "'" + ">");
            }

            if ((!string.IsNullOrEmpty(IFJP_ToVal)) || (includeAll))
            {
                Response.Write("<input type=hidden name=" + "'" + IFJP_To + "'" + " value=" + "'" + IFJP_ToVal + "'" + ">");
            }

            if ((!string.IsNullOrEmpty(IFJP_ToGazVal)) || (includeAll))
            {
                Response.Write("<input type=hidden name=" + "'" + IFJP_ToGaz + "'" + " value=" + "'" + IFJP_ToGazVal + "'" + ">");
            }

            if ((!string.IsNullOrEmpty(IFJP_DayVal)) || (includeAll))
            {
                Response.Write("<input type=hidden name=" + "'" + IFJP_Day + "'" + " value=" + "'" + IFJP_DayVal + "'" + ">");
            }

            if ((!string.IsNullOrEmpty(IFJP_MonthYearVal)) || (includeAll))
            {
                Response.Write("<input type=hidden name=" + "'" + IFJP_MonthYear + "'" + " value=" + "'" + IFJP_MonthYearVal + "'" + ">");
            }

            if ((!string.IsNullOrEmpty(IFJP_HourVal)) || (includeAll))
            {
                Response.Write("<input type=hidden name=" + "'" + IFJP_Hour + "'" + " value=" + "'" + IFJP_HourVal + "'" + ">");
            }

            if ((!string.IsNullOrEmpty(IFJP_MinuteVal)) || (includeAll))
            {
                Response.Write("<input type=hidden name=" + "'" + IFJP_Minute + "'" + " value=" + "'" + IFJP_MinuteVal + "'" + ">");
            }

            if ((!string.IsNullOrEmpty(IFJP_PTVal)) || (includeAll))
            {
                Response.Write("<input type=hidden name=" + "'" + IFJP_PT + "'" + " value=" + "'" + IFJP_PTVal + "'" + ">");
            }

            if ((!string.IsNullOrEmpty(IFJP_CarVal)) || (includeAll))
            {
                Response.Write("<input type=hidden name=" + "'" + IFJP_Car + "'" + " value=" + "'" + IFJP_CarVal + "'" + ">");
            }

            if ((!string.IsNullOrEmpty(IFJP_ShowAdvancedVal)) || (includeAll))
            {
                Response.Write("<input type=hidden name=" + "'" + IFJP_ShowAdvanced + "'" + " value=" + "'" + IFJP_ShowAdvancedVal + "'" + ">");
            }

            if ((!string.IsNullOrEmpty(IFJP_AutoplanVal)) || (includeAll))
            {
                Response.Write("<input type=hidden name=" + "'" + IFJP_Autoplan + "'" + " value=" + "'" + IFJP_AutoplanVal + "'" + ">");
            }

            if ((!string.IsNullOrEmpty(partnerIdVal)) || (includeAll))
            {
                Response.Write("<input type=hidden name=" + "'" + IFJP_PartnerId + "'" + " value=" + "'" + partnerIdVal + "'" + ">");
            }
                        
            // Write out the end of the form
            Response.Write(@"<input type=submit value='POST Request'/><" + "/" + "form>");

            // Automatically generate URL as well
            btnIFJPGenerateGet_Click(sender, e);
        }
        #endregion

        #region Stop Information Button Click
        /// <summary>
        /// Click event for the "Generate Post Request" button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPostStopInformation_Click(object sender, EventArgs e)
        {
            isPostFlag = true;
            //Get the user inputs from the form
            DetermineSIControlValues();

            //Encrypt specified parameters
            if (this.encryptSIUrl)
            {
                encryptedParametersVal = this.GetEncryptedSIURL();
            }

            // Write out start of form
            Response.Write("<form method=post target=_blank action='"
                + urlStart + serverName + "/"
                //+ serverNameHost + "/" + journeyPlanPath
                + journeyPlanPath
                + "/" + SIPageName + "' name='myform'>");

            // Write out each of the parameter values as hidden fields	

            Response.Write("<input type=hidden name=" + "'" + entryType + "'" + " value=" + "'" + entryTypeVal + "'" + ">");
            Response.Write("<input type=hidden name=" + "'" + SI_ExcludedFunctions + "'" + " value=" + "'" + SIExcludedFunctionsVal + "'" + ">");
                
            if (this.encryptSIUrl)
            {
                Response.Write("<input type=hidden name=" + "'" + encryptedParameters + "'" + " value=" + "'" + encryptedParametersVal + "'" + ">");

            }
            else
            {
                Response.Write("<input type=hidden name=" + "'" + SI_StopDataType + "'" + " value=" + "'" + SIStopTypeVal + "'" + ">");
                Response.Write("<input type=hidden name=" + "'" + SI_StopData + "'" + " value=" + "'" + SIStopDataVal + "'" + ">");
                Response.Write("<input type=hidden name=" + "'" + partnerId + "'" + " value=" + "'" + partnerIdVal + "'" + ">");
                
            }


            // Write out the end of the form
            Response.Write(@"<input type=submit value='POST Request'/><" + "/" + "form>");

            // Automatically generate URL as well
            btnGenerateStopInformation_Click(sender, e);
        }

        /// <summary>
        /// Click event for the "Post Request" button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGenerateStopInformation_Click(object sender, EventArgs e)
        {
            isPostFlag = false;
            // Display the hyperlink in the text area
            textURLResult.InnerText = GenerateStopInformationURL();
        }
        #endregion

        #region Private methods
        private void Naptan_CheckedChanged(object sender, System.EventArgs e)
		{
			if (
				(radioOriginNull.Checked == true || radioOriginNaptan.Checked == true || radioOriginCRS.Checked) &&
				(radioDestinationNull.Checked == true || radioDestinationNaptan.Checked == true || radioDestinationCRS.Checked)
				)
			{	
				radioModeCoach.Enabled = true;
				radioModeFlight.Enabled = true;
				radioModeTrain.Enabled = true;
			}

			else
			{
				radioModeCoach.Enabled = false;
				radioModeFlight.Enabled = false;
				radioModeTrain.Enabled = false;
			}
		}

		private string GetExcludedModes()
		{
			StringBuilder exModes = new StringBuilder();

			if (chkExcludeRail.Checked)
				exModes.Append("r");
			if (chkExcludeBusCoach.Checked)
			{
				if ((exModes != null && exModes.ToString().Length != 0))
					exModes.Append(",b");
				else
					exModes.Append("b");
			}
			if (chkExcludeUnderground.Checked)
			{
				if ((exModes != null && exModes.ToString().Length != 0))
					exModes.Append(",u");
				else
					exModes.Append("u");
			}
			if (chkExcludeTram.Checked)
			{
				if ((exModes != null && exModes.ToString().Length != 0))
					exModes.Append(",t");
				else
					exModes.Append("t");
			}
			if (chkExcludeFerry.Checked)
			{
				if ((exModes != null && exModes.ToString().Length != 0))
					exModes.Append(",f");
				else
					exModes.Append("f");
			}
			if (chkExcludePlane.Checked)
			{
				if ((exModes != null && exModes.ToString().Length != 0))
					exModes.Append(",p");
				else
					exModes.Append("p");
			}
            if (chkExcludeTelecabine.Checked)
            {
                if ((exModes != null && exModes.ToString().Length != 0))
                    exModes.Append(",c");
                else
                    exModes.Append("c");
            }
			
			return exModes.ToString();
		}
		#endregion
}

