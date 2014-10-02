// *********************************************** 
// NAME                 : BatchJourneyPlanner.aspx.cs 
// AUTHOR               : David Lane 
// DATE CREATED         : 28/02/2012 
// DESCRIPTION			: Page for registering for and submitting batches of journey plans.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/BatchJourneyPlanner/BatchJourneyPlanner.aspx.cs-arc  $
//
//   Rev 1.23   May 23 2013 17:45:24   DLane
//Further time validation
//Resolution for 5933: Batch - times with colons in them cause the service to stall
//
//   Rev 1.22   May 21 2013 17:03:50   dlane
//Defensive code to stop times containing colons causing batches to stall
//Resolution for 5933: Batch - times with colons in them cause the service to stall
//
//   Rev 1.21   Apr 26 2013 16:47:06   RBroddle
//Updated references to BatchUserStatus to be fully qualified to resolve "ambiguous reference" errors from build process.
//
//   Rev 1.20   Apr 03 2013 18:51:06   DLane
//Empty row fix
//Resolution for 5917: Batch empty row issue
//
//   Rev 1.19   Apr 02 2013 14:57:42   dlane
//Queued position and missing image fixes
//Resolution for 5913: Batch queued position fix
//Resolution for 5914: Batch image missing when batch UI disabled
//
//   Rev 1.18   Mar 28 2013 11:39:54   DLane
//Fix to connection string use
//Resolution for 5910: Batch connection string issue
//
//   Rev 1.17   Mar 22 2013 10:49:00   dlane
//CCN0648c - Super Batch enhancements
//Resolution for 5907: CCN0648c - "Super Batch" enhancements
//
//   Rev 1.16   Oct 31 2012 14:18:56   dlane
//Welsh translations
//Resolution for 5866: Welsh text for translation
//
//   Rev 1.15   Aug 30 2012 16:45:18   DLane
//Batch phase 2
//Resolution for 5831: CCN648b - Batch phase 2
//
//   Rev 1.14   Aug 28 2012 17:20:44   dlane
//Batch phase 2 updates - first checkin
//Resolution for 5831: CCN667 - Batch phase 2
//
//   Rev 1.13   Jul 31 2012 14:02:26   PScott
//IR5825 - default page count to 1 if zero
//
//   Rev 1.12   May 14 2012 15:56:28   DLane
//Batch stored procedure updates
//Resolution for 5809: Batch - update number of download requests
//
//   Rev 1.11   Apr 13 2012 10:45:18   DLane
//Fixed issue with return journeys being missing
//Resolution for 5787: Batch Journey Planner
//
//   Rev 1.10   Apr 10 2012 12:04:20   dlane
//Compensating for excel removing leading zeroes
//Resolution for 5787: Batch Journey Planner
//
//   Rev 1.9   Mar 20 2012 16:30:00   DLane
//Fix to add javascript registration
//Resolution for 5787: Batch Journey Planner
//
//   Rev 1.8   Mar 15 2012 17:39:06   DLane
//Various batch updates
//Resolution for 5787: Batch Journey Planner
//
//   Rev 1.7   Mar 02 2012 11:27:22   DLane
//Batch updates
//Resolution for 5787: Batch Journey Planner
//
//   Rev 1.6   Feb 28 2012 15:12:28   DLane
//Javascript and code updates
//Resolution for 5787: Batch Journey Planner
//
//   Rev 1.5   Feb 28 2012 14:41:56   DLane
//Adding comments + updates
//Resolution for 5787: Batch Journey Planner
//

using System;
using System.Collections.Generic;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Logger = System.Diagnostics.Trace;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Xsl;
using ICSharpCode.SharpZipLib.Zip;
using TransportDirect.Common;
using TransportDirect.Common.DatabaseInfrastructure;
using TransportDirect.Common.Logging;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.ServiceDiscovery;
using TransportDirect.UserPortal.DataServices;
using TransportDirect.UserPortal.LocationService;
using TransportDirect.UserPortal.Resource;
using TransportDirect.UserPortal.SessionManager;
using TransportDirect.UserPortal.Web.Adapters;
using System.Text;
using System.Net;
using TransportDirect.CommonWeb.Helpers;
using TransportDirect.CommonWeb.Batch;

namespace TransportDirect.UserPortal.Web.BatchJourneyPlanner
{
    public partial class BatchJourneyPlanner : TDPage
    {
        #region Constants and vars
        private const string RES_JPIMAGE = "BatchJourneyPlanner.ImageUrl";
        private const string RES_PAGETITLE = "BatchJourneyPlanner.PageTitle";
        private const string RES_WHATIS = "BatchJourneyPlanner.WhatIs";
        private const string RES_APPROVED = "BatchJourneyPlanner.AlreadyApproved";
        private const string RES_HOWREG = "BatchJourneyPlanner.HowRegister";
        private const string RES_WHATISSC = "BatchJourneyPlanner.WhatIs.SC";
        private const string RES_APPROVEDSC = "BatchJourneyPlanner.AlreadyApproved.SC";
        private const string RES_HOWREGSC = "BatchJourneyPlanner.HowRegister.SC";
        private const string RES_REGDAS = "BatchJourneyPlanner.RegdAs";
        private const string RES_FIRSTNAME = "BatchJourneyPlanner.FirstName";
        private const string RES_LASTNAME = "BatchJourneyPlanner.LastName";
        private const string RES_PHONE = "BatchJourneyPlanner.Phone";
        private const string RES_PROPOSEDUSE = "BatchJourneyPlanner.ProposedUse";
        private const string RES_MANDATORY = "BatchJourneyPlanner.Mandatory";
        private const string RES_EMAILSTEXT = "BatchJourneyPlanner.EmailsText";
        private const string RES_MOREINFO = "BatchJourneyPlanner.MoreInfo";
        private const string RES_UKMAP = "BatchJourneyPlanner.UKMap";
        private const string RES_TERMS = "BatchJourneyPlanner.Terms";
        private const string RES_CHKTERMS = "BatchJourneyPlanner.ChkTerms";
        private const string RES_REGBUTTON = "BatchJourneyPlanner.RegButton";
        private const string RES_NOFIRSTNAME = "BatchJourneyPlanner.NoFirstName";
        private const string RES_NOLASTNAME = "BatchJourneyPlanner.NoLastName";
        private const string RES_NOPHONE = "BatchJourneyPlanner.NoPhone";
        private const string RES_NOUSAGE = "BatchJourneyPlanner.NoUsage";
        private const string RES_BADRECAPTCHA = "BatchJourneyPlanner.BadRecaptcha";
        private const string RES_NOTERMS = "BatchJourneyPlanner.NoTerms";
        private const string RES_CONFIRMATION = "BatchJourneyPlanner.Confirmation";
        private const string RES_EMAILFROM = "BatchJourneyPlanner.EmailFrom";
        private const string RES_REGEMAILBODY = "BatchJourneyPlanner.RegEmailBody";
        private const string RES_REGEMAILTITLE = "BatchJourneyPlanner.RegEmailTitle";
        private const string RES_DFTEMAILBODY = "BatchJourneyPlanner.DftEmailBody";
        private const string RES_DFTEMAILTITLE = "BatchJourneyPlanner.DftEmailTitle";
        private const string RES_DFTEMAILADDRESS = "BatchJourneyPlanner.DftEmailAddress";

        private const string RES_UPLOAD = "BatchJourneyPlanner.Upload";
        private const string RES_UPLOADINSTRUCTIONS = "BatchJourneyPlanner.UploadInstructions";
        private const string RES_OUTPUT = "BatchJourneyPlanner.Output";
        private const string RES_CHECKSTATS = "BatchJourneyPlanner.CheckStats";
        private const string RES_CHECKDETAILS = "BatchJourneyPlanner.CheckDetails";
        private const string RES_TYPES = "BatchJourneyPlanner.Types";
        private const string RES_CHECKPUBLIC = "BatchJourneyPlanner.CheckPublic";
        private const string RES_CHECKCAR = "BatchJourneyPlanner.CheckCar";
        private const string RES_CHECKCYCLE = "BatchJourneyPlanner.CheckCycle";
        private const string RES_FORMAT = "BatchJourneyPlanner.Format";

        private const string RES_TEMPLATE = "BatchJourneyPlanner.Template";
        private const string RES_TEMPLATEDESC = "BatchJourneyPlanner.TemplateDesc";
        private const string RES_LINKTEMPLATE = "BatchJourneyPlanner.LinkTemplate";
        private const string RES_LINKTEMPLATEDESC = "BatchJourneyPlanner.LinkTemplateDesc";
        private const string RES_BUTTONLOADFILE = "BatchJourneyPlanner.ButtonLoadFile";
        private const string RES_WRONGFILETYPE = "BatchJourneyPlanner.WrongFileType";
        private const string RES_NOFILESELECTED = "BatchJourneyPlanner.NoFileSelected";
        private const string RES_FILEUPLOADFAILED = "BatchJourneyPlanner.FileUploadFailed";
        private const string RES_EMPTYFILE = "BatchJourneyPlanner.EmptyFile";
        private const string RES_WRONGHEADER = "BatchJourneyPlanner.WrongHeader";
        private const string RES_NODETAILROWS = "BatchJourneyPlanner.NoDetailRows";
        private const string RES_STATSDETAILS = "BatchJourneyPlanner.StatsDetails";
        private const string RES_PTCARCYCLE = "BatchJourneyPlanner.PtCarCycle";
        private const string RES_XMLRTF = "BatchJourneyPlanner.XmlRtf";
        private const string RES_MAXFILELENGTH = "BatchJourneyPlanner.MaxFileLength";
        private const string RES_UPLOADSUCCESS = "BatchJourneyPlanner.UploadSuccess";
        private const string RES_EXCEEDS_USER_LIMIT = "BatchJourneyPlanner.MaxUserFileLength";
        private const string RES_EXCEEDS_DEFAULT_LIMIT = "BatchJourneyPlanner.MaxDefaultFileLength";
        private const string RES_EXCEEDS_JOURNEY_DETAILS_LIMIT = "BatchJourneyPlanner.MaxJourneyDetailsFileLength";
        private const string RES_EXCEEDS_NO_JOURNEY_DETAILS_LIMIT = "BatchJourneyPlanner.MaxNoJourneyDetailsFileLength";

