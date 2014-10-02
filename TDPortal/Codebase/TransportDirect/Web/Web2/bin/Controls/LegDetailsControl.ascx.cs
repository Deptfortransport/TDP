// *********************************************** 
// NAME                 : LegDetailsControl.ascx.cs 
// AUTHOR               : Rachel Geraghty
// DATE CREATED         : 10/01/2005
// DESCRIPTION			: Displays journey information for a single leg
//						  whether priced or unpriced
// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/LegDetailsControl.ascx.cs-arc  $
//
//   Rev 1.4   Oct 27 2010 16:50:34   rbroddle
//Removed explicit wire up to Page_PreRender as AutoEventWireUp=true for this control so it was firing twice.
//Resolution for 5621: USD8048975 - page_init and other events wired up more than once on some pages and controls
//
//   Rev 1.3   Feb 04 2009 11:28:58   mmodi
//Updated styles to correct display issue for Adjust journey
//Resolution for 5223: CCN0385 - TTBO Routeing Guide
//
//   Rev 1.2   Mar 31 2008 13:21:42   mturner
//Drop3 from Dev Factory
//
//   Rev 1.1   Dec 19 2007 10:09:40   jfrank
//Fix for .net 2 release.  To fix null reference exception.
//
//   Rev 1.0   Nov 08 2007 13:15:54   mturner
//Initial revision.
//
//   Rev 1.14   Apr 04 2007 21:54:08   asinclair
//added returnnothroughfares
//
//   Rev 1.13   Apr 04 2007 18:34:04   dsawe
//updated for local zonal services
//Resolution for 4362: Local Zonal Services Phase 2 & 3
//
//   Rev 1.12   Mar 28 2007 14:07:02   dsawe
//added changes for air mode
//Resolution for 4362: Local Zonal Services Phase 2 & 3
//
//   Rev 1.11   Mar 27 2007 13:28:44   dsawe
//changed 
//Resolution for 4362: Local Zonal Services Phase 2 & 3
//
//   Rev 1.10   Mar 21 2007 15:26:18   dsawe
//added for printer friendly 
//Resolution for 4362: Local Zonal Services Phase 2 & 3
//
//   Rev 1.9   Mar 19 2007 18:03:02   asinclair
//Updated with outstanding stream4362 work
//Resolution for 4362: Local Zonal Services Phase 2 & 3
//  Rev 1.8   Mar 16 2007 10:00:50   build
//Automatically merged from branch for stream4362
//
//Rev 1.7.1.1   Mar 14 2007 18:41:42   dsawe
//updated for local zonal services - added localzonalfares & localzonaloperatorfares controls
//Resolution for 4362: Local Zonal Services Phase 2 & 3
//
//   Rev 1.7.1.0   Mar 13 2007 17:50:22   dsawe
//added localzonalfares control
//Resolution for 4362: Local Zonal Services Phase 2 & 3
//
//   Rev 1.7   Feb 23 2006 19:16:54   build
//Automatically merged from branch for stream3129
//
//   Rev 1.6.1.1   Jan 30 2006 14:41:14   mdambrine
//moved TDCultureInfo to the common project
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.6.1.0   Jan 10 2006 15:25:56   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.6   Apr 05 2005 14:06:28   rgeraghty
//fx cop changes, plus commenting
//Resolution for 1925: DEV Code Review: Journey Fares
//
//   Rev 1.5   Apr 01 2005 14:59:44   rgeraghty
//Comments added
//Resolution for 1925: DEV Code Review: Journey Fares
//
//   Rev 1.4   Mar 03 2005 17:57:56   rgeraghty
//FxCop changes made
//Resolution for 1925: DEV Code Review: Journey Fares
//
//   Rev 1.3   Mar 01 2005 15:05:10   rgeraghty
//First version
//Resolution for 1925: DEV Code Review: Journey Fares
//
//   Rev 1.2   Feb 09 2005 10:16:52   rgeraghty
//Work in progress
//
//   Rev 1.1   Jan 18 2005 16:20:06   rgeraghty
//Added ILanguageHandlerIndependent interface

