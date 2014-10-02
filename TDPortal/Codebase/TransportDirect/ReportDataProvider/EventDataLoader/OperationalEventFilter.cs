// *********************************************** 
// NAME                 : OperationalEventFilter.cs 
// AUTHOR               : Jonathan George
// DATE CREATED         : 01/07/2004
// DESCRIPTION  : Class used to filter operational events
// when loaded from the database
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/EventDataLoader/OperationalEventFilter.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:38:28   mturner
//Initial revision.
//
//   Rev 1.4   Jul 12 2004 10:34:10   jgeorge
//Modifications to the way filtering works
//
//   Rev 1.3   Jul 02 2004 10:07:10   jgeorge
//Updated to fix bugs highlighted by unit testing
//
//   Rev 1.2   Jul 01 2004 17:15:48   jgeorge
//Interim check-in
//
//   Rev 1.1   Jul 01 2004 15:58:02   jgeorge
//Work in progress
//
//   Rev 1.0   Jul 01 2004 14:58:58   jgeorge
//Initial revision.

using System;
using System.Collections;

using TransportDirect.Common.Logging;

namespace TransportDirect.ReportDataProvider.EventDataLoader
{

	#region Enumerations

	/// <summary>
	/// Enumeration specifying how to apply an OperationalEventFilter
	/// to a LoadedOperationalEvent
	/// </summary>
	public enum OperationalEventFilterMethod
	{
		And,	// All filters will be applied and all must be met to return an event
		Or,		// Only one filter must be met to return an event
		AndNot,	// None of the filters must be met to return an event
		OrNot
	}

	public enum OperationalEventFilterType
	{
		Standard,
		Composite
	}

	/// <summary>
	/// Field within the Operational Event to perform a string match agains
	/// </summary>
	public enum OperationalEventMatchField
	{
		MessageContains,
		MessageEquals,
		SessionIdEquals,
		MachineNameEquals,
		TypeNameEquals,
		MethodNameEquals,
		AssemblyNameEquals	
	}

	#endregion

	/// <summary>
	/// Summary description for OperationalEventFilter.
	/// </summary>
	public class OperationalEventFilter
	{
		// Date and time can be filtered to be within a range. These will be initialised to 
		// min and max allowable respectively
		private readonly DateTime startTime = DateTime.MinValue;
		private readonly DateTime endTime = DateTime.MaxValue;

		// All fields can be matched against a range of values. If the arraylist
		// for a field is empty, then there are no criteria for that field
		private readonly bool caseSensitive = false;
		private readonly string messageEquals = string.Empty;
		private readonly string messageContains = string.Empty;
		private readonly string sessionId = string.Empty;
		private readonly bool useCategory = false;
		private readonly TDEventCategory category;
		private readonly bool useLevel = false;
		private readonly TDTraceLevel level;
		private readonly string machineName = string.Empty;
		private readonly string typeName = string.Empty;
		private readonly string methodName = string.Empty;
		private readonly string assemblyName = string.Empty;
		private readonly OperationalEventFilterMethod method;

		private readonly OperationalEventFilter[] filters;

		private readonly OperationalEventFilterType type;

		#region Constructors for a matching type filter

		/// <summary>
		/// Private constructor chained to all public constructors
		/// </summary>
		/// <param name="method"></param>
		private OperationalEventFilter(OperationalEventFilterMethod method, OperationalEventFilterType type)
		{
			this.method = method;
			this.type = type;
		}

		/// <summary>
		/// Constructor for a filter using a time interval
		/// </summary>
		/// <param name="startTime"></param>
		/// <param name="endTime"></param>
		/// <param name="method"></param>
		public OperationalEventFilter(DateTime startTime, DateTime endTime, OperationalEventFilterMethod method) : this(method, OperationalEventFilterType.Standard)
		{
			this.startTime = startTime;
			this.endTime = endTime;
		}

