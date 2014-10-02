// *********************************************** 
// NAME			: LocationInformationCatalogue.cs
// AUTHOR		: Mitesh Modi
// DATE CREATED	: 18/10/07
// DESCRIPTION	: Class to provide location information for LocationInformation page. 
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/LocationInformationService/LocationInformationCatalogue.cs-arc  $
//
//   Rev 1.2   Jun 27 2008 09:40:54   apatel
//CCN - 458 Accessibility Updates - Improved linking
//
//   Rev 1.1   Nov 29 2007 12:42:56   mturner
//Changes to remove .Net2 compiler warnings
//
//   Rev 1.0   Nov 28 2007 14:56:42   mturner
//Initial revision.
//
//   Rev 1.3   Nov 06 2007 14:18:32   mmodi
//Used Naptan cache entry to retrieve airport name
//Resolution for 4528: Departure Boards: Airport name not being displayed
//
//   Rev 1.2   Nov 01 2007 13:20:20   mmodi
//Added logging when retrieving the Airport name to aid in debugging
//Resolution for 4518: Del 9.8 - Air Departure Boards
//
//   Rev 1.1   Oct 26 2007 15:05:06   mmodi
//Updated location information change notification
//Resolution for 4518: Del 9.8 - Air Departure Boards
//
//   Rev 1.0   Oct 25 2007 15:38:48   mmodi
//Initial revision.
//Resolution for 4518: Del 9.8 - Air Departure Boards
//

using System;
using System.Collections;
using System.Data.SqlClient;

using TransportDirect.Common;
using TransportDirect.Common.DatabaseInfrastructure;
using TransportDirect.Common.Logging;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.AirDataProvider;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.UserPortal.ExternalLinkService;
using TransportDirect.UserPortal.LocationService;

using Logger = System.Diagnostics.Trace;

namespace TransportDirect.UserPortal.LocationInformationService
{
	/// <summary>
	/// Summary description for LocationInformationCatalogue.
	/// </summary>
	public class LocationInformationCatalogue
	{
		#region Private members
		private const string DataChangeNotificationGroup = "LocationInformationCatalogue";
		private bool receivingChangeNotifications;

		// Hashtable of LocationInformation 
		private Hashtable locationInformationLinks = new Hashtable();

		// Used for loading the reference data
		private Hashtable locationInformationCurrentLoad = new Hashtable();

		#endregion

		#region Constructor
		/// <summary>
		/// Default constructor, loads data and registers for change notification
		/// </summary>
		public LocationInformationCatalogue()
		{
			LoadData();
			receivingChangeNotifications = RegisterForChangeNotification();
		}
		#endregion

