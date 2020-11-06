using System.Collections.Generic;

namespace Xcom
{
	public class Actions
	{
		public Command[] commands = new Command[Constants.Actions];
		public Queue<Command> TurnCommands = new Queue<Command>(); 

		public enum ActionState
		{
			FirstAction, SecondAction, Done
		};

		public ActionState State = ActionState.FirstAction;

		//bloated bad system to queue up commands before they are committed to the turn queue.
		public void AddAction(Command action)
		{
			switch (State)
			{
				case ActionState.FirstAction:
					AddFirstAction(action);
					break;
				case ActionState.SecondAction:
					AddSecondAction(action);
					break;
				case ActionState.Done:
					break;
				default:
					break;
			}
		}

		//more Bad things
		public void AddFirstAction(Command action)
		{
			commands[0] = action;
			State = ActionState.SecondAction;
		}

		public void AddSecondAction(Command action)
		{
			commands[1] = action;
			State = ActionState.Done;
		}

		public void WipeActions()
		{
			State = ActionState.FirstAction;
			for (int i = 0; i < commands.Length; i++)
			{
				commands[i] = null;
			}
		}
		//Executes one command at the time from the queue and the dequeues it, so it only has to be called again
		//to play the next step, proud of this one.
		public bool ExecuteAction()
		{
			TurnCommands.Peek().Execute();
			TurnCommands.Dequeue();
			if (TurnCommands.Count > 0)
				return true;
			else
				return false;
		}


		//This commits the commands the player has selected to the turn, it looks weird because
		//I had to filter out "empty" commands that would break the system if entered.
		//Proud of this one as well
		public void CommitActions()
		{
			foreach (var command in commands)
			{
				if (command == null)
					continue;
				TurnCommands.Enqueue(command);
			}
			Replay.AddCommands(commands);
			WipeActions();
		}

	}
}

