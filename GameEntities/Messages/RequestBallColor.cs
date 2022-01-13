namespace GameEntities.Messages {
  /// <summary>
  /// Um client está requerendo uma nova cor para uma bola
  /// </summary>
  [Serializable]
  public class RequestBallColorMessage : BaseItemMessage {
    private static readonly Random _random = new(DateTime.Now.Millisecond);

    public override void Invoke(BaseTransporter transport, GameMemory gm) {
      var color = (byte)_random.Next(0, 16);

      Console.ForegroundColor = ConsoleColor.Cyan;
      Console.Write("Bola ");
      Console.ForegroundColor = ConsoleColor.White;
      Console.Write($"ID {ItemId:X16}");
      Console.ForegroundColor = ConsoleColor.Cyan;
      Console.Write(", cor ");
      Console.ForegroundColor = ConsoleColor.White;
      Console.WriteLine($"{color} ({(ConsoleColor)color})");
      Console.ResetColor();
      transport.Send(new ChangeBallColorMessage { ItemId = ItemId, Color = color });
    }
  }
}
