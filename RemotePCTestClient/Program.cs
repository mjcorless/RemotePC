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
			string textToSend = $"DateTime.Now.ToString() <EOF>";

			//---create a TCPClient object at the IP and port no.---
			TcpClient client = new TcpClient(SERVER_IP, PORT_NO);
			NetworkStream nwStream = client.GetStream();
			byte[] bytesToSend = ASCIIEncoding.ASCII.GetBytes(textToSend);

			//---send the text---
			Console.WriteLine("Sending : " + textToSend);
			nwStream.Write(bytesToSend, 0, bytesToSend.Length);

			//---read back the text---
			byte[] bytesToRead = new byte[client.ReceiveBufferSize];
			int bytesRead = nwStream.Read(bytesToRead, 0, client.ReceiveBufferSize);
			Console.WriteLine("Received : " + Encoding.ASCII.GetString(bytesToRead, 0, bytesRead));
			Console.ReadLine();
			client.Close();
		}
	}
}