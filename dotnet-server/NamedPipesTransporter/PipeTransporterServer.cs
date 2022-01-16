using GameEntities;
using System;
using System.IO.Pipes;

namespace NamedPipesTransporter
{
	public class PipeTransporterServer : BasePipeTransporter
	{
		public PipeTransporterServer(BaseSerializer serializer) : base(serializer)
		{
			var server = new NamedPipeServerStream(
			  "ppoc_pipe",
			  PipeDirection.InOut,
			  1,
			  PipeTransmissionMode.Byte,
			  PipeOptions.Asynchronous,
			  _buffer.Length,
			  _buffer.Length
			);

			_pipeStream = server;
			Console.WriteLine("Servidor escutando em ./ppoc_pipe...");
			server.BeginWaitForConnection(OnConnected, null);
			BeginRead();
		}

		private void OnConnected(IAsyncResult ar)
		{
			((NamedPipeServerStream)_pipeStream!).EndWaitForConnection(ar);
			Console.WriteLine("Client conectado.");
		}
	}
}
