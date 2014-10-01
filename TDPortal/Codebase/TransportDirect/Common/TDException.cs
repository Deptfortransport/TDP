// **************************************************
// NAME                 : TDException.cs 
// AUTHOR               : Gary Eaton
// DATE CREATED         : 11/07/2003 
// DESCRIPTION			: User-defined exception 
// class for the Transport Direct Portal application.
// ************************************************** 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Common/TDException.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:19:06   mturner
//Initial revision.
//
//   Rev 1.5   Feb 23 2006 19:17:36   build
//Automatically merged from branch for stream3129
//
//   Rev 1.4.1.2   Feb 02 2006 14:43:12   mdambrine
//rework on the journey planner enhanced exposed services see CR053_IR_3407 Journey Planner Service Component.doc 
//
//   Rev 1.4.1.1   Jan 26 2006 10:06:50   mdambrine
//Adding deserilisation of the tdexception over the remoting boundry
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.4.1.0   Jan 18 2006 14:26:02   mdambrine
//Added a additionalinformation object for cjpmessages generated in the journeyplannerservice project
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.4   Dec 11 2003 14:15:34   geaton
//Added methods to support serialization/deserialization across remoting boundaries.
//
//   Rev 1.3   Oct 10 2003 09:52:16   geaton
//Updated ToString() method to use numeric representation of id.
//
//   Rev 1.2   Oct 02 2003 18:07:44   COwczarek
//Added new constructors that take id of type TDExceptionIdentifier. Existing constructors marked obsolete and will be removed shortly.
//
//   Rev 1.1   Sep 04 2003 10:04:32   geaton
//Added comments. Made default constructor public (at request of Atkins). Added ToString() method so  TDException properties are visible when instances serialized to string.

using System;
using System.Runtime.Serialization;
using System.Text;

namespace TransportDirect.Common
{

	[Serializable]
	public class TDException : ApplicationException
	{

        private TDExceptionIdentifier id;
		private bool logged;
		private object additionalInformation;

		/// <summary>
		/// Constructor used for deserialization
		/// </summary>
		public TDException(SerializationInfo info, StreamingContext ctxt) : base(info, ctxt)
		{
			id = (TDExceptionIdentifier)info.GetValue("TDExId", typeof(TDExceptionIdentifier));
			logged = (bool)info.GetValue("TDExLogged", typeof(bool));
			additionalInformation = (object)info.GetValue("TDExAdditionalInformation", typeof(object));
		}
        
		/// <summary>
		/// Serialization method.
		/// </summary>
		public override void GetObjectData(SerializationInfo info, StreamingContext ctxt)
		{
			base.GetObjectData(info, ctxt);

			info.AddValue("TDExId", id);
			info.AddValue("TDExLogged", logged);
			info.AddValue("TDExAdditionalInformation", additionalInformation);
		}

		/// <summary>
		/// Creates a new instance of TDException, setting the identifier to -1.
		/// </summary>
		public TDException() : base(String.Empty, null)
		{
			this.id = TDExceptionIdentifier.Undefined;
			this.logged = false;
		}

		/// <summary>
		/// Creates a new instance of TDException with a specified message, identifier 
		/// and an indication of whether the exeption was logged.
		/// </summary>
		/// <param name="message">A message describing the exception, or an empty string.</param>
		/// <param name="logged">True if the exeception has been logged using the TD Event Logging Service, otherwise false.</param>
		/// <param name="id">An identifier used to identify the exception.</param>
        [Obsolete("This constructor is obsolete - use constructor with id of type TDExceptionIdentifier",false)]
        public TDException(string message, bool logged, long id)
			: this(message, null, logged, TDExceptionIdentifier.Undefined)
		{
		}

		/// <summary>
		/// Creates a new instance of TDException with a specified message, identifier,
		/// inner exception and an indication of whether the exeption was logged.
		/// </summary>
		/// <param name="message">A message describing the exception, or an empty string.</param>
		/// <param name="innerException">A previously caught exception to store with exception.</param>
		/// <param name="logged">True if the exeception has been logged using the TD Event Logging Service, otherwise false.</param>
		/// <param name="id">An identifier used to identify the exception.</param>
		[Obsolete("This constructor is obsolete - use constructor with id of type TDExceptionIdentifier",false)]
		public TDException(string message, Exception innerException, bool logged, long id)
			: base(message, innerException)
		{
			this.id = TDExceptionIdentifier.Undefined;
			this.logged = logged;
		}
		
        /// <summary>
        /// Creates a new instance of TDException with a specified message, identifier 
        /// and an indication of whether the exeption was logged.
        /// </summary>
        /// <param name="message">A message describing the exception, or an empty string.</param>
        /// <param name="logged">True if the exeception has been logged using the TD Event Logging Service, otherwise false.</param>
        /// <param name="id">An identifier used to identify the exception.</param>
        public TDException(string message, bool logged, TDExceptionIdentifier id)
            : this(message, null, logged, id)
        {
        }

        /// <summary>
        /// Creates a new instance of TDException with a specified message, identifier,
        /// inner exception and an indication of whether the exeption was logged.
        /// </summary>
        /// <param name="message">A message describing the exception, or an empty string.</param>
        /// <param name="innerException">A previously caught exception to store with exception.</param>
        /// <param name="logged">True if the exeception has been logged using the TD Event Logging Service, otherwise false.</param>
        /// <param name="id">An identifier used to identify the exception.</param>
        public TDException(string message, Exception innerException, bool logged, TDExceptionIdentifier id)
            : base(message, innerException)
        {
            this.id = id;
            this.logged = logged;
        }

		/// <summary>
		/// Creates a new instance of TDException with a specified message, identifier,
		/// inner exception and an indication of whether the exeption was logged.
		/// </summary>
		/// <param name="messages">An array of messages instead attached to this error</param>		
		/// <param name="logged">True if the exeception has been logged using the TD Event Logging Service, otherwise false.</param>
		/// <param name="id">An identifier used to identify the exception.</param>
		/// <param name="additionalInformation">Additional information linked to this message</param>
		public TDException(string message, bool logged, TDExceptionIdentifier id, object additionalInformation)
			: this(message, null, logged, id)
		{			
			this.additionalInformation = additionalInformation;
		}		       
		
        /// <summary>
        /// Gets the exception identifier.
        /// </summary>
        public TDExceptionIdentifier Identifier
        {
            get {return id;}
        }
		
        /// <summary>
		/// Gets the logged indicator.
		/// Has the value true if the exception has been logged, otherwise false.
		/// </summary>
		public bool Logged
		{
			get {return logged;}	
		}

		/// <summary>
		/// read-only property to return an array of messages linked to this error.
		/// </summary>
		public object AdditionalInformation
		{
			get {return additionalInformation;}
		}

		/// <summary>
		/// Formats TDException as a string.
		/// </summary>
		/// <returns>Formatted TDException as a string.</returns>
		override public string ToString()
		{
			return (String.Format("{0} {1}Logged:{2},Id:{3}{4}",
					"TransportDirect.Common.TDException:",
					this.Message==String.Empty?String.Empty:String.Format("Message:{0},", this.Message),
					this.logged.ToString(),
					this.id.ToString("D"),
					this.InnerException==null?String.Empty:String.Format(",InnerException:{0}", this.InnerException.ToString())));
		}
	}
}
