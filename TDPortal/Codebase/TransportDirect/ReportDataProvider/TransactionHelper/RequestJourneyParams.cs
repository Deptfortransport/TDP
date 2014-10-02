// *********************************************** 
// NAME			: RequestJourneyParams.cs
// AUTHOR		: Jatinder S. Toor
// DATE CREATED	: 07/10/2003 
// DESCRIPTION	: Provides conversion methods to
// convert Transaction Injector parameters between
// string format and binary format. This is necessary
// in order to ensure parameters are passed to 
// TD Transaction Web Service correctly.
// (Data loss was encountered when passing the 
// TDJourneyRequest class as XML to web service.)
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/TransactionHelper/RequestJourneyParams.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:39:42   mturner
//Initial revision.
//
//   Rev 1.4   Nov 12 2003 20:17:12   geaton
//Removed redundant using statement.
//
//   Rev 1.3   Nov 04 2003 19:04:02   geaton
//Initial version.

using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.Common;

namespace TransportDirect.ReportDataProvider.TransactionHelper
{
	[Serializable()]
	public struct RequestJourneyParams
	{
		public TDJourneyRequest request;
		public int				minNumberOutwardRoadJourneyCount;
		public int				minNumberReturnRoadJourneyCount;
		public int				minNumberOutwardPublicJourneyCount;
		public int				minNumberReturnPublicJourneyCount;
		public DateTime			dtOutwardDateTime;
		public DateTime			dtReturnDateTime;
		public string			sessionId; 
	}

	public class RequestJourneyParamsUtil
	{
		/// <summary>
		/// Converts the parameters to binary string format.
		/// </summary>
		/// <param name="param">Parameters to convert.</param>
		/// <returns>Parameters in binary string format.</returns>
		/// <exception cref="TDException">
		/// Error occurred during conversion.
		/// </exception>
		public static string ToBase64Str( RequestJourneyParams param )
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
				throw new TDException("Error converting RequestJourneyParams from class to binary: " + formatException.Message, false, TDExceptionIdentifier.RDPTransactionServiceConversionToBinaryError);
			}
			catch (ArgumentNullException argumentNullException)
			{
				throw new TDException("Error converting RequestJourneyParams from class to binary: " + argumentNullException.Message, false, TDExceptionIdentifier.RDPTransactionServiceConversionToBinaryError);
			}
			catch (SerializationException serializationException)
			{
				throw new TDException("Error converting RequestJourneyParams from class to binary: " + serializationException.Message, false, TDExceptionIdentifier.RDPTransactionServiceConversionToBinaryError);
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
		public static RequestJourneyParams FromBase64Str( string param )
		{
			RequestJourneyParams paramsClass;

			try
			{
				byte[] bytes = Convert.FromBase64String( param );
				MemoryStream ms = new MemoryStream( bytes );			
				IFormatter formatter = new BinaryFormatter();			
				paramsClass = (RequestJourneyParams)formatter.Deserialize( ms );
			}
			catch (FormatException formatException)
			{
				throw new TDException("Error converting RequestJourneyParams from binary to class: " + formatException.Message, false, TDExceptionIdentifier.RDPTransactionServiceConversionToClassError);
			}
			catch (ArgumentNullException argumentNullException)
			{
				throw new TDException("Error converting RequestJourneyParams from binary to class: " + argumentNullException.Message, false, TDExceptionIdentifier.RDPTransactionServiceConversionToClassError);
			}
			catch (SerializationException serializationException)
			{
				throw new TDException("Error converting RequestJourneyParams from binary to class: " + serializationException.Message, false, TDExceptionIdentifier.RDPTransactionServiceConversionToClassError);
			}

			return paramsClass;
		}
	}
}
