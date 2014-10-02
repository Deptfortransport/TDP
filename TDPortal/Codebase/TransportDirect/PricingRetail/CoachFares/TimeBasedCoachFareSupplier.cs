// ***********************************************
// NAME			: TimeBasedCoachFareSupplier.cs
// AUTHOR		: Murat Guney
// DATE CREATED	: 20/10/2005
// DESCRIPTION	: Implementation of the TimeBasedCoachFareSupplier class.
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/CoachFares/TimeBasedCoachFareSupplier.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:36:38   mturner
//Initial revision.
//
//   Rev 1.11   Oct 16 2007 13:51:10   mmodi
//Amended to accept a requestID
//Resolution for 4448: Logging: Request ID is not included when Coach fares request made
//
//   Rev 1.10   Jun 13 2007 15:20:56   mmodi
//Added check for farecode split to prevent error when SCL fare provided
//Resolution for 4450: NX Fares: Scottish citylink fares are not shown
//
//   Rev 1.9   May 25 2007 16:22:14   build
//Automatically merged from branch for stream4401
//
//   Rev 1.8.1.1   May 22 2007 13:26:36   mmodi
//Removed test fares data, and added code to extract Ticket type from fare returned
//Resolution for 4401: DEL 9.6 Stream: National Express New Fares Main Portal
//
//   Rev 1.8.1.0   May 10 2007 16:32:28   asinclair
//Added code for new NX fares.  Also contains dummy results that will need to be removed.
//Resolution for 4401: DEL 9.6 Stream: National Express New Fares Main Portal
//
//   Rev 1.8   Nov 29 2005 20:27:24   mguney
//Changed for Exceptional Fares.
//Resolution for 3230: DN040: Route60 and DayReturn fares should be handled in a different manner.
//
//   Rev 1.7   Nov 03 2005 19:23:22   RPhilpott
//Merge undiscounted and discounted CoachFareData into one.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.6   Nov 02 2005 17:22:58   mguney
//CjpSessionInfo property set for the request.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.5   Nov 02 2005 16:41:02   RPhilpott
//Added CJSessionInfo to PricePricingUnit() method.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.4   Nov 02 2005 09:47:02   mguney
//DiscountCardType checks changed to use length instead of string.Empty.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.3   Nov 02 2005 09:35:10   RPhilpott
//Change PricePricingUnit return type.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.2   Oct 25 2005 11:15:20   mguney
//CoachFareInterfaceFactory usage changed.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.1   Oct 21 2005 10:28:46   mguney
//Associated IR
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.0   Oct 21 2005 10:27:10   mguney
//Initial revision.

using System;
using System.Collections;
using Logger = System.Diagnostics.Trace;
using TransportDirect.Common.DatabaseInfrastructure;
using TransportDirect.Common.Logging;
using TransportDirect.UserPortal.PricingRetail.Logging;
using TransportDirect.UserPortal.PricingRetail.Domain;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.PricingRetail.CoachFareInterfaces;
using CJP =  TransportDirect.JourneyPlanning.CJPInterface;

namespace TransportDirect.UserPortal.PricingRetail.CoachFares
{
	/// <summary>
	/// Summary description for TimeBasedCoachFareSupplier.
	/// </summary>
	public class TimeBasedCoachFareSupplier : ITimeBasedFareSupplier
	{
		private static string NO_DISCOUNT = string.Empty;		

		#region Private methods
		/// <summary>
		/// Prepares the request and returns the result.
		/// </summary>
		/// <param name="pricingUnit"></param>
		/// <returns></returns>
		private FareResult GetFareResult(PricingUnit pricingUnit,CJPSessionInfo sessionInfo, string requestID)
		{
			//prepare the request
			FareRequest fareRequest = new FareRequest();
			fareRequest.OperatorCode = pricingUnit.OperatorCode;
			fareRequest.OriginNaPTAN = ((PublicJourneyDetail)
				(pricingUnit.OutboundLegs[0])).LegStart.Location.NaPTANs[0];
			fareRequest.DestinationNaPTAN = ((PublicJourneyDetail)
				(pricingUnit.OutboundLegs[pricingUnit.OutboundLegs.Count-1])).LegEnd.Location.NaPTANs[0];
			fareRequest.OutwardStartDateTime = ((PublicJourneyDetail)
				(pricingUnit.OutboundLegs[0])).LegStart.DepartureDateTime;
			fareRequest.CjpRequestInfo = sessionInfo;

			fareRequest.RequestID = requestID;
			
			//get the fares interface from the factory
			ICoachFaresInterfaceFactory factory = (ICoachFaresInterfaceFactory)
				TDServiceDiscovery.Current[ServiceDiscoveryKey.CoachFaresInterface];									
			IFaresInterface faresInterface = (IFaresInterface)factory.GetFaresInterface(pricingUnit.OperatorCode);
			//get the result
			FareResult result = faresInterface.GetCoachFares(fareRequest);

			return result;
		}

