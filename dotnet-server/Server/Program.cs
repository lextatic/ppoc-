using CustomSerializer;
using GameEntities;
using GameEntities.Messages;
using NamedPipesTransporter;
using TypeManager;

// TODO: Utilizar injetor de dependência
var serializer = new CustomJsonSerialize();
using var transporter = new PipeTransporterServer(serializer);

// TODO: ObjectPool pra tudo!!! Diferentemente de RunUO, não podemos ter instâncias em memória o tempo todo
// Então é importante ter object pool para reaproveitar objetos sem precisar aguardar o GC!!!

// Talvez o injetor de dependências seja capaz de resolver tipos por nome
// Mas, em qualquer caso, vai uma implementação porca:
TypeManagerTabajara.RegisterClass<ChangeBallColorMessage>();
TypeManagerTabajara.RegisterClass<RequestBallColorMessage>();

Console.WriteLine("Server iniciado. Pressione [C] para parar.");

// Criando memória do jogo (REDIS)
var gameMemory = new GameMemory();

transporter.MessageReceived += (sender, e) => {
  Console.ForegroundColor = ConsoleColor.Magenta;
  Console.Write("Mensagem recebida: ");
  Console.ForegroundColor = ConsoleColor.White;
  Console.WriteLine(e.Message.GetType().FullName);
  e.Message.Invoke(transporter, gameMemory);
};

SpinWait.SpinUntil(() => {
  var keyInfo = Console.ReadKey(true);

  return keyInfo.Key == ConsoleKey.C;
});

Console.WriteLine("Parando...");