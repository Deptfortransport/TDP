<%@ Register TagPrefix="uc1" TagName="HeaderControl" Src="../Controls/HeaderControl.ascx" %>
<%@ Page language="C#" Codebehind="UserSurvey.aspx.cs" AutoEventWireup="True" Inherits="TransportDirect.UserPortal.Web.Templates.UserSurvey" %>
<%@ Register TagPrefix="uc1" TagName="UserSurveyRadioMatrix" Src="../Controls/UserSurveyRadioMatrix.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web"%>
<%@ Register TagPrefix="uc1" TagName="PrintableHeaderControl" Src="../Controls/PrintableHeaderControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="TrackingControl" Src="../Controls/TrackingControl.ascx" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html lang="<%=TransportDirect.Common.TDCultureInfo.CurrentUICulture.Name%>" xmlns="http://www.w3.org/1999/xhtml">
	<head>
		<cc1:headelementcontrol id="headElementControl" runat="server" stylesheets="homepage.css,setup.css,jpstdprint.css,jpstd.css,nifty.css,UserSurvey.aspx.css"></cc1:headelementcontrol>
	</head>
	<body text="#00a000">
	<div class="CenteredContent">
		<form id="UserSurvey" method="post" runat="server">
			<uc1:printableheadercontrol id="printableHeaderControl" runat="server"></uc1:printableheadercontrol>
			<table id="Table1" width="640">
				<tr>
					<td>
						<h1><asp:label id="LabelSurveyHeader" runat="server"></asp:label></h1>
					</td>
				</tr>
				<tr>
					<td>
						<p><asp:label id="LabelMainInstruction" runat="server"></asp:label></p>
					</td>
				</tr>
				<tr>
					<td valign="middle" align="center"><asp:panel id="PanelHeaderImages" runat="server">
