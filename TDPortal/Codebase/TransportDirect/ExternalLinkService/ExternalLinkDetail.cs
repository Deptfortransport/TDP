// *********************************************** 
// NAME                 : ExternalLinkDetail.cs
// AUTHOR               : Rachel Geraghty
// DATE CREATED         : 07/06/2005 
// DESCRIPTION			: Class contains information about a url link
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ExternalLinkService/ExternalLinkDetail.cs-arc  $
//
//   Rev 1.1   Nov 29 2007 10:35:18   mturner
//Updated for Del 9.8
//
//   Rev 1.6   Oct 25 2007 15:14:24   mmodi
//Added a comparer to allow sorting using the LinkText
//Resolution for 4518: Del 9.8 - Air Departure Boards
//
//   Rev 1.5   Feb 16 2006 15:54:24   build
//Automatically merged from branch for stream0002
//
//   Rev 1.4.1.2   Feb 09 2006 10:27:44   jbroome
//Code review - Removed unecessary private properties
//Resolution for 2: DEL 8.1 Workstream - Zonal Services Phase 1
//
//   Rev 1.4.1.1   Jan 11 2006 12:23:22   tolomolaiye
//Code review updates
//Resolution for 2: DEL 8.1 Workstream - Zonal Services Phase 1
//
//   Rev 1.4.1.0   Dec 16 2005 11:54:28   kjosling
//Added extra properties
//Resolution for 2: DEL 8.1 Workstream - Zonal Services Phase 1
//
//   Rev 1.4   Sep 27 2005 10:53:56   asinclair
//Merge from branch for stream2596
//Resolution for 2596: DEL 8 Stream: Park and Ride
//
//   Rev 1.3.1.0   Aug 19 2005 16:32:04   kjosling
//Class now implements ILinkDetails. Added set methods to public properties
//Resolution for 2596: DEL 8 Stream: Park and Ride
//
//   Rev 1.3   Jun 27 2005 14:29:40   rgeraghty
//Updated documentation comments
//Resolution for 2559: DEL 8 Stream: External Links Repository
//
//   Rev 1.2   Jun 27 2005 12:33:26   rgeraghty
//Updated with code review comments
//Resolution for 2559: DEL 8 Stream: External Links Repository
//
//   Rev 1.1   Jun 14 2005 18:58:40   rgeraghty
//Updated to include IR association
//Resolution for 2559: DEL 8 Stream: External Links Repository
//
//   Rev 1.0   Jun 14 2005 18:56:14   rgeraghty
//Initial revision.

using System;
using TransportDirect.Common;

namespace TransportDirect.UserPortal.ExternalLinkService
{
	/// <summary>
	/// ExternalLinkDetail class contains information about a url link
	/// </summary>
	public class ExternalLinkDetail : ILinkDetails, IComparable
	{
		#region private members

		private string urlLink;
		private bool validUrl;
		private TDDateTime startDate;
		private TDDateTime endDate;
		private string linkText;

		#endregion

		#region Constructor
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="url">string representing a url which is the actual link 
		/// to display or a template of the link to display</param>
		/// <param name="isValid">determines if the link is valid</param>
		/// <param name="start">the date to start publishing from</param>
		/// <param name="end">the date to stop publishing the link</param>
		/// <param name="text">link text</param>
		public ExternalLinkDetail(string url, bool isValid, TDDateTime start, TDDateTime end, string text)
		{
			urlLink = url;
			validUrl = isValid;
			linkText = text;
			startDate = start;
			endDate = end;
		}
		#endregion

		#region properties

		/// <summary>
		/// (Read-only) Returns the URL
		/// </summary>
		public string Url
		{
			get 
			{
				return urlLink;
			}
		}

		/// <summary>
		/// (Read-only) Returns a boolean indicating
		/// whether a URL is considered valid 
		/// </summary>
		public bool IsValid
		{
			get 
			{
				return validUrl;
			}
		}

		/// <summary>
		/// (Read-only) Returns true if the link is valid for
		/// publication. False otherwise.
		/// </summary>
		public bool Published
		{
			get
			{
				TDDateTime currentDate = TDDateTime.Now;
				if(startDate > currentDate && endDate < currentDate)
				{
					return false;
				}
				else
				{
					return true;
				}
			}
		}

		/// <summary>
		/// (Read-write) Returns the link text
		/// </summary>
		public string LinkText
		{
			get
			{
				return linkText;
			}
			set
			{
				linkText = value;
			}
		}

		#endregion

		#region IComparable Members

		/// <summary>
		/// Compare an ExternalLinkDetail using the LinkText
		/// </summary>
		/// <param name="obj"></param>
		/// <returns></returns>
		public int CompareTo(object obj)
		{
			ExternalLinkDetail target = obj as ExternalLinkDetail;
			if (target != null)
			{
				return linkText.CompareTo(target.LinkText);
			}

			throw new ArgumentException("Cannot compare ExternalLinkDetail to null");
		}

		#endregion
	}
}
