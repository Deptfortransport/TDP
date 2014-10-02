//********************************************************************************
//NAME         : SupplementAdditionalDataRequest.cs
//AUTHOR       : Richard Philpott
//DATE CREATED : 2005-03-18
//DESCRIPTION  : BO input object used to request extra data 
//				  about a specific supplement from the SBO.   
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/RetailBusinessObjects/SupplementAdditionalDataRequest.cs-arc  $
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
	/// BO input object used to request extra data 
	///  about a specific supplement from the SBO.   
	/// </summary>
	public class SupplementAdditionalDataRequest : BusinessObjectInput
	{
		public SupplementAdditionalDataRequest(string interfaceVersion, Supplement supplement, TDDateTime journeyDate, 
													int outputLength) : base("SU", "GA", interfaceVersion )
		{
			if  (outputLength < 0) 
			{
				string msg = "Output Length must be a non-negative number.  Output Length supplied::" + outputLength.ToString();
				TDException tde = new TDException(msg, true, TDExceptionIdentifier.PRHOutputLengthMustBeNonNegative );
				Trace.Write( new OperationalEvent( TDEventCategory.Infrastructure, TDTraceLevel.Error, msg, tde ) );
				throw tde;
			}

			HeaderInputParameter inputParameter = new HeaderInputParameter(this.BuildRequestHeader(supplement, journeyDate));
			this.AddInputParameter(inputParameter, 0);

			HeaderOutputParameter outputParameter = new HeaderOutputParameter(outputLength);
			this.AddOutputParameter(outputParameter, 0);
		}


		private string BuildRequestHeader(Supplement supplement, TDDateTime journeyDate) 
		{
			StringBuilder sb = new StringBuilder();

			sb.Append("HEADER  ");
			sb.Append("S");			// record type
			sb.Append(supplement.SupplementCode);
			sb.Append(journeyDate.ToString("yyyyMMdd"));

			return sb.ToString();
		}

	}
}
