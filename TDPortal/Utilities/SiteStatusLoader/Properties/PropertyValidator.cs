// *********************************************** 
// NAME                 : PropertyValidator.cs
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 01/04/2009
// DESCRIPTION			: PropertyValidator class. Provides common methods to validate properties
// ************************************************ 
// $Log:   P:/TDPortal/archives/Utilities/SiteStatusLoader/Properties/PropertyValidator.cs-arc  $
//
//   Rev 1.0   Apr 01 2009 13:30:56   mmodi
//Initial revision.
//Resolution for 5273: Site Status Loader

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Globalization;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

using AO.Common;

namespace AO.Properties
{
    /// <summary>
    /// Property Validator class
    /// </summary>
    public abstract class PropertyValidator
    {
        abstract public bool ValidateProperty(string key, ArrayList errors);

        protected enum Optionality : int
        {
            Optional,
            Mandatory,
            Undefined
        }

        protected Properties properties;


        public PropertyValidator(Properties properties)
        {
            this.properties = properties;
        }

        /// <summary>
        /// method that checks a class defined in the properties by a key exists in a given assembly
        /// </summary>
        /// <param name="key"></param>
        /// <param name="myAssembly"></param>
        /// <param name="errors"></param>
        /// <returns>returs true if exists false otherwise</returns>
        protected bool ValidateClassExists(string key, Assembly myAssembly, ArrayList errors)
        {
            bool exists = false;
            string className = properties[key];
            bool validated = false;

            try
            {
                Type[] assemblyTypes = myAssembly.GetTypes();

                foreach (Type assemblyType in assemblyTypes)
                {
                    if (assemblyType.Name == className)
                    {
                        exists = true;
                    }
                }

                validated = true;
            }
            catch (Exception) // no specific exceptions documented so catch all
            {
                errors.Add(String.Format(Messages.PVAssemblyTypeReadFail, myAssembly.FullName, key));
            }

            if ((!exists) && (validated))
            {
                errors.Add(String.Format(Messages.PVClassNotFoundInAssembly,
                           className,
                           key,
                           myAssembly.FullName));
            }

            return exists;
        }

        /// <summary>
        /// Return the assembly against an assembly name stored in the properties
        /// </summary>
        /// <param name="key"></param>
        /// <param name="errors"></param>
        /// <returns>Return an assembly if exists</returns>
        protected Assembly ValidateAssembly(string key, ArrayList errors)
        {
            string property = properties[key];
            Assembly assembly = null;

            try
            {
                assembly = Assembly.Load(property);
            }
            catch (ArgumentNullException argumentNullException)
            {
                errors.Add(String.Format(Messages.PVBadAssembly,
                    key, argumentNullException.Message, Environment.CurrentDirectory.ToString()));
            }
            catch (FileNotFoundException fileNotFoundException)
            {
                errors.Add(String.Format(Messages.PVBadAssembly,
                    key, fileNotFoundException.Message, Environment.CurrentDirectory.ToString()));
            }
            catch (BadImageFormatException badImageFormatException)
            {
                errors.Add(String.Format(Messages.PVBadAssembly,
                    key, badImageFormatException.Message, Environment.CurrentDirectory.ToString()));
            }
            catch (Exception exception)// catch undocumented exceptions
            {
                errors.Add(String.Format(Messages.PVBadAssembly,
                    key, exception.Message, Environment.CurrentDirectory.ToString()));
            }

            return assembly;
        }
        /// <summary>
        /// Checks first if the property at the given key is defined when the property is mandatory.
        /// Validates also if an enum property in the properties exists in a given type.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="t"></param>
        /// <param name="optionality"></param>
        /// <param name="errors"></param>
        /// <returns>return true if the property exist in the type</returns>
        protected bool ValidateEnumProperty(string key,
            Type t,
            Optionality optionality,
            ArrayList errors)
        {
            string property = properties[key];
            bool Valid = true;

            if (property == null)
            {
                if (optionality.Equals(Optionality.Mandatory))
                {
                    errors.Add(String.Format(Messages.PVPropertyValueMissing, key));
                    Valid = false;
                }
            }
            else
            {
                bool found = false;
                StringBuilder usage = new StringBuilder();

                foreach (FieldInfo fieldInfo in t.GetFields())
                {
                    if (fieldInfo.Name == property)
                        found = true;

                    usage.Append(fieldInfo.Name);
                    usage.Append("|");
                }

                if (!found)
                {
                    Valid = false;
                    usage.Replace("value__", "");
                    errors.Add(String.Format(Messages.PVPropertyValueBad, key, property, usage));
                }
            }

            return Valid;
        }
        /// <summary>
        /// checks that a property at a given key is defined when the property is mandatory/ undefined optionality
        /// </summary>
        /// <param name="key"></param>
        /// <param name="optionality"></param>
        /// <param name="errors"></param>
        /// <returns></returns>
        protected bool ValidateExistence(string key, Optionality optionality, ArrayList errors)
        {
            bool Valid = true;

            if (optionality.Equals(Optionality.Mandatory))
            {
                string property = properties[key];
                if (property == null)
                {
                    Valid = false;
                    errors.Add(String.Format(Messages.PVPropertyKeyMissing, key));
                }
            }

            if (optionality.Equals(Optionality.Undefined))
            {
                string property = properties[key];
                if (property != null)
                {
                    Valid = false;
                    errors.Add(String.Format(Messages.PVInvalidPropertyKey, key));
                }
            }

            return Valid;
        }

