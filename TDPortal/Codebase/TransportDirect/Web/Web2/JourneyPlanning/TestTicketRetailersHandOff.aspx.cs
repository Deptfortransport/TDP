using System;using TransportDirect.Common.ResourceManager;
using System.Globalization;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace TransportDirect.UserPortal.Web.Templates
{
	/// <summary>
	/// Summary description for TestTicketRetailersHandOff.
	/// </summary>
    public partial class TestTicketRetailersHandOff : System.Web.UI.Page
	{
		//protected System.Web.UI.WebControls.TextBox TextBox1;
	
		private void Page_Load(object sender, System.EventArgs e)
		{
			// Decode the xml from the form variable
			string xmlText =Request.Form["RetailXML"].ToString(CultureInfo.InvariantCulture);
			string decodedXmlText = HttpUtility.HtmlDecode(xmlText);

			// Write xml to page
			TextBox1.Text = decodedXmlText;
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
			this.Load += new System.EventHandler(this.Page_Load);

		}
		#endregion
	}
}
