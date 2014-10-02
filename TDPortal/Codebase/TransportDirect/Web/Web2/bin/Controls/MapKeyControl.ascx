<%@ Control Language="c#" AutoEventWireup="True" Codebehind="MapKeyControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.MapKeyControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Import namespace="TransportDirect.UserPortal.Web.Controls" %>
<div class="mkMapKeyContainer floatleftonly">
    <div class="mkMapKeyHeading">
        <asp:Label ID="labelKey" runat="server" CssClass="txtnineb"></asp:Label>&nbsp;
    </div>
    
    <div class="mkMapKeys">
        <asp:repeater id="keyRepeater" runat="server">
	        <headertemplate>
		        <table cellspacing="0" class="mkMapKeyTable" lang="en">
	        </headertemplate>
	        <itemtemplate>
		        <tr>
			        <td valign="top"><span class="txtseven"><%# StrongTagOpen(Container.ItemIndex) %><%# ((KeyImagePair)(Container.DataItem)).Key %><%# StrongTagClose(Container.ItemIndex) %></span></td>
			        <td><img src='<%# ((KeyImagePair)(Container.DataItem)).ImageUrl %>' 
			                 alt="<%# ((KeyImagePair)(Container.DataItem)).AlternateText %>"
			                 title="<%# ((KeyImagePair)(Container.DataItem)).AlternateText %>" /></td>
		        </tr>
	        </itemtemplate>
	        <footertemplate>
		        </table>
	        </footertemplate>
        </asp:repeater>
    </div>
</div>