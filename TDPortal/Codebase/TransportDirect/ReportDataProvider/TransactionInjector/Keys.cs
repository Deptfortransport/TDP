// *********************************************** 
// NAME			: Keys.cs
// AUTHOR		: Jatinder S. Toor
// DATE CREATED	: 03/09/2003 
// DESCRIPTION	: Implementation of the Keys class.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/TransactionInjector/Keys.cs-arc  $
//
//   Rev 1.1   Mar 16 2009 12:24:04   build
//Automatically merged from branch for stream5215
//
//   Rev 1.0.1.0   Jan 21 2009 10:10:24   mturner
//Updates for tech refresh
//Resolution for 5215: Workstream for RS620
//
//   Rev 1.0   Nov 08 2007 12:39:58   mturner
//Initial revision.
//
//   Rev 1.8   Oct 20 2004 17:35:20   rhopkins
//Additional Keys used to allow injection interval to be specified in milliseconds
//
//   Rev 1.7   Jun 21 2004 15:25:12   passuied
//Changes for del6-del5.4.1
//
//   Rev 1.6   Apr 23 2004 17:21:58   geaton
//IR827 - Allow timeout specification for transactions.
//
//   Rev 1.5   Feb 16 2004 17:33:06   geaton
//Incident 643.
//
//   Rev 1.4   Nov 10 2003 18:13:12   geaton
//Added key for alerts.

using System;

namespace TransportDirect.ReportDataProvider.TransactionInjector
{
	/// <summary>
	/// Container class used to hold TransactionInjector specific key constants
	/// </summary>
	public class Keys
	{
		public const string TransactionInjectorTransaction			 = "TransactionInjector.Transaction";
		public const string TransactionInjectorTransactionType		 = "TransactionInjector.{0}.Transaction";
		public const string TransactionInjectorTransactionOffset	 = "TransactionInjector.{0}.Offset";
		public const string TransactionInjectorTransactionFrequency	 = "TransactionInjector.{0}.Frequency";
		public const string TransactionInjectorTransactionFrequencyMilliseconds	 = "TransactionInjector.{0}.Milliseconds";
		public const string TransactionInjectorTransactionClass		 = TransactionInjectorTransaction + ".{0}.Class";	
		public const string TransactionInjectorTransactionPath		 = TransactionInjectorTransaction + ".{0}.Path";	
		public const string TransactionInjectorFrequency			 = "TransactionInjector.Frequency";			
		public const string TransactionInjectorWebService			 = "TransactionInjector.WebService";
		public const string TransactionInjectorTemplateFileDirectory = "TransactionInjector.TemplateFileDirectory";
		public const string TransactionInjectorDefaultLogFilepath	 = "DefaultLogFilepath";
		public const string TransactionInjectorAlertAmberThreshold	 = "TransactionInjector.AlertThreshold.{0}.AmberThreshold";
        public const string TransactionInjectorAlertRedThreshold     = "TransactionInjector.AlertThreshold.{0}.RedThreshold";
		public const string TransactionInjectorTypeGap				 = "TransactionInjector.TypeGap";
		public const string TransactionInjectorTransactionGap		 = "TransactionInjector.TransactionGap";
		public const string TransactionInjectorTypeFrequency		 = "TransactionInjector.Transaction.{0}.Frequency";
		public const string TransactionInjectorTimeout				 = "TransactionInjector.Timeout";
		public const string TransactionInjectorTypeTimeout			 = "TransactionInjector.Transaction.{0}.Timeout";


	}
}