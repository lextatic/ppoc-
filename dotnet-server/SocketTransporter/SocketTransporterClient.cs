using GameEntities;
using System;
using System.Net;
using System.Net.Sockets;

namespace SocketTransporter
{
	public class SocketTransporterClient : BaseSocketTransporter
	{
		System.Threading.Thread SocketThread;
		UdpClient client;

		public SocketTransporterClient(BaseSerializer serializer) : base(serializer)
		{
			Connect();
		}

		private void Connect()
		{
			SocketThread = new System.Threading.Thread(networkCode);
			SocketThread.IsBackground = true;
			SocketThread.Start();
		}

		void networkCode()
		{
			client = new UdpClient();
			IPEndPoint ep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 11000); // endpoint where server is listening
			client.Connect(ep);

			// then receive data
			while (true)
			{
				var receivedData = client.Receive(ref ep);
				Console.Write("receive data from " + ep.ToString());

				Received(receivedData);
			}
		}

		protected override void OnSend(byte[] serializedMessage)
		{
			client.Send(serializedMessage, serializedMessage.Length);
		}
	}
}
