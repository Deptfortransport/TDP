//********************************************************************************
//NAME         : ValidateSpecificFareRequest.cs
//AUTHOR       : SchlumbergerSema
//DATE CREATED : 10/05/2003
//DESCRIPTION  : 
//             : 
//DESIGN DOC   : DD034 Reatil Pricing
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/RetailBusinessObjects/LookupTicketTypeRequest.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:46:10   mturner
//Initial revision.
//
//   Rev 1.2   Jan 13 2004 13:13:22   CHosegood
//Now checks to see if a negative length output size has been supplied and throws an excpetion if this is the case.
//Resolution for 594: BO request do not check for a negative length output buffer.
//
//   Rev 1.1   Oct 14 2003 12:27:24   CHosegood
//Intermediate version.  Not ready for build/test
//
//   Rev 1.0   Oct 13 2003 15:25:46   CHosegood
//Initial Revision

#region using
using System;
using System.Diagnostics;
using System.Text;
using System.Collections;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.UserPortal.PricingMessages;
#endregion

namespace TransportDirect.UserPortal.RetailBusinessObjects
{
    /// <summary>
    /// Summary description for LookupTicketTypeRequest.
    /// </summary>
    public class LookupTicketTypeRequest : BusinessObjectInput
    {
        #region RBO query constants
        // name                                                             //length
        public static string SELECT             = "SELECT  ";               //8
        public static string WHERE              = "WHERE   ";               //8
        public static string ORDER_BY           = "ORDER BY";               //8

        public static string TTY_CAPRI_CODE     = "TTY-CAPRI-CODE  ";       //3
        public static string TTY_CODE           = "TTY-CODE        ";       //3
        public static string TTY_GROUP_TYPE     = "TTY-GROUP-TYPE  ";       //2
        public static string TTY_GROUP_CODE     = "TTY-GROUP-CODE-1";       //3
        public static string TTY_DESCRIPTION    = "TTY-DESCRIPTION ";       //15
        public static string TTY_RVR_DESC       = "TTY-RVR-DESC    ";       //30
        public static string TTY_TT_OR_RV       = "TTY-TT-OR-RV    ";       //1
        public static string TTY_TYPE           = "TTY-TYPE        ";       //1
        public static string TTY_RVR_VALID_MM   = "TTY-RVR-VALID-MM";       //3
        public static string TTY_RVR_VALID_DD   = "TTY-RVR-VALID-DD";       //3
        public static string TTY_RVR_DD_TRAV    = "TTY-RVR-DD-TRAV ";       //3
        public static string TTY_RVR_DEST       = "TTY-RVR-DEST    ";       //4
        #endregion
        /// <summary>
        /// 
        /// </summary>
        /// <param name="interfaceVersion"></param>
        /// <param name="outputLength"></param>
        /// <param name="ticketTypes"></param>
        public LookupTicketTypeRequest(string interfaceVersion,
            int outputLength, string ticketType)
            : base("LU", "GD", interfaceVersion )
        {

            if ( outputLength < 0 ) 
            {
                string msg = "Output Lenght must be a non-negative number.  Output Length supplied::" + outputLength.ToString();
                TDException tde = new TDException(msg, true, TDExceptionIdentifier.PRHOutputLengthMustBeNonNegative );
                Trace.Write( new OperationalEvent( TDEventCategory.Infrastructure, TDTraceLevel.Error, msg, tde ) );
                throw tde;
            }

            StringBuilder sb = new StringBuilder();
            sb.Append( "HEDIN   " );
            sb.Append( "TTY" );
            sb.Append( "A" );
            sb.Append( "00005" );
            sb.Append( " " );
            sb.Append( "00001" );

            HeaderInputParameter inputParameter = new HeaderInputParameter( sb.ToString() );
            this.AddInputParameter( inputParameter, 0 );

            inputParameter = new HeaderInputParameter( Select() );
            this.AddInputParameter( inputParameter, 1 );

            inputParameter = new HeaderInputParameter( Where( ticketType ) );
            this.AddInputParameter( inputParameter, 2 );

            inputParameter = new HeaderInputParameter( OrderBy() );
            this.AddInputParameter( inputParameter, 3 );

            HeaderOutputParameter outputParameter = new HeaderOutputParameter( 13 );
            this.AddOutputParameter( outputParameter, 0);

            outputParameter = new HeaderOutputParameter( outputLength );
            this.AddOutputParameter( outputParameter, 1);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string Select( )
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(SELECT);
            sb.Append( TTY_CODE );
            sb.Append( TTY_DESCRIPTION );
            sb.Append( TTY_TYPE );
            return sb.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ticketTypes"></param>
        /// <returns></returns>
        private string Where( string ticketType) 
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(WHERE);
            sb.Append(TTY_TT_OR_RV).Append("= ").Append( "T".ToString().PadRight(30,' ') );
            sb.Append(TTY_CODE).Append("= ").Append( ticketType.ToString().PadRight(30,' ') );

            return sb.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string OrderBy() 
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(ORDER_BY);
            return sb.ToString();
        }
    }
}
