// *********************************************** 
// NAME                 : CarParkCatalogueFactory.cs
// AUTHOR               : Esther Severn
// DATE CREATED         : 04/08/2006
// DESCRIPTION  	    : Service Factory for the Car Park Catalogue.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/LocationService/CarParkCatalogueFactory.cs-arc  $
//
//   Rev 1.2   Mar 10 2008 15:18:44   mturner
//Initial Del10 Codebase from Dev Factory
//
//  Rev DevFactory Feb 06 2008 22:18:00 apatel
//  Change carParkCatalogue to use new CarParksDB instead of TransientPortalDB
//
//   Rev 1.0   Nov 08 2007 12:25:00   mturner
//Initial revision.
//
//   Rev 1.0   Aug 15 2006 14:04:38   esevern
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
	/// Summary description for CarParkCatalogueFactory.
	/// </summary>
	public class CarParkCatalogueFactory : IServiceFactory
	{
		#region private members

		/// <summary>
		/// Singleton instance of CarParkCatalogue
		/// </summary>
		CarParkCatalogue current;

		/// <summary>
		/// Constant - Name of the DataChangeNotificationGroup used for detecting refreshing of Car Park data 
		/// </summary>
		private const string dataChangeNotificationGroup = "CarPark";

		/// <summary>
		/// Boolean used to store whether an event handler with the data change notification service has been registered
		/// </summary>
		private bool receivingChangeNotifications;

		#endregion

		/// <summary>
		/// Constructor.
		/// </summary>
		public CarParkCatalogueFactory()
		{
            // CCN 0426 Changed to use CarParksDB
            current = new CarParkCatalogue(SqlHelperDatabase.CarParksDB); 
			receivingChangeNotifications = RegisterForChangeNotification();
		}

		#region public methods
		/// <summary>
		/// Method used to get an instance of the Car Park Catalogue. 
		/// </summary>
		/// <returns>An instance of CarParkCatalogue</returns>
		public object Get()
		{
			return current;
		}
		#endregion

		#region private methods
		/// <summary>
		/// Method used to instanciate a new instance of CarParkCatalogue
		/// </summary>
		private void Update()
		{
            current = new CarParkCatalogue(SqlHelperDatabase.CarParksDB); //(SqlHelperDatabase.TransientPortalDB);
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
