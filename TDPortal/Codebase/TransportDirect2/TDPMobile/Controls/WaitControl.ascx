<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="WaitControl.ascx.cs" Inherits="TDP.UserPortal.TDPMobile.Controls.WaitControl" %>

<div id="journeyProgress" runat="server" class="journeyProgress" >
    <div class="processMessage processMessageNonJS" aria-live="polite"> 
        <div class="loadingMessageDiv" role="alert">
            <asp:Label ID="loadingMessage" runat="server" role="alert" />
            <noscript>
                <br />
                <asp:Label ID="longWaitMessage" runat="server" />
                <asp:HyperLink ID="longWaitMessageLink" runat="server" />
            </noscript>   
        </div>
        <div class="loadingImageDiv">
            <asp:Image ID="loadingImage" runat="server" />
        </div>
    </div>
</div>