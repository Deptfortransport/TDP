/**
*   This is a compiled version of ESRI (UK) Custom JavaScript library
*   For use with the ESRI JavaScript API
*   @copyright: ESRI (UK) 2009
*   @author: Sam Larsen, ESRI (UK), slarsen@esriuk.com
*   @date: September 2009
*/
if(!dojo._hasResource["ESRIUK.Util"]){ //_hasResource checks added by build. Do not use _hasResource directly in your code.
dojo._hasResource["ESRIUK.Util"] = true;
/**
*   @copyright: ESRI (UK) 2009
*   @author: Sam Larsen, ESRI (UK), slarsen@esriuk.com
*   @date: July 2009
*   @description:
*     Static class to access variety of util functions throughout the application
*     This class can be accessed throughout the application via:
*       //  reference class/file
*       
*       //  get handle on instance of util
*       var util = ESRIUK.util();
*       //  call it's methods
*       util.isString(unknown);
*   @note:
*     One section below is attributed to Douglas Crockford
*     http://javascript.crockford.com/remedial.html
*     No copyright notice provided with this code
*
*   @updates:
*
*/
if(typeof ESRIUK=="undefined"){ESRIUK={};};ESRIUK.Util=ESRIUK.Util||{};dojo._loadedModules["ESRIUK.Util"] = ESRIUK.Util;;
dojo.declare("ESRIUK.Util", null, (function(){
  /**
  *   @class: ESRIUK.Util
  *
  *   @capability: This class is capable of logging
  *     Picked up in url = ?debug=[0|1|2|3|4|5]
  *     0 = no logging (or just no debug parameter)
  *     5 = all logging
  *     logging to be viewerd in firebug console
  */

  //  START: Externally sourced functions
  /**
  *   @functions:
  *     typeOf isEmpty, isEmpty,String.prototype.entityify, String.prototype.quote, 
  *     String.prototype.supplant, String.prototype.trim
  *   @description: Remedial JavaScript functions taken from Douglas Crockford
  *   @copyright: No copyright defined - therefore none propogated
  *   @source: http://javascript.crockford.com/remedial.html
  */
  var typeOf=function(value){var s=typeof value;if(s==='object'){if(value){if(typeof value.length==='number'&&!(value.propertyIsEnumerable('length'))&&typeof value.splice==='function'){s='array';}}else{s='null';}}return s;};
  var isEmpty=function(o){var i,v;if(typeOf(o)==='object'){for(i in o){v=o[i];if(v!==undefined&&typeOf(v)!=='function'){return false;}}}return true;};
  String.prototype.entityify=function(){return this.replace(/&/g,"&amp;").replace(/</g,"&lt;").replace(/>/g,"&gt;");};
  String.prototype.quote=function(){var c,i,l=this.length,o='"';for(i=0;i<l;i+=1){c=this.charAt(i);if(c>=' '){if(c==='\\'||c==='"'){o+='\\';}o+=c;}else{switch(c){case'\b':o+='\\b';break;case'\f':o+='\\f';break;case'\n':o+='\\n';break;case'\r':o+='\\r';break;case'\t':o+='\\t';break;default:c=c.charCodeAt();o+='\\u00'+Math.floor(c/16).toString(16)+(c%16).toString(16);}}}return o+'"';};
  String.prototype.supplant=function(o){return this.replace(/{([^{}]*)}/g,function(a,b){var r=o[b];return typeof r==='string'||typeof r==='number'?r:a;});};
  String.prototype.trim=function(){return this.replace(/^\s+|\s+$/g,"");}; 
  //  END: Externally sourced functions
  String.prototype.camelCase=function(){var s=trim(this);return(/\S[A-Z]/.test(s))?s.replace(/(.)([A-Z])/g,function(t,a,b){return a+' '+b.toLowerCase();}):s.replace(/( )([a-z])/g,function(t,a,b){return b.toUpperCase();});};
  
  return {
  _logLevel:0,
  degrees:"<span class='degrees'>&deg;</span>",
  isValid:false,
  jsonESRITemplate:{
    result:{
      geometryType:"",
      displayFieldName:"",
      fieldAliases:null,
      spatialReference:null,
      features:[]
    }
  },
  //  START: CONSTRUCTOR
  constructor:function(){ 
    this.isValid = true;
    //  prevent console errors in IE
    if(this.isNothing(window.console)){
      this.setupDummyConsole();
    }
    //  read url debug param
    this._getURLParams();
    if(this._logLevel<1){
      //  bind to empty function
      var fcn = function(){ return false; };
      this.logMessage = this.logDebug = this.logInfo = this.logWarning = this.logError = fcn;
    }else{
      //  bind stubs
      this.log = this.logMessage;
      this.debug = this.logDebug;
      this.info = this.logInfo;
      this.warn = this.logWarning;
      this.error = this.logError;
    }
  },
  //  END: CONSTRUCTOR
  //  START: TYPE METHODS
  isNothing:function(){
    if(arguments.length<1){ return true; }
    var isNothing = function(obj){ return obj === undefined || obj === null; };
    for(var i=0;i<arguments.length;i++){
      if(!isNothing(arguments[0])){ return false; }
    }
    return true;
  },
  isBoolean:function(){
    if(arguments.length<1){ return false; }    
    var isBool = function(obj){ return typeOf(obj)==='boolean'; };
    for(var i=0;i<arguments.length;i++){
      if(!isBool(arguments[0])){ return false; }
    }
    return true;
  },
  isObject:function(){
    if(arguments.length<1){ return false; }    
    var isObject = function(obj){ return typeOf(obj)==='object'; };
    for(var i=0;i<arguments.length;i++){
      if(!isObject(arguments[0])){ return false; }
    }
    return true;
  },    
  isString:function(){
    if(arguments.length<1){ return false; }    
    var isString = function(obj){ return typeOf(obj)==='string'; };
    for(var i=0;i<arguments.length;i++){
      if(!isString(arguments[0])){ return false; }
    }
    return true;
  },
  isArray:function(){
    if(arguments.length<1){ return false; }
    var isArray = function(obj){ return typeOf(obj)==='array'; };
    for(var i=0;i<arguments.length;i++){
      if(!isArray(arguments[0])){ return false; }
    }
    return true;
  },
  isNumber:function(){
    if(arguments.length<1){ return false; }    
    var that = this;
    var isNumber = function(obj){
      if(!that.isNothing(obj)){
        if(typeof obj==="string"){
          if(isNaN(obj)){ return false; }
          else{ return true; }
        }else if(typeof obj==="number"){
          return true;
        }else{
          return false;
        }
      }else{
        return false;
      }
    };
    for(var i=0;i<arguments.length;i++){
      if(!isNumber(arguments[i])){ return false; }
    }
    return true;
  },    
  isStringPopulated:function(/*object*/obj){ return this.isString(obj)&&obj.length>0; },
  isArrayPopulated:function(/*object*/obj){ return this.isArray(obj)&&obj.length>0; },
  parseBool:function(/*object*/obj){
    if(this.isBoolean(obj)){
      return obj;
    }else{
      if(this.isString(obj)){
        return (obj.toLowerCase()==="true");
      }else{
        return false;
      }
    }
  },
  parseNumber:function(/*object*/obj){
    if(this.isNumber(obj)){
      return obj*1;
    }else{
      this.warn(this.declaredClass,arguments.callee.nom,' Could not parse number ',obj);
    }
  },
  isValidXY:function(/*object*/obj){
    if(this.isObject(obj)&&this.isNumber(obj.x,obj.y)){
      return true;
    }
    return false;
  },
  isValidWH:function(obj){
    if(this.isNumber(obj.w,obj.h)){
      return true;
    }
    return false;
  },
  isValidExt:function(/*object*/obj){
    if(this.isObject(obj)&&this.isNumber(obj.xmin,obj.ymin,obj.xmax,obj.ymax)){
      return true;
    }
    return false;
  },
  validateXY:function(/*object*/obj){
    if(this.isString(obj.x)){ obj.x = parseFloat(obj.x); }
    if(this.isString(obj.y)){ obj.y = parseFloat(obj.y); }
    return obj;
  },
  validateExt:function(/*Object*/ext){
    if(typeof ext.xmin === 'string'){ ext.xmin = parseFloat(ext.xmin); }
    if(typeof ext.ymin === 'string'){ ext.ymin = parseFloat(ext.ymin); }
    if(typeof ext.xmax === 'string'){ ext.xmax = parseFloat(ext.xmax); }
    if(typeof ext.ymax === 'string'){ ext.ymax = parseFloat(ext.ymax); }
    return ext;
  },
  //  END: TYPE METHODS
  getElmGutterVertical:function(/*DOMElement*/elm){
    var returnVar = 0;
    returnVar += dojo.style(elm,'paddingLeft');
    returnVar += dojo.style(elm,'paddingRight');
    return returnVar;
  },
  getElmGutterHorizontal:function(/*DOMElement*/elm){
    var returnVar = 0;
    returnVar += dojo.style(elm,'paddingTop');
    returnVar += dojo.style(elm,'paddingBottom');
    return returnVar;
  },
  getST_GeometryExtent:function(/*extent*/ext){
    var x = ext.toJson();
    return x.xmin+' '+x.ymin+', '+x.xmin+' '+x.ymax+', '+x.xmax+' '+x.ymax+', '+x.xmax+' '+x.ymin+','+x.xmin+' '+x.ymin;
  },
  getESRIJsonExtent:function(/*extent*/ext){
    var returnVar = ext.toJson();
    returnVar.spatialReference = "";
    return returnVar;
  },
  getValidJSONResponse:function(data){
    var response = this.jsonESRITemplate.result;
    try{
      var rData = data.result || undefined;
      if(typeof rData ==="string"){
        rData = dojo.fromJson(data.result);
      }else if(typeof rData === 'object' && typeof rData.features ==='object' && typeof rData.features.length === "number"){
        response = rData;
        if(typeof rData.displayFieldName !== 'string'){
          response.displayFieldName = '';
        }
        if(typeof rData.geometryType!=='string'){
          response.geometryType = '';
        }
        if(typeof rData.fieldAliases !== 'object'){
          response.fieldAliases = null;
        }
        if(typeof rData.spatialReference !== 'object'){
          response.spatialReference = null;
        }
      }else{
        
      }
    }catch(err){
      
    }
    return response;
  },
  getValidGraphic:function(obj){
    var graphic = null;
    var x = obj.x || null;
    var y = obj.y || null;
    var paths = obj.paths || null;
    var rings = obj.rings || null;
    var spRef = obj.spatialReference;
    if (x && y) {
      graphic = new esri.geometry.Point([x, y], new esri.SpatialReference(spRef));
    } else if (paths) {
      graphic = new esri.geometry.Polyline(obj);
    } else if (rings) {
      graphic = new esri.geometry.Polygon(obj);
    }
    return graphic;
  },
  getPtExtent:function(pt){
    return new esri.geometry.Extent(pt.x,pt.y,pt.x,pt.y,pt.spatialReference);      
  },
  formatConnect:function(conn,name){
    return {handle:conn,name:name};
  },
  arguments2Array:function(obj){
    return Array.prototype.slice.call(obj);
  },
  //  START: LOGGING
  setupDummyConsole:function(){
    window.console={
      log:function(){ return false; },
      debug:function(){ return false; },
      warn:function(){ return false; },
      info:function(){ return false; },
      error:function(){ return false; }
    };
  },
  logMessage:function(){ if(this._logLevel>4){  } },
  logDebug:function(){ if(this._logLevel>3){  } },
  logInfo:function(){ if(this._logLevel>2){  } },
  logWarning:function(){ if(this._logLevel>1){  } },
  logError:function(){ if(this._logLevel>0){  } },
  //  stubs
  log:function(){ return false; },  //  log level 5
  debug:function(){ return false; },  //  log level 4
  info:function(){ return false; },  //  log level 3
  warn:function(){ return false; },  //  log level 2
  error:function(){ return false; },  //  log level 1
  _getURLParams:function(){
    try{
      if(this.isStringPopulated(window.location.search)){
        // convert our query string into an object (use slice to strip the leading "?")
        var params = dojo.queryToObject(window.location.search.slice(1));
        if(this.isNumber(params.debug)){ this._setDebugLevel(parseInt(params.debug)); }
      }
    }catch(err){
      this.log(this.declaredClass,arguments.callee.nom,err,window.location);
    }
  },
  _setDebugLevel:function(obj){
    if(obj>-1&&obj<6){
      this._logLevel = obj*1;
    }
  }
  //  END: LOGGING    
}
})());
ESRIUK._util = null;
ESRIUK.util = function(){
  if(ESRIUK._util===null){
    ESRIUK._util = new ESRIUK.Util();
  }
  return ESRIUK._util;
};

}

if(!dojo._hasResource["dijit._Contained"]){ //_hasResource checks added by build. Do not use _hasResource directly in your code.
dojo._hasResource["dijit._Contained"] = true;
if(typeof dijit=="undefined"){dijit={};};dijit._Contained=dijit._Contained||{};dojo._loadedModules["dijit._Contained"] = dijit._Contained;;

dojo.declare("dijit._Contained",
		null,
		{
			// summary
			//		Mixin for widgets that are children of a container widget
			//
			// example:
			// | 	// make a basic custom widget that knows about it's parents
			// |	dojo.declare("my.customClass",[dijit._Widget,dijit._Contained],{});
			// 
			getParent: function(){
				// summary:
				//		Returns the parent widget of this widget, assuming the parent
				//		implements dijit._Container
				for(var p=this.domNode.parentNode; p; p=p.parentNode){
					var id = p.getAttribute && p.getAttribute("widgetId");
					if(id){
						var parent = dijit.byId(id);
						return parent.isContainer ? parent : null;
					}
				}
				return null;
			},

			_getSibling: function(which){
				// summary:
				//      Returns next or previous sibling
				// which:
				//      Either "next" or "previous"
				// tags:
				//      private
				var node = this.domNode;
				do{
					node = node[which+"Sibling"];
				}while(node && node.nodeType != 1);
				if(!node){ return null; } // null
				var id = node.getAttribute("widgetId");
				return dijit.byId(id);
			},

			getPreviousSibling: function(){
				// summary:
				//		Returns null if this is the first child of the parent,
				//		otherwise returns the next element sibling to the "left".

				return this._getSibling("previous"); // Mixed
			},

			getNextSibling: function(){
				// summary:
				//		Returns null if this is the last child of the parent,
				//		otherwise returns the next element sibling to the "right".

				return this._getSibling("next"); // Mixed
			},
			
			getIndexInParent: function(){
				// summary:
				//		Returns the index of this widget within its container parent.
				//		It returns -1 if the parent does not exist, or if the parent
				//		is not a dijit._Container
				
				var p = this.getParent();
				if(!p || !p.getIndexOfChild){
					return -1; // int
				}
				return p.getIndexOfChild(this); // int
			}
		}
	);


}

if(!dojo._hasResource["dijit.form.TextBox"]){ //_hasResource checks added by build. Do not use _hasResource directly in your code.
dojo._hasResource["dijit.form.TextBox"] = true;
dijit.form=dijit.form||{};dijit.form.TextBox=dijit.form.TextBox||{};dojo._loadedModules["dijit.form.TextBox"] = dijit.form.TextBox;;



dojo.declare(
	"dijit.form.TextBox",
	dijit.form._FormValueWidget,
	{
		//	summary:
		//		A base class for textbox form inputs

		//	trim: Boolean
		//		Removes leading and trailing whitespace if true.  Default is false.
		trim: false,

		//	uppercase: Boolean
		//		Converts all characters to uppercase if true.  Default is false.
		uppercase: false,

		//	lowercase: Boolean
		//		Converts all characters to lowercase if true.  Default is false.
		lowercase: false,

		//	propercase: Boolean
		//		Converts the first character of each word to uppercase if true.
		propercase: false,

		//	maxLength: String
		//		HTML INPUT tag maxLength declaration.
		maxLength: "",

		templateString:"<input class=\"dijit dijitReset dijitLeft\" dojoAttachPoint='textbox,focusNode'\r\n\tdojoAttachEvent='onmouseenter:_onMouse,onmouseleave:_onMouse'\r\n\tautocomplete=\"off\" type=\"${type}\" ${nameAttrSetting}\r\n\t/>\r\n",
		baseClass: "dijitTextBox",

		attributeMap: dojo.delegate(dijit.form._FormValueWidget.prototype.attributeMap, {
			maxLength: "focusNode" 
		}),

		_getValueAttr: function(){
			// summary:
			//		Hook so attr('value') works as we like.
			// description:
			//		For `dijit.form.TextBox` this basically returns the value of the <input>.
			//
			//		For `dijit.form.MappedTextBox` subclasses, which have both
			//		a "displayed value" and a separate "submit value",
			//		This treats the "displayed value" as the master value, computing the
			//		submit value from it via this.parse().
			return this.parse(this.attr('displayedValue'), this.constraints);
		},

		_setValueAttr: function(value, /*Boolean?*/ priorityChange, /*String?*/ formattedValue){
			// summary:
			//		Hook so attr('value', ...) works.
			//
			// description: 
			//		Sets the value of the widget to "value" which can be of
			//		any type as determined by the widget.
			//
			// value:
			//		The visual element value is also set to a corresponding,
			//		but not necessarily the same, value.
			//
			// formattedValue:
			//		If specified, used to set the visual element value,
			//		otherwise a computed visual value is used.
			//
			// priorityChange:
			//		If true, an onChange event is fired immediately instead of 
			//		waiting for the next blur event.

			var filteredValue;
			if(value !== undefined){
				// TODO: this is calling filter() on both the display value and the actual value.
				// I added a comment to the filter() definition about this, but it should be changed.
				filteredValue = this.filter(value);
				if(typeof formattedValue != "string"){
					if(filteredValue !== null && ((typeof filteredValue != "number") || !isNaN(filteredValue))){
						formattedValue = this.filter(this.format(filteredValue, this.constraints));
					}else{ formattedValue = ''; }
				}
			}
			if(formattedValue != null && formattedValue != undefined && ((typeof formattedValue) != "number" || !isNaN(formattedValue)) && this.textbox.value != formattedValue){
				this.textbox.value = formattedValue;
			}
			this.inherited(arguments, [filteredValue, priorityChange]);
		},

		// displayedValue: String
		//		For subclasses like ComboBox where the displayed value
		//		(ex: Kentucky) and the serialized value (ex: KY) are different,
		//		this represents the displayed value.
		//
		//		Setting 'displayedValue' through attr('displayedValue', ...)
		//		updates 'value', and vice-versa.  Othewise 'value' is updated
		//		from 'displayedValue' periodically, like onBlur etc.
		//
		//		TODO: move declaration to MappedTextBox?
		//		Problem is that ComboBox references displayedValue,
		//		for benefit of FilteringSelect.
		displayedValue: "",

		getDisplayedValue: function(){
			// summary:
			//		Deprecated.   Use attr('displayedValue') instead.
			// tags:
			//		deprecated
			dojo.deprecated(this.declaredClass+"::getDisplayedValue() is deprecated. Use attr('displayedValue') instead.", "", "2.0");
			return this.attr('displayedValue');
		},

		_getDisplayedValueAttr: function(){
			// summary:
			//		Hook so attr('displayedValue') works.
			// description:
			//		Returns the displayed value (what the user sees on the screen),
			// 		after filtering (ie, trimming spaces etc.).
			//
			//		For some subclasses of TextBox (like ComboBox), the displayed value
			//		is different from the serialized value that's actually 
			//		sent to the server (see dijit.form.ValidationTextBox.serialize)
			
			return this.filter(this.textbox.value);
		},

		setDisplayedValue: function(/*String*/value){
			// summary:
			//		Deprecated.   Use attr('displayedValue', ...) instead.
			// tags:
			//		deprecated
			dojo.deprecated(this.declaredClass+"::setDisplayedValue() is deprecated. Use attr('displayedValue', ...) instead.", "", "2.0");
			this.attr('displayedValue', value);
		},
			
		_setDisplayedValueAttr: function(/*String*/value){
			// summary:
			//		Hook so attr('displayedValue', ...) works.
			//	description: 
			//		Sets the value of the visual element to the string "value".
			//		The widget value is also set to a corresponding,
			//		but not necessarily the same, value.

			if(value === null || value === undefined){ value = '' }
			else if(typeof value != "string"){ value = String(value) }
			this.textbox.value = value;
			this._setValueAttr(this.attr('value'), undefined, value);
		},

		format: function(/* String */ value, /* Object */ constraints){
			// summary:
			//		Replacable function to convert a value to a properly formatted string.
			// tags:
			//		protected extension
			return ((value == null || value == undefined) ? "" : (value.toString ? value.toString() : value));
		},

		parse: function(/* String */ value, /* Object */ constraints){
			// summary:
			//		Replacable function to convert a formatted string to a value
			// tags:
			//		protected extension

			return value;	// String
		},

		_refreshState: function(){
			// summary:
			//		After the user types some characters, etc., this method is
			//		called to check the field for validity etc.  The base method
			//		in `dijit.form.TextBox` does nothing, but subclasses override.
			// tags:
			//		protected
		},

		_onInput: function(e){
			if(e && e.type && /key/i.test(e.type) && e.keyCode){
				switch(e.keyCode){
					case dojo.keys.SHIFT:
					case dojo.keys.ALT:
					case dojo.keys.CTRL:
					case dojo.keys.TAB:
						return;
				}
			}
			if(this.intermediateChanges){
				var _this = this;
				// the setTimeout allows the key to post to the widget input box
				setTimeout(function(){ _this._handleOnChange(_this.attr('value'), false); }, 0);
			}
			this._refreshState();
		},

		postCreate: function(){
			// setting the value here is needed since value="" in the template causes "undefined"
			// and setting in the DOM (instead of the JS object) helps with form reset actions
			this.textbox.setAttribute("value", this.textbox.value); // DOM and JS values shuld be the same
			this.inherited(arguments);
			if(dojo.isMoz || dojo.isOpera){
				this.connect(this.textbox, "oninput", this._onInput);
			}else{
				this.connect(this.textbox, "onkeydown", this._onInput);
				this.connect(this.textbox, "onkeyup", this._onInput);
				this.connect(this.textbox, "onpaste", this._onInput);
				this.connect(this.textbox, "oncut", this._onInput);
			}

			/*#5297:if(this.srcNodeRef){
				dojo.style(this.textbox, "cssText", this.style);
				this.textbox.className += " " + this["class"];
			}*/
			this._layoutHack();
		},

		_blankValue: '', // if the textbox is blank, what value should be reported
		filter: function(val){
			// summary:
			//		Auto-corrections (such as trimming) that are applied to textbox
			//		value on blur or form submit.
			// description:
			//		For MappedTextBox subclasses, this is called twice
			// 			- once with the display value
			//			- once the value as set/returned by attr('value', ...)
			//		and attr('value'), ex: a Number for NumberTextBox.
			//
			//		In the latter case it does corrections like converting null to NaN.  In
			//		the former case the NumberTextBox.filter() method calls this.inherited()
			//		to execute standard trimming code in TextBox.filter().
			//
			//		TODO: break this into two methods in 2.0
			//
			// tags:
			//		protected extension
			if(val === null){ return this._blankValue; }
			if(typeof val != "string"){ return val; }
			if(this.trim){
				val = dojo.trim(val);
			}
			if(this.uppercase){
				val = val.toUpperCase();
			}
			if(this.lowercase){
				val = val.toLowerCase();
			}
			if(this.propercase){
				val = val.replace(/[^\s]+/g, function(word){
					return word.substring(0,1).toUpperCase() + word.substring(1);
				});
			}
			return val;
		},

		_setBlurValue: function(){
			this._setValueAttr(this.attr('value'), true);
		},

		_onBlur: function(e){
			if(this.disabled){ return; }
			this._setBlurValue();
			this.inherited(arguments);
		},

		_onFocus: function(e){
			if(this.disabled){ return; }
			this._refreshState();
			this.inherited(arguments);
		},

		reset: function(){
			// Overrides dijit._FormWidget.reset().
			// Additionally resets the displayed textbox value to ''
			this.textbox.value = '';
			this.inherited(arguments);
		}
	}
);

dijit.selectInputText = function(/*DomNode*/element, /*Number?*/ start, /*Number?*/ stop){
	// summary:
	//		Select text in the input element argument, from start (default 0), to stop (default end).

	// TODO: use functions in _editor/selection.js?
	var _window = dojo.global;
	var _document = dojo.doc;
	element = dojo.byId(element);
	if(isNaN(start)){ start = 0; }
	if(isNaN(stop)){ stop = element.value ? element.value.length : 0; }
	element.focus();
	if(_document["selection"] && dojo.body()["createTextRange"]){ // IE
		if(element.createTextRange){
			var range = element.createTextRange();
			with(range){
				collapse(true);
				moveStart("character", start);
				moveEnd("character", stop);
				select();
			}
		}
	}else if(_window["getSelection"]){
		var selection = _window.getSelection();	// TODO: unused, remove
		// FIXME: does this work on Safari?
		if(element.setSelectionRange){
			element.setSelectionRange(start, stop);
		}
	}
};

}

