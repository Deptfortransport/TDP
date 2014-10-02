// *********************************************** 
// NAME                 : HeadElementControl.cs 
// AUTHOR               : Marcus Tillett
// DATE CREATED         : 23/11/2005
// DESCRIPTION			: A web custom control to output <HEAD> tag HTML for a host page dependent on
//						  the properties set in this control.
// NOTES				: (Modified version of the control produced for white label stream)
/*
* USING THE CONTROL ON A PAGE
*
*   TO ADD THE CONTROL
*   The control cannot be added by drag and drop. Edit the HTML directly by adding the following lines:
*
*   Register the assembly providing the control (if not regisered on the page already)
*    <%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
*
*   Reference the control between the existing <head></head> element tags
*    <cc1:HeadElementControl id="HeadElementControl1" runat="server" stylesheets="[Stylesheet list]"></cc1:HeadElementControl>
*   The [Stylesheet list] is a comma delimited list of stylesheets. The stylesheets that are already referenced on the
*   page need to be included in the list and those existing references can then be removed.
*   E.g. stylesheets="setup.css,jpstd.css,CalendarSS.css"
*   Other lines between the <head></head> element tags can be removed where they are replaced by this control.
*
*   Once the control is added to the HTML markup the PageTitle property can also be removed, assuming it is the
*   default page title ("JourneyPlanner.DefaultPageTitle").
*
*   All references to the literalPageTitle control must be removed as this is superseded by code in this control.
*/
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/HeadElementControl.cs-arc  $
//
//   Rev 1.10   Jan 18 2010 12:13:26   mmodi
//Don't append URL in auto refresh if not specified
//Resolution for 5375: Maps - Printer friendly map page refresh change
//
//   Rev 1.9   Oct 01 2009 16:25:10   apatel
//Updates for Social Bookmark links
//Resolution for 5305: CCN530 Social Bookmarking
//
//   Rev 1.8   Jan 16 2009 09:16:36   apatel
//XHTML Compliance changes
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.7   Oct 14 2008 16:54:06   mturner
//Removed unused variable to stop compiler warning
//
//   Rev 1.6   Jul 18 2008 11:29:44   apatel
//removed vs_targetSchema metatag.
//Resolution for 5074: correct invalid meta tag on homepage
//
//   Rev 1.5   Jul 18 2008 10:11:38   apatel
//meta tag desc change to description
//Resolution for 5074: correct invalid meta tag on homepage
//
//   Rev 1.4   May 07 2008 10:27:02   apatel
//added method IsStyleSheetExist to check if partner style sheet folder actually got the stylesheet.
//Resolution for 4953: partner stylesheet files  giving HTTP 404 error
//
//   Rev 1.3   May 01 2008 17:32:16   mmodi
//Updated to include autoredirect method
//Resolution for 4888: Del 10.1 - Improve Session Timeout Management
//
//   Rev 1.2   Mar 31 2008 13:20:58   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:14:40   mturner
//Initial revision.
//
//  Rev DevFactory  Mar 07 2008 sbarker
//  Update to look for styles in the themes folder.
//
//   Rev 1.3   Dec 30 2005 12:00:54   NMoorhouse
//Updated following screen review
//Resolution for 3180: DEL 8.1 Stream UEE: work on Homepage phase 2
//
//   Rev 1.2   Nov 29 2005 15:02:36   RGriffith
//Changes to tab spacing
//
//   Rev 1.1   Nov 24 2005 16:20:48   mtillett
//Remove duplicate content-type declaration, update comments and file history
//
//   Rev 1.0   Nov 24 2005 14:52:52   RGriffith
//Initial revision.


using System;
using System.Text;
using System.Web.UI;
using System.ComponentModel;
using TransportDirect.Common.DatabaseInfrastructure.Content;
using TD.ThemeInfrastructure;
using System.IO;


namespace TransportDirect.UserPortal.Web.Controls
{
	/// <summary>
	/// A web custom control to output HEAD tag HTML for a host page dependent on
	//	the properties set in this control.
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	[ToolboxData("<{0}:HeadElementControl runat=server></{0}:HeadElementControl>")]
	public class HeadElementControl : System.Web.UI.WebControls.WebControl
	{

		#region Private member variables

		private string stylesheets = String.Empty;
		private string desc = String.Empty;
		private string keywords = String.Empty;
        private string imageSrc = string.Empty;
                
        private string autoRedirect = String.Empty;
        private string autoRedirectUrl = String.Empty;
        
		#endregion

