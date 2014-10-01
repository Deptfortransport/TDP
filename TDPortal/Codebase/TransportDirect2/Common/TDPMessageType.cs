// *********************************************** 
// NAME             : TDPMessageType.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 25 Mar 2011
// DESCRIPTION  	: Enumeration of TDPMessageType
// ************************************************
// 

using System;

namespace TDP.Common
{
    /// <summary>
    /// Enumeration of TDPMessageType
    /// </summary>
    [Serializable()]
    public enum TDPMessageType
    {
        Info,
        Warning,
        Error
    }
}