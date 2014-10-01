// *********************************************** 
// NAME             : DBSStop.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 07 Feb 2014
// DESCRIPTION  	: DBSStop class providing stop information
// ************************************************
// 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TDP.UserPortal.DepartureBoardService.DepartureBoardServiceTypes
{
    /// <summary>
    /// Class for Stop Information
    /// </summary>
    [Serializable]
    public class DBSStop
    {
        private string sNaptanId;
        private string sName;
        private string sShortCode;

        public DBSStop()
        {
            sNaptanId = string.Empty;
            sName = string.Empty;
            sShortCode = string.Empty;
        }

        public string NaptanId
        {
            get { return sNaptanId; }
            set { sNaptanId = value; }
        }

        public string Name
        {
            get { return sName; }
            set { sName = value; }
        }

        public string ShortCode
        {
            get { return sShortCode; }
            set { sShortCode = value; }
        }
    }
}
