// ****************************************************************
// NAME 		: TestSessionManagerInitialisation.cs
// AUTHOR 		: Atos Origin
// DATE CREATED : 05/11/2003
// DESCRIPTION 	: A mock testing object that basically does nothing
//				  so that the methods can be called that call the
//				  visitplanrunnercaller without the overhead of
//				  invoking the CJP etc.
// ****************************************************************
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/VisitPlanRunner/test/TestMockVisitPlanRunnerCaller.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:51:16   mturner
//Initial revision.
//
//   Rev 1.0   Oct 10 2005 17:54:12   tmollart
//Initial revision.
//Resolution for 2638: DEL 8 Stream: Visit Planner

using System;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.Common.ServiceDiscovery;

namespace TransportDirect.UserPortal.VisitPlanRunner
{
	/// <summary>
	/// Mock class. No implementation.
	/// </summary>
	public class TestMockVisitPlanRunnerCaller : IServiceFactory, IVisitPlanRunnerCaller
	{

		/// <summary>
		/// Standard constructor.
		/// </summary>
		public TestMockVisitPlanRunnerCaller()
		{
		}


		/// <summary>
		/// Mock method - does nothing.
		/// </summary>
		/// <param name="sessionInfo"></param>
		/// <param name="currentPartition"></param>
		public void RunInitialItinerary(CJPSessionInfo sessionInfo, TDSessionPartition currentPartition)
		{
		}


		/// <summary>
		/// Mock method - does nothing.
		/// </summary>
		/// <param name="sessionInfo"></param>
		/// <param name="currentPartition"></param>
		public void RunAddJourneys(CJPSessionInfo sessionInfo, TDSessionPartition currentPartition)
		{
		}


		/// <summary>
		/// Interface member. Returns current object.
		/// </summary>
		/// <returns></returns>
		public object Get()
		{
			return this;
		}

	}
}
