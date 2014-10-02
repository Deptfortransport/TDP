//********************************************************************************
//NAME         : RestrictFaresGD.cs
//AUTHOR       : Richard Philpott
//DATE CREATED : 2005-03-18
//DESCRIPTION  : Wrapper for processing of RBO GD (post-timetable) request.
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/RetailBusinessObjects/RestrictFaresGD.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:46:16   mturner
//Initial revision.
//
//   Rev 1.2   Apr 20 2005 10:32:16   RPhilpott
//Add more thorough and robust error handling.
//Resolution for 2247: PT: error handling by Retail Business Objects
//
//   Rev 1.1   Mar 31 2005 18:44:24   RPhilpott
//Changes to handling of RVBO calls
//
//   Rev 1.0   Mar 22 2005 16:30:42   RPhilpott
//Initial revision.
//

using System;
using System.Collections;
using TransportDirect.UserPortal.PricingMessages;


namespace TransportDirect.UserPortal.RetailBusinessObjects
{
	/// <summary>
	/// Wrapper for processing of RBO GD (post-timetable) request.
	/// </summary>
	public class RestrictFaresGD : RestrictFares
	{

		public RestrictFaresGD(BusinessObjectInput input, BusinessObjectOutput output) : base(input, output)
		{
			stringFunctionID = "GD";
			enumFunctionID = FunctionIDType.GD;

			if (!(input is PostTimetableRequest))
			{
				throw new Exception("Wrong input type passed to RestrictFaresGD in constructor");
			}
		}

		public override ArrayList Restrict(ArrayList fares, Fare fare)
		{
			return null;
		}

		public JourneyValidity Restrict(ArrayList trains)
		{
			JourneyValidity validity = JourneyValidity.ValidFare;		// default value

			if	(output.ErrorSeverity == Messages.ErrorSeverityCritical || output.ErrorSeverity == Messages.ErrorSeverityError)
			{
				return JourneyValidity.InvalidFare;
			}

			if (!output.OutputBody[0].Equals(' ')) 
			{
				return JourneyValidity.InvalidFare;						// no point in doing any further checking
			}

			if (output.OutputBody[1].Equals('M')) 
			{
				validity = JourneyValidity.MinimumFareApplies;
			}

			for  (int i = 0; i < 10; i++) 
			{
				if (output.OutputBody[2 + i].Equals('Q')) 
				{
					((TrainDto)trains[i]).QuotaControlledFare = true;
				}
				else if (!output.OutputBody[2 + i].Equals(' '))
				{
					return JourneyValidity.InvalidFare;					// no point in doing any further checking,
				}														//  - can't use the journey if any leg invalid ...
			}

			return validity;
		}


		public override BusinessObjectOutput FilterOutput (PricingRequestDto request)
		{
			return output;
		}

	}
}
