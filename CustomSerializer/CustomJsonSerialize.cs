using Messages;
using PoCAbstractions.Serialization;
using System.Text;
using System.Text.Json;

namespace CustomSerializer {
  public class CustomJsonSerialize : BaseSerializer {
    public override byte[] Serialize(BaseItemMessage obj) {
      var message = $"{obj.GetType().FullName}\r\n{JsonSerializer.Serialize<object>(obj, new JsonSerializerOptions { WriteIndented = true })}";

      return Encoding.UTF8.GetBytes(message);
    }

    public override BaseItemMessage Deserialize(byte[] serializedMessage) {
      using var textReader = new StringReader(Encoding.UTF8.GetString(serializedMessage!));
      var messageType = Type.GetType(textReader.ReadLine()!)!;
      var messageContents = textReader.ReadToEnd();

      return (BaseItemMessage)JsonSerializer.Deserialize(messageContents, messageType)!;
    }
  }
}