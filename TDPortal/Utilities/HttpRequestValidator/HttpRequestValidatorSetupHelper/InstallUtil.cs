// *********************************************** 
// NAME         : InstallUtil.cs
// AUTHOR       : Mitesh Modi
// DATE CREATED : 02/02/2011
// DESCRIPTION  : Class InstallUtil
// ************************************************ 
// $Log:   P:/TDPortal/archives/Utilities/HttpRequestValidator/HttpRequestValidatorSetupHelper/InstallUtil.cs-arc  $
//
//   Rev 1.1   Feb 08 2011 09:36:54   mmodi
//Updated to remove configuration sections from web.config during an uninstall
//
//   Rev 1.0   Feb 03 2011 17:17:26   mmodi
//Initial revision.
//

using System;
using Microsoft.Web.Administration;

namespace AO.HttpRequestValidatorSetupHelper
{
    /// <summary>
    /// InstallUtil class containing helper methods to update IIS congfig files
    /// </summary>
    public static class InstallUtil
    {
        #region Public methods

        /// <summary> 
        /// Registers a new Module in the Modules section inside ApplicationHost.config 
        /// </summary> 
        public static void AddModule(string name, string type)
        {
            using (ServerManager mgr = new ServerManager())
            {
                Configuration appHostConfig = mgr.GetApplicationHostConfiguration();
                ConfigurationSection modulesSection = appHostConfig.GetSection("system.webServer/modules");
                ConfigurationElementCollection modules = modulesSection.GetCollection();

                if (FindByAttribute(modules, "name", name) == null)
                {
                    ConfigurationElement module = modules.CreateElement();
                    module.SetAttributeValue("name", name);
                    if (!String.IsNullOrEmpty(type))
                    {
                        module.SetAttributeValue("type", type);
                    }

                    modules.Add(module);
                }

                mgr.CommitChanges();
            }
        }

        /// <summary>
        /// Registers a new ModuleProvider in the ModuleProviders section inside Administration.config
        /// </summary>
        public static void AddUIModuleProvider(string name, string type)
        {
            using (ServerManager mgr = new ServerManager())
            {

                // First register the Module Provider  
                Configuration adminConfig = mgr.GetAdministrationConfiguration();

                ConfigurationSection moduleProvidersSection = adminConfig.GetSection("moduleProviders");
                ConfigurationElementCollection moduleProviders = moduleProvidersSection.GetCollection();
                if (FindByAttribute(moduleProviders, "name", name) == null)
                {
                    ConfigurationElement moduleProvider = moduleProviders.CreateElement();
                    moduleProvider.SetAttributeValue("name", name);
                    moduleProvider.SetAttributeValue("type", type);
                    moduleProviders.Add(moduleProvider);
                }

                // Now register it so that all Sites have access to this module 
                ConfigurationSection modulesSection = adminConfig.GetSection("modules");
                ConfigurationElementCollection modules = modulesSection.GetCollection();
                if (FindByAttribute(modules, "name", name) == null)
                {
                    ConfigurationElement module = modules.CreateElement();
                    module.SetAttributeValue("name", name);
                    modules.Add(module);
                }

                mgr.CommitChanges();
            }
        }

        /// <summary>
        /// Registers a new Section in the SectionGroups section inside ApplicationHost.config
        /// </summary>
        /// <param name="name"></param>
        public static void AddSectionGroup(string name)
        {
            using (ServerManager mgr = new ServerManager())
            {
                Configuration appHostConfig = mgr.GetApplicationHostConfiguration();

                SectionGroup sectionGroups = appHostConfig.RootSectionGroup;

                if (FindSectionGroup(sectionGroups, "system.webServer") != null)
                {
                    // Get the section group
                    SectionGroup sectionGroup = FindSectionGroup(sectionGroups, "system.webServer");

                    if (FindSectionDefinition(sectionGroup, name) == null)
                    {
                        // Register the section
                        SectionDefinition secDef = sectionGroup.Sections.Add(name);
                        secDef.OverrideModeDefault = "Allow";
                    }
                }

                mgr.CommitChanges();
            }
        }

        /// <summary> 
        /// Create a new Web Application 
        /// </summary> 
        public static void CreateApplication(string siteName, string virtualPath, string physicalPath)
        {
            using (ServerManager mgr = new ServerManager())
            {
                Site site = mgr.Sites[siteName];
                if (site != null)
                {
                    site.Applications.Add(virtualPath, physicalPath);
                }
                mgr.CommitChanges();
            }
        }
                
        /// <summary>
        /// Remove a Web Application
        /// </summary>
        /// <param name="siteName"></param>
        /// <param name="virtualPath"></param>
        public static void RemoveApplication(string siteName, string virtualPath)
        {
            using (ServerManager mgr = new ServerManager())
            {
                Site site = mgr.Sites[siteName];
                if (site != null)
                {
                    Application app = site.Applications[virtualPath];
                    if (app != null)
                    {
                        site.Applications.Remove(app);
                        mgr.CommitChanges();
                    }
                }
            }
        }

