//********************************************************************************
//NAME         : AvailabilityRequest.cs
//AUTHOR       : Richard Philpott
//DATE CREATED : 2005-03-18
//DESCRIPTION  : BO input object used to request seat 
//				  availibity from NRS via the RVBO. 
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/RetailBusinessObjects/AvailabilityRequest.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:46:00   mturner
//Initial revision.
//
//   Rev 1.10   Dec 15 2005 20:44:36   RPhilpott
//Changes to correctly handle avilability requests with and without railcards.
//Resolution for 3373: Incorrect display of availablility with railcards
//
//   Rev 1.9   Nov 23 2005 15:46:42   RPhilpott
//Correct handling of discount cards.
//Resolution for 3038: DN040: Double reporting/logging of NRS requests
//
//   Rev 1.8   Nov 18 2005 19:45:52   RPhilpott
//Correct handling of journey origin/destination NLC codes.
//Resolution for 3135: DN040: (CG) Fare availability inconsistent between SBT and SBP
//
//   Rev 1.7   Nov 16 2005 12:17:52   RPhilpott
//Allow for changes in RVBO interface.
//Resolution for 3073: DN040: NRS errors on individual trains cause single RVBO error
//
//   Rev 1.6   Nov 09 2005 12:31:48   build
//Automatically merged from branch for stream2818
//
//   Rev 1.5.1.0   Nov 02 2005 17:33:46   rhopkins
//Refactor construction of Request to accomodate multiple Fares
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.5   Aug 25 2005 14:41:20   RPhilpott
//Pass Retail Train Id to RVBO in place of UID.
//Resolution for 2710: NRS interface -- retail train id needed
//
//   Rev 1.4   Apr 29 2005 20:46:54   RPhilpott
//Correct handling of availability checking for return journeys from pricing time-based searches. 
//Resolution for 2342: Del 7 - PT - Door to Door planner does not respond to unavailable ticket as expected
//
//   Rev 1.3   Apr 12 2005 19:33:52   RPhilpott
//Use fare origin/destination, not original request locations.
//
//   Rev 1.2   Apr 07 2005 20:52:06   RPhilpott
//Corrections to Supplement and Availability handling.
//
//   Rev 1.1   Mar 31 2005 18:44:22   RPhilpott
//Changes to handling of RVBO calls
//
//   Rev 1.0   Mar 22 2005 16:30:40   RPhilpott
//Initial revision.

using System;
using System.Text;
using System.Diagnostics;
using System.Collections;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.UserPortal.PricingMessages;

namespace TransportDirect.UserPortal.RetailBusinessObjects
{
	/// <summary>
	/// BO input object used to request seat availibity from NRS via the RVBO. 
	/// </summary>
	public class AvailabilityRequest : BusinessObjectInput
	{

		private const int INPUT_LENGTH = 5114;
		private const int PRODUCT_LENGTH = 17;
		private bool validRequest = true;

		/// <summary>
		/// Constructor that builds a request from an existing request string
		/// </summary>
		/// <param name="interfaceVersion"></param>
		/// <param name="requestString"></param>
		/// <param name="outputLength"></param>
		public AvailabilityRequest(string interfaceVersion, string requestString, int outputLength) : base("RV", "AV", interfaceVersion )
		{
			if	(requestString.Length > 0)
			{
				HeaderInputParameter inputParameter = new HeaderInputParameter(requestString);
				this.AddInputParameter(inputParameter, 0);

				HeaderOutputParameter outputParameter = new HeaderOutputParameter(outputLength);
				this.AddOutputParameter(outputParameter, 0);
			}
			else
			{
				validRequest = false;
			}
		}


		/// <summary>
		/// Constructor that builds a request from Fare and Train details
		/// </summary>
		/// <param name="interfaceVersion"></param>
		/// <param name="request"></param>
		/// <param name="fare"></param>
		/// <param name="train"></param>
		/// <param name="legCount"></param>
		/// <param name="supplements"></param>
		/// <param name="sleeper"></param>
		/// <param name="outputLength"></param>
		public AvailabilityRequest(string interfaceVersion, PricingRequestDto request, FareDataDto fare, TrainDto train, 
			int legCount, ArrayList supplements, bool sleeper, string originNlc, string destinationNlc, int outputLength) : base("RV", "AV", interfaceVersion )
		{
			if  (outputLength < 0) 
			{
				string msg = "Output Length must be a non-negative number.  Output Length supplied::" + outputLength.ToString();
				TDException tde = new TDException(msg, true, TDExceptionIdentifier.PRHOutputLengthMustBeNonNegative );
				Trace.Write( new OperationalEvent( TDEventCategory.Infrastructure, TDTraceLevel.Error, msg, tde ) );
				throw tde;
			}

			string requestString = this.BuildRequest(request, fare, train, legCount, supplements, sleeper, originNlc, destinationNlc);

			if	(requestString.Length > 0)
			{
				HeaderInputParameter inputParameter = new HeaderInputParameter(requestString);
				this.AddInputParameter(inputParameter, 0);

				HeaderOutputParameter outputParameter = new HeaderOutputParameter(outputLength);
				this.AddOutputParameter(outputParameter, 0);
			}
			else
			{
				validRequest = false;
			}
		}


