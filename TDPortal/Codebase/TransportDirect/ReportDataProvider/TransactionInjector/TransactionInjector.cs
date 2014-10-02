// *************************************************************** 
// NAME			: TransactionInjector.cs
// AUTHOR		: Jatinder S. Toor
// DATE CREATED	: 03/09/2003 
// DESCRIPTION	: Injects transactions into the web service.
// *************************************************************** 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/TransactionInjector/TransactionInjector.cs-arc  $
//
//   Rev 1.3   Jul 16 2010 09:39:36   PScott
//IR 5561  -  Changes after review. Read injector machine name at load and persist through each group.
//Old way did it in part of object passed through to other servers and the webserver names were being picked up instead of injector name.
//Resolution for 5561: Capture All Transaction Injectors Data to file.
//
//   Rev 1.2   Mar 16 2009 12:24:06   build
//Automatically merged from branch for stream5215
//
//   Rev 1.1.1.4   Jan 16 2009 11:53:08   mturner
//Added travelnewstransaction group
//
//   Rev 1.1.1.3   Jan 14 2009 11:05:42   mturner
//Fixed bug in previous revision that could lead to the TI failing to ever inject any transactions if started at particular times.
//
//   Rev 1.1.1.2   Jan 13 2009 17:32:28   mturner
//Added code to only start a round of injections if the current number of minutes past the hour is divisible by the TransactionInjector.Frequency property divided by 60 (to adjust the property from seconds to minutes).
//
//   Rev 1.1.1.1   Jan 13 2009 13:58:06   mturner
//Further tech refresh updates
//
//   Rev 1.1.1.0   Jan 12 2009 16:26:46   mturner
//Changes for tech refresh
//Resolution for 5215: Workstream for RS620
//
//   Rev 1.1   Oct 13 2008 16:46:34   build
//Automatically merged from branch for stream5014
//
//   Rev 1.0.1.0   Aug 04 2008 16:38:08   mturner
//Added case for CycleRequests
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.0   Nov 08 2007 12:40:12   mturner
//Initial revision.
//
//   Rev 1.22   Apr 09 2005 15:18:36   schand
//Added case for RTTI request transation
//
//   Rev 1.21   Nov 17 2004 11:18:34   passuied
//addition for TravelineChecker transaction
//
//   Rev 1.20   Jun 21 2004 15:25:16   passuied
//Changes for del6-del5.4.1
//
//   Rev 1.19   Apr 23 2004 17:22:10   geaton
//IR827 - Allow timeout specification for transactions.
//
//   Rev 1.18   Feb 16 2004 17:30:48   geaton
//Incident 643. Clone transaction instances prior to injecting to ensure start time is logged correctly in situations where transaction duration exceeds the frequency. Also added functionality to configure gap and frequency.
//
//   Rev 1.17   Feb 12 2004 10:29:12   geaton
//Added helper methods to support unit test class TestTransactionInjector.
//
//   Rev 1.16   Jan 08 2004 19:43:24   PNorell
//Added new transactions to inject.
//
//   Rev 1.15   Dec 02 2003 20:08:22   geaton
//Changed strategy for submitting transactions - ALL transactions submitted at once.
//
//   Rev 1.14   Nov 13 2003 12:32:12   geaton
//Includes creation of pricing and station info transactions.
//
//   Rev 1.13   Nov 11 2003 19:15:40   geaton
//Create pricing transaction group.
//
//   Rev 1.12   Nov 06 2003 17:20:04   geaton
//Refactored following handover from JT to GE.

using System;
using System.Collections;
using System.Diagnostics;
using System.Threading;
using System.Security;
using System.Globalization;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.PropertyService.Properties;



namespace TransportDirect.ReportDataProvider.TransactionInjector
{
	/// <summary>
	/// Injects transactions into a web service hosted on the TD Portal web servers.
	/// </summary>
	public class TransactionInjector : IDisposable
	{
		private IPropertyProvider	propertyProvider	= null;
		private bool				disposed			= false;
		private ArrayList[]			transactions;
		private	Timer[][]			timers;
		private int					defaultTimeout = 30; // default of 30 seconds
		private string				serviceName = string.Empty;

