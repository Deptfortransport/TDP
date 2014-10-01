// *********************************************** 
// NAME                 : WriteKizoomToReportingDB.cs 
// AUTHOR               : Phil Scott
// DATE CREATED         : 06/01/2011
// DESCRIPTION          : Extract program to read kizoom and write to db
// 
// ************************************************ 
// $Log:   P:/TDPortal/archives/Reporting/ReportSummary/WriteKizoomToReportingDB/WriteKizoomToReportingDB.cs-arc  $
//
//   Rev 1.4   Mar 06 2012 10:05:02   PScott
//Modified to prevent writing of un-guarenteed mobile figures
//Resolution for 5797: Kizoom/Trapeze new web pages screen scraping update
//
//   Rev 1.3   Mar 05 2012 15:01:14   PScott
//Changed for new trapeze pages.
//Resolution for 5797: Kizoom/Trapeze new web pages screen scraping update
//
//   Rev 1.2   Jan 13 2011 11:31:00   PScott
//Post review changes
//Resolution for 5663: Update Kizoom extraction to write to DB
//

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

namespace WriteKizoomToReportingDB
{
    class WriteKizoomToReportingDB
    {


        private string propertyConnectionString;

        [STAThread]
        static int Main(string[] args)
        {

            WriteKizoomToReportingDB app = new WriteKizoomToReportingDB();

            int retCode = app.RunApp(args);
            return retCode;
        }


