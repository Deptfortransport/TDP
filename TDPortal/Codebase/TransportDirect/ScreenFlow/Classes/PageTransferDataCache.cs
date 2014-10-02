// *********************************************** 
// NAME                 : PageTransferDataCache.cs 
// AUTHOR               : Kenny Cheung
// DATE CREATED         : 16/07/2003 
// DESCRIPTION  : Implements the
// IPageTransferDataCache interface.  The cache
// is used to associated PageIds with their
// equilavent PageTransferDetails, as well as
// associating TransitionEvents with Pageids.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ScreenFlow/Classes/PageTransferDataCache.cs-arc  $ 
//
//   Rev 1.2   May 06 2008 16:07:32   mmodi
//Added xsd schema validation 
//Resolution for 4888: Del 10.1 - Improve Session Timeout Management
//
//   Rev 1.1   May 01 2008 17:07:32   mmodi
//Updated for PageGrouping.xml
//Resolution for 4888: Del 10.1 - Improve Session Timeout Management
//
//   Rev 1.0   Nov 08 2007 12:47:48   mturner
//Initial revision.
//
//   Rev 1.19   May 23 2005 10:03:00   rscott
//Updated for NUnit Tests
//
//   Rev 1.18   Jul 15 2004 09:50:38   jgeorge
//Removal of JourneyParametersType from screenflow
//
//   Rev 1.17   Jul 09 2004 13:34:48   jgeorge
//IR1143
//
//   Rev 1.16   Jun 29 2004 15:31:12   esevern
//moved conversion of journey parameter type from xml attribute to page transfer details initialisation
//
//   Rev 1.15   Jun 29 2004 13:45:18   esevern
//added JourneyParameterType attribute required for additional safe page redirection processing
//
//   Rev 1.14   May 20 2004 14:45:50   rgreenwood
//Back Button Enhancement:
//Code now handles the BookmarkValidJourneyRedirect attribute, and will return the associated PageID
//
//   Rev 1.13   Apr 23 2004 14:36:42   acaunt
//CSL Compliant attribute removed so that the Assembly can be marked as non CLS compliant. (All projects are to have an identical AssemblyInfo.cs file).
//
//   Rev 1.12   Oct 03 2003 13:38:48   PNorell
//Updated the new exception identifier.
//
//   Rev 1.11   Sep 18 2003 14:27:02   kcheung
//Added Server.MapPath
//
//   Rev 1.10   Sep 16 2003 14:29:52   jcotton
//Removed redundant, commented out code.
//
//   Rev 1.9   Sep 05 2003 15:17:56   kcheung
//Corrected properties spelling error
//
//   Rev 1.8   Aug 18 2003 10:32:18   passuied
//update after design changes
//
//   Rev 1.7   Aug 15 2003 14:36:50   passuied
//Update after design change
//
//   Rev 1.6   Aug 07 2003 13:53:50   kcheung
//Set CLSComplaint to true
//
//   Rev 1.5   Jul 30 2003 18:55:00   geaton
//Reverted back to old OperationalEvent constructor.
//
//   Rev 1.4   Jul 29 2003 18:33:22   geaton
//Swapped OperationalEvent parameter order after change in OperationalEvent constructor.
//
//   Rev 1.3   Jul 23 2003 14:29:56   kcheung
//Removed the catch(exception) since I'm specifically catching the IndexOutofBounds exception in 2 of the validation methods.
//
//   Rev 1.2   Jul 23 2003 13:07:02   kcheung
//Updated after code review comments
//
//   Rev 1.1   Jul 23 2003 12:27:58   kcheung
//Updated after code review comments

using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Collections;
using System.Globalization;
using TransportDirect.Common;
using TransportDirect.Common.PropertyService;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.Logging;
using Logger = System.Diagnostics.Trace;
using System.Web;
using System.Reflection;

namespace TransportDirect.UserPortal.ScreenFlow
{
	/// <summary>
	/// The cache is used to associated PageIds with their equivalent
	/// PageTransferDetails, as well as associating TransitionEvents with Pageids.
	/// </summary>
	public class PageTransferDataCache : IPageTransferDataCache
    {
        #region Private variables
        // Strings used to access the Xml. Their values will actually
		// be loaded from the Properties Service.
		private string pageTransferRootNode = String.Empty;
		private string pageTransferPageNode = String.Empty;
		private string pageTransferPageIdAttribute = String.Empty;
		private string pageTransferPageUrlAttribute = String.Empty;
		private string pageTransferBookmarkRedirectAttribute = String.Empty;
		private string pageTransferSpecificStateClassAttribute = String.Empty;

		private string pageTransferBookmarkValidJourneyRedirectAttribute = String.Empty;

		private string pageTransferLogonRequiredAttribute = String.Empty;
		private string pageTransferLogonRedirectAttribute = String.Empty;

		private string pageTransitionEventRootNode = String.Empty;
		private string pageTransitionEventPageNode = String.Empty;
		private string pageTransitionEventTransitionEventAttribute = String.Empty;
		private string pageTransitionEventPageIdAttribute = String.Empty;

        private string pageGroupingRootNode = String.Empty;
        private string pageGroupingPageNode = String.Empty;
        private string pageGroupingPageIdAttribute = String.Empty;
        private string pageGroupingGroupAttribute = String.Empty;

		// Path of the xml files.  Their values will be loaded from
		// the Properties Service.
		private string pageTransferDetailsPath = String.Empty;
		private string pageTransitionEventPath = String.Empty;
        private string pageGroupingPath = String.Empty;

