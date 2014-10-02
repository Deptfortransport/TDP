//********************************************************************************
//NAME         : MultipleAvailabilityRequest.cs
//AUTHOR       : Richard Hopkins
//DATE CREATED : 2005-10-25
//DESCRIPTION  : Object used to create and manage a collection
//				of AvailabilityRequest objects 
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/RetailBusinessObjects/MultipleAvailabilityRequest.cs-arc  $
//
//   Rev 1.3   Oct 08 2012 15:38:34   mmodi
//Detect chargeable sleeper supplement scenario and set fare as unavailable
//Resolution for 5856: Fares - Sleeper fare ticket shown when it is not available
//
//   Rev 1.2   Oct 04 2012 13:36:50   mmodi
//Perform availability check for chargeable sleeper reservation, and added extra logging
//Resolution for 5856: Fares - Sleeper fare ticket shown when it is not available
//
//   Rev 1.1   Apr 15 2010 15:35:38   mmodi
//Fixed fare availability (reservations) logic to correctly mark Sleeper fares as available when the rvbo says they are, and similar for seated sleeper fares.
//Resolution for 5509: Fares - RF 035 Find a Train - Sleeper Services Not Offering Full Range of Fares
//
//   Rev 1.0   Nov 08 2007 12:46:12   mturner
//Initial revision.
//
//   Rev 1.4   Dec 15 2005 20:44:36   RPhilpott
//Changes to correctly handle avilability requests with and without railcards.
//Resolution for 3373: Incorrect display of availablility with railcards
//
//   Rev 1.3   Nov 23 2005 15:53:10   RPhilpott
//Fix incorrect availability reporting of inward single and return fares by excluding irrelevant fares from all processing (returns) and from NRS queries (inward singles on outward legs). 
//Resolution for 3101: DN039 - NRS - Single Fares in Return Journeys
//
//   Rev 1.2   Nov 16 2005 12:17:50   RPhilpott
//Allow for changes in RVBO interface.
//Resolution for 3073: DN040: NRS errors on individual trains cause single RVBO error
//
//   Rev 1.1   Nov 11 2005 17:37:56   RWilby
//Updated for NRS compliance enhancement
//Resolution for 3003: NRS enhancement for non-compulsory reservations
//
//   Rev 1.0   Nov 02 2005 16:58:32   rhopkins
//Initial revision.
//

using System;
using System.Collections;
using System.Diagnostics;
using System.Text;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.UserPortal.PricingMessages;

namespace TransportDirect.UserPortal.RetailBusinessObjects
{
	/// <summary>
	/// Class that builds a collection of AvvailabilityRequests.
	/// </summary>
	public class MultipleAvailabilityRequest
    {
        #region Private members

        private const int INPUT_LENGTH = 5114;
		private const int PRODUCT_LENGTH = 17;
		private const int PRODUCT_COUNT = 299;

		private ArrayList availabilityRequests = new ArrayList();
		private bool validRequest = true;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor. Builds a collection of AvailabilityRequests.
        /// </summary>
        /// <param name="interfaceVersion"></param>
        /// <param name="request"></param>
        /// <param name="originNlc"></param>
        /// <param name="destinationNlc"></param>
        /// <param name="fareAvailabilities"></param>
        /// <param name="faresProcessed"></param>
        /// <param name="train"></param>
        /// <param name="legCount"></param>
        /// <param name="sleeper"></param>
        /// <param name="outputLength"></param>
        /// <param name="outward"></param>
        /// <param name="mandatoryReservations"></param>
        /// <param name="errors"></param>
		public MultipleAvailabilityRequest(string interfaceVersion, PricingRequestDto request, string originNlc, string destinationNlc, 
            ref Hashtable fareAvailabilities, ref ArrayList faresNotProcessed, TrainDto train, 
			int legCount, bool sleeper, int outputLength, bool outward, 
            Hashtable mandatoryReservations,ref ArrayList errors)
		{
			if  (outputLength < 0) 
			{
				string msg = "Output Length must be a non-negative number.  Output Length supplied::" + outputLength.ToString();
				TDException tde = new TDException(msg, true, TDExceptionIdentifier.PRHOutputLengthMustBeNonNegative );
				Trace.Write( new OperationalEvent( TDEventCategory.Infrastructure, TDTraceLevel.Error, msg, tde ) );
				throw tde;
			}

			BuildRequests(interfaceVersion, request, originNlc, destinationNlc, ref fareAvailabilities, ref faresNotProcessed, train, legCount, sleeper, outputLength,outward, mandatoryReservations,ref errors);
        }

