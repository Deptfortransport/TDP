// *********************************************** 
// NAME			: JourneyDetail.cs
// AUTHOR		: Andrew Toner
// DATE CREATED	: 10/08/2003 
// DESCRIPTION	: Implementation of the JourneyDetail class
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyControl/JourneyDetail.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:23:46   mturner
//Initial revision.
//
//   Rev 1.5   Aug 19 2005 14:04:04   jgeorge
//Automatically merged from branch for stream2572
//
//   Rev 1.4.1.0   Aug 15 2005 17:15:02   RPhilpott
//FxCop fix
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.4   Sep 11 2003 16:34:10   jcotton
//Made Class Serializable
//
//   Rev 1.3   Sep 08 2003 17:45:14   RPhilpott
//Class should be abstract.
//
//   Rev 1.2   Aug 20 2003 17:55:46   AToner
//Work in progress
using System;

namespace TransportDirect.UserPortal.JourneyControl
{
	/// <summary>
	/// Abstract base class for the different types of journey details.
	/// </summary>
	[Serializable()]
	public abstract class JourneyDetail
	{
		protected JourneyDetail()
		{
		}
	}
}
