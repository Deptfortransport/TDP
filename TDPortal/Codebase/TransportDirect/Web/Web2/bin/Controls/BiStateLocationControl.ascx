<%@ Register TagPrefix="uc1" TagName="LocationSelectControl2" Src="LocationSelectControl2.ascx" %>
<%@ Register TagPrefix="uc1" TagName="LocationDisplayControl" Src="LocationDisplayControl.ascx" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="BiStateLocationControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.BiStateLocationControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<uc1:locationdisplaycontrol id="locationValid" runat="server" visible="False"></uc1:locationdisplaycontrol>
<uc1:locationselectcontrol2 id="locationUnspecified" runat="server" visible="False"></uc1:locationselectcontrol2>