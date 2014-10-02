// *********************************************** 
// NAME                 : LocationServiceFactory.cs
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 20/08/2012 
// DESCRIPTION          : Factory class for returning LocationService
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/LocationService/Cache/LocationServiceFactory.cs-arc  $ 
//
//   Rev 1.1   Dec 05 2012 13:34:30   mmodi
//Updated to remove static location cache and use an instance 
//Resolution for 5877: IIS Recycle issue
//
//   Rev 1.0   Aug 28 2012 10:45:28   mmodi
//Initial revision.
//Resolution for 5832: CCN Gaz

using System;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.Common;
using Logger = System.Diagnostics.Trace;
using TransportDirect.Common.Logging;

namespace TransportDirect.UserPortal.LocationService.Cache
{
    /// <summary>
    /// Factory class for returning LocationService
    /// </summary>
    public class LocationServiceFactory : IServiceFactory
    {
        #region Private members

        private LocationService current;

        /// <summary>
        /// Constant - Name of the DataChangeNotificationGroup used for detecting refreshing of data 
        /// </summary>
        private const string DataChangeNotificationGroup = "LocationService";

        /// <summary>
        /// Boolean used to store whether an event handler with the data change notification service has been registered
        /// </summary>
        private bool receivingChangeNotifications;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public LocationServiceFactory()
        {
            current = new LocationService();
            receivingChangeNotifications = RegisterForChangeNotification();
        }

        #endregion

        #region IServiceFactory methods

        /// <summary>
        ///  Method used by the ServiceDiscovery to get the
        ///  instance of the LocationService.
        /// </summary>
        /// <returns>The current instance of the LocationService.</returns>
        public Object Get()
        {
            return current;
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Method used to instanciate a new instance
        /// </summary>
        private void Update()
        {
            // Create new location service to allow data to load, 
            // then assign to current
            LocationService locationService = new LocationService();

            current = locationService;
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
                    Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Warning, "DataChangeNotificationService was not present when initialising LocationService"));
                    return false;
                }
                else
                    throw;
            }
            catch
            {
                // Non-CLS-compliant exception
                throw;
            }

            notificationService.Changed += new ChangedEventHandler(this.DataChangedNotificationReceived);
            return true;
        }

        #endregion

        #region Event handler

        /// <summary>
        /// Used by the Data Change Notification service to reload the data if it is changed in the DB
        /// </summary>
        private void DataChangedNotificationReceived(object sender, ChangedEventArgs e)
        {
            if (e.GroupId == DataChangeNotificationGroup)
                Update();
        }

        #endregion

    }
}
