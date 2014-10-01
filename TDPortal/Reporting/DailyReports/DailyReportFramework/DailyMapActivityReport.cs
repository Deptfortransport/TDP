///<header>
///DailyMapActivityReport.cs
///Created 01/12/2003
///Author JP Scott
///
///Version	Date		Who	Reason
///1		01/12/2003	PS	Created
///2        21/09/2004  PS Re-write Now based on workload - also reports thresholds
///3		04/10/2004  PS increase testperiod date array

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
	/// Class to produce Monthly Web Page And Map UsageReport
	/// analyses all Page entry events and map events and reports counts for month
	/// </summary>
	/// 
	
public class DailyMapActivityReport
{
	private DateTime currentDateTime;
	private	string reportDateString;
	private	string reportDateStringMinus1;
	private	string reportDateStringMinus2;
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
	private int[] dayTot = new int[32];
	private int[] monthlyDayTot = new int[32];
	private int[,,] DayHrMinCount = new int[32,24,60];  //1-31 - 0-23,0-59
	private DateTime[] TestStarted = new DateTime[250]; 
	private DateTime[] TestCompleted = new DateTime[250]; 
	private int Red1;
	private int Red2;
	private int Red3;
	private int Amber1;
	private int Amber2;
	float percentageMaps;
	// private int testCount;
	// private DateTime SDate;
	// private DateTime EDate;
	// private int SHour,EHour ;
	// private long ScompareString,EcompareString;
	// private StringBuilder excludeString;
	
		
	public DailyMapActivityReport()
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
reportDateString = reportDate.ToShortDateString();
currentDateTime = System.DateTime.Now;
reportDateStringMinus1 = reportDate.AddDays(-1).ToShortDateString();
reportDateStringMinus2 = reportDate.AddDays(-2).ToShortDateString();
RequestedDateString = reportDate.Year.ToString().Substring(2, 2)
    + "-" + reportDate.Month.ToString().PadLeft(2, '0')
    + "-" + reportDate.Day.ToString().PadLeft(2, '0');
int transactionCount = 0;
//			int totalWebPageCount = 0;
int totalMapCount = 0;
//int totalAdjustedMapCount = 0;
int totalWorkload = 0;
//int totalAdjustedWorkload = 0;
	Red1=0;
	Red2=0;
	Red3=0;
	Amber1=0;
	Amber2=0;

			
tmpString = new StringBuilder();

bool readOK =  controller.ReadProperty("2", "FilePath", out filePathString);
if (readOK == false || filePathString.Length == 0)
{
statusCode = (int)StatusCode.ErrorReadingFilePath; // Error Reading FilePath
return statusCode;
}
filePathString = filePathString.Replace("%YY-MM-DD%", RequestedDateString);
    
			// open report text file
file = new StreamWriter( new FileStream(filePathString, System.IO.FileMode.Create ) );
this.file.WriteLine("------------------------------------------");
this.file.WriteLine("TRANSPORT DIRECT");
this.file.WriteLine("Daily Map Activity Report");
this.file.WriteLine("------------------------------------------");
this.file.WriteLine("");
this.file.WriteLine("For Date : " + reportDateString);
this.file.WriteLine("");

this.file.Flush();

