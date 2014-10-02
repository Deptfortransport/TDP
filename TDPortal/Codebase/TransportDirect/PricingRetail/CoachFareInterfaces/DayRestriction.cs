//********************************************************************************
//NAME         : DayRestriction.cs
//AUTHOR       : Murat Guney
//DATE CREATED : 05/10/2005
//DESCRIPTION  : Implementation of DayRestriction enumeration.
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/CoachFareInterfaces/DayRestriction.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:36:28   mturner
//Initial revision.
//
//   Rev 1.4   Oct 13 2005 14:53:08   mguney
//Creation date corrected.
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.3   Oct 12 2005 14:43:06   mguney
//XMLSerialisation attribute added.
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.2   Oct 12 2005 14:38:34   mguney
//XMLSerialisation attribute added.
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.1   Oct 12 2005 11:03:22   mguney
//Initial draft Associated SCR
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.0   Oct 12 2005 11:00:42   mguney
//Initial revision.

using System;

namespace TransportDirect.UserPortal.PricingRetail.CoachFareInterfaces
{
	/// <summary>
	/// DayRestriction enum. Day restrictions for coach fares.
	/// </summary>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.transportdirect.info")]
	public enum DayRestriction
	{
		Monday,
		Tuesday,
		Wednesday,
		Thursday,
		Friday,
		Saturday,
		Sunday,
	}
}
