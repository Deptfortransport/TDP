<%@ Register TagPrefix="uc1" TagName="iFrameJourneyPlanningControl" Src="../Controls/iFrameJourneyPlanningControl.ascx" %>

<%@ Page EnableSessionState="false" Language="c#" Codebehind="iFrameJourneyPlanning.aspx.cs"
    AutoEventWireup="True" Inherits="TransportDirect.UserPortal.Web.Templates.iframeJourneyPlanning" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="<%=TransportDirect.Common.TDCultureInfo.CurrentUICulture.Name%>" xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <link href="/web2/Styles/setup.css" type="text/css" rel="stylesheet"/>
    <link href="/web2/Styles/HomePage.css" type="text/css" rel="stylesheet"/>
</head>
<body>
    <div class="CenteredContent">
        <form id="iframeJourneyPlanning" method="post" runat="server" target="_blank">
            <table bgcolor="#99ccff" cellspacing="0" cellpadding="10" width="325px">
                <tr>
                    <td>
                        <div style="font-size: 0.8em; background-color: #0099fe; width: 100%; padding-left: 10px;
                            padding-right: 10px; padding-top: 2px; padding-bottom: 0px; width: 280px;">
                            &nbsp;&nbsp;&nbsp;<asp:Label ForeColor="#ffffff" Font-Bold="True" BackColor="#0099fe"
                                ID="labelPlanAJourney" runat="server" EnableViewState="false"></asp:Label>
                        </div>
                        <div>
                            <uc1:iFrameJourneyPlanningControl ID="iframeJourneyPlanningControl" runat="server"></uc1:iFrameJourneyPlanningControl>
                        </div>
                        <div>
                        </div>
                    </td>
                </tr>
            </table>
        </form>
    </div>
</body>
</html>
