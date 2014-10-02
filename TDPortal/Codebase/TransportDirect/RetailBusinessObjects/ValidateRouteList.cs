//********************************************************************************
//NAME         : ValidateSpecificFareRequest.cs
//AUTHOR       : SchlumbergerSema
//DATE CREATED : 10/05/2003
//DESCRIPTION  : 
//             : 
//DESIGN DOC   : DD034 Reatil Pricing
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/RetailBusinessObjects/ValidateRouteList.cs-arc  $
//
//   Rev 1.2   Feb 26 2009 15:00:50   mmodi
//Allow origin and destination NLC to be defined be caller
//Resolution for 5239: Routeing Guide - FCC Routed Travelcards
//
//   Rev 1.1   Feb 18 2009 18:20:34   mmodi
//Updated following changes to use interface 0202
//Resolution for 5210: CCN487 - ZPBO Implementation workstream
//
//   Rev 1.0   Nov 08 2007 12:46:26   mturner
//Initial revision.
//
//   Rev 1.3   Jan 13 2004 13:13:26   CHosegood
//Now checks to see if a negative length output size has been supplied and throws an excpetion if this is the case.
//Resolution for 594: BO request do not check for a negative length output buffer.
//
//   Rev 1.2   Oct 21 2003 11:17:20   CHosegood
//No change.
//
//   Rev 1.1   Oct 14 2003 12:26:42   CHosegood
//Intermediate version.  Not ready for build/test
//
//   Rev 1.0   Oct 13 2003 15:25:56   CHosegood
//Initial Revision

#region using
using System;
using System.Collections;
using System.Diagnostics;
using System.Text;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.UserPortal.PricingMessages;
#endregion

namespace TransportDirect.UserPortal.RetailBusinessObjects
{
    /// <summary>
    /// Summary description for ValidateRouteList.
    /// </summary>
    public class ValidateRouteList : BusinessObjectInput
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="interfaceVersion"></param>
        /// <param name="outputLength"></param>
        /// <param name="request"></param>
        /// <param name="fares"></param>
        public ValidateRouteList(string interfaceVersion,
            int outputLength, PricingRequestDto request, ArrayList fares, string originNlc, string destinationNlc)
            : base("RE", "GL", interfaceVersion )
        {

            if ( outputLength < 0 ) 
            {
                string msg = "Output Lenght must be a non-negative number.  Output Length supplied::" + outputLength.ToString();
                TDException tde = new TDException(msg, true, TDExceptionIdentifier.PRHOutputLengthMustBeNonNegative );
                Trace.Write( new OperationalEvent( TDEventCategory.Infrastructure, TDTraceLevel.Error, msg, tde ) );
                throw tde;
            }

            HeaderInputParameter inputParameter = new HeaderInputParameter( this.BuildValidateRouteList( request, fares, originNlc, destinationNlc ) );
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
        protected string BuildValidateRouteList
            (PricingRequestDto request, ArrayList fares, string originNlc, string destinationNlc) 
        {
            StringBuilder sb = new StringBuilder();
            sb.Append( request.Origin.Crs );                                //ORIGIN-CRS

            if (string.IsNullOrEmpty(originNlc))
            {
                sb.Append(request.Origin.Nlc);                              //ORIGIN-NLC
            }
            else
            {
                sb.Append(originNlc);                                       //ORIGIN-NLC
            }
                        
            sb.Append( request.Destination.Crs );                           //DEST-CRS

            if (string.IsNullOrEmpty(destinationNlc))
            {
                sb.Append(request.Destination.Nlc);                         //DEST-NLC
            }
            else
            {
                sb.Append(destinationNlc);                                  //DEST-NLC
            }
            
            sb.Append( request.OutwardDate.ToString("yyyyMMdd") );          //OUT-DATE
            sb.Append( fares.Count.ToString().PadLeft(4,'0') );             //ROUTE-COUNT

            // Interface version 0202 and later allows 99 routes (otherwise 25)
            Hashtable routes = new Hashtable(99);
            // This might not be possible to key on the route code as it may be possible to have a route code with different crossLondon attributes
            foreach (Fare fare in fares) 
            {
                if ( !routes.Contains( fare.Route.Code ) ) 
                {
                    routes.Add( fare.Route.Code, fare.Route.CrossLondon );
                }
            }

            IDictionaryEnumerator enu = routes.GetEnumerator();
            while (enu.MoveNext() ) 
            {
                sb.Append( enu.Key );
                sb.Append( enu.Value );
            }

            for ( int i = 0; i < 99 - routes.Count; i ++ ) 
            {
                sb.Append( "      " );
            }

            return sb.ToString();
        }
    }
}
