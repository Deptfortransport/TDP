// *********************************************** 
// NAME                 : FavouriteLoadOptionsControl.aspx.cs 
// AUTHOR               : Esther Severn
// DATE CREATED         : 09/10/2003 
// DESCRIPTION          : TDUserControl class to handle
//						  visibility of favourite journey
//						  selection controls
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/FavouriteLoadOptionsControl.ascx.cs-arc  $ 
//
//   Rev 1.3   Apr 09 2008 13:12:44   scraddock
//Steve B: Fixed problem with not being able to load existing journeys.
//Resolution for 4849: Saved Favorite Journey details not retrieved.
//
// Rev DevFactory Apr 9 2008 sbarker
// Solved bug with save journeys not loading correctly
//
//   Rev 1.2   Mar 31 2008 13:20:08   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:13:28   mturner
//Initial revision.
//
//   Rev 1.11   Feb 23 2006 19:16:30   build
//Automatically merged from branch for stream3129
//
//   Rev 1.10.1.0   Jan 10 2006 15:24:12   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.10   Sep 29 2005 12:46:42   build
//Automatically merged from branch for stream2673
//
//   Rev 1.9.1.0   Sep 13 2005 16:58:44   NMoorhouse
//DN079 UEE, TD092 Login and Register enhancements
//Resolution for 2757: DEL 8 stream: Login functionality available on every page
//
//   Rev 1.9   Mar 31 2004 16:57:56   ESevern
//removed table
//Resolution for 690: Additional DEL5.2 QA Changes Journey Planner Input
//
//   Rev 1.8   Feb 23 2004 12:28:36   esevern
//DEL5.2 - removed FavouritePreloggedInControl
//
//   Rev 1.7   Feb 13 2004 13:29:18   esevern
//DEL 5.2 seperation of login and register
//
//   Rev 1.6   Jan 21 2004 11:32:40   PNorell
//Update to 5.2
//
//   Rev 1.5   Oct 27 2003 16:13:22   passuied
//Build2 integration tidied up and optimised... still working
//
//   Rev 1.4   Oct 21 2003 15:47:14   esevern
//hide SaveJourneyDisplayControl when user logs out
//
//   Rev 1.3   Oct 17 2003 10:29:30   esevern
//added find control to login/out display 
//
//   Rev 1.2   Oct 15 2003 16:21:14   esevern
//added logged in/out display methods
//
//   Rev 1.1   Oct 13 2003 12:09:50   esevern
//added favourite journey pre/logged in and login controls
//
//   Rev 1.0   Oct 09 2003 14:42:20   esevern
//Initial Revision

namespace TransportDirect.UserPortal.Web.Controls
{
	using System;using TransportDirect.Common.ResourceManager;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	using TransportDirect.UserPortal.SessionManager;

	/// <summary>
	///	Handles the visibility of favourite journey login related controls:
	///	if the user is logged in, displays 'FavouriteLoggedInControl'
	/// </summary>
	public partial  class FavouriteLoadOptionsControl : TDUserControl
	{
		protected FavouriteLoggedInControl loggedInControl;
		protected SaveJourneyDisplayControl displayJourneyNameControl;

		/// <summary>
		/// Sets required controls visible, registers to handle login, 
		/// logout events
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
		    //Back up the selected index to be reapplied once
            //combo box text has been refreshed...
            int selectedJourneyIndex = FavouriteLoggedIn.FavouritesList.SelectedIndex;

			// register to received login request event
			loggedInControl.LoadFavourite += new FavouriteLoggedInControl.LoadFavouriteEventHandler(LoadFavourite);
			
			if( TDSessionManager.Current.Authenticated ) 
			{
				LoggedInDisplay();
			}
			else 
			{
				LoggedOutDisplay();
			}

            //Now reapply the saved journey:
            FavouriteLoggedIn.FavouritesList.SelectedIndex = selectedJourneyIndex;

			displayJourneyNameControl.Visible = false;  // no favourite loaded yet so hide name display

		}

		/// <summary>
		/// Handles the visibility of logged in/ logged out controls. 
		/// </summary>
		public void LoggedInDisplay() 
		{
			loggedInControl.LoggedInDisplay();
			loggedInControl.Visible = true; // allow user to select favourite journey
		}

		/// <summary>
		/// Handles the visibility of logged in/ logged out controls
		/// </summary>
		public void LoggedOutDisplay() 
		{
			loggedInControl.Visible = false; // hide favourite journey selection control
			displayJourneyNameControl.Visible = false;
		}


		/// <summary>
		///  The  handler for notifications from the FavouriteLoggedInControl.LoadFavourite
		///  event (user has selected a favourite journey to display)
		/// </summary>
		/// <param name="sender">object</param>
		/// <param name="e">LoadFavouriteEventArgs</param>
		public void LoadFavourite (object sender, Controls.FavouriteLoggedInControl.LoadFavouriteEventArgs e)
		{
			// set control visibility
			loggedInControl.Visible = true;
			
			// set journey name in display
			FavouriteJourney favouriteJourney = e.FavouriteJourney;
			displayJourneyNameControl.JourneyName = favouriteJourney.Name;
			displayJourneyNameControl.Visible = true;
		}

		// properties

		public FavouriteLoggedInControl FavouriteLoggedIn 
		{
			get 
			{
				return loggedInControl;
			}
			
		}

		public SaveJourneyDisplayControl SaveDisplay 
		{
			get 
			{
				return displayJourneyNameControl;
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


		//
		/// <summary>
		/// Event handler for register confirm button click.
		/// </summary>
		private void RegisterConfirm_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			// set the login control visible
			loggedInControl.Visible = true;
			loggedInControl.LoggedInDisplay();
			displayJourneyNameControl.Visible = false;
		}
	}
}
