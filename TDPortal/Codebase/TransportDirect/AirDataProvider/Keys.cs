// *********************************************** 
// NAME                 : Keys.cs 
// AUTHOR               : Jonathan George
// DATE CREATED         : 22/09/2004
// DESCRIPTION  : Static class that defines keys to
// retrieve properties (relating to Air Data Provider) 
// from the Properties Service.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/AirDataProvider/Keys.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:18:24   mturner
//Initial revision.
//
//   Rev 1.0   Sep 29 2004 12:44:24   jgeorge
//Initial revision.
using System;

namespace TransportDirect.UserPortal.AirDataProvider
{
	/// <summary>
	/// Summary description for Keys.
	/// </summary>
	public sealed class Keys
	{
		/// <summary>
		/// Private constructor to ensure no public constructor is created by the compiler
		/// </summary>
		private Keys()
		{ }

		public const string DataGateway = "datagateway";
		public const string TTBO = DataGateway + ".ttbo"; // eg datagateway.ttbo

		public const string Rail = "rail";
		public const string Air = "air";

		public const string CJPServers = TTBO + ".{0}.cjpservers";
		public const string TTBOServers = TTBO + ".{0}.ttboservers";
		public const string ServerFileLocation = TTBO + ".{0}.servers.{1}.filelocation";
		public const string ServerRemoteObject = TTBO + ".{0}.servers.{1}.remoteobject";


		public const string AirFeedName = DataGateway + ".airbackend.schedules.feedname";
		public const string RailFeedName = TTBO + ".rail.feedname";


	}
}
