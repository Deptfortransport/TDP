// *********************************************** 
// NAME                 : TravelNewsAssembler.cs
// AUTHOR               : Sanjeev Chand
// DATE CREATED         : 12/01/2006 
// DESCRIPTION  		: Travel News Assembler class for converting domain types to exposed types.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesDataTransfer/TravelNews/V1/TravelNewsAssembler.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:22:50   mturner
//Initial revision.
//
//   Rev 1.3   Jan 23 2006 19:32:32   schand
//FxCOp review
//Resolution for 3454: DEL 8.1 Stream: IR for Module assocaitions for Mobile Service TD111
//
//   Rev 1.2   Jan 20 2006 19:38:42   schand
//Added comment and serialisation attribute
//Resolution for 3454: DEL 8.1 Stream: IR for Module assocaitions for Mobile Service TD111
//
//   Rev 1.1   Jan 16 2006 19:30:14   schand
//Changed collection to array
//Resolution for 3454: DEL 8.1 Stream: IR for Module assocaitions for Mobile Service TD111
//
//   Rev 1.0   Jan 13 2006 15:33:04   schand
//Initial revision.
//Resolution for 3454: DEL 8.1 Stream: IR for Module assocaitions for Mobile Service TD111


using System;
using TransportDirect.EnhancedExposedServices;
using TransportDirect.UserPortal.TravelNews;
using TransportDirect.UserPortal.TravelNewsInterface;
using TransportDirect.EnhancedExposedServices.DataTransfer.Common.V1;     

namespace TransportDirect.EnhancedExposedServices.DataTransfer.TravelNews.V1
{
	/// <summary>
	/// Assembler class for converting domain types to exposed types.
	/// </summary>
	public sealed class TravelNewsAssembler
	{
		#region Public Methods
		/// <summary>
		/// Static assembler method to convert DelayType (domain object) to TravelNewsServiceDelayType (DTO)
		/// </summary>
		/// <param name="delayType">DelayType as domain object</param>
		/// <returns>TravelNewsServiceDelayType as dto object</returns>
		public static TravelNewsServiceDelayType CreateTravelNewsServiceDelayTypeDT(DelayType delayType) 
		{
			return (TravelNewsServiceDelayType) Enum.Parse(typeof(TravelNewsServiceDelayType), delayType.ToString(),true);   
		}

		/// <summary>
		///	 Static assembler method to convert DelayType (domain object) to TravelNewsServiceDelayType (DTO)
		/// </summary>
		/// <param name="delayTypes">DelayType as domain object</param>
		/// <returns>TravelNewsServiceDelayType as dto object</returns>
		public static TravelNewsServiceDelayType[] CreateTravelNewsServiceDelayTypeArrayDT(DelayType[] delayTypes)
		{
			if (delayTypes.Length ==0)
				return new TravelNewsServiceDelayType[0];

			int objectCount=0;
			TravelNewsServiceDelayType[] travelNewsServiceDelayTypes = new TravelNewsServiceDelayType[delayTypes.Length];
			foreach (DelayType delayType in delayTypes)
			{
			   travelNewsServiceDelayTypes[objectCount] = CreateTravelNewsServiceDelayTypeDT(delayType);
			   objectCount++;
			}
			return travelNewsServiceDelayTypes;
		}

		
		/// <summary>
		/// Static assembler method to convert TransportType (domain object) to TravelNewsServiceTransportType (DTO)
		/// </summary>
		/// <param name="transportType">TransportType as domain object</param>
		/// <returns>TravelNewsServiceTransportType as dto object</returns>
		public static TravelNewsServiceTransportType CreateTravelNewsServiceTransportTypeDT(TransportType  transportType) 
		{
			return (TravelNewsServiceTransportType) Enum.Parse(typeof(TravelNewsServiceTransportType), transportType.ToString(),true);   
		}

		/// <summary>
		/// Static assembler method to convert TravelNewsServiceDelayType (DTO) to DelayType (domain object) 
		/// </summary>
		/// <param name="travelNewsServiceDelayType">TravelNewsServiceDelayType as dto object</param>
		/// <returns>DelayType as domain object</returns>
		public static DelayType CreateDelayType(TravelNewsServiceDelayType travelNewsServiceDelayType) 
		{
			return (DelayType) Enum.Parse(typeof(DelayType), travelNewsServiceDelayType.ToString(),true);   
		}

		/// <summary>
		/// Static assembler method to convert TravelNewsServiceDelayType (DTO) to DelayType (domain object) 
		/// </summary>
		/// <param name="travelNewsServiceTransportType"> TravelNewsServiceTransportType as dto object</param>
		/// <returns> TransportType as domain object</returns>
		public static TransportType CreateTransportType(TravelNewsServiceTransportType  travelNewsServiceTransportType) 
		{
			return (TransportType) Enum.Parse(typeof(TransportType), travelNewsServiceTransportType.ToString(),true);   
		}

		/// <summary>
		///	 Static assembler method to convert TravelNewsItem array (domain object) to TravelNewsServiceNewsItem array (DTO)
		/// </summary>
		/// <param name="travelNewsItems">TravelNewsItem array as domain object</param>
		/// <returns>TravelNewsServiceNewsItem array as dto object</returns>
		public static TravelNewsServiceNewsItem[] CreateTravelNewsServiceNewsItemArrayDT(TravelNewsItem[] travelNewsItems)
		{
			if (travelNewsItems.Length ==0)
				return new TravelNewsServiceNewsItem[0];

			int objectCount=0;
			TravelNewsServiceNewsItem[] travelNewsServiceNewsItem = new TravelNewsServiceNewsItem[travelNewsItems.Length];
			foreach(TravelNewsItem travelNewsItem in travelNewsItems)
			{
				travelNewsServiceNewsItem[objectCount] = CreateTravelNewsServiceNewsItemDT(travelNewsItem);
				objectCount++;
			}
			return travelNewsServiceNewsItem;
		}

