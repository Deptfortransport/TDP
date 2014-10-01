// *********************************************** 
// NAME                 : BatchProcessor.cs 
// AUTHOR               : David Lane 
// DATE CREATED         : 28/02/2012 
// DESCRIPTION			: Initiates main service thread and polls for results.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/BatchJourneyPlannerService/BatchProcessor.cs-arc  $
//
//   Rev 1.4   Mar 22 2013 10:47:28   dlane
//CCN0648c - Super Batch enhancements
//Resolution for 5907: CCN0648c - "Super Batch" enhancements
//
//   Rev 1.3   Feb 28 2012 15:52:24   DLane
//Batch updates and code commentry
//Resolution for 5787: Batch Journey Planner
//

using System;
using System.Collections.Generic;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Logger = System.Diagnostics.Trace;
using System.Text;
using System.Threading;
using TransportDirect.Common.DatabaseInfrastructure;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.Logging;
using TransportDirect.CommonWeb.Batch;

namespace BatchJourneyPlannerService
{
    public class BatchProcessor
    {
        private bool disposed = false;
        private bool stopping = false;
        private bool stopped = false;

        /// <summary>
		/// Class destructor.
		/// </summary>
		~BatchProcessor()      
		{
			Dispose(false);
		}

        /// <summary>
        /// Disposes of resources. 
        /// Can be called by clients (via Dispose()) or runtime (via destructor).
        /// </summary>
        /// <param name="disposing">
        /// True when called by clients.
        /// False when called by runtime.
        /// </param>
        public void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called.
            if (!this.disposed)
            {
                if (disposing)
                {
                    // Dispose of any managed resources:
                    StopProcessors();
                }

                // Dispose of any unmanaged resources:
            }

            this.disposed = true;
        }
        
        /// <summary>
		/// Default constructor
		/// </summary>
		public BatchProcessor()
		{
		}

		/// <summary>
		/// Starts batch processing.
		/// </summary>
		public void Run()
		{
            Thread mainThread = new Thread(new ThreadStart(ManageThreads));
            mainThread.Start();
        }

        /// <summary>
        /// Starts batch processing. Test method.
        /// </summary>
        public void Run(IPropertyProvider props)
        {
            Thread mainThread = new Thread(new ThreadStart(ManageThreads));
            mainThread.Start();
        }

        /// <summary>
        /// Method run by the main thread - loops looking for work and starts worker threads
        /// </summary>
        private void ManageThreads()
        {
            // Loop until thread gets aborted
            try
            {
                while(true)
                {
                    try
                    {
                        // Check we're not stopping
                        if (stopping)
                        {
                            stopped = true;
                            return;
                        }

                        // properties are already validated, check how service should be running
                        IPropertyProvider properties = Properties.Current;
                        List<string> runningDays = new List<string>(properties[Keys.BatchProcessingRunningDays].Split(';'));

                        if (runningDays.Contains(DateTime.Now.DayOfWeek.ToString()))
                        {
                            // See if we're in a window
                            bool inWindow = false;
                            string dateStub = DateTime.Now.ToString("yyyy-MM-dd") + " ";

                            for (int i = 1; i <= int.Parse(properties[Keys.BatchProcessingWindowCount]); i++)
                            {
                                DateTime windowStart = DateTime.Parse(dateStub + properties[string.Format(Keys.BatchProcessingWindowNStart, i.ToString())]);
                                DateTime windowEnd = DateTime.Parse(dateStub + properties[string.Format(Keys.BatchProcessingWindowNEnd, i.ToString())]);

                                if ((windowStart.CompareTo(DateTime.Now) < 0) && (windowEnd.CompareTo(DateTime.Now) > 0))
                                {
                                    inWindow = true;
                                }
                            }

                            int pollingInterval;
                            int concurrentThreads;

                            if (inWindow)
                            {
                                pollingInterval = int.Parse(properties[Keys.BatchProcessingInWindowIntervalSeconds]);
                                concurrentThreads = int.Parse(properties[Keys.BatchProcessingInWindowConcurrentRequests]);
                            }
                            else
                            {
                                pollingInterval = int.Parse(properties[Keys.BatchProcessingOutWindowIntervalSeconds]);
                                concurrentThreads = int.Parse(properties[Keys.BatchProcessingOutWindowConcurrentRequests]);
                            }

                            if (concurrentThreads < 1)
                            {
                                // sleep for 10 secs
                                Thread.Sleep(10000);
                            }
                            else
                            {
                                PollForRequests(concurrentThreads);
                                Thread.Sleep(pollingInterval * 1000);
                            }
                        }
                        else
                        {
                            // sleep for 10 secs
                            Thread.Sleep(10000);
                        }
                    }
                    catch (ThreadAbortException te)
                    {
                        throw te;
                    }
                    catch (Exception ex) { string text = ex.ToString(); }
                }
            }
            catch (ThreadAbortException) { }
        }

