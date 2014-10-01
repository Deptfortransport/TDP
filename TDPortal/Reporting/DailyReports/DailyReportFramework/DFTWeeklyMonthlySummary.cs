///<header>
///DFTWeeklyMonthlySummary.cs
///Created 17/03/2010
///Author JP Scott
///
///Version	Date		Who	Reason
///1		17/03/2010	PS	Created
///</header>
///
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
	/// Class to DFTWeeklyMonthlySummary
	/// </summary>
	/// 
	
	public class DFTWeeklyMonthlySummary
	{
		private DateTime currentDateTime;
		private string reportStartDateString;
		private string reportEndDateString;
        private DateTime reportEndDatePlus1;
		private string reportEndDatePlus1String;
		private DateTime reportStartDate;
		private DateTime reportEndDate;
        private string RequestedDateString;


		private EventLog EventLogger;
		private DailyReportController controller;
		
		private string propertyConnectionString;
		private string reportingConnectionString;
		private SqlConnection reportingSqlConnection;
		private SqlDataReader myDataReader;
		private SqlCommand mySqlCommand;
		private StringBuilder tmpString;
		private string transactionString;
		private string selectString;
		private System.IO.StreamWriter file;
        //private System.IO.StreamReader kizoomFile;
		private string filePathString;
		private int statusCode;
        private int transactionCount;
        private int periodTotal;
        private DateTime[] TestStarted = new DateTime[250];
        private DateTime[] TestCompleted = new DateTime[250];
        private int testCount;
        private DateTime SDate;
        private DateTime EDate;
        private DateTime thisDay; 
        private int SHour, EHour, SMin, EMin;
        private long ScompareString, EcompareString;
        private StringBuilder excludeString;
        private int[,] MapCmdCount = new int[6, 6];
        private int[,] MapCmdTime = new int[6, 6];
        private string[] mapCommand = new string[6];
        private string[] mapDisplay = new string[6];
        private int totalChartView = 0;
        private int totalTableView = 0;
        private string[] splitString = new string[10];
        private StringBuilder tmpString1;
        private StringBuilder tmpString2;
        private StringBuilder tmpString3;
        private StringBuilder tmpString4;
		private int[] WorkLoad_dayTot = new int[32];
        private int[] UserSessions_dayTot = new int[32];
        private int[] MobileWorkLoad_dayTot = new int[32];
        private int[] MobileUserSessions_dayTot = new int[32];
        private int[] DigiTVUserSessions_dayTot = new int[32];
        private int[] DigiTVWorkLoad_dayTot = new int[32];



        private int NumTypesToReport;
        private int[] RefType = new int[50];
        private string[] RefTypeCode = new string[50];
        private int[] NominalCount = new int[50];
        private int[] FailedCount = new int[50];
        private double FailedPercent;
        private string defaultInjectorString;
        private string backupInjectorString;
        private string travelineInjectorString; 
        
        private float rounding = (float) 0.000499;
        private int mapEventTotal;
        private int mapEventTotalExcl;
        private int userWebPageRequests;
        private int userWebPageRequestsExcl;
        private int userSessions;
        private int userSessionsExcl;
        private int singlePageSessions;
        private int multiPageSessions;
        private int averageSessionDuration;
        private int averageMultiSessionDuration;
        private int newVisitors;
        private int repeatVisitors;
        private int firstDay;
        private int lastDay;
        private int daySpan;
        private string frequency;









		public DFTWeeklyMonthlySummary()
		{
			EventLogger = new EventLog("Application");
			EventLogger.Source = "TD.Reporting";
			controller    = new DailyReportController();
			
            ConfigurationManager.GetSection("appSettings");
            propertyConnectionString = ConfigurationManager.AppSettings["ReportProperties.connectionstring"];
            reportingConnectionString = ConfigurationManager.AppSettings["ReportDatabase.connectionstring"];
        }
		
		public int RunReport(DateTime reportDate, int reportNumber)
		{


            statusCode = (int)StatusCode.Success; // success
            string reportDateString;
            reportDateString = reportDate.ToShortDateString();
            currentDateTime = System.DateTime.Now;
            lastDay = 31;
            firstDay = 1;
                
            if (reportNumber == 28)
            {
                frequency = "W";
                daySpan = 7;
                // find current date.
                RequestedDateString = reportDate.Year.ToString().Substring(2, 2)
                    + "-" + reportDate.Month.ToString().PadLeft(2, '0')
                    + "-" + reportDate.Day.ToString().PadLeft(2, '0');
                reportStartDate = reportDate;
                while (reportStartDate.DayOfWeek.ToString() != "Monday")
                {
                    reportStartDate = reportStartDate.AddDays(-1);
                }
                reportEndDate = reportStartDate.AddDays(6);
                lastDay = 7;
            }
            else
            {
                frequency = "M";
                daySpan = 31;
                RequestedDateString = reportDate.Year.ToString().Substring(2, 2)
                    + "-" + reportDate.Month.ToString().PadLeft(2, '0');
                reportStartDate = new DateTime(reportDate.Year, reportDate.Month, 1, 0, 0, 0);
                reportEndDate = reportStartDate.AddMonths(1);
                reportEndDate = reportEndDate.AddDays(-1);
                lastDay = reportEndDate.Day;
            }
            
            reportStartDateString = reportStartDate.ToShortDateString();
            reportEndDateString = reportEndDate.ToShortDateString();
            reportEndDatePlus1 = reportEndDate.AddDays(1);
            reportEndDatePlus1String = reportEndDatePlus1.ToShortDateString();


            
            
            
   			bool readOK =  controller.ReadProperty(reportNumber.ToString(), "FilePath", out filePathString);
			if (readOK == false || filePathString.Length == 0)
			{
				statusCode = (int)StatusCode.ErrorReadingFilePath; // Error Reading FilePath
				return statusCode;
			}
            filePathString = filePathString.Replace("%YY-MM-DD%", RequestedDateString);
            filePathString = filePathString.Replace("%YY-MM%", RequestedDateString);
    

			// open report text file
			file = new StreamWriter( new FileStream(filePathString, System.IO.FileMode.Create ) );
			this.file.WriteLine("------------------------------------------");
            if (reportNumber==29)
            {
                this.file.WriteLine("TRANSPORT DIRECT DFT MONTHLY SUMMARY");
                this.file.WriteLine("------------------------------------");
                this.file.WriteLine("");
                this.file.WriteLine("For month starting : " + reportStartDate.ToShortDateString());
            }
            else
            {
                this.file.WriteLine("TRANSPORT DIRECT DFT WEEKLY SUMMARY");
                this.file.WriteLine("------------------------------------");
                this.file.WriteLine("");
                this.file.WriteLine("For week starting : " + reportStartDate.ToShortDateString());
            }
            this.file.WriteLine("");
            this.file.WriteLine(",Total");

            reportingSqlConnection = new SqlConnection(reportingConnectionString);


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

            if (testCount > 0)
            {
                this.file.WriteLine(" ");
                this.file.WriteLine("The following periods of agreed outage/testing occurred in this reporting period");
                this.file.WriteLine("From                   To");
                for (int i = 1; i <= testCount; i++)
                {
                    this.file.WriteLine(TestStarted[i] + "   " + TestCompleted[i]);
                }
                this.file.WriteLine(" ");
            }


            //*******************************
                    
            this.file.WriteLine(" ");
            transactionString = "Map Events (pans/zooms/etc.)";
            selectString = "select isnull(sum(MECount),0)from dbo.MapEvents "
                          + "where MEDate >= convert(datetime, '" + reportStartDateString
                          + "',103) and MEDate <= convert(datetime, '" + reportEndDateString
                          + "',103) group by MEDate order by MEDate";
            GetPeriodTotal();
            mapEventTotal = periodTotal;
            this.file.WriteLine("Map Events (pans/zooms/etc.)," + mapEventTotal);


                     //.............................			
                     // build exclusion string
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
                      transactionString = "Map Events (Excluding Tests)";
                      selectString = "select isnull(sum(MECount),0)from dbo.MapEvents "
                          + "where MEDate >= convert(datetime, '" + reportStartDateString
                          + "',103) and MEDate <= convert(datetime, '" + reportEndDateString
                          + "',103) "
                          + excludeString
                          + " group by MEDate order by MEDate";
                      GetPeriodTotal();
                      mapEventTotalExcl = periodTotal;
                      this.file.WriteLine("Map Events (Excluding Tests)," + mapEventTotalExcl);

                      reportingSqlConnection = new SqlConnection(reportingConnectionString);

                      //...............................................

                      transactionString = "User Web Page Requests (Total WorkLoad)";
                      selectString = "select isnull(sum(WECount),0)from dbo.WorkloadEvents "
                          + "where WEDate >= convert(datetime, '" + reportStartDateString
                          + "',103) and WEDate <= convert(datetime, '" + reportEndDateString
                          + "',103) group by WEDate order by WEDate";
                      GetPeriodTotal();
                      userWebPageRequests = periodTotal;
                      DailyEventBreakDown();
                      float tmpVal = ((float)mapEventTotal * 100 / (float)userWebPageRequests) + rounding;
                      this.file.WriteLine("Map Pages as a percentage of user web pages," + 
                          String.Format("{0:0.000}", tmpVal));
                      tmpVal = ((float)userWebPageRequests / (float)mapEventTotal ) + rounding;
                      this.file.WriteLine("Ratio of map pages to Web pages  1: ," + String.Format("{0:0.000}", tmpVal));

                      // build exclusion string
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

                          // build exclusion string - nothing allowed within this 			
                          excludeString.Append(" and not (");
                          excludeString.Append("((year(WEDate)-2000)*100000000+month(WEDate)*1000000+day(WEDate)*10000+WEHour*100+WEMinute) >= " + ScompareString);
                          excludeString.Append(" and ((year(WEDate)-2000)*100000000+month(WEDate)*1000000+day(WEDate)*10000+WEHour*100+WEMinute) <= " + EcompareString);
                          excludeString.Append(")");
                      }

                      
                      transactionString = "User Web Page Requests (Excluding Tests)";
                      selectString = "select isnull(sum(WECount),0)from dbo.WorkloadEvents "
                          + "where WEDate >= convert(datetime, '" + reportStartDateString
                          + "',103) and WEDate <= convert(datetime, '" + reportEndDateString
                          + "',103) "
                          + excludeString
                          + " group by WEDate order by WEDate";
                      GetPeriodTotal();
                      userWebPageRequestsExcl = periodTotal;
                      this.file.WriteLine(" ");
                      this.file.WriteLine(" ");
                      this.file.WriteLine("User Web Page Requests (Total WorkLoad) ," + userWebPageRequests.ToString());
                      this.file.WriteLine("User Web Page Requests (Excluding Tests) ," + userWebPageRequestsExcl.ToString());
                      this.file.WriteLine(" ");
                      this.file.WriteLine(" ");

          //*******************************************************************************

                      // build exclusion string
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

                          if (SMin > 30)
                          {
                              SHour = SHour + 1;
                              if (SHour > 23)
                              {
                                  SHour = 0;
                                  SDate = TestStarted[i].Date.AddDays(1);
                              }

                          }
                          if (EMin > 30)
                          {
                              EHour = EHour + 1;
                              if (EHour > 23)
                              {
                                  EHour = 0;
                                  EDate = TestCompleted[i].Date.AddDays(1);
                              }

                          }
                          ScompareString = (long)SDate.Year * 1000000L + ((long)SDate.Month * 10000L) + ((long)SDate.Day * 100L) + (long)SHour; //+(long)SHourQuarter;
                          EcompareString = (long)EDate.Year * 1000000L + ((long)EDate.Month * 10000L) + ((long)EDate.Day * 100L) + (long)EHour;//+(long)EHourQuarter;

                          // build exclusion string - nothing allowed within this 			
                          excludeString.Append(" and not (");
                          excludeString.Append("(year(SEDate)*1000000+month(SEDate)*10000+day(SEDate)*100+SEHour) >= " + ScompareString);
                          excludeString.Append(" and (year(SEDate)*1000000+month(SEDate)*10000+day(SEDate)*100+SEHour) <= " + EcompareString);
                          excludeString.Append(")");
                      }


                      transactionString = "User Sessions";
                      selectString = "select isnull(sum(SECount),0)from dbo.SessionEvents "
                          + "where SEDate >= convert(datetime, '" + reportStartDateString
                          + "',103) and SEDate <= convert(datetime, '" + reportEndDateString
                          + "',103) ";
                      GetPeriodTotal();
                      userSessions = periodTotal;
                      DailyEventBreakDown();
                      //.............................		

                      transactionString = "Single Page Sessions ";
                      selectString = "select isnull(sum(SDASinglePageSessions ),0)from dbo.SessionDailyAnalysis "
                          + "where SDADate >= convert(datetime, '" + reportStartDateString
                          + "',103) and SDADate <= convert(datetime, '" + reportEndDateString
                          + "',103) ";
                      GetPeriodTotal();
                      singlePageSessions = periodTotal;

                      transactionString = "Multi-Page Sessions ";
                      selectString = "select isnull(sum(SDAMultiplePageSessions ),0)from dbo.SessionDailyAnalysis "
                          + "where SDADate >= convert(datetime, '" + reportStartDateString
                          + "',103) and SDADate <= convert(datetime, '" + reportEndDateString
                          + "',103) ";
                      GetPeriodTotal();
                      multiPageSessions = periodTotal;



                      transactionString = "Average Session Duration (secs) ";
                      selectString = "select isnull(sum(SDAAverageDuration),0)from dbo.SessionDailyAnalysis "
                          + "where SDADate >= convert(datetime, '" + reportStartDateString
                          + "',103) and SDADate <= convert(datetime, '" + reportEndDateString
                          + "',103) ";
                      GetPeriodTotal();
                      averageSessionDuration = periodTotal;




                      selectString = "select isnull(sum(SDAAverageDurationMultiplePageSessions),0)from dbo.SessionDailyAnalysis "
                          + "where SDADate >= convert(datetime, '" + reportStartDateString
                          + "',103) and SDADate <= convert(datetime, '" + reportEndDateString
                          + "',103) ";







                      GetPeriodTotal();
                      averageMultiSessionDuration = periodTotal;

                      //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

                      transactionString = "User Sessions (Excluding Tests)";
                      selectString = "select isnull(sum(SECount),0)from dbo.SessionEvents "
                          + "where SEDate >= convert(datetime, '" + reportStartDateString
                          + "',103) and SEDate <= convert(datetime, '" + reportEndDateString
                          + "',103) "
                          + excludeString;
                      GetPeriodTotal();
                      userSessionsExcl = periodTotal;

                      this.file.WriteLine("User Sessions,"+userSessions.ToString());
                      this.file.WriteLine("Single Page Sessions,"+singlePageSessions.ToString());
                      tmpVal = ((float)singlePageSessions * 100 / (float)userSessions) + rounding;
                      this.file.WriteLine("Single Page Sessions as a percentage of the sum single and multiple sessions," + String.Format("{0:0.000}", tmpVal));
                      this.file.WriteLine("Multi-Page Sessions," + multiPageSessions.ToString());
                      tmpVal = ((float)multiPageSessions * 100 / (float)userSessions) + rounding;
                      this.file.WriteLine("Multi-Page Sessions as a percentage of the sum single and multiple sessions," + String.Format("{0:0.000}", tmpVal));



                      selectString =
                                "select isnull("
                                 + "SUM(SDASinglePageSessions * isnull(SDAAverageDuration,0)) "
                                 + "/ (CASE WHEN SUM(isnull(SDASinglePageSessions,0)) = 0 THEN 1 ELSE SUM(isnull(SDASinglePageSessions,0)) END) "
                                 + ", 0)"
                                 + "from dbo.SessionDailyAnalysis "
                                 + "where SDADate >= convert(datetime, '" + reportStartDateString
                                 + "',103) and SDADate <= convert(datetime, '" + reportEndDateString
                                 + "',103)";
                      GetPeriodTotal();
                      averageSessionDuration = periodTotal;
                      this.file.WriteLine("Average Session Duration (secs) ," + averageSessionDuration.ToString());




                      selectString =
                                  "select isnull("
                                  + "SUM(SDAMultiplePageSessions * isnull(SDAAverageDurationMultiplePageSessions,0)) "
                                  + "/ (CASE WHEN SUM(isnull(SDAMultiplePageSessions,0)) = 0 THEN 1 ELSE SUM(isnull(SDAMultiplePageSessions,0)) END) "
                                  + ", 0)"
                                  + "from dbo.SessionDailyAnalysis "
                                  + "where SDADate >= convert(datetime, '" + reportStartDateString
                                  + "',103) and SDADate <= convert(datetime, '" + reportEndDateString
                                  + "',103)";
                      GetPeriodTotal();
                      averageMultiSessionDuration = periodTotal;    
                      this.file.WriteLine("Average Multi-Page Session Duration (secs) ,"+averageMultiSessionDuration.ToString());
                      
                      this.file.WriteLine("User Sessions (Excluding Tests),"+userSessionsExcl.ToString());

            

                      transactionString = "New Visitors ";
                      selectString = "select isnull(sum(RVECount),0)from dbo.RepeatVisitorEvents "
                          + "where RVEDate >= convert(datetime, '" + reportStartDateString
                          + "',103) and RVEDate <= convert(datetime, '" + reportEndDateString
                          + "',103) and RVERVTID = 2"
                          + " group by RVEDate order by RVEDate";
                      GetPeriodTotal();
                      newVisitors = periodTotal;
                      this.file.WriteLine(transactionString + "," + newVisitors.ToString());

          
                      transactionString = "New Visitors (Excluding Tests)";
                      selectString = "select isnull(sum(RVECount),0)from dbo.RepeatVisitorEvents "
                          + "where RVEDate >= convert(datetime, '" + reportStartDateString
                          + "',103) and RVEDate <= convert(datetime, '" + reportEndDateString
                          + "',103) "
                          + excludeString
                          + " and RVERVTID = 2"
                          + " group by RVEDate order by RVEDate";
                      GetPeriodTotal();
                      this.file.WriteLine(transactionString + "," + periodTotal.ToString());
                  
                      transactionString = "Repeat Visitors ";
                      selectString = "select isnull(sum(RVECount),0)from dbo.RepeatVisitorEvents "
                          + "where RVEDate >= convert(datetime, '" + reportStartDateString
                          + "',103) and RVEDate <= convert(datetime, '" + reportEndDateString
                          + "',103) and RVERVTID = 3"
                          + " group by RVEDate order by RVEDate";
                      GetPeriodTotal();
                      repeatVisitors    = periodTotal;
                      tmpVal = ((float)newVisitors * 100 / ((float)newVisitors + (float)repeatVisitors) + rounding);
                      this.file.WriteLine("New Visitors as a percentage of the sum of new and repeat visitors," + String.Format("{0:0.000}", tmpVal));
                      this.file.WriteLine(transactionString + "," + repeatVisitors.ToString());

                      transactionString = "Repeat Visitors (Excluding Tests)";
                      selectString = "select isnull(sum(RVECount),0)from dbo.RepeatVisitorEvents "
                          + "where RVEDate >= convert(datetime, '" + reportStartDateString
                          + "',103) and RVEDate <= convert(datetime, '" + reportEndDateString
                          + "',103) "
                          + excludeString
                          + " and RVERVTID = 3"
                          + " group by RVEDate order by RVEDate";
                      GetPeriodTotal();
                      this.file.WriteLine(transactionString + "," + periodTotal.ToString());
                      tmpVal = ((float)repeatVisitors * 100 / ((float)newVisitors + (float)repeatVisitors) + rounding);
                      this.file.WriteLine("Repeat Visitors as a percentage of the sum of new and repeat visitors," + String.Format("{0:0.000}", tmpVal));
 
          
                      transactionString = "Registered User Sessions (Logins) ";
                      selectString = "select isnull(sum(LECount),0)from dbo.LoginEvents "
                          + "where LEDate >= convert(datetime, '" + reportStartDateString
                          + "',103) and LEDate <= convert(datetime, '" + reportEndDateString
                          + "',103) group by LEDate order by LEDate";
                      GetPeriodTotal();
                      this.file.WriteLine(transactionString + "," + periodTotal.ToString());
    

                      // build exclusion string
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
                          ScompareString = ((long)SDate.Year - 2000) * 100000000L + ((long)SDate.Month * 1000000L) + ((long)SDate.Day * 10000L) + (long)SHour * 100L + (long)SMin;
                          EcompareString = ((long)EDate.Year - 2000) * 100000000L + ((long)EDate.Month * 1000000L) + ((long)EDate.Day * 10000L) + (long)EHour * 100L + (long)EMin;

                          // build exclusion string - nothing allowed within this 			
                          excludeString.Append(" and not (");
                          excludeString.Append("((year(LEDate)-2000)*100000000+month(LEDate)*1000000+day(LEDate)*10000+LEHour*100+LEHourQuarter) >= " + ScompareString);
                          excludeString.Append(" and ((year(LEDate)-2000)*100000000+month(LEDate)*1000000+day(LEDate)*10000+LEHour*100+LEHourQuarter) <= " + EcompareString);
                          excludeString.Append(")");
                      }


                      transactionString = "Registered User Sessions (Excluding Tests)";
                      selectString = "select isnull(sum(LECount),0)from dbo.LoginEvents "
                          + "where LEDate >= convert(datetime, '" + reportStartDateString
                          + "',103) and LEDate <= convert(datetime, '" + reportEndDateString
                          + "',103) "
                          + excludeString
                          + " group by LEDate order by LEDate";
                      GetPeriodTotal();
                      this.file.WriteLine(transactionString + "," + periodTotal.ToString());
                      this.file.WriteLine(" ");

