// *************************************************************** 
// NAME                 : HelpCustomControl.aspx.cs 
// AUTHOR               : Joe Morrissey 
// DATE CREATED         : 22/08/2003 
// DESCRIPTION			: A custom image button control. When it is clicked,
// it displays its associated help text label 
// **************************************************************** 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/HelpCustomControl.cs-arc  $
//
//   Rev 1.2   Mar 31 2008 13:21:00   mturner
//Drop3 from Dev Factory
//
//  Rev DevFactory Mar 02 2008 15:18:00 apatel
//  Added write only property HelpLabelControl to set internalLabel manually
//
//   Rev 1.0   Nov 08 2007 13:14:50   mturner
//Initial revision.
//
//   Rev 1.27   Feb 23 2006 19:16:24   build
//Automatically merged from branch for stream3129
//
//   Rev 1.26.1.1   Jan 30 2006 14:41:08   mdambrine
//moved TDCultureInfo to the common project
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.26.1.0   Jan 10 2006 15:25:20   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.26   Nov 03 2005 17:08:10   kjosling
//Automatically merged from branch for stream2816
//
//   Rev 1.25.1.0   Oct 12 2005 11:01:18   rgreenwood
//TD089 ES020 Image Button Replacement
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages
//
//   Rev 1.25   Mar 21 2005 15:49:22   jgeorge
//Updated with FxCop recommendations
//
//   Rev 1.24   Mar 10 2005 13:05:04   jgeorge
//Removed ScrollToHelp functionality
//
//   Rev 1.23   Sep 06 2004 21:09:22   JHaydock
//Major update to travel news
//
//   Rev 1.22   Aug 31 2004 14:54:48   jmorrissey
//Updated the Page Load so that the alternate text is not overwritten if it has already been set
//
//   Rev 1.21   Jul 29 2004 11:15:28   passuied
//Added a static method in TDPage to close all calendar and help windows. Replaced local use.
//
//   Rev 1.20   Jul 19 2004 09:04:14   COwczarek
//Save client ID of control  in session state rather than ID to ensure uniqueness - causes problems if ScrollToHelp property is true and control with same id used more than once on page. Also remove redundant code. 
//Resolution for 1202: Implement FindTrainInput page
//
//   Rev 1.19   May 18 2004 13:31:22   jbroome
//IR864 Retaining values when Help is displayed.
//
//   Rev 1.18   Apr 01 2004 10:37:10   CHosegood
//Help button now opens the correct help label
//Resolution for 674: DEL 5.2 Map QA changes
//
//   Rev 1.17   Mar 15 2004 19:40:34   pnorell
//Simplest fix for the help-control and finding controls in a page.
//
//   Rev 1.16   Mar 10 2004 17:27:44   COwczarek
//Locate help label control by searching control containment hierarchy starting at the top of the page rather than the control in which the help custom control is contained in. This is because the help custom control does not necessarily reside on the page itself.
//Resolution for 649: Changes to the way ambiguous locations are resolved
//
//   Rev 1.15   Dec 02 2003 14:05:24   asinclair
//Added code to the Page_Load event to set the ALT tags depending on language
//
//   Rev 1.14   Nov 27 2003 20:50:22   asinclair
//Added fix to IR91
//
//   Rev 1.13   Nov 15 2003 15:37:16   PNorell
//Added support (again) for scrolling to the correct position.
//
//   Rev 1.12   Nov 05 2003 16:40:34   passuied
//restored to the good working version
//
//   Rev 1.9   Sep 30 2003 15:22:58   PNorell
//Added support for ensuring only one "window" open on a web page at the same time.
//Fixed numerous click bug in the Help control.
//Fixed numerous language issues with the help control.
//Updated the journey planner input pages to contain the updated code for ensuring one window.
//Updated the wait page and took out the debug logging.
//
//   Rev 1.8   Sep 25 2003 12:37:44   JMorrissey
//Click event now correctly closes all other visible help labels and calendar controls
//
//   Rev 1.7   Sep 18 2003 17:47:16   JMorrissey
//added new image
//
//   Rev 1.6   Sep 17 2003 10:12:08   JMorrissey
//Now uses correct image from the BBC
//
//   Rev 1.5   Sep 04 2003 17:41:28   JMorrissey
//Updated TypeConverter attribute of HelpLabel to use new HelpLabelControlConverter
//
//   Rev 1.4   Sep 02 2003 16:26:20   JMorrissey
//Added TypeConverter attribute to HelpLabel property
//
//   Rev 1.3   Sep 01 2003 12:39:10   JMorrissey
//Updated attributes for HelpLabel property
//
//   Rev 1.2   Aug 26 2003 12:19:12   JMorrissey
//Updated image file
//
//   Rev 1.1   Aug 22 2003 16:52:28   JMorrissey
//Added click event handler and custom property

using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.Design;
using System.Web.UI.WebControls;
using System.Runtime.InteropServices;

using TransportDirect.UserPortal.Resource;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.Web.Support;
using TransportDirect.Common.ResourceManager;
using TransportDirect.Common;

