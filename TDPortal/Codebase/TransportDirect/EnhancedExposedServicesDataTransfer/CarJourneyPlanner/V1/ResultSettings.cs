// *********************************************** 
// NAME                 : ResultSettings.cs
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 16/07/2009
// DESCRIPTION  		: Class to hold the settings to be used for the Car journey result
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesDataTransfer/CarJourneyPlanner/V1/ResultSettings.cs-arc  $
//
//   Rev 1.0   Aug 04 2009 14:41:18   mmodi
//Initial revision.
//Resolution for 5307: CCN517a Web Service Find a Car Route
//

using System;
using System.Collections.Generic;
using System.Text;

namespace TransportDirect.EnhancedExposedServices.DataTransfer.CarJourneyPlanner.V1
{
    /// <summary>
    /// Class to hold the settings to be used for the Car journey result
    /// </summary>
    [System.Serializable]
    public class ResultSettings
    {
        private ResultType resultType;
        private DistanceUnit distanceUnit;
        private string language;

        // Properties to allow user to optionally include above properties in the request
        private bool resultTypeSpecified;
        private bool distanceUnitSpecified;
        private bool languageSpecified;

        /// <summary>
        /// Constructor
        /// </summary>
        public ResultSettings()
        {
        }

        #region Properties

        /// <summary>
        /// The format in which the result should be in, e.g. Summary details or Full details
        /// </summary>
        public ResultType ResultType
        {
            get { return resultType; }
            set { resultType = value; }
        }

        /// <summary>
        /// The distance units the journey should be in, e.g. Miles or Kms
        /// </summary>
        public DistanceUnit DistanceUnit
        {
            get { return distanceUnit; }
            set { distanceUnit = value; }
        }

        /// <summary>
        /// The language the result car journey instructions should be in
        /// </summary>
        public string Language
        {
            get { return language; }
            set { language = value; }
        }

        #endregion
        
        #region Properties Internal (optional)

        /// <summary>
        /// Property used internally to allow optional inclusion in request
        /// </summary>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool ResultTypeSpecified
        {
            get { return resultTypeSpecified; }
            set { resultTypeSpecified = value; }
        }
        
        /// <summary>
        /// Property used internally to allow optional inclusion in request
        /// </summary>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool DistanceUnitSpecified
        {
            get { return distanceUnitSpecified; }
            set { distanceUnitSpecified = value; }
        }

        /// <summary>
        /// Property used internally to allow optional inclusion in request
        /// </summary>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool LanguageSpecified
        {
            get { return languageSpecified; }
            set { languageSpecified = value; }
        }
        
        #endregion
    }
}
