/*
	Copyright (c) 2004-2009, The Dojo Foundation All Rights Reserved.
	Available via Academic Free License >= 2.1 OR the modified BSD license.
	see: http://dojotoolkit.org/license for details
*/


dojo._xdResourceLoaded(function(_1,_2,_3){return {depends:[["provide","dijit.MenuBar"],["require","dijit.Menu"]],defineResource:function(_4,_5,_6){if(!_4._hasResource["dijit.MenuBar"]){_4._hasResource["dijit.MenuBar"]=true;_4.provide("dijit.MenuBar");_4.require("dijit.Menu");_4.declare("dijit.MenuBar",_5._MenuBase,{templateString:"<div class=\"dijitMenuBar dijitMenuPassive\" dojoAttachPoint=\"containerNode\"  waiRole=\"menubar\" tabIndex=\"${tabIndex}\" dojoAttachEvent=\"onkeypress: _onKeyPress\"></div>\r\n",_isMenuBar:true,constructor:function(){this._orient=this.isLeftToRight()?{BL:"TL"}:{BR:"TR"};},postCreate:function(){var k=_4.keys,l=this.isLeftToRight();this.connectKeyNavHandlers(l?[k.LEFT_ARROW]:[k.RIGHT_ARROW],l?[k.RIGHT_ARROW]:[k.LEFT_ARROW]);},focusChild:function(_9){var _a=this.focusedChild,_b=_a&&_a.popup&&_a.popup.isShowingNow;this.inherited(arguments);if(_b&&!_9.disabled){this._openPopup();}},_onKeyPress:function(_c){if(_c.ctrlKey||_c.altKey){return;}switch(_c.charOrCode){case _4.keys.DOWN_ARROW:this._moveToPopup(_c);_4.stopEvent(_c);}}});}}};});