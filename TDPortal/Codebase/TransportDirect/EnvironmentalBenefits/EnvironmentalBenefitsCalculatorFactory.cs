// *********************************************** 
// NAME			: EnvironmentalBenefitsCalculatorFactory.cs
// AUTHOR		: Mitesh Modi
// DATE CREATED	: 21/09/2009
// DESCRIPTION	: Factory to class to provide an instance of the EnvironmentalBenefitsCalculator class
// ************************************************ 
//
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnvironmentalBenefits/EnvironmentalBenefitsCalculatorFactory.cs-arc  $
//
//   Rev 1.0   Oct 06 2009 13:58:48   mmodi
//Initial revision.
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//

using System;
using System.Collections.Generic;
using System.Text;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.DataServices;

using Logger = System.Diagnostics.Trace;

namespace TransportDirect.UserPortal.EnvironmentalBenefits
{
    public class EnvironmentalBenefitsCalculatorFactory : IServiceFactory
    {
        #region Private members

        /// <summary>
        /// Constant - Name of the DataChangeNotificationGroup used for detecting refreshing of data 
        /// </summary>
        private const string DataChangeNotificationGroup = "EnvironmentalBenefits";

        /// <summary>
        /// Boolean used to store whether an event handler with the data change notification service has been registered
        /// </summary>
        private bool receivingChangeNotifications;

        /// <summary>
        /// Singleton instance
        /// </summary>
        private EnvironmentalBenefitsCalculator current;

        #endregion

        #region Implementation of IServiceFactory

        /// <summary>
        /// Standard constructor. Initialises the EnvironmentalBenefitsCalculator
		/// </summary>
		public EnvironmentalBenefitsCalculatorFactory()
		{
            current = new EnvironmentalBenefitsCalculator();
            receivingChangeNotifications = RegisterForChangeNotification();
		}
        
		/// <summary>
        /// Returns the current EnvironmentalBenefitsCalculator object
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
            // Ok to use current instance, but need to reload its data
            current.ReloadData();
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
                    Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Warning, "DataChangeNotificationService was not present when initialising EnvironmentalBenefits"));
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