        #endregion

        #region Public properties

        /// <summary>
		/// Get/Set the collection of AvailabilityRequests
		/// </summary>
		public ArrayList Item
		{
			get { return availabilityRequests; }
			set { this.availabilityRequests = value; }
        }

        /// <summary>
        /// Read only. Indicates if this availability request if valid
        /// </summary>
        public bool RequestToProcess
        {
            get { return validRequest; }
        }

        #endregion

        #region Private methods

        /// <summary>
		/// Build the array of requests
		/// </summary>
		/// <param name="interfaceVersion"></param>
		/// <param name="request"></param>
		/// <param name="originNlc"></param>
		/// <param name="destinationNlc"></param>
		/// <param name="fareAvailabilities"></param>
        /// <param name="faresProcessed"></param>
		/// <param name="train"></param>
		/// <param name="legCount"></param>
		/// <param name="sleeper"></param>
		/// <param name="outputLength"></param>
		private void BuildRequests(string interfaceVersion, PricingRequestDto request, string originNlc, string destinationNlc,
			ref Hashtable fareAvailabilities, ref ArrayList faresNotProcessed, TrainDto train, int legCount, bool sleeper, int outputLength, bool outward, Hashtable mandatoryReservations, ref ArrayList errors)
		{
			String supplementsRequest;
			FareAvailabilityCombined currentFareAvailabilityCombined;
			int productCount = 0;
			int supplementCount = 0;

			bool returnTrain = (train.ReturnIndicator == ReturnIndicator.Return);

			StringBuilder requestString = StartNewRequest(returnTrain, originNlc, destinationNlc, train, sleeper);

            if (TDTraceSwitch.TraceVerbose)
            {
                Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose,
                    string.Format("Creating availability request for [{0}]", requestString.ToString())));
            }