        // Path of the xsd files.  Their values will be loaded from
        // the Properties Service.
        private string pageTransferDetailsXsd = String.Empty;
        private string pageTransitionEventXsd = String.Empty;
        private string pageGroupingXsd = String.Empty;

		// Holds all the PageTransferDetails.
		private PageTransferDetails[] pageTransferDetailsArray;
		
		// Holds all the PageIds to allow TransitionEvent lookup.
		private PageId[] pageIdArray;

		// Used to hold all the transition events read from the 
		// Xml so that validation can be carried out.
		private TransitionEvent[] transitionEventArray;

        // Used to hold the Group details pages read from the xml
        private PageGroupDetails[] pageGroupDetailsArray;

		// XmlDataDocuments created from the paths.  These Xml files
		// contain all the data to build the cache with.
		private XmlDataDocument pageTransferDetailsXml;
		private XmlDataDocument pageTransitionEventXml;
        private XmlDataDocument pageGroupingXml;
		
		// Variables that are set if and only if xml valiation
		// against it's schema fails.
		private bool xmlIsValid = true;
		private string xmlInvalidMessage = String.Empty;

        #endregion

        // ------------------------------------------------

        #region Constructor
        /// <summary>
		/// Constructor.
		/// </summary>
		public PageTransferDataCache()
		{
			try
			{
				// Load the properties 
				LoadProperties();

				// Used for holding error messages.
				ArrayList errors = new ArrayList();

				// Validate the Xml files.
				bool validXml = ValidateXmlFiles(errors);

				if(validXml)
				{
					// create the xml data documents
					pageTransferDetailsXml = new XmlDataDocument();
					pageTransferDetailsXml.Load(pageTransferDetailsPath);
					pageTransitionEventXml = new XmlDataDocument();
					pageTransitionEventXml.Load(pageTransitionEventPath);
                    pageGroupingXml = new XmlDataDocument();
                    pageGroupingXml.Load(pageGroupingPath);

					// initialise the arrays
					pageTransferDetailsArray = new PageTransferDetails
						[NumberOfPageTransferDetails()];
					pageIdArray = new PageId[NumberOfPageTransitionEvents()];
					transitionEventArray = new TransitionEvent
						[NumberOfPageTransitionEvents()];
                    pageGroupDetailsArray = new PageGroupDetails[NumberOfPageGroupings()];

					try
					{
						PopulatePageTransferDetailsArray();
					}
					catch(ScreenFlowException sfe)
					{
						// propagate the exception
						throw sfe;
					}

					try
					{
						PopulatePageIdArray();
					}
					catch(ScreenFlowException sfe)
					{
						// propagate the exception
						throw sfe;
					}

					// Both arrays have now been populated.
					// Validate both arrays to ensure that all data is correct
					try
					{
						ValidateTables(errors);
					}
					catch(ScreenFlowException sfe)
					{
						// propagate the exception
						throw sfe;
					}

                    try
                    {
                        PopulatePageGroupingArray();
                    }
                    catch (ScreenFlowException sfe)
                    {
                        // propagate the exception
                        throw sfe;
                    }
				}
				else
				{
					string message =
						String.Format
						(Messages.PageTransferDataCacheInvalidXml, errors[0]);

					OperationalEvent operationalEvent = new OperationalEvent
						(TDEventCategory.Business, TDTraceLevel.Error, message);
					Logger.Write(operationalEvent);

					throw new ScreenFlowException(message, true, TDExceptionIdentifier.SFMInvalidXml);
				}
			}
			catch(ScreenFlowException sfe)
			{
				// Caught an exception throw by constructor code.
				// Propogate the exception.
				throw sfe;
			}
			catch(Exception e)
			{
				string message =
					String.Format(Messages.PageTransferDataCacheConstructor);

				OperationalEvent operationalEvent = new OperationalEvent
					(TDEventCategory.Business, TDTraceLevel.Error, message);
				Logger.Write(operationalEvent);

				throw new ScreenFlowException(message, e, true, TDExceptionIdentifier.SFMConstructorFailed);
			}

		} // PageTransferDataCache

        #endregion

        // ------------------------------------------------

        #region Public methods
        /// <summary>
		/// Returns the PageTransferDetails object associcated with the PageId.
		/// </summary>
		/// <param name="pageId">Id of the page to get the details for.</param>
		/// <returns>PageTransferDetails object associated with the PageId.</returns>
		public PageTransferDetails GetPageTransferDetails(PageId pageId)
		{
			// return the object at the index specified by pageId
			int index = Convert.ToInt32
				(pageId, CultureInfo.InvariantCulture);
			return pageTransferDetailsArray[index];
		}

		// ------------------------------------------------

		/// <summary>
		/// Returns the PageId associated with the given Transition Event.
		/// </summary>
		/// <param name="pageTransitionEvent">Transition Event to get the PageId for.</param>
		/// <returns>Id of the page associated with the transition event.</returns>
		public PageId GetPageEvent(TransitionEvent pageTransitionEvent)
		{
			// return the pageId at the index specified by pageTransitionEvent
			int index = Convert.ToInt32
				(pageTransitionEvent, CultureInfo.InvariantCulture);
			return pageIdArray[index];
		}

		// ------------------------------------------------

        /// <summary>
        /// Returns a PageGroupDetails array for the selected PageGroup
        /// </summary>
        /// <param name="pageGroup"></param>
        /// <returns></returns>
        public PageGroupDetails[] GetPageGroupDetails(PageGroup pageGroup)
        {
            ArrayList details = new ArrayList();
            
            // create an array of PageGroupDetails asked for
            foreach (PageGroupDetails detail in pageGroupDetailsArray)
            {
                if (detail.PageGroup == pageGroup)
                    details.Add(detail);
            }

            return (PageGroupDetails[])details.ToArray(typeof(PageGroupDetails));
        }

