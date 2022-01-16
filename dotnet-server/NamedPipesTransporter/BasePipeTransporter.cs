using GameEntities;
using System;
using System.IO.Pipes;
using System.Threading;

namespace NamedPipesTransporter
{
	/// <summary>
	/// Transporter via named-pipes (processos diferentes, mesma máquina)
	/// </summary>
	public class BasePipeTransporter : BaseTransporter
	{
		protected PipeStream? _pipeStream;
		protected readonly byte[] _buffer = new byte[65536];

		public BasePipeTransporter(BaseSerializer serializer) : base(serializer)
		{
		}

		protected override void OnSend(byte[] serializedMessage)
		{
			Console.Write($"Enviado: {serializedMessage.Length:N0} bytes...");
			_pipeStream!.Write(serializedMessage);
			Console.WriteLine(" DONE");
		}

		public override void Dispose()
		{
			_pipeStream!.Close();
			base.Dispose();
			GC.SuppressFinalize(this);
		}

		protected void BeginRead()
		{
			SpinWait.SpinUntil(() => _pipeStream!.IsConnected);
			_pipeStream!.BeginRead(_buffer, 0, _buffer.Length, OnPipeRead, null);
		}

		private void OnPipeRead(IAsyncResult ar)
		{
			Console.Write("Dados chagando...");

			var bytesRead = _pipeStream!.EndRead(ar);
			// TODO: Idealmente aqui, trabalhar com ZeroCopy (talvez ArraySegment. shared memory ou ponteiros):
			var serializedMessage = new byte[bytesRead];

			BeginRead();

			// TODO: BlockCopy bad!
			Buffer.BlockCopy(_buffer, 0, serializedMessage, 0, bytesRead);

			Received(serializedMessage);
			Console.WriteLine($"{(char)8}{(char)8} Recebido: {bytesRead:N0} bytes");
		}
	}
}
