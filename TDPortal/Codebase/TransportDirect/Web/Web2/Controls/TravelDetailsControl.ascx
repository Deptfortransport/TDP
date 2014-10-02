<%@ Import namespace="TransportDirect.Web" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="TravelDetailsControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.TravelDetailsControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="uc1" TagName="TravelDetailsLoginOptionControl" Src="TravelDetailsLoginOptionControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TravelDetailsSaveOptionControl" Src="TravelDetailsSaveOptionControl.ascx" %>
<uc1:TravelDetailsLoginOptionControl id="login" runat="server"></uc1:TravelDetailsLoginOptionControl>
<uc1:TravelDetailsSaveOptionControl id="save" runat="server"></uc1:TravelDetailsSaveOptionControl>
