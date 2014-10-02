//********************************************************************************
//NAME         : RestrictFaresMR.cs
//AUTHOR       : Mitesh Modi
//DATE CREATED : 12 Feb 2009
//DESCRIPTION  : Wrapper for processing of RBO MR (multiple fares for many journeys) request.
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/RetailBusinessObjects/RestrictFaresMR.cs-arc  $
//
//   Rev 1.4   Oct 06 2010 12:02:54   apatel
//Code updated to check if minimum fares applies to child fare and corrected the IF statement after code review comments
//Resolution for 5615: IR5613 - Code review actions
//
//   Rev 1.3   Oct 05 2010 10:47:54   apatel
//Code updated to check if the minimum fares applied on the matching journey so valid fares get shown with minimum fares when search by time.
//Resolution for 5613: Fares Issue with Travel Card/Minimum fares applied
//
//   Rev 1.2   Jun 03 2010 09:32:40   mmodi
//New Restrict method to return the Journey validity for a fare
//Resolution for 5538: Fares - RF013 London Victoria to London Bridge shows invalid services for fare
//
//   Rev 1.1   May 06 2010 13:43:30   mmodi
//Amended to drop fares based on the actual journey it is marked invalid for, rather than a drop for all journeys. Still assumes we will only have upto 2 journeys submitted for a request, an Outward (and Return).
//
//Resolution for 5528: Fares - Route Chesterfield fares displayed incorrectly
//
//   Rev 1.0   Feb 18 2009 18:22:02   mmodi
//Initial revision.
//Resolution for 5210: CCN487 - ZPBO Implementation workstream
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

using TransportDirect.Common.Logging;
using TransportDirect.UserPortal.PricingMessages;

namespace TransportDirect.UserPortal.RetailBusinessObjects
{
    /// <summary>
    /// Wrapper for processing of RBO MR (multiple fares for many journeys) request
    /// </summary>
    public class RestrictFaresMR : RestrictFares
    {
        /// <summary>
        /// Restrict Fares using output from RBO method MR
        /// </summary>
        /// <param name="input"></param>
        /// <param name="output"></param>
        public RestrictFaresMR(BusinessObjectInput input, BusinessObjectOutput output)
            : base(input, output)
        {
            stringFunctionID = "MR";
            enumFunctionID = FunctionIDType.MR;

            if (!(input is ValidateFaresForJourneyRequest))
                throw new Exception("Wrong input type passed to RestrictFaresMR in constructor");
        }

