<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="StopInformationTaxiControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.StopInformationTaxiControl" %>

<%@ Register TagPrefix="uc1" TagName="TaxiInformationControl" Src="../Controls/TaxiInformationControl.ascx" %>

<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>

<div class="roundedcornr_box">
    <div class="roundedcornr_top">
        <div>
            <h2><asp:Label ID="labelSummaryTitle" runat="server"></asp:Label></h2>
        </div>
    </div>
    <div class="roundedcornr_content_clear">
        <table>
            <tr>
                <td>
                    <uc1:TaxiInformationControl ID="TaxiInformationControl1" runat="server">
                    </uc1:TaxiInformationControl>
                </td>
                <td>
                    <cc1:tdimage id="imageTaxi" runat="server" width="60" height="40"></cc1:tdimage>
                </td>
            </tr>
        </table>
        
    </div>
    <div class="roundedcornr_bottom">
        <div>
        </div>
    </div>
</div>
