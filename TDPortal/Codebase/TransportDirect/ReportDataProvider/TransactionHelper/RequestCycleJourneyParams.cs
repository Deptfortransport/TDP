// *********************************************** 
// NAME			: RequestCycleJourneyParams.cs
// AUTHOR		: Mark Turner
// DATE CREATED	: 04/08/2008 
// DESCRIPTION	: Provides conversion methods to
// convert Transaction Injector parameters between
// string format and binary format. This is necessary
// in order to ensure parameters are passed to 
// TD Transaction Web Service correctly.
// (Data loss was encountered when passing the 
// TDCycleJourneyRequest class as XML to web service.)
// ************************************************ 
//
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/TransactionHelper/RequestCycleJourneyParams.cs-arc  $
//
//   Rev 1.0   Aug 04 2008 16:53:42   mturner
//Initial revision.
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream

using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

using TransportDirect.UserPortal.CyclePlannerControl;
using TransportDirect.UserPortal.CyclePlannerService;
using TransportDirect.Common;

namespace TransportDirect.ReportDataProvider.TransactionHelper
{
    [Serializable()]
    public struct RequestCycleJourneyParams
    {
        public ITDCyclePlannerRequest request;
        public int minNumberOutwardCycleJourneyCount;
        public int minNumberReturnCycleJourneyCount;
        public DateTime dtOutwardDateTime;
        public DateTime dtReturnDateTime;
        public string sessionId;
    }

    public class RequestCycleJourneyParamsUtil
    {
        /// <summary>
        /// Converts the parameters to binary string format.
        /// </summary>
        /// <param name="param">Parameters to convert.</param>
        /// <returns>Parameters in binary string format.</returns>
        /// <exception cref="TDException">
        /// Error occurred during conversion.
        /// </exception>
        public static string ToBase64Str(RequestCycleJourneyParams param)
        {
            MemoryStream ms = new MemoryStream();
            IFormatter formatter = new BinaryFormatter();
            string paramsBase64 = String.Empty;

            try
            {
                formatter.Serialize(ms, param);
                byte[] bytes = ms.GetBuffer();
                int count = bytes.Length;
                paramsBase64 = Convert.ToBase64String(bytes);
            }
            catch (FormatException formatException)
            {
                throw new TDException("Error converting RequestCycleJourneyParams from class to binary: " + formatException.Message, false, TDExceptionIdentifier.RDPTransactionServiceConversionToBinaryError);
            }
            catch (ArgumentNullException argumentNullException)
            {
                throw new TDException("Error converting RequestCycleJourneyParams from class to binary: " + argumentNullException.Message, false, TDExceptionIdentifier.RDPTransactionServiceConversionToBinaryError);
            }
            catch (SerializationException serializationException)
            {
                throw new TDException("Error converting RequestCycleJourneyParams from class to binary: " + serializationException.Message, false, TDExceptionIdentifier.RDPTransactionServiceConversionToBinaryError);
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
        public static RequestCycleJourneyParams FromBase64Str(string param)
        {
            RequestCycleJourneyParams paramsClass;

            try
            {
                byte[] bytes = Convert.FromBase64String(param);
                MemoryStream ms = new MemoryStream(bytes);
                IFormatter formatter = new BinaryFormatter();
                paramsClass = (RequestCycleJourneyParams)formatter.Deserialize(ms);
            }
            catch (FormatException formatException)
            {
                throw new TDException("Error converting RequestCycleJourneyParams from binary to class: " + formatException.Message, false, TDExceptionIdentifier.RDPTransactionServiceConversionToClassError);
            }
            catch (ArgumentNullException argumentNullException)
            {
                throw new TDException("Error converting RequestCycleJourneyParams from binary to class: " + argumentNullException.Message, false, TDExceptionIdentifier.RDPTransactionServiceConversionToClassError);
            }
            catch (SerializationException serializationException)
            {
                throw new TDException("Error converting RequestCycleJourneyParams from binary to class: " + serializationException.Message, false, TDExceptionIdentifier.RDPTransactionServiceConversionToClassError);
            }

            return paramsClass;
        }
    }
}
