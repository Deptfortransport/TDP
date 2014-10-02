<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls"
    Assembly="td.userportal.web" %>
<%@ Register TagPrefix="uc1" TagName="SimpleDateControl" Src="SimpleDateControl.ascx" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="ShowNewsControl.ascx.cs"
    Inherits="TransportDirect.UserPortal.Web.Controls.ShowNewsControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<div id="boxtypetwoaltnoborder" >
    <table width="100%" cellspacing="0" cellpadding="0">
        <tr id="warningLabelRow" runat="server">
            <td colspan="5" align="left" class="alerterror">
                <asp:Label ID="warningLabel" CssClass="txtsevenb" EnableViewState="False"
                    runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td colspan="5" align="left">
                <asp:Label ID="filterTitleLabel" CssClass="txtsevenb" EnableViewState="False"
                    runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td valign="top">
                <cc1:tdbutton id="backButton" runat="server" style="display:none;" ></cc1:tdbutton>
            </td>
            <td width="28%">
                <table cellspacing="0" cellpadding="0">
                    <tr>
                        <td>
                            <asp:label id="headingRegion" associatedcontrolid="regionsList" cssclass="txtsevenb" runat="server" enableviewstate="False"></asp:label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" runat="server" id="regionsCell">
                            <asp:dropdownlist id="regionsList" runat="server" style="WIDTH: 160px;" ></asp:dropdownlist>
                        </td>
                    </tr>
                    <tr>
                        <td>
                             <asp:Label ID="futureIncidentsLabel" CssClass="txtsevenb" EnableViewState="False"
                                runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                             <uc1:simpledatecontrol id="dateSelect" runat="server"></uc1:simpledatecontrol>
                        </td>
                    </tr>
                </table>
            </td>
            <td width="35%">
                <table cellspacing="0" cellpadding="0">
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="filterTravelNewsLabel" associatedcontrolid="transportDropList" CssClass="txtsevenb" EnableViewState="false"
                                runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr id="transportRow" runat="server">
                        <td align="right">
                            <asp:Label ID="transportLabel" CssClass="txtsevenb" EnableViewState="False" runat="server"></asp:Label>&nbsp;</td>
                        <td align="right">
                            <asp:DropDownList ID="transportDropList" runat="server" Width="169" >
                            </asp:DropDownList></td>
                    </tr>
                    <tr id="delayRow" runat="server">
                        <td align="right" style="height: 6px">
                            <asp:Label ID="delaysLabel" associatedcontrolid="delaysDropList" CssClass="txtsevenb" EnableViewState="False" runat="server"></asp:Label>&nbsp;</td>
                        <td align="right" style="height: 7px">
                            <asp:DropDownList ID="delaysDropList" runat="server" Width="169" >
                            </asp:DropDownList></td>
                    </tr>
                    <tr id="incidentTypeRow" runat="server">
                        <td align="right" style="height: 6px">
                            <asp:Label ID="incidentTypeLabel" associatedcontrolid="incidentTypeDropList" CssClass="txtsevenb" EnableViewState="False" runat="server"></asp:Label>&nbsp;
                        </td>
                        <td align="right" style="height: 7px">
                            <asp:DropDownList ID="incidentTypeDropList" runat="server" Width="169">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </td>
            <td width="30%">
                <table cellspacing="0" cellpadding="0">
                    <tr>
                        <td>
                            <asp:Label ID="searchTitleLabel" CssClass="txtsevenb" EnableViewState="False" runat="server"></asp:Label></td>
                    </tr>
                    <tr runat="server" id="searchPhraseRow">
                        <td align="center" valign="middle">
                            <div style="padding-top:2px; padding-bottom:2px;">
                                <asp:TextBox ID="searchInputText" runat="server" EnableViewState="False" Width="207"></asp:TextBox>
                            </div></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="searchExampleLabel" associatedcontrolid="searchInputText" CssClass="txtseven" EnableViewState="False" runat="server"></asp:Label></td>
                    </tr>
                </table>
            </td>
            <td align="right" valign="bottom">
                <asp:CheckBox ID="saveCheckBox" CssClass="txtseven" runat="server" Visible="False"></asp:CheckBox>&nbsp;&nbsp;&nbsp;&nbsp;
                <cc1:tdbutton id="showButton" enableviewstate="False" runat="server"></cc1:tdbutton>
            </td>
            
        </tr>
    </table>
    
</div>