if(!dojo._hasResource["dojo.io.script"]){ //_hasResource checks added by build. Do not use _hasResource directly in your code.
dojo._hasResource["dojo.io.script"] = true;
if(typeof dojo=="undefined"){dojo={};};dojo.io=dojo.io||{};dojo.io.script=dojo.io.script||{};dojo._loadedModules["dojo.io.script"] = dojo.io.script;;

/*=====
dojo.declare("dojo.io.script.__ioArgs", dojo.__IoArgs, {
	constructor: function(){
		//	summary:
		//		All the properties described in the dojo.__ioArgs type, apply to this
		//		type as well, EXCEPT "handleAs". It is not applicable to
		//		dojo.io.script.get() calls, since it is implied by the usage of
		//		"callbackParamName" (response will be a JSONP call returning JSON)
		//		or "checkString" (response is pure JavaScript defined in
		//		the body of the script that was attached).
		//	callbackParamName: String
		//		The URL parameter name that indicates the JSONP callback string.
		//		For instance, when using Yahoo JSONP calls it is normally, 
		//		callbackParamName: "callback". For AOL JSONP calls it is normally 
		//		callbackParamName: "c".
		//	checkString: String
		//		A string of JavaScript that when evaluated like so: 
		//		"typeof(" + checkString + ") != 'undefined'"
		//		being true means that the script fetched has been loaded. 
		//		Do not use this if doing a JSONP type of call (use callbackParamName instead).
		//	frameDoc: Document
		//		The Document object for a child iframe. If this is passed in, the script
		//		will be attached to that document. This can be helpful in some comet long-polling
		//		scenarios with Firefox and Opera.
		this.callbackParamName = callbackParamName;
		this.checkString = checkString;
		this.frameDoc = frameDoc;
	}
});
=====*/

dojo.io.script = {
	get: function(/*dojo.io.script.__ioArgs*/args){
		//	summary:
		//		sends a get request using a dynamically created script tag.
		var dfd = this._makeScriptDeferred(args);
		var ioArgs = dfd.ioArgs;
		dojo._ioAddQueryToUrl(ioArgs);

		if(this._canAttach(ioArgs)){
			this.attach(ioArgs.id, ioArgs.url, args.frameDoc);
		}

		dojo._ioWatch(dfd, this._validCheck, this._ioCheck, this._resHandle);
		return dfd;
	},

	attach: function(/*String*/id, /*String*/url, /*Document?*/frameDocument){
		//	summary:
		//		creates a new <script> tag pointing to the specified URL and
		//		adds it to the document.
		//	description:
		//		Attaches the script element to the DOM.  Use this method if you
		//		just want to attach a script to the DOM and do not care when or
		//		if it loads.
		var doc = (frameDocument || dojo.doc);
		var element = doc.createElement("script");
		element.type = "text/javascript";
		element.src = url;
		element.id = id;
		element.charset = "utf-8";
		doc.getElementsByTagName("head")[0].appendChild(element);
	},

	remove: function(/*String*/id, /*Document?*/frameDocument){
		//summary: removes the script element with the given id, from the given frameDocument.
		//If no frameDocument is passed, the current document is used.
		dojo.destroy(dojo.byId(id, frameDocument));
		
		//Remove the jsonp callback on dojo.io.script, if it exists.
		if(this["jsonp_" + id]){
			delete this["jsonp_" + id];
		}
	},

	_makeScriptDeferred: function(/*Object*/args){
		//summary: 
		//		sets up a Deferred object for an IO request.
		var dfd = dojo._ioSetArgs(args, this._deferredCancel, this._deferredOk, this._deferredError);

		var ioArgs = dfd.ioArgs;
		ioArgs.id = dojo._scopeName + "IoScript" + (this._counter++);
		ioArgs.canDelete = false;

		//Special setup for jsonp case
		if(args.callbackParamName){
			//Add the jsonp parameter.
			ioArgs.query = ioArgs.query || "";
			if(ioArgs.query.length > 0){
				ioArgs.query += "&";
			}
			ioArgs.query += args.callbackParamName
				+ "="
				+ (args.frameDoc ? "parent." : "")
				+ dojo._scopeName + ".io.script.jsonp_" + ioArgs.id + "._jsonpCallback";

			ioArgs.frameDoc = args.frameDoc;

			//Setup the Deferred to have the jsonp callback.
			ioArgs.canDelete = true;
			dfd._jsonpCallback = this._jsonpCallback;
			this["jsonp_" + ioArgs.id] = dfd;
		}
		return dfd; // dojo.Deferred
	},
	
	_deferredCancel: function(/*Deferred*/dfd){
		//summary: canceller function for dojo._ioSetArgs call.

		//DO NOT use "this" and expect it to be dojo.io.script.
		dfd.canceled = true;
		if(dfd.ioArgs.canDelete){
			dojo.io.script._addDeadScript(dfd.ioArgs);
		}
	},

	_deferredOk: function(/*Deferred*/dfd){
		//summary: okHandler function for dojo._ioSetArgs call.

		//DO NOT use "this" and expect it to be dojo.io.script.

		//Add script to list of things that can be removed.		
		if(dfd.ioArgs.canDelete){
			dojo.io.script._addDeadScript(dfd.ioArgs);
		}

		if(dfd.ioArgs.json){
			//Make sure to *not* remove the json property from the
			//Deferred, so that the Deferred can still function correctly
			//after the response is received.
			return dfd.ioArgs.json;
		}else{
			//FIXME: cannot return the dfd here, otherwise that stops
			//the callback chain in Deferred. So return the ioArgs instead.
			//This doesn't feel right.
			return dfd.ioArgs;
		}
	},
	
	_deferredError: function(/*Error*/error, /*Deferred*/dfd){
		//summary: errHandler function for dojo._ioSetArgs call.

		if(dfd.ioArgs.canDelete){
			//DO NOT use "this" and expect it to be dojo.io.script.
			if(error.dojoType == "timeout"){
				//For timeouts, remove the script element immediately to
				//avoid a response from it coming back later and causing trouble.
				dojo.io.script.remove(dfd.ioArgs.id, dfd.ioArgs.frameDoc);
			}else{
				dojo.io.script._addDeadScript(dfd.ioArgs);
			}
		}
		
		return error;
	},

	_deadScripts: [],
	_counter: 1,

	_addDeadScript: function(/*Object*/ioArgs){
		//summary: sets up an entry in the deadScripts array.
		dojo.io.script._deadScripts.push({id: ioArgs.id, frameDoc: ioArgs.frameDoc});
		//Being extra paranoid about leaks:
		ioArgs.frameDoc = null;
	},

	_validCheck: function(/*Deferred*/dfd){
		//summary: inflight check function to see if dfd is still valid.

		//Do script cleanup here. We wait for one inflight pass
		//to make sure we don't get any weird things by trying to remove a script
		//tag that is part of the call chain (IE 6 has been known to
		//crash in that case).
		var _self = dojo.io.script;
		var deadScripts = _self._deadScripts;
		if(deadScripts && deadScripts.length > 0){
			for(var i = 0; i < deadScripts.length; i++){
				//Remove the script tag
				_self.remove(deadScripts[i].id, deadScripts[i].frameDoc);
				deadScripts[i].frameDoc = null;
			}
			dojo.io.script._deadScripts = [];
		}

		return true;
	},

	_ioCheck: function(/*Deferred*/dfd){
		//summary: inflight check function to see if IO finished.

		//Check for finished jsonp
		if(dfd.ioArgs.json){
			return true;
		}

		//Check for finished "checkString" case.
		var checkString = dfd.ioArgs.args.checkString;
		if(checkString && eval("typeof(" + checkString + ") != 'undefined'")){
			return true;
		}

		return false;
	},

	_resHandle: function(/*Deferred*/dfd){
		//summary: inflight function to handle a completed response.
		if(dojo.io.script._ioCheck(dfd)){
			dfd.callback(dfd);
		}else{
			//This path should never happen since the only way we can get
			//to _resHandle is if _ioCheck is true.
			dfd.errback(new Error("inconceivable dojo.io.script._resHandle error"));
		}
	},

	_canAttach: function(/*Object*/ioArgs){
		//summary: A method that can be overridden by other modules
		//to control when the script attachment occurs.
		return true;
	},
	
	_jsonpCallback: function(/*JSON Object*/json){
		//summary: 
		//		generic handler for jsonp callback. A pointer to this function
		//		is used for all jsonp callbacks.  NOTE: the "this" in this
		//		function will be the Deferred object that represents the script
		//		request.
		this.ioArgs.json = json;
	}
}

}

if(!dojo._hasResource["ESRIUK.Dijits.Loading"]){ //_hasResource checks added by build. Do not use _hasResource directly in your code.
dojo._hasResource["ESRIUK.Dijits.Loading"] = true;
/**
*   @copyright: ESRI (UK) 2009
*   @author: Sam Larsen, ESRI (UK), slarsen@esriuk.com
*   @date: July 2009
*   @description:
*     Static Loading Icon Dijit
*     Accessed globally via ESRIUK.Dijits.loading()
*
*/
(function(){

ESRIUK.Dijits=ESRIUK.Dijits||{};ESRIUK.Dijits.Loading=ESRIUK.Dijits.Loading||{};dojo._loadedModules["ESRIUK.Dijits.Loading"] = ESRIUK.Dijits.Loading;;


var util = ESRIUK.util();
dojo.declare("ESRIUK.Dijits.Loading", [dijit._Widget,dijit._Templated],{
  counter:0,
  templateString:"<div class='mapOverlay semiTransparent loadingIcon mapPanel noDisplay'><img src='${imagePath}ajax-loader.gif' /><span>&nbsp;&nbsp;Loading&nbsp;...&nbsp;</span></div>",
  imagePath: dojo.moduleUrl("ESRIUK.Dijits", "images/Loading/"),
  toggle:function(/*Boolean*/on){
    
    on = ESRIUK.util().isBoolean(on)? on : false;
    if(on){
      this.counter++;
      if(this.counter==1){
        this.domNode.style.display = 'block';
      }
    }else{
      this.counter--;
      if(this.counter<1){
        this.domNode.style.display = 'none';
      }
      if(this.counter<0){
        this.counter = 0;
      }
    }
  }
});

})();

}

if(!dojo._hasResource['ESRIUK.Dijits.Query']){ //_hasResource checks added by build. Do not use _hasResource directly in your code.
dojo._hasResource['ESRIUK.Dijits.Query'] = true;

ESRIUK.Dijits.Query=ESRIUK.Dijits.Query||{};dojo._loadedModules["ESRIUK.Dijits.Query"] = ESRIUK.Dijits.Query;;




//  this reference is assumed
//dojo.require('esri.layers.Layer');


dojo.declare('ESRIUK.Dijits.Query', esri.layers.Layer, (function() {
    //  summary:
    //    
    
    //  instantiate private variables
    var util = ESRIUK.util(),  //  esriuk utility class
        config = ESRIUK.config()  //  esriuk application config
        sConfig = null,  //  local web service config
        jsonHandler = null;  //  reference to JS Object that handles ajax calls to web service
    
    //  main layer class
    return {
        //  loaded: Boolean
        //    has the jsonHandler been loaded
        loaded:false,  
        //  _extentQueries: Number
        //    number of active queryExtent requests     
        _extentQueries:0,
        //  _loading: function
        //    function stub for toggling loading icon
        _loading:function(){return false;},
        //  loading function setter
        setLoading:function(/*function*/fcn){ this._loading = fcn; },        
        //  START: Init methods
        constructor: function(inputConfig,loadHandler) {
            //  summary:
            //    constructor method, called on all dojo widgets
            //    binds local config variable to incoming config settings
            //    adds all required references for this class
            //  note:
            //    method should only be called once (init method)            
            
            //  bind config
            sConfig = inputConfig;  
            this._loading = loadHandler;
            this._loading(true);
            //  checks config values
            this._checkConfig();  
            //  load required handler
            this._loadJSONHandler();
        },
        onLoad:function(){
            
            this.inherited(arguments);
            this._loading(false);            
        },        
        _cleanUp:function(){
          //  summary:
          //    small clean-up function
          this.loaded = false;
          this._extentQueries = 0;
        },
        _checkConfig:function(){
            //  summary:
            //    checks config property for an error strings object property
            //    adds a default value if not present
            if(!util.isObject(sConfig.errorStrings)){
                sConfig.errorStrings = {};
            }
        },
        _load: function(){
            
            this._loaded = true;
            jsonHandler = new TransportDirectQueryWebService();         
        },
        _loadJSONHandler: function() {
          //  summary:
          //    adds javascript proxy class (generated by Jayrock JSON) 
          //    which handles communication between js & web service (ajax)
          //  note:
          //    method should only be called once (init method)
                
          var that = this,
              scope = arguments.callee.nom;
          //  adds required proxy js file to head of HTML document using dojo.io.script
          var jsonpArgs = {
              url: sConfig.url,
              checkString: sConfig.checkString, //  name of object created after file is successfully added
              timeout: 20000,
              load: function() {
                  if(that._loaded){
                      
                  }else{
                      
                      that._load();
                  }
              },
              error: function() {
                  
              }
          };
          dojo.io.script.get(jsonpArgs);
        },
        queryPoint:function(/*esri.geometry.Point*/geom,/*function*/callback){
          //  summary:
          //    custom method to set all relevant settings for travel news layers     
          
          this._loading(true);          
          var that = this,
              scope = arguments.callee.nom,
              methodName = ' jsonHandler.queryPoint ';        
          jsonHandler.queryPoint(geom.x, geom.y, function(obj){
            try{
              var loaded = false;    
              if (util.isObject(obj)) {
                if (util.isObject(obj.result)) {
                  var result = obj.result;
                  if(util.isArray(result.results)){
                    loaded = true;
                    that._queryResults = result.results;
                    callback(result.results);
                    
                  } 
                }
              }
              if (!loaded) {
                
                config.events.queryError(
                  {code:result.errorStatusCode,message:result.errorMessage},
                  obj
                );
              }
            }catch(err){
              config.events.queryError(err,obj);
            }finally{
              that._loading(false);
            }
          });
        },
        getInitFeatures:function(/*esri.geometry.Extent*/extent,/*esri.Map*/map,/*Object*/layers,/*Object?*/filter,/*function*/callback,/*Object*/thisArg){
          //  summary:
          //    method for accessing a set of features located in a particular map extent
          //    generally called in response to an onExtentChange event
          //  returns:
          //    array of point objects
          
          var that = this;
          if(util.isObject(thisArg)){            
            this.getExtentFeatures(
              extent,map,
              layers,filter,
              function(obj){
                callback.call(thisArg,obj);
                that.loaded = true;
                that.onLoad(that);
              },
              null
            );            
          }else{
            this.getExtentFeatures(
              extent,map,
              layers,filter,
              function(obj){
                callback(obj);
                that.loaded = true;
                that.onLoad(that);
              },
              thisArg
            );            
          }   
        },
        getExtentFeatures:function(/*esri.geometry.Extent*/extent,/*esri.Map*/map,/*Object*/layers,/*Object?*/filter,/*function*/callback,/*Object*/thisArg){
          //  summary:
          //    method for accessing a set of features located in a particular map extent
          //    generally called in response to an onExtentChange event
          //  returns:
          //    array of point objects
          
          this._loading(true);
          var that = this,
              //  fix for version 1.4 -> 1.5
              lod = esri.version<1.5? map._LOD: map.__LOD,
              loaded = false,
              scope = arguments.callee.nom,
              methodName = ' jsonHandler.getExtentFeatures ';
          this._extentQueries++;
          filter = util.isNothing(filter)? config.filter.standard : filter;
          jsonHandler.queryExtent(extent.xmin,extent.ymin,extent.xmax,extent.ymax,
            lod.resolution,layers.pointX,layers.stops,layers.carparkLayerVisible,filter,
            function(obj){
              try{
                var numResults = 0;
                if (util.isObject(obj)&&util.isObject(obj.result)&&that._extentQueries<2) {
                  var result = obj.result;
                  if (util.isArray(result.results)) {
                    loaded = true;
                    numResults = result.results.length;
                    if(util.isObject(thisArg)){
                      callback.call(thisArg,result.results);
                    }else{
                      callback(result.results);
                    }
                    if(result.errorStatusCode===650){
                      numResults = -1;
                    }                    
                  }
                }
                config.events.mapGraphicsCount(numResults);
                if (!loaded) {
                  
                  config.events.queryError(
                    {code:result.errorStatusCode,message:result.errorMessage},
                    obj
                  );
                }
              }catch(err){
                config.events.queryError(err,obj);
              }finally{
                that._extentQueries--;
                that._loading(false);
                that._loading(false);
              }
            }
          );
        },
        findJunctionPoint:function(/*String*/TOID_a,/*String*/TOID_b){
          //  summary:
          //    custom method to set all relevant settings for travel news layers     
              
          var that = this,
              scope = arguments.callee.nom,
              methodName = ' jsonHandler.findJunctionPoint ',
              response = jsonHandler.findJunctionPoint(TOID_a,TOID_b);
          if(util.isObject(response)&&util.isValidXY(response)&&response.errorStatusCode==200){
              return util.validateXY(response);
          }else{
              
              throw response;
          }
        },
        findITNNodePoint:function(/*String*/TOID){
          //  summary:
          //    custom method to set all relevant settings for travel news layers     
              
          var that = this,
              scope = arguments.callee.nom,
              methodName = ' jsonHandler.findITNNodePoint ',
              response = jsonHandler.findITNNodePoint(TOID);
          if(util.isObject(response)&&util.isValidXY(response)&&response.errorStatusCode==200){
              return util.validateXY(response);
          }else{
              
              throw dojo.mixin(response,{message:response.errorMessage});
          }
        },
        getTravelNewsInfo:function(id,callback,thisArg){
          
          this._loading(true);          
          var that = this,
              scope = this.declaredClass,
              methodName = ' jsonHandler.getRoadEnvelope ';
          jsonHandler.getTravelNews(id,function(obj){
            try{
              var loaded = false,
                  settings = config.statics;    
              if (util.isObject(obj)&&util.isObject(obj.result)) {
                if (util.isArrayPopulated(obj.result.results)) {
                  var result = obj.result.results[0],
                      title = '', count = null,
                      vals = null, strName  = null,
                      div = dojo.create('div');
                      elements = [],
                      arr = sConfig.roadIncidentQueryFields;
                      caps = function(s){
                        var str1 = s.slice(0,1),
                            str2 = s.slice(1,s.length);
                        return str1.toUpperCase()+str2;
                      };
                  if(util.isStringPopulated(result.type)&&result.type==="TravelNews"&&util.isArrayPopulated(result.values)){
                    vals = result.values;
                    count = vals.length;
                    var tbody = dojo.create('tbody',null,dojo.create('table',{cellpadding:0,cellspacing:0},div));
                    for(var i=0;i<count-2;i++){
                      strName = arr[i];
                      elm = dojo.create('tr',null,tbody);
                      dojo.create('td',{innerHTML:strName+':','class':settings.cssTravelNewsField},elm);
                      dojo.create('td',{innerHTML:vals[i].replace("&apos;",'&#039;'),'class':settings.cssTravelNewsBody},elm);
                    }
                    var e = settings.travelNewsEnum,
                        type = vals[dojo.indexOf(arr,sConfig.roadIncidentTypeField)],
                        planned = util.parseBool(vals[dojo.indexOf(arr,sConfig.roadIncidentPlannedField)]);
                    
                    title = util.parseBool(planned)?type==="Road"?e.pRoad:e.pRail:type==="Road"?e.upRoad:e.upRail;
                    if(util.isObject(thisArg)){
                      callback.call(thisArg,title,[div]);
                    }else{
                      callback(title,[div]);
                    }
                    loaded = true;                    
                  }
                }
              }
              if (!loaded) {
                
                config.events.queryError(
                  {code:obj.result.errorStatusCode,message:obj.result.errorMessage},
                  obj
                );
              } 
            }catch(err){ 
              
              config.events.queryError(err,obj);
            }
            finally{
              that._loading(false);
            }
          });                
        },
        getPointXInfo:function(id,callback,thisArg){
          
          this._loading(true);
          var that = this;
          jsonHandler.getPointX(id,function(obj){
            try{
              var loaded = false,
                methodName = ' jsonHandler.getPointXInfo ';            
              if (util.isObject(obj)&&util.isObject(obj.result)) {
                if (util.isArrayPopulated(obj.result.results)) {
                  var result = obj.result.results[0],
                    elements = [],
                    title = null;
                  if(util.isStringPopulated(result.type)&&util.isArrayPopulated(result.values)){
                    var addrArr = result.values;
                    title = addrArr[0];
                    for(var i=1;i<addrArr.length;i++){
                      if(addrArr[i]!==''){
                        elements.push(dojo.create('span',{innerHTML:addrArr[i].replace("&apos;",'&#039;')}));
                      }
                    }
                    loaded = true;
                    if(util.isObject(thisArg)){
                      callback.call(thisArg,title,elements);
                    }else{
                      callback(title,elements);
                    }
                  }
                }
              }
              if (!loaded) {
                
                config.events.queryError(
                  {code:obj.result.errorStatusCode,message:obj.result.errorMessage},
                  obj
                );
              }
            }catch(err){
              config.events.queryError(err,obj);
            }finally{
              that._loading(false);            
            }
          }); 
        },
        getRouteEnvelope:function(/*String*/sessionId,/*String*/id,/*String*/type){
          //  summary:
          //    ToDo
                       
          var that = this,
              scope = arguments.callee.nom,
              methodName = ' jsonHandler.getRouteEnvelope ',
              response = jsonHandler.getRouteEnvelope(sessionId,id,type);
          if(util.isObject(response)&&util.isValidExt(response)&&response.errorStatusCode==200){
              return util.validateExt(response);
          }else{
              
              throw dojo.mixin(response,{message:response.errorMessage});
          }          
        }
    };
})());

}

