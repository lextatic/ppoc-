using System;
using UnityEngine;

public class ColorHelper
{
	public static Color ConvertFromConsoleColor(ConsoleColor color)
	{
		switch (color)
		{
			case ConsoleColor.Black:
				return Color.black;

			case ConsoleColor.DarkBlue:
				return new Color(0, 0, 139f / 255f, 1);

			case ConsoleColor.DarkGreen:
				return new Color(0, 100f / 255f, 0, 1);

			case ConsoleColor.DarkCyan:
				return new Color(0, 139f / 255f, 139f / 255f, 1);

			case ConsoleColor.DarkRed:
				return new Color(139f / 255f, 0, 0, 1);

			case ConsoleColor.DarkMagenta:
				return new Color(139f / 255f, 0, 139f / 255f, 1);

			case ConsoleColor.DarkYellow:
				return new Color(171f / 255f, 145f / 255f, 68f / 255f, 1);

			case ConsoleColor.Gray:
				return Color.gray;

			case ConsoleColor.DarkGray:
				return new Color(169f / 255f, 169f / 255f, 169f / 255f, 1);

			case ConsoleColor.Blue:
				return Color.blue;

			case ConsoleColor.Green:
				return Color.green;

			case ConsoleColor.Cyan:
				return Color.cyan;

			case ConsoleColor.Red:
				return Color.red;

			case ConsoleColor.Magenta:
				return Color.magenta;

			case ConsoleColor.Yellow:
				return Color.yellow;

			default:
			case ConsoleColor.White:
				return Color.white;
		}
	}
}
