//********************************************************************************
//NAME         : LookupNlcNameRequest.cs
//AUTHOR       : Richard Philpott
//DATE CREATED : 2006-04-03
//DESCRIPTION  : An LBO request to get the location name of a specified NLC code.
//********************************************************************************
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/RetailBusinessObjects/LookupNlcNameRequest.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:46:10   mturner
//Initial revision.
//
//   Rev 1.2   Apr 06 2006 19:11:46   RPhilpott
//FxCop fixes.
//Resolution for 35: DEL 8.1 Workstream - Find Cheaper (Rail)
//
//   Rev 1.1   Apr 05 2006 17:18:58   RPhilpott
//Associate with stream IR.
//Resolution for 35: DEL 8.1 Workstream - Find Cheaper (Rail)
//
//   Rev 1.0   Apr 05 2006 17:18:26   RPhilpott
//Initial revision.
//

using System;
using System.Text;
using System.Diagnostics;
using System.Collections;
using System.Globalization;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.UserPortal.PricingMessages;

namespace TransportDirect.UserPortal.RetailBusinessObjects
{
	/// <summary>
	/// An LBO request to get the location name of a specified NLC code.
	/// </summary>
	public class LookupNlcNameRequest : BusinessObjectInput
	{
		#region LBO query constants
 
		private static string SELECT             = "SELECT  ";               //8
		private static string WHERE              = "WHERE   ";               //8
		private static string ORDER_BY           = "ORDER BY";               //8

		private static string NLC_DESCRIPTION    = "NLC-DESCRIPTION ";		 //16
		private static string NLC_UIC_CODE		 = "NLC-UIC-CODE    ";	     //16
		private static string DATE_VALID         = "DATE-VALID      ";       //16
        
		#endregion

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="interfaceVersion"></param>
		/// <param name="outputLength"></param>
		/// <param name="nlc"></param>
		public LookupNlcNameRequest(string interfaceVersion,
			int outputLength, string nlc, TDDateTime date)
			: base("LU", "GD", interfaceVersion )
		{

			if (outputLength < 0) 
			{
				string msg = "Output Length must be a non-negative number.  Output Length supplied::" + outputLength.ToString(CultureInfo.InvariantCulture);
				TDException tde = new TDException(msg, true, TDExceptionIdentifier.PRHOutputLengthMustBeNonNegative );
				Trace.Write( new OperationalEvent( TDEventCategory.Infrastructure, TDTraceLevel.Error, msg, tde ) );
				throw tde;
			}

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
			sb.Append(NLC_DESCRIPTION);
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
			
			sb.Append(NLC_UIC_CODE).Append("= ").Append(uicNlc.ToString().PadRight(30,' '));
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

