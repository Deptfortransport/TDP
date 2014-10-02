// *********************************************** 
// NAME             : DBWSMessage.cs   
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 28 Nov 2013
// DESCRIPTION  	: DBWSMessage class for returning result code and messages
// ************************************************
// 
                
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TransportDirect.WebService.DepartureBoardWebService.DataTransfer
{
    /// <summary>
    /// DBWSMessage class for returning result code and messages
    /// </summary>
    [Serializable]
    public class DBWSMessage
    {
        #region Private variables

        private int code;
		private string description;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public DBWSMessage()
		{
            code = -1;
            description = string.Empty;
		}

        /// <summary>
        /// Constructor
        /// </summary>
        public DBWSMessage(int code, string description)
        {
            this.code = code;
            this.description = description;
        }

        #endregion

        #region Public properties

        /// <summary>
        /// Read/Write. Message Code
        /// </summary>
		public int Code
		{
            get { return code; }
            set { code = value; }
		}

        /// <summary>
        /// Read/Write. Message Description
        /// </summary>
		public string Description
		{
            get { return description; }
            set { description = value; }
        }

        #endregion
    }
}