		/// <summary>
		/// Returns the pricing results in a hashtable after processing for discounts.
		/// </summary>
		/// <param name="fares"></param>
		/// <param name="discounts"></param>
		/// <param name="single">true for single, false for return</param>
		/// <returns>Hashtable of pricing results</returns>
		private Hashtable GetPricingResults(CoachFare[] fares, Discounts discounts, bool single, 
			string operatorCode, bool isDayReturn, bool isNewFares)
		{
			PricingResultsBuilder builder = new PricingResultsBuilder(isDayReturn,true);

			//for each undiscounted single fare, call AddUndiscountedFare
			foreach (CoachFare fare in fares)
			{
				if ((fare.DiscountCardType == null || fare.DiscountCardType.Length == 0) && 
					fare.IsSingle == single)
				{
					builder.AddUndiscountedFare(fare, operatorCode, isNewFares);
				}
			}
			//add the current coach discount card, if any
			if (!discounts.CoachDiscount.Equals(NO_DISCOUNT))
			{
				builder.AddDiscountCard(discounts.CoachDiscount);
				//for each discounted single fare, call AddDiscountedFare
				foreach (CoachFare fare in fares)
				{
					if ((fare.DiscountCardType != null && fare.DiscountCardType.Length != 0) && 
						fare.IsSingle == single)
					{
						builder.AddDiscountedFare(fare, operatorCode, isNewFares);
					}
				}
			}

			return builder.GetPricingResults();
		}
		#endregion

		#region ITimeBasedFareSupplier Members

		/// <summary>
		/// Calculates the fares associated with the pricing unit using the discount information.
		/// </summary>
		/// <param name="pricingUnit"></param>
		/// <param name="discounts"></param>
		/// <returns>The updated PricingUnit</returns>

		public PricingUnit PricePricingUnit(PricingUnit pricingUnit, Discounts discounts, CJPSessionInfo sessionInfo, string requestID)
		{
			if (TDTraceSwitch.TraceVerbose) 
			{
				Logger.Write( new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, 
					PricingRequestMessage.Message(pricingUnit)));
			}

			FareResult fareResult = GetFareResult(pricingUnit,sessionInfo, requestID);
			
			//check for errors. don't log, because the errors are logged during the GetCoachFares process.
			if (fareResult.ErrorStatus == FareErrorStatus.Error)
			{
				return pricingUnit;								
			}

			//prepare the isDayReturn flag. (OutwardDate == ReturnDate)
			bool isDayReturn = false;
			if ((pricingUnit.InboundLegs != null) && (pricingUnit.InboundLegs.Count > 0))
			{
				DateTime inboundStartDate = 
					((PublicJourneyDetail)(pricingUnit.InboundLegs[0])).LegStart.DepartureDateTime.GetDateTime().Date;
				DateTime outwardsStartDate = 
					((PublicJourneyDetail)(pricingUnit.OutboundLegs[0])).LegStart.DepartureDateTime.GetDateTime().Date;

				isDayReturn = (outwardsStartDate == inboundStartDate);
			}

			ICoachFaresLookup coachFaresLookup = (ICoachFaresLookup)
				TDServiceDiscovery.Current[ServiceDiscoveryKey.CoachFaresLookup];
			
			// Check to see if we are using new NX Fares.  If one new fare is found, then we are.  
			bool isNewFares = false;
			int i = -1;
			while (++i < fareResult.Fares.Length)
			{
				//Need to use only the Fare Code from the string and not the whole string
				string fullFareInformation = (fareResult.Fares[i].FareType);
				string[] fareCodeSplit = fullFareInformation.Split('(');
				string justFareCode = string.Empty;
				// Check length because SCL do not include the fare code in the name
				if (fareCodeSplit.Length > 1)
					justFareCode = fareCodeSplit[1].TrimEnd(')');
	
				CoachFaresAction coachFaresAction = coachFaresLookup.GetCoachFaresAction(justFareCode);
				if(coachFaresAction == CoachFaresAction.Include)
				{
					pricingUnit.CoachFaresReturnComponent = true;
					isNewFares = true;
					break;
				}
			}


			Hashtable singleResults = GetPricingResults(fareResult.Fares, discounts, true, 
				pricingUnit.OperatorCode, isDayReturn, isNewFares);
			Hashtable returnResults = GetPricingResults(fareResult.Fares, discounts, false,
				pricingUnit.OperatorCode, isDayReturn, isNewFares);

			// Check that the card exists in the single and return results for safety.
			// If it doesn't assume that we don't have any fares information for that discount card. ie. take the undiscounted fares
			string discountCard = (discounts.CoachDiscount != null && discounts.CoachDiscount.Length != 0) 
				? discounts.CoachDiscount : NO_DISCOUNT;
			
			string singleCard = singleResults.ContainsKey(discountCard) ? discountCard : NO_DISCOUNT;
			string returnCard = returnResults.ContainsKey(discountCard) ? discountCard : NO_DISCOUNT;
			
			// Retrieve the appropriate PricingResults from the collections
			PricingResult unfilteredSingleResults = (PricingResult)singleResults[singleCard];
			PricingResult unfilteredReturnResults = (PricingResult)returnResults[returnCard];
			
			// Filter the fares so that only fares appropriate to the selected journey are returned
			CoachFareFilterAndMergeHelper filterHelper = new CoachFareFilterAndMergeHelper();
			PricingResult filteredSingleResult = filterHelper.FilterFares(unfilteredSingleResults);
			PricingResult filteredReturnResult = filterHelper.FilterFares(unfilteredReturnResults);
			
			// Return the filtered set of fares
			pricingUnit.SetFares(filteredSingleResult,filteredReturnResult);

			if (TDTraceSwitch.TraceVerbose)
			{
				Logger.Write( new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, 
					PricingResponseMessage.Message(pricingUnit)));
			}


			return pricingUnit;
		}

		#endregion
	}
}
