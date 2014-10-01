// *********************************************** 
// NAME                 : DatabasePublishersPropertyValidator.cs
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 01/04/2009
// DESCRIPTION			: Property validator for specific database publisher properties
// ************************************************ 
// $Log:   P:/TDPortal/archives/Utilities/SiteStatusLoader/EventLogging/DatabasePublisher/DatabasePublishersPropertyValidator.cs-arc  $
//
//   Rev 1.0   Apr 01 2009 13:28:46   mmodi
//Initial revision.
//Resolution for 5273: Site Status Loader

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using AO.DatabaseInfrastructure;
using AO.Properties;

using PropertyService = AO.Properties.Properties;

namespace AO.EventLogging
{
    public class DatabasePublisherPropertyValidator : PropertyValidator
    {
        /// <summary>
        /// Default constructor.
        /// </summary>
        /// <param name="properties">Properties.</param>
        public DatabasePublisherPropertyValidator(PropertyService properties)
            : base(properties)
        { }

        /// <summary>
        /// Validates the given property.
        /// </summary>
        /// <param name="key">Property name to validate.</param>
        /// <param name="errors">List to append any errors.</param>
        /// <returns>true on success, false on validation failure.</returns>
        public override bool ValidateProperty(string key, ArrayList errors)
        {
            if (key == SqlHelperDatabase.DefaultDB.ToString())
                return ValidateTargetDB(errors);
            else
            {
                // by default validate the key exists
                return ValidateExistence(key, Optionality.Mandatory, errors);
            }

        }

        /// <summary>
        /// Checks target database key containing connection string is valid
        /// </summary>
        /// <param name="errors">Error list.</param>
        /// <returns>true on success, false on validation failure.</returns>
        private bool ValidateTargetDB(ArrayList errors)
        {
            int errorsBefore = errors.Count;

            ValidateExistence(SqlHelperDatabase.DefaultDB.ToString(), Optionality.Mandatory, errors);

            return (errorsBefore == errors.Count);
        }
    }
}
