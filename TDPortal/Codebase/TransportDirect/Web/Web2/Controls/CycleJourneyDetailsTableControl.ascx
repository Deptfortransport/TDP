<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CycleJourneyDetailsTableControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.CycleJourneyDetailsTableControl" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<%@ Register TagPrefix="uc1" TagName="CJPUserInfoControl" Src="CJPUserInfoControl.ascx" %>

<div class="cycleJourneyDetailsBoxType1">
    <div class="cycleJourneyDetailsBoxType2" id="divCycleJourneyDetailsTableTile" runat="server">
        <div class="floatleftonly"><asp:Label id="labelCycleJourneyDetailsTableControlTitle" runat="server" enableviewstate="false" CssClass="txtsevenb"></asp:Label></div>
        <div class="floatrightonly">
            <div id="divShowMore" runat="server" class="<%# GetCSSClassShowMore %>">
                <font size="2">
                    <input id="buttonShowMore" runat="server" type="button" enableviewstate="false" 
                        class="TDButtonDefault"
                        onmouseout="this.className='TDButtonDefault'"
                        onmouseover="this.className='TDButtonDefaultMouseOver'" />
                </font>
            </div>
            <div id="divShowLess" runat="server" class="<%# GetCSSClassShowLess %>">
                <font size="2">
                    <input id="buttonShowLess" runat="server" type="button" enableviewstate="false" 
                        class="TDButtonDefault" 
                        onmouseout="this.className='TDButtonDefault'"
                        onmouseover="this.className='TDButtonDefaultMouseOver'" />
                </font>
            </div>
            <input id="<%# GetHiddenInputId %>" type="hidden" value="<%# ShowDetailsState%>"  name="<%# GetHiddenInputId %>" />
        </div>
        <div class="clearboth"></div>
    </div>
    <div class="cycleJourneyDetailsBoxType3">
        <a id="cycleDetailsTableAnchor" runat="server" tabindex="0"></a>
        <asp:Repeater ID="cycleDetailsRepeater" runat="server" EnableViewState="false">
            <HeaderTemplate>
                <table cellspacing="0px" cellpadding="0" width="100%" summary="Cycle route directions" border="0">
                    <tr>
                        <td colspan="5"><hr class="bluerule" /></td>
                    </tr>
                    <tr>
                        <th class="txtsevenb cycleJourneyDetailsTableHeader1"></th>
                        <th class="txtsevenb cycleJourneyDetailsTableHeader2"><%# HeaderDetail[0] %></th>
                        <th class="txtsevenb cycleJourneyDetailsTableHeader3"><%# HeaderDetail[1] %></th>
                        <th class="txtsevenb cycleJourneyDetailsTableHeader4"></th>
                        <th class="txtsevenb cycleJourneyDetailsTableHeader5"><%# HeaderDetail[2] %></th>
                    </tr>
                    <tr>
                        <td colspan="5"><hr class="bluerule" /></td>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                    <tr>
                        <td id="cellStepNumber" runat="server" class="txtseven cycleJourneyDetailsTableCell1" rowspan="1">
                            <asp:Panel id="panelDirectionNumber" runat="server" Visible="<%# !IsRowMapLinkVisible %>">
                                <%# (string)((object[])Container.DataItem)[0] %>
                            </asp:Panel>
                            <asp:Panel id="panelDirectionLink" runat="server" Visible="<%# IsRowMapLinkVisible %>">
                                <a href=" " onclick="<%# GetShowOnMapScript((string)((object[])Container.DataItem)[0]) %>">
                                <%# (string)((object[])Container.DataItem)[0] %></a>
                            </asp:Panel>
                        </td>
                        <td id="cellDistance" runat="server" class="txtseven cycleJourneyDetailsTableCell2" rowspan="1"><%# (string)((object[])Container.DataItem)[1] %></td>
                        <td class="txtseven cycleJourneyDetailsTableCell3">
                            <%# (string)((object[])Container.DataItem)[2] %>
                            <uc1:CJPUserInfoControl id="CJPUserInfoControl1" runat="server" InfoType="LinkTOIDs" DisplayText="Toid:" NewLineBefore="true" />
                            <uc1:CJPUserInfoControl id="cjpUserLinkTOIDInfo" runat="server" InfoType="LinkTOIDs" DisplayText="<%# (string)((object[])Container.DataItem)[8] %>" NewLineBefore="true" />
                        </td>
                        <td class="txtseven cycleJourneyDetailsTableCell4"><%# (string)((object[])Container.DataItem)[4] %></td>
                        <td id="cellTime" runat="server" class="txtseven cycleJourneyDetailsTableCell5" rowspan="1"><%# (string)((object[])Container.DataItem)[7] %></td>
                    </tr>
                    <tr id="rowCycleRoute" runat="server" visible="false">
                        <td class="txtseven cycleJourneyDetailsTableCell6" colspan="2"><%# (string)((object[])Container.DataItem)[3] %></td>
                    </tr>
                    <tr id="rowCycleInstruction" runat="server" visible="false">
                        <td class="txtseven cycleJourneyDetailsTableCell7" colspan="2"><%# (string)((object[])Container.DataItem)[5] %></td>
                    </tr>
                    <tr id="rowCycleAttribute" runat="server" visible="false">
                        <td class="txtseven cycleJourneyDetailsTableCell8" colspan="2"><%# (string)((object[])Container.DataItem)[6] %></td>
                    </tr>
            </ItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </div>
</div>
