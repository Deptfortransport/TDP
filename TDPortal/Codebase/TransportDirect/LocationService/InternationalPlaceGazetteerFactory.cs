// *********************************************** 
// NAME                 : InternationalPlaceGazetteerFactory
// AUTHOR               : Rich Broddle
// DATE CREATED         : 11/02/2010
// DESCRIPTION  : Implementation of IServiceFactory for the InternationalPlaceGazetteer
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/LocationService/InternationalPlaceGazetteerFactory.cs-arc  $ 
//
//   Rev 1.0   Feb 15 2010 12:02:16   rbroddle
//Initial revision.
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//

using System;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.DatabaseInfrastructure;

namespace TransportDirect.UserPortal.LocationService
{
	/// <summary>
	/// Implementation of IServiceFactory for the InternationalPlaceGazetteer. Creates the object using
	/// the Database data loader.
	/// </summary>
	public class InternationalPlaceGazetteerFactory : IServiceFactory
	{
		private InternationalPlaceGazetteer current;

		/// <summary>
		/// Constructor
		/// </summary>
		public InternationalPlaceGazetteerFactory()
		{
			current = new InternationalPlaceGazetteer(string.Empty , false);
		}
		
		/// <summary>
		/// Returns the current InternationalPlaceGazetteer object
		/// </summary>
		/// <returns></returns>
		public object Get()
		{
			return current;
		}
	}
}
