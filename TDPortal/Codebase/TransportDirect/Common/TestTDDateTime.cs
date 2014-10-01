//***********************************************
//NAME			: TestTDDateTime.cs
//AUTHOR		: Andy Lole
//DATE CREATED	: 15/07/2003
//DESCRIPTION	: NUnit test class for TDDateTime
//***********************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Common/TestTDDateTime.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:19:08   mturner
//Initial revision.
//
//   Rev 1.9   Apr 19 2006 12:39:20   mtillett
//Correct the implicit conversion operator for DateTime to TDDateTime
//Resolution for 3923: DN091 Travel News Updates: Incident dates and times are incorrect in the table view
//
//   Rev 1.8   Aug 18 2005 11:29:06   jgeorge
//Automatically merged from branch for stream2558
//
//   Rev 1.7.1.0   Jul 01 2005 11:10:12   jmorrissey
//Added TestTDMonthYear
//Resolution for 2558: Del 8 Stream: Incident mapping
//
//   Rev 1.7   Apr 01 2005 16:47:22   jbroome
//Added TestAreSameDate and TestGetDifferenceDates methods.
//Resolution for 1941: DEV Code Review : Coach Fares Price Supplier
//
//   Rev 1.6   Feb 07 2005 08:46:54   RScott
//Assertion changed to Assert
//
//   Rev 1.5   Sep 17 2003 12:22:30   croberts
//PVCS Tracker Test
//Resolution for 2: Test SCR Module Association
//
//   Rev 1.4   Sep 16 2003 16:30:26   jcotton
//PVCS Test
//
//   Rev 1.3   Aug 18 2003 17:56:44   geaton
//Added test for testing millisecond constructor.
//
//   Rev 1.2   Jul 24 2003 17:22:44   alole
//Changed all test method names to start Test
//
//   Rev 1.1   Jul 23 2003 11:44:38   alole
//Updated to include tests for all methods
//
//   Rev 1.0   Jul 16 2003 12:01:32   ALole
//Initial Revision

using System;
using System.Threading;
using System.Globalization;
using NUnit.Framework;



namespace TransportDirect.Common
{
	/// <summary>
	/// Summary description for TestTDDateTime.
	/// </summary>
	[TestFixture]
	public class TestTDDateTime
	{
		
		private TDDateTime testTDDateTime;
		private DateTime testDateTime;

		/// <summary>
		/// default constructor
		/// </summary>
		public TestTDDateTime()
		{
		}

		//initialisation in setup method called before every test method.
		[SetUp]
		public void Init()
		{						
			//Set the Known DateTime for the FixNow method
			testDateTime = new DateTime(2003, 7, 15, 16, 15, 0);
		}

		//finalisation in TearDown method called after every test method.
		[TearDown]
		public void CleanUp()
		{
			
		}   			   
		
		/// <summary>
		/// Tests the Constructor taking year, month and day info
		/// </summary>
		[Test]
		public void TestInstantiateYearMonthDay()
		{
			//Create testTDDateTime using (year, month day)
			testTDDateTime = new TDDateTime(2003, 7, 14);
			
			Assert.IsTrue(testTDDateTime.Year == 2003);
			Assert.IsTrue(testTDDateTime.Month == 7);
			Assert.IsTrue(testTDDateTime.Day == 14);
			

		}

		/// <summary>
		/// Tests the Constructor taking year, month, day, hour, minute and second info
		/// </summary>
		[Test]
		public void TestInstantiateYearMonthDayHourMinuteSecond()
		{
			//Create testTDDateTime using (year, month, day, hour, minute, second)
			testTDDateTime = new TDDateTime(2003, 7, 14, 13, 15, 0);
			
			Assert.IsTrue(testTDDateTime.Year == 2003);
			Assert.IsTrue(testTDDateTime.Month == 7);
			Assert.IsTrue(testTDDateTime.Day == 14);
			Assert.IsTrue(testTDDateTime.Hour == 13);
			Assert.IsTrue(testTDDateTime.Minute == 15);
			Assert.IsTrue(testTDDateTime.Second == 0);
		}

