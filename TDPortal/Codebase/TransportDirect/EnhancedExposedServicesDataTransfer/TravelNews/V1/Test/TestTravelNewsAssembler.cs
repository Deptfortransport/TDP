// *********************************************** 
// NAME                 : TestTravelNewsAssembler.cs
// AUTHOR               : Sanjeev Chand
// DATE CREATED         : 16/01/2006 
// DESCRIPTION  	    : Test class for Travel News Assember
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesDataTransfer/TravelNews/V1/Test/TestTravelNewsAssembler.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:22:54   mturner
//Initial revision.
//
//   Rev 1.2   Jan 25 2006 11:37:52   schand
//Code reviews
//Resolution for 3454: DEL 8.1 Stream: IR for Module assocaitions for Mobile Service TD111
//
//   Rev 1.1   Jan 17 2006 14:59:16   schand
//Correction to the test
//Resolution for 3454: DEL 8.1 Stream: IR for Module assocaitions for Mobile Service TD111
//
//   Rev 1.0   Jan 17 2006 10:19:12   schand
//Initial revision.
//Resolution for 3454: DEL 8.1 Stream: IR for Module assocaitions for Mobile Service TD111

using System;
using NUnit.Framework;
using TransportDirect.EnhancedExposedServices;
using TransportDirect.UserPortal.TravelNews;
using TransportDirect.UserPortal.TravelNewsInterface;
using TransportDirect.EnhancedExposedServices.DataTransfer.Common.V1;  
using TransportDirect.EnhancedExposedServices.DataTransfer.TravelNews.V1;  


namespace TransportDirect.EnhancedExposedServices.DataTransfer.TravelNews.V1.Test
{
	/// <summary>
	/// Test class for Travel News Assember.
	/// </summary>
	[TestFixture]
	public class TestTravelNewsAssembler
	{  
		string detailText = "some detail text";
		int easting = 11;
		int northing = 12;
		string headlineText = "some headlineText ";		
		string regionOperator = "XYZ";
		string region = "London";		
		DelayType[] delayTypes = new DelayType[2]{DelayType.Major, DelayType.Recent} ;  
 
		SeverityLevel severityLevel	= SeverityLevel.Medium;
		TransportType transportType	= TransportType.PublicTransport;
		string uid = "RTM1234";

		#region Test Methods
		/// <summary>
		/// Test for CreateCodeServiceCodeDetailArrayDT
		/// </summary>
		[Test]
		public void TestCreateTravelNewsServiceDelayTypeDT()
		{
		
			// creating the instance of domain object 
			  DelayType delayType = DelayType.Major;
			 		
			// Getting DTO object from domain object
			 TravelNewsServiceDelayType travelNewsServiceDelayType =   TravelNewsAssembler.CreateTravelNewsServiceDelayTypeDT(delayType);   
  
			// now testing each params
			Assert.IsTrue(travelNewsServiceDelayType.ToString() == delayType.ToString());   
		}

		/// <summary>
		///	 Test for  CreateTravelNewsServiceTransportTypeDT
		/// </summary>
		[Test]
		public void TestCreateTravelNewsServiceTransportTypeDT()
		{
		
			// creating the instance of domain object 
			   TransportType transportType = TransportType.PublicTransport;
            		
			// Getting DTO object from domain object
			TravelNewsServiceTransportType travelNewsServiceTransportType = TravelNewsAssembler.CreateTravelNewsServiceTransportTypeDT(transportType);  
  
			// now testing each params
			Assert.IsTrue(transportType.ToString() ==  travelNewsServiceTransportType.ToString()); 
		}

		/// <summary>
		/// Test for  CreateDelayType
		/// </summary>
		[Test]
		public void TestCreateDelayType()
		{
		
			// creating the instance of DTO object 
			 TravelNewsServiceDelayType travelNewsServiceDelayType = TravelNewsServiceDelayType.Recent; 

		
			// Getting Domain object from dto object
			DelayType delayType =   TravelNewsAssembler.CreateDelayType(travelNewsServiceDelayType); 
			
  
			// now testing each params
			Assert.IsTrue(delayType.ToString() ==  travelNewsServiceDelayType.ToString()); 
		
		}

