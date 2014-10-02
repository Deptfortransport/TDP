// *********************************************** 
// NAME                 : TestMockProperties.cs 
// AUTHOR               : Kenny Cheung
// DATE CREATED         : 18/07/2003 
// DESCRIPTION  : Contains mock properties that
// are used by the NUnit tests.  (This simulates
// the property service).  The reason why mock
// objects are used instead of actual property
// files is because it allows tests with 
// different properties to executed all at once
// without having to manually make changes to a
// properties file.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ScreenFlow/Test/TestMockProperties.cs-arc  $ 
//
//   Rev 1.1   May 06 2008 16:05:56   mmodi
//Added PageGroup nunit tests
//Resolution for 4888: Del 10.1 - Improve Session Timeout Management
//
//   Rev 1.0   Nov 08 2007 12:47:54   mturner
//Initial revision.
//
//   Rev 1.9   Feb 23 2006 13:55:20   RWilby
//Merged stream3129
//
//   Rev 1.8   Feb 02 2006 11:11:30   mtillett
//Fix path location, so that relative
//
//   Rev 1.7   May 23 2005 10:03:02   rscott
//Updated for NUnit Tests
//
//   Rev 1.6   Sep 05 2003 15:18:00   kcheung
//Corrected properties spelling error
//
//   Rev 1.5   Aug 15 2003 14:36:54   passuied
//Update after design change
//
//   Rev 1.4   Jul 24 2003 17:10:38   kcheung
//Added Supersede to get rid of the warnings.
//
//   Rev 1.3   Jul 23 2003 13:28:44   kcheung
//Changed $log to $Log

using System;
using System.Collections;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common;
using System.IO;
using TransportDirect.UserPortal.ScreenFlow;

namespace TransportDirect.UserPortal.ScreenFlow_NUnit
{
	public class TestMockGoodProperties : IServiceFactory, IPropertyProvider
	{
		Hashtable properties;
		
		public TestMockGoodProperties()
		{
			properties = new Hashtable();
			
			// filenames
			properties[Keys.PageTransferDetailsFilePath] = 
				Directory.GetCurrentDirectory() + @"\PageTransferDetails.xml";
			properties[Keys.PageTransitionEventFilePath] =
				Directory.GetCurrentDirectory() + @"\PageTransitionEvent.xml";
            properties[Keys.PageGroupingFilePath] =
                Directory.GetCurrentDirectory() + @"\PageGrouping.xml";

            properties[Keys.PageTransferDetailsFileXsd] =
                Directory.GetCurrentDirectory() + @"\PageTransferDetails.xsd";
            properties[Keys.PageTransitionEventFileXsd] =
                Directory.GetCurrentDirectory() + @"\PageTransitionEvent.xsd";
            properties[Keys.PageGroupingFileXsd] =
                Directory.GetCurrentDirectory() + @"\PageGrouping.xsd";

			// Xml Node and Attribute Names
			properties[Keys.PageTransferDetailsNodeRoot] = "PageTransferDetails";
			properties[Keys.PageTransferDetailsNodePage] = "page";
			properties[Keys.PageTransferDetailsAttributePageId] = "PageId";
			properties[Keys.PageTransferDetailsAttributePageUrl] = "PageURL";
			properties[Keys.PageTransferDetailsAttributeBookmarkRedirect] = "BookmarkRedirect";
			properties[Keys.PageTransferDetailsAttributeSpecificStateClass] = "SpecificStateClass";
			properties[Keys.PageTransferDetailsAttributeBookmarkValidJourneyRedirect] = "BookmarkValidJourneyRedirect";
			
            properties[Keys.PageTransitionEventNodeRoot] = "PageTransitionEvents";
			properties[Keys.PageTransitionEventNodePage] = "page";
			properties[Keys.PageTransitionEventAttributePageId] = "PageId";
			properties[Keys.PageTransitionEventAttributeTransitionEvent] = "TransitionEvent";

            properties[Keys.PageGroupingNodeRoot] = "PageGrouping";
            properties[Keys.PageGroupingNodePage] = "page";
            properties[Keys.PageGroupingAttributePageId] = "PageId";
            properties[Keys.PageGroupingAttributeGroup] = "Group";
		}

		// Method to fullfill IServiceFactory
		public object Get()
		{
			return this;
		}

		// Methods for IPropertyProvider
		public event SupersededEventHandler Superseded;

		public string this[ string key ]
		{
			get { return properties[key].ToString(); }
		}

		/// <summary>
		/// Dummy Read only Indexer property that also takes a partner ID. 		
		/// Since this method was added to the interface and 
		/// hence every class which is implementing  IPropertyProvider interface must implement this method.
		/// </summary>
		public string this [string key, int partnerID]
		{
			get {return string.Empty;}
		}

		public bool IsSuperseded
		{
			get { return false; }
		}

		public int Version
		{
			get { return 0; }
		}

		public string GroupID
		{
			get { return ""; }
		}
		
		public string ApplicationID
		{
			get { return ""; }
		}
		
		public void Supersede()
		{
			Superseded(this, new EventArgs());
		}
	}

	// ------------------------------------------------------------------------