		/// <summary>
		/// Tests the Constructor taking year, month, day, hour, minute, second and millisecond info
		/// </summary>
		[Test]
		public void TestInstantiateYearMonthDayHourMinuteSecondMillisecond()
		{
			//Create testTDDateTime using (year, month, day, hour, minute, second, millisecond)
			testTDDateTime = new TDDateTime(2003, 7, 14, 13, 15, 0, 12);
			
			Assert.IsTrue(testTDDateTime.Year == 2003);
			Assert.IsTrue(testTDDateTime.Month == 7);
			Assert.IsTrue(testTDDateTime.Day == 14);
			Assert.IsTrue(testTDDateTime.Hour == 13);
			Assert.IsTrue(testTDDateTime.Minute == 15);
			Assert.IsTrue(testTDDateTime.Second == 0);
			Assert.IsTrue(testTDDateTime.Millisecond == 12);
		}

		/// <summary>
		/// Tests the implicit conversion between DateTime and TDDateTime
		/// </summary>
		[Test]
		public void TestImplicitConversionDateTime()
		{
			//Create testTDDateTime using (System.DateTime.Now)
			DateTime testDT = System.DateTime.Now;
			TDDateTime tdDT = testDT;
			
			Assert.IsTrue(testDT.Equals(tdDT.GetDateTime()));
		}

		/// <summary>
		/// Tests the Constructor taking a DateTime
		/// </summary>
		[Test]
		public void TestInstantiateDateTime()
		{
			DateTime testDT = System.DateTime.Now;
			testTDDateTime = new TDDateTime(testDT);
			
			Assert.IsTrue(testDT.Equals(testTDDateTime.GetDateTime()));
		}
		
		/// <summary>
		/// Tests the Add method
		/// </summary>
		[Test]
		public void TestAdd()
		{
			testTDDateTime = new TDDateTime(2003, 7, 14);
			TimeSpan testTimeSpan = new TimeSpan(24,0,0);

			TDDateTime test2 = testTDDateTime.Add(testTimeSpan);
			
			Assert.IsTrue(test2.Day == 15);
		}

		/// <summary>
		/// Tests the Subtract method
		/// </summary>
		[Test]
		public void TestSubtract()
		{
			testTDDateTime = new TDDateTime(2003, 7, 14);
			TimeSpan testTimeSpan = new TimeSpan(24,0,0);

			TDDateTime test2 = testTDDateTime.Subtract(testTimeSpan);
			
			Assert.IsTrue(test2.Day == 13);
		}
		
		/// <summary>
		/// Tests the CompareTo method
		/// </summary>
		[Test]
		public void TestCompareTo()
		{
			//Create testTDDateTime using (System.DateTime.Now)
			DateTime testDT = System.DateTime.Now;
			testTDDateTime = new TDDateTime(testDT);
			TDDateTime test2 = new TDDateTime(testDT);
			
			Assert.IsTrue((testTDDateTime.CompareTo(test2)) == 0);
		}

		/// <summary>
		/// Tests the GetHashCode method
		/// </summary>
		[Test]
		public void TestGetHashCode()
		{
			DateTime testDT = System.DateTime.Now;
			testTDDateTime = new TDDateTime(testDT);
			
			Assert.IsTrue(testDT.GetHashCode() == testTDDateTime.GetHashCode());
		}

		/// <summary>
		/// Tests the SetFormat method
		/// </summary>
		[Test]
		public void TestSetFormat()
		{
			testTDDateTime = new TDDateTime(2003, 3, 4);
			testTDDateTime.SetFormat(new CultureInfo("en-GB"));
			Assert.IsTrue(testTDDateTime.ToString() == "04/03/2003 00:00:00");

			testTDDateTime.SetFormat(new CultureInfo("en-US"));
			Assert.IsTrue(testTDDateTime.ToString() == "3/4/2003 12:00:00 AM");
		}

		/// <summary>
		/// Tests the DaysInMonth method
		/// </summary>
		[Test]
		public void TestDaysInMonth()
		{			
			Assert.IsTrue(TDDateTime.DaysInMonth(2003, 1) == 31);
		}

		/// <summary>
		/// Tests the IsLeapYear method
		/// </summary>
		[Test]
		public void TestIsLeapYear()
		{			
			Assert.IsTrue(TDDateTime.IsLeapYear(2003) == false);
			Assert.IsTrue(TDDateTime.IsLeapYear(2004) == true);
		}

