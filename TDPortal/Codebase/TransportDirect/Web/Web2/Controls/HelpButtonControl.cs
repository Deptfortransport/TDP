//**************************************************************
//NAME			: HelpButtonControl.cs
//AUTHOR		: Marcus Tillett
//DATE CREATED	: 26/09/2005
//DESCRIPTION	: Custom TD Help Button Control
//**************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/HelpButtonControl.cs-arc  $
//
//   Rev 1.2   Mar 31 2008 13:21:00   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:14:50   mturner
//Initial revision.
//
//   Rev 1.3   Feb 23 2006 19:16:24   build
//Automatically merged from branch for stream3129
//
//   Rev 1.2.1.0   Jan 10 2006 15:25:20   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.2   Oct 12 2005 10:23:14   rgreenwood
//TD089 ES020 Image Button Replacement
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages
//
//   Rev 1.1   Sep 28 2005 17:16:56   MTillett
//Create new help button control and lang strings to match SD008
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.0   Sep 28 2005 16:53:42   MTillett
//Initial revision.

using System;using TransportDirect.Common.ResourceManager;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Web.UI;
using System.Web.UI.WebControls;

using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.Resource;
using TransportDirect.Web;

namespace TransportDirect.UserPortal.Web.Controls
{
	/// <summary>
	/// HelpButtonControl control provides Help button functionality
	/// </summary>	
	[ToolboxData("<{0}:HelpButtonControl runat=server></{0}:HelpButtonControl>"),
	ComVisible(false)]
	public class HelpButtonControl : System.Web.UI.WebControls.PlaceHolder, INamingContainer
	{
		/// <summary>
		/// Event raised when the Help button clicked
		/// </summary>
		public event EventHandler HelpEvent;

        private string helpUrl;

        /// <summary>
        /// Read/Write property that specifies the URL for the Help page
        /// </summary>
        [Bindable(true), 
        Category("Appearance"), 
		Description("Partial URL for the help page"),
        DefaultValue("")] 
        public string HelpUrl 
        {
            get
            {
                return helpUrl;
            }

            set
            {
                helpUrl = value;
            }
        }		

		/// <summary>
		/// To ensure that viewstate is loaded for child controls
		/// EnsureChildControls must be called so that child controls
		/// are always added in the same order and have the same Index. 
		/// (ID is not used for view state of child controls).
		/// </summary>
		public override ControlCollection Controls
		{
			get
			{
				EnsureChildControls();
				return base.Controls;
			}
		}
		/// <summary>
		/// Add image button as child control
		/// </summary>
		protected override void CreateChildControls()
		{
			TDButton help = new TDButton();
			help.ID = "Help";
			help.CausesValidation = false;
			//adds the click event handler
			help.Click += new EventHandler(this.HelpButton_Click);

			TDResourceManager langstrings = TDResourceManager.GetResourceManagerFromCache(TDResourceManager.LANG_STRINGS);
			//set the image and alternate text for this control
			help.Text = langstrings.GetString("HelpButtonControl.Text");
			help.ToolTip = langstrings.GetString("HelpButtonControl.ToolTip");

			this.Controls.Add(help);

			base.CreateChildControls();
		}
		/// <summary>
		/// Click event handler for the Help button control
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void HelpButton_Click(object sender, EventArgs e)
		{
			string url = String.Empty;
			InputPageState pageState = TDSessionManager.Current.InputPageState;
			TDPage page = this.Page as TDPage;
			if( page != null )
			{
				string channelName = TDPage.SessionChannelName;
				if( channelName != null )
				{
					url = TDPage.getBaseChannelURL( channelName );
				}
				url += helpUrl;
				if(HelpEvent != null )
				{
					HelpEvent(this, EventArgs.Empty);
				}
				pageState.JourneyInputReturnStack.Push(page.PageId);
				// Need to ensure all data is properly saved away before exiting the page
				// This is needed because this redirection does not use the ScreenFlow framework.
				TDSessionManager.Current.OnPreUnload();
				page.Response.Redirect(url);			
			}
		}
	}
}