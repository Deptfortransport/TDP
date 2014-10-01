// *********************************************** 
// NAME                 : ConsolePublisher.cs 
// AUTHOR               : Kenny Cheung/Gary Eaton
// DATE CREATED         : 08/07/2003 
// DESCRIPTION  : A publisher that publishes
// events to the console.
// ************************************************ 
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/EventLoggingService/ConsolePublisher.cs-arc  $ 
//
//   Rev 1.0   Nov 08 2007 12:22:58   mturner
//Initial revision.
//
//   Rev 1.4   Oct 29 2003 20:20:40   geaton
//Reduced type checking by using formatter directly.
//
//   Rev 1.3   Oct 07 2003 13:40:32   geaton
//Updates following introduction of TDExceptionIdentifier.
//
//   Rev 1.2   Jul 25 2003 14:14:26   geaton
//Changes resulting from code review 2003-07-22. (These were mainly concerned with adding comments and running through FXCop).
//
//   Rev 1.1   Jul 24 2003 18:27:26   geaton
//Added/updated comments

using System;

namespace TransportDirect.Common.Logging
{
	/// <summary>
	/// Publishes events to the console.
	/// </summary>
	public class ConsolePublisher : IEventPublisher
	{
		private string identifier;
		private string streamSetting;
		
		/// <summary>
		/// Gets the identifier of the ConsolePublisher.
		/// </summary>
		public string Identifier
		{
			get {return identifier;}
		}

		/// <summary>
		/// Create a publisher that sends event details to the console.
		/// It is assumed that all parameters have been pre-validated.
		/// </summary>
		/// <param name="identifier">Unique identifier for console publishers.</param>
		/// <param name="streamSetting">Takes values 'Error' or 'Out' to indicate type of console stream to publish to.</param>
		public ConsolePublisher(string identifier, string streamSetting)
		{
			this.identifier = identifier;
			this.streamSetting = streamSetting;
		}

		/// <summary>
		/// Writes a log event to the console.
		/// </summary>
		/// <param name="logEvent">Log Event to write details for.</param>
		public void WriteEvent(LogEvent logEvent)
		{
			try
			{
				string formatString = logEvent.ConsoleFormatter.AsString(logEvent);

				if(streamSetting == Keys.ConsolePublisherOutputStream)
				{
					Console.Out.WriteLine(formatString);
				}
				else if(streamSetting == Keys.ConsolePublisherErrorStream)
				{
					Console.Error.WriteLine(formatString);
				}
			}
			catch(System.IO.IOException ioe)
			{
				String message = Messages.ConsolePublisherWriteEvent;
				throw new TDException(message, ioe, false, TDExceptionIdentifier.ELSConsolePublisherWritingEvent);
			}
			catch(Exception e)
			{
				String message = Messages.ConsolePublisherWriteEvent;
				throw new TDException(message, e, false, TDExceptionIdentifier.ELSConsolePublisherWritingEvent);
			}
			
		}
	}
}
