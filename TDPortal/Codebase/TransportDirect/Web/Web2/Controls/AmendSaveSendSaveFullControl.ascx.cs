// *********************************************** 
// NAME                 : AmendSaveSendSaveFullControl.ascx.cs 
// AUTHOR               : Kenny Cheung (Modified by Esther Severn & Halim Ahad)
// DATE CREATED         : 20/08/2003 
// DESCRIPTION			: Save full pane for the
// AmendSaveSendControl
// ************************************************
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/AmendSaveSendSaveFullControl.ascx.cs-arc  $
//
//   Rev 1.4   Oct 26 2010 15:28:44   rbroddle
//Removed explicit wire up to Page_PreRender as AutoEventWireUp=true for this control so it was firing twice.
//Resolution for 5621: USD8048975 - page_init and other events wired up more than once on some pages and controls
//
//   Rev 1.3   Jan 13 2010 16:52:38   apatel
//Updated LoadDropList method to retain the selected index of dropdown.
//Resolution for 5370: Problem with saving favourite journey
//
//   Rev 1.2   Mar 31 2008 13:19:20   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:12:14   mturner
//Initial revision.
//
//   Rev 1.19   Feb 23 2006 19:16:20   build
//Automatically merged from branch for stream3129
//
//   Rev 1.18.1.0   Jan 10 2006 15:23:28   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.18   Nov 03 2005 17:05:54   kjosling
//Automatically merged from branch for stream2816
//
//   Rev 1.17.1.0   Oct 20 2005 11:23:20   RGriffith
//TD089 ES020 Image Button Replacement
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages
//
//   Rev 1.17   Mar 18 2005 16:36:52   COwczarek
//Optimisation to not do any work if control is not visible.
//Viewstate disabled for labels and image button urls
//(now initialised for each page request if control visible).
//Resolution for 1921: DEV Code Review : FAFticketselectionCodeReview
//
//   Rev 1.16   Jan 23 2004 16:03:10   PNorell
//Favourite journey updates.
//
//   Rev 1.15   Nov 25 2003 10:09:10   passuied
//fix for character limitation #138
//Resolution for 138: User Registration - Character Limitation
//
//   Rev 1.14   Nov 25 2003 09:44:30   passuied
//completed character limitation for user registration controls (#138)
//Fixed some coding mistakes
//Resolution for 138: User Registration - Character Limitation
//
//   Rev 1.13   Nov 18 2003 15:25:44   passuied
//made it work for when max is reached
//
//   Rev 1.12   Nov 18 2003 13:18:08   hahad
//Gets MAX number of journeys allowed from properties database
//
//   Rev 1.11   Nov 06 2003 10:36:30   hahad
//Code Review fixes
//
//   Rev 1.10   Nov 05 2003 11:50:32   hahad
//Updated comments and changed labels
//
//   Rev 1.9   Oct 31 2003 10:36:54   hahad
//FX Cop fixes
//
//   Rev 1.8   Oct 29 2003 10:55:30   hahad
//OK Button now appers properly
//
//   Rev 1.7   Oct 24 2003 10:45:28   esevern
//added call to loaddroplisy() on page_load
//
//   Rev 1.6   Oct 22 2003 20:22:12   esevern
//removed defunct selected index changed event, added property to get favourites drop list
//
//   Rev 1.5   Oct 20 2003 13:38:48   esevern
//added favourite journey drop list population
//
//   Rev 1.4   Oct 13 2003 11:06:36   hahad
//Added HTML Template
//
//   Rev 1.3   Aug 20 2003 17:21:02   kcheung
//Updated header.
//
//   Rev 1.2   Aug 20 2003 17:18:48   kcheung
//Added header.

namespace TransportDirect.UserPortal.Web.Controls
{
	using System;using TransportDirect.Common.ResourceManager;
	using System.Collections;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using TransportDirect.Web.Support;
	using TransportDirect.Common.Logging;
	using Logger = System.Diagnostics.Trace;
	using TransportDirect.ReportDataProvider.TDPCustomEvents;
	using TransportDirect.UserPortal.Web;
	using TransportDirect.UserPortal.SessionManager;
	using TransportDirect.Common;
	using TransportDirect.Common.PropertyService.Properties;

