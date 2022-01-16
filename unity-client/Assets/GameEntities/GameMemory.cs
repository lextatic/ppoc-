using GameEntities.Items;
using System.Collections.Concurrent;

namespace GameEntities
{
	/// <summary>
	/// Esta classe contém todos os itens de jogo do client
	/// Para Unity, contém código para manipular os itens no engine.
	/// Para server, pode ser um Redis.
	/// </summary>
	public class GameMemory
	{
		// Não deveria ser público, só para facilitar o dump do estado do jogo
		public readonly ConcurrentDictionary<long, BaseItem> _items = new ConcurrentDictionary<long, BaseItem>();

		/// <summary>
		/// Coloca um item na memória de jogo.
		/// </summary>
		/// <param name="item">Item a colocar</param>
		public void Put(BaseItem item)
		{
			_items[item.Id] = item;
		}

		/// <summary>
		/// Obtém um item da memória do jogo, se disponível.
		/// </summary>
		/// <typeparam name="T">Tipo de item desejado.</typeparam>
		/// <param name="itemId">Id do item desejado.</param>
		/// <returns>Instância do item ou <c>null</c> para item não encontrado.</returns>
		public T Get<T>(long itemId) where T : BaseItem
		{
			if (_items.TryGetValue(itemId, out var item))
			{
				return item as T;
			}

			return null;
		}
	}
}