        // ------------------------------------------------

        /// <summary>
        /// Returns all PageId's for the selected PageGroup
        /// </summary>
        public PageId[] GetPageIdsForGroup(PageGroup pageGroup)
        {
            ArrayList pageIds = new ArrayList();

            // create an array of PageGroupDetails asked for
            foreach (PageGroupDetails detail in pageGroupDetailsArray)
            {
                if (detail.PageGroup == pageGroup)
                    pageIds.Add(detail.PageId);
            }

            return (PageId[])pageIds.ToArray(typeof(PageId));
        }

        #endregion

        // ------------------------------------------------

        #region Load Properties
        /// <summary>
		/// Load all the properties from the Property Service.
		/// </summary>
		private void LoadProperties()
		{
			try
			{
				IPropertyProvider current = Properties.Current;

				// Xml Filenames and Xsds
				if (HttpContext.Current != null)
				{
					pageTransferDetailsPath = HttpContext.Current.Server.MapPath
						(current[Keys.PageTransferDetailsFilePath]);
					pageTransitionEventPath = HttpContext.Current.Server.MapPath
						(current[Keys.PageTransitionEventFilePath]);
                    pageGroupingPath = HttpContext.Current.Server.MapPath
                        (current[Keys.PageGroupingFilePath]);

                    pageTransferDetailsXsd = HttpContext.Current.Server.MapPath
                        (current[Keys.PageTransferDetailsFileXsd]);
                    pageTransitionEventXsd = HttpContext.Current.Server.MapPath
                        (current[Keys.PageTransitionEventFileXsd]);
                    pageGroupingXsd = HttpContext.Current.Server.MapPath
                        (current[Keys.PageGroupingFileXsd]);
				}
				else
				{
					pageTransferDetailsPath = current[Keys.PageTransferDetailsFilePath];
					pageTransitionEventPath = current[Keys.PageTransitionEventFilePath];
                    pageGroupingPath = current[Keys.PageGroupingFilePath];

                    pageTransferDetailsXsd = current[Keys.PageTransferDetailsFileXsd];
                    pageTransitionEventXsd = current[Keys.PageTransitionEventFileXsd];
                    pageGroupingXsd = current[Keys.PageGroupingFileXsd];
				}

				// Xml Node and Attribute Names
				pageTransferRootNode = current[Keys.PageTransferDetailsNodeRoot];
				pageTransferPageNode = current[Keys.PageTransferDetailsNodePage];
				pageTransferPageIdAttribute =
					current[Keys.PageTransferDetailsAttributePageId];
				pageTransferPageUrlAttribute =
					current[Keys.PageTransferDetailsAttributePageUrl];
				pageTransferBookmarkRedirectAttribute =
					current[Keys.PageTransferDetailsAttributeBookmarkRedirect];
				pageTransferSpecificStateClassAttribute =
					current[Keys.PageTransferDetailsAttributeSpecificStateClass];
				pageTransferBookmarkValidJourneyRedirectAttribute = 
					current[Keys.PageTransferDetailsAttributeBookmarkValidJourneyRedirect];

				pageTransitionEventRootNode = current[Keys.PageTransitionEventNodeRoot];
				pageTransitionEventPageNode = current[Keys.PageTransitionEventNodePage];
				pageTransitionEventPageIdAttribute =
					current[Keys.PageTransitionEventAttributePageId];
				pageTransitionEventTransitionEventAttribute =
					current[Keys.PageTransitionEventAttributeTransitionEvent];

                pageGroupingRootNode = current[Keys.PageGroupingNodeRoot];
                pageGroupingPageNode = current[Keys.PageGroupingNodePage];
                pageGroupingPageIdAttribute =
                    current[Keys.PageGroupingAttributePageId];
                pageGroupingGroupAttribute =
                    current[Keys.PageGroupingAttributeGroup];

			}
			catch(Exception e)
			{
				string message = Messages.PageTransferDataCacheProperties;

				OperationalEvent operationalEvent = new OperationalEvent
					(TDEventCategory.Business, TDTraceLevel.Error, message);
				Logger.Write(operationalEvent);
				
				throw new ScreenFlowException(message, e, true, TDExceptionIdentifier.SFMPropertiesInvalid);
			}
        }
        #endregion

        // ------------------------------------------------

