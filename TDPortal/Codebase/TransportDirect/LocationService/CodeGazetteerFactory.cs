// *********************************************** 
// NAME                 : CodeGazetteerFactory.cs
// AUTHOR               : Patrick Assuied
// DATE CREATED         : /2005
// DESCRIPTION  : Factory class for Code gazetteer real implementation.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/LocationService/CodeGazetteerFactory.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:25:04   mturner
//Initial revision.
//
//   Rev 1.0   Jan 19 2005 14:20:36   passuied
//Initial revision.
//
//   Rev 1.0   Jan 19 2005 14:09:32   passuied
//Initial revision.

using System;

using TransportDirect.Common.ServiceDiscovery;

namespace TransportDirect.UserPortal.LocationService
{
	/// <summary>
	/// Factory class for Code gazetteer real implementation.
	/// </summary>
	public class CodeGazetteerFactory : IServiceFactory
	{
		public CodeGazetteerFactory()
		{
		}

		public object Get()
		{
			return new LocationService.TDCodeGazetteer();
		}
	}
}
