<%@ Register TagPrefix="uc1" TagName="ErrorDisplayControl" Src="../Controls/ErrorDisplayControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ExpandableMenuControl" Src="../Controls/ExpandableMenuControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FooterControl" Src="../Controls/FooterControl.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<%@ Register TagPrefix="uc1" TagName="HeaderControl" Src="../Controls/HeaderControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="PoweredBy" Src="../Controls/PoweredByControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ClientLinkControl" Src="../Controls/ClientLinkControl.ascx" %>
<%@ Register TagPrefix="recaptcha" Namespace="Recaptcha" Assembly="Recaptcha" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BatchJourneyPlanner.aspx.cs" Inherits="TransportDirect.UserPortal.Web.BatchJourneyPlanner.BatchJourneyPlanner" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="<%=TransportDirect.Common.TDCultureInfo.CurrentUICulture.Name%>" xmlns="http://www.w3.org/1999/xhtml">
<head>
    <link rel="canonical" href="http://www.transportdirect.info/Web2/BatchJourneyPlanner/BatchJourneyPlanner.aspx" />
    <meta name="description" content="Submit batches of journeys to plan for or register to do so." />
    <meta name="keywords" content="journey planning, batch journey planning, batch journey planning registration" />
    <link rel="image_src" href="http://www.transportdirect.info/Web2/App_Themes/TransportDirect/images/gifs/softcontent/en/finda_bus.gif" />
    <cc1:HeadElementControl id="headElementControl" runat="server" Stylesheets="setup.css,jpstd.css,CalendarSS.css,HomePage.css,ExpandableMenu.css,Nifty.css,map.css,BatchJourneyPlanner.aspx.css">
    </cc1:HeadElementControl>
