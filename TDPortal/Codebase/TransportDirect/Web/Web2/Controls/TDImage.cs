// *********************************************** 
// NAME                 : TDImages.cs 
// AUTHOR               : Marcus Tillett
// DATE CREATED         : 15/12/2005
// DESCRIPTION			: Replacement for the HTML image button 
//						(to be used in place of <img runat="server">
//						(Viewstate has been disabled on this control)
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Web2/Controls/TDImage.cs-arc  $
//
//   Rev 1.3   Jul 28 2011 16:19:28   DLane
//UK11131377 - Changes for WAI compliance
//Resolution for 5712: Changes for accessibility (WAI)
//
//   Rev 1.3   June 16 2011 12:17:12   dlane
//Allowed use of a space as alt text that doesn't generate a title (accessibility)
//
//   Rev 1.2   Mar 31 2008 13:23:12   mturner
//Drop3 from Dev Factory
//
//   Rev 1.0   Nov 08 2007 13:18:08   mturner
//Initial revision.
//
//   devfactory Feb 14 2008 sbarker
//   Changes made to allow theming of images. Class put into regions as well.   
//
//   Rev 1.2   Dec 23 2005 15:23:26   RGriffith
//FxCop suggested changes
//
//   Rev 1.1   Dec 23 2005 14:23:18   RGriffith
//Changes to implement System.Web.UI.WebControls.Image and have correct tag output in rendered HTML
//
//   Rev 1.0   Dec 15 2005 12:42:48   mtillett
//Initial revision.

using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Globalization;
using TransportDirect.Common.DatabaseInfrastructure.Content;
using TransportDirect.UserPortal.Web.Code;

namespace TransportDirect.UserPortal.Web.Controls
{
	/// <summary>
	/// TDImage: Replacement for the WebControls image object
	/// </summary>
	[System.Runtime.InteropServices.ComVisible(false)]
	[ToolboxData("<{0}:TDImage runat=server></{0}:TDImage>")]
	public class TDImage : System.Web.UI.WebControls.Image
	{
        #region Constructor

        /// <summary>
		/// Default constructor
		/// </summary>
		public TDImage()
		{
            //No implementation
        }

        #endregion

        #region Public Override Properties
        
        /// <summary>
		/// Override of the Height property
		/// Note: Mimics implementation of the 
		/// System.Web.UI.HtmlControls.HtmlImage height property  (of type Unit)
		/// </summary>
		public override Unit Height
		{
			get
			{
				string text1 = base.Attributes["height"];
				if (text1 == null)
				{
					return -1;
				}
				return int.Parse(text1, CultureInfo.InvariantCulture);
			}
			set
			{
				if (value == -1)
				{
					base.Attributes["height"] = null;
				}
				else
				{
					base.Attributes["height"] = value.ToString(CultureInfo.InvariantCulture);
				}
			}
		}		

		/// <summary>
		/// Override of the Width property
		/// Note: Mimics implementation of the 
		/// System.Web.UI.HtmlControls.HtmlImage width property (of type Unit)
		/// </summary>
		public override Unit Width
		{
			get
			{
				string text1 = base.Attributes["width"];
				if (text1 == null)
				{
					return -1;
				}
				return int.Parse(text1, CultureInfo.InvariantCulture);
			}
			set
			{
				if (value == -1)
				{
					base.Attributes["width"] = null;
				}
				else
				{
					base.Attributes["width"] = value.ToString(CultureInfo.InvariantCulture);
				}
			}
        }

        /// <summary>
        /// Override of the AlternateText property
        /// Note: This also sets the ToolTip value to be the same value
        /// </summary>
        public override string AlternateText
        {
            get
            {
                return base.AlternateText;
            }
            set
            {
                if (value.Trim().Length == 0)
                {
                    base.ToolTip = "";
                    base.AlternateText = value;
                }
                else
                {
                    base.ToolTip = value;
                    base.AlternateText = value;
                }
            }
        }

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

        #region Protected Override Methods

        /// <summary>
		/// SaveViewState overriden to prevent saving any viewstate
		/// </summary>
		/// <returns>null object</returns>
		protected override object SaveViewState()
		{
			return null;
        }

        #endregion
    }
}
