using System;

namespace GameEntities
{
	/// <summary>
	/// Toda mensagem de item herda desta classe
	/// </summary>
	[Serializable]
	public abstract class BaseItemMessage
	{
		/// <summary>
		/// Item sendo manipulado
		/// </summary>
		public long ItemId { get; set; }

		/// <summary>
		/// Quando esta mensagem for recebida, este método é executado (server ou client).
		/// </summary>
		/// <param name="transporter">Tranporte atual.</param>
		/// <param name="gameMemory">Repositório onde é possível obter os itens de jogo.</param>
		public abstract void Invoke(BaseTransporter transporter, GameMemory gameMemory);
	}
}