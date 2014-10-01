//// ***********************************************
//// NAME           : ContentProviderException.cs
//// AUTHOR 		: Steve Barker
//// DATE CREATED   : 21-Jan-2008
//// DESCRIPTION 	: Encapsulates errors relateing to the ContentProvider
//// ************************************************
////    Rev Devfactory Jan 21 2008 12:00:00   sbarker
////    CCN 0427 - Encapsulates errors relateing to the ContentProvider

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace TransportDirect.Common.DatabaseInfrastructure.Content
{
    /// <summary>
    /// Used convey 
    /// </summary>
    [global::System.Serializable]
    public class ContentProviderException : Exception
    {
        #region Public Constructors

        /// <summary>
        /// Constructor allowing error message to be set
        /// </summary>
        /// <param name="message"></param>
        public ContentProviderException(string message)
            : base(message)
        {
            //No implementation
        }

        /// <summary>
        /// Constructor allowing error message and inner exception to be set
        /// </summary>
        /// <param name="message"></param>
        /// <param name="inner"></param>
        public ContentProviderException(string message, Exception inner)
            : base(message, inner)
        {
            //No implementation
        }
        #endregion

        #region Protected Constructors

        /// <summary>
        /// Constructor used for serialisation of the exception.
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected ContentProviderException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            //No implementation
        }

        #endregion        
    }
}
