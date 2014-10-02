// *********************************************** 
// NAME                 : Picklist.cs
// AUTHOR               : Patrick ASSUIED
// DATE CREATED         : 01/09/2003 
// DESCRIPTION  : Class in charge of maintaining the Xml picklist entry
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/LocationService/PickList.cs-arc  $ 
//
//   Rev 1.1   Nov 17 2008 14:09:14   pscott
//SCR5173 - When addresses are drilled down that contain ampersands then the picklist becomes non-xml complient and the portal will crash.
//Changed property 'current' to change & to &amp;
//
// 
//Resolution for 5173: Ampersand causing portal crash when drilling down addresses
//
//   Rev 1.0   Nov 08 2007 12:25:18   mturner
//Initial revision.
//
//   Rev 1.2   Sep 22 2003 17:31:26   passuied
//made all objects serializable
//
//   Rev 1.1   Sep 09 2003 17:23:54   passuied
//Clean up all files to pass code review!
//
//   Rev 1.0   Sep 05 2003 15:30:36   passuied
//Initial Revision

using System;
using System.Collections;
using System.IO;
using System.Xml;

namespace TransportDirect.UserPortal.LocationService
{
	/// <summary>
	/// Class in charge of maintaining the Xml picklist entry
	/// </summary>
	[Serializable()]
	public class PickList
	{
		#region constants
		private const string pickListCriteriaName = "PICKLIST_CRITERIA";
		private const string criteriaName = "CRITERIA";
		private const string fieldName = "FIELD";
		private const string valueName = "VALUE";
		#endregion

		private ArrayList criterias;

		#region Constructors
		public PickList()
		{
			criterias = new ArrayList();

		}

		public PickList(string pickListXml): this()
		{


			StringReader sr = new StringReader(pickListXml);
			XmlTextReader reader = new XmlTextReader(sr);

			while (reader.Read())
			{
				// if node is <CRITERIA>
				if (reader.Name == criteriaName 
					&& reader.NodeType == XmlNodeType.Element)
				{
					string field = string.Empty;
					string value = string.Empty;

					// while not end of file and not node </CRITERIA>
					while (reader.Read() 
						&&!( reader.Name == criteriaName 
								&& reader.NodeType == XmlNodeType.EndElement)
						)
					{
						if (reader.NodeType == XmlNodeType.Element)
						{
							switch (reader.Name)
							{
								case fieldName :
								{
									reader.Read();
									if (reader.NodeType == XmlNodeType.Text)
										field = reader.Value;
									break;
								}
								case valueName :
								{
									reader.Read();
									if (reader.NodeType == XmlNodeType.Text)
										value = reader.Value;
									break;
								}
							}
						}
					}
					criterias.Add (new Criteria(field, value));
				}
			}
		}
		#endregion


		/// <summary>
		/// Read-only property. returns the string value of the current pickList
		/// </summary>
		public string Current
		{
			get
			{
				string current = "<" +pickListCriteriaName +">";
				
				foreach (Criteria criteria in criterias)
				{
					current += criteria.Current;
				}

				current += "</" +pickListCriteriaName +">";

                if (current.Contains("&"))
                {
                    if (!current.Contains("&amp;"))
                    {
                        string newCurrent = null;
                        int pos = current.IndexOf("&");
                        newCurrent = current.Substring(0, pos);
                        newCurrent += "&amp;";
                        newCurrent += current.Substring(pos + 1);
                        current = newCurrent;
                    }
                } 
                    return current;
			}
		}

		/// <summary>
		/// Adds a criteria to the list
		/// </summary>
		/// <param name="sField">criteria</param>
		/// <param name="sValue">value</param>
		public void Add(string sField, string sValue)
		{
			Criteria newCriteria = new Criteria(sField, sValue);


			criterias.Add(newCriteria);
		}
		
		/// <summary>
		/// Removes the last criteria
		/// </summary>
		public void RemoveLast ()
		{
			criterias.RemoveAt(criterias.Count-1);
		}

		/// <summary>
		/// Clears all criterias
		/// </summary>
		public void Clear ()
		{
			criterias.Clear();
		}

		/// <summary>
		/// Criteria object
		/// </summary>
		class Criteria
		{
			private string sField = string.Empty;
			private string sValue = string.Empty;

			public Criteria(string field, string value)
			{
				sField = field;
				sValue = value;
			}

			/// <summary>
			/// Read-only property. Returns the string value of the current criteria
			/// </summary>
			public string Current
			{
				get
				{
					string current = "<" +criteriaName +">";
					current += "<" +fieldName +">";
					current += sField;
					current += "</" +fieldName +">";
					
					current += "<" +valueName +">";
					current += sValue;
					current += "</" +valueName +">";

					current += "</" +criteriaName +">";

					return current;
				}
			}
		}


	}
}
