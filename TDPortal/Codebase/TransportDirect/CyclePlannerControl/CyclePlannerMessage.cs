// *********************************************** 
// NAME			: CyclePlannerMessage.cs
// AUTHOR		: Mitesh Modi
// DATE CREATED	: 10/06/2008
// DESCRIPTION	: Class to make Cycle Planner messages useable by the Portal
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/CyclePlannerControl/CyclePlannerMessage.cs-arc  $
//
//   Rev 1.0   Jun 20 2008 15:42:00   mmodi
//Initial revision.
//

using System;
using System.Collections.Generic;
using System.Text;

namespace TransportDirect.UserPortal.CyclePlannerControl
{
    public enum ErrorsType
    {
        Error,
        Warning
    }

    [Serializable()]
    public class CyclePlannerMessage
    {
        private string messageText;
		private string messageResourceId;
		private int minorMessageNumber;
		private int majorMessageNumber;
		private ErrorsType type;


		/// <summary>
		/// Default constructor
		/// </summary>
		public CyclePlannerMessage()
		{
		}

		public CyclePlannerMessage(string messageText, string messageResourceId, int majorNumber, int minorNumber)
		{
			this.messageText = messageText;
			this.messageResourceId = messageResourceId;
			this.majorMessageNumber = majorNumber;
			this.minorMessageNumber = minorNumber;
		}

        public CyclePlannerMessage(string messageText, string messageResourceId, int majorNumber, int minorNumber, ErrorsType type)
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
