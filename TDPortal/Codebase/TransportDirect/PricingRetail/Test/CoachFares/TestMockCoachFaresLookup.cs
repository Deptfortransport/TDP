//********************************************************************************
//NAME         : TestMockCoachFaresLookup.cs
//AUTHOR       : Mitesh Modi
//DATE CREATED : 09/05/2007
//DESCRIPTION  : Implementation of TestMockCoachFaresLookup class. 
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/Test/CoachFares/TestMockCoachFaresLookup.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:37:20   mturner
//Initial revision.
//
//   Rev 1.0   May 09 2007 14:49:20   mmodi
//Initial revision.
//Resolution for 4401: DEL 9.6 Stream: National Express New Fares Main Portal
//

using System;
using System.Globalization;
using System.Collections;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.PricingRetail.CoachFareInterfaces;

namespace TransportDirect.UserPortal.PricingRetail.CoachFares
{
	/// <summary>
	/// Summary description for TestMockCoachFaresLookup. Prepares the mock coach fares lookup cache.
	/// </summary>
	public class TestMockCoachFaresLookup : IServiceFactory, ICoachFaresLookup
	{
		private static TestMockCoachFaresLookup current;

		internal const string FLX = "FLX";
		internal const string ANR = "ANR";
		internal const string RNA = "RNA";
		internal const string RST = "RST";

		/// <summary>
		/// Hashtable used to store coach fares Action data
		/// </summary>
		[NonSerializedAttribute]
		private Hashtable hashCoachFareAction;

		/// <summary>
		/// Hashtable used to store Coach Fares Restriction data
		/// </summary>
		[NonSerializedAttribute]
		private Hashtable hashCoachFareRestictionPriority;

		#region Constructor
		public TestMockCoachFaresLookup()
		{
            hashCoachFareAction = new Hashtable();
					
			//construct the hash table			
			hashCoachFareAction.Add(FLX, CoachFaresAction.Include);
			hashCoachFareAction.Add(ANR, CoachFaresAction.Include);
			hashCoachFareAction.Add(RNA, CoachFaresAction.Include);
			hashCoachFareAction.Add(RST, CoachFaresAction.Include);

			hashCoachFareRestictionPriority = new Hashtable();

			//construct the hash table			
			hashCoachFareRestictionPriority.Add(FLX, (int)1);
			hashCoachFareRestictionPriority.Add(ANR, (int)2);
			hashCoachFareRestictionPriority.Add(RNA, (int)3);
			hashCoachFareRestictionPriority.Add(RST, (int)4);
		}
		#endregion

		#region IServiceFactory Members

		/// <summary>
		/// Returns the current CoachFaresLookup object
		/// </summary>
		/// <returns>CoachFaresLookup object</returns>
		public object Get()
		{
			if (current == null)
				current = new TestMockCoachFaresLookup();			
			return current;
		}

		#endregion

		#region ICoachFaresLookup Members

		/// <summary>
		/// Method for getting the coach fares using the given fare type (fare type code).
		/// </summary>
		/// <param name="fareType">Fare type</param>
		/// <returns>CoachFaresAction</returns>
		public CoachFaresAction GetCoachFaresAction(string fareTypeCode)
		{
			if(hashCoachFareAction.ContainsKey(fareTypeCode.ToUpper()))			
				return (CoachFaresAction)hashCoachFareAction[fareTypeCode.ToUpper()];
			else 
			{				
				return CoachFaresAction.NotFound;
			}
		}

		/// <summary>
		/// Method for getting the coach fares restriction priority using the given fare type (fare type code).
		/// </summary>
		/// <param name="fareType">Fare type code</param>
		/// <returns>Restriction priority as int</returns>
		public int GetCoachFaresRestriction(string fareTypeCode)
		{
			if(hashCoachFareRestictionPriority.ContainsKey(fareTypeCode.ToUpper()))			
				return (int)hashCoachFareRestictionPriority[fareTypeCode.ToUpper()];
			else 
			{				
				return 0;
			}
		}

		#endregion

	}
}
