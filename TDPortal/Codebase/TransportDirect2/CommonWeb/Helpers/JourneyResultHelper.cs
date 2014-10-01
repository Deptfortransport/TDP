// *********************************************** 
// NAME             : JourneyResultHelper.cs      
// AUTHOR           : Amit Patel
// DATE CREATED     : 
// DESCRIPTION  	: JourneyResultAdapter class to provide helper methods for journey results
// ************************************************
// 

using TDP.UserPortal.JourneyControl;

namespace TDP.Common.Web
{
    /// <summary>
    /// JourneyResultHelper class to provide helper methods for journey results
    /// </summary>
    public class JourneyResultHelper
    {
        #region Private Fields

        private JourneyHelper journeyHelper = new JourneyHelper();
        private SessionHelper sessionHelper = new SessionHelper();

        private ITDPJourneyResult journeyResult = null;
        private ITDPJourneyRequest journeyRequest = null;

        private ITDPJourneyResult stopEventResult = null;
        private ITDPJourneyRequest stopEventRequest = null;

        #endregion

        #region Public Properties

        /// <summary>
        /// Retruns true if the journey result is avaiable for the current journey request
        /// </summary>
        public bool IsJourneyResultAvailable
        {
            get
            {
                CheckJourneyResultAvailability();

                return journeyResult != null;
            }
        }

        /// <summary>
        /// Retruns true if the journey result is avaiable for the current journey request
        /// </summary>
        public bool IsStopEventResultAvailable
        {
            get
            {
                CheckStopEventResultAvailability();

                return stopEventResult != null;
            }
        }

        /// <summary>
        /// Gets the current journey request
        /// </summary>
        public ITDPJourneyRequest JourneyRequest
        {
            get { return journeyRequest; }
        }

        /// <summary>
        /// Gets the journey result for the current journey request  
        /// </summary>
        public ITDPJourneyResult JourneyResult
        {
            get { return journeyResult; }
        }

        /// <summary>
        ///  Gets the current stop event request
        /// </summary>
       public ITDPJourneyRequest StopEventRequest 
       {
           get { return stopEventRequest; } 
       }

        /// <summary>
        /// Gets the stop event result for the current stop event request
        /// </summary>
       public ITDPJourneyResult StopEventResult 
       {
           get { return stopEventResult; }
       }

        #endregion

        #region Constructor

        /// <summary>
        /// Default Constructor
        /// </summary>
       public JourneyResultHelper()
        {
            SetJourneyRequest();
            SetStopEventRequest();
        }
               
        #endregion

        #region Public Methods

        /// <summary>
        /// Checks for the journey result availability by checking session for journey request hash code 
        /// in journey result manager
        /// </summary>
        /// <returns>If journey result available returns TDPJourneyResult object otherwise returns null</returns>
        public ITDPJourneyResult CheckJourneyResultAvailability()
        {
            string requestHash = journeyHelper.GetJourneyRequestHash();

            // Retrieve the result and store it for use
            journeyResult = sessionHelper.GetTDPJourneyResult(requestHash);

            return journeyResult;
        }

        
        /// <summary>
        /// Checks for the journey result availability by checking session for journey request hash code 
        /// in journey result manager
        /// </summary>
        /// <returns>If journey result available returns TDPJourneyResult object otherwise returns null</returns>
        public ITDPJourneyResult CheckStopEventResultAvailability()
        {
            string requestHash = journeyHelper.GetStopEventRequestHash();

            // Retrieve the result and store it for use
            stopEventResult = sessionHelper.GetTDPStopEventResult(requestHash);
                    
            return stopEventResult;
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Sets the current planned journey request
        /// </summary>
        private void SetJourneyRequest()
        {
            string requestHash = journeyHelper.GetJourneyRequestHash();
            
            journeyRequest = sessionHelper.GetTDPJourneyRequest(requestHash);
        }

        /// <summary>
        /// Sets the current planned stop event request
        /// </summary>
        private void SetStopEventRequest()
        {
            string requestHash = journeyHelper.GetStopEventRequestHash();

            stopEventRequest = sessionHelper.GetTDPStopEventRequest(requestHash);
        }

        #endregion
    }
}