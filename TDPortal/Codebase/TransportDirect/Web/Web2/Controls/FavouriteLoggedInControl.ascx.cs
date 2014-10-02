// *********************************************** 
// NAME                 : FavouriteLoggedInControl.ascx.cs 
// AUTHOR               : Esther Severn
// DATE CREATED         : 30/09/2003 
// DESCRIPTION          : FavouriteJourney user control  
//                        displayed when user logged in.
//						  Lists the user's saved
//						  favourite journeys
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/FavouriteLoggedInControl.ascx.cs-arc  $ 
//
//   Rev 1.3   Apr 04 2008 09:50:54   apatel
//added favourites label text
//
//   Rev 1.2   Mar 31 2008 13:20:08   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:13:30   mturner
//Initial revision.
//
//   Rev 1.17   Feb 23 2006 19:16:32   build
//Automatically merged from branch for stream3129
//
//   Rev 1.16.1.0   Jan 10 2006 15:24:14   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.16   Nov 15 2005 11:18:40   RGriffith
//UEE Button replacement Code Review suggested changes
//
//   Rev 1.15   Nov 03 2005 17:04:06   kjosling
//Automatically merged from branch for stream2816
//
//   Rev 1.14.1.0   Oct 19 2005 16:28:06   rgreenwood
//TD089 ES020 Image Button Replacement
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages
//
//   Rev 1.14   Apr 28 2004 16:20:02   cshillan
//Replace Commerce Server with TDProfile and TDUserInfo DB
//
//   Rev 1.13   Jan 23 2004 16:27:00   PNorell
//Slight update to presentation.
//
//   Rev 1.12   Jan 21 2004 11:32:42   PNorell
//Update to 5.2
//
//   Rev 1.11   Dec 01 2003 10:24:20   kcheung
//Removed help icon.
//Resolution for 427: Additional Help box appearing that is not needed
//
//   Rev 1.10   Nov 18 2003 13:52:36   esevern
//amended help label name and made scrolltohelp false
//Resolution for 238: Missing help message in Favourite- Logged In control
//
//   Rev 1.9   Oct 27 2003 16:13:20   passuied
//Build2 integration tidied up and optimised... still working
//
//   Rev 1.8   Oct 27 2003 14:27:24   passuied
//build 2 integration in Input pages debugged
//
//   Rev 1.7   Oct 21 2003 15:37:18   esevern
//added properties returning buton(s)/droplist
//
//   Rev 1.6   Oct 17 2003 10:28:12   esevern
//corrected profile name
//
//   Rev 1.5   Oct 15 2003 16:58:00   esevern
//help label hidden on first page load
//
//   Rev 1.4   Oct 13 2003 12:13:02   esevern
//corrected commerce server property
//
//   Rev 1.3   Oct 08 2003 14:15:06   esevern
//added check for isPostBack on Page_Load
//
//   Rev 1.2   Oct 08 2003 12:24:12   esevern
//added help control
//
//   Rev 1.1   Oct 06 2003 14:14:42   esevern
//added cs profiles, html and logging of user events 


namespace TransportDirect.UserPortal.Web.Controls
{
	using System;using TransportDirect.Common.ResourceManager;
	using System.Collections;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using System.Globalization;

	using TransportDirect.Common;
	using TransportDirect.Common.Logging;
	using Logger = System.Diagnostics.Trace;
	using TransportDirect.ReportDataProvider.TDPCustomEvents;
	using TransportDirect.Web.Support;
	using TransportDirect.UserPortal.SessionManager;
	using TransportDirect.UserPortal.Resource;
	using TransportDirect.Common.PropertyService.Properties;
	

	/// <summary>
	///		TDUserControl responsible for the display and retrieval 
	///		of a logged in user's saved favourite journeys.
	/// </summary>
	public partial  class FavouriteLoggedInControl : TDUserControl
	{
		protected string userID = string.Empty; 
		protected bool selectionChanged = false;
		protected string errorMessage = string.Empty;
		private int maxNumberOfFavouriteJourneys;
		protected string msg = string.Empty; // exception string
		protected string selectedID = string.Empty;
		protected int selectedIndex = -1;  