	public class TestMockPropertiesMissingPageIdsInXml : 
		IServiceFactory, IPropertyProvider
	{
		Hashtable properties;
		
		public TestMockPropertiesMissingPageIdsInXml()
		{
			properties = new Hashtable();
			
			// filenames
			properties[Keys.PageTransferDetailsFilePath] =
				Directory.GetCurrentDirectory() + @"\PageTransferDetailsMissingPageIds.xml";
			properties[Keys.PageTransitionEventFilePath] =
				Directory.GetCurrentDirectory() + @"\PageTransitionEvent.xml";
            properties[Keys.PageGroupingFilePath] =
                Directory.GetCurrentDirectory() + @"\PageGrouping.xml";

            properties[Keys.PageTransferDetailsFileXsd] =
                Directory.GetCurrentDirectory() + @"\PageTransferDetails.xsd";
            properties[Keys.PageTransitionEventFileXsd] =
                Directory.GetCurrentDirectory() + @"\PageTransitionEvent.xsd";
            properties[Keys.PageGroupingFileXsd] =
                Directory.GetCurrentDirectory() + @"\PageGrouping.xsd";

			// Xml Node and Attribute Names
			properties[Keys.PageTransferDetailsNodeRoot] = "PageTransferDetails";
			properties[Keys.PageTransferDetailsNodePage] = "page";
			properties[Keys.PageTransferDetailsAttributePageId] = "PageId";
			properties[Keys.PageTransferDetailsAttributePageUrl] = "PageURL";
			properties[Keys.PageTransferDetailsAttributeBookmarkRedirect] = "BookmarkRedirect";
			properties[Keys.PageTransferDetailsAttributeSpecificStateClass] = "SpecificStateClass";
			properties[Keys.PageTransferDetailsAttributeBookmarkValidJourneyRedirect] = "BookmarkValidJourneyRedirect";

            properties[Keys.PageTransitionEventNodeRoot] = "PageTransitionEvents";
			properties[Keys.PageTransitionEventNodePage] = "page";
			properties[Keys.PageTransitionEventAttributePageId] = "PageId";
			properties[Keys.PageTransitionEventAttributeTransitionEvent] = "TransitionEvent";

            properties[Keys.PageGroupingNodeRoot] = "PageGrouping";
            properties[Keys.PageGroupingNodePage] = "page";
            properties[Keys.PageGroupingAttributePageId] = "PageId";
            properties[Keys.PageGroupingAttributeGroup] = "Group";
		}

		// Method to fullfill IServiceFactory
		public object Get()
		{
			return this;
		}

		// Methods for IPropertyProvider
		public event SupersededEventHandler Superseded;

		public string this[ string key ]
		{
			get { return properties[key].ToString(); }
		}

		/// <summary>
		/// Dummy Read only Indexer property that also takes a partner ID. 		
		/// Since this method was added to the interface and 
		/// hence every class which is implementing  IPropertyProvider interface must implement this method.
		/// </summary>
		public string this [string key, int partnerID]
		{
			get {return string.Empty;}
		}

		public bool IsSuperseded
		{
			get { return false; }
		}

		public int Version
		{
			get { return 0; }
		}

		public string GroupID
		{
			get { return ""; }
		}
		
		public string ApplicationID
		{
			get { return ""; }
		}

		public void Supersede()
		{
			Superseded(this, new EventArgs());
		}
	}

	public class TestMockPropertiesMissingTransitionEventsInXml : 
		IServiceFactory, IPropertyProvider
	{
		Hashtable properties;
		
		public TestMockPropertiesMissingTransitionEventsInXml()
		{
			properties = new Hashtable();
			
			// filenames
			properties[Keys.PageTransferDetailsFilePath] =
				Directory.GetCurrentDirectory() + @"\PageTransferDetails.xml";
			properties[Keys.PageTransitionEventFilePath] =
				Directory.GetCurrentDirectory() + @"\PageTransitionEventMissingTransitionEvents.xml";
            properties[Keys.PageGroupingFilePath] =
                Directory.GetCurrentDirectory() + @"\PageGrouping.xml";

            properties[Keys.PageTransferDetailsFileXsd] =
                Directory.GetCurrentDirectory() + @"\PageTransferDetails.xsd";
            properties[Keys.PageTransitionEventFileXsd] =
                Directory.GetCurrentDirectory() + @"\PageTransitionEvent.xsd";
            properties[Keys.PageGroupingFileXsd] =
                Directory.GetCurrentDirectory() + @"\PageGrouping.xsd";
			
			// Xml Node and Attribute Names
			properties[Keys.PageTransferDetailsNodeRoot] = "PageTransferDetails";
			properties[Keys.PageTransferDetailsNodePage] = "page";
			properties[Keys.PageTransferDetailsAttributePageId] = "PageId";
			properties[Keys.PageTransferDetailsAttributePageUrl] = "PageURL";
			properties[Keys.PageTransferDetailsAttributeBookmarkRedirect] = "BookmarkRedirect";
			properties[Keys.PageTransferDetailsAttributeSpecificStateClass] = "SpecificStateClass";
			properties[Keys.PageTransferDetailsAttributeBookmarkValidJourneyRedirect] = "BookmarkValidJourneyRedirect";

            properties[Keys.PageTransitionEventNodeRoot] = "PageTransitionEvents";
			properties[Keys.PageTransitionEventNodePage] = "page";
			properties[Keys.PageTransitionEventAttributePageId] = "PageId";
			properties[Keys.PageTransitionEventAttributeTransitionEvent] = "TransitionEvent";

            properties[Keys.PageGroupingNodeRoot] = "PageGrouping";
            properties[Keys.PageGroupingNodePage] = "page";
            properties[Keys.PageGroupingAttributePageId] = "PageId";
            properties[Keys.PageGroupingAttributeGroup] = "Group";

		}

		// Method to fullfill IServiceFactory
		public object Get()
		{
			return this;
		}

		// Methods for IPropertyProvider
		public event SupersededEventHandler Superseded;

		public string this[ string key ]
		{
			get { return properties[key].ToString(); }
		}

		/// <summary>
		/// Dummy Read only Indexer property that also takes a partner ID. 		
		/// Since this method was added to the interface and 
		/// hence every class which is implementing  IPropertyProvider interface must implement this method.
		/// </summary>
		public string this [string key, int partnerID]
		{
			get {return string.Empty;}
		}

		public bool IsSuperseded
		{
			get { return false; }
		}

		public int Version
		{
			get { return 0; }
		}

		public string GroupID
		{
			get { return ""; }
		}
		
		public string ApplicationID
		{
			get { return ""; }
		}

		public void Supersede()
		{
			Superseded(this, new EventArgs());
		}
	}

