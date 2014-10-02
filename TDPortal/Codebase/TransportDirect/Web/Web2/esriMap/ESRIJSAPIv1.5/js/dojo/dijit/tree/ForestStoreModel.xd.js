/*
	Copyright (c) 2004-2009, The Dojo Foundation All Rights Reserved.
	Available via Academic Free License >= 2.1 OR the modified BSD license.
	see: http://dojotoolkit.org/license for details
*/


dojo._xdResourceLoaded(function(_1,_2,_3){return {depends:[["provide","dijit.tree.ForestStoreModel"],["require","dijit.tree.TreeStoreModel"]],defineResource:function(_4,_5,_6){if(!_4._hasResource["dijit.tree.ForestStoreModel"]){_4._hasResource["dijit.tree.ForestStoreModel"]=true;_4.provide("dijit.tree.ForestStoreModel");_4.require("dijit.tree.TreeStoreModel");_4.declare("dijit.tree.ForestStoreModel",_5.tree.TreeStoreModel,{rootId:"$root$",rootLabel:"ROOT",query:null,constructor:function(_7){this.root={store:this,root:true,id:_7.rootId,label:_7.rootLabel,children:_7.rootChildren};},mayHaveChildren:function(_8){return _8===this.root||this.inherited(arguments);},getChildren:function(_9,_a,_b){if(_9===this.root){if(this.root.children){_a(this.root.children);}else{this.store.fetch({query:this.query,onComplete:_4.hitch(this,function(_c){this.root.children=_c;_a(_c);}),onError:_b});}}else{this.inherited(arguments);}},getIdentity:function(_d){return (_d===this.root)?this.root.id:this.inherited(arguments);},getLabel:function(_e){return (_e===this.root)?this.root.label:this.inherited(arguments);},newItem:function(_f,_10){if(_10===this.root){this.onNewRootItem(_f);return this.store.newItem(_f);}else{return this.inherited(arguments);}},onNewRootItem:function(_11){},pasteItem:function(_12,_13,_14,_15,_16){if(_13===this.root){if(!_15){this.onLeaveRoot(_12);}}_5.tree.TreeStoreModel.prototype.pasteItem.call(this,_12,_13===this.root?null:_13,_14===this.root?null:_14,_15,_16);if(_14===this.root){this.onAddToRoot(_12);}},onAddToRoot:function(_17){console.log(this,": item ",_17," added to root");},onLeaveRoot:function(_18){console.log(this,": item ",_18," removed from root");},_requeryTop:function(){var _19=this.root.children||[];this.store.fetch({query:this.query,onComplete:_4.hitch(this,function(_1a){this.root.children=_1a;if(_19.length!=_1a.length||_4.some(_19,function(_1b,idx){return _1a[idx]!=_1b;})){this.onChildrenChange(this.root,_1a);}})});},_onNewItem:function(_1d,_1e){this._requeryTop();this.inherited(arguments);},_onDeleteItem:function(_1f){if(_4.indexOf(this.root.children,_1f)!=-1){this._requeryTop();}this.inherited(arguments);}});}}};});