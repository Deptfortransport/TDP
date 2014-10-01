///<header>
///DFTWeeklyCombinedReport.cs
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
	/// Class to Weekly DFT Combined Report
	/// </summary>
	/// 
	
	public class DFTWeeklyCombinedReport
	{
		private DateTime currentDateTime;
		private string reportStartDateString;
		private string reportEndDateString;
        private DateTime reportEndDatePlus1;
		private string reportEndDatePlus1String;
		private DateTime reportStartDate;
		private DateTime reportEndDate;
        DateTime thisDay;
        string dayOfWeek;
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
		private string filePathString;
		private int statusCode;
        private int transactionCount;
        private DateTime[] TestStarted = new DateTime[250];
        private DateTime[] TestCompleted = new DateTime[250];
        private int testCount;
        private DateTime SDate;
        private DateTime EDate;
        private int SHour, EHour, SMin, EMin;
        private long ScompareString, EcompareString;
        private StringBuilder excludeString;
        private int[,] MapCmdCount = new int[6, 6];
        private int[,] MapCmdTime = new int[6, 6];
        private string[] mapCommand = new string[6];
        private string[] mapDisplay = new string[6];
        private int totalGP = 0;
        private int totalChartView = 0;
        private int totalTableView = 0;
        private string[] splitString = new string[10];
        private StringBuilder tmpString1;
        private StringBuilder tmpString2;
        private StringBuilder tmpString3;
        private StringBuilder tmpString4;
		private int[] WorkLoad_dayTot = new int[8];
        private int[] UserSessions_dayTot = new int[8];
        private int[] MobileWorkLoad_dayTot = new int[8];
        private int[] MobileUserSessions_dayTot = new int[8];
        private int[] DigiTVUserSessions_dayTot = new int[8];
        private int[] DigiTVWorkLoad_dayTot = new int[8];



        private int NumTypesToReport;
        private int[] RefType = new int[50];
        private string[] RefTypeCode = new string[50];
        private int[] NominalCount = new int[50];
        private int[] FailedCount = new int[50];
        private double FailedPercent;
        private string defaultInjectorString;
        private string backupInjectorString;
        private string travelineInjectorString;




        public DFTWeeklyCombinedReport()
        {
            EventLogger = new EventLog("Application");
            EventLogger.Source = "TD.Reporting";
            controller = new DailyReportController();

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
            reportStartDateString = reportStartDate.ToShortDateString();
            reportEndDateString = reportEndDate.ToShortDateString();
            reportEndDatePlus1 = reportEndDate.AddDays(1);
            reportEndDatePlus1String = reportEndDatePlus1.ToShortDateString();


            
            
            
            tmpString = new StringBuilder();
            tmpString.Append("Day of Week,Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday");
            tmpString.Append(",Total");
			string daysString = tmpString.ToString();


            bool readOK = controller.ReadProperty(reportNumber.ToString(), "FilePath", out filePathString);
			if (readOK == false || filePathString.Length == 0)
			{
				statusCode = (int)StatusCode.ErrorReadingFilePath; // Error Reading FilePath
				return statusCode;
			}
            filePathString = filePathString.Replace("%YY-MM-DD%", RequestedDateString);


			// open report text file
			file = new StreamWriter( new FileStream(filePathString, System.IO.FileMode.Create ) );
			this.file.WriteLine("------------------------------------------");
			this.file.WriteLine("TRANSPORT DIRECT DFT WEEKLY COMBINED REPORT");
			this.file.WriteLine("------------------------------------------");
			this.file.WriteLine("");
			this.file.WriteLine("For week starting : " + reportStartDate.ToShortDateString());
			this.file.WriteLine("");


         

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
            this.file.WriteLine(daysString);

           
            this.file.WriteLine(" ");
            transactionString = "Map Events (pans/zooms/etc.)";
            selectString = "select MEDate, isnull(sum(MECount),0)from dbo.MapEvents "
                          + "where MEDate >= convert(datetime, '" + reportStartDateString
                          + "',103) and MEDate <= convert(datetime, '" + reportEndDateString
                          + "',103) group by MEDate order by MEDate";
                      DailyEventBreakDown();


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
                      selectString = "select MEDate, isnull(sum(MECount),0)from dbo.MapEvents "
                          + "where MEDate >= convert(datetime, '" + reportStartDateString
                          + "',103) and MEDate <= convert(datetime, '" + reportEndDateString
                          + "',103) "
                          + excludeString
                          + " group by MEDate order by MEDate";
                      DailyEventBreakDown();
                      this.file.WriteLine(" ");


                      reportingSqlConnection = new SqlConnection(reportingConnectionString);

                      //...............................................

                      transactionString = "User Web Page Requests (Total WorkLoad)";
                      selectString = "select WEDate, isnull(sum(WECount),0)from dbo.WorkloadEvents "
                          + "where WEDate >= convert(datetime, '" + reportStartDateString
                          + "',103) and WEDate <= convert(datetime, '" + reportEndDateString
                          + "',103) group by WEDate order by WEDate";
                      DailyEventBreakDown();



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
                      selectString = "select WEDate, isnull(sum(WECount),0)from dbo.WorkloadEvents "
                          + "where WEDate >= convert(datetime, '" + reportStartDateString
                          + "',103) and WEDate <= convert(datetime, '" + reportEndDateString
                          + "',103) "
                          + excludeString
                          + " group by WEDate order by WEDate";
                      DailyEventBreakDown();

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
                      selectString = "select SEDate, isnull(sum(SECount),0)from dbo.SessionEvents "
                          + "where SEDate >= convert(datetime, '" + reportStartDateString
                          + "',103) and SEDate <= convert(datetime, '" + reportEndDateString
                          + "',103) group by SEDate order by SEDate";
                      DailyEventBreakDown();

                      //.............................		

                      transactionString = "Single Page Sessions";
                      selectString = "select SDADate, isnull(sum(SDASinglePageSessions ),0)from dbo.SessionDailyAnalysis "
                          + "where SDADate >= convert(datetime, '" + reportStartDateString
                          + "',103) and SDADate <= convert(datetime, '" + reportEndDateString
                          + "',103) group by SDADate order by SDADate";
                      DailyEventBreakDown();

                      transactionString = "Multi-Page Sessions ";
                      selectString = "select SDADate, isnull(sum(SDAMultiplePageSessions ),0)from dbo.SessionDailyAnalysis "
                          + "where SDADate >= convert(datetime, '" + reportStartDateString
                          + "',103) and SDADate <= convert(datetime, '" + reportEndDateString
                          + "',103) group by SDADate order by SDADate";
                      DailyEventBreakDown();
            

                      transactionString = "Average Session Duration (secs) ";
                      selectString = "select SDADate, isnull(sum(SDAAverageDuration),0)from dbo.SessionDailyAnalysis "
                          + "where SDADate >= convert(datetime, '" + reportStartDateString
                          + "',103) and SDADate <= convert(datetime, '" + reportEndDateString
                          + "',103) group by SDADate order by SDADate";
                      DailyEventBreakDownAverage();


                      transactionString = "Average Multi-Page Session Duration (secs) ";
                      selectString = "select SDADate, isnull(sum(SDAAverageDurationMultiplePageSessions),0)from dbo.SessionDailyAnalysis "
                          + "where SDADate >= convert(datetime, '" + reportStartDateString
                          + "',103) and SDADate <= convert(datetime, '" + reportEndDateString
                          + "',103) group by SDADate order by SDADate";
                      DailyEventBreakDownAverage();


                      //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

                      transactionString = "User Sessions (Excluding Tests)";
                      selectString = "select SEDate, isnull(sum(SECount),0)from dbo.SessionEvents "
                          + "where SEDate >= convert(datetime, '" + reportStartDateString
                          + "',103) and SEDate <= convert(datetime, '" + reportEndDateString
                          + "',103) "
                          + excludeString
                          + " group by SEDate order by SEDate";
                      DailyEventBreakDown();




                      transactionString = "New Visitors ";
                      selectString = "select RVEDate, isnull(sum(RVECount),0)from dbo.RepeatVisitorEvents "
                          + "where RVEDate >= convert(datetime, '" + reportStartDateString
                          + "',103) and RVEDate <= convert(datetime, '" + reportEndDateString
                          + "',103) and RVERVTID = 2"
                          + " group by RVEDate order by RVEDate";
                      DailyEventBreakDown();
          
                      transactionString = "New Visitors (Excluding Tests)";
                      selectString = "select RVEDate, isnull(sum(RVECount),0)from dbo.RepeatVisitorEvents "
                          + "where RVEDate >= convert(datetime, '" + reportStartDateString
                          + "',103) and RVEDate <= convert(datetime, '" + reportEndDateString
                          + "',103) "
                          + excludeString
                          + " and RVERVTID = 2"
                          + " group by RVEDate order by RVEDate";
                      DailyEventBreakDown();

                      transactionString = "Repeat Visitors ";
                      selectString = "select RVEDate, isnull(sum(RVECount),0)from dbo.RepeatVisitorEvents "
                          + "where RVEDate >= convert(datetime, '" + reportStartDateString
                          + "',103) and RVEDate <= convert(datetime, '" + reportEndDateString
                          + "',103) and RVERVTID = 3"
                          + " group by RVEDate order by RVEDate";
                      DailyEventBreakDown();

                      transactionString = "Repeat Visitors (Excluding Tests)";
                      selectString = "select RVEDate, isnull(sum(RVECount),0)from dbo.RepeatVisitorEvents "
                          + "where RVEDate >= convert(datetime, '" + reportStartDateString
                          + "',103) and RVEDate <= convert(datetime, '" + reportEndDateString
                          + "',103) "
                          + excludeString
                          + " and RVERVTID = 3"
                          + " group by RVEDate order by RVEDate";
                      DailyEventBreakDown();
          
                      transactionString = "Registered User Sessions (Logins) ";
                      selectString = "select LEDate, isnull(sum(LECount),0)from dbo.LoginEvents "
                          + "where LEDate >= convert(datetime, '" + reportStartDateString
                          + "',103) and LEDate <= convert(datetime, '" + reportEndDateString
                          + "',103) group by LEDate order by LEDate";
                      DailyEventBreakDown();
         

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
                      selectString = "select LEDate, isnull(sum(LECount),0)from dbo.LoginEvents "
                          + "where LEDate >= convert(datetime, '" + reportStartDateString
                          + "',103) and LEDate <= convert(datetime, '" + reportEndDateString
                          + "',103) "
                          + excludeString
                          + " group by LEDate order by LEDate";
                      DailyEventBreakDown();

                      this.file.WriteLine(" ");




                   

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

                      for ( dayPtr = 1; dayPtr <= 7; dayPtr++)
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


                              dayOfWeek = thisDay.DayOfWeek.ToString();

                              switch (dayOfWeek)
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
                                      dayPtr= 4;
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


                      #endregion



                                 
                      int atot;
      

                      tmpString2 = new StringBuilder();
                      tmpString2.Append("Mobile Use Sessions");
                      atot = 0;
                      for (int g = 1; g <= 7; g++)
                      {
                        MobileUserSessions_dayTot[g] = MobileUserSessions_dayTot[g] - DigiTVUserSessions_dayTot[g];
                        tmpString2.Append(",");
                        tmpString2.Append(MobileUserSessions_dayTot[g]);
                        atot = atot + MobileUserSessions_dayTot[g];
                      }
                      tmpString2.Append(",");
                      tmpString2.Append(atot);
                      
            
                      tmpString4 = new StringBuilder();
                      tmpString4.Append("DigiTV User Sessions");
                      atot = 0;
                      for (int g = 1; g <= 7; g++)
                      {
                                  tmpString4.Append(",");
                                  tmpString4.Append(DigiTVUserSessions_dayTot[g]);
                                   atot = atot + DigiTVUserSessions_dayTot[g];
                      }
                      tmpString4.Append(",");
                      tmpString4.Append(atot);
                              
                      tmpString1 = new StringBuilder();
                      tmpString1.Append("Mobile Workload");
                      atot = 0;
                      for (int g = 1; g <= 7; g++)
                       {
                            MobileWorkLoad_dayTot[g] = MobileWorkLoad_dayTot[g] - DigiTVWorkLoad_dayTot[g];
                            tmpString1.Append(",");
                            tmpString1.Append(MobileWorkLoad_dayTot[g]);
                            atot = atot + MobileWorkLoad_dayTot[g]; 
                       }
                       tmpString1.Append(",");
                       tmpString1.Append(atot); 
                       
                       tmpString3 = new StringBuilder();
                       tmpString3.Append("DigiTV Workload");
                       atot = 0;
                       for (int g = 1; g <= 7; g++)
                       {
                                  tmpString3.Append(",");
                                  tmpString3.Append(DigiTVWorkLoad_dayTot[g]);
                                  atot = atot +  DigiTVWorkLoad_dayTot[g];
                       }
                       tmpString3.Append(",");
                       tmpString3.Append(atot); 
                       

                      this.file.WriteLine(tmpString1); //"Mobile Workload,");
                      this.file.WriteLine(tmpString2); //"Mobile Use Sessions,");
                      this.file.WriteLine(tmpString3); //"DigiTV Workload,");
                      this.file.WriteLine(tmpString4); //"DigiTV User Sessions,");

                       this.file.WriteLine(" ");

                  //*********************************************************************************

                       myDataReader = null;
                       reportingSqlConnection = new SqlConnection(reportingConnectionString);
            
                      transactionString = "DepartureBoardService RTTI";
                      selectString = "select EXSEDate, isnull(sum(EXSECount),0)from dbo.ExposedServicesEvents "
                          + "where EXSEDate >= convert(datetime, '" + reportStartDateString
                          + "',103) and EXSEDate <= convert(datetime, '" + reportEndDateString
                          + "',103) "
                          + " and EXSCID = 3 "
                          + " group by EXSEDate order by EXSEDate";
                      DailyEventBreakDown();

                      transactionString = "DepartureBoardService StopEvent";
                      selectString = "select EXSEDate, isnull(sum(EXSECount),0)from dbo.ExposedServicesEvents "
                          + "where EXSEDate >= convert(datetime, '" + reportStartDateString
                          + "',103) and EXSEDate <= convert(datetime, '" + reportEndDateString
                          + "',103) "
                          + " and EXSCID = 2 "
                          + " group by EXSEDate order by EXSEDate";
                      DailyEventBreakDown();

                      this.file.WriteLine(" ");


                      //*******************************************************************


                      transactionString = "Enhanced Exposed Service Calls (Kizoom)";
                      selectString = "select EESEDate, isnull(sum(EESECount),0)from EnhancedExposedServiceEvents "
                          + "where EESEDate >= convert(datetime, '" + reportStartDateString
                          + "',103) and EESEDate <= convert(datetime, '" + reportEndDateString
                          + "',103) "
                          + " and EESEPartnerId = 103 "
                          + " group by EESEDate order by EESEDate";
                      DailyEventBreakDown();

                      transactionString = "Enhanced Exposed Service Calls (Lauren)";
                      selectString = "select EESEDate, isnull(sum(EESECount),0)from EnhancedExposedServiceEvents "
                          + "where EESEDate >= convert(datetime, '" + reportStartDateString
                          + "',103) and EESEDate <= convert(datetime, '" + reportEndDateString
                          + "',103) "
                          + " and EESEPartnerId = 102 "
                          + " group by EESEDate order by EESEDate";
                      DailyEventBreakDown();
                                  transactionString = "Enhanced Exposed Service Calls (Direct Gov)";
                      selectString = "select EESEDate, isnull(sum(EESECount),0)from EnhancedExposedServiceEvents "
                          + "where EESEDate >= convert(datetime, '" + reportStartDateString
                          + "',103) and EESEDate <= convert(datetime, '" + reportEndDateString
                          + "',103) "
                          + " and EESEPartnerId = 104 "
                          + " group by EESEDate order by EESEDate";
                      DailyEventBreakDown();

                      transactionString = "Enhanced Exposed Service Calls (Direct GovDigiTV)";
                      selectString = "select EESEDate, isnull(sum(EESECount),0)from EnhancedExposedServiceEvents "
                          + "where EESEDate >= convert(datetime, '" + reportStartDateString
                          + "',103) and EESEDate <= convert(datetime, '" + reportEndDateString
                          + "',103) "
                          + " and EESEPartnerId = 109 "
                          + " group by EESEDate order by EESEDate";
                      DailyEventBreakDown();

                      transactionString = "Enhanced Exposed Service Calls (Direct GovJPlan)";
                      selectString = "select EESEDate, isnull(sum(EESECount),0)from EnhancedExposedServiceEvents "
                          + "where EESEDate >= convert(datetime, '" + reportStartDateString
                          + "',103) and EESEDate <= convert(datetime, '" + reportEndDateString
                          + "',103) "
                          + " and EESEPartnerId = 111 "
                          + " group by EESEDate order by EESEDate";
                      DailyEventBreakDown();

                      transactionString = "Enhanced Exposed Service Calls (Direct GovAPP2)";
                      selectString = "select EESEDate, isnull(sum(EESECount),0)from EnhancedExposedServiceEvents "
                          + "where EESEDate >= convert(datetime, '" + reportStartDateString
                          + "',103) and EESEDate <= convert(datetime, '" + reportEndDateString
                          + "',103) "
                          + " and EESEPartnerId = 121 "
                          + " group by EESEDate order by EESEDate";
                      DailyEventBreakDown();

                      transactionString = "Enhanced Exposed Service Calls (Batch Journey Planner)";
                      selectString = "select EESEDate, isnull(sum(EESECount),0)from EnhancedExposedServiceEvents "
                          + "where EESEDate >= convert(datetime, '" + reportStartDateString
                          + "',103) and EESEDate <= convert(datetime, '" + reportEndDateString
                          + "',103) "
                          + " and EESEPartnerId = 300 "
                          + " group by EESEDate order by EESEDate";
                      DailyEventBreakDown();

                      transactionString = "Enhanced Exposed Service Calls (Centro)";
                      selectString = "select EESEDate, isnull(sum(EESECount),0)from EnhancedExposedServiceEvents "
                          + "where EESEDate >= convert(datetime, '" + reportStartDateString
                          + "',103) and EESEDate <= convert(datetime, '" + reportEndDateString
                          + "',103) "
                          + " and EESEPartnerId = 122 "
                          + " group by EESEDate order by EESEDate";
                      DailyEventBreakDown();

                      transactionString = "Enhanced Exposed Service Calls (Blackburn)";
                      selectString = "select EESEDate, isnull(sum(EESECount),0)from EnhancedExposedServiceEvents "
                          + "where EESEDate >= convert(datetime, '" + reportStartDateString
                          + "',103) and EESEDate <= convert(datetime, '" + reportEndDateString
                          + "',103) "
                          + " and EESEPartnerId = 133 "
                          + " group by EESEDate order by EESEDate";
                      DailyEventBreakDown();
                      transactionString = "Enhanced Exposed Service Calls (Essex CC)";
                      selectString = "select EESEDate, isnull(sum(EESECount),0)from EnhancedExposedServiceEvents "
                          + "where EESEDate >= convert(datetime, '" + reportStartDateString
                          + "',103) and EESEDate <= convert(datetime, '" + reportEndDateString
                          + "',103) "
                          + " and EESEPartnerId = 136 "
                          + " group by EESEDate order by EESEDate";
                      DailyEventBreakDown();
                      transactionString = "Enhanced Exposed Service Calls (Stoke-on-Trent)";
                      selectString = "select EESEDate, isnull(sum(EESECount),0)from EnhancedExposedServiceEvents "
                          + "where EESEDate >= convert(datetime, '" + reportStartDateString
                          + "',103) and EESEDate <= convert(datetime, '" + reportEndDateString
                          + "',103) "
                          + " and EESEPartnerId = 137 "
                          + " group by EESEDate order by EESEDate";
                      DailyEventBreakDown();

                      transactionString = "Enhanced Exposed Service Calls (LiftShare)";
                      selectString = "select EESEDate, isnull(sum(EESECount),0)from EnhancedExposedServiceEvents "
                          + "where EESEDate >= convert(datetime, '" + reportStartDateString
                          + "',103) and EESEDate <= convert(datetime, '" + reportEndDateString
                          + "',103) "
                          + " and EESEPartnerId = 134 "
                          + " group by EESEDate order by EESEDate";
                      DailyEventBreakDown();
                     
		      transactionString = "Enhanced Exposed Service Calls (CIBER)";
                      selectString = "select EESEDate, isnull(sum(EESECount),0)from EnhancedExposedServiceEvents "
                          + "where EESEDate >= convert(datetime, '" + reportStartDateString
                          + "',103) and EESEDate <= convert(datetime, '" + reportEndDateString
                          + "',103) "
                          + " and EESEPartnerId = 140 "
                          + " group by EESEDate order by EESEDate";
                      DailyEventBreakDown();

                      transactionString = "Enhanced Exposed Service Calls (Middlesbrough Council)";
                      selectString = "select EESEDate, isnull(sum(EESECount),0)from EnhancedExposedServiceEvents "
                          + "where EESEDate >= convert(datetime, '" + reportStartDateString
                          + "',103) and EESEDate <= convert(datetime, '" + reportEndDateString
                          + "',103) "
                          + " and EESEPartnerId = 142 "
                          + " group by EESEDate order by EESEDate";
                      DailyEventBreakDown(); this.file.WriteLine(" ");

                      //*******************************************************************



                      transactionString = "BBC User Sessions ";
                      selectString = "select SEDate, isnull(sum(SECount),0)from dbo.SessionEvents "
                          + "where SEDate >= convert(datetime, '" + reportStartDateString
                          + "',103) and SEDate <= convert(datetime, '" + reportEndDateString
                          + "',103) and SEPartnerId = 2 group by SEDate order by SEDate";
                      DailyEventBreakDown();
                      transactionString = "BBC New Visitors ";
                      selectString = "select RVEDate, isnull(sum(RVECount),0)from dbo.RepeatVisitorEvents "
                          + "where RVEDate >= convert(datetime, '" + reportStartDateString
                          + "',103) and RVEDate <= convert(datetime, '" + reportEndDateString
                          + "',103) and RVEPartnerId = 2 and RVERVTID = 2"
                          + " group by RVEDate order by RVEDate";
                      DailyEventBreakDown();
            
                      transactionString = "BBC Repeat Visitors ";
                      selectString = "select RVEDate, isnull(sum(RVECount),0)from dbo.RepeatVisitorEvents "
                          + "where RVEDate >= convert(datetime, '" + reportStartDateString
                          + "',103) and RVEDate <= convert(datetime, '" + reportEndDateString
                          + "',103) and RVEPartnerId = 2 and RVERVTID = 3"
                          + " group by RVEDate order by RVEDate";
                      DailyEventBreakDown();
            
                      transactionString = "BBC Workload";
                      selectString = "select WEDate, isnull(sum(WECount),0)from dbo.WorkloadEvents "
                          + "where WEDate >= convert(datetime, '" + reportStartDateString
                          + "',103) and WEDate <= convert(datetime, '" + reportEndDateString
                          + "',103) and PartnerId = 2 group by WEDate order by WEDate";
                      DailyEventBreakDown();


                      transactionString = "DirectGov User Sessions ";
                      selectString = "select SEDate, isnull(sum(SECount),0)from dbo.SessionEvents "
                          + "where SEDate >= convert(datetime, '" + reportStartDateString
                          + "',103) and SEDate <= convert(datetime, '" + reportEndDateString
                          + "',103) and SEPartnerId = 4 group by SEDate order by SEDate";
                      DailyEventBreakDown();
                      transactionString = "DirectGov New Visitors ";
                      selectString = "select RVEDate, isnull(sum(RVECount),0)from dbo.RepeatVisitorEvents "
                          + "where RVEDate >= convert(datetime, '" + reportStartDateString
                          + "',103) and RVEDate <= convert(datetime, '" + reportEndDateString
                          + "',103) and RVEPartnerId = 4 and RVERVTID = 2"
                          + " group by RVEDate order by RVEDate";
                      DailyEventBreakDown();
                      transactionString = "DirectGov Repeat Visitors ";
                      selectString = "select RVEDate, isnull(sum(RVECount),0)from dbo.RepeatVisitorEvents "
                          + "where RVEDate >= convert(datetime, '" + reportStartDateString
                          + "',103) and RVEDate <= convert(datetime, '" + reportEndDateString
                          + "',103) and RVEPartnerId = 4 and RVERVTID = 3"
                          + " group by RVEDate order by RVEDate";
                      DailyEventBreakDown();
                      transactionString = "DirectGov Workload";
                      selectString = "select WEDate, isnull(sum(WECount),0)from dbo.WorkloadEvents "
                          + "where WEDate >= convert(datetime, '" + reportStartDateString
                          + "',103) and WEDate <= convert(datetime, '" + reportEndDateString
                          + "',103) and PartnerId = 4 group by WEDate order by WEDate";
                      DailyEventBreakDown();


                      transactionString = "Visit Britain User Sessions ";
                      selectString = "select SEDate, isnull(sum(SECount),0)from dbo.SessionEvents "
                          + "where SEDate >= convert(datetime, '" + reportStartDateString
                          + "',103) and SEDate <= convert(datetime, '" + reportEndDateString
                          + "',103) and SEPartnerId = 1 group by SEDate order by SEDate";
                      DailyEventBreakDown();
                      transactionString = "Visit Britain New Visitors ";
                      selectString = "select RVEDate, isnull(sum(RVECount),0)from dbo.RepeatVisitorEvents "
                          + "where RVEDate >= convert(datetime, '" + reportStartDateString
                          + "',103) and RVEDate <= convert(datetime, '" + reportEndDateString
                          + "',103) and RVEPartnerId = 1 and RVERVTID = 2"
                          + " group by RVEDate order by RVEDate";
                      DailyEventBreakDown();
                      transactionString = "Visit Britain Repeat Visitors ";
                      selectString = "select RVEDate, isnull(sum(RVECount),0)from dbo.RepeatVisitorEvents "
                          + "where RVEDate >= convert(datetime, '" + reportStartDateString
                          + "',103) and RVEDate <= convert(datetime, '" + reportEndDateString
                          + "',103) and RVEPartnerId = 1 and RVERVTID = 3"
                          + " group by RVEDate order by RVEDate";
                      DailyEventBreakDown();
                      transactionString = "Visit Britain Workload";
                      selectString = "select WEDate, isnull(sum(WECount),0)from dbo.WorkloadEvents "
                          + "where WEDate >= convert(datetime, '" + reportStartDateString
                          + "',103) and WEDate <= convert(datetime, '" + reportEndDateString
                          + "',103) and PartnerId = 1 group by WEDate order by WEDate";
                      DailyEventBreakDown();

                      transactionString = "BusinessLink User Sessions ";
                      selectString = "select SEDate, isnull(sum(SECount),0)from dbo.SessionEvents "
                          + "where SEDate >= convert(datetime, '" + reportStartDateString
                          + "',103) and SEDate <= convert(datetime, '" + reportEndDateString
                          + "',103) and SEPartnerId = 7 group by SEDate order by SEDate";
                      DailyEventBreakDown();
                      transactionString = "BusinessLink New Visitors ";
                      selectString = "select RVEDate, isnull(sum(RVECount),0)from dbo.RepeatVisitorEvents "
                          + "where RVEDate >= convert(datetime, '" + reportStartDateString
                          + "',103) and RVEDate <= convert(datetime, '" + reportEndDateString
                          + "',103) and RVEPartnerId = 7 and RVERVTID = 2"
                          + " group by RVEDate order by RVEDate";
                      DailyEventBreakDown();
                      transactionString = "BusinessLink Repeat Visitors ";
                      selectString = "select RVEDate, isnull(sum(RVECount),0)from dbo.RepeatVisitorEvents "
                          + "where RVEDate >= convert(datetime, '" + reportStartDateString
                          + "',103) and RVEDate <= convert(datetime, '" + reportEndDateString
                          + "',103) and RVEPartnerId = 7 and RVERVTID = 3"
                          + " group by RVEDate order by RVEDate";
                      DailyEventBreakDown();
                      transactionString = "BusinessLink Workload";
                      selectString = "select WEDate, isnull(sum(WECount),0)from dbo.WorkloadEvents "
                          + "where WEDate >= convert(datetime, '" + reportStartDateString
                          + "',103) and WEDate <= convert(datetime, '" + reportEndDateString
                          + "',103) and PartnerId = 7 group by WEDate order by WEDate";
                      DailyEventBreakDown();

                      transactionString = "BusinessGateway User Sessions ";
                      selectString = "select SEDate, isnull(sum(SECount),0)from dbo.SessionEvents "
                          + "where SEDate >= convert(datetime, '" + reportStartDateString
                          + "',103) and SEDate <= convert(datetime, '" + reportEndDateString
                          + "',103) and SEPartnerId = 8 group by SEDate order by SEDate";
                      DailyEventBreakDown();
                      transactionString = "BusinessGateway New Visitors ";
                      selectString = "select RVEDate, isnull(sum(RVECount),0)from dbo.RepeatVisitorEvents "
                          + "where RVEDate >= convert(datetime, '" + reportStartDateString
                          + "',103) and RVEDate <= convert(datetime, '" + reportEndDateString
                          + "',103) and RVEPartnerId = 8 and RVERVTID = 2"
                          + " group by RVEDate order by RVEDate";
                      DailyEventBreakDown();
                      transactionString = "BusinessGateway Repeat Visitors ";
                      selectString = "select RVEDate, isnull(sum(RVECount),0)from dbo.RepeatVisitorEvents "
                          + "where RVEDate >= convert(datetime, '" + reportStartDateString
                          + "',103) and RVEDate <= convert(datetime, '" + reportEndDateString
                          + "',103) and RVEPartnerId = 8 and RVERVTID = 3"
                          + " group by RVEDate order by RVEDate";
                      DailyEventBreakDown();
                      transactionString = "BusinessGateway Workload";
                      selectString = "select WEDate, isnull(sum(WECount),0)from dbo.WorkloadEvents "
                          + "where WEDate >= convert(datetime, '" + reportStartDateString
                          + "',103) and WEDate <= convert(datetime, '" + reportEndDateString
                          + "',103) and PartnerId = 8 group by WEDate order by WEDate";
                      DailyEventBreakDown();

                      transactionString = "CycleRoutes User Sessions ";
                      selectString = "select SEDate, isnull(sum(SECount),0)from dbo.SessionEvents "
                          + "where SEDate >= convert(datetime, '" + reportStartDateString
                          + "',103) and SEDate <= convert(datetime, '" + reportEndDateString
                          + "',103) and SEPartnerId = 9 group by SEDate order by SEDate";
                      DailyEventBreakDown();
                      transactionString = "CycleRoutes New Visitors ";
                      selectString = "select RVEDate, isnull(sum(RVECount),0)from dbo.RepeatVisitorEvents "
                          + "where RVEDate >= convert(datetime, '" + reportStartDateString
                          + "',103) and RVEDate <= convert(datetime, '" + reportEndDateString
                          + "',103) and RVEPartnerId = 9 and RVERVTID = 2"
                          + " group by RVEDate order by RVEDate";
                      DailyEventBreakDown();
                      transactionString = "CycleRoutes Repeat Visitors ";
                      selectString = "select RVEDate, isnull(sum(RVECount),0)from dbo.RepeatVisitorEvents "
                          + "where RVEDate >= convert(datetime, '" + reportStartDateString
                          + "',103) and RVEDate <= convert(datetime, '" + reportEndDateString
                          + "',103) and RVEPartnerId = 9 and RVERVTID = 3"
                          + " group by RVEDate order by RVEDate";
                      DailyEventBreakDown();
                      transactionString = "CycleRoutes Workload";
                      selectString = "select WEDate, isnull(sum(WECount),0)from dbo.WorkloadEvents "
                          + "where WEDate >= convert(datetime, '" + reportStartDateString
                          + "',103) and WEDate <= convert(datetime, '" + reportEndDateString
                          + "',103) and PartnerId = 9 group by WEDate order by WEDate";
                      DailyEventBreakDown();
            
                      this.file.WriteLine(" ");

            //**************************************************************

                       transactionString = "Total Page Landing";
                      selectString = "select 'Total Page Landing', isnull(sum(LPECount),0)from dbo.LandingPageEvents "
                          + "where LPEDate >= convert(datetime, '" + reportStartDateString
                          + "',103) and LPEDate <= convert(datetime, '" + reportEndDateString
                          + "',103) ";
                      DailyEventBreakDownWithDesc();
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

                     DailyEventBreakDownWithDesc();

                      this.file.WriteLine(" ");
                      this.file.WriteLine(" ");
                      this.file.WriteLine("Page Landing By Type ");

  
                      selectString = "select  b.LPSDescription, isnull(sum(a.LPECount),0) "
                    + " from dbo.LandingPageService b "
                    + " Left join  dbo.LandingPageEvents a "
                    + " on a.LPELPSID = b.lpsid "
                    + " and a.LPEDate >= convert(datetime, '" + reportStartDateString
                    + " ',103) and a.LPEDate <= convert(datetime, '" + reportEndDateString
                    + "',103) "
                    + " group by b.LPSDescription"
                    + " order by b.LPSDescription";



                      DailyEventBreakDownWithDesc();
                      this.file.WriteLine(" ");

                      selectString = "select  b.LPPDescription, isnull(sum(a.LPECount),0) "
                       + " from dbo.LandingPageEvents a, dbo.LandingPagePartner b "
                           + "where a.LPEDate >= convert(datetime, '" + reportStartDateString
                           + "',103) and a.LPEDate <= convert(datetime, '" + reportEndDateString
                           + "',103) and a.LPELPPID = b.lppid and a.LPelppid = 21 "
                           + " group by b.LPPDescription";
             
                      DailyEventBreakDownWithDesc();
                      this.file.WriteLine(" ");

//*****************************************************************************************************
//*****************************************************************************************************

                      this.file.WriteLine("Web Usage less Site Confidence");
                      this.file.WriteLine(" ");          

                      tmpString = new StringBuilder();
                      tmpString.Append("User Web Pages (Portal)");
                      atot=0;
                      int btot = 0;
                    for(int i=1; i<=7; i++)
                    {
                        btot = WorkLoad_dayTot[i]-1728;
                        tmpString.Append(",");
                        tmpString.Append(btot.ToString());
                        atot=atot+btot;
                    }
                    tmpString.Append(",");
                    tmpString.Append(atot.ToString());
                    this.file.WriteLine(tmpString);


                    tmpString = new StringBuilder();
                      tmpString.Append("User Sessions (Portal)");
                      atot=0;
                      btot = 0;
                    for(int i=1; i<=7; i++)
                    {
                        btot = UserSessions_dayTot[i]-1248;
                        tmpString.Append(",");
                        tmpString.Append(btot.ToString());
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
                    for(int i=1; i<=7; i++)
                    {
                        btot = MobileWorkLoad_dayTot[i]-288;
                        tmpString.Append(",");
                        tmpString.Append(btot.ToString());
                        atot=atot+btot;
                    }
                    tmpString.Append(",");
                    tmpString.Append(atot.ToString());
                    this.file.WriteLine(tmpString);

                        tmpString = new StringBuilder();
                      tmpString.Append("User Sessions (Mobile)");
                      atot=0;
                      btot = 0;
                    for(int i=1; i<=7; i++)
                    {
                        btot = MobileUserSessions_dayTot[i]-288;
                        tmpString.Append(",");
                        tmpString.Append(btot.ToString());
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
                          "select RHT.RHTDescription, RH.RHEDate, isnull(sum(RH.RHECount),0)"
                          + "from dbo.RetailerHandoffEvents RH, dbo.RetailerHandoffType RHT where RHT.RHTID = RH.RHERHTID"
                          + " and RHT.RHTOnlineRetailer = 1 "
                          + "  and RH.RHEDate >= convert(datetime, '" + reportStartDateString
                          + "',103) and RH.RHEDate <= convert(datetime, '" + reportEndDateString
                          + "',103) group by RHT.RHTDescription,RH.RHEDate order by RHT.RHTDescription,RH.RHEDate";


                      DailyEventBreakDownByDayWithDesc();
                      this.file.WriteLine(" ");

//*******************************************************************************************************
//*******************************************************************************************************


        

          
            this.file.WriteLine("Weekly Map Transactions");
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
                      
            //*************************************************************************************************************************
            transactionCount = 0;
            totalChartView = 0;
            totalTableView = 0;
            totalGP = 0;
            
            reportingSqlConnection = new SqlConnection(reportingConnectionString);

            selectString =
                "select GPEGPDTID, isnull(sum(GPECount),0)"
                + "from dbo.GradientProfileEvents "
                + "where  GPEDate >= convert(datetime, '" + reportStartDateString
                + "',103) and GPEDate <= convert(datetime, '" + reportEndDateString
                + "',103) group by GPEGPDTID order by GPEGPDTID";

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
                    transactionCount = Convert.ToInt32(myDataReader.GetSqlInt32(1).ToString());


                    if (productNum == 2)
                    {
                        totalChartView = transactionCount;
                    }
                    if (productNum == 3)
                    {
                        totalTableView = transactionCount;
                    }


                } // end of while
                this.file.Flush();
                myDataReader.Close();
            }
            catch (Exception e)
            {
                EventLogger.WriteEntry("Failure reading Gradient Profile table " + e.Message, EventLogEntryType.Error);
                statusCode = (int)StatusCode.ErrorReadingTable; //
            }
            finally
            {
                // Always call Close when done reading.
                if (myDataReader != null)
                    myDataReader.Close();
            }

            totalGP = totalChartView + totalTableView;
            this.file.WriteLine(" ");
            this.file.WriteLine("Gradient Profile Lookups ");
            this.file.WriteLine(" ,Chart View,Table View ,Total");
            this.file.WriteLine("WebServiceRequests ," + totalChartView.ToString()
                + "," + totalTableView.ToString() + "," + totalGP.ToString());
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

/****************************************************************************************************/

            //*******************************************************************************************************
            //*******************************************************************************************************





            this.file.WriteLine("");
            this.file.WriteLine("Weekly OS Map Sessions");
            this.file.WriteLine("");
            this.file.Flush();

          
            totalVector = 0;
            totalRaster = 0;
            
            reportingSqlConnection = new SqlConnection(reportingConnectionString);

            selectString =
                "select ME.MEMDTID,MDT.MDTDescription, isnull(sum(ME.MECount),0)"
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
                tmpString.Append("Product(Scale),Total Sessions");
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
                tmpString.Append("Product(Scale),Total Sessions");
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
            transactionCount = 0;
            totalChartView = 0;
            totalTableView = 0;
            totalGP = 0;

            reportingSqlConnection = new SqlConnection(reportingConnectionString);

            selectString =
                "select GPEGPDTID, isnull(sum(GPECount),0)"
                + "from dbo.GradientProfileOS "
                + "where  GPEDate >= convert(datetime, '" + reportStartDateString
                + "',103) and GPEDate <= convert(datetime, '" + reportEndDateString
                + "',103) and GPEGPDTID = 2 group by GPEGPDTID";

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
                    transactionCount = Convert.ToInt32(myDataReader.GetSqlInt32(1).ToString());


                    if (productNum == 2)
                    {
                        totalChartView = transactionCount;
                    }


                } // end of while
                this.file.Flush();
                myDataReader.Close();
            }
            catch (Exception e)
            {
                EventLogger.WriteEntry("Failure reading Gradient Profile OS table " + e.Message, EventLogEntryType.Error);
                statusCode = (int)StatusCode.ErrorReadingTable; //
            }
            finally
            {
                // Always call Close when done reading.
                if (myDataReader != null)
                    myDataReader.Close();
            }

            totalGP = totalChartView;
            this.file.WriteLine(" ");
            this.file.WriteLine("Gradient Profile Sessions ");
            this.file.WriteLine("WebServiceRequests ," + totalChartView.ToString());
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











