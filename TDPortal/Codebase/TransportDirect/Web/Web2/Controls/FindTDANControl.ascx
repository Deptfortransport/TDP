<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FindTDANControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.FindTDANControl" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>

<div class="accessibleLocationControl">
    <table>
        <tr>
            <td>
	            <div id="locationNameRow" runat="server" class="locationNameRow" visible="true">
	                <asp:Label ID="locationName" runat="server" CssClass="txtseven locationName" EnableViewState="false"></asp:Label>	    
	            </div>
            </td>
        </tr>
        <tr>
            <td>
                <div id="locationDropDownRow" runat="server" class="accessibleLocDropDownRow" visible="true">
                    <asp:DropDownList ID="locationDrop" runat="server" CssClass="accessibleLocDrop" ></asp:DropDownList>
                    <cc1:TDButton ID="findOnMapBtn" runat="server" OnClick="findOnMapBtn_Click" CssClass="TDButtonDefault findOnMap hide" CssClassMouseOver="TDButtonDefaultMouseOver findOnMap hide" />
                </div>
                <div id="locationErrorRow" runat="server" class="locationErrorRow" visible="false">
                    <asp:Label ID="locationError" runat="server" CssClass="txtseven" EnableViewState="false"></asp:Label>
                </div>
            </td>
        </tr>
    </table>    
</div>