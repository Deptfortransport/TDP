// *********************************************** 
// NAME                 : SeasonalNoticeBoardHandler.cs
// AUTHOR               : Sanjeev Chand
// DATE CREATED         : 29/10/2004 
// DESCRIPTION  : Class that makes Seasonal Notice Board data available to clients
// ************************************************ 

using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.DatabaseInfrastructure;
using TransportDirect.Common;
using TransportDirect.Common.Logging;
using Logger = System.Diagnostics.Trace;

namespace TransportDirect.UserPortal.SeasonalNoticeBoardImport
{
	
	/// <summary>
	/// Summary description for SeasonalNoticeBoardHandler.
	/// </summary>
	[Serializable()]
	public class SeasonalNoticeBoardHandler : ISeasonalNoticeBoardHandler  
	{	
		private DataTable seasonalNoticeBoardData = null;
		string strSPName = "SeasonalNoticeBoardData";
		bool _DataAvailable = false ; 
		
//		// blank contructor
//		public SeasonalNoticeBoardHandler()
//		{
//
//		}
		
		// Constructor for getting the data 
		public SeasonalNoticeBoardHandler()
		{
			SqlHelper sqlHelper = new SqlHelper();
			
			try
			{
				_DataAvailable = false ; 
				// check if user wants any data at this point 
				//open connection to TransientPortalDB
				sqlHelper.ConnOpen(SqlHelperDatabase.TransientPortalDB);
				DataSet dataSet;
				//use stored procedure "SeasonalNoticeBoardData" to return data
				dataSet = sqlHelper.GetDataSet(strSPName , new Hashtable());
				//store the resultant data table
				seasonalNoticeBoardData = dataSet.Tables[0];
				
				
			}
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

			if (seasonalNoticeBoardData.Rows.Count > 0 ) _DataAvailable = true;
			else _DataAvailable = false; 
            

		}

		// blank contructor
		public SeasonalNoticeBoardHandler(bool isBlank)
		{}
		
		/// <summary>
		/// Method to get the data 
		/// </summary>
		public DataTable  GetData()
		{
			DataTable tempSeasonalNoticeBoardData;
			try
			{
				if (DataAvailable) 
				{
					//return (DataTable) SeasonalNoticeBoardData;  					 
					tempSeasonalNoticeBoardData = (DataTable) seasonalNoticeBoardData;  
				}

				else 
				{
					tempSeasonalNoticeBoardData= null;
				}
			}

			catch(TDException tdex)
			{
				string message = "TDError Getting 'GetData' SeasonalNoticeBoardHandler " + tdex.Message.ToString() ;
				OperationalEvent oe = new OperationalEvent(TDEventCategory.Infrastructure,
					TDTraceLevel.Error, message + " "  + "TDException :" + tdex.Message.ToString() );
				tempSeasonalNoticeBoardData= null; 
			}

			

			// returning data
			return tempSeasonalNoticeBoardData;

		}

		/// <summary>
		/// Indicates whether data is available or not?
		/// </summary>
		public bool DataAvailable 
		{
			get
			{
				return _DataAvailable;
			}		

		}

		
		/// <summary>
		/// Returns a complete copy of this object
		/// </summary>		
		public SeasonalNoticeBoardHandler Copy()
		{

			SeasonalNoticeBoardHandler mySeasonalNoticeBoardHandler = new  SeasonalNoticeBoardHandler(true);
			try
			{				
				mySeasonalNoticeBoardHandler.seasonalNoticeBoardData  = this.seasonalNoticeBoardData.Copy();  			
				//return mySeasonalNoticeBoardHandler; 
			}
			catch (TDException tdex)
			{    
				string message = "Error Copying SeasonalNoticeBoardHandler " + tdex.Message;
				OperationalEvent oe = new OperationalEvent(TDEventCategory.Infrastructure,
					TDTraceLevel.Error, message + " "  + "TDException :" + tdex.Message + ":");
				mySeasonalNoticeBoardHandler= null;
				
			}
			
			
			// returning data
			return mySeasonalNoticeBoardHandler;
			
		}

	


	}


}
