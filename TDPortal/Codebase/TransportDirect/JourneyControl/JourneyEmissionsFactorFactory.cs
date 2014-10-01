// *********************************************** 
// NAME			: JourneyEmissionsFactorFactory.cs
// AUTHOR		: Mitesh Modi
// DATE CREATED	: 08/02/2007
// DESCRIPTION	: Implementation of IServiceFactory for the JourneyEmissionsFactor
// so that it can be used with TDServiceDiscovery
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyControl/JourneyEmissionsFactorFactory.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:23:46   mturner
//Initial revision.
//
//   Rev 1.0   Feb 20 2007 17:09:06   mmodi
//Initial revision.
//Resolution for 4350: CO2 Public Transport
//

using System;
using TransportDirect.Common.ServiceDiscovery;

namespace TransportDirect.UserPortal.JourneyControl
{
	/// <summary>
	/// Implementation of IServiceFactory for JourneyEmissionsFactorFactory.
	/// </summary>
	public class JourneyEmissionsFactorFactory : IServiceFactory
	{
		private JourneyEmissionsFactor current;

		#region Implementation of IServiceFactory

		/// <summary>
		/// Standard constructor. Initialises the JourneyEmissionsFactor.
		/// </summary>
		public JourneyEmissionsFactorFactory()
		{
			current = new JourneyEmissionsFactor();
		}

		/// <summary>
		/// Returns the current JourneyEmissionsFactor object
		/// </summary>
		/// <returns></returns>
		public object Get()
		{
			return current;
		}

		#endregion
	}
}
