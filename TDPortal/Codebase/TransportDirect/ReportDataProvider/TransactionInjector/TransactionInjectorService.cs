// *********************************************** 
// NAME			: TransactionInjectorService.cs
// AUTHOR		: Jatinder S. Toor
// DATE CREATED	: 03/09/2003 
// DESCRIPTION	: Implementation of the TransactionInjectorService class.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/TransactionInjector/TransactionInjectorService.cs-arc  $
//
//   Rev 1.1   Mar 16 2009 12:24:06   build
//Automatically merged from branch for stream5215
//
//   Rev 1.0.1.1   Jan 13 2009 12:27:08   mturner
//Further updates for tech refresh
//Resolution for 5215: Workstream for RS620
//
//   Rev 1.0.1.0   Jan 12 2009 16:28:32   mturner
//Updates for tech refresh
//Resolution for 5215: Workstream for RS620
//
//   Rev 1.0   Nov 08 2007 12:40:14   mturner
//Initial revision.
//
//   Rev 1.21   Apr 25 2005 17:16:48   schand
//add code to genrate template JourneyRequestUsingGazetteerTransaction
//
//   Rev 1.20   Mar 01 2005 16:41:56   rscott
//Updated as outwarddatetime and returndatetime changed to TDDate[ ]
//
//   Rev 1.19   Jan 20 2005 11:09:34   RScott
//DEL7 Updated to include PublicViaLocations
//
//   Rev 1.18   Jan 20 2005 11:08:24   RScott
//DEL 7 - Updated adding PublicViaLocations
//
//   Rev 1.17   Jun 21 2004 15:25:18   passuied
//Changes for del6-del5.4.1
//
//   Rev 1.16   Mar 03 2004 12:44:44   geaton
//Removed template generation code for pricing transactions following refactoring of PublicJourneyDetail in DEL5.2. Injector will no longer generate templates for pricing transactions.
//
//   Rev 1.15   Nov 13 2003 12:32:40   geaton
//Generation of templates for pricing and station info.
//
//   Rev 1.14   Nov 11 2003 19:16:06   geaton
//Added pricing support.
//
//   Rev 1.13   Nov 11 2003 18:57:22   geaton
//Added template generation for pricing transactions.

using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.ServiceProcess;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Threading;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.PricingRetail.Domain;

namespace TransportDirect.ReportDataProvider.TransactionInjector
{
	/// <summary>
	/// Windows service start-up class.
	/// </summary>
	public class TransactionInjectorService : System.ServiceProcess.ServiceBase
	{
		private TransactionInjector transactionInjector;
		private const string TestParameter = "/test";
		private const string TemplateParameter = "/generatetemplate";
		private bool testMode;
		private bool generateMode;
		private bool tdServicesInitialised;

		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// This is ONLY called the first time the service is started.
		/// Processing should not be performed in this method.
		/// Initialisation that relies on properties that may change
		/// between starting and stopping the service should NOT be 
		/// performed in this method.
		/// </summary>
		public TransactionInjectorService(string servName)
		{
			// Set up service name - this should match the AID used in the properties, and in ProjectInstaller.cs.
			this.ServiceName = servName;

			// This call is required by the Windows.Forms Component Designer.
			InitializeComponent();

			transactionInjector = null;
		}

		/// <summary>
		/// The main entry point for the process
		/// </summary>
		static void Main(string[] arg)
		{
			
			// get module name and split it in words
			string[] nameParts = System.Reflection.Assembly.GetExecutingAssembly().GetLoadedModules(false)[0].Name.Split('.');
			// servicename = last word - significant name before extension.
			string serviceName = nameParts[nameParts.Length-2];
			
	
			System.ServiceProcess.ServiceBase.Run( new TransactionInjectorService(serviceName) );
	
		}

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		/// <summary>
		/// C# Destructor.
		/// </summary>
		~TransactionInjectorService()
		{
			Dispose( false );
		}

		/// <summary>
		/// Called when service is started.
		/// </summary>
		/// <param name="args">Arguments passed to service.</param>
		protected override void OnStart(string[] args)
		{
			testMode = false;
			generateMode = false;
			tdServicesInitialised = false;
			transactionInjector = null;

			EventLog.WriteEntry( this.ServiceName +" is starting ",  EventLogEntryType.Information, 1);
			
			// Initialise TD Services.
			try
			{
				TDServiceDiscovery.Init(new TransactionInjectorInitialisation(this.ServiceName));
				tdServicesInitialised = true;
				AutoLog = false; // Disable default logging (will be using TD Logging Service).
			}
			catch (TDException tdException)
			{
				// Log error to default trace listener in case TD Listener failed to create.
				EventLog.WriteEntry(String.Format(Messages.Init_Failed, tdException.Message), EventLogEntryType.Error, 1);
				AutoLog = false; // Prevents .NET runtime from making entry to say service has started successfully.
			}

			if (tdServicesInitialised)
			{
				if (TDTraceSwitch.TraceVerbose)
					Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, Messages.Init_Completed));
			}
			
