//**************************************************************
//NAME			: HelpLabelControl.cs
//AUTHOR		: Joe Morrissey
//DATE CREATED	: 28/08/2003
//DESCRIPTION	: Custom TD Help Control Label
//**************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/HelpLabelControl.cs-arc  $
//
//   Rev 1.4   Jul 28 2011 16:19:06   dlane
//UK11131377 - Changes for WAI compliance
//Resolution for 5712: Changes for accessibility (WAI)
//
//   Rev 1.3   Jan 09 2009 13:36:22   apatel
//XHTML Compliance Changes
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.2   Mar 31 2008 13:21:00   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:14:50   mturner
//Initial revision.
//
//   Rev 1.22   May 03 2006 17:16:06   AViitanen
//Moved the setting of 'more...' button text to Render. 
//Resolution for 4048: DD075: Soft content refers to Find a Fare and old button names
//
//   Rev 1.21   Apr 09 2006 16:00:56   kjosling
//Updated UI controls
//Resolution for 3847: DN079 Hyperlink colour change: Help "more" button image uses old hyperlink colour
//
//   Rev 1.20   Feb 23 2006 19:16:24   build
//Automatically merged from branch for stream3129
//
//   Rev 1.19.1.1   Jan 30 2006 14:41:08   mdambrine
//moved TDCultureInfo to the common project
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.19.1.0   Jan 10 2006 15:25:22   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.19   Mar 21 2005 15:49:40   jgeorge
//Updated with FxCop fixes
//
//   Rev 1.18   Mar 10 2005 13:05:04   jgeorge
//Removed ScrollToHelp functionality
//
//   Rev 1.17   Jul 28 2004 11:42:38   COwczarek
//Remove unused variable
//Resolution for 1202: Implement FindTrainInput page
//
//   Rev 1.16   Jul 19 2004 09:09:48   COwczarek
//Add new property MoreHelpUrl to allow the URL to be set
//programatically rather than rely on control's id. In page load
//event handler, use client ID to determine if this control is being
//scrolled rather than ID (ensures uniqueness). Also remove
//redundant code.
//Resolution for 1202: Implement FindTrainInput page
//
//   Rev 1.15   Jul 16 2004 14:49:12   COwczarek
//Check for Text property being null before using id to locate text in langstrings
//Resolution for 1202: Implement FindTrainInput page
//
//   Rev 1.14   Jul 15 2004 15:00:08   jmorrissey
//Amended Render method to allow the label's text to be set by other controls/pages, rather than just from the label's own Render method
//
//   Rev 1.13   Apr 01 2004 19:12:00   PNorell
//Updated for keeping the location and other things where they should after going to help page and/or location information.
//
//   Rev 1.12   Mar 18 2004 10:08:16   asinclair
//Added event handler for More help button
//
//   Rev 1.11   Dec 02 2003 14:04:28   asinclair
//Added code to the Render event to set the ALt tags depending on language
//
//   Rev 1.10   Nov 15 2003 15:37:32   PNorell
//Added support (again) for scrolling down to the correct position.
//
//   Rev 1.9   Nov 04 2003 11:02:46   passuied
//restored before anchorage of help labels
//
//   Rev 1.6   Oct 16 2003 13:21:56   passuied
//Replaced More... by imagebutton to implement back from Help
//
//   Rev 1.5   Sep 30 2003 15:22:54   PNorell
//Added support for ensuring only one "window" open on a web page at the same time.
//Fixed numerous click bug in the Help control.
//Fixed numerous language issues with the help control.
//Updated the journey planner input pages to contain the updated code for ensuring one window.
//Updated the wait page and took out the debug logging.
//
//   Rev 1.4   Sep 29 2003 10:03:54   JMorrissey
//Added check to close all calendar controls that are visible to prevent overlapping of controls.
//
//   Rev 1.3   Sep 24 2003 18:41:22   JMorrissey
//Fixed bug where if no MoreHelp text found, an exception was thrown
//
//   Rev 1.2   Sep 18 2003 17:48:06   JMorrissey
//added html for this control
//
//   Rev 1.1   Sep 04 2003 17:46:44   JMorrissey
//Rendering of this control is still TODO, once the BBC have provided the styles and layout

using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Web.UI.Design.WebControls;
using System.Web.UI.Design; 
using System.Threading;
using System.Runtime.InteropServices;

using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.Resource;
using TransportDirect.Web.Support;
using TransportDirect.Common.ResourceManager;
using TransportDirect.Common;


namespace TransportDirect.UserPortal.Web.Controls
{
	/// <summary>
	/// Summary description for HelpLabelControl.
	/// </summary>	
	[DefaultProperty("Text"),
	ToolboxData("<{0}:HelpLabelControl runat=server></{0}:HelpLabelControl>"), ComVisible(false)]
	public class HelpLabelControl : System.Web.UI.WebControls.Label
	{
		public event EventHandler MoreHelpEvent;

		/// <summary>
		/// Read/Write property that specifies help text to be displayed by this control. 
		/// If this value is not set the control's id will be used to attempt to find a matching
		/// help string in the language resource file.
		/// </summary>
		private string text;

        [Bindable(true), 
        Category("Appearance"), 
        DefaultValue("")] 
        public override string Text 
		{
			get
			{
				return text;
			}

			set
			{
				text = value;
			}
		}		

        private string moreHelpUrl;

        /// <summary>
        /// Read/Write property that specifies the URL to be used for the more link. If this
        /// value is not set the control's id will be used to attempt to find a matching
        /// url string in the language resource file.
        /// </summary>
        [Bindable(true), 
        Category("Appearance"), 
        DefaultValue("")] 
        public string MoreHelpUrl 
        {
            get
            {
                return moreHelpUrl;
            }

            set
            {
                moreHelpUrl = value;
            }
        }		