	//*******************************

reportingSqlConnection = new SqlConnection(reportingConnectionString);
transactionCount = 0;
percentageMaps = 0;

selectString = "select isnull(sum(WECount),0)from dbo.WorkloadEvents "
+ "where WEDate = convert(datetime, '" + reportDateString 
+ "',103)";
myDataReader = null;
mySqlCommand = new SqlCommand(selectString, reportingSqlConnection);
transactionCount = 0;
	
try
{
reportingSqlConnection.Open();
myDataReader = mySqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
while (myDataReader.Read())
{
transactionCount = transactionCount + Convert.ToInt32(myDataReader.GetSqlInt32(0).ToString());
} // end of while
totalWorkload = totalWorkload +transactionCount;
myDataReader.Close();
}
catch(Exception e)
{
EventLogger.WriteEntry ("Error reading workload events "+e.Message,EventLogEntryType.Error);
statusCode = (int)StatusCode.ErrorReadingTable; // Failed reading table
}
finally
{
				// Always call Close when done reading.
if (myDataReader != null)
myDataReader.Close();
}
			
selectString = "select isnull(sum(MECount),0)from dbo.MapEvents "
+ "where MEDate = convert(datetime, '" + reportDateString 
+ "',103)";

myDataReader = null;
mySqlCommand = new SqlCommand(selectString, reportingSqlConnection);
transactionCount = 0;
	
try
{
reportingSqlConnection.Open();
myDataReader = mySqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
while (myDataReader.Read())
{
transactionCount = transactionCount + Convert.ToInt32(myDataReader.GetSqlInt32(0).ToString());
} // end of while
totalMapCount = totalMapCount +transactionCount;
myDataReader.Close();
}
catch(Exception e)
{
EventLogger.WriteEntry ("Error reading Map Events "+e.Message,EventLogEntryType.Error);
statusCode = (int)StatusCode.ErrorReadingTable; // Failed reading table
}
finally
{
				// Always call Close when done reading.
if (myDataReader != null)
myDataReader.Close();
}
			
this.file.WriteLine(" ");
this.file.WriteLine(" ");

this.file.WriteLine("Web Pages ");
this.file.WriteLine("Web Page Hits (WorkloadEvents)              "+totalWorkload.ToString());
//this.file.WriteLine("Web Page Hits (WorkloadEvents)(Excl. Tests) ,"+totalAdjustedWorkload.ToString());
this.file.WriteLine(" ");
this.file.WriteLine("Map Usage  (displays /pans /zooms /overlays etc.)");
this.file.WriteLine("Map Events                                  "+totalMapCount.ToString());
//this.file.WriteLine("Map Events (Excl. Tests)                    ,"+totalAdjustedMapCount.ToString());
this.file.WriteLine(" ");
percentageMaps = (float) (Math.Floor((100000.00 * ( (float) totalMapCount / (float) totalWorkload)))/1000);
this.file.WriteLine("Percentage map page activity                " + percentageMaps.ToString());
//percentageAdjustedMaps = (float) (Math.Floor((100000.00 * ( (float) totalAdjustedMapCount / (float) totalAdjustedWorkload)))/1000);
//this.file.WriteLine("Percentage map page activity (Excl. Tests)   ," + percentageAdjustedMaps.ToString());
	
if (percentageMaps > 7.5)
{
	
	Amber1 = 1;
	if (percentageMaps > 10)
	{
		Red1 = 1;		
		this.file.WriteLine("The ratio of Map Pages to Web Pages has exceeded the maximum ratio (1:10) ");
	}
}
else
{																												
this.file.WriteLine("The ratio of Map Pages to Web Pages has NOT exceeded the maximum ratio (1:10)");
}
		
this.file.Flush();
		
if (Amber1 > 0 || Red1 > 0)
{
	
	
// check yesterdays figures
	//*******************************

	reportingSqlConnection = new SqlConnection(reportingConnectionString);
	transactionCount = 0;
	percentageMaps = 0;


	selectString = "select isnull(sum(WECount),0)from dbo.WorkloadEvents "
		+ "where WEDate = convert(datetime, '" + reportDateStringMinus1 
		+ "',103)";
	myDataReader = null;
	mySqlCommand = new SqlCommand(selectString, reportingSqlConnection);
	transactionCount = 0;
	totalWorkload = 0;
	try
	{
		reportingSqlConnection.Open();
		myDataReader = mySqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
		while (myDataReader.Read())
		{
			transactionCount = transactionCount + Convert.ToInt32(myDataReader.GetSqlInt32(0).ToString());
		} // end of while
		totalWorkload = totalWorkload +transactionCount;
		myDataReader.Close();
	}
	catch(Exception e)
	{
		EventLogger.WriteEntry ("Error reading workload events "+e.Message,EventLogEntryType.Error);
		statusCode = (int)StatusCode.ErrorReadingTable; // Failed reading table
	}
	finally
	{
		// Always call Close when done reading.
		if (myDataReader != null)
			myDataReader.Close();
	}
			
	selectString = "select isnull(sum(MECount),0)from dbo.MapEvents "
		+ "where MEDate = convert(datetime, '" + reportDateStringMinus1 
		+ "',103)";

	myDataReader = null;
	mySqlCommand = new SqlCommand(selectString, reportingSqlConnection);
	transactionCount = 0;
	totalMapCount = 0;
	try
	{
		reportingSqlConnection.Open();
		myDataReader = mySqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
		while (myDataReader.Read())
		{
			transactionCount = transactionCount + Convert.ToInt32(myDataReader.GetSqlInt32(0).ToString());
		} // end of while
		totalMapCount = totalMapCount +transactionCount;
		myDataReader.Close();
	}
	catch(Exception e)
	{
		EventLogger.WriteEntry ("Error reading Map Events "+e.Message,EventLogEntryType.Error);
		statusCode = (int)StatusCode.ErrorReadingTable; // Failed reading table
	}
	finally
	{
		// Always call Close when done reading.
		if (myDataReader != null)
			myDataReader.Close();
	}

	percentageMaps = (float) (Math.Floor((100000.00 * ( (float) totalMapCount / (float) totalWorkload)))/1000);
	if (percentageMaps > 7.5)
	{
		Amber2 = 1;
	}
	if (percentageMaps > 10)
	{
		Red2 = 1;
		reportingSqlConnection = new SqlConnection(reportingConnectionString);
		transactionCount = 0;
		percentageMaps = 0;

		selectString = "select isnull(sum(WECount),0)from dbo.WorkloadEvents "
			+ "where WEDate = convert(datetime, '" + reportDateStringMinus2 
			+ "',103)";
		myDataReader = null;
		mySqlCommand = new SqlCommand(selectString, reportingSqlConnection);
		transactionCount = 0;
		totalWorkload = 0;
		try
		{
			reportingSqlConnection.Open();
			myDataReader = mySqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
			while (myDataReader.Read())
			{
				transactionCount = transactionCount + Convert.ToInt32(myDataReader.GetSqlInt32(0).ToString());
			} // end of while
			totalWorkload = totalWorkload +transactionCount;
			myDataReader.Close();
		}
		catch(Exception e)
		{
			EventLogger.WriteEntry ("Error reading workload events "+e.Message,EventLogEntryType.Error);
			statusCode = (int)StatusCode.ErrorReadingTable; // Failed reading table
		}
		finally
		{
			// Always call Close when done reading.
			if (myDataReader != null)
				myDataReader.Close();
		}
			
		selectString = "select isnull(sum(MECount),0)from dbo.MapEvents "
			+ "where MEDate = convert(datetime, '" + reportDateStringMinus2 
			+ "',103)";

		myDataReader = null;
		mySqlCommand = new SqlCommand(selectString, reportingSqlConnection);
		transactionCount = 0;
		totalMapCount = 0;
		try
		{
			reportingSqlConnection.Open();
			myDataReader = mySqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
			while (myDataReader.Read())
			{
				transactionCount = transactionCount + Convert.ToInt32(myDataReader.GetSqlInt32(0).ToString());
			} // end of while
			totalMapCount = totalMapCount +transactionCount;
			myDataReader.Close();
		}
		catch(Exception e)
		{
			EventLogger.WriteEntry ("Error reading Map Events "+e.Message,EventLogEntryType.Error);
			statusCode = (int)StatusCode.ErrorReadingTable; // Failed reading table
		}
		finally
		{
			// Always call Close when done reading.
			if (myDataReader != null)
				myDataReader.Close();
		}

		percentageMaps = (float) (Math.Floor((100000.00 * ( (float) totalMapCount / (float) totalWorkload)))/1000);

		if (percentageMaps > 10)
		{
			Red3 = 1;
		}
	}
}

