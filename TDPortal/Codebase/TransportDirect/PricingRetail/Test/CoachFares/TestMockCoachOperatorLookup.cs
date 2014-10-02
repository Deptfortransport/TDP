//********************************************************************************
//NAME         : TestMockCoachOperatorLookup.cs
//AUTHOR       : Murat Guney
//DATE CREATED : 27/10/2005
//DESCRIPTION  : Implementation of TestMockCoachOperatorLookup class. 
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/Test/CoachFares/TestMockCoachOperatorLookup.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:37:20   mturner
//Initial revision.
//
//   Rev 1.5   Nov 25 2005 12:47:24   mguney
//Web services url changed to be the local stub web service url.
//Resolution for 3213: IF098 Interface Stub: Issues with displaying fares from IF098 stub
//
//   Rev 1.4   Nov 04 2005 16:19:10   RPhilpott
//NUnit fixes.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.3   Oct 31 2005 11:40:30   mguney
//SC code changed to SCL.
//
//   Rev 1.2   Oct 29 2005 17:00:24   mguney
//New operator codes included.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.1   Oct 27 2005 17:24:48   mguney
//Associated IR.
//Resolution for 2818: DEL 8 Stream: Search by Price
//
//   Rev 1.0   Oct 27 2005 17:24:02   mguney
//Initial revision.

using System;
using System.Globalization;
using System.Collections;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.PricingRetail.CoachFareInterfaces;


namespace TransportDirect.UserPortal.PricingRetail.CoachFares
{
	/// <summary>
	/// TestMockCoachOperatorLookup. Prepares a operator code list and exposes the GetOperatorDetails method.
	/// Codes are O1,02...O9. The even codes are for journey and the odd ones are for the route types.
	/// </summary>
	public class TestMockCoachOperatorLookup : IServiceFactory, ICoachOperatorLookup
	{
		private static TestMockCoachOperatorLookup current;
		private const string NXCODE = "NX";
		private const string SCLCODE = "SCL";
		private const string WEBSERVICEURL = "http://localhost/CoachFaresWebService/CoachFaresService.asmx";
		/// <summary>
		/// Hashtable used to store Coach Operator data
		/// </summary>
		[NonSerializedAttribute]
		private Hashtable CachedData;	



		public TestMockCoachOperatorLookup()
		{
			CoachOperator coachOperator;
			CachedData = new Hashtable();

			for (int i=0;i < 10;i++)
			{
				CoachFaresInterfaceType interfaceType;
				string tdpOperatorCode;
				string url;
				if (i % 2 == 0)
				{
					interfaceType = CoachFaresInterfaceType.ForJourney;
					tdpOperatorCode = NXCODE;
					url = string.Empty;
				}
				else 
				{
					interfaceType = CoachFaresInterfaceType.ForRoute;
					tdpOperatorCode = SCLCODE;
					url = WEBSERVICEURL;
				}

				coachOperator = 
					new CoachOperator(interfaceType,tdpOperatorCode,"NAME" + i.ToString(),url);		
				//add the constructed object to the hash table
				string cjpOperatorCode = "O" + i.ToString();
				CachedData.Add(cjpOperatorCode.ToUpper(),coachOperator);
			}

			coachOperator = new CoachOperator(CoachFaresInterfaceType.ForJourney,NXCODE,"NATEX",string.Empty);		
			//add the constructed object to the hash table			
			CachedData.Add(NXCODE.ToUpper(),coachOperator);			
			CachedData.Add("AB",coachOperator);
			CachedData.Add("CD",coachOperator);
			CachedData.Add("CX",coachOperator);
			CachedData.Add("ML",coachOperator);
			CachedData.Add("AL",coachOperator);
			CachedData.Add("*SC",coachOperator);
			CachedData.Add("SC:",coachOperator);
			CachedData.Add("ABD",coachOperator);
			CachedData.Add("XYZ",coachOperator);

			coachOperator = new CoachOperator(CoachFaresInterfaceType.ForRoute,SCLCODE,"SC",WEBSERVICEURL);		
			//add the constructed object to the hash table			
			CachedData.Add(SCLCODE,coachOperator);
			CachedData.Add("SC",coachOperator);

		}

		#region IServiceFactory Members

		/// <summary>
		/// Returns the current CoachOperatorLookup object
		/// </summary>
		/// <returns>CoachOperatorLookup object</returns>
		public object Get()
		{
			if (current == null)
				current = new TestMockCoachOperatorLookup();			
			return current;
		}

		#endregion

		#region ICoachOperatorLookup Members

		/// <summary>
		/// Method for getting the coach operator details using the given cjp operator code.
		/// </summary>
		/// <param name="operatorCode"></param>
		/// <returns></returns>
		public CoachOperator GetOperatorDetails(string cjpOperatorCode)
		{
			if(CachedData.ContainsKey(cjpOperatorCode.ToUpper()))			
				return (CoachOperator)CachedData[cjpOperatorCode.ToUpper()];
			else 
			{				
				return null;
			}
		}

		#endregion
	}
}
