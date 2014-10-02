<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MapSelectLocationControl2.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.MapSelectLocationControl2" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<div class="mlMapSelectLocationContainer">
    <div>
        <asp:Label ID="labelSelectLocationTitle" runat="server" CssClass="txtnineb"></asp:Label>
        <br />
        
        <asp:Panel ID="panelSelectLocationInfo" runat="server" CssClass="show">
            <div class="mlMapSelectLocationsInfo">
                <asp:Label ID="labelSelectLocationInfo" runat="server" CssClass="txtseven"></asp:Label>
            </div>
        </asp:Panel>
        
        <asp:Panel ID="panelSelectLocationList" runat="server" CssClass="hide">
            <div class="mlMapSelectLocationsList">
                <div class="mlMapSelectLocationListDiv floatleftonly">
                    <div class="floatleftonly">
                        <asp:Label ID="labelSelectInstructions" runat="server" CssClass="txtseven"></asp:Label>&nbsp;
                    </div>
                    <div class="floatleftonly">
                        <select id="selectLocationDropDownList" class="mlMapLocationList" enableviewstate="false"></select>&nbsp;
                    </div>
                    <div class="clearboth"></div>
                </div>
                <div class="mlMapSelectLocationButtonsDiv floatrightonly">
                    <cc1:TDButton ID="buttonOK" runat="server" CausesValidation="false"></cc1:TDButton>
                    <cc1:TDButton ID="buttonCancel" runat="server" CausesValidation="false"></cc1:TDButton>
                </div>
                <div class="clearboth"></div>
            </div>
        </asp:Panel>
    
        <asp:Panel ID="panelSelectLocationError" runat="server" CssClass="hide">
            <div class="mlMapSelectLocationsError">
                <div class="mlMapSelectLocationError floatleftonly">
                    <asp:Label ID="labelSelectLocationError" runat="server" CssClass="txtseven"></asp:Label>
                </div>
                <div class="mlMapSelectLocationButtonsDiv floatrightonly">
                        <cc1:TDButton ID="buttonErrorCancel" runat="server" CausesValidation="false"></cc1:TDButton>
                    </div>
                <div class="clearboth"></div>
            </div>
        </asp:Panel>
            
    </div>
</div>

