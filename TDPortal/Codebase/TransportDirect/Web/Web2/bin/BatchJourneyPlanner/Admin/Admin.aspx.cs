// *********************************************** 
// NAME                 : Admin.aspx.cs 
// AUTHOR               : David Lane 
// DATE CREATED         : 28/02/2012 
// DESCRIPTION			: Page for user maintenance.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/BatchJourneyPlanner/Admin/Admin.aspx.cs-arc  $
//
//   Rev 1.10   Apr 26 2013 16:49:16   rbroddle
//Updated references to BatchUserStatus to be fully qualified to resolve "ambiguous reference" errors from build process.
//
//   Rev 1.9   Mar 22 2013 10:49:02   DLane
//CCN0648c - Super Batch enhancements
//Resolution for 5907: CCN0648c - "Super Batch" enhancements
//
//   Rev 1.8   Sep 04 2012 16:40:14   DLane
//Post-workshop updates
//Resolution for 5831: CCN648b - Batch phase 2
//
//   Rev 1.7   Aug 30 2012 16:45:38   DLane
//Batch phase 2
//Resolution for 5831: CCN648b - Batch phase 2
//
//   Rev 1.6   Aug 28 2012 17:20:54   DLane
//Batch phase 2 updates - first checkin
//Resolution for 5831: CCN667 - Batch phase 2
//
//   Rev 1.5   Jun 08 2012 13:29:36   PScott
//Fix initial page count
//
//   Rev 1.4   Mar 20 2012 16:29:48   dlane
//Fix to add javascript registration
//Resolution for 5787: Batch Journey Planner
//
//   Rev 1.3   Feb 28 2012 15:18:56   DLane
//Updates to javascript and comments
//Resolution for 5787: Batch Journey Planner

using System;
using System.Collections.Generic;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Logger = System.Diagnostics.Trace;
using TransportDirect.Common;
using TransportDirect.Common.DatabaseInfrastructure;
using TransportDirect.Common.Logging;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.Web.Adapters;
using TransportDirect.CommonWeb.Batch;

namespace TransportDirect.UserPortal.Web.BatchJourneyPlanner.Admin
{
    public partial class Admin : TDPage
    {
        #region variables

        private const string RES_TITLE = "BatchUserAdmin.Title";
        private const string RES_USERSLABEL = "BatchUserAdmin.UsersLabel";
        private const string RES_USERSTABLELABEL = "BatchUserAdmin.UsersTableLabel";
        private const string RES_HEADERUSER = "BatchUserAdmin.HeaderUser";
        private const string RES_HEADERSTATUSCHANGE = "BatchUserAdmin.HeaderStatusChange";
        private const string RES_HEADERLASTUPLOAD = "BatchUserAdmin.HeaderLastUpload";
        private const string RES_HEADERSTATUS = "BatchUserAdmin.HeaderStatus";
        private const string RES_HEADERSELECT = "BatchUserAdmin.HeaderSelect";
        private const string RES_NOTLOGGEDIN = "BatchUserAdmin.NotLoggedIn";
        private const string RES_SELECTUSER = "BatchUserAdmin.SelectUser";
        private const string RES_BUTTONRELOAD = "BatchUserAdmin.ButtonReload";
        private const string RES_BUTTONSUSPEND = "BatchUserAdmin.ButtonSuspend";
        private const string RES_BUTTONACTIVATE = "BatchUserAdmin.ButtonActivate";
        private const string RES_USERSUSPENDED = "BatchUserAdmin.UserSuspended";
        private const string RES_USERACTIVATED = "BatchUserAdmin.UserActivated";
        private const string RES_USERALREADYSUSPENDED = "BatchUserAdmin.UserAlreadySuspended";
        private const string RES_USERALREADYACTIVATED = "BatchUserAdmin.UserAlreadyActivated";
        private const string RES_IMAGE = "BatchUserAdmin.ImageUrl";
        private const string RES_EMAILFROM = "BatchJourneyPlanner.EmailFrom";
        private const string RES_ACTIVATEDEMAILBODY = "BatchJourneyPlanner.ActivatedEmailBody";
        private const string RES_ACTIVATEDEMAILTITLE = "BatchJourneyPlanner.ActivatedEmailTitle";
        private const string RES_UPARROW = "BatchJourneyPlanner.ArrowUp";
        private const string RES_DOWNARROW = "BatchJourneyPlanner.ArrowDown";

