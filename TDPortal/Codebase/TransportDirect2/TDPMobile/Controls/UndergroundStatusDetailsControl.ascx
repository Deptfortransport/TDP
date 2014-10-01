<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UndergroundStatusDetailsControl.ascx.cs" Inherits="TDP.UserPortal.TDPMobile.Controls.UndergroundStatusDetailsControl" %>

<div data-role="page" class="undergroundDetail collapsed">
    <div class="undergroundDetailContent" data-role="content">

        <asp:HiddenField ID="undergroundDetailId" runat="server" />

        <h3 id="undergroundServiceHeadlineLbl" runat="server" class="statusColourHeadline"></h3>

        <div class="undergroundDetails">
            <div class="description">
                <asp:Label ID="statusDescriptionLbl" runat="server" />
            </div>
            <div class="detail">
                <asp:Label ID="statusDetailLbl" runat="server" />
            </div>
        </div>
    </div>
</div>
