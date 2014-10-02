// *********************************************** 
// NAME                 : Help.aspx.cs 
// AUTHOR               : Esther Severn
// DATE CREATED         : 15/07/2003 
// DESCRIPTION          : Main Help page providing
//						  users with additional
//						  help information and 
//                        support
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/UserSupport/Help.aspx.cs-arc  $ 
//
//   Rev 1.2   Mar 31 2008 13:27:18   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:33:48   mturner
//Initial revision.
//
//   Rev 1.3   Feb 10 2006 15:09:36   build
//Automatically merged from branch for stream3180
//
//   Rev 1.2.1.0   Dec 06 2005 18:53:30   NMoorhouse
//TD101 Home Page Phase 2 - Standard page changes
//Resolution for 3180: DEL 8.1 Stream UEE: work on Homepage phase 2
//
//   Rev 1.2   Jul 18 2003 11:56:26   JMorrissey
//Updated TDPage.ChannelName to use amended property called TDPage.SessionChannelName
//
//   Rev 1.1   Jul 15 2003 16:16:10   ESevern
//added some temporary general help text
//
//   Rev 1.0   Jul 15 2003 15:21:56   ESevern
//Initial Revision

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

namespace TransportDirect.UserPortal.Web.UserSupport
{
	/// <summary>
	/// Summary description for Help.
	/// </summary>
	public partial class Help : TDPage
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			hypFeedback.NavigateUrl = TDPage.SessionChannelName + "Feedback Help"; 
			PageTitle = "Help";
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
