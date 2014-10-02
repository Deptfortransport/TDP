// *********************************************** 
// NAME                 : LogViewer.aspx.cs
// AUTHOR               : Atos Origin
// DATE CREATED         : 01/07/2004
// DESCRIPTION			: Page to view the logging data for any event types.
//						  ** Access restricted to non-standard users **
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Viewer/LogViewer.aspx.cs-arc  $
//
//   Rev 1.3   Jan 16 2009 10:05:56   devfactory
//No change.
//Resolution for 5209: XHTML Compliance Work for 10.4
//
//   Rev 1.2   Mar 31 2008 13:27:22   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:30:20   mturner
//Initial revision.
//
//Rev DevFatory Feb 8th 16:28:00 dgath
//Line added to Page_Load event for White Label left menu configuration
//
//   Rev 1.7   Feb 23 2006 19:02:32   aviitanen
//Manual merge for Enhanced Exposed Services (stream3129).
//
//   Rev 1.6   Feb 10 2006 15:09:16   build
//Automatically merged from branch for stream3180
//
//   Rev 1.5.1.0   Dec 06 2005 17:42:44   RGriffith
//Changes applied for using new HeaderControl and HeadElementControl for Homepage Phase2
//
//   Rev 1.5   Nov 08 2005 18:30:44   ECHAN
//Fix for code review comment #15
//
//   Rev 1.4   Nov 03 2005 16:11:00   kjosling
//Automatically merged from branch for stream2816
//
//   Rev 1.3.1.0   Oct 18 2005 11:51:44   RGriffith
//TD089 ES020 Image Button Replacement
//Resolution for 2816: DEL 7.3 Stream: UEE work on Input pages
//
//   Rev 1.3   Aug 19 2005 14:08:52   jgeorge
//Automatically merged from branch for stream2572
//
//   Rev 1.2.1.0   Jul 29 2005 16:40:16   asinclair
//Enabled filtering on Category drop down
//Resolution for 2572: DEL 8 Stream: Travel Information / Journey Results
//
//   Rev 1.2   Nov 03 2004 14:24:28   jgeorge
//Modified to use ReportStagingDB instead of DefaultDB
//
//   Rev 1.1   Jul 19 2004 14:53:06   AWindley
//Implementation of CCN098: CJP Logging changes
//
//   Rev 1.0   Jul 02 2004 13:35:52   AWindley
//Initial revision.

using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text;

using TransportDirect.Common;
using TransportDirect.Common.Logging;
using TransportDirect.Common.DatabaseInfrastructure;

using TransportDirect.Common.ResourceManager;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.Web.Support;
using TransportDirect.UserPortal.Web;
using TransportDirect.UserPortal.Resource;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.JourneyControl;
using TransportDirect.ReportDataProvider.EventDataLoader;

using OperationalEventFilter = TransportDirect.ReportDataProvider.EventDataLoader.OperationalEventFilter;

namespace TransportDirect.UserPortal.Web.Templates
{
	/// <summary>
	/// Summary description for LogViewer.
	/// </summary>
	public partial class LogViewer : TDPage
	{
		protected TransportDirect.UserPortal.Web.Controls.HeaderControl headerControl;
		private const string TAB = "\t";

		#region Constructor
		/// <summary>
		/// Constructor, sets the PageId and calls base.
		/// </summary>
		public LogViewer() : base()
		{
			pageId = PageId.LogViewer;
		}
		#endregion

		protected void Page_Load(object sender, System.EventArgs e)
		{
			InitialiseControls();

            //Added for white labelling:
            ConfigureLeftMenu("LogViewer.clientLink.BookmarkTitle", "LogViewer.clientLink.LinkText", clientLink, expandableMenuControl,TransportDirect.UserPortal.SuggestionLinkService.Context.HomePageMenuTipsAndTools);
            //added for white-labelling Related link part of side menu
            expandableMenuControl.AddContext(TransportDirect.UserPortal.SuggestionLinkService.Context.RelatedLinksContextLogViewer);
            expandableMenuControl.AddExpandedCategory("Related links");
		}

