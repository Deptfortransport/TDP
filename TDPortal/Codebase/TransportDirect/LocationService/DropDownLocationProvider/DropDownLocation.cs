// ************************************************ 
// NAME                 : DropDownLocation.cs 
// AUTHOR               : Amit Patel
// DATE CREATED         : 04/06/2010 
// DESCRIPTION          : The serializable DropDownLocation class represents the drop down 
//                        stop location object read from the database.
// ************************************************* 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/LocationService/DropDownLocationProvider/DropDownLocation.cs-arc  $
//
//   Rev 1.5   Jul 08 2010 09:36:04   apatel
//Code review actions
//Resolution for 5568: DropDownGaz - code review actions
//
//   Rev 1.4   Jun 14 2010 16:41:42   apatel
//Made class public
//Resolution for 5548: CCN0572 - Drop Down Gazetteers Rail
//
//   Rev 1.3   Jun 07 2010 16:10:46   apatel
//Updated
//Resolution for 5548: drop down gazetteers rail
//
//   Rev 1.2   Jun 07 2010 12:20:54   apatel
//Added property to determine if the location is an alias
//Resolution for 5548: drop down gazetteers rail
//
//   Rev 1.1   Jun 07 2010 09:45:00   apatel
//Updated to replace Crs Code property with more generic Short Code property
//Resolution for 5548: drop down gazetteers rail
//
//   Rev 1.0   Jun 04 2010 11:27:30   apatel
//Initial revision.
//Resolution for 5548: drop down gazetteers rail

using System;
using System.Collections.Generic;
using System.Text;

namespace TransportDirect.UserPortal.LocationService.DropDownLocationProvider
{
    /// <summary>
    /// DropDownLocation class
    /// </summary>
    [Serializable]
    public class DropDownLocation
    {
        #region Private Fields
        private string locationName = null;
        private string displayName = null;
        private string shortCode = null;

        private string naptans = null;

        private bool isGroup = false;
        private bool isAlias = false;

        private string dropDownType = string.Empty;
        #endregion

        #region Constructor
        /// <summary>
        /// Empty constructor
        /// </summary>
        public DropDownLocation()
        {
        }

        /// <summary>
        /// represents the DropDownLocation individual item data object
        /// </summary>
        /// <param name="locationName">Name of the location - the name will be displayed in the textbox after selectiong in dropdown</param>
        /// <param name="displayName">alias name of the location - the name to be displayed in dropdown</param>
        /// <param name="shortCode">Short Code(i.e. CRS code) for the location</param>
        /// <param name="naptans">NaPTANs for the location seperated by comma</param>
        /// <param name="isGroup">Determines if the rail station is a group station or not</param>
        /// <param name="isAlias">Determines if the location display name is an alias for the actual location name </param>
        public DropDownLocation(string locationName, string displayName, string shortCode, string naptans, bool isGroup, bool isAlias, string dropDownType)
        {
            this.locationName = locationName;
            this.displayName = displayName;
            this.shortCode = shortCode;
            this.naptans = naptans;
            this.isGroup = isGroup;
            this.isAlias = isAlias;
            this.dropDownType = dropDownType;
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Correct Naptan/NPTG name of the location
        /// </summary>
        public string LocationName
        {
            get { return locationName; }
            set { locationName = value; }
        }

        /// <summary>
        /// Display name of the location
        /// Display name is the alias name use for the location
        /// If the location doesn't have alias this will be empty string/null
        /// </summary>
        public string DisplayName
        {
            get { return displayName; }
            set { displayName = value; }
        }

        /// <summary>
        /// Short Code(i.e. CRS code) for the location
        /// </summary>
        public string ShortCode
        {
            get { return shortCode; }
            set { shortCode = value; }
        }

        /// <summary>
        /// Read/Write property holding comma separated NaPTANs value for the location
        /// </summary>
        public string Naptans
        {
            get { return naptans; }
            set { naptans = value; }
        }

        /// <summary>
        /// Read/Write property determining if the location reflects a group of locations (i.e. group station)
        /// </summary>
        public bool IsGroup
        {
            get { return isGroup; }
            set { isGroup = value; }
        }

        /// <summary>
        /// Read/Write property determining if the location is an alias 
        /// </summary>
        public bool IsAlias
        {
            get { return isAlias; }
            set { isAlias = value; }
        }

        /// <summary>
        /// Read/Write property determining drop down type of location
        /// </summary>
        public string DropDownType
        {
            get { return dropDownType; }
            set { dropDownType = value; }
        }

        
        #endregion
    }
}
