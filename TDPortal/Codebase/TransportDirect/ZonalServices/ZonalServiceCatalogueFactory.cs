// *********************************************** 
// NAME                 : ZonalServiceCatalogueFactory.cs
// AUTHOR               : Ken Josling
// DATE CREATED         : 12/12/2005 
// DESCRIPTION			: Factory class for Zonal Services.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ZonalServices/ZonalServiceCatalogueFactory.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 13:03:12   mturner
//Initial revision.
//
//   Rev 1.1   Feb 06 2006 15:14:08   tolomolaiye
//Code review updates
//Resolution for 2: DEL 8.1 Workstream - Zonal Services Phase 1
//
//   Rev 1.0   Dec 19 2005 12:12:40   kjosling
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
	/// A Factory class used to host Zonal Service Links
	/// </summary>
	public class ZonalServiceCatalogueFactory : IServiceFactory
	{
		#region Private attributes
		
		private ZonalServiceCatalogue currentLinks = new ZonalServiceCatalogue();	//A reference to the current ZonalServiceCatalog
		private const string dataChangeNotificationGroup = "ZonalServiceCatalogue";	//Used by change notification

		#endregion

		#region Constructor

		/// <summary>
		/// When the Factory is created it registers for change notification
		/// </summary>
		public ZonalServiceCatalogueFactory()
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
				ZonalServiceCatalogue newLinks = new ZonalServiceCatalogue();
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
					string traceMessage = "DataChangeNotificationService was not present when initializing ZonalServicesCatalogue";
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
		/// Exposes the ZonalServiceCatalogue
		/// </summary>
		/// <returns>The current instance of the ZonalServiceCatalogue</returns>
		public object Get()
		{
			return currentLinks;
		}

		#endregion
	}
}

