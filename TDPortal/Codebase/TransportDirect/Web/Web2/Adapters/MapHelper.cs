// *********************************************** 
// NAME                 : MapHelper.cs 
// AUTHOR               : Atos Origin
// DATE CREATED         : 06/02/2004
// DESCRIPTION			: Adapter class for the mapping control TDMap. Contains methods to handle 
//						  serialization and deserialization of map state, and for managing map grid 
//						  references. The perpose of this class is to seperate buiness logic from front
//						  end logic that should be handled by TDMap. 
//
//                        Adapter updated to include methods for use by the new AJAX driven map control
//                        (MapControl2) and the controls which use it.
//
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Adapters/MapHelper.cs-arc  $
//
//   Rev 1.34   Feb 05 2013 13:21:20   mmodi
//Show Walk Interchange in map journey leg dropdown for accessible journey when required
//Resolution for 5873: CCN:677 - Accessible Journeys Planner
//
//   Rev 1.33   Jan 04 2013 15:36:08   mmodi
//Map envelope method and update to stop information return page id
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.32   Mar 26 2010 11:48:20   apatel
//Updated CreateEnvelope method to add 5% padding to envelope coordinates to resolve the problem with the journey involving small walks not zooming enough.
//Resolution for 5485: Maps for a specific journey leg are not zooming to the correct level
//
//   Rev 1.31   Mar 19 2010 11:32:54   mmodi
//Updated creating StopInformation link
//Resolution for 5440: Maps - Issue 16 ESRI fixes
//
//   Rev 1.30   Mar 18 2010 15:21:02   mmodi
//Updates to creating the StopInformation links
//Resolution for 5440: Maps - Issue 16 ESRI fixes
//
//   Rev 1.29   Mar 16 2010 10:39:08   apatel
//Updated Save Map state methods to clear the map state in input page state to start with
//Resolution for 5457: Cycle Journey : Printer friendly issue
//
//   Rev 1.28   Mar 12 2010 09:13:32   apatel
//updated to show Walkit link  on map
//Resolution for 5444: CCN 0568 Walkit page land - Phase 1b
//
//   Rev 1.27   Mar 11 2010 12:33:18   mmodi
//Updated create stop information link methods
//Resolution for 5440: Maps - Issue 16 ESRI fixes
//
//   Rev 1.26   Mar 10 2010 15:19:08   apatel
//Updated to show Walkit links on map information popup window when user clicks on start location of walk leg
//Resolution for 5444: CCN 0568 Walkit page land - Phase 1b
//
//   Rev 1.25   Feb 09 2010 13:05:48   apatel
//updated for cycle printer friendly map
//Resolution for 5399: Cycle Planner Printer Friendly page broken
//
//   Rev 1.24   Jan 19 2010 13:21:32   mmodi
//Updated for New Mapping RFC73 patch
//Resolution for 5376: Maps - ESRI RFC073 mandatory changes needed for patch
//
//   Rev 1.23   Dec 09 2009 11:29:42   mmodi
//Updated set up location methods, and fixed for Visit planner
//
//   Rev 1.22   Dec 08 2009 11:27:10   mmodi
//Hide Expand map button on stop information page
//
//   Rev 1.21   Dec 03 2009 14:03:04   mmodi
//Updated to display Cycle journey direction symbols
//
//   Rev 1.20   Dec 02 2009 11:51:14   apatel
//Input page work flow change for mapping enhancement
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.19   Dec 01 2009 11:09:16   mmodi
//Commited changes back to session after call from the TDP Map web service
//
//   Rev 1.18   Nov 27 2009 13:14:06   mmodi
//Updated with new map journey class
//
//   Rev 1.17   Nov 18 2009 11:20:34   apatel
//Updated for Journey input planner mapping enhancements
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.16   Nov 17 2009 09:19:34   apatel
//Added method to save cycle map tiles.
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.15   Nov 15 2009 11:00:26   mmodi
//Exposed method to get map zoom javascript details for a public transport leg
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.14   Nov 11 2009 09:48:10   apatel
//Updated SetMapState method
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.13   Nov 10 2009 15:27:12   mmodi
//Added Reset printable map values
//
//   Rev 1.12   Nov 10 2009 11:29:44   apatel
//Find Input pages mapping enhancement changes
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.11   Nov 09 2009 15:41:58   mmodi
//Updated journey directions drop down lists to allow zooming using javascript
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.10   Nov 09 2009 13:49:38   apatel
//Map Input page changes
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.9   Nov 06 2009 10:00:08   mmodi
//Added MapJourneyType enum
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.8   Nov 05 2009 14:56:22   apatel
//mapping enhancement code changes
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.7   Nov 04 2009 10:59:08   mmodi
//Updated with map travel news filter class
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.6   Nov 02 2009 17:48:22   mmodi
//Updated for new mapping
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.5   Oct 14 2008 11:41:38   mmodi
//Manual merge for stream5014
//
//   Rev 1.4   Jul 09 2008 14:54:04   mturner
//Fix for IR5046 - Door to Door Maps Produce Error
//
//   Rev 1.3   Jun 18 2008 16:06:28   dgath
//FindRelevantJourney method updated to fix the ITP issues
//
//   Rev 1.2.1.3   Oct 08 2008 11:31:18   mmodi
//Return scale value
//Resolution for 5134: Cycle Planner - The 'scale of map' is not displayed on bottom right corner of 'Cycle Journey Map - Printer Friendly' page
//
//   Rev 1.2.1.2   Sep 02 2008 11:23:32   mmodi
//Log event for each MapTile
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.2.1.1   Aug 22 2008 10:26:14   mmodi
//Updated
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.2.1.0   Jun 20 2008 15:28:00   mmodi
//Updated for cycle journeys
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.2   Mar 31 2008 12:59:10   mturner
//Drop3 from Dev Factory
//
//   Rev DevFactory   Feb 20 2008 14:00:00   mmodi
//Updated to populate results with modetypes value for city to city journeys
//
//   Rev 1.0   Nov 08 2007 13:11:26   mturner
//Initial revision.
//
//   Rev 1.25   Mar 13 2006 17:32:26   NMoorhouse
//Manual merge of stream3353 -> trunk
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.24   Feb 23 2006 17:11:12   RWilby
//Merged stream3129
//
//   Rev 1.23   Feb 21 2006 12:23:14   build
//Automatically merged from branch for stream0009
//
//   Rev 1.22.2.1   Feb 09 2006 19:01:36   aviitanen
//Fxcop and review changes
//
//   Rev 1.22.3.1   Mar 10 2006 09:52:40   NMoorhouse
//Fix problem display maps for car extension
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.22.3.0   Feb 17 2006 14:13:54   NMoorhouse
//Updates (by Richard Hopkins) to support Replan Maps
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.22.2.0   Feb 03 2006 17:17:42   AViitanen
//TD114 High resolution maps: added MapImageProperties class and method for retrieving properties for high res map.
//
//   Rev 1.22   Nov 01 2005 15:12:12   build
//Automatically merged from branch for stream2638
//
//   Rev 1.21.1.0   Oct 14 2005 09:39:10   jbroome
//Removed multipleItineraryJourneys property as not used
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.21   Sep 02 2005 16:10:26   rgreenwood
//IR2745: Moved increment of journeySegmentIndex
//
//   Rev 1.20   Aug 19 2005 14:07:08   jgeorge
//Automatically merged from branch for stream2572
//
//   Rev 1.19.1.1   Aug 04 2005 17:40:14   rgreenwood
//DD073 Map Details: Fixed dropdown content mismatch not covered in design. Switched from using dropdown selected index to dropdown item value to ensure that greyed out legs don't appear in the dropdown, and that only the valid legs are displayed.
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.19.1.0   Jul 26 2005 09:51:44   rgreenwood
//DD073 Map Details: Added HasJourneyGreyedOutMode() method and checks in PopulateSingleJourneySegments() and PopulateMultipleJourneySegments() for invalid coords in the PublicJourneyDetail object
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.20   Jul 25 2005 18:38:44   rgreenwood
//DD073 Map Details: Amended PopulateSingleJourneySegment() and PopulateMultiJourneySegment() methods to check for invalid coordinates in the PublicJourneyDetail object
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.19   Jun 30 2005 15:52:00   pcross
//Updated handling of text in map leg selection box. Text is of format "Car leg 5 to end". This needs different arrangement if Welsh.
//Also replaced hardcoded "Direction" for car directions with lookup to resource files.
//Resolution for 2367: DEL 7 Welsh text missing
//
//   Rev 1.18   May 04 2005 15:11:18   asinclair
//Added StopoverSections to dropdown for Maps
//
//   Rev 1.17   Apr 27 2005 16:29:20   pcross
//IR2355. Handle scenario where there is a single leg in a journey and therefore no need for an associated leg breakdown
//Resolution for 2355: View map dropdown for single leg journey
//
//   Rev 1.16   Apr 27 2005 15:05:54   pcross
//IR2353. Map view dropdown combo Welsh translation capability
//Resolution for 2353: Map leg dropdown box doesn't handle Welsh translation
//
//   Rev 1.15   Apr 27 2005 10:56:04   pcross
//IR2192. Corrections to extended journey handling. Node icons on return journey were incorrectly numbered.
//
//   Rev 1.14   Apr 26 2005 13:17:14   pcross
//IR2192. Corrections to extended journey handling
//
//   Rev 1.13   Apr 26 2005 10:14:36   pcross
//IR2192. Changed get nodes methods be able to handle extended journeys.
//Resolution for 2192: Display normal map page, add numbered start and end legs
//
//   Rev 1.12   Apr 22 2005 15:58:30   pcross
//IR2192. Added support for adding intermediate node icons onto maps.
//
//   Rev 1.11   Apr 08 2005 16:07:54   rhopkins
//Modified methods, and added new ones, for testing for presence of public/private results
//
//   Rev 1.10   Mar 30 2005 17:03:28   asinclair
//Fixed drop down so that Stopover sections are not returned
//
//   Rev 1.9   Mar 18 2005 15:18:50   asinclair
//Updated to set drop downs for car drive sections
//
//   Rev 1.8   Sep 17 2004 15:13:42   COwczarek
//Removal of unadjusted road journey planning
//Resolution for 1564: Remove unadjusted route functionality
//
//   Rev 1.7   Jun 22 2004 13:39:42   jbroome
//Ensured correct numbering in return journey drop downs.
//
//   Rev 1.6   Jun 08 2004 17:17:14   jbroome
//Ensured correct ordering of journey segments in drop down list when  displaying Return Itinerary journeys.
//
//   Rev 1.5   Jun 08 2004 14:52:08   RPhilpott
//Add method to get modes used by returned journey.
//
//   Rev 1.4   Jun 07 2004 15:05:38   jbroome
//ExtendJourney - added PopulateMultiJourneySegments and updated to check for use of TDItineraryManager.
//
//   Rev 1.3   Apr 16 2004 18:59:36   CHosegood
//Refactored for new ModeType enum type.
//Resolution for 663: Rail fares not being displayed
//Resolution for 697: Bus replacement change
//
//   Rev 1.2   Apr 07 2004 18:42:18   asinclair
//Updated to Save TrafficMaps viewstate
//
//   Rev 1.1   Mar 31 2004 09:26:26   CHosegood
//PopulateJourneySegments method now clears the dropdown lists passed in before populating it with new contents.
//Resolution for 674: DEL 5.2 Map QA changes
//
//   Rev 1.0   Mar 01 2004 16:03:32   CHosegood
//Initial Revision

#region Using
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using TransportDirect.Common;
using TransportDirect.Common.DatabaseInfrastructure;
using TransportDirect.Common.Logging;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ResourceManager;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.UserPortal.CyclePlannerControl;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.UserPortal.Resource;
using TransportDirect.UserPortal.ScreenFlow;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.Web.Controls;
using TransportDirect.Web.Support;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.Presentation.InteractiveMapping;

using Logger = System.Diagnostics.Trace;
using TDPublicJourney = TransportDirect.UserPortal.JourneyControl.PublicJourney;

#endregion

namespace TransportDirect.UserPortal.Web.Adapters
{
    /// <summary>
	/// Helper class used in Maps.
	/// </summary>
	public class MapHelper: TDWebAdapter
    {
        #region Private members

        private const string abandonKey = "abandon";
        private const string abandonQueryString = "abandon=true";

        // Constants used in the javascript file MapJourneyDisplayDetailsControl.js.
        // These indicate which zoom method to use when zooming to a journey
        private const string JOURNEY_ZOOM_JUNCTION = "J";
        private const string JOURNEY_ZOOM_TOID = "T";
        private const string JOURNEY_ZOOM_ENVELOPE = "E";
        private const string JOURNEY_ZOOM_POINT = "P";
        private const string JOURNEY_ZOOM_ALLROUTES = "AR";

        private string baseChannelURL = @"~/";
        
        private string unformattedLegDescription = string.Empty;
        private string startJourneyDescription = string.Empty;
        private string endJourneyDescription = string.Empty;
        private string legDescriptionPadding = string.Empty;
        private string directionText = string.Empty;

        private string toidPrefix = string.Empty;

        private int envelopeMinX = 200000;
        private int envelopeMinY = 120000;
        private int envelopeMaxX = 630000;
        private int envelopeMaxY = 850000;

        #endregion

        #region Map Zoom Legends
        /// <summary>
		/// Street View
		/// 1:10,000
		/// </summary>
		public const int OSSTREETVIEW = 10000;

		/// <summary>
		/// 1:50,000
		/// </summary>
		public const int SCALECOLOURRASTER50 = 50000;

		/// <summary>
		/// 1:250,000
		/// </summary>
		public const int SCALECOLOURRASTER250 = 250000;

		/// <summary>
		/// 1:1,000,000
		/// </summary>
		public const int MINISCALE = 1000000;
		#endregion

        #region Maping locations

        public const string PARKING_EXIT = "Exit";
        public const string PARKING_ENTRANCE = "Entrance";
        public const string PARKING_MAP = "Map";

        #endregion

        #region Constructor

        /// <summary>
		/// class constructor
		/// </summary>
		public MapHelper()
		{
            LoadResources();
        }

        #endregion

        #region Public methods

        #region View state

        /// <summary>
		/// Scans the map state for instances of substituted serialisable objects and converts them
		/// back to their original object types
		/// </summary>
		/// <param name="o">Object graph representing the map state that has been deserialised</param>
		/// <returns>Object graph representing map state with serialisable items substituted with
		/// original object types</returns>
		public object MirrorToPair( object o )
		{
			//store o in tempSerPair to avoid unnecessary casting
			SerPair tempSerPair = o as SerPair;

			if (tempSerPair != null)
			{
				SerPair p = tempSerPair;
				return new Pair( MirrorToPair(p.First), MirrorToPair(p.Second) );
			}

			//replacing TDSerUnit instances with Unit instances.
			ArrayList tempList = o as ArrayList;
			if (tempList != null)			
			{
				ArrayList oldItems = tempList;
				ArrayList newItems = new ArrayList();

				foreach(object obj in oldItems)
				{
					TDSerUnit tempSerUnit = obj as TDSerUnit;

					if (tempSerUnit != null)
					{
						TDSerUnit serUnit = tempSerUnit;
						Unit unit = new Unit(serUnit.Value, serUnit.Type);
						newItems.Add(unit);
					}
					else newItems.Add(obj);
				}
				o = newItems;
			}
			return o;
		}

		/// <summary>
		/// Scans the map state for instances of non-serialisable objects and converts them
		/// into serialisable counterparts
		/// </summary>
		/// <param name="o">Object graph representing the map state that is to be serialized</param>
		/// <returns>Object graph representing maps state with non-serialisable items substituted with
		/// serializable counterparts</returns>
		public object MirrorToSerPair( object o )
		{
			Pair tempPair = o as Pair;

			if (tempPair != null)
			//if( o is Pair )
			{
				Pair p = tempPair;
				return new SerPair( MirrorToSerPair(p.First), MirrorToSerPair(p.Second) );
			}
			
			//replacing any Unit instances with TDSerUnit instances.
			ArrayList tempList = o as ArrayList;
			if (tempList != null)
			{
				ArrayList oldItems = tempList;
				ArrayList newItems = new ArrayList();

				foreach (object obj in oldItems)
				{
					if (obj is Unit)
					{
						Unit unit = (Unit)obj;
						TDSerUnit serUnit = new TDSerUnit();
						serUnit.Value = unit.Value;
						newItems.Add(serUnit);
					}
					else newItems.Add(obj);
				}
				o = newItems;
			}

			else
			{
				Logger.Write( new OperationalEvent( TDEventCategory.Business, TDTraceLevel.Verbose, Format( o ) ) );
			}
			return o;
        }

        #endregion

        /// <summary>
		/// Return the URL for the map legend for the supplied scale
		/// if one exists, otherwise return an empty string.
		/// </summary>
		/// <param name="scale"></param>
		/// <returns></returns>
		public string getLegandUrl( int scale ) 
		{
			string mapUrlKey = "Web.MapLegend.Url.";

			if( (scale > 0) && (scale <= OSSTREETVIEW) )
			{
				mapUrlKey += OSSTREETVIEW;
			}
			else if( scale <= SCALECOLOURRASTER50 )
			{
				mapUrlKey += SCALECOLOURRASTER50;
			}
			else if( scale <= SCALECOLOURRASTER250 )
			{
				mapUrlKey += SCALECOLOURRASTER250;
			}
			else if( scale <= MINISCALE )
			{
				mapUrlKey += MINISCALE;
			} 
			else 
			{
				mapUrlKey = string.Empty;
			}

			return Properties.Current[mapUrlKey];
		}

