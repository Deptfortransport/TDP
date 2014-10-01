// ********************************************************************* 
// NAME                 : TDAdditionalData.cs 
// AUTHOR               : Alistair Caunt
// DATE CREATED         : 16/10/2003
// DESCRIPTION			: Implementation of TDAdditionalData
// ********************************************************************** 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/AdditionalDataModule/TDAddtionalData.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:18:10   mturner
//Initial revision.
//
//   Rev 1.12   Apr 26 2006 12:12:08   RPhilpott
//Manual merge of stream 35.
//
//   Rev 1.11.1.1   Mar 30 2006 17:57:26   RPhilpott
//Remove superfluous logging
//Resolution for 35: DEL 8.1 Workstream - Find Cheaper (Rail)
//
//   Rev 1.11.1.0   Mar 30 2006 17:22:16   RPhilpott
//Use cache for Naptan->CRS and NLC lookups.
//Resolution for 35: DEL 8.1 Workstream - Find Cheaper (Rail)
//
//   Rev 1.11   Dec 01 2005 18:58:50   RPhilpott
//Use Cache to reduce CRS->Naptan lookup overhead.
//Resolution for 2992: DN040:  failure to obtain coach journeys for a ticket in SBP
//
//   Rev 1.10   Mar 31 2005 09:25:24   rscott
//Changes from code review
//
//   Rev 1.9   Mar 30 2005 09:40:00   rscott
//Updated after FXCop fixes
//
//   Rev 1.8   Jan 12 2005 16:58:18   RScott
//updated to accomodate new method LookupStationNameForCRS()
//
//   Rev 1.7   Jan 11 2005 15:21:08   RScott
//Updated to include new method LookupNaptanForCode
//
//   Rev 1.6   May 28 2004 16:15:24   asinclair
//Updated for new DLL.  Added (byte) value
//
//   Rev 1.5   Nov 21 2003 16:54:58   acaunt
//TDAdditionalData now returns string.empty is an invalid NaPTAN (null or empty) is provided
//
//   Rev 1.4   Nov 07 2003 16:29:26   RPhilpott
//Changes to accomodate removal of station name lookup by Atkins.
//
//   Rev 1.3   Nov 07 2003 14:24:22   RPhilpott
//Use arrray item 5 not 4 for CRS/NLC lookups
//
//   Rev 1.2   Nov 07 2003 14:07:40   RPhilpott
//Add TrainTaxiLink support
//
//   Rev 1.1   Nov 05 2003 19:16:14   RPhilpott
//Add CRS and Station Name convenience methods
//
//   Rev 1.0   Oct 16 2003 20:52:42   acaunt
//Initial Revision

using System;
using System.Data;
using System.Collections;
using System.Diagnostics;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.PropertyService.Properties;

using TransportDirect.AdditionalData.AccessModule;

namespace TransportDirect.UserPortal.AdditionalDataModule
{
	/// <summary>
	/// Summary description for TDAddtionalData.
	/// </summary>
	public class TDAdditionalData : IAdditionalData
	{
		private static AdditionalDataAccessModule dataModule;
		
		private const string TRAINTAXI_LINK_CATEGORY = "TrainTaxiLink";
		private const string VALUE_COLUMN_NAME = "Value";
		private const string TYPE_COLUMN_NAME = "Type";
		private const string PDSKEY_COLUMN_NAME = "PDS Key";
		private const string COMMON_NAME_TYPE = "CommonName";
		private const string STOP_CATEGORY = "Stop";

		private const string PROP_LOOKUPCACHE_TIMEOUT = "AdditionalData.LookupCacheTimeoutSeconds";
		private const int DEFAULT_TIMEOUT = 60 * 30;    // used if property not found

		private const string PROP_LOOKUPCACHE_PREFIX = "AdditionalData.LookupCachePrefix";
		private const string DEFAULT_PREFIX = "AdditionalDataLookup:";    // used if property not found

		static TDAdditionalData()
		{
			IPropertyProvider properties = Properties.Current;
			String connection = properties["AdditionalDataDB"];
			dataModule =  new AdditionalDataAccessModule(connection);			
		}

		public TDAdditionalData()
		{
		}

		/// <summary>
		/// Implementation of IAdditionalData method
		/// </summary>
		/// <param name="type"></param>
		/// <param name="naptan"></param>
		/// <returns></returns>
		public string LookupFromNaPTAN(LookupType type, String naptan)
		{
			// Avoid nulls or empty strings throwing errors
			if (naptan == null || naptan.Equals(string.Empty)) 
			{
				return string.Empty;
			}

			string[] results = GetFromCache(type.Type, naptan);
			
			if	(results != null) 
			{
				return results[0];
			}
			
			DataSet data = dataModule.GetByType(pdsEnum.NaPTAN, naptan,	type.Category, type.Type, (byte) 1);

			if (data.Tables[0].Rows.Count != 0) 
			{
				string result = data.Tables[0].Rows[0][VALUE_COLUMN_NAME].ToString();
				AddToCache(type.Type, naptan, new string[] { result });
				return result;
			} 
			else 
			{
				return string.Empty;
			}
		}

		/// <summary>
		/// Implementation of IAdditionalData method
		/// </summary>
		/// <param name="naptan"></param>
		/// <returns></returns>
		public LookupResult[] LookupFromNaPTAN(String naptan)
		{
			LookupResult result = null;
			ArrayList results = new ArrayList();

			DataSet data = dataModule.GetByKey(pdsEnum.NaPTAN, naptan, (byte) 1);
			
			foreach(DataRow row in data.Tables[0].Rows)
			{
				result = new LookupResult(LookupType.FindLookupType(row[TYPE_COLUMN_NAME].ToString()), row[VALUE_COLUMN_NAME].ToString());
				if (result.Type != null) 
				{
					results.Add(result);
				}
				
			}
			return (LookupResult[])results.ToArray(result.GetType());
		}