        private int pageNumber;
        private string sortColumn;
        private string sortDirection;
        private int totalPages;
        private bool sortDone = false;

        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        public Admin()
            : base()
        {
            pageId = PageId.Admin;
        }

        #region Page events

        /// <summary>
        /// Page PreRender event handler
        /// </summary>
        /// <param name="sender">Notifying object</param>
        /// <param name="e">Event data</param>
        protected void Page_PreRender(object sender, EventArgs e)
        {
            // Register javascript
            TDPage thePage = (TDPage)Page;
            ScriptRepository.ScriptRepository repository = (ScriptRepository.ScriptRepository)TDServiceDiscovery.Current[ServiceDiscoveryKey.ScriptRepository];
            thePage.ClientScript.RegisterStartupScript(this.GetType(), "BatchJourneyPlanner", repository.GetScript("BatchJourneyPlanner", thePage.JavascriptDom));
        }

        /// <summary>
        /// Page Init event handler
        /// </summary>
        /// <param name="sender">Notifying object</param>
        /// <param name="e">Event data</param>
        protected void Page_Init(object sender, EventArgs e)
        {
            this.linkFirst.Click += new EventHandler(this.linkFirst_Click);
            this.linkPrevious.Click += new EventHandler(this.linkPrevious_Click);
            this.linkNext.Click += new EventHandler(this.linkNext_Click);
            this.linkLast.Click += new EventHandler(this.linkLast_Click);
            this.buttonReloadTable.Click += new EventHandler(this.buttonReloadTable_Click);
            this.buttonSuspend.Click += new EventHandler(this.buttonSuspend_Click);
            this.buttonActivate.Click += new EventHandler(this.buttonActivate_Click);
        }

        /// <summary>
        /// Page Load event handler
        /// </summary>
        /// <param name="sender">Notifying object</param>
        /// <param name="e">Event data</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            // defaults
            panelErrorMessage.Visible = false;
            panelUserAdmin.Visible = true;

            LoadResources();

            // attempt to retrieve vars from viewstate
            totalPages = 1;
            if (ViewState["pageNumber"] != null)
            {
                pageNumber = (int)ViewState["pageNumber"];
                
            }
            else
            {
                pageNumber = 1;
            }
            if (ViewState["sortColumnAdmin"] != null)
            {
                sortColumn = (string)ViewState["sortColumnAdmin"];
            }
            else
            {
                sortColumn = "Status";
            }
            if (ViewState["sortDirectionAdmin"] != null)
            {
                sortDirection = (string)ViewState["sortDirectionAdmin"];
            }
            else
            {
                sortDirection = "asc";
            }

            if (!TDSessionManager.Current.Authenticated)
            {
                panelErrorMessage.Visible = true;
                labelErrorMessages.Text = GetResource(RES_NOTLOGGEDIN);
                panelUserAdmin.Visible = false;
            }
            else
            {
                PopulateUsersTable();
            }

            ConfigureLeftMenu(expandableMenuControl, TransportDirect.UserPortal.SuggestionLinkService.Context.HomePageMenuTipsAndTools);
            //added for white-labelling Related link part of side menu
            expandableMenuControl.AddContext(TransportDirect.UserPortal.SuggestionLinkService.Context.BatchJourneyPlanner);
            expandableMenuControl.AddExpandedCategory("Related links");
        }

        #endregion

        #region Event handlers

