<?xml version="1.0"  ?>
<xsl:stylesheet version="1.0"
  xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
  xmlns:myns="http://www.atkinsglobal.com/journeyplanning/cycleplannerinterface">
  
  <xsl:output method="xml" />
  <xsl:template match="/" >

    <xsl:element name="JourneyResult">

      <xsl:element name="privateJourneys">

        <xsl:element name="PrivateJourney">
          <xsl:element name="sections">

            <xsl:for-each select="//myns:Section">

              <xsl:element name="Section">

                  <xsl:element name="id">
                    <xsl:value-of select="myns:sectionID"/>
                  </xsl:element>

                  <xsl:for-each select="myns:links/myns:ITNLink">

                    <xsl:element name="polyline">
                      <xsl:value-of select="myns:geometry" />
                    </xsl:element>

                  </xsl:for-each>

                <xsl:if test="myns:node">
                  <xsl:for-each select="myns:node">

                    <xsl:if test="myns:geometry">
                      <xsl:element name="polyline">
                        <xsl:value-of select="myns:geometry" />
                      </xsl:element>
                    </xsl:if>

                  </xsl:for-each>
                </xsl:if>

              </xsl:element>

            </xsl:for-each>

          </xsl:element>

        </xsl:element>
        
      </xsl:element>
      
    </xsl:element>

  </xsl:template>
</xsl:stylesheet>