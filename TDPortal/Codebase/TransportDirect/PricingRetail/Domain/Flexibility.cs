// *********************************************** 
// NAME			: Flexibility.cs
// AUTHOR		: 
// DATE CREATED	: 26/09/03
// DESCRIPTION	: Implementation of the Flexibility class
// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/Domain/Flexibility.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:36:46   mturner
//Initial revision.
//
//   Rev 1.7   Nov 09 2005 12:31:40   build
//Automatically merged from branch for stream2818
//
//   Rev 1.6.1.0   Oct 20 2005 17:17:32   mguney
//Unknown added.
//Resolution for 2818: DEL 7.3 Stream: Search by Price
 
using System;

namespace TransportDirect.UserPortal.PricingRetail.Domain
{
	/// <summary>
	/// Enum of available ticket Flexibilities
	/// </summary>
	[Serializable]
	public enum Flexibility {NoFlexibility, LimitedFlexibility, FullyFlexible, Unknown}
}