		private string cssMainTemplate = "helpbox";

		[Bindable(true), 
		Category("Appearance"), 
		DefaultValue("helpbox")] 
		public string CssMainTemplate
		{
			get
			{
				return cssMainTemplate;
			}
			set
			{
				cssMainTemplate = value;
			}
		}
			
		/// <summary>
		/// 
		/// </summary>
		private TDImageButton closeButton;		
		public TDImageButton CloseButton 
		{
			get
			{
				return closeButton;
			}	
		
			set
			{
				closeButton = value;
			}	
		}	
	
		private TDButton moreButton;
		/// <summary>
		/// Get/Set property to access the more button
		/// </summary>
		public TDButton MoreButton
		{
			get
			{
				return moreButton;
			}
			set
			{
				moreButton= value;
			}
		}

		/// <summary>
		/// Calls a set up method before calling the default OnInit method 
		/// </summary>
		/// <param name="e"></param>
		override protected void OnInit(EventArgs e)
		{			
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Initialises the Help Control and its image button property
		/// </summary>
		private void InitializeComponent()
		{   
			//initialise the close button			
			this.CloseButton = new TDImageButton();

			// Ensure close button has unique id
			this.CloseButton.ID = this.ID+"CloseHelpImgButton";

			this.CloseButton.Visible = true;			
			//turn off auto validation, which the base class image button has associated
			//with its click event
			this.CloseButton.CausesValidation = false;
				
			//set the image and alternate text for this control
			this.CloseButton.ImageUrl = Global.tdResourceManager.GetString("HelpLabelControl.CloseHelp.ImageUrl", TDCultureInfo.CurrentUICulture);

			// Add it
			this.Controls.Add(CloseButton);

			//adds the click event handler
			this.CloseButton.Click += new System.Web.UI.ImageClickEventHandler(this.CloseButton_Click);

			//initialise the More button			
			this.MoreButton = new TDButton();

			// Ensure close button has unique id
			this.MoreButton.ID = this.ID+"MoreHelpImgButton";

			this.MoreButton.Visible = true;			
			//turn off auto validation, which the base class image button has associated
			//with its click event
			this.MoreButton.CausesValidation = false;
				
			// Add it
			this.Controls.Add(MoreButton);

			//adds the click event handler
			this.MoreButton.Click += new EventHandler(MoreButton_Click);

		}    
        
		/// <summary>
		/// closes the help label
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void CloseButton_Click(object sender,System.Web.UI.ImageClickEventArgs e)
		{
			this.Visible = false;
		}

		void MoreButton_Click(object sender, EventArgs e)
		{
            string url;
            if (moreHelpUrl == null) 
            {
                url = Global.tdResourceManager.GetString(this.ID + ".moreURL",TDCultureInfo.CurrentUICulture );
            } 
            else 
            {
                url = moreHelpUrl;
            }

            InputPageState pageState = TDSessionManager.Current.InputPageState;
			TDPage page = this.Page as TDPage;
			if( page != null )
			{
				string channelName = TDPage.SessionChannelName;
				if( channelName != null )
				{
					url = TDPage.getBaseChannelURL( channelName ) + url;
				}
				if( MoreHelpEvent != null )
				{
					MoreHelpEvent( this, EventArgs.Empty );
				}
				pageState.JourneyInputReturnStack.Push(page.PageId);
				// Need to ensure all data is properly saved away before exiting the page
				// This is needed because this redirection does not use the ScreenFlow framework.
				TDSessionManager.Current.OnPreUnload();
				page.Response.Redirect(url);			
			}

			
		}

		/// <summary> 
		/// Renders the html for this control using the appropriate styles 
		/// </summary>
		/// <param name="output"> The HTML writer to write out to </param>
		protected override void Render(HtmlTextWriter output)
		{
			this.MoreButton.Text = Global.tdResourceManager.GetString("HelpLabelControl.MoreHelp.ButtonText", TDCultureInfo.CurrentUICulture); 
			this.CloseButton.AlternateText = Global.tdResourceManager.GetString("HelpLabelControl.AlternateText.Close", TDCultureInfo.CurrentUICulture);

			//temporary strings 
			string helpText = string.Empty;

			//rendering of this help label
			output.Write("<div id=\""+CssMainTemplate+"\">");			

			output.Write("<div id=\"" + this.ID + "_himg\">");
			// Add Close button here
			CloseButton.RenderControl(output);
			output.Write("</div>");			

			output.Write("<div id=\"" + this.ID + "_hhd\">");
			output.Write(Global.tdResourceManager.GetString("HelpLabelControl.Header", TDCultureInfo.CurrentUICulture));
			output.Write("</div>");
			output.Write("<div id=\"" + this.ID + "_hline\"></div>");			

			//if the text has not already been set programmatically, get it from langstrings
			if (this.Text == null)
			{
				//get text for this help label if a resource of that name exists
				if (Global.tdResourceManager.GetString(this.ID,TDCultureInfo.CurrentUICulture) != null)
				{
					helpText = Global.tdResourceManager.GetString(this.ID,TDCultureInfo.CurrentUICulture );
				}
			}
			else
			{
				//use text already set in code
				helpText = this.Text;
			}

			//Add 'more help' link for this help label if a resource of that name exists or MoreHelpUrl property set
			output.Write("<div id=\"htxt\">" + helpText.ToString());

			if ( Global.tdResourceManager.GetString(this.ID + ".moreURL",TDCultureInfo.CurrentUICulture ) != null
                || moreHelpUrl != null)
			{
				MoreButton.RenderControl(output);
			}

			//add the retrieved values to the rendering of the label			 
			output.Write("</div></div>");							
					
		}

		
				
	}
}
