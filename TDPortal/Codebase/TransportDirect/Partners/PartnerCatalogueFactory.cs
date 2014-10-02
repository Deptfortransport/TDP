// *********************************************** 
// NAME                 : PartnerCatalogueFactory.cs 
// AUTHOR               : Manuel Dambrine
// DATE CREATED         : 27/09/2005	 
// DESCRIPTION  : Factory that allows the
// ServiceDiscovery to create an instance of the
// PartnerCatalogue class.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Partners/PartnerCatalogueFactory.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:35:44   mturner
//Initial revision.
//
//   Rev 1.3   Oct 14 2005 15:54:06   COwczarek
//Apply review comments from CR015
//Resolution for 2807: DEL8 White labelling Phase 3
//Resolution for 2809: Del8 White Labelling - Changes to Resource Manager and Partner catalogue
//
//   Rev 1.2   Oct 07 2005 11:23:08   mdambrine
//FXcop changes
//Resolution for 2807: DEL8 White labelling Phase 3
//Resolution for 2809: Del8 White Labelling - Changes to Resource Manager and Partner catalogue
//
//   Rev 1.1   Sep 29 2005 09:20:32   mdambrine
//Wrong connection string
//
//   Rev 1.0   Sep 27 2005 16:43:56   mdambrine
//Initial revision.


using System;
using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.DataServices;
using Logger = System.Diagnostics.Trace;

namespace TransportDirect.Partners
{
	/// <summary>
	/// Factory used by Service Discovery to create a PartnerCatalogue.
	/// </summary>	
	[System.Runtime.InteropServices.ComVisible(false)]
	public class PartnerCatalogueFactory : IServiceFactory
	{
		private IPartnerCatalogue current;
		private const string DataChangeNotificationGroup = "PartnerCatalogue";

		#region constructor
		/// <summary>
		/// Constructor.
		/// </summary>
		public PartnerCatalogueFactory()
		{
			Update();
			RegisterForChangeNotification();
		}
		#endregion

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
					Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Warning, "DataChangeNotificationService was not present when initialising PartnerCatalogue"));
					return false;
				}
				else
					throw;
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
			IPartnerCatalogue newPartnerCatalogue = new PartnerCatalogue();
			current = newPartnerCatalogue;
		}
	}
}

