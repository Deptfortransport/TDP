///<header>
///Monthly WebPage And Map Usage Report (REPORT 11)
///Created 30/04/2004
///Author JP Scott
///
///Version	Date		Who	Reason
///1		30/04/2004	PS	Created
///2        12/08/2004  PS V1.0.12 add test callendarand workload events
///3		04/10/2004  PS increase testperiod date array
///5        18/11/2004  PS increase test period accuracy
///
///</header>
using System;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Text;
using System.Diagnostics;

using DailyReportFramework;

namespace DailyReportFramework
{
	/// <summary>
	/// Class to produce Monthly Web Page And Map UsageReport
	/// analyses all Page entry events and map events and reports counts for month
	/// </summary>
	/// 
	
	public class MonthlyWebPageAndMapUsageReport
	{
		private DateTime currentDateTime;
		private string reportStartDateString;
		private string reportEndDateString;
		private DateTime reportStartDate;
		private DateTime reportEndDate;
		private DateTime reportEndDatePlus1;
		private string reportEndDatePlus1String;
        private string RequestedDateString;
        private EventLog EventLogger;
		private	DailyReportController controller;
		
		private string propertyConnectionString;
		private string reportingConnectionString;
		private SqlConnection reportingSqlConnection;
		private SqlDataReader myDataReader;
		private SqlCommand mySqlCommand;
		private StringBuilder tmpString;
		private string selectString;
		private System.IO.StreamWriter file;
		private string filePathString;
		private int statusCode;
		private int[] dayTot = new int[32];
		private int[] monthlyDayTot = new int[32];
		private int[,,] DayHrMinCount = new int[32,24,60];  //1-31 - 0-23,0-59
		private DateTime[] TestStarted = new DateTime[250]; 
		private DateTime[] TestCompleted = new DateTime[250]; 
		private int testCount;
		private DateTime SDate;
		private DateTime EDate;
		private int SHour,EHour,SMin,EMin ;
		private long ScompareString,EcompareString;
		private StringBuilder excludeString;

		
		public MonthlyWebPageAndMapUsageReport()
		{
			EventLogger = new EventLog("Application");
			EventLogger.Source = "TD.Reporting";
			controller    = new DailyReportController();
			
			
			
            ConfigurationManager.GetSection("appSettings");
            propertyConnectionString = ConfigurationManager.AppSettings["ReportProperties.connectionstring"];
            reportingConnectionString = ConfigurationManager.AppSettings["ReportDatabase.connectionstring"];
        }
		
