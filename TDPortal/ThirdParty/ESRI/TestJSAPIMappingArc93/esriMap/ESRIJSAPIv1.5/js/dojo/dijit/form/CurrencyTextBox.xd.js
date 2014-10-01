/*
	Copyright (c) 2004-2009, The Dojo Foundation All Rights Reserved.
	Available via Academic Free License >= 2.1 OR the modified BSD license.
	see: http://dojotoolkit.org/license for details
*/


dojo._xdResourceLoaded(function(_1,_2,_3){return {depends:[["provide","dijit.form.CurrencyTextBox"],["require","dojo.currency"],["require","dijit.form.NumberTextBox"]],defineResource:function(_4,_5,_6){if(!_4._hasResource["dijit.form.CurrencyTextBox"]){_4._hasResource["dijit.form.CurrencyTextBox"]=true;_4.provide("dijit.form.CurrencyTextBox");_4.require("dojo.currency");_4.require("dijit.form.NumberTextBox");_4.declare("dijit.form.CurrencyTextBox",_5.form.NumberTextBox,{currency:"",regExpGen:_4.currency.regexp,_formatter:_4.currency.format,parse:_4.currency.parse,postMixInProperties:function(){this.constraints.currency=this.currency;this.inherited(arguments);}});}}};});