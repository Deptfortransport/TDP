<%@ Register TagPrefix="cc1" Namespace="TransportDirect.UserPortal.Web.Controls" Assembly="td.userportal.web" %>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="JourneyEmissionsCompareControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.JourneyEmissionsCompareControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="uc1" TagName="HyperlinkPostbackControl" Src="HyperlinkPostbackControl.ascx" %>
<div id="boxtypeJEfour">
	<div id="boxtypeJEfive"><asp:table id="tableJourneyEmissionsCompare" CssClass="emissionstable" CellSpacing="0" Runat="server"
			Width="100%">
			<asp:tablerow runat="server" id="rowHeading">
				<asp:tablecell runat="server" id="cellEmissionsTitle" columnspan="3" cssclass="ecBorderBottomTwo">
					<asp:label id="labelTitle" runat="server" cssclass="txtsevenb" enableviewstate="false"></asp:label>
				</asp:tablecell>
				<asp:tablecell runat="server" HorizontalAlign="Right"  id="cellTitle" columnspan="3" cssclass="ecBorderBottomTwo">
				    <asp:label id="labelDistanceUnits1" runat="server" cssclass="txtseven" enableviewstate="false"></asp:label>&nbsp;
                    <cc1:scriptabledropdownlist id="dropdownlistUnits1"  runat="server" scriptname="UnitsSwitchEmissions" Visible="False"></cc1:scriptabledropdownlist>&nbsp;
					<cc1:TDButton ID="buttonShowJourney" runat="server"></cc1:TDButton>
				</asp:tablecell>
			</asp:tablerow>
			<asp:TableRow Runat="server">
				<asp:TableCell Runat="server" ColumnSpan="4" CssClass="paddingclass">
					<div class="txtseven"><asp:label id="labelScreenReaderTable" CssClass="screenreader" runat="server"></asp:label>
						<cc1:TDButton ID="buttonChangeView" runat="server"></cc1:TDButton>&nbsp;
						</div>
				</asp:TableCell>
			</asp:TableRow>
			<asp:tablerow runat="server" id="rowJourney" height="40">
				<asp:tablecell runat="server" id="cellJourneyTitle" columnspan="1" Width="55px" CssClass="paddingclass">
					<asp:label id="labelJourneyTitle" runat="server" cssclass="txtseven" enableviewstate="false"></asp:label>
				</asp:tablecell>
				<asp:tablecell runat="server" id="cellJourneyImage" cssclass="ecBorderBottomone" width="93px">
					<cc1:tdimage id="imageJourney" runat="server" ></cc1:tdimage>&nbsp;
				</asp:tablecell>
				<asp:tablecell runat="server" id="cellJourney" cssclass="ecBorderBottomoneleft" Width="250px">
					<div style="POSITION: relative; HEIGHT: 30px">
						<div style="LEFT: -1px; POSITION: absolute; TOP: 0px">
							<cc1:tdimage id="journeyEmissionsColoured" border="1" runat="server"></cc1:tdimage>
						</div>
						<div id="journeydiv" runat="server">
							<cc1:tdimage id="imageEmissionsJourney" height="17" border="1" runat="server"></cc1:tdimage>
							<div style="LEFT: 0px; POSITION: absolute; TOP: 0px"><cc1:tdimage id="imageYourJourneyBlackBar" border="1" runat="server" height="16" width="1"></cc1:tdimage></div>
								<div style="LEFT: 0px; WIDTH: 100px; POSITION: absolute; TOP: 0px">&nbsp;<asp:label id="labelJourneyEmits" runat="server" cssclass="txtsevenb" enableviewstate="false"></asp:label></div>
						</div>
					</div>
				</asp:tablecell>
				<asp:TableCell runat="server" id="cellJourneyPassengers" cssclass="dropdownclass" >
					<asp:label id="labelJourneyWith" runat="server" cssclass="txtseven" enableviewstate="false"
						visible="False"></asp:label>&nbsp;
					<asp:DropDownList runat="server" id="listJourneyPassengers" autopostback="True" visible="False"></asp:DropDownList>&nbsp;
					<asp:label id="labelJourneyPassengers" runat="server" cssclass="txtseven" enableviewstate="false"
						visible="False"></asp:label>
				</asp:TableCell>
			</asp:tablerow>
			<asp:tablerow runat="server" id="rowOutwardJourney" height="35">
				<asp:tablecell runat="server" id="cellOutwardTitle" columnspan="1">
					<asp:label id="labelOutwardTitle" runat="server" cssclass="txtseven" enableviewstate="false"></asp:label>
				</asp:tablecell>
				<asp:tablecell runat="server" id="cellOutwardImage">
					<cc1:tdimage id="imageOutwardJourney" runat="server"></cc1:tdimage>
				</asp:tablecell>
				<asp:tablecell runat="server" id="cellOutwardEmissions">
					<cc1:tdimage id="imageOutwardEmissionsBar" runat="server" imagealign="AbsBottom"></cc1:tdimage>
					<cc1:tdimage id="imageOutwardEmissionsBarOutline" runat="server" imagealign="AbsBottom"></cc1:tdimage>
					<cc1:tdimage id="imageOutwardEmissionsBarEnd" runat="server" imagealign="AbsBottom"></cc1:tdimage>
					&nbsp;
