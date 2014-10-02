//********************************************************************************
//NAME         : TestRBOPool.cs
//AUTHOR       : SchlumbergerSema
//DATE CREATED : 10/05/2003
//DESCRIPTION  : NUnit test script for RBOPool
//             : 
//DESIGN DOC   : DD034 Reatil Pricing
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/RetailBusinessObjects/Test/TestRBO.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:46:46   mturner
//Initial revision.
//
//   Rev 1.14   Mar 22 2005 16:09:20   RPhilpott
//Addition of cost-based search for Del 7.
//
//   Rev 1.13   Feb 08 2005 10:07:32   RScott
//Assertion changed to Assert
//
//   Rev 1.12   Jun 17 2004 13:33:14   passuied
//changes for del6:
//Inserted calls to GL and GM functions to restrict fares.
//Changes in RestrictFares design to respect Open-Close Principle
//
//   Rev 1.11   May 14 2004 15:21:14   GEaton
//Updates following testing of c++ RBO dll.
//
//   Rev 1.10   Nov 23 2003 19:54:54   CHosegood
//Added code doco
//
//   Rev 1.9   Oct 31 2003 10:54:48   CHosegood
//Renamed BusinessObjectTest to TestBusinessObject
//
//   Rev 1.8   Oct 28 2003 20:05:14   geaton
//Changes to support housekeeping and timeout functionality.
//
//   Rev 1.7   Oct 22 2003 15:37:12   CHosegood
//Did that stuff with the thing
//
//   Rev 1.6   Oct 17 2003 12:35:56   geaton
//Released instances back to pool at end of test. Added asserts to ensure this is performed.
//
//   Rev 1.5   Oct 17 2003 12:05:26   CHosegood
//Adde unit test trace
//
//   Rev 1.4   Oct 16 2003 13:59:18   CHosegood
//Removed Console.WriteLine statements
//
//   Rev 1.3   Oct 16 2003 13:26:16   CHosegood
//Extends BusinessObjectTest
//
//   Rev 1.2   Oct 15 2003 20:10:50   CHosegood
//Added unit tests
//
//   Rev 1.1   Oct 14 2003 12:45:18   CHosegood
//Intermediate version.  Not ready for build/test
//
//   Rev 1.0   Oct 14 2003 11:24:50   CHosegood
//Initial Revision

using NUnit.Framework;

using System;
using System.Collections;
using System.Diagnostics;
using System.Reflection;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.PricingMessages;

namespace TransportDirect.UserPortal.RetailBusinessObjects
{
	/// <summary>
	/// Test harness for RBO.
	/// </summary>
	[TestFixture]
	public class TestRBO : TestBusinessObject
	{
        /// <summary>
        /// 
        /// </summary>
		public TestRBO() { }

        /// <summary>
        /// Tests the GA call of the RBO
        /// </summary>
        [Test] public void TestValidateSingleFares() 
        {
            if (TDTraceSwitch.TraceVerbose) 
            {   
                Trace.Write( new OperationalEvent( TDEventCategory.Infrastructure, TDTraceLevel.Verbose, string.Format( Messages.UnitTestMethodStarting, MethodInfo.GetCurrentMethod().ReflectedType + ":" + MethodInfo.GetCurrentMethod().Name )) ); 
            }
            FBOPool pool = FBOPool.GetFBOPool();
            int adultFaresOutputLength = 329;
            int specificFareOutputLength = 3;
            int faresOutputLength = 8192;
            PricingRequestDto request;
            request = BuildSingleRequest();

            FareRequest fareRequest = new FareRequest( pool.InterfaceVersion, request , faresOutputLength  );

            BusinessObject fbo = pool.GetInstance();
            Assert.IsNotNull( fbo, "Checking retrieved fbo is not null" );
            BusinessObjectOutput output = fbo.Process( fareRequest );
            pool.Release( ref fbo );

            Fares fares = new Fares( output );

            RBOPool rboPool = RBOPool.GetRBOPool();
            

            ValidateAdultFaresRequest validateAdultFaresRequest;

            validateAdultFaresRequest = new ValidateAdultFaresRequest(rboPool.InterfaceVersion, adultFaresOutputLength, request, fares);
			BusinessObject rbo = rboPool.GetInstance();
            output = rbo.Process( validateAdultFaresRequest );
			IRestrictFares restrictionController = new RestrictFaresGA(validateAdultFaresRequest, output);            
			fares.Item = restrictionController.Restrict(fares.Item, null);

            ValidateSpecificFareRequest validateSpecificFareRequest;

            ArrayList faresCopy = (ArrayList) fares.Item.Clone();
            foreach (Fare fare in fares.Item) 
            {
                validateSpecificFareRequest = new ValidateSpecificFareRequest( rboPool.InterfaceVersion, specificFareOutputLength, request, fare );
                output = rbo.Process( validateSpecificFareRequest );
				
				restrictionController = new RestrictFaresGB(validateSpecificFareRequest, output);
                faresCopy = restrictionController.Restrict(faresCopy, fare);
            }
			rboPool.Release( ref rbo );
            fares.Item = faresCopy;
            if (TDTraceSwitch.TraceVerbose) 
            {   
                Trace.Write( new OperationalEvent( TDEventCategory.Infrastructure, TDTraceLevel.Verbose, string.Format( Messages.UnitTestMethodCompleted, MethodInfo.GetCurrentMethod().ReflectedType + ":" + MethodInfo.GetCurrentMethod().Name ) )); 
            }
        }

