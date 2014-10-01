<%@ Page language="c#" Codebehind="ExposedServicePage.aspx.cs" AutoEventWireup="True" Inherits="ExposedServicesTestToolClient.ExposedServicePage" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head>
		<title>Exposed Services Test Tool</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<link href="../setup.css" type="text/css" rel="stylesheet" media="all" />
		<asp:literal id="refresh" runat="Server"></asp:literal>
	</head>
	<body>
	    <div class="mainbody">
		<form id="Form1" method="post" runat="server">
		    <div>
		        <div class="floatleft">
		            <h2>Exposed services test tool</h2>
		        </div>
		        <div class="floatright">
		            <img src="../Poweredby1a.gif" alt="Powered by Transport Direct" />
		        </div>
		    </div>
		    
		    <br />
		    
		    <table class="tableInput" cellpadding="5px" cellspacing="5px">
			    <tr>
				    <td><p>Select the file with the message(s) to upload</p></td>
					<td><input class="uploadFileInput" id="UploadFile" type="file" name="UploadFile" runat="server"></td>
                    <td class="errorColumn">
						<p><asp:requiredfieldvalidator id="RequiredFieldValidator2" runat="server" ErrorMessage="Please select file to upload"
						    ControlToValidate="UploadFile" EnableViewState="False"></asp:requiredfieldvalidator></p>
                    </td>
                </tr>
                <tr>
				    <td><p>Select Webservice to invoke:</p></td>
					<td colSpan="2"><asp:dropdownlist id="listWebservice" runat="server"></asp:dropdownlist></td>
				</tr>
			    <tr>
				    <td colSpan="3"><p>Send Options</p></td>
                </tr>
                <tr>
				    <td align="right"><p>Interval: every</p></td>
					<td><p><asp:textbox id="textInterval" runat="server" CssClass="secondsInput">1</asp:textbox>second(s)</p></td>
					<td class="errorColumn">
					    <p><asp:rangevalidator id="RangeValidator1" runat="server" ErrorMessage="Should be between 0 and 100 seconds"
										ControlToValidate="textInterval" EnableViewState="False" MaximumValue="100" MinimumValue="0" Type="Integer"
										Display="Dynamic"></asp:rangevalidator>&nbsp;
                        <asp:requiredfieldvalidator id="RequiredFieldValidator1" runat="server" ErrorMessage="Field is required" ControlToValidate="textInterval"
										EnableViewState="False" Display="Dynamic"></asp:requiredfieldvalidator></p>
					</td>
				</tr>
				<tr>
					<td align="right"><p>Number:</p></td>
					<td><p><asp:textbox id="textNrOfTimes" runat="server" CssClass="secondsInput">1</asp:textbox>&nbsp;time(s)</p></td>
					<td class="errorColumn">
						<p><asp:rangevalidator id="RangeValidator3" runat="server" ErrorMessage="Should be between 1 and 100 times"
								ControlToValidate="textNrOfTimes" MaximumValue="100" MinimumValue="1" Type="Integer" Display="Dynamic"></asp:rangevalidator>&nbsp;
                        <asp:requiredfieldvalidator id="RequiredFieldValidator3" runat="server" ErrorMessage="Field is required" ControlToValidate="textNrOfTimes"
								EnableViewState="False" Display="Dynamic"></asp:requiredfieldvalidator></p>
					</td>
				</tr>
			</table>
		    
		    <br />
		    
		    <div>
		        <div class="floatleft">
		            <asp:button id="CallService" runat="server" EnableViewState="False" ForeColor="#003366" BackColor="#dcdcdc"
						Font-Bold="True" Text="Call Webservice" onclick="CallService_Click"></asp:button>
					&nbsp;
			        <asp:button id="buttonStopProcess" runat="server" EnableViewState="False" ForeColor="#003366"
						BackColor="#dcdcdc" Font-Bold="True" Text="Stop Process" CausesValidation="False" onclick="buttonStopProcess_Click"></asp:button>
				</div>
				<div class="floatright">
					<asp:label id="TextRequestsLeft" runat="server" CssClass="requestsLeftLabel" Visible="False"></asp:label>
				</div>
		    </div>
		    
		    <br />
		    <br />
		    
		    <table class="tableResultButtons" cellpadding="5px" cellspacing="5px">
		        <tr>
		            <td align="center">
		                <asp:button id="buttonShowResults" runat="server" EnableViewState="False" ForeColor="#003366"
                            BackColor="#dcdcdc" Font-Bold="True" Text="Show Results" CausesValidation="False" onclick="buttonShowResults_Click"></asp:button>&nbsp;
						<asp:button id="buttonDeleteResults" runat="server" EnableViewState="False" ForeColor="#003366"
						    BackColor="#dcdcdc" Font-Bold="True" Text="Delete Results" CausesValidation="False" onclick="buttonDeleteResults_Click"></asp:button>&nbsp;
					    <asp:button id="buttonShowAllResults" runat="server" EnableViewState="False" ForeColor="#003366"
						    BackColor="#dcdcdc" Font-Bold="True" Text="Show all Results" CausesValidation="False" onclick="buttonShowAllResults_Click"></asp:button>&nbsp;
						<asp:button id="buttonDeleteAllResults" runat="server" EnableViewState="False" ForeColor="#003366"
						    BackColor="#dcdcdc" Font-Bold="True" Text="Delete all Results" CausesValidation="False" onclick="buttonDeleteAllResults_Click"></asp:button>
		            </td>
		        </tr>
		    </table>
		    
		    <br />
		    
		    <div>
		        <asp:datagrid id="gridResponses" runat="server" ForeColor="Black" BackColor="White" PageSize="15"
                        AllowPaging="True" GridLines="Vertical" CellPadding="3" BorderWidth="1px" BorderStyle="Solid" BorderColor="#330099" 
                        AutoGenerateColumns="False" AllowSorting="True" EnableViewState="False" CssClass="resultsGrid">
                
                    <HeaderStyle CssClass="resultsGridHeader"></HeaderStyle>
                    <FooterStyle CssClass="resultsGridFooter" ></FooterStyle>
	    			<SelectedItemStyle Font-Bold="True" ForeColor="White" BackColor="Yellow"></SelectedItemStyle>
		    		<AlternatingItemStyle BackColor="#E6FFFE"></AlternatingItemStyle>
			    					
				    <Columns>
				        <asp:BoundColumn DataField="TransactionId" SortExpression="TransactionId" HeaderText="Transaction Id"></asp:BoundColumn>
    					<asp:BoundColumn DataField="WebserviceUri" SortExpression="WebserviceUri" HeaderText="Webservice name"></asp:BoundColumn>
	    				<asp:HyperLinkColumn Text="Show Request" Target="_blank" DataNavigateUrlField="OutputUriRequest"></asp:HyperLinkColumn>
		    			<asp:BoundColumn DataField="DateRequested" SortExpression="DateRequested" HeaderText="Date Requested"></asp:BoundColumn>
			    		<asp:HyperLinkColumn Text="Show Response" Target="_blank" DataNavigateUrlField="OutputUriResponse"></asp:HyperLinkColumn>
				    	<asp:BoundColumn DataField="DateReceived" SortExpression="DateReceived" HeaderText="Date Received"></asp:BoundColumn>
					    <asp:BoundColumn DataField="Status" SortExpression="Status" HeaderText="Status"></asp:BoundColumn>
    					<asp:ButtonColumn Text="Delete" CommandName="Delete"></asp:ButtonColumn>
	    			</Columns>
				
		    		<PagerStyle NextPageText="Next Page" PrevPageText="Previous Page" HorizontalAlign="Center" PageButtonCount="5" Mode="NumericPages"
		    		    Font-Size="0.7em" BorderColor="#330099" ForeColor="Black" BackColor="#CCCCFF" ></PagerStyle>
                </asp:datagrid>
			</div>
			    
            <br />
			
			<div class="floatleft">
			    <p>version:<asp:label id="versionNumber" runat="server">Label</asp:label></p>
			</div>
		</form>
		</div>
	</body>
</html>
