<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<%@ Register TagPrefix="uc1" TagName="D2DPreferencesOptionsControl" Src="D2DPreferencesOptionsControl.ascx" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="D2DCarPreferencesControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.D2DCarPreferencesControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="uc1" TagName="FindViaLocationControl" Src="FindViaLocationControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="D2DCarJourneyOptionsControl" Src="D2DCarJourneyOptionsControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TravelDetailsControl" Src="../Controls/TravelDetailsControl.ascx" %>

<asp:panel id="preferencesPanel" runat="server" visible="False">
    <div class="boxtypetwo boxOptions">
        <div class="optionHeadingRow txtseven" aria-controls="locationControlVia_optionContentRow" role="button">
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
        <div id="optionContentRow" runat="server" class="optionContentRow hide" aria-expanded="false" aria-labelledby="locationControlVia_labelJsQuestion" role="region">
            <table>
                <tr>
                    <td width="5px"></td>
                    <td>
                        <asp:panel id="panelDivHider" runat="server">
                            <div>
                                <asp:panel id="panelTypeOfJourney" runat="server">
                                    <table>
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
						                                    <asp:label id="doNotUseMotorwaysLabel" runat="server" cssclass="txtseven"></asp:label>
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
                        <uc1:d2dcarjourneyoptionscontrol id="journeyOptionsControl" runat="server"></uc1:d2dcarjourneyoptionscontrol>
                        <span class="jpt">
                            <uc1:traveldetailscontrol id="loginSaveOption" runat="server"></uc1:traveldetailscontrol>
                        </span>
                        <uc1:d2dpreferencesoptionscontrol id="preferencesOptionsControl" runat="server"></uc1:d2dpreferencesoptionscontrol>	
                    </td>
                </tr>
            </table>
        </div>                
    </div>
</asp:panel>

