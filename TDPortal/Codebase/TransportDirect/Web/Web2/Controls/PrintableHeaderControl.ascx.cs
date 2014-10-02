// *********************************************** 
// NAME                 : PrintableHeaderControl.ascx.cs 
// AUTHOR               : Robert Griffith
// DATE CREATED         : 30/11/2005
// DESCRIPTION          : A new header version for printable pages
// ************************************************
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/PrintableHeaderControl.ascx.cs-arc  $
//
//   Rev 1.3   Jan 15 2009 13:22:30   apatel
//XHTML Compliance Changes
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.2   Mar 31 2008 13:22:26   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:16:58   mturner
//Initial revision.
//
//   Rev 1.1   Feb 07 2006 13:57:20   RGriffith
//Suggested changes at Merge time
namespace TransportDirect.UserPortal.Web.Controls
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	using TransportDirect.UserPortal.Web.Controls;
	using TransportDirect.UserPortal.Resource;

	/// <summary>
	///		Summary description for PrintableHeaderControl.
	/// </summary>
	public partial class PrintableHeaderControl : TDUserControl
	{

		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Initialise image Url's/AlternateText for the PrintableHeaderControl
			transportDirectLogoImg.ImageUrl = GetResource("PrintableHeaderControl.transportDirectLogoImg.ImageUrl");
			transportDirectLogoImg.AlternateText = GetResource("PrintableHeaderControl.transportDirectLogoImg.AlternateText");

			connectingPeopleImg.ImageUrl = GetResource("PrintableHeaderControl.connectingPeopleImg.ImageUrl");
			connectingPeopleImg.AlternateText = GetResource("PrintableHeaderControl.connectingPeopleImg.AlternateText");
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
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
		}
		#endregion
	}
}
