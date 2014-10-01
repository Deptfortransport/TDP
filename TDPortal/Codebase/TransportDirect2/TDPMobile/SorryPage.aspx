<%@ Page Title="" Language="C#" MasterPageFile="~/TDPMobile.Master" AutoEventWireup="true" CodeBehind="SorryPage.aspx.cs" Inherits="TDP.UserPortal.TDPMobile.SorryPage" %>

<asp:Content ID="Content4" ContentPlaceHolderID="contentMain" runat="server">

    <div class="headingContent">
        <h1 id="titleMessage" runat="server" enableviewstate="false"></h1>  
    </div>
    <div class="subContent">
        <asp:Label ID="lblMessage" runat="server" EnableViewState="false"></asp:Label>
    </div>

</asp:Content>
