//********************************************************************************
//NAME         : TestPools.cs
//AUTHOR       : SchlumbergerSema
//DATE CREATED : 21/10/2003
//DESCRIPTION  : NUnit test script for testing business object pools.
//				 Note the following timeout values must be used for these tests:
//				 RBO - 3s FBO - 3s RBO - 3s 
//DESIGN DOC   : DD034 Retail Pricing
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/RetailBusinessObjects/Test/TestPools.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:46:46   mturner
//Initial revision.
//
//   Rev 1.17   Apr 25 2005 21:14:08   RPhilpott
//Chnages to LBO calls, and removal of rendundant code.
//Resolution for 2328: PT - fares between Three Bridges and Victoria
//
//   Rev 1.16   Apr 15 2005 09:51:16   rscott
//DEL 7 Code Review - further changes
//
//   Rev 1.15   Mar 30 2005 09:15:50   rscott
//Updated after running FXCop
//
//   Rev 1.14   Mar 24 2005 15:39:50   rscott
//Code Review Ammendments
//
//   Rev 1.13   Feb 21 2005 15:48:18   rscott
//RVBO and SBO testing revised
//
//   Rev 1.12   Feb 17 2005 15:38:30   rscott
//Updated RVBO and SBO calls added - testing needs to be completed.
//
//   Rev 1.11   Feb 08 2005 15:59:00   RScott
//Assertions changed to Asserts
//
//   Rev 1.10   Feb 08 2005 10:07:32   RScott
//Assertion changed to Assert
//
//   Rev 1.9   May 14 2004 15:21:14   GEaton
//Updates following testing of c++ RBO dll.
//
//   Rev 1.8   Feb 04 2004 17:39:12   geaton
//Updated timetable specific data so tests will work against the latest timetable data. Added comments to highlight that this will need to be done the next time that timetables change.
//
//   Rev 1.7   Dec 15 2003 15:26:34   geaton
//Added additional multi-threading test for RBO. (and indirectly FBO)
//
//   Rev 1.6   Nov 27 2003 21:45:48   geaton
//Added test for housekeeping failure handling.
//
//   Rev 1.5   Nov 20 2003 13:46:30   geaton
//Added housekeep test for FBO after successful testing with CH.
//
//   Rev 1.4   Oct 30 2003 08:57:22   geaton
//Added check that BO functions ok after housekeeping performed.
//
//   Rev 1.3   Oct 29 2003 19:47:54   geaton
//Ignored test that requires manual setup.
//
//   Rev 1.2   Oct 28 2003 20:05:12   geaton
//Changes to support housekeeping and timeout functionality.
//
//   Rev 1.1   Oct 22 2003 09:19:22   geaton
//Added housekeeping support to business objects.
//
//   Rev 1.0   Oct 21 2003 15:22:10   geaton
//Initial Revision

using NUnit.Framework;

using System;
using System.Text;
using System.Diagnostics;
using System.Collections;
using System.Reflection;
using System.Threading;
using System.IO;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.UserPortal.PricingMessages;

namespace TransportDirect.UserPortal.RetailBusinessObjects
{

	#region "WorkerThreadClass"

	/// <summary>
	/// Class used to support multi-threading tests.
	/// </summary>
	class WorkerThread 
	{
		private RetailBusinessObjectPool pool;
		private string id;
		public string Id
		{
			get{return id;}
		}

		public WorkerThread(string id, RetailBusinessObjectPool pool)
		{
			bo = null;
			this.id = id + pool.ToString();
			this.pool = pool;
		}

		private BusinessObject bo;
		public BusinessObject Bo
		{
			get {return bo;}
		}

