// *********************************************** 
// NAME                 : StopInformationMapControl.ascx.cs
// AUTHOR               : Amit Patel
// DATE CREATED         : 10/09/2009 
// DESCRIPTION  		: Stop information map control
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/StopInformationMapControl.ascx.cs-arc  $
//
//   Rev 1.6   Jan 19 2010 13:20:26   mmodi
//Updates for New Mapping RFC73 patch
//Resolution for 5376: Maps - ESRI RFC073 mandatory changes needed for patch
//
//   Rev 1.5   Dec 08 2009 11:27:48   mmodi
//Added flag to set visibility of Expand map button
//
//   Rev 1.4   Nov 11 2009 16:51:48   apatel
//Mapping enhancements
//Resolution for 5331: CCN0533  Mapping Enhancements - AJAX Maps
//
//   Rev 1.3   Oct 30 2009 11:50:52   apatel
//Stop information changes
//Resolution for 5315: CCN526 Stop Information Page Landing
//
//   Rev 1.2   Sep 30 2009 09:09:06   apatel
//Stop Information code changes
//Resolution for 5315: CCN526 Stop Information Page Landing
//
//   Rev 1.1   Sep 14 2009 15:15:42   apatel
//updated header logging
//Resolution for 5315: CCN526 Stop Information Page Landing

using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TransportDirect.UserPortal.LocationService;
using System.Collections;
using TransportDirect.Presentation.InteractiveMapping;
using Logger = System.Diagnostics.Trace;
using TransportDirect.Common.Logging;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.Resource;
using TransportDirect.UserPortal.Web.Adapters;

namespace TransportDirect.UserPortal.Web.Controls
{
    /// <summary>
    /// Stop information map control
    /// </summary>
    public partial class StopInformationMapControl : TDUserControl
    {
        #region Private Fields
        private bool isEmpty = true;
        private bool showExpandButton = true;
        private TDStopType stopType;
        private OSGridReference coordinates;
        private string stopName = string.Empty;
        #endregion

        #region Public Properties
        /// <summary>
        /// Read only property specifying if the control got any data to display
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                return isEmpty;
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

        /// <summary>
        ///	Required method for Designer support - do not modify
        ///	the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            ExpandMap.Click += new EventHandler(ExpandMap_Click);
            
        }

        #endregion

        #region Page Events
        /// <summary>
        /// Page load event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            ExpandMap.Text = GetResource("StopInformation.ExpandButton.Text");

            StopInformationMapTitle.Text = GetResource("StopInformationMapTitle.Text");

            int mapScale = GetMapScale(stopType);

            MapLocationPoint mapLocationPoint = new MapLocationPoint(coordinates, MapLocationSymbolType.Circle, stopName, false, false);

            theMap.Initialise(mapScale, mapLocationPoint);

        }

        /// <summary>
        /// Page PreRender event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_PreRender(object sender, EventArgs e)
        {
            panelExpandMapButton.Visible = showExpandButton;
        }

        #endregion

        #region Public Methods
        /// <summary>
        /// Initialises the control
        /// </summary>
        /// <param name="stopType"></param>
        /// <param name="coordinates"></param>
        /// <param name="stopName"></param>
        public void Initialise(TDStopType stopType, OSGridReference coordinates, string stopName, bool showExpandButton)
        {
            this.stopType = stopType;
            this.coordinates = coordinates;
            this.stopName = stopName;
            this.showExpandButton = showExpandButton;

            if (!((TDPage)Page).IsJavascriptEnabled)
            {
                isEmpty = false;
            }

        }
        #endregion

        #region Control Events
        /// <summary>
        /// Expand button click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ExpandMap_Click(object sender, EventArgs e)
        {
            TDSessionManager.Current.IsStopInformationMode = true;

            // Set page id in stack so we know where to come back to
            TDSessionManager.Current.InputPageState.JourneyInputReturnStack.Push(PageId);

            // Navigate to the Journey Accessibility page
            TDSessionManager.Current.FormShift[SessionKey.TransitionEvent] = TransitionEvent.FindMapResult;
        }
        #endregion

        #region Private Methods

              
        /// <summary>
        /// Gets the scale of the map to show depending on the stop type
        /// </summary>
        /// <param name="stopType"></param>
        /// <returns></returns>
        private int GetMapScale(TDStopType stopType)
        {
            int scale = 8000;

            switch (stopType)
            {
                case TDStopType.Air:
                    int.TryParse(Properties.Current["StopTypeDefaultScale.Air"], out scale);
                    break;

                case TDStopType.Bus:
                    int.TryParse(Properties.Current["StopTypeDefaultScale.Bus"], out scale);
                    break;

                case TDStopType.Coach:
                    int.TryParse(Properties.Current["StopTypeDefaultScale.Coach"], out scale);
                    break;

                case TDStopType.Ferry:
                    int.TryParse(Properties.Current["StopTypeDefaultScale.Ferry"], out scale);
                    break;

                case TDStopType.LightRail:
                    int.TryParse(Properties.Current["StopTypeDefaultScale.LightRail"], out scale);
                    break;

                case TDStopType.Rail:
                    int.TryParse(Properties.Current["StopTypeDefaultScale.Rail"], out scale);
                    break;

                case TDStopType.Taxi:
                    int.TryParse(Properties.Current["StopTypeDefaultScale.Taxi"], out scale);
                    break;

                default:
                    int.TryParse(Properties.Current["StopTypeDefaultScale"], out scale);
                    break;

            }

            return scale;
        }
        #endregion
    }
}