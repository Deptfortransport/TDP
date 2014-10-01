<?xml version='1.0'?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform"  xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/" version="1.0"
xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
<xsl:template match="/">
  <html>
  <body>
	<p><h3> Travel News Service- GetTravelNewsDetailsByUid </h3> </p>   
    <table border="1" cellpadding="1" cellspacing="1"  width="100%" align="center">
      <tr align="left">
        <th>Uid</th>
        <th>SeverityLevel</th>
        <th>PublicTransportOperator</th>
        <th>RegionalOperator</th>
        <th>ModeOfTransport</th>
        <th>Regions</th>
        <th>Location</th>
        <th>RegionsLocation</th>
        <th>IncidentType</th>
        <th>HeadlineText</th>
        <th>DetailText</th>
        <th>IncidentStatus</th>
        <th>GridReference</th>
        <th>ReportedDateTime</th>
        <th>ExpiryDateTime</th>
      </tr>      
      <xsl:for-each select="soap:Envelope/soap:Body/GetTravelNewsDetailsByUidResponse/GetTravelNewsDetailsByUidResult">       
      <tr align="left">
        <td><xsl:value-of select="Uid"/></td>
        <td><xsl:value-of select="SeverityLevel"/></td> 
        <td><xsl:value-of select="PublicTransportOperator"/></td> 
        <td><xsl:value-of select="RegionalOperator"/></td> 
        <td><xsl:value-of select="ModeOfTransport"/></td>
        <td><xsl:value-of select="Regions"/></td>
        <td><xsl:value-of select="Location"/></td> 
        <td><xsl:value-of select="RegionsLocation"/></td>  
        <td><xsl:value-of select="IncidentType"/></td>
        <td><xsl:value-of select="HeadlineText"/></td> 
        <td><xsl:value-of select="DetailText"/></td> 
        <td><xsl:value-of select="IncidentStatus"/></td> 
        <td>Easting: <xsl:value-of select="GridReference/Easting"/>  Northing: <xsl:value-of select="GridReference/Northing"/></td>
        <td><xsl:value-of select="ReportedDateTime"/></td>
        <td><xsl:value-of select="ExpiryDateTime"/></td> 
      </tr>
      </xsl:for-each>
      </table>                   
  </body>
  </html>
</xsl:template>
</xsl:stylesheet>