        /// <summary>
        /// Tests the GA call of the RBO
        /// </summary>
        [Test] public void TestValidateReturnFares() 
        {
            if (TDTraceSwitch.TraceVerbose) 
            {   
                Trace.Write( new OperationalEvent( TDEventCategory.Infrastructure, TDTraceLevel.Verbose, string.Format( Messages.UnitTestMethodStarting, MethodInfo.GetCurrentMethod().ReflectedType + ":" + MethodInfo.GetCurrentMethod().Name )) ); 
            }
            FBOPool pool = FBOPool.GetFBOPool();
            int adultFaresOutputLength = 329;
            int specificFareOutputLength = 3;
            int faresOutputLength = 8192;
            PricingRequestDto request;
            request = BuildReturnRequest();

            FareRequest fareRequest = new FareRequest( pool.InterfaceVersion, request , faresOutputLength  );

            BusinessObject fbo = pool.GetInstance();
            Assert.IsNotNull( fbo, "Checking retrieved fbo is not null" );
            BusinessObjectOutput output = fbo.Process( fareRequest );
            pool.Release( ref fbo );

            Fares fares = new Fares( output );

            RBOPool rboPool = RBOPool.GetRBOPool();
            BusinessObject rbo = rboPool.GetInstance();

            ValidateAdultFaresRequest validateAdultFaresRequest;

            validateAdultFaresRequest = new ValidateAdultFaresRequest(rboPool.InterfaceVersion, adultFaresOutputLength, request, fares);
            output = rbo.Process( validateAdultFaresRequest );
			IRestrictFares restrictionController = new RestrictFaresGA(validateAdultFaresRequest, output);
            fares.Item = restrictionController.Restrict(fares.Item, null);

            ValidateSpecificFareRequest valdateSpecificFareRequest;

            ArrayList faresCopy = (ArrayList) fares.Item.Clone();
            foreach (Fare fare in fares.Item) 
            {
                valdateSpecificFareRequest = new ValidateSpecificFareRequest( rboPool.InterfaceVersion, specificFareOutputLength, request, fare );
                output = rbo.Process( valdateSpecificFareRequest );
				
				restrictionController = new RestrictFaresGB(valdateSpecificFareRequest, output);
                faresCopy = restrictionController.Restrict(faresCopy, fare);
            }

			rboPool.Release( ref rbo );
			fares.Item = faresCopy;

            if (TDTraceSwitch.TraceVerbose) 
            {   
                Trace.Write( new OperationalEvent( TDEventCategory.Infrastructure, TDTraceLevel.Verbose, string.Format( Messages.UnitTestMethodCompleted, MethodInfo.GetCurrentMethod().ReflectedType + ":" + MethodInfo.GetCurrentMethod().Name ) )); 
            }
        }

