// *********************************************** 
// NAME                 : OperationalEvent.cs
// AUTHOR               : Mitesh Modi
// DATE CREATED         : 01/04/2009
// DESCRIPTION			: Class for raising OperationalEvents
// ************************************************ 
// $Log:   P:/TDPortal/archives/Utilities/SiteStatusLoader/EventLogging/OperationalEvent.cs-arc  $
//
//   Rev 1.0   Apr 01 2009 13:27:18   mmodi
//Initial revision.
//Resolution for 5273: Site Status Loader

using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Diagnostics;
using System.Reflection;

using AO.Common;

namespace AO.EventLogging
{
    /// <summary>
    /// OperationalEvent class.
    /// Exhibits serializable attribute to allow publishing on MSMQ.
    /// </summary>
    [Serializable]
    public sealed class OperationalEvent : LogEvent
    {
        private SSEventCategory category;
        private SSTraceLevel level;
        private SSTraceLevelOverride overrideLevel;
        private string message;
        private object target;
        private string typeName;
        private string methodName;
        private string assemblyName;
        private string machineName;

        // Prefix for forming the class name that is used by SSTraceListener to associate operational event levels with publishers
        public static readonly string ClassNamePrefix = "OperationalEvent";

        private const string ContextUnassigned = "No Context";

        /// <summary>
        /// Create formatters using the standard formatters of the event logging framework.
        /// </summary>
        private static IEventFormatter fileFormatter = new OperationalEventFileFormatter();
        private static IEventFormatter eventLogFormatter = new OperationalEventEventLogFormatter();
        private static IEventFormatter consoleFormatter = new OperationalEventConsoleFormatter();

        private static Hashtable filter;

        /// <summary>
        /// Number of stack frames to skip upwards to find the caller.
        /// This is set to 2 in order to find the method in which 
        /// the <c>OperationalEvent</c> was created.
        /// </summary>
        private const int StackFrameLevel = 2;

        #region Properties

        /// <summary>
        /// Gets category.
        /// </summary>
        public SSEventCategory Category
        {
            get { return category; }
        }

        /// <summary>
        /// Gets level.
        /// </summary>
        public SSTraceLevel Level
        {
            get { return level; }
        }

        /// <summary>
        /// Gets override level
        /// </summary>
        public SSTraceLevelOverride OverrideLevel
        {
            get { return overrideLevel; }
        }

        /// <summary>
        /// Gets message.
        /// </summary>
        public string Message
        {
            get { return message; }
        }

        /// <summary>
        /// Gets target.
        /// </summary>
        public object Target
        {
            get { return target; }
        }

        /// <summary>
        /// Gets type name.
        /// </summary>
        public string TypeName
        {
            get { return typeName; }
        }

        /// <summary>
        /// Gets method name.
        /// </summary>
        public string MethodName
        {
            get { return methodName; }
        }

        /// <summary>
        /// Gets assembly name.
        /// </summary>
        public string AssemblyName
        {
            get { return assemblyName; }
        }

        /// <summary>
        /// Gets machine name.
        /// </summary>
        public string MachineName
        {
            get { return machineName; }
        }

        /// <summary>
        /// Gets file formatter.
        /// </summary>
        override public IEventFormatter FileFormatter
        {
            get { return fileFormatter; }
        }

        /// <summary>
        /// Gets event log formatter.
        /// </summary>
        override public IEventFormatter EventLogFormatter
        {
            get { return eventLogFormatter; }
        }

        /// <summary>
        /// Gets console formatter.
        /// </summary>
        override public IEventFormatter ConsoleFormatter
        {
            get { return consoleFormatter; }
        }