        /// <summary>
		/// Returns an array of the changes of location (nodes) in a journey.
		/// Only returns intermediate nodes - ie start and end location details are omitted.
		/// </summary>
		/// <param name="outward"></param>
		/// <param name="usingItinerary"></param>
		public OSGridReference[] FindIntermediateNodesGridReferences(bool outward)
		{
			TDItineraryManager itineraryManager = TDItineraryManager.Current;
			int itineraryJourneyCount = itineraryManager.Length;
			OSGridReference[] overallJourneyGridReferences = new OSGridReference[0];

			// If an extended journey has been chosen and we are viewing the full itinerary then we could have a mixture
			// of public and private transport.
			// We need to get all nodes from each journey and then remove the start and end nodes from the overall journey
			// to get the intermediate nodes for the overall journey.
			if (itineraryJourneyCount > 0 && itineraryManager.FullItinerarySelected)
			{
				overallJourneyGridReferences = FindIntermediateNodesGridReferencesFullItinerary(outward);
			}
			else
			{
				// Either a single journey in the itinerary has been selected or there is only one journey in the search
				overallJourneyGridReferences = FindIntermediateNodesGridReferencesSingle(outward);
			}

			// We now have an array of all locations in the journey. Strip off start and end locations to
			// be left with intermediate nodes.
			int overallJourneyArrayIndex2 = 0;
			int intermediateNodesArrayIndex = 0;
			int overallJourneyGridRefCount = overallJourneyGridReferences.Length;
			OSGridReference[] intermediateNodesGridReferences = new OSGridReference[overallJourneyGridRefCount - 2];
			foreach (OSGridReference osGridRefs in overallJourneyGridReferences)
			{
				if (overallJourneyArrayIndex2 == 0 || overallJourneyArrayIndex2 >= overallJourneyGridRefCount - 1)
				{
					// Discard the start and end locations
				}
				else
				{
					// Include all intermediate nodes
					intermediateNodesGridReferences[intermediateNodesArrayIndex] = osGridRefs;
					intermediateNodesArrayIndex++;
				}
				overallJourneyArrayIndex2++;
			}

			return intermediateNodesGridReferences;

        }

        /// <summary>
        /// Method uses an array of osgrid reference to determine the max and min eastings and northings.
        /// Returns two OSGridReferences in an array, the first is min OSGR and the second is the max OSGR.
        /// </summary>
        /// <param name="osgr">Array of osgr's used to determine the outer boundaries of the journey map</param>
        public OSGridReference[] CreateMapEnvelope(OSGridReference[] osgr)
        {
            double minEasting = double.MaxValue;
            double maxEasting = double.MinValue;
            double minNorthing = double.MaxValue;
            double maxNorthing = double.MinValue;

            for (int i = 0; i < osgr.Length; i++)
            {
                if (osgr[i] != null)
                {
                    //compare Easting to current min and max
                    if (osgr[i].Easting != -1)
                    {
                        minEasting = Math.Min(osgr[i].Easting, minEasting);
                        maxEasting = Math.Max(osgr[i].Easting, maxEasting);
                    }
                    //compare Northing to current min and max
                    if (osgr[i].Northing != -1)
                    {
                        minNorthing = Math.Min(osgr[i].Northing, minNorthing);
                        maxNorthing = Math.Max(osgr[i].Northing, maxNorthing);
                    }
                }
            }

            double eastingPadding = (maxEasting - minEasting) / 20;
            minEasting = minEasting - eastingPadding;
            maxEasting = maxEasting + eastingPadding;

            double northingPadding = (maxNorthing - minNorthing) / 20;
            minNorthing = minNorthing - northingPadding;
            maxNorthing = maxNorthing + northingPadding;

            // Create the map osgr envelope to return
            OSGridReference[] envelopeOSGR = new OSGridReference[2];
            envelopeOSGR[0] = new OSGridReference(Convert.ToInt32(minEasting), Convert.ToInt32(minNorthing));
            envelopeOSGR[1] = new OSGridReference(Convert.ToInt32(maxEasting), Convert.ToInt32(maxNorthing));

            return envelopeOSGR;
        }

        /// <summary>
        /// Method uses the center poitn and an array of osgrid reference to 
        /// determine the max and min eastings and northings.
        /// Returns two OSGridReferences in an array, the first is min OSGR and the second is the max OSGR.
        /// </summary>
        /// <param name="osgr">Array of osgr's used to determine the outer boundaries of the journey map</param>
        public OSGridReference[] CreateMapEnvelope(OSGridReference center, OSGridReference[] osgrs)
        {
            int furthestDistance = 0;
            int distance = 0;

            // Determine the coordinate furthest from the center point,
            // could be a far better calculation than using brute force!
            foreach (OSGridReference osgr in osgrs)
            {
                distance = osgr.DistanceFrom(center);
                if (distance > furthestDistance)
                {
                    furthestDistance = distance;
                }
            }

            double minEasting = double.MaxValue;
            double maxEasting = double.MinValue;
            double minNorthing = double.MaxValue;
            double maxNorthing = double.MinValue;

            // minGridReference = X0-Radius ; Y0-Radius
            // 5% tolerance because of icons sometimes not shown entirely,
            minEasting = center.Easting - ((int)(furthestDistance * 1.05));
            minNorthing = center.Northing - ((int)(furthestDistance * 1.05));

            // maxGridReference = X0+Radius ; Y0+Radius
            maxEasting = center.Easting + ((int)(furthestDistance * 1.05));
            maxNorthing = center.Northing + ((int)(furthestDistance * 1.05));

            // Check envelope is valid
            if (minEasting < envelopeMinX) minEasting = envelopeMinX;
            if (minNorthing < envelopeMinY) minNorthing = envelopeMinY;
            if (maxEasting > envelopeMaxX) maxEasting = envelopeMaxX;
            if (maxNorthing > envelopeMaxY) maxNorthing = envelopeMaxY;

            // Create the map osgr envelope to return
            OSGridReference[] envelopeOSGR = new OSGridReference[2];
            envelopeOSGR[0] = new OSGridReference(Convert.ToInt32(minEasting), Convert.ToInt32(minNorthing));
            envelopeOSGR[1] = new OSGridReference(Convert.ToInt32(maxEasting), Convert.ToInt32(maxNorthing));

            return envelopeOSGR;
        }

        #region Database

        /// <summary>
        /// Expands a journey route in the maps database
        /// </summary>
        /// <param name="sessionId"></param>
        /// <param name="routeNumber"></param>
        public void ExpandJourneyRouteInDatabase(string sessionId, int routeNumber)
        {
            try
            {
                //Expand the routes for use
                SqlHelper sqlHelper = new SqlHelper();
                sqlHelper.ConnOpen(SqlHelperDatabase.EsriDB);
                Hashtable htParameters = new Hashtable(2);
                htParameters.Add("@SessionID", sessionId);
                htParameters.Add("@RouteNum", routeNumber);
                sqlHelper.Execute("usp_ExpandRoutes", htParameters);
            }
            catch (Exception ex)
            {
                OperationalEvent operationalEvent = new OperationalEvent
                    (TDEventCategory.ThirdParty, TDTraceLevel.Error, "ESRI Map Exception expanding journey route. " + ex.Message);

                Logger.Write(operationalEvent);
            }
        }

        #endregion

        #region Journey legs dropdown Lists

        /// <summary>
        /// Populates the journey drop down in the journey map control.
        /// User can select specific segment or entire journey to be displayed on map.
        /// </summary>
        /// <param name="list"></param>
        /// <param name="outward"></param>
        /// <param name="usingItinerary"></param>
        /// <param name="javascript">If the list values should be updated to include details used by javascript functions</param>
        public void PopulateSingleJourneySegment(DropDownList list, bool outward, bool usingItinerary, bool javascript)
        {

            int origIndex = list.SelectedIndex;
            //Empty any existing contents of the DropDownList
            list.Items.Clear();

            Journey journey = FindRelevantJourney(outward);

            if (journey is JourneyControl.PublicJourney)
            {
                #region Public journey
                JourneyControl.PublicJourney publicJourney = (JourneyControl.PublicJourney)journey;

                // The first available map is the map of the whole journey.
                list.Items.Add(new ListItem(Global.tdResourceManager.GetString("JourneyMapControl.labelMapOfEntireJourney", TDCultureInfo.CurrentUICulture), "0"));

                // If there is only 1 leg to journey then we only need to show 'entire journey' option in dropdown
                // instead of getting the single leg and adding that to the list
                if (publicJourney.Details.Length > 1)
                {
                    // Loop thought the leg details for the public journey once
                    // to determine which modes occur more than once (as these
                    // will have to appended with a number to uniquely identify them).

                    int journeySegmentIndex = 0;
                    
                    unformattedLegDescription = Global.tdResourceManager.GetString("MapToolsControl.PTLegDescription", TDCultureInfo.CurrentUICulture);

                    // start & end node description (other nodes are numbered)
                    startJourneyDescription = Global.tdResourceManager.GetString("MapToolsControl.StartJourneyDescription", TDCultureInfo.CurrentUICulture);
                    endJourneyDescription = Global.tdResourceManager.GetString("MapToolsControl.EndJourneyDescription", TDCultureInfo.CurrentUICulture);

                    // Can't seem to read in a space from langStrings file so reading in a # and replacing with a space
                    legDescriptionPadding = Global.tdResourceManager.GetString("MapToolsControl.PTLegDescriptionPadding", TDCultureInfo.CurrentUICulture);
                    legDescriptionPadding = legDescriptionPadding.Replace("#", " ");

                    for (int detailIndex = 0; detailIndex < publicJourney.Details.Length; detailIndex++)
                    {
                        PublicJourneyDetail detail = publicJourney.Details[detailIndex];
                                                
                        if (!detail.HasInvalidCoordinates)
                        {
                            ListItem listItem = GetListItemForDetail(detail, journeySegmentIndex, publicJourney, publicJourney.Details.Length, javascript);

                            list.Items.Add(listItem);
                        }
                        
                        journeySegmentIndex++;
                    }
                }
                #endregion
            }
            else if (journey is CyclePlannerControl.CycleJourney)
            {
                #region Cycle journey
                // List is for cycle journer
                CyclePlannerControl.CycleJourney cycleJourney = (CyclePlannerControl.CycleJourney)journey;

                startJourneyDescription = Global.tdResourceManager.GetString("JourneyMapControl.labelMapOfEntireJourney", TDCultureInfo.CurrentUICulture);
                directionText = Global.tdResourceManager.GetString("JourneyMapControl.Direction", TDCultureInfo.CurrentUICulture);

                if (!javascript)
                {
                    #region Setup drop down list for the cycle journey

                    list.Items.Add(startJourneyDescription);
                                        
                    int index;
                    for (index = 1; index < cycleJourney.Details.Length + 3; index++)
                    {
                        list.Items.Add(new ListItem(directionText + " " + index, index.ToString()));
                    }

                    //This removes the 'Directions no' for a journey involving a ferry.
                    int realIndex;
                    for (realIndex = cycleJourney.Details.Length - 1; realIndex > 0; realIndex--)
                    {
                        if (cycleJourney.Details[realIndex].Ferry == true)
                        {
                            list.Items.RemoveAt(realIndex + 2);
                        }
                    }

                    #endregion
                }
                else
                {
                    #region Setup drop down list for the cycle journey zoomed using javascript

                    string listItemValue = "0";
                                        
                    #region Add first item - Map of entire journey

                    // Direction 0
                    list.Items.Add(new ListItem(startJourneyDescription, listItemValue));

                    #endregion

                    #region Add start location direction number

                    // Direction 1
                    OSGridReference osgrStartLocation = cycleJourney.OriginLocation.GridReference;

                    listItemValue = "1," + JOURNEY_ZOOM_POINT + ","
                        + osgrStartLocation.Easting.ToString() + "," + osgrStartLocation.Northing.ToString();

                    list.Items.Add(new ListItem(directionText + " 1", listItemValue));

                    #endregion

                    #region Add cycle journey detail 1 - same as start location

                    // Direction 2
                    listItemValue = "2," + JOURNEY_ZOOM_POINT + ","
                        + osgrStartLocation.Easting.ToString() + "," + osgrStartLocation.Northing.ToString();

                    list.Items.Add(new ListItem(directionText + " 2", listItemValue));

                    #endregion

                    #region Add all other directions, cycle journey detail 2 onwards

                    CycleJourneyDetail currentDetail;
                    CycleJourneyDetail previousDetail;

                    // Start at the 2 item in the array details[1].
                    for (int index = 3; index < cycleJourney.Details.Length + 2; index++)
                    {
                        currentDetail = cycleJourney.Details[index - 2];
                        previousDetail = cycleJourney.Details[index - 3];

                        #region Create list item value

                        if (previousDetail.StopoverSection && !currentDetail.StopoverSection)
                        {
                            // If the previous section was a StopoverSection, but the current one isn't
                            // then it is the first drive section after a Stopover, so
                            // we need to use the ITNNode method to display the map

                            // Allow Zoom to UTurn instruction
                            string ITNToid = string.Empty;
                            if (previousDetail.NodeToid != null && previousDetail.NodeToid.Length != 0)
                            {
                                ITNToid = previousDetail.NodeToid;

                                if (toidPrefix.Length > 0 && ITNToid.StartsWith(toidPrefix))
                                {
                                    ITNToid = ITNToid.Substring(toidPrefix.Length);
                                }

                                listItemValue = index.ToString() + "," + JOURNEY_ZOOM_TOID + "," + ITNToid;
                            }
                            else
                            {
                                // This is a U-Turn so we don't have a Node
                                // Use the OSGR of the section start instead.
                                listItemValue = index.ToString() + "," + JOURNEY_ZOOM_POINT + ","
                                    + currentDetail.StartOSGR.Easting.ToString() + "," 
                                    + currentDetail.StartOSGR.Northing.ToString();
                            }                            
                        }
                        //If the current CycleJourneyDetail is not a StopoverSection
                        else if (!currentDetail.StopoverSection)
                        {
                            // If this detail is not a StopoverSection, then we need to zoom to 
                            // the intersection Junction of where the previous detail joins the current
                            // detail

                            // Variables to hold OSGR found
                            OSGridReference intersectionOSGR = null;
                            OSGridReference firstOSGR = null;

                            GetIntersectionCoordinate(previousDetail.Geometry, currentDetail.Geometry,
                                out intersectionOSGR, out firstOSGR);

                            if (intersectionOSGR != null)
                            {
                                listItemValue = index.ToString() + "," + JOURNEY_ZOOM_POINT + ","
                                    + intersectionOSGR.Easting.ToString() + ","
                                    + intersectionOSGR.Northing.ToString();
                            }
                            else
                            {
                                listItemValue = index.ToString() + "," + JOURNEY_ZOOM_POINT + ","
                                    + firstOSGR.Easting.ToString() + ","
                                    + firstOSGR.Northing.ToString();
                            }
                        }
                        // The current CycleJourneyDetail is a StopoverSection
                        else
                        {
                            // If the current section is a StopoverSection, then zoom to 
                            // the ITNNode

                            string ITNToid = string.Empty;
                            if (currentDetail.NodeToid != null && currentDetail.NodeToid.Length != 0)
                            {
                                ITNToid = currentDetail.NodeToid;

                                if (toidPrefix.Length > 0 && ITNToid.StartsWith(toidPrefix))
                                {
                                    ITNToid = ITNToid.Substring(toidPrefix.Length);
                                }

                                listItemValue = index.ToString() + "," + JOURNEY_ZOOM_TOID + "," + ITNToid;
                            }
                            else
                            {
                                // We don't have a Node as this is a Point on a link being used as a via.
                                //
                                // Instead try to find the last point of the previous section as this should 
                                // be the same Point as this Via. This is found in the last value in the array 
                                // of geometry objects.
                                //
                                // We can then zoom to that Point.
                                OSGridReference previousEndPoint = (OSGridReference)previousDetail.Geometry[previousDetail.Geometry.Count - 1].GetValue(previousDetail.Geometry[previousDetail.Geometry.Count - 1].Length - 1);

                                listItemValue = index.ToString() + "," + JOURNEY_ZOOM_POINT + ","
                                    + previousEndPoint.Easting.ToString() + ","
                                    + previousEndPoint.Northing.ToString();
                            }
                        }

                        #endregion

                        // Add to the drop down list
                        list.Items.Add(new ListItem(directionText + " " + index, listItemValue));
                    }

                    //This removes the 'Directions no' for a journey involving a ferry.
                    for (int realIndex = cycleJourney.Details.Length - 1; realIndex > 0; realIndex--)
                    {
                        if (cycleJourney.Details[realIndex].Ferry == true)
                        {
                            list.Items.RemoveAt(realIndex + 2);
                        }
                    }

                    #endregion

                    #region Add end location direction number

                    // Direction last
                    OSGridReference osgrEndLocation = cycleJourney.DestinationLocation.GridReference;

                    listItemValue = (cycleJourney.Details.Length + 2) + "," + JOURNEY_ZOOM_POINT + ","
                        + osgrEndLocation.Easting.ToString() + "," + osgrEndLocation.Northing.ToString();

                    list.Items.Add(new ListItem(directionText + " " + (cycleJourney.Details.Length + 2), listItemValue));

                    #endregion

                    #endregion
                }
                #endregion
            }
            else
            {
                #region Car journey
                //List is for car Journeys and we now need to include the individual sections

                // Get the road journey
                JourneyControl.RoadJourney privatejourney = (JourneyControl.RoadJourney)journey;

                startJourneyDescription = Global.tdResourceManager.GetString("JourneyMapControl.labelMapOfEntireJourney", TDCultureInfo.CurrentUICulture);
                directionText = Global.tdResourceManager.GetString("JourneyMapControl.Direction", TDCultureInfo.CurrentUICulture);

                if (!javascript)
                {
                    #region Setup drop down list for the car journey

                    list.Items.Add(startJourneyDescription);

                    int index;
                    for (index = 1; index < privatejourney.Details.Length + 3; index++)
                    {
                        list.Items.Add(new ListItem(directionText + " " + index, index.ToString()));
                    }

                    //This removes the 'Directions no' for a journey involving a ferry.
                    int realIndex;
                    for (realIndex = privatejourney.Details.Length - 1; realIndex > 0; realIndex--)
                    {
                        if (privatejourney.Details[realIndex].IsFerry == true)
                        {
                            list.Items.RemoveAt(realIndex + 2);
                        }
                    }
                    #endregion
                }
                else
                {
                    #region Setup drop down list for the car journey zoomed using javascript

                    string listItemValue = "0";

                    #region Add first item - Map of entire journey

                    // If it is to a Park and Ride location, then first item should zoom to a map envelope.
                    // Where no envelope is set, then the javascript function will ensure the map
                    // zooms to the entire journey.
                    if (privatejourney.DestinationLocation.ParkAndRideScheme != null)
                    {
                        OSGridReference[] osgrEnvelope = null;

                        #region Set journey zoom envelope for Park and Ride journey

                        CarParkInfo parkAndRideCarPark = null;

                        if (privatejourney.DestinationLocation.CarPark == null)
                        {
                            string[] carParkToid = { privatejourney.Details[privatejourney.Details.Length - 1].Toid[privatejourney.Details[privatejourney.Details.Length - 1].Toid.Length - 1] };
                            parkAndRideCarPark = privatejourney.DestinationLocation.ParkAndRideScheme.MatchCarPark(carParkToid);
                        }

                        if (parkAndRideCarPark != null)
                        {
                            OSGridReference[] osGridArray = {privatejourney.OriginLocation.GridReference,
														parkAndRideCarPark.GridReference,
														privatejourney.DestinationLocation.ParkAndRideScheme.SchemeGridReference,
														((privatejourney.RequestedViaLocation !=null) ? privatejourney.RequestedViaLocation.GridReference : null)
													};

                            if (osGridArray.Length > 0)
                            {
                                //create the map envelope using the osgrids that are provided. 
                                osgrEnvelope = CreateMapEnvelope(osGridArray);
                            }
                        }
                        else
                        {
                            OSGridReference[] osGridArray = {privatejourney.OriginLocation.GridReference,
														((privatejourney.DestinationLocation.CarPark!=null) ? privatejourney.DestinationLocation.CarPark.GridReference : null),
														privatejourney.DestinationLocation.ParkAndRideScheme.SchemeGridReference,
														((privatejourney.RequestedViaLocation!=null) ? privatejourney.RequestedViaLocation.GridReference : null)
													};

                            if (osGridArray.Length > 0)
                            {
                                //create the map envelope using the osgrids that are provided. 
                                osgrEnvelope = CreateMapEnvelope(osGridArray);
                            }
                        }

                        #endregion

                        if ((osgrEnvelope != null) && (osgrEnvelope.Length > 0))
                        {
                            listItemValue += "," + JOURNEY_ZOOM_ENVELOPE + "," + GetOSGRsAsString(osgrEnvelope);
                        }
                    }
                    
                    // Direction 0
                    list.Items.Add(new ListItem(startJourneyDescription, listItemValue));

                    #endregion
                                        
                    #region Add start location direction number

                    // Direction 1
                    OSGridReference osgrStartLocation = privatejourney.OriginLocation.GridReference;

                    listItemValue = "1," + JOURNEY_ZOOM_POINT + ","
                        + osgrStartLocation.Easting.ToString() + "," + osgrStartLocation.Northing.ToString();

                    list.Items.Add(new ListItem(directionText + " 1", listItemValue));

                    #endregion

                    #region Add private journey detail 1 - same as start location

                    // Direction 2
                    listItemValue = "2," + JOURNEY_ZOOM_POINT + ","
                        + osgrStartLocation.Easting.ToString() + "," + osgrStartLocation.Northing.ToString();

                    list.Items.Add(new ListItem(directionText + " 2", listItemValue));

                    #endregion

                    #region Add all other directions, private journey detail 2 onwards

                    RoadJourneyDetail currentDetail;
                    RoadJourneyDetail previousDetail;

                    // Start at the 2 item in the array details[1].
                    for (int index = 3; index < privatejourney.Details.Length + 2; index++)
                    {
                        currentDetail = privatejourney.Details[index - 2];
                        previousDetail = privatejourney.Details[index - 3];

                        #region Create list item value

                        if (previousDetail.IsStopOver && !currentDetail.IsStopOver)
                        {
                            // If the previous section was a StopoverSection, but the current one isn't
                            // then it is the first drive section after a Stopover, so
                            // we need to use the ITNNode method to display the map

                            string ITNToid = previousDetail.nodeToid;

                            if (toidPrefix.Length > 0 && ITNToid.StartsWith(toidPrefix))
                            {
                                ITNToid = ITNToid.Substring(toidPrefix.Length);
                            }

                            listItemValue = index.ToString() + "," + JOURNEY_ZOOM_TOID + "," + ITNToid;
                        }
                        //If the current RoadJourneyDetail is not a StopoverSection
                        else if (!currentDetail.IsStopOver)
                        {
                            // If this detail is not a StopoverSection, then we need to zoom to 
                            // the Junction of two ITNNodes, where the previous detail joins the current
                            // detail

                            string firstToid = previousDetail.RoadJourneyDetailMapInfo.lastToid;
                            string lastToid = currentDetail.RoadJourneyDetailMapInfo.firstToid;

                            if (toidPrefix.Length > 0 && firstToid.StartsWith(toidPrefix))
                            {
                                firstToid = firstToid.Substring(toidPrefix.Length);
                            }

                            if (toidPrefix.Length > 0 && lastToid.StartsWith(toidPrefix))
                            {
                                lastToid = lastToid.Substring(toidPrefix.Length);
                            }

                            listItemValue = index.ToString() + "," + JOURNEY_ZOOM_JUNCTION + "," 
                                + firstToid + "," + lastToid;
                        }
                        // The current RoadJourneyDetail is a StopoverSection
                        else
                        {
                            // If the current section is a StopoverSection, then zoom to 
                            // the ITNNode
                            
                            string ITNToid = currentDetail.nodeToid;

                            if (toidPrefix.Length > 0 && ITNToid.StartsWith(toidPrefix))
                            {
                                ITNToid = ITNToid.Substring(toidPrefix.Length);
                            }

                            listItemValue = index.ToString() + "," + JOURNEY_ZOOM_TOID + "," + ITNToid;
                        }

                        #endregion

                        // Add to the drop down list
                        list.Items.Add(new ListItem(directionText + " " + index, listItemValue));
                    }

                    //This removes the 'Directions no' for a journey involving a ferry.
                    for (int realIndex = privatejourney.Details.Length - 1; realIndex > 0; realIndex--)
                    {
                        if (privatejourney.Details[realIndex].IsFerry == true)
                        {
                            list.Items.RemoveAt(realIndex + 2);
                        }
                    }

                    #endregion

                    #region Add end location direction number

                    // Direction last
                    OSGridReference osgrEndLocation = privatejourney.DestinationLocation.GridReference;

                    listItemValue = (privatejourney.Details.Length + 2) + "," + JOURNEY_ZOOM_POINT + ","
                        + osgrEndLocation.Easting.ToString() + "," + osgrEndLocation.Northing.ToString();

                    list.Items.Add(new ListItem(directionText + " " + (privatejourney.Details.Length + 2), listItemValue));

                    #endregion

                    #endregion
                }

                #endregion
            }
            if (!TDItineraryManager.Current.ItineraryManagerModeChanged) list.SelectedIndex = origIndex;
        }
        
