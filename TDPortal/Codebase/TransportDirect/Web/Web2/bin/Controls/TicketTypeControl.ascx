<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TicketTypeControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.TicketTypeControl" %>
	<table id="TicketTypeTable" runat="server" width="100%"  summary="Ticket Type Information" class="ticketTypeTable" cellpadding="4" cellspacing="0">
		<tr>
			<td colspan="2" style="height: 21px; padding:0; border:0">
							<div id="hdtypethree">
					<asp:label id="ticketTypeTitle" CssClass="TitleText" Text="Rail Ticket" runat="server">[Rail Ticket Type Title]</asp:label></div>
			</td>
		</tr>
		<tr id="ticketTypeDescriptionRow" runat="server">
			<td colspan="1" class="ticketTypeTableDescCol">
				    <asp:Panel id="descriptionNamePanel" runat="server" width="100%" cssclass="BodyText">
	    			    <asp:label id="descriptionTitle" runat="server" enableviewstate="False" >[Description]</asp:label>
					</asp:Panel>
					</td>
			<td colspan="1">
				    <asp:Panel id="descriptionDescPanel" runat="server" width="100%" cssclass="DescriptionText">
	    			    <asp:label id="descriptionDesc" runat="server" enableviewstate="False" ></asp:label>
					</asp:Panel>
			</td>
		</tr>
				<tr id="ticketTypeValidityRow" runat="server">
						<td colspan="1" class="ticketTypeTableDescCol" >		
					<asp:Panel id="validtyNamePanel" runat="server" width="100%" cssclass="BodyText" >
					   
					    <asp:label id="validityTitle" runat="server" enableviewstate="False" >[Validity]</asp:label>
					     </asp:Panel>
					</td>
					
					<td colspan="1">
					<asp:Panel id="validityDescPanel" runat="server" width="100%" cssclass="DescriptionText">
					<asp:label id="validityDesc" runat="server" enableviewstate="False" ></asp:label>
					    
					</asp:Panel>
					</td>
					</tr>
					<tr id="ticketTypeSleeperRow" runat="server">
					<td colspan="1" class="ticketTypeTableDescCol" >		
                    <asp:Panel id="sleepersNamePanel" runat="server" width="100%"  cssclass="BodyText">
                        
					    <asp:label id="sleepersTitle" runat="server" enableviewstate="False">[Sleepers:]</asp:label></asp:Panel>
                    </td>
                    
                    <td colspan="1">
                    <asp:Panel id="sleeperDescPanel" runat="server" width="100%" cssclass="DescriptionText">
                        
					    <asp:label id="sleeperDesc" runat="server" enableviewstate="False" >[Opening Times:]</asp:label></asp:Panel>
                    </td>
                    	</tr>
                    	
                  <tr id="ticketTypeDiscountRow" runat="server">
                    <td colspan="1" class="ticketTypeTableDescCol">
                    <asp:Panel id="discountsNamePanel" runat="server" width="100%" cssclass="BodyText">
                                    					
					    <asp:label id="discountTitle" runat="server" enableviewstate="False">[Discount:]</asp:label>
					    	
                    </asp:Panel>
                    </td>
                    
                     <td colspan="1">
                    <asp:Panel id="discountDescPanel" runat="server" width="100%" cssclass="DescriptionText">
                                    					
					    <asp:label id="discountDesc" runat="server" enableviewstate="False" >[Total Number of Spaces:]</asp:label></asp:Panel>
                    </td>
                    	</tr>
                    	
                    	<tr id="ticketTypeAvailabilityRow" runat="server">
                    <td colspan="1"  class="ticketTypeTableDescCol" >
                    <asp:Panel id="availabilityNamePanel" runat="server" width="100%" cssclass="BodyText">
                                    					                
					    <asp:label id="availabilityTitle" runat="server" enableviewstate="False" >[Availability:]</asp:label>
					    	
                    </asp:Panel>	
                    </td>	
                    
                     <td colspan="1">
                    <asp:Panel id="availabilityDescPanel" runat="server" width="100%" cssclass="DescriptionText">
                                    					                
					    <asp:label id="availabilityDesc" runat="server" enableviewstate="False" >[Number of disabled spaces:]</asp:label><br />
					    
                    </asp:Panel>	
                    </td>
                    	</tr>
                    	
                    	<tr id="ticketTypeBookingDeadlineRow" runat="server">
                    <td colspan="1" class="ticketTypeTableDescCol" >			
                    <asp:Panel id="bookingDeadlinesNamePanel" runat="server" width="100%" cssclass="BodyText">
                                    					                                
					    <asp:label id="bookingDeadlinesTitle" runat="server" enableviewstate="False" >[Booking Deadlines:]</asp:label>
					    	
                    </asp:Panel>
                    </td>	
                    
                    	<td colspan="1">			
                    <asp:Panel id="bookingDeadlinesDescPanel" runat="server" width="100%" cssclass="DescriptionText">
                                    					                                
					    <asp:label id="bookingDeadlineDesc" runat="server" enableviewstate="False" >[Type of Car Park:]</asp:label><br />
					    
                    </asp:Panel>	
                    </td>
                    
                    	</tr>
                    	
                    	<tr id="ticketTypeRefundsRow" runat="server">
                    <td colspan="1" class="ticketTypeTableDescCol" >				
                    <asp:Panel id="refundsNamePanel" runat="server" width="100%" cssclass="BodyText" >
                                    					                                
					    <asp:label id="refundsTitle" runat="server" enableviewstate="False" >[Refunds:]</asp:label>
					         </asp:Panel>
			</td>
			
			                    <td colspan="1">				
                    <asp:Panel id="refundsDescPanel" runat="server" width="100%" cssclass="DescriptionText">
                                    					                                
					    <asp:label id="refundsDesc" runat="server" enableviewstate="False" ></asp:label>&nbsp;
                    </asp:Panel>
                    </td>
			</tr>
			
                    	
                    	<tr id="ticketTypeBreakofJourneyRow" runat="server">
                    <td colspan="1" class="ticketTypeTableDescCol" >				
                    <asp:Panel id="Panel1" runat="server" width="100%" cssclass="BodyText" >
                                    					                                
					    <asp:label id="breakOfJourneyTitle" runat="server" enableviewstate="False" >[Break of Journey:]</asp:label>
					         </asp:Panel>
			</td>
			
			                    <td colspan="1">				
                    <asp:Panel id="Panel3" runat="server" width="100%" cssclass="DescriptionText">
                                    					                                
					    <asp:label id="breakOfJourneyDesc" runat="server" enableviewstate="False" ></asp:label>&nbsp;
                    </asp:Panel>
                    </td>
			</tr>
			
			
			
			<tr id="ticketTypePackagesRow" runat="server">
                    <td colspan="1" class="ticketTypeTableDescCol" >				
                    <asp:Panel id="Panel2" runat="server" width="100%" cssclass="BodyText" >
                                    					                                
					    <asp:label id="PackagesTitle" runat="server" enableviewstate="False" >[Packages:]</asp:label>
					         </asp:Panel>
			</td>
			
			                    <td colspan="1">				
                    <asp:Panel id="Panel4" runat="server" width="100%" cssclass="DescriptionText">
                                    					                                
					    <asp:label id="PackagesDesc" runat="server" enableviewstate="False" ></asp:label>&nbsp;
                    </asp:Panel>
                    </td>
			</tr>
			
			
					
			<tr id="ticketTypeConditionsRow" runat="server">
                    <td colspan="1" class="ticketTypeTableDescCol" >				
                    <asp:Panel id="Panel5" runat="server" width="100%" cssclass="BodyText" >
                                    					                                
					    <asp:label id="ConditionsTitle" runat="server" enableviewstate="False" >[Conditions:]</asp:label>
					         </asp:Panel>
			</td>
			
			                    <td colspan="1">				
                    <asp:Panel id="Panel6" runat="server" width="100%" cssclass="DescriptionText">
                                    					                                
					    <asp:label id="ConditionsDesc" runat="server" enableviewstate="False" ></asp:label>&nbsp;
                    </asp:Panel>
                    </td>
			</tr>
			
			
						<tr id="ticketTypeRetailingRow" runat="server">
                    <td colspan="1" class="ticketTypeTableDescCol" >				
                    <asp:Panel id="Panel7" runat="server" width="100%" cssclass="BodyText" >
                                    					                                
					    <asp:label id="RetailingTitle" runat="server" enableviewstate="False" >[Conditions:]</asp:label>
					         </asp:Panel>
			</td>
			
			                    <td colspan="1">				
                    <asp:Panel id="Panel8" runat="server" width="100%" cssclass="DescriptionText">
                                    					                                
					    <asp:label id="RetailingDesc" runat="server" enableviewstate="False" ></asp:label>&nbsp;
                    </asp:Panel>
                    </td>
			</tr>
			
									<tr id="ticketTypeChangeToTravelPlansRow" runat="server">
                    <td colspan="1" class="ticketTypeTableDescCol" >				
                    <asp:Panel id="Panel9" runat="server" width="100%" cssclass="BodyText" >
                                    					                                
					    <asp:label id="ChangeToTravelPlansTitle" runat="server" enableviewstate="False" >[Change To Travel Plans:]</asp:label>
					         </asp:Panel>
			</td>
			
			                    <td colspan="1">				
                    <asp:Panel id="Panel10" runat="server" width="100%" cssclass="DescriptionText">
                                    					                                
					    <asp:label id="ChangeToTravelPlansDesc" runat="server" enableviewstate="False" ></asp:label>&nbsp;
                    </asp:Panel>
                    </td>
			</tr>
			
												<tr id="ticketTypeInternetOnlyRow" runat="server">
                    <td colspan="1" class="ticketTypeTableDescCol" >				
                    <asp:Panel id="Panel11" runat="server" width="100%" cssclass="BodyText" >
                                    					                                
					    <asp:label id="InternetOnlyTitle" runat="server" enableviewstate="False" >[Change To Travel Plans:]</asp:label>
					         </asp:Panel>
			</td>
			
			                    <td colspan="1">				
                    <asp:Panel id="Panel12" runat="server" width="100%" cssclass="DescriptionText">
                                    					                                
					    <asp:label id="InternetOnlyDesc" runat="server" enableviewstate="False" ></asp:label>&nbsp;
                    </asp:Panel>
                    </td>
			</tr>
			
			
	</table>
	<asp:label id="ticketNotFound" runat="server"></asp:label>
	
<br />
<br />

