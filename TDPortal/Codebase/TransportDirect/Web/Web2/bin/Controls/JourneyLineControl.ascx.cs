// *********************************************** 
// NAME                 : JourneyLineControl.ascx.cs 
// AUTHOR               : Tolu Olomolaiye 
// DATE CREATED         : 25/08/2005
// DESCRIPTION			: Displays a schematic representation of a Visit Planner journey
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/JourneyLineControl.ascx.cs-arc  $
//
//   Rev 1.2   Mar 31 2008 13:21:34   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:15:40   mturner
//Initial revision.
//
//   Rev 1.7   Feb 23 2006 19:16:50   build
//Automatically merged from branch for stream3129
//
//   Rev 1.6.1.0   Jan 10 2006 15:25:46   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.6   Oct 29 2005 15:42:56   jbroome
//Fixed allignment of cells
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.5   Oct 28 2005 14:59:18   tolomolaiye
//Changes from code review and running fxcop
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.4   Oct 04 2005 15:24:32   jbroome
//Re-factored control
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.3   Sep 16 2005 16:12:24   tolomolaiye
//Check in for review
//Resolution for 2638: DEL 8 Stream: Visit Planner
//
//   Rev 1.2   Sep 14 2005 11:25:30   tolomolaiye
//Work in progress
//
//   Rev 1.1   Sep 05 2005 17:51:30   tolomolaiye
//Check-in for review
//
//   Rev 1.0   Sep 02 2005 10:52:32   tolomolaiye
//Initial revision.
//Resolution for 2638: DEL 8 Stream: Visit Planner

namespace TransportDirect.UserPortal.Web.Controls
{
	using System;using TransportDirect.Common.ResourceManager;
	using System.Web.UI;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using System.Collections;

	using TransportDirect.UserPortal.LocationService;

	/// <summary>
	///	Displays a schematic representation of a Visit Planner journey
	/// </summary>
	public partial class JourneyLineControl : TDPrintableUserControl
	{

		// Private instance variables
		private TDLocation[] arrLocations;
		private bool backToOrigin;

		/// <summary>
		/// Constructor sets local resource manager
		/// </summary>
		public JourneyLineControl()
		{
			this.LocalResourceManager = TDResourceManager.VISIT_PLANNER_RM;
		}

		/// <summary>
		/// Default Page_Load
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
		}

		/// <summary>
		/// Event handler for the prerender event
		/// Sets the datasource for repeaters and databinds
		/// </summary>
		/// <param name="e"></param>
		protected override void OnPreRender( EventArgs e )
		{
			// Check if need to copy first location
			// to end of array
			if (backToOrigin)
			{
				ArrayList copyLocations = new ArrayList();
				// Add the current array
				copyLocations.AddRange(arrLocations);
				// Add the first location again
				copyLocations.Add(arrLocations[0]);
				// Update the original array
				arrLocations = (TDLocation[])copyLocations.ToArray(typeof(TDLocation));
			}
			
			// Set data source of repeaters 
			locations.DataSource = arrLocations;
			ballAndLine.DataSource = arrLocations;

			// Data bind repeaters
			locations.DataBind();
			ballAndLine.DataBind();		
		}

		#region Public Properties

		/// <summary>
		/// Gets/sets the datasource for the repeater controls 
		/// </summary>
		public TDLocation[] DataSource
		{
			get { return arrLocations; }
			set { arrLocations = value; }
		}

		/// <summary>
		/// Read/write property
		/// Determine whether another location 
		/// should be added to create another journey
		/// </summary>
		public bool ReturnToOrigin
		{
			get { return backToOrigin; }
			set { backToOrigin = value; }
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
			this.ballAndLine.ItemDataBound += new System.Web.UI.WebControls.RepeaterItemEventHandler(this.ballAndLine_ItemDataBound);
		}
		#endregion

		#region  Repeater events

		/// <summary>
		/// Method returns the description 
		/// property of a TDLocation for
		/// display in a repeated template
		/// </summary>
		/// <param name="item">int item index</param>
		/// <returns>description string</returns>
		protected string GetDescription(object item)
		{
			return ((TDLocation)item).Description ;
		}

		/// <summary>
		/// Method returns string for cell
		/// allignment according to position 
		/// within row
		/// </summary>
		/// <param name="index">int item index</param>
		/// <returns>allignment string</returns>
		protected string GetAlignment(int index)
		{
			int lastIndex = arrLocations.Length -1;
			string alignment = string.Empty;

			if (index == 0)
			{
				alignment = "left";
			}
			else if (index == lastIndex)
			{
				alignment = "right";
			}
			else 
			{
				alignment = "center";
			}
				

			return alignment;
		}
	
		/// <summary>
		/// Returns string CSS class according
		/// to cell position
		/// </summary>
		/// <param name="index"></param>
		/// <returns></returns>
		protected string GetClass(int index)
		{
			int lastIndex = arrLocations.Length -1;
			string cssclass = string.Empty;
			
			if ((index == 0) || (index == lastIndex))
			{
				cssclass = "journeylineend";
			}
			else
			{
				cssclass = "journeylinemiddle";
			}

			return cssclass;
		}


		/// <summary>
		/// Event handler for the ballAndLine Repeater
		/// Sets the correct images and text in the correct
		/// table cells
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void ballAndLine_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
		{
			if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
			{
				int lastIndex = arrLocations.Length - 1;
				int currentIndex = e.Item.ItemIndex ;

				//bool firstLocation = (e.Item.ItemIndex == 0);
				//bool lastLocation = (e.Item.ItemIndex == (arrLocations.Length - 1));
				Image imageBall, imageHidden1, imageHidden2;

				if (currentIndex == 0)
				{
					//first location. Ball image is in left-most column
					imageBall = (Image)e.Item.FindControl("image1");
					imageBall.AlternateText = GetResource("JourneyLineControl.imageBall_StartingLocation.AlternateText");
					imageBall.ImageAlign = ImageAlign.Left;

					// Obtain unused images to hide
					imageHidden1 = (Image)e.Item.FindControl("image2");
					imageHidden2 = (Image)e.Item.FindControl("image3");
				}
				else if (currentIndex == lastIndex)
				{
					//last location. Ball image is in right-most column
					imageBall = (Image)e.Item.FindControl("image3");
					imageBall.AlternateText = GetResource("JourneyLineControl.imageBall_FinalDestination.AlternateText");
					imageBall.ImageAlign = ImageAlign.Right;

					// Obtain unused images to hide
					imageHidden1 = (Image)e.Item.FindControl("image1");
					imageHidden2 = (Image)e.Item.FindControl("image2");
				}
				else
				{
					// Ball image is in middle column
					imageBall = (Image)e.Item.FindControl("image2");

					if (e.Item.ItemIndex == 1)
						imageBall.AlternateText = GetResource("JourneyLineControl.imageBall_FirstVisitLocation.AlternateText");
					else
						imageBall.AlternateText = GetResource("JourneyLineControl.imageBall_SecondVisitLocation.AlternateText");

					// Obtain unused images to hide
					imageHidden1 = (Image)e.Item.FindControl("image1");
					imageHidden2 = (Image)e.Item.FindControl("image3");
				}				
			
				// Set generic image properties
				imageBall.ImageUrl = GetResource("JourneyLineControl.imageBall.ImageUrl");
				imageBall.ToolTip = imageBall.AlternateText;
				imageHidden1.Visible = false;
				imageHidden1.AlternateText = " ";
				imageHidden2.Visible = false;
				imageHidden2.AlternateText = " ";
			}			
		}		
	}
	
	#endregion
	
}
