// *********************************************** 
// NAME                 : TravelNewsSearchParser.cs
// AUTHOR               : Tim Mollart
// DATE CREATED         : 10/01/2007
// DESCRIPTION			: Helper class for travel news searching
// ************************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/TravelNews/TravelNewsSearchParser.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:50:30   mturner
//Initial revision.
//
//   Rev 1.1   Jan 18 2007 17:39:42   tmollart
//Modifications to search method and search word highlighting.
//Resolution for 4329: Travel News Updates and Search
//
//   Rev 1.0   Jan 12 2007 13:47:26   tmollart
//Initial revision.

using System;
using System.Text;
using System.Collections;
using System.Text.RegularExpressions;

namespace TransportDirect.UserPortal.TravelNews
{
	/// <summary>
	/// Travel news search phrase parser
	/// </summary>
	public class TravelNewsSearchParser
	{

		const char delimiter = '*';

		/// <summary>
		/// Constructor
		/// </summary>
		public TravelNewsSearchParser()
		{
		}

		/// <summary>
		/// Returns an array list of search tokens.
		/// </summary>
		/// <param name="SearchPhrase"></param>
		/// <returns></returns>
		public ArrayList GetSearchTokens(string SearchPhrase)
		{

			// Strip unwanted characters
			Regex rx = new Regex("[A-Za-z0-9'\"\\s]");
			StringBuilder newPhrase = new StringBuilder();

			for (int i = 0; i < SearchPhrase.Length; i ++)
			{
				char[] c = SearchPhrase.Substring(i,1).ToCharArray();

				if (rx.IsMatch(SearchPhrase.Substring(i,1)))
				{
					newPhrase.Append(c);
				}
			}

			SearchPhrase = newPhrase.ToString();

			ArrayList tokens = new ArrayList();

			StringBuilder newSearchPhrase = new StringBuilder();
			bool openingDelimiterFound = false;
			string currentChar;

			// Scan the search phrase
			for (int i = 0; i < SearchPhrase.Length; i ++)
			{
				// Set current char
				currentChar = SearchPhrase.Substring(i,1);

				// Scan until a space or a " is located
				if (currentChar == " " || currentChar == "\"")
				{
					// If its a " then ...
					if (currentChar == "\"")
					{
						// If opening delimiter has been found then we are
						// at the end of a token. Else we are at the start of
						// a token.
						if (openingDelimiterFound)
						{
							newSearchPhrase.Append(delimiter);
							openingDelimiterFound = false;
						}
						else
						{
							openingDelimiterFound = true;
						}
					}

					if (currentChar == " ")
					{
						// If an opening delimiter has been located then a space
						// means we want to append to the last token. Else if we
						// are not in a delimeted section we add the character on
						if (openingDelimiterFound)
						{
							newSearchPhrase.Append(currentChar);
						}
						else
						{
							newSearchPhrase.Append(delimiter);
						}
					}
				}
					
				if (currentChar != " " || currentChar == "\"")
				{
					newSearchPhrase.Append(currentChar.Replace("\"", ""));
				}
			}
		
			string[] finalTokens = newSearchPhrase.ToString().Split('*');

			// Final pass to remove unwanted empty tokens and sort out road names
			ArrayList returnTokens = new ArrayList();

			foreach (string token in finalTokens)
			{
				if (token.Length > 0 && token.ToUpper() != "AND" && token.ToUpper() != "OR" && token.ToUpper() != "NOT")
				{
					returnTokens.Add(token);
				}
			}

			return returnTokens;
		}
	}

	/// <summary>
	/// Search Token Match Evaluator Class
	/// </summary>
	public class SearchTokenMatchEvaluator
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		public SearchTokenMatchEvaluator()
		{
		}


		/// <summary>
		/// Replaces tokens with bold text tokens.
		/// </summary>
		/// <param name="m"></param>
		/// <returns></returns>
		public string TokenHighlighter(Match m)
		{
			return "<b>"+m.Captures[0].ToString()+"</b>";
		}
	}
	
}