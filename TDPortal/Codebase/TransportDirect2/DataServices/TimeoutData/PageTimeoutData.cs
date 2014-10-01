// *********************************************** 
// NAME             : PageTimeoutData.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 22 Aug 2013
// DESCRIPTION  	: PageTimeoutData class to load data used in session timeout processing
// ************************************************
// 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using TDP.Common.PropertyManager;
using System.IO;
using Logger = System.Diagnostics.Trace;
using TDP.Common.Extenders;
using TDP.Common.EventLogging;
using System.Xml.Schema;

namespace TDP.Common.DataServices.TimeoutData
{
    /// <summary>
    /// PageTimeoutData class to load data used in session timeout processing
    /// </summary>
    public class PageTimeoutData
    {
        #region Private members

        private List<PageTimeout> pageTimeoutDataCache = new List<PageTimeout>();

        // Variables that are set if and only if xml valiation
        // against it's schema fails.
        private bool xmlIsValid = true;
        private string xmlInvalidMessage = string.Empty;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public PageTimeoutData()
        {
            LoadData();
        }

        #endregion

        #region Private methods
       
        /// <summary>
        /// Loads the data and performs pre processing
        /// </summary>
        private void LoadData()
        {
            Logger.Write(new OperationalEvent(TDPEventCategory.Business, TDPTraceLevel.Info, "Loading PageTimeout data started"));

            List<PageTimeout> tmpPageTimeoutDataCache = new List<PageTimeout>();

            string xmlPath = Properties.Current["ScreenFlow.PageTimeout.Path"];
            string xsdPath = Properties.Current["ScreenFlow.PageTimeout.Xsd"];

            string rootNode = Properties.Current["ScreenFlow.PageTimeout.Xml.Node.Root"];
            string pageNode = Properties.Current["ScreenFlow.PageTimeout.Xml.Node.Page"];
            string pageIdAttribute = Properties.Current["ScreenFlow.PageTimeout.Xml.Attribute.PageId"];
            string controlIdAttribute = Properties.Current["ScreenFlow.PageTimeout.Xml.Attribute.ControlId"];
            string showTimeoutFlagAttribute = Properties.Current["ScreenFlow.PageTimeout.Xml.Attribute.ShowTimeout"];
            string allowTimeoutEventFlagAttribute = "AllowTimeoutEvent";
                //Properties.Current["ScreenFlow.PageTimeout.Xml.Attribute.AllowTimeoutEvent"];
                        
            #region Validate xml

            // Validate file exist
            FileInfo fileInfo = new FileInfo(xmlPath);

            if (!fileInfo.Exists)
            {
                Logger.Write(new OperationalEvent(TDPEventCategory.Business, TDPTraceLevel.Warning,
                    string.Format("Loading PageTimeout data - file does not exist[{0}]. No custom timeout handling setup", xmlPath)));

                return;
            }
            
            // Validate xml
            using (XmlTextReader reader = new XmlTextReader(xsdPath))
            {
                XmlSchema schema = XmlSchema.Read(reader, new ValidationEventHandler(ValidationHandler));
                XmlReaderSettings settings = new XmlReaderSettings();
                settings.Schemas.Add(schema);
                settings.ValidationType = ValidationType.Schema;
                settings.ValidationEventHandler += new ValidationEventHandler(ValidationHandler);

                using (XmlReader vr = XmlReader.Create(xmlPath, settings))
                {
                    while (vr.Read())
                    {
                    }
                }
            }

            if (!xmlIsValid)
            {
                Logger.Write(new OperationalEvent(TDPEventCategory.Business, TDPTraceLevel.Warning,
                    string.Format("Loading PageTimeout data - xml data failed schema validation, schema[{0}], message[{1}]. No custom timeout handling setup", xsdPath, xmlInvalidMessage)));

                return;
            }

            #endregion

            XmlDataDocument pageTimeoutXml = new XmlDataDocument();
            pageTimeoutXml.Load(xmlPath);

            try
            {
                #region Load data

                // Select the root node
                XmlNode root = pageTimeoutXml.SelectSingleNode("//" + rootNode);

                // Select the list of pages nodes from the root node
                XmlNodeList pageNodes = root.SelectNodes(pageNode);

                // Read the attributes for each node in the list and populate 
                PageTimeout pageTimeout = null;

                foreach (XmlNode node in pageNodes)
                {
                    string pageId = node.Attributes[pageIdAttribute].InnerText;
                    string controlId = node.Attributes[controlIdAttribute].InnerText;
                    string showTimeout = string.Empty;
                    if (node.Attributes[showTimeoutFlagAttribute] != null)
                    {
                        showTimeout = node.Attributes[showTimeoutFlagAttribute].InnerText;
                    }

                    string allowTimeoutEvent = string.Empty;
                    if (node.Attributes[allowTimeoutEventFlagAttribute] != null)
                    {
                        allowTimeoutEvent = node.Attributes[allowTimeoutEventFlagAttribute].InnerText;
                    }

                    // Convert the strings to their enum types
                    PageId pageIdEnum = pageId.Parse(PageId.Empty);
                    bool showTimeoutBool = showTimeout.Parse(true);
                    bool allowTimeoutEventBool = allowTimeoutEvent.Parse(false);

                    if (pageIdEnum == PageId.Empty)
                    {
                        Logger.Write(new OperationalEvent(TDPEventCategory.Business, TDPTraceLevel.Verbose,
                            string.Format("Loading PageTimeout data - Unrecognised PageId[{0}], ignoring page.", pageId)));
                        
                        continue;
                    }

                    if (!string.IsNullOrEmpty(controlId))
                    {
                        // Retrieve existing PageTimeout object if exists
                        pageTimeout = tmpPageTimeoutDataCache.Find(
                            delegate(PageTimeout pt) { return pt.PageId == pageIdEnum; });
                        
                        // Create a new PageTimeout object
                        if (pageTimeout == null)
                        {
                            pageTimeout = new PageTimeout();
                            pageTimeout.PageId = pageIdEnum;
                            tmpPageTimeoutDataCache.Add(pageTimeout);
                        }

                        // Update with the control id to either show or hide timeout message
                        if (showTimeoutBool)
                            pageTimeout.ShowTimeoutControlIds.Add(controlId.ToLower().Trim());
                        else
                            pageTimeout.HideTimeoutControlIds.Add(controlId.ToLower().Trim());

                        if (allowTimeoutEventBool)
                            pageTimeout.AllowTimeoutEventControlIds.Add(controlId.ToLower().Trim());
                    }
                }

                #endregion

                #region Update the cahce

                // Replace the cache with the new data
                pageTimeoutDataCache = tmpPageTimeoutDataCache;

                #endregion

                // Record the fact that the data was loaded.
                Logger.Write(new OperationalEvent(TDPEventCategory.Business, TDPTraceLevel.Verbose,
                    string.Format("PageTimeout data loaded into cache: Pages timeout data count[{0}]", pageTimeoutDataCache.Count)));

                Logger.Write(new OperationalEvent(TDPEventCategory.Business, TDPTraceLevel.Verbose, "Loading PageTimeout data completed"));

            }
            catch (Exception ex)
            {
                Logger.Write(new OperationalEvent(TDPEventCategory.Business, TDPTraceLevel.Error,
                    "An error occurred whilst attempting to load the PageTimeout data.", ex));
            }
        }

