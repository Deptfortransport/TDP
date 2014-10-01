// *********************************************** 
// NAME                 : EventStatusPropertyValidator.cs
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 01/04/2009
// DESCRIPTION			: Property validator for event status properties
// ************************************************ 
// $Log:   P:/TDPortal/archives/Utilities/SiteStatusLoader/SiteStatusLoaderEvents/EventStatusPropertyValidator.cs-arc  $
//
//   Rev 1.0   Aug 23 2011 11:04:36   mmodi
//Initial revision.
//Resolution for 5728: SiteStatusLoader - Updates for daily wrap up file for SJP
//
//   Rev 1.0   Apr 01 2009 13:37:12   mmodi
//Initial revision.
//Resolution for 5273: Site Status Loader

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using AO.Properties;

using PropertyService = AO.Properties.Properties;

namespace AO.SiteStatusLoaderEvents
{
    public class EventStatusPropertyValidator : PropertyValidator
    {
        /// <summary>
        /// Constructor used to create class based on properties.
        /// </summary>
        /// <param name="properties">Properties that are used to perform validation against.</param>
        public EventStatusPropertyValidator(PropertyService properties)
            : base(properties)
        { 
        }

        /// <summary>
        /// Used to validate a property with a given <c>key</c>.
        /// </summary>
        /// <param name="key">Key of property to validate.</param>
        /// <param name="errors">Holds validation errors if any.</param>
        /// <returns><c>true</c> if property is valid otherwise <c>false</c>.</returns>
        override public bool ValidateProperty(string key, ArrayList errors)
        {
            return ValidateExistence(key, Optionality.Mandatory, errors);
        }
    }
}
