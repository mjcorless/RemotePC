using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemotePC.Models
{
	/// <summary>
	/// Dummy class to encapsulate an RPC response
	/// </summary>
	internal class RPCResponse<TResult>
	{
		#region properties

		public TResult Result { get; set; }

		public bool Success { get; set; }

		#endregion properties
	}
}