		#region Initialise Controls
		/// <summary>
		/// Method to initialise controls that are on the page.
		/// </summary>
		private void InitialiseControls()
		{			
			// Set up buttons
			submitButton.Text = GetResource("LogViewer.submitButton.Text");

			// Setup viewer controls
			textEventsRetrieved.Text = String.Empty;
			textEventsRetrieved.ReadOnly = true;
			textEventsRetrieved.TextMode = TextBoxMode.MultiLine;
			textEventsRetrieved.Rows = 30;
			labelNumOfEvents.Text = String.Empty;
			labelNumOfEvents.Visible = false;

			// Disable filter on time. Only current day's events available anyway.
			labelStartTime.Visible = false;
			labelEndTime.Visible = false;

			// Setup filter on Category
			dropCategory.Items.Insert(0, new ListItem("All", String.Empty));
			dropCategory.Items.Insert(1, new ListItem("Business", "Business"));
			dropCategory.Items.Insert(2, new ListItem("CJP", "CJP"));
			dropCategory.Items.Insert(3, new ListItem("Database", "Database"));
			dropCategory.Items.Insert(4, new ListItem("Infrastructure", "Infrastructure"));
			dropCategory.Items.Insert(5, new ListItem("ThirdParty", "ThirdParty"));
			// Default to All
			dropCategory.SelectedIndex = 0;

			// Setup filter on Level
			dropLevel.Items.Insert(0, new ListItem("All", String.Empty));
			dropLevel.Items.Insert(1, new ListItem("Error", "Error"));
			dropLevel.Items.Insert(2, new ListItem("Info", "Info"));
			dropLevel.Items.Insert(3, new ListItem("Warning", "Warning"));
			dropLevel.Items.Insert(4, new ListItem("Verbose", "Verbose"));
			// Default to All
			dropLevel.SelectedIndex = 0;

			// Inform user of their session ID
			ITDSessionManager sessionManager = 
				(ITDSessionManager)TDServiceDiscovery.Current
				[ServiceDiscoveryKey.SessionManager];

			string sessionID = sessionManager.Session.SessionID;
			labelSessionInfo.Text += sessionID.ToString(TDCultureInfo.CurrentUICulture);

			// Inform user of their journey reference
			ITDJourneyResult journeyResult = TDItineraryManager.Current.JourneyResult;

			if (journeyResult != null)
			{
				int refNum = journeyResult.JourneyReferenceNumber;
				labelReferenceInfo.Text += SqlHelper.FormatRef(refNum);
			}
			else
			{
				labelReferenceInfo.Text += Global.tdResourceManager.GetString(
					"LogViewer.NoJourneyReference", TDCultureInfo.CurrentUICulture);
			}

			// User type specific configuration
			/*if ((int)TDSessionManager.Current.CurrentUser.UserType == (int)TDUserType.CJP)
			{*/
				dropCategory.SelectedIndex = 2;
			/*}
			else
			{
				// For all other users, default Session ID to current session
				textSessionID.Text = sessionID.ToString(TDCultureInfo.CurrentUICulture);
			}*/
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
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.ID = "LogViewer";
            submitButton.Click += new EventHandler(this.GoImageButton_Click);
		}
		#endregion

