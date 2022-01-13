using Messages;

namespace GameEntities.PoC {
  [Serializable]
  public class ChangeBallColorMessage : BaseItemMessage {
    public byte Color { get; set; }
  }
}
