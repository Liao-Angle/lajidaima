using System;
using System.Collections.Generic;
using System.ServiceProcess;
using System.Text;

namespace ECPJobQueue
{
    static class Program
    {
        /// <summary>
        /// 應用程式的主要進入點。
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[] 
			{ 
				new RunKernal() 
			};
            ServiceBase.Run(ServicesToRun);
        }
    }
}
