<%@ Page Language="c#" Codebehind="FindNearestLandingPage.aspx.cs" AutoEventWireup="false"
    Inherits="TransportDirect.UserPortal.Web.Templates.FindNearestLandingPage" ValidateRequest="false" %>

<%@ Register TagPrefix="uc1" TagName="FindToFromLocationsControl" Src="../Controls/FindToFromLocationsControl.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls"
    Assembly="td.userportal.web" %>
<!doctype html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="<%=TransportDirect.Common.TDCultureInfo.CurrentUICulture.Name%>">
<head>
    <cc1:HeadElementControl ID="headElementControl" runat="server"></cc1:HeadElementControl>
</head>
<body>
    <div class="CenteredContent">
        <form id="Form1" method="post" runat="server">
            <uc1:FindToFromLocationsControl ID="findAOriginLocationsControl" runat="server" Visible="False">
            </uc1:FindToFromLocationsControl>
        </form>
    </div>
</body>
</html>
