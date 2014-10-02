// *********************************************** 
// NAME                 : ModeSelectControl.ascx.cs
// AUTHOR               : Tim Mollart
// DATE CREATED         : 11/01/2005
// DESCRIPTION			: Mode Select Control for Find a Fare
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/ModeSelectControl.ascx.cs-arc  $
//
//   Rev 1.3   Feb 11 2010 08:53:40   apatel
//International Planner code changes
//Resolution for 5383: CCN 555 TD Extra - International Planner Phase 1
//
//   Rev 1.2   Mar 31 2008 13:22:12   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:16:42   mturner
//Initial revision.
//
//   Rev 1.5   Feb 23 2006 19:16:58   build
//Automatically merged from branch for stream3129
//
//   Rev 1.4.1.0   Jan 10 2006 15:26:32   mdambrine
//Addition of the resourcemanager namespace (now in a seperate project)
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.4   Nov 03 2005 17:06:54   kjosling
//Automatically merged from branch for stream2816
//
//   Rev 1.3.1.0   Nov 02 2005 18:13:10   NMoorhouse
//TD93 - UEE Input Pages - New format for Input Pages
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages & HTML buttons
//
//   Rev 1.3   Mar 01 2005 18:25:46   tmollart
//Commented out air functionality.
//
//   Rev 1.2   Feb 25 2005 10:13:10   tmollart
//Changed resource handling to use FindAFare resources.
//Added resources for help controls.
//
//   Rev 1.1   Jan 12 2005 14:57:24   tmollart
//Del 7 - Work in progress.
//
//   Rev 1.0   Jan 11 2005 14:39:00   tmollart
//Initial revision.

namespace TransportDirect.UserPortal.Web.Controls
{
	using System;using TransportDirect.Common.ResourceManager;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	using TransportDirect.Web.Support;

	/// <summary>
	///	Control allows user to select travel modes for Find a Fare.
	/// </summary>
	public partial  class ModeSelectControl : TDUserControl
	{

		public const string FIND_A_FARE_RM="FindAFare";		
		private const string HelpIconAltTextKey = "FindFare.ModeSelectControl.HelpIcon.AltText";

		private const string instructionalLabelKey = "FindFare.ModeSelectControl.Instruction";
		private const string trainCheckBoxLabelKey = "FindFare.TransportMode.Rail";
		private const string coachCheckBoxLabelKey = "FindFare.TransportMode.Coach";
		private const string airCheckBoxLabelKey = "FindFare.TransportMode.Air";

		public event EventHandler TravelModeChanged;

		private GenericDisplayMode displayMode;

        private bool showInstruction = false;

		#region Constructor
		public ModeSelectControl()
		{
			LocalResourceManager = TDResourceManager.FIND_A_FARE_RM;
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
			this.PreRender += new System.EventHandler(this.Pre_Render);

		}
		#endregion

		#region Page Load and PreRender

		protected void Page_Load(object sender, System.EventArgs e)
		{
		}

		protected void Pre_Render(object sender, System.EventArgs e)
		{
			//Set up control labels.
			instructionLabel.Text = GetResource(instructionalLabelKey);
			trainCheckBox.Text = GetResource(trainCheckBoxLabelKey);
			coachCheckBox.Text = GetResource(coachCheckBoxLabelKey);
			airCheckBox.Text = GetResource(airCheckBoxLabelKey);

			//Set up appreance of the control.
			trainCheckBox.Visible = (displayMode == GenericDisplayMode.Normal);
			coachCheckBox.Visible = (displayMode == GenericDisplayMode.Normal);
			airCheckBox.Visible = (displayMode == GenericDisplayMode.Normal);
			modesLabel.Visible = (displayMode != GenericDisplayMode.Normal);

            instructionLabel.Visible = showInstruction;

			//Set text in the modesLabel.
			if (trainCheckBox.Checked)
			{
				modesLabel.Text = GetResource(trainCheckBoxLabelKey);
			}

			if (coachCheckBox.Checked)
			{
				if (modesLabel.Text != string.Empty)
					modesLabel.Text = modesLabel.Text + ", ";
				modesLabel.Text = modesLabel.Text + GetResource(coachCheckBoxLabelKey);
			}


            if (airCheckBox.Checked)
            {
                if (modesLabel.Text != string.Empty)
                    modesLabel.Text = modesLabel.Text + ", ";
                modesLabel.Text = modesLabel.Text + GetResource(airCheckBoxLabelKey);
            }
		}

		#endregion

		private void travelModeChanged(object sender, System.EventArgs e)
		{
			if (TravelModeChanged != null)
			{
				TravelModeChanged(this, System.EventArgs.Empty);
			}
		}

		protected void Page_Init(object sender, System.EventArgs e)
		{
			//Wire up event handlers.
			this.trainCheckBox.CheckedChanged += new System.EventHandler(this.travelModeChanged);
			this.coachCheckBox.CheckedChanged += new System.EventHandler(this.travelModeChanged);
			this.airCheckBox.CheckedChanged += new System.EventHandler(this.travelModeChanged);
		}

		private void TravelDetails()
		{
			if (TravelModeChanged != null)
			{
				TravelModeChanged(this, System.EventArgs.Empty);
			}
		}

		#region Public Properties

		/// <summary>
		/// Read/Write. Checked property of the Air checkbox on the control.
		/// </summary>
		public bool AirSelected
		{
			get {return airCheckBox.Checked;}
			set {airCheckBox.Checked = value;}
		}

		/// <summary>
		/// Read/Write. Checked property of the Coach checkbox on the control.
		/// </summary>
		public bool CoachSelected
		{
			get {return coachCheckBox.Checked;}
			set {coachCheckBox.Checked = value;}
		}

		/// <summary>
		/// Read/Write. Checked property of the Train checkbox on the control.
		/// </summary>
		public bool RailSelected
		{
			get {return trainCheckBox.Checked;}
			set {trainCheckBox.Checked = value;}
		}

		/// <summary>
		/// Read/Write. Normal, Ambiguity or Readonly. Sets how the control will
		/// be displayed.
		/// </summary>
		public GenericDisplayMode DisplayMode
		{
			get {return displayMode;}
			set {displayMode = value;}
		}

        /// <summary>
        /// Write only. Sets wether to display instruction label or not
        /// </summary>
        public bool ShowInstruction
        {
            set
            {
                showInstruction = value;
            }
        }

		#endregion Public Properties
	}
}
