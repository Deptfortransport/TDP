<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls"
    Assembly="td.userportal.web" %>
<%@ Register TagPrefix="uc1" TagName="JourneyEmissionsSpeedoDial" Src="JourneyEmissionsSpeedoDial.ascx" %>
<%@ Register TagPrefix="uc1" TagName="HyperlinkPostbackControl" Src="HyperlinkPostbackControl.ascx" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="JourneyEmissionsDashboardControl.ascx.cs"
    Inherits="TransportDirect.UserPortal.Web.Controls.JourneyEmissionsDashboardControl"
    TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<div>
    
    <asp:Label runat="server" ID="fuelUsedTitle" CssClass="fuelUsedTitle"></asp:Label>
    <table summary="Journey Emissions Dashboard" width="100%" cellpadding="3" cellspacing="0"
        border="0">
        <tr>
            <td colspan="2">
                <asp:Panel ID="compareNotePanel" runat="server" EnableViewState="False">
                    <asp:Label ID="compareNote" runat="server" CssClass="txtseven" EnableViewState="false">[Compare journey note]</asp:Label>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td width="125" valign="top">
                <%--<asp:label id="journeyEmissionsControlTitle" runat="server" cssclass="txtsevenb" enableviewstate="false"></asp:label>
				<table width="100%" cellpadding="0" cellspacing="0" border="0">
					<tr height="125">
						<td></td>
					</tr>
					<tr>
						<td class="txtseven">
							<uc1:hyperlinkpostbackcontrol id="journeyCostsHyperlink" runat="server" visible="false" enableviewstate="false"></uc1:hyperlinkpostbackcontrol>
						</td>
					</tr>
				</table>--%>
            </td>
            
            <td align="center">
                <asp:Panel ID="graphicalDisplayFuelConsumptionPanel" runat="server" Visible="true">
                <div id="boxtypetwentyseven">
                    <table summary="Journey Emissions Dashboard Dial" width="100%" cellpadding="3" cellspacing="0"
                        border="0">
                        <tr align="center">
                            <td>
                                <uc1:JourneyEmissionsSpeedoDial ID="fuelCostSpeedoDial" runat="server" EnableViewState="false">
                                </uc1:JourneyEmissionsSpeedoDial>
                            </td>
                            <td>
                            </td>
                            <td>
                                <uc1:JourneyEmissionsSpeedoDial ID="emissionsSpeedoDial" runat="server" EnableViewState="false">
                                </uc1:JourneyEmissionsSpeedoDial>
                            </td>
                        </tr>
                        <tr align="center">
                            <td>
                                <asp:Label ID="fuelCostText" runat="server" CssClass="txtseven" EnableViewState="false">[Fuel cost]</asp:Label></td>
                            <td>
                            </td>
                            <td>
                                <asp:Label ID="emissionsText" runat="server" CssClass="txtseven" EnableViewState="false">[CO2 emissions]</asp:Label></td>
                        </tr>
                        <tr align="center">
                            <td>
                                <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                    <tr>
                                        <td align="right">
                                            <asp:Label ID="costTitle" runat="server" CssClass="txtseven" EnableViewState="false">[£]</asp:Label>&nbsp;</td>
                                        <td align="center" width="55" height="30" bgcolor="#ffffff">
                                            <asp:Label ID="fuelCostValue" runat="server" CssClass="txtnine" EnableViewState="false">[9]</asp:Label></td>
                                        <td width="36%">
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                            </td>
                            <td>
                                <table width="100%" cellpadding="0" cellspacing="0" border="0">
                                    <tr>
                                        <td width="40%">
                                        </td>
                                        <td align="center" width="55" height="30" bgcolor="#ffffff">
                                            <asp:Label ID="emissionsValue" runat="server" CssClass="txtnine" EnableViewState="false">[99]</asp:Label></td>
                                        <td align="left">
                                            &nbsp;<asp:Label ID="emissionsTitle" runat="server" CssClass="txtseven" EnableViewState="false">[Kg]</asp:Label></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr align="center">
                            <td>
                                <asp:Label ID="forYourJourneyText1" runat="server" CssClass="txtseven" EnableViewState="false">[For your journey]</asp:Label></td>
                            <td>
                            </td>
                            <td>
                                <asp:Label ID="forYourJourneyText2" runat="server" CssClass="txtseven" EnableViewState="false">[For your journey]</asp:Label></td>
                        </tr>
                    </table>
                </div>
                </asp:Panel>
            </td>
            <td width="125" valign="middle" class="txtseven">
                <%--<table border="0" width="100%" cellpadding="0" cellspacing="0">
					<tr>
						<td><asp:Label ID="saveFuelLabelLink" Runat="server" EnableViewState="False">[Save fuel label link]</asp:Label></td>
					</tr>
					<tr>
						<td><br><uc1:hyperlinkpostbackcontrol id="compareEmissionsHyperlink" runat="server" visible="true" enableviewstate="false"></uc1:hyperlinkpostbackcontrol></td>
					</tr>
				</table>--%>
            </td>
        </tr>
    </table>
    
    
    <asp:Panel ID="tableDisplayFuelConsumptionPanel" runat="server" Visible="false">
