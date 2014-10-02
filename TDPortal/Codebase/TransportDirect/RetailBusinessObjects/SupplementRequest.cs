//********************************************************************************
//NAME         : SupplementRequest.cs
//AUTHOR       : Richard Philpott
//DATE CREATED : 2005-03-18
//DESCRIPTION  : BO input object used to request all 
//				  supplements for a journey from the SBO.   
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/RetailBusinessObjects/SupplementRequest.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:46:24   mturner
//Initial revision.
//
//   Rev 1.0   Mar 22 2005 16:30:44   RPhilpott
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
    /// BO input object used to request all supplements for a journey from the SBO.   
    /// </summary>
    public class SupplementRequest : BusinessObjectInput
    {
        public SupplementRequest(string interfaceVersion, PricingRequestDto request, FareDataDto fare, ArrayList trains,
            int outputLength) : base("SU", "GD", interfaceVersion )
        {
            if  (outputLength < 0) 
            {
                string msg = "Output Length must be a non-negative number.  Output Length supplied::" + outputLength.ToString();
                TDException tde = new TDException(msg, true, TDExceptionIdentifier.PRHOutputLengthMustBeNonNegative );
                Trace.Write( new OperationalEvent( TDEventCategory.Infrastructure, TDTraceLevel.Error, msg, tde ) );
                throw tde;
            }

            HeaderInputParameter inputParameter = new HeaderInputParameter(this.BuildRequestHeader(request, fare));
            this.AddInputParameter(inputParameter, 0);

			inputParameter = new HeaderInputParameter(this.BuildRequestLegs(request));
			this.AddInputParameter(inputParameter, 1);

			inputParameter = new HeaderInputParameter(string.Empty);
			this.AddInputParameter(inputParameter, 2);

			inputParameter = new HeaderInputParameter(string.Empty);
			this.AddInputParameter(inputParameter, 3);

			inputParameter = new HeaderInputParameter(string.Empty);
			this.AddInputParameter(inputParameter, 4);

			HeaderOutputParameter outputParameter = new HeaderOutputParameter(15);
			this.AddOutputParameter(outputParameter, 0);
		
			outputParameter = new HeaderOutputParameter(0);
			this.AddOutputParameter(outputParameter, 1);
		
			outputParameter = new HeaderOutputParameter(0);
			this.AddOutputParameter(outputParameter, 2);

			outputParameter = new HeaderOutputParameter(331);   // allows for 40 supps
			this.AddOutputParameter(outputParameter, 3);
	
		}


        private string BuildRequestHeader(PricingRequestDto request, FareDataDto fare) 
        {
            StringBuilder sb = new StringBuilder();

			sb.Append("HEADER  ");
			sb.Append("N");			// "new" - ie, create list of supplements
			sb.Append("N");			// process, not validation-only

			sb.Append(request.Origin.Nlc);
            sb.Append(request.Destination.Nlc);
			
			sb.Append(fare.ShortTicketCode);
			sb.Append(fare.RailcardCode.PadRight(3, ' '));
			sb.Append(fare.RestrictionCode.PadRight(2, ' '));
			
			sb.Append(request.NumberOfAdults.ToString().PadLeft(3, '0'));
			sb.Append(request.NumberOfChildren.ToString().PadLeft(3, '0'));
			sb.Append("000");	// AAA - not used ...
			
			sb.Append(request.OutwardDate.ToString("yyyyMMdd"));
			sb.Append(request.ReturnDate == null ? "        " : request.ReturnDate.ToString("yyyyMMdd"));
			
			sb.Append(DateTime.Now.ToString("yyyyMMdd"));
			
			sb.Append("T");		// TOC codes to be used

            return sb.ToString();
        }


		private string BuildRequestLegs(PricingRequestDto request) 
		{
			StringBuilder sb = new StringBuilder();

			sb.Append("LEGS    ");

			sb.Append(request.Trains.Count.ToString().PadLeft(2, '0'));

			int legNum = 1;		// leg number within direction (ie, outward/return)

			ReturnIndicator prevReturnIndicator = ((TrainDto)request.Trains[0]).ReturnIndicator;

			foreach (TrainDto train in request.Trains)
			{
				sb.Append((train.ReturnIndicator == ReturnIndicator.Outbound) ? "O" : "R");											// out/return ???
				
				if	(train.ReturnIndicator != prevReturnIndicator)
				{
					legNum = 1;				// switch from outward to return ,,,
					prevReturnIndicator = train.ReturnIndicator;
				}

				sb.Append(legNum.ToString().PadLeft(2, '0'));				
				legNum++;

				sb.Append(train.Uid.PadRight(7, ' '));
				sb.Append(train.Board.Location.Crs);	
				sb.Append(train.Alight.Location.Crs);
				sb.Append(train.QuotaControlledFare ? "Y" : "N");
				sb.Append(train.TrainClass.PadRight(1, ' '));		
				sb.Append(train.Sleeper.PadRight(1, ' '));			
				sb.Append(" ");										// sector code - not used
				sb.Append(train.Tocs[0].Code.PadRight(2,' '));
				sb.Append(train.Tocs[1].Code.PadRight(2,' '));
				sb.Append(train.Catering.PadRight(4, ' '));			
				sb.Append(train.Reservability.PadRight(1, ' '));
				sb.Append(" ");										// spare field - not used
			}

			return sb.ToString();
		}

    }

}
