<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="StopInformationOperatorControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.StopInformationOperatorControl" %>

<%@ Register TagPrefix="uc1" TagName="ZonalAirportOperatorControl" Src="../Controls/ZonalAirportOperatorControl.ascx" %>

<div class="roundedcornr_box">
    <div class="roundedcornr_top">
        <div>
            <h2><asp:Label ID="labelOperator" runat="server"></asp:Label></h2>
        </div>
    </div>
    <div class="roundedcornr_content_clear">
        <div class="stopInformationLinks">
            <uc1:ZonalAirportOperatorControl ID="ZonalAirportOperatorControl1" runat="server"
                Visible="True">
            </uc1:ZonalAirportOperatorControl>
        </div>
    </div>
    <div class="roundedcornr_bottom">
        <div>
        </div>
    </div>
</div>