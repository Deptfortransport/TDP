using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.ComponentModel;
using System.Globalization;

namespace TDP.Common.Extenders
{
    /// <summary>
    /// Contains extension methods for the <see cref="System.String"/> class
    /// </summary>
    public static class StringExtenders
    {
        #region Private constants

        private const string patternPostcode =
            @"\b(GIR\s?0AA)\b|(\b((([A-Z-[QVX]][0-9][0-9]?)|(([A-Z-[QVX]][A-Z-[IJZ]][0-9][0-9]?)|(([A-Z-[QVX??]][0-9][A-HJKSTUW])|([A-Z-[QVX]][A-Z-[IJZ]][0-9][ABEHMNPRVWXY]))))\s?[0-9][A-Z-[C??IKMOV]]{2})\b)";
        //@"((\b[A-Z]{1,2}[0-9][A-Z0-9]?\s?[0-9][ABD-HJLNP-UW-Z]{2}\b)|(^([A-Z]{1}[0-9]{1})$|^([A-Z]{1}[0-9]{2})$|^([A-Z]{2}[0-9]{1})$|^([A-Z]{2}[0-9]{2})$))";
        private const string patternOutcode = @"\b([A-Z,a-z]{1,2}[0-9]{1,2}[A-Z,a-z]{0,1})\b";
        private const string patternIncode = @"\b([ ]{0,3}[0-9][A-Z-[C??IKMOV]]{2})\b";
        //@"\b([ ]{0,3}[0-9]{1,1}[A-Z,a-z]{0,2})\b";

        #endregion

        /// <summary>
        /// Extension method to validate email address
        /// </summary>
        /// <param name="address">email address string to validate</param>
        /// <returns>true if the email address is valid otherwise false</returns>
        public static bool IsValidEmailAddress(this string address)
        {
            Regex regex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
            return regex.IsMatch(address);
        }

