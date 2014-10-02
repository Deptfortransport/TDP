// *********************************************** 
// NAME                 : FindStationResultsTable.ascx
// AUTHOR               : Patrick ASSUIED
// DATE CREATED         : 14/05/2004 
// DESCRIPTION  : Control displaying stations sorted by airport name or distance
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/FindStationResultsTable.ascx.cs-arc  $ 
//
//   Rev 1.13   Oct 27 2010 15:01:56   rbroddle
//Removed explicit wire up to Page_Load as AutoEventWireUp=true for this control so it was firing twice.
//Resolution for 5621: USD8048975 - page_init and other events wired up more than once on some pages and controls
//
//   Rev 1.12   Jun 09 2010 09:04:44   apatel
//Updated to remove javascript eschape characters "'" and "/"
//Resolution for 5550: FindNearest and FindCarPark break with "'" in the content
//
//   Rev 1.11   Jun 08 2010 15:53:42   apatel
//Updated to remove the Javascript escape characters from content passed in a link
//
//Resolution for 5550: FindNearest and FindCarPark break with "'" in the content
//
//   Rev 1.10   Mar 11 2010 12:35:52   mmodi
//Pass in a content string to show in a pop for may location symbols
//Resolution for 5440: Maps - Issue 16 ESRI fixes
//
//   Rev 1.9   Dec 07 2009 11:22:08   mmodi
//Corrected scroll to map issue in IE
//
//   Rev 1.8   Nov 24 2009 14:01:12   mmodi
//Corrected zooming in to stop
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.7   Nov 20 2009 09:54:44   apatel
//added property for default level
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.6   Nov 20 2009 09:26:30   apatel
//updated for map enhancement when javascript is disabled
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.5   Oct 01 2009 16:22:42   apatel
//updated for StopInformationPage
//Resolution for 5315: CCN526 Stop Information Page Landing
//
//   Rev 1.4   Dec 17 2008 15:52:02   apatel
//XHTML Compliance Changes
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.3   Apr 02 2008 10:20:34   mmodi
//Imported from Dev Factory
//
//   Rev DevFactory   Dec 31 2008 18:00:00   mmodi
//Updated station link to not be displayed when in printable mode
//
//   Rev 1.2   Mar 31 2008 13:20:50   mturner
//Drop3 from Dev Factory
//
//   Rev 1.1   Dec 19 2007 10:41:16   jfrank
//Updated for .net 2 release, fixed Null reference exception bug.
//
//   Rev 1.0   Nov 08 2007 13:14:28   mturner
//Initial revision.
//
//   Rev 1.26   Feb 23 2006 19:16:42   build
//Automatically merged from branch for stream3129
//
//   Rev 1.25.1.0   Jan 10 2006 15:24:56   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.25   Nov 15 2005 18:00:00   RGriffith
//Added a ToolTip for the 'i' information button
//
//   Rev 1.24   Nov 10 2005 16:30:56   ralonso
//2982  Open  UEE: information button on results page errors  
//
//   Rev 1.23   Nov 03 2005 16:18:22   kjosling
//Automatically merged from branch for stream2816
//
//   Rev 1.22.1.0   Oct 13 2005 17:40:18   RGriffith
//TD089 ES020 Image Button Replacement
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages
//
//   Rev 1.22   Nov 03 2004 12:54:10   passuied
//Changes to enable a new FindAMode TrunkStation similar to Trunk but with differences...
//
//   Rev 1.21   Nov 01 2004 18:03:24   passuied
//updated so transport column shown in FindTrunk mode stationmode
//
//   Rev 1.20   Oct 08 2004 14:57:24   jgeorge
//Removed "Tick all" box. 
//Resolution for 1693: Changes to 'tick all' options in Find nearest station/airport results
//
//   Rev 1.19   Sep 23 2004 18:27:50   passuied
//translated the resource value of transport column
//Resolution for 1633: Find Nearest station: The transport column doesn't get translated when switching between eng/welsh
//
//   Rev 1.18   Sep 23 2004 13:12:48   JHaydock
//IR1618 - Update to sorting on find nearest search results
//
//   Rev 1.17   Sep 14 2004 15:59:28   passuied
//minor bug that was allowing to sort columns even in printable page
//Resolution for 1579: Find nearest - Unable to deselect tick all button on some browsers
//
//   Rev 1.16   Sep 14 2004 15:48:30   passuied
//Changed the way display/hide columns. Use div with style set to "display: none" instead of panels. This is because panels are interpreted as tables in Netscape and this screws up the javascript
//Resolution for 1579: Find nearest - Unable to deselect tick all button on some browsers
//
//   Rev 1.15   Sep 04 2004 18:17:40   passuied
//Added new sortable and hiddable Transport column
//Resolution for 1459: Find a Sorting order and arrows not working as requested by the DfT
//
//   Rev 1.14   Aug 18 2004 16:29:38   RPhilpott
//Add cast from int to double to mileage formatting.
//Resolution for 1377: Mileage display on Find Station results
//
//   Rev 1.13   Aug 02 2004 15:30:42   jbroome
//IR 1252 - Fixed minor error - made sure checked box defaults to unchecked the first time page is loaded.
//
//   Rev 1.12   Aug 02 2004 11:32:02   jbroome
//IR1252 - Tick All check box retains correct "checked" property after postback.
//
//   Rev 1.11   Jul 27 2004 15:18:06   passuied
//Del6.1 Added "Station name" image displayed in result table
//
//   Rev 1.10   Jul 22 2004 18:05:56   passuied
//Integration between pages and move of code to location service
//
//   Rev 1.9   Jul 14 2004 13:00:24   passuied
//Changes in SessionManager with impact in Web for Del 6.1
//Compiles
//
//   Rev 1.8   Jul 01 2004 14:12:52   passuied
//changes following exhaustive testing
//
//   Rev 1.7   Jun 23 2004 15:54:48   passuied
//addition for Results message functionality
//
//   Rev 1.6   Jun 07 2004 13:57:04   passuied
//fixed table within table problem in Mozilla/Netscape caused by Panels
//
//   Rev 1.5   Jun 04 2004 15:18:20   passuied
//default for tickall set to checked=false
//
//   Rev 1.4   Jun 02 2004 16:38:32   passuied
//working version
//
//   Rev 1.3   May 28 2004 17:48:14   passuied
//update as part of FindStation development
//
//   Rev 1.2   May 24 2004 12:12:34   passuied
//checked in to comply with control changes
//
//   Rev 1.1   May 21 2004 15:49:48   passuied
//partly working Find station pages and controls. Check in for backup
//
//   Rev 1.0   May 14 2004 17:35:24   passuied
//Initial Revision


