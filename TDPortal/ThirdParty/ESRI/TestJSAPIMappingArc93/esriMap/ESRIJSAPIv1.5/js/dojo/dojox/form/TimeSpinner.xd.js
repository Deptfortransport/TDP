/*
	Copyright (c) 2004-2009, The Dojo Foundation All Rights Reserved.
	Available via Academic Free License >= 2.1 OR the modified BSD license.
	see: http://dojotoolkit.org/license for details
*/


dojo._xdResourceLoaded(function(_1,_2,_3){return {depends:[["provide","dojox.form.TimeSpinner"],["require","dijit.form._Spinner"],["require","dijit.form.NumberTextBox"],["require","dojo.date"],["require","dojo.date.locale"],["require","dojo.date.stamp"]],defineResource:function(_4,_5,_6){if(!_4._hasResource["dojox.form.TimeSpinner"]){_4._hasResource["dojox.form.TimeSpinner"]=true;_4.provide("dojox.form.TimeSpinner");_4.require("dijit.form._Spinner");_4.require("dijit.form.NumberTextBox");_4.require("dojo.date");_4.require("dojo.date.locale");_4.require("dojo.date.stamp");_4.declare("dojox.form.TimeSpinner",[_5.form._Spinner],{required:false,adjust:function(_7,_8){return _4.date.add(_7,"minute",_8);},isValid:function(){return true;},smallDelta:5,largeDelta:30,timeoutChangeRate:0.5,parse:function(_9,_a){return _4.date.locale.parse(_9,{selector:"time",formatLength:"short"});},format:function(_b,_c){if(_4.isString(_b)){return _b;}return _4.date.locale.format(_b,{selector:"time",formatLength:"short"});},serialize:_4.date.stamp.toISOString,value:"12:00 AM"});}}};});