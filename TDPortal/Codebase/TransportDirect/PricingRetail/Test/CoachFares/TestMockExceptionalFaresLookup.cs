//********************************************************************************
//NAME         : TestMockExceptionalFaresLookup.cs
//AUTHOR       : Murat Guney
//DATE CREATED : 27/10/2005
//DESCRIPTION  : Implementation of TestMockExceptionalFaresLookup class. 
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/Test/CoachFares/TestMockExceptionalFaresLookup.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:37:22   mturner
//Initial revision.
//
//   Rev 1.0   Nov 29 2005 14:57:38   mguney
//Initial revision.


using System;
using System.Globalization;
using System.Collections;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.PricingRetail.CoachFareInterfaces;


namespace TransportDirect.UserPortal.PricingRetail.CoachFares
{
	/// <summary>
	/// TestMockExceptionalFaresLookup. Prepares the mock exceptional fares lookup cache.
	/// </summary>
	public class TestMockExceptionalFaresLookup : IServiceFactory, IExceptionalFaresLookup
	{
		private static TestMockExceptionalFaresLookup current;
		internal const string DAYRETURN1 = "Day Return";
		internal const string DAYRETURN2 = "Standard Day Return";
		internal const string EXCLUDE1 = "route 60";
		internal const string EXCLUDE2 = "Standard route 60";
		
		/// <summary>
		/// Hashtable used to store Coach Operator data
		/// </summary>
		[NonSerializedAttribute]
		private Hashtable CachedData;	



		public TestMockExceptionalFaresLookup()
		{
            CachedData = new Hashtable();
					
			//construct the hash table			
			CachedData.Add(DAYRETURN1.ToUpper(),ExceptionalFaresAction.DayReturn);
			CachedData.Add(DAYRETURN2.ToUpper(),ExceptionalFaresAction.DayReturn);
			CachedData.Add(EXCLUDE1.ToUpper(),ExceptionalFaresAction.Exclude);
			CachedData.Add(EXCLUDE2.ToUpper(),ExceptionalFaresAction.Exclude);			
		}

		#region IServiceFactory Members

		/// <summary>
		/// Returns the current ExceptionalFaresLookup object
		/// </summary>
		/// <returns>ExceptionalFaresLookup object</returns>
		public object Get()
		{
			if (current == null)
				current = new TestMockExceptionalFaresLookup();			
			return current;
		}

		#endregion

		#region IExceptionalFaresLookup Members

		/// <summary>
		/// Method for getting the exceptional fares using the given fare type (fare name or code).
		/// </summary>
		/// <param name="fareType">Fare type</param>
		/// <returns>ExceptionalFaresAction</returns>
		public ExceptionalFaresAction GetExceptionalFaresAction(string fareType)
		{
			if(CachedData.ContainsKey(fareType.ToUpper()))			
				return (ExceptionalFaresAction)CachedData[fareType.ToUpper()];
			else 
			{				
				return ExceptionalFaresAction.NotFound;
			}
		}

		#endregion
	}
}
