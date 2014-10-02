// *********************************************** 
// NAME                 : IncludedTocs.cs
// AUTHOR               : Steve Tsang
// DATE CREATED         : 26/06/2008
// DESCRIPTION			: Included Terms and Conditions for a Ticket Type
// ************************************************ 

using System;
using System.Collections.Generic;
using System.Text;

namespace TransportDirect.Common.RailTicketType
{
    public class IncludedTOCs
    {
        private string tocRef;
        private string atocName;

        /// <summary>
        /// Included Tocs Constructor
        /// </summary>
        /// <param name="AtocName">Name of the Toc</param>
        /// <param name="TocsRef">Reference for the Toc</param>
        public IncludedTOCs(string AtocName, string TocRef)
        {
            this.tocRef = TocRef;
            this.atocName = AtocName;
        }

        /// <summary>
        /// Toc Reference
        /// </summary>
        public string TocRef
        {
            get
            {
                return tocRef;
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
