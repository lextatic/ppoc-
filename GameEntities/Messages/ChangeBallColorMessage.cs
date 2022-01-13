using Messages;

namespace GameEntities.Messages {
  /// <summary>
  /// Mensagem recebida com ordem de trocar a cor da bola
  /// </summary>
  [Serializable]
  public class ChangeBallColorMessage : BaseItemMessage {
    public byte Color { get; set; }

    public override void Invoke(BaseTransporter transporter) {
      Console.WriteLine("TO AQUI ChangeBallColorMessage!");
    }
  }
}
