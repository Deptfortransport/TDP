<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<%@ Register TagPrefix="uc1" TagName="FindPreferencesOptionsControl" Src="FindPreferencesOptionsControl.ascx" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="FindCarPreferencesControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.FindCarPreferencesControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="uc1" TagName="FindViaLocationControl" Src="FindViaLocationControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FindCarJourneyOptionsControl" Src="FindCarJourneyOptionsControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TravelDetailsControl" Src="../Controls/TravelDetailsControl.ascx" %>
<asp:panel id="preferencesPanel" runat="server">
	<uc1:findpreferencesoptionscontrol id="preferencesOptionsControl" runat="server"></uc1:findpreferencesoptionscontrol>	
        <asp:panel id="panelDivHider" runat="server">
		    <div class="boxtypetwo">
			<asp:panel id="panelTypeOfJourney" runat="server">
				<table>
					<tr>
						<td align="left" colspan="2">
						    <span class="jpt">
							    <uc1:traveldetailscontrol id="loginSaveOption" runat="server"></uc1:traveldetailscontrol>
							</span>
						</td>
					</tr>
					<tr>
						<td>
							<asp:label id="journeyTypeLabel" associatedcontrolid="listCarJourneyType" runat="server" cssclass="txtsevenb"></asp:label><br />
							<table lang="en" id="Table2" summary="Please enter your car travel preferences">
								<tr>
									<td>
										<asp:label id="displayTypeListLabel" runat="server" cssclass="txtsevenb"></asp:label>
										<asp:label id="findLabel" runat="server" cssclass="txtsevenr"></asp:label>
									</td>
									<td>
										<asp:dropdownlist id="listCarJourneyType" runat="server"></asp:dropdownlist>
										<asp:label id="journeysLabel" runat="server" cssclass="txtseven"></asp:label>
									</td>
								</tr>
								<tr>
									<td>
										<asp:label id="displaySpeedListLabel" runat="server" cssclass="txtsevenb"></asp:label>
									</td>
								</tr>
								<tr>
									<td valign="top">
										<asp:label id="displayDoNotUseMotorwaysLabel" runat="server" cssclass="txtsevenb"></asp:label>
										<asp:label id="carSpeedLabel" associatedcontrolid="listCarSpeed" runat="server" cssclass="txtseven"></asp:label>
									</td>
									<td>
										<asp:dropdownlist id="listCarSpeed" runat="server"></asp:dropdownlist><br />
										<asp:checkbox id="doNotUseMotorwaysCheckBox" runat="server" cssclass="txtseven"></asp:checkbox>
									</td>
								</tr>
							</table>
						</td>
					</tr>
				</table>
			</asp:panel>
			<asp:panel id="panelCarDetails" runat="server">
				<table>
					<tr>
						<td>
							<asp:label id="labelCarDetails" runat="server" cssclass="txtsevenb"></asp:label><br />
							<table cellspacing="0">
								<tr>
									<td style="WIDTH: 130px" align="left">
										<asp:label id="iHaveaLabel" associatedcontrolid="listCarSize" runat="server" cssclass="txtseven"></asp:label>
								    </td>
									<td>
										<asp:dropdownlist id="listCarSize" runat="server"></asp:dropdownlist>
										<asp:label id="sizedLabel" associatedcontrolid="listFuelType" runat="server" cssclass="txtseven"></asp:label>
										<asp:dropdownlist id="listFuelType" runat="server"></asp:dropdownlist>
										<asp:label id="carLabel" runat="server" cssclass="txtseven"></asp:label>
									</td>
								</tr>
								<tr>
									<td colspan="2">
										<asp:label id="displayCarDetailsLabel" runat="server" cssclass="txtsevenb"></asp:label>
									</td>
								</tr>
								<tr>
									<td align="left" colspan="2">
										<asp:label id="displayFuelConsumptionErrorLabel" runat="server" cssclass="txtseven"></asp:label>
									</td>
								</tr>
								<tr>
									<td style="WIDTH: 130px" valign="top">
										<asp:label id="myFuelConsumptionIsLabel" associatedcontrolid="fuelConsumptionSelectRadio" runat="server" cssclass="txtseven"></asp:label>
									</td>
									<td>
									    <fieldset>
										    <asp:radiobuttonlist id="fuelConsumptionSelectRadio" runat="server" cssclass="txtseven" repeatlayout="Flow"
											    repeatdirection="Vertical"></asp:radiobuttonlist>
										    <asp:textbox id="textFuelConsumption" runat="server" columns="10"></asp:textbox>
										    <asp:dropdownlist id="listFuelConsumptionUnit" runat="server"></asp:dropdownlist>
										</fieldset>
									</td>
								</tr>
								<tr>
									<td colspan="2">
										<asp:label id="displayFuelConsumptionLabel" runat="server" cssclass="txtsevenb"></asp:label>
									</td>
								</tr>
								<tr>
									<td align="left" colspan="2">
										<asp:label id="displayFuelCostErrorLabel" runat="server" cssclass="txtseven"></asp:label>
									</td>
								</tr>
								<tr>
									<td style="WIDTH: 130px" valign="top">
										<asp:label id="myFuelCostIsLabel" associatedcontrolid="fuelCostSelectRadio" runat="server" cssclass="txtseven"></asp:label>
									</td>
									<td>
									    <fieldset>
										    <asp:radiobuttonlist id="fuelCostSelectRadio" runat="server" cssclass="txtseven" repeatlayout="Flow"
											    repeatdirection="Vertical"></asp:radiobuttonlist>
										    <asp:textbox id="textFuelCost" runat="server" columns="10"></asp:textbox>
										    <asp:label id="pencePerLitreLabel" associatedcontrolid="textFuelCost" runat="server" cssclass="txtseven"></asp:label>
										</fieldset>
									</td>
								</tr>
								<tr>
									<td colspan="2">
										<asp:label id="displayFuelCostLabel" runat="server" cssclass="txtsevenb"></asp:label>
									</td>
								</tr>
							</table>
						</td>
					</tr>
				</table>
			</asp:panel>
            </div>
        </asp:panel>
	<uc1:findcarjourneyoptionscontrol id="journeyOptionsControl" runat="server"></uc1:findcarjourneyoptionscontrol>
</asp:panel>

