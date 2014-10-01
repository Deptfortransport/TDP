// *********************************************** 
// NAME             : InputPageState.cs      
// AUTHOR           : Amit Patel
// DATE CREATED     : 31 Mar 2011
// DESCRIPTION  	: Class to manage journey request input page variables in session
// ************************************************
                
                
using System;
using System.Collections.Generic;
using TDP.UserPortal.JourneyControl;
using TDP.Common;
using TDP.Common.LocationService;

namespace TDP.UserPortal.SessionManager
{
    /// <summary>
    /// Class to manage journey request input page variables in session
    /// </summary>
    [Serializable]
    public class InputPageState : ITDPSessionAware
    {
        #region Private Fields

        // Location search properties
        private LocationSearch originLocationSearch = null;
        private LocationSearch destinationLocationSearch = null;

        // Journey properties
        private string journeyRequestHash = string.Empty;
        private int journeyIdOutward = 0;
        private int journeyIdReturn = 0;

        private bool journeyLegDetailExpandedOutward = false;
        private bool journeyLegDetailExpandedReturn = false;

        // Stop event properties
        private string stopEventRequestHash = string.Empty;
        private int stopEventJourneyIdOutward = 0;
        private int stopEventJourneyIdReturn = 0;

        // River service show earlier/later 
        private bool showEarlierLinkOutwardRiver = true;
        private bool showLaterLinkOutwardRiver = true;
        private bool showEarlierLinkReturnRiver = true;
        private bool showLaterLinkReturnRiver = true;

        // Stop Information properties
        private TDPStopLocation stopLocation = null;
        private string stopInformationMode = string.Empty;

        private List<TDPMessage> messages = new List<TDPMessage>();

