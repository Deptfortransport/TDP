// *********************************************** 
// NAME         : PublicJourneyInterchangeDetail.cs
// AUTHOR       : Mitesh Modi
// DATE CREATED : 21/11/2012
// DESCRIPTION  : PublicJourneyInterchangeDetail class representing the detail of a
// interchange journey leg, e.g. walking
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyControl/PublicJourneyInterchangeDetail.cs-arc  $
//
//   Rev 1.5   Apr 08 2013 15:43:10   mmodi
//Null check on the naptan in the navigation path
//Resolution for 5919: Error with walkit link generation for journey Redhill Rail Station to Addiscombe, Leslie Grove
//
//   Rev 1.4   Mar 27 2013 12:59:44   mmodi
//Capture naptans from navigation path
//Resolution for 5909: Walkit link for accessible journey not from Entrance naptan
//
//   Rev 1.3   Mar 19 2013 12:03:22   mmodi
//Updates to accessible icons logic
//Resolution for 5905: CCN:677a - Accessible Journeys Planner Phase 2
//
//   Rev 1.2   Feb 04 2013 10:39:30   mmodi
//Include start and end location coordinates in geometry
//
//   Rev 1.1   Jan 21 2013 13:18:10   Build
//Clarifying classes
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.0   Dec 06 2012 09:14:48   mmodi
//Initial revision.
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//

using System;
using System.Collections.Generic;
using System.Text;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.UserPortal.LocationService;

namespace TransportDirect.UserPortal.JourneyControl
{
    /// <summary>
    /// PublicJourneyInterchangeDetail class representing the detail of a
    /// interchange journey leg
    /// </summary>
    [Serializable()]
    public class PublicJourneyInterchangeDetail : PublicJourneyContinuousDetail
    {
        #region Private members

        private List<CheckConstraint> checkConstraints = new List<CheckConstraint>();
        private List<AccessibilityType> legAccessibility = new List<AccessibilityType>();
        private List<string> naptansPath = new List<string>();

        private const string originNaptanString = "Origin";
        private const string destinationNaptanString = "Destination";

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor - defined to allow XML serialisation.
        /// </summary>
        public PublicJourneyInterchangeDetail()
            : base()
        {
        }

