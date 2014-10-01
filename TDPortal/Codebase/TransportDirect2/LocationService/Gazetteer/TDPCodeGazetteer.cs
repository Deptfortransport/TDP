// *********************************************** 
// NAME             : TDPCodeGazetteer.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 13 Feb 2014
// DESCRIPTION  	: Implementation of ITDCodeGazetteer interface. Accesses and formats results from ESRI code gazetteer
// ************************************************
// 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Logger = System.Diagnostics.Trace;
using TDP.Common.EventLogging;
using TDP.Common.PropertyManager;
using TDP.Reporting.Events;
using TDP.Common.Extenders;
using System.ServiceModel;

namespace TDP.Common.LocationService.Gazetteer
{
    /// <summary>
    /// Implementation of ITDCodeGazetteer interface. Accesses and formats results from ESRI code gazetteer
    /// </summary>
    [Serializable]
    public class TDPCodeGazetteer : ITDPCodeGazetteer, IDisposable
    {
        #region Private variables

        private string sessionID = string.Empty;
        private bool userLoggedOn = false;

        private string gazetteerId = string.Empty;
		private int maxReturnedRecords = 0;
		private const int maxLength = -1;
		private const int minLength = -1;
        
        [NonSerialized()]
        private GazopsWeb.GazopsServiceSoapClient gazopsWebService = null;

		private XmlGazetteerHandler xmlHandler;

        #endregion

        #region Constructor

        /// <summary>
		/// Default constructor
		/// </summary>
        public TDPCodeGazetteer()
		{
            #region Initialise Gazops Web Service

            if (gazopsWebService == null)
            {
                // Read configuration properites
                string serviceAddress = Properties.Current["locationservice.gazopsweburl"];
                string serviceBindingName = Properties.Current["locationservice.gazopsweb.servicebinding.config"];

                if (string.IsNullOrEmpty(serviceAddress) || string.IsNullOrEmpty(serviceBindingName))
                {
                    OperationalEvent oe = new OperationalEvent(TDPEventCategory.Business, sessionID, TDPTraceLevel.Error,
                        "Unable to access the GazopsWeb url and/or service binding property");
                    Logger.Write(oe);

                    throw new TDPException(oe.Message, true, TDPExceptionIdentifier.LSGazopURLUnavailable);
                }

                //initialise the service
                EndpointAddress serviceEndpoint = new EndpointAddress(serviceAddress);

                gazopsWebService = new GazopsWeb.GazopsServiceSoapClient(serviceBindingName, serviceEndpoint);
            }

            #endregion

            SetGazetteerId("locationservice.codegazetteerid");

            // Set maxReturnedRecords
            maxReturnedRecords = Properties.Current["locationservice.maxreturnedrecords"].Parse(60);

            xmlHandler = new XmlGazetteerHandler();

            if (TDPTraceSwitch.TraceVerbose)
            {
                OperationalEvent log = new OperationalEvent(TDPEventCategory.Business, TDPTraceLevel.Verbose,
                    string.Format("Using {0} with gazetterId[{1}] and maxReturnedRecords[{2}]", this.GetType().Name, gazetteerId, maxReturnedRecords));
                Logger.Write(log);
            }
		}

        #endregion

        #region ITDPCodeGazetteer methods implementations

        /// <summary>
        /// Method that identifies a given code as a valid code or not. 
        /// </summary>
        /// <param name="code">Code to identify.</param>
        /// <returns>If valid, all associated codes will be returned in a an array of CodeDetail objects.
        /// Otherwise, the array will be empty</returns>
		public CodeDetail[] FindCode(string code)
		{
            DateTime submitted = DateTime.Now;
            string result = GazopsWebService.StreetMatch(
                code,
                false, 
                gazetteerId, 
                string.Empty, 
                maxReturnedRecords, 
                string.Empty);
            LogGazetteerEvent(GazetteerEventCategory.GazetteerCode, submitted);

			// read result with modetype filter set to all modeTypes!
            return xmlHandler.ReadCodeGazResult(result, new TDPModeType[] { 
                TDPModeType.Air, TDPModeType.Bus, TDPModeType.Coach, TDPModeType.Ferry, 
                TDPModeType.Metro, TDPModeType.Rail, TDPModeType.Unknown });
		}

