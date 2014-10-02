// *************************************************************************** 
// NAME                 : PrinterFriendlyPageButtonControl.ascx.cs
// AUTHOR               : Richard Hopkins
// DATE CREATED         : 17/02/2005
// DESCRIPTION			: This control displays a button link to open a printable version
//						of the current page in a new window
// *************************************************************************** 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/PrinterFriendlyPageButtonControl.ascx.cs-arc  $
//
//   Rev 1.5   Nov 05 2009 14:56:24   apatel
//mapping enhancement code changes
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.4   Oct 14 2008 14:16:38   mmodi
//Manual merge for stream5014
//
//   Rev 1.3   Jun 26 2008 14:04:20   apatel
//CCN 464 Accessibility related changes
//Resolution for 5019: CCN0458 - Accessability Updates improved linking
//
//   Rev 1.2.1.0   Aug 22 2008 10:34:12   mmodi
//Added an override page property
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.2   Mar 31 2008 13:22:30   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:17:04   mturner
//Initial revision.
//
// dev factory Feb 04 2008 sbarker
//Slight alteration to remove '~/' if it exists at the start of the button URL.
//
//   Rev 1.9   Feb 23 2006 16:13:22   halkatib
//Merge of stream3129 enhanced exposed services
//
//   Rev 1.8   Dec 08 2005 14:39:42   rhopkins
//Use clientside JavaScript to determine whether to display the JavaScript version of the Printer Friendly button.  This ensures that it is always correct, even if the Server does not know whether JavaScript is enabled.
//Resolution for 3219: UEE: Html Buttons: Park and Ride buttons are not aligned
//
//   Rev 1.7   Nov 30 2005 18:01:02   rhopkins
//Ensure that browser does not retrieve printer friendly page from cache.
//Also, corrections to the button alignments.
//Resolution for 3179: Visit Planner - Printer friendly page for results details displays the summary only
//Resolution for 3216: UEE: Javascript disabled - Printer friendly button uses flat style
//
//   Rev 1.6   Nov 10 2005 18:01:28   rhopkins
//Removed image button from hyperlink - used text instead so that it looks like an HTML button.
//
//   Rev 1.5   Nov 04 2005 17:15:38   rhopkins
//Allow URL to be set explicitly
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages & HTML buttons
//
//   Rev 1.4   Nov 03 2005 16:10:50   kjosling
//Automatically merged from branch for stream2816
//
//   Rev 1.3.1.1   Oct 10 2005 18:29:04   rhopkins
//Correction to resource ID
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages
//
//   Rev 1.3.1.0   Oct 10 2005 18:26:02   rhopkins
//Changed to use HTML button
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages
//
//   Rev 1.3   Jun 01 2005 09:37:18   pcross
//Changes after Bobby WAI check
//Resolution for 2512: DEL 7 - WAI changes - following Bobby review
//
//   Rev 1.2   May 20 2005 09:55:14   rgeraghty
//Changed to use a scriptable hyperlink control to enable javascript functionality if required
//Resolution for 2493: Del 7 Car - Server error in '/Web' Application on printer friendly page
//
//   Rev 1.1   Apr 13 2005 14:49:14   asinclair
//Fix for 1984 and 2044
//
//   Rev 1.0   Feb 18 2005 11:53:56   rhopkins
//Initial revision.
//
namespace TransportDirect.UserPortal.Web.Controls
{
	using System;	
	using TransportDirect.Common.ResourceManager;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	using TransportDirect.Common;
	using TransportDirect.Common.ServiceDiscovery;
	using TransportDirect.UserPortal.ScreenFlow;
	using TransportDirect.UserPortal.SessionManager;
	using TransportDirect.UserPortal.Web.Adapters;
	using TransportDirect.Web.Support;

	/// <summary>
	///		Summary description for PrinterFriendlyPageButton.
	/// </summary>
	public partial class PrinterFriendlyPageButtonControl : TDUserControl
	{


		/// <summary>
		/// Name of script (in script repository) used for opening window
		/// </summary>
		private const string JAVASCRIPT_FILE = "ErrorAndTimeoutLinkHandler";

		private string baseUrl;
		private string urlParams;

        private bool usePageIdOverride = false;
        private PageId pageIdOverride = PageId.Empty;

		#region eventhandlers

