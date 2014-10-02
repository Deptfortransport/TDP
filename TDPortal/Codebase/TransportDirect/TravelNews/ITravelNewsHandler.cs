// *********************************************** 
// NAME                 : ITravelNewsHandler.cs 
// AUTHOR               : Joe Morrissey
// DATE CREATED         : 29/09/2003 
// DESCRIPTION  : Interface for classes that make travel 
// news available to clients
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/TravelNews/ITravelNewsHandler.cs-arc  $
//
//   Rev 1.2   Sep 01 2011 10:43:58   apatel
//Code update for Real Time Car
//Resolution for 5731: CCN 0548 - Real Time Information in Car
//
//   Rev 1.1   Sep 29 2009 11:41:38   rbroddle
//CCN 485a Travel News Parts 3 and 4 Hierarchy & Roadworks.
//
//   Rev 1.0   Nov 08 2007 12:50:24   mturner
//Initial revision.
//
//   Rev 1.9   Dec 16 2004 15:25:36   passuied
//Refactoring the TravelNews component
//
//   Rev 1.8   Sep 06 2004 21:08:56   JHaydock
//Major update to travel news
//
//   Rev 1.7   May 26 2004 10:22:48   jgeorge
//IR954 fix
//
//   Rev 1.6   Oct 14 2003 18:10:14   JHaydock
//Update to TravelNews to allow selective display of data
//
//   Rev 1.5   Oct 14 2003 14:36:54   JMorrissey
//Added new method
//
//   Rev 1.4   Oct 13 2003 10:32:12   JMorrissey
//Updated method signatures
//
//   Rev 1.3   Oct 10 2003 16:22:20   JMorrissey
//Removed unimplemented methods
//
//   Rev 1.2   Oct 10 2003 16:15:16   JMorrissey
//Updated signature for GetTravelHeadlines method to return a string
//
//   Rev 1.1   Oct 09 2003 16:09:18   JMorrissey
//Added methods
//
//   Rev 1.0   Sep 29 2003 17:49:08   JMorrissey
//Initial Revision

using System;
using System.Data;
using System.Data.SqlClient;

using TransportDirect.UserPortal.TravelNewsInterface;

namespace TransportDirect.UserPortal.TravelNews
{
	/// <summary>
	/// Summary description for ITravelNewsHandler.
	/// </summary>
	[CLSCompliant(false)]
	public interface ITravelNewsHandler
	{

		/// <summary>
		/// Returns whether the travel news is available
		/// </summary>
		bool IsTravelNewsAvaliable{get;}

		/// <summary>
		/// Returns the travel news unavailable text
		/// </summary>
		string TravelNewsUnavailableText{get;}

		/// <summary>
		/// Returns filtered/sorted data grid data
		/// </summary>
		/// <param name="travelNewsState">Current display settings (drop-down values)</param>
		/// <returns></returns>
		TravelNewsItem[] GetDetails(TravelNewsState travelNewsState);


		/// <summary>
		/// Returns the TravelNewsItem associated with the given uid.
		/// </summary>
		TravelNewsItem GetDetailsByUid(string uid);
		
		/// <summary>
		/// Returns headline data
		/// </summary>
		/// <returns></returns>
		HeadlineItem[] GetHeadlines();

		/// <summary>
		/// Returns headline data
		/// </summary>
		/// <returns></returns>
		HeadlineItem[] GetHeadlines(TravelNewsState travelNewsState);

        /// <summary>
        /// Returns children of given travel news ID
        /// </summary>
        TravelNewsItem[] GetChildrenDetailsByUid(string uid);

        /// <summary>
        /// Returns the travel news items affecting the toids specified on the date of the journey specified
        /// </summary>
        /// <param name="toidsList">List of toids for which any affecting travel news needs finding</param>
        /// <returns></returns>
        TravelNewsItem[] GetTravelNewsByAffectedToids(string[] toidsList);

	}
}
