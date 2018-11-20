using RemotePC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace RemotePCTestClient
{
	/// <summary>
	/// Simple Console program to test the current RPCs before the mobile client is built
	///
	/// This client does not allow for some integration tests like locking the PC and then launching
	/// chrome while locked.
	/// </summary>
	internal class Program
	{
		private const int PORT_NO = 11000;
		private const string SERVER_IP = "DESKTOP-ARF2F5G";

		private static void Main(string[] args)
		{
			Console.Clear();

			//---data to send to the server---
			string request = GetRPCRequest();

			//---create a TCPClient object at the IP and port no.---
			TcpClient client = new TcpClient(SERVER_IP, PORT_NO);
			NetworkStream nwStream = client.GetStream();
			byte[] bytesToSend = ASCIIEncoding.Unicode.GetBytes(request);

			//---send the text---
			Console.WriteLine("Sending : " + request);
			nwStream.Write(bytesToSend, 0, bytesToSend.Length);

			//---read back the text---
			byte[] bytesToRead = new byte[client.ReceiveBufferSize];
			int bytesRead = nwStream.Read(bytesToRead, 0, client.ReceiveBufferSize);
			Console.WriteLine("Received : " + Encoding.Unicode.GetString(bytesToRead, 0, bytesRead));
			client.Close();
			Console.ReadLine();
		}

		private static string GetRPCRequest()
		{
			string route;
			string method;
			int input = 0;
			while (input <= 0 || input > 4)
			{
				Console.WriteLine("Please enter a number for one of the associated functionalities:");
				Console.WriteLine("1. Volume");
				Console.WriteLine("2. Brightness");
				Console.WriteLine("3. PCLocker");
				Console.WriteLine("4. YouTubePlayer");

				int.TryParse(Console.ReadLine(), out input);
			}

			switch (input)
			{
				case 1:
					route = "Volume";
					method = GetVolumeMethod();
					break;

				case 2:
					route = "Brightness";
					Console.Write("Enter Brightness level (0-255):");
					method = Console.ReadLine();
					break;

				case 3:
					route = "PCLocker";
					method = "Lock";
					// UnLock is currently not implemented
					break;

				case 4:
					route = "YouTubePlayer";
					Console.Write("Enter URL:");
					method = Console.ReadLine();
					break;

				default:
					throw new Exception("How did we get here?");
			}

			return $"{route}{Common.EOLCHAR}{method}{Common.EOLCHAR}{Common.EOTCHAR}";
		}

		private static string GetVolumeMethod()
		{
			string method;
			int input = 0;
			while (input <= 0 || input > 3)
			{
				Console.WriteLine("Please enter a number for one of the associated methods:");
				Console.WriteLine("1. Mute");
				Console.WriteLine("2. VolumeDown");
				Console.WriteLine("3. VolumeUp");

				int.TryParse(Console.ReadLine(), out input);
			}

			switch (input)
			{
				case 1:
					method = "Mute";
					break;

				case 2:
					method = "VolumeDown";
					break;

				case 3:
					method = "VolumeUp";
					break;

				default:
					throw new Exception("How did we get here?");
			}

			return method;
		}
	}
}