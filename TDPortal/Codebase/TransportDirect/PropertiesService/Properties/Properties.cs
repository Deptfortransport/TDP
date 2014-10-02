// *********************************************** 
// NAME                 : Properties.dll
// AUTHOR               : Patrick ASSUIED 
// DATE CREATED         : 2/07/2003 
// DESCRIPTION  : Base class of FilePropertyProvider and DatabasePropertyProvider. 
// Component of PropertyService
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PropertiesService/Properties/Properties.cs-arc  $ 
//
//   Rev 1.2   Apr 03 2008 15:25:28   mmodi
//Exposed Theme id
//Resolution for 4631: Del 10 - Transaction injector is not working correctly
//
//   Rev 1.1   Mar 10 2008 15:22:56   mturner
//Initial Del10 Codebase from Dev Factory
//
//   Rev 1.0   Nov 08 2007 12:37:52   mturner
//Initial revision.
//
//   Rev 1.8   Feb 23 2006 19:15:46   build
//Automatically merged from branch for stream3129
//
//   Rev 1.7.2.0   Dec 15 2005 10:12:00   schand
//Getting Partnet White Label changes for stream3129. 
//Resolution for 3129: Del 8.0 Exposed Services Framework Enhancements
//
//   Rev 1.7.1.1   Sep 29 2005 11:25:20   pcross
//Update to indexer handling microsite PartnerID.
//Resolution for 2807: DEL8 White labelling Phase 3
//Resolution for 2810: Del 8 White Labelling Phase 3 - Changes to Properties and Data services Components
//
//   Rev 1.7.1.0   Sep 05 2005 15:34:08   pcross
//Includes an indexer that allows entry of a microsite PartnerID.
//
//   Rev 1.7   Jul 25 2003 10:38:08   passuied
//addition of CLSCompliant
//
//   Rev 1.6   Jul 23 2003 10:23:02   passuied
//Changes after PropertyService namespaces / dll renaming
//
//   Rev 1.5   Jul 17 2003 15:00:34   passuied
//updated

using System;
using System.Timers;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.IO;
using TransportDirect.Common.ServiceDiscovery;
using TD.ThemeInfrastructure;

namespace TransportDirect.Common.PropertyService.Properties
{
	/// <summary>
	/// Base class for PropertyProvider classes implements the interface IPropertyProvider
	/// </summary>
	public abstract class Properties : IPropertyProvider
	{

		// Interface implementation

		public  event SupersededEventHandler Superseded;
		protected bool boolSuperseded= false;
		protected int intVersion=0;
		static protected string strApplicationID = string.Empty;
		static protected string strGroupID = string.Empty;
        static protected string strThemeID = string.Empty;
        protected Dictionary<int, Hashtable> propertyDictionary;
		protected Hashtable propertyTable;
	

		public bool IsSuperseded
		{
			get
			{
				
				return boolSuperseded;
			}
		}


		public int Version
		{
			get{
				return intVersion;
			}
		}

		public string this[string key]
		{
			get 
			{
                ThemeProvider provider = ThemeProvider.Instance;

                Theme theme = null;
                Theme defaultTheme = null;

                try
                {
                    theme = provider.GetTheme();
                    defaultTheme = provider.GetDefaultTheme();
                }
                catch
                {
                    // Couldn't get theme, so need to revert to default
                    theme = provider.GetDefaultTheme();
                    defaultTheme = provider.GetDefaultTheme();
                }

                int themeId = defaultTheme.Id;

                if (propertyDictionary.ContainsKey(theme.Id))
                {
                    themeId = theme.Id;
                }

                if (propertyDictionary[themeId].ContainsKey("0." + key))
                {
                    return propertyDictionary[themeId]["0." + key].ToString();
                }
                else if (propertyDictionary[defaultTheme.Id].ContainsKey("0." + key))
                {
                    return propertyDictionary[defaultTheme.Id]["0." + key].ToString();
                }                

				return null;
			}
		}

		/// Alternative indexer property that also takes a partner ID. To allow white label variations.
		/// Searches for a key that is composed of partnerID and key
        [Obsolete("Don't use this method", true)]
		public string this [string key, int partnerID]
		{
			get
			{
				string prefixedKey = partnerID + "." + key;


				if (propertyTable.ContainsKey(prefixedKey))
					return this[prefixedKey].ToString();
				else
					if (propertyTable.ContainsKey("0." + key))
						return this["0." + key].ToString();
					else
						return null;
				
			}
		}

        public string ThemeID
        {
            get
            {
                return strThemeID;
            }
        }

		public string ApplicationID
		{
			get
			{
				return strApplicationID;
			}
		}

		public string GroupID
		{
			get
			{
				return strGroupID;
			}
		}



		public static IPropertyProvider Current
		{
			get
			{

					
					return (IPropertyProvider)TDServiceDiscovery.Current[ServiceDiscoveryKey.PropertyService];
									
				
			}
		}


		public abstract IPropertyProvider Load();
		
		

		public  void Supersede()
		{
			boolSuperseded = true;
			Superseded(this, new EventArgs());
		}
		
	
	}
}