        private const string RES_HEADERREQUESTID = "BatchJourneyPlanner.HeaderRequestId";
        private const string RES_HEADERSUBMITTED = "BatchJourneyPlanner.HeaderSubmitted";
        private const string RES_HEADERPUBLICTRANSPORT = "BatchJourneyPlanner.HeaderPublicTransport";
        private const string RES_HEADERCAR = "BatchJourneyPlanner.HeaderCar";
        private const string RES_HEADERCYCLE = "BatchJourneyPlanner.HeaderCycle";
        private const string RES_HEADERNUMBERREQUESTS = "BatchJourneyPlanner.HeaderNumberRequests";
        private const string RES_HEADERNUMBERRESULTS = "BatchJourneyPlanner.HeaderNumberResults";
        private const string RES_HEADERNUMBERPARTIALS = "BatchJourneyPlanner.HeaderNumberPartials";
        private const string RES_HEADERVALIDATIONERRORS = "BatchJourneyPlanner.HeaderValidationErrors";
        private const string RES_HEADERNUMBERNORESULTS = "BatchJourneyPlanner.HeaderNumberNoResults";
        private const string RES_HEADERDATECOMPLETE = "BatchJourneyPlanner.HeaderDateComplete";
        private const string RES_HEADERSTATUS = "BatchJourneyPlanner.HeaderStatus";
        private const string RES_HEADERSELECT = "BatchJourneyPlanner.HeaderSelect";
        private const string RES_RESULTSLABEL = "BatchJourneyPlanner.ResultsLabel";
        private const string RES_BATCHTABLELABEL = "BatchJourneyPlanner.BatchTableLabel";
        private const string RES_BUTTONDOWNLOAD = "BatchJourneyPlanner.ButtonDownload";
        private const string RES_BUTTONRELOAD = "BatchJourneyPlanner.ButtonReload";
        private const string RES_LABELFAQ = "BatchJourneyPlanner.LabelFaq";
        private const string RES_TICKIMAGE = "BatchJourneyPlanner.Tick";
        private const string RES_NOTLOGGEDIN = "BatchJourneyPlanner.NotLoggedIn";
        private const string RES_REGPENDING = "BatchJourneyPlanner.RegistrationPending";
        private const string RES_REGSUSPENDED = "BatchJourneyPlanner.RegistrationSuspended";
        private const string RES_UPARROW = "BatchJourneyPlanner.ArrowUp";
        private const string RES_DOWNARROW = "BatchJourneyPlanner.ArrowDown";
        private const string RES_NODATA = "BatchJourneyPlanner.NoData";

        private const string RES_PREVIOUS = "BatchJourneyPlanner.Previous";
        private const string RES_NEXT = "BatchJourneyPlanner.Next";
        private const string RES_NAVLABEL = "BatchJourneyPlanner.NavLabel";
        private const string RES_BATCHDISABLED = "BatchJourneyPlanner.BatchDisabled";
        private const string RES_NOROW = "BatchJourneyPlanner.NoRowSelected";
        private const string RES_COMPLETEDONLY = "BatchJourneyPlanner.CompletedOnly";
        private const string RES_FAILEDBATCH = "BatchJourneyPlanner.FailedBatch";
        private const string RES_BATCHDELETED = "BatchJourneyPlanner.BatchDeleted";
        private const string RES_RETRIEVALFAILURE = "BatchJourneyPlanner.RetrievalFailure";

        private string headerRequestId;
        private string headerSubmitted;
        private string headerPublicTransport;
        private string headerCar;
        private string headerCycle;
        private string headerNumberRequests;
        private string headerNumberResults;
        private string headerNumberPartials;
        private string headerValidationErrors;
        private string headerNumberNoResults;
        private string headerDateComplete;
        private string headerStatus;
        private string headerSelect;
        private string queuedPos = string.Empty;
        private string batchId = string.Empty;

        private int resultsPage;
        private string sortColumn;
        private string sortDirection;
        private int totalPages;
        private bool sortDone = false;
        #endregion

        #region constructor

        public BatchJourneyPlanner()
            : base()
        {
            pageId = PageId.BatchJourneyPlanner;
        }

        # endregion


        #region Table header accessors

        /// <summary>
        /// Accessor for RequestId header
        /// </summary>
        public string HeaderRequestId
        {
            get
            {
                return headerRequestId;
            }
        }

        /// <summary>
        /// Accessor for Submitted header
        /// </summary>
        public string HeaderSubmitted
        {
            get
            {
                return headerSubmitted;
            }
        }

        /// <summary>
        /// Accessor for PT header
        /// </summary>
        public string HeaderPublicTransport
        {
            get
            {
                return headerPublicTransport;
            }
        }

        /// <summary>
        /// Accessor for Car header
        /// </summary>
        public string HeaderCar
        {
            get
            {
                return headerCar;
            }
        }

        /// <summary>
        /// Accessor for Cycle header
        /// </summary>
        public string HeaderCycle
        {
            get
            {
                return headerCycle;
            }
        }

        /// <summary>
        /// Accessor for NumberRequests header
        /// </summary>
        public string HeaderNumberRequests
        {
            get
            {
                return headerNumberRequests;
            }
        }

        /// <summary>
        /// Accessor for NumberResults header
        /// </summary>
        public string HeaderNumberResults
        {
            get
            {
                return headerNumberResults;
            }
        }

        /// <summary>
        /// Accessor for NumberPartials header
        /// </summary>
        public string HeaderNumberPartials
        {
            get
            {
                return headerNumberPartials;
            }
        }
        
        /// <summary>
        /// Accessor for ValidationErrors header
        /// </summary>
        public string HeaderValidationErrors
        {
            get
            {
                return headerValidationErrors;
            }
        }

        /// <summary>
        /// Accessor for NoResults header
        /// </summary>
        public string HeaderNumberNoResults
        {
            get
            {
                return headerNumberNoResults;
            }
        }

        /// <summary>
        /// Accessor for DateComplete header
        /// </summary>
        public string HeaderDateComplete
        {
            get
            {
                return headerDateComplete;
            }
        }

        /// <summary>
        /// Accessor for Status header
        /// </summary>
        public string HeaderStatus
        {
            get
            {
                return headerStatus;
            }
        }

        /// <summary>
        /// Accessor for Select header
        /// </summary>
        public string HeaderSelect
        {
            get
            {
                return headerSelect;
            }
        }
        
        #endregion

        /// <summary>
        /// Page Load event handler
        /// </summary>
        /// <param name="sender">Notifying object</param>
        /// <param name="e">Event data</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            // Catch login / register parameter
            if (Request.Params["loginregister"] != null)
            {
                if (Request.Params["loginregister"].ToLower() == "true")
                {
                    // Set up session manager
                    ITDSessionManager sessionManager =
                        (ITDSessionManager)TDServiceDiscovery.Current
                        [ServiceDiscoveryKey.SessionManager];

                    // Navigate to LoginAndRegister Page
                    sessionManager.FormShift[SessionKey.TransitionEvent] = TransitionEvent.LoginRegister;

                    TDSessionManager.Current.InputPageState.JourneyInputReturnStack.Clear();
                    TDSessionManager.Current.InputPageState.JourneyInputReturnStack.Push(((TDPage)Page).PageId);
                    return;
                }
            }

            string enabled = Properties.Current["BatchProcessingUIEnabled"];
            bool bEnabled;
            if (bool.TryParse(enabled, out bEnabled))
            {
                if (bEnabled)
                {
                    // Good to go
                    LoadResources();

                    // attempt to retrieve vars from viewstate
                    totalPages = 1;
                    if (ViewState["resultsPage"] != null)
                    {
                        resultsPage = (int)ViewState["resultsPage"];
                    }
                    else
                    {
                        resultsPage = 1;
                    }
                    if (ViewState["sortColumn"] != null)
                    {
                        sortColumn = (string)ViewState["sortColumn"];
                    }
                    else
                    {
                        sortColumn = "BatchId";
                    }
                    if (ViewState["sortDirection"] != null)
                    {
                        sortDirection = (string)ViewState["sortDirection"];
                    }
                    else
                    {
                        sortDirection = "desc";
                    }

                    // return to defaults
                    panelAbout.Visible = false;
                    panelErrorMessage.Visible = false;
                    panelRegistration.Visible = false;
                    panelJourneyPlanning.Visible = false;
                    panelConfirmation.Visible = false;
                    labelMessageArea.Visible = false;

                    // Recaptcha usage
                    string property = Properties.Current["BatchJourneyPlannerUseRecaptcha"];
                    if (!bool.Parse(property))
                    {
                        recaptcha.Visible = false;
                    }

                    if (TDSessionManager.Current.Authenticated)
                    {
                        // Check if the user has completed registration
                        switch (UserIsRegistered())
                        {
                            case 0:
                                // not registered - let them reg
                                panelAbout.Visible = true;
                                panelRegistration.Visible = true;
                                break;
                            case (int)TransportDirect.CommonWeb.Batch.BatchUserStatus.Pending:
                                // pending
                                panelErrorMessage.Visible = true;
                                labelErrorMessages.Text = GetResource(RES_REGPENDING);
                                break;
                            case (int)TransportDirect.CommonWeb.Batch.BatchUserStatus.Active:
                                // active
                                panelJourneyPlanning.Visible = true;
                                PopulateBatchTable();
                                if (!Page.IsPostBack)
                                {
                                    UseUserPreferences();
                                }
                                break;
                            case (int)TransportDirect.CommonWeb.Batch.BatchUserStatus.Suspended:
                                // suspended
                                panelErrorMessage.Visible = true;
                                labelErrorMessages.Text = GetResource(RES_REGSUSPENDED);
                                break;
                        }
                    }
                    else
                    {
                        // Show an error saying the user must be logged in
                        panelErrorMessage.Visible = true;
                        labelErrorMessages.Text = GetResource(RES_NOTLOGGEDIN);
                        panelAbout.Visible = true;
                    }

                    //Added for white labelling:
                    ConfigureLeftMenu(expandableMenuControl, TransportDirect.UserPortal.SuggestionLinkService.Context.HomePageMenuTipsAndTools);
                    expandableMenuControl.AddContext(TransportDirect.UserPortal.SuggestionLinkService.Context.BatchJourneyPlanner);
                    expandableMenuControl.AddExpandedCategory("Related links");
                }
                else
                {
                    // Batch disabled
                    panelErrorMessage.Visible = true;
                    labelErrorMessages.Text = GetResource(RES_BATCHDISABLED);

                    imageBatchJourneyPlanner.ImageUrl = GetResource(RES_JPIMAGE);
                    imageBatchJourneyPlanner.AlternateText = " ";
                }
            }
            else
            {
                // Batch disabled (property missing or value invalid)
                panelErrorMessage.Visible = true;
                labelErrorMessages.Text = GetResource(RES_BATCHDISABLED);
            }
        }

