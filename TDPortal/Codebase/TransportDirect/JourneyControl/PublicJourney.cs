// *********************************************** 
// NAME         : PublicJourney.cs
// AUTHOR       : Andrew Toner
// DATE CREATED : 10/08/2003 
// DESCRIPTION  : Implementation of the PublicJourney class
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyControl/PublicJourney.cs-arc  $
//
//   Rev 1.9   Dec 05 2012 14:16:28   mmodi
//Updated for accessible journeys
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.8   Feb 12 2010 11:13:26   apatel
//International Planner code changes
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.7   Feb 18 2009 18:13:38   mmodi
//Update fare routecodes
//Resolution for 5210: CCN487 - ZPBO Implementation workstream
//
//   Rev 1.6   Feb 11 2009 17:10:10   mmodi
//Updated to Routing Guide Section logic to take account of the fare route codes returned in the Pricing Unit
//Resolution for 5241: Routeing Guide - Manchester to KGX via Leeds shows no fares
//
//   Rev 1.5   Feb 09 2009 15:17:26   mmodi
//Updated code logic for Routing Guide compliant journey following modifiy journey changes
//Resolution for 5223: CCN0385 - TTBO Routeing Guide
//
//   Rev 1.4   Feb 02 2009 16:35:04   mmodi
//Populate the Routing Guide sections returned by the CJP for the public journey
//Resolution for 5223: CCN0385 - TTBO Routeing Guide
//
//   Rev 1.3   Jun 18 2008 16:03:44   dgath
//GetJourneyModeType updated to fix ITP journeys issue.
//Resolution for 5025: ITP: Workstream
//
//   Rev 1.2   Mar 10 2008 15:17:54   mturner
//Initial Del10 Codebase from Dev Factory
//

// DAN : added journey Duration property
//       added journeyModeType property

//   Rev 1.0   Nov 08 2007 12:23:54   mturner
//Initial revision.
//
//   Rev 1.27   Mar 21 2007 14:59:32   tmollart
//Changes so that IsSameJourney works correctly.
//
//   Rev 1.26   Mar 30 2006 13:45:28   mguney
//Manual merge for stream0018
//Resolution for 18: DEL 8.1 Workstream - Integrated Air Planner
//
//   Rev 1.25   Mar 14 2006 08:41:36   build
//Automatically merged from branch for stream3353
//
//   Rev 1.24.1.2   Mar 10 2006 19:00:22   rhopkins
//Removal of JourneyDetail class and general code review tidy.
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.24.1.1   Feb 17 2006 14:11:46   NMoorhouse
//Updates (by Richard Hopkins) to support Replan Maps
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.24.1.0   Jan 26 2006 20:09:08   rhopkins
//Added property JourneyLegs to return details array as an array of JourneyLegs
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.24   Aug 24 2005 16:04:34   RPhilpott
//Changes to allow OSGR of first or last location to be replaced by OSGR of request origin/destination in rare case where firs tor last naptan in journey has no supplied OSGR and is not in Stops database.
//Resolution for 2662: DN062:  Map with non-consecutive nodes with no co-ordinates functioning incorrectly
//
//   Rev 1.23   Aug 19 2005 14:04:14   jgeorge
//Automatically merged from branch for stream2572
//
//   Rev 1.22.1.1   Aug 04 2005 14:54:04   rgreenwood
//DD073 Map Details: Updated constructor checks of HasInvalidCoordinates in order to conditionally display new map key for greyed out legs
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.22.1.0   Jul 25 2005 15:55:18   rgreenwood
//DD073 Map Details: Added HasInvalidCoordinates property, set in the constructor
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.22   Apr 26 2005 10:12:30   pcross
//IR2192. Changed get intermediate nodes method to get journey nodes method. This allows extended journey logic to be added.
//
//   Rev 1.21   Apr 22 2005 16:25:36   pcross
//IR2192. Public journeys can have intermediate nodes shown on a map. This class now has an additional method to get the OSGRs of the intermediate nodes of a journey to facilitate that.
//Resolution for 2192: Display normal map page, add numbered start and end legs
//
//   Rev 1.20   Apr 08 2005 12:36:14   jmorrissey
//Added UpgradesAvailable property
//
//   Rev 1.19   Mar 30 2005 18:48:36   RPhilpott
//Don't need separate adult/child minimum fare indications.
//
//   Rev 1.18   Mar 30 2005 18:41:56   RPhilpott
//Add flags to indicate when minimum fare applies to this journey (used by cost-based search only).
//
//   Rev 1.17   Feb 23 2005 15:07:48   rscott
//DEL 7 Update - New Properties Added
//
//   Rev 1.16   Jan 19 2005 14:45:22   RScott
//DEL 7 - PublicViaLocation removed and PublicViaLocations[ ], PublicSoftViaLocations[ ], PublicNotViaLocations[] added.
//
//   Rev 1.15   Aug 20 2004 12:15:44   jgeorge
//IR1354
//
//   Rev 1.14   Jun 25 2004 09:36:34   RPhilpott
//Integrate Check-in, Check-out and transfer details into main Air flight leg.
//
//   Rev 1.13   Jun 08 2004 14:39:40   RPhilpott
//Add method to return list of modes used by the journey.
//
//   Rev 1.12   Feb 19 2004 17:05:34   COwczarek
//Refactored PublicJourneyDetail into new class hierarchy representing different journey leg types (timed, continuous and frequency based)
//Resolution for 629: Frequency based Journeys
//
//   Rev 1.11   Nov 12 2003 15:04:56   geaton
//Updated member var to be private since it already has a public property.
//
//   Rev 1.10   Nov 11 2003 19:10:44   geaton
//Added set on properties for use by transaction injector component to generate XML data file template for pricing transaction.
//
//   Rev 1.9   Oct 15 2003 21:51:54   acaunt
//fares attribute added
//
//   Rev 1.8   Sep 26 2003 11:47:12   geaton
//Added default constructor to allow XML serialisation of class for use by Event Logging Service publishers.
//
//   Rev 1.7   Sep 11 2003 16:34:10   jcotton
//Made Class Serializable
//
//   Rev 1.6   Sep 05 2003 15:28:48   passuied
//Deletion of TDLocation, LocationCodingType, OSGridReference and transfer to LocationService
//
//   Rev 1.5   Sep 01 2003 16:28:40   jcotton
//Updated: RouteNum
//
//   Rev 1.4   Aug 27 2003 09:26:20   kcheung
//Updated constructor to take TDJourneyType
//
//   Rev 1.3   Aug 26 2003 17:08:46   PNorell
//Added support for journey index.
//
//   Rev 1.2   Aug 20 2003 17:55:50   AToner
//Work in progress
using System;
using System.Collections;
using TransportDirect.Common;
using TransportDirect.UserPortal.LocationService;
using CJP = TransportDirect.JourneyPlanning.CJPInterface;

