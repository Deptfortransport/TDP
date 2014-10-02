// *********************************************** 
// NAME                 : Keys.cs 
// AUTHOR               : Gary Eaton
// DATE CREATED         : 18/08/2003 
// DESCRIPTION  :  Defines key names used for
// accessing properties using property service, and
// for accessing values from .config file.
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/RetailBusinessObjects/Keys.cs-arc  $
//
//   Rev 1.1   Jan 11 2009 17:03:02   mmodi
//Added ZPBO property keys
//Resolution for 5210: CCN487 - ZPBO Implementation workstream
//
//   Rev 1.0   Nov 08 2007 12:46:08   mturner
//Initial revision.
//
//   Rev 1.7   Feb 17 2006 14:48:26   aviitanen
//Merge from Del8.0 to 8.1
//
//   Rev 1.6   Feb 10 2005 17:37:18   RScott
//Updated to include Reservation and Supplement Business Objects (RVBO, SBO)
//
//   Rev 1.5   Oct 28 2003 20:04:54   geaton
//Changes to support housekeeping and timeout functionality.
//
//   Rev 1.4   Oct 21 2003 15:22:48   geaton
//Changes to support business object timeout functionality.
//
//   Rev 1.3   Oct 16 2003 11:18:04   geaton
//Corrected typo.
//
//   Rev 1.2   Oct 15 2003 21:34:10   geaton
//Added keys for objectids
//
//   Rev 1.1   Oct 15 2003 19:50:56   CHosegood
//Added RBO & LBO
//
//   Rev 1.0   Oct 15 2003 14:40:04   geaton
//Initial Revision

using System;

namespace TransportDirect.UserPortal.RetailBusinessObjects
{
	/// <summary>
	/// Container class used to key names
	/// </summary>
	public sealed class Keys
	{
		private Keys()
		{
		}

		// .NET file trace listener path
		public const string DefaultLogPath		= "DefaultLogPath";

		// Key Prefixes
		public const string RetailBusinessObjects = "RetailBusinessObjects";
		public const string FBO					  = RetailBusinessObjects + ".FBO";
		public const string RBO					  = RetailBusinessObjects + ".RBO";
		public const string LBO					  = RetailBusinessObjects + ".LBO";
		public const string RVBO				  = RetailBusinessObjects + ".RVBO";
		public const string SBO					  = RetailBusinessObjects + ".SBO";
        public const string ZPBO                  = RetailBusinessObjects + ".ZPBO";

		// FBO Pooling Keys
		public const string FBOInterfaceVersion = FBO + ".InterfaceVersion";
		public const string FBOPoolSize			= FBO + ".PoolSize";
		public const string FBOObjectId			= FBO + ".ObjectId";
		public const string FBOTimeoutDuration	= FBO + ".TimeoutDuration";
		public const string FBOTimeoutCheckFrequency = FBO + ".TimeoutCheckFrequency";
		public const string FBOTimeoutChecking  = FBO + ".TimeoutChecking";
		public const string FBOMinimumPoolSize  = FBO + ".MinimumPoolSize";

        // RBO Pooling Keys
        public const string RBOInterfaceVersion = RBO + ".InterfaceVersion";
        public const string RBOPoolSize			= RBO + ".PoolSize";
		public const string RBOObjectId			= RBO + ".ObjectId";
		public const string RBOTimeoutDuration	= RBO + ".TimeoutDuration";
		public const string RBOTimeoutCheckFrequency = RBO + ".TimeoutCheckFrequency";
		public const string RBOTimeoutChecking  = RBO + ".TimeoutChecking";
		public const string RBOMinimumPoolSize  = RBO + ".MinimumPoolSize";

        // LBO Pooling Keys
        public const string LBOInterfaceVersion = LBO + ".InterfaceVersion";
        public const string LBOPoolSize			= LBO + ".PoolSize";
		public const string LBOObjectId			= LBO + ".ObjectId";
		public const string LBOTimeoutDuration	= LBO + ".TimeoutDuration";
		public const string LBOTimeoutCheckFrequency = LBO + ".TimeoutCheckFrequency";
		public const string LBOTimeoutChecking  = LBO + ".TimeoutChecking";
		public const string LBOMinimumPoolSize  = LBO + ".MinimumPoolSize";

		// RVBO Pooling Keys
		public const string RVBOInterfaceVersion = RVBO + ".InterfaceVersion";
		public const string RVBOPoolSize			= RVBO + ".PoolSize";
		public const string RVBOObjectId			= RVBO + ".ObjectId";
		public const string RVBOTimeoutDuration	= RVBO + ".TimeoutDuration";
		public const string RVBOTimeoutCheckFrequency = RVBO + ".TimeoutCheckFrequency";
		public const string RVBOTimeoutChecking  = RVBO + ".TimeoutChecking";
		public const string RVBOMinimumPoolSize  = RVBO + ".MinimumPoolSize";
		
		// SBO Pooling Keys
		public const string SBOInterfaceVersion = SBO + ".InterfaceVersion";
		public const string SBOPoolSize			= SBO + ".PoolSize";
		public const string SBOObjectId			= SBO + ".ObjectId";
		public const string SBOTimeoutDuration	= SBO + ".TimeoutDuration";
		public const string SBOTimeoutCheckFrequency = SBO + ".TimeoutCheckFrequency";
		public const string SBOTimeoutChecking  = SBO + ".TimeoutChecking";
		public const string SBOMinimumPoolSize  = SBO + ".MinimumPoolSize";

        // ZPBO Pooling Keys
        public const string ZPBOInterfaceVersion = ZPBO + ".InterfaceVersion";
        public const string ZPBOPoolSize = ZPBO + ".PoolSize";
        public const string ZPBOObjectId = ZPBO + ".ObjectId";
        public const string ZPBOTimeoutDuration = ZPBO + ".TimeoutDuration";
        public const string ZPBOTimeoutCheckFrequency = ZPBO + ".TimeoutCheckFrequency";
        public const string ZPBOTimeoutChecking = ZPBO + ".TimeoutChecking";
        public const string ZPBOMinimumPoolSize = ZPBO + ".MinimumPoolSize";

		// Other keys
		public const string HousekeepingCheckFrequency = RetailBusinessObjects + ".HousekeepingCheckFrequency";

		// Keys for RBOImport Task

		public const string RBOServers = "datagateway.retailbusinessobjects.servers";
		public const string ServerFileLocation = RBOServers + ".{0}.filelocation";
		public const string ServerRemoteObject = RBOServers + ".{0}.remotelocation";
	
	}
}

