// ************************************************************** 
// NAME			: TDTransactionGroup.cs
// AUTHOR		: Jatinder S. Toor
// DATE CREATED	: 03/09/2003 
// DESCRIPTION	: Provides base class for transaction factories.
// ************************************************************** 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/TransactionInjector/TDTransactionGroup.cs-arc  $
//
//   Rev 1.1   Jul 16 2010 09:39:32   PScott
//IR 5561  -  Changes after review. Read injector machine name at load and persist through each group.
//Old way did it in part of object passed through to other servers and the webserver names were being picked up instead of injector name.
//Resolution for 5561: Capture All Transaction Injectors Data to file.
//
//   Rev 1.0   Nov 08 2007 12:40:04   mturner
//Initial revision.
//
//   Rev 1.5   Apr 23 2004 17:22:16   geaton
//IR827 - Allow timeout specification for transactions.
//
//   Rev 1.4   Feb 16 2004 17:26:26   geaton
//Incident 643. Store injection frequency of transactions within transaction classes.
//
//   Rev 1.3   Nov 06 2003 17:19:34   geaton
//Refactored following handover from JT to GE.

using System;
using System.Collections;

namespace TransportDirect.ReportDataProvider.TransactionInjector
{
	/// <summary>
	/// Base class for Transaction Factory classes.
	/// </summary>
	public interface ITDTransactionGroup
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
		void CreateTransactions(string dirPath,
								ArrayList transactions,
							    TDTransactionServiceOverride webservice,
							    TimeSpan frequency,
							    TimeSpan timeout,
                                string machineName);
		
	}
}
