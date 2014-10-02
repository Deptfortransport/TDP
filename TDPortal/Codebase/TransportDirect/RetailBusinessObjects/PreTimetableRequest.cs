//********************************************************************************
//NAME         : PreTimetableRequest.cs
//AUTHOR       : Richard Philpott
//DATE CREATED : 2005-03-18
//DESCRIPTION  : Wrapper for contents of RBO GC (pre-timetable) request.
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/RetailBusinessObjects/PreTimetableRequest.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:46:14   mturner
//Initial revision.
//
//   Rev 1.1   Apr 08 2005 18:55:02   RPhilpott
//Corrections to restrictions handling.
//
//   Rev 1.0   Mar 22 2005 16:30:42   RPhilpott
//Initial revision.
//

using System;
using System.Diagnostics;
using System.Text;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.UserPortal.PricingMessages;

namespace TransportDirect.UserPortal.RetailBusinessObjects
{

	/// <summary>
	/// Wrapper for contents of RBO GC (pre-timetable) request.
	/// </summary>
	public class PreTimetableRequest : BusinessObjectInput
    {
		public PreTimetableRequest(string interfaceVersion,
            int outputLength, PricingRequestDto request, FareDataDto fare, bool outward)
            : base("RE", "GC", interfaceVersion )
        {
    
            if (outputLength < 0) 
            {
                string msg = "Output length must be a non-negative number.  Output length supplied::" + outputLength.ToString();
                TDException tde = new TDException(msg, true, TDExceptionIdentifier.PRHOutputLengthMustBeNonNegative );
                Trace.Write( new OperationalEvent( TDEventCategory.Infrastructure, TDTraceLevel.Error, msg, tde ) );
                throw tde;
            }

			HeaderInputParameter inputParameter = new HeaderInputParameter(this.BuildRequest(request, fare, outward));
            this.AddInputParameter(inputParameter, 0);

            HeaderOutputParameter outputParameter = new HeaderOutputParameter(outputLength);
            this.AddOutputParameter(outputParameter, 0);
        }

		protected string BuildRequest(PricingRequestDto request, FareDataDto fare, bool outward)
		{
			StringBuilder sb = new StringBuilder();

			sb.Append(fare.OriginNlc);
			sb.Append("   ");						// origin CRS -- not known at this stage
			sb.Append(fare.DestinationNlc);
			sb.Append("   ");						// destination CRS -- not known at this stage
			sb.Append(request.OutwardDate.ToString("yyyyMMdd"));
			sb.Append(request.ReturnDate == null ? "        " : request.ReturnDate.ToString("yyyyMMdd"));

			sb.Append(outward ? "O" : "R");	

			sb.Append(fare.ShortTicketCode);
			sb.Append(fare.RouteCode);
			sb.Append(fare.RestrictionCode);
			sb.Append(fare.RailcardCode);
			
			sb.Append("D");									// always a depart-afer time
			
			string testDate = (outward ? request.OutwardDate.ToString("yyyyMMdd") : request.ReturnDate.ToString("yyyyMMdd")); 

			if	(testDate.Equals(DateTime.Now.ToString("yyyyMMdd")))
			{
				sb.Append(DateTime.Now.ToString("HHmm"));	// if today, earliest depart time is now	
			}
			else
			{
				sb.Append("0000");							// if not today, want all trains for the day 		
			}

			return sb.ToString();
        }
    }
}
