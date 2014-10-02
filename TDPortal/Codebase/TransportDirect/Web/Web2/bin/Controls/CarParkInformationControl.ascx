<%@ Control Language="c#" AutoEventWireup="True" Codebehind="CarParkInformationControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.CarParkInformationControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<asp:Panel id="CarParkPanel" runat="server" width="100%">
	<table width="100%" summary="Car Park Information">
		<tr>
			<td colspan="2">
				<div class="hdtypefour">
					<asp:label id="carParkSummaryTitle" runat="server">[car park details title]</asp:label></div>
			</td>
		</tr>
		<tr>
			<td>
				<div id="carparkdetails">
				    <asp:Panel id="addressPanel" runat="server" width="100%" CssClass="carparkInformationAddress">
	    			    <asp:label id="carParkAddressTitle" runat="server" enableviewstate="False" cssclass="txtsevenb">[Address]</asp:label><br />
	    			    <div class="spacer2">&nbsp;</div>
    					<asp:label id="carParkAddress" runat="server" enableviewstate="False" cssclass="txtseven">[address]</asp:label><br />
					    <asp:label id="carParkPostcode" runat="server" enableviewstate="False" cssclass="txtseven">[postcode]</asp:label><br />
					    <asp:label id="carParkTelephone" runat="server" enableviewstate="False" cssclass="txtseven">[telephone]</asp:label><br />
					    <asp:label id="operatorName" runat="server" enableviewstate="False" cssclass="txtseven">[operator name]</asp:label>
					    <div class="spacer2">&nbsp;</div>
					    <asp:hyperlink id="operatorNameHyperlink" runat="server" cssclass="txtseven">[operator hyperlink]</asp:hyperlink><br />
					</asp:Panel>
					<asp:Panel id="minimumCostPanel" runat="server" width="100%" CssClass="carparkInformationMinCost">
					    <div class="spacer3">&nbsp;</div>
					    <asp:label id="carParkMinimumCostTitle" runat="server" enableviewstate="False" cssclass="txtsevenb">[Minimum Cost of Parking:]</asp:label><br />
					    <div class="spacer2">&nbsp;</div>
					    <asp:label id="carParkMinimumCost" runat="server" enableviewstate="False" cssclass="txtseven">[minimum cost]</asp:label><br />
					</asp:Panel>
                    <asp:Panel id="openingTimePanel" runat="server" width="100%" CssClass="carparkInformationTimes">
                        <div class="spacer3">&nbsp;</div>
					    <asp:label id="carParkOpeningTimesTitle" runat="server" enableviewstate="False" cssclass="txtsevenb">[Opening Times:]</asp:label><br />
					    <div class="spacer2">&nbsp;</div>
					    <asp:label id="carParkOpeningTimes" runat="server" enableviewstate="False" cssclass="txtseven">[Opening Times]</asp:label><br />
                    </asp:Panel>
                    <asp:Panel id="numberOfSpacesPanel" runat="server" width="100%" CssClass="carparkInformationSpaces">
                        <div class="spacer3">&nbsp;</div>            					
					    <asp:label id="carParkNumberOfSpacesTitle" runat="server" enableviewstate="False" cssclass="txtsevenb">[Total Number of Spaces:]</asp:label><br />
					    <div class="spacer2">&nbsp;</div>
					    <asp:label id="carParkNumberOfSpaces" runat="server" enableviewstate="False" cssclass="txtseven">[number of spaces]</asp:label><br />
                    </asp:Panel>
                    <asp:Panel id="numberOfDisabledSpacePanel" runat="server" width="100%" CssClass="carparkInformationDisabled">
                        <div class="spacer3">&nbsp;</div>            					                
					    <asp:label id="carParkNumberOfDisabledSpacesTitle" runat="server" enableviewstate="False" cssclass="txtsevenb">[Number of disabled spaces:]</asp:label><br />
					    <div class="spacer2">&nbsp;</div>
					    <asp:label id="carParkNumberOfDisabledSpaces" runat="server" enableviewstate="False" cssclass="txtseven">[number of disabled spaces]</asp:label><br />
                    </asp:Panel>					
                    <asp:Panel id="carParkTypePanel" runat="server" width="100%" CssClass="carparkInformationType">
                        <div class="spacer3">&nbsp;</div>            					                                
					    <asp:label id="carParkTypeTitle" runat="server" enableviewstate="False" cssclass="txtsevenb">[Type of Car Park:]</asp:label><br />
					    <div class="spacer2">&nbsp;</div>
					    <asp:label id="carParkType" runat="server" enableviewstate="False" cssclass="txtseven">[car parking type]</asp:label><br />
                    </asp:Panel>					
                    <asp:Panel id="carParkRestrictionsPanel" runat="server" width="100%" CssClass="carparkInformationRestrictions">
                        <div class="spacer3">&nbsp;</div>            					                                
					    <asp:label id="carParkRestrictionsTitle" runat="server" enableviewstate="False" cssclass="txtsevenb">[Parking Restrictions:]</asp:label><br />
					    <div class="spacer2">&nbsp;</div>
					    <asp:label id="carParkRestrictionsHeight" runat="server" enableviewstate="False" cssclass="txtseven">[maximum height]</asp:label>
					    <asp:label id="carParkRestrictionsWidth" runat="server" enableviewstate="False" cssclass="txtseven">[maximum width]</asp:label>
                    </asp:Panel>										
                    <asp:Panel id="carParkAdvancedReservationsPanel" runat="server" width="100%" CssClass="carparkInformationReservations">
                        <div class="spacer3">&nbsp;</div>
					    <asp:label id="carParkAdvancedReservationsTitle" runat="server" enableviewstate="False" cssclass="txtsevenb">[Advanced Reservations:]</asp:label><br />
					    <div class="spacer2">&nbsp;</div>
					    <asp:label id="carParkAdvancedReservations" runat="server" enableviewstate="False" cssclass="txtseven">[advanced reservations]</asp:label><br />
                    </asp:Panel>															
                    <asp:Panel id="additionalNotesPanel" runat="server" width="100%" CssClass="carparkInformationNotes">
                        <div class="spacer3">&nbsp;</div>
					    <asp:label id="carParkAdditionalNotesTitle" runat="server" enableviewstate="False" cssclass="txtsevenb">[Additional Notes:]</asp:label><br />
					    <div class="spacer2">&nbsp;</div>
						<asp:label id="carParkNotes" runat="server" enableviewstate="False" cssclass="txtseven">[notes]</asp:label><br />
					</asp:Panel>
				</div>
			</td>
			<td valign="top">
			    <!-- Park Mark Logo Here -->
                <asp:Panel id="carParkPMSPAPanel" runat="server" width="100%">                					                                                			    
    			    <asp:image id="carParkPMSPALogo" runat="server" EnableViewState="False" CssClass="txtseven" AlternateText="" ImageAlign="right" ImageUrl=""/>
                </asp:Panel>    			    
			</td>
		</tr>
	</table>