using System;using TransportDirect.Common.ResourceManager;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using TransportDirect.Common;
using TransportDirect.UserPortal.Resource;
using TransportDirect.Web.Support;
using TransportDirect.UserPortal.Web;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.ScriptRepository;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.Web.Adapters;
using System.Text;

namespace TransportDirect.UserPortal.Web.Controls
{

	/// <summary>
	///	Control displaying stations sorted by airport name or distance
	/// </summary>
	public partial  class FindStationResultsTable : TDUserControl
	{
        #region Declaration
        protected System.Web.UI.WebControls.Repeater repeaterResultTable;

        private FindStationPageState stationPageState;
        private InputPageState inputPageState;

        private string sortAscUrl = "/web2/images/gifs/SoftContent/TravelNewsSortAscending.gif";
        private string sortDescUrl = "/web2/images/gifs/SoftContent/TravelNewsSortDescending.gif";
        private string sortAscAlternateText;
        private string sortDescAlternateText;

        private bool boolPrintablePage = false;

        private bool rowSelectLinkVisible = false;

        private string mapClientID = string.Empty;
        private string mapScrollToID = string.Empty;

        private MapHelper mapHelper = new MapHelper();

        public event EventHandler InformationRequested;

        #endregion

        #region Constructor
        public FindStationResultsTable()
        {
            stationPageState = TDSessionManager.Current.FindStationPageState;
            inputPageState = TDSessionManager.Current.InputPageState;
        }
        #endregion

