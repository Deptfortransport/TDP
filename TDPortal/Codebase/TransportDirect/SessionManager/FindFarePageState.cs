using System;

namespace TransportDirect.UserPortal.SessionManager
{
	/// <summary>
	/// Summary description for FindFarePageState.
	/// </summary>
	[CLSCompliant(false)]
	[Serializable]
	public class FindFarePageState : FindCostBasedPageState
	{
		public FindFarePageState()
		{
			findMode = FindAMode.Fare;
		}
	}
}
