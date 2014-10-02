// *********************************************** 
// NAME                 : JourneyEmissionsCompareControl.ascx
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 07/02/2007
// DESCRIPTION			: Control displaying the Journey Emissions for different Transport Modes
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/JourneyEmissionsCompareControl.ascx.cs-arc  $ 
//
//   Rev 1.14   Jul 28 2011 16:19:08   dlane
//UK11131377 - Changes for WAI compliance
//Resolution for 5712: Changes for accessibility (WAI)
//
//   Rev 1.13   Jul 15 2010 13:42:36   mmodi
//Set visibility of the Distance label when in the control is in the distance mode.
//Resolution for 5575: Text error on CO2 calculator page
//
//   Rev 1.12   Mar 04 2010 15:39:04   mmodi
//Updated to allow displaying units switch drop down in header or footer area of control
//Resolution for 5433: TD Extra - The miles/km control displayed the bottom of the page on the CO2 comparison page.
//
//   Rev 1.11   Feb 23 2010 15:23:42   mmodi
//Display the journey icon when not single mode but contains only a single vehicle mode type
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.10   Feb 18 2010 19:11:52   rbroddle
//International planner amendments
//
//   Rev 1.9   Feb 17 2010 10:21:16   rbroddle
//Updates to emissions control for display on international planner
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.8   Aug 04 2009 14:23:48   mmodi
//Updated following changes to the JourneyEmissionsHelper
//Resolution for 5307: CCN517a Web Service Find a Car Route
//
//   Rev 1.7   Feb 13 2009 14:25:16   devfactory
//No change.
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.6   Nov 19 2008 11:44:28   jfrank
//Updated for XHTML compliance
//Resolution for 5146: WAI AAA compliance work (CCN 474)
//
//   Rev 1.5   Oct 14 2008 13:28:34   mmodi
//Manual merge for stream5014
//
//   Rev 1.4   Sep 10 2008 12:00:56   mmodi
//Corrected use of Coach emission factor
//Resolution for 5109: CO2 Emissions: Coach factor is not used for coach mode journey
//
//   Rev 1.3.1.1   Sep 17 2008 12:50:08   mmodi
//Updated for xhtml compliance
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.3.1.0   Jul 30 2008 11:11:58   mmodi
//Updated for cycle journeys
//Resolution for 5014: CCN0444 - Cycle Trip Planner - Workstream
//
//   Rev 1.3   Apr 11 2008 15:12:32   mmodi
//Changed switch link to a button
//Resolution for 4856: Del 10 - Change CO2 switch to table links into buttons
//
//   Rev 1.2   Mar 31 2008 13:21:24   mturner
//Drop3 from Dev Factory
//
//   Rev DevFactory   Feb 12 2008 14:00:00   mmodi
//Corrected issue when journey passenger list is displayed for PT journeys, when it should only be displayed
//for car journeys
//
//   Rev 1.0   Nov 08 2007 13:15:26   mturner
//Initial revision.
//
//   Rev 1.19   Oct 12 2007 14:25:18   asinclair
//Updated for JourneyLegNumbering
//Resolution for 4513: 9.8 - Journey Leg Numbering
//
//   Rev 1.18   Oct 01 2007 17:10:10   asinclair
//Updated for Del 9.7 patch
//
//   Rev 1.17   Sep 21 2007 14:11:04   asinclair
//Updated to fix firefox display issues and to add an extra label
//
//   Rev 1.16   Sep 18 2007 15:23:20   asinclair
//Updated to get label string from resource manager
//
//   Rev 1.15   Sep 14 2007 17:32:08   asinclair
//Updated for co2 phase 2
//
//   Rev 1.14   Sep 13 2007 19:51:32   asinclair
//Updated for CO2 phase 2
//
//   Rev 1.13   Sep 07 2007 19:12:06   asinclair
//Updated for CO2 phase 2
//
//   Rev 1.12   Aug 31 2007 16:21:54   build
//Automatically merged from branch for stream4474
//
//   Rev 1.11.1.0   Aug 30 2007 17:57:56   asinclair
//Major Updates for Phase 2
//Resolution for 4474: DEL 9.7 Stream : Public Transport C02
//
//   Rev 1.11   Jul 04 2007 11:10:04   mmodi
//Adjust rounding logic for the Journey emissions row
//Resolution for 4461: 9.7 - Issue with Public Transport CO2 rounding on short journeys
//
//   Rev 1.10   May 31 2007 15:42:48   mmodi
//Updated code to round distance to 2dp when emissions are shown to 2dp
//Resolution for 4429: CO2: Distance value rounding should match emissions rounding
//
//   Rev 1.9   May 17 2007 11:26:54   mmodi
//Corrected compare note rounding when difference is < 0.1
//Resolution for 4413: CO2: Walk journey shows an emissions value
//
//   Rev 1.8   May 15 2007 16:09:40   mmodi
//Updated rounding conditions
//Resolution for 4413: CO2: Walk journey shows an emissions value
//
//   Rev 1.7   Apr 12 2007 11:23:22   mmodi
//Updated rounding logic for distance to display to 1dp
//Resolution for 4383: CO2: Rounding of Distance on CO2 emissions compare panel
//
//   Rev 1.6   Mar 09 2007 16:27:28   mmodi
//Updated rounding of Journey emissions to be displayed
//Resolution for 4350: CO2 Public Transport
//
//   Rev 1.5   Mar 05 2007 17:29:12   mmodi
//Updates to handle Passenger lists selections, and improve load processing
//Resolution for 4350: CO2 Public Transport
//
//   Rev 1.4   Mar 02 2007 16:46:16   mmodi
//Updates as part of work stream
//Resolution for 4350: CO2 Public Transport
//
//   Rev 1.3   Feb 28 2007 15:43:52   mmodi
//Updated visibility of emission rows dependent on transport modes for journey
//Resolution for 4350: CO2 Public Transport
//
//   Rev 1.2   Feb 27 2007 10:40:24   mmodi
//Updated for code review checklist
//Resolution for 4350: CO2 Public Transport
//
//   Rev 1.1   Feb 26 2007 11:39:54   mmodi
//Added emission bar outline images
//Resolution for 4350: CO2 Public Transport
//
//   Rev 1.0   Feb 20 2007 16:57:08   mmodi
//Initial revision.
//Resolution for 4350: CO2 Public Transport
//

using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Text;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using TransportDirect.Common;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ResourceManager;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.UserPortal.Resource;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.Web.Adapters;

namespace TransportDirect.UserPortal.Web.Controls
{
	/// <summary>
	///		Summary description for JourneyEmissionsCompareControl.
	/// </summary>
	public class JourneyEmissionsCompareControl : TDUserControl
	{
		#region Controls

		// Labels
		protected System.Web.UI.WebControls.Label labelTitle;
		protected System.Web.UI.WebControls.Label labelDistanceUnits1;
        protected System.Web.UI.WebControls.Label labelDistanceUnits2;
		protected System.Web.UI.WebControls.Label labelJourneyDistance;
		protected TransportDirect.UserPortal.Web.Controls.ScriptableDropDownList dropdownlistUnits1;
        protected TransportDirect.UserPortal.Web.Controls.ScriptableDropDownList dropdownlistUnits2;

		protected System.Web.UI.WebControls.Label labelCompareJourney;
		protected System.Web.UI.WebControls.Label labelCompareTransportNote;

		protected System.Web.UI.WebControls.Label labelJourneyTitle;
		protected System.Web.UI.WebControls.Label labelOutwardTitle;
		protected System.Web.UI.WebControls.Label labelReturnTitle;
		protected System.Web.UI.WebControls.Label labelJourneyEmits;
		protected System.Web.UI.WebControls.Label labelOutwardEmits;
		protected System.Web.UI.WebControls.Label labelReturnEmits;

		protected System.Web.UI.WebControls.Label labelCarTitle;
		protected System.Web.UI.WebControls.Label labelCarLargeTitle;
		protected System.Web.UI.WebControls.Label labelTrainTitle;
		protected System.Web.UI.WebControls.Label labelBusTitle;
		protected System.Web.UI.WebControls.Label labelCoachTitle;
		protected System.Web.UI.WebControls.Label labelPlaneTitle;

		protected System.Web.UI.WebControls.Label labelSmallCarEmits;
		protected System.Web.UI.WebControls.Label labelCarLargeEmits;
		protected System.Web.UI.WebControls.Label labelTrainEmits;
		protected System.Web.UI.WebControls.Label labelBusEmits;
		protected System.Web.UI.WebControls.Label labelCoachEmits;
		protected System.Web.UI.WebControls.Label labelPlaneEmits;

		protected System.Web.UI.WebControls.Label labelSmallCarEmissions;
		protected System.Web.UI.WebControls.Label labelLargeCarEmissions;
		protected System.Web.UI.WebControls.Label labelBusEmissions;
		protected System.Web.UI.WebControls.Label labelCoachEmissions;
		protected System.Web.UI.WebControls.Label labelTrainEmissions;
		protected System.Web.UI.WebControls.Label labelPlaneEmissions;

		protected System.Web.UI.WebControls.Label labelYourJourneyEmissions;

		protected System.Web.UI.WebControls.Label labelWith;
		protected System.Web.UI.WebControls.Label labelPassengers;
		protected System.Web.UI.WebControls.DropDownList listCarPassengers;

		protected System.Web.UI.WebControls.Label labelLargeWith;
		protected System.Web.UI.WebControls.Label labelLargePassengers;
		protected System.Web.UI.WebControls.DropDownList listCarLargePassengers;

		protected System.Web.UI.WebControls.Label labelJourneyWith;
		protected System.Web.UI.WebControls.Label labelJourneyPassengers;
		protected System.Web.UI.WebControls.DropDownList listJourneyPassengers;

		protected System.Web.UI.WebControls.Label labelTableSmallCarWith;
		protected System.Web.UI.WebControls.Label labelTableSmallCarPassengers;
		protected System.Web.UI.WebControls.DropDownList listTableSmallCarPassengers;

		protected System.Web.UI.WebControls.Label labelTableLargeCarWith;
		protected System.Web.UI.WebControls.Label labelTableLargeCarPassengers;
		protected System.Web.UI.WebControls.DropDownList listTableLargeCarPassengers;

		protected System.Web.UI.WebControls.Label labelYourJourneyWith;
		protected System.Web.UI.WebControls.Label labelYourJourneyPassengers;
		protected System.Web.UI.WebControls.DropDownList listYourJourneyPassengers;

		protected System.Web.UI.WebControls.Label labelOutwardWith;
		protected System.Web.UI.WebControls.Label labelOutwardPassengers;
		protected System.Web.UI.WebControls.Label maxEmissionsValueLabel;
		protected System.Web.UI.WebControls.DropDownList listOutwardPassengers;
		protected System.Web.UI.HtmlControls.HtmlGenericControl graphdiv;
		protected System.Web.UI.HtmlControls.HtmlGenericControl graphcoachdiv;
		protected System.Web.UI.HtmlControls.HtmlGenericControl graphtraindiv;
		protected System.Web.UI.HtmlControls.HtmlGenericControl graphbusdiv;
		protected System.Web.UI.HtmlControls.HtmlGenericControl graphplanediv;
		protected System.Web.UI.HtmlControls.HtmlGenericControl graphsmallcardiv;
		protected System.Web.UI.HtmlControls.HtmlGenericControl journeydiv;


		protected System.Web.UI.WebControls.Label labelTransportHeader;
		protected System.Web.UI.WebControls.Label labelEmissionsHeader;
		protected System.Web.UI.WebControls.Label labelCarTableTitle;
		protected System.Web.UI.WebControls.Label labelLargeCarTableTitle;
		protected System.Web.UI.WebControls.Label labelBusTableTitle;
		protected System.Web.UI.WebControls.Label labelCoachTableTitle;
		protected System.Web.UI.WebControls.Label labelTrainTableTitle;
		protected System.Web.UI.WebControls.Label labelPlaneTableTitle;
		protected System.Web.UI.WebControls.Label labelYourJourney;
		protected System.Web.UI.WebControls.Label labelYourJourneyTransport;
		protected System.Web.UI.WebControls.Label labelYourJourneyEmissionsHeader;
		protected System.Web.UI.WebControls.Label labelScreenReaderTable;


		protected System.Web.UI.WebControls.Label labelLow;
		protected System.Web.UI.WebControls.Label labelMedium;
		protected System.Web.UI.WebControls.Label labelHigh;
		protected System.Web.UI.WebControls.Label labelVeryHigh;
		protected System.Web.UI.WebControls.Label labelGraphKey;

		protected System.Web.UI.WebControls.Label labelYourJourneyPerPassenger;
		protected System.Web.UI.WebControls.Label labelSmallCarPerPassenger;
		protected System.Web.UI.WebControls.Label labelLargeCarPerPassenger;
		protected System.Web.UI.WebControls.Label labelBusPerPassenger;
		protected System.Web.UI.WebControls.Label labelCoachPerPassenger;
		protected System.Web.UI.WebControls.Label labelTrainPerPassenger;
		protected System.Web.UI.WebControls.Label labelPlanePerPassenger;


		

		#region Images
		protected TransportDirect.UserPortal.Web.Controls.TDImage imageJourney;
		protected TransportDirect.UserPortal.Web.Controls.TDImage imageOutwardJourney;
		protected TransportDirect.UserPortal.Web.Controls.TDImage imageReturnJourney;
		protected TransportDirect.UserPortal.Web.Controls.TDImage imageCar;
		protected TransportDirect.UserPortal.Web.Controls.TDImage imageLargeCar;
		protected TransportDirect.UserPortal.Web.Controls.TDImage imageTrain;
		protected TransportDirect.UserPortal.Web.Controls.TDImage imageBus;
		protected TransportDirect.UserPortal.Web.Controls.TDImage imageCoach;
		protected TransportDirect.UserPortal.Web.Controls.TDImage imagePlane;
		protected TransportDirect.UserPortal.Web.Controls.TDImage imageEmissionsJourney;
		protected TransportDirect.UserPortal.Web.Controls.TDImage journeyEmissionsColoured;
		
		protected TransportDirect.UserPortal.Web.Controls.TDImage imageJourneyEmissionsBar;
		protected TransportDirect.UserPortal.Web.Controls.TDImage imageJourneyEmissionsBarOutline;
		protected TransportDirect.UserPortal.Web.Controls.TDImage imageJourneyEmissionsBarEnd;

		protected TransportDirect.UserPortal.Web.Controls.TDImage imageOutwardEmissionsBar;
		protected TransportDirect.UserPortal.Web.Controls.TDImage imageOutwardEmissionsBarOutline;
		protected TransportDirect.UserPortal.Web.Controls.TDImage imageOutwardEmissionsBarEnd;

		protected TransportDirect.UserPortal.Web.Controls.TDImage imageReturnEmissionsBar;
		protected TransportDirect.UserPortal.Web.Controls.TDImage imageReturnEmissionsBarOutline;
		protected TransportDirect.UserPortal.Web.Controls.TDImage imageReturnEmissionsBarEnd;

		protected TransportDirect.UserPortal.Web.Controls.TDImage imageCarEmissionsBar;
		protected TransportDirect.UserPortal.Web.Controls.TDImage imageCarEmissionsBarOutline;
		protected TransportDirect.UserPortal.Web.Controls.TDImage imageCarEmissionsBarEnd;

		protected TransportDirect.UserPortal.Web.Controls.TDImage imageCarLargeEmissionsBar;
		protected TransportDirect.UserPortal.Web.Controls.TDImage imageCarLargeEmissionsBarOutline;
		protected TransportDirect.UserPortal.Web.Controls.TDImage imageCarLargeEmissionsBarEnd;