namespace TransportDirect.UserPortal.JourneyControl
{
    /// <summary>
    /// Summary description for PublicJourney.
    /// </summary>
    [Serializable()]
    public class PublicJourney : Journey
    {
        #region Private variables
        private PublicJourneyDetail[] journeyDetails;
        private CJP.PricingUnit[] fares;
		private string[] routingPointNaptans;
        private TDDateTime journeyDate;
		private bool minimumFareApplies;
		private bool upgradesAvailable;
		private bool hasInvalidLegCoordinates;
        private TimeSpan duration = new TimeSpan(0);
        private CJP.ModeType journeyModeType;
        private bool accessibleJourney = false;

        private bool routingGuideCompliantJourney = false;
        private RoutingGuideSection[] routingGuideSections = new RoutingGuideSection[0];
        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor - defined to allow XML serialisation.
        /// </summary>
        public PublicJourney()
        { 
        }

        /// <summary>
        /// Takes all parameters and creates suitable PublicJourney from it
        /// </summary>
        /// <param name="journeyIndex">The journey index to be used</param>
        /// <param name="details">The journey details</param>
        /// <param name="type">The journey type</param>
        public PublicJourney( int index, PublicJourneyDetail[] details, TDJourneyType type, int routenum) 
            : base(index)
        {
            journeyDetails = details;
            journeyType = type;
            routeNum = routenum;

			foreach (PublicJourneyDetail pjd in details)
			{
				if (pjd.HasInvalidCoordinates)
				{
					hasInvalidLegCoordinates = true;
					break;
				}
			}
        }

