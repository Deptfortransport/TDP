<?xml version='1.0'?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform"  xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/" version="1.0"
xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
<xsl:template match="/">
  <html>
  <body> 
  <p><h3> Code Service - FindText </h3> </p> 
    <table border="1" cellpadding="1" cellspacing="1"  width="100%" align="center">
      <tr align="left">
        <th>NaptanId</th>
        <th>CodeType</th>
        <th>Code</th>
        <th>Description</th>
        <th>Locality</th>
        <th>Region</th>
        <th>GridReference</th>
        <th>ModeType</th>
      </tr>      
      <xsl:for-each select="soap:Envelope/soap:Body/FindTextResponse/FindTextResult/CodeServiceCodeDetail">       
      <tr align="left">
        <td><xsl:value-of select="NaptanId"/></td>
        <td><xsl:value-of select="CodeType"/></td> 
        <td><xsl:value-of select="Code"/></td> 
        <td><xsl:value-of select="Description"/></td> 
        <td><xsl:value-of select="Locality"/></td>
        <td><xsl:value-of select="Region"/></td>
        <td><xsl:value-of select="GridReference"/></td> 
        <td><xsl:value-of select="ModeType"/></td>         
      </tr>
      </xsl:for-each>
      </table>                   
  </body>
  </html>
</xsl:template>
</xsl:stylesheet>