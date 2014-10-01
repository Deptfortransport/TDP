///<header>
///Mobile WEEKLY REPORT (REPORT 20)
///Created 01/12/2003
///Author JP Scott
///
///Version	Date		Who	Reason
///1		 06/09/2005	PS	Created
///2 (1.0.25)21/11/2005  PS  Correct date range for workload
///3		02/11/2005	PS	V1.0.26 add user sessions count
///
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
	
	public class MobileWeeklyReport
	{
		
		private DateTime currentDateTime;
		private string reportStartDateString;
		private string reportEndDateString;
		private DateTime reportStartDate;
		private DateTime reportEndDate;
		private DateTime reportEndDatePlus1;
		private string reportEndDatePlus1String;
		

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
		private int statusCode;
		private DateTime thisDay;
		private string dayOfWeek;
		private int[] dayTot = new int[8];
		private int[] dayGrandTot = new int[8];
		private int WeeklyTotal;
		private int WeeklyWebPageTotal;
		private int WeeklyExpPageTotal;
		private int WeeklyRTTIPageTotal;
		private int WeeklySessionTotal;
		private int WeeklyWorkLoadTotal;
//		private int WeeklyWorkLoadTotalAdjusted;


		private int[] monthlyDayTot = new int[32];
		private int[,,] DayHrMinCount = new int[8,24,60];  //1-31 - 0-23,0-59

		private int day;

		private int[,] MapCmdCount = new int[6,5];
		private int[,] MapCmdTime  = new int[6,5];
		private string[] mapCommand = new string[5];
		private string[] mapDisplay = new string[6];
		private DateTime[] TestStarted = new DateTime[250]; 
		private DateTime[] TestCompleted = new DateTime[250]; 
//		private int testCount;
//		private DateTime SDate;
//		private DateTime EDate;
//		private int SHour,EHour,SMin,EMin ;
//		private long ScompareString,EcompareString;
//		private StringBuilder excludeString;


		private int[] RefType         = new int[20];
		private string[] RefTypeDesc  = new string[20];
		private int[] ResponseTarget  = new int[20];
		
		public MobileWeeklyReport()
		{
			EventLogger = new EventLog("Application");
			EventLogger.Source = "TD.Reporting";
			controller    = new DailyReportController();
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


			// open report text file
			file = new StreamWriter( new FileStream(filePathString, System.IO.FileMode.Create ) );
			this.file.WriteLine("------------------------------------------");
			this.file.WriteLine("TRANSPORT DIRECT");
			this.file.WriteLine("Mobile Weekly Report");
			this.file.WriteLine("------------------------------------------");
			this.file.WriteLine("");
			this.file.WriteLine("For week starting : " + reportStartDate.ToShortDateString());
			this.file.WriteLine("");
			this.file.WriteLine(",Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday");
			this.file.Flush();

			reportingSqlConnection = new SqlConnection(reportingConnectionString);
/*
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

 */
//*******************************
			int transactionCount = 0;

		//.............................			
			transactionString = "Page Entry Events";
			this.file.WriteLine("Page Entry Events");
			selectString = 
				"select PT.PETDescription, PE.PEEDate, isnull(sum(PE.PEECount),0)"
				+ "from dbo.PageEntryEvents PE, dbo.PageEntryType PT where PT.PETID = PE.PEEPETID"
				+ "  and PE.PEEDate >= convert(datetime, '" + reportStartDateString
				+ "',103) and PE.PEEDate < convert(datetime, '" + reportEndDatePlus1String
				+ "',103) and PT.PETCode like 'Mobile%' " 
				+ " group by PT.PETDescription,PE.PEEDate order by PT.PETDescription,PE.PEEDate";

			myDataReader = null;
			mySqlCommand = new SqlCommand(selectString, reportingSqlConnection);
			string webPage;
			string lastWebPage;

			lastWebPage = "";
			webPage="";
			WeeklyWebPageTotal = 0;
			
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
// />>>


			//.............................		
			for(int i = 1;i <= 7;i++)
			{
				dayGrandTot[i] = 0;
				dayTot[i] = 0;
			}
			this.file.WriteLine(" ");
			this.file.WriteLine(" ");
			transactionCount = 0;
			transactionString = "Exposed Services Events";
			this.file.WriteLine("Exposed Services Events");
			selectString = 
				"select EC.EXSCDescription, EE.EXSEDate, isnull(sum(EE.EXSECount),0)"
				+ "from dbo.ExposedServicesEvents EE, dbo.ExposedServicesCategory EC where EE.EXSCID = EC.EXSCID"
				+ "  and EE.EXSEDate >= convert(datetime, '" + reportStartDateString
				+ "',103) and EE.EXSEDate < convert(datetime, '" + reportEndDatePlus1String
				+ "',103) " 
				+ " group by EC.EXSCDescription,EE.EXSEDate order by EC.EXSCDescription,EE.EXSEDate";

			myDataReader = null;
			mySqlCommand = new SqlCommand(selectString, reportingSqlConnection);
			string ExpPage;
			string lastExpPage;

			lastExpPage = "";
			ExpPage="";
			WeeklyExpPageTotal = 0;
			
			try
			{
				reportingSqlConnection.Open();
				myDataReader = mySqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
				while (myDataReader.Read())
				{
					ExpPage  = myDataReader.GetString(0);
					if (ExpPage != lastExpPage)
					{
						if (lastExpPage.Length != 0)
						{
							// print the line on change of exp page
							tmpString = new StringBuilder();
							tmpString.Append(lastExpPage);
							for(int i = 1;i <= 7;i++)
							{
								tmpString.Append(",");
								tmpString.Append(dayTot[i].ToString());
							}
							this.file.WriteLine(tmpString.ToString());
							for(int i = 1;i <= 7;i++) dayTot[i] = 0;
						}
						lastExpPage = ExpPage;
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
				// flush the last exp page
				tmpString = new StringBuilder();
				tmpString.Append(ExpPage);
				for(int i = 1;i <= 7;i++)
				{
					tmpString.Append(",");
					tmpString.Append(dayTot[i].ToString());
				}
				this.file.WriteLine(tmpString.ToString());

				this.file.WriteLine(" ");

				
				// write grand tots
				tmpString = new StringBuilder();
				tmpString.Append("Total Daily Exposed Services Calls ");
				for(int i = 1;i <= 7;i++)
				{
					tmpString.Append(",");
					tmpString.Append(dayGrandTot[i].ToString());
					WeeklyExpPageTotal = WeeklyExpPageTotal + dayGrandTot[i];
				}
				this.file.WriteLine(tmpString.ToString());
				this.file.WriteLine(" ");
				

				this.file.Flush();
				myDataReader.Close();

			}
			catch(Exception e)
			{
				EventLogger.WriteEntry ("Failure reading Exposed services events table "+e.Message,EventLogEntryType.Error);
				statusCode = (int)StatusCode.ErrorReadingTable; // Failed to reading page events table
			}
			finally
			{
				// Always call Close when done reading.
				if (myDataReader != null)
					myDataReader.Close();
			}
			this.file.WriteLine(" ");
			
			
// /<<<<			
/*
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
//**********************************
			for(int i = 1;i <= 7;i++)
			{
				dayGrandTot[i] = 0;
				dayTot[i] = 0;
			}
			this.file.WriteLine(" ");
			transactionString = "RTTI Events ";
			selectString = "select datepart(weekday,RStarttime), isnull(Count(*),0)from dbo.RTTIEvents "
				+ "where RStarttime >= convert(datetime, '" + reportStartDateString 
				+ "',103) and RStarttime <= convert(datetime, '" + reportEndDatePlus1String
				+ "',103) group by datepart(weekday,RStarttime) order by datepart(weekday,RStarttime)";

			
			WeeklyTotal = 0;
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
					day = Convert.ToInt32(myDataReader.GetSqlInt32(0).ToString());
					day = day-1;
					if (day == 0)
					{
						day = 7;
					}
					transactionCount = Convert.ToInt32(myDataReader.GetSqlInt32(1).ToString());
					WeeklyTotal = WeeklyTotal + transactionCount;
					dayTot[day] = dayTot[day] + transactionCount;
					
				
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
				WeeklyRTTIPageTotal = WeeklyTotal;
				this.file.Flush();
				myDataReader.Close();
			}			

			catch(Exception e)
			{
				EventLogger.WriteEntry ("Failure reading RTTI events table "+e.Message,EventLogEntryType.Error);
				statusCode = (int)StatusCode.ErrorReadingTable; // Failed to reading page events table
			}
			finally
			{
				// Always call Close when done reading.
				if (myDataReader != null)
					myDataReader.Close();
			}
			
			
			//.............................			
/*			// build exclusion string
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

				this.file.WriteLine(" ");
				transactionString = "RTTI Events (Excluding Tests)";
				selectString = "select RStarttime, isnull(Count(*),0)from dbo.RTTIEvents "
					+ "where RStarttime >= convert(datetime, '" + reportStartDateString 
					+ "',103) and RStarttime <= convert(datetime, '" + reportEndDateString
					+ "',103) "
					+ excludeString
					+ " group by RStarttime order by RStarttime";

			
				WeeklyTotal = 0;
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
						day = Convert.ToInt32(myDataReader.GetSqlInt32(0).ToString());
						transactionCount = Convert.ToInt32(myDataReader.GetSqlInt32(1).ToString());
					

						WeeklyTotal = WeeklyTotal + transactionCount;
						dayTot[day] = dayTot[day] + transactionCount;
					}

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
				EventLogger.WriteEntry ("Failure reading page events table "+e.Message,EventLogEntryType.Error);
				statusCode = (int)StatusCode.ErrorReadingTable; // Failed to reading page events table
			}
			finally
			{
				// Always call Close when done reading.
				if (myDataReader != null)
					myDataReader.Close();
			}
			



*/


				this.file.WriteLine(" ");
			

			
			//...............................................

			transactionString = "Workload  (Total Load)";
			selectString = "select [date],count(*)from dbo.DFTIISLogdata "
				+ "where [date] >= convert(datetime, '" + reportStartDateString 
				+ "',103) and [date] < convert(datetime, '" + reportEndDatePlus1String
				+ "',103) "
				+ " and cs_uri_stem like '/Mobile%' "
				+ " group by [date] order by [date]";

			DailyEventBreakDown();
			WeeklyWorkLoadTotal = WeeklyTotal;

			

			transactionString = "Mobile User Sessions";

			selectString = "select SEDate, isnull(sum(SECount),0)from dbo.SessionEvents "
				+ "where SEDate >= convert(datetime, '" + reportStartDateString 
				+ "',103) and SEDate <= convert(datetime, '" + reportEndDateString
				+ "',103) and SEPartnerID = 99 "
				+ "group by SEDate order by SEDate";
			DailyEventBreakDown();
			WeeklySessionTotal = WeeklyTotal;

			

/*			// build exclusion string
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

*/
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
		

			transactionString = "User Sessions ";
			selectString = "select SEDate, isnull(sum(SECount),0)from dbo.SessionEvents "
				+ "where SEDate >= convert(datetime, '" + reportStartDateString 
				+ "',103) and SEDate <= convert(datetime, '" + reportEndDateString
				+ "',103) group by SEDate order by SEDate";
			DailyEventBreakDown();
			WeeklySessionTotal = WeeklyTotal;


*/
			//.............................			
			this.file.WriteLine(" ");
			this.file.WriteLine(" ");
			this.file.WriteLine("Summary Totals for Week ");
			this.file.WriteLine(" ");
			this.file.WriteLine("Total Mobile Web Pages visited : "+WeeklyWebPageTotal.ToString());
			this.file.WriteLine("Total Exposed Services Called  : "+WeeklyExpPageTotal.ToString());
			this.file.WriteLine("Total RTTI Events              : "+WeeklyRTTIPageTotal.ToString());
			this.file.WriteLine("Total Mobile User Sessions     : "+WeeklySessionTotal.ToString());
			this.file.WriteLine("Total Mobile WorkLoad Hits     : "+WeeklyWorkLoadTotal.ToString());
			this.file.WriteLine(" ");

			//.............................			
			//*************************************************************************************************************************

		
		

//*************************************************************************************************************************
			
			this.file.Close();

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
				subject = "DFT Weekly Report";
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
