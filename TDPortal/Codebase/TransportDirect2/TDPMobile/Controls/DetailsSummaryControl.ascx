<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DetailsSummaryControl.ascx.cs" Inherits="TDP.UserPortal.TDPMobile.Controls.DetailsSummaryControl" %>
<%@ Register Namespace="TDP.UserPortal.TDPMobile.Controls" TagPrefix="cc1" assembly="tdp.userportal.tdpmobile" %> 

<div class="journeySummary" aria-atomic="true" aria-live="polite">
    <div class="summary jsshow">
        
        <div id="results">
        
            <asp:Repeater ID="journeySummary" runat="server" 
                OnItemDataBound="JourneySummary_DataBound"
                OnItemCommand="JourneySummary_Command" EnableViewState="true">

                <HeaderTemplate>
                    <ul data-role="listview">
                </HeaderTemplate>
                <ItemTemplate>
                    <li>
                        <asp:LinkButton ID="showDetailsBtn" runat="server" OnClick="showDetailsBtn_Click" data-role="dialog" data-transition="none">
                            <div class="summaryDetailContainer">
                                <div class="leave">
                                    <asp:Label ID="leave" CssClass="leaveHeading" runat="server" EnableViewState="true" />
                                    <asp:Label ID="leaveTime" CssClass="leaveTime" runat="server" EnableViewState="true" />
                                </div>
                                <div class="arrive">
                                    <asp:Label ID="arrive" CssClass="arriveHeading" runat="server" EnableViewState="true" />
                                    <asp:Label ID="arriveTime" CssClass="arriveTime" runat="server" EnableViewState="true" />
                                </div>       
                                <div class="changes">
                                    <asp:Label ID="changes" runat="server" EnableViewState="true" />
                                </div>  
                                <div class="duration">
                                    <asp:Label ID="duration" CssClass="durationHeading" runat="server" EnableViewState="true" />
                                    <asp:Label ID="durationSR" CssClass="screenReaderOnly" runat="server" EnableViewState="true" />
                                    <asp:Label ID="durationTime" CssClass="durationTime" runat="server" EnableViewState="true" />
                                </div>  
                            </div>
                            <div class="transportContainer">
                                <asp:Label ID="transport" runat="server" EnableViewState="true" CssClass="screenReaderOnly" />
                                <asp:Repeater ID="rptModeIcons" runat="server" EnableViewState="true" OnItemDataBound="ModeIcons_DataBound">
                                    <ItemTemplate>
                                        <div id="modeIconDiv" runat="server" enableviewstate="true" class="modeIconDiv" aria-hidden="true">
                                            <asp:Image ID="modeIcon" runat="server" EnableViewState="true" />
                                        </div>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </asp:LinkButton>
                        <noscript>
                            <asp:Button ID="showDetailsBtnNonJS" runat="server" CssClass="showDetailsNonJS" OnClick="showDetailsBtnNonJS_Click" />
                        </noscript>
                    </li>
                </ItemTemplate>
                <FooterTemplate>
                    </ul>
                </FooterTemplate>

            </asp:Repeater>
                            
            <div class="clear"></div>
            
            <div class="journeySummaryHead" id="navresults">
                <div id="earlierJourneyDiv" runat="server" class="earlierJourneyDiv">
                    <asp:LinkButton ID="btnEarlierJourney" CssClass="earlier jshide" runat="server" OnClick="BtnEarlierJourney_Click" data-icon="arrow-l" data-transition='none' data-role='button'/>
                    <noscript>
                        <asp:Button ID="btnEarlierJourneyNonJS" runat="server" CssClass="earlierNonJS" OnClick="BtnEarlierJourney_Click" />
                    </noscript>
                </div>       
                <div id="laterJouneyDiv" runat="server" class="laterJouneyDiv">
                    <asp:LinkButton ID="btnLaterJourney" CssClass="later jshide"  runat="server" OnClick="BtnLaterJourney_Click" data-icon="arrow-r" data-transition='none' data-iconpos="right" data-role='button'/>
                    <noscript>
                        <asp:Button ID="btnLaterJourneyNonJS" runat="server" CssClass="laterNonJS" OnClick="BtnLaterJourney_Click" />
                    </noscript>
                </div>  
            </div>
        
            <div class="clear"></div>
                
        </div>
    </div>
</div>