if(!dojo._hasResource["ESRIUK.Dijits.MapNavPanel"]){ //_hasResource checks added by build. Do not use _hasResource directly in your code.
dojo._hasResource["ESRIUK.Dijits.MapNavPanel"] = true;




// Provide namespace
ESRIUK.Dijits.MapNavPanel=ESRIUK.Dijits.MapNavPanel||{};dojo._loadedModules["ESRIUK.Dijits.MapNavPanel"] = ESRIUK.Dijits.MapNavPanel;;
//declare new dijit class
dojo.declare("ESRIUK.Dijits.MapNavPanel", [dijit._Widget, dijit._Templated], (function(){

    // summary:
    //    Map navigation panel dijit
    // description:
    //    simple dijit to handle map navigation events
    var util = ESRIUK.util(),
        config = ESRIUK.config();
return {
    _statics:{
        altSuffix:' navigation image',    
        panNorth:'Pan North',
        panEast:'Pan East',
        panSouth:'Pan South',
        panWest:'Pan West',
        fullExtent:'Full Extent'
    },


    // dijit template contains other dijits
    widgetsInTemplate: true,
    // path to template
    templateString:"<div class=\"navPanelContainer\">\r\n    <div class=\"navPanelHolder mapPanel\">\r\n        <div class=\"navPanelItem top centre\" dojoAttachEvent='onclick:_onPanNorth'>\r\n            <img title=\"${_statics.panNorth}\" alt=\"${_statics.panNorth}${_statics.altSuffix}\" src=\"${imagePath}panNorth${navImgType}\" class=\"transparentPNG\" />\r\n        </div>\r\n        <div class=\"navPanelItem middle left\" dojoAttachEvent='onclick:_onPanWest'>\r\n            <img title=\"${_statics.panWest}\" alt=\"${_statics.panWest}${_statics.altSuffix}\" src=\"${imagePath}panWest${navImgType}\" class=\"transparentPNG\" />\r\n        </div>\r\n        <div class=\"navPanelItem middle centrePlus\" dojoAttachEvent='onclick:_onPanFullExtent'>\r\n            <img title=\"${_statics.fullExtent}\" alt=\"${_statics.fullExtent}${_statics.altSuffix}\" src=\"${imagePath}panFullExtent${navImgType}\" class=\"transparentPNG\" />\r\n        </div>\r\n        <div class=\"navPanelItem middle right\" dojoAttachEvent='onclick:_onPanEast'>\r\n            <img title=\"${_statics.panEast}\" alt=\"${_statics.panEast}${_statics.altSuffix}\" src=\"${imagePath}panEast${navImgType}\" class=\"transparentPNG\" />\r\n        </div>\r\n        <div class=\"navPanelItem bottom centre\" dojoAttachEvent='onclick:_onPanSouth' >\r\n            <img title=\"${_statics.panSouth}\" alt=\"${_statics.panSouth}${_statics.altSuffix}\" src=\"${imagePath}panSouth${navImgType}\" class=\"transparentPNG\" />\r\n        </div>\r\n    </div>\r\n</div>\r\n",
    imagePath: dojo.moduleUrl("ESRIUK.Dijits", "images/MapNavPanel/"), 

    // settings for dijit image
    navImgType: '.png',    
    
    constructor:function(){
        try{
            var obj = config.statics.mapNavPanel;
            if(util.isObject(obj)){
                dojo.mixin(this._statics,obj);
            }
        }catch(err){
            
        } 
    },
  
    
    bindEvents: function(/*function*/north,/*function*/south,/*function*/east,/*function*/west,/*function*/full){   
        // summary:
        //    binds external event handlers to event stubs
        // description:
        //    Binds all directional & full extent navigation external handlers to local event stubs
        // returns:
        //    Nothing
        this._onPanNorth = north;
        this._onPanSouth = south;
        this._onPanEast = east;
        this._onPanWest = west;
        this._onPanFullExtent = full;
    },
    // navigation event stubs
    _onPanNorth: function(){ },
    _onPanWest: function(){ },
    _onPanFullExtent: function(){  },
    _onPanEast: function(){  },
    _onPanSouth: function(){  }
    
  };
  })());

}

if(!dojo._hasResource["ESRIUK.Dijits.MapToolBarPanel"]){ //_hasResource checks added by build. Do not use _hasResource directly in your code.
dojo._hasResource["ESRIUK.Dijits.MapToolBarPanel"] = true;






// Provide namespace
ESRIUK.Dijits.MapToolBarPanel=ESRIUK.Dijits.MapToolBarPanel||{};dojo._loadedModules["ESRIUK.Dijits.MapToolBarPanel"] = ESRIUK.Dijits.MapToolBarPanel;;
//declare new dijit class
dojo.declare("ESRIUK.Dijits.MapToolBarPanel", [dijit._Widget, dijit._Templated],(function(){ 

    var util = ESRIUK.util(),
        config = ESRIUK.config();
    
    return {
    // summary:
    //    Map toolbar panel dijit
    // description:
    //    simple dijit to contain tools on toolbar panel
    
    _statics:{
        altSuffix:' tool image',
        panMap:'Pan Map',
        userDefined:'Add A User Defined Location',
        selectNearby:'Select A Nearby Point'
    },    
    
    isShowing:false,
    tools:[],
    
    // dijit template contains other dijits
    widgetsInTemplate: true,
    // path to template
    templateString:"<div class=\"toolbarPanelContainer\" style=\"display:none\">\r\n    <div class=\"toolbarPanelHolder mapPanel\">\r\n        <span class=\"toolbarPanelItem selected default\" dojoAttachPoint=\"attachDefault\">\r\n            <img title=\"${_statics.panMap}\" alt=\"${_statics.panMap}${_statics.altSuffix}\" src=\"${imagePath}default-map-actions${navImgType}\" dojoAttachEvent='onclick:_onDefaultMapActions' class=\"transparentPNG\" />\r\n        </span>\r\n        <span class=\"toolbarPanelItem\" dojoAttachPoint=\"attachUserDefined\">\r\n            <img title=\"${_statics.userDefined}\" alt=\"${_statics.userDefined}${_statics.altSuffix}\" src=\"${imagePath}user-defined-location${navImgType}\" dojoAttachEvent='onclick:_onUserDefinedLocation' class=\"transparentPNG\" />\r\n        </span>                \r\n        <span class=\"toolbarPanelItem\" dojoAttachPoint=\"attachSelectNearby\">\r\n            <img title=\"${_statics.selectNearby}\" alt=\"${_statics.selectNearby}${_statics.altSuffix}\" src=\"${imagePath}select-nearby-point${navImgType}\" dojoAttachEvent='onclick:_onSelectNearbyPoint' class=\"transparentPNG\" />\r\n        </span>        \r\n    </div>\r\n</div>\r\n",
    imagePath: dojo.moduleUrl("ESRIUK.Dijits", "images/MapToolBarPanel/"), 

    // settings for dijit image
    navImgType: '.png',
    
    constructor:function(){
        try{
            var obj = config.statics.mapToolbarPanel;
            if(util.isObject(obj)){
                dojo.mixin(this._statics,obj);
            }
        }catch(err){
            
        }        
    },    
    
    init: function(/*array*/tools){
         
        tools = util.isArrayPopulated(tools)? tools : [];
        var userDefined = false,
            selectNearby = false;
        this.tools = tools;
        for(var i=0;i<tools.length;i++){
            if(tools[i]==='selectNearby'){
                selectNearby = true;
                this.isShowing = true;
            }else if(tools[i]==='userDefined'){
                userDefined = true;
                this.isShowing = true;
            }
        }
        dojo.style(this.domNode,{display:this.isShowing?'block':'none'}); 
        if(!userDefined){ dojo.style(this.attachUserDefined,{display:'none'}); }
        if(!selectNearby){ dojo.style(this.attachSelectNearby,{display:'none'}); }
    },

    bindEvents: function(/*function*/defaultAction,/*function*/userDefined,/*function*/selectNearby){   
        // summary:
        //    binds external event handlers to event stubs
        // description:
        //    Binds all map interaction changes to external methods
        // returns:
        //    Nothing
        this._onDefaultMapActions = defaultAction;
        this._onUserDefinedLocation = userDefined;
        this._onSelectNearbyPoint = selectNearby;
    },
    // toolbar event stubs
    _onDefaultMapActions: function(){ },
    _onUserDefinedLocation: function(){ },
    _onSelectNearbyPoint: function(){  },
    
    getCurrentActiveTool: function(){
        var selected = config.statics.cssSelectedToolClass;
        var arr = dojo.query('.'+config.statics.cssToolItemClass,this.domNode);
        for(var i=0;i<arr.length;i++){
          if(dojo.hasClass(arr[i],selected)){
            return arr[i];
          }
        }
    },
    getDefaultTool: function(){
        return dojo.query('.'+config.statics.cssDefaultToolClass,this.domNode)[0];        
    },
    toggleActiveTool:function(/*DOMElement/String*/tool){
        if(util.isStringPopulated(tool)){
            var settings = config.toolBar.typeEnum;
            if(tool===settings.defaultTool){
                tool = this.attachDefault;
            }else if(tool===settings.userDefinedLocation){
                tool = this.attachUserDefined;
            }else if(tool===settings.selectNearbyPoint){
                tool = this.attachSelectNearby;
            }else{
                tool = dojo.byId(tool);
            }
        }else if(!util.isObject(tool)){
            return false;
        }
        this._resetAllTools();
        dojo.addClass(tool,config.statics.cssSelectedToolClass);
    },
    _resetAllTools:function(){
        var selected = config.statics.cssSelectedToolClass;
        var arr = dojo.query('.'+config.statics.cssToolItemClass,this.domNode);
        for(var i=0;i<arr.length;i++){
          dojo.removeClass(arr[i],selected);
        }
    }
  } })()
);

}

