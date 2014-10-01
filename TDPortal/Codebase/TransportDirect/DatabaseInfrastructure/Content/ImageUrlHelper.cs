//// ***********************************************
//// NAME           : ImageUrlHelper.cs
//// AUTHOR 		: Steve Barker
//// DATE CREATED   : 19-Feb-2008
//// DESCRIPTION 	: Allows ImageUrls to be edited for White Labelling purposes
//// ************************************************
////
////    $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/DatabaseInfrastructure/Content/ImageUrlHelper.cs-arc  $
//
//   Rev 1.2   May 20 2009 11:57:48   mmodi
//Added version history log
////
////    Rev Devfactory Feb 19 2008 14:00:00   sbarker
////    First release.
////
////    Rev Devfactory Feb 26 2008 sbarker
////    Fix to sort image urls starting with /white label/

using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace TransportDirect.Common.DatabaseInfrastructure.Content
{
    /// <summary>
    /// Helper class to alter ImageUrls for white labelling purposes
    /// </summary>
    public static class ImageUrlHelper
    {
        #region Public Static Methods
        
        /// <summary>
        /// Given an ImageUrl, it is converted to a form ready for white labelling
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetAlteredImageUrl(string imageUrl)
        {
            //Values generally come in with the format:
            //"/Web/images/gifs/skip_navigation.gif"
            //We need to strip the /Web/ bit off the front, and append 
            //"~/App_Themes/{ThemeName}/" to the front.
            //We may also get strings of the form:
            //"/Web2/App_Themes/TransportDirect/images/gifs/skip_navigation.gif"
            //In this case, we replace "/Web2/App_Themes/TransportDirect/ with
            //the replacement text above.
            //This sort of operation should be handled by Skin files under
            //the theme, although this edit approach will require far less
            //changes to the code.
            //Note that we convert null values to empty strings.

            string themeName;
            try
            {
                themeName = TD.ThemeInfrastructure.ThemeProvider.Instance.GetTheme().Name;
            }
            catch
            {
                themeName = TD.ThemeInfrastructure.ThemeProvider.Instance.GetDefaultTheme().Name;
            }

            //First get the replacement string:
            string urlReplacementText = string.Format("~/App_Themes/{0}/", themeName);

            //Now tidy the incoming text:
            imageUrl = (imageUrl ?? "").ToLower();

            //Check to see what the string starts with, and change the start
            //accordingly:
            if (imageUrl.StartsWith("/web/"))
            {
                imageUrl = imageUrl.Replace("/web/", urlReplacementText);
            }
            else if (imageUrl.StartsWith("/web2/"))
            {
                imageUrl = imageUrl.Replace("/web2/app_themes/transportdirect/", urlReplacementText);
            }

            return imageUrl;
        }

        #endregion
    }
}
