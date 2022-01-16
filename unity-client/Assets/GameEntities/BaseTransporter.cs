using System;

namespace GameEntities
{
	/// <summary>
	/// Classe de transporte (aka "rede").
	/// </summary>
	public abstract class BaseTransporter : IDisposable
	{
		protected readonly BaseSerializer _serializer;

		public BaseTransporter(BaseSerializer serializer)
		{
			_serializer = serializer;
		}

		/// <summary>
		/// Uma mensagem foi recebida
		/// </summary>
		public event EventHandler<MessageEventArgs> MessageReceived;


		/// <summary>
		/// Envia uma mensagem para a outra ponta (estamos desconsiderando broadcast, rede, etc.)
		/// </summary>
		/// <param name="message">Mensagem a enviar</param>
		public void Send(BaseItemMessage message)
		{
			var serializedMessage = _serializer.Serialize(message);

			OnSend(serializedMessage);
		}

		/// <summary>
		/// Transportar esta mensagem.
		/// </summary>
		/// <param name="serializedMessage">Mensagem já serializada.</param>
		protected abstract void OnSend(byte[] serializedMessage);

		/// <summary>
		/// Uma mensagem foi recebida.
		/// </summary>
		/// <param name="serializedMessage"></param>
		protected void Received(byte[] serializedMessage)
		{
			var deserializedMessage = _serializer.Deserialize(serializedMessage);

			MessageReceived?.Invoke(this, new MessageEventArgs(deserializedMessage));
		}

		/// <summary>
		/// Limpa o transporte.
		/// </summary>
		public virtual void Dispose()
		{
			if (MessageReceived != null)
			{
				foreach (var invocation in MessageReceived.GetInvocationList())
				{
					MessageReceived -= invocation as EventHandler<MessageEventArgs>;
				}
			}

			GC.SuppressFinalize(this);
		}
	}
}