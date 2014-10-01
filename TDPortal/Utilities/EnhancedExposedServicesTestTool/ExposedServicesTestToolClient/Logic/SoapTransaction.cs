// ***********************************************************************************
// NAME 		: SoapTransaction.cs
// AUTHOR 		: Manuel Dambrine
// DATE CREATED : 09-Dec-2005
// DESCRIPTION 	: SoapTransaction container object
// ************************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Stubs/ExposedServicesTestTool/ExposedServicesTestToolClient/Logic/SoapTransaction.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:49:16   mturner
//Initial revision.
//
//   Rev 1.3   Feb 06 2006 09:38:04   mdambrine
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
//   Rev 1.0   Dec 20 2005 16:36:34   mdambrine
//Initial revision.
//Resolution for 3318: Project Lauren - Exposed Services Test Tool
using System;


namespace ExposedServicesTestToolClient
{
	/// <summary>
	/// SoapTransaction container object
	/// </summary>
	[Serializable]
	public class SoapTransaction
	{		
		#region declarations
		private SoapRequest soapRequest;
		private SoapResponse soapResponse;
		private Status statusEnum;
		private string transactionId;	
		#endregion

		#region properties
		public SoapRequest SoapRequest
		{
			get{ return soapRequest;}
			set
			{ 
				soapRequest = value;
				transactionId = soapRequest.TransactionId;
			}
		}

		public SoapResponse SoapResponse
		{
			get{ return soapResponse;}
			set
			{ 
				soapResponse = value;
				transactionId = soapResponse.TransactionId;
			}
		}

		public Status StatusEnum
		{
			get{ return statusEnum;}
			set{ statusEnum = value;}
		}

		public string TransactionId
		{
			get{ return transactionId;}	
			set{ transactionId = value;}
		}

		/// <summary>
		/// read-only property returning the sessionId contained in the transactionId
		/// </summary>
		public string SessionId
		{
			get{ return (TransactionId.Split('_'))[1] ;}			
		}
		#endregion

		#region constructors
		/// <summary>
		/// default constructor
		/// </summary>
		public SoapTransaction()
		{			
		}
		
		#endregion
	}
}
