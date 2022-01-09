using GameEntities.Items;
using Messages;

namespace GameEntities {

  [Serializable]
  public class Ball : BaseItem {
    public byte Color { get; set; } = (int)ConsoleColor.White;
  }

  [Serializable]
  public class RequestBallColorMessage : BaseItemMessage {
  }

  [Serializable]
  public class ChangeBallColorMessage : BaseItemMessage {
    public byte Color { get; set; }
  }
}