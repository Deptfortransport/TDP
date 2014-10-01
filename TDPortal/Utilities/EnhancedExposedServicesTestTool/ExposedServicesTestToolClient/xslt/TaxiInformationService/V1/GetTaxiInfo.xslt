<?xml version='1.0'?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform"  xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/" version="1.0"
xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
<xsl:template match="/">
  <html>
  <body>
	<p><h3> Taxi Information - GetTaxiInfo</h3> </p>   
    <table border="1" cellpadding="1" cellspacing="1"  width="100%" align="center">
      <tr align="left">
        <th>StopName</th> 
        <th>Stop Comment</th> 
        <th>AccessibleOperatorPresent</th> 
        <th>AccessibleText</th> 
        <th>InformationAvailable</th> 
        <th>Taxi Operator Data</th> 
      </tr>      
      <xsl:for-each select="soap:Envelope/soap:Body/GetTaxiInfoResponse/GetTaxiInfoResult">       
      <tr align="left">
        <td><xsl:value-of select="StopName"/></td>
        <td><xsl:value-of select="Comment"/></td>
	<td><xsl:value-of select="AccessibleOperatorPresent"/></td>
	<td><xsl:value-of select="AccessibleText"/></td>
	<td><xsl:value-of select="InformationAvailable"/></td>

	<td valign= "top">         
		<table border="1" cellpadding="1" cellspacing="1"  width="100%" align="center">
		<tr align="left">
			<th>Taxi Operators Name</th> 
			<th>Taxi Operator PhoneNumber</th>
			<th>Taxi Operator Accessible</th> 
		</tr>    
		<xsl:for-each select="Operators/TaxiInformationOperator">    
		<tr align="left">
			<td><xsl:value-of select="Name"/></td>
			<td><xsl:value-of select="PhoneNumber"/></td>
			<td><xsl:value-of select="Accessible"/></td>
		</tr>
		</xsl:for-each>
		</table>  
	</td>
	
      </tr>
      </xsl:for-each>
      </table>                   
  </body>
  </html>
</xsl:template>
</xsl:stylesheet>