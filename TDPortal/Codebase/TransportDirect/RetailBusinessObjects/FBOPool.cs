//********************************************************************************
//NAME         : FBOPool.cs
//AUTHOR       : SchlumbergerSema
//DATE CREATED : 10/05/2003
//DESCRIPTION  : 
//             : 
//DESIGN DOC   : DD034 Retail Pricing
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/RetailBusinessObjects/FBOPool.cs-arc  $
//
//   Rev 1.1   Jan 11 2009 17:09:02   mmodi
//Updated following introduction of ZPBO pool
//Resolution for 5210: CCN487 - ZPBO Implementation workstream
//
//   Rev 1.0   Nov 08 2007 12:46:06   mturner
//Initial revision.
//
//   Rev 1.10   Oct 28 2003 20:04:52   geaton
//Changes to support housekeeping and timeout functionality.
//
//   Rev 1.9   Oct 22 2003 09:19:12   geaton
//Added housekeeping support to business objects.
//
//   Rev 1.8   Oct 21 2003 15:22:46   geaton
//Changes to support business object timeout functionality.
//
//   Rev 1.7   Oct 17 2003 13:13:20   geaton
//Added initialisation handling to allow progress if only some engines fail to initialise.
//
//   Rev 1.6   Oct 16 2003 10:41:10   geaton
//Added exception handling.
//
//   Rev 1.5   Oct 15 2003 21:44:38   geaton
//Added check of error codes.
//
//   Rev 1.4   Oct 15 2003 21:33:56   geaton
//Added calls to base class.
//
//   Rev 1.3   Oct 15 2003 14:42:44   geaton
//Moved common functionality to base class.
//
//   Rev 1.2   Oct 13 2003 15:29:18   CHosegood
//Added DEBUG and converted to use BusinessObject instead of FBO
//
//   Rev 1.1   Oct 08 2003 13:53:04   acaunt
//Performs a check on the error occured during initialisation to see if it has already been initialisated before throwing an exception.
//
//   Rev 1.0   Oct 08 2003 11:46:30   CHosegood
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
    /// Provides pool of FBO objects.
    /// </summary>
    public sealed class FBOPool : RetailBusinessObjectPool
    {
		/// <summary>
		/// The Retail Business Object Dll name 
		/// used by business object engines in the pool.
		/// Filename convention:
		/// FBFA0012 is:
		/// FB(Prefix for all Dll types) FA (Fares Business Object) 
		/// 0(component number) 01(major version) 2(32bit)
		/// </summary>
		/// <remarks>Also the entry point name into copies of this dll.</remarks>
		private const string DllName = "FBFA0012";

		/// <summary>
		/// The maximum number of FBOs that may be added to pool.
		/// This must match the number of BO engine implementations defined below.
		/// </summary>
		static public readonly int MaximumPoolSize = 5;

		// Define Business Object Engine Implementations
		// Each of these implementations is created by copying the master dll FBFA0012.dll
		[DllImport("FBFA0012_1.dll", EntryPoint=DllName)]
        static extern int faresDll1(string inputHeader, byte[] outputHeader, string inputBody, byte[] outputArray);
		
		[DllImport("FBFA0012_2.dll", EntryPoint=DllName)]
        static extern int faresDll2(string inputHeader, byte[] outputHeader, string inputBody, byte[] outputArray);

		[DllImport("FBFA0012_3.dll", EntryPoint=DllName)]
        static extern int faresDll3(string inputHeader, byte[] outputHeader, string inputBody, byte[] outputArray);

		[DllImport("FBFA0012_4.dll", EntryPoint=DllName)]
        static extern int faresDll4(string inputHeader, byte[] outputHeader, string inputBody, byte[] outputArray);

		[DllImport("FBFA0012_5.dll", EntryPoint=DllName)]
        static extern int faresDll5(string inputHeader, byte[] outputHeader, string inputBody, byte[] outputArray);
		
		
        /// <summary>
        /// Singleton pattern
        /// </summary>
        private static FBOPool fboPool = null;

        /// <summary>
		/// Validates unique properties of FBO Pools.
		/// </summary>
		override protected void ValidateProperties(ArrayList errors)
		{
			// Nothing to do as no unique properties to validate.
		}

        /// <summary>
        /// Private Constructor to support singleton pattern.
        /// Creates the objects to pool.
        /// </summary>
        private FBOPool(bool initialiseEngines) : base(Keys.FBOInterfaceVersion,
								 Keys.FBOObjectId,
								 Keys.FBOPoolSize,
								 MaximumPoolSize,
								 Keys.FBOTimeoutDuration,
								 Keys.FBOTimeoutCheckFrequency,
								 Keys.FBOTimeoutChecking,
								 DllName + ".ini",
								 Keys.FBOMinimumPoolSize)
        {     

			try
			{
				if (MaximumPoolSize >= 1)
					AddEngineDll(new LegacyBusinessObjectWrapper(faresDll1));

				if (MaximumPoolSize >= 2)
					AddEngineDll(new LegacyBusinessObjectWrapper(faresDll2));

				if (MaximumPoolSize >= 3)
					AddEngineDll(new LegacyBusinessObjectWrapper(faresDll3));

				if (MaximumPoolSize >= 4)
					AddEngineDll(new LegacyBusinessObjectWrapper(faresDll4));

				if (MaximumPoolSize >= 5)
					AddEngineDll(new LegacyBusinessObjectWrapper(faresDll5));

			}
			catch (TDException tdException)
			{
				throw tdException;
			}

			try
			{
                Initialise(initialiseEngines);
			}
			catch (TDException tdException)
			{
				throw tdException;
			}

        }

        /// <summary>
        /// Return an FBOPool instance.
        /// Implements the singleton pattern.
        /// </summary>
        /// <returns></returns>
        public static FBOPool GetFBOPool() 
        {
			if (fboPool == null)
			{
                lock ( typeof(FBOPool) ) 
				{
					if (fboPool == null) 
					{
                        fboPool = new FBOPool(!ZPBOPool.UseZPBO());
					}
				}
			}

            return fboPool;
        }

        /// <summary>
        /// Override the base PerformHousekeeping method to add specific handling for the ZPBOPool.
        /// If FBO housekeeping is done, then need to restart the ZPBOPool
        /// </summary>
        /// <param name="stateInfo"></param>
        protected override void PerformHousekeeping(Object stateInfo)
        {
            base.PerformHousekeeping(stateInfo);

            if (ZPBOPool.UseZPBO())
            {
                // PerformHousekeeping is only ever called when a Housekeeping request was made.
                // The status will therefore have changed to HousekeepingStatusId.InProgress. 
                // Only when the housekeep has been attempted will the status have changed back to Idle.
                // Therefore MUST RESTART THE ZPBO following an attempt (sucess or fail) at Housekeeping on FBO, 
                // because this would have stopped the FBO dlls which ZPBO relies on (ZPBO internally starts the FBO dlls)

                if ((this.HousekeepingStatus == HousekeepingStatusId.Idle) && (this.HousekeepingCompleted))
                {
                    // Close open ZPBO engines (ensures when we create new instance they start correctly)
                    ZPBOPool.GetZPBOPool().TerminateEngines(true);

                    // Safely dispose of the old ZPBO
                    ZPBOPool.GetZPBOPool().Dispose(true);

                    // Destroy the current instance
                    ZPBOPool.DestroyZPBOPool();

                    // Start a new instance of ZPBO
                    ZPBOPool.GetZPBOPool();

                    // Reset the housekeeping flag to prevent initialisation of ZPBO occuring here again
                    this.HousekeepingCompleted = false;
                }
            }
        }
        
    }
}
