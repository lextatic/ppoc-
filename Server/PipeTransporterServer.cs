using PoCAbstractions.Serialization;
using PoCAbstractions.Transport;
using System.IO.Pipes;

namespace Server {
  public class PipeTransporterServer : BaseTransporter {
    public PipeTransporterServer(BaseSerializer serializer) : base(serializer) {
      _pipeServer = new("ppoc_pipe", PipeDirection.InOut, 1, PipeTransmissionMode.Byte, PipeOptions.Asynchronous, 65536, 65536);
      Console.WriteLine("Servidor escutando em ./ppoc_pipe...");
      _pipeServer.BeginWaitForConnection(OnConnected, null);
      BeginRead();
    }

    private readonly NamedPipeServerStream _pipeServer;
    private readonly byte[] _buffer = new byte[65536];

    public override void Dispose() {
      _pipeServer.Close();
      base.Dispose();
      GC.SuppressFinalize(this);
    }

    protected override void OnSend(byte[] serializedMessage) {
      Console.Write($"Enviado: {serializedMessage.Length:N0} bytes...");
      _pipeServer.Write(serializedMessage);
      Console.WriteLine(" DONE");
    }

    private void OnConnected(IAsyncResult ar) {
      _pipeServer.EndWaitForConnection(ar);
      Console.WriteLine("Client conectado.");
    }

    private void BeginRead() {
      SpinWait.SpinUntil(() => _pipeServer.IsConnected);
      _pipeServer.BeginRead(_buffer, 0, _buffer.Length, OnPipeRead, null);
    }

    private void OnPipeRead(IAsyncResult ar) {
      Console.Write("Dados chagando...");
      var bytesRead = _pipeServer.EndRead(ar);
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
