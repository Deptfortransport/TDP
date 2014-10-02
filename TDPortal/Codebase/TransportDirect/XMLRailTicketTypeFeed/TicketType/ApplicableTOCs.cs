// *********************************************** 
// NAME                 : ApplicableTOCS.cs
// AUTHOR               : Steve Tsang
// DATE CREATED         : 26/06/2008
// DESCRIPTION			: Class representing the Applicable Terms and Conditions for a Ticket Type
// ************************************************ 

using System;
using System.Collections.Generic;
using System.Text;

namespace TransportDirect.Common.RailTicketType
{
    public class ApplicableTOCs
    {
        private bool allTocs;
        private string tocsAndConnections = String.Empty;
        private readonly List<ExcludedTOCs> excludedTocs;
        private readonly List<IncludedTOCs> includedTocs;

        /// <summary>
        /// Constructor for Applicable Tocs
        /// </summary>
        public ApplicableTOCs()
        {
            excludedTocs = new List<ExcludedTOCs>();
            includedTocs = new List<IncludedTOCs>();
        }

        /// <summary>
        /// All Terms and Conditions (Bool)
        /// </summary>
        public bool AllTocs
        {
            get
            {
                return allTocs;
            }
            set { allTocs = value; }
        }

        /// <summary>
        /// Terms and Conditions 
        /// </summary>
        public string TocsAndConnections
        {
            get
            {
                return tocsAndConnections;
            }
            set { tocsAndConnections = value; }
        }

        /// <summary>
        /// List of Terms and Conditions excluded from the Ticket Type
        /// </summary>
        public List<ExcludedTOCs> ExcludedTocs
        {
            get
            {
                return excludedTocs;
            }
        }

        /// <summary>
        /// List of Terms and Conditions included in the Ticket Type
        /// </summary>
        public List<IncludedTOCs> IncludedTocs
        {
            get
            {
                return includedTocs;
            }
        }
    }
}
