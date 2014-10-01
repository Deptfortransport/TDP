namespace AO.SiteStatusLoaderService
{
    partial class SiteStatusLoaderService
    {
        #region Dispose
        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// C# Destructor.
        /// </summary>
        ~SiteStatusLoaderService()
        {
            Dispose(false);
        }
        #endregion

    }
}
