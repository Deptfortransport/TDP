// *************************************************************************** 
// NAME                 : ScrollManager.cs
// AUTHOR               : Jonathan George
// DATE CREATED         : 08/03/2005
// *************************************************************************** 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Support/ScrollManager.cs-arc  $using System;
//
//   Rev 1.2   Mar 31 2008 13:27:12   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:28:54   mturner
//Initial revision.
//
//   Rev 1.2   Feb 23 2006 19:17:16   build
//Automatically merged from branch for stream3129
//
//   Rev 1.1.1.0   Jan 30 2006 16:34:38   kjosling
//moved TDCulture to common project
//
//   Rev 1.2.1.0   Jan 30 2006 16:26:06   mdambrine
//invalid checking removed from the trunk
//
//   Rev 1.2   Jan 30 2006 14:41:28   mdambrine
//moved TDCultureInfo to the common project
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.1   Mar 22 2005 09:48:00   jgeorge
//FxCop changes
//
//   Rev 1.0   Mar 10 2005 12:44:48   jgeorge
//Initial revision.

using System.Collections;
using System.Text;
using System.Web.UI.HtmlControls;

using TransportDirect.UserPortal.Web;
using TransportDirect.UserPortal.ScriptRepository;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.ResourceManager;
using TransportDirect.Common;

namespace TransportDirect.Web.Support
{
	/// <summary>
	/// Provides functionality to specify the point at which a page should be scrolled
	/// to when it is loaded in the browser.
	/// </summary>
	public class ScrollManager
	{
		#region Private constants

		private const string ScriptKeyStartup = "ScrollManagerOutput";
		private const string ScriptKeyScrollManager = "ScrollManager";
		private const string ScriptKeySubmit = "ScrollManagerSubmit";

		private const string XPositionName = "ScrollManager_X";
		private const string YPositionName = "ScrollManager_Y";

		private const string ScriptTagOpen = "<script language=\"javascript\" type=\"text/javascript\">";
		private const string ScriptTagClose = "</script>";

		private const string ScriptScrollToElement = "scrollToElement('{0}');";
		private const string ScriptScrollToClick = "scrollToClick();";
		private const string ScriptScrollToTop = "scrollToTop();";
		private const string ScriptWireEvent = "wireStoreCoordinates();";

		#endregion

		#region Private members

		private ArrayList elementList = new ArrayList();
		private string restElement = string.Empty;
		private bool scrollToClick = true;

		private TDPage managedPage;

		#endregion

		#region Constructor

		/// <summary>
		/// Default constructor
		/// </summary>
		/// <param name="managedPage"></param>
		public ScrollManager(TDPage managedPage)
		{
			this.managedPage = managedPage;
		}

		#endregion

		#region Public methods

		/// <summary>
		/// Adds an element to the list of items to scroll into view when the page is loaded.
		/// This is intended for elements in scrollable areas (ie DIV blocks)
		/// </summary>
		/// <param name="clientId"></param>
		public void ScrollElementToView(string clientId)
		{
			elementList.Add(clientId);
		}

		/// <summary>
		/// Specifies the element to which the page will be scrolled. If this is specified, the 
		/// co-ordinates the user clicked at are ignored.
		/// </summary>
		/// <param name="clientId"></param>
		public void RestPageAtElement(string clientId)
		{
			restElement = clientId;
			scrollToClick = false;
		}

		/// <summary>
		/// Specifies that the page should scroll to the point at which the user clicked the page.
		/// If this is specified, any item specified using RestPageAtElement is ignored.
		/// </summary>
		public void RestPageAtClick()
		{
			restElement = string.Empty;
			scrollToClick = true;
		}

		/// <summary>
		/// Adds the necessary script to the page.
		/// </summary>
		public void EmitScript()
		{
			if (managedPage.IsJavascriptEnabled)
			{
				StringBuilder outputScript = new StringBuilder();
				ScriptRepository scriptRepository = (ScriptRepository)TDServiceDiscovery.Current[ServiceDiscoveryKey.ScriptRepository];

				// Opening tag
				outputScript.Append( ScriptTagOpen );

				foreach (string element in elementList)
					outputScript.Append( string.Format( TDCultureInfo.InvariantCulture, ScriptScrollToElement, element ) );

				if (scrollToClick)
					outputScript.Append( ScriptScrollToClick );
				else if (restElement.Length != 0)
					outputScript.Append( string.Format( TDCultureInfo.InvariantCulture, ScriptScrollToElement, restElement ) );
				else if (elementList.Count != 0)
					outputScript.Append( ScriptScrollToTop );

				outputScript.Append( ScriptWireEvent );

				// Add the closing tag
				outputScript.Append( ScriptTagClose );

				// Add to the page
				if (!managedPage.ClientScript.IsStartupScriptRegistered( ScriptKeyStartup ))
					managedPage.ClientScript.RegisterStartupScript(typeof(ScrollManager), ScriptKeyStartup , outputScript.ToString() );

				// Add hidden fields to the page
				string XPositionValue = managedPage.Request.Params[XPositionName];
				if (XPositionValue == null)
					XPositionValue = "0";

				string YPositionValue = managedPage.Request.Params[YPositionName];
				if (YPositionValue == null)
					YPositionValue = "0";

				managedPage.ClientScript.RegisterHiddenField(XPositionName, XPositionValue);
				managedPage.ClientScript.RegisterHiddenField(YPositionName, YPositionValue);

				// Add the scroll manager script from the script repository
				if (!managedPage.ClientScript.IsClientScriptBlockRegistered(ScriptKeyScrollManager))
					managedPage.ClientScript.RegisterClientScriptBlock(typeof(ScrollManager), ScriptKeyScrollManager, scriptRepository.GetScript(ScriptKeyScrollManager, managedPage.JavascriptDom));
			}
		}

		#endregion
	}
}
