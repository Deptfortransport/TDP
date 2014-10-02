// *********************************************** 
// NAME                 : TravelineNewsTransactionGroup.cs
// AUTHOR               : Mark Turner
// DATE CREATED         : 14/01/2009 
// DESCRIPTION  : Factory class for creating a
// group of travel news transactions.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/TransactionInjector/TravelNewsTransactionGroup.cs-arc  $ 
//
//   Rev 1.2   Jul 16 2010 09:39:24   PScott
//IR 5561  -  Changes after review. Read injector machine name at load and persist through each group.
//Old way did it in part of object passed through to other servers and the webserver names were being picked up instead of injector name.
//Resolution for 5561: Capture All Transaction Injectors Data to file.
//
//   Rev 1.1   Jan 16 2009 11:54:10   mturner
//Fixed typo in desirialization code
//
//   Rev 1.0   Jan 14 2009 17:40:42   mturner
//Initial revision.
//Resolution for 5215: Workstream for RS620

using System;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;
using System.Text;
using System.IO;

using TransportDirect.Common;

namespace TransportDirect.ReportDataProvider.TransactionInjector
{
    /// <summary>
    /// Factory class for creating a
    /// group of travel news transactions.
    /// </summary>
    public class TravelNewsTransactionGroup : ITDTransactionGroup
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public TravelNewsTransactionGroup()
        {
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
                    new XmlSerializer(typeof(TravelNewsTransaction));

                serializer.UnknownNode += new XmlNodeEventHandler(UnknownNodeFound);
                serializer.UnknownAttribute += new XmlAttributeEventHandler(UnknownAttributeFound);

                foreach (string file in fileList)
                {
                    currentFile.Length = 0;
                    currentFile.Append(file);
                    TextReader reader = new StreamReader(file);
                    TravelNewsTransaction transaction = (TravelNewsTransaction)serializer.Deserialize(reader);
                    transaction.Initialise(webservice, frequency, timeout);
                    transaction.MachineName = machineName;
                    transactions.Add(transaction);
                    reader.Close();
                }

            }
            catch (Exception exception) // Catch all since no documentation.
            {
                throw new TDException(String.Format(Messages.Service_FailedCreatingTravelNews, currentFile.ToString(), exception.Message), false, TDExceptionIdentifier.RDPTransactionInjectorFailedCreatingTravelNews);
            }

        }

        /// <summary>
        /// Event Handler used for deserialisation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UnknownNodeFound(object sender, XmlNodeEventArgs e)
        {
            throw new TDException(String.Format(Messages.Service_XMLFormatIncorrect, "Unknown Node: " + e.Name + "\t" + e.Text), false, TDExceptionIdentifier.RDPTransactionInjectorFailedCreatingTravelNews);
        }

        /// <summary>
        /// Event handler used for deserialisation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UnknownAttributeFound(object sender, XmlAttributeEventArgs e)
        {
            System.Xml.XmlAttribute attr = e.Attr;
            throw new TDException(String.Format(Messages.Service_XMLFormatIncorrect, "Unknown attribute " + attr.Name + "='" + attr.Value + "'"), false, TDExceptionIdentifier.RDPTransactionInjectorFailedCreatingTravelNews);
        }
    }
}