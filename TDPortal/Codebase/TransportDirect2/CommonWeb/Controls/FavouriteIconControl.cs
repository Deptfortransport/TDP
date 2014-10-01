// *********************************************** 
// NAME             : FavouriteIconControl.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 10 Feb 2011
// DESCRIPTION  	: Favourite icon control
// ************************************************
// 

using System.Web.UI;
using System.Web.UI.WebControls;

namespace TDP.Common.Web
{
    /// <summary>
    /// Favourite icon control
    /// </summary>
    public class FavouriteIconControl : WebControl
    {
        #region Private members

        private string iconName;
        private string iconPath;
        private string iconRel = "icon";
        private string iconSizes = string.Empty;
        private string iconType = @"image/png";

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="iconPath"></param>
        /// <param name="iconName"></param>
        public FavouriteIconControl(string iconPath, string iconName) 
            : base(HtmlTextWriterTag.Link)
        {
            this.iconPath = iconPath;
            this.iconName = iconName;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="iconPath"></param>
        /// <param name="iconName"></param>
        public FavouriteIconControl(string iconPath, string iconName, string iconRel, string iconType, string iconSizes)
            : base(HtmlTextWriterTag.Link)
        {
            this.iconPath = iconPath;
            this.iconName = iconName;
            this.iconRel = iconRel;
            this.iconType = iconType;
            this.iconSizes = iconSizes;
        }

        #endregion

        #region Protected methods

        /// <summary>
        /// Overridden method that renders the appropriate fav icon attribute to the page
        /// </summary>
        /// <param name="writer"></param>
        protected override void AddAttributesToRender(HtmlTextWriter writer)
        {
            // Write the attribute that adds the fav icon
            if (!string.IsNullOrEmpty(iconPath))
            {
                writer.AddAttribute(HtmlTextWriterAttribute.Href, ResolveClientUrl(iconPath) + iconName);
                
                if (!string.IsNullOrEmpty(iconRel))
                    writer.AddAttribute(HtmlTextWriterAttribute.Rel, iconRel);
                if (!string.IsNullOrEmpty(iconType))
                    writer.AddAttribute(HtmlTextWriterAttribute.Type, iconType);
                
                if (!string.IsNullOrEmpty(iconSizes))
                    writer.AddAttribute("sizes", iconSizes);
            }

            base.AddAttributesToRender(writer);
        }

        #endregion
    }
}