        /// <summary>
        /// Takes all parameters and creates suitable PublicJourney from it including journey date
        /// </summary>
        /// <param name="journeyIndex">The journey index to be used</param>
        /// <param name="details">The journey details</param>
        /// <param name="type">The journey type</param>
        /// <param name="journeydate">The journey date</param>
        public PublicJourney(int index, PublicJourneyDetail[] details, TDJourneyType type, int routenum, TDDateTime journeydate)
            : base(index)
        {
            journeyDetails = details;
            journeyType = type;
            routeNum = routenum;
            journeyDate = journeydate;

            foreach (PublicJourneyDetail pjd in details)
            {
                if (pjd.HasInvalidCoordinates)
                {
                    hasInvalidLegCoordinates = true;
                    break;
                }

            }
        }

        /// <summary>
        /// Takes a CJP PublicJourney and populates the details array with legs of that journey
        /// </summary>
        /// <param name="cjpPublicJourney">CJP PublicJourney</param>
        /// <param name="publicVia">A location to pass to the detail class for the IsViaLocation property</param>
		public PublicJourney( int index, CJP.PublicJourney cjpPublicJourney,
			TDLocation publicVia, TDLocation requestOrigin, TDLocation requestDestination, 
            TDJourneyType type, bool accessible, int routenum ) 
            : base(index)
        {
            journeyType = type;
            routeNum = routenum;
            fares = cjpPublicJourney.fares;
			routingPointNaptans = cjpPublicJourney.routingStops;

			// if journeyDate not set populate journeyDate from the first non-walk leg
			foreach (CJP.Leg leg in cjpPublicJourney.legs)
			{
				if	(leg.mode != CJP.ModeType.Walk)
				{
					journeyDate = leg.board.departTime;
					journeyDate.Hour = 0;
					journeyDate.Minute = 0;
					journeyDate.Second = 0;
					journeyDate.Millisecond = 0;
					break;
				}
            }

            #region Setup journey legs, i.e. PublicJourneyDetails
            int skippedLegCount = 0;
            foreach (CJP.Leg leg in cjpPublicJourney.legs)
			{
                if (leg.mode == CJP.ModeType.CheckIn || leg.mode == CJP.ModeType.CheckOut || leg.mode == CJP.ModeType.Transfer)
				{
					skippedLegCount++;
				}
			}

            journeyDetails = new PublicJourneyDetail[cjpPublicJourney.legs.Length - skippedLegCount];

			int currentLeg = 0;

            // Used to update routing guide compliant legs in the pricing unit. 
            // Hold original leg index and new leg index mapping.
            Hashtable hashLegIndexes = new Hashtable(); 

			for (int legIndex = 0; legIndex < cjpPublicJourney.legs.Length; legIndex++)
			{
                CJP.Leg leg = cjpPublicJourney.legs[legIndex];
				
				switch (leg.mode)
				{
                    case CJP.ModeType.CheckIn:
					{
						break;
					}

                    case CJP.ModeType.CheckOut:
					{
						break;
					}

                    case CJP.ModeType.Transfer:
					{
						break;
					}

					default:
					{
                        if (leg.mode == CJP.ModeType.Air)
						{
							// There should always be a CheckIn/Transfer leg before this and a
							// CheckOut/Transfer leg after it.
                            CJP.Leg before = null, after = null;
							
							if (legIndex != 0)
							{
								before = cjpPublicJourney.legs[legIndex - 1];
							}

							if (legIndex < (cjpPublicJourney.legs.Length - 1))
							{
								after = cjpPublicJourney.legs[legIndex + 1];
							}

							journeyDetails[currentLeg] = PublicJourneyDetail.Create(leg, publicVia, before, after);
						}
						else
						{
							journeyDetails[currentLeg] = PublicJourneyDetail.Create(leg, publicVia);
						}

                        // Add only those leg indexes we want to know about
                        hashLegIndexes.Add(legIndex, currentLeg);

						currentLeg++;
						break;
					}
				}
            }
            #endregion

            #region Check and update grid references
            if	(!journeyDetails[0].LegStart.Location.GridReference.IsValid && requestOrigin != null)
			{
				journeyDetails[0].LegStart.Location.GridReference = requestOrigin.GridReference;
			}

			if	(!journeyDetails[journeyDetails.Length - 1].LegEnd.Location.GridReference.IsValid && requestDestination != null)
			{
				journeyDetails[journeyDetails.Length - 1].LegEnd.Location.GridReference = requestDestination.GridReference;
			}

			foreach (PublicJourneyDetail pjd in journeyDetails)
			{
				if (pjd.HasInvalidCoordinates)
				{
					hasInvalidLegCoordinates = true;
					break;
				}
            }
            #endregion

            #region Setup Routing guide sections
            // Is this public journey routing guide compliant, assume by default it is 
            // (because only journeys which involve Rail legs will ever have a Pricing Unit setup)
            if ((fares != null) && (fares.Length > 0))
            {
                // Set up the array
                ArrayList routingGuideArray = new ArrayList();
                int rgsIndex = 0;

                foreach (CJP.PricingUnit pu in fares)
                {
                    ArrayList rgCompliantLegs = new ArrayList();

                    // A PU can span a number of legs, e.g. for a Train->Underground->Train journey, the PU
                    // would only have legs 0,2 indicating the whole journey is RG compliant.
                    // Where we see a PU like this, then we upate the TD rgCompliantLegs to include all the 
                    // "inbetween" legs, i.e. 0,1,2, therefore giving us a complete RoutingGuideSection.
                    pu.legs = TidyUpPricingUnitLegs(pu);

                    // TTBO currently is (in some scenarios )unable to return the Any Permitted route code when 
                    // a journey involves a zonal fare. This prevents any "travelcard" type fares being displayed.
                    // Workaround is to include this route code manually.
                    #warning Routing Guide - Any permitted fare route work around
                    pu.fareCodes = TidyUpPricingUnitFareRouteCodes(pu, publicVia);

                    foreach (int leg in pu.legs)
                    {
                        // Map the PU leg index to the new TD PublicJourneyDetail leg index
                        if (hashLegIndexes.Contains(leg))
                        {
                            int newLegIndex = (int)hashLegIndexes[leg];
                            rgCompliantLegs.Add(newLegIndex);

                            // Set the PublicJourneyDetail flag to be routing guide compliant
                            journeyDetails[newLegIndex].RoutingGuideCompliant = pu.routingGuideCompliant;
                            journeyDetails[newLegIndex].RoutingGuideSectionIndex = rgsIndex;
                            journeyDetails[newLegIndex].FareRouteCodes = pu.fareCodes;
                        }
                    }


                    int[] rgCompliantLegsArray = (int[])rgCompliantLegs.ToArray(typeof(int));

                    // Only create the RGS if the PU has fare route codes - this indicates the PU can be fared
                    if ((pu.fareCodes != null) && (pu.fareCodes.Length > 0))
                    {
                        // Add a new Routing Guide section to the array
                        RoutingGuideSection rgs = new RoutingGuideSection(
                            rgsIndex,
                            rgCompliantLegsArray,
                            pu.routingGuideCompliant);

                        routingGuideArray.Add(rgs);
                        rgsIndex++;
                    }
                    else
                    {
                        // No fare route codes apply to this PU, therefore need to create a seperate 
                        // RGS for each rail leg
                        foreach (int leg in rgCompliantLegsArray)
                        {
                            if (IsRailMode(journeyDetails[leg].Mode))
                            {
                                RoutingGuideSection rgs = new RoutingGuideSection(
                                    rgsIndex,
                                    new int[1] { leg },
                                    false);

                                routingGuideArray.Add(rgs);
                                rgsIndex++;
                            }
                        }
                    }
                }

                routingGuideSections = (RoutingGuideSection[])routingGuideArray.ToArray(typeof(RoutingGuideSection));
            }

            routingGuideCompliantJourney = IsJourneyRoutingGuideCompliant();
            #endregion

            #region Set accessible journey flag

            accessibleJourney = accessible;

            #endregion
        }

