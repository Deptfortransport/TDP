// *********************************************** 
// NAME                 : PrintableFindMapResult.aspx
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 02/11/2009
// DESCRIPTION          : Printable Result Page for Find a map
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Maps/PrintableFindMapResult.aspx.cs-arc  $ 
//
//   Rev 1.3   Jan 18 2010 12:17:42   mmodi
//Add an auto refresh page if map image url not detected in session
//Resolution for 5375: Maps - Printer friendly map page refresh change
//
//   Rev 1.2   Nov 11 2009 18:42:34   mmodi
//Updated
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.1   Nov 05 2009 14:56:28   apatel
//mapping enhancement code changes
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.0   Nov 02 2009 17:53:22   mmodi
//Initial revision.
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//

using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using TransportDirect.Common;
using TransportDirect.Common.ResourceManager;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.Web.Adapters;
using TransportDirect.UserPortal.Web.Controls;

namespace TransportDirect.UserPortal.Web.Templates
{
    /// <summary>
    /// Printable Result Page for Find a map
    /// </summary>
    public partial class PrintableFindMapResult : TDPrintablePage, INewWindowPage
    {
        #region Private members

        private ITDSessionManager sessionManager;

        #endregion

        #region Constructor

        /// <summary>
		/// Default Constructor
		/// </summary>
        public PrintableFindMapResult()
		{
            pageId = PageId.PrintableFindMapResult;
        }

        #endregion

        #region Page_Load, Page_PreRender

        /// <summary>
        /// Page Load event handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            // check if the User Survey form should be displayed
            ShowSurvey();

            // load session items
            sessionManager = TDSessionManager.Current;

            LoadResources();

            PopulateControls();

            SetupRefreshPage();

            PopulateFooterControls();
        }

        /// <summary>
        /// Page PreRender event handler
        /// </summary>
        /// <param name="sender">Notifying object</param>
        /// <param name="e">Event data</param>
        protected void Page_PreRender(object sender, EventArgs e)
        {

        }

        #endregion

        #region Private methods

        /// <summary>
        /// Displays a user survey
        /// </summary>
        private void ShowSurvey()
        {
            //check if user survey should be displayed			
            bool showSurvey = UserSurveyHelper.ShowUserSurvey();

            //if user survey should be displayed...
            if (showSurvey)
            {
                //check if JavaScript is supported by the browser
                if (bool.Parse((string)Session[((TDPage)Page).Javascript_Support]))
                {
                    //add javascript block to this page that will open a user survey window when this page closes
                    string javaScriptDom = (string)Session[((TDPage)Page).Javascript_Dom];
                    ScriptRepository.ScriptRepository scriptRepository = (ScriptRepository.ScriptRepository)TDServiceDiscovery.Current[ServiceDiscoveryKey.ScriptRepository];
                    Page.ClientScript.RegisterClientScriptBlock(typeof(PrintableJourneyMapTile), "UserSurvey", scriptRepository.GetScript("UserSurvey", javaScriptDom));
                }
            }
        }

        /// <summary>
        /// Loads text and images on the page
        /// </summary>
        private void LoadResources()
        {
            labelPrinterFriendly.Text = Global.tdResourceManager.GetString(
                "StaticPrinterFriendly.labelPrinterFriendly", TDCultureInfo.CurrentUICulture);
            labelPrinterFriendly.Visible = true;

            labelInstructions.Text = GetResource("StaticPrinterFriendly.labelInstructions");
            labelInstructions.Visible = true;

            labelMap.Text = GetResource("FindMapResult.labelMap.Text");   
        }

        /// <summary>
        /// Populates and initialises the map controls
        /// </summary>
        private void PopulateControls()
        {
            // Set the page heading text
            TDLocation mapLocation = sessionManager.InputPageState.MapLocation;

            if ((mapLocation != null) && (mapLocation.Status == TDLocationStatus.Valid))
            {
                labelSelectedLocation.Text = mapLocation.Description;
            }

            // Populate the map control
            map.Populate(true, true, false);
        }

        /// <summary>
        /// Populates the labels shown at the bottom of the printable page
        /// </summary>
        private void PopulateFooterControls()
        {
            labelDateTimeTitle.Text = GetResource("StaticPrinterFriendly.labelDateTimeTitle");
            labelDateTime.Text = TDDateTime.Now.ToString("G");

            if (sessionManager.Authenticated)
            {
                labelUsername.Text = sessionManager.CurrentUser.Username;
                labelUsername.Visible = true;

                labelUsernameTitle.Text = GetResource("StaticPrinterFriendly.labelUsernameTitle");
                labelUsernameTitle.Visible = true;
            }

            // Reference labels do not need to be shown, left in for future use if needed
            labelReference.Visible = false;
            labelReferenceTitle.Visible = false;
        }

        /// <summary>
        /// Method which checks if page needs to be automatically refreshed, 
        /// e.g. when the map image url is missing
        /// </summary>
        private void SetupRefreshPage()
        {
            #region Refresh page if map image not found

            // Add a refresh to itself if no map image url is found, because the session
            // could still be updating (printable map url is passed by the client browser through the 
            // TD web service).
            if (string.IsNullOrEmpty(TDSessionManager.Current.InputPageState.MapUrlOutward))
            {
                // Check if refresh attempted before
                string refreshAttempt = (string)Request.QueryString["refresh"];

                // Get the URL to redirect to
                string url = Request.RawUrl;

                // First refresh attempt
                if (string.IsNullOrEmpty(refreshAttempt))
                {
                    url += "&refresh=1";

                    headElementControl.AddAutoRedirect(1, url);
                }
                else
                {
                    try
                    {
                        // Check attempt number
                        int attemptNumber = Convert.ToInt32(refreshAttempt);

                        // Update url
                        url = url.Replace("&refresh=" + attemptNumber, "&refresh=" + (attemptNumber + 1));

                        if ((attemptNumber >= 0) && (attemptNumber <= 5))
                        {
                            headElementControl.AddAutoRedirect(1, url);
                        }
                    }
                    catch
                    {
                        // Any exceptions, then someone may have tampered with the refresh value, 
                        // so cancel and dont add the refresh
                    }
                }
            }
            #endregion
        }

        #endregion
    }
}
