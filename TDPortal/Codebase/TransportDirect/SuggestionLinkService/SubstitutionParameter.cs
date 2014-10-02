// *********************************************** 
// NAME                 : SubstitutionParameters.cs
// AUTHOR               : Ken Josling
// DATE CREATED         : 15/08/2005 
// DESCRIPTION			: An enumeration containing a list of valid substitution parameters 
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/SuggestionLinkService/SubstitutionParameter.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:50:10   mturner
//Initial revision.
//
//   Rev 1.1   Sep 02 2005 15:32:12   kjosling
//Updated following code review
//
//   Rev 1.0   Aug 24 2005 16:44:52   kjosling
//Initial revision.

using System;

namespace TransportDirect.UserPortal.SuggestionLinkService
{
	public enum SubstitutionParameter
	{
		OriginLocation,
		DestinationLocation,
		TestUnhandled
	}
}
