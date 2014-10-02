//********************************************************************************
//NAME         : BusinessObjectOutput.cs
//AUTHOR       : SchlumbergerSema
//DATE CREATED : 10/05/2003
//DESCRIPTION  : Provides a class to wrap output data returned by
//               BusinessObject class instances.
//DESIGN DOC   : DD034 Reatil Pricing
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/RetailBusinessObjects/BusinessObjectOutput.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:46:02   mturner
//Initial revision.
//
//   Rev 1.4   Jun 17 2004 13:33:10   passuied
//changes for del6:
//Inserted calls to GL and GM functions to restrict fares.
//Changes in RestrictFares design to respect Open-Close Principle
//
//   Rev 1.3   Oct 28 2003 20:04:50   geaton
//Changes to support housekeeping and timeout functionality.
//
//   Rev 1.2   Oct 14 2003 12:27:48   CHosegood
//Intermediate version.  Not ready for build/test
//
//   Rev 1.1   Oct 13 2003 15:26:14   CHosegood
//Initial Version
//
//   Rev 1.0   Oct 08 2003 11:46:28   CHosegood
//Initial Revision

using System;
using System.Diagnostics;
using System.Text;

namespace TransportDirect.UserPortal.RetailBusinessObjects
{
    /// <summary>
    /// Wraps output data returned by BusinessObject calls.
    /// </summary>
    [Serializable]
    public class BusinessObjectOutput
    {
        private string sectionID = "";
        private string instanceID = "";
        private string state = "";
        private string errorCode = "";
        private string errorSeverity = "";
        private string errorLocation = "";
        private string outputBody;
        private string outputHeader;

        private OutputRecordDetails[] recordDetails = new OutputRecordDetails[6];

		/// <summary>
		/// Gets output body.
		/// </summary>
        public string OutputBody 
        {
            get { return this.outputBody; }
			set{ this.outputBody = value;}
        }

        /// <summary>
        /// Gets the state.
        /// </summary>
        public String State 
        {
            get { return this.state.Trim(); }
        }

        /// <summary>
        /// Gets the error code.
        /// </summary>
        public String ErrorCode 
        {
            get { return this.errorCode.Trim(); }
        }

        /// <summary>
        /// Gets the error severity.
        /// </summary>
        public String ErrorSeverity 
        {
            get { return this.errorSeverity.Trim(); }
        }

        /// <summary>
        /// Gets the error location.
        /// </summary>
        public String ErrorLocation 
        {
            get { return this.errorLocation.Trim(); }
        }

		/// <summary>
		/// Gets the outout record details.
		/// </summary>
        public OutputRecordDetails[] RecordDetails 
        {
            get { return this.recordDetails; }
        }

        /// <summary>
        /// Class constructor.
        /// </summary>
        public BusinessObjectOutput(String outputHeader, string outputBody)
        {
            this.outputHeader = outputHeader;
            this.outputBody = outputBody;
            this.sectionID = outputHeader.Substring(0,8);
            this.instanceID = outputHeader.Substring(8,8);
            this.state = outputHeader.Substring(16,2);
            this.errorCode = outputHeader.Substring(18,4);
            this.errorSeverity = outputHeader.Substring(22,1);
            this.errorLocation = outputHeader.Substring(23,1);

            for (int i = 24,j=0;i<(12*6)+24;i+=12,j++) 
            {
                if ( (i + 12) <= outputHeader.ToString().Length )
                    this.recordDetails[j] = new OutputRecordDetails(outputHeader.Substring(i,12));
            }
        }

        /// <summary>
        /// Converts the value of this instance to its equivalent string representation
        /// </summary>
        /// <returns>Instance as a string.</returns>
        public override String ToString() 
        {
            return this.outputHeader;
        }
    }
}
