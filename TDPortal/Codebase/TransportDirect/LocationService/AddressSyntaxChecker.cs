// *********************************************** 
// NAME                 : AddressSyntaxChecker.cs
// AUTHOR               : Richard SCOTT
// DATE CREATED         : 11/04/2005
// DESCRIPTION  : Static class implementing methods to check address formats
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/LocationService/AddressSyntaxChecker.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:24:58   mturner
//Initial revision.
//
//   Rev 1.0   Apr 12 2005 11:00:38   rscott
//Initial revision.
using System;
using System.Text.RegularExpressions;

namespace TransportDirect.UserPortal.LocationService
{
	/// <summary>
	/// Static class implementing methods to check address formats
	/// </summary>
	[Serializable()]
	public class AddressSyntaxChecker
	{
		private const string alphanumeric = "[^A-Za-z0-9']";

		private AddressSyntaxChecker()
		{
		}

		/// <summary>
		/// Method returning if the input text string is a single word address
		/// </summary>
		/// <param name="text">input text string</param>
		/// <returns>returns true if is a single word address</returns>
		static public bool IsNotSingleWord(string text)
		{
			if (text.Length == 0)
				return false;
			else
			{
				//Trim the string
				text.Trim();

				// creates the Regex postcode object
				Regex regexAddress= new Regex(alphanumeric);
				bool result = regexAddress.IsMatch(text);
				
				return result;
			}
		}
	}
}
