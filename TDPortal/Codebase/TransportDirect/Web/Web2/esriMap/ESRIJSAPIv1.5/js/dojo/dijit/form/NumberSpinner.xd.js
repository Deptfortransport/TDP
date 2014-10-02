/*
	Copyright (c) 2004-2009, The Dojo Foundation All Rights Reserved.
	Available via Academic Free License >= 2.1 OR the modified BSD license.
	see: http://dojotoolkit.org/license for details
*/


dojo._xdResourceLoaded(function(_1,_2,_3){return {depends:[["provide","dijit.form.NumberSpinner"],["require","dijit.form._Spinner"],["require","dijit.form.NumberTextBox"]],defineResource:function(_4,_5,_6){if(!_4._hasResource["dijit.form.NumberSpinner"]){_4._hasResource["dijit.form.NumberSpinner"]=true;_4.provide("dijit.form.NumberSpinner");_4.require("dijit.form._Spinner");_4.require("dijit.form.NumberTextBox");_4.declare("dijit.form.NumberSpinner",[_5.form._Spinner,_5.form.NumberTextBoxMixin],{required:true,adjust:function(_7,_8){var tc=this.constraints,v=isNaN(_7),_b=!isNaN(tc.max),_c=!isNaN(tc.min);if(v&&_8!=0){_7=(_8>0)?_c?tc.min:_b?tc.max:0:_b?this.constraints.max:_c?tc.min:0;}var _d=_7+_8;if(v||isNaN(_d)){return _7;}if(_b&&(_d>tc.max)){_d=tc.max;}if(_c&&(_d<tc.min)){_d=tc.min;}return _d;},_onKeyPress:function(e){if((e.charOrCode==_4.keys.HOME||e.charOrCode==_4.keys.END)&&!e.ctrlKey&&!e.altKey){var _f=this.constraints[(e.charOrCode==_4.keys.HOME?"min":"max")];if(_f){this._setValueAttr(_f,true);}_4.stopEvent(e);}}});}}};});