// *********************************************** 
// NAME             : DBSMessage.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 07 Feb 2014
// DESCRIPTION  	: DepartureBoard message information
// ************************************************
// 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TDP.UserPortal.DepartureBoardService.DepartureBoardServiceTypes
{
    /// <summary>
    /// DepartureBoard message information
    /// </summary>
    [Serializable]
    public class DBSMessage
    {
        private int code;
        private string description;

        public DBSMessage()
        {
            code = -1;
            description = string.Empty;
        }

        public int Code
        {
            get { return code; }
            set { code = value; }
        }

        public string Description
        {
            get { return description; }
            set { description = value; }
        }
    }
}
