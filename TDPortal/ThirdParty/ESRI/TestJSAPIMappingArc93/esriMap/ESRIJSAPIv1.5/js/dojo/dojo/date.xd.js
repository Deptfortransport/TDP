/*
	Copyright (c) 2004-2009, The Dojo Foundation All Rights Reserved.
	Available via Academic Free License >= 2.1 OR the modified BSD license.
	see: http://dojotoolkit.org/license for details
*/


dojo._xdResourceLoaded(function(_1,_2,_3){return {depends:[["provide","dojo.date"]],defineResource:function(_4,_5,_6){if(!_4._hasResource["dojo.date"]){_4._hasResource["dojo.date"]=true;_4.provide("dojo.date");_4.date.getDaysInMonth=function(_7){var _8=_7.getMonth();var _9=[31,28,31,30,31,30,31,31,30,31,30,31];if(_8==1&&_4.date.isLeapYear(_7)){return 29;}return _9[_8];};_4.date.isLeapYear=function(_a){var _b=_a.getFullYear();return !(_b%400)||(!(_b%4)&&!!(_b%100));};_4.date.getTimezoneName=function(_c){var _d=_c.toString();var tz="";var _f;var pos=_d.indexOf("(");if(pos>-1){tz=_d.substring(++pos,_d.indexOf(")"));}else{var pat=/([A-Z\/]+) \d{4}$/;if((_f=_d.match(pat))){tz=_f[1];}else{_d=_c.toLocaleString();pat=/ ([A-Z\/]+)$/;if((_f=_d.match(pat))){tz=_f[1];}}}return (tz=="AM"||tz=="PM")?"":tz;};_4.date.compare=function(_12,_13,_14){_12=new Date(Number(_12));_13=new Date(Number(_13||new Date()));if(_14!=="undefined"){if(_14=="date"){_12.setHours(0,0,0,0);_13.setHours(0,0,0,0);}else{if(_14=="time"){_12.setFullYear(0,0,0);_13.setFullYear(0,0,0);}}}if(_12>_13){return 1;}if(_12<_13){return -1;}return 0;};_4.date.add=function(_15,_16,_17){var sum=new Date(Number(_15));var _19=false;var _1a="Date";switch(_16){case "day":break;case "weekday":var _1b,_1c;var mod=_17%5;if(!mod){_1b=(_17>0)?5:-5;_1c=(_17>0)?((_17-5)/5):((_17+5)/5);}else{_1b=mod;_1c=parseInt(_17/5);}var _1e=_15.getDay();var adj=0;if(_1e==6&&_17>0){adj=1;}else{if(_1e==0&&_17<0){adj=-1;}}var _20=_1e+_1b;if(_20==0||_20==6){adj=(_17>0)?2:-2;}_17=(7*_1c)+_1b+adj;break;case "year":_1a="FullYear";_19=true;break;case "week":_17*=7;break;case "quarter":_17*=3;case "month":_19=true;_1a="Month";break;case "hour":case "minute":case "second":case "millisecond":_1a="UTC"+_16.charAt(0).toUpperCase()+_16.substring(1)+"s";}if(_1a){sum["set"+_1a](sum["get"+_1a]()+_17);}if(_19&&(sum.getDate()<_15.getDate())){sum.setDate(0);}return sum;};_4.date.difference=function(_21,_22,_23){_22=_22||new Date();_23=_23||"day";var _24=_22.getFullYear()-_21.getFullYear();var _25=1;switch(_23){case "quarter":var m1=_21.getMonth();var m2=_22.getMonth();var q1=Math.floor(m1/3)+1;var q2=Math.floor(m2/3)+1;q2+=(_24*4);_25=q2-q1;break;case "weekday":var _2a=Math.round(_4.date.difference(_21,_22,"day"));var _2b=parseInt(_4.date.difference(_21,_22,"week"));var mod=_2a%7;if(mod==0){_2a=_2b*5;}else{var adj=0;var _2e=_21.getDay();var _2f=_22.getDay();_2b=parseInt(_2a/7);mod=_2a%7;var _30=new Date(_21);_30.setDate(_30.getDate()+(_2b*7));var _31=_30.getDay();if(_2a>0){switch(true){case _2e==6:adj=-1;break;case _2e==0:adj=0;break;case _2f==6:adj=-1;break;case _2f==0:adj=-2;break;case (_31+mod)>5:adj=-2;}}else{if(_2a<0){switch(true){case _2e==6:adj=0;break;case _2e==0:adj=1;break;case _2f==6:adj=2;break;case _2f==0:adj=1;break;case (_31+mod)<0:adj=2;}}}_2a+=adj;_2a-=(_2b*2);}_25=_2a;break;case "year":_25=_24;break;case "month":_25=(_22.getMonth()-_21.getMonth())+(_24*12);break;case "week":_25=parseInt(_4.date.difference(_21,_22,"day")/7);break;case "day":_25/=24;case "hour":_25/=60;case "minute":_25/=60;case "second":_25/=1000;case "millisecond":_25*=_22.getTime()-_21.getTime();}return Math.round(_25);};}}};});