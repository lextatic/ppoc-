using System;
using System.Collections.Generic;

namespace TypeManager
{
	/// <summary>
	/// Mapeador de strings -> tipos
	/// </summary>
	public static class TypeManagerTabajara
	{
		private static readonly Dictionary<string, Type> _registry = new Dictionary<string, Type>();

		/// <summary>
		/// Registra uma classe para ser localizada via nome.
		/// </summary>
		/// <typeparam name="T">Classe a registrar</typeparam>
		public static void RegisterClass<T>()
		{
			var type = typeof(T);

			_registry.Add(type.FullName, type);
		}

		/// <summary>
		/// Obtém um tipo a partir de um nome.
		/// </summary>
		/// <param name="typeName">Nome do tipo.</param>
		/// <returns>Tipo, se registrado.</returns>
		/// <exception cref="TypeAccessException">Tipo não encontrado.</exception>
		public static Type Get(string typeName)
		{
			if (_registry.TryGetValue(typeName, out var type) == false)
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.Write("Tipo de entidade não encontrado:");
				Console.ForegroundColor = ConsoleColor.Yellow;
				Console.WriteLine(typeName);
				Console.ResetColor();
				Console.ReadLine();
				throw new TypeAccessException($"Type not found: ${typeName}");
			}

			return type;
		}
	}
}