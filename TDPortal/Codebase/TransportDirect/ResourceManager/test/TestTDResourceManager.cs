//******************************************************
//NAME			: TestTDResourceManager.cs
//AUTHOR		: Joe Morrissey
//DATE CREATED	: 01/07/2003
//DESCRIPTION	: NUnit test class for TDResourceManager 
//******************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ResourceManager/test/TestTDResourceManager.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:46:00   mturner
//Initial revision.
//
//   Rev 1.1   Feb 02 2006 11:07:24   mdambrine
//All tests for welsh and english language have been move to testTDPage as they are now residing in TDPage
//
//   Rev 1.0   Jan 11 2006 10:22:56   mdambrine
//Initial revision.
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.9   May 26 2005 16:28:42   rscott
//Updated to fix NUnit tests
//
//   Rev 1.8   Feb 08 2005 14:48:18   RScott
//Assertions changed to Asserts
//
//   Rev 1.7   Sep 17 2003 09:04:34   jcotton
//Tracker Test
//
//   Rev 1.6   Jul 24 2003 10:59:10   JMorrissey
//commented out declaration of a test Global.TDDTIManager, which is not yet being used but will be in the future
//
//   Rev 1.5   Jul 22 2003 13:43:04   JMorrissey
//Removed #if DEBUG directive
//
//   Rev 1.4   Jul 18 2003 16:28:12   JMorrissey
//No longer test methods SetTextOnControls and SetLanguageCulture which are now in the new LanguageHandler class
//
//   Rev 1.3   Jul 11 2003 14:56:12   JMorrissey
//Added SetEnglishTextOnControls and SetWelshTextOnControls methods

using System;
using System.Threading;
using NUnit.Framework;


namespace TransportDirect.Common.ResourceManager
{
	/// <summary>
	/// Class for testing TDResourceManager.cs
	/// </summary>
	[TestFixture]
	public class TestTDResourceManager
	{
		
		/// <summary>
		/// default constructor
		/// </summary>
		public TestTDResourceManager()
		{
			
		}

		//initialisation in setup method called before every test method
		[SetUp]
		public void Init()
		{			
			
		}

		//finalisation in TearDown method called after every test method
		[TearDown]
		public void CleanUp()
		{

		}   				
				
	}
}
