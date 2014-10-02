// *********************************************** 
// NAME                 : SocialBookMarkingCatalogueFactory.cs
// AUTHOR               : Ken Josling
// DATE CREATED         : 15/08/2005 
// DESCRIPTION			: Factory class for the SuggestionLinksService.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/SocialBookMarkingService/SocialBookMarkCatalogueFactory.cs-arc  $
//
//   Rev 1.0   Sep 23 2009 12:19:22   PScott
//Initial revision.
//Resolution for 5305: CCN530 Social Bookmarking
//
//   Rev 1.0   Sep 22 2009 12:50:10   pscott
//Initial revision.
//

using System;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.DataServices;

using Logger = System.Diagnostics.Trace;

namespace TransportDirect.UserPortal.SocialBookMarkingService
{
	public class SocialBookMarkCatalogueFactory : IServiceFactory
	{
		#region Private attributes

        /// <summary>
        /// Singleton instance
        /// </summary>
		private SocialBookMarkCatalogue current;

        /// <summary>
        /// Constant - Name of the DataChangeNotificationGroup used for detecting refreshing of data 
        /// </summary>
        private const string DataChangeNotificationGroup = "SocialBookMarkCatalogue";
        
        /// <summary>
        /// Boolean used to store whether an event handler with the data change notification service has been registered
        /// </summary>
        private bool receivingChangeNotifications;

		#endregion

		#region Constructor

		public SocialBookMarkCatalogueFactory()
		{
			current = new SocialBookMarkCatalogue();
            receivingChangeNotifications = RegisterForChangeNotification();
		}

		#endregion

        #region Implementation of IServiceFactory

		/// <summary>
        /// Returns the current SocialBookMarkCatalogue object
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
            current = new SocialBookMarkCatalogue();
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
                    Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Warning, "DataChangeNotificationService was not present when initialising SocialBookMarkCatalogue"));
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