//***********************************************************************************************
#region Kizoom



                      selectString = "select Date,isnull(sum(Sessions),0),isnull(sum(TVSessions),0),isnull(sum(Pages),0),isnull(sum(TVPages),0) "
                      + "from dbo.KizoomEvents "
                      + "where Date > = convert(datetime, '" + reportStartDateString + "',103)"
                      + " and  Date < convert(datetime, '" + reportEndDatePlus1String + "',103)"
                      + " group by Date Order by Date";

                      myDataReader = null;
                      reportingSqlConnection = new SqlConnection(reportingConnectionString);
                      mySqlCommand = new SqlCommand(selectString, reportingSqlConnection);

                      int Sessions = 0;
                      int TVSessions = 0;
                      int Pages = 0;
                      int TVPages = 0;
                      int dayPtr;
                      for (dayPtr = 1; dayPtr <= 31; dayPtr++)
                      {
                          MobileUserSessions_dayTot[dayPtr] = 0;
                          DigiTVUserSessions_dayTot[dayPtr] = 0;
                          MobileWorkLoad_dayTot[dayPtr] = 0;
                          DigiTVWorkLoad_dayTot[dayPtr] = 0;
                      }
                      try
                      {
                          reportingSqlConnection.Open();
                          myDataReader = mySqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
                          while (myDataReader.Read())
                          {
                              thisDay = myDataReader.GetDateTime(0);
                              Sessions = Convert.ToInt32(myDataReader.GetSqlInt32(1).ToString());
                              TVSessions = Convert.ToInt32(myDataReader.GetSqlInt32(2).ToString());
                              Pages = Convert.ToInt32(myDataReader.GetSqlInt32(3).ToString());
                              TVPages = Convert.ToInt32(myDataReader.GetSqlInt32(4).ToString());

                              if (frequency.CompareTo("W") == 0)
                              {
                                  switch (thisDay.DayOfWeek.ToString())
                                  {
                                      case "Monday":
                                          dayPtr = 1;
                                          break;
                                      case "Tuesday":
                                          dayPtr = 2;
                                          break;
                                      case "Wednesday":
                                          dayPtr = 3;
                                          break;
                                      case "Thursday":
                                          dayPtr = 4;
                                          break;
                                      case "Friday":
                                          dayPtr = 5;
                                          break;
                                      case "Saturday":
                                          dayPtr = 6;
                                          break;
                                      case "Sunday":
                                          dayPtr = 7;
                                          break;
                                   }
                              }
                              else
                              {
                                  dayPtr = thisDay.Day;
                              }
                    
                              MobileUserSessions_dayTot[dayPtr] = MobileUserSessions_dayTot[dayPtr] + Sessions;
                              DigiTVUserSessions_dayTot[dayPtr] = DigiTVUserSessions_dayTot[dayPtr] + TVSessions;
                              MobileWorkLoad_dayTot[dayPtr] = MobileWorkLoad_dayTot[dayPtr] + Pages;
                              DigiTVWorkLoad_dayTot[dayPtr] = DigiTVWorkLoad_dayTot[dayPtr] + TVPages;

                          } // end of while


                          myDataReader.Close();

                      }
                      catch (Exception ex)
                      {
                          Console.WriteLine("error occurred" + ex.Message);

                      }
                      finally
                      {
                          // Always call Close when done reading.
                          if (myDataReader != null)
                              myDataReader.Close();
                      }


                      tmpString2 = new StringBuilder();
                      tmpString2.Append("Mobile Use Sessions");
                      int atot = 0;
                      for (int g = 1; g <= daySpan; g++)
                      {
                        MobileUserSessions_dayTot[g] = MobileUserSessions_dayTot[g] - DigiTVUserSessions_dayTot[g];
                        atot = atot + MobileUserSessions_dayTot[g];
                      }
                      tmpString2.Append(",");
                      tmpString2.Append(atot);
                      
            
                      tmpString4 = new StringBuilder();
                      tmpString4.Append("DigiTV User Sessions");
                      atot = 0;
                      for (int g = 1; g <= daySpan; g++)
                      {
                          atot = atot + DigiTVUserSessions_dayTot[g];
                      }
                      tmpString4.Append(",");
                      tmpString4.Append(atot);
                              
                      tmpString1 = new StringBuilder();
                      tmpString1.Append("Mobile Workload");
                      atot = 0;
                      for (int g = 1; g <= daySpan; g++)
                       {
                            MobileWorkLoad_dayTot[g] = MobileWorkLoad_dayTot[g] - DigiTVWorkLoad_dayTot[g];
                            atot = atot + MobileWorkLoad_dayTot[g]; 
                       }
                       tmpString1.Append(",");
                       tmpString1.Append(atot); 
                       
                       tmpString3 = new StringBuilder();
                       tmpString3.Append("DigiTV Workload");
                       atot = 0;
                       for (int g = 1; g <= daySpan; g++)
                       {
                                  atot = atot +  DigiTVWorkLoad_dayTot[g];
                       }
                       tmpString3.Append(",");
                       tmpString3.Append(atot);

                      this.file.WriteLine(" ");
                      this.file.WriteLine(" ");
                      this.file.WriteLine(tmpString1); //"Mobile Workload,");
                      this.file.WriteLine(tmpString2); //"Mobile Use Sessions,");
                      this.file.WriteLine(" "); 
                      this.file.WriteLine(tmpString3); //"DigiTV Workload,");
                      this.file.WriteLine(tmpString4); //"DigiTV User Sessions,");
                      this.file.WriteLine(" ");
