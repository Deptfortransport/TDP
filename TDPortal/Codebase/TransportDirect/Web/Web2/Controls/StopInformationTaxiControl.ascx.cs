// *********************************************** 
// NAME                 : StopInformationTaxiControl.ascx.cs
// AUTHOR               : Amit Patel
// DATE CREATED         : 10/09/2009 
// DESCRIPTION  		: Stop information taxi information control
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/StopInformationTaxiControl.ascx.cs-arc  $
//
//   Rev 1.1   Sep 14 2009 15:15:52   apatel
//updated header logging
//Resolution for 5315: CCN526 Stop Information Page Landing

using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TransportDirect.UserPortal.AdditionalDataModule;

namespace TransportDirect.UserPortal.Web.Controls
{
    /// <summary>
    /// Taxi information control for stop
    /// </summary>
    public partial class StopInformationTaxiControl : TDUserControl
    {
        #region Private Fields
        private bool isEmpty = true;
        private StopTaxiInformation selectedStop;
        #endregion

        #region Public Properties
        /// <summary>
        /// Read Only property true if the control got no details
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                return isEmpty;
            }
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
            TaxiInformationControl1.Data = selectedStop;

            labelSummaryTitle.Text = GetResource("StopInformationTaxiControl.labelSummaryTitle.Text");

            // Updatng Image url path & AlternateText
            imageTaxi.ImageUrl = GetResource("StopInformationTaxiControl.imageTaxi.ImageUrl");
            imageTaxi.AlternateText = GetResource("StopInformationTaxiControl.imageTaxi.AlternateText");
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Initialises the control
        /// </summary>
        /// <param name="naptan"></param>
        public void Initialise(string naptan)
        {
            selectedStop = new StopTaxiInformation(naptan, true);

            isEmpty = !selectedStop.InformationAvailable;
                
        }

        #endregion
    }
}