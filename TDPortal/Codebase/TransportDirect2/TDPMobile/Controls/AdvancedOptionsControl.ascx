<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AdvancedOptionsControl.ascx.cs" Inherits="TDP.UserPortal.TDPMobile.Controls.AdvancedOptionsControl" %>
<%@ Register src="~/Controls/AccessibleOptionsControl.ascx" tagname="AccessibleOptionsControl" tagprefix="uc1" %>

<div class="advancedOptions jshide">
    <div class="jssettings">
        <asp:HiddenField ID="transportModesSelected" runat="server" />
        <asp:HiddenField ID="hdnValidationMessage" runat="server" />
    </div>
    <div class="row">
        <div class="screenReaderOnly">
            <asp:Label ID="lblAdvancedOptionsSR" CssClass="advancedOptionsSR" AssociatedControlID="btnAdvancedOptions" runat="server" EnableViewState="false" />
        </div>
        <div class="setAdvancedOptionsBox">
            <asp:Button ID="btnAdvancedOptions" runat="server" CssClass="btnAdvancedOptions jshide" />
            <asp:Label ID="lblAdvancedOptionsSelected" runat="server" CssClass="advancedOptionsSelected" EnableViewState="false"></asp:Label>
        </div>
    
        <div id="advancedoptionspage" data-role="page">
            <div class="advancedoptionspageContent" data-role="content">
                <div id="transportModesDiv" runat="server" class="transportModesDiv">
                    <div class="transportModesHead">
                        <h2 id="hdgTransportModesHead" runat="server" enableviewstate="false"></h2>
                    </div>
                    <asp:Repeater ID="rptTransportModes" runat="server" OnItemDataBound="TransportModes_DataBound">
                        <ItemTemplate>
                            <div class="transportModeRow">
                                <div class="transportModeLabelDiv">
                                    <asp:Label ID="lblTransportMode" runat="server" AssociatedControlID="chkTransportMode"></asp:Label>
                                    <div class="transportModeImageDiv">
                                        <asp:Image ID="imgTransportMode" runat="server" />
                                    </div>
                                </div>
                                <div class="transportModeCheckDiv">
                                    <asp:CheckBox ID="chkTransportMode" runat="server" />
                                </div>
                                <asp:HiddenField ID="hdnTransportMode" runat="server" />
                            </div>                            
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
                <div class="clear"></div>
                <div id="accessibleOptionsDiv" runat="server" class="accessibilityOptionsRow">
                    <uc1:AccessibleOptionsControl ID="accessibleOptionsControl" runat="server" />
                </div>
                <div class="clear"></div>
                <div class="advancedOptionsConfirm">
                    <asp:Button ID="btnOKAdvancedOptions" runat="server" />
                </div>
                <div class="clear"></div>
            </div>
            <asp:HiddenField ID="hdnPageBackText" runat="server" EnableViewState="false" />
        </div>

        <div id="additionalInfoPage" data-role="page" style="display: none;">
            <div id="additionaInfoDialog">
                <div class="headerBack" data-role="header" data-add-back-btn="true" data-position="fixed">
                    <h2 id="additionalInfoHeader" runat="server" enableviewstate="false"></h2>
                    <asp:LinkButton ID="closeinfodialog" runat="server" CssClass="additionalNotesClose" data-role="button" data-icon="delete"></asp:LinkButton>
                </div>
                <div id="additionalInfoNotes" data-role="content" class="ui-content">
                </div>
            </div>
        </div>

    </div>
</div>