// *********************************************** 
// NAME                 : DeliveryMode.cs
// AUTHOR               : Sanjeev Chand
// DATE CREATED         : 20/06/2005 
// DESCRIPTION  	    : The enumerator class for different type of delivery mechanism.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/DepartureBoardService/MobileBookmark/DeliveryMode.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:21:30   mturner
//Initial revision.
//
//   Rev 1.1   Jun 23 2005 14:03:56   schand
//Initial version
//Resolution for 2560: Del 8 Stream: Mobile bookmarks
//
//   Rev 1.0   Jun 20 2005 15:34:08   schand
//Initial revision.

using System;

namespace TransportDirect.UserPortal.DepartureBoardService.MobileBookmark
{
	/// <summary>
	/// The enumerator class for different type of delivery mechanism.
	/// </summary>
	public enum DeliveryMode
	{
		SMS,
		NokiaOTA,
		WapPush
	}
}
