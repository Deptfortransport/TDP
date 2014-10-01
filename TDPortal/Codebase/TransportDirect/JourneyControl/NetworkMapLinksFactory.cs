// *********************************************** 
// NAME                 : NetworkMapLinksFactory.cs
// AUTHOR               : Paul Cross
// DATE CREATED         : 11/07/2005 
// DESCRIPTION			: Factory class for the NetworkMapLinksService.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyControl/NetworkMapLinksFactory.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:23:52   mturner
//Initial revision.
//
//   Rev 1.2   Jul 25 2005 20:59:34   pcross
//FxCop updates
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.1   Jul 18 2005 16:19:40   pcross
//Non-functional updates.
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.0   Jul 12 2005 10:45:48   pcross
//Initial revision.
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
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
	/// Factory class for the NetworkMapLinksService.
	/// </summary>
	public class NetworkMapLinksFactory : IServiceFactory
	{
		#region private members
		
		/// <summary>Instance of NetworkMapLinks</summary>
		private NetworkMapLinks currentNetworkMapLinks;

		/// <summary> Constant - Name of the DataChangeNotificationGroup used for detecting refreshing of Network Map link data </summary>
		private const string dataChangeNotificationGroup = "NetworkMapLinks";

		/// <summary>
		/// Boolean used to store whether an event handler with the data change notification service has been registered
		/// </summary>
		private bool receivingChangeNotifications;

		#endregion

		#region constructor

		/// <summary>
		/// Constructor
		/// </summary>
		public NetworkMapLinksFactory()
		{					
			Update();
			receivingChangeNotifications = RegisterForChangeNotification();
		}

		#endregion
		
		#region methods

		/// <summary>
		/// Instantiates a new NetworkMapLinks instance
		/// </summary>
		private void Update()
		{
			currentNetworkMapLinks = new NetworkMapLinks();
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
					Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Warning, "DataChangeNotificationService was not present when initializing NetworkMapLinks"));
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
		/// Returns the current NetworkMapLinks object
		/// </summary>
		/// <returns>The current instance of NetworkMapLinks.</returns>
		public object Get()
		{
			return currentNetworkMapLinks;
		}

		#endregion
	}
}
