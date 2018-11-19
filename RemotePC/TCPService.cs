using RemotePC.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace RemotePC
{
	public class TCPService : ServiceBase
	{
		public TCPService()
		{
			//InitializeComponent();
		}

		protected override void OnStart(string[] args)
		{
			AsynchronousSocketListener.StartListening();
		}

		protected override void OnStop()
		{
		}
	}
}