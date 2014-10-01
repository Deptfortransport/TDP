using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string redirTo = ConfigurationManager.AppSettings["default"];
        if (!String.IsNullOrEmpty(Request.Url.Host))
        {
            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings[Request.Url.Host]))
            {
                redirTo = ConfigurationManager.AppSettings[Request.Url.Host];
            }
        }
        if (redirTo != null)
        {
            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["ResponseStatus"]))
            {
                // Set up the redirect http status
                HttpContext context = this.Context;
                if (context != null)
                {
                    context.Response.Status = ConfigurationManager.AppSettings["ResponseStatus"];
                    context.Response.AddHeader("Location", redirTo);
                }
            }
            else
            {
                Response.Redirect(redirTo, true);
            }
        }
        else
        {
            Literal lit = new Literal();
            lit.Text = "<h1>DEFAULT SETTING MISSING IN WEB.CONFIG FOR REDIR</h1>";
            this.Controls.Add(lit);
        }
    }
}    
