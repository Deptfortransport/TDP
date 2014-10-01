///<header>
///DailyReportController.cs
///Created 01/12/2003
///Author JP Scott
///
///Version	Date		Who	Reason
///1		01/12/2003	PS	Created
///2		20/02/2004  PS Add Reports
///3        02/06/2004  PS Add Reports
///4        03/08/2004  PS Add user feedback report (v1.0.8)
///5        05/10/2004  PS Add Weighted TX reports (v1.0.15)
///6        18/11/2004  PS Add test details (v1.0.17)
///7        05/01/2005  PS Add Ref Trans Extract (v1.0.19)
///8        05/01/2005  PS Add Weekly Tkting Report (v1.0.20) 
///9        15/06/2005  PS Make Dft Weekly report pass report number (v1.0.21)
///10       01/11/2005  PS Add Weekly GNER Whitelabel Report (v1.0.24) 
///11       20/11/2005  PS Add Weekly Page Landing Report (v1.0.25) 
///12       13/03/2006  PS Add Enhanced Exposed Services Report (v1.0.27)
///13		20/06/2006  JF Add Daily Traveline Failure Report (v1.0.28)
///
///</header>
///
using System;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Diagnostics;

using DailyReportFramework;

namespace DailyReportFramework
{
    /// <summary>
    /// Summary description for DailyReportController.
    /// 
    /// The report controller is the control class through which all 
    /// daily reports produced. Methods in this class decide if a report should run based on
    /// the last production date. It then calls the relevant class for the chosen report.
    /// 
    /// General Error Values

    ///	1001 Error Reading Current Capacity Band from Property Table
    ///	1002 Failed to read CapacityBand table
    ///	1003 Reading FilePath for this report
    ///	1004 Failed to reading page events table
    /// 1005 Error Reading sender email address
    /// 1006 Error Reading recipient email address
    /// 1007 Error Reading smtpServer
    /// 1008 Email send failed



    /// </summary>
    public class DailyReportController
    {
        private string propertyConnectionString;
        private EventLog EventLogger;
        private DateTime currentDateTime;
        private DateTime reportDate;
        private SqlConnection mySqlConnection;

        public DailyReportController()
        {
            propertyConnectionString = ConfigurationManager.AppSettings["ReportProperties.connectionstring"];

            EventLogger = new EventLog("Application");
            EventLogger.Source = "TD.Reporting";
        }

