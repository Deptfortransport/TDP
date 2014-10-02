// *********************************************** 
// NAME                 : Values.cs
// AUTHOR               : Annukka Viitanen
// DATE CREATED         : 03/03/2006 
// DESCRIPTION			: Values for travel news.
// Previously in Enumerations.cs
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/TravelNews/Values.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:50:30   mturner
//Initial revision.
//
//   Rev 1.3   Mar 14 2006 15:42:10   AViitanen
//Updated following code review comments.
//Resolution for 24: DEL 8.1 Workstream - Travel News Updates
//
//   Rev 1.2   Mar 13 2006 10:54:36   AViitanen
//Added headerblock. 
//Resolution for 24: DEL 8.1 Workstream - Travel News Updates

using System;

namespace TransportDirect.UserPortal.TravelNews
{
	/// <summary>
	/// Summary description for Values.
	/// </summary>
	internal class Values
	{
		/// <summary>
		/// Constructor - does nothing
		/// </summary>
		private Values(){}

		public const string RecentCriteria = "61";
		public const int IntRecentCritera = 61; // !!! should be consistent with RecentCriteria string value
		public const string UidUnavailable = "RTM999980";
		public const string Road = "Road";
		
	}
}
