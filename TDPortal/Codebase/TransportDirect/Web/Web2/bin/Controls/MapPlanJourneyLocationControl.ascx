<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="MapPlanJourneyLocationControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.MapPlanJourneyLocationControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<div id="mapPlanJourneyLocationControl" runat="server">
	
	<table width="100%" cellpadding="0px" cellspacing="0px">
		<tr>
		    <td class="planJourneyLocationInstructions" >
		        <asp:label id="labelCurrentLocationInstructions" runat="server"></asp:label>
		    </td>
			<td>
			    <table width="100%" cellpadding="0" cellspacing="4px">
			        <tr>
			            <td>
			                <div class="DisplayInTable">
				                <cc1:tdbutton id="buttonTravelFrom" runat="server"></cc1:tdbutton>
                		
                			
				                <cc1:tdbutton id="buttonTravelTo" runat="server"></cc1:tdbutton>
                			
				                
				            </div>
			            </td>
			            <td align="right">
			                <cc1:tdbutton id="buttonCancel" runat="server" text="" scriptname="MapLocationControl" causesvalidation="false" action="return Plan_Cancel();"></cc1:tdbutton>
			            </td>
			        </tr>
			    </table>
			    
		    </td>
		</tr>
	</table>
</div>
