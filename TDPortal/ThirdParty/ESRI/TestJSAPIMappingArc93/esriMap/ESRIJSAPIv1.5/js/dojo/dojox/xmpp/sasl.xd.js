/*
	Copyright (c) 2004-2009, The Dojo Foundation All Rights Reserved.
	Available via Academic Free License >= 2.1 OR the modified BSD license.
	see: http://dojotoolkit.org/license for details
*/


dojo._xdResourceLoaded(function(_1,_2,_3){return {depends:[["provide","dojox.xmpp.sasl"],["require","dojox.xmpp.util"]],defineResource:function(_4,_5,_6){if(!_4._hasResource["dojox.xmpp.sasl"]){_4._hasResource["dojox.xmpp.sasl"]=true;_4.provide("dojox.xmpp.sasl");_4.require("dojox.xmpp.util");_6.xmpp.sasl.saslNS="urn:ietf:params:xml:ns:xmpp-sasl";_6.xmpp.sasl.SunWebClientAuth=function(_7){var _8={xmlns:_6.xmpp.sasl.saslNS,mechanism:"SUN-COMMS-CLIENT-PROXY-AUTH"};var _9=_6.xmpp.util.createElement("auth",_8,true);_7.dispatchPacket(_9);};_6.xmpp.sasl.SaslPlain=function(_a){var _b={xmlns:_6.xmpp.sasl.saslNS,mechanism:"PLAIN"};var _c=new _6.string.Builder(_6.xmpp.util.createElement("auth",_b,false));var id=_a.jid;var _e=_a.jid.indexOf("@");if(_e!=-1){id=_a.jid.substring(0,_e);}var _f="\x00"+id+"\x00"+_a.password;_f=_6.xmpp.util.Base64.encode(_f);_c.append(_f);_c.append("</auth>");_a.dispatchPacket(_c.toString());};}}};});