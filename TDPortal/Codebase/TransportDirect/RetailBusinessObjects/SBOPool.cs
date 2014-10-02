//********************************************************************************
//NAME         : SBOPool.cs
//AUTHOR       : ATOS Origin
//DATE CREATED : 17/02/2005
//DESCRIPTION  : Provides Pool for SBO Objects.
//             : 
//DESIGN DOC   : DN064 SBO Data Import
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/RetailBusinessObjects/SBOPool.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:46:22   mturner
//Initial revision.
//
//   Rev 1.4   Mar 30 2005 08:54:34   rscott
//Updated with fixes found from running FXCop
//
//   Rev 1.3   Mar 24 2005 15:20:26   rscott
//File updated with code review actions - IR1937
//
//   Rev 1.2   Feb 17 2005 11:24:00   rscott
//Document Information Added
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
	/// Provides Pool for SBO Objects
	/// </summary>
	public sealed class SBOPool : RetailBusinessObjectPool
	{

		/// <summary>
		/// The Retail Business Object Dll name 
		/// used by business object engines in the pool.
		/// Filename convention:
		/// FBSU0012 is:
		/// FB(Prefix for all Dll types) SU (Suppliments Business Object) 
		/// 0(component number) 01(major version) 2(32bit)
		/// </summary>
		/// <remarks>Also the entry point name into copies of this dll.</remarks>

		private const string DllName = "FBSU0022";

		/// <summary>
		/// The maximum number of SBOs that may be added to pool.
		/// This must match the number of BO engines defined below.
		/// </summary>
		public const int MaximumPoolSize = 5;

		// Define Business Object Engine Implementations
		// Each of these implementations is created by copying the master dll FBSU0022.dll
		[DllImport("FBSU0022_1.dll", EntryPoint=DllName)]
		static extern int restrictionsDll1(string inputHeader, byte[] outputHeader, string inputBody, byte[] outputArray);

		[DllImport("FBSU0022_2.dll", EntryPoint=DllName)]
		static extern int restrictionsDll2(string inputHeader, byte[] outputHeader, string inputBody, byte[] outputArray);

		[DllImport("FBSU0022_3.dll", EntryPoint=DllName)]
		static extern int restrictionsDll3(string inputHeader, byte[] outputHeader, string inputBody, byte[] outputArray);

		[DllImport("FBSU0022_4.dll", EntryPoint=DllName)]
		static extern int restrictionsDll4(string inputHeader, byte[] outputHeader, string inputBody, byte[] outputArray);

		[DllImport("FBSU0022_5.dll", EntryPoint=DllName)]
		static extern int restrictionsDll5(string inputHeader, byte[] outputHeader, string inputBody, byte[] outputArray);
		
		/// <summary>
		/// Singleton pattern
		/// </summary>
		private static SBOPool sboPool = null;

		/// <summary>
		/// Validates unique properties of SBO Pools.
		/// </summary>
		override protected void ValidateProperties(ArrayList errors)
		{
			// no unique properties to validate
		}

		/// <summary>
		/// Private Constructor to support singleton pattern.
		/// Creates the objects to pool.
		/// </summary>
		private SBOPool() : base(	Keys.SBOInterfaceVersion,
									Keys.SBOObjectId,
									Keys.SBOPoolSize,
									MaximumPoolSize,
									Keys.SBOTimeoutDuration,
									Keys.SBOTimeoutCheckFrequency,
									Keys.SBOTimeoutChecking,
									DllName + ".ini",
									Keys.SBOMinimumPoolSize)
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
		/// Return an SBOPool instance.
		/// Implements the singleton pattern.
		/// </summary>
		/// <returns></returns>
		public static SBOPool GetSBOPool() 
		{
			if (sboPool == null)
			{
				lock ( typeof(SBOPool) ) 
				{
					if (sboPool == null) 
					{
						sboPool = new SBOPool();
					}
				}
			}

			return sboPool;
		}
	}
}
