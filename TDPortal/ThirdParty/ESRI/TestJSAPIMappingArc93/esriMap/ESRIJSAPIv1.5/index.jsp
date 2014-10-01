<%@page contentType="application/x-javascript" session="false" %>
<% out.print("if (typeof dojo == \"undefined\") {"); %>
<jsp:include page="js/dojo/dojo/dojo.xd.js" />
<% out.print("}"); %>
<jsp:include page="js/esri/esri.js" />
<jsp:include page="js/esri/jsapi.js" />