        /// <summary>
        /// Tests the GB call of the RBO
        /// </summary>
        [Test] public void TestValidateSingleAdultFares() 
        {
            if (TDTraceSwitch.TraceVerbose) 
            {   
                Trace.Write( new OperationalEvent( TDEventCategory.Infrastructure, TDTraceLevel.Verbose, string.Format( Messages.UnitTestMethodStarting, MethodInfo.GetCurrentMethod().ReflectedType + ":" + MethodInfo.GetCurrentMethod().Name )) ); 
            }
            FBOPool pool = FBOPool.GetFBOPool();
            int adultFaresOutputLength = 329;
            int faresOutputLength = 8192;
            PricingRequestDto request;
            request = BuildSingleRequest();

            FareRequest fareRequest = new FareRequest( pool.InterfaceVersion, request , faresOutputLength  );

            BusinessObject fbo = pool.GetInstance();
            Assert.IsNotNull( fbo, "Checking retrieved fbo is not null" );
            BusinessObjectOutput output = fbo.Process( fareRequest );
            pool.Release( ref fbo );

            Fares fares = new Fares( output );

            RBOPool rboPool = RBOPool.GetRBOPool();
            BusinessObject rbo = rboPool.GetInstance();

            ValidateAdultFaresRequest validateAdultFaresRequest;

            validateAdultFaresRequest = new ValidateAdultFaresRequest(rboPool.InterfaceVersion, adultFaresOutputLength, request, fares);
            output = rbo.Process( validateAdultFaresRequest );
			rboPool.Release( ref rbo );
			IRestrictFares restrictionController = new RestrictFaresGA(validateAdultFaresRequest, output);
            fares.Item = restrictionController.Restrict( fares.Item, null);

			if (TDTraceSwitch.TraceVerbose) 
            {   
                Trace.Write( new OperationalEvent( TDEventCategory.Infrastructure, TDTraceLevel.Verbose, string.Format( Messages.UnitTestMethodCompleted, MethodInfo.GetCurrentMethod().ReflectedType + ":" + MethodInfo.GetCurrentMethod().Name ) )); 
            }
        }

        /// <summary>
        /// Tests the GA call of the RBO
        /// </summary>
        [Test] public void TestValidateReturnAdultFares() 
        {
            if (TDTraceSwitch.TraceVerbose) 
            {   
                Trace.Write( new OperationalEvent( TDEventCategory.Infrastructure, TDTraceLevel.Verbose, string.Format( Messages.UnitTestMethodStarting, MethodInfo.GetCurrentMethod().ReflectedType + ":" + MethodInfo.GetCurrentMethod().Name )) ); 
            }
            FBOPool pool = FBOPool.GetFBOPool();
            int adultFaresOutputLength = 329;
            int faresOutputLength = 8192;
            PricingRequestDto request;
            request = BuildReturnRequest();

            FareRequest fareRequest = new FareRequest( pool.InterfaceVersion, request , faresOutputLength  );

            BusinessObject fbo = pool.GetInstance();
            Assert.IsNotNull( fbo, "Checking retrieved fbo is not null" );
            BusinessObjectOutput output = fbo.Process( fareRequest );
            pool.Release( ref fbo );

            Fares fares = new Fares( output );

            RBOPool rboPool = RBOPool.GetRBOPool();
            BusinessObject rbo = rboPool.GetInstance();

            ValidateAdultFaresRequest validateAdultFaresRequest;

            validateAdultFaresRequest = new ValidateAdultFaresRequest(rboPool.InterfaceVersion, adultFaresOutputLength, request, fares);
            output = rbo.Process( validateAdultFaresRequest );
			rboPool.Release( ref rbo );
			IRestrictFares restrictionController = new RestrictFaresGA(validateAdultFaresRequest, output);
            fares.Item = restrictionController.Restrict( fares.Item, null);
		
            if (TDTraceSwitch.TraceVerbose) 
            {   
                Trace.Write( new OperationalEvent( TDEventCategory.Infrastructure, TDTraceLevel.Verbose, string.Format( Messages.UnitTestMethodCompleted, MethodInfo.GetCurrentMethod().ReflectedType + ":" + MethodInfo.GetCurrentMethod().Name ) )); 
            }
        }

