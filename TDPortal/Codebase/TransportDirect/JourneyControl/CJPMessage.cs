// *********************************************** 
// NAME			: CJPMessage.cs
// AUTHOR		: Andrew Toner
// DATE CREATED	: 10/08/2003 
// DESCRIPTION	: Implementation of the CJPMessage class
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyControl/CJPMessage.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:23:40   mturner
//Initial revision.
//
//   Rev 1.7   Aug 19 2005 14:03:46   jgeorge
//Automatically merged from branch for stream2572
//
//   Rev 1.6.1.0   Jul 27 2005 10:24:20   asinclair
//Added ErrorsType enum
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.6   Sep 22 2003 19:35:22   RPhilpott
//CJPMessage enhancements following receipt of new interface from Atkins
//
//   Rev 1.5   Sep 11 2003 16:34:08   jcotton
//Made Class Serializable
//
//   Rev 1.4   Sep 10 2003 15:33:04   RPhilpott
//Made read-only properties read/write (just to keep WSDL happy ... grrr)
//
//   Rev 1.3   Sep 10 2003 11:13:54   RPhilpott
//Changes to CJPMessage handling.
//
//   Rev 1.2   Aug 20 2003 17:55:42   AToner
//Work in progress

using System;

namespace TransportDirect.UserPortal.JourneyControl
{


	public enum ErrorsType
	{
		Error,
		Warning
	} 


	/// <summary>
	/// Summary description for CJPMessage.
	/// </summary>
	[Serializable()]
	public class CJPMessage
	{
		private string messageText;
		private string messageResourceId;
		private int minorMessageNumber;
		private int majorMessageNumber;
		private ErrorsType type;


		/// <summary>
		/// Default constructor added only to support use by WSDL tool
		/// </summary>
		public CJPMessage()
		{
		}

		public CJPMessage(string messageText, string messageResourceId, int majorNumber, int minorNumber)
		{
			this.messageText = messageText;
			this.messageResourceId = messageResourceId;
			this.majorMessageNumber = majorNumber;
			this.minorMessageNumber = minorNumber;
		}

		public CJPMessage(string messageText, string messageResourceId, int majorNumber, int minorNumber, ErrorsType type)
		{
			this.messageText = messageText;
			this.messageResourceId = messageResourceId;
			this.majorMessageNumber = majorNumber;
			this.minorMessageNumber = minorNumber;
			this.type = type;
		}

		public string MessageText
		{
			get { return messageText; }
			set { messageText = value; }
		}
		public string MessageResourceId
		{
			get { return messageResourceId; }
			set { messageResourceId = value; }
		}

		public int MajorMessageNumber
		{
			get { return majorMessageNumber; }
			set { majorMessageNumber = value; }
		}

		public int MinorMessageNumber
		{
			get { return minorMessageNumber; }
			set { minorMessageNumber = value; }
		}

		public ErrorsType Type
		{
			get {return type;}
			set {type = value;}
		}

	}
}
