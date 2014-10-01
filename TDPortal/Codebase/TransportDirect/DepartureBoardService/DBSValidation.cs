// *********************************************** 
// NAME                 : DBSValidation.cs
// AUTHOR               : Patrick Assuied
// DATE CREATED         : 07/01/2005
// DESCRIPTION  : Validation class for DepartureBoard service
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/DepartureBoardService/DBSValidation.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:21:04   mturner
//Initial revision.
//
//   Rev 1.6   May 03 2007 12:25:04   mturner
//Added NaPTAN to the list of valid type codes and created a IsValidForNaptan method to handle their validation.
//
//   Rev 1.5   Feb 23 2006 19:15:36   build
//Automatically merged from branch for stream3129
//
//   Rev 1.4.1.0   Jan 30 2006 14:25:06   schand
//Checkin as a branch
//Resolution for 3454: DEL 8.1 Stream: IR for Module assocaitions for Mobile Service TD111
//
//   Rev 1.4   Jan 30 2006 14:20:14   schand
//Added additional check for code length should ve > 0
//Resolution for 3454: DEL 8.1 Stream: IR for Module assocaitions for Mobile Service TD111
//
//   Rev 1.3   Jun 20 2005 12:12:58   tmollart
//Modified code so that locailty searches are performed before SMS.
//Resolution for 2452: Mobile - 'Reading' is mistaken for an SMS code
//
//   Rev 1.2   Mar 31 2005 19:09:54   schand
//Now returning which code (origin/destination/both) has failed. Fix for 4.4, 4.5
//
//   Rev 1.1   Mar 15 2005 15:01:14   schand
//Fixed CRS journey result without going through Code Finder.
//
//   Rev 1.0   Feb 28 2005 17:21:04   passuied
//Initial revision.
//
//   Rev 1.12   Feb 24 2005 14:37:26   passuied
//again after Fxcop
//
//   Rev 1.11   Feb 24 2005 14:19:54   passuied
//Changes for FxCop
//
//   Rev 1.10   Feb 16 2005 14:54:04   passuied
//Change in interface and behaviour of time Request
//
//possibility to plan in the past within configurable time window
//
//   Rev 1.9   Jan 31 2005 11:51:22   passuied
//tidied up
//
//   Rev 1.8   Jan 26 2005 11:38:56   passuied
//remove comment
//
//   Rev 1.7   Jan 19 2005 14:01:40   passuied
//added more validation + changed UT to allow destination to be optional
//
//   Rev 1.6   Jan 18 2005 17:35:58   passuied
//changed after update of CjpInterface
//
//   Rev 1.5   Jan 17 2005 14:48:38   passuied
//Latest code with Unit test OK!
//
//   Rev 1.4   Jan 14 2005 21:00:36   passuied
//Updates during unit tests. Back up before the week end
//
//   Rev 1.3   Jan 14 2005 18:53:14   passuied
//compiling. back up!
//
//   Rev 1.2   Jan 14 2005 10:21:12   passuied
//changes in interface
//
//   Rev 1.1   Jan 11 2005 13:40:30   passuied
//backed up version
//
//   Rev 1.0   Jan 07 2005 16:25:08   passuied
//Initial revision.

using System;
using System.Collections;
using System.Globalization;

using TransportDirect.Common;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.DepartureBoardService.DepartureBoardFacade;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.Common.Logging;
using Logger = System.Diagnostics.Trace;

namespace TransportDirect.UserPortal.DepartureBoardService
{
	public class TDCodeDetailComparer : IComparer
	{
		public int Compare(object codeDetail1, object codeDetail2)
		{
			TDCodeDetail cdCodeDetail1 = codeDetail1 as TDCodeDetail;
			TDCodeDetail cdCodeDetail2 = codeDetail2 as TDCodeDetail;

			if (cdCodeDetail1 == null || cdCodeDetail2 == null)
				throw new ArgumentException(Messages.CodeDetailArgumentException);

			return string.Compare(cdCodeDetail1.CodeType.ToString(), cdCodeDetail2.CodeType.ToString());

		}
	}

	/// <summary>
	/// Validation class for DepartureBoard service
	/// </summary>
	public sealed class DBSValidation
	{
		private DBSValidation()
		{
		}

