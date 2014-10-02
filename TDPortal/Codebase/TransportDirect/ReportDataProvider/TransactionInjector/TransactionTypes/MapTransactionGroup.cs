// *********************************************** 
// NAME			: MapTransactionGroup.cs
// AUTHOR		: Peter Norell
// DATE CREATED	: 07/01/2004
// DESCRIPTION	: 
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/TransactionInjector/MapTransactionGroup.cs-arc  $
//
//   Rev 1.1   Jul 16 2010 09:39:30   PScott
//IR 5561  -  Changes after review. Read injector machine name at load and persist through each group.
//Old way did it in part of object passed through to other servers and the webserver names were being picked up instead of injector name.
//Resolution for 5561: Capture All Transaction Injectors Data to file.
//
//   Rev 1.0   Nov 08 2007 12:39:58   mturner
//Initial revision.
//
//   Rev 1.3   Apr 23 2004 17:22:02   geaton
//IR827 - Allow timeout specification for transactions.
//
//   Rev 1.2   Feb 16 2004 17:25:58   geaton
//Incident 643. Store injection frequency of transactions within transaction classes.
//
//   Rev 1.1   Jan 09 2004 12:41:12   PNorell
//Updated transactions.
//
//   Rev 1.0   Jan 08 2004 19:41:52   PNorell
//Initial Revision

using System;
using System.Collections;
using System.IO;
using System.Xml.Serialization;
using System.Xml;
using System.Diagnostics;
using System.Text;

using TransportDirect.Common;

namespace TransportDirect.ReportDataProvider.TransactionInjector
{
	/// <summary>
	/// Summary description for MapTransactionGroup.
	/// </summary>
	public class MapTransactionGroup : ITDTransactionGroup
	{
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
			string currentFile = string.Empty;
			try
			{
				string[] fileList = Directory.GetFiles(dirPath, "*.xml");

				XmlSerializer serializer = 
					new XmlSerializer(typeof(MapTransaction));

				foreach (string file in fileList)
				{	
					currentFile = file;
			
					StreamReader reader = new StreamReader( file );
					MapTransaction transaction = (MapTransaction)serializer.Deserialize(reader);
					transaction.Initialise(webservice, frequency, timeout);
                    transaction.MachineName = machineName;
                    transactions.Add(transaction);
					reader.Close();
				}		
	
			}
			catch (Exception exception) // Catch all since no documentation.
			{			
				throw new TDException(String.Format(Messages.Service_FailedCreatingMap, currentFile, exception.Message), false, TDExceptionIdentifier.RDPTransactionInjectorFailedCreatingStationInfo); 
			}

		}

	}
}
