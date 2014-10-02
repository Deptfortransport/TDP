// *********************************************** 
// NAME                 : SuggestionBoxLinkCatalogue.cs
// AUTHOR               : Ken Josling
// DATE CREATED         : 15/08/2005 
// DESCRIPTION			: This class contains the reference data that will be used by the Suggestion
//						  box control. 
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/SuggestionLinkService/SuggestionBoxLinkCatalogue.cs-arc  $
//
//   Rev 1.2   Mar 20 2008 10:17:54   mturner
//Del10 patch1 from Dev factory
//
//   Rev 1.0   Nov 08 2007 12:50:10   mturner
//Initial revision.
//
//   Rev 1.6   Sep 06 2007 18:16:32   mmodi
//Call to retrieve External suggestion links
//Resolution for 4493: Car journey details: Screen reader improvements
//
//   Rev 1.5   Apr 19 2006 17:42:20   AViitanen
//Added check to only offload the links for the current suggestionlink if previouscontext is not equal to String.Empty. 
//Resolution for 3945: DN058 Park and Ride: incorrect suggestion links on Plan to Park and Ride input page
//
//   Rev 1.4   Mar 29 2006 18:18:14   AViitanen
//Extra condition checks for consecutive link ids across contexts in the results set. 
//Resolution for 3621: Homepage phase 2: Remove Departure Board + FAQ links from Related Links
//
//   Rev 1.3   Feb 10 2006 15:04:38   build
//Automatically merged from branch for stream3180
//
//   Rev 1.2.1.1   Dec 14 2005 16:20:30   RGriffith
//Added comments
//
//   Rev 1.2.1.0   Dec 13 2005 12:18:46   NMoorhouse
//Updated for HomePage Expandable Menu (check-in on Rob Griffith's behalf)
//Resolution for 3180: DEL 8.1 Stream UEE: work on Homepage phase 2
//
//   Rev 1.2   Sep 02 2005 15:32:28   kjosling
//Updated following code review
//
//   Rev 1.1   Aug 31 2005 18:13:34   kjosling
//Updated following code review
//
//   Rev 1.0   Aug 24 2005 16:44:52   kjosling
//Initial revision.

using System;
using System.Collections;
using System.Diagnostics;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common; 
using TransportDirect.Common.Logging;
using TransportDirect.Common.DatabaseInfrastructure;
using TransportDirect.UserPortal.DataServices;
using System.Data.SqlClient;
using TransportDirect.Common.DatabaseInfrastructure.Content;
using TD.ThemeInfrastructure;

namespace TransportDirect.UserPortal.SuggestionLinkService
{
	/// <summary>
	/// Summary description for SuggestionBoxLinkCatalogue.
	/// </summary>
	public class SuggestionBoxLinkCatalogue : IComparer
	{
		#region Private Properties

		private Hashtable linksByContext = new Hashtable();
		//Properties used for loading the reference data
		private Hashtable linkItemsCurrentLoad = new Hashtable();
		private ArrayList suggestionLinkItemsCurrentLoad = new ArrayList();

		//private integer variables to track column indexes
		private int contextNameColumnOrdinal;
		private int	suggestionLinkIdColumnOrdinal;
		private int	categoryPriorityColumnOrdinal;
		private int	categoryNameColumnOrdinal;
		private int	linkPriorityColumnOrdinal;
		private int	relativeUrlColumnOrdinal;
		private int	cultureColumnOrdinal;
		private int	resourceStringColumnOrdinal;
		private int isRootBitColumnOrdinal;
        private int isSubRootLinkBitColumnOrdinal;
        private int subRootLinkIdColumnOrdinal;
        private int themeIdColumnOrdinal;

		#endregion

		#region Constructor

		public SuggestionBoxLinkCatalogue()
		{
			Load();
		}

		#endregion

		#region Private Methods

