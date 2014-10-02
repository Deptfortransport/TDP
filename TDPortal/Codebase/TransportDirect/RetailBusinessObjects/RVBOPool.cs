//********************************************************************************
//NAME         : RVBOPool.cs
//AUTHOR       : ATOS Origin
//DATE CREATED : 17/02/2005
//DESCRIPTION  : Provides Pool for RVBO Objects.
//             : 
//DESIGN DOC   : DN063 RVBO Data Import
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/RetailBusinessObjects/RVBOPool.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:46:22   mturner
//Initial revision.
//
//   Rev 1.5   Apr 23 2005 12:39:50   RPhilpott
//Allow for RVBO reinitialisation after NRS comms error.
//Resolution for 2301: PT - RVBO discontinues comms with NRS after error
//
//   Rev 1.4   Mar 30 2005 08:54:32   rscott
//Updated with fixes found from running FXCop
//
//   Rev 1.3   Mar 24 2005 15:20:26   rscott
//File updated with code review actions - IR1937
//
//   Rev 1.2   Feb 17 2005 11:23:56   rscott
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

using Logger = System.Diagnostics.Trace;


namespace TransportDirect.UserPortal.RetailBusinessObjects
{
	/// <summary>
	/// Provides Pool for RVBO Objects.
	/// </summary>
	public sealed class RVBOPool : RetailBusinessObjectPool
	{
		
		/// <summary>
		/// The Retail Business Object Dll name 
		/// used by business object engines in the pool.
		/// Filename convention:
		/// FBRV0012 is:
		/// FB(Prefix for all Dll types) RV (Reservations Business Object) 
		/// 0(component number) 01(major version) 2(32bit)
		/// </summary>
		/// <remarks>Also the entry point name into copies of this dll.</remarks>

		private const string DllName = "FBRV0012";
		private const string ReinitialisationPropertyName = "RetailBusinessObjects.RVBO.ReinitialisationSeconds";


		private TDDateTime reinitAfterCommsFailureDue;
		private bool reinitAfterCommsFailureNeeded = false;

		private int reinitAfterCommsFailureExpirySeconds = 0;

		/// <summary>
		/// The maximum number of RVBOs that may be added to pool.
		/// This must match the number of BO engines defined below.
		/// </summary>
		public const int MaximumPoolSize = 5;

		// Define Business Object Engine Implementations
		// Each of these implementations is created by copying the master dll FBRV0012.dll
		[DllImport("FBRV0012_1.dll", EntryPoint=DllName)]
		static extern int restrictionsDll1(string inputHeader, byte[] outputHeader, string inputBody, byte[] outputArray);

		[DllImport("FBRV0012_2.dll", EntryPoint=DllName)]
		static extern int restrictionsDll2(string inputHeader, byte[] outputHeader, string inputBody, byte[] outputArray);

		[DllImport("FBRV0012_3.dll", EntryPoint=DllName)]
		static extern int restrictionsDll3(string inputHeader, byte[] outputHeader, string inputBody, byte[] outputArray);

		[DllImport("FBRV0012_4.dll", EntryPoint=DllName)]
		static extern int restrictionsDll4(string inputHeader, byte[] outputHeader, string inputBody, byte[] outputArray);

		[DllImport("FBRV0012_5.dll", EntryPoint=DllName)]
		static extern int restrictionsDll5(string inputHeader, byte[] outputHeader, string inputBody, byte[] outputArray);
		
		/// <summary>
		/// Singleton pattern
		/// </summary>
		private static RVBOPool rvboPool = null;

		/// <summary>
		/// Validates unique properties of RVBO Pools.
		/// </summary>
		override protected void ValidateProperties(ArrayList errors)
		{
			// no unique properties to validate
		}

		/// <summary>
		/// Private Constructor to support singleton pattern.
		/// Creates the objects to pool.
		/// </summary>
		private RVBOPool() : base(	Keys.RVBOInterfaceVersion,
									Keys.RVBOObjectId,
									Keys.RVBOPoolSize,
									MaximumPoolSize,
									Keys.RVBOTimeoutDuration,
									Keys.RVBOTimeoutCheckFrequency,
									Keys.RVBOTimeoutChecking,
									DllName + ".ini",
									Keys.RVBOMinimumPoolSize)
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