		#region Private methods : Validation
		private static bool ValidateNaptanIds(string[] NaptanIds)
		{
			if (NaptanIds.Length != 0)
			{
				foreach ( string naptan in NaptanIds)
				{
					if (naptan.Length == 0)
						return false;
				}
				return true;
			}
			else
				return false;
		}


		private static bool IsValidForCrs( DBSLocation location, ArrayList errors)
		{
			bool valid = true;
			//CRS code is always 3 characters
			if (location.Code.Length != 3)
			{
				valid = false;
			}

			// need Naptanids
			if (!ValidateNaptanIds(location.NaptanIds))
			{
				valid = false;
			}

			return valid;
			
		}


		private static bool IsValidForNaptan( DBSLocation location, ArrayList errors)
		{
			bool valid = true;
			
			//need naptanIds
			if (!ValidateNaptanIds(location.NaptanIds))
			{
				valid = false;
			}

			// need locality
			if (location.Locality.Length == 0)
			{
				valid = false;
			}

			return valid;

		}

		private static bool IsValidForSms( DBSLocation location, ArrayList errors)
		{
			bool valid = true;
			
			// no need to check if code there, as not used

			//need naptanIds
			if (!ValidateNaptanIds(location.NaptanIds))
			{
				valid = false;
			}

			// need locality
			if (location.Locality.Length == 0)
			{
				valid = false;
			}

			return valid;

		}

	
		private static bool ValidateDBSLocation(DBSLocation location, ArrayList errors)
		{

			if (IsRejectedCodeType(location.Type ))
				return false;

			switch (location.Type)
			{
				case TDCodeType.CRS:
				{
					bool result = IsValidForCrs(location, errors);
					if (!result)
						errors.Add(string.Format(Messages.IncompleteCrsInfo, location.Code));
					return result;
				}
				case TDCodeType.SMS:
				{
					bool result = IsValidForSms(location, errors);
					if (!result)
						errors.Add(string.Format(Messages.IncompleteNaptanInfo, location.Code));
					return result;
				}
				case TDCodeType.IATA:
				{
					return true;
				}
				case TDCodeType.NAPTAN:
				{
					bool result = IsValidForNaptan(location, errors);
					if (!result)
						errors.Add(string.Format(Messages.IncompleteSmsInfo, location.Code));
					return result;
				}
				default :
				{
	
					return false;
				}
			}
		}

		/// <summary>
		/// Methods that removes all location objects that are not
		/// matching with given code types
		/// </summary>
		/// <param name="type">types to match</param>
		/// <param name="locations">locations to filter</param>
		private static void KeepOnlyMatchingLocations(TDCodeType[] types, ref DBSLocation[] locations)
		{
			ArrayList destLocations = new ArrayList(locations.Length);
			foreach (DBSLocation location in locations)
			{
				bool match = false;
				foreach (TDCodeType type in types)
				{
					if (location.Type == type)
					{
						match = true;
						break;
					}
				}
				// if matches add to destination array
				if (match)
					destLocations.Add(location);
					
			}

			// at the end of processing copy the matching locations back to locations array.
			locations = (DBSLocation[])destLocations.ToArray(typeof(DBSLocation));

		}

		/// <summary>
		/// Methods that returns if type is due to be rejected. Info retrieved from Properties
		/// </summary>
		/// <param name="type"></param>
		/// <returns></returns>
		private static bool IsRejectedCodeType(TDCodeType type)
		{
			string sTypesToRemove = Properties.Current[Keys.CodeTypesToRemoveKey];
			if (sTypesToRemove.Length != 0)
			{ 
				string[] typesToRemove = sTypesToRemove.Split(' ');
				if (typesToRemove.Length != 0)
				{
					foreach (string sType in typesToRemove)
					{
						TDCodeType ctType = (TDCodeType)Enum.Parse(typeof(TDCodeType), sType);
						if (ctType == type)
							return true;
					}
				}
			}
			return false;
		}

