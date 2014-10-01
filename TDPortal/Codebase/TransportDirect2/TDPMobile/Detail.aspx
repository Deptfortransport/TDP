<%@ Page Title="" Language="C#" MasterPageFile="~/TDPMobile.Master" AutoEventWireup="true" CodeBehind="Detail.aspx.cs" Inherits="TDP.UserPortal.TDPMobile.Detail" %>
<%@ Register Namespace="TDP.UserPortal.TDPMobile.Controls" TagPrefix="uc1" assembly="tdp.userportal.tdpmobile" %> 
<%@ Register src="~/Controls/JourneyHeadingControl.ascx" tagname="JourneyHeadingControl" tagprefix="uc1" %>
<%@ Register src="~/Controls/JourneyPageControl.ascx" tagname="JourneyPageControl" tagprefix="uc1" %>
<%@ Register src="~/Controls/DetailsLegsControl.ascx" tagname="LegsControl" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="contentMain" runat="server">
          
    <asp:UpdatePanel ID="journeyInputUpdate" runat="server" UpdateMode="Always" RenderMode="Block">
        <ContentTemplate>
    
            <uc1:JourneyHeadingControl ID="journeyHeadingControl" runat="server" />
            <uc1:JourneyPageControl ID="journeyPageControl" runat="server" />
            <uc1:LegsControl ID="legsDetails" runat="server" />
            <uc1:JourneyHeadingControl ID="journeyHeadingControlFooter" runat="server" />

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
