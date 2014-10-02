// *********************************************** 
// NAME                 : ZonalAccessibilityCatalogueFactory.cs
// AUTHOR               : Sanjeev Johal
// DATE CREATED         : 17/06/2008 
// DESCRIPTION			: Factory class for Zonal Accessibility.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ZonalServices/ZonalAccessibilityCatalogueFactory.cs-arc  $
//
//   Rev 1.1   Jul 03 2008 13:27:50   apatel
//change the namespage zonalaccessibility to zonalservices
//Resolution for 5033: CCN0458 - Accessability Updates improve linking
//
//   Rev 1.0   Jun 27 2008 09:45:56   apatel
//Initial revision.
//Resolution for 5033: CCN0458 - Accessability Updates improve linking
//
//   Rev 1.0   Jun 17 2008 13:00:00   sjohal
//Initial revision.


using System;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.Common;
using TransportDirect.Common.Logging;
using System.Diagnostics;

namespace TransportDirect.UserPortal.ZonalServices
{
	/// <summary>
    /// A Factory class used to host Zonal Accessibility Links
	/// </summary>
	public class ZonalAccessibilityCatalogueFactory : IServiceFactory
	{
		#region Private attributes
		
		private ZonalAccessibilityCatalogue currentLinks = new ZonalAccessibilityCatalogue();	//A reference to the current ZonalAccessibilityCatalog
		private const string dataChangeNotificationGroup = "ZonalAccessibilityCatalogue";	//Used by change notification

		#endregion

		#region Constructor

		/// <summary>
		/// When the Factory is created it registers for change notification
		/// </summary>
        public ZonalAccessibilityCatalogueFactory()
		{
			RegisterForChangeNotification();
		}

		#endregion

		#region DataChangeNotification Members

		/// <summary>
		/// Data notification method implementation
		/// </summary>
		/// <param name="sender">Notifying object</param>
		/// <param name="e">ChangedEventArgs Parameter</param>
		private void DataChangedNotificationReceived(object sender, ChangedEventArgs e)
		{
			if (e.GroupId == dataChangeNotificationGroup)
			{
                ZonalAccessibilityCatalogue newLinks = new ZonalAccessibilityCatalogue();
				currentLinks = newLinks;
			}
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
				// hasn't been initialised, otherwise, rethrow the exception that was received.
				if (e.Identifier == TDExceptionIdentifier.SDInvalidKey)
				{
                    string traceMessage = "DataChangeNotificationService was not present when initializing ZonalAccessibilityCatalogue";
					Trace.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Warning, traceMessage));
					return false;
				}
				else
					throw;
			}

			notificationService.Changed += new ChangedEventHandler(this.DataChangedNotificationReceived);
			return true;
		}

		#endregion

		#region IServiceFactory methods

		/// <summary>
        /// Exposes the ZonalAccessibilityCatalogue
		/// </summary>
        /// <returns>The current instance of the ZonalAccessibilityCatalogue</returns>
		public object Get()
		{
			return currentLinks;
		}

		#endregion
	}
}

