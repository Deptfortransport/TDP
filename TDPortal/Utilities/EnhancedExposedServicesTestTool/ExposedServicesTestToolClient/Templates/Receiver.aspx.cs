// ***********************************************************************************
// NAME 		: reciever.aspx.cs
// AUTHOR 		: Manuel Dambrine
// DATE CREATED : 06-Dec-2005
// DESCRIPTION 	: Page that is handling asynchronous responses from a webservice
// ************************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Stubs/ExposedServicesTestTool/ExposedServicesTestToolClient/Templates/Receiver.aspx.cs-arc  $
//
//   Rev 1.1   Apr 17 2009 14:07:36   mmodi
//Updated for .NET 2.0
//
//   Rev 1.0   Nov 08 2007 12:49:38   mturner
//Initial revision.
//
//   Rev 1.0   Feb 06 2006 09:30:40   mdambrine
//Initial revision.
//
//   Rev 1.2   Dec 23 2005 16:04:14   mdambrine
//Fxcop fixes
//Resolution for 3318: Project Lauren - Exposed Services Test Tool
//
//   Rev 1.1   Dec 20 2005 16:53:44   mdambrine
//removed tracing for the moment
//Resolution for 3318: Project Lauren - Exposed Services Test Tool
//
//   Rev 1.0   Dec 20 2005 16:38:38   mdambrine
//Initial revision.
//Resolution for 3318: Project Lauren - Exposed Services Test Tool

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
using System.Diagnostics;

namespace ExposedServicesTestToolClient
{
	/// <summary>
	/// Summary description for Reveiver.
	/// </summary>
	public partial class Receiver : System.Web.UI.Page
	{
		private void Page_Load(object sender, System.EventArgs e)
		{
			//write the request to a file			
			System.IO.Stream stream = Page.Request.InputStream; //((System.Web.HttpRequest)(((System.Web.HttpApplication)(((Reveiver)((sender))))).Request)).InputStream;

			//convert the stream to a string
			string content = HelperClass.ConvertStreamToString(stream);

			//Here we will catch any content that was send to us through the asynchronous webservices
			//usually the webservices we have called will call the reciever.asmx or any other webservice 
			//in our project and we will catch the response
			if (content.Length !=0 && HelperClass.IsValidXml(content))
			{
				try
				{
					//get the transactionId from the xml doc
					SoapResponse soapResponse = new SoapResponse(content,						
						DateTime.Now);
					
					CacheEngine.RetrieveTransactions(Context).AddSoapResponse(soapResponse, Status.Received);

					string responsexml = HelperClass.ReadXmlFromFile(HttpRuntime.AppDomainAppPath + @"\ConsumerResonse.xml");

					Response.Clear();
					Response.ContentType = "text/xml";	

					Page.Response.Write(responsexml);
					//Response.End();
				}
				catch(Exception ex)
				{
					string exep = ex.Message;
				}
				
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
