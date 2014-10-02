<%@ Control Language="c#" AutoEventWireup="True" Codebehind="MapRegionControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.MapRegionControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<div id="boxtypetwoalt" style="WIDTH: 146px; PADDING-TOP: 0px;">
	<table cellspacing="0" cellpadding="0px" border="0" width="100%" style="table-layout:fixed;">
		<tr>
			<td align="center">
				<asp:label id="headingRegion" cssclass="txtsevenb" runat="server" enableviewstate="False"></asp:label>
			</td>
		</tr>
		<tr>
		    <td align="center"><asp:label id="headingClickToSelect" cssclass="txtseven" runat="server" enableviewstate="False"></asp:label></td>
		    
		</tr>
		
		<tr style="display:none;">
			<td align="left"><asp:label id="headingMap" cssclass="txtsevenb" runat="server" enableviewstate="False"></asp:label></td>
			
		</tr>
		<tr>
			<td bgcolor="white" align="center">
				<cc1:imagemapcontrol id="imageMap1" runat="server"></cc1:imagemapcontrol>
			</td>
		</tr>
		
		<tr>
            <td>
                <cc1:tdbutton id="selectAllUk" runat="server" CssClass="TDSelectAllUk" CssClassMouseOver="TDSelectAllUkMouseOver" ></cc1:tdbutton>
            </td>
        </tr>
		
	</table>
	 
</div>
