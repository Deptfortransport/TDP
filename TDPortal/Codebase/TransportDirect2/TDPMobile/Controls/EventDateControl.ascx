<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EventDateControl.ascx.cs" Inherits="TDP.UserPortal.TDPMobile.Controls.EventDateControl" %>

<div class="eventDate">
    <div class="jssettings">
        <asp:HiddenField ID="calendarStartDate" runat="server" />
        <asp:HiddenField ID="calendarEndDate" runat="server" />
        <asp:HiddenField ID="todayDate" runat="server" />
        <asp:HiddenField ID="isToVenueFlag" runat="server" />
        <asp:HiddenField ID="isArriveByFlag" runat="server" />
        <asp:HiddenField ID="isNowFlag" runat="server" />
        <asp:HiddenField ID="jsEnabled" Value="false" runat="server" />
    </div>
    <div id="outwardDateDiv" runat="server" class="outward">
        <div class="row">
            <div class="screenReaderOnly">
                <asp:Label ID="eventDateLabel" CssClass="eventDateLabel" AssociatedControlID="outwardDate" runat="server" EnableViewState="false" />                
            </div>
            <div class="dateSelect">
                <div class="screenReaderOnly">
                    <asp:Label ID="outwardDateLabel" CssClass="dateLabel"  AssociatedControlID="outwardDate" runat="server" EnableViewState="false" />
                </div>
                <div class="setdatebox">
                    <asp:TextBox ID="outwardDate" CssClass="text dateEntry jshide" runat="server" OnTextChanged="OutwardDate_Changed"></asp:TextBox>
                    <asp:Button ID="btnOutwardDate" runat="server" CssClass="btnOutwardDate hide" />
                    <noscript>
                        <asp:DropDownList ID="drpDayListNonJS" runat="server" CssClass="drpDays nonjs"></asp:DropDownList>
                        <asp:DropDownList ID="drpMonthListNonJS" runat="server" CssClass="drpMonths nonjs"></asp:DropDownList>
                    </noscript>
                </div>

                <div data-role="page" id="datepage">
                    <asp:ListView runat="server" ID="outwardDateMonths" ItemPlaceholderID="monthPlaceholder">
                        <LayoutTemplate>
                            <div class="datepageContent" data-role="content">	
                                <fieldset data-role="controlgroup" >
                                    <div class="months" data-role="collapsible-set">
                                        <asp:PlaceHolder runat="server" ID="monthPlaceholder" /> 
                                    </div>
                                </fieldset>
                            </div>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <div data-role="collapsible" data-collapsed="<%# GetCollapsed(Eval("monthName")) %>" class="collapseDate" id="div<%# Eval("monthName") %>">
                                <h3 id="<%# Eval("monthName") %>" ><%# Eval("monthName") %> <%# Eval("year") %></h3>
                                <div class="collapseDateDays <%# (GetCollapsed(Eval("monthName")) == "true") ? "collapse" : "" %>">
                                    <ul class="week"><li>Mon</li><li>Tue</li><li>Wed</li><li>Thur</li><li>Fri</li><li>Sat</li><li>Sun</li></ul>

                                    <asp:ListView runat="server" ID="outwardDateWeeks" GroupPlaceholderID="weekPlaceholder" ItemPlaceholderID="dayPlaceholder" DataSource='<%# Eval("dates") %>' GroupItemCount="7">
                                        <LayoutTemplate>
                                            <asp:PlaceHolder runat="server" ID="weekPlaceholder" /> 
                                        </LayoutTemplate>
                                        <ItemTemplate>
                                            <div class="<%# (Eval("disabled") != "") ? "day dayDisabled" : "day" %>" >
                                                <input type="button" 
                                                    name="dataDate" 
                                                    id="<%# Eval("monthName") %><%# Eval("date") %>/<%# Eval("month") %>/<%# Eval("year") %><%# (Eval("disabled") != "") ? "d" : "" %>" 
                                                    title="<%# Eval("date") %>/<%# Eval("month") %>/<%# Eval("year") %>"
                                                    value="<%# Eval("date") %>"
                                                    <%# (Eval("disabled") != "") ? Eval("disabled") : Eval("selected") %>/>
                                            </div>
                                        </ItemTemplate>
                                        <GroupTemplate>
                                            <asp:PlaceHolder ID="dayPlaceholder" runat="server" />
                                        </GroupTemplate>
                                    </asp:ListView>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:ListView>
        
                    <div class="clear"></div>
                    <asp:HiddenField ID="hdnPageBackText1" runat="server" EnableViewState="false" />
                </div>
            </div>
            <div class="timePicker">
                <div class="screenReaderOnly">
                    <asp:Label ID="outwardTimeLabel" CssClass="timeLabel"  AssociatedControlID="outwardTime" runat="server" EnableViewState ="false" />
                </div>
                <div class="settimebox">
                    <asp:Label ID="outwardTimeType" runat="server" CssClass="timeType hide"></asp:Label>
                    <asp:TextBox ID="outwardTime" runat="server" CssClass="timeInput jshide" />
                    <asp:Button ID="btnOutwardTime" runat="server" CssClass="btnOutwardTime hide" />
                    <asp:DropDownList ID="drpHoursListNonJS" runat="server" CssClass="drpHours nonjs hide jsshowinline"></asp:DropDownList>
                    <asp:DropDownList ID="drpMinutesListNonJS" runat="server" CssClass="drpMinutes nonjs hide jsshowinline"></asp:DropDownList>
                </div>

                <div id="timepage" data-role="page" >
                    <div class="timepageContent" data-role="content">
                        <div class="divLeaveArrive">
                            <asp:Button ID="btnLeaveAt" runat="server" CssClass="btnLeaveBy" />
                            <asp:Button ID="btnArriveBy" runat="server" CssClass="btnArriveBy" />
                        </div>
                        <div class="clear"></div>
                        <div class="timeSelectorContainer">
                            
                            <asp:ListView runat="server" ID="lstHours" ItemPlaceholderID="hourPlaceholder">
                                <LayoutTemplate>
                                    <asp:PlaceHolder runat="server" ID="hourPlaceholder" /> 
                                </LayoutTemplate>
                                <ItemTemplate>
                                    <div class="hourRow">
                                        <div class="minutes">
                                            <asp:ListView runat="server" ID="lstMinutes" ItemPlaceholderID="minutePlaceholder" 
                                                DataSource='<%# Eval("minutes") %>'>
                                                <LayoutTemplate>
                                                    <asp:PlaceHolder runat="server" ID="minutePlaceholder" /> 
                                                </LayoutTemplate>
                                                <ItemTemplate>
                                                    <div class="<%# (Eval("disabled") != "") ? "minute timeDisabled" : "minute" %>" >
                                                        <input type="button" 
                                                                name="dataTime" 
                                                                id="<%# Eval("minuteId") %><%# (Eval("disabled") != "") ? "d" : "" %>" 
                                                                value="<%# Eval("minuteVal") %>"
                                                                <%# (Eval("disabled") != "") ? Eval("disabled") : Eval("selected") %>/>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:ListView>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:ListView>
                        </div>
                        <div class="clear"></div>
                        <div class="otherTimesDropContainer">  
                            <div class="otherTimesDropDiv">
                            </div>
                            <asp:Button ID="btnOKOutwardTime" runat="server" />
                        </div>
                        <div class="otherTimesDiv hide">
                            <asp:Button ID="btnOtherTimes" runat="server" />
                        </div>
                        <div class="clear"></div>
                        <br />
                    </div>
                    <asp:HiddenField ID="hdnPageBackText2" runat="server" EnableViewState="false" />
                </div>
            </div>
            <div id="nowSelectDiv" runat="server" class="nowSelect" enableviewstate="false">
                <asp:LinkButton ID="nowLink" runat="server" CssClass="jshide" OnClick="nowLink_Click"></asp:LinkButton>
                <noscript>
                    <asp:Button ID="nowLinkNonJS" runat="server" CssClass="nowLinkNonJS" OnClick="nowLink_Click" />
                </noscript>
            </div>
        </div>
    </div>
</div>
