<%@ Import Namespace="TransportDirect.Web" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls"
    Assembly="td.userportal.web" %>
<%@ Register TagPrefix="uc1" TagName="HyperlinkPostbackControl" Src="HyperlinkPostbackControl.ascx" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="LoginControl.ascx.cs"
    Inherits="TransportDirect.UserPortal.Web.Controls.LoginControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<asp:Panel ID="loginPanel" runat="server">
    <div class="lbouthome">
        <div class="lbd">
            <div class="lbc">
                <table width="100%">
                    <tr>
                        <td colspan="4">
                            <div id="logoutTitle">
                                <asp:Literal ID="logoutLabel" runat="server" EnableViewState="false" /></div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <h1>
                                <asp:Label ID="loginLabel" runat="server" EnableViewState="False"></asp:Label></h1>
                        </td>
                    </tr>
                    <tr id="textboxPanel" runat="server">
                        <td colspan="4">
                            <table width="100%">
                                <tr>
                                    <td>
                                        <asp:Label ID="emailLabel" associatedcontrolid="emailTxtBox" runat="server" EnableViewState="False"></asp:Label></td>
                                    <td width="65px">&nbsp;</td>
                                    <td>
                                        <div>
                                            <asp:TextBox ID="emailTxtBox" runat="server" MaxLength="255" Width="250px"></asp:TextBox><br />
                                            <asp:RegularExpressionValidator ID="emailValidator" runat="server" ValidationExpression="[a-zA-Z_0-9!#$%&'*+\-/=?^`{\|}~]+(\.[a-zA-Z_0-9!#$%&'*+\-z=?^`{\|}~]+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                ControlToValidate="emailTxtBox" Display="Dynamic" EnableClientScript="False"></asp:RegularExpressionValidator></div>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="pWordLabel" associatedcontrolid="passwordTxtBox" runat="server" EnableViewState="False"></asp:Label></td>
                                    <td width="65px">&nbsp;</td>
                                    <td>
                                        <div>
                                            <asp:TextBox ID="passwordTxtBox" runat="server" MaxLength="12" Width="134px" TextMode="Password"></asp:TextBox><br />
                                            <asp:RegularExpressionValidator ID="passwordValidator" runat="server" ValidationExpression="^([a-zA-Z0-9]{4,12})$"
                                                ControlToValidate="passwordTxtBox" Display="Dynamic" EnableClientScript="False"
                                                CssClass="txterror"></asp:RegularExpressionValidator></div>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <div>
                                            <asp:Label ID="messageLabel" runat="server" EnableViewState="False"></asp:Label>
                                            <asp:Label ID="emailInMessageLabel" runat="server" EnableViewState="False" CssClass="txtmonospace"></asp:Label></div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <div>
                                <cc1:TDButton ID="forgotPassBtn" runat="server" EnableViewState="False" />
                                 <cc1:TDButton ID="changeUserPreferencesBtn" runat="server" EnableViewState="False" Text="User Preferences"></cc1:TDButton>
                            </div>
                        </td>
                        <td align="left">
                            <div>
                                <cc1:TDButton ID="changeEmailAddressBtn" runat="server" EnableViewState="False"></cc1:TDButton>
                            </div>
                        </td>
                        <td align="left">
                            <div>
                                <cc1:TDButton ID="deleteAccountBtn" runat="server" EnableViewState="False" />
                            </div>
                        </td>
                        <td align="right">
                            <cc1:TDButton ID="logoutButton" runat="server" EnableViewState="false" />
                            <cc1:TDButton ID="logonButton" runat="server" EnableViewState="False"></cc1:TDButton></td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
</asp:Panel>
<asp:Panel ID="forgotPasswordPanel" runat="server" Visible="false">
    <div class="lbouthome">
        <div class="lbd">
            <div class="lbc">
                <table width="100%">
                    <tr>
                        <td>
                            <h1>
                                <asp:Label ID="forgotPasswordTitle" runat="server" EnableViewState="false"></asp:Label></h1>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="forgotPasswordEmailLabel" associatedcontrolid="forgotPasswordEmailTxtBox" runat="server" EnableViewState="False"></asp:Label></td>
                        <td colspan="2">
                            <div>
                                <asp:TextBox ID="forgotPasswordEmailTxtBox" runat="server" MaxLength="255" Width="250px"></asp:TextBox><br />
                                <asp:RegularExpressionValidator ID="forgotPasswordEmailValidator" runat="server"
                                    ValidationExpression="[a-zA-Z_0-9!#$%&'*+\-/=?^`{\|}~]+(\.[a-zA-Z_0-9!#$%&'*+\-z=?^`{\|}~]+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ControlToValidate="forgotPasswordEmailTxtBox"
                                    Display="Dynamic" EnableClientScript="False"></asp:RegularExpressionValidator></div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <div>
                                <asp:Label ID="forgotPasswordMessageLabel" runat="server" EnableViewState="False"></asp:Label>
                                <asp:Label ID="forgotPasswordEmailInMessageLabel" runat="server" EnableViewState="False"
                                    CssClass="txtmonospace"></asp:Label></div>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <cc1:TDButton ID="forgotPasswordCancelButton" runat="server" EnableViewState="False">
                            </cc1:TDButton></td>
                        <td align="right">
                            <cc1:TDButton ID="forgotPasswordConfirmButton" runat="server" EnableViewState="False">
                            </cc1:TDButton></td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
