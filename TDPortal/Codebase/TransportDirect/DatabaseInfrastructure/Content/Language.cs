//// ***********************************************
//// NAME           : Language.cs
//// AUTHOR 		: Steve Barker
//// DATE CREATED   : 07-Jan-2008
//// DESCRIPTION 	: The language to be sent to the content provider
//// ************************************************
////    Rev Devfactory Jan 07 2008 12:00:00   sbarker
////    CCN 0427 - The language to be sent to the content provider

using System;
using System.Collections.Generic;
using System.Text;

namespace TransportDirect.Common.DatabaseInfrastructure.Content
{
    /// <summary>
    /// Enum to specify the language to be obtained from the content provider
    /// </summary>
    public enum Language
    {
        /// <summary>
        /// English
        /// </summary>
        English,
        /// <summary>
        /// Welsh
        /// </summary>
        Welsh,
    }
}
