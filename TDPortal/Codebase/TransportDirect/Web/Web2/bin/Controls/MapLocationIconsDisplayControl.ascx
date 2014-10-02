<%@ Control Language="c#" AutoEventWireup="True" Codebehind="MapLocationIconsDisplayControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.MapLocationIconsDisplayControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<div class="msMapSymbolPrintContainer floatleftonly">
    <div class="msMapSymbolPrintHeading">
        <asp:label id="labelLocationTitle" runat="server" CssClass="txteightb"></asp:label>
    </div>
    <div class="msPanelMapSymbolsPrint">
        <div id="divTransport" runat="server" class="floatleftonly">
            <!-- Transport icons-->
            <div class="msMapSymbolsPrintTableTransport">
	            <asp:table id="tableTransport" runat="server" cellSpacing="0">
		            <asp:TableRow>
			            <asp:TableCell ColumnSpan="2">
				            <asp:label style="font-weight:Bold" ID="labelTransportTitle" runat="server" CssClass="txtsevenb"></asp:label>
			            </asp:TableCell>
		            </asp:TableRow>
		            <asp:TableRow id="tableRow2" runat="server" visible="False">
			            <asp:TableCell VerticalAlign="Top">
				            <asp:label id="RLY" runat="server" CssClass="txtseven"></asp:label>
			            </asp:TableCell>
			            <asp:TableCell HorizontalAlign="right">
				            <asp:image id="Image2" runat="server"></asp:image>
			            </asp:TableCell>
		            </asp:TableRow>
		            <asp:TableRow id="tableRow28" runat="server" visible="False">
			            <asp:TableCell VerticalAlign="Top">
				            <asp:label id="MET" runat="server" CssClass="txtseven"></asp:label>
			            </asp:TableCell>
			            <asp:TableCell HorizontalAlign="Right">
				            <asp:image id="Image28" runat="server"></asp:image>
			            </asp:TableCell>
		            </asp:TableRow>
		            <asp:TableRow id="tableRow1" runat="server" visible="False">
			            <asp:TableCell VerticalAlign="Top">
				            <asp:label id="BCX" runat="server" CssClass="txtseven"></asp:label>
			            </asp:TableCell>
			            <asp:TableCell HorizontalAlign="Right">
				            <asp:image id="Image1" runat="server"></asp:image>
			            </asp:TableCell>
		            </asp:TableRow>
		            <asp:TableRow id="tableRow3" runat="server" visible="False">
			            <asp:TableCell VerticalAlign="Top">
				            <asp:label id="AIR" runat="server" CssClass="txtseven"></asp:label>
			            </asp:TableCell>
			            <asp:TableCell HorizontalAlign="right">
				            <asp:image id="Image3" runat="server"></asp:image>
			            </asp:TableCell>
		            </asp:TableRow>
		            <asp:TableRow id="tableRow29" runat="server" visible="False">
			            <asp:TableCell VerticalAlign="Top">
				            <asp:label id="FER" runat="server" CssClass="txtseven"></asp:label>
			            </asp:TableCell>
			            <asp:TableCell HorizontalAlign="right">
				            <asp:image id="Image29" runat="server"></asp:image>
			            </asp:TableCell>
		            </asp:TableRow>
		            <asp:TableRow id="tableRow4" runat="server" visible="False">
			            <asp:TableCell VerticalAlign="Top">
				            <asp:label id="TXR" runat="server" CssClass="txtseven"></asp:label>
			            </asp:TableCell>
			            <asp:TableCell HorizontalAlign="right">
				            <asp:image id="Image4" runat="server"></asp:image>
			            </asp:TableCell>
		            </asp:TableRow>
		            <asp:TableRow id="tableRow31" runat="server" visible="False">
			            <asp:TableCell VerticalAlign="Top">
				            <asp:label id="CPK" runat="server" CssClass="txtseven"></asp:label>
			            </asp:TableCell>
			            <asp:TableCell HorizontalAlign="right">
				            <asp:image id="Image31" runat="server"></asp:image>
			            </asp:TableCell>
		            </asp:TableRow>
	            </asp:table>
	        </div>
	    </div>
	    <div id="divAccommodation" runat="server" class="floatleftonly">
	        <div class="msMapSymbolsPrintTableAccomodation">
	            <!-- Accomodation icons-->
	            <asp:table id="tableAccommodation" runat="server" cellSpacing="0" visible="False">
		            <asp:TableRow>
			            <asp:TableCell ColumnSpan="2">
				            <asp:label style="font-weight:Bold" id="labelAccommodationTitle" runat="server" CssClass="txtsevenb"></asp:label>
			            </asp:TableCell>
		            </asp:TableRow>
		            <asp:TableRow id="tableRow5" runat="server" visible="False">
			            <asp:TableCell VerticalAlign="Top">
				            <asp:label id="ETDR" runat="server" CssClass="txtseven"></asp:label>
			            </asp:TableCell>
			            <asp:TableCell HorizontalAlign="right">
				            <asp:image id="Image5" runat="server"></asp:image>
			            </asp:TableCell>
		            </asp:TableRow>
		            <asp:TableRow id="tableRow6" runat="server" visible="False">
			            <asp:TableCell VerticalAlign="Top">
				            <asp:label id="HTGH" runat="server" CssClass="txtseven"></asp:label>
			            </asp:TableCell>
			            <asp:TableCell HorizontalAlign="right">
				            <asp:image id="Image6" runat="server"></asp:image>
			            </asp:TableCell>
		            </asp:TableRow>
		            <asp:TableRow id="tableRow7" runat="server" visible="False">
			            <asp:TableCell VerticalAlign="Top">
				            <asp:label id="CCMH" runat="server" CssClass="txtseven"></asp:label>
			            </asp:TableCell>
			            <asp:TableCell HorizontalAlign="right">
				            <asp:image id="Image7" runat="server"></asp:image>
			            </asp:TableCell>
		            </asp:TableRow>
		            <asp:TableRow id="tableRow8" runat="server" visible="False">
			            <asp:TableCell VerticalAlign="Top">
				            <asp:label id="YHST" runat="server" CssClass="txtseven"></asp:label>
			            </asp:TableCell>
			            <asp:TableCell HorizontalAlign="right">
				            <asp:image id="Image8" runat="server"></asp:image>
			            </asp:TableCell>
		            </asp:TableRow>
	            </asp:table>
            </div>
        </div>
	    <div id="divSport" runat="server" class="floatleftonly">
	        <div class="msMapSymbolsPrintTableSport">
	            <!-- Sport icons-->
	            <asp:table id="tableSport" runat="server" cellSpacing="0" visible="False">
		            <asp:TableRow>
			            <asp:TableCell ColumnSpan="2">
				            <asp:label style="font-weight:Bold" id="labelSportTitle" runat="server" CssClass="txtsevenb"></asp:label>
			            </asp:TableCell>
		            </asp:TableRow>
		            <asp:TableRow id="tableRow9" runat="server" visible="False">
			            <asp:TableCell VerticalAlign="Top">
				            <asp:label id="ODPT" runat="server" CssClass="txtseven"></asp:label>
			            </asp:TableCell>
			            <asp:TableCell HorizontalAlign="Right">
				            <asp:image id="Image9" runat="server"></asp:image>
			            </asp:TableCell>
		            </asp:TableRow>
		            <asp:TableRow id="tableRow10" runat="server" visible="False">
			            <asp:TableCell VerticalAlign="Top">
				            <asp:label id="SPCM" runat="server" CssClass="txtseven"></asp:label>
			            </asp:TableCell>
			            <asp:TableCell HorizontalAlign="Right">
				            <asp:image id="Image10" runat="server"></asp:image>
			            </asp:TableCell>
		            </asp:TableRow>
		            <asp:TableRow id="tableRow11" runat="server" visible="False">
			            <asp:TableCell VerticalAlign="Top">
				            <asp:label id="VSSC" runat="server" CssClass="txtseven"></asp:label>
			            </asp:TableCell>
			            <asp:TableCell HorizontalAlign="Right">
				            <asp:image id="Image11" runat="server"></asp:image>
			            </asp:TableCell>
		            </asp:TableRow>
		            <asp:TableRow id="tableRow30" runat="server" visible="False">
			            <asp:TableCell VerticalAlign="Top">
				            <asp:label id="RETL" runat="server" CssClass="txtseven"></asp:label>
			            </asp:TableCell>
			            <asp:TableCell HorizontalAlign="Right">
				            <asp:image id="Image30" runat="server"></asp:image>
			            </asp:TableCell>
		            </asp:TableRow>
	            </asp:table>
            </div>
        </div>
	    <div id="divAttractions" runat="server" class="floatleftonly">
	        <div class="msMapSymbolsPrintTableAttractions">
	            <!-- Attractions icons-->
	            <asp:table id="tableAttractions" runat="server" cellSpacing="0" visible="False">
		            <asp:TableRow>
			            <asp:TableCell ColumnSpan="2">
				            <asp:label style="font-weight:Bold" id="labelAttractionsTitle" runat="server" CssClass="txtsevenb"></asp:label>
			            </asp:TableCell>
		            </asp:TableRow>
		            <asp:TableRow id="tableRow12" runat="server" visible="False">
			            <asp:TableCell VerticalAlign="Top">
				            <asp:label id="BTZL" runat="server" CssClass="txtseven"></asp:label>
			            </asp:TableCell>
			            <asp:TableCell HorizontalAlign="right">
				            <asp:image id="Image12" runat="server"></asp:image>
			            </asp:TableCell>
		            </asp:TableRow>
		            <asp:TableRow id="tableRow13" runat="server" visible="False">
			            <asp:TableCell VerticalAlign="Top">
				            <asp:label id="HSTC" runat="server" CssClass="txtseven"></asp:label>
			            </asp:TableCell>
			            <asp:TableCell HorizontalAlign="right">
				            <asp:image id="Image13" runat="server"></asp:image>
			            </asp:TableCell>
		            </asp:TableRow>
		            <asp:TableRow id="tableRow14" runat="server" visible="False">
			            <asp:TableCell VerticalAlign="Top">
				            <asp:label id="RECS" runat="server" CssClass="txtseven"></asp:label>
			            </asp:TableCell>
			            <asp:TableCell HorizontalAlign="right">
				            <asp:image id="Image14" runat="server"></asp:image>
			            </asp:TableCell>
		            </asp:TableRow>
		            <asp:TableRow id="tableRow15" runat="server" visible="False">
			            <asp:TableCell VerticalAlign="Top">
				            <asp:label id="TOUR" runat="server" CssClass="txtseven"></asp:label>
			            </asp:TableCell>
			            <asp:TableCell HorizontalAlign="right">
				            <asp:image id="Image15" runat="server"></asp:image>
			            </asp:TableCell>
		            </asp:TableRow>
	            </asp:table>
            </div>
        </div>
	    <div id="divHealth" runat="server" class="floatleftonly">
	        <div class="msMapSymbolsPrintTableHealth">
	            <!-- Health icons-->
	            <asp:table id="tableHealth" runat="server" cellSpacing="0" Visible="false">
		            <asp:TableRow>
			            <asp:TableCell ColumnSpan="2">
				            <asp:label style="font-weight:Bold" id="labelHealthTitle" runat="server" CssClass="txtsevenb"></asp:label>
			            </asp:TableCell>
		            </asp:TableRow>
		            <asp:TableRow id="tableRow16" runat="server" visible="False">
			            <asp:TableCell VerticalAlign="Top">
				            <asp:label id="SURG" runat="server" CssClass="txtseven"></asp:label>
			            </asp:TableCell>
			            <asp:TableCell HorizontalAlign="right">
				            <asp:image id="Image16" runat="server"></asp:image>
			            </asp:TableCell>
		            </asp:TableRow>
		            <asp:TableRow id="tableRow17" runat="server" visible="False">
			            <asp:TableCell VerticalAlign="Top">
				            <asp:label id="HCLC" runat="server" CssClass="txtseven"></asp:label>
			            </asp:TableCell>
			            <asp:TableCell HorizontalAlign="right">
				            <asp:image id="Image17" runat="server"></asp:image>
			            </asp:TableCell>
		            </asp:TableRow>
		            <asp:TableRow id="tableRow18" runat="server" visible="False">
			            <asp:TableCell VerticalAlign="Top">
				            <asp:label id="NRCH" runat="server" CssClass="txtseven"></asp:label>
			            </asp:TableCell>
			            <asp:TableCell HorizontalAlign="right">
				            <asp:image id="Image18" runat="server"></asp:image>
			            </asp:TableCell>
		            </asp:TableRow>
		            <asp:TableRow id="tableRow19" runat="server" visible="False">
			            <asp:TableCell VerticalAlign="Top">
				            <asp:label id="CPHO" runat="server" CssClass="txtseven"></asp:label>
			            </asp:TableCell>
			            <asp:TableCell HorizontalAlign="right">
				            <asp:image id="Image19" runat="server"></asp:image>
			            </asp:TableCell>
		            </asp:TableRow>
	            </asp:table>
            </div>
        </div>
	    <div id="divEducation" runat="server" class="floatleftonly">
	        <div class="msMapSymbolsPrintTableEducation">
	            <!-- Education icons-->
	            <asp:table id="tableEducation" runat="server" cellSpacing="0" Visible="false">
		            <asp:TableRow>
			            <asp:TableCell ColumnSpan="2">
				            <asp:label style="font-weight:Bold" id="labelEducationTitle" runat="server" CssClass="txtsevenb"></asp:label>
			            </asp:TableCell>
		            </asp:TableRow>
		            <asp:TableRow id="tableRow20" runat="server" visible="False">
			            <asp:TableCell VerticalAlign="Top">
				            <asp:label id="PRNR" runat="server" CssClass="txtseven"></asp:label>
			            </asp:TableCell>
			            <asp:TableCell HorizontalAlign="right">
				            <asp:image id="Image20" runat="server"></asp:image>
			            </asp:TableCell>
		            </asp:TableRow>
		            <asp:TableRow id="tableRow21" runat="server" visible="False">
			            <asp:TableCell VerticalAlign="Top">
				            <asp:label id="SMIN" runat="server" CssClass="txtseven"></asp:label>
			            </asp:TableCell>
			            <asp:TableCell HorizontalAlign="right">
				            <asp:image id="Image21" runat="server"></asp:image>
			            </asp:TableCell>
		            </asp:TableRow>
		            <asp:TableRow id="tableRow22" runat="server" visible="False">
			            <asp:TableCell VerticalAlign="Top">
				            <asp:label id="CLUN" runat="server" CssClass="txtseven"></asp:label>
			            </asp:TableCell>
			            <asp:TableCell HorizontalAlign="right">
				            <asp:image id="Image22" runat="server"></asp:image>
			            </asp:TableCell>
		            </asp:TableRow>
		            <asp:TableRow id="tableRow23" runat="server" visible="False">
			            <asp:TableCell VerticalAlign="Top">
				            <asp:label id="RECR" runat="server" CssClass="txtseven"></asp:label>
			            </asp:TableCell>
			            <asp:TableCell HorizontalAlign="right">
				            <asp:image id="Image23" runat="server"></asp:image>
			            </asp:TableCell>
		            </asp:TableRow>
	            </asp:table>
            </div>
        </div>
	    <div id="divInfrastructure" runat="server" class="floatleftonly">
	        <div class="msMapSymbolsPrintTableInfrastructure">
	            <!-- Infrastructure icons-->
	            <asp:table id="tableInfrastructure" runat="server" cellSpacing="0" Visible="false">
		            <asp:TableRow>
			            <asp:TableCell ColumnSpan="2">
				            <asp:label style="font-weight:Bold" id="labelInfrastructureTitle" runat="server" CssClass="txtsevenb"></asp:label>
			            </asp:TableCell>
		            </asp:TableRow>
		            <asp:TableRow id="tableRow24" runat="server" visible="False">
			            <asp:TableCell VerticalAlign="Top">
				            <asp:label id="POLC" runat="server" CssClass="txtseven"></asp:label>
			            </asp:TableCell>
			            <asp:TableCell HorizontalAlign="right">
				            <asp:image id="Image24" runat="server"></asp:image>
			            </asp:TableCell>
		            </asp:TableRow>
		            <asp:TableRow id="tableRow25" runat="server" visible="False">
			            <asp:TableCell VerticalAlign="Top">
				            <asp:label id="LOCS" runat="server" CssClass="txtseven"></asp:label>
			            </asp:TableCell>
			            <asp:TableCell HorizontalAlign="right">
				            <asp:image id="Image25" runat="server"></asp:image>
			            </asp:TableCell>
		            </asp:TableRow>
		            <asp:TableRow id="tableRow26" runat="server" visible="False">
			            <asp:TableCell VerticalAlign="Top">
				            <asp:label id="OTGV" runat="server" CssClass="txtseven"></asp:label>
			            </asp:TableCell>
			            <asp:TableCell HorizontalAlign="right">
				            <asp:image id="Image26" runat="server"></asp:image>
			            </asp:TableCell>
		            </asp:TableRow>
		            <asp:TableRow id="tableRow27" runat="server" visible="False">
			            <asp:TableCell VerticalAlign="Top">
				            <asp:label id="PBFC" runat="server" CssClass="txtseven"></asp:label>
			            </asp:TableCell>
			            <asp:TableCell HorizontalAlign="right">
				            <asp:image id="Image27" runat="server"></asp:image>
			            </asp:TableCell>
		            </asp:TableRow>
	            </asp:table>
            </div>
	    </div>
	    
    </div>
</div>