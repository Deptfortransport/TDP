//********************************************************************************
//NAME         : BusinessObject.cs
//AUTHOR       : Open source downloaded from
//				 http://www.codeguru.com/cs_network/FileReader.html
//				 Source updated to use TDException.
//DATE CREATED : 23/10/2003
//DESCRIPTION  : Allows standard Windows .ini files to be read and updated.
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/RetailBusinessObjects/Configuration.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:46:02   mturner
//Initial revision.
//
//   Rev 1.0   Oct 28 2003 20:03:56   geaton
//Initial Revision

using System;

using TransportDirect.Common;

namespace TransportDirect.UserPortal.RetailBusinessObjects
{
		
	/// <summary>
	/// Provides access to .ini files.
	/// </summary>
	public class Configuration
	{
		/// <summary>
		/// The key is a string, the value is another Hashtable, which contains the values.
		/// </summary>
		System.Collections.Hashtable Keys;
		/// <summary>
		/// This contains values without Key
		/// </summary>
		System.Collections.Hashtable Values;

		/// <summary>
		/// Construct an empty configuration
		/// </summary>
		public Configuration()
		{
			Keys = new System.Collections.Hashtable();
			Values = new System.Collections.Hashtable();
		}

		/// <summary>
		/// Construct the class from data got through the text reader
		/// </summary>
		/// <param name="tr">The source for constructing the class</param>
		public Configuration(System.IO.TextReader tr)
		{
			Keys = new System.Collections.Hashtable();
			Values = new System.Collections.Hashtable();
			Parse(tr);
		}
			
		/// <summary>
		/// Get a value from the class
		/// </summary>
		/// <param name="Key">The key from which to get the value</param>
		/// <param name="ValueName">The value name in the key</param>
		/// <returns>The value of the value name inside the key</returns>
		/// <exception cref="TDException">
		/// If value cannot be retrieved.
		/// </exception>
		public string GetValue(string Key, string ValueName)
		{
			System.Collections.Hashtable KeyTable = (System.Collections.Hashtable)Keys[Key];
			if(KeyTable!=null)
			{
				if(KeyTable.ContainsKey(ValueName))
				{
					return (string)KeyTable[ValueName];
				}
				else
				{
					throw new TDException(String.Format(Messages.Pool_ValueNotFoundInIni, ValueName), false, TDExceptionIdentifier.PRHValueNotFoundInIniFile);
				}

			}
			else
			{
				throw new TDException(String.Format(Messages.Pool_KeyNotFoundInIni, Key), false, TDExceptionIdentifier.PRHKeyNotFoundInIniFile);
			}
		}

		/// <summary>
		/// Get a value from ValueName without a key
		/// </summary>
		/// <param name="ValueName">The valuename to get from</param>
		/// <returns>The value of the value name</returns>
		/// <exception cref="TDException">
		/// Value not found.
		/// </exception>
		public string GetValue(string ValueName)
		{
			if(Values.ContainsKey(ValueName))
			{
				return (string)Values[ValueName];
			}
			else
			{
				throw new TDException(String.Format(Messages.Pool_ValueNotFoundInIni, ValueName), false, TDExceptionIdentifier.PRHValueNotFoundInIniFile);
			}
		}

		/// <summary>
		/// Checks if a spesified key is in the class
		/// </summary>
		/// <param name="KeyName">The name of the key</param>
		/// <returns>Whatever the key exists or not</returns>
		bool KeyExists(string KeyName)
		{
			return Keys.Contains(KeyName);
		}

		/// <summary>
		/// Checks if a value exists in the class
		/// </summary>
		/// <param name="KeyName">The name of the key, the key must exists</param>
		/// <param name="ValueName">The name of the value to check</param>
		/// <returns>Whatever the value exists in the key or not</returns>
		/// <exception cref="TDException">
		/// Key not found.
		/// </exception>
		bool ValueExists(string KeyName,string ValueName)
		{
			System.Collections.Hashtable KeyTable = (System.Collections.Hashtable)Keys[KeyName];
			if(KeyTable!=null)
			{
				return KeyTable.Contains(ValueName);
			}
			else
			{
				throw new TDException(String.Format(Messages.Pool_KeyNotFoundInIni, KeyName), false, TDExceptionIdentifier.PRHKeyNotFoundInIniFile);
			}
		}

		/// <summary>
		/// Checks if a value exists in the class
		/// </summary>
		/// <param name="ValueName">Name of the kye to check</param>
		/// <returns>Whatever the value exists or not</returns>
		bool ValueExists(string ValueName)
		{
			return Values.Contains(ValueName);
		}

