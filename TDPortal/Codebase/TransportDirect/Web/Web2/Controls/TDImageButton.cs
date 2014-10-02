//// ***********************************************
//// NAME           : TDImageButton.cs
//// AUTHOR 		: Steve Barker
//// DATE CREATED   : 19-Feb-2008
//// DESCRIPTION 	: Required to allow white labelling of Image Buttons
//// ************************************************
//// Rev Devfactory Feb 19 2008 14:00:00   sbarker
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
    /// Summary description for TDImageButton.
    /// </summary>
    [System.Runtime.InteropServices.ComVisible(false)]
    [ToolboxData("<{0}:TDImageButton runat=server></{0}:TDImageButton>")]
    public class TDImageButton : ImageButton
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
