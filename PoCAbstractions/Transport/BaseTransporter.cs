using Messages;
using PoCAbstractions.Serialization;

namespace PoCAbstractions.Transport {
  public abstract class BaseTransporter : IDisposable {
    protected readonly BaseSerializer _serializer;

    public BaseTransporter(BaseSerializer serializer) {
      _serializer = serializer;
    }

    public event EventHandler<MessageEventArgs>?  MessageReceived;

    public void Send(BaseItemMessage message) {
      var serializedMessage = _serializer.Serialize(message);

      OnSend(serializedMessage);
    }

    protected abstract void OnSend(byte[] serializedMessage);

    protected void Received(byte[] serializedMessage) {
      var deserializedMessage = _serializer.Deserialize(serializedMessage);

      MessageReceived?.Invoke(this, new MessageEventArgs(deserializedMessage));
    }

    public virtual void Dispose() {
      if (MessageReceived != null) {
        foreach (var invocation in MessageReceived.GetInvocationList()) {
          MessageReceived -= invocation as EventHandler<MessageEventArgs>;
        }
      }

      GC.SuppressFinalize(this);
    }
  }
}