if(!dojo._hasResource['ESRIUK.Dijits.IMSServerStateLayer']){ //_hasResource checks added by build. Do not use _hasResource directly in your code.
dojo._hasResource['ESRIUK.Dijits.IMSServerStateLayer'] = true;
ESRIUK.Dijits.IMSServerStateLayer=ESRIUK.Dijits.IMSServerStateLayer||{};dojo._loadedModules["ESRIUK.Dijits.IMSServerStateLayer"] = ESRIUK.Dijits.IMSServerStateLayer;;




dojo.declare('ESRIUK.Dijits.IMSServerStateLayer', esri.layers.DynamicMapServiceLayer, (function() {
    //  summary:
    //    IMS Proxy layer definition
    //    Handles all communication between base map & web service when an IMSServerStateLayer is present in BaseMap
    //  note:
    //    All calls to web service 'jsonHandler.***' are handled by a jsonHandler proxy class generated by 
    //    Jayrock JSON via ajax
    
    //  instantiate private variables
    var util = ESRIUK.util(),  //  esriuk utility class
        config = ESRIUK.config(),  //  esriuk application config
        jsonHandler = null;  //  reference to JS Object that handles ajax calls to web service
    
    //  main layer class
    return {
        _requestId:0,
        _loaded: false,
        _initRoutes:false,
        _lastScale:null,
        _layers: {},
        _externalConfig:null,
        //  _loading: function
        //    function stub for toggling loading icon
        _loading:function(){return false;},
        //  loading function setter
        setLoading:function(/*function*/fcn){ this._loading = fcn; },             
        ovUrl:'',
        urlTest:null,
        //  START: Init methods
        constructor: function(inputConfig,params,loadHandler) {
            //  summary:
            //    constructor method, called on all dojo widgets
            //    binds local config variable to incoming config settings
            //    adds all required references for this class
            //  note:
            //    method should only be called once (init method)            
            
            this._loading = loadHandler;
            this._loading(true);
            //  clean old params and add new
            this._cleanUp(inputConfig,params);
            this._checkConfig();  //  checks config values
            this._loadJSONHandler();  //  load required handler
        },
        onLoad:function(){
            
            this.inherited(arguments);
            this._loading(false);            
        },
        _cleanUp:function(inputConfig,params){
            //  summary:
            //    Cleans up all old properties
            //    Adds new incoming properties
            // bug fix
            this._requestId = 0;
            this._loaded = false;
            this._lastScale = null;
            this._layers = {};
            this.ovUrl = '';
            this._externalConfig = {
                travelNews:null,
                sumbols:[],
                routes:[]
            };
            //  bind specific params
            if(util.isObject(params.travelNews)){
                inputConfig.travelNews = params.travelNews;
            }
            if(util.isArrayPopulated(params.symbols)){
                inputConfig.symbols = params.symbols;
            } 
            if(util.isArrayPopulated(params.routes)){
                inputConfig.routes = params.routes;
            }
            //  bind config       
            this._externalConfig = inputConfig;        
        },
        _checkConfig:function(){
            //  summary:
            //    checks config property for an error strings object property
            //    adds a default value if not present
            if(!util.isObject(this._externalConfig.errorStrings)){
                this._externalConfig.errorStrings = {
                    no_imageUrl:' Result does not contain valid imageUrl. ',
                    no_errorStatusCode:' Result does not contain valid errorStatusCode. ',
                    no_result:' No valid result was returned. ',
                    no_response:' No valid response was returned. ',
                    invalid_status:' Invalid status code returned. ',
                    invalid_url:' Invalid image url returned. '
                };
            }
            this.urlTest = this._externalConfig.imgUrlPattern || /^http:\/\/.+(\.png|\.jpg|\.gif)$/;
        },
        _load: function(){
            
            var m = esri.config.defaults.map;
            this._loaded = true;
            this._loadInitMap(m.width,m.height);            
        },
        _loadJSONHandler: function() {
            //  summary:
            //    adds javascript proxy class (generated by Jayrock JSON) 
            //    which handles communication between js & web service (ajax)
            //  note:
            //    method should only be called once (init method)
                      
            var that = this,
                scope = arguments.callee.nom;
            //  adds required proxy js file to head of HTML document using dojo.io.script
            var jsonpArgs = {
                url: this._externalConfig.url,//+'&r='+Math.random()*1000,
                checkString: this._externalConfig.checkString, //  name of object created after file is successfully added
                timeout: 50000,
                load: function() {
                    if(that._loaded){
                        
                    }else{
                        
                        that._load();
                    }
                },
                error: function() {
                    
                }
            };
            dojo.io.script.get(jsonpArgs);
        },
        _loadInitMap: function(imageWidth, imageHeight) {
            //  summary:
            //    calls initMap method of web service and gets back init params of map
            //  note:
            //    method should only be called once (init method)        
            
            var that = this,
                scope = arguments.callee.nom,
                methodName = ' jsonHandler.initMap ',
                handler = function(obj){
                    
                    try{
                        if (!util.isNothing(obj)) {
                            if (!util.isNothing(obj.result)) {
                                var result = obj.result;
                                if (util.isBoolean(result.isValid) && result.isValid === true){
                                    that.initialExtent = that.fullExtent = new esri.geometry.Extent({
                                        xmin: result.xmin, ymin: result.ymin,
                                        xmax: result.xmax, ymax: result.ymax,
                                        spatialReference: new esri.SpatialReference({ wkid: config.map.wkid })
                                    });
                                    that.spatialReference = new esri.SpatialReference({ wkid: config.map.wkid });
                                    that._readMapLayers(result);
                                    if(that._initRoutes){
                                        dojo.publish('esriukMap/newInitExtent',[that.initialExtent]);
                                    }
                                    that.loaded = true;
                                    
                                    that.onLoad(that);
                                    
                                }
                            }
                        }
                        if (!that.loaded) {
                            
                            config.events.mapError(
                              {code:result.errorStatusCode,message:result.errorMessage},
                              obj
                            );
                            alert(this._externalConfig.errorStrings.no_map);
                        }
                    }catch(err){
                        config.events.mapError(err,obj);
                    }
                };
            jsonHandler = new TransportDirectMapWebService();
            var settings = this._externalConfig,
                symbols = util.isArrayPopulated(settings.symbols),
                routes = util.isArrayPopulated(settings.routes);
            this._initRoutes = routes;
            if(util.isObject(settings.travelNews)){
                if(symbols){
                    if(routes){
                        jsonHandler.initMap_Sym_TN_R(imageWidth, imageHeight, settings.travelNews, settings.symbols, settings.routes, handler);
                    }else{
                        jsonHandler.initMap_Symbols_TravelNews(imageWidth, imageHeight, settings.travelNews, settings.symbols, handler);
                    }
                }else{
                    if(routes){
                        jsonHandler.initMap_TN_R(imageWidth, imageHeight, settings.travelNews, settings.routes, handler);
                    }else{                
                        jsonHandler.initMapAndTravelNews(imageWidth, imageHeight, settings.travelNews, handler);
                    }
                }
            }else{
                if(symbols){
                    if(routes){
                        jsonHandler.initMap_Sym_R(imageWidth, imageHeight, settings.symbols, settings.routes, handler);
                    }else{                
                        jsonHandler.initMapAndSymbols(imageWidth, imageHeight, settings.symbols, handler);
                    }
                }else{
                    if(routes){
                        jsonHandler.initMap_Routes(imageWidth, imageHeight, settings.routes, handler);
                    }else{                
                        jsonHandler.initMap(imageWidth, imageHeight, handler);
                    }
                }
            }
        },
        //  END: Init methods
        //  START: Public map navigation / interaction methods
        getImageUrl: function(extent, width, height, callback) {
            //  summary:
            //    main map navigation method, overrides inherited method
            //    calls to web service to get a new image url whenever JS API requires it
            //  overrides: DynamicMapServiceLayer.getImageUrl
            //  note:
            //    this method is normally called by the JS API internally
            //    it should not be called by user code, unless complex customisation of the API is needed
            
            var that = this,
                map = this._map,
                //  fix for version 1.4 -> 1.5
                lod = esri.version<1.5? map._LOD: map.__LOD,
                scale = lod.scale,
                settings = config.events,
                scaleChange = this._lastScale==scale? false : true,
                scope = arguments.callee.nom,
                methodName = ' jsonHandler.getImage ';
            this._loading(true);
            this._loading(true);
            this._requestId++;                
            if(scaleChange){ this._lastScale = scale; }
            //  call getImage web service method via proxy class           
            jsonHandler.getImage(extent.xmin, extent.ymin, extent.xmax, extent.ymax, scaleChange,
                function(obj) {
                    
                    try{
                        //  check id of request
                        if(util.isNumber(obj.id)){
                            if(obj.id!==that._requestId){
                                
                                return;
                            }
                        }                    
                        //  check response validity & forward callback method to generic jsonResponseHandler 
                        var response = that._jsonResponseHandler(obj,callback);
                        if (!response.valid) {
                            
                        }
                        settings.onMapExtentChange(map.extent,scaleChange,scale,that.ovUrl);
                    }catch(err){
                        
                    }finally{
                        that._loading(false);
                    }
                }
            );
        },
        addStartEndPt: function(/*point*/pt) {
            //  summary:
            //    custom method to add a point to the map
            //    calls 'addPoint' method on server
            //    expects a normal imageUrl response
            //    creates internal callback function to replace the img.src with the new img url
            
            var that = this,
                scope = arguments.callee.nom,
                methodName = ' jsonHandler.addPoint ';
            //  define custom callback function to replace the image url with retrned value
            var callback = function(value){that._replaceImageUrl(value);};
            this._loading(true);            
            this._requestId++;  
            //  call addPoint web service method via proxy class
            jsonHandler.addPoint(pt,
                function(obj) {
                    
                    //  check response validity & forward callback method to generic jsonResponseHandler                    
                    var response = that._jsonResponseHandler(obj,callback);
                    if (!response.valid) {
                        
                    }
                    that._loading(false);
                }
            );
        },
        clearPoints: function(/*String?*/type) {
            //  summary:
            //    custom method to clear points from the map
            //    calls 'clearPoints' method on server
            //    expects a normal imageUrl response
            //    creates internal callback function to replace the img.src with the new img url        
            
            var that = this,
                scope = arguments.callee.nom,
                methodName = ' jsonHandler.clearPoints ';
            type = util.isStringPopulated(type)? type : '';
            this._loading(true);             
            this._requestId++;  
            //  define custom callback function to replace the image url with retrned value
            var callback = function(value){that._replaceImageUrl(value);};
            //  call clearPoints web service method via proxy class
            jsonHandler.clearPoints(type,
                function(obj) {
                    
                    try{
                    //  check response validity & forward callback method to generic jsonResponseHandler
                    var response = that._jsonResponseHandler(obj,callback);
                    if (!response.valid) {
                        
                    }
                    }catch(err){
                        alert(dojo.toJson(err,true));
                    }finally{
                        that._loading(false);
                    }
                }
            );
        },
        setLayerState: function(/*Object*/layers){
            //  summary:
            //    custom method to set all layer visibilities   
            
            var that = this,
                scope = arguments.callee.nom,
                methodName = ' jsonHandler.setLayerStates ';
            if(util.isObject(layers)){
                dojo.mixin(this._layers,layers);
            }else{
                
                return;
            }
            this._loading(true);             
            this._requestId++;  
            //  define custom callback function to replace the image url with retrned value
            var callback = function(value){that._replaceImageUrl(value);};
            jsonHandler.setLayerStates(layers.pointX,layers.stops,layers.carparkLayerVisible,
                function(obj){
                    
                    try{
                    //  check response validity & forward callback method to generic jsonResponseHandler
                    var response = that._jsonResponseHandler(obj,callback);
                    if (!response.valid) {
                        
                    }
                    }catch(err){
                        alert(dojo.toJson(err,true));
                    }finally{
                    that._loading(false);
                    }
                }
            );
        },
        setTravelNewsLayers:function(/*Object*/settings){
            //  summary:
            //    custom method to set all relevant settings for travel news layers     
            
            var that = this,
                scope = arguments.callee.nom,
                methodName = ' jsonHandler.setTravelNewsLayers ';
            this._loading(true);                 
            this._requestId++;  
            //  define custom callback function to replace the image url with retrned value
            var callback = function(value){that._replaceImageUrl(value);};
            jsonHandler.setTravelNewsLayers(settings,
                function(obj){
                    
                    //  check response validity & forward callback method to generic jsonResponseHandler
                    var response = that._jsonResponseHandler(obj,callback);
                    if (!response.valid) {
                        
                    }
                    that._loading(false);
                }
            );
        },
        getPrintImage:function(/*Number*/width,/*Number*/height,/*Number*/dpi){
            //  summary:
            //        
            
            this._loading(true);
            this._requestId++;  
            var obj = null,
                url = null,
                response = null,
                methodName = ' jsonHandler.getPrintImage ';
            try{
                //  call getPrintImage
                obj = jsonHandler.getPrintImage(width,height,dpi);
                response = this._jsonResponseHandler(
                    {result:{errorStatusCode:200,imageUrl:obj.imageUrl,ovUrl:obj.ovUrl}}, 
                    function(){ return false; }
                );
                if(response.valid){
                    url = obj.imageUrl;
                }else{
                    
                }
            }catch(err){
                
            }finally{
                this._loading(false); 
            }
            return url;
        },
        addRoute:function(/*String*/sessionId,/*String*/id,/*String*/type,/*String*/zoomTo){
          //  summary:
          //    ToDo
                  
          this._loading(true);
          this._requestId++;       
          var that = this,
              scope = arguments.callee.nom,
              methodName = ' jsonHandler.addRoute ',
              response = jsonHandler.addRoute(sessionId,id,type,zoomTo);
          this._loading(false);
          if(util.isObject(response)&&util.isValidExt(response)&&response.errorStatusCode==200){
              return util.validateExt(response);
          }else{
              
              throw dojo.mixin(response,{message:response.errorMessage});
          }          
        },
        clearRoutes:function(/*String?*/type){
          //  summary:
          //    Clears all routes from map and replaces map image with new image
          //    If type is given, only that type will be cleared
              
          this._loading(true); 
          this._requestId++;       
          var that = this,
              response = {valid:false},
              scope = arguments.callee.nom,
              methodName = ' jsonHandler.clearRoutes ',
              obj = jsonHandler.clearRoutes(util.isStringPopulated(type)?type:''),
              //  define custom callback function to replace the image url with retrned value
              callback = function(value){that._replaceImageUrl(value);};  
          this._loading(false);  
          if(util.isObject(obj)&&obj.errorStatusCode==200){
            //  check response validity & forward callback method to generic jsonResponseHandler          
            response = that._jsonResponseHandler({result:obj},callback);
          }
          if(!response.valid){
              
              throw dojo.mixin(response,{message:response.errorMessage});
          }
        },
        zoomToAllAddedRoutes:function(){
          //  summary:
          //    Zooms to all added PT & Cycle routes
              
          this._loading(true); 
          this._requestId++;       
          var that = this,
              response = {valid:false},
              scope = arguments.callee.nom,
              methodName = ' jsonHandler.zoomToAllAddedRoutes ';

          obj = jsonHandler.zoomToAllAddedRoutes();
          this._loading(false); 
          if(util.isObject(obj)&&util.isValidExt(obj)&&obj.errorStatusCode==200){
              return util.validateExt(obj);
          }else{
              
              throw dojo.mixin(obj,{message:obj.errorMessage});
          }  

        },
        getNumberOfCycleImages:function(/*Number*/scale){
          //  summary:
          //    ToDo
            
          this._loading(true); 
          this._requestId++;       
          var that = this,
              scope = arguments.callee.nom,
              methodName = ' jsonHandler.getNumberOfCycleImages ',
              response = jsonHandler.getNumberOfCycleImages(scale);
          this._loading(false); 
          if(util.isObject(response)&&response.errorStatusCode==200&&util.isNumber(response.number)){
              return util.parseNumber(response.number);
          }else{
              
              throw dojo.mixin(response,{message:response.errorMessage});
          }  
        },
        getCyclePrintDetails:function(/*Number*/scale){
          //  summary:
          //    ToDo
                  
          this._loading(true); 
          this._requestId++;
          var that = this,
              scope = arguments.callee.nom,
              methodName = ' jsonHandler.getCyclePrintInfo ',
              response = jsonHandler.getCyclePrintInfo(scale);
          this._loading(false); 
          if(util.isObject(response)&&response.errorStatusCode==200&&util.isArrayPopulated(response.cyclePrintInfo)){
              return response.cyclePrintInfo;
          }else{
              
              throw dojo.mixin(response,{message:response.errorMessage});
          }  
        },
        toggleLayer: function(layerId,on){
            var settings = this._externalConfig.layers,
                layers = this._layers;
            if(layerId===settings.carparks){
                layers.carparkLayerVisible = on;
            }else if(layerId.indexOf(settings.pointX)>-1){
                var name = layerId.split(settings.pointX)[1],
                    arr = layers.pointX;
                for(var i=0;i<arr.length;i++){
                    if(arr[i].name===name){
                        arr[i].visible = on;
                        break;
                    }
                }
                layers.pointX = arr;
            }else if(layerId.indexOf(settings.stops)>-1){
                var name = layerId.split(settings.stops)[1],
                    arr = layers.stops;
                for(var i=0;i<arr.length;i++){
                    if(arr[i].name===name){
                        arr[i].visible = on;
                        break;
                    }
                }
                layers.stops = arr;
            }
            this.setLayerState(layers)
        },        
        //  END: Public map navigation / interaction methods
        //  START: Private map navigation / interaction methods
        _replaceImageUrl:function(url){
            //  summary:
            //    util method to replace image url with new value returned from web service
            //    use this method when no JS API callback method is provided to manually change the image displayed
            
            //  test incoming url
            if(util.isStringPopulated(url)&&this.urlTest.test(url)){
                //  change the src attribute of the html image representing this layer
                this._img.src = url;
            }else{
                
            }
        },
        _jsonResponseHandler:function(obj,callback){
            //  summary:
            //    generic util method, used to attempt to read a valid jsonHandler response
            //    checks if there is a valid response object & if it has the right parameters
            //    if it does, will then call the callback method (supplied by the JA API - via the local calling method)
            //    returns validity response & error message if needed
            //  note:
            //    expects return object from web service to have a property called result - this maps to the object returned in web service code
            //    result object should look like:
            //    { errorMessage: *STRING* , errorStatusCode: *INTEGER*, imageUrl: *STRING* }
            var success = false,
                message = '',
                errors = this._externalConfig.errorStrings;
            try{
                //  checks if there is an actual json object sent back
                if (!util.isNothing(obj)) {
                    var result = obj.result;
                    //  checks if there is a property called 'result' in the response object
                    if (!util.isNothing(result)) {
                        //  checks if ther is a valid error status code
                        //  errorStatusCode 200
                        if (util.isNumber(result.errorStatusCode) && result.errorStatusCode == 200) {
                            //  checks if the imageUrl string is populated
                            if (util.isStringPopulated(result.imageUrl)&&this.urlTest.test(result.imageUrl)) {
                                //  sets return object to valid response
                                success = true;
                                //  check for & set overview image url
                                if(util.isStringPopulated(result.ovUrl)&&this.urlTest.test(result.ovUrl)){
                                    this.ovUrl = result.ovUrl;
                                }
                                //  calls callback method supplied by the API
                                callback(result.imageUrl);
                            }else{ message += errors.no_imageUrl; }
                        }else{
                            //  if a genuine error response has been returned from web service, dump to console
                            if(util.isNumber(result.errorStatusCode)){
                                //  dumps out meaningful error message returned from web service to console
                                message += '{str}, Status code:{code}, Error message:{msg} '.supplant(
                                    {str:errors.invalid_status,code:result.errorStatusCode,msg:result.errorMessage}
                                );
                            }else{
                                message += errors.no_errorStatusCode; 
                            }
                        }
                    }else{ message += errors.no_result; }
                }else{ message += errors.no_response; }
            }catch(err){
                //  will be thrown when the properties of 'config.errorStrings' are invalid
                message += ' Error determining response status. '+(util.isStringPopulated(err.message)?err.message:''); 
            }
            return { valid: success, message: message };
        },
        _readMapLayers: function(result){
            //  summary:
            //    this method reads the initMap result layers arrays
            //    populates local layers variables with those coming from the server
            
            try{
                //  read pointX layers
                var ptX = [], stops = [],
                    keyArr = result.pointXVisible[0],
                    valArr = result.pointXVisible[1];
                for(var i=0;i<keyArr.length;i++){
                    ptX.push({name:keyArr[i],visible:valArr[i]});
                }
                //  read stops layer
                keyArr = result.stopsVisible[0];
                valArr = result.stopsVisible[1];  
                for(var i=0;i<keyArr.length;i++){
                    stops.push({name:keyArr[i],visible:valArr[i]});
                }             
                //  add to local layer property
                dojo.mixin(this._layers,{
                    carparkLayerVisible : result.carparkLayerVisible,  //  read carparks layer visibility
                    pointX: ptX,
                    stops: stops
                });
            }catch(err){
                
            }
        }
    };
})());

}