        public int ProduceReport(int reportNumber)
        {
            int statusCode;
            statusCode = (int)StatusCode.ErrorReadingSenderEmailAddress;   // unknown report failure
            switch (reportNumber)
            {
                case 1:  // Daily Surge Report
                    DailySurgeReport newSurgeReport = new DailySurgeReport();
                    statusCode = newSurgeReport.RunReport(ReportDate);
                    if (statusCode == 0)
                        EventLogger.WriteEntry("Daily Surge Report successfully produced", EventLogEntryType.Information);

                    break;

                case 2:  // Daily MapActivityReport
                    DailyMapActivityReport newMAReport = new DailyMapActivityReport();
                    statusCode = newMAReport.RunReport(ReportDate);
                    if (statusCode == 0)
                        EventLogger.WriteEntry("Daily Map Activity Report successfully produced", EventLogEntryType.Information);

                    break;
                case 3:  // MonthlyTransactionalMixReport
                    MonthlyTransactionalMixReport newMTMReport = new MonthlyTransactionalMixReport();
                    statusCode = newMTMReport.RunReport(ReportDate);
                    if (statusCode == 0)
                        EventLogger.WriteEntry("Monthly Transactional Mix Report successfully produced", EventLogEntryType.Information);
                    break;
                case 4:  // DFT Weekly Report
                case 18:
                    DFTWeeklyReport newDFTWReport = new DFTWeeklyReport();
                    statusCode = newDFTWReport.RunReport(ReportDate, reportNumber);
                    if (statusCode == 0)
                        EventLogger.WriteEntry("DFT Weekly Report successfully produced", EventLogEntryType.Information);
                    break;
                case 5:  // Monthly Availability Report
                    MonthlyAvailabilityReport newMAvReport = new MonthlyAvailabilityReport();
                    statusCode = newMAvReport.RunReport(ReportDate);
                    if (statusCode == 0)
                        EventLogger.WriteEntry("Monthly Availability Report successfully produced", EventLogEntryType.Information);
                    break;
                case 6:  // Monthly Surge Report
                    MonthlySurgeReport newMSReport = new MonthlySurgeReport();
                    statusCode = newMSReport.RunReport(ReportDate);
                    if (statusCode == 0)
                        EventLogger.WriteEntry("Monthly Surge Report successfully produced", EventLogEntryType.Information);
                    break;
                case 7:  // Mapping Availability Report
                    MonthlyMappingAvailabilityReport newMMReport = new MonthlyMappingAvailabilityReport();
                    statusCode = newMMReport.RunReport(ReportDate);
                    if (statusCode == 0)
                        EventLogger.WriteEntry("Monthly Mapping Availabiliy Report successfully produced", EventLogEntryType.Information);
                    break;
                case 8:  // Ticketing Report
                    MonthlyTicketingReport newTktReport = new MonthlyTicketingReport();
                    statusCode = newTktReport.RunReport(ReportDate);
                    if (statusCode == 0)
                        EventLogger.WriteEntry("Monthly Ticketing Report successfully produced", EventLogEntryType.Information);
                    break;
                case 9:  // Monthly AverageReponseTime Report
                    MonthlyAverageReponseTimeReport newAverageReponseTimeReport = new MonthlyAverageReponseTimeReport();
                    statusCode = newAverageReponseTimeReport.RunReport(ReportDate);
                    if (statusCode == 0)
                        EventLogger.WriteEntry("Monthly Average Reponse Time Report successfully produced", EventLogEntryType.Information);
                    break;
                case 10:  // TransactionPerformance SLA Response Exceptions Report
                    MonthlyTransactionPerformanceReport newTransactionPerformanceReport = new MonthlyTransactionPerformanceReport();
                    statusCode = newTransactionPerformanceReport.RunReport(ReportDate);
                    if (statusCode == 0)
                        EventLogger.WriteEntry("Monthly Transaction Performance Report successfully produced", EventLogEntryType.Information);
                    break;
                case 11:  // MonthlyWebPageAndMapUsageReport
                    MonthlyWebPageAndMapUsageReport newWebPageAndMapUsageReport = new MonthlyWebPageAndMapUsageReport();
                    statusCode = newWebPageAndMapUsageReport.RunReport(ReportDate);
                    if (statusCode == 0)
                        EventLogger.WriteEntry("Monthly Web Page And Map Usage Report successfully produced", EventLogEntryType.Information);
                    break;
                case 12:  // MonthlyMapTransactionsReport
                    MonthlyMapTransactionsReport newMonthlyMapTransactionsReport = new MonthlyMapTransactionsReport();
                    statusCode = newMonthlyMapTransactionsReport.RunReport(ReportDate);
                    if (statusCode == 0)
                        EventLogger.WriteEntry("Monthly Map Transactions Report successfully produced", EventLogEntryType.Information);
                    break;
                case 13:  // MonthlyMapTransactionsReport
                    MonthlyUserFeedbackReport newMonthlyUserFeedbackReport = new MonthlyUserFeedbackReport();
                    statusCode = newMonthlyUserFeedbackReport.RunReport(ReportDate);
                    if (statusCode == 0)
                        EventLogger.WriteEntry("Monthly User Feedback Report successfully produced", EventLogEntryType.Information);
                    break;

                case 14:  // MonthlyWeightedTransactionReport
                case 15:  // MonthlyWeightedTransactionReport

                    MonthlyWeightedTransactionReport newMonthlyWeightedTransactionReport = new MonthlyWeightedTransactionReport();

                    statusCode = newMonthlyWeightedTransactionReport.RunReport(ReportDate, reportNumber);
                    if (statusCode == 0)
                        EventLogger.WriteEntry("Weighted Transaction Report successfully produced", EventLogEntryType.Information);
                    break;
                case 16:  // Reference Transaction Report

                    DailyRefTransExtract newDailyRefTransExtract = new DailyRefTransExtract();

                    statusCode = newDailyRefTransExtract.RunReport(ReportDate);
                    if (statusCode == 0)
                        EventLogger.WriteEntry("Reference Transaction Extract successfully produced", EventLogEntryType.Information);
                    break;
                case 17:  // Ticketing Report
                    WeeklyTicketingReport newWkTktReport = new WeeklyTicketingReport();
                    statusCode = newWkTktReport.RunReport(ReportDate);
                    if (statusCode == 0)
                        EventLogger.WriteEntry("Weekly Ticketing Report successfully produced", EventLogEntryType.Information);
                    break;
                case 19:  // Partner 1 (WhiteLABEL) Report
                case 21:  // Partner 2 (WhiteLABEL) Report
                case 22:  // Partner 3 (WhiteLABEL) Report
                case 23:  // Partner 3 (WhiteLABEL) Report
                    PartnerWeeklyReport newPartnerReport = new PartnerWeeklyReport();
                    statusCode = newPartnerReport.RunReport(ReportDate, reportNumber);
                    if (statusCode == 0)
                        EventLogger.WriteEntry("Partner Weekly Report successfully produced", EventLogEntryType.Information);
                    break;

                /*case 20:  // Mobile (WhiteLABEL) Report
                    MobileWeeklyReport  newMobileWReport = new MobileWeeklyReport();
                    statusCode = newMobileWReport.RunReport(ReportDate,reportNumber);
                    if (statusCode == 0)
                        EventLogger.WriteEntry ("Mobile Weekly Report successfully produced",EventLogEntryType.Information);
                    break;*/
                case 20:  // Daily Traveline Failure Report

                    DailyTLFailures newDailyTLFailures = new DailyTLFailures();

                    statusCode = newDailyTLFailures.RunReport(ReportDate);
                    if (statusCode == 0)
                        EventLogger.WriteEntry("Reference Transaction Extract successfully produced", EventLogEntryType.Information);
                    break;
                case 24:  // Page Landing Report
                    LandingWeeklyReport newLandingWReport = new LandingWeeklyReport();
                    statusCode = newLandingWReport.RunReport(ReportDate, reportNumber);
                    if (statusCode == 0)
                        EventLogger.WriteEntry("Landing Weekly Report successfully produced", EventLogEntryType.Information);
                    break;
                case 25:  // Enhanced Exposed Services Report
                    EESReport newEESReport = new EESReport();
                    statusCode = newEESReport.RunReport(ReportDate, reportNumber);
                    if (statusCode == 0)
                        EventLogger.WriteEntry("Enhanced Exposed Services Report successfully produced", EventLogEntryType.Information);
                    break;

                case 26:  // Dft Monthly Report

                    DFTMonthlyReport newDFTMonthlyReport = new DFTMonthlyReport();
                    statusCode = newDFTMonthlyReport.RunReport(ReportDate, reportNumber);
                    if (statusCode == 0)
                        EventLogger.WriteEntry("DFT Monthly Report successfully produced", EventLogEntryType.Information);
                    break;
                case 27:  // Dft Weekly Combined Report

                    DFTWeeklyCombinedReport newDDFTWeeklyCombinedReport = new DFTWeeklyCombinedReport();
                    statusCode = newDDFTWeeklyCombinedReport.RunReport(ReportDate, reportNumber);
                    if (statusCode == 0)
                        EventLogger.WriteEntry("DFT Weekly Combined Report successfully produced", EventLogEntryType.Information);
                    break;
                case 28:  // Dft Weekly Summary
                case 29:  // Dft Monthy Summary

                    DFTWeeklyMonthlySummary newDFTWeeklyMonthlySummary = new DFTWeeklyMonthlySummary();
                    statusCode = newDFTWeeklyMonthlySummary.RunReport(ReportDate, reportNumber);
                    if (statusCode == 0)
                        EventLogger.WriteEntry("DFT Weekly/Monthly Combined Report successfully produced", EventLogEntryType.Information);
                    break;
                default:
                    break;
            }
            if (statusCode == 0)
            {
                string dateTimeLastRanString = ReportDate.ToShortDateString();
                string tmpKey = "LastRan";
                bool updateOK = UpdateProperty(reportNumber.ToString(), tmpKey, dateTimeLastRanString);

            }
            else
            {
                EventLogger.WriteEntry("Report failed returning code : " + statusCode, EventLogEntryType.Error);

            }

            return statusCode;
        }

