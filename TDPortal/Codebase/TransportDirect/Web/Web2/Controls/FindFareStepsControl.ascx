<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FindFareStepsControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.FindFareStepsControl" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<div>
<table width="100%" cellpadding="0" cellspacing="0" border="0">
    <tr>
        <td align="left">
            
            <div class="ImageFindFareStepsEnv">
                <div class="ImageFindFareStep">
                    <cc1:TDImage ID="imageFindFareStep1" runat="server" />
                    <cc1:TDImageButton ID="imagebuttonFindFareStep1" runat="server" />
                </div>
                <div class="ImageFindFareStep">
                    <cc1:TDImage ID="imageFindFareStep2" runat="server" />
                    <cc1:TDImageButton ID="imagebuttonFindFareStep2" runat="server" />
                </div>
                <div class="ImageFindFareStep">
                    <cc1:TDImage ID="imageFindFareStep3" runat="server" />
                    <cc1:TDImageButton ID="imagebuttonFindFareStep3" runat="server" />
                </div>
                <div class="ImageFindFareStep">
                    <cc1:TDImage ID="imageFindFareStep4" runat="server" />
                    <cc1:TDImageButton ID="imagebuttonFindFareStep4" runat="server" />
                </div>
            </div>
            
        </td>
    </tr>
</table>
</div>