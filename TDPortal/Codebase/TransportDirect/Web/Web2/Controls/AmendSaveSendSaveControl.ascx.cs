// *********************************************** 
// NAME                 : AmendSaveSendSaveControl.ascx.cs 
// AUTHOR               : Kenny Cheung (Modified by Esther Severn & Halim Ahad)
// DATE CREATED         : 20/08/2003 
// DESCRIPTION			: Save pane for the
// AmendSaveSendControl
// ************************************************
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/AmendSaveSendSaveControl.ascx.cs-arc  $
//
//   Rev 1.3   Oct 26 2010 14:59:36   rbroddle
//Removed explicit wire up to Page_PreRender as AutoEventWireUp=true for this control so it was firing twice.
//Resolution for 5621: USD8048975 - page_init and other events wired up more than once on some pages and controls
//
//   Rev 1.2   Mar 31 2008 13:19:18   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:12:12   mturner
//Initial revision.
//
//   Rev 1.24   Feb 23 2006 19:16:20   build
//Automatically merged from branch for stream3129
//
//   Rev 1.23.1.1   Jan 30 2006 14:41:00   mdambrine
//moved TDCultureInfo to the common project
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.23.1.0   Jan 10 2006 15:23:28   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.23   Nov 03 2005 17:04:34   kjosling
//Automatically merged from branch for stream2816
//
//   Rev 1.22.1.0   Oct 20 2005 11:23:18   RGriffith
//TD089 ES020 Image Button Replacement
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages
//
//   Rev 1.22   Mar 18 2005 16:36:50   COwczarek
//Optimisation to not do any work if control is not visible.
//Viewstate disabled for labels and image button urls
//(now initialised for each page request if control visible).
//Resolution for 1921: DEV Code Review : FAFticketselectionCodeReview
//
//   Rev 1.21   Apr 28 2004 16:20:00   cshillan
//Replace Commerce Server with TDProfile and TDUserInfo DB
//
//   Rev 1.20   Apr 06 2004 17:08:12   ESevern
//amendments to instructional text
//Resolution for 732: Rework of save favourite and send to a friend instructional text
//
//   Rev 1.19   Jan 23 2004 16:03:06   PNorell
//Favourite journey updates.
//
//   Rev 1.18   Nov 25 2003 09:44:26   passuied
//completed character limitation for user registration controls (#138)
//Fixed some coding mistakes
//Resolution for 138: User Registration - Character Limitation
//
//   Rev 1.17   Nov 14 2003 10:46:36   hahad
//MAX number of favourites are read from Properties Service
//
//   Rev 1.16   Nov 06 2003 10:36:58   hahad
//Code Review fixes
//
//   Rev 1.15   Oct 31 2003 10:09:56   hahad
//FXcop fixes
//
//   Rev 1.14   Oct 29 2003 17:47:06   esevern
//corrected rename journey label lang strings call
//
//   Rev 1.13   Oct 29 2003 14:44:26   hahad
//HTML Tweaked and control text is displayed fron OnPreRender event
//
//   Rev 1.12   Oct 27 2003 18:14:28   passuied
//fixed html
//
//   Rev 1.11   Oct 27 2003 10:39:20   hahad
//Input Validation added
//
//   Rev 1.10   Oct 23 2003 14:05:46   esevern
//setting of journey name text moved from (true == true)
//
//   Rev 1.9   Oct 22 2003 13:39:24   esevern
//added check for next available journey slot
//
//   Rev 1.8   Oct 21 2003 10:25:32   hahad
//Removed Help icons and text as they now appear on the master control
//
//   Rev 1.7   Oct 20 2003 13:37:50   esevern
//added imagebutton property
//
//   Rev 1.6   Oct 08 2003 20:42:28   RPhilpott
//Remove duplicate ImageButton declaration.
//
//   Rev 1.5   Oct 08 2003 16:23:14   hahad
//Formatted HTML within Control
//
//   Rev 1.4   Sep 25 2003 18:58:22   kcheung
//Initial styling applied
//
//   Rev 1.3   Aug 20 2003 17:17:52   kcheung
//Added header.

namespace TransportDirect.UserPortal.Web.Controls
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using TransportDirect.Web.Support;
	using TransportDirect.UserPortal.JourneyControl;
	using TransportDirect.UserPortal.SessionManager;
	using TransportDirect.Common.PropertyService.Properties;
	using TransportDirect.Common.ResourceManager;
	using TransportDirect.Common;

	/// <summary>
	/// Thw AmendSaveSendSaveControl contains the template for saving a journey once a user as registered/logged on
	/// </summary>
	public partial  class AmendSaveSendSaveControl : TDUserControl
	{


		/// <summary>
		/// Read only property, returning the 'Ok' image button
		/// </summary>
		/// <returns>ImageButton</returns>
		public TDButton OKButton
		{
			get
			{
				return buttonOK;
			}
		}

		/// <summary>
		/// Read only property, returning the renamed text box
		/// </summary>
		public TextBox NameTextBox 
		{
			get 
			{
				return renameTextBox;
			}
		}

		/// <summary>
		/// Read only property, returning the renameTextBox text
		/// </summary>
		public string JourneyName 
		{
			get 
			{
				string name = HttpUtility.HtmlEncode(renameTextBox.Text.Trim());
				if (name.Length > 50) // size of field in the database
				{
					name = name.Substring(0,50); // truncate the Name to fit in database
				}
				return name;
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
		/// Prerender event handler. Intialises controls with resource manager strings.
		/// </summary>
		/// <param name="e"></param>
        protected void Page_PreRender(object sender, System.EventArgs e)
		{            
            TDUser user = TDSessionManager.Current.CurrentUser;
			if(user != null && this.Visible) 
			{
				// Initialise all labels and buttons from Resource manager

				int journeyNumber = user.NextFavouriteJourneyIndex() + 1;

				labelSaveFavouriteJourneyLabel.Text = Global.tdResourceManager.GetString
					("AmendSaveSendSaveControl.labelSaveFavouriteJourneyLabel", TDCultureInfo.CurrentUICulture); 
						
				labelCurrentName.Text = "\"" + Global.tdResourceManager.GetString("AmendSaveSendSaveControl.labelCurrentName", 
					TDCultureInfo.CurrentUICulture) + " " + journeyNumber.ToString()+ "\"";

				labelCurrentNameLabel.Text = Global.tdResourceManager.GetString
					("AmendSaveSendSaveControl.labelCurrentNameLabel", TDCultureInfo.CurrentUICulture);

				labelRenameJourneyLabel.Text = Global.tdResourceManager.GetString
					("AmendSaveSendSaveControl.labelRenameJourneyLabel", TDCultureInfo.CurrentUICulture);

				buttonOK.Text = GetResource("AmendSaveSendSaveControl.buttonOK.Text");

				labelSaveNote.Text = Global.tdResourceManager.GetString
					("AmendSaveSendSaveControl.labelSaveNote", TDCultureInfo.CurrentUICulture);
				
				labelRenameInstruction.Text = Global.tdResourceManager.GetString
					("AmendSaveSendSaveControl.labelRenameInstruction", TDCultureInfo.CurrentUICulture);
				
			}

		}
		
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
		}

		#endregion


	}
}