        /// <summary>
        /// Tests the GB call of the RBO
        /// </summary>
        [Test] public void TestValidateSingleSpecificFare() 
        {
            if (TDTraceSwitch.TraceVerbose) 
            {   
                Trace.Write( new OperationalEvent( TDEventCategory.Infrastructure, TDTraceLevel.Verbose, string.Format( Messages.UnitTestMethodStarting, MethodInfo.GetCurrentMethod().ReflectedType + ":" + MethodInfo.GetCurrentMethod().Name )) ); 
            }
            FBOPool pool = FBOPool.GetFBOPool();
            int specificFareOutputLength = 3;
            int faresOutputLength = 8192;
            PricingRequestDto request;
            request = BuildSingleRequest();

            FareRequest fareRequest = new FareRequest( pool.InterfaceVersion, request , faresOutputLength  );

            BusinessObject fbo = pool.GetInstance();
            Assert.IsNotNull( fbo, "Checking retrieved fbo is not null" );
            BusinessObjectOutput output = fbo.Process( fareRequest );
            pool.Release( ref fbo );

            Fares fares = new Fares( output );

            RBOPool rboPool = RBOPool.GetRBOPool();
            BusinessObject rbo = rboPool.GetInstance();

            ValidateSpecificFareRequest valdateSpecificFareRequest;
            ArrayList faresCopy = (ArrayList) fares.Item.Clone();
            foreach (Fare fare in fares.Item) 
            {
                valdateSpecificFareRequest = new ValidateSpecificFareRequest( rboPool.InterfaceVersion, specificFareOutputLength, request, fare );
                output = rbo.Process( valdateSpecificFareRequest );
				IRestrictFares restrictionController = new RestrictFaresGB(valdateSpecificFareRequest, output);
                faresCopy = restrictionController.Restrict( faresCopy, fare);
            }
			rboPool.Release( ref rbo );
            fares.Item = faresCopy;

            if (TDTraceSwitch.TraceVerbose) 
            {   
                Trace.Write( new OperationalEvent( TDEventCategory.Infrastructure, TDTraceLevel.Verbose, string.Format( Messages.UnitTestMethodCompleted, MethodInfo.GetCurrentMethod().ReflectedType + ":" + MethodInfo.GetCurrentMethod().Name ) )); 
            }
        }

        /// <summary>
        /// Tests the GB call of the RBO
        /// </summary>
        [Test] public void TestValidateReturnSpecificFare() 
        {
            if (TDTraceSwitch.TraceVerbose) 
            {   
                Trace.Write( new OperationalEvent( TDEventCategory.Infrastructure, TDTraceLevel.Verbose, string.Format( Messages.UnitTestMethodStarting, MethodInfo.GetCurrentMethod().ReflectedType + ":" + MethodInfo.GetCurrentMethod().Name )) ); 
            }
            FBOPool pool = FBOPool.GetFBOPool();
            int specificFareOutputLength = 3;
            int faresOutputLength = 8192;
            PricingRequestDto request;
            request = BuildReturnRequest();

            FareRequest fareRequest = new FareRequest( pool.InterfaceVersion, request , faresOutputLength  );

            BusinessObject fbo = pool.GetInstance();
            Assert.IsNotNull( fbo, "Checking retrieved fbo is not null" );
            BusinessObjectOutput output = fbo.Process( fareRequest );
            pool.Release( ref fbo );

            Fares fares = new Fares( output );

            RBOPool rboPool = RBOPool.GetRBOPool();
            BusinessObject rbo = rboPool.GetInstance();

            ValidateSpecificFareRequest valdateSpecificFareRequest;
            ArrayList faresCopy = (ArrayList) fares.Item.Clone();
            foreach (Fare fare in fares.Item) 
            {
                valdateSpecificFareRequest = new ValidateSpecificFareRequest( rboPool.InterfaceVersion, specificFareOutputLength, request, fare );
                output = rbo.Process( valdateSpecificFareRequest );
	
				IRestrictFares restrictionController = new RestrictFaresGB(valdateSpecificFareRequest, output);
                faresCopy = restrictionController.Restrict( faresCopy, fare);
            }
			rboPool.Release( ref rbo );
            fares.Item = faresCopy;
			
            if (TDTraceSwitch.TraceVerbose) 
            {   
                Trace.Write( new OperationalEvent( TDEventCategory.Infrastructure, TDTraceLevel.Verbose, string.Format( Messages.UnitTestMethodCompleted, MethodInfo.GetCurrentMethod().ReflectedType + ":" + MethodInfo.GetCurrentMethod().Name ) )); 
            }
        }

