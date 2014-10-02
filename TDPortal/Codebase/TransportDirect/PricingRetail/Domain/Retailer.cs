// *********************************************** 
// NAME			: Retailer.cs
// AUTHOR		: C.M. Owczarek
// DATE CREATED	: 26/09/03
// DESCRIPTION	: Class representing a ticket retailer. Instances of this class are immutable.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PricingRetail/Domain/Retailer.cs-arc  $
//
//   Rev 1.1   Mar 10 2008 15:22:24   mturner
//Initial Del10 Codebase from Dev Factory
//
//   Rev 1.0   Nov 08 2007 12:36:54   mturner
//Initial revision.
//
//   Rev 1.12   Mar 02 2005 16:14:42   jgeorge
//Added small icon url property to retailer
//
//   Rev 1.11   Dec 23 2004 11:57:20   jgeorge
//Modified to add new property to Retailer
//
//   Rev 1.10   Nov 18 2003 16:10:12   COwczarek
//SCR#247 :Add $Log: for PVCS history

using System;
using TransportDirect.Common.DatabaseInfrastructure.Content;

namespace TransportDirect.UserPortal.PricingRetail.Domain
{
	/// <summary>
	/// Class representing a ticket retailer
	/// </summary>
	[Serializable()]
	public class Retailer
	{
        #region Private members
        
        /// <summary>
		/// A unique identifier for retailer
		/// </summary>
		private string id;
        
		/// <summary>
		/// The name of the retailer suitable for display purposes
		/// </summary>
		private string name;
		
		/// <summary>
		/// The URL of the retailers website suitable for display purposes
		/// </summary>
		private string displayUrl;
		
		/// <summary>
		/// The URL of the retailers website
		/// </summary>
		private string websiteUrl;
        
		/// <summary>
		/// The URL to which XML data is posted
		/// </summary>
		private string handoffUrl;
		
		/// <summary>
		/// The phone number of the retailer suitable for display purposes
		/// </summary>
		private string phoneNumber;

        /// <summary>
        /// The display phone number of the retailer
        /// </summary>
        private string phoneNumberDisplay;

		/// <summary>
		/// The URL of the retailer's logo displayed on the handoff page
		/// </summary>
		private string iconUrl;

		/// <summary>
		/// The URL of the retailer's logo displayed in lists
		/// </summary>
		private string smallIconUrl;

		/// <summary>
		/// Whether or not the retailer allows MTH
		/// </summary>
		private bool allowsMTH;

        /// <summary>
        /// The resource manager base key containing the resources for retailer
        /// </summary>
        private string resourceKey;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public Retailer()
        {
        }

        /// <summary>
		/// Create a Retailer
		/// </summary>
        /// <param name="id">A unique identifier for retailer</param>
        /// <param name="name">The name of the retailer suitable for display purposes</param>
        /// <param name="websiteUrl">The URL of the retailers website</param>
        /// <param name="handoffUrl">The URL to which XML data is posted</param>
        /// <param name="displayUrl">The URL of the retailers website suitable for display purposes</param>
        /// <param name="phoneNumber">The phone number of the retailer suitable</param>
        /// <param name="phoneNumberDisplay">The phone number of the retailer suitable for display purposes</param>
        /// <param name="iconUrl">The URL of the retailer's logo displayed on the handoff page</param>
        /// <param name="smallIconUrl">The URL of the retailer's logo displayed in lists</param>
        /// <param name="allowsMTH">Whether or not the retailer allows multiple ticket handoff</param>
        /// <param name="resourceKey">The resource manager key containing the resources</param>
        public Retailer(string id, string name,
            string websiteUrl, string handoffUrl, string displayUrl, 
            string phoneNumber, string phoneNumberDisplay, 
            string iconUrl, string smallIconUrl, bool allowsMTH, 
            string resourceKey)
		{
            this.id = id;
			this.name = name;
            this.websiteUrl = websiteUrl;
            this.handoffUrl = handoffUrl;
            this.displayUrl = displayUrl;
            this.phoneNumber = phoneNumber;
            this.phoneNumberDisplay = phoneNumberDisplay;
            this.iconUrl = ImageUrlHelper.GetAlteredImageUrl(iconUrl);
            this.smallIconUrl = ImageUrlHelper.GetAlteredImageUrl(smallIconUrl);
			this.allowsMTH = allowsMTH;
            this.resourceKey = resourceKey;
		}

        #endregion

        #region Properties

        /// <summary>
        /// Read only property for retailer id
        /// </summary>
        public string Id
        {
            get {return id;}
        }

        /// <summary>
		/// Read only property for retailer name
		/// </summary>
		public string Name
		{
			get {return name;}
		}

		/// <summary>
		/// Read only property for the URL of the retailers public website (for display)
		/// </summary>
		public string DisplayUrl
		{
			get {return displayUrl;}
		}

        /// <summary>
        /// Read only property for the URL of the retailers public website (for redirection)
        /// </summary>
        public string WebsiteUrl
        {
            get {return websiteUrl;}
        }

        /// <summary>
		/// Read only property for handoff URL (the URL we post the XML to)
		/// </summary>
		public string HandoffUrl
		{
			get {return handoffUrl;}
		}

		/// <summary>
		/// Read only property for phone number
		/// </summary>
		public string PhoneNumber
		{
			get {return phoneNumber;}
		}

        /// <summary>
        /// Read only property for retailer phone number to display
        /// </summary>
        public string PhoneNumberDisplay
        {
            get { return phoneNumberDisplay; }
        }

        /// <summary>
        /// Read only property for URL of retailer's icon
        /// </summary>
        public string IconUrl
        {
            get {return iconUrl;}
        }
        
		/// <summary>
		/// Read only property for URL of retailer's small icon
		/// </summary>
		public string SmallIconUrl
		{
			get { return smallIconUrl; }
		}

        /// <summary>
        /// Read only property that returns true if retailer can perform ticket handoff
        /// </summary>
        public bool isHandoffSupported
        {
            get {return !((handoffUrl == null) || (handoffUrl.Length == 0));}
        }
        
		/// <summary>
		/// Read only property that returns true if the retailer allows MTH
		/// </summary>
		public bool AllowsMultipleTicketHandoff
		{
			get { return allowsMTH; }
		}

        /// <summary>
        /// Read only property for resource manager base key containing resources for retailer
        /// </summary>
        public string ResourceKey
        {
            get { return resourceKey; }
        }

        #endregion

    }
}