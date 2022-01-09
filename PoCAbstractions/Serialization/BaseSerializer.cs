using Messages;

namespace PoCAbstractions.Serialization {
  public abstract class BaseSerializer {
    public abstract byte[] Serialize(BaseItemMessage obj);

    public abstract BaseItemMessage Deserialize(byte[] serializedMessage);
  }
}
