<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="D2DLocationControlVia.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.D2DLocationControlVia" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<%@ Register TagPrefix="uc1" TagName="LocationControl" Src="LocationControl.ascx" %>

<div class="boxtypetwo boxOptions">
    <div class="optionHeadingRow txtseven" aria-controls="ptPreferencesControl_locationControlVia_optionContentRow" role="button">
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
    <div id="optionContentRow" runat="server" class="optionContentRow hide" aria-expanded="false" aria-labelledby="ptPreferencesControl_locationControlVia_labelJsQuestion" role="region">
        <table cellspacing="0" width="100%">
            <tbody>
                <tr>
	                <td width="12px"></td>
	                <td colspan="2">
                        <asp:label id="labelJourneyOptions" runat="server" CssClass="txtsevenb"></asp:label>
	                </td>
                </tr>
	            <tr>
	                <td></td>
		            <td><asp:label id="travelViaLabel" runat="server" cssclass="txtseven"></asp:label></td>
		            <td></td>
	            </tr>
	            <tr>
	                <td></td>
		            <td colspan="2">
		                <asp:label id="instructionLabel" runat="server" cssclass="txtseven"></asp:label>
		            </td>
	            </tr>
	            <tr>
	                <td></td>
	                <td colspan="2">
	                    <uc1:LocationControl ID="locationControl" runat="server" />
	                </td>
	            </tr>
            </tbody>
        </table>
    </div>
</div>