	public class TestMockPropertiesExtraPageIdInXml : 
		IServiceFactory, IPropertyProvider
	{
		Hashtable properties;
		
		public TestMockPropertiesExtraPageIdInXml()
		{
			properties = new Hashtable();
			
			// filenames
			properties[Keys.PageTransferDetailsFilePath] =
				Directory.GetCurrentDirectory() + @"\PageTransferDetailsExtraPageIds.xml";
			properties[Keys.PageTransitionEventFilePath] =
				Directory.GetCurrentDirectory() + @"\PageTransitionEvent.xml";
            properties[Keys.PageGroupingFilePath] =
                Directory.GetCurrentDirectory() + @"\PageGrouping.xml";

            properties[Keys.PageTransferDetailsFileXsd] =
                Directory.GetCurrentDirectory() + @"\PageTransferDetails.xsd";
            properties[Keys.PageTransitionEventFileXsd] =
                Directory.GetCurrentDirectory() + @"\PageTransitionEvent.xsd";
            properties[Keys.PageGroupingFileXsd] =
                Directory.GetCurrentDirectory() + @"\PageGrouping.xsd";

			// Xml Node and Attribute Names
			properties[Keys.PageTransferDetailsNodeRoot] = "PageTransferDetails";
			properties[Keys.PageTransferDetailsNodePage] = "page";
			properties[Keys.PageTransferDetailsAttributePageId] = "PageId";
			properties[Keys.PageTransferDetailsAttributePageUrl] = "PageURL";
			properties[Keys.PageTransferDetailsAttributeBookmarkRedirect] = "BookmarkRedirect";
			properties[Keys.PageTransferDetailsAttributeSpecificStateClass] = "SpecificStateClass";
			properties[Keys.PageTransferDetailsAttributeBookmarkValidJourneyRedirect] = "BookmarkValidJourneyRedirect";

            properties[Keys.PageTransitionEventNodeRoot] = "PageTransitionEvents";
			properties[Keys.PageTransitionEventNodePage] = "page";
			properties[Keys.PageTransitionEventAttributePageId] = "PageId";
			properties[Keys.PageTransitionEventAttributeTransitionEvent] = "TransitionEvent";

            properties[Keys.PageGroupingNodeRoot] = "PageGrouping";
            properties[Keys.PageGroupingNodePage] = "page";
            properties[Keys.PageGroupingAttributePageId] = "PageId";
            properties[Keys.PageGroupingAttributeGroup] = "Group";
		}

		// Method to fullfill IServiceFactory
		public object Get()
		{
			return this;
		}

		// Methods for IPropertyProvider
		public event SupersededEventHandler Superseded;

		public string this[ string key ]
		{
			get { return properties[key].ToString(); }
		}

		/// <summary>
		/// Dummy Read only Indexer property that also takes a partner ID. 		
		/// Since this method was added to the interface and 
		/// hence every class which is implementing  IPropertyProvider interface must implement this method.
		/// </summary>
		public string this [string key, int partnerID]
		{
			get {return string.Empty;}
		}

		public bool IsSuperseded
		{
			get { return false; }
		}

		public int Version
		{
			get { return 0; }
		}

		public string GroupID
		{
			get { return ""; }
		}
		
		public string ApplicationID
		{
			get { return ""; }
		}

		public void Supersede()
		{
			Superseded(this, new EventArgs());
		}
	}

	public class TestMockPropertiesExtraTransitionEventInXml : 
		IServiceFactory, IPropertyProvider
	{
		Hashtable properties;
		
		public TestMockPropertiesExtraTransitionEventInXml()
		{
			properties = new Hashtable();
			
			// filenames
			properties[Keys.PageTransferDetailsFilePath] =
				Directory.GetCurrentDirectory() + @"\PageTransferDetails.xml";
			properties[Keys.PageTransitionEventFilePath] =
				Directory.GetCurrentDirectory() + @"\PageTransitionEventExtraTransitionEvents.xml";
            properties[Keys.PageGroupingFilePath] =
                Directory.GetCurrentDirectory() + @"\PageGrouping.xml";

            properties[Keys.PageTransferDetailsFileXsd] =
                Directory.GetCurrentDirectory() + @"\PageTransferDetails.xsd";
            properties[Keys.PageTransitionEventFileXsd] =
                Directory.GetCurrentDirectory() + @"\PageTransitionEvent.xsd";
            properties[Keys.PageGroupingFileXsd] =
                Directory.GetCurrentDirectory() + @"\PageGrouping.xsd";

			// Xml Node and Attribute Names
			properties[Keys.PageTransferDetailsNodeRoot] = "PageTransferDetails";
			properties[Keys.PageTransferDetailsNodePage] = "page";
			properties[Keys.PageTransferDetailsAttributePageId] = "PageId";
			properties[Keys.PageTransferDetailsAttributePageUrl] = "PageURL";
			properties[Keys.PageTransferDetailsAttributeBookmarkRedirect] = "BookmarkRedirect";
			properties[Keys.PageTransferDetailsAttributeSpecificStateClass] = "SpecificStateClass";
			properties[Keys.PageTransferDetailsAttributeBookmarkValidJourneyRedirect] = "BookmarkValidJourneyRedirect";

            properties[Keys.PageTransitionEventNodeRoot] = "PageTransitionEvents";
			properties[Keys.PageTransitionEventNodePage] = "page";
			properties[Keys.PageTransitionEventAttributePageId] = "PageId";
			properties[Keys.PageTransitionEventAttributeTransitionEvent] = "TransitionEvent";

            properties[Keys.PageGroupingNodeRoot] = "PageGrouping";
            properties[Keys.PageGroupingNodePage] = "page";
            properties[Keys.PageGroupingAttributePageId] = "PageId";
            properties[Keys.PageGroupingAttributeGroup] = "Group";
		}

		// Method to fullfill IServiceFactory
		public object Get()
		{
			return this;
		}

		// Methods for IPropertyProvider
		public event SupersededEventHandler Superseded;

		public string this[ string key ]
		{
			get { return properties[key].ToString(); }
		}

		/// <summary>
		/// Dummy Read only Indexer property that also takes a partner ID. 		
		/// Since this method was added to the interface and 
		/// hence every class which is implementing  IPropertyProvider interface must implement this method.
		/// </summary>
		public string this [string key, int partnerID]
		{
			get {return string.Empty;}
		}

		public bool IsSuperseded
		{
			get { return false; }
		}

		public int Version
		{
			get { return 0; }
		}

		public string GroupID
		{
			get { return ""; }
		}
		
		public string ApplicationID
		{
			get { return ""; }
		}

		public void Supersede()
		{
			Superseded(this, new EventArgs());
		}
	}