			try
			{
				reinitAfterCommsFailureExpirySeconds = Int32.Parse(Properties.Current[ReinitialisationPropertyName]);			
			}			
			catch (Exception)
			{
				Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error,
													"Property " + ReinitialisationPropertyName + " not found or invalid, defaulting to zero"));
			
				reinitAfterCommsFailureExpirySeconds = 0;	// this turns off reinitialisation ...
			}
					
		}

		/// <summary>
		/// Return an RVBOPool instance.
		/// Implements the singleton pattern.
		/// </summary>
		/// <returns></returns>
		public static RVBOPool GetRVBOPool() 
		{
			if (rvboPool == null)
			{
				lock ( typeof(RVBOPool) ) 
				{
					if (rvboPool == null) 
					{
						rvboPool = new RVBOPool();
					}
				}
			}

			return rvboPool;
		}


		protected override bool ReinitialisationDue
		{
			get 
			{	
				if	(!reinitAfterCommsFailureNeeded)
				{
					return false;
				}
				else
				{
					bool isDue = false;
								
					lock (typeof(RVBOPool))
					{
						isDue = (reinitAfterCommsFailureNeeded && reinitAfterCommsFailureDue < TDDateTime.Now);
	
						if	(isDue && TDTraceSwitch.TraceVerbose)
						{
							Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose,	"RVBO reinitialisation now due"));
						}
					}
					
					return isDue;
				}
			}
		}


		public void SetReinitialisationNeeded()
		{
			if	(!reinitAfterCommsFailureNeeded)
			{
				lock (typeof(RVBOPool))
				{
					if	(reinitAfterCommsFailureExpirySeconds > 0)		// always true unless property missing
					{
						reinitAfterCommsFailureNeeded = true;
						reinitAfterCommsFailureDue = new TDDateTime(DateTime.Now + new TimeSpan(0, 0, 0, reinitAfterCommsFailureExpirySeconds));
	
						if	(TDTraceSwitch.TraceVerbose)
						{
							Logger.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose,
								"RVBO reinitialisation scheduled for " + reinitAfterCommsFailureDue.ToString("yyyyMMdd HHmmss")));
						}
					}
				}
			}
		}

		protected override void ResetReinitialisationNeeded()
		{
			lock (typeof(RVBOPool))
			{
				reinitAfterCommsFailureNeeded = false;
			}
		}


		/// <summary>
		/// Adds a dll to be used by the engine of a reservations business object.
		/// Only a 'single' business object will use the engine using this dll
		/// since retail business object dlls only support single threading.
		/// </summary>
		/// <param name="dllWrapper">
		/// Dll wrapper used to run an engine.
		/// </param>
		/// <exception cref="TDException">
		/// Error when adding the engine.
		/// </exception>
		protected override void AddEngineDll(LegacyBusinessObjectWrapper dllWrapper)
		{
			// Check that maximum pool size has not been exceeded.
			if (freeEngines.Count == this.maxPoolSize)
				throw new TDException(Messages.Pool_MaximumPoolSizeExceeded, false, TDExceptionIdentifier.PRHMaximumPoolSizeExceeded);

			// Create an engine based on the dll wrapper passed in.
			if (freeEngines.Count < this.poolSizeNum)
			{
				try
				{
					// Use the count number as the engines id. (Since this will be unique.)
					Engine engine = new RVBOEngine(freeEngines.Count,
						dllWrapper,
						this.objectId, 
						this.interfaceVersionNum,
						this.dataSequenceNumber,
						this.engTimeoutDuration);

					// Use the engines id as the collection key, 
					// instead of a hash code (since low volume of engines will be stored).
					freeEngines.Add(engine.Id, engine);
				
				}
				catch (ArgumentNullException argumentNullException)
				{
					throw new TDException(Messages.Pool_EngineAddFailed, argumentNullException, false, TDExceptionIdentifier.PRHEngineAddFailed);
				}
				catch (NotSupportedException notSupportedException)
				{
					throw new TDException(Messages.Pool_EngineAddFailed, notSupportedException, false, TDExceptionIdentifier.PRHEngineAddFailed);
				}
			}
		}


	}
}
