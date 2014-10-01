// *********************************************** 
// NAME                 : TestFilePublisher.cs 
// AUTHOR               : Kenny Cheung
// DATE CREATED         : 07/07/2003 
// DESCRIPTION  : NUnit test for the
// FilePublisher class
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EventLoggingService/test/TestFilePublisher.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:23:16   mturner
//Initial revision.
//
//   Rev 1.10   Feb 07 2005 09:15:40   RScott
//Assertion changed to Assert
//
//   Rev 1.9   Oct 20 2003 13:34:46   geaton
//Updated expected output following addition of ms by operational event formatter.
//
//   Rev 1.8   Sep 03 2003 10:14:48   geaton
//Added thread safety measures and introduced applicationId into filepath format.
//
//   Rev 1.7   Aug 22 2003 14:55:58   geaton
//Updated expected output after change to default formatter.
//
//   Rev 1.6   Jul 30 2003 18:08:40   geaton
//Changes to OperationalEvent constructors
//
//   Rev 1.5   Jul 30 2003 09:28:22   geaton
//Changed output file directory to 'current' path instead of C: drive root. (This keeps things tidy and avoids clogging up root directory.)
//
//   Rev 1.4   Jul 29 2003 17:31:48   geaton
//Added TestOperationalEvent. Removed referencenumber property from LogEvent and changed OperationalEvent constructors
//
//   Rev 1.3   Jul 25 2003 14:14:50   geaton
//Changes resulting from code review 2003-07-22. (These were mainly concerned with adding comments and running through FXCop).

using System;
using System.IO;
using NUnit.Framework;