	public class TestMockPropertiesDuplicatePageIdInXml : 
		IServiceFactory, IPropertyProvider
	{
		Hashtable properties;
		
		public TestMockPropertiesDuplicatePageIdInXml()
		{
			properties = new Hashtable();
			
			// filenames
			properties[Keys.PageTransferDetailsFilePath] =
				Directory.GetCurrentDirectory() + @"\PageTransferDetailsDuplicatePageIds.xml";
			properties[Keys.PageTransitionEventFilePath] =
				Directory.GetCurrentDirectory() + @"\PageTransitionEvent.xml";
            properties[Keys.PageGroupingFilePath] =
                Directory.GetCurrentDirectory() + @"\PageGrouping.xml";

            properties[Keys.PageTransferDetailsFileXsd] =
                Directory.GetCurrentDirectory() + @"\PageTransferDetails.xsd";
            properties[Keys.PageTransitionEventFileXsd] =
                Directory.GetCurrentDirectory() + @"\PageTransitionEvent.xsd";
            properties[Keys.PageGroupingFileXsd] =
                Directory.GetCurrentDirectory() + @"\PageGrouping.xsd";

			// Xml Node and Attribute Names
			properties[Keys.PageTransferDetailsNodeRoot] = "PageTransferDetails";
			properties[Keys.PageTransferDetailsNodePage] = "page";
			properties[Keys.PageTransferDetailsAttributePageId] = "PageId";
			properties[Keys.PageTransferDetailsAttributePageUrl] = "PageURL";
			properties[Keys.PageTransferDetailsAttributeBookmarkRedirect] = "BookmarkRedirect";
			properties[Keys.PageTransferDetailsAttributeSpecificStateClass] = "SpecificStateClass";
			properties[Keys.PageTransferDetailsAttributeBookmarkValidJourneyRedirect] = "BookmarkValidJourneyRedirect";

            properties[Keys.PageTransitionEventNodeRoot] = "PageTransitionEvents";
			properties[Keys.PageTransitionEventNodePage] = "page";
			properties[Keys.PageTransitionEventAttributePageId] = "PageId";
			properties[Keys.PageTransitionEventAttributeTransitionEvent] = "TransitionEvent";

            properties[Keys.PageGroupingNodeRoot] = "PageGrouping";
            properties[Keys.PageGroupingNodePage] = "page";
            properties[Keys.PageGroupingAttributePageId] = "PageId";
            properties[Keys.PageGroupingAttributeGroup] = "Group";
		}

		// Method to fullfill IServiceFactory
		public object Get()
		{
			return this;
		}

		// Methods for IPropertyProvider
		public event SupersededEventHandler Superseded;

		public string this[ string key ]
		{
			get { return properties[key].ToString(); }
		}

		/// <summary>
		/// Dummy Read only Indexer property that also takes a partner ID. 		
		/// Since this method was added to the interface and 
		/// hence every class which is implementing  IPropertyProvider interface must implement this method.
		/// </summary>
		public string this [string key, int partnerID]
		{
			get {return string.Empty;}
		}

		public bool IsSuperseded
		{
			get { return false; }
		}

		public int Version
		{
			get { return 0; }
		}

		public string GroupID
		{
			get { return ""; }
		}
		
		public string ApplicationID
		{
			get { return ""; }
		}

		public void Supersede()
		{
			Superseded(this, new EventArgs());
		}
	}

	public class TestMockPropertiesDuplicateTransitionEventInXml : 
		IServiceFactory, IPropertyProvider
	{
		Hashtable properties;
		
		public TestMockPropertiesDuplicateTransitionEventInXml()
		{
			properties = new Hashtable();
			
			// filenames
			properties[Keys.PageTransferDetailsFilePath] =
				Directory.GetCurrentDirectory() + @"\PageTransferDetails.xml";
			properties[Keys.PageTransitionEventFilePath] =
				Directory.GetCurrentDirectory() + @"\PageTransitionEventDuplicateTransitionEvents.xml";
            properties[Keys.PageGroupingFilePath] =
                Directory.GetCurrentDirectory() + @"\PageGrouping.xml";

            properties[Keys.PageTransferDetailsFileXsd] =
                Directory.GetCurrentDirectory() + @"\PageTransferDetails.xsd";
            properties[Keys.PageTransitionEventFileXsd] =
                Directory.GetCurrentDirectory() + @"\PageTransitionEvent.xsd";
            properties[Keys.PageGroupingFileXsd] =
                Directory.GetCurrentDirectory() + @"\PageGrouping.xsd";

			// Xml Node and Attribute Names
			properties[Keys.PageTransferDetailsNodeRoot] = "PageTransferDetails";
			properties[Keys.PageTransferDetailsNodePage] = "page";
			properties[Keys.PageTransferDetailsAttributePageId] = "PageId";
			properties[Keys.PageTransferDetailsAttributePageUrl] = "PageURL";
			properties[Keys.PageTransferDetailsAttributeBookmarkRedirect] = "BookmarkRedirect";
			properties[Keys.PageTransferDetailsAttributeSpecificStateClass] = "SpecificStateClass";
			properties[Keys.PageTransferDetailsAttributeBookmarkValidJourneyRedirect] = "BookmarkValidJourneyRedirect";

            properties[Keys.PageTransitionEventNodeRoot] = "PageTransitionEvents";
			properties[Keys.PageTransitionEventNodePage] = "page";
			properties[Keys.PageTransitionEventAttributePageId] = "PageId";
			properties[Keys.PageTransitionEventAttributeTransitionEvent] = "TransitionEvent";

            properties[Keys.PageGroupingNodeRoot] = "PageGrouping";
            properties[Keys.PageGroupingNodePage] = "page";
            properties[Keys.PageGroupingAttributePageId] = "PageId";
            properties[Keys.PageGroupingAttributeGroup] = "Group";

		}

		// Method to fullfill IServiceFactory
		public object Get()
		{
			return this;
		}

		// Methods for IPropertyProvider
		public event SupersededEventHandler Superseded;

		public string this[ string key ]
		{
			get { return properties[key].ToString(); }
		}

		/// <summary>
		/// Dummy Read only Indexer property that also takes a partner ID. 		
		/// Since this method was added to the interface and 
		/// hence every class which is implementing  IPropertyProvider interface must implement this method.
		/// </summary>
		public string this [string key, int partnerID]
		{
			get {return string.Empty;}
		}

		public bool IsSuperseded
		{
			get { return false; }
		}

		public int Version
		{
			get { return 0; }
		}

		public string GroupID
		{
			get { return ""; }
		}
		
		public string ApplicationID
		{
			get { return ""; }
		}

		public void Supersede()
		{
			Superseded(this, new EventArgs());
		}
	}

