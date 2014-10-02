// *********************************************** 
// NAME			: LocationInformationFactory.cs
// AUTHOR		: Mitesh Modi
// DATE CREATED	: 18/10/07
// DESCRIPTION	: Class to provide location information for location info page
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/LocationInformationService/LocationInformationFactory.cs-arc  $
//
//   Rev 1.0   Nov 28 2007 14:56:42   mturner
//Initial revision.
//
//   Rev 1.0   Oct 25 2007 15:39:10   mmodi
//Initial revision.
//Resolution for 4518: Del 9.8 - Air Departure Boards
//

using System;
using TransportDirect.Common.ServiceDiscovery;

namespace TransportDirect.UserPortal.LocationInformationService
{
	/// <summary>
	/// Summary description for LocationInformationFactory.
	/// </summary>
	public class LocationInformationFactory : IServiceFactory
	{
		private LocationInformationCatalogue current;

		#region Implementation of IServiceFactory
		/// <summary>
		/// Standard constructor. Initialises the LocationInformationCatalogue.
		/// </summary>
		public LocationInformationFactory()
		{
			current = new LocationInformationCatalogue();
		}

		/// <summary>
		/// Returns the current LocationInformation object
		/// </summary>
		/// <returns></returns>
		public object Get()
		{
			return current;
		}

		#endregion
	}
}
