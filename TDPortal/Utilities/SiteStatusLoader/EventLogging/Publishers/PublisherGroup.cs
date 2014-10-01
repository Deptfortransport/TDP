// *********************************************** 
// NAME                 : PublisherGroup.cs
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 01/04/2009
// DESCRIPTION			: Abstract class for creating publisher groups
// ************************************************ 
// $Log:   P:/TDPortal/archives/Utilities/SiteStatusLoader/EventLogging/Publishers/PublisherGroup.cs-arc  $
//
//   Rev 1.0   Apr 01 2009 13:30:18   mmodi
//Initial revision.
//Resolution for 5273: Site Status Loader

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using AO.Properties;

using PropertyService = AO.Properties.Properties;

namespace AO.EventLogging
{
    /// <summary>
    /// Abstract factory class for creating publishing groups.
    /// </summary>
    abstract class PublisherGroup
    {
        protected ArrayList publishers;
        protected PropertyService properties;
        protected LoggingPropertyValidator validator;

        /// <summary>
        /// Constructor based on a property provider.</c>
        /// </summary>
        /// <param name="properties">Properties to use to create publisher group.</param>
        public PublisherGroup(PropertyService properties)
        {
            this.properties = properties;
            this.validator = new LoggingPropertyValidator(properties);
            publishers = new ArrayList();
        }

        /// <summary>
        /// Gets the array list of publishers.
        /// </summary>
        public ArrayList Publishers
        {
            get { return publishers; }
        }

        /// <summary>
        /// Creates the publisher group.
        /// </summary>
        /// <param name="errors">Holds errors occurring during publisher creation.</param>
        abstract public void CreatePublishers(ArrayList errors);
    }
}
