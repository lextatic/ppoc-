using Messages;

namespace PoCAbstractions.Transport {
  public sealed class MessageEventArgs : EventArgs {
    public BaseItemMessage Message { get; }

    public MessageEventArgs(BaseItemMessage message) {
      Message = message;
    }
  }
}
