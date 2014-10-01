// *********************************************** 
// NAME                 : JourneyNoteFilterFactory.cs
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 19/03/2013 
// DESCRIPTION          : Factory class for JourneyNoteFilter data
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyControl/JourneyNoteFilterFactory.cs-arc  $ 
//
//   Rev 1.0   Mar 21 2013 10:14:54   mmodi
//Initial revision.
//Resolution for 5905: CCN:677a - Accessible Journeys Planner Phase 2
//

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.DataServices;
using Logger = System.Diagnostics.Trace;

namespace TransportDirect.UserPortal.JourneyControl
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
				notificationService = (IDataChangeNotification)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataChangeNotification];
			}
			catch (TDException e)
			{
				// If the SDInvalidKey TDException is thrown, return false as the notification service
				// hasn't been initialised.
				// Otherwise, rethrow the exception that was received.
				if (e.Identifier == TDExceptionIdentifier.SDInvalidKey)
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
