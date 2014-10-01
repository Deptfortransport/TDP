// *********************************************** 
// NAME                 : CjpFactory.cs 
// AUTHOR               : Richard Philpott
// DATE CREATED         : 21/07/2003 
// DESCRIPTION			: Mock ServiceDiscovery Factory 
//							for the CJP testing stub.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyControl/Test/TestMockCjpFactory.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:24:16   mturner
//Initial revision.
//
//   Rev 1.4   Jun 24 2004 14:04:30   RPhilpott
//Correct handling of outward/return.
//
//   Rev 1.3   Sep 26 2003 20:59:14   RPhilpott
//CJPManager test harness for non-NUnit testing.
//
//   Rev 1.2   Sep 26 2003 14:04:36   RPhilpott
//Tidy up NUnit testing
//
//   Rev 1.1   Sep 24 2003 17:36:32   RPhilpott
//Make test stub more flexible
//
//   Rev 1.0   Sep 19 2003 15:23:04   RPhilpott
//Initial Revision
//

using System;
using TransportDirect.Common.ServiceDiscovery;

namespace TransportDirect.UserPortal.JourneyControl
{
	/// <summary>
	/// Factory used by Service Discovery to create a CJP Stub.
	/// </summary>
	public class MockCjpFactory : IServiceFactory
	{
		private string propertiesFileName = string.Empty;

		private string fileName = string.Empty;
		private int delay = 0;
	
		private string outFileName = string.Empty;
		private int outDelay = 0;
	
		private string returnFileName = string.Empty;
		private int returnDelay = 0;
	
		private bool outDone = false;
	
		/// <summary>
		/// Constructor.
		/// </summary>
		public MockCjpFactory()
		{
		}

		/// <summary>
		/// Constructor.
		/// </summary>
		public MockCjpFactory(string fileName, int delay)
		{
			this.fileName = fileName;
			this.delay = delay;
		}
		
		/// <summary>
		/// Constructor.
		/// </summary>
		public MockCjpFactory(string outFileName, int outDelay, string returnFileName, int returnDelay)
		{
			this.returnFileName = returnFileName;
			this.returnDelay = returnDelay;

			this.outFileName = outFileName;
			this.outDelay = outDelay;
		}
		
		/// <summary>
		///  Method used by the ServiceDiscovery to get an
		///  instance of an implementation of ICJP
		/// </summary>
		/// <returns>A new instance of a CJP.</returns>
		public Object Get()
		{
			if	(outFileName != string.Empty && !outDone)
			{
				outDone = true;
				return (new CjpStub(outFileName, outDelay));
			}
			else if (returnFileName != string.Empty)
			{
				outDone = false;	// reset ready for next request
				return (new CjpStub(returnFileName, returnDelay));
			}
			else
			{
				return (fileName == string.Empty ? (new CjpStub(propertiesFileName)) : (new CjpStub(fileName, delay)));
			}
		}

		public string PropertiesFileName
		{
			get { return propertiesFileName; }
			set { propertiesFileName = value; }
		}

		public string FileName
		{
			get { return fileName; }
			set { fileName = value; }
		}

		public int Delay
		{
			get { return delay; }
			set { delay = value; }
		}

		public string OutFileName
		{
			get { return outFileName; }
			set { outFileName = value; }
		}

		public int OutDelay
		{
			get { return outDelay; }
			set { outDelay = value; }
		}

		public string ReturnFileName
		{
			get { return returnFileName; }
			set { returnFileName = value; }
		}

		public int ReturnDelay
		{
			get { return returnDelay; }
			set { returnDelay = value; }
		}


	}
}