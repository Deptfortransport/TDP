//********************************************************************************
//NAME         : LocationConvertor.cs
//AUTHOR       : Richard Philpott
//DATE CREATED : 2005-03-01
//DESCRIPTION  : Performs conversions between the different types 
//					of location encoding used by the RBOGateway.
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/RBOGateway/LocationConvertor.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:37:12   mturner
//Initial revision.
//
//   Rev 1.5   Jun 01 2006 17:42:44   rphilpott
//Fix stupid bug -- check locality for zero length, not null ...
//Resolution for 4103: Find Cheaper - journeys not always returned.
//
//   Rev 1.4   Jan 18 2006 18:16:40   RPhilpott
//Chnages for FxCop compliance.
//Resolution for 3398: DN040: Code Review - Unit Test Fixes for DN040
//
//   Rev 1.3   Nov 24 2005 18:22:56   RPhilpott
//Changes to support use of ticket-specific locations in Find-A-Fare AssembleServices calls.
//Resolution for 3198: DN040: Find-A-Fare - handling of non-group tickets
//
//   Rev 1.2   Apr 06 2005 16:04:24   RPhilpott
//Updated commenst only.
//

using System;
using System.Collections;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.ServiceDiscovery;

using TransportDirect.UserPortal.PricingMessages;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.AdditionalDataModule;

using Logger = System.Diagnostics.Trace;

namespace TransportDirect.UserPortal.PricingRetail.RBOGateway
{
	/// <summary>
	/// Performs conversions between the different types 
	/// of location encoding used by the RBOGateway.
	/// Sealed because static-only, so inheritance makes no sense.
	/// </summary>
	public sealed class LocationConvertor
	{
		
		private static IAdditionalData additionalData = (IAdditionalData)TDServiceDiscovery.Current[ServiceDiscoveryKey.AdditionalData];

		private const int CRS_LENGTH = 3; 
		private const int NLC_LENGTH = 4; 

		// Never instantiated 
		//  - has static methods only
		private LocationConvertor()
		{
		}

		/// <summary>
		/// Returns an array of populated LocationDtos corresponding 
		/// to the rail naptans in the given TDLocation
		/// </summary>
		/// <param name="location">TDLocation, including at least one rail naptan</param>
		/// <returns>An array of LocationDto, one per rail naptan flagged as needed 
		///  for fare searches, each containing the corresponding CRS and NLC codes</returns>
		public static LocationDto[] CreateLocationDtos(TDLocation location)
		{
			int naptanCount = 0;

			foreach (TDNaptan tdn in location.NaPTANs)
			{
				if	(tdn.StationType == StationType.Rail && tdn.UseForFareEnquiries)
				{
					naptanCount++;
				}
			}

			if	(naptanCount == 0) 
			{
				TDException tde = new TDException("No valid rail naptan code found for " + location.Description, false, TDExceptionIdentifier.PRHInvalidPricingRequest);
				Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, "Invalid location", tde)); 
				throw tde;
  			}

			LocationDto[] results = new LocationDto[naptanCount];

			naptanCount = 0;

			foreach (TDNaptan tdn in location.NaPTANs)
			{
				if	(tdn.StationType == StationType.Rail && tdn.UseForFareEnquiries)
				{
					string crs = additionalData.LookupFromNaPTAN(LookupType.CRS_Code, tdn.Naptan);
					string nlc = additionalData.LookupFromNaPTAN(LookupType.NLC_Code, tdn.Naptan);

					results[naptanCount++] = new LocationDto(crs, nlc, tdn.Naptan);
				}
			}
			
			return results;
		}

		/// <summary>
		/// Returns a single populated LocationDto corresponding 
		/// to the rail naptan in the given TDLocation, with the assumption
		/// that there is only one rail naptan present, or if there are more
		/// they all map onto the same CRS/NLC code.
		/// </summary>
		/// <param name="location">TDLocation, including at least one rail naptan</param>
		/// <returns>LocationDto containing the corresponding CRS and NLC codes</returns>
		public static LocationDto CreateLocationDto(TDLocation location)
		{
			string naptan = string.Empty;

			foreach (TDNaptan tdn in location.NaPTANs)
			{
				if	(tdn.StationType == StationType.Rail && tdn.UseForFareEnquiries)
				{
					naptan = tdn.Naptan;
					break;
				}
			}

			if	(naptan.Length == 0) 
			{
				TDException tde = new TDException("No valid rail naptan code found for " + location.Description, false, TDExceptionIdentifier.PRHInvalidPricingRequest);
				Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, "Invalid location", tde)); 
				throw tde;
			}

			string crs = additionalData.LookupFromNaPTAN(LookupType.CRS_Code, naptan);
			string nlc = additionalData.LookupFromNaPTAN(LookupType.NLC_Code, naptan);

			return new LocationDto(crs, nlc, naptan);
		}


		/// <summary>
		/// Returns a TDLocation corresponding to the given array of LocationDtos
		/// </summary>
		/// <param name="location">LocationDto, containing CRS and/or NLC codes</param>
		/// <returns>TDLocation, containing naptan(s) for the CRS/NLC codes</returns>
		public static TDLocation CreateTDLocation(LocationDto[] locations)
		{
			ArrayList naptanList = new ArrayList(30);
			string locality = string.Empty;
		
			foreach (LocationDto location in locations)
			{
				string[] naptans = new string[0];

				if	(location.Crs != null && location.Crs.Length == CRS_LENGTH && !location.Crs.StartsWith(" "))
				{
					naptans = additionalData.LookupNaptanForCode(location.Crs, LookupType.CRS_Code);
				}
				else
				{
					if	(location.Nlc != null && location.Nlc.Length == NLC_LENGTH && !location.Nlc.StartsWith(" ")) 
					{
						naptans = additionalData.LookupNaptanForCode(location.Nlc, LookupType.NLC_Code);
					}
				}

				foreach (string naptan in naptans)
				{
					NaptanCacheEntry nce = NaptanLookup.Get(naptan, location.Crs + "/" + location.Nlc);

					if	(nce.Found)
					{
						naptanList.Add(new TDNaptan(nce.Naptan, nce.OSGR, nce.Description));

						// arbitrarily use locality of first naptan as representative
						//  of them all (good enough for rail journey planning)

						if	(locality.Length == 0)
						{
							locality = nce.Locality;
						}
					}
				}

				if	(naptanList.Count == 0)
				{
					Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, "No valid rail naptan code found for " + location.Crs + "/" + location.Nlc)); 
				}
			}

			TDLocation tdl = null;

			if	(naptanList.Count > 0)
			{
				tdl = new TDLocation();

				tdl.NaPTANs			= ((TDNaptan[])naptanList.ToArray(typeof(TDNaptan)));
				tdl.Description		= ((TDNaptan)tdl.NaPTANs[0]).Name;
				tdl.GridReference	= ((TDNaptan)tdl.NaPTANs[0]).GridReference;
				tdl.Locality		= locality;
				tdl.Status			= TDLocationStatus.Valid;
			}
			
			return tdl;
		}

	}
}
