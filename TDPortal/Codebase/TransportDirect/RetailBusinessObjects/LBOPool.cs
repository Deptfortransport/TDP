//********************************************************************************
//NAME         : LBOPool.cs
//AUTHOR       : SchlumbergerSema
//DATE CREATED : 10/05/2003
//DESCRIPTION  : 
//             : 
//DESIGN DOC   : DD034 Reatil Pricing
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/RetailBusinessObjects/LBOPool.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:46:08   mturner
//Initial revision.
//
//   Rev 1.8   Oct 28 2003 20:04:54   geaton
//Changes to support housekeeping and timeout functionality.
//
//   Rev 1.7   Oct 22 2003 09:19:14   geaton
//Added housekeeping support to business objects.
//
//   Rev 1.6   Oct 21 2003 15:22:50   geaton
//Changes to support business object timeout functionality.
//
//   Rev 1.5   Oct 17 2003 13:12:18   geaton
//Subclassed from RetailBusinessObjectPool and removed redundant code.
//
//   Rev 1.4   Oct 16 2003 16:31:32   CHosegood
//Removed Debug statement
//
//   Rev 1.3   Oct 16 2003 10:52:12   geaton
//Added max instances variable.
//
//   Rev 1.2   Oct 15 2003 21:34:20   geaton
//Updated tdexception id.
//
//   Rev 1.1   Oct 13 2003 15:29:52   CHosegood
//Implemented empty methods
//
//   Rev 1.0   Oct 08 2003 11:46:36   CHosegood
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
    /// Summary description for LBOPool.
    /// </summary>
    public class LBOPool : RetailBusinessObjectPool
    {
		
		/// <summary>
		/// The Retail Business Object Dll name 
		/// used by business object engines in the pool.
		/// Filename convention:
		/// FBLU0012 is:
		/// FB(Prefix for all Dll types) LU (Lookup Business Object) 
		/// 0(component number) 01(major version) 2(32bit)
		/// </summary>
		/// <remarks>Also the entry point name into copies of this dll.</remarks>
		private const string DllName = "FBLU0012";

		/// <summary>
		/// The maximum number of LBOs that may be added to pool.
		/// This must match the number of BO engine implementations defined below.
		/// </summary>
		static public readonly int MaximumPoolSize = 5;

		// Define Business Object Engine Implementations
		// Each of these implementations is created by copying the master dll FBLU0012.dll
        [DllImport("FBLU0012_1.dll", EntryPoint=DllName)]
        static extern int lookupDll1(string inputHeader, byte[] outputHeader, string inputBody, byte[] outputArray);

        [DllImport("FBLU0012_2.dll", EntryPoint=DllName)]
        static extern int lookupDll2(string inputHeader, byte[] outputHeader, string inputBody, byte[] outputArray);

        [DllImport("FBLU0012_3.dll", EntryPoint=DllName)]
        static extern int lookupDll3(string inputHeader, byte[] outputHeader, string inputBody, byte[] outputArray);

        [DllImport("FBLU0012_4.dll", EntryPoint=DllName)]
        static extern int lookupDll4(string inputHeader, byte[] outputHeader, string inputBody, byte[] outputArray);

        [DllImport("FBLU0012_5.dll", EntryPoint=DllName)]
        static extern int lookupDll5(string inputHeader, byte[] outputHeader, string inputBody, byte[] outputArray);

        /// <summary>
		/// Singleton pattern
		/// </summary>
		private static LBOPool lboPool = null;

		/// <summary>
		/// Validates unique properties of LBO Pools.
		/// </summary>
		override protected void ValidateProperties(ArrayList errors)
		{
			// no unique properties to validate
		}

		/// <summary>
		/// Private Constructor to support singleton pattern.
		/// Creates the objects to pool.
		/// </summary>
		private LBOPool() : base(Keys.LBOInterfaceVersion,
								 Keys.LBOObjectId,
								 Keys.LBOPoolSize,
								 MaximumPoolSize,
								 Keys.LBOTimeoutDuration,
								 Keys.LBOTimeoutCheckFrequency,
								 Keys.LBOTimeoutChecking,
								 DllName + ".ini",
								 Keys.LBOMinimumPoolSize)
		{     

			try
			{
				if (MaximumPoolSize >= 1)
					AddEngineDll(new LegacyBusinessObjectWrapper(lookupDll1));

				if (MaximumPoolSize >= 2)
					AddEngineDll(new LegacyBusinessObjectWrapper(lookupDll2));

				if (MaximumPoolSize >= 3)
					AddEngineDll(new LegacyBusinessObjectWrapper(lookupDll3));

				if (MaximumPoolSize >= 4)
					AddEngineDll(new LegacyBusinessObjectWrapper(lookupDll4));

				if (MaximumPoolSize >= 5)
					AddEngineDll(new LegacyBusinessObjectWrapper(lookupDll5));

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
		/// Return an LBOPool instance.
		/// Implements the singleton pattern.
		/// </summary>
		/// <returns></returns>
		public static LBOPool GetLBOPool() 
		{
			if (lboPool == null)
			{
				lock ( typeof(LBOPool) ) 
				{
					if (lboPool == null) 
					{
						lboPool = new LBOPool();
					}
				}
			}

			return lboPool;
		}

	}
}