<asp:image id="ImageSection1Arrow1" runat="server" visible="false"></asp:image>
<asp:image id="ImageSection1Arrow1Grey" runat="server" visible="true"></asp:image>....... 
<asp:image id="ImageSection1Arrow2" runat="server" visible="true"></asp:image>
<asp:image id="ImageSection1Arrow2Grey" runat="server" visible="False"></asp:image>....... 
<asp:image id="ImageSection1Arrow3" runat="server" visible="true"></asp:image>
<asp:image id="ImageSection1Arrow3Grey" runat="server" visible="False"></asp:image></asp:panel></td>
				</tr>
				<tr>
					<td>
						<p class="txtsevenb"><asp:label id="LabelUserSurveyPageErrorContinue" runat="server" visible="False" forecolor="Red"></asp:label></p>
						<p class="txtsevenb"><asp:label id="LabelUserSurveyPageErrorSubmit" runat="server" visible="False" forecolor="Red"></asp:label></p>
					</td>
				</tr>
			</table>
			<asp:panel id="PanelSection1" runat="server" width="640px">
				<table id="Table2" border="0">
					<tr>
						<td colspan="2">
							<h1>
								<asp:label id="LabelSection1Header" runat="server"></asp:label></h1>
						</td>
					</tr>
					<tr><td>&nbsp;</td>
					</tr>
					<tr>
						<td valign="middle" width="50">
							<p class="txtsevenb">1.</p>
						</td>
						<td valign="middle">
							<p class="txtsevenb">
								<asp:label id="LabelQ1" runat="server"></asp:label>
								<asp:dropdownlist id="UserSurveyQ1Drop" runat="server"></asp:dropdownlist></p>
						</td>
					</tr>
					<tr><td>&nbsp;</td>
					</tr>
					<tr>
						<td valign="top">
							<p class="txtsevenb">2.</p>
						</td>
						<td>
							<p class="txtsevenb">
								<asp:label id="LabelQ2" runat="server"></asp:label></p>
							 
								<asp:radiobuttonlist id="UserSurveyQ2Radio" runat="server" repeatcolumns="2" CssClass="txtseven"></asp:radiobuttonlist>
							<p class="txtseven">
								<asp:requiredfieldvalidator id="UserSurveyQ2RadioRFValidator" runat="server" font-bold="True" enableclientscript="False" controltovalidate="UserSurveyQ2Radio" display="Dynamic"></asp:requiredfieldvalidator></p>
						</td>
					</tr>
					<tr><td>&nbsp;</td>
					</tr>
					<tr>
						<td valign="top">
							<p class="txtsevenb">3.</p>
						</td>
						<td>
							<p class="txtsevenb">
								<asp:label id="LabelQ3" runat="server"></asp:label>
								<asp:label id="LabelQ3TickAny" runat="server" cssclass="txtninen"></asp:label></p>
							
								<asp:checkboxlist id="UserSurveyQ3Check" runat="server" width="100%" repeatcolumns="4" CssClass="txtseven"></asp:checkboxlist>
							<p class="txtsevenb">
								<cc1:checkboxlistrequiredfieldvalidator id="UserSurveyQ3CheckRFValidator" runat="server" controltovalidate="UserSurveyQ3Check" display="Dynamic"></cc1:checkboxlistrequiredfieldvalidator></p>
						</td>
					</tr>
					<tr><td>&nbsp;</td>
					</tr>
					<tr>
						<td valign="top">
							<p class="txtsevenb">4.</p>
						</td>
						<td>
							<p class="txtsevenb">
								<asp:label id="LabelQ4" runat="server"></asp:label>
								<asp:label id="LabelQ4TickAny" runat="server" cssclass="txtninen"></asp:label></p>
							
								<asp:checkboxlist id="UserSurveyQ4Check" runat="server" width="100%" repeatcolumns="4" CssClass="txtseven"></asp:checkboxlist>
							<p class="txtsevenb">
								<cc1:checkboxlistrequiredfieldvalidator id="UserSurveyQ4CheckRFValidator" runat="server" controltovalidate="UserSurveyQ4Check" display="Dynamic"></cc1:checkboxlistrequiredfieldvalidator></p>
						</td>
					</tr>
					<tr><td>&nbsp;</td>
					</tr>
					<tr>
						<td valign="top">
							<p class="txtsevenb">5.</p>
						</td>
						<td>
							<p class="txtsevenb">
								<asp:label id="LabelQ5" runat="server"></asp:label></p>
							
								<asp:radiobuttonlist id="UserSurveyQ5Radio" runat="server" repeatcolumns="0" CssClass="txtseven"></asp:radiobuttonlist>
							<p class="txtsevenb">
								<asp:requiredfieldvalidator id="UserSurveyQ5RadioRFValidator" runat="server" enableclientscript="False" controltovalidate="UserSurveyQ5Radio" display="Dynamic"></asp:requiredfieldvalidator></p>
						</td>
					</tr>
					<tr><td>&nbsp;</td>
					</tr>
					<tr>
						<td valign="top">
							<p class="txtsevenb">6.</p>
						</td>
						<td class="txtsevenb">
							<asp:label id="LabelQ6" runat="server"></asp:label>
							<asp:label id="LabelQ6TickAny" runat="server" cssclass="txtninen"></asp:label></td>
					</tr>
					<tr>
						<td>&nbsp;</td>
						<td>
							<table id="Table3">
								<tr>
									<td>
										<p class="txtseven">
											<asp:checkbox id="UserSurveyQ6Check1" runat="server"></asp:checkbox>&nbsp;
											<asp:dropdownlist id="UserSurveyQ6Drop1" runat="server"></asp:dropdownlist></p>
									</td>
								</tr>
								<tr>
									<td>
										<p class="txtseven">
											<asp:checkbox id="UserSurveyQ6Check2" runat="server"></asp:checkbox></p>
									</td>
								</tr>
								<tr>
									<td>
										<p class="txtseven">
											<asp:checkbox id="UserSurveyQ6Check3" runat="server"></asp:checkbox>&nbsp;
											<asp:dropdownlist id="UserSurveyQ6Drop2" runat="server"></asp:dropdownlist></p>
									</td>
								</tr>
								<tr>
									<td class="txtsevenb">
										<asp:label id="LabelQ6Error1" runat="server" forecolor="Red" font-bold="True"></asp:label>
										<asp:label id="LabelQ6Error2" runat="server" forecolor="Red" font-bold="True"></asp:label></td>
								</tr>
							</table>
						</td>
					</tr>
					<tr><td>&nbsp;</td>
					</tr>
					<tr>
						<td valign="top">
							<p class="txtsevenb">7.</p>
						</td>
						<td>
							<p class="txtsevenb">
								<asp:label id="LabelQ7" runat="server"></asp:label></p>
						</td>
					</tr>
					<tr>
						<td></td>
						<td>
							
								<asp:radiobuttonlist id="UserSurveyQ7Radio" runat="server" repeatcolumns="0" autopostback="True" CssClass="txtseven"></asp:radiobuttonlist>
							<p class="txtseven">
								<asp:requiredfieldvalidator id="UserSurveyQ7RadioRFValidator" runat="server" font-bold="True" enableclientscript="False" controltovalidate="UserSurveyQ7Radio" display="Dynamic"></asp:requiredfieldvalidator></p>
						</td>
					</tr>
					<tr><td>&nbsp;</td>
					</tr>
					<tr>
						<td valign="top">
							<p class="txtsevenb">8.</p>
						</td>
						<td>
							<p class="txtsevenb">
								<asp:label id="LabelQ8" runat="server" font-bold="true"></asp:label>
								<asp:label id="LabelQ8TickAny" runat="server" cssclass="txtninen"></asp:label></p>
							
								<asp:checkboxlist id="UserSurveyQ8Check" runat="server" repeatcolumns="0" CssClass="txtseven"></asp:checkboxlist>
							<p class="txtsevenb">
								<cc1:checkboxlistrequiredfieldvalidator id="UserSurveyQ8CheckRFValidator" runat="server" controltovalidate="UserSurveyQ8Check" display="Dynamic"></cc1:checkboxlistrequiredfieldvalidator></p>
						</td>
					</tr>
					<tr>
						<td align="right" colspan="2">
							<cc1:tdbutton id="ButtonContinuePage1" runat="server"></cc1:tdbutton></td>
					</tr>
				</table>
			</asp:panel><asp:panel id="PanelSection2" runat="server" width="640px">
				<table id="Table4" border="0">
					<tr>
						<td colspan="2">
							<div>
								<h1>
									<asp:label id="LabelSection2Header" runat="server"></asp:label></h1>
							</div>
						</td>
					</tr>
					<tr><td>&nbsp;</td>
					</tr>
					<tr>
						<td valign="top" width="50">
							<p class="txtsevenb">9.</p>
						</td>
						<td>
							<p class="txtsevenb">
								<asp:label id="LabelQ9" runat="server"></asp:label></p>
							<uc1:usersurveyradiomatrix id="UserSurveyQ9RadioMatrix" runat="server"></uc1:usersurveyradiomatrix></td>
					</tr>
					<tr><td>&nbsp;</td>
					</tr>
					<tr>
						<td valign="top">
							<p class="txtsevenb">10.</p>
						</td>
						<td>
							<p class="txtsevenb">
								<asp:label id="LabelQ10" runat="server"></asp:label>
								<asp:label id="LabelQ10TickAny" runat="server" cssclass="txtninen"></asp:label></p>
							
								<asp:checkboxlist id="UserSurveyQ10Check" runat="server" repeatcolumns="2" CssClass="txtseven"></asp:checkboxlist>
							<p class="txtsevenb">
								<cc1:checkboxlistrequiredfieldvalidator id="UserSurveyQ10CheckRFValidator" runat="server" controltovalidate="UserSurveyQ10Check" display="Dynamic"></cc1:checkboxlistrequiredfieldvalidator></p>
						</td>
					</tr>
					<tr><td>&nbsp;</td>
					</tr>
					<tr>
						<td valign="top">
							<p class="txtsevenb">11.</p>
						</td>
						<td>
							<p class="txtsevenb">
								<asp:label id="LabelQ11" runat="server"></asp:label>
								<asp:label id="LabelQ11TickAny" runat="server" cssclass="txtninen"></asp:label></p>
							
								<asp:checkboxlist id="UserSurveyQ11Check" runat="server" repeatcolumns="0" CssClass="txtseven"></asp:checkboxlist>
							<p>
								<cc1:checkboxlistrequiredfieldvalidator id="UserSurveyQ11CheckRFValidator" runat="server" font-bold="True" controltovalidate="UserSurveyQ11Check" display="Dynamic"></cc1:checkboxlistrequiredfieldvalidator></p>
						</td>
					</tr>
					<tr><td>&nbsp;</td>
					</tr>
					<tr>
						<td valign="top">
							<p class="txtsevenb">12.</p>
						</td>
						<td>
							<p class="txtsevenb">
								<asp:label id="LabelQ12" runat="server"></asp:label></p>
							
								<asp:radiobuttonlist id="UserSurveyQ12Radio" runat="server" repeatcolumns="0"  CssClass="txtseven"></asp:radiobuttonlist>
							<p>
								<asp:requiredfieldvalidator id="UserSurveyQ12RadioRFValidator" runat="server" font-bold="True" enableclientscript="False" controltovalidate="UserSurveyQ12Radio" display="Dynamic"></asp:requiredfieldvalidator></p>
						</td>
					</tr>
					<tr><td>&nbsp;</td>
					</tr>
					<tr>
						<td valign="top">
							<p class="txtsevenb">13.</p>
						</td>
						<td>
							<p class="txtsevenb">
								<asp:label id="LabelQ13" runat="server"></asp:label></p>
							
								<asp:radiobuttonlist id="UserSurveyQ13Radio" runat="server" CssClass="txtseven"></asp:radiobuttonlist>

							<p>
								<asp:requiredfieldvalidator id="UserSurveyQ13RadioRFValidator" runat="server" font-bold="True" enableclientscript="False" controltovalidate="UserSurveyQ13Radio" display="Dynamic"></asp:requiredfieldvalidator></p>
						</td>
					</tr>
					<tr>
						<td align="right" colspan="2">
							<cc1:tdbutton id="ButtonContinuePage2" runat="server"></cc1:tdbutton></td>
					</tr>
				</table>
			</asp:panel><asp:panel id="PanelSection3" runat="server" width="640px">
				<table border="0">
					<tr>
						<td colspan="3">
							<div>
								<h1>
									<asp:label id="LabelSection3Header" runat="server"></asp:label></h1>
							</div>
						</td>
					</tr>
					<tr><td>&nbsp;</td>
					</tr>
					<tr>
						<td valign="top">
							<p class="txtsevenb">14.</p>
						</td>
						<td colspan="2">
							<p class="txtsevenb">
								<asp:label id="LabelQ14" runat="server"></asp:label></p>
							
								<asp:radiobuttonlist id="UserSurveyQ14Radio" runat="server" repeatcolumns="2" CssClass="txtseven"></asp:radiobuttonlist>
							<p>
								<asp:requiredfieldvalidator id="UserSurveyQ14RadioRFValidator" runat="server" font-bold="True" enableclientscript="False" controltovalidate="UserSurveyQ14Radio" display="Dynamic"></asp:requiredfieldvalidator></p>
						</td>
					</tr>
					<tr><td>&nbsp;</td>
					</tr>
					<tr>
						<td valign="top">
							<p class="txtsevenb">15.</p>
						</td>
						<td colspan="2">
							<p class="txtsevenb">
								<asp:label id="LabelQ15" runat="server"></asp:label></p>
							
								<asp:radiobuttonlist id="UserSurveyQ15Radio" runat="server" repeatcolumns="2" CssClass="txtseven"></asp:radiobuttonlist>
							<p>
								<asp:requiredfieldvalidator id="UserSurveyQ15RadioRFValidator" runat="server" font-bold="True" enableclientscript="False" controltovalidate="UserSurveyQ15Radio" display="Dynamic"></asp:requiredfieldvalidator></p>
						</td>
					</tr>
					<tr><td>&nbsp;</td>
					</tr>
					<tr>
						<td valign="middle">
							<p class="txtsevenb">16.</p>
						</td>
						<td valign="middle" colspan="2">
							<p class="txtsevenb">
								<asp:label id="LabelQ16" runat="server"></asp:label>
								<asp:dropdownlist id="UserSurveyQ16Drop" runat="server"></asp:dropdownlist><br />
								<asp:label id="LabelQ16Postcode" runat="server"></asp:label>
								<asp:textbox id="TextBoxQ16" runat="server"></asp:textbox></p>
							
								<asp:requiredfieldvalidator id="UserSurveyQ16DropRFValidator" runat="server" font-bold="True" enableclientscript="False" controltovalidate="UserSurveyQ16Drop" ondatabinding="UserSurveyContactRadio_SelectedIndexChanged" CssClass="txtsevenb"></asp:requiredfieldvalidator>
						</td>
					</tr>
					<tr><td>&nbsp;</td>
					</tr>
					<tr>
						<td class=" txtsevenb" colspan="3">
							<asp:label id="LabelThankYou" runat="server"></asp:label></td>
					</tr>
					<tr>
						<td colspan="3">
							<p>
								<asp:label id="LabelContactYou" runat="server"></asp:label></p>
						</td>
					</tr>
					<tr>
						<td colspan="3">
							
								<asp:radiobuttonlist id="UserSurveyContactRadio" runat="server" repeatcolumns="2" autopostback="True" CssClass="txtseven"></asp:radiobuttonlist>
								<asp:requiredfieldvalidator id="UserSurveyContactRadioRFValidator" runat="server" font-bold="True" enableclientscript="False" controltovalidate="UserSurveyContactRadio" display="Dynamic"></asp:requiredfieldvalidator>
						</td>
					</tr>
					<tr>
						<td colspan="3">
							<p>
								<asp:label id="LabelIfYesType" runat="server"></asp:label></p>
						</td>
					</tr>
					<tr>
						<td colspan="3">
							<table>
								<tr>
									<td class="txtseven">
										<asp:label id="LabelTypeEmail" runat="server"></asp:label></td>
									<td class="txtseven">
										<asp:textbox id="TextUserSurveyEmail1" runat="server" enabled="False" bordercolor="Silver" backcolor="Silver"></asp:textbox></td>
									<td class="txtsevenb">
										<asp:regularexpressionvalidator id="UserSurveyEmailRegExpValidator" runat="server" font-bold="True" enableclientscript="False" controltovalidate="TextUserSurveyEmail1" display="Dynamic" validationexpression="[a-zA-Z_0-9!#$%&'*+\-/=?^`{\|}~]+(\.[a-zA-Z_0-9!#$%&'*+\-z=?^`{\|}~]+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:regularexpressionvalidator>
										<asp:requiredfieldvalidator id="UserSurveyEmailRFValidator" runat="server" font-bold="True" enableclientscript="False" controltovalidate="TextUserSurveyEmail1" display="Dynamic"></asp:requiredfieldvalidator></td>
								</tr>
								<tr>
									<td class="txtseven">
										<asp:label id="LabelTypeEmailAgain" runat="server"></asp:label></td>
									<td class="txtseven">
										<asp:textbox id="TextUserSurveyEmail2" runat="server" enabled="False" backcolor="Silver"></asp:textbox></td>
									<td class="txtsevenb">
										<asp:comparevalidator id="UserSurveyEmailCompareValidator" runat="server" visible="False" font-bold="True" enableclientscript="False" controltovalidate="TextUserSurveyEmail2" controltocompare="TextUserSurveyEmail1"></asp:comparevalidator></td>
								</tr>
							</table>
						</td>
					</tr>
					<tr><td>&nbsp;</td>
					</tr>
					<tr>
						<td colspan="3">
							<p>
								<asp:label id="LabelBeAssured" runat="server"></asp:label></p>
						</td>
					</tr>
					<tr>
						<td colspan="2"></td>
						<td align="right">
							<cc1:tdbutton id="ButtonSubmit" runat="server"></cc1:tdbutton></td>
					</tr>
				</table>
			</asp:panel><asp:panel id="PanelSection4" runat="server" width="640px">
				<table id="Table5">
					<tr>
						<td>
							<h1>
								<asp:label id="LabelUserSurveyThankYou" runat="server"></asp:label></h1>
						</td>
					</tr>
					<tr>
						<td class="txtsevenb">
							<asp:label id="LabelUserSurveyClose" runat="server"></asp:label></td>
					</tr>
				</table>
			</asp:panel></form>
			</div>
			<uc1:TrackingControl id="TrackingControl" runat="server"></uc1:TrackingControl>
	</body>
</html>
