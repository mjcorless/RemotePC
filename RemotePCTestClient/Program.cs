using RemotePC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace RemotePCTestClient
{
	internal class Program
	{
		private const int PORT_NO = 11000;
		private const string SERVER_IP = "DESKTOP-ARF2F5G";

		private static void Main(string[] args)
		{
			//---data to send to the server---
			string route = "YouTubePlayer";
			string function = "https://www.pandora.com/";
			string request = $"{route}{Common.EOLCHAR}{function}{Common.EOLCHAR}{Common.EOTCHAR}";

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
	}
}