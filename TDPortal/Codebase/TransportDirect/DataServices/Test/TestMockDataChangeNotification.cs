// ***********************************************
// NAME 		: TestMockDataChangeNotificationService.cs
// AUTHOR 		: Jonathan George
// DATE CREATED : 10-Jun-2004
// DESCRIPTION 	: Mock DataChangeNotificationService object
// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/DataServices/Test/TestMockDataChangeNotification.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:20:58   mturner
//Initial revision.
//
//   Rev 1.1   Jun 16 2004 17:52:46   jgeorge
//Corrected namespace
//
//   Rev 1.0   Jun 16 2004 17:51:32   jgeorge
//Initial revision.


using System;
using TransportDirect.Common.ServiceDiscovery;

namespace TransportDirect.UserPortal.DataServices
{
	/// <summary>
	/// Summary description for TestMockDataChangeNotification.
	/// </summary>
	public class TestMockDataChangeNotification : IDataChangeNotification, IServiceFactory
	{
		private static TestMockDataChangeNotification current;

		public event ChangedEventHandler Changed;

		public TestMockDataChangeNotification()
		{
		}

		public object Get()
		{
			if (current == null)
				current = new TestMockDataChangeNotification();			
			return current;
		}

		public void RaiseChangedEvent(string forGroup)
		{
			if (Changed != null)
				Changed(this, new ChangedEventArgs(forGroup));
		}

	}
}
