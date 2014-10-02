dojo._xdResourceLoaded(function(dojo, dijit, dojox){
return {depends: [["provide", "esri.nls.jsapi_de"],
["provide", "esri.nls.jsapi"],
["provide", "esri.nls.jsapi.de"],
["provide", "dojo.cldr.nls.number"],
["provide", "dojo.cldr.nls.number.de"],
["provide", "dojo.cldr.nls.gregorian"],
["provide", "dojo.cldr.nls.gregorian.de"]],
defineResource: function(dojo, dijit, dojox){dojo.provide("esri.nls.jsapi_de");dojo.provide("esri.nls.jsapi");esri.nls.jsapi._built=true;dojo.provide("esri.nls.jsapi.de");esri.nls.jsapi.de={"io":{"proxyNotSet":"esri.config.defaults.io.proxyUrl is not set."},"tasks":{"gp":{"gpDataTypeNotHandled":"GP Data type not handled."},"na":{"route":{"routeNameNotSpecified":"'RouteName' not specified for atleast 1 stop in stops FeatureSet."}}},"geometry":{"deprecateToMapPoint":"esri.geometry.toMapPoint deprecated. Use esri.geometry.toMapGeometry.","deprecateToScreenPoint":"esri.geometry.toScreenPoint deprecated. Use esri.geometry.toScreenGeometry."},"map":{"deprecateShiftDblClickZoom":"Map.(enable/disable)ShiftDoubleClickZoom deprecated. Shift-Double-Click zoom behavior will not be supported.","deprecateReorderLayerString":"Map.reorderLayer(/*String*/ id, /*Number*/ index) deprecated. Use Map.reorderLayer(/*Layer*/ layer, /*Number*/ index)."},"layers":{"dynamic":{"imageError":"Unable to load image"},"tiled":{"tileError":"Unable to load tile"},"imageParameters":{"deprecateBBox":"Property 'bbox' deprecated. Use property 'extent'."},"agstiled":{"deprecateRoundrobin":"Constructor option 'roundrobin' deprecated. Use option 'tileServers'."},"graphics":{"drawingError":"Unable to draw graphic "}},"toolbars":{"draw":{"convertAntiClockwisePolygon":"Polygons drawn in anti-clockwise direction will be reversed to be clockwise."}},"virtualearth":{"vetiledlayer":{"tokensNotSpecified":"Either clientToken & serverToken must be provided or tokenUrl must be specified."},"vegeocode":{"tokensNotSpecified":"Either serverToken must be provided or tokenUrl must be specified.","requestQueued":"Server token not retrieved. Queing request to be executed after server token retrieved."}}};dojo.provide("dojo.cldr.nls.number");dojo.cldr.nls.number._built=true;dojo.provide("dojo.cldr.nls.number.de");dojo.cldr.nls.number.de={"group":".","percentSign":"%","exponential":"E","percentFormat":"#,##0 %","scientificFormat":"#E0","list":";","infinity":"∞","patternDigit":"#","minusSign":"-","decimal":",","nan":"NaN","nativeZeroDigit":"0","perMille":"‰","decimalFormat":"#,##0.###","currencyFormat":"#,##0.00 ¤","plusSign":"+","currencySpacing-afterCurrency-currencyMatch":"[:letter:]","currencySpacing-beforeCurrency-surroundingMatch":"[:digit:]","currencySpacing-afterCurrency-insertBetween":" ","currencySpacing-afterCurrency-surroundingMatch":"[:digit:]","currencySpacing-beforeCurrency-currencyMatch":"[:letter:]","currencySpacing-beforeCurrency-insertBetween":" "};dojo.provide("dojo.cldr.nls.gregorian");dojo.cldr.nls.gregorian._built=true;dojo.provide("dojo.cldr.nls.gregorian.de");dojo.cldr.nls.gregorian.de={"months-format-narrow":["J","F","M","A","M","J","J","A","S","O","N","D"],"quarters-standAlone-narrow":["1","2","3","4"],"field-weekday":"Wochentag","dateFormatItem-yyQQQQ":"QQQQ yy","dateFormatItem-yQQQ":"QQQ yyyy","dateFormatItem-yMEd":"EEE, yyyy-M-d","dateFormatItem-MMMEd":"E d. MMM","eraNarrow":["v. Chr.","n. Chr."],"dateFormat-long":"d. MMMM yyyy","months-format-wide":["Januar","Februar","März","April","Mai","Juni","Juli","August","September","Oktober","November","Dezember"],"dateFormat-full":"EEEE, d. MMMM yyyy","dateFormatItem-Md":"d.M.","dateFormatItem-yyMMdd":"dd.MM.yy","field-era":"Epoche","dateFormatItem-yM":"yyyy-M","months-standAlone-wide":["Januar","Februar","März","April","Mai","Juni","Juli","August","September","Oktober","November","Dezember"],"timeFormat-short":"HH:mm","quarters-format-wide":["1. Quartal","2. Quartal","3. Quartal","4. Quartal"],"dateTimeFormat":"{1} {0}","timeFormat-long":"HH:mm:ss z","field-year":"Jahr","dateFormatItem-yMMM":"MMM yyyy","dateFormatItem-yQ":"Q yyyy","dateFormatItem-yyyyMMMM":"MMMM yyyy","field-hour":"Stunde","dateFormatItem-MMdd":"dd.MM.","months-format-abbr":["Jan","Feb","Mrz","Apr","Mai","Jun","Jul","Aug","Sep","Okt","Nov","Dez"],"dateFormatItem-yyQ":"Q yy","patternChars":"GjMtkHmsSEDFwWahKzJeugAZvcL","timeFormat-full":"HH:mm:ss v","am":"vorm.","dateFormatItem-H":"H","months-standAlone-abbr":["Jan","Feb","Mär","Apr","Mai","Jun","Jul","Aug","Sep","Okt","Nov","Dez"],"quarters-format-abbr":["Q1","Q2","Q3","Q4"],"quarters-standAlone-wide":["1. Quartal","2. Quartal","3. Quartal","4. Quartal"],"dateFormatItem-HHmmss":"HH:mm:ss","dateFormatItem-hhmmss":"HH:mm:ss","dateFormatItem-M":"L","days-standAlone-wide":["Sonntag","Montag","Dienstag","Mittwoch","Donnerstag","Freitag","Samstag"],"dateFormatItem-MMMMd":"d. MMMM","dateFormatItem-yyMMM":"MMM yy","timeFormat-medium":"HH:mm:ss","dateFormatItem-Hm":"H:mm","eraAbbr":["v. Chr.","n. Chr."],"field-minute":"Minute","field-dayperiod":"Tageshälfte","days-standAlone-abbr":["So.","Mo.","Di.","Mi.","Do.","Fr.","Sa."],"dateFormatItem-d":"d","dateFormatItem-ms":"mm:ss","dateFormatItem-MMMd":"d. MMM","dateFormatItem-MEd":"E, d.M.","dateFormatItem-yMMMM":"MMMM yyyy","field-day":"Tag","days-format-wide":["Sonntag","Montag","Dienstag","Mittwoch","Donnerstag","Freitag","Samstag"],"field-zone":"Zone","dateFormatItem-y":"yyyy","months-standAlone-narrow":["J","F","M","A","M","J","J","A","S","O","N","D"],"dateFormatItem-yyMM":"MM.yy","days-format-abbr":["So.","Mo.","Di.","Mi.","Do.","Fr.","Sa."],"eraNames":["v. Chr.","n. Chr."],"days-format-narrow":["S","M","D","M","D","F","S"],"field-month":"Monat","days-standAlone-narrow":["S","M","D","M","D","F","S"],"dateFormatItem-MMM":"LLL","dateFormatItem-HHmm":"HH:mm","pm":"nachm.","dateFormatItem-MMMMEd":"E d. MMMM","dateFormatItem-MMMMdd":"dd. MMMM","dateFormat-short":"dd.MM.yy","dateFormatItem-MMd":"d.MM.","field-second":"Sekunde","dateFormatItem-yMMMEd":"EEE, d. MMM yyyy","dateFormatItem-hhmm":"HH:mm","dateFormatItem-Ed":"E d.","field-week":"Woche","dateFormat-medium":"dd.MM.yyyy","dateFormatItem-mmss":"mm:ss","dateFormatItem-yyyy":"yyyy","dateTimeFormats-appendItem-Day-Of-Week":"{0} {1}","dateTimeFormats-appendItem-Second":"{0} ({2}: {1})","dateTimeFormats-appendItem-Era":"{0} {1}","dateTimeFormats-appendItem-Week":"{0} ({2}: {1})","quarters-standAlone-abbr":["Q1","Q2","Q3","Q4"],"quarters-format-narrow":["1","2","3","4"],"dateTimeFormats-appendItem-Day":"{0} ({2}: {1})","dateTimeFormats-appendItem-Year":"{0} {1}","dateTimeFormats-appendItem-Hour":"{0} ({2}: {1})","dateTimeFormats-appendItem-Quarter":"{0} ({2}: {1})","dateTimeFormats-appendItem-Month":"{0} ({2}: {1})","dateTimeFormats-appendItem-Minute":"{0} ({2}: {1})","dateTimeFormats-appendItem-Timezone":"{0} {1}"};

}};});