		/// <summary>
		/// Constructor for a filter which will match a substring of the event message
		/// </summary>
		/// <param name="messageContains"></param>
		/// <param name="method"></param>
		public OperationalEventFilter(string matchString, OperationalEventMatchField matchField, bool caseSensitive, OperationalEventFilterMethod method) : this(method, OperationalEventFilterType.Standard)
		{
			switch (matchField)
			{
				case OperationalEventMatchField.MessageContains:
					this.messageContains = caseSensitive ? matchString : matchString.ToLower();
					break;
				case OperationalEventMatchField.MessageEquals:
					this.messageEquals = caseSensitive ? matchString : matchString.ToLower();
					break;
				case OperationalEventMatchField.SessionIdEquals:
					this.sessionId = caseSensitive ? matchString : matchString.ToLower();
					break;
				case OperationalEventMatchField.MachineNameEquals:
					this.machineName = caseSensitive ? matchString : matchString.ToLower();
					break;
				case OperationalEventMatchField.TypeNameEquals:
					this.typeName = caseSensitive ? matchString : matchString.ToLower();
					break;
				case OperationalEventMatchField.MethodNameEquals:
					this.methodName = caseSensitive ? matchString : matchString.ToLower();
					break;
				case OperationalEventMatchField.AssemblyNameEquals:
					this.assemblyName = caseSensitive ? matchString : matchString.ToLower();
					break;

			}
			this.caseSensitive = caseSensitive;
		}

		/// <summary>
		/// Constructor for a filter using the Event Category
		/// </summary>
		/// <param name="category"></param>
		/// <param name="method"></param>
		public OperationalEventFilter(TDEventCategory category, OperationalEventFilterMethod method) : this(method, OperationalEventFilterType.Standard)
		{
			this.category = category;
			useCategory = true;
		}

		/// <summary>
		/// Constructor for a filter using the trace level
		/// </summary>
		/// <param name="level"></param>
		/// <param name="method"></param>
		public OperationalEventFilter(TDTraceLevel level, OperationalEventFilterMethod method) : this(method, OperationalEventFilterType.Standard)
		{
			this.level = level;
			useLevel = true;
		}

		#endregion

		#region Constructors for a composite filter

		public OperationalEventFilter(OperationalEventFilter[] filters, OperationalEventFilterMethod method) : this(method, OperationalEventFilterType.Composite)
		{
			this.filters = (OperationalEventFilter[])filters.Clone();
		}

		#endregion

		#region Matching methods

		/// <summary>
		/// Returns true if the supplied operational event matches the filter
		/// </summary>
		/// <param name="loe"></param>
		/// <returns></returns>
		public bool IsMatch(LoadedOperationalEvent loe)
		{
			return IsMatch(loe.Time, loe.SessionId, loe.Message, loe.Category, loe.Level, loe.MachineName, loe.TypeName, loe.MethodName, loe.AssemblyName);
		}

