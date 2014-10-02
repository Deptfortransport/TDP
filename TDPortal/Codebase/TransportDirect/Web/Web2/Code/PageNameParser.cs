//******************************************************************************
//NAME			: PageNameParser.cs
//AUTHOR		: Steve Barker
//DATE CREATED	: 25/01/2008
//DESCRIPTION	: Helper class used to convert the page name given by Page.ToString() 
//                to a name usable by the content database provider.
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

namespace TransportDirect.UserPortal.Web.Code
{
    /// <summary>
    /// Helper class used to convert the page name given by Page.ToString() 
    /// to a name usable by the content database provider.
    /// </summary>
    public static class PageNameParser
    {
        #region Public Static Methods

        /// <summary>
        /// Converts the Page.ToString() call to a page name usable as a group name
        /// for the content database provider.
        /// </summary>
        /// <param name="pageToString"></param>
        /// <returns></returns>
        public static string GetPageNameFromPageToString(Page page)
        {
            //The page name is in the form ASP.home_aspx. So, we string the first 4
            //and the last 5 characters to make the page name.
            string name = page.ToString();
            return name.Substring(4, name.Length - 9);
        }

        #endregion
    }
}
