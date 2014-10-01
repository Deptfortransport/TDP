// *********************************************** 
// NAME             : TDGazetteer.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 03 Jul 2013
// DESCRIPTION  	: Base class for all gazetteers
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
    /// Base class for all gazetteers
    /// </summary>
    [Serializable()]
    public abstract class TDPGazetteer : ITDPGazetteer, IDisposable
    {
        #region Private variables

        protected string sessionID;
        protected bool userLoggedOn;

		protected string gazetteerId = string.Empty;
        protected int minScore;

		protected XmlGazetteerHandler xmlHandler;

		[NonSerialized()]
        private GazopsWeb.GazopsServiceSoapClient gazopsWebService = null;
        
		#endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="sessionID"></param>
        /// <param name="userLoggedOn"></param>
		public TDPGazetteer(string sessionID, bool userLoggedOn)
		{
			this.sessionID = sessionID;
			this.userLoggedOn = userLoggedOn;

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
		}

		#endregion

        #region ITDPGazetteer methods implementations

        public abstract LocationQueryResult FindLocation(string text, bool fuzzy);
        
        public abstract LocationQueryResult DrillDown(string text, bool fuzzy, string picklist, string queryRef, LocationChoice choice);

        public abstract TDPLocation GetLocationDetails(string text, bool fuzzy, string pickList, string queryRef, LocationChoice choice);

        public abstract bool SupportHierarchicSearch { get; }

        #endregion

        #region Protected methods

        /// <summary>
        /// GazopsWeb which provides the ESRI gazetteer services
        /// </summary>
        protected GazopsWeb.GazopsServiceSoapClient GazopsWebService
		{
			get { return gazopsWebService; }
		}

        /// <summary>
        /// Sets the gazetteer id using the property key
        /// </summary>
        /// <param name="propGazetteerKey"></param>
        protected void SetGazetteerId(string propGazetteerKey)
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
        /// Sets the min score value using the property key
        /// </summary>
        /// <param name="propMinScoreKey"></param>
        protected void SetMinScore(string propMinScoreKey)
        {
            // Set the min scrore value
            minScore = Convert.ToInt32(Properties.Current[propMinScoreKey]);
        }

		/// <summary>
		/// Method to Log GazetterEvent
		/// </summary>
		/// <param name="gazetterEventCategory">The type of gazetteer that is being called</param>
		protected void LogGazetteerEvent(GazetteerEventCategory gazetterEventCategory, DateTime submitted)
		{
			GazetteerEvent ge =  new GazetteerEvent(gazetterEventCategory, submitted, sessionID, userLoggedOn);
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
        ~TDPGazetteer()
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