		/// <summary>
		/// Convenience method to get CRS code corresponding to specified NAPTAN
		/// </summary>
		/// <param name="naptan">NAPTAN</param>
		/// <returns>CRS code</returns>
		public string LookupCrsForNaptan(String naptan)
		{
			return LookupFromNaPTAN(LookupType.CRS_Code, naptan); 
		}

		/// <summary>
		/// Convenience method to get NLC code corresponding to specified NAPTAN
		/// </summary>
		/// <param name="naptan">NAPTAN</param>
		/// <returns>NLC code</returns>
		public string LookupNlcForNaptan(String naptan)
		{
			return LookupFromNaPTAN(LookupType.NLC_Code, naptan); 
		}

		/// <summary>
		/// Get station name corresponding to specified NAPTAN
		/// </summary>
		/// <param name="naptan">NAPTAN</param>
		/// <returns>station name</returns>
		public string LookupStationNameForNaptan(String naptan)
		{
			DataSet data = dataModule.GetByType(pdsEnum.NaPTAN, naptan, STOP_CATEGORY, COMMON_NAME_TYPE, (byte) 1);

			if (data.Tables[0].Rows.Count != 0) 
			{
				return data.Tables[0].Rows[0][VALUE_COLUMN_NAME].ToString();
			} 
			else 
			{
				return string.Empty;
			}
		}

		/// <summary>
		/// Get station name corresponding to specified CRS
		/// Looks for NAPTAN first then uses the first returned NAPTAN value in the
		/// string array to find station name.
		/// </summary>
		/// <param name="code">CRS Code</param>
		/// <returns>station name</returns>
		public string LookupStationNameForCRS(String code)
		{
			//get the list of naptan/s
			string[] results = LookupNaptanForCode(code, LookupType.CRS_Code);
			
			if (results.Length > 0)
			{
				//get the first result from the returned string array
				string naptan = results[0];

				//use derived naptan to return stationname
				return LookupStationNameForNaptan(naptan);
			}
			else
			{
				return string.Empty;
			}
		}

		/// <summary>
		/// Retrieve dataset of TrainTaxi information for specified NAPTAN.
		/// </summary>
		/// <param name="naptan">NAPTAN</param>
		/// <returns>Dataset of TrainTaxi records</returns>
		public DataSet RetrieveTrainTaxiInfoForNaptan(String naptan)
		{
			return dataModule.GetByCategory(pdsEnum.NaPTAN, naptan, TRAINTAXI_LINK_CATEGORY, (byte)1);
		}

		/// <summary>
		/// Get NAPTAN list for either a CRS or NLC code provided
		/// </summary>
		/// <param name="code">code</param>
		/// <param name="type">type</param> 
		/// <returns>string array of naptans</returns>
		public string[] LookupNaptanForCode(String code, LookupType type)
		{
			string[] results = GetFromCache(type.Type, code);

			if	(results == null) 
			{
				IPropertyProvider properties = Properties.Current;
				
				string napString = properties["FindA.NaptanPrefix.Rail"];
				string naptanString = napString + "%";
				results = new string[0];
				
				// Avoid nulls or empty strings throwing errors
				if (!(code == null || code.Length == 0 || napString == null || napString.Length == 0))
				{
					//Get the dataset
					DataSet data = dataModule.GetByValue(pdsEnum.NaPTAN, naptanString, type.Category, type.Type, code,(byte) 1);

					results = new string[data.Tables[0].Rows.Count];
					
					//Build String array from dataset returned
					int i = 0;
					foreach (DataRow row in data.Tables[0].Rows)
					{
						if (!(row[PDSKEY_COLUMN_NAME].ToString() == null || row[PDSKEY_COLUMN_NAME].ToString().Length == 0)) 
						{
							results[i] = row[PDSKEY_COLUMN_NAME].ToString();
						}
						i++;
					}					
				}
				AddToCache(type.Type, code, results);
			}

			//will return either empty string array or some naptans
			return results;
		}

		private string[] GetFromCache(string type, string code)
		{
			ICache cache = (ICache)TDServiceDiscovery.Current[ServiceDiscoveryKey.Cache];

			string prefix = Properties.Current[PROP_LOOKUPCACHE_PREFIX];

			if	(prefix == null || prefix.Length == 0)
			{
				prefix = DEFAULT_PREFIX; 
			}
			
			return (string[])(cache[prefix + type + ":" + code]);
		}

		private void AddToCache(string type, string code, string[] results)
		{
			ICache cache = (ICache)TDServiceDiscovery.Current[ServiceDiscoveryKey.Cache];
			
			int secs = DEFAULT_TIMEOUT; 
                    
			try 
			{
				secs = Convert.ToInt32(Properties.Current[PROP_LOOKUPCACHE_TIMEOUT],
					System.Globalization.CultureInfo.InvariantCulture);
			}
			catch
			{
				Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure,
					TDTraceLevel.Error, "Illegal definition of property " + PROP_LOOKUPCACHE_TIMEOUT + ". " + PROP_LOOKUPCACHE_TIMEOUT + " should contain a valid number, defaulting to " + DEFAULT_TIMEOUT + "secs."));
			}

			string prefix = Properties.Current[PROP_LOOKUPCACHE_PREFIX];

			if	(prefix == null || prefix.Length == 0)
			{
				prefix = DEFAULT_PREFIX; 
			}

			cache.Add(prefix + type + ":" + code, results, new TimeSpan(0, 0, secs));
		}


	}
}
