///<header>
///Monthly Weighted Transaction Report
///Created 05/10/2004
///Author JP Scott
///
///Version	Date		Who	Reason
///1		01/10/2004	PS	Created
///2        05/11/2004  PS amend daily reporting period
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
	/// Class to produce a Transaction Report showing durations weighted by Workload
	/// analyses for a pre determined period.
	/// </summary>
	/// 
	
	public class MonthlyWeightedTransactionReport
	{
		private DateTime currentDateTime;
		private string reportStartDateString;
		private string reportEndDateString;
		private DateTime reportStartDate;
		private DateTime reportEndDate;
		private DateTime reportEndDatePlus1;
		private string reportEndDatePlus1String;
		private string reportFrequency;
		

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
		
		private int sec;
		private int refType;
		private int[,] numRefs = new int [30,31];
		private int[,] workLoadWeighting = new int [30,31];
		private int[,] workLoadWeightingExcl = new int [30,31];

		private int[] monthlyDayTot = new int[32];
		private int[,,] DayHrMinWLCount = new int[32,24,60];  //1-31 - 0-23,0-59
//		private int[,,] DayHrMinSuccess = new int[32,24,60];  //1-31 - 0-23,0-59
		private int[,,] DayHrMinWasTest = new int[32,24,60];  //1-31 - 0-23,0-59
		private int[,,,] RefDayHrMinRefCount= new int[6,32,24,60];  //1-31 - 0-23,0-59

		private int day;
		private int hour;
		private int minute;
		private int duration = 0;
		private int success;
		private int begin_dayhourmin;
		private int end_dayhourmin;
        private string RequestedDateString;
				
		private string dayOfWeek;
		private DateTime[] TestStarted = new DateTime[250]; 
		private DateTime[] TestCompleted = new DateTime[250]; 
		private int testCount;
		private int transactionCount;
        private string defaultInjectorString;
        private string backupInjectorString;
        private string travelineInjectorString;



        public MonthlyWeightedTransactionReport()
        {
            EventLogger = new EventLog("Application");
            EventLogger.Source = "TD.Reporting";
            controller = new DailyReportController();

            ConfigurationManager.GetSection("appSettings");
            propertyConnectionString = ConfigurationManager.AppSettings["ReportProperties.connectionstring"];
            reportingConnectionString = ConfigurationManager.AppSettings["ReportDatabase.connectionstring"];
        }
		
		public int RunReport(DateTime reportDate,int reportNumber)
		{
			statusCode = (int)StatusCode.Success; // success

			// Get the report frequency
			bool readOK =  controller.ReadProperty(reportNumber.ToString(), "Frequency", out reportFrequency);
			if (readOK == false || reportFrequency == "")
			{
				statusCode = (int)StatusCode.ErrorReadingFrequency; // Error Reading FilePath
				return statusCode;
			}


			string reportDateString;

            RequestedDateString = reportDate.Year.ToString().Substring(2, 2)
                + "-" + reportDate.Month.ToString().PadLeft(2, '0')
                + "-" + reportDate.Day.ToString().PadLeft(2, '0');
  
			//Set up date / time variables
			reportDateString = reportDate.ToShortDateString();
			currentDateTime = System.DateTime.Now;


			if(reportFrequency == "D")
			{
				reportStartDate = reportDate;
				reportStartDate = new DateTime(reportDate.Year,reportDate.Month,reportDate.Day,0,0,0);
				reportEndDate = reportStartDate.AddDays(1);
				reportEndDate = reportEndDate.AddSeconds(-1);
				reportStartDateString = reportStartDate.ToString();
				reportEndDateString = reportEndDate.ToString();

				
				reportEndDatePlus1 = reportEndDate.AddDays(1);
				reportEndDatePlus1String = reportEndDatePlus1.ToShortDateString();
			}
			if(reportFrequency == "M")
			{
                RequestedDateString = reportDate.Year.ToString().Substring(2, 2)
                + "-" + reportDate.Month.ToString().PadLeft(2, '0');
				reportStartDate = new DateTime(reportDate.Year,reportDate.Month,1,0,0,0);
				reportEndDate = reportStartDate.AddMonths(1);

				reportEndDatePlus1 = reportEndDate;
				reportEndDatePlus1String = reportEndDatePlus1.ToShortDateString();

				reportEndDate = reportEndDate.AddMinutes(-1);
				reportStartDateString = reportStartDate.ToString();
				reportEndDateString = reportEndDate.ToString();
			}			
			if(reportFrequency == "W")
			{
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
			}

			tmpString = new StringBuilder();


			readOK =  controller.ReadProperty(reportNumber.ToString(), "FilePath", out filePathString);
			if (readOK == false || filePathString.Length == 0)
			{
				statusCode = (int)StatusCode.ErrorReadingFilePath; // Error Reading FilePath
				return statusCode;
			}
            if (reportFrequency == "M")
            {
                filePathString = filePathString.Replace("%YY-MM%", RequestedDateString);
            }
            else
            {
                filePathString = filePathString.Replace("%YY-MM-DD%", RequestedDateString);
            } 
          
			// clear arrays
			for(day = 1;day <= 31;day++)
			{
				for(hour = 0;hour < 24; hour++)
				{
					for(minute = 0;minute < 59; minute++)
					{
						DayHrMinWLCount[day,hour,minute]=0;
//						DayHrMinSuccess[day,hour,minute]=0;
						DayHrMinWasTest[day,hour,minute]=0;

						for(int reftype = 0;reftype<6;reftype++)
						{
							RefDayHrMinRefCount[reftype,day,hour,minute] = 0;
						}
					}
				}
			}


			// open report text file
			file = new StreamWriter( new FileStream(filePathString, System.IO.FileMode.Create ) );
			this.file.WriteLine("------------------------------------------");
			this.file.WriteLine("TRANSPORT DIRECT");
			this.file.WriteLine("Weighted Reference Transaction Report");
			this.file.WriteLine("------------------------------------------");
			this.file.WriteLine("");
			if (reportFrequency == "W")
			{
				this.file.WriteLine("For week starting : " + reportStartDate.ToShortDateString());
			}
			if (reportFrequency == "D")
			{
				this.file.WriteLine("For Date : " + reportStartDate.ToShortDateString());
			}
			if (reportFrequency == "M")
			{
				this.file.WriteLine("For Month Starting : " + reportStartDate.ToShortDateString());
			}

			this.file.WriteLine("");
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
					begin_dayhourmin = (TestBegins.Day-1)*24*60;
					begin_dayhourmin += (TestBegins.Hour)*60;
					begin_dayhourmin += TestBegins.Minute;

					end_dayhourmin  = (TestEnds.Day-1)*24*60;
					end_dayhourmin += (TestEnds.Hour)*60;
					end_dayhourmin += TestEnds.Minute;

					for (int i = begin_dayhourmin;i <= end_dayhourmin; i++)
					{
						day    = (int)Math.Floor((double)(i/(24*60))+1);

						hour   = (int)Math.Floor((double)((i-((day-1)*24*60))/(60)));
                        minute = (int)Math.Floor((double)((i - ((day - 1) * 24 * 60) - (hour * 60))));
						DayHrMinWasTest[day,hour,minute]=1;
					}
					
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


//  Now lets select the workload events and fill in the minute array

			// select all the workload events between the relevent dates and put into the array
			DateTime WEDate = new DateTime(1,1,1,0,0,0);

			selectString = "select WEDate,WEHour,WEMinute,isnull(sum(WECount),0)from dbo.WorkloadEvents "
				+ "where WEDate >= convert(datetime, '" + reportStartDateString 
				+ "',103) and WEDate <= convert(datetime, '" + reportEndDateString
				+ "',103) group by WEDate,WEHour,WEMinute order by WEDate,WEHour,WEMinute";

			myDataReader = null;
			mySqlCommand = new SqlCommand(selectString, reportingSqlConnection);
			try
			{
				reportingSqlConnection.Open();
				myDataReader = mySqlCommand.ExecuteReader(CommandBehavior.CloseConnection);

				
				while (myDataReader.Read())
				{
					WEDate = Convert.ToDateTime(myDataReader.GetDateTime(0).ToString());
					day = WEDate.Day;
					if (reportFrequency == "D")
					{
						day = 1;
					}
					if (reportFrequency == "W")
					{

						dayOfWeek = WEDate.DayOfWeek.ToString();
						switch (dayOfWeek)
						{
							case "Monday":
								day = 1;
								break;
							case "Tuesday":
								day=2;
								break;
							case "Wednesday":
								day=3;
								break;
							case "Thursday":
								day=4;
								break;
							case "Friday":
								day=5;
								break;
							case "Saturday":
								day=6;
								break;
							case "Sunday":
								day=7;
								break;
						}

					}					
					hour = Convert.ToInt32(myDataReader.GetSqlByte(1).ToString());
					minute = Convert.ToInt32(myDataReader.GetSqlByte(2).ToString());
					transactionCount = Convert.ToInt32(myDataReader.GetSqlInt32(3).ToString());
					DayHrMinWLCount[day,hour,minute] +=	transactionCount;
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


// now create an array of reference transaction durations and apply weightings from workload array

			// look for reference events-
			// load  array of reference transactions count by day / minute in day

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


			selectString = "select RTEDate, RTEHour, RTEMinute,RTERTTID,RTESuccessful,RTEMsDuration "
				+" from dbo.ReferenceTransactionEvents "
				+ "where RTEDate >= convert(datetime, '" + reportStartDateString 
				+ "',103) and RTEDate <= convert(datetime, '" + reportEndDateString
				+ "',103) and rterttid in (1,2,3,20,21,22) "
                + "and (RTEMachineName not like '" + backupInjectorString + "' "
                + " and RTEMachineName not like '" + travelineInjectorString + "' ) "
                + " order by RTErttid,RTEDate, RTEHour, RTEMinute";

			DateTime RTEDate = new DateTime(1,1,1,0,0,0);

			myDataReader = null;
			mySqlCommand = new SqlCommand(selectString, reportingSqlConnection);
			try
			{
				reportingSqlConnection.Open();
				myDataReader = mySqlCommand.ExecuteReader(CommandBehavior.CloseConnection);

				int lastrefType =0;
				int lastday =0;
				int lasthour = 0;
				int lastminute = 0;
				int numdups = 0;
				while (myDataReader.Read())
				{
					RTEDate = Convert.ToDateTime(myDataReader.GetDateTime(0).ToString());
					day = RTEDate.Day;
					if (reportFrequency == "D")
					{
						day = 1;
					}
					if (reportFrequency == "W")
					{
						dayOfWeek = RTEDate.DayOfWeek.ToString();
						switch (dayOfWeek)
						{
							case "Monday":
								day = 1;
								break;
							case "Tuesday":
								day=2;
								break;
							case "Wednesday":
								day=3;
								break;
							case "Thursday":
								day=4;
								break;
							case "Friday":
								day=5;
								break;
							case "Saturday":
								day=6;
								break;
							case "Sunday":
								day=7;
								break;
						}

					}
					hour = Convert.ToInt32(myDataReader.GetSqlByte(1).ToString());
					minute = Convert.ToInt32(myDataReader.GetSqlByte(2).ToString());
					refType = Convert.ToInt32(myDataReader.GetSqlByte(3).ToString());
					success = Convert.ToInt32(myDataReader.GetSqlByte(4).ToString());
					duration = Convert.ToInt32(myDataReader.GetSqlInt32(5).ToString());

					if (success == 0 || duration > 30000)
					{
						duration = 30000;
					}

// slot into arrays
					if (refType == lastrefType && day == lastday && hour == lasthour && minute == lastminute)
					{
						numdups +=1;
						// this is a duplicate we dont want to count it
					}
					else
					{
						lastrefType=refType;
						lastday=day;
						lasthour = hour;
						lastminute = minute;
					
						sec = (int)Math.Floor((double)(duration/1000));
						numRefs[refType,sec] += 1;
					
					
						workLoadWeighting[refType,sec] += DayHrMinWLCount[day,hour,minute] ;
						if (DayHrMinWasTest[RTEDate.Day,hour,minute] == 0)
						{
							workLoadWeightingExcl[refType,sec] += DayHrMinWLCount[day,hour,minute];
						}
						RefDayHrMinRefCount[ReportRefType(refType),day,hour,minute] += 1;
					}
				} // end of while
				myDataReader.Close();
			}
			catch(Exception e)
			{
				EventLogger.WriteEntry ("Failure reading reference transaction table "+e.Message,EventLogEntryType.Error);
				statusCode = (int)StatusCode.ErrorReadingFilePath; // Failed to read events table
			}
			finally
			{
				// Always call Close when done reading.
				if (myDataReader != null)
					myDataReader.Close();
			}

//now lets printout 

			refType = 1;
			PrintRefReport();

			refType = 2;
			PrintRefReport();

			refType = 3;
			PrintRefReport();

			refType = 20;
			PrintRefReport();

			refType = 21;
			PrintRefReport();

			refType = 22;
			PrintRefReport();

			this.file.Flush();
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
            if (reportFrequency == "M")
            {
                filePathString = filePathString.Replace("%YY-MM%", RequestedDateString);
            }
            else
            {
                filePathString = filePathString.Replace("%YY-MM-DD%", RequestedDateString);
            } 
            readOK = controller.ReadProperty("0", "MailAddress", out from);
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
				subject = "Monthly Weighted Transaction Report %YY-MM%";
			}
            subject = subject.Replace("%YY-MM%", RequestedDateString);

			string bodyText =  "Report is attached....";


			SendEmail mailObject = new SendEmail();
			int sendFileErrorCode = mailObject.SendFile(from, to, subject, bodyText, 
				filePathString, smtpServer);
			if (sendFileErrorCode != 0)
				statusCode = sendFileErrorCode;

			return statusCode;

		}
		int ReportRefType(int refType)
		{
			int reftype = refType;
			switch (refType)
			{
				case 1:
					reftype = 0;
					break;
				case 2:
					reftype = 1;
					break;
				case 3:
					reftype = 2;
					break;
				case 20:
					reftype = 3;
					break;
				case 21:
					reftype = 4;
					break;
				case 22:
					reftype = 5;
					break;

			}
			return reftype;
		}
	
		void PrintRefReport()
		{
			float percent;
			float cumpercent;

			float percentExc;
			float cumpercentExc;

			this.file.WriteLine(" ");
			this.file.WriteLine("Reference type "+refType.ToString());
//			this.file.WriteLine("Duration,No Ref trans,WorkLoad,Weighting,Weighting %,Cumlative Weight %,Workload(Excl Tests),Weighting(Excl.),Weighting %(Excl.),Cumlative Weight(Excl.) %");
			this.file.WriteLine("Duration,No Ref trans,WorkLoad,Weighted %,Cumlative Weighted %,Workload(Excl Tests),Weighted %(Excl.),Cumlative Weighted(Excl.) %");
			ulong weight;
			ulong cumweight;
			ulong weightExcl;
			ulong cumweightExc;
			int toSec;
			string secString;
			ulong totweight=0;
			ulong totweightExc=0;
			for (int i=0;i<=30;i++)
			{
				totweight +=(ulong) numRefs[refType,i]*(ulong)workLoadWeighting[refType,i];
				totweightExc += (ulong)numRefs[refType,i]* (ulong)workLoadWeightingExcl[refType,i];
			}
			cumweight = 0;
			cumweightExc = 0;
			for (int i=0;i<=30;i++)
			{
	
				weight = (ulong) numRefs[refType,i]* (ulong)workLoadWeighting[refType,i];
				cumweight +=weight;
				percent = (float)(Math.Floor((100000.0*(float)weight)/(float)totweight)/1000);
				cumpercent = (float)(Math.Floor((100000.0*(float)cumweight)/(float)totweight)/1000);

				weightExcl = (ulong)numRefs[refType,i]* (ulong)workLoadWeightingExcl[refType,i];
				cumweightExc +=weightExcl;
				percentExc = (float)(Math.Floor((100000.0*(float)weightExcl)/(float)totweightExc)/1000);
				cumpercentExc = (float)(Math.Floor((100000.0*(float)cumweightExc)/(float)totweightExc)/1000);

				toSec=i+1;
				secString = i.ToString()+" to "+toSec.ToString();
				if (i==0)
				{
					secString = "Less than 1 Sec.";
				}
				if (i == 30)
				{
					secString = "Greater than 30 Sec.";
				}

				if(numRefs[refType,i]>0)
				{
//					this.file.WriteLine(secString+','+numRefs[refType,i].ToString()+','+workLoadWeighting[refType,i].ToString()+','+weight.ToString()+','+percent+','+cumpercent+','+workLoadWeightingExcl[refType,i].ToString()+','+weightExcl.ToString()+','+percentExc+','+cumpercentExc);
					this.file.WriteLine(secString+','+numRefs[refType,i].ToString()
						+','+workLoadWeighting[refType,i].ToString()
						+','+percent+','+cumpercent
						+','+workLoadWeightingExcl[refType,i].ToString()
						+','+percentExc+','+cumpercentExc);
				}
			}

			int missingtx =0;
			int totworkload = 0;
			int totworkloadexcl=0;
					
			begin_dayhourmin = 0;
			if (reportFrequency == "D")
			{
				end_dayhourmin  = 1440;
			}
			if (reportFrequency == "W")
			{

				end_dayhourmin  = 7*1440;
			}
			if (reportFrequency == "M")
			{
				end_dayhourmin  = 1440*reportEndDate.Day;
			}
			int realDay = 0;
			for (int i = begin_dayhourmin;i < end_dayhourmin; i++)
			{
				
				if (i == 0){day=1;hour=0;minute=0;}
				day    = (int)Math.Floor((double)(i/(24*60))+1);
				hour   = (int)Math.Floor((double)((i-((day-1)*24*60))/(60)));
				minute = (int)Math.Floor((double)((i-((day-1)*24*60)-(hour*60))));

				if (RefDayHrMinRefCount[ReportRefType(refType),day,hour,minute]==0)
				{
					missingtx +=1;
					totworkload += DayHrMinWLCount[day,hour,minute];
					realDay = reportStartDate.AddDays((day-1)).Day;
					if(DayHrMinWasTest[realDay,hour,minute]==0)
					{
						totworkloadexcl += DayHrMinWLCount[day,hour,minute];
					}
				}
			}

			this.file.WriteLine("Missing,"+missingtx.ToString()+","+totworkload.ToString()+",,,"+totworkloadexcl.ToString()+",");
			this.file.WriteLine(" ");
			
			this.file.Flush();

		}
	}
}

