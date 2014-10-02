// *********************************************** 
// NAME                 : PricedJourneySegment.cs 
// AUTHOR               : Jonathan George
// DATE CREATED         : 21/12/2004
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/SessionManager/Adapters/PricedJourneySegment.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:48:52   mturner
//Initial revision.
//
//   Rev 1.3   Jun 29 2007 19:09:14   asinclair
//Fix for usd 1085365
//
//   Rev 1.2   Mar 14 2006 08:41:44   build
//Automatically merged from branch for stream3353
//
//   Rev 1.1.1.0   Mar 10 2006 19:08:32   rhopkins
//Removal of JourneyDetail class.
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.1   Mar 18 2005 09:18:32   jgeorge
//Updated InboundLegs property for unpriced segment.
//
//   Rev 1.0   Dec 23 2004 11:55:04   jgeorge
//Initial revision.

using System;
using System.Collections;
using TransportDirect.UserPortal.PricingRetail.Domain;
using TransportDirect.UserPortal.JourneyControl;

namespace TransportDirect.UserPortal.SessionManager
{
	/// <summary>
	/// Represents a segment of a public journey which will either be several journey
	/// legs contained by a single pricing unit, or a single unpriced leg
	/// </summary>
	[Serializable]
	[CLSCompliant(false)]
	public class PricedJourneySegment
	{
		#region Private fields

        private readonly bool isFirst;
		private readonly bool isLast;
		private readonly PricingUnit wrappedUnit;
		private readonly PublicJourneyDetail unpricedLeg;

		#endregion

		#region Constructor

		/// <summary>
		/// Private constructor for standard behaviour
		/// </summary>
		/// <param name="isFirst">True if this is the first segment of the journey</param>
		/// <param name="isLast">True if this is the last segment of the journey</param>
		private PricedJourneySegment(bool isFirst, bool isLast)
		{
			this.isFirst = isFirst;
			this.isLast = isLast;
		}

		/// <summary>
		/// Constructor for creating an object representing a priced segment of the journey
		/// </summary>
		/// <param name="wrappedUnit">The PricingUnit containing the journey details</param>
		/// <param name="isFirst">True if this is the first segment of the journey</param>
		/// <param name="isLast">True if this is the last segment of the journey</param>
		public PricedJourneySegment(PricingUnit wrappedUnit, bool isFirst, bool isLast) : this(isFirst, isLast)
		{
			this.wrappedUnit = wrappedUnit;
		}
		
		/// <summary>
		/// Constructor for creating an object representing an unpriced segment of the journey.
		/// </summary>
		/// <param name="unpricedLeg">The leg of the journey which has no pricing information</param>
		/// <param name="isFirst">True if this is the first segment of the journey</param>
		/// <param name="isLast">True if this is the last segment of the journey</param>
		public PricedJourneySegment(PublicJourneyDetail unpricedLeg, bool isFirst, bool isLast) : this(isFirst, isLast)
		{
			this.unpricedLeg = unpricedLeg;
		}

		#endregion

		#region Properties

		/// <summary>
		/// The wrapped pricing unit, or null if the segment is unpriced
		/// </summary>
		public PricingUnit PricingUnit
		{
			get { return wrappedUnit; } 
		}

		/// <summary>
		/// True if the segment has a pricing unit associated with it, false otherwise
		/// </summary>
		public bool UnitIsPriced
		{
			get { return (wrappedUnit != null); }
		}

		/// <summary>
		/// Read only. Returns the mode for an unpriced leg.  Used to determine if it is a walk 
		/// leg, or another mode which we don't have the fare for.
		/// </summary>
		public TransportDirect.JourneyPlanning.CJPInterface.ModeType Mode
		{
			get { return unpricedLeg.Mode; }
		}

		/// <summary>
		/// The outbound legs of this segment of the journey
		/// </summary>
		public IList OutboundLegs
		{
			get
			{
				if (UnitIsPriced)
					return wrappedUnit.OutboundLegs;
				else
					return (IList)(new PublicJourneyDetail[] { unpricedLeg });
			}
		}

		/// <summary>
		/// The inbound legs of this segment of the journey
		/// </summary>
		public IList InboundLegs
		{
			get
			{
				if (UnitIsPriced)
					return wrappedUnit.InboundLegs;
				else
					return new ArrayList();
			}
		}

		/// <summary>
		/// True if this is the first segment of the journey
		/// </summary>
		public bool IsFirst
		{
			get { return isFirst; }
		}

		/// <summary>
		/// True if this is the last segment of the journey
		/// </summary>
		public bool IsLast
		{
			get { return isLast; }
		}

		#endregion

	}
}