if(!dojo._hasResource['ESRIUK.Dijits.MapBase']){ //_hasResource checks added by build. Do not use _hasResource directly in your code.
dojo._hasResource['ESRIUK.Dijits.MapBase'] = true;
//  Core dojo references






//  ESRI references
//  This section excludes the esri modules from the build process
//  ESRI (UK) references








ESRIUK.Dijits.MapBase=ESRIUK.Dijits.MapBase||{};dojo._loadedModules["ESRIUK.Dijits.MapBase"] = ESRIUK.Dijits.MapBase;;
dojo.declare('ESRIUK.Dijits.MapBase',[dijit._Widget,dijit._Contained],(function(){
  //  summary:
  //    Main base map class
  //    Handles nuts & bolts of interacting with underlying esri map dijit
  //    and map, web, query & geometry services
  //    Handles  window resizing, data queries, managing map layers etc...

  
  var util = ESRIUK.util(),
      config = ESRIUK.config(),
      connectTypes = { graphics: 'graphics', drawing: 'drawing', extentQuery: 'extentQuery', perm: 'permanent' }; 
 
  return {
    loaded:false,
    _map:null,
    _navBar:null,
    _filter:null,
    _connects:[],
    _graphics:[],
    _initGraphics:[],
    _userGraphics:[],
    _toolBar:null,
    _mainPoint:null,
    _resizeSet:false,
    _drawToolBar:null,
    _geomService:null,
    _queryTimer:null,
    _sliderHandle:null,
    _resizeTimer: null,
    _scrollTimer: null,
    _queryService:null,
    _queryResults:null,
    _loadingHandler:null,
    _baseMapServiceId:null,
    _ie6ResizeTrigger:true,    
    _graphcisMapServiceId:null,
    _disable:{
      extentChange:true
    },
    _externalConfig:{},
    imagesPath: dojo.moduleUrl("ESRIUK", "images/"),    
    //  START: Property Setters
    setMap:function(/*esri.map*/map){
      if(typeof map == 'object'){
        if(typeof map.declaredClass == 'string'){
          if(map.declaredClass == "esri.Map"){
            this._map = map;
          }
        }
      }
    },
    setGeomService:function(service){
      if(util.isNothing(this._geomService)){
        if(typeof service === 'object'){
          if(typeof service.declaredClass === 'string'){
            if(service.declaredClass === ''){
              //  ToDo: fill this setter
            }
          }
        }
      }
    },
    setDrawingToolBar:function(toolBar){
      if(util.isObject(toolBar)){
        this._drawToolBar = toolBar;
      }
    },
    setGraphicsMapService:function(/*int*/layerId){
      if(util.isStringPopulated(layerId)){
        this._graphcisMapServiceId = layerId;
      }
    },
    setBaseMapService:function(/*int*/layerId){
      if(util.isStringPopulated(layerId)){
        this._baseMapServiceId = layerId;
      }
    },
    setQueryService:function(/*ESRIUK.Dijits.Query*/handler){
      if(util.isObject(handler)&&handler.declaredClass==="ESRIUK.Dijits.Query"){
        this._queryService = handler;
      }
    },
    //  END: Property Setters
    //  START: Property Getters
    getInputText:function(){ return this._externalConfig.text; },
    getMap:function(){ return this._map; },
    getDrawingToolBar:function(){ return this._drawToolBar; },
    getQueryService:function(){
      if(util.isNothing(this._queryService)){
        try{
          this._queryService = new ESRIUK.Dijits.Query();
        }catch(err){
          
          this._queryService = null;
        }
      }
      return this._queryService;
    },
    getGeomService:function(){
      if(util.isNothing(this._geomService)){
        try{
          this._geomService = this.getServiceSetting(config.webServiceTypeEnum.geom);
        }catch(err){
          
          this._geomService = null;
        }
      }
      return this._geomService;
    },
    getBaseMapSetting:function(){
      var services = config.mapServices;
      var count = services.length;
      for(var i=0;i<count;i++){
        if(services[i].baseMap){
          return services[i];
        }
      }
      return null;
    },
    getBaseMapService:function(){
      var mService = null;
      if(!util.isNothing(this._graphcisMapServiceId)){
        mService = this.getMap().getLayer(this._baseMapServiceId);
        if(mService===null){
          
        }
      }
      return mService;
    },
    getGraphicsMapService:function(){
      var mService = null;
      if(!util.isNothing(this._graphcisMapServiceId)){
        mService = this.getMap().getLayer(this._graphcisMapServiceId);
        if(mService===null){
          
        }
      }
      return mService;
    },
    getOtherMapSettings:function(){
      var returnServices = [];
      var services = config.mapServices;
      var count = services.length;
      for(var i=0;i<count;i++){
        if(!services[i].baseMap){
          returnServices.push(services[i]);
        }
      }
      return returnServices;
    },
    getServiceSetting:function(/*webServiceTypeEnum*/enumType){
      var services = config.webServices;
      var count = services.length;
      for(var i=0;i<count;i++){
        if(services[i].type === enumType){
          return services[i];
        }
      }
      return null;
    },
    getOverviewUrl:function(){
      var mService = this.getBaseMapService();
      return util.isStringPopulated(mService.ovUrl)? mService.ovUrl : '';
    },
    _getAddPointPrefixText:function(){
      return util.isStringPopulated(this._externalConfig.text)? this._externalConfig.text : config.statics.planAJourneyText.addPointPrefix;
    },
    //  END: Property Getters    
    //  START: Util Methods
    removeConnectTypes:function(/*connectTypes*/type){
      var arr = this._connects,
          arrLength = arr.length;
      for(var i=0;i<arrLength;i++){
              
        if(arr[i].name===type){
          
          dojo.disconnect(arr[i].handle);
                    
          arr.splice(i,1);
          arrLength--;
          i--;
        }
        //

      }
    },
    toggleEvt:function(/*string*/evtToggleEnumVal,/*boolean*/on){
      on = (typeof on === "boolean"? on : true);
      var dis = this._disable;
      if(evtToggleEnumVal===connectTypes.extentQuery){
          dis.extentChange = on;
      }
    },
    checkMapSpatialRef:function(map){
      if(util.isObject(map.spatialReference)){
        if(util.isNothing(map.spatialReference.wkid)){
          if(map.spatialReference.wkt.indexOf('British_National_Grid')>-1){
            map.spatialReference.wkid = 27700;
          }
        }
      }
    },
    //  END: Util Methods    
    //  START: dijit._Widget overridable methods
    constructor:function(){
      //  summary:
      //    overridden dijit._Widget methos
            
      //  bug fix
      this._externalConfig = {};
    },
    postCreate:function(){
      //  summary:
      //    overridden dijit._Widget method
      //    Sets some global esri config values before creating a map
      
      this.inherited(arguments);
      if(util.isNumber(dojo.isIE) && dojo.isIE < 7) {
        //  Fixes for IE6
        //    to help with map performance
        //    remove any animation when panning / zooming map
        esri.config.defaults.map.zoomDuration = 0;
        esri.config.defaults.map.panDuration = 0;
      }
      esri.config.defaults.map.slider = config.map.slider;  //  bind to config settings
    },
    //  END: dijit._Widget overridable methods
    //  main init method
    init:function(inputConfig){
      //  summary:
      //    main init method
      //    called by containing class with input params (style & map)
      
      try{
        //  apply config gathered from url / div
        this._applyPreLoadConfig(inputConfig);
        //  next 3 methods are self explanatory
        this._addBaseMap();
        this._addOtherMaps();
        this._addGeomService();
        this._addOtherServices();
      }catch(err){
        
        config.events.miscError({message:this.declaredClass+'.'+arguments.callee.nom+' - '+err.message,error:err});
      }
    },
    //  collection of init methods
    _applyPreLoadConfig:function(inputConfig){
      
      //  summary:
      //    applies width & height settings gathered from url/div to map
      //    overwrites default config
      
      //  bug fix
      this._externalConfig = {};
      this._filter = null;
      //  end bug fix
      //  apply map dimension settings
      if(typeof inputConfig==="object"){
        if(util.isNumber(inputConfig.width)){
          dojo.style(this.domNode,{width:inputConfig.width+"px"});
          esri.config.defaults.map.width = inputConfig.width;
        }
        if(util.isNumber(inputConfig.height)){
          dojo.style(this.domNode,{height:inputConfig.height+"px"});
          esri.config.defaults.map.height = inputConfig.height;
        }
      }
      //  assign incoming config to local _externalConfig property
      dojo.mixin(this._externalConfig,inputConfig);
      
      dojo.subscribe('esriukMap/newInitExtent',this,'_onNewLoadExtent');
    },
    _getLoadConfig:function(){
      //  summary:
      //    applies config style values after map is loaded
         
      var configExtent = null,
          pt = null,
          level = null,
          inputConfig = this._externalConfig;
      //  type check registered config values
      if(util.isValidXY(inputConfig.location)){
        var xy = util.validateXY(inputConfig.location);
        pt = new esri.geometry.Point(xy.x, xy.y, new esri.SpatialReference({wkid:config.map.wkid}));
        if(util.isNumber(inputConfig.scale)){
          configExtent = this._generateExtentFromPt(pt,inputConfig.scale,false);
        }else if(util.isNumber(inputConfig.level)){
          configExtent = this._generateExtentFromPt(pt,inputConfig.level,true);
        }else{
          configExtent = this._generateExtentFromPt(pt,5,true);
        }
      }else if(util.isValidExt(inputConfig.extent)){
        var ex = inputConfig.extent;
        configExtent = new esri.geometry.Extent(ex.xmin,ex.ymin,ex.xmax,ex.ymax, new esri.SpatialReference({wkid:config.map.wkid}));
      }
      return configExtent;      
    },
    _addBaseMap:function(){   
      //  summary:
      //    looks for base map in config
      //    creates new map dijit, uses base map settings if needed
      //    creates new map service layer
      //    adds an 'onLoad' event to map
      //    sets map property and adds layer to map
              
      var map = null,
          params = { displayGraphicsOnPan:false },
          mService = null,
          baseMap = this.getBaseMapSetting(),
          msEnum = config.mapServiceTypeEnum,
          inputConfig = this._externalConfig,
          configExtent = this._getLoadConfig(),
          imsParams = {};
          
      if(baseMap!==null){
        //  if config has lods - apply them to map
        if(util.isArrayPopulated(config.map.lods)){
          dojo.mixin(params,{lods:config.map.lods});
        }
        //  if incoming extent defined - apply it to map
        if(util.isObject(configExtent)){
          dojo.mixin(params,{extent:configExtent});
          this._externalConfig.extent = configExtent;
        }
        //  if travel news is set - send to IMS layer
        if(util.isObject(inputConfig.travelNews)){
          this._filter = inputConfig.travelNews;
          dojo.mixin(imsParams,{travelNews:this._filter});
        }
        //  if symbols defined - send to IMS layer
        if(util.isArrayPopulated(inputConfig.symbols)){
          dojo.mixin(imsParams,{symbols:inputConfig.symbols});
        }
        //  if routes defined - send to IMS layer
        if(util.isArrayPopulated(inputConfig.routes)){
          dojo.mixin(imsParams,{routes:inputConfig.routes});
        }        
        //  generate map
        map = new esri.Map(this.domNode.id,params);
        dojo.connect(map,'onLoad',function(){
            this.disableDoubleClickZoom();
            dojo.connect(this,'onDblClick',function(evt){ this.centerAt(evt.mapPoint); dojo.stopEvent(evt); return false; });        
        });
        //  determine which map type it is
        switch(baseMap.type){
          case msEnum.dynamic:       
            mService = new esri.layers.ArcGISDynamicMapServiceLayer(baseMap.url);
          break;
          case msEnum.tiled:       
            mService = new esri.layers.ArcGISTiledMapServiceLayer(baseMap.url);
          break;
          case msEnum.imsserver:
            mService = new ESRIUK.Dijits.IMSServerStateLayer(baseMap,imsParams,this._loadingHandler);
          break;
          default:
            //  if no baseMap is found - log error
            
          break;
        }
        //  add handler to map, to add events once map has finished loading
        this._connects.push(util.formatConnect(dojo.connect(map,"onLoad",this,this._setHandlers),"init"));
        //  set map property
        this.setMap(map); 
        //  add base map service to map
        map.addLayer(mService);
        //  check if you can add graphics to this map service
        if(!util.isNothing(baseMap.addGraphics)){
          //  if so, this is the 'addable graphics' map service
          this.setGraphicsMapService(util.isNothing(mService.id)?0:mService.id);
        }
        //  set base map service
        this.setBaseMapService(mService.id);
      }else{
        
      }
    },
    _addOtherMaps:function(){
      
      //  summary
      //    gets all other maps from mapService array and adds to map
      var mService = null,
          map = this.getMap(),
          otherMaps = this.getOtherMapSettings(),
          count = otherMaps.length;
      for(var i=0;i<count;i++){
        if(otherMaps[i].type=="dynamic"){
          mService = new esri.layers.ArcGISDynamicMapServiceLayer(otherMaps[i].url);
        }else if(otherMaps[i].type=="tiled"){
          mService = new esri.layers.ArcGISTiledMapServiceLayer(otherMaps[i].url);
        }else{
          continue;
        }
        map.addLayer(mService);
      }
    },
    _addGeomService:function(){
           
      //  summary:
      //    looks for geometry service url in config webServices settings
      //    if found, use url to set up geometry service
      var gSettings = this.getServiceSetting(config.webServiceTypeEnum.geom);
      if(!util.isNothing(this._geomService) && !util.isNothing(gSettings)){
        var gService = new esri.tasks.GeometryService(gSettings.url);
        this.setGeomService(gService);
      }    
    },
    _addOtherServices:function(){
           
      //  summary:
      //    looks for service url in config webServices settings
      
      //  add Query Service
      var qSettings = this.getServiceSetting(config.webServiceTypeEnum.query);
      if(util.isObject(qSettings)){
        this.setQueryService(new ESRIUK.Dijits.Query(qSettings,this._loadingHandler));
      }   
    },
    //  collection of post-init methods
    _setHandlers: function(){
      //  summary: 
      //    Adds event handlers after map has been initiated
           
      var map = this.getMap();
      try{
        //  check if map should use a window resize event to resize
        if(config.map.useResizeHandler){
          this._connects.push(util.formatConnect(dojo.connect(window,'onresize',this,this.onMapResize),connectTypes.perm));
        }
        //  add required graphics & map event handlers
        this._connects.push(
          util.formatConnect(dojo.connect(map.graphics,'onClick',this,this.onGraphicClick),connectTypes.graphics),
          //  util.formatConnect(dojo.connect(map.graphics,"onMouseOver",this,this.onGraphicMouseOver),connectTypes.graphics),
          util.formatConnect(dojo.connect(window,'onscroll',this,this.onScroll),connectTypes.perm),
          //  util.formatConnect(dojo.connect(map,'onPanStart',this,this.onMapPanStart),connectTypes.perm),
          util.formatConnect(dojo.connect(map,"onExtentChange",this,this.onMapExtentChange),connectTypes.extentQuery)
        );
        //  call subsequent methods
        this._applyPostLoadChanges();
        this._runPostLoad();
        this._onLoad();
      }catch(err){
        config.events.miscError({message:this.declaredClass+'.'+arguments.callee.nom+' - '+err.message,error:err});
      }
    },
    _applyPostLoadChanges:function(){
      //  summary:
      //    applies final changes before init procedure is finished
           
      var map = this.getMap(),
          settings = config.map,
          slider = map._slider,
          node = slider.domNode,
          arr = dojo.query('div.dijitRuleMarkV',node),
          count = arr.length,
          item,elm,bottom,str,div,lods = settings.lods,
          handle = slider.sliderHandle,
          lod = esri.version<1.5? map._LOD : map.__LOD,
          par = arr[0].parentNode,
          pad = settings.sliderBoxPadding,
          boxLevel = settings.sliderBoxStart-1;
      try{  
        //  re-places zoom slider from in map div, to outside map div
        //  this helps with styling the element
        dojo.place(node,this.getParent().attachMapSliderContent);
        //  assign local reference to slider handle
        this._sliderHandle = handle;    
        //  bug fix
        slider.scrollOnFocus = false;
        //  set initial tooltip text for slider handle
        if(util.isObject(handle)){
          handle.title = 'Scale = 1:'+dojo.number.format(lod.scale,"#,##0.###");
          handle.alt = handle.title;
        }
        //  generate box around bottom section
        str = '-'+pad+'px';
        div = dojo.create('div',{'class':'sliderBottomBox',style:{left:str,right:str,top:str}},par,'first');
        //  add tooltips for slider marks         
        for(var i=0, j=lods.length-1;i<count;i++,j--){
          item = arr[i];  
          if(i==boxLevel){
            //  set bottom px value for box
            var coords = dojo.coords(item);
            dojo.style(div,{height:(coords.t+(pad*2))+'px'});
          }
          //  set tooltip text for item
          str = dojo.number.format(lods[j].scale,"#,##0.###");
          item._val = lods[j].level;
          item.title = 'Scale 1:'+str;
          item.alt = item.title;
          dojo.connect(item,'onclick',function(){ map.setLevel(this._val); });
        }
         
      }catch(err){
        config.events.error({message:this.declaredClass+'.'+arguments.callee.nom+' failed to set slider tooltips',obj:err});
      }
      
      //  is isIE, needs to add sliderOverlap number to height of slider container
      //  IE does not respect the pixel value set for slider height
      if(util.isNumber(dojo.isIE)){
        var height = parseInt(dojo.style(this.getParent().attachMapSliderContent,'height'),10);
        if(isNaN(height)||height<2){
          height = parseInt(settings.slider.height,10);
        }
        dojo.style(this.getParent().attachMapSliderContent,{height:(height+settings.sliderIEOverlap)+'px'});
      }
      //  adds a class name to the infoWindow - necessary for styling the window
      dojo.addClass(map.infoWindow.domNode,config.statics.cssInfoWindowClassName);
      //  must call a reposition after all settings have been applied
      //  this is a bug fix
      map.reposition();
    },
    _runPostLoad:function(){
      //  summary:
      //    Runs final misc post loading code
      
      var arr,item,count,
          map = this.getMap(),
          geo = esri.geometry,
          gLayer = map.graphics,
          ref = map.spatialReference,
          qService = this.getQueryService();      
      //  add input symbols if they have been set
      if(util.isArrayPopulated(this._externalConfig.symbols)){
        arr = this._externalConfig.symbols;
        count = arr.length;
        //  run through array and add as map graphics
        for(var i=0;i<count;i++){
          item = arr[i];
          if(util.isBoolean(item.main)){
            var geom = new geo.Point(util.parseNumber(item.x),util.parseNumber(item.y),ref);
            //  store the main point for bookmarking later
            this._mainPoint = gLayer.add(
              new esri.Graphic(geom,config.graphics.pointMain,{text:item.label})
            );
            //  generate an info window for it
            this.drawCallout(
              {screenPoint:esri.geometry.toScreenGeometry(map.extent,map.width,map.height,geom)},
              map,this._mainPoint,geom
            );
          }
        }
      }
      //  attempt to add extent features to map
      if(qService!==null){
        qService.getInitFeatures(
          map.extent,
          map,
          this.getBaseMapService()._layers,
          this._filter,
          this._plotMapFeatures,
          this
        );
      }else{
        config.events.miscEror({message:this.declaredClass+'.'+arguments.callee.nom+' - Failed to load Query Service'});
      }
    },
    _onNewLoadExtent:function(ext){
      
      this._externalConfig.extent = ext;
    },
    _onLoad:function(){
      
      try{
        var map = this.getMap(),
            ext = this._externalConfig.extent,
            mExt = map.extent,
            baseMap = this.getBaseMapService(),
            qService = this.getQueryService();
        if(baseMap.loaded&&qService.loaded){
          this.loaded = true;
          //map.graphics.add(new Graphic(ext,new esri.symbol.SimpleFillSymbol()));   
          if(typeof ext!=='undefined'&&(mExt.getHeight()<ext.getHeight()||mExt.getWidth()<ext.getWidth())){
            
            //map.graphics.add(new esri.Graphic(this._externalConfig.extent,new esri.symbol.SimpleFillSymbol()));
            map.setExtent(ext,true);
            setTimeout(function(){dojo.publish(config.events.mapLoad);},1000);
          }else{
            dojo.publish(config.events.mapLoad);
          }        
        }else if(baseMap.loaded&&(!qService.loaded)){
          dojo.connect(qService,'onLoad',this,this._onLoad);
        }else if((!baseMap.loaded)&&qService.loaded){
          dojo.connect(baseMap,'onLoad',this,this._onLoad);
        }else{
          dojo.connect(qService,'onLoad',this,this._onLoad);
          dojo.connect(baseMap,'onLoad',this,this._onLoad);
        }
      }catch(err){
        config.events.error({message:'error firing final onMapInitialiseComplete method',obj:err});
      }
    },
    //  panel specific event binding methods
    bindNavControls: function(/*ESRIUK.Dijits.MapNavPanel*/navPanel) {
      // summary: 
      //      binds overlay navigation controls.
      // description:
      //      connects navigation dijit to map events
      // returns:
      //    Nothing
        
      var map = this.getMap(),
          that = this;
      if (!util.isNothing(navPanel) && !util.isNothing(map)) {
        var nav = this._navBar = new esri.toolbars.Navigation(map),
            handler;
            
        if(util.isValidExt(this._externalConfig.extent)){
          handler = function(){ map.setExtent(that._externalConfig.extent,true);};
        }else{
          handler = function(){ nav.zoomToFullExtent(); }
        }
        navPanel.bindEvents(
              function() { map.panUp(); },
              function() { map.panDown(); },
              function() { map.panRight(); },
              function() { map.panLeft(); },
              handler
          );
      } else {
        this._navBar = null;
        
      }
    },
    bindToolbar:function(/*ESRIUK.Dijits.MapToolBarPanel*/toolPanel){
      // summary: 
      //      binds overlay toolbar controls.
      // description:
      //      connects toolbar dijit to map events
      // returns:
      //    Nothing
        
      if (!util.isNothing(toolPanel)) {
        this._toolBar = toolPanel;
        if(toolPanel.isShowing){ 
          var that = this,
              settings = config.toolBar.typeEnum;
          toolPanel.bindEvents(
                function(evt) { that._setMapNavigationState(evt,settings.defaultTool); },
                function(evt) { that._setMapNavigationState(evt,settings.userDefinedLocation); },
                function(evt) { that._setMapNavigationState(evt,settings.selectNearbyPoint); }
            );
        }
      } else {
        this._toolBar = null;
        
      }
    },
    bindLoading:function(/*ESRIUK.Dijits.Loading*/loading){
      //  summary:
      //    Assigns a global-style loading handler to all components that need access
      //  params:
      //    loading = instance of a loading icon dijit with method 'toggle' which takes a boolean param
      //  returns:
      //    nothing
      this._loadingHandler = function(obj){ loading.toggle(obj); };
    },
    setActiveTool:function(/*String*/type){
      // summary: 
      //      Incoming API Call
      // description:
      //      connects toolbar dijit to map events
      // returns:
      //    Nothing
           
      if(util.isStringPopulated(type)){
        var settings = config.toolBar.typeEnum;      
        if(type===settings.defaultTool){
          this._setMapNavigationState(settings.defaultTool);
        }else if(type===settings.userDefinedLocation){
          this._setMapNavigationState(settings.userDefinedLocation);
        }else if(type===settings.selectNearbyPoint){
          this._setMapNavigationState(settings.selectNearbyPoint);
        }else{
          
        }
      }else{
        
      }
    },
    _setMapNavigationState:function(/*dojo.event*/evt,/*String*/type){
      //  summary:
      //    changes a toolbar action based on user click on tool item
      //    sets up appropriate events & map navigation mode
      
      //  check for single incoming string parameter
      //  this is used by incoming API call
      if(util.isStringPopulated(evt)&&util.isNothing(type)){
        //  get type string from first parameter
        type = evt;
        //  generate dummy DOM structure, this will pass a string into the 
        //  'toggleActiveTool' method
        evt = {target:{parentNode:type}};
      }
      //  type check if action type was passed in
      if(util.isStringPopulated(type)){
        var map = this.getMap(),
            that = this,
            drawBar = this.getDrawingToolBar(),
            settings = config.toolBar.typeEnum;
        //  get reference to esri drawing toolbar
        if(!util.isObject(drawBar)){
          drawBar = new esri.toolbars.Draw(map);
          this.setDrawingToolBar(drawBar);
        }
        //  set current active tool to the one that was clicked
        this._toolBar.toggleActiveTool(evt.target.parentNode);
        //  remove any current event handlers for drawing actions
        this.removeConnectTypes(connectTypes.drawing);
        //  filter type
        switch(type){
          case settings.defaultTool:
            //  deactivate all drawing toolbar actions
            drawBar.deactivate();
            //  re-enstate default map navigation
            map.enableMapNavigation();
          break;
          case settings.userDefinedLocation:
            //  deactivate map navigation
            map.disableMapNavigation();
            //  add handler for drawing toolbar 'onDrawEnd' and activate toolbar
            that._connects.push(util.formatConnect(dojo.connect(drawBar,'onDrawEnd',function(geom){that._toolAddUserDefinedPoint(geom);}),connectTypes.drawing));
            drawBar.activate(esri.toolbars.Draw.POINT);
          break;
          case settings.selectNearbyPoint:
            //  deactivate map navigation
            map.disableMapNavigation();
            //  add handler for drawing toolbar 'onDrawEnd' and activate toolbar
            that._connects.push(util.formatConnect(dojo.connect(drawBar,'onDrawEnd',function(geom){that._toolSelectNearbyPoints(geom);}),connectTypes.drawing));
            drawBar.activate(esri.toolbars.Draw.POINT);
          break;
          default: 
            
          break;
        }
      }else{
        
      }
    },
    _toolAddUserDefinedPoint:function(geom){
      //  summary:
      //    
      
      //  type / validity checking
      if(util.isStringPopulated(geom.type) && geom.type==='point'){
        var that = this,
            txtBox = null,
            okClicked = false,
            scope = arguments.callee.nom,
            settings = config.statics,
            map = this.getMap(),
            arr = that._userGraphics,
            win = map.infoWindow,
            screenGeom = esri.geometry.toScreenGeometry(map.extent,map.width,map.height,geom),
            graphic = map.graphics.add(new esri.Graphic(geom,config.graphics.pointMisc));
        //  add graphic to user graphics array
        arr.push(graphic);
        //  hide window if open
        if(win.isShowing){
          win.hide();
        }
        //  create form elements
        var mainDiv = dojo.create('div');
        //  define a cancel handler
        var cancelHandler = function() {
          
          if(!okClicked){
            var map = that.getMap(),
                gLayer = map.graphics,
                count = arr.length;
            try{
              gLayer.remove(graphic);                
              arr.pop();
            }catch(err){
              
            }
            if(win.isShowing){
              win.hide(); 
            }
            that._setMapNavigationState({target:that._toolBar.getDefaultTool().getElementsByTagName('img')[0]},'default');
          }
        };
        //  define a submit handler
        var submitHandler = function(){
                  
          var text = txtBox.attr('value');
          graphic.setAttributes({text:text});
          okClicked = true;
          win.setTitle(text);
          win.resize(settings.dims.addPointSize.x,settings.dims.addPointSize.y+30);
          win.setContent(that._getGraphicOptionLinks(graphic,[],true));
          that._setMapNavigationState({target:that._toolBar.getDefaultTool().getElementsByTagName('img')[0]},'default');
        };
        //  Text box
        txtBox = new dijit.form.TextBox(
          { value: settings.planAJourneyText.addPointDefaultText, style: 'width: 100px;', maxLength: 20 },
          dojo.create('div',null,mainDiv)
        );
        var focus = dojo.connect(txtBox,'_onFocus',function(){ this.attr('value','');dojo.disconnect(focus); });
        //  OK button
        var submit = new dijit.form.Button(
          { onClick: submitHandler, label: settings.misc.ok },
          dojo.create('div',null,mainDiv)
        );
        //  Cancel button
        var cancel = new dijit.form.Button(
          { onClick: cancelHandler, label: settings.misc.cancel},
          dojo.create('div',null,mainDiv)
        );
        //  Add close handler to infoWindow
        this._connects.push(util.formatConnect(dojo.connect(win,'onHide',cancelHandler),connectTypes.drawing));        
        //  Add form element to infoWindow & show window
        win.setContent(mainDiv);        
        win.setTitle(settings.planAJourneyText.addPointTitle);        
        win.resize(settings.dims.addPointSize.x,settings.dims.addPointSize.y);
        win.show(screenGeom,map.getInfoWindowAnchor(screenGeom));
        this._resizeInfoWindow(win);
        submit.focus();
      }else{
        
      }
    },
    _toolSelectNearbyPoints:function(/*esri.geometry.Point*/geom){
      
      if(util.isStringPopulated(geom.type) && geom.type==='point'){
        this.getQueryService().queryPoint(geom,config.events.selectNearbyPointResult);
      }
    },
    _getGraphicOptionLinks:function(/*esri.Graphic*/graphic,/*DOM Element Array*/elmArr,/*Boolean?*/override){
      //  summary:
      //    Creates a div with content for use in an infoWindow
      //  graphic:
      //    If graphic is specified, this is used as a reference for creating the 'use as' links
      //      graphic x & y are passed out to the API outgoing function
      //    If graphic is set to null, the 'use as' links are not added, therefore the graphic geometry 
      //      is not needed
      //  elmArr:
      //    If elmArr is specified, it is a list of dom elements which are added the first elements added to the div
      //  override:
      //    If override is specified, it is used as a boolean trigger to remove the map graphic once the user has
      //      clicked on a 'use as' link
      //  description:
      //    Creates a div with class name 'settings.cssInfoWindowContentClassName' as specified in ESRIUK.Config
      //    Populates this div with specific information if supplied (elmArr)
      //    Adds links to the bottom of the contents linking to 'use as (start|via|end)' - only if there is a valid graphic
      //    If override is specified, the graphic is removed when user clicks on a 'use as' link
      //  returns:
      //    HTML 'div' element, not yet inserted into DOM
      
      var that = this,
          i = 0,
          geom = util.isObject(graphic)? graphic.geometry : null,
          settings = config.statics,
          contentDiv = dojo.create('div'),
          values = settings.planAJourneyText,
          clickHandler = function(){ return false; },
          textPrefix = this._getAddPointPrefixText(),
          arr = util.isArrayPopulated(this._externalConfig.mode)? this._externalConfig.mode : ['start','end'];
      //  check for override param input
      override = util.isBoolean(override)? override : false;
      //  check for element array input
      elmArr = elmArr || [];
      //  add css class name to content div
      dojo.addClass(contentDiv,settings.cssInfoWindowContentClassName);
      //  add each element in the input array with a 'br' seperator
      for(i=0;i<elmArr.length;i++){
        if(util.isString(elmArr[i].tagName)){
          if(i>0){ dojo.create('br',null,contentDiv); }
          contentDiv.appendChild(elmArr[i]);
        }
      }
      //  if no geom is provided, just return contents with no 'use as' links
      if(util.isNothing(geom)){ return contentDiv; }
      //  define a click handler
      clickHandler = function(type){
        
        //  call outgoing API method
        config.events[type](
          geom.x,geom.y,
          util.isStringPopulated(graphic.attributes.text)?graphic.attributes.text:settings.pointInfoNoTitle
        );
        //  remove graphic & info window
        var map = that.getMap();
        map.infoWindow.hide();        
        if(override){
          map.graphics.remove(graphic); 
        } 
      }
      if(elmArr.length>0){ dojo.create('br',null,contentDiv); }
      //  Add relevant links      
      for(i=0;i<arr.length;i++){
        //  add a break between links
        if(i>0){ dojo.create('br',null,contentDiv); }
        if(arr[i]==='start'){
          //  add 'Plan a journey from here' link wrapped in an 'onclick' connect
          dojo.connect(
            //  create link tag & add to contentDiv
            dojo.create('a',{innerHTML:textPrefix+values.addPointSuffixStart},contentDiv),
            //  connect to 'onclick'
            'onclick',
            //  add event handler
            function(){ clickHandler('useAsStartPoint');} 
          );
        }else if(arr[i]==='via'){
          //  add 'Plan a journey via here' link and connect it with 'onclick'
          dojo.connect(
            //  create link tag & add to contentDiv
            dojo.create('a',{innerHTML:textPrefix+values.addPointSuffixVia},contentDiv),
            //  connect to 'onclick'
            'onclick',
            //  add event handler
            function(){ clickHandler('useAsViaPoint');} 
          );
        }else if(arr[i]==='end'){
          //  add 'Plan a journey to here' link and connect it with 'onclick'
          dojo.connect(
            //  create link tag & add to contentDiv
            dojo.create('a',{innerHTML:textPrefix+values.addPointSuffixEnd},contentDiv),
            //  connect to 'onclick'
            'onclick',
            //  add event handler
            function(){ clickHandler('useAsEndPoint');} 
          );
        }
      }
      if(contentDiv.firstChild!==null){
        return contentDiv;
      }else{
        return null;
      }
    },
    //  START: Event Handlers  //
    onGraphicClick:function(event){
          
      if(util.isObject(event.graphic)){
        var graphic = event.graphic;
        if(!util.isNothing(graphic.attributes)){
          // Custom draw callout function
          this.drawCallout(event,this.getMap(), graphic, graphic.geometry);
        }
      }
    },
    onGraphicMouseOver:function(event){
          
      if(util.isObject(event.graphic)){
        var graphic = event.graphic;
        if(!util.isNothing(graphic.attributes)){
          // Custom draw callout function
          this.drawCallout(event,this.getMap(), graphic, graphic.geometry);
        }
      }
    },
    onMapExtentChange: function(extent, delta, levelChange, lod){
      
      
      var t = this._queryTimer,
          time = config.query.wait,
          that = this;
      if(util.isNumber(t)){ clearTimeout(t); this._loadingHandler(false); }
      this._queryTimer = setTimeout(function(){ 
        that._onMapExtentChange(extent,delta,levelChange,lod); 
                
      },time);
      if(util.isObject(this._sliderHandle)){
        this._sliderHandle.title = 'Scale = 1:'+dojo.number.format(lod.scale,"#,##0.###");
        this._sliderHandle.alt = this._sliderHandle.title;
      }
    },
    _onMapExtentChange:function(extent, delta, levelChange, lod){
      
      var map = this.getMap(),
          t = this._queryTimer;
      if(util.isNumber(t)){ clearTimeout(t); this._queryTimer = null; }
      this._removeMapFeatures();
      if(lod.level>config.query.maxLOD&&this._disable.extentChange){
        this.getQueryService().getExtentFeatures(extent,map,this.getBaseMapService()._layers,this._filter,this._plotMapFeatures,this);
      }else{
        if(levelChange){
          this.toggleEvt(connectTypes.extentQuery,true);
        }
      }
    },
    onMapPanStart:function(extent,startPoint){
      this._removeMapFeatures();
    },    
    onMapPan: function(){
          
      //Not fully implemented
      //Can do overview updates here
      return;
    },
    onMapPanEnd: function(extent, delta){
      //  summary:
      //    listens for a map pan end event and forwards to any external listeners
          
      //dojo.publish("/esriuk/map/onMapPanEnd",[extent,extent.getCenter()]);
    },
    onMapResize: function(){
      //  summary:
      //    map resize handler
      //    sets timer of 0.3 seconds to do the proper map resize,
      //    using the timer is advised best practise in the ESRI JS API
      
      if(dojo.coords(this.domNode).w>2 && dojo.coords(this.domNode).h>2) {
        //  clear any previous timer
        clearTimeout(this._resizeTimer);
        var that = this;
        var scope = arguments.callee.nom;
        //  hack to handle initial page load resize event in IE6
        if(dojo.isIE<7&&this._ie6ResizeTrigger){
          this._ie6ResizeTrigger = false;
          return;
        }
        //  set timeout function handler
        this._resizeTimer = setTimeout(function(){
          var map = that.getMap();        
          //  if the map is in whole page mode - use these settings
          if(that._externalConfig.wholePage){
            try{
              //  call whole page resize on containing map dijit
              var parentDims = that.getParent()._wholePageResize();
              //  filter the params into this - the child dijit
              dojo.style(that.domNode,{height:parentDims.height+'px',width:parentDims.width+'px'});
            }catch(err){
              
            }
          }
          //  if a specific map size has been set, this is then overriden by default settings
          //  this is done so that the map does not overlap the containing elements
          if(that._resizeSet){
            that._resizeSet = false;
            
          }          
          dojo.style(that.domNode.parentNode,{width:'auto'});
          dojo.style(that.domNode,{width:'auto'});
          var thisDims = dojo.coords(that.domNode);
          //  physically set the new dims of the map
          map.width = parseInt(thisDims.w,10);
          map.height = parseInt(thisDims.h,10);
          
          if(dojo.isIE<7){
            var outerDims = dojo.coords(that.domNode.parentNode.parentNode);
            var hGutter = util.getElmGutterHorizontal(that.domNode.parentNode.parentNode) + util.getElmGutterHorizontal(that.domNode.parentNode);            
            dojo.style(that.domNode,{width:outerDims.w-hGutter});
            map.width = outerDims.w-hGutter;
            //  var innerDims = dojo.coords(that.domNode.parentNode);
            //  alert(outerDims.w+ " " + innerDims.w);
          }         
          //  finally call the proper map resize method
          that._onMapResize();
        },300);
      }
    },
    onScroll:function(){
      //  summary:
      //    window scroll handler
      //    sets timer of 0.3 seconds to do the proper map reposition,
      
      //  clear any previous timer
      clearTimeout(this._scrollTimer);
      var that = this;
      //  set timeout function handler
      this._scrollTimer = setTimeout(function(){                  
        //  call the proper map resize method
        that._onScroll();
      },300);
    },
    _onMapResize:function(){
      //  summary:
      //    called after a timeout - this allows all map paramters time to catch up
      //    uses the advised best practise in ESRI JS API - map.reposition() & map.resize()
      //    finally, includes a bug fix for the JS API
      
      var that = this,
          scope = arguments.callee.nom,
          map = this.getMap();
      map.reposition();
      map.resize();
      //  section added to fix layers out-of-sync issue
      //  issue on ESRI forums: http://forums.esri.com/Thread.asp?c=158&f=2396&t=278604
      setTimeout(function(){
        try{
          //  runs through all layers in the map and calls the refresh method
          for(var i=0;i<map.layerIds.length;i++){
            map.getLayer(map.layerIds[i]).refresh();
          }
        }catch(err){
          
        }
      },300);
    },    
    _onScroll:function(){
      //  summary:
      //    called after a timeout - this allows all map paramters time to catch up
      
      this.getMap().reposition();
    },
    //  END: Event Handlers  //   
    _getClosestScale: function(/*esri.geometry.Extent*/extent,/*Object*/dims){
         
      
      var lodScale,previous,next,scalempp,scale,i
          lods = config.map.lods,
          dpi = 96,
          counter = 0,
          factor = 1.2,
          inchPerMetre = 39.3700787402,
          smaller = true;
      
      while(smaller&&counter < 20){
        counter++;
        next = null; 
        previous = null;
        scalempp = extent.getWidth() / dims.w,
        scale = Math.round(scalempp * dpi * inchPerMetre);
                
        for(i=0;i<lods.length;i++){
          lodScale = lods[i].scale;
          if(scale<lodScale){
            previous = lodScale;
          }else if(scale>lodScale){
            next = lodScale;
            break;
          }else if(scale===lodScale){
            return extent;
          }
        }
        
        if(!util.isNothing(previous,next)){
          var upperDiff = previous - scale,
              lowerDiff = scale - next;
          if(upperDiff>=lowerDiff){
            
            extent = extent.expand(factor);
          }else{
            smaller = false;
            
            return extent;
          }
        }else{
          smaller = false;
        }
      }

    },
    _getClosestLOD: function(scale){
             
      var lods = config.map.lods,
          lod = null;
      for(var i=0;i<lods.length;i++){
        if(scale==lods[i].scale){
          return lods[i].level;
        }else if(scale>lods[i].scale){
          lod = i>0? lods[i-1] : lods[0];
          
          return lod.level;
        }
      }
      lod = lods[lods.length-1];
      
      return lod.level;
    },
    _getLODInfo:function(/*number*/value,/*string?*/type){
      //  summary:
      //    attempts to retrieve an LOD from map lod array - set in config
      //  params:
      //    type = string - optional string indicates if incoming value is 'scale' or 'level', default is 'level'
      //    value = integer - actual int value (scale|level)
      //  note:
      //    looks in ESRIUK.Config for config.map.lods array
             
      var i = null,
          lod = null,
          arr = config.map.lods;
      value = util.parseNumber(value);    
      if(util.isArrayPopulated(arr)){
        if(type!=='level'){
          value = this._getClosestLOD(value);  
        }
        for(i=0;i<arr.length;i++){
          //  if incoming level is in array element level property -> return this element in array
          lod = arr[i];
          if(value===lod.level){
            
            return lod;                
          }
        }
      }else{
        
      }
      //  if 
      
      return lod;
    },
    _generateExtentFromPt:function(/*esri.geometry.Point*/pt,/*number*/scale,/*boolean?*/isLevel){
      //  summary:
      //    attempts to generate an extent from given point at scale or level
      //  note:
      //    if isLevel is true, will look for a LOD level as opposed to matching an actual scale number
      
      try{
        isLevel = util.parseBool(isLevel);
        var settings = esri.config.defaults.map,
            metresPerPx = this._getLODInfo(scale,isLevel?'level':null).resolution,
            x = (settings.width/2)*metresPerPx,
            y = (settings.height/2)*metresPerPx;
        return new esri.geometry.Extent(pt.x-x, pt.y-y, pt.x+x, pt.y+y, new esri.SpatialReference({wkid:config.map.wkid}));
      }catch(err){
        
        return null;
      }
    },
    _plotMapFeatures:function(/*Array[Object]*/arr){
      //  summary:
      //    Adds temporary point graphics to map
      //    Stores them in the 'this._graphics' array property
      
      var item = null,
          pt = null,
          count = arr.length,
          map = this.getMap(),
          geo = esri.geometry,
          gLayer = map.graphics,
          graphics = this._graphics,
          ref = map.spatialReference,
          symbol = config.graphics.point; 
      for(var i=0;i<count;++i){
        item = arr[i];
        if(item.points.length==1){
          pt = item.points[0];
          graphics.push(
            gLayer.add(
              new esri.Graphic(
                new geo.Point(pt.x,pt.y,ref), symbol,{type:item.type,values:item.values}
              )
            )
          );
        }
      }
    },
    _removeMapFeatures:function(){
      //  summary:
      //    Removes all graphics in the temporary graphics array 'this._graphics'
      var arr = this._graphics,
          gLayer = this.getMap().graphics,
          count = arr.length;
      for(var i=0;i<count;++i){
        gLayer.remove(arr[i]);
      }
      this._graphics = [];
    },
    _removeUserGraphics:function(){
      //  summary:
      //    Removes all graphics in the temporary graphics array 'this._userGraphics'
      var arr = this._userGraphics,
          gLayer = this.getMap().graphics,
          count = arr.length;
      for(var i=0;i<count;++i){
        gLayer.remove(arr[i]);
      }
      this._userGraphics = [];
    },
    //  START: Public API Handlers  //
    setExtent: function(extent){
      //  summary:
      //    calls standard esri map api method setExtent
          
      this.getMap().setExtent(extent);
    },
    resize:function(/*integer*/x,/*integer*/y){
      //  sumamry:
      //    resizes map either in response to window resize or an incoming api call
      
      if(util.isNumber(x)&&util.isNumber(y)){
        var map = this.getMap();
        this._resizeSet = true;
        dojo.style(this.domNode,{height:y+"px",width:x+'px'});
        map.width = parseInt(x,10);
        map.height = parseInt(y,10);
        this._onMapResize();      
      }else{
        
      }
    },
    zoomToFullExtent:function(){
      //  sumamry:
      //    
      
      this._navBar.zoomToFullExtent();
    },
    zoomToPreviousExtent:function(){
      //  sumamry:
      //    
      
      this._navBar.zoomToPrevExtent();
    },
    zoomToXY: function(/*number*/x,/*number*/y){
          
      var map = this.getMap();
      var pt = new esri.geometry.Point(util.parseNumber(x), util.parseNumber(y), map.spatialReference);
      map.centerAt(pt);
    },
    zoomToLevel:function(/*integer*/level){
          
      this.getMap().setLevel(level);
    },
    zoomToScale:function(/*integer*/scale){
      
      var lod = this._getClosestLOD(scale);
      this.getMap().setLevel(lod);
    },    
    centerAndZoom:function(/*number*/x,/*number*/y,/*integer*/scale,/*string?*/text){
      //  summary:
      //    external API method
      //    takes x & y values and scale (slider LOD) and centers map at location and zoom and optional text
      //  note:
      //    if optional text parameter is passed in, this action is done, then parameters
      //    are passed on to 'this.addIconAndInfoWindow' to handle adding a graphic and text infoWindow
      //  params:
      //    x = double - x coordinate
      //    y = double - y coordinate
      //    scale = integer - LOD level
      //    text (OPTIONAL)  = string to add as info window to a graphic at given point
          
      var map = this.getMap(),
          that = this,
          zoomEndConnect = null,
          //  define an event handler for the end of the map zoom action
          zoomEndHandler = function(extent, zoomFactor, anchor, level){
            //  remove event handler
            dojo.disconnect(zoomEndConnect);
            //  forward to local method
            this.addIconAndInfoWindow(x,y,text,false);
          };
      //  check if text has been sent in
      text = util.isNothing(text)? null : text;          
      //  only connect event if text has been passed in
      if(util.isStringPopulated(text)){
        zoomEndConnect = dojo.connect(map,'onExtentChange',that,zoomEndHandler);
      }
      //  make map api call to zoom to pt at scale
      map.centerAndZoom(
        new esri.geometry.Point(util.parseNumber(x), util.parseNumber(y), map.spatialReference),
        scale
      );
    },
    centerAndScale:function(/*number*/x,/*number*/y,/*integer*/scale,/*string?*/text){
      //  summary:
      //    external API method
      //    takes x & y values and scale (real) and centers map at location and zoom 
      //  note:
      //    uses incoming real scale to check against nearest LOD level
          
      var lod = this._getClosestLOD(scale);  //  get closest LOD
      text = util.isStringPopulated(text)? text : null;  //  has text been sent in ?          
      //  forward to local handler
      this.centerAndZoom(x, y, lod, text);
    },
    zoomToExtent:function(/*extent*/ext,/*boolean*/zoomToFit){
          
      zoomToFit = util.isBoolean(zoomToFit)? zoomToFit : false;
      this.getMap().setExtent(ext,zoomToFit);
    },
    addStartEndPt:function(/*point*/pt){
      //  summary:
      //    attempts to add a atart/via/end point to the defined 'addable graphics' map service
      
      var mService = this.getGraphicsMapService();
      if(!util.isNothing(mService)){
        //  try to add point
        mService.addStartEndPt(pt);
      }else{
        //  log and propogate exception to Map class
        
        throw { source:this.declaredClass+arguments.callee.nom,
          message: ' No \'addable graphics\' map service available.\n Check there is a map service with \'addGraphics:true\' defined in config. ' };
      }
    },
    addIconAndInfoWindow:function(/*double*/x,/*double*/y,/*string*/text,/*boolean?*/ignoreTool){
      //  summary:
      //    implements API call to add marker with infoWindow
      
      //  create geometry & graphic & add to the map
      var map = this.getMap(),
          geom = new esri.geometry.Point(x,y,map.spatialReference),
          graphic = map.graphics.add(
            new esri.Graphic( geom, config.graphics.pointMisc, {text:text} )
          );
      //  use geom & graphic as base for an infoWindow
      //  sending in a 'mock' event as first parameter
      this.drawCallout({screenPoint:esri.geometry.toScreenGeometry(map.extent,map.width,map.height,geom)},map,graphic,geom);
      if(!util.parseBool(ignoreTool)){
        //  reset toolbar back to default navigation 
        this.setActiveTool(config.toolBar.typeEnum.defaultTool);
      }
    },
    clearPoints:function(/*String*/type){
      //  summary:
      //    attempts to clearPoints from the defined 'addable graphics' map service
      
      var mService = this.getGraphicsMapService();
      if(!util.isNothing(mService)){
        if(((!util.isStringPopulated(type))||type==='graphics')&&type!=='server'){
          this._removeUserGraphics();
        }
        //  try to clearPoints
        mService.clearPoints(type);
      }else{
        //  log and propogate exception to Map class
        
        throw { source:this.declaredClass+arguments.callee.nom,
          message: ' No \'addable graphics\' map service available.\n Check there is a map service with \'addGraphics:true\' defined in config. ' };
      }
    },
    getLayerList: function(){
      //  summary:
      //    returns list of layers
      return this.copyLayerList(this.getBaseMapService()._layers);
    },
    copyLayerList:function(layers){
      //  summary:
      //    copies a layer list into a new object
      var i=0,
        obj = {carparkLayerVisible:false,stops:[],pointX:[]},
        stops = layers.stops,
        pointX = layers.pointX,
        carparks = layers.carparkLayerVisible;
      //  copy carparks visibility
      obj.carparkLayerVisible = carparks;
      //  cpy stops visibility array
      for(i=0;i<stops.length;++i){
        obj.stops.push(dojo.mixin({},stops[i]));
      }
      //  copy pointX visibility array
      for(i=0;i<pointX.length;++i){
        obj.pointX.push(dojo.mixin({},pointX[i]));
      }      
      return obj;      
    },
    getMapProperties:function(){
      var map = this.getMap(),
          lod = esri.version<1.5? map._LOD: map.__LOD,
          extent = map.extent,
          obj = this._externalConfig,
          centrePoint = new esri.geometry.Point(
            extent.xmin+((extent.xmax-extent.xmin)/2),
            extent.ymin+((extent.ymax-extent.ymin)/2),
            map.spatialReference
          ).toJson();
      var inputs = {};
      for (var name in obj) {
        if (obj.hasOwnProperty(name)) {
          if(name==='location'){
            if(obj[name].x!==null){
              dojo.mixin(inputs,{location:obj[name]});
            }
          }else if(name!=='set'&&name!=='wholePage'){
            if(obj[name]!==null){
              dojo.mixin(inputs,dojo.fromJson('{"'+name+'":'+dojo.toJson(obj[name])+'}'));
            }
          }
        }
      }
      return { 
        scale: lod.scale, 
        level: lod.level, 
        centrePoint: centrePoint,
        layers: this.getLayerList(),
        inputs: inputs
      };        
    },
    toggleLayer: function(layerId,on){
          
      var baseMapService = this.getBaseMapService();
      baseMapService.toggleLayer(layerId,on);
    },
    setLayerState: function(layers){
          
      var map = this.getMap(),
          baseMapService = this.getBaseMapService(),
          obj = this.copyLayerList(layers);
          
      this._removeMapFeatures();
      baseMapService.setLayerState(obj);
      this.getQueryService().getExtentFeatures(map.extent,map,obj,this._filter,this._plotMapFeatures,this);      
    },
    setTravelNews:function(/*Object*/settings){
      //  summary:
      //    Public API method
      //    Validates incoming travel news filter and passes to the base map service
      
      var map = this.getMap(),
          mService = this.getBaseMapService();
      this._filter = this.validateTravelFilter(settings);
      this._removeMapFeatures();
      mService.setTravelNewsLayers(this._filter);
      this.getQueryService().getExtentFeatures(map.extent,map,mService._layers,this._filter,this._plotMapFeatures,this);        
    },
    findJunctionPoint:function(/*String*/TOID_a,/*String*/TOID_b,/*Number*/level,/*Boolean*/zoomTo){
          
      var map = this.getMap(),
          obj = this.getQueryService().findJunctionPoint(TOID_a,TOID_b),
          pt = new esri.geometry.Point(obj.x,obj.y,map.spatialReference);
      if(zoomTo){
        if(util.isNumber(level)){
          map.centerAndZoom(pt,util.parseNumber(level)-1);
        }else{
          map.centerAt(pt);
        }
      }
      return pt.toJson();
    },
    findITNNodePoint:function(/*String*/TOID,/*Number*/level,/*Boolean*/zoomTo){
          
      var map = this.getMap(),
          obj = this.getQueryService().findITNNodePoint(TOID),
          pt = new esri.geometry.Point(obj.x,obj.y,map.spatialReference);
      if(zoomTo){
        if(util.isNumber(level)){
          map.centerAndZoom(pt,util.parseNumber(level)-1);
        }else{
          map.centerAt(pt);
        }
      }
      return pt.toJson();
    },   
    addRoute:function(/*String*/sessionID,/*Number*/routeNumber,/*String*/type,/*String?*/zoomTo){
          
      var map = this.getMap(),
          layer = this.getBaseMapService(),
          obj = layer.addRoute(sessionID,routeNumber,type,zoomTo),
          ext = new esri.geometry.Extent(
            {xmin:obj.xmin,ymin:obj.ymin,xmax:obj.xmax,ymax:obj.ymax,spatialReference:map.spatialReference}
          );
      if(zoomTo==='all'||zoomTo==='this'){
        map.setExtent(ext,true);
      }else{
        layer.refresh();
        this._loadingHandler(false);
      }
      return ext.toJson();
    },  
    clearRoutes:function(/*String?*/type){
          
      this.getBaseMapService().clearRoutes(type);
    },
    zoomToRoute:function(/*String*/sessionID,/*Number*/routeNumber,/*String*/type){
          
      var map = this.getMap(),
          obj = this.getQueryService().getRouteEnvelope(sessionID,routeNumber,type),
          ext = new esri.geometry.Extent(
            {xmin:obj.xmin,ymin:obj.ymin,xmax:obj.xmax,ymax:obj.ymax,spatialReference:map.spatialReference}
          );
      map.setExtent(ext,true);
      return ext.toJson();
    },
    zoomToAllAddedRoues:function(){
          
      var map = this.getMap(),
          obj = this.getBaseMapService().zoomToAllAddedRoutes(),
          ext = new esri.geometry.Extent(
            {xmin:obj.xmin,ymin:obj.ymin,xmax:obj.xmax,ymax:obj.ymax,spatialReference:map.spatialReference}
          );
      map.setExtent(ext,true);
      return ext.toJson();
    },
    getPrintImage:function(/*Number*/width,/*Number*/height,/*Number*/dpi){
      //  summary:
      //    Public API method
      //    Forwards params to base map service to get a print image
      //  returns:
      //    String url for print image
      
      return this.getBaseMapService().getPrintImage(width,height,dpi);
    },
    getNumberOfCycleImages:function(/*Number*/scale){
      //  summary:
      //    Public API method
      //    Forwards params to base map service to number of images for cycle print
      //  returns:
      //    Number of images
      
      return this.getBaseMapService().getNumberOfCycleImages(scale);
    },
    getCyclePrintDetails:function(/*Number*/scale){
      //  summary:
      //    Public API method
      //    Forwards params to base map service to get array of cycle print details
      //  returns:
      //    Array of cycle print infos
      
      return this.getBaseMapService().getCyclePrintDetails(scale);
    },
    //  END: Public API Handlers
    //  ==========================  //
    validateTravelFilter:function(/*Object*/settings){
      //  summary:
      //    Takes a pre-checked object representing travel filter
      //    Type checks all properties and formlates a valid travel filter
          
      var str = null,
          valid = false,
          obj = {};    
      //  read visible layers
      if(util.isStringPopulated(settings.transportType)){
        str = settings.transportType;
        if(str==="all"){
          valid = true;
          dojo.mixin(obj,{roadIncidentsVisible:true,publicIncidentsVisible:true});
        }else if(str==="road"){
          valid = true;
          dojo.mixin(obj,{roadIncidentsVisible:true,publicIncidentsVisible:false});
        }else if(str==="public"){
          valid = true;
          dojo.mixin(obj,{roadIncidentsVisible:false,publicIncidentsVisible:true});
        }else if(str==="none"){
          valid = true;
          dojo.mixin(obj,{roadIncidentsVisible:false,publicIncidentsVisible:false});
        }
      }
      //  read incident type filter
      if(util.isStringPopulated(settings.incidentType)){
        str = settings.incidentType;      
        if(str==="all"||str==="planned"||str==="unplanned"){
          valid = true;
          dojo.mixin(obj,{incidentType:settings.incidentType});
        }else{
          
        }
      }
      //  read delays type filter
      if(util.isStringPopulated(settings.severity)){
        str = settings.severity;      
        if(str==="all"||str==="major"){
          valid = true;
          dojo.mixin(obj,{severity:settings.severity});
        }else{
          
        }
      }
      //  read timeperiod type filter
      if(util.isStringPopulated(settings.timePeriod)){
        str = settings.timePeriod;      
        if(str==="current"||str==="recent"){
          valid = true;
          dojo.mixin(obj,{timePeriod:settings.timePeriod});
        }else if(str==="date"||str==="datetime"){
          //  read datetime filter - check datetime against regex
          if(util.isStringPopulated(settings.datetime)){
            str = settings.datetime; 
            if(/^\d{1,2}\/\d{1,2}\/\d{2,4} \d{1,2}[\:\/]\d{1,2}$/.test(str)){
              valid = true;
              dojo.mixin(obj,{datetime:str.replace(/\:/g,"/"),timePeriod:settings.timePeriod});          
            }else{
              valid = false;
              
            }
          }        
        }else{
          
        }
      }

      //  return populated object
      if(valid){
        return obj;
      }else{
        throw { message: this.declaredClass+'.'+arguments.callee.nom+' failed to parse travel news ',obj:settings};
      }
    },
    drawCallout: function(event, map, graphic, geom) {
      //  summary:
      //    draws an info window at a selected graphic point
          
      var id = null,
          link = null,
          elements = [],
          attr = graphic.attributes,
          arr = attr.values,
          settings = config.statics,
          dims = settings.dims,
          win = map.infoWindow,
          removeGraphic = true,
          //  specific feature class infoWindow
          types = config.graphics.typeEnum,
          //  extract type attribute - if it exists
          type = util.isStringPopulated(attr.type)?attr.type:'Generic',
          //  extract text attribute from graphic
          text = util.isStringPopulated(attr.text)?attr.text:settings.pointInfoNoTitle;

                  
      switch(type){
        //  CarParks Feature
        case types.carParks:
          var isPR = arr[0]==="Y",
              location = arr[1],
              name = arr[2],
              ref = arr[3];
          removeGraphic = false;
          str = settings.misc;
          link = dojo.create('a',{innerHTML:str.infoPageCarParks});
          text = '{a}, {b} {c}{d}'.supplant({a:location,b:name,c:(isPR?str.parkandride:''),d:str.carpark});
          dojo.connect(link,'onclick',function(){ config.events.showCarParkInformation(ref); });
          elements = [link];
          win.resize(dims.calloutCarParks.x,dims.calloutCarParks.y);           
        break;
        //  Stops (both) Feature
        case types.stops:
          var str = settings.misc;
          id = arr[0];
          text = arr[1];
          removeGraphic = false;          
          link = dojo.create('a',{innerHTML:str.infoPageStops});
          dojo.connect(link,'onclick',function(){ config.events.showStopsInformation(id); });
          elements = [link];
          win.resize(dims.calloutStops.x,dims.calloutStops.y);    
        break;
        //  Travel News & PointX Features
        case types.travelNews: case types.pointX:
          var that = this,
              elm = null,
              travel = type===types.travelNews,
              scope = arguments.callee.nom,
              method = travel?'getTravelNewsInfo':'getPointXInfo';
          dims = travel? dims.calloutTravelNews : dims.calloutPointX;
          id = arr[0];
          win.setTitle(settings.misc.loading);
          win.setContent('<img src="'+this.imagesPath+'ajax-loader.gif" alt="Loading..." />');                         
          win.resize(dims.x,dims.y);
          win.show(event.screenPoint,map.getInfoWindowAnchor(event.screenPoint));  
          this.getQueryService()[method](id,function(text,elmArr){
            graphic.attributes.text = text;
            win.setTitle(text.replace("&apos;",'&#039;'));
            var elm = that._getGraphicOptionLinks((travel?null:graphic),elmArr);
            if(elm!==null){
              win.setContent(elm); 
              that._resizeInfoWindow(win);              
            }else{
              win.setContent('');
              dojo.style(win.containerNode,{height:'0px'});
              that._resizeInfoWindow(win,true);
            }
          });
          return;
        default:
          //  non-specific infoWindow
          win.resize(dims.calloutGeneric.x,dims.calloutGeneric.y);
        break;
      }
      graphic.attributes.text = text;
      win.setTitle(text.replace("&apos;",'&#039;'));
      win.setContent(this._getGraphicOptionLinks(graphic,elements,removeGraphic));      
      win.show(event.screenPoint,map.getInfoWindowAnchor(event.screenPoint));  
      this._resizeInfoWindow(win);
    },
    _resizeInfoWindow:function(/*esri.dijit.InfoWindow*/win,/*Boolean?*/override){
      var dims = dojo.coords(win._titlebar);
      var dims2 = dojo.coords(util.parseBool(override)?win.containerNode:win.containerNode.firstChild);
      if(util.isNumber(dojo.isIE)){
        dims.h += config.statics.dims.ieBuffer;
      }
      win.resize(win.width,dims.h+dims.t+dims2.h+dims2.t);
    },
    fuzzyPan: function(pt,panx,pany) {
          
      //pans the map just far enough to get the point inside the extent borders
      //pan is an additional factor to scroll inside the borders, e.g. 0.1 adds 10% of the width or height of the extent
      var map = this.getMap();
      var ex = map.extent;
      if(map.extent.contains(pt)) {
        return;
      } else {
        this.toggleEvt(eventToggleEnum.ext,false);
        var ox = 0;
        var oy = 0;
        if(pt.x < ex.xmin) {
          ox = pt.x - ex.xmin - (panx * ex.getWidth());
        } else if (pt.x > ex.xmax) {
          ox = pt.x - ex.xmax + (panx * ex.getWidth());
        }
        if(pt.y < ex.ymin) {
          oy = pt.y - ex.ymin - (pany * ex.getHeight());
        } else if (pt.y > ex.ymax) {
          oy = pt.y - ex.ymax + (pany * ex.getHeight());
        }
        map.setExtent(ex.offset(ox,oy));
      }
    }
  };
})());

}

