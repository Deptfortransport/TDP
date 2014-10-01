/*
	Copyright (c) 2004-2009, The Dojo Foundation All Rights Reserved.
	Available via Academic Free License >= 2.1 OR the modified BSD license.
	see: http://dojotoolkit.org/license for details
*/


dojo._xdResourceLoaded(function(_1,_2,_3){return {depends:[["provide","dojox.storage.GearsStorageProvider"],["require","dojo.gears"],["require","dojox.storage.Provider"],["require","dojox.storage.manager"],["require","dojox.sql"]],defineResource:function(_4,_5,_6){if(!_4._hasResource["dojox.storage.GearsStorageProvider"]){_4._hasResource["dojox.storage.GearsStorageProvider"]=true;_4.provide("dojox.storage.GearsStorageProvider");_4.require("dojo.gears");_4.require("dojox.storage.Provider");_4.require("dojox.storage.manager");_4.require("dojox.sql");if(_4.gears.available){(function(){_4.declare("dojox.storage.GearsStorageProvider",_6.storage.Provider,{constructor:function(){},TABLE_NAME:"__DOJO_STORAGE",initialized:false,_available:null,_storageReady:false,initialize:function(){if(_4.config["disableGearsStorage"]==true){return;}this.TABLE_NAME="__DOJO_STORAGE";this.initialized=true;_6.storage.manager.loaded();},isAvailable:function(){return this._available=_4.gears.available;},put:function(_7,_8,_9,_a){this._initStorage();if(!this.isValidKey(_7)){throw new Error("Invalid key given: "+_7);}_a=_a||this.DEFAULT_NAMESPACE;if(!this.isValidKey(_a)){throw new Error("Invalid namespace given: "+_7);}if(_4.isString(_8)){_8="string:"+_8;}else{_8=_4.toJson(_8);}try{_6.sql("DELETE FROM "+this.TABLE_NAME+" WHERE namespace = ? AND key = ?",_a,_7);_6.sql("INSERT INTO "+this.TABLE_NAME+" VALUES (?, ?, ?)",_a,_7,_8);}catch(e){console.debug("dojox.storage.GearsStorageProvider.put:",e);_9(this.FAILED,_7,e.toString(),_a);return;}if(_9){_9(_6.storage.SUCCESS,_7,null,_a);}},get:function(_b,_c){this._initStorage();if(!this.isValidKey(_b)){throw new Error("Invalid key given: "+_b);}_c=_c||this.DEFAULT_NAMESPACE;if(!this.isValidKey(_c)){throw new Error("Invalid namespace given: "+_b);}var _d=_6.sql("SELECT * FROM "+this.TABLE_NAME+" WHERE namespace = ? AND "+" key = ?",_c,_b);if(!_d.length){return null;}else{_d=_d[0].value;}if(_4.isString(_d)&&(/^string:/.test(_d))){_d=_d.substring("string:".length);}else{_d=_4.fromJson(_d);}return _d;},getNamespaces:function(){this._initStorage();var _e=[_6.storage.DEFAULT_NAMESPACE];var rs=_6.sql("SELECT namespace FROM "+this.TABLE_NAME+" DESC GROUP BY namespace");for(var i=0;i<rs.length;i++){if(rs[i].namespace!=_6.storage.DEFAULT_NAMESPACE){_e.push(rs[i].namespace);}}return _e;},getKeys:function(_11){this._initStorage();_11=_11||this.DEFAULT_NAMESPACE;if(!this.isValidKey(_11)){throw new Error("Invalid namespace given: "+_11);}var rs=_6.sql("SELECT key FROM "+this.TABLE_NAME+" WHERE namespace = ?",_11);var _13=[];for(var i=0;i<rs.length;i++){_13.push(rs[i].key);}return _13;},clear:function(_15){this._initStorage();_15=_15||this.DEFAULT_NAMESPACE;if(!this.isValidKey(_15)){throw new Error("Invalid namespace given: "+_15);}_6.sql("DELETE FROM "+this.TABLE_NAME+" WHERE namespace = ?",_15);},remove:function(key,_17){this._initStorage();if(!this.isValidKey(key)){throw new Error("Invalid key given: "+key);}_17=_17||this.DEFAULT_NAMESPACE;if(!this.isValidKey(_17)){throw new Error("Invalid namespace given: "+key);}_6.sql("DELETE FROM "+this.TABLE_NAME+" WHERE namespace = ? AND"+" key = ?",_17,key);},putMultiple:function(_18,_19,_1a,_1b){this._initStorage();if(!this.isValidKeyArray(_18)||!_19 instanceof Array||_18.length!=_19.length){throw new Error("Invalid arguments: keys = ["+_18+"], values = ["+_19+"]");}if(_1b==null||typeof _1b=="undefined"){_1b=_6.storage.DEFAULT_NAMESPACE;}if(!this.isValidKey(_1b)){throw new Error("Invalid namespace given: "+_1b);}this._statusHandler=_1a;try{_6.sql.open();_6.sql.db.execute("BEGIN TRANSACTION");var _1c="REPLACE INTO "+this.TABLE_NAME+" VALUES (?, ?, ?)";for(var i=0;i<_18.length;i++){var _1e=_19[i];if(_4.isString(_1e)){_1e="string:"+_1e;}else{_1e=_4.toJson(_1e);}_6.sql.db.execute(_1c,[_1b,_18[i],_1e]);}_6.sql.db.execute("COMMIT TRANSACTION");_6.sql.close();}catch(e){console.debug("dojox.storage.GearsStorageProvider.putMultiple:",e);if(_1a){_1a(this.FAILED,_18,e.toString(),_1b);}return;}if(_1a){_1a(_6.storage.SUCCESS,_18,null,_1b);}},getMultiple:function(_1f,_20){this._initStorage();if(!this.isValidKeyArray(_1f)){throw new ("Invalid key array given: "+_1f);}if(_20==null||typeof _20=="undefined"){_20=_6.storage.DEFAULT_NAMESPACE;}if(!this.isValidKey(_20)){throw new Error("Invalid namespace given: "+_20);}var _21="SELECT * FROM "+this.TABLE_NAME+" WHERE namespace = ? AND "+" key = ?";var _22=[];for(var i=0;i<_1f.length;i++){var _24=_6.sql(_21,_20,_1f[i]);if(!_24.length){_22[i]=null;}else{_24=_24[0].value;if(_4.isString(_24)&&(/^string:/.test(_24))){_22[i]=_24.substring("string:".length);}else{_22[i]=_4.fromJson(_24);}}}return _22;},removeMultiple:function(_25,_26){this._initStorage();if(!this.isValidKeyArray(_25)){throw new Error("Invalid arguments: keys = ["+_25+"]");}if(_26==null||typeof _26=="undefined"){_26=_6.storage.DEFAULT_NAMESPACE;}if(!this.isValidKey(_26)){throw new Error("Invalid namespace given: "+_26);}_6.sql.open();_6.sql.db.execute("BEGIN TRANSACTION");var _27="DELETE FROM "+this.TABLE_NAME+" WHERE namespace = ? AND key = ?";for(var i=0;i<_25.length;i++){_6.sql.db.execute(_27,[_26,_25[i]]);}_6.sql.db.execute("COMMIT TRANSACTION");_6.sql.close();},isPermanent:function(){return true;},getMaximumSize:function(){return this.SIZE_NO_LIMIT;},hasSettingsUI:function(){return false;},showSettingsUI:function(){throw new Error(this.declaredClass+" does not support a storage settings user-interface");},hideSettingsUI:function(){throw new Error(this.declaredClass+" does not support a storage settings user-interface");},_initStorage:function(){if(this._storageReady){return;}if(!google.gears.factory.hasPermission){var _29=null;var _2a=null;var msg="This site would like to use Google Gears to enable "+"enhanced functionality.";var _2c=google.gears.factory.getPermission(_29,_2a,msg);if(!_2c){throw new Error("You must give permission to use Gears in order to "+"store data");}}try{_6.sql("CREATE TABLE IF NOT EXISTS "+this.TABLE_NAME+"( "+" namespace TEXT, "+" key TEXT, "+" value TEXT "+")");_6.sql("CREATE UNIQUE INDEX IF NOT EXISTS namespace_key_index"+" ON "+this.TABLE_NAME+" (namespace, key)");}catch(e){console.debug("dojox.storage.GearsStorageProvider._createTables:",e);throw new Error("Unable to create storage tables for Gears in "+"Dojo Storage");}this._storageReady=true;}});_6.storage.manager.register("dojox.storage.GearsStorageProvider",new _6.storage.GearsStorageProvider());})();}}}};});