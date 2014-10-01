// *********************************************** 
// NAME             : PickList.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 03 Jul 2013
// DESCRIPTION  	: Maintains an Xml picklist entry
// ************************************************
// 
                
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;

namespace TDP.Common.LocationService.Gazetteer
{
    /// <summary>
    /// Maintains an Xml picklist entry
    /// </summary>
    public class PickList
    {
        #region Constants

        private const string pickListCriteriaName = "PICKLIST_CRITERIA";
        private const string criteriaName = "CRITERIA";
        private const string fieldName = "FIELD";
        private const string valueName = "VALUE";

        #endregion

        private List<Criteria> criterias;

        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        public PickList()
        {
            criterias = new List<Criteria>();

        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="pickListXml"></param>
        public PickList(string pickListXml)
            : this()
        {
            using (StringReader sr = new StringReader(pickListXml))
            {
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
                            && !(reader.Name == criteriaName
                                    && reader.NodeType == XmlNodeType.EndElement)
                            )
                        {
                            if (reader.NodeType == XmlNodeType.Element)
                            {
                                switch (reader.Name)
                                {
                                    case fieldName:
                                        {
                                            reader.Read();
                                            if (reader.NodeType == XmlNodeType.Text)
                                                field = reader.Value;
                                            break;
                                        }
                                    case valueName:
                                        {
                                            reader.Read();
                                            if (reader.NodeType == XmlNodeType.Text)
                                                value = reader.Value;
                                            break;
                                        }
                                }
                            }
                        }
                        criterias.Add(new Criteria(field, value));
                    }
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
                string current = "<" + pickListCriteriaName + ">";

                foreach (Criteria criteria in criterias)
                {
                    current += criteria.Current;
                }

                current += "</" + pickListCriteriaName + ">";

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
        public void RemoveLast()
        {
            criterias.RemoveAt(criterias.Count - 1);
        }

        /// <summary>
        /// Clears all criterias
        /// </summary>
        public void Clear()
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
                    string current = "<" + criteriaName + ">";
                    current += "<" + fieldName + ">";
                    current += sField;
                    current += "</" + fieldName + ">";

                    current += "<" + valueName + ">";
                    current += sValue;
                    current += "</" + valueName + ">";

                    current += "</" + criteriaName + ">";

                    return current;
                }
            }
        }
    }
}
