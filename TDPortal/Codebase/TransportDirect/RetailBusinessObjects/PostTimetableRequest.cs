//********************************************************************************
//NAME         : PreTimetableRequest.cs
//AUTHOR       : Richard Philpott
//DATE CREATED : 2005-03-18
//DESCRIPTION  : Wrapper for contents of RBO GD (post-timetable) request.
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/RetailBusinessObjects/PostTimetableRequest.cs-arc  $
//
//   Rev 1.1   Feb 18 2009 18:18:08   mmodi
//Update following change to use interface 0202
//Resolution for 5210: CCN487 - ZPBO Implementation workstream
//
//   Rev 1.0   Nov 08 2007 12:46:12   mturner
//Initial revision.
//
//   Rev 1.3   Dec 03 2005 14:23:32   RPhilpott
//Use Board/Alight points for leg Origin/Destination.
//Resolution for 3306: DN040: (CG) Restrictions not applied on York to Bristol
//
//   Rev 1.2   Dec 01 2005 14:54:02   RPhilpott
//Correct GD input after clarification of misleading interface spec.
//Resolution for 3205: DN039 - Fare available in Time Based Planning not available in Fare based Planning
//
//   Rev 1.1   Apr 08 2005 18:54:38   RPhilpott
//Corrections to restrictions checking
//
//   Rev 1.0   Mar 22 2005 16:30:40   RPhilpott
//Initial revision.
//

using System;
using System.Text;
using System.Collections;
using System.Diagnostics;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.UserPortal.PricingMessages;

namespace TransportDirect.UserPortal.RetailBusinessObjects
{

	/// <summary>
	/// Wrapper for contents of RBO GD (post-timetable) request.
	/// </summary>
	public class PostTimetableRequest : BusinessObjectInput
    {
		public PostTimetableRequest(string interfaceVersion,
            int outputLength, PricingRequestDto request, FareDataDto fare) : base("RE", "GD", interfaceVersion )
        {
            if (outputLength < 0) 
            {
                string msg = "Output length must be a non-negative number.  Output length supplied::" + outputLength.ToString();
                TDException tde = new TDException(msg, true, TDExceptionIdentifier.PRHOutputLengthMustBeNonNegative );
                Trace.Write( new OperationalEvent( TDEventCategory.Infrastructure, TDTraceLevel.Error, msg, tde ) );
                throw tde;
            }

			HeaderInputParameter inputParameter = new HeaderInputParameter(this.BuildRequest(request, fare));
            this.AddInputParameter(inputParameter, 0);

            HeaderOutputParameter outputParameter = new HeaderOutputParameter( outputLength );
            this.AddOutputParameter(outputParameter, 0);
        }

		protected string BuildRequest(PricingRequestDto request, FareDataDto fare)
		{
			StringBuilder sb = new StringBuilder();

			bool outward = (((TrainDto)request.Trains[0]).ReturnIndicator == ReturnIndicator.Outbound);

			string journeyOriginCrs		 = ((TrainDto)request.Trains[0]).Board.Location.Crs.PadRight(3,' ');
			string journeyDestinationCrs = ((TrainDto)request.Trains[request.Trains.Count - 1]).Alight.Location.Crs.PadLeft(3,' ');

			sb.Append(journeyOriginCrs);
			sb.Append(journeyDestinationCrs);
			sb.Append(request.OutwardDate.ToString("yyyyMMdd"));
			sb.Append(request.ReturnDate == null ? "        " : request.ReturnDate.ToString("yyyyMMdd"));
			sb.Append(outward ? "O" : "R");
			sb.Append(fare.ShortTicketCode.PadRight(3, ' '));
			sb.Append(fare.RailcardCode.PadRight(3, ' '));
			sb.Append("T");													// always TOC

			sb.Append(fare.RestrictionCodesToReapply.PadRight(40, ' '));	// field is 20 x 2-char codes 

			// field SERV-DEST-CRS is leg destination of last leg 
			//   (which would differ from journey dest if and only if 
			//     final leg of journey were non-rail (eg, tube))

			sb.Append(((TrainDto)request.Trains[request.Trains.Count - 1]).Destination.Location.Crs.PadLeft(3,' '));
			
			sb.Append(outward ? request.OutwardDate.ToString("yyyyMMdd") : request.ReturnDate.ToString("yyyyMMdd"));			

			sb.Append(request.Trains.Count.ToString().PadLeft(2, '0'));
		
            string emptyTrain = string.Empty;
            emptyTrain = emptyTrain.PadLeft((27 + 396), ' '); // Train details

            for (int i = 0; i < 10; i++)
            {
                if ((request.Trains.Count == 0) || (i >= request.Trains.Count))
                {
                    sb.Append(emptyTrain);
                }
                else
                {
                    TrainDto train = request.Trains[i] as TrainDto;

                    sb.Append(train.Board.Location.Crs.PadRight(3, ' '));
                    sb.Append(train.Board.Departure.ToString("HHmm"));
                    sb.Append(train.Alight.Arrival.ToString("HHmm"));
                    sb.Append(train.Uid.PadLeft(6, ' '));
                    sb.Append(train.Alight.Location.Crs.PadLeft(3, ' '));
                    sb.Append(train.Tocs[0].Code.PadLeft(2, ' '));
                    sb.Append(train.Tocs[1].Code.PadLeft(2, ' '));
                    sb.Append(" ");												// sector - redundant field

                    // Number of locations train passes or stops at (including the origin and destination of the train)
                    int trainStopsCount = 2 + train.IntermediateStops.Length;
                    sb.Append(trainStopsCount.ToString().PadLeft(2, '0'));

                    for (int j = -1; j < 98; j++)
                    {
                        string nlc = string.Empty;

                        if (j == -1)
                        {
                            // Append the train start
                            nlc = train.Board.Location.Nlc;
                        }
                        else if (j == train.IntermediateStops.Length)
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

                }   //if-else
            }

			sb.Append(fare.RouteCode.PadRight(5, ' '));
            sb.Append(fare.OriginNlc);
            sb.Append(fare.DestinationNlc);

			return sb.ToString();
        }
    }
}