        public bool OKToPrint(int reportNumber)
        {
            // class to check that it is ok to print the report
            // returns true if not run today and after 0900
            bool isOKToPrint = true;
            int testYear;
            int testMonth;
            int testDay;
            int testHrs;
            int testMin;
            int testSec;
            string currentDateTimeString;
            string dateTimeLastRanString;
            string reportFrequency;
            // get the default time
            string defaultReportTimeString;
            bool retOK = ReadProperty("0", "DefaultReportTime", out defaultReportTimeString);
            if (retOK == false || defaultReportTimeString == "")
            {
                return false;
            }
            // Get the report frequency
            retOK = ReadProperty(reportNumber.ToString(), "Frequency", out reportFrequency);
            if (retOK == false || reportFrequency == "")
            {
                return false;
            }

            testHrs = Convert.ToInt32(defaultReportTimeString.Substring(0, 2), CultureInfo.CurrentCulture);
            testMin = Convert.ToInt32(defaultReportTimeString.Substring(3, 2), CultureInfo.CurrentCulture);
            testSec = Convert.ToInt32(defaultReportTimeString.Substring(6, 2), CultureInfo.CurrentCulture);

            // get current date and time
            currentDateTime = System.DateTime.Now;
            currentDateTimeString = currentDateTime.ToString();
            DateTime comparisonDateTime = new DateTime(currentDateTime.Year, currentDateTime.Month, currentDateTime.Day, testHrs, testMin, testSec);
            // if report is weekly and this is not monday morning then return false

            if (reportFrequency == "W" && currentDateTime.DayOfWeek.ToString() != "Monday")
            {
                return false;
            }
            // if report is monthly and this is not 1st of following month then return false
            if (reportFrequency == "M" && currentDateTime.Day != 1)
            {
                return false;
            }

            // if we are not past the default time then too early so return false
            if (DateTime.Compare(currentDateTime, comparisonDateTime) < 0)
            {
                isOKToPrint = false;
            }
            else
            {
                // get the last ran date and time dd/mm/yyyy hh:mm:yy
                string tmpKey = "LastRan";
                retOK = ReadProperty(reportNumber.ToString(), tmpKey, out dateTimeLastRanString);
                if (retOK == false)
                {
                    return false;
                }
                if (dateTimeLastRanString == "")
                {
                    dateTimeLastRanString = currentDateTimeString;
                    retOK = UpdateProperty(reportNumber.ToString(), tmpKey, dateTimeLastRanString);
                    if (retOK == false)
                    {
                        EventLogger.WriteEntry("Failed updating ReportProperties data base with last run date", EventLogEntryType.Warning);
                    }

                }
                if (dateTimeLastRanString.Length < 15) // i.e. no time element
                    dateTimeLastRanString = dateTimeLastRanString.Substring(0, 10) + " 00:00:00";

                testDay = Convert.ToInt32(dateTimeLastRanString.Substring(0, 2), CultureInfo.CurrentCulture);
                testMonth = Convert.ToInt32(dateTimeLastRanString.Substring(3, 2), CultureInfo.CurrentCulture);
                testYear = Convert.ToInt32(dateTimeLastRanString.Substring(6, 4), CultureInfo.CurrentCulture);
                testHrs = Convert.ToInt32(dateTimeLastRanString.Substring(11, 2), CultureInfo.CurrentCulture);
                testMin = Convert.ToInt32(dateTimeLastRanString.Substring(14, 2), CultureInfo.CurrentCulture);
                testSec = Convert.ToInt32(dateTimeLastRanString.Substring(17, 2), CultureInfo.CurrentCulture);
                DateTime dateTimeLastRan = new DateTime(testYear, testMonth, testDay, testHrs, testMin, testSec);
                // if last ran date is not 2 days in the past then we shouldn't do it again
                if (dateTimeLastRan.Date >= currentDateTime.AddDays(-1).Date)
                {
                    isOKToPrint = false;
                }
            }
            if (isOKToPrint == true)
                ReportDate = currentDateTime.AddDays(-1); // set report day to yesterday

            return isOKToPrint;

        }
        