namespace TransportDirect.Common.Logging
{
	/// <summary>
	/// NUnit for FilePublisher
	/// </summary>
	[TestFixture]
	public class TestFilePublisher
	{
		[Test]
		public void TestWriteEvent()
		{			
			// --------------------------------------------------
			// set-up a directory for the test files

			string currentTime = DateTime.Now.ToString
				("yyMMddHHmmssff", System.Globalization.CultureInfo.InvariantCulture);
			string root = @".\FilePublisherTests\"; 
			string testDirectoryName = "TestFilePublisher-" + currentTime;

			// make a reference to a directory
			DirectoryInfo di = new DirectoryInfo(root);

			// create a subdirectory in the directory just created
			DirectoryInfo dis = di.CreateSubdirectory(testDirectoryName);

			// check to make sure that directory has been successfully
			// created before continuing
			DirectoryInfo testDirectory =
				new DirectoryInfo(root + testDirectoryName + "\\");
			Assert.AreEqual(true, testDirectory.Exists, "Could not create the test directory!");

			// --------------------------------------------------

			// create 16 operational events and write these to files
			// in the specified directory.

			string identifier = "Identifier123";
			int rotation = 5;
			string baseFilepath = root + testDirectoryName + "\\" + "testfile";

			FilePublisher filePublisher = new FilePublisher
				(identifier, rotation, baseFilepath);

			OperationalEvent[] operationalEvents =
				new OperationalEvent[16];

			operationalEvents[0] = new OperationalEvent(
				TDEventCategory.Database,
				TDTraceLevel.Error,
				"Test Error 1: There was a connection error with Database 123XY",
				"Test Target Number 1", "123");

			operationalEvents[1] = new OperationalEvent(
				TDEventCategory.Business,
				TDTraceLevel.Info,
				"Test Error 2: A problem occurred whilst attempting to load G5265",
				"Test Target Number 2", "456");

			operationalEvents[2] = new OperationalEvent(
				TDEventCategory.ThirdParty,
				TDTraceLevel.Warning,
				"Test Error 3: Connection XYF failed",
				"Test Target Number 3", "789");

			operationalEvents[3] = new OperationalEvent(
				TDEventCategory.ThirdParty,
				TDTraceLevel.Verbose,
				"Test Error 4: Timeout occurred processing H6001.aspx",
				"Test Target Number 4", "012");

			operationalEvents[4] = new OperationalEvent(
				TDEventCategory.ThirdParty,
				TDTraceLevel.Error,
				"Test Error 5: Error Number 1234HDJ has occurred",
				"Test Target Number 5", "345");

			operationalEvents[5] = new OperationalEvent(
				TDEventCategory.Business,
				TDTraceLevel.Warning,
				"Test Error 6: Page Index.aspx failed to load",
				"Test Target Number 6", "678");

			operationalEvents[6] = new OperationalEvent(
				TDEventCategory.ThirdParty,
				TDTraceLevel.Verbose,
				"Test Error 7: Server SG12323 has failed",
				"Test Target Number 7", "901");

			operationalEvents[7] = new OperationalEvent(
				TDEventCategory.ThirdParty,
				TDTraceLevel.Info,
				"Test Error 8: Server SG123 failed to reboot",
				"Test Target Number 8", "234");

			operationalEvents[8] = new OperationalEvent(
				TDEventCategory.ThirdParty,
				TDTraceLevel.Info,
				"Test Error 9: Component C123 malfunction",
				"Test Target Number 9", "567");

			operationalEvents[9] = new OperationalEvent(
				TDEventCategory.Business,
				TDTraceLevel.Warning,
				"Test Error 10: Hard disk failure on SG134",
				"Test Target Number 10", "890");

			operationalEvents[10] = new OperationalEvent(
				TDEventCategory.ThirdParty,
				TDTraceLevel.Warning,
				"Test Error 11: SG123 has failed to initialise",
				"Test Target Number 11", "123");
			
			operationalEvents[11] = new OperationalEvent(
				TDEventCategory.Database,
				TDTraceLevel.Verbose,
				"Test Error 12: Database connection error on 'Journeys'",
				"Test Target Number 12", "456");

			operationalEvents[12] = new OperationalEvent(
				TDEventCategory.Business,
				TDTraceLevel.Info,
				"Test Error 13: Unknown Error",
				"Test Target Number 13", "789");

			operationalEvents[13] = new OperationalEvent(
				TDEventCategory.ThirdParty,
				TDTraceLevel.Info,
				"Test Error 14: Error writing to audit trail",
				"Test Target Number 14", "012");

			operationalEvents[14] = new OperationalEvent(
				TDEventCategory.ThirdParty,
				TDTraceLevel.Warning,
				"Test Error 15: Driver D1 failed initialisation routine",
				"Test Target Number 15");

			operationalEvents[15] = new OperationalEvent(
				TDEventCategory.Database,
				TDTraceLevel.Info,
				"Test Error 16: D123 failed",
				"Test Target Number 16");

			// call the write event procedure to write
			// the events to the file

			foreach( OperationalEvent oe in operationalEvents)
			{
				filePublisher.WriteEvent(oe);
			}

			// create custom events
			CustomEventOne customEventOne = new CustomEventOne
				(TDEventCategory.Business, TDTraceLevel.Warning,
				"A custom event one message", Environment.UserName, 12345);

			CustomEventTwo customEventTwo = new CustomEventTwo
				(TDEventCategory.ThirdParty, TDTraceLevel.Error,
				"A custom event two message", Environment.UserName, 3343);

			// CustomEventOne has a file formatter defined, therefore
			// log event should be written in the format specified
			// in CustomEventOneFileFormatter
			filePublisher.WriteEvent(customEventOne);
			
			// CustomEventTwo has no file formatter defined, therefore
			// file should be written in the format specified
			// by the DefaultFormatter.
			filePublisher.WriteEvent(customEventTwo);

			// --------------------------------------------------

			// recreate the expected lines in the files and store
			// in a string array
			
			string head = "TD-OP";
			string tab = "\t";
			string[] expectedLines = new String[18];
			OperationalEvent ope;

			for(int i=0; i<16; i++)
			{
				ope = operationalEvents[i];

				expectedLines[i] =
					head + tab + ope.Time.ToString("yyyy-MM-ddTHH:mm:ss.fff") + tab + ope.Message + tab +
					ope.Category + tab + ope.Level + tab + ope.MachineName + tab +
					ope.TypeName + tab + ope.MethodName + tab +
					ope.AssemblyName + tab + ope.Target;

				// check to see if session id exists and concat
				// if it does otherwise ignore
				if(ope.SessionId != OperationalEvent.SessionIdUnassigned)
				{
					expectedLines[i] += tab + ope.SessionId;
				}
			}

			// add in the expected lines for the custom events
			
			// customEventOne
			expectedLines[16] = 				
				"TD-CustomEventOne"+ "\t" + 
				customEventOne.Time + "\t" +
				customEventOne.Message + "\t" +
				customEventOne.Category + "\t" +
				customEventOne.Level + "\t" +
				customEventOne.ReferenceNumber;

			expectedLines[17] = String.Format(Messages.DefaultFormatterOutput, customEventTwo.Time, customEventTwo.ClassName);


			// --------------------------------------------------

			// read the files that were created

			FileInfo[] fileInfoArray = testDirectory.GetFiles("*.txt");

			// Expect 4 files to be written
			Assert.AreEqual(4, fileInfoArray.Length, "Number of files created is incorrect.");

			// Open each file in turn, read each line and store in actualResults.
			// Open files in order of time created (default ordering of the array)

			string[] actualLines = new String[18];
			
			// read the first file
			FileInfo tempFile = fileInfoArray[0];
			FileStream fileStream = tempFile.OpenRead();
			StreamReader streamReader = new StreamReader(fileStream);

			for(int i=0; i<5; i++)
			{
				actualLines[i] = streamReader.ReadLine();
			}

			streamReader.Close();
			fileStream.Close();

			// read the second file
			tempFile = fileInfoArray[1];
			fileStream = tempFile.OpenRead();
			streamReader = new StreamReader(fileStream);

			for(int i=5; i<10; i++)
			{
				actualLines[i] = streamReader.ReadLine();
			}

			streamReader.Close();
			fileStream.Close();

			// read the third file
			tempFile = fileInfoArray[2];
			fileStream = tempFile.OpenRead();
			streamReader = new StreamReader(fileStream);

			for(int i=10; i<15; i++)
			{
				actualLines[i] = streamReader.ReadLine();
			}

			streamReader.Close();
			fileStream.Close();

			// read the fourth (final) file
			tempFile = fileInfoArray[3];
			fileStream = tempFile.OpenRead();
			streamReader = new StreamReader(fileStream);

			actualLines[15] = streamReader.ReadLine();
			
			// the two custom events
			actualLines[16] = streamReader.ReadLine();
			actualLines[17] = streamReader.ReadLine();

			// --------------------------------------------------

			// perform the assertions

			for(int i=0; i<18; i++)
			{
				Assert.AreEqual(expectedLines[i], actualLines[i], "Line " + (i+1) + " does not match.");
			}

			//-----------------------------------------------


		}
	}
}
