// *********************************************** 
// NAME			: RetailerSortHelper.cs
// AUTHOR		: C.M. Owczarek
// DATE CREATED	: 06/11/03
// DESCRIPTION	: Implements a helper class that provides methods
//                for sorting a collection of RetailerSortDecorator
//                objects into randomized ordered collections
// ************************************************
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/Domain/RetailerSortHelper.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:36:56   mturner
//Initial revision.
//
//   Rev 1.4   Jan 18 2006 18:16:34   RPhilpott
//Chnages for FxCop compliance.
//Resolution for 3398: DN040: Code Review - Unit Test Fixes for DN040
//
//   Rev 1.3   Nov 18 2003 16:10:12   COwczarek
//SCR#247 :Add $Log: for PVCS history

using System;
using System.Collections;

namespace TransportDirect.UserPortal.PricingRetail.Domain
{
	/// <summary>
    /// Implements a helper class that provides methods
    //  for sorting a collection of RetailerSortDecorator
    //  objects into randomized ordered collections
	/// </summary>
	public sealed class RetailerSortHelper 
	{

		// Object used to compare sort key of two instances of RetailerSortDecorator	
		private static readonly Comparer comparer = new Comparer();

		// Class that compares two instances of RetailerSortDecorator
		private class Comparer : IComparer 
		{ 
               
			/// <summary>
			/// Compares the SortKey property of two instances of RetailerSortDecorator
			/// </summary>
			/// <param name="o1">First RetailerSortDecorator</param>
			/// <param name="o2">Second RetailerSortDecorator</param>
			/// <returns>Either -1, 0 or 1 (as for int comparisons)</returns>
			int IComparer.Compare(object o1, object o2) 
			{
				RetailerSortDecorator retailer1 = o1 as RetailerSortDecorator;
				RetailerSortDecorator retailer2 = o2 as RetailerSortDecorator;
				if (retailer1 == null || retailer2 == null) 
				{
					throw new ArgumentException("Parameter type must be RetailerSortDecorator");
				}
				if (retailer1.SortKey > retailer2.SortKey) return 1;
				if (retailer1.SortKey < retailer2.SortKey) return -1;
				return 0;
			}
		}
        /// <summary>
        /// Private ctor to prevent instantiantion of static-only class
        /// </summary>
		private RetailerSortHelper()
		{
		}

        /// <summary>
        /// Sorts the supplied list of Retailer objects and returns a new list containing
        /// RetailerSortDecorator objects sorted in ascending order of their SortKey property
        /// </summary>
        /// <param name="retailers">A list of Retailer objects</param>
        /// <returns>A new list of RetailerSortDecorator objects</returns>
        public static IList SortList(IList retailers) {
            ArrayList sortList = new ArrayList();
            foreach(Retailer retailer in retailers) {
                RetailerSortDecorator decorator = new RetailerSortDecorator(retailer);
                sortList.Add(decorator);                
            }
            object[] sortArray = sortList.ToArray();
            Array.Sort(sortArray, comparer);
            return new ArrayList(sortArray);
        }
		
	}
}