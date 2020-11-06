using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Xcom
{
	public class Replay : MonoBehaviour
	{
		static List<Command> ReplayCommands = new List<Command>();

		private int index = 0;


		//I had planned to store all moves and player starting conditions so the "match" could be replayed
		//I didn't get that far.

		public static void AddCommands(Command[] commands)
		{
			foreach (var command in commands)
			{
				ReplayCommands.Add(command);
			} 
		}

		public static void AddCommands(Command command)
		{
			ReplayCommands.Add(command);
		}


		public void ClearCommands()
		{
			ReplayCommands.Clear();
		}

		public void SkipTo(int desiredTurn)
		{
			for (int i = index; i < desiredTurn; i++)
			{
				ReplayCommands[i].Execute();
			}
		}

		public void PlayNextCommand()
		{
			ReplayCommands[index].Execute();
			index++;
		}
	}
}