        /// <summary>
		/// Populates the journey drop down in the journey map control when using itinerary.
		/// User can select specific segment or entire itinerary to be displayed on map.
		/// </summary>
		/// <param name="list"></param>
		/// <param name="outward"></param>
		/// <param name="noOfJourneys"></param>
        /// <param name="javascript">If the list values should be updated to include details used by javascript functions</param>
		public void PopulateMultiJourneySegments( DropDownList list, bool outward , int noOfJourneys, bool javascript)
		{
			TDItineraryManager itineraryManager = TDItineraryManager.Current;

			int origIndex = list.SelectedIndex;
			Journey[] journeys;
			Journey journey;
			bool firstJourney = true;

			//Empty any existing contents of the DropDownList
			list.Items.Clear();

			// All entries to follow same format
			int journeySegmentIndex = 0;
			string unformattedLegDescription = Global.tdResourceManager.GetString("MapToolsControl.PTLegDescription", TDCultureInfo.CurrentUICulture);

			// start & end node description (other nodes are numbered)
			string startJourneyDescription = Global.tdResourceManager.GetString("MapToolsControl.StartJourneyDescription", TDCultureInfo.CurrentUICulture);
			string endJourneyDescription = Global.tdResourceManager.GetString("MapToolsControl.EndJourneyDescription", TDCultureInfo.CurrentUICulture);

			// Can't seem to read in a space from langStrings file so reading in a # and replacing with a space
			// This is needed for Welsh language difference
			string legDescriptionPadding = Global.tdResourceManager.GetString("MapToolsControl.PTLegDescriptionPadding", TDCultureInfo.CurrentUICulture);
			legDescriptionPadding = legDescriptionPadding.Replace("#", " ");
			
			string lastLegMode = String.Empty;
			string lastLegStart = String.Empty;

			string englishModeDescription ;
			string translatedModeDescription ;

			// Tracks the number of the leg that corresponds with the dropdown list entry,
			// taking into account invalid legs
			int accumulatedLegNumber = 1;

			journeys = (outward) ? itineraryManager.OutwardJourneyItinerary : itineraryManager.ReturnJourneyItinerary;
			noOfJourneys = journeys.Length;

			for (int i = 0; i<noOfJourneys; i++)
			{
				journey = journeys[i];

				if (firstJourney)
				{
                    if (!javascript)
                    {
                        // The first available map is the map of the whole itinerary.
                        list.Items.Add(new ListItem(Global.tdResourceManager.GetString
                            ("JourneyMapControl.labelMapOfEntireJourney", TDCultureInfo.CurrentUICulture), "0"));
                    }
                    else
                    {
                        // Because PopulateMultiJourneySegments is generally only called for modified journeys,
                        // there will likely be multiple journeys added to the map. So add the javascript 
                        // to make journey zoom to all the routes added, 
                        // done by the script MapJourneyDisplayDetailsControl.js
                        string listItemValue = "0";
                        listItemValue += "," + JOURNEY_ZOOM_ALLROUTES;

                        list.Items.Add(new ListItem(Global.tdResourceManager.GetString
                            ("JourneyMapControl.labelMapOfEntireJourney", TDCultureInfo.CurrentUICulture), listItemValue));
                    }

					firstJourney = false;
				}

				bool previousLegInvalid = false;

				// Loop through the legs for the journey building up a list of descriptions
				// including the modes and segment indexes.
				foreach (JourneyLeg journeyLeg in journey.JourneyLegs)
				{
                    if (!journeyLeg.HasInvalidCoordinates)
                    {
                        previousLegInvalid = false;

                        string StartNodeDescription;
                        string EndNodeDescription;
                        if (journeySegmentIndex == 0)
                        {
                            // start node described as "start"
                            StartNodeDescription = startJourneyDescription;
                            EndNodeDescription = legDescriptionPadding + "1";
                        }
                        else
                        {
                            // intermediate nodes described with an incremental number
                            StartNodeDescription = legDescriptionPadding + journeySegmentIndex.ToString();
                            EndNodeDescription = legDescriptionPadding + (journeySegmentIndex + 1).ToString();
                        }
                        // Note - we will do the end node later when we have finished gathering all nodes
                        // by replacing the last one with "end".

                        journeySegmentIndex++;

                        englishModeDescription = journeyLeg.Mode.ToString();
                        translatedModeDescription = (Global.tdResourceManager.GetString("TransportMode." + englishModeDescription, TDCultureInfo.CurrentUICulture)).ToLower();

                        lastLegMode = translatedModeDescription;
                        lastLegStart = StartNodeDescription;

                        string LegDescription = string.Format(unformattedLegDescription,
                            translatedModeDescription, StartNodeDescription, EndNodeDescription);

                        // Capitalise the 1st letter
                        LegDescription = LegDescription.Substring(0, 1).ToUpper() + LegDescription.Substring(1);

                        // Set the list item value
                        string listItemValue = accumulatedLegNumber.ToString();

                        // Add the OSGR values for this leg. This value will be used by a javascript function
                        // to zoom the map to an envelope area, done in script MapJourneyDisplayDetailsControl.js
                        if (javascript)
                        {
                            #region Add envelope to zoom to

                            OSGridReference legStartOSGR = journeyLeg.LegStart.Location.GridReference;
                            OSGridReference legEndOSGR = journeyLeg.LegEnd.Location.GridReference;

                            OSGridReference[] osgrEnvelope = CreateMapEnvelope(new OSGridReference[2] { legStartOSGR, legEndOSGR });

                            listItemValue += "," + JOURNEY_ZOOM_ENVELOPE + "," + GetOSGRsAsString(osgrEnvelope);
                            #endregion
                        }
                        
                        list.Items.Add(new ListItem(LegDescription, listItemValue));
                    }
                    else
                    {
                        journeySegmentIndex++;

                        if (!previousLegInvalid)
                        {
                            previousLegInvalid = true;
                        }
                    }
					accumulatedLegNumber++;
				}
			} // for i=0...
			
			// Replace the last list item with an "end" description
			string lastLegDescription = string.Format(unformattedLegDescription,
				lastLegMode, lastLegStart, endJourneyDescription);

            // Capitalise the 1st letter
            lastLegDescription = lastLegDescription.Substring(0, 1).ToUpper() + lastLegDescription.Substring(1);

			list.Items[list.Items.Count-1].Text = lastLegDescription;
			
			if (!TDItineraryManager.Current.ItineraryManagerModeChanged) list.SelectedIndex = origIndex;
        }

        /// <summary>
        /// Method which returns a ListItem for a drop down list for the supplied PublicJourneyDetail.
        /// If Javascript flag is true, then a map zoom envelope coordinates are included to allow the 
        /// mapping javascript to zoom the map.
        /// 
        /// </summary>
        /// <param name="detail"></param>
        /// <param name="journeySegmentIndex"></param>
        /// <param name="journeyLength"></param>
        /// <param name="javascript"></param>
        /// <returns></returns>
        public ListItem GetListItemForDetail(PublicJourneyDetail journeyDetail, int journeySegmentIndex,
            JourneyControl.PublicJourney journey, int journeyLength, bool javascript)
        {
            string startNodeDescription;
            string endNodeDescription;
            
            if (journeySegmentIndex == 0)
            {
                startNodeDescription = startJourneyDescription;
                endNodeDescription = legDescriptionPadding + "1";
            }
            else if (journeySegmentIndex == journeyLength - 1)
            {
                startNodeDescription = legDescriptionPadding + journeySegmentIndex.ToString();
                endNodeDescription = endJourneyDescription;
            }
            else
            {
                // intermediate nodes described with an incremental number
                startNodeDescription = legDescriptionPadding + journeySegmentIndex.ToString();
                endNodeDescription = legDescriptionPadding + (journeySegmentIndex + 1).ToString();
            }

            journeySegmentIndex++;

            string englishModeDescription = journeyDetail.Mode.ToString();
            string translatedModeDescription = (Global.tdResourceManager.GetString("TransportMode." + englishModeDescription, TDCultureInfo.CurrentUICulture)).ToLower();

            // Check if "WalkInterchange" text is required
            PlannerOutputAdapter plannerOutputAdapter = new PlannerOutputAdapter();
            if (plannerOutputAdapter.WalkInterchangeRequired(journeyDetail, journey, journey.AccessibleJourney))
            {
                translatedModeDescription = (Global.tdResourceManager.GetString("TransportMode.WalkInterchange", TDCultureInfo.CurrentUICulture)).ToLower();
            }

            string legDescription = string.Format(unformattedLegDescription,
                translatedModeDescription, startNodeDescription, endNodeDescription);

            // Capitalise the 1st letter
            legDescription = legDescription.Substring(0, 1).ToUpper() + legDescription.Substring(1);

            // Set the list item value
            string listItemValue = (journeySegmentIndex).ToString();

            // Add the OSGR values for this detail. This value will be used by a javascript function
            // to zoom the map to an envelope area
            if (javascript)
            {
                #region Add envelope to zoom to

                OSGridReference[] osgr = journeyDetail.Geometry;
                if (osgr.Length > 0)
                {
                    OSGridReference[] osgrEnvelope = CreateMapEnvelope(osgr);

                    listItemValue += "," + JOURNEY_ZOOM_ENVELOPE + "," + GetOSGRsAsString(osgrEnvelope);
                }

                #endregion
            }

            ListItem listItem = new ListItem(legDescription, listItemValue);

            return listItem;
        }
                
        #endregion

        #region Journey

        /// <summary>
        /// Returns an array of the modes used in this journey. 
        /// </summary>
        /// <param name="outward"></param>
        /// <param name="usingItinerary"></param>
        public ModeType[] FindUsedModes(bool outward, bool usingItinerary)
        {
            Journey journey = FindRelevantJourney(outward);

            return journey.GetUsedModes();
        }

        /// <summary>
		/// If the currently selected outward journey type is a public transport journey
		/// return true, otherwise return false
		/// </summary>
		public bool PublicOutwardJourney 
		{
			get 
			{
				ITDSessionManager sessionManager = TDSessionManager.Current;
				TDItineraryManager itineraryManager = sessionManager.ItineraryManager;

				if (itineraryManager.FullItinerarySelected)
				{
					// There is an itinerary manager so find out if a public journey
					// exists in its journeys.
					foreach (Journey journey in itineraryManager.OutwardJourneyItinerary)
					{
						if (journey is TDPublicJourney)
						{
							return true;
						}
					}

					// If we have looked through the collection and there are no
					// public journeys then return false here.
					return false;
				}
				else
				{
					// No itinerary so return selected outward joureny type from 
					// session manager.
					TDJourneyType journeyType = sessionManager.JourneyViewState.SelectedOutwardJourneyType;
					return ((journeyType == TDJourneyType.PublicOriginal) || (journeyType == TDJourneyType.PublicAmended));
				}
			}
		}


		/// <summary>
		/// If the currently selected return journey type is a public transport journey
		/// return true, otherwise return false
		/// </summary>
		public bool PublicReturnJourney 
		{
			get 
			{
				ITDSessionManager sessionManager = TDSessionManager.Current;
				TDItineraryManager itineraryManager = sessionManager.ItineraryManager;

				if (itineraryManager.FullItinerarySelected)
				{
					// There is an itinerary manager so find out if a public journey
					// exists in its journeys.
					foreach (Journey journey in itineraryManager.ReturnJourneyItinerary)
					{
						if (journey is TDPublicJourney)
						{
							return true;
						}
					}

					// If we have looked through the collection and there are no
					// public journeys then return false here.
					return false;
				}
				else
				{
					// No itinerary so return selected Return joureny type from 
					// session manager.
					TDJourneyType journeyType = sessionManager.JourneyViewState.SelectedReturnJourneyType;
					return ((journeyType == TDJourneyType.PublicOriginal) || (journeyType == TDJourneyType.PublicAmended));
				}
			}
		}

