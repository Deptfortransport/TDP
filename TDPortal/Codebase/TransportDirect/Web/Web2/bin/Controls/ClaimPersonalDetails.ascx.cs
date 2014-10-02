namespace TransportDirect.UserPortal.Web.Controls
{
	using System;using TransportDirect.Common.ResourceManager;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	/// <summary>
	///		Summary description for ClaimPersonalDetails.
	/// </summary>
	public partial  class ClaimPersonalDetails : TDUserControl
	{
		protected System.Web.UI.WebControls.Label labelAddress;
		protected System.Web.UI.HtmlControls.HtmlTableCell tdErrorFirstName;
		protected System.Web.UI.WebControls.Image imgErrorAddress;

		public string FirstName
		{
			get { return textFirstName.Text; }
			set { textFirstName.Text = value; }
		}

		public string LastName
		{
			get { return textLastName.Text; }
			set { textLastName.Text = value; }
		}

		public string Address1
		{
			get { return textAddress1.Text; }
			set { textAddress1.Text = value; }
		}

		public string Address2
		{
			get { return textAddress2.Text; }
			set { textAddress2.Text = value; }
		}

		public string Address3
		{
			get { return textAddress3.Text; }
			set { textAddress3.Text = value; }
		}

		public string PostCode
		{
			get { return textPostCode.Text; }
			set { textPostCode.Text = value; }
		}

		public bool ValidFirstName
		{
			set	{ SetFieldStatus( "FirstName", value ); }
		}

		public bool ValidLastName
		{
			set	{ SetFieldStatus( "LastName", value ); }
		}

		public bool ValidAddress
		{
			set	{ SetFieldStatus( "Address1", value ); }
		}

		public bool ValidPostCode
		{
			set	{ SetFieldStatus( "PostCode", value ); }
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
				// TODO: Trying to set an unknown field
			}
		}

		protected void Page_Load(object sender, System.EventArgs e)
		{
			
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