        #region Private Methods
        private void StoreAirportSelection()
        {

            // for each row in repeater, find the check box. 
            // Then change the appropriate row in FindStationPageState.AirportResultTable
            for (int i = 0; i < repeaterResultTable.Items.Count; i++)
            {
                RepeaterItem item = repeaterResultTable.Items[i];

                DataRow sourceRow = ((DataRow[])repeaterResultTable.DataSource)[item.ItemIndex];

                CheckBox changedCheckBox = (CheckBox)item.FindControl("checkAirportSelect");

                if (changedCheckBox != null)
                {
                    // in following needs to use index specified in the column 'Index'.
                    // Indeed, we cannot rely on the repeater index, as it changes 
                    // all the time because of sorting



                    // first select index row to select in repeater
                    int stateRowIndex = (int)sourceRow[FindStationHelper.columnIndex];
                    // then select pageState row to update
                    DataRow stateRow = stationPageState.StationResultsTable.Rows[stateRowIndex];

                    // finally update row
                    stateRow[FindStationHelper.columnSelected] = changedCheckBox.Checked;


                }
            }
        }
        #endregion

        #region Page Load and Refresh
        private void Page_Load(object sender, System.EventArgs e)
        {
            // Get resources
            sortAscAlternateText = GetResource("FindStationResultsTable.sortAscAlternateText");
            sortDescAlternateText = GetResource("FindStationResultsTable.sortDescAlternateText");
            sortAscUrl = GetResource("CarParking.Ascending.ImageURL");
            sortDescUrl = GetResource("CarParking.Descending.ImageURL");

            Refresh();
        }

        private void Refresh()
        {
            switch (stationPageState.CurrentSortingType)
            {
                case FindStationPageState.SortingType.StationName:
                    {
                        repeaterResultTable.DataSource = FindStationHelper.SortResultsDataTableByName(
                            stationPageState.StationResultsTable,
                            stationPageState.IsStationSortingAsc);
                        break;
                    }
                case FindStationPageState.SortingType.Distance:
                    {
                        repeaterResultTable.DataSource = FindStationHelper.SortResultsDataTableByDistance(
                            stationPageState.StationResultsTable,
                            stationPageState.IsDistanceSortingAsc);
                        break;
                    }
                case FindStationPageState.SortingType.Option:
                    {
                        repeaterResultTable.DataSource = FindStationHelper.SortResultsDataTableByOption(
                            stationPageState.StationResultsTable,
                            stationPageState.IsOptionSortingAsc);
                        break;
                    }
                case FindStationPageState.SortingType.Transport:
                    {
                        repeaterResultTable.DataSource = FindStationHelper.SortResultsDataTableByTransport(
                            stationPageState.StationResultsTable,
                            stationPageState.IsTransportSortingAsc);
                        break;
                    }

            }

            repeaterResultTable.DataBind();
        }
        #endregion

        #region Public Methods for Repeater
        public string GetRowIndex(DataRow rowAirport)
        {
            int index = (int)rowAirport[FindStationHelper.columnIndex] + 1;
            return index.ToString(TDCultureInfo.CurrentUICulture.NumberFormat);
        }

