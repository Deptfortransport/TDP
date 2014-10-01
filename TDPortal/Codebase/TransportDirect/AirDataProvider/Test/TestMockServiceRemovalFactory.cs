// *********************************************** 
// NAME			: TestMockServiceRemovalFactory.cs
// AUTHOR		: Jonathan George
// DATE CREATED	: 04/06/04
// DESCRIPTION	: IService factory than can be used for testing purposes to simulate 
// removing an item from service discovery. Because TDServiceDiscovery.Init can only
// be called once, there is no way of removing items once this has been done. This
// factory class can be used to replace (using SetServiceForTest) any existing key
// and will raise the same error as would be raised by trying to access a non
// existant service.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/AirDataProvider/Test/TestMockServiceRemovalFactory.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:18:46   mturner
//Initial revision.
//
//   Rev 1.0   Jun 17 2004 09:40:28   jgeorge
//Initial revision.

using System;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common;

namespace TransportDirect.UserPortal.AirDataProvider
{
	/// <summary>
	/// Summary description for TestMockServiceRemovalFactory.
	/// </summary>
	public class TestMockServiceRemovalFactory : IServiceFactory
	{
		private string key;
		public TestMockServiceRemovalFactory(string key)
		{
			this.key = key;
		}

		public object Get()
		{
			throw new TDException("Exception trying to access the ServiceDiscovery at a given key. Key ["+key+"] does not exist", false, TDExceptionIdentifier.SDInvalidKey);
		}
	}
}
