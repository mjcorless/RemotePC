using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace RemotePC.Models
{
	/// <summary>
	/// Adjusts the system volume that the service is running on.
	/// </summary>
	// TODO: can this be done without requiring forms or using a nuget package?
	public class VolumeChanger : Form
	{
		private const int APPCOMMAND_VOLUME_MUTE = 0x80000;
		private const int APPCOMMAND_VOLUME_UP = 0xA0000;
		private const int APPCOMMAND_VOLUME_DOWN = 0x90000;
		private const int WM_APPCOMMAND = 0x319;

		[DllImport("user32.dll")]
		public static extern IntPtr SendMessageW(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

		/// <summary>
		/// Mute the system sound
		/// </summary>
		public void Mute()
		{
			SendMessageW(this.Handle, WM_APPCOMMAND, this.Handle, (IntPtr)APPCOMMAND_VOLUME_MUTE);
		}

		/// <summary>
		/// Lowers the system volume
		/// </summary>
		public void VolumeDown()
		{
			SendMessageW(this.Handle, WM_APPCOMMAND, this.Handle, (IntPtr)APPCOMMAND_VOLUME_DOWN);
		}

		/// <summary>
		/// Increases System Volume
		/// </summary>
		public void VolomeUp()
		{
			SendMessageW(this.Handle, WM_APPCOMMAND, this.Handle, (IntPtr)APPCOMMAND_VOLUME_UP);
		}
	}
}