// *********************************************** 
// NAME         : RoadJourneyChargeItem.cs
// AUTHOR       : Andrew Sinclair
// DATE CREATED : 12/01/2001 
// DESCRIPTION  : A class representing the charge items of a road journey
// leg, e.g. CompanyName, CompanyURL, and Charge
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyControl/RoadJourneyChargeItem.cs-arc  $;
//
//   Rev 1.0   Nov 08 2007 12:23:58   mturner
//Initial revision.
//
//   Rev 1.1   Mar 08 2006 17:02:42   CRees
//Fix for toll road pricing on same-day return journeys
//
//   Rev 1.0   Jan 20 2005 10:18:48   asinclair
//Initial revision.


using System;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.UserPortal.LocationService;

namespace TransportDirect.UserPortal.JourneyControl
{
	/// <summary>
	/// Summary description for RoadJourneyChargeItem.
	/// </summary>
 	[Serializable()]
	public class RoadJourneyChargeItem
	{
		private string companyName;
		private string url;
		private int charge;
		// IR 3518 added sectionType to allow test of congestion charge
		private StopoverSectionType sectionType;

		public RoadJourneyChargeItem(string Company, string URL, int Cost)
		{

			// Set the properties at instantiation
			companyName = Company;
			url = URL;
			charge = Cost;
		}
		// IR 3518 - new overloaded constructor.
		public RoadJourneyChargeItem(string Company, string URL, int Cost, StopoverSectionType Type)
		{

			// Set the properties at instantiation
			companyName = Company;
			url = URL;
			charge = Cost;
			sectionType = Type;
		}
		public string CompanyName
		{
			get { return companyName; }
		}

		public string Url
		{
			get { return url; }
		}

		public int Charge
		{
			get { return charge; }
		}
		// IR 3518 - new method.
		public StopoverSectionType SectionType
		{
			get { return sectionType; }
		}
	}
}
