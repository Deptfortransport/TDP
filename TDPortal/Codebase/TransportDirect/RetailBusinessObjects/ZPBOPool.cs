// *********************************************** 
// NAME			: ZPBOPool.cs
// AUTHOR		: Mitesh Modi
// DATE CREATED	: 22/12/2008
// DESCRIPTION	: Class which contains the Zonal Pointers Business Object (ZPBO) dll instances 
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/RetailBusinessObjects/ZPBOPool.cs-arc  $
//
//   Rev 1.0   Jan 11 2009 17:16:52   mmodi
//Initial revision.
//Resolution for 5210: CCN487 - ZPBO Implementation workstream
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.PropertyService.Properties;

namespace TransportDirect.UserPortal.RetailBusinessObjects
{
    /// <summary>
    /// Provides pool of ZPBO objects.
    /// </summary>
    public sealed class ZPBOPool : RetailBusinessObjectPool
    {
        /// <summary>
        /// The Retail Business Object Dll name 
        /// used by business object engines in the pool.
        /// Filename convention:
        /// fbjc0012 is:
        /// fb(Prefix for all Dll types) jc (Zonal Pointers Business Object) 
        /// 0(component number) 01(major version) 2(32bit)
        /// </summary>
        /// <remarks>Also the entry point name into copies of this dll.</remarks>
        private const string DllName = "FBJC0012";

        /// <summary>
        /// The maximum number of ZPBOs that may be added to pool.
        /// This must match the number of BO Engine Implementations defined below.
        /// </summary>
        static public readonly int MaximumPoolSize = 5;

        // Define Business Object Engine Implementations
        // Each of these implementations is created by copying the master dll fbjc0012.dll
        [DllImport("FBJC0012_1.dll", EntryPoint = DllName)]
        static extern int zonalPointersDll1(string inputHeader, byte[] outputHeader, string inputBody, byte[] outputArray);

        [DllImport("FBJC0012_2.dll", EntryPoint = DllName)]
        static extern int zonalPointersDll2(string inputHeader, byte[] outputHeader, string inputBody, byte[] outputArray);

        [DllImport("FBJC0012_3.dll", EntryPoint = DllName)]
        static extern int zonalPointersDll3(string inputHeader, byte[] outputHeader, string inputBody, byte[] outputArray);

        [DllImport("FBJC0012_4.dll", EntryPoint = DllName)]
        static extern int zonalPointersDll4(string inputHeader, byte[] outputHeader, string inputBody, byte[] outputArray);

        [DllImport("FBJC0012_5.dll", EntryPoint = DllName)]
        static extern int zonalPointersDll5(string inputHeader, byte[] outputHeader, string inputBody, byte[] outputArray);

        /// <summary>
        /// Singleton pattern
        /// </summary>
        private static ZPBOPool zpboPool = null;

        /// <summary>
        /// Singleton pattern, flag to indicate if the ZPBOPool should be made null
        /// </summary>
        private static bool destroyZpboPool = false;

        /// <summary>
        /// Property string used to check if ZPBO should be used
        /// </summary>
        private static readonly string PROPERTY_ZPBO_BASED_FARE_QUERY = "RetailBusinessObjects.ZPBO.Switch";

        /// <summary>
        /// Validates unique properties of ZPBO Pools.
        /// </summary>
        override protected void ValidateProperties(ArrayList errors)
        {
            // Nothing to do as no unique properties to validate.
        }

        /// <summary>
        /// Private Constructor to support singleton pattern.
        /// Creates the objects to pool.
        /// </summary>
        private ZPBOPool() : base(Keys.ZPBOInterfaceVersion,
                                 Keys.ZPBOObjectId,
                                 Keys.ZPBOPoolSize,
								 MaximumPoolSize,
                                 Keys.ZPBOTimeoutDuration,
                                 Keys.ZPBOTimeoutCheckFrequency,
                                 Keys.ZPBOTimeoutChecking,
								 DllName + ".ini",
                                 Keys.ZPBOMinimumPoolSize)
        {     

			try
			{
				if (MaximumPoolSize >= 1)
					AddEngineDll(new LegacyBusinessObjectWrapper(zonalPointersDll1));

				if (MaximumPoolSize >= 2)
                    AddEngineDll(new LegacyBusinessObjectWrapper(zonalPointersDll2));

				if (MaximumPoolSize >= 3)
                    AddEngineDll(new LegacyBusinessObjectWrapper(zonalPointersDll3));

				if (MaximumPoolSize >= 4)
                    AddEngineDll(new LegacyBusinessObjectWrapper(zonalPointersDll4));

				if (MaximumPoolSize >= 5)
                    AddEngineDll(new LegacyBusinessObjectWrapper(zonalPointersDll5));

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
        /// Method return true if the use ZPBO switch is turned on
        /// </summary>
        public static bool UseZPBO()
        {
            bool useZPBO = false;

            // Set the useZPBO flag
            try
            {
                useZPBO = bool.Parse(Properties.Current[PROPERTY_ZPBO_BASED_FARE_QUERY]);
            }
            catch (TDException tdException)
            {
                string message = "ZPBO switch - missing property [" + PROPERTY_ZPBO_BASED_FARE_QUERY + "] in Property database. ";
                Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, message, tdException));

                useZPBO = false;
            }

            return useZPBO;
        }

        /// <summary>
        /// Return an ZPBOPool instance.
        /// Implements the singleton pattern.
        /// </summary>
        /// <returns></returns>
        public static ZPBOPool GetZPBOPool() 
        {
            if (destroyZpboPool)
            {
                // A request was made to remove the existing pool and create a new one
                lock (typeof(ZPBOPool))
                {
                    if (destroyZpboPool)
                    {
                        zpboPool = null;

                        // Reset the destroy flag
                        destroyZpboPool = false;
                    }
                }
            }

			if (zpboPool == null)
			{
				lock ( typeof(ZPBOPool) ) 
				{
					if (zpboPool == null) 
					{
						zpboPool = new ZPBOPool();
					}
				}
			}

            return zpboPool;
        }

        /// <summary>
        /// Sets the flag to make ZPBOPool null. 
        /// The next call to GetZPBOPool() will then create a new instance of the ZPBOPool
        /// </summary>
        public static void DestroyZPBOPool()
        {
            lock (typeof(ZPBOPool))
            {
                destroyZpboPool = true;
            }
        }


    }
}
