using PersonalConvenience.Models;
using RemotePC.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemotePC.Controllers
{
	internal class RouteController
	{
		#region properties

		/// <summary>
		/// Caches the request
		/// </summary>
		private string Request { get; set; }

		/// <summary>
		/// Caches the current location in the request string
		/// </summary>
		private int CurrentLocation { get; set; }

		/// <summary>
		/// Holds the response from the RPC
		/// </summary>
		private string Response { get; set; }

		/// <summary>
		/// Flag if operation succeeded or not.
		/// </summary>
		private bool Success { get; set; }

		#endregion properties

		#region constructors/deconstructors

		public RouteController(string request)
		{
			Request = request;
		}

		#endregion constructors/deconstructors

		#region methods

		/// <summary>
		/// Routes the incoming request to the proper function
		/// </summary>
		public void HandleRPC()
		{
			switch (GetNextLine())
			{
				case "Volume":
					HandleVolumeRPC();
					break;

				case "Brightness":
					HandleBrightnessRPC();
					break;

				case "PCLocker":
					HandlePCLockerRPC();
					break;

				case "YouTubePlayer":
					HandleYouTubePlayerRPC();
					break;

				default:
					Response = $"Route could not be found";
					break;
			}
		}

		/// <summary>
		/// Gets the next line from the request
		/// </summary>
		/// <returns>Next line (lines are delimited by Common.EOLCHAR</returns>
		private string GetNextLine()
		{
			int endLocation = Request.IndexOf(Common.EOLCHAR, CurrentLocation);
			string line = Request.Substring(CurrentLocation, endLocation - CurrentLocation);
			CurrentLocation = endLocation + 1;
			return line;
		}

		/// <summary>
		/// Handles RPCs to adjust system volume
		/// </summary>
		private void HandleVolumeRPC()
		{
			VolumeChanger volumeChanger = new VolumeChanger();

			switch (GetNextLine())
			{
				case "Mute":
					volumeChanger.Mute();
					break;

				case "VolumeDown":
					volumeChanger.VolumeDown();
					break;

				case "VolumeUp":
					volumeChanger.VolomeUp();
					break;

				default:
					Response = $"Method could not be found";
					break;
			}
		}

		/// <summary>
		/// Handles RPCs to adjust system brightness
		/// </summary>
		private void HandleBrightnessRPC()
		{
			if (short.TryParse(GetNextLine(), out short brightness))
			{
				BrightnessAdjuster brightnessAdjuster = BrightnessAdjuster.Instance;
				brightnessAdjuster.SetBrightness(brightness);
			}
			else
			{
				Response = "Could not parse brightness.";
			}
		}

		/// <summary>
		/// Handles RPCs to lock/unlock the PC
		/// </summary>
		private void HandlePCLockerRPC()
		{
			switch (GetNextLine())
			{
				case "Lock":
					PCLocker.LockWorkStation();
					break;

				case "UnLock":
					new PCLocker().UnLockWorkStation();
					break;

				default:
					Response = $"Method could not be found";
					break;
			}
		}

		/// <summary>
		/// Handles RPCs to play youtube
		/// </summary>
		private void HandleYouTubePlayerRPC()
		{
			Process.Start("chrome.exe", GetNextLine());
		}

		#endregion methods
	}
}