		/// <summary>
		/// Page Load event handler
		/// </summary>
		/// <param name="sender">Notifying object</param>
		/// <param name="e">Event data</param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// register the js files
			RegisterJavaScriptFile();
		}

		/// <summary>
		/// Page pre render event handler
		/// </summary>
		/// <param name="sender">Notifying object</param>
		/// <param name="e">Event data</param>
		protected void Page_PreRender(object sender, EventArgs e)
		{
			DataBind();
		}

		#endregion

		/// <summary>
		/// Registers the ErrorAndTimeoutLinkHandler javascript file on the page		
		/// </summary>
		private void RegisterJavaScriptFile()
		{
			/// Note that this code is not dependant on javascript being enabled as it is not 
			/// possible to evaluate whether javascript is enabled if the page is accessed from a hyperlink
			/// e.g. printer friendly when no postback occurs
			string javaScriptDom = ((TDPage)Page).JavascriptDom;			
			ScriptRepository.ScriptRepository scriptRepository = (ScriptRepository.ScriptRepository)TDServiceDiscovery.Current[ServiceDiscoveryKey.ScriptRepository];
			
			// Output reference to necessary JavaScript files from the ScriptRepository
			Page.ClientScript.RegisterClientScriptBlock(typeof(PrinterFriendlyPageButtonControl), "ErrorAndTimeoutLinkHandler", scriptRepository.GetScript("ErrorAndTimeoutLinkHandler", javaScriptDom));		
		}

		#region Protected methods

		/// <summary>
		/// Returns value indicating if client has Javascript enabled.
		/// </summary>
		/// <returns>True if enabled, false otherwise</returns>
		protected bool GetEnableClientScript() 
		{
			return ((TDPage)Page).IsJavascriptEnabled;
		}


		/// <summary>
		/// Returns the Javascript function to execute when the button is clicked
		/// </summary>
		/// <returns>string</returns>
		/// <param name="url">url</param>
		protected string GetAction(string url)
		{
			return	"return OpenWindow('" + url + "');";
		}

		/// <summary>
		/// Returns the name of the Javascript file containing the code to execute when the 
		/// button is clicked
		/// </summary>
		/// <returns></returns>
		protected string GetScriptName() 
		{
			return JAVASCRIPT_FILE;
		}

		#endregion

