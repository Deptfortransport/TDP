<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="StopInformationPlanJourneyControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.StopInformationPlanJourneyControl" %>

<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<%@ Register TagPrefix="uc1" TagName="AmbiguousDateSelectControl" Src="AmbiguousDateSelectControl.ascx" %>


<div class="roundedcornr_box">
    <div class="roundedcornr_top">
        <div>
            <h2><asp:Label ID="labelPlanAJourneyTitle" runat="server"></asp:Label></h2>
        </div>
    </div>
    <div class="roundedcornr_content">
       <div>
            <table style="width:100%;">
                <tr>
                    <td> 
                        <asp:label ID="labelTravel" runat="server" CssClass="txtsevenb"></asp:label>
                    </td>
                    <td colspan="2"> 
                        <asp:RadioButton id="FromOption" runat="server" GroupName="TravelOption" Checked="true" CssClass="txtsevenb" />
                        &nbsp;&nbsp;
                        &nbsp;&nbsp;
                        <asp:RadioButton id="ToOption" runat="server" GroupName="TravelOption" CssClass="txtsevenb" />
                        &nbsp;&nbsp;
                        &nbsp;&nbsp;
                        <cc1:tdbutton id="okButton" runat="server"></cc1:tdbutton>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:Label ID="labelStopName" runat="server" CssClass="txtsevenb" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="labelFrom" runat="server"  style="display:none;" CssClass="txtsevenb" />
                        <asp:Label ID="labelTo" runat="server" CssClass="txtsevenb" />
                    </td>
                    <td>
                        <asp:TextBox ID="textFromTo" runat="server" />                  
                    </td>
                    <td rowspan="3">
                        <asp:hyperlink id="hyperlinkDoorToDoor" runat="server" enableviewstate="false">
				            <cc1:tdimage id="imageDoorToDoor" runat="server" width="50" height="65"></cc1:tdimage>
			            </asp:hyperlink>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <asp:dropdownlist id="dropDownLocationGazeteerOptions" runat="server" enableviewstate="True"></asp:dropdownlist>              
                    </td>
                   
                </tr>
                <tr>
		            <td>
			            <asp:label id="labelLeave" runat="server" enableviewstate="False" CssClass="txtsevenb" ></asp:label>
		            </td>
		            <td>
			            <uc1:ambiguousdateselectcontrol id="ambiguousDateSelectControl" runat="server" width="100%"
				            shortlayoutmode="true" monthlistresourceid="DateSelectControl.listShortMonths" 
				            FlexibilityControlsVisible="false" TimeControlsVisible="false" >
				        </uc1:ambiguousdateselectcontrol>
		            </td>
	            </tr>
	            <tr>
	                <td>&nbsp;</td>
	                <td colspan="2">
	                    <asp:dropdownlist id="listHours" runat="server">
				                <asp:listitem value="00">00</asp:listitem>
				                <asp:listitem value="01">01</asp:listitem>
				                <asp:listitem value="02">02</asp:listitem>
				                <asp:listitem value="03">03</asp:listitem>
				                <asp:listitem value="04">04</asp:listitem>
				                <asp:listitem value="05">05</asp:listitem>
				                <asp:listitem value="06">06</asp:listitem>
				                <asp:listitem value="07">07</asp:listitem>
				                <asp:listitem value="08">08</asp:listitem>
				                <asp:listitem value="09">09</asp:listitem>
				                <asp:listitem value="10">10</asp:listitem>
				                <asp:listitem value="11">11</asp:listitem>
				                <asp:listitem value="12">12</asp:listitem>
				                <asp:listitem value="13">13</asp:listitem>
				                <asp:listitem value="14">14</asp:listitem>
				                <asp:listitem value="15">15</asp:listitem>
				                <asp:listitem value="16">16</asp:listitem>
				                <asp:listitem value="17">17</asp:listitem>
				                <asp:listitem value="18">18</asp:listitem>
				                <asp:listitem value="19">19</asp:listitem>
				                <asp:listitem value="20">20</asp:listitem>
				                <asp:listitem value="21">21</asp:listitem>
				                <asp:listitem value="22">22</asp:listitem>
				                <asp:listitem value="23">23</asp:listitem>
			                </asp:dropdownlist>
		                &nbsp;
		                <asp:dropdownlist id="listMinutes" runat="server">
			                <asp:listitem value="00">00</asp:listitem>
			                <asp:listitem value="05">05</asp:listitem>
			                <asp:listitem value="10">10</asp:listitem>
			                <asp:listitem value="15">15</asp:listitem>
			                <asp:listitem value="20">20</asp:listitem>
			                <asp:listitem value="25">25</asp:listitem>
			                <asp:listitem value="30">30</asp:listitem>
			                <asp:listitem value="35">35</asp:listitem>
			                <asp:listitem value="40">40</asp:listitem>
			                <asp:listitem value="45">45</asp:listitem>
			                <asp:listitem value="50">50</asp:listitem>
			                <asp:listitem value="55">55</asp:listitem>
		                </asp:dropdownlist>
		                &nbsp;&nbsp;
		                <asp:RadioButton ID="LeaveAfterOption" runat="server" GroupName="ArriveBefore" CssClass="txtsevenb" Checked="true" />
                        &nbsp;&nbsp;
                        <asp:RadioButton ID="ArriveBeforeOption" runat="server" GroupName="ArriveBefore" CssClass="txtsevenb" />
	                </td>
	            </tr>
                <tr class="VertAlignTop">
		            <td>
			            <asp:label id="labelShow" runat="server" enableviewstate="false" CssClass="txtsevenb"></asp:label>
		            </td>
		            <td align="left" class="paddingtop2">
			            <asp:checkbox id="checkBoxPublicTransport" runat="server" enableviewstate="False" checked="true" cssclass="txtsevenaligntop"></asp:checkbox>
			            &nbsp;&nbsp;
			            <asp:checkbox id="checkBoxCarRoute" runat="server" enableviewstate="False" checked="true" cssclass="txtsevenaligntop"></asp:checkbox>
		            </td>
		         
		            <td colspan="3" align="center">
			            <cc1:tdbutton id="buttonSubmit" runat="server"></cc1:tdbutton>
		            </td>
	            </tr>
            </table>
                  
       </div>
    </div>
    <div class="roundedcornr_bottom">
        <div>
        </div>
    </div>
</div>
