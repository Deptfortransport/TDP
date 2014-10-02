<%@ Register TagPrefix="uc1" TagName="FindStationsDisplayControl" Src="FindStationsDisplayControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FindLocationControl" Src="FindLocationControl.ascx" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="FindToFromLocationsControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.FindToFromLocationsControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<table lang="en" cellspacing="0" width="100%">
	<tr>
		<td><uc1:findlocationcontrol id="fromLocationControl" runat="server"></uc1:findlocationcontrol>
			<uc1:findstationsdisplaycontrol id="fromStationsControl" runat="server"></uc1:findstationsdisplaycontrol>
		</td>
	</tr>
	<tr>
		<td>
			<uc1:findlocationcontrol id="toLocationControl" runat="server"></uc1:findlocationcontrol>
			<uc1:findstationsdisplaycontrol id="toStationsControl" runat="server"></uc1:findstationsdisplaycontrol>
		</td>
	</tr>
</table>
