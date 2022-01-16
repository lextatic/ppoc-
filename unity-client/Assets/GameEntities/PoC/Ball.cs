using GameEntities.Items;
using System;

namespace GameEntities
{

	[Serializable]
	public class Ball : BaseItem
	{
		public byte Color { get; set; } = (int)ConsoleColor.White;
	}
}