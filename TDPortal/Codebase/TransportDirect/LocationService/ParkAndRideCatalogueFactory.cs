// *********************************************** 
// NAME                 : ParkAndRideCatalogueFactory.cs
// AUTHOR               : Neil Moorhouse
// DATE CREATED         : 22/07/2005
// DESCRIPTION  	    : Service Factory for the Park And Ride Catalogue.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/LocationService/ParkAndRideCatalogueFactory.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:25:16   mturner
//Initial revision.
//
//   Rev 1.1   Aug 12 2005 11:13:22   NMoorhouse
//DN058 Park And Ride, end of CUT
//Resolution for 2596: DEL 8 Stream: Park and Ride
//
//   Rev 1.0   Aug 03 2005 10:22:20   NMoorhouse
//Initial revision.
//
using System;
using TransportDirect.Common;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.DatabaseInfrastructure;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.Common.Logging;
using Logger = System.Diagnostics.Trace;

namespace TransportDirect.UserPortal.LocationService
{

	/// <summary>
	/// Summary description for ParkAndRideCatalogueFactory.
	/// </summary>
	public class ParkAndRideCatalogueFactory : IServiceFactory
	{
		#region private members

		/// <summary>
		/// Singleton instance of ParkAndRideCatalogue
		/// </summary>
		ParkAndRideCatalogue current;

		/// <summary>
		/// Constant - Name of the DataChangeNotificationGroup used for detecting refreshing of Park and Ride data 
		/// </summary>
		private const string dataChangeNotificationGroup = "ParkAndRide";

		/// <summary>
		/// Boolean used to store whether an event handler with the data change notification service has been registered
		/// </summary>
		private bool receivingChangeNotifications;

		#endregion

		#region constructor
		/// <summary>
		/// Constructor.
		/// </summary>
		public ParkAndRideCatalogueFactory()
		{
			current = new ParkAndRideCatalogue(SqlHelperDatabase.TransientPortalDB);
			receivingChangeNotifications = RegisterForChangeNotification();
		}

		#endregion

		#region public methods
		/// <summary>
		/// Method used to get an instance of the Park And Ride Catalogue. 
		/// </summary>
		/// <returns>An instance of ParkAndRideCatalogue</returns>
		public object Get()
		{
			return current;
		}
		#endregion

		#region private methods
		/// <summary>
		/// Method used to instanciate a new instance of ParkAndRideCatalogue
		/// </summary>
		private void Update()
		{
			current = new ParkAndRideCatalogue(SqlHelperDatabase.TransientPortalDB);
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
					string errorMessage = "DataChangeNotificationService was not present when initializing ExternalLinks";
					Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Warning, errorMessage));
					return false;
				}
				else
					throw;
			}

			notificationService.Changed += new ChangedEventHandler(this.DataChangedNotificationReceived);
			return true;
		}
		#endregion

		#region Event handler
		/// <summary>
		/// Data notification method implementation
		/// </summary>
		/// <param name="sender">Notifying object</param>
		/// <param name="e">Event Parameter</param>
		private void DataChangedNotificationReceived(object sender, ChangedEventArgs e)
		{
			if (e.GroupId == dataChangeNotificationGroup)
				Update();
				
		}
		#endregion
	}
}