		/// <summary>
		/// Test for CreateTransportType
		/// </summary>
		[Test]
		public void TestCreateTransportType()
		{      		
			// creating the instance of dto object 
			TravelNewsServiceTransportType travelNewsServiceTransportType = TravelNewsServiceTransportType.PublicTransport;              		
			// Getting domain object from dto object
			TransportType transportType = TravelNewsAssembler.CreateTransportType(travelNewsServiceTransportType);             
			// now testing each params
			Assert.IsTrue(transportType.ToString() ==  travelNewsServiceTransportType.ToString()); 		
		}

		/// <summary>
		/// Test for CreateTravelNewsServiceNewsItemArrayDT
		/// </summary>
		[Test]
		public void TestCreateTravelNewsServiceNewsItemArrayDT()
		{
			// creating the instance of domain object 
			TravelNewsItem[] travelNewsItems = new TravelNewsItem[2];
			for (int i=0; i < travelNewsItems.Length; i++)
			{				
				travelNewsItems[i] = CreateTravelNewsItem(i.ToString());				
			}

			// Getting DTO object from domain object
			TravelNewsServiceNewsItem[] travelNewsServiceNewsItems =  TravelNewsAssembler.CreateTravelNewsServiceNewsItemArrayDT(travelNewsItems); 
  
			// now testing each params
			for (int j=0; j < travelNewsServiceNewsItems.Length; j++)          			
			{                                                        				
				 CheckTravelNewsServiceNewsItemInstance(travelNewsServiceNewsItems[j], j.ToString()); 
			}	
		}

		/// <summary>
		/// Test for CreateTravelNewsServiceNewsItemDT
		/// </summary>
		[Test]
		public void TestCreateTravelNewsServiceNewsItemDT()
		{
		
			// creating the instance of domain object 
			 TravelNewsItem travelNewsItem = new TravelNewsItem();
			
			travelNewsItem = CreateTravelNewsItem("1");				

		
			// Getting DTO object from domain object
			TravelNewsServiceNewsItem travelNewsServiceNewsItem =  TravelNewsAssembler.CreateTravelNewsServiceNewsItemDT(travelNewsItem); 
	
			// now testing each params
			CheckTravelNewsServiceNewsItemInstance(travelNewsServiceNewsItem, "1"); 
		
		}

		/// <summary>
		/// Test for CreateTravelNewsServiceHeadlineItemArrayDT
		/// </summary>
		[Test]
		public void TestCreateTravelNewsServiceHeadlineItemArrayDT()
		{
		
			// creating the instance of domain object 
			HeadlineItem[] headlineItems = new HeadlineItem[2];
			
			for (int i=0; i < headlineItems.Length; i++)
			{				
				headlineItems[i] = CreateHeadlineItemInstance(i.ToString());								
			}
			
			// Getting DTO object from domain object
			 TravelNewsServiceHeadlineItem[] travelNewsServiceHeadlineItem = TravelNewsAssembler.CreateTravelNewsServiceHeadlineItemArrayDT(headlineItems);  
  
			// now testing each params
			for (int j=0; j < travelNewsServiceHeadlineItem.Length; j++)          			
			{
				CheckTravelNewsServiceHeadlineItemInstance(travelNewsServiceHeadlineItem[j], j.ToString()); 
			}
		}

		/// <summary>
		/// Test for CreateTravelNewsServiceSeverityLevelDT
		/// </summary>
		[Test]
		public void CreateTravelNewsServiceSeverityLevelDT()
		{
		
			// creating the instance of domain object 
			  SeverityLevel severitylevel = severityLevel;
            		
			// Getting DTO object from domain object
			TravelNewsServiceSeverityLevel  travelNewsServiceSeverityLevel = TravelNewsAssembler.CreateTravelNewsServiceSeverityLevelDT(severitylevel);  
  
			// now testing each params
			Assert.IsTrue(travelNewsServiceSeverityLevel.ToString() == severitylevel.ToString());   
		}
		
		
		#endregion

