// *********************************************** 
// NAME                 : PropertyServiceFactory.cs
// AUTHOR               : Patrick ASSUIED
// DATE CREATED         : 11/07/2003 
// DESCRIPTION  : Factory class for the Property Service. Implements IServiceFactory
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/PropertiesService/Properties/PropertyServiceFactory.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:37:52   mturner
//Initial revision.
//
//   Rev 1.5   Oct 03 2003 13:38:42   PNorell
//Updated the new exception identifier.
//
//   Rev 1.4   Jul 24 2003 13:57:04   passuied
//handling exception added in ServiceFactory
//
//   Rev 1.3   Jul 23 2003 10:22:58   passuied
//Changes after PropertyService namespaces / dll renaming
//
//   Rev 1.2   Jul 17 2003 17:18:14   passuied
//added IFormatProvider
//
//   Rev 1.1   Jul 17 2003 15:00:36   passuied
//updated


using System;
using System.Configuration;
using System.Reflection;
using System.Timers;
using TransportDirect.Common.ServiceDiscovery;
using System.Globalization;
using System.IO;
using System.Security;
using TransportDirect.Common;


namespace TransportDirect.Common.PropertyService.Properties
{
	/// <summary>
	/// Factory class for the Property Service. Based on IServiceFactory
	/// </summary>
	public class PropertyServiceFactory : IServiceFactory
	{

		private IPropertyProvider current = null;
		private Timer myTimer;
		public PropertyServiceFactory()
		{		
			string strAssembly =
                ConfigurationManager.AppSettings
				["propertyservice.providerassembly"];
			string strClass =
                ConfigurationManager.AppSettings
				["propertyservice.providerclass"];

			Assembly aAssembly;
			Properties newInstance;
			try
			
			{
				// Instantiate it according to properties
				aAssembly = Assembly.Load(strAssembly);
				
			}
			catch( ArgumentNullException)
			{
				throw new TDException(
					"Exception loading the assembly. Argument is null", 
					false, 
					TDExceptionIdentifier.PSInvalidAssembly);
			}
			catch ( FileNotFoundException)
			{
				throw new TDException(
					"Exception loading the assembly. File not Found",
					false, 
					TDExceptionIdentifier.PSInvalidAssembly);
			}

			try
			{

				//Type[] temp = aAssembly.GetTypes(); //for Test
				Type tProvider = aAssembly.GetType(strClass);

				newInstance  = 
					(Properties)Activator.CreateInstance (tProvider);

			}
			catch ( ReflectionTypeLoadException)
			{
				throw new TDException(
					"Exception Getting the type. Type not found in assembly", 
					false, 
					TDExceptionIdentifier.PSInvalidAssemblyType);
			}
			catch (ArgumentException)
			{
				throw new TDException(
					"Exception Getting the type. Type is invalid/ null", 
					false, 
					TDExceptionIdentifier.PSInvalidAssemblyType);
			}
			
			
			// Call Load in new instantiated object
			IPropertyProvider newCurrent = newInstance.Load();

			// if the object has been successfully loaded, then 
			if (newCurrent!= null)
			{
				// Register as current
				Register(newCurrent);

				// Timer Init
				myTimer = new Timer();
				myTimer.Elapsed += new ElapsedEventHandler(TimerElapsed);

				// retrieve refresh rate
				RetrieveRefreshRate();

			}
		}

		public object Get()
		{

			return current;

		}

		public void Register (IPropertyProvider provider)
		{
			current = provider;
		}

		private void TimerElapsed( object sender, ElapsedEventArgs e)
		{
			// Load 
			IPropertyProvider returnedInstance= ((Properties)current).Load();

			
					
			// if object returned not null,
			if (returnedInstance != null)
			{
				Properties oldCurrent = (Properties)current;
				
				//register new instance as current
				Register(returnedInstance);
					
				// retrieve refresh rate
				RetrieveRefreshRate();

				// Supersede
				oldCurrent.Supersede();
			}



		}


		private void RetrieveRefreshRate()
		{
			int refreshRate = Convert.ToInt32
				((string)current["propertyservice.refreshrate"], CultureInfo.CurrentCulture.NumberFormat);

			Properties currentProperties = (Properties)current;

			myTimer.Enabled = false;
			myTimer.Interval = (double)refreshRate;
			myTimer.AutoReset = true;	
			
			myTimer.Start();

		}
	}
}
