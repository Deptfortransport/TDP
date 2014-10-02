// *********************************************** 
// NAME                 :WalkitLinkControl.ascx.cs
// AUTHOR               : Amit Patel
// DATE CREATED         : 16/09/2009 
// DESCRIPTION  		: Walkit link user control to display walkit.com handoff url
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/WalkitLinkControl.ascx.cs-arc  $
//
//   Rev 1.12   Sep 14 2010 09:49:24   apatel
//Updated to correct the walkit link for return journey
//Resolution for 5603: Return Journey - Walkit Links Wrong
//
//   Rev 1.11   Mar 12 2010 09:12:56   apatel
//Refactor Walkit control to put most of the walkit logic in to helper class
//Resolution for 5444: CCN 0568 Walkit page land - Phase 1b
//
//   Rev 1.10   Mar 10 2010 15:19:16   apatel
//Updated to show Walkit links on map information popup window when user clicks on start location of walk leg
//Resolution for 5444: CCN 0568 Walkit page land - Phase 1b
//
//   Rev 1.9   Jan 07 2010 13:39:50   apatel
//Made control to show label instead of link for printer friendly
//Resolution for 5357: Printer friendly page issue of header and walkit links
//
//   Rev 1.8   Jan 05 2010 13:40:50   apatel
//Resolve the issue with the location with search type at the start or end walk leg having incorrect coordinates(of Main Rail/Coach station).
//Resolution for 5353: Walkit Link issues
//
//   Rev 1.7   Dec 14 2009 16:00:24   pghumra
//Added comments
//Resolution for 5334: CCN0538 Page Land on Walkit.com
//
//   Rev 1.6   Dec 09 2009 16:45:46   pghumra
//Added conditions to ensure WalkIt link is not generated if walk data is not available
//Resolution for 5334: CCN0538 Page Land on Walkit.com
//
//   Rev 1.5   Dec 09 2009 07:45:20   apatel
//updated code to add property for min journey duration at start and end leg
//Resolution for 5334: CCN0538 Page Land on Walkit.com
//
//   Rev 1.4   Dec 08 2009 15:59:22   apatel
//Walkit link code
//Resolution for 5334: CCN0538 Page Land on Walkit.com
//
//   Rev 1.3   Dec 04 2009 11:17:24   apatel
//Walkit control update to put walkit logo
//Resolution for 5334: CCN0538 Page Land on Walkit.com
//
//   Rev 1.2   Dec 04 2009 09:20:42   pghumra
//walkit control files
//Resolution for 5334: CCN0538 Page Land on Walkit.com
//
//   Rev 1.1   Nov 23 2009 10:32:40   mmodi
//Check for null request - to fix error for extended journeys
//
//   Rev 1.0   Nov 11 2009 16:50:44   pghumra
//Initial revision.
//Resolution for 5334: CCN0538 Page Land on Walkit.com

using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using TransportDirect.UserPortal.Web.Adapters;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.CoordinateConvertorProvider;
using TransportDirect.UserPortal.JourneyControl;
using CCP = TransportDirect.UserPortal.CoordinateConvertorProvider.CoordinateConvertorWebService;
using Logger = System.Diagnostics.Trace;
using TransportDirect.Common.Logging;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.JourneyPlanning.CJPInterface;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Presentation.InteractiveMapping;
using TransportDirect.UserPortal.SessionManager;
using System.Web.UI.HtmlControls;

namespace TransportDirect.UserPortal.Web.Controls
{
    /// <summary>
    /// Walkit link control class
    /// </summary>
    public partial class WalkitLinkControl : TDPrintableUserControl
    {
        #region Private Fields
        private bool walkitLinkAvailable = false;
        private string walkitUrl = string.Empty;
        private string walkitText = string.Empty;
        #endregion

        #region Public Properties
        /// <summary>
        /// Read only property confirming whether a walkit link available or not for a walk leg of journey
        /// </summary>
        public bool IsWalkitLinkAvailable
        {
            get
            {
                return walkitLinkAvailable;
            }
        }

        /// <summary>
        /// Read only property giving access to walkit url
        /// </summary>
        public string WalkitUrl
        {
            get
            {
                return walkitUrl;
            }
        }

        /// <summary>
        /// Read only property. Gives access to walking control div container
        /// </summary>
        public HtmlControl WalkitContainerControl
        {
            get
            {
                return walkitLinkContainer;
            }
        }

        /// <summary>
        /// Read/write. Specifies the custom text to show for the walkit link
        /// </summary>
        public string WalkitLinkText
        {
            get
            {
                return walkitText;
            }
            set
            {
                walkitText = value;
            }
        }

        #endregion

        #region Page Events
        /// <summary>
        /// Page Load Event
        /// </summary>
        /// <param name="sender">originator of event</param>
        /// <param name="e">event parameters</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            walkitLink.Text = string.Format("{0} {1}", GetResource("WalkitLinkControl.walkitLink.Text"), GetResource("ExternalLinks.OpensNewWindowText"));
            walkitLinkLabel.Text = GetResource("WalkitLinkControl.walkitLink.Text");
        }

        /// <summary>
        /// Page PreRender Event
        /// </summary>
        /// <param name="sender">originator of event</param>
        /// <param name="e">event parameters</param>
        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(WalkitLinkText))
            {
                walkitLinkLabel.Text = WalkitLinkText;
                walkitLink.Text = WalkitLinkText;
            }

            walkitLinkLabel.Visible = PrinterFriendly;
            walkitLink.Visible = !this.PrinterFriendly;
        }
        #endregion


        #region Public Methods
        /// <summary>
        /// Initilises the walk it link control
        /// </summary>
        /// <param name="journey">Journey </param>
        /// <param name="journeyLeg">Walk leg of the journey</param>
        /// <param name="journeyIndex">Index of walk leg of the journey</param>
        public void Initialise(Journey journey,JourneyLeg journeyLeg,int journeyIndex, ITDJourneyRequest jr, bool outward)
        {
            
            
            WalkitURLHandoffHelper walkitURLHelper = new WalkitURLHandoffHelper(journey, journeyLeg, journeyIndex, jr, outward);

            walkitLink.NavigateUrl = walkitURLHelper.GetWalkitHandoffURL();


            if (!string.IsNullOrEmpty(walkitLink.NavigateUrl))
            {
                walkitLinkAvailable = true;
                walkitUrl = walkitLink.NavigateUrl;
            }
               
        }
        #endregion

        

    }
}