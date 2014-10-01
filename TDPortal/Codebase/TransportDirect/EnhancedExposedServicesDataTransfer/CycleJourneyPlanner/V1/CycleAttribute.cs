// *********************************************** 
// NAME             : CycleAttribute.cs      
// AUTHOR           : Amit Patel
// DATE CREATED     : 20 Sep 2010
// DESCRIPTION  	: This class represents a single attribute in the cycle journey result 
//                    providing additional information about the cycle journey detail
// ************************************************
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesDataTransfer/CycleJourneyPlanner/V1/CycleAttribute.cs-arc  $
//
//   Rev 1.0   Sep 29 2010 10:39:38   apatel
//Initial revision.
//Resolution for 5609: CCN 0592 EES Web Service for Cycle
// 

using System;
using System.Collections.Generic;
using System.Text;

namespace TransportDirect.EnhancedExposedServices.DataTransfer.CycleJourneyPlanner.V1
{
    /// <summary>
    /// CycleAttribute class, containing information for a cycle attribute
    /// </summary>
    [Serializable]
    public class CycleAttribute
    {
        #region Private Fields
        private int attributeId;
        private string attributeDescription;
        private string attributeType;
        private string attributeGroup;
        private uint attributeInteger;
        private string attributeMask;
        #endregion

        #region Constructor
        /// <summary>
        /// Default Constructor
        /// </summary>
        public CycleAttribute()
        {
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// The id of the attribute
        /// </summary>
        public int AttributeId
        {
            get { return attributeId; }
            set { attributeId = value; }
        }

        /// <summary>
        /// The description of what this attribute represents
        /// e.g. A Road, Footpath, Tunnel, Cattle Grid
        /// </summary>
        public string AttributeDescription
        {
            get { return attributeDescription; }
            set { attributeDescription = value; }
        }

        /// <summary>
        /// Whether this attribute is used for translating a "Link", "Node" or "Stopover"
        /// type attribute, as contained in the relevant CycleJourneyDetail attribute arrays
        /// </summary>
        public string AttributeType
        {
            get { return attributeType; }
            set { attributeType = value; }
        }

        /// <summary>
        /// Specifies the group this attribute belongs to. And therefore which index position
        /// in an attribute array the value can be used to translate
        /// e.g. "ITN" refers to index 0 in the CycleJourneyDetail attribute array,
        /// "User0" refers to index 1, "User1" refers to index 2, etc...
        /// </summary>
        public string AttributeGroup
        {
            get { return attributeGroup; }
            set { attributeGroup = value; }
        }

        /// <summary>
        /// The integer equivalent number identifying this attribute
        /// </summary>
        public uint AttributeInteger
        {
            get { return attributeInteger; }
            set { attributeInteger = value; }
        }

        /// <summary>
        /// The bit mask to use when translating an attribute value from the CycleJourneyDetail
        /// attributes array. e.g. "0x00000001"
        /// </summary>
        public string AttributeMask
        {
            get { return attributeMask; }
            set { attributeMask = value; }
        }
        #endregion

    }
}
