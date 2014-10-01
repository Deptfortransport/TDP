﻿<%@ Page Title="" Language="C#" MasterPageFile="~/TDPMobile.Master" AutoEventWireup="true" CodeBehind="TravelNewsDetail.aspx.cs" Inherits="TDP.UserPortal.TDPMobile.TravelNewsDetail" %>
<%@ Register src="~/Controls/UndergroundStatusDetailsControl.ascx" tagname="UndergroundStatusDetailsControl" tagprefix="uc1" %>
<%@ Register src="~/Controls/TravelNewsDetailControl.ascx" tagname="TravelNewsDetailControl" tagprefix="uc1" %>

<asp:Content ID="Content4" ContentPlaceHolderID="contentMain" runat="server">

    <uc1:TravelNewsDetailControl ID="travelNewsDetailControl" runat="server" />
    <uc1:UndergroundStatusDetailsControl ID="undergroundStatusDetailControl" runat="server" />

</asp:Content>