		/// <summary>
		/// Returns true if there is a Public journey in either the Outward or Return journeys.
		/// </summary>
		public bool PublicJourney
		{
			get { return (PublicOutwardJourney || PublicReturnJourney); }
		}

		/// <summary>
		/// If the currently selected outward journey type is a private journey
		/// return true, otherwise return false
		/// </summary>
		public bool PrivateOutwardJourney 
		{
			get 
			{
				ITDSessionManager sessionManager = TDSessionManager.Current;
				TDItineraryManager itineraryManager = sessionManager.ItineraryManager;

				if (TDSessionManager.Current.ItineraryMode != ItineraryManagerMode.None)
				{
					// There is an itinerary manager so find out if a road journey
					// exists in its journeys.
					foreach (Journey journey in itineraryManager.OutwardJourneyItinerary)
					{
						if (journey is RoadJourney)
						{
							return true;
						}
					}

					// If we have looked through the collection and there are no
					// road journeys then return false here.
					return false;
				}
				else
				{
					// No itinerary so return selected outward joureny type from 
					// session manager.
					TDJourneyType journeyType = sessionManager.JourneyViewState.SelectedOutwardJourneyType;
					return (journeyType == TDJourneyType.RoadCongested);
				}
			}
		}

		/// <summary>
		/// If the currently selected return journey type is a private journey
		/// return true, otherwise return false
		/// </summary>
		public bool PrivateReturnJourney 
		{
			get 
			{
				ITDSessionManager sessionManager = TDSessionManager.Current;
				TDItineraryManager itineraryManager = sessionManager.ItineraryManager;

				if (TDSessionManager.Current.ItineraryMode != ItineraryManagerMode.None)
				{
					// There is an itinerary manager so find out if a road journey
					// exists in its journeys.
					foreach (Journey journey in itineraryManager.ReturnJourneyItinerary)
					{
						if (journey is RoadJourney)
						{
							return true;
						}
					}

					// If we have looked through the collection and there are no
					// road journeys then return false here.
					return false;
				}
				else
				{
					// No itinerary so return selected Return joureny type from 
					// session manager.
					TDJourneyType journeyType = sessionManager.JourneyViewState.SelectedReturnJourneyType;
					return (journeyType == TDJourneyType.RoadCongested);
				}
			}
		}

		/// <summary>
		/// Returns true if there is a Private journey in either the Outward or Return journeys.
		/// </summary>
		public bool PrivateJourney
		{
			get { return (PrivateOutwardJourney || PrivateReturnJourney); }
		}

        /// <summary>
        /// Returns true if the currently selected Outward journey is a CycleJourney
        /// </summary>
        public bool CycleOutwardJourney
        {
            get
            {
                ITDSessionManager sessionManager = TDSessionManager.Current;
                TDItineraryManager itineraryManager = sessionManager.ItineraryManager;

                if (TDSessionManager.Current.ItineraryMode != ItineraryManagerMode.None)
                {
                    // There is an itinerary manager so find out if a cycle journey
                    // exists in its journeys.
                    // MM - There shouldn't be any as we've only used session manager to store cycle journeys
                    foreach (Journey journey in itineraryManager.OutwardJourneyItinerary)
                    {
                        if (journey is CycleJourney)
                        {
                            return true;
                        }
                    }

                    // No journey is a cycle journey
                    return false;
                }
                else
                {
                    // No itinerary so return selected Return journey type from 
                    // session manager.
                    TDJourneyType journeyType = sessionManager.JourneyViewState.SelectedOutwardJourneyType;
                    return (journeyType == TDJourneyType.Cycle);
                }
            }
        }

        /// <summary>
        /// Returns true if the currently selected Return journey is a CycleJourney
        /// </summary>
        public bool CycleReturnJourney
        {
            get
            {
                ITDSessionManager sessionManager = TDSessionManager.Current;
                TDItineraryManager itineraryManager = sessionManager.ItineraryManager;

                if (TDSessionManager.Current.ItineraryMode != ItineraryManagerMode.None)
                {
                    // There is an itinerary manager so find out if a cycle journey
                    // exists in its journeys.
                    // MM - There shouldn't be any as we've only used session manager to store cycle journeys
                    foreach (Journey journey in itineraryManager.ReturnJourneyItinerary)
                    {
                        if (journey is CycleJourney)
                        {
                            return true;
                        }
                    }

                    // No journey is a cycle journey
                    return false;
                }
                else
                {
                    // No itinerary so return selected Return journey type from 
                    // session manager.
                    TDJourneyType journeyType = sessionManager.JourneyViewState.SelectedReturnJourneyType;
                    return (journeyType == TDJourneyType.Cycle);
                }
            }
        }

		/// <summary>
		/// Returns the journey associated with the currently selected journey.
		/// </summary>
		/// <param name="outward"></param>
		/// <returns></returns>
		public Journey FindRelevantJourney(bool outward)
		{
			TDJourneyViewState viewstate = null;
			ITDJourneyResult result = null;
            ITDCyclePlannerResult cycleResult = null;
			Journey journey;

			// Use the itinerary manager to get the viewstate and result - this will get the
			// details from the itinerary manager if an extended journey or the itinerary will
			// refer to the session manager if not extended.
			viewstate = TDItineraryManager.Current.JourneyViewState;
			result = TDItineraryManager.Current.JourneyResult;
            cycleResult = TDSessionManager.Current.CycleResult;

            // Get the modes used in the display, set by city to city otherwise will be null
            TransportDirect.JourneyPlanning.CJPInterface.ModeType[] modeTypes = null;
            if (TDSessionManager.Current.FindPageState != null)
                modeTypes = TDSessionManager.Current.FindPageState.ModeType;

			if(outward)
			{
				int currentSelected = viewstate.SelectedOutwardJourney;
				bool arriveBefore = viewstate.JourneyLeavingTimeSearchType;

                JourneySummaryLine summary = (TDSessionManager.Current.FindAMode == FindAMode.Cycle) ?
                    cycleResult.OutwardJourneySummary(arriveBefore, modeTypes)[currentSelected] :
                    result.OutwardJourneySummary(arriveBefore, modeTypes)[currentSelected];

                if ((TDSessionManager.Current.FindAMode == FindAMode.Trunk || TDSessionManager.Current.FindAMode == FindAMode.TrunkStation) && TDSessionManager.Current.FindPageState.ITPJourney)
                {
                    summary = GetJourneySummary(result, TDSessionManager.Current.FindPageState.ITPJourney, currentSelected, arriveBefore, modeTypes, true);
                }

				if(summary.Type == TDJourneyType.PublicAmended)
				{
					journey = result.AmendedOutwardPublicJourney;
				}
				else if(summary.Type == TDJourneyType.PublicOriginal)
				{
					journey = result.OutwardPublicJourney(summary.JourneyIndex);
				}
                else if (summary.Type == TDJourneyType.Cycle)
                {
                    journey = cycleResult.OutwardCycleJourney();
                }
				else
				{
					journey = result.OutwardRoadJourney();
				}
			}
			else
			{
				int currentSelected = viewstate.SelectedReturnJourney;
				bool arriveBefore = viewstate.JourneyReturningTimeSearchType;
                
                JourneySummaryLine summary = (TDSessionManager.Current.FindAMode == FindAMode.Cycle) ?
                    cycleResult.ReturnJourneySummary(arriveBefore, modeTypes)[currentSelected] :
                    result.ReturnJourneySummary(arriveBefore, modeTypes)[currentSelected];

                if ((TDSessionManager.Current.FindAMode == FindAMode.Trunk || TDSessionManager.Current.FindAMode == FindAMode.TrunkStation) && TDSessionManager.Current.FindPageState.ITPJourney)
                {
                    summary = GetJourneySummary(result, TDSessionManager.Current.FindPageState.ITPJourney, currentSelected, arriveBefore, modeTypes, false);
                }

				if(summary.Type == TDJourneyType.PublicAmended)
				{
					journey = result.AmendedReturnPublicJourney;
				}
				else if(summary.Type == TDJourneyType.PublicOriginal)
				{
					journey = result.ReturnPublicJourney(summary.JourneyIndex);
				}
                else if (summary.Type == TDJourneyType.Cycle)
                {
                    journey = cycleResult.ReturnCycleJourney();
                }
				else
				{
					journey = result.ReturnRoadJourney();
				}
			}

			return journey;
		}

        public bool HasJourneyGreyedOutMode (bool outward)
		{
			TDItineraryManager itineraryManager = TDItineraryManager.Current;
			Journey[] journeys;
			JourneyControl.PublicJourney publicJourney;

			//Chekc whether itinerary is in use and selected
			if (itineraryManager.FullItinerarySelected)
			{
				//Look at every journey in the itinerary and do the checks for invlid coordinates
				journeys = (outward) ? itineraryManager.OutwardJourneyItinerary : itineraryManager.ReturnJourneyItinerary;

				for (int segmentIndex = 0; segmentIndex < journeys.Length; segmentIndex++)
				{
					publicJourney = journeys[segmentIndex] as JourneyControl.PublicJourney;
					if ((publicJourney != null) && publicJourney.HasInvalidLegCoordinates)

						return true;
				}
				return false;
			}
			else
			{
				Journey journey = FindRelevantJourney(outward);
				publicJourney = journey as JourneyControl.PublicJourney;

				if (publicJourney != null)
				{
					return publicJourney.HasInvalidLegCoordinates;
				}
				else
				{
					return false;
				}
			}
        }

        #endregion

        #region Printer Friendly

        /// <summary>
        /// Resets the session variables used by the printable version of map, used by 
        /// the Printer Friendly page.
        /// </summary>
        public void ResetPrintableMapSessionValues(bool outward, string mapViewTypeText)
        {
            InputPageState inputPageState = TDSessionManager.Current.InputPageState;
            
            if (outward)
            {
                // Reset map url
                inputPageState.MapUrlOutward = string.Empty;

                // Reset journey text
                inputPageState.MapViewTypeOutward = mapViewTypeText;

                // Reset selected map symbols/icons
                if (inputPageState.IconSelectionOutward != null)
                {
                    // Clear the selected icons in the session
                    for (int i = 0; i < inputPageState.IconSelectionOutward.Length; i++)
                    {
                        for (int j = 0; j < inputPageState.IconSelectionOutward[i].Length; j++)
                        {
                            inputPageState.IconSelectionOutward[i][j] = false;
                        }
                    }
                }
            }
            else
            {
                // Reset map url
                inputPageState.MapUrlReturn = string.Empty;

                // Reset journey text
                inputPageState.MapViewTypeReturn = mapViewTypeText;

                // Reset selected map symbols/icons
                if (inputPageState.IconSelectionReturn != null)
                {
                    /// Clear the selected icons in the session.
                    for (int i = 0; i < inputPageState.IconSelectionReturn.Length; i++)
                    {
                        for (int j = 0; j < inputPageState.IconSelectionReturn[i].Length; j++)
                        {
                            inputPageState.IconSelectionReturn[i][j] = false;
                        }
                    }
                }
            }
        }

        /// <summary>
		/// Used to retrieve properties for a high-resolution image of the currently selected map
		/// for rendering an appropriately sized image on a printer friendly page.
		/// </summary>
		public MapImageProperties GetHighResolutionMapImageUrl(bool isOutward)
		{
			TDMap mapControl = new TDMap();
			mapControl.ServiceName = Properties.Current["InteractiveMapping.Map.ServiceName"];
			mapControl.ServerName = Properties.Current["InteractiveMapping.Map.ServerName"];
			
			//retrieve the map state from session data 
			try 
			{
				mapControl.InjectViewState(TDSessionManager.Current.StoredMapViewState[isOutward ? TDSessionManager.OUTWARDMAP : TDSessionManager.RETURNMAP]);
			}
			catch (MapExceptionGeneral exc)
			{
				OperationalEvent oe = new OperationalEvent(TDEventCategory.ThirdParty, TDTraceLevel.Warning, exc.Message + "\nStacktrace:\n" + exc.StackTrace);
				Logger.Write(oe);
			}
		
			// if the injection fails, return a MapImageProperties instance with empty url, and height and width set to 0,
			// otherwise create an image with GetPrintImage with retrieved map width, height and resolution
			if (!mapControl.IsStarted())
			{
				return new MapImageProperties(String.Empty, 0, 0);
			}
			else 
			{
				int imageWidth = Convert.ToInt32(mapControl.Width.Value);
				int imageHeight = Convert.ToInt32(mapControl.Height.Value);
				int imageResolution = Convert.ToInt32(Properties.Current["InteractiveMapping.MapImageResolution"]);
						
				string imageURL = String.Empty;
					
				try 
				{
					imageURL = mapControl.GetPrintImage(imageWidth, imageHeight, imageResolution);
				}
				catch (MapExceptionGeneral exc)
				{
					OperationalEvent oe = new OperationalEvent(TDEventCategory.ThirdParty, TDTraceLevel.Warning, exc.Message +"\nStacktrace:\n"+exc.StackTrace);
					Logger.Write(oe);
					return new MapImageProperties(String.Empty, 0, 0);
				}
		
				return new MapImageProperties(imageURL, imageHeight, imageWidth);
			}
		}

        /// <summary>
        /// Used to retrieve a list of map urls and direction Ids of the 
        /// currently selected map for rendering an appropriately sized image on a printer friendly page.
        /// </summary>
        /// <param name="scale"> out scale, the scale used to generate the map tiles</param>
        public CyclePrintDetail[] GetMapTiles(bool outward, out double scale)
        {
            TDMap mapControl = new TDMap();
            mapControl.ServiceName = Properties.Current["CyclePlanner.InteractiveMapping.Map.ServiceName"];
            mapControl.ServerName = Properties.Current["CyclePlanner.InteractiveMapping.Map.ServerName"];

            //retrieve the map state from session data 
            try
            {
                mapControl.InjectViewState(TDSessionManager.Current.StoredMapViewState[outward ? TDSessionManager.OUTWARDMAP : TDSessionManager.RETURNMAP]);
            }
            catch (MapExceptionGeneral exc)
            {
                OperationalEvent oe = new OperationalEvent(TDEventCategory.ThirdParty, TDTraceLevel.Warning, exc.Message + "\nStacktrace:\n" + exc.StackTrace);
                Logger.Write(oe);
            }

            // Set up a default return object
            //int[] directionId = new int[0];
            //CyclePrintDetail[] cyclePrintDetail = { new CyclePrintDetail(string.Empty, directionId) };
            CyclePrintDetail[] cyclePrintDetail = new CyclePrintDetail[0];

            // intialise the scale value
            scale = 0;

            // if the injection fails, return an empty instance with empty url, and directionIds
            if (!mapControl.IsStarted())
            {
                return cyclePrintDetail;
            }
            else
            {
                // The map control uses the current cycle journey added to generate the maps

                #region Determine the scale we should display the tiled maps at

                #region zoom levels

                string DEFAULT_ZOOM_DEFINITION = "MappingComponent";

                string[] zoomDefinitions = {
														"Web.{0}.ZoomLevelOne",
														"Web.{0}.ZoomLevelTwo",
														"Web.{0}.ZoomLevelThree",
														"Web.{0}.ZoomLevelFour",
														"Web.{0}.ZoomLevelFive",
														"Web.{0}.ZoomLevelSix",
														"Web.{0}.ZoomLevelSeven",
														"Web.{0}.ZoomLevelEight",
														"Web.{0}.ZoomLevelNine",
														"Web.{0}.ZoomLevelTen",
														"Web.{0}.ZoomLevelEleven",
														"Web.{0}.ZoomLevelTwelve",
														"Web.{0}.ZoomLevelThirteen"
													};

                #endregion

                // This is the number of maps we want to aim for
                int targetNumberOfMaps = Convert.ToInt32(Properties.Current["CyclePlanner.InteractiveMapping.Map.NumberOfMapTilesTarget"]);

                // first, get the number of maps for our default zoom level
                scale = Convert.ToDouble(Properties.Current["CyclePlanner.InteractiveMapping.Map.MapTilesDefaultScale"]);

                int numberOfMapImages = mapControl.GetNumberOfCycleMapImages(scale);

                if (!IsNumberOfMapsNearTarget(numberOfMapImages, targetNumberOfMaps))
                {
                    // The default scale would return maps above our target, so we now need to 
                    // go through all of our scale definitions and use the one which returns a number 
                    // of maps value less than our target

                    for (int i = 0; i < zoomDefinitions.Length; i++)
                    {
                        //"Web.MappingComponent.ZoomLevelOne"
                        string zoomDef = string.Format(zoomDefinitions[i], DEFAULT_ZOOM_DEFINITION);

                        scale = Convert.ToDouble(Properties.Current[zoomDef]);

                        numberOfMapImages = mapControl.GetNumberOfCycleMapImages(scale);

                        if (IsNumberOfMapsNearTarget(numberOfMapImages, targetNumberOfMaps))
                        {
                            // we've found a scale which returns the correct number of maps
                            break;
                        }
                    }
                }

                #endregion

                // Image width, height, resolution are set in the web.config

                try
                {
                    DateTime operationStartedDateTime = DateTime.Now;

                    cyclePrintDetail = mapControl.GetCyclePrintDetails(scale);

                    // Log a MapEvent for each tile that was generated
                    for (int i = 0; i < cyclePrintDetail.Length; i++)
                    {
                        MapLogging.Write(TransportDirect.ReportDataProvider.TDPCustomEvents.MapEventCommandCategory.MapTile, Convert.ToInt32(scale), operationStartedDateTime);
                    }
                }
                catch (MapExceptionGeneral exc)
                {
                    OperationalEvent oe = new OperationalEvent(TDEventCategory.ThirdParty, TDTraceLevel.Warning, exc.Message + "\nStacktrace:\n" + exc.StackTrace);
                    Logger.Write(oe);
                }
                catch (Exception exc)
                {
                    OperationalEvent oe = new OperationalEvent(TDEventCategory.ThirdParty, TDTraceLevel.Warning, exc.Message + "\nStacktrace:\n" + exc.StackTrace);
                    Logger.Write(oe);
                }

                return cyclePrintDetail;
            }
        }

        #endregion

        #region MapControl2 helper methods

