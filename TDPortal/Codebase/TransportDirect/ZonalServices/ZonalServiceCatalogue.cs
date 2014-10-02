// *********************************************** 
// NAME                 : ZonalServiceCatalogue.cs
// AUTHOR               : Ken Josling
// DATE CREATED         : 12/12/2005 
// DESCRIPTION			: This class contains the reference data that will be used by Zonal Services
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ZonalServices/ZonalServiceCatalogue.cs-arc  $
//
//   Rev 1.1   Oct 10 2012 14:31:28   mmodi
//Updated trace logging level
//Resolution for 5859: Error message logging tidy-up
//
//   Rev 1.0   Nov 08 2007 13:03:12   mturner
//Initial revision.
//
//   Rev 1.2   Mar 29 2007 17:32:22   dsawe
//added for checking if nptgifo is null
//Resolution for 4362: Local Zonal Services Phase 2 & 3
//
//   Rev 1.1   Mar 16 2007 10:00:48   build
//Automatically merged from branch for stream4362
//
//   Rev 1.0.1.3   Mar 14 2007 14:25:12   dsawe
//return string.empty for empty string
//Resolution for 4362: Local Zonal Services Phase 2 & 3
//
//   Rev 1.0.1.2   Mar 14 2007 11:36:48   dsawe
//checking if zonalServiceLinks.ContainsKey(naptan)
//Resolution for 4362: Local Zonal Services Phase 2 & 3
//
//   Rev 1.0.1.1   Mar 12 2007 11:24:46   dsawe
//updated
//Resolution for 4362: Local Zonal Services Phase 2 & 3
//
//   Rev 1.0.1.0   Mar 09 2007 13:50:56   dsawe
//added code for station stop link to display admin & district links
//Resolution for 4362: Local Zonal Services Phase 2 & 3
//
//   Rev 1.0   Dec 19 2005 12:12:40   kjosling
//Initial revision.

using System;
using System.Collections;
using System.Diagnostics;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common; 
using TransportDirect.Common.Logging;
using TransportDirect.Common.DatabaseInfrastructure;
using TransportDirect.UserPortal.DataServices;
using System.Data.SqlClient;
using TransportDirect.UserPortal.ExternalLinkService;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.Presentation.InteractiveMapping;
using System.Globalization;

namespace TransportDirect.UserPortal.ZonalServices
{
	/// <summary>
	/// Contains the reference data that will be used by Zonal Services
	/// </summary>
	public class ZonalServiceCatalogue
	{
		#region Private Members

		private Hashtable zonalServiceLinks = new Hashtable();
		//Properties used for loading the reference data
		private Hashtable zonalServiceLinksCurrentLoad = new Hashtable();
		// hashtable for loading adminarea & district links
		private Hashtable hashAdminAreaDistrictLinks = new Hashtable();
		private const string CONST_NOLINK = "NOLINK";

		/// <summary>
		/// Structure used to define hashkey for accessing data cached in hashAdminAreaDistrictLinks hashtable
		/// </summary>
		private struct AdminDistrictKey
		{
			public string AdminAreaId, DistrictId;

			public AdminDistrictKey(string AdminAreaId, string DistrictId)
			{
				this.AdminAreaId = AdminAreaId;
				this.DistrictId = DistrictId;
			}
		}

		#endregion

		#region Constructor

		public ZonalServiceCatalogue()
		{
			Load();
		}

		#endregion

		#region Private Methods

		/// <summary>
		/// Connects to the PermanentPortal database and retrieves the reference data, which
		/// is loaded into memory at runtime
		/// </summary>
		private void Load()
		{
			SqlHelper helper = new SqlHelper();
			try
			{
				helper.ConnOpen(SqlHelperDatabase.TransientPortalDB);

				#region Load zonal stops links
				//Retrieve the reference data into a flat table of results
				SqlDataReader reader = helper.GetReader("GetZonalServicesData", new Hashtable(), new Hashtable());
	
				//Assign the column ordinals
				int naptanColumnOrdinal			= reader.GetOrdinal("Naptan");
				int externalLinkIdColumnOrdinal	= reader.GetOrdinal("ExternalLinkID");
				
				zonalServiceLinksCurrentLoad.Clear();

				while (reader.Read())
				{
					//Add a reference to each external link against its natpan
					string naptan = reader.GetString(naptanColumnOrdinal);
					string externalLinkId = reader.GetString(externalLinkIdColumnOrdinal);
					AddRecord(naptan, externalLinkId);
				}
				reader.Close();
				zonalServiceLinks = zonalServiceLinksCurrentLoad;
				#endregion

				#region Load Admin District Links
				reader = helper.GetReader("GetZonalAdminDistrictLinks", new Hashtable(), new Hashtable());

				int AdminAreaIdColumnOrdinal = reader.GetOrdinal("AdminAreaId");
				int DistrictIdColumnOrdinal = reader.GetOrdinal("DistrictId");
				int AdminAreaFaresLinkIdColumnOrdinal = reader.GetOrdinal("AdminAreaFaresLinkId");

				while (reader.Read())
				{
					string AdminAreaId = reader.GetString(AdminAreaIdColumnOrdinal);
					string DistrictId = reader.GetString(DistrictIdColumnOrdinal);
					string AdminAreaFaresLinkId = reader.GetString(AdminAreaFaresLinkIdColumnOrdinal);
					AdminDistrictKey hashkeyAdminDistrict = new AdminDistrictKey(AdminAreaId, DistrictId);
					hashAdminAreaDistrictLinks.Add(hashkeyAdminDistrict, AdminAreaFaresLinkId);
				}
				reader.Close();
	
				#endregion
			}
			finally
			{
				if (helper.ConnIsOpen)
					helper.ConnClose();
			}

		}

