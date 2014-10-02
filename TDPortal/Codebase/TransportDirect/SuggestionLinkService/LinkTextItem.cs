// *********************************************** 
// NAME                 : LinkTextItem.cs
// AUTHOR               : Ken Josling
// DATE CREATED         : 15/08/2005 
// DESCRIPTION			: The LinkTextItem class substitutes placeholder parameters in link
//						  text with runtime reference data held in the Session. 
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/SuggestionLinkService/LinkTextItem.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:50:08   mturner
//Initial revision.
//
//   Rev 1.1   Sep 02 2005 15:31:44   kjosling
//Updated following code review
//
//   Rev 1.0   Aug 24 2005 16:44:52   kjosling
//Initial revision.

using System;
using TransportDirect.Common;
using TransportDirect.Common.Logging;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.LocationService;

namespace TransportDirect.UserPortal.SuggestionLinkService
{
	public class LinkTextItem
	{
		#region Private Properties

		//contains the original text
		private string originalText;
		//contains the original text with array index markers
		private string formattedText;
		//contains the substitution parameters
		private SubstitutionParameter[] substitutionParameters;
		//counter used for replacing placeholder text
		private int i = -1;
		//regular expression used to identify placeholder expressions
		private const string EXPRESSION = @"\w*\{(?<param>\w+)\}";

		#endregion

		#region Constructor

		public LinkTextItem(string linkText)
		{
			originalText = linkText;
			formattedText = linkText;
			extractParameters();
		}

		#endregion

		#region Private Methods

		/// <summary>
		/// Parses the original link text and stores the placeholders in a private array
		/// </summary>
		private void extractParameters()
		{
			ArrayList matchCollection = new ArrayList();
			Regex expression = new Regex(EXPRESSION);
			//Match up all the substitute placeholders in the link text
			MatchCollection matches = expression.Matches(formattedText);
			if(matches.Count > 0)
			{
				foreach(Match match in matches)
				{
					string substituteText = match.Groups["param"].ToString();
					//Check to see if our substitute placeholder is valid
					if(Enum.IsDefined(typeof(SubstitutionParameter), substituteText))
					{
						matchCollection.Add(Enum.Parse(typeof(SubstitutionParameter), substituteText, false));
					}
					else
					{
						string exceptionMessage = 
							"Invalid substitute parameter '" + substituteText + "' found in: '" + originalText + "'. Parameters must be case sensitive.";
						TDException td = new TDException( exceptionMessage, false, TDExceptionIdentifier.SLSInvalidSubstitutionParameter );
						throw td;
					}
				}
				//Replace the substitutions in the link text with array indices
				MatchEvaluator replacementEvaluator = new MatchEvaluator(createFormattedText);
				formattedText = expression.Replace(formattedText, replacementEvaluator);
				
				//Move the retrieved substiute placeholders into the private array
				substitutionParameters = (SubstitutionParameter[])matchCollection.ToArray(typeof(SubstitutionParameter));
			}
		}

		/// <summary>
		/// MatchEvaluator delegate method used to replace instances of substitution parameters
		/// with array index pointers
		/// </summary>
		/// <param name="m">The current Match</param>
		/// <returns>The replacement text</returns>
		private string createFormattedText(Match m)
		{
			i++;
			return "{" + i.ToString() + "}";
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Obtains the session data from the current session and inserts the runtime
		/// values into the placeholders in the link text
		/// </summary>
		/// <returns>The link text with the session data</returns>
		public string GetSubstitutedLinkText()
		{
			if(substitutionParameters == null) return originalText;
			string[] runtimeValues = new string[substitutionParameters.Length];
			string currentRuntimeValue = String.Empty;

			for(int i = 0; i < runtimeValues.Length; i++)
			{
				switch(substitutionParameters[i])
				{
					case SubstitutionParameter.DestinationLocation:
						currentRuntimeValue = 
							SessionManager.TDItineraryManager.Current.JourneyRequest.DestinationLocation.Description;
						break;

					case SubstitutionParameter.OriginLocation:
						currentRuntimeValue = 
							SessionManager.TDItineraryManager.Current.JourneyRequest.OriginLocation.Description;
						break;
					
					default:
						//We have a new enum type that the LinkTextItem class doesn't know about. 
						string exceptionMessage = 
							"Unhandled substitution parameter '" + substitutionParameters[i].ToString() + "' found in link text: '" + originalText + "'";
						TDException td = new TDException( exceptionMessage, false, TDExceptionIdentifier.SLSUnhandledSubstitutionParameter );
						throw td;
				}
				if(currentRuntimeValue == null || currentRuntimeValue.Length == 0)
				{
					return String.Empty;
				}
				runtimeValues[i] = currentRuntimeValue;
			}

			//Plug the retrieved session data into the string using the placeholders
			try
			{	
				return String.Format(formattedText, runtimeValues);
			}
			catch(FormatException e)
			{
				string exceptionMessage = 
					"Unable to apply substitution parameters. Check format of link text '" + originalText + "'";
				TDException td = new TDException( exceptionMessage, e, false, TDExceptionIdentifier.SLSLinkTextFormatError );
				throw td;
			}
		}

		#endregion
	}
}
