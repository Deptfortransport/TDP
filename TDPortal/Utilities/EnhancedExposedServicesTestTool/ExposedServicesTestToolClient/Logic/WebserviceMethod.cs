// ***********************************************************************************
// NAME 		: WebserviceMethod.cs
// AUTHOR 		: Manuel Dambrine
// DATE CREATED : 06-Dec-2005
// DESCRIPTION 	: WebserviceMethod class with properties for each method in a webservice
// ************************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Stubs/ExposedServicesTestTool/ExposedServicesTestToolClient/Logic/WebserviceMethod.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:49:16   mturner
//Initial revision.
//
//   Rev 1.5   Feb 06 2006 09:38:06   mdambrine
//Rework based on CR054_IR_3318 Enhanced Exposed Services Test Tool.doc
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.4   Jan 18 2006 11:55:24   mdambrine
//added serializable attributes
//
//   Rev 1.3   Jan 18 2006 10:25:22   mdambrine
//tweak to get the n-unit test to work with the front-end
//
//   Rev 1.2   Dec 23 2005 16:04:12   mdambrine
//Fxcop fixes
//Resolution for 3318: Project Lauren - Exposed Services Test Tool
//
//   Rev 1.1   Dec 21 2005 15:54:00   mdambrine
//addition of xslt transformation and bugfixes
//Resolution for 3318: Project Lauren - Exposed Services Test Tool
//
//   Rev 1.0   Dec 20 2005 16:36:36   mdambrine
//Initial revision.
//Resolution for 3318: Project Lauren - Exposed Services Test Tool

using System;
using System.Web;

namespace ExposedServicesTestToolClient
{
	/// <summary>
	/// A webservice class encapsulating webservice properties
	/// </summary>
	[Serializable]
	public class WebServiceMethod
	{

		#region declarations
		private string name = string.Empty;		
		private string soapAction = string.Empty;
		private string methodNamespace = string.Empty;
		private string outputPage = "Output.aspx?key=";
		private bool isAsync = false;
		private string xsltPath = string.Empty;
		#endregion
			
		#region constructors
		public WebServiceMethod()
		{			
		}
		#endregion

		#region properties
		public string Name
		{
			get{ return name;}
			set{ name = value;}
		}			
		
		public string SoapAction
		{
			get{ return soapAction;}
			set{ soapAction = value;}
		}

		public string MethodNamespace
		{
			get{ return methodNamespace;}
			set{ methodNamespace = value;}
		}

		public string OutputPage
		{
			get{ return outputPage;}
			set{ outputPage = value;}
		}

		public bool IsAsync
		{
			get{ return isAsync;}
			set{ isAsync = value;}
		}	

		public string XsltPath
		{			
			get{ return xsltPath;}							
			set{ xsltPath = value;}
		}
		#endregion

		#region methods
		/// <summary>
		/// returns the path to the xsltfile for this webservicemethod
		/// </summary>
		/// <param name="serverName"></param>
		/// <returns></returns>
		public string GetTransformationPath(string serverName)
		{	
			return "http://" + serverName + "/" + HttpRuntime.AppDomainAppVirtualPath + "/" + XsltPath;
		}
		#endregion
				
	}
}
