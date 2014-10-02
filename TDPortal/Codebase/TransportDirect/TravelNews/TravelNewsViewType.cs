// ********************************************************************* 
// NAME         : TravelNewsViewType.cs
// AUTHOR       : Joe Morrissey
// DATE CREATED : 22/06/2005
// DESCRIPTION  : Enumeration used by Travel News page when determining 
//				: whether to display details in a table or a map
// ********************************************************************* 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/TravelNews/TravelNewsViewType.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:50:30   mturner
//Initial revision.
//
//   Rev 1.0   Jun 22 2005 16:09:00   jmorrissey
//Initial revision.
//Resolution for 2558: Del 8 Stream: Allow MapControl to display larger maps

using System;

namespace TransportDirect.UserPortal.TravelNews
{
	/// <summary>
	/// Enumeration used by Travel News page when determining 
	/// whether to display details in a table or a map
	/// </summary>
	public enum TravelNewsViewType
	{
		Details,
		Map
	}
}
