namespace GameEntities {
  /// <summary>
  /// Mensagem recebida no transporte.
  /// </summary>
  public sealed class MessageEventArgs : EventArgs {
    /// <summary>
    /// Mensagem recebida.
    /// </summary>
    public BaseItemMessage Message { get; }

    public MessageEventArgs(BaseItemMessage message) {
      Message = message;
    }
  }
}
