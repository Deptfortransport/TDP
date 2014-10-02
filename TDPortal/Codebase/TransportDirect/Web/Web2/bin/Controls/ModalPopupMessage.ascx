<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ModalPopupMessage.ascx.cs"
    Inherits="TransportDirect.UserPortal.Web.Controls.ModalPopupMessage" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls"
    Assembly="td.userportal.web" %>
<div id="ModalPopupDiv" class="modalPopup" runat="server">
    <div id="BackGround" class="modalPopupBackGround" runat="server">
    </div>
    <div id="ForeGround" class="modalPopupForeGround" runat="server">
        <div id="ModalPopupContainer" class="modalPopupContainer" runat="server">
            <div id="ModalPopupMessageDiv" class="modalPopupMessage" runat="server">
            </div>
            <div id="ModalPopupControls" class="modalPopupControls" runat="server">
                <div class="modalPopupCancelButton">
                    <cc1:TDButton ID="CancelButton" runat="server" /></div>
                <div class="modalPopupOkButton">
                    <cc1:TDButton ID="OKButton" runat="server" /></div>
            </div>
        </div>
    </div>
</div>