		protected TransportDirect.UserPortal.Web.Controls.TDImage imageTrainEmissionsBar;
		protected TransportDirect.UserPortal.Web.Controls.TDImage imageTrainEmissionsBarOutline;
		protected TransportDirect.UserPortal.Web.Controls.TDImage imageTrainEmissionsBarEnd;

		protected TransportDirect.UserPortal.Web.Controls.TDImage imageBusEmissionsBar;
		protected TransportDirect.UserPortal.Web.Controls.TDImage imageBusEmissionsBarOutline;
		protected TransportDirect.UserPortal.Web.Controls.TDImage imageBusEmissionsBarEnd;

		protected TransportDirect.UserPortal.Web.Controls.TDImage imageCoachEmissionsBar;
		protected TransportDirect.UserPortal.Web.Controls.TDImage imageCoachEmissionsBarOutline;
		protected TransportDirect.UserPortal.Web.Controls.TDImage imageCoachEmissionsBarEnd;

		protected TransportDirect.UserPortal.Web.Controls.TDImage imagePlaneEmissionsBar;
		protected TransportDirect.UserPortal.Web.Controls.TDImage imagePlaneEmissionsBarOutline;
		protected TransportDirect.UserPortal.Web.Controls.TDImage imagePlaneEmissionsBarEnd;
		protected TransportDirect.UserPortal.Web.Controls.TDImage imageCoachBlackBar;
		protected TransportDirect.UserPortal.Web.Controls.TDImage imageBusBlackBar;
		protected TransportDirect.UserPortal.Web.Controls.TDImage imagePlaneBlackBar;
		protected TransportDirect.UserPortal.Web.Controls.TDImage imageSmallCarBlackBar;
		protected TransportDirect.UserPortal.Web.Controls.TDImage imageLargeCarBlackBar;
		protected TransportDirect.UserPortal.Web.Controls.TDImage imageTrainBlackBar;
		protected TransportDirect.UserPortal.Web.Controls.TDImage imageYourJourneyBlackBar;


		
		#endregion

		#region Table, rows, and cells

		protected System.Web.UI.WebControls.TableRow rowHeading;
		protected System.Web.UI.WebControls.TableRow rowJourneyDistance;
		protected System.Web.UI.WebControls.TableCell cellEmissionsTitle;
		protected System.Web.UI.WebControls.TableCell cellDistanceUnits;
		protected System.Web.UI.WebControls.TableCell cellJourneyDistance;
		
		protected System.Web.UI.WebControls.TableRow rowCompareJourney;
		protected System.Web.UI.WebControls.TableCell cellCompareJourneyText;

		protected System.Web.UI.WebControls.TableRow rowJourney;
		protected System.Web.UI.WebControls.TableCell cellJourneyTitle;
		protected System.Web.UI.WebControls.TableCell cellJourneyImage;
		protected System.Web.UI.WebControls.TableCell cellJourneyEmissions;

		protected System.Web.UI.WebControls.TableRow rowOutwardJourney;
		protected System.Web.UI.WebControls.TableCell cellOutwardTitle;
		protected System.Web.UI.WebControls.TableCell cellOutwardImage;
		protected System.Web.UI.WebControls.TableCell cellOutwardEmissions;

		protected System.Web.UI.WebControls.TableRow rowReturnJourney;
		protected System.Web.UI.WebControls.TableCell cellReturnTitle;
		protected System.Web.UI.WebControls.TableCell cellReturnImage;
		protected System.Web.UI.WebControls.TableCell cellReturnEmissions;

		protected System.Web.UI.WebControls.TableRow rowCarEmissions;
		protected System.Web.UI.WebControls.TableCell cellCarTitle;
		protected System.Web.UI.WebControls.TableCell cellCarImage;
		protected System.Web.UI.WebControls.TableCell cellCarEmissions;

		protected System.Web.UI.WebControls.TableRow rowCarLargeEmissions;
		protected System.Web.UI.WebControls.TableCell cellCarLargeTitle;
		protected System.Web.UI.WebControls.TableCell cellCarLargeImage;
		protected System.Web.UI.WebControls.TableCell cellCarLargeEmissions;
		
		protected System.Web.UI.WebControls.TableRow rowTrainEmissions;
		protected System.Web.UI.WebControls.TableCell cellTrainTitle;
		protected System.Web.UI.WebControls.TableCell cellTrainImage;
		protected System.Web.UI.WebControls.TableCell cellTrainEmissions;

		protected System.Web.UI.WebControls.TableRow rowBusEmissions;
		protected System.Web.UI.WebControls.TableCell cellBusTitle;
		protected System.Web.UI.WebControls.TableCell cellBusImage;
		protected System.Web.UI.WebControls.TableCell cellBusEmissions;

		protected System.Web.UI.WebControls.TableRow rowCoachEmissions;
		protected System.Web.UI.WebControls.TableCell cellCoachTitle;
		protected System.Web.UI.WebControls.TableCell cellCoachImage;
		protected System.Web.UI.WebControls.TableCell cellCoachEmissions;

		protected System.Web.UI.WebControls.TableRow rowPlaneEmissions;
		protected System.Web.UI.WebControls.TableCell cellPlaneTitle;
		protected System.Web.UI.WebControls.TableCell cellPlaneImage;
		protected System.Web.UI.WebControls.TableCell cellPlaneEmissions;

		protected System.Web.UI.WebControls.TableRow smallCarRow;
		protected System.Web.UI.WebControls.TableRow largeCarRow;
		protected System.Web.UI.WebControls.TableRow busRow;
		protected System.Web.UI.WebControls.TableRow coachRow;
		protected System.Web.UI.WebControls.TableRow trainRow;
		protected System.Web.UI.WebControls.TableRow planeRow;
		protected System.Web.UI.WebControls.TableRow yourJourneyTableRow;

		#endregion

		#endregion

        protected TransportDirect.UserPortal.Web.Controls.TDButton buttonChangeView;
        protected TransportDirect.UserPortal.Web.Controls.TDButton buttonShowJourney;

		#region Constants

		private const string RowEven = "eevenRow";
		private const string RowOdd = "";

		private const string ImageEmissionBarJourneyURL = "JourneyEmissionsCompareControl.ImageEmissionBar.Journey.URL";
		private const string ImageEmissionBarJourneyOutlineURL = "JourneyEmissionsCompareControl.ImageEmissionBar.JourneyOutline.URL";
		private const string ImageEmissionBarCompareURL = "JourneyEmissionsCompareControl.ImageEmissionBar.Compare.URL";
		private const string ImageEmissionBarCompareOutlineURL = "JourneyEmissionsCompareControl.ImageEmissionBar.CompareOutline.URL";
		private const string ImageEmissionBarAltText = "JourneyEmissionsCompareControl.ImageEmissionBar.AlternateText";

		private const string ImageCarURL = "JourneyEmissionsDistanceInputControl.ImageCar.URL";
		private const string ImageCarAltText = "JourneyEmissionsDistanceInputControl.ImageCar.AlternateText";
		private const string ImageSmallCarAltText = "JourneyEmissionsDistanceInputControl.ImageSmallCar.AlternateText";
		private const string ImageLargeCarAltText = "JourneyEmissionsDistanceInputControl.ImageLargeCar.AlternateText";
		private const string ImageSmallCarURL = "JourneyEmissionsDistanceInputControl.ImageSmallCar.URL";
		private const string ImageLargeCarURL = "JourneyEmissionsDistanceInputControl.ImageLargeCar.URL";
		private const string ImageMediumCarURL = "JourneyEmissionsDistanceInputControl.ImageMediumCar.URL";
		private const string ImageMediumCarAltText = "JourneyEmissionsDistanceInputControl.ImageCar.AlternateText";
		private const string ImageTrainURL = "JourneyEmissionsDistanceInputControl.ImageTrain.URL";
		private const string ImageTrainAltText = "JourneyEmissionsDistanceInputControl.ImageTrain.AlternateText";
		private const string ImagePlaneURL = "JourneyEmissionsDistanceInputControl.ImagePlane.URL";
		private const string ImagePlaneAltText = "JourneyEmissionsDistanceInputControl.ImagePlane.AlternateText";
		private const string ImageBusURL = "JourneyEmissionsDistanceInputControl.ImageBus.URL";
		private const string ImageBusAltText = "JourneyEmissionsDistanceInputControl.ImageBus.AlternateText";
		private const string ImageCoachURL = "JourneyEmissionsDistanceInputControl.ImageCoach.URL";
		private const string ImageCoachAltText = "JourneyEmissionsDistanceInputControl.ImageCoach.AlternateText";
        private const string ImageCycleURL = "JourneyEmissionsDistanceInputControl.ImageCycle.URL";
        private const string ImageCycleAltText = "JourneyEmissionsDistanceInputControl.ImageCycle.AlternateText";

		private const string ImageEmissionsColouredURL = "JourneyEmissionsColouredBar.ImageCar.URL";
		private const string ImageEmissionsBlackURL = "JourneyEmissionsBlackBar.ImageCar.URL";
		private const string ImageEmissionsWhiteURL = "JourneyEmissionsWhiteBar.ImageCar.URL";

		#endregion

		#region Private variables

		private IDataServices populator;		
		private JourneyEmissionsHelper emissionsHelper;
		private JourneyEmissionsCompareMode journeyEmissionsCompareMode;
		private JourneyEmissionsVisualMode journeyEmissionsVisualMode;

		private bool nonprintable;
		private bool sessionManager = false;
		private bool itineraryManger = false;

		private bool showCarEmissions = true;
		private bool showCarLargeEmissions = true;
		private bool showTrainEmissions = true;
		private bool showBusEmissions = true;
		private bool showCoachEmissions = true;
		private bool showPlaneEmissions = true;

		private bool showEmissionsTable = false;

		private decimal journeyDistance;
		private RoadUnitsEnum roadUnits;
		private string journeyDistanceToDisplay = string.Empty;
        private bool showHeaderDistanceUnitsDropDown = true;

        private bool pageLandingActive;

        private decimal emissionsJourney;
		private decimal emissionsOutwardJourney;
		private decimal emissionsReturnJourney;

		private decimal emissionsCarValue;
		private decimal emissionsCarValueOriginal; //retains the original value before dividing by passengers
		private decimal emissionsLargeCarValue;
		private decimal emissionsLargeCarValueOriginal; //retains the original value before dividing by passengers
		private decimal emissionsSmallCarValue;
		private decimal emissionsSmallCarValueOriginal;
		private decimal emissionsTrainValue;
		private decimal emissionsBusValue;
		private decimal emissionsCoachValue;
		private decimal emissionsPlaneValue;
		protected System.Web.UI.WebControls.Panel panel1;
		protected System.Web.UI.WebControls.Label graphtest;
		protected System.Web.UI.WebControls.Panel emissionsGraphicalPanel;
		protected System.Web.UI.WebControls.Panel emissionsTableViewPanel;
		protected TransportDirect.UserPortal.Web.Controls.TDImage graphtrainimage;
		protected TransportDirect.UserPortal.Web.Controls.TDImage  trainEmissionsColoured;
		protected TransportDirect.UserPortal.Web.Controls.TDImage graphimage;
		protected TransportDirect.UserPortal.Web.Controls.TDImage graphcoachimage;
		protected TransportDirect.UserPortal.Web.Controls.TDImage  coachEmissionsColoured;
		protected TransportDirect.UserPortal.Web.Controls.TDImage  graphplaneimage;
		protected TransportDirect.UserPortal.Web.Controls.TDImage planeEmissionsColoured;
		protected TransportDirect.UserPortal.Web.Controls.TDImage graphsmallcarimage;
		protected TransportDirect.UserPortal.Web.Controls.TDImage  smallCarEmissionsColoured;
		protected TransportDirect.UserPortal.Web.Controls.TDImage  largeCarEmissionsColoured;
		protected TransportDirect.UserPortal.Web.Controls.TDImage graphBottom;
		protected TransportDirect.UserPortal.Web.Controls.TDImage busEmissionsColoured;
		protected TransportDirect.UserPortal.Web.Controls.TDImage graphbusimage;


		protected System.Web.UI.WebControls.Table tableJourneyEmissionsCompare;
		protected System.Web.UI.WebControls.Table tableEmissionsGraph;
		protected System.Web.UI.WebControls.Table tableEmissionsMode;
		protected System.Web.UI.WebControls.Table yourJourneyTable;
		protected System.Web.UI.WebControls.Panel yourJourneyTablePanel;

		private ModeType[] modes; // used for transport modes within a journey

		#endregion

		#region Page_Load, Page_PreRender

		/// <summary>
		/// Page Load
		/// </summary>
		private void Page_Load(object sender, System.EventArgs e)
		{
			if ((!IsPostBack))
			{
				populator = (IDataServices)TDServiceDiscovery.Current[ServiceDiscoveryKey.DataServices];
				populator.LoadListControl(DataServiceType.UnitsDrop, dropdownlistUnits1, Global.tdResourceManager);
                populator.LoadListControl(DataServiceType.UnitsDrop, dropdownlistUnits2, Global.tdResourceManager);

				PopulatePassengerList();				
			}
			PopulateLabels();
			PopulateImages();
			AlignServerWithClient();

			emissionsHelper = new JourneyEmissionsHelper();
		}


		/// <summary>
		/// Page PreRender
		/// </summary>
		private void Page_PreRender(object sender, System.EventArgs e)
		{
			// Load properties from the session in case this is a postback because user changed number of 
			// car passengers.
			LoadPropertiesFromSession();

			journeyEmissionsVisualMode = TDSessionManager.Current.JourneyEmissionsPageState.JourneyEmissionsVisualMode;

			if(journeyEmissionsVisualMode == JourneyEmissionsVisualMode.Table)
			{
                buttonChangeView.Text = GetResource("JourneyEmissionsCompareControl.ChangeViewHyperlink.ShowGraphical.Text");
				emissionsTableViewPanel.Visible = true;
				emissionsGraphicalPanel.Visible = false;
				yourJourneyTableRow.Visible = true;
				rowJourney.Visible = false;
				labelScreenReaderTable.Visible = false;
				
			}
			else
			{
                buttonChangeView.Text = GetResource("JourneyEmissionsCompareControl.ChangeViewHyperlink.ShowTable.Text");

				emissionsTableViewPanel.Visible = false;
				emissionsGraphicalPanel.Visible = true;
				yourJourneyTableRow.Visible = false;
				rowJourney.Visible = true;
				labelScreenReaderTable.Visible = true;
			}

            //showJourney button for international planner
            buttonShowJourney.Text = GetResource("JourneyEmissionsCompareControl.showJourneyHyperlink.ShowGraphical.Text");

			this.DataBind();

			// If this control is in Journey mode, then the distance used is the journey's distance

				if (UseItineraryManager)
				{// If there is no Itinerary manager, then override must set to false to prevent 
				 // later calls in this control
					if (TDItineraryManager.Current != null)
                        journeyDistance = emissionsHelper.GetJourneyDistance(TDItineraryManager.Current);
					else
						UseItineraryManager = false;
				}
				else if (UseSessionManager)
				{
					journeyDistance = emissionsHelper.GetJourneyDistance(TDSessionManager.Current);
				}


			CalculateEmissions();
			PopulateEmissionLabels();
			PopulateEmissionBarImages();
			SetEmissionRowVisibility();
			PopulateJourneyImages();

			SetDistanceLabel();
			SetCompareTransportNote();
			
			#region Javascript related
			// Only want to do this if on the nonprintable page.
			if (nonprintable)
				EnableScriptableObjects();

			string PageName = this.PageId.ToString();	
			dropdownlistUnits1.Action = "ChangeUnits('"+ GetHiddenInputId + "', '" + PageName + "', this)";
            dropdownlistUnits2.Action = "ChangeUnits('" + GetHiddenInputId + "', '" + PageName + "', this)";

			AlignClientWithServer();
			#endregion

			// Have to save the public properties to session in case there is a page postback because
			// the user changed the number of Car Passengers. Allows the page to retain the distance
			// previously entered by user and recalculate the emissions
			SavePropertiesToSession();

			// Display appropriate rows dependent on mode this control is in
			SetControlVisibility();

			//SetRowColours();

			SetPrintableControls();
			
		}