		static private IPropertyProvider staticProvider;
		static private string staticServiceName;
        



		public static void Run()
		{
			Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, "In Run"));
			try
			{
				TransactionInjector ti = new TransactionInjector(staticProvider, staticServiceName);
				ti.Start();
			}
			catch( Exception e)
			{
				Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error , "Problem ["+e.Message+"]"));
			}
			Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, "Done Run"));
		}

		public static void InitStaticParameters(IPropertyProvider provider, string serviceName)
		{
			staticProvider = provider;
			staticServiceName = serviceName;
		}

		/// <summary>
		/// Gets the total number of reference transactions.
		/// </summary>
		/// <remarks>
		/// Supports unit tests.
		/// </remarks>
		public int TransactionCount
		{
			get
			{
				int total = 0;
				
				for (int i=0; i < this.transactions.Length; i++)
					total = total + (this.transactions[i].Count);
				
				return total;
			}
		}

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="propertyProvider">
		/// Property Provider that supplies configuration properties to service.
		/// Passed as a parameter to allow mock properties to be used in testing.
		/// </param>
		/// <exception cref="TDException">
		/// Thrown if errors occurred during construction.
		/// </exception>
		public TransactionInjector(IPropertyProvider propertyProvider, string serviceName)
		{
			this.propertyProvider = propertyProvider;
			this.serviceName = serviceName;
			CreateTransactions();
		}
		
		
		/// <summary>
		/// Start injecting transactions.
		/// </summary>	
		public void Start()
		{	
			// Determine if configuration specified that injection of 
			// transaction types should be staggered.
			int typeGapInt = 0;
			string typeGapString = this.propertyProvider[Keys.TransactionInjectorTypeGap];
			if (typeGapString != null)
			{
				try
				{
					typeGapInt = int.Parse(typeGapString);
				}
				catch(Exception)
				{
					Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, String.Format(Messages.Injector_InvalidTypeGapConfigured, typeGapString)));	
					
					// Continue processing using default.
					typeGapInt = 0;
				}
			}
						
			TimeSpan typeGapSpan = new TimeSpan(0,0,0,typeGapInt,0);

			// Determine if configuration specified that injection of 
			// transactions should be staggered.
			int transactionGapInt = 0;
			string transactionGapString = this.propertyProvider[Keys.TransactionInjectorTransactionGap];
			if (transactionGapString != null)
			{
				try
				{
					transactionGapInt = int.Parse(transactionGapString);
				}
				catch(Exception)
				{
					Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, String.Format(Messages.Injector_InvalidTransactionGapConfigured, transactionGapString)));	
					
					// Continue processing using default.
					transactionGapInt = 0;
				}
			}
						
			TimeSpan transactionGapSpan = new TimeSpan(0,0,0,transactionGapInt,0);

			if (typeGapSpan.Seconds > 0)
			{
				if (TDTraceSwitch.TraceWarning)
					Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Warning, String.Format(Messages.Injector_TypeGapConfigured, typeGapString)));
			}

			if (transactionGapSpan.Seconds > 0)
			{
				if (TDTraceSwitch.TraceWarning)
					Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Warning, String.Format(Messages.Injector_TransactionGapConfigured, transactionGapString)));
			}

			// Wait until the start of a minute before starting. 
			// This allows a full 60 seconds injection slippage for transactions that are reported by minute.
            // We also will only start injecting if the current minute is divisible by the frequency property.
            // This allows you to set 5 minutely frequencies that will also inject at 0, 5, 10 etc past the hour.
			string transactionInjectorTransactionOffset = string.Format(Keys.TransactionInjectorTransactionOffset, serviceName);
            int frequency = Convert.ToInt32(Properties.Current[Keys.TransactionInjectorFrequency]) / 60;
			int offset = Convert.ToInt32(propertyProvider[transactionInjectorTransactionOffset], CultureInfo.InvariantCulture); 
			while (DateTime.Now.Second != offset || (DateTime.Now.Minute % frequency != 0))
			{
				System.Threading.Thread.Sleep(50);
			}

			// Start repeated execution of transactions in all groups.
			for (int transactionGroupIndex=0; 
				transactionGroupIndex < this.transactions.Length;
				transactionGroupIndex++)
			{
				int transactionIndex = 0;

				// Inject all transactions in group:
				foreach (TDTransaction transaction in this.transactions[transactionGroupIndex])
				{
					timers[transactionGroupIndex][transactionIndex].Change(new TimeSpan(0), transaction.RepeatFrequency);
					transactionIndex++;

					// Stagger injection of next transaction, if configured to do so.
					try
					{
						if (transactionGapSpan.Seconds > 0)
							Thread.Sleep(transactionGapSpan);
					}
					catch (ArgumentOutOfRangeException argumentOutOfRangeException)
					{
						Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, String.Format(Messages.Injector_FailedInsertingTransactionGap, argumentOutOfRangeException.Message)));
					}
				}

				// Stagger injection of next transaction group, if configured to do so.
				try
				{
					if (typeGapSpan.Seconds > 0)
						Thread.Sleep(typeGapSpan);
				}
				catch (ArgumentOutOfRangeException argumentOutOfRangeException)
				{
					Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, String.Format(Messages.Injector_FailedInsertingTypeGap, argumentOutOfRangeException.Message)));
				}
			}
		}

		/// <summary>
		/// Stop injecting transactions.
		/// </summary>
		public void Stop()
		{
			for (int transactionGroupIndex = 0; 
				transactionGroupIndex < this.transactions.Length;
				transactionGroupIndex++)
			{
				
				for (int transactionIndex = 0; 
					transactionIndex < this.timers[transactionGroupIndex].Length;
					transactionIndex++)
				{
					timers[transactionGroupIndex][transactionIndex].Change(Timeout.Infinite, Timeout.Infinite);
				}

			}
		}

		/// <summary>
		/// Disposes of resources. 
		/// Can be called by clients (via Dispose()) or runtime (via destructor).
		/// </summary>
		/// <param name="disposing">
		/// True when called by clients.
		/// False when called by runtime.
		/// </param>
		public void Dispose(bool disposing)
		{
			// Check to see if Dispose has already been called.
			if (!this.disposed)
			{
				if (disposing)
				{
					for (int transactionGroupIndex = 0; 
						transactionGroupIndex < this.transactions.Length;
						transactionGroupIndex++)
					{
				
						for (int transactionIndex = 0; 
							transactionIndex < this.timers[transactionGroupIndex].Length;
							transactionIndex++)
						{
							timers[transactionGroupIndex][transactionIndex].Dispose();
						}

					}
						
				}
             
				// Dispose of any unmanaged resources:
				
			}

			this.disposed = true;
		}
	
		/// <summary>
		/// Disposes of pool resources.
		/// Allows clients to dispose of resources.
		/// </summary>
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this); // take off finalization queue to prevent dispose being called again.
		}

		/// <summary>
		/// Class destructor.
		/// </summary>
		~TransactionInjector()      
		{
			Dispose(false);
		}

		/// <summary>
		/// Creates Transaction class instances for each of the 
		/// transactions configured to be injected, using relevant factory class.
		/// Orders the Transactions classes according to given rule.
		/// </summary>
		/// <exception cref="TDException">
		/// Errors occurred when creating the transactions.
		/// </exception>
		private void CreateTransactions()
		{	
			int frequencyInt = 0;
			int timeoutInt = 0;

			// Get Property name to use according to which serviceName we are currently using
			string transactionInjectorTransaction = string.Format(Keys.TransactionInjectorTransactionType, serviceName);

			string[] idList = this.propertyProvider[transactionInjectorTransaction].Split(' ');												

			// create an array of lists, each list will hold transactions of single class.
			this.transactions = new ArrayList[idList.Length];
			for (int i=0; i < this.transactions.Length; i++)
				transactions[i] = new ArrayList();	
			
			// create a (jagged) array of timers for each transaction group.
			this.timers = new Timer[idList.Length][];

			// Create web service which will be called by transactions.
			TDTransactionServiceOverride webservice = 
				new TDTransactionServiceOverride(propertyProvider[Keys.TransactionInjectorWebService]);							
					
			// Determine global frequency (seconds).
			int globalFrequencyInt = int.Parse(propertyProvider[Keys.TransactionInjectorFrequency]);

			// Determine global timeout value (seconds).
			int globalTimeoutInt = this.defaultTimeout;
			string timeoutString = this.propertyProvider[Keys.TransactionInjectorTimeout];
			if (timeoutString != null)
			{
				try
				{
					globalTimeoutInt = int.Parse(timeoutString);
				}
				catch(Exception)
				{
					Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, String.Format(Messages.Injector_InvalidTimeoutConfigured, timeoutString, this.defaultTimeout.ToString())));	
					// Continue processing using default.
				}
			}

			// Define index to use for storing each group of transactions in an array.
			int transactionGroupIndex = 0;

			// Create transactions.
			foreach (string id in idList)
			{								
				string transClassKey  = string.Format(Keys.TransactionInjectorTransactionClass, id);	
				string transClassName = propertyProvider[transClassKey];
				
				// Detemine if a transaction type frequency (seconds) has been configured.
				int typeFrequencyInt = 0;
				string typeFrequencyString = this.propertyProvider[String.Format(Keys.TransactionInjectorTypeFrequency, id)];
				if (typeFrequencyString != null)
				{
					try
					{
						typeFrequencyInt = int.Parse(typeFrequencyString);
					}
					catch(Exception)
					{
						Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, String.Format(Messages.Injector_InvalidTypeFrequencyConfigured, typeFrequencyString, id)));	
					
						// Continue processing using default.
						typeFrequencyInt = 0;
					}
				}
				
				if (typeFrequencyInt == 0)
					frequencyInt = globalFrequencyInt;
				else
					frequencyInt = typeFrequencyInt;

				TimeSpan frequency = new TimeSpan(0,0,0,frequencyInt,0);

				// Detemine if a transaction type timeout (seconds) has been configured.
				int typeTimeoutInt = 0;
				string typeTimeoutString = this.propertyProvider[String.Format(Keys.TransactionInjectorTypeTimeout, id)];
				if (typeTimeoutString != null)
				{
					try
					{
						typeTimeoutInt = int.Parse(typeTimeoutString);
					}
					catch(Exception)
					{
						Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, String.Format(Messages.Injector_InvalidTypeTimeoutConfigured, typeTimeoutString, id)));	
						typeTimeoutInt = 0;
					}
				}
				
				if (typeTimeoutInt == 0)
					timeoutInt = globalTimeoutInt;
				else
					timeoutInt = typeTimeoutInt;

				TimeSpan timeout = new TimeSpan(0,0,0,timeoutInt,0);

                // Read machine Name
                string machineName;
                try
                {
                    machineName = System.Environment.MachineName;
                }
                catch (Exception e)
                {
                    machineName = "unknown";
                    Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, "Unknown Injector "+e.Message));	
                }


				// Create Transactions
				switch(transClassName)
				{
					case "JourneyRequestTransaction":
						JourneyRequestTransactionGroup journeyReqTransGrp = new JourneyRequestTransactionGroup();
						journeyReqTransGrp.CreateTransactions(propertyProvider[string.Format(Keys.TransactionInjectorTransactionPath, id)], this.transactions[transactionGroupIndex], webservice, frequency, timeout, machineName);										
						break;
					case "GazetteerTransaction":
						GazetteerTransactionGroup gazetteerTransGrp = new GazetteerTransactionGroup();
						gazetteerTransGrp.CreateTransactions( propertyProvider[string.Format(Keys.TransactionInjectorTransactionPath, id)], this.transactions[transactionGroupIndex], webservice, frequency, timeout,machineName);
						break;
					case "MapTransaction":
						MapTransactionGroup mapTransGrp = new MapTransactionGroup();
						mapTransGrp.CreateTransactions( propertyProvider[string.Format(Keys.TransactionInjectorTransactionPath, id)], this.transactions[transactionGroupIndex], webservice, frequency, timeout,machineName);
						break;
					case "TravelineCheckerTransaction":
						TravelineCheckerTransactionGroup tcTransGrp = new TravelineCheckerTransactionGroup();
						tcTransGrp.CreateTransactions( propertyProvider[string.Format(Keys.TransactionInjectorTransactionPath, id)], this.transactions[transactionGroupIndex], webservice, frequency, timeout,machineName);
						break;
					case "EESRequestTransaction":
						EESRequestTransactionGroup EESTransGrp = new EESRequestTransactionGroup();
						EESTransGrp.CreateTransactions(propertyProvider[string.Format(Keys.TransactionInjectorTransactionPath, id)], this.transactions[transactionGroupIndex], webservice, frequency, timeout,machineName); 
						break;
                    case "CycleRequestTransaction":
                        CycleRequestTransactionGroup crTransGrp = new CycleRequestTransactionGroup();
                        crTransGrp.CreateTransactions(propertyProvider[string.Format(Keys.TransactionInjectorTransactionPath, id)], this.transactions[transactionGroupIndex], webservice, frequency, timeout,machineName); 
                        break;
                    case "TravelNewsTransaction":
                        TravelNewsTransactionGroup tnTransGrp = new TravelNewsTransactionGroup();
                        tnTransGrp.CreateTransactions(propertyProvider[string.Format(Keys.TransactionInjectorTransactionPath, id)], this.transactions[transactionGroupIndex], webservice, frequency, timeout,machineName);
                        break;
					default:
						throw new TDException(String.Format(Messages.Service_UnknownTransactionClass, transClassName), false, TDExceptionIdentifier.RDPTransactionInjectorUnknownTransactionClass);
				}
			
				// create an array of timers for transactions in the current group.
				this.timers[transactionGroupIndex] = new Timer[this.transactions[transactionGroupIndex].Count];

				int transactionIndex = 0;

				// Create a timer for each transaction in the group.
				foreach (TDTransaction transaction in this.transactions[transactionGroupIndex])
				{
					try
					{
						timers[transactionGroupIndex][transactionIndex] 
							= new Timer(new TimerCallback(this.InjectTransaction),
							(object)transaction,
							Timeout.Infinite,
							Timeout.Infinite);
					}
					catch (ArgumentOutOfRangeException argumentOutOfRangeException)
					{
						throw new TDException(String.Format(Messages.Injector_FailedCreatingTimer, argumentOutOfRangeException.Message), false, TDExceptionIdentifier.Undefined);
					}
					catch (ArgumentNullException argumentNullException)
					{
						throw new TDException(String.Format(Messages.Injector_FailedCreatingTimer, argumentNullException.Message), false, TDExceptionIdentifier.Undefined);
					}

					transactionIndex++;
				}


				transactionGroupIndex++;
			}
	
		}

		/// <summary>
		/// Timer delegate that injects a clone of the transaction passed.
		/// </summary>
		/// <param name="transaction">
		/// The TDTransaction instance from which to create a clone and then inject.
		/// </param>
		private void InjectTransaction(Object transaction)
		{								
			if (TDTraceSwitch.TraceVerbose)
				Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, ((TDTransaction)transaction).SessionId, TDTraceLevel.Verbose, String.Format(Messages.Injector_InjectedTransaction, ((TDTransaction)transaction).Category.ToString())));

			(((TDTransaction)transaction).Clone(true)).ExecuteTransaction();
		}

	}

}













