//********************************************************************************
//NAME         : FaresInterfaceForJourney.cs
//AUTHOR       : Murat Guney
//DATE CREATED : 05/10/2005
//DESCRIPTION  : Implementation of FaresInterfaceForJourney class
//				IMPORTANT NOTE: When the Atkins dll is received, the classes exposed in CoachFaresRemotingService.cs
//								like (FaresRequest, FaresResult) are going to be referred from this dll.
//								Probably it is going to be placed in the Externals folder.
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/CoachFareInterfaces/FaresInterfaceForJourney.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:36:30   mturner
//Initial revision.
//
//   Rev 1.19   Jun 13 2007 15:14:56   mmodi
//Added RequestID to cjpRequest
//Resolution for 4448: Logging: Request ID is not included when Coach fares request made
//
//   Rev 1.18   Nov 26 2005 11:49:18   mguney
//Date time in the past case test removed from the validation.
//Resolution for 3213: IF098 Interface Stub: Issues with displaying fares from IF098 stub
//
//   Rev 1.17   Nov 16 2005 09:55:12   mguney
//WaitOne is used instead of WaitAll.
//
//   Rev 1.16   Nov 10 2005 16:13:00   mguney
//Request logging included.
//
//   Rev 1.13.1.0   Nov 10 2005 13:14:38   mguney
//FareRequest is logged before calling the remote method.
//
//   Rev 1.13   Oct 31 2005 09:29:00   mguney
//ErrorStatus is set to Succes  after constructing a result with success.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.12   Oct 29 2005 17:02:28   mguney
//Changed according to the new Atkins dll namespace: FaresProviderInterface
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.11   Oct 21 2005 14:55:12   mguney
//New dll is being used. Details about coachfaresremotinghost removed.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.10   Oct 20 2005 10:44:30   mguney
//Reference error fixed in GetCJPRequest method. 
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.9   Oct 20 2005 08:59:50   mguney
//Operator code of the TDP FareRequest is transferred to CJP FaresRequest operator code after lookup in GetCJPRequest method.
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.8   Oct 19 2005 17:19:26   mguney
//Outward end datetime check omitted. It is not going to be used for IF3132
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.7   Oct 19 2005 16:11:16   mguney
//Error check in CallRemoteFaresMethod changed in accordance with Atkins dll interface.
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.6   Oct 19 2005 09:38:12   mguney
//Atkins FaresRequest and FaresResult objects referred from the external dll.
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.5   Oct 18 2005 16:49:06   mguney
//Changes made for Atkins interface.
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.4   Oct 18 2005 09:13:02   mguney
//Time in the past case handled
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.3   Oct 17 2005 14:29:16   mguney
//CoachFaresRemotingHost reference removed.
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.2   Oct 13 2005 14:53:48   mguney
//Creation date corrected.
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.1   Oct 12 2005 11:03:58   mguney
//Initial draft Associated SCR
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.0   Oct 12 2005 11:01:00   mguney
//Initial revision.

using System;
using System.Globalization;
using System.Text;
using Logger = System.Diagnostics.Trace;
using System.Threading;

using TransportDirect.Common.Logging;
using TransportDirect.UserPortal.PricingRetail.CoachFares;
using TransportDirect.Common.ServiceDiscovery;
using FP = TransportDirect.JourneyPlanning.FaresProvider;
using FPI = TransportDirect.JourneyPlanning.FaresProviderInterface;

namespace TransportDirect.UserPortal.PricingRetail.CoachFareInterfaces
{
	/// <summary>
	/// Fare interface for journey based fares. (IF31/32)
	/// Calls a dll method provided by Atking via remoting.
	/// </summary>
	public class FaresInterfaceForJourney : FaresInterface
	{		
		private delegate FareResult JourneyCoachFareDelegate(FareRequest fareRequest);
		private JourneyCoachFareDelegate JourneyDel;
		private IAsyncResult JourneyDelASR;
		
		/// <summary>
		/// Returns a FareResult with FareErrorStatus.Error and an empty CoachFare array.
		/// </summary>
		/// <returns>Error FareResult.</returns>
		private FareResult GetResultWithError()
		{
			FareResult fareResult = new FareResult();
			fareResult.ErrorStatus = FareErrorStatus.Error;
			fareResult.Fares = new CoachFareForJourney[0];
			return fareResult;			
		}

		/// <summary>
		/// Checks if the request is valid.
		/// </summary>
		/// <param name="fareRequest">Fare request</param>
		/// <returns>true: valid request</returns>
		private bool ValidateRequest(FareRequest fareRequest)
		{
			if ((fareRequest.OriginNaPTAN == null) || (fareRequest.DestinationNaPTAN == null))
				return false;

			if ((fareRequest.OriginNaPTAN.Naptan.Length == 0) ||
				(fareRequest.DestinationNaPTAN.Naptan.Length == 0))
				return false;
			
			if (fareRequest.OutwardStartDateTime.GetDateTime().Equals(DateTime.MinValue))				
				return false;								
			
			//No need to check for return date times, as they are not being used for journey interface.

			return true;
		}

		/// <summary>
		/// Overriden method for getting the coach fares.
		/// </summary>
		/// <param name="fareRequest">The request to be processed.</param>
		/// <returns>FareResult object.</returns>
		public override FareResult GetCoachFares(FareRequest fareRequest)		
		{
			//Validate before sending the request.
			if (!ValidateRequest(fareRequest))
				return GetResultWithError();

			WaitHandle[] wh = new WaitHandle[1];
			wh[0] = InvokeJourneyCoachFareService(fareRequest);								
			((ManualResetEvent)wh[0]).WaitOne(GetTimeout(),false);			

			FareResult result = GetResult();
			return result;
		}

