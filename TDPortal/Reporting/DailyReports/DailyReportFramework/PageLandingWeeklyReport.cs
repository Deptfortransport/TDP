///<header>
///Page Landings WEEKLY REPORT (REPORT 24)
///Created 24/11/2005
///Author JP Scott
///
///Version	Date		Who	Reason
///1		24/11/2005	PS	Created
///2        09/10/2006  PS  Add unknown Partner section
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
	
	public class LandingWeeklyReport
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
		private int day;
		private int[] dayTot = new int[8];
		private int[] dayGrandTot = new int[8];
		private int WeeklyWebPageTotal;
		private string[] PartnerArray = new string[50];  


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

        public LandingWeeklyReport()
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

			//*******************************
	


			// open report text file
			file = new StreamWriter( new FileStream(filePathString, System.IO.FileMode.Create ) );
			this.file.WriteLine("------------------------------------------");
			this.file.WriteLine("TRANSPORT DIRECT");
			this.file.WriteLine("Page Landings Weekly Report");
			this.file.WriteLine("------------------------------------------");
			this.file.WriteLine("");
			this.file.WriteLine("For week starting : " + reportStartDate.ToShortDateString());
			this.file.WriteLine("");
			this.file.WriteLine("Landing Initiated From, Page Landed On ,Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday");
			this.file.Flush();

			//*******************************
			int transactionCount = 0;

			//.............................			
			//transactionString = "Page Entry Events";
			string partner;
			string lastPartner;
			string pageLandedOn;
			string lastPageLandedOn;


			lastPartner = "";
			partner="";
			pageLandedOn="";
			lastPageLandedOn = "";

			WeeklyWebPageTotal = 0;
			

			//...............................................
		
			selectString = 
				"select LPP.LPPDescription,LPS.LPSDescription,datepart(weekday,LPE.LPEDATE),sum(LPE.LPECOUNT)"
				+" from dbo.LandingPageEvents LPE, "
				+"dbo.LandingPagePartner LPP, "
				+"dbo.LandingPageService LPS "
				+"where LPE.LPELPPID = LPP.LPPID "
				+"and LPE.LPELPSID = LPS.LPSID "
				+ "and LPE.LPEDATE >= convert(datetime, '" + reportStartDateString 
				+ "',103) and LPE.LPEDATE <= convert(datetime, '" + reportEndDateString
				+ "',103)"
				+"group by LPP.LPPDescription,LPS.LPSDescription,datepart(weekday,LPE.LPEDATE) "
				+"order by LPP.LPPDescription,LPS.LPSDescription, datepart(weekday,LPE.LPEDATE) ";

			reportingSqlConnection = new SqlConnection(reportingConnectionString);
			myDataReader = null;
			mySqlCommand = new SqlCommand(selectString, reportingSqlConnection);

			try
			{
				reportingSqlConnection.Open();
				myDataReader = mySqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
				while (myDataReader.Read())
				{
					partner  = myDataReader.GetString(0);
					pageLandedOn = myDataReader.GetString(1);
					
					if ((partner != lastPartner) || (pageLandedOn != lastPageLandedOn))
					{
						if (lastPartner.Length != 0)
						{
							// print the line on change of web page
							tmpString = new StringBuilder();
							tmpString.Append(lastPartner);
							tmpString.Append(",");
							tmpString.Append(lastPageLandedOn);
							for(int i = 1;i <= 7;i++)
							{
								tmpString.Append(",");
								tmpString.Append(dayTot[i].ToString());
							}
							this.file.WriteLine(tmpString.ToString());
							for(int i = 1;i <= 7;i++) dayTot[i] = 0;
						}
						lastPartner = partner;
						lastPageLandedOn = pageLandedOn;
					}
					
					day = Convert.ToInt32(myDataReader.GetSqlInt32(2).ToString());
					day = day-1;
					if (day == 0)
					{
						day = 7;
					}
					transactionCount = Convert.ToInt32(myDataReader.GetSqlInt32(3).ToString());
										
					dayTot[day] = dayTot[day] + transactionCount;
					dayGrandTot[day] = dayGrandTot[day] + transactionCount;
				} // end of while
				// flush the last web page
				tmpString = new StringBuilder();
				tmpString.Append(partner);
				tmpString.Append(",");
				tmpString.Append(pageLandedOn);
				for(int i = 1;i <= 7;i++)
				{
					tmpString.Append(",");
					tmpString.Append(dayTot[i].ToString());
				}
				this.file.WriteLine(tmpString.ToString());

				this.file.WriteLine(" ");
				
				// write grand tots
				tmpString = new StringBuilder();
				tmpString.Append("Total Daily Web Pages Hits ,");
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
				EventLogger.WriteEntry ("Failure reading landing events table "+e.Message,EventLogEntryType.Error);
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
			this.file.WriteLine("Summary Totals for Week ");
			this.file.WriteLine(" ");
			this.file.WriteLine("Total External Landings         : "+WeeklyWebPageTotal.ToString());
			this.file.WriteLine(" ");
			this.file.WriteLine("------------------------------------------");
						this.file.WriteLine("" );
			this.file.Flush();

			
//*************************************************************************************************************************
// Unknown Partner Page Landings
			int UnknownPartnerTot = 0;
			int UnknownPartner = 0;
			lastPartner = "";
			partner="";
			pageLandedOn="";
			lastPageLandedOn = "";

			WeeklyWebPageTotal = 0;

			for (int ii=0;ii<50;ii++) 
			{
				PartnerArray[ii] = null;
			}

			

			//...............................................
		
			selectString = 
			"Select top 50 UPLPartner,SUM(UPLCount)from dbo.UnknownPartnerLandingEvents "
			+ "WHERE  UPLDATE >= convert(datetime, '" + reportStartDateString 
			+ "',103) and UPLDATE <= convert(datetime, '" + reportEndDateString
			+ "',103) and UPLPartner > ''"
			+ " group by UPLPartner "
			+ "order by SUM(UPLCount) desc,UPLPartner";

			reportingSqlConnection = new SqlConnection(reportingConnectionString);
			myDataReader = null;
			mySqlCommand = new SqlCommand(selectString, reportingSqlConnection);

			try
			{
				reportingSqlConnection.Open();
				myDataReader = mySqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
				
				while (myDataReader.Read())
				{
					PartnerArray[UnknownPartnerTot++]  = myDataReader.GetString(0);
				} // end of while
				// flush the last web page

				myDataReader.Close();

			}
			catch(Exception e)
			{
				EventLogger.WriteEntry ("Failure reading UnknownPartnerLandingEvents table "+e.Message,EventLogEntryType.Error);
				statusCode = (int)StatusCode.ErrorReadingTable; // Failed to reading page events table
			}
			finally
			{
				// Always call Close when done reading.
				if (myDataReader != null)
					myDataReader.Close();
			}

            if (UnknownPartnerTot > 0)
            {
                this.file.WriteLine("Unknown Partner Landing Events For week starting : " + reportStartDate.ToShortDateString());
                this.file.WriteLine("");
                this.file.WriteLine("Unknown Partner Code,  ,Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday");

            }

            // Now go through eack unknown partner make sql query and print weekly line

			for (UnknownPartner =0 ; UnknownPartner < UnknownPartnerTot ; UnknownPartner++)
			{

				for(int i = 1;i <= 7;i++) {	dayTot[i] = 0;}



				selectString = 
					"Select datepart(weekday,UPLDATE),UPLCount from dbo.UnknownPartnerLandingEvents "
					+ "WHERE  UPLDATE >= convert(datetime, '" + reportStartDateString 
					+ "',103) and UPLDATE <= convert(datetime, '" + reportEndDateString
					+ "',103)"
					+ " and UPLPartner = '"+ PartnerArray[UnknownPartner] +"' "
					+ "order by datepart(weekday,UPLDATE)";

				myDataReader = null;
				mySqlCommand = new SqlCommand(selectString, reportingSqlConnection);

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
						dayTot[day] = dayTot[day] + transactionCount;
						dayGrandTot[day] = dayGrandTot[day] + transactionCount;
					} // end of while
					// flush the last web page
					tmpString = new StringBuilder();
					tmpString.Append(PartnerArray[UnknownPartner]);
					tmpString.Append(",");
					for(int i = 1;i <= 7;i++)
					{
						tmpString.Append(",");
						tmpString.Append(dayTot[i].ToString());
					}
					this.file.WriteLine(tmpString.ToString());

					this.file.Flush();
					// flush the last web page

					myDataReader.Close();

				}
				catch(Exception e)
				{
					EventLogger.WriteEntry ("Failure reading UnknownPartnerLandingEvents table "+e.Message,EventLogEntryType.Error);
					statusCode = (int)StatusCode.ErrorReadingTable; // Failed to reading page events table
				}
				finally
				{
					// Always call Close when done reading.
					if (myDataReader != null)
						myDataReader.Close();
				}

			}


			//.............................		
			this.file.WriteLine(" ");

			//.............................			
		

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
				subject = "DFT Weekly Report %YY-MM-DD%";
			}
            subject = subject.Replace("%YY-MM-DD%", RequestedDateString); 
            string bodyText = "Report is attached....";


			SendEmail mailObject = new SendEmail();
			int sendFileErrorCode = mailObject.SendFile(from, to, subject, bodyText, 
				filePathString, smtpServer);
			if (sendFileErrorCode != 0)
				statusCode = sendFileErrorCode;

			return statusCode;

		}
	



	}
}