		/// <summary>
		/// Method that compares the type of stop locations  and filters 
		/// and checks their type are consistent (e.g. only CRS...)
		/// It will also remove any inconsistent locations.
		/// If at the end of this, none or just one is left, will return false.
		/// </summary>
		/// <param name="stopLocations">requested stop location info</param>
		/// <param name="locationFilters">location filter</param>
		/// <param name="errors">error list to update</param>
		/// <returns></returns>
		private static bool CheckLocationsConsistency(ref DBSLocation[] stopLocations, ref DBSLocation[] locationFilters, ArrayList errors)
		{
			ArrayList matchingTypes = new ArrayList();
			foreach (DBSLocation location in stopLocations)
			{
				// for each location in first array compare type with filter location's 
				// if one matches add type to matching types.
				TDCodeType currentType = location.Type;

				foreach (DBSLocation filter in locationFilters)
				{
					if (currentType == filter.Type)
					{
						// it is assumed there is only one instance of each type in each location array.
						matchingTypes.Add(currentType);
						break;
					}
				}
			}

			// remove all code types we are not interested in. Get these codes from Properties
			string sTypesToRemove = Properties.Current[Keys.CodeTypesToRemoveKey];
			if (sTypesToRemove.Length != 0)
			{ 
				string[] typesToRemove = sTypesToRemove.Split(' ');
				if (typesToRemove.Length != 0)
				{
					foreach (string type in typesToRemove)
					{
						TDCodeType ctType = (TDCodeType)Enum.Parse(typeof(TDCodeType), type);
						matchingTypes.Remove(ctType);
					}
				}
			}
			


			// In stopLocations and location filters remove all unmatching locations
			TDCodeType[] types = (TDCodeType[])matchingTypes.ToArray(typeof(TDCodeType));
			KeepOnlyMatchingLocations(types, ref stopLocations);
			KeepOnlyMatchingLocations(types, ref locationFilters);

			// Finally return if some locations are consistent
			// i.e. if lenght of types not 0
			return (types.Length != 0);

			
			
		}



		#endregion

		#region Private methods : Fetch info from Code Service

		private static void UpdateNaptanIds(ref string[] naptanIds, string naptan)
		{
			ArrayList extendedNaptanIds = new ArrayList(naptanIds);
			extendedNaptanIds.Add(naptan);

			naptanIds = (string[])extendedNaptanIds.ToArray(typeof(string));
		}

		

		/// <summary>
		/// Fetch location information using code gazetteer.
		/// If ambiguity between IATA and CRS, remove IATA, Postcode
		/// </summary>
		/// <param name="locations">locations array inout for storing fetched data</param>
		/// <returns>true if successful, false otherwise</returns>
		private static bool FetchLocationInfo(ref DBSLocation[] locations)
		{
			ITDCodeGazetteer gaz = 	(ITDCodeGazetteer)TDServiceDiscovery.Current[ServiceDiscoveryKey.CodeGazetteer];

			// at this stage the array should always be size 1!
			if (locations.Length != 1)
				return false;

			// check the code field is not empty!
			if (locations[0].Code.Length == 0)
				return false;

			TDCodeDetail[] codes = null;
			try
			{
				codes =  gaz.FindCode(locations[0].Code);
			}
			catch (TDException exc)
			{
				// Logging
				OperationalEvent oe = new OperationalEvent(
					TDEventCategory.Infrastructure,
					TDTraceLevel.Warning,
					string.Format(Messages.FailedCodeGaz, exc));
				Logger.Write(oe);
					
				return false;
			}

			if (codes.Length != 0)
			{

			
				// if bigger than 2, need to sort to make sure we are 
				// dealing with same code types at the same time!
				if (codes.Length >2)
					// Create a new locations resulting from this code service access
					// agregate entries with same code type!
				{
					ArrayList codeList = new ArrayList(codes);
					codeList.Sort(new TDCodeDetailComparer());
					codes = (TDCodeDetail[])codeList.ToArray(typeof(TDCodeDetail));
				}

				ArrayList newLocationList = new ArrayList();
				DBSLocation currentLocation = new DBSLocation();
				currentLocation.Type = codes[0].CodeType;
				for ( int i= 0; i< codes.Length ; i++)
				{
					// Populate currentLocation with info depending on type
					switch (currentLocation.Type)
					{
						case TDCodeType.CRS:
						{
							currentLocation.Code = codes[i].Code;
							string[] naptanIds = currentLocation.NaptanIds; 
							UpdateNaptanIds(ref naptanIds, codes[i].NaptanId);
							currentLocation.NaptanIds = naptanIds;
							if (codes[i].Locality !=null)
								currentLocation.Locality =  codes[i].Locality;

						}
							break;
						case TDCodeType.SMS:
						{
							try
							{
								TDCodeDetail[] tempCodes =  gaz.FindText(locations[0].Code,false, new TDModeType[]{TDModeType.Rail});
								if (tempCodes.Length > 0)
									return false;
							}
							catch (TDException exc)
							{
								// Logging
								OperationalEvent oe = new OperationalEvent(
									TDEventCategory.Infrastructure,
									TDTraceLevel.Warning,
									string.Format(Messages.FailedCodeGaz, exc));
								Logger.Write(oe);
					
								return false;
							}

							currentLocation.Code = codes[i].Code;
							string[] naptanIds = currentLocation.NaptanIds; 
							UpdateNaptanIds(ref naptanIds, codes[i].NaptanId);
							currentLocation.NaptanIds = naptanIds;
							currentLocation.Locality = codes[i].Locality;

			
						}
							break;
						case TDCodeType.IATA:
						{
							currentLocation.Code = codes[i].Code;
						}
							break;
					}

					// if type changes, add old current one to list and create new one
					// Check first the current index is not the last in the array
					if ( i < (codes.Length-1) && codes[i+1].CodeType != currentLocation.Type)
					{
						
						// add current location to list. If not done the last current will always be missed!
						if (!IsRejectedCodeType( currentLocation.Type)) // do it only if type is accepted!
						{
							currentLocation.Valid = true;
							newLocationList.Add(currentLocation);
						}
						currentLocation = new DBSLocation();
						currentLocation.Type = codes[i+1].CodeType;
					}
				}

				// add current location to list. If not done the last current will always be missed!
				if (!IsRejectedCodeType(currentLocation.Type)) // do it only if type is accepted!
				{
					currentLocation.Valid = true;
					newLocationList.Add(currentLocation);
				}

				// finally replace old locations with new ones
				locations = (DBSLocation[])newLocationList.ToArray(typeof(DBSLocation));

				// success if ends up with location(s) in array!
				return (locations.Length != 0);
					
			}
			else
				return false;

			


		}