        #endregion

        #region Public properties
        /// <summary>
		/// Returns an array of JourneyLegs that represents all of the legs of this journey.
		/// For a public journey this is the same as the Details.
		/// </summary>
		public override JourneyLeg[] JourneyLegs
		{
			get { return journeyDetails; }
		}

		/// <summary>
        /// Gets and sets the journey details.
        /// </summary>
        /// <remarks>Setter is provided for use by Transaction Injector component.</remarks>
        public PublicJourneyDetail [] Details
        {
            get {return journeyDetails;}
            set {journeyDetails = value;}
        }

		/// <summary>
		/// Gets and sets the fares pricing units.
		/// </summary>
		/// <remarks>Setter is provided for use by Transaction Injector component.</remarks>
        public CJP.PricingUnit[] Fares
		{
			get {return fares;}
			set {fares = value;}
		}

		/// <summary>
		/// Gets and sets the routing point naptans.
		/// </summary>
		/// <remarks></remarks>
		public string[] RoutingPointNaptans
		{
			get {return routingPointNaptans;}
			set {routingPointNaptans = value;}
		}


        /// <summary>
        /// Read/write. By default, this value is false.
        /// The value will only be true where the journey contains RoutingGuideSections and all of 
        /// them are compliant.
        /// </summary>
        public bool RoutingGuideCompliantJourney
        {
            get { return routingGuideCompliantJourney; }
            set { routingGuideCompliantJourney = value; }
        }

