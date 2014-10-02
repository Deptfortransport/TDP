//*********************************************** 
// NAME                 : AmbiguousRoadSelectControl.ascx
// AUTHOR               : Reza Bamshad
// DATE CREATED         : 18/01/2005 
// DESCRIPTION  : New version for AmbiguousRoadSelectControl. Allows user to modify the road name if it does not
//                start with an alphabetic character and it is not followed by one digit.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/AmbiguousRoadSelectControl.ascx.cs-arc  $
//
//   Rev 1.2   Mar 31 2008 13:19:04   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:11:56   mturner
//Initial revision.
//
//   Rev 1.9   Feb 23 2006 19:16:18   build
//Automatically merged from branch for stream3129
//
//   Rev 1.8.1.0   Jan 10 2006 15:23:14   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.8   May 11 2005 10:38:32   Ralavi
//Modifications after running FXCop.
//
//   Rev 1.7   May 06 2005 17:31:00   Ralavi
//Changing the HTML page to pick up the style from the style sheet to ensure red border for avoid Roads and use roads are displayed in Mozilla.
//
//   Rev 1.6   Apr 20 2005 12:24:36   Ralavi
//No change.
//
//   Rev 1.5   Apr 06 2005 12:59:42   Ralavi
//Allocated a limit of 12 to text box
//
//   Rev 1.4   Mar 30 2005 18:18:36   RAlavi
//Missing "transportDirect.userportal" from inherit in html
//
//   Rev 1.3   Mar 30 2005 16:50:14   RAlavi
//No change.
//
//   Rev 1.2   Mar 02 2005 15:23:54   RAlavi
//changes for ambiguity
//
//   Rev 1.1   Feb 21 2005 14:05:48   esevern
//checking for integration with input page
//
//   Rev 1.0   Jan 28 2005 09:52:08   ralavi
//Initial revision.

using System;using TransportDirect.Common.ResourceManager;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using TransportDirect.UserPortal.Web;
using TransportDirect.Web.Support;
using TransportDirect.Common;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.SessionManager;
using System.Globalization;

namespace TransportDirect.UserPortal.Web.Controls
{
	
	/// <summary>
	///		Summary description for AmbiguousRoadSelectControl
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public partial  class AmbiguousRoadSelectControl : TDUserControl
	{
		

		#region declaration
		//protected System.Web.UI.WebControls.TextBox ambigRoadTextbox;
		protected System.Web.UI.WebControls.Label labelSRSelect;
		protected System.Web.UI.WebControls.Label labelRoadAlert;
		private bool textIsValid;

		private string possibleOptionsText = string.Empty;
		
		#endregion

		protected void Page_Load(object sender, System.EventArgs e)
		{
			//instantiate road object
			TDRoad road = new TDRoad();

			
			if ((txtRoad.Text != null) && (txtRoad.Text.Length == 0))
			{
				if (road.ValidateRoadName(txtRoad.Text))
				{
					road.Status = TDRoadStatus.Valid;
					textIsValid = true;
				}
				else
				{
					road.Status = TDRoadStatus.Ambiguous;
					textIsValid = false;
					
				}

		
			}

				
		}


		#region Properties giving access to the controls

		/// <summary>
		/// Read/write property returning the textLocation control
		/// </summary>
		public TextBox AmbiguousRoadTextBox
		{
			get
			{
				return txtRoad;
			}
			set
			{
				txtRoad = value;
			}
		}


		/// <summary>
		/// Read-only property to check that returns true if text is valid
		/// </summary>
		public bool TextIsValid
		{
			get
			{
				return textIsValid;
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
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{

		}
		#endregion
	}
}