		#endregion

		#region Public methods	
		public static bool ValidateLocationRequest(ref DBSLocation[] stopLocations, ArrayList errors)
		{
			// before check if array is size 1. Should always come as size 1.
			// if not, exception as it is an internal error!
			if (stopLocations.Length != 1)
			{
				
				Logger.Write( new OperationalEvent(
					TDEventCategory.Business,
					TDTraceLevel.Error,
					Messages.EmptyLocationArray));

				throw new TDException(Messages.EmptyLocationArray, true, TDExceptionIdentifier.DBSEmptyLocationArray);

			}

			// Also check if given location is not null!
			if (stopLocations[0] == null)
			{
				errors.Add(Messages.NullLocationRequest);
				return false;
			}

			// first check if location is valid
			if (stopLocations[0].Valid)
			{
				// if valid, check depending on type if we have everything needed,
				if (!ValidateDBSLocation(stopLocations[0], errors))
				{
					// Check given type is not a rejected one, to avoid making unnessary code service call.
					if (IsRejectedCodeType(stopLocations[0].Type))
					{
						errors.Add(string.Format(Messages.RejectedCodeType, stopLocations[0].Code, stopLocations[0].Type));
						return false;
					}

					// if not everything needed, fetch using code gazetteer,
					if (!FetchLocationInfo(ref stopLocations))
					{
						// if gets here but array empty ==> all found codes were rejected
						if (stopLocations.Length == 0)
						{
							errors.Add(Messages.RejectedCodeTypeInFetching);
						}
						else
						{
							if (stopLocations[0].Code.Length != 0)
							{
								// if returned unsuccessfully, add error
								errors.Add(string.Format(Messages.FailedFetchingInfo, stopLocations[0].Code));
							}
							else
							{
								// if returned unsuccessfully, add error
								errors.Add(Messages.FetchingMissingCode);
							}
						}
						return false;
					}
				}
				return true;
				
			}
			else
			{
				// Here don't check for rejected code, because as the location is not valid
				// we are not supposed to look at the code type! 
				// so Fetch info anyway

				// if invalid, call codegazetteer to fetch needed info then populate locations
				if (!FetchLocationInfo(ref stopLocations))
				{
					// if gets here but array empty ==> all found codes were rejected
					if (stopLocations.Length == 0)
					{
						errors.Add(Messages.RejectedCodeTypeInFetching);
					}
					else
					{
						if (stopLocations[0].Code.Length != 0)
						{
							// if returned unsuccessfully, add error
							errors.Add(string.Format(Messages.FailedFetchingInfo, stopLocations[0].Code));
						}
						else
						{
							// if returned unsuccessfully, add error
							errors.Add(Messages.FetchingMissingCode);
						}
					}
					return false;
				}
				return true;
			}
			
		}


