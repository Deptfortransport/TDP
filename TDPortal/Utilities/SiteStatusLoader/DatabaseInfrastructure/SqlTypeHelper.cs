// *********************************************** 
// NAME                 : SqlTypeHelper.cs
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 01/04/2009
// DESCRIPTION			: Helper class to ensure a .NET type is compatible with a SQL database type
// ************************************************ 
// $Log:   P:/TDPortal/archives/Utilities/SiteStatusLoader/DatabaseInfrastructure/SqlTypeHelper.cs-arc  $
//
//   Rev 1.0   Apr 01 2009 13:26:06   mmodi
//Initial revision.
//Resolution for 5273: Site Status Loader

using System;
using System.Collections.Generic;
using System.Text;

namespace AO.DatabaseInfrastructure
{
    public class SqlTypeHelper
	{
		public static readonly DateTime MinDateTime = new DateTime(1753, 1, 1,12, 0, 0);
		public static readonly DateTime MaxDateTime = new DateTime(9999,12,31,23,59,59);

		private SqlTypeHelper()
		{
			
		}

        /// <summary>
        /// Returns if the DateTime is valid for a SQL Database
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
		public static bool IsSqlDateTimeCompatible(DateTime dateTime)
		{
            if (dateTime >= MinDateTime && dateTime <= MaxDateTime)
				return true;
			else
				return false;
		}
    }
}
