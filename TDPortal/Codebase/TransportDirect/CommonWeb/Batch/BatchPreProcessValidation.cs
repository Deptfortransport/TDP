// *********************************************** 
// NAME             : BatchPreProcessValidation.cs      
// AUTHOR           : Phil Scott
// DATE CREATED     : 21 Jun 2013
// DESCRIPTION      : Pre process validator - ensures line is suitable to go forward for processing
// ************************************************
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/CommonWeb/Batch/BatchPreProcessValidation.cs-arc  $
//
//
//   Rev 1.0   Jun 21 2013 10:19:14   pscott
//Initial revision.
// Batch Journey Planner
//
// 
using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
namespace TransportDirect.CommonWeb.Batch
{
    /// <summary>
    /// Batch pre validator class
    /// </summary>
    
    public class BatchPreProcessValidation
    {

        public bool [] ValidateReader(SqlDataReader reader) 
        {

            bool [] readerItemValidArray = new bool[18];
            int requestId;
            bool incStatistics;
            bool incDetails;
            bool incPT;
            bool incCar;
            bool incCycle;
            string uniqueId;
            string originType;
            string origin;
            string destType; 
            string dest;
            DateTime outDate;
            TimeSpan outTime;
            string outArrDep;
            DateTime retDate;
            TimeSpan retTime;
            string retArrDep;

            string pattern;
            Regex regex = new Regex("");
            Match match;

            // set all validations on the line to true
            for(int i=0;i<18;i++)
            {
                readerItemValidArray[i] = true;
            }

            //-----------------------------------------------------------------
            // Validate Requestid
            try
            {
                requestId = reader.GetInt32(0);

                if (requestId <= 0)
                {
                    readerItemValidArray[0] = false;
                }
            }
            catch
            {
                readerItemValidArray[0] = false;
            }

            //-----------------------------------------------------------------
            // Validate incStatistics
            try
            {
                incStatistics = reader.GetBoolean(1);
            }
            catch
            {
                readerItemValidArray[1] = false;
            }

            //-----------------------------------------------------------------
            // Validate incDetails
            try
            {
                incDetails = reader.GetBoolean(2);
            }
            catch
            {
                readerItemValidArray[2] = false;
            }

            //-----------------------------------------------------------------
            // Validate incPT            
            try
            {
                incPT = reader.GetBoolean(3);
            }
            catch
            {
                readerItemValidArray[3] = false;
            }

            //-----------------------------------------------------------------
            // Validate incCar           
            try
            {
                incCar = reader.GetBoolean(4);
            }
            catch
            {
                readerItemValidArray[4] = false;
            }

            //-----------------------------------------------------------------
            // Validate incCycle            
            try
            {
                incCycle = reader.GetBoolean(5);
            }
            catch
            {
                readerItemValidArray[5] = false;
            }

            //-----------------------------------------------------------------
            // validate  uniqueId   needs to not include chars < > & " ' \ / : * |
            try
            {
                uniqueId = reader.GetString(7);
                if (uniqueId == string.Empty)
                {
                    readerItemValidArray[7] = false;
                }
                else
                {
                    pattern = @"^[a-zA-Z_0-9 !#$%+\-=^`{}@~]+$";
                    regex = new Regex(pattern);
                    match = regex.Match(uniqueId);
                    if (!match.Success)
                    {
                        readerItemValidArray[7] = false;
                    }
                }
            }
            catch
            {
                readerItemValidArray[7] = false;
            }


            //-----------------------------------------------------------------
            // validate originType
            try
            {
                originType = reader.GetString(8);
                if (originType == string.Empty)
                {
                    readerItemValidArray[8] = false;
                }
                else
                {
                    pattern = "^[pcn]+$";
                    regex = new Regex(pattern, RegexOptions.IgnoreCase);
                    match = regex.Match(originType);
                    if (!match.Success)
                    {
                        readerItemValidArray[8] = false;
                    }
                }
            }
            catch
            {
                readerItemValidArray[8] = false;
            }

            //-----------------------------------------------------------------
            // validate origin
            try
            {

                origin = reader.GetString(9);
                if (origin == string.Empty)
                {
                    readerItemValidArray[9] = false;
                }
                else
                {
                    pattern = @"^[a-zA-Z_0-9 !#$%*+\-/=?^`{|}@~]+$";
                    regex = new Regex(pattern, RegexOptions.IgnoreCase);
                    match = regex.Match(origin);
                    if (!match.Success)
                    {
                        readerItemValidArray[9] = false;
                    }
                }
            }
            catch
            {
                readerItemValidArray[9] = false;
            }


            //-----------------------------------------------------------------
            // validate  destType
            try
            {
                destType = reader.GetString(10);
                if (destType == string.Empty)
                {
                    readerItemValidArray[10] = false;
                }
                else
                {
                    pattern = "^[pcn]+$";
                    regex = new Regex(pattern, RegexOptions.IgnoreCase);
                    match = regex.Match(destType);
                    if (!match.Success)
                    {
                        readerItemValidArray[10] = false;
                    }
                }
            }
            catch
            {
                readerItemValidArray[10] = false;
            }


            //-----------------------------------------------------------------
            // validate dest
            try
            {
                dest = reader.GetString(11);
                if (dest == string.Empty)
                {
                    readerItemValidArray[11] = false;
                }
                else
                {
                    pattern = @"^[a-zA-Z_0-9 !#$%*+\-/=?^`{|}@~]+$";
                    regex = new Regex(pattern, RegexOptions.IgnoreCase);
                    match = regex.Match(dest);
                    if (!match.Success)
                    {
                        readerItemValidArray[11] = false;
                    }
                }

            }
            catch
            {
                readerItemValidArray[11] = false;
            }


            //-----------------------------------------------------------------
            // validate outDate and retDate
            try
            {
                outDate = reader.GetDateTime(12);
                outDate = outDate.ToString() == string.Empty ? DateTime.MinValue : outDate;

                if( outDate < DateTime.Today)
                {
                    readerItemValidArray[12] = false;
                }
            
            }
            catch
            {
                readerItemValidArray[12] = false; 
            }


            //-----------------------------------------------------------------
            // validate outTime
            try
            {
                outTime = reader.GetTimeSpan(13);
            }
            catch
            {
                readerItemValidArray[13] = false;
            }

            //-----------------------------------------------------------------
            // validate outArrDep
           try
           {
               outArrDep = reader[14].ToString() == string.Empty ? "empty" : reader.GetString(14);
               if (outArrDep == "empty")
               {
                   readerItemValidArray[14] = false;
               }
               else
               {
                    outArrDep = reader.GetString(14);
                    pattern = "^[ad]+$";
                    regex = new Regex(pattern, RegexOptions.IgnoreCase);
                    match = regex.Match(outArrDep);
                    if (!match.Success)
                    {
                        readerItemValidArray[14] = false;
                    }
                }
            }
            catch
            {
                readerItemValidArray[14] = false;
            }
            //-----------------------------------------------------------------
            // validate retDate
            try
            {
                outDate = reader[12].ToString() == string.Empty ? DateTime.MinValue : reader.GetDateTime(12);
                retDate = reader[15].ToString() == string.Empty ? DateTime.MinValue : reader.GetDateTime(15);

                if (retDate != DateTime.MinValue)
                {
                    if (retDate < DateTime.Today || retDate < outDate)
                    {
                        readerItemValidArray[15] = false;
                    }
                }

            }
            catch
            {

                readerItemValidArray[15] = false;
            }

            //-----------------------------------------------------------------
            // validate retTime
            try
            {
                retTime = reader[16].ToString() == string.Empty ? TimeSpan.MinValue : reader.GetTimeSpan(16);
            }
            catch
            {
                readerItemValidArray[16] = false;
            }


            //-----------------------------------------------------------------
            // validate retArrDep
            try
            {
                retArrDep = reader[17].ToString() == string.Empty ? "empty" : reader.GetString(17);
                if (retArrDep == "empty")
                {
                    retArrDep = null;
                }
                else
                {
                    pattern = "^[ad]+$";
                    regex = new Regex(pattern, RegexOptions.IgnoreCase);
                    match = regex.Match(retArrDep);
                    if (!match.Success)
                    {
                        readerItemValidArray[17] = false;
                    }
                }
            }
            catch
            {
                readerItemValidArray[17] = false;
            }

            return readerItemValidArray;
        }

    }
}


