// *********************************************** 
// NAME                 : ExternalLinks.cs
// AUTHOR               : Rachel Geraghty
// DATE CREATED         : 07/06/2005 
// DESCRIPTION			: Class responsible for holding the url links
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ExternalLinkService/ExternalLinks.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:23:22   mturner
//Initial revision.
//
//   Rev 1.5   Feb 16 2006 15:54:24   build
//Automatically merged from branch for stream0002
//
//   Rev 1.4.1.0   Dec 19 2005 14:50:58   kjosling
//Updated for Zonal Services 
//Resolution for 2: DEL 8.1 Workstream - Zonal Services Phase 1
//
//   Rev 1.4   Nov 01 2005 15:12:56   build
//Automatically merged from branch for stream2638
//
//   Rev 1.3.1.0   Sep 23 2005 08:43:58   pcross
//Updated the Current property to return type IExternalLinks so that any class that inherits IExternalLinks can be returned (and therefore a test class can be returned for the test session manager.
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.3   Jun 27 2005 14:29:42   rgeraghty
//Updated documentation comments
//Resolution for 2559: DEL 8 Stream: External Links Repository
//
//   Rev 1.2   Jun 27 2005 12:33:30   rgeraghty
//Updated with code review comments
//Resolution for 2559: DEL 8 Stream: External Links Repository
//
//   Rev 1.1   Jun 14 2005 18:58:40   rgeraghty
//Updated to include IR association
//Resolution for 2559: DEL 8 Stream: External Links Repository
//
//   Rev 1.0   Jun 14 2005 18:56:14   rgeraghty
//Initial revision.

using System;
using System.Collections;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common; 
using TransportDirect.Common.Logging;
using TransportDirect.Common.DatabaseInfrastructure;
using TransportDirect.UserPortal.DataServices;
using System.Data.SqlClient;
using Logger = System.Diagnostics.Trace;


namespace TransportDirect.UserPortal.ExternalLinkService
{
	/// <summary>
	/// ExternalLinks is responsible for maintaining the state of the links.
	/// </summary>
	public class ExternalLinks : IExternalLinks
	{
		#region private members

		/// <summary> Constant - Name of the DataChangeNotificationGroup used for detecting refreshing of URL data </summary>
		private const string dataChangeNotificationGroup = "ExternalLinks";

		/// <summary>Hashtable to store URL links </summary>
		private Hashtable linksCache= new Hashtable();	

		/// <summary>
		/// Boolean used to store whether an event handler with the data change notification service has been registered
		/// </summary>
		private bool receivingChangeNotifications;

		/// <summary>
		/// Variables to hold column headers for data
		/// </summary>
		private int externalLinkIDColumnOrdinal;
		private int isValidColumnOrdinal;
		private int startDateColumnOrdinal;
		private int endDateColumnOrdinal;
		private int linkTextColumnOrdinal;
		private int linkUrlColumnOrdinal;

		#endregion

		#region constructor

		/// <summary>
		/// ExternalLinks Constructor
		/// </summary>
		public ExternalLinks()
		{
			Load();
			receivingChangeNotifications = RegisterForChangeNotification();
		}

		#endregion

		#region public properties/methods

		/// <summary>
		/// Read only property. Implementation of IExternalLinks Current method
		/// </summary>
		public static IExternalLinks Current
		{
			get
			{
				return (IExternalLinks)TDServiceDiscovery.Current[ ServiceDiscoveryKey.ExternalLinkService ];				
			}
		}

		/// <summary>
		/// Read only Indexer which returns the object requested against a key
		/// </summary>		
		public ExternalLinkDetail this[ string key]
		{
			get
			{
				return (ExternalLinkDetail)linksCache[key];
			}
		}

		/// <summary>
		/// Method which attempts to find the specified url
		/// </summary>	
		/// <param name="url">string url to locate</param>
		/// <returns>ExternalLinkDetail object</returns>	
		public ExternalLinkDetail FindUrl( string url)
		{
			foreach (string externalLinkId in linksCache.Keys)
			{
				ExternalLinkDetail eld = (ExternalLinkDetail) linksCache[externalLinkId];
				if (eld.Url.Equals(url))
				{
					return eld;
				}
			}

			//log operational event with level Error
			OperationalEvent oe = new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error,
					"Missing url - " + url + " could not be found.");
				Logger.Write(oe);

				return null;				
		}

		#endregion

		#region private/internal methods

		/// <summary>
		/// Populates the linksCache hashtable with data from the ExternalLinks table
		/// </summary>
		internal void Load()
		{

			Hashtable localLinksCache= new Hashtable();
			SqlHelper helper = new SqlHelper();

			try
			{
				helper.ConnOpen(SqlHelperDatabase.TransientPortalDB);
				//call the stored procedure which returns the list of records in the ExternalLinks table
				SqlDataReader reader = helper.GetReader("GetExternalLinks",localLinksCache);
				
				//Assign the column ordinals
				externalLinkIDColumnOrdinal	= reader.GetOrdinal("Id");
				linkUrlColumnOrdinal		= reader.GetOrdinal("URL");
				isValidColumnOrdinal		= reader.GetOrdinal("Valid");
				startDateColumnOrdinal		= reader.GetOrdinal("StartDate");
				endDateColumnOrdinal		= reader.GetOrdinal("EndDate");
				linkTextColumnOrdinal		= reader.GetOrdinal("LinkText");

				while (reader.Read())
				{
					string externalLinkId = reader.GetString(externalLinkIDColumnOrdinal);
					string url = reader.GetString(linkUrlColumnOrdinal);
					bool isValid = reader.GetBoolean(isValidColumnOrdinal);
					
					TDDateTime startDate;
					TDDateTime endDate;
					string linkText;
					if(reader.IsDBNull(startDateColumnOrdinal))
					{
						startDate = new TDDateTime(DateTime.MinValue);
					}
					else
					{
						startDate = new TDDateTime(reader.GetDateTime(startDateColumnOrdinal));
					}
					if(reader.IsDBNull(endDateColumnOrdinal))
					{
						endDate = new TDDateTime(DateTime.MaxValue);
					}
					else
					{
						endDate = new TDDateTime(reader.GetDateTime(endDateColumnOrdinal));
					}
					if(reader.IsDBNull(linkTextColumnOrdinal))
					{
						linkText = string.Empty;
					}
					else
					{
						linkText = reader.GetString(linkTextColumnOrdinal);
					}

					ExternalLinkDetail eld = new ExternalLinkDetail(url,isValid,startDate,endDate,linkText);
					localLinksCache.Add(externalLinkId,eld);				
					
				}
				
				reader.Close();

				Hashtable oldTable = new Hashtable();
				
				//Create reference to the existing linksCache hashtable prior to replacing it				
				//This is for multi-threading purposes to ensure there is always data to reference
				if (linksCache !=null)
				{
					oldTable = linksCache;
				}
		
				//replace the linksCache hashtable
				linksCache = localLinksCache;

				//clear the old data storage hashtable
				oldTable.Clear();

			}
			finally
			{
				if (helper.ConnIsOpen)
					helper.ConnClose();
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
					Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Warning, "DataChangeNotificationService was not present when initializing ExternalLinks"));
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
		/// <param name="sender">Notifying object</param>
		/// <param name="e">Event Parameter</param>
		private void DataChangedNotificationReceived(object sender, ChangedEventArgs e)
		{
			if (e.GroupId == dataChangeNotificationGroup)
				Load();
		}

		#endregion
	}
}
