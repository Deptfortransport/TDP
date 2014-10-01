// *********************************************** 
// NAME			: NetworkMapLinks.cs
// AUTHOR		: Paul Cross
// DATE CREATED	: 08/07/2005
// DESCRIPTION	: Implementation for the NetworkMapLinks class
//				  which wraps access to network links information.
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyControl/NetworkMapLinks.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:23:52   mturner
//Initial revision.
//
//   Rev 1.2   Jul 25 2005 20:58:54   pcross
//FxCop updates
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.1   Jul 18 2005 16:19:46   pcross
//Non-functional updates.
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.0   Jul 12 2005 10:45:48   pcross
//Initial revision.
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results

using System;
using System.Collections;
using System.Data.SqlClient;
using TransportDirect.Common.DatabaseInfrastructure;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.Common.ServiceDiscovery;
using System.Globalization;

using Logger = System.Diagnostics.Trace;
using TransportDirect.Common.Logging;


namespace TransportDirect.UserPortal.JourneyControl
{
	/// <summary>
	/// Wraps access to network links information (URLs)
	/// </summary>
	public class NetworkMapLinks : INetworkMapLinks
	{
	
		#region Private Members
		
		private enum FieldIndices {MODE, OPERATOR, URL};
		private const string CONST_DEFAULT = "DE";
		private const string CONST_NOLINK = "NOLINK";

		/// <summary>
		/// Structure used to define hashkey for accessing data cached in network map links hashtable
		/// </summary>
		private struct NetworkMapLinkKey
		{
			public string mode, operatorCode;

			public NetworkMapLinkKey(string mode, string operatorCode)
			{
				this.mode = mode;
				this.operatorCode = operatorCode;
			}
		}

		/// <summary> Hashtable for storing Network Map links </summary>
		private Hashtable networkMapLinksCache = new Hashtable();

		
		#endregion

		#region Constructors

		public NetworkMapLinks()
		{
			// Call the Load method to load the network links into a hashtable object
			Load();
		}

		#endregion

		#region INetworkMapLinks interface methods

		/// <summary>
		/// Given an operator and transport mode, gets the matching URL for operators network map web page (if exists)
		/// </summary>
		/// <param name="mode"></param>
		/// <param name="operatorCode"></param>
		/// <returns></returns>
		public string GetURL(ModeType mode, string operatorCode)
		{
			// Get the matching url from the hashtable
			string url = string.Empty;

			// Create the key in the right format
			NetworkMapLinkKey keyToMatch1 = new NetworkMapLinkKey(mode.ToString(), operatorCode);
			
			// Look up the cached urls for the given key
			if (networkMapLinksCache.ContainsKey(keyToMatch1))
				url = networkMapLinksCache[keyToMatch1].ToString();
			else
			{
				// Exact match not found - try with blank operator
				NetworkMapLinkKey keyToMatch2 = new NetworkMapLinkKey(mode.ToString(), string.Empty);
				if (networkMapLinksCache.ContainsKey(keyToMatch2))
					url = networkMapLinksCache[keyToMatch2].ToString();
			}

			// (returns empty string if no match or if there's a match but no available URL for that mode & operator)
			if (String.Compare(url, CONST_NOLINK, true, CultureInfo.InvariantCulture) == 0)
				url = string.Empty;

			return url;
		}

		#endregion

		#region Public Methods
		
		/// <summary>
		/// Read only property. Implementation of INetworkMapLinks Current method
		/// </summary>
		public static INetworkMapLinks Current
		{
			get
			{
				return (NetworkMapLinks)TDServiceDiscovery.Current[ ServiceDiscoveryKey.NetworkMapLinksService ];				
			}
		}
		
		#endregion

		#region Private Methods

		/// <summary>
		/// Loads all the available network map links data from the database into a hashtable
		/// </summary>
		internal void Load()
		{
			
			Hashtable localLinksCache = new Hashtable();

			SqlDataReader reader;
			SqlHelper helper = new SqlHelper();
			
			// Declare database to open
			SqlHelperDatabase sourceDB = SqlHelperDatabase.TransientPortalDB;

			// Call GetNetworkMapLinks and send the results into a hashtable
			try
			{
				// Get Operator data
				if ( TDTraceSwitch.TraceVerbose ) 
				{
					Logger.Write( new OperationalEvent(TDEventCategory.Business,
						TDTraceLevel.Verbose, "Loading Network Map data started"));
				}

				// Open connection and get a DataReader
				helper.ConnOpen(sourceDB);
				reader = helper.GetReader("GetNetworkMapLinks");

				// Set up a new hashtable with a struct (of mode & operator) as a key and URL as value
				// This will be kept in cache for future reference
				while (reader.Read())
				{
					// Set up and assign the variables for each row
					string operatorCode = string.Empty;
					string mode = string.Empty;
					string url = string.Empty;

					// Operator is blank unless it has a non-default value in the database
					if (reader.GetString(Convert.ToInt32(FieldIndices.OPERATOR, CultureInfo.InvariantCulture)) != CONST_DEFAULT)
					{
						operatorCode = reader.GetString(Convert.ToInt32(FieldIndices.OPERATOR, CultureInfo.InvariantCulture));
					}
					mode = reader.GetString(Convert.ToInt32(FieldIndices.MODE, CultureInfo.InvariantCulture));
					url = reader.GetString(Convert.ToInt32(FieldIndices.URL, CultureInfo.InvariantCulture));

					// Add the row to a local hashtable
					NetworkMapLinkKey hashTableKey = new NetworkMapLinkKey(mode, operatorCode);
					localLinksCache.Add(hashTableKey, url);
				}

				reader.Close();

				Hashtable oldTable = new Hashtable();
				
				//Create reference to the existing networkMapLinksCache hashtable prior to replacing it				
				//This is for multi-threading purposes to ensure there is always data to reference
				if (networkMapLinksCache !=null)
				{
					oldTable = networkMapLinksCache;
				}
		
				//replace the linksCache hashtable
				networkMapLinksCache = localLinksCache;

				//clear the old data storage hashtable
				oldTable.Clear();

				// Log that data has been loaded
				if ( TDTraceSwitch.TraceVerbose ) 
				{
					Logger.Write( new OperationalEvent(TDEventCategory.Business,
						TDTraceLevel.Verbose, "Loading Network Map data completed"));
				}

			}
			catch (SqlException sqle)
			{
				// As there is no serious drawback to this data being missing, just log that there was an execption.
				Logger.Write(new OperationalEvent(TDEventCategory.Database, TDTraceLevel.Error, "An SQL exception occurred whilst attempting to load the Network Map link data.", sqle));
			}
			finally
			{
				if (helper.ConnIsOpen)
					helper.ConnClose();
			}

		}

		#endregion

	}
}
