<%@ WebHandler Language="C#" Class="jsapi" %>

using System;
using System.Web;
using System.IO;
using System.Configuration;

public class jsapi : IHttpHandler {
  public void ProcessRequest (HttpContext context) {
  
    // GZIP if supported
    string AcceptEncoding = HttpContext.Current.Request.Headers["Accept-Encoding"];
    if (!string.IsNullOrEmpty(AcceptEncoding) && AcceptEncoding.Contains("gzip")) {
      context.Response.AppendHeader("Content-Encoding", "gzip");
      context.Response.Filter = new System.IO.Compression.GZipStream(context.Response.Filter, System.IO.Compression.CompressionMode.Compress);
    }

    context.Response.ContentType = "application/x-javascript";
    context.Response.Expires = 1800;

    //  START: added by esriuk
    context.Response.WriteFile(context.Server.MapPath("..\\dojoConfig.js"));
    string language = context.Request.Params.Get("language");
    if (language != null && language == "cy")
    {
      context.Response.Write("djConfig.locale='cy';");
    }
    else
    {
      context.Response.Write("djConfig.locale='en';");
    }
    //  START: Fix for nested directories bug
    string execPath = context.Request.CurrentExecutionFilePath.ToLower();
    context.Response.Write("djConfig.appPath = '" + execPath.Replace("/esrimap/esrijsapiv1.5/default.ashx", "") + "';");
    context.Response.Write("djConfig.esriJSAPI = '" + execPath.Replace("default.ashx", "") + "';");
    context.Response.Write("djConfig.modulePaths.ESRIUK = '" + execPath.Replace("esrijsapiv1.5/default.ashx", "ESRIUK") + "';");
    //  START: Fix for nested directories bug  
    context.Response.WriteFile(context.Server.MapPath("..\\json.js"));
    //  END: added by esriuk    
    
    context.Response.Write("if (typeof dojo == \"undefined\") {");
    context.Response.WriteFile(context.Server.MapPath("js\\dojo\\dojo\\dojo.xd.js"));
    context.Response.Write("}");
    context.Response.WriteFile(context.Server.MapPath("js\\esri\\esri.js"));
    context.Response.WriteFile(context.Server.MapPath("js\\esri\\jsapi.js"));

    //  START: added by esriuk   
    bool isDebug = false;
    Boolean.TryParse(ConfigurationManager.AppSettings.Get("ESRIUK.debug"), out isDebug);
    if (isDebug)
    {
      context.Response.Write("dojo.require('ESRIUK.Dijits.Map');");
    }
    else
    {
      //  add un-obfuscated config file
      context.Response.WriteFile(context.Server.MapPath("..\\ESRIUK\\Config.js"));
      //  define app as live
      context.Response.Write("ESRIUK.config({live:true});");
      //  add esriuk libraries
      context.Response.WriteFile(context.Server.MapPath("..\\release\\esriuk.js"));
      //  add minified Query Web Service
      //context.Response.WriteFile(context.Server.MapPath("..\\TDPQWS.js"));
      //  add minified Map Web Service
      //context.Response.WriteFile(context.Server.MapPath("..\\TDPMWS.js"));      
    }
    //  END: added by esriuk       
  }
 
  public bool IsReusable {
    get {
      return false;
    }
  }
}
