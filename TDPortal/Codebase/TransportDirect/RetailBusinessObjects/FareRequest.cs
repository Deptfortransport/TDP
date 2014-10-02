//********************************************************************************
//NAME         : BusinessObject.cs
//AUTHOR       : SchlumbergerSema
//DATE CREATED : 10/05/2003
//DESCRIPTION  : 
//             : 
//DESIGN DOC   : DD034 Reatil Pricing
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/RetailBusinessObjects/FareRequest.cs-arc  $
//
//   Rev 1.1   Jan 11 2009 17:01:14   mmodi
//Updated to use specific ZPBO values
//Resolution for 5210: CCN487 - ZPBO Implementation workstream
//
//   Rev 1.0   Nov 08 2007 12:46:04   mturner
//Initial revision.
//
//   Rev 1.3   Jan 13 2004 13:13:28   CHosegood
//Now checks to see if a negative length output size has been supplied and throws an excpetion if this is the case.
//Resolution for 594: BO request do not check for a negative length output buffer.
//
//   Rev 1.2   Oct 17 2003 10:10:22   CHosegood
//Removed Total fare prices from query
//
//   Rev 1.1   Oct 14 2003 12:27:50   CHosegood
//Intermediate version.  Not ready for build/test
//
//   Rev 1.0   Oct 13 2003 15:25:40   CHosegood
//Initial Revision

#region using
using System;
using System.Diagnostics;
using System.Text;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.UserPortal.PricingMessages;
#endregion

namespace TransportDirect.UserPortal.RetailBusinessObjects
{
    /// <summary>
    /// Summary description for FareRequest.
    /// </summary>
    public class FareRequest : BusinessObjectInput
    {
        #region RBO query constants
        //                                                                  //length
        public const string ADULT_FARE          = "ADULT-FARE      ";       //13
        public const string ADULT_FARE_M        = "ADULT-FARE-M    ";       //8
        public const string CHILD_FARE          = "CHILD-FARE      ";       //13
        public const string CHILD_FARE_M        = "CHILD-FARE-M    ";       //8
        public const string CLASS               = "CLASS           ";       //1
        public const string CROSS_LONDON        = "CROSS-LONDON    ";       //1
        public const string DESTINATION         = "DESTINATION     ";       //6
        public const string FARE_CURRENCY       = "FARE-CURRENCY   ";       //3
        public const string JOURNEY_TYPE        = "JOURNEY-TYPE    ";       //1
        public const string NUM_ADULTS          = "NUM-ADULTS      ";       //3
        public const string NUM_CHILDREN        = "NUM-CHILDREN    ";       //3
        public const string ORIGIN              = "ORIGIN          ";       //6
        public const string OUTWARD_DATE        = "OUTWARD-DATE    ";       //8
        public const string RAILCARD_CODE       = "RAILCARD-CODE   ";       //3
        public const string RESTRICTION_CODE    = "RESTRICTION-CODE";       //2
        public const string ROUTE_CODE          = "ROUTE-CODE      ";       //5
        public const string TICKET_TYPE         = "TICKET-TYPE     ";       //3
        public const string TICKET_VAL_CODE     = "TICKET-VAL-CODE ";       //2
        public const string TOC_CODE            = "TOC-CODE        ";       //3
        public const string TOTAL_ADULT_FARE    = "TOTAL-ADULT-FARE";       //13
        public const string TOTAL_CHILD_FARE    = "TOTAL-CHILD-FARE";       //1
        public const string ORIGIN_FARE         = "ORIGIN-FARE     ";       //6   - the actual origin for a fare
        public const string DESTINATION_FARE    = "DESTINATION-FARE";       //6   - the actual destination for a fare

        public const string SELECT      = "SELECT  ";
        public const string WHERE       = "WHERE   ";
        public const string ORDER_BY    = "ORDER BY";
        public const string END_SELECT = "9999999999999999";
        public const string END_WHERE = "9999999999999999999999999999999999";
        public const string END_ORDER_BY = "99999999999999999";
        #endregion

        #region Private members

        private bool useZPBO = false;

        #endregion

