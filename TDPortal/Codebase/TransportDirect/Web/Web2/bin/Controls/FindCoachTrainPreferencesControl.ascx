<%@ Control Language="c#" AutoEventWireup="True" Codebehind="FindCoachTrainPreferencesControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.FindCoachTrainPreferencesControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="uc1" TagName="FindPreferencesOptionsControl" Src="FindPreferencesOptionsControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FindViaLocationControl" Src="FindViaLocationControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FindPageOptionsControl" Src="FindPageOptionsControl.ascx" %>
<asp:panel id="panelPreferences" runat="server" visible="False">
    <uc1:findpreferencesoptionscontrol id="preferencesOptionsControl" runat="server"></uc1:findpreferencesoptionscontrol>
    <asp:panel id="panelChanges" visible="False" runat="server">
        <div class="boxtypetwo">
            <table lang="en" style="WIDTH: 560px" cellspacing="0" cellpadding="2" border="0">
                <tr>
                    <td align="left">
                        <div style="FLOAT: left; WIDTH: 100px">
                            <asp:label id="labelChanges" runat="server" cssclass="txtsevenb"></asp:label>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <asp:label id="labelChangesNote" runat="server" cssclass="txtseven"></asp:label>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <asp:label id="labelChangesShowTitle" associatedcontrolid="listChangesShow" runat="server" cssclass="txtseven"></asp:label>
                        <asp:dropdownlist id="listChangesShow" runat="server"></asp:dropdownlist>
                        <asp:label id="listChangesShowFixed" runat="server" cssclass="txtsevenb"></asp:label>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <asp:label id="labelChangesSpeedTitle" associatedcontrolid="listChangesSpeed" runat="server" cssclass="txtseven"></asp:label>
                        <asp:dropdownlist id="listChangesSpeed" runat="server"></asp:dropdownlist>
                        <asp:label id="listChangesSpeedFixed" runat="server" cssclass="txtsevenb"></asp:label>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <asp:label id="labelChangesSpeedNote" runat="server" cssclass="txtnote"></asp:label></td>
                </tr>
            </table>
        </div>
    </asp:panel>
    <uc1:findvialocationcontrol id="viaLocationControl" runat="server"></uc1:findvialocationcontrol>
    
</asp:panel>
