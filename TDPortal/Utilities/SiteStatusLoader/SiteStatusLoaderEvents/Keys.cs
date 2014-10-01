// *********************************************** 
// NAME                 : Keys.cs
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 01/04/2009
// DESCRIPTION			: Property keys
// ************************************************ 
// $Log:   P:/TDPortal/archives/Utilities/SiteStatusLoader/SiteStatusLoaderEvents/Keys.cs-arc  $
//
//   Rev 1.0   Aug 23 2011 11:04:38   mmodi
//Initial revision.
//Resolution for 5728: SiteStatusLoader - Updates for daily wrap up file for SJP
//
//   Rev 1.1   Aug 23 2011 10:35:24   mmodi
//Updates to log historic multi-step transactions download data
//Resolution for 5728: SiteStatusLoader - Updates for daily wrap up file for SJP
//
//   Rev 1.0   Apr 01 2009 13:37:14   mmodi
//Initial revision.
//Resolution for 5273: Site Status Loader

using System;
using System.Collections.Generic;
using System.Text;

namespace AO.SiteStatusLoaderEvents
{
    /// <summary>
    /// Container class used to hold key constants
    /// </summary>
    public class Keys
    {
        private const string SiteStatus = "SiteStatusLoaderService";
        private const string Event = SiteStatus + ".Event";

        public const string EventID = Event + ".{0}.ID";
        public const string EventName = Event + ".{0}.Name";
        public const string EventSuccess = Event + ".{0}.Success";
        public const string EventReferenceTransactionType = Event + ".{0}.ReferenceTransactionType";
        public const string EventServiceLevelAgreememt = Event + ".{0}.ServiceLevelAgreement";
        public const string EventAlertThresholdAmber = Event + ".{0}.AlertThreshold.Amber";
        public const string EventAlertThresholdRed = Event + ".{0}.AlertThreshold.Red";

        public const string EventIDChild = Event + ".{0}.ID.Child";
        public const string EventIDChildRefTransType = Event + ".{0}.ID.Child.{1}.ReferenceTransactionType";

        
        /// <summary>
        /// Default constructor
        /// </summary>
        static Keys()
        { }
    }
}
