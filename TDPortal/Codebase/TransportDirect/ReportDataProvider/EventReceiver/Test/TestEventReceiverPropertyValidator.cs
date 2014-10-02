// *********************************************** 
// NAME                 : TestEventReceiverPropertyValidator.cs 
// AUTHOR               : Jatinder S. Toor
// DATE CREATED         : 18/08/2003 
// DESCRIPTION  :  Implements the tests for the EventReceiverPropertyValidator class.
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/EventReceiver/TestEventReceiverPropertyValidator.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:38:38   mturner
//Initial revision.
//
//   Rev 1.3   Feb 07 2005 10:05:24   RScott
//Assertion changed to Assert
//
//   Rev 1.2   Nov 06 2003 19:54:26   geaton
//Removed redundant key.
//
//   Rev 1.1   Sep 05 2003 09:49:32   jtoor
//Changes made to comply with Code Review.
//
//   Rev 1.0   Aug 22 2003 11:49:46   jtoor
//Initial Revision

using System;
using System.Collections;
using System.Messaging;

using TransportDirect.Common;

using Logging = TransportDirect.Common.Logging;
using TransportDirect.Common.PropertyService.Properties;

namespace TransportDirect.ReportDataProvider.EventReceiver
{
	using NUnit.Framework;

	/// <summary>
	/// Tests the EventReceiverPropertyValidator
	/// class. Sets up mock properties to test against the
	/// validation code.
	/// </summary>
	[TestFixture]
	public class TestEventReceiverPropertyValidator
	{
		/// <summary>
		/// Checks that when the validation class is supplied with valid properties,
		/// it will process them correctly.
		/// </summary>
		[Test]
		public void TestGoodProperties()
		{
			MockPropertiesGoodProperties properties  = new MockPropertiesGoodProperties();			
			EventReceiverPropertyValidator validator = new EventReceiverPropertyValidator( properties );

			ArrayList errors = new ArrayList();

			string sQueue = properties["Receiver.Queue.1.Path"];
			
			if( !MessageQueue.Exists( sQueue ) )
				MessageQueue.Create( sQueue );

			try
			{
				Assert.IsTrue(validator.ValidateProperty( Keys.ReceiverQueue, errors ),"validation failed");
			}
			catch
			{
				throw;
			}
			finally
			{				
				MessageQueue.Delete( sQueue );
			}
			
		}
		

		/// <summary>
		/// Tests that the validation class can handle a properties object with no message queue specified.
		/// </summary>
		[Test]
		public void TestGoodPropertiesWithNoQueue()
		{
			MockPropertiesGoodProperties properties  = new MockPropertiesGoodProperties();			
			EventReceiverPropertyValidator validator = new EventReceiverPropertyValidator( properties );

			ArrayList errors = new ArrayList();			

			Assert.IsTrue(!validator.ValidateProperty( Keys.ReceiverQueue, errors ),"validation failed");	
		}

	}
}
