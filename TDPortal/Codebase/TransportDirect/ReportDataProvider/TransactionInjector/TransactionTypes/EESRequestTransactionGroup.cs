// *********************************************** 
// NAME                 : EESRequestTransactionGroup.cs
// AUTHOR               : Mark Turner
// DATE CREATED         : 13/01/2009 
// DESCRIPTION  	: Factory class for creating a group of RTTI request info transactions.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/TransactionInjector/EESRequestTransactionGroup.cs-arc  $ 
//
//   Rev 1.3   Jul 16 2010 09:39:28   PScott
//IR 5561  -  Changes after review. Read injector machine name at load and persist through each group.
//Old way did it in part of object passed through to other servers and the webserver names were being picked up instead of injector name.
//Resolution for 5561: Capture All Transaction Injectors Data to file.
//
//   Rev 1.2   Jan 13 2009 14:43:58   mturner
//Further tech refresh updates
//Resolution for 5215: Workstream for RS620
//
//   Rev 1.1   Jan 13 2009 10:04:42   mturner
//Further updates for tech refresh
//
//   Rev 1.0   Jan 13 2009 09:57:50   mturner
//Initial revision.

using System;
using System.IO;
using System.Collections;
using System.Xml.Serialization;

using TransportDirect.Common;

namespace TransportDirect.ReportDataProvider.TransactionInjector
{
	/// <summary>
	/// Factory class.
	/// </summary>
	public class EESRequestTransactionGroup  : ITDTransactionGroup
	{
		public EESRequestTransactionGroup()
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
			string file = string.Empty;
			try
			{
				string[] fileList = Directory.GetFiles(dirPath, "*.xml");

				XmlSerializer serializer = new XmlSerializer(typeof(EESRequestTransaction));

				serializer.UnknownNode+= new XmlNodeEventHandler(UnknownNodeFound);
				serializer.UnknownAttribute+= new XmlAttributeEventHandler(UnknownAttributeFound);

				foreach (string eachFile in fileList)
				{				
					file = eachFile;
					TextReader reader = new StreamReader( file );
					EESRequestTransaction transaction = (EESRequestTransaction)serializer.Deserialize(reader);
					transaction.Initialise(webservice, frequency, timeout);
                    transaction.MachineName = machineName;
					transactions.Add( transaction );
					reader.Close();
				}		
	
			}
			catch (Exception exception) // Catch all since no documentation.
			{			
				throw new TDException(String.Format(Messages.Service_FailedCreatingEESRequest, file, exception.Message), false, TDExceptionIdentifier.RDPTransactionInjectorFailedCreatingEESInfo); 
			} 
		}

		private void UnknownNodeFound(object sender, XmlNodeEventArgs e)
		{
			throw new TDException(String.Format(Messages.Service_XMLFormatIncorrect, "Unknown Node: " + e.Name + "\t" + e.Text), false, TDExceptionIdentifier.RDPTransactionInjectorFailedCreatingEESInfo); 
		}

		private void UnknownAttributeFound(object sender, XmlAttributeEventArgs e)
		{
			System.Xml.XmlAttribute attr = e.Attr;
			throw new TDException(String.Format(Messages.Service_XMLFormatIncorrect, "Unknown attribute " + attr.Name + "='" + attr.Value + "'"), false, TDExceptionIdentifier.RDPTransactionInjectorFailedCreatingEESInfo); 
		}
	}
}
