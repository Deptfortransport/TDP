//********************************************************************************
//NAME         : JourneyType.cs
//AUTHOR       : SchlumbergerSema
//DATE CREATED : 10/05/2003
//DESCRIPTION  : Data Transfer Object for fare tickets
//             : 
//DESIGN DOC   : DD034 Reatil Pricing
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingMessages/ReturnIndicator.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:35:54   mturner
//Initial revision.
//
//   Rev 1.0   Oct 15 2003 11:32:34   CHosegood
//Initial Revision

using System;

namespace TransportDirect.UserPortal.PricingMessages
{
	/// <summary>
	/// Summary description for JourneyType.
	/// </summary>
	public enum ReturnIndicator
	{
        Outbound = 0,
        Return = 1
	}
}
