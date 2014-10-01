<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HeaderControl.ascx.cs" Inherits="TDP.UserPortal.TDPMobile.Controls.HeaderControl" %>

<a id="top"></a>
<div class="header">

    <div id="backDiv" runat="server" enableviewstate="false" class="headerBack">
        <div class="screenReaderOnly">
            <h2 id="navigationTitle" runat="server" enableviewstate="false"></h2>
        </div>
        <div class="topNavLeftDiv" id="topNavLeftDiv" runat="server" enableviewstate="false">
            <asp:Button ID="backBtn" runat="server" OnClick="backBtn_Click" CssClass="topNavLeft" EnableViewState="false" TabIndex="1"></asp:Button>
        </div>
        <div class="topNavRightDiv" id="topNavRightDiv" runat="server" enableviewstate="false">
            <asp:Button ID="nextBtn" runat="server" OnClick="nextBtn_Click" CssClass="topNavRight" EnableViewState="false" TabIndex="2"></asp:Button>
        </div>
	</div>
    
    <div>
        <div class="menuWrap jshide">
            <asp:HyperLink ID="lnkMenuWrap" runat="server" aria-haspopup="true" TabIndex="4"></asp:HyperLink>
		</div>
        <div class="logoWrap">
			<div class="logo">
				<a href="./Default.aspx" title="Transport Direct" rel="external" accesskey="1" tabindex="3"></a>
			</div>
		</div>
    </div>

    <a id="menunav"></a>
    <div id="menuHolder" class="menuContainer">
        <asp:Repeater ID="rptMenu" runat="server" EnableViewState="false" OnItemDataBound="Menu_ItemDataBound">
            <HeaderTemplate>
                <div class="menuNav">
                    <ul role="menu">
            </HeaderTemplate>
            <ItemTemplate>
                        <li class="menuItem" role="menuitem">
                            <asp:HyperLink ID="lnkMenuLink" runat="server" EnableViewState="false" CssClass="menuLink"></asp:HyperLink>
                        </li>
            </ItemTemplate>
            <FooterTemplate>
                    </ul>
                </div>
            </FooterTemplate>
        </asp:Repeater>
    </div>
</div>
<div class="clear"></div>