#endregion

                      //*********************************************************************************

                      this.file.WriteLine(" ");
                      transactionString = "DepartureBoardService RTTI";
                      selectString = "select isnull(sum(EXSECount),0)from dbo.ExposedServicesEvents "
                          + "where EXSEDate >= convert(datetime, '" + reportStartDateString
                          + "',103) and EXSEDate <= convert(datetime, '" + reportEndDateString
                          + "',103) "
                          + " and EXSCID = 3 "
                          + " group by EXSEDate order by EXSEDate";
                      GetPeriodTotal();
                      this.file.WriteLine(transactionString + "," + periodTotal.ToString());
    
                      transactionString = "DepartureBoardService StopEvent";
                      selectString = "select isnull(sum(EXSECount),0)from dbo.ExposedServicesEvents "
                          + "where EXSEDate >= convert(datetime, '" + reportStartDateString
                          + "',103) and EXSEDate <= convert(datetime, '" + reportEndDateString
                          + "',103) "
                          + " and EXSCID = 2 "
                          + " group by EXSEDate order by EXSEDate";
                      GetPeriodTotal();
                      this.file.WriteLine(transactionString + "," + periodTotal.ToString());
    
                      this.file.WriteLine(" ");
                      this.file.WriteLine(" ");

                      //*******************************************************************


                      transactionString = "Enhanced Exposed Service Calls (Kizoom)";
                      selectString = "select isnull(sum(EESECount),0)from EnhancedExposedServiceEvents "
                          + "where EESEDate >= convert(datetime, '" + reportStartDateString
                          + "',103) and EESEDate <= convert(datetime, '" + reportEndDateString
                          + "',103) "
                          + " and EESEPartnerId = 103 "
                          + " group by EESEDate order by EESEDate";
                      GetPeriodTotal();
                      this.file.WriteLine(transactionString + "," + periodTotal.ToString());
    
                      transactionString = "Enhanced Exposed Service Calls (Lauren)";
                      selectString = "select isnull(sum(EESECount),0)from EnhancedExposedServiceEvents "
                          + "where EESEDate >= convert(datetime, '" + reportStartDateString
                          + "',103) and EESEDate <= convert(datetime, '" + reportEndDateString
                          + "',103) "
                          + " and EESEPartnerId = 102 "
                          + " group by EESEDate order by EESEDate";
                      GetPeriodTotal();
                      this.file.WriteLine(transactionString + "," + periodTotal.ToString());
    

                      transactionString = "Enhanced Exposed Service Calls (Direct Gov)";
                      selectString = "select isnull(sum(EESECount),0)from EnhancedExposedServiceEvents "
                          + "where EESEDate >= convert(datetime, '" + reportStartDateString
                          + "',103) and EESEDate <= convert(datetime, '" + reportEndDateString
                          + "',103) "
                          + " and EESEPartnerId = 104 "
                          + " group by EESEDate order by EESEDate";
                      GetPeriodTotal();
                      this.file.WriteLine(transactionString + "," + periodTotal.ToString());
    
                      transactionString = "Enhanced Exposed Service Calls (Direct GovDigiTV)";
                      selectString = "select isnull(sum(EESECount),0)from EnhancedExposedServiceEvents "
                          + "where EESEDate >= convert(datetime, '" + reportStartDateString
                          + "',103) and EESEDate <= convert(datetime, '" + reportEndDateString
                          + "',103) "
                          + " and EESEPartnerId = 109 "
                          + " group by EESEDate order by EESEDate";
                      GetPeriodTotal();
                      this.file.WriteLine(transactionString + "," + periodTotal.ToString());
    
                      transactionString = "Enhanced Exposed Service Calls (Direct GovJPlan)";
                      selectString = "select isnull(sum(EESECount),0)from EnhancedExposedServiceEvents "
                          + "where EESEDate >= convert(datetime, '" + reportStartDateString
                          + "',103) and EESEDate <= convert(datetime, '" + reportEndDateString
                          + "',103) "
                          + " and EESEPartnerId = 111 "
                          + " group by EESEDate order by EESEDate";
                      GetPeriodTotal();
                      this.file.WriteLine(transactionString + "," + periodTotal.ToString());
    
                      transactionString = "Enhanced Exposed Service Calls (Direct GovAPP2)";
                      selectString = "select isnull(sum(EESECount),0)from EnhancedExposedServiceEvents "
                          + "where EESEDate >= convert(datetime, '" + reportStartDateString
                          + "',103) and EESEDate <= convert(datetime, '" + reportEndDateString
                          + "',103) "
                          + " and EESEPartnerId = 121 "
                          + " group by EESEDate order by EESEDate";
                      GetPeriodTotal();
                      this.file.WriteLine(transactionString + "," + periodTotal.ToString());




                      transactionString = "Enhanced Exposed Service Calls (Batch Journey Planner)";
                      selectString = "select isnull(sum(EESECount),0)from EnhancedExposedServiceEvents "
                          + "where EESEDate >= convert(datetime, '" + reportStartDateString
                          + "',103) and EESEDate <= convert(datetime, '" + reportEndDateString
                          + "',103) "
                          + " and EESEPartnerId = 300 "
                          + " group by EESEDate order by EESEDate";
                      GetPeriodTotal();
                      this.file.WriteLine(transactionString + "," + periodTotal.ToString());

                      transactionString = "Enhanced Exposed Service Calls (Centro)";
                      selectString = "select isnull(sum(EESECount),0)from EnhancedExposedServiceEvents "
                          + "where EESEDate >= convert(datetime, '" + reportStartDateString
                          + "',103) and EESEDate <= convert(datetime, '" + reportEndDateString
                          + "',103) "
                          + " and EESEPartnerId = 122 "
                          + " group by EESEDate order by EESEDate";
                      GetPeriodTotal();
                      this.file.WriteLine(transactionString + "," + periodTotal.ToString());

                      transactionString = "Enhanced Exposed Service Calls (Blackburn)";
                      selectString = "select isnull(sum(EESECount),0)from EnhancedExposedServiceEvents "
                          + "where EESEDate >= convert(datetime, '" + reportStartDateString
                          + "',103) and EESEDate <= convert(datetime, '" + reportEndDateString
                          + "',103) "
                          + " and EESEPartnerId = 133 "
                          + " group by EESEDate order by EESEDate";
                      GetPeriodTotal();
                      this.file.WriteLine(transactionString + "," + periodTotal.ToString());
                      transactionString = "Enhanced Exposed Service Calls (Essex CC)";
                      selectString = "select isnull(sum(EESECount),0)from EnhancedExposedServiceEvents "
                          + "where EESEDate >= convert(datetime, '" + reportStartDateString
                          + "',103) and EESEDate <= convert(datetime, '" + reportEndDateString
                          + "',103) "
                          + " and EESEPartnerId = 136 "
                          + " group by EESEDate order by EESEDate";
                      GetPeriodTotal();
                      this.file.WriteLine(transactionString + "," + periodTotal.ToString());

                      transactionString = "Enhanced Exposed Service Calls (Stoke-on-Trent)";
                      selectString = "select isnull(sum(EESECount),0)from EnhancedExposedServiceEvents "
                          + "where EESEDate >= convert(datetime, '" + reportStartDateString
                          + "',103) and EESEDate <= convert(datetime, '" + reportEndDateString
                          + "',103) "
                          + " and EESEPartnerId = 137 "
                          + " group by EESEDate order by EESEDate";
                      GetPeriodTotal();
                      this.file.WriteLine(transactionString + "," + periodTotal.ToString());

                      transactionString = "Enhanced Exposed Service Calls (LiftShare)";
                      selectString = "select isnull(sum(EESECount),0)from EnhancedExposedServiceEvents "
                          + "where EESEDate >= convert(datetime, '" + reportStartDateString
                          + "',103) and EESEDate <= convert(datetime, '" + reportEndDateString
                          + "',103) "
                          + " and EESEPartnerId = 134 "
                          + " group by EESEDate order by EESEDate";
                      GetPeriodTotal();
                      this.file.WriteLine(transactionString + "," + periodTotal.ToString());

                      transactionString = "Enhanced Exposed Service Calls (CIBER)";
                      selectString = "select isnull(sum(EESECount),0)from EnhancedExposedServiceEvents "
                          + "where EESEDate >= convert(datetime, '" + reportStartDateString
                          + "',103) and EESEDate <= convert(datetime, '" + reportEndDateString
                          + "',103) "
                          + " and EESEPartnerId = 140 "
                          + " group by EESEDate order by EESEDate";
                      GetPeriodTotal();
                      this.file.WriteLine(transactionString + "," + periodTotal.ToString());

                      transactionString = "Enhanced Exposed Service Calls (Middlesbrough Council)";
                      selectString = "select isnull(sum(EESECount),0)from EnhancedExposedServiceEvents "
                          + "where EESEDate >= convert(datetime, '" + reportStartDateString
                          + "',103) and EESEDate <= convert(datetime, '" + reportEndDateString
                          + "',103) "
                          + " and EESEPartnerId = 142 "
                          + " group by EESEDate order by EESEDate";
                      GetPeriodTotal();
                      this.file.WriteLine(transactionString + "," + periodTotal.ToString());

                      this.file.WriteLine(" ");
 
                      //*******************************************************************


                      this.file.WriteLine(" ");
                      transactionString = "BBC User Sessions ";
                      selectString = "select isnull(sum(SECount),0)from dbo.SessionEvents "
                          + "where SEDate >= convert(datetime, '" + reportStartDateString
                          + "',103) and SEDate <= convert(datetime, '" + reportEndDateString
                          + "',103) and SEPartnerId = 2 group by SEDate order by SEDate";
                      GetPeriodTotal();
                      this.file.WriteLine(transactionString + "," + periodTotal.ToString());
                      transactionString = "BBC Workload";
                      selectString = "select isnull(sum(WECount),0)from dbo.WorkloadEvents "
                          + "where WEDate >= convert(datetime, '" + reportStartDateString
                          + "',103) and WEDate <= convert(datetime, '" + reportEndDateString
                          + "',103) and PartnerId = 2 group by WEDate order by WEDate";
                      GetPeriodTotal();
                      this.file.WriteLine(transactionString + "," + periodTotal.ToString());
                     
                      transactionString = "DirectGov User Sessions ";
                      selectString = "select isnull(sum(SECount),0)from dbo.SessionEvents "
                          + "where SEDate >= convert(datetime, '" + reportStartDateString
                          + "',103) and SEDate <= convert(datetime, '" + reportEndDateString
                          + "',103) and SEPartnerId = 4 group by SEDate order by SEDate";
                      GetPeriodTotal();
                      this.file.WriteLine(transactionString + "," + periodTotal.ToString());
                      transactionString = "DirectGov Workload";
                      selectString = "select isnull(sum(WECount),0)from dbo.WorkloadEvents "
                          + "where WEDate >= convert(datetime, '" + reportStartDateString
                          + "',103) and WEDate <= convert(datetime, '" + reportEndDateString
                          + "',103) and PartnerId = 4 group by WEDate order by WEDate";
                      GetPeriodTotal();
                      this.file.WriteLine(transactionString + "," + periodTotal.ToString());
                      

                      transactionString = "Visit Britain User Sessions ";
                      selectString = "select isnull(sum(SECount),0)from dbo.SessionEvents "
                          + "where SEDate >= convert(datetime, '" + reportStartDateString
                          + "',103) and SEDate <= convert(datetime, '" + reportEndDateString
                          + "',103) and SEPartnerId = 1 group by SEDate order by SEDate";
                      GetPeriodTotal();
                      this.file.WriteLine(transactionString + "," + periodTotal.ToString());
                      transactionString = "Visit Britain Workload";
                      selectString = "select isnull(sum(WECount),0)from dbo.WorkloadEvents "
                          + "where WEDate >= convert(datetime, '" + reportStartDateString
                          + "',103) and WEDate <= convert(datetime, '" + reportEndDateString
                          + "',103) and PartnerId = 1 group by WEDate order by WEDate";
                      GetPeriodTotal();
                      this.file.WriteLine(transactionString + "," + periodTotal.ToString());
                      
                      transactionString = "BusinessLink User Sessions ";
                      selectString = "select isnull(sum(SECount),0)from dbo.SessionEvents "
                          + "where SEDate >= convert(datetime, '" + reportStartDateString
                          + "',103) and SEDate <= convert(datetime, '" + reportEndDateString
                          + "',103) and SEPartnerId = 7 group by SEDate order by SEDate";
                      GetPeriodTotal();
                      this.file.WriteLine(transactionString + "," + periodTotal.ToString());
                      transactionString = "BusinessLink Workload";
                      selectString = "select isnull(sum(WECount),0)from dbo.WorkloadEvents "
                          + "where WEDate >= convert(datetime, '" + reportStartDateString
                          + "',103) and WEDate <= convert(datetime, '" + reportEndDateString
                          + "',103) and PartnerId = 7 group by WEDate order by WEDate";
                      GetPeriodTotal();
                      this.file.WriteLine(transactionString + "," + periodTotal.ToString());
                      
                      transactionString = "BusinessGateway User Sessions ";
                      selectString = "select isnull(sum(SECount),0)from dbo.SessionEvents "
                          + "where SEDate >= convert(datetime, '" + reportStartDateString
                          + "',103) and SEDate <= convert(datetime, '" + reportEndDateString
                          + "',103) and SEPartnerId = 8 group by SEDate order by SEDate";
                      GetPeriodTotal();
                      this.file.WriteLine(transactionString + "," + periodTotal.ToString());
                      transactionString = "BusinessGateway Workload";
                      selectString = "select isnull(sum(WECount),0)from dbo.WorkloadEvents "
                          + "where WEDate >= convert(datetime, '" + reportStartDateString
                          + "',103) and WEDate <= convert(datetime, '" + reportEndDateString
                          + "',103) and PartnerId = 8 group by WEDate order by WEDate";
                      GetPeriodTotal();
                      this.file.WriteLine(transactionString + "," + periodTotal.ToString());

                      transactionString = "CycleRoutes User Sessions ";
                      selectString = "select isnull(sum(SECount),0)from dbo.SessionEvents "
                          + "where SEDate >= convert(datetime, '" + reportStartDateString
                          + "',103) and SEDate <= convert(datetime, '" + reportEndDateString
                          + "',103) and SEPartnerId = 9 group by SEDate order by SEDate";
                      GetPeriodTotal();
                      this.file.WriteLine(transactionString + "," + periodTotal.ToString());
                      transactionString = "CycleRoutes Workload";
                      selectString = "select isnull(sum(WECount),0)from dbo.WorkloadEvents "
                          + "where WEDate >= convert(datetime, '" + reportStartDateString
                          + "',103) and WEDate <= convert(datetime, '" + reportEndDateString
                          + "',103) and PartnerId = 9 group by WEDate order by WEDate";
                      GetPeriodTotal();
                      this.file.WriteLine(transactionString + "," + periodTotal.ToString());
   
            //**************************************************************

                       transactionString = "Total Page Landing";
                      selectString = "select isnull(sum(LPECount),0)from dbo.LandingPageEvents "
                          + "where LPEDate >= convert(datetime, '" + reportStartDateString
                          + "',103) and LPEDate <= convert(datetime, '" + reportEndDateString
                          + "',103) ";
                      GetPeriodTotal();
                      this.file.WriteLine(transactionString + "," + periodTotal.ToString());
                      this.file.WriteLine(" "); 
            
                      this.file.WriteLine("Page Landing Top 5 ");

                      transactionString = "Top 5 Partner Totals";

                      selectString = "WITH CTETop5LandingPages "
                      + "AS ( select top 5 b.LPPDescription LPPDescription, isnull(sum(a.LPECount),0)  [Count] "
                      + "from dbo.LandingPageEvents a inner join dbo.LandingPagePartner b "
                      + "ON b.lppid = a.LPELPPID "
                      + "where a.LPEDate >= convert(datetime, '" + reportStartDateString
                      + "',103) and a.LPEDate <= convert(datetime, '" + reportEndDateString
                      + "',103) and a.LPELPPID = b.lppid and a.LPelppid NOT IN( 16  ,999  ) "
                      + " group by b.LPPDescription,a.LPELPSID order by SUM(a.lPECount) desc )"
                      + "SELECT * FROM CTETop5LandingPages "
                      + "UNION ALL select 'Other', isnull( "
                      + "   (SUM(a.lPECount) - (SELECT SUM([Count]) FROM CTETop5LandingPages)), 0) "
                      + "from dbo.LandingPageEvents a INNER JOIN dbo.LandingPagePartner b "
                      + "ON b.lppid = a.LPELPPID "
                       + "where a.LPEDate >= convert(datetime, '" + reportStartDateString
                      + "',103) and a.LPEDate <= convert(datetime, '" + reportEndDateString
                      + "',103)";

                     GetPeriodTotalWithDesc();

                      this.file.WriteLine(" ");
                      this.file.WriteLine(" ");
                      this.file.WriteLine("Page Landing By Type ");

                      transactionString = "Page Landing By Type ";
                      selectString = "select  b.LPSDescription, isnull(sum(a.LPECount),0) "
                    + " from dbo.LandingPageService b "
                    + " Left join  dbo.LandingPageEvents a "
                    + " on a.LPELPSID = b.lpsid "
                    + " and a.LPEDate >= convert(datetime, '" + reportStartDateString
                    + " ',103) and a.LPEDate <= convert(datetime, '" + reportEndDateString
                    + "',103) "
                    + " group by b.LPSDescription"
                    + " order by b.LPSDescription";

                      GetPeriodTotalWithDesc();
                      this.file.WriteLine(" ");
                      this.file.WriteLine(" ");
                      selectString = "select  b.LPPDescription, isnull(sum(a.LPECount),0) "
                       + " from dbo.LandingPageEvents a, dbo.LandingPagePartner b "
                           + "where a.LPEDate >= convert(datetime, '" + reportStartDateString
                           + "',103) and a.LPEDate <= convert(datetime, '" + reportEndDateString
                           + "',103) and a.LPELPPID = b.lppid and a.LPelppid = 21 "
                           + " group by b.LPPDescription";
             
                      GetPeriodTotalWithDesc();
                      this.file.WriteLine(" ");
                      this.file.WriteLine(" ");
