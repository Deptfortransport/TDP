// *********************************************** 
// NAME             : TDPVenueGate.cs      
// AUTHOR           : Mark Turner
// DATE CREATED     : 11 May 2011
// DESCRIPTION  	: Represents venue gates
// ************************************************


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TDP.Common.LocationService
{
    /// <summary>
    /// Represents venue gates
    /// </summary>
    [Serializable]
    public class TDPVenueGate
    {
        #region Private Fields
        private string gateNaPTAN;
        private string gateName;
        private OSGridReference gateGridRef;
        private DateTime availableFrom;
        private DateTime availableTo;
        #endregion

        #region Public Properties
        /// <summary>
        /// The unique NaPTAN ID of this venue gate.
        /// </summary>
        public string GateNaPTAN
        {
            get { return gateNaPTAN; }
            set { gateNaPTAN = value; }
        }

        /// <summary>
        /// The display name of this venue gate. 
        /// </summary>
        public string GateName
        {
            get { return gateName; }
            set { gateName = value; }
        }

        /// <summary>
        /// The Easting and Northing of this venue gate. 
        /// </summary>
        public OSGridReference GateGridRef
        {
            get { return gateGridRef; }
            set { gateGridRef = value; }
        }

        /// <summary>
        /// The first date that this venue gate is available. 
        /// </summary>
        public DateTime AvailableFrom
        {
            get { return availableFrom; }
            set { availableFrom = value; }
        }

        /// <summary>
        /// The last date that this venue gate is available. 
        /// </summary>
        public DateTime AvailableTo
        {
            get { return availableTo; }
            set { availableTo = value; }
        }

        #endregion
    }
}
