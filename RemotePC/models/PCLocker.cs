using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace RemotePC.Models
{
	internal class PCLocker
	{
		#region methods

		/// <summary>
		/// Locks the PC
		/// </summary>
		/// <returns>True if successfully locked PC</returns>
		[DllImport("user32.dll")]
		public static extern bool LockWorkStation();

		/// <summary>
		/// Unlocks the PC
		/// </summary>
		/// <returns>True if successfully unlocked</returns>
		public bool UnLockWorkStation()
		{
			throw new NotImplementedException("Will implement when I figure out how to cache my credentials securely.");
		}

		#endregion methods
	}
}