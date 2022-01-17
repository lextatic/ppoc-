using GameEntities;
using Newtonsoft.Json;
using System.IO;
using System.Text;
using TypeManager;

namespace CustomSerializer
{
	/// <summary>
	/// Serializador custom que injeta o nome do tipo sendo manipulado
	/// </summary>
	public class CustomJsonSerialize : BaseSerializer
	{
		public override byte[] Serialize(BaseItemMessage obj)
		{
			var message = $"{obj.GetType().FullName}\r\n{JsonConvert.SerializeObject(obj, Formatting.Indented)}";

			return Encoding.UTF8.GetBytes(message);
		}

		public override BaseItemMessage Deserialize(byte[] serializedMessage)
		{
			using (var textReader = new StringReader(Encoding.UTF8.GetString(serializedMessage)))
			{
				var messageTypeName = textReader.ReadLine();
				var messageContents = textReader.ReadToEnd();
				var messageType = TypeManagerTabajara.Get(messageTypeName);

				return (BaseItemMessage)JsonConvert.DeserializeObject(messageContents, messageType);
			}
		}
	}
}
