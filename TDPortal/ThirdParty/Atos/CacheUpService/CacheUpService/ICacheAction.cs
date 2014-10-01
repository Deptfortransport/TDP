#region Amendment history
// *********************************************** 
// NAME			: $Workfile:   ICacheAction.cs  $
// AUTHOR		: Peter Norell
// DATE CREATED	: 01/11/2007
// REVISION		: $Revision:   1.0  $
// DESCRIPTION	: An ICacheAction class
// ************************************************ 
// $Log:   P:\archives\Codebase\WebTIS\CacheUpService\ICacheAction.cs-arc  $ 
//
//   Rev 1.0   Nov 02 2007 16:57:44   p.norell
//Initial Revision
//
#endregion
#region Imports
using System;
using System.Collections.Generic;
using System.Text;
using WT.Common.Logging;
#endregion

namespace WT.CacheUpService
{
    /// <summary>
    /// Class for actions - executed for any case that has items on it
    /// </summary>
    public interface ICacheAction
    {
        /// <summary>
        /// Executes on the given level
        /// </summary>
        /// <param name="level">The level for the current running URL</param>
        /// <param name="settingsId">The settings set id to read (empty will use default set)</param>
        void ExecuteOn(WTTraceLevel level, string settingsId);
    }
}