<asp:label id="labelOutwardEmits" runat="server" cssclass="txtsevenb" enableviewstate="false"></asp:label>
				</asp:tablecell>
				<asp:TableCell runat="server" id="cellOutwardPassengers">
					<asp:label id="labelOutwardWith" runat="server" cssclass="txtseven" enableviewstate="false"
						visible="False"></asp:label>&nbsp;
					<asp:DropDownList runat="server" id="listOutwardPassengers" autopostback="True" visible="False"></asp:DropDownList>&nbsp;
					<asp:label id="labelOutwardPassengers" runat="server" cssclass="txtseven" enableviewstate="false"
						visible="False"></asp:label>
				</asp:TableCell>
			</asp:tablerow>
			<asp:tablerow runat="server" id="rowReturnJourney" height="35">
				<asp:tablecell runat="server" id="cellReturnTitle" columnspan="1">
					<asp:label id="labelReturnTitle" runat="server" cssclass="txtseven" enableviewstate="false"></asp:label>
				</asp:tablecell>
				<asp:tablecell runat="server" id="cellReturnImage">
					<cc1:tdimage id="imageReturnJourney" runat="server"></cc1:tdimage>
				</asp:tablecell>
				<asp:tablecell runat="server" id="cellReturnEmissions">
					<cc1:tdimage id="imageReturnEmissionsBar" runat="server" imagealign="AbsBottom"></cc1:tdimage>
					<cc1:tdimage id="imageReturnEmissionsBarOutline" runat="server" imagealign="AbsBottom"></cc1:tdimage>
					<cc1:tdimage id="imageReturnEmissionsBarEnd" runat="server" imagealign="AbsBottom"></cc1:tdimage>
					&nbsp;
