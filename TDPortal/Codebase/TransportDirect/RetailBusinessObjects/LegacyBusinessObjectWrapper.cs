//********************************************************************************
//NAME         : LegacyBusinessObjectWrapper.cs
//AUTHOR       : SchlumbergerSema
//DATE CREATED : 10/05/2003
//DESCRIPTION  : 
//             : 
//DESIGN DOC   : DD034 Reatil Pricing
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/RetailBusinessObjects/LegacyBusinessObjectWrapper.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:46:08   mturner
//Initial revision.
//
//   Rev 1.0   Oct 08 2003 11:46:36   CHosegood
//Initial Revision

using System;

namespace TransportDirect.UserPortal.RetailBusinessObjects
{
	/// <summary>
    /// A delegate for the Flat Interface Object Calling Convention.
    /// This interface is defined in the Retail BO User Guide
    /// </summary>
    public delegate int LegacyBusinessObjectWrapper(string inputHeader, byte[] outputHeader, string inputBody, byte[] outputArray);
}
