<%@ Control Language="c#" AutoEventWireup="True" Codebehind="MapLocationControl.ascx.cs"
    Inherits="TransportDirect.UserPortal.Web.Controls.MapLocationControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="uc1" TagName="MapFindInformationLocationControl" Src="MapFindInformationLocationControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="MapPlanJourneyLocationControl" Src="MapPlanJourneyLocationControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="MapSelectLocationControl" Src="MapSelectLocationControl.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls"
    Assembly="td.userportal.web" %>
<table id="mapLocationControl" lang="en" summary="" border="0" width="100%" cellspacing="2px">
    <tr id="headerTextRow" runat="server">
        <td>
            <h1><asp:Label ID="labelPlanAJourney" runat="server"  EnableViewState="false"></asp:Label>
            <asp:Label ID="labelFindInformation" runat="server"  EnableViewState="false"></asp:Label>
            <asp:Label ID="labelSelectLocation" runat="server"  EnableViewState="false"></asp:Label>
            </h1>
        </td>
    </tr>
    <tr>
        <td class="txtseven">
            <table cellpadding="0px" cellspacing="0px">
                <tr>
                    <td>
                        <asp:Label ID="labelOptions" runat="server" EnableViewState="false"></asp:Label>
                        <asp:Label ID="labelOptions2" runat="server" EnableViewState="false"></asp:Label>
                    </td>
                    <td align="right">
                        <div id="headerButtonRow" runat="server" class="DisplayInTable">
                            <cc1:TDButton ID="buttonPlanAJourney" runat="server" CausesValidation="false" Action="return Plan();"
                                ScriptName="MapLocationControl" EnableViewState="false"></cc1:TDButton>
                            <cc1:TDButton ID="buttonFindInformation" runat="server" CausesValidation="false"
                                Action="return FindInformation(false);" ScriptName="MapLocationControl" EnableViewState="false"
                                Visible="false"></cc1:TDButton>
                            <cc1:TDButton ID="buttonSelectLocation" runat="server" CausesValidation="false" Action="return SelectLocation(false);"
                                ScriptName="MapLocationControl" EnableViewState="false"></cc1:TDButton>
                        </div>
                    </td>
                </tr>
            </table>
            <uc1:MapPlanJourneyLocationControl ID="mapPlanJourneyLocationControl" runat="server">
            </uc1:MapPlanJourneyLocationControl>
            <uc1:MapFindInformationLocationControl ID="mapFindInformationLocationControl" runat="server">
            </uc1:MapFindInformationLocationControl>
            <uc1:MapSelectLocationControl ID="mapSelectLocationControl" runat="server"></uc1:MapSelectLocationControl>
            <input type="hidden" id="<%# GetHiddenInputId %>" name="<%# GetHiddenInputId %>"
                value="<%# GetJourneyMapState%>" />
        </td>
    </tr>
</table>
