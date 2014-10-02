<%@ Control Language="c#" AutoEventWireup="True" Codebehind="AccessibleOptionsControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.AccessibleOptionsControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" EnableViewState="true" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<%@ Register TagPrefix="uc1" TagName="TravelDetailsControl" Src="TravelDetailsControl.ascx" %>

<asp:Panel ID="pnlAccessibleOptions" runat="server"  >
    <div class="boxtypetwo boxOptions txtseven" >
        <div class="optionHeadingRow" aria-controls="accessibleOptionsControl_optionContentRow" role="button">
            <div class="boxArrowToggle">
            </div>
            <div class="boxOptionHeading">
                <asp:Label ID="labelJsQuestion" runat="server" />
            </div>
            <div class="boxOptionSelected">
                <asp:Label ID="labelOptionsSelected" runat="server" CssClass="labelJsRed hide" />
                <noscript>
                    <cc1:tdbutton id="btnShow" runat="server" OnClick="btnShow_Click"></cc1:tdbutton>
                </noscript>
            </div>
        </div>
        <div id="optionContentRow" runat="server" class="optionContentRow accessibleBox hide" aria-labelledby="accessibleOptionsControl_labelJsQuestion" aria-expanded="false" role="region">
            <table>
                <tr>
                    <td width="7px"></td>
                    <td colspan="2">
                        <strong>
                            <asp:Label ID="labelTitle" runat="server" />
                        </strong>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:RadioButton ID="stepFree" runat="server" GroupName="accessiblityOptions" />
                    </td>
                    <td width="250px" class="accessibleInfo">
                        <a id="stepFreeAnchor" class="tooltip_information" href="#" onclick="return false;" runat="server" enableviewstate="false">
                            <asp:Image ID="imgStepFree" CssClass="information" runat="server" EnableViewState="false" />
                        </a>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:RadioButton ID="stepFreeAndAssistance" runat="server" GroupName="accessiblityOptions" />
                    </td>
                    <td width="250px" class="accessibleInfo">
                        <a id="stepFreeAndAssistanceAnchor" class="tooltip_information" href="#" onclick="return false;" runat="server" enableviewstate="false">
                            <asp:Image ID="imgStepFreeAndAssistance" CssClass="information" runat="server" EnableViewState="false" />
                        </a>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:RadioButton ID="assistance" runat="server" GroupName="accessiblityOptions" />
                    </td>
                    <td width="250px" class="accessibleInfo">
                        <a id="assistanceAnchor" class="tooltip_information" href="#" onclick="return false;" runat="server" enableviewstate="false">
                            <asp:Image ID="imgAssistance" CssClass="information" runat="server" EnableViewState="false" />
                        </a>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:RadioButton ID="noRequirement" runat="server" GroupName="accessiblityOptions" />
                    </td>
                    <td width="250px">
                        <a id="noRequirementAnchor" class="tooltip_information" href="#" onclick="return false;" runat="server" enableviewstate="false">
                            <asp:Image ID="imgNoRequirement" CssClass="information" runat="server" EnableViewState="false" />
                        </a>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td>
                        <asp:CheckBox ID="checkFewestChanges" runat="server" />
                    </td>
                    <td>
                        <a id="checkFewestChangesAnchor" class="tooltip_information" href="#" onclick="return false;" runat="server" enableviewstate="false">
                            <asp:Image ID="imgCheckFewestChanges" CssClass="information" runat="server" EnableViewState="false" />
                        </a>
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td colspan="2">
                        &nbsp;<a id="accessibleFaq" runat="server" enableviewstate="false" />
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td colspan="2">
                        <span class="jptnew">
	                        &nbsp;<uc1:traveldetailscontrol id="loginSaveOption" runat="server"></uc1:traveldetailscontrol>
	                    </span>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Panel>

<asp:Panel id="pnlAccessibleOptionsReadonly" runat="server">
    <div class="boxtypetwo">
        <asp:Label id="lblAccessibleOptionReadOnly" runat="server" CssClass="txtsevenb" EnableViewState="false"></asp:Label>
        <br />
	    <asp:Label ID="lblAccessibleOptionSelected" runat="server" CssClass="txtsevenb" EnableViewState="false"></asp:Label>
	    <br />
	</div>
</asp:Panel>