<asp:label id="labelReturnEmits" runat="server" cssclass="txtsevenb" enableviewstate="false"></asp:label>
				</asp:tablecell>
				<asp:TableCell runat="server" id="Tablecell2"></asp:TableCell>
			</asp:tablerow>
			<asp:TableRow Runat="server" ID="yourJourneyTableRow"><asp:TableCell ColumnSpan="4"><div style="MARGIN-LEFT: 5px">
				<asp:table id="yourJourneyTable" runat="server" summary="<%# GetYourTableSummary() %>"
					cellSpacing="0" CssClass="emissionsJourneyTable">
					<asp:TableRow>
						<asp:tableheadercell cssclass="emissionheader" id="headerYourTransport"><asp:label id="labelYourJourneyTransport" runat="server" cssclass="txtseven" enableviewstate="false"></asp:label>
							</asp:tableheadercell>
						<asp:tableheadercell cssclass="emissionheader" id="headerYourEmissions">
							<asp:label id="labelYourJourneyEmissionsHeader" runat="server" cssclass="txtseven" enableviewstate="false"></asp:label></asp:tableheadercell>
					</asp:TableRow>
					<asp:TableRow id="smallCarRow2">
						<asp:tablecell cssclass="emissiondata" headers= "<%# GetYourTransportHeader()%>">
							<asp:Label class="txtseven" id="labelYourJourney" Runat="server"></asp:Label></asp:tablecell>
						<asp:tablecell cssclass="emissiondata" headers= "<%# GetYourEmissionsHeader()%>">
						<asp:label id="labelYourJourneyEmissions" runat="server" cssclass="txtseven"></asp:label>&nbsp;
						<asp:Label ID="labelYourJourneyPerPassenger" Runat="server" CssClass="screenreader"></asp:Label>
						<asp:label id="labelYourJourneyWith" runat="server" cssclass="txtseven" enableviewstate="false" visible="False"></asp:label>&nbsp;
						<asp:DropDownList id="listYourJourneyPassengers" runat="server" autopostback="True" visible="False"></asp:DropDownList>&nbsp;
						<asp:label id="labelYourJourneyPassengers" runat="server" cssclass="txtseven" enableviewstate="false" visible="False"></asp:label></asp:tablecell>
					</asp:TableRow>
				</asp:table></div></asp:TableCell>
