<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls"
    Assembly="td.userportal.web" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="RetailerInformationControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.RetailerInformationControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>

	<asp:table id="RetailerInformationTable" runat="server" cellspacing="5" cellpadding="1" width="50%">
		<asp:tablerow runat="server" id="TitleRow">
			<asp:tablecell runat="server" horizontalalign="Left" columnspan="2" id="titleCell">
				<h1>
					<asp:label id="labelTitle" runat="server" cssclass="h1" enableviewstate="False"></asp:label>
				</h1>
			</asp:tablecell>
		</asp:tablerow>
		<asp:tablerow runat="server" id="nameRow" cssclass="txtseven">
			<asp:tablecell id="nameLabelCell" runat="server">
				<asp:label id="labelNameHeader" runat="server" enableviewstate="False"></asp:label>
			</asp:tablecell>
			<asp:tablecell id="nameCell" runat="server" horizontalalign="Left">
				<asp:label id="labelName" runat="server" enableviewstate="False"></asp:label> &nbsp; <br />
				<cc1:TDImage id="imageCompanyLogo" runat="server" imagealign="Middle" enableviewstate="False" />
			</asp:tablecell>
		</asp:tablerow>
		<asp:tablerow runat="server" id="telephoneRow" cssclass="txtseven">
			<asp:tablecell id="telephoneLabelCell">
				<asp:label id="labelTelephoneHeader" runat="server" enableviewstate="False"></asp:label>
			</asp:tablecell>
			<asp:tablecell id="TelephoneCell" runat="server" horizontalalign="Left">
				<asp:label id="labelTelephoneValue" runat="server" enableviewstate="False"></asp:label>
			</asp:tablecell>
		</asp:tablerow>
		<asp:tablerow runat="server" id="urlRow" cssclass="txtseven">
			<asp:tablecell id="urlLabelCell" runat="server">
				<asp:label id="labelUrlHeader" runat="server" enableviewstate="False"></asp:label>
			</asp:tablecell>
			<asp:tablecell runat="server" id="URLCell" horizontalalign="Left">
				<asp:label id="labelUrlValue" runat="server" enableviewstate="False"></asp:label>
			</asp:tablecell>
		</asp:tablerow>
	</asp:table>

