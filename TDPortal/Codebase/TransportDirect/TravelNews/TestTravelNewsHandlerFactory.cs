// *********************************************** 
// NAME                 : TestTravelNewsHandlerFactory.cs 
// AUTHOR               : Murat Guney
// DATE CREATED         : 17/02/2006
// DESCRIPTION  : Class for creating TestStubTravelNewsHandler.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/TravelNews/TestTravelNewsHandlerFactory.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:50:26   mturner
//Initial revision.
//
//   Rev 1.0   Feb 21 2006 10:02:24   mguney
//Initial revision.

using System;
using TransportDirect.Common.ServiceDiscovery;

namespace TransportDirect.UserPortal.TravelNews
{
	/// <summary>
	/// Summary description for TestTravelNewsHandlerFactory.
	/// </summary>
	public class TestTravelNewsHandlerFactory : IServiceFactory
	{
		private ITravelNewsHandler current;

		/// <summary>
		/// Constructor.
		/// </summary>
		public TestTravelNewsHandlerFactory()
		{
			current = new TestStubTravelNewsHandler();			
		}

		/// <summary>
		/// Method used by the ServiceDiscovery to get the instance of the TravelNewsHandler.
		/// </summary>
		/// <returns>The current instance of the TravelNewsHandler.</returns>
		public Object Get()
		{
			return current;
		}
	}
}
