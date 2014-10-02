<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="FindCarParkResultsLocationControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.FindCarParkResultsLocationControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<cc1:helplabelcontrol id="findCarParkResultsHelpLabel" visible="False" runat="server" cssmaintemplate="helpboxoutput"></cc1:helplabelcontrol>
<div id="boxtypeeightstd">
	<table summary="Location Description" width="100%" cellpadding="0" cellspacing="0">
		<tr>
			<td>
			    <table cellpadding="0" cellspacing="0">
			        <tr>
			            <td>
			                 <h1><asp:label id="labelLocationNameTitle" runat="server" ></asp:label></h1>
			            </td>
			            <td>
			                <h1>&nbsp;<asp:label id="labelLocationName" runat="server" ></asp:label></h1>
			            </td>
			        </tr>
			    </table>
			</td>
			<td valign="bottom" align="right">
				<cc1:tdbutton id="commandNewLocation" runat="server"></cc1:tdbutton>
			</td>
		</tr>
		
	</table>
</div>
