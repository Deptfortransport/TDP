<%@ Register TagPrefix="uc1" TagName="RoadSelectControl" Src="RoadSelectControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="RoadDisplayControl" Src="RoadDisplayControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="AmbiguousRoadSelectControl" Src="AmbiguousRoadSelectControl.ascx" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="TriStateRoadControl.ascx.cs" Inherits="TransportDirect.UserPortal.Web.Controls.TristateRoadControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<uc1:ambiguousroadselectcontrol id="ambiguousRoad" runat="server" visible="False"></uc1:ambiguousroadselectcontrol>
<uc1:roaddisplaycontrol id="validRoad" runat="server" visible="False"></uc1:roaddisplaycontrol>
<uc1:roadselectcontrol id="unspecifiedRoad" runat="server"></uc1:roadselectcontrol>
