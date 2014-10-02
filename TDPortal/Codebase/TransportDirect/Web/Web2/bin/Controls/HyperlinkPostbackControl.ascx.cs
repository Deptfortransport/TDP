// *********************************************** 
// NAME                 : HyperlinkPostbackControl.ascx 
// AUTHOR               : James Broome
// DATE CREATED         : 24/06/2005
// DESCRIPTION          : Control detects whether Javascript support
//						: is enabled. If yes, then renders a hyperlink
//						: if no, then link text is rendered as plain text,
//						: with an adjacent image button to perform the action. 
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/HyperlinkPostbackControl.ascx.cs-arc  $ 
//
//   Rev 1.4   Feb 23 2010 12:25:46   apatel
//Updated to expose the linkbutton control for popup message control
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.3   Nov 15 2009 11:01:38   mmodi
//Updated to allow javascript to be attached to the hyperlink
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.2   Mar 31 2008 13:21:02   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:14:54   mturner
//Initial revision.
//
//   Rev 1.9   Mar 13 2006 15:20:04   pcross
//Manual merge for stream3353
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.5.2.1   Feb 24 2006 14:34:20   NMoorhouse
//Changes to support the addition of new page to display CarDetails
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.5.2.0   Jan 10 2006 10:31:08   pcross
//Update to allow a non-Javascript mode that shows just a button and not  a label and a "Go" button.
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.8   Feb 23 2006 16:11:26   halkatib
//Merge of stream3129 enhanced exposed services
//
//   Rev 1.7   Feb 10 2006 12:24:46   tolomolaiye
//Merge for stream 3180 - Homepage Phase 2
//
//   Rev 1.6   Jan 25 2006 11:58:12   pcross
//Removed default tooltip (which said "Go")
//Resolution for 3505: UEE: Inconsistency in use of tooltips
//
//   Rev 1.5.1.0   Dec 01 2005 13:53:16   AViitanen
//Refactored to use TDLinkButton, as part of JavaScript changes
//Resolution for 3180: DEL 8.1 Stream UEE: work on Homepage phase 2
//
//   Rev 1.5   Nov 15 2005 11:26:12   RGriffith
//UEE Button replacement Code Review suggested changes
//
//   Rev 1.4   Nov 09 2005 15:27:48   rgreenwood
//TD089 ES020 Code review actions
//
//   Rev 1.3   Nov 07 2005 14:28:58   ralonso
//Problem with AltText resolved
//
//   Rev 1.2   Nov 03 2005 17:06:48   kjosling
//Automatically merged from branch for stream2816
//
//   Rev 1.1.1.0   Oct 26 2005 18:37:24   rgreenwood
//TD089 ES020 Image Button Replacement
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages & HTML buttons
//
//   Rev 1.1   Jul 22 2005 19:59:12   RPhilpott
//Add printing support, add ToolTip to link, expose CommandName. 
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.0   Jun 29 2005 11:09:06   jbroome
//Initial revision.
//Resolution for 2556: DEL 8 Stream: Accessibility Links

namespace TransportDirect.UserPortal.Web.Controls
{
	using System;	
	using TransportDirect.Common.ResourceManager;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	using TransportDirect.Web.Support;

	/// <summary>
	///	Control consists of a link button, a label and an image button.
	///	If JavaScript is supported, then the link button is displayed, 
	///	rendered as a hyperlink. If not, then plain text is displayed 
	///	with an adjacent image button.	
	/// </summary>
	public partial class HyperlinkPostbackControl : TDPrintableUserControl
	{

		private string hyperlinkText = string.Empty;
		private string toolTipText = string.Empty;
		private string buttonText = string.Empty;
		private bool showLabelForNonJS = true;

		public event EventHandler link_Clicked;
		