        /// <summary>
        /// Handler to validate the Xml files
        /// </summary>
        private void ValidationHandler(object sender, ValidationEventArgs args)
        {
            xmlInvalidMessage = args.Message;
            xmlIsValid = false;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Returns false if the session timeout message should not be displayed for the control id.
        /// Default is true
        /// </summary>
        public bool ShowTimeoutMessage(PageId pageId, string controlId)
        {
            // Default to show timeout
            bool showTimeout = true;

            PageTimeout pageTimeout = pageTimeoutDataCache.Find(
                            delegate(PageTimeout pt) { return pt.PageId == pageId; });

            if (pageTimeout != null)
            {
                if (!string.IsNullOrEmpty(controlId))
                {
                    if (pageTimeout.HideTimeoutControlIds.Contains(controlId.ToLower()))
                    {
                        showTimeout = false;
                    }
                    else if (pageTimeout.ShowTimeoutControlIds.Contains(controlId.ToLower()))
                    {
                        showTimeout = true;
                    }
                }
            }

            return showTimeout;
        }

        /// <summary>
        /// Returns true if the timeout event for control id is allowed
        /// Default is false
        /// </summary>
        public bool AllowTimeoutEvent(PageId pageId, string controlId)
        {
            // Default to allow 
            bool allow = false;

            PageTimeout pageTimeout = pageTimeoutDataCache.Find(
                            delegate(PageTimeout pt) { return pt.PageId == pageId; });

            if (pageTimeout != null)
            {
                if (!string.IsNullOrEmpty(controlId))
                {
                    if (pageTimeout.AllowTimeoutEventControlIds.Contains(controlId.ToLower()))
                    {
                        allow = true;
                    }
                }
            }

            return allow;
        }        

        #endregion
    }
}