		#endregion

		#region Private methods

		#region Populate Labels and Images

		/// <summary>
		/// Populates the label text
		/// </summary>
		private void PopulateLabels()
		{
			// Set up label text
			labelDistanceUnits1.Text = GetResource("JourneyEmissionsCompareControl.DistanceUnits");
            labelDistanceUnits2.Text = GetResource("JourneyEmissionsCompareControl.DistanceUnits");

			labelJourneyTitle.Text = GetResource("JourneyEmissionsCompareControl.JourneyTitle");
			labelOutwardTitle.Text = GetResource("JourneyEmissionsCompareControl.OutwardTitle");
			labelReturnTitle.Text = GetResource("JourneyEmissionsCompareControl.ReturnTitle");
			labelCompareJourney.Text = GetResource("JourneyEmissionsCompareControl.JourneyDistanceTableView");
			labelCompareJourney.Text = GetResource("JourneyEmissionsCompareControl.CompareJourney");
			labelCarTitle.Text = GetResource("JourneyEmissionsCompareControl.CarTitle");
			labelCarLargeTitle.Text = GetResource("JourneyEmissionsCompareControl.LargeCarTitle");
			labelTrainTitle.Text = GetResource("JourneyEmissionsCompareControl.TrainTitle");
			labelBusTitle.Text = GetResource("JourneyEmissionsCompareControl.BusTitle");
			labelCoachTitle.Text = GetResource("JourneyEmissionsCompareControl.CoachTitle");
			labelPlaneTitle.Text = GetResource("JourneyEmissionsCompareControl.PlaneTitle");

			labelCarTableTitle.Text = labelCarTitle.Text;
			labelLargeCarTableTitle.Text = labelCarLargeTitle.Text;
			labelTrainTableTitle.Text = labelTrainTitle.Text;
			labelBusTableTitle.Text = labelBusTitle.Text;
			labelCoachTableTitle.Text = labelCoachTitle.Text;
			labelPlaneTableTitle.Text = labelPlaneTitle.Text;

			labelTransportHeader.Text = GetResource("JourneyEmissionsCompareControl.Transport");
			labelEmissionsHeader.Text = GetResource("JourneyEmissionsCompareControl.CO2Emissions");

			labelYourJourney.Text = GetResource("JourneyEmissionsCompareControl.JourneyTitle");
			labelYourJourneyTransport.Text = GetResource("JourneyEmissionsCompareControl.Transport");
			labelYourJourneyEmissionsHeader.Text = GetResource("JourneyEmissionsCompareControl.CO2Emissions");

			// labels for the passenger number drop down lists
			labelWith.Text = GetResource("JourneyEmissionsCompareControl.With");
			labelPassengers.Text = GetResource("JourneyEmissionsCompareControl.Passenger") 
				+ GetResource("JourneyEmissionsCompareControl.Passenger.End");
			labelLargeWith.Text = GetResource("JourneyEmissionsCompareControl.With");
			labelLargePassengers.Text = GetResource("JourneyEmissionsCompareControl.Passenger") 
				+ GetResource("JourneyEmissionsCompareControl.Passenger.End");
			labelJourneyWith.Text = labelWith.Text;
			
			//The following label will need to change if it is a combined public & private transport journey
			//and depending on which direction is the private journey.  This is done in CalculateEmissions()
			labelJourneyPassengers.Text = labelPassengers.Text;
		
			labelOutwardWith.Text = labelWith.Text;
			labelOutwardPassengers.Text = labelPassengers.Text;

			labelTableSmallCarWith.Text = labelWith.Text;
			labelTableSmallCarPassengers.Text = labelPassengers.Text;
			
			labelTableLargeCarWith.Text = labelWith.Text;
			labelTableLargeCarPassengers.Text = labelPassengers.Text;

			labelYourJourneyWith.Text = labelWith.Text;
			labelYourJourneyPassengers.Text = labelPassengers.Text;

			labelLow.Text = GetResource("JourneyEmissionsCompareControl.labelLow");
			labelMedium.Text = GetResource("JourneyEmissionsCompareControl.labelMedium");
			labelHigh.Text = GetResource("JourneyEmissionsCompareControl.labelHigh");
			labelVeryHigh.Text = GetResource("JourneyEmissionsCompareControl.labelVeryHigh");
			labelGraphKey.Text = GetResource("JourneyEmissionsCompareControl.labelGraphKey");

			labelLow.Attributes.Add("title", GetResource("JourneyEmissionsCompareControl.labelLow.TitleText"));
			labelVeryHigh.Attributes.Add("title", GetResource("JourneyEmissionsCompareControl.labelHigh.TitleText"));
			
			labelScreenReaderTable.Text = GetResource("JourneyEmissionsCompareControl.labelScreenreader.Text");

			labelYourJourneyPerPassenger.Text = GetResource("JourneyEmissionsCompareControl.labelScreenreader.PerPassenger.Text");
			labelSmallCarPerPassenger.Text = labelYourJourneyPerPassenger.Text;
			labelLargeCarPerPassenger.Text = labelYourJourneyPerPassenger.Text;
			labelBusPerPassenger.Text = labelYourJourneyPerPassenger.Text;
			labelCoachPerPassenger.Text = labelYourJourneyPerPassenger.Text;
			labelTrainPerPassenger.Text	= labelYourJourneyPerPassenger.Text;
			labelPlanePerPassenger.Text = labelYourJourneyPerPassenger.Text;
		
		}

		/// <summary>
		/// Populates the labels showing emissions text above the Emission bar images
		/// </summary>
		private void PopulateEmissionLabels()
		{
			labelJourneyEmits.Text = string.Format(
				GetResource("JourneyEmissionsCompareControl.Emits"),
				emissionsJourney.ToString());

			labelOutwardEmits.Text = string.Format(
				GetResource("JourneyEmissionsCompareControl.Emits"),
				emissionsOutwardJourney.ToString());

			labelReturnEmits.Text = string.Format(
				GetResource("JourneyEmissionsCompareControl.Emits"),
				emissionsReturnJourney.ToString());

			labelSmallCarEmits.Text = string.Format(
				GetResource("JourneyEmissionsCompareControl.Emits"),
				emissionsSmallCarValue.ToString());

			labelSmallCarEmissions.Text = string.Format(
				GetResource("JourneyEmissionsCompareControl.Emits"),
				emissionsSmallCarValue.ToString());

			labelCarLargeEmits.Text = string.Format(
				GetResource("JourneyEmissionsCompareControl.Emits"),
				emissionsLargeCarValue.ToString());

			labelLargeCarEmissions.Text = string.Format(
				GetResource("JourneyEmissionsCompareControl.Emits"),
				emissionsLargeCarValue.ToString());

			labelTrainEmits.Text = string.Format(
				GetResource("JourneyEmissionsCompareControl.Emits"),
				emissionsTrainValue.ToString());

			labelTrainEmissions.Text = string.Format(
				GetResource("JourneyEmissionsCompareControl.Emits"),
				emissionsTrainValue.ToString());

			labelBusEmits.Text = string.Format(
				GetResource("JourneyEmissionsCompareControl.Emits"),
				emissionsBusValue.ToString());

			labelBusEmissions.Text = string.Format(
				GetResource("JourneyEmissionsCompareControl.Emits"),
				emissionsBusValue.ToString());

			labelCoachEmits.Text = string.Format(
				GetResource("JourneyEmissionsCompareControl.Emits"),
				emissionsCoachValue.ToString());

			labelCoachEmissions.Text = string.Format(
				GetResource("JourneyEmissionsCompareControl.Emits"),
				emissionsCoachValue.ToString());

			labelPlaneEmits.Text = string.Format(
				GetResource("JourneyEmissionsCompareControl.Emits"),
				emissionsPlaneValue.ToString());

			labelPlaneEmissions.Text = string.Format(
				GetResource("JourneyEmissionsCompareControl.Emits"),
				emissionsPlaneValue.ToString());

			labelYourJourneyEmissions.Text = string.Format(
				GetResource("JourneyEmissionsCompareControl.Emits"),
				emissionsJourney.ToString());

		}

		/// <summary>
		/// Populates and dynamically sets the Emission bar images
		/// </summary>
		private void PopulateEmissionBarImages()
		{		
			int height = 15;

			// Get the max emission value of all the transport modes
			decimal maxEmissionValue = MaxEmissionValue();

			decimal half = 0.5M;

			decimal rounded = Decimal.Round(maxEmissionValue, 0);
			if (rounded < maxEmissionValue)
			{
				rounded = Decimal.Round(maxEmissionValue + half, 0);
			}
			
			int maxValue = (int)rounded;
			
			maxEmissionsValueLabel.Text = maxValue.ToString();


			int trainwidth = JourneyEmissionsHelper.NewEmissionsBarWidth(emissionsTrainValue, maxEmissionValue);
			string helpTrain =  "LEFT:" +  trainwidth +"px; WIDTH:300px; POSITION: absolute; TOP: 5px";
			graphtraindiv.Attributes.Add("style", helpTrain);

			int trainimagewidth = (200 - trainwidth);
			graphtrainimage.Attributes.Add("width", trainimagewidth.ToString());

			graphtrainimage.ImageUrl = GetResource(ImageEmissionsWhiteURL);
			trainEmissionsColoured.ImageUrl = GetResource(ImageEmissionsColouredURL);

			int buswidth = JourneyEmissionsHelper.NewEmissionsBarWidth(emissionsBusValue, maxEmissionValue);
			string helpBus =  "LEFT:" +  buswidth +"px; WIDTH:300px; POSITION: absolute; TOP: 5px";
			graphbusdiv.Attributes.Add("style", helpBus);

			int busumagewidth = (200 -buswidth);
			graphbusimage.Attributes.Add("width", busumagewidth.ToString());

			graphbusimage.ImageUrl = GetResource(ImageEmissionsWhiteURL);
			busEmissionsColoured.ImageUrl = GetResource(ImageEmissionsColouredURL);

			int smallwidth = JourneyEmissionsHelper.NewEmissionsBarWidth(emissionsSmallCarValue, maxEmissionValue);
			string carhelp = "LEFT:" +  smallwidth +"px; POSITION: absolute; TOP: 5px";
			graphsmallcardiv.Attributes.Add("style", carhelp);

			int carimagewidth = (200 - smallwidth);
			graphsmallcarimage.Attributes.Add("width", carimagewidth.ToString());
            		
			graphsmallcarimage.ImageUrl = GetResource(ImageEmissionsWhiteURL);
			smallCarEmissionsColoured.ImageUrl = GetResource(ImageEmissionsColouredURL);

			int width = JourneyEmissionsHelper.NewEmissionsBarWidth(emissionsLargeCarValue, maxEmissionValue);
			string help = "LEFT:" +  width +"px; POSITION: absolute; TOP: 5px";
			graphdiv.Attributes.Add("style", help);

			int largecarimagewidth = (200 - width);
			graphimage.Attributes.Add("width", largecarimagewidth.ToString());

			graphimage.ImageUrl = GetResource(ImageEmissionsWhiteURL);
			largeCarEmissionsColoured.ImageUrl = GetResource(ImageEmissionsColouredURL);

			graphBottom.ImageUrl = GetResource(ImageEmissionsBlackURL);

			int coachwidth = JourneyEmissionsHelper.NewEmissionsBarWidth(emissionsCoachValue, maxEmissionValue);
			string helpCoach =  "LEFT:" +  coachwidth +"px; WIDTH:300px; POSITION: absolute; TOP: 5px";
			graphcoachdiv.Attributes.Add("style", helpCoach);

			int coachimagewidth = (200 - coachwidth);
			graphcoachimage.Attributes.Add("width", coachimagewidth.ToString());

			graphcoachimage.ImageUrl = GetResource(ImageEmissionsWhiteURL);
			coachEmissionsColoured.ImageUrl = GetResource(ImageEmissionsColouredURL);
            
			int planewidth = JourneyEmissionsHelper.NewEmissionsBarWidth(emissionsPlaneValue, maxEmissionValue);
			string helpPlane =  "LEFT:" +  planewidth +"px; WIDTH:300px; POSITION: absolute; TOP: 5px";
			graphplanediv.Attributes.Add("style", helpPlane);
			
			int planeimagewidth = (200 - planewidth);
			graphplaneimage.Attributes.Add("width", planeimagewidth.ToString());
			graphplaneimage.ImageUrl = GetResource(ImageEmissionsWhiteURL);

			planeEmissionsColoured.ImageUrl =  GetResource(ImageEmissionsColouredURL);

            // Alt text not required
            trainEmissionsColoured.AlternateText = " ";
            busEmissionsColoured.AlternateText = " ";
            coachEmissionsColoured.AlternateText = " ";
            smallCarEmissionsColoured.AlternateText = " ";
            largeCarEmissionsColoured.AlternateText = " ";
            planeEmissionsColoured.AlternateText = " ";

            // Alt text not required
            graphtrainimage.AlternateText = " ";
            graphbusimage.AlternateText = " ";
            graphcoachimage.AlternateText = " ";
            graphsmallcarimage.AlternateText = " ";
            graphimage.AlternateText = " ";
            graphplaneimage.AlternateText = " ";
            graphBottom.AlternateText = " ";

			// Only populate the journey images if in the correct mode to avoid extra overhead in loading control
			if (journeyEmissionsCompareMode != JourneyEmissionsCompareMode.DistanceDefault)
			{
				imageOutwardEmissionsBar.ImageUrl = GetResource(ImageEmissionBarJourneyURL);
				imageOutwardEmissionsBar.AlternateText = GetResource(ImageEmissionBarAltText);
				imageOutwardEmissionsBar.Width = JourneyEmissionsHelper.EmissionsBarWidth(emissionsOutwardJourney, maxEmissionValue);;
				imageOutwardEmissionsBar.Height = height;

				imageReturnEmissionsBar.ImageUrl = GetResource(ImageEmissionBarJourneyURL);
				imageReturnEmissionsBar.AlternateText = GetResource(ImageEmissionBarAltText);
				imageReturnEmissionsBar.Width = JourneyEmissionsHelper.EmissionsBarWidth(emissionsReturnJourney, maxEmissionValue);;
				imageReturnEmissionsBar.Height = height;

				int journeywidth = JourneyEmissionsHelper.NewEmissionsBarWidth(emissionsJourney, maxEmissionValue);;
				string helpjourney = "LEFT:" +  journeywidth +"px;  POSITION: absolute; TOP: 0px BORDER-LEFT: black thin solid";
				journeydiv.Attributes.Add("style", helpjourney);

				int journeyimagewidth = (200 - journeywidth);
				imageEmissionsJourney.Attributes.Add("width", journeyimagewidth.ToString());

				imageEmissionsJourney.ImageUrl = GetResource(ImageEmissionsWhiteURL);
				journeyEmissionsColoured.ImageUrl = GetResource(ImageEmissionsColouredURL);

                // Alt text
                imageEmissionsJourney.AlternateText = " "; // The white bar to hide the coloured bar
                journeyEmissionsColoured.AlternateText = " ";
			}

			#region Emission bar outlines
			bool showBarOutline = true;
			if (showBarOutline)
			{

				// Only populate the journey images if in the correct mode to avoid extra overhead in loading control
				if (journeyEmissionsCompareMode != JourneyEmissionsCompareMode.DistanceDefault)
				{
					imageOutwardEmissionsBarOutline.ImageUrl = GetResource(ImageEmissionBarJourneyOutlineURL);
					imageOutwardEmissionsBarOutline.Width = JourneyEmissionsHelper.EmissionsBarWidth(maxEmissionValue-emissionsOutwardJourney, maxEmissionValue);
					imageOutwardEmissionsBarOutline.Height = height;
					imageOutwardEmissionsBarOutline.Visible = (maxEmissionValue != emissionsOutwardJourney); // prevents image displaying unneccessary outline
					imageOutwardEmissionsBarEnd.ImageUrl = GetResource(ImageEmissionBarJourneyURL);
					imageOutwardEmissionsBarEnd.Width = 1;
					imageOutwardEmissionsBarEnd.Height = height;

					imageReturnEmissionsBarOutline.ImageUrl = GetResource(ImageEmissionBarJourneyOutlineURL);
					imageReturnEmissionsBarOutline.Width = JourneyEmissionsHelper.EmissionsBarWidth(maxEmissionValue-emissionsReturnJourney, maxEmissionValue);
					imageReturnEmissionsBarOutline.Height = height;
					imageReturnEmissionsBarEnd.ImageUrl = GetResource(ImageEmissionBarJourneyURL);
					imageReturnEmissionsBarEnd.Width = 1;
					imageReturnEmissionsBarEnd.Height = height;

					// Alt text all set to nothing
					imageOutwardEmissionsBarOutline.AlternateText = String.Empty;
					imageOutwardEmissionsBarEnd.AlternateText = String.Empty;
					imageReturnEmissionsBarOutline.AlternateText = String.Empty;
					imageReturnEmissionsBarEnd.AlternateText = String.Empty;

				}
			}
			else
			{

				imageOutwardEmissionsBarOutline.Visible = false;
				imageOutwardEmissionsBarEnd.Visible = false;

				imageReturnEmissionsBarOutline.Visible = false;
				imageReturnEmissionsBarEnd.Visible = false;

			}
			#endregion
		}

