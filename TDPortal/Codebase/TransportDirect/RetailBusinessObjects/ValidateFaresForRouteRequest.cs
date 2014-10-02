//********************************************************************************
//NAME         : ValidateFaresForRouteRequest.cs
//AUTHOR       : Richard Philpott
//DATE CREATED : 2005-03-18
//DESCRIPTION  : Wrapper for contents of RBO GR (validate faes for route) request.
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/RetailBusinessObjects/ValidateFaresForRouteRequest.cs-arc  $
//
//   Rev 1.1   Feb 18 2009 18:20:34   mmodi
//Updated following changes to use interface 0202
//Resolution for 5210: CCN487 - ZPBO Implementation workstream
//
//   Rev 1.0   Nov 08 2007 12:46:26   mturner
//Initial revision.
//
//   Rev 1.2   Dec 03 2005 17:18:30   RPhilpott
//Use origin/destination locations from fare, not from request.
//Resolution for 3308: DN040: (CG) Incorrect day/rate availability on weekender fare
//
//   Rev 1.1   Apr 14 2005 13:42:54   RPhilpott
//Correct length of trains field, and route numbering.
//Resolution for 2148: PT - Fares Journey Planner tries to sell day returns for journeys seperated by a day
//
//   Rev 1.0   Mar 22 2005 16:30:44   RPhilpott
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

    public class ValidateFaresForRouteRequest : BusinessObjectInput
    {
        public ValidateFaresForRouteRequest(string interfaceVersion, int outputLength, PricingRequestDto request, Fares fares)
            : base("RE", "GR", interfaceVersion)
        {

            if (outputLength < 0) 
            {
                string msg = "Output length must be a non-negative number.  Output length supplied::" + outputLength.ToString();
                TDException tde = new TDException(msg, true, TDExceptionIdentifier.PRHOutputLengthMustBeNonNegative );
                Trace.Write( new OperationalEvent( TDEventCategory.Infrastructure, TDTraceLevel.Error, msg, tde ) );
                throw tde;
            }

            HeaderInputParameter inputParameter = new HeaderInputParameter(this.BuildValidateFaresForRouteRequest(request, fares));
            this.AddInputParameter(inputParameter, 0);

            HeaderOutputParameter outputParameter = new HeaderOutputParameter(outputLength);
            this.AddOutputParameter(outputParameter, 0);
        }


		protected string BuildValidateFaresForRouteRequest(PricingRequestDto request, Fares fares) 
		{
			StringBuilder sb = new StringBuilder();

			sb.Append(fares.FareOriginNlc);  
			sb.Append("   ");				// CRS code - not yet known  
			sb.Append(" ");                 // country code
			sb.Append(fares.FareDestinationNlc);
			sb.Append("   ");				// CRS code - not yet known
			sb.Append( request.OutwardDate.ToString("yyyyMMdd") );
			sb.Append((request.ReturnDate == null) ? "        " : request.ReturnDate.ToString("yyyyMMdd"));
			sb.Append( "Y" );				// singles ARE valid, even if we have return date ...
            
			sb.Append("00");				// no trains yet
			sb.Append(' ', ((36 + (99*4)) * 20) );

			ArrayList routes = new ArrayList();
			StringBuilder sb2 = new StringBuilder();


            string emptyFare = string.Empty;
            emptyFare = emptyFare.PadLeft(16, ' '); // Fare details

            sb2.Append(fares.Item.Count.ToString().PadLeft(3,'0'));

            for (int i = 0; i < 300; i++)                               //FARES-TICKET
            {
                if ((fares.Item.Count == 0) || (i >= fares.Item.Count))
                {
                    sb2.Append(emptyFare);
                }
                else
                {
                    Fare fare = (Fare)fares.Item[i];

                    if (!routes.Contains(fare.Route.Code))
                    {
                        routes.Add(fare.Route.Code);
                    }

                    sb2.Append(fare.TicketType.Code.PadRight(3, ' '));
                    sb2.Append(fare.RailcardCode.PadRight(3, ' '));
                    sb2.Append((routes.IndexOf(fare.Route.Code) + 1).ToString().PadLeft(2, '0'));
                    sb2.Append(fare.RestrictionCode.PadRight(2, ' '));

                    if (fare.TicketType.Type.Equals(JourneyType.Return))
                    {
                        sb2.Append("R");
                    }
                    else
                    {
                        sb2.Append("S");
                    }
                    sb2.Append(fare.TicketValidityCode.PadRight(2, ' '));
                    sb2.Append("   ");      // POV-Days
                }
            }


			sb.Append(routes.Count.ToString().PadLeft(2,'0'));          // Add the routes

			for (int i = 0; i < 99; i++) 
			{
				if ((routes.Count == 0) || (i >= routes.Count))
				{
					sb.Append("     ");
				} 
				else 
				{
					sb.Append(routes[i].ToString().PadRight(5,' '));
				}
			}

			sb.Append(sb2.ToString());                                  // Add the fares


            sb.Append(' ', ((1 + 99) * 2));                             // No routing guide validity


			return sb.ToString();
		}
    }
}
