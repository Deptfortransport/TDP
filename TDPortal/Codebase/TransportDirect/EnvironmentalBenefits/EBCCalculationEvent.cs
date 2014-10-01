// *********************************************** 
// NAME			: EBCCalculationEvent.cs
// AUTHOR		: Mark Turner
// DATE CREATED	: 29/09/2009
// DESCRIPTION	: Class which defines a custom event for logging an EBC event
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnvironmentalBenefits/EBCCalculationEvent.cs-arc  $
//
//   Rev 1.0   Oct 06 2009 13:58:40   mmodi
//Initial revision.
//Resolution for 5323: CCN539 Environmental Benefit Calculator
//
//

using System;
using System.Collections.Generic;
using System.Text;

using TransportDirect.Common.Logging;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.ReportDataProvider.TDPCustomEvents;

namespace TransportDirect.UserPortal.EnvironmentalBenefits
{
    /// <summary>
    /// Class which defines a custom event for logging an EBC event
    /// </summary>
    [Serializable]
    public class EBCCalculationEvent : TDPCustomEvent
    {
        #region Private members

        private bool success;
        private DateTime submitted;
        private static EBCCalculationEventFileFormatter fileFormatter = new EBCCalculationEventFileFormatter();

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public EBCCalculationEvent(DateTime submitted, bool success, string sessionId)
            : base(sessionId, false)
		{
            this.submitted = submitted;
            if (this.submitted.Year < 2000)
            {
                this.submitted = DateTime.Now;
            }

            this.success = success;
        }

        #endregion

        #region Public properties

        /// <summary>
        /// Gets the date/time at which the transaction was submitted.
        /// </summary>
        public DateTime Submitted
        {
            get { return submitted; }
        }

        /// <summary>
        /// Gets the success flag.
        /// </summary>
        public bool Success
        {
            get { return success; }
        }

        /// <summary>
        /// Provides an event formatter for publishing to files.
        /// </summary>
        override public IEventFormatter FileFormatter
        {
            get { return fileFormatter; }
        }
        #endregion
    }
}