		#region Private methods
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
					Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Warning, "DataChangeNotificationService was not present when initialising LocationInformationCatalogue"));
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


		/// <summary>
		/// Loads the data and performs pre processing
		/// </summary>
		private void LoadData()
		{
			SqlHelper helper = new SqlHelper();

			try
			{
				// Initialise a SqlHelper and connect to the database.
				Logger.Write( new OperationalEvent( TDEventCategory.Business,
					TDTraceLevel.Info, "Opening database connection to " + SqlHelperDatabase.TransientPortalDB.ToString() ));
				helper.ConnOpen(SqlHelperDatabase.TransientPortalDB);

				#region Load links into hashtables
				//Retrieve the reference data into a flat table of results
				SqlDataReader reader = helper.GetReader("GetLocationInformation", new Hashtable(), new Hashtable());

				//Assign the column ordinals
				int naptanColumnOrdinal			= reader.GetOrdinal("Naptan");
				int externalLinkIdColumnOrdinal	= reader.GetOrdinal("ExternalLinkID");
				int linkTypeColumnOrdinal		= reader.GetOrdinal("LinkType");

				locationInformationCurrentLoad.Clear();
				locationInformationLinks.Clear();

				while (reader.Read())
				{
					//Add a reference to each external link against its natpan
					string naptan = reader.GetString(naptanColumnOrdinal);
					string externalLinkId = reader.GetString(externalLinkIdColumnOrdinal);
					string linkType = reader.GetString(linkTypeColumnOrdinal);
					AddRecord(naptan, externalLinkId, linkType);
				}
				reader.Close();
				
				locationInformationLinks = locationInformationCurrentLoad;
				#endregion

				// Record the fact that the data was loaded.
				if ( TDTraceSwitch.TraceVerbose ) 
				{
					Logger.Write( new OperationalEvent( TDEventCategory.Business,
						TDTraceLevel.Verbose, "Loading Location information completed" ));
				}

			}
			catch (Exception e)
			{
				// Catching the base Exception class because we don't want any possibility
				// of this raising any errors outside of the class in case it causes the
				// application to fall over. As long as the exception doesn't occur in 
				// the final block of code, which copies the new data into the module-level
				// hashtables and arraylists, the object will still be internally consistant,
				// although the data will be inconsistant with that stored in the database.
				// One exception to this: if this is the first time LoadData has been run,
				// the exception should be raised.
				Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, "An error occurred whilst attempting to reload the Location information data.", e));
				if ((locationInformationLinks == null) || (locationInformationLinks.Count == 0))
				{
					throw;
				}
			}
			finally
			{
				//close the database connection
				if (helper.ConnIsOpen)
					helper.ConnClose();
			}
		}


		/// <summary>
		/// Adds Location Information link information to the cache
		/// </summary>
		/// <param name="naptan">The naptan</param>
		/// <param name="externalLinkId">The unique identifier for the corresponding external link</param>
		private void AddRecord(string naptan, string externalLinkId, string externallinkType)
		{
			LocationInformation locationInformation;

			LocationInformationLinkType linkType = (LocationInformationLinkType)Enum.Parse(typeof(LocationInformationLinkType), externallinkType, true);

			//If the hashtable currently contains links for the current naptan, then
			//add the link, else insert a new entry into the hashtable for the current naptan
			if(locationInformationCurrentLoad.ContainsKey(naptan))
			{
				locationInformation = (LocationInformation)locationInformationCurrentLoad[naptan];
				locationInformation.AddLinkID(externalLinkId, linkType);
			}
			else
			{
				locationInformation = new LocationInformation();
				locationInformation.AddLinkID(externalLinkId, linkType);
				locationInformationCurrentLoad.Add(naptan, locationInformation);
			}
		}

		/// <summary>
		/// Populates the urls for the LocationInformation object
		/// </summary>
		/// <param name="li">LocationInformation object to populate with links</param>
		/// <returns>LocationInformation object</returns>
		private LocationInformation PopulateLocationInformationLinks(LocationInformation li, string naptan)
		{
			#region Get the Departure Link
			if (li.DepartureLinkID != string.Empty)
			{
				ExternalLinkDetail link = (ExternalLinkDetail)ExternalLinks.Current[li.DepartureLinkID];
				if (link != null)
				{	

					link.LinkText = GetAirportName(naptan);
					
					if (link.Published)
						li.DepartureLink = link;
				}
				else 
				{
					string errorMessage = "No ExternalLink was found for link id: " + li.DepartureLinkID + ". LocationInformation and ExternalLinks are out of sync.";
					OperationalEvent oe = new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Error, errorMessage);
					Logger.Write(oe);
				}
			}
			#endregion

			#region Get the Arrival Link
			if (li.ArrivalLinkID != string.Empty)
			{
				ExternalLinkDetail link = (ExternalLinkDetail)ExternalLinks.Current[li.ArrivalLinkID];
				if (link != null)
				{	
					link.LinkText = GetAirportName(naptan);

					if (link.Published)
						li.ArrivalLink = link;
				}
				else 
				{
					string errorMessage = "No ExternalLink was found for link id: " + li.ArrivalLinkID + ". LocationInformation and ExternalLinks are out of sync.";
					OperationalEvent oe = new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Error, errorMessage);
					Logger.Write(oe);
				}
			}
			#endregion

			#region Get the Information Link
			if (li.InformationLinkID != string.Empty)
			{
				ExternalLinkDetail link = (ExternalLinkDetail)ExternalLinks.Current[li.InformationLinkID];
				if (link != null)
				{	
					link.LinkText = GetAirportName(naptan);

					if (link.Published)
						li.InformationLink = link;
				}
				else 
				{
					string errorMessage = "No ExternalLink was found for link id: " + li.InformationLinkID + ". LocationInformation and ExternalLinks are out of sync.";
					OperationalEvent oe = new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Error, errorMessage);
					Logger.Write(oe);
				}
			}
			#endregion

            #region Get the Accessibility Link
            if (li.AccessibilityLinkID != string.Empty)
            {
                ExternalLinkDetail link = (ExternalLinkDetail)ExternalLinks.Current[li.AccessibilityLinkID];
                if (link != null)
                {
                    link.LinkText = GetAirportName(naptan);

                    if (link.Published)
                        li.AccessibilityLink = link;
                }
                else
                {
                    string errorMessage = "No ExternalLink was found for link id: " + li.AccessibilityLinkID + ". LocationInformation and ExternalLinks are out of sync.";
                    OperationalEvent oe = new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Error, errorMessage);
                    Logger.Write(oe);
                }
            }
            #endregion

			return li;
		}

		/// <summary>
		/// Returns the display name for an airport
		/// </summary>
		/// <param name="naptan"></param>
		/// <returns></returns>
		private string GetAirportName(string naptan)
		{
			NaptanCacheEntry nce = NaptanLookup.Get(naptan, string.Empty);

			if	(nce != null && nce.Found) 
			{
				string message = "Airport object was found for LocationInformation object with NaPTAN: " + naptan + " Airport name: " + nce.Description;
				OperationalEvent oe = new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Info, message);
				Logger.Write(oe);

				return nce.Description;
			}
			else
			{
				string message = "No Airport object was found for LocationInformation object with NaPTAN: " + naptan + " ";
				OperationalEvent oe = new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Info, message);
				Logger.Write(oe);

				return string.Empty;
			}
		}
		#endregion

		#region Event handler

		/// <summary>
		/// Used by the Data Change Notification service to reload the data if it is changed in the DB
		/// </summary>
		private void DataChangedNotificationReceived(object sender, ChangedEventArgs e)
		{
			if (e.GroupId == DataChangeNotificationGroup)
				LoadData();
		}

		#endregion

		#region Public methods
		/// <summary>
		/// Retrieves the location information links for a given naptan. Returns null if no links available
		/// </summary>
		/// <param name="naptan">The naptan identifier for the current stop</param>
		/// <returns>A LocationInformation object representing the information available for the given stop</returns>
		public LocationInformation GetLocationInformation(string naptan)
		{
			if (!locationInformationLinks.ContainsKey(naptan)) 
				return null;
		
			LocationInformation li = (LocationInformation)locationInformationLinks[naptan];

			li = PopulateLocationInformationLinks(li, naptan);

			// Check if we have any links for this naptan (location information object)
            if ((li.DepartureLink == null) && (li.ArrivalLink == null) && (li.InformationLink == null) && (li.AccessibilityLink == null))
			{
				return null;
			}
			else
			{
				return li;
			}
		}


		/// <summary>
		/// Retrieve only the main airport location departure board links, i.e. not the individual terminals for an airport.
		/// Returns null if none available
		/// </summary>
		/// <returns></returns>
		public LocationInformation[] GetAirportDepartureBoardLinks()
		{
			if ((locationInformationLinks != null) && (locationInformationLinks.Count > 0))
			{
				// Filter out airport terminals to leave us with only the major airport naptans
				ArrayList uniqueAirports = new ArrayList();

				string naptan;
				string airportNaptan;
				foreach (DictionaryEntry de in locationInformationLinks)
				{
					naptan = de.Key.ToString();
					
					// Check it is an airport naptan
					if (naptan.Substring(0,Airport.NaptanPrefix.Length) == Airport.NaptanPrefix)
					{
						// Taking only the 3 characters after the prefix ensures we remove the terminal number
						airportNaptan = Airport.NaptanPrefix + naptan.Substring(Airport.NaptanPrefix.Length, 3);

						// Add to list only if we haven't already included
						if (!uniqueAirports.Contains(airportNaptan))
							uniqueAirports.Add(airportNaptan);
					}
				}

				// Now go through the location hashtable and get the objects we've asked for
				ArrayList arraylistLocInfo = new ArrayList();
				foreach (DictionaryEntry de in locationInformationLinks)
				{
					if (uniqueAirports.Contains(de.Key.ToString()))
					{
						LocationInformation locInfo = (LocationInformation)de.Value;

						locInfo = PopulateLocationInformationLinks(locInfo, de.Key.ToString());

						arraylistLocInfo.Add(locInfo);
					}
				}
		
				// Convert array list to a LocationInformation array
				LocationInformation[] arrayLocInfo = new LocationInformation[arraylistLocInfo.Count];
				arraylistLocInfo.CopyTo(arrayLocInfo);
				
				return arrayLocInfo; 
			}
			else
				return null;
		}
		#endregion
	}
}
