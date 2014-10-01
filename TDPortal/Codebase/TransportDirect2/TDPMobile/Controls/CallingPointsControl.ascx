<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CallingPointsControl.ascx.cs" Inherits="TDP.UserPortal.TDPMobile.Controls.CallingPointsControl" %>

<asp:Repeater ID="legCallingPoints" runat="server" OnItemDataBound="LegCallingPoints_DataBound">
    <HeaderTemplate>
        <div class="callingPointsDiv">
    </HeaderTemplate>
    <ItemTemplate>
            <div class="row2a">
                <div class="columnTime pointTimeDiv">
                    <div class="timeDepart">
                        <asp:Label ID="pointDepartTime" runat="server" EnableViewState="true" />                
                    </div>
                </div>
                <div class="columnEmpty"> 
                </div>
                <div class="columnNode">
                    <div class="nodeImage">
                        <asp:Image ID="legNodeImage" runat="server" EnableViewState="true" />
                    </div>
                </div>
                <div class="columnLocation pointLocDiv">
                    <asp:Label ID="pointLocation" runat="server" EnableViewState="true" />
                </div>
            </div>
    </ItemTemplate>
    <FooterTemplate>
        </div>
    </FooterTemplate>
</asp:Repeater>