        /// <summary>
        /// Search for batch details to process
        /// </summary>
        /// <param name="numThreads">number of results / threads required</param>
        private void PollForRequests(int numThreads)
        {
            // Create hashtables containing parameters and data types for the stored procs
            Hashtable parameterValues = new Hashtable(3);
            Hashtable parameterTypes = new Hashtable(3);

            // The batch processing processor id
            parameterValues.Add("@ProcessorId", ConfigurationManager.AppSettings["ProcessorId"]);
            parameterTypes.Add("@ProcessorId", SqlDbType.NVarChar);

            // The number of records
            parameterValues.Add("@NumberRequests", numThreads);
            parameterTypes.Add("@NumberRequests", SqlDbType.Int);

            // Max rows
            parameterValues.Add("@BatchMaxLineLimit", int.Parse(ConfigurationManager.AppSettings["BatchMaxLineLimit"]));
            parameterTypes.Add("@BatchMaxLineLimit", SqlDbType.Int);

            // Min rows
            parameterValues.Add("@BatchMinLineLimit", int.Parse(ConfigurationManager.AppSettings["BatchMinLineLimit"]));
            parameterTypes.Add("@BatchMinLineLimit", SqlDbType.Int);

            // The RETURN VALUE
            SqlParameter paramReturnValue = new SqlParameter("RETURN_VALUE", SqlDbType.Int);
            paramReturnValue.Direction = ParameterDirection.ReturnValue;

            //Use the SQL Helper class
            using (SqlHelper sqlHelper = new SqlHelper())
            {
                sqlHelper.ConnOpen(SqlHelperDatabase.BatchJourneyPlannerDBLongTimeout);
                SqlDataReader reader = sqlHelper.GetReader("GetBatchRequestDetailsForProcessing", parameterValues, parameterTypes, paramReturnValue);

                // Start a thread for each detail record
                List<EventWaitHandle> threadFinishEvents = new List<EventWaitHandle>();
                BatchPreProcessValidation validBatchLine = new BatchPreProcessValidation();
               
                bool[] readerPreValidationArray = new bool[18];
                for (int i = 0; i < 18; i++) readerPreValidationArray[i] = true;
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        EventWaitHandle threadFinish = new EventWaitHandle(false, EventResetMode.ManualReset);
                        threadFinishEvents.Add(threadFinish);

                        BatchPreProcessValidation Validator = new BatchPreProcessValidation();
                        readerPreValidationArray = Validator.ValidateReader(reader);

                        ProcessRequest request = new ProcessRequest(threadFinish,
                                                                    reader.GetInt32(0),
                                                                    reader.GetBoolean(1),
                                                                    reader.GetBoolean(2),
                                                                    reader.GetBoolean(3),
                                                                    reader.GetBoolean(4),
                                                                    reader.GetBoolean(5),
                                                                    reader.GetString(7),
                                                                    reader.GetString(8),
                                                                    reader.GetString(9),
                                                                    reader.GetString(10),
                                                                    reader.GetString(11),
                                                                    reader.GetDateTime(12),
                                                                    reader.GetTimeSpan(13),
                                                                    reader.GetString(14),
                                                                    reader[15].ToString() == string.Empty ? DateTime.MinValue : reader.GetDateTime(15),
                                                                    reader[16].ToString() == string.Empty ? TimeSpan.MinValue : reader.GetTimeSpan(16),
                                                                    reader[17].ToString() == string.Empty ? "" : reader.GetString(17),
                                                                    readerPreValidationArray);

                        Thread requestThread = new Thread(new ThreadStart(request.Process));
                        requestThread.Start();
                    }

                    reader.Close();
                    sqlHelper.ConnClose();

                    // Wait for the threads to finish
                    if (threadFinishEvents.Count > 0)
                    {
                        Mutex.WaitAll(threadFinishEvents.ToArray());
                    }
                }
                else
                {
                    reader.Close();
                    sqlHelper.ConnClose();
                    int returnValue = int.Parse(paramReturnValue.Value.ToString());

                    if (0 != returnValue)
                    {
                        // Create hashtables containing parameters and data types for the stored procs
                        parameterValues = new Hashtable(1);
                        parameterTypes = new Hashtable(1);

                        // The batch processing processor id
                        parameterValues.Add("@BatchId", returnValue);
                        parameterTypes.Add("@BatchId", SqlDbType.Int);

                        string emailToAddress = string.Empty;

                        //Use the SQL Helper class
                        using (SqlHelper sqlHelper2 = new SqlHelper())
                        {
                            sqlHelper2.ConnOpen(SqlHelperDatabase.BatchJourneyPlannerDBLongTimeout);
                            SqlDataReader reader2 = sqlHelper2.GetReader("GetEmailAddressForBatch", parameterValues, parameterTypes);

                            if (reader2.HasRows)
                            {
                                reader2.Read();
                                emailToAddress = reader2.GetString(0);
                            }

                            reader2.Close();
                            sqlHelper2.ConnClose();
                        }

                        // Create the download zip
                        CreateDownloadZip(returnValue, emailToAddress);

                        // Email the user that the batch is done.
                        SendCompletionEmail(returnValue, emailToAddress);
                    }
                }
            }
        }

        /// <summary>
        /// Creates and stores the zip for the user to download
        /// </summary>
        private void CreateDownloadZip(int batchId, string email)
        {
            string error = BatchZipHelper.CreateZip(batchId.ToString(), email);
        }

        /// <summary>
        /// Informs the user the batch is complete
        /// </summary>
        /// <param name="batchNo"></param>
        private void SendCompletionEmail(int batchNo, string emailToAddress)
        {
            // Compose email details
            string batchId = batchNo.ToString();
            while (batchId.Length < 6)
            {
                batchId = "0" + batchId;
            }
            string emailFromAddress = "noreply@transportdirect.info";
            string emailBody = @"Transport Direct has completed processing your request {0}. Please log on to Transport Direct to download the results.

Yours sincerely,
Tom Herring
Business Service Manager
Transport Direct";
            emailBody = string.Format(emailBody, batchId);
            string emailSubject = string.Format("Transport Direct Batch Request {0}", batchId);

            //Create Custom Event
            CustomEmailEvent mailattachmentJourneyDetailEvent =
                new CustomEmailEvent(emailFromAddress, emailToAddress, emailBody, emailSubject);
            //Add to listener
            Logger.Write(mailattachmentJourneyDetailEvent);
        }

        /// <summary>
        /// Stops the service once the worker threads are complete
        /// </summary>
        private void StopProcessors()
        {
            stopping = true;

            while (!stopped)
            {
                Thread.Sleep(500);
            }
        }
    }
}
