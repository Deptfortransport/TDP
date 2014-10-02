// *********************************************** 
// NAME                 : TDImageButtonAdapter.cs 
// AUTHOR               : David Lane
// DATE CREATED         : 28/08/2011
// DESCRIPTION			: Adapter for TDImageButton that
// removes the spurious border=0px that .Net puts in (.Net bug)
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/TDImageButtonAdapter.cs-arc  $
//
//   Rev 1.0   Jul 28 2011 16:30:22   dlane
//Initial revision.

using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.Adapters;

namespace TransportDirect.UserPortal.Web.Controls
{
    public class TDImageButtonAdapter : WebControlAdapter
    {
        public TDImageButtonAdapter()
        {
        }

        protected override void  Render(HtmlTextWriter writer)
        {
            Image img = this.Control as Image;

            if (img.BorderWidth.IsEmpty)
            {
                writer = new ImageAdapterHtmlTextWriter(writer);
            }

            base.RenderBeginTag(writer);
        }
    }

    public sealed class ImageAdapterHtmlTextWriter : HtmlTextWriter
    {
        public ImageAdapterHtmlTextWriter(TextWriter writer)
            : base(writer)
        {
        }

        public override void AddStyleAttribute(HtmlTextWriterStyle key, string value)
        {
            if (HtmlTextWriterStyle.BorderWidth != key || "0px" != value)
            {
                base.AddStyleAttribute(key, value);
            }
        }
    }
}
