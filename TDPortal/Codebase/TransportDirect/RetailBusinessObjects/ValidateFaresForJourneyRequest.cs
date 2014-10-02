//********************************************************************************
//NAME         : ValidateFaresForJourneyRequest.cs
//AUTHOR       : Mitesh Modi
//DATE CREATED : 12 Feb 2009
//DESCRIPTION  : Wrapper for contents of RBO MR (multiple fares for many journeys) request.
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/RetailBusinessObjects/ValidateFaresForJourneyRequest.cs-arc  $
//
//   Rev 1.6   Jun 04 2010 11:04:12   mmodi
//If the Train leg Walk flag is set, then ignore when setting Routing guide flags for fare route codes
//Resolution for 5547: Fares - Balaston to Wedgewood rail replacement bus journey shows no fares
//
//   Rev 1.5   Jun 03 2010 09:33:50   mmodi
//Corrected journey numbering if only a Return journey was supplied
//Resolution for 5538: Fares - RF013 London Victoria to London Bridge shows invalid services for fare
//
//   Rev 1.4   May 06 2010 13:40:56   mmodi
//Corrected the number of journeys (trains) added for the routing guide flags, RG-JOURNEY
//Resolution for 5528: Fares - Route Chesterfield fares displayed incorrectly
//
//   Rev 1.3   Mar 05 2009 16:15:58   mmodi
//Updated to correct the values for ROUTE-CRS 
//Resolution for 5267: Routing Guide - Not London fare shown when journey goes through london
//
//   Rev 1.2   Feb 26 2009 15:02:00   mmodi
//Flag to specify using routing guide validation, and to correctly set up the CRS codes for each train journey
//Resolution for 5239: Routeing Guide - FCC Routed Travelcards
//
//   Rev 1.1   Feb 25 2009 11:29:16   mmodi
//Correctly set the Routing Guide flag only when a train journey has fare route codes
//Resolution for 5256: Routeing Guide - Invalid fare shown for Gretna Green - Glasgow journey
//
//   Rev 1.0   Feb 18 2009 18:21:06   mmodi
//Initial revision.
//Resolution for 5210: CCN487 - ZPBO Implementation workstream
//

using System;
using System.Collections.Generic;
using System.Text;
using TransportDirect.UserPortal.PricingMessages;
using TransportDirect.Common;
using System.Diagnostics;
using TransportDirect.Common.Logging;
using System.Collections;

namespace TransportDirect.UserPortal.RetailBusinessObjects
{
    public class ValidateFaresForJourneyRequest : BusinessObjectInput
    {
        /// <summary>
        /// Class to create an input request for the Restriction Business Object - MR method call.
        /// CURRENTLY ONLY IMPLEMENTED FOR ONE (or two) JOURNEY(s) BEING VALIDATED
        /// </summary>
        /// <param name="interfaceVersion">Interface version to the RBO</param>
        /// <param name="outputLength">Allocated length for the output from call</param>
        /// <param name="request">Pricing request containing journey(s)</param>
        /// <param name="fares">Fares found for the journey(s)</param>
        /// <param name="routeListOutput">Output from the RBO GL method call</param>
        public ValidateFaresForJourneyRequest(string interfaceVersion, int outputLength,
            PricingRequestDto request, ArrayList fares,
            String routeListOutput, String originNlc, String destinationNlc, bool useRoutingGuide)
            : base("RE", "MR", interfaceVersion)
        {
            if (outputLength < 0)
            {
                string msg = "Output length must be a non-negative number.  Output length supplied::" + outputLength.ToString();
                TDException tde = new TDException(msg, true, TDExceptionIdentifier.PRHOutputLengthMustBeNonNegative);
                Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, msg, tde));
                throw tde;
            }

            HeaderInputParameter inputParameter = new HeaderInputParameter(this.BuildValidateFaresForJourneyRequest(request, fares, routeListOutput, originNlc, destinationNlc, useRoutingGuide));
            this.AddInputParameter(inputParameter, 0);