        /// <summary>
        /// Takes a CJP leg and creates a new intance of this class populated with
        /// the leg information
        /// </summary>
        /// <param name="leg">A journey leg passed back from the CJP</param>
        public PublicJourneyInterchangeDetail(Leg leg, TDLocation publicVia)
            : base(leg, publicVia)
        {
            InterchangeLeg interchangeLeg = leg as InterchangeLeg;

            #region Check constraints

            if ((interchangeLeg.checkConstraints != null) && (interchangeLeg.checkConstraints.Length > 0))
            {
                // Constraint details applying to this interchange leg
                checkConstraints = new List<CheckConstraint>(interchangeLeg.checkConstraints);
            }
            
            #endregion

            #region Leg Accessibility

            if (interchangeLeg.legAccessibility != null)
            {
                // Accessible information applying to this interchange leg
                TransportDirect.JourneyPlanning.CJPInterface.Accessibility cjpLegAccessibility = interchangeLeg.legAccessibility;

                StringBuilder debug = new StringBuilder();

                legAccessibility = PopulateLegAccessibility(cjpLegAccessibility, ref debug);

                if (debug.Length > 0)
                    this.Debug.Add(debug.ToString());
            }
            else if ((interchangeLeg.alight != null) && (interchangeLeg.alight.stop != null) && (interchangeLeg.alight.stop.accessibility != null))
            {
                // If no LegAccessibility, then use the accessible information applying to the alight
                TransportDirect.JourneyPlanning.CJPInterface.Accessibility cjpAccessibilityAlight = interchangeLeg.alight.stop.accessibility;

                StringBuilder debug = new StringBuilder();

                legAccessibility = PopulateLegAccessibility(cjpAccessibilityAlight, ref debug);

                if (debug.Length > 0)
                    this.Debug.Add(string.Concat("Alight", debug.ToString()));
            }

            #endregion

            #region Navigation path

            if (interchangeLeg.navigationPath != null)
            {
                NavigationPath cjpNavPath = interchangeLeg.navigationPath;

                // Set path naptans and coordinates for this interchange leg (used in handing off mapping of walk legs)
                if (cjpNavPath.pathLinks != null)
                {
                    List<OSGridReference> osgrs = new List<OSGridReference>();

                    // Include leg start location
                    osgrs.Add(legStart.Location.GridReference);
                    
                    if (legStart.Location.NaPTANs != null && legStart.Location.NaPTANs.Length > 0)
                        naptansPath.Add(legStart.Location.NaPTANs[0].Naptan);
                    else // If no naptan for leg, add a dummy origin naptan to allow geometry and path naptans to stay in sync
                        naptansPath.Add(originNaptanString);
                    
                    // Path coordinates/naptans
                    foreach (PathLinkInSequence pl in cjpNavPath.pathLinks)
                    {
                        // From location
                        if ((pl.from != null) && (pl.from.coordinate != null))
                        {
                            // Create the OSGR
                            OSGridReference osgr = new OSGridReference(pl.from.coordinate.easting, pl.from.coordinate.northing);

                            // Add the OSGR to the array
                            if (osgr.IsValid)
                                osgrs.Add(osgr);

                            // Add the path naptan
                            if (!string.IsNullOrEmpty(pl.from.NaPTANID))
                                naptansPath.Add(pl.from.NaPTANID);
                        }

                        // To location
                        if ((pl.to != null) && (pl.to.coordinate != null))
                        {
                            // Create the OSGR
                            OSGridReference osgr = new OSGridReference(pl.to.coordinate.easting, pl.to.coordinate.northing);

                            // Add the OSGR to the array
                            if (osgr.IsValid)
                                osgrs.Add(osgr);

                            // Add the path naptan
                            if (!string.IsNullOrEmpty(pl.to.NaPTANID))
                                naptansPath.Add(pl.to.NaPTANID);
                        }
                    }

                    // Include leg end location
                    osgrs.Add(legEnd.Location.GridReference);

                    if (legEnd.Location.NaPTANs != null && legEnd.Location.NaPTANs.Length > 0)
                        naptansPath.Add(legEnd.Location.NaPTANs[0].Naptan);
                    else // If no naptan for leg, add a dummy origin naptan to allow geometry and path naptans to stay in sync
                        naptansPath.Add(destinationNaptanString);

                    this.Geometry = osgrs.ToArray();
                }
            }

            #endregion
        }

        #endregion

        #region Public properties

        /// <summary>
        /// Read/Write. Constraint details applying to this interchange leg
        /// </summary>
        public List<CheckConstraint> CheckConstraints
        {
            get { return checkConstraints; }
            set { checkConstraints = value; }
        }

        /// <summary>
        /// Read/Write. Accessibility details for the interchange leg
        /// </summary>
        public List<AccessibilityType> InterchangeLegAccessibility
        {
            get { return legAccessibility; }
            set { legAccessibility = value; }
        }

        /// <summary>
        /// Read/Write. Naptans path in sequence for this interchange leg. 
        /// Includes the leg start and leg end naptan, and each navigation path link from and to 
        /// naptans (therefore duplicates will exist where for the to/from locations of adjoining nav path links)
        /// </summary>
        public List<string> NaPTANsPath
        {
            get { return naptansPath; }
            set { naptansPath = value; }
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Returns true if this PublicJourneyDetail contains "check constraints" which 
        /// requires an instruction to be displayed in the UI.
        /// May only be true for PublicJourneyInterchangeDetail detail types
        /// </summary>
        public bool HasCheckConstraint()
        {
            if (checkConstraints != null)
            {
                // Only want to display a check constraint instruction for a "security" or "egress" check constraint,
                foreach (CheckConstraint cc in checkConstraints)
                {
                    if ((cc.checkProcess == CheckProcess.securityCheck)
                        || (cc.checkProcess == CheckProcess.baggageSecurityCheck)
                        || (cc.checkProcess == CheckProcess.egress))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Read only. Duration (seconds) total for the check constraints within this PublicJourneyInterchangeDetail
        /// </summary>
        public int DurationCheckConstraints
        {
            get
            {
                int durationSecsCC = 0;

                if (checkConstraints != null)
                {
                    foreach (CheckConstraint cc in checkConstraints)
                    {
                        durationSecsCC += cc.averageDelay * 60;
                    }
                }

                return durationSecsCC;
            }
        }

        #endregion
    }
}