        /// <summary>
        /// Extension method to validate United Kingdom postcodes
        /// </summary>
        /// <param name="postcode">United Kingdom postcode</param>
        /// <returns>true if the postcode is valid otherwise false</returns>
        public static bool IsValidPostcode(this string postcode)
        {
            if (string.IsNullOrEmpty(postcode))
                return false;

            Regex regex = new Regex(patternPostcode, RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);

            string result = regex.Replace(postcode, "", 1);

            if (result == string.Empty)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Extension method to validate United Kingdom partial postcodes
        /// </summary>
        /// <param name="postcode">United Kingdom postcode</param>
        /// <returns>true if the postcode is a valid partial postcode otherwise false</returns>
        public static bool IsValidPartPostcode(this string postcode)
        {
            if (string.IsNullOrEmpty(postcode))
                return false;

            if (!postcode.IsValidPostcode())
            {
                // Check for an Outcode?
                Regex regex = new Regex(patternOutcode, RegexOptions.IgnorePatternWhitespace);

                string result = regex.Replace(postcode, "", 1);

                if (result == string.Empty)
                    return true;
                else
                {
                    // Is there an Incode?
                    regex = new Regex(patternIncode, RegexOptions.None);
                    result = regex.Replace(result, "").Trim();
                    if (result == string.Empty)
                        return true;
                    else
                        return false;
                }
            }
            
            return false;
        }

        /// <summary>
        /// Extension method to input text string contains a valide United Kingdom postcode
        /// </summary>
        /// <param name="postcode">United Kingdom postcode</param>
        /// <returns>true if the postcode is valid otherwise false</returns>
        public static bool IsContainsPostcode(this string text)
        {
            if (string.IsNullOrEmpty(text))
                return false;

            // If it is a postcode, then it contains postcode
            Regex regex = new Regex(patternPostcode, RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);

            return regex.IsMatch(text);
        }

        /// <summary>
        /// Extension method if the input text string is a single word
        /// </summary>
        /// <param name="text">input text string</param>
        /// <returns>true if is a single word</returns>
        public static bool IsNotSingleWord(this string text)
        {
            string alphanumeric = "[^A-Za-z0-9']";

            if (string.IsNullOrEmpty(text))
                return false;
            else
            {
                //Trim the string
                text.Trim();

                // creates the Regex postcode object
                Regex regexAddress = new Regex(alphanumeric);
                bool result = regexAddress.IsMatch(text);

                return result;
            }
        }

        /// <summary>
        /// Coverts string to other types i.e. int, double
        /// </summary>
        /// <typeparam name="T">Type the string converting to</typeparam>
        /// <param name="value">string value</param>
        /// <returns></returns>
        public static T Parse<T>(this string value)
        {
            // Get default value for type so if string
            // is empty then we can return default value.
            T result = default(T);

            if (!string.IsNullOrEmpty(value))
            {
                try
                {
                    TypeConverter converter = TypeDescriptor.GetConverter(typeof(T));
                    result = (T)converter.ConvertFromString(value);

                }
                catch
                {
                    result = default(T);

                }
            }

            return result;
        }

        /// <summary>
        /// Coverts string to other types i.e. int, double
        /// </summary>
        /// <typeparam name="T">Type the string converting to</typeparam>
        /// <param name="value">String value</param>
        /// <param name="defaultValue">Default value to return when conversion fails</param>
        /// <returns></returns>
        public static T Parse<T>(this string value, T defaultValue)
        {
            // Get default value for type so if string
            // is empty then we can return default value.
            T result = default(T);

            if (!string.IsNullOrEmpty(value))
            {
                try
                {
                    TypeConverter converter = TypeDescriptor.GetConverter(typeof(T));
                    result = (T)converter.ConvertFromString(value);

                }
                catch
                {
                    if (defaultValue != null)
                    {
                        result = defaultValue;
                    }
                    else
                    {
                        result = default(T);
                    }

                }
            }
            else
            {
                if (defaultValue != null)
                {
                    result = defaultValue;
                }
                else
                {
                    result = default(T);
                }
            }

            return result;
        }

        /// <summary>
        /// Coverts string to other types i.e. int, double
        /// </summary>
        /// <typeparam name="T">Type the string converting to</typeparam>
        /// <param name="value">String value</param>
        /// <param name="defaultValue">Default value to return when conversion fails</param>
        /// <returns></returns>
        public static T Parse<T>(this string value, T defaultValue, CultureInfo cultureInfo)
        {
            // Get default value for type so if string
            // is empty then we can return default value.
            T result = default(T);

            if (!string.IsNullOrEmpty(value))
            {
                try
                {
                    TypeConverter converter = TypeDescriptor.GetConverter(typeof(T));
                    result = (T)converter.ConvertFromString(null, cultureInfo, value);

                }
                catch
                {
                    if (defaultValue != null)
                    {
                        result = defaultValue;
                    }
                    else
                    {
                        result = default(T);
                    }

                }
            }

            return result;
        }

        /// <summary>
        /// Makes the string to title case - a capital letter for the initial letter of each word 
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string TitleCase(this string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return string.Empty;
            }

            List<char> result = new List<char>();

            // Work through each character of all words,
            // allow checks for words:
            // - "O'Neills" where the O and N need to be capitals
            // - "McIntire" 

            bool newWord = true;
            bool checkApostrophe = true;
            bool isPreviousApostrophe = false;
            bool checkHyphen = true;
            bool isPreviousHyphen = false;

            bool isPreviousChar1_M = false;
            bool isPreviousChar2_C = false;

            foreach (char c in text)
            {
                if (newWord)
                {
                    // First char of new word is upper
                    result.Add(Char.ToUpper(c));
                    newWord = false;

                    // Check for "Mc" scenario
                    if (Char.ToUpper(c) == 'M')
                        isPreviousChar1_M = true;
                }
                else
                {
                    // Previous char at start of word was apostrophe so this char is uppercase
                    if (isPreviousApostrophe)
                    {
                        result.Add(Char.ToUpper(c));
                        isPreviousApostrophe = false;
                    }
                    // Previous char anywhere in word was hyphen so this char is uppercase
                    else if (isPreviousHyphen)
                    {
                        result.Add(Char.ToUpper(c));
                        isPreviousHyphen = false;
                        checkHyphen = true; // Check again as can be multiple hyphens Bidford-On-Avon
                    }
                    // Previous two chars at start of word are "Mc" so this char is uppercase
                    else if (isPreviousChar1_M && isPreviousChar2_C)
                    {
                        result.Add(Char.ToUpper(c));
                        isPreviousChar1_M = false;
                        isPreviousChar2_C = false;
                    }
                    else
                    {
                        // Otherwise its another char in a word, lowercase
                        result.Add(Char.ToLower(c));

                        // Check for "Mc" scenario
                        if (isPreviousChar1_M && (Char.ToLower(c) == 'c'))
                            isPreviousChar2_C = true;
                        else
                        {
                            isPreviousChar1_M = false;
                        }
                    }

                    if (checkApostrophe && c == '\'')
                        isPreviousApostrophe = true;
                    else if (checkHyphen && c == '-')
                    {
                        isPreviousHyphen = true;
                        checkHyphen = false;
                    }

                    checkApostrophe = false;
                }

                if (c == ' ')
                {
                    newWord = true;
                    checkApostrophe = true; // Only check apostrophe when new word detected
                    isPreviousApostrophe = false;
                    checkHyphen = true;
                    isPreviousHyphen = false;

                    isPreviousChar1_M = false;
                    isPreviousChar2_C = false;
                }
            }

            return new string(result.ToArray());
        }