	public class TestMockPropertiesIncorrectPageIdInXml : 
		IServiceFactory, IPropertyProvider
	{
		Hashtable properties;
		
		public TestMockPropertiesIncorrectPageIdInXml()
		{
			properties = new Hashtable();
			
			// filenames
			properties[Keys.PageTransferDetailsFilePath] =
				Directory.GetCurrentDirectory() + @"\PageTransferDetailsIncorrectPageId.xml";
			properties[Keys.PageTransitionEventFilePath] =
				Directory.GetCurrentDirectory() + @"\PageTransitionEvent.xml";
            properties[Keys.PageGroupingFilePath] =
                Directory.GetCurrentDirectory() + @"\PageGrouping.xml";

            properties[Keys.PageTransferDetailsFileXsd] =
                Directory.GetCurrentDirectory() + @"\PageTransferDetails.xsd";
            properties[Keys.PageTransitionEventFileXsd] =
                Directory.GetCurrentDirectory() + @"\PageTransitionEvent.xsd";
            properties[Keys.PageGroupingFileXsd] =
                Directory.GetCurrentDirectory() + @"\PageGrouping.xsd";

			// Xml Node and Attribute Names
			properties[Keys.PageTransferDetailsNodeRoot] = "PageTransferDetails";
			properties[Keys.PageTransferDetailsNodePage] = "page";
			properties[Keys.PageTransferDetailsAttributePageId] = "PageId";
			properties[Keys.PageTransferDetailsAttributePageUrl] = "PageURL";
			properties[Keys.PageTransferDetailsAttributeBookmarkRedirect] = "BookmarkRedirect";
			properties[Keys.PageTransferDetailsAttributeSpecificStateClass] = "SpecificStateClass";
			properties[Keys.PageTransferDetailsAttributeBookmarkValidJourneyRedirect] = "BookmarkValidJourneyRedirect";

            properties[Keys.PageTransitionEventNodeRoot] = "PageTransitionEvents";
			properties[Keys.PageTransitionEventNodePage] = "page";
			properties[Keys.PageTransitionEventAttributePageId] = "PageId";
			properties[Keys.PageTransitionEventAttributeTransitionEvent] = "TransitionEvent";

            properties[Keys.PageGroupingNodeRoot] = "PageGrouping";
            properties[Keys.PageGroupingNodePage] = "page";
            properties[Keys.PageGroupingAttributePageId] = "PageId";
            properties[Keys.PageGroupingAttributeGroup] = "Group";

		}

		// Method to fullfill IServiceFactory
		public object Get()
		{
			return this;
		}

		// Methods for IPropertyProvider
		public event SupersededEventHandler Superseded;

		public string this[ string key ]
		{
			get { return properties[key].ToString(); }
		}

		/// <summary>
		/// Dummy Read only Indexer property that also takes a partner ID. 		
		/// Since this method was added to the interface and 
		/// hence every class which is implementing  IPropertyProvider interface must implement this method.
		/// </summary>
		public string this [string key, int partnerID]
		{
			get {return string.Empty;}
		}

		public bool IsSuperseded
		{
			get { return false; }
		}

		public int Version
		{
			get { return 0; }
		}

		public string GroupID
		{
			get { return ""; }
		}
		
		public string ApplicationID
		{
			get { return ""; }
		}

		public void Supersede()
		{
			Superseded(this, new EventArgs());
		}
	}

	public class TestMockPropertiesIncorrectTransitionEventInXml : 
		IServiceFactory, IPropertyProvider
	{
		Hashtable properties;
		
		public TestMockPropertiesIncorrectTransitionEventInXml()
		{
			properties = new Hashtable();
			
			// filenames
			properties[Keys.PageTransferDetailsFilePath] =
				Directory.GetCurrentDirectory() + @"\PageTransferDetails.xml";
			properties[Keys.PageTransitionEventFilePath] =
				Directory.GetCurrentDirectory() + @"\PageTransitionEventIncorrectTransitionEvents.xml";
            properties[Keys.PageGroupingFilePath] =
                Directory.GetCurrentDirectory() + @"\PageGrouping.xml";

            properties[Keys.PageTransferDetailsFileXsd] =
                Directory.GetCurrentDirectory() + @"\PageTransferDetails.xsd";
            properties[Keys.PageTransitionEventFileXsd] =
                Directory.GetCurrentDirectory() + @"\PageTransitionEvent.xsd";
            properties[Keys.PageGroupingFileXsd] =
                Directory.GetCurrentDirectory() + @"\PageGrouping.xsd";

			// Xml Node and Attribute Names
			properties[Keys.PageTransferDetailsNodeRoot] = "PageTransferDetails";
			properties[Keys.PageTransferDetailsNodePage] = "page";
			properties[Keys.PageTransferDetailsAttributePageId] = "PageId";
			properties[Keys.PageTransferDetailsAttributePageUrl] = "PageURL";
			properties[Keys.PageTransferDetailsAttributeBookmarkRedirect] = "BookmarkRedirect";
			properties[Keys.PageTransferDetailsAttributeSpecificStateClass] = "SpecificStateClass";
			properties[Keys.PageTransferDetailsAttributeBookmarkValidJourneyRedirect] = "BookmarkValidJourneyRedirect";

            properties[Keys.PageTransitionEventNodeRoot] = "PageTransitionEvents";
			properties[Keys.PageTransitionEventNodePage] = "page";
			properties[Keys.PageTransitionEventAttributePageId] = "PageId";
			properties[Keys.PageTransitionEventAttributeTransitionEvent] = "TransitionEvent";

            properties[Keys.PageGroupingNodeRoot] = "PageGrouping";
            properties[Keys.PageGroupingNodePage] = "page";
            properties[Keys.PageGroupingAttributePageId] = "PageId";
            properties[Keys.PageGroupingAttributeGroup] = "Group";
		}

		// Method to fullfill IServiceFactory
		public object Get()
		{
			return this;
		}

		// Methods for IPropertyProvider
		public event SupersededEventHandler Superseded;

		public string this[ string key ]
		{
			get { return properties[key].ToString(); }
		}

		/// <summary>
		/// Dummy Read only Indexer property that also takes a partner ID. 		
		/// Since this method was added to the interface and 
		/// hence every class which is implementing  IPropertyProvider interface must implement this method.
		/// </summary>
		public string this [string key, int partnerID]
		{
			get {return string.Empty;}
		}

		public bool IsSuperseded
		{
			get { return false; }
		}

		public int Version
		{
			get { return 0; }
		}

		public string GroupID
		{
			get { return ""; }
		}
		
		public string ApplicationID
		{
			get { return ""; }
		}

		public void Supersede()
		{
			Superseded(this, new EventArgs());
		}
	}

