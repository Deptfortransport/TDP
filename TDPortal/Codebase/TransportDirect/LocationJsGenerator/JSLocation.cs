// ************************************************ 
// NAME                 : JSLocation.cs 
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 06/08/2012
// DESCRIPTION          : Represents a location to use in javascript generation
// ************************************************* 

using System;
using System.Collections.Generic;
using System.Text;

namespace TransportDirect.UserPortal.LocationJsGenerator
{
    /// <summary>
    /// Represents a location to use in javascript generation
    /// </summary>
    class JSLocation
    {
        #region Private Fields

        private string datasetID;
        private string parentID;
        private string name;
        private string displayName;
        private string alias;
        private JSLocationType type;
        private string naptan;
        private string locality;

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        public JSLocation(string datasetID, string parentID, 
                          string name, string displayName, string alias,
                          string type, string naptan, string locality)
        {
            this.datasetID = datasetID;
            this.parentID = parentID;
            this.name = name;
            this.displayName = displayName;
            this.alias = alias;
            this.type = JSLocationTypeHelper.GetLocationType(type);
            this.naptan = naptan;
            this.locality = locality;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Read only property determining the location identifier
        /// </summary>
        public string Id
        {
            get { return GetLocationId(); }
        }

        /// <summary>
        /// Read only property determining display name for the location
        /// </summary>
        public string DisplayName
        {
            get { return displayName; }
        }

        /// <summary>
        /// Read only property determining the alias for SJP location
        /// </summary>
        public string Alias
        {
            get { return alias; }
        }

        /// <summary>
        /// Read only property determining type of location
        /// </summary>
        public JSLocationType LocationType
        {
            get { return type; }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Returns the location object properties serialised as Javascript array
        /// </summary>
        /// <returns>String representing location object serialised as Javascript array</returns>
        public string GetJSArrayString()
        {
            if (string.IsNullOrEmpty(alias))
            {
                return string.Format("[\"{0}\",\"{1}\",\"{2}\"]", DisplayName, Id, LocationType);
            }

            return string.Format("[\"{0}\",\"{1}\",\"{2}\",\"{3}\"]", DisplayName, Id, LocationType, Alias);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Returns the identifier for the location
        /// </summary>
        /// <param name="loc"> object</param>
        /// <returns>Location identifier string</returns>
        private string GetLocationId()
        {
            switch (type)
            {
                case JSLocationType.Air:
                case JSLocationType.Coach:
                case JSLocationType.Ferry:
                case JSLocationType.Rail:
                case JSLocationType.LightRail:
                    return naptan;
                case JSLocationType.Group:
                case JSLocationType.POI:
                    return datasetID;
                case JSLocationType.Locality:
                    return locality;
            }

            return string.Empty;
        }

        #endregion
    }
}