        /// <summary> 
        /// Removes the specified module from the Modules section by name 
        /// </summary> 
        public static void RemoveModule(string name)
        {
            using (ServerManager mgr = new ServerManager())
            {
                Configuration appHostConfig = mgr.GetApplicationHostConfiguration();
                ConfigurationSection modulesSection = appHostConfig.GetSection("system.webServer/modules");
                ConfigurationElementCollection modules = modulesSection.GetCollection();
                ConfigurationElement module = FindByAttribute(modules, "name", name);
                if (module != null)
                {
                    modules.Remove(module);
                }

                mgr.CommitChanges();
            }
        }
        
        /// <summary> 
        /// Removes the specified UI Module by name 
        /// </summary> 
        public static void RemoveUIModuleProvider(string name)
        {
            using (ServerManager mgr = new ServerManager())
            {
                // First remove it from the sites 
                Configuration adminConfig = mgr.GetAdministrationConfiguration();
                ConfigurationSection modulesSection = adminConfig.GetSection("modules");
                ConfigurationElementCollection modules = modulesSection.GetCollection();
                ConfigurationElement module = FindByAttribute(modules, "name", name);
                if (module != null)
                {
                    modules.Remove(module);
                }

                // now remove the ModuleProvider 
                ConfigurationSection moduleProvidersSection = adminConfig.GetSection("moduleProviders");
                ConfigurationElementCollection moduleProviders = moduleProvidersSection.GetCollection();
                ConfigurationElement moduleProvider = FindByAttribute(moduleProviders, "name", name);
                if (moduleProvider != null)
                {
                    moduleProviders.Remove(moduleProvider);
                }

                mgr.CommitChanges();
            }
        }

        /// <summary> 
        /// Removes the specified config section from all Site Application's web.config files
        /// </summary> 
        public static void RemoveSectionConfig(string name)
        {
            using (ServerManager mgr = new ServerManager())
            {
                // Loop through all of the sites/applications and remove any validator config values
                // that may have been added for this section
                foreach (Site site in mgr.Sites)
                {
                    foreach (Application app in site.Applications)
                    {
                        Configuration webConfig = null;

                        try
                        {
                            // Read the local web.config file
                            webConfig = app.GetWebConfiguration();

                            ConfigurationSection cs = webConfig.GetSection("system.webServer/" + name);

                            if ((cs != null) && (cs.IsLocallyStored))
                            {
                                // Reverting to parent will remove the section from local config file
                                cs.RevertToParent();

                                mgr.CommitChanges();
                            }
                        }
                        catch
                        {
                            // Web config file may not exist for application.
                            // Catch because above Get call throws an error. 
                            // No need to do anything, just move on to the next file
                        }
                    }
                }
            }
        }

        /// <summary> 
        /// Removes the specified section from the SectionGroups section by name 
        /// </summary> 
        public static void RemoveSectionGroup(string name)
        {
            using (ServerManager mgr = new ServerManager())
            {
                Configuration appHostConfig = mgr.GetApplicationHostConfiguration();

                SectionGroup sectionGroups = appHostConfig.RootSectionGroup;

                if (FindSectionGroup(sectionGroups, "system.webServer") != null)
                {
                    // Get the section group
                    SectionGroup sectionGroup = FindSectionGroup(sectionGroups, "system.webServer");

                    if (FindSectionDefinition(sectionGroup, name) != null)
                    {
                        sectionGroup.Sections.Remove(name);
                    }
                }

                mgr.CommitChanges();
            }
        }

        #endregion

        #region Private methods

        /// <summary> 
        /// Helper method to find an element based on an attribute 
        /// </summary> 
        private static ConfigurationElement FindByAttribute(ConfigurationElementCollection collection, string attributeName, string value)
        {
            foreach (ConfigurationElement element in collection)
            {
                if (String.Equals((string)element.GetAttribute(attributeName).Value, value, StringComparison.OrdinalIgnoreCase))
                {
                    return element;
                }
            }

            return null;
        }

        /// <summary>
        /// Helper nethod to find a SectionGroup 
        /// </summary>
        private static SectionGroup FindSectionGroup(SectionGroup sectionGroups, string sectionGroupName)
        {
            foreach (SectionGroup sectionGroup in sectionGroups.SectionGroups)
            {
                if (sectionGroup.Name == sectionGroupName)
                {
                    return sectionGroup;
                }
            }

            return null;
        }

        /// <summary>
        /// Helper method to find a SectionDefinition
        /// </summary>
        private static SectionDefinition FindSectionDefinition(SectionGroup sectionGroup, string sectionName)
        {
            foreach (SectionDefinition sectionDef in sectionGroup.Sections)
            {
                if (sectionDef.Name == sectionName)
                {
                    return sectionDef;
                }
            }

            return null;
        }

        #endregion
    }
}