        /// <summary>
        /// Page Init event handler
        /// </summary>
        /// <param name="sender">Notifying object</param>
        /// <param name="e">Event data</param>
        protected void Page_Init(object sender, EventArgs e)
        {
            // Event handlers
            this.buttonRegister.Click += new EventHandler(this.buttonRegister_Click);
            this.buttonFileLoad.Click += new EventHandler(this.buttonLoadFile_Click);
            this.linkFirst.Click += new EventHandler(this.linkFirst_Click);
            this.linkPrevious.Click += new EventHandler(this.linkPrevious_Click);
            this.linkNext.Click += new EventHandler(this.linkNext_Click);
            this.linkLast.Click += new EventHandler(this.linkLast_Click);
            this.buttonDownload.Click += new EventHandler(this.buttonDownload_Click);
            this.buttonReload.Click += new EventHandler(this.buttonReload_Click);
        }

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
        /// Gets the user's batch registration status
        /// </summary>
        /// <returns>User's reg status (0 - not reg'd, 1 - pending, 2 - active, 3 - suspended)</returns>
        private int UserIsRegistered()
        {
            // Create hashtables containing parameters and data types for the stored procs
            Hashtable parameterValues = new Hashtable(1);
            Hashtable parameterTypes = new Hashtable(1);

            // The Username
            parameterValues.Add("@EmailAddress", TDSessionManager.Current.CurrentUser.Username);
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

        /// <summary>
        /// Sets static label text for the page, including page title.
        /// </summary>
        private void LoadResources()
        {
            // General
            PageTitle = GetResource("BatchJourneyPlanner.AppendPageTitle") + GetResource("JourneyPlanner.DefaultPageTitle");
            labelPageTitle.Text = GetResource(RES_PAGETITLE);
            imageBatchJourneyPlanner.ImageUrl = GetResource(RES_JPIMAGE);
            imageBatchJourneyPlanner.AlternateText = " ";

            // Registration mode
            labelWhatIs.Text = GetResource(RES_WHATIS);
            labelWhatIsSC.Text = GetResource(RES_WHATISSC);
            labelAlreadyApproved.Text = GetResource(RES_APPROVED);
            labelAlreadyApprovedSC.Text = GetResource(RES_APPROVEDSC);
            labelHowRegister.Text = GetResource(RES_HOWREG);
            labelHowRegisterSC.Text = GetResource(RES_HOWREGSC);
            divTerms.InnerHtml = GetResource(RES_TERMS);
            chkTerms.Text = GetResource(RES_CHKTERMS);
            labelFirstName.Text = GetResource(RES_FIRSTNAME);
            labelLastName.Text = GetResource(RES_LASTNAME);
            labelPhone.Text = GetResource(RES_PHONE);
            labelProposedUse.Text = GetResource(RES_PROPOSEDUSE);
            labelMandatory.Text = GetResource(RES_MANDATORY);
            labelEmailsText.Text = GetResource(RES_EMAILSTEXT);
            labelMoreInfo.Text = GetResource(RES_MOREINFO);
            imageUKMap.ImageUrl = GetResource(RES_UKMAP);
            imageUKMap.AlternateText = " ";
            buttonRegister.Text = GetResource(RES_REGBUTTON);

            // Registered mode
            labelUpload.Text = GetResource(RES_UPLOAD);
            labelUploadInstructions.Text = GetResource(RES_UPLOADINSTRUCTIONS);
            labelOutput.Text = GetResource(RES_OUTPUT);
            chkStatistics.Text = GetResource(RES_CHECKSTATS);
            chkDetails.Text = GetResource(RES_CHECKDETAILS);
            labelTypes.Text = GetResource(RES_TYPES);
            chkPublic.Text = GetResource(RES_CHECKPUBLIC);
            chkCar.Text = GetResource(RES_CHECKCAR);
            chkCycle.Text = GetResource(RES_CHECKCYCLE);
            labelFormat.Text = GetResource(RES_FORMAT);

            labelTemplate.Text = GetResource(RES_TEMPLATE);
            labelTemplateDescription.Text = GetResource(RES_TEMPLATEDESC);
            linkTemplate.Text = GetResource(RES_LINKTEMPLATE);
            linkDescription.Text = GetResource(RES_LINKTEMPLATEDESC);
            buttonFileLoad.Text = GetResource(RES_BUTTONLOADFILE);
            buttonReload.Text = GetResource(RES_BUTTONRELOAD);
            buttonDownload.Text = GetResource(RES_BUTTONDOWNLOAD);
            labelFaq.Text = GetResource(RES_LABELFAQ);

            // Table headers
            headerRequestId = GetResource(RES_HEADERREQUESTID);
            headerSubmitted = GetResource(RES_HEADERSUBMITTED);
            headerPublicTransport = GetResource(RES_HEADERPUBLICTRANSPORT);
            headerCar = GetResource(RES_HEADERCAR);
            headerCycle = GetResource(RES_HEADERCYCLE);
            headerNumberRequests = GetResource(RES_HEADERNUMBERREQUESTS);
            headerNumberResults = GetResource(RES_HEADERNUMBERRESULTS);
            headerNumberPartials = GetResource(RES_HEADERNUMBERPARTIALS);
            headerValidationErrors = GetResource(RES_HEADERVALIDATIONERRORS);
            headerNumberNoResults = GetResource(RES_HEADERNUMBERNORESULTS);
            headerDateComplete = GetResource(RES_HEADERDATECOMPLETE);
            headerStatus = GetResource(RES_HEADERSTATUS);
            headerSelect = GetResource(RES_HEADERSELECT);

            labelResults.Text = GetResource(RES_RESULTSLABEL);

            // Table nav controls
            linkPrevious.Text = GetResource(RES_PREVIOUS);
            linkNext.Text = GetResource(RES_NEXT);

            if (TDSessionManager.Current.Authenticated)
            {
                labelRegdAs.Text = string.Format(GetResource(RES_REGDAS), TDSessionManager.Current.CurrentUser.Username);
                labelTableBatches.Text = string.Format(GetResource(RES_BATCHTABLELABEL), TDSessionManager.Current.CurrentUser.Username);
            }
        }

		/// <summary>
		/// Event handler for clicks of the Register button
		/// </summary>
        /// <param name="sender">sending object</param>
        /// <param name="e">event args</param>
        protected void buttonRegister_Click(object sender, System.EventArgs e)
        {
            // In case of refresh being done check for user's existence
            if (UserIsRegistered() > 0)
            {
                return;
            }

            // Validate the page
            bool isValid = true;
            string errors = "";

            if (textBoxFirstName.Text == "")
            {
                errors = GetResource(RES_NOFIRSTNAME);
                isValid = false;
            }

            if (textBoxLastName.Text == "")
            {
                errors += GetResource(RES_NOLASTNAME);
                isValid = false;
            }

            if (textBoxPhone.Text == "")
            {
                errors += GetResource(RES_NOPHONE);
                isValid = false;
            }
            else
            {
                // check content
                string pattern = "([0-9+()])+";
                if (!Regex.IsMatch(textBoxPhone.Text, pattern))
                {
                    errors += GetResource(RES_NOPHONE);
                    isValid = false;
                }
            }

            if (textBoxProposedUse.Text == "")
            {
                errors += GetResource(RES_NOUSAGE);
                isValid = false;
            }

            // Check the terms and conditions
            if (!chkTerms.Checked)
            {
                errors += GetResource(RES_NOTERMS);
                isValid = false;
            }

            // Recaptcha usage
            bool recaptchaValid = false;
            string property = Properties.Current["BatchJourneyPlannerUseRecaptcha"];
            if (bool.Parse(property))
            {
                OperationalEvent operationalEvent = new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, string.Format("BatchJourneyPlannerUseRecaptcha = {0}", property));
                Logger.Write(operationalEvent);
                string propertyUseProxy = Properties.Current["BatchJourneyPlannerUseProxy"];

                if (bool.Parse(propertyUseProxy))
                {
                    operationalEvent = new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, string.Format("BatchJourneyPlannerUseProxy = {0}", propertyUseProxy));
                    Logger.Write(operationalEvent);
                    string challenge = Request.Form["recaptcha_challenge_field"];
                    string clientResponse = Request.Form["recaptcha_response_field"];
                    string post = "privatekey=" + HttpUtility.UrlEncode(recaptcha.PrivateKey) +
                        "&remoteip=" + HttpUtility.UrlEncode(Request.UserHostAddress) + "&challenge=" +
                        HttpUtility.UrlEncode(challenge) + "&response=" +
                        HttpUtility.UrlEncode(clientResponse);

                    WebRequest wr = HttpWebRequest.Create("http://www.google.com/recaptcha/api/verify");
                    wr.Method = "POST";

                    string propertyProxyUrl = Properties.Current["BatchJourneyPlannerProxyUrl"];
                    operationalEvent = new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, string.Format("BatchJourneyPlannerProxyUrl = {0}", propertyProxyUrl));
                    Logger.Write(operationalEvent);
                    System.Net.WebProxy proxy = new System.Net.WebProxy(propertyProxyUrl);
                    proxy.UseDefaultCredentials = true;
                    proxy.BypassProxyOnLocal = false;
                    wr.Proxy = proxy;

                    operationalEvent = new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, string.Format("Recaptcha post = {0}", post));
                    Logger.Write(operationalEvent);
                    wr.ContentLength = post.Length;
                    wr.ContentType = "application/x-www-form-urlencoded";
                    using (StreamWriter sw = new StreamWriter(wr.GetRequestStream()))
                    {
                        sw.Write(post);
                        sw.Close();
                    }

                    HttpWebResponse resp = (HttpWebResponse)wr.GetResponse();
                    using (StreamReader sr = new StreamReader(resp.GetResponseStream()))
                    {
                        string response = sr.ReadLine();
                        if (response != null)
                        {
                            operationalEvent = new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, string.Format("Recaptcha response = {0}", response));
                            Logger.Write(operationalEvent);
                            if (response.ToLower().Trim() == "true")
                            {
                                recaptchaValid = true;
                            }
                        }