            if (TDTraceSwitch.TraceVerbose)
            {
                Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose,
                    string.Format("Fare availabilities to be checked and requests built count[{0}]", fareAvailabilities.Keys.Count)));
            }

            foreach (string fareKey in fareAvailabilities.Keys)
            {
                if (TDTraceSwitch.TraceVerbose)
                {
                    Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose,
                        string.Format("Checking fare availability request for FareKey [{0}]", fareKey)));
                }

                currentFareAvailabilityCombined = (FareAvailabilityCombined)fareAvailabilities[fareKey];

                MandatoryReservationFlag mandResFlag;

                //FareWithoutDiscount and FareWithoutDiscount have the same ticket type
                //but either one could potentially be null

                FareAvailability fareAvailability;

                if (currentFareAvailabilityCombined.FareWithoutDiscount != null)
                {
                    fareAvailability = currentFareAvailabilityCombined.FareWithoutDiscount;
                }
                else if (currentFareAvailabilityCombined.FareWithDiscount != null)
                {
                    fareAvailability = currentFareAvailabilityCombined.FareWithDiscount;
                }
                else
                {
                    fareAvailability = null;
                }

                if (fareAvailability != null)
                {
                    if (mandatoryReservations.ContainsKey(fareAvailability.FareDto.ShortTicketCode))
                    {
                        //Get mandatory reservation for ticket type
                        mandResFlag = (MandatoryReservationFlag)mandatoryReservations[fareAvailability.FareDto.ShortTicketCode];

                        //Reservation check not required if:
                        if (mandResFlag == MandatoryReservationFlag.NotRequired
                            || (mandResFlag == MandatoryReservationFlag.OutwardOnly && !outward)
                            || mandResFlag == MandatoryReservationFlag.ReturnOnly && outward)
                        {
                            // Record whether previous request was successful and reset internal flags
                            ConsolidateFareAvailabilityResult(currentFareAvailabilityCombined, ref faresNotProcessed, fareKey, outward);

                            //Set availability to true for ticket
                            currentFareAvailabilityCombined.DiscountedOutwardPlacesAvailable = true;
                            currentFareAvailabilityCombined.UndiscountedOutwardPlacesAvailable = true;
                            currentFareAvailabilityCombined.DiscountedInwardPlacesAvailable = true;
                            currentFareAvailabilityCombined.UndiscountedInwardPlacesAvailable = true;
                            currentFareAvailabilityCombined.OutwardResultFound = true;
                            currentFareAvailabilityCombined.InwardResultFound = true;

                            if (TDTraceSwitch.TraceVerbose)
                            {
                                Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose,
                                    string.Format("Availability check not required for fare [{0}]. Mandatory reservation flag[{1}] Outward[{2}]",
                                        fareKey, mandResFlag, outward)));
                            }

                            continue; //No check required. Skip to next foreach.
                        }

                        // Reservation check not required for single fares on inbound  
                        // services if we are processing an outbound request ... 
                        if (request.JourneyType == JourneyType.Return || request.JourneyType == JourneyType.OutwardSingle)
                        {
                            if (!(fareAvailability.FareDto.IsReturn))
                            {
                                if (train.ReturnIndicator == ReturnIndicator.Return)
                                {
                                    // Record whether previous request was successful and reset internal flags
                                    ConsolidateFareAvailabilityResult(currentFareAvailabilityCombined, ref faresNotProcessed, fareKey, outward);

                                    currentFareAvailabilityCombined.DiscountedInwardPlacesAvailable = true;
                                    currentFareAvailabilityCombined.UndiscountedInwardPlacesAvailable = true;
                                    currentFareAvailabilityCombined.InwardResultFound = true;

                                    if (TDTraceSwitch.TraceVerbose)
                                    {
                                        Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose,
                                            string.Format("Availability check not required for fare [{0}]. Journey type[{1}] Train return indicator[{2}]",
                                                fareKey, request.JourneyType, train.ReturnIndicator)));
                                    }

                                    continue; //No check required. Skip to next foreach.
                                }
                            }
                        }

                        if (TDTraceSwitch.TraceVerbose)
                        {
                            Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose,
                                string.Format("Availability check is required for fare [{0}]. Mandatory reservation flag[{1}] Outward[{2}]",
                                    fareKey, mandResFlag, outward)));
                        }
                    }
                    else
                    {
                        // Record whether previous request was successful and reset internal flags
                        ConsolidateFareAvailabilityResult(currentFareAvailabilityCombined, ref faresNotProcessed, fareKey, outward);

                        //Set availability to true for ticket
                        currentFareAvailabilityCombined.UndiscountedOutwardPlacesAvailable = true;
                        currentFareAvailabilityCombined.DiscountedOutwardPlacesAvailable = true;
                        currentFareAvailabilityCombined.UndiscountedInwardPlacesAvailable = true;
                        currentFareAvailabilityCombined.DiscountedInwardPlacesAvailable = true;
                        currentFareAvailabilityCombined.OutwardResultFound = true;
                        currentFareAvailabilityCombined.InwardResultFound = true;

                        //if the ticket code cannot be found
                        errors.Add(RetailBusinessObjectsFacade.NRS_UNAVAILABLE);

                        if (TDTraceSwitch.TraceVerbose)
                        {
                            Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose,
                                string.Format("Availability check not required for fare [{0}]. Fare not found in mandatoryReservations list",
                                    fareAvailability.FareDto.ShortTicketCode)));
                        }

                        continue;//Don't check availabitily. Skip to next foreach.
                    }
                }
                else
                {
                    if (TDTraceSwitch.TraceVerbose)
                    {
                        Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose,
                            string.Format("Fare availability is null for FareKey [{0}]", fareKey)));
                    }
                }

                supplementsRequest = BuildRequestForOneFare(currentFareAvailabilityCombined.FareWithoutDiscount, request.JourneyType, legCount, sleeper, returnTrain)
                    + BuildRequestForOneFare(currentFareAvailabilityCombined.FareWithDiscount, request.JourneyType, legCount, sleeper, returnTrain);

                if (TDTraceSwitch.TraceVerbose)
                {
                    Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose,
                        string.Format("Availability request string for fare [{0}] is [{1}]",
                            fareKey, supplementsRequest)));
                }
                
                supplementCount = supplementsRequest.Length / PRODUCT_LENGTH;

                if ((supplementCount == 0) || (supplementCount > PRODUCT_COUNT))
                {
                    // Do not call ConsolidateFareAvailabilityResult(..) because this may be a sleeper call followed by a seat call,
                    // and so may need to processed in the seat call

                    // If there are no supplements added to the availabilty request, then check for
                    // chargeable sleeper reservation supplement scenario - as this could leave the 
                    // fare as available even though an availability check will not be done. This is 
                    // incorrect as this could leave a sleeper fare marked as available and it is not.
                    // e.g. Aberdeen to Stafford, VDS 00452 fare, has a chargeable sleeper supplement
                    // but with zero availability
                    if (supplementCount == 0)
                    {
                        if (HasChargeableSleeperSupplement(fareAvailability, legCount, sleeper, returnTrain))
                        {
                            // Record whether previous request was successful and reset internal flags
                            ConsolidateFareAvailabilityResult(currentFareAvailabilityCombined, ref faresNotProcessed, fareKey, outward);

                            //Set no availability for ticket
                            currentFareAvailabilityCombined.DiscountedOutwardPlacesAvailable = false;
                            currentFareAvailabilityCombined.UndiscountedOutwardPlacesAvailable = false;
                            currentFareAvailabilityCombined.DiscountedInwardPlacesAvailable = false;
                            currentFareAvailabilityCombined.UndiscountedInwardPlacesAvailable = false;
                            currentFareAvailabilityCombined.OutwardResultFound = false;
                            currentFareAvailabilityCombined.InwardResultFound = false;

                            if (TDTraceSwitch.TraceVerbose)
                            {
                                Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose,
                                    string.Format("No availability request string created for fare [{0}]. Chargeable sleeper supplement scenario detected, setting places as unavailable.",
                                        fareKey)));
                            }
                        }
                    }

                    // Either we haven't found any supplements
                    // or the Fare has more supplements than we can handle.
                    continue;
                }
                else
                {
                    // Record whether previous request was successful and reset internal flags
                    ConsolidateFareAvailabilityResult(currentFareAvailabilityCombined, ref faresNotProcessed, fareKey, outward);

                    if ((productCount + supplementCount) > PRODUCT_COUNT)
                    {
                        // This fare is too long to fit within the maximum length when added to
                        // the current request string, so we need to start a new Request

                        // Finish off the previous Request
                        requestString.Append(' ', INPUT_LENGTH - requestString.Length);					// fixed length - pad as necessary
                        availabilityRequests.Add(new AvailabilityRequest(interfaceVersion, requestString.ToString(), outputLength));

                        // Start a new Request
                        productCount = 0;
                        requestString = StartNewRequest(returnTrain, originNlc, destinationNlc, train, sleeper);
                    }

                    productCount += supplementCount;
                    requestString.Append(supplementsRequest);
                }
            }

			if (productCount > 0)
			{
				// Finish off the previous Request
				requestString.Append(' ', INPUT_LENGTH - requestString.Length);					// fixed length - pad as necessary
				availabilityRequests.Add(new AvailabilityRequest(interfaceVersion, requestString.ToString(), outputLength));
			}
			else if (availabilityRequests.Count == 0)
			{
				// We did not find any valid products
				validRequest = false;
			}

			return;
		}
        
		/// <summary>
		/// Creates the header string for the Request
		/// </summary>
		/// <param name="returnTrain"></param>
		/// <param name="originNlc"></param>
		/// <param name="destinationNlc"></param>
		/// <param name="train"></param>
		/// <param name="sleeper"></param>
		/// <returns></returns>
		private StringBuilder StartNewRequest(bool returnTrain, string originNlc, string destinationNlc, TrainDto train, bool sleeper)
		{
			StringBuilder sb = new StringBuilder(INPUT_LENGTH);

			if	(!returnTrain)
			{
				sb.Append(originNlc);
				sb.Append(destinationNlc);
			}
			else
			{
				sb.Append(destinationNlc);
				sb.Append(originNlc);
			}

			sb.Append(train.Board.Location.Crs);
			sb.Append(train.Alight.Location.Crs);
			sb.Append(train.Board.Departure.ToString("yyyyMMdd"));
			sb.Append(train.RetailId.PadRight(8, ' '));
			sb.Append(sleeper ? "B" : "S");								// berth or seat

			return sb;
		}

		/// <summary>
		/// Append a product request string for each appropriate supplement for the current fare and leg
		/// </summary>
		/// <param name="fareAvailability"></param>
		/// <param name="journeyType"></param>
		/// <param name="legCount"></param>
		/// <param name="sleeper"></param>
		/// <param name="returnTrain"></param>
		/// <returns></returns>
		private string BuildRequestForOneFare(FareAvailability fareAvailability, JourneyType journeyType, int legCount, bool sleeper, bool returnTrain)
		{
			if (fareAvailability == null)
			{
				// Nothing to process
				return String.Empty;
			}

            if (TDTraceSwitch.TraceVerbose)
            {
                Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose,
                    string.Format("Building supplement availability request for fare [{0}]",
                        fareAvailability.FareKey)));
            }

			FareDataDto fare = fareAvailability.FareDto;
			ArrayList supplements = fareAvailability.Supplements.SupplementList;

			StringBuilder sb = new StringBuilder(PRODUCT_LENGTH * supplements.Count);

			int productCount = 0;

			foreach (Supplement supplement in supplements)
			{
                if (TDTraceSwitch.TraceVerbose)
                {
                    Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose,
                        string.Format("Checking supplement [{0}]",
                            supplement.ToString())));
                }

				if	((returnTrain && !supplement.ReturnDirection)
					|| (!returnTrain && supplement.ReturnDirection))
				{
                    if (TDTraceSwitch.TraceVerbose)
                    {
                        Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose,
                            string.Format("Supplement direction is not valid for train direction, discarding.")));
                    }

					continue;
				}

				if	(supplement.LegNumber != legCount)
				{
                    if (TDTraceSwitch.TraceVerbose)
                    {
                        Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose,
                            string.Format("Supplement LegNumber does not equal LegCount, discarding.")));
                    }

					continue;
				}

                // we are only interested in free reservations ... 
				if	(supplement.Chargeable)
				{
                    if (TDTraceSwitch.TraceVerbose)
                    {
                        Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose,
                            string.Format("Supplement is chargeable, discarding.")));
                    }

					continue;
				}

                if ((sleeper && supplement.Berth) || (!sleeper && supplement.Seat))
                {
                    if (fareAvailability.IsWithDiscount)
                    {
                        sb.Append(AddProduct(fare, supplement.SupplementCode, fare.RailcardCode, journeyType, returnTrain, sleeper));
                        productCount++;
                    }
                    else
                    {
                        sb.Append(AddProduct(fare, supplement.SupplementCode, string.Empty, journeyType, returnTrain, sleeper));
                        productCount++;
                    }
                }
                else
                {
                    if (TDTraceSwitch.TraceVerbose)
                    {
                        Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose,
                            string.Format("Supplement sleeper berth/seat is not valid, discarding.")));
                    }
                }
			}

			return (productCount > 0 ? sb.ToString() : string.Empty);	// if no products found, we don't  
																		// have a valid string to return
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

        /// <summary>
        /// Returns true if there is a chargeable sleeper supplement
        /// </summary>
        private bool HasChargeableSleeperSupplement(FareAvailability fareAvailability, int legCount, bool sleeper, bool returnTrain)
        {
            if (fareAvailability == null)
            {
                // Nothing to process
                return false;
            }

            ArrayList supplements = fareAvailability.Supplements.SupplementList;

            foreach (Supplement supplement in supplements)
            {
                // Supplement not valid for the train direction, ignore
                if ((returnTrain && !supplement.ReturnDirection)
                    || (!returnTrain && supplement.ReturnDirection))
                {
                    continue;
                }

                // Supplement not valid for leg, ignore
                if (supplement.LegNumber != legCount)
                {
                    continue;
                }

                if (supplement.Chargeable && sleeper && supplement.Berth)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Method which calls the FareAvailabilityCombined.ConsolidateResultOfPreviousRequest() method, and 
        /// removes the fare from the faresNotProcessed list
        /// </summary>
        /// <param name="fareAvailabilityCombined"></param>
        /// <param name="faresNotProcessed"></param>
        private void ConsolidateFareAvailabilityResult(FareAvailabilityCombined fareAvailabilityCombined, ref ArrayList faresNotProcessed, string fareKey, bool outward)
        {
            if (faresNotProcessed.Contains(fareKey))
            {
                if (TDTraceSwitch.TraceVerbose)
                {
                    Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose,
                        string.Format("Consolidating availability check for fare [{0}] with FareAvailabiltyCombined for[{1}]",
                            fareKey, fareAvailabilityCombined.FareKey)));
                }

                fareAvailabilityCombined.ConsolidateResultOfPreviousRequest(outward);

                faresNotProcessed.Remove(fareKey);
            }
        }

        #endregion
	}
}
