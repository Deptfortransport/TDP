// *********************************************** 
// NAME                 : Message.cs
// AUTHOR               : C.M. Owczarek
// DATE CREATED         : 28.03.06
// DESCRIPTION  		: This class represents a journey result message
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EnhancedExposedServicesDataTransfer/OpenJourneyPlanner/V1/Message.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:22:38   mturner
//Initial revision.
//
//   Rev 1.0   Apr 06 2006 16:21:48   COwczarek
//Initial revision.
//Resolution for 3754: IR for module associations for Open Journey Planner Exposed Service
//

using System;
namespace TransportDirect.EnhancedExposedServices.DataTransfer.OpenJourneyPlanner.V1 
{

    /// <summary>
    /// This class represents a journey result message
    /// </summary>
    [Serializable]
    public class Message
    {
        
        private int code;
        private string description;
        private int subCode;
        private string region;
        private int subClass;
        private MessageType type;

        public Message(){}

        /// <summary>
        /// Read/write property that is the message code.
        /// </summary>
        public int Code
        {
            get {  return code; }
            set { code = value; }    
        }    

        /// <summary>
        /// Read/write property that is the message text. 
        /// </summary>
        public string Description
        {
            get {  return description; }
            set { description = value; }    
        }

        /// <summary>
        /// Read/write property that is the sub classification code returned by the Rail Planning Engine.
        /// </summary>
        public int SubCode
        {
            get {  return subCode; }
            set { subCode = value; }    
        }

        /// <summary>
        /// Read/write property that is the Id of Traveline region that raised the error.
        /// </summary>
        public string Region
        {
            get {  return region; }
            set { region = value; }    
        }

        /// <summary>
        /// Read/write property that is the tub classification code returned by JourneyWeb.
        /// </summary>
        public int SubClass
        {
            get {  return subClass; }
            set { subClass = value; }    
        }

        /// <summary>
        /// Read/write property that determines the type of message and hence the level of message data
        /// that is present.
        /// </summary>
        public MessageType Type
        {
            get {  return type; }
            set { type = value; }    
        }

    }
 
}