if(!dojo._hasResource['ESRIUK.Dijits.Map']){ //_hasResource checks added by build. Do not use _hasResource directly in your code.
dojo._hasResource['ESRIUK.Dijits.Map'] = true;
//  Core dojo references




//  ESRI (UK) references






ESRIUK.Dijits.Map=ESRIUK.Dijits.Map||{};dojo._loadedModules["ESRIUK.Dijits.Map"] = ESRIUK.Dijits.Map;;
dojo.declare('ESRIUK.Dijits.Map',[dijit._Widget,dijit._Templated,dijit._Container],(function(){
    //  summary:
    //    Main map container dijit
    //    Handles all incoming map mainpulation method, map styling etc...
    //    Reads URL & Inline parameters set on map
    var util = ESRIUK.util(),
        config = null;
    return {
        loaded:false,
        _tmpConnect:null,
        _tmpWidth:null,
        _tmpHeight:null,
        settings:{},
    /* START: Initiation attributes */
        param_XMin:null,  //  map extent xmin
        param_YMin:null,  //  map extent ymin
        param_XMax:null,  //  map extent xmax
        param_YMax:null,  //  map extent ymax
        param_Scale:null,  //  map scale - scale overrides level
        param_Level:null,  //  map zoom slider level
        param_Width:null,  //  map width
        param_Height:null,  //  map height
        param_LocationX:null,  //  map centre point x
        param_LocationY:null,  //  map centre point y
        param_Text:'',  //  plan a journey text
        param_Mode:'',  //  map 'plan a journey' mode
        param_Tools:'',  //  list of map tools
        param_Symbols:'',  //  map graphic symbols
        param_Routes:'',  //  route info objects
        param_TravelNews:'',  //  map travel news filter        
        param_WholePage:false,  //  assumes the map is the only element on the page
    /* END: Initiation attributes */
    /* START: Implementing dijit._Templated */
        widgetsInTemplate:true,
        templateString:"<div class='esriuk-custom-mapbase'>\r\n  <div dojoAttachPoint='attachNavPanel' dojoType='ESRIUK.Dijits.MapNavPanel' class='mapOverlay semiTransparent' style=\"display:none\"></div>\r\n  <div dojoAttachPoint='attachToolBarPanel' dojoType='ESRIUK.Dijits.MapToolBarPanel' class='mapOverlay'></div>\r\n  <div class='sliderPanelContainer mapOverlay semiTransparent' style=\"display:none\"><div dojoAttachPoint='attachMapSliderContent' class='sliderPanelHolder mapPanel'></div></div>\r\n  <div dojoAttachPoint='attachLoading' dojoType='ESRIUK.Dijits.Loading'></div>\r\n  <div dojoAttachPoint='attachMap' dojoType='ESRIUK.Dijits.MapBase' class=\"esriuk-custom-map\"></div>\r\n</div>\r\n",
        imagesPath: dojo.moduleUrl("ESRIUK.Dijits", "images/Map/"),
    /* END: Implementing dijit._Templated */
    /* START: Widget lifecycle http://docs.dojocampus.org/dijit/_Widget */
        constructor:function(){
            
            
            dojo.mixin(this,arguments[0]);
            //  must reset all settings here
            //  this is a bug fix - memory issue
            this.settings = {
                set:false,
                scale:null,
                level:null,
                location:{x:null,y:null},
                extent:{xmin:null,ymin:null,xmax:null,ymax:null},
                height:null,
                width:null,
                text:null,
                tools:[],
                travelNews:null,
                symbols:null,
                mode:null
            };
            
            ESRIUK._config = null;
            config = ESRIUK.config();
            
            dojo.subscribe(config.events.mapLoad,this,'onLoad');
        },      
        postCreate:function(){
            //  summary:
            //    core dijit._Widget method - overriden here
            //    called after widget has been rendered but children have not
            //    used here to insert local config to child map widget     
             
            //  call inherited function
            this.inherited(arguments);
            //  switch on loading icon
            this.attachLoading.toggle(true);
            //  read init params
            //  first try url params
            this._readURLSettings();
            //  if none are valid, try inline elements
            this._readInlineSettings();            
            //  add required css files
            this._addRequiredCSSFiles();
            //  add setting to child map element
            this._applyStyleParams(true);
            //  check for map object property
            if(!util.isNothing(this.attachMap)){
                try{
                    //  attach events to loading icon/panel
                    this.attachMap.bindLoading(this.attachLoading);
                    //  get all settings aquired from inline & URL
                    //  initiate MapBase class with config
                    this.attachMap.init(this.getMapParams());
                    //  show/hide zoom&pan toolbars
                    this.showPanels(this.getToolsInput());
                    //  get all settings for toolbar items
                    //  apply them to toolbar
                    this.attachToolBarPanel.init(this.getToolsInput());
                }catch(err){
                    
                }
            }else{
                
            }
        },
        startup:function(){
            //  summary:
            //    core dijit._Widget method - overriden here
            //    called after all children widgets have been instantiated
            //    used here to bind events between child widgets
             
            this.inherited(arguments);
            //  get reference to child widgets
            var map = this.attachMap,
                nav = this.attachNavPanel,
                tools = this.attachToolBarPanel;
            //  bind nav & tool events to map
            try{
                //  attach events to navigation panel
                map.bindNavControls(nav);
                //  attach events to toolbar panel         
                map.bindToolbar(tools);
                //  add JS API bug fixes
                this._addBugFixes();
            }catch(err){
                
            }finally{
                //  switch off loading icon
                this.attachLoading.toggle(false);
            }
             
        },
        onLoad:function(){
            
            this.loaded = true;
            //  call map init complete method
            config.events.onMapInitialiseComplete(this);
            //debugger;
        },
        destroy:function(){
            //  summary:
            //    core dijit._Widget method - overriden here
            //    called to destroy this widget
            //    used here to perform clean-up code
             
            //  call custom clean-up code
            
            //  call inherited method
            this.inherited(arguments);
            
        },
        destroyRecursive:function(){
            //  summary:
            //    core dijit._Widget method - overriden here
            //    called to destroy all child widgets
            //    used here for logging of destroy process in console window
            //  note: 
            //    Also exposed as an external API call
             
            //  call inherited method
            this.inherited(arguments);
            //  log end of process
            
            
        },
    /* END: Widget lifecycle http://docs.dojocampus.org/dijit/_Widget */
    /* START: Public Methods */
        resize:function(/*Number|Object*/x,/*Number?*/y){
            //  summary:
            //    this function will be called by the dojo api in response to a page resize
            //    it is also open for external calling via published ESRIUK.Map API
            //    takes x (width) & y (height) and resizes map to these params
            
            //  look for external API calls
            if(util.isNumber(x,y)){
                try{
                    this.setWidth(x);
                    this.setHeight(y);
                    //  apply settings to current map containing element
                    this._applyStyleParams();
                    //  propogate settings down to child MapBase class
                    this.attachMap.resize(x,y);
                    
                }catch(err){
                    
                }
            //  look for Dojo resize events
            }else if(util.isValidWH(x)){
                //  check if resize handler has been set
                if(config.map.useResizeHandler){
                    
                }else{
                    try{
                        //  if coming from API, call the scroll handler,
                        //  this is needed to call a map.reposition()
                        this.attachMap.onScroll();
                    }catch(err){
                        
                    }                
                }
            //  invalid calls
            }else{
                
            }
        },
        zoomToXY:function(/*Number*/x,/*Number*/y){
            //  summary:
            //    takes x & y values and pans map to location
                        
            if(util.isNumber(x,y)){
                try{
                    //  forwards the valid parameters to the MapBase class
                    this.attachMap.zoomToXY(x,y);
                    
                }catch(err){
                    
                }
            }else{
                
            }
        },
        zoomToLevel:function(/*Number*/level){
            //  summary
            //    takes integer scale level (LOD) and zooms map to that level
                        
            if(util.isNumber(level)){
                try{
                    //  forwards valid integer scale to map dijit
                    this.attachMap.zoomToLevel(level);
                    
                }catch(err){
                    
                }
            }else{
                
            }
        },        
        zoomToScale:function(/*Number*/scale){
            //  summary
            //    takes integer scale and zooms map to that level
                        
            if(util.isNumber(scale)){
                try{
                    //  forwards valid integer scale to map dijit
                    this.attachMap.zoomToScale(scale);
                    
                }catch(err){
                    
                }
            }else{
                
            }
        },
        zoomToLevelAndPoint:function(/*Number*/x,/*Number*/y,/*Number*/level,/*String?*/text){
            //  summary:
            //    zooms map to given point and zoom level
            //  params:
            //    x = BNG Easting
            //    y = BNG Northing
            //    level = LOD
            //    text = (Optional)Text to show in info window
            //  note:
            //    takes an optional text parameter.
            //    if this is passed in, map adds an infowindow and graphic at the given point
                        
            if(util.isNumber(x,y,level)){
                try{
                    //  forwards valid params to map dijit
                    this.attachMap.centerAndZoom(x,y,level,text);
                }catch(err){
                    
                }
            }else{
                
            }
        },
        zoomToScaleAndPoint:function(/*Number*/x,/*Number*/y,/*Number*/scale,/*String?*/text){
            //  summary:
            //    zooms map to given point and zoom level
            //  params:
            //    x = BNG Easting
            //    y = BNG Northing
            //    scale = Map scale
            //    text = (Optional)Text to show in info window
            //  note:
            //    takes an optional text parameter.
            //    if this is passed in, map adds an infowindow and graphic at the given point    
                        
            if(util.isNumber(x,y,scale)){
                try{
                    //  forwards valid params to map dijit
                    this.attachMap.centerAndScale(x,y,scale,text);
                }catch(err){
                    
                }
            }else{
                
            }
        },
        zoomToExtent:function(/*Number*/xmin,/*Number*/ymin,/*Number*/xmax,/*Number*/ymax,/*Boolean?*/fit){
            //  summary:
            //    zooms map to the extent provided
            //  note:
            //    Optional fit parameter ensures the extent is within the resulting map extent
            //    Default is true
                        
            if(util.isNumber(xmin,ymin,xmax,ymax)){
                try{
                    fit = util.isBoolean(fit)? fit : true;
                    //  takes valid params and forwards to map dijit
                    this.attachMap.zoomToExtent(
                        new esri.geometry.Extent(  //  new Extent object
                            util.parseNumber(xmin),util.parseNumber(ymin),
                            util.parseNumber(xmax),util.parseNumber(ymax),
                            this.attachMap.getMap().spatialReference  //  use map spatial reference
                        ),
                        fit  //  fit extent in map extent
                    );
                }catch(err){
                    
                }
            }else{
                
            }
        },
        setActiveTool: function(/*String*/type){
            //  summary:
            //    takes a tool type string & sets current map action and selected toolbar item to match that
            //  note:
            //    toolbar type strings are stored in ESRIUK.Config.toolBar.typeEnum
            
            if(util.isStringPopulated(type)){
                try{
                    //  forward valid param to map
                    this.attachMap.setActiveTool(type);
                }catch(err){
                    
                    config.events.error({message:this.declaredClass+'.'+arguments.callee.nom+' failed ',exception:err});                       
                }
            }else{
                
            }
        },
        addIconAndInfoWindow: function(/*Number*/x,/*Number*/y,/*String*/text){
            //  summary:
            //    Adds a map graphic at given point and associates text with the graphic
            //    Then opens an infoWindow for the graphic showing the point text and options
            
            if(util.isNumber(x,y)&&util.isStringPopulated(text)){
                try{
                    //  forward valid params to map
                    this.attachMap.addIconAndInfoWindow(util.parseNumber(x),util.parseNumber(y),text);
                }catch(err){
                    
                    config.events.error({message:this.declaredClass+'.'+arguments.callee.nom+' failed ',exception:err});                    
                }
            }else{
                
            }
        },
        zoomToFullExtent:function(){
            //  summary:
            //    Zooms map to full extent
               
            try{
                this.attachMap.zoomToFullExtent();
            }catch(err){
                
            }
        },
        zoomPrevious: function() {
            //  summary:
            //    Zooms map to previous extent
               
            try{
                this.attachMap.zoomToPreviousExtent();
            }catch(err){
                
            }      
        },        
        toggleLayer:function(/*String*/layerID,/*Boolean*/on){
            //  summary:
            //    Toggles a single layer visibility
            
            //  NOT SUPPORTED
                      
            return;
            
            if(util.isString(layerID)&&util.isBoolean(on)){
                try{
                    this.attachMap.toggleLayer(layerID,util.parseBool(on));
                }catch(err){
                    
                }
            }else{
                
            }
        },
        setLayerState:function(/*Object*/layers){
            //  summary:
            //    ToDo
                              
            if(util.isObject(layers)){
                try{
                    this.attachMap.setLayerState(layers);
                }catch(err){
                    
                    config.events.error({message:this.declaredClass+'.'+arguments.callee.nom+' failed ',exception:err});                       
                }
            }else{
                
            }
        },
        addStartEndPt:function(/*Object*/pt){
            //  summary:
            //    adds a start/via/end point to the map image
                                
            if(util.isValidXY(pt)){
                pt = util.validateXY(pt);
                try{
                    this.attachMap.addStartEndPt(pt);
                }catch(err){
                    
                    config.events.error({message:this.declaredClass+'.'+arguments.callee.nom+' failed ',exception:err});                       
                }
            }else{
                
            }
        },
        clearPoints:function(/*String?*/type){
            //  summary:
            //    clears points from the map image
                                
            try{
                this.attachMap.clearPoints(type);
            }catch(err){
                
            }
        },
        getLayerList:function(){
            //  summary:
            //    ToDo
            
            var returnVar = null;
            try{
                returnVar = this.attachMap.getLayerList();
            }catch(err){
                
            }        
            return returnVar;
        },
        getMapProperties:function(){
            //  summary:
            //    ToDo
            
            var returnVar = null;
            try{
                returnVar = this.attachMap.getMapProperties();
            }catch(err){
                
            }        
            return returnVar;
        },
        setTravelNews:function(/*Object*/settings){
            //  summary:
            //    ToDo
                                
            if(util.isObject(settings)){
                try{
                    this.attachMap.setTravelNews(settings);
                }catch(err){
                    
                    config.events.error({message:this.declaredClass+'.'+arguments.callee.nom+' failed ',exception:err});                    
                }
            }else{
                
            }
        },
        findJunctionPoint:function(/*String*/TOID_a,/*String*/TOID_b,/*Number?*/level,/*Boolean?*/zoomTo){
            //  summary:
            //    zooms map to junction of two ITN road TOIDs
            //    with optional zoom level & zoom to parameter
            
            var returnVar = null;
            level = level || null;
            zoomTo = zoomTo || false;
            if(util.isStringPopulated(TOID_a)&&util.isStringPopulated(TOID_b)){
                try{
                    returnVar = this.attachMap.findJunctionPoint(TOID_a,TOID_b,level,zoomTo);
                }catch(err){
                    
                    config.events.error({message:this.declaredClass+'.'+arguments.callee.nom+' failed ',exception:err});                       
                }
            }else{
                
            }
            return returnVar;
        },
        findITNNodePoint:function(/*String*/TOID,/*Number?*/level,/*Boolean?*/zoomTo){
            //  summary:
            //    zooms map to ITN Node point
            //    with optional zoom level & zoom to parameter
            
            var returnVar = null;
            level = level || null;
            zoomTo = zoomTo || false;            
            if(util.isStringPopulated(TOID)){
                try{
                    returnVar = this.attachMap.findITNNodePoint(TOID,level,zoomTo);
                }catch(err){
                    
                    config.events.error({message:this.declaredClass+'.'+arguments.callee.nom+' failed ',exception:err});                       
                }
            }else{
                
            }
            return returnVar;            
        },
        getPrintImage:function(/*Number|String*/width,/*Number|String*/height,/*Number|String*/dpi){
            //  summary:
            //    gets a print image url with given width,height & dpi settings
            //  returns:
            //    Image URL for print image
            
            var returnString = null;
            if(util.isNumber(width,height,dpi)){
                try{
                    returnString = this.attachMap.getPrintImage(
                        util.parseNumber(width),
                        util.parseNumber(height),
                        util.parseNumber(dpi)
                    );
                }catch(err){
                    
                    config.events.error({message:this.declaredClass+'.'+arguments.callee.nom+' failed ',exception:err});                       
                }
            }else{
                
            }
            return returnString;
        },
        addRoute:function(/*String*/sessionID,/*Number*/routeNumber,/*String*/type,/*String*/zoomTo){
            //  summary:
            //    Adds a route to the map and optionally zooms to the route extent
            //  zoomTo:
            //    Optional zoom to all or this route
            //    Values: 'all' || 'this'
            //  returns:
            //    Extent of the route or null if failed
            
            var returnVar = null;
            zoomTo = util.isStringPopulated(zoomTo)? zoomTo : '';
            if(util.isStringPopulated(sessionID)&&util.isNumber(routeNumber)&&util.isStringPopulated(type)){
                try{
                    returnVar = this.attachMap.addRoute(sessionID,util.parseNumber(routeNumber),type,zoomTo);
                }catch(err){
                    
                    config.events.error({message:this.declaredClass+'.'+arguments.callee.nom+' failed ',exception:err});                       
                }
            }else{
                
            }
            return returnVar;            
        },
        clearRoutes:function(/*String?*/type){
            //  summary:
            //    Clears all routes from map
            //  params:
            //    type = optional route type to clear
            //  returns:
            //    True/False depending on success of call
            
            var returnVar = false;
            type = util.isStringPopulated(type)? type : '';
            try{
                this.attachMap.clearRoutes(type);
                returnVar = true;
            }catch(err){
                
            }
            return returnVar;
        },
        zoomToRoute:function(/*String*/sessionID,/*Number*/routeNumber,/*String*/type){
            //  summary:
            //    Zooms map to appropriate route
            //  returns:
            //    Extent of the route or null if failed
            
            var returnVar = null;            
            if(util.isStringPopulated(sessionID)&&util.isNumber(routeNumber)&&util.isStringPopulated(type)){
                try{
                    returnVar = this.attachMap.zoomToRoute(sessionID,util.parseNumber(routeNumber),type);
                }catch(err){
                    
                }
            }else{
                
            }
            return returnVar;
        },
        zoomtoAllAddedRoutes:function(){
            //  summary:
            //    Zooms map to all PT & Road routes
            //  returns:
            //    Extent of the route or null if failed
            
            var returnVar = null;            
            try{
                returnVar = this.attachMap.zoomToAllAddedRoues();
            }catch(err){
                
            }
            return returnVar; 
        },
        getNumberOfCycleImages:function(/*Number*/scale){
            //  summary:
            //    Requests number of images for a cycle print at given scale
            //  returns:
            //    Number of images or -1 if failed
            
            var returnVar = -1;            
            if(util.isNumber(scale)){
                try{
                    returnVar = this.attachMap.getNumberOfCycleImages(util.parseNumber(scale));
                }catch(err){
                    
                    config.events.error({message:this.declaredClass+'.'+arguments.callee.nom+' failed ',exception:err});                       
                }
            }else{
                
            }
            return returnVar;    
        },
        getCyclePrintDetails:function(/*Number*/scale){
            //  summary:
            //    Gets CycleRouteInfo for cycle print at given scale
            //  returns:
            //    Array of CycleRouteInfo objects or null if failed
            
            var returnVar = null;            
            if(util.isNumber(scale)){
                try{
                    returnVar = this.attachMap.getCyclePrintDetails(util.parseNumber(scale));
                }catch(err){
                    
                    config.events.error({message:this.declaredClass+'.'+arguments.callee.nom+' failed ',exception:err});                       
                }
            }else{
                
            }
            return returnVar;            
        },
    /* END: Public Methods */
    /* START: Input Properties */
        setScaleInput:function(/*Number*/scale){
            if(util.isNumber(scale)){
                if(typeof scale==="string"){ scale = util.parseNumber(scale); }
                this.settings.scale = scale;
                if(!this.settings.set){ this.settings.set = true; }
            }
        },
        setLevelInput:function(/*Number*/level){
            if(util.isNumber(level)){
                if(typeof level==="string"){ level = util.parseNumber(level); }
                //  correction for 0 based array
                //  public facing array is 1 based
                if(level>0){ level--; }
                this.settings.level = level;
                if(!this.settings.set){ this.settings.set = true; }
            }
        },
        setLocationInput:function(/*Object*/location){
            if(util.isObject(location)){
                if(util.isNumber(location.x,location.y)){
                    if(typeof location.x==="string"){ location.x = util.parseNumber(location.x); }
                    if(typeof location.y==="string"){ location.y = util.parseNumber(location.y); }
                    this.settings.location = location;
                    if(!this.settings.set){ this.settings.set = true; }
                }
            }
        },
        setExtentInput:function(/*Object*/ext){
            if(util.isValidExt(ext)){
                if(util.isString(ext.xmin)){ ext.xmin = util.parseNumber(ext.xmin); }
                if(util.isString(ext.ymin)){ ext.ymin = util.parseNumber(ext.ymin); }
                if(util.isString(ext.xmax)){ ext.xmax = util.parseNumber(ext.xmax); }
                if(util.isString(ext.ymax)){ ext.ymax = util.parseNumber(ext.ymax); }                    
                this.settings.extent = ext;
                if(!this.settings.set){ this.settings.set = true; }
            }
        },
        setTextInput:function(/*String*/text){
            if(util.isStringPopulated(text)){
                this.settings.text = text;
                if(!this.settings.set){ this.settings.set = true; }
            }
        },
        setToolsInput:function(/*String*/text){
            if(util.isStringPopulated(text)){
                var arr = text.split(',');
                this.settings.tools = [];
                for(var i=0;i<arr.length;i++){
                    if(util.isStringPopulated(arr[i])){
                        this.settings.tools.push(arr[i]);
                    }
                }
                if(!this.settings.set){ this.settings.set = true; }
            }
        },
        setTravelNewsInput:function(/*String*/text){
            if(util.isStringPopulated(text)){
                var arr = text.split(','),
                    obj = {};
                try{    
                    for(var i=0;i<arr.length;i++){
                        var pair = arr[i].split(':');
                        obj[pair[0]]=pair[1];
                    }
                    this.settings.travelNews = this.attachMap.validateTravelFilter(obj);
                    if(!this.settings.set){ this.settings.set = true; }
                }catch(err){
                    
                    config.events.error({message:this.declaredClass+'.'+arguments.callee.nom+' failed ',exception:err});          
                }
            }
        },
        setSymbolsInput:function(/*String*/text){
            if(util.isStringPopulated(text)){
                var obj = dojo.fromJson(text),
                    arr = [];
                try{    
                    for(var i=0;i<obj.length;i++){
                        if(util.isValidXY(obj[i])){
                            arr.push(obj[i]);
                        }
                    }
                    this.settings.symbols = arr;
                    if(!this.settings.set){ this.settings.set = true; }
                }catch(err){
                    
                    config.events.error({message:this.declaredClass+'.'+arguments.callee.nom+' failed ',exception:err});                    
                }
            }
        },
        setRouteInput:function(/*String*/text){
            if(util.isStringPopulated(text)){
                var obj = dojo.fromJson(text),
                    arr = [];
                try{    
                    for(var i=0;i<obj.length;i++){
                        var o = obj[i],
                            s = o.type;
                        if(s==='PT'||s==='Road'||s==='Cycle'){
                            arr.push(o)
                        }
                    }
                    this.settings.routes = arr;
                    if(!this.settings.set){ this.settings.set = true; }
                }catch(err){
                    
                    config.events.error({message:this.declaredClass+'.'+arguments.callee.nom+' failed ',exception:err});                    
                }
            }
        },
        setModeInput:function(/*String*/text){
            if(util.isStringPopulated(text)){
                var arr = text.split(',');
                this.settings.mode = [];
                for(var i=0;i<arr.length;i++){
                    if(util.isStringPopulated(arr[i])){
                        this.settings.mode.push(arr[i]);
                    }
                }
                if(!this.settings.set){ this.settings.set = true; }
            }
        },
        setHeightInput:function(/*String*/height){
            if(util.isNumber(height)){
                if(typeof height==="string"){ height = util.parseNumber(height); }
                this.settings.height = height;
                if(!this.settings.set){ this.settings.set = true; }
            }
        },
        setWidthInput:function(/*String*/width){
            if(util.isNumber(width)){
                if(typeof width==="string"){ width = util.parseNumber(width); }
                this.settings.width = width;
                if(!this.settings.set){ this.settings.set = true; }
            }
        },
        getScaleInput:function(){
            return this.settings.scale;
        },
        getLevelInput:function(){
            return this.settings.level;
        },
        getLocationInput:function(){
            return this.settings.location;
        },
        getExtentInput:function(){
            return this.settings.extent;
        },
        getTextInput:function(){
            return this.settings.text;
        },
        getToolsInput:function(){
            return this.settings.tools;
        },
        getHeightInput:function(){
            return this.settings.height;
        },
        getWidthInput:function(){
            return this.settings.width;
        },
        getTravelNewsInput:function(){
            return this.settings.travelNews;
        },
        getSymbolsInput:function(){
            return this.settings.symbols;
        },
        getRoutesInput:function(){
            return this.settings.routes;
        },        
        getModeInput:function(){
            return this.settings.mode;
        },
        getMapParams:function(){
            //  summary:
            //    Retrieves all relevant & valid settings to use for initiating the child map
            
            var settings = {wholePage:this.param_WholePage},
                pt = this.getLocationInput(),
                ext = this.getExtentInput();
            if(this.settings.set){
                if(util.isValidXY(pt)){
                    dojo.mixin(settings,{location:pt});
                }else if(util.isValidExt(ext)){
                    dojo.mixin(settings,{extent:ext});
                }
                if(util.isNumber(this.getScaleInput())){
                    dojo.mixin(settings,{scale:this.getScaleInput()});
                }else if(util.isNumber(this.getLevelInput())){
                    dojo.mixin(settings,{level:this.getLevelInput()});
                }
                if(util.isStringPopulated(this.getTextInput())){    dojo.mixin(settings,{text:this.getTextInput()}); }
                if(util.isNumber(this.getHeightInput())){           dojo.mixin(settings,{height:this.getHeightInput()}); }
                if(util.isNumber(this.getWidthInput())){            dojo.mixin(settings,{width:this.getWidthInput()}); }
                if(util.isObject(this.getTravelNewsInput())){       dojo.mixin(settings,{travelNews:this.getTravelNewsInput()}); }
                if(util.isArrayPopulated(this.getSymbolsInput())){  dojo.mixin(settings,{symbols:this.getSymbolsInput()}); }
                if(util.isArrayPopulated(this.getRoutesInput())){  dojo.mixin(settings,{routes:this.getRoutesInput()}); }
                if(util.isArrayPopulated(this.getModeInput())){     dojo.mixin(settings,{mode:this.getModeInput()}); }
            }
            
            return settings;
        },
    /* END: Input Properties */    
    /* START: Private methods */
        _addRequiredCSSFiles:function(){
            //  summary:
            //    adds config defined - required css 'link' tags to document head dynamically
            //    this reduces the amount of link tags placed in the main document
                    
            if(util.isArrayPopulated(config.requiredCSSFiles)){
                var head = document.getElementsByTagName('head')[0],
                    suffix = config.live?'':'?r='+Math.random()*1000;
                for(var i=0;i<config.requiredCSSFiles.length;i++){
                    dojo.create(
                        'link',
                        {type:"text/css",rel:"stylesheet",href:config.requiredCSSFiles[i]+suffix},
                        head
                    );
                }
            }
        },
        _applyStyleParams:function(/*Boolean*/isInit){
            //  summary:
            //    gathers map settings (URL and inline) and applies to the map container
             
            //  is the system in init mode?
            isInit = util.isBoolean(isInit)? isInit : false;
            var styleParams = null;
            //  if map is to take whole page then apply different settings
            //  this can be set using the inline param_WholePage='true' parameter
            if(!this.param_WholePage){
                if(isInit){
                    var defaults = config.map.defaultDimensions;
                    //  if height has not been set - look in esriuk config
                    if(util.isNothing(this.getHeightInput())){
                        if(util.isNothing(defaults)&&!util.isNumber(defaults.height)){
                            //  if nothing set in esriuk config - use esri config values
                            this.setHeight(esri.config.defaults.map.height);
                        }else{
                            //  use esriuk config value
                            this.setHeightInput(defaults.height);
                        }
                        
                    }
                    //  if width has not been set - look in esriuk config
                    if(util.isNothing(this.getWidthInput())){
                        if(util.isNothing(defaults)&&!util.isNumber(defaults.width)){
                            //  if nothing set in esriuk config - use esri config values
                            this.setWidthInput(esri.config.defaults.map.width);
                        }else{
                            //  use esriuk config value
                            this.setWidthInput(defaults.width);
                        }
                        
                    }
                }
                //  formulate style parameters to apply to main div
                var height = this.getHeightInput(),
                    width = this.getWidthInput();
                if(util.isNumber(height)){
                    styleParams = {height:height+"px"};
                }
                if(util.isNumber(width)){
                    if(util.isNothing(styleParams)){
                        styleParams = {width:width+"px"};
                    }else{
                        styleParams.width = width+"px";
                    }
                }
                //  apply style params
                dojo.style(this.domNode,styleParams);
                                
            }else{
                //  assume that this.domNode.parentNode is same as document.body
                //  set up whole page style params
                this._wholePageResize();
            }
        },
        _wholePageResize:function(){
            //  summary:
            //    foramts style params for an entire-page map dijit
            //  returns:
            //    new dimensions of map container
            
            //  get dimensions of parent nodwe (assumed to be document.body)
            var parentCoords = dojo.coords(this.domNode.parentNode);
            //  work out 'padding' css 'gutters'
            var vGutter = util.getElmGutterVertical(this.domNode.parentNode) + util.getElmGutterVertical(this.domNode);
            var hGutter = util.getElmGutterHorizontal(this.domNode.parentNode) + util.getElmGutterHorizontal(this.domNode);
            var h = (parentCoords.h-vGutter-2);
            var w = (parentCoords.w-hGutter-2);
            //  set dims to these new values
            this.setHeightInput(h);
            this.setWidthInput(w);               
            dojo.style(this.domNode,{
                height:h+'px',
                width:w+'px'
            });
            //  log whole page message
                        
            //  return new size of map
            return {height:h,width:w};
        },
        _readURLSettings:function(){
            //  summary:
            //    reads parameters from the page URL
            
            //  if inline settings have not been set, try url params        
            if(!this.settings.set){
                if(window.location.search) {
                    // convert our query string into an object (use slice to strip the leading "?")
                    var params = dojo.queryToObject(window.location.search.slice(1));
                    if(!util.isNothing(params.scale)){ this.setScaleInput(params.scale); }
                    if(!util.isNothing(params.level)){ this.setLevelInput(params.level); }
                    if(!util.isNothing(params.mapWidth)){ this.setWidthInput(params.mapWidth); }
                    if(!util.isNothing(params.mapHeight)){ this.setHeightInput(params.mapHeight); }   
                    if(util.isNumber(params.x)&&util.isNumber(params.y)){ 
                        this.setLocationInput({x:params.x,y:params.y});
                    }
                    
                    //  ToDo:   add new reading params here
                    
                }
            }
        },
        _readInlineSettings:function(){
            //  summary:
            //    reads inline / programmatic parameters & validates them
               
            //  if settings have not been set, try inline elements
            if(!this.settings.set){
                if(!util.isNothing(this.param_Scale)){ this.setScaleInput(this.param_Scale); }
                if(!util.isNothing(this.param_Level)){ this.setLevelInput(this.param_Level); }                
                if(!util.isNothing(this.param_Height)){ this.setHeightInput(this.param_Height); }
                if(!util.isNothing(this.param_Width)){ this.setWidthInput(this.param_Width); }
                if(!util.isNothing(this.param_LocationX,this.param_LocationY)){
                    this.setLocationInput({x:util.parseNumber(this.param_LocationX),y:util.parseNumber(this.param_LocationY)}); 
                }else if(!util.isNothing(this.param_XMin,this.param_YMin,this.param_XMax,this.param_YMax)){
                    this.setExtentInput({
                        xmin:util.parseNumber(this.param_XMin),ymin:util.parseNumber(this.param_YMin),
                        xmax:util.parseNumber(this.param_XMax),ymax:util.parseNumber(this.param_YMax)
                    }); 
                }
                if(util.isStringPopulated(this.param_Text)){ this.setTextInput(this.param_Text); }
                if(util.isStringPopulated(this.param_Tools)){ this.setToolsInput(this.param_Tools); }
                if(util.isStringPopulated(this.param_TravelNews)){ this.setTravelNewsInput(this.param_TravelNews); }
                if(util.isStringPopulated(this.param_Symbols)){ this.setSymbolsInput(this.param_Symbols); }
                if(util.isStringPopulated(this.param_Routes)){ this.setRouteInput(this.param_Routes); }                
                if(util.isStringPopulated(this.param_Mode)){ this.setModeInput(this.param_Mode); }
                this.param_WholePage = util.parseBool(this.param_WholePage);
            }
        },
        showPanels:function(/*Array[String]*/arr){
           
          
          
          
          var count = arr.length,
              showing = false,
              item;
              
          for(var i=0;i<count;i++){
            item = arr[i];
            if(item==='pan'){
              showing = true;
              dojo.style(this.attachNavPanel.domNode,{display:'block'});
            }else if(item==='zoom'){
              showing = true;
              dojo.style(this.attachMapSliderContent.parentNode,{display:'block'});
            }
          }
          
          if(!showing){
            dojo.addClass(this.attachToolBarPanel.domNode,'toolbarPanelContainerFlush');
          }
        },        
        _addBugFixes:function(){
            //  summary:
            //    Bug fix for page scroll error
            //    http://forums.esri.com/Thread.asp?c=158&f=2396&t=289272&mc=3#msgid899791
               
            
            var map = this.attachMap.getMap();
            
            
            // FIX that take into account the position in the document
            //(vs. in the browser window as before).
            dojo.global.esri.Map.prototype.reposition = function() {
                //var pos = dojo.coords(this.container),
                var pos = dojo.coords(this.container, true),
                    brdr = dojo._getBorderExtents(this.container);
                this.position.setX(pos.x+brdr.l);
                this.position.setY(pos.y+brdr.t);
                this.onReposition(this.position.x,this.position.y);
            };
            
            //  Disable double click map zoom
            

        }
    /* END: Private methods */
    };
})());

}

