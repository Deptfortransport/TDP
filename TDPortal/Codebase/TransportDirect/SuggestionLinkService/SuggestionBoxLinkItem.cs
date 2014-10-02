// *********************************************** 
// NAME                 : SuggestionBoxLinkItem.cs
// AUTHOR               : Ken Josling
// DATE CREATED         : 15/08/2005 
// DESCRIPTION			: Represents a link that will be used in the Suggestion Box user control					 
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/SuggestionLinkService/SuggestionBoxLinkItem.cs-arc  $
//
//   Rev 1.2   Apr 03 2008 12:18:28   pscott
//SCR 4632 Link id errors reported in WebLogs
//
//Added if statement to only issue warnings when an internal link.
//
//   Rev 1.1   Mar 10 2008 15:27:58   mturner
//Initial Del10 Codebase from Dev Factory
//
//   Rev 1.0   Nov 08 2007 12:50:10   mturner
//Initial revision.
//
//   Rev 1.4   Sep 06 2007 18:16:04   mmodi
//Add IsExternal link property
//Resolution for 4493: Car journey details: Screen reader improvements
//
//   Rev 1.3   Feb 10 2006 15:04:38   build
//Automatically merged from branch for stream3180
//
//   Rev 1.2.1.0   Dec 13 2005 12:18:46   NMoorhouse
//Updated for HomePage Expandable Menu (check-in on Rob Griffith's behalf)
//Resolution for 3180: DEL 8.1 Stream UEE: work on Homepage phase 2
//
//   Rev 1.2   Sep 02 2005 16:08:30   kjosling
//Updated following code review
//
//   Rev 1.1   Sep 02 2005 15:32:44   kjosling
//Updated following code review
//
//   Rev 1.0   Aug 24 2005 16:44:54   kjosling
//Initial revision.

using System;
using System.Collections;
using System.Diagnostics;
//using TransportDirect.Web.Support;
using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.DatabaseInfrastructure.Content;

namespace TransportDirect.UserPortal.SuggestionLinkService
{
	/// <summary>
	/// Represents a link that will be used in the Suggestion Box user control
	/// </summary>
	public class SuggestionBoxLinkItem : IComparable
	{
		#region Private Properties

		private int suggestionLinkId;
		private short categoryPriority;
		private string category;
		private short linkPriority;
		private bool isRoot;
		private Hashtable linkItems;
		private ILinkDetails linkDetails;
		private bool isExternal;
        private bool isSubRootLink;
        private int subRootLinkId;
        private int themeId;
		#endregion
		
		#region Constructor
		
		/// <summary>
		/// Creates a new SuggestionBoxLinkItem instance 
		/// </summary>
		/// <param name="suggestionLinkId">The unique identifier for this SuggestionBoxLinkItem</param>
		/// <param name="categoryPriority">The category priority</param>
		/// <param name="category">The category full name</param>
		/// <param name="linkPriority">The link priority</param>
		/// <param name="linkItems">A Hashtable containing a series of LinkTextItems, representing the link
		/// text in a number of supported languages</param>
		/// <param name="linkDetails">An ILinkDetails object containing information about the URL</param>
		public SuggestionBoxLinkItem(	int suggestionLinkId,
										short categoryPriority,
										string category,
										short linkPriority,
										bool isRoot,
										Hashtable linkItems,
										ILinkDetails linkDetails,
										bool isExternal,
                                        bool isSubRootLink,
                                        int subRootLinkId,
                                        int themeId)
		{
			this.suggestionLinkId = suggestionLinkId;
			this.categoryPriority = categoryPriority;
			this.category = category;
			this.linkPriority = linkPriority;
			this.isRoot = isRoot;
			this.linkItems = linkItems;
			this.linkDetails = linkDetails;
			this.isExternal = isExternal;
            this.isSubRootLink = isSubRootLink;
            this.subRootLinkId = subRootLinkId;
            this.themeId = themeId;
		}

		#endregion

		#region Public Properties

		/// <summary>
		/// (Read-only) Returns the unique identifier for this SuggestionBoxLinkItem
		/// </summary>
		public int SuggestionLinkId
		{
			get{	return suggestionLinkId;	}
		
		}

		/// <summary>
		/// (Read-only) Returns an short value that determines the order in which this SuggestionBoxLinkItem's category
		/// will appear
		/// </summary>
		public short CategoryPriority
		{
			get{	return categoryPriority;	}
		}

		/// <summary>
		/// (Read-only) Returns the name of the category to which this SuggestionBoxLinkItem belongs
		/// </summary>
		public string Category
		{
			get{	return category;	}
		}

