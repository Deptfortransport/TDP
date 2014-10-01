///<header>
///Monthly Map Transaction Report (Rep12)
///Created 23/04/2004
///Author JP Scott
///
///Version	Date		Who	Reason
///1		23/04/2004	PS	Created transactions
///2		01/06/2004	PS	Minor display change (lines A10/A19)
///3        25/06/2004  PS  Test Period Adjustments
///4		06/08/2004  PS  Show Analysis for total and test adjusted figures
///5        08/08/2004  PS  amend test cal select to ensure last day is selected 
///6		04/10/2004  PS increase testperiod date array
///7        18/11/2004  PS Increase test calendar exclusion accuracy

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
	/// Class to produce Monthly Map Transactions Report
	/// analyses all map events and reports counts for month by map product
	/// </summary>
	/// 
	
	public class MonthlyMapTransactionsReport
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
		private int statusCode;
		private int[,] MapCmdCount = new int[6,6];
		private int[,] MapCmdTime  = new int[6,6];
		private string[] mapCommand = new string[6];
		private string[] mapDisplay = new string[6];
		private DateTime[] TestStarted = new DateTime[250]; 
		private DateTime[] TestCompleted = new DateTime[250]; 
		private int testCount;
		private DateTime SDate;
		private DateTime EDate;
		private int SHour,EHour,SMin,EMin ;
		private long ScompareString,EcompareString;
		private StringBuilder excludeString;

        private int totalGP = 0;
        private int totalChartView = 0;
        private int totalTableView = 0;

		private string productString;
		private string commandString;
		private int productNum;
		private int commandNum;

		public MonthlyMapTransactionsReport()
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
            RequestedDateString = reportDate.Year.ToString().Substring(2, 2)
            + "-" + reportDate.Month.ToString().PadLeft(2, '0');
            reportDateString = reportDate.ToShortDateString();
			currentDateTime = System.DateTime.Now;
			reportStartDate = new DateTime(reportDate.Year,reportDate.Month,1,0,0,0);
			reportEndDate = reportStartDate.AddMonths(1);
			reportEndDate = reportEndDate.AddMinutes(-1);
			reportStartDateString = reportStartDate.ToString();
			reportEndDateString = reportEndDate.ToString();
			reportEndDatePlus1 = reportEndDate.AddDays(1);
			reportEndDatePlus1String = reportEndDatePlus1.ToShortDateString();
			
			int daysInMonth;
			int transactionCount = 0;
			int averageTime = 0;
			int totalVector = 0;
			int totalRaster = 0;



			daysInMonth = reportEndDate.Day;
			
			tmpString = new StringBuilder();

			bool readOK =  controller.ReadProperty("12", "FilePath", out filePathString);
			if (readOK == false || filePathString.Length == 0)
			{
				statusCode = (int)StatusCode.ErrorReadingFilePath; // Error Reading FilePath
				return statusCode;
			}
            filePathString = filePathString.Replace("%YY-MM%", RequestedDateString);

			// open report text file
			file = new StreamWriter( new FileStream(filePathString, System.IO.FileMode.Create ) );
			this.file.WriteLine("------------------------------------------");
			this.file.WriteLine("TRANSPORT DIRECT");
			this.file.WriteLine("DfT Monthly Map Transactions Report");
			this.file.WriteLine("------------------------------------------");
			this.file.WriteLine("");
			this.file.WriteLine("For month starting : " + reportStartDate.ToShortDateString());
			this.file.WriteLine("");

			this.file.WriteLine("");
			this.file.Flush();