		/// <summary>
		/// Populates the static images and alt text
		/// </summary>
		private void PopulateImages()
		{
			// Assign images to image controls
			imageCar.ImageUrl = GetResource(ImageSmallCarURL);
			imageLargeCar.ImageUrl = GetResource(ImageLargeCarURL);
			imageTrain.ImageUrl = GetResource(ImageTrainURL);
			imagePlane.ImageUrl = GetResource(ImagePlaneURL);
			imageBus.ImageUrl = GetResource(ImageBusURL);
			imageCoach.ImageUrl = GetResource(ImageCoachURL);

			imageCoachBlackBar.ImageUrl = GetResource(ImageEmissionsBlackURL);
			imageBusBlackBar.ImageUrl = GetResource(ImageEmissionsBlackURL);
			imagePlaneBlackBar.ImageUrl = GetResource(ImageEmissionsBlackURL);
			imageSmallCarBlackBar.ImageUrl = GetResource(ImageEmissionsBlackURL);
			imageLargeCarBlackBar.ImageUrl = GetResource(ImageEmissionsBlackURL);
			imageTrainBlackBar.ImageUrl = GetResource(ImageEmissionsBlackURL);
			imageYourJourneyBlackBar.ImageUrl = GetResource(ImageEmissionsBlackURL);
			

			// Assign alternate text
			imageCar.AlternateText = " ";
			imageLargeCar.AlternateText = " ";
			imageTrain.AlternateText = " ";
			imagePlane.AlternateText = " ";
			imageBus.AlternateText = " ";
			imageCoach.AlternateText = " ";

            // Requires no alt text
            imageCoachBlackBar.AlternateText = " ";
            imageBusBlackBar.AlternateText = " ";
            imagePlaneBlackBar.AlternateText = " ";
            imageSmallCarBlackBar.AlternateText = " ";
            imageLargeCarBlackBar.AlternateText = " ";
            imageTrainBlackBar.AlternateText = " ";
            imageYourJourneyBlackBar.AlternateText = " ";
		}

		/// <summary>
		/// Populates the transport images for the journey emissions rows
		/// </summary>
		private void PopulateJourneyImages()
		{
			if (journeyEmissionsCompareMode != JourneyEmissionsCompareMode.DistanceDefault)
			{	// Only want to show the image next to the journey emissions bar if this is a single mode journey
				if (IsSingleModeType(modes))
				{
					string imageURL = string.Empty;
					string imageAltText = string.Empty;

					switch (GetSingleModeType(modes))
					{
						case ModeType.Air:
							imageURL = GetResource(ImagePlaneURL);
							imageAltText = GetResource(ImagePlaneAltText);
							break;
						case ModeType.Car:
							if (TDSessionManager.Current.JourneyEmissionsPageState.YourCarSize == "small")
							{
								imageURL = GetResource(ImageSmallCarURL);
								imageAltText = GetResource(ImageSmallCarAltText);
							}
							else if (TDSessionManager.Current.JourneyEmissionsPageState.YourCarSize == "large")
							{
								imageURL = GetResource(ImageLargeCarURL);
								imageAltText = GetResource(ImageLargeCarAltText);
							}
							else
							{
								imageURL = GetResource(ImageMediumCarURL);
								imageAltText = GetResource(ImageMediumCarAltText);
							}
							ShowPassengerListForJourney(true);
							break;
						case ModeType.Bus:
							imageURL = GetResource(ImageBusURL);
							imageAltText = GetResource(ImageBusAltText);
							break;
						case ModeType.Coach:
							imageURL = GetResource(ImageCoachURL);
							imageAltText = GetResource(ImageCoachAltText);
							break;
						case ModeType.Rail:
							imageURL = GetResource(ImageTrainURL);
							imageAltText = GetResource(ImageTrainAltText);
							break;
                        case ModeType.Cycle:
                            imageURL = GetResource(ImageCycleURL);
                            imageAltText = GetResource(ImageCycleAltText);
                            break;
						default:
							imageJourney.Visible = false;
							imageOutwardJourney.Visible = false;
							imageReturnJourney.Visible = false;
							break;
					}

					//if any of the modes in the journey are car, then set show to true
                    ArrayList modesArrayList = new ArrayList(modes);
                    if (modesArrayList.Contains(ModeType.Car))
    					ShowPassengerListForJourney(true);

					imageJourney.ImageUrl = imageURL;
					imageOutwardJourney.ImageUrl = imageURL;
					imageReturnJourney.ImageUrl = imageURL;
					imageJourney.AlternateText = " ";
					imageOutwardJourney.AlternateText = imageAltText;
					imageReturnJourney.AlternateText = imageAltText;
				}
				else
				{
					imageJourney.Visible = false;
					imageOutwardJourney.Visible = false;
					imageReturnJourney.Visible = false;
				}
			}
		}

        /// <summary>
        /// Determines is there is only a single mode for this modes array, 
        /// ignoring the modes Transfer, CheckOut, or CheckIn
        /// </summary>
        /// <param name="modes"></param>
        private bool IsSingleModeType(ModeType[] modes)
        {
            bool singleMode = false;

            if (modes.Length == 1)
            {
                singleMode = true;
            }
            else
            {
                List<ModeType> modesFound = new List<ModeType>();

                foreach (ModeType mt in modes)
                {
                    // Keep track of modes ignoring those which don't affect the overall journey mode
                    if ((mt != ModeType.CheckIn) && (mt != ModeType.CheckOut) && (mt != ModeType.Transfer))
                    {
                        if (!modesFound.Contains(mt))
                        {
                            modesFound.Add(mt);
                        }
                    }
                }

                singleMode = (modesFound.Count == 1);
            }

            return singleMode;
        }

        /// <summary>
        /// Returns the first ModeType which is not Transfer, CheckOut, or CheckIn
        /// </summary>
        /// <param name="modes"></param>
        /// <returns></returns>
        private ModeType GetSingleModeType(ModeType[] modes)
        {
            if (modes.Length == 1)
            {
                return modes[0];
            }
            else
            {
                foreach (ModeType mt in modes)
                {
                    if ((mt != ModeType.CheckIn) && (mt != ModeType.CheckOut) && (mt != ModeType.Transfer))
                    {
                        return mt;
                    }
                }
            }

            return ModeType.Walk; // Default
        }

		/// <summary>
		/// Sets the control title and journey distance text to allow Javascript to switch the units
		/// e.g. "CO2 emissions for your journey of 231 miles"
		/// </summary>
		private void SetDistanceLabel()
		{
			string miles = GetResource("JourneyEmissionsCompareControl.MilesString");
			string kms = GetResource("JourneyEmissionsCompareControl.KmsString");
			
			#region Distances 
			string distanceInMiles;
			string distanceInKms;
			decimal tempDistanceMiles;
			decimal tempDistanceKms;

			// Convert the distance (metres) into Miles and Kms
			if (JourneyDistance <= 0)
			{
				distanceInMiles = "0";
				distanceInKms = "0";
			}
			else
			{
                // Handling for journey distances which are 0
                string strDistanceMiles = MeasurementConversion.Convert(Convert.ToDouble(JourneyDistance), ConversionType.MetresToMileage);
                if (string.IsNullOrEmpty(strDistanceMiles))
                {
                    strDistanceMiles = "0";
                }

                tempDistanceMiles = Convert.ToDecimal(strDistanceMiles);
				tempDistanceKms = Convert.ToDecimal( JourneyDistance / 1000 );
				
				//If any emission values are shown to 2.d.p, we want the distance to be shown to 2.d.p
				if (EmissionsShownToTwoDecimalPlaces()) 
				{
					tempDistanceMiles = Decimal.Round(tempDistanceMiles, 2);
					tempDistanceKms = Decimal.Round(tempDistanceKms, 2);
				}
				else					
				{
					tempDistanceMiles = Decimal.Round(tempDistanceMiles, 1);
					tempDistanceKms = Decimal.Round(tempDistanceKms, 1);
				}
				

				// Use the specified distance valud to display on the control, otherwise calculate them
				if ((JourneyDistanceToDisplay != string.Empty) && (NonPrintable))
				{
					if (roadUnits == RoadUnitsEnum.Miles)
					{
						distanceInMiles = JourneyDistanceToDisplay;
						distanceInKms = tempDistanceKms.ToString();
					}
					else
					{
						distanceInMiles = tempDistanceMiles.ToString();
						distanceInKms = JourneyDistanceToDisplay;
					}
				}
				else
				{
					distanceInMiles = Convert.ToDouble(tempDistanceMiles).ToString(); // To ensure trailing zeros are removed
					distanceInKms = Convert.ToDouble(tempDistanceKms).ToString();
				}
			}
			#endregion

			#region Strings
			string journeyTitleMiles = string.Format(
					GetResource("JourneyEmissionsCompareControl.Title"),
					distanceInMiles,
					miles);

			string journeyTitleKms = string.Format(
					GetResource("JourneyEmissionsCompareControl.Title"),
					distanceInKms,
					kms);


			string journeyDistanceMiles;
			string journeyDistanceKms;
			// Different version of text to output for Printer friendly
			string compareText;

			if((journeyEmissionsVisualMode == JourneyEmissionsVisualMode.Table))
			{
				compareText = GetResource("JourneyEmissionsCompareControl.JourneyDistanceTableView");
			}
			else
			{
				compareText = GetResource("JourneyEmissionsCompareControl.JourneyDistance");
			}
			
			if (NonPrintable)
			{
				journeyDistanceMiles = string.Format(
					compareText,
					distanceInMiles,
					miles);

				journeyDistanceKms = string.Format(
					compareText,
					distanceInKms,
					kms);
			}
			else
			{
				journeyDistanceMiles = string.Format(
					compareText,
					distanceInMiles,
					miles);

				journeyDistanceKms = string.Format(
					compareText,
					distanceInKms,
					kms);
			}
			#endregion

            #region Set label text - javascript strings
            // The text displayed for distance in miles and kms, when drop down selected
            if (roadUnits == RoadUnitsEnum.Miles)
            {
                labelTitle.Text =
                    "<span class=\"milesshow\">"
                    + journeyTitleMiles
                    + "</span>"
                    + "<span class=\"kmshide\">"
                    + journeyTitleKms
                    + "</span>";

                labelJourneyDistance.Text =
                    "<span class=\"milesshow\">"
                    + journeyDistanceMiles
                    + "</span>"
                    + "<span class=\"kmshide\">"
                    + journeyDistanceKms
                    + "</span>";
            }
            else
            {
                labelTitle.Text =
                    "<span class=\"mileshide\">"
                    + journeyTitleMiles
                    + "</span>"
                    + "<span class=\"kmsshow\">"
                    + journeyTitleKms
                    + "</span>";

                labelJourneyDistance.Text =
                    "<span class=\"mileshide\">"
                    + journeyDistanceMiles
                    + "</span>"
                    + "<span class=\"kmsshow\">"
                    + journeyDistanceKms
                    + "</span>";
            }

            #endregion
			
		}

		/// <summary>
		/// Checks all the emission values to determine if any are shown to 2 d.p.
		/// </summary>
		/// <returns></returns>
		private bool EmissionsShownToTwoDecimalPlaces()
		{
			if ((emissionsSmallCarValue < (decimal)0.1) && (emissionsSmallCarValue > 0))
				return true;
			else if ((emissionsLargeCarValue < (decimal)0.1) && (emissionsLargeCarValue > 0))
				return true;
			else if ((emissionsTrainValue < (decimal)0.1) && (emissionsTrainValue > 0))
				return true;
			else if ((emissionsBusValue < (decimal)0.1) && (emissionsBusValue > 0))
				return true;
			else if ((emissionsCoachValue < (decimal)0.1) && (emissionsCoachValue > 0))
				return true;
			else if ((emissionsPlaneValue < (decimal)0.1) && (emissionsPlaneValue > 0))
				return true;
			else if ((emissionsJourney < (decimal)0.1) && (emissionsJourney > 0))
				return true;
			else
				return false;
		}

