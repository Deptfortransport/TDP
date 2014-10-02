// *********************************************** 
// NAME                 : EventReceiverPropertyValidator.cs 
// AUTHOR               : Jatinder S. Toor
// DATE CREATED         : 18/08/2003 
// DESCRIPTION  :  Validates properties specifically 
// for this application.
// ************************************************ 
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/ReportDataProvider/EventReceiver/EventReceiverPropertyValidator.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:38:34   mturner
//Initial revision.
//
//   Rev 1.7   Jan 09 2004 15:02:58   geaton
//Removed validation that is not supported for remote computer queues.
//
//   Rev 1.6   Nov 07 2003 08:56:58   geaton
//Updated exception id.
//
//   Rev 1.5   Nov 06 2003 19:54:20   geaton
//Removed redundant key.
//
//   Rev 1.4   Oct 09 2003 12:33:40   geaton
//Tidied up error handling and added verbose messages to assist in debugging.
//
//   Rev 1.3   Oct 08 2003 16:25:36   pscott
//.
//
//   Rev 1.2   Oct 07 2003 12:22:32   PScott
//exception enumerations added
//
//   Rev 1.1   Sep 05 2003 09:49:30   jtoor
//Changes made to comply with Code Review.
//
//   Rev 1.0   Aug 22 2003 11:49:36   jtoor
//Initial Revision

using System;

using System.Collections;
using System.Text.RegularExpressions;
using System.Messaging;
using System.Diagnostics;

using TransportDirect.Common;
using TransportDirect.Common.PropertyService.Properties;
using TransportDirect.Common.Logging;
using Logger = System.Diagnostics.Trace;

namespace TransportDirect.ReportDataProvider.EventReceiver
{
	/// <summary>
	/// Validates specific propeties from the properties.xml file.
	/// </summary>
	public class EventReceiverPropertyValidator : PropertyValidator
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		/// <param name="properties">Properties.</param>
		public EventReceiverPropertyValidator( IPropertyProvider properties ) : base( properties )
		{}

		/// <summary>
		/// Validates the given property.
		/// </summary>
		/// <param name="key">Property name to validate.</param>
		/// <param name="errors">List to append any errors.</param>
		/// <returns>true on success, false on validation failure.</returns>
		public override bool ValidateProperty( string key, ArrayList errors )
		{
			if( key == Keys.ReceiverQueue )
				return ValidateReceiverQueue( errors );				
			else
			{
				throw new TDException(String.Format(Messages.Init_UnknownPropertyKey, key), false, TDExceptionIdentifier.RDPEventReceiverUnknownPropertyKey);
			}

		}

		/// <summary>
		/// Checks that for every space delimited 'ID' entry in the Receiver.Queue property,
		/// there is a corresponding property of type Receiver.Queue.'ID'.Path in the property list.
		/// </summary>
		/// <param name="errors">Error list.</param>
		/// <returns>true on success, false on validation failure.</returns>
		private bool ValidateReceiverQueue( ArrayList errors )
		{
			int errorsBefore = errors.Count;			
					
			if( ValidateExistence( Keys.ReceiverQueue, Optionality.Mandatory, errors ) )
			{

				string[] idList = properties[Keys.ReceiverQueue].Split(' ');	

				if( !(idList.Length == 1 && idList[0].Length == 0) )
				{

					string path = null;			
			
					foreach( string id in idList )
					{
						if( id != " " )
						{							
							path  = string.Format( Keys.ReceiverQueuePath, id );					

							// Go through and validate each property, and build up the error list.
							if( ValidateExistence( path, Optionality.Mandatory, errors ) )
							{						
								ValidateReceiverQueueCanRead( path, errors ); 				
							}
						}
					}
				}
				else
				{
					errors.Add(String.Format(Messages.Validation_NoQueues, properties[Keys.ReceiverQueue], Keys.ReceiverQueue)); 
				}
			}

			return (errorsBefore == errors.Count);
		}

		/// <summary>
		/// Checks that queue exists in MSMQ.
		/// </summary>
		/// <param name="key">Property key.</param>
		/// <param name="errors">Error list.</param>
		/// <returns>true on success, false on validation failure.</returns>
		/// <remarks>
		/// This method should **not** be used to validate queues on remote computers
		/// when running in workgroup mode.
		/// </remarks>
		private bool ValidateReceiverQueuePath( string key, ArrayList errors )
		{
			int errorsBefore = errors.Count;	
			
			string queue = properties[key];

			if( !MessageQueue.Exists( queue ) )					
			{
				errors.Add(String.Format(Messages.Validation_QueueMissing, properties[key], key));
			}						

			return (errorsBefore == errors.Count);
		}		
		

		/// <summary>
		/// Checks that the queue can be read.
		/// </summary>		
		/// <param name="key">Property key.</param>
		/// <param name="errors">Error list.</param>
		/// <returns>true on success, false on validation failure.</returns>
		/// <remarks>
		/// When validating queues on remote computers in workgroup mode,
		/// the queue path must be provided as a Direct Format Name.
		/// </remarks>
		private bool ValidateReceiverQueueCanRead( string key, ArrayList errors )
		{
			int errorsBefore = errors.Count;									
			string queue = properties[key];

			try
			{

				using( MessageQueue mq = new MessageQueue( queue ) )
				{
					if( !mq.CanRead )					
					{
						errors.Add(String.Format(Messages.Validation_QueueNotReadable, properties[key], key));
					}						
				}
			}
			catch( Exception e )
			{
				errors.Add(String.Format(Messages.Validation_TestQueueNotReadable, properties[key], key, e.Message));
			}

			return (errorsBefore == errors.Count);
		}

	}
}