		public static bool ValidateLocationRequest(ref DBSLocation[] stopLocations, ArrayList errors, ref DBSLocation[] locationFilters, ref DBSValidationType validationType)
		{

			bool result = ValidateLocationRequest(ref stopLocations, errors);

			// don't perform further check if error or if optional filter null
			if (result && locationFilters[0] != null)
			{
				// If success, wipe all existing errors off! Not interested anymore!
				errors.Clear();


				// Validate filter location using standard method. 
				// if both locations are valid, check consistency
				if (ValidateLocationRequest(ref locationFilters, errors) )
				{
					string stopCode = stopLocations[0].Code;
					string filterCode = locationFilters[0].Code;
					if (!CheckLocationsConsistency(ref stopLocations, ref locationFilters, errors))
					{
						errors.Add (string.Format(Messages.FailureConsistencyCheck, stopCode, filterCode));
						// Indicating which validation type has failed
						validationType = DBSValidationType.Inconsistent;
						return false;
					}
					return true;
				}
				else
				{	// Indicating which validation type has failed
					validationType = DBSValidationType.Destination;
					return false;
				}
				
			}
			else
			{
				// Checking if origin is valid and no destination
				if (result && (locationFilters[0]==null || locationFilters[0].Code == null || locationFilters[0].Code.Length < 1))
				{
					return result;
				}

				//Validation for Origin has failed now check if destination is valid or not?
				// Modified for Enhanced Exposed Services as some times if there no code set then it tries to fetch the location. So, explicitly checking for code length
				if (ValidateLocationRequest(ref locationFilters, errors) || locationFilters[0]==null || locationFilters[0].Code.Length == 0  )
				{	// In this case, destination code is fine but origin validation failed
					// Indicating which validation type has failed
					validationType = DBSValidationType.Origin;
					return result;
				}				
				else
				{
					// If ddestination fails, then indicate ValidationType -> Both
					// Indicating which validation type has failed
					validationType = DBSValidationType.Both;
					return result;
				}
			}

		}

		/// <summary>
		/// Validate the DBSTimeRequest object. Constraints only if Type = Time.
		/// </summary>
		/// <param name="time">DBSTimeRequest to validate</param>
		/// <param name="errors">errors list array to populate in case of errors</param>
		public static bool ValidateTimeRequest(DBSTimeRequest time, ArrayList errors)
		{
			
			if (time == null)
			{
				errors.Add(Messages.TimeRequestNull);
				return false;
			}
			
			if (time.Type == TimeRequestType.TimeToday || time.Type == TimeRequestType.TimeTomorrow)
			{

				// Checking first for time format
				if (time.Hour < 0 || time.Hour >23 || time.Minute <0 || time.Minute > 59)
				{
					// Add error to list
					errors.Add(Messages.TimeRequestInvalid);
					return false;
				}

				// Checking for if time in past and/or within accepted time window

				// Getting the property
				int timeWindow = 0; // in minutes
				string sTimeWindow = Properties.Current[Keys.PastTimeWindow];
				try
				{
					timeWindow = Convert.ToInt32(sTimeWindow, CultureInfo.InvariantCulture.NumberFormat);
				}
				catch
				{
					timeWindow = 0;
					OperationalEvent oe = new OperationalEvent(
						TDEventCategory.Infrastructure,
						TDTraceLevel.Warning,
						string.Format(Messages.MissingProperty, Keys.PastTimeWindow));
					Logger.Write(oe);
					
				}

				// if type set to TimeToday and hour:minute in past 
				DateTime dtRequested = DateTime.Today;
				dtRequested = dtRequested.AddHours(time.Hour);
				dtRequested = dtRequested.AddMinutes(time.Minute);
				if (time.Type == TimeRequestType.TimeToday
					&& dtRequested < DateTime.Now)
				{
					// test now if time outside past time window ( lower than current time - minutes)
					if ( dtRequested < DateTime.Now.AddMinutes(-timeWindow))
					{
						// then day will be tomorrow
						time.Type = TimeRequestType.TimeTomorrow;
					}
					// Otherwise, it stays set to TimeToday! ==> The request will be in the past for Today!

				}

			}
			
			return true;
	
		}

		#endregion
	}
}
