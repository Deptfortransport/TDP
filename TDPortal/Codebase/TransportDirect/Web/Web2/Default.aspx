<%@ Register TagPrefix="uc1" TagName="HeaderControl" Src="Controls/HeaderControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FooterControl" Src="Controls/FooterControl.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<%@ Page language="c#" Codebehind="Default.aspx.cs" validateRequest = "false" AutoEventWireup="true" Inherits="TransportDirect.UserPortal.Web.Default" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/tr/xhtml1/DTD/xhtml1-transitional.dtd">
<HTML 
lang="<%=TransportDirect.Common.TDCultureInfo.CurrentUICulture.Name%>">
	<HEAD>
		<cc1:headelementcontrol id="headElementControl" runat="server" stylesheets="setup.css,jpstd.css,homepage.css,expandablemenu.css, nifty.css,MapIncidents.css"></cc1:headelementcontrol>
		<meta name="ROBOTS" content="NOODP" />
	</HEAD>
	<body>
    <div class="CenteredContent">	
	    <div id="SkipToMainContentArea" class="SkipToMainContentArea">
	    </div>
		<form id="Form1" method="post" runat="server">
			<P> 
				<uc1:HeaderControl id="HeaderControl1" runat="server"></uc1:HeaderControl></P>
			<P>
				<asp:Panel id="placeHolderPanel" runat="server"></asp:Panel></P>
			<P>
			<uc1:footercontrol id="FooterControl1" runat="server" enableviewstate="false"></uc1:footercontrol>
        </form>			
    </div>
	</body>
</HTML>
