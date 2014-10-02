// *********************************************** 
// NAME         : CostBasedSearchWaitControlData.cs
// AUTHOR       : Tim Mollart
// DATE CREATED : 22/12/2004
// DESCRIPTION  : Class for cost based search wait control data
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/SessionManager/CostBasedSearchWaitControlData.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:48:20   mturner
//Initial revision.
//
//   Rev 1.1   Jan 17 2005 15:52:48   tmollart
//Added [Serializabe] directive.
//
//   Rev 1.0   Dec 22 2004 15:29:24   tmollart
//Initial revision.

using System;
using TransportDirect.Common;

namespace TransportDirect.UserPortal.SessionManager
{
	/// <summary>
	/// Wait page data for cost based searches.
	/// </summary>
	[CLSCompliant(false)]
	[Serializable]
	public class CostBasedSearchWaitControlData
	{

		#region Private Member Declaration
		
		private PageId waitingPage;
		private PageId ambiguityPage;
		private PageId errorPage;
		private PageId destinationPage;

		#endregion

		#region Constructor

		public CostBasedSearchWaitControlData()
		{
		}

		#endregion

		#region Public Properties

		/// <summary>
		/// Read/Write property. Gets/Sets WaitingPage Page Id
		/// </summary>
		public PageId WaitingPage
		{
			get {return waitingPage;}
			set {waitingPage = value;}
		}

		/// <summary>
		/// Read/Write property. Gets/Sets AmbiguityPage Page Id
		/// </summary>
		public PageId AmbiguityPage
		{
			get {return ambiguityPage;}
			set {ambiguityPage = value;}
		}

		/// <summary>
		/// Read/Write property. Gets/Sets ErrorPage Page Id
		/// </summary>
		public PageId ErrorPage
		{
			get {return errorPage;}
			set {errorPage = value;}
		}

		/// <summary>
		/// Read/Write property. Gets/Sets DestinationPage Page Id
		/// </summary>
		public PageId DestinationPage
		{
			get {return destinationPage;}
			set {destinationPage = value;}
		}

		#endregion

	}
}
