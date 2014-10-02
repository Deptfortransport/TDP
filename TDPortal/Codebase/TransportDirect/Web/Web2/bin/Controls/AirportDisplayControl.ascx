<%@ Control Language="c#" EnableViewState="false" AutoEventWireup="True" Codebehind="AirportDisplayControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.AirportDisplayControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<table cellspacing="0" cellpadding="0" title="Airport browse table">
	<tbody>
		<tr>
			<td class="findafromcolumn" align="right"><asp:label id="labelFrom" runat="server" cssclass="txtsevenb"></asp:label>
			</td>
			<td>
			    <table lang="en" class="tableDetails" cellspacing="0" cellpadding="0" summary="Selection">
			        <tbody>
			            <tr>
			                <td></td>
			                <td>
			                    <div class="floatright" style="margin-top:2px;">
			                        <cc1:tdbutton id="buttonNewLocation" runat="server"></cc1:tdbutton>
			                    </div>
			                    <asp:panel id="regionDisplayPanel" runat="server">
					                <asp:label id="labelRegion" cssclass="txtsevenb" runat="server"></asp:label>
				                </asp:panel>
				                <asp:datalist id="dlistAirports" runat="server" repeatdirection="Vertical" repeatcolumns="2">
					                <itemstyle cssclass="txtseven"></itemstyle>
					                <itemtemplate>
						                <%# (string)Container.DataItem %>
						                &nbsp;&nbsp;
					                </itemtemplate>
				                </asp:datalist>
				                <input type="hidden" id="airportCodes" runat="server" />
			                </td>
			            </tr>
			        </tbody>
			    </table>
				
			</td>
		</tr>
	</tbody>
</table>
