<?xml version='1.0'?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform"  xmlns:soap="http://schemas.xmlsoap.org/soap/envelope/" version="1.0"
xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
<xsl:template match="/">
  <html>
  <body> 
  <p><h3> Departure Board Service - GetDepartureBoard </h3> </p> 
    <table border="1" cellpadding="1" cellspacing="1"  width="100%" align="center">          
      <xsl:for-each select="soap:Envelope/soap:Body/GetDepartureBoardResponse/GetDepartureBoardResult/DepartureBoardServiceStopInformation">       
      <tr> <td colspan="2">The Values for the given DepartureBoardServiceStopInformation: </td></tr>
       <tr align="left"><td>CallingStopStatus </td><td><xsl:value-of select="CallingStopStatus"/></td></tr>
        <tr align="left"><td>Departure ActivityType</td><td><xsl:value-of select="Departure/ActivityType"/></td></tr> 
        <tr align="left"><td>Departure DepartTime</td><td><xsl:value-of select="Departure/DepartTime"/></td></tr> 
        <tr align="left"><td>Departure ArriveTime</td><td><xsl:value-of select="Departure/ArriveTime"/></td></tr> 
        <tr align="left"><td>Departure RealTime DepartTime </td><td><xsl:value-of select="Departure/RealTime/DepartTime"/></td></tr> 
        <tr align="left"><td>Departure RealTime ArriveTime </td><td><xsl:value-of select="Departure/RealTime/ArriveTime"/></td></tr> 
         <tr align="left"><td>Departure RealTime ArriveTimeType </td><td><xsl:value-of select="Departure/RealTime/ArriveTimeType"/></td></tr> 
        <tr align="left"><td>Departure Stop Name</td><td><xsl:value-of select="Departure/Stop/Name"/></td></tr>
       
        <tr align="left"><td>Stop ActivityType</td><td><xsl:value-of select="Stop/ActivityType"/></td></tr> 
        <tr align="left"><td>Stop DepartTime</td><td><xsl:value-of select="Stop/DepartTime"/></td></tr> 
        <tr align="left"><td>Stop ArriveTime</td><td><xsl:value-of select="Stop/ArriveTime"/></td></tr> 
        <tr align="left"><td>Stop RealTime DepartTime </td><td><xsl:value-of select="Stop/RealTime/DepartTime"/></td></tr> 
        <tr align="left"><td>Stop RealTime ArriveTime </td><td><xsl:value-of select="Stop/RealTime/ArriveTime"/></td></tr> 
         <tr align="left"><td>Stop RealTime ArriveTimeType </td><td><xsl:value-of select="Stop/RealTime/ArriveTimeType"/></td></tr> 
        <tr align="left"><td>Stop Stop Name</td><td><xsl:value-of select="Stop/Stop/Name"/></td></tr>
        
         
	 <tr align="left"><td>Arrival ActivityType</td><td><xsl:value-of select="Arrival/ActivityType"/></td></tr> 
        <tr align="left"><td>Arrival DepartTime</td><td><xsl:value-of select="Arrival/DepartTime"/></td></tr> 
        <tr align="left"><td>Arrival ArriveTime</td><td><xsl:value-of select="Arrival/ArriveTime"/></td></tr> 
        <tr align="left"><td>Arrival RealTime DepartTime </td><td><xsl:value-of select="Arrival/RealTime/DepartTime"/></td></tr> 
        <tr align="left"><td>Arrival RealTime ArriveTime </td><td><xsl:value-of select="Arrival/RealTime/ArriveTime"/></td></tr> 
         <tr align="left"><td>Arrival RealTime ArriveTimeType </td><td><xsl:value-of select="Arrival/RealTime/ArriveTimeType"/></td></tr> 
        <tr align="left"><td>Arrival Stop Name</td><td><xsl:value-of select="Arrival/Stop/Name"/></td></tr>
        
			
	 <tr align="left"><td>HasTrainDetails</td><td><xsl:value-of select="HasTrainDetails"/></td></tr>
        <tr align="left"><td>Service OperatorCode </td><td><xsl:value-of select="Service/OperatorCode"/></td></tr>
        <tr align="left"><td>Service ServiceNumber </td><td><xsl:value-of select="Service/ServiceNumber"/></td></tr>
        <tr align="left"><td>CircularRoute </td><td><xsl:value-of select="CircularRoute"/></td></tr>
        <tr align="left"><td>FalseDestination </td><td><xsl:value-of select="FalseDestination"/></td></tr>
        <tr align="left"><td>Cancelled </td><td><xsl:value-of select="Cancelled"/></td></tr>
        <tr align="left"><td>Via </td><td><xsl:value-of select="Via"/></td></tr>
         <tr> <td colspan="2" bgcolor="black">**********************</td></tr>
      </xsl:for-each>
      </table>
      <p><br/></p>
      <xsl:if test="count(soap:Envelope/soap:Body/GetDepartureBoardResponse/GetDepartureBoardResult/DepartureBoardServiceStopInformation/PreviousIntermediates/DepartureBoardServiceInformation) > 0">                   
	<p><h4> The Previous stops are as follows: </h4></p>
	 <table border="1" cellpadding="1" cellspacing="1"  width="100%" align="center">  
	 	<xsl:for-each select="soap:Envelope/soap:Body/GetDepartureBoardResponse/GetDepartureBoardResult/DepartureBoardServiceStopInformation/PreviousIntermediates/DepartureBoardServiceInformation">
	 		<tr align="left"><td>ActivityType</td><td><xsl:value-of select="ActivityType"/></td></tr> 
	 		<tr align="left"><td>DepartTime</td><td><xsl:value-of select="DepartTime"/></td></tr> 
			<tr align="left"><td>ArriveTime</td><td><xsl:value-of select="ArriveTime"/></td></tr> 
			<tr align="left"><td>RealTime DepartTime</td><td><xsl:value-of select="RealTime/DepartTime"/></td></tr> 
			<tr align="left"><td>RealTime DepartTimeType</td><td><xsl:value-of select="RealTime/DepartTimeType"/></td></tr> 
			<tr align="left"><td>RealTime ArriveTime</td><td><xsl:value-of select="RealTime/ArriveTime"/></td></tr> 
			<tr align="left"><td>RealTime ArriveTimeType</td><td><xsl:value-of select="RealTime/ArriveTimeType"/></td></tr>
			<tr align="left"><td>Stop Name</td><td><xsl:value-of select="Stop/Name"/></td></tr>
			<tr> <td colspan="2" bgcolor="black">**********************</td></tr> 
	 	  </xsl:for-each>
	 </table>
	</xsl:if>
	
	<p><br/></p>
      <xsl:if test="count(soap:Envelope/soap:Body/GetDepartureBoardResponse/GetDepartureBoardResult/DepartureBoardServiceStopInformation/OnwardIntermediates/DepartureBoardServiceInformation) > 0">                   
	<p><h4> The Next stops are as follows: </h4></p>
	 <table border="1" cellpadding="1" cellspacing="1"  width="100%" align="center">  
	 	<xsl:for-each select="soap:Envelope/soap:Body/GetDepartureBoardResponse/GetDepartureBoardResult/DepartureBoardServiceStopInformation/OnwardIntermediates/DepartureBoardServiceInformation">
	 		<tr align="left"><td>ActivityType</td><td><xsl:value-of select="ActivityType"/></td></tr> 
	 		<tr align="left"><td>DepartTime</td><td><xsl:value-of select="DepartTime"/></td></tr> 
			<tr align="left"><td>ArriveTime</td><td><xsl:value-of select="ArriveTime"/></td></tr> 
			<tr align="left"><td>RealTime DepartTime</td><td><xsl:value-of select="RealTime/DepartTime"/></td></tr> 
			<tr align="left"><td>RealTime DepartTimeType</td><td><xsl:value-of select="RealTime/DepartTimeType"/></td></tr> 
			<tr align="left"><td>RealTime ArriveTime</td><td><xsl:value-of select="RealTime/ArriveTime"/></td></tr> 
			<tr align="left"><td>RealTime ArriveTimeType</td><td><xsl:value-of select="RealTime/ArriveTimeType"/></td></tr>
			<tr align="left"><td>Stop Name</td><td><xsl:value-of select="Stop/Name"/></td></tr>
			<tr> <td colspan="2" bgcolor="black">**********************</td></tr> 
	 	  </xsl:for-each>
	 </table>
	</xsl:if>

  </body>
  </html>
</xsl:template>
</xsl:stylesheet>