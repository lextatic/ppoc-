using CustomSerializer;
using GameEntities;
using GameEntities.Messages;
using SocketTransporter;
using System;
using System.Collections.Generic;
using System.Linq;
using TypeManager;
using UnityEngine;

public class Program : MonoBehaviour
{
	SocketTransporterClient _transporter;

	[SerializeField]
	GameBall[] _gameBalls;

	Dictionary<Ball, GameBall> _ballGameObject = new Dictionary<Ball, GameBall>();

	private void Start()
	{

		var serializer = new CustomJsonSerialize();
		var transporter = new SocketTransporterClient(serializer);

		_transporter = transporter;

		// Talvez o injetor de dependências seja capaz de resolver tipos por nome
		// Mas, em qualquer caso, vai uma implementação porca:
		TypeManagerTabajara.RegisterClass<ChangeBallColorMessage>();
		TypeManagerTabajara.RegisterClass<RequestBallColorMessage>();

		// Criando 3 bolas
		var ball1 = new Ball { Id = UnityEngine.Random.Range(0, int.MaxValue), Color = (int)ConsoleColor.White };
		var ball2 = new Ball { Id = UnityEngine.Random.Range(0, int.MaxValue), Color = (int)ConsoleColor.White };
		var ball3 = new Ball { Id = UnityEngine.Random.Range(0, int.MaxValue), Color = (int)ConsoleColor.White };

		_ballGameObject.Add(ball1, _gameBalls[0]);
		_ballGameObject.Add(ball2, _gameBalls[1]);
		_ballGameObject.Add(ball3, _gameBalls[2]);

		// Criando memória do jogo (local somente (ou REDIS readonly talvez))
		var gameMemory = new GameMemory();

		// Adicionando bolas à memória do jogo
		gameMemory.Put(ball1);
		gameMemory.Put(ball2);
		gameMemory.Put(ball3);

		// Escutando mensagens chegando
		_transporter.MessageReceived += (sender, e) =>
		{
			e.Message.Invoke(_transporter, gameMemory);
			DumpBallsState(gameMemory);
		};

		DumpBallsState(gameMemory);
	}

	void DumpBallsState(GameMemory gameMemory)
	{
		foreach (var kv in gameMemory._items)
		{
			_ballGameObject[(Ball)kv.Value].ChangeColor((ConsoleColor)((Ball)kv.Value).Color);
		}

		//Console.WriteLine();
		//Console.ResetColor();
		//Console.WriteLine("Pressione:");
		//WriteMenuEntry('1', "Selecionar a bola #1");
		//WriteMenuEntry('2', "Selecionar a bola #2");
		//WriteMenuEntry('3', "Selecionar a bola #3");
		//WriteMenuEntry('A', "Selecionar todas as bolas");
		//WriteMenuEntry('C', "Sair");
		//Console.ResetColor();
		//Console.WriteLine();
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			_transporter.Send(new RequestBallColorMessage { ItemId = FindBallByIndex(0).Id });
		}

		if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			_transporter.Send(new RequestBallColorMessage { ItemId = FindBallByIndex(1).Id });
		}

		if (Input.GetKeyDown(KeyCode.Alpha3))
		{
			_transporter.Send(new RequestBallColorMessage { ItemId = FindBallByIndex(2).Id });
		}

		if (Input.GetKeyDown(KeyCode.Alpha4))
		{
			foreach (var kv in _ballGameObject.Keys)
			{
				_transporter.Send(new RequestBallColorMessage { ItemId = kv.Id });
			}
		}
	}

	private Ball FindBallByIndex(int index)
	{
		return _ballGameObject.FirstOrDefault(x => x.Value == _gameBalls[index]).Key;
	}

	private void OnDestroy()
	{
		_transporter.Dispose();
	}
}
