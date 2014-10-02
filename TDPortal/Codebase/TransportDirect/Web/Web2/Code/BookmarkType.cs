//// ***********************************************
//// NAME           : BookmarkType.cs
//// AUTHOR 		: Steve Barker
//// DATE CREATED   : 21-Feb-2008
//// DESCRIPTION 	: Required to distinguish the type of bookmark on each page
//// ************************************************
//// Rev Devfactory Feb 21 2008 14:00:00   sbarker
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

namespace TransportDirect.UserPortal.Web.Code
{
    public enum BookmarkType
    {
        Page,
        Journey,
    }
}