//*****************************************************************************************************
//*****************************************************************************************************

                      this.file.WriteLine("Web Usage less Site Confidence");
                      this.file.WriteLine(" ");          

                      tmpString = new StringBuilder();
                      tmpString.Append("User Web Pages (Portal)");
                      atot=0;
                      int btot = 0;
                    for(int i=firstDay; i<=lastDay; i++)
                    {
                        btot = WorkLoad_dayTot[i]-1728;
                        atot=atot+btot;
                    }
                    tmpString.Append(",");
                    tmpString.Append(atot.ToString());
                    this.file.WriteLine(tmpString);


                    tmpString = new StringBuilder();
                      tmpString.Append("User Sessions (Portal)");
                      atot=0;
                      btot = 0;
                      for (int i = firstDay; i <= lastDay; i++)
                    {
                        btot = UserSessions_dayTot[i]-1248;
                        atot=atot+btot;
                    }
                    tmpString.Append(",");
                    tmpString.Append(atot.ToString());
                    this.file.WriteLine(tmpString);

                    this.file.WriteLine();

                    tmpString = new StringBuilder();
                      tmpString.Append("User Web Pages (Mobile)");
                      atot=0;
                      btot = 0;
                    for(int i=1; i<=daySpan; i++)
                    {
                        btot = MobileWorkLoad_dayTot[i]-288;
                        
                        atot=atot+btot;
                    }
                    tmpString.Append(",");
                    tmpString.Append(atot.ToString());
                    this.file.WriteLine(tmpString);

                        tmpString = new StringBuilder();
                      tmpString.Append("User Sessions (Mobile)");
                      atot=0;
                      btot = 0;
                    for(int i=1; i<=daySpan; i++)
                    {
                        btot = MobileUserSessions_dayTot[i]-288;
                      
                        atot=atot+btot;
                    }
                    tmpString.Append(",");
                    tmpString.Append(atot.ToString());
                    this.file.WriteLine(tmpString);

                    this.file.WriteLine();
        
//*******************************************************************************************************
 //*******************************************************************************************************

                      this.file.WriteLine("Retailer Handoff Events");
                      transactionString = "RetailerHandoffEvents";
                      selectString =
                          "select RHT.RHTDescription, isnull(sum(RH.RHECount),0)"
                          + "from dbo.RetailerHandoffEvents RH, dbo.RetailerHandoffType RHT where RHT.RHTID = RH.RHERHTID"
                          + " and RHT.RHTOnlineRetailer = 1 "
                          + "  and RH.RHEDate >= convert(datetime, '" + reportStartDateString
                          + "',103) and RH.RHEDate <= convert(datetime, '" + reportEndDateString
                          + "',103) group by RHT.RHTDescription order by RHT.RHTDescription";


                      GetPeriodTotalWithDesc();
                      this.file.WriteLine(" ");

//*******************************************************************************************************
//*******************************************************************************************************





                      if (reportNumber == 29)
                      {
                          this.file.WriteLine("Monthly Map Transactions");
                      }
                      else
                      {
                          this.file.WriteLine("Weekly Map Transactions");
                      }

                          this.file.WriteLine("");
            this.file.Flush();
            
            int productNum;
            int commandNum;
            int averageTime = 0;
            int totalVector = 0;
            int totalRaster = 0;
            string productString;
            string commandString;

            reportingSqlConnection = new SqlConnection(reportingConnectionString);

            selectString =
                "select ME.MEMDTID,MDT.MDTDescription,ME.MEMCTID,MCT.MCTDescription, isnull(sum(ME.MECount),0),avg(ME.MEAvMsDuration)"
                + "from dbo.MapEvents ME, dbo.MapCommandType MCT, dbo.MapDisplayType MDT "
                + "where ME.MEMCTID = MCT.MCTID and ME.MEMDTID = MDT.MDTID "
                + "  and ME.MEDate >= convert(datetime, '" + reportStartDateString
                + "',103) and ME.MEDate <= convert(datetime, '" + reportEndDateString
                + "',103) group by ME.MEMDTID,MDT.MDTDescription,ME.MEMCTID,MCT.MCTDescription order by ME.MEMDTID,MDT.MDTDescription,ME.MEMCTID,MCT.MCTDescription";

            myDataReader = null;
            mySqlCommand = new SqlCommand(selectString, reportingSqlConnection);
            transactionCount = 0;

            try
            {
                reportingSqlConnection.Open();
                myDataReader = mySqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (myDataReader.Read())
                {
                    productNum = Convert.ToInt32(myDataReader.GetByte(0).ToString());
                    productString = myDataReader.GetString(1);
                    commandNum = Convert.ToInt32(myDataReader.GetByte(2).ToString());
                    commandString = myDataReader.GetString(3);
                    transactionCount = Convert.ToInt32(myDataReader.GetSqlInt32(4).ToString());
                    averageTime = Convert.ToInt32(myDataReader.GetSqlInt32(5).ToString());

                    mapCommand[commandNum] = commandString;
                    mapDisplay[productNum] = productString;
                    MapCmdCount[productNum, commandNum] = transactionCount;
                    MapCmdTime[productNum, commandNum] = averageTime;


                } // end of while

                tmpString = new StringBuilder();
                tmpString.Append("Raster Maps");
                this.file.WriteLine(tmpString);

                tmpString = new StringBuilder();
                tmpString.Append("Product(Scale)");
                this.file.WriteLine(tmpString);
               
                int i;
                for (i = 1; i <= 2; i++)
                {

                    tmpString = new StringBuilder();
                    switch (i)
                    {
                        case 1:
                            tmpString.Append("OS Street View <1:10000 Scale");
                            break;
                        case 2:
                            tmpString.Append("OS 1/50th Raster <1:50000 Scale");
                            break;
                        case 3:
                            tmpString.Append("OS Meridian 2 <1:250000 Scale");
                            break;
                        case 4:
                            tmpString.Append("OS Meridian 2 <1:1000000 Scale");
                            break;
                        case 5:
                            tmpString.Append("OS Strategi >1:1000000 Scale");
                            break;
                    }
                    tmpString.Append(",");
                   
                    tmpString.Append(MapCmdCount[i, 1] + MapCmdCount[i, 2] + MapCmdCount[i, 3] + MapCmdCount[i, 5]);
                    this.file.WriteLine(tmpString);

                    totalRaster = totalRaster + MapCmdCount[i, 1] + MapCmdCount[i, 2] + MapCmdCount[i, 3] + MapCmdCount[i, 5];


                }

                this.file.WriteLine(" ");
             
                tmpString = new StringBuilder();
                tmpString.Append("Vector Maps");
                this.file.WriteLine(tmpString);

                tmpString = new StringBuilder();
                tmpString.Append("Product(Scale)");
                this.file.WriteLine(tmpString);

                for (i = 3; i <= 5; i++)
                {

                    tmpString = new StringBuilder();
                    switch (i)
                    {
                        case 1:
                            tmpString.Append("OS Street View <1:10000 Scale");
                            break;
                        case 2:
                            tmpString.Append("OS 1/50th Raster <1:50000 Scale");
                            break;
                        case 3:
                            tmpString.Append("OS Meridian 2 <1:250000 Scale");
                            break;
                        case 4:
                            tmpString.Append("OS Meridian 2 <1:1000000 Scale");
                            break;
                        case 5:
                            tmpString.Append("OS Strategi >1:1000000 Scale");
                            break;
                    }
                    tmpString.Append(",");

                    tmpString.Append(MapCmdCount[i, 1] + MapCmdCount[i, 2] + MapCmdCount[i, 3] + MapCmdCount[i, 5]);
                    this.file.WriteLine(tmpString);

                    totalVector = totalVector + MapCmdCount[i, 1] + MapCmdCount[i, 2] + MapCmdCount[i, 3] + MapCmdCount[i, 5];


                }

                this.file.Flush();
                myDataReader.Close();
            }
            catch (Exception e)
            {
                EventLogger.WriteEntry("Failure reading map events table " + e.Message, EventLogEntryType.Error);
                statusCode = (int)StatusCode.ErrorReadingTable; // Failed to reading map events table
            }
            finally
            {
                // Always call Close when done reading.
                if (myDataReader != null)
                    myDataReader.Close();
            }
                      
            //*************************************************************************************************************************
            
            selectString =
               "select  isnull(sum(GPECount),0)"
               + "from dbo.GradientProfileEvents "
               + "where  GPEDate >= convert(datetime, '" + reportStartDateString
               + "',103) and GPEDate <= convert(datetime, '" + reportEndDateString
               + "',103) and GPEGPDTID = 2";

            GetPeriodTotal();
            totalChartView = periodTotal;
            selectString =
               "select  isnull(sum(GPECount),0)"
               + "from dbo.GradientProfileEvents "
               + "where  GPEDate >= convert(datetime, '" + reportStartDateString
               + "',103) and GPEDate <= convert(datetime, '" + reportEndDateString
               + "',103) and GPEGPDTID = 3";

            GetPeriodTotal();
            totalTableView = periodTotal;
            this.file.WriteLine(" ");
            this.file.WriteLine("Gradient Profile Lookups ");
            this.file.WriteLine("WebServiceRequests ");
            this.file.WriteLine("Chart View,"+ totalChartView.ToString());
            this.file.WriteLine("Table View,"+ totalTableView.ToString());
            this.file.WriteLine(" ");
            this.file.Flush();



            // now find number of ITN routing transactions
            selectString =
                "select SUM(RPECount),RPECongestedRoute "
                + "from dbo.RoadPlanEvents "
                + "where RPEDate >= convert(datetime, '" + reportStartDateString
                + "',103) and RPEDate <= convert(datetime, '" + reportEndDateString
                + "',103) and RPECongestedRoute = 1 group by RPECongestedRoute order by RPECongestedRoute";

            myDataReader = null;
            mySqlCommand = new SqlCommand(selectString, reportingSqlConnection);
            transactionCount = 0;
            bool congested;
            int totalTrans = 0;
            try
            {
                reportingSqlConnection.Open();
                myDataReader = mySqlCommand.ExecuteReader(CommandBehavior.CloseConnection);

                tmpString = new StringBuilder();
                tmpString.Append("ITN Routing Transactions");
                this.file.WriteLine(tmpString);
                this.file.Flush();

                while (myDataReader.Read())
                {
                    transactionCount = Convert.ToInt32(myDataReader.GetSqlInt32(0).ToString());
                    totalTrans = totalTrans + transactionCount;
                    congested = myDataReader.GetBoolean(1);
                    if (congested == true)
                    {
                        tmpString = new StringBuilder();
                        tmpString.Append("Adjusted Route,");
                        tmpString.Append(transactionCount.ToString());
                        this.file.WriteLine(tmpString);
                    }
                } // end of while

                this.file.Flush();
                myDataReader.Close();
            }
            catch (Exception e)
            {
                EventLogger.WriteEntry("Failure reading RoadPlanEvents table " + e.Message, EventLogEntryType.Error);
                statusCode = (int)StatusCode.ErrorReadingTable; // Failed to reading page events table
            }
            finally
            {
                // Always call Close when done reading.
                if (myDataReader != null)
                    myDataReader.Close();
            }

