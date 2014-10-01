<?xml version='1.0'?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform"  xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/" version="1.0"
xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
<xsl:template match="/">
  <html>
  <body>
	<p><h3> Travel News Service- GetTravelNewsDetails </h3> </p>   
    <table border="1" cellpadding="1" cellspacing="1"  width="100%" align="center">
      <tr align="left">
        <th>Uid</th>
        <th>HeadlineText</th>
        <th>DelayTypes</th>
        <th>SeverityLevel</th>
        <th>TransportType</th>
        <th>Regions</th>       
      </tr>      
      <xsl:for-each select="soap:Envelope/soap:Body/GetTravelNewsHeadlinesResponse/GetTravelNewsHeadlinesResult/TravelNewsServiceHeadlineItem">       
      <tr align="left">
        <td><xsl:value-of select="Uid"/></td>
        <td><xsl:value-of select="HeadlineText"/></td> 
        <td><xsl:value-of select="DelayTypes/TravelNewsServiceDelayType"/></td> 
        <td><xsl:value-of select="SeverityLevel"/></td> 
        <td><xsl:value-of select="TransportType"/></td>
        <td><xsl:value-of select="Regions"/></td>       
      </tr>
      </xsl:for-each>
      </table>                   
  </body>
  </html>
</xsl:template>
</xsl:stylesheet>