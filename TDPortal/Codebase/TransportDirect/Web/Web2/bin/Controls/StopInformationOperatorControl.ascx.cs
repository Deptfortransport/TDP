// *********************************************** 
// NAME                 : StopInformationOperatorControl.ascx.cs
// AUTHOR               : Amit Patel
// DATE CREATED         : 10/09/2009 
// DESCRIPTION  		: Stop information operator control
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/StopInformationOperatorControl.ascx.cs-arc  $
//
//   Rev 1.2   Sep 30 2009 09:09:08   apatel
//Stop Information code changes
//Resolution for 5315: CCN526 Stop Information Page Landing
//
//   Rev 1.1   Sep 14 2009 15:15:44   apatel
//updated header logging
//Resolution for 5315: CCN526 Stop Information Page Landing

using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.AirDataProvider;

namespace TransportDirect.UserPortal.Web.Controls
{
    /// <summary>
    /// Stop information operator control provides information of the operator
    /// </summary>
    public partial class StopInformationOperatorControl : TDUserControl
    {
        #region Private Fields
        private bool isEmpty = true;
        private TDStopType stopType;
        private string iataCode = string.Empty;
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

        #region Public Methods
        /// <summary>
        /// Initialises the control
        /// </summary>
        /// <param name="stopType"></param>
        /// <param name="iataCode"></param>
        public void Initialise(TDStopType stopType, string naptan, string iataCode)
        {
            this.stopType = stopType;
            this.iataCode = iataCode;

            labelOperator.Text = GetResource("LocationInformation.labelSummaryTitle_Operators.Text");

            if (!string.IsNullOrEmpty(iataCode))
            {
                ZonalAirportOperatorControl1.AirportNaptan = naptan.Substring(0, Airport.NaptanPrefix.Length + 3);;

                isEmpty = ZonalAirportOperatorControl1.IsEmpty;
            }
        }
        #endregion
    }
}