using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using TransportDirect.UserPortal.SessionManager;
using TransportDirect.Common;
using TransportDirect.Common.ResourceManager;

namespace TransportDirect.UserPortal.Web.Templates
{
	/// <summary>
	/// Summary description for JourneyPlanning.
	/// </summary>
	public partial class iframeJourneyPlanning : System.Web.UI.Page
	{
		protected TDResourceManager resourceManager = Global.tdResourceManager;
	

		protected void Page_Load(object sender, System.EventArgs e)
		{
			labelPlanAJourney.Text = resourceManager.GetString("HomeDefault.labelPlanAJourney.Text", TDCultureInfo.CurrentUICulture);
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
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    

		}
		#endregion
	}
}