        /// <summary>
        /// Restricts the fares
        /// </summary>
        /// <returns></returns>
        public override ArrayList Restrict(ArrayList fares, Fare fare)
        {
            if (output.ErrorSeverity == Messages.ErrorSeverityCritical || output.ErrorSeverity == Messages.ErrorSeverityError)
            {
                fares.Clear();
                return fares;
            }

            // The output can have upto 99 journeys in the result. Determine how many journeys we are dealing with.
            ArrayList outputJourneys = GetOutputJourneys();
            
            // Determine the route codes that were passed to the BO.
            ArrayList routeCodes = GetInputRouteCodes();
                       

            // Arrays to hold the Routes and Fares removed accross the many journeys
            ArrayList invalidRoutes = new ArrayList();
            ArrayList invalidFares = new ArrayList();

            int outputJourneyCount = 0;

            // Go through each journey returned
            foreach (string outputJourney in outputJourneys)
            {
                outputJourneyCount++;

                // Only perform the processing if there is "outputJourney" data, as we can have upto 99 journeys
                // where most of them could be an empty string
                if (!string.IsNullOrEmpty(outputJourney.Trim()))
                {
                    if (TDTraceSwitch.TraceVerbose)
                    {
                        Trace.Write(new OperationalEvent(
                                    TDEventCategory.Infrastructure,
                                    TDTraceLevel.Verbose,
                                    "Processing restriction output - journey: [" + outputJourney + "]."));
                    }

                    int numberOfValidEntries = int.Parse(outputJourney.Substring(101, 3).Trim());

                    bool[] faresDelete = new bool[numberOfValidEntries];

                    #region Detect invalid routes

                    //Determine which routes are invalid
                    int routeStartIndex = 2;

                    for (int j = 0; j < 99; j++)
                    {
                        if ((routeCodes.Count != 0) && (j <= (routeCodes.Count - 1)))
                        {
                            // Anything other than ' ' indicates it is "not valid"
                            if (!outputJourney[j + routeStartIndex].Equals(' '))
                            {
                                if (!invalidRoutes.Contains(routeCodes[j].ToString()))
                                {
                                    invalidRoutes.Add(routeCodes[j].ToString());

                                    if (TDTraceSwitch.TraceVerbose)
                                    {
                                        Trace.Write(new OperationalEvent(
                                                    TDEventCategory.Infrastructure,
                                                    TDTraceLevel.Verbose,
                                                    "Removing invalid route [" + routeCodes[j].ToString() + "]."));
                                    }
                                }
                            }
                        }
                    }

                    #endregion

                    #region Detect invalid fares

                    //Determine which fares are invalid
                    int fareStartIndex = 2 + 99 + 3;

                    for (int k = 0; k < faresDelete.Length; k++)
                    {
                        // Anything other than ' ' indicates it is "not valid"
                        if (!outputJourney[fareStartIndex].Equals(' '))
                        {
                            faresDelete[k] = true;
                        }
                        else
                        {
                            faresDelete[k] = false;
                            // If min-fare indicator is not ' ' it indicates that min - fare applies to the route
                            if (!outputJourney[fareStartIndex + 1].Equals(' '))
                            {
                                Fare currFare = ((Fare)fares[k]);
                                // for adult fare - check minimum fares
                                if (!float.IsNaN(currFare.AdultFare) && !float.IsNaN(currFare.AdultFareMinimum))
                                {
                                    currFare.AdultFare = Math.Max(currFare.AdultFare, currFare.AdultFareMinimum);
                                }

                                // for child fare - check minimum fares
                                if (!float.IsNaN(currFare.ChildFare) && !float.IsNaN(currFare.ChildFareMinimum))
                                {
                                    currFare.ChildFare = Math.Max(currFare.ChildFare, currFare.ChildFareMinimum);
                                }
                            }
                        }

                        fareStartIndex += 2; // Increment fare index by two, because each fare in output contains a "valid" and "min-fare" indicator

                    }

                    //Update the invalid fares array
                    for (int l = faresDelete.Length - 1; l >= 0; l--)
                    {
                        if (faresDelete[l])
                        {
                            if (!invalidFares.Contains((Fare)fares[l]))
                            {
                                // If this is the 2nd journey, then ignore removing the OutwardSingle fares
                                // as these will have been marked valid/invalid by the 1st journey.
                                // This is to allow Single fares on a specific route to remain valid on the outward
                                // journey, given that sometimes the return journey travelling on a different 
                                // route marks it as invalid.
                                // Assumes the MR call only has 1 outward and optional 1 return
                                if (!
                                    ((((Fare)fares[l]).TicketType.Type == JourneyType.OutwardSingle)
                                    && (outputJourneyCount > 1))
                                    )
                                {
                                    invalidFares.Add((Fare)fares[l]);


                                    if (TDTraceSwitch.TraceVerbose)
                                    {
                                        Trace.Write(new OperationalEvent(
                                                TDEventCategory.Infrastructure,
                                                TDTraceLevel.Verbose,
                                                "Dropping restricted fare: " + ((Fare)fares[l]).TicketType.Code
                                                    + " (" + ((Fare)fares[l]).ToString() + " )."));
                                    }
                                }
                            }
                        }
                    }

                    #endregion

                    #region Detect invalid fares due to route

                    if (TDTraceSwitch.TraceVerbose)
                    {
                        Trace.Write(new OperationalEvent(
                                TDEventCategory.Infrastructure,
                                TDTraceLevel.Verbose, "Removing fares due to invalid route."));
                    }

                    foreach (Fare fareToRemove in fares)
                    {
                        if (invalidRoutes.Contains(fareToRemove.Route.Code))
                        {
                            // This fares route code has been identified as invalid
                            if (!invalidFares.Contains(fareToRemove))
                            {
                                // If this is the 2nd journey, then ignore removing the OutwardSingle fares
                                // as these will have been marked valid/invalid by the 1st journey.
                                // This is to allow Single fares on a specific route to remain valid on the outward
                                // journey, given that sometimes the return journey travelling on a different 
                                // route marks it as invalid.
                                // Assumes the MR call only has 1 outward and optional 1 return
                                if (!
                                    ((fareToRemove.TicketType.Type == JourneyType.OutwardSingle)
                                    && (outputJourneyCount > 1))
                                    )
                                {
                                    // Add the fare to invalid array so it is removed.
                                    invalidFares.Add(fareToRemove);

                                    if (TDTraceSwitch.TraceVerbose)
                                    {
                                        Trace.Write(new OperationalEvent(
                                                TDEventCategory.Infrastructure,
                                                TDTraceLevel.Verbose,
                                                "Dropping restricted fare due to invalid route: " + fareToRemove.TicketType.Code
                                                    + " (" + fareToRemove.ToString() + " )."));
                                    }
                                }
                            }
                        }
                    }

                    // Clear out to prevent next loop iteration from repeatedly removing fares
                    invalidRoutes.Clear();
                    #endregion
                }
            }

            //Update the valid fares array to be returned by going through the original fares
            ArrayList validFares = new ArrayList();

            #region Retain valid fares

            foreach (Fare originalFare in fares)
            {
                if (!invalidFares.Contains(originalFare))
                {
                    validFares.Add(originalFare);
                }
            }

            #endregion

            return validFares;
        }

