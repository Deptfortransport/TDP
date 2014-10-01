// ***********************************************************************************
// NAME 		: Webservice.cs
// AUTHOR 		: Manuel Dambrine
// DATE CREATED : 06-Dec-2005
// DESCRIPTION 	: Webservice class with webservice properties
// ************************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Stubs/ExposedServicesTestTool/ExposedServicesTestToolClient/Logic/Webservice.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:49:16   mturner
//Initial revision.
//
//   Rev 1.3   Feb 06 2006 09:38:06   mdambrine
//Rework based on CR054_IR_3318 Enhanced Exposed Services Test Tool.doc
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.2   Jan 18 2006 11:55:24   mdambrine
//added serializable attributes
//
//   Rev 1.1   Dec 23 2005 16:04:12   mdambrine
//Fxcop fixes
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
	public class Webservice
	{

		#region declarations
		private string name = string.Empty;		
		private string soapHeaderPath = string.Empty;
		private string uri = string.Empty;
		private WebServiceMethod[] webServiceMethods;
		#endregion
			
		#region constructors
		public Webservice()
		{			
		}
		#endregion

		#region properties
		public string Name
		{
			get{ return name;}
			set{ name = value;}
		}

		public string SoapHeaderPath
		{
			get
			{ 				
				return soapHeaderPath;
			}				
			set{ soapHeaderPath = value;}
		}

		public string Uri
		{
			get{ return uri;}
			set{ uri = value;}
		}

		public WebServiceMethod[] WebServiceMethods
		{
			get{ return webServiceMethods;}
			set{ webServiceMethods = value;}
		}
	
		#endregion

		
	}
}
