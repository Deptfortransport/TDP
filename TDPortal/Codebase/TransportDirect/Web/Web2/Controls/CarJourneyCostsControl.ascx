<%@ Control Language="c#" AutoEventWireup="True" Codebehind="CarJourneyCostsControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.CarJourneyCostsControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="uc1" TagName="CarJourneyItemisedCostsControl" Src="CarJourneyItemisedCostsControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TotalCarJourneyCostsControl" Src="TotalCarJourneyCostsControl.ascx" %>
	<uc1:CarJourneyItemisedCostsControl id="carJourneyItemisedCostsControl" runat="server"></uc1:CarJourneyItemisedCostsControl>
	<uc1:TotalCarJourneyCostsControl id="totalCarJourneyCostsControl" runat="server"></uc1:TotalCarJourneyCostsControl>