        public FareRequest(string interfaceVersion, PricingRequestDto request,
            int outputLength, bool useZPBO)
        {

            if ( outputLength < 0 ) 
            {
                string msg = "Output Lenght must be a non-negative number.  Output Length supplied::" + outputLength.ToString();
                TDException tde = new TDException(msg, true, TDExceptionIdentifier.PRHOutputLengthMustBeNonNegative );
                Trace.Write( new OperationalEvent( TDEventCategory.Infrastructure, TDTraceLevel.Error, msg, tde ) );
                throw tde;
            }

            // Set up object values
            if (useZPBO)
            {
                this.ObjectID = "JC";   // Object code for the ZPBO
                this.FunctionID = "F2"; // Function code to get fares incl Group and Zonal fares
            }
            else
            {
                this.ObjectID = "FA";   // Object code for the FBO
                this.FunctionID = "GF"; // Function code to get fares
            }

            this.InterfaceVersion = interfaceVersion;

            this.useZPBO = useZPBO;

            HeaderInputParameter inputParameter = new HeaderInputParameter( this.BuildFareRequest( request ) );
            this.AddInputParameter( inputParameter, 0 );

            HeaderOutputParameter outputParameter = new HeaderOutputParameter( outputLength );
            this.AddOutputParameter( outputParameter, 0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private string BuildFareRequest( PricingRequestDto request) 
        {
            StringBuilder fareRequest = new StringBuilder();
            // build Select clause
            fareRequest.Append(SELECT);
            fareRequest.Append(TICKET_TYPE).Append(ROUTE_CODE).Append(CROSS_LONDON);
            fareRequest.Append(RESTRICTION_CODE);
            fareRequest.Append(JOURNEY_TYPE).Append(TICKET_VAL_CODE).Append(RAILCARD_CODE);
            fareRequest.Append(ADULT_FARE).Append(ADULT_FARE_M);
            fareRequest.Append(CHILD_FARE).Append(CHILD_FARE_M);
            fareRequest.Append(CLASS).Append(TOC_CODE);

            if (useZPBO)
            {
                fareRequest.Append(ORIGIN_FARE);
                fareRequest.Append(DESTINATION_FARE);
            }
            
            fareRequest.Append(END_SELECT);

            // build Where clause
            fareRequest.Append(WHERE);

            fareRequest.Append(ORIGIN).Append("= ").Append(request.Origin.Nlc.ToString().PadRight(16,' '));
            fareRequest.Append(DESTINATION).Append("= ").Append(request.Destination.Nlc.ToString().PadRight(16,' '));
            TicketClassType ticketClass = (TicketClassType) Enum.Parse( typeof(TicketClassType), request.TicketClass.ToString() );
            if ( !ticketClass.Equals(TicketClassType.None) ) 
            {
                fareRequest.Append(CLASS).Append("= ").Append( ticketClass.ToString("D").PadRight(16,' '));
            }
            fareRequest.Append(OUTWARD_DATE).Append("= ").Append(request.OutwardDate.ToString("yyyyMMdd").PadRight(16,' '));

            //If the number of adults was supplied then append it
            if ( request.NumberOfAdults > 0 ) 
            {
                fareRequest.Append(NUM_ADULTS).Append("= ").Append(request.NumberOfAdults.ToString().PadRight(16,' '));
            }

            //If the number of children was supplied then append it
            if ( request.NumberOfChildren > 0 ) 
            {
                fareRequest.Append(NUM_CHILDREN).Append("= ").Append(request.NumberOfChildren.ToString().PadRight(16,' '));
            }

            //append the railcards supplied
            fareRequest.Append(RAILCARD_CODE).Append("= ").Append("".PadRight(16,' '));
            if (request.Railcard != null ) 
            {
                if (!request.Railcard.Trim().Equals("")) 
                {
                    fareRequest.Append(RAILCARD_CODE).Append("= ").Append(request.Railcard.PadRight(16,' '));
                }
            }
            fareRequest.Append(END_WHERE);

            // build Order by clause
            fareRequest.Append(ORDER_BY);
            fareRequest.Append(END_ORDER_BY);

            return fareRequest.ToString();
        }
    }
}
