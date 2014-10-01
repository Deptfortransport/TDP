using System;
using System.Collections.Generic;
using System.Text;

namespace CmsMockObject.Objects
{
    public class ChannelItem
    {
        public string Name
        {
            get
            {
                return "";
            }
        }

        public CmsUrl Url
        {
            get
            {
                return new CmsUrl();
            }
        }
    }

    public class Posting : ChannelItem
    {
        public string DisplayName
        {
            get
            {
                return "";
            }
        }
    }
}
