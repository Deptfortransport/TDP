// *********************************************** 
// NAME                 : PropertyValidator.cs 
// AUTHOR               : Gary Eaton
// DATE CREATED         : 11/07/2003 
// DESCRIPTION  : Property Validator
// ************************************************ 
// updated by Patrick ASSUIED on 11/07/2003 : addition of comments and changes after code review
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PropertiesService/Properties/PropertyValidator.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:37:54   mturner
//Initial revision.
//
//   Rev 1.10   Nov 18 2003 20:39:50   geaton
//Added static validation method. (Previously in the importer project.)
//
//   Rev 1.9   Oct 20 2003 13:23:04   geaton
//Made StringToEnum method static to provide ease of reuse.
//
//   Rev 1.8   Oct 08 2003 16:34:46   geaton
//Updated to use Assembly.Load rather than Assembly.LoadFrom.
//
//   Rev 1.7   Oct 03 2003 13:38:40   PNorell
//Updated the new exception identifier.
//
//   Rev 1.6   Sep 18 2003 15:51:28   geaton
//Changed ambiguous message.
//
//   Rev 1.5   Sep 04 2003 16:19:08   geaton
//Updated error message for bad assemblies.
//
//   Rev 1.4   Jul 23 2003 10:23:00   passuied
//Changes after PropertyService namespaces / dll renaming
//
//   Rev 1.3   Jul 17 2003 17:18:14   passuied
//added IFormatProvider
//
//   Rev 1.2   Jul 17 2003 15:00:36   passuied
//updated

using System;
using System.Collections;
using System.Text.RegularExpressions;
using System.Reflection;
using System.Text;
using System.IO;
using System.Diagnostics;
using TransportDirect.Common;
using System.Globalization;


namespace TransportDirect.Common.PropertyService.Properties
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

		protected IPropertyProvider properties;


		public PropertyValidator(IPropertyProvider properties)
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
				errors.Add(String.Format(TransportDirect.Common.Messages.AssemblyTypeReadFail, myAssembly.FullName, key));
			}

			if ((!exists) && (validated))
			{
				errors.Add(String.Format(TransportDirect.Common.Messages.ClassNotFoundInAssembly,
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
				errors.Add(String.Format(TransportDirect.Common.Messages.BadAssembly, 
					key, argumentNullException.Message, Environment.CurrentDirectory.ToString()));
			}
			catch (FileNotFoundException fileNotFoundException)
			{
				errors.Add(String.Format(TransportDirect.Common.Messages.BadAssembly, 
					key, fileNotFoundException.Message, Environment.CurrentDirectory.ToString()));
			}
			catch (BadImageFormatException badImageFormatException)
			{
				errors.Add(String.Format(TransportDirect.Common.Messages.BadAssembly, 
					key, badImageFormatException.Message, Environment.CurrentDirectory.ToString()));
			}
			catch (Exception exception)// catch undocumented exceptions
			{
				errors.Add(String.Format(TransportDirect.Common.Messages.BadAssembly, 
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
					errors.Add(String.Format(TransportDirect.Common.Messages.PropertyValueMissing, key));
					Valid = false;
				}
			}
			else
			{
				bool found = false;
				StringBuilder usage = new StringBuilder();

				foreach ( FieldInfo fieldInfo in t.GetFields() )
				{
					if ( fieldInfo.Name == property )
						found = true;		

					usage.Append(fieldInfo.Name);
					usage.Append("|");
				}
	
				if (!found)
				{
					Valid = false;
					usage.Replace("value__", "");	
					errors.Add(String.Format(TransportDirect.Common.Messages.PropertyValueBad, key, property, usage)); 
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
					errors.Add(String.Format(TransportDirect.Common.Messages.PropertyKeyMissing, key));
				}
			}

			if (optionality.Equals(Optionality.Undefined))
			{
				string property = properties[key];
				if (property != null)
				{
					Valid = false;
					errors.Add(String.Format(TransportDirect.Common.Messages.InvalidPropertyKey, key));
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
				errors.Add(String.Format(TransportDirect.Common.Messages.InvalidPropertyLength,
					property, key,  min.ToString( CultureInfo.CurrentCulture.NumberFormat), max.ToString(CultureInfo.CurrentCulture.NumberFormat))); 
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
				errors.Add(String.Format(TransportDirect.Common.Messages.InvalidPropertyLengthMin,
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
		/// <exception cref="TDException">
		/// Value to convert does not have a matching enum value.
		/// </exception>
		public static object StringToEnum(Type t, string strValue)
		{
			foreach ( FieldInfo fieldInfo in t.GetFields() )
				if ( fieldInfo.Name == strValue )
					return fieldInfo.GetValue( null );

			throw new TDException(String.Format(TransportDirect.Common.Messages.EnumConversionError,
				strValue, t.FullName), false, TDExceptionIdentifier.PSBadEnum); 
		}

		/// <summary>
		/// Checks to see if a string passed is a Whole number
		/// </summary>
		/// <param name="strNumber">String to evaluate</param>
		/// <returns>True if string is whole number.</returns>
		public static bool IsWholeNumber(String strNumber)
		{
			Regex objNotWholePattern=new Regex("[^0-9]");
			return !objNotWholePattern.IsMatch(strNumber);
		}

	}
}
