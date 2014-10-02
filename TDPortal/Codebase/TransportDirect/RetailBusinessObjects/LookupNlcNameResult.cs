//********************************************************************************
//NAME         : LookupNlcNameResult.cs
//AUTHOR       : Richard Philpott
//DATE CREATED : 2006-04-03
//DESCRIPTION  : Extracts all Group locations relating to a given NLC 
//				 from the result of an LBO call.
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/RetailBusinessObjects/LookupNlcNameResult.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:46:10   mturner
//Initial revision.
//
//   Rev 1.3   Apr 07 2006 15:46:16   RPhilpott
//Improved comments, post code review.
//Resolution for 35: DEL 8.1 Workstream - Find Cheaper (Rail)
//
//   Rev 1.2   Apr 06 2006 19:11:46   RPhilpott
//FxCop fixes.
//Resolution for 35: DEL 8.1 Workstream - Find Cheaper (Rail)
//
//   Rev 1.1   Apr 05 2006 17:18:58   RPhilpott
//Associate with stream IR.
//Resolution for 35: DEL 8.1 Workstream - Find Cheaper (Rail)
//
//   Rev 1.0   Apr 05 2006 17:18:26   RPhilpott
//Initial revision.
//

using System;
using System.Text;
using System.Collections;
using System.Diagnostics;
using System.Globalization;

using TransportDirect.Common;
using TransportDirect.Common.Logging;

using TransportDirect.UserPortal.PricingMessages;

namespace TransportDirect.UserPortal.RetailBusinessObjects
{
	/// <summary>
	/// Extracts all Group locations relating to a 
	/// given NCL code from the result of an LBO call.
	/// </summary>
	[Serializable]
	public class LookupNlcNameResult
	{

		private const int HEADER_LENGTH = 13;
		private const int DESCRIPTION_LENGTH  = 30;

		private string locationName = string.Empty;
		private bool fatalError;

		/// <summary>
		/// Read-only property to return the name of the location.
		/// </summary>
		public string LocationName
		{
			get { return locationName; }
		}

		/// <summary>
		/// Read-only property indicating whether 
		/// an error has prevented completion
		/// of the output processing.
		/// </summary>
		public bool FatalError
		{
			get { return fatalError; }
		}

		/// <summary>
		/// Constructor - extracts the returned location name
		/// from the supplied LBO output details and stores 
		/// it in an instance variable for subsequent use.
		/// </summary>
		/// <param name="output">Output returned by LBOPool</param>
		public LookupNlcNameResult(BusinessObjectOutput output) 
		{
			if	(output.ErrorSeverity == Messages.ErrorSeverityCritical || output.ErrorSeverity == Messages.ErrorSeverityError)
			{
				fatalError = true;
				return;
			}

			if	(output.RecordDetails.Length < 2)
			{
				Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, "Not enough entries in output header"));
				fatalError = true;
				return;
			}

			locationName = TidyLocationName(output.OutputBody.Substring(HEADER_LENGTH, DESCRIPTION_LENGTH));
		}


		/// <summary>
		/// Name returned by LBO is all upper case. This method converts it 
		/// to mixed case (allowing for odd cases like Henley-on-Thames), which
		/// is both more readable and more consistent with the rest of the site.
		/// </summary>
		/// <param name="input">original name</param>
		/// <returns>tidied string</returns>
		private string TidyLocationName(string input)
		{
			input = input.Trim();

			StringBuilder outputString = new StringBuilder(input.Length);	
				
			bool wordBreak = true; 
			bool firstHyphen = true; 
				
			for (int i = 0; i < input.Length; i++) 
			{
				if	(wordBreak)
				{
					outputString.Append(Char.ToUpper(input[i], CultureInfo.InvariantCulture));
					
					if	(!Char.IsPunctuation(input[i]) && !Char.IsWhiteSpace(input[i]))
					{
						wordBreak = false;
					}
				}
				else if	(Char.IsLetterOrDigit(input[i]))
				{
					outputString.Append(Char.ToLower(input[i], CultureInfo.InvariantCulture));
				}
				else
				{
					outputString.Append(input[i]);
						
					if	(Char.IsWhiteSpace(input[i]))
					{
						wordBreak = true;
						firstHyphen = true;
						continue;
						
					}

					if	(Char.IsPunctuation(input[i]))
					{
						if	(input[i] == '-')
						{
							if	(!firstHyphen)
							{
								wordBreak = true;
							}
							else
							{
								firstHyphen = false;
							}
						}
						else
						{
							wordBreak = true;
						}
					}
				}
			}
			
			return outputString.ToString();
		}
	}
}