		/// <summary>
		/// (Read-only) Returns an short value that determines the order in which this link will appear
		/// for a given category
		/// </summary>
		public short LinkPriority
		{
			get{	return linkPriority;	}
		}

		/// <summary>
		/// (Read-only) Returns a boolean to determine wether the link is a root category link or 
		/// a subcategory link.
		/// </summary>
		public bool IsRoot
		{
			get{	return isRoot;	}
		}

		/// <summary>
		/// (Read-only) Returns a boolean to determine wether the link is an external link
		/// </summary>
		public bool IsExternal
		{
			get{	return isExternal; }
		}

        /// <summary>
        /// (Read-only) Returns a boolean to determine wether the link is a sub root category link or 
        /// a subcategory link.
        /// </summary>
        public bool IsSubRootLink
        {
            get { return isSubRootLink; }
        }

        /// <summary>
        /// (Read-only) Returns a integer to determine  the link which is a sub root category link for 
        /// a level 2(subcategory of a subcategory) subcategory link.  This will be 0 for root category
        /// and level 1 subcategory.
        /// </summary>
        public int SubRootLinkId
        {
            get { return subRootLinkId; }
        }

        /// <summary>
        /// (Read-only) Returns a integer to determine  the theme id of the link 
        /// </summary>
        public int ThemeId
        {
            get { return themeId; }
        }

        #endregion
	
		#region Public Methods

		/// <summary>
		/// Returns the formatted link text based on the current culture
		/// </summary>
		/// <returns>The formatted link text based on the current culture. If the culture is 
		/// not currently supported, this method will return an empty string</returns>
		public string GetLinkText()
		{
            string strKey = CurrentLanguage.Culture;  
			if(linkItems.ContainsKey(strKey))
			{
				LinkTextItem item = (LinkTextItem)linkItems[strKey];
				return item.GetSubstitutedLinkText();
			}
			//We do not bomb out here because the parent Suggestion Box may contain other valid SuggestionBoxLinkItems
			//so we log a message and return an empty string

            // but dont issue warning if it is an external link as specified culure is irrelevent to outside sites
            if (!this.isExternal)
            {
                string warningMessage =
                    "The specified culture '" + strKey + "' is not supported for link ID: '" + suggestionLinkId.ToString() + "'";

                OperationalEvent oe = new OperationalEvent(TDEventCategory.Database,
                    TDTraceLevel.Warning, warningMessage);

                Trace.Write(oe);
            }
			return String.Empty;
		}

		/// <summary>
		/// IComparable member. Orders SuggestionBoxLinkItems based first on categoryPriority
		/// and then on linkPriority
		/// </summary>
		/// <param name="otherObject"></param>
		/// <returns></returns>
		public int CompareTo(object otherObject)
		{
			SuggestionBoxLinkItem target = otherObject as SuggestionBoxLinkItem;
			if(target != null)
			{		
				if(target.categoryPriority != categoryPriority) 
				{
					return categoryPriority.CompareTo(target.categoryPriority);
				}
				return linkPriority.CompareTo(target.linkPriority);
			}
			throw new ArgumentException("Cannot compare SuggestionBoxLinkItem to null");
		}

		/// <summary>
		/// Compares two SuggestionBoxLinkItem to determine if they are equal. The check is
		/// made against the suggestionLinkID property, which uniquely identifies a 
		/// SuggestionBoxLinkItem
		/// </summary>
		/// <param name="otherObject"></param>
		/// <returns>true if the objects are the same, false otherwise</returns>
		public override bool Equals(object otherObject)
		{
			//Check for null and compare run-time types
			if (otherObject == null || GetType() != otherObject.GetType()) return false;
			SuggestionBoxLinkItem target = (SuggestionBoxLinkItem)otherObject;
			return(target.suggestionLinkId == suggestionLinkId);
		}

		/// <summary>
		/// Helper method to support overriden Equals method
		/// </summary>
		/// <returns>and integer hashcode</returns>
		public override int GetHashCode() 
		{
			return base.GetHashCode();
		}

		/// <summary>
		/// Returns the full URL for the link
		/// </summary>
		/// <param name="baseURL">For internal links, a base URL is supplied</param>
		/// <returns>The full URL for the link</returns>
		public string GetUrl(string baseUrl)
		{
			if(linkDetails.GetType() == typeof(InternalLinkDetail))
			{
				if (isExternal)
					return linkDetails.Url;
				else
					return baseUrl + linkDetails.Url;
			}
			else
			{
				return linkDetails.Url;
			}
		}


		#endregion
	
	}
}
