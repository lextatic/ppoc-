using Client;
using CustomSerializer;
using GameEntities;
using GameEntities.Items;
using GameEntities.PoC;
using System.Collections.Concurrent;

// PoG: Aguarda 1 segundo para dar tempo do server subir.
Thread.Sleep(1000);

// TODO: Utilizar injetor de dependência
var serializer = new CustomJsonSerialize();
using var transport = new PipeTransporterClient(serializer);

// Crio 4 bolas com IDs aleatórios e coloco na memória
var gameItems = new ConcurrentDictionary<long, BaseItem>();

InitializeBalls(gameItems);

// Escutando mensagens chegando
transport.MessageReceived += (sender, args) => {
  DumpBallsState(gameItems);
};

DumpBallsState(gameItems);

// MainLoop
SpinWait.SpinUntil(() => {
  var keyInfo = Console.ReadKey(true);

  switch (keyInfo.Key) {
    case ConsoleKey.D1:
      Console.ForegroundColor = ConsoleColor.Green;
      Console.WriteLine("Bola 1 selecionada");
      Console.ResetColor();
      transport.Send(new RequestBallColorMessage { ItemId = gameItems.Keys.ElementAt(0) });
      return false;
    case ConsoleKey.D2:
      Console.ForegroundColor = ConsoleColor.Green;
      Console.WriteLine("Bola 2 selecionada");
      Console.ResetColor();
      transport.Send(new RequestBallColorMessage { ItemId = gameItems.Keys.ElementAt(1) });
      return false;
    case ConsoleKey.D3:
      Console.ForegroundColor = ConsoleColor.Green;
      Console.WriteLine("Bola 3 selecionada");
      Console.ResetColor();
      transport.Send(new RequestBallColorMessage { ItemId = gameItems.Keys.ElementAt(2) });
      return false;
    case ConsoleKey.A:
      Console.ForegroundColor = ConsoleColor.Green;
      Console.Write("Todas as bolas selecionadas");
      Console.ResetColor();
      transport.Send(new RequestBallColorMessage { ItemId = gameItems.Keys.ElementAt(0) });
      transport.Send(new RequestBallColorMessage { ItemId = gameItems.Keys.ElementAt(1) });
      transport.Send(new RequestBallColorMessage { ItemId = gameItems.Keys.ElementAt(2) });
      return false;
    case ConsoleKey.C:
      Console.ForegroundColor = ConsoleColor.Magenta;
      Console.WriteLine("É... vá jogar LoL... vc é muito ruim pra jogar Bolas Action MMO!");
      Console.ResetColor();
      return true;
    default:
      Console.ForegroundColor = ConsoleColor.Red;
      Console.Write("Presta atenção, abestado!\r");
      Console.ResetColor();
      return false;
  }
});

// Server finalizado
Console.WriteLine("Server is closing.");

static void InitializeBalls(ConcurrentDictionary<long, BaseItem> gameItems) {
  var rand = new Random(DateTime.Now.Millisecond);
  var ball1 = new Ball { Id = rand.NextInt64(), Color = (int)ConsoleColor.White };
  var ball2 = new Ball { Id = rand.NextInt64(), Color = (int)ConsoleColor.White };
  var ball3 = new Ball { Id = rand.NextInt64(), Color = (int)ConsoleColor.White };

  gameItems[ball1.Id] = ball1;
  gameItems[ball2.Id] = ball2;
  gameItems[ball3.Id] = ball3;
}

static void DumpBallsState(ConcurrentDictionary<long, BaseItem> gameItems) {
  Console.ForegroundColor = ConsoleColor.Yellow;
  Console.WriteLine("Seja bem vindo ao Bolas Action MMO!");
  Console.WriteLine();

  Console.ForegroundColor = ConsoleColor.White;
  Console.WriteLine("Situação atual das bolas:");

  int ballIndex = 1;

  foreach (var kv in gameItems) {
    Console.ForegroundColor = ConsoleColor.Gray;
    Console.Write("Bola #");
    Console.ForegroundColor = ConsoleColor.White;
    Console.Write(ballIndex++);
    Console.ForegroundColor = ConsoleColor.Gray;
    Console.Write(", id ");
    Console.ForegroundColor = ConsoleColor.White;
    Console.Write($"0x{kv.Key:x16}");
    Console.ForegroundColor = ConsoleColor.Gray;
    Console.Write(", cor ");
    Console.ForegroundColor = (ConsoleColor)((Ball)kv.Value).Color;
    Console.WriteLine(Console.ForegroundColor);
  }

  Console.WriteLine();
  Console.ResetColor();
  Console.WriteLine("Pressione:");
  WriteMenuEntry('1', "Selecionar a bola #1");
  WriteMenuEntry('2', "Selecionar a bola #2");
  WriteMenuEntry('3', "Selecionar a bola #3");
  WriteMenuEntry('A', "Selecionar todas as bolas");
  WriteMenuEntry('C', "Sair");
  Console.ResetColor();
  Console.WriteLine();
}

static void WriteMenuEntry(char key, string text) {
  Console.ForegroundColor = ConsoleColor.Black;
  Console.BackgroundColor = ConsoleColor.White;
  Console.Write($"[{key}]");
  Console.ResetColor();
  Console.ForegroundColor = ConsoleColor.White;
  Console.WriteLine($" - {text}");
}