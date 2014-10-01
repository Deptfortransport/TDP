// *********************************************** 
// NAME                 : LookupTranslation.cs
// AUTHOR               : Sanjeev Chand
// DATE CREATED         : 17/01/2005 
// DESCRIPTION			: This component will translate lookup code to plain english. 
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/DepartureBoardService/RTTIManager/LookupTranslation.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:21:36   mturner
//Initial revision.
//
//   Rev 1.0   Feb 28 2005 16:23:04   passuied
//Initial revision.
//
//   Rev 1.6   Jan 21 2005 14:22:36   schand
//Code clean-up and comments has been added
//
//   Rev 1.5   Jan 17 2005 11:43:46   schand
//Service key changed from RTIManager to RTTIlookupHandler


using System;
using System.Collections;
using TransportDirect.Common.DatabaseInfrastructure;
using TransportDirect.Common;
using TransportDirect.Common.Logging;
using Logger = System.Diagnostics.Trace;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.UserPortal.DepartureBoardService;   
using TransportDirect.UserPortal.DepartureBoardService.DepartureBoardFacade;
using TransportDirect.UserPortal.DepartureBoardService.RTTIManager ;

namespace TransportDirect.UserPortal.DepartureBoardService.RTTIManager
{
	/// <summary>
	/// Summary description for LookupTranslation.
	/// This component will translate some of the code to plain english. 
	/// It will use AdditionalDataModule and some lookup tables.
	/// The lookup table are as follows:
	/// 1. TrainOperators.
	/// 2. ReasonCode
	/// 3. ActivityCode
	/// 
	/// </summary>
	public class LookupTranslation:ILookupTranslation
	{	
		RTTILookupHandler lookupHandler; 		

		public LookupTranslation()
		{
			
			// getting discovery data 
			lookupHandler = (RTTILookupHandler) TDServiceDiscovery.Current[ServiceDiscoveryKey.RTTILookupHandler];   
		}
		
		/// <summary>
		/// Gets the operator name from the specified opeartor code.
		/// </summary>
		/// <param name="operatorCode">Operator code</param>		
		/// <returns>operator name from the specified opeartor code </returns>
		public string GetOperatorName(string operatorCode)
		{
			try
			{
				if (lookupHandler == null)
					throw new Exception("LookupTranslation.GetOperatorName: Unable to get RTTILookupHandler object.") ;
				
				return lookupHandler.GetOperatorName(operatorCode);
			}
			catch(Exception ex)
			{
				// log the error 
				string errMessage = "Error occured in LookupTranslation for method GetOperatorName " + " error:  " + ex.Message ;  
				Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, errMessage)) ;
				// return empty string 
				return string.Empty ;
			}
		}
		
		
		
		/// <summary>
		/// Gets the reason description from the specified reason code.
		/// </summary>
		/// <param name="operatorCode">Reason code</param>		
		/// <returns>Reason description from the specified reason code</returns>
		public string GetReasonDescription(string reasonCode)
		{
			try
			{
				if (lookupHandler == null)
					throw new Exception("LookupTranslation.GetReasonDescription: Unable to get RTTILookupHandler object.") ;
				
				return lookupHandler.GetReasonDescription(reasonCode);
			}
			catch(Exception ex)
			{
				// log the error 
				string errMessage = "Error occured in LookupTranslation for method GetReasonDescription " + " error:  " + ex.Message ;  
				Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, errMessage)) ;
				// return empty string 
				return string.Empty ;
			}
		}


	}
}
