///<header>
///Monthly Transaction Performance Report (Monthly SLA Exception Report
///Created 14/12/2003
///Author R Broddlet
///
///Version	Date		Who	Reason
///1		14/12/2004	RB	Created
///2		23/02/2004	PS	Adjust method of finding surge
///3		03/03/2004	PS	display % figures of failed/overran transactions/ Renaming
///4        30/06/2004  PS  ensure full last day is taken into account by selecting to last minute
///5        05/11/2004  PS  parameterise the target response times and sla printed ref txs
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
	/// Class to produce Monthly Transaction Performance Report for capacity planning
 	/// analyses all recorded event tables and reports any SLA breaches
 	/// Path: c:\MonthlyTransactionPerformanceReport.csv
	/// </summary>
	/// 
	
	public class MonthlyTransactionPerformanceReport
	{
		private DateTime currentDateTime;
		private string reportStartDateString;
		private string reportEndDateString;
		private DateTime reportStartDate;
		private DateTime reportEndDate;

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
		private int day;
		private int hour;
		private int minute;
		private int[,,] DayHrMinSurge = new int[32,24,60];  //1-31 - 0-23,0-59
		private int[,,] DayHrMinCount = new int[32,24,60];  //1-31 - 0-23,0-59
		private int[,,] DayHrMinSuccess = new int[32,24,60];  //1-31 - 0-23,0-59
		private int transactionCount = 0;
		private int surgeMinCount = 0;
		private int endDay = 0;
		private int startHour = 0;
		private int endHour = 0;
		private int startDay = 0;
		private int startMinute = 0;
		private int endMinute = 0;
		private int duration = 0;
        private string RequestedDateString;
        private int CBMaxRequests;
		private int CBSurgeThreshold;
		
		private int NumTypesToReport;
		private int[] ResponseTarget  = new int[20];

      
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
        private int daysToReportOn;
        
        private int[, ,] RefCount = new int[50, 31, 24];

        private int NumChannelsToReport;
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
        private string defaultInjectorString;
        private string backupInjectorString;
        private string travelineInjectorString;



		public MonthlyTransactionPerformanceReport()
		//constructor
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
			statusCode = (int)StatusCode.Success; // assume success
			string reportDateString;
			string CapacityBandString;			
			int CapacityBand = 0;

			//Set up date / time variables
            RequestedDateString = reportDate.Year.ToString().Substring(2, 2)
            + "-" + reportDate.Month.ToString().PadLeft(2, '0');
     
			reportDateString = reportDate.ToShortDateString();
			currentDateTime = System.DateTime.Now;
			reportStartDate = new DateTime(reportDate.Year,reportDate.Month,1,0,0,0);
			reportEndDate = reportStartDate.AddMonths(1);
			reportEndDate = reportEndDate.AddMinutes(-1);
			reportStartDateString = reportStartDate.ToString();
			reportEndDateString = reportEndDate.ToString();
			
			//Set up "Day" header row using a stringbuilder
			tmpString = new StringBuilder();
			tmpString.Append(",Day,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28");
			if (reportEndDate.Day == 29)
			{
				tmpString.Append(",29");
			}
			if (reportEndDate.Day == 30)
			{
				tmpString.Append(",29,30");
			}
			if (reportEndDate.Day == 31)
			{
				tmpString.Append(",29,30,31");
			}
			string daysString = tmpString.ToString();

			bool readOK =  controller.ReadProperty("10", "FilePath", out filePathString);
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


			// open report text file
			file = new StreamWriter( new FileStream(filePathString, System.IO.FileMode.Create ) );
			// find the minute threshold figure  for this capacity band

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
			

//	===============



            reportingSqlConnection = new SqlConnection(reportingConnectionString);


            for (day = 1; day <= 7; day++)
            {
                for (hour = 0; hour <= 23; hour++)
                {
                    for (minute = 0; minute <= 59; minute++)
                    {
                        DayHrMinCount[day, hour, minute] = 0;
                    }
                }
            }

            // look for reference events-
            // find which reference events need reporting on

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
                ActualCount[i] = 0;
                MissingCount[i] = 0;
                FailedCount[i] = 0;
                FailedPercent[i] = 0;
                AmberCount[i] = 0;
                AmberPercent[i] = 0;
                RedCount[i] = 0;
                RedPercent[i] = 0;
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
                daysToReportOn = reportEndDate.Day;
                NominalCount[NumTypesToReport] = (daysToReportOn * 1440 / frq);




                if (NominalCount[NumTypesToReport] > ActualCount[NumTypesToReport])
                {
                    MissingCount[NumTypesToReport] = NominalCount[NumTypesToReport] - ActualCount[NumTypesToReport];
                    MissingPercent[NumTypesToReport] = (((MissingCount[NumTypesToReport] * 1000) / NominalCount[NumTypesToReport]) + 5) / 10;
                }
                else
                {
                    MissingCount[NumTypesToReport] = 0;
                    MissingPercent[NumTypesToReport] = 0;
                }

                FailedPercent[NumTypesToReport] = (((FailedCount[NumTypesToReport] * 1000) / NominalCount[NumTypesToReport]) + 5) / 10;


                if (red != 0 && RefPassed != 0)
                {
                    if (red - refduration < 0) 
                    {
                        RedCount[NumTypesToReport]++; 
                    }
                    else
                    {
                        if (amber - refduration < 0) AmberCount[NumTypesToReport]++;
                    }
                    AmberPercent[NumTypesToReport] = (((AmberCount[NumTypesToReport] * 1000) / NominalCount[NumTypesToReport]) + 5) / 10;
                    RedPercent[NumTypesToReport] = (((RedCount[NumTypesToReport] * 1000) / NominalCount[NumTypesToReport]) + 5) / 10;
                }
                else
                {
                    AmberCount[NumTypesToReport] = 0;
                    RedCount[NumTypesToReport] = 0;
                    AmberPercent[NumTypesToReport] = 0;
                    RedPercent[NumTypesToReport] = 0;
                }


            }
            myDataReader.Close();





            this.file.WriteLine("");
            this.file.WriteLine("");
            this.file.WriteLine("------------------------------------------");
            this.file.WriteLine("Monthly Transaction Performance ");
            this.file.WriteLine("------------------------------------------");
            this.file.WriteLine("");
            this.file.WriteLine("For month starting : " + reportStartDate.ToShortDateString());
            this.file.WriteLine("");
            this.file.WriteLine("Transaction,Code,Nominal,Actual,Missing,Missing,Failed,Failed,Amber,Amber,Red,Red");
            this.file.WriteLine("Description,Code,Count,Count,Count,(%),Count,(%),Count,(%),Count,(%)");
            this.file.Flush();

           
            for (int i = 1; i <= NumTypesToReport; i++)
            {
                StringBuilder tempString = new StringBuilder();
                tempString.Append(RefTypeDesc[i] + ",");
                tempString.Append(" " + RefTypeCode[i] + ",");
                tempString.Append(NominalCount[i] + ",");
                tempString.Append(ActualCount[i] + ",");
                tempString.Append(MissingCount[i] + ",");
                tempString.Append(MissingPercent[i] + ",");
                tempString.Append(FailedCount[i] + ",");
                tempString.Append(FailedPercent[i] + ",");

                tempString.Append(AmberCount[i] + ",");
                tempString.Append(AmberPercent[i] + ",");
                tempString.Append(RedCount[i] + ",");
                tempString.Append(RedPercent[i]);
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
                    }
                }
            }

            this.file.WriteLine("");
            this.file.WriteLine("");
            this.file.WriteLine("------------------------------------------");
            this.file.WriteLine("Monthly Channel Performance ");
            this.file.WriteLine("------------------------------------------");
            this.file.WriteLine("");
            this.file.WriteLine("");
            this.file.WriteLine("Channel,Nominal,Actual,Missing,Missing,Failed,Failed,Amber,Amber,Red,Red");
            this.file.WriteLine("Description,Count,Count,Count,(%),Count,(%),Count,(%),Count,(%)");
            this.file.Flush();

            for (int i = 0; i < NumChannelsToReport; i++)
            {
                StringBuilder tempString = new StringBuilder();
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
                tempString.Append(ChannelRedPercent[i]);
                this.file.WriteLine(tempString);
            }

            this.file.Flush();
            this.file.Close();






		//E - MAIL OUT THE REPORT
		string from = "";
		string to = "";
		string smtpServer = "";
		string subject = "";

		readOK =  controller.ReadProperty("10", "FilePath", out filePathString);
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
		readOK =  controller.ReadProperty("10", "MailRecipient", out to);
		if (readOK == false || to.Length == 0)
		{
			statusCode = (int)StatusCode.ErrorReadingRecipientEmailAddress; // Error Reading recipient email address
			return statusCode;
		}
		readOK =  controller.ReadProperty("10", "Title", out subject);
		if (readOK == false || subject.Length == 0)
		{
            subject = "Monthly Transaction Performance Report %YY-MM%";
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
