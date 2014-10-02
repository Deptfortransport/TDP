<%@ Control Language="c#" AutoEventWireup="True" Codebehind="D2DTransportTypesControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.D2DTransportTypesControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" EnableViewState="true" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>

<div class="boxtypetwo boxOptions txtseven">
    <div class="optionHeadingRow">
        <div class="boxArrowToggle">
        </div>
        <div class="boxOptionHeading">
            <asp:Label ID="labelJsQuestion" runat="server" />
        </div>
        <div class="boxOptionSelected">
            <asp:Label ID="labelOptionsSelected" runat="server" CssClass="labelJsRed hide" />
            <noscript>
                <cc1:tdbutton id="btnShow" runat="server" OnClick="btnShow_Click"></cc1:tdbutton>
            </noscript>
        </div>
    </div>
    <div id="optionContentRow" runat="server" class="optionContentRow hide">
        <table>
            <tr>
                <td width="8px"></td>
                <td>
                    <asp:label id="labelSRJourneyType" runat="server" cssclass="screenreader"></asp:label>
                    <strong>
                        <asp:label id="labelType" runat="server"></asp:label>
                    </strong>
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <asp:panel id="transportTypesPanel" runat="server">
	                    <asp:label id="labelTickAllTypes"  runat="server"></asp:label>
	                    <asp:checkboxlist id="checklistModesPublicTransport" runat="server" repeatcolumns="4"></asp:checkboxlist>
	                    <asp:label id="labelPublicModesNote"  runat="server"></asp:label>
                    </asp:panel>
                </td>
            </tr>
        </table>               
    </div>
</div>