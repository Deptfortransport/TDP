// *********************************************** 
// NAME			: ICarParkCatalgue.cs
// AUTHOR		: Esther Severn
// DATE CREATED	: 03/08/2006
// DESCRIPTION	: Interface for Car Park lookup and caching classes
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/LocationService/ICarParkCatalogue.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:25:06   mturner
//Initial revision.
//
//   Rev 1.4   Sep 18 2006 17:05:14   tmollart
//Removed signature of method thats no longer required.
//Resolution for 4190: Thread Safety Issue on Car Park Catalogue
//
//   Rev 1.3   Sep 08 2006 14:54:54   esevern
//Amended LoadData() - now only loads data for the specific car park selected.
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.2   Aug 24 2006 12:02:30   esevern
//Removed TDLocation from LoadData call
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.1   Aug 15 2006 16:37:40   esevern
//Added LoadData()
//Resolution for 4143: CCN319 Car Parking Phase 1 and 2
//
//   Rev 1.0   Aug 15 2006 14:04:42   esevern
//Initial revision.
//

using System;

namespace TransportDirect.UserPortal.LocationService
{
	/// <summary>
	/// Interface for CarParkCatalogue.
	/// </summary>
	public interface ICarParkCatalogue
	{
		CarPark GetCarPark(string carParkRef);
	}
}