		/// <summary>
		/// Sets the Compare Transport note and its visibility
		/// </summary>
		private void SetCompareTransportNote()
		{
			// Only display if we're showing control for a planned Journey, i.e in Journey mode
			if ((JourneyEmissionsCompareMode == JourneyEmissionsCompareMode.DistanceDefault)
				|| (JourneyEmissionsCompareMode == JourneyEmissionsCompareMode.JourneyDefault))
			{
				labelCompareTransportNote.Visible = false;
			}
			else
			{
				ModeType leastEmissionsMode = GetMinEmissionsMode();

				// Emissions used to calculate difference
				decimal minPTEmissions = MinEmissionValueForPT();
				decimal yourJourneyEmissions = emissionsJourney;
				decimal carEmissions = emissionsSmallCarValue;	
				decimal largeCarEmissions = emissionsLargeCarValue;
				decimal emissionsDifference = -1;

				string compareTextResource = string.Empty;
				string differenceText = string.Empty;

				// Determine your journey mode, and set text accordingly
				if ((modes.Length == 1) && (modes[0] == ModeType.Car))
				{
					if (leastEmissionsMode == ModeType.Car)
					{
						labelCompareTransportNote.Visible = false;
					}
					else
					{
						// "Public transport would create y Kg of CO2 less per traveller 
						// for this journey than travelling by YOUR car"
						emissionsDifference = yourJourneyEmissions - minPTEmissions;
						compareTextResource = GetResource("JourneyEmissionsCompareControl.CompareJourney.PublicTransport");
						differenceText = GetResource("JourneyEmissionsCompareControl.CompareJourney.Less");

						labelCompareTransportNote.Visible = true;					
					}
				}
				else
				{
					if ((leastEmissionsMode == ModeType.Car) && (yourJourneyEmissions > largeCarEmissions))
					{
						// "Your journey would create y Kg of CO2 more per traveller 
						// than travelling by LARGE car"
						emissionsDifference = yourJourneyEmissions - largeCarEmissions;
						compareTextResource = GetResource("JourneyEmissionsCompareControl.CompareJourney.YourJourney");
						differenceText = GetResource("JourneyEmissionsCompareControl.CompareJourney.More");

						labelCompareTransportNote.Visible = true;
					}
					else
					{
						// "Your journey would create y Kg of CO2 less per traveller 
						// than travelling by LARGE car"
						emissionsDifference =  largeCarEmissions - yourJourneyEmissions;
						compareTextResource = GetResource("JourneyEmissionsCompareControl.CompareJourney.YourJourney");
						differenceText = GetResource("JourneyEmissionsCompareControl.CompareJourney.Less");

						labelCompareTransportNote.Visible = true;
					}
				}

				if (emissionsDifference < 0)
				{
					emissionsDifference = Decimal.Negate(emissionsDifference);
					differenceText = GetResource("JourneyEmissionsCompareControl.CompareJourney.More");
				}

				if (emissionsDifference < (decimal)0.1)
					emissionsDifference = Decimal.Round(emissionsDifference, 2);
				else
					emissionsDifference = Decimal.Round(emissionsDifference, 1);

				labelCompareTransportNote.Text = string.Format(compareTextResource, emissionsDifference, differenceText);
			}
		}

		#endregion

		#region Emissions
		/// <summary>
		/// Calculates the emissions for each transport mode
		/// </summary>
		private void CalculateEmissions()
		{
			decimal distance = journeyDistance;

			int passengers;
			int largePassengers;

            TDJourneyParametersMulti journeyParams = TDSessionManager.Current.JourneyParameters as TDJourneyParametersMulti;
            JourneyEmissionsPageState pageState = TDSessionManager.Current.JourneyEmissionsPageState;

			if(journeyEmissionsVisualMode == JourneyEmissionsVisualMode.Diagram)
			{
				passengers = Convert.ToInt32(listCarPassengers.SelectedValue);
				largePassengers = Convert.ToInt32(listCarLargePassengers.SelectedValue);
			}
			else
			{
				passengers = Convert.ToInt32(listTableSmallCarPassengers.SelectedValue);
				largePassengers = Convert.ToInt32(listTableLargeCarPassengers.SelectedValue);
			}

            if (TDSessionManager.Current.FindAMode != FindAMode.International )
            {
                // Must calculate all the mode emissions to allow us to determine which is the 
                // largest and therefore calculate the Emission Bar image width's (PopulateEmissionBarImages)
                emissionsTrainValue = emissionsHelper.GetEmissions(ModeType.Rail, distance);
                emissionsBusValue = emissionsHelper.GetEmissions(ModeType.Bus, distance);
                emissionsCoachValue = emissionsHelper.GetEmissions(ModeType.Coach, distance);
                emissionsPlaneValue = emissionsHelper.GetEmissions(ModeType.Air, distance);
            }
            else
            {
                emissionsTrainValue = emissionsHelper.GetEmissions(ModeType.Rail, distance,true);
                emissionsBusValue = emissionsHelper.GetEmissions(ModeType.Bus, distance, true);
                emissionsCoachValue = emissionsHelper.GetEmissions(ModeType.Coach, distance, true);
                emissionsPlaneValue = emissionsHelper.GetEmissions(ModeType.Air, distance, true);
            }

			// emissions for car is divided by the number of passengers selected.
			// the Original is used later when determing max emissions value (MaxEmissionValue)
			emissionsCarValueOriginal = emissionsHelper.GetEmissions(ModeType.Car, distance, "small", "diesel");
			emissionsCarValue =  RoundEmissionsWithPassengers(emissionsCarValueOriginal, passengers);

			emissionsLargeCarValueOriginal = emissionsHelper.GetEmissions(ModeType.Car, distance, "large", "petrol");
			emissionsLargeCarValue =  RoundEmissionsWithPassengers(emissionsLargeCarValueOriginal, largePassengers);
			
			emissionsSmallCarValueOriginal = emissionsHelper.GetEmissions(ModeType.Car, distance, "small", "diesel");
			emissionsSmallCarValue =  RoundEmissionsWithPassengers(emissionsSmallCarValueOriginal, passengers);



			#region Emissions for journey

			bool reCalculate = false;

			// Only calculate emissions for journey if we're in Journey mode
			if ((journeyEmissionsCompareMode == JourneyEmissionsCompareMode.JourneyDefault) ||
				(journeyEmissionsCompareMode == JourneyEmissionsCompareMode.JourneyCompare))
			{
				if (UseItineraryManager)
				{	
					TDItineraryManager itineraryManager = TDItineraryManager.Current;
                    
                    // Journeys exist, get each Outward (and Return) leg and calculate emissions
					if (itineraryManager.OutwardLength > 0)
					{
						foreach (Journey journey in itineraryManager.OutwardJourneyItinerary)
						{
							emissionsOutwardJourney += emissionsHelper.GetEmissions(journey, journeyParams, pageState);
						}
					}

					if (itineraryManager.ReturnLength > 0)
					{
						foreach (Journey journey in itineraryManager.ReturnJourneyItinerary)
						{
							emissionsReturnJourney += emissionsHelper.GetEmissions(journey, journeyParams, pageState);
						}

						emissionsJourney = emissionsOutwardJourney + emissionsReturnJourney;
					}
					else
					{	// no return journey so Journey emissions are Outward emissions
						emissionsJourney = emissionsOutwardJourney;

						// set to -1 so the return row is not displayed
						emissionsReturnJourney = -1;
					}

					// If the itinerary is ALL car journeys, then we want the same emissions value
					// to be shown as for the Journey.
					// Outward and Return journey
					if ((itineraryManager.OutwardLength > 0) && (itineraryManager.ReturnLength > 0))
					{	// Both Outward and Return are car journeys
						if ((!itineraryManager.OutwardIsPublic) && (!itineraryManager.ReturnIsPublic))
						{
							emissionsCarValueOriginal = emissionsJourney;
							emissionsCarValue = RoundEmissionsWithPassengers(emissionsCarValueOriginal, passengers);
						//	GetEmissionsForReturnCarJourneys(passengers, largePassengers);
						}
					}	
					else if ((itineraryManager.OutwardLength > 0) && (itineraryManager.ReturnLength <= 0))
					{	// Outward only, and is a car journey
						if (!itineraryManager.OutwardIsPublic)
						{
							emissionsCarValueOriginal = emissionsJourney;
							emissionsCarValue = RoundEmissionsWithPassengers(emissionsCarValueOriginal, passengers);
							//GetEmissionsForCarJourneys(passengers, largePassengers);
						}
					}
					else if ((itineraryManager.OutwardLength <= 0) && (itineraryManager.ReturnLength > 0))
					{	//Return only, and is a car journey
						if (!itineraryManager.ReturnIsPublic)
						{
							emissionsCarValueOriginal = emissionsJourney;
							emissionsCarValue = RoundEmissionsWithPassengers(emissionsCarValueOriginal, passengers);
							//GetEmissionsForCarJourneys(passengers, largePassengers);
						}
					}

				}
				else if (UseSessionManager)
				{
					ITDSessionManager sessionManager = TDSessionManager.Current;

					// get the selected journeys
                    Journey outwardJourney = emissionsHelper.GetRequiredJourney(sessionManager.JourneyResult, sessionManager.CycleResult, sessionManager.JourneyViewState, false);
					emissionsOutwardJourney = emissionsHelper.GetEmissions(outwardJourney, journeyParams, pageState);					

					// If the user has specified a return journey then get selected journey
                    if (((sessionManager.JourneyResult != null) && (sessionManager.JourneyResult.ReturnPublicJourneyCount > 0 || sessionManager.JourneyResult.ReturnRoadJourneyCount > 0))
                        ||
                         ((sessionManager.CycleResult != null) && (sessionManager.CycleResult.ReturnCycleJourneyCount > 0))
                       )
					{
                        Journey returnJourney = emissionsHelper.GetRequiredJourney(sessionManager.JourneyResult, sessionManager.CycleResult, sessionManager.JourneyViewState, true);
						emissionsReturnJourney = emissionsHelper.GetEmissions(returnJourney, journeyParams, pageState);
					
						//if the return journey or the outward was car, then we will need to recaluculate based on the number of passengers.
						if(outwardJourney.Type == TDJourneyType.RoadCongested && ((returnJourney.Type == TDJourneyType.PublicOriginal) ||(returnJourney.Type ==  TDJourneyType.PublicAmended )))
						{
							emissionsOutwardJourney = RoundEmissionsWithPassengers(emissionsOutwardJourney, Convert.ToInt32(listOutwardPassengers.SelectedValue));
							ShowPassengerListForJourney(true);
							reCalculate = true;
							labelJourneyPassengers.Text = "occupant(s) for your outward car journey";
						}

						if(returnJourney.Type == TDJourneyType.RoadCongested && ((outwardJourney.Type == TDJourneyType.PublicOriginal) ||(outwardJourney.Type ==  TDJourneyType.PublicAmended )))
						{
							emissionsReturnJourney = RoundEmissionsWithPassengers(emissionsReturnJourney, Convert.ToInt32(listOutwardPassengers.SelectedValue));
						    ShowPassengerListForJourney(true);
							reCalculate = true;
							labelJourneyPassengers.Text = "occupant(s) for your return car journey";
						}


						emissionsJourney = emissionsOutwardJourney + emissionsReturnJourney;
						
						// Update the Journey label to indicate it is total emissions for Outward and Return
						labelJourneyTitle.Text = GetResource("JourneyEmissionsCompareControl.JourneyTitle") + " " 
							+"<br />" + GetResource("JourneyEmissionsCompareControl.JourneyTitle.OutwardAndReturn");
                        
						// If the user has planned and selected an Outward AND Return Car journey, 
						// we want to have the same emissions value for Car mode
						if ((returnJourney.Type == TDJourneyType.RoadCongested) && (outwardJourney.Type == TDJourneyType.RoadCongested))
						{
							emissionsCarValueOriginal = emissionsOutwardJourney + emissionsReturnJourney;
							emissionsCarValue = RoundEmissionsWithPassengers(emissionsCarValueOriginal, passengers);
							//Here the user has planned an outward and a return journey, so we need to add
							//both together for the other car types.
							
							GetEmissionsForReturnCarJourneys(outwardJourney, returnJourney, passengers, largePassengers);
						}
					}
					else
					{
						emissionsJourney = emissionsOutwardJourney;

						// If the user has planned and selected a Car journey, 
						// we want to have the same emissions value for Car mode
						if (outwardJourney.Type == TDJourneyType.RoadCongested)
						{ 
							emissionsCarValueOriginal = emissionsOutwardJourney;
							emissionsCarValue = RoundEmissionsWithPassengers(emissionsCarValueOriginal, passengers);
							GetEmissionsForSingleCarJourneys(outwardJourney, passengers, largePassengers);
						}

						// set to -1 so the return row is not displayed
						emissionsReturnJourney = -1;
					}
				}		

				// for a very short journeys, the emissions can potentially be < 0.01, therefore 
				// round the value up so 0kg is not shown to the user for their journey 
				if (!reCalculate)
				{
					emissionsOutwardJourney = RoundEmissionsWithPassengers(emissionsOutwardJourney, Convert.ToInt32(listOutwardPassengers.SelectedValue));
					emissionsReturnJourney = RoundEmissionsWithPassengers(emissionsReturnJourney, Convert.ToInt32(listOutwardPassengers.SelectedValue));
					emissionsJourney = RoundEmissionsWithPassengers(emissionsJourney, Convert.ToInt32(listJourneyPassengers.SelectedValue));
				}
			}
			else
			{   //Setting to -1 ensures none of these rows will be displayed - See SetControlVisibility()
				emissionsOutwardJourney = -1;
				emissionsReturnJourney = -1;
				emissionsJourney = -1;
			}

			#endregion
		}


		/// <summary>
		/// Sets the Small and Large Car Emission Values based on the outward road journey that the 
		/// user has planned, and the number of passengers selected
		/// </summary>
		/// <param name="journey"></param>
		/// <param name="passengers"></param>
		/// <param name="largePassengers"></param>
		/// <returns></returns>
		private void GetEmissionsForSingleCarJourneys(Journey journey, int passengers, int largePassengers)
		{
			RoadJourney roadJourney = journey as RoadJourney;
			
			emissionsSmallCarValueOriginal = emissionsHelper.GetEmissions(TDSessionManager.Current.JourneyParameters as TDJourneyParametersMulti,"small", "diesel", roadJourney); 
			emissionsSmallCarValue = RoundEmissionsWithPassengers(emissionsSmallCarValueOriginal, passengers);

			emissionsLargeCarValueOriginal = emissionsHelper.GetEmissions(TDSessionManager.Current.JourneyParameters as TDJourneyParametersMulti,"large", "petrol", roadJourney); 
			emissionsLargeCarValue = RoundEmissionsWithPassengers(emissionsLargeCarValueOriginal, largePassengers);
		}


		/// <summary>
		/// Sets the Small and Large Car Emission Values based on the outward road journey that the 
		/// user has planned, and the Return road journey, and the number of passengers selected
		/// </summary>
		/// <param name="outwardJourney"></param>
		/// <param name="returnJourney"></param>
		/// <param name="passengers"></param>
		/// <param name="largePassengers"></param>
		/// <returns></returns>
		private void GetEmissionsForReturnCarJourneys(Journey outwardJourney, Journey returnJourney, int passengers, int largePassengers)
		{
			RoadJourney roadOutwardJourney = outwardJourney as RoadJourney;
			RoadJourney roadReturnJourney = returnJourney as RoadJourney;
			
			//If it is a return journey then we need to add together the emissions from both the outward
			//and the return journeys.
				
			decimal emissionsSmallCarValueOutward;
			decimal emissionsSmallCarValueReturn;

			emissionsSmallCarValueOutward = emissionsHelper.GetEmissions(TDSessionManager.Current.JourneyParameters as TDJourneyParametersMulti,"small", "diesel", roadOutwardJourney);
			emissionsSmallCarValueReturn = emissionsHelper.GetEmissions(TDSessionManager.Current.JourneyParameters as TDJourneyParametersMulti,"small", "diesel", roadReturnJourney);
				
			emissionsSmallCarValueOriginal = emissionsSmallCarValueOutward + emissionsSmallCarValueReturn;
 
			emissionsSmallCarValue = RoundEmissionsWithPassengers(emissionsSmallCarValueOriginal, passengers);

			decimal emissionsLargeCarValueOutward;
			decimal emissionsLargeCarValueReturn;

            emissionsLargeCarValueOutward = emissionsHelper.GetEmissions(TDSessionManager.Current.JourneyParameters as TDJourneyParametersMulti, "large", "petrol", roadOutwardJourney);
            emissionsLargeCarValueReturn = emissionsHelper.GetEmissions(TDSessionManager.Current.JourneyParameters as TDJourneyParametersMulti, "large", "petrol", roadReturnJourney);

			emissionsLargeCarValueOriginal = emissionsLargeCarValueOutward + emissionsLargeCarValueReturn;
			emissionsLargeCarValue = RoundEmissionsWithPassengers(emissionsLargeCarValueOriginal, largePassengers);
		}

