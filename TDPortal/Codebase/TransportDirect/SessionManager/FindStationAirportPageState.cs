// *********************************************** 
// NAME                 : FindStationAirportPageState.cs
// AUTHOR               : C.M. Owczarek
// DATE CREATED         : 19.08.04
// DESCRIPTION          : Responsible for managing session state
// data relating to Find A Station/Airport pages. Currently this
// class is only a placeholder that allows for the indentification
// of the current Find A mode.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/SessionManager/FindStationAirportPageState.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:48:26   mturner
//Initial revision.
//
//   Rev 1.0   Aug 19 2004 12:38:24   COwczarek
//Initial revision.
//Resolution for 1345: Clicking Find A tab should display page for current Find A mode

using System;

namespace TransportDirect.UserPortal.SessionManager
{
	/// <summary>
    /// Responsible for managing session state
    /// data relating to Find A Station/Airport pages. Currently this
    /// class is only a placeholder that allows for the indentification
    /// of the current Find A mode.
	/// </summary>
	
	[CLSCompliant(false)]
	[Serializable]
	public class FindStationAirportPageState : FindPageState
	{

        /// <summary>
        /// Constructor. Sets the Mode property to indicate Find A Station/Airport mode.
        /// </summary>
		public FindStationAirportPageState()
		{
			findMode = FindAMode.Station;
		}
	}
}