        /// <summary>
        /// Populates the MapParameters used by the MapControl2 in intialising a map, using TDLocation
        /// </summary>
        public MapParameters GetMapParametersForLocation(TDLocation tdLocation, LocationSearch locationSearch)
        {
            MapParameters mapParameters = new MapParameters();

            if ((tdLocation != null) && (tdLocation.Status == TDLocationStatus.Valid))
            {
                // If partial postcode, then show map at the defined envelope
                if (tdLocation.PartPostcode)
                {
                    int envelopeMinX = Convert.ToInt32(tdLocation.PartPostcodeMinX);
                    int envelopeMinY = Convert.ToInt32(tdLocation.PartPostcodeMinY);
                    int envelopeMaxX = Convert.ToInt32(tdLocation.PartPostcodeMaxX);
                    int envelopeMaxY = Convert.ToInt32(tdLocation.PartPostcodeMaxY);

                    mapParameters.MapEnvelopeMin = new OSGridReference(envelopeMinX, envelopeMinY);
                    mapParameters.MapEnvelopeMax = new OSGridReference(envelopeMaxX, envelopeMaxY);
                }
                // Else, show map at the location point at the scale for its search type
                else
                {
                    // Determine scale
                    int scaleToSet = -1;
                    if (locationSearch != null)
                    {
                        scaleToSet = GetScaleForSearchType(locationSearch.SearchType);
                    }

                    mapParameters.MapCentre = tdLocation.GridReference;
                    mapParameters.MapScale = scaleToSet;
                }
            }

            return mapParameters;
        }

        /// <summary>
        /// Returns a MapLocationPoint to be shown on a map in MapControl2
        /// </summary>
        /// <param name="tdLocation"></param>
        /// <param name="mapLocationSymbol"></param>
        /// <param name="infoPopupRequired">Indicates if the location information popup is required</param>
        /// <returns></returns>
        public MapLocationPoint GetMapLocationPoint(TDLocation tdLocation, MapLocationSymbolType mapLocationSymbol,
            bool infoPopupRequired, bool showPopup)
        {
            if ((tdLocation != null) && (tdLocation.Status == TDLocationStatus.Valid))
            {
                return new MapLocationPoint(tdLocation.GridReference, mapLocationSymbol, tdLocation.Description, infoPopupRequired, showPopup);
            }
            else
            {
                return new MapLocationPoint();
            }
        }

        /// <summary>
        /// Returns a MapTravelNewsFilter class
        /// </summary>
        /// <param name="mapTransportType"></param>
        /// <param name="mapIncidentType"></param>
        /// <param name="mapSeverity"></param>
        /// <param name="mapTimePeriod"></param>
        /// <param name="mapDateTime"></param>
        /// <returns></returns>
        public MapTravelNewsFilter GetMapTravelNewsFilter(MapTransportType mapTransportType, MapIncidentType mapIncidentType,
            MapSeverity mapSeverity, MapTimePeriod mapTimePeriod, DateTime mapDateTime)
        {
            MapTravelNewsFilter mapTravelNewsFilter = new MapTravelNewsFilter();

            mapTravelNewsFilter.MapTransportType = mapTransportType;
            mapTravelNewsFilter.MapIncidentType = mapIncidentType;
            mapTravelNewsFilter.MapSeverity = mapSeverity;
            mapTravelNewsFilter.MapTimePeriod = mapTimePeriod;
            mapTravelNewsFilter.MapDateTime = mapDateTime;
            mapTravelNewsFilter.IsValid = true;

            return mapTravelNewsFilter;
        }

        /// <summary>
        /// Returns the map scale for the search type
        /// </summary>
        /// <returns></returns>
        public int GetScaleForSearchType(SearchType searchType)
        {
            int scaleToSet = -1;

            try
            {
                string scaleString = String.Empty;

                // Determine what the initial zoom level is based on the search type
                switch (searchType)
                {
                    // Get scale to set from the Properties Service

                    case SearchType.AddressPostCode:
                    case SearchType.Map:
                        scaleString = Properties.Current["GazetteerDefaultScale.AddressPostCode"];
                        break;

                    case SearchType.AllStationStops:
                        scaleString = Properties.Current["GazetteerDefaultScale.AllStations"];
                        break;

                    case SearchType.Locality:
                        scaleString = Properties.Current["GazetteerDefaultScale.Locality"];
                        break;

                    case SearchType.MainStationAirport:
                        scaleString = Properties.Current["GazetteerDefaultScale.MajorStations"];
                        break;

                    case SearchType.POI:
                        scaleString = Properties.Current["GazetteerDefaultScale.AttractionsFacilities"];
                        break;
                }

                scaleToSet = Convert.ToInt32(scaleString, TDCultureInfo.CurrentCulture.NumberFormat);
            }
            catch
            {
                scaleToSet = -1;
            }

            return scaleToSet;
        }
        
        #endregion

        #region MapControl2 web service helper methods

        /// <summary>
        /// Sets start location map point
        /// </summary>
        /// <param name="easting">OSGR easting </param>
        /// <param name="northing">OSGR northing</param>
        /// <param name="locationText">Location text</param>
        /// <param name="pageId">Current Page id</param>
        /// <returns></returns>
        public string SetStartLocationPoint(int easting, int northing, string locationText, PageId pageId)
        {
            TDLocation location = GetLocationFromGridReference(easting, northing, locationText);
            location.SearchType = SearchType.Map;
            
            TDSessionManager.Current.InputPageState.MapLocation = location;
            TDSessionManager.Current.InputPageState.MapLocationSearch.InputText = location.Description;
            TDSessionManager.Current.InputPageState.MapLocationControlType = new TransportDirect.UserPortal.SessionManager.TDJourneyParameters.LocationSelectControlType(TransportDirect.UserPortal.SessionManager.TDJourneyParameters.ControlType.NewLocation);
            TDSessionManager.Current.InputPageState.MapLocationSearch.SearchType = SearchType.Map;
                        
            // If there is a valid results set, reset the parameters and pagestate, and 
            // and invalidate the results.
            if (((TDItineraryManager.Current.JourneyResult != null) && (TDItineraryManager.Current.JourneyResult.IsValid))
                || (TDItineraryManager.Current.FullItinerarySelected))
            {
                TDItineraryManager.Current.ResetItinerary();
                TDSessionManager.Current.InputPageState.Initialise();
                TDSessionManager.Current.JourneyResult.IsValid = false;

                // Do not reset the parameters for VisitPlanner as FullItinerarySelected is always true
                if (TDSessionManager.Current.InputPageState.MapMode != CurrentMapMode.VisitPlannerLocationOrigin
                    && TDSessionManager.Current.InputPageState.MapMode != CurrentMapMode.VisitPlannerLocation1
                    && TDSessionManager.Current.InputPageState.MapMode != CurrentMapMode.VisitPlannerLocation2)
                {
                    TDSessionManager.Current.JourneyParameters = new TDJourneyParametersMulti();
                }
            }

            // If its a car park selected, obtain the car park data and add to location
            if (TDSessionManager.Current.InputPageState.CarParkReference != string.Empty)
            {
                SetupCarParkPlanning(true);
            }

            // Save the location to be used by the journey input page
            TDSessionManager.Current.JourneyParameters.OriginLocation = TDSessionManager.Current.InputPageState.MapLocation;
            TDSessionManager.Current.JourneyParameters.Origin = TDSessionManager.Current.InputPageState.MapLocationSearch;
            TDSessionManager.Current.JourneyMapState.Location = TDSessionManager.Current.InputPageState.MapLocation;
            TDSessionManager.Current.JourneyMapState.LocationSearch = TDSessionManager.Current.InputPageState.MapLocationSearch;
            
            
            if (TDSessionManager.Current.InputPageState.MapMode != CurrentMapMode.FromJourneyInput
                && TDSessionManager.Current.InputPageState.MapMode != CurrentMapMode.FromFindAInput
                && TDSessionManager.Current.InputPageState.MapMode != CurrentMapMode.VisitPlannerLocationOrigin
                && TDSessionManager.Current.InputPageState.MapMode != CurrentMapMode.VisitPlannerLocation1
                && TDSessionManager.Current.InputPageState.MapMode != CurrentMapMode.VisitPlannerLocation2)
            {
                // Check to see if Destination is valid
                if (TDSessionManager.Current.JourneyParameters.DestinationLocation.Status == TDLocationStatus.Valid)
                {
                    TDSessionManager.Current.InputPageState.MapMode = CurrentMapMode.FindJourneys;
                }
                else
                {
                    TDSessionManager.Current.InputPageState.MapMode = CurrentMapMode.TravelTo;
                    TDSessionManager.Current.InputPageState.MapLocationSearch = new LocationSearch();
                    TDSessionManager.Current.InputPageState.MapLocation = new TDLocation();
                    TDSessionManager.Current.InputPageState.MapLocationControlType.Type = TDJourneyParameters.ControlType.Default;
                }
            }
            // Check for all VisitPlanner modes because any Find on map button could have been selected on
            // input page
            else if (TDSessionManager.Current.InputPageState.MapMode == CurrentMapMode.VisitPlannerLocationOrigin
                     || TDSessionManager.Current.InputPageState.MapMode == CurrentMapMode.VisitPlannerLocation1
                     || TDSessionManager.Current.InputPageState.MapMode == CurrentMapMode.VisitPlannerLocation2)
            {
                // Set up location for Visit Planner input page
                TDJourneyParametersVisitPlan parameters = TDSessionManager.Current.JourneyParameters as TDJourneyParametersVisitPlan;

                // If there have been no journey planner parameters set up then do so now
                if (parameters == null)
                {
                    parameters = new TDJourneyParametersVisitPlan();
                }

                parameters.SetLocation(0, TDSessionManager.Current.InputPageState.MapLocation);
                parameters.SetLocationSearch(0, TDSessionManager.Current.InputPageState.MapLocationSearch);

                TDSessionManager.Current.JourneyParameters = parameters;
            }
            
            if (TDSessionManager.Current.InputPageState.MapMode == CurrentMapMode.FromFindAInput &&
                (TDSessionManager.Current.FindAMode != FindAMode.Bus))
            {
                TDSessionManager.Current.JourneyParameters.DestinationLocation.NaPTANs = new TDNaptan[0];
            }

            #region Save session and return next page URL

            TransitionEvent te = GetPageTransitionEvent(pageId);

            ITDSessionManager sessionManager = (ITDSessionManager)TDServiceDiscovery.Current[ServiceDiscoveryKey.SessionManager];
            sessionManager.FormShift[SessionKey.TransitionEvent] = te;
            sessionManager.SaveData();

            #endregion

            return GetNextPageUrl(pageId);
        }

        /// <summary>
        /// Sets End map location point
        /// </summary>
        /// <param name="easting">OSGR easting</param>
        /// <param name="northing">OSGR northing</param>
        /// <param name="locationText">Location Text</param>
        /// <param name="pageId">Current page id</param>
        /// <returns></returns>
        public string SetEndLocationPoint(int easting, int northing, string locationText, PageId pageId)
        {
            TDLocation location = GetLocationFromGridReference(easting, northing, locationText);
            location.SearchType = SearchType.Map;

            TDSessionManager.Current.InputPageState.MapLocation = location;
            TDSessionManager.Current.InputPageState.MapLocationSearch.InputText = location.Description;
            TDSessionManager.Current.InputPageState.MapLocationControlType = new TransportDirect.UserPortal.SessionManager.TDJourneyParameters.LocationSelectControlType(TransportDirect.UserPortal.SessionManager.TDJourneyParameters.ControlType.NewLocation);
            TDSessionManager.Current.InputPageState.MapLocationSearch.SearchType = SearchType.Map;
            
            // If there is a valid results set, reset the parameters and pagestate, and 
            // and invalidate the results.
            if (((TDItineraryManager.Current.JourneyResult != null) && (TDItineraryManager.Current.JourneyResult.IsValid))
                || (TDItineraryManager.Current.FullItinerarySelected))
            {
                TDItineraryManager.Current.ResetItinerary();
                TDSessionManager.Current.InputPageState.Initialise();
                TDSessionManager.Current.JourneyResult.IsValid = false;

                // Do not reset the parameters for VisitPlanner as FullItinerarySelected is always true
                if (TDSessionManager.Current.InputPageState.MapMode != CurrentMapMode.VisitPlannerLocationOrigin
                    && TDSessionManager.Current.InputPageState.MapMode != CurrentMapMode.VisitPlannerLocation1
                    && TDSessionManager.Current.InputPageState.MapMode != CurrentMapMode.VisitPlannerLocation2)
                {
                    TDSessionManager.Current.JourneyParameters = new TDJourneyParametersMulti();
                }
            }

            // If its a car park selected, obtain the car park data and add to location
            if (TDSessionManager.Current.InputPageState.CarParkReference != string.Empty)
            {
                SetupCarParkPlanning(false);
            }

            TDSessionManager.Current.JourneyParameters.DestinationLocation = TDSessionManager.Current.InputPageState.MapLocation;
            TDSessionManager.Current.JourneyParameters.Destination = TDSessionManager.Current.InputPageState.MapLocationSearch;
            TDSessionManager.Current.JourneyMapState.Location = TDSessionManager.Current.InputPageState.MapLocation;
            TDSessionManager.Current.JourneyMapState.LocationSearch = TDSessionManager.Current.InputPageState.MapLocationSearch;

            if (TDSessionManager.Current.InputPageState.MapMode != CurrentMapMode.FromJourneyInput
                && TDSessionManager.Current.InputPageState.MapMode != CurrentMapMode.FromFindAInput
                && TDSessionManager.Current.InputPageState.MapMode != CurrentMapMode.VisitPlannerLocationOrigin
                && TDSessionManager.Current.InputPageState.MapMode != CurrentMapMode.VisitPlannerLocation1
                && TDSessionManager.Current.InputPageState.MapMode != CurrentMapMode.VisitPlannerLocation2)
            {
                // Check to see if Destination is valid
                if (TDSessionManager.Current.JourneyParameters.OriginLocation.Status == TDLocationStatus.Valid)
                {
                    TDSessionManager.Current.InputPageState.MapMode = CurrentMapMode.FindJourneys;
                }
                else
                {
                    TDSessionManager.Current.InputPageState.MapMode = CurrentMapMode.TravelTo;
                    TDSessionManager.Current.InputPageState.MapLocationSearch = new LocationSearch();
                    TDSessionManager.Current.InputPageState.MapLocation = new TDLocation();
                    TDSessionManager.Current.InputPageState.MapLocationControlType.Type = TDJourneyParameters.ControlType.Default;
                }
            }
            // Check for all VisitPlanner modes because any Find on map button could have been selected on
            // input page
            else if (TDSessionManager.Current.InputPageState.MapMode == CurrentMapMode.VisitPlannerLocationOrigin
                     || TDSessionManager.Current.InputPageState.MapMode == CurrentMapMode.VisitPlannerLocation1
                     || TDSessionManager.Current.InputPageState.MapMode == CurrentMapMode.VisitPlannerLocation2)
            {
                // Set up location for Visit Planner input page
                TDJourneyParametersVisitPlan parameters = TDSessionManager.Current.JourneyParameters as TDJourneyParametersVisitPlan;

                // If there have been no journey planner parameters set up then do so now
                if (parameters == null)
                {
                    parameters = new TDJourneyParametersVisitPlan();
                }

                parameters.SetLocation(2, TDSessionManager.Current.InputPageState.MapLocation);
                parameters.SetLocationSearch(2, TDSessionManager.Current.InputPageState.MapLocationSearch);

                TDSessionManager.Current.JourneyParameters = parameters;
            }

            if (TDSessionManager.Current.InputPageState.MapMode == CurrentMapMode.FromFindAInput
                && (TDSessionManager.Current.FindAMode != FindAMode.Bus))
            {
                TDSessionManager.Current.JourneyParameters.DestinationLocation.NaPTANs = new TDNaptan[0];
            }

            #region Save session and return next page URL

            TransitionEvent te = GetPageTransitionEvent(pageId);

            ITDSessionManager sessionManager = (ITDSessionManager)TDServiceDiscovery.Current[ServiceDiscoveryKey.SessionManager];
            sessionManager.FormShift[SessionKey.TransitionEvent] = te;
            sessionManager.SaveData();

            #endregion

            return GetNextPageUrl(pageId);
        }

