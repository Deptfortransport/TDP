// *********************************************** 
// NAME                 : TestMockMatchingCjpFactory.cs 
// AUTHOR               : Jonathan George
// DATE CREATED         : 06/04/05
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyControl/TestStub/TestMockMatchingCjpFactory.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:24:26   mturner
//Initial revision.
//
//   Rev 1.0   Apr 06 2005 15:23:02   jgeorge
//Initial revision.

using System;
using TransportDirect.Common.ServiceDiscovery;

namespace TransportDirect.UserPortal.JourneyControl
{
	/// <summary>
	/// Cjp factory which initialises the CJP stub in "matching" mode,
	/// where it will attempt to match requests to results. Properties
	/// file must specify either the data files to use or a folder to use.
	/// </summary>
	public class TestMockMatchingCjpFactory : IServiceFactory
	{
		private static object cjpStub;

		public TestMockMatchingCjpFactory(string propertiesFileName)
		{
			cjpStub = new CjpStub(propertiesFileName, true);
		}

		public Object Get()
		{
			return cjpStub;
		}
	}
}
