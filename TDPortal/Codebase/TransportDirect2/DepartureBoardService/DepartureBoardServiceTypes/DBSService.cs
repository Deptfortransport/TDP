// *********************************************** 
// NAME             : DBSService.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 07 Feb 2014
// DESCRIPTION  	: DBSService class providing service related information
// ************************************************
// 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TDP.UserPortal.DepartureBoardService.DepartureBoardServiceTypes
{
    /// <summary>
    /// DBSService class providing service related information
    /// </summary>
    [Serializable]
    public class DBSService
    {
        private string sOperatorCode;
        private string sOperatorName;
        private string sServiceNumber;

        public DBSService()
        {
            sOperatorCode = string.Empty;
            sOperatorName = string.Empty;
            sServiceNumber = string.Empty;
        }

        public string OperatorCode
        {
            get { return sOperatorCode; }
            set { sOperatorCode = value; }
        }

        public string OperatorName
        {
            get { return sOperatorName; }
            set { sOperatorName = value; }
        }

        public string ServiceNumber
        {
            get { return sServiceNumber; }
            set { sServiceNumber = value; }
        }
    }
}
