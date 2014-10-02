// *********************************************** 
// NAME                 : CallingPointsControl.cs
// AUTHOR               : Richard Philpott
// DATE CREATED         : 2005-07-11
// DESCRIPTION			: Control to display formatted lines of a 
//                        portion of a public transport schedule  
// ************************************************
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/CallingPointsControl.ascx.cs-arc  $
//
//   Rev 1.3   Jan 12 2009 11:13:28   apatel
//XHTML Compliance Changes
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.2   Mar 31 2008 13:19:30   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:12:28   mturner
//Initial revision.
//
//   Rev 1.6   Feb 23 2006 19:16:24   build
//Automatically merged from branch for stream3129
//
//   Rev 1.5.1.1   Jan 30 2006 14:41:00   mdambrine
//moved TDCultureInfo to the common project
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.5.1.0   Jan 10 2006 15:23:40   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.5   Aug 16 2005 17:52:42   RPhilpott
//FxCop and code review fixes.
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.4   Aug 09 2005 18:44:04   RPhilpott
//Make board/alight times bold.
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.3   Jul 29 2005 17:15:34   RPhilpott
//Add CSS classes.
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.2   Jul 28 2005 17:25:26   RPhilpott
//Correct id tags.
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.1   Jul 22 2005 19:59:58   RPhilpott
//Development of new ServiceDetails page.
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.0   Jul 19 2005 10:08:10   RPhilpott
//Initial revision.
//

using System;
using System.Data;
using System.Text;
using System.Drawing;
using System.Globalization;

using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using TransportDirect.UserPortal.Web;
using TransportDirect.UserPortal.Web.Adapters;
using TransportDirect.Common.ResourceManager;


namespace TransportDirect.UserPortal.Web.Controls
{

	/// <summary>
	/// Control to display formatted lines of a 
	/// portion of a public transport schedule	
	/// </summary>
	public partial class CallingPointsControl : TDPrintableUserControl
	{

		private CallingPointControlType mode;
		private CallingPointLine[] callingPoints;

		protected void Page_Load(object sender, System.EventArgs e)
		{
		}

		/// <summary>
		/// Constructor.
		/// </summary>
		public CallingPointsControl()
		{
			LocalResourceManager = TDResourceManager.JOURNEY_RESULTS_RM;
		}

