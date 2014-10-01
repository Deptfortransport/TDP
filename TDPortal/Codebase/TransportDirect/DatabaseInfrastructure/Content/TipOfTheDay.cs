//// ***********************************************
//// NAME           : TipOfTheDay.cs
//// AUTHOR 		: Steve Barker
//// DATE CREATED   : 18-Mar-2008
//// DESCRIPTION 	: Class to represent a "Tip of the Day"
//// ************************************************
////
////    Rev Devfactory Mar 18 2008 sbarker
////    Initial version

using System;
using System.Collections.Generic;
using System.Text;

namespace TransportDirect.Common.DatabaseInfrastructure.Content
{
    /// <summary>
    /// Class to represent a "Tip of the Day"
    /// </summary>
    internal class TipOfTheDay
    {
        #region Private Fields

        private readonly object valuesLock = new object();
        private Dictionary<Language, string> values = new Dictionary<Language, string>();

        #endregion

        #region Constructor

        internal TipOfTheDay()
        {
            //No implementation
        }

        #endregion

        #region Internal Methods
        
        internal void AddValue(Language language, string value)
        {
            lock (valuesLock)
            {
                if (values.ContainsKey(language))
                {
                    values[language] = value;
                }
                else
                {
                    values.Add(language, value);
                }                
            }
        }

        #endregion

        #region Internal Indexers

        internal string this[Language language]
        {
            get
            {
                lock (valuesLock)
                {
                    if (values.ContainsKey(language))
                    {
                        return values[language];
                    }

                    string message = string.Format("No Tip available for language {0}", language.ToString());
                    throw new TDException(message, false, TDExceptionIdentifier.Undefined);
                }
            }
        }

        #endregion
    }
}
