// *********************************************** 
// NAME			: IOperatorCatalogue.cs
// AUTHOR		: Paul Cross
// DATE CREATED	: 15/07/2005
// DESCRIPTION	: Interface definition for the OperatorCatalogue class
//				  which wraps access to service operator information.
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyControl/IOperatorCatalogue.cs-arc  $
//
//   Rev 1.4   Mar 21 2013 15:54:22   rbroddle
//Amended to translate operators for MDV region prefixes
//
//   Rev 1.3   Dec 10 2012 12:13:16   mmodi
//Updated accessible operator to include WEF and WEU datetimes
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.2   Dec 05 2012 14:14:50   mmodi
//Updated for accessible operators
//Resolution for 5873: CCN:XXX - Accessible Journeys Planner
//
//   Rev 1.1   Mar 20 2008 10:11:46   mturner
//Del10 patch1 from Dev factory
//
//   Rev 1.0   Nov 08 2007 12:23:42   mturner
//Initial revision.
//
//   Rev 1.3   Apr 03 2007 10:18:06   dsawe
//updated for local zonal services phase 2 & 3
//Resolution for 4362: Local Zonal Services Phase 2 & 3
//
//   Rev 1.2   Mar 16 2007 10:00:56   build
//Automatically merged from branch for stream4362
//
//   Rev 1.1.1.0   Mar 12 2007 16:03:34   dsawe
//added zonal operator links code
//Resolution for 4362: Local Zonal Services Phase 2 & 3
//
//   Rev 1.1   Jul 25 2005 20:56:54   pcross
//FxCop update
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.0   Jul 18 2005 16:16:34   pcross
//Initial revision.
//

using System;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.Common;

namespace TransportDirect.UserPortal.JourneyControl
{
	/// <summary>
	/// Interface for objects handling service operator information.
	/// </summary>
	public interface IOperatorCatalogue
	{
        String TranslateOperator(string operatorCode);
		ServiceOperator GetOperator(string operatorCode, ModeType travelMode);
        ServiceOperator GetAccessibleOperator(string operatorCode, string serviceNumber, string region, ModeType mode, TDDateTime dateTime);
        ServiceOperator GetZonalOperatorLinks(string ModeId, string OperatorCode, string RegionId);
		string GetZonalOperatorFaresLinks(string ModeId, string OperatorCode, string RegionId);
	}

}