		/// <summary>
		/// Set a value in the configuration data
		/// </summary>
		/// <param name="Key">Must already exist</param>
		/// <param name="ValueName">Doesn't have to exist before calling this function</param>
		/// <param name="Value">The value that will be stored in ValueName</param>
		/// <exception cref="TDException">
		/// Key not found.
		/// </exception>
		public void SetValue(string Key, string ValueName, string Value)
		{
			System.Collections.Hashtable KeyTable = (System.Collections.Hashtable)Keys[Key];
			if(KeyTable!=null)
			{
				KeyTable[ValueName] = Value;
			}
			else
			{
				throw new TDException(String.Format(Messages.Pool_KeyNotFoundInIni, Key), false, TDExceptionIdentifier.PRHKeyNotFoundInIniFile);
			}
		}

		/// <summary>
		/// Set a value in the configuration data without the need for a key
		/// </summary>
		/// <param name="ValueName">Doesn't have to exist before-hand</param>
		/// <param name="Value">The value that will be stored in ValueName</param>
		public void SetValue(string ValueName, string Value)
		{
			Values[ValueName] = Value;
		}


		/// <summary>
		/// Add a key to the class, attempts to add a key already existing are ignored.
		/// </summary>
		/// <param name="NewKey">The new key name</param>
		public void AddKey(string NewKey)
		{
			System.Collections.Hashtable New = new System.Collections.Hashtable();
			Keys[NewKey] = New;
		}

		/// <summary>
		/// Saves the class data in the form of INI file into the TextWriter
		/// </summary>
		/// <param name="sw">The destination for the output</param>
		public void Save(System.IO.TextWriter sw)
		{
			System.Collections.IDictionaryEnumerator Enumerator = Values.GetEnumerator();
			//Print values
			sw.WriteLine("; The values in this group");
			while(Enumerator.MoveNext())
			{
				sw.WriteLine("{0} = {1}",Enumerator.Key,Enumerator.Value);
			}
			sw.WriteLine("; This is where the keys begins");
			Enumerator = Keys.GetEnumerator();
			while(Enumerator.MoveNext())
			{
				System.Collections.IDictionaryEnumerator Enumerator2nd = ((System.Collections.Hashtable)Enumerator.Value).GetEnumerator();
				sw.WriteLine("[{0}]",Enumerator.Key);
				while(Enumerator2nd.MoveNext())
				{
					sw.WriteLine("{0} = {1}",Enumerator2nd.Key,Enumerator2nd.Value);
				}
			}
		}

		/// <summary>
		/// This private method Parse the input and sort it properly in the class
		/// for later retrival.
		/// </summary>
		/// <param name="sr">an already initialize StreamReader</param>
		private void Parse(System.IO.TextReader sr)
		{
			System.Collections.Hashtable CurrentKey=null;
			string Line,ValueName,Value;
			while (null != (Line = sr.ReadLine()))
			{
				int j,i=0;
				while(Line.Length>i && Char.IsWhiteSpace(Line,i)) i++;//skip white space in beginning of line
				if(Line.Length<=i)
					continue;
				if(Line[i] == ';')//Comment
					continue;
				if(Line[i] == '[')//Start new Key
				{
					string KeyName;
					j = Line.IndexOf(']',i);
					if(j==-1)//last ']' not found
						throw new TDException(Messages.Pool_InvalidInputWhenParsingIni, false, TDExceptionIdentifier.PRHInvalidInputWhenParsingIniFile);

					KeyName = Line.Substring(i+1,j-i-1).Trim();

					if(!Keys.ContainsKey(KeyName))
					{
						this.AddKey(KeyName);
					}
					CurrentKey = (System.Collections.Hashtable)Keys[KeyName];
					while(Line.Length>++j && Char.IsWhiteSpace(Line,j));//skip white space in beginning of line
					if(Line.Length>j)
					{
						if (Line[j]!=';')//Anything but a comment is unacceptable after a key name
							throw new TDException(Messages.Pool_InvalidInputWhenParsingIni, false, TDExceptionIdentifier.PRHInvalidInputWhenParsingIniFile);
					}
					continue;
				}
				//Start of a value name, ends with a '='
				j = Line.IndexOf('=',i);
				if(j==-1)
					throw new TDException(Messages.Pool_InvalidInputWhenParsingIni, false, TDExceptionIdentifier.PRHInvalidInputWhenParsingIniFile);
				ValueName = Line.Substring(i,j-i).Trim();
				if((i = Line.IndexOf(';',j+1))!=-1)//remove comments from end of line
					Value = Line.Substring(j+1,i-(j+1)).Trim();
				else 
					Value = Line.Substring(j+1).Trim();
				if(CurrentKey != null)
				{
					CurrentKey[ValueName] = Value;
				}
				else
				{
					Values[ValueName] = Value;
				}
			}
		}
	}
}


