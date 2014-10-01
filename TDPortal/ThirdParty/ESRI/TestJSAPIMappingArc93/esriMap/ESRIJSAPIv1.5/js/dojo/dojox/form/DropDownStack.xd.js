/*
	Copyright (c) 2004-2009, The Dojo Foundation All Rights Reserved.
	Available via Academic Free License >= 2.1 OR the modified BSD license.
	see: http://dojotoolkit.org/license for details
*/


dojo._xdResourceLoaded(function(_1,_2,_3){return {depends:[["provide","dojox.form.DropDownStack"],["require","dojox.form.DropDownSelect"],["require","dojox.form._SelectStackMixin"]],defineResource:function(_4,_5,_6){if(!_4._hasResource["dojox.form.DropDownStack"]){_4._hasResource["dojox.form.DropDownStack"]=true;_4.provide("dojox.form.DropDownStack");_4.require("dojox.form.DropDownSelect");_4.require("dojox.form._SelectStackMixin");_4.declare("dojox.form.DropDownStack",[_6.form.DropDownSelect,_6.form._SelectStackMixin],{});}}};});