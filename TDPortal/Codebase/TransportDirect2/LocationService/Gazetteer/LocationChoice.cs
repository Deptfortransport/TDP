// *********************************************** 
// NAME             : LocationChoice.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 03 Jul 2013
// DESCRIPTION  	: Location choice offered to user, or picked by them
// ************************************************
// 
                
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TDP.Common.LocationService.Gazetteer
{
    /// <summary>
    /// Location choice offered to user, or picked by them
    /// </summary>
    [Serializable()]
    public class LocationChoice
    {
        #region Private members declaration

        private string description = string.Empty;
        private bool hasChildren = false;
        private string pickListCriteria = string.Empty;
        private string pickListValue = string.Empty;
        private OSGridReference gridReference = null;
        private string naptan = string.Empty;
        private double score = 0;
        private string locality = string.Empty;
        private string exchangePointType = string.Empty;
        private bool isAdminArea = false;
        
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public LocationChoice(string description, bool hasChildren, string pickListCriteria, string pickListValue, OSGridReference gridReference, string naptan, double score, string locality, string exchangePointType, bool isAdminArea)
        {
            this.description = description;
            this.hasChildren = hasChildren;
            this.pickListCriteria = pickListCriteria;
            this.pickListValue = pickListValue;
            this.gridReference = gridReference;
            this.naptan = naptan;
            this.score = score;
            this.locality = locality;
            this.exchangePointType = exchangePointType;
            this.isAdminArea = isAdminArea;
        }

        #endregion

        #region Public properties

        /// <summary>
        /// Read-only property. gets the choice description
        /// </summary>
        public string Description
        {
            get { return description; }
        }

        /// <summary>
        /// Read-only property. Indicates if choice has children
        /// </summary>
        public bool HasChilden
        {
            get { return hasChildren; }
        }

        /// <summary>
        /// Read-only property. PickList criteria
        /// </summary>
        public string PicklistCriteria
        {
            get { return pickListCriteria; }
        }

        /// <summary>
        /// Read-only property. PickList value
        /// </summary>
        public string PicklistValue
        {
            get { return pickListValue; }
        }

        /// <summary>
        /// Read-only property. gridReference
        /// </summary>
        public OSGridReference OSGridReference
        {
            get { return gridReference; }
        }

        /// <summary>
        /// Read-only property. Naptan
        /// </summary>
        public string Naptan
        {
            get { return naptan; }
        }

        /// <summary>
        /// Read-only property. choice's score
        /// </summary>
        public double Score
        {
            get { return score; }
        }

        /// <summary>
        /// Read-only property. Locality
        /// </summary>
        public string Locality
        {
            get { return locality; }
        }

        /// <summary>
        /// Read-only property. ExchangePoint type
        /// </summary>
        public string ExchangePointType
        {
            get { return exchangePointType; }
        }

        /// <summary>
        /// Read-only property. IsAdminArea
        /// </summary>
        public bool IsAdminArea
        {
            get { return isAdminArea; }
        }

        #endregion

        #region Public methods

        /// <summary>
        /// String representation of object
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "[desc = " + Description
                + " : hasChildren = " + hasChildren
                + " : picklistCriteria = " + PicklistCriteria
                + " : picklistValue = " + PicklistValue
                + " : gridReference = " + gridReference
                + " : Naptan = " + naptan
                + " : score = " + score
                + " : locality = " + Locality
                + " : exchangepoint type = " + ExchangePointType;
        }

        #endregion
    }
}