        public bool ReadProperty(string reportNumber, string propertyKey, out string propertyValue)
        {
            bool status = true;
            propertyValue = null;
            try
            {
                string selectString;
                selectString = "select * from ReportProperties where reportnumber = '" + reportNumber + "' and propertykey = '" + propertyKey + "'";
                mySqlConnection = new SqlConnection(propertyConnectionString);
                // Create data adapter
                SqlDataAdapter mySqlDataAdapter = new SqlDataAdapter(selectString, mySqlConnection);
                //create and populate dataset
                DataSet myDataSet = new DataSet();
                mySqlDataAdapter.Fill(myDataSet, "ReportProperties");
                foreach (DataRow myDataRow in myDataSet.Tables["ReportProperties"].Rows)
                {
                    propertyValue = myDataRow["value"].ToString();
                }
            }
            catch (Exception e)
            {
                EventLogger.WriteEntry("Error Reading Property " + reportNumber + " " + propertyKey + " " + e.ToString(), EventLogEntryType.Error);
                status = false;
            }
            mySqlConnection.Close();

            return status;
        }
        
        public bool UpdateProperty(string reportNumber, string propertyKey, string propertyValue)
        {
            bool status = true;
            try
            {
                string selectString;
                selectString = "select * from ReportProperties where reportnumber = '" + reportNumber + "' and propertykey = '" + propertyKey + "'";
                mySqlConnection = new SqlConnection(propertyConnectionString);
                // Create data adapter
                SqlDataAdapter mySqlDataAdapter = new SqlDataAdapter(selectString, mySqlConnection);
                //create and populate dataset
                DataSet myDataSet = new DataSet();
                SqlCommandBuilder mySqlCommandBuilder = new SqlCommandBuilder(mySqlDataAdapter);
                mySqlDataAdapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;
                mySqlDataAdapter.Fill(myDataSet, "ReportProperties");
                myDataSet.Tables["ReportProperties"].Rows[0]["value"] = propertyValue;
                mySqlDataAdapter.Update(myDataSet, "ReportProperties");
                mySqlConnection.Close();
            }
            catch (Exception e)
            {
                EventLogger.WriteEntry("Failed updating Last run date : " + e.Message, EventLogEntryType.Error);
                status = false;
            }

            return status;
        }
        
        #region Properties
        /// <summary>
        /// 
        /// </summary>
        public DateTime ReportDate
        {
            get
            {
                return reportDate;
            }
            set
            {
                reportDate = value;
            }
        }

        #endregion

    }

}
