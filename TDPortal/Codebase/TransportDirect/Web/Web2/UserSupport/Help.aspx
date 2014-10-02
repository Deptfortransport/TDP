<%@ Page language="c#" Codebehind="Help.aspx.cs" AutoEventWireup="True" Inherits="TransportDirect.UserPortal.Web.UserSupport.Help" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html lang="<%=TransportDirect.Common.TDCultureInfo.CurrentUICulture.Name%>">
	<head>
		<cc1:headelementcontrol id="headElementControl" runat="server"></cc1:headelementcontrol>
	</head>
	<body>
		<form id="Help" method="post" runat="server">
			<p>Help - Main Page</p>
			<p>
				We'd like you to get the most out of the Transport Direct Portal. These pages 
				are designed to give you the information you may need to do that.
			</p>
			Information/Instructions Are Currently Available For:
			<p>
			</p>
			<table>
				<tr>
					<td><asp:hyperlink id="hypAbout" runat="server" target="_blank">About
						</asp:hyperlink></td>
				</tr>
				<tr>
					<td><asp:hyperlink id="hypFeedback" runat="server" target="_blank">Feedback
						</asp:hyperlink></td>
				</tr>
				<tr>
					<td><asp:hyperlink id="hypJPlanner" runat="server" target="_blank">Journey Planner
						</asp:hyperlink></td>
				</tr>
				<tr>
					<td><asp:hyperlink id="hypHome" runat="server" target="_blank">Home
						</asp:hyperlink></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
