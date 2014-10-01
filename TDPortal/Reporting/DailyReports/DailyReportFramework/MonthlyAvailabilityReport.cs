///<header>
///Monthly Availability Report   (report 5)
///Created 14/11/2003
///Author JP Scott
///
///Version	Date		Who	Reason
///1		14/11/2003	PS	Created
///2		23/02/2004	PS	Availability comes from reference transactions
///3		03/06/2004	PS	show count of failed (rather than missing) reference transactions
///4		15/08/2005  PS  change ref trans checked to 20/21/22
///5		30/08/2005  PS  change ref trans checked to 30 - 34
///6        27/04/2009  PS  rewrite for new format reference injections
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
	/// Class to produce Monthly Transactional Mix report for capacity planning
 	/// analyses all recorded event tables and reports total count per day
	/// </summary>
	/// 
	
	public class MonthlyAvailabilityReport
	{
		private DateTime currentDateTime;
		private string reportStartDateString;
		private string reportEndDateString;
		private DateTime reportStartDate;
		private DateTime reportEndDate;
		private EventLog EventLogger;
		private	DailyReportController controller;
        private string RequestedDateString;
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
		private int[] dayTot = new int[32];
		private int[] monthlyDayTot = new int[32];
		private int[,,] DayHrMinCount = new int[32,24,60];  //1-31 - 0-23,0-59
		private int[,,] DayHrMinFailedCount = new int[32,24,60];  //1-31 - 0-23,0-59
		private int day;
		private int hour;

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

        private int[, ,] RefCount = new int[50, 32, 24];
        private string defaultInjectorString;
        private string backupInjectorString;
        private string travelineInjectorString;





        public MonthlyAvailabilityReport()
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

			statusCode = (int)StatusCode.Success; // success
			string reportDateString;

            RequestedDateString = reportDate.Year.ToString().Substring(2, 2)
                + "-" + reportDate.Month.ToString().PadLeft(2, '0');
           
			reportDateString = reportDate.ToShortDateString();
			currentDateTime = System.DateTime.Now;
			reportStartDate = new DateTime(reportDate.Year,reportDate.Month,1,0,0,0);
			reportEndDate = reportStartDate.AddMonths(1);
			reportEndDate = reportEndDate.AddDays(-1);
			reportStartDateString = reportStartDate.ToShortDateString();
			reportEndDateString = reportEndDate.ToShortDateString();
			int daysInMonth;
			int missingCount = 0;
			string strReportDate;

			daysInMonth = reportEndDate.Day;
			
			tmpString = new StringBuilder();

			bool readOK =  controller.ReadProperty("5", "FilePath", out filePathString);
			if (readOK == false || filePathString.Length == 0)
			{
				statusCode = (int)StatusCode.ErrorReadingFilePath; // Error Reading FilePath
				return statusCode;
			}
            filePathString = filePathString.Replace("%YY-MM%", RequestedDateString);
  
	   //.............................			



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
                for (int day = 0; day < 31; day++)
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
                        + "and ( RTE.RTEMachineName not like '" + backupInjectorString + "' "
                        + "and RTE.RTEMachineName not like '" + travelineInjectorString + "' ) " 
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
            // open report text file
            file = new StreamWriter(new FileStream(filePathString, System.IO.FileMode.Create));
            this.file.WriteLine("------------------------------------------");
            this.file.WriteLine("TRANSPORT DIRECT");
            this.file.WriteLine("DfT Monthly Availability Report");
            this.file.WriteLine("------------------------------------------");
            this.file.WriteLine("");
            this.file.WriteLine("For month starting : " + reportStartDate.ToShortDateString());
            this.file.WriteLine("");
            this.file.WriteLine("");
            this.file.WriteLine("");
            this.file.WriteLine("The times detailed below indicate when the hourly count of injected reference");
            this.file.WriteLine("transactions was different to the expected count based on the known injection");
            this.file.WriteLine("frequency. The most likely cause is Transaction Injector problems.");
            this.file.WriteLine("");
            this.file.WriteLine("Transaction Description,Code,Day,Hour,Missed Count");
            this.file.Flush();




            missingCount = 0;
            for (int i = 0; i < 50; i++)
            {
                for (day = 1; day <= reportEndDate.Day; day++)
                {
                    for (int hour = 0; hour < 24; hour++)
                    {
                        if (RefCount[i, day, hour] != NominalCount[i])
                        {
                            StringBuilder tempString = new StringBuilder();
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


			this.file.Close();

			string from = "";
			string to = "";
			string smtpServer = "";
			string subject = "";


			readOK =  controller.ReadProperty("5", "FilePath", out filePathString);
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
			readOK =  controller.ReadProperty("5", "MailRecipient", out to);
			if (readOK == false || to.Length == 0)
			{
				statusCode = (int)StatusCode.ErrorReadingRecipientEmailAddress; // Error Reading recipient email address
				return statusCode;
			}
			readOK =  controller.ReadProperty("5", "Title", out subject);
			if (readOK == false || subject.Length == 0)
			{
				subject = "Monthly Availability Report %YY-MM%";
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
