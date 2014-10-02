// *********************************************** 
// NAME                 : PostcodeSyntaxChecker.cs
// AUTHOR               : Patrick ASSUIED
// DATE CREATED         : 29/08/2003 
// DESCRIPTION  : Static class implementing methods to check if a string contains postcode or is a postcode
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/LocationService/PostcodeSyntaxChecker.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:25:20   mturner
//Initial revision.
//
//   Rev 1.5   Mar 08 2005 13:49:56   esevern
//check-in on behalf of phil scott.  Fix for vantive 3387922, tested on BBP under change no.  3646801.   Should be included in Del 6.3.8 maintenance release
//
//   Rev 1.5   Mar 02 2005 12:49:42   pscott
//Modified pattern string so that only postcodes with no or one space are put to 
// the fullpost code check. More than one space will be sent to partial post code
// check which can handle more than one space (full post code check cannot).
//
//   Rev 1.4   Nov 03 2004 09:49:42   jgeorge
//Modified postcode checking methods to return false when an empty string is supplied.
//
//   Rev 1.3   Apr 27 2004 13:44:36   jbroome
//DEL 5.4 Merge. 
//Partial Postcode searching
//
//   Rev 1.2   Sep 22 2003 17:31:26   passuied
//made all objects serializable
//
//   Rev 1.1   Sep 09 2003 17:23:56   passuied
//Clean up all files to pass code review!
//
//   Rev 1.0   Sep 05 2003 15:30:38   passuied
//Initial Revision

using System;
using System.Text.RegularExpressions;

namespace TransportDirect.UserPortal.LocationService
{
	/// <summary>
	/// Static class implementing methods to check if a string contains postcode or is a postcode
	/// </summary>
	[Serializable()]
	public class PostcodeSyntaxChecker
	{
		
		private const string pattern = "[A-Z,a-z]{1,2}[0-9]{1,2}[A-Z,a-z]{0,1}[ ]{0,1}[0-9]{1,1}[A-Z,a-z]{2,2}";
		private const string patternOutcode = "[A-Z,a-z]{1,2}[0-9]{1,2}[A-Z,a-z]{0,1}";
		private const string patternIncode = "[ ]{1,3}[0-9]{1,1}[A-Z,a-z]{0,2}";

		private PostcodeSyntaxChecker()
		{
		}

		/// <summary>
		/// Method returning if the input text string contains a postcode
		/// </summary>
		/// <param name="text">input text string</param>
		/// <returns>returns true if contains a postcode</returns>
		static public bool ContainsPostcode (string text)
		{
			if (text.Length == 0)
				return false;
			else
			{
				// creates the Regex postcode object
				Regex regexPostcode = new Regex(pattern, RegexOptions.IgnorePatternWhitespace);
				return regexPostcode.IsMatch(text);
			}
		}

		/// <summary>
		/// Method returning if the input text string is a postcode
		/// </summary>
		/// <param name="text">input text string</param>
		/// <returns>returns true if is a postcode</returns>
		static public bool IsPostCode (string text)
		{
			if (text.Length == 0)
				return false;
			else
			{
				// creates the Regex postcode object
				Regex regexPostcode = new Regex(pattern,RegexOptions.IgnorePatternWhitespace);

				string result = regexPostcode.Replace(text, "", 1).Trim();

				if (result == string.Empty)
					return true;
				else 
					return false;
			}
		}

		/// <summary>
		/// Method returning if the input text string is a partial postcode
		/// </summary>
		/// <param name="text">input text string</param>
		/// <returns>returns true if is a partial postcode</returns>
		static public bool IsPartPostCode (string text)
		{
			if ((text.Length != 0) && !IsPostCode(text))
			{
				// creates the Regex postcode object
				Regex regexPostcode = new Regex(patternOutcode,RegexOptions.IgnorePatternWhitespace);
				
				// Check for an Outcode?
				string result = regexPostcode.Replace(text, "", 1);

				if (result == string.Empty)
					return true;
				else
				{
					// Is there an Incode?
					regexPostcode = new Regex(patternIncode, RegexOptions.None);
					result = regexPostcode.Replace(result, "").Trim();				
					if (result == string.Empty)
						return true;
					else
						return false;
				}
			}
			else
			{
				return false;
			}
		}
	}

}