        /// <summary>
        /// Sort out column values
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        protected void usersRepeater_ItemDatabound(object sender, RepeaterItemEventArgs e)
        {
            string backColourClass = "cellNormal";

            switch (e.Item.ItemType)
            {
                case ListItemType.Header:
                    LinkButton header1 = (LinkButton)e.Item.FindControl("header1");
                    header1.Text = sortColumn != "User" ? GetResource(RES_HEADERUSER) : sortDirection == "asc" ? GetResource(RES_HEADERUSER) + GetResource(RES_DOWNARROW) : GetResource(RES_HEADERUSER) + GetResource(RES_UPARROW);
                    header1.Click += new EventHandler(sortUser_Click);
                    LinkButton header2 = (LinkButton)e.Item.FindControl("header2");
                    header2.Text = sortColumn != "StatusChange" ? GetResource(RES_HEADERSTATUSCHANGE) : sortDirection == "asc" ? GetResource(RES_HEADERSTATUSCHANGE) + GetResource(RES_DOWNARROW) : GetResource(RES_HEADERSTATUSCHANGE) + GetResource(RES_UPARROW);
                    header2.Click += new EventHandler(sortStatusChange_Click);
                    LinkButton header3 = (LinkButton)e.Item.FindControl("header3");
                    header3.Text = sortColumn != "LastFileUpload" ? GetResource(RES_HEADERLASTUPLOAD) : sortDirection == "asc" ? GetResource(RES_HEADERLASTUPLOAD) + GetResource(RES_DOWNARROW) : GetResource(RES_HEADERLASTUPLOAD) + GetResource(RES_UPARROW);
                    header3.Click += new EventHandler(sortLastFileUpload_Click);
                    LinkButton header4 = (LinkButton)e.Item.FindControl("header4");
                    header4.Text = sortColumn != "Status" ? GetResource(RES_HEADERSTATUS) : sortDirection == "asc" ? GetResource(RES_HEADERSTATUS) + GetResource(RES_DOWNARROW) : GetResource(RES_HEADERSTATUS) + GetResource(RES_UPARROW);
                    header4.Click += new EventHandler(sortStatus_Click);
                    Label header5 = (Label)e.Item.FindControl("header5");
                    header5.Text = GetResource(RES_HEADERSELECT);
                    break;
                case ListItemType.AlternatingItem:
                case ListItemType.Item:
                    // Colour row to get alternating colours
                    if (e.Item.ItemType != ListItemType.AlternatingItem)
                    {
                        backColourClass = "cellBlue";
                    }

                    // Get the data record
                    System.Data.Common.DbDataRecord record = (System.Data.Common.DbDataRecord)e.Item.DataItem;
                    string temp;

                    HtmlTableCell cell1 = (HtmlTableCell)e.Item.FindControl("cell1");
                    cell1.InnerText = record["EmailAddress"].ToString();
                    cell1.Attributes["class"] = backColourClass;
                    HtmlTableCell cell2 = (HtmlTableCell)e.Item.FindControl("cell2");
                    temp = record["StatusChanged"].ToString();
                    cell2.InnerText = string.IsNullOrEmpty(temp) ? "-- -- --" : temp;
                    cell2.Attributes["class"] = backColourClass;
                    HtmlTableCell cell3 = (HtmlTableCell)e.Item.FindControl("cell3");
                    temp = record["QueuedDateTime"].ToString();
                    cell3.InnerText = string.IsNullOrEmpty(temp) ? "-- -- --" : temp;
                    cell3.Attributes["class"] = backColourClass;
                    HtmlTableCell cell4 = (HtmlTableCell)e.Item.FindControl("cell4");
                    cell4.InnerText = record["UserStatusDescription"].ToString();
                    cell4.Attributes["class"] = backColourClass;
                    HtmlTableCell cell5 = (HtmlTableCell)e.Item.FindControl("cell5");
                    cell5.Attributes["class"] = backColourClass;

                    // Add javascript to radio
                    RadioButton radio = (RadioButton)e.Item.FindControl("radioSelect");
                    string script = "SetRadioButton('usersRepeater.*radioSelect',this)";
                    radio.Attributes.Add("onclick", script);
                    break;
            }
        }

        /// <summary>
        /// Changes the sort column / direction
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void sortUser_Click(object sender, EventArgs e)
        {
            pageNumber = 1;

            if (sortColumn == "User")
            {
                sortDirection = sortDirection.Equals("desc") ? "asc" : "desc";
            }
            else
            {
                sortColumn = "User";
                sortDirection = "asc";
            }

            if (!sortDone)
            {
                PopulateUsersTable();
            }
            sortDone = true;
        }

        /// <summary>
        /// Changes the sort column / direction
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void sortStatusChange_Click(object sender, EventArgs e)
        {
            pageNumber = 1;

            if (sortColumn == "StatusChange")
            {
                sortDirection = sortDirection.Equals("desc") ? "asc" : "desc";
            }
            else
            {
                sortColumn = "StatusChange";
                sortDirection = "asc";
            }

            if (!sortDone)
            {
                PopulateUsersTable();
            }
            sortDone = true;
        }

        /// <summary>
        /// Changes the sort column / direction
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void sortLastFileUpload_Click(object sender, EventArgs e)
        {
            pageNumber = 1;

            if (sortColumn == "LastFileUpload")
            {
                sortDirection = sortDirection.Equals("desc") ? "asc" : "desc";
            }
            else
            {
                sortColumn = "LastFileUpload";
                sortDirection = "asc";
            }

            if (!sortDone)
            {
                PopulateUsersTable();
            }
            sortDone = true;
        }

