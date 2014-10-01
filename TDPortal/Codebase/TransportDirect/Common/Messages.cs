// *********************************************** 
// NAME                 : Messages.cs 
// AUTHOR               : Gary Eaton
// DATE CREATED         : 11/07/2003 
// DESCRIPTION			: Container for messages
// used by common TDP components.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Common/Messages.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:19:04   mturner
//Initial revision.
//
//   Rev 1.4   Oct 08 2003 16:32:52   geaton
//Added message to support error reading types from assembly.
//
//   Rev 1.3   Sep 18 2003 15:51:42   geaton
//Changed ambiguous message.
//
//   Rev 1.2   Sep 04 2003 16:19:54   geaton
//Updated error message for bad assemblies.
//
//   Rev 1.1   Sep 04 2003 09:53:28   geaton
//Updated file header.

using System;

namespace TransportDirect.Common
{
	
	public class Messages
	{
		// PropertyValidator Messages
		public const string PropertyValueBad = "Property with key [{0}] has invalid value of [{1}]. Usage: [{2}].";
		public const string ClassNotFoundInAssembly = "Class [{0}] specified in property key [{1}] not found in assembly [{2}].";
		public const string BadAssembly = "Assembly specified in property key [{0}] cannot be loaded for the reason: [{1}]. Ensure the assembly name specified in properties does not include .dll extension. A full file path is also not necessary.";
		public const string PropertyValueMissing = "A value for the property specified in key [{0}] is missing.";
		public const string InvalidPropertyKey = "The property key [{0}] is invalid and must be removed completely.";
		public const string InvalidPropertyLength = "Property [{0}] specified with key [{1}] has an invalid length. Length must be between [{2}] and [{3}].";
		public const string InvalidPropertyLengthMin = "Property [{0}] specified with key [{1}] has an invalid length. Length must be at least [{2}].";
		public const string EnumConversionError = "Error converting property value [{0}] to enumerated type [{1}].";
		public const string PropertyKeyMissing = "The property key [{0}] has not been defined. This property key is mandatory.";
		public const string AssemblyTypeReadFail = "Unable to inspect types defined in assembly [{0}]. This assembly has been declared as containing the class specified in key [{1}]. Ensure that assemblies containing types used by this assembly are in same directory as the assembly specified.";

		static Messages()
		{}
	}
}
