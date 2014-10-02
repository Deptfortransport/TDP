<%@ Register TagPrefix="uc1" TagName="TravelDetailsControl" Src="../Controls/TravelDetailsControl.ascx" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="D2DPtJourneyChangesOptionsControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.D2DPtJourneyChangesOptionsControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>

<div class="boxtypetwo boxOptions">
    <div class="optionHeadingRow txtseven" aria-controls="ptPreferencesControl_journeyChangesOptionsControl_optionContentRow" role="button">
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
    <div id="optionContentRow" runat="server" class="optionContentRow hide" aria-expanded="false" aria-labelledby="ptPreferencesControl_journeyChangesOptionsControl_labelJsQuestion" role="region">
        <asp:panel id="panelChanges" runat="server">
            <div>
                <table>
	                <tr>
	                    <td width="7px"></td>
		                <td style="WIDTH: 10px; HEIGHT: 5px" align="left" colspan="2">
			                <asp:label id="labelChanges" runat="server" cssclass="txtsevenb"></asp:label></td>
	                </tr>
	                <tr>
	                    <td></td>
		                <td align="left" colspan="2">
			                <asp:label id="labelChangesShowTitle" associatedcontrolid="listChangesShow" runat="server" cssclass="txtseven"></asp:label>
			                <asp:dropdownlist id="listChangesShow" runat="server"></asp:dropdownlist>
			                <asp:label id="listChangesShowFixed" runat="server" cssclass="txtsevenb"></asp:label></td>
		                <td align="left" colspan="2"></td>
	                </tr>
	                <tr>
	                    <td></td>
		                <td style="WIDTH: 30px" align="left"></td>
		                <td align="left">
			                <asp:label id="labelChangesShowNote" runat="server" cssClass="txtnote"></asp:label></td>
	                </tr>
	                <tr>
	                    <td></td>
		                <td align="left" colspan="2">
			                <asp:label id="labelChangesSpeedTitle" associatedcontrolid="listChangesSpeed" runat="server" cssclass="txtseven"></asp:label>
			                <asp:dropdownlist id="listChangesSpeed" runat="server"></asp:dropdownlist>
			                <asp:label id="listChangesSpeedFixed" runat="server" cssclass="txtsevenb"></asp:label></td>
		                <td align="left" colspan="2"></td>
	                </tr>
	                <tr>
	                    <td></td>
		                <td style="WIDTH: 30px" align="left"></td>
		                <td align="left" colspan="2">
			                <asp:label id="labelChangesSpeedNote" runat="server" cssclass="txtnote"></asp:label></td>
	                </tr>
	                <tr>
	                    <td></td>
		                <td align="left" colspan="2"><span class="jpt">
				                <uc1:traveldetailscontrol id="loginSaveOption" runat="server"></uc1:traveldetailscontrol></span></td>
		                <td style="HEIGHT: 5px" align="left"><span class="jpt"></span></td>
	                </tr>
                </table>
            </div>
        </asp:panel>
    </div>
</div>