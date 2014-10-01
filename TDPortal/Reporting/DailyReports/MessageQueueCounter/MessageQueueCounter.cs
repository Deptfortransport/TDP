using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Messaging;
using System.Diagnostics;


namespace MessageQueueCounter
{
   
    public class MsgCounter
    {
        private static int Main(string[] args)
        {
            int num1 = -99;
            int num2;
            try
            {
                string str = ".";
                string str2 = "\\private$\\tdprimaryqueue";
                num1 = -99;
                if (args != null && args.Length > 0)
                {
                    str = args[0];
                    if (args.Length > 1)
                    {
                        str2 = args[1];
                    }
                }
                PerformanceCounter performanceCounter = new PerformanceCounter();
                performanceCounter.CategoryName = "MSMQ Queue";
                performanceCounter.CounterName = "Messages in Queue";
                performanceCounter.MachineName = str;
                performanceCounter.InstanceName = str + str2;
                num2 = (int)performanceCounter.NextValue();
                Console.WriteLine("Message Queue lines :  "+str + str2+" = "+ num2.ToString());
                performanceCounter.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                num2 = -1;
                Console.WriteLine("Message Queue lines :  "  + num2.ToString());
                
            }
            return num2;
        }
    }

}
