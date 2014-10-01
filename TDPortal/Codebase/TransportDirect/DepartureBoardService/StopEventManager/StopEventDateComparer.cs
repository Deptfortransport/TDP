// *********************************************** 
// NAME                 : StopEventDateComparer.cs
// AUTHOR               : Sanjeev Chand
// DATE CREATED         : 15/03/2005 
// DESCRIPTION  		: Comparer class to do StopEvnet arrival/departure time comparison 
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/DepartureBoardService/StopEventManager/StopEventDateComparer.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:21:44   mturner
//Initial revision.
//
//   Rev 1.0   Mar 15 2005 13:49:06   schand
//Initial revision.

using System;
using System.Collections;
using TransportDirect.UserPortal.DepartureBoardService.DepartureBoardFacade ;   
using TransportDirect.JourneyPlanning.CJPInterface;

namespace TransportDirect.UserPortal.DepartureBoardService

{
	/// <summary>
	/// This class will do the datetime (arrival/departure)comparison for StopEvent object.
	/// </summary>
	public class StopEventDateComparer : IComparer 
	{
		public enum SortOrder {	Ascending,	Descending };
		private SortOrder sortOrder;                     
		private bool showDepartures; 

		#region Constructors
		public StopEventDateComparer() 
		{sortOrder = SortOrder.Ascending;}

		public StopEventDateComparer(SortOrder sortOrder, bool showDepartures) 
		{
			this.sortOrder = sortOrder;
			this.showDepartures = showDepartures;
		}

		public StopEventDateComparer(bool showDepartures) 
		{
			this.sortOrder = SortOrder.Ascending;
			this.showDepartures = showDepartures;
		}

		#endregion
		
		

		// Note: Always sorts null entries in the front.
		/// <summary>
		/// Implamentation of IComparer.Compare method
		/// </summary>
		/// <param name="x">StopEvent object 1</param>
		/// <param name="y">StopEvent object 2</param>
		/// <returns>Return 1 if greater, 0 if smaller and -1 if null</returns>
		public int Compare(Object x, Object y) 
		{
			if (x == y) return 0;
			if (x == null) return -1;
			if (y == null) return 1;

			 
			StopEvent stopEvent1 = (StopEvent) x;
			StopEvent stopEvent2 = (StopEvent) y;
			
			DateTime datetime1 ; 
			DateTime datetime2; 

			if (showDepartures)
			{
				datetime1 =	   stopEvent1.stop.departTime ; 
				datetime2 =	   stopEvent2.stop.departTime ;
			}
			else
			{
				datetime1 =	   stopEvent1.stop.arriveTime  ; 
				datetime2 =	   stopEvent2.stop.arriveTime ;
			}


			if (sortOrder == SortOrder.Ascending) 
			{
				return DateTime.Compare(datetime1   , datetime2);
			} 
			else 
			{
				return DateTime.Compare(datetime2, datetime1);
			}
		}

	}

}
