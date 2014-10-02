//******************************************************************************
//NAME			: PageControlPropertyPopulator.cs
//AUTHOR		: Steve Barker
//DATE CREATED	: 25/01/2008
//DESCRIPTION	: Used to populate controls on pages with information from the content
//                database
//******************************************************************************
using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Drawing;
using System.Collections.Generic;
using System.Text;
using TransportDirect.Common.DatabaseInfrastructure.Content;
using TransportDirect.Common.ServiceDiscovery;

using LatestNews = TransportDirect.UserPortal.LatestNewsService;


//
//   Rev DevFactory Feb 08 09:44:05 psheldrake
//   added support for panels to the content renderer
//
namespace TransportDirect.UserPortal.Web.Code
{
    /// <summary>
    /// Used to populate controls on pages with information from the content
    /// database
    /// </summary>
    public static class PageControlPropertyPopulator
    {
        #region Public Static Methods

        /// <summary>
        /// Populate the controls on the page specified
        /// </summary>
        /// <param name="page"></param>
        public static void Populate(Page page)
        {
            //Here we set the values of control properties using information
            //from the content database:
            //Get the page name, which is the group name for the content database:
            string pageName = PageNameParser.GetPageNameFromPageToString(page);

            //Now get the content for the page/group:
            ControlPropertyCollection properties = ContentProvider.Instance[pageName].GetControlProperties();

            //Apply the content to the controls:
            foreach (Control control in page.Controls)
            {
                SetControlProperties(control, properties);
            }
        }

        #endregion

        #region Private Static Methods

        private static void SetControlProperties(Control control, ControlPropertyCollection properties)
        {
            //Set the properties of all sub-controls. 
            //Note this uses recursive programming, hence checking to
            //see if the controls is null before doing any thing:
            if (control.Controls != null)
            {
                foreach (Control subControl in control.Controls)
                {
                    SetControlProperties(subControl, properties);
                }
            }

            //Thc control must have an name (ID). If it doesn't it's an auto-
            //generated control (for instance), so we ignore it and move to
            //the next control.
            if (control.ID == null)
            {
                return;
            }

            //Now we're ready to set the properties of the control:
            //Get property names available:
            string[] propertyNames = properties.GetPropertyNames(control.ID);

            //If the current control has no properties available in the content,
            //then we ignore and move to the next;
            if (propertyNames.Length == 0)
            {
                return;
            }

            try
            {
                //Now we deduce the type of control being handled:
                switch (control.GetType().Name)
                {
                    //Include controls in this section which we know how to handle:
                    case "TextBox":
                        SetTextBoxProperties(control, propertyNames, properties);
                        break;
                    case "Panel":
                        SetPanelProperties(control, propertyNames, properties);
                        break;
                    default:
                        //Any non-handled and non-ignored controls cause the following
                        //exception to be thrown:
                        throw new Exception();
                }
            }
            catch(Exception e)
            {
                //We rebrand the error here:
                StringBuilder message = new StringBuilder();

                message.Append("An error occurred in the method SetControlProperties() ");
                message.Append("of the PageControlPropertyPopulator class. It is likely ");
                message.Append("to have been a casting problem when converting a string ");
                message.Append("to a strongly typed control property. See inner ");
                message.Append("exception for details.");

                //Note that we should never see this error during execution of the site in a
                //live environment, unless someone has made a bad mistake with the contents
                //of the Content database:
                throw new Exception(message.ToString(), e);
            }
        }

        private static void SetPanelProperties(Control control, string[] propertyNames, ControlPropertyCollection properties)
        {
            //First cast the control to the correct type:
            Panel panel = control as Panel;
            
            //Loop through available property names:
            foreach (string propertyName in propertyNames)
            {
                //Get the value for the property:
                string value = properties.GetPropertyValue(control.ID, propertyName);

                //Switch to find out how to handle the property name:
                switch (propertyName)
                {
                    case "Visible":
                        panel.Visible = bool.Parse(value);
                        break;
                    default:
                        if (panel.ID == "TDInformationHtmlPlaceholderDefinition")
                        {
                            #region Get Latest news
                            try
                            {
                                LatestNews.LatestNewsProvider refData
                                    = (LatestNews.LatestNewsProvider)TDServiceDiscovery.Current[ServiceDiscoveryKey.LatestNewsFactory];

                                value = refData.GetLatestNews(CurrentLanguage.Culture);
                            }
                            catch
                            {
                                value = string.Empty;
                            }
                            #endregion
                        }
                        if (panel.ID == "SpecialNoticeBoardHtmlPlaceHolder")
                        {
                            #region Get Special notice
                            try
                            {
                                LatestNews.LatestNewsProvider refData
                                    = (LatestNews.LatestNewsProvider)TDServiceDiscovery.Current[ServiceDiscoveryKey.LatestNewsFactory];

                                value = refData.GetSpecialNotice(CurrentLanguage.Culture);
                            }
                            catch
                            {
                                value = string.Empty;
                            }
                            #endregion
                        }
                        
                        if (!panel.HasControls())
                        {
                           panel.Controls.Add(new LiteralControl(value));
                        }
                        else
                            (panel.Controls[0] as LiteralControl).Text = value;
                        break;

                }
            }
        }

        
        private static void SetTextBoxProperties(Control control, string[] propertyNames, ControlPropertyCollection properties)
        {
            //First cast the control to the correct type:
            TextBox textBox = control as TextBox;

            //Loop through available property names:
            foreach (string propertyName in propertyNames)
            {
                //Get the value for the property:
                string value = properties.GetPropertyValue(control.ID, propertyName);

                //Switch to find out how to handle the property name:
                switch (propertyName)
                {
                    case "Text":
                        textBox.Text = value;
                        break;
                    case "ForeColor":
                        textBox.ForeColor = Color.FromName(value);
                        break;
                    case "Visible":
                        textBox.Visible = bool.Parse(value);
                        break;
                    default:
                        //If the property name isn't handled, we throw an exception:
                        throw new Exception("Bad!");
                }
            }
        }

        #endregion
    }
}