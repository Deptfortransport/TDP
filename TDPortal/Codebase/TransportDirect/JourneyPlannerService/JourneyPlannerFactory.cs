// ***********************************************
// NAME 		: JourneyPlannerFactory.cs
// AUTHOR 		: Manuel Dambrine
// DATE CREATED : 03/01/2006
// DESCRIPTION 	: This class provides a single Get method that creates a 
//				  new instance of the JournerPlannerSynchronous class for each call
// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyPlannerService/JourneyPlannerFactory.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:24:30   mturner
//Initial revision.
//
//   Rev 1.2   Jan 23 2006 14:05:36   mdambrine
//Ncover changes
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.1   Jan 13 2006 18:29:14   mdambrine
//In development
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.0   Jan 11 2006 13:39:04   mdambrine
//Initial revision.
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103

using System;
using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.ServiceDiscovery;
using System.Runtime.Remoting;
using System.Diagnostics;

namespace TransportDirect.UserPortal.JourneyPlannerService
{

	/// <summary>
	/// This class provides a single Get method that creates a new instance 
	/// of the JournerPlanner class for each call
	/// </summary>
	public class JourneyPlannerFactory	: IServiceFactory
	{					

		#region constructor
		/// <summary>
		/// Constructor.
		/// </summary>
		public JourneyPlannerFactory()
		{			
		}
		#endregion

		#region IServiceFactory Members
		/// <summary>
		/// Method used by the ServiceDiscovery to get the instance of the Journeyplanner.
		/// </summary>
		/// <returns>The current instance of the Journeyplanner.</returns>
		public Object Get()
		{			
			return new JourneyPlanner();			
		}	
		#endregion

	}

}
