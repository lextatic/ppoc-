namespace GameEntities.Messages {
  /// <summary>
  /// Mensagem recebida com ordem de trocar a cor da bola
  /// </summary>
  [Serializable]
  public class ChangeBallColorMessage : BaseItemMessage {
    public byte Color { get; set; }

    public override void Invoke(BaseTransporter transporter, GameMemory gm) {
      var ball = gm.Get<Ball>(ItemId);

      if (ball != null) {
        ball.Color = Color;
      }
    }
  }
}