		#region Button Handlers
		protected void GoImageButton_Click(object sender, EventArgs e)
		{

			// Clear events retrieved display
			textEventsRetrieved.Text = String.Empty;
			labelNumOfEvents.Text = String.Empty;

			// Create filters from user selection
			OperationalEventFilter combination = null;
			OperationalEventFilter filterCategory = null;
			OperationalEventFilter filterLevel = null;
			
			// Category
			if ((int)TDSessionManager.Current.CurrentUser.UserType == (int)TDUserType.CJP)
			{
				// For CJP users, force filter on Category to CJP events
				filterCategory = new OperationalEventFilter( TDEventCategory.CJP, OperationalEventFilterMethod.And);
			}
			else if (dropCategory.SelectedItem.Value.Length != 0)
			{
				// Filter according to user selection
				TDEventCategory category = (TDEventCategory)Enum.Parse(typeof(TDEventCategory), dropCategory.SelectedItem.Value, true);
				filterCategory = new OperationalEventFilter( category, OperationalEventFilterMethod.And);
			}

			if (filterCategory != null)
			{
				// Add filter
				combination = new OperationalEventFilter( 
					new OperationalEventFilter[] { filterCategory }, OperationalEventFilterMethod.And);
			}

			// Level
			if (dropLevel.SelectedItem.Value.Length != 0)
			{
				// Filter according to user selection
				TDTraceLevel level = (TDTraceLevel)Enum.Parse(typeof(TDTraceLevel), dropLevel.SelectedItem.Value, true);
				filterLevel = new OperationalEventFilter( level, OperationalEventFilterMethod.And);
			}

			if (filterLevel != null)
			{
				// Add filter
				combination = new OperationalEventFilter(
					new OperationalEventFilter[] { combination, filterLevel }, OperationalEventFilterMethod.And);
			}

			// Session ID
			if (textSessionID.Text.Trim().Length != 0)
			{
				// Filter has been requested
				OperationalEventFilter filterSession = new OperationalEventFilter( 
					HttpUtility.HtmlEncode(textSessionID.Text.Trim()), 
					OperationalEventMatchField.SessionIdEquals, 
					false, 
					OperationalEventFilterMethod.And);

				// Add filter to combination
				combination = new OperationalEventFilter(
					new OperationalEventFilter[] { combination, filterSession }, OperationalEventFilterMethod.And);
			}

			// Machine Name
			if (textMachineName.Text.Trim().Length != 0)
			{
				OperationalEventFilter filterMachine = new OperationalEventFilter( 
					HttpUtility.HtmlEncode(textMachineName.Text.Trim()), 
					OperationalEventMatchField.MachineNameEquals, 
					false, 
					OperationalEventFilterMethod.And);
				
				combination = new OperationalEventFilter(
					new OperationalEventFilter[] { combination, filterMachine }, OperationalEventFilterMethod.And);
			}

			// Message
			if (textMessage.Text.Trim().Length != 0)
			{
				OperationalEventFilter filterMessage = new OperationalEventFilter( 
					HttpUtility.HtmlEncode(textMessage.Text.Trim()), 
					OperationalEventMatchField.MessageContains, 
					false, 
					OperationalEventFilterMethod.And);
				
				combination = new OperationalEventFilter(
					new OperationalEventFilter[] { combination, filterMessage }, OperationalEventFilterMethod.And);
			}

			// Method Name
			if (textMethod.Text.Trim().Length != 0)
			{
				OperationalEventFilter filterMethod = new OperationalEventFilter( 
					HttpUtility.HtmlEncode(textMethod.Text.Trim()), 
					OperationalEventMatchField.MethodNameEquals, 
					false, 
					OperationalEventFilterMethod.And);

				combination = new OperationalEventFilter(
					new OperationalEventFilter[] { combination, filterMethod }, OperationalEventFilterMethod.And);
			}

			// Type Name
			if (textType.Text.Trim().Length != 0)
			{
				OperationalEventFilter filterType = new OperationalEventFilter( 
					HttpUtility.HtmlEncode(textType.Text.Trim()), 
					OperationalEventMatchField.TypeNameEquals, 
					false, 
					OperationalEventFilterMethod.And);

				combination = new OperationalEventFilter(
					new OperationalEventFilter[] { combination, filterType }, OperationalEventFilterMethod.And);
			}

			// Assembly Name
			if (textAssembly.Text.Trim().Length != 0)
			{
				OperationalEventFilter filterAssembly = new OperationalEventFilter( 
					HttpUtility.HtmlEncode(textAssembly.Text.Trim()), 
					OperationalEventMatchField.AssemblyNameEquals, 
					false, 
					OperationalEventFilterMethod.And);

				combination = new OperationalEventFilter(
					new OperationalEventFilter[] { combination, filterAssembly }, OperationalEventFilterMethod.And);
			}

			// Retrieve filtered events
			IOperationalEventLoader oel = new DatabaseOperationalEventLoader(SqlHelperDatabase.ReportStagingDB);
			LoadedOperationalEvent[] events = oel.GetEvents(combination);

			// Inform user of events received out of maximum possible events
			string noOfEvents = Global.tdResourceManager.GetString(
				"LogViewer.labelNumOfEvents", TDCultureInfo.CurrentUICulture);
			
			labelNumOfEvents.Text = string.Format(noOfEvents, 
				events.Length.ToString(TDCultureInfo.CurrentUICulture), 
				DatabaseOperationalEventLoader.MaxNoOfEvents);

			labelNumOfEvents.Visible = true;
						
			// Format retrieved events
			StringBuilder sb = new StringBuilder();
			for	(int i = 0; i < events.Length; i++) 
			{
				sb.Append(
					events[i].Time.ToString() 
					+ TAB + events[i].Category.ToString() 
					+ TAB + events[i].Level.ToString() 
					+ "\n Session: " + events[i].SessionId
					+ "\n Machine: " + events[i].MachineName 
					+ "\n Message: " + events[i].Message
					+ "\n Method : " + events[i].MethodName
					+ ", Type: " + events[i].TypeName
					+ ", Assembly: " + events[i].AssemblyName
					+ "\n--------------------------------------------------\n");
			}
			
			if (sb.Length != 0)
			{
				textEventsRetrieved.Text = sb.ToString();
			}
			else 
			{
				// Inform user that no matches were returned
				textEventsRetrieved.Text = Global.tdResourceManager.GetString(
					"LogViewer.NoEventsRetrieved", TDCultureInfo.CurrentUICulture);
			}
		}
		#endregion
	}
}
