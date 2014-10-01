// ***********************************************************************************
// NAME 		: ExposedServicePage.aspx.cs
// AUTHOR 		: Manuel Dambrine
// DATE CREATED : 06-Dec-2005
// DESCRIPTION 	: TestPage for Exposed webservices
// ************************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Stubs/ExposedServicesTestTool/ExposedServicesTestToolClient/Logic/SoapResponse.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:49:16   mturner
//Initial revision.
//
//   Rev 1.4   Feb 06 2006 09:38:04   mdambrine
//Rework based on CR054_IR_3318 Enhanced Exposed Services Test Tool.doc
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.3   Jan 18 2006 11:55:24   mdambrine
//added serializable attributes
//
//   Rev 1.2   Jan 18 2006 10:25:22   mdambrine
//tweak to get the n-unit test to work with the front-end
//
//   Rev 1.1   Dec 23 2005 16:04:12   mdambrine
//Fxcop fixes
//Resolution for 3318: Project Lauren - Exposed Services Test Tool
//
//   Rev 1.0   Dec 20 2005 16:36:34   mdambrine
//Initial revision.
//Resolution for 3318: Project Lauren - Exposed Services Test Tool

using System;

namespace ExposedServicesTestToolClient
{
	/// <summary>
	/// Summary description for Response.
	/// </summary>
	[Serializable]
	public class SoapResponse
	{		
		#region declarations
		private string transactionId;
		private string soapMessage;
		private Webservice webService;
		private DateTime dateReceived;
		
		#region const values
		private string transactionIdTagValue = HelperClass.GetConfigSetting("TransactionIdTagValue");
		
		#endregion

		#endregion

		#region properties
		public string TransactionId
		{
			get{ return transactionId;}
			set{ transactionId = value;}
		}

		public string SoapMessage
		{
			get{ return soapMessage;}
			set{ soapMessage = value;}
		}

		public Webservice WebService
		{
			get{ return webService;}
			set{ webService = value;}
		}

		public DateTime DateReceived
		{
			get{ return dateReceived;}
			set{ dateReceived = value;}
		}
		#endregion

		#region constructors
		/// <summary>
		/// default constructor
		/// </summary>
		public SoapResponse()
		{					
		}

		/// <summary>
		/// Constructor when all members are known
		/// </summary>
		/// <param name="transactionId">TransactionId for this response</param>
		/// <param name="soapMessage">Message returned from the webservice</param>
		/// <param name="webService">Webservice resonse was coming from</param>
		/// <param name="dateRecieved">Date the response was recieved</param>
		public SoapResponse(string transactionId,
							string soapMessage,
							Webservice webService,
							DateTime dateReceived)
		{					
			this.transactionId = transactionId;
			this.soapMessage = soapMessage;
			this.webService = webService;
			this.dateReceived = dateReceived;
		}

		/// <summary>
		/// Constructor when the the transactionId is unknown and needs to be feched from the SOAP message
		/// </summary>
		/// <param name="soapMessage">Message returned from the webservice</param>		
		/// <param name="dateRecieved">Date the response was recieved</param>
		public SoapResponse(string soapMessage,							
							DateTime dateReceived)
		{
			//get the transactionId and append the users session
			this.soapMessage = soapMessage;
			this.webService = new Webservice();
			this.WebService.WebServiceMethods = new WebServiceMethod[1];
			this.WebService.WebServiceMethods[0] = new WebServiceMethod();
			this.dateReceived = dateReceived;
			try
			{
				this.transactionId = HelperClass.GetInnerXml(soapMessage, transactionIdTagValue);
			}
			catch
			{
				throw new Exception("The mandatory field <" + transactionIdTagValue + "> was not found in the message that was returned from the webservice:" + soapMessage);
			}					
		}
		#endregion

	}
}
