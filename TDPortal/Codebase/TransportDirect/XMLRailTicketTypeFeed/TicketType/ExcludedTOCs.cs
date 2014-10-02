// *********************************************** 
// NAME                 : ExcludedTocs.cs
// AUTHOR               : Steve Tsang
// DATE CREATED         : 26/06/2008
// DESCRIPTION			: Excluded Terms and Conditions
// ************************************************ 

using System;
using System.Collections.Generic;
using System.Text;

namespace TransportDirect.Common.RailTicketType
{
    public class ExcludedTOCs
    {
        private string tocsRef;
        private string atocName;

        /// <summary>
        /// Excluded Tocs Constructor
        /// </summary>
        /// <param name="AtocName">Name of the Toc</param>
        /// <param name="TocsRef">Reference for the Toc</param>
        public ExcludedTOCs(string AtocName, string TocsRef)
        {
            this.tocsRef = TocsRef;
            this.atocName = AtocName;
        }

        /// <summary>
        /// Toc Reference
        /// </summary>
        public string TocsRef
        {
            get
            {
                return tocsRef;
            }
        }

        /// <summary>
        /// Toc Name
        /// </summary>
        public string AtocName
        {
            get
            {
                return atocName;
            }
        }
    }
}
