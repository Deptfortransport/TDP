//******************************************************************************
//NAME			: ItineraryViasControl.ascx
//AUTHOR		: Andrew Sinclair
//DATE CREATED	: 19/12/2005
//DESCRIPTION	: Control  lists the names of the locations at the interchange points
// between the exisiting segments.   
//******************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/ItineraryViasControl.ascx.cs-arc  $ 
//
//   Rev 1.2   Mar 31 2008 13:21:08   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:15:04   mturner
//Initial revision.
//
//   Rev 1.4   Apr 13 2006 15:39:04   mdambrine
//emoved the code that was overwriting the locations in the ItineraryViasControl.ascx control
//Resolution for 3702: DN068 Extend: Clicking 'Amend' on Extensions results page switches 'Via' and 'To' locations on input page
//
//   Rev 1.3   Mar 21 2006 11:46:30   asinclair
//Updated with code review comments
//
//   Rev 1.2   Mar 08 2006 16:02:56   RGriffith
//FxCop Suggested Changes
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.1   Feb 21 2006 08:50:14   asinclair
//Updated
//Resolution for 3353: DEL 8.1 Stream: Extend, Adjust and Replan
//
//   Rev 1.0   Jan 13 2006 14:35:38   asinclair
//Initial revision.
namespace TransportDirect.UserPortal.Web.Controls
{
	using System;
	using System.Collections;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using TransportDirect.UserPortal.LocationService;
	using TransportDirect.UserPortal.SessionManager;
	

	/// <summary>
	///	Summary description for ItineraryViasControl.
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public partial class ItineraryViasControl :  TDUserControl
	{

		private TDLocation[] outwardViaLocations;
		private TDLocation[] returnViaLocations;

		#region  Page Load / OnPreRender Methods

		/// <summary>
		/// Handles page load event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, System.EventArgs e)
		{

		}

		/// <summary>
		/// Event handler for the prerender event
		/// Sets the datasource for repeaters and databinds
		/// </summary>
		/// <param name="e"></param>
		protected override void OnPreRender( EventArgs e )
		{

			ArrayList locations = new ArrayList();
			locations.AddRange(outwardViaLocations);

			outwardViaLocations = (TDLocation[])locations.ToArray(typeof(TDLocation));

			ArrayList viaOutwardLocations = new ArrayList();

			int i = 0;
			foreach (TDLocation location in outwardViaLocations)
			{				
				if ( i < outwardViaLocations.Length)
				{
					viaOutwardLocations.Add(location.Description.ToString());
					i++;
				}
			}

			locationsList.DataSource = viaOutwardLocations;
			locationsList.DataBind();
		}
		#endregion

		#region  Properties

		/// <summary>
		///  Gets/Sets the array of OutwardViaLocations
		/// </summary>		
		public TDLocation[] OutwardViaLocations
		{
			get {return outwardViaLocations;}
			set {outwardViaLocations = value;}
		}


		/// <summary>
		///  Gets/Sets the array of ReturnViaLocations
		/// </summary>		
		public TDLocation[] ReturnViaLocations
		{
			get {return returnViaLocations;}
			set {returnViaLocations = value;}
		}

		/// <summary>
		///  Gets/Sets the first label in the control, as text changes depending on where control is used
		/// </summary>	
		public Label LabelTitle1Text
		{
			get {return labelTitle1Text;}
			set {labelTitle1Text = value;}
		}

		/// <summary>
		///  Gets/Sets the second label in the control, as text changes depending on where control is used
		/// </summary>	
		public Label LabelTitle2Text
		{
			get {return labelTitle2Text;}
			set {labelTitle2Text = value;}
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

		}
		#endregion
	}
}
