<?xml version="1.0" encoding="UTF-8" ?>
<stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	<xsl:template match="/*">
		<xsl:for-each select="//airregion">
			<xsl:sort select="@code" order="ascending" data-type="number" />
			<xsl:copy-of select="." />
		</xsl:for-each>
	</xsl:template>
</stylesheet>
