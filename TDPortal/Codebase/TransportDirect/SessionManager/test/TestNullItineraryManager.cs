// ***********************************************
// NAME 		: TestNullItineraryManager.cs
// AUTHOR 		: Tim Mollart
// DATE CREATED : 29/10/2005
// DESCRIPTION 	: NUnit test for NullItineraryManager class.
// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/SessionManager/test/TestNullItineraryManager.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:49:02   mturner
//Initial revision.
//
//   Rev 1.0   Oct 29 2005 14:19:44   tmollart
//Initial revision.
//Resolution for 2638: DEL 8 Stream: Visit Planner

using System;
using NUnit.Framework;

namespace TransportDirect.UserPortal.SessionManager
{
	/// <summary>
	/// Test class for NullItineraryManager
	/// </summary>
	[TestFixture]
	public class TestNullItineraryManager
	{

		/// <summary>
		/// Default constructor.
		/// </summary>
		public TestNullItineraryManager()
		{
		}

		/// <summary>
		/// Tests that a Null Itinerary Manager can be constructed.
		/// </summary>
		[Test]
		public void TestCreateNullItineraryManager()
		{

			NullItineraryManager im;

			try
			{
				im = new NullItineraryManager();
				Assert.IsNotNull(im, "Itinerary Manager was found to be null");
			}
			catch (Exception ex)
			{
				Assert.Fail(ex.Message);
			}
		}
	}
}