</asp:TableRow>
			<asp:tablerow runat="server" id="rowCompareJourney">
				<asp:tablecell runat="server" id="cellCompareJourneyText" columnspan="4">
					<asp:label id="labelCompareJourney" runat="server" cssclass="txtseven" enableviewstate="false"></asp:label>
				</asp:tablecell>
			</asp:tablerow>
			<asp:tablerow runat="server" id="rowJourneyDistance">
				<asp:tablecell runat="server" id="cellJourneyDistance" columnspan="4" CssClass="paddingclass">
					<asp:label id="labelJourneyDistance" runat="server" cssclass="txtseven" enableviewstate="false"></asp:label>
				</asp:tablecell>
			</asp:tablerow>
		</asp:table>
		<asp:Panel ID="emissionsGraphicalPanel" Runat="server">
			<asp:table id="tableEmissionsGraph"  Runat="server" CellSpacing="0" CssClass="emissionstable" >
				<asp:TableRow runat="server" id="rowCarEmissions">
					<asp:tablecell runat="server" id="cellCarTitle" Width="60px" CssClass="paddingclass" >
						<asp:label id="labelCarTitle" runat="server" cssclass="txtseven" enableviewstate="false"></asp:label>
					</asp:tablecell>
					<asp:tablecell runat="server" id="cellCarImage" CssClass="rightboarder" >
						<cc1:tdimage id="imageCar" runat="server"></cc1:tdimage>
					</asp:tablecell>
					<asp:tablecell CssClass="nospace" Width="250px">
						<div style="POSITION: relative; HEIGHT: 30px">
							<div style="LEFT: -1px; POSITION: absolute; TOP: 5px">
								<cc1:tdimage id="smallCarEmissionsColoured" border="1" runat="server"></cc1:tdimage>
							</div>
							<div id="graphsmallcardiv" runat="server"><cc1:tdimage id="graphsmallcarimage" height="17" border="1" runat="server"></cc1:tdimage>
								<div style="LEFT: 0px; POSITION: absolute; TOP: 0px"><cc1:tdimage id="imageSmallCarBlackBar" border="1" runat="server" height="16" width="1"></cc1:tdimage></div>
								<div style="LEFT: 0px; WIDTH: 100px; POSITION: absolute; TOP: 0px">&nbsp;
									<asp:label id="labelSmallCarEmits" runat="server" cssclass="txtsevenb" enableviewstate="false"></asp:label></div>
							</div>
						</div>
					</asp:tablecell>
					<asp:TableCell runat="server" id="cellCarPassengers" HorizontalAlign="Right" VerticalAlign="top"
						CssClass="dropdownClass">
					<asp:label id="labelWith" runat="server" cssclass="txtseven" enableviewstate="false"></asp:label>&nbsp;
					<asp:DropDownList runat="server" id="listCarPassengers" autopostback="True"></asp:DropDownList>&nbsp;
					<asp:label id="labelPassengers" runat="server" cssclass="txtseven" enableviewstate="false"></asp:label>
				</asp:TableCell>
				</asp:TableRow>
				<asp:TableRow runat="server" id="rowCarLargeEmissions">
					<asp:tablecell runat="server" id="cellCarLargeTitle" Width="60px" CssClass="paddingclass">
						<asp:label id="labelCarLargeTitle" runat="server" cssclass="txtseven" enableviewstate="false"
							Width="60px"></asp:label>
					</asp:tablecell>
					<asp:tablecell runat="server" id="cellCarLargeImage" CssClass="rightboarder" width="95px">
						<cc1:tdimage id="imageLargeCar" runat="server" CssClass="rightboarder" ></cc1:tdimage>
					</asp:tablecell>
					<asp:tablecell runat="server" id="cellCarLargeEmissions" CssClass="nospace" Width="250px">
						<div style="POSITION: relative; HEIGHT: 30px">
							<div style="LEFT: -1px; WIDTH: 200px; POSITION: absolute; TOP: 5px">
								<cc1:tdimage id="largeCarEmissionsColoured" border="1" runat="server"></cc1:tdimage>
							</div>
							<div id="graphdiv" runat="server">
								<cc1:tdimage id="graphimage" height="17" border="1" runat="server"></cc1:tdimage>
								<div style="LEFT: 0px; POSITION: absolute; TOP: 0px"><cc1:tdimage id="imageLargeCarBlackBar" border="1" runat="server" height="16" width="1"></cc1:tdimage></div>
								<div style="LEFT: 0px; WIDTH: 100px; POSITION: absolute; TOP: 0px">&nbsp;<asp:label id="labelCarLargeEmits" runat="server" cssclass="txtsevenb" enableviewstate="false"></asp:label></div>
							</div>
						</div>
					</asp:tablecell>
					<asp:TableCell runat="server" id="cellCarLargePassengers" HorizontalAlign="Right" 
						CssClass="dropdownClass" >
					<asp:label id="labelLargeWith" runat="server" cssclass="txtseven" enableviewstate="false"></asp:label>&nbsp;
					<asp:DropDownList runat="server" id="listCarLargePassengers" autopostback="True" ></asp:DropDownList>&nbsp;
					<asp:label id="labelLargePassengers" runat="server" cssclass="txtseven" enableviewstate="false"></asp:label>
				</asp:TableCell>
				</asp:TableRow>
				<asp:tablerow runat="server" id="rowTrainEmissions">
					<asp:tablecell runat="server" id="cellTrainTitle" CssClass="paddingclass">
						<asp:label id="labelTrainTitle" runat="server" cssclass="txtseven" enableviewstate="false"
							Width="60px"></asp:label>
					</asp:tablecell>
					<asp:tablecell runat="server" id="cellTrainImage" CssClass="rightboarder" width="97px">
						<cc1:tdimage id="imageTrain" runat="server"></cc1:tdimage>
					</asp:tablecell>
					<asp:tablecell runat="server" id="cellTrainEmissions" CssClass="nospace">
						<div style="WIDTH: 200px; POSITION: relative; HEIGHT: 30px">
							<div style="LEFT: -1px; WIDTH: 200px; POSITION: absolute; TOP: 5px">
								<cc1:tdimage id="trainEmissionsColoured" border="1" runat="server"></cc1:tdimage>
							</div>
							<div id="graphtraindiv" runat="server">
								<cc1:tdimage id="graphtrainimage" height="17" border="1" runat="server"></cc1:tdimage><div style="LEFT: 0px; WIDTH: 200px; POSITION: absolute; TOP: 0px"><cc1:tdimage id="imageTrainBlackBar" border="1" runat="server" height="16" width="1"></cc1:tdimage>&nbsp;
									<asp:label id="labelTrainEmits" runat="server" cssclass="txtsevenb" enableviewstate="false"></asp:label></div>
							</div>
						</div>
					</asp:tablecell>
					<asp:TableCell runat="server" id="cellTrainPassengers" CssClass="paddingclass"></asp:TableCell>
				</asp:tablerow>
				<asp:tablerow runat="server" id="rowBusEmissions">
					<asp:tablecell runat="server" id="cellBusTitle" CssClass="paddingclass">
						<asp:label id="labelBusTitle" runat="server" cssclass="txtseven" enableviewstate="false"></asp:label>
					</asp:tablecell>
					<asp:tablecell runat="server" id="cellBusImage">
						<cc1:tdimage id="imageBus" runat="server"></cc1:tdimage>
					</asp:tablecell>
					<asp:tablecell runat="server" id="Tablecell5" CssClass="nospace">
						<div style="WIDTH: 200px; POSITION: relative; HEIGHT: 30px">
							<div style="LEFT: -1px; WIDTH: 200px; POSITION: absolute; TOP: 5px">
								<cc1:tdimage id="busEmissionsColoured" border="1" runat="server"></cc1:tdimage>
							</div>
							<div id="graphbusdiv" runat="server">
								<cc1:tdimage id="graphbusimage" height="17" border="1" runat="server"></cc1:tdimage><div style="LEFT: 0px; WIDTH: 200px; POSITION: absolute; TOP: 0px"><div style="LEFT: 0px; WIDTH: 200px; POSITION: absolute; TOP: 0px"><cc1:tdimage id="imageBusBlackBar" border="1" runat="server" height="16" width="1"></cc1:tdimage></div>
									&nbsp;<asp:label id="labelBusEmits" runat="server" cssclass="txtsevenb" enableviewstate="false"></asp:label></div>
							</div>
						</div>
					</asp:tablecell>
					<asp:TableCell runat="server" id="cellBusCoachPassengers"></asp:TableCell>
				</asp:tablerow>
				<asp:tablerow runat="server" id="rowCoachEmissions">
					<asp:tablecell runat="server" id="cellCoachTitle" CssClass="paddingclass" Width="60px">
						<asp:label id="labelCoachTitle" runat="server" cssclass="txtseven" enableviewstate="false"
							Width="60px"></asp:label>
					</asp:tablecell>
					<asp:tablecell runat="server" id="cellCoachImage" CssClass="rightboarder">
						<cc1:tdimage id="imageCoach" runat="server"></cc1:tdimage>
					</asp:tablecell>
					<asp:tablecell runat="server" id="cellCoachEmissions" CssClass="nospace">
						<div style="WIDTH: 200px; POSITION: relative; HEIGHT: 30px">
							<div style="LEFT: -1px; WIDTH: 200px; POSITION: absolute; TOP: 5px">
								<cc1:tdimage id="coachEmissionsColoured" border="1" runat="server"></cc1:tdimage>
							</div>
							<div id="graphcoachdiv" runat="server">
								<cc1:tdimage id="graphcoachimage" height="17" runat="server"></cc1:tdimage><div style="LEFT: 0px; WIDTH: 200px; POSITION: absolute; TOP: 0px"><cc1:tdimage id="imageCoachBlackBar" border="1" runat="server" height="16" width="1"></cc1:tdimage></div>
								<div style="LEFT: 0px; WIDTH: 200px; POSITION: absolute; TOP: 0px">&nbsp;
									<asp:label id="labelCoachEmits" runat="server" cssclass="txtsevenb" enableviewstate="false"></asp:label>
								</div>
							</div>
						</div>
					</asp:tablecell>
					<asp:TableCell runat="server" id="cellCoachPassengers" CssClass="paddingclass"></asp:TableCell>
				</asp:tablerow>
				<asp:tablerow runat="server" id="rowPlaneEmissions">
					<asp:tablecell runat="server" id="cellPlaneTitle" CssClass="paddingclass">
						<asp:label id="labelPlaneTitle" runat="server" cssclass="txtseven" enableviewstate="false"
							Width="60px"></asp:label>
					</asp:tablecell>
					<asp:tablecell runat="server" id="cellPlaneImage" CssClass="rightboarder">
						<cc1:tdimage id="imagePlane" runat="server"></cc1:tdimage>
					</asp:tablecell>
					<asp:tablecell runat="server" id="cellPlaneEmissions" CssClass="nospacegraph" Width="185px">
						<div style="WIDTH: 200px; POSITION: relative; HEIGHT: 30px;">
							<div style="LEFT: -1px; WIDTH: 200px; POSITION: absolute; TOP: 5px">
								<cc1:tdimage id="planeEmissionsColoured" border="1" runat="server"></cc1:tdimage>
							</div>
							<div id="graphplanediv" runat="server">
								<cc1:tdimage id="graphplaneimage" height="17" border="1" runat="server"></cc1:tdimage><div style="LEFT: 0px; WIDTH: 200px; POSITION: absolute; TOP: 0px"><cc1:tdimage id="imagePlaneBlackBar" border="1" runat="server" height="16" width="1"></cc1:tdimage></div>
								<div style="LEFT: 0px; WIDTH: 200px; POSITION: absolute; TOP: 0px">&nbsp;
									<asp:label id="labelPlaneEmits" runat="server" cssclass="txtsevenb" enableviewstate="false"></asp:label></div>
							</div>
						</div>
					</asp:tablecell>
					<asp:TableCell runat="server" id="cellPlanePassengers"></asp:TableCell>
				</asp:tablerow>
				<asp:tablerow runat="server" id="rowgraphlabels">
					<asp:tablecell runat="server" id="cellgraphTitle"></asp:tablecell>
					<asp:tablecell runat="server" id="cellgraphImage"></asp:tablecell>
					<asp:tablecell runat="server" id="Tablecell1" CssClass="nospacegraph"><div id="graphxAxisDiv" class="width180">
						<div class="leftBox"><asp:Label id="labelLow" runat="server" cssclass="txtseven" enableviewstate="false"></asp:Label></div>