/************************************************************************************************************/
            this.file.WriteLine(""); 
            
            if (reportNumber == 29)
            {
                this.file.WriteLine("Monthly OS Map Sessions");
            }
            else
            {
                this.file.WriteLine("Weekly OS Map Sessions");
            }

            this.file.WriteLine("");
            this.file.Flush();
            totalVector = 0;
            totalRaster = 0;
            
            reportingSqlConnection = new SqlConnection(reportingConnectionString);

            selectString =
                "select ME.MEMDTID,MDT.MDTDescription,isnull(sum(ME.MECount),0) "
                + "from dbo.MapEventsOS ME, dbo.MapCommandType MCT, dbo.MapDisplayType MDT "
                + "where ME.MEMCTID = MCT.MCTID and ME.MEMDTID = MDT.MDTID "
                + "  and ME.MEDate >= convert(datetime, '" + reportStartDateString
                + "',103) and ME.MEDate <= convert(datetime, '" + reportEndDateString
                + "',103) and ME.MEMCTID = 1 group by ME.MEMDTID,MDT.MDTDescription order by ME.MEMDTID,MDT.MDTDescription";


            
            myDataReader = null;
            mySqlCommand = new SqlCommand(selectString, reportingSqlConnection);
            transactionCount = 0;

            try
            {
                reportingSqlConnection.Open();
                myDataReader = mySqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (myDataReader.Read())
                {
                    productNum = Convert.ToInt32(myDataReader.GetByte(0).ToString());
                    productString = myDataReader.GetString(1);
                   
                    transactionCount = Convert.ToInt32(myDataReader.GetSqlInt32(2).ToString());
                    
                    mapDisplay[productNum] = productString;
                    MapCmdCount[productNum, 1] = transactionCount;
                    

                } // end of while

                tmpString = new StringBuilder();
                tmpString.Append("Raster Maps");
                this.file.WriteLine(tmpString);

                tmpString = new StringBuilder();
                tmpString.Append("Product(Scale)");
                this.file.WriteLine(tmpString);

                int i;
                for (i = 1; i <= 2; i++)
                {

                    tmpString = new StringBuilder();
                    switch (i)
                    {
                        case 1:
                            tmpString.Append("OS Street View <1:10000 Scale");
                            break;
                        case 2:
                            tmpString.Append("OS 1/50th Raster <1:50000 Scale");
                            break;
                        case 3:
                            tmpString.Append("OS Meridian 2 <1:250000 Scale");
                            break;
                        case 4:
                            tmpString.Append("OS Meridian 2 <1:1000000 Scale");
                            break;
                        case 5:
                            tmpString.Append("OS Strategi >1:1000000 Scale");
                            break;
                    }
                    tmpString.Append(",");

                    tmpString.Append(MapCmdCount[i, 1]);
                    this.file.WriteLine(tmpString);

                    totalRaster = totalRaster + MapCmdCount[i, 1];


                }

                this.file.WriteLine(" ");

                tmpString = new StringBuilder();
                tmpString.Append("Vector Maps");
                this.file.WriteLine(tmpString);

                tmpString = new StringBuilder();
                tmpString.Append("Product(Scale)");
                this.file.WriteLine(tmpString);

                for (i = 3; i <= 5; i++)
                {

                    tmpString = new StringBuilder();
                    switch (i)
                    {
                        case 1:
                            tmpString.Append("OS Street View <1:10000 Scale");
                            break;
                        case 2:
                            tmpString.Append("OS 1/50th Raster <1:50000 Scale");
                            break;
                        case 3:
                            tmpString.Append("OS Meridian 2 <1:250000 Scale");
                            break;
                        case 4:
                            tmpString.Append("OS Meridian 2 <1:1000000 Scale");
                            break;
                        case 5:
                            tmpString.Append("OS Strategi >1:1000000 Scale");
                            break;
                    }
                    tmpString.Append(",");

                    tmpString.Append(MapCmdCount[i, 1]);
                    this.file.WriteLine(tmpString);

                    totalVector = totalVector + MapCmdCount[i, 1];


                }

                this.file.Flush();
                myDataReader.Close();
            }
            catch (Exception e)
            {
                EventLogger.WriteEntry("Failure reading map events OS table " + e.Message, EventLogEntryType.Error);
                statusCode = (int)StatusCode.ErrorReadingTable; // Failed to reading map events table
            }
            finally
            {
                // Always call Close when done reading.
                if (myDataReader != null)
                    myDataReader.Close();
            }

            //*************************************************************************************************************************

            selectString =
               "select  isnull(sum(GPECount),0)"
               + "from dbo.GradientProfileOS "
               + "where  GPEDate >= convert(datetime, '" + reportStartDateString
               + "',103) and GPEDate <= convert(datetime, '" + reportEndDateString
               + "',103) and GPEGPDTID = 2";

            GetPeriodTotal();
            totalChartView = periodTotal;

            GetPeriodTotal();
            totalTableView = periodTotal;
            this.file.WriteLine(" ");
            this.file.WriteLine("Gradient Profile Sessions ");
            this.file.WriteLine("WebServiceRequests ,"  + totalChartView.ToString());
            this.file.WriteLine(" ");
            this.file.Flush();



            // now find number of ITN routing transactions
            selectString =
                "select SUM(RPECount),RPECongestedRoute "
                + "from dbo.RoadPlanEventsOS "
                + "where RPEDate >= convert(datetime, '" + reportStartDateString
                + "',103) and RPEDate <= convert(datetime, '" + reportEndDateString
                + "',103) and RPECongestedRoute = 1 group by RPECongestedRoute order by RPECongestedRoute";

            myDataReader = null;
            mySqlCommand = new SqlCommand(selectString, reportingSqlConnection);
            transactionCount = 0;
            totalTrans = 0;
            try
            {
                reportingSqlConnection.Open();
                myDataReader = mySqlCommand.ExecuteReader(CommandBehavior.CloseConnection);

                tmpString = new StringBuilder();
                tmpString.Append("ITN Routing Sessions");
                this.file.WriteLine(tmpString);
                this.file.Flush();

                while (myDataReader.Read())
                {
                    transactionCount = Convert.ToInt32(myDataReader.GetSqlInt32(0).ToString());
                    totalTrans = totalTrans + transactionCount;
                    congested = myDataReader.GetBoolean(1);
                    if (congested == true)
                    {
                        tmpString = new StringBuilder();
                        tmpString.Append("Adjusted Route,");
                        tmpString.Append(transactionCount.ToString());
                        this.file.WriteLine(tmpString);
                    }
                } // end of while

                this.file.Flush();
                myDataReader.Close();
            }
            catch (Exception e)
            {
                EventLogger.WriteEntry("Failure reading RoadPlanEventsOS table " + e.Message, EventLogEntryType.Error);
                statusCode = (int)StatusCode.ErrorReadingTable; // Failed to reading page events table
            }
            finally
            {
                // Always call Close when done reading.
                if (myDataReader != null)
                    myDataReader.Close();
            }


            // now find number of ITN Gazetteer Sessions
            this.file.WriteLine(" ");
            this.file.WriteLine("Gazetteer Sessions");

            tmpString = new StringBuilder();
            tmpString.Append("Product Description");
            this.file.WriteLine(tmpString);


            selectString =
                "select GT.GTID,GT.GTDescription, isnull(sum(GE.GECount),0) "
                + "from dbo.GazetteerEventsOS GE, dbo.GazetteerType GT "
                + "where GT.GTID = GE.GEGTID "
                + "  and GE.GEDate >= convert(datetime, '" + reportStartDateString
                + "',103) and GE.GEDate <= convert(datetime, '" + reportEndDateString
                + "',103) and (GT.GTDescription like 'Address' or GT.GTDescription like 'Post Code') "
                + "group by GT.GTID,GT.GTDescription order by GT.GTID,GT.GTDescription";

            myDataReader = null;
            mySqlCommand = new SqlCommand(selectString, reportingSqlConnection);
            transactionCount = 0;

            try
            {
                reportingSqlConnection.Open();
                myDataReader = mySqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (myDataReader.Read())
                {
                    productNum = Convert.ToInt32(myDataReader.GetByte(0).ToString());
                    productString = myDataReader.GetString(1);
                    transactionCount = Convert.ToInt32(myDataReader.GetSqlInt32(2).ToString());

                    tmpString = new StringBuilder();
                    tmpString.Append(productString);
                    tmpString.Append(",");
                    tmpString.Append(transactionCount.ToString());
                    this.file.WriteLine(tmpString);

                } // end of while



                this.file.Flush();
                myDataReader.Close();
            }
            catch (Exception e)
            {
                EventLogger.WriteEntry("Failure reading gazetteer events OS table " + e.Message, EventLogEntryType.Error);
                statusCode = (int)StatusCode.ErrorReadingTable; // Failed to reading page events table
            }
            finally
            {
                // Always call Close when done reading.
                if (myDataReader != null)
                    myDataReader.Close();
            }
           
            this.file.Flush();




