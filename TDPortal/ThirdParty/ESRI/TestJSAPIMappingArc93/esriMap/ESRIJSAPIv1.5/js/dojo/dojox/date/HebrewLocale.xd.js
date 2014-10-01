/*
	Copyright (c) 2004-2009, The Dojo Foundation All Rights Reserved.
	Available via Academic Free License >= 2.1 OR the modified BSD license.
	see: http://dojotoolkit.org/license for details
*/


dojo._xdResourceLoaded(function(_1,_2,_3){return {depends:[["provide","dojox.date.HebrewLocale"],["require","dojox.date.HebrewDate"],["require","dojox.date.HebrewNumerals"],["require","dojo.regexp"],["require","dojo.string"],["require","dojo.i18n"],["requireLocalization","dojo.cldr","hebrew",null,"ROOT,he","ROOT,he"]],defineResource:function(_4,_5,_6){if(!_4._hasResource["dojox.date.HebrewLocale"]){_4._hasResource["dojox.date.HebrewLocale"]=true;_4.provide("dojox.date.HebrewLocale");_4.experimental("dojox.date.HebrewLocale");_4.require("dojox.date.HebrewDate");_4.require("dojox.date.HebrewNumerals");_4.require("dojo.regexp");_4.require("dojo.string");_4.require("dojo.i18n");(function(){function _7(_8,_9,_a,_b,_c){return _c.replace(/([a-z])\1*/ig,function(_d){var s,_f;var c=_d.charAt(0);var l=_d.length;var _12=["abbr","wide","narrow"];switch(c){case "y":if(_a=="he"){s=_6.date.HebrewNumerals.getYearHebrewLetters(_8.getFullYear());}else{s=String(_8.getFullYear());}break;case "M":var m=_8.getMonth();if(l<3){if(!_8.isLeapYear(_8.getFullYear())&&m>5){m--;}if(_a=="he"){s=_6.date.HebrewNumerals.getMonthHebrewLetters(m);}else{s=m+1;_f=true;}}else{if(!_8.isLeapYear(_8.getFullYear())&&m==6){m--;}var _14=["months","format",_12[l-3]].join("-");s=_9[_14][m];}break;case "d":if(_a=="he"){s=_6.date.HebrewNumerals.getDayHebrewLetters(_8.getDate());}else{s=_8.getDate();_f=true;}break;case "E":var d=_8.getDay();if(l<3){s=d+1;_f=true;}else{var _16=["days","format",_12[l-3]].join("-");s=_9[_16][d];}break;case "a":var _17=(_8.getHours()<12)?"am":"pm";s=_9[_17];break;case "h":case "H":case "K":case "k":var h=_8.getHours();switch(c){case "h":s=(h%12)||12;break;case "H":s=h;break;case "K":s=(h%12);break;case "k":s=h||24;break;}_f=true;break;case "m":s=_8.getMinutes();_f=true;break;case "s":s=_8.getSeconds();_f=true;break;case "S":s=Math.round(_8.getMilliseconds()*Math.pow(10,l-3));_f=true;break;default:throw new Error("dojox.date.HebrewLocale.formatPattern: invalid pattern char: "+_c);}if(_f){s=_4.string.pad(s,l);}return s;});};_6.date.HebrewLocale.format=function(_19,_1a){_1a=_1a||{};var _1b=_4.i18n.normalizeLocale(_1a.locale);var _1c=_1a.formatLength||"short";var _1d=_6.date.HebrewLocale._getHebrewBundle(_1b);var str=[];var _1f=_4.hitch(this,_7,_19,_1d,_1b,_1a.fullYear);if(_1a.selector!="time"){var _20=_1a.datePattern||_1d["dateFormat-"+_1c];if(_20){str.push(_21(_20,_1f));}}if(_1a.selector!="date"){var _22=_1a.timePattern||_1d["timeFormat-"+_1c];if(_22){str.push(_21(_22,_1f));}}var _23=str.join(" ");return _23;};_6.date.HebrewLocale.regexp=function(_24){return _6.date.HebrewLocale._parseInfo(_24).regexp;};_6.date.HebrewLocale._parseInfo=function(_25){_25=_25||{};var _26=_4.i18n.normalizeLocale(_25.locale);var _27=_6.date.HebrewLocale._getHebrewBundle(_26);var _28=_25.formatLength||"short";var _29=_25.datePattern||_27["dateFormat-"+_28];var _2a=_25.timePattern||_27["timeFormat-"+_28];var _2b;if(_25.selector=="date"){_2b=_29;}else{if(_25.selector=="time"){_2b=_2a;}else{_2b=(typeof (_2a)=="undefined")?_29:_29+" "+_2a;}}var _2c=[];var re=_21(_2b,_4.hitch(this,_2e,_2c,_27,_25));return {regexp:re,tokens:_2c,bundle:_27};};_6.date.HebrewLocale.parse=function(_2f,_30){if(!_30){_30={};}var _31=_6.date.HebrewLocale._parseInfo(_30);var _32=_31.tokens,_33=_31.bundle;var re=new RegExp("^"+_31.regexp+"$");var _35=re.exec(_2f);var _36=_4.i18n.normalizeLocale(_30.locale);if(!_35){console.debug("dojox.date.HebrewLocale.parse: value  "+_2f+" doesn't match pattern   "+re);return null;}var _37,_38;var _39=[5730,3,23,0,0,0,0];var _3a="";var _3b=0;var _3c=["abbr","wide","narrow"];var _3d=_4.every(_35,function(v,i){if(!i){return true;}var _40=_32[i-1];var l=_40.length;switch(_40.charAt(0)){case "y":if(_36=="he"){_39[0]=_6.date.HebrewNumerals.parseYearHebrewLetters(v);}else{_39[0]=Number(v);}break;case "M":if(l>2){var _42=_33["months-format-"+_3c[l-3]].concat();if(!_30.strict){v=v.replace(".","").toLowerCase();_42=_4.map(_42,function(s){return s.replace(".","").toLowerCase();});}v=_4.indexOf(_42,v);if(v==-1){return false;}_3b=l;}else{if(_36=="he"){v=_6.date.HebrewNumerals.parseMonthHebrewLetters(v);}else{v--;}}_39[1]=Number(v);break;case "D":_39[1]=0;case "d":if(_36=="he"){_39[2]=_6.date.HebrewNumerals.parseDayHebrewLetters(v);}else{_39[2]=Number(v);}break;case "a":var am=_30.am||_33.am;var pm=_30.pm||_33.pm;if(!_30.strict){var _46=/\./g;v=v.replace(_46,"").toLowerCase();am=am.replace(_46,"").toLowerCase();pm=pm.replace(_46,"").toLowerCase();}if(_30.strict&&v!=am&&v!=pm){return false;}_3a=(v==pm)?"p":(v==am)?"a":"";break;case "K":if(v==24){v=0;}case "h":case "H":case "k":_39[3]=Number(v);break;case "m":_39[4]=Number(v);break;case "s":_39[5]=Number(v);break;case "S":_39[6]=Number(v);}return true;});var _47=+_39[3];if(_3a==="p"&&_47<12){_39[3]=_47+12;}else{if(_3a==="a"&&_47==12){_39[3]=0;}}var _48=new _6.date.HebrewDate(_39[0],_39[1],_39[2],_39[3],_39[4],_39[5],_39[6]);if((_3b>2)&&(_39[1]>5)&&!_48.isLeapYear(_48.getFullYear())){_48=new _6.date.HebrewDate(_39[0],_39[1]-1,_39[2],_39[3],_39[4],_39[5],_39[6]);}return _48;};function _21(_49,_4a,_4b,_4c){var _4d=function(x){return x;};_4a=_4a||_4d;_4b=_4b||_4d;_4c=_4c||_4d;var _4f=_49.match(/(''|[^'])+/g);var _50=_49.charAt(0)=="'";_4.forEach(_4f,function(_51,i){if(!_51){_4f[i]="";}else{_4f[i]=(_50?_4b:_4a)(_51);_50=!_50;}});return _4c(_4f.join(""));};function _2e(_53,_54,_55,_56){_56=_4.regexp.escapeString(_56);var _57=_4.i18n.normalizeLocale(_55.locale);return _56.replace(/([a-z])\1*/ig,function(_58){var s;var c=_58.charAt(0);var l=_58.length;var p2="",p3="";if(_55.strict){if(l>1){p2="0"+"{"+(l-1)+"}";}if(l>2){p3="0"+"{"+(l-2)+"}";}}else{p2="0?";p3="0{0,2}";}switch(c){case "y":s="\\S+";break;case "M":if(_57=="he"){s=(l>2)?"\\S+ ?\\S+":"\\S{1,4}";}else{s=(l>2)?"\\S+ ?\\S+":p2+"[1-9]|1[0-2]";}break;case "d":if(_57=="he"){s="\\S['\"']{1,2}\\S?";}else{s="[12]\\d|"+p2+"[1-9]|30";}break;case "E":if(_57=="he"){s=(l>3)?"\\S+ ?\\S+":"\\S";}else{s="\\S+";}break;case "h":s=p2+"[1-9]|1[0-2]";break;case "k":s=p2+"\\d|1[01]";break;case "H":s=p2+"\\d|1\\d|2[0-3]";break;case "K":s=p2+"[1-9]|1\\d|2[0-4]";break;case "m":case "s":s=p2+"\\d|[0-5]\\d";break;case "S":s="\\d{"+l+"}";break;case "a":var am=_55.am||_54.am||"AM";var pm=_55.pm||_54.pm||"PM";if(_55.strict){s=am+"|"+pm;}else{s=am+"|"+pm;if(am!=am.toLowerCase()){s+="|"+am.toLowerCase();}if(pm!=pm.toLowerCase()){s+="|"+pm.toLowerCase();}}break;default:s=".*";}if(_53){_53.push(_58);}return "("+s+")";}).replace(/[\xa0 ]/g,"[\\s\\xa0]");};})();(function(){var _60=[];_6.date.HebrewLocale.addCustomFormats=function(_61,_62){_60.push({pkg:_61,name:_62});};_6.date.HebrewLocale._getHebrewBundle=function(_63){var _64={};_4.forEach(_60,function(_65){var _66=_4.i18n.getLocalization(_65.pkg,_65.name,_63);_64=_4.mixin(_64,_66);},this);return _64;};})();_6.date.HebrewLocale.addCustomFormats("dojo.cldr","hebrew");_6.date.HebrewLocale.getNames=function(_67,_68,_69,_6a){var _6b;var _6c=_6.date.HebrewLocale._getHebrewBundle;var _6d=[_67,_69,_68];if(_69=="standAlone"){var key=_6d.join("-");_6b=_6c(_6a)[key];if(_6b===_6c("ROOT")[key]){_6b=undefined;}}_6d[1]="format";return (_6b||_6c(_6a)[_6d.join("-")]).concat();};}}};});