        /// <summary>
        /// Read/write. An array of RoutingGuideSections for this public journey
        /// </summary>
        public RoutingGuideSection[] RoutingGuideSections
        {
            get { return routingGuideSections; }
            set { routingGuideSections = value; }
        }

		/// <summary>
		/// Gets and sets the JourneyDate.
		/// </summary>
		/// <remarks></remarks>
		public TDDateTime JourneyDate
		{
			get {return journeyDate;}
			set {journeyDate = value;}
		}
	
		/// <summary>
		/// Gets and sets the MinimumFareApplies flag.
		/// </summary>
		/// <remarks></remarks>
		public bool MinimumFareApplies
		{
			get {return minimumFareApplies;}
			set {minimumFareApplies = value;}
		}

		/// <summary>
		/// Gets and sets the UpgradesAvailable flag.
		/// </summary>
		/// <remarks></remarks>
		public bool UpgradesAvailable
		{
			get {return upgradesAvailable;}
			set {upgradesAvailable = value;}
		}

		public bool HasInvalidLegCoordinates
		{
			get
			{
				return hasInvalidLegCoordinates;
			}
		}

        /// <summary>
        /// Read write property. Returns the duration of the journey, using the First detail leg and Last detail leg 
        /// of the journey
        /// Set method should only be used when duration calculated before setting up public journey
        /// </summary>
        public TimeSpan Duration
        {
            get
            {
                CalculateDuration();

                return duration;
            }
            set
            {
                // Added for TD Extra CCN
                // Should only be used where duration gets calculated before setting public journey
                duration = value;
            }
        }

        /// <summary>
        /// Read/Write. Indicates if this is an accessible journey
        /// </summary>
        public bool AccessibleJourney
        {
            get { return accessibleJourney; }
            set { accessibleJourney = value; }
        }

        #endregion

        #region Public methods
        /// <summary>
		/// Returns list of modes used in this journey (get only).
		/// </summary>
        public override CJP.ModeType[] GetUsedModes()
		{
			ArrayList modes = new ArrayList(10);

			foreach (PublicJourneyDetail detail in journeyDetails)
			{
				if (!modes.Contains(detail.Mode))
				{
					modes.Add(detail.Mode);
				}
			}

            return ((CJP.ModeType[])modes.ToArray(typeof(CJP.ModeType)));
		}

        /// <summary>
        /// Returns the ModeType of the journey, classifying it as a Rail, Coach, or Air
        /// </summary>
        /// <returns></returns>
        public CJP.ModeType GetJourneyModeType()
        {
            ArrayList journeyModes = new ArrayList();
            journeyModes.AddRange(this.GetUsedModes());

            bool coachmode = ((journeyModes.Contains(CJP.ModeType.Coach)) || (journeyModes.Contains(CJP.ModeType.Bus)));
            bool trainmode = ((journeyModes.Contains(CJP.ModeType.Rail))
                       ||
                     (journeyModes.Contains(CJP.ModeType.RailReplacementBus))
                       ||
                     (journeyModes.Contains(CJP.ModeType.Metro))
                       ||
                     (journeyModes.Contains(CJP.ModeType.Tram))
                       ||
                     (journeyModes.Contains(CJP.ModeType.Underground)));

            if (((journeyModes.Contains(CJP.ModeType.Coach)) || (journeyModes.Contains(CJP.ModeType.Bus))) && !journeyModes.Contains(CJP.ModeType.Air))
                journeyModeType = CJP.ModeType.Coach;

            else if (((journeyModes.Contains(CJP.ModeType.Rail))
                       ||
                     (journeyModes.Contains(CJP.ModeType.RailReplacementBus))
                       ||
                     (journeyModes.Contains(CJP.ModeType.Metro))
                       ||
                     (journeyModes.Contains(CJP.ModeType.Tram))
                       ||
                     (journeyModes.Contains(CJP.ModeType.Underground))) && !journeyModes.Contains(CJP.ModeType.Air))
                journeyModeType = CJP.ModeType.Rail;
            else if (journeyModes.Contains(CJP.ModeType.Air)
                   && !coachmode && !trainmode)
            {
                journeyModeType = CJP.ModeType.Air;
            }

            return journeyModeType;
        }