        #region Validate xml
        /// <summary>
		/// Validates the two xml files by first checking that they exist,
		/// and then validating the files against the schema.
		/// </summary>
		/// <returns>True if Xml files exist and are valid.
		/// False if Xml files do not exist or are not valid.</returns>
		private bool ValidateXmlFiles(ArrayList errors)
		{
			bool valid = false;
			FileInfo fileInfo;

            #region Files exist
            // check the existence of the first file
			fileInfo = new FileInfo(pageTransferDetailsPath);
			valid = fileInfo.Exists;

			if(valid)
			{
				// check the existence of the second file
				fileInfo = new FileInfo(pageTransitionEventPath);
				valid = fileInfo.Exists;
			}
			else
			{
				errors.Add(pageTransferDetailsPath + " does not exist.");
				// Return, since an error was found
				return false;
			}

            if(valid)
            {
                // check the existence of the third file
                fileInfo = new FileInfo(pageGroupingPath);
                valid = fileInfo.Exists;
            }
            else
            {
                errors.Add(pageTransitionEventPath + " does not exist.");
                // Return, since an error was found
                return false;
            }
            #endregion

            #region Validate xml file
            if (valid)
			{
                // Validate the first Xml file
                XmlSchema schema = XmlSchema.Read(new XmlTextReader(pageTransferDetailsXsd), new ValidationEventHandler(ValidationHandler));
                XmlReaderSettings settings = new XmlReaderSettings();
                settings.Schemas.Add(schema);
                settings.ValidationType = ValidationType.Schema;
                settings.ValidationEventHandler += new ValidationEventHandler(ValidationHandler);
                XmlReader vr = XmlReader.Create(pageTransferDetailsPath,settings);
				
				while(vr.Read()){}
				vr.Close();
 
				if(xmlIsValid)
				{
					// Validate the second Xml file
                    settings.Schemas.Remove(schema);
                    schema = XmlSchema.Read(new XmlTextReader(pageTransitionEventXsd), new ValidationEventHandler(ValidationHandler));
                    settings.Schemas.Add(schema);

                    XmlReader reader = XmlReader.Create(pageTransitionEventPath,settings);

					while(reader.Read()){}
					reader.Close();

                    if (xmlIsValid)
                    {

                        // Validate the third Xml file
                        settings.Schemas.Remove(schema);
                        schema = XmlSchema.Read(new XmlTextReader(pageGroupingXsd), new ValidationEventHandler(ValidationHandler));
                        settings.Schemas.Add(schema);

                        reader = XmlReader.Create(pageGroupingPath, settings);

                        while (reader.Read()) { }
                        reader.Close();

                        if (xmlIsValid)
                            return true;
                        else
                        {
                            errors.Add(pageGroupingPath +
                            " failed the schema validation.\n" + xmlInvalidMessage);
                            // Return, since an error was found
                            return false;
                        }
                    }
                    else
                    {
                        errors.Add(pageTransitionEventPath +
                            " failed the schema validation.\n" + xmlInvalidMessage);
                        // Return, since an error was found
                        return false;
                    }
				}
				else
				{
					errors.Add(pageTransferDetailsPath +
						" failed the schema validation.\n" + xmlInvalidMessage);
					return false;
				}

			}
			else
			{
                errors.Add(pageGroupingPath + " does not exist.");
				// Return, since an error was found
				return false;
            }
            #endregion
        }

		// ------------------------------------------------

		/// <summary>
		/// Handler to validate the Xml files
		/// </summary>
		public void ValidationHandler(object sender, ValidationEventArgs args)
		{
			xmlInvalidMessage = args.Message;
			xmlIsValid = false;
        }
        #endregion

        // ------------------------------------------------

        #region Number of items in xml
        /// <summary>
		/// Returns the number of PageTransferDetails that was found in the Xml.
		/// </summary>
		/// <returns>Number of Page Transfer Details.</returns>
		private int NumberOfPageTransferDetails()
		{	
			// select the root node
			XmlNode root =
				pageTransferDetailsXml.DocumentElement.SelectSingleNode
				("//" + pageTransferRootNode);

			// select the list of pages nodes from the root node
			XmlNodeList pageNodes = root.SelectNodes(pageTransferPageNode);

			// return the total number of page nodes in the list
			return pageNodes.Count;
		
		} // NumberOfPageTransferDetails

		// ------------------------------------------------

		/// <summary>
		/// Returns the number of PageTransitionEvents found in the Xml.
		/// </summary>
		/// <returns>Number of Page Transition Events</returns>
		private int NumberOfPageTransitionEvents()
		{
			// select the root node
			XmlNode root = pageTransitionEventXml.DocumentElement.
				SelectSingleNode("//" + pageTransitionEventRootNode);

			// select the list of pages nodes from the root node
			XmlNodeList pageNodes = root.SelectNodes(pageTransitionEventPageNode);

			// return the total number of pages nodes in the list
			return pageNodes.Count;

		} // NumberOfPageTransitionEvents

		// ------------------------------------------------

        /// <summary>
        /// Returns the number of PageGroupings found in the Xml.
        /// </summary>
        /// <returns>Number of Page Groupings</returns>
        private int NumberOfPageGroupings()
        {
            // select the root node
            XmlNode root = pageGroupingXml.DocumentElement.
                SelectSingleNode("//" + pageGroupingRootNode);

            // select the list of pages nodes from the root node
            XmlNodeList pageNodes = root.SelectNodes(pageGroupingPageNode);

            // return the total number of pages nodes in the list
            return pageNodes.Count;

        } // NumberOfPageGroupings

        #endregion

        // ------------------------------------------------

