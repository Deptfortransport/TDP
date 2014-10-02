<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CycleJourneyGraphControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.CycleJourneyGraphControl" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<div class="cycleJourneyGraphBoxType1">
    <asp:label id="labelSRGradientProfile" runat="server" cssclass="screenreader"></asp:label>
    <div>
        <div class="floatleftonly"><asp:Label id="labelTitle" runat="server" enableviewstate="false" CssClass="txtsevenb"></asp:Label></div>
        <div class="floatrightonly"><cc1:TDButton ID="buttonShowTable" runat="server"></cc1:TDButton></div>
        <div class="clearboth"></div>
    </div>
    
    <asp:Panel id="panelChart" runat="server">
        <div id="divGradientProfileChartWait" runat="server" class="chartWait" enableviewstate="false">
            <asp:Label id="labelWait" runat="server" enableviewstate="false" CssClass="txtseven"></asp:Label>
            <br />
            <cc1:tdimage id="imageWait" runat="server" enableviewstate="false"></cc1:tdimage>
        </div>
        
        <div id="divGradientProfileChartContainer" runat="server" class="chartContainer" enableviewstate="false">
            <asp:Label id="labelDistanceInMiles" runat="server" enableviewstate="false" CssClass="txtseven"></asp:Label>
            
            <div class="clearboth"></div>
            
            <div id="divGradientProfileChart" runat="server" class="chart" enableviewstate="false"></div>
            
            <div class="clearboth"></div>
            
            <div class="chartColour" id="divChartColour" runat="server" enableviewstate="false"></div>
            <div>
                <asp:Label id="labelHighestPoint" runat="server" enableviewstate="false" CssClass="txtseven"></asp:Label>
                <asp:Label id="labelHighestPointValue" runat="server" EnableViewState="false" CssClass="txtseven"></asp:Label>&nbsp;
                <asp:Label id="labelLowestPoint" runat="server" enableviewstate="false" CssClass="txtseven"></asp:Label>
                <asp:Label id="labelLowestPointValue" runat="server" EnableViewState="false" CssClass="txtseven"></asp:Label>
            </div>
            <div>
                <asp:Label id="labelTotalClimb" runat="server" enableviewstate="false" CssClass="txtseven"></asp:Label>
                <asp:Label id="labelTotalClimbValue" runat="server" EnableViewState="false" CssClass="txtseven"></asp:Label>&nbsp;
                <asp:Label id="labelTotalDescent" runat="server" enableviewstate="false" CssClass="txtseven"></asp:Label>
                <asp:Label id="labelTotalDescentValue" runat="server" EnableViewState="false" CssClass="txtseven"></asp:Label>
            </div>
            
        </div>
        <div>
            <noscript>
                <asp:Label id="labelNoScript" runat="server" enableviewstate="false" CssClass="txtseven"></asp:Label>            
            </noscript>
        </div>
        <div id="divGradientProfileError" runat="server" class="chartError" enableviewstate="false">
            <asp:Label id="labelError" runat="server" enableviewstate="false" CssClass="txtseven"></asp:Label>            
        </div>
    </asp:Panel>
    
    <asp:Panel id="panelTable" runat="server" CssClass="divChartTable">
            <a id="gradientProfileTableAnchor" runat="server" tabindex="0"></a>
            <asp:Repeater id="repeaterGradientProfileTable" runat="server">
                <HeaderTemplate>
                    <table class="chartTable" cellpadding="0" cellspacing="0">
                        <tr>
                            <th class="chartTableHeaderDistance"><asp:Label id="labelTableHeaderDistance" runat="server" enableviewstate="false" CssClass="txtsevenb"></asp:Label></th>
                            <th class="chartTableHeaderHeight"><asp:Label id="labelTableHeaderHeight" runat="server" enableviewstate="false" CssClass="txtsevenb"></asp:Label></th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                        <tr>
                            <td class="txtseven chartTableDistance"><%# (string)((object[])Container.DataItem)[0] %></td>
                            <td class="txtseven chartTableHeight"><%# (string)((object[])Container.DataItem)[1] %></td>
                        </tr>
                </ItemTemplate>
                <AlternatingItemTemplate>
                        <tr>
                            <td class="txtseven chartTableDistance"><%# (string)((object[])Container.DataItem)[0] %></td>
                            <td class="txtseven chartTableHeight"><%# (string)((object[])Container.DataItem)[1] %></td>
                        </tr>
                </AlternatingItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
            <asp:Label id="labelTableError" runat="server" enableviewstate="false" CssClass="txtseven"></asp:Label>
    </asp:Panel>
</div>