	/// <summary>
	///	Class that displays the label when all Journey save locations are full, requiring user to
	///	select an old journey to overwrite
	/// </summary>
	public partial  class AmendSaveSendSaveFullControl : TDUserControl
	{
		#region Web/User controls
		

		protected string userID = string.Empty;
		protected string msg = string.Empty;
		
		#endregion

		/// <summary>
		/// Prerender event handler. Intialises controls with resource manager strings.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_PreRender(object sender, System.EventArgs e)
		{
            if (this.Visible)
            {
                labelJourneyListFullMsg.Text = Global.tdResourceManager.GetString
                    ("AmendSaveSendSaveFullControl.labelJourneyListFullMsg", TDCultureInfo.CurrentUICulture);

                labelReplaceJourneyLabel.Text = Global.tdResourceManager.GetString
                    ("AmendSaveSendSaveFullControl.labelReplaceJourneyLabel", TDCultureInfo.CurrentUICulture);

                labelWithNewJourneyLabel.Text = Global.tdResourceManager.GetString
                    ("AmendSaveSendSaveFullControl.labelWithNewJourneyLabel", TDCultureInfo.CurrentUICulture);
		
                btnOK.Text = GetResource("AmendSaveSendSaveFullControl.btnOK.Text");

                labelJourneyPlanSaveNote.Text = Global.tdResourceManager.GetString
                    ("AmendSaveSendSaveFullControl.labelJourneyPlanSaveNote", TDCultureInfo.CurrentUICulture);

                labelJourneyNotReplace.Text = Global.tdResourceManager.GetString
                    ("AmendSaveSendSaveFullControl.labelJourneyNotReplace", TDCultureInfo.CurrentUICulture);
            }
		}

		/// <summary>
		/// Read only property, returning the 'Ok' image button
		/// </summary>
		/// <returns>ImageButton</returns>
		public TDButton OKButton
		{
			get
			{
				return btnOK;
			}
		}

		
		/// <summary>
		/// Populates a drop list of the users favourite journeys
		/// </summary>
		public void LoadDropList() 
		{
			try 
			{
				TDUser user = TDSessionManager.Current.CurrentUser;
                int selectedJourneyIndex = ddlJourney.SelectedIndex;
				ddlJourney.Items.Clear();
				ArrayList al = user.FindRegisteredFavourites();
				foreach( FavouriteJourney fj in al )
				{
					ddlJourney.Items.Add( new ListItem(fj.DisplayName, fj.Guid ) );
				}

                ddlJourney.SelectedIndex = selectedJourneyIndex;
			}
			catch(Exception exp)
			{
				//log and rethrow unexpected exception 
				string msg = "Unexpected error occurred attempting to retrieve favourite journey";

				OperationalEvent operationalEvent = new OperationalEvent
					(TDEventCategory.Business, TDTraceLevel.Error, msg);

				throw new TDException(msg, exp, true, TDExceptionIdentifier.BTCRetrievalOfProfileFailed); 
			}
		}

		/// <summary>
		/// Read/Write property for new saved journey name
		/// </summary>
		/// <returns>string, user entered journey name</returns>
		public string FavouriteName 		
		{
			get 
			{
				string name = HttpUtility.HtmlEncode(txtNewJourneyName.Text.Trim());
				if (name.Length > 50) // size of field in the database
				{
					name = name.Substring(0,50); // truncate the Name to fit in database
				}
				return name;
			}
			set 
			{
				txtNewJourneyName.Text = HttpUtility.HtmlEncode(value);
			}
		}

		/// <summary>
		/// Read only property, returning Favourite Journeys DropDownList
		/// </summary>
		public DropDownList JourneyDropList 		
		{
			get 
			{
				return ddlJourney;
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
		
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
		}
		#endregion
	}
}
