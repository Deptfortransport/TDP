<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="StopInformationDepartureBoardControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.StopInformationDepartureBoardControl" %>
<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>

<%@ Register TagPrefix="uc1" TagName="DepartureBoardServiceResult" Src="~/Controls/StopInformationDepartureBoardResultControl.ascx" %>

<div class="roundedcornr_box">
    <div class="roundedcornr_top">
        <div>
            <h2><asp:Label ID="labelDepartureBoardTitle" runat="server"></asp:Label></h2>
        </div>
    </div>
    <div class="roundedcornr_content">
        <div class="siDepartureBoardSubheading">
            <table style="width:100%;">
                <tr>
                    <td>
                        <asp:Label ID="labelDepartureBoardSubTitle" runat="server"></asp:Label>
                    </td>
                    <td align="right" rowspan="2"> 
                        <cc1:tdimage id="imageDepartureBoard" runat="server" Width="50" Height="26"></cc1:tdimage>
                    </td>
                 </tr>
                 <tr>
                    <td>
                        <asp:Label ID="labelDepartureBoardTime" runat="server"></asp:Label>
                    </td>
                 </tr>
            </table>
        </div>
        <div>
            
            <uc1:DepartureBoardServiceResult ID="DepartureBoardResult" 
                runat="server" visible="true" />
                
        </div>
        <div class="siDepartureBoardLinks">
            <table style="width:100%;">
                <tr>
                    <td>
                        <cc1:TDButton ID="MobileServiceLink" runat="server" />
                    </td>
                    <td align="right">
                        <cc1:TDButton ID="ServiceButton" runat="server" />
                    </td>
                </tr>
          
                <tr>
                    <td>
                        <cc1:tdimage ID="NRELogo" runat="server" Width="162" Height="36"></cc1:tdimage>
                    </td>
                    
                    <td>
                        <asp:Label ID="NRELink" runat="server"></asp:Label>
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