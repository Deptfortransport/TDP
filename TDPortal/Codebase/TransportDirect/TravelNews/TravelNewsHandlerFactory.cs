// *********************************************** 
// NAME                 : TravelNewsHandlerFactory.cs 
// AUTHOR               : Joe Morrissey
// DATE CREATED         : 29/09/2003 
// DESCRIPTION  : Factory that allows the
// ServiceDiscovery to create an instance of the
// TravelNewsHandler class.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/TravelNews/TravelNewsHandlerFactory.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:50:28   mturner
//Initial revision.
//
//   Rev 1.1   Sep 06 2004 21:08:58   JHaydock
//Major update to travel news
//
//   Rev 1.0   Sep 29 2003 17:49:16   JMorrissey
//Initial Revision

using System;
using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.DataServices;
using Logger = System.Diagnostics.Trace;

namespace TransportDirect.UserPortal.TravelNews
{
	/// <summary>
	/// Factory used by Service Discovery to create a TravelNewsHandler.
	/// </summary>	
	public class TravelNewsHandlerFactory : IServiceFactory
	{
		private ITravelNewsHandler current;
		private const string DataChangeNotificationGroup = "TravelNewsImport";

		/// <summary>
		/// Constructor.
		/// </summary>
		public TravelNewsHandlerFactory()
		{
			Update();
			RegisterForChangeNotification();
		}

		/// <summary>
		/// Method used by the ServiceDiscovery to get the instance of the TravelNewsHandler.
		/// </summary>
		/// <returns>The current instance of the TravelNewsHandler.</returns>
		public Object Get()
		{
			return current;
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
					Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Warning, "DataChangeNotificationService was not present when initialising TravelNewsImport"));
					return false;
				}
				else
					throw e;
			}

			notificationService.Changed += new ChangedEventHandler(this.DataChangedNotificationReceived);
			return true;
		}

		/// <summary>
		/// Event handler for data change notification
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DataChangedNotificationReceived(object sender, ChangedEventArgs e)
		{
			if (e.GroupId == DataChangeNotificationGroup)
				Update();
		}

		/// <summary>
		/// Updates the travel news data
		/// </summary>
		private void Update()
		{
			ITravelNewsHandler newTravelNewsHandler = new TravelNewsHandler();
			current = newTravelNewsHandler;
		}
	}
}

