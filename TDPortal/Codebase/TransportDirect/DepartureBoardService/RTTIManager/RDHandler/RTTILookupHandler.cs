// *********************************************** 
// NAME                 : RTTILookupHandler.cs
// AUTHOR               : Sanjeev Chand
// DATE CREATED         : 14/01/2005 
// DESCRIPTION  		: The data handler class for the lookup tables used for RTTI Manager
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/DepartureBoardService/RTTIManager/RTTILookupHandler.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:21:40   mturner
//Initial revision.
//
//   Rev 1.1   Mar 02 2005 10:48:46   schand
//Fixed Code review errors
//
//   Rev 1.0   Feb 28 2005 16:23:06   passuied
//Initial revision.
//
//   Rev 1.2   Feb 02 2005 15:33:24   schand
//applied FxCop rules
//
//   Rev 1.1   Jan 21 2005 14:22:38   schand
//Code clean-up and comments has been added

using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.DatabaseInfrastructure;
using TransportDirect.Common;
using TransportDirect.Common.Logging;
using Logger = System.Diagnostics.Trace;
using TransportDirect.UserPortal.DepartureBoardService;   
using TransportDirect.UserPortal.DepartureBoardService.DepartureBoardFacade;
using TransportDirect.UserPortal.DepartureBoardService.RTTIManager ;

namespace TransportDirect.UserPortal.DepartureBoardService.RTTIManager
{
	/// <summary>
	/// The data handler class for the lookup tables used for RTTI Manager.
	/// </summary>
	public class RTTILookupHandler: IRTTILookupHandler
	{	
		private bool dataLookupAvailable = false;
		private const string strSPName = "RTTILookup";
		private const string trainLookupFilter = "ATOC";
		private const string reasonLookupFilter = "ReasonCode";
		private DataTable trainOperators = new DataTable();
		private DataTable reasonLookUp = new DataTable();


		/// <summary>
		/// Class contructor 
		/// </summary>
		public RTTILookupHandler()
		{	// creating the instance of sql helper 
			SqlHelper sqlHelper = new SqlHelper();
			
			try
			{
				dataLookupAvailable = false ; 
				// check if user wants any data at this point 
				//open connection to TransientPortalDB
				sqlHelper.ConnOpen(SqlHelperDatabase.TransientPortalDB);
				DataSet dataSet;
				//use stored procedure to return two tables ReasonCode description and Train Operators
				dataSet = sqlHelper.GetDataSet(strSPName , new Hashtable());
				
				// Check number of tables it should be 2 
				if (dataSet.Tables.Count == 2 )
					dataLookupAvailable = true;				
				else
					dataLookupAvailable = false;				

				//store the resultant data table
				reasonLookUp = (DataTable) dataSet.Tables[0];
				trainOperators = (DataTable) dataSet.Tables[1];								
			}
			// catching exceptions
			catch (SqlException sqlEx)
			{
				OperationalEvent oe = new OperationalEvent(	TDEventCategory.Infrastructure,
					TDTraceLevel.Error, strSPName + " "  + "SQLNUM["+sqlEx.Number+"] :"+sqlEx.Message+":" );

				throw new TDException("SqlException caught : " + sqlEx.Message, sqlEx,
					false, TDExceptionIdentifier.TNSQLHelperError);
			}
			catch (TDException tdex)
			{    
				string message = "Error Calling Stored Procedure : " + strSPName.ToString() + "  "  + tdex.Message;
				OperationalEvent oe = new OperationalEvent(TDEventCategory.Infrastructure,
					TDTraceLevel.Error, message + " "  + "TDException :" + tdex.Message + ":");

				throw new TDException(message, tdex, false, TDExceptionIdentifier.TNSQLHelperStoredProcedureFailure);
			}
			catch (Exception ex)
			{
				string message = "Error Calling Stored Procedure : " + strSPName.ToString() + "  "  + ex.Message;
				OperationalEvent oe = new OperationalEvent(TDEventCategory.Infrastructure,
					TDTraceLevel.Error, message + " "  + "TDException :" + ex.Message + ":");
				throw ex ;
				
			}
			finally
			{
				//close the database connection
				sqlHelper.ConnClose();
			}

		}

		
		/// <summary>
		/// Public property to indicate whether data lookup is available or not?
		/// </summary>
		public bool DataLookUpAvailable
		{
			get{return dataLookupAvailable;}			
		}
	
		
		/// <summary>
		/// Get the description for the given reason code
		/// </summary>
		/// <param name="reasonCode">Reason code</param>
		/// <returns>Reason description as string</returns>
		public string GetReasonDescription(string reasonCode)
		{
			string reasonDescription = string.Empty ;
			string filterExpression = string.Empty ;
			try
			{	
				// Check whether data lookup tables are available
				// Check if datatable are blank or not?
				if ((!dataLookupAvailable) || reasonLookUp == null || reasonCode == null || reasonCode.Length==0)
				{						
					return reasonDescription;
				}

				filterExpression = reasonLookupFilter + "=" + reasonCode.Trim().ToUpper() ; 
				
				DataRow[] dataRowColl = trainOperators.Select(filterExpression) ;

				if (dataRowColl.Length == 0)
				{
					return reasonDescription;
				}
				
				//Getting first row & 2nd column 
				DataRow dataRow = dataRowColl[0];			

				if (dataRow == null)
				{
					return reasonDescription;
				}

				reasonDescription = dataRow[1].ToString() ;
				
				if (reasonDescription ==null)
				{
					return string.Empty ;
				}
				
				return reasonDescription;


			}
			catch
			{
				return string.Empty ;
			}
		}


		/// <summary>
		/// Gets the operator name for the given operator code
		/// </summary>
		/// <param name="operatorCode">Operator code</param>
		/// <returns>The operator name </returns>
		public string GetOperatorName(string operatorCode)
		{	string operatorName = string.Empty ;
			string filterExpression = string.Empty ;
			try
			{	
				// Check whether data lookup tables are available
				// Check if datatable are blank or not?
				if ((!dataLookupAvailable) || trainOperators == null || operatorCode == null || operatorCode.Length ==0)
					return operatorName;				

				// building filter expression
				filterExpression = trainLookupFilter + "='" + operatorCode.Trim().ToUpper() + "' "; 
				
				//apply filter
				DataRow[] dataRowColl = trainOperators.Select(filterExpression) ;
				
				// Check for rsult rows 
				if (dataRowColl.Length == 0)
					return operatorName;				
				
				// Ideally above query should return only one row. 
				// The second column would contain the operator name. 
				//Getting first row & 2nd column 
				DataRow dataRow = dataRowColl[0];			
				
				if (dataRow == null)
					return operatorName;				

				operatorName = dataRow[1].ToString() ;
				
				if (operatorName ==null)
					return string.Empty ;				

				return operatorName; 
			}
			catch
			{
				return string.Empty ;
			}
		}

		

	}

}