/************************************************************************************************************/

            this.file.WriteLine(" ");
            this.file.WriteLine("Gazetteer Transactions");

            tmpString = new StringBuilder();
            tmpString.Append("Product Description");
            this.file.WriteLine(tmpString);


            selectString =
                "select GT.GTID,GT.GTDescription, isnull(sum(GE.GECount),0) "
                + "from dbo.GazetteerEvents GE, dbo.GazetteerType GT "
                + "where GT.GTID = GE.GEGTID "
                + "  and GE.GEDate >= convert(datetime, '" + reportStartDateString
                + "',103) and GE.GEDate <= convert(datetime, '" + reportEndDateString
                + "',103) and (GT.GTDescription like 'Address' or GT.GTDescription like 'Post Code') "
                + "group by GT.GTID,GT.GTDescription order by GT.GTID,GT.GTDescription";

            myDataReader = null;
            mySqlCommand = new SqlCommand(selectString, reportingSqlConnection);
            transactionCount = 0;

            try
            {
                reportingSqlConnection.Open();
                myDataReader = mySqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (myDataReader.Read())
                {
                    productNum = Convert.ToInt32(myDataReader.GetByte(0).ToString());
                    productString = myDataReader.GetString(1);
                    transactionCount = Convert.ToInt32(myDataReader.GetSqlInt32(2).ToString());

                    tmpString = new StringBuilder();
                    tmpString.Append(productString);
                    tmpString.Append(",");
                    tmpString.Append(transactionCount.ToString());
                    this.file.WriteLine(tmpString);

                } // end of while



                this.file.Flush();
                myDataReader.Close();
            }
            catch (Exception e)
            {
                EventLogger.WriteEntry("Failure reading gazetteer events table " + e.Message, EventLogEntryType.Error);
                statusCode = (int)StatusCode.ErrorReadingTable; // Failed to reading page events table
            }
            finally
            {
                // Always call Close when done reading.
                if (myDataReader != null)
                    myDataReader.Close();
            }
            this.file.WriteLine(" ");
            
            this.file.Flush();
            /////////////////////////////////////////////////////////////////////////////////////////////////
            /////////////////////////////////////////////////////////////////////////////////////////////////
            // find correct injector transactions
            readOK = controller.ReadProperty("0", "DefaultInjector", out defaultInjectorString);
            if (readOK == false || defaultInjectorString.Length == 0)
            {
                statusCode = (int)StatusCode.ErrorReadingFilePathDefaultInjector; // Error Reading FilePath
                return statusCode;
            }
            //.............................	
            readOK = controller.ReadProperty("0", "BackupInjector", out backupInjectorString);
            if (readOK == false || defaultInjectorString.Length == 0)
            {
                statusCode = (int)StatusCode.ErrorReadingFilePathDefaultInjector; // Error Reading FilePath
                return statusCode;
            }
            //.............................	
            readOK = controller.ReadProperty("0", "TravelineInjector", out travelineInjectorString);
            if (readOK == false || defaultInjectorString.Length == 0)
            {
                statusCode = (int)StatusCode.ErrorReadingFilePathDefaultInjector; // Error Reading FilePath
                return statusCode;
            }
            //.............................	

            //.............................	

            selectString = "select rte.RTERTTID,rtt.rttcode,rte.RTESuccessful,rtt.RTTInjectionFrequency "
                        + "from dbo.ReferenceTransactionEvents rte "
                        + "inner join dbo.ReferenceTransactionType rtt on rtt.rttid = rte.rterttid "
                        + "where RTEDate >= convert(datetime, '" + reportStartDateString
                            + "',103) and RTEDate <= convert(datetime, '" + reportEndDateString
                            + "',103) "
                        + "and RTT.RTTSLAInclude = 1 and RTE.rterttid >= 51 "
                        + "and ( RTE.RTEMachineName not like '" + backupInjectorString + "' "
                        + "and RTE.RTEMachineName not like '" + travelineInjectorString + "' ) "
                        + "order by rtt.rttcode,rte.rtesuccessful"
                        + ",(rte.RTEMsDuration-rtt.RTTAmberMsDuration),(rte.RTEMsDuration-rtt.RTTMaxMsDuration)";

            reportingSqlConnection = new SqlConnection(reportingConnectionString);


            myDataReader = null;
            mySqlCommand = new SqlCommand(selectString, reportingSqlConnection);

            reportingSqlConnection.Open();

            myDataReader = mySqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
            NumTypesToReport = 0;


            for (int i = 0; i < 50; i++)
            {
                NominalCount[i] = 0;
                FailedCount[i] = 0;
            }

            while (myDataReader.Read())
            {
                int id = Convert.ToInt32(myDataReader.GetSqlByte(0).ToString());
                bool isNewtype = true;
                // is this a new type or old
                for (int i = 0; i <= NumTypesToReport; i++)
                {
                    if (RefType[i] == id)
                    {
                        isNewtype = false;
                        i = NumTypesToReport;
                    }
                }
                if (isNewtype)
                {
                    NumTypesToReport++;
                }
                RefType[NumTypesToReport] = id;
                RefTypeCode[NumTypesToReport] = myDataReader.GetString(1);

                int RefPassed = Convert.ToInt32(myDataReader.GetSqlByte(2).ToString());
                if (RefPassed == 0) FailedCount[NumTypesToReport]++;
                int frq = Convert.ToInt32(myDataReader.GetSqlByte(3).ToString());
                int daysToReportOn = 7;
                NominalCount[NumTypesToReport] = (daysToReportOn * 1440 / frq);
            }
            myDataReader.Close();

            int totNominalCount = 0;
            int totFailedCount = 0;





            for (int i = 1; i <= NumTypesToReport; i++)
            {
                totNominalCount += NominalCount[i];
                totFailedCount += FailedCount[i];
            }
            FailedPercent = Math.Round(((((double)totFailedCount * 1000) / totNominalCount) + 0.005) / 10, 2);


            this.file.WriteLine("");
            if (reportNumber == 29)
            {
                this.file.WriteLine("Monthly Transaction Performance");
            }
            else
            {
                this.file.WriteLine("Weekly Transaction Performance");
            }

            this.file.WriteLine("Nominal Count," + totNominalCount.ToString());
            this.file.WriteLine("Failed Count," + totFailedCount.ToString());
            this.file.WriteLine("Failed Percentage," + FailedPercent.ToString());

            this.file.WriteLine("");
            this.file.WriteLine("");
            this.file.Flush();


            ///////////////////////////////////////////////////////////////////////////////////////////

///////////////////////////////////////////////////////////////////////////////////////////

            this.file.WriteLine(" ");
            this.file.WriteLine("Accessibility Transactions");

            tmpString = new StringBuilder();
            tmpString.Append("Type, Count");
            this.file.WriteLine(tmpString);

            selectString =
                "select AT.AETID,AT.AETDescription, isnull(sum(AE.AECount),0) "
                + "from dbo.AccessibleEvents AE, dbo.AccessibleEventType AT "
                + "where AT.AETID = AE.AETID "
                + "  and AE.AEDate >= convert(datetime, '" + reportStartDateString
                + "',103) and AE.AEDate <= convert(datetime, '" + reportEndDateString
                + "',103) group by AT.AETID,AT.AETDescription order by AT.AETID,AT.AETDescription";


            myDataReader = null;
            mySqlCommand = new SqlCommand(selectString, reportingSqlConnection);
            transactionCount = 0;

            try
            {
                reportingSqlConnection.Open();
                myDataReader = mySqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (myDataReader.Read())
                {
                    productNum = Convert.ToInt32(myDataReader.GetSqlInt32(0).ToString());
                    productString = myDataReader.GetString(1);
                    transactionCount = Convert.ToInt32(myDataReader.GetSqlInt32(2).ToString());

                    tmpString = new StringBuilder();
                    tmpString.Append(productString);
                    tmpString.Append(",");
                    tmpString.Append(transactionCount.ToString());
                    this.file.WriteLine(tmpString);

                } // end of while



                this.file.Flush();
                myDataReader.Close();
            }
            catch (Exception e)
            {
                EventLogger.WriteEntry("Failure reading Accessibility events table " + e.Message, EventLogEntryType.Error);
                statusCode = (int)StatusCode.ErrorReadingTable; // Failed to reading page events table
            }
            finally
            {
                // Always call Close when done reading.
                if (myDataReader != null)
                    myDataReader.Close();
            }


            this.file.Flush();



            /*---------------------------------------------------------------------------------------------------------*/
            this.file.WriteLine(" ");
            this.file.WriteLine("Geospatial calls for accessible locations");

            tmpString = new StringBuilder();
            tmpString.Append("Type, Count");
            this.file.WriteLine(tmpString);


            selectString =
                "select GQET.GQETID,GQET.GQETDescription, isnull(sum(GQECount),0) "
                + "from dbo.GISQueryEvents GQE, dbo.GISQueryEventType GQET "
                + "where GQET.GQETID = GQE.GQEGQETID "
                + " and  GQET.GQETCode in ('IsPointInAccessibleLocation','FindNearestAccessibleStops','FindNearestAccessibleLocalities')"
                + "  and GQE.GQEDate >= convert(datetime, '" + reportStartDateString
                + "',103) and GQE.GQEDate <= convert(datetime, '" + reportEndDateString
                + "',103) group by GQET.GQETID,GQET.GQETDescription order by GQET.GQETID,GQET.GQETDescription";

            myDataReader = null;
            mySqlCommand = new SqlCommand(selectString, reportingSqlConnection);
            transactionCount = 0;

            try
            {
                reportingSqlConnection.Open();
                myDataReader = mySqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (myDataReader.Read())
                {
                    productNum = Convert.ToInt32(myDataReader.GetSqlInt32(0).ToString());
                    productString = myDataReader.GetString(1);
                    transactionCount = Convert.ToInt32(myDataReader.GetSqlInt32(2).ToString());

                    tmpString = new StringBuilder();
                    tmpString.Append(productString);
                    tmpString.Append(",");
                    tmpString.Append(transactionCount.ToString());
                    this.file.WriteLine(tmpString);

                } // end of while



                this.file.Flush();
                myDataReader.Close();
            }
            catch (Exception e)
            {
                EventLogger.WriteEntry("Failure reading GISQuery events table " + e.Message, EventLogEntryType.Error);
                statusCode = (int)StatusCode.ErrorReadingTable; // Failed to reading page events table
            }
            finally
            {
                // Always call Close when done reading.
                if (myDataReader != null)
                    myDataReader.Close();
            }


            this.file.Flush();


