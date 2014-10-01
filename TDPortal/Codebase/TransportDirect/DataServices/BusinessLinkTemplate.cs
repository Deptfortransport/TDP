// *********************************************** 
// NAME                 :	BusinessLinkTemplate.cs
// AUTHOR               :	Tolu Olomolaiye
// DATE CREATED         :	22 Nov 2005 
// DESCRIPTION			: Immutable class for Business Links
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/DataServices/BusinessLinkTemplate.cs-arc  $ 
//
//   Rev 1.1   Mar 31 2008 12:05:54   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 12:20:44   mturner
//Initial revision.
//
//  DEVFACTORY Mar 28 2008 sbarker
//  Allow images to be partner customisable
//
//   Rev 1.3   Dec 16 2005 12:11:42   jbroome
//Made class serializable
//Resolution for 3143: DEL 8 stream: Business Links Development
//
//   Rev 1.2   Nov 28 2005 09:57:00   tolomolaiye
//Updates following code review
//Resolution for 3143: DEL 8 stream: Business Links Development
//
//   Rev 1.1   Nov 22 2005 16:49:56   tolomolaiye
//Re-ordered parameters
//Resolution for 3143: DEL 8 stream: Business Links Development
//
//   Rev 1.0   Nov 22 2005 11:23:10   tolomolaiye
//Initial revision.

using System;
using TD.ThemeInfrastructure;

namespace TransportDirect.UserPortal.DataServices
{
	/// <summary>
	/// Summary description for BusinessLinkTemplate.
	/// </summary>
	[Serializable]
	public class BusinessLinkTemplate
	{
        private const string PartnerName = "{PARTNER_NAME}";

		private int id;
		private string resourceId = String.Empty;
		private string imageUrl = String.Empty;
		private string html = String.Empty;

		/// <summary>
		/// Constructor - set up the BusinessLinkTemplate object
		/// The parameters correspond to the field names from the table in SQL server
		/// </summary>
		/// <param name="id">The id of the html script</param>
		/// <param name="imageUrl">The image url used for diaply purposes</param>
		/// <param name="resourceId">The resource id</param>
		/// <param name="html">The HTML script that will be displayed to the user</param>
		public BusinessLinkTemplate(int id, string resourceId, string imageUrl, string html)
		{
			this.id = id;
			this.resourceId = resourceId;
			this.imageUrl = imageUrl;
			this.html = html;
		}

		/// <summary>
		/// Read only property. Get the id of the class
		/// </summary>
		public int Id
		{
			get {return id;}
		}

		/// <summary>
		/// Read only property. Get the resourceId of the class
		/// </summary>
		public string ResourceId
		{
			get {return resourceId;}
		}

		/// <summary>
		/// Read only property. Get the imageUrl of the class
		/// </summary>
		public string ImageUrl
		{
			get 
            {
                //We replace the partner name with correct text:
                return imageUrl.Replace(PartnerName, ThemeProvider.Instance.GetTheme().Name);
            }
		}


		/// <summary>
		/// Read only property. Get the html of the class
		/// </summary>
		public string Html
		{
			get {return html;}
		}
	}
}