        /// <summary>
        /// Generates and return a script to associate with a link to show station information on map
        /// </summary>
        /// <param name="row">data row object</param>
        /// <returns>javascript string</returns>
        public string GetShowOnMapScript(DataRow rowAirport)
        {
            string linkScript = string.Empty;

            if (IsRowSelectLinkVisible && !string.IsNullOrEmpty(mapClientID) && !string.IsNullOrEmpty(mapScrollToID))
            {
                StringBuilder showOnMapScript = new StringBuilder(string.Format("scrollToElement('{0}');",mapScrollToID));

                string mapLevel = Properties.Current["MapControl.FindNearest.DefaultMapLevel"];

                if (string.IsNullOrEmpty(mapLevel))
                {
                    mapLevel = "9";
                }

                // Build up the content to be shown in the popup
                string stopname = rowAirport[FindStationHelper.columnStationName].ToString();
                string stopInfoLink = mapHelper.GetStopInformationLink(rowAirport[FindStationHelper.columnNaptanId].ToString());
                // Removed "'" and "\" from stop name as they causing problems when rendered in the esri map api
                string content = "<b>" + stopname.Replace("\\", "").Replace("\'", "\\\'") + "</b><br />" + stopInfoLink;

                showOnMapScript.Append("try { ");
                showOnMapScript.AppendFormat("ESRIUKTDPAPI.zoomToLevelAndPoint('{0}',{1},{2},{3},{4},'{5}','{6}');", 
                    mapClientID,
                    (int)rowAirport[FindStationHelper.columnIndex] + 1,
                    rowAirport[FindStationHelper.columnEasting].ToString(),
                    rowAirport[FindStationHelper.columnNorthing].ToString(), 
                    mapLevel, 
                    " ",
                    Server.HtmlEncode(content));
                showOnMapScript.Append(" }catch(err){}return false;");

                linkScript = showOnMapScript.ToString();
            }

            return linkScript;
        }

        public string GetAirportName(DataRow rowAirport)
        {
            return (string)rowAirport[FindStationHelper.columnStationName];
        }

        public string GetTransport(DataRow rowAirport)
        {
            return GetResource(
                string.Format(FindStationHelper.TRANSPORT_TYPE, rowAirport[FindStationHelper.columnTransport]));
        }

        public string GetDistance(DataRow rowAirport)
        {
            string miles = Global.tdResourceManager.GetString(
                "FindStation.miles",
                TDCultureInfo.CurrentUICulture);

            return String.Format("{0:F1} {1}", (double)((int)rowAirport[FindStationHelper.columnDistance]) / 1609, miles);
        }

        public bool GetSelected(DataRow rowAirport)
        {
            return (bool)rowAirport[FindStationHelper.columnSelected];
        }

        public string GetInfoArgument(DataRow rowAirport)
        {
            return (string)rowAirport[FindStationHelper.columnNaptanId];
        }

        /// <summary>
        /// Returns the Css class that the row text should be rendered with.
        /// </summary>
        /// <param name="summary">Current item being rendered.</param>
        /// <returns>Css class string.</returns>
        public string GetTextCssClass(int index)
        {
            // If there is only one result then no rows should
            // be highlighed. Check to see if this is the case.
            if (stationPageState.StationResultsTable.Rows.Count == 1)
                return string.Empty;


            return (index % 2) == 0 ? "g" : "";
        }

        /// <summary>
        /// Returns the button CssClass
        /// </summary>
        /// <returns></returns>
        public string GetButtonCssClass()
        {
            return Properties.Current["WebControlLibrary.TDButton.DefaultStyle"].ToString() + " TDButtonSecondary";
        }

        /// <summary>
        /// Returns the button mouse over CssClass
        /// </summary>
        /// <returns></returns>
        public string GetButtonCssClassMouseOver()
        {
            return Properties.Current["WebControlLibrary.TDButton.DefaultMouseOverStyle"].ToString() + " TDButtonSecondaryMouseOver";
        }

        public string StationNameHeaderStyle
        {
            get
            {
                if (stationPageState.CurrentSortingType == FindStationPageState.SortingType.StationName)
                    return "p";
                else
                    return string.Empty;
            }
        }


        public string OptionHeaderStyle
        {
            get
            {
                if (stationPageState.CurrentSortingType == FindStationPageState.SortingType.Option)
                    return "p";
                else
                    return string.Empty;
            }
        }

        public string TransportHeaderStyle
        {
            get
            {
                if (stationPageState.CurrentSortingType == FindStationPageState.SortingType.Transport)
                    return "p";
                else
                    return string.Empty;
            }
        }

