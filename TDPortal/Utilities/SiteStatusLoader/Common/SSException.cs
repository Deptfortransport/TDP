// *********************************************** 
// NAME                 : SSException.cs
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 01/04/2009
// DESCRIPTION			: Exception class for any errors thrown by the application
// ************************************************ 
// $Log:   P:/TDPortal/archives/Utilities/SiteStatusLoader/Common/SSException.cs-arc  $
//
//   Rev 1.0   Apr 01 2009 13:23:42   mmodi
//Initial revision.
//Resolution for 5273: Site Status Loader

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace AO.Common
{
    [Serializable]
    public class SSException : ApplicationException
    {
        #region Private members

        private SSExceptionIdentifier id;
        private bool logged;
        private object additionalInformation;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor used for deserialization
        /// </summary>
        public SSException(SerializationInfo info, StreamingContext ctxt) : base(info, ctxt)
        {
            id = (SSExceptionIdentifier)info.GetValue("SSExId", typeof(SSExceptionIdentifier));
            logged = (bool)info.GetValue("SSExLogged", typeof(bool));
            additionalInformation = (object)info.GetValue("SSExAdditionalInformation", typeof(object));
        }

        /// <summary>
        /// Serialization method.
        /// </summary>
        public override void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            base.GetObjectData(info, ctxt);

            info.AddValue("SSExId", id);
            info.AddValue("SSExLogged", logged);
            info.AddValue("SSExAdditionalInformation", additionalInformation);
        }

        /// <summary>
        /// Creates a new instance of SSException, setting the identifier to -1.
        /// </summary>
        public SSException() : base(String.Empty, null)
        {
            this.id = SSExceptionIdentifier.Undefined;
            this.logged = false;
        }

        /// <summary>
        /// Creates a new instance of SSException with a specified message, identifier 
        /// and an indication of whether the exeption was logged.
        /// </summary>
        /// <param name="message">A message describing the exception, or an empty string.</param>
        /// <param name="logged">True if the exeception has been logged using the Event Logging Service, otherwise false.</param>
        /// <param name="id">An identifier used to identify the exception.</param>
        public SSException(string message, bool logged, SSExceptionIdentifier id)
            : this(message, null, logged, id)
        {
        }

        /// <summary>
        /// Creates a new instance of SSException with a specified message, identifier,
        /// inner exception and an indication of whether the exeption was logged.
        /// </summary>
        /// <param name="message">A message describing the exception, or an empty string.</param>
        /// <param name="innerException">A previously caught exception to store with exception.</param>
        /// <param name="logged">True if the exeception has been logged using the Event Logging Service, otherwise false.</param>
        /// <param name="id">An identifier used to identify the exception.</param>
        public SSException(string message, Exception innerException, bool logged, SSExceptionIdentifier id)
            : base(message, innerException)
        {
            this.id = id;
            this.logged = logged;
        }

        /// <summary>
        /// Creates a new instance of SSException with a specified message, identifier,
        /// inner exception and an indication of whether the exeption was logged.
        /// </summary>
        /// <param name="messages">An array of messages instead attached to this error</param>		
        /// <param name="logged">True if the exeception has been logged using the Event Logging Service, otherwise false.</param>
        /// <param name="id">An identifier used to identify the exception.</param>
        /// <param name="additionalInformation">Additional information linked to this message</param>
        public SSException(string message, bool logged, SSExceptionIdentifier id, object additionalInformation)
            : this(message, null, logged, id)
        {
            this.additionalInformation = additionalInformation;
        }

        #endregion

        #region Public properties

        /// <summary>
        /// Gets the exception identifier.
        /// </summary>
        public SSExceptionIdentifier Identifier
        {
            get { return id; }
        }

        /// <summary>
        /// Gets the logged indicator.
        /// Has the value true if the exception has been logged, otherwise false.
        /// </summary>
        public bool Logged
        {
            get { return logged; }
        }

        /// <summary>
        /// read-only property to return an array of messages linked to this error.
        /// </summary>
        public object AdditionalInformation
        {
            get { return additionalInformation; }
        }

        /// <summary>
        /// Formats SSException as a string.
        /// </summary>
        /// <returns>Formatted TDException as a string.</returns>
        override public string ToString()
        {
            return (String.Format("{0} {1}Logged:{2},Id:{3}{4}",
                    "SSException:",
                    this.Message == String.Empty ? String.Empty : String.Format("Message:{0},", this.Message),
                    this.logged.ToString(),
                    this.id.ToString("D"),
                    this.InnerException == null ? String.Empty : String.Format(",InnerException:{0}", this.InnerException.ToString())));
        }

        #endregion
    }
}
