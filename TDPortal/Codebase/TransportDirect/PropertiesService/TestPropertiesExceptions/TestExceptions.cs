// *********************************************** 
// NAME                 : TestException.cs
// AUTHOR               : Patrick ASSUIED
// DATE CREATED         : 17/07/2003 
// DESCRIPTION  : Test for the throwing of the Properties Exceptions
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PropertiesService/TestPropertiesExceptions/TestExceptions.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:37:58   mturner
//Initial revision.
//
//   Rev 1.5   Feb 08 2005 11:52:20   RScott
//Assertions changed to Asserts
//
//   Rev 1.4   Oct 03 2003 14:35:40   PNorell
//Updated for exception identifier.
//
//   Rev 1.3   Jul 23 2003 10:23:04   passuied
//Changes after PropertyService namespaces / dll renaming
//
//   Rev 1.2   Jul 18 2003 11:00:10   passuied
//test updated
//
//   Rev 1.1   Jul 17 2003 17:24:08   passuied
//addition of header

using System;
using TransportDirect.Common.PropertyService;
using TransportDirect.Common.PropertyService.DatabasePropertyProvider;
using TransportDirect.Common.PropertyService.FilePropertyProvider;
using NUnit.Framework;
using TransportDirect.Common;
using System.Configuration;
namespace TestPropertiesExceptions
{
	/// <summary>
	/// Test for the throwing of the Properties Exceptions
	/// </summary>
	[TestFixture]
	public class TestExceptions
	{
		public TestExceptions()
		{
			
		}

		[Test]
		public void TestDBException()
		{
			bool exceptionThrown = false;
			try
			{
				DatabasePropertyProvider dbProvider = new DatabasePropertyProvider();
		
				dbProvider.Load();
			}
			catch (TDException e)
			{
				Assert.AreEqual(TDExceptionIdentifier.PSDatabaseFailure, e.Identifier,
					"Exception ID is unexpected");
				exceptionThrown = true;

			}
			Assert.AreEqual(true, exceptionThrown,
				"Exception has not been thrown");
		}

		[Test]
		public void TestIsNewVersionDBException()
		{
			bool exceptionThrown = false;
			try
			{
				DatabasePropertyProvider dbProvider = new DatabasePropertyProvider();
		
				bool result = dbProvider.IsNewVersion;
			}
			catch (TDException e)
			{
				Assert.AreEqual(TDExceptionIdentifier.PSInvalidVersion, e.Identifier,
					"Exception ID is unexpected");
				exceptionThrown = true;

			}
			Assert.AreEqual(true, exceptionThrown,
				"Exception has not been thrown");
		}

		[Test]
		public void TestIsNewVersionXmlException()
		{
			bool exceptionThrown = false;
			try
			{
				FilePropertyProvider provider = new FilePropertyProvider();
		
				bool result = provider.IsNewVersion;
			}	
			catch (TDException e)
			{
				Assert.AreEqual(TDExceptionIdentifier.PSInvalidVersion, e.Identifier,
					"Exception ID is unexpected");
				exceptionThrown = true;

			}
			Assert.AreEqual(true, exceptionThrown,
				"Exception has not been thrown");
		}


		[Test]
		public void TestXmlException()
		{
			bool exceptionThrown = false;
			try
			{
				FilePropertyProvider provider = new FilePropertyProvider();
				// No need to do the test if file is valid since no exception will be thrown
                if (ConfigurationManager.AppSettings["propertyservice.providers.fileprovider.filepath"] == string.Empty)
					provider.Load();
				else
					return;
			}
			catch (TDException e)
			{
				Assert.AreEqual(TDExceptionIdentifier.PSInvalidPropertyFile, e.Identifier,
					"Exception ID is unexpected");
				exceptionThrown = true;

			}
			Assert.AreEqual(true, exceptionThrown,
				"Exception has not been thrown");

		}
	}
}
