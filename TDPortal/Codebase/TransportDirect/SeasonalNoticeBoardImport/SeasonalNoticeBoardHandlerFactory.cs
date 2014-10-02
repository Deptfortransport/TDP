// *********************************************** 
// NAME                 : SeasonalNoticeBoardHandlerFactory.cs
// AUTHOR               : Sanjeev Chand
// DATE CREATED         : 01/11/2004 
// DESCRIPTION  : Factory that allows the
// ServiceDiscovery to create an instance of the
// SeasonalNoticeBoardHandler class.
// ************************************************ 

using System;
using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.DataServices;
using Logger = System.Diagnostics.Trace;

namespace TransportDirect.UserPortal.SeasonalNoticeBoardImport
{
	/// <summary>
	/// Factory used by Service Discovery to create a SeasonalNoticeBoardHandler.
	/// </summary>	
	public class SeasonalNoticeBoardHandlerFactory : IServiceFactory 
	{
		private ISeasonalNoticeBoardHandler  current;
		private const string DataChangeNotificationGroup = "SeasonalNoticeBoardImport";
		

		/// <summary>
		/// Constructor
		/// </summary>	
		public SeasonalNoticeBoardHandlerFactory()
		{
			Update();
			RegisterForChangeNotification();
		}

		
		/// <summary>
		/// Method used by the ServiceDiscovery to get the instance of the SeasonalNoticeBoardHandler.
		/// </summary>
		/// <returns>The current instance of the ISeasonalNoticeBoardHandler.</returns>
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
					Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Warning, "DataChangeNotificationService was not present when initialising SeasonalNoticeBoardImport"));
					return false;
				}
				else
					throw e;
			}

			notificationService.Changed += new ChangedEventHandler(this.DataChangedNotificationReceived);
			return true;
		}


		/// <summary>
		/// Updates the Seasonal Notice Board data
		/// </summary>
		private void Update()
		{
			ISeasonalNoticeBoardHandler  newSeasonalNoticeBoardHandler = new  SeasonalNoticeBoardHandler();
			current = newSeasonalNoticeBoardHandler;
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
	}
}
