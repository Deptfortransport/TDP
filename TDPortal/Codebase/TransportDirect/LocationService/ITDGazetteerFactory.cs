// *********************************************** 
// NAME                 : ITDGazetteerFactory
// AUTHOR               : Patrick ASSUIED
// DATE CREATED         : 29/08/2003 
// DESCRIPTION  : Interface for the Factory class in charge of creating the appropriate Gazetteer
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/LocationService/ITDGazetteerFactory.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:25:08   mturner
//Initial revision.
//
//   Rev 1.2   Jul 22 2004 10:40:26   RPhilpott
//Remove redundant "text" parameter
//
//   Rev 1.1   Jul 09 2004 13:09:12   passuied
//Changes for FindStation del 6.1 back end
//
//   Rev 1.0   Sep 05 2003 15:30:28   passuied
//Initial Revision

using System;

namespace TransportDirect.UserPortal.LocationService
{
	/// <summary>
	/// Summary description for ITDGazetteerFactory.
	/// </summary>
	public interface ITDGazetteerFactory
	{
		ITDGazetteer Gazetteer (SearchType type, string sessionID, bool userLoggedOn, StationType stationType);
	}
}