        /// <summary>
        /// Checks that a property at given key does not exceed the min/max boundaries
        /// </summary>
        /// <param name="key"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="errors"></param>
        /// <returns>returns true if the length is valid, false otherwise</returns>
        protected bool ValidateLength(string key, int min, int max, ArrayList errors)
        {
            bool valid = true;
            string property = properties[key];
            int length = property.Length;

            if ((length < min) || (length > max))
            {
                valid = false;
                errors.Add(String.Format(Messages.PVInvalidPropertyLength,
                    property, key, min.ToString(CultureInfo.CurrentCulture.NumberFormat), max.ToString(CultureInfo.CurrentCulture.NumberFormat)));
            }

            return valid;
        }

        /// <summary>
        /// Checks that a property at a given key does not exceed the min boudary.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="min"></param>
        /// <param name="errors"></param>
        /// <returns>returns true if the length is valid, false otherwise</returns>
        protected bool ValidateLength(string key, int min, ArrayList errors)
        {
            bool valid = true;
            string property = properties[key];
            int length = property.Length;

            if (length < min)
            {
                valid = false;
                errors.Add(String.Format(Messages.PVInvalidPropertyLengthMin,
                    property, key, min.ToString(CultureInfo.CurrentCulture.NumberFormat)));
            }

            return valid;
        }

        /// <summary>
        /// Returns the Enum value from a string in a given type
        /// </summary>
        /// <param name="t">
        /// The enum type.
        /// </param>
        /// <param name="strValue">
        /// The value to convert to the given enum type.
        /// </param>
        /// <returns>
        /// Returns the Enum type if exists, throws an exception otherwise
        /// </returns>
        /// <exception cref="SSException">
        /// Value to convert does not have a matching enum value.
        /// </exception>
        public static object StringToEnum(Type t, string strValue)
        {
            foreach (FieldInfo fieldInfo in t.GetFields())
                if (fieldInfo.Name == strValue)
                    return fieldInfo.GetValue(null);

            throw new SSException(String.Format(Messages.PVEnumConversionError,
                strValue, t.FullName), false, SSExceptionIdentifier.PSBadEnum);
        }

        /// <summary>
        /// Checks to see if a string passed is a Whole number
        /// </summary>
        /// <param name="strNumber">String to evaluate</param>
        /// <returns>True if string is whole number.</returns>
        public static bool IsWholeNumber(String strNumber)
        {
            Regex objNotWholePattern = new Regex("[^0-9]");
            return !objNotWholePattern.IsMatch(strNumber);
        }

    }
}
