// *********************************************** 
// NAME         : SetupAction.cs
// AUTHOR       : Mitesh Modi
// DATE CREATED : 02/02/2011
// DESCRIPTION  : Class SetupAction
// ************************************************ 
// $Log:   P:/TDPortal/archives/Utilities/HttpRequestValidator/HttpRequestValidatorSetupHelper/SetupAction.cs-arc  $
//
//   Rev 1.2   Mar 04 2011 11:34:26   mmodi
//Updated version to be 1.0.0.2
//Resolution for 5671: Http Request Validator IIS tool
//
//   Rev 1.1   Feb 08 2011 09:36:54   mmodi
//Updated to remove configuration sections from web.config during an uninstall
//
//   Rev 1.0   Feb 03 2011 17:17:26   mmodi
//Initial revision.
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;

namespace AO.HttpRequestValidatorSetupHelper
{
    [RunInstaller(true)]
    public partial class SetupAction : Installer
    {
        #region Private members

        private string moduleProviderUIName = string.Empty;
        private string moduleProviderUIType = string.Empty;
        private string moduleName = string.Empty;
        private string moduleType = string.Empty;
        private string moduleSectionName = string.Empty;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public SetupAction()
        {
            InitializeComponent();

            // Module values
            moduleProviderUIName = "HttpRequestValidatorUI";
            moduleProviderUIType = "AO.HttpRequestValidatorUI.HttpRequestValidatorUIModuleProvider, ao.httprequestvalidatorui, Version=2.1.0.0, Culture=neutral, PublicKeyToken=c0941950aa29108c";
            moduleName = "HttpRequestValidatorModule";
            moduleType = "AO.HttpRequestValidator.RequestValidatorModule, ao.httprequestvalidator, Version=2.1.0.0, Culture=neutral, PublicKeyToken=a94d451d2bf09f95";
            moduleSectionName = "httpRequestValidator";
        }

        #endregion

        #region Installer methods

        /// <summary>
        /// Install method
        /// </summary>
        /// <param name="stateSaver"></param>
        public override void Install(System.Collections.IDictionary stateSaver)
        {
            base.Install(stateSaver);
            
            // Add a Server Module Provider UI to administration.config
            // This displays the module icon in IIS
            InstallUtil.AddUIModuleProvider(moduleProviderUIName,moduleProviderUIType);
                
            // Add a Server Module to applicationHost.config 
            // This adds the module to each site in IIS
            InstallUtil.AddModule(moduleName, moduleType);

            // Add the Section configuration for the Module
            // This updates the sections list to allow configuration for the module
            InstallUtil.AddSectionGroup(moduleSectionName);
        }

        /// <summary>
        /// Uninstall method
        /// </summary>
        /// <param name="savedState"></param>
        public override void Uninstall(System.Collections.IDictionary savedState)
        {
            base.Uninstall(savedState);

            // Remove all trace of the module and its configurations
            InstallUtil.RemoveUIModuleProvider(moduleProviderUIName);
            InstallUtil.RemoveModule(moduleName);
            InstallUtil.RemoveSectionConfig(moduleSectionName);
            InstallUtil.RemoveSectionGroup(moduleSectionName);
        }

        #endregion
    }
}
