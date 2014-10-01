// *********************************************** 
// NAME                 : OperationalEvent.cs 
// AUTHOR               : Gary Eaton
// DATE CREATED         : 11/07/2003 
// DESCRIPTION  : Defines an operational event.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EventLoggingService/OperationalEvent.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:23:06   mturner
//Initial revision.
//
//   Rev 1.10   Jun 30 2004 17:02:52   jgeorge
//Changes for "force logging" functionality
//
//   Rev 1.9   Oct 07 2003 13:40:44   geaton
//Updates following introduction of TDExceptionIdentifier.
//
//   Rev 1.8   Aug 22 2003 10:18:32   geaton
//Added code to set ClassName which is used by TDTraceListener. This reduces reflection needed at publish time.
//
//   Rev 1.7   Jul 30 2003 18:45:10   geaton
//Removed redundant constructor from OperationalEvent class and updated references to use alternative constructor.
//
//   Rev 1.6   Jul 30 2003 18:08:36   geaton
//Changes to OperationalEvent constructors
//
//   Rev 1.5   Jul 30 2003 08:56:16   geaton
//Redefined constructor for backwards compatibilty. (Note that this constructor should not be used for new code.) This constructor only differs in parameter order to an another constructor.
//
//   Rev 1.4   Jul 29 2003 17:31:32   geaton
//Removed referencenumber property from LogEvent and changed OperationalEvent constructors
//
//   Rev 1.3   Jul 25 2003 14:14:36   geaton
//Changes resulting from code review 2003-07-22. (These were mainly concerned with adding comments and running through FXCop).
//
//   Rev 1.2   Jul 24 2003 18:27:44   geaton
//Added/updated comments

using System;
using System.Text;
using System.Diagnostics;
using System.Reflection;
using System.Collections;

namespace TransportDirect.Common.Logging
{
	/// <summary>
	/// OperationalEvent class.
	/// Exhibits serializable attribute to allow publishing on MSMQ.
	/// </summary>
	[Serializable]
	public sealed class OperationalEvent : LogEvent
	{
		private TDEventCategory category;
		private TDTraceLevel level;
		private string message;
		private object target;
		private string typeName;
		private string methodName;
		private string assemblyName;
		private string machineName;
		private const string ContextUnassigned = "No Context";
		private string sessionId;
		private TDTraceLevelOverride overrideLevel;

		public const string SessionIdUnassigned = "No Session";

		// Prefix for forming the class name that is used by TDTraceListener to associate operational event levels with publishers
		public static readonly string ClassNamePrefix = "OperationalEvent";

		/// <summary>
		/// Create formatters using the standard formatters of the event logging framework.
		/// </summary>
		private static IEventFormatter fileFormatter = new OperationalEventFileFormatter();
		private static IEventFormatter emailFormatter = new OperationalEventEmailFormatter();
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
		/// Gets session id.
		/// </summary>
		public string SessionId
		{
			get {return sessionId;}
		}

		/// <summary>
		/// Gets category.
		/// </summary>
		public TDEventCategory Category
		{
			get {return category;}
		}

		/// <summary>
		/// Gets level.
		/// </summary>
		public TDTraceLevel Level
		{
			get {return level;}
		}

		/// <summary>
		/// Gets override level
		/// </summary>
		public TDTraceLevelOverride OverrideLevel
		{
			get {return overrideLevel;}
		}

		/// <summary>
		/// Gets message.
		/// </summary>
		public string Message
		{
			get {return message;}
		}

		/// <summary>
		/// Gets target.
		/// </summary>
		public object Target
		{
			get {return target;}
		}

		/// <summary>
		/// Gets type name.
		/// </summary>
		public string TypeName
		{
			get {return typeName;}
		}

		/// <summary>
		/// Gets method name.
		/// </summary>
		public string MethodName
		{
			get {return methodName;}
		}

		/// <summary>
		/// Gets assembly name.
		/// </summary>
		public string AssemblyName
		{
			get {return assemblyName;}
		}

		/// <summary>
		/// Gets machine name.
		/// </summary>
		public string MachineName
		{
			get {return machineName;}
		}

		/// <summary>
		/// Gets file formatter.
		/// </summary>
		override public IEventFormatter FileFormatter
		{
			get {return fileFormatter;}
		}

		/// <summary>
		/// Gets email formatter.
		/// </summary>
		override public IEventFormatter EmailFormatter
		{
			get {return emailFormatter;}
		}

		/// <summary>
		/// Gets event log formatter.
		/// </summary>
		override public IEventFormatter EventLogFormatter
		{
			get {return eventLogFormatter;}
		}

		/// <summary>
		/// Gets console formatter.
		/// </summary>
		override public IEventFormatter ConsoleFormatter
		{
			get {return consoleFormatter;}
		}

		/// <summary>
		/// Gets filter.
		/// </summary>
		override public IEventFilter Filter
		{
			get {return (IEventFilter)filter[overrideLevel];}
		}

		#endregion

		#region Constructors

		/// <summary>
		/// Static constructor which initialises the Hashtable
		/// of OperationalEventFilter objects corresponding to
		/// each TDTraceLevelOverride value
		/// </summary>
		static OperationalEvent()
		{
			TDTraceLevelOverride[] allValues = (TDTraceLevelOverride[])Enum.GetValues(typeof(TDTraceLevelOverride));
			filter = new Hashtable(allValues.Length);
			foreach (TDTraceLevelOverride curr in allValues)
				filter.Add(curr, new OperationalEventFilter(curr));
		}

