<%@ Page Language="c#" Codebehind="JPLandingPage.aspx.cs" ValidateRequest="false"
    AutoEventWireup="True" EnableViewStateMac="false" Inherits="TransportDirect.UserPortal.Web.Templates.JPLandingPage" %>

<%@ Register TagPrefix="uc1" TagName="BiStateLocationControl" Src="../Controls/BiStateLocationControl.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls"
    Assembly="td.userportal.web" %>
<%@ Register TagPrefix="uc1" TagName="FindLeaveReturnDatesControl" Src="../Controls/FindLeaveReturnDatesControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FindCoachTrainPreferencesControl" Src="../Controls/FindCoachTrainPreferencesControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FindToFromLocationsControl" Src="../Controls/FindToFromLocationsControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TriStateDateControl" Src="../Controls/TriStateDateControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AirportDisplayControl" Src="../Controls/AirportDisplayControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FindCarPreferencesControl" Src="../Controls/FindCarPreferencesControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FindCarJourneyOptionsControl" Src="../Controls/FindCarJourneyOptionsControl.ascx" %>
<!doctype html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="<%=TransportDirect.Common.TDCultureInfo.CurrentUICulture.Name%>">
<head>
    <cc1:HeadElementControl ID="headElementControl" runat="server"></cc1:HeadElementControl>
</head>
<body>
    <div class="CenteredContent">
        <form id="Form1" method="post" runat="server">
            <uc1:BiStateLocationControl id="originSelect" runat="server" Visible="False">
            </uc1:BiStateLocationControl>
            <uc1:BiStateLocationControl id="destinationSelect" runat="server" Visible="False">
            </uc1:BiStateLocationControl>
            <uc1:findtofromlocationscontrol id="findAOriginLocationsControl" runat="server" Visible="False">
            </uc1:findtofromlocationscontrol>
            <uc1:FindToFromLocationsControl id="findADestinationLocationsControl" runat="server"
                Visible="False">
            </uc1:FindToFromLocationsControl>
            <uc1:findcoachtrainpreferencescontrol id="findACoachTrainPreferencesControl" runat="server"
                Visible="False">
            </uc1:findcoachtrainpreferencescontrol>
            <uc1:findleavereturndatescontrol id="findADateControl" runat="server" Visible="False">
            </uc1:findleavereturndatescontrol>
            <uc1:FindCarPreferencesControl id="findACarPreferencesControl" runat="server" Visible="False">
            </uc1:FindCarPreferencesControl>
            <uc1:FindCarJourneyOptionsControl id="findACarJourneyOptionsControl" runat="server"
                Visible="False">
            </uc1:FindCarJourneyOptionsControl>
        </form>
    </div>
</body>
</html>