        /// <summary>
        /// Tests the GL call of the RBO
        /// </summary>
        [Test] public void TestValidateRouteList() 
        {
            if (TDTraceSwitch.TraceVerbose) 
            {   
                Trace.Write( new OperationalEvent( TDEventCategory.Infrastructure, TDTraceLevel.Verbose, string.Format( Messages.UnitTestMethodStarting, MethodInfo.GetCurrentMethod().ReflectedType + ":" + MethodInfo.GetCurrentMethod().Name )) ); 
            }
            FBOPool pool = FBOPool.GetFBOPool();
            int faresOutputLength = 8192;
            int routeListOutput = 8192;
            PricingRequestDto request;
            FareRequest fareRequest;
            BusinessObject fbo;
            BusinessObjectOutput output;
            RBOPool rboPool;
            Fares fares;
            BusinessObject rbo;
            ValidateRouteList validateRouteListRequest;

            //Validate a single request
            request = BuildSingleRequest();

            fareRequest = new FareRequest( pool.InterfaceVersion, request , faresOutputLength  );

            fbo = pool.GetInstance();
            Assert.IsNotNull( fbo, "Checking retrieved fbo is not null" );
            output = fbo.Process( fareRequest );
            pool.Release( ref fbo );

            fares = new Fares( output );

            rboPool = RBOPool.GetRBOPool();
            rbo = rboPool.GetInstance();
            Assert.IsNotNull( rbo, "Retrieved rbo" );

            validateRouteListRequest = new ValidateRouteList( rboPool.InterfaceVersion, routeListOutput, request, fares );
            output = rbo.Process( validateRouteListRequest );
			IRestrictFares restrictionController = new RestrictFaresGL(validateRouteListRequest, output);
            fares.Item = restrictionController.Restrict( fares.Item, null);

            //Validate a return request
            request = BuildReturnRequest();
            fareRequest = new FareRequest( pool.InterfaceVersion, request , faresOutputLength  );

            fbo = pool.GetInstance();
            Assert.IsNotNull( fbo, "Checking retrieved fbo is not null" );
            output = fbo.Process( fareRequest );
            pool.Release( ref fbo );

            fares = new Fares( output );

            rboPool = RBOPool.GetRBOPool();
            rbo = rboPool.GetInstance();
            Assert.IsNotNull( rbo, "Retrieved rbo" );

            validateRouteListRequest = new ValidateRouteList( rboPool.InterfaceVersion, routeListOutput, request, fares );
            output = rbo.Process( validateRouteListRequest );
			restrictionController = new RestrictFaresGL(validateRouteListRequest, output);
            fares.Item = restrictionController.Restrict( fares.Item, null);

            //Validate leicester to nottingham
            request = BuildLeicesterToNottinghamPricingRequestDto();

            fareRequest = new FareRequest( pool.InterfaceVersion, request , faresOutputLength  );

            fbo = pool.GetInstance();
            Assert.IsNotNull( fbo, "Checking retrieved fbo is not null" );
            output = fbo.Process( fareRequest );
            pool.Release( ref fbo );

            fares = new Fares( output );

            rboPool = RBOPool.GetRBOPool();
            rbo = rboPool.GetInstance();
            Assert.IsNotNull( rbo, "Retrieved rbo" );

            validateRouteListRequest = new ValidateRouteList( rboPool.InterfaceVersion, routeListOutput, request, fares );
            output = rbo.Process( validateRouteListRequest );
			restrictionController = new RestrictFaresGL(validateRouteListRequest, output);
            fares.Item = restrictionController.Restrict( fares.Item, null);

            //Validate Wigan to Egton
            request = BuildWiganToEgtonRequest();

            fareRequest = new FareRequest( pool.InterfaceVersion, request , faresOutputLength  );

            fbo = pool.GetInstance();
            Assert.IsNotNull( fbo, "Checking retrieved fbo is not null" );
            output = fbo.Process( fareRequest );
            pool.Release( ref fbo );

            fares = new Fares( output );

            rboPool = RBOPool.GetRBOPool();
            rbo = rboPool.GetInstance();
            Assert.IsNotNull( rbo, "Retrieved rbo" );

            validateRouteListRequest = new ValidateRouteList( rboPool.InterfaceVersion, routeListOutput, request, fares );
            output = rbo.Process( validateRouteListRequest );
			restrictionController = new RestrictFaresGL(validateRouteListRequest, output);

            fares.Item = restrictionController.Restrict( fares.Item, null);

			rboPool.Release( ref rbo );

		
            if (TDTraceSwitch.TraceVerbose) 
            {   
                Trace.Write( new OperationalEvent( TDEventCategory.Infrastructure, TDTraceLevel.Verbose, string.Format( Messages.UnitTestMethodCompleted, MethodInfo.GetCurrentMethod().ReflectedType + ":" + MethodInfo.GetCurrentMethod().Name ) )); 
            }
        }

