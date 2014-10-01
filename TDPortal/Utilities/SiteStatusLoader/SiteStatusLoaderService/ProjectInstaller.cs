// *********************************************** 
// NAME                 : ProjectInstaller.cs
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 01/04/2009
// DESCRIPTION			: Service installer
// ************************************************ 
// $Log:   P:/TDPortal/archives/Utilities/SiteStatusLoader/SiteStatusLoaderService/ProjectInstaller.cs-arc  $
//
//   Rev 1.0   Apr 01 2009 13:37:14   mmodi
//Initial revision.
//Resolution for 5273: Site Status Loader

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;

namespace AO.SiteStatusLoaderService
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : Installer
    {
        public ProjectInstaller()
        {
            InitializeComponent();
        }
    }
}