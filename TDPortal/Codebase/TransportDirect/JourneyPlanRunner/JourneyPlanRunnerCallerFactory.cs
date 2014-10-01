// ****************************************************************
// NAME         : JourneyPlanRunnerCallerFactory.cs
// AUTHOR       : Andrew Sinclair
// DATE CREATED : 2005-06-08
// DESCRIPTION  : 
// ****************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyPlanRunner/JourneyPlanRunnerCallerFactory.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:24:46   mturner
//Initial revision.
//
//   Rev 1.2   Jul 04 2005 15:36:58   asinclair
//Added Comments
//Resolution for 2557: DEL 8 Stream: CJP Architecture Changes
//
//   Rev 1.1   Jun 17 2005 09:49:04   jgeorge
//Added additional logging
//Resolution for 2557: DEL 8 Stream: CJP Architecture Changes
//
//   Rev 1.0   Jun 15 2005 14:20:54   asinclair
//Initial revision.

using System;
using System.Runtime.Remoting;
using System.Diagnostics;
using TransportDirect.Common.Logging;
using TransportDirect.Common.ServiceDiscovery;

namespace TransportDirect.UserPortal.JourneyPlanRunner
{
	/// <summary>
	/// Summary description for JourneyPlanRunnerCallerFactory.
	/// </summary>
	public class JourneyPlanRunnerCallerFactory : IServiceFactory
	{
		
		/// <summary>
		/// Constructor - nothing to do.
		/// </summary>
		public JourneyPlanRunnerCallerFactory()
		{
		}

		#region IServiceFactory Members


		/// <summary>
		///  This enables JourneyPlanRunnerCaller to 
		///  be used with Service Discovery, and returns
		///  a new instance of JourneyPlanRunnerCaller
		/// </summary>
		/// <returns>A new instance of JourneyPlanRunnerCaller</returns>
		public object Get()
		{
			try
			{
				return new JourneyPlanRunnerCaller();
			}
			catch (RemotingException e)
			{
				Trace.Write( new OperationalEvent( TDEventCategory.Infrastructure, TDTraceLevel.Error, e.Message, e));
			}
			catch (Exception e)
			{
				Trace.Write( new OperationalEvent( TDEventCategory.Infrastructure, TDTraceLevel.Error, e.Message, e));
			}
			return null;
		}

		#endregion
	}
}
