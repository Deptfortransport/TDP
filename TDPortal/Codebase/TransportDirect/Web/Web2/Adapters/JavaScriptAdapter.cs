// *********************************************** 
// NAME			: JavaScriptAdapter.cs
// AUTHOR		: Richard Hopkins
// DATE CREATED	: 07/12/2005
// DESCRIPTION	: Provides helper/adapter methods for pages that use JavaScript
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Adapters/JavaScriptAdapter.cs-arc  $
//
//   Rev 1.3   Oct 09 2008 12:54:06   mturner
//Updated for XHTML compliance
//
//   Rev 1.2.1.0   Sep 15 2008 10:46:06   mmodi
//Updated for xhtml compliance
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.2   Mar 31 2008 12:59:04   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:11:24   mturner
//Initial revision.
//
//   Rev 1.1   Jan 24 2006 14:40:44   rhopkins
//Added method to use clientside JavaScript refresh if server thinks JavaScript not enabled.
//Resolution for 3322: DN077 -  Missing button on initial landing on Travel news page
//
//   Rev 1.0   Dec 08 2005 14:28:42   rhopkins
//Initial revision.
//

using System.Web.UI;

namespace TransportDirect.UserPortal.Web.Adapters
{
	/// <summary>
	/// This class provides helper/adapter methods for use by input pages 
	/// These methods are not visit planner specific.
	/// </summary>
	public class JavaScriptAdapter
	{

		#region Constructor

		/// <summary>
		/// Constructor
		/// </summary>
		public JavaScriptAdapter()
		{}

		#endregion

		#region Public Methods

		/// <summary>
		/// This method uses clientside JavaScript to set the initial visibility of a control.
		/// This ensures that the visibility of the control is only altered if JavaScript is enabled.
		/// It uses inline code rather than a function call because we cannot guarantee
		/// that the browser has loaded the JavaScript scripts by the time this code executes.
		/// </summary>
		/// <param name="controlId"></param>
		/// <param name="visible"></param>
		public static void InitialiseControlVisibility(Control thisControl, bool visible)
		{
			string controlId = thisControl.ClientID;
			string displayStyle = (visible) ? "" : "none";

            string scriptString = @"<script language=""JavaScript"" type=""text/javascript"">"
                + "\r\n <!--  \r\n"
                + "//<![CDATA[ \r\n"
                + "var thisElement = document.getElementById('"
				+ controlId
                + "'); \r\n"
                + "if (thisElement && thisElement.style) { thisElement.style.display='"
				+ displayStyle
                + "';}; \r\n"
                + "//]]> \r\n"
                + "--> \r\n"
                + "</script> \r\n";

			thisControl.Page.ClientScript.RegisterStartupScript(typeof(JavaScriptAdapter), "JavaScriptAdapter_InitialiseControlVisibility_" + controlId, scriptString);
		}

		/// <summary>
		/// This method generates clientside JavaScript that will resubmit the page if the
		/// client has JavaScript enabled, but the servier thinks that it is not enabled.
		/// </summary>
		/// <param name="page"></param>
		public static void RedirectJavaScriptNoJavaScript(TDPage page)
		{
			const string JAVASCRIPT_SYNC_CHECK_SUBMITTED = "JavaScriptSyncCheckSubmitted";

			if (!page.IsJavascriptEnabled)
			{
				// Check that we haven't tried to do this already (avoid infinite loop)
				if (page.Session[JAVASCRIPT_SYNC_CHECK_SUBMITTED] == null)
				{
					page.Session.Add(JAVASCRIPT_SYNC_CHECK_SUBMITTED, true);  // Infinite loop flag

					page.ClientScript.RegisterClientScriptBlock(typeof(JavaScriptAdapter), "RedirectJavaScriptNoJavaScript",
						"<script language=\"JavaScript\"> "
						+ "if (document.getElementById) { "
						+ "document.getElementsByName(\"hdnTest\")[0].value=\"true\";"
						+ "document.getElementsByName(\"hdnDOMStyle\")[0].value=\"W3C_STYLE\";"
						+ "document.forms[0].submit(); } "
						+ "</script>");
				}
			}
			else
			{
				page.Session.Add(JAVASCRIPT_SYNC_CHECK_SUBMITTED, null);  // Clear infinite loop flag
			}
		}

		#endregion
	}
}
