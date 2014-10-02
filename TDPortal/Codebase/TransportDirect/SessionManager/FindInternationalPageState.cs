using System;
using System.Collections.Generic;
using System.Text;

namespace TransportDirect.UserPortal.SessionManager
{
    /// <summary>
    /// PageState for FindInternational page. Inherit from FindPageState
    /// </summary>
    [CLSCompliant(false)]
    [Serializable]
    public class FindInternationalPageState: FindPageState
	{

        public FindInternationalPageState()
		{
			findMode = FindAMode.International;			
		}
    }
}