		/// <summary>
		/// Divides the emissions value with the passengers and applies rounding to ensure 
		/// the value is to 1.d.p or 2.d.p depending on how small it is.
		/// Also ensures a value of 0 is not rounded up to 0.1
		/// </summary>
		/// <param name="emissionsOriginal"></param>
		/// <param name="passengers"></param>
		/// <returns></returns>
		private decimal RoundEmissionsWithPassengers(decimal emissionsOriginal, int passengers)
		{
			decimal emissionsRounded = 0;

			emissionsRounded = emissionsOriginal / passengers;

			// Round to 1.d.p
			if (emissionsRounded > Convert.ToDecimal(0.05))
				emissionsRounded  = Decimal.Round(emissionsRounded, 1);
			else 
			{	// Prevent the posibility of displaying 0 emissions, when it is actualy 0.04 or less
				// Also prevents rounding up to 0.01 if the actual value is 0.
				if ((emissionsRounded > Convert.ToDecimal(0.005)) || (emissionsRounded <= 0))
					emissionsRounded  = Decimal.Round(emissionsRounded, 2);
				else
					// where the value is less than 0.005, round it up to 0.01
					emissionsRounded  = Convert.ToDecimal(0.01);
			}

			return emissionsRounded;
		}

		/// <summary>
		/// Returns the maximum emission value from comparing all the transport modes
		/// </summary>
		/// <returns></returns>
		private decimal MaxEmissionValue()
		{
			decimal maxValue = 0;

			if (maxValue < emissionsSmallCarValue)
				maxValue = emissionsSmallCarValueOriginal;
			
			if (maxValue < emissionsLargeCarValueOriginal)
				maxValue = emissionsLargeCarValueOriginal;

			if (maxValue < emissionsTrainValue)
				maxValue = emissionsTrainValue;

			if (maxValue < emissionsBusValue)
				maxValue = emissionsBusValue;

			if (maxValue < emissionsCoachValue)
				maxValue = emissionsCoachValue;

			if (maxValue < emissionsPlaneValue)
				maxValue = emissionsPlaneValue;

			if (maxValue < emissionsJourney)
				maxValue = emissionsJourney;

			return maxValue;
		}

		/// <summary>
		/// Returns the minimum emission value for PT modes from comparing all the PT transport modes
		/// </summary>
		/// <returns></returns>
		private decimal MinEmissionValueForPT()
		{
			decimal minValue = emissionsTrainValue;

			if (emissionsBusValue < minValue)
				minValue = emissionsBusValue;

			if (emissionsCoachValue < minValue)
				minValue = emissionsCoachValue;

			if (emissionsPlaneValue < minValue)
				minValue = emissionsPlaneValue;

			return minValue;
		}

		/// <summary>
		/// Returns the minimum emissions mode
		/// </summary>
		/// <returns></returns>
		private ModeType GetMinEmissionsMode()
		{
			decimal minValue = emissionsSmallCarValue;
			ModeType mt = ModeType.Car;

			if (emissionsTrainValue < minValue)
			{
				minValue = emissionsTrainValue;
				mt = ModeType.Rail;
			}


			if (emissionsBusValue < minValue)
			{
				minValue = emissionsBusValue;
				mt = ModeType.Bus;
			}

			if (emissionsCoachValue < minValue)
			{
				minValue = emissionsCoachValue;
				mt = ModeType.Coach;
			}

			if (emissionsPlaneValue < minValue)
			{
				minValue = emissionsPlaneValue;
				mt = ModeType.Air;
			}

			return mt;
		}
		#endregion

		#region Control visibility
		/// <summary>
		/// Sets the visibility of controls dependent on the mode this control is in
		/// </summary>
		private void SetControlVisibility()
		{
            //showJourney button only applies for International journeys
            buttonShowJourney.Visible = (TDSessionManager.Current.FindAMode == FindAMode.International);

			switch (journeyEmissionsCompareMode)
			{
				case JourneyEmissionsCompareMode.DistanceDefault:
				{ 
					// If in Distance mode, then hide all Journey related rows
					HideJourneyRows(); 
					if(!showEmissionsTable)
					DisplayCompareRows();

                    // Show the distance unit dropdown in the title row
                    labelDistanceUnits1.Visible = showHeaderDistanceUnitsDropDown;
                    dropdownlistUnits1.Visible = showHeaderDistanceUnitsDropDown;
                    // Show the distance unit dropdown in the footer row
                    labelDistanceUnits2.Visible = !showHeaderDistanceUnitsDropDown;
                    dropdownlistUnits2.Visible = !showHeaderDistanceUnitsDropDown;

					// Only show the Air transport mode emissions if its above minimum air distance
					rowPlaneEmissions.Visible = JourneyEmissionsHelper.ShowAir(JourneyDistance);

					//Display Coach if over minimum coach distance, if not display Bus
					rowCoachEmissions.Visible = JourneyEmissionsHelper.ShowCoach(JourneyDistance);
					rowBusEmissions.Visible = !rowCoachEmissions.Visible;

				}
					break;
				case JourneyEmissionsCompareMode.JourneyDefault:
				{
					// Only display the journey rows if the emission values have been set
					rowOutwardJourney.Visible = (emissionsOutwardJourney >= 0);
					rowReturnJourney.Visible = (emissionsReturnJourney >= 0);			
					rowCompareJourney.Visible = true;

					// Hide the total journey emissions row
					rowJourney.Visible = false;

					// Hide the Compare rows
					HideCompareRows();

					// Hide the distance unit dropdown in the title row
					labelDistanceUnits1.Visible = false;
					dropdownlistUnits1.Visible = false;
                    labelDistanceUnits2.Visible = false;
                    dropdownlistUnits2.Visible = false;
				}
					break;
				case JourneyEmissionsCompareMode.JourneyCompare:
				{
					// Hide the Outward and Return journey rows as we now want to use the
					// total journey emissions row to show the total outward and return
					// journey emissions
					rowCompareJourney.Visible = false;
					rowOutwardJourney.Visible = false;
					rowReturnJourney.Visible = false;
					
					//Only if not in diagram mode!
					if(!yourJourneyTableRow.Visible)
					rowJourney.Visible = true;

					// Display the Compare rows
					DisplayCompareRows();
	
					// Show the distance unit dropdown in the title row
					labelDistanceUnits1.Visible = showHeaderDistanceUnitsDropDown;
					dropdownlistUnits1.Visible = showHeaderDistanceUnitsDropDown;
                    // Show the distance unit dropdown in the footer row
                    labelDistanceUnits2.Visible = !showHeaderDistanceUnitsDropDown;
                    dropdownlistUnits2.Visible = !showHeaderDistanceUnitsDropDown;

					// Only show the Air transport mode emissions if its above minimum air distance
					rowPlaneEmissions.Visible = JourneyEmissionsHelper.ShowAir(JourneyDistance);

					//Display Coach if over minimum coach distance, if not display Bus
					rowCoachEmissions.Visible = JourneyEmissionsHelper.ShowCoach(JourneyDistance);
					rowBusEmissions.Visible = !JourneyEmissionsHelper.ShowCoach(JourneyDistance);

					if(TDSessionManager.Current.JourneyEmissionsPageState.YourCarSize == "large")
					{
						rowCarLargeEmissions.Visible = false;
					}


				}
					break;
				default:
				{
					// Hide everything
					tableJourneyEmissionsCompare.Visible = false;
				}
					break;
			}

			// Set the visibility of the emission rows based on the Property values.
			// We do an AND with itself because the visibility is also set in above switch
			// dependent on the control mode
			rowCarEmissions.Visible = ShowCarEmissions && rowCarEmissions.Visible;
			rowCarLargeEmissions.Visible = showCarLargeEmissions && rowCarLargeEmissions.Visible;
			rowTrainEmissions.Visible = ShowTrainEmissions && rowTrainEmissions.Visible;
			rowBusEmissions.Visible = ShowBusEmissions && rowBusEmissions.Visible;
			rowCoachEmissions.Visible = ShowCoachEmissions && rowCoachEmissions.Visible;
			rowPlaneEmissions.Visible = ShowPlaneEmissions && rowPlaneEmissions.Visible;

			smallCarRow.Visible = ShowCarEmissions;
			largeCarRow.Visible = showCarLargeEmissions;
			busRow.Visible = ShowBusEmissions  && !JourneyEmissionsHelper.ShowCoach(JourneyDistance);
			coachRow.Visible = ShowCoachEmissions && JourneyEmissionsHelper.ShowCoach(JourneyDistance);
			trainRow.Visible = ShowTrainEmissions;
			planeRow.Visible = ShowPlaneEmissions && JourneyEmissionsHelper.ShowAir(JourneyDistance);

            
			// set the visibility according to page landing  choices
			// if (pageLandingMode)
            if (PageLandingActive)
            {


                // if all landing modes set then just set the all mode
                if (TDSessionManager.Current.JourneyEmissionsPageState.LandingModeSmallCar == true
                    && TDSessionManager.Current.JourneyEmissionsPageState.LandingModeLargeCar == true
                    && TDSessionManager.Current.JourneyEmissionsPageState.LandingModeCoach == true
                    && TDSessionManager.Current.JourneyEmissionsPageState.LandingModeTrain == true
                    && TDSessionManager.Current.JourneyEmissionsPageState.LandingModePlane == true)
                    TDSessionManager.Current.JourneyEmissionsPageState.LandingModeAll = true;

                // if no landing modes set then default to the all mode
                if (!(TDSessionManager.Current.JourneyEmissionsPageState.LandingModeSmallCar
                    || TDSessionManager.Current.JourneyEmissionsPageState.LandingModeLargeCar
                    || TDSessionManager.Current.JourneyEmissionsPageState.LandingModeCoach
                    || TDSessionManager.Current.JourneyEmissionsPageState.LandingModeTrain
                    || TDSessionManager.Current.JourneyEmissionsPageState.LandingModePlane))
                    TDSessionManager.Current.JourneyEmissionsPageState.LandingModeAll = true;

                if (TDSessionManager.Current.JourneyEmissionsPageState.LandingModeAll)
                {
                    // don't do anything all modes visibility should already be set.

                }
                else
                {
                    // Only display the specific mode selected


                    smallCarRow.Visible = false;
                    largeCarRow.Visible = false;
                    busRow.Visible = false;
                    coachRow.Visible = false;
                    trainRow.Visible = false;
                    planeRow.Visible = false;




                    if (TDSessionManager.Current.JourneyEmissionsPageState.LandingModeSmallCar)
                    {
                        rowCarEmissions.Visible = ShowCarEmissions && rowCarEmissions.Visible;
                        smallCarRow.Visible = ShowCarEmissions;
                    }
                    else
                    {
                        rowCarEmissions.Visible = false;
                        smallCarRow.Visible = false;
                    }

                    if (TDSessionManager.Current.JourneyEmissionsPageState.LandingModeLargeCar)
                    {
                        rowCarLargeEmissions.Visible = showCarLargeEmissions && rowCarLargeEmissions.Visible;
                        largeCarRow.Visible = showCarLargeEmissions;
                    }
                    else
                    {
                        rowCarLargeEmissions.Visible = false;
                        largeCarRow.Visible = false;
                    }

                    if (TDSessionManager.Current.JourneyEmissionsPageState.LandingModeTrain)
                    {
                        rowTrainEmissions.Visible = ShowTrainEmissions && rowTrainEmissions.Visible;
                        trainRow.Visible = ShowTrainEmissions;
                    }
                    else
                    {
                        rowTrainEmissions.Visible = false;
                        trainRow.Visible = false;
                    }
                    if (TDSessionManager.Current.JourneyEmissionsPageState.LandingModeCoach)
                    {
                        rowBusEmissions.Visible = ShowBusEmissions && rowBusEmissions.Visible;
                        rowCoachEmissions.Visible = ShowCoachEmissions && rowCoachEmissions.Visible;
                        busRow.Visible = ShowBusEmissions && !JourneyEmissionsHelper.ShowCoach(JourneyDistance);
                        coachRow.Visible = ShowCoachEmissions && JourneyEmissionsHelper.ShowCoach(JourneyDistance);
                    }
                    else
                    {
                        rowBusEmissions.Visible = false;
                        rowCoachEmissions.Visible = false;
                        busRow.Visible = false;
                        coachRow.Visible = false;
                    }
                    if (TDSessionManager.Current.JourneyEmissionsPageState.LandingModePlane)
                    {
                        rowPlaneEmissions.Visible = ShowPlaneEmissions && rowPlaneEmissions.Visible;
                        planeRow.Visible = ShowPlaneEmissions && JourneyEmissionsHelper.ShowAir(JourneyDistance);
                    }
                    else
                    {
                        rowPlaneEmissions.Visible = false;
                        planeRow.Visible = false;
                    }
                }
            }
		}

		/// <summary>
		/// Hides all the Journey rows in the table
		/// </summary>
		private void HideJourneyRows()
		{
			rowJourney.Visible = false;
			rowOutwardJourney.Visible = false;
			rowReturnJourney.Visible = false;
			rowCompareJourney.Visible = false;
			yourJourneyTableRow.Visible = false;
		}

		/// <summary>
		/// Hides all the Compare emission rows in the table
		/// </summary>
		private void HideCompareRows()
		{
			rowJourneyDistance.Visible = false;
			rowCarEmissions.Visible = false;
			rowTrainEmissions.Visible = false;
			rowBusEmissions.Visible = false;
			rowCoachEmissions.Visible = false;
			rowPlaneEmissions.Visible = false;
		}

		/// <summary>
		/// Shows all the Compare emission rows in the table
		/// </summary>
		private void DisplayCompareRows()
		{
			rowJourneyDistance.Visible = true;
			rowCarEmissions.Visible = true;
			rowCarLargeEmissions.Visible = true;
			rowTrainEmissions.Visible = true;
			rowBusEmissions.Visible = true;
			rowCoachEmissions.Visible = true;
			rowPlaneEmissions.Visible = true;
		}

