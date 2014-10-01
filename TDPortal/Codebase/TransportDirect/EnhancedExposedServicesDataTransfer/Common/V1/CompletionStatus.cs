// *********************************************** 
// NAME                 : CompletionStatus.cs
// AUTHOR               : Manuel Dambrine
// DATE CREATED         : 04/01/2006
// DESCRIPTION  		: CompletionStatus Class.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesDataTransfer/Common/V1/CompletionStatus.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:22:16   mturner
//Initial revision.
//
//   Rev 1.4   Mar 03 2006 15:42:54   mtillett
//Add Read/write to properties to pass code review CR056
//
//   Rev 1.3   Feb 02 2006 14:43:16   mdambrine
//rework on the journey planner enhanced exposed services see CR053_IR_3407 Journey Planner Service Component.doc 
//
//   Rev 1.2   Jan 25 2006 16:18:20   mdambrine
//Adding serialization attribute
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.1   Jan 19 2006 14:37:42   mdambrine
//Added property setters
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.0   Jan 04 2006 11:05:18   mdambrine
//Initial revision.
//Resolution for 3407:  DEL 8.1 Stream: IR for Module associations for Lauren  TD103

namespace TransportDirect.EnhancedExposedServices.DataTransfer.Common.V1
{
	[System.Serializable]
	public class CompletionStatus
	{
		private StatusType status;
		private Message[] messages;

		/// <summary>
		/// constructor
		/// </summary>
		public CompletionStatus()
		{
			
		}

		/// <summary>
		/// Read/write status the service is in
		/// </summary>
		public StatusType Status
		{
			get
			{
				return status;
			}
			set
			{
				status = value;
			}
		}

		/// <summary>
		/// Read/write message with this service
		/// </summary>
		public Message[] Messages
		{
			get
			{
				return messages;
			}
			set
			{
				messages = value;
			}
		}
	}

}
