///<header>
///DFT WEEKLY REPORT (REPORT 4)
///Created 01/12/2003
///Author JP Scott
///
///Version	Date		Who	Reason
///1		01/12/2003	PS	Created
///2		20/02/2004  PS Various changes
///3        02/06/2004  PS Incorporatemissing/failed ref transactions
///4	    15/07/2004  PS Incorporate map trans
///5        20/07/2004  PS Incorporate test calendar for map trans
///6        03/08/2004  PS Add Workload event lines
///7        10/08/2004  PS Add adjustment to page event total and correct sunday test
///8        11/08/2004  PS Add Registered User Count
///9		04/10/2004  PS increase testperiod date array
///10       05/11/2004  PS Parameterise the reference target times and type reporting
///11       18/11/2004  PS design changes / increase accuracy of test calendar exclusions
///12       09/12/2004  PS correct missing trans figure v1.0.18
///13       15/06/2005  PS Pass report number when called to allow correct addressing of email
///14       12/04/2006  PS Only report the adjusted road plan events v1.0.28
///
/// 
/// 1.0     19/06/2008  PS Reversioned in PVCS
/// 1.1     06/03/2008  PS Recompile in .net 2 and Partner Changes
/// 1.2     14/05/2008  PS Add New and repeat visitor counts 
/// Versioning now done in PVCS 
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
	/// Class to produce weekly report for the DFT
 	/// analyses all recorded event tables and reports total count per day
	/// </summary>
	/// 
	
	public class DFTWeeklyReport
	{
		
		private DateTime currentDateTime;
		private string reportStartDateString;
		private string reportEndDateString;
		private DateTime reportStartDate;
		private DateTime reportEndDate;
		private DateTime reportEndDatePlus1;
		private string reportEndDatePlus1String;
        private int daysToReportOn;
		

		private EventLog EventLogger;
		private	DailyReportController controller;
		
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
        private string filePathStringDefault;
        private string defaultInjectorString;
        private string backupInjectorString;
        private string travelineInjectorString;

		private int statusCode;
		private DateTime thisDay;
		private string dayOfWeek;
		private int[] dayTot = new int[8];
		private int[] dayGrandTot = new int[8];
		private int WeeklyTotal;
//		private int WeeklyWebPageTotalAdjusted;
		private int WeeklyLoginTotal;
		private int WeeklySessionTotal;
        private int WeeklyNewVisitorTotal;
        private int WeeklyOldVisitorTotal;

		private int WeeklyWorkLoadTotal;
		private int WeeklyWorkLoadTotalAdjusted;
		private int WeeklyLoginTotalAdjusted;
		private int WeeklySessionTotalAdjusted;
        private int WeeklyNewVisitorTotalAdjusted;
        private int WeeklyOldVisitorTotalAdjusted;
        private int WeeklySinglePageSessionTotal;
        private int WeeklyMultiplePageSessionTotal;


		private int[] monthlyDayTot = new int[32];
		private int[,,] DayHrMinCount = new int[8,24,60];  //1-31 - 0-23,0-59
		private int[,,] DayHrMinSurge = new int[8,24,60];  //1-31 - 0-23,0-59
		private int[,,] DayHrMinSuccess = new int[8,24,60];  //1-31 - 0-23,0-59
		

		private int day;
		private int hour;
		private int minute;
		private int lineCount = 0;
		private int missingCount = 0;
		private int endDay = 0;
		private int startHour = 0;
		private int endHour = 0;
		private int startDay = 0;
		private int startMinute = 0;
		private int endMinute = 0;
		
		private int durHour = 0;
		private int durMin = 0;
		private int duration = 0;
		private string strReportDate;
		private string strStartHour; 
		private string strStartMin; 
		private string strEndHour; 
		private string strEndMin;
//		private string strDurDay; 
		private string strDurHour; 
		private string strDurMin; 
//		private int totCount;
//		private int success;
//		private int failureCount;
		
		private string CapacityBandString;
		private int CapacityBand;

        private int totalGP = 0;
        private int totalChartView =0;
        private int totalTableView = 0;

		private int CBMaxRequests;
		private int CBSurgeThreshold;
		private float CBSurgePercentage;

		private int[,] MapCmdCount = new int[6,6];
		private int[,] MapCmdTime  = new int[6,6];
		private string[] mapCommand = new string[6];
		private string[] mapDisplay = new string[6];
		private DateTime[] TestStarted = new DateTime[250]; 
		private DateTime[] TestCompleted = new DateTime[250]; 
		private int testCount;
		private DateTime SDate;
		private DateTime EDate;
		private int SHour,EHour,SMin,EMin ;
		private long ScompareString,EcompareString;
		private StringBuilder excludeString;
		private StringBuilder tempString;
        private string RequestedDateString;
		private string productString;
		private string commandString;
		private int productNum;
		private int commandNum;

		private int NumTypesToReport;
		private int[] RefType         = new int[50];
        private string[] RefTypeCode = new string[50];
        private string[] RefTypeDesc = new string[50];
        private string[] RefChannelDesc = new string[50];
        private int[] NominalCount = new int[50];
        private int[] ActualCount = new int[50];
        private int[] MissingCount = new int[50];
        private int[] MissingPercent  = new int[50];
        private int[] FailedCount = new int[50];
        private int[] FailedPercent = new int[50];
        private int[] AmberCount = new int[50];
        private int[] AmberPercent = new int[50];
        private int[] RedCount = new int[50];
        private int[] RedPercent = new int[50];
        private double[] TIPercent = new double[50];
        private int[, ,] RefCount = new int[50, 8, 24];

        private int NumChannelsToReport;
        private string[] ChannelDesc = new string[10];
        private int[] ChannelNominalCount = new int[10];
        private int[] ChannelActualCount = new int[10];
        private int[] ChannelMissingCount = new int[10];
        private int[] ChannelMissingPercent  = new int[10];
        private int[] ChannelFailedCount = new int[10];
        private int[] ChannelFailedPercent = new int[10];
        private int[] ChannelAmberCount = new int[10];
        private int[] ChannelAmberPercent = new int[10];
        private int[] ChannelRedCount = new int[10];
        private int[] ChannelRedPercent = new int[10];
        private double[] ChannelTIPercent = new double[10];
        
        
        private int[] ResponseTarget  = new int[20];
		
		public DFTWeeklyReport()
		{
			EventLogger = new EventLog("Application");
			EventLogger.Source = "TD.Reporting";
			controller    = new DailyReportController();
            
            ConfigurationManager.GetSection("appSettings");
			propertyConnectionString = ConfigurationManager.AppSettings["ReportProperties.connectionstring"];
			reportingConnectionString = ConfigurationManager.AppSettings["ReportDatabase.connectionstring"];
        }
		
		public int RunReport(DateTime reportDate,int reportNumber)
		{
			statusCode = (int)StatusCode.Success; // success
			string reportDateString;
			reportDateString = reportDate.ToShortDateString();
			currentDateTime = System.DateTime.Now;
			// find current date.
			reportStartDate = reportDate;

            RequestedDateString = reportDate.Year.ToString().Substring(2,2)
                + "-" + reportDate.Month.ToString().PadLeft(2, '0')
                + "-" + reportDate.Day.ToString().PadLeft(2, '0');
           
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


			bool readOK =  controller.ReadProperty(reportNumber.ToString(), "FilePath", out filePathString);
			if (readOK == false || filePathString.Length == 0)
			{
				statusCode = (int)StatusCode.ErrorReadingFilePath; // Error Reading FilePath
				return statusCode;
			}
            filePathStringDefault = filePathString.Replace(" %YY-MM-DD%", "");
            filePathString = filePathString.Replace("%YY-MM-DD%", RequestedDateString);
            
			// open report text file
			file = new StreamWriter( new FileStream(filePathString, System.IO.FileMode.Create ) );
			this.file.WriteLine("------------------------------------------");
			this.file.WriteLine("TRANSPORT DIRECT");
			this.file.WriteLine("DfT Weekly Report");
			this.file.WriteLine("------------------------------------------");
			this.file.WriteLine("");
			this.file.WriteLine("For week starting : " + reportStartDate.ToShortDateString());
			this.file.WriteLine("");
//			this.file.WriteLine("Web Page,Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday");
			this.file.Flush();
			reportingSqlConnection = new SqlConnection(reportingConnectionString);




			selectString = 
				"select TimeStarted,TimeCompleted "
				+ "from dbo.TestCalendar "
				+ "where TimeCompleted >= convert(datetime, '" + reportStartDateString
				+ "',103) and TimeStarted < convert(datetime, '" + reportEndDatePlus1String
				+ "',103) order by TimeStarted";

			
			testCount = 0;
			DateTime TestBegins = new DateTime(1,1,1,0,0,0);
			DateTime TestEnds = new DateTime(1,1,1,0,0,0);
			reportingSqlConnection = new SqlConnection(reportingConnectionString);

			myDataReader = null;
			mySqlCommand = new SqlCommand(selectString, reportingSqlConnection);


			try
			{
				reportingSqlConnection.Open();

				
				myDataReader = mySqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
				while (myDataReader.Read())
				{
					TestBegins  = Convert.ToDateTime(myDataReader.GetDateTime(0).ToString());
					TestEnds  = Convert.ToDateTime(myDataReader.GetDateTime(1).ToString());
					testCount = testCount+1;
					TestStarted[testCount] = TestBegins;
					TestCompleted[testCount] = TestEnds;
				} // end of while

			}
			catch(Exception e)
			{
				EventLogger.WriteEntry ("Failure reading test calendar "+e.Message,EventLogEntryType.Error);
				statusCode = (int)StatusCode.ErrorReadingCalendar; // Failed to calendar
			}
			finally
			{
				// Always call Close when done reading.
				if (myDataReader != null)
					myDataReader.Close();
			}
			reportingSqlConnection.Close();

			if (testCount>0)
			{
				this.file.WriteLine(" ");
				this.file.WriteLine("The following periods of agreed outage/testing occurred in this reporting period");
				this.file.WriteLine("From                   To");
				for(int i=1;i<=testCount;i++)
				{
					this.file.WriteLine(TestStarted[i]+"   "+TestCompleted[i]);
				}
				this.file.WriteLine(" ");
			}

 
			//*******************************
			int transactionCount = 0;
			this.file.WriteLine(",Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday");

/*
			//.............................			
			transactionString = "Page Entry Events";
			selectString = 
				"select PT.PETDescription, PE.PEEDate, isnull(sum(PE.PEECount),0)"
				+ "from dbo.PageEntryEvents PE, dbo.PageEntryType PT where PT.PETID = PE.PEEPETID"
				+ "  and PE.PEEDate >= convert(datetime, '" + reportStartDateString
				+ "',103) and PE.PEEDate <= convert(datetime, '" + reportEndDateString
				+ "',103) group by PT.PETDescription,PE.PEEDate order by PT.PETDescription,PE.PEEDate";

			myDataReader = null;
			mySqlCommand = new SqlCommand(selectString, reportingSqlConnection);
			string webPage;
			string lastWebPage;

			lastWebPage = "";
			webPage="";
			WeeklyWebPageTotal = 0;
			int transactionCount = 0;

			try
			{
				reportingSqlConnection.Open();
				myDataReader = mySqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
				while (myDataReader.Read())
				{
					webPage  = myDataReader.GetString(0);
					if (webPage != lastWebPage)
					{
						if (lastWebPage.Length != 0)
						{
							// print the line on change of web page
							tmpString = new StringBuilder();
							tmpString.Append(lastWebPage);
							for(int i = 1;i <= 7;i++)
							{
								tmpString.Append(",");
								tmpString.Append(dayTot[i].ToString());
							}
							this.file.WriteLine(tmpString.ToString());
							for(int i = 1;i <= 7;i++) dayTot[i] = 0;
						}
						lastWebPage = webPage;
					}
					thisDay = myDataReader.GetDateTime(1);
					transactionCount = Convert.ToInt32(myDataReader.GetSqlInt32(2).ToString());
					dayOfWeek = thisDay.DayOfWeek.ToString();
					switch (dayOfWeek)
					{
						case "Monday":
							dayTot[1] = dayTot[1] + transactionCount;
							dayGrandTot[1] = dayGrandTot[1] + transactionCount;
							break;
						case "Tuesday":
							dayTot[2] = dayTot[2] + transactionCount;
							dayGrandTot[2] = dayGrandTot[2] + transactionCount;
							break;
						case "Wednesday":
							dayTot[3] = dayTot[3] + transactionCount;
							dayGrandTot[3] = dayGrandTot[3] + transactionCount;
							break;
						case "Thursday":
							dayTot[4] = dayTot[4] + transactionCount;
							dayGrandTot[4] = dayGrandTot[4] + transactionCount;
							break;
						case "Friday":
							dayTot[5] = dayTot[5] + transactionCount;
							dayGrandTot[5] = dayGrandTot[5] + transactionCount;
							break;
						case "Saturday":
							dayTot[6] = dayTot[6] + transactionCount;
							dayGrandTot[6] = dayGrandTot[6] + transactionCount;
							break;
						case "Sunday":
							dayTot[7] = dayTot[7] + transactionCount;
							dayGrandTot[7] = dayGrandTot[7] + transactionCount;
							break;

					}
				} // end of while
				// flush the last web page
				tmpString = new StringBuilder();
				tmpString.Append(webPage);
				for(int i = 1;i <= 7;i++)
				{
					tmpString.Append(",");
					tmpString.Append(dayTot[i].ToString());
				}
				this.file.WriteLine(tmpString.ToString());

				this.file.WriteLine(" ");
				
				// write grand tots
				tmpString = new StringBuilder();
				tmpString.Append("Total Daily Web Pages Hits ");
				for(int i = 1;i <= 7;i++)
				{
					tmpString.Append(",");
					tmpString.Append(dayGrandTot[i].ToString());
					WeeklyWebPageTotal = WeeklyWebPageTotal + dayGrandTot[i];
				}
				this.file.WriteLine(tmpString.ToString());
				

				this.file.Flush();
				myDataReader.Close();

			}
			catch(Exception e)
			{
				EventLogger.WriteEntry ("Failure reading page events table "+e.Message,EventLogEntryType.Error);
				statusCode = (int)StatusCode.ErrorReadingTable; // Failed to reading page events table
			}
			finally
			{
				// Always call Close when done reading.
				if (myDataReader != null)
					myDataReader.Close();
			}

			//.............................			
			//.............................			
			// build exclusion string
			SDate  = new DateTime(1,1,1,0,0,0);
			EDate  = new DateTime(1,1,1,0,0,0);
			excludeString   = new StringBuilder();
			for(int i = 1;i<=testCount;i++)
			{
				SDate =  TestStarted[i].Date;
				SHour = TestStarted[i].Hour;
				SMin  = TestStarted[i].Minute;
					
				EDate =  TestCompleted[i].Date;
				EHour = TestCompleted[i].Hour;
				EMin  = TestCompleted[i].Minute;
					
				ScompareString = (long)SDate.Year *1000000L + ((long)SDate.Month *10000L) + ((long)SDate.Day*100L) + (long)SHour; //+(long)SHourQuarter;
				EcompareString = (long)EDate.Year *1000000L + ((long)EDate.Month *10000L) + ((long)EDate.Day*100L) + (long)EHour ;//+(long)EHourQuarter;

				// build exclusion string - nothing allowed within this 			
				excludeString.Append(" and not (");
				excludeString.Append("(year(PE.PEEDate)*1000000+month(PE.PEEDate)*10000+day(PE.PEEDate)*100+PE.PEEHour) >= "+ScompareString);
				excludeString.Append(" and (year(PE.PEEDate)*1000000+month(PE.PEEDate)*10000+day(PE.PEEDate)*100+PE.PEEHour) <= "+EcompareString);
				excludeString.Append(")");
			}

			WeeklyWebPageTotalAdjusted = 0;

			transactionString = "Total Daily Web Pages Hits(Excl. Tests)";
			selectString = 
				"select PE.PEEDate, isnull(sum(PE.PEECount),0) "
				+ "from dbo.PageEntryEvents PE, dbo.PageEntryType PT "
				+ "where PT.PETID = PE.PEEPETID "
				+ "and PE.PEEDate >= convert(datetime, '" + reportStartDateString
				+ "',103) and PE.PEEDate <= convert(datetime, '" + reportEndDateString
				+ "',103) "
				+excludeString
				+ " group by PE.PEEDate order by PE.PEEDate";
			DailyEventBreakDown();
			WeeklyWebPageTotalAdjusted = WeeklyTotal;
*/			
			this.file.WriteLine(" ");
			transactionString = "Map Events (pans/zooms/etc.)";
			selectString = "select MEDate, isnull(sum(MECount),0)from dbo.MapEvents "
				+ "where MEDate >= convert(datetime, '" + reportStartDateString 
				+ "',103) and MEDate <= convert(datetime, '" + reportEndDateString
				+ "',103) group by MEDate order by MEDate";
			DailyEventBreakDown();


			//.............................			
			// build exclusion string
			SDate  = new DateTime(1,1,1,0,0,0);
			EDate  = new DateTime(1,1,1,0,0,0);
			excludeString   = new StringBuilder();
			for(int i = 1;i<=testCount;i++)
			{
				SDate =  TestStarted[i].Date;
				SHour = TestStarted[i].Hour;
				SMin  = TestStarted[i].Minute;
					
				EDate =  TestCompleted[i].Date;
				EHour = TestCompleted[i].Hour;
				EMin  = TestCompleted[i].Minute;
				SMin = SMin/15;
				EMin = EMin/15;
				ScompareString = ((long)SDate.Year-2000) *100000000L + ((long)SDate.Month *1000000L) + ((long)SDate.Day*10000L) + ((long)SHour*100) +(long)SMin;
				EcompareString = ((long)EDate.Year-2000) *100000000L + ((long)EDate.Month *1000000L) + ((long)EDate.Day*10000L) + ((long)EHour*100) +(long)EMin;

				// build exclusion string - nothing allowed within this 			
				excludeString.Append(" and not (");
				excludeString.Append("((year(MEDate)-2000)*100000000+month(MEDate)*1000000+day(MEDate)*10000+MEHour*100 + MEHourQuarter) >= "+ScompareString);
				excludeString.Append(" and ((year(MEDate)-2000)*100000000+month(MEDate)*1000000+day(MEDate)*10000+MEHour*100 + MEHourQuarter) <= "+EcompareString);
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
			

			
			//...............................................

			transactionString = "Workload  (Total Load)";
			selectString = "select WEDate, isnull(sum(WECount),0)from dbo.WorkloadEvents "
				+ "where WEDate >= convert(datetime, '" + reportStartDateString 
				+ "',103) and WEDate <= convert(datetime, '" + reportEndDateString
				+ "',103) group by WEDate order by WEDate";
			DailyEventBreakDown();
			WeeklyWorkLoadTotal = WeeklyTotal;

			

			// build exclusion string
			SDate  = new DateTime(1,1,1,0,0,0);
			EDate  = new DateTime(1,1,1,0,0,0);
			excludeString   = new StringBuilder();
			for(int i = 1;i<=testCount;i++)
			{
				SDate =  TestStarted[i].Date;
				SHour = TestStarted[i].Hour;
				SMin  = TestStarted[i].Minute;
					
				EDate =  TestCompleted[i].Date;
				EHour = TestCompleted[i].Hour;
				EMin  = TestCompleted[i].Minute;
					
				ScompareString = ((long)SDate.Year-2000) *100000000L + ((long)SDate.Month *1000000L) + ((long)SDate.Day*10000L) + ((long)SHour*100) +(long)SMin;
				EcompareString = ((long)EDate.Year-2000) *100000000L + ((long)EDate.Month *1000000L) + ((long)EDate.Day*10000L) + ((long)EHour*100) +(long)EMin;

				// build exclusion string - nothing allowed within this 			
				excludeString.Append(" and not (");
				excludeString.Append("((year(WEDate)-2000)*100000000+month(WEDate)*1000000+day(WEDate)*10000+WEHour*100+WEMinute) >= "+ScompareString);
				excludeString.Append(" and ((year(WEDate)-2000)*100000000+month(WEDate)*1000000+day(WEDate)*10000+WEHour*100+WEMinute) <= "+EcompareString);
				excludeString.Append(")");
			}

			transactionString = "Workload (Excluding Tests)";
			selectString = "select WEDate, isnull(sum(WECount),0)from dbo.WorkloadEvents "
				+ "where WEDate >= convert(datetime, '" + reportStartDateString 
				+ "',103) and WEDate <= convert(datetime, '" + reportEndDateString
				+ "',103) "
				+ excludeString 
				+ " group by WEDate order by WEDate";
			DailyEventBreakDown();
			WeeklyWorkLoadTotalAdjusted = WeeklyTotal;

/*
						
			transactionString = "Workload Max Rate (Excluding Tests)";

			selectString = "select WEDate, isnull((Max(WECount*100)),0)from dbo.WorkloadEvents "
				+ "where WEDate >= convert(datetime, '" + reportStartDateString 
				+ "',103) and WEDate <= convert(datetime, '" + reportEndDateString
				+ "',103) "
				+ excludeString 
				+ " group by WEDate order by WEDate";

			tmpString = new StringBuilder();
			tmpString.Append(transactionString);
			myDataReader = null;
			mySqlCommand = new SqlCommand(selectString, reportingSqlConnection);
			transactionCount = 0;
			for(int i = 1;i <= 7;i++) dayTot[i] = 0;
			try
			{
				reportingSqlConnection.Open();
				myDataReader = mySqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
				while (myDataReader.Read())
				{
					thisDay = myDataReader.GetDateTime(0);
					transactionCount = Convert.ToInt32(myDataReader.GetSqlInt32(1).ToString());
					dayOfWeek = thisDay.DayOfWeek.ToString();
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
				double ddaytot = 0;
				for(int i = 1;i <= 7;i++)
				{
					ddaytot = (double)dayTot[i]/6000;
					tmpString.Append(",");
					tmpString.Append(ddaytot.ToString("#####.##"));
				}

				this.file.WriteLine(tmpString.ToString());
					
				this.file.Flush();
				myDataReader.Close();

			}
			catch(Exception e)
			{
				EventLogger.WriteEntry ("Error reading "+transactionString+ " "+e.Message,EventLogEntryType.Error);

				statusCode = (int)StatusCode.ErrorReadingTable; // Failed reading table
			}
			finally
			{
				// Always call Close when done reading.
				if (myDataReader != null)
					myDataReader.Close();
			}
		


*/

            //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

            //.............................		
            this.file.WriteLine(" ");
            this.file.WriteLine("Weekly Count of User Sessions and Logins");
            this.file.WriteLine(" ,Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday");


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


            transactionString = "User Sessions ";
            selectString = "select SEDate, isnull(sum(SECount),0)from dbo.SessionEvents "
                + "where SEDate >= convert(datetime, '" + reportStartDateString
                + "',103) and SEDate <= convert(datetime, '" + reportEndDateString
                + "',103) group by SEDate order by SEDate";
            DailyEventBreakDown();
            WeeklySessionTotal = WeeklyTotal;

            //<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

      
//>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

			//.............................		
			transactionString = "Single Page Sessions ";
            selectString = "select SDADate, isnull(sum(SDASinglePageSessions ),0)from dbo.SessionDailyAnalysis "
				+ "where SDADate >= convert(datetime, '" + reportStartDateString 
				+ "',103) and SDADate <= convert(datetime, '" + reportEndDateString
				+ "',103) group by SDADate order by SDADate";
			DailyEventBreakDown();
			WeeklySinglePageSessionTotal = WeeklyTotal;


            transactionString = "Multi-Page Sessions ";
            selectString = "select SDADate, isnull(sum(SDAMultiplePageSessions ),0)from dbo.SessionDailyAnalysis "
                + "where SDADate >= convert(datetime, '" + reportStartDateString
                + "',103) and SDADate <= convert(datetime, '" + reportEndDateString
                + "',103) group by SDADate order by SDADate";
            DailyEventBreakDown();
            WeeklyMultiplePageSessionTotal = WeeklyTotal;

            
            transactionString = "Average Session Duration (secs) ";
            selectString = "select SDADate, isnull(sum(SDAAverageDuration),0)from dbo.SessionDailyAnalysis "
                + "where SDADate >= convert(datetime, '" + reportStartDateString
                + "',103) and SDADate <= convert(datetime, '" + reportEndDateString
                + "',103) group by SDADate order by SDADate";
            DailyEventBreakDown();

            transactionString = "Average Multi-Page Session Duration (secs) ";
            selectString = "select SDADate, isnull(sum(SDAAverageDurationMultiplePageSessions),0)from dbo.SessionDailyAnalysis "
                + "where SDADate >= convert(datetime, '" + reportStartDateString
                + "',103) and SDADate <= convert(datetime, '" + reportEndDateString
                + "',103) group by SDADate order by SDADate";
            DailyEventBreakDown();
         

//<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<

            transactionString = "User Sessions (Excluding Tests)";
            selectString = "select SEDate, isnull(sum(SECount),0)from dbo.SessionEvents "
                + "where SEDate >= convert(datetime, '" + reportStartDateString
                + "',103) and SEDate <= convert(datetime, '" + reportEndDateString
                + "',103) "
                + excludeString
                + " group by SEDate order by SEDate";
            DailyEventBreakDown();
            WeeklySessionTotalAdjusted = WeeklyTotal;





            transactionString = "New Visitors ";
            selectString = "select RVEDate, isnull(sum(RVECount),0)from dbo.RepeatVisitorEvents "
                + "where RVEDate >= convert(datetime, '" + reportStartDateString
                + "',103) and RVEDate <= convert(datetime, '" + reportEndDateString
                + "',103) and RVERVTID = 2" 
                + " group by RVEDate order by RVEDate";
            DailyEventBreakDown();
            WeeklyNewVisitorTotal = WeeklyTotal;

            transactionString = "New Visitors (Excluding Tests)";
            selectString = "select RVEDate, isnull(sum(RVECount),0)from dbo.RepeatVisitorEvents "
                + "where RVEDate >= convert(datetime, '" + reportStartDateString
                + "',103) and RVEDate <= convert(datetime, '" + reportEndDateString
                + "',103) "
                + excludeString
                + " and RVERVTID = 2"
                + " group by RVEDate order by RVEDate";
            DailyEventBreakDown();
            WeeklyNewVisitorTotalAdjusted = WeeklyTotal;

            transactionString = "Repeat Visitors ";
            selectString = "select RVEDate, isnull(sum(RVECount),0)from dbo.RepeatVisitorEvents "
                + "where RVEDate >= convert(datetime, '" + reportStartDateString
                + "',103) and RVEDate <= convert(datetime, '" + reportEndDateString
                + "',103) and RVERVTID = 3"
                + " group by RVEDate order by RVEDate";
            DailyEventBreakDown();
            WeeklyOldVisitorTotal = WeeklyTotal;

            transactionString = "Repeat Visitors (Excluding Tests)";
            selectString = "select RVEDate, isnull(sum(RVECount),0)from dbo.RepeatVisitorEvents "
                + "where RVEDate >= convert(datetime, '" + reportStartDateString
                + "',103) and RVEDate <= convert(datetime, '" + reportEndDateString
                + "',103) "
                + excludeString
                + " and RVERVTID = 3"
                + " group by RVEDate order by RVEDate";
            DailyEventBreakDown();
            WeeklyOldVisitorTotalAdjusted = WeeklyTotal;

			transactionString = "Logins ";
			selectString = "select LEDate, isnull(sum(LECount),0)from dbo.LoginEvents "
				+ "where LEDate >= convert(datetime, '" + reportStartDateString 
				+ "',103) and LEDate <= convert(datetime, '" + reportEndDateString
				+ "',103) group by LEDate order by LEDate";
			DailyEventBreakDown();
			WeeklyLoginTotal = WeeklyTotal;


			// build exclusion string
			SDate  = new DateTime(1,1,1,0,0,0);
			EDate  = new DateTime(1,1,1,0,0,0);
			excludeString   = new StringBuilder();
			for(int i = 1;i<=testCount;i++)
			{
				SDate =  TestStarted[i].Date;
				SHour = TestStarted[i].Hour;
				SMin  = TestStarted[i].Minute;
					
				EDate =  TestCompleted[i].Date;
				EHour = TestCompleted[i].Hour;
				EMin  = TestCompleted[i].Minute;
				SMin = SMin/15;
				EMin = EMin/15;
				ScompareString = ((long)SDate.Year-2000) *100000000L + ((long)SDate.Month *1000000L) + ((long)SDate.Day*10000L) + (long)SHour*100L +(long)SMin;
				EcompareString = ((long)EDate.Year-2000) *100000000L + ((long)EDate.Month *1000000L) + ((long)EDate.Day*10000L) + (long)EHour*100L +(long)EMin;

				// build exclusion string - nothing allowed within this 			
				excludeString.Append(" and not (");
				excludeString.Append("((year(LEDate)-2000)*100000000+month(LEDate)*1000000+day(LEDate)*10000+LEHour*100+LEHourQuarter) >= "+ScompareString);
				excludeString.Append(" and ((year(LEDate)-2000)*100000000+month(LEDate)*1000000+day(LEDate)*10000+LEHour*100+LEHourQuarter) <= "+EcompareString);
				excludeString.Append(")");
			}


			transactionString = "Logins (Excluding Tests)";
			selectString = "select LEDate, isnull(sum(LECount),0)from dbo.LoginEvents "
				+ "where LEDate >= convert(datetime, '" + reportStartDateString 
				+ "',103) and LEDate <= convert(datetime, '" + reportEndDateString
				+ "',103) "
				+ excludeString
				+ " group by LEDate order by LEDate";
			DailyEventBreakDown();
			WeeklyLoginTotalAdjusted = WeeklyTotal;

			transactionString = "No. of Registed Users ";
			selectString = "SELECT CAST(FLOOR(CAST(Date_of_survey AS float(53))) AS datetime) , MAX(Registered_Users) "
				+ "FROM UserNumbers "
				+ "where CAST(FLOOR(CAST(Date_of_survey AS float(53))) AS datetime) >= convert(datetime, '" + reportStartDateString 
				+ "',103) and CAST(FLOOR(CAST(Date_of_survey AS float(53))) AS datetime) <= convert(datetime, '" + reportEndDateString
				+ "',103) "
				+ "GROUP BY CAST(FLOOR(CAST(Date_of_survey AS float(53))) AS datetime) "
				+ "ORDER BY CAST(FLOOR(CAST(Date_of_survey AS float(53))) AS datetime)";
			DailyEventBreakDown();


			//.............................			
			this.file.WriteLine(" ");
			this.file.WriteLine(" ");
			this.file.WriteLine("Summary Totals for Week ");
			this.file.WriteLine(" ");
//			this.file.WriteLine("Total Web Pages visited          : "+WeeklyWebPageTotal.ToString());
//			this.file.WriteLine("Total Web Page visits(Excl.Tests): "+WeeklyWebPageTotalAdjusted.ToString());
			this.file.WriteLine("Total WorkLoad Hits              : "+WeeklyWorkLoadTotal.ToString());
			this.file.WriteLine("Total WorkLoad Hits(Excl. Tests) : "+WeeklyWorkLoadTotalAdjusted.ToString());
			this.file.WriteLine("Total User Logins                : "+WeeklyLoginTotal.ToString());
			this.file.WriteLine("Total User Logins  (Excl. Tests) : "+WeeklyLoginTotalAdjusted.ToString());
			this.file.WriteLine("Total User Sessions              : " + WeeklySessionTotal.ToString());
            this.file.WriteLine("Total Single Page Sessions       : " + WeeklySinglePageSessionTotal.ToString());
            this.file.WriteLine("Total Multi-Page Sessions        : " + WeeklyMultiplePageSessionTotal.ToString());
            this.file.WriteLine("Total User Sessions (Excl. Tests): " + WeeklySessionTotalAdjusted.ToString());
            this.file.WriteLine("Total New Visitors               : "+WeeklyNewVisitorTotal.ToString());
            this.file.WriteLine("Total New Visitors (Excl. Tests) : "+WeeklyNewVisitorTotalAdjusted.ToString());
            this.file.WriteLine("Total Repeat Visitors            : "+WeeklyOldVisitorTotal.ToString());
            this.file.WriteLine("Total Repeat Visitors(Excl.Tests): "+WeeklyOldVisitorTotalAdjusted.ToString());
            this.file.WriteLine(" ");


			reportingSqlConnection = new SqlConnection(reportingConnectionString);

			
			for (day = 1; day <= 7;day++)
			{
				for (hour = 0; hour <= 23;hour++)
				{
					for (minute = 0; minute <= 59;minute++)
					{
						DayHrMinCount[day,hour,minute] = 0;
					}
				}
			}

			// look for reference events-
			// find which reference events need reporting on

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

            
selectString = "select rte.RTERTTID,rtt.rttcode,rtt.rttdescription,"
            + "rte.RTESuccessful,"
            + "rte.RTEMsDuration,"
            + "rtt.RTTAmberMsDuration,"
            + "rtt.RTTMaxMsDuration,"
            + "rtt.RTTChannel,rtt.RTTInjectionFrequency "
            + "from dbo.ReferenceTransactionEvents rte "
            + "inner join dbo.ReferenceTransactionType rtt on rtt.rttid = rte.rterttid "
            + "where RTEDate >= convert(datetime, '" + reportStartDateString 
				+ "',103) and RTEDate <= convert(datetime, '" + reportEndDateString
				+ "',103) "
            + "and RTT.RTTSLAInclude = 1 and RTE.rterttid >= 51 "
            + "and ( RTE.RTEMachineName not like '" + backupInjectorString + "' ) "
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
                NominalCount[i]  = 0;
                ActualCount[i]   = 0;
                MissingCount[i]  = 0;
                FailedCount[i]   = 0;
                FailedPercent[i] = 0;
                AmberCount[i]    = 0;
                AmberPercent[i]  = 0;
                RedCount[i]      = 0;
                RedPercent[i] = 0;
                TIPercent[i] = 0;
            }
            
            while (myDataReader.Read())
			{
                int id =   Convert.ToInt32(myDataReader.GetSqlByte(0).ToString());
                bool isNewtype = true;
                // is this a new type or old
                for (int i = 0;i<= NumTypesToReport;i++)
                {
                    if (RefType[i] == id)
                    {
                        isNewtype = false;
                        i=NumTypesToReport;
                    }
                }
                if (isNewtype)
                {
                     NumTypesToReport++;
                }
                RefType[NumTypesToReport] = id;
                RefTypeCode[NumTypesToReport] = myDataReader.GetString(1);
                RefTypeDesc[NumTypesToReport] = myDataReader.GetString(2);
                int RefPassed = Convert.ToInt32(myDataReader.GetSqlByte(3).ToString());
                 if (RefPassed==0) FailedCount[NumTypesToReport]++;
                ActualCount[NumTypesToReport]++;
                int refduration = Convert.ToInt32(myDataReader.GetInt32(4).ToString());
                int amber = Convert.ToInt32(myDataReader.GetInt32(5).ToString());
                int red = Convert.ToInt32(myDataReader.GetInt32(6).ToString());
                RefChannelDesc[NumTypesToReport] = myDataReader.GetString(7);
                int frq = Convert.ToInt32(myDataReader.GetSqlByte(8).ToString());


                //How many days to reportDate?

                if (reportEndDate <= DateTime.Today)
                {
                    daysToReportOn = 7;
                }
                else
                {
                        TimeSpan ts = reportDate.Subtract(reportStartDate); // both are DateTime
                        daysToReportOn = ts.Days+1;
                }


                NominalCount[NumTypesToReport] = (daysToReportOn * 1440 / frq);



               
                if (NominalCount[NumTypesToReport] > ActualCount[NumTypesToReport])
                {
                    MissingCount[NumTypesToReport] = NominalCount[NumTypesToReport] - ActualCount[NumTypesToReport];
                    MissingPercent[NumTypesToReport] = (((MissingCount[NumTypesToReport] * 1000) / NominalCount[NumTypesToReport])+5)/10;
                }
                else
                {
                    MissingCount[NumTypesToReport] = 0;
                    MissingPercent[NumTypesToReport] = 0;
                }

                FailedPercent[NumTypesToReport] = (((FailedCount[NumTypesToReport] * 1000) / NominalCount[NumTypesToReport])+5)/10;


                if (red != 0  && RefPassed!=0 )
                {
                    if (red - refduration < 0) 
                    {
                        RedCount[NumTypesToReport]++;
                    }
                    else
                    {
                        if (amber - refduration < 0)
                        {
                            AmberCount[NumTypesToReport]++;
                        }
                    }
                    AmberPercent[NumTypesToReport] = (((AmberCount[NumTypesToReport] * 1000) / NominalCount[NumTypesToReport]) + 5) / 10;
                    RedPercent[NumTypesToReport] = (((RedCount[NumTypesToReport] * 1000) / NominalCount[NumTypesToReport]) + 5) / 10;

                    int PassedCount = NominalCount[NumTypesToReport]
                                    - MissingCount[NumTypesToReport]
                                    - AmberCount[NumTypesToReport]
                                    - FailedCount[NumTypesToReport]
                                    - RedCount[NumTypesToReport];
                    if (NominalCount[NumTypesToReport] > 0)
                    {
                      
                        TIPercent[NumTypesToReport] =
    (((Double)(PassedCount * 100000) / (Double)NominalCount[NumTypesToReport] + 5) / (Double)1000);

                    }
                    else
                    {
                        TIPercent[NumTypesToReport] = 0;
                    }
                }
                else
                {
                    AmberCount[NumTypesToReport]=0;
                    RedCount[NumTypesToReport]=0;
                    AmberPercent[NumTypesToReport] = 0;
                    RedPercent[NumTypesToReport] = 0;
                    int PassedCount = NominalCount[NumTypesToReport]
                                    - MissingCount[NumTypesToReport]
                                    - FailedCount[NumTypesToReport];
                    if (NominalCount[NumTypesToReport] > 0)
                    {
                        TIPercent[NumTypesToReport] =
                            (((Double)(PassedCount * 100000) / (Double)NominalCount[NumTypesToReport] + 5) / (double)1000);
                    }
                    else
                    {
                        TIPercent[NumTypesToReport] = 0;
                    }
                    
                }
                
             
            }
			myDataReader.Close();





            this.file.WriteLine("");
            this.file.WriteLine("");
            this.file.WriteLine("------------------------------------------");
            this.file.WriteLine("Weekly Transaction Performance ");
            this.file.WriteLine("------------------------------------------");
            this.file.WriteLine("");
            this.file.WriteLine("");
            this.file.WriteLine("Transaction,Code,Nominal,Actual,Missing,Missing,Failed,Failed,Amber,Amber,Red,Red,TI Performance");
            this.file.WriteLine("Description,Code,Count,Count,Count,(%),Count,(%),Count,(%),Count,(%),(%)");
            this.file.Flush();

            
			for(int i=1 ;i<=NumTypesToReport;i++)
            {
                tempString = new StringBuilder();
                tempString.Append(RefTypeDesc[i] + ",");
                tempString.Append(" "+RefTypeCode[i]+",");
                tempString.Append(NominalCount[i]+",");
                tempString.Append(ActualCount[i] + ",");
                tempString.Append(MissingCount[i] + ",");
                tempString.Append(MissingPercent[i]+",");
                tempString.Append(FailedCount[i]+",");
                tempString.Append(FailedPercent[i]+",");
                
                tempString.Append(AmberCount[i]+",");
                tempString.Append(AmberPercent[i]+",");
                tempString.Append(RedCount[i]+",");
                tempString.Append(RedPercent[i] + ",");
                tempString.Append(Math.Round(TIPercent[i],2));


                this.file.WriteLine(tempString);
			}


            this.file.WriteLine("");
            this.file.WriteLine("");
            this.file.WriteLine("Note: Failed means the transaction did not return a valid response");
            this.file.WriteLine("      Amber means the transaction returned a valid response but took longer than the Amber threshold (but under the Red Threshold)");
            this.file.WriteLine("      Red means the transaction returned a valid response but took longer than the Red threshold");
            this.file.WriteLine("      All figures are mutually exclusive");
            this.file.WriteLine("");

            
            
            this.file.Flush();





   selectString = "select rttchannel,sum(1440/rttInjectionfrequency) "
            + "from dbo.ReferenceTransactionType "
            + "where RTTSLAInclude = 1 and rttid > 50 "
            + "group by rttchannel order by rttchannel";

            reportingSqlConnection = new SqlConnection(reportingConnectionString);
			myDataReader = null;
			mySqlCommand = new SqlCommand(selectString, reportingSqlConnection);

			reportingSqlConnection.Open();
			
			myDataReader = mySqlCommand.ExecuteReader(CommandBehavior.CloseConnection);

            NumChannelsToReport = 0;
            for (int i = 0; i < 10; i++)
            {
                ChannelDesc[i] = " ";
                ChannelNominalCount[i] = 0;
                ChannelActualCount[i] = 0;
                ChannelMissingCount[i] = 0;
                ChannelMissingPercent[i] = 0;
                ChannelFailedCount[i] = 0;
                ChannelFailedPercent[i] = 0;
                ChannelAmberCount[i] = 0;
                ChannelAmberPercent[i] = 0;
                ChannelRedCount[i] = 0;
                ChannelRedPercent[i] = 0;
                ChannelTIPercent[i] = 0;
            }
           
            while (myDataReader.Read())
			{
                ChannelDesc[NumChannelsToReport] = myDataReader.GetString(0);
                int frq = Convert.ToInt32(myDataReader.GetInt32(1).ToString());
                 ChannelNominalCount[NumChannelsToReport] = (daysToReportOn * frq);
                 NumChannelsToReport++;
            }
			myDataReader.Close();

            for (int i = 1; i <= NumTypesToReport; i++)
            {
                for (int j = 0; j <= NumChannelsToReport; j++)
                {
                    if (RefChannelDesc[i] == ChannelDesc[j])
                    {
                        ChannelActualCount[j] += ActualCount[i];
                        ChannelMissingCount[j] += MissingCount[i];
                        ChannelFailedCount[j] += FailedCount[i];
                        ChannelAmberCount[j] += AmberCount[i];
                        ChannelRedCount[j] += RedCount[i];
                        
                        ChannelMissingPercent[j] =
                        (((ChannelMissingCount[j] * 1000) / ChannelNominalCount[j]) + 5) / 10;
                        
                        ChannelFailedPercent[j] =
                        (((ChannelFailedCount[j] * 1000) / ChannelNominalCount[j]) + 5) / 10;
                       
                        ChannelAmberPercent[j] =
                        (((ChannelAmberCount[j] * 1000) / ChannelNominalCount[j]) + 5) / 10;
                        
                        ChannelRedPercent[j] =
                        (((ChannelRedCount[j] * 1000) / ChannelNominalCount[j]) + 5) / 10;

                        int PassedCount = ChannelNominalCount[j] - ChannelMissingCount[j]
                                                            - ChannelAmberCount[j]
                                                            - ChannelFailedCount[j]
                                                            - ChannelRedCount[j];
                        ChannelTIPercent[j] =
                      (((double)(PassedCount * 100000) /  (double)ChannelNominalCount[j] + 5) / (double)1000);
                    }
                }
            }

            this.file.WriteLine("");
            this.file.WriteLine("");
            this.file.WriteLine("------------------------------------------");
            this.file.WriteLine("Weekly Channel Performance ");
            this.file.WriteLine("------------------------------------------");
            this.file.WriteLine("");
            this.file.WriteLine("");
            this.file.WriteLine("Channel,Nominal,Actual,Missing,Missing,Failed,Failed,Amber,Amber,Red,Red,TI Performance");
            this.file.WriteLine("Description,Count,Count,Count,(%),Count,(%),Count,(%),Count,(%),(%)");
            this.file.Flush();
            
            for (int i = 0; i < NumChannelsToReport; i++)
            {
                tempString = new StringBuilder();
                tempString.Append(ChannelDesc[i] + ",");
                tempString.Append(ChannelNominalCount[i] + ",");
                tempString.Append(ChannelActualCount[i] + ",");
                tempString.Append(ChannelMissingCount[i] + ",");
                tempString.Append(ChannelMissingPercent[i] + ",");
                tempString.Append(ChannelFailedCount[i] + ",");
                tempString.Append(ChannelFailedPercent[i] + ",");
                tempString.Append(ChannelAmberCount[i] + ",");
                tempString.Append(ChannelAmberPercent[i] + ",");
                tempString.Append(ChannelRedCount[i] + ",");
                tempString.Append(ChannelRedPercent[i] + ",");
                tempString.Append(Math.Round(ChannelTIPercent[i],2));
                this.file.WriteLine(tempString);
            }

            this.file.Flush();






			//.............................			

            readOK = controller.ReadProperty("0", "BackupInjector", out backupInjectorString);
            if (readOK == false || defaultInjectorString.Length == 0)
            {
                statusCode = (int)StatusCode.ErrorReadingFilePathDefaultInjector; // Error Reading FilePath
                return statusCode;
            }

            // look for reference types-


            // find which reference events need reporting on
            selectString = "select rttid,rttcode,rttdescription,60/RTTInjectionFrequency "
                        + "from dbo.ReferenceTransactionType "
                        + "where RTTSLAInclude = 1 and rttid >= 51 ";

            reportingSqlConnection = new SqlConnection(reportingConnectionString);


            myDataReader = null;
            mySqlCommand = new SqlCommand(selectString, reportingSqlConnection);

            reportingSqlConnection.Open();

            myDataReader = mySqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
            NumTypesToReport = 0;

            for (NumTypesToReport = 0; NumTypesToReport < 50; NumTypesToReport++)
            {
                RefType[NumTypesToReport] = 0;
                RefTypeCode[NumTypesToReport] = null;
                RefTypeDesc[NumTypesToReport] = null;
                NominalCount[NumTypesToReport] = 0;
                for (int day = 0; day < 7; day++)
                {
                    for (int hour = 0; hour < 7; hour++)
                    {
                        RefCount[NumTypesToReport, day, hour] = 0;

                    }
                }
            }
            NumTypesToReport = 0;
            while (myDataReader.Read())
            {
                RefType[NumTypesToReport] = Convert.ToInt32(myDataReader.GetSqlByte(0).ToString());
                RefTypeCode[NumTypesToReport] = myDataReader.GetString(1);
                RefTypeDesc[NumTypesToReport] = myDataReader.GetString(2);
                NominalCount[NumTypesToReport] = Convert.ToInt32(myDataReader.GetInt32(3).ToString());
                NumTypesToReport++;

            }
            myDataReader.Close();


            selectString = "select RTE.rterttid,"
                        + "(DATEDIFF([day], CONVERT(datetime, '"
                        + reportStartDateString + "', 105),rte.rtedate)+1),"
                        + "rte.rtehour,count(*)"
                        + "from dbo.ReferenceTransactionEvents rte "
                        + "inner join dbo.ReferenceTransactionType rtt on rtt.rttid = rte.rterttid "
                        + "where RTEDate >= convert(datetime, '" + reportStartDateString
                            + "',103) and RTEDate <= convert(datetime, '" + reportEndDateString
                            + "',103) "
                        + "and RTT.RTTSLAInclude = 1 and RTE.rterttid >= 50 "
                        + "and ( RTE.RTEMachineName not like '" + backupInjectorString + "' ) "
                        + "group by RTE.rterttid,rte.rtedate,rte.rtehour "
                        + "order by RTE.rterttid,rte.rtedate,rte.rtehour ";


            reportingSqlConnection = new SqlConnection(reportingConnectionString);


            myDataReader = null;
            mySqlCommand = new SqlCommand(selectString, reportingSqlConnection);

            reportingSqlConnection.Open();

            myDataReader = mySqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
            NumTypesToReport = 0;

            while (myDataReader.Read())
            {
                int id = Convert.ToInt32(myDataReader.GetSqlByte(0).ToString());
                day = Convert.ToInt32(myDataReader.GetInt32(1).ToString());
                hour = Convert.ToInt32(myDataReader.GetSqlByte(2).ToString());


                for (int i = 0; i < 50; i++)
                {
                    if (id == RefType[i])
                    {
                        RefCount[i, day, hour] = Convert.ToInt32(myDataReader.GetInt32(3).ToString());
                        id = 50;
                    }
                }
            }
            myDataReader.Close();


            // at this point analyse array and deduct theoretical count of transactions so we end up with deficit

            this.file.WriteLine("");
            this.file.WriteLine("");
            this.file.WriteLine("------------------------------------------");
            this.file.WriteLine("Weekly Missing Transactions Report ");
            this.file.WriteLine("------------------------------------------");
            this.file.WriteLine("");
            this.file.WriteLine("");
            this.file.WriteLine("Transaction Description,Code,Day,Hour,Missed Count");
            this.file.Flush();





            missingCount = 0;
            for (int i = 0; i < 50; i++)
            {
                for (int day = 1; day <= daysToReportOn; day++)
                {
                    for (int hour = 0; hour < 24; hour++)
                    {
                        if (RefCount[i, day, hour] != NominalCount[i])
                        {
                            tempString = new StringBuilder();
                            if (RefType[i] >= 51)
                            {
                                tempString.Append(RefTypeDesc[i] + ",");
                                tempString.Append(RefTypeCode[i] + ",");
                                RefType[i] = 0;
                            }
                            else
                            {
                                string blankString = "                                                  ";
                                tempString.Append(blankString.Remove(RefTypeDesc[i].Length, 50 - RefTypeDesc[i].Length) + ",     ,");
                            }

                            reportDate = reportStartDate.AddDays((day - 1));
                            strReportDate = reportDate.ToShortDateString();
                            tempString.Append(strReportDate + ",");
                            tempString.Append(hour + ",");
                            tempString.Append(NominalCount[i] - RefCount[i, day, hour]);

                            this.file.WriteLine(tempString);
                            missingCount = 1;
                        }
                    }
                }
            }
            if (missingCount == 0)
            {
                this.file.WriteLine("There are no missing Transactions this week"); 
            }


            this.file.Flush();

            //*************************************************************************************************************************

            
            
            
            
            
            
            
            
            //surges this week

			// open report properties DB and read in the current capacity band.
			
			readOK =  controller.ReadProperty("0", "CurrentCapacityBand", out CapacityBandString);
			if (readOK == false)
			{
				statusCode = (int)StatusCode.ErrorReadingCapacityBand; // Error Reading Capacity Band Property
				return statusCode;
			}
			CapacityBand = Convert.ToInt32(CapacityBandString, CultureInfo.CurrentCulture);


			// open report text file

            this.file.WriteLine("");
            this.file.WriteLine("");
            this.file.WriteLine("----------------");
            this.file.WriteLine("Surge Analysis");
			this.file.WriteLine("----------------");
            this.file.WriteLine("");
			this.file.WriteLine("The periods detailed below exceeded the surge threshold");
			this.file.WriteLine("for a period of 15 minutes or more. ");
			this.file.WriteLine("(Based on capacity band " + CapacityBandString + " values.)");
			this.file.WriteLine("");
			this.file.WriteLine("Date        Start   End    Duration");
			this.file.WriteLine("----------  -----   -----  ---------");
			this.file.Flush();

			reportingSqlConnection = new SqlConnection(reportingConnectionString);

			
		

			for (day = 1; day <= 7;day++)
			{
				for (hour = 0; hour <= 23;hour++)
				{
					for (minute = 0; minute <= 59;minute++)
					{
						DayHrMinCount[day,hour,minute] = 0;
					}
				}
			}
			

			// extract current capacity band CBMaxRequests
			// and CBSurgePercentage

			selectString = "select CBMAxRequests,CBSurgePercentage from CapacityBands where CBNumber = '" + CapacityBand + "'";
			reportingSqlConnection = new SqlConnection(reportingConnectionString);

			myDataReader = null;
			mySqlCommand = new SqlCommand(selectString, reportingSqlConnection);

			try
			{
				reportingSqlConnection.Open();
				myDataReader = mySqlCommand.ExecuteReader(CommandBehavior.CloseConnection);

				// Always call Read before accessing data.

				while (myDataReader.Read())
				{
					CBMaxRequests = Convert.ToInt32(myDataReader.GetSqlInt32(0).ToString());
					CBSurgePercentage = (float) Convert.ToDecimal( myDataReader.GetSqlDecimal(1).ToString());
				}
			}
			catch(Exception e)
			{
				EventLogger.WriteEntry ("Error Reading Capacity Band table"+e.Message,EventLogEntryType.Error);
				statusCode = (int)StatusCode.ErrorReadingTable; // Failed to read from property table
			}
			finally
			{
				// Always call Close when done reading.
				if (myDataReader != null)
					myDataReader.Close();

				// Close the connection when done with it.
				if (reportingSqlConnection.State == ConnectionState.Open)
					reportingSqlConnection.Close();
			}

			// work out threshold for 1 minute threshold figure for capacity band are per week
			CBSurgeThreshold = Convert.ToInt32(Math.Floor( ((CBMaxRequests/6)*0.2*2)/60));


			// look for workload events-

			selectString = "select (DATEDIFF([day], CONVERT(datetime, '" +reportStartDateString+"', 105), WEDate)+1), WEHour, WEMinute, isnull(sum(WECount),0)from dbo.WorkloadEvents "
				+ "where WEDate >= convert(datetime, '" + reportStartDateString 
				+ "',103) and WEDate <= convert(datetime, '" + reportEndDateString
				+ "',103) group by WEDate, WEHour, WEMinute order by WEDate, WEHour, WEMinute";

			myDataReader = null;
			mySqlCommand = new SqlCommand(selectString, reportingSqlConnection);
			try
			{
				reportingSqlConnection.Open();
				myDataReader = mySqlCommand.ExecuteReader(CommandBehavior.CloseConnection);

				
				while (myDataReader.Read())
				{
					day = Convert.ToInt32(myDataReader.GetInt32(0).ToString());
					hour = Convert.ToInt32(myDataReader.GetSqlByte(1).ToString());
					minute = Convert.ToInt32(myDataReader.GetSqlByte(2).ToString());
					transactionCount = Convert.ToInt32(myDataReader.GetSqlInt32(3).ToString());
					//hourMinute = hour*60+minute;					
					DayHrMinCount[day,hour,minute] = DayHrMinCount[day,hour,minute] + transactionCount;
				} // end of while
				myDataReader.Close();
			}
			catch(Exception e)
			{
				EventLogger.WriteEntry ("Failure reading workload event table "+e.ToString(),EventLogEntryType.Error);
				statusCode = (int)StatusCode.ErrorReadingFilePath; // Failed to read events table
			}
			finally
			{
				// Always call Close when done reading.
				if (myDataReader != null)
					myDataReader.Close();
			}
			int surgeMinCount = 0;
			duration = 0;
			for (day = 1; day <= 7;day++)
			{
				for (hour = 0; hour <= 23;hour++)
				{
					for (minute = 0; minute <= 59;minute++)
					{
						transactionCount = DayHrMinCount[day,hour,minute];
						if (transactionCount > CBSurgeThreshold)
						{
							if (duration == 0)
							{
								startDay    = day;
								startHour   = hour;
								startMinute = minute;
							}
							duration = duration+1;
							endDay = day;
							endHour = hour;
							endMinute = minute; 
							surgeMinCount = surgeMinCount +1;
						}
						else
						{
							if (duration >=15)
							{
								// requires reporting
								reportDate = reportStartDate.AddDays(((double) startDay-1));
								strReportDate = reportDate.ToShortDateString();
			
								strStartHour = startHour.ToString("D2");
								strStartMin  = startMinute.ToString("D2");
								strEndHour   = endHour.ToString("D2");
								strEndMin    = endMinute.ToString("D2");

								durHour = duration/60;
								strDurHour = durHour.ToString("D2");
								durMin = duration - ((duration/60)*60);
								strDurMin = durMin.ToString("D2");
	
								this.file.WriteLine(strReportDate+"  "+strStartHour+ ":"+strStartMin+"   "+
									strEndHour+ ":"+strEndMin+"   "+
									strDurHour+ ":"+strDurMin);
								lineCount = lineCount+1;

							}
							duration = 0;
							startDay    = day;
							startHour   = hour;
							startMinute = minute;
						}
					}	
				}
			}
			// flush
			if (duration >=15)
			{
				// requires reporting

				reportDate = reportStartDate.AddDays(((double) startDay-1));
				strReportDate = reportDate.ToShortDateString();
			
				strStartHour = startHour.ToString("D2");
				strStartMin  = startMinute.ToString("D2");
				strEndHour   = endHour.ToString("D2");
				strEndMin    = endMinute.ToString("D2");

				durHour = duration/60;
				strDurHour = durHour.ToString("D2");
				durMin = duration - ((duration/60)*60);
				strDurMin = durMin.ToString("D2");
	
				this.file.WriteLine(strReportDate+"  "+strStartHour+ ":"+strStartMin+"   "+
					strEndHour+ ":"+strEndMin+"   "+
					strDurHour+ ":"+strDurMin);
				lineCount = lineCount+1;

			}
			
			if (lineCount == 0)
			{
				this.file.WriteLine("");
				this.file.WriteLine("");
				this.file.WriteLine("NO REPORTING PERIODS WERE IN SURGE THIS WEEK");
			}
			this.file.WriteLine("");
			this.file.WriteLine("");
			this.file.WriteLine("TREND:-  "+surgeMinCount+" minutes exceeded surge threshold this week.");
			this.file.WriteLine("         This represents "+Math.Floor((double)surgeMinCount/(double)(7*14.4))+" percent of the time.");

			this.file.WriteLine("         (Note this figure reports on individual minutes which");
			this.file.WriteLine("          may not be in a 15min SLA continuous surge period.)");
					
			this.file.Flush();
		

			//*************************************************************************************************************************
			//*************************************************************************************************************************
			//*************************************************************************************************************************

			// extract current capacity band CBMaxRequests
			// and CBSurgePercentage
	
			
			selectString = "select CBMAxRequests,CBSurgePercentage from CapacityBands where CBNumber = '" + CapacityBand + "'";
			reportingSqlConnection = new SqlConnection(reportingConnectionString);

			myDataReader = null;
			mySqlCommand = new SqlCommand(selectString, reportingSqlConnection);

			try
			{

				reportingSqlConnection.Open();
				myDataReader = mySqlCommand.ExecuteReader(CommandBehavior.CloseConnection);

				// Always call Read before accessing data.

				while (myDataReader.Read())
				{
					CBMaxRequests = Convert.ToInt32(myDataReader.GetSqlInt32(0).ToString());
				}
			}
			catch(Exception e)
			{
				EventLogger.WriteEntry ("Error Reading Capacity Band table"+e.Message,EventLogEntryType.Error);
				statusCode = (int)StatusCode.ErrorReadingTable; // Failed to read from property table
			}
			finally
			{
				// Always call Close when done reading.
				if (myDataReader != null)
					myDataReader.Close();

				// Close the connection when done with it.
				if (reportingSqlConnection.State == ConnectionState.Open)
					reportingSqlConnection.Close();
			}

			// work out threshold for 1 minute threshold figure for capacity band are per week
			CBSurgeThreshold = Convert.ToInt32(Math.Floor( ((CBMaxRequests/6)*0.2*2)/60));
			
			// clear the array
			for (day = 1; day <= 7;day++)
			{
				for (hour = 0; hour <= 23;hour++)
				{
					for (minute = 0; minute <= 59;minute++)
					{
						DayHrMinSurge[day,hour,minute]=0;
					}
				}
			}

			// look for workload events-
			selectString = "select (DATEDIFF([day], CONVERT(datetime, '" +reportStartDateString+"', 105),WEDate)+1), WEHour, WEMinute, isnull(sum(WECount),0)from dbo.WorkloadEvents "
				+ "where WEDate >= convert(datetime, '" + reportStartDateString 
				+ "',103) and WEDate <= convert(datetime, '" + reportEndDateString
				+ "',103) group by WEDate, WEHour, WEMinute order by WEDate, WEHour, WEMinute";

			myDataReader = null;
			mySqlCommand = new SqlCommand(selectString, reportingSqlConnection);
			try
			{
				reportingSqlConnection.Open();
				myDataReader = mySqlCommand.ExecuteReader(CommandBehavior.CloseConnection);

				
				while (myDataReader.Read())
				{
					day = Convert.ToInt32(myDataReader.GetInt32(0).ToString());
					hour = Convert.ToInt32(myDataReader.GetSqlByte(1).ToString());
					minute = Convert.ToInt32(myDataReader.GetSqlByte(2).ToString());
					transactionCount = Convert.ToInt32(myDataReader.GetSqlInt32(3).ToString());
					// hourMinute = hour*60+minute;					
					if (transactionCount > CBSurgeThreshold)
					{
						DayHrMinSurge[day,hour,minute] = transactionCount;
					}
				} // end of while
				myDataReader.Close();
			}
			catch(Exception e)
			{
				EventLogger.WriteEntry ("Failure reading workload event table "+e.ToString(),EventLogEntryType.Error);
				statusCode = (int)StatusCode.ErrorReadingFilePath; // Failed to read events table
			}
			finally
			{
				// Always call Close when done reading.
				if (myDataReader != null)
					myDataReader.Close();
			}
			
			// go through array and mark continuous 15min periods
			for (day = 1; day <= 7;day++)
			{
				for (hour = 0; hour <= 23;hour++)
				{
					for (minute = 0; minute <= 59;minute++)
					{
						transactionCount = DayHrMinSurge[day,hour,minute];
						if (transactionCount > 0)
						{
							duration = duration+1;
							endDay = day;
							endHour = hour;
							endMinute = minute; 
							DayHrMinSurge[day,hour,minute]=0;
							surgeMinCount = surgeMinCount +1;
						}
						else
						{
							if (duration >=15)
							{
								// requires marking as period of surge

								int newDay = day;
								int newHour = hour;
								int newMin = minute;

								// mark previous mins as surge
								for (int x=0;x<duration;x++)
								{
									if (newMin > 0)
									{
										newMin=newMin-1;
									}
									else
									{
										newMin = 59;
										if (newHour > 0)
										{
											newHour = newHour-1;
										}
										else
										{
											newHour = 23;
											if (newDay > 1)
											{
												newDay=newDay-1;
											}
										}
									}
									DayHrMinSurge[newDay,newHour,newMin]=1;
								}
							}
							duration = 0;
							startDay    = day;
							startHour   = hour;
							startMinute = minute;
						}
					}	
				}
			}
			

			//	===============

			
			
//*************************************************************************************************************************
			transactionCount = 0;
			int averageTime = 0;
			int totalVector = 0;
			int totalRaster = 0;

			this.file.WriteLine("");
			this.file.WriteLine("");
            this.file.WriteLine("-----------------------");
            this.file.WriteLine("Weekly Map Transactions");
			this.file.WriteLine("-----------------------");
			this.file.WriteLine("");
			this.file.Flush();

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
					productNum  = Convert.ToInt32(myDataReader.GetByte(0).ToString());
					productString  = myDataReader.GetString(1);
					commandNum  = Convert.ToInt32(myDataReader.GetByte(2).ToString());
					commandString  = myDataReader.GetString(3);
					transactionCount = Convert.ToInt32(myDataReader.GetSqlInt32(4).ToString());
					averageTime = Convert.ToInt32(myDataReader.GetSqlInt32(5).ToString());

					mapCommand[commandNum]=commandString;
					mapDisplay[productNum]=productString;
					MapCmdCount[productNum,commandNum]=transactionCount;
					MapCmdTime[productNum,commandNum] = averageTime;
					

				} // end of while

				tmpString = new StringBuilder();
				tmpString.Append("Raster Maps");
				this.file.WriteLine(tmpString);
				
				tmpString = new StringBuilder();
				tmpString.Append("Command,");
				tmpString.Append(mapCommand[1]);
				tmpString.Append(",");
				tmpString.Append(mapCommand[2]);
				tmpString.Append(",");
				tmpString.Append(mapCommand[3]);
				tmpString.Append(",");
				tmpString.Append(mapCommand[4]);
                tmpString.Append(",");
                tmpString.Append(mapCommand[5]);
                tmpString.Append(",Total");
				this.file.WriteLine(tmpString);

				tmpString = new StringBuilder();
				tmpString.Append("Product(Scale),Count");
				tmpString.Append(",Count");
                tmpString.Append(",Count");
                tmpString.Append(",Count");
                tmpString.Append(",Count,Transaction Count");
				this.file.WriteLine(tmpString);
				int i;
				for (i=1;i<=4;i++)
				{

					tmpString = new StringBuilder();
					switch(i)
					{
						case 1:
							tmpString.Append("OS Street View");
							break;
						case 2:
							tmpString.Append("1:50000 Scale");
							break;
						case 3:
							tmpString.Append("1:250000 Scale");
							break;
						case 4:
							tmpString.Append("MiniScale");
							break;
						case 5:
							tmpString.Append("Strategi");
							break;
					}
					tmpString.Append(",");
					tmpString.Append(MapCmdCount[i,1]);
					tmpString.Append(",");
					tmpString.Append(MapCmdCount[i,2]);
					tmpString.Append(",");
					tmpString.Append(MapCmdCount[i,3]);
					tmpString.Append(",");
					tmpString.Append(MapCmdCount[i,4]);
                    tmpString.Append(",");
                    tmpString.Append(MapCmdCount[i, 5]);
                    tmpString.Append(",");
                    tmpString.Append(MapCmdCount[i, 1] + MapCmdCount[i, 2] + MapCmdCount[i, 3] + MapCmdCount[i, 5]);
					this.file.WriteLine(tmpString);

                    totalRaster = totalRaster + MapCmdCount[i, 1] + MapCmdCount[i, 2] + MapCmdCount[i, 3] + MapCmdCount[i, 5];
				
				
				}

				this.file.WriteLine(" ");
				this.file.WriteLine(" ");

				tmpString = new StringBuilder();
				tmpString.Append("Vector Maps");
				this.file.WriteLine(tmpString);
				
				tmpString = new StringBuilder();
				tmpString.Append("Command,");
				tmpString.Append(mapCommand[1]);
				tmpString.Append(",");
				tmpString.Append(mapCommand[2]);
				tmpString.Append(",");
				tmpString.Append(mapCommand[3]);
				tmpString.Append(",");
				tmpString.Append(mapCommand[4]);
        		tmpString.Append(",");
        		tmpString.Append(mapCommand[5]);
                tmpString.Append(",Total");
				this.file.WriteLine(tmpString);

				tmpString = new StringBuilder();
				tmpString.Append("Product (Scale),Count");
				tmpString.Append(",Count,Count,Count,Count,Transaction Count");
				this.file.WriteLine(tmpString);
				i=5;
				tmpString = new StringBuilder();
				tmpString.Append("Strategi");
				tmpString.Append(",");
				tmpString.Append(MapCmdCount[i,1]);
				tmpString.Append(",");
				tmpString.Append(MapCmdCount[i,2]);
				tmpString.Append(",");
				tmpString.Append(MapCmdCount[i,3]);
				tmpString.Append(",");
				tmpString.Append(MapCmdCount[i,4]);
				tmpString.Append(",");
				tmpString.Append(MapCmdCount[i,5]);
				tmpString.Append(",");
				tmpString.Append(MapCmdCount[i,1] +MapCmdCount[i,2]+MapCmdCount[i,3] +MapCmdCount[i,5]);
				this.file.WriteLine(tmpString);

				totalVector = MapCmdCount[i,1] +MapCmdCount[i,2] +MapCmdCount[i,3] +MapCmdCount[i,5];
	
				
				
				this.file.Flush();
				myDataReader.Close();
			}
			catch(Exception e)
			{
				EventLogger.WriteEntry ("Failure reading page events table "+e.Message,EventLogEntryType.Error);
				statusCode = (int)StatusCode.ErrorReadingTable; // Failed to reading page events table
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
			this.file.WriteLine("Summary Transaction Totals for Week ");
			this.file.WriteLine(" ");
			this.file.WriteLine("Raster Maps ,"+totalRaster.ToString());
			this.file.WriteLine("Vector Maps ,"+totalVector.ToString());
			this.file.WriteLine(" ");
			this.file.WriteLine(" ");
			this.file.Flush();








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
            this.file.WriteLine(" ");
            this.file.WriteLine("Gradient Profile Lookups for Week ");
            this.file.WriteLine(" ,Chart View,Table View ,Total");
            this.file.WriteLine("WebServiceRequests ," + totalChartView.ToString() 
                + "," + totalTableView.ToString()+ ","+ totalGP.ToString());
            this.file.WriteLine(" ");
            this.file.WriteLine(" ");
            this.file.Flush();



// now find number of ITN routing transactions

						
//			selectString = 
//				"select SUM(RPECount),RPECongestedRoute "
//				+ "from dbo.RoadPlanEvents "
//				+ "where RPEDate >= convert(datetime, '" + reportStartDateString
//				+ "',103) and RPEDate <= convert(datetime, '" + reportEndDateString
//				+ "',103) group by RPECongestedRoute order by RPECongestedRoute";
// Only 
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
					totalTrans = totalTrans+transactionCount;
					congested  = myDataReader.GetBoolean(1);
					if (congested == true)
					{
						tmpString = new StringBuilder();
						tmpString.Append("Adjusted Route,");
						tmpString.Append(transactionCount.ToString());
						this.file.WriteLine(tmpString);	
					} 
//					else
//					{
//						tmpString = new StringBuilder();
//						tmpString.Append("Normal Route,");
//						tmpString.Append(transactionCount.ToString());
//						this.file.WriteLine(tmpString);	
//					}

				} // end of while
				