        /// <summary>
        /// Makes the first character in the string Lower case
        /// </summary>
        /// <param name="text">String to change</param>
        /// <returns>String with first character as lower case</returns>
        public static string LowercaseFirst(this string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return string.Empty;
            }

            char[] a = text.ToCharArray();
            a[0] = char.ToLower(a[0]);
            return new string(a);
        }
        
        /// <summary>
        /// Retrieves a substring from this instance. The substring starts at character position 0 and retrieves
        /// upto the specified length
        /// </summary>
        /// <param name="length">Length</param>
        /// <returns>String upto the specified length</returns>
        public static string SubstringFirst(this string text, int length)
        {
            if (string.IsNullOrEmpty(text))
            {
                return string.Empty;
            }

            if (text.Length <= length)
            {
                return text;
            }
            else
            {
                return text.Substring(0, length);
            }
        }

        /// <summary>
        /// Retrieves a substring from this instance. The substring starts at character position 0 and retrieves
        /// upto the specified character. If not found, the string is returned
        /// </summary>
        /// <param name="character">character</param>
        /// <returns>String upto the specified character</returns>
        public static string SubstringFirst(this string text, char character)
        {
            if (string.IsNullOrEmpty(text))
            {
                return string.Empty;
            }

            if (!text.Contains(character))
            {
                return text;
            }
            else
            {
                return text.Substring(0, text.IndexOf(character));
            }
        }

        /// <summary>
        /// Applies simple address formatting to the address for display
        /// </summary>
        /// <param name="text">String to change</param>
        /// <returns>Address formatted string</returns>
        public static string AddressFormat(this string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return string.Empty;
            }

            // Put a "space" after each comma ',' in the address
            string patternCommaSpace = @"([,]\s*)";

            Regex regex = new Regex(patternCommaSpace, RegexOptions.IgnorePatternWhitespace);

            string result = regex.Replace(text, ", ");

            return result;
        }

        /// <summary>
        /// Checks if the string matches the regular expression
        /// </summary>
        /// <param name="text">Text to match</param>
        /// <param name="regex">Regular expression</param>
        /// <returns>True if the text matches the regular expression</returns>
        public static bool MatchesRegex(this string text, string regex)
        {
            if (text == null || regex == null)
                return false;
            else
            {
                Match match = Regex.Match(text, regex);
                return match.Success;
            }
        }
    }
}
