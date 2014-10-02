<%@ Control Language="c#" AutoEventWireup="True" Codebehind="ExpandableMenuControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.ExpandableMenuControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<div id="expandableMenu" runat="server" class="<%# CategoryCssClass %>">
	<asp:repeater id="categories" runat="server" enableviewstate="False">
		<itemtemplate>
			<ul id="category" class="categoryLink" runat="server">
				<asp:repeater id="rootLinkRepeater" runat="server" enableviewstate="False">
					<itemtemplate>
						<li><cc1:scriptablehyperlink id="rootLink" runat="server" onmouseover="this.style.cursor='hand'"></cc1:scriptablehyperlink></li>
					</itemtemplate>
				</asp:repeater>
			</ul>
			<ul id="subcategory" class="subcategoryLink" runat="server">
				<asp:repeater id="subcategoryLinkRepeater" runat="server" enableviewstate="False">
					<itemtemplate>
						<li>
						    <cc1:scriptablehyperlink id="subrootLink" runat="server" onmouseover="this.style.cursor='hand'" visible="false"></cc1:scriptablehyperlink>
						    <asp:hyperlink id="subcategoryLink" runat="server" enableviewstate="False"></asp:hyperlink>
						    <ul id="subrootcategory" class="subrootcategoryLink" runat="server" visible="false" style="display:none;">
						        <asp:repeater id="subrootcategoryLinkRepeater" runat="server" enableviewstate="False" Visible="false">
						            <itemtemplate>
						               <li>
						                    <asp:hyperlink id="subrootcategoryLink" runat="server" enableviewstate="False"></asp:hyperlink>
						               </li>
						            </itemtemplate>
						        </asp:repeater>
						    </ul>
						</li>
					</itemtemplate>
				</asp:repeater>
			</ul>
		</itemtemplate>
	</asp:repeater>
</div>