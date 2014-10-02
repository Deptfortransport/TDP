// ***********************************************
// NAME 		: TestScriptRepository.cs
// AUTHOR 		: James Broome
// DATE CREATED : 11-May-2004
// DESCRIPTION 	: Test Script Repository.
// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ScriptRepository/TestScriptRepository.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:47:58   mturner
//Initial revision.
//
//   Rev 1.4   Nov 03 2005 17:06:46   kjosling
//Automatically merged from branch for stream2816
//
//   Rev 1.3.1.0   Oct 19 2005 13:55:00   rhopkins
//Standardise text case used for default DOM ID to be uppercase
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages
//
//   Rev 1.3   Feb 08 2005 10:22:52   RScott
//Assertion changed to assert
//
//   Rev 1.2   Jan 10 2005 11:36:54   RScott
//Updated paths from DEL5 to CodeBase
//
//   Rev 1.1   May 19 2004 12:14:18   jbroome
//Added TestAddTempScript test method
//
//   Rev 1.0   May 12 2004 16:30:08   jbroome
//Initial revision.

using System;
using System.Collections;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Web.UI.WebControls;
using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.PropertyService.Properties;

using NUnit.Framework;

using Logger = System.Diagnostics.Trace;


namespace TransportDirect.UserPortal.ScriptRepository
{

	#region Initialization Classes
	public class TestInitialization : IServiceInitialisation
	{
		public void Populate(Hashtable serviceCache)
		{
			// Enable PropertyService
			serviceCache.Add (ServiceDiscoveryKey.PropertyService, new PropertyServiceFactory());

			//Enable the Script Repository Service
			string applicationRoot = "/Web";
			string filePath = @"C:\TDPortal\CodeBase\TransportDirect\ScriptRepository\bin\Debug\" + Properties.Current["TransportDirect.UserPortal.ScriptRepository.ScriptsFile"];
			serviceCache.Add(ServiceDiscoveryKey.ScriptRepository, new ScriptRepositoryFactory(applicationRoot, filePath));

			try
			{
				ArrayList errors = new ArrayList();
				IEventPublisher[] customPublishers = new IEventPublisher[0];
				Trace.Listeners.Add( new TDTraceListener(Properties.Current, customPublishers, errors ));
			}
			catch (TDException tdEx)
			{
				OperationalEvent oe =
					new OperationalEvent( TDEventCategory.Business, TDTraceLevel.Error, (tdEx.Identifier) + tdEx.Message );
				Logger.Write( oe );
				throw tdEx;
			}
		}
	}
	#endregion
	
	/// <summary>
	/// Summary description for TestScriptRepository.
	/// </summary>
	[TestFixture]
	public class TestScriptRepository
	{
		
		#region Constructor, Init & Teardown
		/// <summary>
		/// Constructor, not used.
		/// </summary>
		public TestScriptRepository()
		{
		}

		/// <summary>
		/// Initialise Service Discovery, Property and Event Logging Service.
		/// </summary>
		[SetUp]
		public void Init()
		{
			TDServiceDiscovery.Init(new TestInitialization());
		}

		[TearDown]
		public void CleanUp()
		{

		}
		#endregion

		[Test]
		public void TestGetScript()
		{
			// Create ScriptRepository
			ScriptRepository sr = (ScriptRepository)TDServiceDiscovery.Current[ServiceDiscoveryKey.ScriptRepository];
			// Check Static Members
			Assert.AreEqual ("JavaScriptDetection", sr.DetectionScriptName, "Test Failed");

			// Check Properties
			Assert.IsTrue (sr.DetectionScript.IndexOf("Web/DetectJavaScript.js", 1)!= 0, "Test Failed");

			// Check GetScript method

			// 1. Valid ScriptName, Valid DOM - script exists
			Assert.IsTrue (sr.GetScript("TestControl", "IE4_Style").IndexOf ("Web/testcontrol_ie4style.js")!=0,
				"Test Failed - could not get reference to valid script name.");

			// 2. Valid ScriptName, Invalid DOM - default DOM is used
			Assert.IsTrue (sr.GetScript("TestControl2", "W3C_STYLE").IndexOf ("Web/testcontrol2_w3cstyle.js")!=0,
				"Test Failed - could not get reference to script using default DOM style.");

			// 3. Invalid ScriptName
			// Attempt to get a reference to an invalid script name
			bool thrown = false;
			try 
			{
				string s = sr.GetScript("InvalidScriptName", "W3C_STYLE");
			}
			catch (TDException)
			{
				// Correctly throws exception (because the script name does not exist)
				thrown = true;
			}
		
			Assert.AreEqual(true, thrown, "Exception was not thrown on attempt to obtain a reference to an invalid script name.");

		}

		[Test]
		public void TestAddTempScript()
		{
			// Create ScriptRepository
			ScriptRepository sr = (ScriptRepository)TDServiceDiscovery.Current[ServiceDiscoveryKey.ScriptRepository];
			// Check Static Members
			Assert.AreEqual ("JavaScriptDetection", sr.DetectionScriptName, "Test Failed");

			// Check Properties
			Assert.IsTrue (sr.DetectionScript.IndexOf("Web/DetectJavaScript.js", 1)!= 0,"Test Failed");

			string test = string.Empty;
			string tempDir = @"C:\TDPortal\CodeBase\TransportDirect\ScriptRepository\bin\Debug\TempScripts\";
			
			// Create dummy JavaScript File with string builder
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			System.IO.StringWriter sw = new System.IO.StringWriter(sb);
			sw.WriteLine("alert('"+ DateTime.Now.ToString() +"');");
			sw.Close();

			// Check AddTempScript methods

			//1. Check that temp script file is being created
			test = sr.AddTempScript("TestAddControl", "W3C_STYLE", sb);
			Assert.IsTrue (System.IO.File.Exists(tempDir + @"TestAddControl_W3C_STYLE.js"),
				"Test Failed - Could not create temp script file.");
			
			//2. Check that temp script file is overwritten if present
			test = sr.AddTempScript("TestAddControl", "W3C_STYLE", sb);
			Assert.IsTrue (System.IO.File.Exists(tempDir + @"TestAddControl_W3C_STYLE.js"),
				"Test Failed - Could not overwrite temp script file.");

			//3. Check that script is added to repository if does not exist
			test = sr.AddTempScript("TestAddNewControl", "IE4_Style", sb);
			Assert.IsTrue(sr.GetScript("TestAddNewControl", "IE4_Style").IndexOf ("Web/TestAddNewControl_ie4style.js")!=0,
				"Test Failed - could not add script to repository.");

			//4. Check that Dom style is added to script name if does not exist
			test = sr.AddTempScript("TestAddNewControl", "W3C_STYLE", sb);
			Assert.IsTrue (sr.GetScript("TestAddNewControl", "W3C_STYLE").IndexOf ("Web/TestAddNewControl_w3cstyle.js")!=0,
				"Test Failed - could not add DOM style to existing script.");
		
			// Clean up - delete temp directory and files
			string[] aFiles = System.IO.Directory.GetFiles(tempDir);
			foreach (string file in aFiles)
			{
				System.IO.File.Delete(file);				
			}
			System.IO.Directory.Delete(tempDir);
		}
	}
}
