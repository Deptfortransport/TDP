// *********************************************** 
// NAME                 : LanguageConverter.cs
// AUTHOR               : Patrick ASSUIED
// DATE CREATED         : 26/11/2003 
// DESCRIPTION  : Static class converting the language used in 
// the current Web culture into the language string recognisable by Atkins
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/JourneyPlanRunner/LanguageConverter.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:24:46   mturner
//Initial revision.
//
//   Rev 1.0   Nov 26 2003 11:50:32   passuied
//Initial Revision


using System;
using System.Globalization;

namespace TransportDirect.UserPortal.JourneyPlanRunner
{
	/// <summary>
	/// Summary description for LanguageConverter.
	/// </summary>
	public class LanguageConverter
	{
		private LanguageConverter()
		{
		}

		/// <summary>
		/// Static method that converts the Web Culture language string 
		/// into the language string recognized by ATKINS
		/// </summary>
		/// <param name="lang">language keyword used in the Web environment</param>
		/// <returns>Returns a 2 characters text string representing the language used</returns>
		public static string Convert(string lang)
		{
			switch (lang.ToLower(CultureInfo.InvariantCulture))
			{
				case "en-gb":
					return "EN";
					
				case "cy-gb":
					return "CY";
					
				default:
					return "EN";
			}
		}
	}
}
