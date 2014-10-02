/*
	Copyright (c) 2004-2009, The Dojo Foundation All Rights Reserved.
	Available via Academic Free License >= 2.1 OR the modified BSD license.
	see: http://dojotoolkit.org/license for details
*/


dojo._xdResourceLoaded(function(_1,_2,_3){return {depends:[["provide","dojox.analytics.Urchin"]],defineResource:function(_4,_5,_6){if(!_4._hasResource["dojox.analytics.Urchin"]){_4._hasResource["dojox.analytics.Urchin"]=true;_4.provide("dojox.analytics.Urchin");_4.declare("dojox.analytics.Urchin",null,{acct:_4.config.urchin,loadInterval:42,decay:0.5,timeout:4200,constructor:function(_7){this.tracker=null;_4.mixin(this,_7);this._loadGA();},_loadGA:function(){var _8=("https:"==document.location.protocol)?"https://ssl.":"http://www.";_4.create("script",{src:_8+"google-analytics.com/ga.js"},_4.doc.getElementsByTagName("head")[0]);setTimeout(_4.hitch(this,"_checkGA"),this.loadInterval);},_checkGA:function(){if(this.loadInterval>this.timeout){return;}setTimeout(_4.hitch(this,!window["_gat"]?"_checkGA":"_gotGA"),this.loadInterval);this.loadInterval*=(this.decay+1);},_gotGA:function(){this.tracker=_gat._getTracker(this.acct);this.tracker._initData();this.GAonLoad.apply(this,arguments);},GAonLoad:function(){this.trackPageView();},trackPageView:function(_9){this.tracker._trackPageview.apply(this,arguments);}});}}};});