		public int RunReport(DateTime reportDate)
		{

			statusCode = (int)StatusCode.Success; // success
			string reportDateString;
            RequestedDateString = reportDate.Year.ToString().Substring(2, 2)
            + "-" + reportDate.Month.ToString().PadLeft(2, '0');
            reportDateString = reportDate.ToShortDateString();
			currentDateTime = System.DateTime.Now;
			reportStartDate = new DateTime(reportDate.Year,reportDate.Month,1,0,0,0);
			reportEndDate = reportStartDate.AddMonths(1);
			reportEndDate = reportEndDate.AddDays(-1);
			reportStartDateString = reportStartDate.ToShortDateString();
			reportEndDateString = reportEndDate.ToShortDateString();
			reportEndDatePlus1 = reportEndDate.AddDays(1);
			reportEndDatePlus1String = reportEndDatePlus1.ToShortDateString();

			int daysInMonth;
			int transactionCount = 0;
//			int totalWebPageCount = 0;
			int totalMapCount = 0;
			int totalAdjustedMapCount = 0;
			int totalWorkload = 0;
			int totalAdjustedWorkload = 0;


			daysInMonth = reportEndDate.Day;
			
			tmpString = new StringBuilder();

			bool readOK =  controller.ReadProperty("11", "FilePath", out filePathString);
			if (readOK == false || filePathString.Length == 0)
			{
				statusCode = (int)StatusCode.ErrorReadingFilePath; // Error Reading FilePath
				return statusCode;
			}
            filePathString = filePathString.Replace("%YY-MM%", RequestedDateString);

			// open report text file
            using (file = new StreamWriter(filePathString, false)) // new FileStream(filePathString, System.IO.FileMode.Create)))
            {
                this.file.WriteLine("------------------------------------------");
                this.file.WriteLine("TRANSPORT DIRECT");
                this.file.WriteLine("DfT Monthly Web Page and Maps Usage Report");
                this.file.WriteLine("------------------------------------------");
                this.file.WriteLine("");
                this.file.WriteLine("For month starting : " + reportStartDate.ToShortDateString());
                this.file.WriteLine("");

                this.file.WriteLine("");
                this.file.Flush();



                // open and read test calendar		
                selectString =
                    "select TimeStarted,TimeCompleted "
                    + "from dbo.TestCalendar "
                    + "where TimeCompleted >= convert(datetime, '" + reportStartDateString
                    + "',103) and TimeStarted < convert(datetime, '" + reportEndDatePlus1String
                    + "',103) order by TimeStarted";




                testCount = 0;
                DateTime TestBegins = new DateTime(1, 1, 1, 0, 0, 0);
                DateTime TestEnds = new DateTime(1, 1, 1, 0, 0, 0);


                reportingSqlConnection = new SqlConnection(reportingConnectionString);

                myDataReader = null;
                mySqlCommand = new SqlCommand(selectString, reportingSqlConnection);


                try
                {
                    reportingSqlConnection.Open();


                    myDataReader = mySqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
                    while (myDataReader.Read())
                    {
                        TestBegins = Convert.ToDateTime(myDataReader.GetDateTime(0).ToString());
                        TestEnds = Convert.ToDateTime(myDataReader.GetDateTime(1).ToString());
                        testCount = testCount + 1;
                        TestStarted[testCount] = TestBegins;
                        TestCompleted[testCount] = TestEnds;
                    } // end of while

                }
                catch (Exception e)
                {
                    EventLogger.WriteEntry("Failure reading test calendar " + e.Message, EventLogEntryType.Error);
                    statusCode = (int)StatusCode.ErrorReadingCalendar; // Failed to calendar
                }
                finally
                {
                    // Always call Close when done reading.
                    if (myDataReader != null)
                        myDataReader.Close();
                }
                reportingSqlConnection.Close();


                //*******************************


                reportingSqlConnection = new SqlConnection(reportingConnectionString);
                transactionCount = 0;
                float percentageMaps = 0;
                float percentageAdjustedMaps = 0;


                //
                //			// map events  7,11,15,16,28,30,32,37,38
                //			//7, 'JourneyMap', 'Journey Map'
                //			//11, 'DetailedLegMap', 'Detailed Leg Map'
                //			//15, 'PrintableJourneyMaps', 'Printable Journey Maps'
                //			//16, 'PrintableJourneyMapInput', 'Printable Journey Map Input'
                //			//28, 'GeneralMaps', 'General Maps'	
                //			//30, 'NetworkMaps', 'Network Maps'
                //			//32, 'Map', 'Map'
                //			//37, 'TrafficMap', 'Traffic Map'
                //			//38, 'PrintableTrafficMap', 'Printable Traffic Map'
                //	
                //
                //			selectString = 
                //				"select PT.PETDescription, isnull(sum(PE.PEECount),0)"
                //				+ "from dbo.PageEntryEvents PE, dbo.PageEntryType PT "
                //				+ "where PT.PETID = PE.PEEPETID "
                //				+ " and PE.PEEPETID not in (5,7,11,15,16,28,30,32,37,38) "
                //				+ " and PE.PEEDate >= convert(datetime, '" + reportStartDateString
                //				+ "',103) and PE.PEEDate <= convert(datetime, '" + reportEndDateString
                //				+ "',103) group by PT.PETDescription order by PT.PETDescription";
                //
                //			myDataReader = null;
                //			mySqlCommand = new SqlCommand(selectString, reportingSqlConnection);
                //			string webPage;
                //			webPage="";
                //			try
                //			{
                //				reportingSqlConnection.Open();
                //				myDataReader = mySqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
                //				while (myDataReader.Read())
                //				{
                //					webPage  = myDataReader.GetString(0);
                //					transactionCount = Convert.ToInt32(myDataReader.GetSqlInt32(1).ToString());
                //					tmpString = new StringBuilder();
                //					tmpString.Append(webPage);
                //					tmpString.Append(",");
                //					tmpString.Append(transactionCount.ToString());
                //					this.file.WriteLine(tmpString);
                //					totalWebPageCount = totalWebPageCount +transactionCount;
                //				} // end of while
                //				this.file.WriteLine(" ");
                //				this.file.Flush();
                //				myDataReader.Close();
                //			}
                //			catch(Exception e)
                //			{
                //				EventLogger.WriteEntry ("Failure reading page events table "+e.Message,EventLogEntryType.Error);
                //				statusCode = (int)StatusCode.ErrorReadingTable; // Failed to reading page events table
                //			}
                //			finally
                //			{
                //				// Always call Close when done reading.
                //				if (myDataReader != null)
                //					myDataReader.Close();
                //			}
                //			//.............................			
                //
                //			
                //			selectString = 
                //				"select PT.PETDescription, isnull(sum(PE.PEECount),0)"
                //				+ "from dbo.PageEntryEvents PE, dbo.PageEntryType PT "
                //				+ "where PT.PETID = PE.PEEPETID "
                //				+ " and PE.PEEPETID in (5,7,11,15,16,28,30,32,37,38) "
                //				+ "  and PE.PEEDate >= convert(datetime, '" + reportStartDateString
                //				+ "',103) and PE.PEEDate <= convert(datetime, '" + reportEndDateString
                //				+ "',103) group by PT.PETDescription order by PT.PETDescription";
                //
                //			myDataReader = null;
                //			mySqlCommand = new SqlCommand(selectString, reportingSqlConnection);
                //			webPage="";
                //			transactionCount = 0;
                //
                //			try
                //			{
                //				reportingSqlConnection.Open();
                //				myDataReader = mySqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
                //				while (myDataReader.Read())
                //				{
                //					webPage  = myDataReader.GetString(0);
                //					transactionCount = Convert.ToInt32(myDataReader.GetSqlInt32(1).ToString());
                //					tmpString = new StringBuilder();
                //					tmpString.Append(webPage);
                //					tmpString.Append(",");
                //					tmpString.Append(transactionCount.ToString());
                //					this.file.WriteLine(tmpString);
                //					totalMapCount = totalMapCount +transactionCount;
                //
                //				} // end of while
                //				this.file.Flush();
                //				myDataReader.Close();
                //			}
                //			catch(Exception e)
                //			{
                //				EventLogger.WriteEntry ("Failure reading page events table "+e.Message,EventLogEntryType.Error);
                //				statusCode = (int)StatusCode.ErrorReadingTable; // Failed to reading page events table
                //			}
                //			finally
                //			{
                //				// Always call Close when done reading.
                //				if (myDataReader != null)
                //					myDataReader.Close();
                //			}
                //
                //

                selectString = "select isnull(sum(WECount),0)from dbo.WorkloadEvents "
                    + "where WEDate >= convert(datetime, '" + reportStartDateString
                    + "',103) and WEDate <= convert(datetime, '" + reportEndDateString
                    + "',103)";
                myDataReader = null;
                mySqlCommand = new SqlCommand(selectString, reportingSqlConnection);
                transactionCount = 0;

                try
                {
                    reportingSqlConnection.Open();
                    myDataReader = mySqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
                    while (myDataReader.Read())
                    {
                        transactionCount = transactionCount + Convert.ToInt32(myDataReader.GetSqlInt32(0).ToString());
                    } // end of while
                    totalWorkload = totalWorkload + transactionCount;
                    //				tmpString = new StringBuilder();
                    //				tmpString.Append("WorkloadEvents (Web Hits),");
                    //				tmpString.Append(transactionCount.ToString());
                    //				this.file.WriteLine(tmpString.ToString());
                    //				this.file.Flush();
                    myDataReader.Close();
                }
                catch (Exception e)
                {
                    EventLogger.WriteEntry("Error reading workload events " + e.Message, EventLogEntryType.Error);
                    statusCode = (int)StatusCode.ErrorReadingTable; // Failed reading table
                }
                finally
                {
                    // Always call Close when done reading.
                    if (myDataReader != null)
                        myDataReader.Close();
                }



                SDate = new DateTime(1, 1, 1, 0, 0, 0);
                EDate = new DateTime(1, 1, 1, 0, 0, 0);
                excludeString = new StringBuilder();
                for (int i = 1; i <= testCount; i++)
                {
                    SDate = TestStarted[i].Date;
                    SHour = TestStarted[i].Hour;
                    SMin = TestStarted[i].Minute;

                    EDate = TestCompleted[i].Date;
                    EHour = TestCompleted[i].Hour;
                    EMin = TestCompleted[i].Minute;

                    ScompareString = ((long)SDate.Year - 2000) * 100000000L + ((long)SDate.Month * 1000000L) + ((long)SDate.Day * 10000L) + ((long)SHour * 100) + (long)SMin;
                    EcompareString = ((long)EDate.Year - 2000) * 100000000L + ((long)EDate.Month * 1000000L) + ((long)EDate.Day * 10000L) + ((long)EHour * 100) + (long)EMin;

                    //				ScompareString = (long)SDate.Year *1000000L + ((long)SDate.Month *10000L) + ((long)SDate.Day*100L) + (long)SHour; //+(long)SHourQuarter;
                    //				EcompareString = (long)EDate.Year *1000000L + ((long)EDate.Month *10000L) + ((long)EDate.Day*100L) + (long)EHour ;//+(long)EHourQuarter;

                    // build exclusion string - nothing allowed within this 			
                    excludeString.Append(" and not (");
                    excludeString.Append("((year(WEDate)-2000)*100000000+month(WEDate)*1000000+day(WEDate)*10000+WEHour*100+WEMinute) >= " + ScompareString);
                    excludeString.Append(" and ((year(WEDate)-2000)*100000000+month(WEDate)*1000000+day(WEDate)*10000+WEHour*100+WEMinute) <= " + EcompareString);
                    excludeString.Append(")");


                }
                selectString = "select isnull(sum(WECount),0)from dbo.WorkloadEvents "
                + "where WEDate >= convert(datetime, '" + reportStartDateString
                + "',103) and WEDate <= convert(datetime, '" + reportEndDateString
                + "',103) "
                + excludeString;
                myDataReader = null;
                mySqlCommand = new SqlCommand(selectString, reportingSqlConnection);
                transactionCount = 0;
                try
                {
                    reportingSqlConnection.Open();
                    myDataReader = mySqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
                    while (myDataReader.Read())
                    {
                        transactionCount = transactionCount + Convert.ToInt32(myDataReader.GetSqlInt32(0).ToString());
                    } // end of while
                    totalAdjustedWorkload = totalAdjustedMapCount + transactionCount;
                    //				tmpString = new StringBuilder();
                    //				tmpString.Append("WorkloadEvents (Excluding Test Periods),");
                    //				tmpString.Append(transactionCount.ToString());
                    //				this.file.WriteLine(tmpString.ToString());
                    //				this.file.Flush();
                    myDataReader.Close();
                }
                catch (Exception e)
                {
                    EventLogger.WriteEntry("Error reading Map Events " + e.Message, EventLogEntryType.Error);
                    statusCode = (int)StatusCode.ErrorReadingTable; // Failed reading table
                }
                finally
                {
                    // Always call Close when done reading.
                    if (myDataReader != null)
                        myDataReader.Close();
                }

                selectString = "select isnull(sum(MECount),0)from dbo.MapEvents "
                + "where MEDate >= convert(datetime, '" + reportStartDateString
                + "',103) and MEDate <= convert(datetime, '" + reportEndDateString
                + "',103)";

                myDataReader = null;
                mySqlCommand = new SqlCommand(selectString, reportingSqlConnection);
                transactionCount = 0;

                try
                {
                    reportingSqlConnection.Open();
                    myDataReader = mySqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
                    while (myDataReader.Read())
                    {
                        transactionCount = transactionCount + Convert.ToInt32(myDataReader.GetSqlInt32(0).ToString());
                    } // end of while
                    totalMapCount = totalMapCount + transactionCount;
                    //				tmpString = new StringBuilder();
                    //				tmpString.Append("MapPageEvents (displays/pans/zooms/etc.),");
                    //				tmpString.Append(transactionCount.ToString());
                    //				this.file.WriteLine(tmpString.ToString());
                    //				this.file.Flush();
                    myDataReader.Close();
                }
                catch (Exception e)
                {
                    EventLogger.WriteEntry("Error reading Map Events " + e.Message, EventLogEntryType.Error);
                    statusCode = (int)StatusCode.ErrorReadingTable; // Failed reading table
                }
                finally
                {
                    // Always call Close when done reading.
                    if (myDataReader != null)
                        myDataReader.Close();
                }



                SDate = new DateTime(1, 1, 1, 0, 0, 0);
                EDate = new DateTime(1, 1, 1, 0, 0, 0);
                excludeString = new StringBuilder();
                for (int i = 1; i <= testCount; i++)
                {
                    SDate = TestStarted[i].Date;
                    SHour = TestStarted[i].Hour;
                    SMin = TestStarted[i].Minute;

                    EDate = TestCompleted[i].Date;
                    EHour = TestCompleted[i].Hour;
                    EMin = TestCompleted[i].Minute;
                    SMin = SMin / 15;
                    EMin = EMin / 15;
                    ScompareString = ((long)SDate.Year - 2000) * 100000000L + ((long)SDate.Month * 1000000L) + ((long)SDate.Day * 10000L) + ((long)SHour * 100) + (long)SMin;
                    EcompareString = ((long)EDate.Year - 2000) * 100000000L + ((long)EDate.Month * 1000000L) + ((long)EDate.Day * 10000L) + ((long)EHour * 100) + (long)EMin;

                    // build exclusion string - nothing allowed within this 			
                    excludeString.Append(" and not (");
                    excludeString.Append("((year(MEDate)-2000)*100000000+month(MEDate)*1000000+day(MEDate)*10000+MEHour*100 + MEHourQuarter) >= " + ScompareString);
                    excludeString.Append(" and ((year(MEDate)-2000)*100000000+month(MEDate)*1000000+day(MEDate)*10000+MEHour*100 + MEHourQuarter) <= " + EcompareString);
                    excludeString.Append(")");
                }

                selectString = "select isnull(sum(MECount),0)from dbo.MapEvents "
                    + "where MEDate >= convert(datetime, '" + reportStartDateString
                    + "',103) and MEDate <= convert(datetime, '" + reportEndDateString
                    + "',103) "
                    + excludeString;

                //			tmpString = new StringBuilder();
                mySqlCommand = new SqlCommand(selectString, reportingSqlConnection);
                transactionCount = 0;

                try
                {
                    reportingSqlConnection.Open();
                    myDataReader = mySqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
                    while (myDataReader.Read())
                    {
                        transactionCount = transactionCount + Convert.ToInt32(myDataReader.GetSqlInt32(0).ToString());
                    } // end of while
                    totalAdjustedMapCount = totalAdjustedMapCount + transactionCount;
                    //				tmpString.Append("MapPageEvents (Excluding Test Periods),");
                    //				myDataReader = null;
                    //				tmpString.Append(transactionCount.ToString());
                    //				this.file.WriteLine(tmpString.ToString());
                    //				this.file.Flush();
                    myDataReader.Close();
                }
                catch (Exception e)
                {
                    EventLogger.WriteEntry("Error reading Map Events " + e.Message, EventLogEntryType.Error);
                    statusCode = (int)StatusCode.ErrorReadingTable; // Failed reading table
                }
                finally
                {
                    // Always call Close when done reading.
                    if (myDataReader != null)
                        myDataReader.Close();
                }



                //.............................			
                this.file.WriteLine(" ");
                this.file.WriteLine(" ");
                //			this.file.WriteLine("Summary Totals for Month ");
                //			this.file.WriteLine(" ");
                //			this.file.WriteLine("Total Web Pages visited ,"+totalWebPageCount.ToString());

                this.file.WriteLine("Web Pages ");
                this.file.WriteLine("Web Page Hits (WorkloadEvents)              ," + totalWorkload.ToString());
                this.file.WriteLine("Web Page Hits (WorkloadEvents)(Excl. Tests) ," + totalAdjustedWorkload.ToString());
                this.file.WriteLine(" ");
                this.file.WriteLine("Map Usage  (displays /pans /zooms /overlays etc.)");
                this.file.WriteLine("Map Events                                  ," + totalMapCount.ToString());
                this.file.WriteLine("Map Events (Excl. Tests)                    ," + totalAdjustedMapCount.ToString());
                this.file.WriteLine(" ");
                percentageMaps = (float)(Math.Floor((100000.00 * ((float)totalMapCount / (float)totalWorkload))) / 1000);
                this.file.WriteLine("Percentage map page activity                 ," + percentageMaps.ToString());
                percentageAdjustedMaps = (float)(Math.Floor((100000.00 * ((float)totalAdjustedMapCount / (float)totalAdjustedWorkload))) / 1000);
                this.file.WriteLine("Percentage map page activity (Excl. Tests)   ," + percentageAdjustedMaps.ToString());

                if (percentageMaps > 10)
                {

                    this.file.WriteLine("The ratio of Map Pages to Web Pages has exceeded the maximum ratio (1:10) this month.");
                }
                else
                {
                    this.file.WriteLine("The ratio of Map Pages to Web Pages has NOT exceeded the maximum ratio (1:10) this month.");
                }

                this.file.Flush();

                this.file.Close();
            }

			string from = "";
			string to = "";
			string smtpServer = "";
			string subject = "";


			readOK =  controller.ReadProperty("11", "FilePath", out filePathString);
			if (readOK == false || filePathString.Length == 0)
			{
				statusCode = (int)StatusCode.ErrorReadingFilePath; // Error Reading FilePath
				return statusCode;
			}
            filePathString = filePathString.Replace("%YY-MM%", RequestedDateString);
            readOK = controller.ReadProperty("0", "MailAddress", out from);
			if (readOK == false || from.Length == 0)
			{
				statusCode = (int)StatusCode.ErrorReadingSenderEmailAddress; // Error Reading sender email address
				return statusCode;
			}
			readOK =  controller.ReadProperty("11", "MailRecipient", out to);
			if (readOK == false || to.Length == 0)
			{
				statusCode = (int)StatusCode.ErrorReadingRecipientEmailAddress; // Error Reading recipient email address
				return statusCode;
			}
			readOK =  controller.ReadProperty("11", "Title", out subject);
			if (readOK == false || subject.Length == 0)
			{
				subject = "Monthly Web Page And Map Usage Report %YY-MM%";
			}
            subject = subject.Replace("%YY-MM%", RequestedDateString);

			readOK =  controller.ReadProperty("0", "smtpServer", out smtpServer);
			if (readOK == false || smtpServer.Length == 0)
			{
				statusCode = (int)StatusCode.ErrorReadingSmtpServer; // Error Reading smtpServer
				return statusCode;
			}

			string bodyText =  "Report is attached....";


			SendEmail mailObject = new SendEmail();
			int sendFileErrorCode = mailObject.SendFile(from, to, subject, bodyText, 
				filePathString, smtpServer);
			if (sendFileErrorCode != 0)
				statusCode = sendFileErrorCode;

			return statusCode;


		}
	}
}
