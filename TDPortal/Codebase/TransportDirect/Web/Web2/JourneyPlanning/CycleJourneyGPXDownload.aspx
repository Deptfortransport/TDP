<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CycleJourneyGPXDownload.aspx.cs" Inherits="TransportDirect.UserPortal.Web.JourneyPlanning.CycleJourneyGPXDownload" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<%@ Register TagPrefix="uc1" TagName="HeaderControl" Src="../Controls/HeaderControl.ascx" %>
        
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html lang="<%=TransportDirect.Common.TDCultureInfo.CurrentUICulture.Name%>" xmlns="http://www.w3.org/1999/xhtml">
<head>
    <cc1:HeadElementControl ID="headElementControl" Stylesheets="setup.css, jpstd.css, homepage.css, CycleJourneyDetails.aspx.css" runat="server"></cc1:HeadElementControl>
</head>
<body>
    <div class="CenteredContent">
        <form id="CycleJourneyGPXDownload" runat="server">
        <div>
            <uc1:headercontrol id="headerControl" runat="server"></uc1:headercontrol>
            <br />
            <div id="boxtypeeight">
                <asp:Label ID="labelError" runat="server" EnableViewState="false" Visible="false" CssClass="txtseven"></asp:Label>
            </div>    
        </div>
        </form>
    </div>
</body>
</html>