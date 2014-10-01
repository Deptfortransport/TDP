<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FooterControl.ascx.cs" Inherits="TDP.UserPortal.TDPMobile.Controls.FooterControl" %>

<div class="clear"></div>
<div id="footer" class="footer">
    <div id="bottomNav" class="bottomNav">
		<a id="bottomnav"></a>
		<div id="nav" class="navWrap">
		    <div class="colNav colNavFirst">
			    <ul>
				    <li class="navItem">
                        <a id="privacyLink" href="" rel="external" accesskey="4" runat="server"></a>
                    </li>
			    </ul>
            </div>
            <div class="colNav colNavSecond">
			    <ul>
			    <li class="navItem">
			        <div class="fullsite">
			            <a id="fullsiteLink" rel="external" accesskey="5" runat="server"></a>
		            </div>	    
			    </li>
			    </ul>
		    </div>
		    <div class="colNav colNavThird">
			    <ul>
			    <li id="liLanguageLink" runat="server" class="navItem language">
                    <asp:LinkButton id="lnkbtnLanguage" runat="server" CssClass="languageLink jshide" AccessKey="6"></asp:LinkButton>
                    <noscript>
                        <asp:Button id="btnLanguage" runat="server" CssClass="languageLink" AccessKey="6"></asp:Button>
                    </noscript>
			    </li>
			    </ul>
		    </div>
		    <div class="clear"></div>            
		</div>
	</div>
</div>
