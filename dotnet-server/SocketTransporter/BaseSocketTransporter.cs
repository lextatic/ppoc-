using GameEntities;

namespace SocketTransporter
{
	public class BaseSocketTransporter : BaseTransporter
	{
		protected readonly byte[] _buffer = new byte[65536];

		public BaseSocketTransporter(BaseSerializer serializer) : base(serializer) { }

		protected override void OnSend(byte[] serializedMessage)
		{

		}
	}
}
