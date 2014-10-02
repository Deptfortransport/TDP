// ****************************************************** 
// NAME			: Probability.cs
// AUTHOR		: Joe Morrissey
// DATE CREATED	: 25/01/2005
// DESCRIPTION	: Probability enumerator
// ****************************************************** 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/Domain/Probability.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:36:52   mturner
//Initial revision.
//
//   Rev 1.1   Mar 09 2005 14:09:20   jbroome
//Added enum value None
//
//   Rev 1.0   Jan 25 2005 14:42:24   jmorrissey
//Initial revision.

using System;

namespace TransportDirect.UserPortal.PricingRetail.Domain
{
	/// <summary>
	/// Probability indicator enum
	/// </summary>	
	[Serializable]
	public enum Probability {None, Low, Medium, High}
	
}
