///<header>
///Partner Partner WEEKLY REPORT (REPORT 19)
///Created 06/09/2005
///Author JP Scott
///
///Version	Date		Who	Reason
///1		06/09/2005	PS	Created
///1		02/11/2005	PS	V1.0.26 add user sessions count
///3
///
/// 1.0     19/06/07    PS  Now controlled BY PVCS versions
/// 1.1     06/03/08    PS  Convert to .net 2
/// 1.2     06/03/08    PS  Only Produce Partner reports whose number is less than 100
/// 1.3     13/05/08    PS  File Path changes
/// 1.4     14/05/08    PS  Add new/repeat visitor counts
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
	
	public class PartnerWeeklyReport
	{


        private int partnerIndex; 
        private int maxPartnerIndex;
        private string requestedPartnerID;
		private string [] PartnerID = new string[20];
		private string [] HostName = new string[20];
		private string [] PartnerName = new string[20];
		private string [] Channel = new string[20];


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
		private string transactionString;
		private string selectString;
		private System.IO.StreamWriter file;
		private string filePathString;
        private string filePathStringDefault;
        private string fileDriveString;
		private int statusCode;
		private DateTime thisDay;
		private string dayOfWeek;
		private int[] dayTot = new int[8];
		private int[] dayGrandTot = new int[8];
		private int WeeklyTotal;
		private int WeeklyWebPageTotal;
		private int WeeklySessionTotal;
		private int WeeklyWorkLoadTotal;
        private int WeeklyNewVisitorTotal;
        private int WeeklyOldVisitorTotal;

		private int[] monthlyDayTot = new int[32];
		private int[,,] DayHrMinCount = new int[8,24,60];  //1-31 - 0-23,0-59
		private int[,,] DayHrMinSurge = new int[8,24,60];  //1-31 - 0-23,0-59
		private int[,,] DayHrMinSuccess = new int[8,24,60];  //1-31 - 0-23,0-59

		

		private int[,] MapCmdCount = new int[6,5];
		private int[,] MapCmdTime  = new int[6,5];
		private string[] mapCommand = new string[5];
		private string[] mapDisplay = new string[6];
		private DateTime[] TestStarted = new DateTime[250]; 
		private DateTime[] TestCompleted = new DateTime[250]; 
		

		private int[] RefType         = new int[20];
		private string[] RefTypeDesc  = new string[20];
		private int[] ResponseTarget  = new int[20];

        public PartnerWeeklyReport()
        {
            EventLogger = new EventLog("Application");
            EventLogger.Source = "TD.Reporting";
            controller = new DailyReportController();

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

            bool addDate = false;
			bool readOK =  controller.ReadProperty(reportNumber.ToString(), "FilePath", out filePathString);
			if (readOK == false || filePathString.Length == 0)
			{
				statusCode = (int)StatusCode.ErrorReadingFilePath; // Error Reading FilePath
				return statusCode;
			}
            fileDriveString = filePathString.Substring(0,3);
            if (filePathString.IndexOf("%YY-MM-DD%") > 0) addDate = true;
            
			readOK =  controller.ReadProperty(reportNumber.ToString(), "PartnerID", out requestedPartnerID);
			if (readOK == false || filePathString.Length == 0)
			{
				statusCode = (int)StatusCode.ErrorReadingTable; // Error Reading PartnerID
				return statusCode;
			}


			reportingSqlConnection = new SqlConnection(reportingConnectionString);

			myDataReader = null;
			mySqlCommand = new SqlCommand(selectString, reportingSqlConnection);

			//*******************************
            // if partner id is -1 then loop through all partners in partner table

            if (requestedPartnerID == "-1")
            {
                selectString = "select PartnerId,HostName,PartnerName,Channel from Partner "
					+ "where PartnerId < '100' and Printable = 'Y'";
            }
            else
            {
                selectString = "select PartnerId,HostName,PartnerName,Channel from Partner "
                    + "where PartnerId = '"
                    + requestedPartnerID
                    + "' and Printable = 'Y'";
            }

			myDataReader = null;
			mySqlCommand = new SqlCommand(selectString, reportingSqlConnection);

			try
			{
				reportingSqlConnection.Open();
				myDataReader = mySqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
                partnerIndex = 0;

				while (myDataReader.Read())
				{
                    partnerIndex += 1;

                    PartnerID[partnerIndex] = myDataReader.GetSqlInt32(0).ToString();
                    HostName[partnerIndex]  = myDataReader.GetString(1);
                    PartnerName[partnerIndex] = myDataReader.GetString(2);
                    Channel[partnerIndex]   = myDataReader.GetString(3);

				}
                maxPartnerIndex = partnerIndex;
			}			
			catch(Exception e)
			{
				EventLogger.WriteEntry ("Failure reading partner table "+e.Message,EventLogEntryType.Error);
				statusCode = (int)StatusCode.ErrorReadingTable; // Failed to reading page events table
			}
			finally
			{
				// Always call Close when done reading.
				if (myDataReader != null)
					myDataReader.Close();
			}


            for (partnerIndex = 1; partnerIndex <= maxPartnerIndex;partnerIndex++ )
            {

                if (requestedPartnerID == "-1")
                {
                    if (addDate)
                    {
                        filePathString = fileDriveString + "Weekly" + PartnerName[partnerIndex] + "Report " + RequestedDateString + ".csv";
                        filePathStringDefault = fileDriveString + "Weekly" + PartnerName[partnerIndex] + "Report.csv";
                    }
                    else
                    {
                        filePathString = fileDriveString + "Weekly" + PartnerName[partnerIndex] + "Report.csv";
                        filePathStringDefault = fileDriveString + "Weekly" + PartnerName[partnerIndex] + "Report.csv";
                    }
                }
                else
                {

                    readOK = controller.ReadProperty(reportNumber.ToString(), "FilePath", out filePathString);
                    if (readOK == false || filePathString.Length == 0)
                    {
                        if (addDate)
                        {
                            filePathString = fileDriveString + "Weekly" + PartnerName[partnerIndex] + "Report " + RequestedDateString + ".csv";
                            filePathStringDefault = fileDriveString + "Weekly" + PartnerName[partnerIndex] + "Report.csv";
                        }
                        else
                        {
                            filePathString = fileDriveString + "Weekly" + PartnerName[partnerIndex] + "Report.csv";
                            filePathStringDefault = fileDriveString + "Weekly" + PartnerName[partnerIndex] + "Report.csv";
                        }
                    }
                    else
                    {
                        filePathStringDefault = filePathString.Replace(" %YY-MM-DD%", "");
                        filePathString = filePathString.Replace("%YY-MM-DD%", RequestedDateString);
                    }
                }

 
              // open report text file
            file = new StreamWriter(new FileStream(filePathString, System.IO.FileMode.Create));
			this.file.WriteLine("------------------------------------------");
			this.file.WriteLine("TRANSPORT DIRECT");
			this.file.WriteLine(PartnerName[partnerIndex]+" Weekly Report");
			this.file.WriteLine("------------------------------------------");
			this.file.WriteLine("");
			this.file.WriteLine("For week starting : " + reportStartDate.ToShortDateString());
			this.file.WriteLine("");
			this.file.WriteLine("Web Page,Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday");
			this.file.Flush();
			reportingSqlConnection = new SqlConnection(reportingConnectionString);

			myDataReader = null;
			mySqlCommand = new SqlCommand(selectString, reportingSqlConnection);



			//*******************************
			int transactionCount = 0;

			//.............................			
			transactionString = "Page Entry Events";
			selectString = 
				"select PT.PETDescription, PE.PEEDate, isnull(sum(PE.PEECount),0)"
				+ "from dbo.PageEntryEvents PE, dbo.PageEntryType PT where PT.PETID = PE.PEEPETID"
				+ "  and PE.PEEDate >= convert(datetime, '" + reportStartDateString
				+ "',103) and PE.PEEDate <= convert(datetime, '" + reportEndDateString
				+ "',103) and PE.PEEPartnerID like '%"
				+ PartnerID[partnerIndex] 
				+ "' group by PT.PETDescription,PE.PEEDate order by PT.PETDescription,PE.PEEDate";

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

			
			//...............................................


			transactionString = "Partner User Sessions";

			selectString = "select SEDate, isnull(sum(SECount),0)from dbo.SessionEvents "
				+ "where SEDate >= convert(datetime, '" + reportStartDateString 
				+ "',103) and SEDate <= convert(datetime, '" + reportEndDateString
				+ "',103) and SEPartnerID = "
				+ PartnerID[partnerIndex]
				+ " group by SEDate order by SEDate";
			DailyEventBreakDown();
			WeeklySessionTotal = WeeklyTotal;

//jps
            transactionString = "Partner New Visitors ";
            selectString = "select RVEDate, isnull(sum(RVECount),0)from dbo.RepeatVisitorEvents "
                + "where RVEDate >= convert(datetime, '" + reportStartDateString
                + "',103) and RVEDate <= convert(datetime, '" + reportEndDateString
                + "',103) and RVERVTID = 2 and RVEPartnerID = "
				+ PartnerID[partnerIndex]
                + " group by RVEDate order by RVEDate";
            DailyEventBreakDown();
            WeeklyNewVisitorTotal = WeeklyTotal;

         
            transactionString = "Partner Repeat Visitors ";
            selectString = "select RVEDate, isnull(sum(RVECount),0)from dbo.RepeatVisitorEvents "
                + "where RVEDate >= convert(datetime, '" + reportStartDateString
                + "',103) and RVEDate <= convert(datetime, '" + reportEndDateString
                + "',103) and RVERVTID = 3 and RVEPartnerID = "
                + PartnerID[partnerIndex]
                + " group by RVEDate order by RVEDate";
            DailyEventBreakDown();
            WeeklyOldVisitorTotal = WeeklyTotal;
//JPS

			transactionString = "Workload  (Total Load)";

			transactionString = "Workload  (Total Load)";
			selectString = "select WEDate, isnull(sum(WECount),0)from dbo.WorkloadEvents "
				+ "where WEDate >= convert(datetime, '" + reportStartDateString 
				+ "',103) and WEDate <= convert(datetime, '" + reportEndDateString
				+ "',103) and PartnerID = '"
				+ PartnerID[partnerIndex]
				+"' group by WEDate order by WEDate";
			DailyEventBreakDown();
			WeeklyWorkLoadTotal = WeeklyTotal;



		//.............................		
			this.file.WriteLine(" ");

			//.............................			
			this.file.WriteLine(" ");
			this.file.WriteLine(" ");
			this.file.WriteLine("Summary Totals for Week ");
			this.file.WriteLine(" ");
			this.file.WriteLine("Total Web Pages visited          : "+WeeklyWebPageTotal.ToString());
			this.file.WriteLine("Total Partner User Sessions      : "+WeeklySessionTotal.ToString());
			this.file.WriteLine("Total Partner New Visitors       : "+WeeklyNewVisitorTotal.ToString());
			this.file.WriteLine("Total Partner Repeat Visitors    : "+WeeklyOldVisitorTotal.ToString());
            this.file.WriteLine("Total WorkLoad Hits              : "+WeeklyWorkLoadTotal.ToString());
			this.file.WriteLine(" ");



			
//*************************************************************************************************************************


//*************************************************************************************************************************
			
			this.file.Close();

            try
            {
                if (!filePathString.Equals(filePathStringDefault))
                {
                    File.Copy(filePathString, filePathStringDefault, true);
                }
			}
			catch(Exception e)
			{
				EventLogger.WriteEntry ("Failure copying file "+e.Message,EventLogEntryType.Error);
				statusCode = (int)StatusCode.ErrorCopyingFile; // Failed to reading page events table
			}
			string from = "";
			string smtpServer = "";
			string to = "";
			string subject = "";


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
                subject = "Partner Weekly Report %YY-MM-DD%";
			}
            subject = PartnerName[partnerIndex] +" "+ subject.Replace("%YY-MM-DD%", RequestedDateString);

			string bodyText =  "Report is attached....";


			SendEmail mailObject = new SendEmail();
			int sendFileErrorCode = mailObject.SendFile(from, to, subject, bodyText, 
				filePathString, smtpServer);
			if (sendFileErrorCode != 0)
				statusCode = sendFileErrorCode;

			


		WeeklyTotal = 0;
		WeeklyWebPageTotal = 0;
		WeeklySessionTotal = 0;
		WeeklyWorkLoadTotal = 0;
        WeeklyNewVisitorTotal = 0;
        WeeklyOldVisitorTotal = 0;

		for(int i = 1;i <= 7;i++) dayTot[i] = 0;
		for(int i = 1;i <= 7;i++) dayGrandTot[i] = 0;



		}




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
