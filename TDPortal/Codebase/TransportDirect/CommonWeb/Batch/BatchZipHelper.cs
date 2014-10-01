using System;
using System.Collections.Generic;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Xsl;

using ICSharpCode.SharpZipLib.Zip;
using TransportDirect.Common;
using TransportDirect.Common.DatabaseInfrastructure;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.CommonWeb.Helpers;
using TransportDirect.UserPortal.LocationService;

namespace TransportDirect.CommonWeb.Batch
{
    public static class BatchZipHelper
    {
        private const string RES_FAILEDBATCH = "BatchJourneyPlanner.FailedBatch";
        private const string RES_BATCHDELETED = "BatchJourneyPlanner.BatchDeleted";
        private const string RES_RETRIEVALFAILURE = "BatchJourneyPlanner.RetrievalFailure";

        public static string CreateZip(string batchNo, string userName)
        {
            // pad batch number out to 6 figures
            string batchId = batchNo;
            while (batchId.Length < 6)
            {
                batchId = "0" + batchId;
            }

            bool incStats = false;
            bool incDets = false;
            bool incPT = false;
            bool incCar = false;
            bool incCycle = false;
            bool convertToRtf = false;

            string landingPage = Properties.Current["BatchLandingPage"];

            string folderName = Properties.Current["BatchJourneyPlannerFileStorage"];
            folderName += @"\" + batchId;
            string csvFileName = folderName + @"\" + batchId + "_Statistics.csv";
            string xmlFileName = folderName + @"\" + batchId + "_Details.xml";

            if (Directory.Exists(folderName))
            {
                // Clear out the directory because something
                // must have gone awry otherwise the zip would
                // already be in the database
                Directory.Delete(folderName);
                //DirectoryInfo output = new DirectoryInfo(folderName);
                //foreach (FileInfo file in output.GetFiles())
                //{
                //    file.Delete();
                //}

                //// just in case
                //foreach (DirectoryInfo dir in output.GetDirectories())
                //{
                //    dir.Delete();
                //}
            }

            Directory.CreateDirectory(folderName);

            //Use the SQL Helper class
            using (SqlHelper sqlHelper = new SqlHelper())
            {
                // Create hashtables containing parameters and data types for the stored procs
                Hashtable parameterValues = new Hashtable(1);
                Hashtable parameterTypes = new Hashtable(1);

                // The Batch Id
                parameterValues.Add("@BatchId", int.Parse(batchId));
                parameterTypes.Add("@BatchId", SqlDbType.Int);

                // get the summary info
                sqlHelper.ConnOpen(SqlHelperDatabase.BatchJourneyPlannerDB);
                SqlDataReader reader = sqlHelper.GetReader("GetBatchSummary", parameterValues, parameterTypes);

                string message = string.Empty;
                bool rtfIncludeImage = false;

                if (reader.HasRows)
                {
                    reader.Read();
                    if (reader.GetInt32(1) == 4)
                    {
                        message = RES_FAILEDBATCH;
                    }
                    else if (reader[0].ToString() != string.Empty)
                    {
                        message = RES_BATCHDELETED;
                    }

                    incStats = reader.GetBoolean(2);
                    incDets = reader.GetBoolean(3);
                    incPT = reader.GetBoolean(4);
                    incCar = reader.GetBoolean(5);
                    incCycle = reader.GetBoolean(6);
                    convertToRtf = reader.GetBoolean(7);

                    int requests = reader.GetInt32(10);
                    if (requests <= int.Parse(Properties.Current["BatchMaxLinesWithOutputImage"]))
                    {
                        rtfIncludeImage = true;
                    }

                    if (message == string.Empty)
                    {
                        StreamWriter statistics = null;
                        StreamWriter xmlDetails = null;

                        if (incStats)
                        {
                            // Create the csv file
                            FileStream fs = File.Create(csvFileName);
                            statistics = new StreamWriter(fs);
                            statistics.WriteLine("JourneyID,Status,Message,PTOutwardJourneyTime,PTOutwardChanges,PTOutwardModes,PTOutwardCO2(kg),PTReturnJourneyTime,PTReturnChanges,PTReturnModes,PTReturnCO2(kg),CarOutwardJourneyTime,CarOutwardCO2(kg),CarOutwardDistance(miles),CarReturnJourneyTime,CarReturnCO2(kg),CarReturnDistance(miles),CycleOutwardJourneyTime,CycleOutwardDistance(miles),CycleReturnJourneyTime,CycleReturnDistance(miles),PTCarURL,CycleURL");
                        }

                        if (incPT && incDets)
                        {
                            // Create the XML output
                            FileStream fs = File.Create(xmlFileName);
                            xmlDetails = new StreamWriter(fs);
                            xmlDetails.WriteLine("<BatchRequest>");
                            string line = "<DateLoaded>" + reader.GetDateTime(8).ToString() + "</DateLoaded>";
                            xmlDetails.WriteLine(line);
                            line = "<DateCompleted>" + reader.GetDateTime(9) + "</DateCompleted>";
                            xmlDetails.WriteLine(line);
                            line = "<NumberRequests>" + reader.GetInt32(10).ToString() + "</NumberRequests>";
                            xmlDetails.WriteLine(line);
                            line = "<NumberOfJourneys>" + reader.GetInt32(11).ToString() + "</NumberOfJourneys>";
                            xmlDetails.WriteLine(line);
                            line = "<NumberOfPartialJourneys>" + reader.GetInt32(15).ToString() + "</NumberOfPartialJourneys>";
                            xmlDetails.WriteLine(line);
                            line = "<NoJourneys>" + reader.GetInt32(12).ToString() + "</NoJourneys>";
                            xmlDetails.WriteLine(line);
                            line = "<NumberValidationErrors>" + reader.GetInt32(13).ToString() + "</NumberValidationErrors>";
                            xmlDetails.WriteLine(line);
                            line = "<UserEmail>" + userName + "</UserEmail>";
                            xmlDetails.WriteLine(line);
                            line = "<BatchId>" + reader.GetString(14) + "</BatchId>";
                            xmlDetails.WriteLine(line);
                        }

                        // Create hashtables containing parameters and data types for the stored procs
                        Hashtable parameterValues2 = new Hashtable(1);
                        Hashtable parameterTypes2 = new Hashtable(1);

                        // The Batch Id
                        parameterValues2.Add("@BatchId", int.Parse(batchId));
                        parameterTypes2.Add("@BatchId", SqlDbType.Int);

                        //Use the SQL Helper class
                        using (SqlHelper sqlHelper2 = new SqlHelper())
                        {
                            // Add the user
                            sqlHelper2.ConnOpen(SqlHelperDatabase.BatchJourneyPlannerDB);
                            SqlDataReader readerDetails = sqlHelper2.GetReader("GetBatchResults", parameterValues, parameterTypes);

                            if (readerDetails.HasRows)
                            {
                                while (readerDetails.Read())
                                {
                                    #region stats line
                                    if (incStats)
                                    {
                                        string line = string.Empty;
                                        if (!readerDetails.IsDBNull(0) && readerDetails.GetString(0).Length > 0)
                                        {
                                            line = readerDetails.GetString(0);
                                        }
                                        string errorString = string.Empty;
                                        if (!readerDetails.IsDBNull(1) && readerDetails.GetString(1).Length > 0)
                                        {
                                            // legacy error string
                                            errorString = readerDetails.GetString(1);
                                        }
                                        else
                                        {
                                            if (!readerDetails.IsDBNull(25) && readerDetails.GetString(25).Length > 0)
                                                errorString += readerDetails.GetString(25);
                                            if (!readerDetails.IsDBNull(26) && readerDetails.GetString(26).Length > 0)
                                                errorString += readerDetails.GetString(26);
                                            if (!readerDetails.IsDBNull(27) && readerDetails.GetString(27).Length > 0)
                                                errorString += readerDetails.GetString(27);
                                            if (!readerDetails.IsDBNull(28) && readerDetails.GetString(28).Length > 0)
                                                errorString += readerDetails.GetString(28);
                                            if (!readerDetails.IsDBNull(29) && readerDetails.GetString(29).Length > 0)
                                                errorString += readerDetails.GetString(29);
                                            if (!readerDetails.IsDBNull(30) && readerDetails.GetString(30).Length > 0)
                                                errorString += readerDetails.GetString(30);
                                        }

                                        if (readerDetails.GetInt32(22) == 4)
                                        {
                                            line += ",V,";
                                            line += errorString + ",,,,,,,,,,,,,,,,,,,"; // no other results posssible
                                        }
                                        else if (readerDetails.GetInt32(22) == 3)
                                        {
                                            line += ",E,";
                                            line += errorString + ",,,,,,,,,,,,,,,,,,,"; // no other results posssible
                                        }
                                        else
                                        {
                                            if (readerDetails.GetInt32(8) == 4 || readerDetails.GetInt32(9) == 4 || readerDetails.GetInt32(10) == 4 || readerDetails.GetInt32(11) == 4 || readerDetails.GetInt32(12) == 4 || readerDetails.GetInt32(13) == 4)
                                            {
                                                line += ",V,";
                                            }
                                            else if (readerDetails.GetInt32(22) == 5)
                                            {
                                                line += ",P,";
                                            }
                                            else
                                            {
                                                line += ",OK,";
                                            }

                                            line += errorString + ",";
                                            string ptSummary = readerDetails.GetString(2);
                                            string doorToDoorURL = ptSummary.Split(',')[8];
                                            if (doorToDoorURL.Length > 0)
                                            {
                                                doorToDoorURL = landingPage + doorToDoorURL.Replace("&amp;", "&");
                                            }
                                            ptSummary = ptSummary.Substring(0, ptSummary.LastIndexOf(',') + 1);
                                            line += ptSummary;
                                            line += readerDetails.GetString(4);
                                            string cycleSummary = readerDetails.GetString(6);
                                            string cycleURL = cycleSummary.Split(',')[4];
                                            if (cycleURL.Length > 0)
                                            {
                                                cycleURL = landingPage + cycleURL.Replace("&amp;", "&");
                                            }
                                            cycleSummary = cycleSummary.Substring(0, cycleSummary.LastIndexOf(',') + 1);
                                            line += cycleSummary;
                                            line += doorToDoorURL;
                                            line += "," + cycleURL;
                                        }
                                        statistics.WriteLine(line);
                                    }
                                    #endregion

                                    if (incPT && incDets)
                                    {
                                        if (readerDetails.GetInt32(22) != 4)
                                        {
                                            // Output an XML journey result
                                            StringBuilder journeyRecord = new StringBuilder();
                                            journeyRecord.AppendLine();
                                            journeyRecord.AppendLine("<JourneyResult>");
                                            journeyRecord.AppendLine("<JourneyID>" + readerDetails[0].ToString() + "</JourneyID>");

                                            // Deal with co-ordinate and Naptan origins
                                            string originName = readerDetails[14].ToString();
                                            switch (readerDetails[23].ToString())
                                            {
                                                case "c":
                                                    if (originName.Contains("|"))
                                                    {
                                                        originName = "OS Grid Reference " + originName.Split('|')[0] + "," + originName.Split('|')[1];
                                                    }
                                                    break;
                                                case "n":
                                                    NaptanCacheEntry entry = NaptanLookup.Get(originName, "Naptan");
                                                    if (entry.Found && entry.Description != null && entry.Description.Length > 0)
                                                    {
                                                        originName = entry.Description;
                                                    }
                                                    break;
                                            }
                                            journeyRecord.AppendLine("<JourneyOriginName>" + originName + "</JourneyOriginName>");

                                            // Deal with co-ordinate and Naptan destinations
                                            string destName = readerDetails[15].ToString();
                                            switch (readerDetails[24].ToString())
                                            {
                                                case "c":
                                                    if (destName.Contains("|"))
                                                    {
                                                        destName = "OS Grid Reference " + destName.Split('|')[0] + "," + destName.Split('|')[1];
                                                    }
                                                    break;
                                                case "n":
                                                    NaptanCacheEntry entry = NaptanLookup.Get(destName, "Naptan");
                                                    if (entry.Found && entry.Description != null && entry.Description.Length > 0)
                                                    {
                                                        destName = entry.Description;
                                                    }
                                                    break;
                                            }
                                            journeyRecord.AppendLine("<JourneyDestinationName>" + destName + "</JourneyDestinationName>");

                                            journeyRecord.AppendLine("<PTOutwardMessage>" + readerDetails[25].ToString() + "</PTOutwardMessage>");
                                            journeyRecord.AppendLine("<CarOutwardMessage>" + readerDetails[27].ToString() + "</CarOutwardMessage>");
                                            journeyRecord.AppendLine("<CycleOutwardMessage>" + readerDetails[26].ToString() + "</CycleOutwardMessage>");
                                            journeyRecord.AppendLine("<OutwardDate>" + readerDetails[16].ToString().Substring(0, 10) + "</OutwardDate>");
                                            journeyRecord.AppendLine("<OutwardTime>" + readerDetails[17].ToString().Substring(0, 5) + "</OutwardTime>");
                                            journeyRecord.AppendLine("<OutwardArrDep>" + readerDetails[18].ToString() + "</OutwardArrDep>");

                                            if (readerDetails[21].ToString() != string.Empty)
                                            {
                                                journeyRecord.AppendLine("<PTReturnMessage>" + readerDetails[28].ToString() + "</PTReturnMessage>");
                                                journeyRecord.AppendLine("<CarReturnMessage>" + readerDetails[30].ToString() + "</CarReturnMessage>");
                                                journeyRecord.AppendLine("<CycleReturnMessage>" + readerDetails[29].ToString() + "</CycleReturnMessage>");
                                                journeyRecord.AppendLine("<ReturnDate>" + readerDetails[19].ToString().Substring(0, 10) + "</ReturnDate>");
                                                journeyRecord.AppendLine("<ReturnTime>" + readerDetails[20].ToString().Substring(0, 5) + "</ReturnTime>");
                                                journeyRecord.AppendLine("<ReturnArrDep>" + readerDetails[21].ToString() + "</ReturnArrDep>");
                                            }

                                            string status;
                                            if (readerDetails.GetInt32(8) == 2 && readerDetails.GetInt32(9) == 2
                                                && readerDetails.GetInt32(10) == 2 && readerDetails.GetInt32(11) == 2
                                                && readerDetails.GetInt32(12) == 2 && readerDetails.GetInt32(13) == 2)
                                            {
                                                status = "OK";
                                            }
                                            else
                                            {
                                                status = "IncompleteResults";
                                            }
                                            journeyRecord.AppendLine("<JourneyStatus>" + status + "</JourneyStatus>");

                                            string doorToDoorURL = readerDetails[2].ToString().Split(',')[8];
                                            if (readerDetails.GetInt32(8) == 2)
                                            {
                                                journeyRecord.AppendLine("<PtOutwardJourneyTime>" + readerDetails[2].ToString().Split(',')[0] + "</PtOutwardJourneyTime>");
                                                journeyRecord.AppendLine("<PtOutwardJourneyChanges>" + readerDetails[2].ToString().Split(',')[1] + "</PtOutwardJourneyChanges>");
                                                journeyRecord.AppendLine("<PtOutwardJourneyModes>" + readerDetails[2].ToString().Split(',')[2] + "</PtOutwardJourneyModes>");
                                                journeyRecord.AppendLine("<PtOutwardJourneyCO2>" + readerDetails[2].ToString().Split(',')[3] + "</PtOutwardJourneyCO2>");
                                                if (doorToDoorURL.Length > 0)
                                                {
                                                    doorToDoorURL = landingPage + doorToDoorURL;
                                                }
                                                journeyRecord.AppendLine("<PtJourneyLandingURL>" + doorToDoorURL + "</PtJourneyLandingURL>");
                                            }
                                            else if (doorToDoorURL.Length > 0)
                                            {
                                                doorToDoorURL = landingPage + doorToDoorURL;
                                                journeyRecord.AppendLine("<PtJourneyLandingURL>" + doorToDoorURL + "</PtJourneyLandingURL>");
                                            }

                                            if (readerDetails.GetInt32(9) == 2)
                                            {
                                                journeyRecord.AppendLine("<PtReturnJourneyTime>" + readerDetails[2].ToString().Split(',')[4] + "</PtReturnJourneyTime>");
                                                journeyRecord.AppendLine("<PtReturnJourneyChanges>" + readerDetails[2].ToString().Split(',')[5] + "</PtReturnJourneyChanges>");
                                                journeyRecord.AppendLine("<PtReturnJourneyModes>" + readerDetails[2].ToString().Split(',')[6] + "</PtReturnJourneyModes>");
                                                journeyRecord.AppendLine("<PtReturnJourneyCO2>" + readerDetails[2].ToString().Split(',')[7] + "</PtReturnJourneyCO2>");
                                            }

                                            if (incCar)
                                            {
                                                if (readerDetails.GetInt32(10) == 2)
                                                {
                                                    journeyRecord.AppendLine("<CarOutwardJourneyTime>" + readerDetails[4].ToString().Split(',')[0] + "</CarOutwardJourneyTime>");
                                                    journeyRecord.AppendLine("<CarOutwardJourneyCO2>" + readerDetails[4].ToString().Split(',')[1] + "</CarOutwardJourneyCO2>");
                                                    journeyRecord.AppendLine("<CarOutwardJourneyDistance>" + readerDetails[4].ToString().Split(',')[2] + "</CarOutwardJourneyDistance>");
                                                }

                                                if (readerDetails.GetInt32(11) == 2)
                                                {
                                                    journeyRecord.AppendLine("<CarReturnJourneyTime>" + readerDetails[4].ToString().Split(',')[3] + "</CarReturnJourneyTime>");
                                                    journeyRecord.AppendLine("<CarReturnJourneyCO2>" + readerDetails[4].ToString().Split(',')[4] + "</CarReturnJourneyCO2>");
                                                    journeyRecord.AppendLine("<CarReturnJourneyDistance>" + readerDetails[4].ToString().Split(',')[5] + "</CarReturnJourneyDistance>");
                                                }
                                            }

                                            if (incCycle)
                                            {
                                                string cycleURL = readerDetails[6].ToString().Split(',')[4];
                                                if (readerDetails.GetInt32(12) == 2)
                                                {
                                                    journeyRecord.AppendLine("<CycleOutwardJourneyTime>" + readerDetails[6].ToString().Split(',')[0] + "</CycleOutwardJourneyTime>");
                                                    journeyRecord.AppendLine("<CycleOutwardDistance>" + readerDetails[6].ToString().Split(',')[1] + "</CycleOutwardDistance>");
                                                    if (cycleURL.Length > 0)
                                                    {
                                                        cycleURL = landingPage + cycleURL;
                                                    }
                                                    journeyRecord.AppendLine("<CycleLandingURL>" + cycleURL + "</CycleLandingURL>");
                                                }
                                                else if (cycleURL.Length > 0)
                                                {
                                                    cycleURL = landingPage + cycleURL;
                                                    journeyRecord.AppendLine("<CycleLandingURL>" + cycleURL + "</CycleLandingURL>");
                                                }


                                                if (readerDetails.GetInt32(13) == 2)
                                                {
                                                    journeyRecord.AppendLine("<CycleReturnJourneyTime>" + readerDetails[6].ToString().Split(',')[2] + "</CycleReturnJourneyTime>");
                                                    journeyRecord.AppendLine("<CycleReturnDistance>" + readerDetails[6].ToString().Split(',')[3] + "</CycleReturnDistance>");
                                                }
                                            }

                                            if (readerDetails[3].ToString() != string.Empty)
                                            {
                                                XmlDocument doc = new XmlDocument();
                                                doc.LoadXml(readerDetails.GetString(3));
                                                journeyRecord.AppendLine(doc.DocumentElement.InnerXml);
                                            }

                                            journeyRecord.AppendLine("</JourneyResult>");
                                            xmlDetails.Write(journeyRecord.ToString());

                                            // Journey rtf file
                                            if (convertToRtf)
                                            {
                                                XmlDocument journeyResult = new XmlDocument();
                                                using (XmlTextWriter xtw = new XmlTextWriter(folderName + "\\" + readerDetails[0].ToString() + ".rtf", System.Text.Encoding.Default))
                                                {
                                                    journeyResult.LoadXml(journeyRecord.ToString());
                                                    XslCompiledTransform transform = new XslCompiledTransform();
                                                    if (Properties.Current.ApplicationID == "BatchJourneyPlannerService")
                                                    {
                                                        transform.Load(AppDomain.CurrentDomain.BaseDirectory + Properties.Current["BatchJourneyPlannerJourneyTransform"]);
                                                    }
                                                    else
                                                    {
                                                        transform.Load(Properties.Current["BatchJourneyPlannerJourneyTransform"]);
                                                    }
                                                    XsltArgumentList xslArg = new XsltArgumentList();
                                                    if (rtfIncludeImage)
                                                    {
                                                        xslArg.AddParam("OutputType", "", "1");
                                                    }
                                                    else
                                                    {
                                                        xslArg.AddParam("OutputType", "", "2");
                                                    }
                                                    transform.Transform(journeyResult, xslArg, xtw);
                                                    xtw.Close();
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                            readerDetails.Close();
                            sqlHelper2.ConnClose();
                        }

                        if (incStats)
                        {
                            statistics.Close();
                        }

                        if (incPT && incDets)
                        {
                            xmlDetails.WriteLine("</BatchRequest>");
                            xmlDetails.Close();

                            if (convertToRtf)
                            {
                                // Create RTF for this request using a transform and the
                                // xml details file create
                                using (XmlTextWriter xtw = new XmlTextWriter(folderName + "\\_BatchSummary.rtf", System.Text.Encoding.Default))
                                {
                                    XmlDocument detailsDoc = new XmlDocument();
                                    detailsDoc.Load(xmlFileName);
                                    XslCompiledTransform transform = new XslCompiledTransform();
                                    if (Properties.Current.ApplicationID == "BatchJourneyPlannerService")
                                    {
                                        transform.Load(AppDomain.CurrentDomain.BaseDirectory + Properties.Current["BatchJourneyPlannerSummaryTransform"]);
                                    }
                                    else
                                    {
                                        transform.Load(Properties.Current["BatchJourneyPlannerSummaryTransform"]);
                                    }
                                    transform.Transform(detailsDoc, null, xtw);
                                    xtw.Close();
                                }
                                File.Delete(xmlFileName);
                            }
                        }
                    }
                    else
                    {
                        return message;
                    }
                }
                else
                {
                    return RES_RETRIEVALFAILURE;
                }

                reader.Close();
                sqlHelper.ConnClose();
            }

            // Zip with ICSharpCode.SharpZipLib.Zip
            using (MemoryStream ms = new MemoryStream())
            {
                using (ZipOutputStream zipStream = new ZipOutputStream(ms))
                {
                    zipStream.SetLevel(3);

                    foreach (string fileName in Directory.GetFiles(folderName))
                    {
                        FileStream fs = File.OpenRead(fileName);
                        string fileNameNoPath = fileName.Substring(fileName.LastIndexOf(@"\"), fileName.Length - fileName.LastIndexOf(@"\"));
                        ZipEntry entry = new ZipEntry(ZipEntry.CleanName(fileNameNoPath));
                        entry.Size = fs.Length;
                        zipStream.PutNextEntry(entry);

                        byte[] buffer = new byte[fs.Length];
                        fs.Read(buffer, 0, (int)fs.Length);
                        zipStream.Write(buffer, 0, buffer.Length);
                        fs.Close();
                    }

                    zipStream.Close();
                    byte[] theZip = ms.ToArray();

                    using (SqlHelper sqlHelper = new SqlHelper())
                    {
                        // Create hashtables containing parameters and data types for the stored procs
                        Hashtable parameterValues = new Hashtable(1);
                        Hashtable parameterTypes = new Hashtable(1);

                        // The Batch Id
                        parameterValues.Add("@BatchId", int.Parse(batchId));
                        parameterTypes.Add("@BatchId", SqlDbType.Int);

                        // The Zip
                        parameterValues.Add("@ZipFile", theZip);
                        parameterTypes.Add("@ZipFile", SqlDbType.VarBinary);

                        // Set the zip
                        sqlHelper.ConnOpen(SqlHelperDatabase.BatchJourneyPlannerDBLongTimeout);
                        sqlHelper.Execute("SetZip", parameterValues, parameterTypes);
                    }
                }
            }

            // Remove the directory as it's no longer required
            Directory.Delete(folderName, true);

            // Update the batch to say it's compelete
            using (SqlHelper sqlHelper = new SqlHelper())
            {
                // Create hashtables containing parameters and data types for the stored procs
                Hashtable parameterValues = new Hashtable(1);
                Hashtable parameterTypes = new Hashtable(1);

                // The Batch Id
                parameterValues.Add("@BatchId", int.Parse(batchId));
                parameterTypes.Add("@BatchId", SqlDbType.Int);

                // Get the zip
                sqlHelper.ConnOpen(SqlHelperDatabase.BatchJourneyPlannerDB);
                sqlHelper.Execute("FinishBatch", parameterValues, parameterTypes);
            }

            return string.Empty;
        }
    }
}