</head>
<body onload="EnableDisableCheckboxes()">
	<div id="SkipToMainContentArea" class="SkipToMainContentArea">
	    <a id="skipToMainContentLink" onfocus="this.className='HackFocusSkipMain';" onblur="this.className='SkipToMainContentLink';" href="#BatchJourneyPlanner" tabindex="1" class="SkipToMainContentLink">Skip to main content</a>
	</div>
    <div class="CenteredContent">
        <form id="BatchJourneyPlanner" method="post" runat="server">
            <uc1:HeaderControl id="headerControl" runat="server"></uc1:HeaderControl>
            <table class="HomepageOutlineTable" cellspacing="0" cellpadding="0" border="0">
                <tr valign="top">
                    <!-- Left Hand Navigaion Bar -->
                    <td class="LeftHandNavigationBar" valign="top">
                        <uc1:ExpandableMenuControl id="expandableMenuControl" runat="server" EnableViewState="False"
                            Categorycssclass="HomePageMenu"></uc1:ExpandableMenuControl>
                    </td>
                    <!-- Page Content -->
                    <td valign="top">
                        <cc1:RoundedCornerControl id="mainbit" runat="server" InnerColour="White" OuterColour="#CCECFF"
                            Corners="TopLeft" cssclass="bodyArea">
                            <!-- Main content control table -->
                            <table lang="en" cellspacing="0" cellpadding="0" width="100%" border="0">
                                <tr valign="top">
                                    <!-- Journey Planning Column -->
                                    <td valign="top">
                                        <div class="toptitlediv">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <a name="BatchJourneyPlanner"></a>
                                                        <cc1:TDImage id="imageBatchJourneyPlanner" runat="server" width="70" height="36"></cc1:TDImage>
                                                    </td>
                                                    <td>
                                                        <h1>
                                                            <asp:Label id="labelPageTitle" runat="server"></asp:Label>
                                                        </h1>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <p>&nbsp;</p>
                                                    </td>
                                                </tr>
                                            </table>    
                                        </div>
                                        <asp:Panel id="panelErrorDisplayControl" runat="server" visible="False">
                                            <div class="boxtypeerrormsgfour">
                                                <uc1:ErrorDisplayControl id="errorDisplayControl" runat="server" visible="False"></uc1:ErrorDisplayControl>
                                            </div>
                                        </asp:Panel>
                                        <asp:Panel id="panelSubHeading" runat="server">
                                            <div class="boxtypeeightalt">
                                                <asp:Label id="labelFromToTitle" runat="server" cssclass="txtseven"></asp:Label>
                                            </div>
                                        </asp:Panel>
                                        <asp:Panel id="panelErrorMessage" runat="server" visible="False">
                                            <div class="boxtypeeightalt">
                                                <asp:Label id="labelErrorMessages" runat="server" cssclass="labelError"></asp:Label>
                                                <p>&nbsp;</p>
                                            </div>
                                        </asp:Panel>
                                        <asp:Panel id="panelConfirmation" runat="server" visible="False">
                                            <div class="boxtypeeightalt">
                                                <table>
                                                    <tr>
                                                        <td class="labelConfirmation">
                                                            <asp:Label id="labelConfirmation" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </asp:Panel>
                                        <!-- about batch -->
                                        <asp:Panel id="panelAbout" runat="server" visible="false">
                                            <div class="boxtypeeightalt">
                                                <table width="600px">
                                                    <tr>
                                                        <td>
                                                            <asp:Label id="labelWhatIs" cssclass="labelSubHeading" runat="server" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label id="labelWhatIsSC" cssclass="labelInfoBox" runat="server" width="600px" />
                                                            <p>&nbsp;</p>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label id="labelAlreadyApproved" cssclass="labelSubHeading" runat="server" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label id="labelAlreadyApprovedSC" cssclass="labelInfoBox" runat="server" width="600px" />
                                                            <p>&nbsp;</p>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label id="labelHowRegister" cssclass="labelSubHeading" runat="server" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:Label id="labelHowRegisterSC" cssclass="labelInfoBox" runat="server" width="600px" />
                                                            <p>&nbsp;</p>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </asp:Panel>
                                        <!-- registration panel -->
                                        <asp:Panel id="panelRegistration" runat="server" visible="false">
                                            <div class="boxtypeeightalt">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <table width="600px">
                                                                <tr>
                                                                    <td align=center>
                                                                        <table width="500px">
                                                                            <tr>
                                                                                <td align="left" class="textTerms">
                                                                                    <div id="divTerms" runat="server" class="divTerms">
                                                                                    </div>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="right">
                                                                                    <asp:CheckBox id="chkTerms" cssclass="labelNormal" runat="server" onchange="EnableRegister()" />
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td>
                                                            <cc1:TDImage id="imageUKMap" runat="server"></cc1:TDImage>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:Label id="labelRegdAs" runat="server" cssclass="labelNormal" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <table>
                                                                <colgroup>
                                                                    <col width="12%" />
                                                                    <col width="88%" />
                                                                </colgroup>
                                                                <tr>
                                                                    <td align="right">
                                                                        <asp:Label id="labelFirstName" cssclass="labelNormalRight" associatedcontrolid="textBoxFirstName" runat="server"/>
                                                                    </td>
                                                                    <td>
			                                                            <asp:textbox id="textBoxFirstName" runat="server" enableviewstate="False" cssclass="batchInput1" onkeyup="EnableRegister()" onpaste="EnableRegister()" oninput="EnableRegister()" oncut="EnableRegister()" ></asp:textbox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right">
                                                                        <asp:Label id="labelLastName" cssclass="labelNormalRight" associatedcontrolid="textBoxLastName" runat="server"/>
                                                                    </td>
                                                                    <td>
			                                                            <asp:textbox id="textBoxLastName" runat="server" enableviewstate="False" cssclass="batchInput1" onkeyup="EnableRegister()" onpaste="EnableRegister()" oninput="EnableRegister()" oncut="EnableRegister()" ></asp:textbox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right">
                                                                        <asp:Label id="labelPhone" cssclass="labelNormalRight" associatedcontrolid="textBoxPhone" runat="server"/>
                                                                    </td>
                                                                    <td>
			                                                            <asp:textbox id="textBoxPhone" runat="server" enableviewstate="False" cssclass="batchInput1" onkeyup="EnableRegister()" onpaste="EnableRegister()" oninput="EnableRegister()" oncut="EnableRegister()" ></asp:textbox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="top">
                                                                        <asp:Label id="labelProposedUse" cssclass="labelNormal" associatedcontrolid="textBoxProposedUse" runat="server" />
                                                                    </td>
                                                                    <td>
			                                                            <asp:textbox id="textBoxProposedUse" wrap="true" textmode="MultiLine" runat="server" enableviewstate="False" cssclass="batchInput2" onkeyup="EnableRegister()" onpaste="EnableRegister()" oninput="EnableRegister()" oncut="EnableRegister()" ></asp:textbox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                    </td>
                                                                    <td>
                                                                        <table width="610px">
                                                                            <tr>
                                                                                <td valign="top">
                                                                                    <asp:Label id="labelMandatory" runat="server" cssclass="labelNormal" />
                                                                                </td>
                                                                                <td align="right">
                                                                                    <recaptcha:RecaptchaControl id="recaptcha" runat="server" PublicKey="6LcMls4SAAAAAGH_PO80vrSv38Hx7LRK8bVLc6GY" PrivateKey="6LcMls4SAAAAABfsG7zBQtid5a8XalalcdlhVIFV" />
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td colspan="2">
                                                                                    <asp:Panel id="panelRegistrationError" runat="server" visible="False">
                                                                                        <div class="boxtypeeightalt">
                                                                                            <asp:Label id="labelRegistrationError" runat="server" cssclass="labelError"></asp:Label>
                                                                                        </div>
                                                                                    </asp:Panel>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                </td>
                                                                                <td align="right">
                                                                                    <cc1:tdbutton id="buttonRegister" runat="server" cssclass="TDButtonDefault" onmouseover="this.className='TDButtonDefaultMouseOver'" />
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label id="labelEmailsText" runat="server" cssclass="labelNormal" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label id="labelMoreInfo" runat="server" cssclass="labelNormal" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </asp:Panel>
                                        <!-- journey planning panel -->
                                        <asp:Panel id="panelJourneyPlanning" runat="server" visible="false">
                                            <div>
                                                <table width="100%">
                                                    <col width="5%" />
                                                    <col width="55%" />
                                                    <col width="5%" />
                                                    <col width="35%" />
                                                    <tr>
                                                        <td />
                                                        <td colspan="3">
                                                            <p style="text-decoration: underline">
                                                                <b>Please be aware that the Transport Direct portal will be closing on the 30th September.
                                                                In order to ensure that we can process any backlog of batch files we will not be accepting 
                                                                any new batch files after the 14th September. Any file submitted after this date will not be processed</b>
                                                            </p>
                                                            <br />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td />
                                                        <td class="cellOutlined">
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label id="labelUpload" cssclass="labelSubHeading" runat="server" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label id="labelUploadInstructions" cssclass="labelNormal" runat="server" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <table>
                                                                            <col width="50%" />
                                                                            <col width="50%" />
                                                                            <tr>
                                                                                <td colspan="2">
                                                                                    <asp:FileUpload id="fileUpload" runat="server" width="400px" onchange="EnableLoad()" />
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td valign = "top">
                                                                                    <br />
                                                                                    <asp:Label id="labelTypes" cssclass="labelNormal" runat="server" />
                                                                                    <br />
                                                                                    <asp:CheckBox id="chkPublic" cssclass="labelNormal" runat="server" onclick="EnableDisableCheckboxes()" />
                                                                                    <br />
                                                                                    <asp:CheckBox id="chkCar" cssclass="labelNormal" runat="server" onclick="EnableLoad()" />
                                                                                    <br />
                                                                                    <asp:CheckBox id="chkCycle" cssclass="labelNormal" runat="server" onclick="EnableLoad()" />
                                                                                    <p>&nbsp;</p>
                                                                                </td>
                                                                                <td valign="top">
                                                                                    <br />
                                                                                    <asp:Label id="labelOutput" cssclass="labelNormal" runat="server" />
                                                                                    <br />
                                                                                    <asp:CheckBox id="chkStatistics" cssclass="labelNormal" runat="server" onclick="EnableLoad()" />
                                                                                    <br />
                                                                                    <asp:CheckBox id="chkDetails" cssclass="labelNormal" runat="server" onclick="EnableDisableCheckboxes()" />
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                        <table width="450px">
                                                                            <col width="320px" />
                                                                            <col width="130px" />
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label id="labelFormat" cssclass="labelNormal" runat="server" />
                                                                                    <asp:RadioButtonList id="radioListFormat" cssclass="labelNormal" runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal">
                                                                                        <asp:ListItem text="RTF or" selected="True" />
                                                                                        <asp:ListItem text="XML?" />
                                                                                    </asp:RadioButtonList>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:Button id="buttonFileLoad" runat="server" width="83px" height="21px" OnClientClick="LoadClick();" />
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td />
                                                        <td>
                                                            <table>
                                                                <tr>
                                                                    <td class="cellOutlined">
                                                                        <asp:Label id="labelTemplate" cssclass="labelSubHeading" runat="server" />
                                                                        <br />
                                                                        <asp:Label id="labelTemplateDescription" cssclass="labelNormal" runat="server" />
                                                                        <asp:Label id="linkDescription" cssclass="labelNormal" runat="server" />
                                                                        <br />
                                                                        <asp:Label id="linkTemplate" cssclass="labelNormal" runat="server" />
                                                                        <p>&nbsp;</p>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:Label id="labelMessageArea" cssclass="labelNormal" runat="server" visible="false" />
                                                        </td>
                                                    </tr>
                                                </table>
                                                <br />
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:Label id="labelResults" runat="server" cssclass="labelNormal" />
                                                        </td>
                                                        <td align="right" valign="bottom">
                                                            <!-- table nav controls -->
                                                            <asp:LinkButton id="linkFirst" cssclass="labelNormal" runat="server">&lt;&lt;</asp:LinkButton>
                                                            <asp:LinkButton id="linkPrevious" cssclass="labelNormal" runat="server"></asp:LinkButton>
                                                            <asp:Label id="labelNavControls" cssclass="labelNormal" runat="server" />
                                                            <asp:LinkButton id="linkNext" cssclass="labelNormal" runat="server"></asp:LinkButton>
                                                            <asp:LinkButton id="linkLast" cssclass="labelNormal" runat="server">&gt;&gt;</asp:LinkButton>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <table class="tableBatches" width="850px">
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label id="labelTableBatches" cssclass="labelSubHeading" runat="server" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:repeater id="batchesRepeater" runat="server" OnItemDataBound="batchesRepeater_ItemDatabound">
	                                                                        <headertemplate>
		                                                                        <table cellspacing="0" class="tableBatches" summary="Batches Tables" lang="en" width="830px">
		                                                                            <col width="7%" />
		                                                                            <col width="9%" />
		                                                                            <col width="9%" />
		                                                                            <col width="5%" />
		                                                                            <col width="7%" />
		                                                                            <col width="8%" />
		                                                                            <col width="7%" />
		                                                                            <col width="8%" />
		                                                                            <col width="8%" />
		                                                                            <col width="9%" />
		                                                                            <col width="7%" />
		                                                                            <col width="9%" />
		                                                                            <col width="7%" />
			                                                                        <thead>
				                                                                        <tr bgcolor="#ffffff" valign="top">
					                                                                        <td align="center" class="headerCell">
					                                                                            <asp:LinkButton id="header1"  runat="server" class="linkHeader" /> 
					                                                                        </td>
					                                                                        <td align="center" class="headerCell">
					                                                                            <asp:LinkButton id="header2"  runat="server" class="linkHeader" /> 
					                                                                        </td>
					                                                                        <td align="center" class="headerCell">
					                                                                            <asp:LinkButton id="header3"  runat="server" class="linkHeader" /> 
					                                                                        </td>
					                                                                        <td align="center" class="headerCell">
					                                                                            <asp:LinkButton id="header4"  runat="server" class="linkHeader" /> 
					                                                                        </td>
					                                                                        <td align="center" class="headerCell">
					                                                                            <asp:LinkButton id="header5"  runat="server" class="linkHeader" /> 
					                                                                        </td>
					                                                                        <td align="center" class="headerCell">
					                                                                            <asp:LinkButton id="header6"  runat="server" class="linkHeader" /> 
					                                                                        </td>
					                                                                        <td align="center" class="headerCell">
					                                                                            <asp:LinkButton id="header7"  runat="server" class="linkHeader" /> 
					                                                                        </td>
					                                                                        <td align="center" class="headerCell">
					                                                                            <asp:LinkButton id="header13"  runat="server" class="linkHeader" /> 
					                                                                        </td>
					                                                                        <td align="center" class="headerCell">
					                                                                            <asp:LinkButton id="header9"  runat="server" class="linkHeader" /> 
					                                                                        </td>
					                                                                        <td align="center" class="headerCell">
					                                                                            <asp:LinkButton id="header8"  runat="server" class="linkHeader" /> 
					                                                                        </td>
					                                                                        <td align="center" class="headerCell">
					                                                                            <asp:LinkButton id="header10"  runat="server" class="linkHeader" /> 
					                                                                        </td>
					                                                                        <td align="center" class="headerCell">
					                                                                            <asp:LinkButton id="header11"  runat="server" class="linkHeader" /> 
					                                                                        </td>
					                                                                        <td align="center" class="headerCell">
					                                                                            <asp:Label id="header12"  runat="server" class="linkHeader" /> 
					                                                                        </td>
				                                                                        </tr>
			                                                                        </thead>
			                                                                        <tbody>
	                                                                        </headertemplate>
	                                                                        <itemtemplate>
			                                                                        <tr>
				                                                                        <td id="cell1" runat="server"></td>
				                                                                        <td id="cell2" runat="server"></td>
				                                                                        <td id="cell3" runat="server"><img id="imagePtTick" src="" runat="server" alt="Tick" /></td>
				                                                                        <td id="cell4" runat="server"><img id="imageCarTick" src="" runat="server" alt="Tick" /></td>
				                                                                        <td id="cell5" runat="server"><img id="imageCycleTick" src="" runat="server" alt="Tick" /></td>
				                                                                        <td id="cell6" runat="server"></td>
				                                                                        <td id="cell7" runat="server"><asp:Label ID="infoLabel" Visible="false" runat="server" Width="323px" CssClass="infoLabel" /></td>
				                                                                        <td id="cell13" runat="server"></td>
				                                                                        <td id="cell9" runat="server"></td>
				                                                                        <td id="cell8" runat="server"></td>
				                                                                        <td id="cell10" runat="server"></td>
				                                                                        <td id="cell11" runat="server"></td>
				                                                                        <td id="cell12" runat="server">
				                                                                            <asp:RadioButton id="radioSelect" groupname="radioBatch" runat="server" />
				                                                                        </td>
			                                                                        </tr>
	                                                                        </itemtemplate>
	                                                                        <footertemplate>
	                                                                                <tr>
	                                                                                    <td colspan="13" align="center">
	                                                                                        <asp:Label ID="labelNoData" CssClass="cellNormal" Visible="false" runat="server" />
	                                                                                    </td>
	                                                                                </tr>
		                                                                        </tbody></table>
	                                                                        </footertemplate>
                                                                        </asp:repeater>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <cc1:tdbutton id="buttonReload" runat="server" cssclass="TDButtonDefault" onmouseover="this.className='TDButtonDefaultMouseOver'" />
                                                        </td>
                                                        <td align="right">
                                                            <cc1:tdbutton id="buttonDownload" runat="server" cssclass="TDButtonDefault" onmouseover="this.className='TDButtonDefaultMouseOver'" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <p>&nbsp;</p>
                                                            <asp:Label id="labelFaq" cssclass="labelNormal" runat="server" />
                                                            <p>&nbsp;</p>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                        </cc1:RoundedCornerControl>
                    </td>
                </tr>
            </table>
            <uc1:FooterControl id="FooterControl1" runat="server" EnableViewState="False"></uc1:FooterControl>
        </form>
    </div>
</body>
</html>
