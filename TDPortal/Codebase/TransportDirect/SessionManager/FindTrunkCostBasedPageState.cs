using System;

namespace TransportDirect.UserPortal.SessionManager
{
	/// <summary>
	/// Summary description for FindTrunkCostBasedPageState.
	/// </summary>
	[CLSCompliant(false)]
	[Serializable]
	public class FindTrunkCostBasedPageState : FindCostBasedPageState
	{
		public FindTrunkCostBasedPageState()
		{
			findMode = FindAMode.TrunkCostBased;
		}
	}
}