		/// <summary>
		/// Returns true if the supplied operational event data matches the filter
		/// </summary>
		/// <param name="loe"></param>
		/// <returns></returns>
		public bool IsMatch(DateTime time, 
			string sessionId,
			string message, 
			TDEventCategory category, 
			TDTraceLevel level, 
			string machineName,
			string typeName,
			string methodName,
			string assemblyName)
		{

			if (type == OperationalEventFilterType.Standard)
			{
				// Check date
				if ( (DateTime.Compare(time, startTime) < 0) || (DateTime.Compare(endTime, time) < 0) )
					return false;

				// Check other items
				if (this.messageContains.Length != 0 && caseSensitive ? message.IndexOf(messageContains) == -1 : message.ToLower().IndexOf(messageContains) == -1)
					return false;

				if (this.messageEquals.Length != 0 && this.messageEquals != (caseSensitive ? message : message.ToLower()))
					return false;

				if (this.sessionId.Length != 0 && this.sessionId != (caseSensitive ? sessionId : sessionId.ToLower()))
					return false;

				if (this.useCategory && this.category != category)
					return false;

				if (this.useLevel && this.level != level)
					return false;

				if (this.machineName.Length != 0 && this.machineName != (caseSensitive ? machineName : machineName.ToLower()))
					return false;

				if (this.typeName.Length != 0 && this.typeName != (caseSensitive ? typeName : typeName.ToLower()))
					return false;

				if (this.methodName.Length != 0 && this.methodName != (caseSensitive ? methodName : methodName.ToLower()))
					return false;

				if (this.assemblyName.Length != 0 && this.assemblyName != (caseSensitive ? assemblyName : assemblyName.ToLower()))
					return false;

				return true;
			}
			else if (type == OperationalEventFilterType.Composite)
			{
				// Need to try matching against each filter in turn
				if ((filters == null) || (filters.Length == 0))
					return true;
				else
				{
					// Work out what the inital value should be
					bool useEvent = (filters[0].Method == OperationalEventFilterMethod.And) || (filters[0].Method == OperationalEventFilterMethod.AndNot);

					foreach (OperationalEventFilter filter in filters)
					{
						if (filter.Method == OperationalEventFilterMethod.And)
							useEvent = useEvent && filter.IsMatch(time, sessionId, message, category, level, machineName, typeName, methodName, assemblyName);
						else if (filter.Method == OperationalEventFilterMethod.AndNot)
							useEvent = useEvent && !filter.IsMatch(time, sessionId, message, category, level, machineName, typeName, methodName, assemblyName);
						else if (filter.Method == OperationalEventFilterMethod.Or)
							useEvent = useEvent || filter.IsMatch(time, sessionId, message, category, level, machineName, typeName, methodName, assemblyName);
						else if (filter.Method == OperationalEventFilterMethod.OrNot)
							useEvent = useEvent || !filter.IsMatch(time, sessionId, message, category, level, machineName, typeName, methodName, assemblyName);
					}

					// If we have reached this point, we know that all of the And conditions
					// were met, so we return the result of the Or conditions
					return useEvent;
				}
			}
			else
				return false;
		}

		#endregion

		#region Properties

		/// <summary>
		/// How the filter should be used by clients
		/// </summary>
		public OperationalEventFilterMethod Method
		{
			get { return method; }
		}

		/// <summary>
		/// The type of filter (standard or composite)
		/// </summary>
		public OperationalEventFilterType Type
		{
			get { return type; }
		}

		/// <summary>
		/// Returns any sub filters
		/// </summary>
		public OperationalEventFilter[] Filters
		{
			get 
			{
				if (filters == null)
					return null;
				else
					return (OperationalEventFilter[])filters.Clone();
			}
		}

		/// <summary>
		/// Start of the time period within which the OEs must have been logged
		/// </summary>
		public DateTime StartTime
		{
			get { return startTime; }
		}

		/// <summary>
		/// End of the time period within which the OEs must have been logged
		/// </summary>
		public DateTime EndTime
		{
			get { return endTime; }
		}

		/// <summary>
		/// Allowable value for the SessionId field of the OE
		/// </summary>
		public string SessionId
		{
			get { return sessionId; }
		}

		/// <summary>
		/// String which must be contained in the Message field of the OE
		/// </summary>
		public string MessageContains
		{
			get { return messageContains; }
		}

		/// <summary>
		/// String which must match the Message field of the OE
		/// </summary>
		public string MessageEquals
		{
			get { return messageEquals; }
		}

		/// <summary>
		/// Allowable values for the Category field of the OE
		/// </summary>
		public TDEventCategory[] Categories
		{
			get 
			{ 
				if (useCategory)
					return new TDEventCategory[] {category};
				else
					return new TDEventCategory[0];
			}
		}

		/// <summary>
		/// Allowable values for the Level field of the OE
		/// </summary>
		public TDTraceLevel[] Levels
		{
			get 
			{ 
				if (useLevel)
					return new TDTraceLevel[] {level};
				else
					return new TDTraceLevel[0];
			}
		}

		/// <summary>
		/// Allowable value for the MachineName field of the OE
		/// </summary>
		public string MachineName
		{
			get { return machineName; }
		}

		/// <summary>
		/// Allowable value for the TypeName field of the OE
		/// </summary>
		public string TypeName
		{
			get { return typeName; }
		}

		/// <summary>
		/// Allowable value for the MethodName field of the OE
		/// </summary>
		public string MethodName
		{
			get { return methodName; }
		}

		/// <summary>
		/// Allowable value for the AssemblyName field of the OE
		/// </summary>
		public string AssemblyNames
		{
			get { return assemblyName; }
		}

		#endregion
	}
}