		/// <summary>
		/// Sets the printable status of controls
		/// </summary>
		private void SetPrintableControls()
		{
			if(!nonprintable)
			{
                buttonChangeView.Visible = false;
                buttonShowJourney.Visible = false;
				// hide drop down list in the title row
				labelDistanceUnits1.Visible = false;
				dropdownlistUnits1.Visible = false;
                // hide drop down list in the footer row
                labelDistanceUnits2.Visible = false;
                dropdownlistUnits2.Visible = false;

				// Build the journey passengers selected string
				StringBuilder sb3 = new StringBuilder();
				sb3.Append(GetResource("JourneyEmissionsCompareControl.With"));
				sb3.Append(" ");
				sb3.Append(listJourneyPassengers.SelectedValue.ToString());
				sb3.Append(" ");
				sb3.Append(labelJourneyPassengers.Text);
					
				labelJourneyWith.Text = sb3.ToString();

				// Build the small car passengers selected string
				StringBuilder sb = new StringBuilder();
				sb.Append(GetResource("JourneyEmissionsCompareControl.With"));
				sb.Append(" ");
				sb.Append(listCarPassengers.SelectedValue.ToString());
				sb.Append(" ");
				sb.Append(GetResource("JourneyEmissionsCompareControl.Passenger"));
              
				// Need to display appropriate text if its just 1 car occupant, rather than the plural
				if ((listCarPassengers.SelectedIndex >= 1) )
					sb.Append(GetResource("JourneyEmissionsCompareControl.Passenger.End"));
					
				labelWith.Text = sb.ToString();

				//Build the large car passengers selected string
				StringBuilder sb2 = new StringBuilder();
				sb2.Append(GetResource("JourneyEmissionsCompareControl.With"));
				sb2.Append(" ");
				sb2.Append(listCarLargePassengers.SelectedValue.ToString());
				sb2.Append(" ");
				sb2.Append(GetResource("JourneyEmissionsCompareControl.Passenger"));
              
				// Need to display appropriate text if its just 1 car occupant, rather than the plural
				if ((listCarLargePassengers.SelectedIndex >= 1) )
					sb2.Append(GetResource("JourneyEmissionsCompareControl.Passenger.End"));
					
				labelLargeWith.Text = sb2.ToString();

				//Hide the small car list and label
				listCarPassengers.Visible = false;
				labelPassengers.Visible = false;
				
				//Hide the large car list and label
				labelLargePassengers.Visible = false;
				listCarLargePassengers.Visible = false;

				//Hide the passengers label
				listJourneyPassengers.Visible = false;
				labelJourneyPassengers.Visible = false;

				//For the table view
				if(emissionsGraphicalPanel.Visible == false)
				{
					StringBuilder sb5 = new StringBuilder();
					sb5.Append(GetResource("JourneyEmissionsCompareControl.With"));
					sb5.Append(" ");
					sb5.Append(listTableLargeCarPassengers.SelectedValue.ToString());
					sb5.Append(" ");
					sb5.Append(GetResource("JourneyEmissionsCompareControl.Passenger"));
              
					// Need to display appropriate text if its just 1 car occupant, rather than the plural
					if ((listTableLargeCarPassengers.SelectedIndex >= 1) )
						sb5.Append(GetResource("JourneyEmissionsCompareControl.Passenger.End"));
					labelTableLargeCarWith.Text = sb5.ToString();
					
					
					listTableLargeCarPassengers.Visible = false;
					labelTableLargeCarPassengers.Visible = false;

					StringBuilder sb4 = new StringBuilder();
					sb4.Append(GetResource("JourneyEmissionsCompareControl.With"));
					sb4.Append(" ");
					sb4.Append(listTableSmallCarPassengers.SelectedValue.ToString());
					sb4.Append(" ");
					sb4.Append(GetResource("JourneyEmissionsCompareControl.Passenger"));
              
					// Need to display appropriate text if its just 1 car occupant, rather than the plural
					if ((listTableSmallCarPassengers.SelectedIndex >= 1) )
						sb4.Append(GetResource("JourneyEmissionsCompareControl.Passenger.End"));
					labelTableSmallCarWith.Text = sb4.ToString();
 
					listTableSmallCarPassengers.Visible = false;
					labelTableSmallCarPassengers.Visible = false;

					//For the table view of 'Your journey'
					StringBuilder sb6 = new StringBuilder();
					sb6.Append(GetResource("JourneyEmissionsCompareControl.With"));
					sb6.Append(" ");
					sb6.Append(listYourJourneyPassengers.SelectedValue.ToString());
					sb6.Append(" ");
					sb6.Append(GetResource("JourneyEmissionsCompareControl.Passenger"));
              
					// Need to display appropriate text if its just 1 car occupant, rather than the plural
					if ((listYourJourneyPassengers.SelectedIndex >= 1) )
						sb6.Append(GetResource("JourneyEmissionsCompareControl.Passenger.End"));
					labelYourJourneyWith.Text = sb6.ToString();

					listYourJourneyPassengers.Visible = false;
					labelYourJourneyPassengers.Visible = false;


				}

			}
		}

		/// <summary>
		/// Determines which Emission rows should be displayed dependent on the transport modes 
		/// in the Journey planned.
		/// </summary>
		private void SetEmissionRowVisibility()
		{
			// Only set visibility if this control is in the JourneyCompare mode
			if (journeyEmissionsCompareMode != JourneyEmissionsCompareMode.DistanceDefault)
			{
				// If the journey uses only a single mode (Car, Rail, Bus/Coach, Plane, 
				// then we dont want to display that emissions row in the Compare emissions 
				// for journeypanel
				if (UseItineraryManager)
				{
					TDItineraryManager itineraryManager = TDItineraryManager.Current;

					// Used to hold modes for outward and return journey
					ArrayList al = new ArrayList();

					#region Outward journey
					// Journeys exist, get each Outward (and Return) leg 
					if (itineraryManager.OutwardLength > 0)
					{
						ModeType[] mt = new ModeType[0];
						foreach (Journey journey in itineraryManager.OutwardJourneyItinerary)
						{
							mt = JourneyEmissionsHelper.GetJourneyModes(journey);

							foreach (ModeType m in mt)
							{
								if (!al.Contains(m))
									al.Add(m);
							}
						}
					}
					#endregion

					#region Return journey
					if (itineraryManager.ReturnLength > 0)
					{
						ModeType[] mt = new ModeType[0];
						foreach (Journey journey in itineraryManager.ReturnJourneyItinerary)
						{
							mt = JourneyEmissionsHelper.GetJourneyModes(journey);
							
							foreach (ModeType m in mt)
							{
								if (!al.Contains(m))
									al.Add(m);
							}
						}
					}
					#endregion

					modes = (ModeType[])al.ToArray(typeof(TransportDirect.JourneyPlanning.CJPInterface.ModeType));
					
					HideEmissionRow(modes);
				}
				else if (UseSessionManager)
				{
					ITDSessionManager sessionManager = TDSessionManager.Current;

					// Used to hold modes for outward and return journey
					ArrayList al = new ArrayList();

					#region Outward journey
					// get the selected journey
                    Journey journey = emissionsHelper.GetRequiredJourney(sessionManager.JourneyResult, sessionManager.CycleResult, sessionManager.JourneyViewState, false);

					modes = JourneyEmissionsHelper.GetJourneyModes(journey);

					foreach (ModeType m in modes)
					{
						if (!al.Contains(m))
							al.Add(m);
					}
					#endregion

					#region Return journey
					// If the user has specified a return journey then get selected journey
                    if (((sessionManager.JourneyResult != null) && (sessionManager.JourneyResult.ReturnPublicJourneyCount > 0 || sessionManager.JourneyResult.ReturnRoadJourneyCount > 0))
                        ||
                         ((sessionManager.CycleResult != null) && (sessionManager.CycleResult.ReturnCycleJourneyCount > 0))
                        )
					{
                        journey = emissionsHelper.GetRequiredJourney(sessionManager.JourneyResult, sessionManager.CycleResult, sessionManager.JourneyViewState, true);

						modes = JourneyEmissionsHelper.GetJourneyModes(journey);

						foreach (ModeType m in modes)
						{
							if (!al.Contains(m))
								al.Add(m);
						}
					}
					#endregion

					modes = (ModeType[])al.ToArray(typeof(TransportDirect.JourneyPlanning.CJPInterface.ModeType));
					
					HideEmissionRow(modes);
				}
			}
		}

		/// <summary>
		/// Sets the Emission row display property to false if a single mode is passed
		/// </summary>
		/// <param name="modes">Array of ModeTypes</param>
		private void HideEmissionRow(ModeType[] modes)
		{
			if (IsSingleModeType(modes))
			{
				switch (GetSingleModeType(modes))
				{
					case ModeType.Air:
						ShowPlaneEmissions = false;
						break;
					case ModeType.Car:
						if (TDSessionManager.Current.JourneyEmissionsPageState.YourCarSize == "small")
						{
							ShowCarEmissions = false;
							showCarLargeEmissions = true;
						}
						if (TDSessionManager.Current.JourneyEmissionsPageState.YourCarSize == "large")
						{
							showCarLargeEmissions = false;
							ShowCarEmissions = true;
						}
						break;
					case ModeType.Bus:
						ShowBusEmissions = false;
						break;
					case ModeType.Coach:
						ShowCoachEmissions = false;
						break;
					case ModeType.Rail:
						ShowTrainEmissions = false;
						break;
					default:
						// do nothing
						break;
				}
			}
		}

		/// <summary>
		/// Displays the Passenger dropdown list and labels on Journey Emissions Row
		/// </summary>
		private void ShowPassengerListForJourney(bool show)
		{
			// Show one list or the another but not both as this effects the value saved to session property
			if (journeyEmissionsCompareMode == JourneyEmissionsCompareMode.JourneyDefault)
			{
				labelOutwardWith.Visible = show;
				labelOutwardPassengers.Visible = show;
				listOutwardPassengers.Visible = show;
				
				labelJourneyWith.Visible = !show;
				labelJourneyPassengers.Visible = !show;
				listJourneyPassengers.Visible = !show;

				listYourJourneyPassengers.Visible = !show;
				labelYourJourneyWith.Visible = !show;
				labelYourJourneyPassengers.Visible = !show;

			}
			else if (journeyEmissionsCompareMode == JourneyEmissionsCompareMode.JourneyCompare)
			{
				labelOutwardWith.Visible = !show;
				labelOutwardPassengers.Visible = !show;
				listOutwardPassengers.Visible = !show;

				labelJourneyWith.Visible = show;
				labelJourneyPassengers.Visible = show;
				listJourneyPassengers.Visible = show;

				listYourJourneyPassengers.Visible = show;
				labelYourJourneyWith.Visible = show;
				labelYourJourneyPassengers.Visible = show;
			}
		}

		#endregion

		#region Session save/load methods
		/// <summary>
		/// Load the properties for this object from the session		
		/// </summary>
		private void LoadPropertiesFromSession()
		{
			JourneyDistance = Convert.ToDecimal(TDSessionManager.Current.JourneyEmissionsPageState.JourneyDistance);
			JourneyDistanceToDisplay = TDSessionManager.Current.JourneyEmissionsPageState.JourneyDistanceToDisplay;
			
			listCarPassengers.SelectedIndex = TDSessionManager.Current.JourneyEmissionsPageState.CarPassenger;
			listCarLargePassengers.SelectedIndex = TDSessionManager.Current.JourneyEmissionsPageState.LargeCarPassenger;
			listOutwardPassengers.SelectedIndex = TDSessionManager.Current.JourneyEmissionsPageState.JourneyPassenger;
			listJourneyPassengers.SelectedIndex = TDSessionManager.Current.JourneyEmissionsPageState.JourneyPassenger;	
			listTableSmallCarPassengers.SelectedIndex = TDSessionManager.Current.JourneyEmissionsPageState.CarPassenger;
			listTableLargeCarPassengers.SelectedIndex = TDSessionManager.Current.JourneyEmissionsPageState.LargeCarPassenger;
			listYourJourneyPassengers.SelectedIndex = TDSessionManager.Current.JourneyEmissionsPageState.JourneyPassenger;
		}

		/// <summary>
		/// Saves the current properties for this object to the session
		/// </summary>
		private void SavePropertiesToSession()
		{		
			TDSessionManager.Current.JourneyEmissionsPageState.JourneyDistance = JourneyDistance.ToString();
			TDSessionManager.Current.JourneyEmissionsPageState.JourneyDistanceToDisplay = JourneyDistanceToDisplay;
		
			// This is in case the list is displayed on the journey emissions row because the journey was single mode
			if (listOutwardPassengers.Visible)
				TDSessionManager.Current.JourneyEmissionsPageState.JourneyPassenger = listJourneyPassengers.SelectedIndex;
			//else if (listJourneyPassengers.Visible)
				//TDSessionManager.Current.JourneyEmissionsPageState.JourneyPassenger = listOutwardPassengers.SelectedIndex;
			else
			{
				if(journeyEmissionsVisualMode == JourneyEmissionsVisualMode.Diagram)
				{
					TDSessionManager.Current.JourneyEmissionsPageState.CarPassenger = listCarPassengers.SelectedIndex;
					TDSessionManager.Current.JourneyEmissionsPageState.LargeCarPassenger = listCarLargePassengers.SelectedIndex;
					TDSessionManager.Current.JourneyEmissionsPageState.JourneyPassenger = listYourJourneyPassengers.SelectedIndex;
				}
				else
				{
					TDSessionManager.Current.JourneyEmissionsPageState.CarPassenger = listTableSmallCarPassengers.SelectedIndex;
					TDSessionManager.Current.JourneyEmissionsPageState.LargeCarPassenger = listTableLargeCarPassengers.SelectedIndex;
					TDSessionManager.Current.JourneyEmissionsPageState.JourneyPassenger = listYourJourneyPassengers.SelectedIndex;
				}
			}	
		}
		
		///<summary>
		/// The client may have changed things through JavaScript so need to update server state.  Used to set the journey distance units.
		///</summary>
		private void AlignServerWithClient()	
		{	
			// Ensures correct we have the correct units selected if we return from the Help page
			if (((!IsPostBack)) && (NonPrintable))
			{
				if (TDSessionManager.Current.JourneyEmissionsPageState.JourneyDistanceUnit == 1)
					RoadUnits = RoadUnitsEnum.Miles;
				else
					RoadUnits = RoadUnitsEnum.Kms;
			}

			string HiddenUnitsField = "";
			if (this.ClientID != null && this.ClientID != "journeyEmissionsCompareControl")
			{
				HiddenUnitsField = this.ClientID;
			}
			else
			{
				HiddenUnitsField = "journeyEmissionsCompareControl";
			}

			if (Request.Params[HiddenUnitsField + "_hdnUnitsState"] != null)
			{
				roadUnits = (RoadUnitsEnum) Enum.Parse(typeof(RoadUnitsEnum), Request.Params[HiddenUnitsField + "_hdnUnitsState"], true);			
			}

			RoadUnitsEnum serverUnits = roadUnits;
			TDSessionManager.Current.JourneyEmissionsPageState.Units = serverUnits;
			
			if (roadUnits == RoadUnitsEnum.Miles)
			{
				dropdownlistUnits1.SelectedIndex = 0;
                dropdownlistUnits2.SelectedIndex = 0;
			}
			else
			{
				dropdownlistUnits1.SelectedIndex = 1;
                dropdownlistUnits2.SelectedIndex = 1;	
			}
		}

		
		///<summary>
		/// The client and server need to be kept in sync. Used in postbacks to set the journey distance units
		///</summary>
		private void AlignClientWithServer()
		{
			if (roadUnits == RoadUnitsEnum.Miles)
			{
				dropdownlistUnits1.SelectedIndex = 0;
                dropdownlistUnits2.SelectedIndex = 0;
			}
			else
			{
				dropdownlistUnits1.SelectedIndex = 1;
                dropdownlistUnits2.SelectedIndex = 1;	
			}

			TDSessionManager.Current.JourneyEmissionsPageState.Units = roadUnits;
		}

		#endregion