</asp:Panel>
<br />
<asp:Panel id="ParkAndRidePanel" runat="server" width="100%">
	<table width="100%" summary="Park And Ride Information">
		<tr>
			<td>
				<div class="hdtypefour">
					<asp:label id="parkAndRideSummaryTitle" runat="server">[park and ride details title]</asp:label></div>
			</td>
		</tr>
		<tr>
			<td>
				<div id="parkandridedetails" class="carparkInformationParkAndRide">
					<asp:label id="parkAndRideText1" runat="server" enableviewstate="False" cssclass="txtseven">[This car park is part of the]</asp:label>
					<asp:label id="parkAndRideLocation" runat="server" enableviewstate="False" cssclass="txtseven">[scheme location name]</asp:label>
					<asp:label id="parkAndRideText2" runat="server" enableviewstate="False" cssclass="txtseven">[park and ride scheme]</asp:label><br />
					<br />
					<asp:label id="parkAndRideComments" runat="server" enableviewstate="False" cssclass="txtseven">[park and ride comments]</asp:label><br />
				</div>
			</td>
		</tr>
	</table>
</asp:Panel>
<br />
<table summary="Park And Ride Information" width="100%">
	<tr>
		<td>
			<div id="notes">
				<asp:label id="informationNote" runat="server" enableviewstate="False" cssclass="txtseven">[information note (opening time)]</asp:label></div>
		</td>
	</tr>
</table>
