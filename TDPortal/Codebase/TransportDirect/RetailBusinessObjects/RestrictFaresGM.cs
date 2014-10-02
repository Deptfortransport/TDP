//********************************************************************************
//NAME         : RestrictFaresGM.cs
//AUTHOR       : Richard Philpott
//DATE CREATED : 2005-03-18
//DESCRIPTION  : Wrapper for processing of RBO GM (validate routes) request.
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/RetailBusinessObjects/RestrictFaresGM.cs-arc  $
//
//   Rev 1.1   Feb 18 2009 18:18:22   mmodi
//Update following change to use interface 0202
//Resolution for 5210: CCN487 - ZPBO Implementation workstream
//
//   Rev 1.0   Nov 08 2007 12:46:18   mturner
//Initial revision.
//
//   Rev 1.1   Mar 22 2005 16:09:16   RPhilpott
//Addition of cost-based search for Del 7.
//

using System;
using System.Collections;
using TransportDirect.UserPortal.PricingMessages;
using System.Globalization;


namespace TransportDirect.UserPortal.RetailBusinessObjects
{
	/// <summary>
	/// Wrapper for processing of RBO GM (validate routes) request.
	/// </summary>
	public class RestrictFaresGM : RestrictFares
	{

		public RestrictFaresGM(BusinessObjectInput input, BusinessObjectOutput output) : base(input, output)
		{
			stringFunctionID = "GM";
			enumFunctionID = FunctionIDType.GM;

			if (!(input is ValidateRoutes))
				throw new Exception("Wrong input type passed to RestrictFaresGA in constructor");
		}

		public override ArrayList Restrict (ArrayList fares, Fare fare)
		{
            // Interface version 0202 and later allows 99 route codes (otherwise 25)
			string[] validRoutes = new string[99];
			bool[] toc = new bool[99];
			int index = 0;

			for (int i = 0; i < 99; i++) 
			{
			    validRoutes[i] = output.OutputBody.Substring(index, 2);
			    index += 2;
			}

			index++;

			for (int j = 0; j < 99; j++) 
			{
			    if ( output.OutputBody[index+j].Equals('Y') ) 
			    {
			        toc[j] = true;
			    } 
			    else 
			    {
			        toc[j] = false;
			    }
			}

			// get Route-List from Input
			int routeListStartIndex = 64;
			int inputIndex = routeListStartIndex;
			int maxRouteNumber = 99;

			int routeListCount = Convert.ToInt32(input.InputBody.Substring(inputIndex, 4));
			inputIndex += 4;

			if (routeListCount> maxRouteNumber)
			{
				routeListCount = maxRouteNumber;
			}

			ArrayList routeListInput = new ArrayList(maxRouteNumber);
			
			for (int i=0; i < routeListCount; i++)
			{
				string routeCode = input.InputBody.Substring(inputIndex, 5);
				inputIndex += 5;

				string londonMarker = input.InputBody.Substring(inputIndex, 1);
				inputIndex++;
				
				Route newRoute = new Route(routeCode, londonMarker);
				
				// increment inputIndex to skip useless data
				inputIndex += 40;

				routeListInput.Add(newRoute);
			}

			ArrayList validFares = new ArrayList();
		
			// foreach fare, only keep the one whose route matches a valid route returned by GM
			
			foreach (string stringRouteIndex in validRoutes)
			{
				// get valid Route returned in output
				int routeIndex = Convert.ToInt32(stringRouteIndex, CultureInfo.InvariantCulture);
			
				if (routeIndex == 0)
				{
					continue;
				}

				Route validRoute = (Route)routeListInput[routeIndex - 1];		// -1 applied becaue COBOL output list starts with index 1
				
				// go through fares and if route code matches, add it to valid fares list
				
				foreach( Fare aFare in fares)
				{
					if (aFare.Route.Code == validRoute.Code)
					{
						validFares.Add(aFare);
						
					}
				}
			}

			return validFares;
		}

		public override BusinessObjectOutput FilterOutput (PricingRequestDto request)
		{
			return output;
		}
	}
}