		/// <summary>
		/// Populates the Passenger dropdown list with the number of passngers
		/// </summary>
		private void PopulatePassengerList()
		{
			int smallMaxNumberOfPassengers;
			int mediumMaxNumberOfPassengers;
			int largeMaxNumberOfPassengers;

			try
			{
				smallMaxNumberOfPassengers = Convert.ToInt32(Properties.Current["JourneyEmissions.MaxNumberOfCarPassengers.Small"]);
			}
			catch
			{
				smallMaxNumberOfPassengers = 1;
			}

			try
			{
				mediumMaxNumberOfPassengers = Convert.ToInt32(Properties.Current["JourneyEmissions.MaxNumberOfCarPassengers.Medium"]);
			}
			catch
			{
				mediumMaxNumberOfPassengers = 1;
			}

			try
			{
				largeMaxNumberOfPassengers = Convert.ToInt32(Properties.Current["JourneyEmissions.MaxNumberOfCarPassengers.Large"]);
			}
			catch
			{
				largeMaxNumberOfPassengers = 1;
			}

			if (smallMaxNumberOfPassengers > 0)
			{
				for (int i=1; i<= smallMaxNumberOfPassengers; i++)
				{
					
					//if user has small car
					if (TDSessionManager.Current.JourneyEmissionsPageState.YourCarSize == "small")
					{
						listJourneyPassengers.Items.Add(i.ToString());
						listYourJourneyPassengers.Items.Add(i.ToString());
						listOutwardPassengers.Items.Add(i.ToString());
					}
					listTableSmallCarPassengers.Items.Add(i.ToString());
					listCarPassengers.Items.Add(i.ToString());
				
				}
			}

			if (mediumMaxNumberOfPassengers > 0)
			{
				for (int i=1; i<= mediumMaxNumberOfPassengers; i++)
				{
					//if user has medium car
					if ((TDSessionManager.Current.JourneyEmissionsPageState.YourCarSize == "medium")
                        ||
                        (string.IsNullOrEmpty(TDSessionManager.Current.JourneyEmissionsPageState.YourCarSize)))
					{
						listJourneyPassengers.Items.Add(i.ToString());
						listYourJourneyPassengers.Items.Add(i.ToString());
						listOutwardPassengers.Items.Add(i.ToString());
					}
					//listOutwardPassengers.Items.Add(i.ToString());

				}
			}

			if (largeMaxNumberOfPassengers > 0)
			{
				for (int i=1; i<= largeMaxNumberOfPassengers; i++)
				{
					//listCarPassengers.Items.Add(i.ToString());
					//if user has large car
					if (TDSessionManager.Current.JourneyEmissionsPageState.YourCarSize == "large")
					{
						listJourneyPassengers.Items.Add(i.ToString());
						listYourJourneyPassengers.Items.Add(i.ToString());
						listOutwardPassengers.Items.Add(i.ToString());
					}

					listCarLargePassengers.Items.Add(i.ToString());
					listTableLargeCarPassengers.Items.Add(i.ToString());				
				}
			}
		}

		/// <summary>
		/// Sets the alternating background colours of all the rows in the table
		/// </summary>
		private void SetRowColours()
		{
			int rowNum = 1;
			TableRow row;

			for (int i = 0; i < tableJourneyEmissionsCompare.Rows.Count; i++)
			{
				row = tableJourneyEmissionsCompare.Rows[i];

				if (row.Visible)
				{
					if ((rowNum % 2) == 0)
						row.CssClass = RowEven;
					else
						row.CssClass = RowOdd;

					rowNum++;
				}
			}
		}

		#endregion

		#region Protected methods

		///<summary>
		/// The EnableClientScript property of a scriptable control is set so that they
		/// output an action attribute when appropriate.
		/// If JavaScript is enabled then appropriate script blocks are added to the page.
		///</summary>
		protected void EnableScriptableObjects()
		{
			bool javaScriptSupported = bool.Parse((string) Session[((TDPage)Page).Javascript_Support]);
			string javaScriptDom = (string) Session[((TDPage)Page).Javascript_Dom];

			if (javaScriptSupported) 
			{
				dropdownlistUnits1.Visible = showHeaderDistanceUnitsDropDown;
                dropdownlistUnits2.Visible = !showHeaderDistanceUnitsDropDown;
				
                dropdownlistUnits1.EnableClientScript = true;
                dropdownlistUnits2.EnableClientScript = true;

                ScriptRepository.ScriptRepository scriptRepository = (ScriptRepository.ScriptRepository)TDServiceDiscovery.Current[ServiceDiscoveryKey.ScriptRepository];

                Page.ClientScript.RegisterStartupScript(typeof(JourneyEmissionsCompareControl), dropdownlistUnits1.ScriptName, scriptRepository.GetScript(dropdownlistUnits1.ScriptName, javaScriptDom));
			}
			else
			{
                labelDistanceUnits1.Visible = false;
				dropdownlistUnits1.Visible = false;
                labelDistanceUnits2.Visible = false;
                dropdownlistUnits2.Visible = false;
			}
		}

		/// <summary>
		/// Returns the string containing the Transport Header for the other modes table
		/// </summary>
		/// <returns>string</returns>
		protected string GetTransportHeader() 
		{
			return  this.ClientID+"_headerTransport";
		}


		/// <summary>
		/// Returns the string containing the Emission Header for the other modes table
		/// </summary>
		/// <returns>string</returns>
		protected string GetEmissionsHeader() 
		{
			return  this.ClientID+"_headerEmissions";
		}

		/// <summary>
		/// Returns the string containing the Transport Header for the other modes table
		/// </summary>
		/// <returns>string</returns>
		protected string GetYourTransportHeader() 
		{
			return  this.ClientID+"_headerYourTransport";
		}


		/// <summary>
		/// Returns the string containing the Emission Header for the other modes table
		/// </summary>
		/// <returns>string</returns>
		protected string GetYourEmissionsHeader() 
		{
			return  this.ClientID+"_headerYourEmissions";
		}


		/// <summary>
		/// Returns the string containing the Table Summary for your journey emissions
		/// </summary>
		/// <returns>string</returns>
		protected string GetYourTableSummary() 
		{
			return  GetResource("JourneyEmissionsCompareControl.YourTableSummary.Text");
		}


		/// <summary>
		/// CReturns the string containing the Table Summary for other modes' emissions
		/// </summary>
		/// <returns></returns>
		protected string GetEmissionsTableSummary()
		{
			return  GetResource("JourneyEmissionsCompareControl.EmissionsTableSummary.Text"); 
		}

		#endregion

		#region Public properties

		/// <summary>
		/// Read/Write. Journey distance in metres used to calculate emissions for different modes
		/// </summary>
		public decimal JourneyDistance
		{
			get{ return journeyDistance; }
			set{ journeyDistance = value; }
		}

		/// <summary>
		/// Read/Write. Journey distance to display on the control. Note the value is rounded to 1dp
		/// Leave empty to use JourneyDistance property, which is then converted to Miles and Kms
		/// </summary>
		public string JourneyDistanceToDisplay
		{
			get{ return journeyDistanceToDisplay; }
			set
			{	// place in try catch in case value cannot be converted, thus being an invaid value
				try
				{
					decimal distance = Convert.ToDecimal(value);
					distance = Decimal.Round(distance, 1);
					journeyDistanceToDisplay = distance.ToString();
				}
				catch
				{
					journeyDistanceToDisplay = string.Empty;
				}
			}
		}

		/// <summary>
		/// Read/Write. Sets the Journey distance Road units
		/// </summary>
		public RoadUnitsEnum RoadUnits
		{
			get{ return roadUnits; }
			set{ roadUnits = value; }
		}

		/// <summary>
		/// Read/Write. Flag to use journey in the current Session Manager.
		/// This will tell the control to use the selected journey within the Session Manager to
		/// determine the journey distance and CO2 emissions.
		/// </summary>
		public bool UseSessionManager
		{
			get{ return sessionManager; }
			set{ sessionManager = value; }
		}

		/// <summary>
		/// Read/Write. Flag to use journey in the current Itinerary Manager.
		/// This will tell the control to use the journey within the Itinerary Manager to
		/// determine the journey distance and CO2 emissions.
		/// This takes priority of UseSessionManager flag
		/// </summary>
		public bool UseItineraryManager
		{
			get{ return itineraryManger; }
			set{ itineraryManger = value; }
		}

		/// <summary>
		/// Read/Write. Emissions to display for Outward journey
		/// </summary>
		public decimal EmissionsOutwardJourney
		{
			get{ return emissionsOutwardJourney; }
			set{ emissionsOutwardJourney = value; }
		}

		/// <summary>
		/// Read/Write. Emissions to display for Return journey
		/// </summary>
		public decimal EmissionsReturnJourney
		{
			get{ return emissionsReturnJourney; }
			set{ emissionsReturnJourney = value; }
		}

		/// <summary>
		/// Read/Write. Sets the CompareMode this control is in
		/// </summary>
		public JourneyEmissionsCompareMode JourneyEmissionsCompareMode
		{
			get{ return journeyEmissionsCompareMode; }
			set{ journeyEmissionsCompareMode = value; }
		}

		/// <summary>
		/// Read/Write. Sets the JourneyEmissionsVisualMode this control is in
		/// </summary>
		public JourneyEmissionsVisualMode JourneyEmissionsVisualMode
		{
			get { return journeyEmissionsVisualMode; }
			set { journeyEmissionsVisualMode = value; }
		}

		/// <summary>
		/// Read/Write. If this control is in printable mode or not.
		/// </summary>
		public bool NonPrintable 
		{
			get{ return nonprintable; }
			set{ nonprintable = value; }
		}

		/// <summary>
		/// Read/Write. Sets the visibility of the Car emissions row
		/// </summary>
		public bool ShowCarEmissions
		{
			get{ return showCarEmissions; }
			set{ showCarEmissions = value; }
		}

		/// <summary>
		/// Read/Write. Sets the visibility of the Train emissions row
		/// </summary>
		public bool ShowTrainEmissions
		{
			get{ return showTrainEmissions; }
			set{ showTrainEmissions = value; }
		}

		/// <summary>
		/// Read/Write. Sets the visibility of the Bus/Coach emissions row
		/// </summary>
		public bool ShowBusEmissions
		{
			get{ return showBusEmissions; }
			set{ showBusEmissions = value; }
		}

		/// <summary>
		/// Read/Write. Sets the visibility of the Coach emissions row
		/// </summary>
		public bool ShowCoachEmissions
		{
			get{ return showCoachEmissions; }
			set{ showCoachEmissions = value; }
		}

		/// <summary>
		/// Read/Write. Sets the visibility of the Plane emissions row
		/// </summary>
		public bool ShowPlaneEmissions
		{
			get{ return showPlaneEmissions; }
			set{ showPlaneEmissions = value; }
		}

		/// <summary>
		/// Read. Gets the hidden units input id
		/// </summary>
		public string GetHiddenInputId
		{
			get{ return this.ClientID + "_hdnUnitsState"; }
		}

		/// <summary>
		/// Read. Gets the current Road Units value
		/// </summary>
		public string UnitsState
		{
			get{ return RoadUnits.ToString(); }
		}

        /// <summary>
        /// Read/Write. True to show the header units switch dropdown, False to display
        /// the footer units switch dropdown. Default is true
        /// </summary>
        public bool ShowHeaderUnitsDropdown
        {
            get { return showHeaderDistanceUnitsDropDown; }
            set { showHeaderDistanceUnitsDropDown = value; }
        }

		/// <summary>
		/// Read/Write. Sets the visibility of the Emissions Table View
		/// </summary>
		public bool ShowEmissionsTable
		{
			get{ return showEmissionsTable; }
			set{ showEmissionsTable = value; }
		}

        /// <summary>
        /// Read/Write. Sets the visibility of the Emissions Table View
        /// </summary>
        public bool PageLandingActive
        {
            get { return pageLandingActive; }
            set { pageLandingActive = value; }
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
			this.listTableSmallCarPassengers.SelectedIndexChanged += new System.EventHandler(this.listPassengers_Changed);
			this.listTableLargeCarPassengers.SelectedIndexChanged += new System.EventHandler(this.listPassengers_Changed);
			this.listCarLargePassengers.SelectedIndexChanged += new System.EventHandler(this.listPassengers_Changed);
			this.listCarPassengers.SelectedIndexChanged += new System.EventHandler(this.listPassengers_Changed);
			this.listJourneyPassengers.SelectedIndexChanged += new System.EventHandler(this.listPassengers_Changed);
			this.listYourJourneyPassengers.SelectedIndexChanged += new System.EventHandler(this.listPassengers_Changed);
            this.buttonChangeView.Click += new System.EventHandler(buttonChangeView_Click);
            this.buttonShowJourney.Click += new System.EventHandler(buttonShowJourney_Click);
			this.Load += new System.EventHandler(this.Page_Load);
			this.PreRender += new System.EventHandler(this.Page_PreRender);

		}
		#endregion

		#region Event handlers
		/// <summary>
		/// Saves the current properties for this object to the session
		/// </summary>
		private void listPassengers_Changed(object sender, System.EventArgs e)
		{		
			// This is in case the list is displayed on the journey emissions row because the journey was single mode
			if (listOutwardPassengers.Visible)
				TDSessionManager.Current.JourneyEmissionsPageState.JourneyPassenger = listOutwardPassengers.SelectedIndex;
			else if (listJourneyPassengers.Visible)
			{
				//TDSessionManager.Current.JourneyEmissionsPageState.JourneyPassenger = listJourneyPassengers.SelectedIndex;
				if(emissionsGraphicalPanel.Visible)
				{
					TDSessionManager.Current.JourneyEmissionsPageState.CarPassenger = listCarPassengers.SelectedIndex;
					TDSessionManager.Current.JourneyEmissionsPageState.LargeCarPassenger = listCarLargePassengers.SelectedIndex;
					TDSessionManager.Current.JourneyEmissionsPageState.JourneyPassenger = listJourneyPassengers.SelectedIndex;
				}
				else
				{
					TDSessionManager.Current.JourneyEmissionsPageState.CarPassenger = listTableSmallCarPassengers.SelectedIndex;
					TDSessionManager.Current.JourneyEmissionsPageState.LargeCarPassenger = listTableLargeCarPassengers.SelectedIndex;
					TDSessionManager.Current.JourneyEmissionsPageState.JourneyPassenger = listYourJourneyPassengers.SelectedIndex;
				}
				

			}
			else if (TDSessionManager.Current.JourneyEmissionsPageState.JourneyEmissionsVisualMode == JourneyEmissionsVisualMode.Diagram)
			{
				TDSessionManager.Current.JourneyEmissionsPageState.CarPassenger = listCarPassengers.SelectedIndex;
				TDSessionManager.Current.JourneyEmissionsPageState.LargeCarPassenger = listCarLargePassengers.SelectedIndex;
				TDSessionManager.Current.JourneyEmissionsPageState.JourneyPassenger = listJourneyPassengers.SelectedIndex;
			}
			else
			{
				TDSessionManager.Current.JourneyEmissionsPageState.CarPassenger = listTableSmallCarPassengers.SelectedIndex;
				TDSessionManager.Current.JourneyEmissionsPageState.LargeCarPassenger = listTableLargeCarPassengers.SelectedIndex;
				TDSessionManager.Current.JourneyEmissionsPageState.JourneyPassenger = listYourJourneyPassengers.SelectedIndex;
			}

		}
	
        /// <summary>
        /// Handles the visibility of the Table / Diagram mode
        /// </summary>
        private void buttonChangeView_Click(object sender, EventArgs e)
        {
            //Change to other view.
            if (emissionsGraphicalPanel.Visible == false)
            {
                journeyEmissionsVisualMode = JourneyEmissionsVisualMode.Diagram;
            }
            else
            {
                journeyEmissionsVisualMode = JourneyEmissionsVisualMode.Table;
            }

            TDSessionManager.Current.JourneyEmissionsPageState.JourneyEmissionsVisualMode = journeyEmissionsVisualMode;

        }

        /// <summary>
        /// Redisplays the details page
        /// </summary>
        private void buttonShowJourney_Click(object sender, EventArgs e)
        {
            TDItineraryManager.Current.JourneyViewState.ShowCO2 = false;
            TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.GoJourneyDetails;
        }

	#endregion

	}
}