		/// <summary>
		/// Returns a grid reference object for each node of the journey including start and finish.
		/// This can then be used to find the northings and eastings and other info.
		/// </summary>
		/// <returns></returns>
		public new OSGridReference[] GetJourneyNodesGridReferences()
		{
			// Enumerate the journey details that make up this journey to return
			// in an array of grid references
			ArrayList journeyNodesGridReferences = new ArrayList(journeyDetails.Length);

			// Get the start location for each leg and then get the end location for the final leg
			// to result in a location for each point of change
			int i = 0;
			foreach (PublicJourneyDetail detail in journeyDetails)
			{
				// Logic to get all nodes is to get all start locations for each node then for the last node,
				// get the end location too.
				journeyNodesGridReferences.Add(detail.LegStart.Location.GridReference);

				i++;
				if(i==journeyDetails.Length)
				{
					// get end location for last leg
					journeyNodesGridReferences.Add(detail.LegEnd.Location.GridReference);
				}
			}
			return (OSGridReference[])journeyNodesGridReferences.ToArray(typeof(OSGridReference));
		}

        /// <summary>
        /// Returns the applicable RoutingGuideSection for the specified 
        /// PublicJourneyDetails index for this journey. 
        /// If not found, then an empty RoutingGuideSection is returned.
        /// </summary>
        /// <returns></returns>
        public RoutingGuideSection GetRoutingGuideSection(int index)
        {
            if ((index >= 0) && (index < journeyDetails.Length) && (routingGuideSections.Length > 0))
            {
                foreach (RoutingGuideSection rgs in routingGuideSections)
                {
                    if (rgs.Contains(index))
                        return rgs;
                }
            }

            return new RoutingGuideSection();
        }

		/// <summary>
		/// Compares the details of the current object (legs, modes, 
		///  start and end locations and times) with the passed object
		///  to determine if the two objects represent the same journey.
		/// </summary>
		/// <param name="otherJourney">A second PublicJourney object</param>
		/// <returns>True if the objects represent the same journey, false otherwise</returns>
		public bool IsSameJourney(PublicJourney otherJourney)
		{
			if	(this.Details.Length != otherJourney.Details.Length)
			{
				return false;
			}

			for (int i = 0; i < this.Details.Length; i++)
			{
				if	(this.Details[i].Mode != otherJourney.Details[i].Mode)
				{
					return false;
				}
			}

			// in a journey result we can safely assume that the start
			//  and end TDLocation's have no more than one naptan, but
			//  we defensively check that we do have at least one ...

			if	(this.Details[0].LegStart.Location.NaPTANs.Length 
				!= otherJourney.Details[0].LegStart.Location.NaPTANs.Length)
			{
				return false;
			}

			if	(this.Details[0].LegStart.Location.NaPTANs.Length > 0) 
			{
				if	(this.Details[0].LegStart.Location.NaPTANs[0].Naptan 
					!= otherJourney.Details[0].LegStart.Location.NaPTANs[0].Naptan)
				{
					return false;
				}
			}

			if	(!this.Details[0].LegStart.DepartureDateTime.Equals(otherJourney.Details[0].LegStart.DepartureDateTime))
			{
				return false;
			}

			if	(this.Details[this.Details.Length - 1].LegEnd.Location.NaPTANs.Length 
				!= otherJourney.Details[otherJourney.Details.Length - 1].LegEnd.Location.NaPTANs.Length)
			{
				return false;
			}

			if	(this.Details[this.Details.Length - 1].LegEnd.Location.NaPTANs.Length > 0) 
			{
				if	(this.Details[this.Details.Length - 1].LegEnd.Location.NaPTANs[0].Naptan 
					!= otherJourney.Details[otherJourney.Details.Length - 1].LegEnd.Location.NaPTANs[0].Naptan)
				{
					return false;
				}
			}

			//if	(this.Details[this.Details.Length - 1].LegEnd.ArrivalDateTime
			//	!= otherJourney.Details[otherJourney.Details.Length - 1].LegEnd.ArrivalDateTime)
			if (!this.Details[this.Details.Length - 1].LegEnd.ArrivalDateTime.Equals(otherJourney.Details[otherJourney.Details.Length - 1].LegEnd.ArrivalDateTime))
			{
				return false;
			}

			return true;

        }

