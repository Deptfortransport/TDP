using System;

namespace TransportDirect.UserPortal.RetailBusinessObjects
{
	/// <summary>
	/// Summary description for OutputRecordDetails.
	/// </summary>
	public class OutputRecordDetails
	{

        private int recordLength;
        private int recordOutput;
        private int recordsRemaining;

        /// <summary>
        /// The length of the record. Equals the sum of the lengths of the
        /// selected fields. For fixed format output then this is equal to the
        /// length of the output
        /// </summary>
        public int RecordLength 
        {
            get { return this.recordLength; }
        }

        /// <summary>
        /// The number of records returned in the output parameter. For fixed
        /// format output then this is equal to 1. For variable length SQL type
        /// outputs this will be 0 to many.Records-output may be less than the
        /// total number of records found if there are too many records to fit
        /// in the output parameter. In this case the function should be called
        /// multiple times.
        /// </summary>
        public int RecordOutput 
        {
            get { return this.recordOutput; }
        }

        /// <summary>
        /// The number of records remaining to be output. If this is greater
        /// than 0 then the application should call the object again until the
        /// remaining records equals 0.
        /// </summary>
        public int RecordsRemaining 
        {
            get { return this.recordsRemaining; }
        }

        /// <summary>
        /// Details about the output parameters. There will be one occurrence
        /// for each output parameter (excluding the header parameter) and the
        /// order will match the order returned by the function.
        /// </summary>
        /// <param name="recordDetails"></param>
		public OutputRecordDetails( string recordDetails )
		{
            if ((recordDetails != null) && (recordDetails.Trim().Length != 0) && (recordDetails.Length == 12)) 
            {
                if ( recordDetails.Substring(0,4).Trim().Length != 0 )
                    this.recordLength = int.Parse(recordDetails.Substring(0,4));

                if ( recordDetails.Substring(4,4).Trim().Length != 0 )
                    this.recordOutput = int.Parse(recordDetails.Substring(4,4));

                if ( recordDetails.Substring(8,4).Trim().Length != 0 )
                    this.recordsRemaining = int.Parse(recordDetails.Substring(8,4));
            }
		}
	}
}