        /// <summary>
        /// Method which returns if the fare passed into the RBO MR call is valid. 
        /// Assumes only 1 fare was passed into the RBO MR call.
        /// This method is called ValidateServiceDetailsForFareAndJourney() for Search By Price
        /// </summary>
        /// <param name="trains"></param>
        /// <returns></returns>
        public JourneyValidity Restrict(ArrayList trains)
        {
            JourneyValidity validity = JourneyValidity.ValidFare;		// default value

            if (output.ErrorSeverity == Messages.ErrorSeverityCritical || output.ErrorSeverity == Messages.ErrorSeverityError)
            {
                return JourneyValidity.InvalidFare;
            }

            // The output can have upto 99 journeys in the result. Should only be dealing with 1 here
            ArrayList outputJourneys = GetOutputJourneys();

            // Determine the route codes that were passed to the BO.
            ArrayList routeCodes = GetInputRouteCodes();


            // Go through each journey returned, should only be 1 journey to process
            foreach (string outputJourney in outputJourneys)
            {                
                // Only perform the processing if there is "outputJourney" data, as we can have upto 99 journeys
                // where most of them could be an empty string
                if (!string.IsNullOrEmpty(outputJourney.Trim()))
                {
                    if (TDTraceSwitch.TraceVerbose)
                    {
                        Trace.Write(new OperationalEvent(
                                    TDEventCategory.Infrastructure,
                                    TDTraceLevel.Verbose,
                                    "Processing restriction output - journey: [" + outputJourney + "]."));
                    }

                    // The route code(s) must be valid for the journey, otherwise fare is invalid
                                        
                    #region Detect invalid routes

                    // Determine which routes are invalid
                    int routeStartIndex = 2;

                    // Should only be 1 route code to process
                    for (int j = 0; j < 99; j++)
                    {
                        if ((routeCodes.Count != 0) && (j <= (routeCodes.Count - 1)))
                        {
                            // Anything other than ' ' indicates it is "not valid"
                            if (!outputJourney[j + routeStartIndex].Equals(' '))
                            {
                                return JourneyValidity.InvalidFare;  // no point in doing any further checking
                            }
                        }
                    }

                    #endregion

                    #region Detect invalid fares

                    // Should only be 1 fare to process
                    int numberOfValidEntries = int.Parse(outputJourney.Substring(101, 3).Trim()); // FARES-COUNT
                                        
                    // Determine which fares are invalid
                    int fareStartIndex = 2 + 99 + 3;

                    for (int k = 0; k < numberOfValidEntries; k++)
                    {
                        // Allow a Quota controlled restriction to pass through
                        if (outputJourney[fareStartIndex].Equals('Q'))
                        {
                            for (int l = 0; l < trains.Count; l++)
                            {
                                // Mark all trains as Quota controlled
                                ((TrainDto)trains[l]).QuotaControlledFare = true;
                            }
                        }
                        // Else, anything other than ' ' indicates it is "not valid"
                        else if (!outputJourney[fareStartIndex].Equals(' '))
                        {
                            return JourneyValidity.InvalidFare;
                        }

                        if (outputJourney[fareStartIndex + 1].Equals('M'))
                        {
                            validity = JourneyValidity.MinimumFareApplies;
                        }
                        
                        fareStartIndex += 2; // Increment fare index by two, because each fare in output contains a "valid" and "min-fare" indicator
                    }

                    break; // Exit the loop, should only ever be 1 journey to process
                   
                    #endregion
                }
            }

            return validity;
        }

