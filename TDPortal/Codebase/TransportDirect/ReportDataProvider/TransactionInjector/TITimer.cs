using System;
using System.Threading;

namespace TransportDirect.ReportDataProvider.TransactionInjector
{
	/// <summary>
	/// Summary description for TITimer.
	/// </summary>
	public class TITimer
	{
		public TITimer()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		private TDTransaction transaction = null;
		public TDTransaction Transaction
		{
			get{ return transaction; }
			set{ transaction = value; }
		}

		private int frequency = 60;
		public int Frequency
		{
			get { return frequency; }
			set { frequency = value; }
		}

		public void Start()
		{
			new Thread(new ThreadStart( Run ) ).Start();
		}

		private bool running = false;
		public void Stop()
		{
			running = false;
		}

		public void Run()
		{
			running = true;

			while( running )
			{
				DateTime start = DateTime.Now;
				transaction.ExecuteTransaction();
				DateTime stop = DateTime.Now;
				TimeSpan ts = stop - start;
				Thread.Sleep( Math.Max(0,  Frequency * 1000 - (int)ts.TotalMilliseconds ) );

			}
		}
	}
}