		#region Private Methods
		/// <summary>
		/// Helper method to return the new instance of TravelNewsItem
		/// </summary>
		/// <param name="iteration">the position of the array element</param>
		/// <returns>A new instance of TravelNewsItem</returns>
		private TravelNewsItem CreateTravelNewsItem(string iteration)
		{	
			TravelNewsItem travelNewsItem = new TravelNewsItem();
			travelNewsItem.ClearedDateTime = DateTime.Now;
			travelNewsItem.DetailText = detailText + iteration.ToString() ;
			travelNewsItem.Easting = easting ;
			travelNewsItem.Northing = northing ;
			travelNewsItem.ExpiryDateTime = DateTime.Now.AddMinutes(1);
			travelNewsItem.HeadlineText  = headlineText + iteration.ToString();
			travelNewsItem.Operator = regionOperator; 
			travelNewsItem.Regions  = region; 
			return	travelNewsItem;
		}

		/// <summary>
		/// Helper method to compare and test the instance of TravelNewsServiceNewsItem
		/// </summary>
		/// <param name="travelNewsServiceNewsItem">An Instance of TravelNewsServiceNewsItem to compare</param>
		/// <param name="iteration">the position of the array element</param>
		private void CheckTravelNewsServiceNewsItemInstance(TravelNewsServiceNewsItem travelNewsServiceNewsItem, string iteration)
		{
			Assert.IsTrue(travelNewsServiceNewsItem.ClearedDateTime != DateTime.MinValue);
			Assert.IsTrue(travelNewsServiceNewsItem.DetailText == detailText + iteration.ToString()) ;
			Assert.IsTrue(travelNewsServiceNewsItem.GridReference.Easting == easting) ;
			Assert.IsTrue(travelNewsServiceNewsItem.GridReference.Northing == northing) ;
			Assert.IsTrue(travelNewsServiceNewsItem.HeadlineText  == headlineText + iteration.ToString());
			Assert.IsTrue(travelNewsServiceNewsItem.RegionalOperator == regionOperator); 
			Assert.IsTrue(travelNewsServiceNewsItem.Regions  == region); 
		}

		/// <summary>
		///	Helper method to return the new instance of HeadlineItem
		/// </summary>
		/// <param name="iteration">the position of the array element</param>
		/// <returns>A new instance of HeadlineItem</returns>
		private HeadlineItem CreateHeadlineItemInstance(string iteration)
		{   		 
			HeadlineItem headlineItem = new HeadlineItem();
			headlineItem.DelayTypes = delayTypes;
			headlineItem.HeadlineText = headlineText + iteration;
			headlineItem.Regions = region;
			headlineItem.SeverityLevel =  severityLevel;
			headlineItem.TransportType = transportType;
			headlineItem.Uid = uid;
			return headlineItem;  
		}

		/// <summary>
		/// Helper method to compare and test the instance of TravelNewsServiceHeadlineItem
		/// </summary>
		/// <param name="travelNewsServiceHeadlineItem">An Instance of TravelNewsServiceHeadlineItem to compare</param>
		/// <param name="iteration">the position of the array element</param>
		private  void CheckTravelNewsServiceHeadlineItemInstance(TravelNewsServiceHeadlineItem travelNewsServiceHeadlineItem, string iteration)
		{
			Assert.IsTrue(travelNewsServiceHeadlineItem.DelayTypes.Length == delayTypes.Length);
			Assert.IsTrue(travelNewsServiceHeadlineItem.DelayTypes[0].ToString()  == delayTypes[0].ToString());
			Assert.IsTrue(travelNewsServiceHeadlineItem.HeadlineText == headlineText + iteration);
			Assert.IsTrue(travelNewsServiceHeadlineItem.Regions == region);
			Assert.IsTrue(travelNewsServiceHeadlineItem.SeverityLevel.ToString()  ==  severityLevel.ToString() );
			Assert.IsTrue(travelNewsServiceHeadlineItem.TransportType.ToString() == transportType.ToString());
			Assert.IsTrue(travelNewsServiceHeadlineItem.Uid == uid);
		}
		#endregion
	}
}
