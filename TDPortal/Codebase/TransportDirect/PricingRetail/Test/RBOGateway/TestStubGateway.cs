//********************************************************************************
//NAME         : TestStubGateway.cs
//AUTHOR       : Alistair Caunt
//DATE CREATED : 15/10/2003
//DESCRIPTION  : Implementation of TestStubGateway class
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/Test/RBOGateway/TestStubGateway.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:37:36   mturner
//Initial revision.
//
//   Rev 1.11   Oct 16 2007 13:54:32   mmodi
//Amended to accept a request ID
//Resolution for 4448: Logging: Request ID is not included when Coach fares request made
//
//   Rev 1.10   Mar 06 2007 13:43:48   build
//Automatically merged from branch for stream4358
//
//   Rev 1.9.1.0   Mar 02 2007 11:27:42   asinclair
//Updated to pass a new parameter into the PricingResultDto
//Resolution for 4358: Del 9.x Stream: Improved Rail Fares CCN0354
//
//   Rev 1.9   Nov 09 2005 12:31:44   build
//Automatically merged from branch for stream2818
//
//   Rev 1.8.1.3   Nov 02 2005 16:42:56   RPhilpott
//Added CJSessionInfo to PricePricingUnit() method.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.8.1.2   Nov 02 2005 09:37:48   RPhilpott
//Change PricePricingUnit return type.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.8.1.1   Oct 29 2005 13:56:02   RPhilpott
//Add new interface.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.8.1.0   Oct 28 2005 16:29:32   RPhilpott
//Change PriceRoute() sig for new interface.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.8   Apr 28 2005 18:05:34   RPhilpott
//Split noPlacesAvaialble flag into singles and returns.
//Resolution for 2210: PT - Cosmetic issues in time-based rail fare output.
//
//   Rev 1.7   Apr 28 2005 15:49:36   RPhilpott
//Add "noPlacesAvailable" property to PricingResultsDto.
//Resolution for 2210: PT - Cosmetic issues in time-based rail fare output.
//
//   Rev 1.6   Apr 25 2005 10:12:08   jbroome
//Updated after change to PriceRoute definition
//
//   Rev 1.5   Apr 17 2005 18:17:12   RPhilpott
//No change.
//
//   Rev 1.4   Mar 22 2005 17:13:28   RPhilpott
//Change to IPriceSupplier interface 
//
//   Rev 1.3   Mar 22 2005 16:09:04   RPhilpott
//Addition of cost-based search for Del 7.
//
//   Rev 1.2   Feb 24 2005 09:59:20   jbroome
//Work in progress
//Resolution for 1941: DEV Code Review : Coach Fares Price Supplier
//
//   Rev 1.1   Oct 27 2003 21:19:50   acaunt
//Now returns different single and return values
//
//   Rev 1.0   Oct 23 2003 16:03:16   acaunt
//Initial Revision
using System;
using System.Collections;

using TransportDirect.Common;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.PricingMessages;
using TransportDirect.UserPortal.PricingRetail.Domain;
using TransportDirect.UserPortal.PricingRetail.CoachRoutes;

namespace TransportDirect.UserPortal.PricingRetail.RBOGateway
{
	/// <summary>
	/// Dummy implementation of Gateway
	/// </summary>
	[Serializable]
	public class TestStubGateway : IRoutePriceSupplier, ITimeBasedFareSupplier
	{
		public TestStubGateway()
		{
		}


		/// <summary>
		/// Implementation of IPriceSupplier.Price
		/// </summary>
		/// <param name="pricingUnit"></param>
		/// <param name="discounts"></param>
		public PricingUnit PricePricingUnit(PricingUnit pricingUnit, Discounts discounts, CJPSessionInfo sessionInfo, string requestID)
		{
			TDDateTime outwardDate = new TDDateTime(2005, 3, 23);
			TDDateTime returnDate  = new TDDateTime(2005, 3, 29);

			PricingResultDto resultDto = new PricingResultDto(outwardDate, returnDate, 6,15, "GBP", null, JourneyType.Return, false, false, false);
			PricingResultBuilder builder = new PricingResultBuilder();
			builder.CreatePricingResult(resultDto);
			builder.AddTicketDto(TestSampleJourneyData.SingleDiscountedStandard);
			builder.AddTicketDto(TestSampleJourneyData.SingleUndiscountedStandard);
			PricingResult singles = builder.GetPricingResult();
			builder.CreatePricingResult(resultDto);
			builder.AddTicketDto(TestSampleJourneyData.ReturnDiscountedStandard);
			builder.AddTicketDto(TestSampleJourneyData.ReturnUndiscountedStandard);
			PricingResult returns = builder.GetPricingResult();

			pricingUnit.SetFares(singles, returns);
			return pricingUnit;
		}

		public string[] PriceRoute(ArrayList dates, TDLocation origin, TDLocation destination, Discounts discounts, CJPSessionInfo cjpInfo, string operatorCode, int legNumber, int ticketIndex, QuotaFareList quotaFares)
		{
			return null;
		}

	}
}
