using CustomSerializer;
using GameEntities.PoC;
using Server;
using TypeManager;

// TODO: Utilizar injetor de dependência
var serializer = new CustomJsonSerialize();
using var transport = new PipeTransporterServer(serializer);

// TODO: ObjectPool pra tudo!!! Diferentemente de RunUO, não podemos ter instâncias em memória o tempo todo
// Então é importante ter object pool para reaproveitar objetos sem precisar aguardar o GC!!!

// Talvez o injetor de dependências seja capaz de resolver tipos por nome
// Mas, em qualquer caso, vai uma implementação porca:
TypeManagerTabajara.RegisterClass<ChangeBallColorMessage>();
TypeManagerTabajara.RegisterClass<RequestBallColorMessage>();

Console.WriteLine("Server iniciado. Pressione [C] para parar.");

SpinWait.SpinUntil(() => {
  var keyInfo = Console.ReadKey(true);

  return keyInfo.Key == ConsoleKey.C;
});

Console.WriteLine("Parando...");