//////////////////////////////////////////////////////////////////////////////////////////






            //********************************************
            //			Now do same again accounting for the test calendar

            transactionCount = 0;

            if (testCount == 0)
            {
                this.file.WriteLine(" ");
                this.file.WriteLine(" ");
            }
            else
            {
                this.file.WriteLine("The above mapping figures include periods of testing where maps have been auto-generated but not actually viewed by anybody.");
                this.file.WriteLine("The figures below have been adjusted to ignore these testing periods and should be used as an indication of actual user requests for charging purposes.");

                reportingSqlConnection = new SqlConnection(reportingConnectionString);
                reportingSqlConnection.Open();
                // build exclusion string
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


                    ScompareString = (long)SDate.Year * 1000000L + ((long)SDate.Month * 10000L) + ((long)SDate.Day * 100L) + (long)SHour; //+(long)SHourQuarter;
                    EcompareString = (long)EDate.Year * 1000000L + ((long)EDate.Month * 10000L) + ((long)EDate.Day * 100L) + (long)EHour;//+(long)EHourQuarter;

                    // build exclusion string - nothing allowed within this 			
                    excludeString.Append(" and not (");
                    excludeString.Append("(year(ME.MEDate)*1000000+month(ME.MEDate)*10000+day(ME.MEDate)*100+ME.MEHour) >= " + ScompareString);
                    excludeString.Append(" and (year(ME.MEDate)*1000000+month(ME.MEDate)*10000+day(ME.MEDate)*100+ME.MEHour) <= " + EcompareString);
                    excludeString.Append(")");
                }


                transactionCount = 0;
                averageTime = 0;
                totalVector = 0;
                totalRaster = 0;

                this.file.WriteLine("");
                if (reportNumber == 29)
                {
                    this.file.WriteLine("Monthly Map Transactions (Adjusted for test periods)");
                }
                else
                {
                    this.file.WriteLine("Weekly Map Transactions (Adjusted for test periods)");
                }
                this.file.WriteLine("");

                this.file.Flush();

                reportingSqlConnection = new SqlConnection(reportingConnectionString);

                selectString =
                    "select ME.MEMDTID,MDT.MDTDescription,ME.MEMCTID,MCT.MCTDescription, isnull(sum(ME.MECount),0),avg(ME.MEAvMsDuration)"
                    + "from dbo.MapEvents ME, dbo.MapCommandType MCT, dbo.MapDisplayType MDT "
                    + "where ME.MEMCTID = MCT.MCTID and ME.MEMDTID = MDT.MDTID "
                    + "  and ME.MEDate >= convert(datetime, '" + reportStartDateString
                    + "',103) and ME.MEDate <= convert(datetime, '" + reportEndDateString
                    + "',103) "
                    + excludeString
                    + " group by ME.MEMDTID,MDT.MDTDescription,ME.MEMCTID,MCT.MCTDescription order by ME.MEMDTID,MDT.MDTDescription,ME.MEMCTID,MCT.MCTDescription";

                myDataReader = null;
                mySqlCommand = new SqlCommand(selectString, reportingSqlConnection);
                transactionCount = 0;

                try
                {
                    reportingSqlConnection.Open();
                    myDataReader = mySqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
                    while (myDataReader.Read())
                    {
                        productNum = Convert.ToInt32(myDataReader.GetByte(0).ToString());
                        productString = myDataReader.GetString(1);
                        commandNum = Convert.ToInt32(myDataReader.GetByte(2).ToString());
                        commandString = myDataReader.GetString(3);
                        transactionCount = Convert.ToInt32(myDataReader.GetSqlInt32(4).ToString());
                        averageTime = Convert.ToInt32(myDataReader.GetSqlInt32(5).ToString());

                        mapCommand[commandNum] = commandString;
                        mapDisplay[productNum] = productString;
                        MapCmdCount[productNum, commandNum] = transactionCount;
                        MapCmdTime[productNum, commandNum] = averageTime;


                    } // end of while

                    tmpString = new StringBuilder();
                    tmpString.Append("Raster Maps");
                    this.file.WriteLine(tmpString);

                    tmpString = new StringBuilder();
                    tmpString.Append("Product(Scale),Total InitialDisplay/Zooms/Pans");
                    this.file.WriteLine(tmpString);

                    int i;
                    for (i = 1; i <= 2; i++)
                    {

                        tmpString = new StringBuilder();
                        switch (i)
                        {
                            case 1:
                                tmpString.Append("OS Street View <1:10000 Scale");
                                break;
                            case 2:
                                tmpString.Append("OS 1/50th Raster <1:50000 Scale");
                                break;
                            case 3:
                                tmpString.Append("OS Meridian 2 <1:250000 Scale");
                                break;
                            case 4:
                                tmpString.Append("OS Meridian 2 <1:1000000 Scale");
                                break;
                            case 5:
                                tmpString.Append("OS Strategi >1:1000000 Scale");
                                break;
                        }
                        tmpString.Append(",");

                        tmpString.Append(MapCmdCount[i, 1] + MapCmdCount[i, 2] + MapCmdCount[i, 3] + MapCmdCount[i, 5]);
                        this.file.WriteLine(tmpString);

                        totalRaster = totalRaster + MapCmdCount[i, 1] + MapCmdCount[i, 2] + MapCmdCount[i, 3] + MapCmdCount[i, 5];


                    }

                    this.file.WriteLine(" ");
                  

                    tmpString = new StringBuilder();
                    tmpString.Append("Vector Maps");
                    this.file.WriteLine(tmpString);

                    tmpString = new StringBuilder();
                    tmpString.Append("Product(Scale),Total InitialDisplay/Zooms/Pans");
                    this.file.WriteLine(tmpString);

                    for (i = 3; i <= 5; i++)
                    {

                        tmpString = new StringBuilder();
                        switch (i)
                        {
                            case 1:
                                tmpString.Append("OS Street View <1:10000 Scale");
                                break;
                            case 2:
                                tmpString.Append("OS 1/50th Raster <1:50000 Scale");
                                break;
                            case 3:
                                tmpString.Append("OS Meridian 2 <1:250000 Scale");
                                break;
                            case 4:
                                tmpString.Append("OS Meridian 2 <1:1000000 Scale");
                                break;
                            case 5:
                                tmpString.Append("OS Strategi >1:1000000 Scale");
                                break;
                        }
                        tmpString.Append(",");

                        tmpString.Append(MapCmdCount[i, 1] + MapCmdCount[i, 2] + MapCmdCount[i, 3] + MapCmdCount[i, 5]);
                        this.file.WriteLine(tmpString);

                        totalVector = totalVector + MapCmdCount[i, 1] + MapCmdCount[i, 2] + MapCmdCount[i, 3] + MapCmdCount[i, 5];


                    }


                    this.file.Flush();
                    myDataReader.Close();
                }
                catch (Exception e)
                {
                    EventLogger.WriteEntry("Failure reading map events table " + e.Message, EventLogEntryType.Error);
                    statusCode = (int)StatusCode.ErrorReadingTable; // Failed to reading map events table
                }
                finally
                {
                    // Always call Close when done reading.
                    if (myDataReader != null)
                        myDataReader.Close();
                }
                      

                //.............................			
                this.file.Flush();


                
                //*************************************************************************************************************************
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
                    ScompareString = ((long)SDate.Year - 2000) * 100000000L + ((long)SDate.Month * 1000000L) + ((long)SDate.Day * 10000L) + (long)SHour * 100L + (long)SMin;
                    EcompareString = ((long)EDate.Year - 2000) * 100000000L + ((long)EDate.Month * 1000000L) + ((long)EDate.Day * 10000L) + (long)EHour * 100L + (long)EMin;

                    // build exclusion string - nothing allowed within this 			
                    excludeString.Append(" and not (");
                    excludeString.Append("((year(GPEDate)-2000)*100000000+month(GPEDate)*1000000+day(GPEDate)*10000+GPEHour*100+GPEHourQuarter) >= " + ScompareString);
                    excludeString.Append(" and ((year(GPEDate)-2000)*100000000+month(GPEDate)*1000000+day(GPEDate)*10000+GPEHour*100+GPEHourQuarter) <= " + EcompareString);
                    excludeString.Append(")");
                }



                selectString =
                   "select  isnull(sum(GPECount),0)"
                   + "from dbo.GradientProfileEvents "
                   + "where  GPEDate >= convert(datetime, '" + reportStartDateString
                   + "',103) and GPEDate <= convert(datetime, '" + reportEndDateString
                   + "',103) and GPEGPDTID = 2 "
                   + excludeString;


                GetPeriodTotal();
                totalChartView = periodTotal;
                selectString =
                   "select  isnull(sum(GPECount),0)"
                   + "from dbo.GradientProfileEvents "
                   + "where  GPEDate >= convert(datetime, '" + reportStartDateString
                   + "',103) and GPEDate <= convert(datetime, '" + reportEndDateString
                   + "',103) and GPEGPDTID = 3"
                      + excludeString;

                GetPeriodTotal();
                totalTableView = periodTotal;
                this.file.WriteLine(" ");
                this.file.WriteLine("Gradient Profile Lookups ");
                this.file.WriteLine("WebServiceRequests ");
                this.file.WriteLine("Chart View," + totalChartView.ToString());
                this.file.WriteLine("Table View," + totalTableView.ToString());
                this.file.WriteLine(" ");
                this.file.Flush();




                // now find number of ITN routing transactions

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
                    ScompareString = ((long)SDate.Year - 2000) * 100000000L + ((long)SDate.Month * 1000000L) + ((long)SDate.Day * 10000L) + (long)SHour * 100L + (long)SMin;
                    EcompareString = ((long)EDate.Year - 2000) * 100000000L + ((long)EDate.Month * 1000000L) + ((long)EDate.Day * 10000L) + (long)EHour * 100L + (long)EMin;

                    // build exclusion string - nothing allowed within this 			
                    excludeString.Append(" and not (");
                    excludeString.Append("((year(RPEDate)-2000)*100000000+month(RPEDate)*1000000+day(RPEDate)*10000+RPEHour*100+RPEHourQuarter) >= " + ScompareString);
                    excludeString.Append(" and ((year(RPEDate)-2000)*100000000+month(RPEDate)*1000000+day(RPEDate)*10000+RPEHour*100+RPEHourQuarter) <= " + EcompareString);
                    excludeString.Append(")");
                }


                selectString =
                    "select SUM(RPECount),RPECongestedRoute "
                    + "from dbo.RoadPlanEvents "
                    + "where RPEDate >= convert(datetime, '" + reportStartDateString
                    + "',103) and RPEDate <= convert(datetime, '" + reportEndDateString
                    + "',103) and RPECongestedRoute = 1 "
                    + excludeString;


                myDataReader = null;
                mySqlCommand = new SqlCommand(selectString, reportingSqlConnection);
                transactionCount = 0;
                congested = false;
                totalTrans = 0;
                try
                {
                    reportingSqlConnection.Open();
                    myDataReader = mySqlCommand.ExecuteReader(CommandBehavior.CloseConnection);

                    tmpString = new StringBuilder();
                    tmpString.Append("ITN Routing Transactions");
                    this.file.WriteLine(tmpString);

                    while (myDataReader.Read())
                    {
                        transactionCount = Convert.ToInt32(myDataReader.GetSqlInt32(0).ToString());
                        totalTrans = totalTrans + transactionCount;
                        congested = myDataReader.GetBoolean(1);
                        if (congested == true)
                        {
                            tmpString = new StringBuilder();
                            tmpString.Append("Adjusted Route,");
                            tmpString.Append(transactionCount.ToString());
                            this.file.WriteLine(tmpString);
                        }
                    } // end of while

                    



                    this.file.Flush();
                    myDataReader.Close();
                }
                catch (Exception e)
                {
                    EventLogger.WriteEntry("Failure reading RoadPlanEvents table " + e.Message, EventLogEntryType.Error);
                    statusCode = (int)StatusCode.ErrorReadingTable; // Failed to reading page events table
                }
                finally
                {
                    // Always call Close when done reading.
                    if (myDataReader != null)
                        myDataReader.Close();
                }


                this.file.WriteLine(" ");
                this.file.WriteLine("Gazetteer Transactions");

                tmpString = new StringBuilder();
                tmpString.Append("Product Description");
                this.file.WriteLine(tmpString);
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

                    ScompareString = ((long)SDate.Year - 2000) * 100000000L + ((long)SDate.Month * 1000000L) + ((long)SDate.Day * 10000L) + (long)SHour * 100L + SMin; //+(long)SHourQuarter;
                    EcompareString = ((long)EDate.Year - 2000) * 100000000L + ((long)EDate.Month * 1000000L) + ((long)EDate.Day * 10000L) + (long)EHour * 100L + EMin;//+(long)EHourQuarter;

                    // build exclusion string - nothing allowed within this 			
                    excludeString.Append(" and not (");
                    excludeString.Append("((year(GE.GEDate)-2000)*100000000+month(GE.GEDate)*1000000+day(GE.GEDate)*10000+GE.GEHour*100+GE.GEHourQuarter) >= " + ScompareString);
                    excludeString.Append(" and ((year(GE.GEDate)-2000)*100000000+month(GE.GEDate)*1000000+day(GE.GEDate)*10000+GE.GEHour*100+GE.GEHourQuarter) <= " + EcompareString);
                    excludeString.Append(")");
                }


                selectString =
                    "select GT.GTID,GT.GTDescription, isnull(sum(GE.GECount),0) "
                    + "from dbo.GazetteerEvents GE, dbo.GazetteerType GT "
                    + "where GT.GTID = GE.GEGTID "
                    + "  and GE.GEDate >= convert(datetime, '" + reportStartDateString
                    + "',103) and GE.GEDate <= convert(datetime, '" + reportEndDateString
                    + "',103) "
                    + excludeString
                    +" and (GT.GTDescription like 'Address' or GT.GTDescription like 'Post Code') "
                    + "group by GT.GTID,GT.GTDescription order by GT.GTID,GT.GTDescription";


                myDataReader = null;
                mySqlCommand = new SqlCommand(selectString, reportingSqlConnection);
                transactionCount = 0;

                try
                {
                    reportingSqlConnection.Open();
                    myDataReader = mySqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
                    while (myDataReader.Read())
                    {
                        productNum = Convert.ToInt32(myDataReader.GetByte(0).ToString());
                        productString = myDataReader.GetString(1);
                        transactionCount = Convert.ToInt32(myDataReader.GetSqlInt32(2).ToString());

                        tmpString = new StringBuilder();
                        tmpString.Append(productString);
                        tmpString.Append(",");
                        tmpString.Append(transactionCount.ToString());
                        this.file.WriteLine(tmpString);

                    } // end of while



                    this.file.Flush();
                    myDataReader.Close();
                }
                catch (Exception e)
                {
                    EventLogger.WriteEntry("Failure reading gazetteer events table " + e.Message, EventLogEntryType.Error);
                    statusCode = (int)StatusCode.ErrorReadingTable; // Failed to reading page events table
                }
                finally
                {
                    // Always call Close when done reading.
                    if (myDataReader != null)
                        myDataReader.Close();
                }

                this.file.WriteLine(" ");
                

                this.file.Flush();