namespace TransportDirect.UserPortal.Web.Controls
{
#region .Net namespaces
	using System;
	using System.Data;
	using System.Drawing;
	using System.Text;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using TransportDirect.UserPortal.JourneyControl;
	using TransportDirect.JourneyPlanning.CJPInterface;	
	using TransportDirect.Web.Support;
	using TransportDirect.Common.ResourceManager;
	using TransportDirect.Common;

#endregion

	/// <summary>
	///	Displays Leg Detail information for a single leg, priced or unpriced
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]	
	public partial class LegDetailsControl : TDPrintableUserControl, ILanguageHandlerIndependent
	{
		#region instance members

		protected System.Web.UI.WebControls.TableCell descriptionCell;
		protected TransportDirect.UserPortal.Web.Controls.LocalZonalFaresControl LocalZonalFaresControl1;
		protected TransportDirect.UserPortal.Web.Controls.LocalZonalOpertatorFaresControl LocalZonalOpertatorFaresControl1;
		
		#endregion

		#region control variables

		// control level variables
		private string noFaresInfoText = String.Empty;
		private string toText = String.Empty;
		private string towardsText = String.Empty;
		private string noFaresText = String.Empty;
		private string orText = String.Empty;
		private bool isPriced;
		private PublicJourneyDetail publicJourneyDetail;
		private bool tableView = false;
		private const string tableViewClass = "legTable";
        private const string tableViewRowClass = "legTableRow";
		private bool returnfaresincluded;
		private bool returnNoThroughFares;

		#endregion

		#region Constructor
		/// <summary>
		/// Constructor
		/// </summary>
		public LegDetailsControl()
		{
			//use the fares and tickets resource with this control
			this.LocalResourceManager = TDResourceManager.FARES_AND_TICKETS_RM;
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

		#region Page events

		/// <summary>
		/// Event handler for page load
		/// </summary>
		/// <param name="sender">event originator</param>
		/// <param name="e">event arguments</param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			SetStaticLabelText();

			GetData();					
		}
		
		/// <summary>
		/// Event handler for page pre-render event
		/// </summary>
		/// <param name="sender">event originator</param>
		/// <param name="e">event arguments</param>
		protected void Page_PreRender(object sender, EventArgs e)
		{
			if (publicJourneyDetail == null) //hide the control if there is no Public Journey Detail
				this.Visible=false;
			else
				GetData();
				SetDisplayMode();	
				if (noFaresInfoText.Length == 0)
					labelFaresAbove.Text = GetResource("LegDetailsControl.FaresIncludedLabelText");
				else
					labelFaresAbove.Text = noFaresInfoText;
			
			if(LocalZonalFaresControl1.IsEmpty)
				LocalZonalFaresControl1.Visible = false;
			else
				LocalZonalFaresControl1.Visible = true;

			if(LocalZonalOpertatorFaresControl1.IsEmpty)
				LocalZonalOpertatorFaresControl1.Visible = false;
			else
				LocalZonalOpertatorFaresControl1.Visible = true;
		}

		#endregion

		#region Private methods


		/// <summary>
		/// Sets the label texts for the table rows
		/// </summary>
		private void GetData()
		{
			if (publicJourneyDetail != null )
			{	

					//get the mode text from langstrings
					string resourceModeQueryString = "TransportMode" + "." + publicJourneyDetail.Mode.ToString();

					labelMode.Text = GetResource(resourceModeQueryString);

					//construct the leg description
					StringBuilder legDescription = new StringBuilder();
					legDescription.Append(publicJourneyDetail.LegStart.Location.Description);
					legDescription.Append(" ");
					legDescription.Append(toText);
					legDescription.Append(" ");
					legDescription.Append(publicJourneyDetail.LegEnd.Location.Description);
					labelLegDescription.Text = legDescription.ToString();

					//get the service description
					StringBuilder serviceDescription = new StringBuilder();
					serviceDescription.Append(GetDescription(publicJourneyDetail));
					serviceDescription.Append(" ");
					serviceDescription.Append(noFaresText);
								
					labelLegService.Text = serviceDescription.ToString();

			}
		}

		/// <summary>
		/// Hides/Shows appropriate table rows dependant on whether the leg is priced
		/// </summary>
		private void SetDisplayMode()
		{
			if (publicJourneyDetail != null)
			{
				//hide the middle and bottom rows as n/a for a walk leg
				if (publicJourneyDetail.Mode == ModeType.Walk)
				{
					legRetailerRow.Visible =false;
					faresIncludedRow.Visible =false;	
					dummyRow1.Visible = false;
					dummyRow2.Visible = false;
					if(tableView)
					{
						labelMode.Visible = false;
						labelLegDescription.Visible = false;
						labelLegService.Visible = false;
						legDetailsTable.Visible = false;
					}

				}
				else //not dealing with a walk leg
				{
					// Put user code to initialize the page here
					switch (isPriced)
					{
						case true:
							//not a walk leg
							faresIncludedRow.Visible = true; //show the bottom row
							dummyRow1.Visible = true;
							dummyRow2.Visible = true;
							legRetailerRow.Visible =false; //hide the middle row

							if(tableView)
							{
								labelMode.Visible = false;
								labelLegDescription.Visible = false;
								labelLegService.Visible = false;
								labelFaresAbove.Visible = false;
								legDetailsTable.Visible = false;
							}
							break;

						case false: //leg is unpriced							
							legRetailerRow.Visible = true; //show the middle row	
							faresIncludedRow.Visible = false; //hide the bottom row	
							dummyRow1.Visible = false;
							dummyRow2.Visible = false;


							if(tableView)
							{
								labelLegService.Visible = false;
								labelNotHaveFare.Visible = true;
								if(returnfaresincluded)
								{
									labelNotHaveFare.Text = GetResource("JourneyFaresControl.labelFaresIncludedAbove");
								}
								else
								{
									labelNotHaveFare.Text = GetResource("JourneyFaresControl.labelDoesNotHaveFares");						
								}
								if(returnNoThroughFares)
								{
									labelNotHaveFare.Text = GetResource ("JourneyFaresControl.NoThroughFares");

								}

							}
							break;
					}
				}
				switch (publicJourneyDetail.Mode.ToString())
				{
					case "Bus":
					case "Ferry":
					case "Metro":
					case "Tram":
					case "Underground":
						if(isPriced)
						{
							LocalZonalFaresControl1.Visible = false;
						}
						else
						{
							LocalZonalOpertatorFaresControl1.JourneyDetail = publicJourneyDetail;
							LocalZonalFaresControl1.JourneyDetail = publicJourneyDetail;
							LocalZonalOpertatorFaresControl1.PrinterFriendly = this.PrinterFriendly;
							LocalZonalFaresControl1.PrinterFriendly = this.PrinterFriendly;
							LocalZonalOpertatorFaresControl1.Visible = true;
							LocalZonalFaresControl1.Visible = true;
						}
						break;
						
					case "Air":
						LocalZonalOpertatorFaresControl1.Visible = true;
						LocalZonalFaresControl1.Visible = false;
						LocalZonalOpertatorFaresControl1.JourneyDetail = publicJourneyDetail;
						LocalZonalOpertatorFaresControl1.PrinterFriendly = this.PrinterFriendly;
						break;
						
					default:
						LocalZonalOpertatorFaresControl1.Visible = false;
						LocalZonalFaresControl1.Visible = false;
						break;
				}
			}
		}
		/// <summary>
		/// Initialise static label text 
		/// </summary>
		private void SetStaticLabelText()
		{				
			toText = GetResource("LegDetailsControl.LabelToString");		
			noFaresText = GetResource("LegDetailsControl.NoFareInfoLabelText");
		}



		/// <summary>
		/// Returns the description that should be rendered for the current item.
		/// </summary>
		/// <param name="publicJourneyDetail">Current item being rendered.</param>
		/// <returns>Description string.</returns>
		private string GetDescription(PublicJourneyDetail publicJourneyDetail)
		{   
			string description = String.Empty;

			// Ignore items for which no fare information is available
			if( publicJourneyDetail.Mode == ModeType.Cycle)
				return description;
			
			// Assemble string for car leg
			if( publicJourneyDetail.Mode == ModeType.Car)
				return description;

			// Check to see if walk leg
			if( publicJourneyDetail.Mode == ModeType.Walk )
				return description;

			towardsText = GetResource("LegDetailsControl.TowardsString");

			orText = GetResource("LegDetailsControl.OrString");


			if(publicJourneyDetail.Services != null) // null check
			{
				// build the description for the first service
            
				string resourceModeQueryString = "TransportMode" + "." +
					publicJourneyDetail.Mode.ToString();
                
				string mode = GetResource(resourceModeQueryString);

				if(publicJourneyDetail.Services[0] != null) // null check
				{
					if (publicJourneyDetail.Services[0].OperatorName != null && publicJourneyDetail.Services[0].OperatorName.Length != 0)
						description += publicJourneyDetail.Services[0].OperatorName;
					else
						description += mode;

					if (publicJourneyDetail.Services[0].ServiceNumber != null && publicJourneyDetail.Services[0].ServiceNumber.Length != 0)
						description += "/" + publicJourneyDetail.Services[0].ServiceNumber;

					if (publicJourneyDetail.Services[0].DestinationBoard != null && publicJourneyDetail.Services[0].DestinationBoard.Length != 0)
						description += " " + towardsText + " " + publicJourneyDetail.Services[0].DestinationBoard;

                    else if (publicJourneyDetail.Destination != null
                            && publicJourneyDetail.Destination.Location != null  
							&& publicJourneyDetail.Destination.Location.Description != null
							&& publicJourneyDetail.Destination.Location.Description.Length != 0)
						description += " " + towardsText + " " + publicJourneyDetail.Destination.Location.Description;
				}
				else if( publicJourneyDetail.Destination.Location != null && publicJourneyDetail.Destination.Location.Description != null
						&& publicJourneyDetail.Destination.Location.Description.Length != 0)				
						description += " " + towardsText + " " + publicJourneyDetail.Destination.Location.Description;
				


				StringBuilder stringBuilder = new StringBuilder(description);
	

				// Add other services (if they exist)
				if(publicJourneyDetail.Services.Length > 1)
				{
					for(int i=1; i<publicJourneyDetail.Services.Length; i++)
					{
						if(publicJourneyDetail.Services[i] != null)
						{							
							stringBuilder.Append(" " + orText);
                
							if(publicJourneyDetail.Services[i].OperatorName != null	&& publicJourneyDetail.Services[i].OperatorName.Length != 0)													
								stringBuilder.Append(" " + publicJourneyDetail.Services[i].OperatorName);
															
							if(publicJourneyDetail.Services[i].ServiceNumber != null && publicJourneyDetail.Services[i].ServiceNumber.Length != 0)								
								stringBuilder.Append("/" + publicJourneyDetail.Services[i].ServiceNumber);

							if(publicJourneyDetail.Services[i].DestinationBoard != null	&& publicJourneyDetail.Services[i].DestinationBoard.Length != 0)								
								stringBuilder.Append(" " + towardsText + " " + publicJourneyDetail.Services[i].DestinationBoard);
						}
					}
					description = stringBuilder.ToString();
				}
			}


			return description;
		}


		#endregion

		#region Public properties 

		public bool ReturnFaresIncluded
		{
			get {return returnfaresincluded;}
			set {returnfaresincluded = value;}
		}

		/// <summary>
		/// Read/write property which describes whether the leg is priced
		/// </summary>
		public bool IsPriced
		{
			get {return isPriced;}
			set {isPriced=value;}
		}

		/// <summary>
		/// Read/Write property which sets the text of the fares above label
		/// </summary>
		public string FaresAboveText
		{
			get{return noFaresInfoText;}
			set {noFaresInfoText=value;}
		}

		/// <summary>
		/// Read/write property which describes the PublicJourneyDetail used for the leg
		/// </summary>
		public PublicJourneyDetail LegDetail
		{
			get {return publicJourneyDetail;}
			set {publicJourneyDetail=value;}
		}

		/// <summary>
		/// Read/write property which describes whether the leg is priced
		/// </summary>
		public bool TableView
		{
			get {return tableView;}
			set {tableView=value;}
		}
		

		public string GetCssClass()
		{
			if(tableView)
				return tableViewClass;
			else
				return string.Empty;

		}

        public string GetRowCssClass()
        {
            if (tableView)
                return tableViewRowClass;
            else
                return string.Empty;

        }

		public bool ReturnNoThroughFares
		{
			get {return returnNoThroughFares;}
			set {returnNoThroughFares = value;}
		}


#endregion 

	}
}