		/// <summary>
		/// Connects to the TransientPortal database and retrieves the reference data, which
		/// is loaded into memory at runtime
		/// </summary>
		private void Load()
		{
			SqlHelper helper = new SqlHelper();

			try
			{
                helper.ConnOpen(SqlHelperDatabase.TransientPortalDB);

                //Retrieve the reference data into a flat table of results
				SqlDataReader reader = helper.GetReader("GetSuggestionLinkData");
	
				//Assign the column ordinals
				contextNameColumnOrdinal		= reader.GetOrdinal("ContextName");
				suggestionLinkIdColumnOrdinal	= reader.GetOrdinal("SuggestionLinkID");
				categoryPriorityColumnOrdinal	= reader.GetOrdinal("CategoryPriority");
				categoryNameColumnOrdinal		= reader.GetOrdinal("CategoryName");
				linkPriorityColumnOrdinal		= reader.GetOrdinal("LinkPriority");
				relativeUrlColumnOrdinal		= reader.GetOrdinal("RelativeURL");
				cultureColumnOrdinal			= reader.GetOrdinal("Culture");
				resourceStringColumnOrdinal		= reader.GetOrdinal("ResourceString");
				isRootBitColumnOrdinal			= reader.GetOrdinal("IsRoot");
                isSubRootLinkBitColumnOrdinal       = reader.GetOrdinal("IsSubRootLink");
                subRootLinkIdColumnOrdinal = reader.GetOrdinal("SubRootLinkId");
                themeIdColumnOrdinal = reader.GetOrdinal("ThemeId");
                					
				while (reader.Read())
				{
					//Validate the context by name using the Context enumeration
					string nextContext = reader.GetString(contextNameColumnOrdinal);
					if(Enum.IsDefined(typeof(Context), nextContext))
					{
						ProcessRecord(reader, false);
					}
					else
					{
						string exceptionMessage = 
							"Invalid context '" + nextContext + "' is not defined in the Context enumeration";
						TDException td = new TDException( exceptionMessage, false, TDExceptionIdentifier.SLSUnhandledSubstitutionParameter );
						throw td;
					}
				}
				//Now that the reader is EOF, we can save the remaining reference data
				if(linkItemsCurrentLoad.Count > 0)
				{
					AddNewSuggestionBoxItem();
				}
				if(suggestionLinkItemsCurrentLoad.Count > 0)
				{
					AddNewContext();
				}
				reader.Close();

				#region External links

				//Retrieve the reference data into a flat table of results
				SqlDataReader readerExternal = helper.GetReader("GetExternalSuggestionLinkData", new Hashtable(), new Hashtable());
	
				//Assign the column ordinals
				contextNameColumnOrdinal		= readerExternal.GetOrdinal("ContextName");
				suggestionLinkIdColumnOrdinal	= readerExternal.GetOrdinal("SuggestionLinkID");
				categoryPriorityColumnOrdinal	= readerExternal.GetOrdinal("CategoryPriority");
				categoryNameColumnOrdinal		= readerExternal.GetOrdinal("CategoryName");
				linkPriorityColumnOrdinal		= readerExternal.GetOrdinal("LinkPriority");
				relativeUrlColumnOrdinal		= readerExternal.GetOrdinal("URL");
				cultureColumnOrdinal			= readerExternal.GetOrdinal("Culture");
				resourceStringColumnOrdinal		= readerExternal.GetOrdinal("ResourceString");
				isRootBitColumnOrdinal			= readerExternal.GetOrdinal("IsRoot");
                isSubRootLinkBitColumnOrdinal = readerExternal.GetOrdinal("IsSubRootLink");
                subRootLinkIdColumnOrdinal = readerExternal.GetOrdinal("SubRootLinkId");

				while (readerExternal.Read())
				{
					//Validate the context by name using the Context enumeration
					string nextContext = readerExternal.GetString(contextNameColumnOrdinal);
					if(Enum.IsDefined(typeof(Context), nextContext))
					{
						ProcessRecord(readerExternal, true);
					}
					else
					{
						string exceptionMessage = 
							"Invalid context '" + nextContext + "' is not defined in the Context enumeration";
						TDException td = new TDException( exceptionMessage, false, TDExceptionIdentifier.SLSUnhandledSubstitutionParameter );
						throw td;
					}
				}
				//Now that the reader is EOF, we can save the remaining reference data
				if(linkItemsCurrentLoad.Count > 0)
				{
					AddNewSuggestionBoxItem();
				}
				if(suggestionLinkItemsCurrentLoad.Count > 0)
				{
					AddNewContext();
				}
				readerExternal.Close();

				#endregion
			}
			finally
			{
				if (helper.ConnIsOpen)
					helper.ConnClose();
			}

		}

		#endregion

		#region Public Methods

