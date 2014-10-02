// *********************************************** 
// NAME			: CycleRequestTransactionGroup.cs
// AUTHOR		: Mark Turner
// DATE CREATED	: 04/08/08 
// DESCRIPTION	:Factory class for creating a
// group of cycle request transactions.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/TransactionInjector/CycleRequestTransactionGroup.cs-arc  $
//
//   Rev 1.2   Jul 16 2010 09:39:38   PScott
//IR 5561  -  Changes after review. Read injector machine name at load and persist through each group.
//Old way did it in part of object passed through to other servers and the webserver names were being picked up instead of injector name.
//Resolution for 5561: Capture All Transaction Injectors Data to file.
//
//   Rev 1.1   Sep 10 2008 10:28:40   mturner
//Fixed bug in name of class to be serialized
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.0   Aug 04 2008 16:15:48   mturner
//Initial revision.
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream

using System;
using System.IO;
using System.Collections;
using System.Xml.Serialization;
using System.Xml;
using System.Diagnostics;
using System.Text;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.UserPortal.LocationService;

namespace TransportDirect.ReportDataProvider.TransactionInjector
{
    /// <summary>
    /// Factory class.
    /// </summary>
    public class CycleRequestTransactionGroup : ITDTransactionGroup
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        public CycleRequestTransactionGroup()
        {

        }

        private string ConvertToXML(object obj)
        {
            XmlSerializer xmls = new XmlSerializer(obj.GetType());
            StringWriter sw = new StringWriter();
            xmls.Serialize(sw, obj);
            sw.Close();
            return sw.ToString();
        }

        /// <summary>
        /// Used to creates transactions based on the type of group 
        /// using XML files from a source directory.
        /// </summary>
        /// <param name="dirPath">Path to source directory of transaction XML files used to create transactions.</param>
        /// <param name="transactions">Collection of transactions created.</param>
        /// <param name="webservice">Web service that should be associated with transactions in the group.</param>
        /// <param name="frequency">Frequency at which transactions in the group should be injected.</param>
        /// <param name="timeout">Duration after which a timeout should apply for transactions in the group.</param>
        /// <exception cref="TDException">Thrown if errors occur when creating the transactions.</exception>
        public void CreateTransactions(string dirPath,
                                       ArrayList transactions,
                                       TDTransactionServiceOverride webservice,
                                       TimeSpan frequency,
                                       TimeSpan timeout,
                                       string machineName)
        {
            StringBuilder currentFile = new StringBuilder(30);

            try
            {
                string[] fileList = Directory.GetFiles(dirPath, "*.xml");

                XmlSerializer serializer =
                    new XmlSerializer(typeof(CycleRequestTransaction));

                serializer.UnknownNode += new XmlNodeEventHandler(UnknownNodeFound);
                serializer.UnknownAttribute += new XmlAttributeEventHandler(UnknownAttributeFound);

                foreach (string file in fileList)
                {
                    currentFile.Length = 0;
                    currentFile.Append(file);
                    TextReader reader = new StreamReader(file);
                    CycleRequestTransaction transaction = (CycleRequestTransaction)serializer.Deserialize(reader);
                    transaction.Initialise(webservice, frequency, timeout);
                    transaction.MachineName = machineName;
                    transactions.Add(transaction);
                    reader.Close();
                }

            }
            catch (Exception exception) // Catch all since no documentation.
            {
                throw new TDException(String.Format(Messages.Service_FailedCreatingJourneyRequest, currentFile.ToString(), exception.Message), false, TDExceptionIdentifier.RDPTransactionInjectorFailedCreatingJourneyRequest);
            }

        }

        private void UnknownNodeFound(object sender, XmlNodeEventArgs e)
        {
            throw new TDException(String.Format(Messages.Service_XMLFormatIncorrect, "Unknown Node: " + e.Name + "\t" + e.Text), false, TDExceptionIdentifier.RDPTransactionInjectorFailedCreatingJourneyRequest);
        }

        private void UnknownAttributeFound(object sender, XmlAttributeEventArgs e)
        {
            System.Xml.XmlAttribute attr = e.Attr;
            throw new TDException(String.Format(Messages.Service_XMLFormatIncorrect, "Unknown attribute " + attr.Name + "='" + attr.Value + "'"), false, TDExceptionIdentifier.RDPTransactionInjectorFailedCreatingJourneyRequest);
        }

    }
}
