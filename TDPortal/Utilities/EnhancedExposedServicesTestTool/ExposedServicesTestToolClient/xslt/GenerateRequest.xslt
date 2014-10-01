<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform"  xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/" version="1.0"
xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
<xsl:template match="/">
  <html>
  <body>
    <table border="1">
      <tr>
        <th>Succeeded at: </th>
      </tr>
      
      <xsl:for-each select="soap:Envelope/soap:Body/GenerateRequestResponse/GenerateRequestResult">
      <tr>
        <td><xsl:value-of select="."/></td>        
      </tr>
      </xsl:for-each>
    </table>
  </body>
  </html>
</xsl:template>
</xsl:stylesheet>