<div class="centreBox2" align="center"><asp:Label id="labelMedium" runat="server" cssclass="txtseven" enableviewstate="false"></asp:Label></div>
<div class="rightBox"><asp:Label id="labelHigh" runat="server" cssclass="txtseven" enableviewstate="false"></asp:Label>
						</div>
										<div class="newRightBox"><asp:Label id="labelVeryHigh" runat="server" cssclass="txtseven" enableviewstate="false"></asp:Label></div>
						<div id="graphdiv2" style="WIDTH: 200px; LEFT: -1px; TOP: 0px; position:relative" runat="server">
							<cc1:tdimage id="graphBottom" width="180" height="1" runat="server"></cc1:tdimage></div></div>
					</asp:tablecell>
					<asp:TableCell runat="server" id="Tablecell3"></asp:TableCell>
				</asp:tablerow>
				<asp:TableRow>
					<asp:TableCell runat="server" id="Tablecell4"></asp:TableCell>
				</asp:TableRow>
				<asp:tablerow runat="server" id="graphLabelRow">
					<asp:tablecell runat="server" id="graphLabelCell1"></asp:tablecell>
					<asp:tablecell runat="server" id="graphLabelCell2"></asp:tablecell>
					<asp:tablecell runat="server" id="graphLabelCell3" CssClass="graphLabelCell3">
						<div class="width180"><div class="leftBox"><label class="leftBoxText">0</label></div>
							<div class="centreBox" align="center">&nbsp;&nbsp;<asp:label ID="labelGraphKey" cssclass="centreBoxText" Runat="server"></asp:label>
							</div>
							<div class="newRightBox">
								<asp:label id="maxEmissionsValueLabel" runat="server" cssclass="rightBoxText"></asp:label></div>
										</div>
					</asp:tablecell>
					<asp:TableCell runat="server" id="graphLabelCell4"></asp:TableCell>
				</asp:tablerow>
			</asp:table>
		</asp:Panel>
		<asp:Panel ID="emissionsTableViewPanel" Runat="server">&nbsp; 