/////////////////////////////////////////////////////////////////////////////////////////////////
/////////////////////////////////////////////////////////////////////////////////////////////////
            /////////////////////////////////////////////////////////////////////////////////////////////////
            // find correct injector transactions
            readOK = controller.ReadProperty("0", "DefaultInjector", out defaultInjectorString);
            if (readOK == false || defaultInjectorString.Length == 0)
            {
                statusCode = (int)StatusCode.ErrorReadingFilePathDefaultInjector; // Error Reading FilePath
                return statusCode;
            }
            //.............................	
            readOK = controller.ReadProperty("0", "BackupInjector", out backupInjectorString);
            if (readOK == false || defaultInjectorString.Length == 0)
            {
                statusCode = (int)StatusCode.ErrorReadingFilePathDefaultInjector; // Error Reading FilePath
                return statusCode;
            }
            //.............................	
            readOK = controller.ReadProperty("0", "TravelineInjector", out travelineInjectorString);
            if (readOK == false || defaultInjectorString.Length == 0)
            {
                statusCode = (int)StatusCode.ErrorReadingFilePathDefaultInjector; // Error Reading FilePath
                return statusCode;
            }
            //.............................	

            //.............................	

            selectString = "select rte.RTERTTID,rtt.rttcode,rte.RTESuccessful,rtt.RTTInjectionFrequency "
                        + "from dbo.ReferenceTransactionEvents rte "
                        + "inner join dbo.ReferenceTransactionType rtt on rtt.rttid = rte.rterttid "
                        + "where RTEDate >= convert(datetime, '" + reportStartDateString
                            + "',103) and RTEDate <= convert(datetime, '" + reportEndDateString
                            + "',103) "
                        + "and RTT.RTTSLAInclude = 1 and RTE.rterttid >= 51 "
                        + "and ( RTE.RTEMachineName not like '" + backupInjectorString + "' "
                        + "and RTE.RTEMachineName not like '" + travelineInjectorString + "' ) "
                        + "order by rtt.rttcode,rte.rtesuccessful"
                        + ",(rte.RTEMsDuration-rtt.RTTAmberMsDuration),(rte.RTEMsDuration-rtt.RTTMaxMsDuration)";

            reportingSqlConnection = new SqlConnection(reportingConnectionString);


            myDataReader = null;
            mySqlCommand = new SqlCommand(selectString, reportingSqlConnection);

            reportingSqlConnection.Open();

            myDataReader = mySqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
            NumTypesToReport = 0;
            

            for (int i = 0; i < 50; i++)
            {
                NominalCount[i] = 0;
                FailedCount[i] = 0;
             }

            while (myDataReader.Read())
            {
                int id = Convert.ToInt32(myDataReader.GetSqlByte(0).ToString());
                bool isNewtype = true;
                // is this a new type or old
                for (int i = 0; i <= NumTypesToReport; i++)
                {
                    if (RefType[i] == id)
                    {
                        isNewtype = false;
                        i = NumTypesToReport;
                    }
                }
                if (isNewtype)
                {
                    NumTypesToReport++;
                }
                RefType[NumTypesToReport] = id;
                RefTypeCode[NumTypesToReport] = myDataReader.GetString(1);
                
                int RefPassed = Convert.ToInt32(myDataReader.GetSqlByte(2).ToString());
                if (RefPassed == 0) FailedCount[NumTypesToReport]++;
                int frq = Convert.ToInt32(myDataReader.GetSqlByte(3).ToString());
                int daysToReportOn = 7;
                NominalCount[NumTypesToReport] = (daysToReportOn * 1440 / frq);
            }
            myDataReader.Close();

             totNominalCount = 0;
             totFailedCount = 0;

            
          


            for (int i = 1; i <= NumTypesToReport; i++)
            {
                totNominalCount +=NominalCount[i];
                totFailedCount += FailedCount[i];
            }
            FailedPercent = Math.Round(((((double)totFailedCount * 1000) / totNominalCount) + 0.005) / 10,2);

               
                this.file.WriteLine("");
                if (reportNumber == 29)
                {
                    this.file.WriteLine("Monthly Transaction Performance");
                }
                else
                {
                    this.file.WriteLine("Weekly Transaction Performance");
                }
               
                this.file.WriteLine("Nominal Count," + totNominalCount.ToString());
                this.file.WriteLine("Failed Count," + totFailedCount.ToString());
                this.file.WriteLine("Failed Percentage," + FailedPercent.ToString());
                
                this.file.WriteLine("");
                this.file.WriteLine("");
                this.file.Flush();


                ///////////////////////////////////////////////////////////////////////////////////////////

                this.file.WriteLine(" ");
                this.file.WriteLine("Accessibility Transactions");

                tmpString = new StringBuilder();
                tmpString.Append("Type, Count");
                this.file.WriteLine(tmpString);
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

                    ScompareString = ((long)SDate.Year - 2000) * 100000000L + ((long)SDate.Month * 1000000L) + ((long)SDate.Day * 10000L) + (long)SHour * 100L + SMin; //+(long)SHourQuarter;
                    EcompareString = ((long)EDate.Year - 2000) * 100000000L + ((long)EDate.Month * 1000000L) + ((long)EDate.Day * 10000L) + (long)EHour * 100L + EMin;//+(long)EHourQuarter;

                    // build exclusion string - nothing allowed within this 			
                    excludeString.Append(" and not (");
                    excludeString.Append("((year(AE.AEDate)-2000)*100000000+month(AE.AEDate)*1000000+day(AE.AEDate)*10000+AE.AEHour*100+AE.AEHourQuarter) >= " + ScompareString);
                    excludeString.Append(" and ((year(AE.AEDate)-2000)*100000000+month(AE.AEDate)*1000000+day(AE.AEDate)*10000+AE.AEHour*100+AE.AEHourQuarter) <= " + EcompareString);
                    excludeString.Append(")");
                }

                selectString =
                    "select AT.AETID,AT.AETDescription, isnull(sum(AE.AECount),0) "
                    + "from dbo.AccessibleEvents AE, dbo.AccessibleEventType AT "
                    + "where AT.AETID = AE.AETID "
                    + "  and AE.AEDate >= convert(datetime, '" + reportStartDateString
                    + "',103) and AE.AEDate <= convert(datetime, '" + reportEndDateString
                    + "',103) "
                    + excludeString
                    + " group by AT.AETID,AT.AETDescription order by AT.AETID,AT.AETDescription";


                myDataReader = null;
                mySqlCommand = new SqlCommand(selectString, reportingSqlConnection);
                transactionCount = 0;

                try
                {
                    reportingSqlConnection.Open();
                    myDataReader = mySqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
                    while (myDataReader.Read())
                    {
                        productNum = Convert.ToInt32(myDataReader.GetSqlInt32(0).ToString());
                        productString = myDataReader.GetString(1);
                        transactionCount = Convert.ToInt32(myDataReader.GetSqlInt32(2).ToString());

                        tmpString = new StringBuilder();
                        tmpString.Append(productString);
                        tmpString.Append(",");
                        tmpString.Append(transactionCount.ToString());
                        this.file.WriteLine(tmpString);

                    } // end of while



                    this.file.Flush();
                    myDataReader.Close();
                }
                catch (Exception e)
                {
                    EventLogger.WriteEntry("Failure reading Accessibility events table " + e.Message, EventLogEntryType.Error);
                    statusCode = (int)StatusCode.ErrorReadingTable; // Failed to reading page events table
                }
                finally
                {
                    // Always call Close when done reading.
                    if (myDataReader != null)
                        myDataReader.Close();
                }


                this.file.Flush();



                /*---------------------------------------------------------------------------------------------------------*/
                this.file.WriteLine(" ");
                this.file.WriteLine("Geospatial calls for accessible locations");

                tmpString = new StringBuilder();
                tmpString.Append("Type, Count");
                this.file.WriteLine(tmpString);
                this.file.WriteLine(tmpString);
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

                    ScompareString = ((long)SDate.Year - 2000) * 100000000L + ((long)SDate.Month * 1000000L) + ((long)SDate.Day * 10000L) + (long)SHour * 100L + SMin; //+(long)SHourQuarter;
                    EcompareString = ((long)EDate.Year - 2000) * 100000000L + ((long)EDate.Month * 1000000L) + ((long)EDate.Day * 10000L) + (long)EHour * 100L + EMin;//+(long)EHourQuarter;

                    // build exclusion string - nothing allowed within this 			
                    excludeString.Append(" and not (");
                    excludeString.Append("((year(GQE.GQEDate)-2000)*100000000+month(GQE.GQEDate)*1000000+day(AE.AEDate)*10000+GQE.GQEHour*100+GQE.GQEHourQuarter) >= " + ScompareString);
                    excludeString.Append(" and ((year(GQE.GQEDate)-2000)*100000000+month(GQE.GQEDate)*1000000+day(AE.AEDate)*10000+GQE.GQEHour*100+GQE.GQEHourQuarter) <= " + EcompareString);
                    excludeString.Append(")");
                }

                selectString =
                    "select GQET.GQETID,GQET.GQETDescription, isnull(sum(GQECount),0) "
                    + "from dbo.GISQueryEvents GQE, dbo.GISQueryEventType GQET "
                    + "where GQET.GQETID = GQE.GQEGQETID "
                    + " and  GQET.GQETCode in ('IsPointInAccessibleLocation','FindNearestAccessibleStops','FindNearestAccessibleLocalities')"
                    + "  and GQE.GQEDate >= convert(datetime, '" + reportStartDateString
                    + "',103) and GQE.GQEDate <= convert(datetime, '" + reportEndDateString
                    + "',103) "
                    + excludeString
                    + " group by GQET.GQETID,GQET.GQETDescription order by GQET.GQETID,GQET.GQETDescription";

                myDataReader = null;
                mySqlCommand = new SqlCommand(selectString, reportingSqlConnection);
                transactionCount = 0;

                try
                {
                    reportingSqlConnection.Open();
                    myDataReader = mySqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
                    while (myDataReader.Read())
                    {
                        productNum = Convert.ToInt32(myDataReader.GetSqlInt32(0).ToString());
                        productString = myDataReader.GetString(1);
                        transactionCount = Convert.ToInt32(myDataReader.GetSqlInt32(2).ToString());

                        tmpString = new StringBuilder();
                        tmpString.Append(productString);
                        tmpString.Append(",");
                        tmpString.Append(transactionCount.ToString());
                        this.file.WriteLine(tmpString);

                    } // end of while



                    this.file.Flush();
                    myDataReader.Close();
                }
                catch (Exception e)
                {
                    EventLogger.WriteEntry("Failure reading GISQuery events table " + e.Message, EventLogEntryType.Error);
                    statusCode = (int)StatusCode.ErrorReadingTable; // Failed to reading page events table
                }
                finally
                {
                    // Always call Close when done reading.
                    if (myDataReader != null)
                        myDataReader.Close();
                }


                this.file.Flush();

            }


            //////////////////////////////////////////////////////////////////////////////////////////////////////////////
  
///////////////////////////////////////////////////////////////////////////////////////////////////













      
			this.file.Close();

			string from = "";
			string to = "";
			string smtpServer = "";
			string subject = "";


            readOK = controller.ReadProperty(reportNumber.ToString(), "FilePath", out filePathString);
			if (readOK == false || filePathString.Length == 0)
			{
				statusCode = (int)StatusCode.ErrorReadingFilePath; // Error Reading FilePath
				return statusCode;
			}
            filePathString = filePathString.Replace("%YY-MM-DD%", RequestedDateString);
            filePathString = filePathString.Replace("%YY-MM%", RequestedDateString);

			readOK =  controller.ReadProperty("0", "MailAddress", out from);
			if (readOK == false || from.Length == 0)
			{
				statusCode = (int)StatusCode.ErrorReadingSenderEmailAddress; // Error Reading sender email address
				return statusCode;
			}
            readOK = controller.ReadProperty(reportNumber.ToString(), "MailRecipient", out to);
			if (readOK == false || to.Length == 0)
			{
				statusCode = (int)StatusCode.ErrorReadingRecipientEmailAddress; // Error Reading recipient email address
				return statusCode;
			}
            readOK = controller.ReadProperty(reportNumber.ToString(), "Title", out subject);
			if (readOK == false || subject.Length == 0)
			{
				subject = "DfT Weekly/Monthly Summary Report "+RequestedDateString;
			}
            subject = subject.Replace("%YY-MM-DD%", RequestedDateString);
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



        void DailyEventBreakDown()
        {
            DateTime thisDay;
            string dayOfWeek;
            if (transactionString.CompareTo("User Web Page Requests (Total WorkLoad)") == 0)
            {
                selectString = "select WEDate, isnull(sum(WECount),0)from dbo.WorkloadEvents "
                          + "where WEDate >= convert(datetime, '" + reportStartDateString
                          + "',103) and WEDate <= convert(datetime, '" + reportEndDateString
                          + "',103) group by WEDate order by WEDate";
            }
            else
            {

                selectString = "select SEDate, isnull(sum(SECount),0)from dbo.SessionEvents "
        + "where SEDate >= convert(datetime, '" + reportStartDateString
        + "',103) and SEDate <= convert(datetime, '" + reportEndDateString
        + "',103) group by SEDate order by SEDate";
            }

            int[] dayTot = new int[32];
            for (int i = 0; i <= 31; i++) dayTot[i] = 0;
            myDataReader = null;
            mySqlCommand = new SqlCommand(selectString, reportingSqlConnection);
          
            int day = 0;
            int monthlyTotal = 0;
            int transactionCount = 0;
            try
            {
                reportingSqlConnection.Open();
                myDataReader = mySqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (myDataReader.Read())
                {

                    thisDay = myDataReader.GetDateTime(0);
                    transactionCount = Convert.ToInt32(myDataReader.GetSqlInt32(1).ToString());

                    if (frequency.CompareTo("W") == 0)
                    {
                        dayOfWeek = thisDay.DayOfWeek.ToString();
                        switch (dayOfWeek)
                        {
                            case "Monday":
                                day = 1;

                                break;
                            case "Tuesday":
                                day = 2;
                                break;
                            case "Wednesday":
                                day = 3;
                                break;
                            case "Thursday":
                                day = 4;
                                break;
                            case "Friday":
                                day = 5;
                                break;
                            case "Saturday":
                                day = 6;
                                break;
                            case "Sunday":
                                day = 7;
                                break;

                        }



                    }
                    else
                    {
                        day = thisDay.Day;
                    }
                    
                    monthlyTotal += transactionCount;

                    dayTot[day] = dayTot[day] + transactionCount;


                    if (transactionString.CompareTo("User Web Page Requests (Total WorkLoad)") == 0)
                    {
                        WorkLoad_dayTot[day] = WorkLoad_dayTot[day] + transactionCount;
                    }
                    if (transactionString.CompareTo("User Sessions") == 0)
                    {
                        UserSessions_dayTot[day] = UserSessions_dayTot[day] + transactionCount;
                    }




                } // end of while

                this.file.Flush();
                myDataReader.Close();

            }
            catch (Exception e)
            {
                EventLogger.WriteEntry("Failure reading table " + transactionString + "  " + e.Message, EventLogEntryType.Error);
                statusCode = (int)StatusCode.ErrorReadingFilePath; // Failed to reading page events table
            }
            finally
            {
                // Always call Close when done reading.
                if (myDataReader != null)
                    myDataReader.Close();
            }
        }



        void GetPeriodTotal()
        {
            periodTotal = 0;
            tmpString = new StringBuilder();
            tmpString.Append(transactionString);
            myDataReader = null;
            mySqlCommand = new SqlCommand(selectString, reportingSqlConnection);
            int transactionCount = 0;
          
            try
            {
                reportingSqlConnection.Open();
                myDataReader = mySqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (myDataReader.Read())
                {
                    transactionCount = Convert.ToInt32(myDataReader.GetSqlInt32(0).ToString());
          
                    periodTotal = periodTotal + transactionCount;
          
                } // end of while


                myDataReader.Close();

             }
            catch (Exception e)
            {
                EventLogger.WriteEntry("Error reading " + transactionString + " " + e.Message, EventLogEntryType.Error);

                statusCode = (int)StatusCode.ErrorReadingTable; // Failed reading table
            }
            finally
            {
                // Always call Close when done reading.
                if (myDataReader != null)
                    myDataReader.Close();
            }
        }




    



        void GetPeriodTotalWithDesc()
        {

            
            myDataReader = null;
            mySqlCommand = new SqlCommand(selectString, reportingSqlConnection);
            int transactionCount = 0;
            int pos = 0;
            String title = "";
            try
            {
                reportingSqlConnection.Open();
                myDataReader = mySqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (myDataReader.Read())
                {
                    tmpString = new StringBuilder();
                    title = myDataReader.GetString(0);
                    transactionCount = Convert.ToInt32(myDataReader.GetSqlInt32(1).ToString());
                    pos++;

                    if (transactionString == "Top 5 Partner Totals")
                    {


                         switch (pos)
                              {
                                  case 1:
                                      tmpString.Append("1st ," + title + " " + transactionCount.ToString());
                                      break;
                                  case 2:
                                      tmpString.Append("2nd ," + title + " " + transactionCount.ToString());
                                      break;
                                  case 3:
                                      tmpString.Append("3rd ," + title + " " + transactionCount.ToString());
                                      break;
                                  case 4:
                                      tmpString.Append("4th ," + title + " " + transactionCount.ToString());
                                      break;
                                  case 5:
                                      tmpString.Append("5th ," + title + " " + transactionCount.ToString());
                                      break;
                                  case 6:
                                      tmpString.Append(" ," + title + " " + transactionCount.ToString());
                                      break;
                              }
                     
                    }
                    else
                    {
                        tmpString.Append(title);
                        tmpString.Append(",");
                        tmpString.Append(transactionCount.ToString());
                    }
                    this.file.WriteLine(tmpString.ToString());
                } // end of while

               

                this.file.Flush();
                myDataReader.Close();

            }
            catch (Exception e)
            {
                EventLogger.WriteEntry("Failure reading table " + transactionString + "  " + e.Message, EventLogEntryType.Error);
                statusCode = (int)StatusCode.ErrorReadingFilePath; // Failed to reading page events table
            }
            finally
            {
                // Always call Close when done reading.
                if (myDataReader != null)
                    myDataReader.Close();
            }
        }


         
    
    
      

	}
}