<div style="MARGIN-LEFT: 5px">
				<asp:table id="tableEmissionsMode" runat="server" BorderWidth="1"
					cellSpacing="0" borderColor="black">
					<asp:TableRow>
						<asp:tableheadercell cssclass="transportHeader" id="headerTransport">
							<asp:label  cssclass="txtseven" id="labelTransportHeader" EnableViewState=False runat="server"></asp:label></asp:tableheadercell>
						<asp:tableheadercell cssclass="emissionHeader" id="headerEmissions">
							<asp:label cssclass="txtseven" id="labelEmissionsHeader" EnableViewState=False runat="server"></asp:label></asp:tableheadercell>
						<asp:tableheadercell cssclass="costHeader" id="headerCost">
							<asp:label cssclass="txtseven" id="labelCostHeader" EnableViewState=False runat="server"></asp:label></asp:tableheadercell>
					</asp:TableRow>
					
					<asp:TableRow id="smallCarRow">
						<asp:tablecell cssclass="emissiondata">
							<asp:Label CssClass="txtseven" id="forYourJourneyText3" EnableViewState="false" runat="server"></asp:Label></asp:tablecell>
						<asp:tablecell cssclass="emissiondata">
						<asp:label id="emissionsValueTable" runat="server" cssclass="txtseven"></asp:label>
						<asp:Label ID="emissionsTitle2" runat="server" CssClass="txtseven" EnableViewState="false">[Kg]</asp:Label>
						</asp:tablecell>
						<asp:tablecell cssclass="emissiondata">
						<asp:Label ID="costTitle2" runat="server" CssClass="txtseven" EnableViewState="false">[£]</asp:Label>
						<asp:label id="fuelCostValueTable" runat="server" cssclass="txtseven"></asp:label>&nbsp;
						</asp:tablecell>
					</asp:TableRow>
					<asp:TableRow ID="yourCarRow" runat="server">
					    <asp:tablecell cssclass="emissiondata">
							<asp:Label CssClass="txtseven" id="forYourJourneyText4" EnableViewState="false" runat="server"></asp:Label></asp:tablecell>
						<asp:tablecell cssclass="emissiondata">
						<asp:label id="emissionsValueTable3" runat="server" cssclass="txtseven"></asp:label>
						<asp:Label ID="emissionsTitle3" runat="server" CssClass="txtseven" EnableViewState="false">[Kg]</asp:Label>
						</asp:tablecell>
						<asp:tablecell cssclass="emissiondata">
						<asp:Label ID="costTitle3" runat="server" CssClass="txtseven" EnableViewState="false">[£]</asp:Label>
						<asp:label id="fuelCostValueTable3" runat="server" cssclass="txtseven"></asp:label>&nbsp;
						</asp:tablecell>
					</asp:TableRow>
				</asp:table></div>
		&nbsp;
</asp:Panel>
    
    
    <table>
        <tr>
            <td width="125" valign="top">
            </td>
            <td align="right" valign="bottom" width="754px">
                <cc1:TDButton ID="journeyCosts" runat="server" enableviewstate="false"></cc1:TDButton>
                <cc1:TDButton ID="compareEmissions" runat="server" enableviewstate="false"></cc1:TDButton></td>
            <td width="108" valign="top">
            </td>
        </tr>
    </table>
</div>
