<?xml version='1.0'?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform"  xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/" version="1.0"
xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
<xsl:template match="/">
  <html>
  <body>
	<p><h3> Travel News Service- GetTravelNewsAvailableRegions</h3> </p>   
    <table border="1" cellpadding="1" cellspacing="1"  width="100%" align="center">
      <tr align="left">
        <th>Available Region</th>     
      </tr>      
      <xsl:for-each select="soap:Envelope/soap:Body/GetTravelNewsAvailableRegionsResponse/GetTravelNewsAvailableRegionsResult/string">       
      <tr align="left">
        <td><xsl:value-of select="."/></td>     
      </tr>
      </xsl:for-each>
      </table>                   
  </body>
  </html>
</xsl:template>
</xsl:stylesheet>