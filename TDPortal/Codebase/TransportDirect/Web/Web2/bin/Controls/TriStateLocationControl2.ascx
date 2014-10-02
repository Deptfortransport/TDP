<%@ Control Language="c#" AutoEventWireup="True" Codebehind="TriStateLocationControl2.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.TriStateLocationControl2" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="uc1" TagName="LocationDisplayControl" Src="LocationDisplayControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="LocationSelectControl2" Src="LocationSelectControl2.ascx" %>
<%@ Register TagPrefix="uc1" TagName="LocationAmbiguousControl" Src="LocationAmbiguousControl.ascx" %>
<uc1:locationdisplaycontrol id="locationValid" visible="False" runat="server"></uc1:locationdisplaycontrol><uc1:locationselectcontrol2 id="locationUnspecified" visible="False" runat="server"></uc1:locationselectcontrol2>
<uc1:locationambiguouscontrol id="locationAmbiguous" visible="False" runat="server" />