        #region Populate
        /// <summary>
		/// Reads the xml file containing the page transfer data and populates
		/// the PageTransferDetails array.  This method assumes that validation to check
		/// that the file exists and is valid has already been done.
		/// </summary>
		/// <param name="filename">Xml file containing the page transfer data.</param>
		private void PopulatePageTransferDetailsArray()
		{
			// select the root node
			XmlNode root =
				pageTransferDetailsXml.SelectSingleNode("//" + pageTransferRootNode);

			// select the list of pages nodes from the root node
			XmlNodeList pageNodes = root.SelectNodes(pageTransferPageNode);

			// Read the attributes for each node in the list and
			// populate the hash table.

			PageTransferDetails pageTransferDetails;
			XmlAttribute tempAttributeBook;

			foreach(XmlNode node in pageNodes)
			{
				string pageId = node.Attributes[pageTransferPageIdAttribute].InnerText;
				string pageUrl = node.Attributes[pageTransferPageUrlAttribute].InnerText;
				string bookmarkRedirect =
					node.Attributes[pageTransferBookmarkRedirectAttribute].InnerText;
				bool specificStateClass =
					Convert.ToBoolean(node.Attributes[pageTransferSpecificStateClassAttribute].InnerText);

				//Test whether the BookmarkValidJourneyRedirect attribute exists for the page
				tempAttributeBook = node.Attributes[pageTransferBookmarkValidJourneyRedirectAttribute];
				string bookmarkValidJourneyRedirect = String.Empty;
				if (tempAttributeBook != null)
				{
					bookmarkValidJourneyRedirect = tempAttributeBook.InnerText;
				}

				// convert the pageId strings to the equivalent emuerated type
				PageId pageIdEnum = PageId.Empty;
				PageId bookmarkRedirectEnum = PageId.Empty;

				PageId bookmarkValidJourneyRedirectEnum = PageId.Empty;

				try
				{
					pageIdEnum = (PageId)Enum.Parse(typeof(PageId), pageId);

					bookmarkRedirectEnum =
						(PageId)Enum.Parse(typeof(PageId), bookmarkRedirect);

					if (bookmarkValidJourneyRedirect != String.Empty)
					{
						bookmarkValidJourneyRedirectEnum =
							(PageId)Enum.Parse(typeof(PageId), bookmarkValidJourneyRedirect);
					}
				}
				catch(ArgumentNullException ane)
				{
					// enumType or value is a null reference
					string message =
						String.Format(Messages.EnumConversionFailed,
						"pageId:" + pageId + "," + "bookmarkRedirect:" + 
						bookmarkRedirect) ;

					OperationalEvent operationalEvent = new OperationalEvent
						(TDEventCategory.Business, TDTraceLevel.Error, message);
					Logger.Write(operationalEvent);

					throw new ScreenFlowException(message, ane, true, TDExceptionIdentifier.SFMScreenFlowTableError); 

				}
				catch(ArgumentException ae)
				{
					string message =
						String.Format(Messages.EnumConversionFailed,
						"pageId:" + pageId + "," + "bookmarkRedirect:" +
						bookmarkRedirect);

					OperationalEvent operationalEvent = new OperationalEvent
						(TDEventCategory.Business, TDTraceLevel.Error, message);
					Logger.Write(operationalEvent);

					throw new ScreenFlowException(message, ae, true, TDExceptionIdentifier.SFMScreenFlowTableError); 
					
				}
				// create a new PageTransferDetails object
				pageTransferDetails = new PageTransferDetails
					(pageIdEnum, pageUrl, bookmarkRedirectEnum, specificStateClass, 
					bookmarkValidJourneyRedirectEnum);

				try
				{
					// convert the pageId enum into an int for indexing
					int index =
						Convert.ToInt32(pageIdEnum, CultureInfo.InvariantCulture);

					// assign the object to the pageTransferDetails array.
					pageTransferDetailsArray[index] = pageTransferDetails;
				}
				catch(IndexOutOfRangeException ioore)
				{
					// This can occur if the index is invalid.
					// i.e. 4 page events are defined in the xml,
					// however there are 5 page events in the enum.

					string message =
						String.Format
						(Messages.LoadingXmlFailed, "File:" +
						pageTransferDetailsPath);

					OperationalEvent operationalEvent = new OperationalEvent
						(TDEventCategory.Business, TDTraceLevel.Error, message);
					Logger.Write(operationalEvent);

					throw new ScreenFlowException(message, ioore, true, TDExceptionIdentifier.SFMInvalidScreenFlowTableIndex); 
				}
			}

		} // PopulatePageTransferData

		// ------------------------------------------------

		/// <summary>
		/// Populates the PageId array from the Xml.  This method assumes that
		/// validation to check that the file exists and is valid has already been done.
		/// </summary>
		private void PopulatePageIdArray()
		{
			// select the root node
			XmlNode root =
				pageTransitionEventXml.SelectSingleNode
				("//" + pageTransitionEventRootNode);

			// select the list of pages nodes from the root node
			XmlNodeList pageNodes = root.SelectNodes(pageTransitionEventPageNode);

			// Read the attributes for each node in the list and
			// populate the hash table.

			foreach(XmlNode node in pageNodes)
			{
				string transitionEvent =
					node.Attributes
					[pageTransitionEventTransitionEventAttribute].InnerText;
				string pageId =
					node.Attributes[pageTransitionEventPageIdAttribute].InnerText;
				
				// convert the transition event and pageId strings to their
				// equilavent enum types
				
				PageId pageIdEnum = PageId.Empty;
				TransitionEvent transitionEventEnum = TransitionEvent.Empty;

				try
				{
					transitionEventEnum = 
						(TransitionEvent)Enum.Parse
						(typeof(TransitionEvent), transitionEvent);
					pageIdEnum = (PageId)Enum.Parse(typeof(PageId), pageId);
				}
				catch(ArgumentNullException ane)
				{
					string message =
						String.Format(Messages.EnumConversionFailed,
						"pageId:" + pageId + "," + "transitionEvent:" + transitionEvent);

					OperationalEvent operationalEvent = new OperationalEvent
						(TDEventCategory.Business, TDTraceLevel.Error, message);
					Logger.Write(operationalEvent);

					throw new ScreenFlowException(message, ane, true, TDExceptionIdentifier.SFMScreenFlowNodeError); 
				}
				catch(ArgumentException ae)
				{
					string message =
						String.Format(Messages.EnumConversionFailed,
						"pageId:" + pageId + "," + "transitionEvent:" + transitionEvent);

					OperationalEvent operationalEvent = new OperationalEvent
						(TDEventCategory.Business, TDTraceLevel.Error, message);
					Logger.Write(operationalEvent);

					throw new ScreenFlowException(message, ae, true, TDExceptionIdentifier.SFMScreenFlowNodeError); 
				}

				try
				{
					// Add the page id to the pageTransitionEvents array
					// at index specified by the page transition event.
					int index = Convert.ToInt32
						(transitionEventEnum, CultureInfo.InvariantCulture);
					pageIdArray[index] = pageIdEnum;

					// Add the transiton event to the transitionEvent array.
					// This array is used only for valiation
					transitionEventArray[index] = transitionEventEnum;
				}
				catch(IndexOutOfRangeException ioore)
				{
					// This can occur if the index is invalid.
					// i.e. 4 page events are defined in the xml,
					// however there are 5 page events in the enum.

					string message =
						String.Format(Messages.LoadingXmlFailed,
						"File:" + pageTransitionEventPath);

					OperationalEvent operationalEvent = new OperationalEvent
						(TDEventCategory.Business, TDTraceLevel.Error, message);
					Logger.Write(operationalEvent);

					throw new ScreenFlowException(message, ioore, true, TDExceptionIdentifier.SFMInvalidScreenFlowTableIndex2); 
				}
			}
		} 

