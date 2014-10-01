using System;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Net;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Globalization;

namespace KizoomDBExtract
{
    class KizoomDBExtract
    {
        /// <summary>
        /// The main entry point for the application.


        [STAThread]
        static int Main(string[] args)
        {

            KizoomDBExtract app = new KizoomDBExtract();

            int retCode = app.RunApp(args);
            return retCode;
        }


        public int RunApp(string[] args)
        {
            //
            // TODO: Add code to start application here
            //
            string path = null;
            string searchString = null;
            string returnString = null;
            DateTime thisDay;
            string dayOfWeek;
            string[] displayDateString = new string[32];
            int[] dayTot = new int[32];
            int[] daySessions = new int[32];
            int[] dayTVSessions = new int[32];
            int[] dayPages = new int[32];
            int[] dayTVPages = new int[32];

            int statusFlag = 0;
            DateTime reportStartDate;
            DateTime reportEndDate;
            string reportStartDateString = null;
            string reportEndDateString = null;
            
            string reportingConnectionString;

            string propertyConnectionString;



            // get date


            DateTime maxDate = DateTime.Now.AddDays(-2);
            DateTime minDate = maxDate;
            
            if (args.Length > 1)
            {
                displayHelp();
                statusFlag = 2;
            }
            else
            {

                int argNo = 0;
                string arg1 = "";
                string arg2 = "";
                foreach (string s in args)
                {
                    s.ToLower(CultureInfo.CurrentCulture);
                    argNo++;
                    if (argNo == 1)
                    {
                        arg1 = s;
                        maxDate = Convert.ToDateTime(arg1);
                    }
                    if (argNo == 2)
                    {
                        arg2 = s;
                        maxDate = Convert.ToDateTime(arg2);
                    }

                }

            }

           

            // now find the start and end of the window 
            minDate = maxDate.AddDays(-31);

            reportStartDateString = minDate.ToShortDateString();
            reportEndDateString = maxDate.ToShortDateString();

            ConfigurationManager.GetSection("appSettings");
            propertyConnectionString = ConfigurationManager.AppSettings["ReportProperties.connectionstring"];

            reportingConnectionString = ConfigurationManager.AppSettings["ReportDatabase.connectionstring"];

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
            + " and  Date < convert(datetime, '" + reportEndDateString + "',103)"
            + " group by Date Order by Date";

            SqlDataReader myDataReader = null;
            SqlConnection reportingSqlConnection = new SqlConnection(reportingConnectionString);
            SqlCommand mySqlCommand = new SqlCommand(selectString, reportingSqlConnection);

            int Sessions = 0;
            int TVSessions = 0;
            int Pages = 0;
            int TVPages = 0;
            int dayPos = 0;

            for (int i = 1; i <= 31; i++)
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
                dayPos = 0;
                while (myDataReader.Read())
                {
                    thisDay = myDataReader.GetDateTime(0);
                    Sessions = Convert.ToInt32(myDataReader.GetSqlInt32(1).ToString());
                    TVSessions = Convert.ToInt32(myDataReader.GetSqlInt32(2).ToString());
                    Pages = Convert.ToInt32(myDataReader.GetSqlInt32(3).ToString());
                    TVPages = Convert.ToInt32(myDataReader.GetSqlInt32(4).ToString());

                    dayPos++;
                    displayDateString[dayPos] = thisDay.Date.ToShortDateString();
                    
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


            int totPages = 0;
            int totSessions = 0;
            float ratio;
            int totTVPages = 0;
            int totTVSessions = 0;
            

            StreamWriter sw = null;
            FileStream fs = null;
            try
            {
                StringBuilder tempStr = new StringBuilder();
                string logFile = @"c:\KizoomSummaryExtract.csv";

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

                tempStr = new StringBuilder();
                tempStr.Append("Mobile Volumes ");
                for (int i = 1;i<32;i++)
                {
                    tempStr.Append(",");
                    tempStr.Append(displayDateString[i]);
                }
                sw.Write(tempStr);
                sw.Write(sw.NewLine);

                //------------------------------------------------------------------------------------------
                tempStr = new StringBuilder();
                tempStr.Append("User WebPages");
                for (int i = 1; i < 8; i++)
                {
                
                    totPages = dayPages[i] - dayTVPages[i];
                    
                    tempStr.Append(",");
                    tempStr.Append(totPages.ToString());
                }
                sw.Write(tempStr); 
                sw.Write(sw.NewLine);
                //------------------------------------------------------------------------------------------
                tempStr = new StringBuilder();
                tempStr.Append("User Sessions");
                for (int i = 1; i < 8; i++)
                {
                    totSessions = daySessions[i] - dayTVSessions[i];
                    tempStr.Append(",");
                    tempStr.Append(totSessions.ToString());
                }
                sw.Write(tempStr); 
                sw.Write(sw.NewLine);
                //------------------------------------------------------------------------------------------
                tempStr = new StringBuilder();
                tempStr.Append("Ratio of Web Pages / User Sessions");
                for (int i = 1; i < 8; i++)
                {
                    totPages = dayPages[i] - dayTVPages[i];
                    totSessions = daySessions[i] - dayTVSessions[i];
                    if (totSessions > 0)
                    {
                        ratio = (float)totPages / (float)totSessions;
                    }
                    else
                    {
                        ratio = 0;
                    }
                    tempStr.Append(",");
                    tempStr.Append(ratio.ToString("N2"));
                }
                sw.Write(tempStr); 
                sw.Write(sw.NewLine);
                //------------------------------------------------------------------------------------------
                sw.Write(sw.NewLine);
                sw.Write("Mobile Volumes less SiteConfidence Monitoring");
                 sw.Write(sw.NewLine);

                //------------------------------------------------------------------------------------------
                tempStr = new StringBuilder();
                tempStr.Append("User WebPages");
                for (int i = 1; i < 8; i++)
                {
                
                    totPages = dayPages[i] - dayTVPages[i]-288;
                    if (totPages<0) totPages=0;

                    tempStr.Append(",");
                    tempStr.Append(totPages.ToString());
                }
                sw.Write(tempStr); 
                sw.Write(sw.NewLine);
                //------------------------
                //------------------------------------------------------------------------------------------
                tempStr = new StringBuilder();
                tempStr.Append("User Sessions");
                for (int i = 1; i < 8; i++)
                {
                    totSessions = daySessions[i] - dayTVSessions[i] - 288;
                    if (totSessions < 0) totSessions = 0;
                    tempStr.Append(",");
                    tempStr.Append(totSessions.ToString());
                }
                sw.Write(tempStr); 
                sw.Write(sw.NewLine);
                //------------------------------------------------------------------------------------------
                tempStr = new StringBuilder();
                tempStr.Append("Ratio of Web Pages / User Sessions");
                for (int i = 1; i < 8; i++)
                {
                    totPages = dayPages[i] - dayTVPages[i]-288;
                    if (totPages < 0) totPages = 0;

                    totSessions = daySessions[i] - dayTVSessions[i]-288;
                    if (totSessions < 0) totSessions = 0;
                    if (totSessions > 0)
                    {
                        ratio = (float)totPages / (float)totSessions;
                    }
                    else
                    {
                        ratio = 0;
                    }
                    tempStr.Append(",");
                    tempStr.Append(ratio.ToString("N2"));
                }
                sw.Write(tempStr); 
                sw.Write(sw.NewLine);
                //------------------------------------------------------------------------------------------
                sw.Write(sw.NewLine); 
                sw.Write("Digi TV");
                sw.Write(sw.NewLine);
                sw.Write("Total Digi TV Volumes");
                sw.Write(sw.NewLine);
                //------------------------------------------------------------------------------------------
                tempStr = new StringBuilder();
                tempStr.Append("User WebPages");
                for (int i = 1; i < 8; i++)
                {
                    totPages = dayTVPages[i];
                    tempStr.Append(",");
                    tempStr.Append(totPages.ToString());
                }
                sw.Write(tempStr);
                sw.Write(sw.NewLine);
                //------------------------------------------------------------------------------------------
                tempStr = new StringBuilder();
                tempStr.Append("User Sessions");
                for (int i = 1; i < 8; i++)
                {
                    totSessions = dayTVSessions[i];
                    tempStr.Append(",");
                    tempStr.Append(totSessions.ToString());
                }
                sw.Write(tempStr);
                sw.Write(sw.NewLine);
                //------------------------------------------------------------------------------------------
                tempStr = new StringBuilder();
                tempStr.Append("Ratio of Web Pages / User Sessions");
                for (int i = 1; i < 8; i++)
                {
                    totPages = dayTVPages[i];
                    totSessions = dayTVSessions[i];
                    if (totSessions > 0)
                    {
                        ratio = (float)totPages / (float)totSessions;
                    }
                    else
                    {
                        ratio = 0;
                    }
                    tempStr.Append(",");
                    tempStr.Append(ratio.ToString("N2"));
                }
                sw.Write(tempStr);
                sw.Write(sw.NewLine);
                //------------------------------------------------------------------------------------------
                sw.Write(sw.NewLine);
                sw.Write(sw.NewLine);

                //------------------------------------------------------------------------------------------
                tempStr = new StringBuilder();
                tempStr.Append("Total Sessions");
                for (int i = 1; i < 8; i++)
                {
                    totSessions = daySessions[i];
                    tempStr.Append(",");
                    tempStr.Append(totSessions.ToString());
                }
                sw.Write(tempStr);
                sw.Write(sw.NewLine);
                //------------------------------------------------------------------------------------------
                tempStr = new StringBuilder();
                tempStr.Append("TV Sessions");
                for (int i = 1; i < 8; i++)
                {
                    totSessions = dayTVSessions[i];
                    tempStr.Append(",");
                    tempStr.Append(totSessions.ToString());
                }
                sw.Write(tempStr);
                sw.Write(sw.NewLine);
                //------------------------------------------------------------------------------------------
                tempStr = new StringBuilder();
                tempStr.Append("Total pages");
                for (int i = 1; i < 8; i++)
                {
                    totPages = dayPages[i];
                    tempStr.Append(",");
                    tempStr.Append(totPages.ToString());
                }
                sw.Write(tempStr);
                sw.Write(sw.NewLine);
                //------------------------------------------------------------------------------------------
                tempStr = new StringBuilder();
                tempStr.Append("TV pages");
                for (int i = 1; i < 8; i++)
                {
                    totPages = dayTVPages[i];
                    tempStr.Append(",");
                    tempStr.Append(totPages.ToString());
                }
                sw.Write(tempStr);
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
                    sw.Close();
                }


            }
            return statusFlag;
        }
    
        private void displayHelp()
        {
            Console.WriteLine("KizoomDBExtract.exe");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("execute this program from the command line by typing one ");
            Console.WriteLine("of the following commands");
            Console.WriteLine("KizoomDBExtract");
            Console.WriteLine("KizoomDBExtract [Date]");
            Console.WriteLine("KizoomDBExtract /help");
            Console.WriteLine("");


            Console.WriteLine("");
            Console.WriteLine("[Date] is the date you wish to extract kizoom data on.");
            Console.WriteLine("It must be in the past. The preceeding 31 days data from the date entered will be extracted");
            Console.WriteLine("If no Date is supplied, yesterday will be assumed");
            Console.WriteLine("/help    -  display this text");
            Console.WriteLine("");
       }
}
}