		/// <summary>
		/// On Pre Render Method.		
		/// </summary>
		protected override void OnPreRender(System.EventArgs e)
		{
			if (this.Visible)
			{
				try
				{
					// If a URL has not been set then derive it from the current page
					if (baseUrl == null)
					{
						// Get the page ID of the current page and stick the word "Printable" on the front to create the desired printable page ID
						// If this fails then there is no appropriately named printable page, and the try...catch block will detect this
                        PageId printablePageId;
                        if (!usePageIdOverride)
                        {
                            printablePageId = (PageId)Enum.Parse(typeof(PageId), "Printable" + ((TDPage)Page).PageId.ToString());
                        }
                        else
                        {
                            // Check if we've been passed a "Printable" page, otherwise prefix with this 
                            string pageid = pageIdOverride.ToString();

                            if (pageid.StartsWith("Printable"))
                            {
                                printablePageId = pageIdOverride;
                            }
                            else
                            {
                                printablePageId = (PageId)Enum.Parse(typeof(PageId), "Printable" + pageid);
                            }
                        }
                        
						IPageController pageController =
							(PageController)TDServiceDiscovery.Current
							[ServiceDiscoveryKey.PageController];

						// Get the URL for a PageID
						PageTransferDetails pageTransferDetails =
							pageController.GetPageTransferDetails(printablePageId);

						if (pageTransferDetails != null)
						{
							if (TDPage.SessionChannelName !=  null )
							{
								baseUrl = TDPage.getBaseChannelURL(TDPage.SessionChannelName) + pageTransferDetails.PageUrl;
							}
							else
							{
								baseUrl = pageTransferDetails.PageUrl;
							}
						}
					}

					// If there is still no URL for a printable version of this page then don't display the button
					if (baseUrl == null)
					{
						this.Visible = false;
					}
					else
					{
                        //Before we set up the target URL, we must correct the 
                        //base URL:
                        //Get the page and subfolder from the base URL it is the 
                        //last section:
                        //We assume that the printable page always exists in a sub 
                        //folder:
                        string[] sections = baseUrl.Split('/');
                        string page = sections[sections.Length - 1];
                        string subFolder = sections[sections.Length - 2];
                        baseUrl = string.Format("{0}/{1}/{2}", Request.ApplicationPath, subFolder, page);

						// Set up the target URL
						string targetURL;

						if (urlParams == null)
						{
							if (baseUrl.IndexOf("?") != -1)
							{
                                targetURL = baseUrl + "&" + "id=" + Request.QueryString["id"] + "&cacheparam=" + TDSessionManager.Current.CacheParam;					
							}
							else 
							{
                                targetURL = baseUrl + "?" + "id=" + Request.QueryString["id"] + "&cacheparam=" + TDSessionManager.Current.CacheParam;					
							}
						}
						else
						{
							if (baseUrl.IndexOf("?") != -1)
							{
								targetURL = baseUrl + "&" + "id=" + Request.QueryString["id"] + "&cacheparam=" + TDSessionManager.Current.CacheParam + "&" + urlParams;
							} 
							else 
							{
								targetURL = baseUrl + "?" + "id=" + Request.QueryString["id"] + "&cacheparam=" + TDSessionManager.Current.CacheParam + "&" + urlParams;
							}
						}

                        // Update the parameter used to defeat browser caching
						TDSessionManager.Current.CacheParam++;

						// Set up the HTML buton
						printButton.Action = GetAction(targetURL);
						printButton.Text = GetResource("PrinterFriendlyPageButton.Text");
                        printButton.ToolTip = GetResource("PrinterFriendlyButton.Tooltip");

						// Use the hyperlink
						hyperlinkPrint.NavigateUrl = targetURL;
						hyperlinkText.Text = printButton.Text;
                        hyperlinkPrint.ToolTip = GetResource("PrinterFriendlyButton.Tooltip");

						JavaScriptAdapter.InitialiseControlVisibility(printButton, true);
					}
				}
				catch (ArgumentNullException)
				{
					// There is no "Printable" version of the current PageID, so don't show the link
					this.Visible = false;
				}
				catch (ArgumentException)
				{
					// There is no "Printable" version of the current PageID, so don't show the link
					this.Visible = false;
				}
			}
		}


		/// <summary>
		/// Gets/Sets the URL for the printer friendly page.
		/// This should only be used where the correct URL cannot be derived from the ID of the normal page
		/// </summary>
		public string Url
		{
			get { return baseUrl; }

			set
			{
				// URLs won't be set very often,
				// so put the faff in here to keep the frequently executed main-body code simple
				if ((value == null) || (value.ToString().Trim().Length == 0))
				{
					baseUrl = null;
				}
				else
				{
					baseUrl = value.ToString().Trim();
				}
			}
		}

		/// <summary>
		/// Gets and sets the urlParams.
		/// </summary>
		/// <remarks>
		/// String added to Url to refine display of PrinterFriendly pages.
		/// </remarks>
		public string UrlParams
		{
			get { return urlParams; }

			set
			{
				// URL parameters won't be set very often,
				// so put the faff in here to keep the frequently executed main-body code simple
				if ((value == null) || (value.ToString().Trim().Length == 0))
				{
					urlParams = null;
				}
				else
				{
					urlParams = value.ToString().Trim();
				}
			}
		}

        /// <summary>
        /// Gets/sets the PageId override value. This should be used by the caller to invoke 
        /// a different printable page. 
        /// If a Printable PageId is passed, then this will be used.
        /// If a normal PageId is passed, this will be prefixed with "Printable" to obtain printable PageId
        /// Ensure UsePageIdOverride flag is set to true to use this PageIdOverride
        /// </summary>
        public PageId PageIdOverride
        {
            get { return pageIdOverride; }
            set { pageIdOverride = value; }
        }

        /// <summary>
        /// Gets/sets the flag indicating if control should use the PageIdOverride value.
        /// </summary>
        public bool UsePageIdOverride
        {
            get { return usePageIdOverride; }
            set { usePageIdOverride = value; }
        }

        /// <summary>
        /// Read only property returning print button control
        /// </summary>
        public TDButton PrintButton
        {
            get
            {
                return printButton;
            }
        }

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			Page.PreRender +=new EventHandler(Page_PreRender);
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
