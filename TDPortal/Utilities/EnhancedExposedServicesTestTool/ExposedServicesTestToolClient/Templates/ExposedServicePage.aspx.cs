// ***********************************************************************************
// NAME 		: ExposedServicePage.aspx.cs
// AUTHOR 		: Manuel Dambrine
// DATE CREATED : 06-Dec-2005
// DESCRIPTION 	: MainPage for Exposed webservices
// ************************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Stubs/ExposedServicesTestTool/ExposedServicesTestToolClient/Templates/ExposedServicePage.aspx.cs-arc  $
//
//   Rev 1.2   Aug 18 2009 15:27:56   mmodi
//Corrected build warnings
//
//   Rev 1.1   Apr 17 2009 13:53:52   mmodi
//Updated to .NET 2.0
//
//   Rev 1.0   Nov 08 2007 12:49:36   mturner
//Initial revision.
//
//   Rev 1.5   Feb 06 2006 09:38:06   mdambrine
//Rework based on CR054_IR_3318 Enhanced Exposed Services Test Tool.doc
//Resolution for 3407: DEL 8.1 Stream: IR for Module associations for Lauren  TD103
//
//   Rev 1.4   Feb 03 2006 10:56:18   mdambrine
//errors are now popping up instread of a server error
//
//   Rev 1.3   Feb 02 2006 17:22:56   mdambrine
//added version number
//
//   Rev 1.2   Jan 24 2006 11:47:32   mdambrine
//make sure that the selected value from the list stays selected
//
//   Rev 1.1   Dec 23 2005 16:04:14   mdambrine
//Fxcop fixes
//Resolution for 3318: Project Lauren - Exposed Services Test Tool
//
//   Rev 1.0   Dec 20 2005 16:38:34   mdambrine
//Initial revision.
//Resolution for 3318: Project Lauren - Exposed Services Test Tool

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
using System.Xml;
using System.Net;
using System.IO;
using System.Reflection;
using System.Diagnostics;

namespace ExposedServicesTestToolClient
{
	/// <summary>
	/// Summary description for WebForm1.
	/// </summary>
	public partial class ExposedServicePage : System.Web.UI.Page
	{
		
		#region declarations
		
		private string refreshInterval = HelperClass.GetConfigSetting("RefreshInterval");

		
		protected RequestAsync requestAsync;
		#endregion
	