	public class TestMockPropertiesEmptyUrlInXml : 
		IServiceFactory, IPropertyProvider
	{
		Hashtable properties;
		
		public TestMockPropertiesEmptyUrlInXml()
		{
			properties = new Hashtable();
			
			// filenames
			properties[Keys.PageTransferDetailsFilePath] =
				Directory.GetCurrentDirectory() + @"\PageTransferDetailsEmptyUrl.xml";
			properties[Keys.PageTransitionEventFilePath] =
				Directory.GetCurrentDirectory() + @"\PageTransitionEvent.xml";
            properties[Keys.PageGroupingFilePath] =
                Directory.GetCurrentDirectory() + @"\PageGrouping.xml";

            properties[Keys.PageTransferDetailsFileXsd] =
                Directory.GetCurrentDirectory() + @"\PageTransferDetails.xsd";
            properties[Keys.PageTransitionEventFileXsd] =
                Directory.GetCurrentDirectory() + @"\PageTransitionEvent.xsd";
            properties[Keys.PageGroupingFileXsd] =
                Directory.GetCurrentDirectory() + @"\PageGrouping.xsd";
    
            // Xml Node and Attribute Names
			properties[Keys.PageTransferDetailsNodeRoot] = "PageTransferDetails";
			properties[Keys.PageTransferDetailsNodePage] = "page";
			properties[Keys.PageTransferDetailsAttributePageId] = "PageId";
			properties[Keys.PageTransferDetailsAttributePageUrl] = "PageURL";
			properties[Keys.PageTransferDetailsAttributeBookmarkRedirect] = "BookmarkRedirect";
			properties[Keys.PageTransferDetailsAttributeSpecificStateClass] = "SpecificStateClass";
			properties[Keys.PageTransferDetailsAttributeBookmarkValidJourneyRedirect] = "BookmarkValidJourneyRedirect";

            properties[Keys.PageTransitionEventNodeRoot] = "PageTransitionEvents";
			properties[Keys.PageTransitionEventNodePage] = "page";
			properties[Keys.PageTransitionEventAttributePageId] = "PageId";
			properties[Keys.PageTransitionEventAttributeTransitionEvent] = "TransitionEvent";

            properties[Keys.PageGroupingNodeRoot] = "PageGrouping";
            properties[Keys.PageGroupingNodePage] = "page";
            properties[Keys.PageGroupingAttributePageId] = "PageId";
            properties[Keys.PageGroupingAttributeGroup] = "Group";
		}

		// Method to fullfill IServiceFactory
		public object Get()
		{
			return this;
		}

		// Methods for IPropertyProvider
		public event SupersededEventHandler Superseded;

		public string this[ string key ]
		{
			get { return properties[key].ToString(); }
		}

		/// <summary>
		/// Dummy Read only Indexer property that also takes a partner ID. 		
		/// Since this method was added to the interface and 
		/// hence every class which is implementing  IPropertyProvider interface must implement this method.
		/// </summary>
		public string this [string key, int partnerID]
		{
			get {return string.Empty;}
		}

		public bool IsSuperseded
		{
			get { return false; }
		}

		public int Version
		{
			get { return 0; }
		}

		public string GroupID
		{
			get { return ""; }
		}
		
		public string ApplicationID
		{
			get { return ""; }
		}

		public void Supersede()
		{
			Superseded(this, new EventArgs());
		}
	}

	public class TestMockPropertiesInvalidBookmarkRedirect : 
		IServiceFactory, IPropertyProvider
	{
		Hashtable properties;
		
		public TestMockPropertiesInvalidBookmarkRedirect()
		{
			properties = new Hashtable();
			
			// filenames
			properties[Keys.PageTransferDetailsFilePath] =
				Directory.GetCurrentDirectory() + @"\PageTransferDetailsInvalidBookmarkRedirect.xml";
			properties[Keys.PageTransitionEventFilePath] =
				Directory.GetCurrentDirectory() + @"\PageTransitionEvent.xml";
            properties[Keys.PageGroupingFilePath] =
                Directory.GetCurrentDirectory() + @"\PageGrouping.xml";

            properties[Keys.PageTransferDetailsFileXsd] =
                Directory.GetCurrentDirectory() + @"\PageTransferDetails.xsd";
            properties[Keys.PageTransitionEventFileXsd] =
                Directory.GetCurrentDirectory() + @"\PageTransitionEvent.xsd";
            properties[Keys.PageGroupingFileXsd] =
                Directory.GetCurrentDirectory() + @"\PageGrouping.xsd";

			// Xml Node and Attribute Names
			properties[Keys.PageTransferDetailsNodeRoot] = "PageTransferDetails";
			properties[Keys.PageTransferDetailsNodePage] = "page";
			properties[Keys.PageTransferDetailsAttributePageId] = "PageId";
			properties[Keys.PageTransferDetailsAttributePageUrl] = "PageURL";
			properties[Keys.PageTransferDetailsAttributeBookmarkRedirect] = "BookmarkRedirect";
			properties[Keys.PageTransferDetailsAttributeSpecificStateClass] = "SpecificStateClass";
			properties[Keys.PageTransferDetailsAttributeBookmarkValidJourneyRedirect] = "BookmarkValidJourneyRedirect";

            properties[Keys.PageTransitionEventNodeRoot] = "PageTransitionEvents";
			properties[Keys.PageTransitionEventNodePage] = "page";
			properties[Keys.PageTransitionEventAttributePageId] = "PageId";
			properties[Keys.PageTransitionEventAttributeTransitionEvent] = "TransitionEvent";

            properties[Keys.PageGroupingNodeRoot] = "PageGrouping";
            properties[Keys.PageGroupingNodePage] = "page";
            properties[Keys.PageGroupingAttributePageId] = "PageId";
            properties[Keys.PageGroupingAttributeGroup] = "Group";
		}

		// Method to fullfill IServiceFactory
		public object Get()
		{
			return this;
		}

		// Methods for IPropertyProvider
		public event SupersededEventHandler Superseded;

		public string this[ string key ]
		{
			get { return properties[key].ToString(); }
		}

		/// <summary>
		/// Dummy Read only Indexer property that also takes a partner ID. 		
		/// Since this method was added to the interface and 
		/// hence every class which is implementing  IPropertyProvider interface must implement this method.
		/// </summary>
		public string this [string key, int partnerID]
		{
			get {return string.Empty;}
		}

		public bool IsSuperseded
		{
			get { return false; }
		}

		public int Version
		{
			get { return 0; }
		}

		public string GroupID
		{
			get { return ""; }
		}
		
		public string ApplicationID
		{
			get { return ""; }
		}

		public void Supersede()
		{
			Superseded(this, new EventArgs());
		}
	}

