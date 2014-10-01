// ***********************************************************************************
// NAME 		: SoapRequest
// AUTHOR 		: Manuel Dambrine
// DATE CREATED : 07-Dec-2005
// DESCRIPTION 	: Incapsulation of a soaprequest
// ************************************************************************************
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Stubs/ExposedServicesTestTool/ExposedServicesTestToolClient/Logic/SoapRequest.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:49:14   mturner
//Initial revision.
//
//   Rev 1.11   Feb 07 2006 10:50:34   mdambrine
//added pvcs log
//
//   Rev 1.10   Feb 07 2006 10:17:40   mdambrine
//added log

using System;
using System.Net;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Web;
using System.Web.SessionState;


namespace ExposedServicesTestToolClient
{

	/// <summary>
	/// Incapsulation of a soaprequest
	/// </summary>
	[Serializable]
	public class SoapRequest
	{	
		#region declarations
		private string transactionId; 
		private string soapMessage;
		private Webservice webService;
		private DateTime dateRequested;			
	
		#region const values
		private string transactionIdTagValue = HelperClass.GetConfigSetting("TransactionIdTagValue");
		
		#endregion
		#endregion

		#region constructors
		public SoapRequest()
		{
		
		}

		/// <summary>
		/// this constructor will get the transactionid from the soapmessage
		/// </summary>
		/// <param name="soapMessage"></param>
		/// <param name="webService"></param>
		/// <param name="sessionId"></param>		
		public SoapRequest(string soapMessage,
						   Webservice webService,
						   string sessionId)
		{
			//get the transactionId and append the users session
			try
			{
				this.transactionId = HelperClass.GetParameter(soapMessage, transactionIdTagValue, false,webService.WebServiceMethods[0].MethodNamespace);
				//check if there are underscore characters in the transactionID				
				if (transactionId.IndexOf('_', 0, transactionId.Length) != -1)
					throw new Exception("underscore");

				this.transactionId += "_" + sessionId;				
			}
			catch(Exception ex)
			{
				if (ex.Message.StartsWith("underscore"))
					throw new Exception("Please do not use underscore characters in the <" + transactionIdTagValue + "> field, this is a limitation to the testtool");
				else
					throw new Exception("The mandatory field <" + transactionIdTagValue + "> was not found in one of the messages that you tried to upload or" +
										" this uploaded message does not match with the webservice you are calling");
			}

			this.soapMessage = soapMessage;
			this.webService = webService;
		}
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

		public DateTime DateRequested
		{
			get{ return dateRequested;}
			set{ dateRequested = value;}
		}
		#endregion

