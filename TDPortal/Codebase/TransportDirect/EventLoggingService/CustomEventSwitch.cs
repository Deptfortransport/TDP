// *********************************************** 
// NAME                 : CustomEventSwitch.cs 
// AUTHOR               : Gary Eaton
// DATE CREATED         : 11/07/2003 
// DESCRIPTION  : Static class used to determine
// the logging level of custom events.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EventLoggingService/CustomEventSwitch.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:23:00   mturner
//Initial revision.
//
//   Rev 1.7   Oct 10 2003 15:17:26   geaton
//Removed logging of Operational events that may cause recursion.
//
//   Rev 1.6   Oct 09 2003 13:11:40   geaton
//Support for error handling should an unknown event class be logged.
//
//   Rev 1.5   Oct 07 2003 13:40:36   geaton
//Updates following introduction of TDExceptionIdentifier.
//
//   Rev 1.4   Sep 12 2003 13:25:58   geaton
//Bug fix. Global custom event level was not being checked before Individual custom event level.
//
//   Rev 1.3   Jul 30 2003 11:57:58   geaton
//Updated comment.
//
//   Rev 1.2   Jul 25 2003 14:14:28   geaton
//Changes resulting from code review 2003-07-22. (These were mainly concerned with adding comments and running through FXCop).
//
//   Rev 1.1   Jul 24 2003 18:27:30   geaton
//Added/updated comments

using System;
using System.Collections;
using System.Diagnostics;

namespace TransportDirect.Common.Logging
{
	/// <summary>
	/// Static class used to determine the level of <c>CustomEvent</c>s.
	/// </summary>
	public class CustomEventSwitch
	{
		/// <summary>
		/// Contains a level setting for each configured <c>CustomEvent</c>.
		/// </summary>
		private static Hashtable individualLevels;

		/// <summary>
		/// Constains the level setting across all CustomEvents,
		/// indexed by <c>CustomEvent</c> class names.
		/// </summary>
		private static CustomEventLevel globalLevel;

		/// <summary>
		/// Default constructor.
		/// </summary>
		static CustomEventSwitch()
		{
			// Set global level to undefined until properties service notifies 
			// this class of the value read from the configuration settings.
			globalLevel = CustomEventLevel.Undefined;

			// Note that until the properties service notifies this class 
			// of the real values, this hash table will not contain any keys.
			individualLevels = new Hashtable();
	
			// Register listener for changes in levels including the actual initialisation of levels
			TDTraceListener.CustomLevelChange += new CustomLevelChangeEventHandler(CustomLevelChangeEventHandler);
			TDTraceListener.CustomLevelsChange += new CustomLevelsChangeEventHandler(CustomLevelsChangeEventHandler);
		}

		private static void CustomLevelChangeEventHandler(object sender, CustomLevelEventArgs customLevelEventArgs)
		{
			globalLevel = customLevelEventArgs.CustomLevel;
		}

		private static void CustomLevelsChangeEventHandler(object sender, CustomLevelsEventArgs customLevelsEventArgs)
		{
			IDictionaryEnumerator levelsEnum = 
				(IDictionaryEnumerator) customLevelsEventArgs.CustomLevels.GetEnumerator();

			while (levelsEnum.MoveNext())
			{
				object key = levelsEnum.Key;
				string keyName = key.ToString();
				object levelValue = levelsEnum.Value;

				individualLevels[keyName] = levelValue;
			}

		}

		/// <summary>
		/// Returns the global level across all Custom Events.
		/// </summary>
		/// <returns><c>true</c> is 'on' otherwise <c>false</c></returns>
		/// <exception cref="TransportDirect.Common.TDException">Level is undefined.</exception>"
		private static bool CheckLevel()
		{
			if (globalLevel == CustomEventLevel.Undefined)
			{
				// NB this test is performed to catch cases where level is changed at runtime to an undefined value. 
				throw new TDException(Messages.CustomEventSwitchNotInitialised, false, TDExceptionIdentifier.ELSGlobalTraceLevelUndefined);
			}
			else
			{
				return (globalLevel == CustomEventLevel.On);
			}
		}

		/// <summary>
		/// Indicates the value of an individual <c>CustomEvent</c> level.
		/// This method takes into account the global trace level for custom events
		/// which overrides individual levels if it is set to off.
		/// </summary>
		/// <param name="level">The level to which the check is being made.</param>
		/// <param name="eventClassName">The class name (not including path) of the <c>CustomEvent</c></param>
		/// <returns><c>true</c> if the level passed in <c>level</c> matches 
		/// the current level for <c>CustomEvent</c> with class name <c>eventClassName</c>.</returns>
		private static bool CheckLevel(CustomEventLevel level, string eventClassName)
		{
			if (CheckLevel()) // check global level
			{
				try
				{
					if (individualLevels.ContainsKey((object)eventClassName))
						return ((CustomEventLevel)individualLevels[eventClassName] == level);
					else
					{
						throw new TDException(String.Format(Messages.UnknownCustomEventClassName, eventClassName), false, TDExceptionIdentifier.ELSSwitchOnUnknownEvent);
					}
				}
				catch (Exception exception)  // catch undocumented exceptions
				{
					throw new TDException(String.Format(Messages.UnknownCustomEventClassName, eventClassName), exception, false, TDExceptionIdentifier.ELSSwitchOnUnknownEvent);
				}
			}
			else
			{
				return false; // global level set to off
			}
		}

		/// <summary>
		/// Indicates whether a <c>CustomEvent</c>s level is 'On'.
		/// </summary>
		/// <param name="eventClassName">Class name of <c>CustomEvent</c> not including path.</param>
		/// <returns><c>true</c> if <c>CustomEvent</c> has level of 'On', otherwise <c>false</c>.</returns> 
		public static bool On(string eventClassName)
		{
			return CheckLevel(CustomEventLevel.On, eventClassName);
		}

		/// <summary>
		/// Indicates whether a <c>CustomEvent</c>s level is 'Off'.
		/// </summary>
		/// <param name="eventClassName">Class name of <c>CustomEvent</c> not including path.</param>
		/// <returns><c>true</c> if <c>CustomEvent</c> has level of 'Off', otherwise <c>false</c>.</returns> 
		public static bool Off(string eventClassName)
		{
			return CheckLevel(CustomEventLevel.Off, eventClassName);
		}

		/// <summary>
		/// Gets the global on event level indicator. <c>true</c> if event level across all CustomEvents is 'On'.
		/// </summary>
		/// <returns><c>true</c> if global level is 'On', otherwise <c>false</c>.</returns> 
		public static bool GlobalOn
		{
			get {return CheckLevel();}
		}

	}
}