        public override BusinessObjectOutput FilterOutput(PricingRequestDto request)
        {
            return output;
        }

        #region Private methods 

        /// <summary>
        /// Method to break up the Output result in to the separate journeys is consists of
        /// </summary>
        /// <param name="output"></param>
        /// <returns></returns>
        private ArrayList GetOutputJourneys()
        {
            // The output can have upto 99 journeys in the result. Determine how many journeys we are dealing with.
            int outputJourneyLength = 704;

            ArrayList outputJourneys = new ArrayList();
            string outputBody = output.OutputBody;
            int outputJourneyStartIndex = 0;

            bool done = false;
            while (!done)
            {
                if (string.IsNullOrEmpty(outputBody))
                {
                    done = true;
                }
                else if (outputBody.Length < outputJourneyLength)
                {
                    string message = "Unexpected output length from RBO from MR method call, OutputBody length is: " + output.OutputBody.Length.ToString() + ", Expected: " + input.OutputLength.ToString();

                    // Unexpected output length, log error throw exception
                    Trace.Write(new OperationalEvent(
                            TDEventCategory.FaresProvider,
                            TDTraceLevel.Error,
                            message));

                    throw new Exception(message);
                }
                else
                {
                    // Take out a journey
                    outputJourneys.Add(outputBody.Substring(outputJourneyStartIndex, outputJourneyLength));

                    // Remove the journey from the output
                    outputBody = outputBody.Remove(outputJourneyStartIndex, outputJourneyLength);
                }
            }

            return outputJourneys;
        }

        /// <summary>
        /// Method to get the Route codes passed in to the RBO call
        /// </summary>
        /// <returns></returns>
        private ArrayList GetInputRouteCodes()
        {
            // Need to get it from the GL ouput/MR input in case it has changed the order of the Route codes
            // originally passed in

            int routesStartIndex = 43002;
            int routesLength = 4554;

            //ROUTE-CODE, ROUTE-X-LONDON, ROUTE-INC-EXC-CRS, ROUTE-CRS, INC-EXC-IND
            string routeCodesOutput = input.InputBody.Substring(routesStartIndex, routesLength);

            ArrayList routeCodes = new ArrayList();

            int routeCodeStart = 0;
            int routeCodeLength = 5;
            int routeCodeIncrement = 41;

            for (int i = 0; i < 99; i++)
            {
                if (routeCodeStart < routeCodesOutput.Length)
                {
                    string routeCode = routeCodesOutput.Substring(routeCodeStart, routeCodeLength);

                    if (!string.IsNullOrEmpty(routeCode.Trim()))
                    {
                        routeCodes.Add(routeCode);
                    }

                    routeCodeStart = routeCodeStart + routeCodeLength + routeCodeIncrement;
                }
            }

            return routeCodes;
        }

        #endregion
    }
}
