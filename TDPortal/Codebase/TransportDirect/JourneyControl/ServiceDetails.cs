// *********************************************** 
// NAME			: ServiceDetails.cs
// AUTHOR		: Andrew Toner
// DATE CREATED	: 10/08/2003 
// DESCRIPTION	: Implementation of the ServiceDetails class
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyControl/ServiceDetails.cs-arc  $
//
//   Rev 1.1   Apr 15 2010 10:59:28   mturner
//Removed logic that strips leading or trailing : and * chars from a operator code.
//Resolution for 5513: No operator link appears for NX services in Trapeze regions
//
//   Rev 1.0   Nov 08 2007 12:23:58   mturner
//Initial revision.
//
//   Rev 1.7   Aug 25 2005 14:41:16   RPhilpott
//Pass Retail Train Id to RVBO in place of UID.
//Resolution for 2710: NRS interface -- retail train id needed
//
//   Rev 1.6   Jan 05 2004 16:43:26   RPhilpott
//Check for null operatorCode before truncating it.
//
//   Rev 1.5   Dec 16 2003 16:51:20   COwczarek
//Trim leading or trailing ":" or "*" from operator code passed into constructor
//Resolution for 417: Trimming OperatorCode Fields
//
//   Rev 1.4   Oct 15 2003 21:52:54   acaunt
//direction and privateId attributes added
//
//   Rev 1.3   Sep 26 2003 11:47:16   geaton
//Added default constructor to allow XML serialisation of class for use by Event Logging Service publishers.
//
//   Rev 1.2   Sep 11 2003 16:34:14   jcotton
//Made Class Serializable
//
//   Rev 1.1   Aug 20 2003 17:55:52   AToner
//Work in progress
using System;

namespace TransportDirect.UserPortal.JourneyControl
{
	/// <summary>
	/// Summary description for ServiceDetails.
	/// </summary>
	[Serializable()]
	public class ServiceDetails
	{
		private string opCode;
		private string opName;
		private string servNumber;
		private string destBoard;
		private string dir;
		private string privId;
		private string retailId;


		/// <summary>
		/// Default constructor - defined to allow XML serialisation.
		/// </summary>
		public ServiceDetails()
		{}

		public ServiceDetails( string operatorCode,
								string operatorName,
								string serviceNumber,
								string destinationBoard,
								string direction,
								string privateId,
								string retailTrainId)
		{
    		opCode = operatorCode;
    		opName = operatorName;
			servNumber = serviceNumber;
			destBoard = destinationBoard;
			dir = direction;
			privId = privateId;
			retailId = retailTrainId;
		}

		public string OperatorCode
		{
			get { return opCode; }
		}

		public string OperatorName
		{
			get { return opName; }
		}

		public string ServiceNumber
		{
			get { return servNumber; }
		}

		public string DestinationBoard
		{
			get { return destBoard; }
		}

		public string Direction
		{
			get { return dir; }
		}

		public string PrivateId
		{
			get { return privId; }
		}

		public string RetailTrainId
		{
			get { return retailId; }
		}
	}
}
