// *********************************************** 
// NAME                 : Enumerations.cs
// AUTHOR               : Patrick ASSUIED
// DATE CREATED         : 16/12/2004 
// DESCRIPTION : Contains classes to convert travel 
// news data between internal and external values
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/TravelNewsInterface/Enumerations.cs-arc  $ 
//
//   Rev 1.3   Sep 29 2009 11:46:12   RBroddle
//CCN 485a Travel News Parts 3 and 4 Hierarchy & Roadworks.
//Resolution for 5321: Travel News Parts 3 and 4 Hierarchy & Roadworks
//
//   Rev 1.2   Mar 10 2008 15:28:28   mturner
//Initial Del10 Codebase from Dev Factory
//
//   Rev 1.0   Nov 08 2007 12:50:38   mturner
//Initial revision.
//
//  Rev DevFactory Jan 08 2008 11:34:53 apatel
//new enum incident type added
//
//   Rev 1.3   Mar 28 2006 11:09:02   build
//Automatically merged from branch for stream0024
//
//   Rev 1.2.1.0   Mar 03 2006 15:55:42   AViitanen
//Updates for Travel News Updates (DN091)
//
//   Rev 1.2   Aug 18 2005 14:26:40   jgeorge
//Added header blocks
//Resolution for 2558: Del 8 Stream: Incident mapping

using System;

namespace TransportDirect.UserPortal.TravelNewsInterface
{
	internal class KeyValue
	{
		public const string All = "All";
		public const string PublicTransport = "Public Transport";
		public const string Road = "Road";
		public const string Recent = "Recent";
		public const string Full = "Full";
		public const string Summary = "Summary";
		public const string Major = "Very Severe";
        public const string Unplanned = "Unplanned";
        public const string Planned = "Planned";

	}
	
	public enum TransportType
	{
		All,
		PublicTransport,
		Road
	}

	public enum DelayType
	{
		All,
		Major,
		Recent
	}

    /// <summary>
    /// CCN 0421 new IncidentType enum for new shownews control filter 
    /// </summary>
    public enum IncidentType
    { 
        All, 
        Unplanned,
        Planned
    }

	public enum DisplayType
	{
		Full,
		Summary
	}

	public enum SeverityLevel
	{
		Critical = 0,
		Serious = 1,
		VerySevere = 2,
		Severe = 3,
		Medium = 4,
		Slight = 5,
		VerySlight = 6		
	}
    
	public enum SeverityFilter
	{
		Default,
		CriticalIncidents
	}

    public enum IncidentActiveStatus : byte 
    {
        Inactive,
        Active
    }

}