/****************************************************************************************************/
            this.file.WriteLine(" ");
            this.file.WriteLine("Gazetteer Transactions");

            tmpString = new StringBuilder();
            tmpString.Append("Product Description, Count");
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



            //********************************************
            //			Now do same again accounting for the test calendar

            transactionCount = 0;

            if (testCount == 0)
            {
                this.file.WriteLine(" ");
                this.file.WriteLine("(The above mapping figures are derived from actual user transactions. No testing transactions are present)");
                this.file.WriteLine(" ");
            }
            else
            {
                this.file.WriteLine(" ");
                this.file.WriteLine("The above mapping figures include periods of testing where maps have been auto-generated but not actually viewed by anybody.");
                this.file.WriteLine("The figures below have been adjusted to ignore these testing periods and should be used as an indication of actual user requests for charging purposes.");
                this.file.WriteLine(" ");

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
               
                this.file.WriteLine("Weekly Map Transactions (Adjusted for test periods)");
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
                transactionCount = 0;
                totalChartView = 0;
                totalTableView = 0;
                totalGP = 0;
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



                reportingSqlConnection = new SqlConnection(reportingConnectionString);




                selectString =
                    "select GPEGPDTID, isnull(sum(GPECount),0)"
                    + "from dbo.GradientProfileEvents "
                    + "where  GPEDate >= convert(datetime, '" + reportStartDateString
                    + "',103) and GPEDate <= convert(datetime, '" + reportEndDateString
                    + "',103) "
                    + excludeString
                    + " group by GPEGPDTID order by GPEGPDTID";

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
                        transactionCount = Convert.ToInt32(myDataReader.GetSqlInt32(1).ToString());


                        if (productNum == 2)
                        {
                            totalChartView = transactionCount;
                        }
                        if (productNum == 3)
                        {
                            totalTableView = transactionCount;
                        }


                    } // end of while
                    this.file.Flush();
                    myDataReader.Close();
                }
                catch (Exception e)
                {
                    EventLogger.WriteEntry("Failure reading Gradient Profile table " + e.Message, EventLogEntryType.Error);
                    statusCode = (int)StatusCode.ErrorReadingTable; //
                }
                finally
                {
                    // Always call Close when done reading.
                    if (myDataReader != null)
                        myDataReader.Close();
                }

                totalGP = totalChartView + totalTableView;
                this.file.WriteLine(" ");
                this.file.WriteLine(" ");
                this.file.WriteLine("Gradient Profile Lookups for Week ");
                this.file.WriteLine(" ,Chart View,Table View ,Total");
                this.file.WriteLine("WebServiceRequests ," + totalChartView.ToString()
                    + "," + totalTableView.ToString() + "," + totalGP.ToString());
                this.file.WriteLine(" ");
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
                tmpString.Append("Product Description, Count");
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






            }


