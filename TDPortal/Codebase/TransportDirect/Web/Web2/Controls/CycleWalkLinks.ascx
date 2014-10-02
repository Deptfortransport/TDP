<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CycleWalkLinks.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.CycleWalkLinks" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<%@ Register TagPrefix="uc1" TagName="ModalPopupMessage" Src="ModalPopupMessage.ascx"  %>
<div id="cyclewalklinksdiv" class="boxtypetwelve" runat="server">
    <div class="dmtitle" id="cyclewalkdiv">
        <table width="100%">
            <tr>
                <td class="cyclewalkimage">
                    <cc1:TDImage id="cycleWalkImage" runat="server" />
                </td>
                <td>
                    <span id="cycleWalkMessage" runat="server" />
                </td>
                <td align="center" class="cyclewalkLinks">
                    <cc1:TDHyperlink id="linkWalkit" runat="server" Visible="false" Target="_blank" />
                    <span id="cyclewalkspacer" class="cyclewalkspacer" runat="server" visible="false">&nbsp;</span>
                    <cc1:TDImageButton id="planCycle" runat="server" Visible="false" />
                </td>
            </tr>
        </table>
    </div>
</div>
<uc1:ModalPopupMessage id="modalPopup" runat="server" visible="false" />
