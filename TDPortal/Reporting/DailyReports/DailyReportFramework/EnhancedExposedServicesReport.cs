///<header>
///ENHANCED EXPOSED SERVICES WEEKLY REPORT (REPORT 25)
///Created 13/03/2006
///Author JP Scott
///
///Version	Date		Who	Reason
///1		 13/03/2006	PS	Created
///
///
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
	
	public class EESReport
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
        private string filePathStringDefault;
        private int statusCode;
		private DateTime thisDay;
		private string dayOfWeek;
		private int[] dayTot = new int[8];
		private int[] partnerGrandTot = new int[8];
		private int[] dayGrandTot = new int[8];
		private int WeeklyPartnerTotal;
		private int[] monthlyDayTot = new int[32];
		private int[,,] DayHrMinCount = new int[8,24,60];  //1-31 - 0-23,0-59
		private int[,] MapCmdCount = new int[6,5];
		private int[,] MapCmdTime  = new int[6,5];
		private string[] mapCommand = new string[5];
		private string[] mapDisplay = new string[6];
		private DateTime[] TestStarted = new DateTime[250]; 
		private DateTime[] TestCompleted = new DateTime[250]; 

		private string transactionString;
		private int WeeklyExpPageTotal;
		private int WeeklyRTTIPageTotal;
		private int WeeklyTotal;
		private int day;

		private int[] RefType         = new int[20];
		private string[] RefTypeDesc  = new string[20];
		private int[] ResponseTarget  = new int[20];
		
		public EESReport()
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

            RequestedDateString = reportDate.Year.ToString().Substring(2, 2)
                + "-" + reportDate.Month.ToString().PadLeft(2, '0')
                + "-" + reportDate.Day.ToString().PadLeft(2, '0');
           
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
            filePathStringDefault = filePathString.Replace(" %YY-MM-DD%", "");
            filePathString = filePathString.Replace("%YY-MM-DD%", RequestedDateString);
     
			// open report text file
			file = new StreamWriter( new FileStream(filePathString, System.IO.FileMode.Create ) );
			this.file.WriteLine("------------------------------------------");
			this.file.WriteLine("TRANSPORT DIRECT");
			this.file.WriteLine("Enhanced Exposed Services Weekly Report");
			this.file.WriteLine("------------------------------------------");
			this.file.WriteLine("");
			this.file.WriteLine("For week starting : " + reportStartDate.ToShortDateString());
			this.file.WriteLine(" ");
			this.file.WriteLine("Partner");
			this.file.WriteLine("Service Type,Operation Type,Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday");
			this.file.Flush();

			reportingSqlConnection = new SqlConnection(reportingConnectionString);