        /// <summary>
        /// Gets filter.
        /// </summary>
        override public IEventFilter Filter
        {
            get { return (IEventFilter)filter[overrideLevel]; }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Static constructor 
        /// </summary>
        static OperationalEvent()
        {
            SSTraceLevelOverride[] allValues = (SSTraceLevelOverride[])Enum.GetValues(typeof(SSTraceLevelOverride));
            filter = new Hashtable(allValues.Length);
            foreach (SSTraceLevelOverride curr in allValues)
                filter.Add(curr, new OperationalEventFilter(curr));
        }

        /// <summary>
        /// Constructor based on category, level and message
        /// </summary>
        /// <param name="category">Category of event.</param>
        /// <param name="level">Level of event.</param>
        /// <param name="message">Message associated with event.</param>
        public OperationalEvent(SSEventCategory category,
                                SSTraceLevel level,
                                string message)
            : base()
        {
            AssignProperties(category, level, message, null, SSTraceLevelOverride.None);
            SetContextProperties();
        }

                /// <summary>
        /// Constructor based on category, level and message
        /// </summary>
        /// <param name="category">Category of event.</param>
        /// <param name="level">Level of event.</param>
        /// <param name="message">Message associated with event.</param>
        /// <param name="overrideLevel">Override level to use</param>
        public OperationalEvent(SSEventCategory category,
                                SSTraceLevel level,
                                string message,
                                SSTraceLevelOverride overrideLevel)
            : base()
        {
            AssignProperties(category, level, message, null, overrideLevel);
            SetContextProperties();
        }

        /// <summary>
        /// Constructor based on category, level, message and target
        /// </summary>
        /// <param name="category">Category of event.</param>
        /// <param name="level">Level of event.</param>
        /// <param name="message">Message associated with event.</param>
        /// <param name="target">Target object associated with event.</param>
        public OperationalEvent(SSEventCategory category,
                                SSTraceLevel level,
                                string message,
                                object target)
            : base()
        {
            AssignProperties(category, level, message, target, SSTraceLevelOverride.None);
            SetContextProperties();
        }

        /// <summary>
        /// Constructor based on category, level, message and target
        /// </summary>
        /// <param name="category">Category of event.</param>
        /// <param name="level">Level of event.</param>
        /// <param name="message">Message associated with event.</param>
        /// <param name="target">Target object associated with event.</param>
        public OperationalEvent(SSEventCategory category,
                                SSTraceLevel level,
                                string message,
                                object target,
                                SSTraceLevelOverride overrideLevel)
            : base()
        {
            AssignProperties(category, level, message, target, overrideLevel);
            SetContextProperties();
        }

        #endregion

        #region Helper methods

        /// <summary>
        /// Assigns properties of operational event.
        /// </summary>
        /// <remarks>
        /// The alternative to using this method would be to call a single constructor
        /// from each of the constructors. However, this means that the context properties
        /// do not get assigned the correct value, notably the Method property, since
        /// an extra call has been made.
        /// </remarks>
        private void AssignProperties(SSEventCategory category,
            SSTraceLevel level,
            string message,
            object target,
            SSTraceLevelOverride overrideLevel)
        {
            if ((level == SSTraceLevel.Off) || (level == SSTraceLevel.Undefined))
                throw new SSException(Messages.ELPVOperationalEventLevelInvalid, false, SSExceptionIdentifier.ELSCustomOperationalConstructor);

            this.category = category;
            this.level = level;
            this.message = message;
            this.target = target;
            this.overrideLevel = overrideLevel;
            
            // used to determine publisher at publish time
            this.ClassName = ClassNamePrefix + level.ToString();
        }

        private void SetContextProperties()
        {
            try
            {
                StackFrame stackFrame = new StackFrame(StackFrameLevel, false);
                MethodBase methodBase = stackFrame.GetMethod();
                this.methodName = methodBase.Name;
                this.typeName = methodBase.DeclaringType.Name;

                string assemblyQualifiedName =
                    methodBase.DeclaringType.AssemblyQualifiedName;
                assemblyName = assemblyQualifiedName.Split(',')[1].Trim();

                this.machineName = System.Environment.MachineName;
            }
            catch
            {
                this.methodName = ContextUnassigned;
                this.typeName = ContextUnassigned;
                this.assemblyName = ContextUnassigned;
                this.machineName = ContextUnassigned;
            }
        }

        #endregion

    }
}
