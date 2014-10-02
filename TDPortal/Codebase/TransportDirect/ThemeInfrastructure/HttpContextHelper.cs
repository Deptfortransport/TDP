//// ***********************************************
//// NAME           : HttpContextHelper.cs
//// AUTHOR 		: Steve Barker
//// DATE CREATED   : 19-Feb-2008
//// DESCRIPTION 	: Provides a safe way for classes to obtain the current HttpContext. If it is not possible to get this, a suitable error is thrown.
//// ************************************************
////    Rev Devfactory Feb 19 2008 14:00:00   sbarker
////    First release.

using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace TD.ThemeInfrastructure
{
    /// <summary>
    /// Helper for HttpContext
    /// </summary>
    public static class HttpContextHelper
    {
        #region Public Static Methods
        
        /// <summary>
        /// Gets the current context. If it is not possible to get the context, 
        /// a HttpException exception is thrown.
        /// </summary>
        /// <returns></returns>
        public static HttpContext GetCurrent()
        {
            HttpContext context = HttpContext.Current;

            //Make sure the context isn't null:
            if (context == null)
            {
                throw new System.Web.HttpException("Cannot get the HttpContext for the current application domain");
            }

            return context;
        }

        #endregion
    }
}
