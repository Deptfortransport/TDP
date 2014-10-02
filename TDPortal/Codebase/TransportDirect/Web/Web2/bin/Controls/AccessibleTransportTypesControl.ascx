<%@ Control Language="c#" AutoEventWireup="True" Codebehind="AccessibleTransportTypesControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.AccessibleTransportTypesControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" EnableViewState="true" %>

<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>

<asp:panel id="transportTypesPanel" runat="server" CssClass="accessibleTransportTypes txtseven">
    <asp:label id="labelInstructions"  runat="server"></asp:label>
	<asp:checkboxlist id="checklistModesAccessibleTransport" runat="server" AutoPostBack="true" CssClass="accessibleTransportTypesChk" ></asp:checkboxlist>
	<noscript>
	    <div class="floatright">
            <cc1:TDButton ID="btnUpdate" runat="server"></cc1:TDButton>
        </div>
	</noscript>
</asp:panel>