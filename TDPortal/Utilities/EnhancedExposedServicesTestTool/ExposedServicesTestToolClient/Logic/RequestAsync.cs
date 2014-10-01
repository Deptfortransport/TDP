// ***********************************************************************************
// NAME 		: SoapRequest
// AUTHOR 		: Manuel Dambrine
// DATE CREATED : 07-Dec-2005
// DESCRIPTION 	: class for sending SoapRequests asynchorous
// ************************************************************************************#
// $Log:   P:/TDPortal/archives/DotNet2.0Codebase/TransportDirect/Stubs/ExposedServicesTestTool/ExposedServicesTestToolClient/Logic/RequestAsync.cs-arc  $
//
//   Rev 1.1   Apr 17 2009 14:10:36   mmodi
//Added null check for timer
//
//   Rev 1.0   Nov 08 2007 12:49:14   mturner
//Initial revision.
//
//   Rev 1.4   Feb 07 2006 10:18:46   mdambrine
//added pvcs log

using System;
using System.Collections;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;

using System.Timers;

namespace ExposedServicesTestToolClient
{
	/// <summary>
	/// class for sending SoapRequests asynchorous
	/// </summary>
	public class RequestAsync : IDisposable
	{ 
		#region declarations

		protected delegate void AsyncDelegate(System.Web.HttpContext Context);

		private SoapRequest[] requests;
		private int timeSpan;
		private int quantity;	
	
		private int requestsLeft;
		private int requestsTotal;	
	
		private bool isFinished;

		private Timer timer ;

		private System.Web.HttpContext context;

		#endregion

		#region constructors
		/// <summary>
		/// creates a asycronious request caller object
		/// </summary>
		/// <param name="requests">number of requests it need to handle</param>
		/// <param name="timeSpan">timespan between each request call</param>
		/// <param name="quantity">number of request to be handled</param>
		public RequestAsync(SoapRequest[] requests, 
							int timeSpan,
							int quantity)
		{
			this.requests = requests;
			this.timeSpan = timeSpan;
			this.quantity = quantity;
		}

		public RequestAsync()
		{
			
		}
		#endregion

		#region properties
		public int TimeSpan
		{
			get{ return timeSpan;}
			set{ timeSpan = value * 1000;} //milliseconds
		}

		public int Quantity
		{
			get{ return quantity;}
			set{ quantity = value;}
		}

		public SoapRequest[] Requests
		{			
			get{ return requests;}
			set{ requests = value;}
		}
	
		/// <summary>
		/// Read only property for determing if all requests have been send
		/// </summary>
		public int RequestsLeft
		{
			get{ return requestsLeft;}			
		}

		/// <summary>
		/// property for determing if all requests have been send
		/// </summary>
		public bool IsFinished
		{
			get{ return isFinished;}
			set{ isFinished = value;}		
		}
		#endregion

		#region public methods
		/// <summary>
		/// Sends all requests in a seperate thread.
		/// </summary>
		/// <param name="context">the context the application is running in</param>
		public void SendRequests(System.Web.HttpContext context)
		{			
			//calculate the number of request to send 
			requestsTotal = quantity * (requests.Length);
			requestsLeft = requestsTotal;

			RequestAsync.AsyncDelegate doStuffAsync = new RequestAsync.AsyncDelegate(WorkerFunction);
			AsyncCallback callBack = new AsyncCallback(CallbackResults);
			Object state = new object();

			//IAsyncResult ar = doStuffAsync.BeginInvoke(context, callBack, state);			
			IAsyncResult ar = doStuffAsync.BeginInvoke(context, callBack, state);			
		}

		/// <summary>
		/// abort the asynchronous request caller
		/// </summary>
		public void AbortProcess()
		{
			requestsLeft = 0;
			isFinished = true;
			if (timer != null)
			{
				timer.Stop();
				timer.Close();
			}
		}

		/// <summary>
		/// Types that own disposable fields should be disposable (timer)
		/// </summary>
		public void Dispose()
		{
			timer.Close();			
		}
		#endregion

		#region private methods
		/// <summary>
		/// catches all the results when the asynchorous process is finished
		/// </summary>
		/// <param name="ar">the results</param>
		[OneWay()]
		protected void CallbackResults(IAsyncResult ar)
		{
			AsyncDelegate asyncDelegate = (AsyncDelegate) ((AsyncResult) ar).AsyncDelegate;					
		}

		/// <summary>
		/// This function will send each request in the array, depending on the timespan 
		/// and the quantity given by the user
		/// </summary>
		/// <param name="context">the context the application is running in</param>
		private void WorkerFunction(System.Web.HttpContext context)
		{		
			this.context = context;

			if (requestsTotal > 1 && timeSpan != 0)
			{
				timer = new Timer();
				timer.Elapsed += new ElapsedEventHandler(OnTimedEvent);

				timer.Interval = timeSpan;
				timer.Enabled = true;	
			
				timer.Start();
		
				//now Call the first in the queue
				OnTimedEvent(null, null);
			}
			else
			{
				//when there's only one request and no interval just call the timed event 
				//the number of times there are requests, no need to create a timer
				for(int i = 0; i < requestsTotal; i++)
					OnTimedEvent(null, null);
			}
		}

		/// <summary>
		/// this method handles the ontimedevent of the timer
		/// </summary>
		/// <param name="source"></param>
		/// <param name="e"></param>
		private void OnTimedEvent(object source, ElapsedEventArgs e)
		{					
			//get the number of the request to send
			int RequestNumber = (requestsTotal - requestsLeft) % requests.Length;

			SoapRequest soapRequest = requests[RequestNumber];

			if (requestsLeft != 1)
			{				
				//call the request in a new tread
				//We call each request in a separate tread, so we will call the same RequestAsync class again. 
				//That way the interval will be between each request and not between last response and new request.

				//create an array with one request
				SoapRequest[] soapRequests = new SoapRequest[1];
				soapRequests[0] = soapRequest;

				//send the request async
				RequestAsync requestAsync = new RequestAsync(soapRequests, 0, 1);
				requestAsync.SendRequests(context);	
		
				requestsLeft -= 1;												
				
			}
			else
			{			
					
				//if this is the last request call the invoke on the webservice
				soapRequest.Invoke(context);				
								
				requestsLeft -= 1;
				isFinished = true;

				//No request left, stop the timer
                if (timer != null)
                {
                    timer.Stop();
                    timer.Close();
                }
								
			}

		

		}
		
		#endregion


		
	}
}