		// ------------------------------------------------

        /// <summary>
        /// Populates the PageGrouping array from the Xml.  This method assumes that
        /// validation to check that the file exists and is valid has already been done.
        /// </summary>
        private void PopulatePageGroupingArray()
        {
            // select the root node
            XmlNode root =
                pageGroupingXml.SelectSingleNode
                ("//" + pageGroupingRootNode);

            // select the list of pages nodes from the root node
            XmlNodeList pageNodes = root.SelectNodes(pageGroupingPageNode);

            // Read the attributes for each node in the list and
            // populate the hash table.

            PageGroupDetails pageGroupDetails;
            int index = 0;

            foreach (XmlNode node in pageNodes)
            {
                string group =
                    node.Attributes
                    [pageGroupingGroupAttribute].InnerText;
                string pageId =
                    node.Attributes[pageGroupingPageIdAttribute].InnerText;

                // convert the grouping and pageId strings to their
                // equilavent enum types

                PageId pageIdEnum = PageId.Empty;
                PageGroup pageGroupEnum = PageGroup.None;

                try
                {
                    pageGroupEnum =
                        (PageGroup)Enum.Parse
                        (typeof(PageGroup), group);
                    pageIdEnum = (PageId)Enum.Parse(typeof(PageId), pageId, true);
                }
                catch (ArgumentNullException ane)
                {
                    string message =
                        String.Format(Messages.EnumConversionFailed,
                        "pageId:" + pageId + "," + "pageGroup:" + group);

                    OperationalEvent operationalEvent = new OperationalEvent
                        (TDEventCategory.Business, TDTraceLevel.Error, message);
                    Logger.Write(operationalEvent);

                    throw new ScreenFlowException(message, ane, true, TDExceptionIdentifier.SFMScreenFlowNodeError);
                }
                catch (ArgumentException ae)
                {
                    string message =
                        String.Format(Messages.EnumConversionFailed,
                        "pageId:" + pageId + "," + "pageGroup:" + group);

                    OperationalEvent operationalEvent = new OperationalEvent
                        (TDEventCategory.Business, TDTraceLevel.Error, message);
                    Logger.Write(operationalEvent);

                    throw new ScreenFlowException(message, ae, true, TDExceptionIdentifier.SFMScreenFlowNodeError);
                }

                try
                {
                    // Assume PageTransferDetails have been loaded and validated by now, get to obtain the url
                    PageTransferDetails ptd = GetPageTransferDetails(pageIdEnum);

                    if (ptd == null)
                    {
                        // PageId doesnt exist, throw exception
                        string message = String.Format(Messages.PageTransferDataCachePageIdDoesNotExistForGroup, pageIdEnum);

                        throw new Exception(message);
                    }

                    // create a new PageGroupDetails object
                    pageGroupDetails = new PageGroupDetails(pageIdEnum, pageGroupEnum, ptd.PageUrl);

                    // assign the object to the pageGroupDetails array.
                    pageGroupDetailsArray[index] = pageGroupDetails;
                    index++;
                }
                catch (Exception ex)
                {
                    string message =
                        String.Format
                        (Messages.LoadingXmlFailed, "File:" +
                        pageGroupingPath) + " Exception: " + ex.Message;

                    OperationalEvent operationalEvent = new OperationalEvent
                        (TDEventCategory.Business, TDTraceLevel.Error, message);
                    Logger.Write(operationalEvent);

                    throw new ScreenFlowException(message, ex, true, TDExceptionIdentifier.SFMInvalidXml);
                }
            }
        }
        #endregion

        // ------------------------------------------------

        #region Validation