		/// <summary>
		///	 Static assembler method to convert TravelNewsItem (domain object) to TravelNewsServiceNewsItem (DTO)
		/// </summary>
		/// <param name="travelNewsItem">TravelNewsItem as domain object</param>
		/// <returns>TravelNewsServiceNewsItem as dto object</returns>
		public static TravelNewsServiceNewsItem CreateTravelNewsServiceNewsItemDT(TravelNewsItem travelNewsItem)
		{
			if (travelNewsItem == null)
				return null;

			TravelNewsServiceNewsItem travelNewsServiceNewsItem = new TravelNewsServiceNewsItem();
			travelNewsServiceNewsItem.ClearedDateTime  = travelNewsItem.ClearedDateTime;
			travelNewsServiceNewsItem.DetailText   = travelNewsItem.DetailText;
			travelNewsServiceNewsItem.ExpiryDateTime   = travelNewsItem.ExpiryDateTime;
			travelNewsServiceNewsItem.GridReference   = CommonAssembler.CreateOSGridReferenceDT(travelNewsItem.Easting, travelNewsItem.Northing);
			travelNewsServiceNewsItem.HeadlineText   = travelNewsItem.HeadlineText;
			travelNewsServiceNewsItem.IncidentStatus   = travelNewsItem.IncidentStatus;
			travelNewsServiceNewsItem.IncidentType  = travelNewsItem.IncidentType;
			travelNewsServiceNewsItem.LastModifiedDateTime  = travelNewsItem.LastModifiedDateTime;
			travelNewsServiceNewsItem.Location   = travelNewsItem.Location;
			travelNewsServiceNewsItem.MinutesFromNow  = travelNewsItem.StartToNowMinDiff;
			travelNewsServiceNewsItem.ModeOfTransport   = travelNewsItem.ModeOfTransport;
			travelNewsServiceNewsItem.PublicTransportOperator  = travelNewsItem.PublicTransportOperator;
			travelNewsServiceNewsItem.RegionalOperator   = travelNewsItem.Operator;
			travelNewsServiceNewsItem.Regions   = travelNewsItem.Regions;
			travelNewsServiceNewsItem.RegionsLocation   = travelNewsItem.RegionsLocation;
			travelNewsServiceNewsItem.ReportedDateTime   = travelNewsItem.ReportedDateTime;
			travelNewsServiceNewsItem.SeverityLevel  =  CreateTravelNewsServiceSeverityLevelDT(travelNewsItem.SeverityLevel);
			travelNewsServiceNewsItem.StartDateTime  =  travelNewsItem.StartDateTime;
			travelNewsServiceNewsItem.Uid   =  travelNewsItem.Uid;	
			
			return travelNewsServiceNewsItem;
		}

		
		/// <summary>
		///	 Static assembler method to convert HeadlineItem array (domain object) to TravelNewsServiceHeadlineItem array (DTO)
		/// </summary>
		/// <param name="headlineItems">HeadlineItem array as domain object</param>
		/// <returns>TravelNewsServiceHeadlineItem array as dto object</returns>
		public static TravelNewsServiceHeadlineItem[] CreateTravelNewsServiceHeadlineItemArrayDT(HeadlineItem[] headlineItems)
		{
			if (headlineItems.Length ==0)
				return new TravelNewsServiceHeadlineItem[0];

			int objectCount=0; 
			TravelNewsServiceHeadlineItem[] travelNewsServiceHeadlineItems = new TravelNewsServiceHeadlineItem[headlineItems.Length];
			foreach(HeadlineItem headlineItem in headlineItems)
			{
				if (headlineItem!= null)
				{
					travelNewsServiceHeadlineItems[objectCount] = new TravelNewsServiceHeadlineItem(); 
					travelNewsServiceHeadlineItems[objectCount].DelayTypes  = CreateTravelNewsServiceDelayTypeArrayDT(headlineItem.DelayTypes);  
					travelNewsServiceHeadlineItems[objectCount].HeadlineText   =headlineItem.HeadlineText;  
					travelNewsServiceHeadlineItems[objectCount].Regions  = headlineItem.Regions;  
					travelNewsServiceHeadlineItems[objectCount].SeverityLevel= CreateTravelNewsServiceSeverityLevelDT(headlineItem.SeverityLevel);  
					travelNewsServiceHeadlineItems[objectCount].TransportType  = CreateTravelNewsServiceTransportTypeDT(headlineItem.TransportType);  
					travelNewsServiceHeadlineItems[objectCount].Uid = headlineItem.Uid;  					
				}
				else
				{
					travelNewsServiceHeadlineItems[objectCount] = null;
				}  
				objectCount++;
			}
			return travelNewsServiceHeadlineItems;
		}
        		
		/// <summary>
		///	 Static assembler method to convert SeverityLevel (domain object) to TravelNewsServiceSeverityLevel (DTO)
		/// </summary>
		/// <param name="severityLevel">SeverityLevel as domain object</param>
		/// <returns>TravelNewsServiceSeverityLevel as dto object</returns>
		public static TravelNewsServiceSeverityLevel CreateTravelNewsServiceSeverityLevelDT(SeverityLevel severityLevel)
		{
			return (TravelNewsServiceSeverityLevel) Enum.Parse(typeof(TravelNewsServiceSeverityLevel), severityLevel.ToString(),true);   

		}
		#endregion
	}
}
