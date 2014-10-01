// *********************************************** 
// NAME                 : JourneyNoteFilterFactory.cs
// AUTHOR               : David Lane
// DATE CREATED         : 08/08/2013 
// DESCRIPTION          : Factory class for JourneyNoteFilter data
// ************************************************ 

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TDP.Common.ServiceDiscovery;
using TDP.Common.DataServices;
using Logger = System.Diagnostics.Trace;
using TDP.Common.DatabaseInfrastructure;
using TDP.Common;

namespace TDP.UserPortal.JourneyControl
{
    /// <summary>
    /// Factory class for JourneyNoteFilter data
    /// </summary>
    public class JourneyNoteFilterFactory : IServiceFactory
    {
        #region Private members

        /// <summary> Current instance </summary>
        private JourneyNoteFilter current;

        /// <summary> Name of the DataChangeNotificationGroup used for detecting refreshing of data </summary>
        private const string dataChangeNotificationGroup = "JourneyNoteFilter";

        /// <summary>
        /// Boolean used to store whether an event handler with the data change notification service has been registered
        /// </summary>
        private bool receivingChangeNotifications;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public JourneyNoteFilterFactory()
        {
            current = new JourneyNoteFilter();
            receivingChangeNotifications = RegisterForChangeNotification();
        }

        #endregion

        #region methods

        /// <summary>
        /// Instantiates a new JourneyNoteFilter instance
        /// </summary>
        private void Update()
        {
            // Create a new instance
            JourneyNoteFilter instance = new JourneyNoteFilter();

            // Assign to the current
            current = instance;
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
                    Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Warning, "DataChangeNotificationService was not present when initializing JourneyNoteFilter"));
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
        /// Returns the current JourneyNoteFilter object
        /// </summary>
        /// <returns>The current instance of JourneyNoteFilter</returns>
        public object Get()
        {
            return current;
        }

        #endregion
    }
}