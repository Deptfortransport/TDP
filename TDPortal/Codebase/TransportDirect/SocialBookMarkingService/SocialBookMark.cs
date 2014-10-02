// *********************************************** 
// NAME			: SocialBookMark.cs
// AUTHOR		: PhilScott
// DATE CREATED	: 22/09/2009 
// DESCRIPTION	: Class which hold data about a Social BookMark
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/SocialBookMarkingService/SocialBookMark.cs-arc  $
//
//   Rev 1.0   Sep 23 2009 12:19:22   PScott
//Initial revision.
//Resolution for 5305: CCN530 Social Bookmarking
//



using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.PropertyService.Properties;

using Logger = System.Diagnostics.Trace;
using TransportDirect.Common.ServiceDiscovery;

namespace TransportDirect.UserPortal.SocialBookMarkingService
{
	
	
	/// <summary>
	/// Displays and stores information about a Social BookMark
	/// </summary>
	public class SocialBookMark
	{
 
		private readonly int sbmId;
		private readonly string sbmDescription;
		private readonly string sbmDisplayText;
		private readonly string sbmDisplayIconPath;
		private readonly string sbmURLTemplate;
		private readonly string sbmLandingPartnerCode;
        private readonly int sbmThemeId;

		#region Constructor
		/// <summary>
		/// Default constructor.
		/// </summary>
		public SocialBookMark()
		{
		}

		/// <summary>
		/// SocialBookMarks Constructor
		/// Initialise all properties from the database
		/// </summary>
		/// <param name="sbmId">The Social BookMark reference</param>
		/// <param name="sbmDescription">Name of the Social BookMark</param>
		/// <param name="sbmDisplayText">text to be displayed on screen</param>
		/// <param name="sbmDisplayIconPath">file path to icon</param>
		/// <param name="sbmURLTemplate">template to use in formatting bookmark request</param>
		/// <param name="sbmLandingPartnerCode">the landingPartner ref for the bookmark</param>
		/// <param name="sbmThemeId">theme id</param>


		public SocialBookMark(	int sbmId,
						string sbmDescription, 
						string sbmDisplayText, 
						string sbmDisplayIconPath,
						string sbmURLTemplate,
						string sbmLandingPartnerCode,
						int sbmThemeId)
		{

			this.sbmId 			= sbmId;
			this.sbmDescription 		= sbmDescription;
			this.sbmDisplayText 		= sbmDisplayText;
			this.sbmDisplayIconPath 	= sbmDisplayIconPath;
			this.sbmURLTemplate 		= sbmURLTemplate;
			this.sbmLandingPartnerCode 	= sbmLandingPartnerCode;
			this.sbmThemeId 		= sbmThemeId;
		}
		#endregion

		#region Social BookMark Public Properties
        
        /// <summary>
		/// Read only property. Get the Social Book Mark reference
		/// </summary>
		public int SBMId
		{
			get {return sbmId;}
		}

		/// <summary>
		/// Read only property. Get the Social Book Mark Description
		/// </summary>
		public string SBMDescription
		{
			get {return sbmDescription;}
		}

		/// <summary>
		/// Read only property. Get the SBMDisplayText
		/// </summary>
		public string SBMDisplayText
		{
			get { return sbmDisplayText; }
		}
		/// <summary>
		/// Read only property. Get the SBMDisplayIconPath
		/// </summary>
		public string SBMDisplayIconPath
		{
			get { return sbmDisplayIconPath; }
		}
		/// <summary>
		/// Read only property. Get the SBMURLTemplate
		/// </summary>
		public string SBMURLTemplate
		{
			get { return sbmURLTemplate; }
		}
		/// <summary>
		/// Read only property. Get the SBMLandingPartnerCode
		/// </summary>
		public string SBMLandingPartnerCode
		{
			get { return sbmLandingPartnerCode; }
		}
		/// <summary>
		/// Read only property. Get the SBMThemeId
		/// </summary>
		public int SBMThemeId
		{
			get { return sbmThemeId; }
		}
		
		#endregion

	}
}
