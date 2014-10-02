// *********************************************** 
// NAME                 : TriStateRoadControl.ascx
// AUTHOR               : Reza Bamshad
// DATE CREATED         : 18/01/2005 
// DESCRIPTION			: User control that displays either Valid/unspecified/ambiguous road according to whether 
// User control that display either Valid/unspecified/Ambiguous road 
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/TristateRoadControl.ascx.cs-arc  $ 
//
//   Rev 1.2   Mar 31 2008 13:23:32   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:18:34   mturner
//Initial revision.
//
//   Rev 1.9   Feb 23 2006 19:17:14   build
//Automatically merged from branch for stream3129
//
//   Rev 1.8.1.0   Jan 10 2006 15:28:02   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.8   May 19 2005 13:55:10   ralavi
//Changes made as the result of FXCop run.
//
//   Rev 1.7   Apr 26 2005 09:16:50   Ralavi
//Fixing problems with AvoidRoads and UseRoads as the result of IRs 2011 and 2246
//
//   Rev 1.6   Mar 18 2005 11:13:54   RAlavi
//Fixes for door to door via locations control
//
//   Rev 1.5   Mar 14 2005 15:46:50   RAlavi
//Car Costing changes
//
//   Rev 1.4   Mar 04 2005 16:25:42   RAlavi
//Modified problems with road entry ambiguity
//
//   Rev 1.3   Mar 02 2005 15:26:38   RAlavi
//Changes for ambiguity
//
//   Rev 1.2   Feb 21 2005 14:06:40   esevern
//removed populatedisplay method
//
//   Rev 1.1   Feb 18 2005 17:16:22   esevern
//Car costing - interim working copy checked in for code integration
//
//   Rev 1.0   Jan 28 2005 09:57:00   ralavi
//Initial revision.
//
namespace TransportDirect.UserPortal.Web.Controls
{
	using System;using TransportDirect.Common.ResourceManager;
	using System.Globalization;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using TransportDirect.Common;
	using TransportDirect.Common.Logging;
	using TransportDirect.Web.Support;
	using TransportDirect.UserPortal.SessionManager;
	using Logger = System.Diagnostics.Trace;

	/// <summary>
	///		This control provides access to input, modifications and 
	///		display of journey dates and times
	/// </summary>
	public partial  class TristateRoadControl : TDUserControl
	{
		#region declaration

		protected RoadDisplayControl validRoad;
		protected RoadSelectControl  unspecifiedRoad;
		protected AmbiguousRoadSelectControl ambiguousRoad;
		private TDRoad road = new TDRoad();
		public String RoadEntered;
		private TDJourneyParametersMulti journeyParameters;
		private bool textIsValid;
		private bool ambiguityMode;
		private bool isTextRepeated;
		protected RoadSelectControl roadUnspecAmbiguousInput;
	
        
		#endregion

		/// <summary>
		/// Checks current display mode and sets control visibility accordingly
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			//instantiate road object
						
			
			
			journeyParameters = TDSessionManager.Current.JourneyParameters as TDJourneyParametersMulti;
			
			if (journeyParameters == null)
			{
				Logger.Write(new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, "Error trying to cast TDJourneyParameters object to a TDJourneyParametersMulti - supplied object was of type " + TDSessionManager.Current.JourneyParameters.GetType().Name));
				throw new TDException("JourneyPlannerAmbiguity page requires a TDJourneyParametersMulti object", true, TDExceptionIdentifier.SMInvalidTDJourneyParametersType);

			}

			if ((!IsPostBack) && (PageId != PageId.JourneyPlannerAmbiguity))
			{
				unspecifiedRoad.Visible=true;
				validRoad.Visible=false;
				ambiguousRoad.Visible=false;
			}
			else
			{
				if ((RoadEntered != string.Empty) && (RoadEntered != null))
				{
					
					if (road.ValidateRoadName(RoadEntered))
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
				else
				{
					road.Status = TDRoadStatus.Unspecified;
					textIsValid = true;
				
				}
			}

			if (IsTextRepeated)
			{
				road.Status = TDRoadStatus.Ambiguous;
				textIsValid = false;
			}

			switch (road.Status)
			{
				case TDRoadStatus.Unspecified:
					DisplayNormal();
					break;
				case TDRoadStatus.Ambiguous:
					DisplayAmbiguity();
					break;
				case TDRoadStatus.Valid:
					DisplayReadOnly();
					break;
			}

		}

		public void Populate(ref TDRoad thisRoad)	
		{
			Populate(ref thisRoad);         
		}
		

	
		#region set control visibility

		/// <summary>
		/// Displays input control, hides ambiguity and read only controls
		/// </summary>
		/// <param name="e"></param>
		private void DisplayNormal()
		{
			unspecifiedRoad.Visible = true;
			validRoad.Visible = false;
			ambiguousRoad.Visible = false;
		}

		/// <summary>
		/// Displays ambiguity control, hides input and read only controls
		/// </summary>
		/// <param name="e"></param>
		private void DisplayAmbiguity()
		{
			unspecifiedRoad.Visible = false;
			validRoad.Visible = false;
			ambiguousRoad.Visible = true;
			ambiguousRoad.AmbiguousRoadTextBox.Text = RoadEntered;  
		}

		/// <summary>
		/// Displays read only control, hides input and ambiguity controls
		/// </summary>
		/// <param name="e"></param>
		private void DisplayReadOnly()
		{
			unspecifiedRoad.Visible = false;
			validRoad.Visible = true;
			ambiguousRoad.Visible = false;
			validRoad.RoadLabel.Text = RoadEntered;
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

		#region public properties
		
		/// <summary>
		/// Returns the road status
		/// </summary>
		/// <param name="e"></param>
		public TDRoadStatus Status
		{
			get
			{
				if (road != null)
					return road.Status;
				else
					return TDRoadStatus.Unspecified;
			}
		}

		public RoadSelectControl RoadUnspecified
		{
			get
			{
				return unspecifiedRoad;
			}
			
		}

		public RoadDisplayControl RoadValid
		{
			get
			{
				return validRoad;
			}
		}

		public AmbiguousRoadSelectControl RoadAmbiguous
		{
			get
			{
				return ambiguousRoad;
			}
		}

		public bool TextIsValid
		{
			get
			{
				return textIsValid;
			}
		}

		public String RoadSpecified
		{
			get
			{
				return RoadEntered;
			}
			set
			{
				RoadEntered = value;
			}
		}

		public bool AmbiguityMode
		{
			get 
			{
				return ambiguityMode;
			}
			set
			{
				ambiguityMode = value;
			}

		}

		public bool IsTextRepeated
		{
			get
			{
				return isTextRepeated;
			}
			set
			{
				isTextRepeated = value;
			}
		}


		#endregion


	}
}