		/// <summary>
		/// Adds Zonal Service link information to the cache
		/// </summary>
		/// <param name="naptan">The naptan</param>
		/// <param name="externalLinkId">The unique identifier for the corresponding external link</param>
		private void AddRecord(string naptan, string externalLinkId)
		{
			ArrayList externalLinkIds;
			//If the hashtable currently contains external links for the current naptan, then
			//add the link, else insert a new entry into the hashtable for the current naptan
			if(zonalServiceLinksCurrentLoad.ContainsKey(naptan))
			{
				externalLinkIds = (ArrayList)zonalServiceLinksCurrentLoad[naptan];
				externalLinkIds.Add(externalLinkId);
			}
			else
			{
				externalLinkIds = new ArrayList();
				externalLinkIds.Add(externalLinkId);
				zonalServiceLinksCurrentLoad.Add(naptan, externalLinkIds);
			}
		}

		public string GetAdminAreaDistrictLinks(string AdminArea, string DistrictId)
		{
			bool noOperatorMatch = false;
			string adminLinksUrlId = String.Empty;

			AdminDistrictKey hashkeyToMatch = new AdminDistrictKey(AdminArea, DistrictId);

			if (hashAdminAreaDistrictLinks.ContainsKey(hashkeyToMatch))
				adminLinksUrlId = (string)hashAdminAreaDistrictLinks[hashkeyToMatch];
			else
				noOperatorMatch = true;
			
			if (noOperatorMatch)
				return string.Empty;
			else
			{
				// (returns empty url if no match or if there's a match but no available URL for that mode & operator)
				if (String.Compare(adminLinksUrlId, CONST_NOLINK, true, CultureInfo.InvariantCulture) == 0)
					adminLinksUrlId = string.Empty;

				return adminLinksUrlId;
			}
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Retrieves the zonal service links for a given stop. Returns null if no zonal
		/// service links exist for the given stop
		/// </summary>
		/// <param name="naptan">The naptan identifier for the current stop</param>
		/// <returns>An array of ExternalLinkDetail objects representing the zonal service
		/// links for the given stop</returns>
		public ExternalLinkDetail[] GetZonalServiceLinks(string naptan)
		{
				string adminLinkId = string.Empty;
				ExternalLinkDetail adminLinkDetail = null;
				IGisQuery gisQuery = (IGisQuery)TDServiceDiscovery.Current[ServiceDiscoveryKey.GisQuery];
				NPTGInfo nptginfo = gisQuery.GetNPTGInfoForNaPTAN(naptan);

				// if(!zonalServiceLinks.ContainsKey(naptan)) return null;
		
				ArrayList externalLinkDetails = new ArrayList();
			if(zonalServiceLinks.ContainsKey(naptan))
			{
				ArrayList externalLinkIDs = (ArrayList)zonalServiceLinks[naptan];
				//Get the external link for each zonal service. Link must be in publication to be returned
				foreach(string externalLinkID in externalLinkIDs)
				{
					ExternalLinkDetail link = (ExternalLinkDetail)ExternalLinks.Current[externalLinkID];
					if(link != null)
					{	
						if(link.Published)
						{	externalLinkDetails.Add(link);	}
					}
					else 
					{
                        if (!string.IsNullOrEmpty(externalLinkID))
                        {
                            string errorMessage = "No ExternalLink was found for link id: " + externalLinkID + ". ZonalServices and ExternalLinks are out of sync.";
                            OperationalEvent oe = new OperationalEvent(TDEventCategory.Database,
                                TDTraceLevel.Error, errorMessage);
                            Trace.Write(oe);
                        }
					}
				}
			}
			if(nptginfo != null)
			{
				adminLinkId = GetAdminAreaDistrictLinks(nptginfo.AdminAreaID, nptginfo.DistrictID);
			}
			if(adminLinkId != string.Empty)
			{
				adminLinkDetail = (ExternalLinkDetail)ExternalLinks.Current[adminLinkId];
			}
			
				if((adminLinkDetail !=null))
				{
					if(adminLinkDetail.Published)
					{
						if(!externalLinkDetails.Contains(adminLinkDetail))
						{	
							externalLinkDetails.Add(adminLinkDetail);
						}
					}
				}
				else 
				{
                    if (!string.IsNullOrEmpty(adminLinkId))
                    {
                        string errorMessage = "No ExternalLink was found for admin link id: " + adminLinkId + ". Admin Links and ExternalLinks are out of sync.";
                        OperationalEvent oe = new OperationalEvent(TDEventCategory.Database,
                            TDTraceLevel.Error, errorMessage);
                        Trace.Write(oe);
                    }
				}
			
				if(externalLinkDetails.Count == 0)
				{
					return null;
				}
				else
				{
					return (ExternalLinkDetail[])externalLinkDetails.ToArray(typeof(ExternalLinkDetail));
				}
			
		}

		#endregion
	
	}
}
