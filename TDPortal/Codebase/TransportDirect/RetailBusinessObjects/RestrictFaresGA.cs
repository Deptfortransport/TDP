//********************************************************************************
//NAME         : RestrictFaresGA.cs
//AUTHOR       : Richard Philpott
//DATE CREATED : 2005-03-18
//DESCRIPTION  : Wrapper for processing of RBO GA (validate fare) request.
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/RetailBusinessObjects/RestrictFaresGA.cs-arc  $
//
//   Rev 1.1   Feb 18 2009 18:18:22   mmodi
//Update following change to use interface 0202
//Resolution for 5210: CCN487 - ZPBO Implementation workstream
//
//   Rev 1.0   Nov 08 2007 12:46:16   mturner
//Initial revision.
//
//   Rev 1.5   Dec 08 2005 15:17:40   RPhilpott
//Correct route validation.
//Resolution for 3350: DN040: Route validation by RE GA call
//
//   Rev 1.4   Dec 05 2005 16:30:48   RPhilpott
//Correct logic in removing fares with invalid routes after GA call, and apply the same logic to GR processing.
//Resolution for 3205: DN039 - Fare available in Time Based Planning not available in Fare based Planning
//
//   Rev 1.3   Nov 29 2005 17:53:40   RWilby
//Updated class to exclude fares with invalid routes 
//Resolution for 3205: DN039 - Fare available in Time Based Planning not available in Fare based Planning
//
//   Rev 1.2   Apr 20 2005 10:32:14   RPhilpott
//Add more thorough and robust error handling.
//Resolution for 2247: PT: error handling by Retail Business Objects
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
	/// Wrapper for processing of RBO GA (validate fare) request
	/// </summary>
	public class RestrictFaresGA : RestrictFares
	{

		public RestrictFaresGA(BusinessObjectInput input, BusinessObjectOutput output) : base(input, output)
		{
			stringFunctionID = "GA";
			enumFunctionID = FunctionIDType.GA;

			if (!(input is ValidateAdultFaresRequest))
				throw new Exception("Wrong input type passed to RestrictFaresGA in constructor");
		}

		public override ArrayList Restrict (ArrayList fares, Fare fare)
		{

			if	(output.ErrorSeverity == Messages.ErrorSeverityCritical || output.ErrorSeverity == Messages.ErrorSeverityError)
			{
				fares.Clear();
				return fares;
			}

            // Interface version 0202 and later allows 99 route codes (otherwise 25)
			int numberOfValidEntries = int.Parse(output.OutputBody.Substring(99, 4).Trim());
			
			bool[] delete = new bool[numberOfValidEntries];

			//Determine the route codes that were passed to the BO
			
			ArrayList routes = new ArrayList();

			for (int i = 0; i < fares.Count; i++) 
			{
				if ( !routes.Contains((fares[i] as Fare).Route.Code)) 
				{
					routes.Add((fares[i] as Fare).Route.Code);
				}
			}

			//Determine which routes are invalid

			ArrayList invalidRoutes = new ArrayList();
			
			for ( int i = 0; i < 99; i++ ) 
			{
				if ( (routes.Count != 0) && (i <= routes.Count-1 ) )
				{
					if (output.OutputBody[i].Equals('T')) 
					{
						invalidRoutes.Add(routes[i].ToString());
					}
				}
			}

			//Determine which fares are invalid
			
			int start = 99 + 4;
			
			for ( int j = 0; j < delete.Length; j++ ) 
			{
				if (!output.OutputBody[j + start].Equals(' ')) 
				{
					delete[j] = true;
				} 
				else 
				{
					delete[j] = false;
				}
			}

			//Remove any invalid fares from the collection
			
			for (int k = delete.Length - 1; k >= 0; k--) 
			{
				if	(delete[k]) 
				{
					fares.RemoveAt(k);
				}
			}
			
			//Remove fares with invalid routes

			delete = new bool[fares.Count];

			for  (int i = 0; i < fares.Count; i++)
			{
				delete[i] =	(invalidRoutes.Contains((fares[i] as Fare).Route.Code));
			}


			for (int k = delete.Length - 1; k >= 0; k--) 
			{
				if	(delete[k]) 
				{
					fares.RemoveAt(k);
				}
			}

			return fares;
		}
		
		
		public override BusinessObjectOutput FilterOutput (PricingRequestDto request)
		{
			return output;
		}

	}
}
