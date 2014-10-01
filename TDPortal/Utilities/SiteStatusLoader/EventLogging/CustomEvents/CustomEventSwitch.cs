// *********************************************** 
// NAME                 : CustomEventSwitch.cs
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 01/04/2009
// DESCRIPTION			: Static class used to determine the level of CustomEvent logging
// ************************************************ 
// $Log:   P:/TDPortal/archives/Utilities/SiteStatusLoader/EventLogging/CustomEvents/CustomEventSwitch.cs-arc  $
//
//   Rev 1.0   Apr 01 2009 13:28:16   mmodi
//Initial revision.
//Resolution for 5273: Site Status Loader

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using AO.Common;

namespace AO.EventLogging
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
            SSTraceListener.CustomLevelChange += new CustomLevelChangeEventHandler(CustomLevelChangeEventHandler);
            SSTraceListener.CustomLevelsChange += new CustomLevelsChangeEventHandler(CustomLevelsChangeEventHandler);
        }

        private static void CustomLevelChangeEventHandler(object sender, CustomLevelEventArgs customLevelEventArgs)
        {
            globalLevel = customLevelEventArgs.CustomLevel;
        }

        private static void CustomLevelsChangeEventHandler(object sender, CustomLevelsEventArgs customLevelsEventArgs)
        {
            IDictionaryEnumerator levelsEnum =
                (IDictionaryEnumerator)customLevelsEventArgs.CustomLevels.GetEnumerator();

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
                throw new SSException(Messages.SSTCustomEventSwitchNotInitialised, false, SSExceptionIdentifier.ELSGlobalTraceLevelUndefined);
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
                        throw new SSException(String.Format(Messages.SSTUnknownCustomEventClassName, eventClassName), false, SSExceptionIdentifier.ELSSwitchOnUnknownEvent);
                    }
                }
                catch (Exception exception)  // catch undocumented exceptions
                {
                    throw new SSException(String.Format(Messages.SSTUnknownCustomEventClassName, eventClassName), exception, false, SSExceptionIdentifier.ELSSwitchOnUnknownEvent);
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
            get { return CheckLevel(); }
        }
    }
}