		/// <summary>
		/// Method for calling the remote objects method asynchronously.
		/// </summary>
		/// <param name="fareRequest">Request to be processed.</param>
		/// <returns>The wait handle for waiting for the call to finish.</returns>
		private WaitHandle InvokeJourneyCoachFareService(FareRequest fareRequest)
		{
			//Exception handling not needed as exceptions will occur when calling the EndInvoke.
			JourneyDel = new JourneyCoachFareDelegate(CallRemoteFaresMethod);
			JourneyDelASR = JourneyDel.BeginInvoke(fareRequest,null,null);
			return JourneyDelASR.AsyncWaitHandle;			
		}

		/// <summary>
		/// Transforms the TDP FareRequest to CJPs FaresRequest object.
		/// </summary>
		/// <param name="tdpRequest">tdp's version of FareRequest</param>
		/// <returns>CJP's FaresRequest object</returns>
		private FPI.FaresRequest GetCJPRequest(FareRequest tdpRequest)
		{
			//get the corresponding operator code from the lookup interface
			ICoachOperatorLookup coachOperatorLookup = (ICoachOperatorLookup)
				TDServiceDiscovery.Current[ServiceDiscoveryKey.CoachOperatorLookup];
			CoachOperator coachOperator = coachOperatorLookup.GetOperatorDetails(tdpRequest.OperatorCode);
			string operatorCode = coachOperator.OperatorCode;
			
			FPI.FaresRequest cjpRequest = new FPI.FaresRequest();
			cjpRequest.departureTime = tdpRequest.OutwardStartDateTime.GetDateTime();
			cjpRequest.destinationNaptan = tdpRequest.DestinationNaPTAN.Naptan;
			cjpRequest.faresProviderCode = operatorCode;
			cjpRequest.originNaptan = tdpRequest.OriginNaPTAN.Naptan;
			cjpRequest.referenceTransaction = false;
			cjpRequest.sessionID = tdpRequest.CjpRequestInfo.SessionId;
			cjpRequest.userType = tdpRequest.CjpRequestInfo.UserType;
			cjpRequest.requestID = tdpRequest.RequestID;

			return cjpRequest;

		}

		/// <summary>
		/// Returns the error message received from FaresProvider.dll.
		/// </summary>
		/// <param name="messages"></param>
		/// <returns></returns>
		private string GetErrorMessageText(FPI.Message[] messages)
		{
			StringBuilder sb = new StringBuilder();			
			foreach (FPI.Message message in messages)
			{
				sb.Append(Environment.NewLine + "Code: " + message.code.ToString(CultureInfo.CurrentCulture));
				sb.Append("\tDescription: " + message.description);
				sb.Append("\tExternal Error: " + message.externalError);
			}
			return sb.ToString();
		}

		/// <summary>
		/// Calls the FindFares method using remoting.
		/// </summary>
		/// <param name="fareRequest">Request to be processed</param>
		/// <returns>FareResult for the given request.</returns>
		private FareResult CallRemoteFaresMethod(FareRequest fareRequest)
		{																
			FareResult fareResult = new FareResult();
			FPI.FaresRequest cjpRequest = GetCJPRequest(fareRequest);
			Logger.Write(new OperationalEvent(TDEventCategory.ThirdParty, TDTraceLevel.Verbose, 
				fareRequest.Message()));

			try
			{				
				FP.FaresProvider service = new FP.FaresProvider();
				FPI.FaresResult cjpResult = (FPI.FaresResult)service.Request(cjpRequest);				
				//check if there are any errors.
				if (cjpResult.messages != null)
				{										
					Logger.Write(new OperationalEvent(TDEventCategory.ThirdParty, TDTraceLevel.Error, 
						"Error message returned from FaresProvider.dll:" + GetErrorMessageText(cjpResult.messages)));

					return GetResultWithError();
				}

				if (cjpResult.fares == null)
				{
					fareResult.Fares = new CoachFare[0];
				}
				else
				{
					fareResult.Fares = new CoachFare[cjpResult.fares.Length];
					for (int i=0;i < cjpResult.fares.Length;i++)
					{
						CoachFareForJourney coachFare = new CoachFareForJourney(cjpResult.fares[i],
							fareRequest.OriginNaPTAN,
							fareRequest.DestinationNaPTAN);
						fareResult.Fares[i] = coachFare;
					}
				}
				fareResult.ErrorStatus = FareErrorStatus.Success;
				return fareResult;
			}
			catch (System.Runtime.Remoting.RemotingException ex)
			{
				Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, 
					"Exception while calling the remoting object method." + ex.Message));
				return GetResultWithError();
			}
		}

		/// <summary>
		/// This method is called after the result is received or timeout occured.
		/// </summary>
		/// <returns>FareResult</returns>
		private FareResult GetResult()
		{			
			//Exception handling done in the CallRemoteFaresMethod.
			if (JourneyDelASR.IsCompleted)
			{				
				return (FareResult) JourneyDel.EndInvoke(JourneyDelASR);
			}
			else 
			{
				//TIMEOUT	
				Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, 
					"Timeout occurred while calling the remoting object method."));
				return GetResultWithError();
			}									
		}

		
	}

}