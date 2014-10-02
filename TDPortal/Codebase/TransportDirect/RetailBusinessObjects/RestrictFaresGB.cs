//********************************************************************************
//NAME         : RestrictFaresGA.cs
//AUTHOR       : Richard Philpott
//DATE CREATED : 2005-03-18
//DESCRIPTION  : Wrapper for processing of RBO GB (validate fare) request.
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/RetailBusinessObjects/RestrictFaresGB.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:46:16   mturner
//Initial revision.
//
//   Rev 1.3   Apr 20 2005 10:32:14   RPhilpott
//Add more thorough and robust error handling.
//Resolution for 2247: PT: error handling by Retail Business Objects
//
//   Rev 1.2   Mar 22 2005 16:09:16   RPhilpott
//Addition of cost-based search for Del 7.
//

using System;
using System.Collections;
using System.Diagnostics;
using TransportDirect.UserPortal.PricingMessages;
using TransportDirect.Common.Logging;


namespace TransportDirect.UserPortal.RetailBusinessObjects
{
	/// <summary>
	/// Wrapper for processing of RBO GB (validate fare) request.
	/// </summary>
	public class RestrictFaresGB : RestrictFares
	{

		public RestrictFaresGB(BusinessObjectInput input, BusinessObjectOutput output) : base(input, output)
		{
			stringFunctionID = "GB";
			enumFunctionID = FunctionIDType.GB;

			if (!(input is ValidateSpecificFareRequest))
			{
				throw new Exception("Wrong input type passed to RestrictFaresGB in constructor");
			}

		}

		public override ArrayList Restrict (ArrayList fares, Fare fare)
		{
			bool minimumFareApplies = false;
			bool quotaControlledFare = false;
			bool invalidFare = false;

			if ( !output.OutputBody[0].Equals(' ') ) 
			{
			    minimumFareApplies = true;
			}

			if ( !output.OutputBody[1].Equals(' ') ) 
			{
			    quotaControlledFare = true;
			}

			if ( !output.OutputBody[2].Equals(' ') ) 
			{
			    invalidFare = true;
			}

			if	(output.ErrorSeverity == Messages.ErrorSeverityCritical || output.ErrorSeverity == Messages.ErrorSeverityError)
			{
				invalidFare = true;
			}

			// If the fare is not valid remove it from the collection of fares
			if  (invalidFare) 
			{
			    fares.Remove(fare);
			} 
			else 
			{
			    // If a minimum fare applies make sure the fare is has a
			    //  value of  greater than or equal to the minimum fare
			    if ( minimumFareApplies ) 
			    {
					if (fare.AdultFare < fare.AdultFareMinimum )
					{
						fare.AdultFare = fare.AdultFareMinimum;
					}
			        
					if (fare.ChildFare < fare.ChildFareMinimum)
					{
						fare.ChildFare = fare.ChildFareMinimum;
					}
			    }

			    // If this ticket may be quota controlled set the identifier
			    if  (quotaControlledFare) 
			    {
			        fare.QuotaControlled = true;
			    }
			}
			return fares;
		}

		public override BusinessObjectOutput FilterOutput(PricingRequestDto request)
		{
			return output;
		}

	}
}
