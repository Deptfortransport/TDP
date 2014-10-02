//********************************************************************************
//NAME         : FaresInterfaceForRoute.cs
//AUTHOR       : Murat Guney
//DATE CREATED : 05/10/2005
//DESCRIPTION  : Implementation of FaresInterfaceForRoute class.
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/CoachFareInterfaces/FaresInterfaceForRoute.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:36:30   mturner
//Initial revision.
//
//   Rev 1.10   Nov 26 2005 11:47:52   mguney
//Return date time validation removed as there may not be return date time.
//Resolution for 3213: IF098 Interface Stub: Issues with displaying fares from IF098 stub
//
//   Rev 1.9   Nov 01 2005 17:22:50   mguney
//coachOperator check conditions improved.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.8   Oct 22 2005 16:19:06   mguney
//More logging added.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.7   Oct 18 2005 09:15:08   mguney
//ValidateRequest method made private.
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.6   Oct 18 2005 09:13:00   mguney
//Time in the past case handled
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.5   Oct 14 2005 09:56:54   mguney
//FareProviderURL changed to FareProviderUrl
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.4   Oct 13 2005 14:54:56   mguney
//Creation date corrected.
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.3   Oct 13 2005 09:38:56   mguney
//ICoachOperatorLookup is used to bet the web service url.
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.2   Oct 12 2005 14:16:18   mguney
//Comments added.
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.1   Oct 12 2005 11:04:02   mguney
//Initial draft Associated SCR
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.0   Oct 12 2005 11:01:02   mguney
//Initial revision.

using System;
using System.Net;
using Logger = System.Diagnostics.Trace;

using TransportDirect.Common.Logging;
using TransportDirect.UserPortal.PricingRetail.CoachFares;
using TransportDirect.Common.ServiceDiscovery;


namespace TransportDirect.UserPortal.PricingRetail.CoachFareInterfaces
{
	/// <summary>
	/// Fare interface for journey based fares. (IF98)
	/// Calls a web services method provided by coach fare providers.
	/// </summary>
	public class FaresInterfaceForRoute	: FaresInterface
	{
		
		/// <summary>
		/// Generates the request id to be passed to the web service method.
		/// </summary>
		/// <returns></returns>
		private string GenerateRequestId()
		{
			return Guid.NewGuid().ToString();
		}

		/// <summary>
		/// Returns a FareResult with FareErrorStatus.Error and an empty CoachFare array.
		/// </summary>
		/// <returns>Error FareResult.</returns>
		private FareResult GetResultWithError()
		{
			FareResult fareResult = new FareResult();
			fareResult.ErrorStatus = FareErrorStatus.Error;
			fareResult.Fares = new CoachFareForRoute[0];
			return fareResult;			
		}

		/// <summary>
		/// Transforms the given FareResultRemote object to a FareResult object.
		/// If the given FareResultRemote object is null, returns a FareResult with
		/// ErrorStatus=FareErrorStatus.Error and an empty array of Fares.
		/// </summary>
		/// <param name="remoteFareResult">Given RemoteFareResult.</param>
		/// <returns>FareResult</returns>
		private FareResult GetFareResult(FareResultRemote remoteFareResult)
		{
			FareResult fareResult = new FareResult();

			//no results, return error fare result
			if (remoteFareResult == null)
			{					
				return GetResultWithError();			
			}

			//Construct FareResult from FareResultRemote
			fareResult.RequestId = remoteFareResult.RequestId;
			fareResult.ErrorStatus = FareErrorStatus.Success;
			fareResult.Fares = new CoachFareForRoute[remoteFareResult.Fares.Length];
			for (int i=0;i < remoteFareResult.Fares.Length;i++)
				fareResult.Fares[i] = new CoachFareForRoute(remoteFareResult.Fares[i]);

			return fareResult;
		}		

