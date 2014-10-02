// *********************************************** 
// NAME                 : ServiceNotesControl.cs
// AUTHOR               : Richard Philpott
// DATE CREATED         : 2005-07-18
// DESCRIPTION			: Control to display formatted notes
//                        about a public transport schedule  
// ************************************************
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/ServiceNotesControl.ascx.cs-arc  $
//
//   Rev 1.3   Mar 21 2013 10:13:20   mmodi
//Updates for journey display notes filtering
//Resolution for 5905: CCN:677a - Accessible Journeys Planner Phase 2
//
//   Rev 1.2   Mar 31 2008 13:23:02   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:17:52   mturner
//Initial revision.
//
//   Rev 1.4   Feb 23 2006 19:17:08   build
//Automatically merged from branch for stream3129
//
//   Rev 1.3.1.0   Jan 10 2006 15:27:26   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.3   Aug 16 2005 17:52:48   RPhilpott
//FxCop and code review fixes.
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.2   Jul 25 2005 18:14:26   RPhilpott
//Updates during testing.
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.1   Jul 22 2005 20:01:04   RPhilpott
//Development ofd ServiceDetails page.
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.0   Jul 19 2005 10:08:08   RPhilpott
//Initial revision.
//

using System;using TransportDirect.Common.ResourceManager;
using System.Data;
using System.Text;
using System.Drawing;
using System.Collections;

using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

using TransportDirect.UserPortal.Web;
using TransportDirect.UserPortal.Web.Adapters;
using TransportDirect.UserPortal.JourneyControl;


namespace TransportDirect.UserPortal.Web.Controls
{

	/// <summary>
	///	Control to display formatted notes
	//  about a public transport schedule. 
	/// </summary>
	public partial class ServiceNotesControl : TDPrintableUserControl
	{

		private int[] features = new int[0];
        private PublicJourney journey = null;
        private PublicJourneyDetail journeyDetail = null;
		private ArrayList vehicleFeatureIcons = new ArrayList();

		protected RailVehicleFeaturesIconMapper railVehicleFeaturesIconMapper;

		protected void Page_Load(object sender, System.EventArgs e)
		{
		}

		/// <summary>
		/// Constructor
		/// </summary>
		public ServiceNotesControl()
		{
			LocalResourceManager = TDResourceManager.JOURNEY_RESULTS_RM;
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

		/// <summary>
		/// Event handler for the prerender event
		/// </summary>
		/// <param name="e"></param>
		protected override void OnPreRender( EventArgs e )
		{
			if	(features.Length > 0) 
			{
				railVehicleFeaturesIconMapper = new RailVehicleFeaturesIconMapper();
				vehicleFeatureIcons = railVehicleFeaturesIconMapper.GetIcons(features);
			}

			if	(vehicleFeatureIcons == null || vehicleFeatureIcons.Count == 0)
			{
				vehicleFeaturesRepeater.Visible = false;
			}
			else
			{
				vehicleFeaturesRepeater.DataSource = vehicleFeatureIcons;
				vehicleFeaturesRepeater.DataBind();
				vehicleFeaturesRepeater.Visible = true;
			}

            notesLabel.Text = GetDisplayNotes();
            notesLabel.Visible = !string.IsNullOrEmpty(notesLabel.Text);

			if	(!vehicleFeaturesRepeater.Visible && !notesLabel.Visible)
			{
				headingLabel.Visible = false;
				this.Visible = false;
			}
			else
			{
				headingLabel.Text = GetResource("ServiceDetails.Notes.Heading");
			}
 			
 		}

	
		/// <summary>
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{

		}
		#endregion

		#region Properties
		
		/// <summary>
		/// Gets/Sets vehicle features to be displayed.
		/// </summary>
		public int[] Features
		{
			get { return features; }
			set { features = value; }
		}

        /// <summary>
        /// Gets/Sets the journey 
        /// </summary>
        public PublicJourney Journey
        {
            get { return journey; }
            set { journey = value; }
        }

		/// <summary>
		/// Gets/Sets the journey details containing the notes to be displayed.
		/// </summary>
        public PublicJourneyDetail JourneyDetail
        {
            get { return journeyDetail; }
            set { journeyDetail = value; }
        }

		/// <summary>
		/// Gets the image URL of specified vehicle feature.
		/// </summary>
		/// <param name="vehicleFeatures"></param>
		/// <returns></returns>
		public string GetImageURL(VehicleFeatureIcon vehicleFeatures)
		{
			return GetResource(vehicleFeatures.ImageUrlResource);
		}

		/// <summary>
		/// Gets the alternate text for the specified vehicle feature.
		/// </summary>
		/// <param name="vehicleFeatures"></param>
		/// <returns></returns>
		public string GetAltText(VehicleFeatureIcon vehicleFeatures)
		{
			return GetResource(vehicleFeatures.AltTextResource);
		}

		/// <summary>
		/// Gets the alternate text (tooltip) for the specified vehicle feature.
		/// </summary>
		/// <param name="vehicleFeatures"></param>
		/// <returns></returns>
		public string GetToolTip(VehicleFeatureIcon vehicleFeatures)
		{
			return GetResource(vehicleFeatures.ToolTipResource);
		}

		#endregion

        #region Private methods

        /// <summary>
        /// Get the display notes, formatted by the appropriate adapter.
        /// </summary>
        /// <returns></returns>
        private string GetDisplayNotes()
        {
            NotesDisplayAdapter notesDisplayAdapter = new NotesDisplayAdapter();

            return notesDisplayAdapter.GetDisplayableNotes(journey, journeyDetail);
        }

        #endregion
    }
}