//*******************************
			int transactionCount = 0;
			selectString = 
				"select P.PartnerName, ES.EESTDescription,"
				+ "EO.EEOTDescription,E.EESEDate,   isnull(sum(E.EESECount),0)"
				+ "from dbo.EnhancedExposedServiceEvents E, "
				+ "dbo.EnhancedExposedServicesType ES, "
				+ "dbo.EnhancedExposedOperationType EO, "
				+ "Partner P "
				+ "where ES.EESTID = E.EESEEESTID "
				+ "and EO.EEOTID  = E.EESEEEOTID "
				+ "and P.PartnerId = E.EESEPartnerId "
				+ "  and E.EESEDate >= convert(datetime, '" + reportStartDateString
				+ "',103) and E.EESEDate < convert(datetime, '" + reportEndDatePlus1String 
				+ "',103) "
				+ "Group by P.PartnerName,ES.EESTDescription, EO.EEOTDescription,E.EESEDate "
                + "order by P.PartnerName,ES.EESTDescription, EO.EEOTDescription,E.EESEDate ";


			myDataReader = null;
			mySqlCommand = new SqlCommand(selectString, reportingSqlConnection);
			string Partner;
			string lastPartner;
			string STDescription;
			string lastSTDescription;
			string OTDescription;
			string lastOTDescription;
			bool partnerPrintFlag = true;

			Partner = "";
			STDescription = "";
			OTDescription = "";
			lastPartner = Partner;
			lastSTDescription = STDescription;
			lastOTDescription = OTDescription;

			
			WeeklyPartnerTotal = 0;
			
			try
			{
				reportingSqlConnection.Open();
				myDataReader = mySqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
				while (myDataReader.Read())
				{
					Partner  = myDataReader.GetString(0);
					STDescription = myDataReader.GetString(1);
					OTDescription = myDataReader.GetString(2);
					thisDay = myDataReader.GetDateTime(3);
					transactionCount = Convert.ToInt32(myDataReader.GetSqlInt32(4).ToString());
					dayOfWeek = thisDay.DayOfWeek.ToString();

					//accumulate day totals and print on first change
					if (lastPartner.Length == 0) // first partner
						lastPartner = Partner;
					if (lastOTDescription.Length == 0) // first OTDescription
						lastOTDescription = OTDescription;
					if (lastSTDescription.Length == 0) // first STDescription
						lastSTDescription = STDescription;

					
					if (Partner != lastPartner || 
						STDescription != lastSTDescription || 
						OTDescription != lastOTDescription)
					{ // A change has happened so print totals line

						if (partnerPrintFlag)
						{
							this.file.WriteLine(" ");
							this.file.WriteLine(" ");
							this.file.WriteLine(lastPartner);
							partnerPrintFlag = false;
						}
						
						tmpString = new StringBuilder();
						tmpString.Append(lastSTDescription);
						tmpString.Append(",");
						tmpString.Append(lastOTDescription);
						for(int i = 1;i <= 7;i++)
						{
							tmpString.Append(",");
							tmpString.Append(dayTot[i].ToString());
						}
						this.file.WriteLine(tmpString.ToString());
						//Change last values to current and start new totals
						if (lastPartner != Partner)
						{
							partnerPrintFlag = true;
							WeeklyPartnerTotal = 0;
							this.file.WriteLine(" ");
							tmpString = new StringBuilder();
							tmpString.Append("Total Daily  Partner Events, ");

							for(int i = 1;i <= 7;i++)
							{
								tmpString.Append(",");
								tmpString.Append(partnerGrandTot[i].ToString());
								WeeklyPartnerTotal = WeeklyPartnerTotal + partnerGrandTot[i];
								partnerGrandTot[i] = 0;
							}
							this.file.WriteLine(tmpString.ToString());
							tmpString = new StringBuilder();
							tmpString.Append("Total Weekly Partner Events = ");
							tmpString.Append(WeeklyPartnerTotal.ToString());
							this.file.WriteLine(tmpString.ToString());
							
						
						}
						lastPartner = Partner;
						lastSTDescription = STDescription;
						lastOTDescription = OTDescription;
						for(int i = 1;i <= 7;i++) dayTot[i] = 0;

					}

				//Accumulate totals for current variables				
				switch (dayOfWeek)
				{
					case "Monday":
						dayTot[1] = dayTot[1] + transactionCount;
						dayGrandTot[1] = dayGrandTot[1] + transactionCount;
						partnerGrandTot[1] = partnerGrandTot[1] + transactionCount;
						break;
					case "Tuesday":
						dayTot[2] = dayTot[2] + transactionCount;
						dayGrandTot[2] = dayGrandTot[2] + transactionCount;
						partnerGrandTot[2] = partnerGrandTot[2] + transactionCount;
						break;
					case "Wednesday":
						dayTot[3] = dayTot[3] + transactionCount;
						dayGrandTot[3] = dayGrandTot[3] + transactionCount;
						partnerGrandTot[3] = partnerGrandTot[3] + transactionCount;
						break;
					case "Thursday":
						dayTot[4] = dayTot[4] + transactionCount;
						dayGrandTot[4] = dayGrandTot[4] + transactionCount;
						partnerGrandTot[4] = partnerGrandTot[4] + transactionCount;
						break;
					case "Friday":
						dayTot[5] = dayTot[5] + transactionCount;
						dayGrandTot[5] = dayGrandTot[5] + transactionCount;
						partnerGrandTot[5] = partnerGrandTot[5] + transactionCount;
						break;
					case "Saturday":
						dayTot[6] = dayTot[6] + transactionCount;
						dayGrandTot[6] = dayGrandTot[6] + transactionCount;
						partnerGrandTot[6] = partnerGrandTot[6] + transactionCount;
						break;
					case "Sunday":
						dayTot[7] = dayTot[7] + transactionCount;
						dayGrandTot[7] = dayGrandTot[7] + transactionCount;
						partnerGrandTot[7] = partnerGrandTot[7] + transactionCount;
						break;
					}
				} // end of while
				// flush the last data
				if (partnerPrintFlag)
				{
					this.file.WriteLine(lastPartner);
				}
				tmpString = new StringBuilder();
				tmpString.Append(lastSTDescription);
				tmpString.Append(",");
				tmpString.Append(lastOTDescription);
				for(int i = 1;i <= 7;i++)
				{
					tmpString.Append(",");
					tmpString.Append(dayTot[i].ToString());
				}
				this.file.WriteLine(tmpString.ToString());

				

				
				// write grand tots
				tmpString = new StringBuilder();
				this.file.WriteLine(" ");
				tmpString.Append("Total Daily  Partner Events, ");
				WeeklyPartnerTotal = 0;
				for(int i = 1;i <= 7;i++)
				{
					tmpString.Append(",");
					tmpString.Append(partnerGrandTot[i].ToString());
					WeeklyPartnerTotal = WeeklyPartnerTotal + partnerGrandTot[i];
				}
				this.file.WriteLine(tmpString.ToString());
				tmpString = new StringBuilder();
				tmpString.Append("Total Weekly Partner Events = ");
				tmpString.Append(WeeklyPartnerTotal.ToString());
				this.file.WriteLine(tmpString.ToString());
							

				this.file.WriteLine(" ");
				WeeklyPartnerTotal = 0;
				tmpString = new StringBuilder();
				tmpString.Append("Total Daily Events, ");
				for(int i = 1;i <= 7;i++)
				{
					tmpString.Append(",");
					tmpString.Append(dayGrandTot[i].ToString());
					WeeklyPartnerTotal = WeeklyPartnerTotal + dayGrandTot[i];
				}
				this.file.WriteLine(tmpString.ToString());
				tmpString = new StringBuilder();
				tmpString.Append("Total Weekly Events = ");
				tmpString.Append(WeeklyPartnerTotal.ToString());
				this.file.WriteLine(tmpString.ToString());
							

				this.file.Flush();
				myDataReader.Close();

			}
			catch(Exception e)
			{
				EventLogger.WriteEntry ("Failure reading EnhancedExposedService events table "+e.Message,EventLogEntryType.Error);
				statusCode = (int)StatusCode.ErrorReadingTable; // Failed to reading ees events table
			}
			finally
			{
				// Always call Close when done reading.
				if (myDataReader != null)
					myDataReader.Close();
			}


			//.............................	Below Sections Migrated from Mobile Report	
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

			
			//...............................................
			//summary figures

			this.file.WriteLine(" ");
			this.file.WriteLine(" ");
			this.file.WriteLine("Summary Totals for Week ");
			this.file.WriteLine(" ");
			this.file.WriteLine("Total Exposed Services Called  : "+WeeklyExpPageTotal.ToString());
			this.file.WriteLine("Total RTTI Events              : "+WeeklyRTTIPageTotal.ToString());
			this.file.WriteLine(" ");

			//.............................		
			for(int i = 1;i <= 7;i++)
			{
				dayGrandTot[i] = 0;
				dayTot[i] = 0;
			}
			this.file.WriteLine(" ");
			
			
			// ***************************************************************************************************
			

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
				subject = "Enhanced Exposed Services Report %YY-MM-DD%";
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

	}
}
