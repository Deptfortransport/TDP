//********************************************************************************
//NAME         : TTBORemoteCommands.cs
//AUTHOR       : Jonathan George
//DATE CREATED : 22/09/2004
//DESCRIPTION  : Remote TTBO Update Command interface and classes.
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/AirDataProvider/TTBORemoteCommands.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:18:26   mturner
//Initial revision.
//
//   Rev 1.1   Oct 20 2004 17:52:58   jgeorge
//Bug fix for Air CJP update command
//
//   Rev 1.0   Sep 30 2004 09:18:34   jgeorge
//Initial revision.

using System;
using System.Collections;
using System.Net;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;

using TransportDirect.Common;
using TransportDirect.Common.Logging;

using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.JourneyPlanning.CJP;
using TransportDirect.JourneyPlanning.TTBO;

namespace TransportDirect.UserPortal.AirDataProvider
{
	
	#region IRemoteTTBOUpdateCommand

	/// <summary>
	/// Interface provides a common interface for the TTBO and CJP update commands
	/// </summary>
	internal interface IRemoteTTBOUpdateCommand
	{
		bool Init(string targetUrl);
		int Update();
		string Id();
	}

	#endregion

	#region RemoteGWTTBOUpdateCommand

	/// <summary>
	/// Implementation of the IRemoteTTBOUpdateCommand interface for calling the TTBOHost update method directly
	/// Note: The "GW" in the class name stands for Gateway - at time of creation, the only place that the 
	/// TTBOHost update method needed to be called directly is on the gateway servers.
	/// </summary>
	internal class RemoteGWTTBOUpdateCommand : IRemoteTTBOUpdateCommand
	{
		string id;
		private TTBOInterface ttbo;
			
		public RemoteGWTTBOUpdateCommand(string id)
		{this.id = id;}

		public bool Init(string targetUrl)
		{
			ttbo = (TTBOInterface)RemotingServices.Connect(typeof(TTBOInterface), targetUrl);
			if (ttbo != null) 
			{
				// Specify Windows Integrated Authentication credentials
				IDictionary Props = ChannelServices.GetChannelSinkProperties(ttbo);
				Props["credentials"] = CredentialCache.DefaultCredentials;
				return true;
			} 
			else
			{
				return false;
			}
		}

		public int Update()
		{return ttbo.UpdateDatabase( false );}

		public string Id() 
		{
		{return id;}
		}			
	}

	#endregion

	#region RemoteCJPTTBOUpdateCommand

	/// <summary>
	/// Implementation of the IRemoteTTBOUpdateCommand interface for calling the CJP TTBO update method
	/// </summary>
	internal class RemoteCJPTTBOUpdateCommand : IRemoteTTBOUpdateCommand
	{
		string id;
		private ICJP cjp;

		public RemoteCJPTTBOUpdateCommand(string id)
		{this.id = id;}

		public bool Init(string targetUrl)
		{
			cjp = (ICJP)RemotingServices.Connect(typeof(ICJP), targetUrl);
			if (cjp != null)
			{
				// Specify Windows Integrated Authentication credentials
				IDictionary Props = ChannelServices.GetChannelSinkProperties(cjp);
				Props["credentials"] = CredentialCache.DefaultCredentials;
				return true;
			}
			else
			{
				return false;
			}
		}

		public int Update()
		{
			Message message = cjp.UpdateRailData();
			if (message != null) 
			{
				return int.Parse(message.code.ToString());
			}
			else 
			{
				return 9999;
			}
		}

		public string Id() 
		{return id;}	
	}

	#endregion

	#region RemoteGWAirTTBOUpdateCommand

	/// <summary>
	/// Implementation of the IRemoteTTBOUpdateCommand interface for calling the TTBOHost update method directly
	/// Note: The "GW" in the class name stands for Gateway - at time of creation, the only place that the 
	/// TTBOHost update method needed to be called directly is on the gateway servers.
	/// </summary>
	internal class RemoteGWAirTTBOUpdateCommand : IRemoteTTBOUpdateCommand
	{
		string id;
		private TTBOInterface ttbo;
			
		public RemoteGWAirTTBOUpdateCommand(string id)
		{this.id = id;}

		public bool Init(string targetUrl)
		{
			ttbo = (TTBOInterface)RemotingServices.Connect(typeof(TTBOInterface), targetUrl);
			if (ttbo != null) 
			{
				// Specify Windows Integrated Authentication credentials
				IDictionary Props = ChannelServices.GetChannelSinkProperties(ttbo);
				Props["credentials"] = CredentialCache.DefaultCredentials;
				return true;
			} 
			else
			{
				return false;
			}
		}

		public int Update()
		{return ttbo.UpdateDatabase( true );}

		public string Id() 
		{
		{return id;}
		}			
	}

	#endregion

	#region RemoteCJPAirTTBOUpdateCommand

	/// <summary>
	/// Implementation of the IRemoteTTBOUpdateCommand interface for calling the CJP TTBO update method
	/// </summary>
	internal class RemoteCJPAirTTBOUpdateCommand : IRemoteTTBOUpdateCommand
	{
		string id;
		private ICJP cjp;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="id"></param>
		public RemoteCJPAirTTBOUpdateCommand(string id)
		{
			this.id = id;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="targetUrl"></param>
		/// <returns></returns>
		public bool Init(string targetUrl)
		{
			cjp = (ICJP)RemotingServices.Connect(typeof(ICJP), targetUrl);
			if (cjp != null)
			{
				// Specify Windows Integrated Authentication credentials
				IDictionary Props = ChannelServices.GetChannelSinkProperties(cjp);
				Props["credentials"] = CredentialCache.DefaultCredentials;
				return true;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public int Update()
		{
			Message message = cjp.UpdateAirData();
			if (message != null) 
			{
				return int.Parse(message.code.ToString());
			}
			else 
			{
				return 9999;
			}
		}


		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public string Id() 
		{return id;}	
	}

	#endregion
}
