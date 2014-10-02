<%@ Register TagPrefix="uc1" TagName="HeaderControl" Src="Controls/HeaderControl.ascx" %>

<%@ Page Language="c#" Codebehind="TimeOut.aspx.cs" AutoEventWireup="True" Inherits="TransportDirect.UserPortal.Web.Templates.TimeOut"
    EnableViewState="False" ValidateRequest="false" %>

<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls"
    Assembly="td.userportal.web" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="<%=TransportDirect.Common.TDCultureInfo.CurrentUICulture.Name%>" xmlns="http://www.w3.org/1999/xhtml">
<head>
    <cc1:HeadElementControl ID="headElementControl" runat="server" Stylesheets="setup.css, jpstd.css, homepage.css, TimeOut.aspx.css">
    </cc1:HeadElementControl>
</head>
<body>
	<div id="SkipToMainContentArea" class="SkipToMainContentArea">
	</div>
    <div class="CenteredContent">
        <form id="HomeDefault" method="post" runat="server">
            <uc1:headercontrol id="headerControl" runat="server">
            </uc1:headercontrol>
            <div class="TextArea">
            <table id="informationbox">
                <tr>
                    <td>
                        <asp:Panel runat="server" id="TimeOutHtmlPlaceholderDefinition"></asp:Panel>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <asp:Panel ID="panelMessage" runat="server">
                            <!-- don't show link if Javascript disabled -->

                            <script type="text/javascript">                                
						<!-- 	
								document.write("<p><span id=\"link\"><%=GetInstructionPrefix()%> <a id=\"anchor\" class=\"errorLink\" href=\"<%=GetHRefValue()%>\" onclick=\"CloseChildWindow('<%=GetChannelLanguage()%>');\" ><%=GetAnchorText()%></a><%=GetInstructionSuffix()%></span></p>");  														
						//-->
                            </script>

                            <noscript>
                                <p>
                                    <span id="closeBrowser">
                                        <%=GetCloseBrowserText()%>
                                    </span>
                                </p>
                            </noscript>
                        </asp:Panel>
                        <asp:Panel ID="panelUserSurvey" runat="server">
                            <p>
                                <span id="closeUserSurveyBrowser">
                                    <%=GetCloseBrowserText()%>
                                </span>
                            </p>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
            </div>
        </form>
    </div>
    <!-- Start of Intellitracker Tag -->
<script type="text/javascript"><!--
    var pqry = "SK%3dhfmi0f55obep0vyreoep42iz";
    var rqry = "iREGQry";
    var sqry = "iSale";

    var dt = window.document, nr = navigator, ina = nr.appName, sr = "0&0", px = 0, je = 0, sv = 10;
    var sev = 0/*@cc_on + @_jscript_version * 10@*/; if (sev >= 30) { je = (nr.javaEnabled() ? 1 : 2); sv = (sev >= 50 ? 14 : 13); } else { sv = 11 };
    var inav = nr.appVersion, iie = inav.indexOf('MSIE '), intp = (ina.indexOf('Netscape') >= 0);
    if (iie > 0) inavi = parseInt(inav.substring(iie + 5)); else inavi = parseFloat(inav);
    if (screen) { px = (iie > 0 ? screen.colorDepth : screen.pixelDepth); sr = screen.width + "&" + screen.height };
    function irs(s, f, r) { var p = s.indexOf(f); while (p >= 0) { s = s.substring(0, p) + r + s.substring(p + f.length, s.length); p = s.indexOf(f) } return s }
    function cesc(s) { if (s.length > 0) return irs(irs(irs(irs(irs(s, '+', '%2B'), '.', '%2E'), '/', '%2F'), '=', '%3D'), '&', '%26'); else return s; }
    function iesc(s) { return cesc(escape(s)); }
    function gpr() {
        var pr = '', ipw = window, ipr = 'window', iwL = '', ipL = '';
        while (ipL == iwL) {
            iw = ipw; pr = iw.document.referrer;
            if (intp) break; if (('' + iw.parent.location) == '') break;
            iwL = (iw.document.location.protocol + '\/\/' + iw.document.location.hostname).toLowerCase();
            ipL = pr.substring(0, iwL.length).toLowerCase();
            ipr = ipr + '.parent'; ipw = eval(ipr); if (iw == ipw) break;
        } return pr;
    }
    function itrc() {
        var nw = new Date(), ce = 2, iul = '';
        if (dt.cookie) ce = 1;
        else { var ex = new Date(nw.getTime() + 1000); dt.cookie = "itc=3; EXPIRES=" + ex.toGMTString() + "; path=/"; if (dt.cookie) ce = 1; }
        if (inavi >= 4) iul = iesc(iie > 0 && nr.userLanguage ? nr.userLanguage : nr.language);
        var un = Math.round(Math.random() * 2100000000);
        il = isl + un + "&" + iesc(gpr()) + "%20&" + cesc(pqry) + "%20&" + cesc(rqry) + "%20&"
+ cesc(sqry) + "%20&" + ce + "&" + sr + "&" + px + "&" + je + "&" + sv + "&" + iul + "%20&" + nw.getTimezoneOffset() + "&" + iesc(idl) + "%20";
        if (iie > 0 && il.length > 2045) il = il.substring(0, 2045);
        var iin = 'itr1282', iwri = true;
        if (dt.images) {
            if (!dt.images[iin]) dt.write('<div style="display:none"><i' + 'mg name="' + iin + '" height="1" width="1" alt="IntelliTracker"/></div>');
            if (dt.images[iin]) { dt.images[iin].src = il + '&0'; iwri = false; } 
        }
        if (iwri) dt.write('<i' + 'mg sr' + 'c="' + il + '&0" height="1" width="1">');
    }
    var idl = window.location.href; var isl = "http" + (idl.indexOf('https:') == 0 ? 's' : '') + "://tags.transportdirect.info/e/t3.dll?1282&";
    itrc();
//--></script>
 
<script type="text/javascript"><!--
    if (iie > 0) dt.write("\<\!\-\-");
//--></script><noscript>
<div style="display:none"><img src='http://tags.transportdirect.info/e/t3.dll?1282&amp;0&amp;%20&amp;SK%3dhfmi0f55obep0vyreoep42iz&amp;iREGQry&amp;iSale&amp;0&amp;0&amp;0&amp;0&amp;0&amp;0&amp;%20&amp;1500&amp;%20&amp;0' height="1" width="1" alt="IntelliTracker"/></div>
</noscript><!--//-->
<!-- End of Intellitracker Tag -->
</body>
</html>