        public int RunApp(string[] args)
        {


            string reportingConnectionString;

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
            int statusFlag = 0;
            // run the kizoom extract based on current day or date passed and extract all previous week

            DateTime maxDate = DateTime.Now.AddDays(-1);
            DateTime minDate = DateTime.Now.AddDays(-7);
            DateTime GuarenteeDate = DateTime.Now.AddDays(-2);
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



            // now find the start and end of the week window 
            minDate = maxDate.AddDays(-6);



            #region readwebsite
            string[] Pages = new string[7];
            string[] TVPages = new string[7];
            string[] Sessions = new string[7];
            string[] TVSessions = new string[7];
            string[] tempTot = new string[7];
            string[] cellDescription = new string[5];

            string chosenDateStr = null;
            string searchDateStr = null;

            string tempStr = null;
            string strUrl = null;
            DateTime chosenDate = minDate;
            WebClient webClient = new WebClient();
            string urlContent;
            string[] mySubArray = null;
           
            NetworkCredential myCred = new NetworkCredential("transportdirect", "ceHans6", "");
            CredentialCache myCache = new CredentialCache();
            byte[] reqHTML;

            int totPages;
            int totSessions;
            int tdtim = 0;
            int tdtit = 0;
            int tdti = 0;
            int tdtimSessions = 0;
            int tdtitSessions = 0;
            int tdtiSessions = 0;

            for (int dayPtr = 0; dayPtr < 7; dayPtr++)
            {

                chosenDate = minDate.AddDays(dayPtr);
                chosenDateStr = string.Format("{0:yyyy/MM/dd}", chosenDate);
                searchDateStr = string.Format("{0:yyyy-MM-dd}", chosenDate);

                if (chosenDate <= maxDate)
                {
                    try
                    {
                        totPages = 0;
                        totSessions = 0;
                        tdtim = 0;
                        tdtit = 0;
                        tdti = 0;
                        tdtimSessions = 0;
                        tdtitSessions = 0;
                        tdtiSessions = 0;
                        // now find todays tdtim figure
                        strUrl = "http://stats.prod.tpti.co.uk/transportdirect/" + chosenDateStr + "/csv/tdtim_pageviews_google_analytics.csv";


                        myCache.Add(new Uri(strUrl), "Basic", myCred);
                        WebRequest wr = WebRequest.Create(strUrl);
                        wr.Credentials = myCache;
                        webClient.Credentials = wr.Credentials;
                        reqHTML = webClient.DownloadData(strUrl);
                        UTF8Encoding objUTF8 = new UTF8Encoding();
                        if (reqHTML.Length == 0)
                        {
                            urlContent = "0";
                        }
                        else
                        {

                            urlContent = objUTF8.GetString(reqHTML);
                            urlContent = urlContent.Substring(urlContent.IndexOf(searchDateStr) + 11, 6);
                        }
                        mySubArray = urlContent.Split('\n');
                        urlContent = mySubArray[0];
                        tdtim = Convert.ToInt32(urlContent);


                        // now find todays tdtit figure 
                        strUrl = "http://stats.prod.tpti.co.uk/transportdirect/" + chosenDateStr + "/csv/tdtit_pageviews_google_analytics.csv";
                        wr = WebRequest.Create(strUrl);
                        wr.Credentials = myCache;

                        webClient.Credentials = wr.Credentials;
                        objUTF8 = new UTF8Encoding();
                        reqHTML = webClient.DownloadData(strUrl);
                        if (reqHTML.Length == 0)
                        {
                            urlContent = "0";
                        }
                        else
                        {
                            urlContent = objUTF8.GetString(reqHTML);
                            urlContent = urlContent.Substring(urlContent.IndexOf(searchDateStr) + 11, 6);
                        }


                        mySubArray = urlContent.Split('\n');
                        urlContent = mySubArray[0];
                        tdtit = Convert.ToInt32(urlContent);

                        // now find todays tdti figure 
                        strUrl = "http://stats.prod.tpti.co.uk/transportdirect/" + chosenDateStr + "/csv/tdti_pageviews_google_analytics.csv";

                        wr = WebRequest.Create(strUrl);
                        wr.Credentials = myCache;
                        webClient.Credentials = wr.Credentials;
                        reqHTML = webClient.DownloadData(strUrl);
                        objUTF8 = new UTF8Encoding();

                        if (reqHTML.Length == 0)
                        {
                            urlContent = "0";
                        }
                        else
                        {
                            urlContent = objUTF8.GetString(reqHTML);
                            urlContent = urlContent.Substring(urlContent.IndexOf(searchDateStr) + 11, 6);
                        }
                        mySubArray = urlContent.Split('\n');
                        urlContent = mySubArray[0];
                        tdti = Convert.ToInt32(urlContent);

                        // now find todays session figures 
                        strUrl = "http://stats.prod.tpti.co.uk/transportdirect/" + chosenDateStr + "/csv/daily_sessions.csv";

                        wr = WebRequest.Create(strUrl);
                        wr.Credentials = myCache;
                        webClient.Credentials = wr.Credentials;
                        reqHTML = webClient.DownloadData(strUrl);
                        objUTF8 = new UTF8Encoding();
                        urlContent = objUTF8.GetString(reqHTML);
                        if (urlContent.Length == 0)
                        {
                            tdtiSessions = 0;
                            tdtitSessions = 0;
                            tdtimSessions = 0;
                        }
                        else
                        {
                            tempStr = mySubArray[1];

                            tdtitSessions = Convert.ToInt32(tempStr);

                            tempStr = urlContent.Substring(urlContent.IndexOf("(tdti)") + 8);
                            mySubArray = tempStr.Split('"');
                            if (mySubArray[1].Length == 0)
                            {
                                tempStr = "0";
                            }
                            else
                            {
                                tempStr = mySubArray[1];
                            }
                            tdtiSessions = Convert.ToInt32(tempStr);


                            tempStr = urlContent.Substring(urlContent.IndexOf("(tdtit)") + 9);
                            mySubArray = tempStr.Split('"');
                            if (mySubArray[1].Length == 0)
                            {
                                tempStr = "0";
                            }
                            else
                            {
                                tempStr = mySubArray[1];
                            }
                            tdtitSessions = Convert.ToInt32(tempStr);

                            tempStr = urlContent.Substring(urlContent.IndexOf("(tdtim)") + 9);
                            mySubArray = tempStr.Split('"');
                            if (mySubArray[1].Length == 0)
                            {
                                tempStr = "0";
                            }
                            else
                            {
                                tempStr = mySubArray[1];
                            }
                            tdtimSessions = Convert.ToInt32(tempStr);
                        }

                        if (chosenDate > GuarenteeDate)   // can't guarentee figures so dont publish
                        {
                            tdtit=0;
                            tdti=0;
                            tdtim=0;
                            tdtitSessions=0;
                            tdtiSessions=0;
                            tdtimSessions = 0;
                        }
                        TVPages[dayPtr] = tdtit.ToString();
                        totPages = (tdtit + tdti + tdtim);
                        Pages[dayPtr] = totPages.ToString();
                        TVSessions[dayPtr] = tdtitSessions.ToString();
                        totSessions = (tdtitSessions + tdtiSessions + tdtimSessions);
                        Sessions[dayPtr] = totSessions.ToString();
                        

                    }
                    catch (Exception ex)
 
                    {
                        Console.WriteLine(strUrl + " Problems found :- \n" + ex.Message + "\n" + ex.StackTrace);
                        statusFlag = 4;

                    }
                }
                else
                {
                    TVPages[dayPtr] = "0";
                    TVSessions[dayPtr] = "0";
                    Sessions[dayPtr] = "0";
                    Pages[dayPtr] = "0";
                }
            }

            #endregion





            #region update db

            string qry = "select * from KizoomEvents where [Date] >= '" + minDate
                    + "' and [Date] <= '" + maxDate + "'";
            string del = "delete from KizoomEvents where [Date] >= convert(datetime,'" + minDate.Date
                    + "',105) and [Date] <= convert(datetime,'" + maxDate.Date + "',105)";


            string ins = @"insert into KizoomEvents (Date,Sessions,TVSessions,Pages,TVPages) Values (@Date,@Sessions,@TVSessions,@Pages,@TVPages)";

            SqlConnection conn = new SqlConnection(reportingConnectionString);

            try
            {
                conn.Open();


                // delete existing db entries in supplied date range

                SqlCommand cmd = new SqlCommand(del, conn);

                int x = cmd.ExecuteNonQuery();

                // now write the ones read from KizoomEvents

                // Update KizoomEvents
                cmd = new SqlCommand(ins, conn);

                SqlParameter myDate = cmd.Parameters.Add("@Date", SqlDbType.Date, 10, "Date");

                SqlParameter mySessions = cmd.Parameters.Add("@Sessions", SqlDbType.Int, 4, "Sessions");
                mySessions.SourceVersion = DataRowVersion.Original;


                SqlParameter myTVSessions = cmd.Parameters.Add("@TVSessions", SqlDbType.Int, 4, "TVSessions");


                SqlParameter myPages = cmd.Parameters.Add("@Pages", SqlDbType.Int, 4, "Pages");

                SqlParameter myTVPages = cmd.Parameters.Add("@TVPages", SqlDbType.Int, 4, "TVPages");


                for (int dayPtr = 0; dayPtr < 7; dayPtr++)
                {
                    myDate.Value = minDate.AddDays(dayPtr);
                    mySessions.Value = Sessions[dayPtr];
                    myTVSessions.Value = TVSessions[dayPtr];
                    myPages.Value = Pages[dayPtr];
                    myTVPages.Value = TVPages[dayPtr];
                    cmd.ExecuteNonQuery();

                }


            }
            catch (Exception e)
            {
                Console.WriteLine("Failed updating KizoomEvents DB : " + e.Message, EventLogEntryType.Error);
                statusFlag = 5;
            }
            finally
            {
                conn.Close();
            }




            #endregion



            return statusFlag;
        }