		#region pageHandlers
		/// <summary>
		/// page_load handler for this page
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, System.EventArgs e)
		{			
			if (!Page.IsPostBack)
			{
				//bind the webservices to the combobox
				BindWebServices();	
			}

			InitialiseTestTool();
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
			this.gridResponses.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.gridResponses_PageIndexChanged);
			this.gridResponses.SortCommand += new System.Web.UI.WebControls.DataGridSortCommandEventHandler(this.gridResponses_SortCommand);
			this.gridResponses.DeleteCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.gridResponses_DeleteCommand);
			this.PreRender += new System.EventHandler(this.ExposedServicePage_PreRender);

		}
		#endregion

		#region properties
		/// <summary>
		/// read-only property returning the full sortcommand
		/// </summary>
		public string SortCommand
		{
			get{ return SortColumn + " " + SortDirection;}			
		}

		private string SortDirection
		{
			get{ return (string) Session["SortDirection"] ;}			
			set{ Session["SortDirection"] = value;}
		}

		private string SortColumn
		{
			get{ return (string) Session["SortColumn"] ;}			
			set{ Session["SortColumn"] = value;}
		}

		private displayMode GridDisplayMode
		{
			get{ return (displayMode) Session["Mode"] ;}			
			set{ Session["Mode"] = value;}
		}

		private int PageIndex
		{
			get{ return Convert.ToInt32(Session["PageIndex"]) ;}			
			set{ Session["PageIndex"] = value;}
		}

		private enum displayMode
		{
			users = 1,
			All = 2
		}

		private int SelectedListValue
		{
			get{ return Convert.ToInt32(Session["SelectedListValue"]); }
			set{ Session["SelectedListValue"] = value;}
		}
		#endregion
		
		#region binddata
		/// <summary>
		/// Binds the webservices from the configuration file to the listbox on the screen
		/// </summary>
		private void BindWebServices()
		{
			
			ArrayList WebservicesArray = (ArrayList) CacheEngine.RetrieveWebServices();
			int count = 0;							

			foreach(Webservice webService in WebservicesArray)
			{	
				//listWebservice.Items.Add(new ListItem(webService.Name, "WS"));

				for(int i = 0; i< webService.WebServiceMethods.Length; i++)
				{					
					string textField = webService.Name + " --> " + webService.WebServiceMethods[i].Name.ToString();
					//when the method is async put a special message
					if (webService.WebServiceMethods[i].IsAsync)
						textField += " (async)";
					if (webService.SoapHeaderPath.Length == 0)
						textField += " (non-secure)";
					string valueField = count + "-" + i;

					listWebservice.Items.Add(new ListItem(textField, valueField));
				}

				count++;
			}

			if (SelectedListValue != -1 )
				listWebservice.Items[SelectedListValue].Selected = true;
			
			
		}

		/// <summary>
		/// Binds the responses to the datagrid 
		/// keeps in mind the paging, sorting and mode the application is in
		/// </summary>
		private void BindResponseGrid()
		{
			DataTable resultsTable = new DataTable(); 			

			if (GridDisplayMode == displayMode.users)
				resultsTable = (DataTable) CacheEngine.RetrieveTransactions(Context).GetResultsTable(Page.Session.SessionID);
			else
				resultsTable = (DataTable) CacheEngine.RetrieveTransactions(Context).GetResultsTable(string.Empty);

			DataView SortedTable = new DataView(resultsTable);

			//if we need to sort on a column
			if (SortCommand != " ")
				SortedTable.Sort = SortCommand;

			//check if the pageindex is not exeeding the records in our datatable
			if (PageIndex > ((int)((resultsTable.Rows.Count - 1)/gridResponses.PageSize)))
				PageIndex -= 1;

			gridResponses.DataSource = SortedTable;			
			gridResponses.CurrentPageIndex = PageIndex;
			gridResponses.DataBind();	
		}
		#endregion

		#region initialise
		/// <summary>
		/// initialises the page
		/// </summary>
		private void InitialiseTestTool()
		{			
			
			BindResponseGrid();

			//handle display state
			HandleStateOfDisplay();
				
		}

		/// <summary>
		/// Handles the state of the controls on the screen depending on the processes running
		/// in the background
		/// </summary>
		private void HandleStateOfDisplay()
		{			
			//assign session/application object to some variables for easy coding			
			requestAsync = (RequestAsync) Session["TestToolRequestAsync"];

			//check if a request is in progress
			bool requestInProgress = (requestAsync != null && !requestAsync.IsFinished);
			
			CallService.Enabled = !requestInProgress;
			buttonStopProcess.Enabled = requestInProgress;

			// Put user code to initialize the page here
			if (requestInProgress)
			{
				refresh.Text = "<meta http-equiv='refresh' content='" + refreshInterval + ";URL=ExposedServicePage.aspx?undvik=1'>";				
				if (requestAsync.RequestsLeft >= 1)
					TextRequestsLeft.Text = "Requests Left: " + requestAsync.RequestsLeft;	
				else
					TextRequestsLeft.Text = "Waiting until all responses are received";	
				TextRequestsLeft.Visible = true;
			}	
			else
			{
				refresh.Text = "";		
				TextRequestsLeft.Visible = false;				
			}

			versionNumber.Text = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileMajorPart.ToString() + "." +
								 FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileMinorPart.ToString() + "." +
								 FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileBuildPart.ToString() ;

			//only allow access to the delete button when it is run remoteonly
			if (Page.Request.UserHostName == "127.0.0.1")
				buttonDeleteAllResults.Visible = true;

			

		}
		#endregion

		#region Eventhandlers
		/// <summary>
		/// Event hanlder for the call webservice button, creates a soaprequest array and fires
		/// them off asynchoronously
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void CallService_Click(object sender, System.EventArgs e)
		{
			try
			{
				//build an array of soaprequest objects
				SoapRequest[] soapRequests = BuildRequestObjects();

				//Create a async request handler
				RequestAsync requestAsync = new RequestAsync();									
			
				//add quantity and timespan
				requestAsync.Quantity = Convert.ToInt32(textNrOfTimes.Text, HelperClass.Provider) ;
				requestAsync.TimeSpan = Convert.ToInt32(textInterval.Text, HelperClass.Provider); //milliseconds
				requestAsync.Requests = soapRequests;
						
				//Do the request async
				requestAsync.SendRequests(Context);
		
				//add the async request object to the session
				Session["TestToolRequestAsync"] = requestAsync;

				//set the state of buttons, etc on the screen.
				HandleStateOfDisplay();
			}
			catch(Exception ex)
			{
                ClientScript.RegisterClientScriptBlock(typeof(System.Web.UI.Page), "alert", "<script language='javascript'>alert('" + ex.Message + "', 2);</script>");
				//Page.RegisterClientScriptBlock("alert", "<script language='javascript'>alert('" + ex.Message + "', 2);</script>");
			}

		}

		/// <summary>
		/// Event handler for the show results button, shows the results for this user
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void buttonShowResults_Click(object sender, System.EventArgs e)
		{
			PageIndex = 0;
			GridDisplayMode = displayMode.users;
			BindResponseGrid();
		}

		/// <summary>
		/// Event handler for the show results button, shows the results for all users
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void buttonShowAllResults_Click(object sender, System.EventArgs e)
		{
			PageIndex = 0;
			GridDisplayMode = displayMode.All;
			BindResponseGrid();
		}

		/// <summary>
		/// event handler for the stop process button
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void buttonStopProcess_Click(object sender, System.EventArgs e)
		{
			((RequestAsync)Session["TestToolRequestAsync"]).AbortProcess();
			HandleStateOfDisplay();
		}

		/// <summary>
		/// event handler for the delete my results button, will delete all this users
		/// results
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void buttonDeleteResults_Click(object sender, System.EventArgs e)
		{
			PageIndex = 0;
			CacheEngine.RetrieveTransactions(Context).DeleteTransactions(Session.SessionID);
			BindResponseGrid();
		}

		/// <summary>
		/// event handler for the delete all results button, will delete all results from the cache
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void buttonDeleteAllResults_Click(object sender, System.EventArgs e)
		{
			PageIndex = 0;
			CacheEngine.RetrieveTransactions(Context).DeleteTransactions(string.Empty);
			BindResponseGrid();
		}

		
		
		#region grid
		/// <summary>
		/// Deletes a transaction if the delete hyperlink is clicked on the datagrid
		/// </summary>
		/// <param name="source">the datagrid selected</param>
		/// <param name="e">information about the row selected</param>
		private void gridResponses_DeleteCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			string transactionSelected = (string) e.Item.Cells[0].Text;
			CacheEngine.RetrieveTransactions(Context).DeleteTransaction(transactionSelected);

			BindResponseGrid();
		}

		/// <summary>
		/// event handler for the sort command of the grid
		/// </summary>
		/// <param name="source"></param>
		/// <param name="e"></param>
		private void gridResponses_SortCommand(object source, System.Web.UI.WebControls.DataGridSortCommandEventArgs e)
		{
			//set the sortorder
			if (SortColumn == e.SortExpression)
			{
				if (SortDirection == "ASC")
					SortDirection = "DESC";
				else
					SortDirection = "ASC";
			}
			else
				SortDirection = "DESC";

			//set the sortcolumn
			SortColumn = e.SortExpression;

			BindResponseGrid();
			
		}

		/// <summary>
		/// event handler of the pageindexchanged event on the grid
		/// </summary>
		/// <param name="source"></param>
		/// <param name="e"></param>
		private void gridResponses_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			PageIndex = e.NewPageIndex;

			BindResponseGrid();
		}

		
		#endregion

		#endregion

		#region SupportFunctions	
		/// <summary>
		/// Builds an array of request objects depending on the file selected for upload
		/// </summary>
		/// <returns>an array of request objects</returns>
		private SoapRequest[] BuildRequestObjects()
		{			
			string soapMessageSeperator = HelperClass.GetConfigSetting("SoapMessageSeperator");

			//Get the soapmessage(s) from the selected file
			string[] soapMessages = HelperClass.SplitSoapMessages(HelperClass.ConvertStreamToString(UploadFile.PostedFile.InputStream), 
																  soapMessageSeperator);

			SoapRequest[] soapRequests = new SoapRequest[soapMessages.Length];

			//per soapmessage create a request object
			for( int i=0; i < soapMessages.Length; i++)
			{
				SoapRequest soapRequest = new SoapRequest(soapMessages[i].ToString(), 
														  GetSelectedWebservice(),
														  Page.Session.SessionID);				

				soapRequests[i] = soapRequest;
			}

			return soapRequests;
			
		}

		/// <summary>
		/// gets the currently selected webservice
		/// </summary>
		/// <returns></returns>
		private Webservice GetSelectedWebservice()
		{
			string[] selectedValues = listWebservice.SelectedValue.Split('-');
			ArrayList webServicesArray = (ArrayList) CacheEngine.RetrieveWebServices();
						
			//search the werbservice from the application state
			Webservice webservice = (Webservice) webServicesArray[Convert.ToInt32(selectedValues[0], HelperClass.Provider)];
			//add the method selected
			WebServiceMethod[] webserviceMethod = new WebServiceMethod[1];
			webserviceMethod[0] = webservice.WebServiceMethods[Convert.ToInt32(selectedValues[1])];
			webservice.WebServiceMethods = webserviceMethod;
			
			return webservice;
		}
		#endregion

		protected void ExposedServicePage_PreRender(object sender, System.EventArgs e)
		{
			SelectedListValue = listWebservice.SelectedIndex;
		}

	}
}
