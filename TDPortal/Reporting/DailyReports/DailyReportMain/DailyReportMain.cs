///<header>
///DailyReportMain.cs
///Created 01/12/2003
///Author JP Scott
///
///Version	Date		Who	Reason
///1		01/12/2003	PS	Created
///2		20/02/2004  PS Add Reports
///3        02/06/2004  PS Add Reports
///4        03/08/2004  PS Add User Feedback Reports (release 1.0.8)
///5        05/10/2004  PS Add weighted tx reports
///6        05/01/2005  PS Add Ref Trans Extract
///7        13/04/2005  PS Add Weekly Tkting Report
///8        15/05/2005  PS Add Weekly Report (week)
///9        01/11/2005  PS Add Whitelabel and mobile reports
///10       20/11/2005  PS Add Weekly Page Landing Report
///11       13/03/2006  PS Add ENHANCED eXPOSED sERVICES Report V1.027
///12		20/06/2006  JF Add Daily Traveline Failure Report v1.0.28
///13       17/03/2010  PS Add Dft Montly and Weekly combined report
///</header>
///
using System;
using DailyReportFramework;
using System.Globalization;


namespace DailyReportMain
{

    /// <summary>
    /// Command line utility for call of FTP controller class.
    /// </summary>
    class DailyReportMain
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]

        static int Main(string[] args)
        {
            DailyReportMain app = new DailyReportMain();
            int retCode = app.RunApp(args);
            return retCode;
        }

        public int RunApp(string[] args)
        {
            int statusFlag = 0;
            // run the daily report associated with the passed argument


            if (args.Length < 1 || args.Length > 2)
            {
                displayHelp();
                statusFlag = 2;
            }
            else
            {
                bool isWanted;
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
                    }
                    if (argNo == 2)
                    {
                        arg2 = s;
                    }

                }

                switch (arg1)
                {
                    case "/help":
                        displayHelp();
                        statusFlag = 2;
                        break;

                    case "1": // Daily Surge Report
                    case "2": // MapActivity Report
                    case "3": // Monthly Transactional Mix Report
                    case "4": // DFT Weekly Report
                    case "5": // Monthly Availability Report
                    case "6": // Monthly Surge Report
                    case "7": // Mapping Availability Report
                    case "8": // Ticketing Report
                    case "9": // Monthly Average Response Time Report
                    case "10": // Monthly Transaction Performance Report
                    case "11": // Monthly WebPage&MapUsageReport
                    case "12": // Monthly MapTransactionSReport
                    case "13": // Monthly User Feedback Report
                    case "14": // Monthy Weighted Transaction Report
                    case "15": // Daily Weighted Transaction Report
                    case "16": // Daily Reference Transaction Extracts 
                    case "17": // Weekly Ticketing Report
                    case "18": // DFT Weekly Report
                    case "19": // Visit Britain Weekly Report
                    case "20": // Daily Traveline Failure Report
                    case "21": // BBC Weekly Report
                    case "22": // GNER Weekly Report
                    case "23": // DirectGov Weekly Report
                    case "24": // Page Landing Weekly Report
                    case "25": // Enhanced Exposed Services Report
                    case "26": // DfT Monthly Report
                    case "27": // DftWeekly Combined Report
                    case "28": // Dft Weekly Summary  
                    case "29": // Dft Monthly Summary

                        DailyReportController newReport = new DailyReportController();
                        if (args.Length == 1)
                        {
                            isWanted = newReport.OKToPrint(Convert.ToInt32(arg1));
                        }
                        else
                        {
                            int tmpDay = Convert.ToInt32(arg2.Substring(0, 2));
                            int tmpMonth = Convert.ToInt32(arg2.Substring(3, 2));
                            int tmpYear = Convert.ToInt32(arg2.Substring(6, 4));
                            DateTime reportDate = new DateTime(tmpYear, tmpMonth, tmpDay, 0, 0, 0);
                            isWanted = true;
                            newReport.ReportDate = reportDate;

                        }
                        if (isWanted == true)
                        {
                            statusFlag = newReport.ProduceReport(Convert.ToInt32(arg1));
                        }
                        else
                        {
                            statusFlag = 1; //already run
                        }
                        break;
                    default:
                        Console.WriteLine("Invalid parameters, please read help text for correct usage");
                        Console.WriteLine(" ");
                        displayHelp();
                        statusFlag = 2;
                        break;
                }
            }

            if (statusFlag == 0)
            {
                Console.WriteLine("Report completed successfully");
            }
            else if (statusFlag == 1)
            {
                Console.WriteLine("Report has been run today");
            }
            else
            {
                Console.WriteLine("Report failed with exit code : " + statusFlag.ToString());
            }

            return statusFlag;
        }

        private void displayHelp()
        {
            Console.WriteLine("DailyReportMain.exe");
            Console.WriteLine("");
            Console.WriteLine("");
            Console.WriteLine("execute this program from the command line by typing one ");
            Console.WriteLine("of the following commands");
            Console.WriteLine("DailyReportMain [ReportNo]");
            Console.WriteLine("DailyReportMain [ReportNo] [Date]");
            Console.WriteLine("DailyReportMain /help");
            Console.WriteLine("");
            Console.WriteLine("Where :-");
            Console.WriteLine("");
            Console.WriteLine("[ReportNo] is the the report number ");
            Console.WriteLine("           1 = Daily Surge Report");
            Console.WriteLine("           2 = Daily Map Usage Report");
            Console.WriteLine("           3 = Monthly Transactional Mix Report");
            Console.WriteLine("           4 = DFT Weekly Report [week-to-date]");
            Console.WriteLine("           5 = Monthly Availability Report");
            Console.WriteLine("           6 = Monthly Surge Report");
            Console.WriteLine("           7 = Mapping Availability Report");
            Console.WriteLine("           8 = Monthly Ticketing Report");
            Console.WriteLine("           9 = Monthly Average Response Time Report");
            Console.WriteLine("           10 = Monthly Transaction Performance Report");
            Console.WriteLine("           11 = Monthly Web Page And Map Usage Report");
            Console.WriteLine("           12 = Monthly Map Transactions Report");
            Console.WriteLine("           13 = Monthly User Feedback Report");
            Console.WriteLine("           14 = Monthly Weighted Transaction Report");
            Console.WriteLine("           15 = Daily Weighted Transaction Report");
            Console.WriteLine("           16 = Daily Reference Transaction Extract");
            Console.WriteLine("           17 = Weekly Ticketing Report");
            Console.WriteLine("           18 = DFT Weekly Report [Week End]");
            Console.WriteLine("           19 = Visit Britain Weekly Report");
            Console.WriteLine("           20 = Daily Traveline Failure Report");
            Console.WriteLine("           21 = BBC Weekly Report");
            Console.WriteLine("           22 = GNER Weekly Report");
            Console.WriteLine("           23 = DirectGov Weekly Report");
            Console.WriteLine("           24 = Page Landing Weekly Report");
            Console.WriteLine("           25 = Enhanced Exposed Services");
            Console.WriteLine("           26 = DfT Monthly Report");
            Console.WriteLine("           27 = DfT Weekly Combined Report");
            Console.WriteLine("           28 = DfT Weekly Summary");
            Console.WriteLine("           29 = DfT Monthly Summary");

            Console.WriteLine("");
            Console.WriteLine("[Date] is the date you wish to report on (must be in the past)");
            Console.WriteLine("");
            Console.WriteLine("/help    -  display this text");
            Console.WriteLine("");
        }
    }
}
