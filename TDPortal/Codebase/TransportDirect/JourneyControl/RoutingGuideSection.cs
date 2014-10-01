// *********************************************** 
// NAME         : RoutingGuideSection.cs
// AUTHOR       : Mitesh Modi
// DATE CREATED : 28/01/2009
// DESCRIPTION  : Implementation of the RoutingGuideSection class
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyControl/RoutingGuideSection.cs-arc  $
//
//   Rev 1.0   Feb 02 2009 17:17:10   mmodi
//Initial revision.
//Resolution for 5223: CCN0385 - TTBO Routeing Guide
//

using System;
using System.Collections.Generic;
using System.Text;

namespace TransportDirect.UserPortal.JourneyControl
{
    /// <summary>
    /// Class to store Routing guide sections for a journey
    /// </summary>
    [Serializable()]
    public class RoutingGuideSection
    {
        #region Private members

        private int id;
        private int[] legIndexes;
        private bool compliant;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public RoutingGuideSection()
        {
            id = -1;
            legIndexes = new int[0];
            compliant = false;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="id">The id of this routing guide section</param>
        /// <param name="legIndexes">Array of leg indexes this routing guide section applies to</param>
        /// <param name="compliant">Is this section routing guide compliant</param>
        public RoutingGuideSection(int id, int[] legIndexes, bool compliant)
        {
            this.id = id;
            this.legIndexes = legIndexes;
            this.compliant = compliant;
        }

        #endregion

        #region Public properties

        /// <summary>
        /// Read only. Returns the Id of this Routing guide section
        /// </summary>
        public int Id
        {
            get { return id; }
        }

        /// <summary>
        /// Read only. Returns an array of leg indexes
        /// </summary>
        public int[] Legs
        {
            get { return legIndexes; }
        }

        /// <summary>
        /// Read only. Indicates if this section is Routing guide compliant
        /// </summary>
        public bool Compliant
        {
            get { return compliant; }
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Returns true if the Legs Index contain the specified Index 
        /// </summary>
        /// <param name="index">The leg index to check</param>
        /// <returns></returns>
        public bool Contains(int index)
        {
            foreach (int leg in legIndexes)
            {
                if (index == leg)
                    return true;
            }

            return false;
        }

        #endregion
    }
}
