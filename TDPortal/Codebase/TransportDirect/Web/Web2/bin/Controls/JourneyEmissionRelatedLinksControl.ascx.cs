// *********************************************** 
// NAME                 : JourneyEmissionRelatedLinksControl.ascx
// AUTHOR               : Darshan Sawe
// DATE CREATED         : 26/11/2006
// DESCRIPTION			: Control displaying the Internal and External Links on the Journey Emissions pages
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/JourneyEmissionRelatedLinksControl.ascx.cs-arc  $ 
//
//   Rev 1.2   Mar 31 2008 13:21:22   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:15:24   mturner
//Initial revision.
//
//   Rev 1.2   Apr 04 2007 14:32:40   mmodi
//Updated control to check for empty url string array
//Resolution for 4350: CO2 Public Transport
//
//   Rev 1.1   Mar 06 2007 12:29:54   Build
//Automatically merged from branch for stream4350
//
//   Rev 1.0.1.0   Mar 05 2007 17:15:28   mmodi
//Updated control to make it more reuseable
//Resolution for 4350: CO2 Public Transport
//

namespace TransportDirect.UserPortal.Web.Controls
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	using TransportDirect.UserPortal.Web.Controls;
	using TransportDirect.UserPortal.ExternalLinkService;
	using TransportDirect.Common.ResourceManager;

	/// <summary>
	///		Summary description for JourneyEmissionRelatedLinksControl.
	/// </summary>
	public partial class JourneyEmissionRelatedLinksControl : TDUserControl
	{

		private string[] externalLinkIds;
		private string[] internalLinkIds;
		
		#region Constants

		private const string LinkText = "LinkText";
		private const string LinkUrl = "LinkUrl";
		
		#endregion

		#region Page Load
		protected void Page_Load(object sender, System.EventArgs e)
		{
			DataRow[] formattedExternalLinks = ProcessExternalLinks(ExternalLinkIds);
			DataRow[] formattedInternalLinks = ProcessInternalLinks(InternalLinkIds);

			if (formattedInternalLinks != null && formattedInternalLinks.Length > 0)
			{
				rptRelatedLinksInternal.DataSource = formattedInternalLinks;
				rptRelatedLinksInternal.DataBind();
				rptRelatedLinksInternal.Visible = true;
			}
			else
			{
				// No links to show, so hide Internal Links Repeater
				rptRelatedLinksInternal.Visible = false;
			}

			if (formattedExternalLinks != null && formattedExternalLinks.Length > 0)
			{
				rptRelatedLinksExternal.DataSource = formattedExternalLinks;
				rptRelatedLinksExternal.DataBind();
				rptRelatedLinksExternal.Visible = true;
			}
			else
			{
				// No links to show, so hide External Links Repeater
				rptRelatedLinksExternal.Visible = false;
			}
		}

		#endregion

		#region Private methods

		private DataRow[] ProcessExternalLinks(string[] linkIds)
		{
			// Access External Links service
			IExternalLinks externalLinks = ExternalLinks.Current;

			// Create data table
			DataTable linkTable = new DataTable();
			// Create and add columns
			DataColumn linkUrlColumn = new DataColumn(LinkUrl);
			DataColumn linkTextColumn = new DataColumn(LinkText);

			linkTable.Columns.Add(linkUrlColumn);
			linkTable.Columns.Add(linkTextColumn);

			foreach (string link in linkIds)
			{
				ExternalLinkDetail linkDetail = externalLinks[link];
	
				// If link is valid, then include it
				if ((linkDetail != null) && (linkDetail.IsValid))
				{
					// Create new row for each mode and add to table
					DataRow row = linkTable.NewRow();
					row[LinkUrl] = linkDetail.Url;
					row[LinkText] = GetResource(string.Format("{0}", link.ToString()));
					linkTable.Rows.Add(row);
				}
			}
			return linkTable.Select();
		}

		private DataRow[] ProcessInternalLinks(string[] linkIds)
		{
			// Create data table
			DataTable linkTable = new DataTable();
			// Create and add columns
			DataColumn linkUrlColumn = new DataColumn(LinkUrl);
			DataColumn linkTextColumn = new DataColumn(LinkText);

			linkTable.Columns.Add(linkUrlColumn);
			linkTable.Columns.Add(linkTextColumn);

			foreach (string link in linkIds)
			{
				if (link != string.Empty)
				{
					string linkUrl = GetResource(link + ".Url");
					string linkText = GetResource(link + ".Text");

					if ((linkUrl != string.Empty) && (linkText != string.Empty))
					{
						// Create new row for each mode and add to table
						DataRow row = linkTable.NewRow();
						row[LinkUrl] = linkUrl;
						row[LinkText] = linkText;
						linkTable.Rows.Add(row);
					}
				}
			}
			return linkTable.Select();
		}

		#endregion

		#region Public properties to populate repeater
		/// <summary>
		/// Method returns the url 
		/// </summary>
		/// <param name="row">DataRow</param>
		/// <returns>string</returns>
		protected string GetLinkUrl(DataRow row)
		{
			return row[LinkUrl].ToString();
		}

		/// <summary>
		/// Method Returns the Link Text
		/// </summary>
		/// <param name="row">DataRow</param>
		/// <returns>string</returns>
		protected string GetLinkText(DataRow row)
		{
			return row[LinkText].ToString();
		}

		#endregion

		#region Public Properties

		/// <summary>
		/// Read/Write. An array of external link ids, these are the Id's in the External links table
		/// </summary>
		public string[] ExternalLinkIds
		{
			get{ return externalLinkIds; }
			set{ externalLinkIds = value; }
		}

		/// <summary>
		/// Read/Write. An array of internal link ids, these refer to the Resource lang string keys, which
		/// must be in pairs e.g. "JourneyEmissions.RelatedLinksFAQ.Url", and "JourneyEmissions.RelatedLinksFAQ.Text"
		/// Note. Do not include the ".Url" or ".Text" part of the key
		/// </summary>
		public string[] InternalLinkIds
		{
			get{ return internalLinkIds; }
			set{ internalLinkIds = value; }
		}

		#endregion

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{

		}
		#endregion
	}
}
