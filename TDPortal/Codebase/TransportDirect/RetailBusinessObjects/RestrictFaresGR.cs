//********************************************************************************
//NAME         : RestrictFaresGR.cs
//AUTHOR       : Richard Philpott
//DATE CREATED : 2005-03-18
//DESCRIPTION  : Wrapper for processing of RBO GR (validate faes for route) request.
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/RetailBusinessObjects/RestrictFaresGR.cs-arc  $
//
//   Rev 1.2   Jul 23 2010 10:50:40   mmodi
//Updated to check for additional invalid service characters
//Resolution for 5580: Fares - RF 038 - Off Peak day return fare availability
//
//   Rev 1.1   Feb 18 2009 18:18:22   mmodi
//Update following change to use interface 0202
//Resolution for 5210: CCN487 - ZPBO Implementation workstream
//
//   Rev 1.0   Nov 08 2007 12:46:18   mturner
//Initial revision.
//
//   Rev 1.3   Dec 05 2005 16:30:48   RPhilpott
//Correct logic in removing fares with invalid routes after GA call, and apply the same logic to GR processing.
//Resolution for 3205: DN039 - Fare available in Time Based Planning not available in Fare based Planning
//
//   Rev 1.2   Apr 20 2005 10:32:24   RPhilpott
//Add more thorough and robust error handling.
//Resolution for 2247: PT: error handling by Retail Business Objects
//
//   Rev 1.1   Apr 14 2005 13:43:56   RPhilpott
//Correct invalid fare identification.
//Resolution for 2148: PT - Fares Journey Planner tries to sell day returns for journeys seperated by a day
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
	/// Wrapper for processing of RBO GR (validate faes for route) request.
	/// </summary>
	public class RestrictFaresGR : RestrictFares
	{

		public RestrictFaresGR(BusinessObjectInput input, BusinessObjectOutput output) : base(input, output)
		{
			stringFunctionID = "GR";
			enumFunctionID = FunctionIDType.GR;

			if (!(input is ValidateFaresForRouteRequest))
			{
				throw new Exception("Wrong input type passed to RestrictFaresGR in constructor");
			}
		}


		public override ArrayList Restrict(ArrayList fares, Fare fare)
		{
			
			if	(output.ErrorSeverity == Messages.ErrorSeverityCritical || output.ErrorSeverity == Messages.ErrorSeverityError)
			{
				fares.Clear();
				return fares;
			}
			
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
					if ( !output.OutputBody[i].Equals(' ') ) 
					{
						invalidRoutes.Add(routes[i].ToString());
					}
				}
			}

            // Fares-count
			int numberOfValidEntries = int.Parse(output.OutputBody.Substring(99, 3).Trim());

			bool[] delete = new bool[numberOfValidEntries];

			int start = 99 + 3;
		
			for (int i = 0; i < delete.Length; i++ ) 
			{
				delete[i] = (  output.OutputBody[(i * 2) + start].Equals('*')   // Not valid
                            || output.OutputBody[(i * 2) + start].Equals('T')   // Not valid due to TOC values
                            || output.OutputBody[(i * 2) + start].Equals('C')   // Not valid due to calendar restriction or return date
                            || output.OutputBody[(i * 2) + start].Equals('Z')   // Not valid due to zonal restriction
                            || output.OutputBody[(i * 2) + start].Equals('G')   // Not valid due to routeing guide restriction
                            || output.OutputBody[(i * 2) + start].Equals('U')   // Never valid
                            );
				((Fare)fares[i]).QuotaControlled = (output.OutputBody[(i * 2) + start].Equals('Q')); 
			}

			for (int i = delete.Length - 1; i >= 0; i--) 
			{
				if  (delete[i]) 
				{
					fares.RemoveAt(i);
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

		
		public override BusinessObjectOutput FilterOutput(PricingRequestDto request)
		{
			return output;
		}

	}
}