        /// <summary>
        /// Changes the sort column / direction
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void sortStatus_Click(object sender, EventArgs e)
        {
            pageNumber = 1;

            if (sortColumn == "Status")
            {
                sortDirection = sortDirection.Equals("desc") ? "asc" : "desc";
            }
            else
            {
                sortColumn = "Status";
                sortDirection = "asc";
            }

            if (!sortDone)
            {
                PopulateUsersTable();
            }
            sortDone = true;
        }

        /// <summary>
        /// Changes the user list page
        /// </summary>
        /// <param name="sender">sender </param>
        /// <param name="e">event args</param>
        protected void linkFirst_Click(object sender, EventArgs e)
        {
            pageNumber = 1;
            PopulateUsersTable();
        }

        /// <summary>
        /// Changes the user list page
        /// </summary>
        /// <param name="sender">sender </param>
        /// <param name="e">event args</param>
        protected void linkPrevious_Click(object sender, EventArgs e)
        {
            if (pageNumber > 1)
            {
                pageNumber--;
                PopulateUsersTable();
            }
        }

        /// <summary>
        /// Changes the user list page
        /// </summary>
        /// <param name="sender">sender </param>
        /// <param name="e">event args</param>
        protected void linkNext_Click(object sender, EventArgs e)
        {
            if (pageNumber < totalPages)
            {
                pageNumber++;
                PopulateUsersTable();
            }
        }

        /// <summary>
        /// Changes the user list page
        /// </summary>
        /// <param name="sender">sender </param>
        /// <param name="e">event args</param>
        protected void linkLast_Click(object sender, EventArgs e)
        {
            pageNumber = totalPages;
            PopulateUsersTable();
        }

        /// <summary>
        /// Reloads the user list 
        /// </summary>
        /// <param name="sender">sender </param>
        /// <param name="e">event args</param>
        protected void buttonReloadTable_Click(object sender, EventArgs e)
        {
            PopulateUsersTable();
        }

        /// <summary>
        /// Suspends the selected user
        /// </summary>
        /// <param name="sender">sender </param>
        /// <param name="e">event args</param>
        protected void buttonSuspend_Click(object sender, EventArgs e)
        {
            // Get the selected item in the list
            string emailAddress = string.Empty;

            foreach (RepeaterItem item in usersRepeater.Items)
            {
                RadioButton radio = (RadioButton)item.FindControl("radioSelect");

                if (radio.Checked)
                {
                    HtmlTableCell cell1 = (HtmlTableCell)item.FindControl("cell1");
                    emailAddress = cell1.InnerText;
                }
            }

            if (emailAddress == string.Empty)
            {
                panelErrorMessage.Visible = true;
                labelErrorMessages.Text = GetResource(RES_SELECTUSER);
            }
            else if (GetUserStatus(emailAddress) == (int)TransportDirect.CommonWeb.Batch.BatchUserStatus.Suspended)
            {
                panelErrorMessage.Visible = true;
                labelErrorMessages.Text = string.Format(GetResource(RES_USERALREADYSUSPENDED), emailAddress);
            }
            else
            {
                UpdateUserStatus(emailAddress, (int)TransportDirect.CommonWeb.Batch.BatchUserStatus.Suspended);
                panelErrorMessage.Visible = true;
                labelErrorMessages.Text = string.Format(GetResource(RES_USERSUSPENDED), emailAddress);
            }

            PopulateUsersTable();
        }

        /// <summary>
        /// Activates the selected user
        /// </summary>
        /// <param name="sender">sender </param>
        /// <param name="e">event args</param>
        protected void buttonActivate_Click(object sender, EventArgs e)
        {
            // Get the selected item in the list
            string emailAddress = string.Empty;

            foreach (RepeaterItem item in usersRepeater.Items)
            {
                RadioButton radio = (RadioButton)item.FindControl("radioSelect");

                if (radio.Checked)
                {
                    HtmlTableCell cell1 = (HtmlTableCell)item.FindControl("cell1");
                    emailAddress = cell1.InnerText;
                }
            }

            if (emailAddress == string.Empty)
            {
                panelErrorMessage.Visible = true;
                labelErrorMessages.Text = GetResource(RES_SELECTUSER);
            }
            else if (GetUserStatus(emailAddress) == (int)TransportDirect.CommonWeb.Batch.BatchUserStatus.Active)
            {
                panelErrorMessage.Visible = true;
                labelErrorMessages.Text = string.Format(GetResource(RES_USERALREADYACTIVATED), emailAddress);
            }
            else
            {
                UpdateUserStatus(emailAddress, (int)TransportDirect.CommonWeb.Batch.BatchUserStatus.Active);
                panelErrorMessage.Visible = true;
                labelErrorMessages.Text = string.Format(GetResource(RES_USERACTIVATED), emailAddress);
            }

            PopulateUsersTable();
        }

