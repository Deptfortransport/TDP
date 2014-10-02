// *********************************************** 
// NAME                 : TravelDetailsControl.cs 
// AUTHOR               : Esther Severn
// DATE CREATED         : 23/09/2003 
// DESCRIPTION          : UI providing user with 
//						  number of drop down travel
//						  options which may be saved
//						  to their user profile 
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/TravelDetailsControl.ascx.cs-arc  $ 
//
//   Rev 1.2   Mar 31 2008 13:23:22   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:18:22   mturner
//Initial revision.
//
//   Rev 1.12   Feb 23 2006 19:17:12   build
//Automatically merged from branch for stream3129
//
//   Rev 1.11.1.0   Jan 10 2006 15:27:48   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.11   Apr 28 2004 16:20:10   cshillan
//Replace Commerce Server with TDProfile and TDUserInfo DB
//
//   Rev 1.10   Oct 24 2003 11:42:30   passuied
//Build 2 for ambiguity page first working version
//
//   Rev 1.9   Oct 17 2003 10:25:34   esevern
//moved setting of control visibility from page load
//
//   Rev 1.8   Oct 15 2003 14:31:22   esevern
//sets visibility of required controls on first page load
//
//   Rev 1.7   Oct 09 2003 11:14:12   esevern
//bug fix
//
//   Rev 1.6   Oct 09 2003 09:47:54   esevern
//profile code moved to journeyplannerinput page
//
//   Rev 1.5   Oct 08 2003 14:15:22   esevern
//added check for isPostBack on Page_Load
//
//   Rev 1.4   Oct 07 2003 18:01:06   esevern
//added call to logged in display
//
//   Rev 1.3   Oct 07 2003 17:58:22   esevern
//added amends to display if user logged in
//
//   Rev 1.2   Oct 07 2003 17:44:20   esevern
//label text set using lang resource manager
//
//   Rev 1.1   Sep 23 2003 17:55:48   esevern
//added drop downs and checkboxes
//
//   Rev 1.0   Sep 23 2003 17:08:54   esevern
//Initial Revision

namespace TransportDirect.UserPortal.Web.Controls
{
	using System;using TransportDirect.Common.ResourceManager;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using TransportDirect.UserPortal.Web.Events;
	using TransportDirect.Common;
	using TransportDirect.Common.Logging;
	using TransportDirect.UserPortal.SessionManager;
	using Logger = System.Diagnostics.Trace;
	using TransportDirect.Web.Support;
	using TransportDirect.Web;


	/// <summary>
	///	Responsible for the display of travel detail options,  
	///	allowing the user to elect to save preferred travel details
	/// </summary>
	public partial  class TravelDetailsControl : TDUserControl
	{

		protected TravelDetailsLoginOptionControl login;
		protected TravelDetailsSaveOptionControl save;
				
		/// <summary>
		/// If the user is autheticated, hides the login register information text and
		/// displays the 'Save details' checkbox
		/// </summary>
		/// <param name="sender">object</param>
		/// <param name="e">System.EventArgs</param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if(TDSessionManager.Current.Authenticated) 
			{
				LoggedInDisplay();
			}
			else 
			{
				LoggedOutDisplay();
			}
		}

		/// <summary>
		/// Hides login/register label, sets save checkbox visible
		/// </summary>
		public void LoggedInDisplay()
		{
			save.Visible = true;
			login.Visible = false;
		}

		/// <summary>
		/// Hides save checkbox, sets login/register label visible
		/// </summary>
		public void LoggedOutDisplay() 
		{	
			save.Visible = false;
			login.Visible = true;
		}

		public bool SaveDetails
		{
			get
			{
				return save.Visible && save.SaveDetails;
				
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
