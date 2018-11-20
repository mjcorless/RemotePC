using System;
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
			// https://www.codeproject.com/Articles/16197/Remotely-Unlock-a-Windows-Workstation has a
			// hack to do this but it seems like more of a hassle than is worth committing to. I
			// can't think of a valid use case for the need unlock my PC programmatically if I am at
			// my computer I can just use my keyboard to unlock instead
		}

		#endregion methods
	}
}