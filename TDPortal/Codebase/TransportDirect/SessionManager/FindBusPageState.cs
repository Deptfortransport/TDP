// *********************************************** 
// NAME                 : FindBusPageState
// AUTHOR               : Esther Severn
// DATE CREATED         : 20/03/2006
// DESCRIPTION  : Session state information specific 
//				  to FindABus input page
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/SessionManager/FindBusPageState.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:48:22   mturner
//Initial revision.
//
//   Rev 1.3   Apr 03 2006 14:35:34   esevern
//Code review - added class header description
//Resolution for 30: DEL 8.1 Workstream - Find a Bus
//
//   Rev 1.2   Mar 29 2006 12:47:08   mdambrine
//fixed the problem with the datecontrols not showing the time now
//Resolution for 30: DEL8.1 Workstream - Find a Bus (New stream)
//
//   Rev 1.1   Mar 22 2006 11:07:10   esevern
//added re-instate and save journey parameters()
//Resolution for 30: DEL8.1 Workstream - Find a Bus (New stream)

using System;

using TransportDirect.UserPortal.LocationService;
using LocationSelectControlType = TransportDirect.UserPortal.SessionManager.TDJourneyParameters.LocationSelectControlType;

namespace TransportDirect.UserPortal.SessionManager
{
	/// <summary>
	/// Class responsible for managing session state
	/// data relating to Find A Bus page.
	/// </summary>
	[CLSCompliant(false)]
	[Serializable]
	public class FindBusPageState : FindPageState
	{
		
		#region Declaration
		
		private LocationSearch publicViaLocationSearch;
		private TDLocation publicViaLocation;
		private LocationSelectControlType publicViaType;
	
		#endregion	
	
		public FindBusPageState()
		{
			base.findMode = FindAMode.Bus;
		}

		/// <summary>
		/// Sets the journey parameters currently associated with the session to be those
		/// stored by this object, saved by previously calling SaveJourneyParameters()
		/// </summary>
		public override void ReinstateJourneyParameters(TDJourneyParameters journeyParameters) 
		{
			TDJourneyParametersMulti journeyParams = journeyParameters as TDJourneyParametersMulti;

			journeyParams.PublicVia = publicViaLocationSearch;
			journeyParams.PublicViaLocation = publicViaLocation;
			journeyParams.PublicViaType = publicViaType;

			base.ReinstateJourneyParameters(journeyParameters);
		}

		/// <summary>
		/// Stores (references) of the journey parameters currently associated with the
		/// session so that they may be reinstated when switching from ambiguity mode
		/// back to input mode
		/// </summary>
		public override void SaveJourneyParameters(TDJourneyParameters journeyParameters) 
		{
			TDJourneyParametersMulti journeyParams = journeyParameters as TDJourneyParametersMulti;

			publicViaLocationSearch = journeyParams.PublicVia;
			publicViaLocation = journeyParams.PublicViaLocation;
			publicViaType = journeyParams.PublicViaType;

			base.SaveJourneyParameters(journeyParameters);
		}


	}		
}