        /// <summary>
		/// Validates the tables to ensure that all data is correct.
		/// </summary>
		/// <param name="errors"></param>
		private void ValidateTables(ArrayList errors)
		{
			// Validation steps to perform:
			// 1. Check that no null objects exist in the arrays
			// 2. Check that each PageId and TransitionEvent
			// enum has a corresponding entry in the array.
			// 3. Check that if LogonRequired is true, then a corresponding
			// LogonRedirect entry exists.
			// 4. Check that for each entry, BookmarkRedirect and LogonRedirect
			// (if defined) have a corresponding entry in the array.
			// 5. Check that StateClassName is a valid class.
			// 6. Check that the Url is not an empty string.
			// 7. Check that PageId in the PageTransitionEvent table has
			// a corresponding entry in the PageTransferDetails array.

			bool valid = ValidateNonEmptyArrays(errors);

			if(!valid)
			{
				string message = String.Format
					(Messages.PageTransferDataCacheInconsistentEnumXml, errors[0]);
				// validate non empty arrays has failed

				OperationalEvent operationalEvent = new OperationalEvent
					(TDEventCategory.Business, TDTraceLevel.Error, message);
				Logger.Write(operationalEvent);

				throw new ScreenFlowException(message, true, TDExceptionIdentifier.SFMInvalidNonEmptyArray);
			}
			else
			{
				// continue validation
				valid = ValidatePageIdArrayEntryExists(errors);
			}

			if(!valid)
			{
				string message = String.Format
					(Messages.PageTransferDataCacheEmptyArray, errors[0]);
				// validate array entry exists has failed

				OperationalEvent operationalEvent = new OperationalEvent
					(TDEventCategory.Business, TDTraceLevel.Error, message);
				Logger.Write(operationalEvent);

				throw new ScreenFlowException(message, true, TDExceptionIdentifier.SFMArrayEntryValidationFailed);
			}
			else
			{
				// continue validation
				valid = ValidateTransitionEventArrayEntryExists(errors);
			}



			if(!valid)
			{
				string message = String.Format
					(Messages.PageTransferDataCacheEmptyArray, errors[0]);
				// validate array entry exists has failed
			
				OperationalEvent operationalEvent = new OperationalEvent
					(TDEventCategory.Business, TDTraceLevel.Error, message);
				Logger.Write(operationalEvent);

				throw new ScreenFlowException(message, true, TDExceptionIdentifier.SFMArrayEntryValidationFailed2);
			}
			else
			{
				// continue validation
				valid = ValidateEntryExists(errors);
			}

			if(!valid)
			{
				string message = String.Format
					(Messages.PageTransferDataCacheInvalidEntry, errors[0]);
				// previous validation step has failed
	
				OperationalEvent operationalEvent = new OperationalEvent
					(TDEventCategory.Business, TDTraceLevel.Error, message);
				Logger.Write(operationalEvent);

				throw new ScreenFlowException(message, true, TDExceptionIdentifier.SFMPreviousValidationFailed);
			}

			else
			{
				// continue validation
				valid = ValidatePageIdInTransitionEvent(errors);
			}

			if(!valid)
			{
				string message = String.Format
					(Messages.PageTransferDataCachePageIdDoesNotExist, errors[0]);

				OperationalEvent operationalEvent = new OperationalEvent
					(TDEventCategory.Business, TDTraceLevel.Error, message);
				Logger.Write(operationalEvent);

				throw new ScreenFlowException(message, true, TDExceptionIdentifier.SFMScreenFlowTableError2);
			}

		} // ValidateTables

		// ------------------------------------------------

		/// <summary>
		/// Checks that no element in the arrays are empty.
		/// </summary>
		/// <param name="errors">If any errors occur, a message
		/// diagnosing the error will be added to this.</param>
		/// <returns>True if no errors exist. False if errors exist.</returns>
		private bool ValidateNonEmptyArrays(ArrayList errors)
		{
			bool valid = true;
			
			// Check the PageTransferDetails array.
			int i = 0;
			while(i < pageTransferDetailsArray.Length & valid)
			{	
				valid = (pageTransferDetailsArray[i] != null);
				i++;
			}

			if(valid)
			{
				// Check the PageId array.
				int j=0;
				while(j < pageIdArray.Length & valid)
				{
					// Each pageId in the array must not be an empty value
					// i.e. it must set to a REAL pageId.
					valid = (pageIdArray[j] != PageId.Empty);

					if(!valid)
					{
						errors.Add("The PageIdArray contained an empty " +
							"element.  This could be because of a duplicate " +
							"transition event in the Xml file.");
					}

					j++;
				}
			}
			else
			{
				errors.Add("The PageTransferDetailsArray contained a " +
					"null element.  This could be because of a duplicate " +
					"page id in the Xml file.");
			}

			return valid;
		}

		// ------------------------------------------------

		/// <summary>
		/// Checks for consistency between the PageTransferDetails and
		/// the PageId enumeration.
		/// <param name="errors">If any errors occur, a message
		/// diagnosing the error will be added to this.</param>
		/// <returns>True if no errors exist. False if errors exist.</returns>
		private bool ValidatePageIdArrayEntryExists(ArrayList errors)
		{
			// Get all the pageIds that exist in the enumeration
			PageId[] allPageIds = (PageId[])Enum.GetValues(typeof(PageId));
			
			// Check that the number of values in the enumeration is equal to
			// the length of the pageTransferDetailsArray
			// NOTE: subtract 1 from allPageIds.Length because allPageIds contains
			// an extra value - PageId.Empty (this obviously doesn't exist in the Xml)
			bool valid = (allPageIds.Length-1 == pageTransferDetailsArray.Length);

			if(!valid)
			{
				// add an error message
				errors.Add("The number of pageIds in the PageId enumeration did " +
					"not match the number of PageIds read from the Xml file." +
					"The number of values in the enumeration was " +
					(allPageIds.Length-1) + " but the number of pageIds " +
					"read from the Xml file was " + pageTransferDetailsArray.Length);
			}
			else
			{

				int i = 0;
				while(i < allPageIds.Length-1 & valid)
				{
					PageId pageId = allPageIds[i];

					// expect the same pageId to exist in the
					// pageTransferDetailsArray at index i
					valid = (pageId == pageTransferDetailsArray[i].PageId);

					if(!valid)
					{
						// write an error message
						errors.Add("Validation failed when comparing the pageId " +
							"enum " + pageId + " with the Xml equilavent.");
					}

					i++;
				}
			}

			return valid;
		}

