// ************************************************************** 
// NAME			: DateFlexibility.cs
// AUTHOR		: Joe Morrissey
// DATE CREATED	: 22/12/2004 
// DESCRIPTION	: Definition of a DateFlexibility enumerator
// ************************************************************** 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/Domain/DateFlexibility.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:36:44   mturner
//Initial revision.
//
//   Rev 1.0   Dec 22 2004 12:25:32   jmorrissey
//Initial revision.

using System;

namespace TransportDirect.UserPortal.PricingRetail.Domain
{
	/// <summary>
	/// Summary description for FlexibiltyDays.
	/// </summary>
	public enum DateFlexibility
	{
		noFlexibility,
		oneDay,
		twoDays,
		threeDays
	}
}