        #endregion

        #region private methods

        /// <summary>
        /// Load the content values
        /// </summary>
        private void LoadResources()
        {
            labelPageTitle.Text = GetResource(RES_TITLE);
            labelUsers.Text = GetResource(RES_USERSLABEL);
            labelTableUsers.Text = GetResource(RES_USERSTABLELABEL);
            buttonReloadTable.Text = GetResource(RES_BUTTONRELOAD);
            buttonSuspend.Text = GetResource(RES_BUTTONSUSPEND);
            buttonActivate.Text = GetResource(RES_BUTTONACTIVATE);
            imageBatchJourneyPlanner.ImageUrl = GetResource(RES_IMAGE);
        }

        /// <summary>
        /// Populates the user list
        /// </summary>
        private void PopulateUsersTable()
        {
            this.usersRepeater.ItemDataBound += new System.Web.UI.WebControls.RepeaterItemEventHandler(this.usersRepeater_ItemDatabound);

            // Create hashtables containing parameters and data types for the stored procs
            Hashtable parameterValues = new Hashtable(3);
            Hashtable parameterTypes = new Hashtable(3);

            // page number
            parameterValues.Add("@Page", pageNumber);
            parameterTypes.Add("@Page", SqlDbType.Int);

            // sort column
            parameterValues.Add("@SortColumn", sortColumn);
            parameterTypes.Add("@SortColumn", SqlDbType.NVarChar);

            // sort direction
            parameterValues.Add("@SortDirection", sortDirection);
            parameterTypes.Add("@SortDirection", SqlDbType.NVarChar);

            // The RETURN VALUE
            SqlParameter paramReturnValue = new SqlParameter("RETURN_VALUE", SqlDbType.Int);
            paramReturnValue.Direction = ParameterDirection.ReturnValue;

            //Use the SQL Helper class
            using (SqlHelper sqlHelper = new SqlHelper())
            {
                sqlHelper.ConnOpen(SqlHelperDatabase.BatchJourneyPlannerDB);
                SqlDataReader reader = sqlHelper.GetReader("GetUsers", parameterValues, parameterTypes, paramReturnValue);
                usersRepeater.DataSource = reader;
                usersRepeater.DataBind();
                reader.Close();

                // Set up the table nav control
                decimal pages = ((decimal)((int)paramReturnValue.Value)) / 20;
                totalPages = (int)System.Math.Ceiling(pages);
                if (totalPages == 0) totalPages = 1;
                string navText = " | Page " + pageNumber + " of " + totalPages.ToString() + " | ";
                labelNavControls.Text = navText;

                sqlHelper.ConnClose();
            }

            // store table info in viewstate
            ViewState["pageNumber"] = pageNumber;
            ViewState["sortColumnAdmin"] = sortColumn;
            ViewState["sortDirectionAdmin"] = sortDirection;
        }