		/// <summary>
		/// Constructor based on category, level and message
		/// </summary>
		/// <param name="category">Category of event.</param>
		/// <param name="level">Level of event.</param>
		/// <param name="message">Message associated with event.</param>
		public OperationalEvent(TDEventCategory category, 					
								TDTraceLevel level,
								string message) : base()
		{
			AssignProperties(category, level, message, null, SessionIdUnassigned, TDTraceLevelOverride.None);
			SetContextProperties();
		}

		/// <summary>
		/// Constructor based on category, level and message
		/// </summary>
		/// <param name="category">Category of event.</param>
		/// <param name="level">Level of event.</param>
		/// <param name="message">Message associated with event.</param>
		/// <param name="overrideLevel">Override level to use</param>
		public OperationalEvent(TDEventCategory category, 					
			TDTraceLevel level,
			string message,
			TDTraceLevelOverride overrideLevel) : base()
		{
			AssignProperties(category, level, message, null, SessionIdUnassigned, overrideLevel);
			SetContextProperties();
		}

		/// <summary>
		/// Constructor based on category, level, message and target
		/// </summary>
		/// <param name="category">Category of event.</param>
		/// <param name="level">Level of event.</param>
		/// <param name="message">Message associated with event.</param>
		/// <param name="target">Target object associated with event.</param>
		public OperationalEvent(TDEventCategory category,
								TDTraceLevel level,
								string message,
								object target) : base()
		{
			AssignProperties(category, level, message, target, SessionIdUnassigned, TDTraceLevelOverride.None);
			SetContextProperties();
		}

		/// <summary>
		/// Constructor based on category, level, message and target
		/// </summary>
		/// <param name="category">Category of event.</param>
		/// <param name="level">Level of event.</param>
		/// <param name="message">Message associated with event.</param>
		/// <param name="target">Target object associated with event.</param>
		/// <param name="overrideLevel">Override level to use</param>
		public OperationalEvent(TDEventCategory category,
			TDTraceLevel level,
			string message,
			object target,
			TDTraceLevelOverride overrideLevel) : base()
		{
			AssignProperties(category, level, message, target, SessionIdUnassigned, overrideLevel);
			SetContextProperties();
		}

		/// <summary>
		/// Constructor based on category, level, message and session id
		/// </summary>
		/// <param name="category">Category of event.</param>
		/// <param name="level">Level of event.</param>
		/// <param name="message">Message associated with event.</param>
		/// <param name="sessionId">Identifies the ASP.NET session associated with event.</param>
		public OperationalEvent(TDEventCategory category,
								string sessionId,
								TDTraceLevel level,
								string message) : base()
		{
			AssignProperties(category, level, message, null, sessionId, TDTraceLevelOverride.None);
			SetContextProperties();
		}

		/// <summary>
		/// Constructor based on category, level, message and session id
		/// </summary>
		/// <param name="category">Category of event.</param>
		/// <param name="level">Level of event.</param>
		/// <param name="message">Message associated with event.</param>
		/// <param name="sessionId">Identifies the ASP.NET session associated with event.</param>
		/// <param name="overrideLevel">Override level to use</param>
		public OperationalEvent(TDEventCategory category,
			string sessionId,
			TDTraceLevel level,
			string message,
			TDTraceLevelOverride overrideLevel) : base()
		{
			AssignProperties(category, level, message, null, sessionId, overrideLevel);
			SetContextProperties();
		}

		/// <summary>
		/// Constructor based on category, level, message, target and session identifier
		/// </summary>
		/// <param name="category">Category of event.</param>
		/// <param name="level">Level of event.</param>
		/// <param name="message">Message associated with event.</param>
		/// <param name="target">Target object associated with event.</param>
		/// <param name="sessionId">Identifies the ASP.NET session associated with event.</param>
		public OperationalEvent(TDEventCategory category, 
								TDTraceLevel level,
								string message,
								object target,
								string sessionId) : base()
		{
			AssignProperties(category, level, message, target, sessionId, TDTraceLevelOverride.None);
			SetContextProperties();
		}

		/// <summary>
		/// Constructor based on category, level, message, target and session identifier
		/// </summary>
		/// <param name="category">Category of event.</param>
		/// <param name="level">Level of event.</param>
		/// <param name="message">Message associated with event.</param>
		/// <param name="target">Target object associated with event.</param>
		/// <param name="sessionId">Identifies the ASP.NET session associated with event.</param>
		/// <param name="overrideLevel">Override level to use</param>
		public OperationalEvent(TDEventCategory category, 
			TDTraceLevel level,
			string message,
			object target,
			string sessionId,
			TDTraceLevelOverride overrideLevel) : base()
		{
			AssignProperties(category, level, message, target, sessionId, overrideLevel);
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
		private void AssignProperties(TDEventCategory category, 
			TDTraceLevel level,
			string message,
			object target,
			string sessionId,
			TDTraceLevelOverride overrideLevel)
		{
			if ((level == TDTraceLevel.Off) || (level == TDTraceLevel.Undefined))
				throw new TDException(Messages.OperationalEventLevelInvalid, false, TDExceptionIdentifier.ELSCustomOperationalConstructor);

			this.category = category;
			this.level = level;
			this.message = message;
			this.target = target;
			this.sessionId = sessionId;
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
				this.typeName =  ContextUnassigned;
				this.assemblyName = ContextUnassigned;
				this.machineName = ContextUnassigned;
			}
		}

		#endregion

	}

}
