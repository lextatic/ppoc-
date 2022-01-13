namespace GameEntities.Items {

  /// <summary>
  /// Item básico do jogo (todos itens herdam esta classe)
  /// </summary>
  [Serializable]
  public abstract class BaseItem {
    public long Id { get; set; }
  }
}
