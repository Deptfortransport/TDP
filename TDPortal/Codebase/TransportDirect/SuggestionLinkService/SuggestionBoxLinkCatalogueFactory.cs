// *********************************************** 
// NAME                 : SuggestionBoxLinkCatalogueFactory.cs
// AUTHOR               : Ken Josling
// DATE CREATED         : 15/08/2005 
// DESCRIPTION			: Factory class for the SuggestionLinksService.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/SuggestionLinkService/SuggestionBoxLinkCatalogueFactory.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:50:10   mturner
//Initial revision.
//
//   Rev 1.1   Aug 31 2005 18:13:40   kjosling
//Updated following code review
//
//   Rev 1.0   Aug 24 2005 16:44:54   kjosling
//Initial revision.

using System;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.Common;
using TransportDirect.Common.Logging;
using System.Diagnostics;

namespace TransportDirect.UserPortal.SuggestionLinkService
{
	public class SuggestionBoxLinkCatalogueFactory : IServiceFactory
	{
		#region Private attributes
		
		private SuggestionBoxLinkCatalogue currentLinks = new SuggestionBoxLinkCatalogue();
		private const string dataChangeNotificationGroup = "SuggestionBoxLinkCatalogue";

		#endregion

		#region Constructor

		public SuggestionBoxLinkCatalogueFactory()
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
				SuggestionBoxLinkCatalogue newLinks = new SuggestionBoxLinkCatalogue();
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
				// hasn't been initialised.
				// Otherwise, rethrow the exception that was received.
				if (e.Identifier == TDExceptionIdentifier.SDInvalidKey)
				{
					Trace.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Warning, "DataChangeNotificationService was not present when initializing SuggestionBoxLinkCatalogue"));
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

		public object Get()
		{
			return currentLinks;
		}

		#endregion
	}
}