                        sr.Close();
                    }
                }
                else
                {
                    Page.Validate();
                    recaptcha.Validate();
                    recaptchaValid = recaptcha.IsValid;
                }
            }
            else
            {
                // Always be true if not validating recaptcha
                recaptchaValid = true;
            }

            if(!recaptchaValid)
            {
                errors += GetResource(RES_BADRECAPTCHA);
                isValid = false;
            }

            if (!isValid)
            {
                errors += "<p>&nbsp;</p>";
                panelRegistrationError.Visible = true;
                labelRegistrationError.Text = errors;
            }
            else
            {
                // Create hashtables containing parameters and data types for the stored procs
                Hashtable parameterValues = new Hashtable(1);
                Hashtable parameterTypes = new Hashtable(1);

                // The Username
                parameterValues.Add("@EmailAddress", TDSessionManager.Current.CurrentUser.Username);
                parameterTypes.Add("@EmailAddress", SqlDbType.NVarChar);

                try
                {
                    //Use the SQL Helper class
                    using (SqlHelper sqlHelper = new SqlHelper())
                    {
                        // Add the user
                        sqlHelper.ConnOpen(SqlHelperDatabase.BatchJourneyPlannerDB);
                        sqlHelper.Execute("AddNewUser", parameterValues, parameterTypes);
                        sqlHelper.ConnClose();
                    }
                }
                catch (SqlException sqlException)
                {
                    // Log the SQL Exception
                    OperationalEvent operationalEvent = new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, ".TDUserProfile.Persist.SQLException. SQL Procedure = '" + sqlException.Procedure + "' Message = " + sqlException.Message);
                    Logger.Write(operationalEvent);
                    throw new TDException("Database call to register failed", sqlException, true, TDExceptionIdentifier.BJPRegisterFailed);
                }
                catch (Exception exception)
                {
                    // Log the general exception
                    OperationalEvent operationalEvent = new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, ".TDUserProfile.Persist.Exception. Message = " + exception.Message);
                    Logger.Write(operationalEvent);
                    throw new TDException("Database call to register failed", exception, true, TDExceptionIdentifier.BJPRegisterFailed);
                }

                // Sort out emails
                //Exception message
                string msg = "Emailing the newly-registered batch journey planner user failed ";

                try
                {
                    // Compose email details
                    string emailFromAddress = GetResource(RES_EMAILFROM);
                    string emailToAddress = TDSessionManager.Current.CurrentUser.Username;
                    string emailBody = string.Format(GetResource(RES_REGEMAILBODY), textBoxFirstName.Text, textBoxLastName.Text, textBoxProposedUse.Text, "TODO company name");
                    string emailSubject = GetResource(RES_REGEMAILTITLE);

                    //Create Custom Event
                    CustomEmailEvent mailattachmentJourneyDetailEvent =
                        new CustomEmailEvent(emailFromAddress, emailToAddress, emailBody, emailSubject);
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

                msg = "Emailing the DfT about a newly-registered batch journey planner user failed";

                try
                {
                    // Compose email details
                    string emailFromAddress = GetResource(RES_EMAILFROM);
                    string emailToAddress = GetResource(RES_DFTEMAILADDRESS);
                    string emailBody = string.Format(GetResource(RES_DFTEMAILBODY), textBoxFirstName.Text, textBoxLastName.Text, "TODO company name", textBoxPhone.Text, TDSessionManager.Current.CurrentUser.Username, textBoxProposedUse.Text, DateTime.Now.ToString());
                    string emailSubject = GetResource(RES_DFTEMAILTITLE);

                    //Create Custom Event
                    CustomEmailEvent mailattachmentJourneyDetailEvent =
                        new CustomEmailEvent(emailFromAddress, emailToAddress, emailBody, emailSubject);
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

                // Show the confirmation message
                panelConfirmation.Visible = true;
                panelRegistration.Visible = false;
                panelAbout.Visible = false;
                labelConfirmation.Text = GetResource(RES_CONFIRMATION);
            }
        }

        /// <summary>
        /// Handler for the load file click event
        /// </summary>
        /// <param name="sender">sending object</param>
        /// <param name="e">event args</param>
        protected void buttonLoadFile_Click(object sender, EventArgs args)
        {
            // Check for minimum input requirement
            bool insufficient = false;
            string error = string.Empty;

            // Upload the file if it is specified
            if (!(fileUpload.FileName.Length > 0))
            {
                insufficient = true;
                error = GetResource(RES_NOFILESELECTED);
            }
            else
            {
                // Check it's a csv
                if (!fileUpload.PostedFile.FileName.EndsWith(".csv"))
                {
                    insufficient = true;
                    error = GetResource(RES_WRONGFILETYPE);
                }
            }

            // Check stats / details
            if (!chkStatistics.Checked && !chkDetails.Checked)
            {
                insufficient = true;
                error += GetResource(RES_STATSDETAILS);
            }

            if (!chkPublic.Checked && !chkCar.Checked && !chkCycle.Checked)
            {
                insufficient = true;
                error += GetResource(RES_PTCARCYCLE);
            }

            if (radioListFormat.SelectedIndex == -1)
            {
                if (chkPublic.Checked && chkDetails.Checked)
                {
                    insufficient = true;
                    error += GetResource(RES_XMLRTF);
                }
            }

            if (insufficient)
            {
                panelErrorMessage.Visible = true;
                labelErrorMessages.Text = error;
            }
            else
            {
                try
                {
                    // grab it
                    Stream theStream = fileUpload.PostedFile.InputStream;
                    StreamReader reader = new StreamReader(theStream);

                    // Check there is content
                    if (reader.EndOfStream)
                    {
                        // empty file
                        panelErrorMessage.Visible = true;
                        labelErrorMessages.Text = GetResource(RES_EMPTYFILE);
                        return;
                    }

                    // check the header row
                    string line = reader.ReadLine();
                    if (line.Trim() != "JourneyID,OriginType,Origin,DestinationType,Destination,OutwardDate,OutwardTime,OutwardArrDep,ReturnDate,ReturnTime,ReturnArrDep")
                    {
                        // header row wrong
                        panelErrorMessage.Visible = true;
                        labelErrorMessages.Text = GetResource(RES_WRONGHEADER);
                        return;
                    }

                    // Check there are more rows
                    if (reader.EndOfStream)
                    {
                        // no body rows
                        panelErrorMessage.Visible = true;
                        labelErrorMessages.Text = GetResource(RES_NODETAILROWS);
                        return;
                    }

                    // Process the file
                    if (!ProcessFileUpload(reader))
                    {
                        // too many lines
                        panelErrorMessage.Visible = true;
                        return;
                    }

                    SavePreferences();

                    PopulateBatchTable();

                    // success - report to the user
                    labelMessageArea.Text = string.Format(GetResource(RES_UPLOADSUCCESS), batchId, queuedPos);
                    labelMessageArea.Visible = true;
                }
                catch (Exception ex)
                {
                    // log exception and display an error message
                    OperationalEvent operationalEvent = new OperationalEvent
                        (TDEventCategory.Business, TDTraceLevel.Error, ex.ToString());

                    panelErrorMessage.Visible = true;
                    labelErrorMessages.Text = GetResource(RES_FILEUPLOADFAILED);
                }
            }
        }

        /// <summary>
        /// Save's the user's checkbox and radio preferences
        /// </summary>
        private void SavePreferences()
        {
            // Record the checkbox and radio options in the user's preferences
            // Create hashtables containing parameters and data types for the stored procs
            Hashtable parameterValues = new Hashtable(7);
            Hashtable parameterTypes = new Hashtable(7);

            // The user id
            parameterValues.Add("@EmailAddress", TDSessionManager.Current.CurrentUser.Username);
            parameterTypes.Add("@EmailAddress", SqlDbType.NVarChar);

            // Stats
            parameterValues.Add("@Statistics", chkStatistics.Checked ? 1 : 0);
            parameterTypes.Add("@Statistics", SqlDbType.Bit);

            // Details
            parameterValues.Add("@Details", chkDetails.Checked ? 1 : 0);
            parameterTypes.Add("@Details", SqlDbType.Bit);

            // Inc PT
            parameterValues.Add("@PublicTransport", chkPublic.Checked ? 1 : 0);
            parameterTypes.Add("@PublicTransport", SqlDbType.Bit);

            // Inc Car
            parameterValues.Add("@Car", chkCar.Checked ? 1 : 0);
            parameterTypes.Add("@Car", SqlDbType.Bit);

            // Inc cycle
            parameterValues.Add("@Cycle", chkCycle.Checked ? 1 : 0);
            parameterTypes.Add("@Cycle", SqlDbType.Bit);

            // RTF
            parameterValues.Add("@Rtf", radioListFormat.SelectedIndex == 0 ? 1 : 0);
            parameterTypes.Add("@Rtf", SqlDbType.Bit);

            try
            {
                //Use the SQL Helper class
                using (SqlHelper sqlHelper = new SqlHelper())
                {
                    sqlHelper.ConnOpen(SqlHelperDatabase.BatchJourneyPlannerDB);
                    sqlHelper.Execute("SetUserPreferences", parameterValues, parameterTypes);
                    sqlHelper.ConnClose();
                }
            }
            catch (Exception exception)
            {
                // Log and swallow the general exception
                OperationalEvent operationalEvent = new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, ".TDUserProfile.Persist.Exception. Message = " + exception.Message);
                Logger.Write(operationalEvent);
            }
        }

        /// <summary>
        /// Retrieve and use the user's preferences
        /// </summary>
        private void UseUserPreferences()
        {
            // Record the checkbox and radio options in the user's preferences
            // Create hashtables containing parameters and data types for the stored procs
            Hashtable parameterValues = new Hashtable(1);
            Hashtable parameterTypes = new Hashtable(1);

            // The user id
            parameterValues.Add("@EmailAddress", TDSessionManager.Current.CurrentUser.Username);
            parameterTypes.Add("@EmailAddress", SqlDbType.NVarChar);

            try
            {
                //Use the SQL Helper class
                using (SqlHelper sqlHelper = new SqlHelper())
                {
                    sqlHelper.ConnOpen(SqlHelperDatabase.BatchJourneyPlannerDB);
                    SqlDataReader reader = sqlHelper.GetReader("GetUserPreferences", parameterValues, parameterTypes);
                    reader.Read();
                    chkStatistics.Checked = (bool)reader["IncludeStatistics"];
                    chkDetails.Checked = (bool)reader["IncludeDetails"];
                    chkPublic.Checked = (bool)reader["PublicTransport"];
                    chkCar.Checked = (bool)reader["Car"];
                    chkCycle.Checked = (bool)reader["Cycle"];
                    if ((bool)reader["ResultsAsRtf"])
                    {
                        radioListFormat.SelectedIndex = 0;
                    }
                    else
                    {
                        radioListFormat.SelectedIndex = 1;
                    }
                    sqlHelper.ConnClose();
                }
            }
            catch (Exception exception)
            {
                // Log and swallow the general exception
                OperationalEvent operationalEvent = new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, ".TDUserProfile.Persist.Exception. Message = " + exception.Message);
                Logger.Write(operationalEvent);
            }
        }

        /// <summary>
        /// Adds the batch request to the database
        /// </summary>
        /// <param name="reader">the file stream's reader</param>
        /// <returns>false if file too large</returns>
        private bool ProcessFileUpload(StreamReader reader)
        {
            // various limits to be concerned about :- 
            // 1. user's own limit (if non zero),
            // 2. default limit (if user's is zero), 
            // 3. limit for uploads that request journey detail files, 
            // 4. limit for uploads without journey detail files
            IPropertyProvider currProps = Properties.Current;
            int limitType = 0;
            int maxLines = 0;
            int defaultMaxLines = int.Parse(currProps["BatchJourneyPlannerDefaultMaxLines"]);
            int userLineLimit = 0;

            //Get user's personal limit from RegisteredUser table
            //Use the SQL Helper class
            using (SqlHelper sqlHelper = new SqlHelper())
            {
                // Create hashtables containing parameters and data types for the stored procs
                Hashtable parameterValues = new Hashtable(1);
                Hashtable parameterTypes = new Hashtable(1);

                // The Username
                parameterValues.Add("@EmailAddress", TDSessionManager.Current.CurrentUser.Username);
                parameterTypes.Add("@EmailAddress", SqlDbType.NVarChar);

                // Get the user's limit
                sqlHelper.ConnOpen(SqlHelperDatabase.BatchJourneyPlannerDB);
                SqlDataReader dataReader = sqlHelper.GetReader("GetUserUploadLimit", parameterValues, parameterTypes);

                dataReader.Read();
                userLineLimit = dataReader.GetInt32(0);

                sqlHelper.ConnClose();
            }

            if (userLineLimit != 0)
            {
                maxLines = userLineLimit;
                limitType = 1;
            }
            else
            {
                maxLines = defaultMaxLines;
                limitType = 2;
            }

            if (chkDetails.Checked)
            {
                int detailsMaxLines = int.Parse(currProps["BatchJourneyPlannerJourneyDetailsMaxLines"]);

                if (maxLines > detailsMaxLines)
                {
                    maxLines = detailsMaxLines;
                    limitType = 3;
                }
            }
            else
            {
                int noDetailsMaxLines = int.Parse(currProps["BatchJourneyPlannerNoJourneyDetailsMaxLines"]);

                if (maxLines > noDetailsMaxLines)
                {
                    maxLines = noDetailsMaxLines;
                    limitType = 4;
                }
            }

            string line;
            int lines = 0;

            while (!reader.EndOfStream)
            {
                line = reader.ReadLine();

                if (line.Trim().Length > 0)
                {
                    lines++;
                    if (lines > maxLines)
                    {
                        // set the error message
                        switch (limitType)
                        {
                            case 1:
                                labelErrorMessages.Text = string.Format(GetResource(RES_EXCEEDS_USER_LIMIT), maxLines.ToString());
                                break;
                            case 2:
                                labelErrorMessages.Text = string.Format(GetResource(RES_EXCEEDS_DEFAULT_LIMIT), maxLines.ToString());
                                break;
                            case 3:
                                labelErrorMessages.Text = string.Format(GetResource(RES_EXCEEDS_JOURNEY_DETAILS_LIMIT), maxLines.ToString());
                                break;
                            case 4:
                                labelErrorMessages.Text = string.Format(GetResource(RES_EXCEEDS_NO_JOURNEY_DETAILS_LIMIT), maxLines.ToString());
                                break;
                            default:
                                labelErrorMessages.Text = string.Format(GetResource(RES_EXCEEDS_DEFAULT_LIMIT), maxLines.ToString());
                                break;
                        }

                        // bomb out
                        return false;
                    }
                }
            }

            // Create the batch record, with status uploading and get a batchid
            int batchId = 0;

            try
            {
                using (SqlHelper sqlHelper = new SqlHelper())
                {
                    // Create hashtables containing parameters and data types for the stored procs
                    Hashtable parameterValues = new Hashtable(1);
                    Hashtable parameterTypes = new Hashtable(1);

                    // The email address
                    parameterValues.Add("@EmailAddress", TDSessionManager.Current.CurrentUser.Username);
                    parameterTypes.Add("@EmailAddress", SqlDbType.NVarChar);

                    // Create the batch
                    sqlHelper.ConnOpen(SqlHelperDatabase.BatchJourneyPlannerDB);
                    batchId = int.Parse(sqlHelper.GetScalar("AddBatchSummary", parameterValues, parameterTypes).ToString());
                }
            }
            catch
            {
                return false;
            }

#region Create data table
            // Datatable upload
            DataTable detailRows = new DataTable("BatchRequestDetail");

            DataColumn colRequestId = new DataColumn("RequestId", System.Type.GetType("System.Int32"));
            colRequestId.AutoIncrement = true;
            detailRows.Columns.Add(colRequestId);

            DataColumn colBatchId = new DataColumn("BatchId", System.Type.GetType("System.Int32"));
            detailRows.Columns.Add(colBatchId);

            DataColumn colBatchDetailStatusId = new DataColumn("BatchDetailStatusId", System.Type.GetType("System.Int32"));
            detailRows.Columns.Add(colBatchDetailStatusId);

            DataColumn userSuppliedUniqueId = new DataColumn("UserSuppliedUniqueId", System.Type.GetType("System.String"));
            detailRows.Columns.Add(userSuppliedUniqueId);

            DataColumn originType = new DataColumn("JourneyParameters.OriginType", System.Type.GetType("System.Char"));
            detailRows.Columns.Add(originType);

            DataColumn origin = new DataColumn("JourneyParameters.Origin", System.Type.GetType("System.String"));
            detailRows.Columns.Add(origin);

            DataColumn destinationType = new DataColumn("JourneyParameters.DestinationType", System.Type.GetType("System.Char"));
            detailRows.Columns.Add(destinationType);

            DataColumn destination = new DataColumn("JourneyParameters.Destination", System.Type.GetType("System.String"));
            detailRows.Columns.Add(destination);

            DataColumn outwardDate = new DataColumn("JourneyParameters.OutwardDate", System.Type.GetType("System.DateTime"));
            detailRows.Columns.Add(outwardDate);

            DataColumn outwardTime = new DataColumn("JourneyParameters.OutwardTime", System.Type.GetType("System.TimeSpan"));
            detailRows.Columns.Add(outwardTime);

            DataColumn outwardArrDep = new DataColumn("JourneyParameters.OutwardArrDep", System.Type.GetType("System.Char"));
            detailRows.Columns.Add(outwardArrDep);

            DataColumn returnDate = new DataColumn("JourneyParameters.ReturnDate", System.Type.GetType("System.DateTime"));
            detailRows.Columns.Add(returnDate);

            DataColumn returnTime = new DataColumn("JourneyParameters.ReturnTime", System.Type.GetType("System.TimeSpan"));
            detailRows.Columns.Add(returnTime);

            DataColumn returnArrDep = new DataColumn("JourneyParameters.ReturnArrDep", System.Type.GetType("System.Char"));
            detailRows.Columns.Add(returnArrDep);

            DataColumn errorMessages = new DataColumn("ErrorMessages", System.Type.GetType("System.String"));
            detailRows.Columns.Add(errorMessages);

            // Co-ordinate limits
            int maxEasting = int.Parse(currProps["BatchJourneyPlannerMaxEasting"]);
            int maxNorthing = int.Parse(currProps["BatchJourneyPlannerMaxNorthing"]);
#endregion

            // Create the datatable rows
            lines = 0;
            int validationErrors = 0;
            reader.BaseStream.Position = 0;
            reader.ReadLine(); // ignore header row
            TimeSpan hours24 = new TimeSpan(24, 0, 0);

#region Create data rows

            while (!reader.EndOfStream)
            {
                line = reader.ReadLine();

                if (line.Trim().Length > 0)
                {
                    lines++;
                    if (lines > maxLines)
                    {
                        // bomb out - should never get here as file length is checked above
                        return false;
                    }

                    DataRow row = detailRows.NewRow();
                    bool rowIsGood = true;
                    string[] parts = line.Split(',');

                    row["BatchId"] = batchId;

                    // check there are the appropriate number of fields (either outward or out + return)
                    if (!((parts.Length == 8) || (parts.Length == 11)))
                    {
                        rowIsGood = false;
                    }
                    else
                    {
                        string temp = parts[0];
                        if (temp.Length == 0)
                        {
                            rowIsGood = false;
                        }
                        row["UserSuppliedUniqueId"] = temp;

                        temp = parts[1];
                        if (!((temp == "n") || (temp == "p") || (temp == "c")))
                        {
                            rowIsGood = false;
                        }
                        row["JourneyParameters.OriginType"] = temp;

                        if (temp == "c")
                        {
                            temp = parts[2];

                            try
                            {
                                int easting = int.Parse(temp.Split('|')[0]);
                                int northing = int.Parse(temp.Split('|')[1]);

                                if (easting > maxEasting)
                                {
                                    rowIsGood = false;
                                }
                                else if (northing > maxNorthing)
                                {
                                    rowIsGood = false;
                                }
                            }
                            catch (Exception)
                            {
                                rowIsGood = false;
                            }
                        }
                        else
                        {
                            temp = parts[2];
                            if (temp.Length == 0)
                            {
                                rowIsGood = false;
                            }
                        }
                        row["JourneyParameters.Origin"] = temp;

                        temp = parts[3];
                        if (!((temp == "n") || (temp == "p") || (temp == "c") || (temp == "")))
                        {
                            rowIsGood = false;
                        }
                        row["JourneyParameters.DestinationType"] = temp;

                        if (temp == "c")
                        {
                            temp = parts[4];

                            try
                            {
                                int easting = int.Parse(temp.Split('|')[0]);
                                int northing = int.Parse(temp.Split('|')[1]);

                                if (easting > maxEasting)
                                {
                                    rowIsGood = false;
                                }
                                else if (northing > maxNorthing)
                                {
                                    rowIsGood = false;
                                }
                            }
                            catch (Exception)
                            {
                                rowIsGood = false;
                            }
                        }
                        else
                        {
                            temp = parts[4];
                            if (temp.Length == 0)
                            {
                                rowIsGood = false;
                            }
                        }
                        row["JourneyParameters.Destination"] = temp;

                        // incoming date is DDMMYYYY
                        temp = parts[5];
                        try
                        {
                            // deal with excel losing leading zero
                            if (temp.Length == 7)
                            {
                                temp = "0" + temp;
                            }

                            //temp = temp.Substring(6, 2) + temp.Substring(2, 2) + temp.Substring(0, 2);
                            if (temp.Length == 8)
                            {
                                row["JourneyParameters.OutwardDate"] = new DateTime(int.Parse(temp.Substring(4, 4)), int.Parse(temp.Substring(2, 2)), int.Parse(temp.Substring(0, 2)));
                            }
                            else
                            {
                                rowIsGood = false;
                            }
                        }
                        catch (Exception)
                        {
                            // this'll be a date string being too short, pass record to db anyway so
                            // can be marked as invalid
                            rowIsGood = false;
                        }

                        temp = parts[6];
                        // deal with excel losing leading zero
                        if (temp.Length == 3)
                        {
                            temp = "0" + temp;
                        }

                        if (temp.Length == 4)
                        {
                            try
                            {
                                TimeSpan outTime = new TimeSpan(int.Parse(temp.Substring(0, 2)), int.Parse(temp.Substring(2, 2)), 0);
                                if ((0 <= outTime.Ticks) && (outTime.Ticks < TimeSpan.TicksPerDay))
                                {
                                    row["JourneyParameters.OutwardTime"] = outTime;
                                }
                                else
                                {
                                    rowIsGood = false;
                                }
                            }
                            catch (Exception)
                            {
                                rowIsGood = false;
                            }
                        }
                        else
                        {
                            rowIsGood = false;
                        }

                        temp = parts[7];
                        if (!((temp == "a") || (temp == "d")))
                        {
                            rowIsGood = false;
                        }
                        else
                        {
                            row["JourneyParameters.OutwardArrDep"] = parts[7];
                        }

                        // There may or may not be remaining fields for a return
                        // journey and they may or may not be filled in
                        if ((parts.Length == 11) && (parts[8].Length > 6))
                        {
                            // incoming date is DDMMYYYY, outgoing needs to be YYMMDD
                            temp = parts[8];
                            try
                            {
                                // deal with excel losing leading zero
                                if (temp.Length == 7)
                                {
                                    temp = "0" + temp;
                                }

                                //temp = temp.Substring(6, 2) + temp.Substring(2, 2) + temp.Substring(0, 2);
                                if (temp.Length == 8)
                                {
                                    row["JourneyParameters.ReturnDate"] = new DateTime(int.Parse(temp.Substring(4, 4)), int.Parse(temp.Substring(2, 2)), int.Parse(temp.Substring(0, 2)));

                                    temp = parts[9];
                                    // deal with excel losing leading zero
                                    if (temp.Length == 3)
                                    {
                                        temp = "0" + temp;
                                    }

                                    if (temp.Length == 4)
                                    {
                                        TimeSpan retTime = new TimeSpan(int.Parse(temp.Substring(0, 2)), int.Parse(temp.Substring(2, 2)), 0);
                                        if((0 < retTime.Ticks) && (retTime.Ticks < TimeSpan.TicksPerDay))
                                        {
                                            row["JourneyParameters.ReturnTime"] = retTime;
                                        }
                                        else
                                        {
                                            rowIsGood = false;
                                        }
                                    }
                                    else
                                    {
                                        rowIsGood = false;
                                    }
                                }
                            }
                            catch (Exception)
                            {
                                // this'll be a date string being too short
                                rowIsGood = false;
                            }

                            temp = parts[10];
                            if (!((temp == "a") || (temp == "d") || (temp == "")))
                            {
                                rowIsGood = false;
                            }
                            else if (temp != "")
                            {
                                row["JourneyParameters.ReturnArrDep"] = parts[10];
                            }
                        }
                    }

                    if (!rowIsGood)
                    {
                        validationErrors++;
                        row["BatchDetailStatusId"] = 4;
                        row["ErrorMessages"] = "Journey details could not be processed";
                    }

                    detailRows.Rows.Add(row);
                }
            }
#endregion

            bool uploadedOK = true;

            try
            {
                using (SqlConnection connection = new SqlConnection(Properties.Current["BatchJourneyPlannerDBLongTimeout"]))
                {
                    connection.Open();

                    // Add the batch details
                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection))
                    {
                        bulkCopy.DestinationTableName = "dbo.BatchRequestDetail";
                        bulkCopy.WriteToServer(detailRows);
                    }
                }
            }
            catch
            {
                uploadedOK = false;
            }

            // update the batch summary with total rows, validation errors
            // and clear the processor id
            try
            {
                using (SqlHelper sqlHelper = new SqlHelper())
                {
                    // Create hashtables containing parameters and data types for the stored procs
                    Hashtable parameterValues = new Hashtable(10);
                    Hashtable parameterTypes = new Hashtable(10);

                    // The batch id
                    parameterValues.Add("@BatchId", batchId);
                    parameterTypes.Add("@BatchId", SqlDbType.Int);

                    // uploaded ok
                    parameterValues.Add("@UploadedOK", uploadedOK);
                    parameterTypes.Add("@UploadedOK", SqlDbType.Bit);

                    // number request
                    parameterValues.Add("@NumberofRequests", lines);
                    parameterTypes.Add("@NumberofRequests", SqlDbType.Int);

                    // number invalid requests
                    parameterValues.Add("@NumberInvalidRequests", validationErrors);
                    parameterTypes.Add("@NumberInvalidRequests", SqlDbType.Int);

                    // inc stats
                    parameterValues.Add("@IncStatistics", chkStatistics.Checked);
                    parameterTypes.Add("@IncStatistics", SqlDbType.Bit);

                    // inc details
                    parameterValues.Add("@IncDetails", chkDetails.Checked);
                    parameterTypes.Add("@IncDetails", SqlDbType.Bit);

                    // inc PT
                    parameterValues.Add("@IncPT", chkPublic.Checked);
                    parameterTypes.Add("@IncPT", SqlDbType.Bit);

                    // inc Car
                    parameterValues.Add("@IncCar", chkCar.Checked);
                    parameterTypes.Add("@IncCar", SqlDbType.Bit);

                    // inc Cycle
                    parameterValues.Add("@IncCycle", chkCycle.Checked);
                    parameterTypes.Add("@IncCycle", SqlDbType.Bit);

                    // convert RTF
                    bool param = false;
                    if ((chkDetails.Checked) && (radioListFormat.SelectedIndex == 0))
                    {
                        param = true;
                    }
                    parameterValues.Add("@ConvertToRtf", param);
                    parameterTypes.Add("@ConvertToRtf", SqlDbType.Bit);

                    // Create the batch
                    sqlHelper.ConnOpen(SqlHelperDatabase.BatchJourneyPlannerDB);
                    sqlHelper.Execute("UpdateBatchSummary", parameterValues, parameterTypes);
                }
            }
            catch
            {
                return false;
            }

            return uploadedOK;
        }


        /// <summary>
        /// Populates the list of the user's batches
        /// </summary>
        private void PopulateBatchTable()
        {
            // add the data bound event handler
            this.batchesRepeater.ItemDataBound += new System.Web.UI.WebControls.RepeaterItemEventHandler(this.batchesRepeater_ItemDatabound);

            // Create hashtables containing parameters and data types for the stored procs
            Hashtable parameterValues = new Hashtable(4);
            Hashtable parameterTypes = new Hashtable(4);

            // The Username
            parameterValues.Add("@EmailAddress", TDSessionManager.Current.CurrentUser.Username);
            parameterTypes.Add("@EmailAddress", SqlDbType.NVarChar);

            // The page
            parameterValues.Add("@Page", resultsPage);
            parameterTypes.Add("@Page", SqlDbType.Int);

            // The sort column
            parameterValues.Add("@SortColumn", sortColumn);
            parameterTypes.Add("@SortColumn", SqlDbType.NVarChar);

            // The sort direction
            parameterValues.Add("@SortDirection", sortDirection);
            parameterTypes.Add("@SortDirection", SqlDbType.NVarChar);

            // The RETURN VALUE
            SqlParameter paramReturnValue = new SqlParameter("RETURN_VALUE", SqlDbType.Int);
            paramReturnValue.Direction = ParameterDirection.ReturnValue;

            //Use the SQL Helper class
            using (SqlHelper sqlHelper = new SqlHelper())
            {
                // Add the user
                sqlHelper.ConnOpen(SqlHelperDatabase.BatchJourneyPlannerDB);
                SqlDataReader dataReader = sqlHelper.GetReader("GetBatchRequestsForUser", parameterValues, parameterTypes, paramReturnValue);

                // Get the return values
                batchesRepeater.DataSource = dataReader;
                batchesRepeater.DataBind();

                // Set up the table nav control
                dataReader.Close();
                decimal pages = ((decimal)((int)paramReturnValue.Value)) / 20;
                totalPages = (int)System.Math.Ceiling(pages);
                if (totalPages == 0) totalPages = 1;
                string navText = " | " + string.Format(GetResource(RES_NAVLABEL), resultsPage, totalPages.ToString()) + " | ";
                labelNavControls.Text = navText;
                sqlHelper.ConnClose();
            }

            // store table info in viewstate
            ViewState["resultsPage"] = resultsPage;
            ViewState["sortColumn"] = sortColumn;
            ViewState["sortDirection"] = sortDirection;
        }

        /// <summary>
        /// The event handler for the resultsRepeater Item Data Bound event
        /// </summary>
        /// <param name="sender">Notifying object</param>
        /// <param name="e">Event data</param>
        protected void batchesRepeater_ItemDatabound(object sender, RepeaterItemEventArgs e)
        {
            string backColourClass = "cellNormal";

            switch (e.Item.ItemType)
            {
                case ListItemType.Header:
                    // Get header texts
                    LinkButton header1 = (LinkButton)e.Item.FindControl("header1");
                    header1.Text = sortColumn != "BatchId" ? HeaderRequestId : sortDirection == "asc" ? HeaderRequestId + GetResource(RES_DOWNARROW) : HeaderRequestId + GetResource(RES_UPARROW);
                    header1.Click += new EventHandler(sortBatchId_Click);
                    LinkButton header2 = (LinkButton)e.Item.FindControl("header2");
                    header2.Text = sortColumn != "Submitted" ? HeaderSubmitted : sortDirection == "asc" ? HeaderSubmitted + GetResource(RES_DOWNARROW) : HeaderSubmitted + GetResource(RES_UPARROW);
                    header2.Click += new EventHandler(sortSubmitted_Click);
                    LinkButton header3 = (LinkButton)e.Item.FindControl("header3");
                    header3.Text = sortColumn != "PublicTransport" ? HeaderPublicTransport : sortDirection == "asc" ? HeaderPublicTransport + GetResource(RES_DOWNARROW) : HeaderPublicTransport + GetResource(RES_UPARROW);
                    header3.Click += new EventHandler(sortPublicTransport_Click);
                    LinkButton header4 = (LinkButton)e.Item.FindControl("header4");
                    header4.Text = sortColumn != "Car" ? HeaderCar : sortDirection == "asc" ? HeaderCar + GetResource(RES_DOWNARROW) : HeaderCar + GetResource(RES_UPARROW);
                    header4.Click += new EventHandler(sortCar_Click);
                    LinkButton header5 = (LinkButton)e.Item.FindControl("header5");
                    header5.Text = sortColumn != "Cycle" ? HeaderCycle : sortDirection == "asc" ? HeaderCycle + GetResource(RES_DOWNARROW) : HeaderCycle + GetResource(RES_UPARROW);
                    header5.Click += new EventHandler(sortCycle_Click);
                    LinkButton header6 = (LinkButton)e.Item.FindControl("header6");
                    header6.Text = sortColumn != "NumberRequests" ? HeaderNumberRequests : sortDirection == "asc" ? HeaderNumberRequests + GetResource(RES_DOWNARROW) : HeaderNumberRequests + GetResource(RES_UPARROW);
                    header6.Click += new EventHandler(sortNumberRequests_Click);
                    LinkButton header7 = (LinkButton)e.Item.FindControl("header7");
                    header7.Text = sortColumn != "NumberResults" ? HeaderNumberResults : sortDirection == "asc" ? HeaderNumberResults + GetResource(RES_DOWNARROW) : HeaderNumberResults + GetResource(RES_UPARROW);
                    header7.Click += new EventHandler(sortNumberResults_Click);
                    LinkButton header13 = (LinkButton)e.Item.FindControl("header13");
                    header13.Text = sortColumn != "NumberPartials" ? HeaderNumberPartials : sortDirection == "asc" ? HeaderNumberPartials + GetResource(RES_DOWNARROW) : HeaderNumberPartials + GetResource(RES_UPARROW);
                    header13.Click += new EventHandler(sortNumberPartials_Click);
                    LinkButton header8 = (LinkButton)e.Item.FindControl("header8");
                    header8.Text = sortColumn != "ValidationErrors" ? HeaderValidationErrors : sortDirection == "asc" ? HeaderValidationErrors + GetResource(RES_DOWNARROW) : HeaderValidationErrors + GetResource(RES_UPARROW);
                    header8.Click += new EventHandler(sortValidationErrors_Click);
                    LinkButton header9 = (LinkButton)e.Item.FindControl("header9");
                    header9.Text = sortColumn != "NoResults" ? HeaderNumberNoResults : sortDirection == "asc" ? HeaderNumberNoResults + GetResource(RES_DOWNARROW) : HeaderNumberNoResults + GetResource(RES_UPARROW);
                    header9.Click += new EventHandler(sortNoResults_Click);
                    LinkButton header10 = (LinkButton)e.Item.FindControl("header10");
                    header10.Text = sortColumn != "DateComplete" ? HeaderDateComplete : sortDirection == "asc" ? HeaderDateComplete + GetResource(RES_DOWNARROW) : HeaderDateComplete + GetResource(RES_UPARROW);
                    header10.Click += new EventHandler(sortDateComplete_Click);
                    LinkButton header11 = (LinkButton)e.Item.FindControl("header11");
                    header11.Text = sortColumn != "Status" ? HeaderStatus : sortDirection == "asc" ? HeaderStatus + GetResource(RES_DOWNARROW) : HeaderStatus + GetResource(RES_UPARROW);
                    header11.Click += new EventHandler(sortStatus_Click);
                    Label header12 = (Label)e.Item.FindControl("header12");
                    header12.Text = HeaderSelect;
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

                    // Sort out the data cells
                    HtmlTableCell cell1 = (HtmlTableCell)e.Item.FindControl("cell1");
                    cell1.InnerText = record["BatchId"].ToString();
                    cell1.Attributes["class"] = backColourClass;
                    HtmlTableCell cell2 = (HtmlTableCell)e.Item.FindControl("cell2");
                    cell2.InnerText = record["QueuedDateTime"].ToString();
                    cell2.Attributes["class"] = backColourClass;
                    HtmlTableCell cell3 = (HtmlTableCell)e.Item.FindControl("cell3");
                    cell3.Attributes["class"] = backColourClass;
                    HtmlTableCell cell4 = (HtmlTableCell)e.Item.FindControl("cell4");
                    cell4.Attributes["class"] = backColourClass;
                    HtmlTableCell cell5 = (HtmlTableCell)e.Item.FindControl("cell5");
                    cell5.Attributes["class"] = backColourClass;
                    HtmlTableCell cell6 = (HtmlTableCell)e.Item.FindControl("cell6");
                    cell6.InnerText = record["NumberOfRequests"].ToString();
                    cell6.Attributes["class"] = backColourClass;
                    HtmlTableCell cell7 = (HtmlTableCell)e.Item.FindControl("cell7");
                    if (!string.IsNullOrEmpty(record["CompletedDateTime"].ToString()))
                    {
                        cell7.InnerText = record["NumberOfSuccessfulResults"].ToString();
                    }
                    cell7.Attributes["class"] = backColourClass;
                    HtmlTableCell cell13 = (HtmlTableCell)e.Item.FindControl("cell13");
                    if (!string.IsNullOrEmpty(record["CompletedDateTime"].ToString()))
                    {
                        cell13.InnerText = record["NumberOfPartialSuccesses"].ToString();
                    }
                    cell13.Attributes["class"] = backColourClass;
                    HtmlTableCell cell8 = (HtmlTableCell)e.Item.FindControl("cell8");
                    if (!string.IsNullOrEmpty(record["CompletedDateTime"].ToString()))
                    {
                        cell8.InnerText = record["NumberOfInvalidRequests"].ToString();
                    }
                    cell8.Attributes["class"] = backColourClass;
                    HtmlTableCell cell9 = (HtmlTableCell)e.Item.FindControl("cell9");
                    if (!string.IsNullOrEmpty(record["CompletedDateTime"].ToString()))
                    {
                        cell9.InnerText = record["NumberOfUnsuccessfulRequests"].ToString();
                    }
                    cell9.Attributes["class"] = backColourClass;
                    HtmlTableCell cell10 = (HtmlTableCell)e.Item.FindControl("cell10");
                    cell10.InnerText = record["CompletedDateTime"].ToString();
                    cell10.Attributes["class"] = backColourClass;
                    HtmlTableCell cell11 = (HtmlTableCell)e.Item.FindControl("cell11");
                    cell11.InnerText = record["BatchStatusDescription"].ToString();
                    cell11.Attributes["class"] = backColourClass;
                    HtmlTableCell cell12 = (HtmlTableCell)e.Item.FindControl("cell12");
                    cell12.Attributes["class"] = backColourClass;

                    // Add javascript to radio
                    RadioButton radio = (RadioButton)e.Item.FindControl("radioSelect");
                    string script = "SetRadioButton('batchesRepeater.*radioSelect',this)";
                    radio.Attributes.Add("onclick", script);

                    // Sort out the ticks
                    HtmlImage imagePT = (HtmlImage)e.Item.FindControl("imagePtTick");
                    imagePT.Src = GetResource(RES_TICKIMAGE);
                    imagePT.Visible = bool.Parse(record["ReportParameters.IncludePublicTransport"].ToString());
                    HtmlImage imageCar = (HtmlImage)e.Item.FindControl("imageCarTick");
                    imageCar.Src = GetResource(RES_TICKIMAGE);
                    imageCar.Visible = bool.Parse(record["ReportParameters.IncludeCar"].ToString());
                    HtmlImage imageCycle = (HtmlImage)e.Item.FindControl("imageCycleTick");
                    imageCycle.Src = GetResource(RES_TICKIMAGE);
                    imageCycle.Visible = bool.Parse(record["ReportParameters.IncludeCycle"].ToString());

                    // Info label for queued and in progess items
                    Label infoLabel = (Label)e.Item.FindControl("infoLabel");
                    switch (record["BatchStatusDescription"].ToString())
                    {
                        case "In Progress":
                            if (record.IsDBNull(13))
                            {
                                break;
                            }
                            // Span the cell across the next 4 columns
                            cell7.ColSpan = 5;
                            cell13.Visible = false;
                            cell9.Visible = false;
                            cell8.Visible = false;
                            cell10.Visible = false;
                            infoLabel.Text = string.Format(GetResource("BatchJourneyPlanner.PercentageComplete"), record["BatchId"], record["PercentageComplete"]);
                            infoLabel.Visible = true;

                            break;
                        case "Queued":
                            if (record.IsDBNull(14))
                            {
                                break;
                            }
                            string forLabel = record["QueuedPosition"].ToString();
                            if ((forLabel.Length == 2) && (forLabel.StartsWith("1")))
                            {
                                forLabel += "th";
                            }
                            else
                            {
                                switch (forLabel.Substring(forLabel.Length - 1))
                                {
                                    case "1":
                                        forLabel += "st";
                                        break;
                                    case "2":
                                        forLabel += "nd";
                                        break;
                                    case "3":
                                        forLabel += "rd";
                                        break;
                                    default:
                                        forLabel += "th";
                                        break;
                                }
                            }
                            // Span the cell across the next 4 columns
                            cell7.ColSpan = 5;
                            cell13.Visible = false;
                            cell9.Visible = false;
                            cell8.Visible = false;
                            cell10.Visible = false;
                            infoLabel.Text = string.Format(GetResource("BatchJourneyPlanner.QueuedPosition"), record["BatchId"], forLabel);
                            infoLabel.Visible = true;
                            break;
                    }
                    break;
                case ListItemType.Footer:
                    // Handle empy list
                    if (batchesRepeater.Items.Count < 1)
                    {
                        Label lblNoData = (Label)e.Item.FindControl("labelNoData");
                        lblNoData.Visible = true;
                        lblNoData.Text = GetResource(RES_NODATA);
                    }
                    break;
            }
        }

        /// <summary>
        /// Changes the sort order
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        protected void sortBatchId_Click(object sender, EventArgs e)
        {
            resultsPage = 1;

            if (sortColumn == "BatchId")
            {
                sortDirection = sortDirection.Equals("desc") ? "asc" : "desc";
            }
            else
            {
                sortColumn = "BatchId";
                sortDirection = "asc";
            }

            if (!sortDone)
            {
                PopulateBatchTable();
            }
            sortDone = true;
        }

        /// <summary>
        /// Changes the sort order
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        protected void sortSubmitted_Click(object sender, EventArgs e)
        {
            resultsPage = 1;

            if (sortColumn == "Submitted")
            {
                sortDirection = sortDirection.Equals("desc") ? "asc" : "desc";
            }
            else
            {
                sortColumn = "Submitted";
                sortDirection = "asc";
            }

            if (!sortDone)
            {
                PopulateBatchTable();
            }
            sortDone = true;
        }

        /// <summary>
        /// Changes the sort order
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        protected void sortPublicTransport_Click(object sender, EventArgs e)
        {
            resultsPage = 1;

            if (sortColumn == "PublicTransport")
            {
                sortDirection = sortDirection.Equals("desc") ? "asc" : "desc";
            }
            else
            {
                sortColumn = "PublicTransport";
                sortDirection = "desc";
            }

            if (!sortDone)
            {
                PopulateBatchTable();
            }
            sortDone = true;
        }

        /// <summary>
        /// Changes the sort order
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        protected void sortCar_Click(object sender, EventArgs e)
        {
            resultsPage = 1;

            if (sortColumn == "Car")
            {
                sortDirection = sortDirection.Equals("desc") ? "asc" : "desc";
            }
            else
            {
                sortColumn = "Car";
                sortDirection = "desc";
            }

            if (!sortDone)
            {
                PopulateBatchTable();
            }
            sortDone = true;
        }

        /// <summary>
        /// Changes the sort order
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        protected void sortCycle_Click(object sender, EventArgs e)
        {
            resultsPage = 1;

            if (sortColumn == "Cycle")
            {
                sortDirection = sortDirection.Equals("desc") ? "asc" : "desc";
            }
            else
            {
                sortColumn = "Cycle";
                sortDirection = "desc";
            }

            if (!sortDone)
            {
                PopulateBatchTable();
            }
            sortDone = true;
        }

        /// <summary>
        /// Changes the sort order
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        protected void sortNumberRequests_Click(object sender, EventArgs e)
        {
            resultsPage = 1;

            if (sortColumn == "NumberRequests")
            {
                sortDirection = sortDirection.Equals("desc") ? "asc" : "desc";
            }
            else
            {
                sortColumn = "NumberRequests";
                sortDirection = "asc";
            }

            if (!sortDone)
            {
                PopulateBatchTable();
            }
            sortDone = true;
        }

        /// <summary>
        /// Changes the sort order
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        protected void sortNumberResults_Click(object sender, EventArgs e)
        {
            resultsPage = 1;

            if (sortColumn == "NumberResults")
            {
                sortDirection = sortDirection.Equals("desc") ? "asc" : "desc";
            }
            else
            {
                sortColumn = "NumberResults";
                sortDirection = "asc";
            }

            if (!sortDone)
            {
                PopulateBatchTable();
            }
            sortDone = true;
        }

        /// <summary>
        /// Changes the sort order
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        protected void sortNumberPartials_Click(object sender, EventArgs e)
        {
            resultsPage = 1;

            if (sortColumn == "NumberPartials")
            {
                sortDirection = sortDirection.Equals("desc") ? "asc" : "desc";
            }
            else
            {
                sortColumn = "NumberPartials";
                sortDirection = "asc";
            }

            if (!sortDone)
            {
                PopulateBatchTable();
            }
            sortDone = true;
        }

        /// <summary>
        /// Changes the sort order
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        protected void sortValidationErrors_Click(object sender, EventArgs e)
        {
            resultsPage = 1;

            if (sortColumn == "ValidationErrors")
            {
                sortDirection = sortDirection.Equals("desc") ? "asc" : "desc";
            }
            else
            {
                sortColumn = "ValidationErrors";
                sortDirection = "asc";
            }

            if (!sortDone)
            {
                PopulateBatchTable();
            }
            sortDone = true;
        }

        /// <summary>
        /// Changes the sort order
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        protected void sortNoResults_Click(object sender, EventArgs e)
        {
            resultsPage = 1;

            if (sortColumn == "NoResults")
            {
                sortDirection = sortDirection.Equals("desc") ? "asc" : "desc";
            }
            else
            {
                sortColumn = "NoResults";
                sortDirection = "asc";
            }

            if (!sortDone)
            {
                PopulateBatchTable();
            }
            sortDone = true;
        }

        /// <summary>
        /// Changes the sort order
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        protected void sortDateComplete_Click(object sender, EventArgs e)
        {
            resultsPage = 1;

            if (sortColumn == "DateComplete")
            {
                sortDirection = sortDirection.Equals("desc") ? "asc" : "desc";
            }
            else
            {
                sortColumn = "DateComplete";
                sortDirection = "asc";
            }

            if (!sortDone)
            {
                PopulateBatchTable();
            }
            sortDone = true;
        }

        /// <summary>
        /// Changes the sort order
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        protected void sortStatus_Click(object sender, EventArgs e)
        {
            resultsPage = 1;

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
                PopulateBatchTable();
            }
            sortDone = true;
        }

        /// <summary>
        /// Changes the results page
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        protected void linkFirst_Click(object sender, EventArgs e)
        {
            resultsPage = 1;
            PopulateBatchTable();
        }

        /// <summary>
        /// Changes the results page
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        protected void linkPrevious_Click(object sender, EventArgs e)
        {
            if (resultsPage > 1)
            {
                resultsPage--;
                PopulateBatchTable();
            }
        }

        /// <summary>
        /// Changes the results page
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        protected void linkNext_Click(object sender, EventArgs e)
        {
            if (resultsPage < totalPages)
            {
                resultsPage++;
                PopulateBatchTable();
            }
        }

        /// <summary>
        /// Changes the results page
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        protected void linkLast_Click(object sender, EventArgs e)
        {
            resultsPage = totalPages;
            PopulateBatchTable();
        }

        /// <summary>
        /// Reloads the results table
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        protected void buttonReload_Click(object sender, EventArgs e)
        {
            PopulateBatchTable();
        }

        /// <summary>
        /// Downloads a zip of results
        /// </summary>
        /// <param name="sender">sender</param>
        /// <param name="e">event args</param>
        protected void buttonDownload_Click(object sender, EventArgs e)
        {
            // Check the selected batch is 
            string selectedBatchId = string.Empty;
            string batchStatus = string.Empty;

            foreach (RepeaterItem item in batchesRepeater.Items)
            {
                RadioButton radio = (RadioButton)item.FindControl("radioSelect");

                if (radio.Checked)
                {
                    HtmlTableCell cell1 = (HtmlTableCell)item.FindControl("cell1");
                    selectedBatchId = cell1.InnerText;
                    HtmlTableCell cell11 = (HtmlTableCell)item.FindControl("cell11");
                    batchStatus = cell11.InnerText;
                }
            }

            if (selectedBatchId == string.Empty)
            {
                panelErrorMessage.Visible = true;
                labelErrorMessages.Visible = true;
                labelErrorMessages.Text = GetResource(RES_NOROW);
            }
            else if (batchStatus != "Complete")
            {
                panelErrorMessage.Visible = true;
                labelErrorMessages.Visible = true;
                labelErrorMessages.Text = GetResource(RES_COMPLETEDONLY);
            }
            else
            {
                // Get the results
                ReturnResults(selectedBatchId);
            }
        }

        /// <summary>
        /// Returns the results, if necessary creates them in the file storage
        /// </summary>
        /// <param name="selectedBatchId">Results to return</param>
        private void ReturnResults(string selectedBatchId)
        {
            // Check to see if the zip is already present
            byte[] zipFile = new byte[0];
            bool zipExists = false;

            using (SqlHelper sqlHelper = new SqlHelper())
            {
                // Create hashtables containing parameters and data types for the stored procs
                Hashtable parameterValues = new Hashtable(1);
                Hashtable parameterTypes = new Hashtable(1);

                // The Batch Id
                parameterValues.Add("@BatchId", int.Parse(selectedBatchId));
                parameterTypes.Add("@BatchId", SqlDbType.Int);

                // Get the zip
                sqlHelper.ConnOpen(SqlHelperDatabase.BatchJourneyPlannerDB);
                SqlDataReader reader = sqlHelper.GetReader("GetZip", parameterValues, parameterTypes);

                if (reader.HasRows)
                {
                    reader.Read();
                    if (!reader.IsDBNull(0))
                    {
                        zipFile = (byte[])reader["ZipDownload"];
                        zipExists = true;
                    }
                }
            }

            if (!zipExists)
            {
                string error = BatchZipHelper.CreateZip(selectedBatchId, TDSessionManager.Current.CurrentUser.Username);

                if (error != string.Empty)
                {
                    panelErrorMessage.Visible = true;
                    labelErrorMessages.Visible = true;
                    labelErrorMessages.Text = GetResource(error);
                    return;
                }

                using (SqlHelper sqlHelper = new SqlHelper())
                {
                    // Create hashtables containing parameters and data types for the stored procs
                    Hashtable parameterValues = new Hashtable(1);
                    Hashtable parameterTypes = new Hashtable(1);

                    // The Batch Id
                    parameterValues.Add("@BatchId", int.Parse(selectedBatchId));
                    parameterTypes.Add("@BatchId", SqlDbType.Int);

                    // Get the zip
                    sqlHelper.ConnOpen(SqlHelperDatabase.BatchJourneyPlannerDB);
                    SqlDataReader reader = sqlHelper.GetReader("GetZip", parameterValues, parameterTypes);

                    if (reader.HasRows)
                    {
                        reader.Read();
                        if (!reader.IsDBNull(0))
                        {
                            zipFile = (byte[])reader["ZipDownload"];
                            zipExists = true;
                        }
                    }
                }
            }

            if (zipExists)
            {
                // Create hashtables containing parameters and data types for the stored procs
                Hashtable parameterValues3 = new Hashtable(1);
                Hashtable parameterTypes3 = new Hashtable(1);

                // The Batch Id
                parameterValues3.Add("@BatchId", int.Parse(selectedBatchId));
                parameterTypes3.Add("@BatchId", SqlDbType.Int);

                //Use the SQL Helper class
                using (SqlHelper sqlHelper = new SqlHelper())
                {
                    // Add the user
                    sqlHelper.ConnOpen(SqlHelperDatabase.BatchJourneyPlannerDB);
                    sqlHelper.Execute("UpdateBatchSummaryDownloadRequestInfo", parameterValues3, parameterTypes3);
                }

                Response.Clear();
                Response.ContentType = "";
                Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}.zip", selectedBatchId));
                Response.AppendHeader("Content-Type", "application/zip");
                Response.AddHeader("Content-Length", zipFile.Length.ToString());

                // don't think chunking is required, if it is use this code
                //int chunkSize = 1024;
                //byte[] buffer = new byte[chunkSize];
                //for (long i = 0; i < zipFile.LongLength; i += chunkSize)
                //{
                //    if (i + chunkSize > zipFile.LongLength)
                //    {
                //        buffer = new byte[zipFile.LongLength - i];
                //    }
                //    Array.Copy(zipFile, i, buffer, 0, buffer.Length);
                //    Response.OutputStream.Write(buffer, 0, buffer.Length);
                //    Response.OutputStream.Flush();
                //}

                Response.BinaryWrite(zipFile);
                Response.End();
            }
            else
            {
                panelErrorMessage.Visible = true;
                labelErrorMessages.Visible = true;
                labelErrorMessages.Text = GetResource("BatchJourneyPlanner.RetrievalFailure");
            }
        }
    }
}