		/// <summary>
		/// Event handler for the prerender event
		/// </summary>
		/// <param name="e"></param>
		protected override void OnPreRender(EventArgs e)
		{

			if	(callingPoints == null || callingPoints.Length == 0)
			{
				this.Visible = false;
			}
			else
			{
				serviceDetailsRepeater.DataSource = callingPoints;
				serviceDetailsRepeater.DataBind();
				serviceDetailsRepeater.Visible = true;
				this.Visible = true;
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
		/// Gets/Sets the calling point lines 
		/// to be displayed by the control. 
		/// </summary>
		public CallingPointLine[] CallingPoints
		{
			get { return callingPoints; }
			set { callingPoints = value; }
		}

		/// <summary>
		/// Gets/Sets the mode, determining which portion of the journey
		/// is being displayed (this affects the formatting used).   
		/// </summary>
		public CallingPointControlType Mode
		{
			get { return mode; }
			set { mode = value; }
		}
	
		/// <summary>
		/// Gets an id string for the whole table (used by CSS).
		/// </summary>
		/// <returns>CSS id</returns>
		public string TableId()
		{
			string tableId = string.Empty;
			
			switch (mode)
			{
				case CallingPointControlType.Before:
				case CallingPointControlType.After:
					tableId = "sdTableBeforeAfter";	
					break;

				case CallingPointControlType.Leg:
					tableId = "sdTableleg";	
					break;
			}

			return tableId; 
		}

		/// <summary>
		/// Gets an id string for the header row (used by CSS).
		/// </summary>
		/// <returns>CSS id</returns>
		public string HeaderRowId()
		{
			string rowId = string.Empty;
			
			switch (mode)
			{
				case CallingPointControlType.Before:
				case CallingPointControlType.After:
					rowId = "sdHeaderBeforeAfter";	
					break;

				case CallingPointControlType.Leg:
					rowId = "sdHeaderLeg";	
					break;
			}

			return rowId; 

		}

		/// <summary>
		/// Gets an id string for a header column (used by CSS).
		/// </summary>
		/// <param name="index">The column number</param>
		/// <returns>CSS id</returns>
		public string HeaderColId(int index)
		{
			string colId = string.Empty;
			
			string colNo = (index + 1).ToString(CultureInfo.InvariantCulture);

			switch (mode)
			{
				case CallingPointControlType.Before:
				case CallingPointControlType.After:
					colId = "sdHeaderBeforeAfterCol" + colNo;	
					break;

				case CallingPointControlType.Leg:
					colId = "sdHeaderLegCol" + colNo;	
					break;
			}

			return colId; 
		}


		/// <summary>
		/// Gets an id string for a detail row (used by CSS).
		/// </summary>
		/// <returns>CSS id</returns>
		public string DetailRowId()
		{
			string rowId = string.Empty;
			
			switch (mode)
			{
				case CallingPointControlType.Before:
				case CallingPointControlType.After:
					rowId = "sdDetailBeforeAfter";	
					break;

				case CallingPointControlType.Leg:
					rowId = "sdDetailLeg";	
					break;
			}

			return rowId; 
		}


		/// <summary>
		/// Gets an id string for a detail column (used by CSS).
		/// </summary>
		/// <param name="itemIndex">The row number</param>
		/// <param name="colIndex">The column number</param>
		/// <returns></returns>
		public string DetailColId(int itemIndex, int colIndex)
		{
			string colId = string.Empty;
			string colNo = (colIndex + 1).ToString(CultureInfo.InvariantCulture);

			switch (mode)
			{
				case CallingPointControlType.Before:
				case CallingPointControlType.After:
					colId = "sdDetailBeforeAfterCol" + colNo;	
					break;

				case CallingPointControlType.Leg:
					string isBold = string.Empty;
					
					if	(callingPoints[itemIndex].SignificantStation)
					{
						if	(colIndex == 0)
						{
							isBold = "Bold";
						}
						else if (colIndex == 2 && itemIndex == 0)  
						{	
							isBold = "Bold";
						}
						else if (colIndex == 1 && itemIndex > 0)  
						{
							isBold = "Bold";
						}
					}
					colId = "sdDetailLegCol" + isBold + colNo;	
					break;
			}

			return colId; 
		}


		/// <summary>
		/// Gets the header description.
		/// </summary>
		/// <returns></returns>
		public string HeaderDescription()
		{
			string description = string.Empty;
			
			switch (mode)
			{
				case CallingPointControlType.Before:
					description = GetResource("CallingPoint.Heading.Before");	
					break;

				case CallingPointControlType.Leg:
					description = GetResource("CallingPoint.Heading.Leg");	
					break;

				case CallingPointControlType.After:
					description = GetResource("CallingPoint.Heading.After");	
					break;
			}
			
			return description;
		}


		/// <summary>
		/// Text for arrival header column.
		/// </summary>
		/// <returns></returns>
		public string HeaderArrival()
		{
			return GetResource("CallingPoint.Heading.Arrive");
		}

		/// <summary>
		/// Text for departure header column.
		/// </summary>
		/// <returns></returns>
		public string HeaderDeparture()
		{
			return GetResource("CallingPoint.Heading.Depart");
		}

		/// <summary>
		/// Station name
		/// </summary>
		/// <param name="index">Row number</param>
		/// <returns></returns>
		public string DetailDescription(int index)
		{
			return callingPoints[index].StationName;
		}

		/// <summary>
		/// Gets arrival time for this row (may be "arrives" or "--").
		/// </summary>
		/// <param name="index">Row number</param>
		/// <returns></returns>
		public string DetailArrival(int index)
		{
			return callingPoints[index].ArrivalTime;
		}

		/// <summary>
		/// Gets departure time for this row (may be "departs" or "--").
		/// </summary>
		/// <param name="index">Row number</param>
		/// <returns></returns>
		public string DetailDeparture(int index)
		{
			return callingPoints[index].DepartureTime;
		}

	}


	/// <summary>
	/// Used to describe the portion of the journey to be formatted.
	/// </summary>
	public enum CallingPointControlType
	{
		Before,
		After,
		Leg
	}
}
