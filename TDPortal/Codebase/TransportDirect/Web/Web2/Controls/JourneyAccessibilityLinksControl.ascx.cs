// *********************************************** 
// NAME                 : JourneyAccessibilityLinksControl.ascx
// AUTHOR               : James Broome
// DATE CREATED         : 07/06/2005 
// DESCRIPTION			: Control displaying icons and links for modes of tranport used, 
//						: which take user to DPTAC website for accessibility info.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/JourneyAccessibilityLinksControl.ascx.cs-arc  $ 
//
//   Rev 1.5   Jan 20 2013 16:26:46   mmodi
//Updated for transport modetype Telecabine
//Resolution for 5884: CCN:XXX - Telecabine modetype
//
//   Rev 1.4   Jan 12 2009 15:43:14   apatel
//XHTML Compliance Changes
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.3   Jul 24 2008 10:44:28   apatel
//Removed External Links tooltip and added (opens new window) text to the links
//Resolution for 5087: Accessibility - external links text changes
//
//   Rev 1.2   Mar 31 2008 13:21:10   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:15:04   mturner
//Initial revision.
//
//   Rev 1.2   Feb 23 2006 19:16:46   build
//Automatically merged from branch for stream3129
//
//   Rev 1.1.1.0   Jan 10 2006 15:25:28   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.1   Jul 20 2005 15:31:52   NMoorhouse
//Apply Review Comments
//Resolution for 2556: DEL 8 Stream: Accessibility Links
//
//   Rev 1.0   Jun 29 2005 11:10:44   jbroome
//Initial revision.
//Resolution for 2556: DEL 8 Stream: Accessibility Links

namespace TransportDirect.UserPortal.Web.Controls
{
	using System;using TransportDirect.Common.ResourceManager;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	using TransportDirect.UserPortal.Web.Controls;

	using TransportDirect.JourneyPlanning.CJPInterface;
	using TransportDirect.UserPortal.ExternalLinkService;

	/// <summary>
	///	Control displaying icons and links for modes of tranport used, 
	///	which take user to DPTAC website for accessibility info.
	///	Consists of a datalist control, bound to a collection 
	///	of transport modes.
	/// </summary>
	public partial class JourneyAccessibilityLinksControl : TDUserControl
	{
		private ModeType[] transportModes;

		#region Constants

		private const string modeName = "ModeName";
		private const string modeId = "ModeId";
		private const string modeUrl = "ModeUrl";
		private const string orderByClause = "ModeName ASC";
		
		#endregion

		/// <summary>
		/// Page load method
		/// Bind DataList to collection of transport modes
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			labelInfoText.Text = GetResource("JourneyAccessibilityLinksControl.labelInfoText");
			
			// Bind Data List to processed collection of transport modes
			DataRow[] formattedModes = ProcessTransportModes(transportModes);
			if (formattedModes != null && formattedModes.Length > 0)
			{
				transportModeList.DataSource = formattedModes;
				transportModeList.DataBind();
				labelInfoText.Visible = true;
			}
			else
			{
				// No icons to show, so hide info label
				labelInfoText.Visible = false;
			}
		}

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

		/// <summary>
		/// Method processes an array of ModeTypes to get
		/// the mode name, image, url and title for each mode.
		/// This data is sorted and returned as an array of DataRows.
		/// </summary>
		/// <param name="transportModes">ModeType array</param>
		/// <returns>DataRow array</returns>
		private DataRow[] ProcessTransportModes(ModeType[] transportModes)
		{
			// Access External Links service
			IExternalLinks externalLinks = ExternalLinks.Current;
			
			// Create data table
			DataTable modeTable = new DataTable();
			// Create and add columns
			DataColumn modeIdColumn = new  DataColumn(modeId);
			DataColumn modeNameColumn = new  DataColumn(modeName);
			DataColumn modeUrlColumn = new  DataColumn(modeUrl);
			
			modeTable.Columns.Add(modeIdColumn);
			modeTable.Columns.Add(modeNameColumn);
			modeTable.Columns.Add(modeUrlColumn);
			
			foreach (ModeType mode in transportModes)
			{
				// Get link key for external link repository
				string linkKey = getExternalLinkKey(mode);

				// Get link from external link repository
				ExternalLinkDetail linkDetail = externalLinks[linkKey];				
			
				// If link is valid, then include it
				if ((linkDetail != null) && (linkDetail.IsValid))
				{
					// Create new row for each mode and add to table
					DataRow row = modeTable.NewRow();
					row[modeId] = mode.ToString();
					row[modeName] = GetResource(string.Format("JourneyAccessibilityLinksControl.ModeName.{0}", mode.ToString()));
					row[modeUrl] = linkDetail.Url;
					modeTable.Rows.Add(row);
				}
			}

			// Sort table by Mode Name column and return
			return modeTable.Select(string.Empty, orderByClause);
		}