        /// <summary>
		/// Renders the control. In this case just outputs a "<HEAD></HEAD>" element and appropriate contents
		/// </summary>
		/// <param name="writer"></param>
		protected override void Render(HtmlTextWriter output)
		{
			// Output the page title
			output.WriteLine("<title>" + ((TDPage)this.Page).PageTitle + "</title>");

			// Output the auto-generated guff
			output.WriteLine("		<meta name=\"GENERATOR\" content=\"Microsoft Visual Studio 8.0\" />");
			output.WriteLine("		<meta name=\"CODE_LANGUAGE\" content=\"C#\" />");
			output.WriteLine("		<meta name=\"vs_defaultClientScript\" content=\"JavaScript\" />");
			output.WriteLine("		<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />");

            if (!string.IsNullOrEmpty(autoRedirect))
            {
                output.WriteLine(autoRedirect);
            }

			//output Desc value
			if (desc.Length > 0)
			{
				output.WriteLine("		<meta name=\"description\" content=\"" + desc + "\" />");
			}
			//output Keywords value
			if (keywords.Length > 0)
			{
				output.WriteLine("		<meta name=\"keywords\" content=\"" + keywords + "\" />");
			}

            //output Image Src value
            if (imageSrc.Length > 0)
            {
                output.WriteLine("		<link rel=\"image_src\" href=\"" + imageSrc + "\" />");
            }

            //Persist the stylesheet folder to save repeated lookups:
            string styleFolder = this.stylesheetFolder;

			// For each comma separated stylesheet in the property, output a stylesheet reference
			string[] s = stylesheets.Split(',');
            foreach(string sheet in s)
			{
                output.WriteLine("		<link href=\"" + styleFolder + sheet.Trim() + "\" type=\"text/css\" rel=\"stylesheet\" media=\"all\" />");
			}

            // When we're in partner mode, then need to pick up stylesheets for the partner
            Theme defaultTheme = TD.ThemeInfrastructure.ThemeProvider.Instance.GetDefaultTheme();
            Theme currentTheme = TD.ThemeInfrastructure.ThemeProvider.Instance.GetTheme();

            //We add an entry to support custom favourite (etc..) images:
            string favIconPath = string.Format("{0}/App_Themes/{1}/Images/favicon.ico", Page.Request.ApplicationPath, currentTheme.Name);
            output.WriteLine(@"		<link rel=""SHORTCUT ICON"" href=""" + favIconPath + @""" type=""image/x-icon"" />");
             
            if (currentTheme.Id != defaultTheme.Id)
            {
                foreach (string sheet in s)
                {
                    //Check if the file really exist in theme folder and only then add css to page
                    if (IsStyleSheetExists(currentTheme.Name, sheet.Trim()))
                    {
                        string partnerCss = string.Format(@"{0}{1}/{2}", styleFolder, currentTheme.Name, sheet.Trim());
                        output.WriteLine("		<link href=\"" + partnerCss + "\" type=\"text/css\" rel=\"stylesheet\" media=\"all\" />");
                    }
                }
            }

            // Printable pages have the following output too
			if (this.Page is TDPrintablePage)
			{
                output.WriteLine("		<link href=\"" + styleFolder + "PrintablePageWhiteBackGroundOverride.css\" type=\"text/css\" rel=\"stylesheet\" media=\"all\" />");
				output.WriteLine("		<style type=\"text/css\" media=\"print\">.onscreen { VISIBILITY: hidden }</style>");
			}
		}

        private bool IsStyleSheetExists(string themeName, string stylesheet)
        {
            bool styleSheetFound = false;

            string styleFolder = this.stylesheetFolder;

            try
            {
                string path = string.Format("{0}{1}/{2}", styleFolder, themeName , stylesheet.Trim());

                string physicalpath = System.Web.HttpContext.Current.Server.MapPath(path);
                styleSheetFound = File.Exists(physicalpath);
            }
            catch (Exception ex)
            {
                //We rebrand the error here:
                StringBuilder message = new StringBuilder();

                message.Append("An error occurred in the method IsStyleSheetExists() ");
                message.Append("of the HeadElementControl class. It is likely ");
                message.Append("to have been a problem with permissions denied to access the file");
                message.Append("See inner exception for details.");

                //Note that we should never see this error during execution of the site in a
                //live environment, unless someone has made a bad mistake with the contents
                //of the Content database:
                throw new Exception(message.ToString(), ex);
            }

            return styleSheetFound;
        }

		#region Properties

		/// <summary>
		/// Read/write property containing comma 
		/// seperated list of css files
		/// </summary>
		[Bindable(true)
		, Category("Appearance")
		, Description("Comma seperated list of css files")] 
		public string Stylesheets
		{
			get {return stylesheets;}
			set {stylesheets = value;}
		}

		/// <summary>
		/// Read/write property for the Desc meta tags
		/// </summary>
		[Bindable(true)
		, Category("Appearance")
		, Description("Content of the Desc meta tag")] 
		public string Desc
		{
			get {return desc;}
			set {desc = value;}
		}

		/// <summary>
		/// Read/write property for the Keywords meta tags
		/// </summary>
		[Bindable(true)
		, Category("Appearance")
		, Description("Content of the Keywords meta tag")] 
		public string Keywords
		{
			get {return keywords;}
			set {keywords = value;}
		}

        /// <summary>
        /// Read/write property for the Keywords meta tags
        /// </summary>
        [Bindable(true)
        , Category("Appearance")
        , Description("Content of the image_src link reference")]
        public string ImageSource
        {
            get { return imageSrc; }
            set { imageSrc = value; }
        }

		#endregion

        #region Public Methods

        /// <summary>
        /// Method to add an autoredirect value to a page head section. 
        /// This must be called on page load
        /// </summary>
        /// <param name="redirectSeconds"></param>
        /// <param name="redirectUrl"></param>
        public void AddAutoRedirect(int redirectSeconds, string redirectUrl)
        {
            StringBuilder redirectString = new StringBuilder();
            redirectString.Append("		<meta http-equiv=\"refresh\" content=\"");
            redirectString.Append(redirectSeconds);

            if (!string.IsNullOrEmpty(redirectUrl))
            {
                redirectString.Append(";url=");
                redirectString.Append(redirectUrl);
            }

            redirectString.Append("\" />");

            autoRedirect = redirectString.ToString();
        }

        #endregion

        #region Private Properties

        /// <summary>
        /// Gets the location of the style sheet folder
        /// </summary>
        private string stylesheetFolder
        {
            get
            {
                return string.Format("{0}/Styles/", Page.Request.ApplicationPath);
            }
        }

        private string pageName
        {
            get
            {
                string[] pathSections = Page.Request.FilePath.Split('/');
                return pathSections[pathSections.Length - 1];
            }
        }

        #endregion
	}
}
