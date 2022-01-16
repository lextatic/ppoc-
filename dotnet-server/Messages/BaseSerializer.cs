namespace Messages {
  /// <summary>
  /// Serializador
  /// </summary>
  public abstract class BaseSerializer {
    /// <summary>
    /// Serializa um objeto.
    /// </summary>
    /// <param name="obj">Objeto a serializar.</param>
    /// <returns>Bytes contendo o objeto serializado.</returns>
    public abstract byte[] Serialize(BaseItemMessage obj);

    /// <summary>
    /// Deserializa um objeto.
    /// </summary>
    /// <param name="serializedMessage">Bytes contendo o objeto serializado.</param>
    /// <returns>Objeto deserializado.</returns>
    public abstract BaseItemMessage Deserialize(byte[] serializedMessage);
  }
}
