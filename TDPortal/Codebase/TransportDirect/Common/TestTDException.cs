//************************************************
//NAME			: TestTDException.cs
//AUTHOR		: Gary Eaton
//DATE CREATED	: 4/09/2003
//DESCRIPTION	: NUnit test class for TDException
//************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Common/TestTDException.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:19:08   mturner
//Initial revision.
//
//   Rev 1.7   Feb 07 2005 08:51:00   RScott
//Assertion changed to Assert
//
//   Rev 1.6   Oct 06 2003 19:10:24   geaton
//Changes resulting from intro of TDExceptionIdentifier.
//
//   Rev 1.5   Oct 06 2003 12:02:00   geaton
//Changed TDException constructors to take id of type TDExceptionIdentifier.
//
//   Rev 1.4   Sep 16 2003 17:53:20   jcotton
//PVCS Tracker Test 3
//
//   Rev 1.3   Sep 16 2003 17:49:18   jcotton
//PVCS Tracker test 2
//
//   Rev 1.2   Sep 16 2003 17:46:08   jcotton
//PVCS Tracker test
//
//   Rev 1.1   Sep 16 2003 16:39:04   jcotton
//PVCS Association test
//
//   Rev 1.0   Sep 04 2003 10:04:46   geaton
//Initial Revision

using System;
using NUnit.Framework;

namespace TransportDirect.Common
{
	
	[TestFixture]
	public class TestTDException
	{
		
		[Test]
		public void TestConstructors()
		{
			TDException e1 = new TDException();

			Assert.IsTrue(e1.Identifier == TDExceptionIdentifier.Undefined);
			Assert.IsTrue(e1.Logged == false);
			Assert.IsTrue(e1.Message == String.Empty);
			Console.WriteLine(e1.ToString());

			TDException e2 = new TDException("Message for e2", true, TDExceptionIdentifier.BTCInvalidNumberOfMonths);
			Assert.IsTrue(e2.Message == "Message for e2");
			Assert.IsTrue(e2.Logged == true);
			Assert.IsTrue(e2.Identifier == TDExceptionIdentifier.BTCInvalidNumberOfMonths);
			Console.WriteLine(e2.ToString());

			ApplicationException a1 = new ApplicationException("Message for a1");
			TDException e3 = new TDException("Message for e3", a1, true, TDExceptionIdentifier.BTCInvalidNumberOfMonths);
			Assert.IsTrue(e3.Message == "Message for e3");
			Assert.IsTrue(e3.Logged == true);
			Assert.IsTrue(e3.Identifier == TDExceptionIdentifier.BTCInvalidNumberOfMonths);
			Assert.IsTrue(e3.InnerException.Message == "Message for a1");
			Console.WriteLine(e3.ToString());
		}

		[Test]
		public void TestThrowing()
		{
			try
			{
				throw (new TDException("Message for e", false, TDExceptionIdentifier.BTCInvalidNumberOfMonths));
			}
			catch (TDException e)
			{
				Assert.IsTrue(e.Message == "Message for e");
				Assert.IsTrue(e.Identifier == TDExceptionIdentifier.BTCInvalidNumberOfMonths);
			}
		}
	}
}