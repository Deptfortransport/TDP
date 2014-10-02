// *********************************************** 
// NAME                 : RefinePageOptionsControl.ascx.cs
// AUTHOR               : NMoorhouse
// DATE CREATED         : 19/01/06
// DESCRIPTION  : Control containing Back, Clear, and Submit buttons for Refine pages
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/RefinePageOptionsControl.ascx.cs-arc  $:
//
//   Rev 1.2   Mar 31 2008 13:22:34   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:17:12   mturner
//Initial revision.
//
//   Rev 1.1   Mar 21 2006 11:53:24   asinclair
//Updated after code review comments
namespace TransportDirect.UserPortal.Web.Controls
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using TransportDirect.Common;

	using TransportDirect.Web.Support;

	/// <summary>
	///	Control to display submit, clear and back buttons
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	public partial  class RefinePageOptionsControl : TDUserControl
	{
		#region Controls
		#endregion

		#region Constants/variables
		// Keys used to obtain strings from the resource file
		private const string BackImageTextKey = "FindPageOptionsControl.Back.Text";
		private const string ClearImageTextKey = "FindPageOptionsControl.Clear.Text";
		private const string SubmitImageTextKey = "FindPageOptionsControl.Submit.Text";

		// Keys used to save/load elements in the viewstate
		private const int AllowBackKey = 2;
		private const int MaxViewStateIndex = 2;

		private bool allowBack;
		private bool allowClear = true;
		#endregion

		#region Public events

		// The following lines declare objects that can be used as
		// keys in the EventHandlers table for the control.
		private static readonly object SubmitEventKey = new object();
		private static readonly object ClearEventKey = new object();
		private static readonly object BackEventKey = new object();

		/// <summary>
		/// Occurs when the Submit button is clicked
		/// </summary>
		public event EventHandler Submit
		{
			add { this.Events.AddHandler(SubmitEventKey, value); }
			remove { this.Events.RemoveHandler(SubmitEventKey, value); }
		}

		/// <summary>
		/// Occurs when the Clear button is clicked
		/// </summary>
		public event EventHandler Clear
		{
			add { this.Events.AddHandler(ClearEventKey, value); }
			remove { this.Events.RemoveHandler(ClearEventKey, value); }
		}

		/// <summary>
		/// Occurs when the Back button is clicked
		/// </summary>
		public event EventHandler Back
		{
			add { this.Events.AddHandler(BackEventKey, value); }
			remove { this.Events.RemoveHandler(BackEventKey, value); }
		}
		#endregion

		#region Page lifecycle event handlers
		/// <summary>
		/// Handler for the Load event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			commandBack.Text = GetResource(BackImageTextKey);
			commandClear.Text = GetResource(ClearImageTextKey);
			commandSubmit.Text = GetResource(SubmitImageTextKey);				
		}

		/// <summary>
		/// Sets control visibility depending on properties
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_PreRender(object sender, System.EventArgs e)
		{
			commandBack.Visible = allowBack;
			commandClear.Visible = allowClear;
		}

		
		/// <summary>
		/// Sets up the necessary event handlers.
		/// </summary>
		private void ExtraWiringEvents() 
		{
			this.commandBack.Click += new EventHandler(this.OnCommandButtonClick);
			this.commandClear.Click += new EventHandler(this.OnCommandButtonClick);
			this.commandSubmit.Click += new EventHandler(this.OnCommandButtonClick);
		}
		#endregion

		#region Properties

		/// <summary>
		///Read-Write property.  Controls visibility of the back button
		/// </summary>
		public bool AllowBack
		{
			get { return allowBack; }
			set { allowBack = value; }
		}

		/// <summary>
		///Read-Write property.  Controls visibility of the clear button
		/// </summary>
		public bool AllowClear
		{
			get { return allowClear; }
			set { allowClear = value; }
		}
		#endregion

		#region Control event handler
		/// <summary>
		/// Handles the click event of the command buttons and raises the appropriate event
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnCommandButtonClick(object sender, EventArgs e)
		{
			EventHandler theDelegate = null;

			if (sender.Equals(commandBack))
				theDelegate = this.Events[BackEventKey] as EventHandler;
			else if (sender.Equals(commandClear))
				theDelegate = this.Events[ClearEventKey] as EventHandler;
			else if (sender.Equals(commandSubmit))
				theDelegate = this.Events[SubmitEventKey] as EventHandler;

			if (theDelegate != null)
				theDelegate(this, EventArgs.Empty);

		}
		#endregion

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{

			ExtraWiringEvents();
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{

		}
		#endregion

	}
}
