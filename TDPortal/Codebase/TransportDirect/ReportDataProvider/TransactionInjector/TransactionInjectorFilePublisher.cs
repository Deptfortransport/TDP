// **************************/************************************* 
// NAME			: TransactionInjectorFilePublisher.cs
// AUTHOR		: Peter Norell
// DATE CREATED	: 19/12/2003 
// DESCRIPTION	: Publisher writing events in an easy way to import to a database.
// *************************************************************** 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/TransactionInjector/TransactionInjectorFilePublisher.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:40:14   mturner
//Initial revision.
//
//   Rev 1.3   Feb 16 2004 15:39:48   geaton
//Added thread-safety measures.
//
//   Rev 1.2   Jan 14 2004 09:26:02   geaton
//Corrected format of temporal data published to allow successful import into SQL server database.
//
//   Rev 1.1   Jan 08 2004 19:43:22   PNorell
//Added new transactions to inject.
//
//   Rev 1.0   Dec 19 2003 13:38:54   PNorell
//Initial Revision
using System;
using System.IO;
using System.Globalization;
using System.Collections;
using System.Text;

using TransportDirect.Common.Logging;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common;

using TransportDirect.ReportDataProvider.TDPCustomEvents;

namespace TransportDirect.ReportDataProvider.TransactionInjector
{
	/// <summary>
	/// Custom file publisher for use by the Transaction Injector component.
	/// Originally developed as fallback option when Event Receiver could not receive from Injector queue.
	/// </summary>
	public class TransactionInjectorFilePublisher : IEventPublisher
	{
		#region Variable declaration
		private string identifier = string.Empty;
		private string filenameFormat = "DD";
		private TimeSpan timespan;
		private DateTime timeout;

		private StreamWriter swOE = null;
		private StreamWriter swRte = null;

		// Default directory
		private string directory = "C:\temp";
		#endregion

		#region Constant definitions
		private const string PROP_NAME = "Logging.Publisher.Custom.TinjFP.Name";
		private const string PROP_DIR = "Logging.Publisher.Custom.TinjFP.Directory";
		private const string PROP_TIMESPAN = "Logging.Publisher.Custom.TinjFP.TimeSpanMins";
		private const string PROP_FILEFORMAT = "Logging.Publisher.Custom.TinjFP.FileFormat";

		private const string PREFIX_OE = "operationalevents";
		private const string PREFIX_RTE = "referenceevents";

		private const string TIME_FORMAT = "yyyy-MM-dd HH:mm:ss.fff000000";
		private const string DB_FORMAT_OE = "1,'{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}'";
		private const string DB_FORMAT_RTE = "1,'{0}','{1}','{2}','{3}','{4}','{5}'";
		#endregion

		#region IEventPublisher definitions
		/// <summary>
		/// Gets the identifier associated with this publisher.
		/// This identifier is used to tie events with this publisher.
		/// This identifier must match with that used in the configuration properties
		/// </summary>
		public string Identifier
		{
			get {return identifier;}
		}

	
		/// <summary>
		/// Publishes the event <c>LogEvent</c> passed.
		/// </summary>
		/// <remarks>
		/// This publisher is only able to publish custom events
		/// of type <c>EmailAttachmentPublisher</c>.
		/// </remarks>
		/// <exception cref="TDException">
		/// Thrown if an unsupported <c>LogEvent</c> type is passed 
		/// or an error occurs publishing the event passed.
		/// </exception>
		/// <param name="logEvent">Event to be published.</param>
		public void WriteEvent(LogEvent logEvent)
		{
			lock (this)
			{
				if( DateTime.Now > timeout )
				{
					// Rotate first
					RotateWriters();
				}
				string[] insertValues = {};
				string formatter = null;
				StreamWriter writer = null;
				
				if (logEvent is OperationalEvent)
				{
					try
					{
						OperationalEvent oe = (OperationalEvent)logEvent;
						// Log this to the OE event log in suitable format
						string target = oe.Target == null ? string.Empty : oe.Target.ToString();
						insertValues = new string[] {
														oe.SessionId,
														oe.Message,
														oe.MachineName,
														oe.AssemblyName,
														oe.MethodName,
														oe.TypeName,
														oe.Level.ToString(),
														oe.Category.ToString(),
														target,
														oe.Time.ToString(TIME_FORMAT)
													};
						// Oe formatter	"'{0}','{1}','{2}','{3}','{4}',etc
						formatter = DB_FORMAT_OE;
						writer = swOE;
					} 
					catch( Exception e)
					{
						throw new TDException("Failed to publish operational event using the Transaction Injector File Publisher", e, false, TDExceptionIdentifier.Undefined);
					}

				}
				else if( logEvent is ReferenceTransactionEvent )
				{
					ReferenceTransactionEvent rte = (ReferenceTransactionEvent)logEvent;
					
					// Log this to the RTE event log in suitable format
					insertValues = new string[] {
													rte.EventType,
													rte.ServiceLevelAgreement.ToString(),
													rte.Submitted.ToString(TIME_FORMAT),
													rte.SessionId,
													rte.Time.ToString(TIME_FORMAT),
													rte.Successful.ToString() };

					// Format according to the rules
					formatter = DB_FORMAT_RTE;
					writer = swRte;
				}

				try
				{
					writer.WriteLine( formatter, insertValues );
				}
				catch( Exception e)
				{
					throw new TDException(String.Format("Failed to publish event using the Transaction Injector File Publisher. Reason:[{0}]", e.Message), e, false, TDExceptionIdentifier.Undefined);
				}
				

			}


		}

		#endregion

		#region Private methods
		private void CreateWriters()
		{
			DateTime now = DateTime.Now;
			timeout = now.Add( timespan );
			// format on string is "{0}"
			string dateName = now.ToString( filenameFormat );
			string fileOeName = directory + "\\" + PREFIX_OE + dateName + ".txt";
			this.swOE = new StreamWriter( fileOeName, true );
			swOE.AutoFlush = true;

			string fileRteName = directory + "\\" + PREFIX_RTE + dateName + ".txt";
			swRte = new StreamWriter( fileRteName, true);
			swRte.AutoFlush = true;
		}

		private void RotateWriters()
		{
			if( swOE != null )
			{
				swOE.Close();
				swOE = null;
			}
			if( swRte != null )
			{
				swRte.Close();
				swRte = null;
			}
			CreateWriters();
		}

		#endregion

		#region Constructors and deconstructors
		/// <summary>
		/// Constructor.
		/// </summary>
		/// <exception cref="TDException">
		/// Invalid property does not allow construction.
		/// </exception>
		public TransactionInjectorFilePublisher()
		{
			identifier = "TinjFP";

			TransactionInjectorFilePublisherPropertyValidator validator = new TransactionInjectorFilePublisherPropertyValidator(Properties.Current);
			ArrayList errors = new ArrayList();
			validator.ValidateProperty(PROP_FILEFORMAT, errors);
			validator.ValidateProperty(PROP_DIR, errors);
			validator.ValidateProperty(PROP_TIMESPAN, errors);

			if (errors.Count > 0)
			{
				StringBuilder message = new StringBuilder(100);

				foreach( string error in errors )
				{
					message.Append(error);
					message.Append(" ");	
				}

				throw new TDException(message.ToString(), false, TDExceptionIdentifier.Undefined);
			}

			// Settings
			this.filenameFormat = Properties.Current[PROP_FILEFORMAT];
			this.directory = Properties.Current[PROP_DIR];
			this.timespan = new TimeSpan(0,0, Convert.ToInt32(Properties.Current[PROP_TIMESPAN]), 0, 0);
			CreateWriters();
		}
		#endregion
	}
}
