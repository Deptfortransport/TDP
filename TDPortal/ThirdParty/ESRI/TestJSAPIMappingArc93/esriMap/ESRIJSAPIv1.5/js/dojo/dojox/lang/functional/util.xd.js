/*
	Copyright (c) 2004-2009, The Dojo Foundation All Rights Reserved.
	Available via Academic Free License >= 2.1 OR the modified BSD license.
	see: http://dojotoolkit.org/license for details
*/


dojo._xdResourceLoaded(function(_1,_2,_3){return {depends:[["provide","dojox.lang.functional.util"],["require","dojox.lang.functional.lambda"]],defineResource:function(_4,_5,_6){if(!_4._hasResource["dojox.lang.functional.util"]){_4._hasResource["dojox.lang.functional.util"]=true;_4.provide("dojox.lang.functional.util");_4.require("dojox.lang.functional.lambda");(function(){var df=_6.lang.functional;_4.mixin(df,{inlineLambda:function(_8,_9,_a){var s=df.rawLambda(_8);if(_a){df.forEach(s.args,_a);}var ap=typeof _9=="string",n=ap?s.args.length:Math.min(s.args.length,_9.length),a=new Array(4*n+4),i,j=1;for(i=0;i<n;++i){a[j++]=s.args[i];a[j++]="=";a[j++]=ap?_9+"["+i+"]":_9[i];a[j++]=",";}a[0]="(";a[j++]="(";a[j++]=s.body;a[j]="))";return a.join("");}});})();}}};});