// *********************************************** 
// NAME                 : Keys.cs 
// AUTHOR               : Kenny Cheung
// DATE CREATED         : 16/07/2003 
// DESCRIPTION  : Defines all the keys used by
// ScreenFlow to access the properties service with.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ScreenFlow/Classes/Keys.cs-arc  $ 
//
//   Rev 1.2   May 06 2008 16:06:28   mmodi
//Added xsd paths
//Resolution for 4888: Del 10.1 - Improve Session Timeout Management
//
//   Rev 1.1   May 01 2008 17:07:34   mmodi
//Updated for PageGrouping.xml
//Resolution for 4888: Del 10.1 - Improve Session Timeout Management
//
//   Rev 1.0   Nov 08 2007 12:47:46   mturner
//Initial revision.
//
//   Rev 1.8   Jul 15 2004 09:50:36   jgeorge
//Removal of JourneyParametersType from screenflow
//
//   Rev 1.7   Jun 29 2004 13:45:16   esevern
//added JourneyParameterType attribute required for additional safe page redirection processing
//
//   Rev 1.6   May 20 2004 14:36:36   rgreenwood
//Back Button Enhancement:
//Added new key for BookmarkValidJourneyRedirect
//
//   Rev 1.5   Apr 23 2004 14:36:38   acaunt
//CSL Compliant attribute removed so that the Assembly can be marked as non CLS compliant. (All projects are to have an identical AssemblyInfo.cs file).
//
//   Rev 1.4   Sep 16 2003 14:29:56   jcotton
//Removed redundant, commented out code.
//
//   Rev 1.3   Aug 15 2003 14:36:48   passuied
//Update after design change
//
//   Rev 1.2   Aug 07 2003 13:54:08   kcheung
//Set CLSComplaint to true
//
//   Rev 1.1   Jul 23 2003 12:28:24   kcheung
//Updated after code review comments

using System;

namespace TransportDirect.UserPortal.ScreenFlow
{
	/// <summary>
	/// Keys for all properties used.
	/// </summary>
	public class Keys
	{
		public const string ScreenFlow = "ScreenFlow";
		public const string PageTransferDetails = ScreenFlow + ".PageTransferDetails";
		public const string PageTransitionEvent = ScreenFlow + ".PageTransitionEvent";
        public const string PageGrouping = ScreenFlow + ".PageGrouping";
		public const string Path = "Path";
        public const string Xsd = "Xsd";

        // Xml files
		public const string PageTransferDetailsFilePath =
			PageTransferDetails + "." + Path;

		public const string PageTransitionEventFilePath =
			PageTransitionEvent + "." + Path;

        public const string PageGroupingFilePath =
            PageGrouping + "." + Path;

        // Xsd files
        public const string PageTransferDetailsFileXsd =
            PageTransferDetails + "." + Xsd;

        public const string PageTransitionEventFileXsd =
            PageTransitionEvent + "." + Xsd;

        public const string PageGroupingFileXsd =
            PageGrouping + "." + Xsd;

		// Xml node names
		public const string Xml = "Xml";
		public const string Node = "Node";
		public const string XmlAttribute = "Attribute";

        
        // Page Transfer
		public const string PageTransferDetailsNodeRoot =
			PageTransferDetails + "." + Xml + "." + Node + "." + "Root";

		public const string PageTransferDetailsNodePage =
			PageTransferDetails + "." + Xml + "." + Node + "." + "Page";

		public const string PageTransferDetailsAttributePageId =
			PageTransferDetails + "." + Xml + "." + XmlAttribute + "." + "PageId";

		public const string PageTransferDetailsAttributePageUrl =
			PageTransferDetails + "." + Xml + "." + XmlAttribute + "." + "PageURL";

		public const string PageTransferDetailsAttributeBookmarkRedirect =
			PageTransferDetails + "." + Xml + "." + XmlAttribute + "." + "BookmarkRedirect";

		public const string PageTransferDetailsAttributeSpecificStateClass =
			PageTransferDetails + "." + Xml + "." + XmlAttribute + "." + "SpecificStateClass";

		public const string PageTransferDetailsAttributeBookmarkValidJourneyRedirect =
			PageTransferDetails + "." + Xml + "." + XmlAttribute + "." + "BookmarkValidJourneyRedirect";

        
        // Transition Event
		public const string PageTransitionEventNodeRoot =
			PageTransitionEvent + "." + Xml + "." + Node + "." + "Root";

		public const string PageTransitionEventNodePage =
			PageTransitionEvent + "." + Xml + "." + Node + "." + "Page";

		public const string PageTransitionEventAttributeTransitionEvent =
			PageTransitionEvent + "." + Xml + "." + XmlAttribute + "." + "TransitionEvent";

		public const string PageTransitionEventAttributePageId =
			PageTransitionEvent + "." + Xml + "." + XmlAttribute + "." + "PageId";


        // Groupings
        public const string PageGroupingNodeRoot =
            PageGrouping + "." + Xml + "." + Node + "." + "Root";

        public const string PageGroupingNodePage =
            PageGrouping + "." + Xml + "." + Node + "." + "Page";

        public const string PageGroupingAttributeGroup =
            PageGrouping + "." + Xml + "." + XmlAttribute + "." + "Group";

        public const string PageGroupingAttributePageId =
            PageGrouping + "." + Xml + "." + XmlAttribute + "." + "PageId";
	} // Keys
}
