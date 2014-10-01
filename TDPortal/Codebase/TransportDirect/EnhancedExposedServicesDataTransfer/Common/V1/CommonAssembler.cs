// *********************************************** 
// NAME                 : CommonAssembler.cs
// AUTHOR               : Manuel Dambrine
// DATE CREATED         : 04/01/2006
// DESCRIPTION  		: CommonAssembler Class.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesDataTransfer/Common/V1/CommonAssembler.cs-arc  $
//
//   Rev 1.1   Aug 04 2009 13:38:08   mmodi
//Added method to create a CompletionStatus and Message objects
//Resolution for 5307: CCN517a Web Service Find a Car Route
//
//   Rev 1.0   Nov 08 2007 12:22:16   mturner
//Initial revision.
//
//   Rev 1.12   Feb 24 2006 12:15:54   RWilby
//Fixed problem with merge of stream3129
//
//   Rev 1.11   Feb 24 2006 10:39:46   RWilby
//Merged 3129
//
//   Rev 1.10   Feb 01 2006 16:25:56   halkatib
//Changes made as part of code review.
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.9   Jan 26 2006 18:10:10   halkatib
//Implemented fixes for the commonassembler and the journeyplannerassembler
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.8   Jan 20 2006 10:48:32   halkatib
//Fixed bugs revealed by unit test
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.7   Jan 16 2006 10:43:30   schand
//Added overloaded method for CreateOSGridReferenceDT
//Resolution for 3454: DEL 8.1 Stream: IR for Module assocaitions for Mobile Service TD111
//
//   Rev 1.6   Jan 13 2006 14:07:00   halkatib
//Added functionality required by the journeyplanner webservices.
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.5   Jan 12 2006 15:34:12   RWilby
//Implemented CreateOSGridReferenceDT and CreateTDLocation methods.
//Resolution for 3410: DEL 8.1 Stream: IR for Module assocaitions for Digi TV TD110
//
//   Rev 1.4   Jan 10 2006 17:24:12   mdambrine
//Changed from resourcemanager base to TDResourcemanger as the resourcemanager project changed
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.3   Jan 06 2006 16:37:46   mdambrine
//Spelling mistake in common, should be with a capital C
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.2   Jan 06 2006 16:36:46   mdambrine
//Spelling mistake in common, should be with a capital C
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.1   Jan 05 2006 15:53:02   mdambrine
//changed the namespace from transportdirect.resourcemanager to TransportDirect.common.ResourceManager
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.0   Jan 04 2006 11:05:18   mdambrine
//Initial revision.
//Resolution for 3407:  DEL 8.1 Stream: IR for Module associations for Lauren  TD103

using System;
using System.Collections;
using TransportDirect.Common;
using System.Text;
using TransportDirect.Common.ResourceManager;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.LocationService;
using cjpInterface = TransportDirect.JourneyPlanning.CJPInterface;
using journeyControl = TransportDirect.UserPortal.JourneyControl;
using locationService = TransportDirect.UserPortal.LocationService;

namespace TransportDirect.EnhancedExposedServices.DataTransfer.Common.V1
{
	/// <summary>
	/// Class responible for the inter-conversion of domain types and DTO types for the Common DTOs
	/// </summary>
	public sealed class CommonAssembler
	{

		#region static methods
		/// <summary>
		/// Creates a completion status DTO object from the domain object supplied
		/// </summary>
		/// <param name="valid"></param>
		/// <param name="messages"></param>
		/// <param name="rm"></param>
		/// <returns></returns>
		public static CompletionStatus CreateCompletionStatusDT(bool valid,
														  CJPMessage[] messages,
														  TDResourceManager rm)
		{			
			CompletionStatus csDTO = new CompletionStatus();
			if (valid)
				csDTO.Status = StatusType.Success;
			else 			
			{
				csDTO.Status = StatusType.Failed;
				ArrayList messageDTOArrayList = new ArrayList();
				if (messages != null)
				{
						foreach(CJPMessage cjpm in messages)
						{
							if (cjpm.Type == ErrorsType.Error)
							{
								Message messageDTO = new Message();
								messageDTO.Text = rm.GetString(cjpm.MessageResourceId);
								messageDTO.Code = cjpm.MajorMessageNumber;
								//add message to array
								messageDTOArrayList.Add(messageDTO);
							}
						}
					//(ModeType)Enum.Parse(typeof(ModeType), mode.ToString());
					csDTO.Messages = (Message[])messageDTOArrayList.ToArray(typeof(Message));
				}
				else 
					csDTO.Messages = null;
			}
			return csDTO;
		}

