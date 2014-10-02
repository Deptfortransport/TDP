// *********************************************** 
// NAME                 : TooVagueException.cs 
// AUTHOR               : Neil Moorhouse
// DATE CREATED         : 20/06/2005
// DESCRIPTION  : Defines an exception
// (inherits from TDException) that is thrown
// if a ESRI Gazetteer is too vague. 
// ************************************************ 
// $$ 

using System;
using TransportDirect.Common;

namespace TransportDirect.UserPortal.LocationService
{
	/// <summary>
	/// Summary description for TooVagueException.
	/// </summary>
	[Serializable]
	public class TooVagueException : TDException
	{
		public TooVagueException(string message, bool logged, TDExceptionIdentifier id)
			: base(message, null, logged, id)
		{
		}

		public TooVagueException(string message, Exception innerException, bool logged, TDExceptionIdentifier id)
			: base(message, innerException, logged, id)
		{
		}
	}
}
