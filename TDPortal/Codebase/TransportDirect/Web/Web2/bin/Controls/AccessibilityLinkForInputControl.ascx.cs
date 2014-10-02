// *********************************************** 
// NAME                 : AccessibilityLinkForInputControl.aspx.cs
// AUTHOR               : James Brooome
// DATE CREATED         : 13/06/2005
// DESCRIPTION			: Control displays link to accessibility information
//						: on DPTAC website
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/AccessibilityLinkForInputControl.ascx.cs-arc  $
//
//   Rev 1.2   Mar 31 2008 13:18:54   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:11:44   mturner
//Initial revision.
//
//   Rev 1.1   Feb 23 2006 19:16:14   build
//Automatically merged from branch for stream3129
//
//   Rev 1.0.1.0   Jan 10 2006 15:23:02   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.0   Jun 29 2005 11:07:56   jbroome
//Initial revision.
//Resolution for 2556: DEL 8 Stream: Accessibility Links

namespace TransportDirect.UserPortal.Web.Controls
{
	using System;using TransportDirect.Common.ResourceManager;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	using TransportDirect.UserPortal.ExternalLinkService;
	using TransportDirect.UserPortal.Web.Controls;

	/// <summary>
	/// Control consists of single hyperlink. 
	/// Displays link to accessibility information
	//	on DPTAC website. 
	/// </summary>
	public partial class AccessibilityLinkForInputControl : TDUserControl 
	{

		/// <summary>
		/// Page load checks validity of external URL.
		/// Only displayed if valid.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Get reference to External Links repository and access correct link
			IExternalLinks externalLinks = ExternalLinks.Current;
			ExternalLinkDetail linkDetail = externalLinks[ExternalLinksKeys.AccessibilityInformation_BeforeTravel];

			// If link is valid, then display it
			if ((linkDetail != null) && (linkDetail.IsValid))
			{
				// Ensure link is visible and set up properties
				this.Visible = true;
				linkAccessibility.Text = GetResource("AccessibilityLinkForInputControl.linkAccessibilityText");
				linkAccessibility.Target = "_blank";
				linkAccessibility.NavigateUrl = linkDetail.Url;
			}
			else
			{
				// External link cannot be found or 
				// is invalid so hide control
				this.Visible = false;
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

		}
		#endregion
	}
}
