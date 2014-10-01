/*
	Copyright (c) 2004-2009, The Dojo Foundation All Rights Reserved.
	Available via Academic Free License >= 2.1 OR the modified BSD license.
	see: http://dojotoolkit.org/license for details
*/


dojo._xdResourceLoaded(function(_1,_2,_3){return {depends:[["provide","dojox.validate.web"],["require","dojox.validate._base"]],defineResource:function(_4,_5,_6){if(!_4._hasResource["dojox.validate.web"]){_4._hasResource["dojox.validate.web"]=true;_4.provide("dojox.validate.web");_4.require("dojox.validate._base");_6.validate.isIpAddress=function(_7,_8){var re=new RegExp("^"+_6.validate.regexp.ipAddress(_8)+"$","i");return re.test(_7);};_6.validate.isUrl=function(_a,_b){var re=new RegExp("^"+_6.validate.regexp.url(_b)+"$","i");return re.test(_a);};_6.validate.isEmailAddress=function(_d,_e){var re=new RegExp("^"+_6.validate.regexp.emailAddress(_e)+"$","i");return re.test(_d);};_6.validate.isEmailAddressList=function(_10,_11){var re=new RegExp("^"+_6.validate.regexp.emailAddressList(_11)+"$","i");return re.test(_10);};_6.validate.getEmailAddressList=function(_13,_14){if(!_14){_14={};}if(!_14.listSeparator){_14.listSeparator="\\s;,";}if(_6.validate.isEmailAddressList(_13,_14)){return _13.split(new RegExp("\\s*["+_14.listSeparator+"]\\s*"));}return [];};}}};});