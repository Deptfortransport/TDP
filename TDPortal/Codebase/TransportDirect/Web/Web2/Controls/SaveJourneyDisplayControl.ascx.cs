// *********************************************** 
// NAME                 : SaveJourneyDisplayControl.cs 
// AUTHOR               : Esther Severn
// DATE CREATED         : 03/10/2003 
// DESCRIPTION          : Displays the name of the 
//						  currently selected favourite 
//						  journey
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/SaveJourneyDisplayControl.ascx.cs-arc  $ 
//
//   Rev 1.2   Mar 31 2008 13:22:56   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:17:40   mturner
//Initial revision.
//
//   Rev 1.6   Feb 23 2006 19:17:06   build
//Automatically merged from branch for stream3129
//
//   Rev 1.5.1.1   Jan 30 2006 14:41:22   mdambrine
//moved TDCultureInfo to the common project
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.5.1.0   Jan 10 2006 15:27:12   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.5   Jan 23 2004 16:03:22   PNorell
//Favourite journey updates.
//
//   Rev 1.4   Jan 21 2004 11:32:36   PNorell
//Update to 5.2
//
//   Rev 1.3   Oct 23 2003 16:15:34   passuied
//display good label depending on if on input page or ambiguous
//
//   Rev 1.2   Oct 08 2003 14:15:16   esevern
//added check for isPostBack on Page_Load
//
//   Rev 1.1   Oct 08 2003 13:26:28   esevern
//added html, setting of text with lang. res. man

namespace TransportDirect.UserPortal.Web.Controls
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using TransportDirect.Web.Support;
	using TransportDirect.Web;
	using TransportDirect.Common.ResourceManager;
	using TransportDirect.Common;

	/// <summary>
	///		TDUserControl responsible for the display of the current
	///		journey name. This control will only be visible if the 
	///		user is logged in as only authenticated user's are able
	///		to save/load favourite journeys.
	/// </summary>
	public partial  class SaveJourneyDisplayControl : TDUserControl 
	{

		private bool isAmbiguous = false;
		/// <summary>
		/// Sets all instruction/label/error message text in the 
		/// current language
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// set label using language resource manager : If ambiguous Page set title to ambiguous message, else input message
			labelJourneyNameTitle.Text = isAmbiguous?
				Global.tdResourceManager.GetString("FavouriteSaveDisplayControl.JourneyNameTitle", TDCultureInfo.CurrentUICulture):
				Global.tdResourceManager.GetString("SaveJourneySelectControl.labelJourneyNameTitle", TDCultureInfo.CurrentUICulture);
		}


		/// <summary>
		/// Write only property for favourite journey name
		/// </summary>
		public string JourneyName 
		{
			set 
			{
				labelJourneyName.Text = value;
			}
		}

		public string JourneyNameTitle
		{
			set
			{
				labelJourneyNameTitle.Text = value;
			}
		}

		public bool IsAmbiguousPage
		{
			set
			{
				isAmbiguous = value;
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
