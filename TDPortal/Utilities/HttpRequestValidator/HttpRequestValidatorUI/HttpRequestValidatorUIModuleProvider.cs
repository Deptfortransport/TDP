// *********************************************** 
// NAME         : HttpRequestValidatorUIModuleProvider.cs
// AUTHOR       : Mitesh Modi
// DATE CREATED : 28/01/2011
// DESCRIPTION  : Class HttpRequestValidatorUIModuleProvider
// ************************************************ 
// $Log:   P:/TDPortal/archives/Utilities/HttpRequestValidator/HttpRequestValidatorUI/HttpRequestValidatorUIModuleProvider.cs-arc  $
//
//   Rev 1.0   Feb 03 2011 10:15:08   mmodi
//Initial revision.
//

using System;
using System.Security;
using Microsoft.Web.Management.Server;

namespace AO.HttpRequestValidatorUI
{
    class HttpRequestValidatorUIModuleProvider : ModuleProvider
    {
        public override Type ServiceType
        {
            get { return null; }
        }

        public override ModuleDefinition GetModuleDefinition(IManagementContext context)
        {
            return new ModuleDefinition(Name, typeof(HttpRequestValidatorUI).AssemblyQualifiedName);
        }

        public override bool SupportsScope(ManagementScope scope)
        {
            return true;
        }
    }
}
