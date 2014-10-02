//********************************************************************************
//NAME         : RBOPool.cs
//AUTHOR       : SchlumbergerSema
//DATE CREATED : 10/05/2003
//DESCRIPTION  : 
//             : 
//DESIGN DOC   : DD034 Retail Pricing
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/RetailBusinessObjects/RBOPool.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:46:14   mturner
//Initial revision.
//
//   Rev 1.11   May 18 2004 18:30:42   GEaton
//IR900 - updates to interface with new (C++) RBO
//
//   Rev 1.10   Oct 28 2003 20:04:58   geaton
//Changes to support housekeeping and timeout functionality.
//
//   Rev 1.9   Oct 22 2003 09:19:18   geaton
//Added housekeeping support to business objects.
//
//   Rev 1.8   Oct 21 2003 15:22:52   geaton
//Changes to support business object timeout functionality.
//
//   Rev 1.7   Oct 17 2003 13:12:56   geaton
//Added initialisation failure handling to allow progress if only some engines fail.
//
//   Rev 1.6   Oct 17 2003 11:57:48   geaton
//Removed obsolete interface property
//
//   Rev 1.5   Oct 17 2003 10:49:16   geaton
//Corrected typo.
//
//   Rev 1.4   Oct 16 2003 11:18:26   geaton
//Corrected typo.
//
//   Rev 1.3   Oct 16 2003 10:52:00   geaton
//Moved code to base class.
//
//   Rev 1.2   Oct 15 2003 21:33:44   geaton
//Updated exception id.
//
//   Rev 1.1   Oct 13 2003 15:29:50   CHosegood
//Implemented empty methods
//
//   Rev 1.0   Oct 08 2003 11:46:40   CHosegood
//Initial Revision

using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.PropertyService.Properties;

namespace TransportDirect.UserPortal.RetailBusinessObjects
{
	/// <summary>
	/// Provides pool of RBO objects.
	/// </summary>
	public sealed class RBOPool : RetailBusinessObjectPool
	{
		
		/// <summary>
		/// The Retail Business Object Dll name 
		/// used by business object engines in the pool.
		/// Filename convention:
		/// FBRE0012 is:
		/// FB(Prefix for all Dll types) RE (Restrictions Business Object) 
		/// 0(component number) 01(major version) 2(32bit)
		/// </summary>
		/// <remarks>Also the entry point name into copies of this dll.</remarks>
		
		private const string DllName = "FBRE0022";

		/// <summary>
		/// The maximum number of RBOs that may be added to pool.
		/// This must match the number of BO engines defined below.
		/// </summary>
		static public readonly int MaximumPoolSize = 5;

		// Define Business Object Engine Implementations
		// Each of these implementations is created by copying the master dll FBRE0022.dll
		[DllImport("FBRE0022_1.dll", EntryPoint=DllName)]
		static extern int restrictionsDll1(string inputHeader, byte[] outputHeader, string inputBody, byte[] outputArray);

		[DllImport("FBRE0022_2.dll", EntryPoint=DllName)]
		static extern int restrictionsDll2(string inputHeader, byte[] outputHeader, string inputBody, byte[] outputArray);

		[DllImport("FBRE0022_3.dll", EntryPoint=DllName)]
		static extern int restrictionsDll3(string inputHeader, byte[] outputHeader, string inputBody, byte[] outputArray);

		[DllImport("FBRE0022_4.dll", EntryPoint=DllName)]
		static extern int restrictionsDll4(string inputHeader, byte[] outputHeader, string inputBody, byte[] outputArray);

		[DllImport("FBRE0022_5.dll", EntryPoint=DllName)]
		static extern int restrictionsDll5(string inputHeader, byte[] outputHeader, string inputBody, byte[] outputArray);
		


		/// <summary>
		/// Singleton pattern
		/// </summary>
		private static RBOPool rboPool = null;

		/// <summary>
		/// Validates unique properties of RBO Pools.
		/// </summary>
		override protected void ValidateProperties(ArrayList errors)
		{
			// no unique properties to validate
		}

		/// <summary>
		/// Private Constructor to support singleton pattern.
		/// Creates the objects to pool.
		/// </summary>
		private RBOPool() : base(Keys.RBOInterfaceVersion,
								 Keys.RBOObjectId,
								 Keys.RBOPoolSize,
								 MaximumPoolSize,
								 Keys.RBOTimeoutDuration,
								 Keys.RBOTimeoutCheckFrequency,
								 Keys.RBOTimeoutChecking,
								 DllName + ".ini",
								 Keys.RBOMinimumPoolSize)
		{     

			try
			{
				if (MaximumPoolSize >= 1)
					AddEngineDll(new LegacyBusinessObjectWrapper(restrictionsDll1));

				if (MaximumPoolSize >= 2)
					AddEngineDll(new LegacyBusinessObjectWrapper(restrictionsDll2));

				if (MaximumPoolSize >= 3)
					AddEngineDll(new LegacyBusinessObjectWrapper(restrictionsDll3));

				if (MaximumPoolSize >= 4)
					AddEngineDll(new LegacyBusinessObjectWrapper(restrictionsDll4));

				if (MaximumPoolSize >= 5)
					AddEngineDll(new LegacyBusinessObjectWrapper(restrictionsDll5));

			}
			catch (TDException tdException)
			{
				throw tdException;
			}

			try
			{
				Initialise();
			}
			catch (TDException tdException)
			{
				throw tdException;
			}
			
		}

		/// <summary>
		/// Return an RBOPool instance.
		/// Implements the singleton pattern.
		/// </summary>
		/// <returns></returns>
		public static RBOPool GetRBOPool() 
		{
			if (rboPool == null)
			{
				lock ( typeof(RBOPool) ) 
				{
					if (rboPool == null) 
					{
						rboPool = new RBOPool();
					}
				}
			}

			return rboPool;
		}

	}
}

