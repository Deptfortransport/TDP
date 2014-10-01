// ***********************************************************************************
// NAME 		: Output.aspx.cs
// AUTHOR 		: Manuel Dambrine
// DATE CREATED : 06-Dec-2005
// DESCRIPTION 	: Page for displaying soapresponses and requests
// ************************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Stubs/ExposedServicesTestTool/ExposedServicesTestToolClient/Templates/Output.aspx.cs-arc  $
//
//   Rev 1.2   Aug 18 2009 15:27:58   mmodi
//Corrected build warnings
//
//   Rev 1.1   Apr 17 2009 13:53:54   mmodi
//Updated to .NET 2.0
//
//   Rev 1.0   Nov 08 2007 12:49:38   mturner
//Initial revision.
//
//   Rev 1.6   Feb 09 2006 10:44:28   mdambrine
//added config setting for hostname
//
//   Rev 1.5   Feb 08 2006 13:51:50   mdambrine
//changed the servicename in the xslt path to the url host name
//
//   Rev 1.4   Jan 18 2006 17:30:02   schand
//Removing the webservice namespace if any
//Resolution for 3129: Enhanced  Exposed Services Framework Enhancements
//
//   Rev 1.3   Jan 18 2006 10:25:04   mdambrine
//tweak to get the n-unit test to work with the front-end
//
//   Rev 1.2   Dec 23 2005 16:04:14   mdambrine
//Fxcop fixes
//Resolution for 3318: Project Lauren - Exposed Services Test Tool
//
//   Rev 1.1   Dec 21 2005 15:55:48   mdambrine
//addition of xslt transformation
//Resolution for 3318: Project Lauren - Exposed Services Test Tool
//
//   Rev 1.0   Dec 20 2005 16:38:36   mdambrine
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
using System.Text;
using System.IO;
using System.Xml.Xsl;
using System.Xml.XPath;
using System.Configuration;

namespace ExposedServicesTestToolClient
{
	/// <summary>
	/// Summary description for Output.
	/// </summary>
	public partial class Output : System.Web.UI.Page
	{
	
		#region pageload
		protected void Page_Load(object sender, System.EventArgs e)
		{
			String hostName = ConfigurationManager.AppSettings["HostName"];

			// Put user code to initialize the page here						
			Response.Clear();
			Response.ContentType = "text/xml";				
			
			//get the querystring values
			string key = Page.Request.QueryString["Key"].ToString();
			bool IsResponse = Convert.ToBoolean(Page.Request.QueryString["IsResponse"].ToString(), HelperClass.Provider);

			string xml = string.Empty;

			SoapTransaction soapTransaction = CacheEngine.RetrieveTransactions(Context).GetTransaction(key);
			if (IsResponse)
			{
				xml = soapTransaction.SoapResponse.SoapMessage;

				//if there is a transformation file specified
				//tranform the response using xslt
				if (soapTransaction.SoapResponse.WebService.WebServiceMethods[0].XsltPath.Length != 0 && 
					soapTransaction.StatusEnum == Status.Received)
				{											
					string xmlHeader = HelperClass.GetConfigSetting("xmlHeader");					
					string xsltHeader = HelperClass.GetConfigSetting("xsltHeader");					

					//add the path to the xslt file
					xsltHeader = xsltHeader.Replace("{0}",  soapTransaction.SoapRequest.WebService.WebServiceMethods[0].GetTransformationPath(hostName));
					string replaceValue = @"xmlns=""" + soapTransaction.SoapRequest.WebService.WebServiceMethods[0].MethodNamespace + @""""; 
					xml = xml.Replace(replaceValue, ""); 

					xml = xml.Replace(xmlHeader, xmlHeader + xsltHeader);
				}

			}

			else
				xml = soapTransaction.SoapRequest.SoapMessage;

			

			
			Response.Write(xml);
			Response.End();
		}
		#endregion

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