			if ((tdServicesInitialised) && (args.Length == 1))
			{
				
				if (args[0].Equals(TestParameter))
				{
					testMode = true;

					if (TDTraceSwitch.TraceWarning)
						Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Warning, Messages.Service_StartedTestMode));														
				}
				else if (args[0].Equals(TemplateParameter))
				{
					generateMode = true;

					if (TDTraceSwitch.TraceWarning)
						Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Warning, Messages.Service_StartedGenerateMode));		
				}
				else
				{
					if (TDTraceSwitch.TraceWarning)
						Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Warning, Messages.Service_BadParam));
				}
			}
			
				
			if (tdServicesInitialised)
			{
				if (testMode)
				{
					// do nothing...
				}
				else if (generateMode)
				{
					GenerateTransactionTemplates();
				}
				else
				{
					// Start the injection.
					try
					{

						TransactionInjector.InitStaticParameters(Properties.Current, this.ServiceName);

						ThreadStart ts = new ThreadStart(TransactionInjector.Run);

						Thread tiThread = new Thread(ts);
						tiThread.Start();
						EventLog.WriteEntry( this.ServiceName +" has started",  EventLogEntryType.Information, 1);

					}
					catch (TDException tdException)
					{
						EventLog.WriteEntry(String.Format(Messages.Init_Failed, tdException.Message), EventLogEntryType.Error, 1);
						Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, String.Format(Messages.Service_FailedCreatingTransactionInstance, tdException.Message)));
					}
					catch(Exception e)
					{
						EventLog.WriteEntry(String.Format(Messages.Init_Failed, e.Message), EventLogEntryType.Error, 1);						
					}
				}
			}

		}
 
		/// <summary>
		/// Stops the service.
		/// </summary>
		protected override void OnStop()
		{
			if (tdServicesInitialised)
			{
				if (TDTraceSwitch.TraceVerbose)
					Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, Messages.Service_Stopping));
			}


			if (transactionInjector != null)
			{
				transactionInjector.Dispose(true);
				transactionInjector = null;
			}
		}

		/// <summary>
		/// Generate the template files for a subsetTDTransaction type classes 
		/// supported by the TransactionInjector.
		/// Due to XML serialization restrictions, templates are generated only for
		/// the following transaction types:
		/// JourneyRequestTransaction
		/// SoftContentTransaction
		/// StationInfoTransaction
		/// </summary>
		/// <remarks>
		/// Note: if any of the transaction classes change,
		/// then changes may need to be made to this method
		/// to generate the correct XML.
		/// </remarks>
		private void GenerateTransactionTemplates()
		{
			string templateDir = Properties.Current[Keys.TransactionInjectorTemplateFileDirectory];			

			// For JourneyRequestTransaction
			JourneyRequestTransaction journeyReq = new JourneyRequestTransaction();

			journeyReq.Category = "Insert_Category_Here";
			journeyReq.ServiceLevelAgreement = true;
			journeyReq.MinNumberOutwardRoadJourney = 0;
			journeyReq.MinNumberReturnRoadJourney = 0;
			journeyReq.MinNumberOutwardPublicJourney = 0;
			journeyReq.MinNumberReturnPublicJourney = 0;

            journeyReq.Frequency = 0;
            journeyReq.Offset = 0;

			journeyReq.OutwardDayOfWeek = 0;
			journeyReq.OutwardTime =  new TDTimeSpan();
			journeyReq.ReturnDayOfWeek = 0;
			journeyReq.ReturnTime = new TDTimeSpan();             

			journeyReq.RequestData.Modes = new ModeType[1];
			journeyReq.RequestData.IsReturnRequired	= false;
			journeyReq.RequestData.OutwardArriveBefore = false;
			journeyReq.RequestData.ReturnArriveBefore  = false;

			journeyReq.RequestData.OutwardDateTime = new TDDateTime[1]; // NB TDDateTime fails to serialise if parameters passed ex journeyReqGaz.RequestData.OutwardDateTime[0]= new TDDateTime() will fail;
			journeyReq.RequestData.ReturnDateTime = new TDDateTime[1]; // NB TDDateTime fails to serialise if parameters passed. ex journeyReqGaz.RequestData.ReturnDateTime[0]		= new TDDateTime();

			journeyReq.RequestData.InterchangeSpeed = 0;
			journeyReq.RequestData.WalkingSpeed = 0;
			journeyReq.RequestData.MaxWalkingTime = 0;
			journeyReq.RequestData.DrivingSpeed = 0;
			journeyReq.RequestData.AvoidMotorways = false;

			journeyReq.RequestData.OriginLocation = new TDLocation();
			journeyReq.RequestData.DestinationLocation = new TDLocation();			
			journeyReq.RequestData.AvoidRoads = new string[1];			
			journeyReq.RequestData.AlternateLocationsFrom = false;
			journeyReq.RequestData.PrivateAlgorithm = PrivateAlgorithmType.Fastest;
			journeyReq.RequestData.PublicAlgorithm  = PublicAlgorithmType.Default;		

			GenerateTransactionTemplate(journeyReq, templateDir + "\\JourneyRequestTransaction.xml");
		}

		/// <summary>
		/// Generates an individual template file based on the trans type argument.
		/// </summary>
		/// <param name="trans">Transaction class instance.</param>
		/// <param name="templatePath">File path to template file.</param>
		private void GenerateTransactionTemplate( TDTransaction trans, string templatePath )		
		{
			TextWriter writer = null; 
						
			try
			{
				XmlSerializer serializer	= new XmlSerializer(trans.GetType());
				writer = new StreamWriter(templatePath);
				serializer.Serialize(writer, trans);
			}
			catch (Exception exception) // None documented so catch all.
			{
				Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, String.Format(Messages.Template_GenerationFailed, exception.Message)));
			}
			finally
			{
				if (writer != null)
					writer.Close();
			}
		}

	}
}
