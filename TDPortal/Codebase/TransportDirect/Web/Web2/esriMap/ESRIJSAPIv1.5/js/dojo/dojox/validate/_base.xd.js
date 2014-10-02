/*
	Copyright (c) 2004-2009, The Dojo Foundation All Rights Reserved.
	Available via Academic Free License >= 2.1 OR the modified BSD license.
	see: http://dojotoolkit.org/license for details
*/


dojo._xdResourceLoaded(function(_1,_2,_3){return {depends:[["provide","dojox.validate._base"],["require","dojo.regexp"],["require","dojo.number"],["require","dojox.validate.regexp"]],defineResource:function(_4,_5,_6){if(!_4._hasResource["dojox.validate._base"]){_4._hasResource["dojox.validate._base"]=true;_4.provide("dojox.validate._base");_4.experimental("dojox.validate");_4.require("dojo.regexp");_4.require("dojo.number");_4.require("dojox.validate.regexp");_6.validate.isText=function(_7,_8){_8=(typeof _8=="object")?_8:{};if(/^\s*$/.test(_7)){return false;}if(typeof _8.length=="number"&&_8.length!=_7.length){return false;}if(typeof _8.minlength=="number"&&_8.minlength>_7.length){return false;}if(typeof _8.maxlength=="number"&&_8.maxlength<_7.length){return false;}return true;};_6.validate._isInRangeCache={};_6.validate.isInRange=function(_9,_a){_9=_4.number.parse(_9,_a);if(isNaN(_9)){return false;}_a=(typeof _a=="object")?_a:{};var _b=(typeof _a.max=="number")?_a.max:Infinity,_c=(typeof _a.min=="number")?_a.min:-Infinity,_d=(typeof _a.decimal=="string")?_a.decimal:".",_e=_6.validate._isInRangeCache,_f=_9+"max"+_b+"min"+_c+"dec"+_d;if(typeof _e[_f]!="undefined"){return _e[_f];}_e[_f]=!(_9<_c||_9>_b);return _e[_f];};_6.validate.isNumberFormat=function(_10,_11){var re=new RegExp("^"+_6.validate.regexp.numberFormat(_11)+"$","i");return re.test(_10);};_6.validate.isValidLuhn=function(_13){var sum=0,_15,_16;if(!_4.isString(_13)){_13=String(_13);}_13=_13.replace(/[- ]/g,"");_15=_13.length%2;for(var i=0;i<_13.length;i++){_16=parseInt(_13.charAt(i));if(i%2==_15){_16*=2;}if(_16>9){_16-=9;}sum+=_16;}return !(sum%10);};}}};});