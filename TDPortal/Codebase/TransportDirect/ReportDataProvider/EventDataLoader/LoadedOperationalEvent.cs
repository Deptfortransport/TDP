// *********************************************** 
// NAME                 : LoadedOperationalEvent.cs 
// AUTHOR               : Jonathan George
// DATE CREATED         : 01/07/2004
// DESCRIPTION  : Class representing an operational event
// that has been loaded from a sink
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/EventDataLoader/LoadedOperationalEvent.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:38:26   mturner
//Initial revision.
//
//   Rev 1.3   Jul 02 2004 10:07:10   jgeorge
//Updated to fix bugs highlighted by unit testing
//
//   Rev 1.2   Jul 01 2004 17:15:46   jgeorge
//Interim check-in
//
//   Rev 1.1   Jul 01 2004 15:58:02   jgeorge
//Work in progress
//
//   Rev 1.0   Jul 01 2004 14:58:56   jgeorge
//Initial revision.

using System;
using System.Diagnostics;
using TransportDirect.Common;
using TransportDirect.Common.Logging;

namespace TransportDirect.ReportDataProvider.EventDataLoader
{
	/// <summary>
	/// Class representing an operational event that has been loaded from a sink
	/// </summary>
	public class LoadedOperationalEvent
	{
		private readonly DateTime time;
		private readonly string sessionId;
		private readonly string message;
		private readonly TDEventCategory category;
		private readonly TDTraceLevel level;
		private readonly string machineName;
		private readonly string typeName;
		private readonly string methodName;
		private readonly string assemblyName;

		#region Constructor

		/// <summary>
		/// Default constructor. Initialises the object
		/// </summary>
		/// <param name="time"></param>
		/// <param name="sessionId"></param>
		/// <param name="message"></param>
		/// <param name="category"></param>
		/// <param name="level"></param>
		/// <param name="machineName"></param>
		/// <param name="typeName"></param>
		/// <param name="methodName"></param>
		/// <param name="assemblyName"></param>
		/// <exception cref="TDException">An exception with Identifier TDExceptionIdentifier.EDLFailedConvertingTDEventCategory will be raised if the category parameter is not a valid TDEventCategory</exception>
		/// <exception cref="TDException">An exception with Identifier TDExceptionIdentifier.EDLFailedConvertingTDTraceLevel will be raised if the level parameter is not a valid TDTraceLevel</exception>
		public LoadedOperationalEvent(DateTime time, 
			string sessionId,
			string message, 
			string category, 
			string level, 
			string machineName,
			string typeName,
			string methodName,
			string assemblyName)
		{
			try
			{
				this.category = (TDEventCategory)Enum.Parse(typeof(TDEventCategory), category, true);
			}
			catch (ArgumentException e)
			{
				OperationalEvent oe = new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, String.Format(Messages.InvalidEventCategory, category), e);
				Trace.Write(oe);
				throw new TDException(oe.Message, e, oe.PublishedBy.Length != 0, TDExceptionIdentifier.EDLFailedConvertingTDEventCategory);
			}

			try
			{
				this.level = (TDTraceLevel)Enum.Parse(typeof(TDTraceLevel), level, true);
			}
			catch (ArgumentException e)
			{
				OperationalEvent oe = new OperationalEvent(TDEventCategory.Business, TDTraceLevel.Error, String.Format(Messages.InvalidTraceLevel, level), e);
				Trace.Write(oe);
				throw new TDException(oe.Message, e, oe.PublishedBy.Length != 0, TDExceptionIdentifier.EDLFailedConvertingTDTraceLevel);
			}

			this.time = time;
			this.sessionId = sessionId;
			this.message = message;
			this.machineName = machineName;
			this.typeName = typeName;
			this.methodName = methodName;
			this.assemblyName = assemblyName;
		}

		#endregion

		#region Properties

		/// <summary>
		/// Gets time
		/// </summary>
		public DateTime Time
		{
			get { return time; }
		}

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
		/// Gets message.
		/// </summary>
		public string Message
		{
			get {return message;}
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

		#endregion




	}
}
