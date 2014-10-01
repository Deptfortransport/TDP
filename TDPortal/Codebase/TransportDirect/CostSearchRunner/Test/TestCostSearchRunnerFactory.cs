// ************************************************************** 
// NAME			: TestCostSearchRunnerFactory.cs
// AUTHOR		: C.M. Owczarek
// DATE CREATED	: 11/03/2005 
// DESCRIPTION	: Factory class that returns a test cost search runner object.
// ************************************************************** 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/CostSearchRunner/Test/TestCostSearchRunnerFactory.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:19:42   mturner
//Initial revision.
//
//   Rev 1.1   Mar 15 2005 18:29:24   jmorrissey
//Now returns new MockCostSearchRunner instance each time rather than a singleton instance

using System;
using TransportDirect.Common.ServiceDiscovery;

namespace TransportDirect.UserPortal.CostSearchRunner
{
	/// <summary>
    /// Factory class that returns a test cost search runner object.
	/// </summary>
	[Serializable()]
	public class TestCostSearchRunnerFactory : IServiceFactory
	{
		/// <summary>
		/// Constructor 
		/// </summary>
		public TestCostSearchRunnerFactory()
		{
			
		}				

		/// <summary>
		///  Method used by ServiceDiscovery to get an instance of the TestMockCostSearchRunner class.
		/// </summary>
		/// <returns>A new instance of a TestMockCostSearchRunner.</returns>
		public Object Get()
		{
			return new TestMockCostSearchRunner();
		}
	}
}
