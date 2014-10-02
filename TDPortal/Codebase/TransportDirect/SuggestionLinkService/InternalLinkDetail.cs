// *********************************************** 
// NAME                 : InternalLinkDetail.cs
// AUTHOR               : Ken Josling
// DATE CREATED         : 18/08/2005 
// DESCRIPTION			: InternalLinkDetail contains information about an internal link
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/SuggestionLinkService/InternalLinkDetail.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:50:08   mturner
//Initial revision.
//
//   Rev 1.2   Feb 16 2006 15:54:30   build
//Automatically merged from branch for stream0002
//
//   Rev 1.1.1.0   Dec 16 2005 11:49:12   kjosling
//Modified to make properties read-only 
//
//   Rev 1.1   Aug 31 2005 14:47:22   kjosling
//Updated following code review
//
//   Rev 1.0   Aug 24 2005 16:44:52   kjosling
//Initial revision.

using System;
using TransportDirect.Common;

namespace TransportDirect.UserPortal.SuggestionLinkService
{
	/// <summary>
	/// InternalLinkDetail contains information about an internal link
	/// </summary>
	public class InternalLinkDetail : ILinkDetails
	{
		#region Private Properties

		/// <summary> String used by the URL property which represents the URL link </summary>
		private string urlLink;
		
				
		/// <summary>
		/// Boolean used by IsValid property to store whether a URL is considered valid 
		/// </summary>
		private bool validUrl;


		#endregion

		#region Constructor
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="url">string representing a url which is the actual link 
		/// to display or a template of the link to display</param>
		/// <param name="isValid">boolean indicating whether the url is deemed valid. </param>
		public InternalLinkDetail(string url, bool isValid)
		{
			urlLink = url;
			validUrl = isValid;

		}
		#endregion

		#region Public Properties

		/// <summary>
		/// (Read-Write) Returns the URL string
		/// </summary>
		public string Url
		{
			get 
			{
				return urlLink;
			}
		}

		/// <summary>
		/// (Read-Write) Returns a boolean indicating whether the URL is considered valid 
		/// </summary>
		public bool IsValid
		{
			get 
			{
				return validUrl;
			}
		}
		#endregion

	}
}
