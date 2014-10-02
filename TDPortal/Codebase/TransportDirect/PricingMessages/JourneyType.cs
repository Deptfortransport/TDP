//********************************************************************************
//NAME         : JourneyType.cs
//AUTHOR       : SchlumbergerSema
//DATE CREATED : 10/05/2003
//DESCRIPTION  : Data Transfer Object for Pricing Request
//             : 
//DESIGN DOC   : DD034 Reatil Pricing
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingMessages/JourneyType.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:35:50   mturner
//Initial revision.
//
//   Rev 1.1   Mar 22 2005 16:08:16   RPhilpott
//Addition of cost-based search for Del 7.
//
//   Rev 1.0   Oct 17 2003 10:15:52   CHosegood
//Initial Revision

using System;

namespace TransportDirect.UserPortal.PricingMessages
{
    /// <summary>
    /// Summary description for JourneyType.
    /// </summary>
    public enum JourneyType
    {
        OutwardSingle = 0,
		InwardSingle = 1,
		Return = 2,

    }
}
