// *********************************************** 
// NAME             : DBWSOperator.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 03 Jul 2013
// DESCRIPTION  	: DBWSOperator class for information about a service operator
// ************************************************
// 
                
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TransportDirect.WebService.DepartureBoardWebService.DataTransfer
{
    /// <summary>
    /// DBWSOperator class for information about a service operator
    /// </summary>
    [Serializable]
    public class DBWSOperator
    {
        #region Private variables

        private string operatorCode;
		private string operatorName;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public DBWSOperator()
		{
		}

        /// <summary>
        /// Constructor
        /// </summary>
        public DBWSOperator(string operatorCode, string operatorName)
        {
            if (string.IsNullOrEmpty(operatorCode))
                operatorCode = string.Empty;

            if (string.IsNullOrEmpty(operatorName))
                operatorName = string.Empty;

            this.operatorCode = operatorCode;
            this.operatorName = operatorName;
        }

        #endregion

        #region Public properties

        /// <summary>
        /// Read/Write. Operator code
        /// </summary>
        public string OperatorCode
		{
			get{ return operatorCode;}
			set{ operatorCode = value;}
		}

        /// <summary>
        /// Read/Write. Operator name
        /// </summary>
		public string OperatorName
		{
			get{ return operatorName;}
			set{ operatorName = value;}
        }

        #endregion
    }
}