		/// <summary>
		/// Page load sets default properties for image button
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, System.EventArgs e)
		{

			if (buttonText.Length == 0)
			{
				buttonText = GetResource("HyperlinkPostbackControl.button.DefaultText");
			}
			
		}

		
		/// <summary>
		/// OnPreRender event sets visibility of controls
		/// </summary>
		/// <param name="e"></param>
		protected override void OnPreRender(EventArgs e)
		{
			if	(!PrinterFriendly)
			{
				// If Javascript supported, show hyperlink
				if ((Page as TDPage).IsJavascriptEnabled)
				{
					tdLinkHyperlink.Visible = true;
					tdLinkHyperlink.Text = hyperlinkText;
					tdLinkHyperlink.ToolTip = toolTipText;
					labelPlainText.Visible = false;
					button.Visible = false;
				}
					// Else, show label and associated button
				else
				{
					// Only show label if governing property allows.
					// Additionally, if label is not shown then instead of the button saying "Go", it needs
					// to pick up the text that is normally displayed for the hyperlink
					if (showLabelForNonJS)
					{
						labelPlainText.Visible = true;
						labelPlainText.Text = hyperlinkText;
						button.Text = buttonText;
						button.ToolTip = toolTipText;
					}
					else
					{
						labelPlainText.Visible = false;
						button.Text = hyperlinkText;
						button.ToolTip = toolTipText;
					}

					button.Visible = true;
					tdLinkHyperlink.Visible = false;
				}
			}
			else
			{
				// in printable mode, just display a plain text 
				// label whether javascipt is supported or not.
				labelPlainText.Visible = true;
				labelPlainText.Text = hyperlinkText;
				button.Visible = false;
				tdLinkHyperlink.Visible = false;
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
			this.button.Click += new EventHandler(this.button_Click);
            this.tdLinkHyperlink.Click += new EventHandler(this.linkHyperlink_Click);
		}
		#endregion

		/// <summary>
		/// Method handles click event for link button
		/// Raises public event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void linkHyperlink_Click(object sender, System.EventArgs e)
		{
			if (link_Clicked != null)
			{
				link_Clicked(this, e);	
			}
		}

		/// <summary>
		/// Method handles click event for image button
		/// Raises public event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void button_Click(object sender, EventArgs e)
		{
			if (link_Clicked != null)
			{
				link_Clicked(this, e);	
			}
		}
	
		/// <summary>
		/// Read/write property
		/// Used to set the text of the hyperlink
		/// </summary>
		public string Text
		{
			get { return hyperlinkText; }
			set { hyperlinkText = value; }
		}

		
		/// <summary>
		/// Read/write property
		/// Used to override the default
		/// ToolTip text for the td button
		/// </summary>
		public string ToolTipText
		{
			get { return toolTipText; }
			set { toolTipText = value; }
		}

		/// <summary>
		/// Read/write property
		/// Exposes the CommandName of 
		/// the embedded td button    
		/// </summary>
		public string CommandName
		{
			get { return button.CommandName; }
			set { button.CommandName = value; }
		}

		/// <summary>
		/// Read/write property
		/// Exposes the CommandArgument of 
		/// the embedded image button  
		/// </summary>
		public string CommandArgument
		{
			get { return button.CommandArgument; }
			set { button.CommandArgument = value; }
		}

        /// <summary>
        /// Read/write property.
        /// Adds OnClientClick javascript to fire when the Hyperlink is clicked. 
        /// This will prevent a page postback if set.
        /// </summary>
        public string ClientSideJavascript
        {
            get { return tdLinkHyperlink.ClientSideJavascript; }
            set { tdLinkHyperlink.ClientSideJavascript = value; }
        }

		/// <summary>
		/// Read/write property
		/// Handles whether the label is visible when viewing the control in non-Javascript mode
		/// Default is visible.
		/// </summary>
		public bool ShowLabelForNonJS
		{
			get { return showLabelForNonJS; }
			set { showLabelForNonJS = value; }
		}

        /// <summary>
        /// Read only property
        /// Provides access to the link button of this control
        /// </summary>
        public LinkButton LinkHyperLink
        {
            get { return tdLinkHyperlink; }
        }

	}
}
