// *********************************************** 
// NAME                 : SoapExceptionHelper.cs
// AUTHOR               : Marcus Tillett
// DATE CREATED         : 10/01/20056
// DESCRIPTION  		: Helper methods to create SoapException and fault detail block
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/WebServices/EnhancedExposedServices/SoapFault/V1/SoapExceptionHelper.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 13:52:18   mturner
//Initial revision.
//
//   Rev 1.6   Feb 02 2006 14:43:24   mdambrine
//rework on the journey planner enhanced exposed services see CR053_IR_3407 Journey Planner Service Component.doc 
//
//   Rev 1.5   Jan 27 2006 16:26:44   COwczarek
//Change signature of ThrowClientException to take a
//TDException and then use its Identifier property as  value for
//code element.
//Resolution for 3129: Enhanced  Exposed Services Framework Enhancements
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.4   Jan 20 2006 12:58:02   mtillett
//Update throw soap exception methods to allow status to be defined
//Resolution for 3129: Enhanced  Exposed Services Framework Enhancements
//
//   Rev 1.3   Jan 20 2006 11:58:00   COwczarek
//Status element now set conditionally to either Failed or ValidationError
//Resolution for 3129: Enhanced  Exposed Services Framework Enhancements
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.2   Jan 18 2006 14:24:08   mdambrine
//Adding the handling of cjpmessages if they are attached with a tdexception
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.1   Jan 11 2006 09:17:14   mtillett
//Move soap exception helper to SoapFault/V1 namespace and fix up references to methods and unit tests
//Resolution for 3129: Enhanced  Exposed Services Framework Enhancements
//
//   Rev 1.0   Jan 11 2006 09:02:28   mtillett
//Initial revision.
//

using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Web;
using System.Web.Services.Protocols;
using System.Xml;
using TransportDirect.Common; 
using TransportDirect.UserPortal.DepartureBoardService.DepartureBoardFacade;      
using TransportDirect.UserPortal.JourneyControl;

namespace TransportDirect.EnhancedExposedServices.SoapFault.V1
{	
	/// <summary>
	/// Enhanced Exposed SoapExceptionHelper class
	/// </summary>
	public sealed class SoapExceptionHelper
	{
		#region	Default Constructor 
		/// <summary>
		/// Private contructor
		/// </summary>
		private SoapExceptionHelper()
		{}
		#endregion

