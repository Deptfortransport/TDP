/*
	Copyright (c) 2004-2009, The Dojo Foundation All Rights Reserved.
	Available via Academic Free License >= 2.1 OR the modified BSD license.
	see: http://dojotoolkit.org/license for details
*/


dojo._xdResourceLoaded(function(_1,_2,_3){return {depends:[["provide","dojox.xmpp.ChatService"]],defineResource:function(_4,_5,_6){if(!_4._hasResource["dojox.xmpp.ChatService"]){_4._hasResource["dojox.xmpp.ChatService"]=true;_4.provide("dojox.xmpp.ChatService");_6.xmpp.chat={CHAT_STATE_NS:"http://jabber.org/protocol/chatstates",ACTIVE_STATE:"active",COMPOSING_STATE:"composing",INACTIVE_STATE:"inactive",PAUSED_STATE:"paused",GONE_STATE:"gone"};_4.declare("dojox.xmpp.ChatService",null,{state:"",constructor:function(){this.state="";this.chatid=Math.round(Math.random()*1000000000000000);},recieveMessage:function(_7,_8){if(_7&&!_8){this.onNewMessage(_7);}},setSession:function(_9){this.session=_9;},setState:function(_a){if(this.state!=_a){this.state=_a;}},invite:function(_b){if(this.uid){return;}if(!_b||_b==""){throw new Error("ChatService::invite() contact is NULL");}this.uid=_b;var _c={xmlns:"jabber:client",to:this.uid,from:this.session.jid+"/"+this.session.resource,type:"chat"};var _d=new _6.string.Builder(_6.xmpp.util.createElement("message",_c,false));_d.append(_6.xmpp.util.createElement("thread",{},false));_d.append(this.chatid);_d.append("</thread>");_d.append(_6.xmpp.util.createElement("active",{xmlns:_6.xmpp.chat.CHAT_STATE_NS},true));_d.append("</message>");this.session.dispatchPacket(_d.toString());this.onInvite(_b);this.setState(_6.xmpp.chat.CHAT_STATE_NS);},sendMessage:function(_e){if(!this.uid){return;}if((!_e.body||_e.body=="")&&!_e.xhtml){return;}var _f={xmlns:"jabber:client",to:this.uid,from:this.session.jid+"/"+this.session.resource,type:"chat"};var _10=new _6.string.Builder(_6.xmpp.util.createElement("message",_f,false));var _11=_6.xmpp.util.createElement("html",{"xmlns":_6.xmpp.xmpp.XHTML_IM_NS},false);var _12=_6.xmpp.util.createElement("body",{"xml:lang":this.session.lang,"xmlns":_6.xmpp.xmpp.XHTML_BODY_NS},false)+_e.body+"</body>";var _13=_6.xmpp.util.createElement("body",{},false)+_6.xmpp.util.stripHtml(_e.body)+"</body>";if(_10.subject&&_10.subject!=""){_10.append(_6.xmpp.util.createElement("subject",{},false));_10.append(_10.subject);_10.append("</subject>");}_10.append(_13);_10.append(_11);_10.append(_12);_10.append("</html>");_10.append(_6.xmpp.util.createElement("thread",{},false));_10.append(this.chatid);_10.append("</thread>");if(this.useChatStates){_10.append(_6.xmpp.util.createElement("active",{xmlns:_6.xmpp.chat.CHAT_STATE_NS},true));}_10.append("</message>");this.session.dispatchPacket(_10.toString());},sendChatState:function(_14){if(!this.useChatState||this.firstMessage){return;}if(_14==this._currentState){return;}var req={xmlns:"jabber:client",to:this.uid,from:this.session.jid+"/"+this.session.resource,type:"chat"};var _16=new _6.string.Builder(_6.xmpp.util.createElement("message",req,false));_16.append(_6.xmpp.util.createElement(_14,{xmlns:_6.xmpp.chat.CHAT_STATE_NS},true));this._currentState=_14;_16.append("<thread>");_16.append(this.chatid);_16.append("</thread></message>");this.session.dispatchPacket(_16.toString());},onNewMessage:function(msg){},onInvite:function(_18){}});}}};});