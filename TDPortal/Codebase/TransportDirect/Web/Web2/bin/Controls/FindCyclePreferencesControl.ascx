<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FindCyclePreferencesControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.FindCyclePreferencesControl" %>
<%@ Register TagPrefix="uc1" TagName="triStateLocationControl2" Src="TriStateLocationControl2.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FindPageOptionsControl" Src="FindPageOptionsControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TravelDetailsControl" Src="../Controls/TravelDetailsControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="LocationControl" Src="LocationControl.ascx" %>

<asp:panel id="preferencesPanel" runat="server" visible="False" enableviewstate="true">
    <div class="boxtypeeightalt" >
        <asp:label id="labelCycleJourneyOptions" runat="server" cssclass="txteightb" enableviewstate="false"></asp:label>
    </div>
    <div class="boxtypetwo">
        <asp:panel id="panelCycleDetails" runat="server" enableviewstate="true">
        
            <div id="divLogin" runat="server" class="txtseven" enableviewstate="false">
                <uc1:traveldetailscontrol id="loginSaveOption" runat="server"></uc1:traveldetailscontrol>
            </div>
            
            <div class="clearboth"></div>
            
            <div id="divSpeed" runat="server" enableviewstate="true">
                <asp:label id="displaySpeedDetailsLabel" runat="server" cssclass="txtsevenb" enableviewstate="false"></asp:label>
                <asp:label id="displaySpeedErrorLabel" runat="server" cssclass="txtseven" enableviewstate="false"></asp:label>
                <asp:label id="labelSpeedMax" associatedcontrolid="textSpeed" runat="server" cssclass="txtseven" enableviewstate="false"></asp:label>
                &nbsp;
                <asp:textbox id="textSpeed" runat="server" columns="10" enableviewstate="true"></asp:textbox>
                <asp:dropdownlist id="listSpeedUnit" runat="server" enableviewstate="true" CssClass="cycleSpeedUnitsDropDown"></asp:dropdownlist>
            </div>
            
            <div class="clearboth"></div>
            
            <div id="divIPrefer" runat="server" enableviewstate="true">
                <asp:label id="displayIPreferDetailsLabel" runat="server" cssclass="txtsevenb" enableviewstate="false"></asp:label>
                <div class="floatleftonly" id="divIPreferTitle" runat="server" enableviewstate="false">
                    <asp:label id="labelIPrefer" runat="server" cssclass="txtseven" enableviewstate="false"></asp:label>
                </div>
                <div class="floatleftonly" id="divIPreferCheckBoxes" runat="server" enableviewstate="true">
                    <asp:checkbox id="checkboxAvoidUnlitRoads" runat="server" cssclass="txtseven" enableviewstate="true"></asp:checkbox>&nbsp;
                    <br />
				    <asp:checkbox id="checkboxAvoidWalkingYourBike" runat="server" cssclass="txtseven" enableviewstate="true"></asp:checkbox>&nbsp;
				    <br />
				    <div style="display:none">
				    <asp:checkbox id="checkboxAvoidSteepClimbs" runat="server" cssclass="txtseven" enableviewstate="true"></asp:checkbox>&nbsp;
				    <br />
				    </div>
				    <asp:checkbox id="checkboxAvoidTimeBased" runat="server" cssclass="txtseven" enableviewstate="true"></asp:checkbox>&nbsp;
				</div>
            </div>
            
            <div class="clearboth"></div>
            
            <div id="divViaLocation" runat="server" enableviewstate="true">
                <asp:label id="labelTravelVia" runat="server" cssclass="txtseven" enableviewstate="false"></asp:label>
                <br />
                <uc1:tristatelocationcontrol2 id="locationTristateControl" runat="server"></uc1:tristatelocationcontrol2>
                <div class="locationControlViaCycle">
		            <uc1:LocationControl ID="locationControl" runat="server" />
			    </div>
            </div>
            
            <div id="divAdditionalPreferences" runat="server" visible="false">
                <div>
                    <asp:label id="labelLocationOverride" runat="server" cssclass="txtsevenb" enableviewstate="false"></asp:label>&nbsp;
                    <br />
                    <asp:label id="labelOriginOverride" runat="server" cssclass="txtseven" enableviewstate="false"></asp:label>&nbsp;
                    <asp:label id="labelOriginEasting" runat="server" cssclass="txtseven" enableviewstate="false"></asp:label>
                    <asp:textbox id="textOriginEasting" runat="server" cssclass="txtseven" CausesValidation="false" MaxLength="6" Columns="6" enableviewstate="true"></asp:textbox>&nbsp;
                    <asp:label id="labelOriginNorthing" runat="server" cssclass="txtseven" enableviewstate="false"></asp:label>
                    <asp:textbox id="textOriginNorthing" runat="server" cssclass="txtseven" CausesValidation="false" MaxLength="6" Columns="6" enableviewstate="true"></asp:textbox>&nbsp;
                    <asp:label id="labelDestinationOverride" runat="server" cssclass="txtseven" enableviewstate="false"></asp:label>&nbsp;
                    <asp:label id="labelDestinationEasting" runat="server" cssclass="txtseven" enableviewstate="false"></asp:label>    
                    <asp:textbox id="textDestinationEasting" runat="server" cssclass="txtseven" CausesValidation="false" MaxLength="6" Columns="6" enableviewstate="true"></asp:textbox>&nbsp;
                    <asp:label id="labelDestinationNorthing" runat="server" cssclass="txtseven" enableviewstate="false"></asp:label>
                    <asp:textbox id="textDestinationNorthing" runat="server" cssclass="txtseven" CausesValidation="false" MaxLength="6" Columns="6" enableviewstate="true"></asp:textbox>
                    <br />
                </div>
                <asp:label id="displayPenaltyFunction" runat="server" cssclass="txtsevenb" enableviewstate="false"></asp:label>
                <asp:label id="labelPenaltyFunctionOverride" runat="server" cssclass="txtsevenb" enableviewstate="false"></asp:label>&nbsp;
                <asp:label id="labelPenaltyFunctionOverrideHelp" runat="server" cssclass="txtsevenb" enableviewstate="false"></asp:label>
                <div class="clearboth"></div>
                <asp:label id="labelPenaltyFunctionOverrideCall" runat="server" cssclass="txtseven" enableviewstate="false"></asp:label>
                <div class="clearboth"></div>
                <asp:textbox id="textboxPenaltyFunctionOverride" runat="server" cssclass="txtseven" columns="95" rows="20" CausesValidation="false" MaxLength="500" TextMode="MultiLine" enableviewstate="true"></asp:textbox>
                <div class="clearboth"></div>
            </div>
            
            <div class="clearboth"></div>
            
            <uc1:findpageoptionscontrol id="pageOptionsControl" runat="server"></uc1:findpageoptionscontrol>
        </asp:panel>
    </div>
</asp:panel>