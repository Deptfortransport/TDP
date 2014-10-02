<%@ Control Language="c#" AutoEventWireup="True" Codebehind="OperatorSelectionControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.OperatorSelectionControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<%@ Import namespace="TransportDirect.UserPortal.AirDataProvider" %>
<div><h4 style="DISPLAY: inline"><asp:label id="labelOperatorSelectionTitle" runat="server"></asp:label></h4>
</div>
<asp:panel id="explanationPanel" runat="server">
    <asp:label id="labelOperatorSelectionExplanation" runat="server" cssclass="txtseven"></asp:label>
</asp:panel>
<asp:panel id="ambiguityMessage" runat="server" visible="False">
    <asp:label id="labelAmbiguityMessage" runat="server" cssclass="txtseven"></asp:label>
</asp:panel>
<asp:panel id="highlightPanel" runat="server">
    <div>
        <asp:label id="labelSRrblistOperatorOptions" runat="server" cssclass="screenreader"></asp:label>
        <asp:radiobuttonlist id="rblistOperatorOptions" runat="server" cssclass="txtseven" repeatdirection="Horizontal"></asp:radiobuttonlist>
        <asp:label id="labelOperatorOptionsFixed" runat="server" cssclass="txtsevenb"></asp:label></div>
    <div>
        <asp:datalist id="dlistOperators" runat="server" repeatdirection="Vertical" repeatcolumns="3" enableviewstate="False">
            <itemtemplate>
                <cc1:scriptablecheckbox id="checkOperator" runat="server" CssClassDisabled="txtseveng" CssClassEnabled="txtseven" Value="<%# ((AirOperator)Container.DataItem).IATACode %>" text="<%# ((AirOperator)Container.DataItem).Name %>" enableviewstate="true">
                </cc1:scriptablecheckbox>&nbsp;&nbsp;
            </itemtemplate>
        </asp:datalist>
        <asp:datalist id="dlistOperatorsFixed" runat="server" repeatdirection="Vertical" repeatcolumns="1">
            <itemstyle cssclass="txtseven"></itemstyle>
            <itemtemplate>
                <asp:label runat="server" id="labelOperator">
                    <%# GetOperatorName((string)Container.DataItem) %>
                </asp:label>&nbsp;&nbsp;
            </itemtemplate>
        </asp:datalist></div>
</asp:panel>
