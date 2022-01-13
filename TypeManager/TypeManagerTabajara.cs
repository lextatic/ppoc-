namespace TypeManager {
  public static class TypeManagerTabajara {
    private static readonly Dictionary<string, Type> _registry = new();

    public static void RegisterClass<T>() {
      var type = typeof(T);

      _registry.Add(type.FullName!, type);
    }

    public static Type Get(string typeName) {
      if (_registry.TryGetValue(typeName, out var type) == false) {
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