        /// <summary>
        /// Sets via map location point 
        /// </summary>
        /// <param name="easting">OSGR easting</param>
        /// <param name="northing">OSGR northing</param>
        /// <param name="locationText">Location text</param>
        /// <param name="pageId">Current page id</param>
        /// <returns></returns>
        public string SetViaLocationPoint(int easting, int northing, string locationText, PageId pageId)
        {
            TDLocation location = GetLocationFromGridReference(easting, northing, locationText);
            location.SearchType = SearchType.Map;

            TDSessionManager.Current.InputPageState.MapLocation = location;
            TDSessionManager.Current.InputPageState.MapLocationSearch.InputText = location.Description;
            TDSessionManager.Current.InputPageState.MapLocationControlType = new TransportDirect.UserPortal.SessionManager.TDJourneyParameters.LocationSelectControlType(TransportDirect.UserPortal.SessionManager.TDJourneyParameters.ControlType.NewLocation);
            TDSessionManager.Current.InputPageState.MapLocationSearch.SearchType = SearchType.Map;

            // If its a car park selected, obtain the car park data and add to location
            if (TDSessionManager.Current.InputPageState.CarParkReference != string.Empty)
            {
                SetupCarParkPlanning(true);
            }

            // Set location for Door to door, Car, Cycle planners
            TDJourneyParametersMulti tdJourneyParameters = TDSessionManager.Current.JourneyParameters as TDJourneyParametersMulti;
            if (tdJourneyParameters != null)
            {
                if (pageId == PageId.FindCycleInput)
                {
                    tdJourneyParameters.CycleVia = TDSessionManager.Current.InputPageState.MapLocationSearch;
                    tdJourneyParameters.CycleViaLocation = TDSessionManager.Current.InputPageState.MapLocation;
                }
                else
                {
                    tdJourneyParameters.PrivateViaLocation = TDSessionManager.Current.InputPageState.MapLocation;
                    tdJourneyParameters.PrivateVia = TDSessionManager.Current.InputPageState.MapLocationSearch;
                }
            }

            TDSessionManager.Current.JourneyMapState.Location = TDSessionManager.Current.InputPageState.MapLocation;
            TDSessionManager.Current.JourneyMapState.LocationSearch = TDSessionManager.Current.InputPageState.MapLocationSearch;

            if (TDSessionManager.Current.InputPageState.MapMode != CurrentMapMode.FromJourneyInput
                && TDSessionManager.Current.InputPageState.MapMode != CurrentMapMode.FromFindAInput
                && TDSessionManager.Current.InputPageState.MapMode != CurrentMapMode.VisitPlannerLocationOrigin
                && TDSessionManager.Current.InputPageState.MapMode != CurrentMapMode.VisitPlannerLocation1
                && TDSessionManager.Current.InputPageState.MapMode != CurrentMapMode.VisitPlannerLocation2)
            {
                // Check to see if Destination is valid
                if (tdJourneyParameters.PrivateViaLocation.Status == TDLocationStatus.Valid)
                {
                    TDSessionManager.Current.InputPageState.MapMode = CurrentMapMode.FindJourneys;
                }
                else
                {
                    TDSessionManager.Current.InputPageState.MapMode = CurrentMapMode.TravelTo;
                    TDSessionManager.Current.InputPageState.MapLocationSearch = new LocationSearch();
                    TDSessionManager.Current.InputPageState.MapLocation = new TDLocation();
                    TDSessionManager.Current.InputPageState.MapLocationControlType.Type = TDJourneyParameters.ControlType.Default;
                }
            }
            // Check for all VisitPlanner modes because any Find on map button could have been selected on
            // input page
            else if (TDSessionManager.Current.InputPageState.MapMode == CurrentMapMode.VisitPlannerLocationOrigin
                     || TDSessionManager.Current.InputPageState.MapMode == CurrentMapMode.VisitPlannerLocation1
                     || TDSessionManager.Current.InputPageState.MapMode == CurrentMapMode.VisitPlannerLocation2)
            {
                // Set up location for Visit Planner input page
                TDJourneyParametersVisitPlan parameters = TDSessionManager.Current.JourneyParameters as TDJourneyParametersVisitPlan;

                // If there have been no journey planner parameters set up then do so now
                if (parameters == null)
                {
                    parameters = new TDJourneyParametersVisitPlan();
                }

                parameters.SetLocation(1, TDSessionManager.Current.InputPageState.MapLocation);
                parameters.SetLocationSearch(1, TDSessionManager.Current.InputPageState.MapLocationSearch);

                TDSessionManager.Current.JourneyParameters = parameters;
            }

            #region Save session and return next page URL

            TransitionEvent te = GetPageTransitionEvent(pageId);

            ITDSessionManager sessionManager = (ITDSessionManager)TDServiceDiscovery.Current[ServiceDiscoveryKey.SessionManager];
            sessionManager.FormShift[SessionKey.TransitionEvent] = te;
            sessionManager.SaveData();

            #endregion

            return GetNextPageUrl(pageId);
        }

        /// <summary>
        /// Sets stop information in session and returns stop information page url
        /// </summary>
        /// <param name="stopId">stop id - naptan</param>
        /// <returns>Url of stopinformation page</returns>
        public string SetStopInformation(string stopId, PageId pageId)
        {
            InputPageState inputPageState = TDSessionManager.Current.InputPageState;

            // Set stop code to be used by the StopInformation page
            inputPageState.StopCode = stopId;
            inputPageState.StopCodeType = TDCodeType.NAPTAN;
            inputPageState.ShowStopInformationPlanJourneyControl = false;
            // Prevent posible endless loop of Map -> Stop info -> Expand map -> Map -> Stop info
            inputPageState.ShowStopInformationMapControlExpandButton = false;

            // Prevents continuously adding pages (if above endless loop check fails)
            if (inputPageState.JourneyInputReturnStack.Count == 0)
                inputPageState.JourneyInputReturnStack.Push(pageId);
            else if (inputPageState.JourneyInputReturnStack.Count == 1)
            {
                // Handle scenario of Journey Input -> Find Nearest map -> Stop info
                // back being selected to return to journey input page
                PageId stackPageId = (PageId)inputPageState.JourneyInputReturnStack.Peek();

                if (stackPageId != pageId)
                    inputPageState.JourneyInputReturnStack.Push(pageId);
            }

            // Commit back to database
            TDSessionManager.Current.SaveData();

            return GetPageTransferURL(TransitionEvent.StopInformation, false);
        }

        /// <summary>
        /// Sets car park information in session and returns car park information page url
        /// </summary>
        /// <param name="carParkRef">Car park id</param>
        /// <param name="pageId">PageId to return back to after car park information page is displayed</param>
        /// <returns>Car park information page url</returns>
        public string SetCarParkInformation(string carParkRef, PageId pageId)
        {
            InputPageState inputPageState = TDSessionManager.Current.InputPageState;

            TDSessionManager.Current.InputPageState.CarParkReference = carParkRef;

            if (TDSessionManager.Current.InputPageState.JourneyInputReturnStack.Count == 0)
                TDSessionManager.Current.InputPageState.JourneyInputReturnStack.Push(pageId);

            ITDSessionManager sessionManager = (ITDSessionManager)TDServiceDiscovery.Current[ServiceDiscoveryKey.SessionManager];

            sessionManager.FormShift[SessionKey.TransitionEvent] = TransitionEvent.CarParkInformation;

            // Commit back to database
            TDSessionManager.Current.SaveData();

            // This will get the CarParkInformation page (using pageId and transition event)
            return GetNextPageUrl(pageId);
        }

        /// <summary>
        /// Saves map url in session. 
        /// Should be set before moving to a printable map page
        /// </summary>
        /// <param name="mapUrl">Url of map</param>
        /// <param name="isOutward">if true it is for outward otherwise for return</param>
        public void SaveMapState(string mapUrl, bool[][] mapSymbolsState, string mapViewTypeText, bool isOutward)
        {
            InputPageState inputPageState = TDSessionManager.Current.InputPageState;

            //clear the input page map state 
            ClearMapState();

            if (isOutward)
            {
                inputPageState.MapUrlOutward = mapUrl;
                inputPageState.IconSelectionOutward = mapSymbolsState;
                inputPageState.MapViewTypeOutward = mapViewTypeText;
            }
            else
            {
                inputPageState.MapUrlReturn = mapUrl;
                inputPageState.IconSelectionReturn = mapSymbolsState;
                inputPageState.MapViewTypeReturn = mapViewTypeText;
            }

            // Commit back to database
            TDSessionManager.Current.SaveData();
        }

        /// <summary>
        /// Saves map tile print details in session. 
        /// Should be set before moving to a printable map page
        /// </summary>
        /// <param name="mapUrl">Url of map</param>
        /// <param name="mapTileUrls">CyclePrintDetail array providing map tile urls and journey instructions</param>
        /// <param name="mapTileScale">Scale at which map tiles been taken</param>
        /// <param name="mapSymbolsState">Map Symbole control state</param>
        /// <param name="mapViewTypeText">Map view type text</param>
        /// <param name="isOutward">if true it is for outward otherwise for return</param>
        public void SaveCycleMapTileState(string mapUrl, string mapTileUrls, double mapTileScale, bool[][] mapSymbolsState, string mapViewTypeText, bool isOutward)
        {
            InputPageState inputPageState = TDSessionManager.Current.InputPageState;

            //clear the input page map state 
             ClearMapState();

            if (isOutward)
            {
                inputPageState.MapUrlOutward = mapUrl;
                inputPageState.IconSelectionOutward = mapSymbolsState;
                inputPageState.MapViewTypeOutward = mapViewTypeText;
                inputPageState.MapTileScaleOutward = Convert.ToInt32(mapTileScale);
                inputPageState.CycleMapTilesOutward = mapTileUrls;
            }
            else
            {
                inputPageState.MapUrlReturn = mapUrl;
                inputPageState.IconSelectionReturn = mapSymbolsState;
                inputPageState.MapViewTypeReturn = mapViewTypeText;
                inputPageState.MapTileScaleReturn = Convert.ToInt32(mapTileScale);
                inputPageState.CycleMapTilesReturn = mapTileUrls;
            }

            // Commit back to database
            TDSessionManager.Current.SaveData();
        }

        /// <summary>
        /// Clears the Map state(i.e. urls, view type , tile urls) stored in the input page state.
        /// The reason behind clearing states is to make sure correct map displays on printer friendly page.
        /// </summary>
        public void ClearMapState()
        {
            InputPageState inputPageState = TDSessionManager.Current.InputPageState;
            
            //Clear the outward map state
            inputPageState.MapUrlOutward = string.Empty;
            inputPageState.MapViewTypeOutward = string.Empty;
            inputPageState.MapTileScaleOutward = 0;
            inputPageState.CycleMapTilesOutward = string.Empty;

            //Clear the return map state
            inputPageState.MapUrlReturn = string.Empty;
            inputPageState.MapViewTypeReturn = string.Empty;
            inputPageState.MapTileScaleReturn = 0;
            inputPageState.CycleMapTilesReturn = string.Empty;
            
        }
        #endregion

        #region MapControl2 StopInformation Link
        /// <summary>
        /// Creates and return stop information links or car park information links for a location.
        /// The links contain the hyperlink tag. 
        /// If location is not a stop or car park then returns an empty string.
        /// </summary>
        /// <param name="stopLocation">TD location for which stop/car informaion link required</param>
        /// <returns>stop/car information link</returns>
        public string GetStopInformationLinks(TDLocation stopLocation)
        {
            StringBuilder stopInformationLinks = new StringBuilder();

            if (stopLocation != null)
            {
                #region Car park

                // If the stop location has got a carpark its a car park. Return car park link.
                // Car park set using Find a Car Park
                if (stopLocation.CarParking != null && !string.IsNullOrEmpty(stopLocation.CarParking.CarParkReference))
                {
                    stopInformationLinks.Append(GetCarParkInformationLink(stopLocation.CarParking.CarParkReference));
                    stopInformationLinks.Append("<br />");
                    
                    // Return here as this is a car park location and therefore shouldnt include the stop links
                    return stopInformationLinks.ToString();
                }

                #endregion

                #region Park and ride scheme car park

                // If the stop location has got a carpark its a car park. Return car park link.
                // Car park set using Park and Ride
                if (stopLocation.CarPark != null && !string.IsNullOrEmpty(stopLocation.CarPark.UrlLink))
                {
                    // Car park
                    stopInformationLinks.Append(
                        GetExternalInformationLink(
                        stopLocation.CarPark.UrlLink, 
                        string.Format("{0} {1}", stopLocation.CarPark.CarParkName, GetResource("ParkAndRide.CarkPark.Suffix"))));
                                        
                    // Park and ride 
                    if (stopLocation.ParkAndRideScheme != null && !string.IsNullOrEmpty(stopLocation.ParkAndRideScheme.UrlLink))
                    {
                        stopInformationLinks.Append(
                            GetExternalInformationLink(
                            stopLocation.ParkAndRideScheme.UrlLink, 
                            string.Format("{0} {1}", stopLocation.ParkAndRideScheme.Location, GetResource("ParkAndRide.Suffix"))));
                    }

                    // Return here as this is a car park location and therefore shouldnt include the stop links
                    return stopInformationLinks.ToString();
                }

                #endregion

                #region Stop naptan

                //If the stop location has naptan(s), build and return stop information page link.
                if (stopLocation.NaPTANs != null && stopLocation.NaPTANs.Length > 0)
                {
                    // If there is more than one naptan, then include the stop name in the link
                    bool showStopName = (stopLocation.NaPTANs.Length > 1);

                    for (int i = 0; i < stopLocation.NaPTANs.Length; i++)
                    {
                        TDNaptan naptan = stopLocation.NaPTANs[i];

                        // Have to check for valid naptan because if a journey was from a Postcode,
                        // then the naptan will be the postcode and be called "Origin". This shouldn't be
                        // made into a link
                        if (IsValidNaptan(naptan.Naptan))
                        {
                            stopInformationLinks.Append(GetStopInformationLink(naptan, showStopName));
                        }
                    }
                }

                #endregion

            }
            return stopInformationLinks.ToString();
        }

        /// <summary>
        /// Creates and returns a stop information link for a naptan.
        /// The link contains the hyperlink tag. 
        /// </summary>
        /// <param name="naptan">Naptan for which stop informaion link required</param>
        /// <returns>stop information link</returns>
        public string GetStopInformationLink(string naptan)
        {
            string stopInformationLink = string.Empty;

            // Ensure its a valid naptan before assembling link
            if ((!string.IsNullOrEmpty(naptan)) && (IsValidNaptan(naptan)))
            {
                string stopInformationLinkTemplate = GetResource("MapHelper.StopInformationLink");

                if (!string.IsNullOrEmpty(stopInformationLinkTemplate))
                {
                    stopInformationLink = string.Format(stopInformationLinkTemplate, naptan);
                }
            }

            return stopInformationLink;
        }

        /// <summary>
        /// Creates and returns a stop information link for a naptan.
        /// The link contains the hyperlink tag. 
        /// </summary>
        /// <param name="naptan">Naptan for which stop informaion link required</param>
        /// <returns>stop information link</returns>
        public string GetStopInformationLink(TDNaptan naptan, bool showStopName)
        {
            string stopInformationLink = string.Empty;

            if (naptan != null)
            {
                // Set the link text based on if stop name should be displayed
                if (showStopName)
                {
                    #region Set up info link with name

                    if (IsValidNaptan(naptan.Naptan))
                    {
                        bool nameOk = true;
                        string name = naptan.Name;

                        // Check there is a name for this naptan
                        if (string.IsNullOrEmpty(name))
                        {
                            NaptanCacheEntry nce = NaptanLookup.Get(naptan.Naptan, "Naptan");

                            nameOk = nce.Found;
                            
                            if (nce.Found)
                            {
                                name = nce.Description;
                            }
                        }

                        if (nameOk)
                        {
                            // Remove ' from the name
                            name = name.Replace("'", "\\'");

                            string stopInformationLinkTemplate = GetResource("MapHelper.StopInformationLink.SpecificStop");

                            // Naptan has a name, use the name in the link
                            stopInformationLink = string.Format(stopInformationLinkTemplate, naptan.Naptan, name);
                        }
                        else
                        {
                            // No name found for naptan, so need to set up the standard stop link
                            stopInformationLink = GetStopInformationLink(naptan.Naptan);
                        }
                    }
                    #endregion
                }
                else
                {
                    // Get a standard stop link with no name
                    stopInformationLink = GetStopInformationLink(naptan.Naptan);
                }
                
            }

            return stopInformationLink;
        }

        /// <summary>
        /// Creates and returns a car park information link for a car park reference.
        /// The link contains the hyperlink tag. 
        /// </summary>
        /// <param name="carParkReference">CarParkReference for which informaion link required</param>
        /// <returns>car park information link</returns>
        public string GetCarParkInformationLink(string carParkReference)
        {
            string carParkLink = string.Empty;

            if (!string.IsNullOrEmpty(carParkReference))
            {
                string carParkLinkTemplate = GetResource("MapHelper.CarParkInformationLink");
                if (!string.IsNullOrEmpty(carParkLinkTemplate))
                {
                    carParkLink = string.Format(carParkLinkTemplate, carParkReference);
                }
            }

            return carParkLink;
        }

        /// <summary>
        /// Creates and returns an information link to be displayed for an external site
        /// </summary>
        /// <param name="url">URL to the information link</param>
        /// <param name="description">Description to show</param>
        /// <returns>information link</returns>
        public string GetExternalInformationLink(string url, string description)
        {
            string informationLink = string.Empty;

            if (!string.IsNullOrEmpty(url))
            {
                string informationLinkTemplate = GetResource("MapHelper.InformationLink.External");
                if (!string.IsNullOrEmpty(informationLinkTemplate))
                {
                    informationLink = string.Format(informationLinkTemplate, url, description, GetResource("ExternalLinks.OpensNewWindowImage"));
                }
            }

            return informationLink;
        }

        #endregion

        #region Walkit link
        /// <summary>
        /// Creates and returns a walkit link from walkit url to show on map
        /// The link contains the hyperlink tag. 
        /// </summary>
        /// <param name="url">Walkit URL</param>
        /// <returns></returns>
        public string GetWalkitLink(string url)
        {
            string walkitLink = string.Empty;

            if (!string.IsNullOrEmpty(url))
            {
                string walkitTemplate = GetResource("MapHelper.WalkitLink");
                if (!string.IsNullOrEmpty(walkitTemplate))
                {
                    walkitLink = string.Format(walkitTemplate, url, GetResource("ExternalLinks.OpensNewWindowImage"));
                }
            }

            return walkitLink;
        }

        #endregion

        #endregion Public methods

        #region Private methods

        /// <summary>
        /// Loads any global strings/properties needed by the helper
        /// </summary>
        private void LoadResources()
        {
            unformattedLegDescription = Global.tdResourceManager.GetString("MapToolsControl.PTLegDescription", TDCultureInfo.CurrentUICulture);

            // start & end node description (other nodes are numbered)
            startJourneyDescription = Global.tdResourceManager.GetString("MapToolsControl.StartJourneyDescription", TDCultureInfo.CurrentUICulture);
            endJourneyDescription = Global.tdResourceManager.GetString("MapToolsControl.EndJourneyDescription", TDCultureInfo.CurrentUICulture);

            // Can't seem to read in a space from langStrings file so reading in a # and replacing with a space
            legDescriptionPadding = Global.tdResourceManager.GetString("MapToolsControl.PTLegDescriptionPadding", TDCultureInfo.CurrentUICulture);
            legDescriptionPadding = legDescriptionPadding.Replace("#", " ");

            directionText = Global.tdResourceManager.GetString ("JourneyMapControl.Direction", TDCultureInfo.CurrentUICulture);

            toidPrefix = Properties.Current["JourneyControl.ToidPrefix"];
        }

        private string Format(object o)
        {
            string msg = string.Empty;
            if (o is Hashtable)
            {
                msg += " ([";
                foreach (object key in ((Hashtable)o).Keys)
                {
                    msg += key + "=" + Format(((Hashtable)o)[key]) + ", ";
                }
                msg += ")] ";
            }
            else if (o is ArrayList)
            {
                msg += " {";
                foreach (object tmpO in (ArrayList)o)
                {
                    msg += Format(tmpO) + ", ";
                }
                msg += "} ";
            }
            else
            {
                msg += o;
            }
            return msg;
        }

