<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AccessibleOptionsControl.ascx.cs" Inherits="TDP.UserPortal.TDPMobile.Controls.AccessibleOptionsControl" %>

<div id="accessibilityOptions" runat="server" class="accessibilityOptions">
    <div class="jssettings">
        <asp:HiddenField ID="mobilityOptionsSelected" runat="server" />
    </div>
    <div class="mobilityOptionsHead">
        <h2 id="hdgMobilityOptionsHead" runat="server" enableviewstate="false"></h2>
    </div>
    <div class="mobilityOptionRow">
        <div class="mobilityOptionLabelDiv">
            <asp:Label ID="lblStepFree" runat="server" AssociatedControlID="chkStepFree"></asp:Label>
            <div class="accessibleNotesLinkDiv">
                <a id="accessibleNotesLinkSF" runat="server" class="accessibleNotesLink" href="#" aria-haspopup="true" />
            </div>        
        </div>
        <div class="mobilityOptionCheckDiv">
            <asp:CheckBox ID="chkStepFree" runat="server" />
        </div>
        <div class="detailNote hide jsshow">
            <asp:Label ID="lblStepFreeInfo" runat="server"></asp:Label>
        </div>
    </div>
    <div class="mobilityOptionRow">
        <div class="mobilityOptionLabelDiv">
            <asp:Label ID="lblAssistance" runat="server" AssociatedControlID="chkAssistance"></asp:Label>
            <div class="accessibleNotesLinkDiv">
                <a id="accessibleNotesLinkAss" runat="server" class="accessibleNotesLink" href="#" aria-haspopup="true" />
            </div>
        </div>
        <div class="mobilityOptionCheckDiv">
            <asp:CheckBox ID="chkAssistance" runat="server" />
        </div>
        <div class="detailNote hide jsshow">
            <asp:Label ID="lblAssistanceInfo" runat="server"></asp:Label>
        </div>
    </div>
    <div class="mobilityOptionRow">
        <div class="mobilityOptionLabelDiv">
            <asp:Label ID="lblFewestChanges" runat="server" AssociatedControlID="chkFewestChanges"></asp:Label>
        </div>
        <div class="mobilityOptionCheckDiv">
            <asp:CheckBox ID="chkFewestChanges" runat="server" />
        </div>
    </div>
</div>