//********************************************************************************
//NAME         : ValidateSpecificFareRequest.cs
//AUTHOR       : SchlumbergerSema
//DATE CREATED : 10/05/2003
//DESCRIPTION  : 
//             : 
//DESIGN DOC   : DD034 Reatil Pricing
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/RetailBusinessObjects/ValidateRoutes.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:46:26   mturner
//Initial revision.
//
//   Rev 1.6   Jun 17 2004 13:33:14   passuied
//changes for del6:
//Inserted calls to GL and GM functions to restrict fares.
//Changes in RestrictFares design to respect Open-Close Principle
//
//   Rev 1.5   Jun 09 2004 13:28:00   asinclair
//Started work for Calling passing locations. Not finished
//
//   Rev 1.4   Jan 13 2004 13:13:26   CHosegood
//Now checks to see if a negative length output size has been supplied and throws an excpetion if this is the case.
//Resolution for 594: BO request do not check for a negative length output buffer.
//
//   Rev 1.3   Oct 28 2003 20:05:08   geaton
//Changes to support housekeeping and timeout functionality.
//
//   Rev 1.2   Oct 22 2003 15:35:00   CHosegood
//Intermediate version
//
//   Rev 1.1   Oct 14 2003 12:26:42   CHosegood
//Intermediate version.  Not ready for build/test
//
//   Rev 1.0   Oct 13 2003 15:25:56   CHosegood
//Initial Revision

using System;
using System.Diagnostics;
using System.Text;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.UserPortal.PricingMessages;

namespace TransportDirect.UserPortal.RetailBusinessObjects
{
    /// <summary>
    /// Summary description for ValidateRoutes.
    /// </summary>
    public class ValidateRoutes : BusinessObjectInput
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="interfaceVersion"></param>
        /// <param name="outputLength"></param>
        /// <param name="request"></param>
        /// <param name="fare"></param>
        public ValidateRoutes(string interfaceVersion,
            int outputLength, PricingRequestDto request, String routeListOutput )
            : base("RE", "GM", interfaceVersion )
        {
    
            if ( outputLength < 0 ) 
            {
                string msg = "Output Lenght must be a non-negative number.  Output Length supplied::" + outputLength.ToString();
                TDException tde = new TDException(msg, true, TDExceptionIdentifier.PRHOutputLengthMustBeNonNegative );
                Trace.Write( new OperationalEvent( TDEventCategory.Infrastructure, TDTraceLevel.Error, msg, tde ) );
                throw tde;
            }

			if (TDTraceSwitch.TraceVerbose)
				Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Info, "CRS: " + BuildVisitCrs( request )));
            
			HeaderInputParameter inputParameter = new HeaderInputParameter( routeListOutput );
            this.AddInputParameter( inputParameter, 0 );

            HeaderOutputParameter outputParameter = new HeaderOutputParameter( outputLength );
            this.AddOutputParameter( outputParameter, 0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="fare"></param>
        /// <returns></returns>
		protected string BuildVisitCrs
			( PricingRequestDto request )
//		( PricingRequestDto request, Fares fares )
		{
			StringBuilder sb = new StringBuilder();
			int count = 0;

			//Build the VISIT CRS list
			if ( request.Trains.Count > 0 ) 
			{
				foreach( TrainDto train in request.Trains ) 
				{
					if ( train.IntermediateStops !=null && train.IntermediateStops.Length > 0 )
					{
						foreach (LocationDto location in train.IntermediateStops )
						{
							sb.Append( location.Crs );
							count++;
						}
					}
				}
			}
			return count.ToString().PadLeft(4,'0') + sb.ToString().PadRight(60, ' ').Substring(0,60);



//			AS THE STUFF BELOW IS NOT NEEDED AS THE OTHER OUTPUT FROM THE GL CALL REMAINS UNCHANGED
			//The number of route entries to follow
//			sb.Append(fares.Item.Count.ToString().PadLeft (4,'0') );			
//			
//			for ( int i = 0; i< 25; i++)
//			{
//				foreach (Fare fare in fares.Item)
//				{
//					sb.Append ( fare.Route.Code);// RT-ROUTE-CODE
//					if (fare.Route.CrossLondon.Equals( true)) //RT-XLONDON-MARKER
//					{
//						sb.Append( "Y");
//					}
//					else 
//					{
//						sb.Append( "N");
//					}
//
//					sb.Append (fare.Route.
//
//
//				}
					// LONDON-CRS
//					sb.Append ( "KCMEUSMYBSTPPADLBGVICWATKGXLSTZMGBFRCSTCHXWAEVXHFST         ");
//					
//				}
        }
    }
}
