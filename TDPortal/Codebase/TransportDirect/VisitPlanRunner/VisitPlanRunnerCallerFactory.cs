// ****************************************************************
// NAME         : VisitPlanRunnerCallerFactory.cs
// AUTHOR       : Tim Mollart
// DATE CREATED : 2005-06-08
// DESCRIPTION  : Factory class for VisitPlanRunnerCaller
// ****************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/VisitPlanRunner/VisitPlanRunnerCallerFactory.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:51:12   mturner
//Initial revision.
//
//   Rev 1.0   Sep 13 2005 12:16:06   tmollart
//Initial revision.

using System;
using System.Runtime.Remoting;
using System.Diagnostics;
using TransportDirect.Common.Logging;
using TransportDirect.Common.ServiceDiscovery;

namespace TransportDirect.UserPortal.VisitPlanRunner
{
	/// <summary>
	/// Factory to create VisitPlanRunnerCaller object.
	/// </summary>
	public class VisitPlanRunnerCallerFactory : IServiceFactory
	{

		#region Constructor

		/// <summary>
		/// Default constructor.
		/// </summary>
		public VisitPlanRunnerCallerFactory()
		{
		}

		#endregion


		#region IServiceFactory Members

		/// <summary>
		/// Returns a new instance of VisitPlanRunnerCaller
		/// </summary>
		/// <returns>A new VisitPlanRunnerCaller</returns>
		public object Get()
		{
			try
			{
				return new VisitPlanRunnerCaller();
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
