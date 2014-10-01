///<header>
///Daily Surge Report
///Created 14/11/2003
///Author JP Scott
///
///Version	Date		Who	Reason
///1		14/11/2003	PS	Created
///2		20/02/2004	PS	Amend Surge threshold and method of display
///3        21/09/2004  PS  show threshold levels
///
///</header>
using System;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Diagnostics;

using DailyReportFramework;

namespace DailyReportFramework
{
	/// <summary>
	/// Class to produce daily surge report
	/// reads workload event counts and checks whether there
	/// are more requests than the max number + surge percentage in capacity bands table .
	/// statusCodes
	/// 1001 Error Reading Current Capacity Band from Property Table
	///	1002 Failed to read CapacityBand table
	///	1003 Reading FilePath for this report
	///	1004 Failed to reading table
	/// 1005 Error Reading sender email address
	/// 1006 Error Reading recipient email address
	/// 1007 Error Reading smtpServer




	/// </summary>
	/// 
	
	public class DailySurgeReport
	{
		private DateTime currentDateTime;
        private string RequestedDateString;
        private EventLog EventLogger;
		DailyReportController controller;
		private string propertyConnectionString;
		private string reportingConnectionString;
		private SqlConnection reportingSqlConnection;
		private string selectString;
		private System.IO.StreamWriter file;
		private string filePathString;
		private int CBMaxRequests;
		private int CBSurgeThreshold;
		private int CBSurgeAmberThreshold;
		private float CBSurgePercentage;
		private int statusCode;
		private int Red1;
		private int Red2;
		private int Red3;
		private int Amber1;
		private int Amber2;		
		
		
		
		public DailySurgeReport()
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
			string reportDateStringMinus1;
			string reportDateStringMinus2;
			string CapacityBandString;
			int CapacityBand;
			int hour;
			int minute;
			int transactionCount;
			string from = "";
			string smtpServer = "";
			string to = "";
			string subject = "";
            RequestedDateString = reportDate.Year.ToString().Substring(2, 2)
            + "-" + reportDate.Month.ToString().PadLeft(2, '0')
            + "-" + reportDate.Day.ToString().PadLeft(2, '0');

			
			
			reportDateString = reportDate.ToShortDateString();
			reportDateStringMinus1 = reportDate.AddDays(-1).ToShortDateString();
			reportDateStringMinus2 = reportDate.AddDays(-2).ToShortDateString();
			currentDateTime = System.DateTime.Now;
			Red1 = 0;
			Red2 = 0;
			Red3 = 0;
			Amber1 = 0;
			Amber2 = 0;

			// open report properties DB and read in the current capacity band.
			
			bool readOK =  controller.ReadProperty("0", "CurrentCapacityBand", out CapacityBandString);
			if (readOK == false)
			{
				statusCode = (int)StatusCode.ErrorReadingCapacityBand; // Error Reading Capacity Band Property
				return statusCode;
			}
			CapacityBand = Convert.ToInt32(CapacityBandString, CultureInfo.CurrentCulture);
			

			// extract current capacity band CBMaxRequests
			// and CBSurgePercentage

			selectString = "select CBMAxRequests,CBSurgePercentage from CapacityBands where CBNumber = '" + CapacityBand + "'";
			reportingSqlConnection = new SqlConnection(reportingConnectionString);

			SqlDataReader myDataReader = null;
			SqlCommand mySqlCommand = new SqlCommand(selectString, reportingSqlConnection);
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
			CBSurgeAmberThreshold=Convert.ToInt32(Math.Floor( ((CBMaxRequests/8)*0.2*2)/60));
	

			readOK =  controller.ReadProperty("1", "FilePath", out filePathString);
			if (readOK == false)
			{
				statusCode = (int)StatusCode.ErrorReadingCapacityBand; // Error Reading FilePath
				return statusCode;
			}
            filePathString = filePathString.Replace("%YY-MM-DD%", RequestedDateString);






			// Select all Workload events for this date 
			// total the count for all 15min periods
			// report if over CBSurgeThreshold


			selectString = "select WEHour,WEMinute, isnull(sum(WECount),0) from dbo.WorkloadEvents "
			             + "where WEDate = convert(datetime, '" + reportDateString
						 + "',103) group by WEHour,WEMinute order by WEHour,WEMinute";

			reportingSqlConnection = new SqlConnection(reportingConnectionString);

			myDataReader = null;
			mySqlCommand = new SqlCommand(selectString, reportingSqlConnection);

			// open report text file
			file = new StreamWriter( new FileStream(filePathString, System.IO.FileMode.Create ) );
			this.file.WriteLine("-----------------------------------");
			this.file.WriteLine("TRANSPORT DIRECT DAILY SURGE REPORT");
			this.file.WriteLine("-----------------------------------");
			this.file.WriteLine("");
			this.file.WriteLine("For date : " + reportDateString);
			this.file.WriteLine("");
			this.file.WriteLine("The periods detailed below exceeded the surge threshold");
			this.file.WriteLine("for a period of 15 minutes or more. ");
			this.file.WriteLine("(Based on capacity band " + CapacityBandString + " values.)");
			this.file.WriteLine("");
			this.file.WriteLine("Start   End    Duration");
			this.file.WriteLine("-----   -----  ---------");
			this.file.Flush();