//namespace for TD web controls
namespace TransportDirect.UserPortal.Web.Controls
{
	/// <summary>
	/// Class inherits from ImageButton so that all the image 
	/// button functionality is available, but adds a new property
	/// called HelpLabel, used to link the image button to a particular label
	/// </summary>			 
	[ToolboxData("<{0}:HelpCustomControl runat=server></{0}:HelpCustomControl>"), ComVisible(false)]	
	public class HelpCustomControl : TDButton, ISingleWindow
	{
		//private field for the linked help label
		//private string associatedHelpLabel;	
		
		#region Public and private properties
		/// <summary>
		///public property for the associated help label		
		/// </summary>		
		[Category("Misc"), Description("ID of the control to display when help image is clicked"),
		DefaultValue(""),TypeConverter(typeof(HelpLabelControlConverter))]
		public string HelpLabel
		{
			get
			{
				//use ViewState to get the name of the linked label
				object o = ViewState["HelpLabel"];
				return((o==null) ? String.Empty : (string)o);
			}
			set
			{
				//assign the name of the linked label to the ViewState of this property
				ViewState["HelpLabel"] = value;
			}
		}

        /// <summary>
        /// Traverse the tree upwards until the control is found
        /// Returns null if the control could not be found
        /// </summary>
        /// <param name="control"></param>
        /// <returns></returns>
        private Control recurseUpwards( Control control ) 
        {
            //Attempt to find the control
            Control ctrl = control.FindControl( HelpLabel.ToString() );

            //If the control is found then return it
            if ( (ctrl != null )
                && (ctrl.ID != null)
                && (ctrl.ID.Equals( HelpLabel.ToString() ))) 
            {
                return ctrl;
            }
            else 
            {
                //The control has not been found so if the parent
                //exists search the controls contained within
                //it otherwise return null
                if ( control.Parent == null )
                    return null;
                return recurseUpwards( control.Parent );
            }
        }

		private Control internalLabel;
		private Control InternalLabel 
		{
			get
			{
				if( internalLabel == null )
				{
					internalLabel = FindControl(HelpLabel.ToString());
					// If we could not find the control in the containing control - broaden search
					// The page-wide search does not always find all controls - unknown reason
					if ( internalLabel == null )
						internalLabel  = Page.FindControl(HelpLabel.ToString());

                    //We could not find the HelpLabel so traverse the tree
                    //upwards searcing for it
                    if ( internalLabel == null )
                        internalLabel  = recurseUpwards( this.Parent );
				}
				return internalLabel;
			}
           
		}

        /// <summary>
        /// Sets internal Label control
        /// </summary>
        public Control HelpLabelControl
        {
            set
            {
                internalLabel = value;
            }
        }
		#endregion

		/// <summary>
		/// Event handler for the control's click event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void this_Click(object sender,EventArgs e)
		{
			TDPage.CloseAllSingleWindows(this.Page);
			if( IsOpen )
			{
				Close();
			}
			else 
			{
				Open();
			}
		}

		/// <summary>
		/// page load event 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Page_Load(object sender, System.EventArgs e)
		{

			//sets the alternate text for the help button if it has not already been set
			if (this.AlternateText.Length == 0)
			{
				string controlID = this.ID;
				string reference = Global.tdResourceManager.GetString(controlID+".AlternateText", TDCultureInfo.CurrentUICulture);
	
				this.AlternateText = reference;
			}
		}

		#region ISingleWindow interface methods
		public void Close()
		{
			//find the associated label
			if (InternalLabel != null)
			{
				InternalLabel.Visible = false;
			}
			//This is no longer needed as we want the alt text to remain as that set in Langstrings
			//this.AlternateText = Global.tdResourceManager.GetString("HelpControl.AlternateText.Closed", TDCultureInfo.CurrentUICulture);
		}

		public void Open()
		{
			if (InternalLabel != null)
			{
				InternalLabel.Visible = true;
			}
			//This is no longer needed as we want the alt text to remain as that set in Langstrings
			//this.AlternateText = Global.tdResourceManager.GetString("HelpControl.AlternateText.Opened", TDCultureInfo.CurrentUICulture);
		}

		public bool IsOpen
		{
			get
			{
				if (InternalLabel != null)
				{
					return InternalLabel.Visible;
				}
				return false;
			}
		}

		#endregion

		

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
		/// Initialises the Help Control 
		/// </summary>
		private void InitializeComponent()
		{   
			//adds the click event handler
			this.Click += new EventHandler(this_Click);

			this.Load += new System.EventHandler(this.Page_Load);

			//turn off auto validation, which the base class image button has associated
			//with its click event
			this.CausesValidation = false;

			//set the image and alternate text for this control
			this.Text = Global.tdResourceManager.GetString("HelpControl.Text", TDCultureInfo.CurrentUICulture);					
		}
		
		public string ImageUrl
		{
			get{ return string.Empty; }
			set{}
		}

		public string AlternateText
		{
			get{ return string.Empty; }
			set{ ToolTip = value; }
		}
	}
}