//********************************************************************************
//NAME         : LookupLocationGroupRequest.cs
//AUTHOR       : SchlumbergerSema
//DATE CREATED : 10/05/2003
//DESCRIPTION  : 
//             : 
//DESIGN DOC   : DD034 Reatil Pricing
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/RetailBusinessObjects/LookupLocationGroupRequest.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:46:08   mturner
//Initial revision.
//
//   Rev 1.3   Nov 24 2005 18:20:58   RPhilpott
//1) Use NLC lookups, not GRP.
//
//2) Generalise to get stations in a group as well as v.v.
//Resolution for 3198: DN040: Find-A-Fare - handling of non-group tickets
//
//   Rev 1.2   Apr 25 2005 21:12:46   RPhilpott
//Change LBO lookups to get all groups to which a location belongs, and remove associated redundant code.
//Resolution for 2328: PT - fares between Three Bridges and Victoria
//
//   Rev 1.1   Jan 13 2004 13:13:20   CHosegood
//Now checks to see if a negative length output size has been supplied and throws an excpetion if this is the case.
//Resolution for 594: BO request do not check for a negative length output buffer.
//
//   Rev 1.0   Jan 09 2004 16:15:06   CHosegood
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
	/// A LBO request that will determine the station group ( or cluster ) a
	/// station belongs to if one exists.
	/// </summary>
	/// 
	public class LookupLocationGroupRequest : BusinessObjectInput
	{
        #region LBO query constants
 
		private static string SELECT             = "SELECT  ";               //8
        private static string WHERE              = "WHERE   ";               //8
        private static string ORDER_BY           = "ORDER BY";               //8

		private static string NLC_GROUP_UIC      = "NLC-GROUP-UIC   ";		 //16
		private static string NLC_CRS_CODE       = "NLC-CRS-CODE    ";		 //16
		private static string NLC_UIC_CODE		 = "NLC-UIC-CODE    ";	     //16
        private static string DATE_VALID         = "DATE-VALID      ";       //16
        
        #endregion

		private bool fromIndividualToGroup = false;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="interfaceVersion"></param>
        /// <param name="outputLength"></param>
        /// <param name="nlc"></param>
        public LookupLocationGroupRequest(string interfaceVersion,
            int outputLength, string nlc, TDDateTime date, bool fromIndividualToGroup)
            : base("LU", "GD", interfaceVersion )
        {

            if (outputLength < 0) 
            {
                string msg = "Output Length must be a non-negative number.  Output Length supplied::" + outputLength.ToString();
                TDException tde = new TDException(msg, true, TDExceptionIdentifier.PRHOutputLengthMustBeNonNegative );
                Trace.Write( new OperationalEvent( TDEventCategory.Infrastructure, TDTraceLevel.Error, msg, tde ) );
                throw tde;
            }

			this.fromIndividualToGroup = fromIndividualToGroup;

            StringBuilder sb = new StringBuilder();
            sb.Append( "HEDIN   " );
            sb.Append( "NLC" );
            sb.Append( "A" );
            sb.Append( "99999" );
            sb.Append( " " );
            sb.Append( "00001" );

            //Add the input header to the request
            HeaderInputParameter inputParameter = new HeaderInputParameter( sb.ToString() );
            this.AddInputParameter( inputParameter, 0 );

            //Add the select to the request
            inputParameter = new HeaderInputParameter( Select() );
            this.AddInputParameter( inputParameter, 1 );

            //Add the where to the request
            inputParameter = new HeaderInputParameter( Where( nlc, date ) );
            this.AddInputParameter( inputParameter, 2 );

            //Add the order by to the request
            inputParameter = new HeaderInputParameter( OrderBy() );
            this.AddInputParameter( inputParameter, 3 );

            //The output header
            HeaderOutputParameter outputParameter = new HeaderOutputParameter( 13 );
            this.AddOutputParameter( outputParameter, 0);

            //Response
            outputParameter = new HeaderOutputParameter( outputLength );
            this.AddOutputParameter( outputParameter, 1);
        }


        #region Query

        /// <summary>
        /// Returns the select portion of the lookup request
        /// </summary>
        /// <returns></returns>
        private string Select( )
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(SELECT);

			if	(fromIndividualToGroup)
			{
				sb.Append(NLC_GROUP_UIC);
			}
			else
			{
				sb.Append(NLC_UIC_CODE);
				sb.Append(NLC_CRS_CODE);
			}

			return sb.ToString();
        }

        /// <summary>
        /// Returns the where portion of the lookup request including
        /// the supplied parameters
        /// </summary>
        /// <param name="nlc"></param>
        /// <returns></returns>
        private string Where(string nlc, TDDateTime date) 
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(WHERE);
			
			StringBuilder uicNlc = new StringBuilder();
			uicNlc.Append("70 ");
			uicNlc.Append(nlc.PadRight(4, ' '));
			uicNlc.Append("00");
			
			sb.Append(fromIndividualToGroup ? NLC_UIC_CODE : NLC_GROUP_UIC).Append("= ").Append(uicNlc.ToString().PadRight(30,' '));
            sb.Append(DATE_VALID).Append("= ").Append(date.ToString("yyyyMMdd").PadRight(16,' '));
            return sb.ToString();
        }

        /// <summary>
        /// Returns the order by portion of the lookup request
        /// </summary>
        /// <returns></returns>
        private string OrderBy() 
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(ORDER_BY);
            return sb.ToString();
        }
        #endregion

	}
}
