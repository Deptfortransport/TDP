// *********************************************** 
// NAME			: JourneyEmissionsFactorFactory.cs
// AUTHOR		: Mitesh Modi
// DATE CREATED	: 08/02/2007
// DESCRIPTION	: Implementation of IServiceFactory for the JourneyEmissionsFactor
// so that it can be used with TDServiceDiscovery
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyEmissions/JourneyEmissionsFactorFactory.cs-arc  $
//
//   Rev 1.0   Aug 04 2009 14:36:56   mmodi
//Initial revision.
//Resolution for 5307: CCN517a Web Service Find a Car Route
//
//   Rev 1.0   Nov 08 2007 12:23:46   mturner
//Initial revision.
//
//   Rev 1.0   Feb 20 2007 17:09:06   mmodi
//Initial revision.
//Resolution for 4350: CO2 Public Transport
//

using System;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.DataServices;

using Logger = System.Diagnostics.Trace;

namespace TransportDirect.UserPortal.JourneyEmissions
{
	/// <summary>
	/// Implementation of IServiceFactory for JourneyEmissionsFactorFactory.
	/// </summary>
	public class JourneyEmissionsFactorFactory : IServiceFactory
	{
        /// <summary>
        /// Constant - Name of the DataChangeNotificationGroup used for detecting refreshing of data 
        /// </summary>
        private const string DataChangeNotificationGroup = "JourneyEmissionsFactor";
        
        /// <summary>
        /// Boolean used to store whether an event handler with the data change notification service has been registered
        /// </summary>
        private bool receivingChangeNotifications;

        /// <summary>
        /// Singleton instance
        /// </summary>
		private JourneyEmissionsFactor current;

		#region Implementation of IServiceFactory

		/// <summary>
		/// Standard constructor. Initialises the JourneyEmissionsFactor.
		/// </summary>
		public JourneyEmissionsFactorFactory()
		{
			current = new JourneyEmissionsFactor();
            receivingChangeNotifications = RegisterForChangeNotification();
		}
        
		/// <summary>
		/// Returns the current JourneyEmissionsFactor object
		/// </summary>
		/// <returns></returns>
		public object Get()
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
            current = new JourneyEmissionsFactor();
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
                    Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Warning, "DataChangeNotificationService was not present when initialising JourneyEmissionsFactor"));
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
