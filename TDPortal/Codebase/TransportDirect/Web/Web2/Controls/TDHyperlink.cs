//// ***********************************************
//// NAME           : TDHyperlink.cs
//// AUTHOR 		: Steve Barker
//// DATE CREATED   : 20-Feb-2008
//// DESCRIPTION 	: Required to allow white labelling of hyperlinks
//// ************************************************
//// Rev Devfactory Feb 20 2008 14:00:00   sbarker
//// Initial release.

using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using TransportDirect.UserPortal.Web.Code;
using TransportDirect.Common.DatabaseInfrastructure.Content;

namespace TransportDirect.UserPortal.Web.Controls
{
    /// <summary>
    /// Summary description for TDHyperlink.
    /// </summary>
    [System.Runtime.InteropServices.ComVisible(false)]
    [ToolboxData("<{0}:TDHyperlink runat=server></{0}:TDHyperlink>")]
    public class TDHyperlink : HyperLink
    {
        #region Public Override Methods

        public override string ImageUrl
        {
            get
            {
                return base.ImageUrl;
            }
            set
            {
                base.ImageUrl = ImageUrlHelper.GetAlteredImageUrl(value);
            }
        }

        #endregion
    }
}
