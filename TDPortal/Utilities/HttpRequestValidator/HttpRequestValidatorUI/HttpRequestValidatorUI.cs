// *********************************************** 
// NAME         : HttpRequestValidatorUI.cs
// AUTHOR       : Mitesh Modi
// DATE CREATED : 28/01/2011
// DESCRIPTION  : Class HttpRequestValidatorUI
// ************************************************ 
// $Log:   P:/TDPortal/archives/Utilities/HttpRequestValidator/HttpRequestValidatorUI/HttpRequestValidatorUI.cs-arc  $
//
//   Rev 1.0   Feb 03 2011 10:15:08   mmodi
//Initial revision.
//

using System;
using System.Windows.Forms;
using Microsoft.Web.Management.Client;
using Microsoft.Web.Management.Server;

namespace AO.HttpRequestValidatorUI
{
    internal class HttpRequestValidatorUI : Module
    {
        protected override void Initialize(IServiceProvider serviceProvider, ModuleInfo moduleInfo)
        {
            base.Initialize(serviceProvider, moduleInfo);

            string title = "HTTP Request Validator";
            string description = title;

            IControlPanel controlPanel = (IControlPanel)GetService(typeof(IControlPanel));
            ModulePageInfo modulePageInfo = new ModulePageInfo(this, typeof(HttpRequestValidatorUIPage), 
                title, description); 
            controlPanel.RegisterPage(modulePageInfo);
        }
    }
}
