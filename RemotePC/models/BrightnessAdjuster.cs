using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;

namespace PersonalConvenience.Models
{
	/// <summary>
	/// This class adjusts the brightness of the system. It does not work well and requires unsafe code.
	/// </summary>
	[Obsolete("BrightnessAdjuster requires unsafe code and does not work well. Lock PC instead")]
	public class BrightnessAdjuster
	{
		[DllImport("gdi32.dll")]
		private unsafe static extern bool SetDeviceGammaRamp(Int32 hdc, void* ramp);

		private static int _hdc;
		private static short _brightness = 90; // 90 is where I typically like it set

		private static BrightnessAdjuster instance = null;
		private static readonly object _lock = new object();

		#region Properties

		/// <summary>
		/// Gets/Sets the brightness for the intance
		/// </summary>
		public short Brightness
		{
			get
			{
				return _brightness;
			}
			set
			{
				if (value > 255)
				{
					value = 255;
				}

				if (value < 0)
				{
					value = 0;
				}

				_brightness = value;
			}
		}

		/// <summary>
		/// Instance of BrightnessAdjuster class
		/// </summary>
		public static BrightnessAdjuster Instance
		{
			get
			{
				lock (_lock)
				{
					if (instance == null)
					{
						instance = new BrightnessAdjuster();
					}
					return instance;
				}
			}
		}

		#endregion Properties

		#region constructors

		private BrightnessAdjuster()
		{
			//Get the hardware device context of the screen, we can do
			//this by getting the graphics object of null (IntPtr.Zero)
			//then getting the HDC and converting that to an Int32.
			_hdc = Graphics.FromHwnd(IntPtr.Zero).GetHdc().ToInt32();
		}

		#endregion constructors

		#region methods

		public unsafe bool SetBrightness(short brightLevel)
		{
			Brightness = brightLevel;

			short* gArray = stackalloc short[3 * 256];
			short* idx = gArray;

			for (int j = 0; j < 3; j++)
			{
				for (int i = 0; i < 256; i++)
				{
					int arrayVal = i * (Brightness + 128);

					if (arrayVal > 65535)
					{
						arrayVal = 65535;
					}

					*idx = (short)arrayVal;
					idx++;
				}
			}

			//For some reason, this always returns false?
			bool retVal = SetDeviceGammaRamp(_hdc, gArray);

			//Memory allocated through stackalloc is automatically free'd
			//by the CLR.

			return retVal;
		}

		#endregion methods
	}
}