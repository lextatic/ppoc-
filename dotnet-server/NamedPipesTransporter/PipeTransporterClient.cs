using GameEntities;
using System;
using System.IO.Pipes;
using System.Security.Principal;

namespace NamedPipesTransporter
{
	public class PipeTransporterClient : BasePipeTransporter
	{
		public PipeTransporterClient(BaseSerializer serializer) : base(serializer)
		{
			var client = new NamedPipeClientStream(
			  ".",
			  "ppoc_pipe",
			  PipeDirection.InOut,
			  PipeOptions.Asynchronous,
			  TokenImpersonationLevel.Impersonation
			);

			_pipeStream = client;
			Console.WriteLine("Conectando a ./ppoc_pipe...");
			client.Connect();
			BeginRead();
		}
	}
}