		/// <summary>
		/// Checks if the request is valid.
		/// </summary>
		/// <param name="fareRequest">Fare request</param>
		/// <returns>true: valid request</returns>
		private bool ValidateRequest(FareRequest fareRequest)
		{			
			if (fareRequest.OperatorCode.Length == 0)
				return false;

			if ((fareRequest.OriginNaPTAN == null) || (fareRequest.DestinationNaPTAN == null))
				return false;

			if ((fareRequest.OriginNaPTAN.Naptan.Length == 0) ||
				(fareRequest.DestinationNaPTAN.Naptan.Length == 0))
				return false;
			
			if ((fareRequest.OutwardStartDateTime.GetDateTime().CompareTo(DateTime.MinValue) == 0) ||
				(fareRequest.OutwardEndDateTime.GetDateTime().CompareTo(DateTime.MinValue) == 0))
				return false;

			if (fareRequest.OutwardStartDateTime > fareRequest.OutwardEndDateTime)
				return false;			

			/*if ((fareRequest.OutwardStartDateTime.GetDateTime().CompareTo(DateTime.MinValue) > 0)
				&& (fareRequest.OutwardStartDateTime.GetDateTime().CompareTo(DateTime.Now) < 0))
				return false;

			if ((fareRequest.OutwardEndDateTime.GetDateTime().CompareTo(DateTime.MinValue) > 0)
				&& (fareRequest.OutwardEndDateTime.GetDateTime().CompareTo(DateTime.Now) < 0))
				return false;*/

			return true;
		}

		/// <summary>
		/// Implementation of the IFaresInterface for the web services (IF98).
		/// </summary>
		/// <param name="fareRequest">Request</param>
		/// <returns>FareResult for the given request.</returns>
		public override FareResult GetCoachFares(FareRequest fareRequest)
		{			
			if (!ValidateRequest(fareRequest))
			{
				Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, 
					"Invalid fare request."));
				return GetResultWithError();
			}

			Logger.Write(new OperationalEvent(TDEventCategory.ThirdParty, TDTraceLevel.Verbose, 
				fareRequest.Message()));

			CoachFaresService coachFareService = new CoachFaresService();
			//set the timeout			
			coachFareService.Timeout = GetTimeout();						
			//set the service url according to the provider	
			ICoachOperatorLookup coachOperatorLookup = (ICoachOperatorLookup)
				TDServiceDiscovery.Current[ServiceDiscoveryKey.CoachOperatorLookup];									
			CoachOperator coachOperator = coachOperatorLookup.GetOperatorDetails(fareRequest.OperatorCode);
			if (coachOperator == null || coachOperator.FareProviderUrl == null || 
				coachOperator.FareProviderUrl.Length == 0)
			{
				Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, 
					"Couldn't get the operator details."));
				return GetResultWithError();
			}
			coachFareService.Url = coachOperator.FareProviderUrl;			
	
			DateTime returnStartDateTime = (fareRequest.ReturnStartDateTime == null) ? DateTime.MinValue :
				fareRequest.ReturnStartDateTime.GetDateTime();

			DateTime returnEndDateTime = (fareRequest.ReturnEndDateTime == null) ? DateTime.MinValue :
				fareRequest.ReturnEndDateTime.GetDateTime();

			try
			{
				string requestId = GenerateRequestId();
				//Call the GetCoachFares web method using properties of the request.
				FareResultRemote remoteFareResult = coachFareService.GetCoachFares(
					requestId,fareRequest.OriginNaPTAN.Naptan,fareRequest.DestinationNaPTAN.Naptan,
					fareRequest.OutwardStartDateTime.GetDateTime(),fareRequest.OutwardEndDateTime.GetDateTime(),
					returnStartDateTime,returnEndDateTime);
				
				if (remoteFareResult == null)
				{
					Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, 
						"Invalid fare result received."));
					return GetResultWithError();
				}
				return GetFareResult(remoteFareResult);
			}
			catch (WebException webEx)
			{				
				//TIMEOUT OR OTHER ERROR
				Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, 
					"Exception occurred while calling the coach fares web services method." + webEx.Message));
				return GetResultWithError();					
			}			
			
		}

	}
}