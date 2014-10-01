// *********************************************** 
// NAME             : IPageTransferCache.cs      
// AUTHOR           : Mitesh Modi
// DATE CREATED     : 18 Mar 2011
// DESCRIPTION  	: IPageTransferCache interface
// ************************************************
// 

using TDP.Common;

namespace TDP.UserPortal.ScreenFlow
{
    /// <summary>
    /// Interface for PageTransferCache
    /// </summary>
    public interface IPageTransferCache
    {
        /// <summary>
        /// Returns the PageTransferDetail object for the given pageId.
        /// </summary>
        PageTransferDetail GetPageTransferDetails(PageId pageId);
    }
}
