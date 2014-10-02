/**
*   @author: Sam Larsen, ESRI (UK), slarsen@esriuk.com
*   @date: July 2009
*   @description:
*     Main Dojo config settings
*     Must be changed before going live!!
*/
var djConfig = {
  isDebug:false, //  must be changed before going live !!
  xdWaitSeconds:10,  //  can play with this (timeout for receiving js files) - if you are feeling adventurous  
  popup:false,  //  do not change this
  parseOnLoad:true,  //  do not change this
  useXDomain:false,  //  do not change this
  baseUrl:'./',  //  do not change this,
  modulePaths:{'ESRIUK':'./esriMap/ESRIUK'},  //  do not change this
  /**
  *   @esrijsapi: Generates url of app to enable copy & paste of the api
  *   @param: j
  *     in the following script from the function below:
  *       var j='/script/ESRIJSAPIv1.4/'
  *     replace '/script/ESRIJSAPIv1.4/' with anything relative to your root web application directory
  */
  esriJSAPI:(function(){
      var j='/esriMap/ESRIJSAPIv1.4/';
      var w=window.location;
      var m=w.protocol+"//"+w.host;
      var a=w.pathname.split("/");
      for(var i=1;i<a.length-1;i++){
        m+='/'+a[i];
      }
      return m+j;
    })()
};
