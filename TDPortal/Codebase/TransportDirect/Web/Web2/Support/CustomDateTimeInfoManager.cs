//**************************************************************
//NAME			: CustomDateTimeInfoManager.cs
//AUTHOR		: Andy Lole
//DATE CREATED	: 10/07/2003
//DESCRIPTION	: Wraps DateTimeFormatInfo to allow customisation dependent on language
//**************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Support/CustomDateTimeInfoManager.cs-arc  $
//
//   Rev 1.2   Mar 31 2008 13:27:12   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:28:52   mturner
//Initial revision.
//
//   Rev 1.2   Oct 14 2003 15:40:24   geaton
//Updated exception id to use TDExceptionIdentifier
//
//   Rev 1.1   Jul 18 2003 11:58:00   JMorrissey
//Changed all occurrences of Global.TDLanguageManager.GetString  to Global.tdResourceManager.GetString as the name of the global resource manger has changed
//
//   Rev 1.0   Jul 17 2003 11:51:28   ALole
//Initial Revision

using System;
using System.Globalization;
using System.Collections;
using TransportDirect.UserPortal.Web;
using TransportDirect.Common;

namespace TransportDirect.Web.Support
{
	/// <summary>
	/// Summary description for CustomDateTimeInfoManager.
	/// </summary>
	public class CustomDateTimeInfoManager
	{
		// Private Hashtable to hold all non default Culture DateTimeFormatInfo
		private Hashtable cultureTable;

		/// <summary>
		/// Instantiates cultureTable with read only DateTimeFormatInfo objects
		/// containing the correctly translated text for Month Names, Day Names and Am/Pm Designators.
		/// </summary>
		/// <param name="cultureDetails">Array of type TDCultureInfo containing an entry for each non default language</param>
		public void Init(TDCultureInfo[] cultureDetails)
		{
			cultureTable = new Hashtable();
			DateTimeFormatInfo tdDTFI;

			// Loop through cultureDetails and set all Date/Time related text to the correct Culture
			for (int i = 0; i < cultureDetails.Length; i++)
			{
				tdDTFI = new DateTimeFormatInfo();

				// Retrieve Full Month names. There must be an entry in the resource file called DTFI.MonthNames
				// It will contain a single string of all full month names, separated by commas and terminated 
				// with a comma as there must be a blank entry at the end for MonthNames. 
                string monthNames = Global.tdResourceManager.GetString("DTFI.MonthNames",cultureDetails[i]);

                if (monthNames != null)
                {
                    tdDTFI.MonthNames = monthNames.Split(',');
                }

				// Retrieve Abbreviated Month names. There must be an entry in the resource file called DTFI.AbbreviatedMonthNames
				// It will contain a single string of all abbreviated month names, separated by commas and terminated 
				// with a comma as there must be a blank entry at the end for AbbreviatedMonthNames.
				tdDTFI.AbbreviatedMonthNames = Global.tdResourceManager.GetString("DTFI.AbbreviatedMonthNames",cultureDetails[i]).Split(',');
				
				// Retrieve Full Day names. There must be an entry in the resource file called DTFI.DayNames
				// It will contain a single string of all day names, separated by commas. First Day is Sunday, last is Saturday.
				tdDTFI.DayNames = Global.tdResourceManager.GetString("DTFI.DayNames",cultureDetails[i]).Split(',');
				
				// Retrieve Abbreviated Day names. There must be an entry in the resource file called DTFI.AbbreviatedDayNames
				// It will contain a single string of all abbreviated day names, separated by commas. First Day is Sunday, last is Saturday.
				tdDTFI.AbbreviatedDayNames = Global.tdResourceManager.GetString("DTFI.AbbreviatedDayNames",cultureDetails[i]).Split(',');
				
				// Retrieve AM Designator. There must be an entry in the resource file called DTFI.AMDesignator
				tdDTFI.AMDesignator = Global.tdResourceManager.GetString("DTFI.AMDesignator",cultureDetails[i]);
				
				// Retrieve PM Designator. There must be an entry in the resource file called DTFI.PMDesignator
				tdDTFI.PMDesignator = Global.tdResourceManager.GetString("DTFI.PMDesignator",cultureDetails[i]);
				
				// Add DateTimeFormatInfo to cultureTable using the culture name as the key
				cultureTable.Add(cultureDetails[i].Name, DateTimeFormatInfo.ReadOnly(tdDTFI));
			}
		}

		/// <summary>
		/// Retrieves the correct DateTimeFormatInfo object from cultureTable using the string provided as the key
		/// </summary>
		/// <param name="cultureName">string containing the Culture Name to return (eg. "cy-GB")</param>
		public DateTimeFormatInfo GetDateTimeFormat(string cultureName)
		{
			// If cultureTable has been insantiated and the key provided exists
			// then return the DateTimeFormatInfo object for the requested Culture.
			if (cultureTable != null && cultureTable.Contains(cultureName))
			{
				return (DateTimeFormatInfo)cultureTable[cultureName];
			}
			else
			{
				// If cultureTable has not been instantiated or the key is not valid throw a TDException
				throw new TransportDirect.Common.TDException("CustomDateTimeInfoManager has not been initialised", false, TDExceptionIdentifier.WEBCultureTableNotInitialised);
			}
		}
	}
}