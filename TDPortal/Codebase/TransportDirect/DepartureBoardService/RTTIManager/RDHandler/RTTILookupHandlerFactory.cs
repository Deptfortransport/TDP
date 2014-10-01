// *********************************************** 
// NAME                 : RTTILookupHandlerFactory.cs
// AUTHOR               : Sanjeev Chand
// DATE CREATED         : 14/01/2005 
// DESCRIPTION  		: Factory implementation of  RTTILookupHandler
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/DepartureBoardService/RTTIManager/RTTILookupHandlerFactory.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:21:40   mturner
//Initial revision.
//
//   Rev 1.2   Mar 02 2005 11:17:40   schand
//FxCop fix
//
//   Rev 1.1   Mar 02 2005 10:48:46   schand
//Fixed Code review errors
//
//   Rev 1.0   Feb 28 2005 16:23:06   passuied
//Initial revision.
//
//   Rev 1.3   Jan 21 2005 14:22:38   schand
//Code clean-up and comments has been added

using System;
using System.Collections;
using TransportDirect.Common.DatabaseInfrastructure;
using TransportDirect.Common;
using TransportDirect.Common.Logging;
using Logger = System.Diagnostics.Trace;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.UserPortal.DepartureBoardService;   
using TransportDirect.UserPortal.DepartureBoardService.DepartureBoardFacade;
using TransportDirect.UserPortal.DepartureBoardService.RTTIManager ;


namespace TransportDirect.UserPortal.DepartureBoardService
{
	/// <summary>
	/// Factory implementation of  RTTILookupHandler
	/// </summary>
	public class RTTILookupHandlerFactory: IServiceFactory
	{	
		private IRTTILookupHandler current = null;
		private const string DataChangeNotificationGroup =  ""; //will be implemented in future (if agreed by analyst). //"RTTILookupHandler" ; ;
		
		/// <summary>
		/// Class constructor
		/// </summary>
		public RTTILookupHandlerFactory()
		{
			Update();		
			// Uncomment below line when data notification is completed. 
			//RegisterForChangeNotification();
		}
		
		
		/// <summary>
		/// Method used by the ServiceDiscovery to get the instance of the RTTILookupHandler.
		/// </summary>
		/// <returns>The current instance of the IRTTILookupHandler.</returns>
		public Object Get()
		{
			return current;
		}

		

		#region "Private Members"
		/// <summary>
		/// Initialise the whole object
		/// </summary>
		private void Update()
		{
			IRTTILookupHandler  newRTTILookupHandler = new  RTTILookupHandler() ;
			current = newRTTILookupHandler;
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
					Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Warning, "DataChangeNotificationService was not present when initialising RTTILookupHandler"));
					return false;
				}
				else
					throw e;
			}

			notificationService.Changed += new ChangedEventHandler(this.DataChangedNotificationReceived);
			return true;
		}

		
		/// <summary>
		/// Data notification method implementation
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void DataChangedNotificationReceived(object sender, ChangedEventArgs e)
		{
			if (e.GroupId.Equals(DataChangeNotificationGroup))
				Update();
		}

		#endregion
	}
}