	if(Red3 == 1)
	{
		this.file.WriteLine(" ");
		this.file.WriteLine("RED+1 THRESHOLD HAS BEEN PASSED  ");
		this.file.WriteLine("Percentage has been greater than 10% for last 3 days");
	}
	else
	{
		if(Red2 ==1)
		{
			this.file.WriteLine(" ");
			this.file.WriteLine("RED THRESHOLD HAS BEEN PASSED  ");
			this.file.WriteLine("Percentage has been greater than 10% for last 2 days");
		}
		else
		{
			if(Amber2 == 1)
			{
				this.file.WriteLine(" ");
				this.file.WriteLine("AMBER THRESHOLD HAS BEEN PASSED  ");
				this.file.WriteLine("Percentage has been greater than 7.5% for last 2 days");
			}
		}
	}
this.file.Close();

string from = "";
string to = "";
string smtpServer = "";
string subject = "";


readOK =  controller.ReadProperty("2", "FilePath", out filePathString);
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
readOK =  controller.ReadProperty("2", "MailRecipient", out to);
if (readOK == false || to.Length == 0)
{
statusCode = (int)StatusCode.ErrorReadingRecipientEmailAddress; // Error Reading recipient email address
return statusCode;
}
readOK =  controller.ReadProperty("2", "Title", out subject);
if (readOK == false || subject.Length == 0)
{
    subject = "Monthly Web Page And Map Usage Report %YY-MM-DD%";
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
/*

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
	/// Class to produce daily map activity report
	/// analyses page entry event and shows proportion of these web requests that are map page requests
	/// also shows count of map events within the map page requests
	/// statusCodes
	///	1001 Error Reading Current Capacity Band from Property Table
	///	1002 Failed to read CapacityBand table
	///	1003 Reading FilePath for this report
	///	1004 Failed reading a table
	/// 1005 Error Reading sender email address
	/// 1006 Error Reading recipient email address
	/// 1007 Error Reading smtpServer
	/// 1008 
	/// 1009 

	/// </summary>
	/// 
	
	public class DailyMapActivityReport
	{
		private DateTime currentDateTime;
		private EventLog EventLogger;
		private string propertyConnectionString;
		private string reportingConnectionString;
		DailyReportController controller;
		private SqlConnection reportingSqlConnection;
		private string selectString;
		private System.IO.StreamWriter file;
		private string filePathString;
		private int statusCode;
		
		
		
		
		public DailyMapActivityReport()
		{
			//
			// TODO: Add constructor logic here
			//
			EventLogger = new EventLog("Application");
			EventLogger.Source = "TD.Reporting";
			ConfigurationSettings.GetConfig("appSettings");
			propertyConnectionString = ConfigurationSettings.AppSettings["ReportProperties.connectionstring"];
			reportingConnectionString = ConfigurationSettings.AppSettings["ReportDatabase.connectionstring"];
			controller    = new DailyReportController();
		}
		
		public int RunReport(DateTime reportDate)
		{
			statusCode = (int)StatusCode.Success; // success
			string reportDateString;
			reportDateString = reportDate.ToShortDateString();
			currentDateTime = System.DateTime.Now;

			bool readOK =  controller.ReadProperty("2", "FilePath", out filePathString);
			if (readOK == false || filePathString.Length == 0)
			{
				statusCode = (int)StatusCode.ErrorReadingCapacityBand; // Error Reading FilePath
				return statusCode;
			}



			int transactionCount = 0;
			long mapTransactionCount= 0;
			long totalTransactionCount = 0;
			int PEEPETID;
			float percentageMaps = 0;
			string from = "";
			string smtpServer = "";
			string to = "";
			string subject = "";

			
			selectString = "select PEEPETID,isnull(sum(PEECount),0)from dbo.PageEntryEvents "
			             + "where PEEDate = convert(datetime, '" + reportDateString
						 + "',103) group by PEEPETID";

			reportingSqlConnection = new SqlConnection(reportingConnectionString);

			SqlDataReader myDataReader = null;
			SqlCommand mySqlCommand = new SqlCommand(selectString, reportingSqlConnection);


			// open report text file
			file = new StreamWriter( new FileStream(filePathString, System.IO.FileMode.Create ) );
			this.file.WriteLine("------------------------------------------");
			this.file.WriteLine("TRANSPORT DIRECT DAILY MAP ACTIVITY REPORT");
			this.file.WriteLine("------------------------------------------");
			this.file.WriteLine("");
			this.file.WriteLine("For date : " + reportDateString);
			this.file.WriteLine("");
			this.file.Flush();

			try
			{
				reportingSqlConnection.Open();
				myDataReader = mySqlCommand.ExecuteReader(CommandBehavior.CloseConnection);

				while (myDataReader.Read())
				{
					PEEPETID = Convert.ToInt32(myDataReader.GetSqlByte(0).ToString());
					transactionCount = Convert.ToInt32(myDataReader.GetSqlInt32(1).ToString());

					switch (PEEPETID)
					{
					// map events  7,11,15,16,28,30,32,37,38
					//7, 'JourneyMap', 'Journey Map'
					//11, 'DetailedLegMap', 'Detailed Leg Map'
					//15, 'PrintableJourneyMaps', 'Printable Journey Maps'
					//16, 'PrintableJourneyMapInput', 'Printable Journey Map Input'
					//28, 'GeneralMaps', 'General Maps'	
					//30, 'NetworkMaps', 'Network Maps'
					//32, 'Map', 'Map'
					//37, 'TrafficMap', 'Traffic Map'
					//38, 'PrintableTrafficMap', 'Printable Traffic Map'

						case 7:
						case 11:
						case 15:
						case 16:
						case 28:
						case 30:
						case 32:
						case 37:
						case 38:
							mapTransactionCount += transactionCount;
							totalTransactionCount += transactionCount;
							break;
						default:
							totalTransactionCount += transactionCount;
							break;
					} // end of switch
				} // end of while

				percentageMaps = (float) (100.00 * ( (float) mapTransactionCount / (float) totalTransactionCount ));

				this.file.WriteLine("Total page requests today    = " + totalTransactionCount.ToString());
				this.file.WriteLine("Map page requests            = " + mapTransactionCount.ToString());

				this.file.WriteLine("Percentage that were maps    = " + percentageMaps.ToString());

				
				this.file.Flush();
			}
			catch(Exception e)
			{
				string error = "Failed reading page events table in DailyMapActivity Report :- "+e.Message;
				EventLogger.WriteEntry (error,EventLogEntryType.Error);
				statusCode = (int)StatusCode.ErrorReadingTable; // Failed to reading page events table
				return statusCode;
			}
			finally
			{
				// Always call Close when done reading.
				if (myDataReader != null)
					myDataReader.Close();

			}
			transactionCount = 0;
			selectString = "select isnull(sum(MECount),0) from dbo.MapEvents where MEDate = convert(datetime, '"
			+ reportDateString + "',103)";

			reportingSqlConnection = new SqlConnection(reportingConnectionString);

			myDataReader = null;
			mySqlCommand = new SqlCommand(selectString, reportingSqlConnection);

			try
			{
				reportingSqlConnection.Open();
				myDataReader = mySqlCommand.ExecuteReader(CommandBehavior.CloseConnection);

				while (myDataReader.Read())
				{
					transactionCount = Convert.ToInt32(myDataReader.GetSqlInt32(0).ToString());

				} // end of while


				this.file.WriteLine("");
				this.file.WriteLine("Total events within map pages  = " + transactionCount.ToString());
				this.file.WriteLine("(pans up,down,left,right,resizes etc.) ");
				
				this.file.Flush();
			}
			catch(Exception e)
			{
				string error = "Failed reading map events table in DailyMapActivityReport :-"+e.Message;
				EventLogger.WriteEntry (error,EventLogEntryType.Error);
				statusCode = (int)StatusCode.ErrorReadingTable; // Failed to read map events table
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
			this.file.Close();


			readOK =  controller.ReadProperty("2", "FilePath", out filePathString);
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
			readOK =  controller.ReadProperty("2", "MailRecipient", out to);
			if (readOK == false || to.Length == 0)
			{
				statusCode = (int)StatusCode.ErrorReadingRecipientEmailAddress; // Error Reading recipient email address
				return statusCode;
			}
			readOK =  controller.ReadProperty("2", "Title", out subject);
			if (readOK == false || subject.Length == 0)
			{
				subject = "Daily Map Activity Report";
			}
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
*/
