///<header>
///Service1.cs
///Created 01/12/2003
///Author JP Scott
///
///Version	Date		Who	Reason
///1		01/12/2003	PS	Created
///</header>
///
using System;
using DailyReportFramework;
using System.Configuration;

namespace DailyReportService
{
    public class DailyReportService : System.ServiceProcess.ServiceBase
    {
        // The main entry point for the process
        static void Main()
        {
            System.ServiceProcess.ServiceBase[] ServicesToRun;

            // More than one user Service may run within the same process. To add
            // another service to this process, change the following line to
            // create a second service object. For example,
            //
            //   ServicesToRun = New System.ServiceProcess.ServiceBase[] {new Service1(), new MySecondUserService()};
            //
            ServicesToRun = new System.ServiceProcess.ServiceBase[] { new DailyReportService() };

            System.ServiceProcess.ServiceBase.Run(ServicesToRun);

        }

        #region DailyReportService

        /// <summary> 
        /// Required designer variable.
        /// 
        /// </summary>
        private System.ComponentModel.Container components = null;

        public DailyReportService()
        {

            // This call is required by the Windows.Forms Component Designer.
            InitializeComponent();

            // TODO: Add any initialization after the InitComponent call

        }

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            this.ServiceName = "DailyReportService";

        }

        #region Events

        protected void MyTimerElapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            bool isWanted;
            int maxReports;
            string maxReportString;
            DailyReportController newDailyReport = new DailyReportController();
            bool maxReportsOk = newDailyReport.ReadProperty("0", "NumberOfReports", out maxReportString);
            maxReports = Convert.ToInt32(maxReportString);
            for (int reportNumber = 1; reportNumber <= maxReports; reportNumber++)
            {
                // check if report to be printed
                newDailyReport = new DailyReportController();
                isWanted = newDailyReport.OKToPrint(reportNumber);
                if (isWanted == true)
                {
                    int statusCode = newDailyReport.ProduceReport(reportNumber);
                }
            }
        }

        #endregion

        #region Service events

        /// <summary>
        /// Set things in motion so your service can do its work.
        /// </summary>
        protected override void OnStart(string[] args)
        {
            double interval = 600000; // every 10 mins
            if (!Double.TryParse(ConfigurationManager.AppSettings["DailyReportService.TimerInterval.Seconds"], out interval))
            {
                interval = 600000; // every 10 mins
            }

            System.Timers.Timer myTimer = new System.Timers.Timer();
            myTimer.Enabled = true;
            myTimer.Interval = interval;
            myTimer.Elapsed += new System.Timers.ElapsedEventHandler(MyTimerElapsed);
        }

        /// <summary>
        /// Stop this service.
        /// </summary>
        protected override void OnStop()
        {
            // TODO: Add code here to perform any tear-down necessary to stop your service.
        }

        protected override void OnContinue()
        {
        }

        #endregion

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #endregion
    }
}