        /// <summary>
        /// Tests the GM call of the RBO
        /// </summary>
        [Test] public void TestValidateRoutes() 
        {
            if (TDTraceSwitch.TraceVerbose) 
            {   
                Trace.Write( new OperationalEvent( TDEventCategory.Infrastructure, TDTraceLevel.Verbose, string.Format( Messages.UnitTestMethodStarting, MethodInfo.GetCurrentMethod().ReflectedType + ":" + MethodInfo.GetCurrentMethod().Name )) ); 
            }
            FBOPool fboPool = FBOPool.GetFBOPool();
            int faresOutputLength = 8192;
            int validateRouteListOutputLength = 2076;
            int validateRoutesOutputLength = 76;
            PricingRequestDto request;
            FareRequest fareRequest;
            BusinessObject fbo;
            BusinessObjectOutput output;
            Fares fares;
            RBOPool rboPool = RBOPool.GetRBOPool();
            BusinessObject rbo = rboPool.GetInstance();
            ValidateRouteList validateRouteListRequest;
            ValidateRoutes validateRoutes;

            fbo = fboPool.GetInstance();
            Assert.IsNotNull( fbo, "Checking retrieved fbo is not null" );

            //Validate single request
            Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, "Single request"));;
            request = BuildSingleRequest();
            fareRequest = new FareRequest( fboPool.InterfaceVersion, request , faresOutputLength  );
            output = fbo.Process( fareRequest );
            fares = new Fares( output );
            validateRouteListRequest = new ValidateRouteList( rboPool.InterfaceVersion, validateRouteListOutputLength, request, fares );
            output = rbo.Process( validateRouteListRequest );

            validateRoutes = new ValidateRoutes( rboPool.InterfaceVersion, validateRoutesOutputLength, request, output.OutputBody );
            output = rbo.Process( validateRoutes );

			IRestrictFares restrictionController = new RestrictFaresGM(validateRoutes, output);
            fares.Item = restrictionController.Restrict( fares.Item, null);

            //Validate return request
			Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, "Return request"));
            request = BuildReturnRequest();
            fareRequest = new FareRequest( fboPool.InterfaceVersion, request , faresOutputLength  );
            output = fbo.Process( fareRequest );
            fares = new Fares( output );
            validateRouteListRequest = new ValidateRouteList( rboPool.InterfaceVersion, validateRouteListOutputLength, request, fares );
            output = rbo.Process( validateRouteListRequest );

            validateRoutes = new ValidateRoutes( rboPool.InterfaceVersion, validateRoutesOutputLength, request, output.OutputBody );
            output = rbo.Process( validateRoutes );

			restrictionController = new RestrictFaresGM(validateRoutes, output);
            fares.Item = restrictionController.Restrict(fares.Item, null);

            //Validate Leicester to Nottingham
            Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, "Leicester to Nottingham request"));
            request = BuildLeicesterToNottinghamPricingRequestDto();
            fareRequest = new FareRequest( fboPool.InterfaceVersion, request , faresOutputLength  );
            output = fbo.Process( fareRequest );
            fares = new Fares( output );
            validateRouteListRequest = new ValidateRouteList( rboPool.InterfaceVersion, validateRouteListOutputLength, request, fares );
            output = rbo.Process( validateRouteListRequest );

            validateRoutes = new ValidateRoutes( rboPool.InterfaceVersion, validateRoutesOutputLength, request, output.OutputBody );
            output = rbo.Process( validateRoutes );
	
			restrictionController = new RestrictFaresGM(validateRoutes, output);
            fares.Item = restrictionController.Restrict( fares.Item, null);

            //Validate Wigan to Egton
			Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Verbose, "Wigan to Egton request"));
            request = BuildWiganToEgtonRequest();
            fareRequest = new FareRequest( fboPool.InterfaceVersion, request , faresOutputLength  );
            output = fbo.Process( fareRequest );
            fares = new Fares( output );
            validateRouteListRequest = new ValidateRouteList( rboPool.InterfaceVersion, validateRouteListOutputLength, request, fares );
            output = rbo.Process( validateRouteListRequest );

            validateRoutes = new ValidateRoutes( rboPool.InterfaceVersion, validateRoutesOutputLength, request, output.OutputBody );
            output = rbo.Process( validateRoutes );
	
			restrictionController = new RestrictFaresGM(validateRoutes, output);
            fares.Item = restrictionController.Restrict( fares.Item, null);

            fboPool.Release( ref fbo );
			rboPool.Release( ref rbo );

	
            if (TDTraceSwitch.TraceVerbose) 
            {   
                Trace.Write( new OperationalEvent( TDEventCategory.Infrastructure, TDTraceLevel.Verbose, string.Format( Messages.UnitTestMethodCompleted, MethodInfo.GetCurrentMethod().ReflectedType + ":" + MethodInfo.GetCurrentMethod().Name ) )); 
            }
        }
	}
}
