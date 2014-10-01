// ***********************************************
// NAME 		: LoggableTask.cs
// AUTHOR 		: Mark Turner
// DATE CREATED : 12/08/2003
// DESCRIPTION 	: Provides a template for all scheduled tasks that need to log to the
// TD Event Logging Service.  It provides a template method run that controls logical 
// sequence of logging and proccessing an import.
// ************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/DataGatewayFramework/LoggableTask.cs-arc  $  
//
//   Rev 1.0   Nov 08 2007 12:20:14   mturner
//Initial revision.
//
//   Rev 1.10   Sep 16 2003 10:56:42   MTurner
//Changed PreTask() from void to int
//
//   Rev 1.9   Sep 09 2003 17:23:34   PScott
//code review changes
//
//   Rev 1.8   Sep 03 2003 17:20:16   MTurner
//Changes to Event Logging
//
//   Rev 1.7   Sep 03 2003 16:36:44   PScott
//work in progress
//
//   Rev 1.6   Sep 01 2003 14:11:38   MTurner
//Changes after comments by A. Caunt
//
//   Rev 1.5   Aug 29 2003 09:40:50   PScott
//work in progress
//
//   Rev 1.4   Aug 27 2003 17:24:12   MTurner
//Changes to remove IDataRow
//
//   Rev 1.3   Aug 26 2003 13:53:26   pscott
//No change.
//
//   Rev 1.2   Aug 15 2003 13:35:40   mturner
//Not fully functional - checked in while on Leave
//
//   Rev 1.1   Aug 13 2003 12:09:34   mturner
//Added event logging methods
//
//   Rev 1.0   Aug 12 2003 16:04:02   mturner
//Initial Revision

using System;
using System.Collections;

namespace TransportDirect.Datagateway.Framework
{
	/// <summary>
	/// Provides a template for all scheduled tasks that need to log to the
	/// TD Event Logging Service.  It also provides a number of 'dummy' methods
	/// that should be overritten by all sub-classes  
	/// </summary>
	public class LoggableTask
	{
		public LoggableTask()
		{
			
		}	
			
		/// <summary>
		/// A template for all scheduled tasks.  run() calls the other methods within this
		/// class in a pre-determined order to control the sequence of logging.
		/// Sub-classes will usually create their own version of this method.
		/// </summary>
		public int Run()
		{
			int result = 0;
			LogStart();
			PreTask();
			result = PerformTask();
			PostTask(result);
			LogFinish(result);
			return result;
		}

		/// <summary>
		/// Basic implemetation of the LogStart event.  Sub classes may create overloaded
		/// instances of this method but all of these call base.LogStart().
		/// </summary>
		protected virtual void LogStart()
		{
			
		}

		/// <summary>
		/// Basic implemetation of the LogFinish event.  Sub classes may create overloaded
		/// instances of this method but all of these call base.LogFinish().
		/// </summary>
		protected virtual void LogFinish(int retCode)
		{
			
		}

		#region Dummy Methods
		/// <summary>
		/// Dummy version of PreTask method that should be hidden by sub-class
		/// </summary>
		protected virtual int PreTask()
		{
			return 0;
		}

		/// <summary>
		/// Dummy version of PostTask method that should be hidden by sub-class
		/// </summary>
		/// <param name="retCode">Status code returned from PerformTask()</param>
		protected virtual void PostTask(int retCode)
		{
			return;
		}

		/// <summary>
		/// Dummy version of PerformTask method that should be hidden by sub-class.
		/// A return code of 0 signifies success.
		/// A return code of 1 indicates that no files could be found to import
		/// Any other value denotes a failure.
		/// </summary>
		protected virtual int PerformTask()
		{
			return 0;
		}

		/// <summary>
		/// Dummy version of SetParameters method that should be hidden by sub-class
		/// </summary>
		/// <param name="table">A Hashtable object containing configuration information</param>
		public void SetParameters(Hashtable table)
		{
			return;
		}
		#endregion
	}
}
