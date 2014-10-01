/*
	Copyright (c) 2004-2009, The Dojo Foundation All Rights Reserved.
	Available via Academic Free License >= 2.1 OR the modified BSD license.
	see: http://dojotoolkit.org/license for details
*/


dojo._xdResourceLoaded(function(_1,_2,_3){return {depends:[["provide","dojox.lang.functional.numrec"],["require","dojox.lang.functional.lambda"],["require","dojox.lang.functional.util"]],defineResource:function(_4,_5,_6){if(!_4._hasResource["dojox.lang.functional.numrec"]){_4._hasResource["dojox.lang.functional.numrec"]=true;_4.provide("dojox.lang.functional.numrec");_4.require("dojox.lang.functional.lambda");_4.require("dojox.lang.functional.util");(function(){var df=_6.lang.functional,_8=df.inlineLambda,_9=["_r","_i"];df.numrec=function(_a,_b){var a,as,_e={},_f=function(x){_e[x]=1;};if(typeof _b=="string"){as=_8(_b,_9,_f);}else{a=df.lambda(_b);as="_a.call(this, _r, _i)";}var _11=df.keys(_e),f=new Function(["_x"],"var _t=arguments.callee,_r=_t.t,_i".concat(_11.length?","+_11.join(","):"",a?",_a=_t.a":"",";for(_i=1;_i<=_x;++_i){_r=",as,"}return _r"));f.t=_a;if(a){f.a=a;}return f;};})();}}};});