//*******************************
			transactionCount = 0;

		
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
 
 
//*******************************
  
			reportingSqlConnection = new SqlConnection(reportingConnectionString);
			reportingSqlConnection.Open();
			
			selectString =
				"select ME.MEMDTID,MDT.MDTDescription,ME.MEMCTID,MCT.MCTDescription, isnull(sum(ME.MECount),0),avg(ME.MEAvMsDuration)"
				+ "from dbo.MapEvents ME, dbo.MapCommandType MCT, dbo.MapDisplayType MDT "
				+ "where ME.MEMCTID = MCT.MCTID and ME.MEMDTID = MDT.MDTID "
				+ "  and ME.MEDate >= convert(datetime, '" + reportStartDateString
				+ "',103) and ME.MEDate <= convert(datetime, '" + reportEndDateString + "',103) "
				+"group by ME.MEMDTID,MDT.MDTDescription,ME.MEMCTID,MCT.MCTDescription order by ME.MEMDTID,MDT.MDTDescription,ME.MEMCTID,MCT.MCTDescription";


			myDataReader = null;
			mySqlCommand = new SqlCommand(selectString, reportingSqlConnection);

			transactionCount = 0;

			try
			{
				myDataReader = mySqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
				while (myDataReader.Read())
				{


					productNum  = Convert.ToInt32(myDataReader.GetByte(0).ToString());
					productString  = myDataReader.GetString(1);
					commandNum  = Convert.ToInt32(myDataReader.GetByte(2).ToString());
					commandString  = myDataReader.GetString(3);
					transactionCount = Convert.ToInt32(myDataReader.GetSqlInt32(4).ToString());
					averageTime = Convert.ToInt32(myDataReader.GetSqlInt32(5).ToString());

					mapCommand[commandNum]=commandString;
					mapDisplay[productNum]=productString;
					MapCmdCount[productNum,commandNum]=transactionCount;
					MapCmdTime[productNum,commandNum] = averageTime;
					

				} // end of while

				tmpString = new StringBuilder();
				tmpString.Append("Raster Maps");
				this.file.WriteLine(tmpString);
				
				tmpString = new StringBuilder();
				tmpString.Append("Command,");
				tmpString.Append(mapCommand[1]);
				tmpString.Append(",");
				tmpString.Append(mapCommand[2]);
				tmpString.Append(",");
				tmpString.Append(mapCommand[3]);
				tmpString.Append(",");
				tmpString.Append(mapCommand[4]);
                tmpString.Append(",");
                tmpString.Append(mapCommand[5]);
                tmpString.Append(",Total");
				this.file.WriteLine(tmpString);

				tmpString = new StringBuilder();
				tmpString.Append("Product(Scale),Count");
				tmpString.Append(",Count");
				tmpString.Append(",Count");
				tmpString.Append(",Count,Transaction Count");
				this.file.WriteLine(tmpString);
				int i;
				for (i=1;i<=4;i++)
				{

					tmpString = new StringBuilder();
					switch(i)
					{
						case 1:
							tmpString.Append("OS Street View");
							break;
						case 2:
							tmpString.Append("1:50000 Scale");
							break;
						case 3:
							tmpString.Append("1:250000 Scale");
							break;
						case 4:
							tmpString.Append("MiniScale");
							break;
						case 5:
							tmpString.Append("Strategi");
							break;
					}
					tmpString.Append(",");
					tmpString.Append(MapCmdCount[i,1]);
					tmpString.Append(",");
					tmpString.Append(MapCmdCount[i,2]);
					tmpString.Append(",");
					tmpString.Append(MapCmdCount[i,3]);
					tmpString.Append(",");
					tmpString.Append(MapCmdCount[i,4]);
                    tmpString.Append(",");
                    tmpString.Append(MapCmdCount[i, 5]);
                    tmpString.Append(",");
                    tmpString.Append(MapCmdCount[i, 1] + MapCmdCount[i, 2] + MapCmdCount[i, 3] + MapCmdCount[i, 5]);
					this.file.WriteLine(tmpString);

                    totalRaster = totalRaster + MapCmdCount[i, 1] + MapCmdCount[i, 2] + MapCmdCount[i, 3] + MapCmdCount[i, 5];
				
				
				}

				this.file.WriteLine(" ");
				this.file.WriteLine(" ");

				tmpString = new StringBuilder();
				tmpString.Append("Vector Maps");
				this.file.WriteLine(tmpString);
				
				tmpString = new StringBuilder();
				tmpString.Append("Command,");
				tmpString.Append(mapCommand[1]);
				tmpString.Append(",");
				tmpString.Append(mapCommand[2]);
				tmpString.Append(",");
				tmpString.Append(mapCommand[3]);
				tmpString.Append(",");
				tmpString.Append(mapCommand[4]);
                tmpString.Append(",");
                tmpString.Append(mapCommand[5]);
                tmpString.Append(",Total");
				this.file.WriteLine(tmpString);

				tmpString = new StringBuilder();
				tmpString.Append("Product (Scale),Count");
				tmpString.Append(",Count,Count,Count,Transaction Count");
				this.file.WriteLine(tmpString);
				i=5;
				tmpString = new StringBuilder();
				tmpString.Append("Strategi");
				tmpString.Append(",");
				tmpString.Append(MapCmdCount[i,1]);
				tmpString.Append(",");
				tmpString.Append(MapCmdCount[i,2]);
				tmpString.Append(",");
				tmpString.Append(MapCmdCount[i,3]);
				tmpString.Append(",");
                tmpString.Append(MapCmdCount[i, 4]);
                tmpString.Append(",");
                tmpString.Append(MapCmdCount[i, 5]);
                tmpString.Append(",");
                tmpString.Append(MapCmdCount[i, 1] + MapCmdCount[i, 2] + MapCmdCount[i, 3] + MapCmdCount[i, 5]);
				this.file.WriteLine(tmpString);

                totalVector = MapCmdCount[i, 1] + MapCmdCount[i, 2] + MapCmdCount[i, 3] + MapCmdCount[i, 5];
	
				
				
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

			
			//.............................			
			this.file.WriteLine(" ");
			this.file.WriteLine(" ");
			this.file.WriteLine("Summary Transaction Totals for Month ");
			this.file.WriteLine(" ");
			this.file.WriteLine("Raster Maps ,"+totalRaster.ToString());
			this.file.WriteLine("Vector Maps ,"+totalVector.ToString());
			this.file.WriteLine(" ");
			this.file.WriteLine(" ");
			this.file.Flush();




            //*************************************************************************************************************************
            transactionCount = 0;
            totalChartView = 0;
            totalTableView = 0;
            totalGP = 0;



            reportingSqlConnection = new SqlConnection(reportingConnectionString);

            selectString =
                "select GPEGPDTID, isnull(sum(GPECount),0)"
                + "from dbo.GradientProfileEvents "
                + "where  GPEDate >= convert(datetime, '" + reportStartDateString
                + "',103) and GPEDate <= convert(datetime, '" + reportEndDateString
                + "',103) group by GPEGPDTID order by GPEGPDTID";

            myDataReader = null;
            mySqlCommand = new SqlCommand(selectString, reportingSqlConnection);
            transactionCount = 0;

            try
            {
                reportingSqlConnection.Open();
                myDataReader = mySqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (myDataReader.Read())
                {
                    productNum = Convert.ToInt32(myDataReader.GetByte(0).ToString());
                    transactionCount = Convert.ToInt32(myDataReader.GetSqlInt32(1).ToString());


                    if (productNum == 2)
                    {
                        totalChartView = transactionCount;
                    }
                    if (productNum == 3)
                    {
                        totalTableView = transactionCount;
                    }


                } // end of while
                this.file.Flush();
                myDataReader.Close();
            }
            catch (Exception e)
            {
                EventLogger.WriteEntry("Failure reading Gradient Profile table " + e.Message, EventLogEntryType.Error);
                statusCode = (int)StatusCode.ErrorReadingTable; //
            }
            finally
            {
                // Always call Close when done reading.
                if (myDataReader != null)
                    myDataReader.Close();
            }

            totalGP = totalChartView + totalTableView;
            this.file.WriteLine(" ");
            this.file.WriteLine(" ");
            this.file.WriteLine("Gradient Profile Lookups for Month ");
            this.file.WriteLine(" ,Chart View,Table View ,Total");
            this.file.WriteLine("WebServiceRequests ," + totalChartView.ToString()
                + "," + totalTableView.ToString() + "," + totalGP.ToString());
            this.file.WriteLine(" ");
            this.file.WriteLine(" ");
            this.file.Flush();

            
            
            
            // now find number of ITN routing transactions
			
			selectString = 
			"select SUM(RPECount),RPECongestedRoute "
			+ "from dbo.RoadPlanEvents "
			+ "where RPEDate >= convert(datetime, '" + reportStartDateString
			+ "',103) and RPEDate <= convert(datetime, '" + reportEndDateString
			+ "',103) " 
			+ "group by RPECongestedRoute order by RPECongestedRoute";

			myDataReader = null;
			mySqlCommand = new SqlCommand(selectString, reportingSqlConnection);
			transactionCount = 0;
			bool congested;
			int totalTrans = 0;
            			try
			{
				reportingSqlConnection.Open();
				myDataReader = mySqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
				
				tmpString = new StringBuilder();
				tmpString.Append("ITN Routing Transactions");
				this.file.WriteLine(tmpString);	
		
				while (myDataReader.Read())
				{
					transactionCount = Convert.ToInt32(myDataReader.GetSqlInt32(0).ToString());
					totalTrans = totalTrans+transactionCount;
					congested  = myDataReader.GetBoolean(1);
					if (congested == true)
					{
						tmpString = new StringBuilder();
						tmpString.Append("Adjusted Route,");
						tmpString.Append(transactionCount.ToString());
						this.file.WriteLine(tmpString);	
					} 
					else
					{
						tmpString = new StringBuilder();
						tmpString.Append("Normal Route,");
						tmpString.Append(transactionCount.ToString());
						this.file.WriteLine(tmpString);	
					}

				} // end of while
				
				tmpString = new StringBuilder();
				tmpString.Append("Total,");
				tmpString.Append(totalTrans.ToString());
				this.file.WriteLine(tmpString);	

				
				
				
				this.file.Flush();
				myDataReader.Close();
			}
			catch(Exception e)
			{
				EventLogger.WriteEntry ("Failure reading RoadPlanEvents table "+e.Message,EventLogEntryType.Error);
				statusCode = (int)StatusCode.ErrorReadingTable; // Failed to reading page events table
			}
			finally
			{
				// Always call Close when done reading.
				if (myDataReader != null)
					myDataReader.Close();
			}


			this.file.WriteLine(" ");
			this.file.WriteLine("Gazetteer Transactions");
			
			tmpString = new StringBuilder();
			tmpString.Append("Product Description, Count");
			this.file.WriteLine(tmpString);

				
		selectString = 
		"select GT.GTID,GT.GTDescription, isnull(sum(GE.GECount),0) "
		+ "from dbo.GazetteerEvents GE, dbo.GazetteerType GT "
		+ "where GT.GTID = GE.GEGTID "
		+ "  and GE.GEDate >= convert(datetime, '" + reportStartDateString
		+ "',103) and GE.GEDate <= convert(datetime, '" + reportEndDateString
		+ "',103) " 
		+ "group by GT.GTID,GT.GTDescription order by GT.GTID,GT.GTDescription";

			myDataReader = null;
			mySqlCommand = new SqlCommand(selectString, reportingSqlConnection);
			transactionCount = 0;

			try
			{
				reportingSqlConnection.Open();
				myDataReader = mySqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
				while (myDataReader.Read())
				{
					productNum  = Convert.ToInt32(myDataReader.GetByte(0).ToString());
					productString  = myDataReader.GetString(1);
					transactionCount = Convert.ToInt32(myDataReader.GetSqlInt32(2).ToString());
					
					tmpString = new StringBuilder();
					tmpString.Append(productString);
					tmpString.Append(",");
					tmpString.Append(transactionCount.ToString());
					this.file.WriteLine(tmpString);
					
				} // end of while

				
				
				this.file.Flush();
				myDataReader.Close();
			}
			catch(Exception e)
			{
				EventLogger.WriteEntry ("Failure reading gazetteer events table "+e.Message,EventLogEntryType.Error);
				statusCode = (int)StatusCode.ErrorReadingTable; // Failed to reading page events table
			}
			finally
			{
				// Always call Close when done reading.
				if (myDataReader != null)
					myDataReader.Close();
			}
			this.file.Flush();
//================================
			
			//			Now do same again accounting for the test calendar

			transactionCount = 0;
			totalRaster = 0;
			totalVector = 0;

			if (testCount == 0)
			{
				this.file.WriteLine(" ");
				this.file.WriteLine("(The above mapping figures are derived from actual user transactions. No testing transactions are present)");
				this.file.WriteLine(" ");
			}
			else
			{
				this.file.WriteLine(" ");
				this.file.WriteLine("The above mapping figures include periods of testing where maps have been auto-generated but not actually viewed by anybody.");
				this.file.WriteLine("The figures below have been adjusted to ignore these testing periods and should be used as an indication of actual user requests for charging purposes.");
				this.file.WriteLine(" ");

				this.file.Flush();
				//*******************************
				transactionCount = 0;

				//*******************************
  
				reportingSqlConnection = new SqlConnection(reportingConnectionString);
				reportingSqlConnection.Open();
			
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
			
				selectString =
					"select ME.MEMDTID,MDT.MDTDescription,ME.MEMCTID,MCT.MCTDescription, isnull(sum(ME.MECount),0),avg(ME.MEAvMsDuration)"
					+ "from dbo.MapEvents ME, dbo.MapCommandType MCT, dbo.MapDisplayType MDT "
					+ "where ME.MEMCTID = MCT.MCTID and ME.MEMDTID = MDT.MDTID "
					+ "  and ME.MEDate >= convert(datetime, '" + reportStartDateString
					+ "',103) and ME.MEDate <= convert(datetime, '" + reportEndDateString + "',103) "
					+ excludeString
					+" group by ME.MEMDTID,MDT.MDTDescription,ME.MEMCTID,MCT.MCTDescription order by ME.MEMDTID,MDT.MDTDescription,ME.MEMCTID,MCT.MCTDescription";


				myDataReader = null;
				mySqlCommand = new SqlCommand(selectString, reportingSqlConnection);

				transactionCount = 0;

				try
				{
					myDataReader = mySqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
					while (myDataReader.Read())
					{


						productNum  = Convert.ToInt32(myDataReader.GetByte(0).ToString());
						productString  = myDataReader.GetString(1);
						commandNum  = Convert.ToInt32(myDataReader.GetByte(2).ToString());
						commandString  = myDataReader.GetString(3);
						transactionCount = Convert.ToInt32(myDataReader.GetSqlInt32(4).ToString());
						averageTime = Convert.ToInt32(myDataReader.GetSqlInt32(5).ToString());

						mapCommand[commandNum]=commandString;
						mapDisplay[productNum]=productString;
						MapCmdCount[productNum,commandNum]=transactionCount;
						MapCmdTime[productNum,commandNum] = averageTime;
					

					} // end of while

					tmpString = new StringBuilder();
					tmpString.Append("Raster Maps");
					this.file.WriteLine(tmpString);
				
					tmpString = new StringBuilder();
					tmpString.Append("Command,");
					tmpString.Append(mapCommand[1]);
					tmpString.Append(",");
					tmpString.Append(mapCommand[2]);
					tmpString.Append(",");
					tmpString.Append(mapCommand[3]);
					tmpString.Append(",");
					tmpString.Append(mapCommand[4]);
                    tmpString.Append(",");
                    tmpString.Append(mapCommand[5]);
                    tmpString.Append(",Total");
					this.file.WriteLine(tmpString);

					tmpString = new StringBuilder();
					tmpString.Append("Product(Scale),Count");
					tmpString.Append(",Count");
					tmpString.Append(",Count");
					tmpString.Append(",Count,Transaction Count");
					this.file.WriteLine(tmpString);
					int i;
					for (i=1;i<=4;i++)
					{

						tmpString = new StringBuilder();
						switch(i)
						{
							case 1:
								tmpString.Append("OS Street View");
								break;
							case 2:
								tmpString.Append("1:50000 Scale");
								break;
							case 3:
								tmpString.Append("1:250000 Scale");
								break;
							case 4:
								tmpString.Append("MiniScale");
								break;
							case 5:
								tmpString.Append("Strategi");
								break;
						}
						tmpString.Append(",");
						tmpString.Append(MapCmdCount[i,1]);
						tmpString.Append(",");
						tmpString.Append(MapCmdCount[i,2]);
						tmpString.Append(",");
						tmpString.Append(MapCmdCount[i,3]);
						tmpString.Append(",");
						tmpString.Append(MapCmdCount[i,4]);
						tmpString.Append(",");
                        tmpString.Append(MapCmdCount[i,5]);
                        tmpString.Append(",");
                        tmpString.Append(MapCmdCount[i, 1] + MapCmdCount[i, 2] + MapCmdCount[i, 3] + MapCmdCount[i, 5]);
						this.file.WriteLine(tmpString);

                        totalRaster = totalRaster + MapCmdCount[i, 1] + MapCmdCount[i, 2] + MapCmdCount[i, 3] + MapCmdCount[i, 5];
				
				
					}

					this.file.WriteLine(" ");
					this.file.WriteLine(" ");

					tmpString = new StringBuilder();
					tmpString.Append("Vector Maps");
					this.file.WriteLine(tmpString);
				
					tmpString = new StringBuilder();
					tmpString.Append("Command,");
					tmpString.Append(mapCommand[1]);
					tmpString.Append(",");
					tmpString.Append(mapCommand[2]);
					tmpString.Append(",");
					tmpString.Append(mapCommand[3]);
					tmpString.Append(",");
					tmpString.Append(mapCommand[4]);
                    tmpString.Append(",");
                    tmpString.Append(mapCommand[5]);
                    tmpString.Append(",Total");
					this.file.WriteLine(tmpString);

					tmpString = new StringBuilder();
					tmpString.Append("Product (Scale),Count");
					tmpString.Append(",Count,Count,Count,Transaction Count");
					this.file.WriteLine(tmpString);
					i=5;
					tmpString = new StringBuilder();
					tmpString.Append("Strategi");
					tmpString.Append(",");
					tmpString.Append(MapCmdCount[i,1]);
					tmpString.Append(",");
					tmpString.Append(MapCmdCount[i,2]);
					tmpString.Append(",");
					tmpString.Append(MapCmdCount[i,3]);
					tmpString.Append(",");
					tmpString.Append(MapCmdCount[i,4]);
                    tmpString.Append(",");
                    tmpString.Append(MapCmdCount[i, 5]);
                    tmpString.Append(",");
                    tmpString.Append(MapCmdCount[i, 1] + MapCmdCount[i, 2] + MapCmdCount[i, 3] + MapCmdCount[i, 5]);
					this.file.WriteLine(tmpString);

                    totalVector = MapCmdCount[i, 1] + MapCmdCount[i, 2] + MapCmdCount[i, 3] + MapCmdCount[i, 5];
	
				
				
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

			
				//.............................			
				this.file.WriteLine(" ");
				this.file.WriteLine(" ");
				this.file.WriteLine("Summary Transaction Totals for Month ");
				this.file.WriteLine(" ");
				this.file.WriteLine("Raster Maps ,"+totalRaster.ToString());
				this.file.WriteLine("Vector Maps ,"+totalVector.ToString());
				this.file.WriteLine(" ");
				this.file.WriteLine(" ");
				this.file.Flush();




                //*************************************************************************************************************************
                transactionCount = 0;
                totalChartView = 0;
                totalTableView = 0;
                totalGP = 0;

                SDate = new DateTime(1, 1, 1, 0, 0, 0);
                EDate = new DateTime(1, 1, 1, 0, 0, 0);
                excludeString = new StringBuilder();
                for (int i = 1; i <= testCount; i++)
                {
                    SDate = TestStarted[i].Date;
                    SHour = TestStarted[i].Hour;
                    SMin = TestStarted[i].Minute;

                    EDate = TestCompleted[i].Date;
                    EHour = TestCompleted[i].Hour;
                    EMin = TestCompleted[i].Minute;

                    SMin = SMin / 15;
                    EMin = EMin / 15;
                    ScompareString = ((long)SDate.Year - 2000) * 100000000L + ((long)SDate.Month * 1000000L) + ((long)SDate.Day * 10000L) + (long)SHour * 100L + (long)SMin;
                    EcompareString = ((long)EDate.Year - 2000) * 100000000L + ((long)EDate.Month * 1000000L) + ((long)EDate.Day * 10000L) + (long)EHour * 100L + (long)EMin;

                    // build exclusion string - nothing allowed within this 			
                    excludeString.Append(" and not (");
                    excludeString.Append("((year(GPEDate)-2000)*100000000+month(GPEDate)*1000000+day(GPEDate)*10000+GPEHour*100+GPEHourQuarter) >= " + ScompareString);
                    excludeString.Append(" and ((year(GPEDate)-2000)*100000000+month(GPEDate)*1000000+day(GPEDate)*10000+GPEHour*100+GPEHourQuarter) <= " + EcompareString);
                    excludeString.Append(")");

                }


                reportingSqlConnection = new SqlConnection(reportingConnectionString);

                selectString =
                    "select GPEGPDTID, isnull(sum(GPECount),0)"
                    + "from dbo.GradientProfileEvents "
                    + "where  GPEDate >= convert(datetime, '" + reportStartDateString
                    + "',103) and GPEDate <= convert(datetime, '" + reportEndDateString
                    + "',103) "
                    + excludeString
                    + " group by GPEGPDTID order by GPEGPDTID";

                myDataReader = null;
                mySqlCommand = new SqlCommand(selectString, reportingSqlConnection);
                transactionCount = 0;

                try
                {
                    reportingSqlConnection.Open();
                    myDataReader = mySqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
                    while (myDataReader.Read())
                    {
                        productNum = Convert.ToInt32(myDataReader.GetByte(0).ToString());
                        transactionCount = Convert.ToInt32(myDataReader.GetSqlInt32(1).ToString());


                        if (productNum == 2)
                        {
                            totalChartView = transactionCount;
                        }
                        if (productNum == 3)
                        {
                            totalTableView = transactionCount;
                        }


                    } // end of while
                    this.file.Flush();
                    myDataReader.Close();
                }
                catch (Exception e)
                {
                    EventLogger.WriteEntry("Failure reading Gradient Profile table " + e.Message, EventLogEntryType.Error);
                    statusCode = (int)StatusCode.ErrorReadingTable; //
                }
                finally
                {
                    // Always call Close when done reading.
                    if (myDataReader != null)
                        myDataReader.Close();
                }

                totalGP = totalChartView + totalTableView;
                this.file.WriteLine(" ");
                this.file.WriteLine(" ");
                this.file.WriteLine("Gradient Profile Lookups for Month ");
                this.file.WriteLine(" ,Chart View,Table View ,Total");
                this.file.WriteLine("WebServiceRequests ," + totalChartView.ToString()
                    + "," + totalTableView.ToString() + "," + totalGP.ToString());
                this.file.WriteLine(" ");
                this.file.WriteLine(" ");
                this.file.Flush();









				// now find number of ITN routing transactions

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
					
					SMin=SMin/15;
					EMin=EMin/15;
					ScompareString = ((long)SDate.Year-2000) *100000000L + ((long)SDate.Month *1000000L) + ((long)SDate.Day*10000L) + (long)SHour*100L+(long)SMin;
					EcompareString = ((long)EDate.Year-2000) *100000000L + ((long)EDate.Month *1000000L) + ((long)EDate.Day*10000L) + (long)EHour*100L+(long)EMin;

					// build exclusion string - nothing allowed within this 			
					excludeString.Append(" and not (");
					excludeString.Append(     "((year(RPEDate)-2000)*100000000+month(RPEDate)*1000000+day(RPEDate)*10000+RPEHour*100+RPEHourQuarter) >= "+ScompareString);
					excludeString.Append(" and ((year(RPEDate)-2000)*100000000+month(RPEDate)*1000000+day(RPEDate)*10000+RPEHour*100+RPEHourQuarter) <= "+EcompareString);
					excludeString.Append(")");

				}
						
				selectString = 
					"select SUM(RPECount),RPECongestedRoute "
					+ "from dbo.RoadPlanEvents "
					+ "where RPEDate >= convert(datetime, '" + reportStartDateString
					+ "',103) and RPEDate <= convert(datetime, '" + reportEndDateString
					+ "',103) " 
					+ excludeString
					+ " group by RPECongestedRoute order by RPECongestedRoute";

				myDataReader = null;
				mySqlCommand = new SqlCommand(selectString, reportingSqlConnection);
				transactionCount = 0;
				congested = false;
				totalTrans = 0;
				try
				{
					reportingSqlConnection.Open();
					myDataReader = mySqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
				
					tmpString = new StringBuilder();
					tmpString.Append("ITN Routing Transactions");
					this.file.WriteLine(tmpString);	
		
					while (myDataReader.Read())
					{
						transactionCount = Convert.ToInt32(myDataReader.GetSqlInt32(0).ToString());
						totalTrans = totalTrans+transactionCount;
						congested  = myDataReader.GetBoolean(1);
						if (congested == true)
						{
							tmpString = new StringBuilder();
							tmpString.Append("Adjusted Route,");
							tmpString.Append(transactionCount.ToString());
							this.file.WriteLine(tmpString);	
						} 
						else
						{
							tmpString = new StringBuilder();
							tmpString.Append("Normal Route,");
							tmpString.Append(transactionCount.ToString());
							this.file.WriteLine(tmpString);	
						}

					} // end of while
				
					tmpString = new StringBuilder();
					tmpString.Append("Total,");
					tmpString.Append(totalTrans.ToString());
					this.file.WriteLine(tmpString);	

				
				
				
					this.file.Flush();
					myDataReader.Close();
				}
				catch(Exception e)
				{
					EventLogger.WriteEntry ("Failure reading RoadPlanEvents table "+e.Message,EventLogEntryType.Error);
					statusCode = (int)StatusCode.ErrorReadingTable; // Failed to reading page events table
				}
				finally
				{
					// Always call Close when done reading.
					if (myDataReader != null)
						myDataReader.Close();
				}


				this.file.WriteLine(" ");
				this.file.WriteLine("Gazetteer Transactions");
			
				tmpString = new StringBuilder();
				tmpString.Append("Product Description, Count");
				this.file.WriteLine(tmpString);

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
					SMin=SMin/15;
					EMin=EMin/15;
					
					ScompareString = ((long)SDate.Year-2000) *100000000L + ((long)SDate.Month *1000000L) + ((long)SDate.Day*10000L) + (long)SHour*100L+SMin; //+(long)SHourQuarter;
					EcompareString = ((long)EDate.Year-2000) *100000000L + ((long)EDate.Month *1000000L) + ((long)EDate.Day*10000L) + (long)EHour*100L+EMin ;//+(long)EHourQuarter;

					// build exclusion string - nothing allowed within this 			
					excludeString.Append(" and not (");
					excludeString.Append(     "((year(GE.GEDate)-2000)*100000000+month(GE.GEDate)*1000000+day(GE.GEDate)*10000+GE.GEHour*100+GE.GEHourQuarter) >= "+ScompareString);
					excludeString.Append(" and ((year(GE.GEDate)-2000)*100000000+month(GE.GEDate)*1000000+day(GE.GEDate)*10000+GE.GEHour*100+GE.GEHourQuarter) <= "+EcompareString);
					excludeString.Append(")");
				}
			
					
				selectString = 
					"select GT.GTID,GT.GTDescription, isnull(sum(GE.GECount),0) "
					+ "from dbo.GazetteerEvents GE, dbo.GazetteerType GT "
					+ "where GT.GTID = GE.GEGTID "
					+ "  and GE.GEDate >= convert(datetime, '" + reportStartDateString
					+ "',103) and GE.GEDate <= convert(datetime, '" + reportEndDateString
					+ "',103) " 
					+ excludeString
					+ " group by GT.GTID,GT.GTDescription order by GT.GTID,GT.GTDescription";

				myDataReader = null;
				mySqlCommand = new SqlCommand(selectString, reportingSqlConnection);
				transactionCount = 0;

				try
				{
					reportingSqlConnection.Open();
					myDataReader = mySqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
					while (myDataReader.Read())
					{
						productNum  = Convert.ToInt32(myDataReader.GetByte(0).ToString());
						productString  = myDataReader.GetString(1);
						transactionCount = Convert.ToInt32(myDataReader.GetSqlInt32(2).ToString());
					
						tmpString = new StringBuilder();
						tmpString.Append(productString);
						tmpString.Append(",");
						tmpString.Append(transactionCount.ToString());
						this.file.WriteLine(tmpString);
					
					} // end of while

				
				
					this.file.Flush();
					myDataReader.Close();
				}
				catch(Exception e)
				{
					EventLogger.WriteEntry ("Failure reading gazetteer events table "+e.Message,EventLogEntryType.Error);
					statusCode = (int)StatusCode.ErrorReadingTable; // Failed to reading page events table
				}
				finally
				{
					// Always call Close when done reading.
					if (myDataReader != null)
						myDataReader.Close();
				}
				this.file.Flush();
			}

//===================================

			this.file.Close();


			string from = "";
			string to = "";
			string smtpServer = "";
			string subject = "";


			readOK =  controller.ReadProperty("12", "FilePath", out filePathString);
			if (readOK == false || filePathString.Length == 0)
			{
				statusCode = (int)StatusCode.ErrorReadingFilePath; // Error Reading FilePath
				return statusCode;
			}
            filePathString = filePathString.Replace("%YY-MM%", RequestedDateString);
            readOK = controller.ReadProperty("0", "MailAddress", out from);
			if (readOK == false || from.Length == 0)
			{
				statusCode = (int)StatusCode.ErrorReadingSenderEmailAddress; // Error Reading sender email address
				return statusCode;
			}
			readOK =  controller.ReadProperty("12", "MailRecipient", out to);
			if (readOK == false || to.Length == 0)
			{
				statusCode = (int)StatusCode.ErrorReadingRecipientEmailAddress; // Error Reading recipient email address
				return statusCode;
			}
			readOK =  controller.ReadProperty("12", "Title", out subject);
			if (readOK == false || subject.Length == 0)
			{
                subject = "Monthly Map Transactions Report %YY-MM%";
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