		// ------------------------------------------------

		/// <summary>
		/// Checks for consistency between the PageTransitionEvent Xml
		/// and the TransitionEvent enumeration.
		/// <param name="errors">If any errors occur, a message
		/// diagnosing the error will be added to this.</param>
		/// <returns>True if no errors exist. False if errors exist.</returns>
		private bool ValidateTransitionEventArrayEntryExists(ArrayList errors)
		{
			// Get all the TransitionEvents that exist in the enumeration
			TransitionEvent[] allTransitionEvents =
				(TransitionEvent[])Enum.GetValues(typeof(TransitionEvent));

			// Check that the number of values in the enumeration is equal
			// to the length of the array pageIdArray
			// NOTE: subtract 2 from allPageIds.Length because allPageIds contains
			// two extra values - TransitionEvent.Empty and
			// TransitionEvent,Default (this obviously doesn't exist in the Xml)
			bool valid = (allTransitionEvents.Length-2 == pageIdArray.Length);

			if(!valid)
			{
				// add an error message
				errors.Add("The number of transitionEvents in the " +
					"TransitionEvent enumeration did not match the number " +
					"of transition events read from the Xml file." +
					"The number of values in the enumeration was " +
					(allTransitionEvents.Length-2) + " but the number of transition " +
					"events read from the Xml file was " + pageIdArray.Length);
			}
			else
			{
				int j=0;
				while(j < allTransitionEvents.Length-2 & valid)
				{
					TransitionEvent transitionEvent = allTransitionEvents[j];

					// expect the same transition event to exist in the
					// transitionEventsArray at index i
					valid = (transitionEvent == transitionEventArray[j]);

					if(!valid)
					{
						// write an error message
						errors.Add("Validation failed when comparing the " +
							"transitonEvent enum " + transitionEvent + 
							" with the Xml equilavent.");
					}

					j++;
				}
			}
			return valid;
		}

		// ------------------------------------------------

		/// <summary>
		/// Checks to ensure that all entries that should exist do exist
		/// (e.g. all PageTransferDetails must have a non-empty Url.)
		/// Also check to ensure that all PageId references exist.
		/// <param name="errors">If any errors occur, a message
		/// diagnosing the error will be added to this.</param>
		/// <returns>True if no errors exist. False if errors exist.</returns>
		private bool ValidateEntryExists(ArrayList errors)
		{
			bool valid = true;
			int i = 0;

			try
			{
				while(i < pageTransferDetailsArray.Length & valid)
				{
					bool urlExists = false;
					bool bookmarkValid = false;
				
					PageTransferDetails pageTransferDetails =
						pageTransferDetailsArray[i];

					// Check that a URL exists
					urlExists = (pageTransferDetails.PageUrl.Length > 0);

					// get the bookmark redirect page Id
					PageId bookmarkRedirect = PageId.Empty;
					bookmarkRedirect = pageTransferDetails.BookmarkRedirect;

					// get the object specified at the bookmarkRedirect index
					// potentially throw an IndexOutOfBoundsException?
					int index = Convert.ToInt32
						(bookmarkRedirect,  CultureInfo.InvariantCulture);
					PageTransferDetails bookmarkRedirectObject =
						pageTransferDetailsArray[index];

					// Check that the bookmarkRedirect pageId matches the pageId of the
					// pageId inside the bookmarkRedirect
					bookmarkValid  = (bookmarkRedirect == bookmarkRedirectObject.PageId);

					// Finally, AND all booleans together to get the final validity result
					valid = urlExists & bookmarkValid;

					if(!valid)
					{
						errors.Add("Validation of Xml file failed.  The data in " +
							"the file: " + pageTransferDetailsPath + " is incorrect.");
					}

					// increment the counter
					i++;

				} // while

				return valid;

			}
			catch(IndexOutOfRangeException) 
			{
				// Index out of bounds for the array lookup,
				// it should never happen

				return false;
			}
		}

		// ------------------------------------------------

		/// <summary>
		/// Checks that a pageId reference from a TransitionEvent exists.
		/// <param name="errors">If any errors occur, a message
		/// diagnosing the error will be added to this.</param>
		/// <returns>True if no errors exist. False if errors exist.</returns>
		private bool ValidatePageIdInTransitionEvent(ArrayList errors)
		{
			bool pageIdExists = true;
			int i = 0;

			try
			{
				while(i < pageIdArray.Length & pageIdExists)
				{
					// get the pageId from the pageTransitionEvent array
					PageId pageId = pageIdArray[i];
					
					int index = Convert.ToInt32
						(pageId, CultureInfo.InvariantCulture);
					// retrive the PageTransitionDetails object
					PageTransferDetails pageTransferDetails
						= pageTransferDetailsArray[index];

					// check that the pageID matches
					pageIdExists = (pageId == pageTransferDetails.PageId);
					i++;
				}

				return pageIdExists;

			}
			catch(IndexOutOfRangeException) 
			{
				// Index out of bounds for the array lookup,
				// it should never happen

				return false;
			}
        }
        #endregion
    }
}
