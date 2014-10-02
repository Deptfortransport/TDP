dojo._xdResourceLoaded(function(dojo, dijit, dojox){
return {depends: [["provide", "esri.nls.jsapi_fi"],
["provide", "esri.nls.jsapi"],
["provide", "esri.nls.jsapi.fi"],
["provide", "dojo.cldr.nls.number"],
["provide", "dojo.cldr.nls.number.fi"],
["provide", "dojo.cldr.nls.gregorian"],
["provide", "dojo.cldr.nls.gregorian.fi"]],
defineResource: function(dojo, dijit, dojox){dojo.provide("esri.nls.jsapi_fi");dojo.provide("esri.nls.jsapi");esri.nls.jsapi._built=true;dojo.provide("esri.nls.jsapi.fi");esri.nls.jsapi.fi={"io":{"proxyNotSet":"esri.config.defaults.io.proxyUrl is not set."},"tasks":{"gp":{"gpDataTypeNotHandled":"GP Data type not handled."},"na":{"route":{"routeNameNotSpecified":"'RouteName' not specified for atleast 1 stop in stops FeatureSet."}}},"geometry":{"deprecateToMapPoint":"esri.geometry.toMapPoint deprecated. Use esri.geometry.toMapGeometry.","deprecateToScreenPoint":"esri.geometry.toScreenPoint deprecated. Use esri.geometry.toScreenGeometry."},"map":{"deprecateShiftDblClickZoom":"Map.(enable/disable)ShiftDoubleClickZoom deprecated. Shift-Double-Click zoom behavior will not be supported.","deprecateReorderLayerString":"Map.reorderLayer(/*String*/ id, /*Number*/ index) deprecated. Use Map.reorderLayer(/*Layer*/ layer, /*Number*/ index)."},"layers":{"dynamic":{"imageError":"Unable to load image"},"tiled":{"tileError":"Unable to load tile"},"imageParameters":{"deprecateBBox":"Property 'bbox' deprecated. Use property 'extent'."},"agstiled":{"deprecateRoundrobin":"Constructor option 'roundrobin' deprecated. Use option 'tileServers'."},"graphics":{"drawingError":"Unable to draw graphic "}},"toolbars":{"draw":{"convertAntiClockwisePolygon":"Polygons drawn in anti-clockwise direction will be reversed to be clockwise."}},"virtualearth":{"vetiledlayer":{"tokensNotSpecified":"Either clientToken & serverToken must be provided or tokenUrl must be specified."},"vegeocode":{"tokensNotSpecified":"Either serverToken must be provided or tokenUrl must be specified.","requestQueued":"Server token not retrieved. Queing request to be executed after server token retrieved."}}};dojo.provide("dojo.cldr.nls.number");dojo.cldr.nls.number._built=true;dojo.provide("dojo.cldr.nls.number.fi");dojo.cldr.nls.number.fi={"group":" ","percentSign":"%","exponential":"E","percentFormat":"#,##0 %","scientificFormat":"#E0","list":";","infinity":"∞","patternDigit":"#","minusSign":"-","decimal":",","nan":"epäluku","nativeZeroDigit":"0","perMille":"‰","decimalFormat":"#,##0.###","currencyFormat":"#,##0.00 ¤","plusSign":"+","currencySpacing-afterCurrency-currencyMatch":"[:letter:]","currencySpacing-beforeCurrency-surroundingMatch":"[:digit:]","currencySpacing-afterCurrency-insertBetween":" ","currencySpacing-afterCurrency-surroundingMatch":"[:digit:]","currencySpacing-beforeCurrency-currencyMatch":"[:letter:]","currencySpacing-beforeCurrency-insertBetween":" "};dojo.provide("dojo.cldr.nls.gregorian");dojo.cldr.nls.gregorian._built=true;dojo.provide("dojo.cldr.nls.gregorian.fi");dojo.cldr.nls.gregorian.fi={"months-format-narrow":["T","H","M","H","T","K","H","E","S","L","M","J"],"quarters-standAlone-narrow":["1","2","3","4"],"field-weekday":"viikonpäivä","dateFormatItem-yQQQ":"QQQ yyyy","dateFormatItem-yMEd":"EEE d.M.yyyy","dateFormatItem-MMMEd":"E d. MMM","eraNarrow":["eKr.","jKr."],"dateFormat-long":"d. MMMM yyyy","months-format-wide":["tammikuuta","helmikuuta","maaliskuuta","huhtikuuta","toukokuuta","kesäkuuta","heinäkuuta","elokuuta","syyskuuta","lokakuuta","marraskuuta","joulukuuta"],"dateFormat-full":"EEEE d. MMMM yyyy","dateFormatItem-Md":"d.M.","field-era":"aikakausi","dateFormatItem-yM":"L.yyyy","months-standAlone-wide":["tammikuuta","helmikuuta","maaliskuuta","huhtikuuta","toukokuuta","kesäkuuta","heinäkuuta","elokuuta","syyskuuta","lokakuuta","marraskuuta","joulukuuta"],"timeFormat-short":"H.mm","quarters-format-wide":["1. neljännes","2. neljännes","3. neljännes","4. neljännes"],"dateTimeFormat":"{1} {0}","timeFormat-long":"H.mm.ss z","field-year":"vuosi","dateFormatItem-yMMM":"LLL yyyy","dateFormatItem-yQ":"Q/yyyy","dateFormatItem-yyyyMMMM":"LLLL yyyy","field-hour":"tunti","months-format-abbr":["tammi","helmi","maalis","huhti","touko","kesä","heinä","elo","syys","loka","marras","joulu"],"dateFormatItem-yyQ":"Q/yy","patternChars":"GanjkHmsSEDFwWxhKzAeugXZvcL","timeFormat-full":"H.mm.ss v","dateFormatItem-yyyyMEEEd":"EEE d.M.yyyy","am":"ap.","months-standAlone-abbr":["tammi","helmi","maalis","huhti","touko","kesä","heinä","elo","syys","loka","marras","joulu"],"quarters-format-abbr":["1. nelj.","2. nelj.","3. nelj.","4. nelj."],"quarters-standAlone-wide":["1. neljännes","2. neljännes","3. neljännes","4. neljännes"],"dateFormatItem-HHmmss":"HH.mm.ss","dateFormatItem-M":"L","days-standAlone-wide":["sunnuntaina","maanantaina","tiistaina","keskiviikkona","torstaina","perjantaina","lauantaina"],"dateFormatItem-MMMMd":"d. MMMM","dateFormatItem-yyMMM":"MMM yy","timeFormat-medium":"H.mm.ss","dateFormatItem-Hm":"H.mm","quarters-standAlone-abbr":["1. nelj.","2. nelj.","3. nelj.","4. nelj."],"eraAbbr":["eKr.","jKr."],"field-minute":"minuutti","field-dayperiod":"ap/ip","days-standAlone-abbr":["su","ma","ti","ke","to","pe","la"],"dateFormatItem-d":"d","dateFormatItem-ms":"mm.ss","dateFormatItem-MMMd":"d. MMM","dateFormatItem-MEd":"E d.M.","dateFormatItem-yMMMM":"LLLL yyyy","field-day":"päivä","days-format-wide":["sunnuntaina","maanantaina","tiistaina","keskiviikkona","torstaina","perjantaina","lauantaina"],"field-zone":"aikavyöhyke","dateFormatItem-y":"yyyy","months-standAlone-narrow":["T","H","M","H","T","K","H","E","S","L","M","J"],"dateFormatItem-yyMM":"M/yy","days-format-abbr":["su","ma","ti","ke","to","pe","la"],"eraNames":["ennen Kristuksen syntymää","jälkeen Kristuksen syntymän"],"days-format-narrow":["S","M","T","K","T","P","L"],"field-month":"kuukausi","days-standAlone-narrow":["S","M","T","K","T","P","L"],"dateFormatItem-MMM":"LLL","dateFormatItem-HHmm":"HH.mm","pm":"ip.","dateFormatItem-MMMMEd":"E d. MMMM","dateFormat-short":"d.M.yyyy","field-second":"sekunti","dateFormatItem-yMMMEd":"EEE d. MMM yyyy","field-week":"viikko","dateFormat-medium":"d.M.yyyy","dateFormatItem-yyyyM":"M/yyyy","dateFormatItem-mmss":"mm.ss","dateFormatItem-yyyyQQQQ":"QQQQ yyyy","dateTimeFormats-appendItem-Day-Of-Week":"{0} {1}","dateTimeFormats-appendItem-Second":"{0} ({2}: {1})","dateTimeFormats-appendItem-Era":"{0} {1}","dateTimeFormats-appendItem-Week":"{0} ({2}: {1})","quarters-format-narrow":["1","2","3","4"],"dateTimeFormats-appendItem-Day":"{0} ({2}: {1})","dateTimeFormats-appendItem-Year":"{0} {1}","dateTimeFormats-appendItem-Hour":"{0} ({2}: {1})","dateTimeFormats-appendItem-Quarter":"{0} ({2}: {1})","dateTimeFormats-appendItem-Month":"{0} ({2}: {1})","dateTimeFormats-appendItem-Minute":"{0} ({2}: {1})","dateTimeFormats-appendItem-Timezone":"{0} {1}"};

}};});