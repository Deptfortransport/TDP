
using System;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Text;
using System.Diagnostics;






namespace DailySummaryExtract
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
    /// 



	class DailySummaryExtract
	{
              
		/// <summary>
		/// The main entry point for the application.

     
        
        /// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			//
			// TODO: Add code to start application here
			//
			string path = null ;
			string searchString = null;
			string returnString= null;
		    DateTime thisDay;
		    string dayOfWeek;
		    int[] dayTot = new int[8];
            int[] daySessions= new int[8];
            int[] dayTVSessions= new int[8];
            int[] dayPages = new int[8];
            int[] dayTVPages = new int[8];


		    DateTime reportStartDate;
		    DateTime reportEndDatePlus1;
		    
            string reportingConnectionString;

            string propertyConnectionString;



// get date

       			path = @"c:\DFTWeeklyReport.csv" ;
				searchString = "For week starting";
                string reportStartDateString = "";
                string reportEndDatePlus1String = "";
				if (GetLine(path,searchString, out returnString))
				{
					string [] temp = null;
					temp = returnString.Split(':');
					returnString = temp[1].Trim();
				    reportStartDateString = returnString;
                    reportStartDate = DateTime.Parse(reportStartDateString);
                    reportEndDatePlus1 = reportStartDate.AddDays(7);
                    reportEndDatePlus1String = reportEndDatePlus1.ToShortDateString();

				}



            propertyConnectionString = System.Configuration.ConfigurationSettings.AppSettings["ReportProperties.connectionstring"];

            reportingConnectionString = System.Configuration.ConfigurationSettings.AppSettings["ReportDatabase.connectionstring"];

#if DEBUG
            if (propertyConnectionString == null)
            {
                propertyConnectionString = "server=localhost;Trusted_Connection=yes;database=ReportProperties";
            }
            if (reportingConnectionString == null)
            {
                reportingConnectionString = "server=localhost;Trusted_Connection=yes;database=Reporting";
            }
#endif


            #region Kizoom


            string selectString = "select Date,isnull(sum(Sessions),0),isnull(sum(TVSessions),0),isnull(sum(Pages),0),isnull(sum(TVPages),0) "
            + "from dbo.KizoomEvents "
            + "where Date > = convert(datetime, '" + reportStartDateString + "',103)"
            + " and  Date < convert(datetime, '" + reportEndDatePlus1String + "',103)"
            +" group by Date Order by Date";

            SqlDataReader myDataReader = null;
            SqlConnection reportingSqlConnection = new SqlConnection(reportingConnectionString);
            SqlCommand mySqlCommand = new SqlCommand(selectString, reportingSqlConnection);
            
            int Sessions = 0;
            int TVSessions = 0;
            int Pages = 0;
            int TVPages = 0;
            int dayPos = 0;

            for (int i = 1; i <= 7; i++)
            {
                daySessions[i] = 0;
                dayTVSessions[i] = 0;
                dayPages[i] = 0;
                dayTVPages[i] = 0;
            }
			try
			{
                reportingSqlConnection.Open();
				myDataReader = mySqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
				while (myDataReader.Read())
				{
					thisDay = myDataReader.GetDateTime(0);
                    Sessions = Convert.ToInt32(myDataReader.GetSqlInt32(1).ToString());
                    TVSessions = Convert.ToInt32(myDataReader.GetSqlInt32(2).ToString());
                    Pages = Convert.ToInt32(myDataReader.GetSqlInt32(3).ToString());
                    TVPages = Convert.ToInt32(myDataReader.GetSqlInt32(4).ToString());
                    
 
					dayOfWeek = thisDay.DayOfWeek.ToString();

					switch (dayOfWeek)
					{
						case "Monday":
                            dayPos = 1;
							break;
						case "Tuesday":
                            dayPos = 2;
                            break;
						case "Wednesday":
                            dayPos = 3;
                            break;
                        case "Thursday": 
                            dayPos = 4;
                            break;
						case "Friday":
                            dayPos = 5;
							break;
						case "Saturday":
                            dayPos = 6;
							break;
						case "Sunday":
                            dayPos = 7;
							break;
					}
                    daySessions[dayPos] = daySessions[dayPos] + Sessions;
                    dayTVSessions[dayPos] = dayTVSessions[dayPos] + TVSessions;
                    dayPages[dayPos] = dayPages[dayPos] + Pages;
                    dayTVPages[dayPos] = dayTVPages[dayPos] + TVPages;

				
				} // end of while


				myDataReader.Close();

			}
            catch (Exception ex)
            {
                Console.WriteLine("error occurred" + ex.Message);
   
			}
			finally
			{
				// Always call Close when done reading.
				if (myDataReader != null)
				myDataReader.Close();
            }


            #endregion


            #region Maps


            selectString = "select MEDate,isnull(sum(MECount),0)from dbo.MapEvents "
            + "where MEDate > = convert(datetime, '" + reportStartDateString + "',103)"
            + " and   MEDate < convert(datetime, '" + reportEndDatePlus1String + "',103)"
            + " group by MEDate Order by MEDate";

            myDataReader = null;
            //SqlConnection reportingSqlConnection = new SqlConnection(reportingConnectionString);
            mySqlCommand = new SqlCommand(selectString, reportingSqlConnection);

            int transactionCount = 0;


            for (int i = 1; i <= 7; i++) dayTot[i] = 0;
            try
            {
                reportingSqlConnection.Open();
                myDataReader = mySqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (myDataReader.Read())
                {
                    thisDay = myDataReader.GetDateTime(0);
                    transactionCount = Convert.ToInt32(myDataReader.GetSqlInt32(1).ToString());
                    dayOfWeek = thisDay.DayOfWeek.ToString();

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


                myDataReader.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine("error occurred" + ex.Message);

            }
            finally
            {
                // Always call Close when done reading.
                if (myDataReader != null)
                    myDataReader.Close();
            }


            #endregion










            StreamWriter sw = null;
			FileStream fs = null;
			try
			{
               
				string logFile = @"c:\DailySummaryExtract.csv";

				if (File.Exists(logFile))
				{
					File.Delete(logFile);
				}
				else
				{ }
            
				fs = new FileStream(logFile,
					FileMode.CreateNew, FileAccess.Write, FileShare.Write);

            
				fs.Close();
				
				sw = new StreamWriter(logFile);

				path = @"c:\DFTWeeklyReport.csv" ;
				searchString = "For week starting";
  				if (GetLine(path,searchString, out returnString))
				{
					string [] temp = null;
					temp = returnString.Split(':');
					returnString = temp[1].Trim();
					sw.Write(returnString);
					sw.Write(sw.NewLine);

				}



				searchString = "Workload (Excluding Tests)";
				if (GetLine(path,searchString, out returnString))
				{
					sw.Write(returnString);
				}
                else
                {
                    sw.Write(" ,0,0,0,0,0,0,0");
                }
                sw.Write(sw.NewLine);
 

				searchString = "User Sessions (Excluding Tests)";
				if (GetLine(path,searchString, out returnString))
				{
					sw.Write(returnString);
				}
                else
                {
                    sw.Write("User Sessions (Excluding Tests),0,0,0,0,0,0,0");
                }
                sw.Write(sw.NewLine);
 
                
				path = @"c:\WeeklyBBCReport.csv";
				searchString = "Workload  (Total Load)";
				if (GetLine(path,searchString, out returnString))
				{
					sw.Write("BBC "+returnString);
				}
                else
                {
                    sw.Write("BBC Workload  (Total Load),0,0,0,0,0,0,0");
                }
                sw.Write(sw.NewLine);
 
                searchString = "Partner User Sessions";
				if (GetLine(path,searchString, out returnString))
				{
					sw.Write("BBC "+returnString);
				}
                else
                {
                    sw.Write("BBC Partner User Sessions,0,0,0,0,0,0,0");
                }
                sw.Write(sw.NewLine);
 

				path = @"c:\WeeklyDirectGovReport.csv";
				searchString = "Workload  (Total Load)";
				if (GetLine(path,searchString, out returnString))
				{
					sw.Write("DirectGov "+returnString);
				}
                else
                {
                    sw.Write("DirectGov Workload  (Total Load),0,0,0,0,0,0,0");
                }
                sw.Write(sw.NewLine);
 

				searchString = "Partner User Sessions";
				if (GetLine(path,searchString, out returnString))
				{
					sw.Write("DirectGov "+returnString);
				}
                else
                {
                    sw.Write("DirectGov Partner User Sessions,0,0,0,0,0,0,0");
                }
                sw.Write(sw.NewLine);
 


				path = @"c:\WeeklyVisitBritainReport.csv";
				searchString = "Workload  (Total Load)";
				if (GetLine(path,searchString, out returnString))
				{
					sw.Write("VisitBritain "+returnString);
				}
                else
                {
                    sw.Write("Visit Britain Workload  (Total Load),0,0,0,0,0,0,0");
                }
                sw.Write(sw.NewLine);
 

				searchString = "Partner User Sessions";
				if (GetLine(path,searchString, out returnString))
				{
					sw.Write("VisitBritain "+returnString);
				}
                else
                {
                    sw.Write("Visit Britain Partner User Sessions,0,0,0,0,0,0,0");
                }
                sw.Write(sw.NewLine);
 

				path = @"c:\WeeklyBusinessLinkReport.csv";
				searchString = "Workload  (Total Load)";
				if (GetLine(path,searchString, out returnString))
				{
					sw.Write("BusinessLink "+returnString);
				}
                else
                {
                    sw.Write("BusinessLink Workload  (Total Load),0,0,0,0,0,0,0");
                }
                sw.Write(sw.NewLine);
 
				searchString = "Partner User Sessions";
				if (GetLine(path,searchString, out returnString))
				{
                    sw.Write("BusinessLink " + returnString);
				}
                else
                {
                    sw.Write("BusinessLink User Sessions,0,0,0,0,0,0,0");
                }
                sw.Write(sw.NewLine);
 
                path = @"c:\WeeklyBusinessGatewayReport.csv";
                searchString = "Workload  (Total Load)";
                if (GetLine(path, searchString, out returnString))
                {
                    sw.Write("BusinessGateway " + returnString);
                }
                else
                {
                    sw.Write("BusinessGateway Workload  (Total Load),0,0,0,0,0,0,0");
                }
                sw.Write(sw.NewLine);
 

                searchString = "Partner User Sessions";
                if (GetLine(path, searchString, out returnString))
                {
                    sw.Write("BusinessGateway " + returnString);
                }
                else
                {
                    sw.Write("BusinessGateway Partner User Sessions,0,0,0,0,0,0,0");
                }
                sw.Write(sw.NewLine);
                
                path = @"c:\EnhancedExposedServiceReport.csv";
                searchString = "DirectGovDTV";
                if (GetLine(path, searchString, out returnString))
                {
                    sw.Write("DirectGovDTV Exposed Svc" + returnString.Substring(29));
                }
                else
                {
                    sw.Write("DirectGovDTV Exposed Svc,0,0,0,0,0,0,0");
                }
                sw.Write(sw.NewLine);
 
                searchString = "DirectGovEES";
                if (GetLine(path, searchString, out returnString))
                {
                    sw.Write("DirectGovEES Exposed Svc" + returnString.Substring(29));
                }
                else
                {
                    sw.Write("DirectGovEES Exposed Svc,0,0,0,0,0,0,0");
                }
                sw.Write(sw.NewLine);
 

                searchString = "Lauren";
				if (GetLine(path,searchString, out returnString))
				{
					sw.Write("Lauren Exposed Svc"+returnString.Substring(29));
				}
                else
                {
                    sw.Write("Lauren Exposed Svc,0,0,0,0,0,0,0");
                }
                sw.Write(sw.NewLine);
 
                path = @"c:\WeeklyPageLandingReport.csv";
                searchString = "GTDF,Door to Door Landing Page";
                if (GetLine(path, searchString, out returnString))
                {
                    sw.Write("GTDF - Door to Door" + returnString.Substring(30));
                }
                else
                {
                    sw.Write("GTDF - Door to Door,0,0,0,0,0,0,0");
                }
                sw.Write(sw.NewLine);
 

				path = @"c:\WeeklyPageLandingReport.csv";
				searchString = "LastMinute.com,iFrameFindAPlace";
				if (GetLine(path,searchString, out returnString))
				{
					sw.Write("LastMinute"+returnString.Substring(15));
				}
                else
                {
                    sw.Write("LastMinute iFrameFindAPlace,0,0,0,0,0,0,0");
                }
                sw.Write(sw.NewLine);
 
                searchString = "LastMinute.com,iFrameJourneyLandingPage";
                if (GetLine(path, searchString, out returnString))
                {
                    sw.Write("LastMinute" + returnString.Substring(15));
                }
                else
                {
                    sw.Write("LastMinute iFrameJourneyLandingPage,0,0,0,0,0,0,0");
                }
                sw.Write(sw.NewLine);
 
				searchString = "LastMinute.com,iFrameLocationLandingPage";
				if (GetLine(path,searchString, out returnString))
				{
					sw.Write("LastMinute"+returnString.Substring(15));
				}
                else
                {
                    sw.Write("LastMinute iFrameLocationLandingPage,0,0,0,0,0,0,0");
                }
                sw.Write(sw.NewLine);
 


                searchString = "LastMinute.com,iFrameJourneyPlanning";
                if (GetLine(path, searchString, out returnString))
                {
                    sw.Write("LastMinute" + returnString.Substring(15));
                }
                else
                {
                    sw.Write("LastMinute iFrameJourneyPlanning,0,0,0,0,0,0,0");
                }
                sw.Write(sw.NewLine);
 
                searchString = "National Rail Enquiries";
				if (GetLine(path,searchString, out returnString))
				{
					sw.Write("NRE "+returnString.Substring(24));
				}
                else
                {
                    sw.Write("National Rail Enquiries,0,0,0,0,0,0,0");
                }
                sw.Write(sw.NewLine);
 


                path = @"c:\DFTWeeklyReport.csv";
                
                searchString = "Single Page Sessions";
                if (GetLine(path, searchString, out returnString))
                {
                    sw.Write(returnString);
                }
                else
                {
                    sw.Write("Single Page Sessions,0,0,0,0,0,0,0");
                }
                sw.Write(sw.NewLine);
 

                searchString = "Multi-Page Sessions";
                if (GetLine(path, searchString, out returnString))
                {
                    sw.Write(returnString);
                }
                else
                {
                    sw.Write("Multi-Page Sessions,0,0,0,0,0,0,0");
                }
                sw.Write(sw.NewLine);
 

                searchString = "Average Session Duration";
                if (GetLine(path, searchString, out returnString))
                {
                    sw.Write(returnString);
                }
                else
                {
                    sw.Write("Average Session Duration,0,0,0,0,0,0,0");
                }
                sw.Write(sw.NewLine);
 


                searchString = "Average Multi-Page Session Duration";
                if (GetLine(path, searchString, out returnString))
                {
                    sw.Write(returnString);
                }
                else
                {
                    sw.Write("Average Multi-Page Session Duration,0,0,0,0,0,0,0");
                }
                sw.Write(sw.NewLine);
 



                path = @"c:\WeeklyPageLandingReport.csv";
                searchString = "GTDF,StopInformation";
                if (GetLine(path, searchString, out returnString))
                {
                    sw.Write("GTDF - Stop Info" + returnString.Substring(20));
                }
                else
                {
                    sw.Write("GTDF - Stop Info,0,0,0,0,0,0,0");
                }
                sw.Write(sw.NewLine);
 



                path = @"c:\EnhancedExposedServiceReport.csv";
                searchString = "DirectGovJPlan";
                if (GetLine(path, searchString, out returnString))
                {
                    sw.Write("DirectGovJPlan Exposed Svc" + returnString.Substring(29));
                }
                else
                {
                    sw.Write("DirectGovJPlan Exposed Svc,0,0,0,0,0,0,0");
                }
                sw.Write(sw.NewLine);


                searchString = "DirectGovAPP";
                if (GetLine(path, searchString, out returnString))
                {
                    sw.Write("DirectGovAPP Exposed Svc" + returnString.Substring(29));
                }
                else
                {
                    sw.Write("DirectGovAPP Exposed Svc,0,0,0,0,0,0,0");
                }
                sw.Write(sw.NewLine);
               



                searchString = "DirectGovAPP2";
                if (GetLine(path, searchString, out returnString))
                {
                    sw.Write("DirectGovAPP2 Exposed Svc" + returnString.Substring(29));
                }
                else
                {
                    sw.Write("DirectGovAPP2 Exposed Svc,0,0,0,0,0,0,0");
                }
                sw.Write(sw.NewLine);




                searchString = "TestDirectGov";
                if (GetLine(path, searchString, out returnString))
                {
                    sw.Write("TestDirectGov Exposed Svc" + returnString.Substring(29));
                }
                else
                {
                    sw.Write("TestDirectGov Exposed Svc,0,0,0,0,0,0,0");
                }
                sw.Write(sw.NewLine);


                sw.Write("Map Events (pans/zooms/etc.)" 
                    +","+ dayTot[1].ToString()
                    + "," + dayTot[2].ToString()
                    + "," + dayTot[3].ToString()
                    + "," + dayTot[4].ToString()
                    + "," + dayTot[5].ToString()
                    + "," + dayTot[6].ToString()
                    + "," + dayTot[7].ToString() );
                sw.Write(sw.NewLine);


                sw.Write("Kizoom Pages "
                  + "," + dayPages[1].ToString()
                  + "," + dayPages[2].ToString()
                  + "," + dayPages[3].ToString()
                  + "," + dayPages[4].ToString()
                  + "," + dayPages[5].ToString()
                  + "," + dayPages[6].ToString()
                  + "," + dayPages[7].ToString());
                sw.Write(sw.NewLine);
                sw.Write("Kizoom TVPages "
                 + "," + dayTVPages[1].ToString()
                 + "," + dayTVPages[2].ToString()
                 + "," + dayTVPages[3].ToString()
                 + "," + dayTVPages[4].ToString()
                 + "," + dayTVPages[5].ToString()
                 + "," + dayTVPages[6].ToString()
                 + "," + dayTVPages[7].ToString());
                sw.Write(sw.NewLine);
                sw.Write("Kizoom Sessions "
                 + "," + daySessions[1].ToString()
                 + "," + daySessions[2].ToString()
                 + "," + daySessions[3].ToString()
                 + "," + daySessions[4].ToString()
                 + "," + daySessions[5].ToString()
                 + "," + daySessions[6].ToString()
                 + "," + daySessions[7].ToString());
                sw.Write(sw.NewLine);
                sw.Write("Kizoom TVSessions "
                 + "," + dayTVSessions[1].ToString()
                 + "," + dayTVSessions[2].ToString()
                 + "," + dayTVSessions[3].ToString()
                 + "," + dayTVSessions[4].ToString()
                 + "," + dayTVSessions[5].ToString()
                 + "," + dayTVSessions[6].ToString()
                 + "," + dayTVSessions[7].ToString());
                sw.Write(sw.NewLine);
 
                //------------------------



                path = @"c:\WeeklyCycleRoutesReport.csv";
                searchString = "Workload  (Total Load)";
                if (GetLine(path, searchString, out returnString))
                {
                    sw.Write("CycleRoutes " + returnString);
                }
                else
                {
                    sw.Write("CycleRoutes Workload  (Total Load),0,0,0,0,0,0,0");
                }
                sw.Write(sw.NewLine);
 
                searchString = "Partner User Sessions";
                if (GetLine(path, searchString, out returnString))
                {
                    sw.Write("CycleRoutes " + returnString);
                }
                else
                {
                    sw.Write("CycleRoutes Partner User Sessions,0,0,0,0,0,0,0");
                }
                sw.Write(sw.NewLine);

                path = @"c:\EnhancedExposedServiceReport.csv";
                searchString = "BatchJourneyPlanner";
                if (GetLine(path, searchString, out returnString))
                {
                    sw.Write(searchString + " Exposed Svc" + returnString.Substring(29));
                }
                else
                {
                    sw.Write(searchString + " Exposed Svc,0,0,0,0,0,0,0");
                }
                sw.Write(sw.NewLine);
                path = @"c:\EnhancedExposedServiceReport.csv";
                searchString = "Centro";
                if (GetLine(path, searchString, out returnString))
                {
                    sw.Write(searchString + " Exposed Svc" + returnString.Substring(29));
                }
                else
                {
                    sw.Write(searchString + " Exposed Svc,0,0,0,0,0,0,0");
                }
                sw.Write(sw.NewLine);
                path = @"c:\EnhancedExposedServiceReport.csv";
                searchString = "Blackburn";
                if (GetLine(path, searchString, out returnString))
                {
                    sw.Write(searchString + " Exposed Svc" + returnString.Substring(29));
                }
                else
                {
                    sw.Write(searchString + " Exposed Svc,0,0,0,0,0,0,0");
                }
                sw.Write(sw.NewLine);

                path = @"c:\EnhancedExposedServiceReport.csv";
                searchString = "EssexCC";
                if (GetLine(path, searchString, out returnString))
                {
                    sw.Write(searchString + " Exposed Svc" + returnString.Substring(29));
                }
                else
                {
                    sw.Write(searchString + " Exposed Svc,0,0,0,0,0,0,0");
                }
                sw.Write(sw.NewLine);

                path = @"c:\EnhancedExposedServiceReport.csv";
                searchString = "Stoke-on-Trent";
                if (GetLine(path, searchString, out returnString))
                {
                    sw.Write(searchString + " Exposed Svc" + returnString.Substring(29));
                }
                else
                {
                    sw.Write(searchString + " Exposed Svc,0,0,0,0,0,0,0");
                }
                sw.Write(sw.NewLine);


                path = @"c:\EnhancedExposedServiceReport.csv";
                searchString = "LiftShare";
                if (GetLine(path, searchString, out returnString))
                {
                    sw.Write(searchString + " Exposed Svc" + returnString.Substring(29));
                }
                else
                {
                    sw.Write(searchString + " Exposed Svc,0,0,0,0,0,0,0");
                }
                sw.Write(sw.NewLine);


                path = @"c:\EnhancedExposedServiceReport.csv";
                searchString = "CIBER";
                if (GetLine(path, searchString, out returnString))
                {
                    sw.Write(searchString + " Exposed Svc" + returnString.Substring(29));
                }
                else
                {
                    sw.Write(searchString + " Exposed Svc,0,0,0,0,0,0,0");
                }
                sw.Write(sw.NewLine);

                path = @"c:\EnhancedExposedServiceReport.csv";
                searchString = "Middlesbrough";
                if (GetLine(path, searchString, out returnString))
                {
                    sw.Write(searchString + " Exposed Svc" + returnString.Substring(29));
                }
                else
                {
                    sw.Write(searchString + " Exposed Svc,0,0,0,0,0,0,0");
                }
                sw.Write(sw.NewLine);

			}
			catch (Exception ex) 
			{
				Console.WriteLine("error occurred" + ex.Message);
			}
			finally 
			{
				if (sw != null)
				{
					sw.Flush();
					sw.Close(); }
             
  				
			}
		}
		static bool GetLine(string Path,string SearchString, out string ReturnString)
		{
			
			// Initialize "myArray"
			int pos = 0;

			
			string currentLine = null;
			ReturnString = null;
			bool itemFound = false;
			using (StreamReader sr = new StreamReader(Path)) 
			{
				while (sr.Peek() >= 0  && !itemFound) 
				{
					currentLine = sr.ReadLine();
					pos = currentLine.IndexOf(SearchString);
					
					if( pos >= 0)
					{

                        if (SearchString == "DirectGovAPP")
                        {
                            // if its DirectGovAPP2 then ignore otherwise repeat search for DirectGovAPP
                            pos = currentLine.IndexOf("DirectGovAPP2");
                            if (pos >= 0)
                            {
                                return false;
                            }
                            
                        }
                        if (SearchString == "GTDF,StopInformation")
                        {
                            // if its dGTDF ignore
                            pos = currentLine.IndexOf("GTDF");
                            if (pos > 0)
                            {
                                itemFound =  false;
                            }
                            else
                            {
                                itemFound = true;
                                ReturnString = currentLine;
                                return true;
                            }
                        }
                        else
                        {
                            if (SearchString == "DirectGovDTV"
                                || SearchString == "DirectGovEES"
                                || SearchString == "Lauren"
                                || SearchString == "DirectGovAPP"
                                || SearchString == "DirectGovAPP2"
                                || SearchString == "DirectGovJPlan"
                                || SearchString == "TestDirectGov"
                                || SearchString == "BatchJourneyPlanner"
                                || SearchString == "Centro"
                                || SearchString == "Blackburn"
                                || SearchString == "EssexCC"
                                || SearchString == "Stoke-on-Trent"
                                || SearchString == "LiftShare"
                                || SearchString == "CIBER"
                                || SearchString == "Middlesbrough")
                            {
                                SearchString = "Total Daily";//  Partner Events";
                            }
                            else
                            {
                                itemFound = true;
                                ReturnString = currentLine;
                            }
                        }
					}
				}
				// Close reader
				sr.Close();
				
			}

			return itemFound;
		}
			
	}
}
