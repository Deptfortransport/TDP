// *********************************************** 
// NAME                 : OperatorCatalogueFactory.cs
// AUTHOR               : Paul Cross
// DATE CREATED         : 15/07/2005
// DESCRIPTION			: Factory class for the OperatorsService.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyControl/OperatorCatalogueFactory.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:23:54   mturner
//Initial revision.
//
//   Rev 1.1   Jul 25 2005 21:00:50   pcross
//FxCop updates
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.0   Jul 18 2005 16:16:48   pcross
//Initial revision.
//

using System;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.Common;
using TransportDirect.Common.Logging;
using Logger = System.Diagnostics.Trace;

namespace TransportDirect.UserPortal.JourneyControl
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
				notificationService = (IDataChangeNotification)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataChangeNotification];
			}
			catch (TDException e)
			{
				// If the SDInvalidKey TDException is thrown, return false as the notification service
				// hasn't been initialised.
				// Otherwise, rethrow the exception that was received.
				if (e.Identifier == TDExceptionIdentifier.SDInvalidKey)
				{
					Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Warning, "DataChangeNotificationService was not present when initializing OperatorCatalogue"));
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
