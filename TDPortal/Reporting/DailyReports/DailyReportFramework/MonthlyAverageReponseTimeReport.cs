///<header>
///Monthly Average Response Time Report
///Created 14/12/2003
///Author R Broddle
///
///Version	Date		Who	Reason
///1		14/12/2004	RB	Created
///2		23/02/2004	PS	Adjust method of finding surge
///3		24/03/2004	PS	Retitle
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
	/// Class to produce Monthly SLA Response Time report for capacity planning
 	/// analyses all recorded event tables and reports total count per day
 	/// Path: c:\MonthlySLAResponseReport.csv
	/// </summary>
	/// 
	
	public class MonthlyAverageReponseTimeReport
	{
		private DateTime currentDateTime;
		private string reportStartDateString;
		private string reportEndDateString;
		private DateTime reportStartDate;
		private DateTime reportEndDate;
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
		private int[,,] DayHrMinCount = new int[32,24,60];  //1-31 - 0-23,0-59
		private int[,,] DayHrMinSurge = new int[32,24,60];	
		
		private int[] HourCount   = new int[24];
		private double[] HourTotTime = new double[24];

		private int[] DayCount   = new int[32];
		private double[] DayTotTime = new double[32];

        private int NumTypesToReport;
        private int[] RefType = new int[50];
        private string[] RefTypeCode = new string[50];
        private string[] RefTypeDesc = new string[50];
        private string[] RefChannelDesc = new string[50];
        private int[] NominalCount = new int[50];
        private int[] ActualCount = new int[50];
        private int[] MissingCount = new int[50];
        private int[] MissingPercent = new int[50];
        private int[] FailedCount = new int[50];
        private int[] FailedPercent = new int[50];
        private int[] AmberCount = new int[50];
        private int[] AmberPercent = new int[50];
        private int[] RedCount = new int[50];
        private int[] RedPercent = new int[50];

        private int[, ,] RefCount = new int[50, 8, 24];

        private string[] ChannelDesc = new string[10];
        private int[] ChannelNominalCount = new int[10];
        private int[] ChannelActualCount = new int[10];
        private int[] ChannelMissingCount = new int[10];
        private int[] ChannelMissingPercent = new int[10];
        private int[] ChannelFailedCount = new int[10];
        private int[] ChannelFailedPercent = new int[10];
        private int[] ChannelAmberCount = new int[10];
        private int[] ChannelAmberPercent = new int[10];
        private int[] ChannelRedCount = new int[10];
        private int[] ChannelRedPercent = new int[10];

        

		private	string[,] DayHrString = new string[32,24];
		private int duration;
		private int surgeMinCount = 0;
		private int endDay = 0;
		private int startHour = 0;
		private int endHour = 0;
		private int startDay = 0;
		private int startMinute = 0;
		private int endMinute = 0;
        private string defaultInjectorString;
        private string backupInjectorString;



        public MonthlyAverageReponseTimeReport()
        //constructor
        {
            EventLogger = new EventLog("Application");
            EventLogger.Source = "TD.Reporting";
            controller = new DailyReportController();

            ConfigurationManager.GetSection("appSettings");
            propertyConnectionString = ConfigurationManager.AppSettings["ReportProperties.connectionstring"];
            reportingConnectionString = ConfigurationManager.AppSettings["ReportDatabase.connectionstring"];
        }
		
		public int RunReport(DateTime reportDate)
		{
			int day = 0, hour = 0, minute = 0;
			int transactionCount = 0;
			int ResponseTimeSum = 0;
			statusCode = (int)StatusCode.Success; // assume success
			string reportDateString;
			string CapacityBandString;			
			int CapacityBand = 0, RTEtype = 0;

			//Set up date / time variables
			reportDateString = reportDate.ToShortDateString();
            RequestedDateString = reportDate.Year.ToString().Substring(2, 2)
             + "-" + reportDate.Month.ToString().PadLeft(2, '0');
            currentDateTime = System.DateTime.Now;
			reportStartDate = new DateTime(reportDate.Year,reportDate.Month,1,0,0,0);
			reportEndDate = reportStartDate.AddMonths(1);
			reportEndDate = reportEndDate.AddDays(-1);
			reportStartDateString = reportStartDate.ToShortDateString();
			reportEndDateString = reportEndDate.ToShortDateString();
			
			//Set up "Day" header row using a stringbuilder
			tmpString = new StringBuilder();
			tmpString.Append("Hour,0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23");
			string daysString = tmpString.ToString();

			bool readOK =  controller.ReadProperty("9", "FilePath", out filePathString);
			if (readOK == false || filePathString.Length == 0)
			{
				statusCode = (int)StatusCode.ErrorReadingFilePath; // Error Reading FilePath
				return statusCode;
			}
            filePathString = filePathString.Replace("%YY-MM%", RequestedDateString);
    
			// open report properties DB and read in the current capacity band.
			readOK =  controller.ReadProperty("0", "CurrentCapacityBand", out CapacityBandString);
			if (readOK == false)
			{
				statusCode = (int)StatusCode.ErrorReadingCapacityBand; // Error Reading Capacity Band Property
				return statusCode;
			}
			CapacityBand = Convert.ToInt32(CapacityBandString, CultureInfo.CurrentCulture);

			//		First stage - 

			// find surge periods
			//		Second stage - 
			//		Get all the hours which contained a period that was in surge
			//		This bit copied pretty much verbatim from monthly surge report!

			int CBMaxRequests = 0;
			int CBSurgeThreshold = 0;
			float CBSurgePercentage = 0;

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
				EventLogger.WriteEntry ("Error Reading Capacity Band table"+e.ToString(),EventLogEntryType.Error);
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
			
			// clear the arrays
			for (day = 1; day <= reportEndDate.Day;day++)
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
			selectString = "select day(WEDate), WEHour, WEMinute, isnull(sum(WECount),0)from dbo.WorkloadEvents "
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
						DayHrMinSurge[day,hour,minute] = DayHrMinSurge[day,hour,minute]+transactionCount;
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
			for (day = 1; day <= reportEndDate.Day;day++)
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
			

			// open report text file
			file = new StreamWriter( new FileStream(filePathString, System.IO.FileMode.Create ) );

            myDataReader.Close();
            reportingSqlConnection.Close();



            // find which reference events need reporting on
            selectString = "select rttid,rttcode,rttdescription "
                        + "from dbo.ReferenceTransactionType "
                        + "where RTTSLAInclude = 1 and rttid >= 51 ";





           reportingSqlConnection = new SqlConnection(reportingConnectionString);

			myDataReader = null;
			mySqlCommand = new SqlCommand(selectString, reportingSqlConnection);
            try
            {
                reportingSqlConnection.Open();
                myDataReader = mySqlCommand.ExecuteReader(CommandBehavior.CloseConnection);



                NumTypesToReport = 0;

                for (NumTypesToReport = 0; NumTypesToReport < 50; NumTypesToReport++)
                {
                    RefType[NumTypesToReport] = 0;
                    RefTypeCode[NumTypesToReport] = null;
                    RefTypeDesc[NumTypesToReport] = null;
                }
                NumTypesToReport = 0;
                while (myDataReader.Read())
                {
                    RefType[NumTypesToReport] = Convert.ToInt32(myDataReader.GetSqlByte(0).ToString());
                    RefTypeCode[NumTypesToReport] = myDataReader.GetString(1);
                    RefTypeDesc[NumTypesToReport] = myDataReader.GetString(2);
                     NumTypesToReport++;

                }
            }
            catch (Exception e)
            {
                EventLogger.WriteEntry("Failure producing MonthlyAverageResponseTime report " + e.Message, EventLogEntryType.Error);
                statusCode = (int)StatusCode.ErrorReadingCalendar; // Failed to calendar
            }
            finally
            {
                // Always call Close when done reading.
                if (myDataReader != null)
                    myDataReader.Close();
            }
            myDataReader.Close();


            			//Calculate the figures for each Transaction type
            for (RTEtype = 0; RTEtype < NumTypesToReport; RTEtype++)
			{


			this.file.WriteLine("");
			this.file.WriteLine("");
			this.file.WriteLine("------------------------------------------");
			this.file.WriteLine("TRANSPORT DIRECT");
			this.file.WriteLine("Monthly Average Reponse Time Report");
			this.file.WriteLine("------------------------------------------");
			this.file.WriteLine("");
			this.file.WriteLine("For month starting : " + reportStartDate.ToShortDateString());
			this.file.WriteLine("");
            this.file.WriteLine("For Transactions of type : " +RefTypeCode[RTEtype] + " " + RefTypeDesc[RTEtype]);
			this.file.WriteLine("");
			this.file.WriteLine("(* = Hour contained a period in surge which are not included in mean times)");
			this.file.WriteLine("Figures shown are average response times in seconds discounting any SLA surge periods");
			this.file.WriteLine("");
			this.file.WriteLine(daysString+",Day Avg.");
			this.file.WriteLine("Day");
			this.file.Flush();
			reportingSqlConnection = new SqlConnection(reportingConnectionString);

			//	===============
			//	Second stage create array of times of successful reference events
		    //  marking surge times as -1  so they are identified and not reported.
            /////////////////////////////////////////////////////////////////////////////////////////////////
            // find correct injector transactions
            readOK = controller.ReadProperty("0", "DefaultInjector", out defaultInjectorString);
            if (readOK == false || defaultInjectorString.Length == 0)
            {
                statusCode = (int)StatusCode.ErrorReadingFilePathDefaultInjector; // Error Reading FilePath
                return statusCode;
            }
            readOK = controller.ReadProperty("0", "BackupInjector", out backupInjectorString);
            if (readOK == false || backupInjectorString.Length == 0)
            {
                statusCode = (int)StatusCode.ErrorReadingFilePathBackupInjector; // Error Reading FilePath
                return statusCode;
            }
            //.............................	

			//		Get the Reference transaction events data
			try
			{
				// load  array of reference transactions of type 
				selectString = "select day(RTEDate), RTEHour, RTEMinute, isnull(RTEMsDuration,0)"
					+ " from ReferenceTransactionEvents "
					+ "where RTEDate >= convert(datetime, '" + reportStartDateString 
					+ "',103) and RTEDate <= convert(datetime, '" + reportEndDateString 
					+ "',103) and RTERTTID = " + RefType[RTEtype] + " and RTESuccessful = 1"
                    + " and ( RTEMachineName not like '" + backupInjectorString + "' "
                    + "or RTEMachineName is NULL ) "
                    + " order by RTEDate, RTEHour,RteMinute ";
					myDataReader = null;
				mySqlCommand = new SqlCommand(selectString, reportingSqlConnection);
					reportingSqlConnection.Open();
				myDataReader = mySqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
				while (myDataReader.Read())
				{
					day = Convert.ToInt32(myDataReader.GetInt32(0).ToString());
					hour = Convert.ToInt32(myDataReader.GetSqlByte(1).ToString());
					minute = Convert.ToInt32(myDataReader.GetSqlByte(2).ToString());
					ResponseTimeSum = Convert.ToInt32(myDataReader.GetSqlInt32(3).ToString());
				
					if (DayHrMinSurge[day,hour,minute] == 1)
						ResponseTimeSum = -1;
					DayHrMinCount[day,hour,minute] = ResponseTimeSum;;
			
				} // end of while
				myDataReader.Close();
				//end of first quarter hour
			}
			catch(Exception e)
			{
				EventLogger.WriteEntry ("Failure reading reference transaction table "+e.ToString(),EventLogEntryType.Error);
				statusCode = (int)StatusCode.ErrorReadingFilePath; // Failed to reading page events table
			}
			finally
			{
				// Always call Close when done reading.
				if (myDataReader != null)
					myDataReader.Close();
			}



			//		Third stage - 
			//		Cycle through RTE array X-referencing against Surge periods array
			

			double RunningSum = 0;
			int Divisor = 0;
			int Avg = 0;
			bool SurgePeriod = false;
			int meanTimeSec = 0;
			int meanTimePartSec = 0;
			for (hour=0;hour<24;hour++)
			{
				HourCount[hour]    = 0;
				HourTotTime[hour]  = 0;
			}

			for (day=1;day<=reportEndDate.Day;day++)
			{
				tmpString = new StringBuilder();
				tmpString.Append(day.ToString()+",");

				DayCount[day]    = 0;
				DayTotTime[day]  = 0;

				for (hour=0;hour<24;hour++)
				{
					RunningSum = 0;
					Divisor = 0;
					Avg = 0;
					SurgePeriod = false;
	
					for(minute = 0; minute < 60 ; minute++)
					{
						if (DayHrMinCount[day,hour,minute] > 0)
						{
							DayCount[day]      = DayCount[day]     + 1;
							DayTotTime[day]    = DayTotTime[day]   + (DayHrMinCount[day,hour,minute] * 1000);
							HourCount[hour]    = HourCount[hour]   + 1;
							HourTotTime[hour]  = HourTotTime[hour] + (DayHrMinCount[day,hour,minute] * 1000);

							RunningSum = RunningSum + (DayHrMinCount[day,hour,minute] * 1000);
							Divisor = Divisor+1;
							Avg = (int) (RunningSum / Divisor);
						}
						else
						{
							if (DayHrMinCount[day,hour,minute] > 0)
							{
								SurgePeriod = true;
							}
						}
					}
					
					
					meanTimeSec = (Avg/1000000);
					meanTimePartSec = ((Avg/1000 - meanTimeSec*1000)+5)/10 ;
					if (meanTimePartSec < 0 ) 
					{
						meanTimePartSec = 0 ;
					}
					DayHrString[day,hour] = meanTimeSec.ToString("D2")
						+"."+meanTimePartSec.ToString("D2");
					
					if(SurgePeriod == true)
					{
						DayHrString[day,hour] = DayHrString[day,hour] +"*";
					}
					tmpString.Append(DayHrString[day,hour]+",");

				}// end hr
//				this.file.WriteLine(tmpString.ToString());
// print average for this day line
				Avg = 0;
				if (DayCount[day] > 0)
				{
						Avg = (int) (DayTotTime[day] / DayCount[day]);
				}
				meanTimeSec = (Avg/1000000);
				meanTimePartSec = ((Avg/1000 - meanTimeSec*1000)+5)/10 ;
				if (meanTimePartSec < 0 ) 
				{
					meanTimePartSec = 0 ;
				}
				tmpString.Append(meanTimeSec.ToString("D2")
					+"."+meanTimePartSec.ToString("D2"));
				this.file.WriteLine(tmpString.ToString());
				
//


				}

				// print line of all hourly averages

				tmpString = new StringBuilder();
				tmpString.Append("Hour Average,");

				for (hour = 0;hour<=23;hour++)
				{
					Avg = 0;
					if (HourCount[hour] > 0)
					{
						Avg = (int) (HourTotTime[hour] / HourCount[hour]);
					}
					meanTimeSec = (Avg/1000000);
					meanTimePartSec = ((Avg/1000 - meanTimeSec*1000)+5)/10 ;
					if (meanTimePartSec < 0 ) 
					{
						meanTimePartSec = 0 ;
					}
					tmpString.Append(meanTimeSec.ToString("D2")
						+"."+meanTimePartSec.ToString("D2")+",");
				
					//

				}
				this.file.WriteLine(tmpString.ToString());

			this.file.Flush();

		} //end of looping through 3 different transaction types

			this.file.Close();

			//E - MAIL OUT THE REPORT
			string from = "";
			string to = "";
			string smtpServer = "";
			string subject = "";

			readOK =  controller.ReadProperty("9", "FilePath", out filePathString);
			if (readOK == false || filePathString.Length == 0)
			{
				statusCode = (int)StatusCode.ErrorReadingFilePath; // Error Reading FilePath
				return statusCode;
			}
            filePathString = filePathString.Replace("%YY-MM%", RequestedDateString);

			readOK =  controller.ReadProperty("0", "MailAddress", out from);
			if (readOK == false || from.Length == 0)
			{
				statusCode = (int)StatusCode.ErrorReadingSenderEmailAddress; // Error Reading sender email address
				return statusCode;
			}
			readOK =  controller.ReadProperty("9", "MailRecipient", out to);
			if (readOK == false || to.Length == 0)
			{
				statusCode = (int)StatusCode.ErrorReadingRecipientEmailAddress; // Error Reading recipient email address
				return statusCode;
			}
			readOK =  controller.ReadProperty("9", "Title", out subject);
			if (readOK == false || subject.Length == 0)
			{
                subject = "Monthly SLA Response Time Report %YY-MM%";
			}
            subject = subject.Replace("%YY-MM%", RequestedDateString);
            readOK = controller.ReadProperty("0", "smtpServer", out smtpServer);
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