        private OSGridReference[] FindIntermediateNodesGridReferencesSingle(bool outward)
        {
            // Either a single journey in the itinerary has been selected or there is only one journey in the search
            Journey journey = FindRelevantJourney(outward);

            // This should only be called for a public journey.
            // Set journey type to be public journey - if the journey is of public journey type then
            // this will return an object else it will return null.
            OSGridReference[] singleJourneyGridReferences = new OSGridReference[0];
            if ((journey as JourneyControl.PublicJourney) != null)
            {
                JourneyControl.PublicJourney ptJourney = (JourneyControl.PublicJourney)(journey);
                singleJourneyGridReferences = ptJourney.GetJourneyNodesGridReferences();
            }
            else
            {
                // Journey type is not a public journey therefore there are no changes so we just
                // return an empty array.
                return new OSGridReference[0];
            }
            return singleJourneyGridReferences;
        }

        private OSGridReference[] FindIntermediateNodesGridReferencesFullItinerary(bool outward)
        {
            TDItineraryManager itineraryManager = TDItineraryManager.Current;
            Journey[] journeys = (outward) ? itineraryManager.OutwardJourneyItinerary : itineraryManager.ReturnJourneyItinerary;
            Journey journey;

            int itineraryJourneyCount = journeys.Length;
            ArrayList overallJourneyGridReferences3D = new ArrayList(itineraryJourneyCount);
            int nodesGathered = 0;	// Keep a track of how many nodes are gathered so we can size the overall array

            // Need to get the nodes for all journeys in the itinerary
            // Itinerary Manager stores Return journeys in same order as Outward journeys.
            // This code loops forward through journeys array if Outward, but loops 
            // backwards through the array if dealing with Return journeys.
            for (int journeyIndex = 0; journeyIndex < itineraryJourneyCount; journeyIndex++)
            {
                journey = journeys[journeyIndex];

                // An array to store the locations for a single journey
                OSGridReference[] singleJourneyGridReferences = new OSGridReference[2];

                // Get the intermediate nodes from the journey legs
                singleJourneyGridReferences = journey.GetJourneyNodesGridReferences();
                nodesGathered += singleJourneyGridReferences.Length;

                // Store the grid ref arrays in a compound overall 3D journey array
                overallJourneyGridReferences3D.Add(singleJourneyGridReferences);
            }

            // We now have all grid references. Amalgamate into a single array.

            OSGridReference[] overallJourneyGridReferences = new OSGridReference[nodesGathered - (itineraryJourneyCount - 1)];
            // Set up a new indexing variable to count the journeys as we loop around them
            int journeyIndex2 = 0;
            int overallJourneyArrayIndex = 0;
            // Loop around the overall journeys array
            foreach (OSGridReference[] singleJourneyGridRefs in overallJourneyGridReferences3D)
            {
                int nodeIndex = 0;
                // Loop around the nodes of a single journey
                foreach (OSGridReference osGridRefs in singleJourneyGridRefs)
                {
                    // Include the *start* location of a new overall journey (but no other start locations)
                    // include the intermediate nodes of all journeys (including end location)
                    if ((journeyIndex2 == 0) || (nodeIndex != 0))
                    {
                        overallJourneyGridReferences[overallJourneyArrayIndex] = osGridRefs;
                        overallJourneyArrayIndex++;
                    }
                    nodeIndex++;
                }
                journeyIndex2++;

            }
            return overallJourneyGridReferences;
        }

        /// <summary>
        /// Returns journey summary
        /// </summary>
        /// <param name="result"></param>
        /// <param name="itpJourney"></param>
        /// <param name="currentSelected"></param>
        /// <param name="arriveBefore"></param>
        /// <param name="modeTypes"></param>
        /// <param name="outward"></param>
        /// <returns></returns>
        private JourneySummaryLine GetJourneySummary(ITDJourneyResult result, bool itpJourney, int currentSelected, bool arriveBefore, ModeType[] modeTypes, bool outward)
        {
            JourneySummaryLine summary = null;
            JourneySummaryLine[] summarylines = outward ? result.OutwardJourneySummary(arriveBefore, modeTypes) : result.ReturnJourneySummary(arriveBefore, modeTypes);
            if (!itpJourney)
            {
                summary = summarylines[currentSelected];
            }
            else
            {


                List<JourneySummaryLine> itpjourneys = new List<JourneySummaryLine>();

                foreach (JourneySummaryLine jsl in summarylines)
                {
                    ArrayList journeyModes = new ArrayList();
                    journeyModes.AddRange(jsl.Modes);

                    bool coachmode = ((journeyModes.Contains(ModeType.Coach)) || (journeyModes.Contains(ModeType.Bus)));
                    bool trainmode = ((journeyModes.Contains(ModeType.Rail))
                               ||
                             (journeyModes.Contains(ModeType.RailReplacementBus))
                               ||
                             (journeyModes.Contains(ModeType.Metro))
                               ||
                             (journeyModes.Contains(ModeType.Tram))
                               ||
                             (journeyModes.Contains(ModeType.Underground)));

                    bool airmode = journeyModes.Contains(ModeType.Air);

                    if (airmode && (coachmode || trainmode))
                        itpjourneys.Add(jsl);
                    if (coachmode && trainmode && !airmode)
                        itpjourneys.Add(jsl);


                }

                summary = itpjourneys[currentSelected];
            }

            return summary;
        }

        /// <summary>
        /// Returns false if the number of maps is more than the target requested
        /// </summary>
        /// <returns></returns>
        private bool IsNumberOfMapsNearTarget(int numberOfMaps, int target)
        {
            return (numberOfMaps <= target);
        }

        /// <summary>
        /// Gets the the first OSGR found which matches in the two geometry arrays (the intersectionOSGR).
        /// Also sets the first OSGR coordinate found in the currentGeometry array.
        /// </summary>
        /// <param name="inteserctionOSGR"></param>
        /// <param name="firstOSGR"></param>
        public void GetIntersectionCoordinate(Dictionary<int, OSGridReference[]> previousGeometry, Dictionary<int, OSGridReference[]> currentGeometry,
            out OSGridReference intersectionOSGR, out OSGridReference firstOSGR)
        {
            bool intersectionOSGRFound = false;
            intersectionOSGR = null;
            firstOSGR = null;

            // Loop through the two OSGR arrays and find the coordinate which is in both.
            // This will indicate the point where the two sections intersect/join.
            foreach (KeyValuePair<int, OSGridReference[]> keyvaluePreviousGeometry in previousGeometry)
            {
                if (!intersectionOSGRFound)
                {
                    foreach (OSGridReference previousOSGR in keyvaluePreviousGeometry.Value)
                    {
                        if (!intersectionOSGRFound)
                        {
                            foreach (KeyValuePair<int, OSGridReference[]> keyvalueCurrentGeometry in currentGeometry)
                            {
                                if (!intersectionOSGRFound)
                                {
                                    foreach (OSGridReference currentOSGR in keyvalueCurrentGeometry.Value)
                                    {
                                        // Fall back is to use the first OSGR of currentDetail
                                        if (firstOSGR == null)
                                            firstOSGR = currentOSGR;

                                        // Check if there is a matching OSGR
                                        if ((currentOSGR.Easting == previousOSGR.Easting)
                                            &&
                                            (currentOSGR.Northing == previousOSGR.Northing))
                                        {
                                            intersectionOSGR = currentOSGR;
                                            intersectionOSGRFound = true;
                                            break;
                                        }
                                    }
                                }
                                else
                                    break;
                            }
                        }
                        else
                            break;
                    }
                }
                else
                    break;
            }
        }

        /// <summary>
        /// Returns an OSGR array as a string, with each coordinate int seperated by a ","
        /// </summary>
        /// <param name="osgrs"></param>
        /// <returns></returns>
        private string GetOSGRsAsString(OSGridReference[] osgrs)
        {
            StringBuilder sbOsgr = new StringBuilder();

            foreach (OSGridReference osgr in osgrs)
            {
                sbOsgr.Append(osgr.Easting.ToString() + ",");
                sbOsgr.Append(osgr.Northing.ToString() + ",");
            }

            string osgrString = sbOsgr.ToString();

            // Remove last comma
            osgrString = osgrString.TrimEnd(',');

            return osgrString;
        }

        /// <summary>
        /// Returns the next page TransitionEvent for the current page id, 
        /// defaults to Door to door JourneyPlannerInputDefault
        /// </summary>
        /// <param name="pageId"></param>
        /// <returns></returns>
        private TransitionEvent GetPageTransitionEvent(PageId pageId)
        {
            // Always send to Door to door input when coming from FindMapResult
            TransitionEvent te = TransitionEvent.JourneyPlannerInputDefault;

            switch (pageId)
            {
                case PageId.FindMapResult:
                // te already set
                    break;
                case PageId.JourneyPlannerAmbiguity:
                    te = TransitionEvent.JourneyPlannerAmbiguityDefault;
                    break;
                case PageId.FindCarInput:
                    te = TransitionEvent.FindCarInputDefault;
                    break;
            }
            
            return te;
        }

        /// <summary>
        /// Returns NextPageUrl for the PageId provided
        /// </summary>
        /// <param name="pageId"></param>
        /// <returns></returns>
        private string GetNextPageUrl(PageId pageId)
        {
            IPageController pageController = (PageController)TDServiceDiscovery.Current[ServiceDiscoveryKey.PageController];

            PageId nextPageId = pageController.GetNextPageId(pageId);

            string url = "";

            if (nextPageId != pageId || TDSessionManager.Current.FormShift[SessionKey.ForceRedirect])
            {
                // Returned Page-id differs from the current value, so
                // call GetPageTransferDetails to get the corresponding Url
                // for the Response.Redirect.

                PageTransferDetails pageTransferDetails = pageController.GetPageTransferDetails(nextPageId);

                // PageId of PageId.Links is a special case, because it represents "StaticNoPrint.aspx". 
                // This is used by a number of different CMS templates, so we cannot redirect to it 
                // directly. However, we should never be going *to* these pages via a PostBack, so   
                // we can assume that any post to these has come from the same page and so we can
                // use the CMS URL of the current page instead. 

                if (nextPageId != PageId.Links)
                {
                    // Perform the response redirect
                    url = pageTransferDetails.PageUrl;

                    if (TDPage.SessionChannelName != null)
                    {
                        url = baseChannelURL + url;
                    }

                    // IR1063 - Add cacheParam for Browser Back button functionality

                    if (url.IndexOf("?") > 0)
                    {
                        url += "&" + "cacheparam=" + TDSessionManager.Current.CacheParam;
                        TDSessionManager.Current.CacheParam = TDSessionManager.Current.CacheParam + 1;
                    }
                    else
                    {
                        url += "?" + "cacheparam=" + TDSessionManager.Current.CacheParam;
                        TDSessionManager.Current.CacheParam = TDSessionManager.Current.CacheParam + 1;
                    }

                    // Add anchor #<id> here
                    if (TDSessionManager.Current.Session[SessionKey.Anchor] != null)
                    {
                        string anch = TDSessionManager.Current.Session[SessionKey.Anchor];
                        url += "#" + anch;

                    }

                    // Add querystring to FindStationInput url in case OneUseKey NotFindAMode is used
                    string notFindAMode = TDSessionManager.Current.GetOneUseKey(SessionKey.NotFindAMode) as string;
                    if (nextPageId == PageId.FindStationInput)
                    {
                        if (notFindAMode != null)
                        {
                            url += "?NotFindAMode=true";
                        }
                    }

                    // Add querystring to FindTrunkInput url in case OneUseKey ClassicMode is used
                    string classicMode = TDSessionManager.Current.GetOneUseKey(SessionKey.ClassicMode) as string;
                    if (nextPageId == PageId.FindTrunkInput)
                    {
                        if (classicMode != null)
                        {
                            url += "?ClassicMode=true";
                        }
                    }

                    //Add query string to FindFareInput url to determine correct mode. Check if OneUseKey
                    //is in use.
                    string findModeTrunkCostBased = TDSessionManager.Current.GetOneUseKey(SessionKey.FindModeTrunkCostBased) as string;
                    if (nextPageId == PageId.FindFareInput)
                    {
                        if (findModeTrunkCostBased != null)
                        {
                            url += "?FindMode=TrunkCostBased";
                        }
                    }
                }

                // Finally remove the abandon from query string as no longer needed.
                // Ensures we detect future timeouts correctly
                if (url.IndexOf(abandonKey) >= 0)
                {
                    url = url.Replace("abandon=true", string.Empty);
                }
            }
                      
            return  url;
        }

        /// <summary>
        /// Returns the page transfer url object for the provided TransitionEvent
        /// Flag to add the abandon query string value
        /// </summary>
        /// <param name="transitionEvent"></param>
        /// <returns></returns>
        private string GetPageTransferURL(TransitionEvent transitionEvent, bool abandonFlag)
        {
            // Get the PageController from Service Discovery
            IPageController pageController = (PageController)TDServiceDiscovery.Current[ServiceDiscoveryKey.PageController];

            // Get the PageTransferDataCache from the pageController
            IPageTransferDataCache pageTransferDataCache = pageController.PageTransferDataCache;

            // Now, get the pageId associated with the transiton event.
            PageId transferPage = pageTransferDataCache.GetPageEvent(transitionEvent);

            // Get the PageTransferDetails object to which holds the Url
            PageTransferDetails pageTransferDetails = pageController.GetPageTransferDetails(transferPage);

            StringBuilder url = new StringBuilder();

            // url prefix
            if (TDPage.SessionChannelName != null)
            {
                url.Append(baseChannelURL);
            }

            // the page to transfer to 
            url.Append(pageTransferDetails.PageUrl);
            
           
            if (abandonFlag)
            {
                // abandon to prevent endless session timeout loop
                url.Append((url.ToString().IndexOf("?") > -1) ? "&" : "?");
                url.Append(abandonQueryString);
            }

            return url.ToString();
        }

        /// <summary>
        /// Checks the planning point of the car park to determine which grid
        /// reference should be used for journey planning. If the car park 
        /// should be used as a planning point, the map grid reference of 
        /// the centre of the car park should be used. If the car park is 
        /// not a planning point, and there is a street name for the entrance
        /// (if the car park is the journey destination), or the exit (if
        /// the car park is the journey origin), then use the grid reference
        /// for the street. If no street name is available, use road name
        /// search ...
        /// </summary>
        /// <param name="theLocation"></param>
        /// <param name="carPark"></param>
        /// <param name="origin"></param>
        /// <returns></returns>
        private TDLocation SetUpGridRefenceAndToids(TDLocation theLocation, CarPark carPark, bool origin)
        {
            // check the access point
            CarParkAccessPoint[] pointsList = carPark.AccessPoints;

            for (int i = 0; i < pointsList.Length; i++)
            {
                CarParkAccessPoint accessPoint = pointsList[i];
                string geocodeType = accessPoint.GeocodeType;

                if (carPark.PlanningPoint)
                {
                    // if its a planning point and a map access point
                    if (string.Compare(geocodeType, PARKING_MAP, true) == 0)
                    {
                        theLocation.GridReference = accessPoint.GridReference;
                        theLocation.Toid = carPark.GetMapToids();
                    }
                }
                else
                {
                    // car park is the origin so use the exit co-ordinates if there are any
                    if (accessPoint.StreetName != null)
                    {
                        if (origin)
                        {
                            // get the exit street name to use for name search
                            if (string.Compare(geocodeType, PARKING_EXIT, true) == 0)
                            {
                                theLocation.AddressToMatch = accessPoint.StreetName;
                                theLocation.GridReference = accessPoint.GridReference;
                                theLocation.Toid = carPark.GetExitToids();
                            }
                        }
                        else
                        {
                            // get the entrance street name to use for name search
                            if (string.Compare(geocodeType, PARKING_ENTRANCE, true) == 0)
                            {
                                theLocation.AddressToMatch = accessPoint.StreetName;
                                theLocation.GridReference = accessPoint.GridReference;
                                theLocation.Toid = carPark.GetEntranceToids();
                            }
                        }
                    }
                    else
                    {
                        if (origin)
                        {
                            // use the exit grid reference
                            if (string.Compare(geocodeType, PARKING_EXIT, true) == 0)
                            {
                                theLocation.GridReference = accessPoint.GridReference;
                                theLocation.Toid = carPark.GetExitToids();
                            }
                        }
                        else
                        {
                            // use the entrance grid reference
                            if (string.Compare(geocodeType, PARKING_ENTRANCE, true) == 0)
                            {
                                theLocation.GridReference = accessPoint.GridReference;
                                theLocation.Toid = carPark.GetEntranceToids();
                            }
                        }
                    }
                }
            }

            return theLocation;
        }
        
        /// <summary>
        /// Populates the MapLocation in the session with the CarParking object. 
        /// Assigns the GridReference and TOIDs to use for the location.
        /// </summary>
        /// <param name="origin">boolean true if the car park is the journey origin location</param>
        private void SetupCarParkPlanning(bool origin)
        {
            //Load car park data for car park
            ICarParkCatalogue carParkCatalogue = (ICarParkCatalogue)TDServiceDiscovery.Current[ServiceDiscoveryKey.CarParkCatalogue];
            string carParkRef = TDSessionManager.Current.InputPageState.CarParkReference;

            // access the car park data
            CarPark carPark = carParkCatalogue.GetCarPark(carParkRef);

            // set the car park
            TDLocation theLocation = TDSessionManager.Current.InputPageState.MapLocation;

            if (carPark != null)
            {
                theLocation.CarParking = carPark;

                // poulate the grid reference, address to match, and toids
                theLocation = SetUpGridRefenceAndToids(theLocation, carPark, origin);

                // set the location locality, prevents ambiguity page being displayed in journey planning
                IGisQuery gisQuery = (IGisQuery)TDServiceDiscovery.Current[ServiceDiscoveryKey.GisQuery];
                theLocation.Locality = gisQuery.FindNearestLocality(
                    theLocation.GridReference.Easting, theLocation.GridReference.Northing);

                // Because the car park can use different entrance and exit coordinates
                // need to set up to return to the same location but using the appropriate coordinates
                TDLocation theReturnLocation = new TDLocation(carParkRef, theLocation.Description, !origin);

                if (origin)
                    TDSessionManager.Current.JourneyParameters.ReturnDestinationLocation = theReturnLocation;
                else
                    TDSessionManager.Current.JourneyParameters.ReturnOriginLocation = theReturnLocation;
            }
            else
            {
                // Log car park was not found in the Car park catalogue
                Logger.Write(new OperationalEvent(
                    TDEventCategory.Infrastructure,
                    TDTraceLevel.Warning,
                    "Unable to find Car park in the CarParkCatalogue, ID: " + carParkRef + " " + theLocation.Description));

                // Attempt to populate as much information as we can to carry on planning the journey
                // to the click point. This will mean the Car park cost is not shown in the Tickets/costs page
                // because the car park object was not found.
                theLocation.PopulateToids();

                // set the location locality
                IGisQuery gisQuery = (IGisQuery)TDServiceDiscovery.Current[ServiceDiscoveryKey.GisQuery];
                theLocation.Locality = gisQuery.FindNearestLocality(
                    theLocation.GridReference.Easting, theLocation.GridReference.Northing);
            }

            TDSessionManager.Current.InputPageState.MapLocation = theLocation;
        }