        public string DistanceHeaderStyle
        {
            get
            {
                if (stationPageState.CurrentSortingType == FindStationPageState.SortingType.Distance)
                    return "p";
                else
                    return string.Empty;
            }
        }

        private const string displayNoneStyle = "display: none";

        public string IsSelectVisible
        {
            get
            {
                string style = boolPrintablePage ? displayNoneStyle : String.Empty;
                return style;
            }
        }

        public string IsTransportVisible
        {
            get
            {
                // We want to show the transport column if in trunk station mode
                bool trunkStationMode = (TDSessionManager.Current.FindAMode == FindAMode.TrunkStation);

                // show as well if in classic station mode (as original)
                string style = (TDSessionManager.Current.FindAMode == FindAMode.Station || trunkStationMode) ?
                    string.Empty : displayNoneStyle;
                return style;
            }
        }

        /// <summary>
        /// Read/Write property to determin if the row index should show as link
        /// </summary>
        public bool IsRowSelectLinkVisible
        {
            get
            {
                TDPage page = (TDPage)this.Page;
                return rowSelectLinkVisible && !boolPrintablePage && page.IsJavascriptEnabled;
            }
            set
            {
                rowSelectLinkVisible = value;
            }
        }

        /// <summary>
        /// Map client side id to generate script to show station information on map when they get clicked
        /// </summary>
        public string MapClientID
        {
            set
            {
                mapClientID = value;
            }
        }

        /// <summary>
        /// Map scroll to id to allow page to scroll to the first map element
        /// </summary>
        public string MapScrollToID
        {
            set { mapScrollToID = value; }
        }

        /// <summary>
        /// Returns visibility for the Station Link
        /// </summary>
        /// <returns></returns>
        public bool IsStationLinkVisible()
        {
            return !boolPrintablePage;
        }

        /// <summary>
        /// Returns visibility for the Station Label
        /// </summary>
        /// <returns></returns>
        public bool IsStationLabelVisible()
        {
            return boolPrintablePage;
        }

        #endregion


        #region Public methods for pages
        public bool Printable
        {
            set
            {
                boolPrintablePage = value;
            }
        }
        #endregion

        #region Public Properties for Repeater

        public string OptionTitle
        {
            get
            {
                return GetResource("FindStationResultsTable.labelOptionTitle");
            }
        }

        public string TransportTitle
        {
            get
            {
                return GetResource("FindStationResultsTable.labelTransportTitle");
            }
        }

        public string AirportStationTitle
        {
            get
            {
                if (TDSessionManager.Current.FindPageState.Mode == FindAMode.Flight)
                {
                    return GetResource("FindStationResultsTable.labelAirportNameTitle");
                }
                else
                {
                    return GetResource("FindStationResultsTable.labelStationNameTitle");
                }
            }
        }

        public string InfoTitle
        {
            get
            {
                return GetResource("FindStationResultsTable.labelInfoTitle");
            }
        }

        public string DistanceTitle
        {
            get
            {
                return GetResource("FindStationResultsTable.labelDistanceTitle");
            }
        }

        public string SelectTitle
        {
            get
            {
                return GetResource("FindStationResultsTable.labelSelectTitle");
            }
        }

        public string SortByOptionUrl
        {
            get
            {
                return GetResource("FindStationResultsTable.commandSortByOption.ImageUrl");
            }
        }

        public string SortByOptionAlternateText
        {
            get
            {
                return GetResource("FindStationResultsTable.commandSortByOption.AlternateText");
            }
        }

        public string SortByAirportNameUrl
        {
            get
            {
                if (TDSessionManager.Current.FindPageState.Mode == FindAMode.Flight)
                {
                    return GetResource("FindStationResultsTable.commandSortByAirportName.ImageUrl");
                }
                else
                {
                    return GetResource("FindStationResultsTable.commandSortByStationName.ImageUrl");
                }
            }
        }

