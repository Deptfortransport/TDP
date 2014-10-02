/*
	Copyright (c) 2004-2009, The Dojo Foundation All Rights Reserved.
	Available via Academic Free License >= 2.1 OR the modified BSD license.
	see: http://dojotoolkit.org/license for details
*/


dojo._xdResourceLoaded(function(_1,_2,_3){return {depends:[["provide","dojo.regexp"]],defineResource:function(_4,_5,_6){if(!_4._hasResource["dojo.regexp"]){_4._hasResource["dojo.regexp"]=true;_4.provide("dojo.regexp");_4.regexp.escapeString=function(_7,_8){return _7.replace(/([\.$?*|{}\(\)\[\]\\\/\+^])/g,function(ch){if(_8&&_8.indexOf(ch)!=-1){return ch;}return "\\"+ch;});};_4.regexp.buildGroupRE=function(_a,re,_c){if(!(_a instanceof Array)){return re(_a);}var b=[];for(var i=0;i<_a.length;i++){b.push(re(_a[i]));}return _4.regexp.group(b.join("|"),_c);};_4.regexp.group=function(_f,_10){return "("+(_10?"?:":"")+_f+")";};}}};});