//				tmpString = new StringBuilder();
//				tmpString.Append("Total,");
//				tmpString.Append(totalTrans.ToString());
//				this.file.WriteLine(tmpString);	

					
				
				this.file.Flush();
				myDataReader.Close();
			}
			catch(Exception e)
			{
				EventLogger.WriteEntry ("Failure reading RoadPlanEvents table "+e.Message,EventLogEntryType.Error);
				statusCode = (int)StatusCode.ErrorReadingTable; // Failed to reading page events table
			}
			finally
			{
				// Always call Close when done reading.
				if (myDataReader != null)
					myDataReader.Close();
			}







            //*************************************************************************************************************************
            transactionCount = 0;
            averageTime = 0;
            totalVector = 0;
            totalRaster = 0;

            this.file.WriteLine("");
            this.file.WriteLine("");
            this.file.WriteLine("-----------------------");
            this.file.WriteLine("Weekly OS Map Sessions ");
            this.file.WriteLine("-----------------------");
            this.file.WriteLine("");
            this.file.Flush();

            reportingSqlConnection = new SqlConnection(reportingConnectionString);

            selectString =


            "select ME.MEMDTID,MDT.MDTDescription,isnull(sum(ME.MECount),0) "
            + "from dbo.MapEventsOS ME , dbo.MapCommandType MCT , dbo.MapDisplayType MDT "
            + "where ME.MEMCTID = MCT.MCTID and ME.MEMDTID = MDT.MDTID "
            + "  and ME.MEDate >= convert(datetime, '" + reportStartDateString
            + "',103) and ME.MEMCTID = 1 and ME.MEDate <= convert(datetime, '" + reportEndDateString
            + "',103) group by ME.MEMDTID,MDT.MDTDescription order by ME.MEMDTID,MDT.MDTDescription";


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
                tmpString.Append("Product(Scale),Session Count");
                this.file.WriteLine(tmpString);
                int i;
                for (i = 1; i <= 4; i++)
                {

                    tmpString = new StringBuilder();
                    switch (i)
                    {
                        case 1:
                            tmpString.Append("OS Street View");
                            break;
                        case 2:
                            tmpString.Append("1:50000 Scale");
                            break;
                        case 3:
                            tmpString.Append("1:250000 Scale");
                            break;
                        case 4:
                            tmpString.Append("MiniScale");
                            break;
                        case 5:
                            tmpString.Append("Strategi");
                            break;
                    }
                    tmpString.Append(",");
                    tmpString.Append(MapCmdCount[i, 1]);
                    this.file.WriteLine(tmpString);

                    totalRaster = totalRaster + MapCmdCount[i, 1];


                }

                this.file.WriteLine(" ");
                this.file.WriteLine(" ");

                tmpString = new StringBuilder();
                tmpString.Append("Vector Maps");
                this.file.WriteLine(tmpString);
                tmpString = new StringBuilder();
                tmpString.Append("Product (Scale),Session Count");
                this.file.WriteLine(tmpString);
                i = 5;
                tmpString = new StringBuilder();
                tmpString.Append("Strategi");
                tmpString.Append(",");
                tmpString.Append(MapCmdCount[i, 1]);
                this.file.WriteLine(tmpString);

                totalVector = MapCmdCount[i, 1];


                this.file.Flush();
                myDataReader.Close();
            }
            catch (Exception e)
            {
                EventLogger.WriteEntry("Failure reading MapEventOS table " + e.Message, EventLogEntryType.Error);
                statusCode = (int)StatusCode.ErrorReadingTable; // Failed to reading page events table
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
            this.file.WriteLine("Summary OS Map Sessions for Week ");
            this.file.WriteLine(" ");
            this.file.WriteLine("Raster Maps ," + totalRaster.ToString());
            this.file.WriteLine("Vector Maps ," + totalVector.ToString());
            this.file.WriteLine(" ");
            this.file.WriteLine(" ");
            this.file.Flush();


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
                + "',103) and GPEGPDTID = 2 group by GPEGPDTID order by GPEGPDTID";

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
                    totalChartView = transactionCount;
 
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
            this.file.WriteLine("Gradient Profile Sessions for Week, "+ totalChartView.ToString());
            this.file.WriteLine(" ");
            this.file.Flush();

            // now find number of ITN routing transactions


          selectString =
                "select SUM(RPECount),RPECongestedRoute "
                + "from dbo.RoadPlanEventsOS"
                + " where RPEDate >= convert(datetime, '" + reportStartDateString
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
                EventLogger.WriteEntry("Failure reading RoadPlanEvents OS table " + e.Message, EventLogEntryType.Error);
                statusCode = (int)StatusCode.ErrorReadingTable; // Failed to reading page events table
            }
            finally
            {
                // Always call Close when done reading.
                if (myDataReader != null)
                    myDataReader.Close();
            }




            int xtot = totalRaster + totalVector + totalTrans;
            this.file.WriteLine(" ");
            this.file.WriteLine("TOTAL O/S Transactions : " + xtot.ToString());

			


 /*---------------------------------------------------------------------------------------------------------*/
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
				+ "',103) group by GT.GTID,GT.GTDescription order by GT.GTID,GT.GTDescription";

			myDataReader = null;
			mySqlCommand = new SqlCommand(selectString, reportingSqlConnection);
			transactionCount = 0;

			try
			{
				reportingSqlConnection.Open();
				myDataReader = mySqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
				while (myDataReader.Read())
				{
					productNum  = Convert.ToInt32(myDataReader.GetByte(0).ToString());
					productString  = myDataReader.GetString(1);
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
			catch(Exception e)
			{
				EventLogger.WriteEntry ("Failure reading gazetteer events table "+e.Message,EventLogEntryType.Error);
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
            this.file.WriteLine("Accessibility Transactions");

            tmpString = new StringBuilder();
            tmpString.Append("Type,Description, Count");
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
            tmpString.Append("Type,Description, Count");
            this.file.WriteLine(tmpString);


            selectString =
                "select GQET.GQETID,GQET.GQETDescription, isnull(sum(GQECount),0) "
                + "from dbo.GISQueryEvents GQE, dbo.GISQueryEventType GQET "
                + "where GQET.GQETID = GQE.GQEGQETID "
                +" and  GQET.GQETCode in ('IsPointInAccessibleLocation','FindNearestAccessibleStops','FindNearestAccessibleLocalities')"
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
				SDate  = new DateTime(1,1,1,0,0,0);
				EDate  = new DateTime(1,1,1,0,0,0);
				excludeString   = new StringBuilder();
				for(int i = 1;i<=testCount;i++)
				{
					SDate =  TestStarted[i].Date;
					SHour = TestStarted[i].Hour;
					SMin  = TestStarted[i].Minute;
					EDate =  TestCompleted[i].Date;
					EHour = TestCompleted[i].Hour;
					EMin  = TestCompleted[i].Minute;
					

					ScompareString = (long)SDate.Year *1000000L + ((long)SDate.Month *10000L) + ((long)SDate.Day*100L) + (long)SHour; //+(long)SHourQuarter;
					EcompareString = (long)EDate.Year *1000000L + ((long)EDate.Month *10000L) + ((long)EDate.Day*100L) + (long)EHour ;//+(long)EHourQuarter;

					// build exclusion string - nothing allowed within this 			
					excludeString.Append(" and not (");
					excludeString.Append("(year(ME.MEDate)*1000000+month(ME.MEDate)*10000+day(ME.MEDate)*100+ME.MEHour) >= "+ScompareString);
					excludeString.Append(" and (year(ME.MEDate)*1000000+month(ME.MEDate)*10000+day(ME.MEDate)*100+ME.MEHour) <= "+EcompareString);
					excludeString.Append(")");
				}


				transactionCount = 0;
				averageTime = 0;
				totalVector = 0;
				totalRaster = 0;

				this.file.WriteLine("");
				this.file.WriteLine("");
				this.file.WriteLine("Weekly Map Transactions (Adjusted for test periods)");
				this.file.WriteLine("---------------------------------------------------");
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
						productNum  = Convert.ToInt32(myDataReader.GetByte(0).ToString());
						productString  = myDataReader.GetString(1);
						commandNum  = Convert.ToInt32(myDataReader.GetByte(2).ToString());
						commandString  = myDataReader.GetString(3);
						transactionCount = Convert.ToInt32(myDataReader.GetSqlInt32(4).ToString());
						averageTime = Convert.ToInt32(myDataReader.GetSqlInt32(5).ToString());

						mapCommand[commandNum]=commandString;
						mapDisplay[productNum]=productString;
						MapCmdCount[productNum,commandNum]=transactionCount;
						MapCmdTime[productNum,commandNum] = averageTime;
					

					} // end of while

					tmpString = new StringBuilder();
					tmpString.Append("Raster Maps");
					this.file.WriteLine(tmpString);
				
					tmpString = new StringBuilder();
					tmpString.Append("Command,");
					tmpString.Append(mapCommand[1]);
					tmpString.Append(",");
					tmpString.Append(mapCommand[2]);
					tmpString.Append(",");
					tmpString.Append(mapCommand[3]);
					tmpString.Append(",");
					tmpString.Append(mapCommand[4]);
					tmpString.Append(",");
					tmpString.Append(mapCommand[5]);
					tmpString.Append(",Total");
					this.file.WriteLine(tmpString);

					tmpString = new StringBuilder();
					tmpString.Append("Product(Scale),Count");
					tmpString.Append(",Count");
					tmpString.Append(",Count");
					tmpString.Append(",Count");
					tmpString.Append(",Count,Transaction Count");
					this.file.WriteLine(tmpString);
					int i;
					for (i=1;i<=4;i++)
					{

						tmpString = new StringBuilder();
						switch(i)
						{
							case 1:
								tmpString.Append("OS Street View");
								break;
							case 2:
								tmpString.Append("1:50000 Scale");
								break;
							case 3:
								tmpString.Append("1:250000 Scale");
								break;
							case 4:
								tmpString.Append("MiniScale");
								break;
							case 5:
								tmpString.Append("Strategi");
								break;
						}
						tmpString.Append(",");
						tmpString.Append(MapCmdCount[i,1]);
						tmpString.Append(",");
						tmpString.Append(MapCmdCount[i,2]);
						tmpString.Append(",");
						tmpString.Append(MapCmdCount[i,3]);
						tmpString.Append(",");
						tmpString.Append(MapCmdCount[i,4]);
						tmpString.Append(",");
						tmpString.Append(MapCmdCount[i,5]);
						tmpString.Append(",");
						tmpString.Append(MapCmdCount[i,1] +MapCmdCount[i,2]  +MapCmdCount[i,3] +MapCmdCount[i,5]);
						this.file.WriteLine(tmpString);

						totalRaster = totalRaster + MapCmdCount[i,1] +MapCmdCount[i,2] +MapCmdCount[i,3] +MapCmdCount[i,5];
				
				
					}

					this.file.WriteLine(" ");
					this.file.WriteLine(" ");

					tmpString = new StringBuilder();
					tmpString.Append("Vector Maps");
					this.file.WriteLine(tmpString);
				
					tmpString = new StringBuilder();
					tmpString.Append("Command,");
					tmpString.Append(mapCommand[1]);
					tmpString.Append(",");
					tmpString.Append(mapCommand[2]);
					tmpString.Append(",");
					tmpString.Append(mapCommand[3]);
					tmpString.Append(",");
					tmpString.Append(mapCommand[4]);
					tmpString.Append(",");
					tmpString.Append(mapCommand[5]);
					tmpString.Append(",Total");
					this.file.WriteLine(tmpString);

					tmpString = new StringBuilder();
					tmpString.Append("Product (Scale),Count");
					tmpString.Append(",Count,Count,Count,Count,Transaction Count");
					this.file.WriteLine(tmpString);
					i=5;
					tmpString = new StringBuilder();
					tmpString.Append("Strategi");
					tmpString.Append(",");
					tmpString.Append(MapCmdCount[i,1]);
					tmpString.Append(",");
					tmpString.Append(MapCmdCount[i,2]);
					tmpString.Append(",");
					tmpString.Append(MapCmdCount[i,3]);
					tmpString.Append(",");
					tmpString.Append(MapCmdCount[i,4]);
					tmpString.Append(",");
					tmpString.Append(MapCmdCount[i,5]);
					tmpString.Append(",");
					tmpString.Append(MapCmdCount[i,1] +MapCmdCount[i,2] +MapCmdCount[i,3] +MapCmdCount[i,5]);
					this.file.WriteLine(tmpString);

					totalVector = MapCmdCount[i,1] +MapCmdCount[i,2] +MapCmdCount[i,3] +MapCmdCount[i,5];
	
				
				
					this.file.Flush();
					myDataReader.Close();
				}
				catch(Exception e)
				{
					EventLogger.WriteEntry ("Failure reading page events table "+e.Message,EventLogEntryType.Error);
					statusCode = (int)StatusCode.ErrorReadingTable; // Failed to reading page events table
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
				this.file.WriteLine("Summary Transaction Totals for Week ");
				this.file.WriteLine(" ");
				this.file.WriteLine("Raster Maps ,"+totalRaster.ToString());
				this.file.WriteLine("Vector Maps ,"+totalVector.ToString());
				this.file.WriteLine(" ");
				this.file.WriteLine(" ");
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

				SDate  = new DateTime(1,1,1,0,0,0);
				EDate  = new DateTime(1,1,1,0,0,0);
				excludeString   = new StringBuilder();
				for(int i = 1;i<=testCount;i++)
				{
					SDate =  TestStarted[i].Date;
					SHour = TestStarted[i].Hour;
					SMin  = TestStarted[i].Minute;
					
					EDate =  TestCompleted[i].Date;
					EHour = TestCompleted[i].Hour;
					EMin  = TestCompleted[i].Minute;
					
					SMin=SMin/15;
					EMin=EMin/15;
					ScompareString = ((long)SDate.Year-2000) *100000000L + ((long)SDate.Month *1000000L) + ((long)SDate.Day*10000L) + (long)SHour*100L+(long)SMin;
					EcompareString = ((long)EDate.Year-2000) *100000000L + ((long)EDate.Month *1000000L) + ((long)EDate.Day*10000L) + (long)EHour*100L+(long)EMin;

					// build exclusion string - nothing allowed within this 			
					excludeString.Append(" and not (");
					excludeString.Append(     "((year(RPEDate)-2000)*100000000+month(RPEDate)*1000000+day(RPEDate)*10000+RPEHour*100+RPEHourQuarter) >= "+ScompareString);
					excludeString.Append(" and ((year(RPEDate)-2000)*100000000+month(RPEDate)*1000000+day(RPEDate)*10000+RPEHour*100+RPEHourQuarter) <= "+EcompareString);
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
                    }
				
				
					this.file.Flush();
					myDataReader.Close();
				}
				catch(Exception e)
				{
					EventLogger.WriteEntry ("Failure reading RoadPlanEvents table "+e.Message,EventLogEntryType.Error);
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
				SDate  = new DateTime(1,1,1,0,0,0);
				EDate  = new DateTime(1,1,1,0,0,0);
				excludeString   = new StringBuilder();
				for(int i = 1;i<=testCount;i++)
				{
					SDate =  TestStarted[i].Date;
					SHour = TestStarted[i].Hour;
					SMin  = TestStarted[i].Minute;
					
					EDate =  TestCompleted[i].Date;
					EHour = TestCompleted[i].Hour;
					EMin  = TestCompleted[i].Minute;
					SMin=SMin/15;
					EMin=EMin/15;
					
					ScompareString = ((long)SDate.Year-2000) *100000000L + ((long)SDate.Month *1000000L) + ((long)SDate.Day*10000L) + (long)SHour*100L+SMin; //+(long)SHourQuarter;
					EcompareString = ((long)EDate.Year-2000) *100000000L + ((long)EDate.Month *1000000L) + ((long)EDate.Day*10000L) + (long)EHour*100L+EMin ;//+(long)EHourQuarter;

					// build exclusion string - nothing allowed within this 			
					excludeString.Append(" and not (");
					excludeString.Append(     "((year(GE.GEDate)-2000)*100000000+month(GE.GEDate)*1000000+day(GE.GEDate)*10000+GE.GEHour*100+GE.GEHourQuarter) >= "+ScompareString);
					excludeString.Append(" and ((year(GE.GEDate)-2000)*100000000+month(GE.GEDate)*1000000+day(GE.GEDate)*10000+GE.GEHour*100+GE.GEHourQuarter) <= "+EcompareString);
					excludeString.Append(")");
				}
			
					
				selectString = 
					"select GT.GTID,GT.GTDescription, isnull(sum(GE.GECount),0) "
					+ "from dbo.GazetteerEvents GE, dbo.GazetteerType GT "
					+ "where GT.GTID = GE.GEGTID "
					+ "  and GE.GEDate >= convert(datetime, '" + reportStartDateString
					+ "',103) and GE.GEDate <= convert(datetime, '" + reportEndDateString
					+ "',103) "
					+excludeString
					+ " group by GT.GTID,GT.GTDescription order by GT.GTID,GT.GTDescription";

				myDataReader = null;
				mySqlCommand = new SqlCommand(selectString, reportingSqlConnection);
				transactionCount = 0;

				try
				{
					reportingSqlConnection.Open();
					myDataReader = mySqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
					while (myDataReader.Read())
					{
						productNum  = Convert.ToInt32(myDataReader.GetByte(0).ToString());
						productString  = myDataReader.GetString(1);
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
				catch(Exception e)
				{
					EventLogger.WriteEntry ("Failure reading gazetteer events table "+e.Message,EventLogEntryType.Error);
					statusCode = (int)StatusCode.ErrorReadingTable; // Failed to reading page events table
				}
				finally
				{
					// Always call Close when done reading.
					if (myDataReader != null)
						myDataReader.Close();
				}

				xtot = totalRaster +totalVector + totalTrans ;
				this.file.WriteLine(" ");
				this.file.WriteLine("TOTAL O/S Transactions : "+xtot.ToString());

				
				this.file.Flush();

                /*---------------------------------------------------------------------------------------------------------*/
                this.file.WriteLine(" ");
                this.file.WriteLine("Accessibility Transactions");

                tmpString = new StringBuilder();
                tmpString.Append("Type,Description, Count");
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
                    excludeString.Append(" and ((year(AE.AEDate)-2000)*100000000+month(AE.AEDate)*10000+AE.AEHour*100+AE.AEHourQuarter) <= " + EcompareString);
                    excludeString.Append(")");
                }



                selectString =
                    "select AT.AETID,AT.AETDescription, isnull(sum(AE.AECount),0) "
                    + "from dbo.AccessibleEvents AE, dbo.AccessibleEventType AT "
                    + "where AT.AETID = AE.AETID "
                    + "  and AE.AEDate >= convert(datetime, '" + reportStartDateString
                    + "',103) and AE.AEDate <= convert(datetime, '" + reportEndDateString
                    + "',103) group by AT.AETID,AT.AETDescription order by AT.AETID,AT.AETDescription "
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

                this.file.WriteLine(tmpString);
                tmpString = new StringBuilder();
                tmpString.Append("Type,Description, Count");
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
                    excludeString.Append("((year(GQE.GQEDate)-2000)*100000000+month(GQE.GQEDate)*1000000+day(GQE.GQEDate)*10000+AE.AEHour*100+GQE.GQEHourQuarter) >= " + ScompareString);
                    excludeString.Append(" and ((year(GQE.GQEDate)-2000)*100000000+month(GQE.GQEDate)*10000+GQE.GQEHour*100+GQE.GQEHourQuarter) <= " + EcompareString);
                    excludeString.Append(")");
                }

                    selectString =
                    "select GQET.GQETID,GQET.GQETDescription, isnull(sum(GQECount),0) "
                    + "from dbo.GISQueryEvents GQE, dbo.GISQueryEventType GQET "
                    + "where GQET.GQETID = GQE.GQEGQETID "
                    + " and  GQET.GQETCode in ('IsPointInAccessibleLocation','FindNearestAccessibleStops','FindNearestAccessibleLocalities')"
                    + "  and GQE.GQEDate >= convert(datetime, '" + reportStartDateString
                    + "',103) and GQE.GQEDate <= convert(datetime, '" + reportEndDateString
                    + "',103) group by GQET.GQETID,GQET.GQETDescription order by GQET.GQETID,GQET.GQETDescription"
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

//*************************************************************************************************************************
			
			this.file.Close();


            try
            {
                if (!filePathString.Equals(filePathStringDefault))
                {
                    File.Copy(filePathString, filePathStringDefault, true);
                }
            }
            catch (Exception e)
            {
                EventLogger.WriteEntry("Failure copying file " + e.Message, EventLogEntryType.Error);
                statusCode = (int)StatusCode.ErrorCopyingFile; // Failed to reading page events table
            }


			string from = "";
			string smtpServer = "";
			string to = "";
			string subject = "";


			readOK =  controller.ReadProperty(reportNumber.ToString(), "FilePath", out filePathString);
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
			readOK =  controller.ReadProperty("0", "smtpServer", out smtpServer);
			if (readOK == false || smtpServer.Length == 0)
			{
				statusCode = (int)StatusCode.ErrorReadingSmtpServer; // Error Reading smtpServer
				return statusCode;
			}

			readOK = controller.ReadProperty(reportNumber.ToString(), "MailRecipient", out to);
			if (readOK == false || to.Length == 0)
			{
				statusCode = (int)StatusCode.ErrorReadingRecipientEmailAddress; // Error Reading recipient email address
				return statusCode;
			}
			readOK =  controller.ReadProperty(reportNumber.ToString(), "Title", out subject);
			if (readOK == false || subject.Length == 0)
			{
                subject = "DFT Weekly Report %YY-MM-DD%";
			}
            subject = subject.Replace("%YY-MM-DD%", RequestedDateString);
 
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
			WeeklyTotal = 0;
			tmpString = new StringBuilder();
			tmpString.Append(transactionString);
			myDataReader = null;
			mySqlCommand = new SqlCommand(selectString, reportingSqlConnection);
			int transactionCount = 0;
			for(int i = 1;i <= 7;i++) dayTot[i] = 0;
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
				for(int i = 1;i <= 7;i++)
				{
					tmpString.Append(",");
					tmpString.Append(dayTot[i].ToString());
				}

				this.file.WriteLine(tmpString.ToString());
					
				this.file.Flush();
				myDataReader.Close();

			}
			catch(Exception e)
			{
				EventLogger.WriteEntry ("Error reading "+transactionString+ " "+e.Message,EventLogEntryType.Error);

				statusCode = (int)StatusCode.ErrorReadingTable; // Failed reading table
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
