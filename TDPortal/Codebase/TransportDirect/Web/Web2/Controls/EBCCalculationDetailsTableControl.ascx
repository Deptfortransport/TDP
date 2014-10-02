<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EBCCalculationDetailsTableControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.EBCCalculationDetailsTableControl" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<%@ Register TagPrefix="uc1" TagName="ErrorDisplayControl" Src="./ErrorDisplayControl.ascx" %>
<div class="ebcCalculationDetailsBoxType1">
    <div class="ebcCalculationDetailsBoxType2">
        <span class="txteightb"><asp:Label ID="labelCalculationTitle" runat="server"></asp:Label></span>
    </div>
    <div class="ebcCalculationDetailsBoxType3">
        <asp:Panel ID="errorMessagePanel" runat="server" Visible="False" EnableViewState="false">
            <uc1:ErrorDisplayControl ID="errorDisplayControl" runat="server" Visible="False"></uc1:ErrorDisplayControl>
        </asp:Panel>
        <asp:Repeater ID="ebcDetailsRepeater" runat="server" EnableViewState="false">
            <HeaderTemplate>
                <table cellspacing="0" cellpadding="0" width="100%" summary="Enivronmental Benefits" class="ebcCalculationTable">
                    <tr>
                        <td colspan="5" class="ebcCalculationTableHeader6">
                            <hr class="bluerule" align="left" width="100%" size="1" noshade="noshade" />
                        </td>
                    </tr>
                    <tr>
                        <th class="txtsevenb ebcCalculationTableHeader1"><%# HeaderDetail[0] %></th>
                        <th class="txtsevenb ebcCalculationTableHeader2"><%# HeaderDetail[1] %></th>
                        <th class="txtsevenb ebcCalculationTableHeader3"><%# HeaderDetail[2] %></th>
                        <th class="txtsevenb ebcCalculationTableHeader4"><%# HeaderDetail[3] %></th>
                        <th class="txtsevenb ebcCalculationTableHeader5"><%# HeaderDetail[4] %></th>
                    </tr>
                    <tr>
                        <td colspan="5" class="ebcCalculationTableHeader6">
                            <hr class="bluerule" align="left" width="100%" size="1" noshade="noshade" />
                        </td>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                    <tr>
                        <td class="txtsevenb ebcCalculationTableCell1"><%# (string)((object[])Container.DataItem)[0] %>&nbsp;</td>
                        <td class="txtseven ebcCalculationTableCell2"><%# (string)((object[])Container.DataItem)[1] %>&nbsp;</td>
                        <td class="txtseven ebcCalculationTableCell3"><%# (string)((object[])Container.DataItem)[2] %>&nbsp;</td>
                        <td class="txtseven ebcCalculationTableCell4"><%# (string)((object[])Container.DataItem)[3] %>&nbsp;</td>
                        <td class="txtsevenb ebcCalculationTableCell5"><%# (string)((object[])Container.DataItem)[4] %>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="txtsevenb ebcCalculationTableCell6"><%# (string)((object[])Container.DataItem)[5] %>&nbsp;</td>
                        <td class="txtseven ebcCalculationTableCell7"><%# (string)((object[])Container.DataItem)[6] %>&nbsp;</td>
                        <td class="txtseven ebcCalculationTableCell8"><%# (string)((object[])Container.DataItem)[7] %>&nbsp;</td>
                        <td class="txtseven ebcCalculationTableCell9"><%# (string)((object[])Container.DataItem)[8] %>&nbsp;</td>
                        <td class="txtsevenb ebcCalculationTableCell10"><%# (string)((object[])Container.DataItem)[9] %>&nbsp;</td>
                    </tr>
            </ItemTemplate>
            <FooterTemplate>
                    <tr>
                        <td class="txtsevenb ebcCalculationTableFooter1"><%# FooterDetail[0] %>&nbsp;</td>
                        <td class="txtsevenb ebcCalculationTableFooter2"><%# FooterDetail[1] %>&nbsp;</td>
                        <td class="txtsevenb ebcCalculationTableFooter3"><%# FooterDetail[2] %>&nbsp;</td>
                        <td class="txtsevenb ebcCalculationTableFooter4"><%# FooterDetail[3] %>&nbsp;</td>
                        <td class="txtsevenb ebcCalculationTableFooter5"><%# FooterDetail[4] %>&nbsp;</td>
                    </tr>
                    <tr>
                        <td class="txtsevenb ebcCalculationTableFooter1"><%# FooterDetail[5] %>&nbsp;</td>
                        <td class="txtsevenb ebcCalculationTableFooter2"><%# FooterDetail[6] %>&nbsp;</td>
                        <td class="txtsevenb ebcCalculationTableFooter3"><%# FooterDetail[7] %>&nbsp;</td>
                        <td class="txtsevenb ebcCalculationTableFooter4"><%# FooterDetail[8] %>&nbsp;</td>
                        <td class="txtsevenb ebcCalculationTableFooter5"><%# FooterDetail[9] %>&nbsp;</td>
                    </tr>
                </table>
            </FooterTemplate>
        </asp:Repeater>
    </div>
</div>