			try
			{
				reportingSqlConnection.Open();
				myDataReader = mySqlCommand.ExecuteReader(CommandBehavior.CloseConnection);

				// Always call Read before accessing data.
				int lineCount = 0;
				int surgeMinCount = 0;
				int hourMinute = 0;
				int startMinute = 0;
				int endMinute = 0;
				int duration = 0;
				int amberduration = 0;
				string strStartHour; 
				string strStartMin; 
				string strEndHour; 
				string strEndMin;
				string strDurHour; 
				string strDurMin; 
				int[] minCountArray = new int[1440];
				Red1 = 0;

				for(int i = 0;i <1440;i++)
				{
					minCountArray[i] = 0;
				}
				while (myDataReader.Read())
				{
					hour = Convert.ToInt32(myDataReader.GetSqlByte(0).ToString());
					minute = Convert.ToInt32(myDataReader.GetSqlByte(1).ToString());
					transactionCount = Convert.ToInt32(myDataReader.GetSqlInt32(2).ToString());
					hourMinute = hour*60+minute;					
					minCountArray[hourMinute] = transactionCount;
				} // end of while

				for(hourMinute = 0;hourMinute<1440;hourMinute++)
				{
					transactionCount = minCountArray[hourMinute];
					if (transactionCount > CBSurgeAmberThreshold)
					{
						amberduration = amberduration+1;
					}
					else
					{
						if (amberduration >=15)
						{
							Amber1 = 1;
						}
						amberduration = 0;
					}
						
					if (transactionCount > CBSurgeThreshold)
					{
						duration = duration+1;
						endMinute = hourMinute; 
						surgeMinCount = surgeMinCount +1;
					}
					else
					{
						if (duration >=15)
						{
							Red1 = 1;
							Amber1 = 1;
							// requires reporting
							hour = startMinute/60;
							strStartHour = hour.ToString("D2");
							
							minute = startMinute - ((startMinute/60)*60);
							strStartMin = minute.ToString("D2");

							hour = endMinute/60;
							strEndHour = hour.ToString("D2");
							minute = endMinute - ((endMinute/60)*60);
							strEndMin = minute.ToString("D2");

							hour = duration/60;
							strDurHour = hour.ToString("D2");
							minute = duration - ((duration/60)*60);
							strDurMin = minute.ToString("D2");

							this.file.WriteLine(strStartHour+ ":"+strStartMin+"   "+
								strEndHour+ ":"+strEndMin+"   "+
								strDurHour+ ":"+strDurMin);
							lineCount = lineCount+1;


						}
						duration = 0;
						startMinute = hourMinute;
					}
				}	

			
				if (lineCount == 0)
				{
					this.file.WriteLine("");
					this.file.WriteLine("");
					this.file.WriteLine("NO REPORTING PERIODS WERE IN SURGE TODAY");
				}
				this.file.WriteLine("");
				this.file.WriteLine("");
				this.file.WriteLine("TREND:-  "+surgeMinCount+" minutes exceeded surge threshold today.");
				this.file.WriteLine("         This represents "+Math.Floor((double)surgeMinCount/(double)14.4)+" percent of the time.");

				this.file.WriteLine("         (Note this figure reports on individual minutes which");
				this.file.WriteLine("          may not be in a 15min SLA continuous surge period.)");
					
				this.file.Flush();

			
			}
			catch(Exception e)
			{
				EventLogger.WriteEntry ("Failure reading workload events table "+e.Message,EventLogEntryType.Error);
				statusCode = (int)StatusCode.ErrorReadingTable; // Failed reading page events table
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
			this.file.Flush();


//-----------------------
			if (Amber1 > 0)
			{
				// we need to check previous days for breaches of thresholds

				// Select all Workload events for this date 
				// total the count for all 15min periods
				selectString = "select WEHour,WEMinute, isnull(sum(WECount),0) from dbo.WorkloadEvents "
					+ "where WEDate = convert(datetime, '" + reportDateStringMinus1
					+ "',103) group by WEHour,WEMinute order by WEHour,WEMinute";

				reportingSqlConnection = new SqlConnection(reportingConnectionString);

				myDataReader = null;
				mySqlCommand = new SqlCommand(selectString, reportingSqlConnection);

				try
				{
					reportingSqlConnection.Open();
					myDataReader = mySqlCommand.ExecuteReader(CommandBehavior.CloseConnection);

					// Always call Read before accessing data.
					int hourMinute = 0;
					int duration = 0;
					int amberduration = 0;
					int[] minCountArray = new int[1440];
					for(int i = 0;i <1440;i++)
					{
						minCountArray[i] = 0;
					}
					while (myDataReader.Read())
					{
						hour = Convert.ToInt32(myDataReader.GetSqlByte(0).ToString());
						minute = Convert.ToInt32(myDataReader.GetSqlByte(1).ToString());
						transactionCount = Convert.ToInt32(myDataReader.GetSqlInt32(2).ToString());
						hourMinute = hour*60+minute;					
						minCountArray[hourMinute] = transactionCount;
					} // end of while

					for(hourMinute = 0;hourMinute<1440;hourMinute++)
					{
						transactionCount = minCountArray[hourMinute];
						if (transactionCount > CBSurgeAmberThreshold)
						{
							amberduration = amberduration+1;
						}
						else
						{
							if (amberduration >=15)
							{
								Amber2 = 1;
							}
							amberduration = 0;
						}
						
						if (transactionCount > CBSurgeThreshold)
						{
							duration = duration+1;
						}
						else
						{
							if (duration >=15)
							{
								Red2 = 1;
								Amber2 = 1;
							}
							duration = 0;
						}
					}	
				}
				catch(Exception e)
				{
					EventLogger.WriteEntry ("Failure reading workload events table "+e.Message,EventLogEntryType.Error);
					statusCode = (int)StatusCode.ErrorReadingTable; // Failed reading page events table
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
	

			}
//----------------------------
			if (Red2 > 0)
			{
				// we need to check previous days for breaches of thresholds
				// Select all Workload events for this date 
				// total the count for all 15min periods

				selectString = "select WEHour,WEMinute, isnull(sum(WECount),0) from dbo.WorkloadEvents "
					+ "where WEDate = convert(datetime, '" + reportDateStringMinus2
					+ "',103) group by WEHour,WEMinute order by WEHour,WEMinute";

				reportingSqlConnection = new SqlConnection(reportingConnectionString);

				myDataReader = null;
				mySqlCommand = new SqlCommand(selectString, reportingSqlConnection);

				try
				{
					reportingSqlConnection.Open();
					myDataReader = mySqlCommand.ExecuteReader(CommandBehavior.CloseConnection);

					// Always call Read before accessing data.
					int hourMinute = 0;
					int duration = 0;
					int[] minCountArray = new int[1440];
					for(int i = 0;i <1440;i++)
					{
						minCountArray[i] = 0;
					}
					while (myDataReader.Read())
					{
						hour = Convert.ToInt32(myDataReader.GetSqlByte(0).ToString());
						minute = Convert.ToInt32(myDataReader.GetSqlByte(1).ToString());
						transactionCount = Convert.ToInt32(myDataReader.GetSqlInt32(2).ToString());
						hourMinute = hour*60+minute;					
						minCountArray[hourMinute] = transactionCount;
					} // end of while

					for(hourMinute = 0;hourMinute<1440;hourMinute++)
					{
						transactionCount = minCountArray[hourMinute];
						
						if (transactionCount > CBSurgeThreshold)
						{
							duration = duration+1;
						}
						else
						{
							if (duration >=15)
							{
								Red3 = 1;
								Amber2 = 1;
							}
							duration = 0;
						}
					}	
				}
				catch(Exception e)
				{
					EventLogger.WriteEntry ("Failure reading workload events table "+e.Message,EventLogEntryType.Error);
					statusCode = (int)StatusCode.ErrorReadingTable; // Failed reading page events table
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
			}
			this.file.WriteLine("");
			this.file.WriteLine("");
			if (Red3 > 0)
			{
				this.file.WriteLine("");
				this.file.WriteLine("***** RED +1 THRESHOLD HAS BEEN BREACHED *****");
				this.file.WriteLine("100% Surge level reached on 3 consecutive days");
				this.file.WriteLine("");
			}
			else
			{
				if(Red1 > 0)
				{
					this.file.WriteLine("");
					this.file.WriteLine("***** RED THRESHOLD HAS BEEN BREACHED *****");
					this.file.WriteLine("100% Surge level reached for continuous 15 min period");
					this.file.WriteLine("");
				}
				if (Amber2 > 0)
				{
					this.file.WriteLine("");
					this.file.WriteLine("***** AMBER THRESHOLD HAS BEEN BREACHED *****");
					this.file.WriteLine("75% Surge level reached on 2 successive days");
					this.file.WriteLine("");
				}
			}

			this.file.Flush();
			this.file.Close();

			
			readOK =  controller.ReadProperty("1", "FilePath", out filePathString);
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
			readOK =  controller.ReadProperty("1", "MailRecipient", out to);
			if (readOK == false || to.Length == 0)
			{
				statusCode = (int)StatusCode.ErrorReadingRecipientEmailAddress; // Error Reading recipient email address
				return statusCode;
			}
			readOK =  controller.ReadProperty("1", "Title", out subject);
			if (readOK == false || subject.Length == 0)
			{
				subject = "Daily Surge Report %YY-MM-DD%";
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
	
	}
}
