//// ***********************************************
//// NAME           : Redirector.cs
//// AUTHOR 		: Steve Barker
//// DATE CREATED   : 07-Mar-2008
//// DESCRIPTION 	: Class to allow previous TD web pages to redirect to the new white 
////                  labelled sites, taking into account language.
//// ************************************************
////    Rev Devfactory Mar 07 2008 12:00:00   sbarker
////    Initial release

using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace OldBookmarkRedirector
{
    /// <summary>
    /// Class that provides functionality for existing TD web pages to redirect to new
    /// white labelled sites. Language is coded into a query string value, which is 
    /// picked up and handled by the new white labelled site.
    /// </summary>
    public static class Redirector
    {
        #region Public Static Methods

        /// <summary>
        /// Given an HttpContent, this method will relocate to the new white label site,
        /// preserving language and the particular page request.
        /// </summary>
        /// <param name="context"></param>
        public static void Process(HttpContext context)
        {
            //Get a lower case version of the string, without
            //the host etc...:
            Uri oldUri = context.Request.Url;
            string path = oldUri.LocalPath.ToLower();
            string[] pathSections = path.Split(new string[] { @"/" }, StringSplitOptions.RemoveEmptyEntries);
            Dictionary<string, string> newQueryStringSections = new Dictionary<string, string>();

            #region Handle language
            //If we get here, we must convert the bookmark. First we
            //chop out the language identifier...
            if (path.Contains("/en/"))
            {
                path = path.Replace("en/", "");
                newQueryStringSections.Add("CurrentLanguage", "English");
            }
            else if (path.Contains("/cy/"))
            {
                path = path.Replace("cy/", "");
                newQueryStringSections.Add("CurrentLanguage", "Welsh");
            }
            #endregion

            //Now we edit the link so we can transfer to the new 
            //style of hyperlink:

            #region Handle default page
            string homepath = "Web2/Home.aspx";

            // Check if url is for a default page
            if (path.ToLower().EndsWith("web/home.aspx"))
                path = path.Replace("web/home.aspx", homepath);

            else if (path.ToLower().EndsWith("web/templates/home.aspx"))
                path = path.Replace("web/templates/home.aspx", homepath);

            else if (path.ToLower().EndsWith("transportdirect/home.aspx"))
                path = path.Replace("transportdirect/home.aspx", homepath);

            else if (path.ToLower().EndsWith("directgov/home.aspx"))
                path = path.Replace("directgov/home.aspx", homepath);

            else if (path.ToLower().EndsWith("visitbritain/home.aspx"))
                path = path.Replace("visitbritain/home.aspx", homepath);

            else if (path.ToLower().EndsWith("bbc/home.aspx"))
                path = path.Replace("bbc/home.aspx", homepath);

            else if (path.ToLower().EndsWith("nationaltrust/home.aspx"))
                path = path.Replace("nationaltrust/home.aspx", homepath);
            #endregion

            //We replace TransportDirect/ with web2/
            path = path.Replace("transportdirect/", "web2/");

            
            path = path.Replace("directgov/", "web2/");
            path = path.Replace("visitbritain/", "web2/");
            path = path.Replace("bbc/", "web2/");
            path = path.Replace("nationaltrust/", "web2/");


            #region Handle non existent folder paths, e.g. web/templates
            //if user has come with a web/templates, then need to redirect the page to the correct subdirectory
            string subdirectory = GetPathSubDirectory(pathSections);

            //Also replace web/templates/ with web2/
            path = path.Replace("web/templates/", "web2/" + subdirectory);
            path = path.Replace("web2/finda/", "web2/" + subdirectory);

            #endregion

            #region Handle page no longer existing

            path = ReplacePageNotExists(path);

            #endregion

            //Build the new query string:
            StringBuilder newQueryString = new StringBuilder(oldUri.Query);

            if (newQueryStringSections.Count > 0)
            {
                //Now add new sections:
                foreach (string key in newQueryStringSections.Keys)
                {
                    if (newQueryString.Length == 0)
                    {
                        newQueryString.Append("?");
                    }
                    else
                    {
                        newQueryString.Append("&");
                    }

                    newQueryString.AppendFormat("{0}={1}", key, newQueryStringSections[key]);
                }
            }

            // Checking for the post data and sending post data to landing page in form of query string
            if (context.Request.RequestType.ToLower() == "post")
            {
                if (context.Request.Form.HasKeys())
                {
                    if (newQueryString.Length == 0)
                    {
                        newQueryString.Append("?");
                    }
                    else
                    {
                        newQueryString.Append("&");
                    }

                    newQueryString.Append(context.Request.Form.ToString());
                }
            }

            //Build all the information up into a query string:
            string newUri = string.Format("http://{0}{1}{2}", oldUri.Host, path, newQueryString.ToString());
                        
			context.Response.Status = "301 Moved Permanently"; 
			context.Response.AddHeader("Location", newUri);
        }

        #endregion

        #region Private methods
        /// <summary>
        /// Returns the subdirectory of a page
        /// </summary>
        /// <param name="pathsections"></param>
        /// <returns></returns>
        private static string GetPathSubDirectory(string[] pathsections)
        {
            string page = pathsections[pathsections.Length - 1];

            string subdirectory = string.Empty;

            if ((page.ToLower().EndsWith(".aspx")) && (page != "default.aspx") && (page != "home.aspx"))
            {
                switch (page.ToLower())
                {
                    //ContactUs
                    case "details.aspx":
                    case "feedbackpage.aspx":
                    case "feedbackinitialpage.aspx":
                        subdirectory = "ContactUs/";
                        break;

                    //Help
                    case "helptoolbar.aspx":
                        subdirectory = "Help/";
                        break;

                    //iFrames
                    case "iframefindaplace.aspx":
                    case "iframejourneyplanning.aspx":
                    case "journeylandingpage.aspx":
                    case "locationlandingpage.aspx":
                        subdirectory = "iFrames/";
                        break;

                    // Journey Planning (not all need to be done, only if they exist in a directory other than journeyplanning)
                    case "findcarInput.aspx":
                    case "findtrainInput.aspx":
                    case "findtrunkInput.aspx":
                        subdirectory = "JourneyPlanning/";
                        break;

                    //Live Travel
                    case "departureboards.aspx":
                    case "printabletravelnews.aspx":
                    case "tnlandingpage.aspx":
                    case "travelnews.aspx":
                        subdirectory = "LiveTravel/";
                        break;

                    //Maps
                    case "journeyplannerlocationmap.aspx":
                    case "printabletrafficmaps.aspx":
                    case "trafficmaps.aspx":
                        subdirectory = "Maps/";
                        break;

                    //SiteMap
                    case "sitemapdefault.aspx":
                        subdirectory = "SiteMap/";
                        break;

                    //TDOnTheMove
                    case "tdonthemove.aspx":
                        subdirectory = "TDOnTheMove/";
                        break;

                    //Tools
                    case "businesslinks.aspx":
                    case "toolbardownload.aspx":
                        subdirectory = "Tools/";
                        break;

                    //UserSupport
                    case "help.aspx":
                        subdirectory = "UserSupport/";
                        break;

                    //UserSurvey
                    case "usersurvey.aspx":
                        subdirectory = "UserSurvey/";
                        break;

                    //Viewer
                    case "feedbackviewer.aspx":
                    case "logviewer.aspx":
                    case "versionviewer.aspx":
                        subdirectory = "Viewer/";
                        break;

                    default:
                        subdirectory = "JourneyPlanning/";
                        break;
                }
            }

            return subdirectory;
        }

        /// <summary>
        /// Replaces any pages that no longer exist with the page which should be redirected to
        /// </summary>
        private static string ReplacePageNotExists(string path)
        {
            if (!string.IsNullOrEmpty(path))
            {
                if (path.Contains("feedbackinitialpage.aspx"))
                {
                    path = path.Replace("feedbackinitialpage.aspx", "feedbackpage.aspx");
                }
            }

            return path;
        }
        #endregion
    }
}
