// *********************************************** 
// NAME			: Journey.cs
// AUTHOR		: Andrew Toner
// DATE CREATED	: 10/08/2003 
// DESCRIPTION	: Implementation of the Journey class
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyControl/Journey.cs-arc  $
//
//   Rev 1.1   Feb 19 2010 12:07:14   mmodi
//New property for emissions
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.0   Nov 08 2007 12:23:44   mturner
//Initial revision.
//
//   Rev 1.12   Mar 22 2006 16:24:26   rhopkins
//Minor FxCop fixes
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.11   Mar 14 2006 08:41:36   build
//Automatically merged from branch for stream3353
//
//   Rev 1.10.1.2   Mar 10 2006 18:55:26   rhopkins
//Removal of JourneyDetail class and general code review tidy.
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.10.1.1   Feb 17 2006 14:11:36   NMoorhouse
//Updates (by Richard Hopkins) to support Replan Maps
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.10.1.0   Jan 26 2006 20:02:04   rhopkins
//Additional property to return JourneyLegs
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.10   Feb 25 2005 10:29:12   rscott
//JourneyIndex property changed from ReadOnly to Read/Write
//
//   Rev 1.9   Jun 08 2004 14:39:40   RPhilpott
//Add method to return list of modes used by the journey.
//
//   Rev 1.8   Sep 26 2003 11:47:18   geaton
//Added default constructor to allow XML serialisation of class for use by Event Logging Service publishers.
//
//   Rev 1.7   Sep 11 2003 16:34:08   jcotton
//Made Class Serializable
//
//   Rev 1.6   Sep 08 2003 17:45:16   RPhilpott
//Class should be abstract.
//
//   Rev 1.5   Sep 01 2003 16:28:38   jcotton
//Updated: RouteNum
//
//   Rev 1.4   Aug 28 2003 16:24:44   jcotton
//Added read only property: RouteNum (int)
//
//   Rev 1.3   Aug 26 2003 17:08:54   kcheung
//Updated constructor
//
//   Rev 1.2   Aug 20 2003 17:55:46   AToner
//Work in progress
//

using System;
using System.Collections;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.UserPortal.LocationService;

namespace TransportDirect.UserPortal.JourneyControl
{
	/// <summary>
	/// Abstract base class encapsulating journey 
	/// data common to all types of journey.
	/// </summary>
	[Serializable()]
	public abstract class Journey
	{
		protected int index;
		protected int routeNum;
		protected TDJourneyType journeyType;
        protected double emissions = -1;

		#region Constructors

		protected Journey()
		{}

		protected Journey(int index)
		{
			this.index = index;
		}

		#endregion


		#region Abstract Properties

		public abstract JourneyLeg[] JourneyLegs
		{
			get;
		}

		public TDJourneyType Type
		{
			get { return journeyType; }
		}

		#endregion


		#region Concrete Properties

        /// <summary>
        /// Read/Write.
        /// </summary>
		public int RouteNum
		{
			get { return routeNum; }
			set { routeNum = value; }
		}

        /// <summary>
        /// Read/Write.
        /// </summary>
		public int JourneyIndex
		{
			get { return index; }
			set { index = value; }
		}

        /// <summary>
        /// Read/Write. The emissions (in Kg) for the journey, default is -1. 
        /// User is required to calculate and update the value if to be used.
        /// </summary>
        public double Emissions
        {
            get { return emissions; }
            set { emissions = value; }
        }

		#endregion


		#region Abstract Methods

		public abstract ModeType[] GetUsedModes();

		#endregion


		#region Concrete Methods

		/// <summary>
		/// Returns a grid reference object for each node of the journey including start and finish.
		/// This can then be used to find the northings and eastings and other info.
		/// </summary>
		/// <returns></returns>
		public OSGridReference[] GetJourneyNodesGridReferences()
		{
			JourneyLeg[] journeyLegs = JourneyLegs;

			// Enumerate the journey details that make up this journey to return
			// an array of grid references
			ArrayList journeyNodesGridReferences = new ArrayList(journeyLegs.Length + 1);

			// Get the start location for each leg and then get the end location for the final leg
			// to result in a location for each point of change
			int i = 0;
			foreach (JourneyLeg leg in journeyLegs)
			{
				// Logic to get all nodes is to get all start locations for each node then for the last node,
				// get the end location too.
				journeyNodesGridReferences.Add(leg.LegStart.Location.GridReference);

				i++;
				if(i==journeyLegs.Length)
				{
					// get end location for last leg
					journeyNodesGridReferences.Add(leg.LegEnd.Location.GridReference);
				}
			}
			return (OSGridReference[])journeyNodesGridReferences.ToArray(typeof(OSGridReference));
		}

		#endregion
	}
}
