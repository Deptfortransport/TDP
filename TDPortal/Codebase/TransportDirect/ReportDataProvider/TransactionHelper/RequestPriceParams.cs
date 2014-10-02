// **************************************************************
// NAME			: RequestPriceParams.cs
// AUTHOR		: Gary Eaton
// DATE CREATED	: 11/11/2003 
// DESCRIPTION	: Implementation of the RequestPriceParams class.
// ************************************************************** 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/TransactionHelper/RequestPriceParams.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:39:44   mturner
//Initial revision.
//
//   Rev 1.5   Nov 13 2003 11:34:28   geaton
//Added support for relative datetimes.
//
//   Rev 1.4   Nov 12 2003 20:25:14   geaton
//Marked as serialisable to allow conversion to string.
//
//   Rev 1.3   Nov 12 2003 20:21:10   geaton
//Corrected utils class name.
//
//   Rev 1.2   Nov 12 2003 20:18:12   geaton
//Initial version - follows problems when passing pricing parameters to web service in class form - passing as a string removes this problem.
//
//   Rev 1.1   Nov 12 2003 16:09:38   geaton
//Initial version.
//
//   Rev 1.0   Nov 11 2003 18:55:06   geaton
//Initial Revision

using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.PricingRetail.Domain;
using TransportDirect.Common;

namespace TransportDirect.ReportDataProvider.TransactionHelper
{
	[Serializable()]
	public struct RequestPriceParams
	{
		public PublicJourney outwardJourney;
		public PublicJourney returnJourney;
		public bool returnRequired;
		public Discounts discounts;
		public int minNumberOutwardPriceUnits;
		public int minNumberReturnPriceUnits;
		public DateTime	dtOutwardDateTime;
		public DateTime	dtReturnDateTime;
	}

	public class RequestPriceParamsUtil
	{
		/// <summary>
		/// Converts the parameters to binary string format.
		/// This allows web service to be called without serialization issues.
		/// </summary>
		/// <param name="param">Parameters to convert.</param>
		/// <returns>Parameters in binary string format.</returns>
		/// <exception cref="TDException">
		/// Error occurred during conversion.
		/// </exception>
		public static string ToBase64Str( RequestPriceParams param )
		{
			MemoryStream ms = new MemoryStream();	
			IFormatter formatter = new BinaryFormatter();
			string paramsBase64 = String.Empty;

			try
			{
				formatter.Serialize(ms, param);
				byte[] bytes = ms.GetBuffer();
				int    count = bytes.Length;
				paramsBase64 = Convert.ToBase64String(bytes);
			}
			catch (FormatException formatException)
			{
				throw new TDException("Error converting RequestPriceParams from class to binary: " + formatException.Message, false, TDExceptionIdentifier.RDPTransactionServiceConversionToBinaryError);
			}
			catch (ArgumentNullException argumentNullException)
			{
				throw new TDException("Error converting RequestPriceParams from class to binary: " + argumentNullException.Message, false, TDExceptionIdentifier.RDPTransactionServiceConversionToBinaryError);
			}
			catch (SerializationException serializationException)
			{
				throw new TDException("Error converting RequestPriceParams from class to binary: " + serializationException.Message, false, TDExceptionIdentifier.RDPTransactionServiceConversionToBinaryError);
			}
			
			
			return paramsBase64;
		}

		/// <summary>
		/// Converts binary string format parameter to a class.
		/// </summary>
		/// <param name="param">Binary string to convert to a class.</param>
		/// <returns>Parameter as a class.</returns>
		/// <exception cref="TDException">
		/// Error occurred during conversion.
		/// </exception>
		public static RequestPriceParams FromBase64Str( string param )
		{
			RequestPriceParams paramsClass;

			try
			{
				byte[] bytes = Convert.FromBase64String( param );
				MemoryStream ms = new MemoryStream( bytes );			
				IFormatter formatter = new BinaryFormatter();			
				paramsClass = (RequestPriceParams)formatter.Deserialize( ms );
			}
			catch (FormatException formatException)
			{
				throw new TDException("Error converting RequestPriceParams from binary to class: " + formatException.Message, false, TDExceptionIdentifier.RDPTransactionServiceConversionToClassError);
			}
			catch (ArgumentNullException argumentNullException)
			{
				throw new TDException("Error converting RequestPriceParams from binary to class: " + argumentNullException.Message, false, TDExceptionIdentifier.RDPTransactionServiceConversionToClassError);
			}
			catch (SerializationException serializationException)
			{
				throw new TDException("Error converting RequestPriceParams from binary to class: " + serializationException.Message, false, TDExceptionIdentifier.RDPTransactionServiceConversionToClassError);
			}

			return paramsClass;
		}
	}
}
