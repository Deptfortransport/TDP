using System;
using System.Collections.Generic;
using System.Text;

namespace CmsMockObject.Objects
{
    public class CmsHttpContext : CmsAspContext
    {
        public static CmsHttpContext Current 
        {
            get
            {
                CmsHttpContext _context = null;
                if (_context == null)
                    _context = new CmsHttpContext();

                return _context;
            }
        }
        public string UserCacheKey 
        {
            get
            {
                return "";
            }
        }

        new public void Dispose()
        {

        }
    }
}