            HeaderOutputParameter outputParameter = new HeaderOutputParameter(outputLength);
            this.AddOutputParameter(outputParameter, 0);
        }

        /// <summary>
        /// Method to build the MR input request
        /// </summary>
        /// <returns>Input request as a string</returns>
        protected string BuildValidateFaresForJourneyRequest(PricingRequestDto request, ArrayList fares,
            String routeListOutput, String originNlc, String destinationNlc, bool useRoutingGuide)
        {
            StringBuilder sb = new StringBuilder();

            #region Origin, Destination, Dates

            if (string.IsNullOrEmpty(originNlc))
            {
                sb.Append(request.Origin.Nlc);                          //ORIGIN-NLC
            }
            else
            {
                sb.Append(originNlc);                                   //ORIGIN-NLC
            }

            sb.Append(request.Origin.Crs);                              //ORIGIN-CRS
            sb.Append(" ");                                             //ORIGIN-COUNTRY

            if (string.IsNullOrEmpty(destinationNlc))
            {
                sb.Append(request.Destination.Nlc);                     //DEST-NLC
            }
            else
            {
                sb.Append(destinationNlc);                              //DEST-NLC
            }

            sb.Append(request.Destination.Crs);                         //DEST-CRS

            sb.Append(request.OutwardDate.ToString("yyyyMMdd"));        //OUT-DATE
            sb.Append(request.ReturnDate == null ? "        " : request.ReturnDate.ToString("yyyyMMdd")); //RET-DATE

            // Y stops single tickets being marked as invalid when a a return date is specified
            sb.Append("Y");                                             //IGNORE-SINGLES

            #endregion

            #region Trains
            //The number of train enteries to follow
            sb.Append(request.Trains.Count.ToString().PadLeft(2, '0'));   //TT-TRAIN-COUNT

            string emptyTrain = string.Empty;
            emptyTrain = emptyTrain.PadLeft((38 + 396), ' '); // Train details

            bool previousOutwardJourney = false;

            // ASSUME THERE WILL ONLY EVER BY UP TO TWO JOURNEYS (outward and return) - but potentially with many train legs.
            int journeyNumber = 0;

            for (int i = 0; i < 99; i++)
            {
                if ((request.Trains.Count == 0) || (i >= request.Trains.Count))
                {
                    sb.Append(emptyTrain);
                }
                else
                {
                    #region Set Journey number and outward/return train indicator
                    bool appendTrainDetails;
                    string outwardReturnIndicator = " ";

                    TrainDto train = request.Trains[i] as TrainDto;
                    
                    // ASSUME TRAINS ARE ORDERED FIRST BY ALL OUTWARD TRAIN LEGS AND THEN RETURN TRAIN LEGS.
                    // outbound or return indicator
                    if (Enum.Parse(typeof(ReturnIndicator), train.ReturnIndicator.ToString(), true).Equals(ReturnIndicator.Outbound))
                    {
                        outwardReturnIndicator = "O";
                        appendTrainDetails = true;

                        if (!previousOutwardJourney)
                        {
                            previousOutwardJourney = true;
                            journeyNumber++;
                        }
                    }
                    else if (Enum.Parse(typeof(ReturnIndicator), train.ReturnIndicator.ToString(), true).Equals(ReturnIndicator.Return))
                    {
                        outwardReturnIndicator = "R";
                        appendTrainDetails = true;

                        // Need to update the previous journey flag, so that we can increment the Journey number,
                        // assume outward journey already added so journey number becomes 02, 
                        // or if only a return journey supplied also increment so journey number is 01
                        if ((previousOutwardJourney) || (!previousOutwardJourney && journeyNumber == 0))
                        {
                            previousOutwardJourney = false;
                            journeyNumber++;
                        }
                    }
                    else
                    {
                        outwardReturnIndicator = " ";
                        appendTrainDetails = true;
                    }
                    #endregion

                    if (appendTrainDetails)
                    {
                        sb.Append(journeyNumber.ToString().PadLeft(2, '0'));        //TT-JOURNEY-NO
                        sb.Append(outwardReturnIndicator);                          //TT-OUT-RET-IND
                        sb.Append(train.Uid.PadLeft(6, ' '));                       //TT_UID
                        sb.Append(train.Board.Location.Crs.PadLeft(3, ' '));        //TT-ORIGIN-CRS
                        sb.Append(train.Alight.Location.Crs.PadLeft(3, ' '));       //TT-DEST-CRS
                        sb.Append(train.Board.Departure.Hour.ToString().PadLeft(2, '0') + train.Board.Departure.Minute.ToString().PadLeft(2, '0')); //TT-DEP
                        sb.Append(train.Alight.Arrival.Hour.ToString().PadLeft(2, '0') + train.Alight.Arrival.Minute.ToString().PadLeft(2, '0'));   //TT-ARR
                        sb.Append(train.Destination.Location.Crs.PadLeft(3, ' '));  //TT-TDEST  CRS of train destination
                        sb.Append(train.Destination.Arrival.Hour.ToString().PadLeft(2, '0') + train.Destination.Arrival.Minute.ToString().PadLeft(2, '0')); //TT-TARR Arrival time at train destination
                        sb.Append(train.TrainClass.PadLeft(1, ' '));                //TT-TCLASS
                        sb.Append(train.Sleeper.PadLeft(1, ' '));                   //TT-TSLEEPER Sleeper accommodation code of train
                        sb.Append(train.Tocs[0].Code.PadLeft(2, ' '));              //TT-TTOC TOC code(s) of train operator
                        sb.Append(train.Tocs[1].Code.PadLeft(2, ' '));

                        // Number of locations train passes or stops at (including the origin and destination of the train)
                        int trainStopsCount = 2 + train.IntermediateStops.Length;
                        sb.Append(trainStopsCount.ToString().PadLeft(2, '0'));   //TT-LOC-COUNT

                        for (int j = -1; j < 98; j++)                                //TT-LOCATION NLC code of passing/stopping points passed through
                        {
                            string nlc = string.Empty;

                            if (j == -1)
                            {
                                // Append the train start
                                nlc = train.Board.Location.Nlc;
                            }
                            else if ( j == train.IntermediateStops.Length )
                            {
                                // Append the train end
                                nlc = train.Alight.Location.Nlc;
                            }
                            else if ((train.IntermediateStops.Length == 0) || (j > train.IntermediateStops.Length))
                            {
                                nlc = "    ";
                            }
                            else
                            {
                                nlc = train.IntermediateStops[j].Nlc;
                            }

                            sb.Append(string.IsNullOrEmpty(nlc) ? "    " : nlc);
                        }
                    }
                    else
                    {
                        sb.Append(emptyTrain);
                    }

                }   //if-else
            }

            #endregion

            #region Routes
            
            // Assume the GL output keeps all the routes we passed in for the GL input. 
            // Then we can populate the routes section using the GL output

            string routeCountOutput = routeListOutput.Substring(64, 4);
            routeCountOutput = routeCountOutput.Substring(2,2);
            sb.Append(routeCountOutput);                                      //ROUTE-COUNT

            // Get the Routes details from the GL output.          
            // For interface 0202, the list should start at index 68 for 99 x (6 + (10 x (3 + 1) ) ) chars
            int routesStartIndex = 68;
            int routesLength = 4554;
            
            //ROUTE-CODE, ROUTE-X-LONDON, ROUTE-INC-EXC-CRS, ROUTE-CRS, INC-EXC-IND
            string routeCodesOutput = routeListOutput.Substring(routesStartIndex, routesLength);
            sb.Append(routeCodesOutput);

            // Creat a routeCodes array, used later to index a Fare's route code to the route codes list 
            // output from the GL.
            // Need to get it from the GL ouput in case it has changed the order of the Route codes
            // originally passed in
            ArrayList routeCodes = new ArrayList();

            int routeCodeStart = 0;
            int routeCodeLength = 5;
            int routeCodeIncrement = 41;

            for (int k = 0; k < 99; k++)
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
                        
            #endregion

            #region Fares

            sb.Append(fares.Count.ToString().PadLeft(3, '0'));     //FARES-COUNT

            string emptyFare = string.Empty;
            emptyFare = emptyFare.PadLeft(16, ' '); // Fare details

            for (int k = 0; k < 300; k++)                               //FARES-TICKET
            {
                if ((fares.Count == 0) || (k >= fares.Count))
                {
                    sb.Append(emptyFare);
                }
                else
                {
                    Fare fare = (Fare)fares[k];

                    sb.Append(fare.TicketType.Code.PadLeft(3, ' '));    //TICKET-TYPE
                    sb.Append(fare.RailcardCode.PadLeft(3, ' '));       //DISCOUNT-TYPE
                    sb.Append((routeCodes.IndexOf(fare.Route.Code) + 1).ToString().PadLeft(2, '0'));  //ROUTE-DISP
                    sb.Append(fare.RestrictionCode.PadLeft(2, ' '));    //REST

                    if (fare.TicketType.Type.Equals(JourneyType.OutwardSingle))
                    {
                        sb.Append("S");                                 //REPEAT
                    }
                    else
                    {
                        sb.Append("R");
                    }
                    sb.Append(fare.TicketValidityCode.PadLeft(2, ' ')); //VALIDITY
                    sb.Append("   ");                                   //POV-DAYS      TDP does not provide rovers or seasons, therefore no need to poulate
                }
            }

            #endregion

            #region Routing guide
            // Determine if validation should use routing guide.
            // Use routing guide if at least one train journey contains a fare route code, 
            // otherwise it will use the locations defined in ROUTE-CRS
            bool useRoutingGuideValidation = false;
            foreach (TrainDto train in request.Trains)
            {
                if ((train.FareRouteCodes != null) && (train.FareRouteCodes.Length > 0))
                {
                    useRoutingGuideValidation = true;
                    break;
                }
            }

            // Ensure caller has asked to use routing guide
            useRoutingGuideValidation = (useRoutingGuideValidation && useRoutingGuide);

            sb.Append(useRoutingGuideValidation ? "Y" : "N");         //RG-FLAG

            string emptyRoutingGuide = string.Empty;
            emptyRoutingGuide = emptyRoutingGuide.PadLeft(198, ' ');  // Routing guide details

            // Build up the route valid flags for each outward/return Journey, assume we only ever have
            // one outward (and/or one return).
            // e.g. So for Journey 1 containing 3 trains, we want to produce just ONE set of RG-OUT-VALID/RG-RET-VALID flags 
            // for the trains contained within it, e.g. "Y NN...."
            
            // Group the trains into outward and return
            List<TrainDto> outTrains = new List<TrainDto>();
            List<TrainDto> inTrains = new List<TrainDto>();

            for (int i = 0; i < request.Trains.Count; i++)
            {
                TrainDto train = request.Trains[i] as TrainDto;

                // The route code in the array is valid for this train
                if (train.ReturnIndicator == ReturnIndicator.Outbound)
                {
                    outTrains.Add(train);
                }
                else
                {
                    inTrains.Add(train);
                }
            }
            
            // Up to 99 Journeys routing guide flags are set
            for (int m = 0; m < 99; m++)
            {
                if (outTrains.Count > 0)
                {
                    string validRouteFlags = BuildRoutingGuideFlagsForJourney(outTrains, new List<string>((string[])routeCodes.ToArray(typeof(string))), true);

                    sb.Append(validRouteFlags);

                    // Clear trains to prevent readding on next loop iteration
                    outTrains.Clear();
                }
                else if (inTrains.Count > 0)
                {
                    string validRouteFlags = BuildRoutingGuideFlagsForJourney(inTrains, new List<string>((string[])routeCodes.ToArray(typeof(string))), false);
        
                    sb.Append(validRouteFlags);
                    
                    // Clear trains to prevent readding on next loop iteration
                    inTrains.Clear();
                }
                else
                {
                    // No more journeys to process, append the empty routing guide valid string
                    sb.Append(emptyRoutingGuide);
                }
            }

            #endregion

            #region Route locations

            string emptyRouteLocations = string.Empty;
            emptyRouteLocations = emptyRouteLocations.PadLeft(60, ' ');  // Route location details

            // Get the Visit CRS locations from the GL output
            int visitCRSStart = 4;
            int visitCRSLength = 60;

            string outputRouteCRSs = routeListOutput.Substring(visitCRSStart, visitCRSLength); // Holds complete CRS from GL output
            ArrayList outputRouteCRScodes = new ArrayList(); // Used to hold the different CRS codes from the GL output

            int startIndex = 0;

            // Extract the individual CRS codes into an array to make them usable
            bool done = false;
            while (!done)
            {
                if (string.IsNullOrEmpty(outputRouteCRSs))
                {
                    done = true;
                }
                else
                {
                    // Take out a CRS
                    string crs = outputRouteCRSs.Substring(startIndex, 3);

                    // Add to the array
                    if (!string.IsNullOrEmpty(crs))
                    {
                        outputRouteCRScodes.Add(crs);
                    }

                    // Remove the CRS from the output
                    outputRouteCRSs = outputRouteCRSs.Remove(startIndex, 3);
                }
            }

            // ASSUME THERE WILL ONLY EVER BY UP TO TWO JOURNEYS (outward and return) - but potentially with many train legs
            ArrayList outwardTrains = new ArrayList();
            ArrayList inwardTrains = new ArrayList();

            foreach (TrainDto train in request.Trains)
            {
                //outbound or return indicator                        
                if (Enum.Parse(typeof(ReturnIndicator), train.ReturnIndicator.ToString(), true).Equals(ReturnIndicator.Outbound))
                {
                    outwardTrains.Add(train);
                }
                else if (Enum.Parse(typeof(ReturnIndicator), train.ReturnIndicator.ToString(), true).Equals(ReturnIndicator.Return))
                {
                    inwardTrains.Add(train);
                }
            }

            // Build up the CRS in order they are visited by each journey (using all the train legs applicable to that journey)
            ArrayList trainRouteCRS = new ArrayList();

            // Add the CRS string for the outward journey to the array
            trainRouteCRS.Add(BuildRouteCRSForTrains(outwardTrains, outputRouteCRScodes));
            
            // Add the CRS string for the return journey to the array
            trainRouteCRS.Add(BuildRouteCRSForTrains(inwardTrains, outputRouteCRScodes));
            


            // Now add each journeys CRS route string to the MR input
            for (int t = 0; t < 99; t++)                                                    //ROUTE-LOCATIONS
            {
                if (t >= 2)
                {
                    sb.Append(emptyRouteLocations);
                }
                else
                {
                    sb.Append(trainRouteCRS[t]);                                            //ROUTE-CRS
                }
            }
            #endregion

            return sb.ToString();
        }


        /// <summary>
        /// Builds up the journey route CRS codes for the trains supplied, only if the CRS code is in the 
        /// list of CRS codes supplied
        /// </summary>
        /// <param name="trains"></param>
        /// <param name="outputRouteCRScodes"></param>
        /// <returns></returns>
        private string BuildRouteCRSForTrains(ArrayList trains, ArrayList outputRouteCRScodes)
        {
            string inputRouteCRSs = string.Empty;             // Holds the applicable CRS to pass into MR input
            ArrayList inputRouteCRScodes = new ArrayList();   // Used to ensure duplicate CRS not added to MR crs input string
            StringBuilder tempRouteCRS = new StringBuilder(); // Temp string to build up the CRS for all train legs

            for (int r = 0; r < trains.Count; r++)
            {
                TrainDto train = trains[r] as TrainDto;

                // Obtain previous and next trains to append the alight or board CRS codes.
                // This is done to capture a "cross london" journey scenario and ensure the london stations
                // appear as the route taken. Ensures invalid london fares are removed by the RBO
                TrainDto previousTrain = null;
                TrainDto nextTrain = null;

                if (r > 0)
                {
                    previousTrain = trains[r - 1] as TrainDto;
                }

                if (r < (trains.Count - 1))
                {
                    nextTrain = trains[r + 1] as TrainDto;
                }

                // Append Previous trains alight CRS code if required
                if (previousTrain != null)
                {
                    if ((outputRouteCRScodes.Contains(previousTrain.Alight.Location.Crs))
                    &&
                    (!inputRouteCRScodes.Contains(previousTrain.Alight.Location.Crs)))
                    {
                        inputRouteCRScodes.Add(previousTrain.Alight.Location.Crs);
                        tempRouteCRS.Append(previousTrain.Alight.Location.Crs);
                    }
                }

                #region Current train CRS codes
                // The ROUTE-CRS list doesn't need to include the origin and destination (unless they are in the list returned by the GL call)
                if ((outputRouteCRScodes.Contains(train.Board.Location.Crs))
                    &&
                    (!inputRouteCRScodes.Contains(train.Board.Location.Crs)))
                {
                    inputRouteCRScodes.Add(train.Board.Location.Crs);
                    tempRouteCRS.Append(train.Board.Location.Crs);
                }

                for (int s = 0; s < train.IntermediateStops.Length; s++)
                {
                    if (outputRouteCRScodes.Contains(train.IntermediateStops[s].Crs))
                    {
                        if (!inputRouteCRScodes.Contains(train.IntermediateStops[s].Crs))
                        {
                            inputRouteCRScodes.Add(train.IntermediateStops[s].Crs);
                            tempRouteCRS.Append(train.IntermediateStops[s].Crs);
                        }
                    }
                }

                if ((outputRouteCRScodes.Contains(train.Alight.Location.Crs))
                    &&
                    (!inputRouteCRScodes.Contains(train.Alight.Location.Crs)))
                {
                    inputRouteCRScodes.Add(train.Alight.Location.Crs);
                    tempRouteCRS.Append(train.Alight.Location.Crs);
                }

                #endregion

                // Append Next trains board CRS code if required
                if (nextTrain != null)
                {
                    if ((outputRouteCRScodes.Contains(nextTrain.Board.Location.Crs))
                        &&
                        (!inputRouteCRScodes.Contains(nextTrain.Board.Location.Crs)))
                    {
                        inputRouteCRScodes.Add(nextTrain.Board.Location.Crs);
                        tempRouteCRS.Append(nextTrain.Board.Location.Crs);
                    }
                }
            }


            // Set up the CRS string for this journey (all train legs)
            inputRouteCRSs = tempRouteCRS.ToString().PadRight(60, ' ');
            inputRouteCRSs = inputRouteCRSs.Substring(0, 60);  //Force it to be 60 chars in length

            return inputRouteCRSs;
        }

        /// <summary>
        /// Builds a string of route code compliant flags for a Journey (containing trains with applicable route
        /// codes) against the route codes list supplied.
        /// Will create 99 pairs in a string, e.g. "Y N ..." - where "Y " indicates the first routecode 
        /// is applicable for the outward train and unknown for the return
        /// </summary>
        /// <param name="trains">All the trains for a Journey</param>
        /// <param name="routeCodes">The route codes to mark as valid/invalid for the Journey</param>
        /// <returns></returns>
        private string BuildRoutingGuideFlagsForJourney(List<TrainDto> trains, List<string> routeCodes, bool outward)
        {
            // The return string
            StringBuilder sb = new StringBuilder();

            // Are there any trains to process
            if (trains.Count == 0)
            {
                // No trains, so setup an empty route code string containing route code flags to make up 99 entries
                for (int i = 0; i < (99); i++)
                {
                    sb.Append(" ");     //RG-OUT-VALID
                    sb.Append(" ");     //RG-RET-VALID
                }

                return sb.ToString();
            }
           
            // Go through each route code and train route code combination, and leave only those
            // route codes which apply to all the trains for the journey
            foreach (string routeCode in routeCodes)
            {
                bool found = true;

                foreach(TrainDto train in trains)
                {
                    // Ignore trains which represent a walk leg as they won't have any route codes to match
                    if (!train.IsForWalk)
                    {
                        // Check each route code for this train, we need to find a match
                        // otherwise have to mark this routecode as overall invalid for this journey
                        // (because all trains didn't contain it)
                        bool match = false;

                        foreach (string trainRouteCode in train.FareRouteCodes)
                        {
                            if (routeCode == trainRouteCode)
                            {
                                // Found the route code, move on to the next train
                                match = true;
                                break;
                            }
                        }

                        if (!match)
                        {
                            // This train didn't contain the route code, fail and move on to the next route code
                            // to check
                            found = false;
                            break;
                        }
                    }
                }

                // After matching all train route codes for this journey, we will end up with a string of 
                // journey compliant indicators for each fare routecode.
                // e.g. If there are two route codes in the array, then we will end up with:
                // "Y N ..." up to 99 pairs. This equates to Journey is outward valid for Route in index 1 and
                // return valid unknown, and not outward valid for index 2 and return valid unknown. Spaces padding
                // is added indicating unknown for the remaining routes upto 99.

                // Was this route code found on all the trains in this journey?
                if (found)
                {
                    sb.Append((outward) ? "Y " : " Y"); //RG-OUT-VALID //RG-RET-VALID
                }
                else
                {
                    sb.Append((outward) ? "N " : " N"); //RG-OUT-VALID //RG-RET-VALID
                }
            }

            // Populate the remaining route code spots to make up 99 entries
            for (int i = 0; i < (99 - routeCodes.Count); i++)
            {
                sb.Append(" ");     //RG-OUT-VALID
                sb.Append(" ");     //RG-RET-VALID
            }

            return sb.ToString();
        }
    }
}