        static bool GetLine(string Path, string SearchString, out string ReturnString)
        {

            // Initialize "myArray"
            int pos = 0;


            string currentLine = null;
            ReturnString = null;
            bool itemFound = false;
            using (StreamReader sr = new StreamReader(Path))
            {
                while (sr.Peek() >= 0 && !itemFound)
                {
                    currentLine = sr.ReadLine();
                    pos = currentLine.IndexOf(SearchString);
                    if (pos >= 0)
                    {
                        itemFound = true;
                        ReturnString = currentLine;
                    }
                }
                // Close reader
                sr.Close();

            }

            return itemFound;
        }

        private void displayHelp()
        {
            Console.WriteLine("WriteKizoomToReportingDB.exe");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("execute this program from the command line by typing one ");
            Console.WriteLine("of the following commands");
            Console.WriteLine("WriteKizoomToReportingDB");
            Console.WriteLine("WriteKizoomToReportingDB [Date]");
            Console.WriteLine("WriteKizoomToReportingDB /help");
            Console.WriteLine("");


            Console.WriteLine("");
            Console.WriteLine("[Date] is the date you wish to gather kizoom data on.");
            Console.WriteLine("It must be in the past. The whole week of the date entered will be gathered");
            Console.WriteLine("If no Date is supplied, yesterday will be assumed and the whole associated week gathered");
            Console.WriteLine("/help    -  display this text");
            Console.WriteLine("");

        }




    }
}


