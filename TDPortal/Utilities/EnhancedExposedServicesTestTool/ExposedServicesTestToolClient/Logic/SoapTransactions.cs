// ***********************************************************************************
// NAME 		: SoapTransaction.cs
// AUTHOR 		: Manuel Dambrine
// DATE CREATED : 06-Dec-2005
// DESCRIPTION 	: SoapTransactions container object
// ************************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Stubs/ExposedServicesTestTool/ExposedServicesTestToolClient/Logic/SoapTransactions.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:49:16   mturner
//Initial revision.
//
//   Rev 1.5   Feb 06 2006 09:38:04   mdambrine
//Rework based on CR054_IR_3318 Enhanced Exposed Services Test Tool.doc
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.4   Jan 24 2006 11:47:56   mdambrine
//fixed bug in deletetransactions
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
//   Rev 1.0   Dec 20 2005 16:36:36   mdambrine
//Initial revision.
//Resolution for 3318: Project Lauren - Exposed Services Test Tool
using System;
using System.Collections;
using System.Data;
using System.Globalization;


namespace ExposedServicesTestToolClient
{
	/// <summary>
	/// This class will encapsulate all the requests and responses send by the tool. 
	/// Also this class will be added to the system cache. 
	/// </summary>
	[Serializable]
	public class SoapTransactions
	{
		private Hashtable soapTransactions;

		#region constructors
		/// <summary>
		/// default constructor
		/// </summary>
		public SoapTransactions()
		{
			soapTransactions = new Hashtable();
		}
		#endregion

		#region public methods
		/// <summary>
		/// This functions will construct a DataTable object and a row will 
		/// be added for each entry in the SoapTransactions hashtable.
		/// </summary>
		/// <param name="sessionId">The session ID of the user to sort the table on</param>
		/// <returns>a datatable that is bindable to the datagrid</returns>
		public DataTable GetResultsTable(string sessionId)
		{
			DataTable resultsTable = DefineResultsTable();

			//get all the soaptransactions created by that session
			foreach(string key in soapTransactions.Keys)
			{	
				string[] idParts = key.Split('_');
				string sessionIdInKey = string.Empty;

				if (idParts.Length > 1)
					sessionIdInKey = ((string[])key.Split('_'))[1];								

				//look for the sessionId in the key
				if (sessionIdInKey.StartsWith(sessionId))
				{
					SoapTransaction soapTransaction = (SoapTransaction) soapTransactions[key];

					//add a row to the datatable
					DataRow newRow = resultsTable.NewRow();

					newRow["TransactionId"] = key;
					if (soapTransaction.SoapRequest != null)
					{
						newRow["WebserviceUri"] = soapTransaction.SoapRequest.WebService.Name + "/" + soapTransaction.SoapRequest.WebService.WebServiceMethods[0].Name;
						newRow["OutputUriRequest"] = soapTransaction.SoapRequest.WebService.WebServiceMethods[0].OutputPage + key + "&IsResponse=false";
						newRow["DateRequested"] = soapTransaction.SoapRequest.DateRequested;
					}
					newRow["Status"] = soapTransaction.StatusEnum;						
					if 	(soapTransaction.StatusEnum == 	Status.Received || 
						 soapTransaction.StatusEnum == 	Status.Error)
					{	
						newRow["OutputUriResponse"] = soapTransaction.SoapResponse.WebService.WebServiceMethods[0].OutputPage + key + "&IsResponse=true";
						newRow["DateReceived"] = soapTransaction.SoapResponse.DateReceived;
					}					
					resultsTable.Rows.Add(newRow);

				}
			}

			return resultsTable;
		}
		

		/// <summary>
		/// Adds a soaprequest to the array of transactions
		/// will create a new transaction if it is not existing yet
		/// </summary>
		/// <param name="soapRequest">Soaprequest object to add to the array</param>
		/// <param name="status">status the transaction is in</param>
		/// <param name="createNew">Defines if a new unique transaction needs to be created</param>
		/// <returns>The transactionID, this key might have changed when unique transaction is set to true</returns>
		public string AddSoapRequest(SoapRequest soapRequest, Status status, bool createNew)
		{
			string transactionId = soapRequest.TransactionId;
			bool uniqueTransactions = Convert.ToBoolean(HelperClass.GetConfigSetting("UniqueTransactionIds"), HelperClass.Provider);

			//get a unique transaction id
			//will add a "_sequence in array" to the transactionId to make sure there are no duplicates
			if (uniqueTransactions && createNew)
				transactionId = GetUniqueTransactionNumber(transactionId);
			
			if (soapTransactions.ContainsKey(transactionId))
			{
				SoapTransaction soapTransaction = (SoapTransaction) soapTransactions[transactionId];
				
				soapTransaction.SoapRequest = soapRequest;
				soapTransaction.StatusEnum = status;
				soapTransaction.TransactionId = transactionId;
			}
			else
			{
				//if no transaction has been found create a new one
				SoapTransaction soapTransaction = new SoapTransaction();
				
				soapTransaction.SoapRequest = soapRequest;
				soapTransaction.StatusEnum = status;
				soapTransaction.TransactionId = transactionId;

				soapTransactions.Add(transactionId, soapTransaction);
			}

			return transactionId;

		}