        // Session aware
        private bool isDirty = true;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public InputPageState()
        {
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Adds an TDPMessage to the Messages list in this page state
        /// </summary>
        public void AddMessage(TDPMessage message)
        {
            foreach (TDPMessage msg in messages)
            {
                if (msg.MessageResourceId == message.MessageResourceId)
                {
                    return;
                }
            }

            // Doesn't exist in list, add it
            messages.Add(message);
            isDirty = true;
        }

        /// <summary>
        /// Adds TDPMessage to the Messages list in this page state
        /// </summary>
        public void AddMessages(List<TDPMessage> messagesToAdd)
        {
            if (messagesToAdd != null)
            {
                foreach (TDPMessage message in messagesToAdd)
                {
                    AddMessage(message);
                }
            }
        }

        /// <summary>
        /// Clears all TDPMessage in the Messages list in this page state
        /// </summary>
        public void ClearMessages()
        {
            this.Messages = null; // Setter handles the null
        }

        #endregion

        #region Public properties

        #region Location search properties

        /// <summary>
        /// Origin Search used for Location resolution
        /// </summary>
        public LocationSearch OriginSearch 
        { 
            get { return originLocationSearch;}
            set
            {
                originLocationSearch = value;
                isDirty = true;
            }
        }

        /// <summary>
        /// Destination Search used for Location resolution
        /// </summary>
        public LocationSearch DestinationSearch
        {
            get { return destinationLocationSearch; }
            set
            {
                destinationLocationSearch = value;
                isDirty = true;
            }
        }

        #endregion

        #region Journey properties

        /// <summary>
        /// Read/Write. Current journey request hash to use
        /// </summary>
        public string JourneyRequestHash 
        {
            get { return journeyRequestHash; }
            set 
            { 
                journeyRequestHash = value;
                isDirty = true;
            }
        }

        /// <summary>
        /// Read/Write. Current outward journey id selected
        /// </summary>
        public int JourneyIdOutward
        {
            get { return journeyIdOutward; }
            set
            {
                journeyIdOutward = value;
                isDirty = true;
            }
        }

        /// <summary>
        /// Read/Write. Current return journey id selected
        /// </summary>
        public int JourneyIdReturn
        {
            get { return journeyIdReturn; }
            set
            {
                journeyIdReturn = value;
                isDirty = true;
            }
        }

        /// <summary>
        /// Read/Write. Indicates if user has expanded outward journey leg detail
        /// </summary>
        public bool JourneyLegDetailExpandedOutward
        {
            get { return journeyLegDetailExpandedOutward; }
            set
            {
                journeyLegDetailExpandedOutward = value;
                isDirty = true;
            }
        }

        /// <summary>
        /// Read/Write. Indicates if user has expanded return journey leg detail
        /// </summary>
        public bool JourneyLegDetailExpandedReturn
        {
            get { return journeyLegDetailExpandedReturn; }
            set
            {
                journeyLegDetailExpandedReturn = value;
                isDirty = true;
            }
        }

        #endregion

        #region Stop Event properties

        /// <summary>
        /// Read/Write. Current stop event request hash to use
        /// </summary>
        public string StopEventRequestHash
        {
            get { return stopEventRequestHash; }
            set
            {
                stopEventRequestHash = value;
                isDirty = true;
            }
        }

        /// <summary>
        /// Read/Write. Current stop event outward journey id selected
        /// </summary>
        public int StopEventJourneyIdOutward
        {
            get { return stopEventJourneyIdOutward; }
            set
            {
                stopEventJourneyIdOutward = value;
                isDirty = true;
            }
        }

        /// <summary>
        /// Read/Write. Current stop event return journey id selected
        /// </summary>
        public int StopEventJourneyIdReturn
        {
            get { return stopEventJourneyIdReturn; }
            set
            {
                stopEventJourneyIdReturn = value;
                isDirty = true;
            }
        }

        #endregion

        #region Earlier/Later properties

        /// <summary>
        /// Read/Write. ShowEarlierLinkOutwardRiver flag (default = true) 
        /// </summary>
        public bool ShowEarlierLinkOutwardRiver
        {
            get { return showEarlierLinkOutwardRiver; }
            set
            {
                showEarlierLinkOutwardRiver = value;
                isDirty = true;
            }
        }

        /// <summary>
        /// Read/Write. ShowLaterLinkOutwardRiver flag (default = true) 
        /// </summary>
        public bool ShowLaterLinkOutwardRiver
        {
            get { return showLaterLinkOutwardRiver; }
            set
            {
                showLaterLinkOutwardRiver = value;
                isDirty = true;
            }
        }

        /// <summary>
        /// Read/Write. ShowEarlierLinkReturnRiver flag (default = true) 
        /// </summary>
        public bool ShowEarlierLinkReturnRiver
        {
            get { return showEarlierLinkReturnRiver; }
            set
            {
                showEarlierLinkReturnRiver = value;
                isDirty = true;
            }
        }

        /// <summary>
        /// Read/Write. ShowLaterLinkReturnRiver flag (default = true) 
        /// </summary>
        public bool ShowLaterLinkReturnRiver
        {
            get { return showLaterLinkReturnRiver; }
            set
            {
                showLaterLinkReturnRiver = value;
                isDirty = true;
            }
        }

        #endregion

        #region StopInformation properties

        /// <summary>
        /// Stop Location used on stop information page
        /// </summary>
        public TDPStopLocation StopLocation 
        {
            get { return stopLocation; }
            set
            {
                stopLocation = value;
                isDirty = true;
            }
        }

        /// <summary>
        /// Stop Information mode as a string
        /// </summary>
        public string StopInformationMode
        {
            get { return stopInformationMode; }
            set
            {
                stopInformationMode = value;
                isDirty = true;
            }
        }

        #endregion

        /// <summary>
        /// Read/Write. Messages held in this page state.
        /// Ensure ClearMessages() is called once messages have been used and are no longer required
        /// </summary>
        public List<TDPMessage> Messages
        {
            get { return messages; }
            set 
            {
                if (value != null)
                {
                    messages = value;
                }
                else
                {
                    messages = new List<TDPMessage>();
                }
                isDirty = true;
            }
        }

        #endregion

        #region ITDPSessionAware methods

        /// <summary>
        /// Gets/Sets if the session aware object considers itself to have changed or not
        /// </summary>
        public bool IsDirty
        {
            get { return isDirty; }
            set { isDirty = value; }
        }

        #endregion
    }
}