        /// <summary>
        /// Determines if this journey contains only one rail leg.
        /// (Rail leg is of mode type Rail or RailReplacementBus)
        /// </summary>
        /// <returns>True if journey has only one rail leg</returns>
        public bool HasOnlyOneRailLeg()
        {
            int railLegCount = 0;

            if (this.journeyDetails != null)
            {
                foreach (PublicJourneyDetail pjd in this.journeyDetails)
                {
                    if (IsRailMode(pjd.Mode))
                    {
                        railLegCount++;
                    }
                }
            }

            return (railLegCount == 1);
        }

        #endregion

        #region Private methods
        /// <summary>
        /// Calculates the journey duration
        /// </summary>
        private void CalculateDuration()
        {
            if (duration != new TimeSpan(0))
                return;

            TDDateTime departureTime = null;
            TDDateTime arrivalTime = null;

            // Get the depart and arrival times for this journey
            // Use CheckInTime or ExitTime over DepartDateTime and ArriveDateTime
            // respectively.
            if (this.Details[0].CheckInTime != null)
                departureTime = this.Details[0].CheckInTime;
            else
                departureTime = this.Details[0].LegStart.DepartureDateTime;

            if (this.Details[this.Details.Length - 1].ExitTime != null)
                arrivalTime = this.Details[this.Details.Length - 1].ExitTime;
            else
                arrivalTime = this.Details[this.Details.Length - 1].LegEnd.ArrivalDateTime;
            
            duration = new TimeSpan(0);

            DateTime dateTimeArrivalTime = new DateTime
                (arrivalTime.Year, arrivalTime.Month, arrivalTime.Day, arrivalTime.Hour, arrivalTime.Minute, arrivalTime.Second, arrivalTime.Millisecond);

            DateTime dateTimeDepartureTime = new DateTime
                (departureTime.Year, departureTime.Month, departureTime.Day, departureTime.Hour, departureTime.Minute, departureTime.Second, departureTime.Millisecond);

            // find the difference between the two times
            duration = dateTimeArrivalTime.Subtract(dateTimeDepartureTime);
        }

        /// <summary>
        /// Takes a PricingUnit and loops through the Legs array. Where there are gaps in the 
        /// Legs array, they will be gap filled. E.g. Legs 0,2 will return Leg 0,1,2
        /// </summary>
        /// <param name="pricingUnit">PricingUnit containing a legs int array</param>
        /// <returns>Int array</returns>
        private int[] TidyUpPricingUnitLegs(CJP.PricingUnit pricingUnit)
        {
            ArrayList newPULegs = new ArrayList();

            if ((pricingUnit.legs != null) && pricingUnit.legs.Length > 0)
            {
                // Set the starting leg index to first one in the array
                int previousLegIndex = pricingUnit.legs[0];

                for (int i = 0; i < pricingUnit.legs.Length; i++)
                {
                    int currentLegIndex = pricingUnit.legs[i];

                    // Check if the previous and current index are consecutive.
                    // If theres a gap in the index numbers, we have situation like 0,2
                    // therefore fill the gap with the missing leg indexes
                    while ((previousLegIndex + 1) < currentLegIndex)
                    {
                        previousLegIndex++;

                        newPULegs.Add(previousLegIndex);
                    }

                    // Add the current to the new legs index
                    newPULegs.Add(currentLegIndex);

                    // Ready for the next loop round
                    previousLegIndex = currentLegIndex;
                }
            }

            return (int[])newPULegs.ToArray(typeof(int));
        }


        /// <summary>
        /// Takes a PricingUnit and where it doesn't contain a fare route code "00000", this is inserted.
        /// This is only done when there is no via location for the journey.
        /// </summary>
        /// <param name="pricingUnit"></param>
        /// <param name="publicVia"></param>
        /// <returns></returns>
        private string[] TidyUpPricingUnitFareRouteCodes(CJP.PricingUnit pricingUnit, TDLocation publicVia)
        {
            ArrayList newFareCodes = new ArrayList();

            if ((pricingUnit.fareCodes != null) && (pricingUnit.fareCodes.Length > 0))
            {
                newFareCodes.AddRange(pricingUnit.fareCodes);

                // Only add the Any permitted route for journeys which don't have a via location
                if (((publicVia == null) || (publicVia.Status == TDLocationStatus.Unspecified)) && (!newFareCodes.Contains("00000")))
                {
                    newFareCodes.Add("00000");
                }
            }

            return (string[])newFareCodes.ToArray(typeof(string));
        }