		/// <summary>
		/// adds a soapresponse to the transactions table
		/// will create a new transaction if it is not created yet
		/// </summary>
		/// <param name="soapResponse"></param>
		public void AddSoapResponse(SoapResponse soapResponse, Status status)
		{
			string transactionId = soapResponse.TransactionId;			

			if (soapTransactions.ContainsKey(transactionId))
			{				
				SoapTransaction soapTransaction = (SoapTransaction) soapTransactions[transactionId];

				soapTransaction.SoapResponse = soapResponse;
				soapTransaction.StatusEnum = status;
			}
			else
			{
				//if no transaction has been found create a new one
				SoapTransaction soapTransaction = new SoapTransaction();
				
				soapTransaction.SoapResponse = soapResponse;
				soapTransaction.StatusEnum = status;

				soapTransactions.Add(transactionId, soapTransaction);
			}
			
		}

		/// <summary>
		/// This method will return a transactionId that is unique across all keys in the 
		/// soaptransactions array. Will append the key with _count
		/// </summary>
		/// <param name="transactionId">id to be made unique</param>
		/// <returns>unique transactionid</returns>
		public string GetUniqueTransactionNumber(string transactionId)
		{
			int count = 0;

			//get the actual transactionId and the sessionId
			string transactionIdAcross = transactionId.Split('_')[0].ToString() + //transactionID										
										 "_" +transactionId.Split('_')[1].ToString();  //sessionID

			//get all the soaptransactions created by that session
			foreach(string key in soapTransactions.Keys)
			{
				if (key.StartsWith(transactionIdAcross))
					count++;
			}

			//if there are more occurences add _next number
			if (count !=0)
				transactionId = transactionIdAcross + "_" + count;
			
			return transactionId;
		}

		/// <summary>
		/// Gets a transaction based on a transactionId key
		/// </summary>
		/// <param name="transactionId"></param>
		/// <returns></returns>
		public SoapTransaction GetTransaction(string transactionId)
		{
			return (SoapTransaction) soapTransactions[transactionId];
		}

		/// <summary>
		/// get the request within a transaction
		/// </summary>
		/// <param name="transactionId">id of the transaction</param>
		/// <returns>Soaprequest of that transaction</returns>
		public SoapRequest GetRequest(string transactionId)
		{
			return ((SoapTransaction) soapTransactions[transactionId]).SoapRequest;
		}

		/// <summary>
		/// gets a response within a transaction
		/// </summary>
		/// <param name="transactionId">id of the transaction</param>
		/// <returns>SoapResponse of that transaction</returns>
		public SoapResponse GetResponse(string transactionId)
		{
			return ((SoapTransaction) soapTransactions[transactionId]).SoapResponse;
		}

		/// <summary>
		/// Deletes a transaction base on the id
		/// </summary>
		/// <param name="transactionId">id of the transaction to delete</param>
		public void DeleteTransaction(string transactionId)
		{
			soapTransactions.Remove(transactionId);
		}

		/// <summary>
		/// Will delete all the transactions for that session
		/// </summary>
		/// <param name="sessionId">sessionId to deleteon</param>
		public void DeleteTransactions(string sessionId)
		{			
			ArrayList keysArray = new ArrayList();

			foreach(string key in soapTransactions.Keys)
			{
				keysArray.Add(key);
			}

			//count in reverse direction as we are removing items
			for(int i = keysArray.Count-1; i>=0; i--)
			{				
				string key = (string) keysArray[i];
				string[] idParts = key.Split('_');
				string sessionIdInKey = string.Empty;

				if (idParts.Length > 1)
					sessionIdInKey = ((string[])key.Split('_'))[1];

				if (sessionIdInKey.StartsWith(sessionId))
					soapTransactions.Remove(key);	
			}
		}
		
		#endregion

		#region private methods
		/// <summary>
		/// Define the structure of the table that is passed back to the front-end
		/// the structure of this table needs to match the datagrid object it is to be 
		/// bound to.
		/// </summary>
		/// <returns></returns>
		private DataTable DefineResultsTable()
		{
			DataTable resultsTable = new DataTable();	
			resultsTable.Locale = new CultureInfo("");

			resultsTable.Columns.Add(new DataColumn("TransactionId", System.Type.GetType("System.String")));
			resultsTable.Columns.Add(new DataColumn("WebserviceUri", System.Type.GetType("System.String")));
			resultsTable.Columns.Add(new DataColumn("OutputUriRequest", System.Type.GetType("System.String")));
			resultsTable.Columns.Add(new DataColumn("OutputUriResponse", System.Type.GetType("System.String")));
			resultsTable.Columns.Add(new DataColumn("DateRequested", System.Type.GetType("System.DateTime")));
			resultsTable.Columns.Add(new DataColumn("DateReceived", System.Type.GetType("System.DateTime")));
			resultsTable.Columns.Add(new DataColumn("Status", System.Type.GetType("System.String")));

			return resultsTable;
		}

		

		#endregion

	}
}
