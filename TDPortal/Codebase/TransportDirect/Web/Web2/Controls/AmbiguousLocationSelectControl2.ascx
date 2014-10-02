<%@ Control Language="c#" AutoEventWireup="True" Codebehind="AmbiguousLocationSelectControl2.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.AmbiguousLocationSelectControl2" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<table lang="en" id="Table1" class="alertwarning" cellspacing="3" summary="location selection 1">
    <tr>
        <td><asp:label id="labelLocationTitle" runat="server" cssclass="txtseven"></asp:label></td>
    </tr>
    <tr>
        <td><asp:label id="labelSRSelect" runat="server" cssclass="screenreader"></asp:label>
            <asp:dropdownlist id="listLocations" runat="server" cssclass="locationdrop"></asp:dropdownlist></td>
    </tr>
</table>
<asp:table id="radioButtonTable" cssclass="spacersearchtype" runat="server">
    <asp:tablerow>
        <asp:tablecell cssclass="txtseven"></asp:tablecell>
        <asp:tablecell cssclass="txtseven"></asp:tablecell>
        <asp:tablecell cssclass="txtseven"></asp:tablecell>
    </asp:tablerow>
    <asp:tablerow>
        <asp:tablecell cssclass="txtseven"></asp:tablecell>
        <asp:tablecell cssclass="txtseven"></asp:tablecell>
        <asp:tablecell cssclass="txtseven">
            <asp:imagebutton id="commandNewLocation" runat="server"></asp:imagebutton>
        </asp:tablecell>
    </asp:tablerow>
</asp:table>
