using CustomSerializer;
using GameEntities;
using GameEntities.Messages;
using SocketTransporter;
using System;
using System.Threading;
using TypeManager;

{
	// PoG: Aguarda 1 segundo para dar tempo do server subir.
	Thread.Sleep(1000);

	// TODO: Utilizar injetor de dependência
	var serializer = new CustomJsonSerialize();
	using var transporter = new SocketTransporterClient(serializer);

	// Talvez o injetor de dependências seja capaz de resolver tipos por nome
	// Mas, em qualquer caso, vai uma implementação porca:
	TypeManagerTabajara.RegisterClass<ChangeBallColorMessage>();
	TypeManagerTabajara.RegisterClass<RequestBallColorMessage>();

	// Criando 3 bolas
	var rand = new Random(DateTime.Now.Millisecond);
	var ball1 = new Ball { Id = rand.Next(), Color = (int)ConsoleColor.White };
	var ball2 = new Ball { Id = rand.Next(), Color = (int)ConsoleColor.White };
	var ball3 = new Ball { Id = rand.Next(), Color = (int)ConsoleColor.White };

	// Criando memória do jogo (local somente (ou REDIS readonly talvez))
	var gameMemory = new GameMemory();

	// Adicionando bolas à memória do jogo
	gameMemory.Put(ball1);
	gameMemory.Put(ball2);
	gameMemory.Put(ball3);

	// Escutando mensagens chegando
	transporter.MessageReceived += (sender, e) =>
	{
		e.Message.Invoke(transporter, gameMemory);
		DumpBallsState(gameMemory);
	};

	DumpBallsState(gameMemory);

	// MainLoop
	SpinWait.SpinUntil(() =>
	{
		var keyInfo = Console.ReadKey(true);

		switch (keyInfo.Key)
		{
			case ConsoleKey.D1:
				Console.ForegroundColor = ConsoleColor.Green;
				Console.WriteLine("Bola 1 selecionada");
				Console.ResetColor();
				transporter.Send(new RequestBallColorMessage { ItemId = ball1.Id });
				return false;
			case ConsoleKey.D2:
				Console.ForegroundColor = ConsoleColor.Green;
				Console.WriteLine("Bola 2 selecionada");
				Console.ResetColor();
				transporter.Send(new RequestBallColorMessage { ItemId = ball2.Id });
				return false;
			case ConsoleKey.D3:
				Console.ForegroundColor = ConsoleColor.Green;
				Console.WriteLine("Bola 3 selecionada");
				Console.ResetColor();
				transporter.Send(new RequestBallColorMessage { ItemId = ball3.Id });
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

	static void DumpBallsState(GameMemory gameMemory)
	{
		Console.ForegroundColor = ConsoleColor.Yellow;
		Console.WriteLine("Seja bem vindo ao Bolas Action MMO!");
		Console.WriteLine();

		Console.ForegroundColor = ConsoleColor.White;
		Console.WriteLine("Situação atual das bolas:");

		int ballIndex = 1;

		foreach (var kv in gameMemory._items)
		{
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

	static void WriteMenuEntry(char key, string text)
	{
		Console.ForegroundColor = ConsoleColor.Black;
		Console.BackgroundColor = ConsoleColor.White;
		Console.Write($"[{key}]");
		Console.ResetColor();
		Console.ForegroundColor = ConsoleColor.White;
		Console.WriteLine($" - {text}");
	}
}