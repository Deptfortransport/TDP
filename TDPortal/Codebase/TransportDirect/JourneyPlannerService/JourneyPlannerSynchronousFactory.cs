// ***********************************************
// NAME 		: JourneyPlannerSynchronousFactory.cs
// AUTHOR 		: Manuel Dambrine
// DATE CREATED : 03/01/2006
// DESCRIPTION 	: This class provides a single Get method that creates a new 
//				  instance of the JournerPlannerSynchronous class for each call.
// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyPlannerService/JourneyPlannerSynchronousFactory.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:24:32   mturner
//Initial revision.
//
//   Rev 1.2   Jan 23 2006 14:05:38   mdambrine
//Ncover changes
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.1   Jan 13 2006 18:29:12   mdambrine
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
using TransportDirect.UserPortal.DataServices;
using Logger = System.Diagnostics.Trace;
using System.Runtime.Remoting;
using System.Diagnostics;

namespace TransportDirect.UserPortal.JourneyPlannerService
{

	/// <summary>
	/// This class provides a single Get method that creates a new instance 
	/// of the JournerPlannerSynchronous class for each call
	/// </summary>
	public class JourneyPlannerSynchronousFactory	: IServiceFactory
	{		
		/// <summary>
		/// Constructor - nothing to do.
		/// </summary>
		public JourneyPlannerSynchronousFactory()
		{
		}

		#region IServiceFactory Members


		/// <summary>
		///  This enables JourneyPlanRunnerCaller to 
		///  be used with Service Discovery, and returns
		///  a new instance of JourneyPlannerSynchronous
		/// </summary>
		/// <returns>A new instance of JourneyPlannerSynchronous</returns>
		public object Get()
		{		
			return new JourneyPlannerSynchronous();						
		}

		#endregion
	}

}

