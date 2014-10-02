/*
	Copyright (c) 2004-2009, The Dojo Foundation All Rights Reserved.
	Available via Academic Free License >= 2.1 OR the modified BSD license.
	see: http://dojotoolkit.org/license for details
*/


dojo._xdResourceLoaded(function(_1,_2,_3){return {depends:[["provide","dojox.lang.functional.curry"],["require","dojox.lang.functional.lambda"]],defineResource:function(_4,_5,_6){if(!_4._hasResource["dojox.lang.functional.curry"]){_4._hasResource["dojox.lang.functional.curry"]=true;_4.provide("dojox.lang.functional.curry");_4.require("dojox.lang.functional.lambda");(function(){var df=_6.lang.functional,ap=Array.prototype;var _9=function(_a){return function(){var _b=_a.args.concat(ap.slice.call(arguments,0));if(arguments.length+_a.args.length<_a.arity){return _9({func:_a.func,arity:_a.arity,args:_b});}return _a.func.apply(this,_b);};};_4.mixin(df,{curry:function(f,_d){f=df.lambda(f);_d=typeof _d=="number"?_d:f.length;return _9({func:f,arity:_d,args:[]});},arg:{},partial:function(f){var a=arguments,l=a.length,_11=new Array(l-1),p=[],i=1,t;f=df.lambda(f);for(;i<l;++i){t=a[i];_11[i-1]=t;if(t===df.arg){p.push(i-1);}}return function(){var t=ap.slice.call(_11,0),i=0,l=p.length;for(;i<l;++i){t[p[i]]=arguments[i];}return f.apply(this,t);};},mixer:function(f,mix){f=df.lambda(f);return function(){var t=new Array(mix.length),i=0,l=mix.length;for(;i<l;++i){t[i]=arguments[mix[i]];}return f.apply(this,t);};},flip:function(f){f=df.lambda(f);return function(){var a=arguments,l=a.length-1,t=new Array(l+1),i=0;for(;i<=l;++i){t[l-i]=a[i];}return f.apply(this,t);};}});})();}}};});