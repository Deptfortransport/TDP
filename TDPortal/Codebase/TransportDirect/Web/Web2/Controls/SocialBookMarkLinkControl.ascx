<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SocialBookMarkLinkControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.SocialBookMarkLinkControl" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<%@ Register TagPrefix="uc1" TagName="ClientLinkControl" Src="../Controls/ClientLinkControl.ascx" %>

<div class="SocialBookMarkLinkContainer">
    
    <div class="SocialBookMarkLinkTitle">
        <asp:Label ID="linkTitle" runat="server" CssClass="txtseven" />
    </div>
    <table>
            <tr>
                <td class="SocialBookMarkLink">
                    
                    <uc1:ClientLinkControl ID="clientLink" runat="server" EnableViewState="False"></uc1:ClientLinkControl>
                </td>
            </tr>
    <asp:Repeater ID="socialBookMarkRepeater" runat="server" EnableViewState="false">
        
        <ItemTemplate>
            <tr>
                <td class="SocialBookMarkLink">
                    
                    <asp:HyperLink ID="hyperlinkSocialBookMark" runat="server" Target="_blank">
                        <cc1:tdimage id="imageSocialBookMark" runat="server" Width="17" Height="17"></cc1:tdimage>
                        &nbsp;<asp:Label ID="labelSocialBookmark" runat="server" CssClass="txtseven" EnableViewState="False"></asp:Label>
                    </asp:HyperLink>
                </td>
            </tr>
        </ItemTemplate>
        
    </asp:Repeater>
        <tr>
            <td class="SocialBookMarkLink">
                <cc1:TDLinkButton ID="emailLinkButton" runat="server">
                    <cc1:tdimage id="imageEmail" runat="server" Width="17" Height="17"></cc1:tdimage>
                    &nbsp;<asp:Label ID="labelEmail" runat="server" CssClass="txtseven" EnableViewState="False"></asp:Label>
                </cc1:TDLinkButton>
                <asp:HyperLink ID="emailLink" runat="server" Visible="false">
                     <cc1:tdimage id="imageEmailLink" runat="server" Width="17" Height="17"></cc1:tdimage>
                    &nbsp;<asp:Label ID="labelEmailLink" runat="server" CssClass="txtseven" EnableViewState="False"></asp:Label>
                </asp:HyperLink>
                
            </td>
        </tr>
    </table>
</div>