</asp:Panel>
<asp:Panel ID="changeEmailPanel" runat="server" Visible="false">
    <div class="lbouthome">
        <div class="lbd">
            <div class="lbc">
                <table width="100%">
                    <tr>
                        <td>
                            <h1>
                                <asp:Label ID="changeEmailTitleLabel" runat="server" EnableViewState="False"></asp:Label></h1>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="changeEmailAddressLabel1" associatedcontrolid="changeEmailAddressTextbox1" runat="server" EnableViewState="False"></asp:Label></td>
                        <td colspan="2">
                            <div>
                                <asp:TextBox ID="changeEmailAddressTextbox1" runat="server" MaxLength="255" Width="250px"></asp:TextBox><br />
                                <asp:RegularExpressionValidator ID="ChangeEmailValidator1" runat="server" ValidationExpression="[a-zA-Z_0-9!#$%&'*+\-/=?^`{\|}~]+(\.[a-zA-Z_0-9!#$%&'*+\-z=?^`{\|}~]+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                    ControlToValidate="emailTxtBox" Display="Dynamic" EnableClientScript="False"></asp:RegularExpressionValidator></div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="changeEmailAddressLabel2" associatedcontrolid="changeEmailAddressTextbox2" runat="server" EnableViewState="False"></asp:Label></td>
                        <td colspan="2">
                            <div>
                                <asp:TextBox ID="changeEmailAddressTextbox2" runat="server" MaxLength="255" Width="250px"></asp:TextBox><br />
                                <asp:RegularExpressionValidator ID="ChangeEmailValidator2" runat="server" ValidationExpression="[a-zA-Z_0-9!#$%&'*+\-/=?^`{\|}~]+(\.[a-zA-Z_0-9!#$%&'*+\-z=?^`{\|}~]+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                    ControlToValidate="emailTxtBox" Display="Dynamic" EnableClientScript="False"></asp:RegularExpressionValidator></div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <div>
                                <asp:Label ID="changeEmailMessageLabel" runat="server" EnableViewState="False"></asp:Label></div>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <cc1:TDButton ID="changeEmailCancelButton" runat="server" EnableViewState="False"></cc1:TDButton></td>
                        <td align="right">
                            <cc1:TDButton ID="changeEmailConfirmButton" runat="server" EnableViewState="False"></cc1:TDButton></td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
</asp:Panel>
<div>
    <cc1:HelpLabelControl ID="loginHelpLabel" runat="server" CssMainTemplate="helpboxLogin"
        Visible="False"></cc1:HelpLabelControl></div>
<asp:Panel ID="deleteUserPanel" runat="server" Visible="false">
    <div class="lbouthome">
        <div class="lbd">
            <div class="lbc">
                <table id="Table1" width="100%">
                    <tr>
                        <td>
                            <strong>
                                <asp:Label ID="deleteUserTitleLabel" runat="server" EnableViewState="False"></asp:Label></strong></td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="deleteUserConfirmCancelLabel" runat="server" EnableViewState="False"></asp:Label>
                            <p>
                                &nbsp;</p>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <cc1:TDButton ID="deleteUserConfirmCancelButton" runat="server" EnableViewState="False">
                            </cc1:TDButton></td>
                        <td align="right">
                            <cc1:TDButton ID="deleteUserConfirmButton" runat="server" EnableViewState="False"></cc1:TDButton></td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
</asp:Panel>
<asp:Panel ID="userPreferencePanel" runat="server" Visible="false">
    <div class="lbouthome">
        <div class="lbd">
            <div class="lbc">
                <table id="Table1" width="100%">
                    <tr>
                        <td>
                            <strong>
                                <asp:Label ID="userPreferencesTitleLabel" runat="server" EnableViewState="False"></asp:Label></strong></td>
                    </tr>
                    <tr>
                        <td colspan="2">
                        <p>
                                &nbsp;</p>
                            <asp:CheckBox ID="extendSessionTimeOutCheckBox" runat="server"
                                Checked="false" TextAlign="Right" />
                            <p>
                                &nbsp;</p>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <cc1:TDButton ID="saveUserPreferencesCancelButton" runat="server" EnableViewState="False">
                            </cc1:TDButton></td>
                        <td align="right">
                            <cc1:TDButton ID="saveUserPreferencesConfirmButton" runat="server" EnableViewState="False"></cc1:TDButton></td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
</asp:Panel>
