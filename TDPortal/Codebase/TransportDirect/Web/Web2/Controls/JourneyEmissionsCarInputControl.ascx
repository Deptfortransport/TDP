<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="JourneyEmissionsCarInputControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.JourneyEmissionsCarInputControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<div>
	<table cellspacing="0" cellpadding="3" width="100%" summary="Journey Emissions Car Input Details"
		border="0">
		<tr>
			<td colspan="4"><asp:label id="carInputDetailsTitle" cssclass="txtsevenb" runat="server">[Check fuel consumption]</asp:label>
			</td>
		</tr>
		<tr>
			<td width="15"></td>
			<td colspan="3">
				<asp:Panel id="panelFirstRow" runat="server" cssclass="panelFirstRow">
					<table cellspacing="0" cellpadding="0" width="100%">
						<tr>
							<td>
							    <asp:label id="carInputDetailsSubHeading" cssclass="txtseven" runat="server">[Enter your car details]</asp:label>
							</td>
							<td>
								<asp:Panel id="panelCarImages" runat="server" cssclass="txtseven panelCarImages">
									<cc1:tdimage id="imageSmallCar" runat="server"></cc1:tdimage>&nbsp;&nbsp;&nbsp;&nbsp;
									<cc1:tdimage id="imageMediumCar" runat="server"></cc1:tdimage>&nbsp;&nbsp;&nbsp;&nbsp;
									<cc1:tdimage id="imageLargeCar" runat="server"></cc1:tdimage>
								</asp:Panel>
							</td>
						</tr>
					</table>					
				</asp:Panel>						
			</td>
		</tr>
		<tr>
			<td></td>
			<td colspan="3">
				<asp:panel id="panelCarDetails" runat="server">
					<table cellspacing="0" cellpadding="0" border="0">
						<tr>
							<td align="left" colspan="2">
								<table cellspacing="0" cellpadding="3" width="100%" border="0">
									<tr>
										<td style="WIDTH: 140px" align="right">
											<asp:label id="iHaveaLabel" runat="server" cssclass="txtseven"></asp:label></td>
										<td>&nbsp;
											<asp:dropdownlist id="listCarSize" runat="server"></asp:dropdownlist>
											<asp:label id="sizedLabel" runat="server" cssclass="txtseven"></asp:label>
											<asp:dropdownlist id="listFuelType" runat="server"></asp:dropdownlist>
											<asp:label id="carLabel" runat="server" cssclass="txtseven"></asp:label></td>
									</tr>
								</table>
							</td>
						</tr>
						<tr>
							<td align="left" colspan="2">
								<asp:Panel id="panelCarDetailsMore" runat="server">
									<table cellspacing="0" cellpadding="3" width="100%" border="0">
										<tr>
											<td style="WIDTH: 140px" valign="top" align="right">
												<asp:label id="myFuelConsumptionIsLabel" runat="server" cssclass="txtseven"></asp:label></td>
											<td>
												<table cellspacing="0" cellpadding="0" width="100%" border="0">
													<tr>
														<td valign="top" width="185" rowspan="3">
															<asp:radiobuttonlist id="fuelConsumptionSelectRadio" runat="server" cssclass="txtseven" repeatdirection="Vertical"
																repeatlayout="Table" autopostback="True"></asp:radiobuttonlist></td>
														<td></td>
													</tr>
													<tr>
														<td></td>
														<td>
														    <br />
															<asp:textbox id="textFuelConsumption" runat="server" columns="10"></asp:textbox>
															<asp:dropdownlist id="listFuelConsumptionUnit" runat="server"></asp:dropdownlist></td>
													</tr>
													<tr>
														<td></td>
														<td>
															<asp:textbox id="textCO2Consumption" runat="server" columns="10"></asp:textbox></td>
													</tr>
												</table>
											</td>
										</tr>
										<tr>
											<td align="left" colspan="2">
												<asp:label id="displayFuelConsumptionErrorLabel" runat="server" cssclass="txtseven"></asp:label></td>
										</tr>
										<tr>
											<td style="WIDTH: 140px" valign="top" align="right">
												<asp:label id="myFuelCostIsLabel" runat="server" cssclass="txtseven"></asp:label></td>
											<td valign="top">
												<asp:radiobuttonlist id="fuelCostSelectRadio" AutoPostBack="True" runat="server" cssclass="txtseven" repeatdirection="Vertical"
													repeatlayout="Flow"></asp:radiobuttonlist>
												<asp:textbox id="textFuelCost" runat="server" columns="10"></asp:textbox>
												<asp:label id="pencePerLitreLabel" runat="server" cssclass="txtseven"></asp:label></td>
										</tr>
										<tr>
											<td align="left" colspan="2">
												<asp:label id="displayFuelCostErrorLabel" runat="server" cssclass="txtseven"></asp:label></td>
										</tr>
									</table>
								</asp:Panel></td>
						</tr>
					</table>
				</asp:panel>
			</td>
		</tr>
	</table>
</div>
