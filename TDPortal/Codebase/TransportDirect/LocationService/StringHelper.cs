// *********************************************** 
// NAME                 : StringHelper.cs 
// AUTHOR               : Patrick ASSUIED
// DATE CREATED         : 04/09/2003 
// DESCRIPTION  : Class providing extra string functionality
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/LocationService/StringHelper.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:25:20   mturner
//Initial revision.
//
//   Rev 1.3   Sep 22 2003 17:31:28   passuied
//made all objects serializable
//
//   Rev 1.2   Sep 10 2003 11:09:04   passuied
//Changes after FxCop
//
//   Rev 1.1   Sep 09 2003 17:23:56   passuied
//Clean up all files to pass code review!
//
//   Rev 1.0   Sep 05 2003 15:30:40   passuied
//Initial Revision

using System;
using System.Collections;

namespace TransportDirect.UserPortal.LocationService
{
	/// <summary>
	/// Class providing extra string functionality
	/// </summary>
	[Serializable()]
	public class StringHelper
	{
		private StringHelper()
		{
		}

		/// <summary>
		/// Gets the more frequent string in a array of strings
		/// </summary>
		/// <param name="words">array of words</param>
		/// <returns>returns the more frequent word</returns>
		public static string GetMoreFrequentWord(string[] words)
		{
			Hashtable occurrences = new Hashtable();
	
			foreach (string word in words)
			{
				if (occurrences.Contains(word))
				{
					int count = (int)occurrences[word];
					occurrences[word] = count+1;
				}
				else
				{
					occurrences.Add(word, 1);
				}
			}

			IDictionaryEnumerator index = occurrences.GetEnumerator();

			string moreFrequentWord = string.Empty;
			int occurrenceCount = 0;
			while (index.MoveNext())
			{
				int count = (int)occurrences[index.Key];
				if (count > occurrenceCount)
				{
					occurrenceCount = (int) index.Value;
					moreFrequentWord = (string)index.Key;
				}
			}

			return moreFrequentWord;
			
		}
	}
}
