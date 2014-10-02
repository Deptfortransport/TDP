// ***********************************************
// NAME 		: ScriptRepositoryFactory.cs
// AUTHOR 		: James Broome
// DATE CREATED : 16-Apr-2004
// DESCRIPTION 	: Factory class for the ScriptRepository.
// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ScriptRepository/ScriptRepositoryFactory.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:47:58   mturner
//Initial revision.
//
//   Rev 1.2   May 19 2004 12:13:28   jbroome
//Removed unecessary references
//
//   Rev 1.1   May 12 2004 16:25:26   jbroome
//Amended constructor so NUnit can be used.
//
//   Rev 1.0   Apr 30 2004 13:53:52   jbroome
//Initial revision.

using System;
using System.Resources;
using TransportDirect.Common;
using TransportDirect.Common.ServiceDiscovery;
using System.Web;



namespace TransportDirect.UserPortal.ScriptRepository
{
	/// <summary>
	/// Factory class for the DataServices component
	/// </summary>
	public class ScriptRepositoryFactory : IServiceFactory
	{
		ScriptRepository current;
		public ScriptRepositoryFactory(string rootFilePath, string configFile)
		{
			current = new ScriptRepository(rootFilePath, configFile);
		}

		public object Get()
		{
			return current;
		}
	}
}