<div style="MARGIN-LEFT: 5px">
				<asp:table id="tableEmissionsMode" runat="server" summary="<%# GetEmissionsTableSummary() %>"
					cellSpacing="0" CssClass="journeyEmissionsTableModeTable">
					<asp:TableRow>
						<asp:tableheadercell cssclass="emissionheader" id="headerTransport">
							<asp:label  cssclass="txtseven" id="labelTransportHeader" EnableViewState="False" runat="server"></asp:label></asp:tableheadercell>
						<asp:tableheadercell cssclass="emissionheaderright" id="headerEmissions">
							<asp:label cssclass="txtseven" id="labelEmissionsHeader" EnableViewState="False" runat="server"></asp:label></asp:tableheadercell>
					</asp:TableRow>
					<asp:TableRow id="smallCarRow">
						<asp:tablecell cssclass="emissiondata" headers="<%# GetTransportHeader() %>">
							<asp:Label CssClass="txtseven" id="labelCarTableTitle" EnableViewState="False" runat="server"></asp:Label></asp:tablecell>
						<asp:tablecell cssclass="emissiondata" headers="<%# GetEmissionsHeader() %>">
						<asp:label id="labelSmallCarEmissions" runat="server" cssclass="txtseven"></asp:label>&nbsp;
						<asp:label id="labelSmallCarPerPassenger" runat="server" CssClass="screenreader"></asp:label>
						<asp:label id="labelTableSmallCarWith" runat="server" cssclass="txtseven" enableviewstate="false"></asp:label>&nbsp;
						<asp:DropDownList id="listTableSmallCarPassengers" runat="server" autopostback="True"></asp:DropDownList>&nbsp;
						<asp:label id="labelTableSmallCarPassengers" runat="server" cssclass="txtseven" enableviewstate="false"></asp:label></asp:tablecell>
					</asp:TableRow>
					<asp:TableRow id="largeCarRow" >
						<asp:tablecell cssclass="emissiondata" headers="<%# GetTransportHeader() %>">
							<asp:Label CssClass="txtseven" id="labelLargeCarTableTitle" EnableViewState="False" runat="server"></asp:Label></asp:tablecell>
						<asp:tablecell cssclass="emissiondata" headers="<%# GetEmissionsHeader() %>">
						<asp:label id="labelLargeCarEmissions" runat="server" cssclass="txtseven"></asp:label>&nbsp;
						<asp:Label id="labelLargeCarPerPassenger" Runat="server" CssClass="screenreader"></asp:Label>
						<asp:label id="labelTableLargeCarWith" runat="server" cssclass="txtseven" enableviewstate="false"></asp:label>&nbsp;
						<asp:DropDownList id="listTableLargeCarPassengers" runat="server" autopostback="True"></asp:DropDownList>&nbsp;
						<asp:label id="labelTableLargeCarPassengers" runat="server" cssclass="txtseven" enableviewstate="false"></asp:label></asp:tablecell>
					</asp:TableRow>
					<asp:TableRow id="trainRow">
						<asp:tablecell cssclass="emissiondata" headers="<%# GetTransportHeader() %>">
							<asp:label cssclass="txtseven" id="labelTrainTableTitle" EnableViewState="False" runat="server"></asp:label></asp:tablecell>
						<asp:tablecell cssclass="emissiondata" headers="<%# GetEmissionsHeader() %>">
							<asp:label id="labelTrainEmissions" runat="server" cssclass="txtseven"></asp:label>
							<asp:Label ID="labelTrainPerPassenger" Runat="server" CssClass="screenreader"></asp:Label>
						</asp:tablecell>
					</asp:TableRow>
					<asp:TableRow id="busRow">
						<asp:tablecell cssclass="emissiondata" headers="<%# GetTransportHeader() %>">
							<asp:label cssclass="txtseven" id="labelBusTableTitle" EnableViewState="False" runat="server"></asp:label></asp:tablecell>
						<asp:tablecell cssclass="emissiondata" headers="<%# GetEmissionsHeader() %>">
							<asp:label id="labelBusEmissions" runat="server" cssclass="txtseven"></asp:label>
							<asp:Label ID="labelBusPerPassenger" Runat="server" CssClass="screenreader"></asp:Label>
						</asp:tablecell>
					</asp:TableRow>
					<asp:TableRow id="coachRow">
						<asp:tablecell cssclass="emissiondata" headers="<%# GetTransportHeader() %>">
							<asp:label cssclass="txtseven" id="labelCoachTableTitle" EnableViewState="False" runat="server"></asp:label></asp:tablecell>
						<asp:tablecell cssclass="emissiondata" headers="<%# GetEmissionsHeader() %>">
							<asp:label id="labelCoachEmissions" runat="server" cssclass="txtseven"></asp:label>
							<asp:Label ID="labelCoachPerPassenger" Runat="server" CssClass="screenreader"></asp:Label>
						</asp:tablecell>
					</asp:TableRow>
					<asp:TableRow id="planeRow">
						<asp:tablecell cssclass="emissiondata" headers="<%# GetTransportHeader() %>">
							<asp:label cssclass="txtseven" id="labelPlaneTableTitle" EnableViewState="False" runat="server"></asp:label></asp:tablecell>
						<asp:tablecell cssclass="emissiondata" headers="<%# GetEmissionsHeader() %>">
							<asp:label id="labelPlaneEmissions" runat="server" cssclass="txtseven"></asp:label>
							<asp:Label ID="labelPlanePerPassenger" Runat="server" CssClass="screenreader"></asp:Label>
						</asp:tablecell>
					</asp:TableRow>
				</asp:table></div>
		&nbsp;
</asp:Panel>
        <asp:table id="tableDistanceUnits" CssClass="emissionstable" CellSpacing="0" Runat="server" Width="100%">
            <asp:tablerow runat="server" id="unitsRow">
		        <asp:tablecell runat="server" id="cellDistanceUnits" HorizontalAlign="Right" cssclass="ecBorderBottomTwo">
			        <asp:label id="labelDistanceUnits2" runat="server" cssclass="txtseven" enableviewstate="false"></asp:label>&nbsp;
                    <cc1:scriptabledropdownlist id="dropdownlistUnits2"  runat="server" scriptname="UnitsSwitchEmissions" Visible="False"></cc1:scriptabledropdownlist>
                </asp:tablecell>	
            </asp:tablerow>	  
		</asp:table>
		<input id="<%# GetHiddenInputId %>" type="hidden" value="<%# UnitsState %>" name="<%# GetHiddenInputId %>" />
		<div style="PADDING-LEFT: 5px; PADDING-BOTTOM: 5px; PADDING-TOP: 5px"><asp:label id="labelCompareTransportNote" runat="server" enableviewstate="false" cssclass="txtseven"></asp:label></div>
	</div>
</div>
&nbsp;