        /// <summary>
        /// Method that takes a given text entry and searches for associated codes.
        /// </summary>
        /// <param name="text">Text to find</param>
        /// <param name="fuzzy">Indicate if user is unsure of spelling</param>
        /// <param name="modeTypes">Requested mode types-associated codes to be returned.</param>
        /// <returns>If codes are found, all matching ones will be returned in an array of CodeDetail objects.
        /// Otherwise, the array will be empty.</returns>
		public CodeDetail[] FindText(string text, bool fuzzy, TDPModeType[] modeTypes)
		{            			
			StringBuilder modeSelection = new StringBuilder();
			
			// first parse the array to see if there are any modes selected. 
			// If so, create filter string for later.
            foreach (TDPModeType modeInProgress in modeTypes)
			{ 
				//	Valid modes are Undefined, Rail, Bus, Coach, Air, Ferry, Metro
                if (modeInProgress != TDPModeType.Unknown) 
				{
					switch (modeInProgress)
					{
                        case TDPModeType.Air:
                            modeSelection.Append("'Air',");
							break;
                        case TDPModeType.Bus:
                            modeSelection.Append("'Bus',");
							break;
                        case TDPModeType.Coach:
                            modeSelection.Append("'Coach',");
							break;
                        case TDPModeType.Ferry:
                            modeSelection.Append("'Ferry',");
                            break;
                        case TDPModeType.Rail:
                            modeSelection.Append("'Rail',");
							break;
						default:
                            if (!modeSelection.ToString().Contains("Other"))
                            {
                                // 'Other' includes Tram, Metro and Underground.
                                modeSelection.Append("'Other',");
                            }
							break;
					}
				}
			}
			
            // If any modes were selected, add the directive
            if (modeSelection.Length > 0) 
			{
                text = string.Format("{0}[//GAZOPS_DIRECTIVE=MODE:{1}]", text, modeSelection.ToString().TrimEnd(','));
			}

            DateTime submitted = DateTime.Now;
			string result = GazopsWebService.PlaceNameMatch(
                text, 
                fuzzy, 
                maxLength, 
                minLength, 
                gazetteerId, 
                string.Empty, 
                maxReturnedRecords, 
                string.Empty);
            LogGazetteerEvent(GazetteerEventCategory.GazetteerCode, submitted);

			return xmlHandler.ReadCodeGazResult(result, modeTypes);
		}

        #endregion
        
        #region Private methods

        /// <summary>
        /// GazopsWeb which provides the ESRI gazetteer services
        /// </summary>
        private GazopsWeb.GazopsServiceSoapClient GazopsWebService
        {
            get { return gazopsWebService; }
        }

        /// <summary>
        /// Sets the gazetteer id using the property key
        /// </summary>
        /// <param name="propGazetteerKey"></param>
        private void SetGazetteerId(string propGazetteerKey)
        {
            // Set the gazeteer id
            gazetteerId = Properties.Current[propGazetteerKey];

            if (string.IsNullOrEmpty(gazetteerId))
            {
                OperationalEvent oe = new OperationalEvent(TDPEventCategory.Business, sessionID, TDPTraceLevel.Error,
                    string.Format("{0} not set or set to a wrong format", propGazetteerKey));
                Logger.Write(oe);

                throw new TDPException(oe.Message, true, TDPExceptionIdentifier.LSGazetteerIDInvalid);
            }
        }

        /// <summary>
        /// Method to Log GazetterEvent
        /// </summary>
        /// <param name="gazetterEventCategory">The type of gazetteer that is being called</param>
        private void LogGazetteerEvent(GazetteerEventCategory gazetterEventCategory, DateTime submitted)
        {
            GazetteerEvent ge = new GazetteerEvent(gazetterEventCategory, submitted, sessionID, userLoggedOn);
            Logger.Write(ge);
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // NOTE: Leave out the finalizer altogether if this class doesn't 
        // own unmanaged resources itself, but leave the other methods
        // exactly as they are. 
        ~TDPCodeGazetteer()
        {
            // Finalizer calls Dispose(false)
            Dispose(false);
        }

        // The bulk of the clean-up code is implemented in Dispose(bool)
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // free managed resources
                if (gazopsWebService != null)
                {
                    gazopsWebService.Close();
                    gazopsWebService = null;
                }

            }
        }

        #endregion
    }
}