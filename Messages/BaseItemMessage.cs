namespace Messages {
  /// <summary>
  /// Toda mensagem de item herda desta classe
  /// </summary>
  [Serializable]
  public abstract class BaseItemMessage {
    /// <summary>
    /// Item sendo manipulado
    /// </summary>
    public long ItemId { get; set; }

    /// <summary>
    /// Quando esta mensagem for recebida, este método é executado
    /// </summary>
    /// <param name="transporter">Tranporte atual (pode ser client ou server, não faz diferença)</param>
    public abstract void Invoke(BaseTransporter transporter);
  }
}