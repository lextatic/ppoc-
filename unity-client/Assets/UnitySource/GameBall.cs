using System;
using UnityEngine;

public class GameBall : MonoBehaviour
{
	private Color _newColor;
	private bool _updateColor = false;

	public void ChangeColor(ConsoleColor newColor)
	{
		// Forma "simplificada" de jogar para a MainThread.
		_newColor = ColorHelper.ConvertFromConsoleColor(newColor);
		_updateColor = true;
	}

	void Update()
	{
		if (_updateColor)
		{
			GetComponent<Renderer>().material.SetColor("_Color", _newColor);
			_updateColor = false;
		}
	}
}