		/// <summary>
		/// Creates a completion status DTO object from the exception object supplied
		/// </summary>
		/// <param name="exception"></param>
		/// <returns></returns>
		public static CompletionStatus CreateCompletionStatusDT(Exception exception)
		{
			if (exception != null)
			{
				CompletionStatus csDTO = new CompletionStatus();
				Message messageDTO = new Message();
				if (exception is TDException)
				{
					TDException tdException = (TDException)exception;
					csDTO.Status = MapExceptionIdentifierToStatus(tdException.Identifier);			
					messageDTO.Text = tdException.Message;
					string code = Enum.Format(typeof(TDExceptionIdentifier), tdException.Identifier, "d");
					messageDTO.Code = int.Parse(code); 
				}
				else
				{
					csDTO.Status = StatusType.Failed;
					messageDTO.Text = exception.Message;
					messageDTO.Code = 0;
				}			
				Message[] messagesDTO = new Message[]{messageDTO};
				csDTO.Messages = messagesDTO;
				return csDTO;
			}
			else 
				return null;
		}

        /// <summary>
        /// Creates a CompletionStatus object for valid flag, code and message passed.
        /// If valid, no message is added.
        /// </summary>
        public static CompletionStatus CreateCompletionStatusDT(bool valid, int code, string message)
        {
            CompletionStatus csDTO = new CompletionStatus();
            csDTO.Status = valid ? StatusType.Success : StatusType.Failed;

            // Add message if this is an failed status
            if (!valid)
            {
                Message messageDTO = new Message();
                messageDTO.Code = code;
                messageDTO.Text = message;

                csDTO.Messages = new Message[] { messageDTO };
            }

            return csDTO;
        }

        /// <summary>
        /// Creates a Message object
        /// </summary>
        /// <param name="message"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public static Message CreateMessageDT(string message, int code)
        {
            Message messageDTO = new Message();
            messageDTO.Text = message;
            messageDTO.Code = code;

            return messageDTO;
        }

		/// <summary>
		/// Creates a Naptan DTO from the tdnaptan domain object.
		/// </summary>
		/// <param name="naptan"></param>
		/// <returns></returns>
		public static Naptan CreateNaptanDT(TDNaptan naptan)
		{
			if (naptan != null)
			{
				Naptan naptanDTO = new Naptan();
				naptanDTO.GridReference = CreateOSGridReferenceDT(naptan.GridReference);
				naptanDTO.NaptanId = naptan.Naptan;
				naptanDTO.Name = naptan.Name;
				return naptanDTO;
			}
			else
			{
				return null;
			}
		}

		/// <summary>
		/// Creates OSGridReference DTO object from the domain object
		/// </summary>
		/// <param name="gridReference">EnhancedExposedServices.DataTransfer.Common.V1.OSGridReference.OSGridReference</param>
		/// <returns>LocationService.OSGridReference</returns>
		public static OSGridReference CreateOSGridReferenceDT(locationService.OSGridReference gridReference)
		{
			if (gridReference != null)
			{
				OSGridReference osgrDTO = new OSGridReference();
				osgrDTO.Easting = gridReference.Easting;
				osgrDTO.Northing = gridReference.Northing;

				return osgrDTO;
			}
			else 
			{
				return null;
			}
		}

		
		/// <summary>
		///	 Creates OSGridReference DTO object from easting and northing parameters
		/// </summary>
		/// <param name="easting">easting as int </param>
		/// <param name="northing">northing as int </param>
		/// <returns>OSGridReference object</returns>
		public static OSGridReference CreateOSGridReferenceDT(int easting, int northing)
		{
			OSGridReference oSGridReference = new OSGridReference();
			oSGridReference.Easting = easting;
			oSGridReference.Northing = northing; 
			return oSGridReference;
		}

		/// <summary>
		/// Creates TDLocation object from OSGridReference object
		/// </summary>
		/// <param name="gridReference">OSGridReference</param>
		/// <returns>TDLocation</returns>
		public static TDLocation CreateTDLocation(OSGridReference gridReference)
		{
			TDLocation tdLocation = new TDLocation();
			tdLocation.GridReference.Easting = gridReference.Easting;
			tdLocation.GridReference.Northing = gridReference.Northing;

			return tdLocation;
		}

		/// <summary>
		/// Determinses the status based on the supplied tdexceptionidenitfire enum value
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public static StatusType MapExceptionIdentifierToStatus(TDExceptionIdentifier id)
		{			
			StatusType statusType = new StatusType();
			string code = Enum.Format(typeof(TDExceptionIdentifier), id, "d");
			int idcode = int.Parse(code);
			if (idcode >= Convert.ToInt32(TDExceptionIdentifier.JPResolvePostcodeFailed) &&
				idcode <= Convert.ToInt32(TDExceptionIdentifier.JPMissingReturnDestination))
			{
				statusType = StatusType.ValidationError;
			}
			else
			{
				statusType = StatusType.Failed;
			}			
			return statusType;			
		}

		#endregion

	}

} 
