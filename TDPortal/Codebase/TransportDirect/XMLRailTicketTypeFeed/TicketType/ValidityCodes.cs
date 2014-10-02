// *********************************************** 
// NAME                 : ValidityCodes.cs
// AUTHOR               : Steve Tsang
// DATE CREATED         : 26/06/2008
// DESCRIPTION			: Contains a list of Validity codes
// ************************************************ 

using System;
using System.Collections.Generic;
using System.Text;

namespace TransportDirect.Common.RailTicketType
{
    public class ValidityCodes
    {
        private List<string> validityCode;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="ValidityCode">A string list of validity codes</param>
        public ValidityCodes(System.Collections.Generic.List<string> ValidityCode)
        {
            this.validityCode = ValidityCode;
        }

        /// <summary>
        /// A list of validity codes
        /// </summary>
        public List<string> ValidityCode
        {
            get
            {
                return validityCode;
            }
        }
    }
}
