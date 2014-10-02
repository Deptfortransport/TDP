<%@ Register TagPrefix="uc1" TagName="iFrameFindAPlaceControl" Src="../Controls/iFrameFindAPlaceControl.ascx" %>

<%@ Page EnableSessionState="false" Language="c#" Codebehind="iFrameFindAPlace.aspx.cs"
    AutoEventWireup="True" Inherits="TransportDirect.UserPortal.Web.Templates.iFrameFindAPlace" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="<%=TransportDirect.Common.TDCultureInfo.CurrentUICulture.Name%>"  xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <link href="/web2/Styles/setup.css" type="text/css" rel="stylesheet" />
    <link href="/web2/Styles/HomePage.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <div class="CenteredContent">
        <form id="iframeFindAPlace" method="post" runat="server" target="_blank">
            <table bgcolor="#99ccff" cellspacing="0" cellpadding="10" width="300px">
                <tr>
                    <td>
                        <div style="font-size: 0.8em; padding-right: 10px; padding-left: 10px; padding-bottom: 0px;
                            width: 280px; padding-top: 2px; background-color: #0099fe">
                            &nbsp;&nbsp;&nbsp;<asp:Label ForeColor="#ffffff" Font-Bold="True" BackColor="#0099fe"
                                ID="labelFindAPlace" runat="server" EnableViewState="false"></asp:Label>
                        </div>
                        <div>
                            <uc1:iFrameFindAPlaceControl ID="FindAPlaceControl1" runat="server"></uc1:iFrameFindAPlaceControl>
                        </div>
                    </td>
                </tr>
            </table>
        </form>
    </div>
</body>
</html>
