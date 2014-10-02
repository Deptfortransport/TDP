// *********************************************** 
// NAME                 : HeadlineItem.cs
// AUTHOR               : Patrick ASSUIED
// DATE CREATED         : 16/12/2004 
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/TravelNewsInterface/HeadlineItem.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:50:38   mturner
//Initial revision.
//
//   Rev 1.3   Aug 18 2005 14:26:40   jgeorge
//Added header blocks
//Resolution for 2558: Del 8 Stream: Incident mapping

using System;
using System.Data;

namespace TransportDirect.UserPortal.TravelNewsInterface
{
	/// <summary>
	/// Represents a travel news headline
	/// </summary>
	[Serializable]
	public class HeadlineItem
	{
		
		private string sUid = string.Empty;
		private string sHeadlineText = string.Empty;
		private SeverityLevel slSeverityLevel;
		private TransportType ttTransportType= TransportType.All;
		private string sRegions = string.Empty;
		private DelayType[] dltDelayTypes = new DelayType[0];
	
		public HeadlineItem()
		{
		}

		public HeadlineItem(DataRow headlineRow)
		{
			
		}

		public string Uid
		{
			get
			{
				return sUid;
			}
			set
			{
				sUid = value;
			}
		}

		public string HeadlineText
		{
			get
			{
				return sHeadlineText;
			}
			set
			{
				sHeadlineText = value;
			}
		}

		public SeverityLevel SeverityLevel
		{
			get
			{
				return slSeverityLevel;
			}
			set
			{
				slSeverityLevel = value;
			}
		}

		public TransportType TransportType
		{
			get{ return ttTransportType;}
			set{ ttTransportType = value;}
		}

		public string Regions
		{
			get{ return sRegions;}
			set{ sRegions = value;}
		}

		public DelayType[] DelayTypes
		{
			get{ return dltDelayTypes;}
			set{ dltDelayTypes = value;}
		}


	}
}
