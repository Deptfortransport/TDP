<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="AirportBrowseControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.AirportBrowseControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Import namespace="TransportDirect.UserPortal.AirDataProvider" %>
<table cellspacing="0">
    <tbody>
        <tr>
            <td class="findafromcolumn" align="right">
                <asp:label id="labelFrom" runat="server" cssclass="txtsevenb"></asp:label>
            </td>
            <td valign="top">
                <asp:panel id="highlightPanel" runat="server">
                    <div class="alertbox">
                        <asp:label id="labelAmbiguityMessage" runat="server" cssclass="txtseven"></asp:label>
                        <table lang="en" class="tableDetails" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:label id="labelSRdropMain" associatedcontrolid="dropMain" runat="server" cssclass="screenreader"></asp:label>
                                    <asp:label id="labelSRdropMainAmbiguous" runat="server" cssclass="screenreader"></asp:label>
                                </td>
                                <td>
                                    <div class="floatright" style="margin-top:2px;">
                                        <cc1:tdbutton id="buttonFindNearest" runat="server"></cc1:tdbutton>
                                    </div>
                                    <cc1:scriptabledropdownlist id="dropMain" runat="server" style="width: 260px"></cc1:scriptabledropdownlist>&nbsp;
                                    <cc1:scriptablepanel id="panelRegions" runat="server">
                                    <asp:repeater id="rptRegionPanels" runat="server">
                                            <itemtemplate>
                                                <div id="<%# GetRegionPanelName((AirRegion)Container.DataItem) %>" style="display: none;">
                                                    <asp:datalist id="dlistAirports" runat="server" repeatcolumns="2" repeatdirection="Vertical" repeatlayout="Table">
                                                        <itemtemplate>
                                                                <cc1:scriptablecheckbox id="checkAirport" Checked="true" CssClassDisabled="txtseveng" CssClassEnabled="txtseven" Value="<%# ((Airport)Container.DataItem).IATACode %>" Text="<%# ((Airport)Container.DataItem).Name %>" EnableClientScript="false" runat="server">
                                                                </cc1:scriptablecheckbox>&nbsp;&nbsp;
                                                        </itemtemplate>
                                                    </asp:datalist>
                                                </div>
                                            </itemtemplate>
                                        </asp:repeater>
                                    </cc1:scriptablepanel>
                                </td>
                            </tr>
                         </table>
                    </div>
                </asp:panel>
            </td>
        </tr>
    </tbody>
</table>
