// *******************************************************
// NAME                 : RailCostPageState.cs
// AUTHOR               : Tim Mollart
// DATE CREATED         : 12/07/2004 
// DESCRIPTION			: Rail Search by Price Page State
// *******************************************************

using System;

namespace TransportDirect.UserPortal.SessionManager
{
	/// <summary>
	/// Summary description for FindFarePageState.
	/// </summary>
	[CLSCompliant(false)]
	[Serializable]
	public class RailCostPageState : FindFarePageState 
	{
		public RailCostPageState()
		{
			findMode = FindAMode.RailCost;
		}
	}
}
