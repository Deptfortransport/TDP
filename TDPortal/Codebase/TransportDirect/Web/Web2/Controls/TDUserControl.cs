//******************************************************************************
//NAME			: TDUserControl.cs
//AUTHOR		: Joe Morrissey
//DATE CREATED	: 11/07/2003
//DESCRIPTION	: A base user control class derived from System.Web.UI.UserControl, 
//all other user controls to inherit from this class. This provides a single place 
//where behaviour can be altered for all user controls
//******************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/TDUserControl.cs-arc  $
//
//   Rev 1.2   Mar 31 2008 13:23:16   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:18:10   mturner
//Initial revision.
//
//   Rev 1.10   Feb 23 2006 16:14:02   halkatib
//Merge of stream3129 enhanced exposed services
//
//   Rev 1.9   Jan 30 2006 16:41:02   aviitanen
//Use local resource manager of control rather than resource manager of parent.
//Resolution for 3515: Del 8 Welsh Text:  Various welsh text issues
//
//   Rev 1.8   Nov 01 2005 15:11:42   build
//Automatically merged from branch for stream2638
//
//   Rev 1.7.1.0   Sep 19 2005 10:35:34   jbroome
//Updated for Visit Planner
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.7   Feb 10 2005 09:23:46   COwczarek
//Changes to allow for multiple resource managers
//Resolution for 1921: DEL 7 Development : Find A Fare Ticket Selection
//
//   Rev 1.6   Jul 23 2004 17:39:28   passuied
//FindStation 6.1. Labels and text updates
//
//   Rev 1.5   Jul 23 2004 11:48:22   passuied
//Changes to add GetResource Method in TDPage and TDUserControl to ease access to resources. Also removal of local GetResouce in controls and pages
//
//   Rev 1.4   Jul 09 2004 15:57:02   jmorrissey
//Updated PageID property
//
//   Rev 1.3   Jul 09 2004 12:11:38   jmorrissey
//Added properties PageId and UsesLanguageHandler
//
//   Rev 1.2   Jul 24 2003 12:29:56   JMorrissey
//Added class description documentation comment
//
//   Rev 1.1   Jul 11 2003 11:44:42   JMorrissey
//Updated Log info in header

using System;

using TransportDirect.Common;
using TransportDirect.UserPortal.Web;
using TransportDirect.Web.Support;
using TransportDirect.Common.ResourceManager;

namespace TransportDirect.UserPortal.Web.Controls
{
	/// <summary>	
	///A base user control class that provides a single place 
	///where behaviour can be altered for all user controls that inherit from it.
	/// </summary>
	public class TDUserControl : System.Web.UI.UserControl
	{

		/// <summary>
		/// The resource manager for this user control
		/// </summary>
		protected TDResourceManager resourceManager = Global.tdResourceManager;

		/// <summary>
		/// Sets the local resource manager for the control (but not nested controls). 
		/// Setting this property means that resource strings will be obtained from the local
		/// resource manager and not the global instance (langstrings).
		/// </summary>
		public string LocalResourceManager 
		{
			set 
			{
				resourceManager = TDResourceManager.GetResourceManagerFromCache(value);
			}
		}

		/// <summary>
		/// Gets the resource manager instance that is being used by this control.
		/// </summary>
		public TDResourceManager ResourceManager
		{
			get
			{
				return resourceManager;
			}
		}

		/// <summary>
		/// default constructor
		/// </summary>
		public TDUserControl()
		{
			
		}

		/// <summary>
		/// property to identify the page that contains the user control
		/// </summary>
		public PageId PageId
		{
			get
			{
				//temporary TDPage instance
				TDPage page;
				//check if the control is on a page
				if (this.Page != null)				
				{
					page = (TDPage)this.Page;
					//return the ID enumerator of the page
					return page.PageId;
				}
				else
				{
					//return -1 value to indicate no page exists
					return PageId.Empty;
				}

			 }

		}

		/// <summary>
		/// Method that returns the resource associated with the given key
		/// using the Current UI Culture. This method will first attempt to get the 
		/// resource from the local resource manager if one has been specified.		
		/// </summary>
		/// <param name="key">resource key</param>
		/// <returns>resource string</returns>
		public string GetResource(string key)
		{
			return resourceManager.GetString(
				key, 
				TDCultureInfo.CurrentUICulture);
		}

		/// <summary>
		/// Method that returns the resource associated with the given key from 
		/// the specified non-default resource manager. 
		/// </summary>
		/// <param name="key">resource key</param>
		/// <param name="resourceFileName">name of resource file</param>
		/// <returns>resource string</returns>
		public string GetResource(string resourceFileName, string key)
		{
			return resourceManager.GetString (resourceFileName, key);
		}

	}
}