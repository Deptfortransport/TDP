<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="TravelNewsMapKeyControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.TravelNewsMapKeyControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<div class="mtTravelNewsMapKeyControl">
    <table width="100%">
	    <tr>
		    <td align="left" class="travelNewsMapKeyTitle">
		            <strong>&nbsp;<asp:label id="labelKeyHeading" runat="server"></asp:label></strong></td>
		    <td valign="middle" align="center">
			    <cc1:tdimage id="imageMajor" runat="server" imagealign="Middle" width="27" height="23"></cc1:tdimage>		
			    <asp:label id="labelMajor" runat="server"></asp:label></td>
		    <td valign="middle" align="center">
			    <cc1:tdimage id="imageNormal" runat="server" imagealign="Middle" width="27" height="23"></cc1:tdimage>
			    <asp:label id="labelNormal" runat="server"></asp:label></td>
		    <td valign="middle" align="center">
			    <cc1:tdimage id="imageFutureMajor" runat="server" imagealign="Middle" width="27" height="23"></cc1:tdimage>
			    <asp:label id="labelFutureMajor" runat="server"></asp:label></td>
		    <td valign="middle" align="center">
			    <cc1:tdimage id="imageFutureNormal" runat="server" imagealign="Middle" width="27" height="23"></cc1:tdimage>
			    <asp:label id="labelFutureNormal" runat="server"></asp:label></td>
	    </tr>
    </table>
</div>
