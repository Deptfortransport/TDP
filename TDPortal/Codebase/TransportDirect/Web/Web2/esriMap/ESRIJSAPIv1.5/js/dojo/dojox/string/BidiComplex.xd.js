/*
	Copyright (c) 2004-2009, The Dojo Foundation All Rights Reserved.
	Available via Academic Free License >= 2.1 OR the modified BSD license.
	see: http://dojotoolkit.org/license for details
*/


dojo._xdResourceLoaded(function(_1,_2,_3){return {depends:[["provide","dojox.string.BidiComplex"]],defineResource:function(_4,_5,_6){if(!_4._hasResource["dojox.string.BidiComplex"]){_4._hasResource["dojox.string.BidiComplex"]=true;_4.provide("dojox.string.BidiComplex");_4.experimental("dojox.string.BidiComplex");(function(){var _7=[];_6.string.BidiComplex.attachInput=function(_8,_9){_8.alt=_9;_4.connect(_8,"onkeydown",this,"_ceKeyDown");_4.connect(_8,"onkeyup",this,"_ceKeyUp");_4.connect(_8,"oncut",this,"_ceCutText");_4.connect(_8,"oncopy",this,"_ceCopyText");_8.value=_6.string.BidiComplex.createDisplayString(_8.value,_8.alt);};_6.string.BidiComplex.createDisplayString=function(_a,_b){_a=_6.string.BidiComplex.stripSpecialCharacters(_a);var _c=_6.string.BidiComplex._parse(_a,_b);var _d="‪"+_a;var _e=1;_4.forEach(_c,function(n){if(n!=null){var _10=_d.substring(0,n+_e);var _11=_d.substring(n+_e,_d.length);_d=_10+"‎"+_11;_e++;}});return _d;};_6.string.BidiComplex.stripSpecialCharacters=function(str){return str.replace(/[\u200E\u200F\u202A-\u202E]/g,"");};_6.string.BidiComplex._ceKeyDown=function(_13){var _14=_4.isIE?_13.srcElement:_13.target;_7=_14.value;};_6.string.BidiComplex._ceKeyUp=function(_15){var LRM="‎";var _17=_4.isIE?_15.srcElement:_15.target;var _18=_17.value;var _19=_15.keyCode;if((_19==_4.keys.HOME)||(_19==_4.keys.END)||(_19==_4.keys.SHIFT)){return;}var _1a,_1b;var _1c=_6.string.BidiComplex._getCaretPos(_15,_17);if(_1c){_1a=_1c[0];_1b=_1c[1];}if(_4.isIE){var _1d=_1a,_1e=_1b;if(_19==_4.keys.LEFT_ARROW){if((_18.charAt(_1b-1)==LRM)&&(_1a==_1b)){_6.string.BidiComplex._setSelectedRange(_17,_1a-1,_1b-1);}return;}if(_19==_4.keys.RIGHT_ARROW){if(_18.charAt(_1b-1)==LRM){_1e=_1b+1;if(_1a==_1b){_1d=_1a+1;}}_6.string.BidiComplex._setSelectedRange(_17,_1d,_1e);return;}}else{if(_19==_4.keys.LEFT_ARROW){if(_18.charAt(_1b-1)==LRM){_6.string.BidiComplex._setSelectedRange(_17,_1a-1,_1b-1);}return;}if(_19==_4.keys.RIGHT_ARROW){if(_18.charAt(_1b-1)==LRM){_6.string.BidiComplex._setSelectedRange(_17,_1a+1,_1b+1);}return;}}var _1f=_6.string.BidiComplex.createDisplayString(_18,_17.alt);if(_18!=_1f){window.status=_18+" c="+_1b;_17.value=_1f;if((_19==_4.keys.DELETE)&&(_1f.charAt(_1b)==LRM)){_17.value=_1f.substring(0,_1b)+_1f.substring(_1b+2,_1f.length);}if(_19==_4.keys.DELETE){_6.string.BidiComplex._setSelectedRange(_17,_1a,_1b);}else{if(_19==_4.keys.BACKSPACE){if((_7.length>=_1b)&&(_7.charAt(_1b-1)==LRM)){_6.string.BidiComplex._setSelectedRange(_17,_1a-1,_1b-1);}else{_6.string.BidiComplex._setSelectedRange(_17,_1a,_1b);}}else{if(_17.value.charAt(_1b)!=LRM){_6.string.BidiComplex._setSelectedRange(_17,_1a+1,_1b+1);}}}}};_6.string.BidiComplex._processCopy=function(_20,_21,_22){if(_21==null){if(_4.isIE){var _23=document.selection.createRange();_21=_23.text;}else{_21=_20.value.substring(_20.selectionStart,_20.selectionEnd);}}var _24=_6.string.BidiComplex.stripSpecialCharacters(_21);if(_4.isIE){window.clipboardData.setData("Text",_24);}return true;};_6.string.BidiComplex._ceCopyText=function(_25){if(_4.isIE){_25.returnValue=false;}return _6.string.BidiComplex._processCopy(_25,null,false);};_6.string.BidiComplex._ceCutText=function(_26){var ret=_6.string.BidiComplex._processCopy(_26,null,false);if(!ret){return false;}if(_4.isIE){document.selection.clear();}else{var _28=_26.selectionStart;_26.value=_26.value.substring(0,_28)+_26.value.substring(_26.selectionEnd);_26.setSelectionRange(_28,_28);}return true;};_6.string.BidiComplex._getCaretPos=function(_29,_2a){if(_4.isIE){var _2b=0,_2c=document.selection.createRange().duplicate(),_2d=_2c.duplicate(),_2e=_2c.text.length;if(_2a.type=="textarea"){_2d.moveToElementText(_2a);}else{_2d.expand("textedit");}while(_2c.compareEndPoints("StartToStart",_2d)>0){_2c.moveStart("character",-1);++_2b;}return [_2b,_2b+_2e];}return [_29.target.selectionStart,_29.target.selectionEnd];};_6.string.BidiComplex._setSelectedRange=function(_2f,_30,_31){if(_4.isIE){var _32=_2f.createTextRange();if(_32){if(_2f.type=="textarea"){_32.moveToElementText(_2f);}else{_32.expand("textedit");}_32.collapse();_32.moveEnd("character",_31);_32.moveStart("character",_30);_32.select();}}else{_2f.selectionStart=_30;_2f.selectionEnd=_31;}};var _33=function(c){return (c>="0"&&c<="9")||(c>"ÿ");};var _35=function(c){return (c>="A"&&c<="Z")||(c>="a"&&c<="z");};var _37=function(_38,i,_3a){while(i>0){if(i==_3a){return false;}i--;if(_33(_38.charAt(i))){return true;}if(_35(_38.charAt(i))){return false;}}return false;};_6.string.BidiComplex._parse=function(str,_3c){var _3d=-1,_3e=[];var _3f={FILE_PATH:"/\\:.",URL:"/:.?=&#",XPATH:"/\\:.<>=[]",EMAIL:"<>@.,;"}[_3c];switch(_3c){case "FILE_PATH":case "URL":case "XPATH":_4.forEach(str,function(ch,i){if(_3f.indexOf(ch)>=0&&_37(str,i,_3d)){_3d=i;_3e.push(i);}});break;case "EMAIL":var _42=false;_4.forEach(str,function(ch,i){if(ch=="\""){if(_37(str,i,_3d)){_3d=i;_3e.push(i);}i++;var i1=str.indexOf("\"",i);if(i1>=i){i=i1;}if(_37(str,i,_3d)){_3d=i;_3e.push(i);}}if(_3f.indexOf(ch)>=0&&_37(str,i,_3d)){_3d=i;_3e.push(i);}});}return _3e;};})();}}};});