	public class TestMockPropertiesMissingPageTransferDetailsFile : 
		IServiceFactory, IPropertyProvider
	{
		Hashtable properties;
		
		public TestMockPropertiesMissingPageTransferDetailsFile()
		{
			properties = new Hashtable();
			
			// filenames
			properties[Keys.PageTransferDetailsFilePath] =
				Directory.GetCurrentDirectory() + @"\ThisFileDoesNotExist.xml";
			properties[Keys.PageTransitionEventFilePath] =
				Directory.GetCurrentDirectory() + @"\PageTransitionEvent.xml";
            properties[Keys.PageGroupingFilePath] =
                Directory.GetCurrentDirectory() + @"\PageGrouping.xml";

            properties[Keys.PageTransferDetailsFileXsd] =
                Directory.GetCurrentDirectory() + @"\PageTransferDetails.xsd";
            properties[Keys.PageTransitionEventFileXsd] =
                Directory.GetCurrentDirectory() + @"\PageTransitionEvent.xsd";
            properties[Keys.PageGroupingFileXsd] =
                Directory.GetCurrentDirectory() + @"\PageGrouping.xsd";

			// Xml Node and Attribute Names
			properties[Keys.PageTransferDetailsNodeRoot] = "PageTransferDetails";
			properties[Keys.PageTransferDetailsNodePage] = "page";
			properties[Keys.PageTransferDetailsAttributePageId] = "PageId";
			properties[Keys.PageTransferDetailsAttributePageUrl] = "PageURL";
			properties[Keys.PageTransferDetailsAttributeBookmarkRedirect] = "BookmarkRedirect";
			properties[Keys.PageTransferDetailsAttributeSpecificStateClass] = "SpecificStateClass";
			properties[Keys.PageTransferDetailsAttributeBookmarkValidJourneyRedirect] = "BookmarkValidJourneyRedirect";

            properties[Keys.PageTransitionEventNodeRoot] = "PageTransitionEvents";
			properties[Keys.PageTransitionEventNodePage] = "page";
			properties[Keys.PageTransitionEventAttributePageId] = "PageId";
			properties[Keys.PageTransitionEventAttributeTransitionEvent] = "TransitionEvent";

            properties[Keys.PageGroupingNodeRoot] = "PageGrouping";
            properties[Keys.PageGroupingNodePage] = "page";
            properties[Keys.PageGroupingAttributePageId] = "PageId";
            properties[Keys.PageGroupingAttributeGroup] = "Group";
		}

		// Method to fullfill IServiceFactory
		public object Get()
		{
			return this;
		}

		// Methods for IPropertyProvider
		public event SupersededEventHandler Superseded;

		public string this[ string key ]
		{
			get { return properties[key].ToString(); }
		}

		/// <summary>
		/// Dummy Read only Indexer property that also takes a partner ID. 		
		/// Since this method was added to the interface and 
		/// hence every class which is implementing  IPropertyProvider interface must implement this method.
		/// </summary>
		public string this [string key, int partnerID]
		{
			get {return string.Empty;}
		}

		public bool IsSuperseded
		{
			get { return false; }
		}

		public int Version
		{
			get { return 0; }
		}

		public string GroupID
		{
			get { return ""; }
		}
		
		public string ApplicationID
		{
			get { return ""; }
		}

		public void Supersede()
		{
			Superseded(this, new EventArgs());
		}
	}

	public class TestMockPropertiesMissingTransitionEventFile : 
		IServiceFactory, IPropertyProvider
	{
		Hashtable properties;
		
		public TestMockPropertiesMissingTransitionEventFile()
		{
			properties = new Hashtable();
			
			// filenames
			properties[Keys.PageTransferDetailsFilePath] =
				Directory.GetCurrentDirectory() + @"\PageTransferDetails.xml";
			properties[Keys.PageTransitionEventFilePath] =
				Directory.GetCurrentDirectory() + @"\ThisFileDoesNotExist.xml";
            properties[Keys.PageGroupingFilePath] =
                Directory.GetCurrentDirectory() + @"\PageGrouping.xml";

            properties[Keys.PageTransferDetailsFileXsd] =
                Directory.GetCurrentDirectory() + @"\PageTransferDetails.xsd";
            properties[Keys.PageTransitionEventFileXsd] =
                Directory.GetCurrentDirectory() + @"\PageTransitionEvent.xsd";
            properties[Keys.PageGroupingFileXsd] =
                Directory.GetCurrentDirectory() + @"\PageGrouping.xsd";

            // Xml Node and Attribute Names
			properties[Keys.PageTransferDetailsNodeRoot] = "PageTransferDetails";
			properties[Keys.PageTransferDetailsNodePage] = "page";
			properties[Keys.PageTransferDetailsAttributePageId] = "PageId";
			properties[Keys.PageTransferDetailsAttributePageUrl] = "PageURL";
			properties[Keys.PageTransferDetailsAttributeBookmarkRedirect] = "BookmarkRedirect";
			properties[Keys.PageTransferDetailsAttributeSpecificStateClass] = "SpecificStateClass";
			properties[Keys.PageTransferDetailsAttributeBookmarkValidJourneyRedirect] = "BookmarkValidJourneyRedirect";

            properties[Keys.PageTransitionEventNodeRoot] = "PageTransitionEvents";
			properties[Keys.PageTransitionEventNodePage] = "page";
			properties[Keys.PageTransitionEventAttributePageId] = "PageId";
			properties[Keys.PageTransitionEventAttributeTransitionEvent] = "TransitionEvent";

            properties[Keys.PageGroupingNodeRoot] = "PageGrouping";
            properties[Keys.PageGroupingNodePage] = "page";
            properties[Keys.PageGroupingAttributePageId] = "PageId";
            properties[Keys.PageGroupingAttributeGroup] = "Group";
		}

		// Method to fullfill IServiceFactory
		public object Get()
		{
			return this;
		}

		// Methods for IPropertyProvider
		public event SupersededEventHandler Superseded;

		public string this[ string key ]
		{
			get { return properties[key].ToString(); }
		}

		/// <summary>
		/// Dummy Read only Indexer property that also takes a partner ID. 		
		/// Since this method was added to the interface and 
		/// hence every class which is implementing  IPropertyProvider interface must implement this method.
		/// </summary>
		public string this [string key, int partnerID]
		{
			get {return string.Empty;}
		}

		public bool IsSuperseded
		{
			get { return false; }
		}

		public int Version
		{
			get { return 0; }
		}

		public string GroupID
		{
			get { return ""; }
		}
		
		public string ApplicationID
		{
			get { return ""; }
		}

		public void Supersede()
		{
			Superseded(this, new EventArgs());
		}
	}