//////////////////////////////////////////////////////////////////////////////////////////////////////////////
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
                totNominalCount +=NominalCount[i];
                totFailedCount += FailedCount[i];
            }
            FailedPercent = Math.Round(((((double)totFailedCount * 1000) / totNominalCount) + 0.005) / 10,2);

                tmpString = new StringBuilder();
                tmpString.Append(totNominalCount + ",");
                tmpString.Append(totFailedCount + ",");
                tmpString.Append(FailedPercent);

                this.file.WriteLine("");
                this.file.WriteLine("");
                this.file.WriteLine("Weekly Transaction Performance ");
                this.file.WriteLine("Nominal,Failed,Failed");
                this.file.WriteLine("Count,Count,Percentage");
                this.file.WriteLine(tmpString);
                this.file.WriteLine("");
                this.file.WriteLine("");
                this.file.Flush();
    
///////////////////////////////////////////////////////////////////////////////////////////////////













            this.file.WriteLine("These figures ex DfT Daily Report");

            this.file.Flush();
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
				subject = "DfT Weekly Combined Report %YY-MM-DD%";
			}
            subject = subject.Replace("%YY-MM-DD%", RequestedDateString);

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

            int[] dayTot = new int[8];
            int[] dayGrandTot = new int[8];
            int WeeklyTotal = 0;
            DateTime thisDay;
            string dayOfWeek;
            tmpString = new StringBuilder();
            tmpString.Append(transactionString);
            myDataReader = null;
            mySqlCommand = new SqlCommand(selectString, reportingSqlConnection);


            int transactionCount = 0;
            for (int i = 1; i <= 7; i++) dayTot[i] = 0;
            try
            {
                reportingSqlConnection.Open();
                myDataReader = mySqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (myDataReader.Read())
                {
                    thisDay = myDataReader.GetDateTime(0);
                    transactionCount = Convert.ToInt32(myDataReader.GetSqlInt32(1).ToString());
                    dayOfWeek = thisDay.DayOfWeek.ToString();

                    WeeklyTotal = WeeklyTotal + transactionCount;
                    switch (dayOfWeek)
                    {
                        case "Monday":
                            dayTot[1] = dayTot[1] + transactionCount;

                            break;
                        case "Tuesday":
                            dayTot[2] = dayTot[2] + transactionCount;
                            break;
                        case "Wednesday":
                            dayTot[3] = dayTot[3] + transactionCount;
                            break;
                        case "Thursday":
                            dayTot[4] = dayTot[4] + transactionCount;
                            break;
                        case "Friday":
                            dayTot[5] = dayTot[5] + transactionCount;
                            break;
                        case "Saturday":
                            dayTot[6] = dayTot[6] + transactionCount;
                            break;
                        case "Sunday":
                            dayTot[7] = dayTot[7] + transactionCount;
                            break;

                    }

                } // end of while


                // flush the last web page
                tmpString = new StringBuilder();
                tmpString.Append(transactionString);
                for (int i = 1; i <= 7; i++)
                {
                    tmpString.Append(",");
                    tmpString.Append(dayTot[i].ToString());
                }
                tmpString.Append(","); 
                tmpString.Append(WeeklyTotal);
                this.file.WriteLine(tmpString.ToString());

                this.file.Flush();
                myDataReader.Close();
                if (transactionString.CompareTo("User Web Page Requests (Total WorkLoad)") == 0)
                {
                    for (int i = 1; i <= 7; i++)
                    {
                        WorkLoad_dayTot[i] = dayTot[i];
                    }
                }
                if (transactionString.CompareTo("User Sessions") == 0)
                {
                    for (int i = 1; i <= 7; i++)
                    {
                        UserSessions_dayTot[i] = dayTot[i];
                    }
                }
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




        void DailyEventBreakDownAverage()
        {
            int[] dayTot = new int[8];
            int[] dayGrandTot = new int[8];
            int WeeklyTotal = 0;
            DateTime thisDay;
            string dayOfWeek;
            tmpString = new StringBuilder();
            tmpString.Append(transactionString);
            myDataReader = null;
            mySqlCommand = new SqlCommand(selectString, reportingSqlConnection);
            int transactionCount = 0;
            for (int i = 1; i <= 7; i++) dayTot[i] = 0;
            try
            {
                reportingSqlConnection.Open();
                myDataReader = mySqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (myDataReader.Read())
                {
                    thisDay = myDataReader.GetDateTime(0);
                    transactionCount = Convert.ToInt32(myDataReader.GetSqlInt32(1).ToString());
                    dayOfWeek = thisDay.DayOfWeek.ToString();

                    WeeklyTotal = WeeklyTotal + transactionCount;
                    switch (dayOfWeek)
                    {
                        case "Monday":
                            dayTot[1] = dayTot[1] + transactionCount;
                            break;
                        case "Tuesday":
                            dayTot[2] = dayTot[2] + transactionCount;
                            break;
                        case "Wednesday":
                            dayTot[3] = dayTot[3] + transactionCount;
                            break;
                        case "Thursday":
                            dayTot[4] = dayTot[4] + transactionCount;
                            break;
                        case "Friday":
                            dayTot[5] = dayTot[5] + transactionCount;
                            break;
                        case "Saturday":
                            dayTot[6] = dayTot[6] + transactionCount;
                            break;
                        case "Sunday":
                            dayTot[7] = dayTot[7] + transactionCount;
                            break;

                    }

                } // end of while


                // flush the last web page
                tmpString = new StringBuilder();
                tmpString.Append(transactionString);
                for (int i = 1; i <= 7; i++)
                {
                    tmpString.Append(",");
                    tmpString.Append(dayTot[i].ToString());
                }
                tmpString.Append(",");

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

            if (transactionString.Contains("Average Session Duration (secs) ")
                ||transactionString.Contains("Average Multi-Page Session Duration (secs) "))
            {

                      if (transactionString.Contains("Average Session Duration (secs) "))
                      {   
                        selectString =
                         "select isnull("
                        + "SUM(SDASinglePageSessions * isnull(SDAAverageDuration,0)) "
                        + "/ (CASE WHEN SUM(isnull(SDASinglePageSessions,0)) = 0 THEN 1 ELSE SUM(isnull(SDASinglePageSessions,0)) END) "
                        + ", 0)"
                        + "from dbo.SessionDailyAnalysis "
                        + "where SDADate >= convert(datetime, '" + reportStartDateString
                        + "',103) and SDADate <= convert(datetime, '" + reportEndDateString
                        + "',103)";
                      }
                      else
                      {
                       selectString =
                         "select isnull("
                        + "SUM(SDAMultiplePageSessions * isnull(SDAAverageDurationMultiplePageSessions,0)) "
                        + "/ (CASE WHEN SUM(isnull(SDAMultiplePageSessions,0)) = 0 THEN 1 ELSE SUM(isnull(SDAMultiplePageSessions,0)) END) "
                        + ", 0)"
                        + "from dbo.SessionDailyAnalysis "
                        + "where SDADate >= convert(datetime, '" + reportStartDateString
                        + "',103) and SDADate <= convert(datetime, '" + reportEndDateString
                        + "',103)";
                      }

                myDataReader = null;
                reportingSqlConnection = new SqlConnection(reportingConnectionString);
                mySqlCommand = new SqlCommand(selectString, reportingSqlConnection);
                reportingSqlConnection.Open();
                try
                {
                    myDataReader = mySqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
                    while (myDataReader.Read())
                    {
                        transactionCount = Convert.ToInt32(myDataReader.GetSqlInt32(0).ToString());
                    }

                    tmpString.Append(transactionCount);

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
            else
            {
                tmpString.Append(WeeklyTotal / 7);
            }


            this.file.WriteLine(tmpString.ToString());

            this.file.Flush();
        }








        void DailyEventBreakDownWithDesc()
        {

            
            myDataReader = null;
            mySqlCommand = new SqlCommand(selectString, reportingSqlConnection);
            int transactionCount = 0;
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
                    tmpString.Append(title);
                    tmpString.Append(",");
                    tmpString.Append(transactionCount.ToString());
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


         
    
    
      
        void DailyEventBreakDownByDayWithDesc()
       {
            int[] dayTot = new int[8];
            int[] dayGrandTot = new int[8];
            int WeeklyTotal = 0;
            int lineCount = 0;
            string description;
            string lastDescription;
            DateTime thisDay;
            string dayOfWeek;
            tmpString = new StringBuilder();
            tmpString.Append(transactionString);
            myDataReader = null;
            mySqlCommand = new SqlCommand(selectString, reportingSqlConnection);
            int transactionCount = 0;
            for (int i = 1; i <= 7; i++) dayTot[i] = 0;
            try
            {
                lastDescription = "default";
                reportingSqlConnection.Open();
                myDataReader = mySqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (myDataReader.Read())
                {
                    description = myDataReader.GetString(0);
                    thisDay = myDataReader.GetDateTime(1);
                    transactionCount = Convert.ToInt32(myDataReader.GetSqlInt32(2).ToString());
                    dayOfWeek = thisDay.DayOfWeek.ToString();


                    if (lastDescription == "default")
                    {
                            lastDescription = description;
                    }
                    
                    if (description.CompareTo(lastDescription) != 0)
                    {
                            tmpString = new StringBuilder();
                            tmpString.Append(lastDescription);
                            for (int i = 1; i <= 7; i++)
                            {
                                tmpString.Append("," + dayTot[i]);
                                WeeklyTotal = WeeklyTotal + dayTot[i];
                                dayTot[i] = 0;
                            }
                            tmpString.Append("," + WeeklyTotal);
                            WeeklyTotal = 0;
                            this.file.WriteLine(tmpString.ToString());
                            lineCount = lineCount + 1;
                            this.file.Flush();
                            lastDescription = description;
                      }

                        switch (dayOfWeek)
                        {
                            case "Monday":
                                dayTot[1] = dayTot[1] + transactionCount;
                                break;
                            case "Tuesday":
                                dayTot[2] = dayTot[2] + transactionCount;
                                break;
                            case "Wednesday":
                                dayTot[3] = dayTot[3] + transactionCount;
                                break;
                            case "Thursday":
                                dayTot[4] = dayTot[4] + transactionCount;
                                break;
                            case "Friday":
                                dayTot[5] = dayTot[5] + transactionCount;
                                break;
                            case "Saturday":
                                dayTot[6] = dayTot[6] + transactionCount;
                                break;
                            case "Sunday":
                                dayTot[7] = dayTot[7] + transactionCount;
                                break;

                        
                    }
                   

                } // end of while


                // flush the last web page
                tmpString = new StringBuilder();
                tmpString.Append(lastDescription);
                WeeklyTotal = 0;
                for (int i = 1; i <= 7; i++)
                {
                    tmpString.Append("," + dayTot[i]);
                    WeeklyTotal = WeeklyTotal + dayTot[i];
                    dayTot[i] = 0;
                }
                tmpString.Append("," + WeeklyTotal);
                WeeklyTotal = 0;
                this.file.WriteLine(tmpString.ToString());
                lineCount = lineCount + 1;
                this.file.Flush();
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
            while (lineCount < 6)
            {
                this.file.WriteLine(" ");
                lineCount = lineCount + 1;
            }
        }





	}
}
