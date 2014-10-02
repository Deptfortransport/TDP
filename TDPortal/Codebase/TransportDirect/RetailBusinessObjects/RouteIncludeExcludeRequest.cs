//********************************************************************************
//NAME         : RouteIncludeExcludeRequest.cs
//AUTHOR       : Richard Philpott
//DATE CREATED : 2005-03-18
//DESCRIPTION  : Wrapper for contents of RBO GN (route include/exclude) request.
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/RetailBusinessObjects/RouteIncludeExcludeRequest.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:46:22   mturner
//Initial revision.
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
	/// Wrapper for contents of RBO GN (route include/exclude) request.
	/// </summary>
	public class RouteIncludeExcludeRequest : BusinessObjectInput
    {
		public RouteIncludeExcludeRequest(string interfaceVersion,
            int outputLength, PricingRequestDto request, FareDataDto fare )
            : base("RE", "GN", interfaceVersion )
        {
    
            if ( outputLength < 0 ) 
            {
                string msg = "Output length must be a non-negative number.  Output length supplied::" + outputLength.ToString();
                TDException tde = new TDException(msg, true, TDExceptionIdentifier.PRHOutputLengthMustBeNonNegative );
                Trace.Write( new OperationalEvent( TDEventCategory.Infrastructure, TDTraceLevel.Error, msg, tde ) );
                throw tde;
            }

			HeaderInputParameter inputParameter = new HeaderInputParameter(this.BuildRequest(request, fare));
            this.AddInputParameter(inputParameter, 0);

            HeaderOutputParameter outputParameter = new HeaderOutputParameter(outputLength);
            this.AddOutputParameter(outputParameter, 0);
        }

		protected string BuildRequest(PricingRequestDto request, FareDataDto fare)
		{
			StringBuilder sb = new StringBuilder();

            sb.Append(fare.RouteCode.PadRight(5, ' '));
			sb.Append(request.OutwardDate.ToString("yyyyMMdd"));
			sb.Append(fare.ShortTicketCode.PadRight(3, ' '));
			sb.Append(fare.RestrictionCode.PadRight(2, ' '));
			
			return sb.ToString();
        }
    }
}