		/// <summary>
		/// Method returns the key for the External Links repository
		/// based on the mode of transport.
		/// </summary>
		/// <param name="mode">Mode of transport</param>
		/// <returns>ExternalLinksKey string</returns>
		private string getExternalLinkKey(ModeType mode)
		{
			switch( mode )
			{
				case ModeType.Car:
					return ExternalLinksKeys.AccessibilityInformation_Car;
				case ModeType.Air:
					return ExternalLinksKeys.AccessibilityInformation_Air;
				case ModeType.Bus:
					return ExternalLinksKeys.AccessibilityInformation_Bus;
				case ModeType.Coach:
					return ExternalLinksKeys.AccessibilityInformation_Coach;
				case ModeType.Cycle:
					return ExternalLinksKeys.AccessibilityInformation_Cycle;
				case ModeType.Drt:
					return ExternalLinksKeys.AccessibilityInformation_Drt;
				case ModeType.Ferry:
					return ExternalLinksKeys.AccessibilityInformation_Ferry;
				case ModeType.Metro:
					return ExternalLinksKeys.AccessibilityInformation_Metro;
				case ModeType.Rail:
					return ExternalLinksKeys.AccessibilityInformation_Rail;
				case ModeType.Taxi:
					return ExternalLinksKeys.AccessibilityInformation_Taxi;
                case ModeType.Telecabine:
                    return ExternalLinksKeys.AccessibilityInformation_Telecabine;
				case ModeType.Tram:
					return ExternalLinksKeys.AccessibilityInformation_Tram;
				case ModeType.Underground:
					return ExternalLinksKeys.AccessibilityInformation_Underground;
				case ModeType.RailReplacementBus:
					return ExternalLinksKeys.AccessibilityInformation_RailReplacementBus;
				default:
					return string.Empty;
			}
		}

		/// <summary>
		/// Method returns the name of the
		/// transport mode that the row applies to.
		/// </summary>
		/// <param name="row">Data row</param>
		/// <returns>string name</returns>
		protected string GetLinkText(DataRow row)
		{
            return string.Format("{0} {1}", row[modeName].ToString(), GetResource("ExternalLinks.OpensNewWindowText")); 
		}

		/// <summary>
		/// Method returns the path of the
		/// image for the transport mode that 
		/// the row applies to.
		/// </summary>
		/// <param name="row">Data row</param>
		/// <returns>string path</returns>
		protected string GetLinkImage(DataRow row)
		{
			return GetResource(string.Format("JourneyAccessibilityLinksControl.ImagePath.{0}", row[modeId].ToString()));		
		}

		/// <summary>
		/// Method returns the url of the
		/// link for the transport mode that 
		/// the row applies to.
		/// </summary>
		/// <param name="row">Data row</param>
		/// <returns>string url</returns>
		protected string GetLinkUrl(DataRow row)
		{
			return row[modeUrl].ToString();
		}

		/// <summary>
		/// Method returns the title of the
		/// transport mode that the row applies to.
		/// </summary>
		/// <param name="row">Data row</param>
		/// <returns>string title</returns>
		protected string GetLinkTitle(DataRow row)
		{
			return GetResource(string.Format("JourneyAccessibilityLinksControl.TitleText.{0}", row[modeId].ToString()));		
		}

		/// <summary>
		/// Public read/write property
		/// Modes of transport that are 
		/// applicable to the current journey
		/// </summary>
		public ModeType[] TransportModes
		{
			get 
			{
				return transportModes;
			}
			set 
			{
				transportModes = value;
			}
		}
	}
}
