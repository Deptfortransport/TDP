/*
	Copyright (c) 2004-2009, The Dojo Foundation All Rights Reserved.
	Available via Academic Free License >= 2.1 OR the modified BSD license.
	see: http://dojotoolkit.org/license for details
*/


dojo._xdResourceLoaded(function(_1,_2,_3){return {depends:[["provide","dojox.validate.us"],["require","dojox.validate._base"]],defineResource:function(_4,_5,_6){if(!_4._hasResource["dojox.validate.us"]){_4._hasResource["dojox.validate.us"]=true;_4.provide("dojox.validate.us");_4.require("dojox.validate._base");_6.validate.us.isState=function(_7,_8){var re=new RegExp("^"+_6.validate.regexp.us.state(_8)+"$","i");return re.test(_7);};_6.validate.us.isPhoneNumber=function(_a){var _b={format:["###-###-####","(###) ###-####","(###) ### ####","###.###.####","###/###-####","### ### ####","###-###-#### x#???","(###) ###-#### x#???","(###) ### #### x#???","###.###.#### x#???","###/###-#### x#???","### ### #### x#???","##########"]};return _6.validate.isNumberFormat(_a,_b);};_6.validate.us.isSocialSecurityNumber=function(_c){var _d={format:["###-##-####","### ## ####","#########"]};return _6.validate.isNumberFormat(_c,_d);};_6.validate.us.isZipCode=function(_e){var _f={format:["#####-####","##### ####","#########","#####"]};return _6.validate.isNumberFormat(_e,_f);};}}};});