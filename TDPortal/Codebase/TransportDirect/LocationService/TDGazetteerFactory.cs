// *********************************************** 
// NAME                 : TDGazetteerFactory.cs
// AUTHOR               : Patrick ASSUIED
// DATE CREATED         : 29/08/2003 
// DESCRIPTION  : Factory class in charge of creating the appropriate gazetteer. Implements ITDGazetteerFactory
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/LocationService/TDGazetteerFactory.cs-arc  $ 
//
//   Rev 1.2   Feb 11 2010 14:30:16   rbroddle
//Minor corrections
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.1   Feb 10 2010 17:00:00   RBroddle
//Changed to return an InternationalPlaceGazetteer if search is international
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.0   Nov 08 2007 12:25:22   mturner
//Initial revision.
//
//   Rev 1.8   Nov 03 2004 10:03:02   jgeorge
//Changed DatabaseGazetteer to ImportantPlaceGazetteer
//
//   Rev 1.7   Nov 01 2004 15:41:00   jgeorge
//Added code to create DatabaseGazetteer for City searches
//
//   Rev 1.6   Jul 22 2004 10:40:46   RPhilpott
//Remove redundant "text" parameter.
//
//   Rev 1.5   Jul 12 2004 18:56:52   JHaydock
//DEL 5.4.7 Merge: IR 1089
//
//   Rev 1.4   Jul 09 2004 13:09:18   passuied
//Changes for FindStation del 6.1 back end
//
//   Rev 1.3   Apr 27 2004 13:44:38   jbroome
//DEL 5.4 Merge. 
//Partial Postcode searching
//
//   Rev 1.2   Sep 22 2003 17:31:30   passuied
//made all objects serializable
//
//   Rev 1.1   Sep 09 2003 17:23:58   passuied
//Clean up all files to pass code review!
//
//   Rev 1.0   Sep 05 2003 15:30:42   passuied
//Initial Revision

using System;
using TransportDirect.Common;
using TransportDirect.Common.ServiceDiscovery;

namespace TransportDirect.UserPortal.LocationService
{
	/// <summary>
	/// Factory class in charge of creating the appropriate gazetteer. Implements ITDGazetteerFactory
	/// </summary>
	[Serializable()]
	public class TDGazetteerFactory : ITDGazetteerFactory, IServiceFactory
	{
		public TDGazetteerFactory()
		{
		}

		/// <summary>
		/// Evaluates the type of the Input text and returns the appropriate gazetteer.
		/// </summary>
		/// <param name="type">Search type</param>
		/// <param name="sessionID">session ID</param>
		/// <param name="userLoggedOn">indicates if user is logged on</param>
		/// <returns>Returns a Gazetteer object</returns>
		public ITDGazetteer Gazetteer (SearchType type, string sessionID, bool userLoggedOn, StationType stationType)
		{
			switch ( type )
			{
				case SearchType.AddressPostCode :
					return new AddressPostcodeGazetteer(sessionID, userLoggedOn);
				case SearchType.AllStationStops :
					return new AllStationsGazetteer(sessionID, userLoggedOn);
				case SearchType.Locality :
					return new LocalityGazetteer(sessionID, userLoggedOn);
				case SearchType.MainStationAirport:
					return new MajorStationsGazetteer(sessionID, userLoggedOn, stationType);
				case SearchType.POI :
					return new AttractionsGazetteer(sessionID, userLoggedOn);
				case SearchType.City :
					return new ImportantPlaceGazetteer(sessionID, userLoggedOn, PlaceType.City);
                case SearchType.International :
                    //Get the current International Place Gazetteer
                    return (InternationalPlaceGazetteer)TDServiceDiscovery.Current[ServiceDiscoveryKey.InternationalPlaceGazetteer];
				default :
					return new AddressPostcodeGazetteer(sessionID, userLoggedOn); // arbitrary choice... To be defined (cannot happen)
			}
		}

		/// <summary>
		/// Implements the TDGazetteer Factory Get() method.Returns itself
		/// </summary>
		/// <returns></returns>
		public object Get()
		{
			return this;
		}
	}
}