		#region SoapException helpers
		/// <summary>
		/// Overloaded help method to create a SoapException
		/// </summary>
		/// <param name="faultstring">Overall highlevel error message</param>
		/// <param name="tdException">TDException object</param>
		/// <returns>SoapException</returns>
		public static SoapException ThrowSoapException(string faultstring, TDException tdException)
		{
			XmlNode node = null;
			CJPMessage[] cjpMessages = tdException.AdditionalInformation as CJPMessage[];

			if (cjpMessages == null)
				node = CreateFaultDetailsNode(tdException.Identifier, tdException.Message, ErrorStatus.Failed);
			else
			{
				XmlDocument doc = new XmlDocument();
				XmlNode messages = CreateMessagesNode(doc);								

				foreach(CJPMessage message in cjpMessages)
				{
					AppendExceptionMessage(doc, messages, message.MajorMessageNumber.ToString(CultureInfo.InvariantCulture) , message.MessageText);
				}

				ErrorStatus status;
				if (tdException.Identifier >= TDExceptionIdentifier.JPResolvePostcodeFailed & tdException.Identifier <= TDExceptionIdentifier.JPMissingReturnDestination) 
				{
					status = ErrorStatus.ValidationError;
				} 
				else 
				{
					status = ErrorStatus.Failed;
				}
				node = CreateSoapExceptionNode(doc, messages, status);
			}
			//Throw the exception.    
			return ThrowSoapException(faultstring, SoapException.ServerFaultCode, node);
		}
		/// <summary>
		/// Overloaded help method to create a SoapException
		/// </summary>
		/// <param name="faultstring">Overall highlevel error message</param>
		/// <param name="exception">Exception object</param>
		/// <returns>SoapException</returns>
		public static SoapException ThrowSoapException(string faultstring, Exception exception)
		{
			XmlNode node = CreateFaultDetailsNode(TDExceptionIdentifier.EESGeneralErrorCode, exception.ToString(), ErrorStatus.Failed);
			//Throw the exception.    
			return ThrowSoapException(faultstring, SoapException.ServerFaultCode, node);
		}
		/// <summary>
		/// Overloaded help method to create a SoapException
		/// </summary>
		/// <param name="faultstring">Overall highlevel error message</param>
		/// <param name="errorIdentifier">error code</param>
		/// <param name="errorDescription">detailed error description</param>
		/// <returns>SoapException</returns>
		public static SoapException ThrowSoapException(string faultstring, string errorIdentifier, string errorDescription)
		{
			XmlNode node = CreateFaultDetailsNode(errorIdentifier, errorDescription, ErrorStatus.Failed);
			//Throw the exception.    
			return ThrowSoapException(faultstring, SoapException.ServerFaultCode, node);
		}
		/// <summary>
		/// Overloaded help method to create a SoapException
		/// </summary>
		/// <param name="faultstring">Overall highlevel error message</param>
		/// <param name="dbsMessages">DBSMessage array</param>
		/// <returns>SoapException</returns>
		public static SoapException ThrowSoapException(string faultstring, DBSMessage[] dbsMessages)
		{
			XmlDocument doc = new XmlDocument();
			XmlNode messages = CreateMessagesNode(doc);

			foreach (DBSMessage dbsMessage in dbsMessages)
			{
				AppendExceptionMessage(doc, messages, dbsMessage.Code.ToString(CultureInfo.InvariantCulture), dbsMessage.Description);
			}

			XmlNode node = CreateSoapExceptionNode(doc, messages, ErrorStatus.Failed);
			//Throw the exception.    
			return ThrowSoapException(faultstring, SoapException.ServerFaultCode, node);
		}
		/// <summary>
		/// Overloaded help method to create a SoapException where the 
		/// client has made an invalid request
		/// </summary>
		/// <param name="faultstring">Overall highlevel error message</param>
		/// <param name="exception">Exception object</param>
		/// <returns>SoapException</returns>
		public static SoapException ThrowClientSoapException(string faultstring, TDException exception)
		{
			XmlNode node = CreateFaultDetailsNode(exception.Identifier, exception.ToString(), ErrorStatus.Failed);
			//Throw the exception.    
			return ThrowSoapException(faultstring, SoapException.ClientFaultCode, node);
		}
		/// <summary>
		/// Overloaded help method to create a SoapException where the 
		/// client has made an invalid request
		/// </summary>
		/// <param name="faultstring">Overall highlevel error message</param>
		/// <param name="errorIdentifier">error code</param>
		/// <param name="errorDescription">detailed error description</param>
		/// <returns>SoapException</returns>
		public static SoapException ThrowClientSoapException(string faultstring, string errorIdentifier, string errorDescription)
		{
			XmlNode node = CreateFaultDetailsNode(errorIdentifier, errorDescription, ErrorStatus.Failed);
			//Throw the exception.    
			return ThrowSoapException(faultstring, SoapException.ClientFaultCode, node);
		}
		/// <summary>
		/// Private help method to create SoapException
		/// </summary>
		/// <param name="faultstring">Overall highlevel error message</param>
		/// <param name="code">XmlQualifiedName for Server/Client error code</param>
		/// <param name="detail">XmlNode for details</param>
		/// <returns>SoapException</returns>
		private static SoapException ThrowSoapException(string faultstring, XmlQualifiedName code, XmlNode detail)
		{
			SoapException se = new SoapException(faultstring, code, HttpContext.Current.Request.Url.AbsoluteUri, detail);
			return se;
		}

		#endregion
		