        /// <summary>
        /// Returns reference data for a Suggestion Box based on a specified list of contexts
        /// </summary>
        /// <param name="context">An array of Context enumerable types, representing a list of contexts
        /// that can be used with the Suggestion Box</param>
        /// <returns>An array of SuggestionBoxLinkItems representing the links that are valid for the
        /// specified context(s)</returns>
        public SuggestionBoxLinkItem[] GetUniqueLinkByContext(Context[] context)
        {
            Theme theme = ThemeProvider.Instance.GetDefaultTheme();

            return GetUniqueLinkByContext(context, theme.Id);
        }

		/// <summary>
		/// Returns reference data for a Suggestion Box based on a specified list of contexts
		/// </summary>
		/// <param name="context">An array of Context enumerable types, representing a list of contexts
		/// that can be used with the Suggestion Box</param>
        /// <param name="themeId">ThemeId</param>
		/// <returns>An array of SuggestionBoxLinkItems representing the links that are valid for the
		/// specified context(s)</returns>
		public SuggestionBoxLinkItem[] GetUniqueLinkByContext(Context[] context, int themeId)
		{
			if(context.Length == 0) return new SuggestionBoxLinkItem[] {};

            // check theme id, if not valid use default theme
            Theme theme = ThemeProvider.Instance.GetDefaultTheme();
            if (themeId < 1)
                themeId = theme.Id;

			ArrayList validLinks = new ArrayList();
			ArrayList currentItems;
			string currentContext;

			foreach(Context con in context)
			{
				currentContext = con.ToString();
				if(linksByContext.ContainsKey(currentContext))
				{
					currentItems = (ArrayList)linksByContext[currentContext];
					foreach(SuggestionBoxLinkItem item in currentItems)
					{
						if((!validLinks.Contains(item)) && (!string.IsNullOrEmpty(item.GetLinkText())))
						{
                            if (item.ThemeId == themeId)
                            {
                                validLinks.Add(item);
                            }
						}
					}
				}
				else
				{
					//We have no link data for the specified context. Log this and move on
					string warningMessage = "No valid links were found for context '" + currentContext + "'";
					OperationalEvent oe = new OperationalEvent(TDEventCategory.Business,
						TDTraceLevel.Warning, warningMessage);
					Trace.Write(oe);
				}
			}
			validLinks.Sort();
			return (SuggestionBoxLinkItem[])validLinks.ToArray(typeof(SuggestionBoxLinkItem));
		}

		#endregion

		#region Reference Data Load Methods
		
		private int previousLinkID = -1;
		private string previousContext = String.Empty;
		private short previousCategoryPriority;
		private string previousCategoryName;
		private string isRootInt;
		private bool isRoot;
		private short previousLinkPriority;
		private string previousRelativeURL;
		private bool isExternal;
        private bool isSubRootLink;
        private string isSubRootLinkInt;
        private int subRootLinkId;
        private int previousThemeId = -1;

