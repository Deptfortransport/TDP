//********************************************************************************
//NAME         : CoachJourneyFareDate.cs
//AUTHOR       : James Broome
//DATE CREATED : 22/02/2005
//DESCRIPTION  : Implementation of CoachJourneyFareDate class
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/NatExFares/CoachJourneyFareDate.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:37:04   mturner
//Initial revision.
//
//   Rev 1.0   Mar 23 2005 09:30:22   jbroome
//Initial revision.
//Resolution for 1405: Adjusting Journey causes unexpected results (DEL5.4)

using System;
using System.Collections;

using TransportDirect.Common;

namespace TransportDirect.UserPortal.PricingRetail.NatExFares
{
	/// <summary>
	/// Class used during the cost based search process.
	/// Holds a collection of CoachJourneyFareSummary objects 
	/// that apply to a specific date.
	/// </summary>
	public class CoachJourneyFareDate
	{

		#region Private members
			
		TDDateTime date;
		ArrayList fareSummary = null;

		#endregion
		
		#region Constructor

		/// <summary>
		/// Constructor. Requires the TDDateTime to which this 
		/// CoachJourneyFareDate applies. Intialises the internal 
		/// CoachJourneyFareSummary collection.
		/// </summary>
		/// <param name="date"></param>
		public CoachJourneyFareDate(TDDateTime date)
		{
			this.date = date;
			this.fareSummary = new ArrayList();
		}
		
		#endregion

		#region Public properties and methods

		/// <summary>
		/// Read-only TDDateTime property.
		/// </summary>
		public TDDateTime Date
		{
			get { return date; }
		}

		/// <summary>
		/// Adds specified CoachJourneyFareSummary object to the 
		/// internal collection of CoachJourneyFareSummary objects.
		/// </summary>
		/// <param name="summary"></param>
		public void AddFareSummary(CoachJourneyFareSummary summary)
		{
			fareSummary.Add(summary);
		}

		/// <summary>
		/// Read-only CoachJourneyFareSummary array property.
		/// The collection of CoachJourneyFareSummary which apply to
		/// this CoachJourneyFareDate.
		/// </summary>
		/// <returns>CoachJourneyFareSummary array</returns>
		public CoachJourneyFareSummary[] FareSummary
		{
			get
			{
				return (CoachJourneyFareSummary[])fareSummary.ToArray(typeof(CoachJourneyFareSummary));
			}
		}
		
		#endregion

	}
}
