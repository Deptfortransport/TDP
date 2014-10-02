// *********************************************** 
// NAME                 : TravelNewsHeadline.cs
// AUTHOR               : Jonathan George
// DATE CREATED         : 24 May 2004
// DESCRIPTION  : Class representing a single travel
// news headline. Used by TravelNewsHandler.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/TravelNews/TravelNewsHeadline.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:50:28   mturner
//Initial revision.
//
//   Rev 1.0   May 25 2004 15:20:00   asinclair
//Initial Revision

using System;

namespace TransportDirect.UserPortal.TravelNews
{
	/// <summary>
	/// Represents a single travel news headline. Clients can use the SeverityLevel
	/// property to determine how to display the item.
	/// </summary>
	public class TravelNewsHeadline
	{
		string headline;
		int severityLevel;

		public TravelNewsHeadline(string headline, int severityLevel)
		{
			this.headline = headline;
			this.severityLevel = severityLevel;
		}

		public string Headline
		{
			get { return headline; }
		}

		public int SeverityLevel
		{
			get { return severityLevel; }
		}
	}
}
