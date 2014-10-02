//********************************************************************************
//NAME         : FlexibilityType.cs
//AUTHOR       : Murat Guney
//DATE CREATED : 05/10/2005
//DESCRIPTION  : Implementation of FlexibilityType enumeration.
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/CoachFareInterfaces/FlexibilityType.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:36:30   mturner
//Initial revision.
//
//   Rev 1.4   Oct 13 2005 14:55:04   mguney
//Creation date corrected.
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.3   Oct 12 2005 14:43:12   mguney
//XMLSerialisation attribute added.
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.2   Oct 12 2005 14:38:40   mguney
//XMLSerialisation attribute added.
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.1   Oct 12 2005 11:04:06   mguney
//Initial draft Associated SCR
//Resolution for 2818: DEL 7.3 Stream: Search by Price
//
//   Rev 1.0   Oct 12 2005 11:01:04   mguney
//Initial revision.

using System;

namespace TransportDirect.UserPortal.PricingRetail.CoachFareInterfaces
{
	/// <summary>
	/// FlexibilityType enum. Flexibility type for the coach fare tickets.
	/// </summary>
	[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.transportdirect.info")]
	public enum FlexibilityType
	{
		Low,
		Fully,
		No,
		Unknown
	}
}
