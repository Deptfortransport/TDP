// *********************************************** 
// NAME                 : TestMockDataServicesFactory.cs
// AUTHOR               : Jonathan George
// DATE CREATED         : 31/03/2004 
// DESCRIPTION  : Factory class for the TestMockDataServices component. 
// ************************************************ 

using System;
using System.Resources;
using TransportDirect.Common;
using TransportDirect.Common.ServiceDiscovery;


namespace TransportDirect.UserPortal.DataServices
{
	/// <summary>
	/// Factory class for the DataServices component
	/// </summary>
	public class TestMockDataServicesFactory : IServiceFactory
	{
		TestMockDataServices current;
		public TestMockDataServicesFactory()
		{
			current = new TestMockDataServices();
		}

		public object Get()
		{
			return current;
		}
	}
}
