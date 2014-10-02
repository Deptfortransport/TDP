<%@ Control Language="c#" AutoEventWireup="True" Codebehind="BusinessLinkTemplateSelectControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.BusinessLinkTemplateSelectControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" enableviewstate="true"%>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<div class="templateSelectBox">
<table>
	<tr>
		<td>
			<asp:datalist id="dataListBusinessTemplates" repeatdirection="Horizontal" repeatcolumns="2" runat="server"
				itemstyle-verticalalign="Bottom">
				<itemtemplate>
					<cc1:TDImage runat="server" ImageUrl="<%# GetTemplateImagePath(Container.DataItem) %>" AlternateText="<%# GetTemplateImageAlt(Container.DataItem) %>" ImageAlign="bottom" /><br/>
					&nbsp;&nbsp;&nbsp;&nbsp;
					<cc1:scriptablegroupradiobutton id="templateSelectRadioButton" groupname="template" runat="server" cssclass="txtseven" value="<%# GetTemplateIdValue(Container.DataItem) %>">
					</cc1:scriptablegroupradiobutton>
					<asp:label runat="server" text="<%# GetTemplateDescription(Container.DataItem) %>" cssclass="txtseven">
					</asp:label>
					<p>&nbsp;</p>
				</itemtemplate>
			</asp:datalist>
		</td>
	</tr>
</table>
</div>