		public void ReleaseBusinessObject()
		{
			try
			{
				Console.WriteLine("WorkerThread: " + id + " before release: " + DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fff"));
				pool.Release(ref bo);
				Console.WriteLine("WorkerThread: " + id + " afer release: " + DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fff"));
			}
			catch (Exception exception)
			{
				throw new TDException("WorkerThread failed to release.", exception, false, TDExceptionIdentifier.PRHBOReleaseFailed);
			}
		}

		public void GetBusinessObject()
		{
			try
			{
				Console.WriteLine("WorkerThread: " + id + " before get: " + DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fff"));
				bo = pool.GetInstance();
				Console.WriteLine("WorkerThread: " + id + " after get: " + DateTime.Now.ToString("yyyy-MM-ddTHH:mm:ss.fff"));
			}
			catch (Exception exception)
			{
				throw new TDException("WorkerThread failed to get.", exception, false, TDExceptionIdentifier.PRHBOGetIntanceFailed);
			}
		}

	}

	#endregion

	//*******************************************************************************
	//****************************         TESTS           **************************
	//*******************************************************************************

	/// <summary>
	/// NUnit test harness for TestPools.
	/// </summary>
	[TestFixture]
	public class TestPools
	{
		public TestPools() 
		{

		}

		#region "Setup/Teardown"

		[SetUp] 
		public void SetUp() 
		{
			// Intialise property service and td logging service. In production this will be performed in global.asax.
			// NB RBOServiceInitialisation will only be called once per test suite run despite being in SetUp.

			TDServiceDiscovery.Init(new RBOServiceInitialisation());			
			
			Assert.IsTrue(RBOPool.GetRBOPool().NumberFree == RBOPool.GetRBOPool().PoolSize,
				"SetUp: RBO Free Pool Not full. Note assert may have occurred before reaching SetUp");
			Assert.IsTrue(FBOPool.GetFBOPool().NumberFree == FBOPool.GetFBOPool().PoolSize,
				"SetUp: FBO Free Pool Not full. Note assert may have occurred before reaching SetUp");
			Assert.IsTrue(LBOPool.GetLBOPool().NumberFree == LBOPool.GetLBOPool().PoolSize,
				"SetUp: LBO Free Pool Not full. Note assert may have occurred before reaching SetUp");

			Assert.IsTrue(RVBOPool.GetRVBOPool().NumberFree == RVBOPool.GetRVBOPool().PoolSize,
				"SetUp: RVBO Free Pool Not full. Note assert may have occurred before reaching SetUp");
			Assert.IsTrue(SBOPool.GetSBOPool().NumberFree == SBOPool.GetSBOPool().PoolSize,
				"SetUp: SBO Free Pool Not full. Note assert may have occurred before reaching SetUp");

		}

		[TearDown] 
		public void TearDown()
		{
			if (!RBOPool.GetRBOPool().Disposed)
				Assert.IsTrue(RBOPool.GetRBOPool().NumberFree == RBOPool.GetRBOPool().PoolSize,
					"SetUp: RBO Free Pool Not full. Note assert may have occurred before reaching SetUp");
			if (!FBOPool.GetFBOPool().Disposed)
				Assert.IsTrue(FBOPool.GetFBOPool().NumberFree == FBOPool.GetFBOPool().PoolSize,
					"SetUp: FBO Free Pool Not full. Note assert may have occurred before reaching SetUp");
			if (!LBOPool.GetLBOPool().Disposed)
				Assert.IsTrue(LBOPool.GetLBOPool().NumberFree == LBOPool.GetLBOPool().PoolSize, 
					"SetUp: LBO Free Pool Not full. Note assert may have occurred before reaching SetUp");

			if (!RVBOPool.GetRVBOPool().Disposed)
				Assert.IsTrue(RVBOPool.GetRVBOPool().NumberFree == RVBOPool.GetRVBOPool().PoolSize, 
					"SetUp: RVBO Free Pool Not full. Note assert may have occurred before reaching SetUp");
			if (!SBOPool.GetSBOPool().Disposed)
				Assert.IsTrue(SBOPool.GetSBOPool().NumberFree == SBOPool.GetSBOPool().PoolSize, 
					"SetUp: SBO Free Pool Not full. Note assert may have occurred before reaching SetUp");

			TDServiceDiscovery.ResetServiceDiscoveryForTest();

		}

		#endregion

		#region "TestGetSingleInstance()"

		/// <summary>
		/// Test retrieval of a single instance where pool has >0 instances free
		/// </summary>
		[Test] 
		public void TestGetSingleInstance() 
		{
			PoolGetSingleInstance(RBOPool.GetRBOPool());
			PoolGetSingleInstance(FBOPool.GetFBOPool());
			PoolGetSingleInstance(LBOPool.GetLBOPool());
			PoolGetSingleInstance(RVBOPool.GetRVBOPool());
			PoolGetSingleInstance(SBOPool.GetSBOPool());
		}

		private void PoolGetSingleInstance(RetailBusinessObjectPool pool) 
		{
            if (TDTraceSwitch.TraceVerbose) 
            {   
                Trace.Write( new OperationalEvent( TDEventCategory.Infrastructure, TDTraceLevel.Verbose, string.Format( Messages.UnitTestMethodStarting, MethodInfo.GetCurrentMethod().ReflectedType + ":" + MethodInfo.GetCurrentMethod().Name + " Pool:" + pool.ToString())) ); 
            }

			BusinessObject BO = pool.GetInstance();
			Assert.IsNotNull(BO,"Checking retrieved BO is not null");
			try
			{
				BoTest(pool, BO);
			}
			catch (Exception)
			{
				Assert.Fail("Bo failed to function.");
			}

			pool.Release(ref BO);
			Assert.IsNull(BO, "Checking released BO is null");

			// wait for pool to put all engines back in free list
			while (pool.NumberFree != pool.PoolSize);

            if (TDTraceSwitch.TraceVerbose) 
            {   
                Trace.Write( new OperationalEvent( TDEventCategory.Infrastructure, TDTraceLevel.Verbose, string.Format( Messages.UnitTestMethodCompleted, MethodInfo.GetCurrentMethod().ReflectedType + ":" + MethodInfo.GetCurrentMethod().Name + " Pool:" + pool.ToString()) )); 
            }
		}

		#endregion

		// Delegate, defines signature of RBO worker method to execute asynchronously
		public delegate void RBOWorkerDelegate(ValidateAdultFaresRequest validateAdultFaresRequest);

		#region "TestSingleInstanceDoesNotTimeOut()"

		/// <summary>
		/// Test retrieval of a single instance where pool has >0 instances free,
		/// Tests that the single instance can be used for longer than the engine 
		/// time out duration, where the bo calls made during the period are less than
		/// the engine time out duration.
		/// </summary>
		[Test] 
		public void TestSingleInstanceDoesNotTimeOut() 
		{
			PoolGetSingleInstanceDoesNotTimeOut(RBOPool.GetRBOPool());
			PoolGetSingleInstanceDoesNotTimeOut(FBOPool.GetFBOPool());
			PoolGetSingleInstanceDoesNotTimeOut(LBOPool.GetLBOPool());
			PoolGetSingleInstanceDoesNotTimeOut(RVBOPool.GetRVBOPool());
			PoolGetSingleInstanceDoesNotTimeOut(SBOPool.GetSBOPool());
		}


		private void PoolGetSingleInstanceDoesNotTimeOut(RetailBusinessObjectPool pool) 
		{
			if (TDTraceSwitch.TraceVerbose) 
			{   
				Trace.Write( new OperationalEvent( TDEventCategory.Infrastructure, TDTraceLevel.Verbose, string.Format( Messages.UnitTestMethodStarting, MethodInfo.GetCurrentMethod().ReflectedType + ":" + MethodInfo.GetCurrentMethod().Name + " Pool:" + pool.ToString())) ); 
			}
			
			BusinessObject BO = pool.GetInstance();
			Assert.IsNotNull(BO, "Checking retrieved BO is not null");

			DateTime beforeFirstCall = DateTime.Now;
			
			try
			{
				// This tests that the bo does not timeout for a duration equal to the timeout period.
				while ((DateTime.Now - beforeFirstCall) <= pool.EngineTimeoutDuration)
					BoTest(pool, BO); // Note that the timeout period is reset on every call to the BO.
			}
			catch (Exception)
			{
				Assert.Fail("Bo failed to function.");
			}

			pool.Release(ref BO);
			Assert.IsNull(BO, "Checking released BO is null");
			
			// wait for pool to put all engines back in free list
			while (pool.NumberFree != pool.PoolSize);

			if (TDTraceSwitch.TraceVerbose) 
			{   
				Trace.Write( new OperationalEvent( TDEventCategory.Infrastructure, TDTraceLevel.Verbose, string.Format( Messages.UnitTestMethodCompleted, MethodInfo.GetCurrentMethod().ReflectedType + ":" + MethodInfo.GetCurrentMethod().Name + " Pool:" + pool.ToString()) )); 
			}
		}

		#endregion

		#region "TestGetAllInstances()"

		/// <summary>
		/// Test retrieval of all available instances in pool.
		/// </summary>
		[Test] 
		public void TestGetAllInstances()
		{
			PoolGetAllInstances(RBOPool.GetRBOPool());
			PoolGetAllInstances(FBOPool.GetFBOPool());
			PoolGetAllInstances(LBOPool.GetLBOPool());
			PoolGetAllInstances(RVBOPool.GetRVBOPool());
			PoolGetAllInstances(SBOPool.GetSBOPool());
		}

		private void PoolGetAllInstances(RetailBusinessObjectPool pool) 
		{
			int poolSize = pool.PoolSize;

			if (TDTraceSwitch.TraceVerbose)
			{   
				Trace.Write( new OperationalEvent( TDEventCategory.Infrastructure, TDTraceLevel.Verbose, string.Format( Messages.UnitTestMethodStarting, MethodInfo.GetCurrentMethod().ReflectedType + ":" + MethodInfo.GetCurrentMethod().Name + " Pool:" + pool.ToString())) ); 
			}

			int freeInstances = poolSize;
			BusinessObject[] instances = new BusinessObject[poolSize];
			BusinessObject BO = null;
			
			for (int count=0; count < freeInstances; count++)
			{
				BO = pool.GetInstance();
				instances[count] = BO;
			}

			for (int count=0; count < freeInstances; count++)
			{
				pool.Release(ref instances[count]);
			}

			// wait for pool to put all engines back in free list
			while (pool.NumberFree != pool.PoolSize);

            if (TDTraceSwitch.TraceVerbose) 
            {   
                Trace.Write( new OperationalEvent( TDEventCategory.Infrastructure, TDTraceLevel.Verbose, string.Format( Messages.UnitTestMethodCompleted, MethodInfo.GetCurrentMethod().ReflectedType + ":" + MethodInfo.GetCurrentMethod().Name + " Pool:" + pool.ToString()) )); 
            }
		}

		#endregion

		#region "TestSingleThreadedBlocking()"

		/// <summary>
		/// Tests that a single request is blocked when pool is empty and
		/// and is serviced correctly when instances become available in pool.
		/// </summary>
		[Test]
		public void TestSingleThreadedBlocking()
		{
			PoolSingleThreadedBlocking(RBOPool.GetRBOPool());
			PoolSingleThreadedBlocking(FBOPool.GetFBOPool());
			PoolSingleThreadedBlocking(LBOPool.GetLBOPool());
			PoolSingleThreadedBlocking(RVBOPool.GetRVBOPool());
			PoolSingleThreadedBlocking(SBOPool.GetSBOPool());
		}

		private void PoolSingleThreadedBlocking(RetailBusinessObjectPool pool)
		{
			int poolSize = pool.PoolSize;

            if (TDTraceSwitch.TraceVerbose) 
            {   
                Trace.Write( new OperationalEvent( TDEventCategory.Infrastructure, TDTraceLevel.Verbose, string.Format( Messages.UnitTestMethodStarting, MethodInfo.GetCurrentMethod().ReflectedType + ":" + MethodInfo.GetCurrentMethod().Name + " Pool:" + pool.ToString())) ); 
            }

			// Stop timeouts so does not intefere with test
			pool.TimeoutChecking = TimeoutCheckingSwitch.Off;

			BusinessObject[] instances = new BusinessObject[poolSize];
			BusinessObject BO = null;
			
			for (int count=0; count < poolSize; count++)
			{
				BO = pool.GetInstance();
				instances[count] = BO;
			}

			Assert.IsTrue(pool.NumberFree == 0);

			// create a thread that will attempt to get an instance from the pool
			WorkerThread worker = new WorkerThread("TestPools.TestSingleThreadedBlocking.", pool);	
			Thread thread = new Thread(new ThreadStart(worker.GetBusinessObject));
			thread.Start();

			// assert that thread is being blocked
			Assert.IsNull(worker.Bo, "BO of blocked thread is not null.");

			// release an instance to allow thread to get instance
			pool.Release(ref instances[0]);

			// wait for thread to complete ie to get instance
			while(pool.NumberFree != 0);

			// thread should now have got an instance
			Assert.IsNotNull(worker.Bo, "Checking retrieved BO of thread is not null");

			// release all instances so that future tests start from total free pool
			worker.ReleaseBusinessObject();
			for (int count=1; count < poolSize; count++)
				pool.Release(ref instances[count]);

			// Start timeout checking for future tests.
			pool.TimeoutChecking = TimeoutCheckingSwitch.On;

			// wait for pool to put all engines back in free list
			while (pool.NumberFree != pool.PoolSize);

            if (TDTraceSwitch.TraceVerbose) 
            {   
                Trace.Write( new OperationalEvent( TDEventCategory.Infrastructure, TDTraceLevel.Verbose, string.Format( Messages.UnitTestMethodCompleted, MethodInfo.GetCurrentMethod().ReflectedType + ":" + MethodInfo.GetCurrentMethod().Name + " Pool:" + pool.ToString()) )); 
            }
		}

		#endregion

		#region "TestMultithreadedBlocking()"
		/// <summary>
		/// Tests blocking (for each pool type) with multiple threads.
		/// NOTE: calls are not made to engines in this test. This test simply takes engines from the pools.
		/// </summary>
		[Test]
		public void TestMultithreadedBlocking()
		{
			PoolMultithreadedBlocking(RBOPool.GetRBOPool());
			PoolMultithreadedBlocking(FBOPool.GetFBOPool());
			PoolMultithreadedBlocking(LBOPool.GetLBOPool());
			PoolMultithreadedBlocking(RVBOPool.GetRVBOPool());
			PoolMultithreadedBlocking(SBOPool.GetSBOPool());
		}
		

		private void PoolMultithreadedBlocking(RetailBusinessObjectPool pool)
		{
			int poolSize = pool.PoolSize;

            if (TDTraceSwitch.TraceVerbose) 
            {   
                Trace.Write( new OperationalEvent( TDEventCategory.Infrastructure, TDTraceLevel.Verbose, string.Format( Messages.UnitTestMethodStarting, MethodInfo.GetCurrentMethod().ReflectedType + ":" + MethodInfo.GetCurrentMethod().Name + " Pool:" + pool.ToString())) ); 
            }
	
			// Stop timeouts so does not intefere with test
			pool.TimeoutChecking = TimeoutCheckingSwitch.Off;
			

			WorkerThread[]  workers = new WorkerThread[poolSize*2];
			Thread[] threads = new Thread[poolSize*2];

			// Create twice as many worker instances as there are instances in the pool
			for (int count=0; count<poolSize*2; count++)
			{
				workers[count] = new WorkerThread("TestPools.TestPoolMultithreadedBlocking." + count.ToString(), pool);	
			}
			
			// Create twice as many threads as there are instances in the pool - associate with worker instances
			for (int count=0; count<poolSize*2; count++)
			{
				threads[count] = new Thread(new ThreadStart(workers[count].GetBusinessObject));
			}
			
			// Start threads
			for (int count=0; count<poolSize*2; count++)
			{
				threads[count].Start();
			}

			// wait until all instances have been requested from pool
			while(pool.NumberFree != 0);

			Assert.IsTrue(pool.NumberFree == 0);
			
			// Release business objects to allow remaining threads to complete.
			// Determine those worker instances that got an instance in first round before releasing
			int[] haveInstance = new int[poolSize];
			int haveInstanceCount = 0;
			for (int count=0; count<poolSize*2; count++)
			{
				if (workers[count].Bo != null)
				{
					haveInstance[haveInstanceCount] = count;
					haveInstanceCount++;
				}
					
			}

			// release - when releasing, waiting threads will attempt to get instance from pool
			for (int count=0; count<poolSize; count++)
			{
				workers[haveInstance[count]].ReleaseBusinessObject();
			}

			// wait until all instances have been requested from pool
			while(pool.NumberFree != 0);

			Assert.IsTrue(pool.NumberFree == 0);

			// release remaining instances from pool
			for (int count=0; count<poolSize*2; count++)
			{
				if (workers[count].Bo != null)
					workers[count].ReleaseBusinessObject();
			}

			Assert.IsTrue(pool.NumberFree == pool.PoolSize);

			// Start timeout checking for future tests.
			pool.TimeoutChecking = TimeoutCheckingSwitch.On;

			// wait for pool to put all engines back in free list
			while (pool.NumberFree != pool.PoolSize);

            if (TDTraceSwitch.TraceVerbose) 
            {   
                Trace.Write( new OperationalEvent( TDEventCategory.Infrastructure, TDTraceLevel.Verbose, string.Format( Messages.UnitTestMethodCompleted, MethodInfo.GetCurrentMethod().ReflectedType + ":" + MethodInfo.GetCurrentMethod().Name + " Pool:" + pool.ToString()) )); 
            }
		}

		#endregion

		#region "TestRBOPoolConcurrentUse()"

		/// <summary>
		/// Tests concurrent use of business objects from the RBO pool
		/// </summary>
		[Test]
		public void TestRBOPoolConcurrentUse()
		{
			RetailBusinessObjectPool pool = RBOPool.GetRBOPool();

			if (TDTraceSwitch.TraceVerbose) 
			{   
				Trace.Write( new OperationalEvent( TDEventCategory.Infrastructure, TDTraceLevel.Verbose, string.Format( Messages.UnitTestMethodStarting, MethodInfo.GetCurrentMethod().ReflectedType + ":" + MethodInfo.GetCurrentMethod().Name + " Pool:" + pool.ToString())) ); 
			}

			Assert.IsTrue(pool.PoolSize >=2,
				"Pool size must be greater than or equal to 2 for test to run");

			// Need to first run FBO to get input to RBO
			FBOPool fboPool = FBOPool.GetFBOPool();
			int outputLength = 8192;
			TestFBO testFBO = new TestFBO();
			PricingRequestDto request = testFBO.BuildSingleRequest();
			FareRequest fareRequest = new FareRequest( fboPool.InterfaceVersion, request, outputLength  );
			BusinessObject fbo = fboPool.GetInstance();
			BusinessObjectOutput output = fbo.Process( fareRequest );	
			fboPool.Release( ref fbo );
			Fares fares = new Fares( output ); // based on output from fbo above
			int adultFaresOutputLength = 329;
			ValidateAdultFaresRequest validateAdultFaresRequest;
			validateAdultFaresRequest = new ValidateAdultFaresRequest(pool.InterfaceVersion, adultFaresOutputLength, request, fares);

			BusinessObject bo1 = null;
			BusinessObject bo2 = null;
			try
			{
				bo1 = pool.GetInstance();
				bo2 = pool.GetInstance();
				BusinessObjectOutput output1 = bo1.Process( validateAdultFaresRequest );
				BusinessObjectOutput output2 = bo2.Process( validateAdultFaresRequest );
				Assert.IsNotNull(output1, "RBO output null.");
				Assert.IsNotNull(output2, "RBO output null.");
			}
			catch (Exception exception)
			{
				Assert.Fail("Exception during concurrent usage of RBO: " + exception.Message);	
			}
			finally
			{
				if (bo1 != null)
					pool.Release(ref bo1);
				if (bo2 != null)
					pool.Release(ref bo2);
			}

			// wait for pool to put all engines back in free list
			while (pool.NumberFree != pool.PoolSize);

			if (TDTraceSwitch.TraceVerbose) 
			{   
				Trace.Write( new OperationalEvent( TDEventCategory.Infrastructure, TDTraceLevel.Verbose, string.Format( Messages.UnitTestMethodCompleted, MethodInfo.GetCurrentMethod().ReflectedType + ":" + MethodInfo.GetCurrentMethod().Name + " Pool:" + pool.ToString()) )); 
			}
		}

		#endregion

		#region "BoTest Support Method"

		/// <summary>
		/// Used to support other tests to ensure that business objects work as expected.
		/// This uses methods defined in other test suites.
		/// Only minimal testing is performed to ensure bo's work as expected.
		/// Exhaustive tests are carried out elsewhere independant of pool functionality.
		/// IMPORTANT!
		/// The timeout configuration values used in the tests should be set to such values
		/// that a time outs will not occur on bo's used in this method. If this is
		/// not the case then test results will be unpredictable.
		/// </summary>
		/// <param name="pool">Pool from which bo was taken</param>
		/// <param name="bo">Bo to test. This is not released.</param>
		private void BoTest(RetailBusinessObjectPool pool, BusinessObject bo)
		{
			if (pool is FBOPool)
			{
				int outputLength = 8192;
				TestFBO testFBO = new TestFBO();
				PricingRequestDto request = testFBO.BuildReturnRequest();
				FareRequest fareRequest = new FareRequest( pool.InterfaceVersion, request, outputLength  );
				BusinessObjectOutput output = bo.Process( fareRequest );
			}
			else if (pool is RBOPool)
			{
				// Need to run FBO to get input to rbo (use same code as above)
				FBOPool fboPool = FBOPool.GetFBOPool();
				int outputLength = 8192;
				TestFBO testFBO = new TestFBO();
				PricingRequestDto request = testFBO.BuildReturnRequest();
				FareRequest fareRequest = new FareRequest( fboPool.InterfaceVersion, request, outputLength  );
				BusinessObject fbo = fboPool.GetInstance();
				BusinessObjectOutput output = fbo.Process( fareRequest );	
				fboPool.Release( ref fbo );

				// test rbo
				Fares fares = new Fares( output ); // based on output from fbo above
				Assert.IsTrue(fares.Item.Count > 0,
					"Fares data not available to test BO - ensure that test data in class TestBusinessObject corresponds with the latest timetable data.");
				int adultFaresOutputLength = 329;
				ValidateAdultFaresRequest validateAdultFaresRequest;
				validateAdultFaresRequest = new ValidateAdultFaresRequest(pool.InterfaceVersion, adultFaresOutputLength, request, fares);
				output = bo.Process( validateAdultFaresRequest );
			}
			else if (pool is LBOPool)
			{
				// Need to run FBO to get input to rbo (use same code as above)
				FBOPool fboPool = FBOPool.GetFBOPool();
				int outputLength = 8192;
				TestFBO testFBO = new TestFBO();
				PricingRequestDto request = testFBO.BuildReturnRequest();
				FareRequest fareRequest = new FareRequest( fboPool.InterfaceVersion, request, outputLength  );
				BusinessObject fbo = fboPool.GetInstance();
				BusinessObjectOutput output = fbo.Process( fareRequest );	
				fboPool.Release( ref fbo );

				Fares fares = new Fares( output );  // based on output from fbo above

				Assert.IsTrue(fares.Item.Count > 0,
					"Fares data not available to test BO - ensure that test data in class TestBusinessObject corresponds with the latest timetable data.");
				
				LookupTransform lookup = new LookupTransform();
				
				LocationDto[] groups = lookup.LookupFareGroups("1444", TDDateTime.Now);

				Assert.IsTrue(groups.Length > 0, "No fare groups found for London Euston(1444) - check LBO data");  
				
			}

		}

		#endregion

		#region "TestSingleEngineTimeout"

		/// <summary>
		/// Tests that engines of each pool time out after a configured duration.
		/// </summary>
		[Test]
		public void TestSingleEngineTimeout()
		{
			SingleEngineTimesout(RBOPool.GetRBOPool());
			SingleEngineTimesout(FBOPool.GetFBOPool());
			SingleEngineTimesout(LBOPool.GetLBOPool());
		}
		
		private void SingleEngineTimesout(RetailBusinessObjectPool pool)
		{
			if (TDTraceSwitch.TraceVerbose) 
			{   
				Trace.Write( new OperationalEvent( TDEventCategory.Infrastructure, TDTraceLevel.Verbose, string.Format( Messages.UnitTestMethodStarting, MethodInfo.GetCurrentMethod().ReflectedType + ":" + MethodInfo.GetCurrentMethod().Name + " Pool: " + pool.ToString())) ); 
			}

			Assert.IsTrue(pool.TimeoutChecking == TimeoutCheckingSwitch.On,
				"Timeout checking not turned on. Must be on for test to complete.");

			BusinessObject bo = pool.GetInstance();
			
			Assert.IsTrue(pool.NumberFree == (pool.PoolSize - 1)); 

			DateTime afterGet = DateTime.Now;

			// Check that instance will function correctly
			try
			{
				BoTest(pool,bo);
			}
			catch (Exception)
			{
				Assert.Fail("Bo failed to work as expected.");
			}

			// Wait for engine to time out (ie for pool to detect timeout engine and put back in pool)
			int waiter = 0;
			while (pool.NumberFree != pool.PoolSize)
				waiter++;

			// Time taken for pool to reach max size should be greater than or equal to the frequency at whcih time out checks are performed
			Assert.IsTrue((DateTime.Now - afterGet) >= pool.TimeoutCheckFrequency);

			// check that exception thrown if using the bo that has a timed out engine
			bool thrown = false;
			try
			{
				BoTest(pool, bo);
			}
			catch (TDException)
			{
				thrown = true;
			}
			
			Assert.IsTrue(thrown, "Call to timed out BO did not throw exception.");
			
			// release bo
			pool.Release(ref bo);

			if (TDTraceSwitch.TraceVerbose) 
			{   
				Trace.Write( new OperationalEvent( TDEventCategory.Infrastructure, TDTraceLevel.Verbose, string.Format( Messages.UnitTestMethodCompleted, MethodInfo.GetCurrentMethod().ReflectedType + ":" + MethodInfo.GetCurrentMethod().Name + " Pool: " + pool.ToString()) )); 
			}
		}

		#endregion
		
		#region "TestSingleThreadedEngineTimeout"

		/// <summary>
		/// Tests engine timeout checking using a single thread, for each pool type.
		/// </summary>
		[Test]
		public void TestSingleThreadedEngineTimeout()
		{
			SingleThreadedEngineTimeout(RBOPool.GetRBOPool());
			SingleThreadedEngineTimeout(FBOPool.GetFBOPool());
			SingleThreadedEngineTimeout(LBOPool.GetLBOPool());
			SingleThreadedEngineTimeout(RVBOPool.GetRVBOPool());
			SingleThreadedEngineTimeout(SBOPool.GetSBOPool());
		}

		private void SingleThreadedEngineTimeout(RetailBusinessObjectPool pool)
		{
			if (TDTraceSwitch.TraceVerbose) 
			{   
				Trace.Write( new OperationalEvent( TDEventCategory.Infrastructure, TDTraceLevel.Verbose, string.Format( Messages.UnitTestMethodStarting, MethodInfo.GetCurrentMethod().ReflectedType + ":" + MethodInfo.GetCurrentMethod().Name + pool.ToString())) ); 
			}

			Assert.IsTrue(pool.TimeoutChecking == TimeoutCheckingSwitch.On, 
				"Timeout checking not turned on. Must be on for test to complete.");

			BusinessObject[] instances = new BusinessObject[pool.PoolSize];

			for (int count=0; count < pool.PoolSize; count++)
			{
				instances[count] = pool.GetInstance();
				Assert.IsNotNull(instances[count], "Checking retrieved BO is not null");
			}

			Assert.IsTrue(pool.NumberFree == 0);

			WorkerThread worker = new WorkerThread("TestPools.TestSingleThreadedTimeout.", pool);	
			Thread thread = new Thread(new ThreadStart(worker.GetBusinessObject));
			thread.Start();
			
			// wait for thread to finish (without explicitly releasing an object!)
			int waiter = 0;
			while(worker.Bo == null)
			{
				Console.WriteLine("Worker thread of id [" + worker.Id + "] waiting for object.");
				waiter++;
			}

			// release thread bo ready for next test
			worker.ReleaseBusinessObject();
	
			// release any other objects that did not get removed following a timeout
			for (int count=0; count < pool.PoolSize; count++)
			{
				pool.Release(ref instances[count]);
			}
		
			// wait for pool to put all engines back in free list
			waiter = 0;
			while (pool.NumberFree != pool.PoolSize)
				waiter++;

			if (TDTraceSwitch.TraceVerbose) 
			{   
				Trace.Write( new OperationalEvent( TDEventCategory.Infrastructure, TDTraceLevel.Verbose, string.Format( Messages.UnitTestMethodCompleted, MethodInfo.GetCurrentMethod().ReflectedType + ":" + MethodInfo.GetCurrentMethod().Name + pool.ToString()) )); 
			}
		}
		
		#endregion
		
		#region "PoolHousekeepingTests"

		/// <summary>
		/// Tests housekeeping on each pool type.
		/// Expected results based on the ini file that the housekeeping process updates.
		/// And also a check against business objects in the pool for the same data id.
		/// </summary>
		/// <remarks>
		/// Manual setup required before running this test:
		///	1) Update the sequence number in server config file to say 1. (ie a number other than that which is the sequence being used to perform the update.)
		/// </remarks>
		[Test]
		[Ignore("Manual setup required before running this test - see remarks.")]
		public void TestFBOPoolHousekeep()
		{
			//ensure required .Dat file exists in RBOData folder
			string path1 = @"C:\Inetpub\wwwroot\RetailBusinessObjects\Test\HouseKeepingDataFiles\DLFA.DAT";
			string path2 = @"C:\RBOData\DLFA.DAT";
			CopyDataFile(path1, path2);

			//start housekeeping
			PoolHousekeep(FBOPool.GetFBOPool());
		}

		/// <remarks>
		/// Manual setup required before running this test:
		///1) Update the sequence number in server config file to say 1 - RVSERV.INI. (ie a number other than that which is the sequence being used to perform the update.)
		/// </remarks>
		[Test]
		[Ignore("Manual setup required before running this test - see remarks.")]
		public void TestRBOPoolHousekeep()
		{
			//ensure required .Dat file exists in RBOData folder
			string path1 = @"C:\Inetpub\wwwroot\RetailBusinessObjects\Test\HouseKeepingDataFiles\DLRE.dat";
			string path2 = @"C:\RBOData\DLRE.DAT";
			CopyDataFile(path1, path2);

			//start housekeeping
			PoolHousekeep(RBOPool.GetRBOPool());
		}

		/// <remarks>
		/// Manual setup required before running this test:
		///1) Update the sequence number in server config file to say 1 - LUSERV.INI. (ie a number other than that which is the sequence being used to perform the update.)
		/// </remarks>
		[Test]
		[Ignore("Manual setup required before running this test - see remarks.")]
		public void TestLBOPoolHousekeep()
		{
			//ensure required .Dat file exists in RBOData folder
			string path1 = @"C:\Inetpub\wwwroot\RetailBusinessObjects\Test\HouseKeepingDataFiles\DLLU.dat";
			string path2 = @"C:\RBOData\DLLU.DAT";
			CopyDataFile(path1, path2);

			//start housekeeping
			PoolHousekeep(LBOPool.GetLBOPool());
		}

		/// <remarks>
		/// Manual setup required before running this test:
		///1) Update the sequence number in server config file to say 1 - RVSERV.INI. (ie a number other than that which is the sequence being used to perform the update.)
		/// </remarks>
		[Test]
		[Ignore("Manual setup required before running this test - see remarks.")]
		public void TestRVBOPoolHousekeep()
		{
			//ensure required .Dat file exists in RBOData folder
			string path1 = @"C:\Inetpub\wwwroot\RetailBusinessObjects\Test\HouseKeepingDataFiles\DLRV.dat";
			string path2 = @"C:\RBOData\DLRV.DAT";
			CopyDataFile(path1, path2);

			//start housekeeping
			PoolHousekeep(RVBOPool.GetRVBOPool());
		}

		/// <remarks>
		/// Manual setup required before running this test:
		///	1) Update the sequence number in server config file to say 1 - SUSERV.INI. (ie a number other than that which is the sequence being used to perform the update.)
		/// </remarks>
		[Test]
		[Ignore("Manual setup required before running this test - see remarks.")]
		public void TestSBOPoolHousekeep()
		{
			//ensure required .Dat file exists in RBOData folder
			string path1 = @"C:\Inetpub\wwwroot\RetailBusinessObjects\Test\HouseKeepingDataFiles\DLSU.DAT";
			string path2 = @"C:\RBOData\DLSU.DAT";
			CopyDataFile(path1, path2);

			//start housekeeping
			PoolHousekeep(SBOPool.GetSBOPool()); 
		}

		/// <summary>
		/// Helper method - ensures .dat file is placed in RBOData folder.
		/// </summary>
		public void CopyDataFile(string path1, string path2)
		{
			if (!File.Exists(path2)) 
			{
				// Copy the data file.
				File.Copy(path1, path2); 
			}			
		}

		/// <summary>
		/// Helper method - decrements the SeqNo of the INI file by 1.
		/// </summary>
		public void ResetIniFile(string path)
		{
			string strSeq = String.Empty;
			int iSeq = 0;

			//get current ini file values into conf object
			System.IO.StreamReader sr = new System.IO.StreamReader(path);
			Configuration conf = new Configuration(sr);
			sr.Close();

			System.IO.StreamWriter sw = new System.IO.StreamWriter(path);
			try
			{
				//decrement seqno by 1
				strSeq = conf.GetValue("General","SeqNo");
				iSeq = int.Parse(strSeq);
				iSeq = iSeq - 1;
				strSeq = iSeq.ToString();

				//Set conf SeqNo to new value and write cof values to ini file
				conf.SetValue("General", "SeqNo", strSeq);
				conf.Save(sw);
			}
			catch (Exception e)
			{
				Assert.Fail("Failed to decrement SeqNo in RVSERV.INI " + e.Message);
			}

			sw.Close();
		}

		/// <summary>
		/// Helper method for Pool Housekeeping
		/// </summary>
		/// <param name="pool"></param>
		private void PoolHousekeep(RetailBusinessObjectPool pool)
		{
			if (TDTraceSwitch.TraceVerbose) 
			{   
				Trace.Write( new OperationalEvent( TDEventCategory.Infrastructure, TDTraceLevel.Verbose, string.Format( Messages.UnitTestMethodStarting, MethodInfo.GetCurrentMethod().ReflectedType + ":" + MethodInfo.GetCurrentMethod().Name + pool.ToString())) ); 
			}

			Assert.IsTrue(pool.HousekeepingStatus == HousekeepingStatusId.Idle, 
				"Housekeeping already in progress.");

			System.IO.StreamReader sr = new System.IO.StreamReader(pool.ServerConfigFilepath);
			Configuration conf = new Configuration(sr);
			sr.Close();
			string currentFile = String.Empty;
			string backupFile = String.Empty;
			string oldestFile = String.Empty;
			string updateFile = String.Empty;

			try
			{
				currentFile = conf.GetValue("Refdata", "CurrentFile");
				backupFile = conf.GetValue("Refdata", "BackupFile");
				oldestFile = conf.GetValue("Refdata", "OldestFile");
				updateFile = conf.GetValue("Update", "UpdateFile");
			}
			catch (Exception)
			{
				Assert.Fail("Unable to read server config file for pool: " + pool.GetType().ToString());
			}

			Assert.IsTrue(currentFile.Length > 0, "No entry in housekeeping ini file for current file");
			Assert.IsTrue(backupFile.Length > 0, "No entry in housekeeping ini file for backup file");
			Assert.IsTrue(oldestFile.Length > 0, "No entry in housekeeping ini file for oldest file");
			Assert.IsTrue(updateFile.Length > 0, "No entry in housekeeping ini file for update file");
			Assert.IsTrue(!currentFile.Equals(backupFile),
				"Current file and backup file specified in ini file are the same. Must be different for test to run.");
			Assert.IsTrue(!backupFile.Equals(oldestFile),
				"Backup file and oldest file specified in ini file are the same. Must be different for test to run.");
			Assert.IsTrue(!currentFile.Equals(oldestFile),
				"Current file and oldest file specified in ini file are the same. Must be different for test to run.");
			
			// store current data sequence
			int dataSequenceBefore = pool.DataSequence;

			// *** RUN HOUSEKEEPING ON POOL ***
			try
			{
				if (!pool.InitiateHousekeeping("TestFeedId"))
					Assert.Fail("Failed to initiate housekeeping.");
			}
			catch(Exception)
			{
				Assert.Fail("Failed to initiate houeskeeping - check log for details.");
			}

			int wait = 0;
			while (pool.HousekeepingStatus != HousekeepingStatusId.Idle)
				wait++;

			// *** CHECK RESULTS ***
			System.IO.StreamReader srAfter = new System.IO.StreamReader(pool.ServerConfigFilepath);
			Configuration confAfter = new Configuration(srAfter);
			sr.Close();
			string currentFileAfter = String.Empty;
			string backupFileAfter = String.Empty;
			string oldestFileAfter = String.Empty;
			try
			{
				currentFileAfter = confAfter.GetValue("Refdata", "CurrentFile");
				backupFileAfter = confAfter.GetValue("Refdata", "BackupFile");
				oldestFileAfter = confAfter.GetValue("Refdata", "OldestFile");
			}
			catch (Exception)
			{
				Assert.Fail("Unable to read server config file for pool: " + pool.GetType().ToString());
			}

			Assert.IsTrue(!currentFile.Equals(currentFileAfter),
				"Current file path has not been updated.");
			Assert.IsTrue(backupFileAfter.Equals(currentFile), 
				"Backup file path has not been updated.");
			Assert.IsTrue(oldestFileAfter.Equals(backupFile), 
				"Oldest file path has not been updated.");
			
			// Check data sequence id's of business objects have been updated
			Assert.IsTrue(pool.DataSequence != dataSequenceBefore, 
				"Pool data sequence has not been updated");

			Assert.IsTrue(pool.EngineDataSequencesMatch(),
				"Pool engines do not all have the same data sequence id following the housekeep.");

			// Check that a BO that has been updated still works correctly
			BusinessObject BO = pool.GetInstance();						
			try
			{
				BoTest(pool, BO);
			}
			catch (Exception)
			{
				Assert.Fail("Housekept BO no longer functions.");
			}
			pool.Release(ref BO);
			

			if (TDTraceSwitch.TraceVerbose) 
			{   
				Trace.Write( new OperationalEvent( TDEventCategory.Infrastructure, TDTraceLevel.Verbose, string.Format( Messages.UnitTestMethodCompleted, MethodInfo.GetCurrentMethod().ReflectedType + ":" + MethodInfo.GetCurrentMethod().Name + pool.ToString()) )); 
			}
		}

		#endregion

		#region "PoolHouseKeepingFailureTests"


		/// <summary>
		/// Tests result of a housekeeping failure for each pool type.
		/// </summary>
		/// <remarks>
		/// Manual setup required before running this test - see UT Plan for details.
		/// (Hint: Use an old update file.)
		/// </remarks>
		[Test]
		[Ignore("Manual setup required before running this test - see remarks.")]
		public void TestPoolHousekeepFailure()
		{
			PoolHousekeepFailure(LBOPool.GetLBOPool());
			PoolHousekeepFailure(RBOPool.GetRBOPool());
			PoolHousekeepFailure(FBOPool.GetFBOPool());
			PoolHousekeepFailure(RVBOPool.GetRVBOPool());
			PoolHousekeepFailure(SBOPool.GetSBOPool());
		}

		private void PoolHousekeepFailure(RetailBusinessObjectPool pool)
		{
			if (TDTraceSwitch.TraceVerbose) 
			{   
				Trace.Write( new OperationalEvent( TDEventCategory.Infrastructure, TDTraceLevel.Verbose, string.Format( Messages.UnitTestMethodStarting, MethodInfo.GetCurrentMethod().ReflectedType + ":" + MethodInfo.GetCurrentMethod().Name + pool.ToString())) ); 
			}

			Assert.IsTrue(pool.HousekeepingStatus == HousekeepingStatusId.Idle,
				"Housekeeping already in progress.");

			System.IO.StreamReader sr = new System.IO.StreamReader(pool.ServerConfigFilepath);
			Configuration conf = new Configuration(sr);
			sr.Close();
			string currentFile = String.Empty;
			string backupFile = String.Empty;
			string oldestFile = String.Empty;
			string updateFile = String.Empty;

			try
			{
				currentFile = conf.GetValue("Refdata", "CurrentFile");
				backupFile = conf.GetValue("Refdata", "BackupFile");
				oldestFile = conf.GetValue("Refdata", "OldestFile");
				updateFile = conf.GetValue("Update", "UpdateFile");
			}
			catch (Exception)
			{
				Assert.Fail("Unable to read server config file for pool: " + pool.GetType().ToString());
			}

			// store current data sequence
			int dataSequenceBefore = pool.DataSequence;

			// *** RUN HOUSEKEEPING ON POOL ***
			if (!pool.InitiateHousekeeping("TestFeedId"))
			{
				Assert.Fail("Failed to initiate housekeeping.");
			}

			int wait = 0;
			while (pool.HousekeepingStatus != HousekeepingStatusId.Idle)
				wait++;

			// *** CHECK RESULTS - expected results are that housekeeping failed ***
			System.IO.StreamReader srAfter = new System.IO.StreamReader(pool.ServerConfigFilepath);
			Configuration confAfter = new Configuration(srAfter);
			sr.Close();
			string currentFileAfter = String.Empty;
			string backupFileAfter = String.Empty;
			string oldestFileAfter = String.Empty;
			try
			{
				currentFileAfter = confAfter.GetValue("Refdata", "CurrentFile");
				backupFileAfter = confAfter.GetValue("Refdata", "BackupFile");
				oldestFileAfter = confAfter.GetValue("Refdata", "OldestFile");
			}
			catch (Exception)
			{
				Assert.Fail("Unable to read server config file for pool: " + pool.GetType().ToString());
			}

			// Ensure none of the filepaths have been updated.
			Assert.IsTrue(currentFileAfter.Equals(currentFile),
				"Current file path has been updated when it should not have been.");
			Assert.IsTrue(backupFileAfter.Equals(backupFile),
				"Backup file path has been updated when it should not have been.");
			Assert.IsTrue(oldestFileAfter.Equals(oldestFile),
				"Oldest file path has not been updated.");
			
			// Check data sequence id's of business objects have not been updated
			Assert.IsTrue(pool.DataSequence == dataSequenceBefore,
				"Pool data sequence has not been updated");

			// Ensure pool ids all have same id (should be the case whether housekeeping fails or not)
			Assert.IsTrue(pool.EngineDataSequencesMatch(),
				"Pool engines do not all have the same data sequence id following the housekeep.");

			// Check that a BO that has been updated still works correctly
			BusinessObject BO = pool.GetInstance();						
			try
			{
				BoTest(pool, BO);
			}
			catch (Exception)
			{
				Assert.Fail("Housekept BO no longer functions.");
			}
			pool.Release(ref BO);
	
			if (TDTraceSwitch.TraceVerbose) 
			{   
				Trace.Write( new OperationalEvent( TDEventCategory.Infrastructure, TDTraceLevel.Verbose, string.Format( Messages.UnitTestMethodCompleted, MethodInfo.GetCurrentMethod().ReflectedType + ":" + MethodInfo.GetCurrentMethod().Name + pool.ToString()) )); 
			}
		}


		#endregion

		#region "TestReleasesNull"

		/// <summary>
		/// Tests that business objects released back to pool can not be used once released.
		/// </summary>
		[Test]
		public void TestReleasesNull()
		{
			BusinessObject lbo = LBOPool.GetLBOPool().GetInstance();
			BusinessObject fbo = FBOPool.GetFBOPool().GetInstance();
			BusinessObject rbo = RBOPool.GetRBOPool().GetInstance();
			BusinessObject rvbo = RVBOPool.GetRVBOPool().GetInstance();
			BusinessObject sbo = SBOPool.GetSBOPool().GetInstance();

			Assert.IsNotNull(lbo, "LBO Business Object retreived is null");
			Assert.IsNotNull(fbo, "FBO Business Object retreived is null");
			Assert.IsNotNull(rbo, "RBO Business Object retreived is null");
			Assert.IsNotNull(rvbo, "RVBO Business Object retreived is null");
			Assert.IsNotNull(sbo, "SBO Business Object retreived is null");

			LBOPool.GetLBOPool().Release(ref lbo);
			FBOPool.GetFBOPool().Release(ref fbo);
			RBOPool.GetRBOPool().Release(ref rbo);
			RVBOPool.GetRVBOPool().Release(ref rvbo);
			SBOPool.GetSBOPool().Release(ref sbo);

			Assert.IsNull(lbo, "LBO Business Object released is not null after release");
			Assert.IsNull(fbo, "FBO Business Object released is not null after release");
			Assert.IsNull(rbo, "RBO Business Object released is not null after release");
			Assert.IsNull(rvbo, "RVBO Business Object released is not null after release");
			Assert.IsNull(sbo, "SBO Business Object released is not null after release");
		}

		#endregion

		#region "TestDispose"

		/// <summary>
		/// Tests disposal of pools.
		/// NB Assumes pools have already been created in SetUp.
		/// </summary>
		/// <remarks>
		/// This test must be executed LAST in this suite since it detroys pool data.
		/// Unknown time delay until GC removes pools from runtime.
		/// </remarks>
		[Test]
		[Ignore("Temporarily removed since can only be run last in this suite.")]
		public void TestDispose()
		{
			if (TDTraceSwitch.TraceVerbose) 
			{   
				Trace.Write( new OperationalEvent( TDEventCategory.Infrastructure, TDTraceLevel.Verbose, string.Format( Messages.UnitTestMethodStarting, MethodInfo.GetCurrentMethod().ReflectedType + ":" + MethodInfo.GetCurrentMethod().Name)) ); 
			}

			// get some instances to test that can be disposed of when instances have been taken from pool
			BusinessObject lbo = LBOPool.GetLBOPool().GetInstance();
			BusinessObject fbo = FBOPool.GetFBOPool().GetInstance();
			BusinessObject rbo = RBOPool.GetRBOPool().GetInstance();
			BusinessObject rvbo = RVBOPool.GetRVBOPool().GetInstance();
			BusinessObject sbo = SBOPool.GetSBOPool().GetInstance();
			
			try
			{
				LBOPool.GetLBOPool().Dispose();
				FBOPool.GetFBOPool().Dispose();
				RBOPool.GetRBOPool().Dispose();
				RVBOPool.GetRVBOPool().Dispose();
				SBOPool.GetSBOPool().Dispose();

			}
			catch (Exception)
			{
				Assert.Fail("Exception thrown when disposing.");
			}

			// calling dispose again should not cause exceptions
			try
			{
				LBOPool.GetLBOPool().Dispose();
				FBOPool.GetFBOPool().Dispose();
				RBOPool.GetRBOPool().Dispose();
				RVBOPool.GetRVBOPool().Dispose();
				SBOPool.GetSBOPool().Dispose();
			}
			catch (Exception)
			{
				Assert.Fail("Exception thrown when repeating dispose.");
			}

			if (TDTraceSwitch.TraceVerbose) 
			{   
				Trace.Write( new OperationalEvent( TDEventCategory.Infrastructure, TDTraceLevel.Verbose, string.Format( Messages.UnitTestMethodCompleted, MethodInfo.GetCurrentMethod().ReflectedType + ":" + MethodInfo.GetCurrentMethod().Name) )); 
			}			
		}
		#endregion
	}
}
