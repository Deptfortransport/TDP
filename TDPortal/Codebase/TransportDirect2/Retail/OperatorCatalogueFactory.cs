using System;
using TDP.Common.ServiceDiscovery;
using TDP.Common.DataServices;
using TDP.Common;
using TDP.Common.EventLogging;
using Logger = System.Diagnostics.Trace;
using TDP.Common.DatabaseInfrastructure;

namespace TDP.UserPortal.Retail
{
	/// <summary>
	/// Factory class for the OperatorsService.
	/// </summary>
	public class OperatorCatalogueFactory : IServiceFactory
	{
		#region private members

		/// <summary>Instance of OperatorCatalogue</summary>
		private OperatorCatalogue currentOperatorCatalogue;

		/// <summary> Constant - Name of the DataChangeNotificationGroup used for detecting refreshing of Operator Catalogue data </summary>
		private const string dataChangeNotificationGroup = "OperatorCatalogue";

		/// <summary>
		/// Boolean used to store whether an event handler with the data change notification service has been registered
		/// </summary>
		private bool receivingChangeNotifications;

		#endregion

		#region constructor

		/// <summary>
		/// Constructor
		/// </summary>
		public OperatorCatalogueFactory()
		{					
			Update();
			receivingChangeNotifications = RegisterForChangeNotification();
		}

		#endregion
		
		#region methods

		/// <summary>
		/// Instantiates a new OperatorCatalogue instance
		/// </summary>
		private void Update()
		{
			currentOperatorCatalogue = new OperatorCatalogue();
		}

		/// <summary>
		/// Registers an event handler with the data change notification service
		/// </summary>
		private bool RegisterForChangeNotification()
		{
			IDataChangeNotification notificationService;
			try
			{
				notificationService = (IDataChangeNotification)TDPServiceDiscovery.Current.Get<IDataChangeNotification>(ServiceDiscoveryKey.DataChangeNotification);
			}
			catch (TDPException e)
			{
				// If the SDInvalidKey TDException is thrown, return false as the notification service
				// hasn't been initialised.
				// Otherwise, rethrow the exception that was received.
				if (e.Identifier == TDPExceptionIdentifier.SDInvalidKey)
				{
					Logger.Write(new OperationalEvent(TDPEventCategory.Business, TDPTraceLevel.Warning, "DataChangeNotificationService was not present when initializing OperatorCatalogue"));
					return false;
				}
				else
					throw;
			}

			notificationService.Changed += new ChangedEventHandler(this.DataChangedNotificationReceived);
			return true;
		}

		/// <summary>
		/// Data notification method implementation
		/// When the data changes, reload
		/// </summary>
		/// <param name="sender">Notifying object</param>
		/// <param name="e">Event Parameter</param>
		private void DataChangedNotificationReceived(object sender, ChangedEventArgs e)
		{
			if (e.GroupId == dataChangeNotificationGroup)
				Update();
		}

		/// <summary>
		/// Returns the current OperatorCatalogue object
		/// </summary>
		/// <returns>The current instance of OperatorCatalogue.</returns>
		public object Get()
		{
			return currentOperatorCatalogue;
		}

		#endregion
	}
}