		/// <summary>
		/// Offloads the database reference data into a memory resident data structure that can
		/// be queried by suggestion boxes at runtime. 
		/// </summary>
		/// <param name="reader">The SQL DataReader containing the database results</param>
		private void ProcessRecord(SqlDataReader reader, bool external)
		{
			isExternal = external;

			int linkID = reader.GetInt32(suggestionLinkIdColumnOrdinal);
			if(previousLinkID == -1) 
			{
				previousLinkID = linkID;
			}

            int themeId = reader.GetInt32(themeIdColumnOrdinal);
            if (previousThemeId == -1)
            {
                previousThemeId = themeId;
            }

			LinkTextItem newLinkTextItem = new LinkTextItem(reader.GetString(resourceStringColumnOrdinal));
				
			string contextID = reader.GetString(contextNameColumnOrdinal);

            // Processing: 
            // A link is deemed to be New if it has a different Context, Theme, and LinkId.
            // As the stored procedure returns a unique row for each physical link sorted by Context, Theme, 
            // and Culture, we need to create a suggestion link populating the different cultures, 
            // and then only add it to the main array once the LinkId, Context, or Theme changes.
            // The use of previous and current values allows us to track the suggestion link being worked on
            // through the records returned by the stored proc.

			if((linkID != previousLinkID) || 
				// Extra condition checks for consecutive link ids across contexts in the results set
				((linkID == previousLinkID) && (contextID != previousContext))
                ||
                // Extra condition check for consecutive theme ids across contexts and links in the results set
                ((linkID == previousLinkID) && (contextID == previousContext) && (themeId != previousThemeId)))
			{	
                //Its a brand new link, so we need to create a new SuggestionBoxLinkItem
				if(previousContext != String.Empty)
				{
					AddNewSuggestionBoxItem();
				}
				previousLinkID = linkID;
				
				if(contextID != previousContext && previousContext.Length > 0)
				{	
                    //Its a brand new context,lets add the links for this context to the ref data
					AddNewContext();
					previousContext = contextID;
				}
				if(previousContext.Length == 0)
				{
					previousContext = contextID;
				}

                // The same link but for a different Theme has been created, therefore set previous link to
                // ensure we don't enter this condition section until we have a brand new link (i.e linkId/ContextId/themeId changes)
                if ((contextID == previousContext) && (themeId != previousThemeId))
                {
                    previousThemeId = themeId;
                }
			}

            if (!linkItemsCurrentLoad.ContainsKey(reader.GetString(cultureColumnOrdinal)))
            {
                linkItemsCurrentLoad.Add(reader.GetString(cultureColumnOrdinal), newLinkTextItem);
            }

			// Set previousRelativeUrl to empty string if database value is null else read as normal
			if (reader.IsDBNull(relativeUrlColumnOrdinal))
			{
				previousRelativeURL = string.Empty;
			}
			else
			{
				previousRelativeURL = reader.GetString(relativeUrlColumnOrdinal);
			}

			// Determine Boolean value of isRoot
			isRootInt = reader.GetValue(isRootBitColumnOrdinal).ToString();
			if (isRootInt == "True")
				isRoot = true;
			else
				isRoot = false;

            // Determine Boolean value of isSubRoot
            isSubRootLinkInt = reader.GetValue(isSubRootLinkBitColumnOrdinal).ToString();
            if (isSubRootLinkInt == "True")
                isSubRootLink = true;
            else
                isSubRootLink = false;

            subRootLinkId = reader.GetInt32(subRootLinkIdColumnOrdinal);

			previousLinkPriority = reader.GetInt16(linkPriorityColumnOrdinal);
			previousCategoryName = reader.GetString(categoryNameColumnOrdinal);
			previousCategoryPriority = reader.GetInt16(categoryPriorityColumnOrdinal);
		}

		/// <summary>
		/// Helper method. Adds a new SuggestionBoxItem to the suggestionLinkItemsCurrentLoad ArrayList
		/// </summary>
		private void AddNewSuggestionBoxItem()
		{
			ILinkDetails newLinkDetail = new InternalLinkDetail(previousRelativeURL, true);
				
			suggestionLinkItemsCurrentLoad.Add(
				new SuggestionBoxLinkItem(previousLinkID, previousCategoryPriority, previousCategoryName,
				previousLinkPriority, isRoot, (Hashtable)linkItemsCurrentLoad.Clone(), newLinkDetail, isExternal,isSubRootLink,subRootLinkId, previousThemeId));
			//Clear the buffer of LinkTextItems
			linkItemsCurrentLoad.Clear();
		}

		/// <summary>
		/// Helper method. Adds a new Context to the linksByContext Hashtable
		/// </summary>
		private void AddNewContext()
		{
			if (!linksByContext.ContainsKey(previousContext))
			{
				linksByContext.Add(previousContext, (ArrayList)suggestionLinkItemsCurrentLoad.Clone());
			}
			else // The context already exists, so add to that
			{
				ArrayList suggestionLinks = new ArrayList();
				IDictionaryEnumerator myEnumerator = linksByContext.GetEnumerator();
				while ( myEnumerator.MoveNext() )
				{
					if ((string)myEnumerator.Key == previousContext)
					{
						suggestionLinks = (ArrayList)myEnumerator.Value;
					}
				}
				
				foreach (object obj in suggestionLinks)
				{
					suggestionLinkItemsCurrentLoad.Add(obj);
				}

				suggestionLinkItemsCurrentLoad.Sort(this);

				linksByContext.Remove(previousContext);

				linksByContext.Add(previousContext, (ArrayList)suggestionLinkItemsCurrentLoad.Clone());
			}
			//Clear the buffer of SuggestionBoxLinkItems
			suggestionLinkItemsCurrentLoad.Clear();
		}

		#endregion

		#region IComparer Members

		public int Compare(object x, object y)
		{
			SuggestionBoxLinkItem xx = (SuggestionBoxLinkItem)x;
			SuggestionBoxLinkItem yy = (SuggestionBoxLinkItem)y;
			if (xx.LinkPriority < yy.LinkPriority)
				return 0;
			else
				return 1;
		}

		#endregion
	}
}
