<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LocationControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.LocationControl" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>

<div class="locationControl">
    <div class="jssettings">
        <asp:HiddenField ID="scriptId" runat="server" />
        <asp:HiddenField ID="scriptPath" runat="server" />
        <asp:HiddenField ID="jsEnabled" runat="server" Value="" />
        <asp:HiddenField ID="locationSuggestDisabled" runat="server" Value="" />
        <asp:HiddenField ID="moreSelected" runat="server" Value="" />
    </div>
    
    <asp:HiddenField ID="locationInput_hdnValue"  runat="server" Value="" />
    <asp:HiddenField ID="locationInput_hdnType"  runat="server" Value="" />
    
    <div id="ambiguityInputRow" runat="server" class="ambiguityInputRow" visible="false">
        <asp:Label ID="ambiguityInputText" runat="server" CssClass="txtseven ambiguityinfo" />
    </div>
    
    <div class="locationInputRow">
        <asp:Label ID="locationInputDescription" runat="server" CssClass="screenreader" AssociatedControlID="locationInput" />
        <asp:TextBox ID="locationInput" runat="server" columns="48" CssClass="locationBox" ></asp:TextBox>
    </div>
    
    <div id="unsureSpellingRow" runat="server" class="unsureSpelling">
        <asp:CheckBox id="unsureSpellingCheck" runat="server" CssClass="txtseven" ></asp:CheckBox>
    </div>
    
    <div class="locationOptionsBtnRow">
        <cc1:TDButton ID="moreOptionsBtn" runat="server" OnClick="moreOptionsBtn_Click" CssClass="TDButtonDefault moreOptions hide" CssClassMouseOver="TDButtonDefaultMouseOver moreOptions hide" />
        <cc1:TDButton ID="findOnMapBtn" runat="server" OnClick="findOnMapBtn_Click" CssClass="TDButtonDefault findOnMap hide" CssClassMouseOver="TDButtonDefaultMouseOver findOnMap hide" />
    </div>
    
    <div id="ambiguityRow" runat="server" class="ambiguityRow" visible="false">
        <asp:Label ID="ambiguityText" runat="server" CssClass="txtseven ambiguityinfo" AssociatedControlID="ambiguityDrop" />
        <asp:DropDownList ID="ambiguityDrop" runat="server" CssClass="ambiguitydrop" ></asp:DropDownList>
        <cc1:TDButton ID="ambiguityResetBtn" runat="server" OnClick="resetBtn_Click" CssClass="TDButtonDefault locationReset" CssClassMouseOver="TDButtonDefaultMouseOver locationReset" />
        <div id="ambiguityTypeDropRow" runat="server" class="ambiguityTypeDropDiv" visible="false">
	        <asp:Label ID="ambiguityTypeText" runat="server" CssClass="txtseven ambiguityTypeText"></asp:Label>	    
            <asp:DropDownList ID="ambiguityTypeDrop" runat="server" CssClass="ambiguityTypeDrop"></asp:DropDownList>
        </div>
    </div>
    
    <div id="locationTypesRow" runat="server" class="locationTypesRow">
        <asp:Label ID="locationTypeDescription" runat="server" CssClass="screenreader" ></asp:Label>
        <fieldset ID="locationTypes" runat="server">
		    <asp:RadioButtonList ID="locationTypeRadio" runat="server" CssClass="txtseven radioLocationType" RepeatDirection="Horizontal" RepeatColumns="3"></asp:RadioButtonList>
        </fieldset>
        <% //Only displayed on homepage "Plan a journey" control, and is currently only required for non-js users %>
        <noscript>
            <div class="locationTypeDropDiv">
                <asp:DropDownList ID="locationTypeDrop" runat="server" CssClass="locationTypeDrop"></asp:DropDownList>
            </div>
        </noscript>
	</div>
	
	<div id="fixedLocationRow" runat="server" class="fixedLocationRow" visible="false">
	    <cc1:TDButton ID="newLocationBtn" runat="server" OnClick="newLocationBtn_Click" CssClass="TDButtonDefault locationNew" CssClassMouseOver="TDButtonDefaultMouseOver locationNew" />	    
	    <asp:Label ID="fixedLocationDescription" runat="server" CssClass="txtseven fixedLocationDescription"></asp:Label>
	    <asp:Label ID="fixedLocation" runat="server" CssClass="txtseven fixedLocation"></asp:Label>	    
	</div>
</div>