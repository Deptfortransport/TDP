//********************************************************************************
//NAME         : BusinessObject.cs
//AUTHOR       : SchlumbergerSema
//DATE CREATED : 10/05/2003
//DESCRIPTION  : Used by clients to perform retail pricing processing.
//DESIGN DOC   : DD034 Retail Pricing
//********************************************************************************
//$Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/RetailBusinessObjects/BusinessObject.cs-arc  $
//
//   Rev 1.0   Nov 08 2007 12:46:02   mturner
//Initial revision.
//
//   Rev 1.8   Apr 20 2005 10:32:12   RPhilpott
//Add more thorough and robust error handling.
//Resolution for 2247: PT: error handling by Retail Business Objects
//
//   Rev 1.7   Oct 29 2003 19:47:36   geaton
//Added comments.
//
//   Rev 1.6   Oct 28 2003 20:04:46   geaton
//Changes to support housekeeping and timeout functionality.
//
//   Rev 1.5   Oct 22 2003 09:19:10   geaton
//Added housekeeping support to business objects.
//
//   Rev 1.4   Oct 21 2003 15:22:44   geaton
//Changes to support business object timeout functionality.
//
//   Rev 1.3   Oct 17 2003 10:07:28   CHosegood
//Added Trace
//
//   Rev 1.2   Oct 16 2003 13:59:14   CHosegood
//Removed Console.WriteLine statements
//
//   Rev 1.1   Oct 14 2003 12:27:52   CHosegood
//Intermediate version.  Not ready for build/test
//
//   Rev 1.0   Oct 13 2003 15:25:38   CHosegood
//Initial Revision

using System;
using System.Diagnostics;

using TransportDirect.Common;
using TransportDirect.Common.Logging;

namespace TransportDirect.UserPortal.RetailBusinessObjects
{
	/// <summary>
	/// Class used to provide access to retail business object functionality.
	/// </summary>
	public class BusinessObject : IDisposable
	{
		/// <summary>
		/// Engine used by business object to service process requests.
		/// This is not directly accessible by clients of the business object.
		/// Access to engine is provided through the engine id property only.
		/// </summary>
		private Engine engine;

		/// <summary>
		/// Gets the unique identifier of the business objects engine.
		/// </summary>
		public int EngineId
		{
			get {return this.engine.Id;}
		}

		/// <summary>
		/// True if instance has been disposed.
		/// </summary>
		private bool disposed;

		/// <summary>
		/// Gets the allocation identifier of the engine initially 
		/// provided to business object.
		/// This is used to ensure business objects do not use timed out engines.
		/// </summary>
		private EngineAllocationIdentifier engineAllocationId;
		public EngineAllocationIdentifier EngineAllocationId
		{
			get{return engineAllocationId;}
		}

        /// <summary>
        /// Class constructor. 
        /// Used by pool class to provide a business object to a client.
        /// </summary>
		/// <param name="engine">
		/// Engine to be used by business object.
		/// </param>
		/// <remarks>
		/// This constructor should not be used directly by clients.
		/// Clients must request business objects via the pool class.
		/// </remarks>
        public BusinessObject(Engine engine)
		{
            this.engine = engine;
			this.disposed = false;

			// Copy engine allocation id. 
			// This is used to prevent the business object using the engine if it 
			// has timed out (and has subsequently been allocated to another business object).
			engineAllocationId = (EngineAllocationIdentifier)engine.AllocationId.Clone();
		}

	
        /// <summary>
        /// Uses the business object to perform generic processing specified by input.
        /// </summary>
        /// <param name="inputHeader">Proccess specification.</param>
        /// <returns>Process output.</returns>
        /// <exception cref="TDException">
        /// Failure when calling the business object's engine.
        /// This could be caused by a timed out engine, invalid input, 
        /// or a corrupt or missing engine.
		/// </exception>
        public BusinessObjectOutput Process(BusinessObjectInput input) 
        {
			BusinessObjectOutput output = null;

			if (!this.engineAllocationId.Equals(this.engine.AllocationId))
			{
				Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, String.Format(Messages.BusinessObject_EngineTimedOut, this.engineAllocationId.ToString(), this.engine.AllocationId.ToString())));
				throw new TDException(String.Format(Messages.BusinessObject_EngineTimedOut, this.engineAllocationId.ToString(), this.engine.AllocationId.ToString()), true, TDExceptionIdentifier.PRHBusinessObjectEngineTimedOut);
			}
			else
			{
				output = this.engine.Run(input);  // Allow all exceptions to filter up to caller.
			}

			string severity = output.ErrorSeverity;			

			if (severity.Equals(Messages.ErrorSeverityCritical) || severity.Equals(Messages.ErrorSeverityError))
			{
				string errorText = this.engine.GetErrorMessage(output.ErrorCode);
				Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Error, String.Format(Messages.BusinessObject_Error, input.ObjectID, output.ErrorCode, severity, errorText)));
			}
			else if (TDTraceSwitch.TraceWarning)
			{
				if	(severity.Equals(Messages.ErrorSeverityInformation) || severity.Equals(Messages.ErrorSeverityWarning))
				{
					string errorText = this.engine.GetErrorMessage(output.ErrorCode);
					Trace.Write(new OperationalEvent(TDEventCategory.Infrastructure, TDTraceLevel.Warning, String.Format(Messages.BusinessObject_Warning, input.ObjectID, output.ErrorCode, severity, errorText)));
				}
			}

			return output;
        }

		/// <summary>
		/// Dispose method for business objects.
		/// </summary>
		/// <param name="disposing">
		/// True when Dispose called by clients.
		/// False when Dispose called by runtime.
		/// </param>
		public void Dispose(bool disposing)
		{
			// Check to see if Dispose has already been called.
			if(!this.disposed)
			{
				if (disposing)
				{
					// Dispose of any managed resources.
				}
             
				// Dispose of any unmanaged resources.			
			}

			this.disposed = true;
		}

	
		/// <summary>
		/// Disposes of business objects resources.
		/// Allows clients to dispose of resources.
		/// </summary>
		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this); // take off finalization queue to prevent dispose being called again.
		}

		/// <summary>
		/// Class destructor.
		/// </summary>
		~BusinessObject()      
		{
			Dispose(false);
		}
	}
}