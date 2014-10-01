// *********************************************** 
// NAME			: DefaultAddemblyInfo.cs
// AUTHOR		: Jason Brown
// DATE CREATED	: 18/09/2006
// REVISION		: $Revision$
// DESCRIPTION	: PROJECT WIDE Assembly Information
// ************************************************ 
// NOTE - this file is shared with all projects.
// Changing this file will chanage it for all projects.
// ************************************************ 
// $Log$ 
// 
// 

using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Atos")]
[assembly: AssemblyProduct("WebTIS Library")]
[assembly: AssemblyCopyright("Copyright © Atos 2012")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// All assemblies are CLSCompliant
[assembly: CLSCompliant(true)]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Revision and Build Numbers 
// by using the '*' as shown below:
[assembly: AssemblyVersion("5.3.8.2")]
[assembly: AssemblyFileVersion("5.3.8.2")]

// [CoverageExclude] should be applied to those classes/methods 
// which should be excluded from coverage statistics.
internal sealed class CoverageExcludeAttribute : System.Attribute {}
