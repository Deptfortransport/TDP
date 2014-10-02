//********************************************************************************
//NAME         : RestrictFaresGC.cs
//AUTHOR       : Richard Philpott
//DATE CREATED : 2005-03-18
//DESCRIPTION  : Wrapper for processing of RBO GC (pre-timetable) request.
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/RetailBusinessObjects/RestrictFaresGC.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:46:16   mturner
//Initial revision.
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
	/// Wrapper for RBO GC (pre-timetable) request.
	/// </summary>
	public class RestrictFaresGC : RestrictFares
	{

		public RestrictFaresGC(BusinessObjectInput input, BusinessObjectOutput output) : base(input, output)
		{
			stringFunctionID = "GC";
			enumFunctionID = FunctionIDType.GC;

			if (!(input is PreTimetableRequest))
			{
				throw new Exception("Wrong input type passed to RestrictFaresGC in constructor");
			}

		}

		/// <summary>
		/// Dummy implementation to satisfy interface contract -- real parsing
		///  of output takes place in TTBOParametersDto.PopulateFromGCOutput().
		/// </summary>
		public override ArrayList Restrict (ArrayList fares, Fare fare)
		{
			return fares;
		}

		public override BusinessObjectOutput FilterOutput (PricingRequestDto request)
		{
			return output;
		}

	}
}