        /// <summary>
        /// Returns true if this whole public journey is Routing guide compliant.
        /// Will only return true if there are legs which have a mode of Rail/RailReplacementBus
        /// and there are valid RoutingGuideSections
        /// </summary>
        /// <returns></returns>
        private bool IsJourneyRoutingGuideCompliant()
        {
            bool rgCompliantJourney = false;

            // Routing guide sections exist for this journey. 
            // They should only have been created if there were Rail legs
            if ((this.routingGuideSections != null) && (this.routingGuideSections.Length > 0))
            {
                // Assume journey is RG compliant until proved otherwise
                rgCompliantJourney = true;

                // There are three tests which dictate if the overall journey can be RG compliant
                // 1) Are there any RG sections with a compliant of false
                // 2) Are there multiple RG sections. This indicates we cannot fare the whole journey with one fare,
                // so not compliant
                // 3) But if there are multiple RG sections which cover a Rail->walk/underground/walk->Rail scenario,
                // then it is still compliant. (CJP returns two RG sections to cover only the Rail leg on either side.)

                foreach (RoutingGuideSection rgs in this.routingGuideSections)
                {
                    // Test 1)
                    if (!rgs.Compliant)
                    {
                        rgCompliantJourney = false;
                    }
                }

                // Test 2)
                if ((rgCompliantJourney) && (this.routingGuideSections.Length > 1))
                {
                    // Count to keep track of the RGS sections looked at and deemed ok to merge
                    int numberOfRoutingGuideSectionsMerged = 0;

                    // Test 3)
                    for (int i = 0; i < journeyDetails.Length; i++)
                    {
                        CJP.ModeType legMode = journeyDetails[i].Mode;
                        
                        if (IsRailMode(legMode))
                        {
                            // By default the leg's RGS cannot be combined with another RGS
                            bool rgsForThisLegIsMergedWithASubsequentRGS = false;
                            int rgsToMergeId = -1;

                            // This is a rail leg, check the next few legs for the test 3) scenario
                            for (int k = (i + 1); k < journeyDetails.Length; k++)
                            {
                                CJP.ModeType innerLegMode = journeyDetails[k].Mode;
                                // if the immediate next leg to the one we're checking is rail, then scenario not met
                                if ((IsRailMode(innerLegMode)) && (k == (i + 1)))
                                {
                                    break;
                                }
                                else if (IsRailMode(innerLegMode))
                                {
                                    // Keep the RGS Id as it may be different to the leg's RGS Id being compared
                                    rgsForThisLegIsMergedWithASubsequentRGS = true;
                                    rgsToMergeId = this.GetRoutingGuideSection(k).Id;
                                    break;
                                }

                                // Anything other than underground or walk, then the RGS for this leg not merged with another
                                if (!(innerLegMode == CJP.ModeType.Underground || innerLegMode == CJP.ModeType.Walk))
                                {
                                    break;
                                }
                            }

                            // Update RGS merge count
                            if (rgsForThisLegIsMergedWithASubsequentRGS)
                            {
                                // Increase the merge count as we've accounted for this RGS
                                numberOfRoutingGuideSectionsMerged++;

                                // And also if the RGS it can be merged with is different
                                if ((rgsToMergeId >= 0) && (this.GetRoutingGuideSection(i).Id != rgsToMergeId))
                                {
                                    numberOfRoutingGuideSectionsMerged++;
                                }
                            }

                        }
                    }

                    // Finally, if all RGS haven't been accounted for (i.e. merge-ability), then
                    // there are some standalone RGS, and therefore test 2) is satisfied
                    if (this.routingGuideSections.Length != numberOfRoutingGuideSectionsMerged)
                    {
                        rgCompliantJourney = false;
                    }
                }
            }

            return rgCompliantJourney;
        }

        /// <summary>
        /// Returns true if the supplied travel mode is regarded as a rail mode, in respect to pricing
        /// </summary>
        /// <param name="mode">Mode of journey leg</param>
        /// <returns>True if the supplied mode is regarded as a rail mode, false otherwise</returns>
        private bool IsRailMode(CJP.ModeType mode)
        {
            return mode == CJP.ModeType.Rail || mode == CJP.ModeType.RailReplacementBus;
        }

        #endregion

    }
}