        /// <summary>
        /// Returns a TDLocation object based on osgr coordinates passed
        /// </summary>
        /// <param name="easting">osgr easting value</param>
        /// <param name="northing">osgr northing value</param>
        /// <param name="locationText">location text</param>
        /// <returns>TDLocation object</returns>
        private TDLocation GetLocationFromGridReference(int easting, int northing, string locationText)
        {
            TDLocation location = new TDLocation();
            try
            {
                location.Description = locationText;

                OSGridReference defaultosgren = new OSGridReference(easting, northing);
                location.GridReference = defaultosgren;
                location.PopulateToids();
                location.Locality = PopulateLocality(defaultosgren);
                location.RequestPlaceType = RequestPlaceType.Coordinate;
                location.Status = TDLocationStatus.Valid;
                
            }
            catch (Exception ex)
            {
                //osgrid not resolved, redirect to input page
                OperationalEvent operationEvent =
                    new OperationalEvent(TDEventCategory.Business,
                    TDTraceLevel.Error,
                    "TDMapWebService: Coordinates provided could not be resolved into a TDLocation." + ex.StackTrace);
                Logger.Write(operationEvent);

                throw;
            }

            return location;
        }

        /// <summary>
        /// Populate locality data into relevant object hierarchy
        /// </summary>
        /// <param name="osgr"></param>
        /// <returns></returns>
        private string PopulateLocality(OSGridReference osgr)
        {
            IGisQuery gisQuery = (IGisQuery)TDServiceDiscovery.Current[ServiceDiscoveryKey.GisQuery];

            return gisQuery.FindNearestLocality(osgr.Easting, osgr.Northing);
        }


        /// <summary>
        /// Method which checks if the naptan id starts with an unrecognised prefix, e.g. "Orig", or "Dest"
        /// </summary>
        /// <param name="naptanId"></param>
        /// <returns></returns>
        private bool IsValidNaptan(string naptanId)
        {
            if (string.IsNullOrEmpty(naptanId))
            {
                return false;
            }

            if ((naptanId.ToLower().StartsWith("orig")) || 
                (naptanId.ToLower().StartsWith("dest")))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        #endregion  
    }

    #region Public Enums

    public enum MapAdditionalIconSelected
    {
        None,
        Accomodation,
        Sport,
        Education,
        Infrastructure,
        Health,
        Attractions

    }

    /// <summary>
    /// Enum defining the map toolbar options to show on a map in MapControl2
    /// </summary>
    public enum MapToolbarTool
    {
        UserDefinedLocation,
        SelectNearbyLocation,
        Zoom,
        Pan
    }

    /// <summary>
    /// Enum defining the map location info popup links mode, i.e. the links available on the location 
    /// info pop up to "plan a journey from the Start, Via, or End"
    /// </summary>
    public enum MapLocationMode
    {
        Start,
        Via,
        End,
        None
    }

    /// <summary>
    /// Enum defining the symbol type to use for a location point shown on a map
    /// </summary>
    public enum MapLocationSymbolType
    {
        Circle,
        Square,
        Triangle,
        Diamond,
        Start,
        Via,
        End,
        Ferry,
        Toll,
        PushPin,
        Custom
    }

    /// <summary>
    /// Enum defining the journey type to add to the map
    /// </summary>
    public enum MapJourneyType
    {
        PublicTransport,
        Road,
        Cycle
    }

    /// <summary>
    /// Enum for maps travel news layer filter - transport type
    /// </summary>
    public enum MapTransportType
    {
        All,
        Road,
        Public,
        None
    }

    /// <summary>
    /// Enum for maps travel news layer filter - incident type
    /// </summary>
    public enum MapIncidentType
    {
        All,
        Planned,
        Unplanned
    }

    /// <summary>
    /// Enum for maps travel news layer filter - severity
    /// </summary>
    public enum MapSeverity
    {
        All,
        Major
    }

    /// <summary>
    /// Enum for maps travel news layer filter - time period
    /// </summary>
    public enum MapTimePeriod
    {
        Current,
        Recent,
        Date,
        DateTime
    }

    #endregion

    #region Public Classes

    /// <summary>
    /// Class containing parameters for initialising a map on MapControl2
    /// </summary>
    public class MapParameters
    {
        #region Private members

        private int mapScale = -1;
        private int mapScaleLevel = -1;
        private OSGridReference mapCentre;
        private OSGridReference mapEnvelopeMin;
        private OSGridReference mapEnvelopeMax;
        private string mapInfoDisplayText;
        private MapToolbarTool[] mapToolbarTools;
        private MapLocationMode[] mapLocationModes;
        private int mapHeight;
        private int mapWidth;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public MapParameters()
        {
            mapScale = -1;
            mapScaleLevel = -1;
            mapCentre = new OSGridReference();
            mapEnvelopeMin = new OSGridReference();
            mapEnvelopeMax = new OSGridReference();
            mapInfoDisplayText = string.Empty;
            mapToolbarTools = new MapToolbarTool[0];
            mapLocationModes = new MapLocationMode[0];
            mapHeight = -1;
            mapWidth = -1;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mapScale">The map scale to be used. 1 to 10000000. Set -1 to use default.</param>
        /// <param name="mapScaleLevel">The map scale level to be used. 1 to 13. Set -1 to use default.</param>
        /// <param name="mapCentre">The coordinate to centre the map on. Set null to use default.</param>
        /// <param name="mapEnvelopeMin">The map envelope min coordinate. Set null to use default.</param>
        /// <param name="mapEnvelopeMax">The map envelope max coordinate. Set null to use default.</param>
        /// <param name="mapInfoDisplayText">The map info window popup display text. Set null to use default.</param>
        /// <param name="mapToolbarTools">The map toolbar tools to be shown, in addition to the default
        /// pan and zoom. Set null to use default.</param>
        /// <param name="mapLocationModes">The map location modes to show in the info window popup</param>
        public MapParameters(int mapScale, int mapScaleLevel,
            OSGridReference mapCentre, OSGridReference mapEnvelopeMin, OSGridReference mapEnvelopeMax,
            string mapInfoDisplayText, MapToolbarTool[] mapToolbarTools, MapLocationMode[] mapLocationModes)
        {
            this.mapScale = mapScale;
            this.mapScaleLevel = mapScaleLevel;
            this.mapCentre = mapCentre;
            this.mapEnvelopeMin = mapEnvelopeMin;
            this.mapEnvelopeMax = mapEnvelopeMax;
            this.mapInfoDisplayText = mapInfoDisplayText;
            this.mapToolbarTools = mapToolbarTools;
            this.mapLocationModes = mapLocationModes;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Read/Write. The map scale to be used. 1 to 10000000.
        /// Setting this value will override MapScaleLevel.
        /// </summary>
        public int MapScale
        {
            get { return mapScale; }
            set { mapScale = value; }
        }

        /// <summary>
        /// Read/Write. The map scale level to be used. 1 to 13.
        /// This indicates the zoom slider level setting.
        /// </summary>
        public int MapScaleLevel
        {
            get { return mapScaleLevel; }
            set { mapScaleLevel = value; }
        }

        /// <summary>
        /// Read/Write. The coordinate to centre the map on.
        /// Setting this value will override the MapEnvelope values.
        /// </summary>
        public OSGridReference MapCentre
        {
            get { return mapCentre; }
            set { mapCentre = value; }
        }

        /// <summary>
        /// Read/Write. The map envelope min coordinate.
        /// </summary>
        public OSGridReference MapEnvelopeMin
        {
            get { return mapEnvelopeMin; }
            set { mapEnvelopeMin = value; }
        }

        /// <summary>
        /// Read/Write. The map envelope max coordinate.
        /// </summary>
        public OSGridReference MapEnvelopeMax
        {
            get { return mapEnvelopeMax; }
            set { mapEnvelopeMax = value; }
        }

        /// <summary>
        /// Read/Write. The map info window popup display text.
        /// </summary>
        public string MapInfoDisplayText
        {
            get { return mapInfoDisplayText; }
            set { mapInfoDisplayText = value; }
        }

        /// <summary>
        /// Read/Write. The map toolbar tools to be shown, in addition to the default.
        /// pan and zoom.
        /// </summary>
        public MapToolbarTool[] MapToolbarTools
        {
            get { return mapToolbarTools; }
            set { mapToolbarTools = value; }
        }

        /// <summary>
        /// Read/Write. The map location modes to show in the info window popup.
        /// </summary>
        public MapLocationMode[] MapLocationModes
        {
            get { return mapLocationModes; }
            set { mapLocationModes = value; }
        }

        /// <summary>
        /// Read/Write. The map height.
        /// IMPORTANT - This should only be set where the default map height read from the map config 
        /// file is not acceptable.
        /// </summary>
        public int MapHeight
        {
            get { return mapHeight; }
            set { mapHeight = value; }
        }

        /// <summary>
        /// Read/Write. The map width.
        /// IMPORTANT - This should only be set where the default map width read from the map config 
        /// file is not acceptable.
        /// </summary>
        public int MapWidth
        {
            get { return mapWidth; }
            set { mapWidth = value; }
        }

        #endregion
    }

    /// <summary>
    /// Class containing a location point to show on a map on MapControl2
    /// </summary>
    public class MapLocationPoint
    {
        #region Private members

        private OSGridReference mapLocationOSGR;
        private MapLocationSymbolType mapLocationSymbol;
        private string mapSymbol;
        private string mapLocationDescription;
        private bool infoPopupRequired;
        private bool showPopup;
        private string content;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public MapLocationPoint()
        {
            this.mapLocationOSGR = new OSGridReference();
            this.mapLocationSymbol = MapLocationSymbolType.Circle;
            this.mapLocationDescription = string.Empty;
            this.infoPopupRequired = false;
            this.showPopup = false;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public MapLocationPoint(OSGridReference mapLocationOSGR, MapLocationSymbolType mapLocationSymbol,
            string mapLocationDescription, bool infoPopupRequired, bool showPopup)
        {
            this.mapLocationOSGR = mapLocationOSGR;
            this.mapLocationSymbol = mapLocationSymbol;
            this.mapLocationDescription = mapLocationDescription;
            this.infoPopupRequired = infoPopupRequired;
            this.showPopup = showPopup;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mapSymbol">The symbol name to use, if MapLocationSymbolType is Custom</param>
        public MapLocationPoint(OSGridReference mapLocationOSGR, MapLocationSymbolType mapLocationSymbol,
            string mapSymbol, string mapLocationDescription, bool infoPopupRequired, bool showPopup)
        {
            this.mapLocationOSGR = mapLocationOSGR;
            this.mapLocationSymbol = mapLocationSymbol;
            this.mapSymbol = mapSymbol;
            this.mapLocationDescription = mapLocationDescription;
            this.infoPopupRequired = infoPopupRequired;
            this.showPopup = showPopup;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mapSymbol">The symbol name to use, if MapLocationSymbolType is Custom</param>
        /// <param name="content">The content needs to be shown in information window. 
        /// Content could be simple html content or it can be a reference(client id) of another control on page</param>
        public MapLocationPoint(OSGridReference mapLocationOSGR, MapLocationSymbolType mapLocationSymbol,
            string mapSymbol, string mapLocationDescription, bool infoPopupRequired , bool showPopup, string content)
            : this(mapLocationOSGR, mapLocationSymbol, mapSymbol,mapLocationDescription,infoPopupRequired,showPopup)
        {
            if (!string.IsNullOrEmpty(content))
            {
                this.content= content;
            }
        }
                
        #endregion

        #region Public Properties

        /// <summary>
        /// Read/Write. The location coordinate to add on the map
        /// </summary>
        public OSGridReference MapLocationOSGR
        {
            get { return mapLocationOSGR; }
            set { mapLocationOSGR = value; }
        }

        /// <summary>
        /// Read/Write. The symbol to use for this location
        /// </summary>
        public MapLocationSymbolType MapLocationSymbol
        {
            get { return mapLocationSymbol; }
            set { mapLocationSymbol = value; }
        }

        /// <summary>
        /// Read/Write. The symbol image to use for this location, 
        /// if MapLocationSymbolType is Custom
        /// </summary>
        public string MapSymbol
        {
            get { return mapSymbol; }
            set { mapSymbol = value; }
        }

        /// <summary>
        /// Read/Write. The description to display against the location symbol
        /// </summary>
        public string MapLocationDescription
        {
            get { return mapLocationDescription; }
            set { mapLocationDescription = value; }
        }

        /// <summary>
        /// Read/Write. If this location point can have an info popup. If set to true,
        /// then the info popup is shown when the location point is clicked
        /// </summary>
        public bool MapInfoPopupRequired
        {
            get { return infoPopupRequired; }
            set { infoPopupRequired = value; }
        }

        /// <summary>
        /// Read/Write. If set to true, then the info popup is shown against the location point
        /// on the initial map
        /// </summary>
        public bool MapShowPopup
        {
            get { return showPopup; }
            set { showPopup = value; }

        }

        /// <summary>
        /// Read/Write. The client side id(not server side Id) of the control holding the custom content to show 
        ///  in the information popup or the actual html content needs showing in information popup
        /// </summary>
        public string Content
        {
            get { return content; }
            set { content = value; }
        }

        #endregion
    }

    /// <summary>
    /// Class containing the information needed to display a journey on a map on MapControl2
    /// </summary>
    public class MapJourney
    {
        #region Private members

        private string sessionId;
        private int routeNumber;
        private MapJourneyType mapJourneyType;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public MapJourney()
        {
            this.sessionId = string.Empty;
            this.routeNumber = -1;
            this.mapJourneyType = MapJourneyType.PublicTransport;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public MapJourney(string sessionId, int routeNumber, MapJourneyType mapJourneyType)
        {
            this.sessionId = sessionId;
            this.routeNumber = routeNumber;
            this.mapJourneyType = mapJourneyType;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Read/Write. Session id
        /// </summary>
        public string SessionId
        {
            get { return sessionId; }
            set { sessionId = value; }
        }

        /// <summary>
        /// Read/Write. The journey route number to be shown on the map
        /// </summary>
        public int RouteNumber
        {
            get { return routeNumber; }
            set { routeNumber = value; }
        }

        /// <summary>
        /// Read/Write. The type of journey that is being added
        /// </summary>
        public MapJourneyType MapJourneyType
        {
            get { return mapJourneyType; }
            set { mapJourneyType = value; }
        }

        #endregion
    }

    /// <summary>
    /// Class containing filters for showing travel news incidents on a map on MapControl2
    /// </summary>
    public class MapTravelNewsFilter
    {
        #region Private members

        private MapTransportType mapTransportType;
        private MapIncidentType mapIncidentType;
        private MapSeverity mapSeverity;
        private MapTimePeriod mapTimePeriod;
        private DateTime mapDateTime;
        private bool isValid = false;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public MapTravelNewsFilter()
        {
            this.mapTransportType = MapTransportType.All;
            this.mapIncidentType = MapIncidentType.All;
            this.mapSeverity = MapSeverity.All;
            this.mapTimePeriod = MapTimePeriod.Current;
            this.mapDateTime = DateTime.Now;

            this.isValid = false;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public MapTravelNewsFilter(MapTransportType mapTransportType, MapIncidentType mapIncidentType, 
            MapSeverity mapSeverity, MapTimePeriod mapTimePeriod, DateTime mapDateTime)
        {
            this.mapTransportType= mapTransportType;
            this.mapIncidentType = mapIncidentType;
            this.mapSeverity = mapSeverity;
            this.mapTimePeriod = mapTimePeriod;
            this.mapDateTime = mapDateTime;

            this.isValid = true;
        }
                
        #endregion

        #region Public Properties

        /// <summary>
        /// Read/Write. The Transport types to show for travel news incidents on the map
        /// </summary>
        public MapTransportType MapTransportType
        {
            get { return mapTransportType; }
            set { mapTransportType = value; }
        }

        /// <summary>
        /// Read/Write. The Incident types to show for travel news incidents on the map
        /// </summary>
        public MapIncidentType MapIncidentType
        {
            get { return mapIncidentType; }
            set { mapIncidentType = value; }
        }

        /// <summary>
        /// Read/Write. The Severity of travel news incidents to show on the map
        /// </summary>
        public MapSeverity MapSeverity
        {
            get { return mapSeverity; }
            set { mapSeverity = value; }
        }

        /// <summary>
        /// Read/Write. The Time period type to show for travel news incidents on the map
        /// </summary>
        public MapTimePeriod MapTimePeriod
        {
            get { return mapTimePeriod; }
            set { mapTimePeriod = value; }
        }

        /// <summary>
        /// Read/Write. The Date time to use when showing travel news incidents on the map
        /// </summary>
        public DateTime MapDateTime
        {
            get { return mapDateTime; }
            set { mapDateTime = value; }
        }

        /// <summary>
        /// Read/Write. Flag indicating if the filter is valid
        /// </summary>
        public bool IsValid
        {
            get { return isValid; }
            set { isValid = value; }
        }

        #endregion
    }

    #endregion
}