        /// <summary>
        /// Calls the db to update the user's status
        /// </summary>
        /// <param name="emailAddress">the user</param>
        /// <param name="statusId">the status to set to</param>
        private void UpdateUserStatus(string emailAddress, int statusId)
        {
            if (emailAddress == string.Empty)
            {
                panelErrorMessage.Visible = true;
                labelErrorMessages.Text = GetResource(RES_SELECTUSER);
            }
            else
            {
                // Update the user
                // Create hashtables containing parameters and data types for the stored procs
                Hashtable parameterValues = new Hashtable(2);
                Hashtable parameterTypes = new Hashtable(2);

                // email address
                parameterValues.Add("@EmailAddress", emailAddress);
                parameterTypes.Add("@EmailAddress", SqlDbType.NVarChar);

                // new status
                parameterValues.Add("@StatusId", statusId);
                parameterTypes.Add("@StatusId", SqlDbType.Int);

                //Use the SQL Helper class
                using (SqlHelper sqlHelper = new SqlHelper())
                {
                    sqlHelper.ConnOpen(SqlHelperDatabase.BatchJourneyPlannerDB);
                    SqlDataReader reader = sqlHelper.GetReader("SetUserStatus", parameterValues, parameterTypes);
                    usersRepeater.DataSource = reader;
                    usersRepeater.DataBind();
                    reader.Close();
                    sqlHelper.ConnClose();
                }

                // Send an email to the user
                if (statusId == (int)TransportDirect.CommonWeb.Batch.BatchUserStatus.Active)
                {
                    //Exception message
                    string msg = "Emailing the newly-registered batch journey planner user failed ";

                    try
                    {
                        // Compose email details
                        string emailFromAddress = GetResource(RES_EMAILFROM);
                        string emailBody = GetResource(RES_ACTIVATEDEMAILBODY);
                        //string emailBody = string.Format(GetResource(RES_ACTIVATEDEMAILBODY), textBoxFirstName.Text, textBoxLastName.Text, textBoxProposedUse.Text, "TODO company name");
                        string emailSubject = GetResource(RES_ACTIVATEDEMAILTITLE);

                        //Create Custom Event
                        CustomEmailEvent mailattachmentJourneyDetailEvent =
                            new CustomEmailEvent(emailFromAddress, emailAddress, emailBody, emailSubject);
                        //Add to listener
                        Logger.Write(mailattachmentJourneyDetailEvent);
                    }
                    catch (ArgumentNullException argumentNullException)
                    {
                        // log exception and re-throw TDException
                        OperationalEvent operationalEvent = new OperationalEvent
                            (TDEventCategory.Business, TDTraceLevel.Error, msg);

                        throw new TDException(msg, argumentNullException, true, TDExceptionIdentifier.BJPSendEmailFailed);
                    }
                    catch (TDException tde)
                    {
                        // log exception and re-throw TDException
                        OperationalEvent operationalEvent = new OperationalEvent
                            (TDEventCategory.Business, TDTraceLevel.Error, msg);

                        throw new TDException(msg, tde, true, TDExceptionIdentifier.BJPSendEmailFailed);
                    }
                    catch (Exception exception)
                    {
                        // log exception and re-throw TDException
                        OperationalEvent operationalEvent = new OperationalEvent
                            (TDEventCategory.Business, TDTraceLevel.Error, msg);

                        throw new TDException(msg, exception, true, TDExceptionIdentifier.BJPSendEmailFailed);
                    }
                }
            }
        }

        /// <summary>
        /// Gets the user's batch registration status
        /// </summary>
        /// <returns>User's reg status (0 - not reg'd, 1 - pending, 2 - active, 3 - suspended</returns>
        private int GetUserStatus(string emailAddress)
        {
            // Create hashtables containing parameters and data types for the stored procs
            Hashtable parameterValues = new Hashtable(1);
            Hashtable parameterTypes = new Hashtable(1);

            // The Username
            parameterValues.Add("@EmailAddress", emailAddress);
            parameterTypes.Add("@EmailAddress", SqlDbType.NVarChar);

            // The RETURN VALUE
            SqlParameter paramReturnValue = new SqlParameter("RETURN_VALUE", SqlDbType.Int);
            paramReturnValue.Direction = ParameterDirection.ReturnValue;

            int storedProcedureReturnValue = 0;

            try
            {
                //Use the SQL Helper class
                using (SqlHelper sqlHelper = new SqlHelper())
                {
                    sqlHelper.ConnOpen(SqlHelperDatabase.BatchJourneyPlannerDB);
                    sqlHelper.Execute("GetUserStatus", parameterValues, parameterTypes, paramReturnValue);
                    
                    // Get the return value
                    storedProcedureReturnValue = (int)paramReturnValue.Value;

                    sqlHelper.ConnClose();
                }
            }
            catch (SqlException sqlException)
            {
                // Log the SQL Exception
                OperationalEvent operationalEvent = new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, ".TDUserProfile.Persist.SQLException. SQL Procedure = '" + sqlException.Procedure + "' Message = " + sqlException.Message);
                Logger.Write(operationalEvent);

            }
            catch (Exception exception)
            {
                // Log the general exception
                OperationalEvent operationalEvent = new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, ".TDUserProfile.Persist.Exception. Message = " + exception.Message);
                Logger.Write(operationalEvent);
            }

            return (storedProcedureReturnValue);
        }

        #endregion
    }
}