        public string SortByAirportNameAlternateText
        {
            get
            {
                if (TDSessionManager.Current.FindPageState.Mode == FindAMode.Flight)
                {
                    return GetResource("FindStationResultsTable.commandSortByAirportName.AlternateText");
                }
                else
                {
                    return GetResource("FindStationResultsTable.commandSortByStationName.AlternateText");
                }

            }
        }

        public string SortByDistanceUrl
        {
            get
            {
                return GetResource("FindStationResultsTable.commandSortByDistance.ImageUrl");
            }
        }

        public string SortByDistanceAlternateText
        {
            get
            {
                return GetResource("FindStationResultsTable.commandSortByDistance.AlternateText");
            }
        }

        public string SortByTransportUrl
        {
            get
            {
                return GetResource("FindStationResultsTable.commandSortByTransport.ImageUrl");
            }
        }

        public string SortByTransportAlternateText
        {
            get
            {
                return GetResource("FindStationResultsTable.commandSortByTransport.AlternateText");
            }
        }

        public string OptionSortSymbolUrl
        {
            get
            {
                return stationPageState.IsOptionSortingAsc ?
                sortAscUrl : sortDescUrl;
            }
        }

        public string OptionSortSymbolAlternateText
        {
            get
            {
                return stationPageState.IsOptionSortingAsc ?
                sortAscAlternateText : sortDescAlternateText;

            }
        }
        public string AirportNameSortSymbolUrl
        {
            get
            {
                return stationPageState.IsStationSortingAsc ?
                sortAscUrl : sortDescUrl;
            }
        }

        public string AirportNameSortSymbolAlternateText
        {
            get
            {
                return stationPageState.IsStationSortingAsc ?
                sortAscAlternateText : sortDescAlternateText;

            }
        }

        public string DistanceSortSymbolUrl
        {
            get
            {
                return stationPageState.IsDistanceSortingAsc ?
                sortAscUrl : sortDescUrl;
            }
        }

        public string DistanceSortSymbolAlternateText
        {
            get
            {
                return stationPageState.IsDistanceSortingAsc ?
                sortAscAlternateText : sortDescAlternateText;
            }
        }

        public string TransportSortSymbolUrl
        {
            get
            {
                return stationPageState.IsTransportSortingAsc ?
                sortAscUrl : sortDescUrl;
            }
        }

        public string TransportSortSymbolAlternateText
        {
            get
            {
                return stationPageState.IsTransportSortingAsc ?
                sortAscAlternateText : sortDescAlternateText;
            }
        }

        public string InfoText
        {
            get
            {
                return GetResource("FindStationResultsTable.commandInfo.Text");
            }
        }

        public string InfoToolTip
        {
            get
            {
                return GetResource("FindStationResultsTable.commandInfo.ToolTip");
            }
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

        ///		Required method for Designer support - do not modify
        ///		the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
        }
        #endregion

        #region Event handlers
        /// <summary>
        /// Event triggered by click on Info button of one row. Use command Argument of imagebutton sender
        /// to call the location information page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void CommandInfoClick(object sender, EventArgs e)
        {
            //ImageButton infoButton = (ImageButton)sender;
            if (!boolPrintablePage)
            {
                TDButton infoButton = (TDButton)sender;

                string naptan = infoButton.CommandArgument;

                inputPageState.AdditionalDataLocation = naptan;

                SetStopInformation(naptan);

                inputPageState.JourneyInputReturnStack.Clear();
                inputPageState.JourneyInputReturnStack.Push(((TDPage)Page).PageId);

                // triggers InformationRequestedEvent, so the map is stored 
                if (InformationRequested != null)
                    InformationRequested(this, new EventArgs());

                TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.FindStationResultsInfo;
            }
        }

