// *********************************************** 
// NAME                 : PrintableSimpleMapControl.ascx
// AUTHOR               : Andy Lole
// DATE CREATED         : 17/10/2003 
// DESCRIPTION			: Printable SimpleMap control
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/PrintableSimpleMapControl.ascx.cs-arc  $
//
//   Rev 1.4   Oct 12 2011 12:02:20   mmodi
//Updated logic for Traffic Maps page to set and persist the selected date and times
//Resolution for 5753: Traffic Levels map does not use selected date
//
//   Rev 1.3   Jan 05 2009 14:20:48   devfactory
//XHTML Compliance Changes
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.2   Mar 31 2008 13:22:28   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:17:02   mturner
//Initial revision.
//
//   Rev 1.8   Jun 08 2006 10:08:54   mmodi
//IR4114: Added parameter to accept a formatted location name
//Resolution for 4114: Map Locations with long names are cut when Printer Friendly is printed
//
//   Rev 1.7   Feb 23 2006 19:17:02   build
//Automatically merged from branch for stream3129
//
//   Rev 1.6.1.0   Jan 10 2006 15:26:48   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.6   Jul 12 2004 19:54:46   JHaydock
//DEL 5.4.7 Merge: IR 1132
//
//   Rev 1.5   Apr 05 2004 17:13:20   CHosegood
//Del 5.2 map QA fixes
//
//   Rev 1.4   Mar 25 2004 15:18:08   CHosegood
//Del 5.2 map qa fixes
//Resolution for 674: DEL 5.2 Map QA changes
//
//   Rev 1.3   Mar 25 2004 14:39:54   CHosegood
//DEL 5.2 Map QA changes
//Resolution for 674: DEL 5.2 Map QA changes
//
//   Rev 1.2   Nov 25 2003 19:14:54   alole
//Added Map Key
//
//   Rev 1.1   Nov 21 2003 09:52:54   alole
//Updated CalendarDateTime display format.
//
//   Rev 1.0   Oct 21 2003 09:15:08   ALole
//Initial Revision


namespace TransportDirect.UserPortal.Web.Controls
{
	using System;using TransportDirect.Common.ResourceManager;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using TransportDirect.UserPortal.JourneyControl;
	using TransportDirect.UserPortal.SessionManager;
	using TransportDirect.Web.Support;
	using TransportDirect.UserPortal.Web.UserSupport;
	using TransportDirect.Common;
	

	/// <summary>
	///		Printable map control
	/// </summary>
	public partial  class PrintableSimpleMapControl : TDUserControl
	{

		protected MapKeyControl MapKeyControl1;
		
		protected void Page_Load(object sender, System.EventArgs e)
		{
			labelKey.Text = Global.tdResourceManager.GetString("MapKeyControl.labelKey", TDCultureInfo.CurrentUICulture);
            labelOverview.Text = Global.tdResourceManager.GetString("PrintableMapControl.labelOverview", TDCultureInfo.CurrentUICulture);
			//Simple map control is only used on the traffic maps page so
            //this is always a private journey
            MapKeyControl1.InitialisePrivate( true, true );
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
		
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{

        }
		#endregion
		
		public void Populate(bool isOutward, bool isInput, string locationName)
		{

			TDJourneyResult journeyResult = (TDJourneyResult)TDSessionManager.Current.JourneyResult;
			InputPageState inputPageState = (InputPageState) TDSessionManager.Current.InputPageState;
			TDJourneyViewState viewState = TDSessionManager.Current.JourneyViewState;
			
			
			if (isOutward)
			{
				if ( inputPageState.MapLocation.Description != null )
				{
					// locationName used from parameter instead of inputPageState 
					// because it has been formatted by the calling class
					labelJourneysFor.Text = locationName;
				}
				if ( inputPageState.CalendarDateTimePrintable != null )
				{
					labelDateTimeFor.Text = TDSessionManager.Current.InputPageState.CalendarDateTimePrintable.ToString( "F" );
				}
				else
				{
					labelDateTimeFor.Text = TDDateTime.Now.ToString( "F" );
				}
				labelMapScale.Text = inputPageState.MapScaleOutward.ToString(TDCultureInfo.CurrentUICulture.NumberFormat);

				imageMap.ImageUrl = inputPageState.MapUrlOutward;
				imageOverview.ImageUrl = inputPageState.OverviewMapUrlOutward;

                imageMap.AlternateText = Global.tdResourceManager.GetString
                ("langStrings", "JourneyMapControl.imageMap.AlternateText");

                imageOverview.AlternateText = Global.tdResourceManager.GetString
                    ("JourneyMapControl.imageSummaryMap.AlternateText");
			}
		}
	}
}
