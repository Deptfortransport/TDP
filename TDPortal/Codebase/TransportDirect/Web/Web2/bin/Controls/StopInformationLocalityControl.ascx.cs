// *********************************************** 
// NAME                 : StopInformationLocalityControl.ascx.cs
// AUTHOR               : Amit Patel
// DATE CREATED         : 10/09/2009 
// DESCRIPTION  		: Stop information locality control
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/StopInformationLocalityControl.ascx.cs-arc  $
//
//   Rev 1.1   Sep 14 2009 15:15:40   apatel
//updated header logging
//Resolution for 5315: CCN526 Stop Information Page Landing

using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TransportDirect.UserPortal.Web.Controls
{
    /// <summary>
    /// Stop information locality information control
    /// </summary>
    public partial class StopInformationLocalityControl : TDUserControl
    {
        #region Private Fields
        private bool isEmpty = true;
        private string naptan = string.Empty;
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
        /// initializes the control
        /// </summary>
        /// <param name="naptan"></param>
        public void initialise(string naptan)
        {
            this.naptan = naptan;

            localInformation.Text = GetResource("LocationInformation.localInformation.Text");

            ZonalServiceLinksControl1.Naptan = naptan;
            ZonalAccessibilityLinksControl1.AccessibilityByNaptan = false;
            ZonalAccessibilityLinksControl1.Naptan = naptan;

            if (!ZonalServiceLinksControl1.IsEmpty || !ZonalAccessibilityLinksControl1.IsEmpty)
            {
                isEmpty = false;
            }
        }
        #endregion
    }
}