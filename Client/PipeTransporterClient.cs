﻿using PoCAbstractions.Serialization;
using PoCAbstractions.Transport;
using System.IO.Pipes;
using System.Security.Principal;

namespace Client {
  public class PipeTransporterClient : BaseTransporter {
    public PipeTransporterClient(BaseSerializer serializer) : base(serializer) {
      _pipeClient = new(".", "ppoc_pipe", PipeDirection.InOut, PipeOptions.None, TokenImpersonationLevel.Impersonation);
      Console.WriteLine("Conectando a ./ppoc_pipe...");
      _pipeClient.Connect();
      BeginRead();
    }

    private readonly NamedPipeClientStream _pipeClient;
    private readonly byte[] _buffer = new byte[65536];

    protected override void OnSend(byte[] serializedMessage) {
      _pipeClient.Write(serializedMessage);
    }

    public override void Dispose() {
      _pipeClient.Close();
      base.Dispose();
      GC.SuppressFinalize(this);
    }

    private void BeginRead() {
      if (_pipeClient.IsConnected == false) {
        return;
      }

      _pipeClient.BeginRead(_buffer, 0, _buffer.Length, OnPipeRead, null);
    }

    private void OnPipeRead(IAsyncResult ar) {
      var bytesRead = _pipeClient.EndRead(ar);
      var serializedMessage = new ArraySegment<byte>(_buffer, 0, bytesRead);

      BeginRead();
      Received(serializedMessage.Array!);
    }
  }
}