		/// <summary>
		/// Tests the AreSameDate method
		/// </summary>
		[Test]
		public void TestAreSameDate()
		{
			TDDateTime TestDate = new TDDateTime(2005, 01, 01, 12, 00, 00, 00);
			TDDateTime date1 = new TDDateTime(2004, 01, 01, 12, 00, 00, 00);
			TDDateTime date2 = new TDDateTime(2005, 03, 01, 12, 00, 00, 00);
			TDDateTime date3 = new TDDateTime(2005, 01, 02, 12, 00, 00, 00);
			TDDateTime date4 = new TDDateTime(2005, 01, 01, 14, 00, 00, 00);
			TDDateTime date5 = new TDDateTime(2005, 01, 01, 14, 30, 00, 00);
			TDDateTime date6 = new TDDateTime(2005, 01, 01, 14, 00, 10, 00);
			TDDateTime date7 = new TDDateTime(2005, 01, 01, 14, 00, 10, 10);
			TDDateTime date8 = new TDDateTime();

			Assert.AreEqual(false, TDDateTime.AreSameDate(TestDate, date1), "TestDate and date1 are not the same date");
			Assert.AreEqual(false, TDDateTime.AreSameDate(TestDate, date2), "TestDate and date2 are not the same date");
			Assert.AreEqual(false, TDDateTime.AreSameDate(TestDate, date3), "TestDate and date3 are not the same date");
			Assert.AreEqual(true, TDDateTime.AreSameDate(TestDate, date4), "TestDate and date4 are the same date");
			Assert.AreEqual(true, TDDateTime.AreSameDate(TestDate, date5), "TestDate and date5 are the same date");
			Assert.AreEqual(true, TDDateTime.AreSameDate(TestDate, date6), "TestDate and date6 are the same date");
			Assert.AreEqual(true, TDDateTime.AreSameDate(TestDate, date7), "TestDate and date7 are the same date");
			Assert.AreEqual(false, TDDateTime.AreSameDate(TestDate, date8), "TestDate and date8 are not the same date");
			
		}

		/// <summary>
		/// Tests the GetDifferenceDates method
		/// </summary>
		[Test]
		public void TestGetDifferenceDates()
		{
			TDDateTime TestDate = new TDDateTime(2005, 01, 01, 12, 30, 15, 02);
			TDDateTime date1 = new TDDateTime(2005, 01, 03);
			TDDateTime date2 = new TDDateTime(2004, 12, 28, 15, 00, 25);
			TDDateTime date3 = new TDDateTime(2005, 01, 01, 18, 00, 00);
			TDDateTime date4 = new TDDateTime(2005, 01, 02, 00, 00, 00);
			
			Assert.AreEqual(2, TestDate.GetDifferenceDates(date1), "Difference between TestDate and date1 incorrect");
			Assert.AreEqual(-4, TestDate.GetDifferenceDates(date2), "Difference between TestDate and date2 incorrect");
			Assert.AreEqual(0, TestDate.GetDifferenceDates(date3), "Difference between TestDate and date3 incorrect");
			Assert.AreEqual(1, TestDate.GetDifferenceDates(date4), "Difference between TestDate and date4 incorrect");
			
		}

		/// <summary>
		/// Tests the Parse method
		/// </summary>
		[Test]
		public void TestParse()
		{
			testTDDateTime = TDDateTime.Parse("04/03/2003", new CultureInfo("en-GB"));
			
			Assert.IsTrue(testTDDateTime.ToString() == "04/03/2003 00:00:00");
		}
		
		/// <summary>
		/// Tests the FixNow method
		/// </summary>
		[Test]
		public void TestFixNow()
		{
			testTDDateTime = new TDDateTime(2003, 03, 04);

			Assert.IsTrue(TDDateTime.Now != testTDDateTime);

			TDDateTime.FixNow(testTDDateTime);

			Assert.IsTrue(TDDateTime.Now == testTDDateTime);
		}

		/// <summary>
		/// Tests the TDMonthYear method
		/// </summary>
		[Test]
		public void TestTDMonthYear()
		{
			testTDDateTime = new TDDateTime();
			string date = testTDDateTime.ToString("MM/yyyy");

			string monthyear = testTDDateTime.TDMonthYear;

			Assert.IsTrue(monthyear == date);

			Assert.IsTrue(monthyear.Substring(0,2) == testTDDateTime.ToString("MM"));
			Assert.IsTrue(monthyear.Substring(2,1) == "/");
			Assert.IsTrue(monthyear.Substring(3,4) == testTDDateTime.Year.ToString());
		}

	}		
	
}