		#region public methods
		/// <summary>
		/// this method will call the webservice sending a soapmessage that is 
		/// selected by the user. It will als catch the actual soapresponse if the 
		/// webservice is synchronous.
		/// </summary>
		public void Invoke(System.Web.HttpContext context)
		{			
		
			string transId = string.Empty;

			try
			{
				//we need to clone the request because we want to have no influence of requests following				
				SoapRequest thisRequest = CloneSoapRequest();

				//set the pre-send status
				thisRequest.dateRequested = DateTime.Now;		

				//add the request
				thisRequest.TransactionId = CacheEngine.RetrieveTransactions(context).AddSoapRequest(thisRequest, Status.Pending, true);																			
				transId = thisRequest.TransactionId;				

				string SoapEnv = thisRequest.soapMessage;				

				//attach the soapheader if it is a secure webservice
				if (webService.SoapHeaderPath.Length != 0)
				{
					SoapEnv = HelperClass.ReadXmlFromFile(webService.SoapHeaderPath);
					
					string messageBody = HelperClass.GetOuterXml(soapMessage, "soap:Body");

					//replace the soap:Body, wsa:Action and the wsa:To fields
					SoapEnv = HelperClass.ReplaceParameter(SoapEnv, "soap:Body", messageBody);					
					SoapEnv = HelperClass.ReplaceParameter(SoapEnv, "wsa:Action", "<wsa:Action>" + webService.WebServiceMethods[0].SoapAction );
					SoapEnv = HelperClass.ReplaceParameter(SoapEnv, "wsa:To", "<wsa:To>" + webService.Uri);
					SoapEnv = HelperClass.ReplaceParameter(SoapEnv, "wsa:MessageID", "<wsa:MessageID>" + "uuid:" + thisRequest.TransactionId);					
				}				

				thisRequest.soapMessage = SoapEnv;

				//Update the soapmessage because the sessionId has been appended to the transactionId
				thisRequest.soapMessage = HelperClass.ReplaceParameter(thisRequest.soapMessage, transactionIdTagValue, "<" + transactionIdTagValue + ">" + thisRequest.TransactionId );
																				
				HttpWebRequest req = (HttpWebRequest)WebRequest.Create(WebService.Uri);
				req.Headers.Add("SOAPAction:" + WebService.WebServiceMethods[0].SoapAction);
				req.Method = "POST";				
				req.ContentType = "text/xml;charset=\"utf-8\"";
				//req.KeepAlive = true;
				req.Timeout = Convert.ToInt32(HelperClass.GetConfigSetting("WebserviceTimeout"), HelperClass.Provider);

				req.ContentLength = thisRequest.soapMessage.Length; 

				using(StreamWriter sw = new StreamWriter(req.GetRequestStream()))
				{
					sw.Write(thisRequest.soapMessage);
					sw.Close();

				}							


				HttpWebResponse resp =  (HttpWebResponse)req.GetResponse();
				string ResponseString = "";
		
				using (StreamReader sr = new StreamReader(resp.GetResponseStream()))
				{
					// ResponseString will hold the Response SOAP envelope.
					ResponseString = sr.ReadToEnd();
					sr.Close();

				} 				
			
				//if the webservice is synchronous call get the response straight away
				if (WebService.WebServiceMethods[0].IsAsync == false)					
					AddSyncResponse(thisRequest.TransactionId, ResponseString, Status.Received, context);				
				else
					AddSyncResponse(thisRequest.TransactionId, ResponseString, Status.Pending, context);				
			}			
			catch (WebException  wEx)
			{
				string ResponseString = "";	
				if (wEx.Status != WebExceptionStatus.Timeout)
				{
					using (StreamReader sr = new StreamReader(wEx.Response.GetResponseStream()))
					{
						// ResponseString will hold the Response SOAP envelope.
						ResponseString = sr.ReadToEnd();
						sr.Close();					
					}
				}
				else
					ResponseString = ((Exception) wEx).Message;

				//add an response object with the error message in it
				string errormessage = "<?xml version='1.0' encoding='utf-8' ?><errormessage>" +
					ResponseString + 
					"</errormessage>";
				if (ResponseString.Length ==0)										
					AddSyncResponse(transId, errormessage, Status.Error, context);				
				else
					AddSyncResponse(transId, ResponseString, Status.Error, context);				

			}				
			catch (Exception ex)
			{
				//add an response object with the error message in it
				string errormessage = "<?xml version='1.0' encoding='utf-8' ?><errormessage>" +
									  ex.Message + 
									  "</errormessage>";
										
				AddSyncResponse(transId, errormessage, Status.Error, context);
			}					
		}
		#endregion

		#region private methods
		/// <summary>
		/// adds a response to the transactions array
		/// </summary>
		/// <param name="transactionId">transaction id of response</param>
		/// <param name="response">response text (xml)</param>
		/// <param name="status">status the transaction is in</param>
		/// <param name="context">the context the application is running in</param>
		private void AddSyncResponse(string transactionId,
									 string response, 
									 Status status, 
									 System.Web.HttpContext context)
		{
			SoapResponse soapResponse = new SoapResponse(transactionId, 
														 response,
														 webService,
														 DateTime.Now);
					
			CacheEngine.RetrieveTransactions(context).AddSoapResponse(soapResponse, status);
		}

		/// <summary>
		/// this method is needed to detach a newly created method from one that you created
		/// </summary>
		/// <param name="SoapRequest">Request to clone</param>
		/// <returns></returns>
		private SoapRequest CloneSoapRequest()
		{
			SoapRequest clonedSoapRequest = new SoapRequest();

			clonedSoapRequest.DateRequested = this.DateRequested;
			clonedSoapRequest.SoapMessage = this.soapMessage;
			clonedSoapRequest.TransactionId = this.TransactionId;
			clonedSoapRequest.WebService = this.WebService;

			return clonedSoapRequest;
		}
		#endregion
	}
}