	public class TestMockPropertiesPageTransferDetailsInvalidXml : 
		IServiceFactory, IPropertyProvider
	{
		Hashtable properties;
		
		public TestMockPropertiesPageTransferDetailsInvalidXml()
		{
			properties = new Hashtable();
			
			// filenames
			properties[Keys.PageTransferDetailsFilePath] =
				Directory.GetCurrentDirectory() + @"\PageTransferDetailsInvalidXml.xml";
			properties[Keys.PageTransitionEventFilePath] =
				Directory.GetCurrentDirectory() + @"\PageTransitionEvent.xml";
            properties[Keys.PageGroupingFilePath] =
                Directory.GetCurrentDirectory() + @"\PageGrouping.xml";

            properties[Keys.PageTransferDetailsFileXsd] =
                Directory.GetCurrentDirectory() + @"\PageTransferDetails.xsd";
            properties[Keys.PageTransitionEventFileXsd] =
                Directory.GetCurrentDirectory() + @"\PageTransitionEvent.xsd";
            properties[Keys.PageGroupingFileXsd] =
                Directory.GetCurrentDirectory() + @"\PageGrouping.xsd";

			// Xml Node and Attribute Names
			properties[Keys.PageTransferDetailsNodeRoot] = "PageTransferDetails";
			properties[Keys.PageTransferDetailsNodePage] = "page";
			properties[Keys.PageTransferDetailsAttributePageId] = "PageId";
			properties[Keys.PageTransferDetailsAttributePageUrl] = "PageURL";
			properties[Keys.PageTransferDetailsAttributeBookmarkRedirect] = "BookmarkRedirect";
			properties[Keys.PageTransferDetailsAttributeSpecificStateClass] = "SpecificStateClass";
		    properties[Keys.PageTransferDetailsAttributeBookmarkValidJourneyRedirect] = "BookmarkValidJourneyRedirect";

            properties[Keys.PageTransitionEventNodeRoot] = "PageTransitionEvents";
			properties[Keys.PageTransitionEventNodePage] = "page";
			properties[Keys.PageTransitionEventAttributePageId] = "PageId";
			properties[Keys.PageTransitionEventAttributeTransitionEvent] = "TransitionEvent";

            properties[Keys.PageGroupingNodeRoot] = "PageGrouping";
            properties[Keys.PageGroupingNodePage] = "page";
            properties[Keys.PageGroupingAttributePageId] = "PageId";
            properties[Keys.PageGroupingAttributeGroup] = "Group";
		}

		// Method to fullfill IServiceFactory
		public object Get()
		{
			return this;
		}

		// Methods for IPropertyProvider
		public event SupersededEventHandler Superseded;

		public string this[ string key ]
		{
			get { return properties[key].ToString(); }
		}

		/// <summary>
		/// Dummy Read only Indexer property that also takes a partner ID. 		
		/// Since this method was added to the interface and 
		/// hence every class which is implementing  IPropertyProvider interface must implement this method.
		/// </summary>
		public string this [string key, int partnerID]
		{
			get {return string.Empty;}
		}

		public bool IsSuperseded
		{
			get { return false; }
		}

		public int Version
		{
			get { return 0; }
		}

		public string GroupID
		{
			get { return ""; }
		}
		
		public string ApplicationID
		{
			get { return ""; }
		}

		public void Supersede()
		{
			Superseded(this, new EventArgs());
		}
	}

	public class TestMockPropertiesPageTransitionEventInvalidXml : 
		IServiceFactory, IPropertyProvider
	{
		Hashtable properties;
		
		public TestMockPropertiesPageTransitionEventInvalidXml()
		{
			properties = new Hashtable();
			
			// filenames
			properties[Keys.PageTransferDetailsFilePath] =
				Directory.GetCurrentDirectory() + @"\PageTransferDetails.xml";
			properties[Keys.PageTransitionEventFilePath] =
				Directory.GetCurrentDirectory() + @"\PageTransitionEventInvalidXml.xml";
            properties[Keys.PageGroupingFilePath] =
                Directory.GetCurrentDirectory() + @"\PageGrouping.xml";

            properties[Keys.PageTransferDetailsFileXsd] =
                Directory.GetCurrentDirectory() + @"\PageTransferDetails.xsd";
            properties[Keys.PageTransitionEventFileXsd] =
                Directory.GetCurrentDirectory() + @"\PageTransitionEvent.xsd";
            properties[Keys.PageGroupingFileXsd] =
                Directory.GetCurrentDirectory() + @"\PageGrouping.xsd";

			// Xml Node and Attribute Names
			properties[Keys.PageTransferDetailsNodeRoot] = "PageTransferDetails";
			properties[Keys.PageTransferDetailsNodePage] = "page";
			properties[Keys.PageTransferDetailsAttributePageId] = "PageId";
			properties[Keys.PageTransferDetailsAttributePageUrl] = "PageURL";
			properties[Keys.PageTransferDetailsAttributeBookmarkRedirect] = "BookmarkRedirect";
			properties[Keys.PageTransferDetailsAttributeSpecificStateClass] = "SpecificStateClass";
			properties[Keys.PageTransferDetailsAttributeBookmarkValidJourneyRedirect] = "BookmarkValidJourneyRedirect";

            properties[Keys.PageTransitionEventNodeRoot] = "PageTransitionEvents";
			properties[Keys.PageTransitionEventNodePage] = "page";
			properties[Keys.PageTransitionEventAttributePageId] = "PageId";
			properties[Keys.PageTransitionEventAttributeTransitionEvent] = "TransitionEvent";

            properties[Keys.PageGroupingNodeRoot] = "PageGrouping";
            properties[Keys.PageGroupingNodePage] = "page";
            properties[Keys.PageGroupingAttributePageId] = "PageId";
            properties[Keys.PageGroupingAttributeGroup] = "Group";
		}

		// Method to fullfill IServiceFactory
		public object Get()
		{
			return this;
		}

		// Methods for IPropertyProvider
		public event SupersededEventHandler Superseded;

		public string this[ string key ]
		{
			get { return properties[key].ToString(); }
		}

		/// <summary>
		/// Dummy Read only Indexer property that also takes a partner ID. 		
		/// Since this method was added to the interface and 
		/// hence every class which is implementing  IPropertyProvider interface must implement this method.
		/// </summary>
		public string this [string key, int partnerID]
		{
			get {return string.Empty;}
		}

		public bool IsSuperseded
		{
			get { return false; }
		}

		public int Version
		{
			get { return 0; }
		}

		public string GroupID
		{
			get { return ""; }
		}
		
		public string ApplicationID
		{
			get { return ""; }
		}

		public void Supersede()
		{
			Superseded(this, new EventArgs());
		}
	}

}
