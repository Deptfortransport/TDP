﻿// *********************************************** 
// NAME             : TDPTraceLevel.cs      
// AUTHOR           : Amit Patel
// DATE CREATED     : 10 Feb 2011
// DESCRIPTION  	: Enumeration that defines the levels that may be associated with an OperationalEvent.
// ************************************************

namespace TDP.Common.EventLogging
{
    /// <summary>
    /// Enumeration that defines the levels that may be associated 
    /// with an <c>OperationalEvent</c>.
    /// </summary>
    public enum TDPTraceLevel
    {
        Undefined,
        Off,
        Error,
        Warning,
        Info,
        Verbose
    }
}
