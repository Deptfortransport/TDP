// *********************************************** 
// NAME                 : ResultsStatePate.cs
// AUTHOR               : James Broome
// DATE CREATED         : 26/09/2005 
// DESCRIPTION			: Holds variables indicating state of results page etc.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/SessionManager/ResultsPageState.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:48:36   mturner
//Initial revision.
//
//   Rev 1.3   Mar 14 2006 08:41:42   build
//Automatically merged from branch for stream3353
//
//   Rev 1.2.1.0   Jan 24 2006 10:45:22   pcross
//Updated to allow tickets / costs view for itinerery results page
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.2   Oct 13 2005 15:05:32   jbroome
//Made objects serializable
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.1   Oct 07 2005 16:16:18   tolomolaiye
//Modified ResultsModes
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.0   Sep 27 2005 14:14:32   jbroome
//Initial revision.
//Resolution for 2638: DEL 8 Stream: Visit Planner

using System;

namespace TransportDirect.UserPortal.SessionManager
{

	/// <summary>
	/// Enum to specify the available modes
	/// of the results page
	/// </summary>
	[Serializable()]
	public enum ResultsModes
	{
		Summary, 
		SchematicDetailsView, 
		TabularDetailsView, 
		MapView,
		TicketsCostsView
	}
	
	/// <summary>
	/// Summary description for ResultsPageState.
	/// </summary>
	[Serializable()]
	public class ResultsPageState
	{

		// Instance variables
		private ResultsModes resultMode;
		private int currentViewSelection;

		/// <summary>
		/// Default Constructor
		/// </summary>
		public ResultsPageState()
		{
		}

		/// <summary>
		/// Read-write property
		/// Used to determine the current 
		/// display mode of the results page
		/// </summary>
		public ResultsModes ResultsMode 
		{
			get { return resultMode; }
			set { resultMode = value; }
		}

		/// <summary>
		/// Read-write property
		/// Used to determine the current
		/// selection in the "show" list
		/// </summary>
		public int CurrentViewSelection
		{
			get { return currentViewSelection; }
			set { currentViewSelection = value; }
		}
	}
}