		private string BuildRequest(PricingRequestDto request, FareDataDto fare, TrainDto train, int legCount, ArrayList supplements, bool sleeper, string originNlc, string destinationNlc) 
		{
			StringBuilder sb = new StringBuilder(INPUT_LENGTH);

			bool returnTrain = (train.ReturnIndicator == ReturnIndicator.Return);
	
			sb.Append(originNlc);
			sb.Append(destinationNlc);
			sb.Append(train.Board.Location.Crs);
			sb.Append(train.Alight.Location.Crs);
			sb.Append(train.Board.Departure.ToString("yyyyMMdd"));
			sb.Append(train.RetailId.PadRight(8, ' '));						

			sb.Append(sleeper ? "B" : "S");								// berth or seat

			string supplementsRequest = BuildRequestForSupplements(fare, supplements, request.JourneyType, legCount, sleeper, returnTrain);

			if (supplementsRequest.Length == 0)
			{
				// if no products found, we don't have a valid string to return
				return String.Empty;
			}
			else
			{
				sb.Append(supplementsRequest);
				sb.Append(' ', INPUT_LENGTH - sb.Length);					// fixed length - pad as necessary 

				return sb.ToString();
			}
		}


		/// <summary>
		/// Append a product request string for each appropriate supplement for the current fare and leg
		/// </summary>
		/// <param name="fare"></param>
		/// <param name="supplements"></param>
		/// <param name="journeyType"></param>
		/// <param name="legCount"></param>
		/// <param name="sleeper"></param>
		/// <param name="returnTrain"></param>
		/// <returns></returns>
		private string BuildRequestForSupplements(FareDataDto fare, ArrayList supplements, JourneyType journeyType, int legCount, bool sleeper, bool returnTrain)
		{
			StringBuilder sb = new StringBuilder(PRODUCT_LENGTH * supplements.Count);

			int productCount = 0;

			bool isWithDiscount = (fare.RailcardCode.Trim().Length > 0);

			foreach (Supplement supplement in supplements)
			{
				if	((returnTrain && !supplement.ReturnDirection)
					|| (!returnTrain && supplement.ReturnDirection))
				{
					continue;
				}

				if	(supplement.LegNumber != legCount)
				{
					continue;
				}
				
				if	(supplement.Chargeable)							// we are only interested in free reservations ...
				{
					continue;
				}

				if	((sleeper && supplement.Berth) || (!sleeper && supplement.Seat))
				{
					
					if	(isWithDiscount)
					{
						sb.Append(AddProduct(fare, supplement.SupplementCode, fare.RailcardCode, journeyType, returnTrain, sleeper));
					}
					else
					{
						sb.Append(AddProduct(fare, supplement.SupplementCode, string.Empty, journeyType, returnTrain, sleeper));
					}
					
					productCount++;
				}
			}

			return (productCount > 0 ? sb.ToString() : string.Empty);	// if no products found, we don't  
																		//  have a valid string to return
		}

		/// <summary>
		/// Adds the string that represents the current product
		/// </summary>
		/// <param name="fare"></param>
		/// <param name="supplement"></param>
		/// <param name="railcard"></param>
		/// <param name="journeyType"></param>
		/// <param name="returnTrain"></param>
		/// <param name="sleeper">true if berth, false if seat</param>
		/// <returns>Product string</returns>
		private string AddProduct(FareDataDto fare, string supplement, string railcard, JourneyType journeyType, bool returnTrain, bool sleeper)
		{
			StringBuilder sb = new StringBuilder(PRODUCT_LENGTH);

			sb.Append(fare.ShortTicketCode.PadRight(3, ' '));
			sb.Append(railcard.PadRight(3, ' '));
			sb.Append(fare.RouteCode.PadRight(5, ' '));
			sb.Append(supplement.PadRight(3, ' '));
			sb.Append("A");											// adult only
			
			if	(journeyType == JourneyType.OutwardSingle || journeyType == JourneyType.InwardSingle)
			{
				sb.Append("S");		// single ticket (outward or return)
			}
			else if (returnTrain)
			{
				sb.Append("R");		// return - inbound leg
			}
			else
			{
				sb.Append("O");		// return - outbound leg
			}

			sb.Append(sleeper ? "B" : "S");								// berth or seat

			return sb.ToString();
		}
		
		public bool RequestToProcess
		{
			get { return validRequest; }
		}

	}
}
