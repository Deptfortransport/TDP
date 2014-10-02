<%@ Control Language="c#" AutoEventWireup="True" Codebehind="ClaimPersonalDetails.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.ClaimPersonalDetails" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<div><A href="#"></A></div>
<table border="0" width="550">
	<tr>
		<td width="1%"><p><asp:label id="labelMandatoryFirstName" Runat="server" ForeColor="Black">*</asp:label></p>
		</td>
		<td width="15%" align="right"><p><asp:label id="labelFirstName" Runat="server"></asp:label></p>
		</td>
		<td width="3%"><asp:image id="imgErrorFirstName" runat="server" ImageUrl="/web2/Images/gifs/JourneyPlanning/alertArrow.gif" Visible="false"></asp:image></td>
		<td><asp:textbox id="textFirstName" Runat="server" Width="100%" Columns="70"></asp:textbox></td>
	</tr>
	<tr>
		<td><p><asp:label id="labelMandatoryLastName" Runat="server" ForeColor="Black">*</asp:label></p>
		</td>
		<td align="right"><p><asp:label id="labelLastName" Runat="server"></asp:label></p>
		</td>
		<td><asp:image id="imgErrorLastName" runat="server" ImageUrl="/web2/Images/gifs/JourneyPlanning/alertArrow.gif" Visible="false"></asp:image></td>
		<TD><asp:textbox id="textLastName" Runat="server" Width="100%" Columns="70"></asp:textbox></TD>
	</tr>
	<TR>
		<td><p><asp:label id="labelMandatoryAddress" Runat="server" ForeColor="Black">*</asp:label></p>
		</td>
		<td align="right"><p><asp:label id="labelAddress1" Runat="server"></asp:label></p>
		</td>
		<TD><asp:image id="imgErrorAddress1" runat="server" ImageUrl="/web2/Images/gifs/JourneyPlanning/alertArrow.gif" Visible="false"></asp:image></TD>
		<TD><asp:textbox id="textAddress1" Runat="server" Width="100%" Columns="70"></asp:textbox></TD>
	</TR>
	<TR>
		<TD></TD>
		<TD></TD>
		<TD></TD>
		<TD><asp:textbox id="textAddress2" Runat="server" Width="100%" Columns="70"></asp:textbox></TD>
	</TR>
	<TR>
		<TD></TD>
		<TD></TD>
		<TD></TD>
		<TD><asp:textbox id="textAddress3" Runat="server" Width="100%" Columns="70"></asp:textbox></TD>
	</TR>
	<TR>
		<td><p><asp:label id="labelMandatoryPostCode" Runat="server" ForeColor="Black">*</asp:label></p>
		</td>
		<td align="right"><p><asp:label id="labelPostCode" Runat="server"></asp:label></p>
		</td>
		<TD><asp:image id="imgErrorPostCode" runat="server" ImageUrl="/web2/Images/gifs/JourneyPlanning/alertArrow.gif" Visible="false"></asp:image></TD>
		<TD><asp:textbox id="textPostCode" Runat="server" Width="50%" Columns="40"></asp:textbox></TD>
	</TR>
</table>