		#region Private Helper Members
		/// <summary>
		/// Overloaded method to create single "Message" node
		/// </summary>
		/// <param name="tdcode">TDExceptionIdentifier code</param>
		/// <param name="description">error description</param>
		/// <returns>XmlNode for Message</returns>
		private static XmlNode CreateFaultDetailsNode(TDExceptionIdentifier tdcode, string description, ErrorStatus status)
		{
			//convert the enum to decimal number
			string code = Enum.Format(typeof(TDExceptionIdentifier), tdcode, "d");
			return CreateFaultDetailsNode(code, description, status);
		}
		/// <summary>
		/// Overloaded method to create single "Message" node
		/// </summary>
		/// <param name="code">error code</param>
		/// <param name="description">error description</param>
		/// <returns>XmlNode for Message</returns>
		private static XmlNode CreateFaultDetailsNode(string code, string description, ErrorStatus status)
		{
			XmlDocument doc = new XmlDocument();
			XmlNode messagesNode = CreateMessagesNode(doc);
			AppendExceptionMessage(doc, messagesNode, code, description);
			return CreateSoapExceptionNode(doc, messagesNode, status);
		}
		/// <summary>
		/// Creates a "Messages" node to hold multiple "Message" nodes
		/// </summary>
		/// <param name="doc">XmlDocument</param>
		/// <returns>Empty XmlNode Message node</returns>
		private static XmlNode CreateMessagesNode(XmlDocument doc)
		{
			XmlNode messagesNode = doc.CreateNode(XmlNodeType.Element, 
				FaultElements.TopLevelErrorNodeName, FaultElements.DetailsNamespace);
			return messagesNode;
		}
		/// <summary>
		/// Builds final detail fault node with 
		/// </summary>
		/// <param name="doc">XmlDocument</param>
		/// <param name="messagesNode">Populated Messages node</param>
		/// <returns>XmlNode populated with detail fault</returns>
		private static XmlNode CreateSoapExceptionNode(XmlDocument doc, XmlNode messagesNode, ErrorStatus status)
		{
			// Build the detail element of the SOAP fault.
			XmlNode node = doc.CreateNode(XmlNodeType.Element, 
				SoapException.DetailElementName.Name, 
				SoapException.DetailElementName.Namespace);    

			XmlNode detailsNode = doc.CreateNode(XmlNodeType.Element, 
				FaultElements.TopLevelNodeName, 
				FaultElements.DetailsNamespace);    

			XmlNode statusNode = doc.CreateNode(XmlNodeType.Element, 
				FaultElements.ErrorStatusNodeName, FaultElements.DetailsNamespace);
            statusNode.InnerText = status.ToString();

			detailsNode.AppendChild(statusNode);
			detailsNode.AppendChild(messagesNode);

			node.AppendChild(detailsNode);
			
			return node; 
		}
		/// <summary>
		/// Creates new Message node with code and description nodes and
		/// appends this to the Messages node
		/// </summary>
		/// <param name="doc">XmlDocument</param>
		/// <param name="messagesNode">Messages node</param>
		/// <param name="code">error code</param>
		/// <param name="description">error description</param>
		private static void AppendExceptionMessage(XmlDocument doc, XmlNode messagesNode, string code, string description)
		{
			XmlNode messageNode = doc.CreateNode(XmlNodeType.Element, 
				FaultElements.ChildLevelErrorNodeName, FaultElements.DetailsNamespace);
			XmlNode codeNode = doc.CreateNode(XmlNodeType.Element, 
				FaultElements.ErrorCodeNodeName, FaultElements.DetailsNamespace);
			codeNode.InnerText = code;
			XmlNode descriptionNode = doc.CreateNode(XmlNodeType.Element, 
				FaultElements.ErrorDescriptionNodeName, FaultElements.DetailsNamespace);
			descriptionNode.InnerText = description;

			messageNode.AppendChild(codeNode);
			messageNode.AppendChild(descriptionNode);
			messagesNode.AppendChild(messageNode);
		}
        	
		#endregion
	}
}