        /// <summary>
        /// when AirportName sorting button clicked.
        ///		reverse sorting (asc/desc)
        ///		
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void CommandSortByAirportNameClick(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            if (!boolPrintablePage)
            {
                if (stationPageState.CurrentSortingType != FindStationPageState.SortingType.StationName)
                {
                    stationPageState.CurrentSortingType = FindStationPageState.SortingType.StationName;

                    stationPageState.IsDistanceSortingAsc = false;
                    stationPageState.IsOptionSortingAsc = false;
                    stationPageState.IsStationSortingAsc = true;
                    stationPageState.IsTransportSortingAsc = false;
                }
                else
                    stationPageState.IsStationSortingAsc = !stationPageState.IsStationSortingAsc;

                Refresh();
            }
        }

        /// <summary>
        /// when Transport sorting button clicked.
        ///		reverse sorting (asc/desc)
        ///		
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void CommandSortByTransportClick(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            if (!boolPrintablePage)
            {
                if (stationPageState.CurrentSortingType != FindStationPageState.SortingType.Transport)
                {
                    stationPageState.CurrentSortingType = FindStationPageState.SortingType.Transport;

                    stationPageState.IsDistanceSortingAsc = false;
                    stationPageState.IsOptionSortingAsc = false;
                    stationPageState.IsStationSortingAsc = false;
                    stationPageState.IsTransportSortingAsc = true;
                }
                else
                    stationPageState.IsTransportSortingAsc = !stationPageState.IsTransportSortingAsc;

                Refresh();
            }

        }

        /// <summary>
        /// when Distance sorting button clicked.
        ///		reverse sorting (asc/desc)
        ///		
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void CommandSortByDistanceClick(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            if (!boolPrintablePage)
            {
                if (stationPageState.CurrentSortingType != FindStationPageState.SortingType.Distance)
                {
                    stationPageState.CurrentSortingType = FindStationPageState.SortingType.Distance;

                    stationPageState.IsDistanceSortingAsc = true;
                    stationPageState.IsOptionSortingAsc = false;
                    stationPageState.IsStationSortingAsc = false;
                    stationPageState.IsTransportSortingAsc = false;
                }
                else
                    stationPageState.IsDistanceSortingAsc = !stationPageState.IsDistanceSortingAsc;

                Refresh();
            }

        }

        /// <summary>
        /// when Option sorting button clicked.
        ///		reverse sorting (asc/desc)
        ///		
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void CommandSortByOptionClick(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            if (!boolPrintablePage)
            {
                if (stationPageState.CurrentSortingType != FindStationPageState.SortingType.Option)
                {
                    stationPageState.CurrentSortingType = FindStationPageState.SortingType.Option;

                    stationPageState.IsDistanceSortingAsc = false;
                    stationPageState.IsOptionSortingAsc = true;
                    stationPageState.IsStationSortingAsc = false;
                    stationPageState.IsTransportSortingAsc = false;
                }
                else
                    stationPageState.IsOptionSortingAsc = !stationPageState.IsOptionSortingAsc;

                Refresh();
            }

        }

        public void AirportCheckedChanged(object sender, EventArgs e)
        {
            //CheckBox check = (CheckBox) sender;

            StoreAirportSelection();
            Refresh();
        }

        /// <summary>
        /// Method called after the OnCheckedChanged event handler is fired
        /// for the Tick All ScriptableCheckBox.
        /// Used to set the correct "checked" property after postback.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void TickAllCheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBoxChanged = (CheckBox)sender;
            stationPageState.IsCheckedAll = checkBoxChanged.Checked;
        }

        #endregion

        protected override void OnPreRender(EventArgs e)
        {
            stationPageState.IsShowingHidingMap = false;
            base.OnPreRender(e);
        }

        /// <summary>
        /// Seting the naptan information for Stop Information page
        /// </summary>
        /// <param name="naptan"></param>
        private void SetStopInformation(string naptan)
        {
            InputPageState inputPageState = TDSessionManager.Current.InputPageState;

            inputPageState.StopCode = naptan;
            inputPageState.StopCodeType = TDCodeType.NAPTAN;
            inputPageState.ShowStopInformationPlanJourneyControl = false;
        }





	}

}
