<%@ Register TagPrefix="uc1" TagName="FindPreferencesOptionsControl" Src="FindPreferencesOptionsControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FindPageOptionsControl" Src="FindPageOptionsControl.ascx" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="FindFarePreferenceControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.FindFarePreferenceControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" enableViewState="True"%>
<%@ Register TagPrefix="uc1" TagName="TravelDetailsControl" Src="TravelDetailsControl.ascx" %>
<asp:panel id="preferencesPanel" runat="server" Visible="false">
	<uc1:findpreferencesoptionscontrol id="preferencesOptionsControl" runat="server"></uc1:findpreferencesoptionscontrol>
	<div class="boxtypetwo">
		<table cellspacing="0" cellpadding="0" width="100%">
			<tr>
				<td> 
					<table cellpadding="0" cellspacing="0" width="100%">
						<tr>
							<td width="500px">
								<table cellpadding="0" cellspacing="0" width="100%">
									<tr>
										<td class="txtseven">
											<uc1:traveldetailscontrol id="travelDetailsControl" runat="server" visible="false"></uc1:traveldetailscontrol>
										</td>
									</tr>
								</table>
								<asp:label id="fareDetailsLabel" runat="server" cssclass="txtnineb" enableviewstate="False">[fareDetailsLabel]</asp:label><br />
								<asp:table id="DiscountCardsTable" runat="server">
									<asp:tablerow id="DiscountCardsRow" width="210px" verticalalign="Top">
										<asp:tablecell>
											<asp:label id="discountCardsLabel" runat="server" cssclass="txtsevenb" enableviewstate="False">[discountCardsLabel]</asp:label>
										</asp:tablecell>
									</asp:tablerow>
									<asp:tablerow id="RailCardRow" VerticalAlign="Top">
										<asp:tablecell width="210px">
											<asp:label id="railCardLabel" runat="server" cssclass="txtseven" enableviewstate="False">[railCardLabel]</asp:label>
										</asp:tablecell>
										<asp:tablecell>
											<asp:label id="selectedRailCardLabel" runat="server" cssclass="txtsevenb" enableviewstate="False">[selectedRailCardLabel]</asp:label>
											<asp:label id="railCardSRLabel" runat="server" cssclass="screenreader" enableviewstate="False">[railCardSR]</asp:label>
											<asp:dropdownlist id="railCardDropList" runat="server" enableviewstate="True"></asp:dropdownlist>
										</asp:tablecell>
									</asp:tablerow>
									<asp:tablerow id="CoachCardRow" verticalalign="Top">
										<asp:tablecell width="210px">
											<asp:label id="coachCardLabel" runat="server" cssclass="txtseven" enableviewstate="False">[coachCardLabel]</asp:label>
										</asp:tablecell>
										<asp:tablecell>
											<asp:label id="selectedCoachCardLabel" runat="server" cssclass="txtsevenb" enableviewstate="False">[coachCardLabel]</asp:label>
											<asp:label id="coachCardSRLabel" runat="server" cssclass="screenreader" enableviewstate="False">[coachCardSR]</asp:label>
											<asp:dropdownlist id="coachCardDropList" runat="server" enableviewstate="True"></asp:dropdownlist>
										</asp:tablecell>
									</asp:tablerow>
								</asp:table>
								<asp:panel id="adultChildPanel" runat="server" width="300">
									<asp:label id="adultChildLabel" runat="server" cssclass="txtsevenb" enableviewstate="False">[adultChildLabel]</asp:label>
									<br />
									<asp:label id="showLabel" runat="server" cssclass="txtseven" enableviewstate="False">[showLabel]</asp:label>
									<asp:label id="adultChildSRLabel" enableviewstate="False" runat="server" cssclass="screenreader">[adultChildSRLabel]</asp:label>
									<asp:label id="selectedAdultChildLabel" runat="server" cssclass="txtsevenb" enableviewstate="False">[selectedAdultChildLabel]</asp:label>
									<asp:radiobuttonlist id="adultChildRadioList" enableviewstate="True" runat="server" cssclass="txtseven" repeatdirection="Horizontal" repeatlayout="Flow"></asp:radiobuttonlist>
								</asp:panel>
							</td> 
						</tr>
					</table> <!-- Pref. container table -->
			</td>
		</tr>
		</table>
		<uc1:findpageoptionscontrol id="pageOptionsControl" runat="server"></uc1:findpageoptionscontrol>
	</div>
</asp:panel>
<p></p>