		public FavouriteLoggedInControl()
		{
			maxNumberOfFavouriteJourneys = Convert.ToInt32(
				Properties.Current["favourites.maxnumberoffavouritejourneys"]);
		}
		/// <summary>
		/// Hides help label on first page load
		/// </summary>
		/// <param name="sender">object</param>
		/// <param name="e">System.EventArgs</param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			buttonOK.Text = GetResource("FavouriteLoggedInControl.buttonOK.Text");
            favouritesLabel.Text = GetResource("loggedInControl.favouritesLabel");
		}


		/// <summary>
		/// Handles the button click event.  Will attempt to retrieve the 
		/// user's selected favourite journey details from the user profile
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void buttonOK_Click(object sender, EventArgs e)
		{
			// if a list item was selected and it wasn't the default option ('Choose from your list')
			if (favouritesDropList.SelectedIndex > -1 && favouritesDropList.SelectedIndex != 0)  
			{
				
				// obtain the favourite journey's guid - the selected list item value
				selectedID = favouritesDropList.SelectedItem.Value;

				FavouriteJourney fj = TDSessionManager.Current.CurrentUser.FindRegisteredFavourite( selectedID );
				
				// check that a user has subscribed to the event before raising it
				if(fj != null && LoadFavourite != null) 
				{
					LoadFavourite(this, new LoadFavouriteEventArgs(fj));
				}
			}
		}


		//delegate for favourite journey requested event
		public delegate void LoadFavouriteEventHandler (object sender, LoadFavouriteEventArgs e);
		// event for favourite journey requested
		public event LoadFavouriteEventHandler LoadFavourite; 
		
		/// <summary>
		/// Class contains one property to set/return the favourite journey profile
		/// </summary>
		public class LoadFavouriteEventArgs : EventArgs 
		{      	
			private FavouriteJourney journey;

			public FavouriteJourney FavouriteJourney 
			{
				get 
				{
					return journey;
				}
				set 
				{
					journey = value;
				}
			}

			/// <summary>
			/// need to return a favourite journey for use by container page
			/// </summary>
			/// <param name="favourite"></param>
			public LoadFavouriteEventArgs (FavouriteJourney favourite) 
			{
				this.journey = favourite;
			}
		}

		/// <summary>
		/// When the control is visible (the user has logged in) this method is
		/// called to load the favourite journey dropdownlist with a collection 
		/// of the user's favourite journeys.  The user's profile is obtained by 
		/// retrieving their userID from the AuthTicket, set when they were 
		/// authenticated at login.  The name and guid of any favourite journeys
		/// associated with this profile are then added to the dropdownlist.  
		/// </summary>
		public void LoggedInDisplay() 
		{
			favouritesDropList.Items.Clear();

			// add the default list item
			string defaultListItem = Global.tdResourceManager.GetString("DataServices.favouritesDropList.Default", TDCultureInfo.CurrentUICulture);
			favouritesDropList.Items.Add(new ListItem(defaultListItem, defaultListItem));

			ArrayList al = TDSessionManager.Current.CurrentUser.FindRegisteredFavourites();
			foreach( FavouriteJourney fj in al )
			{
				favouritesDropList.Items.Add(new ListItem(fj.DisplayName, fj.Guid));
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
			this.buttonOK.Click += new EventHandler(this.buttonOK_Click);

		}
		#endregion


		/// <summary>
		/// Read only property to access Favourite journeys drop list
		/// </summary>
		public DropDownList FavouritesList 
		{
			get 
			{
				return favouritesDropList;
			}
		}

		/// <summary>
		/// Read only property to access Ok image button
		/// </summary>
		public TDButton OkButton 
		{
			get 
			{
				return buttonOK;
			}
		}
			


	}
}
