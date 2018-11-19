using RemotePC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace RemotePC
{
	internal static class Application
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		private static int Main()
		{
			// TODO: is there a convenient way to debug services or continue to use console for debug?
#if DEBUG
			AsynchronousSocketListener.StartListening();
			Console.Read();
#endif
			ServiceBase[] ServicesToRun = new ServiceBase[]
			{
			new TCPService()
			};
			ServiceBase.Run(ServicesToRun);

			return 1;
		}
	}
}