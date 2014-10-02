//********************************************************************************
//NAME         : RestrictFaresGL.cs
//AUTHOR       : Richard Philpott
//DATE CREATED : 2005-03-18
//DESCRIPTION  : Wrapper for processing of RBO GL (validate route list) request.
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/RetailBusinessObjects/RestrictFaresGL.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:46:16   mturner
//Initial revision.
//
//   Rev 1.1   Mar 22 2005 16:09:16   RPhilpott
//Addition of cost-based search for Del 7.
//

using System;
using System.Collections;
using TransportDirect.UserPortal.PricingMessages;


namespace TransportDirect.UserPortal.RetailBusinessObjects
{
	/// <summary>
	/// Wrapper for processing of RBO GL (validate route list) request.
	/// </summary>
	public class RestrictFaresGL : RestrictFares
	{

		public RestrictFaresGL(BusinessObjectInput input, BusinessObjectOutput output) : base(input, output)
		{
			stringFunctionID = "GL";
			enumFunctionID = FunctionIDType.GL;

			if (!(input is ValidateRouteList))
				throw new Exception("Wrong input type passed to RestrictFaresGL in constructor");
		}

		public override ArrayList Restrict (ArrayList fares, Fare fare)
		{
			return fares;
		}

		public override BusinessObjectOutput FilterOutput (PricingRequestDto request)
		{
			BusinessObjectOutput filteredOutput = this.output;

	
			// go through all crs. If they don't match origin, destination or intermediate stops crs
			// remove from visit-crs list

			int crsLength = 3;
			int startIndex = 4;
			int maxCrsListLenth = 20;
			
			string crsList = filteredOutput.OutputBody.Substring(0, startIndex + (maxCrsListLenth * crsLength));
			
			filteredOutput.OutputBody = filteredOutput.OutputBody.Remove(0, startIndex + (maxCrsListLenth * crsLength));

			int crsCount = Convert.ToInt32(crsList.Substring(0, 4));

			// check if crs count bigger than max crs list size
			if (crsCount > maxCrsListLenth)
			{
				crsCount = maxCrsListLenth;
			}

			string filteredList = string.Empty;

			int crsNewCount =0;
			
			for (int i=0; i < crsCount; i++)
			{
				bool match = false;

				string currentCrs = crsList.Substring(startIndex + (crsLength * i), crsLength);
				
				foreach (TrainDto train in request.Trains)
				{
					if  (currentCrs == train.Alight.Location.Crs)
					{
						match = true;
					}

					if  (currentCrs == train.Board.Location.Crs)
					{
						match = true;
					}

					if  (train.IntermediateStops!= null)
					{
						foreach (LocationDto stop in train.IntermediateStops)
						{
							if  (currentCrs == stop.Crs)
							{
								match = true;
							}
						}
					}
				}

				if (match)
				{
					filteredList += currentCrs;
					crsNewCount++;
				}
			}

			// now replace crs list in output
			string toInsert = string.Format("{0:D4}", crsNewCount);
			
			toInsert += filteredList;
			
			toInsert = toInsert.PadRight(maxCrsListLenth*3+startIndex, ' ');

			filteredOutput.OutputBody = filteredOutput.OutputBody.Insert(0, toInsert);
		
			return filteredOutput;
		}

	}
}
