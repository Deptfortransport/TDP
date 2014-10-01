﻿// *********************************************** 
// NAME             : VenueSelectControl.ascx.cs      
// AUTHOR           : Mark Danforth
// DATE CREATED     : ?? ??? 2012
// DESCRIPTION  	: VenueSelectControl
// ************************************************
// 
                
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using TDP.Common.Extenders;
using TDP.Common.LocationService;
using TDP.Common.PropertyManager;
using TDP.Common.Web;

namespace TDP.UserPortal.TDPMobile.Controls
{
    /// <summary>
    /// VenueSelectControl
    /// </summary>
    public partial class VenueSelectControl : System.Web.UI.UserControl
    {
        #region Page_Load

        /// <summary>
        /// Page_Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// Page_Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_PreRender(object sender, EventArgs e)
        {
            SetupResources();

            allVenuesDiv.Visible = allVenuesButton.Visible;
        }

        #endregion

        #region Public properties

        /// <summary>
        /// List containing venue locations which can be selected
        /// </summary>
        public GroupRadioButtonList List
        {
            get { return locationSelector; }
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Populates the venue location selector list
        /// </summary>
        /// <param name="locationService"></param>
        public void Populate(LocationService locationService)
        {
            if (locationService != null)
            {
                List<TDPLocation> venues = locationService.GetTDPVenueLocations();

                locationSelector.Items.Clear();

                if (Properties.Current["LocationControl.VenueGrouping.Switch"].Parse(true))
                {
                    Dictionary<string, List<TDPVenueLocation>> groupedVenues = new Dictionary<string, List<TDPVenueLocation>>();

                    // To group items for which there is no group specified.
                    // These items will be shown last in the list
                    string defaultGroupName = "Other";
                    List<TDPVenueLocation> defaultGroupVenues = new List<TDPVenueLocation>();
                    
                    foreach (TDPLocation loc in venues)
                    {
                        TDPVenueLocation venue = (TDPVenueLocation)loc;

                        if (string.IsNullOrEmpty(venue.VenueGroupName) && !string.IsNullOrEmpty(venue.Parent))
                        {
                            // If the group name is empty but parent is not empty get the group name from the parent

                            TDPLocation parentVenue = venues.SingleOrDefault(v => v.ID == venue.Parent);

                            if (parentVenue != null)
                            {
                                TDPVenueLocation pvl = (TDPVenueLocation)parentVenue;
                                if (!groupedVenues.Keys.Contains(pvl.VenueGroupName))
                                {
                                    groupedVenues.Add(pvl.VenueGroupName, new List<TDPVenueLocation>());
                                }
                                groupedVenues[pvl.VenueGroupName].Add(venue);
                            }
                            else // no suitable parent found add it as no group
                            {
                                defaultGroupVenues.Add(venue);
                            }
                        }
                        else if (!string.IsNullOrEmpty(venue.VenueGroupName))
                        {
                            // group name specified add it will group name
                            if (!groupedVenues.Keys.Contains(venue.VenueGroupName))
                            {
                                groupedVenues.Add(venue.VenueGroupName, new List<TDPVenueLocation>());
                            }
                            groupedVenues[venue.VenueGroupName].Add(venue);
                        }
                        else // No grouping specified add it as no group
                        {
                            defaultGroupVenues.Add(venue);
                        }
                    }

                    // Add the default venues last
                    groupedVenues.Add(defaultGroupName, defaultGroupVenues);

                    foreach (string venueGroupName in groupedVenues.Keys)
                    {
                        foreach (TDPVenueLocation vl in groupedVenues[venueGroupName].OrderBy(v => v.DisplayName))
                        {
                            ListItem vItem = new ListItem();
                            vItem.Text = vl.DisplayName;
                            vItem.Value = vl.ID;

                            vItem.Attributes.Add("OptionGroup", venueGroupName);

                            locationSelector.Items.Add(vItem);
                        }
                    }
                }
                else
                {
                    locationSelector.DataSource = venues;
                    locationSelector.DataTextField = "DisplayName";
                    locationSelector.DataValueField = "Id";
                    locationSelector.DataBind();
                }
            }
        }

        public void Populate(LocationService locationService, bool allVenuesButtonVisible)
        {
            allVenuesButton.Visible = allVenuesButtonVisible;
            Populate(locationService);
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Sets up resource content
        /// </summary>
        private void SetupResources()
        {
            TDPPageMobile currentPage = (TDPPageMobile)Page;

            venuesSelectorLabel.InnerText = currentPage.GetResourceMobile("JourneyInput.VenuePage.SelectVenue.Text");
            closevenues.Text = currentPage.GetResourceMobile("JourneyInput.Back.Text");
            allVenuesButton.Text = currentPage.GetResourceMobile("JourneyInput.SelectVenue.AllVenuesButton.Text");
        }

        #endregion
    }
}