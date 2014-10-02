// *********************************************** 
// NAME                 : AvoidSelectControl.ascx
// AUTHOR               : Patrick ASSUIED
// DATE CREATED         : 22/09/2003 
// DESCRIPTION  : Selection of roads to avoid control
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/AvoidSelectControl.ascx.cs-arc  $ 
//
//   Rev 1.2   Mar 31 2008 13:19:24   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:12:20   mturner
//Initial revision.
//
//   Rev 1.15   Feb 23 2006 19:16:22   build
//Automatically merged from branch for stream3129
//
//   Rev 1.14.1.1   Jan 30 2006 14:41:00   mdambrine
//moved TDCultureInfo to the common project
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.14.1.0   Jan 10 2006 15:23:34   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.14   Aug 02 2004 15:37:22   passuied
//working verson of FindCarInput page + changes to some adapters, controls...
//
//   Rev 1.13   Jul 27 2004 10:17:44   esevern
//added setting of label text on page load
//
//   Rev 1.12   Mar 10 2004 14:55:58   AWindley
//DEL5.2 Added label text for Screen Reader
//
//   Rev 1.11   Nov 18 2003 10:44:02   passuied
//changes to hopefully pass code review
//
//   Rev 1.10   Nov 13 2003 17:26:00   passuied
//minor changes
//
//   Rev 1.9   Nov 04 2003 15:10:04   passuied
//Changes : Whenever a travel detail changes on Input page, display Travel details panel on Ambiguity page
//Whenever a route options changes (but not empty) on Input page, display route options panel on ambiguity page
//
//   Rev 1.8   Sep 29 2003 16:14:18   passuied
//updated
//
//   Rev 1.7   Sep 26 2003 07:43:18   passuied
//working version
//
//   Rev 1.6   Sep 26 2003 07:37:18   passuied
//removed map button
//
//   Rev 1.5   Sep 25 2003 18:36:22   passuied
//Added missing headers


namespace TransportDirect.UserPortal.Web.Controls
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using TransportDirect.UserPortal.Web;
	using TransportDirect.Web.Support;
	using TransportDirect.Common.ResourceManager;
	using TransportDirect.Common;

	/// <summary>
	///		Selection of roads to avoid control
	/// </summary>
	public partial  class AvoidSelectControl : TDUserControl
	{
		#region declaration

		public event EventHandler RoadEntered;
		public event EventHandler RoadChanged;
		#endregion

		protected void Page_Load(object sender, System.EventArgs e)
		{
			// labelEg
			labelEg.Text =  Global.tdResourceManager.GetString("AvoidSelectControl.labelEg", TDCultureInfo.CurrentUICulture);
			labelAvoidTitle.Text = Global.tdResourceManager.GetString("avoidSelect.labelAvoidTitle", TDCultureInfo.CurrentUICulture);
		}

		/// <summary>
		/// Gets/Sets the current value of the road to avoid
		/// </summary>
		public string Current	
		{
			get
			{
				return HttpUtility.HtmlDecode(textAvoid.Text);
			}

			set
			{
				textAvoid.Text = HttpUtility.HtmlEncode(value);
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
            this.textAvoid.TextChanged += new EventHandler(this.OnTextChanged);
		}
		#endregion

		/// <summary>
		/// Handler For the Textbox TextChanged event
		/// </summary>
		/// <param name="sender">sender</param>
		/// <param name="e">event args</param>
		protected void OnTextChanged(object sender, System.EventArgs e)
		{
			// if text not empty, ==> user entered sth : Trigger event
			if (RoadEntered!= null && Current.Length != 0)
				RoadEntered(this, new EventArgs());

			if (RoadChanged != null)
				RoadChanged(this, EventArgs.Empty);
		}
	}
}
