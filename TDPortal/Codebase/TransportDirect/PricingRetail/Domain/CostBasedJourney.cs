// ************************************************************** 
// NAME			: CostBasedJourney.cs
// AUTHOR		: Joe Morrissey
// DATE CREATED	: 22/12/2004 
// DESCRIPTION	: Implementation of the CostBasedJourney class
// ************************************************************** 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/Domain/CostBasedJourney.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:36:44   mturner
//Initial revision.
//
//   Rev 1.5   Jan 18 2006 18:16:32   RPhilpott
//Chnages for FxCop compliance.
//Resolution for 3398: DN040: Code Review - Unit Test Fixes for DN040
//
//   Rev 1.4   Apr 25 2005 10:06:44   jbroome
//Added OutwardDateIndex and InwardDateIndex and renamed OutwardJourneIndexes and InwardJourneyIndexes
//
//   Rev 1.3   Mar 21 2005 11:29:28   jbroome
//Added InwardJourneys and OutwardJourneys collections
//
//   Rev 1.2   Feb 16 2005 11:07:56   jmorrissey
//Made serializable
//
//   Rev 1.1   Jan 12 2005 13:54:00   jmorrissey
//Added TDJourneyRequest and TDJourneyResult member variables.
//
//   Rev 1.0   Dec 22 2004 12:25:32   jmorrissey
//Initial revision.

using System;
using System.Collections;

using TransportDirect.UserPortal.JourneyControl;

namespace TransportDirect.UserPortal.PricingRetail.Domain
{
	/// <summary>
	/// Summary description for CostBasedJourney.
	/// </summary>
	[Serializable]
	public class CostBasedJourney
	{
		private TDJourneyResult costJourneyResult;
		private TDJourneyRequest costJourneyRequest;
		private ArrayList inwardJourneyIndexes;
		private ArrayList outwardJourneyIndexes;
		private int outwardDateIndex;
		private int inwardDateIndex;

		/// <summary>
		/// Constructor. Initialises internal collections
		/// </summary>
		public CostBasedJourney()
		{
			// Initialise ArrayLists so that they can be populated
			outwardJourneyIndexes = new ArrayList();
			inwardJourneyIndexes = new ArrayList();
		}

		/// <summary>
		/// Read/write public property for costJourneyResult
		/// </summary>
		public TDJourneyResult CostJourneyResult
		{
			get { return costJourneyResult;	}
			set	{ costJourneyResult = value; }
		}
		/// <summary>
		/// Read/write public property for costJourneyRequest
		/// </summary>
		public TDJourneyRequest CostJourneyRequest
		{
			get { return costJourneyRequest; }
			set	{ costJourneyRequest = value; }
		}

		/// <summary>
		/// Read/Write public property. ArrayList of integers
		/// </summary>
		public ArrayList InwardJourneyIndexes
		{
			get	{ return inwardJourneyIndexes; }
			set	{ inwardJourneyIndexes = value; }
		}

		/// <summary>
		/// Read/Write public property. ArrayList of integers
		/// </summary>
		public ArrayList OutwardJourneyIndexes
		{
			get	{ return outwardJourneyIndexes; }
			set	{ outwardJourneyIndexes = value; }
		}

		/// <summary>
		/// Read/write property. 
		/// </summary>
		public int OutwardDateIndex
		{
			get {return outwardDateIndex;}
			set {outwardDateIndex = value;}
		}
		
		/// <summary>
		/// Read/write property. 
		/// </summary>
		public int InwardDateIndex
		{
			get {return inwardDateIndex;}
			set {inwardDateIndex = value;}
		}
	}
}
