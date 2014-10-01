// *********************************************** 
// NAME             : PageTransferCache.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 18 Mar 2011
// DESCRIPTION  	: PageTransferCache class is used to associated PageIds with their
// equilavent PageTransferDetails from the Sitemap
// ************************************************
// 

using System;
using System.Collections.Generic;
using System.Web;

using TDP.Common;
using TDP.Common.EventLogging;

using Logger = System.Diagnostics.Trace;
using TDP.Common.PropertyManager;

namespace TDP.UserPortal.ScreenFlow
{
    /// <summary>
    /// PageTransferCache class is used to associated PageIds with their
    /// equilavent PageTransferDetails from the Sitemap
    /// </summary>
    public class PageTransferCache : IPageTransferCache
    {
        #region Private members

        // Holds all the PageTransferDetails
        private Dictionary<PageId, PageTransferDetail> pageTransferDetails;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        public PageTransferCache()
        {
            try
            {
                // Setup the page transfer details
                pageTransferDetails = new Dictionary<PageId, PageTransferDetail>();

                PopulatePageTransferDetails();
            }
            catch (Exception e)
            {
                string message =
                    String.Format(Messages.PageTransferDataCacheConstructor);

                OperationalEvent operationalEvent = new OperationalEvent(TDPEventCategory.Business, TDPTraceLevel.Error, message);
                Logger.Write(operationalEvent);

                // Handle for unit test
                if (!(e is NullReferenceException))
                    throw new TDPException(message, e, true, TDPExceptionIdentifier.SFMConstructorFailed);
            }
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Reads all Sitemap nodes and adds to array of PageTransferDetails
        /// </summary>
        private void PopulatePageTransferDetails()
        {
            // Parse the root node
            ParseSiteMapNode(SiteMap.RootNode);

            // Get all the nodes in the site map
            SiteMapNodeCollection siteMapNodes = SiteMap.RootNode.GetAllNodes();

            foreach (SiteMapNode node in siteMapNodes)
            {
                ParseSiteMapNode(node);
            }
        }

        /// <summary>
        /// Parses a SiteMapNode and adds it to the PageTransferDetail cache
        /// </summary>
        private void ParseSiteMapNode(SiteMapNode node)
        {
            // Custom attribute to read from sitemap nodes
            string sitemapPageIdAttribute = "pageId";

            PageTransferDetail pageTransferDetail;

            // Not all nodes have a pageId attribute, these can be ignored
            if (node[sitemapPageIdAttribute] != null)
            {
                string pageId = node[sitemapPageIdAttribute];

                //convert the pageId strings to the equivalent emuerated type
                PageId pageIdEnum = PageId.Empty;

                try
                {
                    pageIdEnum = (PageId)Enum.Parse(typeof(PageId), pageId);
                }
                catch (ArgumentNullException ane)
                {
                    // enumType or value is a null reference
                    string message = String.Format(Messages.EnumConversionFailed, "pageId:" + pageId);

                    OperationalEvent operationalEvent = new OperationalEvent(TDPEventCategory.Business, TDPTraceLevel.Error, message);
                    Logger.Write(operationalEvent);

                    throw new TDPException(message, ane, true, TDPExceptionIdentifier.SFMScreenFlowPageIdError);
                }
                catch (ArgumentException ae)
                {
                    string message = String.Format(Messages.EnumConversionFailed, "pageId:" + pageId);

                    OperationalEvent operationalEvent = new OperationalEvent(TDPEventCategory.Business, TDPTraceLevel.Error, message);
                    Logger.Write(operationalEvent);

                    throw new TDPException(message, ae, true, TDPExceptionIdentifier.SFMScreenFlowPageIdError);
                }

                string url = node.Url;

                // Update url hostname if necessary
                if (url.Contains("localhost"))
                {
                    string host = Properties.Current[string.Format("ScreenFlow.PageTransfer.{0}.Host", pageIdEnum.ToString())];

                    if (!string.IsNullOrEmpty(host))
                    {
                        url = url.Replace("localhost", host);
                    }
                }
                                
                // Create and add the page transfer detail
                pageTransferDetail = new PageTransferDetail(pageIdEnum, url);

                pageTransferDetails.Add(pageIdEnum, pageTransferDetail);
            }
        }
                
        #endregion

        #region Public methods

        /// <summary>
        /// Returns the PageTransferDetail object associcated with the PageId.
        /// </summary>
        /// <param name="pageId">Id of the page to get the details for.</param>
        /// <returns>PageTransferDetail object associated with the PageId.</returns>
        public PageTransferDetail GetPageTransferDetails(PageId pageId)
        {
            // return the object at the index specified by pageId
            return pageTransferDetails[pageId];
        }

        #endregion
    }
}
