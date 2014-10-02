namespace TransportDirect.UserPortal.Web.Controls
{
	using System;using TransportDirect.Common.ResourceManager;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	/// <summary>
	///		Summary description for ClaimEmailDetails.
	/// </summary>
	public partial  class ClaimEmailDetails : TDUserControl
	{

		public string Email
		{
			get { return textEmail.Text; }
			set { textEmail.Text = value; }
		}

		public string ConfirmEmail
		{
			get { return textConfirmEmail.Text; }
			set { textConfirmEmail.Text = value; }
		}

		public bool ValidEmail
		{
			set	{ SetFieldStatus( "Email", value ); }
		}

		public bool ValidConfirmEmail
		{
			set	{ SetFieldStatus( "ConfirmEmail", value ); }
		}

		private void SetFieldStatus( string FieldName, bool Valid )
		{
			try
			{
				System.Web.UI.WebControls.Image image = (System.Web.UI.WebControls.Image)FindControl( "imgError" + FieldName );
				System.Web.UI.WebControls.TextBox text = (System.Web.UI.WebControls.TextBox)FindControl( "text" + FieldName );
				image.Visible = !Valid;
				if( Valid )
				{
					text.BorderStyle = BorderStyle.NotSet;
					text.BorderColor = Color.Empty;
				}
				else
				{
					text.BorderStyle = BorderStyle.Solid;
					text.BorderColor = Color.Red;
				}
			}
			catch
			{
			}
		}

		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
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
