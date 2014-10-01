using System;
using System.Collections.Generic;
using System.Text;

namespace CmsMockObject.Objects
{
    public class CmsAspContext : CmsContext
    {

        public Channel Channel 
        {
            get { return new Channel(); }
        }

        public ChannelItem ChannelItem 
        {
            get { return new ChannelItem(); } 
        }

        public string CmsQueryString 
        {
            get { return ""; } 
        }

        public bool IsUsingTemplate 
        {
            get { return false; }
        }

        public Posting Posting 
        {
            get { return new Posting(); } 
        }

        public bool ChannelItemIsVisible()
        {
            return false;
        }

        public bool ChannelItemIsVisible(string urlOrGuid)
        {
            return false;
        }


        public string ResolveUrl(string urlToResolve)
        {
            return "";
        }
    }
}
